using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;
using System.Security.Cryptography;
using Mono.CSharp;

namespace CSharpCompiler
{
	// Token: 0x02000004 RID: 4
	public class CustomDynamicDriver
	{
		// Token: 0x06000011 RID: 17 RVA: 0x0000258C File Offset: 0x0000078C
		public CustomDynamicDriver(CompilerContext ctx)
		{
			this.ctx = ctx;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000259B File Offset: 0x0000079B
		public Report Report
		{
			get
			{
				return this.ctx.Report;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000025A8 File Offset: 0x000007A8
		private void tokenize_file(SourceFile sourceFile, ModuleContainer module, ParserSession session)
		{
			Stream dataStream;
			try
			{
				dataStream = sourceFile.GetDataStream();
			}
			catch
			{
				this.Report.Error(2001, "Source file `" + sourceFile.Name + "' could not be found");
				return;
			}
			using (dataStream)
			{
				SeekableStreamReader input = new SeekableStreamReader(dataStream, this.ctx.Settings.Encoding, null);
				CompilationSourceFile file = new CompilationSourceFile(module, sourceFile);
				Tokenizer tokenizer = new Tokenizer(input, file, session, this.ctx.Report);
				int num = 0;
				int num2 = 0;
				int num3;
				while ((num3 = tokenizer.token()) != 257)
				{
					num++;
					if (num3 == 259)
					{
						num2++;
					}
				}
				Console.WriteLine(string.Concat(new string[]
				{
					"Tokenized: ",
					num.ToString(),
					" found ",
					num2.ToString(),
					" errors"
				}));
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000026B0 File Offset: 0x000008B0
		public void Parse(ModuleContainer module)
		{
			bool tokenizeOnly = module.Compiler.Settings.TokenizeOnly;
			List<SourceFile> sourceFiles = module.Compiler.SourceFiles;
			Location.Initialize(sourceFiles);
			ParserSession session = new ParserSession
			{
				UseJayGlobalArrays = true,
				LocatedTokens = new LocatedToken[15000]
			};
			for (int i = 0; i < sourceFiles.Count; i++)
			{
				if (tokenizeOnly)
				{
					this.tokenize_file(sourceFiles[i], module, session);
				}
				else
				{
					this.Parse(sourceFiles[i], module, session, this.Report);
				}
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002738 File Offset: 0x00000938
		public void Parse(SourceFile file, ModuleContainer module, ParserSession session, Report report)
		{
			Stream dataStream;
			try
			{
				dataStream = file.GetDataStream();
			}
			catch
			{
				report.Error(2001, "Source file `{0}' could not be found", file.Name);
				return;
			}
			if (dataStream.ReadByte() == 77 && dataStream.ReadByte() == 90)
			{
				report.Error(2015, "Source file `{0}' is a binary file and not a text file", file.Name);
				dataStream.Close();
				return;
			}
			dataStream.Position = 0L;
			SeekableStreamReader seekableStreamReader = new SeekableStreamReader(dataStream, this.ctx.Settings.Encoding, session.StreamReaderBuffer);
			CustomDynamicDriver.Parse(seekableStreamReader, file, module, session, report);
			if (this.ctx.Settings.GenerateDebugInfo && report.Errors == 0 && !file.HasChecksum)
			{
				dataStream.Position = 0L;
				MD5 checksumAlgorithm = session.GetChecksumAlgorithm();
				file.SetChecksum(checksumAlgorithm.ComputeHash(dataStream));
			}
			seekableStreamReader.Dispose();
			dataStream.Close();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002828 File Offset: 0x00000A28
		public static void Parse(SeekableStreamReader reader, SourceFile sourceFile, ModuleContainer module, ParserSession session, Report report)
		{
			CompilationSourceFile compilationSourceFile = new CompilationSourceFile(module, sourceFile);
			module.AddTypeContainer(compilationSourceFile);
			new CSharpParser(reader, compilationSourceFile, report, session).parse();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002854 File Offset: 0x00000A54
		public bool Compile(out AssemblyBuilder outAssembly, AppDomain domain, bool generateInMemory)
		{
			CompilerSettings settings = this.ctx.Settings;
			outAssembly = null;
			if (settings.FirstSourceFile == null && (settings.Target == Target.Exe || settings.Target == Target.WinExe || settings.Target == Target.Module || settings.Resources == null))
			{
				this.Report.Error(2008, "No files to compile were specified");
				return false;
			}
			if (settings.Platform == Platform.AnyCPU32Preferred && (settings.Target == Target.Library || settings.Target == Target.Module))
			{
				this.Report.Error(4023, "Platform option `anycpu32bitpreferred' is valid only for executables");
				return false;
			}
			TimeReporter timeReporter = new TimeReporter(settings.Timestamps);
			this.ctx.TimeReporter = timeReporter;
			timeReporter.StartTotal();
			ModuleContainer moduleContainer = new ModuleContainer(this.ctx);
			RootContext.ToplevelTypes = moduleContainer;
			timeReporter.Start(TimeReporter.TimerType.ParseTotal);
			this.Parse(moduleContainer);
			timeReporter.Stop(TimeReporter.TimerType.ParseTotal);
			if (this.Report.Errors > 0)
			{
				return false;
			}
			if (settings.TokenizeOnly || settings.ParseOnly)
			{
				timeReporter.StopTotal();
				timeReporter.ShowStats();
				return true;
			}
			string outputFile = settings.OutputFile;
			string fileName = Path.GetFileName(outputFile);
			AssemblyDefinitionDynamic assemblyDefinitionDynamic = new AssemblyDefinitionDynamic(moduleContainer, fileName, outputFile);
			moduleContainer.SetDeclaringAssembly(assemblyDefinitionDynamic);
			ReflectionImporter importer = new ReflectionImporter(moduleContainer, this.ctx.BuiltinTypes);
			assemblyDefinitionDynamic.Importer = importer;
			DynamicLoader dynamicLoader = new DynamicLoader(importer, this.ctx);
			dynamicLoader.LoadReferences(moduleContainer);
			if (!this.ctx.BuiltinTypes.CheckDefinitions(moduleContainer))
			{
				return false;
			}
			if (!assemblyDefinitionDynamic.Create(domain, AssemblyBuilderAccess.RunAndSave))
			{
				return false;
			}
			moduleContainer.CreateContainer();
			dynamicLoader.LoadModules(assemblyDefinitionDynamic, moduleContainer.GlobalRootNamespace);
			moduleContainer.InitializePredefinedTypes();
			if (settings.GetResourceStrings != null)
			{
				moduleContainer.LoadGetResourceStrings(settings.GetResourceStrings);
			}
			timeReporter.Start(TimeReporter.TimerType.ModuleDefinitionTotal);
			moduleContainer.Define();
			timeReporter.Stop(TimeReporter.TimerType.ModuleDefinitionTotal);
			if (this.Report.Errors > 0)
			{
				return false;
			}
			if (settings.DocumentationFile != null)
			{
				new DocumentationBuilder(moduleContainer).OutputDocComment(outputFile, settings.DocumentationFile);
			}
			assemblyDefinitionDynamic.Resolve();
			if (this.Report.Errors > 0)
			{
				return false;
			}
			timeReporter.Start(TimeReporter.TimerType.EmitTotal);
			assemblyDefinitionDynamic.Emit();
			timeReporter.Stop(TimeReporter.TimerType.EmitTotal);
			if (this.Report.Errors > 0)
			{
				return false;
			}
			timeReporter.Start(TimeReporter.TimerType.CloseTypes);
			moduleContainer.CloseContainer();
			timeReporter.Stop(TimeReporter.TimerType.CloseTypes);
			timeReporter.Start(TimeReporter.TimerType.Resouces);
			if (!settings.WriteMetadataOnly)
			{
				assemblyDefinitionDynamic.EmbedResources();
			}
			timeReporter.Stop(TimeReporter.TimerType.Resouces);
			if (this.Report.Errors > 0)
			{
				return false;
			}
			if (!generateInMemory)
			{
				assemblyDefinitionDynamic.Save();
			}
			outAssembly = assemblyDefinitionDynamic.Builder;
			timeReporter.StopTotal();
			timeReporter.ShowStats();
			return this.Report.Errors == 0;
		}

		// Token: 0x04000004 RID: 4
		private readonly CompilerContext ctx;
	}
}
