using System;

namespace System.Xml.Schema
{
	// Token: 0x020004F0 RID: 1264
	internal class KSStruct
	{
		// Token: 0x060033D7 RID: 13271 RVA: 0x00126B63 File Offset: 0x00124D63
		public KSStruct(KeySequence ks, int dim)
		{
			this.ks = ks;
			this.fields = new LocatedActiveAxis[dim];
		}

		// Token: 0x040026B9 RID: 9913
		public int depth;

		// Token: 0x040026BA RID: 9914
		public KeySequence ks;

		// Token: 0x040026BB RID: 9915
		public LocatedActiveAxis[] fields;
	}
}
