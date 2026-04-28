using Microsoft.EntityFrameworkCore;
using LeveInvestimentos.Domain.Entities;

namespace LeveInvestimentos.Infrastructure.Data;

public class LeveInvestimentosDbContext : DbContext
{
    public LeveInvestimentosDbContext(DbContextOptions<LeveInvestimentos> options) : base(options){}

    public
}