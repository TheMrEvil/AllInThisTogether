using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Mono.CSharp
{
	// Token: 0x02000283 RID: 643
	public class DynamicLoader : AssemblyReferencesLoader<Assembly>
	{
		// Token: 0x06001F5E RID: 8030 RVA: 0x0009A7E0 File Offset: 0x000989E0
		public DynamicLoader(ReflectionImporter importer, CompilerContext compiler) : base(compiler)
		{
			this.paths.Add(DynamicLoader.GetSystemDir());
			this.importer = importer;
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001F5F RID: 8031 RVA: 0x0009A800 File Offset: 0x00098A00
		public ReflectionImporter Importer
		{
			get
			{
				return this.importer;
			}
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x0009A808 File Offset: 0x00098A08
		protected override string[] GetDefaultReferences()
		{
			List<string> list = new List<string>(8);
			list.Add("System");
			list.Add("System.Xml");
			list.Add("System.Net");
			list.Add("System.Windows");
			list.Add("System.Windows.Browser");
			if (this.compiler.Settings.Version > LanguageVersion.ISO_2)
			{
				list.Add("System.Core");
			}
			if (this.compiler.Settings.Version > LanguageVersion.V_3)
			{
				list.Add("Microsoft.CSharp");
			}
			return list.ToArray();
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x0009A895 File Offset: 0x00098A95
		private static string GetSystemDir()
		{
			return Path.GetDirectoryName(typeof(object).Assembly.Location);
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x0009A8B0 File Offset: 0x00098AB0
		public override bool HasObjectType(Assembly assembly)
		{
			return assembly.GetType(this.compiler.BuiltinTypes.Object.FullName) != null;
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x0009A8D0 File Offset: 0x00098AD0
		public override Assembly LoadAssemblyFile(string assembly, bool isImplicitReference)
		{
			Assembly result = null;
			try
			{
				try
				{
					char[] anyOf = new char[]
					{
						'/',
						'\\'
					};
					if (assembly.IndexOfAny(anyOf) != -1)
					{
						result = Assembly.LoadFrom(assembly);
					}
					else
					{
						string text = assembly;
						if (text.EndsWith(".dll") || text.EndsWith(".exe"))
						{
							text = assembly.Substring(0, assembly.Length - 4);
						}
						result = Assembly.Load(text);
					}
				}
				catch (FileNotFoundException)
				{
					bool flag = !isImplicitReference;
					foreach (string path in this.paths)
					{
						string text2 = Path.Combine(path, assembly);
						if (!assembly.EndsWith(".dll") && !assembly.EndsWith(".exe"))
						{
							text2 += ".dll";
						}
						try
						{
							result = Assembly.LoadFrom(text2);
							flag = false;
							break;
						}
						catch (FileNotFoundException)
						{
						}
					}
					if (flag)
					{
						base.Error_FileNotFound(assembly);
						return result;
					}
				}
			}
			catch (BadImageFormatException)
			{
				base.Error_FileCorrupted(assembly);
			}
			return result;
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x0009AA08 File Offset: 0x00098C08
		private Module LoadModuleFile(AssemblyDefinitionDynamic assembly, string module)
		{
			string str = "";
			try
			{
				try
				{
					return assembly.IncludeModule(module);
				}
				catch (FileNotFoundException)
				{
					bool flag = true;
					foreach (string path in this.paths)
					{
						string text = Path.Combine(path, module);
						if (!module.EndsWith(".netmodule"))
						{
							text += ".netmodule";
						}
						try
						{
							return assembly.IncludeModule(text);
						}
						catch (FileNotFoundException ex)
						{
							str += ex.FusionLog;
						}
					}
					if (flag)
					{
						base.Error_FileNotFound(module);
						return null;
					}
				}
			}
			catch (BadImageFormatException)
			{
				base.Error_FileCorrupted(module);
			}
			return null;
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x0009AAF0 File Offset: 0x00098CF0
		public void LoadModules(AssemblyDefinitionDynamic assembly, RootNamespace targetNamespace)
		{
			foreach (string module in this.compiler.Settings.Modules)
			{
				Module module2 = this.LoadModuleFile(assembly, module);
				if (module2 != null)
				{
					ImportedModuleDefinition module3 = this.importer.ImportModule(module2, targetNamespace);
					assembly.AddModule(module3);
				}
			}
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x0009AB68 File Offset: 0x00098D68
		public override void LoadReferences(ModuleContainer module)
		{
			Assembly assembly;
			List<Tuple<RootNamespace, Assembly>> list;
			base.LoadReferencesCore(module, out assembly, out list);
			if (assembly == null)
			{
				return;
			}
			this.importer.ImportAssembly(assembly, module.GlobalRootNamespace);
			foreach (Tuple<RootNamespace, Assembly> tuple in list)
			{
				this.importer.ImportAssembly(tuple.Item2, tuple.Item1);
			}
		}

		// Token: 0x04000B86 RID: 2950
		private readonly ReflectionImporter importer;
	}
}
