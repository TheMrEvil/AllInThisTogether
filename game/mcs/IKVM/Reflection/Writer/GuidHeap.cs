using System;
using System.Collections.Generic;

namespace IKVM.Reflection.Writer
{
	// Token: 0x02000082 RID: 130
	internal sealed class GuidHeap : SimpleHeap
	{
		// Token: 0x060006D4 RID: 1748 RVA: 0x00014A1C File Offset: 0x00012C1C
		internal GuidHeap()
		{
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00014A2F File Offset: 0x00012C2F
		internal int Add(Guid guid)
		{
			this.list.Add(guid);
			return this.list.Count;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00014A48 File Offset: 0x00012C48
		protected override int GetLength()
		{
			return this.list.Count * 16;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00014A58 File Offset: 0x00012C58
		protected override void WriteImpl(MetadataWriter mw)
		{
			foreach (Guid guid in this.list)
			{
				mw.Write(guid.ToByteArray());
			}
		}

		// Token: 0x0400028C RID: 652
		private List<Guid> list = new List<Guid>();
	}
}
