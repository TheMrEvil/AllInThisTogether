using System;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200001E RID: 30
	[UsedByNativeCode]
	[Serializable]
	public struct GlyphValueRecord : IEquatable<GlyphValueRecord>
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00004A18 File Offset: 0x00002C18
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00004A30 File Offset: 0x00002C30
		public float xPlacement
		{
			get
			{
				return this.m_XPlacement;
			}
			set
			{
				this.m_XPlacement = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00004A3C File Offset: 0x00002C3C
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00004A54 File Offset: 0x00002C54
		public float yPlacement
		{
			get
			{
				return this.m_YPlacement;
			}
			set
			{
				this.m_YPlacement = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00004A60 File Offset: 0x00002C60
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00004A78 File Offset: 0x00002C78
		public float xAdvance
		{
			get
			{
				return this.m_XAdvance;
			}
			set
			{
				this.m_XAdvance = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00004A84 File Offset: 0x00002C84
		// (set) Token: 0x06000125 RID: 293 RVA: 0x00004A9C File Offset: 0x00002C9C
		public float yAdvance
		{
			get
			{
				return this.m_YAdvance;
			}
			set
			{
				this.m_YAdvance = value;
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004AA6 File Offset: 0x00002CA6
		public GlyphValueRecord(float xPlacement, float yPlacement, float xAdvance, float yAdvance)
		{
			this.m_XPlacement = xPlacement;
			this.m_YPlacement = yPlacement;
			this.m_XAdvance = xAdvance;
			this.m_YAdvance = yAdvance;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004AC8 File Offset: 0x00002CC8
		public static GlyphValueRecord operator +(GlyphValueRecord a, GlyphValueRecord b)
		{
			GlyphValueRecord result;
			result.m_XPlacement = a.xPlacement + b.xPlacement;
			result.m_YPlacement = a.yPlacement + b.yPlacement;
			result.m_XAdvance = a.xAdvance + b.xAdvance;
			result.m_YAdvance = a.yAdvance + b.yAdvance;
			return result;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004B34 File Offset: 0x00002D34
		[ExcludeFromDocs]
		public static GlyphValueRecord operator *(GlyphValueRecord a, float emScale)
		{
			a.m_XPlacement = a.xPlacement * emScale;
			a.m_YPlacement = a.yPlacement * emScale;
			a.m_XAdvance = a.xAdvance * emScale;
			a.m_YAdvance = a.yAdvance * emScale;
			return a;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00004B88 File Offset: 0x00002D88
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004BAC File Offset: 0x00002DAC
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004BD0 File Offset: 0x00002DD0
		public bool Equals(GlyphValueRecord other)
		{
			return base.Equals(other);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004BF8 File Offset: 0x00002DF8
		public static bool operator ==(GlyphValueRecord lhs, GlyphValueRecord rhs)
		{
			return lhs.m_XPlacement == rhs.m_XPlacement && lhs.m_YPlacement == rhs.m_YPlacement && lhs.m_XAdvance == rhs.m_XAdvance && lhs.m_YAdvance == rhs.m_YAdvance;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004C48 File Offset: 0x00002E48
		public static bool operator !=(GlyphValueRecord lhs, GlyphValueRecord rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x040000BB RID: 187
		[SerializeField]
		[NativeName("xPlacement")]
		private float m_XPlacement;

		// Token: 0x040000BC RID: 188
		[SerializeField]
		[NativeName("yPlacement")]
		private float m_YPlacement;

		// Token: 0x040000BD RID: 189
		[SerializeField]
		[NativeName("xAdvance")]
		private float m_XAdvance;

		// Token: 0x040000BE RID: 190
		[NativeName("yAdvance")]
		[SerializeField]
		private float m_YAdvance;
	}
}
