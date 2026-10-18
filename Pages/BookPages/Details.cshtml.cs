using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Chiuariu_Medeea_Cezara_Lab2.Models;

namespace Chiuariu_Medeea_Cezara_Lab2.Pages.BookPages;

public class DetailsModel : PageModel
{
    private readonly Chiuariu_Medeea_Cezara_Lab2Context _context;
    public DetailsModel(Chiuariu_Medeea_Cezara_Lab2Context context)
    {
        _context = context;
    }

    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var book = await _context.Book
  .Include(b => b.Author)
  .Include(b => b.Publisher)
  .FirstOrDefaultAsync(m => m.ID == id);
        if (book is null)
        {
            return NotFound();
        }
        else
        {
            Book = book;
        }

        return Page();
    }
}
