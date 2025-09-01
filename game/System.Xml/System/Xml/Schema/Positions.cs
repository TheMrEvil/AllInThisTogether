using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020004F7 RID: 1271
	internal class Positions
	{
		// Token: 0x06003405 RID: 13317 RVA: 0x00127660 File Offset: 0x00125860
		public int Add(int symbol, object particle)
		{
			return this.positions.Add(new Position(symbol, particle));
		}

		// Token: 0x1700093B RID: 2363
		public Position this[int pos]
		{
			get
			{
				return (Position)this.positions[pos];
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06003407 RID: 13319 RVA: 0x0012768C File Offset: 0x0012588C
		public int Count
		{
			get
			{
				return this.positions.Count;
			}
		}

		// Token: 0x06003408 RID: 13320 RVA: 0x00127699 File Offset: 0x00125899
		public Positions()
		{
		}

		// Token: 0x040026D3 RID: 9939
		private ArrayList positions = new ArrayList();
	}
}
