using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000286 RID: 646
	internal abstract class Mapping
	{
		// Token: 0x0600185D RID: 6237 RVA: 0x0000216B File Offset: 0x0000036B
		internal Mapping()
		{
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x0008E8BB File Offset: 0x0008CABB
		protected Mapping(Mapping mapping)
		{
			this.isSoap = mapping.isSoap;
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600185F RID: 6239 RVA: 0x0008E8CF File Offset: 0x0008CACF
		// (set) Token: 0x06001860 RID: 6240 RVA: 0x0008E8D7 File Offset: 0x0008CAD7
		internal bool IsSoap
		{
			get
			{
				return this.isSoap;
			}
			set
			{
				this.isSoap = value;
			}
		}

		// Token: 0x040018C1 RID: 6337
		private bool isSoap;
	}
}
