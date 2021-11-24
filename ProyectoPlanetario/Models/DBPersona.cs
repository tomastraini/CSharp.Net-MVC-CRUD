using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace ProyectoPlanetario.Models
{
    public class DBPersona
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["personConn"].ToString();
            con = new SqlConnection(constring);
        }

        public bool AddStudent(persona smodel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("agregarPersona", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@nombre", smodel.nombre);
            cmd.Parameters.AddWithValue("@fechanacimiento", smodel.fechanacimiento);
            cmd.Parameters.AddWithValue("@creditomaximo", smodel.creditomaximo);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        // ********** VIEW PERSON DETAILS ********************
        public List<persona> GetStudent()
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

        // ***************** UPDATE PERSON DETAILS *********************
        public bool UpdateDetails(persona smodel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("updatepersonas", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PersonaID", Convert.ToInt32((smodel.PersonaID)));
            cmd.Parameters.AddWithValue("@nombre", smodel.nombre);
            cmd.Parameters.AddWithValue("@fechanacimiento", smodel.fechanacimiento);
            cmd.Parameters.AddWithValue("@creditomaximo", Convert.ToDouble(smodel.creditomaximo));

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        // ********************** DELETE PERSON DETAILS *******************
        public bool DeleteStudent(int id)
        {
            connection();
            SqlCommand cmd = new SqlCommand("borrarpersona", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PersonaID", id);

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