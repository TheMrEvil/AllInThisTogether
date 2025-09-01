using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x02000226 RID: 550
	[ConfigurationCollection(typeof(SwitchElement))]
	internal class SwitchElementsCollection : ConfigurationElementCollection
	{
		// Token: 0x170002A1 RID: 673
		public SwitchElement this[string name]
		{
			get
			{
				return (SwitchElement)base.BaseGet(name);
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06001005 RID: 4101 RVA: 0x0000390E File Offset: 0x00001B0E
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.AddRemoveClearMap;
			}
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x00046BF4 File Offset: 0x00044DF4
		protected override ConfigurationElement CreateNewElement()
		{
			return new SwitchElement();
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x00046BFB File Offset: 0x00044DFB
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SwitchElement)element).Name;
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x000316D3 File Offset: 0x0002F8D3
		public SwitchElementsCollection()
		{
		}
	}
}
