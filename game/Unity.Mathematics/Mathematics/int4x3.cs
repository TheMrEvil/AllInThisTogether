using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000036 RID: 54
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct int4x3 : IEquatable<int4x3>, IFormattable
	{
		// Token: 0x06001D46 RID: 7494 RVA: 0x0004F039 File Offset: 0x0004D239
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x3(int4 c0, int4 c1, int4 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x0004F050 File Offset: 0x0004D250
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x3(int m00, int m01, int m02, int m10, int m11, int m12, int m20, int m21, int m22, int m30, int m31, int m32)
		{
			this.c0 = new int4(m00, m10, m20, m30);
			this.c1 = new int4(m01, m11, m21, m31);
			this.c2 = new int4(m02, m12, m22, m32);
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x0004F088 File Offset: 0x0004D288
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x3(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x0004F0B0 File Offset: 0x0004D2B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x3(bool v)
		{
			this.c0 = math.select(new int4(0), new int4(1), v);
			this.c1 = math.select(new int4(0), new int4(1), v);
			this.c2 = math.select(new int4(0), new int4(1), v);
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x0004F108 File Offset: 0x0004D308
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x3(bool4x3 v)
		{
			this.c0 = math.select(new int4(0), new int4(1), v.c0);
			this.c1 = math.select(new int4(0), new int4(1), v.c1);
			this.c2 = math.select(new int4(0), new int4(1), v.c2);
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x0004F16C File Offset: 0x0004D36C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x3(uint v)
		{
			this.c0 = (int4)v;
			this.c1 = (int4)v;
			this.c2 = (int4)v;
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x0004F192 File Offset: 0x0004D392
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x3(uint4x3 v)
		{
			this.c0 = (int4)v.c0;
			this.c1 = (int4)v.c1;
			this.c2 = (int4)v.c2;
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x0004F1C7 File Offset: 0x0004D3C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x3(float v)
		{
			this.c0 = (int4)v;
			this.c1 = (int4)v;
			this.c2 = (int4)v;
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x0004F1ED File Offset: 0x0004D3ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x3(float4x3 v)
		{
			this.c0 = (int4)v.c0;
			this.c1 = (int4)v.c1;
			this.c2 = (int4)v.c2;
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x0004F222 File Offset: 0x0004D422
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x3(double v)
		{
			this.c0 = (int4)v;
			this.c1 = (int4)v;
			this.c2 = (int4)v;
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x0004F248 File Offset: 0x0004D448
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x3(double4x3 v)
		{
			this.c0 = (int4)v.c0;
			this.c1 = (int4)v.c1;
			this.c2 = (int4)v.c2;
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x0004F27D File Offset: 0x0004D47D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int4x3(int v)
		{
			return new int4x3(v);
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x0004F285 File Offset: 0x0004D485
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x3(bool v)
		{
			return new int4x3(v);
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x0004F28D File Offset: 0x0004D48D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x3(bool4x3 v)
		{
			return new int4x3(v);
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x0004F295 File Offset: 0x0004D495
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x3(uint v)
		{
			return new int4x3(v);
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x0004F29D File Offset: 0x0004D49D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x3(uint4x3 v)
		{
			return new int4x3(v);
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x0004F2A5 File Offset: 0x0004D4A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x3(float v)
		{
			return new int4x3(v);
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x0004F2AD File Offset: 0x0004D4AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x3(float4x3 v)
		{
			return new int4x3(v);
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x0004F2B5 File Offset: 0x0004D4B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x3(double v)
		{
			return new int4x3(v);
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x0004F2BD File Offset: 0x0004D4BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x3(double4x3 v)
		{
			return new int4x3(v);
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x0004F2C5 File Offset: 0x0004D4C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator *(int4x3 lhs, int4x3 rhs)
		{
			return new int4x3(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2);
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x0004F2FF File Offset: 0x0004D4FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator *(int4x3 lhs, int rhs)
		{
			return new int4x3(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs);
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x0004F32A File Offset: 0x0004D52A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator *(int lhs, int4x3 rhs)
		{
			return new int4x3(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2);
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x0004F355 File Offset: 0x0004D555
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator +(int4x3 lhs, int4x3 rhs)
		{
			return new int4x3(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2);
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x0004F38F File Offset: 0x0004D58F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator +(int4x3 lhs, int rhs)
		{
			return new int4x3(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs);
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x0004F3BA File Offset: 0x0004D5BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator +(int lhs, int4x3 rhs)
		{
			return new int4x3(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2);
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x0004F3E5 File Offset: 0x0004D5E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator -(int4x3 lhs, int4x3 rhs)
		{
			return new int4x3(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2);
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x0004F41F File Offset: 0x0004D61F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator -(int4x3 lhs, int rhs)
		{
			return new int4x3(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs);
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x0004F44A File Offset: 0x0004D64A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator -(int lhs, int4x3 rhs)
		{
			return new int4x3(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2);
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x0004F475 File Offset: 0x0004D675
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator /(int4x3 lhs, int4x3 rhs)
		{
			return new int4x3(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2);
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x0004F4AF File Offset: 0x0004D6AF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator /(int4x3 lhs, int rhs)
		{
			return new int4x3(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs);
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x0004F4DA File Offset: 0x0004D6DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator /(int lhs, int4x3 rhs)
		{
			return new int4x3(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2);
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x0004F505 File Offset: 0x0004D705
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator %(int4x3 lhs, int4x3 rhs)
		{
			return new int4x3(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2);
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x0004F53F File Offset: 0x0004D73F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator %(int4x3 lhs, int rhs)
		{
			return new int4x3(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs);
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x0004F56A File Offset: 0x0004D76A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator %(int lhs, int4x3 rhs)
		{
			return new int4x3(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2);
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x0004F598 File Offset: 0x0004D798
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator ++(int4x3 val)
		{
			int4 @int = ++val.c0;
			val.c0 = @int;
			int4 int2 = @int;
			@int = ++val.c1;
			val.c1 = @int;
			int4 int3 = @int;
			@int = ++val.c2;
			val.c2 = @int;
			return new int4x3(int2, int3, @int);
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x0004F5F8 File Offset: 0x0004D7F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator --(int4x3 val)
		{
			int4 @int = --val.c0;
			val.c0 = @int;
			int4 int2 = @int;
			@int = --val.c1;
			val.c1 = @int;
			int4 int3 = @int;
			@int = --val.c2;
			val.c2 = @int;
			return new int4x3(int2, int3, @int);
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x0004F658 File Offset: 0x0004D858
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <(int4x3 lhs, int4x3 rhs)
		{
			return new bool4x3(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2);
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x0004F692 File Offset: 0x0004D892
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <(int4x3 lhs, int rhs)
		{
			return new bool4x3(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs);
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x0004F6BD File Offset: 0x0004D8BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <(int lhs, int4x3 rhs)
		{
			return new bool4x3(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2);
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x0004F6E8 File Offset: 0x0004D8E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <=(int4x3 lhs, int4x3 rhs)
		{
			return new bool4x3(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2);
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x0004F722 File Offset: 0x0004D922
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <=(int4x3 lhs, int rhs)
		{
			return new bool4x3(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs);
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x0004F74D File Offset: 0x0004D94D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <=(int lhs, int4x3 rhs)
		{
			return new bool4x3(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2);
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x0004F778 File Offset: 0x0004D978
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >(int4x3 lhs, int4x3 rhs)
		{
			return new bool4x3(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2);
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x0004F7B2 File Offset: 0x0004D9B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >(int4x3 lhs, int rhs)
		{
			return new bool4x3(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs);
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x0004F7DD File Offset: 0x0004D9DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >(int lhs, int4x3 rhs)
		{
			return new bool4x3(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2);
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x0004F808 File Offset: 0x0004DA08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >=(int4x3 lhs, int4x3 rhs)
		{
			return new bool4x3(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2);
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x0004F842 File Offset: 0x0004DA42
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >=(int4x3 lhs, int rhs)
		{
			return new bool4x3(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs);
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x0004F86D File Offset: 0x0004DA6D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >=(int lhs, int4x3 rhs)
		{
			return new bool4x3(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2);
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x0004F898 File Offset: 0x0004DA98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator -(int4x3 val)
		{
			return new int4x3(-val.c0, -val.c1, -val.c2);
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x0004F8C0 File Offset: 0x0004DAC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator +(int4x3 val)
		{
			return new int4x3(+val.c0, +val.c1, +val.c2);
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x0004F8E8 File Offset: 0x0004DAE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator <<(int4x3 x, int n)
		{
			return new int4x3(x.c0 << n, x.c1 << n, x.c2 << n);
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x0004F913 File Offset: 0x0004DB13
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator >>(int4x3 x, int n)
		{
			return new int4x3(x.c0 >> n, x.c1 >> n, x.c2 >> n);
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x0004F93E File Offset: 0x0004DB3E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(int4x3 lhs, int4x3 rhs)
		{
			return new bool4x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x0004F978 File Offset: 0x0004DB78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(int4x3 lhs, int rhs)
		{
			return new bool4x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x0004F9A3 File Offset: 0x0004DBA3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(int lhs, int4x3 rhs)
		{
			return new bool4x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x0004F9CE File Offset: 0x0004DBCE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(int4x3 lhs, int4x3 rhs)
		{
			return new bool4x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x0004FA08 File Offset: 0x0004DC08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(int4x3 lhs, int rhs)
		{
			return new bool4x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x0004FA33 File Offset: 0x0004DC33
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(int lhs, int4x3 rhs)
		{
			return new bool4x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x0004FA5E File Offset: 0x0004DC5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator ~(int4x3 val)
		{
			return new int4x3(~val.c0, ~val.c1, ~val.c2);
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x0004FA86 File Offset: 0x0004DC86
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator &(int4x3 lhs, int4x3 rhs)
		{
			return new int4x3(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2);
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x0004FAC0 File Offset: 0x0004DCC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator &(int4x3 lhs, int rhs)
		{
			return new int4x3(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs);
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x0004FAEB File Offset: 0x0004DCEB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator &(int lhs, int4x3 rhs)
		{
			return new int4x3(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2);
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x0004FB16 File Offset: 0x0004DD16
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator |(int4x3 lhs, int4x3 rhs)
		{
			return new int4x3(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2);
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x0004FB50 File Offset: 0x0004DD50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator |(int4x3 lhs, int rhs)
		{
			return new int4x3(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs);
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x0004FB7B File Offset: 0x0004DD7B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator |(int lhs, int4x3 rhs)
		{
			return new int4x3(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2);
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x0004FBA6 File Offset: 0x0004DDA6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator ^(int4x3 lhs, int4x3 rhs)
		{
			return new int4x3(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2);
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x0004FBE0 File Offset: 0x0004DDE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator ^(int4x3 lhs, int rhs)
		{
			return new int4x3(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs);
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x0004FC0B File Offset: 0x0004DE0B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator ^(int lhs, int4x3 rhs)
		{
			return new int4x3(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2);
		}

		// Token: 0x1700099B RID: 2459
		public unsafe int4 this[int index]
		{
			get
			{
				fixed (int4x3* ptr = &this)
				{
					return ref *(int4*)(ptr + (IntPtr)index * (IntPtr)sizeof(int4) / (IntPtr)sizeof(int4x3));
				}
			}
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x0004FC53 File Offset: 0x0004DE53
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(int4x3 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2);
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x0004FC90 File Offset: 0x0004DE90
		public override bool Equals(object o)
		{
			if (o is int4x3)
			{
				int4x3 rhs = (int4x3)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x0004FCB5 File Offset: 0x0004DEB5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x0004FCC4 File Offset: 0x0004DEC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("int4x3({0}, {1}, {2},  {3}, {4}, {5},  {6}, {7}, {8},  {9}, {10}, {11})", new object[]
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

		// Token: 0x06001D90 RID: 7568 RVA: 0x0004FDCC File Offset: 0x0004DFCC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("int4x3({0}, {1}, {2},  {3}, {4}, {5},  {6}, {7}, {8},  {9}, {10}, {11})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider),
				this.c0.z.ToString(format, formatProvider),
				this.c1.z.ToString(format, formatProvider),
				this.c2.z.ToString(format, formatProvider),
				this.c0.w.ToString(format, formatProvider),
				this.c1.w.ToString(format, formatProvider),
				this.c2.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x040000D8 RID: 216
		public int4 c0;

		// Token: 0x040000D9 RID: 217
		public int4 c1;

		// Token: 0x040000DA RID: 218
		public int4 c2;

		// Token: 0x040000DB RID: 219
		public static readonly int4x3 zero;
	}
}
