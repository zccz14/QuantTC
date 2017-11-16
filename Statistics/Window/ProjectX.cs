using System;

namespace QuantTC.Statistics.Window
{
	public static class ProjectX
	{
		/// <summary>
		/// Print Window Project's OCSS Results
		/// </summary>
		/// <param name="project"></param>
		public static void Print(this Project project)
		{
			project.OCSSResults.ForEach(ovp =>
			{
				var oc = ovp.Key;
				Console.WriteLine($"{oc.Title}");
				ovp.Value.ForEach(cvp =>
				{
					var cc = cvp.Key;
					Console.WriteLine($"\t{cc.Title}");
					cvp.Value.ForEach(svp =>
					{
						var subject = svp.Key;
						Console.WriteLine($"\t\t{subject.Title}");
						svp.Value.ForEach(srp =>
						{
							var summary = srp.Key;
							var result = srp.Value;
							Console.WriteLine($"\t\t\t{summary.Title}: {result}");
						});
					});
				});
			});
		}
	}
}