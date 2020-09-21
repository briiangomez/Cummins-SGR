using System;
using System.Configuration;
using System.Data.Objects;
using System.Runtime.CompilerServices;
namespace CMM.Survey.ModelsDb
{
	public class DbSurvey : ObjectContext
	{
		public const string ConceptualFilePath = "res://*/ModelsDb.DbSurvey.csdl";
		public const string StoreFilePath = "res://*/ModelsDb.DbSurvey.ssdl";
		public const string MappingFilePath = "res://*/ModelsDb.DbSurvey.msl";
		
		private static string g15ynZap8()
		{

            string str = string.Format("metadata={0}|{1}|{2};", ConceptualFilePath, StoreFilePath, MappingFilePath);
            string str2 = string.Format("provider={0};provider connection string=\"{1}\"", "System.Data.SqlClient", ConfigurationManager.ConnectionStrings["CMMCommunicator"].ConnectionString);
			return str + str2;
		}
		
		public DbSurvey() : base (DbSurvey.g15ynZap8(), "CMMCommunicator")
		{
			
		}
	}
}
