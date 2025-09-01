using System;
using System.Collections.Generic;
using System.Configuration;

namespace System.CodeDom.Compiler
{
	// Token: 0x02000359 RID: 857
	[ConfigurationCollection(typeof(Compiler), AddItemName = "compiler", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	internal sealed class CompilerCollection : ConfigurationElementCollection
	{
		// Token: 0x06001C63 RID: 7267 RVA: 0x00066FFC File Offset: 0x000651FC
		static CompilerCollection()
		{
			CompilerInfo compilerInfo = new CompilerInfo(null, "Microsoft.CSharp.CSharpCodeProvider, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", new string[]
			{
				"c#",
				"cs",
				"csharp"
			}, new string[]
			{
				".cs"
			});
			compilerInfo.ProviderOptions["CompilerVersion"] = CompilerCollection.defaultCompilerVersion;
			CompilerCollection.AddCompilerInfo(compilerInfo);
			CompilerInfo compilerInfo2 = new CompilerInfo(null, "Microsoft.VisualBasic.VBCodeProvider, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", new string[]
			{
				"vb",
				"vbs",
				"visualbasic",
				"vbscript"
			}, new string[]
			{
				".vb"
			});
			compilerInfo2.ProviderOptions["CompilerVersion"] = CompilerCollection.defaultCompilerVersion;
			CompilerCollection.AddCompilerInfo(compilerInfo2);
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x000316D3 File Offset: 0x0002F8D3
		public CompilerCollection()
		{
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x000670F4 File Offset: 0x000652F4
		private static void AddCompilerInfo(CompilerInfo ci)
		{
			ci.CreateProvider();
			CompilerCollection.compiler_infos.Add(ci);
			string[] languages = ci.GetLanguages();
			if (languages != null)
			{
				foreach (string key in languages)
				{
					CompilerCollection.compiler_languages[key] = ci;
				}
			}
			string[] extensions = ci.GetExtensions();
			if (extensions != null)
			{
				foreach (string key2 in extensions)
				{
					CompilerCollection.compiler_extensions[key2] = ci;
				}
			}
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x0006716C File Offset: 0x0006536C
		private static void AddCompilerInfo(Compiler compiler)
		{
			CompilerCollection.AddCompilerInfo(new CompilerInfo(null, compiler.Type, new string[]
			{
				compiler.Extension
			}, new string[]
			{
				compiler.Language
			})
			{
				CompilerParams = 
				{
					CompilerOptions = compiler.CompilerOptions,
					WarningLevel = compiler.WarningLevel
				}
			});
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x000671CC File Offset: 0x000653CC
		protected override void BaseAdd(ConfigurationElement element)
		{
			Compiler compiler = element as Compiler;
			if (compiler != null)
			{
				CompilerCollection.AddCompilerInfo(compiler);
			}
			base.BaseAdd(element);
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001C68 RID: 7272 RVA: 0x00003062 File Offset: 0x00001262
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x000671F0 File Offset: 0x000653F0
		protected override ConfigurationElement CreateNewElement()
		{
			return new Compiler();
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x000671F8 File Offset: 0x000653F8
		public CompilerInfo GetCompilerInfoForLanguage(string language)
		{
			if (CompilerCollection.compiler_languages.Count == 0)
			{
				return null;
			}
			CompilerInfo result;
			if (CompilerCollection.compiler_languages.TryGetValue(language, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x00067228 File Offset: 0x00065428
		public CompilerInfo GetCompilerInfoForExtension(string extension)
		{
			if (CompilerCollection.compiler_extensions.Count == 0)
			{
				return null;
			}
			CompilerInfo result;
			if (CompilerCollection.compiler_extensions.TryGetValue(extension, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x00067258 File Offset: 0x00065458
		public string GetLanguageFromExtension(string extension)
		{
			CompilerInfo compilerInfoForExtension = this.GetCompilerInfoForExtension(extension);
			if (compilerInfoForExtension == null)
			{
				return null;
			}
			string[] languages = compilerInfoForExtension.GetLanguages();
			if (languages != null && languages.Length != 0)
			{
				return languages[0];
			}
			return null;
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x00067285 File Offset: 0x00065485
		public Compiler Get(int index)
		{
			return (Compiler)base.BaseGet(index);
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x00067293 File Offset: 0x00065493
		public Compiler Get(string language)
		{
			return (Compiler)base.BaseGet(language);
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x000672A1 File Offset: 0x000654A1
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((Compiler)element).Language;
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x000672AE File Offset: 0x000654AE
		public string GetKey(int index)
		{
			return (string)base.BaseGetKey(index);
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001C71 RID: 7281 RVA: 0x000672BC File Offset: 0x000654BC
		public string[] AllKeys
		{
			get
			{
				string[] array = new string[CompilerCollection.compiler_infos.Count];
				for (int i = 0; i < base.Count; i++)
				{
					array[i] = string.Join(";", CompilerCollection.compiler_infos[i].GetLanguages());
				}
				return array;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x00003062 File Offset: 0x00001262
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001C73 RID: 7283 RVA: 0x00067308 File Offset: 0x00065508
		protected override string ElementName
		{
			get
			{
				return "compiler";
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001C74 RID: 7284 RVA: 0x0006730F File Offset: 0x0006550F
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CompilerCollection.properties;
			}
		}

		// Token: 0x170005BA RID: 1466
		public Compiler this[int index]
		{
			get
			{
				return (Compiler)base.BaseGet(index);
			}
		}

		// Token: 0x170005BB RID: 1467
		public CompilerInfo this[string language]
		{
			get
			{
				return this.GetCompilerInfoForLanguage(language);
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001C77 RID: 7287 RVA: 0x0006731F File Offset: 0x0006551F
		public CompilerInfo[] CompilerInfos
		{
			get
			{
				return CompilerCollection.compiler_infos.ToArray();
			}
		}

		// Token: 0x04000E7F RID: 3711
		private static readonly string defaultCompilerVersion = "4.0";

		// Token: 0x04000E80 RID: 3712
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000E81 RID: 3713
		private static List<CompilerInfo> compiler_infos = new List<CompilerInfo>();

		// Token: 0x04000E82 RID: 3714
		private static Dictionary<string, CompilerInfo> compiler_languages = new Dictionary<string, CompilerInfo>(16, StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000E83 RID: 3715
		private static Dictionary<string, CompilerInfo> compiler_extensions = new Dictionary<string, CompilerInfo>(4, StringComparer.OrdinalIgnoreCase);
	}
}
