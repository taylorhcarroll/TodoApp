using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;
using Todo.Models.ViewModels;

namespace Todo.Controllers
{
    [Authorize]
    public class TodoItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TodoItemsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: TodoItems
        public async Task<IActionResult> Index(string filterButton)
        {
            var user = await GetCurrentUserAsync();

            if (filterButton == "To Do")
            {
                var items = await _context.TodoItem
                    .Include(t => t.TodoStatus)
                    .Where(tdi => tdi.ApplicationUserId == user.Id)
                    .Where(ti => ti.TodoStatusId == 1)
                    .ToListAsync();

                return View(items);
            }
            if (filterButton == "In Progress")
            {
                var items = await _context.TodoItem
                    .Include(t => t.TodoStatus)
                    .Where(tdi => tdi.ApplicationUserId == user.Id)
                    .Where(ti => ti.TodoStatusId == 2)
                    .ToListAsync();

                return View(items);
            }
            if (filterButton == "Done")
            {
                var items = await _context.TodoItem
                    .Include(t => t.TodoStatus)
                    .Where(tdi => tdi.ApplicationUserId == user.Id)
                    .Where(ti => ti.TodoStatusId == 3)
                    .ToListAsync();

                return View(items);
            }
            if (filterButton == "All")
            {
                var items = await _context.TodoItem
                    .Include(t => t.TodoStatus)
                    .Where(tdi => tdi.ApplicationUserId == user.Id)
                    .ToListAsync();

                return View(items);
            }
            else
            {
                var items = await _context.TodoItem
                    .Include(t => t.TodoStatus)
                    .Where(tdi => tdi.ApplicationUserId == user.Id)
                    .ToListAsync();

                return View(items);
            }


        }

        // GET: TodoItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItem
                .Include(t => t.ApplicationUser)
                .Include(t => t.TodoStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // GET: TodoItems/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            ViewData["TodoStatusId"] = new SelectList(_context.TodoStatus, "Id", "Title");
            return View();
        }

        // POST: TodoItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TodoItem todoItem)
        {
            try
            {
                var user = await GetCurrentUserAsync();
                todoItem.ApplicationUserId = user.Id;

                _context.TodoItem.Add(todoItem);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TodoItems/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.TodoItem.FirstOrDefaultAsync(ti => ti.Id == id);
            var loggedInUser = await GetCurrentUserAsync();
            var TodoStatuses = await _context.TodoStatus
               .Select(td => new SelectListItem() { Text = td.Title, Value = td.Id.ToString() })
               .ToListAsync();

            if (item == null)
            {
                return NotFound();
            }


            var viewModel = new TodoItemFormViewModel()
            {
                Id = id,
                Title = item.Title,
                TodoStatusId = item.TodoStatusId,
                TodoStatusOptions = TodoStatuses,

            };

            if (item.ApplicationUserId != loggedInUser.Id)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // POST: TodoItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TodoItemFormViewModel toDoViewItem)
        {
            try
            {

                var toDoItem = new TodoItem()
                {
                    Id = toDoViewItem.Id,
                    Title = toDoViewItem.Title,
                    TodoStatusId = toDoViewItem.TodoStatusId
                };

                var user = await GetCurrentUserAsync();
                toDoItem.ApplicationUserId = user.Id;

                _context.TodoItem.Update(toDoItem);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TodoItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItem
                .Include(t => t.ApplicationUser)
                .Include(t => t.TodoStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // POST: TodoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todoItem = await _context.TodoItem.FindAsync(id);
            _context.TodoItem.Remove(todoItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoItemExists(int id)
        {
            return _context.TodoItem.Any(e => e.Id == id);
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
