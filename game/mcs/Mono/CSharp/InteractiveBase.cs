using System;
using System.IO;

namespace Mono.CSharp
{
	// Token: 0x02000172 RID: 370
	public class InteractiveBase
	{
		// Token: 0x060011D8 RID: 4568 RVA: 0x0004A294 File Offset: 0x00048494
		public static void ShowVars()
		{
			InteractiveBase.Output.Write(InteractiveBase.Evaluator.GetVars());
			InteractiveBase.Output.Flush();
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0004A2B4 File Offset: 0x000484B4
		public static void ShowUsing()
		{
			InteractiveBase.Output.Write(InteractiveBase.Evaluator.GetUsing());
			InteractiveBase.Output.Flush();
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0004A2D4 File Offset: 0x000484D4
		public static TimeSpan Time(Action a)
		{
			DateTime now = DateTime.Now;
			a();
			return DateTime.Now - now;
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0004A2F8 File Offset: 0x000484F8
		public static void LoadPackage(string pkg)
		{
			if (pkg == null)
			{
				InteractiveBase.Error.WriteLine("Invalid package specified");
				return;
			}
			foreach (string text in Driver.GetPackageFlags(pkg, null).Trim(new char[]
			{
				' ',
				'\n',
				'\r',
				'\t'
			}).Split(new char[]
			{
				' ',
				'\t'
			}))
			{
				if (text.StartsWith("-r:") || text.StartsWith("/r:") || text.StartsWith("/reference:"))
				{
					string text2 = text;
					string file = text2.Substring(text2.IndexOf(':') + 1);
					InteractiveBase.Evaluator.LoadAssembly(file);
				}
			}
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0004A3A0 File Offset: 0x000485A0
		public static void LoadAssembly(string assembly)
		{
			InteractiveBase.Evaluator.LoadAssembly(assembly);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0004A3AD File Offset: 0x000485AD
		public static void print(object obj)
		{
			InteractiveBase.Output.WriteLine(obj);
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0004A3BA File Offset: 0x000485BA
		public static void print(string fmt, params object[] args)
		{
			InteractiveBase.Output.WriteLine(fmt, args);
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x0004A3C8 File Offset: 0x000485C8
		public static string help
		{
			get
			{
				return "Static methods:\n  Describe (object);       - Describes the object's type\n  LoadPackage (package);   - Loads the given Package (like -pkg:FILE)\n  LoadAssembly (assembly); - Loads the given assembly (like -r:ASSEMBLY)\n  ShowVars ();             - Shows defined local variables.\n  ShowUsing ();            - Show active using declarations.\n  Prompt                   - The prompt used by the C# shell\n  ContinuationPrompt       - The prompt for partial input\n  Time (() => { });        - Times the specified code\n  print (obj);             - Shorthand for Console.WriteLine\n  quit;                    - You'll never believe it - this quits the repl!\n  help;                    - This help text\n";
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x0004A3CF File Offset: 0x000485CF
		public static object quit
		{
			get
			{
				InteractiveBase.QuitRequested = true;
				return typeof(Evaluator.QuitValue);
			}
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0004A3E1 File Offset: 0x000485E1
		public static void Quit()
		{
			InteractiveBase.QuitRequested = true;
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x00002CCC File Offset: 0x00000ECC
		public InteractiveBase()
		{
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0004A3E9 File Offset: 0x000485E9
		// Note: this type is marked as 'beforefieldinit'.
		static InteractiveBase()
		{
		}

		// Token: 0x040007A0 RID: 1952
		public static TextWriter Output = Console.Out;

		// Token: 0x040007A1 RID: 1953
		public static TextWriter Error = Console.Error;

		// Token: 0x040007A2 RID: 1954
		public static string Prompt = "csharp> ";

		// Token: 0x040007A3 RID: 1955
		public static string ContinuationPrompt = "      > ";

		// Token: 0x040007A4 RID: 1956
		public static bool QuitRequested;

		// Token: 0x040007A5 RID: 1957
		public static Evaluator Evaluator;
	}
}
