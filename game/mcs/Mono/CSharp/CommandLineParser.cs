using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace Mono.CSharp
{
	// Token: 0x02000296 RID: 662
	public class CommandLineParser
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06001FD9 RID: 8153 RVA: 0x0009C104 File Offset: 0x0009A304
		// (remove) Token: 0x06001FDA RID: 8154 RVA: 0x0009C13C File Offset: 0x0009A33C
		public event Func<string[], int, int> UnknownOptionHandler
		{
			[CompilerGenerated]
			add
			{
				Func<string[], int, int> func = this.UnknownOptionHandler;
				Func<string[], int, int> func2;
				do
				{
					func2 = func;
					Func<string[], int, int> value2 = (Func<string[], int, int>)Delegate.Combine(func2, value);
					func = Interlocked.CompareExchange<Func<string[], int, int>>(ref this.UnknownOptionHandler, value2, func2);
				}
				while (func != func2);
			}
			[CompilerGenerated]
			remove
			{
				Func<string[], int, int> func = this.UnknownOptionHandler;
				Func<string[], int, int> func2;
				do
				{
					func2 = func;
					Func<string[], int, int> value2 = (Func<string[], int, int>)Delegate.Remove(func2, value);
					func = Interlocked.CompareExchange<Func<string[], int, int>>(ref this.UnknownOptionHandler, value2, func2);
				}
				while (func != func2);
			}
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x0009C171 File Offset: 0x0009A371
		public CommandLineParser(TextWriter errorOutput) : this(errorOutput, Console.Out)
		{
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x0009C180 File Offset: 0x0009A380
		public CommandLineParser(TextWriter errorOutput, TextWriter messagesOutput)
		{
			StreamReportPrinter streamReportPrinter = new StreamReportPrinter(errorOutput);
			this.parser_settings = new CompilerSettings();
			this.report = new Report(new CompilerContext(this.parser_settings, streamReportPrinter), streamReportPrinter);
			this.output = messagesOutput;
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001FDD RID: 8157 RVA: 0x0009C1C4 File Offset: 0x0009A3C4
		public bool HasBeenStopped
		{
			get
			{
				return this.stop_argument;
			}
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x0009C1CC File Offset: 0x0009A3CC
		private void About()
		{
			this.output.WriteLine("The Mono C# compiler is Copyright 2001-2011, Novell, Inc.\n\nThe compiler source code is released under the terms of the \nMIT X11 or GNU GPL licenses\n\nFor more information on Mono, visit the project Web site\n   http://www.mono-project.com\n\nThe compiler was written by Miguel de Icaza, Ravi Pratap, Martin Baulig, Marek Safar, Raja R Harinath, Atushi Enomoto");
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x0009C1E0 File Offset: 0x0009A3E0
		public CompilerSettings ParseArguments(string[] args)
		{
			CompilerSettings compilerSettings = new CompilerSettings();
			if (!this.ParseArguments(compilerSettings, args))
			{
				return null;
			}
			return compilerSettings;
		}

		// Token: 0x06001FE0 RID: 8160 RVA: 0x0009C200 File Offset: 0x0009A400
		public bool ParseArguments(CompilerSettings settings, string[] args)
		{
			if (settings == null)
			{
				throw new ArgumentNullException("settings");
			}
			List<string> list = null;
			bool flag = true;
			this.stop_argument = false;
			this.source_file_index = new Dictionary<string, int>();
			for (int i = 0; i < args.Length; i++)
			{
				string text = args[i];
				if (text.Length != 0)
				{
					if (text[0] == '@')
					{
						string text2 = text.Substring(1);
						if (list == null)
						{
							list = new List<string>();
						}
						if (list.Contains(text2))
						{
							this.report.Error(1515, "Response file `{0}' specified multiple times", text2);
							return false;
						}
						list.Add(text2);
						string[] array = CommandLineParser.LoadArgs(text2);
						if (array == null)
						{
							this.report.Error(2011, "Unable to open response file: " + text2);
							return false;
						}
						args = CommandLineParser.AddArgs(args, array);
					}
					else
					{
						if (flag)
						{
							if (text == "--")
							{
								flag = false;
								goto IL_1EC;
							}
							bool flag2 = text[0] == '-';
							bool flag3 = text[0] == '/';
							if (flag2)
							{
								switch (this.ParseOptionUnix(text, ref args, ref i, settings))
								{
								case CommandLineParser.ParseResult.Success:
								case CommandLineParser.ParseResult.Error:
									goto IL_1EC;
								case CommandLineParser.ParseResult.Stop:
									this.stop_argument = true;
									return true;
								case CommandLineParser.ParseResult.UnknownOption:
									if (this.UnknownOptionHandler != null)
									{
										int num = this.UnknownOptionHandler(args, i);
										if (num != -1)
										{
											i = num;
											goto IL_1EC;
										}
									}
									break;
								}
							}
							if (flag2 || flag3)
							{
								string option = flag2 ? ("/" + text.Substring(1)) : text;
								switch (this.ParseOption(option, ref args, settings))
								{
								case CommandLineParser.ParseResult.Success:
								case CommandLineParser.ParseResult.Error:
									goto IL_1EC;
								case CommandLineParser.ParseResult.Stop:
									this.stop_argument = true;
									return true;
								case CommandLineParser.ParseResult.UnknownOption:
									if (!flag3 || text.Length <= 3 || text.IndexOf('/', 2) <= 0)
									{
										if (this.UnknownOptionHandler != null)
										{
											int num2 = this.UnknownOptionHandler(args, i);
											if (num2 != -1)
											{
												i = num2;
												goto IL_1EC;
											}
										}
										this.Error_WrongOption(text);
										return false;
									}
									break;
								}
							}
						}
						this.ProcessSourceFiles(text, false, settings.SourceFiles);
					}
				}
				IL_1EC:;
			}
			return this.report.Errors == 0;
		}

		// Token: 0x06001FE1 RID: 8161 RVA: 0x0009C414 File Offset: 0x0009A614
		private void ProcessSourceFiles(string spec, bool recurse, List<SourceFile> sourceFiles)
		{
			string path;
			string text;
			CommandLineParser.SplitPathAndPattern(spec, out path, out text);
			if (text.IndexOf('*') == -1)
			{
				this.AddSourceFile(spec, sourceFiles);
				return;
			}
			string[] files;
			try
			{
				files = Directory.GetFiles(path, text);
			}
			catch (DirectoryNotFoundException)
			{
				this.report.Error(2001, "Source file `" + spec + "' could not be found");
				return;
			}
			catch (IOException)
			{
				this.report.Error(2001, "Source file `" + spec + "' could not be found");
				return;
			}
			foreach (string fileName in files)
			{
				this.AddSourceFile(fileName, sourceFiles);
			}
			if (!recurse)
			{
				return;
			}
			string[] array2 = null;
			try
			{
				array2 = Directory.GetDirectories(path);
			}
			catch
			{
			}
			foreach (string str in array2)
			{
				this.ProcessSourceFiles(str + "/" + text, true, sourceFiles);
			}
		}

		// Token: 0x06001FE2 RID: 8162 RVA: 0x0009C528 File Offset: 0x0009A728
		private static string[] AddArgs(string[] args, string[] extra_args)
		{
			string[] array = new string[extra_args.Length + args.Length];
			int num = Array.IndexOf<string>(args, "--");
			if (num != -1)
			{
				Array.Copy(args, array, num);
				extra_args.CopyTo(array, num);
				Array.Copy(args, num, array, num + extra_args.Length, args.Length - num);
			}
			else
			{
				args.CopyTo(array, 0);
				extra_args.CopyTo(array, args.Length);
			}
			return array;
		}

		// Token: 0x06001FE3 RID: 8163 RVA: 0x0009C58C File Offset: 0x0009A78C
		private void AddAssemblyReference(string alias, string assembly, CompilerSettings settings)
		{
			if (assembly.Length == 0)
			{
				this.report.Error(1680, "Invalid reference alias `{0}='. Missing filename", alias);
				return;
			}
			if (!CommandLineParser.IsExternAliasValid(alias))
			{
				this.report.Error(1679, "Invalid extern alias for -reference. Alias `{0}' is not a valid identifier", alias);
				return;
			}
			settings.AssemblyReferencesAliases.Add(Tuple.Create<string, string>(alias, assembly));
		}

		// Token: 0x06001FE4 RID: 8164 RVA: 0x0009C5EC File Offset: 0x0009A7EC
		private void AddResource(AssemblyResource res, CompilerSettings settings)
		{
			if (settings.Resources == null)
			{
				settings.Resources = new List<AssemblyResource>();
				settings.Resources.Add(res);
				return;
			}
			if (settings.Resources.Contains(res))
			{
				this.report.Error(1508, "The resource identifier `{0}' has already been used in this assembly", res.Name);
				return;
			}
			settings.Resources.Add(res);
		}

		// Token: 0x06001FE5 RID: 8165 RVA: 0x0009C650 File Offset: 0x0009A850
		private void AddSourceFile(string fileName, List<SourceFile> sourceFiles)
		{
			string fullPath = Path.GetFullPath(fileName);
			int num;
			if (!this.source_file_index.TryGetValue(fullPath, out num))
			{
				SourceFile sourceFile = new SourceFile(fileName, fullPath, sourceFiles.Count + 1, null);
				sourceFiles.Add(sourceFile);
				this.source_file_index.Add(fullPath, sourceFile.Index);
				return;
			}
			string name = sourceFiles[num - 1].Name;
			if (fileName.Equals(name))
			{
				this.report.Warning(2002, 1, "Source file `{0}' specified multiple times", name);
				return;
			}
			this.report.Warning(2002, 1, "Source filenames `{0}' and `{1}' both refer to the same file: {2}", new string[]
			{
				fileName,
				name,
				fullPath
			});
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x0009C6F8 File Offset: 0x0009A8F8
		public bool ProcessWarningsList(string text, Action<int> action)
		{
			bool result = true;
			foreach (string text2 in text.Split(CommandLineParser.numeric_value_separator, StringSplitOptions.RemoveEmptyEntries))
			{
				int num;
				if (!int.TryParse(text2, NumberStyles.AllowLeadingWhite, CultureInfo.InvariantCulture, out num))
				{
					this.report.Error(1904, "`{0}' is not a valid warning number", text2);
					result = false;
				}
				else if (this.report.CheckWarningCode(num, Location.Null))
				{
					action(num);
				}
			}
			return result;
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x0009C76D File Offset: 0x0009A96D
		private void Error_RequiresArgument(string option)
		{
			this.report.Error(2006, "Missing argument for `{0}' option", option);
		}

		// Token: 0x06001FE8 RID: 8168 RVA: 0x0009C785 File Offset: 0x0009A985
		private void Error_RequiresFileName(string option)
		{
			this.report.Error(2005, "Missing file specification for `{0}' option", option);
		}

		// Token: 0x06001FE9 RID: 8169 RVA: 0x0009C79D File Offset: 0x0009A99D
		private void Error_WrongOption(string option)
		{
			this.report.Error(2007, "Unrecognized command-line option: `{0}'", option);
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x0009C7B5 File Offset: 0x0009A9B5
		private static bool IsExternAliasValid(string identifier)
		{
			return Tokenizer.IsValidIdentifier(identifier);
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x0009C7C0 File Offset: 0x0009A9C0
		private static string[] LoadArgs(string file)
		{
			List<string> list = new List<string>();
			StreamReader streamReader;
			try
			{
				streamReader = new StreamReader(file);
			}
			catch
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text;
			while ((text = streamReader.ReadLine()) != null)
			{
				int length = text.Length;
				for (int i = 0; i < length; i++)
				{
					char c = text[i];
					if (c == '"' || c == '\'')
					{
						char c2 = c;
						for (i++; i < length; i++)
						{
							c = text[i];
							if (c == c2)
							{
								break;
							}
							stringBuilder.Append(c);
						}
					}
					else if (c == ' ')
					{
						if (stringBuilder.Length > 0)
						{
							list.Add(stringBuilder.ToString());
							stringBuilder.Length = 0;
						}
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
				if (stringBuilder.Length > 0)
				{
					list.Add(stringBuilder.ToString());
					stringBuilder.Length = 0;
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x0009C8C4 File Offset: 0x0009AAC4
		private void OtherFlags()
		{
			this.output.WriteLine("Other flags in the compiler\n   --fatal[=COUNT]    Makes error after COUNT fatal\n   --lint             Enhanced warnings\n   --metadata-only    Produced assembly will contain metadata only\n   --parse            Only parses the source file\n   --runtime:VERSION  Sets mscorlib.dll metadata version: v1, v2, v4\n   --stacktrace       Shows stack trace at error location\n   --timestamp        Displays time stamps of various compiler events\n   -v                 Verbose parsing (for debugging the parser)\n   --mcs-debug X      Sets MCS debugging level to X\n   --break-on-ice     Breaks compilation on public compiler error");
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x0009C8D8 File Offset: 0x0009AAD8
		private CommandLineParser.ParseResult ParseOption(string option, ref string[] args, CompilerSettings settings)
		{
			int num = option.IndexOf(':');
			string text;
			string text2;
			if (num == -1)
			{
				text = option;
				text2 = "";
			}
			else
			{
				text = option.Substring(0, num);
				text2 = option.Substring(num + 1);
			}
			string text3 = text.ToLowerInvariant();
			uint num2 = <PrivateImplementationDetails>.ComputeStringHash(text3);
			if (num2 > 1691485513U)
			{
				if (num2 <= 2560176792U)
				{
					if (num2 <= 2150995998U)
					{
						if (num2 <= 2066209393U)
						{
							if (num2 <= 1829198682U)
							{
								if (num2 != 1808928846U)
								{
									if (num2 != 1829198682U)
									{
										return CommandLineParser.ParseResult.UnknownOption;
									}
									if (!(text3 == "runtimemetadataversion"))
									{
										return CommandLineParser.ParseResult.UnknownOption;
									}
									if (text2.Length == 0)
									{
										this.Error_RequiresArgument(option);
										return CommandLineParser.ParseResult.Error;
									}
									settings.RuntimeMetadataVersion = text2;
									return CommandLineParser.ParseResult.Success;
								}
								else
								{
									if (!(text3 == "/d"))
									{
										return CommandLineParser.ParseResult.UnknownOption;
									}
									goto IL_BA0;
								}
							}
							else if (num2 != 2048596402U)
							{
								if (num2 != 2066209393U)
								{
									return CommandLineParser.ParseResult.UnknownOption;
								}
								if (!(text3 == "/clscheck-"))
								{
									return CommandLineParser.ParseResult.UnknownOption;
								}
								settings.VerifyClsCompliance = false;
								return CommandLineParser.ParseResult.Success;
							}
							else if (!(text3 == "/errorreport"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
						}
						else if (num2 <= 2099764631U)
						{
							if (num2 != 2071489246U)
							{
								if (num2 != 2099764631U)
								{
									return CommandLineParser.ParseResult.UnknownOption;
								}
								if (!(text3 == "/clscheck+"))
								{
									return CommandLineParser.ParseResult.UnknownOption;
								}
								goto IL_FFB;
							}
							else
							{
								if (!(text3 == "/langversion"))
								{
									return CommandLineParser.ParseResult.UnknownOption;
								}
								if (text2.Length == 0)
								{
									this.Error_RequiresArgument(option);
									return CommandLineParser.ParseResult.Error;
								}
								text3 = text2.ToLowerInvariant();
								num2 = <PrivateImplementationDetails>.ComputeStringHash(text3);
								if (num2 <= 856466825U)
								{
									if (num2 <= 448036273U)
									{
										if (num2 != 397703416U)
										{
											if (num2 != 448036273U)
											{
												goto IL_1431;
											}
											if (!(text3 == "iso-2"))
											{
												goto IL_1431;
											}
											goto IL_13E2;
										}
										else if (!(text3 == "iso-1"))
										{
											goto IL_1431;
										}
									}
									else if (num2 != 806133968U)
									{
										if (num2 != 822911587U)
										{
											if (num2 != 856466825U)
											{
												goto IL_1431;
											}
											if (!(text3 == "6"))
											{
												goto IL_1431;
											}
											goto IL_1406;
										}
										else
										{
											if (!(text3 == "4"))
											{
												goto IL_1431;
											}
											settings.Version = LanguageVersion.V_4;
											return CommandLineParser.ParseResult.Success;
										}
									}
									else
									{
										if (!(text3 == "5"))
										{
											goto IL_1431;
										}
										settings.Version = LanguageVersion.V_5;
										return CommandLineParser.ParseResult.Success;
									}
								}
								else if (num2 <= 923577301U)
								{
									if (num2 != 873244444U)
									{
										if (num2 != 906799682U)
										{
											if (num2 != 923577301U)
											{
												goto IL_1431;
											}
											if (!(text3 == "2"))
											{
												goto IL_1431;
											}
											goto IL_13E2;
										}
										else
										{
											if (!(text3 == "3"))
											{
												goto IL_1431;
											}
											settings.Version = LanguageVersion.V_3;
											return CommandLineParser.ParseResult.Success;
										}
									}
									else if (!(text3 == "1"))
									{
										goto IL_1431;
									}
								}
								else if (num2 != 1431713430U)
								{
									if (num2 != 2470140894U)
									{
										if (num2 != 3394570047U)
										{
											goto IL_1431;
										}
										if (!(text3 == "experimental"))
										{
											goto IL_1431;
										}
										settings.Version = LanguageVersion.Experimental;
										return CommandLineParser.ParseResult.Success;
									}
									else
									{
										if (!(text3 == "default"))
										{
											goto IL_1431;
										}
										settings.Version = LanguageVersion.V_6;
										return CommandLineParser.ParseResult.Success;
									}
								}
								else
								{
									if (!(text3 == "future"))
									{
										goto IL_1431;
									}
									this.report.Warning(8000, 1, "Language version `future' is no longer supported");
									goto IL_1406;
								}
								settings.Version = LanguageVersion.ISO_1;
								return CommandLineParser.ParseResult.Success;
								IL_13E2:
								settings.Version = LanguageVersion.ISO_2;
								return CommandLineParser.ParseResult.Success;
								IL_1406:
								settings.Version = LanguageVersion.V_6;
								return CommandLineParser.ParseResult.Success;
								IL_1431:
								this.report.Error(1617, "Invalid -langversion option `{0}'. It must be `ISO-1', `ISO-2', Default or value in range 1 to 6", text2);
								return CommandLineParser.ParseResult.Error;
							}
						}
						else if (num2 != 2110946990U)
						{
							if (num2 != 2150995998U)
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							if (!(text3 == "/warn"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							goto IL_107F;
						}
						else
						{
							if (!(text3 == "/bugreport"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							this.output.WriteLine("To file bug reports, please visit: http://www.mono-project.com/Bugs");
							return CommandLineParser.ParseResult.Success;
						}
					}
					else if (num2 <= 2416398997U)
					{
						if (num2 <= 2327475901U)
						{
							if (num2 != 2315733283U)
							{
								if (num2 != 2327475901U)
								{
									return CommandLineParser.ParseResult.UnknownOption;
								}
								if (!(text3 == "/lib"))
								{
									return CommandLineParser.ParseResult.UnknownOption;
								}
								if (text2.Length == 0)
								{
									return CommandLineParser.ParseResult.Error;
								}
								foreach (string item in text2.Split(CommandLineParser.argument_value_separator))
								{
									settings.ReferencesLookupPaths.Add(item);
								}
								return CommandLineParser.ParseResult.Success;
							}
							else
							{
								if (!(text3 == "/warnaserror-"))
								{
									return CommandLineParser.ParseResult.UnknownOption;
								}
								if (text2.Length == 0)
								{
									settings.WarningsAreErrors = false;
								}
								else if (!this.ProcessWarningsList(text2, new Action<int>(settings.AddWarningOnly)))
								{
									return CommandLineParser.ParseResult.Error;
								}
								return CommandLineParser.ParseResult.Success;
							}
						}
						else if (num2 != 2385105393U)
						{
							if (num2 != 2416398997U)
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							if (!(text3 == "/warnaserror+"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							goto IL_101F;
						}
						else
						{
							if (!(text3 == "/delaysign+"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							goto IL_122B;
						}
					}
					else if (num2 <= 2438109477U)
					{
						if (num2 != 2418660631U)
						{
							if (num2 != 2438109477U)
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							if (!(text3 == "/filealign"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
						}
						else
						{
							if (!(text3 == "/delaysign-"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							settings.StrongNameDelaySign = false;
							return CommandLineParser.ParseResult.Success;
						}
					}
					else if (num2 != 2463255987U)
					{
						if (num2 != 2516616198U)
						{
							if (num2 != 2560176792U)
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							if (!(text3 == "/o+"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							goto IL_B8C;
						}
						else
						{
							if (!(text3 == "/resource"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							goto IL_C9D;
						}
					}
					else
					{
						if (!(text3 == "/?"))
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						goto IL_11BA;
					}
					if (text2.Length == 0)
					{
						this.Error_RequiresArgument(option);
						return CommandLineParser.ParseResult.Error;
					}
					return CommandLineParser.ParseResult.Success;
				}
				else if (num2 <= 3534135543U)
				{
					if (num2 <= 2791501133U)
					{
						if (num2 <= 2615368093U)
						{
							if (num2 != 2595342849U)
							{
								if (num2 != 2615368093U)
								{
									return CommandLineParser.ParseResult.UnknownOption;
								}
								if (!(text3 == "/win32res"))
								{
									return CommandLineParser.ParseResult.UnknownOption;
								}
								if (text2.Length == 0)
								{
									this.Error_RequiresFileName(option);
									return CommandLineParser.ParseResult.Error;
								}
								if (settings.Win32IconFile != null)
								{
									this.report.Error(1565, "Cannot specify the `win32res' and the `win32ico' compiler option at the same time");
								}
								settings.Win32ResourceFile = text2;
								return CommandLineParser.ParseResult.Success;
							}
							else
							{
								if (!(text3 == "/help"))
								{
									return CommandLineParser.ParseResult.UnknownOption;
								}
								goto IL_11BA;
							}
						}
						else if (num2 != 2660842506U)
						{
							if (num2 != 2791501133U)
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							if (!(text3 == "/optimize"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							goto IL_B8C;
						}
						else
						{
							if (!(text3 == "/o-"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							goto IL_B95;
						}
					}
					else if (num2 <= 3404401326U)
					{
						if (num2 != 2835564102U)
						{
							if (num2 != 3404401326U)
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							if (!(text3 == "/linkresource"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							goto IL_C9D;
						}
						else if (!(text3 == "/clscheck"))
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
					}
					else if (num2 != 3427592952U)
					{
						if (num2 != 3500580305U)
						{
							if (num2 != 3534135543U)
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							if (!(text3 == "/unsafe-"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							settings.Unsafe = false;
							return CommandLineParser.ParseResult.Success;
						}
						else
						{
							if (!(text3 == "/unsafe+"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							goto IL_100D;
						}
					}
					else
					{
						if (!(text3 == "/keycontainer"))
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						if (text2.Length == 0)
						{
							this.Error_RequiresArgument(option);
							return CommandLineParser.ParseResult.Error;
						}
						settings.StrongNameKeyContainer = text2;
						return CommandLineParser.ParseResult.Success;
					}
				}
				else if (num2 <= 3687146380U)
				{
					if (num2 <= 3649069995U)
					{
						if (num2 != 3537398322U)
						{
							if (num2 != 3649069995U)
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							if (!(text3 == "/recurse"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							if (text2.Length == 0)
							{
								this.Error_RequiresFileName(option);
								return CommandLineParser.ParseResult.Error;
							}
							this.ProcessSourceFiles(text2, true, settings.SourceFiles);
							return CommandLineParser.ParseResult.Success;
						}
						else
						{
							if (!(text3 == "/nologo"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							return CommandLineParser.ParseResult.Success;
						}
					}
					else if (num2 != 3653591142U)
					{
						if (num2 != 3687146380U)
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						if (!(text3 == "/checked+"))
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						goto IL_FE9;
					}
					else
					{
						if (!(text3 == "/checked-"))
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						settings.Checked = false;
						return CommandLineParser.ParseResult.Success;
					}
				}
				else if (num2 <= 3732121289U)
				{
					if (num2 != 3706853837U)
					{
						if (num2 != 3732121289U)
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						if (!(text3 == "/debug"))
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						if (text2.Equals("full", StringComparison.OrdinalIgnoreCase) || text2.Equals("pdbonly", StringComparison.OrdinalIgnoreCase) || num < 0)
						{
							settings.GenerateDebugInfo = true;
							return CommandLineParser.ParseResult.Success;
						}
						if (text2.Length > 0)
						{
							this.report.Error(1902, "Invalid debug option `{0}'. Valid options are `full' or `pdbonly'", text2);
						}
						else
						{
							this.Error_RequiresArgument(option);
						}
						return CommandLineParser.ParseResult.Error;
					}
					else
					{
						if (!(text3 == "/platform"))
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						if (text2.Length == 0)
						{
							this.Error_RequiresArgument(option);
							return CommandLineParser.ParseResult.Error;
						}
						text3 = text2.ToLowerInvariant();
						if (!(text3 == "arm"))
						{
							if (!(text3 == "anycpu"))
							{
								if (!(text3 == "x86"))
								{
									if (!(text3 == "x64"))
									{
										if (!(text3 == "itanium"))
										{
											if (!(text3 == "anycpu32bitpreferred"))
											{
												this.report.Error(1672, "Invalid -platform option `{0}'. Valid options are `anycpu', `anycpu32bitpreferred', `arm', `x86', `x64' or `itanium'", text2);
												return CommandLineParser.ParseResult.Error;
											}
											settings.Platform = Platform.AnyCPU32Preferred;
										}
										else
										{
											settings.Platform = Platform.IA64;
										}
									}
									else
									{
										settings.Platform = Platform.X64;
									}
								}
								else
								{
									settings.Platform = Platform.X86;
								}
							}
							else
							{
								settings.Platform = Platform.AnyCPU;
							}
						}
						else
						{
							settings.Platform = Platform.Arm;
						}
						return CommandLineParser.ParseResult.Success;
					}
				}
				else if (num2 != 3770013724U)
				{
					if (num2 != 4086602880U)
					{
						if (num2 != 4162436016U)
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						if (!(text3 == "/doc"))
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						if (text2.Length == 0)
						{
							this.Error_RequiresFileName(option);
							return CommandLineParser.ParseResult.Error;
						}
						settings.DocumentationFile = text2;
						return CommandLineParser.ParseResult.Success;
					}
					else
					{
						if (!(text3 == "/res"))
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						goto IL_C9D;
					}
				}
				else
				{
					if (!(text3 == "/pkg"))
					{
						return CommandLineParser.ParseResult.UnknownOption;
					}
					if (text2.Length == 0)
					{
						this.Error_RequiresArgument(option);
						return CommandLineParser.ParseResult.Error;
					}
					string packageFlags = Driver.GetPackageFlags(string.Join(" ", text2.Split(new char[]
					{
						';',
						',',
						'\n',
						'\r'
					})), this.report);
					if (packageFlags == null)
					{
						return CommandLineParser.ParseResult.Error;
					}
					string[] extra_args = packageFlags.Trim(new char[]
					{
						' ',
						'\n',
						'\r',
						'\t'
					}).Split(new char[]
					{
						' ',
						'\t'
					});
					args = CommandLineParser.AddArgs(args, extra_args);
					return CommandLineParser.ParseResult.Success;
				}
				IL_FFB:
				settings.VerifyClsCompliance = true;
				return CommandLineParser.ParseResult.Success;
				IL_11BA:
				this.Usage();
				return CommandLineParser.ParseResult.Stop;
			}
			if (num2 <= 1004903793U)
			{
				if (num2 <= 336574700U)
				{
					if (num2 <= 120132000U)
					{
						if (num2 <= 110321603U)
						{
							if (num2 != 49422528U)
							{
								if (num2 != 110321603U)
								{
									return CommandLineParser.ParseResult.UnknownOption;
								}
								if (!(text3 == "/keyfile"))
								{
									return CommandLineParser.ParseResult.UnknownOption;
								}
								if (text2.Length == 0)
								{
									this.Error_RequiresFileName(option);
									return CommandLineParser.ParseResult.Error;
								}
								settings.StrongNameKeyFile = text2;
								return CommandLineParser.ParseResult.Success;
							}
							else
							{
								if (!(text3 == "/delaysign"))
								{
									return CommandLineParser.ParseResult.UnknownOption;
								}
								goto IL_122B;
							}
						}
						else if (num2 != 115672644U)
						{
							if (num2 != 120132000U)
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							if (!(text3 == "/unsafe"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							goto IL_100D;
						}
						else
						{
							if (!(text3 == "/helpinternal"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							this.OtherFlags();
							return CommandLineParser.ParseResult.Stop;
						}
					}
					else if (num2 <= 242014249U)
					{
						if (num2 != 162205771U)
						{
							if (num2 != 242014249U)
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							if (!(text3 == "/nowarn"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							if (text2.Length == 0)
							{
								this.Error_RequiresArgument(option);
								return CommandLineParser.ParseResult.Error;
							}
							if (!this.ProcessWarningsList(text2, new Action<int>(settings.SetIgnoreWarning)))
							{
								return CommandLineParser.ParseResult.Error;
							}
							return CommandLineParser.ParseResult.Success;
						}
						else
						{
							if (!(text3 == "/reference"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							goto IL_DC8;
						}
					}
					else if (num2 != 303019462U)
					{
						if (num2 != 336574700U)
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						if (!(text3 == "/debug-"))
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						settings.GenerateDebugInfo = false;
						return CommandLineParser.ParseResult.Success;
					}
					else
					{
						if (!(text3 == "/debug+"))
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						settings.GenerateDebugInfo = true;
						return CommandLineParser.ParseResult.Success;
					}
				}
				else if (num2 <= 746084189U)
				{
					if (num2 <= 608466415U)
					{
						if (num2 != 434862498U)
						{
							if (num2 != 608466415U)
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							if (!(text3 == "/checked"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							goto IL_FE9;
						}
						else
						{
							if (!(text3 == "/out"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							if (text2.Length == 0)
							{
								this.Error_RequiresFileName(option);
								return CommandLineParser.ParseResult.Error;
							}
							settings.OutputFile = text2;
							return CommandLineParser.ParseResult.Success;
						}
					}
					else if (num2 != 645418475U)
					{
						if (num2 != 746084189U)
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						if (!(text3 == "/incremental-"))
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
					}
					else if (!(text3 == "/incremental+"))
					{
						return CommandLineParser.ParseResult.UnknownOption;
					}
				}
				else if (num2 <= 891772916U)
				{
					if (num2 != 831470725U)
					{
						if (num2 != 891772916U)
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						if (!(text3 == "/win32icon"))
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						if (text2.Length == 0)
						{
							this.Error_RequiresFileName(option);
							return CommandLineParser.ParseResult.Error;
						}
						if (settings.Win32ResourceFile != null)
						{
							this.report.Error(1565, "Cannot specify the `win32res' and the `win32ico' compiler option at the same time");
						}
						settings.Win32IconFile = text2;
						return CommandLineParser.ParseResult.Success;
					}
					else
					{
						if (!(text3 == "/addmodule"))
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						if (text2.Length == 0)
						{
							this.Error_RequiresFileName(option);
							return CommandLineParser.ParseResult.Error;
						}
						foreach (string item2 in text2.Split(CommandLineParser.argument_value_separator))
						{
							settings.Modules.Add(item2);
						}
						return CommandLineParser.ParseResult.Success;
					}
				}
				else if (num2 != 917072863U)
				{
					if (num2 != 939889960U)
					{
						if (num2 != 1004903793U)
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						if (!(text3 == "/define"))
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						goto IL_BA0;
					}
					else
					{
						if (!(text3 == "/linkres"))
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						goto IL_C9D;
					}
				}
				else
				{
					if (!(text3 == "/noconfig"))
					{
						return CommandLineParser.ParseResult.UnknownOption;
					}
					settings.LoadDefaultReferences = false;
					return CommandLineParser.ParseResult.Success;
				}
			}
			else
			{
				if (num2 <= 1483147510U)
				{
					if (num2 <= 1378522140U)
					{
						if (num2 <= 1219216601U)
						{
							if (num2 != 1097477080U)
							{
								if (num2 != 1219216601U)
								{
									return CommandLineParser.ParseResult.UnknownOption;
								}
								if (!(text3 == "/nostdlib"))
								{
									return CommandLineParser.ParseResult.UnknownOption;
								}
							}
							else
							{
								if (!(text3 == "/codepage"))
								{
									return CommandLineParser.ParseResult.UnknownOption;
								}
								if (text2.Length == 0)
								{
									this.Error_RequiresArgument(option);
									return CommandLineParser.ParseResult.Error;
								}
								if (!(text2 == "utf8"))
								{
									if (!(text2 == "reset"))
									{
										try
										{
											settings.Encoding = Encoding.GetEncoding(int.Parse(text2));
										}
										catch
										{
											this.report.Error(2016, "Code page `{0}' is invalid or not installed", text2);
										}
										return CommandLineParser.ParseResult.Error;
									}
									settings.Encoding = Encoding.Default;
								}
								else
								{
									settings.Encoding = Encoding.UTF8;
								}
								return CommandLineParser.ParseResult.Success;
							}
						}
						else if (num2 != 1304145440U)
						{
							if (num2 != 1378522140U)
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							if (!(text3 == "/warnaserror"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							goto IL_101F;
						}
						else
						{
							if (!(text3 == "/optimize-"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							goto IL_B95;
						}
					}
					else if (num2 <= 1410537431U)
					{
						if (num2 != 1404811154U)
						{
							if (num2 != 1410537431U)
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							if (!(text3 == "/main"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							goto IL_11C2;
						}
						else
						{
							if (!(text3 == "/optimize+"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							goto IL_B8C;
						}
					}
					else if (num2 != 1447939663U)
					{
						if (num2 != 1483147510U)
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						if (!(text3 == "/nostdlib+"))
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
					}
					else
					{
						if (!(text3 == "/target"))
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						goto IL_AE9;
					}
					settings.StdLib = false;
					return CommandLineParser.ParseResult.Success;
				}
				if (num2 <= 1523709323U)
				{
					if (num2 <= 1501474425U)
					{
						if (num2 != 1484344582U)
						{
							if (num2 != 1501474425U)
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							if (!(text3 == "/fullpaths"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							settings.ShowFullPaths = true;
							return CommandLineParser.ParseResult.Success;
						}
						else
						{
							if (!(text3 == "/sdk"))
							{
								return CommandLineParser.ParseResult.UnknownOption;
							}
							if (text2.Length == 0)
							{
								this.Error_RequiresArgument(option);
								return CommandLineParser.ParseResult.Error;
							}
							settings.SdkVersion = text2;
							return CommandLineParser.ParseResult.Success;
						}
					}
					else if (num2 != 1516702748U)
					{
						if (num2 != 1523709323U)
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						if (!(text3 == "/w"))
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						goto IL_107F;
					}
					else
					{
						if (!(text3 == "/nostdlib-"))
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						settings.StdLib = true;
						return CommandLineParser.ParseResult.Success;
					}
				}
				else if (num2 <= 1574042180U)
				{
					if (num2 != 1540486942U)
					{
						if (num2 != 1574042180U)
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						if (!(text3 == "/r"))
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						goto IL_DC8;
					}
					else if (!(text3 == "/t"))
					{
						return CommandLineParser.ParseResult.UnknownOption;
					}
				}
				else if (num2 != 1639815522U)
				{
					if (num2 != 1657930275U)
					{
						if (num2 != 1691485513U)
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						if (!(text3 == "/m"))
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						goto IL_11C2;
					}
					else
					{
						if (!(text3 == "/o"))
						{
							return CommandLineParser.ParseResult.UnknownOption;
						}
						goto IL_B8C;
					}
				}
				else
				{
					if (!(text3 == "/incremental"))
					{
						return CommandLineParser.ParseResult.UnknownOption;
					}
					return CommandLineParser.ParseResult.Success;
				}
				IL_AE9:
				if (!(text2 == "exe"))
				{
					if (!(text2 == "winexe"))
					{
						if (!(text2 == "library"))
						{
							if (!(text2 == "module"))
							{
								this.report.Error(2019, "Invalid target type for -target. Valid options are `exe', `winexe', `library' or `module'");
								return CommandLineParser.ParseResult.Error;
							}
							settings.Target = Target.Module;
							settings.TargetExt = ".netmodule";
						}
						else
						{
							settings.Target = Target.Library;
							settings.TargetExt = ".dll";
						}
					}
					else
					{
						settings.Target = Target.WinExe;
					}
				}
				else
				{
					settings.Target = Target.Exe;
				}
				return CommandLineParser.ParseResult.Success;
				IL_11C2:
				if (text2.Length == 0)
				{
					this.Error_RequiresArgument(option);
					return CommandLineParser.ParseResult.Error;
				}
				settings.MainClass = text2;
				return CommandLineParser.ParseResult.Success;
			}
			return CommandLineParser.ParseResult.Success;
			IL_DC8:
			if (text2.Length == 0)
			{
				this.Error_RequiresFileName(option);
				return CommandLineParser.ParseResult.Error;
			}
			string[] array2 = text2.Split(CommandLineParser.argument_value_separator);
			foreach (string text4 in array2)
			{
				if (text4.Length != 0)
				{
					string text5 = text4;
					int num3 = text5.IndexOf('=');
					if (num3 > -1)
					{
						string alias = text4.Substring(0, num3);
						string assembly = text4.Substring(num3 + 1);
						this.AddAssemblyReference(alias, assembly, settings);
						if (array2.Length != 1)
						{
							this.report.Error(2034, "Cannot specify multiple aliases using single /reference option");
							return CommandLineParser.ParseResult.Error;
						}
					}
					else
					{
						settings.AssemblyReferences.Add(text5);
					}
				}
			}
			return CommandLineParser.ParseResult.Success;
			IL_B8C:
			settings.Optimize = true;
			return CommandLineParser.ParseResult.Success;
			IL_B95:
			settings.Optimize = false;
			return CommandLineParser.ParseResult.Success;
			IL_BA0:
			if (text2.Length == 0)
			{
				this.Error_RequiresArgument(option);
				return CommandLineParser.ParseResult.Error;
			}
			string[] array = text2.Split(CommandLineParser.argument_value_separator);
			for (int i = 0; i < array.Length; i++)
			{
				string text6 = array[i].Trim();
				if (!Tokenizer.IsValidIdentifier(text6))
				{
					this.report.Warning(2029, 1, "Invalid conditional define symbol `{0}'", text6);
				}
				else
				{
					settings.AddConditionalSymbol(text6);
				}
			}
			return CommandLineParser.ParseResult.Success;
			IL_C9D:
			string[] array3 = text2.Split(CommandLineParser.argument_value_separator, StringSplitOptions.RemoveEmptyEntries);
			AssemblyResource assemblyResource;
			switch (array3.Length)
			{
			case 1:
				if (array3[0].Length != 0)
				{
					assemblyResource = new AssemblyResource(array3[0], Path.GetFileName(array3[0]));
					goto IL_D78;
				}
				break;
			case 2:
				assemblyResource = new AssemblyResource(array3[0], array3[1]);
				goto IL_D78;
			case 3:
				if (array3[2] != "public" && array3[2] != "private")
				{
					this.report.Error(1906, "Invalid resource visibility option `{0}'. Use either `public' or `private' instead", array3[2]);
					return CommandLineParser.ParseResult.Error;
				}
				assemblyResource = new AssemblyResource(array3[0], array3[1], array3[2] == "private");
				goto IL_D78;
			}
			this.report.Error(-2005, "Wrong number of arguments for option `{0}'", option);
			return CommandLineParser.ParseResult.Error;
			IL_D78:
			if (assemblyResource != null)
			{
				assemblyResource.IsEmbeded = (text[1] == 'r' || text[1] == 'R');
				this.AddResource(assemblyResource, settings);
			}
			return CommandLineParser.ParseResult.Success;
			IL_FE9:
			settings.Checked = true;
			return CommandLineParser.ParseResult.Success;
			IL_100D:
			settings.Unsafe = true;
			return CommandLineParser.ParseResult.Success;
			IL_101F:
			if (text2.Length == 0)
			{
				settings.WarningsAreErrors = true;
				this.parser_settings.WarningsAreErrors = true;
			}
			else if (!this.ProcessWarningsList(text2, new Action<int>(settings.AddWarningAsError)))
			{
				return CommandLineParser.ParseResult.Error;
			}
			return CommandLineParser.ParseResult.Success;
			IL_107F:
			if (text2.Length == 0)
			{
				this.Error_RequiresArgument(option);
				return CommandLineParser.ParseResult.Error;
			}
			this.SetWarningLevel(text2, settings);
			return CommandLineParser.ParseResult.Success;
			IL_122B:
			settings.StrongNameDelaySign = true;
			return CommandLineParser.ParseResult.Success;
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x0009DDD4 File Offset: 0x0009BFD4
		private CommandLineParser.ParseResult ParseOptionUnix(string arg, ref string[] args, ref int i, CompilerSettings settings)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(arg);
			if (num <= 1705727106U)
			{
				int num2;
				if (num <= 1252956226U)
				{
					if (num <= 635780926U)
					{
						if (num <= 281316830U)
						{
							if (num != 81769468U)
							{
								if (num != 281316830U)
								{
									goto IL_A42;
								}
								if (!(arg == "--wlevel"))
								{
									goto IL_A42;
								}
								this.report.Warning(-29, 1, "Compatibility: Use -warn:LEVEL instead of --wlevel LEVEL");
								if (i + 1 >= args.Length)
								{
									this.Error_RequiresArgument(arg);
									return CommandLineParser.ParseResult.Error;
								}
								string[] array = args;
								num2 = i + 1;
								i = num2;
								this.SetWarningLevel(array[num2], settings);
								return CommandLineParser.ParseResult.Success;
							}
							else
							{
								if (!(arg == "--define"))
								{
									goto IL_A42;
								}
								this.report.Warning(-29, 1, "Compatibility: Use -d:SYMBOL instead of --define SYMBOL");
								if (i + 1 >= args.Length)
								{
									this.Error_RequiresArgument(arg);
									return CommandLineParser.ParseResult.Error;
								}
								string[] array2 = args;
								num2 = i + 1;
								i = num2;
								settings.AddConditionalSymbol(array2[num2]);
								return CommandLineParser.ParseResult.Success;
							}
						}
						else if (num != 345909838U)
						{
							if (num != 635780926U)
							{
								goto IL_A42;
							}
							if (!(arg == "--main"))
							{
								goto IL_A42;
							}
						}
						else
						{
							if (!(arg == "--debug"))
							{
								goto IL_A42;
							}
							goto IL_9F4;
						}
					}
					else if (num <= 756358161U)
					{
						if (num != 670725043U)
						{
							if (num != 756358161U)
							{
								goto IL_A42;
							}
							if (!(arg == "--version"))
							{
								goto IL_A42;
							}
							this.Version();
							return CommandLineParser.ParseResult.Stop;
						}
						else
						{
							if (!(arg == "--linkres"))
							{
								goto IL_A42;
							}
							goto IL_68B;
						}
					}
					else if (num != 926784076U)
					{
						if (num != 1134218776U)
						{
							if (num != 1252956226U)
							{
								goto IL_A42;
							}
							if (!(arg == "--output"))
							{
								goto IL_A42;
							}
							goto IL_626;
						}
						else
						{
							if (!(arg == "--checked"))
							{
								goto IL_A42;
							}
							this.report.Warning(-29, 1, "Compatibility: Use -checked instead of --checked");
							settings.Checked = true;
							return CommandLineParser.ParseResult.Success;
						}
					}
					else
					{
						if (!(arg == "--nostdlib"))
						{
							goto IL_A42;
						}
						this.report.Warning(-29, 1, "Compatibility: Use -nostdlib instead of --nostdlib");
						settings.StdLib = false;
						return CommandLineParser.ParseResult.Success;
					}
				}
				else if (num <= 1506637514U)
				{
					if (num <= 1448228735U)
					{
						if (num != 1439527038U)
						{
							if (num != 1448228735U)
							{
								goto IL_A42;
							}
							if (!(arg == "--metadata-only"))
							{
								goto IL_A42;
							}
							settings.WriteMetadataOnly = true;
							return CommandLineParser.ParseResult.Success;
						}
						else
						{
							if (!(arg == "-r"))
							{
								goto IL_A42;
							}
							this.report.Warning(-29, 1, "Compatibility: Use -r:LIBRARY instead of -r library");
							if (i + 1 >= args.Length)
							{
								this.Error_RequiresArgument(arg);
								return CommandLineParser.ParseResult.Error;
							}
							string[] array3 = args;
							num2 = i + 1;
							i = num2;
							string text = array3[num2];
							int num3 = text.IndexOf('=');
							if (num3 > -1)
							{
								string alias = text.Substring(0, num3);
								string assembly = text.Substring(num3 + 1);
								this.AddAssemblyReference(alias, assembly, settings);
								return CommandLineParser.ParseResult.Success;
							}
							settings.AssemblyReferences.Add(text);
							return CommandLineParser.ParseResult.Success;
						}
					}
					else if (num != 1497114112U)
					{
						if (num != 1506637514U)
						{
							goto IL_A42;
						}
						if (!(arg == "-v"))
						{
							goto IL_A42;
						}
						settings.VerboseParserFlag++;
						return CommandLineParser.ParseResult.Success;
					}
					else
					{
						if (!(arg == "--nowarn"))
						{
							goto IL_A42;
						}
						this.report.Warning(-29, 1, "Compatibility: Use -nowarn instead of --nowarn");
						if (i + 1 >= args.Length)
						{
							this.Error_RequiresArgument(arg);
							return CommandLineParser.ParseResult.Error;
						}
						int ignoreWarning = 0;
						try
						{
							string[] array4 = args;
							num2 = i + 1;
							i = num2;
							ignoreWarning = int.Parse(array4[num2]);
						}
						catch
						{
							this.Usage();
							Environment.Exit(1);
						}
						settings.SetIgnoreWarning(ignoreWarning);
						return CommandLineParser.ParseResult.Success;
					}
				}
				else if (num <= 1624080847U)
				{
					if (num != 1590045295U)
					{
						if (num != 1624080847U)
						{
							goto IL_A42;
						}
						if (!(arg == "-m"))
						{
							goto IL_A42;
						}
					}
					else
					{
						if (!(arg == "--linkresource"))
						{
							goto IL_A42;
						}
						goto IL_68B;
					}
				}
				else if (num != 1657636085U)
				{
					if (num != 1658267218U)
					{
						if (num != 1705727106U)
						{
							goto IL_A42;
						}
						if (!(arg == "--mcs-debug"))
						{
							goto IL_A42;
						}
						if (i + 1 >= args.Length)
						{
							this.Error_RequiresArgument(arg);
							return CommandLineParser.ParseResult.Error;
						}
						try
						{
							string[] array5 = args;
							num2 = i + 1;
							i = num2;
							settings.DebugFlags = int.Parse(array5[num2]);
						}
						catch
						{
							this.Error_RequiresArgument(arg);
							return CommandLineParser.ParseResult.Error;
						}
						return CommandLineParser.ParseResult.Success;
					}
					else
					{
						if (!(arg == "--noconfig"))
						{
							goto IL_A42;
						}
						this.report.Warning(-29, 1, "Compatibility: Use -noconfig option instead of --noconfig");
						settings.LoadDefaultReferences = false;
						return CommandLineParser.ParseResult.Success;
					}
				}
				else
				{
					if (!(arg == "-o"))
					{
						goto IL_A42;
					}
					goto IL_626;
				}
				this.report.Warning(-29, 1, "Compatibility: Use -main:CLASS instead of --main CLASS or -m CLASS");
				if (i + 1 >= args.Length)
				{
					this.Error_RequiresArgument(arg);
					return CommandLineParser.ParseResult.Error;
				}
				string[] array6 = args;
				num2 = i + 1;
				i = num2;
				settings.MainClass = array6[num2];
				return CommandLineParser.ParseResult.Success;
				IL_626:
				this.report.Warning(-29, 1, "Compatibility: Use -out:FILE instead of --output FILE or -o FILE");
				if (i + 1 >= args.Length)
				{
					this.Error_RequiresArgument(arg);
					return CommandLineParser.ParseResult.Error;
				}
				string[] array7 = args;
				num2 = i + 1;
				i = num2;
				settings.OutputFile = array7[num2];
				return CommandLineParser.ParseResult.Success;
				IL_68B:
				this.report.Warning(-29, 1, "Compatibility: Use -linkres:VALUE instead of --linkres VALUE");
				if (i + 1 >= args.Length)
				{
					this.Error_RequiresArgument(arg);
					return CommandLineParser.ParseResult.Error;
				}
				string[] array8 = args;
				num2 = i + 1;
				i = num2;
				this.AddResource(new AssemblyResource(array8[num2], args[i]), settings);
				return CommandLineParser.ParseResult.Success;
			}
			else
			{
				int num2;
				if (num <= 2428880093U)
				{
					if (num <= 2142407772U)
					{
						if (num <= 1791857037U)
						{
							if (num != 1741818370U)
							{
								if (num != 1791857037U)
								{
									goto IL_A42;
								}
								if (!(arg == "-g"))
								{
									goto IL_A42;
								}
								goto IL_9F4;
							}
							else if (!(arg == "/h"))
							{
								goto IL_A42;
							}
						}
						else if (num != 1904858349U)
						{
							if (num != 2142407772U)
							{
								goto IL_A42;
							}
							if (!(arg == "--help"))
							{
								goto IL_A42;
							}
						}
						else
						{
							if (!(arg == "--unsafe"))
							{
								goto IL_A42;
							}
							this.report.Warning(-29, 1, "Compatibility: Use -unsafe instead of --unsafe");
							settings.Unsafe = true;
							return CommandLineParser.ParseResult.Success;
						}
					}
					else if (num <= 2179685138U)
					{
						if (num != 2144187036U)
						{
							if (num != 2179685138U)
							{
								goto IL_A42;
							}
							if (!(arg == "--target"))
							{
								goto IL_A42;
							}
							this.report.Warning(-29, 1, "Compatibility: Use -target:KIND instead of --target KIND");
							if (i + 1 >= args.Length)
							{
								this.Error_RequiresArgument(arg);
								return CommandLineParser.ParseResult.Error;
							}
							string[] array9 = args;
							num2 = i + 1;
							i = num2;
							string a = array9[num2];
							if (!(a == "library"))
							{
								if (!(a == "exe"))
								{
									if (!(a == "winexe"))
									{
										if (!(a == "module"))
										{
											this.report.Error(2019, "Invalid target type for -target. Valid options are `exe', `winexe', `library' or `module'");
										}
										else
										{
											settings.Target = Target.Module;
											settings.TargetExt = ".dll";
										}
									}
									else
									{
										settings.Target = Target.WinExe;
									}
								}
								else
								{
									settings.Target = Target.Exe;
								}
							}
							else
							{
								settings.Target = Target.Library;
								settings.TargetExt = ".dll";
							}
							return CommandLineParser.ParseResult.Success;
						}
						else
						{
							if (!(arg == "-L"))
							{
								goto IL_A42;
							}
							this.report.Warning(-29, 1, "Compatibility: Use -lib:ARG instead of --L arg");
							if (i + 1 >= args.Length)
							{
								this.Error_RequiresArgument(arg);
								return CommandLineParser.ParseResult.Error;
							}
							List<string> referencesLookupPaths = settings.ReferencesLookupPaths;
							string[] array10 = args;
							num2 = i + 1;
							i = num2;
							referencesLookupPaths.Add(array10[num2]);
							return CommandLineParser.ParseResult.Success;
						}
					}
					else if (num != 2232704418U)
					{
						if (num != 2336707915U)
						{
							if (num != 2428880093U)
							{
								goto IL_A42;
							}
							if (!(arg == "--timestamp"))
							{
								goto IL_A42;
							}
							settings.Timestamps = true;
							return CommandLineParser.ParseResult.Success;
						}
						else
						{
							if (!(arg == "--resource"))
							{
								goto IL_A42;
							}
							goto IL_6D4;
						}
					}
					else
					{
						if (!(arg == "--break-on-ice"))
						{
							goto IL_A42;
						}
						settings.BreakOnInternalError = true;
						return CommandLineParser.ParseResult.Success;
					}
				}
				else if (num <= 3137072490U)
				{
					if (num <= 2595342849U)
					{
						if (num != 2463255987U)
						{
							if (num != 2595342849U)
							{
								goto IL_A42;
							}
							if (!(arg == "/help"))
							{
								goto IL_A42;
							}
						}
						else if (!(arg == "/?"))
						{
							goto IL_A42;
						}
					}
					else if (num != 2846572434U)
					{
						if (num != 3137072490U)
						{
							goto IL_A42;
						}
						if (!(arg == "--lint"))
						{
							goto IL_A42;
						}
						settings.EnhancedWarnings = true;
						return CommandLineParser.ParseResult.Success;
					}
					else
					{
						if (!(arg == "--parse"))
						{
							goto IL_A42;
						}
						settings.ParseOnly = true;
						return CommandLineParser.ParseResult.Success;
					}
				}
				else if (num <= 3327484604U)
				{
					if (num != 3165194287U)
					{
						if (num != 3327484604U)
						{
							goto IL_A42;
						}
						if (!(arg == "--about"))
						{
							goto IL_A42;
						}
						this.About();
						return CommandLineParser.ParseResult.Stop;
					}
					else
					{
						if (!(arg == "--res"))
						{
							goto IL_A42;
						}
						goto IL_6D4;
					}
				}
				else if (num != 3351207728U)
				{
					if (num != 3940532374U)
					{
						if (num != 4182809490U)
						{
							goto IL_A42;
						}
						if (!(arg == "--stacktrace"))
						{
							goto IL_A42;
						}
						settings.Stacktrace = true;
						return CommandLineParser.ParseResult.Success;
					}
					else
					{
						if (!(arg == "--tokenize"))
						{
							goto IL_A42;
						}
						settings.TokenizeOnly = true;
						return CommandLineParser.ParseResult.Success;
					}
				}
				else
				{
					if (!(arg == "--recurse"))
					{
						goto IL_A42;
					}
					this.report.Warning(-29, 1, "Compatibility: Use -recurse:PATTERN option instead --recurse PATTERN");
					if (i + 1 >= args.Length)
					{
						this.Error_RequiresArgument(arg);
						return CommandLineParser.ParseResult.Error;
					}
					string[] array11 = args;
					num2 = i + 1;
					i = num2;
					this.ProcessSourceFiles(array11[num2], true, settings.SourceFiles);
					return CommandLineParser.ParseResult.Success;
				}
				this.Usage();
				return CommandLineParser.ParseResult.Stop;
				IL_6D4:
				this.report.Warning(-29, 1, "Compatibility: Use -res:VALUE instead of --res VALUE");
				if (i + 1 >= args.Length)
				{
					this.Error_RequiresArgument(arg);
					return CommandLineParser.ParseResult.Error;
				}
				string[] array12 = args;
				num2 = i + 1;
				i = num2;
				this.AddResource(new AssemblyResource(array12[num2], args[i], true), settings);
				return CommandLineParser.ParseResult.Success;
			}
			IL_9F4:
			this.report.Warning(-29, 1, "Compatibility: Use -debug option instead of -g or --debug");
			settings.GenerateDebugInfo = true;
			return CommandLineParser.ParseResult.Success;
			IL_A42:
			if (arg.StartsWith("--fatal", StringComparison.Ordinal))
			{
				int fatalCounter = 1;
				if (arg.StartsWith("--fatal=", StringComparison.Ordinal))
				{
					int.TryParse(arg.Substring(8), out fatalCounter);
				}
				settings.FatalCounter = fatalCounter;
				return CommandLineParser.ParseResult.Success;
			}
			if (arg.StartsWith("--runtime:", StringComparison.Ordinal))
			{
				string a2 = arg.Substring(10);
				if (!(a2 == "v1") && !(a2 == "V1"))
				{
					if (!(a2 == "v2") && !(a2 == "V2"))
					{
						if (a2 == "v4" || a2 == "V4")
						{
							settings.StdLibRuntimeVersion = RuntimeVersion.v4;
						}
					}
					else
					{
						settings.StdLibRuntimeVersion = RuntimeVersion.v2;
					}
				}
				else
				{
					settings.StdLibRuntimeVersion = RuntimeVersion.v1;
				}
				return CommandLineParser.ParseResult.Success;
			}
			if (!arg.StartsWith("--getresourcestrings:", StringComparison.Ordinal))
			{
				return CommandLineParser.ParseResult.UnknownOption;
			}
			string text2 = arg.Substring(21).Trim();
			if (text2.Length < 1)
			{
				this.Error_RequiresArgument(arg);
				return CommandLineParser.ParseResult.Error;
			}
			if (settings.GetResourceStrings == null)
			{
				settings.GetResourceStrings = new List<string>();
			}
			settings.GetResourceStrings.Add(text2);
			return CommandLineParser.ParseResult.Success;
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x0009E960 File Offset: 0x0009CB60
		private void SetWarningLevel(string s, CompilerSettings settings)
		{
			int num = -1;
			try
			{
				num = int.Parse(s);
			}
			catch
			{
			}
			if (num < 0 || num > 4)
			{
				this.report.Error(1900, "Warning level must be in the range 0-4");
				return;
			}
			settings.WarningLevel = num;
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x0009E9B0 File Offset: 0x0009CBB0
		private static void SplitPathAndPattern(string spec, out string path, out string pattern)
		{
			int num = spec.LastIndexOf('/');
			if (num != -1)
			{
				if (num == 0)
				{
					path = "\\";
					pattern = spec.Substring(1);
					return;
				}
				path = spec.Substring(0, num);
				pattern = spec.Substring(num + 1);
				return;
			}
			else
			{
				num = spec.LastIndexOf('\\');
				if (num != -1)
				{
					path = spec.Substring(0, num);
					pattern = spec.Substring(num + 1);
					return;
				}
				path = ".";
				pattern = spec;
				return;
			}
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x0009EA21 File Offset: 0x0009CC21
		private void Usage()
		{
			this.output.WriteLine("Mono C# compiler, Copyright 2001-2011 Novell, Inc., Copyright 2011-2012 Xamarin, Inc\nmcs [options] source-files\n   --about              About the Mono C# compiler\n   -addmodule:M1[,Mn]   Adds the module to the generated assembly\n   -checked[+|-]        Sets default aritmetic overflow context\n   -clscheck[+|-]       Disables CLS Compliance verifications\n   -codepage:ID         Sets code page to the one in ID (number, utf8, reset)\n   -define:S1[;S2]      Defines one or more conditional symbols (short: -d)\n   -debug[+|-], -g      Generate debugging information\n   -delaysign[+|-]      Only insert the public key into the assembly (no signing)\n   -doc:FILE            Process documentation comments to XML file\n   -fullpaths           Any issued error or warning uses absolute file path\n   -help                Lists all compiler options (short: -?)\n   -keycontainer:NAME   The key pair container used to sign the output assembly\n   -keyfile:FILE        The key file used to strongname the ouput assembly\n   -langversion:TEXT    Specifies language version: ISO-1, ISO-2, 3, 4, 5, Default or Future\n   -lib:PATH1[,PATHn]   Specifies the location of referenced assemblies\n   -main:CLASS          Specifies the class with the Main method (short: -m)\n   -noconfig            Disables implicitly referenced assemblies\n   -nostdlib[+|-]       Does not reference mscorlib.dll library\n   -nowarn:W1[,Wn]      Suppress one or more compiler warnings\n   -optimize[+|-]       Enables advanced compiler optimizations (short: -o)\n   -out:FILE            Specifies output assembly name\n   -pkg:P1[,Pn]         References packages P1..Pn\n   -platform:ARCH       Specifies the target platform of the output assembly\n                        ARCH can be one of: anycpu, anycpu32bitpreferred, arm,\n                        x86, x64 or itanium. The default is anycpu.\n   -recurse:SPEC        Recursively compiles files according to SPEC pattern\n   -reference:A1[,An]   Imports metadata from the specified assembly (short: -r)\n   -reference:ALIAS=A   Imports metadata using specified extern alias (short: -r)\n   -sdk:VERSION         Specifies SDK version of referenced assemblies\n                        VERSION can be one of: 2, 4, 4.5 (default) or a custom value\n   -target:KIND         Specifies the format of the output assembly (short: -t)\n                        KIND can be one of: exe, winexe, library, module\n   -unsafe[+|-]         Allows to compile code which uses unsafe keyword\n   -warnaserror[+|-]    Treats all warnings as errors\n   -warnaserror[+|-]:W1[,Wn] Treats one or more compiler warnings as errors\n   -warn:0-4            Sets warning level, the default is 4 (short -w:)\n   -helppublic        Shows public and advanced compiler options\n\nResources:\n   -linkresource:FILE[,ID] Links FILE as a resource (short: -linkres)\n   -resource:FILE[,ID]     Embed FILE as a resource (short: -res)\n   -win32res:FILE          Specifies Win32 resource file (.res)\n   -win32icon:FILE         Use this icon for the output\n   @file                   Read response file for more options\n\nOptions can be of the form -option or /option");
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x0009EA34 File Offset: 0x0009CC34
		private void Version()
		{
			string arg = MethodBase.GetCurrentMethod().DeclaringType.Assembly.GetName().Version.ToString();
			this.output.WriteLine("Mono C# compiler version {0}", arg);
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x0009EA71 File Offset: 0x0009CC71
		// Note: this type is marked as 'beforefieldinit'.
		static CommandLineParser()
		{
		}

		// Token: 0x04000BEA RID: 3050
		private static readonly char[] argument_value_separator = new char[]
		{
			';',
			','
		};

		// Token: 0x04000BEB RID: 3051
		private static readonly char[] numeric_value_separator = new char[]
		{
			';',
			',',
			' '
		};

		// Token: 0x04000BEC RID: 3052
		private readonly TextWriter output;

		// Token: 0x04000BED RID: 3053
		private readonly Report report;

		// Token: 0x04000BEE RID: 3054
		private bool stop_argument;

		// Token: 0x04000BEF RID: 3055
		private Dictionary<string, int> source_file_index;

		// Token: 0x04000BF0 RID: 3056
		[CompilerGenerated]
		private Func<string[], int, int> UnknownOptionHandler;

		// Token: 0x04000BF1 RID: 3057
		private CompilerSettings parser_settings;

		// Token: 0x020003ED RID: 1005
		private enum ParseResult
		{
			// Token: 0x04001126 RID: 4390
			Success,
			// Token: 0x04001127 RID: 4391
			Error,
			// Token: 0x04001128 RID: 4392
			Stop,
			// Token: 0x04001129 RID: 4393
			UnknownOption
		}
	}
}
