using System;
using System.IO;
namespace CMM.Survey
{
	public interface ISurveyField
	{
		void Render(System.IO.TextWriter writer);
	}
}
