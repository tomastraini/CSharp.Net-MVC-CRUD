using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoPlanetario.Models;

namespace ProyectoPlanetario.Controllers
{
    public class PersonaController : Controller
    {

        public ActionResult Index(string search)
        {
            int i;
            DBPersona dbhandle = new DBPersona();
            ModelState.Clear();
            if (search == null)
            {
                return View(dbhandle.GetStudent());
            }
            else if (int.TryParse(search, out i))
            {
                return View(dbhandle.GetStudent().Where(m => m.creditomaximo.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList());
            }
            else
            {
                return View(dbhandle.GetStudent().Where(m => m.nombre.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList());
            }

        }

        public ActionResult Details(int id)
        {
            return View();
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(FormCollection collection, persona smodel)
        {
            if (ModelState.IsValid)
            {
                DBPersona sdb = new DBPersona();
                if (sdb.AddStudent(smodel))
                {
                    ViewBag.Message = "Persona agregada correctamente!";
                    ModelState.Clear();
                }
            }
            return View();
            

        }


        public ActionResult Edit(int id)
        {

            DBPersona sdb = new DBPersona();
            return View(sdb.GetStudent().Find(smodel => smodel.PersonaID == id));

        }

        [HttpPost]
        public ActionResult Edit(int id, persona smodel)
        {

            DBPersona sdb = new DBPersona();
            
            sdb.UpdateDetails(smodel);
            return RedirectToAction("Index");
            

        }


        public ActionResult Delete(int id)
        {
            try
            {
                DBPersona sdb = new DBPersona();
                if (sdb.DeleteStudent(id))
                {
                    ViewBag.AlertMsg = "Persona borrada correctamente!";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
