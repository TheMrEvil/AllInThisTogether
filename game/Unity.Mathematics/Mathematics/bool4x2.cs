using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200000D RID: 13
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct bool4x2 : IEquatable<bool4x2>
	{
		// Token: 0x06000A8D RID: 2701 RVA: 0x00021129 File Offset: 0x0001F329
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4x2(bool4 c0, bool4 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00021139 File Offset: 0x0001F339
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4x2(bool m00, bool m01, bool m10, bool m11, bool m20, bool m21, bool m30, bool m31)
		{
			this.c0 = new bool4(m00, m10, m20, m30);
			this.c1 = new bool4(m01, m11, m21, m31);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0002115E File Offset: 0x0001F35E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4x2(bool v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00021178 File Offset: 0x0001F378
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator bool4x2(bool v)
		{
			return new bool4x2(v);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00021180 File Offset: 0x0001F380
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(bool4x2 lhs, bool4x2 rhs)
		{
			return new bool4x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x000211A9 File Offset: 0x0001F3A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(bool4x2 lhs, bool rhs)
		{
			return new bool4x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x000211C8 File Offset: 0x0001F3C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(bool lhs, bool4x2 rhs)
		{
			return new bool4x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x000211E7 File Offset: 0x0001F3E7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(bool4x2 lhs, bool4x2 rhs)
		{
			return new bool4x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00021210 File Offset: 0x0001F410
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(bool4x2 lhs, bool rhs)
		{
			return new bool4x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0002122F File Offset: 0x0001F42F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(bool lhs, bool4x2 rhs)
		{
			return new bool4x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0002124E File Offset: 0x0001F44E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !(bool4x2 val)
		{
			return new bool4x2(!val.c0, !val.c1);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0002126B File Offset: 0x0001F46B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator &(bool4x2 lhs, bool4x2 rhs)
		{
			return new bool4x2(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1);
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00021294 File Offset: 0x0001F494
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator &(bool4x2 lhs, bool rhs)
		{
			return new bool4x2(lhs.c0 & rhs, lhs.c1 & rhs);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x000212B3 File Offset: 0x0001F4B3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator &(bool lhs, bool4x2 rhs)
		{
			return new bool4x2(lhs & rhs.c0, lhs & rhs.c1);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x000212D2 File Offset: 0x0001F4D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator |(bool4x2 lhs, bool4x2 rhs)
		{
			return new bool4x2(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x000212FB File Offset: 0x0001F4FB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator |(bool4x2 lhs, bool rhs)
		{
			return new bool4x2(lhs.c0 | rhs, lhs.c1 | rhs);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0002131A File Offset: 0x0001F51A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator |(bool lhs, bool4x2 rhs)
		{
			return new bool4x2(lhs | rhs.c0, lhs | rhs.c1);
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00021339 File Offset: 0x0001F539
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ^(bool4x2 lhs, bool4x2 rhs)
		{
			return new bool4x2(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00021362 File Offset: 0x0001F562
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ^(bool4x2 lhs, bool rhs)
		{
			return new bool4x2(lhs.c0 ^ rhs, lhs.c1 ^ rhs);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00021381 File Offset: 0x0001F581
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ^(bool lhs, bool4x2 rhs)
		{
			return new bool4x2(lhs ^ rhs.c0, lhs ^ rhs.c1);
		}

		// Token: 0x170001EB RID: 491
		public unsafe bool4 this[int index]
		{
			get
			{
				fixed (bool4x2* ptr = &this)
				{
					return ref *(bool4*)(ptr + (IntPtr)index * (IntPtr)sizeof(bool4) / (IntPtr)sizeof(bool4x2));
				}
			}
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x000213BB File Offset: 0x0001F5BB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(bool4x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x000213E4 File Offset: 0x0001F5E4
		public override bool Equals(object o)
		{
			if (o is bool4x2)
			{
				bool4x2 rhs = (bool4x2)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00021409 File Offset: 0x0001F609
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00021418 File Offset: 0x0001F618
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("bool4x2({0}, {1},  {2}, {3},  {4}, {5},  {6}, {7})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c0.y,
				this.c1.y,
				this.c0.z,
				this.c1.z,
				this.c0.w,
				this.c1.w
			});
		}

		// Token: 0x04000032 RID: 50
		public bool4 c0;

		// Token: 0x04000033 RID: 51
		public bool4 c1;
	}
}
