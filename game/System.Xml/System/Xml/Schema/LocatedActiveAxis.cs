using System;

namespace System.Xml.Schema
{
	// Token: 0x020004EE RID: 1262
	internal class LocatedActiveAxis : ActiveAxis
	{
		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x060033CE RID: 13262 RVA: 0x0012697E File Offset: 0x00124B7E
		internal int Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x060033CF RID: 13263 RVA: 0x00126986 File Offset: 0x00124B86
		internal LocatedActiveAxis(Asttree astfield, KeySequence ks, int column) : base(astfield)
		{
			this.Ks = ks;
			this.column = column;
			this.isMatched = false;
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x001269A4 File Offset: 0x00124BA4
		internal void Reactivate(KeySequence ks)
		{
			base.Reactivate();
			this.Ks = ks;
		}

		// Token: 0x040026B3 RID: 9907
		private int column;

		// Token: 0x040026B4 RID: 9908
		internal bool isMatched;

		// Token: 0x040026B5 RID: 9909
		internal KeySequence Ks;
	}
}
