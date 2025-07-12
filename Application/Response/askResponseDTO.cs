using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Response
{
    public class AskResponseDTO
    {
        public int IdAsk { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AskText { get; set; }
        public List<OptionResponseDTO> Options { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
