using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using viajanet.Models;

namespace viajanet.Controllers
{
    public class QuartosController : Controller
    {
        private ViajanetContext db = new ViajanetContext();

        // GET: Quartos
        public async Task<ActionResult> Index()
        {
            var quarto = db.Quarto.Include(q => q.Hotel);
            return View(await quarto.ToListAsync());
        }
        
        // GET: Quartos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quarto quarto = await db.Quarto.FindAsync(id);
            if (quarto == null)
            {
                return HttpNotFound();
            }
            return View(quarto);
        }

        // GET: Quartos/Create
        public ActionResult Create()
        {
            ViewBag.Fk_Hotel = new SelectList(db.Hotel, "Id", "Nome");
            return View();
        }

        // POST: Quartos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Fk_Hotel,Descricao,Diaria,Capacidade,Suite")] Quarto quarto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Quarto.Add(quarto);
                    await db.SaveChangesAsync();
                    TempData["Mensagem"] = "Quarto adicionado com";
                    TempData["tipo"] = "success";
                    return RedirectToAction("Index");
                }

            }
            catch (Exception e)
            {
                TempData["Mensagem"] = "Ocorreu um erro ao editar este quarto , erro:";
                TempData["tipo"] = "error";
                TempData["Erro"] = e.GetType().Name;
                return RedirectToAction("Index");
            }
            

            ViewBag.Fk_Hotel = new SelectList(db.Hotel, "Id", "Nome", quarto.Fk_Hotel);
            return View(quarto);
        }

        // GET: Quartos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quarto quarto = await db.Quarto.FindAsync(id);
            if (quarto == null)
            {
                return HttpNotFound();
            }
            ViewBag.Fk_Hotel = new SelectList(db.Hotel, "Id", "Nome", quarto.Fk_Hotel);
            return View(quarto);
        }

        // POST: Quartos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Fk_Hotel,Descricao,Diaria,Capacidade,Suite")] Quarto quarto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(quarto).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    TempData["Mensagem"] = "Edições realizadas com sucesso!";
                    TempData["tipo"] = "success";
                    return RedirectToAction("viewQuartosHotel", "Quartos", new { id = quarto.Fk_Hotel });
                }
            }catch(Exception e)
            {
                TempData["Mensagem"] = "Ocorreu um erro ao editar este quarto , erro:";
                TempData["tipo"] = "error";
                TempData["Erro"] = e.GetType().Name;
                return RedirectToAction("viewQuartosHotel", "Quartos", new { id = quarto.Fk_Hotel });
            }
            
            ViewBag.Fk_Hotel = new SelectList(db.Hotel, "Id", "Nome", quarto.Fk_Hotel);
            return View(quarto);
        }

        // GET: Quartos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quarto quarto = await db.Quarto.FindAsync(id);
            if (quarto == null)
            {
                return HttpNotFound();
            }
            return View(quarto);
        }

        // POST: Quartos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Quarto quarto = await db.Quarto.FindAsync(id);
            try
            {
                db.Quarto.Remove(quarto);
                await db.SaveChangesAsync();
                TempData["Mensagem"] = "Quarto apagado com sucesso!";
                TempData["tipo"] = "success";
                return RedirectToAction("viewQuartosHotel", "Quartos", new { id = quarto.Fk_Hotel });

            }
            catch (Exception e)
            {
                TempData["Mensagem"] = "Ocorreu um erro ao deletar este quarto , erro:";
                TempData["tipo"] = "error";
                TempData["Erro"] = e.GetType().Name;
                return RedirectToAction("viewQuartosHotel", "Quartos", new { id = quarto.Fk_Hotel });
            }
           
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        /// métodos criados

        //GET
        public ActionResult viewQuartosHotel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int idHotel = Convert.ToInt32(id); //Convert.ToInt32(Request["id"]);
            var quartos = db.Quarto.Where(q => q.Fk_Hotel == idHotel);
            ViewBag.idHotel = idHotel; // passo a id do hotel pra view pra quando o usuário clicar em adicionar um novo quar
            return View(quartos.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateQuartoHotel([Bind(Include = "Id,Fk_Hotel,Descricao,Diaria,Capacidade,Suite")] Quarto quarto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Quarto.Add(quarto);
                    await db.SaveChangesAsync();
                    TempData["Mensagem"] = "Quarto adicionado com sucesso!";
                    TempData["tipo"] = "success";
                    return RedirectToAction("viewQuartosHotel", new { @id = quarto.Fk_Hotel });
                }
            }
            catch(Exception e)
            {
                TempData["Mensagem"] = "Ocorreu um erro ao adicionar este quarto , erro:";
                TempData["tipo"] = "error";
                TempData["Erro"] = e.GetType().Name;
                return RedirectToAction("viewQuartosHotel", new { @id = quarto.Fk_Hotel });
            }
           

            ViewBag.Fk_Hotel = new SelectList(db.Hotel, "Id", "Nome", quarto.Fk_Hotel);
            return View(quarto);
        }

        // GET: Quartos/Create
        public ActionResult CreateQuartoHotel(int? id) // recebe a id do hotel que serácriado
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TempData["Fk_Hotel"] = Convert.ToInt32(id);
            var nome = db.Hotel.Where(h => h.Id == id).Select(h => h.Nome).FirstOrDefault();
            TempData["Nome_Hotel"] = nome;

            return View();
        }


    }
}
