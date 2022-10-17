using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ToDoDbContext _dbContext;

        public ToDoController(ToDoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Index1", _dbContext.ToDoItems.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create1", new ToDoItem());
        }

        [HttpPost]
        public IActionResult Create(ToDoItem data)
        {
            if (ModelState.IsValid)
            {
                _dbContext.ToDoItems.Add(new ToDoItem { Id = new Random().Next(), Text = data.Text });
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Create1", data);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return View(_dbContext.ToDoItems.Find(id));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _dbContext.ToDoItems.Remove(_dbContext.ToDoItems.Find(id));
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = _dbContext.ToDoItems.Find(id);
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(ToDoItem model)
        { 
          if (ModelState.IsValid)
          {
            var item = _dbContext.ToDoItems.Find(model.Id);

          if (model.IsComplete && !item.IsComplete)
          {
             item.IsComplete = true;
             item.CompleteDate = DateTime.UtcNow;
          }
          item.Text = model.Text;
          _dbContext.SaveChanges();
          return RedirectToAction("Index");
          }
        return View(model);
       }

    [HttpGet]
        public IActionResult Init()
        {
            if (_dbContext.ToDoItems.Any())
                return Ok("Бд уже ініціалізоване!");

            _dbContext.ToDoItems.AddRange(new List<ToDoItem>
            {
                new ToDoItem { Id = 1, Text = "Пройти курс на DataCamp - Introduction to Git"},
                new ToDoItem { Id = 2, Text = "Пройти курс на DataCamp - Introduction to R"},
                new ToDoItem { Id = 3, Text = "Пройти курс на DataCamp - Introduction to Python"},
            });

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
