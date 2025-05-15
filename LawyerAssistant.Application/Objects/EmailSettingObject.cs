namespace LawyerAssistant.Application.Objects
{
    public class EmailSettingObject
    {
        //public string Sender { get; set; }
        public string Receiver { get; }
        public string Subject { get; }
        public string Message { get; }
        public bool IsBodyHtml { get; set; }
    }
}
