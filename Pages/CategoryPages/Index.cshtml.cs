using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Chiuariu_Medeea_Cezara_Lab2.Models;

namespace Chiuariu_Medeea_Cezara_Lab2.Pages.CategoryPages;

public class IndexModel : PageModel
{
    private readonly Chiuariu_Medeea_Cezara_Lab2Context _context;

    public IndexModel(Chiuariu_Medeea_Cezara_Lab2Context context)
    {
        _context = context;
    }

    public IList<Category> Category { get; set; } = default!;

    public IEnumerable<Book>? Books { get; set; }

    public int CategoryID { get; set; }

    public async Task OnGetAsync(int? id)
    {
        Category = await _context.Category
            .Include(c => c.BookCategories)
                .ThenInclude(bc => bc.Book)
                    .ThenInclude(b => b.Author)
            .OrderBy(c => c.CategoryName)
            .ToListAsync();

        if (id != null)
        {
            CategoryID = id.Value;

            var selectedCategory = Category
                .Where(c => c.ID == id.Value)
                .Single();

            Books = selectedCategory.BookCategories
                .Select(bc => bc.Book);
        }
    }
}