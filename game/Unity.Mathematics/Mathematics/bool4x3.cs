using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200000E RID: 14
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct bool4x3 : IEquatable<bool4x3>
	{
		// Token: 0x06000AA6 RID: 2726 RVA: 0x000214CD File Offset: 0x0001F6CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4x3(bool4 c0, bool4 c1, bool4 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x000214E4 File Offset: 0x0001F6E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4x3(bool m00, bool m01, bool m02, bool m10, bool m11, bool m12, bool m20, bool m21, bool m22, bool m30, bool m31, bool m32)
		{
			this.c0 = new bool4(m00, m10, m20, m30);
			this.c1 = new bool4(m01, m11, m21, m31);
			this.c2 = new bool4(m02, m12, m22, m32);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0002151C File Offset: 0x0001F71C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4x3(bool v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00021542 File Offset: 0x0001F742
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator bool4x3(bool v)
		{
			return new bool4x3(v);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0002154A File Offset: 0x0001F74A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(bool4x3 lhs, bool4x3 rhs)
		{
			return new bool4x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00021584 File Offset: 0x0001F784
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(bool4x3 lhs, bool rhs)
		{
			return new bool4x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x000215AF File Offset: 0x0001F7AF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(bool lhs, bool4x3 rhs)
		{
			return new bool4x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x000215DA File Offset: 0x0001F7DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(bool4x3 lhs, bool4x3 rhs)
		{
			return new bool4x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00021614 File Offset: 0x0001F814
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(bool4x3 lhs, bool rhs)
		{
			return new bool4x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0002163F File Offset: 0x0001F83F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(bool lhs, bool4x3 rhs)
		{
			return new bool4x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0002166A File Offset: 0x0001F86A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !(bool4x3 val)
		{
			return new bool4x3(!val.c0, !val.c1, !val.c2);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00021692 File Offset: 0x0001F892
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator &(bool4x3 lhs, bool4x3 rhs)
		{
			return new bool4x3(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x000216CC File Offset: 0x0001F8CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator &(bool4x3 lhs, bool rhs)
		{
			return new bool4x3(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs);
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x000216F7 File Offset: 0x0001F8F7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator &(bool lhs, bool4x3 rhs)
		{
			return new bool4x3(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00021722 File Offset: 0x0001F922
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator |(bool4x3 lhs, bool4x3 rhs)
		{
			return new bool4x3(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0002175C File Offset: 0x0001F95C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator |(bool4x3 lhs, bool rhs)
		{
			return new bool4x3(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00021787 File Offset: 0x0001F987
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator |(bool lhs, bool4x3 rhs)
		{
			return new bool4x3(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2);
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x000217B2 File Offset: 0x0001F9B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ^(bool4x3 lhs, bool4x3 rhs)
		{
			return new bool4x3(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x000217EC File Offset: 0x0001F9EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ^(bool4x3 lhs, bool rhs)
		{
			return new bool4x3(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00021817 File Offset: 0x0001FA17
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ^(bool lhs, bool4x3 rhs)
		{
			return new bool4x3(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2);
		}

		// Token: 0x170001EC RID: 492
		public unsafe bool4 this[int index]
		{
			get
			{
				fixed (bool4x3* ptr = &this)
				{
					return ref *(bool4*)(ptr + (IntPtr)index * (IntPtr)sizeof(bool4) / (IntPtr)sizeof(bool4x3));
				}
			}
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0002185F File Offset: 0x0001FA5F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(bool4x3 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2);
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0002189C File Offset: 0x0001FA9C
		public override bool Equals(object o)
		{
			if (o is bool4x3)
			{
				bool4x3 rhs = (bool4x3)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x000218C1 File Offset: 0x0001FAC1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x000218D0 File Offset: 0x0001FAD0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("bool4x3({0}, {1}, {2},  {3}, {4}, {5},  {6}, {7}, {8},  {9}, {10}, {11})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c0.y,
				this.c1.y,
				this.c2.y,
				this.c0.z,
				this.c1.z,
				this.c2.z,
				this.c0.w,
				this.c1.w,
				this.c2.w
			});
		}

		// Token: 0x04000034 RID: 52
		public bool4 c0;

		// Token: 0x04000035 RID: 53
		public bool4 c1;

		// Token: 0x04000036 RID: 54
		public bool4 c2;
	}
}
