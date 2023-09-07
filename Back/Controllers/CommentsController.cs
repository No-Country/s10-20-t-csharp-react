using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using s10.Back.Data;
using s10.Back.Data.Repositories;
using s10.Back.DTO;

namespace quejapp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{   

    private readonly ILogger<CommentsController> _logger;
    private readonly RedCoContext _context;
    //private List<Comment> _comments;
    public CommentsController(ILogger<CommentsController> logger, RedCoContext context)
    {
        _logger = logger;
        _context = context;       
    }          

    [HttpDelete("{id}")]
    [Authorize]
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
        //var elComentario = await _context.Comment.FindAsync(id);
        //if (elComentario is null)
        //    return NotFound();

        //_context.Comment.Remove(elComentario);
        //var changes = await _context.SaveChangesAsync();
        #endregion

        #region WithUnitOfWorkPattern
        var unitOfWork = new UnitOfWork(_context);
        var comment = unitOfWork.Comments.Get(id);
        if(comment is not null)
            unitOfWork.Comments.Remove(comment);
        int changes = await unitOfWork.Complete();
        #endregion
        if (changes > 0)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }        
}