namespace SGR.Communicator.Models
{
    public class TagListModel : ListModel<Tag>
    {
        public AddTagModel AddModel { get; set; }
    }

	public class Tag
	{
		public virtual System.Guid Id
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
		public virtual int Count
		{

			get;

			set;
		}
		public virtual string Group
		{

			get;

			set;
		}

		public override string ToString()
		{
			return this.Name;
		}

		public override bool Equals(object obj)
		{
			return obj is Tag && this.Name.Equals((obj as Tag).Name);
		}

		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		public static Tag Ensure(System.Guid portalId, string name)
		{
			//name = name.Trim();
			//ITagRepository tagRepository = PersistenceManager.ResolveDao<ITagRepository>();
			//Tag tag = tagRepository.FindByName(portalId, name);
			//if (tag == null)
			//{
			//	tag = new Tag
			//	{
			//		//Id = System.Guid.NewGuid(), 
			//		Id = CMM.Data.SequentialGuid.NewSqlCompatibleGuid(),
			//		Name = name,
			//		PortalId = portalId
			//	};
			//	tagRepository.Add(tag);
			//}
			//return tag;
			return null;
		}

		public Tag()
		{


		}
	}
}

