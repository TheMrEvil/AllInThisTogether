using System;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200001F RID: 31
	[UsedByNativeCode]
	[Serializable]
	public struct GlyphAdjustmentRecord : IEquatable<GlyphAdjustmentRecord>
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00004C64 File Offset: 0x00002E64
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00004C7C File Offset: 0x00002E7C
		public uint glyphIndex
		{
			get
			{
				return this.m_GlyphIndex;
			}
			set
			{
				this.m_GlyphIndex = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00004C88 File Offset: 0x00002E88
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00004CA0 File Offset: 0x00002EA0
		public GlyphValueRecord glyphValueRecord
		{
			get
			{
				return this.m_GlyphValueRecord;
			}
			set
			{
				this.m_GlyphValueRecord = value;
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004CAA File Offset: 0x00002EAA
		public GlyphAdjustmentRecord(uint glyphIndex, GlyphValueRecord glyphValueRecord)
		{
			this.m_GlyphIndex = glyphIndex;
			this.m_GlyphValueRecord = glyphValueRecord;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004CBC File Offset: 0x00002EBC
		[ExcludeFromDocs]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004CE0 File Offset: 0x00002EE0
		[ExcludeFromDocs]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004D04 File Offset: 0x00002F04
		[ExcludeFromDocs]
		public bool Equals(GlyphAdjustmentRecord other)
		{
			return base.Equals(other);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004D2C File Offset: 0x00002F2C
		[ExcludeFromDocs]
		public static bool operator ==(GlyphAdjustmentRecord lhs, GlyphAdjustmentRecord rhs)
		{
			return lhs.m_GlyphIndex == rhs.m_GlyphIndex && lhs.m_GlyphValueRecord == rhs.m_GlyphValueRecord;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004D60 File Offset: 0x00002F60
		[ExcludeFromDocs]
		public static bool operator !=(GlyphAdjustmentRecord lhs, GlyphAdjustmentRecord rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x040000BF RID: 191
		[SerializeField]
		[NativeName("glyphIndex")]
		private uint m_GlyphIndex;

		// Token: 0x040000C0 RID: 192
		[SerializeField]
		[NativeName("glyphValueRecord")]
		private GlyphValueRecord m_GlyphValueRecord;
	}
}
