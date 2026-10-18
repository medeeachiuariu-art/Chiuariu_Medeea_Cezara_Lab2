using Chiuariu_Medeea_Cezara_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Chiuariu_Medeea_Cezara_Lab2.Pages.BookPages;

public class CreateModel : PageModel
{
    private readonly Chiuariu_Medeea_Cezara_Lab2Context _context;

    public CreateModel(Chiuariu_Medeea_Cezara_Lab2Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        ViewData["PublisherID"] = new SelectList(
        _context.Set<Publisher>(), "ID", "PublisherName");

        ViewData["AuthorID"] = new SelectList(
            _context.Authors.Select(a => new
            {
                a.ID,
                FullName = a.FirstName + " " + a.LastName
            }),
            "ID", "FullName");

        return Page();
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            OnGet();
            return Page();
        }

        _context.Book.Add(Book);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
