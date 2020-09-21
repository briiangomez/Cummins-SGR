namespace SGR.Communicator.Models
{
    using System;

    public class UserModelBase
    {
        //public void FromEntity(Portal portal)
        //{
        //    this.PortalId = portal.Id;
        //    this.PortalName = portal.Name;
        //}

        //public virtual void FromEntity(User user)
        //{
        //    this.FirstName = user.FirstName;
        //    this.MiddleName = user.MiddleName;
        //    this.LastName = user.LastName;
        //    this.EmailAddress = user.EmailAddress;
        //    this.IsAdmin = user.IsAdmin;
        //}

        //public virtual void ToEntity(User user)
        //{
        //    user.FirstName = this.FirstName;
        //    user.MiddleName = this.MiddleName;
        //    user.LastName = this.LastName;
        //    user.EmailAddress = this.EmailAddress;
        //    user.IsAdmin = this.IsAdmin;
        //}

        public string EmailAddress { get; set; }

        public string FirstName { get; set; }

        public bool IsAdmin { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public Guid PortalId { get; set; }

        public string PortalName { get; set; }
    }
}

