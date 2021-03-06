using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Controllers
{
    public class TransactionController : Controller
    {

        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Transaction
        public ActionResult Index()
        {
            List<Transaction> transactionList = _db.Transactions.ToList();
            List<Transaction> orderedList = transactionList.OrderByDescending(t => t.DateOfTransaction).ToList();
            return View(orderedList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create(Transaction transaction)
        {
            Product product = _db.Products.Find(transaction.ProductId);
            if (product.InventoryCount < transaction.Quantity)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            

            if (ModelState.IsValid)
            {
                _db.Transactions.Add(transaction);
                
            }
            _db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}