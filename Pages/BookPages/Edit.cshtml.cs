using Chiuariu_Medeea_Cezara_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Chiuariu_Medeea_Cezara_Lab2.Pages.BookPages;

public class EditModel : PageModel
{
    private readonly Chiuariu_Medeea_Cezara_Lab2Context _context;

    public EditModel(Chiuariu_Medeea_Cezara_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var book = await _context.Book.FirstOrDefaultAsync(m => m.ID == id);
        if (book is null)
        {
            return NotFound();
        }
        Book = book;
        LoadLists();
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            LoadLists();
            return Page();
        }

        _context.Attach(Book).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookExists(Book.ID))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }
    private void LoadLists()
    {
        ViewData["PublisherID"] = new SelectList(
            _context.Set<Publisher>(), "ID", "PublisherName",
            Book.PublisherID);

        ViewData["AuthorID"] = new SelectList(
            _context.Authors.Select(a => new
            {
                a.ID,
                FullName = a.FirstName + " " + a.LastName
            }),
            "ID", "FullName", Book.AuthorID);
    }
    private bool BookExists(int id)
    {
        return _context.Book.Any(e => e.ID == id);
    }
}
