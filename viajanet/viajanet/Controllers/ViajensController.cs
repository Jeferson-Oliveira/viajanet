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
using System.Text;
namespace viajanet.Controllers
{
    public class ViajensController : Controller
    {
        private ViajanetContext db = new ViajanetContext();

        // GET: Viajens
        public async Task<ActionResult> Index()
        {
            var viajem = db.Viajem.Include(v => v.Cidade).Include(v => v.Cidade1).Include(v => v.Companhia).Include(v => v.Estado).Include(v => v.Estado1);
            return View(await viajem.ToListAsync());
        }

        // GET: Viajens/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Viajem viajem = await db.Viajem.FindAsync(id);
            if (viajem == null)
            {
                return HttpNotFound();
            }
            return View(viajem);
        }

        // GET: Viajens/Create
        public ActionResult Create()
        {
            ViewBag.FK_Cidade_Destino = new SelectList(db.Cidade, "Id", "Nome");
            ViewBag.FK_Cidade_Saida = new SelectList(db.Cidade, "Id", "Nome");
            ViewBag.FK_Companhia = new SelectList(db.Companhia, "Id", "Nome");
            ViewBag.FK_Estado_Saida = new SelectList(db.Estado, "Id", "Nome");
            ViewBag.FK_Estado_Destino = new SelectList(db.Estado, "Id", "Nome");
            return View();
        }

        // POST: Viajens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FK_Estado_Saida,FK_Cidade_Saida,FK_Estado_Destino,FK_Cidade_Destino,Valor,Data_Ida,Data_Volta,FK_Companhia")] Viajem viajem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Viajem.Add(viajem);
                    await db.SaveChangesAsync();
                    TempData["Mensagem"] = "Viajem cadastrada com sucesso!";
                    TempData["tipo"] = "success";
                    return RedirectToAction("Index");
                }


            }
            catch (Exception e)
            {
                TempData["Mensagem"] = "Ocorreu um erro ao cadastrar esta viajem erro:";
                TempData["tipo"] = "error";
                TempData["Erro"] = e.GetType().Name;
                return RedirectToAction("Index");
            }
           
            ViewBag.FK_Cidade_Destino = new SelectList(db.Cidade, "Id", "Nome", viajem.FK_Cidade_Destino);
            ViewBag.FK_Cidade_Saida = new SelectList(db.Cidade, "Id", "Nome", viajem.FK_Cidade_Saida);
            ViewBag.FK_Companhia = new SelectList(db.Companhia, "Id", "Nome", viajem.FK_Companhia);
            ViewBag.FK_Estado_Saida = new SelectList(db.Estado, "Id", "Nome", viajem.FK_Estado_Saida);
            ViewBag.FK_Estado_Destino = new SelectList(db.Estado, "Id", "Nome", viajem.FK_Estado_Destino);
            return View(viajem);
        }
       
        public ActionResult obterEstados()
        {
            var estados = db.Estado.ToList();
            StringBuilder options = new StringBuilder();
            options.Append("<option value=''> Selecione um estado </option>");
            foreach (var estado in estados)
            {
                options.Append("<option value='" + estado.Id + "'>" + estado.Nome + "</option>");
            }
            return Content(options.ToString());
        }

        [HttpPost]
        public ActionResult obterCidades()
        {
            int idEstado = Convert.ToInt32(Request["id"]);
            var cidades = db.Cidade.Where(c => c.FK_Estado == idEstado).ToList();
            StringBuilder options = new StringBuilder();
            options.Append("<option value='' selected> Selecione a cidade </option>");
            foreach(var cidade in cidades)
            {
                options.Append("<option value='" + cidade.Id + "'>" + cidade.Nome + "</option>");
            }
            return Content(options.ToString());
        }

        // GET: Viajens/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Viajem viajem = await db.Viajem.FindAsync(id);
            if (viajem == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_Cidade_Destino = new SelectList(db.Cidade, "Id", "Nome", viajem.FK_Cidade_Destino);
            ViewBag.FK_Cidade_Saida = new SelectList(db.Cidade, "Id", "Nome", viajem.FK_Cidade_Saida);
            ViewBag.FK_Companhia = new SelectList(db.Companhia, "Id", "Nome", viajem.FK_Companhia);
            ViewBag.FK_Estado_Saida = new SelectList(db.Estado, "Id", "Nome", viajem.FK_Estado_Saida);
            ViewBag.FK_Estado_Destino = new SelectList(db.Estado, "Id", "Nome", viajem.FK_Estado_Destino);
            return View(viajem);
        }

        // POST: Viajens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FK_Estado_Saida,FK_Cidade_Saida,FK_Estado_Destino,FK_Cidade_Destino,Valor,Data_Ida,Data_Volta,FK_Companhia")] Viajem viajem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(viajem).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    TempData["Mensagem"] = "Edições realizadas comm sucesso!";
                    TempData["tipo"] = "success";
                    return RedirectToAction("Index");
                }

            }
            catch (Exception e)
            {
                TempData["Mensagem"] = "Ocorreu um erro ao editar esta viajem , erro:";
                TempData["tipo"] = "error";
                TempData["Erro"] = e.GetType().Name;
                return RedirectToAction("Index");
            }
            
            ViewBag.FK_Cidade_Destino = new SelectList(db.Cidade, "Id", "Nome", viajem.FK_Cidade_Destino);
            ViewBag.FK_Cidade_Saida = new SelectList(db.Cidade, "Id", "Nome", viajem.FK_Cidade_Saida);
            ViewBag.FK_Companhia = new SelectList(db.Companhia, "Id", "Nome", viajem.FK_Companhia);
            ViewBag.FK_Estado_Saida = new SelectList(db.Estado, "Id", "Nome", viajem.FK_Estado_Saida);
            ViewBag.FK_Estado_Destino = new SelectList(db.Estado, "Id", "Nome", viajem.FK_Estado_Destino);
            return View(viajem);
        }

        // GET: Viajens/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Viajem viajem = await db.Viajem.FindAsync(id);
            if (viajem == null)
            {
                return HttpNotFound();
            }
            return View(viajem);
        }

        // POST: Viajens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try {
                Viajem viajem = await db.Viajem.FindAsync(id);
                db.Viajem.Remove(viajem);
                await db.SaveChangesAsync();

                TempData["Mensagem"] = "Viajem deletada";
                TempData["tipo"] = "success";
               
                return RedirectToAction("Index");
            }catch(Exception e)
            {
                TempData["Mensagem"] = "Ocorreu um erro ao deletar esta viajem , provavelmente ele está associado a alguma compra, caso não seja, contate o administrador e informe o seguinte erro:";
                TempData["tipo"] = "error";
                TempData["Erro"] = e.GetType().Name;
                return RedirectToAction("Index");
            }
        }

        //Get
        public ActionResult procurarViajens() {
            return View();
        }
        [HttpPost]
        public ActionResult resultViajens(FormCollection result) {

            var Csaida = Convert.ToInt32(result["CidadeSaida"]); // eu só preciso das Id's da cidade pra fazer a pesquisa
            var Cdestino = Convert.ToInt32(result["CidadeDestino"]);
            try
            {
                var viajens = db.Viajem.Include(v => v.Cidade).Include(v => v.Cidade1).Include(v => v.Companhia).Include(v => v.Estado).Include(v => v.Estado1).Where(v => v.FK_Cidade_Saida == Csaida).Where(v => v.FK_Cidade_Destino == Cdestino);
                return View(viajens.ToList());

            }
            catch (Exception e)
            {
                TempData["Mensagem"] = "Ocorreu um erro ao caregar as viajens , erro:";
                TempData["tipo"] = "error";
                TempData["Erro"] = e.GetType().Name;
                return RedirectToAction("procurarViajens");
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
    }
}
