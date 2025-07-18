﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Session
    {
        public Guid SessionId { get; set; }
        public int? access_code { get; set; }
        public required string description { get; set; }
        public int interation_count { get; set; }
        public required bool active_status { get; set; }
        public required int max_participants { get; set; }
        public required DateTime start_time { get; set; }
        public DateTime? end_time { get; set; }
        public required int presentation_id { get; set; }
        public required Guid created_by { get; set; }
        public int? currentSlide { get; set; }
        public AccesCode AccesCode { get; set; }
        public IList<Participant> Participants { get; set; }
    }
}
