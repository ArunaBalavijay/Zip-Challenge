namespace Zip.Challenge.Common.Events
{
    public class CreateAccountRejected : IRejectedEvent
    {
        protected CreateAccountRejected()
        {
        }

        public CreateAccountRejected(string email,
            string reason, string code)
        {
            UserEmail = email;
            Reason = reason;
            Code = code;
        }

        public string UserEmail { get; }
        public string Reason { get; }
        public string Code { get; }
    }
}
