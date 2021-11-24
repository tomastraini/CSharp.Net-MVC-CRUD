using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;


namespace ProyectoPlanetario.Models
{
    public class persona
    {
       

        [Display(Name = "PersonaID")]
        public int PersonaID { get; set; }

        [Required(ErrorMessage = "Falta el nombre!")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Falta la fecha de nacimiento!")]
        public string fechanacimiento { get; set; }

        [Required(ErrorMessage = "Falta el crédito máximo!")]
        public string creditomaximo { get; set; }


    }
   
}