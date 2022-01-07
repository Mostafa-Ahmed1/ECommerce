using AutoMapper;
using ECommerce.BLL.Interfaces;
using ECommerce.BLL.Models;
using ECommerce.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce.PL.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMapper mapper;
        private readonly IorderRep repository;
        private readonly IproductRep p;
        private readonly IcustomerRep c;
        private readonly IorderProductRep op;

        public OrderController(IMapper mapper, IorderRep repository,
            IproductRep p, IcustomerRep c, IorderProductRep op)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.p = p;
            this.c = c;
            this.op = op;
        }
        public IActionResult Index()
        {
            var data = repository.GetAll();                           //entity>Ienumrable
            return View(mapper.Map<IEnumerable<OrderVModel>>(data));
        }
        public IActionResult Create()
        {
            var customers = c.GetAll();
            var products = p.GetAll();
            ViewBag.cst = new SelectList(customers, "id", "name");
            ViewBag.prod = new SelectList(products, "id", "name");

            return View();
        }
        [HttpPost]
        public IActionResult Create(OrderVModel model)
        {
            var data = repository.Create(mapper.Map<order>(model));
            foreach (var item in model.productsId)
            {
                op.Create(data, item);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var data = repository.GetById(id);
            repository.Delete(data);
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var data = repository.GetById(id);
            return View(mapper.Map<OrderVModel>(data));
        }

        public JsonResult Count()
        {
           return Json(repository.Count());
        }
    }
}
