using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200001D RID: 29
	[Serializable]
	internal abstract class OpenTypeLayoutLookup
	{
		// Token: 0x06000117 RID: 279
		public abstract void InitializeLookupDictionary();

		// Token: 0x06000118 RID: 280 RVA: 0x00004A09 File Offset: 0x00002C09
		public virtual void UpdateRecords(int lookupIndex, uint glyphIndex)
		{
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004A09 File Offset: 0x00002C09
		public virtual void UpdateRecords(int lookupIndex, uint glyphIndex, float emScale)
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004A09 File Offset: 0x00002C09
		public virtual void UpdateRecords(int lookupIndex, List<uint> glyphIndexes)
		{
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004A09 File Offset: 0x00002C09
		public virtual void UpdateRecords(int lookupIndex, List<uint> glyphIndexes, float emScale)
		{
		}

		// Token: 0x0600011C RID: 284
		public abstract void ClearRecords();

		// Token: 0x0600011D RID: 285 RVA: 0x00004A0C File Offset: 0x00002C0C
		protected OpenTypeLayoutLookup()
		{
		}

		// Token: 0x040000B8 RID: 184
		public uint lookupType;

		// Token: 0x040000B9 RID: 185
		public uint lookupFlag;

		// Token: 0x040000BA RID: 186
		public uint markFilteringSet;
	}
}
