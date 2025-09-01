using System;
using System.Collections.Specialized;

namespace System.Configuration
{
	// Token: 0x02000051 RID: 81
	internal class KeyValueInternalCollection : NameValueCollection
	{
		// Token: 0x060002BD RID: 701 RVA: 0x000084DF File Offset: 0x000066DF
		public void SetReadOnly()
		{
			base.IsReadOnly = true;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x000084E8 File Offset: 0x000066E8
		public override void Add(string name, string val)
		{
			this.Remove(name);
			base.Add(name, val);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00002605 File Offset: 0x00000805
		public KeyValueInternalCollection()
		{
		}
	}
}
