
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAdmin.Models;

public class GenresController : Controller
{
    private readonly MoviesAdminContext _context;

    public GenresController(MoviesAdminContext context)
    {
        _context = context;
    }

    // GET: GENRES
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Genre.ToListAsync());
    }

    // GET: GENRES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var genre = await _context.Genre
            .FirstOrDefaultAsync(m => m.Id == id);
        if (genre == null)
        {
            return NotFound();
        }

        return View(genre);
    }

    // GET: GENRES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: GENRES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Movies,Title")] Genre genre)
    {
        if (ModelState.IsValid)
        {
            _context.Add(genre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(genre);
    }

    // GET: GENRES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var genre = await _context.Genre.FindAsync(id);
        if (genre == null)
        {
            return NotFound();
        }
        return View(genre);
    }

    // POST: GENRES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Movies,Title")] Genre genre)
    {
        if (id != genre.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(genre);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(genre.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(genre);
    }

    // GET: GENRES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var genre = await _context.Genre
            .FirstOrDefaultAsync(m => m.Id == id);
        if (genre == null)
        {
            return NotFound();
        }

        return View(genre);
    }

    // POST: GENRES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var genre = await _context.Genre.FindAsync(id);
        if (genre != null)
        {
            _context.Genre.Remove(genre);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool GenreExists(int? id)
    {
        return _context.Genre.Any(e => e.Id == id);
    }
}
