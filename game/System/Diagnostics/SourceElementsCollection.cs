using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x0200021F RID: 543
	[ConfigurationCollection(typeof(SourceElement), AddItemName = "source", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	internal class SourceElementsCollection : ConfigurationElementCollection
	{
		// Token: 0x1700028C RID: 652
		public SourceElement this[string name]
		{
			get
			{
				return (SourceElement)base.BaseGet(name);
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x000460F7 File Offset: 0x000442F7
		protected override string ElementName
		{
			get
			{
				return "source";
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x00003062 File Offset: 0x00001262
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x000460FE File Offset: 0x000442FE
		protected override ConfigurationElement CreateNewElement()
		{
			SourceElement sourceElement = new SourceElement();
			sourceElement.Listeners.InitializeDefaultInternal();
			return sourceElement;
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x00046110 File Offset: 0x00044310
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SourceElement)element).Name;
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x000316D3 File Offset: 0x0002F8D3
		public SourceElementsCollection()
		{
		}
	}
}
