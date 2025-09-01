using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
	// Token: 0x02000173 RID: 371
	internal static class MonoToolsLocator
	{
		// Token: 0x060009DA RID: 2522 RVA: 0x0002B6C8 File Offset: 0x000298C8
		static MonoToolsLocator()
		{
			string directoryName = Path.GetDirectoryName((string)typeof(Environment).GetProperty("GacPath", BindingFlags.Static | BindingFlags.NonPublic).GetGetMethod(true).Invoke(null, null));
			if (Path.DirectorySeparatorChar == '\\')
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				MonoToolsLocator.GetModuleFileName(IntPtr.Zero, stringBuilder, stringBuilder.Capacity);
				string text = stringBuilder.ToString();
				string fileName = Path.GetFileName(text);
				if (fileName.StartsWith("mono") && fileName.EndsWith(".exe"))
				{
					MonoToolsLocator.Mono = text;
				}
				if (!File.Exists(MonoToolsLocator.Mono))
				{
					MonoToolsLocator.Mono = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(directoryName)), "bin\\mono.exe");
				}
				if (!File.Exists(MonoToolsLocator.Mono))
				{
					MonoToolsLocator.Mono = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(directoryName))), "mono\\mini\\mono.exe");
				}
				MonoToolsLocator.McsCSharpCompiler = Path.Combine(directoryName, "4.5", "mcs.exe");
				if (!File.Exists(MonoToolsLocator.McsCSharpCompiler))
				{
					MonoToolsLocator.McsCSharpCompiler = Path.Combine(Path.GetDirectoryName(directoryName), "lib", "net_4_x", "mcs.exe");
				}
				MonoToolsLocator.VBCompiler = Path.Combine(directoryName, "4.5\\vbnc.exe");
				MonoToolsLocator.AssemblyLinker = Path.Combine(directoryName, "4.5\\al.exe");
				if (!File.Exists(MonoToolsLocator.AssemblyLinker))
				{
					MonoToolsLocator.AssemblyLinker = Path.Combine(Path.GetDirectoryName(directoryName), "lib\\net_4_x\\al.exe");
					return;
				}
			}
			else
			{
				MonoToolsLocator.Mono = Path.Combine(directoryName, "bin", "mono");
				if (!File.Exists(MonoToolsLocator.Mono))
				{
					MonoToolsLocator.Mono = "mono";
				}
				string localPath = new Uri(typeof(object).Assembly.CodeBase).LocalPath;
				MonoToolsLocator.McsCSharpCompiler = Path.GetFullPath(Path.Combine(new string[]
				{
					localPath,
					"..",
					"..",
					"..",
					"..",
					"bin",
					"mcs"
				}));
				if (!File.Exists(MonoToolsLocator.McsCSharpCompiler))
				{
					MonoToolsLocator.McsCSharpCompiler = "mcs";
				}
				MonoToolsLocator.VBCompiler = Path.GetFullPath(Path.Combine(new string[]
				{
					localPath,
					"..",
					"..",
					"..",
					"..",
					"bin",
					"vbnc"
				}));
				if (!File.Exists(MonoToolsLocator.VBCompiler))
				{
					MonoToolsLocator.VBCompiler = "vbnc";
				}
				MonoToolsLocator.AssemblyLinker = Path.GetFullPath(Path.Combine(new string[]
				{
					localPath,
					"..",
					"..",
					"..",
					"..",
					"bin",
					"al"
				}));
				if (!File.Exists(MonoToolsLocator.AssemblyLinker))
				{
					MonoToolsLocator.AssemblyLinker = "al";
				}
			}
		}

		// Token: 0x060009DB RID: 2523
		[DllImport("kernel32.dll")]
		private static extern uint GetModuleFileName([In] IntPtr hModule, [Out] StringBuilder lpFilename, [In] int nSize);

		// Token: 0x040006AC RID: 1708
		public static readonly string Mono;

		// Token: 0x040006AD RID: 1709
		public static readonly string McsCSharpCompiler;

		// Token: 0x040006AE RID: 1710
		public static readonly string VBCompiler;

		// Token: 0x040006AF RID: 1711
		public static readonly string AssemblyLinker;
	}
}
