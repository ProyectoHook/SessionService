using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request
{
    public class SlideSnapshotDto
    {
        public int SlideId { get; set; }
        public int SlideIndex { get; set; }
        public string? Ask { get; set; }
        public string? AnswerCorrect { get; set; }
        public List<string>? Options { get; set; }
        public List<ParticipantHistoryDto> ConnectedUserIds { get; set; }
        public Guid UserCreateId { get; set; }
        public int presentationId { get; set; }
    }
}
