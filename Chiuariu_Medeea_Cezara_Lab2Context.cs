using Microsoft.EntityFrameworkCore;

public class Chiuariu_Medeea_Cezara_Lab2Context(
    DbContextOptions<Chiuariu_Medeea_Cezara_Lab2Context> options)
    : DbContext(options)
{
    public DbSet<Chiuariu_Medeea_Cezara_Lab2.Models.Book> Book { get; set; } = default!;

    public DbSet<Chiuariu_Medeea_Cezara_Lab2.Models.Publisher> Publisher { get; set; } = default!;
    public DbSet<Chiuariu_Medeea_Cezara_Lab2.Models.Authors> Authors { get; set; } = default!;
}