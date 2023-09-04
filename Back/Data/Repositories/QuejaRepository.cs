﻿using Microsoft.EntityFrameworkCore;
using quejapp.Data;
using quejapp.DTO;
using quejapp.Models;
using System.Linq.Dynamic.Core;
using s10.Back.Data.IRepositories;
using Microsoft.Extensions.Logging.Abstractions;
using CloudinaryDotNet.Actions;
using NetTopologySuite.Geometries;
using System.Security.Claims;
using s10.Back.Services;
using System.Security.Cryptography;
using s10.Back.Handler;

namespace s10.Back.Data.Repositories;

public class QuejaRepository : GenericRepository<Queja>, IQuejaRepository
{
    //private readonly ICloudinaryService _cloudinaryService;
    //private readonly GeometryFactory _geometryFactory;
    public QuejaRepository(RedCoContext context )//NO hacer injección POr constructor acá
        //ICloudinaryService cloudinaryService,
        //GeometryFactory geometryFactory))
        : base(context)
    {
        //_cloudinaryService = cloudinaryService;
        //_geometryFactory = geometryFactory;
    }

    public RedCoContext? RedCoContext
    {
        get { return Context as RedCoContext; }
    }

    public PagedList<QuejaResponseDTO> GetFeed(QuejaRequestDTO model)
    {
        var query = RedCoContext!.Queja.AsQueryable();

        if (model.Category_ID is not null)
            query = query.Where(q => q.Category_ID == model.Category_ID);
        if (model.District_ID is not null)
            query = query.Where(q => q.District_ID == model.District_ID);
        if (!string.IsNullOrEmpty(model.FilterQuery))
            query = query.Where(q => q.Title.Contains(model.FilterQuery));

        var viewFeed = query
            .Include(q => q.Category)
            .Include(q => q.District)
            .Include(q => q.User)
            .Select(q => new QuejaResponseDTO
            {
                Complaint_ID = q.Complaint_ID,
                Text = q.Text,
                Title = q.Title,
                PhotoAdress = q.PhotoAdress,
                VideoAddress = q.VideoAddress,
                District_Name = q.District.Name,
                UserName = q.User.Name,
                Category_Name = q.Category.Name,
                UserPhoto = q.User.ProfilePicAddress,
                CreatedAt = q.CreatedAt,
                Longitude = q.Location != null ? q.Location.X : 0,
                Latitude = q.Location != null ? q.Location.Y : 0

            }).OrderBy($"{model.SortColumn} {model.SortOrder}");

        return PagedList<QuejaResponseDTO>.Create(viewFeed, model.PageIndex, model.PageSize);
            
    }
    public PagedList<QuejaResponseDTO>? GetPaged(int id)
    {
        var query = RedCoContext!.Queja.Where(q => q.Complaint_ID == id);
        if (query.Any())
        {
            var quejaView = query
                .Include(q => q.Category)
                .Include(q => q.District)
                .Include(q => q.User)
                .Select(q => new QuejaResponseDTO
                {
                    Complaint_ID = q.Complaint_ID,
                    Text = q.Text,
                    Title = q.Title,
                    PhotoAdress = q.PhotoAdress,
                    VideoAddress = q.VideoAddress,
                    District_Name = q.District.Name,
                    UserName = q.User.Name,
                    Category_Name = q.Category.Name,
                    UserPhoto = q.User.ProfilePicAddress,
                    CreatedAt = q.CreatedAt,
                    Longitude = q.Location != null ? q.Location.X : 0,
                    Latitude = q.Location != null ? q.Location.Y : 0
                });

            return PagedList<QuejaResponseDTO>.Create(quejaView, 1, 1);
        }
        else
        {
            return null;
        }
    }
    public async Task<Queja> Update(QuejaDTO model, int qId)
    {
        var laQueja = Get(qId);
        if (laQueja is not null)
        {
          
            var _geometryFactory = new GeometryFactory();
            var _cloudinaryService = new CloudinaryHelper();
            IFormFile? file = model.media;
            var uploadResult = new ImageUploadResult();
            if (file is not null)
            {
                // si no es nulo la url de la foto se le pasa esta y se destruye
                var deletionResult = laQueja.PhotoAdress != null
                    ? await _cloudinaryService.DeletePhotoAsync(laQueja.PhotoAdress)
                    : null;
                // y se agrega otra foto y se la dirección de esta en al bd
                uploadResult = await _cloudinaryService.AddPhotoAsync(file!);     
            }
                    
            laQueja.PhotoAdress = uploadResult.SecureUrl is not null
                ? "https://res.cloudinary.com" + uploadResult.SecureUrl.AbsolutePath
                : laQueja.PhotoAdress;
            //hacer esto en el controlador
            //if (((int)laQueja.User_ID!) == null)
            //{                    
            //}
            // si no se hace esto, pueden quedarse nulas las propiedas ya existentes en la bd
            laQueja.Text = model.Text ?? laQueja.Text;
            laQueja.District_ID = model.District_ID ?? laQueja.District_ID;
            laQueja.Category_ID = model.Category_ID ?? laQueja.Category_ID;
            laQueja.Title = model.Title ?? laQueja.Title;
            laQueja.Location = (model.Longitude != null && model.Latitude != null) ? _geometryFactory.CreatePoint(new NetTopologySuite.Geometries.Coordinate(
                (double)model.Longitude, (double)model.Latitude)) : laQueja.Location;

            return laQueja;
        }
        else
        {
            return new Queja();
        }
    }
}
