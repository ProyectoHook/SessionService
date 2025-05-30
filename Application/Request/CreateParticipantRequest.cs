
namespace Application.Request
{
    public class CreateParticipantRequest
    {        
        public required Guid idUser { get; set; }
        public required Guid idSession { get; set; }
        public string access_code { get; set; }
    }
}
