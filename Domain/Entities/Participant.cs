using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Participant
    {
        public int idParticipant { get; set; }
        public required Guid idUser { get; set; }
        public required DateTime connectionStart { get; set; }
        public required bool activityStatus { get; set; }
        public int? connectionId { get; set; }

        public required int idSession;
        public Session session { get; set; }
    }
}
