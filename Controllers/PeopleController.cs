using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Contacts.Models;
using ContactsBook.Data;


namespace ContactsBook.Controllers
{
    public class PeopleController : Controller
    {
        private readonly ContactsBookContext _context; 
        public PeopleController(ContactsBookContext context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Person == null)
            {
                return Problem("Entity set 'ContactsBookContext.Person'  is null.");
            }
            var filter = from m in _context.Person select m;
            if (!String.IsNullOrEmpty(searchString)) {
                filter = filter.Where(s => s.Name!.Contains(searchString)
                || s.Surname!.Contains(searchString)
                || s.Email!.Contains(searchString)
                || s.PhoneNumber.ToString().Contains(searchString)
                || s.Date.ToString().Contains(searchString)
                );
            }
              return View(await filter.ToListAsync());
                          
        }
        //GET: Peoople with birthdays in the current week

        public async Task<IActionResult> Home()
        {
            if (_context.Person == null)
            {
                return Problem("Entity set 'ContactsBookContext.Person'  is null.");
            }
            var Birthday = from B in _context.Person select B;
            DateTime now = DateTime.Now;
            DateTime today = now.Date;
            DayOfWeek day = DateTime.Now.DayOfWeek;
            int Days = day - DayOfWeek.Monday;
            Birthday = Birthday.Where(j => (j.Date.Month.Equals(now.Date.Month) &&  j.Date.Day.Equals(today.AddDays(-Days).Date.Day)) || (j.Date.Month.Equals(now.Date.Month) && j.Date.Day.Equals(today.AddDays(-Days + 1).Date.Day)) || (j.Date.Month.Equals(now.Date.Month) && j.Date.Day.Equals(today.AddDays(-Days + 2).Date.Day)) || (j.Date.Month.Equals(now.Date.Month) && j.Date.Day.Equals(today.AddDays(-Days + 3).Date.Day)) || (j.Date.Month.Equals(now.Date.Month) && j.Date.Day.Equals(today.AddDays(-Days + 4).Date.Day)) || (j.Date.Month.Equals(now.Date.Month) && j.Date.Day.Equals(today.AddDays(-Days + 5).Date.Day)) || (j.Date.Month.Equals(now.Date.Month) && j.Date.Day.Equals(today.AddDays(-Days + 6).Date.Day)));
            return View(await Birthday.ToListAsync());
        }
        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,PhoneNumber,Email,Date")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,PhoneNumber,Email,Date")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
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
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Person == null)
            {
                return Problem("Entity set 'ContactsBookContext.Person'  is null.");
            }
            var person = await _context.Person.FindAsync(id);
            if (person != null)
            {
                _context.Person.Remove(person);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
          return (_context.Person?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
