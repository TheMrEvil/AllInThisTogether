using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000029 RID: 41
	[UsedByNativeCode]
	[Serializable]
	internal struct GlyphIDSequence
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600016D RID: 365 RVA: 0x000051A8 File Offset: 0x000033A8
		// (set) Token: 0x0600016E RID: 366 RVA: 0x000051C0 File Offset: 0x000033C0
		public uint[] glyphIDs
		{
			get
			{
				return this.m_GlyphIDs;
			}
			set
			{
				this.m_GlyphIDs = value;
			}
		}

		// Token: 0x040000D8 RID: 216
		[SerializeField]
		[NativeName("glyphIDs")]
		private uint[] m_GlyphIDs;
	}
}
