using LawyerAssistant.Domain.Base;
using LawyerAssistant.Domain.Base.Contracts;

namespace LawyerAssistant.Domain.Aggregates;

public class SMSCenterModel : ModifyDateTimeWithUserModel, IEntity
{

    protected SMSCenterModel()
    {
        
    }
    public SMSCenterModel(string messageId , int reactionId)
    {
        MessageId = messageId;
        ReactionId = reactionId;
        RegDateTime = DateTime.UtcNow;
    }
    public void Edit(int status, string statusText)
    { 
        Status = status;
        StatusText = statusText;
        ModDateTime = DateTime.UtcNow;
    }
    public string MessageId { get; set; }
    public int ReactionId { get; set; }
    public int? Status { get; set; }
    public string? StatusText { get; set; }
    public ReActionModel Reaction { get; set; }
}
