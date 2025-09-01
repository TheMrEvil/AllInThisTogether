using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200000A RID: 10
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct bool3x3 : IEquatable<bool3x3>
	{
		// Token: 0x060008AF RID: 2223 RVA: 0x0001D083 File Offset: 0x0001B283
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3x3(bool3 c0, bool3 c1, bool3 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0001D09A File Offset: 0x0001B29A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3x3(bool m00, bool m01, bool m02, bool m10, bool m11, bool m12, bool m20, bool m21, bool m22)
		{
			this.c0 = new bool3(m00, m10, m20);
			this.c1 = new bool3(m01, m11, m21);
			this.c2 = new bool3(m02, m12, m22);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0001D0CC File Offset: 0x0001B2CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3x3(bool v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0001D0F2 File Offset: 0x0001B2F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator bool3x3(bool v)
		{
			return new bool3x3(v);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0001D0FA File Offset: 0x0001B2FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ==(bool3x3 lhs, bool3x3 rhs)
		{
			return new bool3x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0001D134 File Offset: 0x0001B334
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ==(bool3x3 lhs, bool rhs)
		{
			return new bool3x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0001D15F File Offset: 0x0001B35F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ==(bool lhs, bool3x3 rhs)
		{
			return new bool3x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0001D18A File Offset: 0x0001B38A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator !=(bool3x3 lhs, bool3x3 rhs)
		{
			return new bool3x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0001D1C4 File Offset: 0x0001B3C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator !=(bool3x3 lhs, bool rhs)
		{
			return new bool3x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0001D1EF File Offset: 0x0001B3EF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator !=(bool lhs, bool3x3 rhs)
		{
			return new bool3x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0001D21A File Offset: 0x0001B41A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator !(bool3x3 val)
		{
			return new bool3x3(!val.c0, !val.c1, !val.c2);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0001D242 File Offset: 0x0001B442
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator &(bool3x3 lhs, bool3x3 rhs)
		{
			return new bool3x3(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0001D27C File Offset: 0x0001B47C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator &(bool3x3 lhs, bool rhs)
		{
			return new bool3x3(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0001D2A7 File Offset: 0x0001B4A7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator &(bool lhs, bool3x3 rhs)
		{
			return new bool3x3(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0001D2D2 File Offset: 0x0001B4D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator |(bool3x3 lhs, bool3x3 rhs)
		{
			return new bool3x3(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0001D30C File Offset: 0x0001B50C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator |(bool3x3 lhs, bool rhs)
		{
			return new bool3x3(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs);
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0001D337 File Offset: 0x0001B537
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator |(bool lhs, bool3x3 rhs)
		{
			return new bool3x3(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0001D362 File Offset: 0x0001B562
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ^(bool3x3 lhs, bool3x3 rhs)
		{
			return new bool3x3(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0001D39C File Offset: 0x0001B59C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ^(bool3x3 lhs, bool rhs)
		{
			return new bool3x3(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0001D3C7 File Offset: 0x0001B5C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ^(bool lhs, bool3x3 rhs)
		{
			return new bool3x3(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2);
		}

		// Token: 0x17000098 RID: 152
		public unsafe bool3 this[int index]
		{
			get
			{
				fixed (bool3x3* ptr = &this)
				{
					return ref *(bool3*)(ptr + (IntPtr)index * (IntPtr)sizeof(bool3) / (IntPtr)sizeof(bool3x3));
				}
			}
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0001D40F File Offset: 0x0001B60F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(bool3x3 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2);
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0001D44C File Offset: 0x0001B64C
		public override bool Equals(object o)
		{
			if (o is bool3x3)
			{
				bool3x3 rhs = (bool3x3)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0001D471 File Offset: 0x0001B671
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0001D480 File Offset: 0x0001B680
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("bool3x3({0}, {1}, {2},  {3}, {4}, {5},  {6}, {7}, {8})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c0.y,
				this.c1.y,
				this.c2.y,
				this.c0.z,
				this.c1.z,
				this.c2.z
			});
		}

		// Token: 0x04000027 RID: 39
		public bool3 c0;

		// Token: 0x04000028 RID: 40
		public bool3 c1;

		// Token: 0x04000029 RID: 41
		public bool3 c2;
	}
}
