using System;
using System.IO;
namespace CMM.Survey
{
	public interface ISurveyWriter
	{
		void Initialize(SurveyFormClass survey);
		void Write(System.IO.TextWriter writer);
	}
}
