using System;
using System.Collections.Generic;

namespace LeveInvestimentos.Domain.Entities;

public class AppUser
{
    public int Id {get; set;}
    public string FullName {get; set;} = string.Empty;
    public DateTime BirthDate {get; set;}

    public string Phone {get; set;} = string.Empty;

    public string MobilePhone {get; set;} = string.Empty;

    public string Email {get; set;} = string.Empty;

    public string Address {get; set;} = string.Empty;
    public string? PhotoPath {get; set;}
    public bool IsManager {get; set;}
    public string Password {get; set;} = string.Empty;

    // Propriedades de navegação
    public ICollection<AppTask> CreatedTasks {get; set;} = new List<AppTask>();
    public ICollection<AppTask> AssignedTasks {get; set;} = new List<AppTask>();





}
