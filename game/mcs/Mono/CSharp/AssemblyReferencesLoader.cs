using System;
using System.Collections.Generic;
using System.IO;

namespace Mono.CSharp
{
	// Token: 0x02000104 RID: 260
	public abstract class AssemblyReferencesLoader<T> where T : class
	{
		// Token: 0x06000D0F RID: 3343 RVA: 0x0002F3BA File Offset: 0x0002D5BA
		protected AssemblyReferencesLoader(CompilerContext compiler)
		{
			this.compiler = compiler;
			this.paths = new List<string>();
			this.paths.Add(Directory.GetCurrentDirectory());
			this.paths.AddRange(compiler.Settings.ReferencesLookupPaths);
		}

		// Token: 0x06000D10 RID: 3344
		public abstract bool HasObjectType(T assembly);

		// Token: 0x06000D11 RID: 3345
		protected abstract string[] GetDefaultReferences();

		// Token: 0x06000D12 RID: 3346
		public abstract T LoadAssemblyFile(string fileName, bool isImplicitReference);

		// Token: 0x06000D13 RID: 3347
		public abstract void LoadReferences(ModuleContainer module);

		// Token: 0x06000D14 RID: 3348 RVA: 0x0002F3FA File Offset: 0x0002D5FA
		protected void Error_FileNotFound(string fileName)
		{
			this.compiler.Report.Error(6, "Metadata file `{0}' could not be found", fileName);
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0002F413 File Offset: 0x0002D613
		protected void Error_FileCorrupted(string fileName)
		{
			this.compiler.Report.Error(9, "Metadata file `{0}' does not contain valid metadata", fileName);
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0002F42D File Offset: 0x0002D62D
		protected void Error_AssemblyIsModule(string fileName)
		{
			this.compiler.Report.Error(1509, "Referenced assembly file `{0}' is a module. Consider using `-addmodule' option to add the module", fileName);
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0002F44A File Offset: 0x0002D64A
		protected void Error_ModuleIsAssembly(string fileName)
		{
			this.compiler.Report.Error(1542, "Added module file `{0}' is an assembly. Consider using `-r' option to reference the file", fileName);
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0002F468 File Offset: 0x0002D668
		protected void LoadReferencesCore(ModuleContainer module, out T corlib_assembly, out List<Tuple<RootNamespace, T>> loaded)
		{
			this.compiler.TimeReporter.Start(TimeReporter.TimerType.ReferencesLoading);
			loaded = new List<Tuple<RootNamespace, T>>();
			if (module.Compiler.Settings.StdLib)
			{
				corlib_assembly = this.LoadAssemblyFile("mscorlib.dll", true);
			}
			else
			{
				corlib_assembly = default(T);
			}
			foreach (string fileName in module.Compiler.Settings.AssemblyReferences)
			{
				T t = this.LoadAssemblyFile(fileName, false);
				if (t != null && !EqualityComparer<T>.Default.Equals(t, corlib_assembly))
				{
					Tuple<RootNamespace, T> item = Tuple.Create<RootNamespace, T>(module.GlobalRootNamespace, t);
					if (!loaded.Contains(item))
					{
						loaded.Add(item);
					}
				}
			}
			if (corlib_assembly == null)
			{
				for (int i = 0; i < loaded.Count; i++)
				{
					Tuple<RootNamespace, T> tuple = loaded[i];
					if (this.HasObjectType(tuple.Item2))
					{
						corlib_assembly = tuple.Item2;
						loaded.RemoveAt(i);
						break;
					}
				}
			}
			foreach (Tuple<string, string> tuple2 in module.Compiler.Settings.AssemblyReferencesAliases)
			{
				T t = this.LoadAssemblyFile(tuple2.Item2, false);
				if (t != null)
				{
					Tuple<RootNamespace, T> item2 = Tuple.Create<RootNamespace, T>(module.CreateRootNamespace(tuple2.Item1), t);
					if (!loaded.Contains(item2))
					{
						loaded.Add(item2);
					}
				}
			}
			if (this.compiler.Settings.LoadDefaultReferences)
			{
				foreach (string fileName2 in this.GetDefaultReferences())
				{
					T t = this.LoadAssemblyFile(fileName2, true);
					if (t != null)
					{
						Tuple<RootNamespace, T> item3 = Tuple.Create<RootNamespace, T>(module.GlobalRootNamespace, t);
						if (!loaded.Contains(item3))
						{
							loaded.Add(item3);
						}
					}
				}
			}
			this.compiler.TimeReporter.Stop(TimeReporter.TimerType.ReferencesLoading);
		}

		// Token: 0x0400063E RID: 1598
		protected readonly CompilerContext compiler;

		// Token: 0x0400063F RID: 1599
		protected readonly List<string> paths;
	}
}
