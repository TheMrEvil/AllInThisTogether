using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CSharpCompiler;

namespace QFSW.QC
{
	// Token: 0x02000007 RID: 7
	public static class DynamicCodeCommands
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002C90 File Offset: 0x00000E90
		[CommandDescription("Loads the code at the specified file and compiles it to C# which will then be executed. Use with caution as no safety checks will be performed. Not supported in AOT (IL2CPP) builds.\n\nBy default, boiler plate code will NOT be inserted around the code you provide. Please see 'exec' for more information about boilerplate insertion")]
		private static Task ExecuteExternalArbitaryCodeAsync(string filePath, bool insertBoilerplate = false)
		{
			DynamicCodeCommands.<ExecuteExternalArbitaryCodeAsync>d__1 <ExecuteExternalArbitaryCodeAsync>d__;
			<ExecuteExternalArbitaryCodeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ExecuteExternalArbitaryCodeAsync>d__.filePath = filePath;
			<ExecuteExternalArbitaryCodeAsync>d__.insertBoilerplate = insertBoilerplate;
			<ExecuteExternalArbitaryCodeAsync>d__.<>1__state = -1;
			<ExecuteExternalArbitaryCodeAsync>d__.<>t__builder.Start<DynamicCodeCommands.<ExecuteExternalArbitaryCodeAsync>d__1>(ref <ExecuteExternalArbitaryCodeAsync>d__);
			return <ExecuteExternalArbitaryCodeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002CDC File Offset: 0x00000EDC
		[CommandDescription("Compiles the given code to C# which will then be executed. Use with caution as no safety checks will be performed. Not supported in AOT (IL2CPP) builds.\n\nBy default, boiler plate code will be inserted around the code you provide. This means various namespaces will be included, and the main class and main function entry point will provided. In this case, the code you provide should be code that would exist within the body of the main function, and thus cannot contain things such as class definition. If you disable boiler plate insertion, you can write whatever code you want, however you must provide a static entry point called Main in a static class called Program")]
		private static Task ExecuteArbitaryCodeAsync(string code, bool insertBoilerplate = true)
		{
			DynamicCodeCommands.<ExecuteArbitaryCodeAsync>d__2 <ExecuteArbitaryCodeAsync>d__;
			<ExecuteArbitaryCodeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ExecuteArbitaryCodeAsync>d__.code = code;
			<ExecuteArbitaryCodeAsync>d__.insertBoilerplate = insertBoilerplate;
			<ExecuteArbitaryCodeAsync>d__.<>1__state = -1;
			<ExecuteArbitaryCodeAsync>d__.<>t__builder.Start<DynamicCodeCommands.<ExecuteArbitaryCodeAsync>d__2>(ref <ExecuteArbitaryCodeAsync>d__);
			return <ExecuteArbitaryCodeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002D28 File Offset: 0x00000F28
		private static Assembly CompileCode(string code)
		{
			CSharpCompiler.CodeCompiler codeCompiler = new CSharpCompiler.CodeCompiler();
			CompilerParameters compilerParameters = new CompilerParameters();
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			compilerParameters.GenerateExecutable = false;
			compilerParameters.GenerateInMemory = true;
			for (int i = 0; i < assemblies.Length; i++)
			{
				if (!assemblies[i].IsDynamic)
				{
					string location = assemblies[i].Location;
					compilerParameters.ReferencedAssemblies.Add(location);
				}
			}
			CompilerResults compilerResults = codeCompiler.CompileAssemblyFromSource(compilerParameters, code);
			if (compilerResults.Errors.HasErrors)
			{
				string text = "Code Compilation Failure";
				for (int j = 0; j < compilerResults.Errors.Count; j++)
				{
					text = string.Concat(new string[]
					{
						text,
						"\n",
						compilerResults.Errors[j].ErrorNumber,
						" - ",
						compilerResults.Errors[j].ErrorText
					});
				}
				throw new ArgumentException(text);
			}
			return compilerResults.CompiledAssembly;
		}

		// Token: 0x0400000D RID: 13
		private const Platform execAvailability = ~(Platform.IPhonePlayer | Platform.WebGLPlayer | Platform.PS4 | Platform.XboxOne | Platform.Switch);

		// Token: 0x02000017 RID: 23
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ExecuteExternalArbitaryCodeAsync>d__1 : IAsyncStateMachine
		{
			// Token: 0x06000069 RID: 105 RVA: 0x00003C64 File Offset: 0x00001E64
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					TaskAwaiter awaiter;
					if (num != 0)
					{
						if (!File.Exists(this.filePath))
						{
							throw new ArgumentException("file at the specified path '" + this.filePath + "' did not exist.");
						}
						awaiter = DynamicCodeCommands.ExecuteArbitaryCodeAsync(File.ReadAllText(this.filePath).Replace("”", "\"").Replace("“", "\""), this.insertBoilerplate).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, DynamicCodeCommands.<ExecuteExternalArbitaryCodeAsync>d__1>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600006A RID: 106 RVA: 0x00003D68 File Offset: 0x00001F68
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000023 RID: 35
			public int <>1__state;

			// Token: 0x04000024 RID: 36
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000025 RID: 37
			public string filePath;

			// Token: 0x04000026 RID: 38
			public bool insertBoilerplate;

			// Token: 0x04000027 RID: 39
			private TaskAwaiter <>u__1;
		}

		// Token: 0x02000018 RID: 24
		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_0
		{
			// Token: 0x0600006B RID: 107 RVA: 0x00003D76 File Offset: 0x00001F76
			public <>c__DisplayClass2_0()
			{
			}

			// Token: 0x0600006C RID: 108 RVA: 0x00003D80 File Offset: 0x00001F80
			internal MethodInfo <ExecuteArbitaryCodeAsync>b__0()
			{
				string str = string.Empty;
				if (this.insertBoilerplate)
				{
					string[] array = new string[]
					{
						"System",
						"System.Collections",
						"System.Collections.Generic",
						"System.Reflection",
						"System.Linq",
						"System.Text",
						"System.Globalization",
						"UnityEngine",
						"UnityEngine.Events",
						"UnityEngine.EventSystems",
						"UnityEngine.UI"
					};
					for (int i = 0; i < array.Length; i++)
					{
						str = str + "using " + array[i] + ";\n";
					}
					str = str + "\r\n                        public class Program\r\n                        {\r\n                            public static void Main()\r\n                            {" + this.code + "}\r\n                        }";
				}
				else
				{
					str = this.code;
				}
				Assembly assembly = DynamicCodeCommands.CompileCode(str);
				BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
				Type type = assembly.GetType("Program");
				if (type == null)
				{
					throw new ArgumentException("Code Execution Failure - required static class Program could not be found");
				}
				this.entryPoint = type.GetMethod("Main", bindingAttr);
				if (this.entryPoint == null)
				{
					throw new ArgumentException("Code Execution Failure - required static entry point Main could not be found");
				}
				return this.entryPoint;
			}

			// Token: 0x04000028 RID: 40
			public bool insertBoilerplate;

			// Token: 0x04000029 RID: 41
			public string code;

			// Token: 0x0400002A RID: 42
			public MethodInfo entryPoint;
		}

		// Token: 0x02000019 RID: 25
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ExecuteArbitaryCodeAsync>d__2 : IAsyncStateMachine
		{
			// Token: 0x0600006D RID: 109 RVA: 0x00003EA0 File Offset: 0x000020A0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					TaskAwaiter<MethodInfo> awaiter;
					if (num != 0)
					{
						this.<>8__1 = new DynamicCodeCommands.<>c__DisplayClass2_0();
						this.<>8__1.insertBoilerplate = this.insertBoilerplate;
						this.<>8__1.code = this.code;
						awaiter = Task.Run<MethodInfo>(new Func<MethodInfo>(this.<>8__1.<ExecuteArbitaryCodeAsync>b__0)).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<MethodInfo>, DynamicCodeCommands.<ExecuteArbitaryCodeAsync>d__2>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<MethodInfo>);
						this.<>1__state = -1;
					}
					MethodInfo result = awaiter.GetResult();
					this.<>8__1.entryPoint = result;
					this.<>8__1.entryPoint.Invoke(null, null);
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>8__1 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>8__1 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600006E RID: 110 RVA: 0x00003FB8 File Offset: 0x000021B8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400002B RID: 43
			public int <>1__state;

			// Token: 0x0400002C RID: 44
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400002D RID: 45
			public bool insertBoilerplate;

			// Token: 0x0400002E RID: 46
			public string code;

			// Token: 0x0400002F RID: 47
			private DynamicCodeCommands.<>c__DisplayClass2_0 <>8__1;

			// Token: 0x04000030 RID: 48
			private TaskAwaiter<MethodInfo> <>u__1;
		}
	}
}
