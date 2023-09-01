using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using quejapp.Data;
using quejapp.DTO;
using quejapp.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Newtonsoft.Json;

namespace quejapp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{   

    private readonly ILogger<CommentsController> _logger;
    private readonly RedCoContext _context;
    private List<Comment> _comments;
    public CommentsController(ILogger<CommentsController> logger, RedCoContext context)
    {
        _logger = logger;
        _context = context;       
    }          

    //[Authorize(Roles = RoleNames.Administrator)] // solo el admin o moderador podría eliminar un comentario
    [HttpDelete("{id}")]
    [ResponseCache(CacheProfileName = "NoCache")]
    public async Task<ActionResult<PagedListResponse<CommentResponseDTO>>> Delete(int id)
    {
        #region WithFiles
        //if (_comments is null)
        //{
        //    _comments = JsonConvert.DeserializeObject<List<Comment>>(System.IO.File.ReadAllText(@"./Back/Data/comments.json"))!;
        //}
        ////var qComentario = _context.Comment.Where(c => c.Comment_ID == id);
        //var elComentario = _comments.Where(c => c.Comment_ID == id);

        //if (/*qComentario.Any()*/ elComentario.Any())
        //{
        //    var comment = elComentario.First();
        //    var qComentario = _comments.Remove(comment);

        //    int changes = 1;
        #endregion

        #region SQLServer
        var elComentario = await _context.Comment.FindAsync(id);
        if (elComentario is null)
            return NotFound();

        _context.Comment.Remove(elComentario);
        var changes = await _context.SaveChangesAsync();
        #endregion
        if (changes > 0)
        {
            var commentResponse = JsonConvert.DeserializeObject<CommentResponseDTO>(JsonConvert.SerializeObject(elComentario));
            var paged = PagedList<CommentResponseDTO>.Create((new List<CommentResponseDTO> { commentResponse! }).AsQueryable(), 1, 1);
            return new PagedListResponse<CommentResponseDTO>(
                paged,
                (HttpContext.GetEndpoint() as RouteEndpoint)!.RoutePattern.RawText!);
        }
        else
        {
            return BadRequest();
        }
    }        
}