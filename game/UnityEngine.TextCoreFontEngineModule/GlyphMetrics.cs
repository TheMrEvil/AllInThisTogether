using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore
{
	// Token: 0x02000005 RID: 5
	[UsedByNativeCode]
	[Serializable]
	public struct GlyphMetrics : IEquatable<GlyphMetrics>
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000027CC File Offset: 0x000009CC
		// (set) Token: 0x0600003F RID: 63 RVA: 0x000027E4 File Offset: 0x000009E4
		public float width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				this.m_Width = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000027F0 File Offset: 0x000009F0
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002808 File Offset: 0x00000A08
		public float height
		{
			get
			{
				return this.m_Height;
			}
			set
			{
				this.m_Height = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002814 File Offset: 0x00000A14
		// (set) Token: 0x06000043 RID: 67 RVA: 0x0000282C File Offset: 0x00000A2C
		public float horizontalBearingX
		{
			get
			{
				return this.m_HorizontalBearingX;
			}
			set
			{
				this.m_HorizontalBearingX = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002838 File Offset: 0x00000A38
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002850 File Offset: 0x00000A50
		public float horizontalBearingY
		{
			get
			{
				return this.m_HorizontalBearingY;
			}
			set
			{
				this.m_HorizontalBearingY = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000285C File Offset: 0x00000A5C
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002874 File Offset: 0x00000A74
		public float horizontalAdvance
		{
			get
			{
				return this.m_HorizontalAdvance;
			}
			set
			{
				this.m_HorizontalAdvance = value;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000287E File Offset: 0x00000A7E
		public GlyphMetrics(float width, float height, float bearingX, float bearingY, float advance)
		{
			this.m_Width = width;
			this.m_Height = height;
			this.m_HorizontalBearingX = bearingX;
			this.m_HorizontalBearingY = bearingY;
			this.m_HorizontalAdvance = advance;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000028A8 File Offset: 0x00000AA8
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000028CC File Offset: 0x00000ACC
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000028F0 File Offset: 0x00000AF0
		public bool Equals(GlyphMetrics other)
		{
			return base.Equals(other);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002918 File Offset: 0x00000B18
		public static bool operator ==(GlyphMetrics lhs, GlyphMetrics rhs)
		{
			return lhs.width == rhs.width && lhs.height == rhs.height && lhs.horizontalBearingX == rhs.horizontalBearingX && lhs.horizontalBearingY == rhs.horizontalBearingY && lhs.horizontalAdvance == rhs.horizontalAdvance;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002980 File Offset: 0x00000B80
		public static bool operator !=(GlyphMetrics lhs, GlyphMetrics rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x04000021 RID: 33
		[SerializeField]
		[NativeName("width")]
		private float m_Width;

		// Token: 0x04000022 RID: 34
		[SerializeField]
		[NativeName("height")]
		private float m_Height;

		// Token: 0x04000023 RID: 35
		[SerializeField]
		[NativeName("horizontalBearingX")]
		private float m_HorizontalBearingX;

		// Token: 0x04000024 RID: 36
		[SerializeField]
		[NativeName("horizontalBearingY")]
		private float m_HorizontalBearingY;

		// Token: 0x04000025 RID: 37
		[NativeName("horizontalAdvance")]
		[SerializeField]
		private float m_HorizontalAdvance;
	}
}
