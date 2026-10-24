using Chiuariu_Medeea_Cezara_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Chiuariu_Medeea_Cezara_Lab2.Pages.BookPages;

public class CreateModel : BookCategoriesPageModel
{
    private readonly Chiuariu_Medeea_Cezara_Lab2Context _context;

    public CreateModel(Chiuariu_Medeea_Cezara_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public IActionResult OnGet()
    {
        LoadLists();

        var book = new Book();
        book.BookCategories = new List<BookCategory>();

        PopulateAssignedCategoryData(_context, book);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(
        string[] selectedCategories)
    {
        Book.BookCategories = new List<BookCategory>();

        if (selectedCategories != null)
        {
            foreach (var cat in selectedCategories)
            {
                var catToAdd = new BookCategory
                {
                    CategoryID = int.Parse(cat)
                };

                Book.BookCategories.Add(catToAdd);
            }
        }

        if (!ModelState.IsValid)
        {
            LoadLists();
            PopulateAssignedCategoryData(_context, Book);

            return Page();
        }

        _context.Book.Add(Book);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

    private void LoadLists()
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
    }
}