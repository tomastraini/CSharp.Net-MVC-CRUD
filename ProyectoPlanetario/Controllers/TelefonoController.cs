using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoPlanetario.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace ProyectoPlanetario.Controllers
{
    public class TelefonoController : Controller
    {

        
        public SqlConnection con;
        public List<SelectListItem>  list;

        public void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["personConn"].ToString();
            con = new SqlConnection(constring);
        }


        public ActionResult Index(string search)
        {
            int i;
            DBTelefono dbhandle = new DBTelefono();
            ModelState.Clear();
            if (search == null)
            {
                return View(dbhandle.getTelefonoPer());
            }
            else if (int.TryParse(search, out i))
            {
                return View(dbhandle.getTelefonoPer().Where(m => m.telefono.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList());  
            }
            else
            {
                return View(dbhandle.getTelefonoPer().Where(m => m.nombre.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList());
            }

        }

        public ActionResult Create()
        {
            try
            {
                connection();
                SqlCommand cmd = new SqlCommand("GetPersonaDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                con.Open();
                sd.Fill(dt);
                con.Close();

                ViewBag.personaid1 = ToSelectList(dt, "PersonaID", "nombre");

                return View();
            }
            catch
            {
                return View();
            }
        }

        [NonAction]
        public SelectList ToSelectList(DataTable table, string valueField, string textField)
        {
            list = new List<SelectListItem>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row[textField].ToString(),
                    Value = row[valueField].ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }

        [HttpPost]
        public ActionResult Create(telefonum smodel)
        {
            try
            {
                connection();
                SqlCommand cmd = new SqlCommand("GetPersonaDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                con.Open();
                sd.Fill(dt);
                con.Close();
                DBTelefono sdb = new DBTelefono();
                ViewBag.personaid1 = ToSelectList(dt, "PersonaID", "nombre");

                sdb.agregarTelefono(smodel);
                ModelState.Clear();
                return View();
            }
            catch
            {
                return View();
            }
           
        }

        public ActionResult Edit(int id)
        {
            DBTelefono sdb = new DBTelefono();
            return View(sdb.getTelefonoPer().Find(smodel => smodel.telefonoid == id));
        }


        [HttpPost]
        public ActionResult Edit(int id, telefonum smodel)
        {

            DBTelefono sdb = new DBTelefono();
            sdb.updatetelefonos(smodel);
            return RedirectToAction("Index");
            

        }


        public ActionResult Delete(int id)
        {
            try
            {
                DBTelefono sdb = new DBTelefono();
                if (sdb.borrartelefono(id))
                {
                    ViewBag.AlertMsg = "Student Deleted Successfully";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
