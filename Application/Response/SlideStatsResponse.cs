using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class SlideStatsResponse
    {
        public int Total { get; set; }  // Total de respuestas recibidas
        public int Correct { get; set; } // Numero de respuestas correctas
        public int Incorrect { get; set; } // Numero de respuestas incorrectas
        public double CorrectPercentage { get; set; } // Porcentaje de respuestas correctas
    }
}
