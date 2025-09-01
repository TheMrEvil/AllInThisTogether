using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore
{
	// Token: 0x02000004 RID: 4
	[UsedByNativeCode]
	[Serializable]
	public struct GlyphRect : IEquatable<GlyphRect>
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000025D4 File Offset: 0x000007D4
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000025EC File Offset: 0x000007EC
		public int x
		{
			get
			{
				return this.m_X;
			}
			set
			{
				this.m_X = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000025F8 File Offset: 0x000007F8
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002610 File Offset: 0x00000810
		public int y
		{
			get
			{
				return this.m_Y;
			}
			set
			{
				this.m_Y = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000261C File Offset: 0x0000081C
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002634 File Offset: 0x00000834
		public int width
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

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002640 File Offset: 0x00000840
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002658 File Offset: 0x00000858
		public int height
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

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002664 File Offset: 0x00000864
		public static GlyphRect zero
		{
			get
			{
				return GlyphRect.s_ZeroGlyphRect;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000267B File Offset: 0x0000087B
		public GlyphRect(int x, int y, int width, int height)
		{
			this.m_X = x;
			this.m_Y = y;
			this.m_Width = width;
			this.m_Height = height;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000269B File Offset: 0x0000089B
		public GlyphRect(Rect rect)
		{
			this.m_X = (int)rect.x;
			this.m_Y = (int)rect.y;
			this.m_Width = (int)rect.width;
			this.m_Height = (int)rect.height;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000026D8 File Offset: 0x000008D8
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000026FC File Offset: 0x000008FC
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002720 File Offset: 0x00000920
		public bool Equals(GlyphRect other)
		{
			return base.Equals(other);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002748 File Offset: 0x00000948
		public static bool operator ==(GlyphRect lhs, GlyphRect rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.width == rhs.width && lhs.height == rhs.height;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000027A0 File Offset: 0x000009A0
		public static bool operator !=(GlyphRect lhs, GlyphRect rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000027BC File Offset: 0x000009BC
		// Note: this type is marked as 'beforefieldinit'.
		static GlyphRect()
		{
		}

		// Token: 0x0400001C RID: 28
		[SerializeField]
		[NativeName("x")]
		private int m_X;

		// Token: 0x0400001D RID: 29
		[SerializeField]
		[NativeName("y")]
		private int m_Y;

		// Token: 0x0400001E RID: 30
		[NativeName("width")]
		[SerializeField]
		private int m_Width;

		// Token: 0x0400001F RID: 31
		[SerializeField]
		[NativeName("height")]
		private int m_Height;

		// Token: 0x04000020 RID: 32
		private static readonly GlyphRect s_ZeroGlyphRect = new GlyphRect(0, 0, 0, 0);
	}
}
