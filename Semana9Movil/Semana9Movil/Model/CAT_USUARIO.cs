using System;
using System.Collections.Generic;
using System.Text;

namespace Semana9Movil.Model
{
    public class CAT_USUARIO
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public string Activa { get; set; }
    }


    public class links
    {
        public string rel { get; set; }
        public string href { get; set; }

        public override string ToString()
        {
            return href;
        }
    }


    public class ArrayProblem
    {
        public CAT_USUARIO[] items { get; set; }
        public bool hasMore { get; set; }
        public bool limit { get; set; }
        public bool offset { get; set; }
        public bool count { get; set; }
        public links[] links { get; set; }
    }
}
