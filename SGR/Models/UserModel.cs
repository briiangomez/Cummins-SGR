namespace SGR.Communicator.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class UserModel : UserModelBase
    {
        //public override void FromEntity(User user)
        //{
        //    base.FromEntity(user);
        //    this.UserName = user.Entry;
        //}

        public string UserName { get; set; }
    }
}

