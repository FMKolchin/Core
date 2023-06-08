using System.ComponentModel.DataAnnotations.Schema;

namespace _4.Models;

public class TaskTODO
{
    public string? Id { get; set; }

    public string? Description { get; set; }

    public bool Status { get; set; }

    [ForeignKey("User")]
    public string? User {get;set;}

}