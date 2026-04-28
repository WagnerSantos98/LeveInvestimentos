using System;
using LeveInvestimentos.Domain.Enums;

namespace LeveInvestimentos.Domain.Entities;

public class AppTask
{
    public int Id {get; set;}
    public string Description {get; set;} = string.Empty;
    public DateTime DueDate {get; set;}
    public AppTaskStatus Status {get; set;} = AppTaskStatus.Pedding;

    // Relacionamentos
    public int CreatedById {get; set;}
    public AppUser? CreatedBy {get; set;}
    public int AssignedToId {get; set;}
    public AppUser? AssignedTo {get; set;}

}