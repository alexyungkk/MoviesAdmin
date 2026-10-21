
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAdmin.Models;
using MoviesAdmin.Services;

public class MoviesController : Controller
{
    private readonly MoviesAdminContext _context;
    private readonly AzureStroageService _blobService;

    public MoviesController(MoviesAdminContext context, AzureStroageService blobService)
    {
        _context = context;
        _blobService = blobService;
    }

    // GET: MOVIES
    public async Task<IActionResult> Index()
    {
        var movies = await _context.Movie.OrderBy(m => m.RelaseDate).ToListAsync();
        // Retrieve all trails from the database
        //var movies = await _context.Movie.ToListAsync();
        return View(movies);
    }

    // GET: MOVIES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        // Check if the id parameter is null from URL
        if (id == null)
        {
            return NotFound();  // return a 404 Not Found response if the id is null
        }
        // Retrieve the movie with the specified id from the database
        var movie = await _context.Movie
            .FirstOrDefaultAsync(m => m.Id == id);
        // Check if the movie exists in the database
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // GET: MOVIES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: MOVIES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,Synopsis,Genre,Rating,Runtime,RelaseDate")] Movie movie, IFormFile? imageFile)
    {
        //?Validate if image file is uploaded
        if (imageFile == null)
        {
            // Manully add a validation failure
            ModelState.AddModelError("ImageFile", "The image file upload is required.");

        }
        // Check if the model state is valid before adding the trail to the database
        if (ModelState.IsValid)
        {   //TO-DO: we need unique file names so they don't get overwritten. Usa a GUID.jpg
            var extension = Path.GetExtension(imageFile!.FileName);
            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            
            string imageUrl = await _blobService.UploadFileAsync(imageFile, uniqueFileName); // use the original filename
            // Save image Url in Movie record
            movie.ImageUrl = imageUrl;

            //Save record in database
            _context.Add(movie);
            await _context.SaveChangesAsync();

            // Redirect to the Index action after successfully creating the trail
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }

    // GET: MOVIES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        // Retrieve the movie with the specified id from the database
        var movie = await _context.Movie.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }
        return View(movie);
    }

    // POST: MOVIES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,Synopsis,Genre,Rating,Runtime,RelaseDate,ImageUrl")] Movie movie)
    {
        if (id != movie.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.Id))
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
        return View(movie);
    }

    // GET: MOVIES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // POST: MOVIES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var movie = await _context.Movie.FindAsync(id);
        if (movie != null)
        {
            _context.Movie.Remove(movie);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MovieExists(int? id)
    {
        return _context.Movie.Any(e => e.Id == id);
    }
}
