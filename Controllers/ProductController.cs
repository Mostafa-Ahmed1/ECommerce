using AutoMapper;
using ECommerce.BLL.Interfaces;
using ECommerce.BLL.Models;
using ECommerce.BLL.Repository;
using ECommerce.DAL.Entities;
using Microsoft.AspNetCore.Mvc;


namespace ECommerce.PL.Controllers
{
    public class ProductController : Controller
    {
        private readonly IproductRep _repository;
        private readonly IMapper mapper;

        //ProductRep _repository = new ProductRep();
        public ProductController(IproductRep repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var data = _repository.GetAll();
            return View(mapper.Map<IEnumerable<productVModel>>(data));
        }
        public IActionResult Details(int id)
        {
            var data = _repository.GetProductById(id);
            return View(mapper.Map<productVModel>(data));
        }
        public IActionResult Edit(int id)
        {
            var data = _repository.GetProductById(id);
            return View(mapper.Map<productVModel>(data));
        }
        [HttpPost]
        public IActionResult Edit(productVModel model)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(mapper.Map<product>(model));
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Delete(productVModel model)
        {
            //var data = _repository.GetProductById(id);
            _repository.Delete(mapper.Map<product>(model));
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(productVModel model)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(mapper.Map<product>(model));
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
