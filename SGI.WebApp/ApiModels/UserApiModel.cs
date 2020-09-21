using System.ComponentModel.DataAnnotations;

namespace SGI.WebApp.ApiModels
{
    public class UserApiModel : BaseApiModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string UserIdentifier { get; set; }

        public UserRoleApiModel[] UserRoles { get; set; }
    }
}
