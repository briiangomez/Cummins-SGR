namespace SGR.Communicator.Models
{
    using CMM.ComponentModel.DataAnnotations;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class SignInModel
    {
        [DataType(DataType.Password), CMMRequired("Input your password")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        [CMMRequired("Input your user name")]
        public string UserName { get; set; }

        public string SubUserName { get; set; }
    }
}

