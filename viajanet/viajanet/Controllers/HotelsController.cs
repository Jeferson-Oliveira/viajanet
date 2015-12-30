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
using System.IO;

namespace viajanet.Controllers
{
    public class HotelsController : Controller
    {
        private ViajanetContext db = new ViajanetContext();

        // GET: Hotels
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            var hotel = db.Hotel.Include(h => h.Cidade).Include(h => h.Estado);
            return View(await hotel.ToListAsync());
        }

        // GET: Hotels/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = await db.Hotel.FindAsync(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // GET: Hotels/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.FK_Cidade = new SelectList(db.Cidade, "Id", "Nome");
            ViewBag.FK_Estado = new SelectList(db.Estado, "Id", "Nome");
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nome,Telefone,Longradouro,FK_Estado,FK_Cidade,Bairro,Cep,Img")] Hotel hotel,HttpPostedFileBase img)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        FileInfo imgInfo = new FileInfo(img.FileName);

                        if (imgInfo.Extension.Equals(".jpg", StringComparison.InvariantCultureIgnoreCase) || imgInfo.Extension.Equals(".png", StringComparison.InvariantCultureIgnoreCase) || imgInfo.Extension.Equals(".gif", StringComparison.InvariantCultureIgnoreCase))
                        {
                            string imgNome = Guid.NewGuid().ToString("N") + imgInfo.Extension.ToLower();
                            img.SaveAs(Server.MapPath("~/Imagens/Hoteis/" + imgNome));
                            hotel.Img = imgNome;
                        }

                        //System.IO.Directory.CreateDirectory(HttpContext.Server.MapPath("~/Imagens/Hoteis/") + hotel.Nome + "/" + hotel.Cep);
                        //img.SaveAs(HttpContext.Server.MapPath("~/Imagens/Hoteis/" + hotel.Nome + "/" + hotel.Cep + "/") + img.FileName);
                        //hotel.Img = "/Imagens/Hoteis/" + hotel.Nome + "/" + hotel.Cep + "/" + img.FileName;
                        //hotel.Img = hotel.Nome + "/" + hotel.Cep + "/" +  img.FileName;
                    }

                    db.Hotel.Add(hotel);
                    await db.SaveChangesAsync();
                    TempData["Mensagem"] = "Hotel Cadastrado com sucesso";
                    TempData["tipo"] = "success";
                    return RedirectToAction("Index");
                }
            }
            catch(Exception e)
            {
                TempData["Mensagem"] = "Ocorreu um erro ao cadastrar este hotel , erro:";
                TempData["tipo"] = "error";
                TempData["Erro"] = e.GetType().Name;
                return RedirectToAction("Index");
            }
            

            ViewBag.FK_Cidade = new SelectList(db.Cidade, "Id", "Nome", hotel.FK_Cidade);
            ViewBag.FK_Estado = new SelectList(db.Estado, "Id", "Nome", hotel.FK_Estado);
            return View(hotel);
        }
        //Post
        [HttpPost]
        public ActionResult obterCidades()
        {
            int id = Convert.ToInt32(Request["id"]);
            var cidades = db.Cidade.Where(c => c.FK_Estado == id);

            StringBuilder options = new StringBuilder();
            options.Append("<option value=''>Selecione sua cidade </option>");
            foreach (var cid in cidades)
            {
                options.Append("<option value='" + cid.Id + "'>" + cid.Nome + "</option>");
            }
            return Content(options.ToString());

        }
        // GET: Hotels/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = await db.Hotel.FindAsync(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_Cidade = new SelectList(db.Cidade, "Id", "Nome", hotel.FK_Cidade);
            ViewBag.FK_Estado = new SelectList(db.Estado, "Id", "Nome", hotel.FK_Estado);
            return View(hotel);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nome,Telefone,Longradouro,FK_Estado,FK_Cidade,Bairro,Cep,Img")] Hotel hotel, HttpPostedFileBase img)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string nomeImagemAntiga = Request["imagemAntiga"];
                    if (img != null) // se o usuário quis outra imagem
                    {

                        FileInfo imgInfo = new FileInfo(img.FileName);

                        if (imgInfo.Extension.Equals(".jpg", StringComparison.InvariantCultureIgnoreCase) || imgInfo.Extension.Equals(".png", StringComparison.InvariantCultureIgnoreCase) || imgInfo.Extension.Equals(".gif", StringComparison.InvariantCultureIgnoreCase))
                        {
                            string imgNome = "";
                            if (nomeImagemAntiga != string.Empty) // pra saber se já existe uma imagem deste hotel
                            {
                                var caminhoImagem = Server.MapPath("~/Imagens/Hoteis/" + nomeImagemAntiga);
                                System.IO.File.Delete(caminhoImagem);
                            }
                            imgNome = Guid.NewGuid().ToString("N") + imgInfo.Extension.ToLower();
                            img.SaveAs(Server.MapPath("~/Imagens/Hoteis/" + imgNome));
                            hotel.Img = imgNome;
                        }
                    }
                    else // se a imagem for nula eu atribuo null ao meu objeto
                    {

                        hotel.Img = nomeImagemAntiga;

                    }

                    db.Entry(hotel).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    TempData["Mensagem"] = "Edições realizadas com succeso";
                    TempData["tipo"] = "success";
                    return RedirectToAction("Details", new { id = hotel.Id });

                }
            }
            catch(Exception e)
            {
                TempData["Mensagem"] = "Ocorreu um erro ao editar este hotel , erro:";
                TempData["tipo"] = "error";
                TempData["Erro"] = e.GetType().Name;
                return RedirectToAction("Index");
            }
            ViewBag.FK_Cidade = new SelectList(db.Cidade, "Id", "Nome", hotel.FK_Cidade);
            ViewBag.FK_Estado = new SelectList(db.Estado, "Id", "Nome", hotel.FK_Estado);
            return View(hotel);
        }

        // GET: Hotels/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = await db.Hotel.FindAsync(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try {
                
                Hotel hotel = await db.Hotel.FindAsync(id);
                if (hotel.Img != string.Empty) // pra saber se já existe uma imagem deste hotel
                {
                    var caminhoImagem = Server.MapPath("~/Imagens/Hoteis/" + hotel.Img);
                    System.IO.File.Delete(caminhoImagem);
                }
                db.Hotel.Remove(hotel);
                await db.SaveChangesAsync();
                TempData["Mensagem"] = "Hotel Deletado com sucesso";
                TempData["tipo"] = "success";
                return RedirectToAction("Index");
            }catch(Exception e)
            {
                TempData["Mensagem"] = "Ocorreu um erro ao deletar este hotel , provavelmente ele está associado a alguma compra.";
                TempData["tipo"] = "error";
                TempData["Erro"] = e.GetType().Name;
                return RedirectToAction("Index");
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
