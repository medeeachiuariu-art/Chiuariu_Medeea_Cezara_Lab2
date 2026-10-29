using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Chiuariu_Medeea_Cezara_Lab2.Models;
using Chiuariu_Medeea_Cezara_Lab2.ViewModels;

namespace Chiuariu_Medeea_Cezara_Lab2.Pages.PublisherPages;

public class IndexModel : PageModel
{
    private readonly Chiuariu_Medeea_Cezara_Lab2Context _context;

    public IndexModel(Chiuariu_Medeea_Cezara_Lab2Context context)
    {
        _context = context;
    }

    public IList<Publisher> Publisher { get; set; } = default!;
    public PublisherIndexData PublisherData { get; set; } = default!;
    public int PublisherID { get; set; }
    public int BookID { get; set; }

    public async Task OnGetAsync(int? id, int? bookID)
    {
        PublisherData = new PublisherIndexData();

        PublisherData.Publishers = await _context.Publisher
            .Include(i => i.Books)
                .ThenInclude(c => c.Author)
            .OrderBy(i => i.PublisherName)
            .ToListAsync();

        if (id != null)
        {
            PublisherID = id.Value;

            Publisher publisher = PublisherData.Publishers
                .Where(i => i.ID == id.Value)
                .Single();

            PublisherData.Books = publisher.Books;
        }
    }
}