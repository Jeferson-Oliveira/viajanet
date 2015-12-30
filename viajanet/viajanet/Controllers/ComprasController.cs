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
    public class ComprasController : Controller
    {
        private ViajanetContext db = new ViajanetContext();

        // get  método que receberá a id da viagem que o usuário selecionou e pegará os hoteis da cidade da viagem pra exibir
       
        public ActionResult hospedagemHoteis()
        {
            int idCidadeViagem = Convert.ToInt32(Request["cidade"]);
            int id = Convert.ToInt32(Request["viagem"]);

            TempData["idViagem"] = id;

            var hoteis = db.Hotel.Include(h => h.Cidade).Include(h => h.Estado).Where(h => h.FK_Cidade == idCidadeViagem);
            
            return View(hoteis.ToList());
        }

        // método que irá conferir se o usuário deseja a hospedagem 

        public ActionResult confirmHospedagem()
        {
            if (!string.IsNullOrEmpty(Request["hotel"]) || !string.IsNullOrEmpty(Request["viagem"])) // isso me fará saber se o usuário reservou hotel ou não
            {
                var idHotel = Convert.ToInt32(Request["hotel"]);
                var idViajem = Convert.ToInt32(Request["viagem"]);

                
                ViewBag.Viajem = db.Viajem.Find(idViajem);
                var cidade = ViewBag.Viajem.Cidade;
                ViewBag.Hotel = db.Hotel.Find(idHotel);

            }
            else
            {
                if (!string.IsNullOrEmpty(Request["viagem"]))
                {
                    var idViagem = Convert.ToInt32(Request["viagem"]);
                    ViewBag.Viagem = db.Viajem.Find(idViagem);
                }
            }
            return View();
        }



        // GET: Compras
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            var compra = db.Compra.Include(c => c.Hotel).Include(c => c.Viajem);
            return View(await compra.ToListAsync());
        }

        // GET: Compras/Details/5
        
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = await db.Compra.FindAsync(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // GET: Compras/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.FK_Hotel = new SelectList(db.Hotel, "Id", "Nome");
            ViewBag.FK_Viajem = new SelectList(db.Viajem, "Id", "Id");
            return View();
        }

        // POST: Compras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Fk_Usuario,FK_Viajem,FK_Hotel,Qtd_Adultos,Qtd_Criancas,Valor_Total")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                db.Compra.Add(compra);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FK_Hotel = new SelectList(db.Hotel, "Id", "Nome", compra.FK_Hotel);
            ViewBag.FK_Viajem = new SelectList(db.Viajem, "Id", "Id", compra.FK_Viajem);
            return View(compra);
        }
        [Authorize(Roles = "Admin")]
        // GET: Compras/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = await db.Compra.FindAsync(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_Hotel = new SelectList(db.Hotel, "Id", "Nome", compra.FK_Hotel);
            ViewBag.FK_Viajem = new SelectList(db.Viajem, "Id", "Id", compra.FK_Viajem);
            return View(compra);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Fk_Usuario,FK_Viajem,FK_Hotel,Qtd_Adultos,Qtd_Criancas,Valor_Total")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compra).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FK_Hotel = new SelectList(db.Hotel, "Id", "Nome", compra.FK_Hotel);
            ViewBag.FK_Viajem = new SelectList(db.Viajem, "Id", "Id", compra.FK_Viajem);
            return View(compra);
        }
        [Authorize(Roles = "Admin")]
        // GET: Compras/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = await db.Compra.FindAsync(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Compra compra = await db.Compra.FindAsync(id);
            db.Compra.Remove(compra);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: /Create
        [HttpPost]
        public ActionResult confirmCompra([Bind(Include = "Id,Fk_Usuario,FK_Viajem,FK_Hotel,Qtd_Adultos,Qtd_Criancas,Valor_Total,NomeTitular,CVC,Expiracao,NumeroCartao")] FormCollection collection)
        {
            try
            {
                Compra compra = new Compra();  
                compra.Fk_Usuario = collection["Fk_Usuario"];
                compra.FK_Viajem = Convert.ToInt32(collection["FK_Viajem"]);
                if (!string.IsNullOrEmpty(collection["FK_Hotel"])) { // aqui eu vejo se está nulo pois eu não sei se o usuário quiz algum hotel
                    compra.FK_Hotel = Convert.ToInt32(collection["FK_Hotel"]);
                }
                compra.Qtd_Adultos = Convert.ToInt32(collection["Qtd_Adultos"]);
                compra.Qtd_Criancas = Convert.ToInt32(collection["Qtd_Criancas"]);
                compra.Valor_Total = Convert.ToDouble(collection["Valor_Total"]);
                compra.NomeTitular = collection["name"];
                compra.NumeroCartao = collection["number"];
                compra.Expiracao = collection["expiry"];
                compra.CVC = collection["cvc"];
                db.Compra.Add(compra);
                db.SaveChanges();
                TempData["Compra"] = "Compra realizada com sucesso!";
                return RedirectToAction("procurarViajens","Viajens");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult viewCompras()
        {
            try
            {
                if (!string.IsNullOrEmpty(Request["user"]))
                {
                    string id = Request["user"];
                    var compras = db.Compra.Include(c => c.Hotel).Include(c => c.Viajem).Where(c => c.Fk_Usuario == id);
                    return View(compras.ToList());
                }
            }catch(Exception e)
            {
                TempData["Mensagem"] = "Ocorreu um erro ao tentar listar suas compras , erro:";
                TempData["tipo"] = "error";
                TempData["Erro"] = e.GetType().Name;
                RedirectToAction("procurarViajens", "Viajens");
            }
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
