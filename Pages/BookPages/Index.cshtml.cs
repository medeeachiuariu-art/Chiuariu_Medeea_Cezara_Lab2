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
    public BookData BookD { get; set; } = default!;

    public int BookID { get; set; }
    public int CategoryID { get; set; }

    public string TitleSort { get; set; } = "";
    public string AuthorSort { get; set; } = "";
    public string CurrentFilter { get; set; } = "";

    public SelectList AuthorsList { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public int? AuthorID { get; set; }

    public async Task OnGetAsync(
          int? id, int? categoryID, string? sortOrder, string? searchString)
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

        BookD = new BookData();

        TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
        AuthorSort = sortOrder == "author" ? "author_desc" : "author";
        CurrentFilter = searchString ?? "";

        BookD.Books = await _context.Book
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .Include(b => b.BookCategories)
                .ThenInclude(b => b.Category)
            .AsNoTracking()
            .OrderBy(b => b.Title)
            .ToListAsync();

        if (AuthorID.HasValue)
        {
            BookD.Books = BookD.Books
                .Where(b => b.AuthorID == AuthorID.Value);
        }
if (!String.IsNullOrEmpty(searchString))
{
    BookD.Books = BookD.Books.Where(s =>
        s.Author.FirstName.Contains(searchString)
        || s.Author.LastName.Contains(searchString)
        || s.Title.Contains(searchString));
}
        if (id != null)
        {
            BookID = id.Value;

            var book = BookD.Books
                .SingleOrDefault(b => b.ID == id.Value);

            if (book != null)
            {
                BookD.Categories = book.BookCategories
                    .Select(s => s.Category);
            }
        }

        switch (sortOrder)
        {
            case "title_desc":
                BookD.Books = BookD.Books
                    .OrderByDescending(s => s.Title);
                break;

            case "author_desc":
                BookD.Books = BookD.Books
                    .OrderByDescending(s => s.Author?.FullName);
                break;

            case "author":
                BookD.Books = BookD.Books
                    .OrderBy(s => s.Author?.FullName);
                break;

            default:
                BookD.Books = BookD.Books
                    .OrderBy(s => s.Title);
                break;
        }

        Book = BookD.Books.ToList();
    }
}