using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSite_ClientesManager.Models;

namespace WebSite_ClientesManager.Controllers
{
    public class ClientesController : Controller
    {
        private bd_pruebaEntities db = new bd_pruebaEntities();

        // GET: Clientes
        public ActionResult Index()
        {
            return View(db.CLIENTES.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENTES cLIENTES = db.CLIENTES.Find(id);
            if (cLIENTES == null)
            {
                return HttpNotFound();
            }
            return View(cLIENTES);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Create([Bind(Include = "ID,NOMBRE_COMPLETO,IDENTIFICACION,TELEFONO")] CLIENTES cLIENTES)
        {
            System.Data.Entity.Core.Objects.ObjectParameter Msg = new System.Data.Entity.Core.Objects.ObjectParameter("P_Mensaje","");

            var query = db.STPR_CLIENTES_PRUEBA_MANTENIMIENTO(cLIENTES.NOMBRE_COMPLETO, cLIENTES.IDENTIFICACION, cLIENTES.TELEFONO, null, "I",Msg);

         
            return Msg.Value.ToString();
         
        }

        [HttpPost]
        public string CreateByAjax(string Nombre,string Iden, string Tel)
        {
            //Creo el objeto que contendra el mensaje
            System.Data.Entity.Core.Objects.ObjectParameter Msg = new System.Data.Entity.Core.Objects.ObjectParameter("P_Mensaje", "");
            //LLamo al Procedimiento
            var query = db.STPR_CLIENTES_PRUEBA_MANTENIMIENTO(Nombre, int.Parse(Iden), int.Parse(Tel), null, "I", Msg);
            return Msg.Value.ToString();
        }

        [HttpPost]
        public string EditByAjax(string Nombre, string Iden, string Tel, string Id)
        {
            //Creo el objeto que contendra el mensaje
            System.Data.Entity.Core.Objects.ObjectParameter Msg = new System.Data.Entity.Core.Objects.ObjectParameter("P_Mensaje", "");
            //LLamo al Procedimiento pasandole un A porque ahora modifica 
            var query = db.STPR_CLIENTES_PRUEBA_MANTENIMIENTO(Nombre, int.Parse(Iden), int.Parse(Tel), int.Parse(Id), "A", Msg);
            return Msg.Value.ToString();

        }

        [HttpPost]
        public string DeleteByAjax(string Nombre, string Iden, string Tel, string Id)
        {
            //Creo el objeto que contendra el mensaje
            System.Data.Entity.Core.Objects.ObjectParameter Msg = new System.Data.Entity.Core.Objects.ObjectParameter("P_Mensaje", "");
            //LLamo al Procedimiento pasandole un A porque ahora modifica 
            var query = db.STPR_CLIENTES_PRUEBA_MANTENIMIENTO(null, null, null, int.Parse(Id), "B", Msg);
            return Msg.Value.ToString();
        }

        [HttpPost]
        public ActionResult FindByAjax(string Nombre)
        {
                     //LLamo al Procedimiento pasandole un A porque ahora modifica 
            var query = db.FTN_CLIENTES_PRUEBA_LISTA_CLIENTES(Nombre);


            return Json(new
            {
                query = query
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int Check_Iden_ByAjax(int Iden)
        {
            //Aki miro si existe en BD un registro con esa identificacion (si la lista viene con valores es que hay y lo guado en Cantidad)
            int Cantidad = db.CLIENTES.Where(m => m.IDENTIFICACION == Iden).ToList().Count;

            return Cantidad;

        }


        // GET: Clientes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENTES cLIENTES = db.CLIENTES.Find(id);
            if (cLIENTES == null)
            {
                return HttpNotFound();
            }
            return View(cLIENTES);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NOMBRE_COMPLETO,IDENTIFICACION,TELEFONO")] CLIENTES cLIENTES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cLIENTES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cLIENTES);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENTES cLIENTES = db.CLIENTES.Find(id);
            if (cLIENTES == null)
            {
                return HttpNotFound();
            }
            return View(cLIENTES);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CLIENTES cLIENTES = db.CLIENTES.Find(id);
            db.CLIENTES.Remove(cLIENTES);
            db.SaveChanges();
            return RedirectToAction("Index");
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
