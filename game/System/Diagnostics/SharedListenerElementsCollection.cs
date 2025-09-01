using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x0200021B RID: 539
	[ConfigurationCollection(typeof(ListenerElement), AddItemName = "add", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	internal class SharedListenerElementsCollection : ListenerElementsCollection
	{
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000FA8 RID: 4008 RVA: 0x00003062 File Offset: 0x00001262
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0004592C File Offset: 0x00043B2C
		protected override ConfigurationElement CreateNewElement()
		{
			return new ListenerElement(false);
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x00045934 File Offset: 0x00043B34
		protected override string ElementName
		{
			get
			{
				return "add";
			}
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0004593B File Offset: 0x00043B3B
		public SharedListenerElementsCollection()
		{
		}
	}
}
