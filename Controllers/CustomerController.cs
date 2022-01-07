using AutoMapper;
using ECommerce.BLL.Interfaces;
using ECommerce.BLL.Models;
using ECommerce.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce.PL.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IcustomerRep repository;
        private readonly IMapper mapper;

        public CustomerController(IcustomerRep repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            var data = repository.GetAll();
            return View(mapper.Map<IEnumerable<CustomerVModel>>(data));
        }
        public IActionResult Details(int id)
        {
            var data = repository.GetById(id);
            return View(mapper.Map<CustomerVModel>(data));
        }
        public IActionResult Edit(int id)
        {
            var data = repository.GetById(id);
            return View(mapper.Map<CustomerVModel>(data));
        }
        [HttpPost]
        public IActionResult Edit(CustomerVModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    repository.Update(mapper.Map<customer>(model));
            //    return RedirectToAction("Index");
            //}
            //return View(model);

            repository.Update(mapper.Map<customer>(model));
            return RedirectToAction("Index");

        }
        public IActionResult Create(CustomerVModel model)
        {
            repository.Create(mapper.Map<customer>(model));
            return RedirectToAction("Index");
        }
        public IActionResult CheckProducts()
        {
            var Customers = repository.GetAll();
            ViewBag.Cst=new SelectList(Customers, "id", "name");
            return View();
        }
        [HttpPost]
        public JsonResult CheckProducts(int id)
        {
            var data = repository.ProductByCstId(id);
            return Json(data);
        }
    }
}
