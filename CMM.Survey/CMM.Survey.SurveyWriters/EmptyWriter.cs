
using System;
using System.IO;
using System.Runtime.CompilerServices;
namespace CMM.Survey.SurveyWriters
{
	public class EmptyWriter : ISurveyWriter
	{
		
		public void Initialize(SurveyFormClass survey)
		{
		}
		
		public void Write(System.IO.TextWriter writer)
		{
		}
		
		public EmptyWriter()
		{
			
			
		}
	}
}
