using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200003F RID: 63
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct uint2x2 : IEquatable<uint2x2>, IFormattable
	{
		// Token: 0x06001EF0 RID: 7920 RVA: 0x00059456 File Offset: 0x00057656
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x2(uint2 c0, uint2 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x00059466 File Offset: 0x00057666
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x2(uint m00, uint m01, uint m10, uint m11)
		{
			this.c0 = new uint2(m00, m10);
			this.c1 = new uint2(m01, m11);
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x00059483 File Offset: 0x00057683
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x2(uint v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x0005949D File Offset: 0x0005769D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x2(bool v)
		{
			this.c0 = math.select(new uint2(0U), new uint2(1U), v);
			this.c1 = math.select(new uint2(0U), new uint2(1U), v);
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x000594CF File Offset: 0x000576CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x2(bool2x2 v)
		{
			this.c0 = math.select(new uint2(0U), new uint2(1U), v.c0);
			this.c1 = math.select(new uint2(0U), new uint2(1U), v.c1);
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x0005950B File Offset: 0x0005770B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x2(int v)
		{
			this.c0 = (uint2)v;
			this.c1 = (uint2)v;
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x00059525 File Offset: 0x00057725
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x2(int2x2 v)
		{
			this.c0 = (uint2)v.c0;
			this.c1 = (uint2)v.c1;
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x00059549 File Offset: 0x00057749
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x2(float v)
		{
			this.c0 = (uint2)v;
			this.c1 = (uint2)v;
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x00059563 File Offset: 0x00057763
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x2(float2x2 v)
		{
			this.c0 = (uint2)v.c0;
			this.c1 = (uint2)v.c1;
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x00059587 File Offset: 0x00057787
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x2(double v)
		{
			this.c0 = (uint2)v;
			this.c1 = (uint2)v;
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x000595A1 File Offset: 0x000577A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x2(double2x2 v)
		{
			this.c0 = (uint2)v.c0;
			this.c1 = (uint2)v.c1;
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x000595C5 File Offset: 0x000577C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator uint2x2(uint v)
		{
			return new uint2x2(v);
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x000595CD File Offset: 0x000577CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x2(bool v)
		{
			return new uint2x2(v);
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x000595D5 File Offset: 0x000577D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x2(bool2x2 v)
		{
			return new uint2x2(v);
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x000595DD File Offset: 0x000577DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x2(int v)
		{
			return new uint2x2(v);
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x000595E5 File Offset: 0x000577E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x2(int2x2 v)
		{
			return new uint2x2(v);
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x000595ED File Offset: 0x000577ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x2(float v)
		{
			return new uint2x2(v);
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x000595F5 File Offset: 0x000577F5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x2(float2x2 v)
		{
			return new uint2x2(v);
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x000595FD File Offset: 0x000577FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x2(double v)
		{
			return new uint2x2(v);
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x00059605 File Offset: 0x00057805
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x2(double2x2 v)
		{
			return new uint2x2(v);
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x0005960D File Offset: 0x0005780D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator *(uint2x2 lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1);
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x00059636 File Offset: 0x00057836
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator *(uint2x2 lhs, uint rhs)
		{
			return new uint2x2(lhs.c0 * rhs, lhs.c1 * rhs);
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x00059655 File Offset: 0x00057855
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator *(uint lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs * rhs.c0, lhs * rhs.c1);
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x00059674 File Offset: 0x00057874
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator +(uint2x2 lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1);
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x0005969D File Offset: 0x0005789D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator +(uint2x2 lhs, uint rhs)
		{
			return new uint2x2(lhs.c0 + rhs, lhs.c1 + rhs);
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x000596BC File Offset: 0x000578BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator +(uint lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs + rhs.c0, lhs + rhs.c1);
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x000596DB File Offset: 0x000578DB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator -(uint2x2 lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1);
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x00059704 File Offset: 0x00057904
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator -(uint2x2 lhs, uint rhs)
		{
			return new uint2x2(lhs.c0 - rhs, lhs.c1 - rhs);
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x00059723 File Offset: 0x00057923
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator -(uint lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs - rhs.c0, lhs - rhs.c1);
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x00059742 File Offset: 0x00057942
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator /(uint2x2 lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1);
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x0005976B File Offset: 0x0005796B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator /(uint2x2 lhs, uint rhs)
		{
			return new uint2x2(lhs.c0 / rhs, lhs.c1 / rhs);
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x0005978A File Offset: 0x0005798A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator /(uint lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs / rhs.c0, lhs / rhs.c1);
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x000597A9 File Offset: 0x000579A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator %(uint2x2 lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1);
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x000597D2 File Offset: 0x000579D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator %(uint2x2 lhs, uint rhs)
		{
			return new uint2x2(lhs.c0 % rhs, lhs.c1 % rhs);
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x000597F1 File Offset: 0x000579F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator %(uint lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs % rhs.c0, lhs % rhs.c1);
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x00059810 File Offset: 0x00057A10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator ++(uint2x2 val)
		{
			uint2 @uint = ++val.c0;
			val.c0 = @uint;
			uint2 uint2 = @uint;
			@uint = ++val.c1;
			val.c1 = @uint;
			return new uint2x2(uint2, @uint);
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x00059858 File Offset: 0x00057A58
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator --(uint2x2 val)
		{
			uint2 @uint = --val.c0;
			val.c0 = @uint;
			uint2 uint2 = @uint;
			@uint = --val.c1;
			val.c1 = @uint;
			return new uint2x2(uint2, @uint);
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x0005989E File Offset: 0x00057A9E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <(uint2x2 lhs, uint2x2 rhs)
		{
			return new bool2x2(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1);
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x000598C7 File Offset: 0x00057AC7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <(uint2x2 lhs, uint rhs)
		{
			return new bool2x2(lhs.c0 < rhs, lhs.c1 < rhs);
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x000598E6 File Offset: 0x00057AE6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <(uint lhs, uint2x2 rhs)
		{
			return new bool2x2(lhs < rhs.c0, lhs < rhs.c1);
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x00059905 File Offset: 0x00057B05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <=(uint2x2 lhs, uint2x2 rhs)
		{
			return new bool2x2(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1);
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x0005992E File Offset: 0x00057B2E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <=(uint2x2 lhs, uint rhs)
		{
			return new bool2x2(lhs.c0 <= rhs, lhs.c1 <= rhs);
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x0005994D File Offset: 0x00057B4D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <=(uint lhs, uint2x2 rhs)
		{
			return new bool2x2(lhs <= rhs.c0, lhs <= rhs.c1);
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x0005996C File Offset: 0x00057B6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >(uint2x2 lhs, uint2x2 rhs)
		{
			return new bool2x2(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1);
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x00059995 File Offset: 0x00057B95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >(uint2x2 lhs, uint rhs)
		{
			return new bool2x2(lhs.c0 > rhs, lhs.c1 > rhs);
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x000599B4 File Offset: 0x00057BB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >(uint lhs, uint2x2 rhs)
		{
			return new bool2x2(lhs > rhs.c0, lhs > rhs.c1);
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x000599D3 File Offset: 0x00057BD3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >=(uint2x2 lhs, uint2x2 rhs)
		{
			return new bool2x2(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1);
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x000599FC File Offset: 0x00057BFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >=(uint2x2 lhs, uint rhs)
		{
			return new bool2x2(lhs.c0 >= rhs, lhs.c1 >= rhs);
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x00059A1B File Offset: 0x00057C1B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >=(uint lhs, uint2x2 rhs)
		{
			return new bool2x2(lhs >= rhs.c0, lhs >= rhs.c1);
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x00059A3A File Offset: 0x00057C3A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator -(uint2x2 val)
		{
			return new uint2x2(-val.c0, -val.c1);
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x00059A57 File Offset: 0x00057C57
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator +(uint2x2 val)
		{
			return new uint2x2(+val.c0, +val.c1);
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x00059A74 File Offset: 0x00057C74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator <<(uint2x2 x, int n)
		{
			return new uint2x2(x.c0 << n, x.c1 << n);
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x00059A93 File Offset: 0x00057C93
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator >>(uint2x2 x, int n)
		{
			return new uint2x2(x.c0 >> n, x.c1 >> n);
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x00059AB2 File Offset: 0x00057CB2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(uint2x2 lhs, uint2x2 rhs)
		{
			return new bool2x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x00059ADB File Offset: 0x00057CDB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(uint2x2 lhs, uint rhs)
		{
			return new bool2x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x00059AFA File Offset: 0x00057CFA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(uint lhs, uint2x2 rhs)
		{
			return new bool2x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x00059B19 File Offset: 0x00057D19
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(uint2x2 lhs, uint2x2 rhs)
		{
			return new bool2x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x00059B42 File Offset: 0x00057D42
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(uint2x2 lhs, uint rhs)
		{
			return new bool2x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x00059B61 File Offset: 0x00057D61
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(uint lhs, uint2x2 rhs)
		{
			return new bool2x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x00059B80 File Offset: 0x00057D80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator ~(uint2x2 val)
		{
			return new uint2x2(~val.c0, ~val.c1);
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x00059B9D File Offset: 0x00057D9D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator &(uint2x2 lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1);
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x00059BC6 File Offset: 0x00057DC6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator &(uint2x2 lhs, uint rhs)
		{
			return new uint2x2(lhs.c0 & rhs, lhs.c1 & rhs);
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x00059BE5 File Offset: 0x00057DE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator &(uint lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs & rhs.c0, lhs & rhs.c1);
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x00059C04 File Offset: 0x00057E04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator |(uint2x2 lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1);
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x00059C2D File Offset: 0x00057E2D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator |(uint2x2 lhs, uint rhs)
		{
			return new uint2x2(lhs.c0 | rhs, lhs.c1 | rhs);
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x00059C4C File Offset: 0x00057E4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator |(uint lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs | rhs.c0, lhs | rhs.c1);
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x00059C6B File Offset: 0x00057E6B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator ^(uint2x2 lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1);
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x00059C94 File Offset: 0x00057E94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator ^(uint2x2 lhs, uint rhs)
		{
			return new uint2x2(lhs.c0 ^ rhs, lhs.c1 ^ rhs);
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x00059CB3 File Offset: 0x00057EB3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator ^(uint lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs ^ rhs.c0, lhs ^ rhs.c1);
		}

		// Token: 0x170009BA RID: 2490
		public unsafe uint2 this[int index]
		{
			get
			{
				fixed (uint2x2* ptr = &this)
				{
					return ref *(uint2*)(ptr + (IntPtr)index * (IntPtr)sizeof(uint2) / (IntPtr)sizeof(uint2x2));
				}
			}
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x00059CEF File Offset: 0x00057EEF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(uint2x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x00059D18 File Offset: 0x00057F18
		public override bool Equals(object o)
		{
			if (o is uint2x2)
			{
				uint2x2 rhs = (uint2x2)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x00059D3D File Offset: 0x00057F3D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x00059D4C File Offset: 0x00057F4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("uint2x2({0}, {1},  {2}, {3})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c0.y,
				this.c1.y
			});
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x00059DB8 File Offset: 0x00057FB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("uint2x2({0}, {1},  {2}, {3})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x00059E29 File Offset: 0x00058029
		// Note: this type is marked as 'beforefieldinit'.
		static uint2x2()
		{
		}

		// Token: 0x040000EB RID: 235
		public uint2 c0;

		// Token: 0x040000EC RID: 236
		public uint2 c1;

		// Token: 0x040000ED RID: 237
		public static readonly uint2x2 identity = new uint2x2(1U, 0U, 0U, 1U);

		// Token: 0x040000EE RID: 238
		public static readonly uint2x2 zero;
	}
}
