using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200001B RID: 27
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct double4x4 : IEquatable<double4x4>, IFormattable
	{
		// Token: 0x06000FD1 RID: 4049 RVA: 0x0002E1F1 File Offset: 0x0002C3F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x4(double4 c0, double4 c1, double4 c2, double4 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0002E210 File Offset: 0x0002C410
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x4(double m00, double m01, double m02, double m03, double m10, double m11, double m12, double m13, double m20, double m21, double m22, double m23, double m30, double m31, double m32, double m33)
		{
			this.c0 = new double4(m00, m10, m20, m30);
			this.c1 = new double4(m01, m11, m21, m31);
			this.c2 = new double4(m02, m12, m22, m32);
			this.c3 = new double4(m03, m13, m23, m33);
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0002E266 File Offset: 0x0002C466
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x4(double v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x0002E298 File Offset: 0x0002C498
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x4(bool v)
		{
			this.c0 = math.select(new double4(0.0), new double4(1.0), v);
			this.c1 = math.select(new double4(0.0), new double4(1.0), v);
			this.c2 = math.select(new double4(0.0), new double4(1.0), v);
			this.c3 = math.select(new double4(0.0), new double4(1.0), v);
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x0002E348 File Offset: 0x0002C548
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x4(bool4x4 v)
		{
			this.c0 = math.select(new double4(0.0), new double4(1.0), v.c0);
			this.c1 = math.select(new double4(0.0), new double4(1.0), v.c1);
			this.c2 = math.select(new double4(0.0), new double4(1.0), v.c2);
			this.c3 = math.select(new double4(0.0), new double4(1.0), v.c3);
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x0002E409 File Offset: 0x0002C609
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x4(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x0002E43C File Offset: 0x0002C63C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x4(int4x4 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
			this.c3 = v.c3;
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0002E48D File Offset: 0x0002C68D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x4(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0002E4C0 File Offset: 0x0002C6C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x4(uint4x4 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
			this.c3 = v.c3;
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0002E511 File Offset: 0x0002C711
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x4(float v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0002E544 File Offset: 0x0002C744
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x4(float4x4 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
			this.c3 = v.c3;
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0002E595 File Offset: 0x0002C795
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x4(double v)
		{
			return new double4x4(v);
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0002E59D File Offset: 0x0002C79D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double4x4(bool v)
		{
			return new double4x4(v);
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x0002E5A5 File Offset: 0x0002C7A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double4x4(bool4x4 v)
		{
			return new double4x4(v);
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x0002E5AD File Offset: 0x0002C7AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x4(int v)
		{
			return new double4x4(v);
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0002E5B5 File Offset: 0x0002C7B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x4(int4x4 v)
		{
			return new double4x4(v);
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0002E5BD File Offset: 0x0002C7BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x4(uint v)
		{
			return new double4x4(v);
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0002E5C5 File Offset: 0x0002C7C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x4(uint4x4 v)
		{
			return new double4x4(v);
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0002E5CD File Offset: 0x0002C7CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x4(float v)
		{
			return new double4x4(v);
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0002E5D5 File Offset: 0x0002C7D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x4(float4x4 v)
		{
			return new double4x4(v);
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0002E5E0 File Offset: 0x0002C7E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator *(double4x4 lhs, double4x4 rhs)
		{
			return new double4x4(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2, lhs.c3 * rhs.c3);
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0002E636 File Offset: 0x0002C836
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator *(double4x4 lhs, double rhs)
		{
			return new double4x4(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs, lhs.c3 * rhs);
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0002E66D File Offset: 0x0002C86D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator *(double lhs, double4x4 rhs)
		{
			return new double4x4(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2, lhs * rhs.c3);
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0002E6A4 File Offset: 0x0002C8A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator +(double4x4 lhs, double4x4 rhs)
		{
			return new double4x4(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2, lhs.c3 + rhs.c3);
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0002E6FA File Offset: 0x0002C8FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator +(double4x4 lhs, double rhs)
		{
			return new double4x4(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs, lhs.c3 + rhs);
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0002E731 File Offset: 0x0002C931
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator +(double lhs, double4x4 rhs)
		{
			return new double4x4(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2, lhs + rhs.c3);
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0002E768 File Offset: 0x0002C968
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator -(double4x4 lhs, double4x4 rhs)
		{
			return new double4x4(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2, lhs.c3 - rhs.c3);
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0002E7BE File Offset: 0x0002C9BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator -(double4x4 lhs, double rhs)
		{
			return new double4x4(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs, lhs.c3 - rhs);
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0002E7F5 File Offset: 0x0002C9F5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator -(double lhs, double4x4 rhs)
		{
			return new double4x4(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2, lhs - rhs.c3);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0002E82C File Offset: 0x0002CA2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator /(double4x4 lhs, double4x4 rhs)
		{
			return new double4x4(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2, lhs.c3 / rhs.c3);
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0002E882 File Offset: 0x0002CA82
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator /(double4x4 lhs, double rhs)
		{
			return new double4x4(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs, lhs.c3 / rhs);
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0002E8B9 File Offset: 0x0002CAB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator /(double lhs, double4x4 rhs)
		{
			return new double4x4(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2, lhs / rhs.c3);
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0002E8F0 File Offset: 0x0002CAF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator %(double4x4 lhs, double4x4 rhs)
		{
			return new double4x4(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2, lhs.c3 % rhs.c3);
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0002E946 File Offset: 0x0002CB46
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator %(double4x4 lhs, double rhs)
		{
			return new double4x4(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs, lhs.c3 % rhs);
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0002E97D File Offset: 0x0002CB7D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator %(double lhs, double4x4 rhs)
		{
			return new double4x4(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2, lhs % rhs.c3);
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0002E9B4 File Offset: 0x0002CBB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator ++(double4x4 val)
		{
			double4 @double = ++val.c0;
			val.c0 = @double;
			double4 double2 = @double;
			@double = ++val.c1;
			val.c1 = @double;
			double4 double3 = @double;
			@double = ++val.c2;
			val.c2 = @double;
			double4 double4 = @double;
			@double = ++val.c3;
			val.c3 = @double;
			return new double4x4(double2, double3, double4, @double);
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0002EA30 File Offset: 0x0002CC30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator --(double4x4 val)
		{
			double4 @double = --val.c0;
			val.c0 = @double;
			double4 double2 = @double;
			@double = --val.c1;
			val.c1 = @double;
			double4 double3 = @double;
			@double = --val.c2;
			val.c2 = @double;
			double4 double4 = @double;
			@double = --val.c3;
			val.c3 = @double;
			return new double4x4(double2, double3, double4, @double);
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0002EAAC File Offset: 0x0002CCAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <(double4x4 lhs, double4x4 rhs)
		{
			return new bool4x4(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2, lhs.c3 < rhs.c3);
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0002EB02 File Offset: 0x0002CD02
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <(double4x4 lhs, double rhs)
		{
			return new bool4x4(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs, lhs.c3 < rhs);
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0002EB39 File Offset: 0x0002CD39
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <(double lhs, double4x4 rhs)
		{
			return new bool4x4(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2, lhs < rhs.c3);
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0002EB70 File Offset: 0x0002CD70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <=(double4x4 lhs, double4x4 rhs)
		{
			return new bool4x4(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2, lhs.c3 <= rhs.c3);
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0002EBC6 File Offset: 0x0002CDC6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <=(double4x4 lhs, double rhs)
		{
			return new bool4x4(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs, lhs.c3 <= rhs);
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0002EBFD File Offset: 0x0002CDFD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <=(double lhs, double4x4 rhs)
		{
			return new bool4x4(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2, lhs <= rhs.c3);
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0002EC34 File Offset: 0x0002CE34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >(double4x4 lhs, double4x4 rhs)
		{
			return new bool4x4(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2, lhs.c3 > rhs.c3);
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0002EC8A File Offset: 0x0002CE8A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >(double4x4 lhs, double rhs)
		{
			return new bool4x4(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs, lhs.c3 > rhs);
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x0002ECC1 File Offset: 0x0002CEC1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >(double lhs, double4x4 rhs)
		{
			return new bool4x4(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2, lhs > rhs.c3);
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x0002ECF8 File Offset: 0x0002CEF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >=(double4x4 lhs, double4x4 rhs)
		{
			return new bool4x4(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2, lhs.c3 >= rhs.c3);
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0002ED4E File Offset: 0x0002CF4E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >=(double4x4 lhs, double rhs)
		{
			return new bool4x4(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs, lhs.c3 >= rhs);
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0002ED85 File Offset: 0x0002CF85
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >=(double lhs, double4x4 rhs)
		{
			return new bool4x4(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2, lhs >= rhs.c3);
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0002EDBC File Offset: 0x0002CFBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator -(double4x4 val)
		{
			return new double4x4(-val.c0, -val.c1, -val.c2, -val.c3);
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0002EDEF File Offset: 0x0002CFEF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator +(double4x4 val)
		{
			return new double4x4(+val.c0, +val.c1, +val.c2, +val.c3);
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0002EE24 File Offset: 0x0002D024
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(double4x4 lhs, double4x4 rhs)
		{
			return new bool4x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0002EE7A File Offset: 0x0002D07A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(double4x4 lhs, double rhs)
		{
			return new bool4x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0002EEB1 File Offset: 0x0002D0B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(double lhs, double4x4 rhs)
		{
			return new bool4x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0002EEE8 File Offset: 0x0002D0E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(double4x4 lhs, double4x4 rhs)
		{
			return new bool4x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x0002EF3E File Offset: 0x0002D13E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(double4x4 lhs, double rhs)
		{
			return new bool4x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0002EF75 File Offset: 0x0002D175
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(double lhs, double4x4 rhs)
		{
			return new bool4x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x170003DA RID: 986
		public unsafe double4 this[int index]
		{
			get
			{
				fixed (double4x4* ptr = &this)
				{
					return ref *(double4*)(ptr + (IntPtr)index * (IntPtr)sizeof(double4) / (IntPtr)sizeof(double4x4));
				}
			}
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0002EFC8 File Offset: 0x0002D1C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(double4x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0002F024 File Offset: 0x0002D224
		public override bool Equals(object o)
		{
			if (o is double4x4)
			{
				double4x4 rhs = (double4x4)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0002F049 File Offset: 0x0002D249
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0002F058 File Offset: 0x0002D258
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("double4x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11},  {12}, {13}, {14}, {15})", new object[]
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

		// Token: 0x0600100F RID: 4111 RVA: 0x0002F1B0 File Offset: 0x0002D3B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("double4x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11},  {12}, {13}, {14}, {15})", new object[]
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
				this.c3.z.ToString(format, formatProvider),
				this.c0.w.ToString(format, formatProvider),
				this.c1.w.ToString(format, formatProvider),
				this.c2.w.ToString(format, formatProvider),
				this.c3.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0002F328 File Offset: 0x0002D528
		// Note: this type is marked as 'beforefieldinit'.
		static double4x4()
		{
		}

		// Token: 0x04000068 RID: 104
		public double4 c0;

		// Token: 0x04000069 RID: 105
		public double4 c1;

		// Token: 0x0400006A RID: 106
		public double4 c2;

		// Token: 0x0400006B RID: 107
		public double4 c3;

		// Token: 0x0400006C RID: 108
		public static readonly double4x4 identity = new double4x4(1.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 1.0);

		// Token: 0x0400006D RID: 109
		public static readonly double4x4 zero;
	}
}
