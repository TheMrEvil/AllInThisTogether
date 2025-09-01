using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200000F RID: 15
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct bool4x4 : IEquatable<bool4x4>
	{
		// Token: 0x06000ABF RID: 2751 RVA: 0x000219D5 File Offset: 0x0001FBD5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4x4(bool4 c0, bool4 c1, bool4 c2, bool4 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x000219F4 File Offset: 0x0001FBF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4x4(bool m00, bool m01, bool m02, bool m03, bool m10, bool m11, bool m12, bool m13, bool m20, bool m21, bool m22, bool m23, bool m30, bool m31, bool m32, bool m33)
		{
			this.c0 = new bool4(m00, m10, m20, m30);
			this.c1 = new bool4(m01, m11, m21, m31);
			this.c2 = new bool4(m02, m12, m22, m32);
			this.c3 = new bool4(m03, m13, m23, m33);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00021A4A File Offset: 0x0001FC4A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4x4(bool v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00021A7C File Offset: 0x0001FC7C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator bool4x4(bool v)
		{
			return new bool4x4(v);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00021A84 File Offset: 0x0001FC84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(bool4x4 lhs, bool4x4 rhs)
		{
			return new bool4x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00021ADA File Offset: 0x0001FCDA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(bool4x4 lhs, bool rhs)
		{
			return new bool4x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00021B11 File Offset: 0x0001FD11
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(bool lhs, bool4x4 rhs)
		{
			return new bool4x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00021B48 File Offset: 0x0001FD48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(bool4x4 lhs, bool4x4 rhs)
		{
			return new bool4x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00021B9E File Offset: 0x0001FD9E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(bool4x4 lhs, bool rhs)
		{
			return new bool4x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00021BD5 File Offset: 0x0001FDD5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(bool lhs, bool4x4 rhs)
		{
			return new bool4x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00021C0C File Offset: 0x0001FE0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !(bool4x4 val)
		{
			return new bool4x4(!val.c0, !val.c1, !val.c2, !val.c3);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00021C40 File Offset: 0x0001FE40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator &(bool4x4 lhs, bool4x4 rhs)
		{
			return new bool4x4(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2, lhs.c3 & rhs.c3);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00021C96 File Offset: 0x0001FE96
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator &(bool4x4 lhs, bool rhs)
		{
			return new bool4x4(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs, lhs.c3 & rhs);
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00021CCD File Offset: 0x0001FECD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator &(bool lhs, bool4x4 rhs)
		{
			return new bool4x4(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2, lhs & rhs.c3);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00021D04 File Offset: 0x0001FF04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator |(bool4x4 lhs, bool4x4 rhs)
		{
			return new bool4x4(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2, lhs.c3 | rhs.c3);
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00021D5A File Offset: 0x0001FF5A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator |(bool4x4 lhs, bool rhs)
		{
			return new bool4x4(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs, lhs.c3 | rhs);
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00021D91 File Offset: 0x0001FF91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator |(bool lhs, bool4x4 rhs)
		{
			return new bool4x4(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2, lhs | rhs.c3);
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00021DC8 File Offset: 0x0001FFC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ^(bool4x4 lhs, bool4x4 rhs)
		{
			return new bool4x4(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2, lhs.c3 ^ rhs.c3);
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00021E1E File Offset: 0x0002001E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ^(bool4x4 lhs, bool rhs)
		{
			return new bool4x4(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs, lhs.c3 ^ rhs);
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00021E55 File Offset: 0x00020055
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ^(bool lhs, bool4x4 rhs)
		{
			return new bool4x4(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2, lhs ^ rhs.c3);
		}

		// Token: 0x170001ED RID: 493
		public unsafe bool4 this[int index]
		{
			get
			{
				fixed (bool4x4* ptr = &this)
				{
					return ref *(bool4*)(ptr + (IntPtr)index * (IntPtr)sizeof(bool4) / (IntPtr)sizeof(bool4x4));
				}
			}
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00021EA8 File Offset: 0x000200A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(bool4x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00021F04 File Offset: 0x00020104
		public override bool Equals(object o)
		{
			if (o is bool4x4)
			{
				bool4x4 rhs = (bool4x4)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00021F29 File Offset: 0x00020129
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00021F38 File Offset: 0x00020138
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("bool4x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11},  {12}, {13}, {14}, {15})", new object[]
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
				this.c3.z,
				this.c0.w,
				this.c1.w,
				this.c2.w,
				this.c3.w
			});
		}

		// Token: 0x04000037 RID: 55
		public bool4 c0;

		// Token: 0x04000038 RID: 56
		public bool4 c1;

		// Token: 0x04000039 RID: 57
		public bool4 c2;

		// Token: 0x0400003A RID: 58
		public bool4 c3;
	}
}
