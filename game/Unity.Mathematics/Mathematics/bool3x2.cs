using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000009 RID: 9
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct bool3x2 : IEquatable<bool3x2>
	{
		// Token: 0x06000896 RID: 2198 RVA: 0x0001CD07 File Offset: 0x0001AF07
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3x2(bool3 c0, bool3 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0001CD17 File Offset: 0x0001AF17
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3x2(bool m00, bool m01, bool m10, bool m11, bool m20, bool m21)
		{
			this.c0 = new bool3(m00, m10, m20);
			this.c1 = new bool3(m01, m11, m21);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0001CD38 File Offset: 0x0001AF38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3x2(bool v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0001CD52 File Offset: 0x0001AF52
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator bool3x2(bool v)
		{
			return new bool3x2(v);
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0001CD5A File Offset: 0x0001AF5A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(bool3x2 lhs, bool3x2 rhs)
		{
			return new bool3x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0001CD83 File Offset: 0x0001AF83
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(bool3x2 lhs, bool rhs)
		{
			return new bool3x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0001CDA2 File Offset: 0x0001AFA2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(bool lhs, bool3x2 rhs)
		{
			return new bool3x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0001CDC1 File Offset: 0x0001AFC1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(bool3x2 lhs, bool3x2 rhs)
		{
			return new bool3x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0001CDEA File Offset: 0x0001AFEA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(bool3x2 lhs, bool rhs)
		{
			return new bool3x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0001CE09 File Offset: 0x0001B009
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(bool lhs, bool3x2 rhs)
		{
			return new bool3x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0001CE28 File Offset: 0x0001B028
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !(bool3x2 val)
		{
			return new bool3x2(!val.c0, !val.c1);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0001CE45 File Offset: 0x0001B045
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator &(bool3x2 lhs, bool3x2 rhs)
		{
			return new bool3x2(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1);
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0001CE6E File Offset: 0x0001B06E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator &(bool3x2 lhs, bool rhs)
		{
			return new bool3x2(lhs.c0 & rhs, lhs.c1 & rhs);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001CE8D File Offset: 0x0001B08D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator &(bool lhs, bool3x2 rhs)
		{
			return new bool3x2(lhs & rhs.c0, lhs & rhs.c1);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0001CEAC File Offset: 0x0001B0AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator |(bool3x2 lhs, bool3x2 rhs)
		{
			return new bool3x2(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0001CED5 File Offset: 0x0001B0D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator |(bool3x2 lhs, bool rhs)
		{
			return new bool3x2(lhs.c0 | rhs, lhs.c1 | rhs);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0001CEF4 File Offset: 0x0001B0F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator |(bool lhs, bool3x2 rhs)
		{
			return new bool3x2(lhs | rhs.c0, lhs | rhs.c1);
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0001CF13 File Offset: 0x0001B113
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ^(bool3x2 lhs, bool3x2 rhs)
		{
			return new bool3x2(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0001CF3C File Offset: 0x0001B13C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ^(bool3x2 lhs, bool rhs)
		{
			return new bool3x2(lhs.c0 ^ rhs, lhs.c1 ^ rhs);
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0001CF5B File Offset: 0x0001B15B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ^(bool lhs, bool3x2 rhs)
		{
			return new bool3x2(lhs ^ rhs.c0, lhs ^ rhs.c1);
		}

		// Token: 0x17000097 RID: 151
		public unsafe bool3 this[int index]
		{
			get
			{
				fixed (bool3x2* ptr = &this)
				{
					return ref *(bool3*)(ptr + (IntPtr)index * (IntPtr)sizeof(bool3) / (IntPtr)sizeof(bool3x2));
				}
			}
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0001CF97 File Offset: 0x0001B197
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(bool3x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0001CFC0 File Offset: 0x0001B1C0
		public override bool Equals(object o)
		{
			if (o is bool3x2)
			{
				bool3x2 rhs = (bool3x2)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0001CFE5 File Offset: 0x0001B1E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0001CFF4 File Offset: 0x0001B1F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("bool3x2({0}, {1},  {2}, {3},  {4}, {5})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c0.y,
				this.c1.y,
				this.c0.z,
				this.c1.z
			});
		}

		// Token: 0x04000025 RID: 37
		public bool3 c0;

		// Token: 0x04000026 RID: 38
		public bool3 c1;
	}
}
