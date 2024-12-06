using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using MVC.Data;
using MVC.Models;
using MVC.Repository;
using MVC.Repository.IRepository;
using MVC.ViewModels;

namespace MVC.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        // GET: CategoryController
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public ActionResult Index()
        {
            List<Product> objProductList = _unitOfWork
                .Product.GetAll(includeProperties: "Category")
                .ToList();
            return View(objProductList);
        }

        public ActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork
                    .Category.GetAll()
                    .Select(u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() }),
                Product = new Product(),
            };
            //Select Feature
            // IEnumerable<SelectListItem> CategoryList = _unitOfWork
            //     .Category.GetAll()
            //     .Select(u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() });

            // ViewBag
            // ViewBag.CategoryList = CategoryList;

            // ViewData
            // ViewData["CategoryList"] = CategoryList;

            if (id == null || id == 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }
        }

        [HttpPost]
        public ActionResult Upsert(ProductVM productVM, IFormFile file)
        {
            ModelState.Remove("File");
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    //give random name
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images/product");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        //delete the old image
                        var oldImagePath = Path.Combine(
                            wwwRootPath,
                            productVM.Product.ImageUrl.TrimStart('/')
                        );

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (
                        var fileStream = new FileStream(
                            Path.Combine(productPath, fileName),
                            FileMode.Create
                        )
                    )
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageUrl = @"/images/product/" + fileName;
                }

                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }
                // _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";

                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork
                    .Category.GetAll()
                    .Select(u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() });

                return View(productVM);
            }
        }

        //combine with Create as Upsert

        // public ActionResult Edit(int? id)
        // {
        //     if (id == null || id == 0)
        //     {
        //         return NotFound();
        //     }
        //     Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);

        //     if (productFromDb == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(productFromDb);
        // }

        // [HttpPost]
        // public ActionResult Edit(Product obj)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         _unitOfWork.Product.Update(obj);
        //         _unitOfWork.Save();
        //         TempData["success"] = "Product updated successfully";

        //         return RedirectToAction("Index");
        //     }
        //     return View();
        // }

        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeletePost(int? id)
        {
            Product? obj = _unitOfWork.Product.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
