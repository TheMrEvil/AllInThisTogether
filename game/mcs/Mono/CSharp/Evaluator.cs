using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace Mono.CSharp
{
	// Token: 0x02000170 RID: 368
	public class Evaluator
	{
		// Token: 0x060011B6 RID: 4534 RVA: 0x00049114 File Offset: 0x00047314
		public Evaluator(CompilerContext ctx)
		{
			this.ctx = ctx;
			this.module = new ModuleContainer(ctx);
			this.module.Evaluator = this;
			this.source_file = new CompilationSourceFile(this.module, null);
			this.module.AddTypeContainer(this.source_file);
			this.startup_files = ctx.SourceFiles.Count;
			this.module.SetDeclaringAssembly(new AssemblyDefinitionDynamic(this.module, "evaluator"));
			this.importer = new ReflectionImporter(this.module, ctx.BuiltinTypes);
			this.InteractiveBaseClass = typeof(InteractiveBase);
			this.fields = new Dictionary<string, Tuple<FieldSpec, FieldInfo>>();
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x000491D0 File Offset: 0x000473D0
		private void Init()
		{
			AssemblyReferencesLoader<Assembly> assemblyReferencesLoader = new DynamicLoader(this.importer, this.ctx);
			CompilerCallableEntryPoint.Reset();
			RootContext.ToplevelTypes = this.module;
			assemblyReferencesLoader.LoadReferences(this.module);
			this.ctx.BuiltinTypes.CheckDefinitions(this.module);
			this.module.InitializePredefinedTypes();
			this.inited = true;
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00049234 File Offset: 0x00047434
		private void ParseStartupFiles()
		{
			Driver driver = new Driver(this.ctx);
			Location.Initialize(this.ctx.SourceFiles);
			ParserSession session = new ParserSession();
			for (int i = 0; i < this.startup_files; i++)
			{
				SourceFile file = this.ctx.SourceFiles[i];
				driver.Parse(file, this.module, session, this.ctx.Report);
			}
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0004929F File Offset: 0x0004749F
		private void Reset()
		{
			CompilerCallableEntryPoint.PartialReset();
			Location.Reset();
			Location.Initialize(this.ctx.SourceFiles);
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x000492BB File Offset: 0x000474BB
		// (set) Token: 0x060011BB RID: 4539 RVA: 0x000492C3 File Offset: 0x000474C3
		public bool WaitOnTask
		{
			[CompilerGenerated]
			get
			{
				return this.<WaitOnTask>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<WaitOnTask>k__BackingField = value;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x000492CC File Offset: 0x000474CC
		// (set) Token: 0x060011BD RID: 4541 RVA: 0x000492D4 File Offset: 0x000474D4
		public Type InteractiveBaseClass
		{
			get
			{
				return this.base_class;
			}
			set
			{
				this.base_class = value;
				if (value != null && typeof(InteractiveBase).IsAssignableFrom(value))
				{
					InteractiveBase.Evaluator = this;
				}
			}
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x000492F8 File Offset: 0x000474F8
		public void Interrupt()
		{
			if (!this.inited || !Evaluator.invoking)
			{
				return;
			}
			if (Evaluator.invoke_thread != null)
			{
				Evaluator.invoke_thread.Abort();
			}
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x00049320 File Offset: 0x00047520
		public string Compile(string input, out CompiledMethod compiled)
		{
			if (input == null || input.Length == 0)
			{
				compiled = null;
				return null;
			}
			object obj = Evaluator.evaluator_lock;
			string result;
			lock (obj)
			{
				if (!this.inited)
				{
					this.Init();
					this.ParseStartupFiles();
				}
				else
				{
					this.ctx.Report.Printer.Reset();
				}
				bool flag;
				CSharpParser csharpParser = this.ParseString(Evaluator.ParseMode.Silent, input, out flag);
				bool flag2;
				if (csharpParser == null && this.Terse && flag && this.ParseString(Evaluator.ParseMode.Silent, input + "{}", out flag2) == null)
				{
					csharpParser = this.ParseString(Evaluator.ParseMode.Silent, input + ";", out flag2);
				}
				if (csharpParser == null)
				{
					compiled = null;
					if (flag)
					{
						result = input;
					}
					else
					{
						this.ParseString(Evaluator.ParseMode.ReportErrors, input, out flag);
						result = null;
					}
				}
				else
				{
					Class interactiveResult = csharpParser.InteractiveResult;
					compiled = this.CompileBlock(interactiveResult, csharpParser.undo, this.ctx.Report);
					result = null;
				}
			}
			return result;
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0004941C File Offset: 0x0004761C
		public CompiledMethod Compile(string input)
		{
			CompiledMethod result;
			if (this.Compile(input, out result) != null)
			{
				return null;
			}
			return result;
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00049438 File Offset: 0x00047638
		public void EmitValueChangedCallback(EmitContext ec, string name, TypeSpec type, Location loc)
		{
			if (this.listener_id == null)
			{
				this.listener_id = new int?(ListenerProxy.Register(this.ModificationListener));
			}
			if (Evaluator.listener_proxy_value == null)
			{
				Evaluator.listener_proxy_value = typeof(ListenerProxy).GetMethod("ValueChanged");
			}
			if (type.IsStructOrEnum)
			{
				ec.Emit(OpCodes.Box, type);
			}
			ec.EmitInt(loc.Row);
			ec.EmitInt(loc.Column);
			ec.Emit(OpCodes.Ldstr, name);
			ec.EmitInt(this.listener_id.Value);
			ec.Emit(OpCodes.Call, Evaluator.listener_proxy_value);
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x000494E4 File Offset: 0x000476E4
		public string Evaluate(string input, out object result, out bool result_set)
		{
			result_set = false;
			result = null;
			CompiledMethod compiledMethod;
			input = this.Compile(input, out compiledMethod);
			if (input != null)
			{
				return input;
			}
			if (compiledMethod == null)
			{
				return null;
			}
			object typeFromHandle = typeof(Evaluator.QuitValue);
			try
			{
				Evaluator.invoke_thread = Thread.CurrentThread;
				Evaluator.invoking = true;
				compiledMethod(ref typeFromHandle);
			}
			catch (ThreadAbortException arg)
			{
				Thread.ResetAbort();
				Console.WriteLine("Interrupted!\n{0}", arg);
			}
			finally
			{
				Evaluator.invoking = false;
				if (this.listener_id != null)
				{
					ListenerProxy.Unregister(this.listener_id.Value);
					this.listener_id = null;
				}
			}
			if (typeFromHandle != typeof(Evaluator.QuitValue))
			{
				result_set = true;
				result = typeFromHandle;
			}
			return null;
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x000495A8 File Offset: 0x000477A8
		public string[] GetCompletions(string input, out string prefix)
		{
			prefix = "";
			if (input == null || input.Length == 0)
			{
				return null;
			}
			object obj = Evaluator.evaluator_lock;
			lock (obj)
			{
				if (!this.inited)
				{
					this.Init();
				}
				bool flag;
				CSharpParser csharpParser = this.ParseString(Evaluator.ParseMode.GetCompletions, input, out flag);
				if (csharpParser == null)
				{
					return null;
				}
				Class interactiveResult = csharpParser.InteractiveResult;
				TypeSpec t = this.importer.ImportType(this.base_class);
				List<FullNamedExpression> baseTypes = new List<FullNamedExpression>(1)
				{
					new TypeExpression(t, interactiveResult.Location)
				};
				interactiveResult.SetBaseTypes(baseTypes);
				AssemblyBuilderAccess access = AssemblyBuilderAccess.Run;
				AssemblyDefinitionDynamic assemblyDefinitionDynamic = new AssemblyDefinitionDynamic(this.module, "completions");
				assemblyDefinitionDynamic.Create(AppDomain.CurrentDomain, access);
				this.module.SetDeclaringAssembly(assemblyDefinitionDynamic);
				interactiveResult.CreateContainer();
				interactiveResult.DefineContainer();
				Method method = interactiveResult.Members[0] as Method;
				BlockContext bc = new BlockContext(method, method.Block, this.ctx.BuiltinTypes.Void);
				try
				{
					method.Block.Resolve(bc, method);
				}
				catch (CompletionResult completionResult)
				{
					prefix = completionResult.BaseText;
					return completionResult.Result;
				}
			}
			return null;
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x000496F8 File Offset: 0x000478F8
		public bool Run(string statement)
		{
			object obj;
			bool flag;
			return this.Evaluate(statement, out obj, out flag) == null;
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00049714 File Offset: 0x00047914
		public object Evaluate(string input)
		{
			object result;
			bool flag;
			if (this.Evaluate(input, out result, out flag) != null)
			{
				throw new ArgumentException("Syntax error on input: partial input");
			}
			if (!flag)
			{
				throw new ArgumentException("The expression failed to resolve");
			}
			return result;
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x00049748 File Offset: 0x00047948
		// (set) Token: 0x060011C7 RID: 4551 RVA: 0x00049750 File Offset: 0x00047950
		public ValueModificationHandler ModificationListener
		{
			[CompilerGenerated]
			get
			{
				return this.<ModificationListener>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ModificationListener>k__BackingField = value;
			}
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x0004975C File Offset: 0x0004795C
		private Evaluator.InputKind ToplevelOrStatement(SeekableStreamReader seekable)
		{
			Tokenizer tokenizer = new Tokenizer(seekable, this.source_file, new ParserSession(), this.ctx.Report);
			tokenizer.parsing_block++;
			int num = tokenizer.token();
			if (num <= 270)
			{
				if (num <= 265)
				{
					switch (num)
					{
					case 257:
						return Evaluator.InputKind.EOF;
					case 258:
					case 260:
						return Evaluator.InputKind.StatementOrExpression;
					case 259:
						return Evaluator.InputKind.Error;
					case 261:
						break;
					default:
						if (num != 265)
						{
							return Evaluator.InputKind.StatementOrExpression;
						}
						return Evaluator.InputKind.StatementOrExpression;
					}
				}
				else
				{
					if (num != 267 && num != 270)
					{
						return Evaluator.InputKind.StatementOrExpression;
					}
					return Evaluator.InputKind.StatementOrExpression;
				}
			}
			else if (num <= 323)
			{
				if (num != 272)
				{
					switch (num)
					{
					case 275:
					case 279:
					case 287:
					case 288:
					case 295:
					case 300:
					case 302:
					case 304:
					case 316:
					case 318:
					case 322:
						return Evaluator.InputKind.StatementOrExpression;
					case 276:
					case 278:
					case 280:
					case 282:
					case 283:
					case 285:
					case 286:
					case 289:
					case 290:
					case 291:
					case 292:
					case 293:
					case 294:
					case 298:
					case 299:
					case 303:
					case 305:
					case 306:
					case 308:
					case 313:
					case 314:
					case 315:
					case 319:
					case 320:
						return Evaluator.InputKind.StatementOrExpression;
					case 277:
						num = tokenizer.token();
						if (num == 257)
						{
							return Evaluator.InputKind.EOF;
						}
						if (num == 375 || num == 371)
						{
							return Evaluator.InputKind.StatementOrExpression;
						}
						return Evaluator.InputKind.CompilationUnit;
					case 281:
					case 284:
					case 296:
					case 297:
					case 301:
					case 309:
					case 310:
					case 311:
					case 317:
					case 321:
					case 323:
						break;
					case 307:
					case 312:
						return Evaluator.InputKind.Error;
					default:
						return Evaluator.InputKind.StatementOrExpression;
					}
				}
			}
			else
			{
				switch (num)
				{
				case 330:
				case 331:
					return Evaluator.InputKind.StatementOrExpression;
				case 332:
				case 334:
					return Evaluator.InputKind.StatementOrExpression;
				case 333:
					num = tokenizer.token();
					if (num == 257)
					{
						return Evaluator.InputKind.EOF;
					}
					if (num == 375)
					{
						return Evaluator.InputKind.StatementOrExpression;
					}
					return Evaluator.InputKind.CompilationUnit;
				case 335:
					num = tokenizer.token();
					if (num == 257)
					{
						return Evaluator.InputKind.EOF;
					}
					if (num == 422 || num == 321)
					{
						return Evaluator.InputKind.CompilationUnit;
					}
					return Evaluator.InputKind.StatementOrExpression;
				default:
					if (num != 373 && num != 427)
					{
						return Evaluator.InputKind.StatementOrExpression;
					}
					break;
				}
			}
			return Evaluator.InputKind.CompilationUnit;
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00049988 File Offset: 0x00047B88
		private CSharpParser ParseString(Evaluator.ParseMode mode, string input, out bool partial_input)
		{
			partial_input = false;
			this.Reset();
			Encoding encoding = this.ctx.Settings.Encoding;
			SeekableStreamReader seekableStreamReader = new SeekableStreamReader(new MemoryStream(encoding.GetBytes(input)), encoding, null);
			Evaluator.InputKind inputKind = this.ToplevelOrStatement(seekableStreamReader);
			if (inputKind == Evaluator.InputKind.Error)
			{
				if (mode == Evaluator.ParseMode.ReportErrors)
				{
					this.ctx.Report.Error(-25, "Detection Parsing Error");
				}
				partial_input = false;
				return null;
			}
			if (inputKind == Evaluator.InputKind.EOF)
			{
				if (mode == Evaluator.ParseMode.ReportErrors)
				{
					Console.Error.WriteLine("Internal error: EOF condition should have been detected in a previous call with silent=true");
				}
				partial_input = true;
				return null;
			}
			seekableStreamReader.Position = 0;
			this.source_file.DeclarationFound = false;
			CSharpParser csharpParser = new CSharpParser(seekableStreamReader, this.source_file, new ParserSession());
			if (inputKind == Evaluator.InputKind.StatementOrExpression)
			{
				csharpParser.Lexer.putback_char = 1048576;
				csharpParser.Lexer.parsing_block++;
				this.ctx.Settings.StatementMode = true;
			}
			else
			{
				csharpParser.Lexer.putback_char = 1048577;
				this.ctx.Settings.StatementMode = false;
			}
			if (mode == Evaluator.ParseMode.GetCompletions)
			{
				csharpParser.Lexer.CompleteOnEOF = true;
			}
			ReportPrinter reportPrinter = null;
			if (mode == Evaluator.ParseMode.Silent || mode == Evaluator.ParseMode.GetCompletions)
			{
				reportPrinter = this.ctx.Report.SetPrinter(new StreamReportPrinter(TextWriter.Null));
			}
			try
			{
				csharpParser.parse();
			}
			finally
			{
				if (this.ctx.Report.Errors != 0)
				{
					if (mode != Evaluator.ParseMode.ReportErrors && csharpParser.UnexpectedEOF)
					{
						partial_input = true;
					}
					if (csharpParser.undo != null)
					{
						csharpParser.undo.ExecuteUndo();
					}
					csharpParser = null;
				}
				if (reportPrinter != null)
				{
					this.ctx.Report.SetPrinter(reportPrinter);
				}
			}
			return csharpParser;
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00049B28 File Offset: 0x00047D28
		private CompiledMethod CompileBlock(Class host, Undo undo, Report Report)
		{
			string text = "eval-" + Evaluator.count + ".dll";
			Evaluator.count++;
			AssemblyBuilderAccess assemblyBuilderAccess;
			AssemblyDefinitionDynamic assemblyDefinitionDynamic;
			if (Environment.GetEnvironmentVariable("SAVE") != null)
			{
				assemblyBuilderAccess = AssemblyBuilderAccess.RunAndSave;
				ModuleContainer moduleContainer = this.module;
				string text2 = text;
				assemblyDefinitionDynamic = new AssemblyDefinitionDynamic(moduleContainer, text2, text2);
				assemblyDefinitionDynamic.Importer = this.importer;
			}
			else
			{
				assemblyBuilderAccess = AssemblyBuilderAccess.Run;
				assemblyDefinitionDynamic = new AssemblyDefinitionDynamic(this.module, text);
			}
			assemblyDefinitionDynamic.Create(AppDomain.CurrentDomain, assemblyBuilderAccess);
			Method method;
			if (host != null)
			{
				TypeSpec t = this.importer.ImportType(this.base_class);
				List<FullNamedExpression> baseTypes = new List<FullNamedExpression>(1)
				{
					new TypeExpression(t, host.Location)
				};
				host.SetBaseTypes(baseTypes);
				method = (Method)host.Members[0];
				if ((method.ModFlags & Modifiers.ASYNC) != (Modifiers)0)
				{
					ParametersCompiled parametersCompiled = new ParametersCompiled(new Parameter[]
					{
						new Parameter(new TypeExpression(this.module.Compiler.BuiltinTypes.Object, Location.Null), "$retval", Parameter.Modifier.REF, null, Location.Null)
					});
					Method method2 = new Method(host, new TypeExpression(this.module.Compiler.BuiltinTypes.Void, Location.Null), Modifiers.PUBLIC | Modifiers.STATIC, new MemberName("AsyncWait"), parametersCompiled, null);
					Method method3 = method2;
					method3.Block = new ToplevelBlock(method3.Compiler, parametersCompiled, Location.Null, (Block.Flags)0);
					method2.Block.AddStatement(new StatementExpression(new SimpleAssign(new SimpleName(parametersCompiled[0].Name, Location.Null), new Invocation(new SimpleName(method.MemberName.Name, Location.Null), new Arguments(0)), Location.Null), Location.Null));
					if (this.WaitOnTask)
					{
						Cast expr = new Cast(method.TypeExpression, new SimpleName(parametersCompiled[0].Name, Location.Null), Location.Null);
						method2.Block.AddStatement(new StatementExpression(new Invocation(new MemberAccess(expr, "Wait", Location.Null), new Arguments(0)), Location.Null));
					}
					host.AddMember(method2);
					method = method2;
				}
				host.CreateContainer();
				host.DefineContainer();
				host.Define();
			}
			else
			{
				method = null;
			}
			this.module.CreateContainer();
			this.module.EnableRedefinition();
			this.source_file.EnableRedefinition();
			this.module.Define();
			if (Report.Errors != 0)
			{
				if (undo != null)
				{
					undo.ExecuteUndo();
				}
				return null;
			}
			if (host != null)
			{
				host.PrepareEmit();
				host.EmitContainer();
			}
			this.module.EmitContainer();
			if (Report.Errors != 0)
			{
				if (undo != null)
				{
					undo.ExecuteUndo();
				}
				return null;
			}
			this.module.CloseContainer();
			if (host != null)
			{
				host.CloseContainer();
			}
			if (assemblyBuilderAccess == AssemblyBuilderAccess.RunAndSave)
			{
				assemblyDefinitionDynamic.Save();
			}
			if (host == null)
			{
				return null;
			}
			Type type = assemblyDefinitionDynamic.Builder.GetType(host.TypeBuilder.Name);
			MethodInfo method4 = type.GetMethod(method.MemberName.Name);
			foreach (MemberCore memberCore in host.Members)
			{
				Field field = memberCore as Field;
				if (field != null)
				{
					FieldInfo field2 = type.GetField(field.Name);
					Tuple<FieldSpec, FieldInfo> tuple;
					if (this.fields.TryGetValue(field.Name, out tuple) && !tuple.Item1.MemberType.IsStruct)
					{
						try
						{
							tuple.Item2.SetValue(null, null);
						}
						catch
						{
						}
					}
					this.fields[field.Name] = Tuple.Create<FieldSpec, FieldInfo>(field.Spec, field2);
				}
			}
			return (CompiledMethod)Delegate.CreateDelegate(typeof(CompiledMethod), method4);
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00049F0C File Offset: 0x0004810C
		public Tuple<FieldSpec, FieldInfo> LookupField(string name)
		{
			Tuple<FieldSpec, FieldInfo> result;
			this.fields.TryGetValue(name, out result);
			return result;
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00049F29 File Offset: 0x00048129
		private static string Quote(string s)
		{
			if (s.IndexOf('"') != -1)
			{
				s = s.Replace("\"", "\\\"");
			}
			return "\"" + s + "\"";
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00049F58 File Offset: 0x00048158
		public string GetUsing()
		{
			if (this.source_file == null || this.source_file.Usings == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (UsingClause usingClause in this.source_file.Usings)
			{
				if (usingClause.Alias == null && usingClause.ResolvedExpression != null)
				{
					stringBuilder.AppendFormat("using {0};", usingClause.ToString());
					stringBuilder.Append(Environment.NewLine);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0004A000 File Offset: 0x00048200
		public List<string> GetUsingList()
		{
			List<string> list = new List<string>();
			if (this.source_file == null || this.source_file.Usings == null)
			{
				return list;
			}
			foreach (UsingClause usingClause in this.source_file.Usings)
			{
				if (usingClause.Alias == null && usingClause.ResolvedExpression != null)
				{
					list.Add(usingClause.NamespaceExpression.Name);
				}
			}
			return list;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0004A090 File Offset: 0x00048290
		public string[] GetVarNames()
		{
			object obj = Evaluator.evaluator_lock;
			string[] result;
			lock (obj)
			{
				result = new List<string>(this.fields.Keys).ToArray();
			}
			return result;
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0004A0DC File Offset: 0x000482DC
		public string GetVars()
		{
			object obj = Evaluator.evaluator_lock;
			string result;
			lock (obj)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (KeyValuePair<string, Tuple<FieldSpec, FieldInfo>> keyValuePair in this.fields)
				{
					Tuple<FieldSpec, FieldInfo> tuple = this.LookupField(keyValuePair.Key);
					object obj2;
					try
					{
						obj2 = tuple.Item2.GetValue(null);
						if (obj2 is string)
						{
							obj2 = Evaluator.Quote((string)obj2);
						}
					}
					catch
					{
						obj2 = "<error reading value>";
					}
					stringBuilder.AppendFormat("{0} {1} = {2}", tuple.Item1.MemberType.GetSignatureForError(), keyValuePair.Key, obj2);
					stringBuilder.AppendLine();
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0004A1D4 File Offset: 0x000483D4
		public void LoadAssembly(string file)
		{
			Assembly assembly = new DynamicLoader(this.importer, this.ctx).LoadAssemblyFile(file, false);
			if (assembly == null)
			{
				return;
			}
			object obj = Evaluator.evaluator_lock;
			lock (obj)
			{
				this.importer.ImportAssembly(assembly, this.module.GlobalRootNamespace);
			}
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0004A23C File Offset: 0x0004843C
		public void ReferenceAssembly(Assembly a)
		{
			object obj = Evaluator.evaluator_lock;
			lock (obj)
			{
				this.importer.ImportAssembly(a, this.module.GlobalRootNamespace);
			}
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0004A288 File Offset: 0x00048488
		// Note: this type is marked as 'beforefieldinit'.
		static Evaluator()
		{
		}

		// Token: 0x0400078E RID: 1934
		private static object evaluator_lock = new object();

		// Token: 0x0400078F RID: 1935
		private static volatile bool invoking;

		// Token: 0x04000790 RID: 1936
		private static int count;

		// Token: 0x04000791 RID: 1937
		private static Thread invoke_thread;

		// Token: 0x04000792 RID: 1938
		private readonly Dictionary<string, Tuple<FieldSpec, FieldInfo>> fields;

		// Token: 0x04000793 RID: 1939
		private Type base_class;

		// Token: 0x04000794 RID: 1940
		private bool inited;

		// Token: 0x04000795 RID: 1941
		private int startup_files;

		// Token: 0x04000796 RID: 1942
		private readonly CompilerContext ctx;

		// Token: 0x04000797 RID: 1943
		private readonly ModuleContainer module;

		// Token: 0x04000798 RID: 1944
		private readonly ReflectionImporter importer;

		// Token: 0x04000799 RID: 1945
		private readonly CompilationSourceFile source_file;

		// Token: 0x0400079A RID: 1946
		private int? listener_id;

		// Token: 0x0400079B RID: 1947
		[CompilerGenerated]
		private bool <WaitOnTask>k__BackingField;

		// Token: 0x0400079C RID: 1948
		public bool DescribeTypeExpressions;

		// Token: 0x0400079D RID: 1949
		public bool Terse = true;

		// Token: 0x0400079E RID: 1950
		private static MethodInfo listener_proxy_value;

		// Token: 0x0400079F RID: 1951
		[CompilerGenerated]
		private ValueModificationHandler <ModificationListener>k__BackingField;

		// Token: 0x02000392 RID: 914
		private enum ParseMode
		{
			// Token: 0x04000F8F RID: 3983
			Silent,
			// Token: 0x04000F90 RID: 3984
			ReportErrors,
			// Token: 0x04000F91 RID: 3985
			GetCompletions
		}

		// Token: 0x02000393 RID: 915
		private enum InputKind
		{
			// Token: 0x04000F93 RID: 3987
			EOF,
			// Token: 0x04000F94 RID: 3988
			StatementOrExpression,
			// Token: 0x04000F95 RID: 3989
			CompilationUnit,
			// Token: 0x04000F96 RID: 3990
			Error
		}

		// Token: 0x02000394 RID: 916
		public static class QuitValue
		{
		}
	}
}
