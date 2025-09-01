using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using Mono.CSharp;

namespace CSharpCompiler
{
	// Token: 0x02000003 RID: 3
	public class CodeCompiler : ICodeCompiler
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002250 File Offset: 0x00000450
		public CompilerResults CompileAssemblyFromDom(CompilerParameters options, CodeCompileUnit compilationUnit)
		{
			return this.CompileAssemblyFromDomBatch(options, new CodeCompileUnit[]
			{
				compilationUnit
			});
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002264 File Offset: 0x00000464
		public CompilerResults CompileAssemblyFromDomBatch(CompilerParameters options, CodeCompileUnit[] ea)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults result;
			try
			{
				result = this.CompileFromDomBatch(options, ea);
			}
			finally
			{
				options.TempFiles.Delete();
			}
			return result;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022A8 File Offset: 0x000004A8
		private CompilerResults CompileFromDomBatch(CompilerParameters options, CodeCompileUnit[] ea)
		{
			throw new NotImplementedException("sorry ICodeGenerator is not implemented, feel free to fix it and request merge");
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022B4 File Offset: 0x000004B4
		public CompilerResults CompileAssemblyFromFile(CompilerParameters options, string fileName)
		{
			return this.CompileAssemblyFromFileBatch(options, new string[]
			{
				fileName
			});
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022C8 File Offset: 0x000004C8
		public CompilerResults CompileAssemblyFromFileBatch(CompilerParameters options, string[] fileNames)
		{
			CompilerSettings compilerSettings = this.ParamsToSettings(options);
			foreach (string text in fileNames)
			{
				string fullPath = Path.GetFullPath(text);
				SourceFile item = new SourceFile(text, fullPath, compilerSettings.SourceFiles.Count + 1, null);
				compilerSettings.SourceFiles.Add(item);
			}
			return this.CompileFromCompilerSettings(compilerSettings, options.GenerateInMemory);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002327 File Offset: 0x00000527
		public CompilerResults CompileAssemblyFromSource(CompilerParameters options, string source)
		{
			return this.CompileAssemblyFromSourceBatch(options, new string[]
			{
				source
			});
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000233C File Offset: 0x0000053C
		public CompilerResults CompileAssemblyFromSourceBatch(CompilerParameters options, string[] sources)
		{
			CompilerSettings compilerSettings = this.ParamsToSettings(options);
			int num = 0;
			for (int i = 0; i < sources.Length; i++)
			{
				string source2 = sources[i];
				string source = source2;
				Func<Stream> streamIfDynamicFile = () => new MemoryStream(Encoding.UTF8.GetBytes(source ?? ""));
				string text = num.ToString();
				SourceFile item = new SourceFile(text, text, compilerSettings.SourceFiles.Count + 1, streamIfDynamicFile);
				compilerSettings.SourceFiles.Add(item);
				num++;
			}
			return this.CompileFromCompilerSettings(compilerSettings, options.GenerateInMemory);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000023C0 File Offset: 0x000005C0
		private CompilerResults CompileFromCompilerSettings(CompilerSettings settings, bool generateInMemory)
		{
			CompilerResults compilerResults = new CompilerResults(new TempFileCollection(Path.GetTempPath()));
			CustomDynamicDriver customDynamicDriver = new CustomDynamicDriver(new CompilerContext(settings, new CustomReportPrinter(compilerResults)));
			AssemblyBuilder compiledAssembly = null;
			try
			{
				customDynamicDriver.Compile(out compiledAssembly, AppDomain.CurrentDomain, generateInMemory);
			}
			catch (Exception ex)
			{
				compilerResults.Errors.Add(new CompilerError
				{
					IsWarning = false,
					ErrorText = ex.Message
				});
			}
			compilerResults.CompiledAssembly = compiledAssembly;
			return compilerResults;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002444 File Offset: 0x00000644
		private CompilerSettings ParamsToSettings(CompilerParameters parameters)
		{
			CompilerSettings compilerSettings = new CompilerSettings();
			foreach (string item in parameters.ReferencedAssemblies)
			{
				compilerSettings.AssemblyReferences.Add(item);
			}
			compilerSettings.Encoding = Encoding.UTF8;
			compilerSettings.GenerateDebugInfo = parameters.IncludeDebugInformation;
			compilerSettings.MainClass = parameters.MainClass;
			compilerSettings.Platform = Platform.AnyCPU;
			compilerSettings.StdLibRuntimeVersion = RuntimeVersion.v4;
			if (parameters.GenerateExecutable)
			{
				compilerSettings.Target = Target.Exe;
				compilerSettings.TargetExt = ".exe";
			}
			else
			{
				compilerSettings.Target = Target.Library;
				compilerSettings.TargetExt = ".dll";
			}
			if (parameters.GenerateInMemory)
			{
				compilerSettings.Target = Target.Library;
			}
			if (string.IsNullOrEmpty(parameters.OutputAssembly))
			{
				parameters.OutputAssembly = (compilerSettings.OutputFile = "DynamicAssembly_" + CodeCompiler.assemblyCounter.ToString() + compilerSettings.TargetExt);
				CodeCompiler.assemblyCounter += 1L;
			}
			compilerSettings.OutputFile = parameters.OutputAssembly;
			compilerSettings.Version = LanguageVersion.V_6;
			compilerSettings.WarningLevel = parameters.WarningLevel;
			compilerSettings.WarningsAreErrors = parameters.TreatWarningsAsErrors;
			return compilerSettings;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002584 File Offset: 0x00000784
		public CodeCompiler()
		{
		}

		// Token: 0x04000003 RID: 3
		private static long assemblyCounter;

		// Token: 0x02000014 RID: 20
		[CompilerGenerated]
		private sealed class <>c__DisplayClass7_0
		{
			// Token: 0x06000059 RID: 89 RVA: 0x00003772 File Offset: 0x00001972
			public <>c__DisplayClass7_0()
			{
			}

			// Token: 0x0600005A RID: 90 RVA: 0x0000377A File Offset: 0x0000197A
			internal Stream <CompileAssemblyFromSourceBatch>b__0()
			{
				return new MemoryStream(Encoding.UTF8.GetBytes(this.source ?? ""));
			}

			// Token: 0x04000019 RID: 25
			public string source;
		}
	}
}
