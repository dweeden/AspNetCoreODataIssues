using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreODataIssues.Models;

[Table("People")]
public class Person
{
    [Key]
    public int PersonId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}