using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoPlanetario.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ProyectoPlanetario.Models
{
    public class DBTelefono
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["personConn"].ToString();
            con = new SqlConnection(constring);
        }

        public List<persona> getPersoning()
        {
            connection();
            List<persona> studentlist = new List<persona>();

            SqlCommand cmd = new SqlCommand("GetPersonaDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                studentlist.Add(
                    new persona
                    {
                        PersonaID = Convert.ToInt32(dr["PersonaID"]),
                        nombre = Convert.ToString(dr["nombre"]),
                        fechanacimiento = Convert.ToString(dr["fechanacimiento"]),
                        creditomaximo = Convert.ToString(dr["creditomaximo"])
                    });
            }
            return studentlist;
        }


        public bool agregarTelefono(telefonum smodel)
        {
            try
            {
                connection();

                SqlCommand cmd = new SqlCommand("agregarTelefono", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@personid", smodel.personaid);
                cmd.Parameters.AddWithValue("@telf", smodel.telefono);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i >= 1)
                    return true;

                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        public List<telefonum> getTelefonoPer()
        {
            connection();
            List<telefonum> studentlist = new List<telefonum>();

            SqlCommand cmd = new SqlCommand("getTelefonoper", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                studentlist.Add(
                    new telefonum
                    {
                        telefonoid = Convert.ToInt32(dr["telefonoid"]),
                        personaid = Convert.ToInt32(dr["personaid"]),
                        nombre = Convert.ToString(dr["nombre"]),
                        telefono = Convert.ToString(dr["telefono"]),
                    });
            }
            return studentlist;
        }

        public bool updatetelefonos(telefonum smodel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("updatetelefonos", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@telfid", Convert.ToInt32((smodel.telefonoid)));
            cmd.Parameters.AddWithValue("@telf", smodel.telefono.ToString());
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }


        public bool borrartelefono(int id)
        {
            connection();
            SqlCommand cmd = new SqlCommand("borrartelefono", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@telfid", id);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}