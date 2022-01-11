using AspNetCoreODataIssues.Data;
using AspNetCoreODataIssues.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreODataIssues.Controllers;

public class AnimalController : ODataEntityControllerBase<Animal>
{
    public AnimalController(ApplicationDbContext context) : base(context)
    {
    }
    
    protected override DbSet<Animal> _entities => _context.Animals;
         
    protected override Animal? GetEntityById(int id)
    {
        return _entities.SingleOrDefault(e => e.AnimalId == id);
    }
}