using AspNetCoreODataIssues.Data;
using AspNetCoreODataIssues.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreODataIssues.Controllers;

public class PersonController : ODataEntityControllerBase<Person>
{
    public PersonController(ApplicationDbContext context) : base(context)
    {
    }
    
    protected override DbSet<Person> _entities => _context.People;
         
    protected override Person? GetEntityById(int id)
    {
        return _entities.SingleOrDefault(e => e.PersonId == id);
    }
    
    [HttpPatch("odata/Person({id:int})")]
    public override ActionResult Patch(int id, Delta<Person> patch)
    {
        return base.Patch(id, patch);
    }
}