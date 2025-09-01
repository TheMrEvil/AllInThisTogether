using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection.Emit;
using System.Security.Cryptography;

namespace Mono.CSharp
{
	// Token: 0x0200019B RID: 411
	public class Driver
	{
		// Token: 0x06001614 RID: 5652 RVA: 0x0006A4E7 File Offset: 0x000686E7
		public Driver(CompilerContext ctx)
		{
			this.ctx = ctx;
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x0006A4F6 File Offset: 0x000686F6
		public Report Report
		{
			get
			{
				return this.ctx.Report;
			}
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x0006A504 File Offset: 0x00068704
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
				Console.WriteLine(string.Concat(new object[]
				{
					"Tokenized: ",
					num,
					" found ",
					num2,
					" errors"
				}));
			}
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x0006A60C File Offset: 0x0006880C
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

		// Token: 0x06001618 RID: 5656 RVA: 0x0006A694 File Offset: 0x00068894
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
			Driver.Parse(seekableStreamReader, file, module, session, report);
			if (this.ctx.Settings.GenerateDebugInfo && report.Errors == 0 && !file.HasChecksum)
			{
				dataStream.Position = 0L;
				MD5 checksumAlgorithm = session.GetChecksumAlgorithm();
				file.SetChecksum(checksumAlgorithm.ComputeHash(dataStream));
			}
			seekableStreamReader.Dispose();
			dataStream.Close();
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x0006A784 File Offset: 0x00068984
		public static void Parse(SeekableStreamReader reader, SourceFile sourceFile, ModuleContainer module, ParserSession session, Report report)
		{
			CompilationSourceFile compilationSourceFile = new CompilationSourceFile(module, sourceFile);
			module.AddTypeContainer(compilationSourceFile);
			new CSharpParser(reader, compilationSourceFile, report, session).parse();
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x0006A7B0 File Offset: 0x000689B0
		public static int Main(string[] args)
		{
			Location.InEmacs = (Environment.GetEnvironmentVariable("EMACS") == "t");
			CommandLineParser commandLineParser = new CommandLineParser(Console.Out);
			CompilerSettings compilerSettings = commandLineParser.ParseArguments(args);
			if (compilerSettings == null)
			{
				return 1;
			}
			if (commandLineParser.HasBeenStopped)
			{
				return 0;
			}
			Driver driver = new Driver(new CompilerContext(compilerSettings, new ConsoleReportPrinter()));
			if (driver.Compile() && driver.Report.Errors == 0)
			{
				if (driver.Report.Warnings > 0)
				{
					Console.WriteLine("Compilation succeeded - {0} warning(s)", driver.Report.Warnings);
				}
				Environment.Exit(0);
				return 0;
			}
			Console.WriteLine("Compilation failed: {0} error(s), {1} warnings", driver.Report.Errors, driver.Report.Warnings);
			Environment.Exit(1);
			return 1;
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x0006A880 File Offset: 0x00068A80
		public static string GetPackageFlags(string packages, Report report)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.FileName = "pkg-config";
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.UseShellExecute = false;
			processStartInfo.Arguments = "--libs " + packages;
			Process process = null;
			try
			{
				process = Process.Start(processStartInfo);
			}
			catch (Exception ex)
			{
				if (report == null)
				{
					throw;
				}
				report.Error(-27, "Couldn't run pkg-config: " + ex.Message);
				return null;
			}
			if (process.StandardOutput == null)
			{
				if (report == null)
				{
					throw new ApplicationException("Specified package did not return any information");
				}
				report.Warning(-27, 1, "Specified package did not return any information");
				process.Close();
				return null;
			}
			else
			{
				string text = process.StandardOutput.ReadToEnd();
				process.WaitForExit();
				if (process.ExitCode == 0)
				{
					process.Close();
					return text;
				}
				if (report == null)
				{
					throw new ApplicationException(text);
				}
				report.Error(-27, "Error running pkg-config. Check the above output.");
				process.Close();
				return null;
			}
			string result;
			return result;
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x0006A96C File Offset: 0x00068B6C
		public bool Compile()
		{
			CompilerSettings settings = this.ctx.Settings;
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
			string text = settings.OutputFile;
			string text2;
			if (text == null)
			{
				SourceFile firstSourceFile = settings.FirstSourceFile;
				if (firstSourceFile == null)
				{
					this.Report.Error(1562, "If no source files are specified you must specify the output file with -out:");
					return false;
				}
				text2 = firstSourceFile.Name;
				int num = text2.LastIndexOf('.');
				if (num > 0)
				{
					text2 = text2.Substring(0, num);
				}
				text2 += settings.TargetExt;
				text = text2;
			}
			else
			{
				text2 = Path.GetFileName(text);
				if (string.IsNullOrEmpty(Path.GetFileNameWithoutExtension(text2)) || text2.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
				{
					this.Report.Error(2021, "Output file name is not valid");
					return false;
				}
			}
			AssemblyDefinitionDynamic assemblyDefinitionDynamic = new AssemblyDefinitionDynamic(moduleContainer, text2, text);
			moduleContainer.SetDeclaringAssembly(assemblyDefinitionDynamic);
			ReflectionImporter importer = new ReflectionImporter(moduleContainer, this.ctx.BuiltinTypes);
			assemblyDefinitionDynamic.Importer = importer;
			DynamicLoader dynamicLoader = new DynamicLoader(importer, this.ctx);
			dynamicLoader.LoadReferences(moduleContainer);
			if (!this.ctx.BuiltinTypes.CheckDefinitions(moduleContainer))
			{
				return false;
			}
			if (!assemblyDefinitionDynamic.Create(AppDomain.CurrentDomain, AssemblyBuilderAccess.Save))
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
				new DocumentationBuilder(moduleContainer).OutputDocComment(text, settings.DocumentationFile);
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
			assemblyDefinitionDynamic.Save();
			timeReporter.StopTotal();
			timeReporter.ShowStats();
			return this.Report.Errors == 0;
		}

		// Token: 0x04000939 RID: 2361
		private readonly CompilerContext ctx;
	}
}
