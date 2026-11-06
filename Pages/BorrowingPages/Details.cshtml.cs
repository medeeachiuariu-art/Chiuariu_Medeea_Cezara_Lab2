using Chiuariu_Medeea_Cezara_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Chiuariu_Medeea_Cezara_Lab2.Pages.BorrowingPages;

public class DetailsModel : PageModel
{
    private readonly Chiuariu_Medeea_Cezara_Lab2Context _context;
    public DetailsModel(Chiuariu_Medeea_Cezara_Lab2Context context)
    {
        _context = context;
    }

    public Borrowing Borrowing { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var borrowing = await _context.Borrowing
    .Include(b => b.Member)
    .Include(b => b.Book)
        .ThenInclude(b => b.Author)
    .FirstOrDefaultAsync(m => m.ID == id);
        if (borrowing is null)
        {
            return NotFound();
        }
        else
        {
            Borrowing = borrowing;
        }

        return Page();
    }
}
