using Microsoft.AspNetCore.Mvc;
using MVC.Data;
using MVC.Models;

namespace MVC.Controllers
{
    public class CategoryController : Controller
    {
        // GET: CategoryController
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db){
            _db = db;
        }
        public ActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public ActionResult Create(){
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category obj){

            if(obj.Name == obj.DisplayOrder.ToString()){
                ModelState.AddModelError("name","The Display Order cannot exactly match the name.");
            }

            if(ModelState.IsValid){
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public ActionResult Edit(int? id){

            if(id == null || id == 0){
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);

            if(categoryFromDb == null){
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public ActionResult Edit(Category obj){

            if(ModelState.IsValid){
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int? id){

            if(id == null || id == 0){
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);

            if(categoryFromDb == null){
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeletePost(int? id){

            Category? obj = _db.Categories.Find(id);
            
            if(obj == null){
                return NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
