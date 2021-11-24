using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq;
using System.ComponentModel;

namespace ProyectoPlanetario.Models
{
    public class telefonum
    {
        [Display(Name = "telefonoid")]
        public int telefonoid { get; set; }

        [Required(ErrorMessage = "Falta la persona!")]
        public int personaid { get; set; }

        [Required(ErrorMessage = "Falta el Nombre!")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Falta el telefono!")]
        public string telefono { get; set; }

        [Required(ErrorMessage = "Falta el telefono!")]
        public IEnumerable<SelectListItem> personaid1  { get; set; }
    }
}