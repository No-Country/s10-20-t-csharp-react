using quejapp.Models;
using s10.Back.DTO;

namespace s10.Back.Data.IRepositories
{
    public interface IQuejaRepository : IGenericRepository<Queja>
    {
        PagedList<QuejaResponseDTO> GetFeed(QuejaRequestDTO model);
        PagedList<QuejaResponseDTO>? GetPaged(int id);
        Task<Queja> Update(QuejaDTO model, int quejaId);

    }
}
