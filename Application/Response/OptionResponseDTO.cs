using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Response
{
    public class OptionResponseDTO
    {
        public int IdOption { get; set; }
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
        public int IdAsk { get; set; }
      //  public DateTime? ModifiedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
