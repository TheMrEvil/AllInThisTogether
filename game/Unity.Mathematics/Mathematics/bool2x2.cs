using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000005 RID: 5
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct bool2x2 : IEquatable<bool2x2>
	{
		// Token: 0x060007AE RID: 1966 RVA: 0x0001ACCA File Offset: 0x00018ECA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool2x2(bool2 c0, bool2 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0001ACDA File Offset: 0x00018EDA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool2x2(bool m00, bool m01, bool m10, bool m11)
		{
			this.c0 = new bool2(m00, m10);
			this.c1 = new bool2(m01, m11);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0001ACF7 File Offset: 0x00018EF7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool2x2(bool v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0001AD11 File Offset: 0x00018F11
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator bool2x2(bool v)
		{
			return new bool2x2(v);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001AD19 File Offset: 0x00018F19
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(bool2x2 lhs, bool2x2 rhs)
		{
			return new bool2x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0001AD42 File Offset: 0x00018F42
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(bool2x2 lhs, bool rhs)
		{
			return new bool2x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0001AD61 File Offset: 0x00018F61
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(bool lhs, bool2x2 rhs)
		{
			return new bool2x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0001AD80 File Offset: 0x00018F80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(bool2x2 lhs, bool2x2 rhs)
		{
			return new bool2x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0001ADA9 File Offset: 0x00018FA9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(bool2x2 lhs, bool rhs)
		{
			return new bool2x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0001ADC8 File Offset: 0x00018FC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(bool lhs, bool2x2 rhs)
		{
			return new bool2x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0001ADE7 File Offset: 0x00018FE7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !(bool2x2 val)
		{
			return new bool2x2(!val.c0, !val.c1);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0001AE04 File Offset: 0x00019004
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator &(bool2x2 lhs, bool2x2 rhs)
		{
			return new bool2x2(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0001AE2D File Offset: 0x0001902D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator &(bool2x2 lhs, bool rhs)
		{
			return new bool2x2(lhs.c0 & rhs, lhs.c1 & rhs);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0001AE4C File Offset: 0x0001904C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator &(bool lhs, bool2x2 rhs)
		{
			return new bool2x2(lhs & rhs.c0, lhs & rhs.c1);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0001AE6B File Offset: 0x0001906B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator |(bool2x2 lhs, bool2x2 rhs)
		{
			return new bool2x2(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1);
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0001AE94 File Offset: 0x00019094
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator |(bool2x2 lhs, bool rhs)
		{
			return new bool2x2(lhs.c0 | rhs, lhs.c1 | rhs);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0001AEB3 File Offset: 0x000190B3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator |(bool lhs, bool2x2 rhs)
		{
			return new bool2x2(lhs | rhs.c0, lhs | rhs.c1);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0001AED2 File Offset: 0x000190D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ^(bool2x2 lhs, bool2x2 rhs)
		{
			return new bool2x2(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001AEFB File Offset: 0x000190FB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ^(bool2x2 lhs, bool rhs)
		{
			return new bool2x2(lhs.c0 ^ rhs, lhs.c1 ^ rhs);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001AF1A File Offset: 0x0001911A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ^(bool lhs, bool2x2 rhs)
		{
			return new bool2x2(lhs ^ rhs.c0, lhs ^ rhs.c1);
		}

		// Token: 0x1700001E RID: 30
		public unsafe bool2 this[int index]
		{
			get
			{
				fixed (bool2x2* ptr = &this)
				{
					return ref *(bool2*)(ptr + (IntPtr)index * (IntPtr)sizeof(bool2) / (IntPtr)sizeof(bool2x2));
				}
			}
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0001AF57 File Offset: 0x00019157
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(bool2x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0001AF80 File Offset: 0x00019180
		public override bool Equals(object o)
		{
			if (o is bool2x2)
			{
				bool2x2 rhs = (bool2x2)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0001AFA5 File Offset: 0x000191A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001AFB4 File Offset: 0x000191B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("bool2x2({0}, {1},  {2}, {3})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c0.y,
				this.c1.y
			});
		}

		// Token: 0x04000019 RID: 25
		public bool2 c0;

		// Token: 0x0400001A RID: 26
		public bool2 c1;
	}
}
