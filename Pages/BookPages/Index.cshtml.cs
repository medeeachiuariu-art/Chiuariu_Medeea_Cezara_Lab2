using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Chiuariu_Medeea_Cezara_Lab2.Models;

namespace Chiuariu_Medeea_Cezara_Lab2.Pages.BookPages;

public class IndexModel : PageModel
{
    private readonly Chiuariu_Medeea_Cezara_Lab2Context _context;

    public IndexModel(Chiuariu_Medeea_Cezara_Lab2Context context)
    {
        _context = context;
    }

    public IList<Book> Book { get; set; } = default!;

    public SelectList AuthorsList { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public int? AuthorID { get; set; }

    public async Task OnGetAsync()
    {
        AuthorsList = new SelectList(
            await _context.Authors
                .Select(a => new
                {
                    a.ID,
                    FullName = a.FirstName + " " + a.LastName
                })
                .ToListAsync(),
            "ID", "FullName");

        var books = _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Author)
            .AsQueryable();

        if (AuthorID.HasValue)
        {
            books = books.Where(b => b.AuthorID == AuthorID.Value);
        }

        Book = await books.ToListAsync();
    }
}