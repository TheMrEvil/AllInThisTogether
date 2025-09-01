using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000033 RID: 51
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct int3x4 : IEquatable<int3x4>, IFormattable
	{
		// Token: 0x06001AD2 RID: 6866 RVA: 0x00049478 File Offset: 0x00047678
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x4(int3 c0, int3 c1, int3 c2, int3 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x00049498 File Offset: 0x00047698
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x4(int m00, int m01, int m02, int m03, int m10, int m11, int m12, int m13, int m20, int m21, int m22, int m23)
		{
			this.c0 = new int3(m00, m10, m20);
			this.c1 = new int3(m01, m11, m21);
			this.c2 = new int3(m02, m12, m22);
			this.c3 = new int3(m03, m13, m23);
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x000494E6 File Offset: 0x000476E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x4(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x00049518 File Offset: 0x00047718
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x4(bool v)
		{
			this.c0 = math.select(new int3(0), new int3(1), v);
			this.c1 = math.select(new int3(0), new int3(1), v);
			this.c2 = math.select(new int3(0), new int3(1), v);
			this.c3 = math.select(new int3(0), new int3(1), v);
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x00049588 File Offset: 0x00047788
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x4(bool3x4 v)
		{
			this.c0 = math.select(new int3(0), new int3(1), v.c0);
			this.c1 = math.select(new int3(0), new int3(1), v.c1);
			this.c2 = math.select(new int3(0), new int3(1), v.c2);
			this.c3 = math.select(new int3(0), new int3(1), v.c3);
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x00049609 File Offset: 0x00047809
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x4(uint v)
		{
			this.c0 = (int3)v;
			this.c1 = (int3)v;
			this.c2 = (int3)v;
			this.c3 = (int3)v;
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x0004963C File Offset: 0x0004783C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x4(uint3x4 v)
		{
			this.c0 = (int3)v.c0;
			this.c1 = (int3)v.c1;
			this.c2 = (int3)v.c2;
			this.c3 = (int3)v.c3;
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x0004968D File Offset: 0x0004788D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x4(float v)
		{
			this.c0 = (int3)v;
			this.c1 = (int3)v;
			this.c2 = (int3)v;
			this.c3 = (int3)v;
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x000496C0 File Offset: 0x000478C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x4(float3x4 v)
		{
			this.c0 = (int3)v.c0;
			this.c1 = (int3)v.c1;
			this.c2 = (int3)v.c2;
			this.c3 = (int3)v.c3;
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x00049711 File Offset: 0x00047911
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x4(double v)
		{
			this.c0 = (int3)v;
			this.c1 = (int3)v;
			this.c2 = (int3)v;
			this.c3 = (int3)v;
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x00049744 File Offset: 0x00047944
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x4(double3x4 v)
		{
			this.c0 = (int3)v.c0;
			this.c1 = (int3)v.c1;
			this.c2 = (int3)v.c2;
			this.c3 = (int3)v.c3;
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x00049795 File Offset: 0x00047995
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int3x4(int v)
		{
			return new int3x4(v);
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x0004979D File Offset: 0x0004799D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x4(bool v)
		{
			return new int3x4(v);
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x000497A5 File Offset: 0x000479A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x4(bool3x4 v)
		{
			return new int3x4(v);
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x000497AD File Offset: 0x000479AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x4(uint v)
		{
			return new int3x4(v);
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x000497B5 File Offset: 0x000479B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x4(uint3x4 v)
		{
			return new int3x4(v);
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x000497BD File Offset: 0x000479BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x4(float v)
		{
			return new int3x4(v);
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x000497C5 File Offset: 0x000479C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x4(float3x4 v)
		{
			return new int3x4(v);
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x000497CD File Offset: 0x000479CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x4(double v)
		{
			return new int3x4(v);
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x000497D5 File Offset: 0x000479D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x4(double3x4 v)
		{
			return new int3x4(v);
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x000497E0 File Offset: 0x000479E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator *(int3x4 lhs, int3x4 rhs)
		{
			return new int3x4(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2, lhs.c3 * rhs.c3);
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x00049836 File Offset: 0x00047A36
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator *(int3x4 lhs, int rhs)
		{
			return new int3x4(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs, lhs.c3 * rhs);
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x0004986D File Offset: 0x00047A6D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator *(int lhs, int3x4 rhs)
		{
			return new int3x4(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2, lhs * rhs.c3);
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x000498A4 File Offset: 0x00047AA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator +(int3x4 lhs, int3x4 rhs)
		{
			return new int3x4(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2, lhs.c3 + rhs.c3);
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x000498FA File Offset: 0x00047AFA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator +(int3x4 lhs, int rhs)
		{
			return new int3x4(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs, lhs.c3 + rhs);
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x00049931 File Offset: 0x00047B31
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator +(int lhs, int3x4 rhs)
		{
			return new int3x4(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2, lhs + rhs.c3);
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x00049968 File Offset: 0x00047B68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator -(int3x4 lhs, int3x4 rhs)
		{
			return new int3x4(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2, lhs.c3 - rhs.c3);
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x000499BE File Offset: 0x00047BBE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator -(int3x4 lhs, int rhs)
		{
			return new int3x4(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs, lhs.c3 - rhs);
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x000499F5 File Offset: 0x00047BF5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator -(int lhs, int3x4 rhs)
		{
			return new int3x4(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2, lhs - rhs.c3);
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x00049A2C File Offset: 0x00047C2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator /(int3x4 lhs, int3x4 rhs)
		{
			return new int3x4(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2, lhs.c3 / rhs.c3);
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x00049A82 File Offset: 0x00047C82
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator /(int3x4 lhs, int rhs)
		{
			return new int3x4(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs, lhs.c3 / rhs);
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x00049AB9 File Offset: 0x00047CB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator /(int lhs, int3x4 rhs)
		{
			return new int3x4(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2, lhs / rhs.c3);
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x00049AF0 File Offset: 0x00047CF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator %(int3x4 lhs, int3x4 rhs)
		{
			return new int3x4(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2, lhs.c3 % rhs.c3);
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x00049B46 File Offset: 0x00047D46
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator %(int3x4 lhs, int rhs)
		{
			return new int3x4(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs, lhs.c3 % rhs);
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x00049B7D File Offset: 0x00047D7D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator %(int lhs, int3x4 rhs)
		{
			return new int3x4(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2, lhs % rhs.c3);
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x00049BB4 File Offset: 0x00047DB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator ++(int3x4 val)
		{
			int3 @int = ++val.c0;
			val.c0 = @int;
			int3 int2 = @int;
			@int = ++val.c1;
			val.c1 = @int;
			int3 int3 = @int;
			@int = ++val.c2;
			val.c2 = @int;
			int3 int4 = @int;
			@int = ++val.c3;
			val.c3 = @int;
			return new int3x4(int2, int3, int4, @int);
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x00049C30 File Offset: 0x00047E30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator --(int3x4 val)
		{
			int3 @int = --val.c0;
			val.c0 = @int;
			int3 int2 = @int;
			@int = --val.c1;
			val.c1 = @int;
			int3 int3 = @int;
			@int = --val.c2;
			val.c2 = @int;
			int3 int4 = @int;
			@int = --val.c3;
			val.c3 = @int;
			return new int3x4(int2, int3, int4, @int);
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x00049CAC File Offset: 0x00047EAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <(int3x4 lhs, int3x4 rhs)
		{
			return new bool3x4(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2, lhs.c3 < rhs.c3);
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x00049D02 File Offset: 0x00047F02
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <(int3x4 lhs, int rhs)
		{
			return new bool3x4(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs, lhs.c3 < rhs);
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x00049D39 File Offset: 0x00047F39
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <(int lhs, int3x4 rhs)
		{
			return new bool3x4(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2, lhs < rhs.c3);
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x00049D70 File Offset: 0x00047F70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <=(int3x4 lhs, int3x4 rhs)
		{
			return new bool3x4(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2, lhs.c3 <= rhs.c3);
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x00049DC6 File Offset: 0x00047FC6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <=(int3x4 lhs, int rhs)
		{
			return new bool3x4(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs, lhs.c3 <= rhs);
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x00049DFD File Offset: 0x00047FFD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <=(int lhs, int3x4 rhs)
		{
			return new bool3x4(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2, lhs <= rhs.c3);
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x00049E34 File Offset: 0x00048034
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >(int3x4 lhs, int3x4 rhs)
		{
			return new bool3x4(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2, lhs.c3 > rhs.c3);
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x00049E8A File Offset: 0x0004808A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >(int3x4 lhs, int rhs)
		{
			return new bool3x4(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs, lhs.c3 > rhs);
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x00049EC1 File Offset: 0x000480C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >(int lhs, int3x4 rhs)
		{
			return new bool3x4(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2, lhs > rhs.c3);
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x00049EF8 File Offset: 0x000480F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >=(int3x4 lhs, int3x4 rhs)
		{
			return new bool3x4(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2, lhs.c3 >= rhs.c3);
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x00049F4E File Offset: 0x0004814E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >=(int3x4 lhs, int rhs)
		{
			return new bool3x4(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs, lhs.c3 >= rhs);
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x00049F85 File Offset: 0x00048185
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >=(int lhs, int3x4 rhs)
		{
			return new bool3x4(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2, lhs >= rhs.c3);
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x00049FBC File Offset: 0x000481BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator -(int3x4 val)
		{
			return new int3x4(-val.c0, -val.c1, -val.c2, -val.c3);
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x00049FEF File Offset: 0x000481EF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator +(int3x4 val)
		{
			return new int3x4(+val.c0, +val.c1, +val.c2, +val.c3);
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x0004A022 File Offset: 0x00048222
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator <<(int3x4 x, int n)
		{
			return new int3x4(x.c0 << n, x.c1 << n, x.c2 << n, x.c3 << n);
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x0004A059 File Offset: 0x00048259
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator >>(int3x4 x, int n)
		{
			return new int3x4(x.c0 >> n, x.c1 >> n, x.c2 >> n, x.c3 >> n);
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x0004A090 File Offset: 0x00048290
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(int3x4 lhs, int3x4 rhs)
		{
			return new bool3x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x0004A0E6 File Offset: 0x000482E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(int3x4 lhs, int rhs)
		{
			return new bool3x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x0004A11D File Offset: 0x0004831D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(int lhs, int3x4 rhs)
		{
			return new bool3x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x0004A154 File Offset: 0x00048354
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(int3x4 lhs, int3x4 rhs)
		{
			return new bool3x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x0004A1AA File Offset: 0x000483AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(int3x4 lhs, int rhs)
		{
			return new bool3x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x0004A1E1 File Offset: 0x000483E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(int lhs, int3x4 rhs)
		{
			return new bool3x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x0004A218 File Offset: 0x00048418
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator ~(int3x4 val)
		{
			return new int3x4(~val.c0, ~val.c1, ~val.c2, ~val.c3);
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x0004A24C File Offset: 0x0004844C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator &(int3x4 lhs, int3x4 rhs)
		{
			return new int3x4(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2, lhs.c3 & rhs.c3);
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x0004A2A2 File Offset: 0x000484A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator &(int3x4 lhs, int rhs)
		{
			return new int3x4(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs, lhs.c3 & rhs);
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x0004A2D9 File Offset: 0x000484D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator &(int lhs, int3x4 rhs)
		{
			return new int3x4(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2, lhs & rhs.c3);
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x0004A310 File Offset: 0x00048510
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator |(int3x4 lhs, int3x4 rhs)
		{
			return new int3x4(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2, lhs.c3 | rhs.c3);
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x0004A366 File Offset: 0x00048566
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator |(int3x4 lhs, int rhs)
		{
			return new int3x4(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs, lhs.c3 | rhs);
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x0004A39D File Offset: 0x0004859D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator |(int lhs, int3x4 rhs)
		{
			return new int3x4(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2, lhs | rhs.c3);
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x0004A3D4 File Offset: 0x000485D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator ^(int3x4 lhs, int3x4 rhs)
		{
			return new int3x4(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2, lhs.c3 ^ rhs.c3);
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x0004A42A File Offset: 0x0004862A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator ^(int3x4 lhs, int rhs)
		{
			return new int3x4(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs, lhs.c3 ^ rhs);
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x0004A461 File Offset: 0x00048661
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator ^(int lhs, int3x4 rhs)
		{
			return new int3x4(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2, lhs ^ rhs.c3);
		}

		// Token: 0x17000848 RID: 2120
		public unsafe int3 this[int index]
		{
			get
			{
				fixed (int3x4* ptr = &this)
				{
					return ref *(int3*)(ptr + (IntPtr)index * (IntPtr)sizeof(int3) / (IntPtr)sizeof(int3x4));
				}
			}
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x0004A4B4 File Offset: 0x000486B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(int3x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x0004A510 File Offset: 0x00048710
		public override bool Equals(object o)
		{
			if (o is int3x4)
			{
				int3x4 rhs = (int3x4)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x0004A535 File Offset: 0x00048735
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x0004A544 File Offset: 0x00048744
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("int3x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11})", new object[]
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

		// Token: 0x06001B1C RID: 6940 RVA: 0x0004A64C File Offset: 0x0004884C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("int3x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c3.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider),
				this.c3.y.ToString(format, formatProvider),
				this.c0.z.ToString(format, formatProvider),
				this.c1.z.ToString(format, formatProvider),
				this.c2.z.ToString(format, formatProvider),
				this.c3.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x040000CB RID: 203
		public int3 c0;

		// Token: 0x040000CC RID: 204
		public int3 c1;

		// Token: 0x040000CD RID: 205
		public int3 c2;

		// Token: 0x040000CE RID: 206
		public int3 c3;

		// Token: 0x040000CF RID: 207
		public static readonly int3x4 zero;
	}
}
