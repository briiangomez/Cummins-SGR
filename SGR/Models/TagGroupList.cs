namespace SGR.Communicator.Models
{
    using System;

    public class TagGroupList : ListModel<EnumItem>
    {
        public TagGroupModel AddModel { get; set; }

        public Guid? EditItemId { get; set; }

        public TagGroupModel EditModel { get; set; }
    }

    public class EnumItem
    {
        public virtual System.Guid Id
        {

            get;

            set;
        }
        public string EnumType
        {

            get;

            set;
        }
        public virtual System.Guid PortalId
        {

            get;

            set;
        }
        public virtual string Name
        {

            get;

            set;
        }

        public EnumItem()
        {


        }
    }
}