using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_App.Data;
using MVC_App.Models;
using System.Threading.Tasks;

namespace MVC_App.Controllers
{
    public class ContactsController : Controller
    {
       private readonly IContactsRepository _contactsRepository;


        public ContactsController(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
        }


        // GET: ContactController
        public async Task<ActionResult> Index()
        {
            var contacts = await _contactsRepository.GetAll();
            return View(contacts);
        }

        // GET: ContactController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var contacts = await _contactsRepository.GetDetailsById(id);
        
            return View(contacts);
        
        }

        // GET: ContactController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                var contact = new Contact()
                {
                    FirstName = collection["FirstName"],
                    LastName = collection["LastName"],
                    Phone = collection["Phone"],
                    Address = collection["Address"]
                };
                
                await _contactsRepository.Insert(contact);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ContactController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            //hago una consulta a la base de datos para traer el contacto que se quiere editar
            var contact = await _contactsRepository.GetDetailsById(id);
            return View(contact);
        }

        // POST: ContactController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                var contact = new Contact()
                {
                    Id = int.Parse(collection["id"]),
                    FirstName = collection["FirstName"],
                    LastName = collection["LastName"],
                    Phone = collection["Phone"],
                    Address = collection["Address"]
                };

                await _contactsRepository.Update(contact);
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ContactController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var contact = await _contactsRepository.GetDetailsById(id);
            return View(contact);
        }

        // POST: ContactController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _contactsRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
