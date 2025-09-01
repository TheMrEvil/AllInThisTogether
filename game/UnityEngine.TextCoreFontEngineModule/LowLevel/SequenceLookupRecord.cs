using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200002A RID: 42
	[UsedByNativeCode]
	[Serializable]
	internal struct SequenceLookupRecord
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000051CC File Offset: 0x000033CC
		// (set) Token: 0x06000170 RID: 368 RVA: 0x000051E4 File Offset: 0x000033E4
		public uint glyphSequenceIndex
		{
			get
			{
				return this.m_GlyphSequenceIndex;
			}
			set
			{
				this.m_GlyphSequenceIndex = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000051F0 File Offset: 0x000033F0
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00005208 File Offset: 0x00003408
		public uint lookupListIndex
		{
			get
			{
				return this.m_LookupListIndex;
			}
			set
			{
				this.m_LookupListIndex = value;
			}
		}

		// Token: 0x040000D9 RID: 217
		[NativeName("glyphSequenceIndex")]
		[SerializeField]
		private uint m_GlyphSequenceIndex;

		// Token: 0x040000DA RID: 218
		[SerializeField]
		[NativeName("lookupListIndex")]
		private uint m_LookupListIndex;
	}
}
