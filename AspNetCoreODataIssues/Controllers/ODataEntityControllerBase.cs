using AspNetCoreODataIssues.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreODataIssues.Controllers;

public abstract class ODataEntityControllerBase<T> : ODataController where T : class
{
    protected readonly ApplicationDbContext _context;

    protected abstract T? GetEntityById(int id);

    protected abstract DbSet<T> _entities { get; }

    protected ODataEntityControllerBase(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [EnableQuery]
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_entities);
    }

    /*[EnableQuery]
    [HttpGet("odata/[controller]({id:int})")]
    public IActionResult Get(int id)
    {
        var entity = GetEntityById(id);
	        
        if (entity == null)
        {
            return NotFound();
        }

        return Ok(entity);
    }*/
        
    [HttpPost]
    public IActionResult Post([FromBody] T entity)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // add to collection and save
        _entities.Add(entity);
        _context.SaveChanges();

        // return the entity
        return Created(entity);
    }

    public virtual ActionResult Patch(int id, [FromBody] Delta<T> patch)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var entity = GetEntityById(id);

        if (entity == null)
        {
            return NotFound();
        }

        // update entity and save
        patch.Patch(entity);
        _context.SaveChanges();
  	        
        return NoContent();
    }

    [HttpDelete("odata/[controller]({id:int})")]
    public IActionResult Delete(int id)
    {
        var entity = GetEntityById(id);

        if (entity == null)
        {
            return NotFound();
        }

        // delete entity and save
        _entities.Remove(entity);
        _context.SaveChanges();
  	        
        return Updated(entity);
    }
}