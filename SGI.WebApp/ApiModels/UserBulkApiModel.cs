using System.Collections.Generic;

namespace SGI.WebApp.ApiModels
{
    public class UserBulkApiModel
    {
        public string Content { get; set; }
        public IEnumerable<UserRoleApiModel> RoleId { get; set; }
    }

    public class BulkUserResultApiModel
    {

        public string Line { get; set; }
        public string Message { get; set; }

        public BulkUserResultApiModel(string line, string message)
        {
            this.Line = line;
            this.Message = message;
        }
    }
}
