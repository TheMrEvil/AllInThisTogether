using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200028C RID: 652
	internal class ConstantMapping : Mapping
	{
		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001888 RID: 6280 RVA: 0x0008EAAD File Offset: 0x0008CCAD
		// (set) Token: 0x06001889 RID: 6281 RVA: 0x0008EAC3 File Offset: 0x0008CCC3
		internal string XmlName
		{
			get
			{
				if (this.xmlName != null)
				{
					return this.xmlName;
				}
				return string.Empty;
			}
			set
			{
				this.xmlName = value;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600188A RID: 6282 RVA: 0x0008EACC File Offset: 0x0008CCCC
		// (set) Token: 0x0600188B RID: 6283 RVA: 0x0008EAE2 File Offset: 0x0008CCE2
		internal string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x0600188C RID: 6284 RVA: 0x0008EAEB File Offset: 0x0008CCEB
		// (set) Token: 0x0600188D RID: 6285 RVA: 0x0008EAF3 File Offset: 0x0008CCF3
		internal long Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x0008EAFC File Offset: 0x0008CCFC
		public ConstantMapping()
		{
		}

		// Token: 0x040018D1 RID: 6353
		private string xmlName;

		// Token: 0x040018D2 RID: 6354
		private string name;

		// Token: 0x040018D3 RID: 6355
		private long value;
	}
}
