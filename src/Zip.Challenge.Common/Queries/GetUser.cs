using Zip.Challenge.Common.Dto;

namespace Zip.Challenge.Common.Queries
{
    public class GetUser : IQuery<User>
    {
        public GetUser(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}
