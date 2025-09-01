using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200000B RID: 11
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct bool3x4 : IEquatable<bool3x4>
	{
		// Token: 0x060008C8 RID: 2248 RVA: 0x0001D549 File Offset: 0x0001B749
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3x4(bool3 c0, bool3 c1, bool3 c2, bool3 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0001D568 File Offset: 0x0001B768
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3x4(bool m00, bool m01, bool m02, bool m03, bool m10, bool m11, bool m12, bool m13, bool m20, bool m21, bool m22, bool m23)
		{
			this.c0 = new bool3(m00, m10, m20);
			this.c1 = new bool3(m01, m11, m21);
			this.c2 = new bool3(m02, m12, m22);
			this.c3 = new bool3(m03, m13, m23);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0001D5B6 File Offset: 0x0001B7B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3x4(bool v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0001D5E8 File Offset: 0x0001B7E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator bool3x4(bool v)
		{
			return new bool3x4(v);
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0001D5F0 File Offset: 0x0001B7F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(bool3x4 lhs, bool3x4 rhs)
		{
			return new bool3x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0001D646 File Offset: 0x0001B846
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(bool3x4 lhs, bool rhs)
		{
			return new bool3x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0001D67D File Offset: 0x0001B87D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(bool lhs, bool3x4 rhs)
		{
			return new bool3x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0001D6B4 File Offset: 0x0001B8B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(bool3x4 lhs, bool3x4 rhs)
		{
			return new bool3x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0001D70A File Offset: 0x0001B90A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(bool3x4 lhs, bool rhs)
		{
			return new bool3x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0001D741 File Offset: 0x0001B941
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(bool lhs, bool3x4 rhs)
		{
			return new bool3x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0001D778 File Offset: 0x0001B978
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !(bool3x4 val)
		{
			return new bool3x4(!val.c0, !val.c1, !val.c2, !val.c3);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0001D7AC File Offset: 0x0001B9AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator &(bool3x4 lhs, bool3x4 rhs)
		{
			return new bool3x4(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2, lhs.c3 & rhs.c3);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0001D802 File Offset: 0x0001BA02
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator &(bool3x4 lhs, bool rhs)
		{
			return new bool3x4(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs, lhs.c3 & rhs);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0001D839 File Offset: 0x0001BA39
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator &(bool lhs, bool3x4 rhs)
		{
			return new bool3x4(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2, lhs & rhs.c3);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0001D870 File Offset: 0x0001BA70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator |(bool3x4 lhs, bool3x4 rhs)
		{
			return new bool3x4(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2, lhs.c3 | rhs.c3);
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0001D8C6 File Offset: 0x0001BAC6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator |(bool3x4 lhs, bool rhs)
		{
			return new bool3x4(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs, lhs.c3 | rhs);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0001D8FD File Offset: 0x0001BAFD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator |(bool lhs, bool3x4 rhs)
		{
			return new bool3x4(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2, lhs | rhs.c3);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0001D934 File Offset: 0x0001BB34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ^(bool3x4 lhs, bool3x4 rhs)
		{
			return new bool3x4(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2, lhs.c3 ^ rhs.c3);
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0001D98A File Offset: 0x0001BB8A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ^(bool3x4 lhs, bool rhs)
		{
			return new bool3x4(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs, lhs.c3 ^ rhs);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0001D9C1 File Offset: 0x0001BBC1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ^(bool lhs, bool3x4 rhs)
		{
			return new bool3x4(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2, lhs ^ rhs.c3);
		}

		// Token: 0x17000099 RID: 153
		public unsafe bool3 this[int index]
		{
			get
			{
				fixed (bool3x4* ptr = &this)
				{
					return ref *(bool3*)(ptr + (IntPtr)index * (IntPtr)sizeof(bool3) / (IntPtr)sizeof(bool3x4));
				}
			}
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0001DA14 File Offset: 0x0001BC14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(bool3x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0001DA70 File Offset: 0x0001BC70
		public override bool Equals(object o)
		{
			if (o is bool3x4)
			{
				bool3x4 rhs = (bool3x4)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0001DA95 File Offset: 0x0001BC95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0001DAA4 File Offset: 0x0001BCA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("bool3x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c3.x,
				this.c0.y,
				this.c1.y,
				this.c2.y,
				this.c3.y,
				this.c0.z,
				this.c1.z,
				this.c2.z,
				this.c3.z
			});
		}

		// Token: 0x0400002A RID: 42
		public bool3 c0;

		// Token: 0x0400002B RID: 43
		public bool3 c1;

		// Token: 0x0400002C RID: 44
		public bool3 c2;

		// Token: 0x0400002D RID: 45
		public bool3 c3;
	}
}
