using System;
using System.Collections.Generic;
using System.Configuration;

namespace System.CodeDom.Compiler
{
	// Token: 0x0200035B RID: 859
	[ConfigurationCollection(typeof(CompilerProviderOption), CollectionType = ConfigurationElementCollectionType.BasicMap, AddItemName = "providerOption")]
	internal sealed class CompilerProviderOptionsCollection : ConfigurationElementCollection
	{
		// Token: 0x06001C7F RID: 7295 RVA: 0x000673E6 File Offset: 0x000655E6
		static CompilerProviderOptionsCollection()
		{
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x000316D3 File Offset: 0x0002F8D3
		public CompilerProviderOptionsCollection()
		{
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x000673F2 File Offset: 0x000655F2
		protected override ConfigurationElement CreateNewElement()
		{
			return new CompilerProviderOption();
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x000673F9 File Offset: 0x000655F9
		public CompilerProviderOption Get(int index)
		{
			return (CompilerProviderOption)base.BaseGet(index);
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x00067407 File Offset: 0x00065607
		public CompilerProviderOption Get(string name)
		{
			return (CompilerProviderOption)base.BaseGet(name);
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x00067415 File Offset: 0x00065615
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((CompilerProviderOption)element).Name;
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x000672AE File Offset: 0x000654AE
		public string GetKey(int index)
		{
			return (string)base.BaseGetKey(index);
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001C86 RID: 7302 RVA: 0x00067424 File Offset: 0x00065624
		public string[] AllKeys
		{
			get
			{
				int count = base.Count;
				string[] array = new string[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = this[i].Name;
				}
				return array;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001C87 RID: 7303 RVA: 0x0006745B File Offset: 0x0006565B
		protected override string ElementName
		{
			get
			{
				return "providerOption";
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001C88 RID: 7304 RVA: 0x00067462 File Offset: 0x00065662
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CompilerProviderOptionsCollection.properties;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001C89 RID: 7305 RVA: 0x0006746C File Offset: 0x0006566C
		public Dictionary<string, string> ProviderOptions
		{
			get
			{
				int count = base.Count;
				if (count == 0)
				{
					return null;
				}
				Dictionary<string, string> dictionary = new Dictionary<string, string>(count);
				for (int i = 0; i < count; i++)
				{
					CompilerProviderOption compilerProviderOption = this[i];
					dictionary.Add(compilerProviderOption.Name, compilerProviderOption.Value);
				}
				return dictionary;
			}
		}

		// Token: 0x170005C4 RID: 1476
		public CompilerProviderOption this[int index]
		{
			get
			{
				return (CompilerProviderOption)base.BaseGet(index);
			}
		}

		// Token: 0x170005C5 RID: 1477
		public CompilerProviderOption this[string name]
		{
			get
			{
				foreach (object obj in this)
				{
					CompilerProviderOption compilerProviderOption = (CompilerProviderOption)obj;
					if (compilerProviderOption.Name == name)
					{
						return compilerProviderOption;
					}
				}
				return null;
			}
		}

		// Token: 0x04000E87 RID: 3719
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
