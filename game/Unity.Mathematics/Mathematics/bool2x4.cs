using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000007 RID: 7
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct bool2x4 : IEquatable<bool2x4>
	{
		// Token: 0x060007E0 RID: 2016 RVA: 0x0001B4A3 File Offset: 0x000196A3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool2x4(bool2 c0, bool2 c1, bool2 c2, bool2 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001B4C2 File Offset: 0x000196C2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool2x4(bool m00, bool m01, bool m02, bool m03, bool m10, bool m11, bool m12, bool m13)
		{
			this.c0 = new bool2(m00, m10);
			this.c1 = new bool2(m01, m11);
			this.c2 = new bool2(m02, m12);
			this.c3 = new bool2(m03, m13);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0001B4FD File Offset: 0x000196FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool2x4(bool v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0001B52F File Offset: 0x0001972F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator bool2x4(bool v)
		{
			return new bool2x4(v);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0001B538 File Offset: 0x00019738
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ==(bool2x4 lhs, bool2x4 rhs)
		{
			return new bool2x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0001B58E File Offset: 0x0001978E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ==(bool2x4 lhs, bool rhs)
		{
			return new bool2x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0001B5C5 File Offset: 0x000197C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ==(bool lhs, bool2x4 rhs)
		{
			return new bool2x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0001B5FC File Offset: 0x000197FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator !=(bool2x4 lhs, bool2x4 rhs)
		{
			return new bool2x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0001B652 File Offset: 0x00019852
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator !=(bool2x4 lhs, bool rhs)
		{
			return new bool2x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0001B689 File Offset: 0x00019889
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator !=(bool lhs, bool2x4 rhs)
		{
			return new bool2x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001B6C0 File Offset: 0x000198C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator !(bool2x4 val)
		{
			return new bool2x4(!val.c0, !val.c1, !val.c2, !val.c3);
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0001B6F4 File Offset: 0x000198F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator &(bool2x4 lhs, bool2x4 rhs)
		{
			return new bool2x4(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2, lhs.c3 & rhs.c3);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0001B74A File Offset: 0x0001994A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator &(bool2x4 lhs, bool rhs)
		{
			return new bool2x4(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs, lhs.c3 & rhs);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0001B781 File Offset: 0x00019981
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator &(bool lhs, bool2x4 rhs)
		{
			return new bool2x4(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2, lhs & rhs.c3);
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0001B7B8 File Offset: 0x000199B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator |(bool2x4 lhs, bool2x4 rhs)
		{
			return new bool2x4(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2, lhs.c3 | rhs.c3);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0001B80E File Offset: 0x00019A0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator |(bool2x4 lhs, bool rhs)
		{
			return new bool2x4(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs, lhs.c3 | rhs);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0001B845 File Offset: 0x00019A45
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator |(bool lhs, bool2x4 rhs)
		{
			return new bool2x4(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2, lhs | rhs.c3);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0001B87C File Offset: 0x00019A7C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ^(bool2x4 lhs, bool2x4 rhs)
		{
			return new bool2x4(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2, lhs.c3 ^ rhs.c3);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0001B8D2 File Offset: 0x00019AD2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ^(bool2x4 lhs, bool rhs)
		{
			return new bool2x4(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs, lhs.c3 ^ rhs);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0001B909 File Offset: 0x00019B09
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ^(bool lhs, bool2x4 rhs)
		{
			return new bool2x4(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2, lhs ^ rhs.c3);
		}

		// Token: 0x17000020 RID: 32
		public unsafe bool2 this[int index]
		{
			get
			{
				fixed (bool2x4* ptr = &this)
				{
					return ref *(bool2*)(ptr + (IntPtr)index * (IntPtr)sizeof(bool2) / (IntPtr)sizeof(bool2x4));
				}
			}
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001B95C File Offset: 0x00019B5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(bool2x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001B9B8 File Offset: 0x00019BB8
		public override bool Equals(object o)
		{
			if (o is bool2x4)
			{
				bool2x4 rhs = (bool2x4)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0001B9DD File Offset: 0x00019BDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0001B9EC File Offset: 0x00019BEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("bool2x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c3.x,
				this.c0.y,
				this.c1.y,
				this.c2.y,
				this.c3.y
			});
		}

		// Token: 0x0400001E RID: 30
		public bool2 c0;

		// Token: 0x0400001F RID: 31
		public bool2 c1;

		// Token: 0x04000020 RID: 32
		public bool2 c2;

		// Token: 0x04000021 RID: 33
		public bool2 c3;
	}
}
