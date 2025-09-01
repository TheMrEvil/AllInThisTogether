using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000034 RID: 52
	[DebuggerTypeProxy(typeof(int4.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct int4 : IEquatable<int4>, IFormattable
	{
		// Token: 0x06001B1D RID: 6941 RVA: 0x0004A769 File Offset: 0x00048969
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(int x, int y, int z, int w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x0004A788 File Offset: 0x00048988
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(int x, int y, int2 zw)
		{
			this.x = x;
			this.y = y;
			this.z = zw.x;
			this.w = zw.y;
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x0004A7B0 File Offset: 0x000489B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(int x, int2 yz, int w)
		{
			this.x = x;
			this.y = yz.x;
			this.z = yz.y;
			this.w = w;
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x0004A7D8 File Offset: 0x000489D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(int x, int3 yzw)
		{
			this.x = x;
			this.y = yzw.x;
			this.z = yzw.y;
			this.w = yzw.z;
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x0004A805 File Offset: 0x00048A05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(int2 xy, int z, int w)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x0004A82D File Offset: 0x00048A2D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(int2 xy, int2 zw)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = zw.x;
			this.w = zw.y;
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x0004A85F File Offset: 0x00048A5F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(int3 xyz, int w)
		{
			this.x = xyz.x;
			this.y = xyz.y;
			this.z = xyz.z;
			this.w = w;
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x0004A88C File Offset: 0x00048A8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(int4 xyzw)
		{
			this.x = xyzw.x;
			this.y = xyzw.y;
			this.z = xyzw.z;
			this.w = xyzw.w;
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x0004A8BE File Offset: 0x00048ABE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(int v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
			this.w = v;
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x0004A8DC File Offset: 0x00048ADC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(bool v)
		{
			this.x = (v ? 1 : 0);
			this.y = (v ? 1 : 0);
			this.z = (v ? 1 : 0);
			this.w = (v ? 1 : 0);
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x0004A914 File Offset: 0x00048B14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(bool4 v)
		{
			this.x = (v.x ? 1 : 0);
			this.y = (v.y ? 1 : 0);
			this.z = (v.z ? 1 : 0);
			this.w = (v.w ? 1 : 0);
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x0004A969 File Offset: 0x00048B69
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(uint v)
		{
			this.x = (int)v;
			this.y = (int)v;
			this.z = (int)v;
			this.w = (int)v;
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x0004A987 File Offset: 0x00048B87
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(uint4 v)
		{
			this.x = (int)v.x;
			this.y = (int)v.y;
			this.z = (int)v.z;
			this.w = (int)v.w;
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x0004A9B9 File Offset: 0x00048BB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(float v)
		{
			this.x = (int)v;
			this.y = (int)v;
			this.z = (int)v;
			this.w = (int)v;
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x0004A9DB File Offset: 0x00048BDB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(float4 v)
		{
			this.x = (int)v.x;
			this.y = (int)v.y;
			this.z = (int)v.z;
			this.w = (int)v.w;
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x0004AA11 File Offset: 0x00048C11
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(double v)
		{
			this.x = (int)v;
			this.y = (int)v;
			this.z = (int)v;
			this.w = (int)v;
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x0004AA33 File Offset: 0x00048C33
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(double4 v)
		{
			this.x = (int)v.x;
			this.y = (int)v.y;
			this.z = (int)v.z;
			this.w = (int)v.w;
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x0004AA69 File Offset: 0x00048C69
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int4(int v)
		{
			return new int4(v);
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x0004AA71 File Offset: 0x00048C71
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4(bool v)
		{
			return new int4(v);
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x0004AA79 File Offset: 0x00048C79
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4(bool4 v)
		{
			return new int4(v);
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x0004AA81 File Offset: 0x00048C81
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4(uint v)
		{
			return new int4(v);
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x0004AA89 File Offset: 0x00048C89
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4(uint4 v)
		{
			return new int4(v);
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x0004AA91 File Offset: 0x00048C91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4(float v)
		{
			return new int4(v);
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x0004AA99 File Offset: 0x00048C99
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4(float4 v)
		{
			return new int4(v);
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x0004AAA1 File Offset: 0x00048CA1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4(double v)
		{
			return new int4(v);
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x0004AAA9 File Offset: 0x00048CA9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4(double4 v)
		{
			return new int4(v);
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x0004AAB1 File Offset: 0x00048CB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator *(int4 lhs, int4 rhs)
		{
			return new int4(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z, lhs.w * rhs.w);
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x0004AAEC File Offset: 0x00048CEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator *(int4 lhs, int rhs)
		{
			return new int4(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs, lhs.w * rhs);
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x0004AB13 File Offset: 0x00048D13
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator *(int lhs, int4 rhs)
		{
			return new int4(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z, lhs * rhs.w);
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x0004AB3A File Offset: 0x00048D3A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator +(int4 lhs, int4 rhs)
		{
			return new int4(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w);
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x0004AB75 File Offset: 0x00048D75
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator +(int4 lhs, int rhs)
		{
			return new int4(lhs.x + rhs, lhs.y + rhs, lhs.z + rhs, lhs.w + rhs);
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x0004AB9C File Offset: 0x00048D9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator +(int lhs, int4 rhs)
		{
			return new int4(lhs + rhs.x, lhs + rhs.y, lhs + rhs.z, lhs + rhs.w);
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x0004ABC3 File Offset: 0x00048DC3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator -(int4 lhs, int4 rhs)
		{
			return new int4(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w);
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x0004ABFE File Offset: 0x00048DFE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator -(int4 lhs, int rhs)
		{
			return new int4(lhs.x - rhs, lhs.y - rhs, lhs.z - rhs, lhs.w - rhs);
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x0004AC25 File Offset: 0x00048E25
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator -(int lhs, int4 rhs)
		{
			return new int4(lhs - rhs.x, lhs - rhs.y, lhs - rhs.z, lhs - rhs.w);
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x0004AC4C File Offset: 0x00048E4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator /(int4 lhs, int4 rhs)
		{
			return new int4(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z, lhs.w / rhs.w);
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x0004AC87 File Offset: 0x00048E87
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator /(int4 lhs, int rhs)
		{
			return new int4(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs, lhs.w / rhs);
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x0004ACAE File Offset: 0x00048EAE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator /(int lhs, int4 rhs)
		{
			return new int4(lhs / rhs.x, lhs / rhs.y, lhs / rhs.z, lhs / rhs.w);
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x0004ACD5 File Offset: 0x00048ED5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator %(int4 lhs, int4 rhs)
		{
			return new int4(lhs.x % rhs.x, lhs.y % rhs.y, lhs.z % rhs.z, lhs.w % rhs.w);
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x0004AD10 File Offset: 0x00048F10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator %(int4 lhs, int rhs)
		{
			return new int4(lhs.x % rhs, lhs.y % rhs, lhs.z % rhs, lhs.w % rhs);
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x0004AD37 File Offset: 0x00048F37
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator %(int lhs, int4 rhs)
		{
			return new int4(lhs % rhs.x, lhs % rhs.y, lhs % rhs.z, lhs % rhs.w);
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x0004AD60 File Offset: 0x00048F60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator ++(int4 val)
		{
			int num = val.x + 1;
			val.x = num;
			int num2 = num;
			num = val.y + 1;
			val.y = num;
			int num3 = num;
			num = val.z + 1;
			val.z = num;
			int num4 = num;
			num = val.w + 1;
			val.w = num;
			return new int4(num2, num3, num4, num);
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x0004ADB0 File Offset: 0x00048FB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator --(int4 val)
		{
			int num = val.x - 1;
			val.x = num;
			int num2 = num;
			num = val.y - 1;
			val.y = num;
			int num3 = num;
			num = val.z - 1;
			val.z = num;
			int num4 = num;
			num = val.w - 1;
			val.w = num;
			return new int4(num2, num3, num4, num);
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x0004ADFE File Offset: 0x00048FFE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <(int4 lhs, int4 rhs)
		{
			return new bool4(lhs.x < rhs.x, lhs.y < rhs.y, lhs.z < rhs.z, lhs.w < rhs.w);
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x0004AE3D File Offset: 0x0004903D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <(int4 lhs, int rhs)
		{
			return new bool4(lhs.x < rhs, lhs.y < rhs, lhs.z < rhs, lhs.w < rhs);
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x0004AE68 File Offset: 0x00049068
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <(int lhs, int4 rhs)
		{
			return new bool4(lhs < rhs.x, lhs < rhs.y, lhs < rhs.z, lhs < rhs.w);
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x0004AE94 File Offset: 0x00049094
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <=(int4 lhs, int4 rhs)
		{
			return new bool4(lhs.x <= rhs.x, lhs.y <= rhs.y, lhs.z <= rhs.z, lhs.w <= rhs.w);
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x0004AEEA File Offset: 0x000490EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <=(int4 lhs, int rhs)
		{
			return new bool4(lhs.x <= rhs, lhs.y <= rhs, lhs.z <= rhs, lhs.w <= rhs);
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x0004AF21 File Offset: 0x00049121
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <=(int lhs, int4 rhs)
		{
			return new bool4(lhs <= rhs.x, lhs <= rhs.y, lhs <= rhs.z, lhs <= rhs.w);
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x0004AF58 File Offset: 0x00049158
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >(int4 lhs, int4 rhs)
		{
			return new bool4(lhs.x > rhs.x, lhs.y > rhs.y, lhs.z > rhs.z, lhs.w > rhs.w);
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x0004AF97 File Offset: 0x00049197
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >(int4 lhs, int rhs)
		{
			return new bool4(lhs.x > rhs, lhs.y > rhs, lhs.z > rhs, lhs.w > rhs);
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x0004AFC2 File Offset: 0x000491C2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >(int lhs, int4 rhs)
		{
			return new bool4(lhs > rhs.x, lhs > rhs.y, lhs > rhs.z, lhs > rhs.w);
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x0004AFF0 File Offset: 0x000491F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >=(int4 lhs, int4 rhs)
		{
			return new bool4(lhs.x >= rhs.x, lhs.y >= rhs.y, lhs.z >= rhs.z, lhs.w >= rhs.w);
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x0004B046 File Offset: 0x00049246
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >=(int4 lhs, int rhs)
		{
			return new bool4(lhs.x >= rhs, lhs.y >= rhs, lhs.z >= rhs, lhs.w >= rhs);
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x0004B07D File Offset: 0x0004927D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >=(int lhs, int4 rhs)
		{
			return new bool4(lhs >= rhs.x, lhs >= rhs.y, lhs >= rhs.z, lhs >= rhs.w);
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x0004B0B4 File Offset: 0x000492B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator -(int4 val)
		{
			return new int4(-val.x, -val.y, -val.z, -val.w);
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x0004B0D7 File Offset: 0x000492D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator +(int4 val)
		{
			return new int4(val.x, val.y, val.z, val.w);
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x0004B0F6 File Offset: 0x000492F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator <<(int4 x, int n)
		{
			return new int4(x.x << n, x.y << n, x.z << n, x.w << n);
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x0004B129 File Offset: 0x00049329
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator >>(int4 x, int n)
		{
			return new int4(x.x >> n, x.y >> n, x.z >> n, x.w >> n);
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x0004B15C File Offset: 0x0004935C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(int4 lhs, int4 rhs)
		{
			return new bool4(lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z, lhs.w == rhs.w);
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x0004B19B File Offset: 0x0004939B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(int4 lhs, int rhs)
		{
			return new bool4(lhs.x == rhs, lhs.y == rhs, lhs.z == rhs, lhs.w == rhs);
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x0004B1C6 File Offset: 0x000493C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(int lhs, int4 rhs)
		{
			return new bool4(lhs == rhs.x, lhs == rhs.y, lhs == rhs.z, lhs == rhs.w);
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x0004B1F4 File Offset: 0x000493F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(int4 lhs, int4 rhs)
		{
			return new bool4(lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z, lhs.w != rhs.w);
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x0004B24A File Offset: 0x0004944A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(int4 lhs, int rhs)
		{
			return new bool4(lhs.x != rhs, lhs.y != rhs, lhs.z != rhs, lhs.w != rhs);
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x0004B281 File Offset: 0x00049481
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(int lhs, int4 rhs)
		{
			return new bool4(lhs != rhs.x, lhs != rhs.y, lhs != rhs.z, lhs != rhs.w);
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x0004B2B8 File Offset: 0x000494B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator ~(int4 val)
		{
			return new int4(~val.x, ~val.y, ~val.z, ~val.w);
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x0004B2DB File Offset: 0x000494DB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator &(int4 lhs, int4 rhs)
		{
			return new int4(lhs.x & rhs.x, lhs.y & rhs.y, lhs.z & rhs.z, lhs.w & rhs.w);
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x0004B316 File Offset: 0x00049516
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator &(int4 lhs, int rhs)
		{
			return new int4(lhs.x & rhs, lhs.y & rhs, lhs.z & rhs, lhs.w & rhs);
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x0004B33D File Offset: 0x0004953D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator &(int lhs, int4 rhs)
		{
			return new int4(lhs & rhs.x, lhs & rhs.y, lhs & rhs.z, lhs & rhs.w);
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x0004B364 File Offset: 0x00049564
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator |(int4 lhs, int4 rhs)
		{
			return new int4(lhs.x | rhs.x, lhs.y | rhs.y, lhs.z | rhs.z, lhs.w | rhs.w);
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x0004B39F File Offset: 0x0004959F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator |(int4 lhs, int rhs)
		{
			return new int4(lhs.x | rhs, lhs.y | rhs, lhs.z | rhs, lhs.w | rhs);
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x0004B3C6 File Offset: 0x000495C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator |(int lhs, int4 rhs)
		{
			return new int4(lhs | rhs.x, lhs | rhs.y, lhs | rhs.z, lhs | rhs.w);
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x0004B3ED File Offset: 0x000495ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator ^(int4 lhs, int4 rhs)
		{
			return new int4(lhs.x ^ rhs.x, lhs.y ^ rhs.y, lhs.z ^ rhs.z, lhs.w ^ rhs.w);
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x0004B428 File Offset: 0x00049628
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator ^(int4 lhs, int rhs)
		{
			return new int4(lhs.x ^ rhs, lhs.y ^ rhs, lhs.z ^ rhs, lhs.w ^ rhs);
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x0004B44F File Offset: 0x0004964F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator ^(int lhs, int4 rhs)
		{
			return new int4(lhs ^ rhs.x, lhs ^ rhs.y, lhs ^ rhs.z, lhs ^ rhs.w);
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06001B68 RID: 7016 RVA: 0x0004B476 File Offset: 0x00049676
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06001B69 RID: 7017 RVA: 0x0004B495 File Offset: 0x00049695
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06001B6A RID: 7018 RVA: 0x0004B4B4 File Offset: 0x000496B4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.x, this.z);
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x0004B4D3 File Offset: 0x000496D3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.x, this.w);
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06001B6C RID: 7020 RVA: 0x0004B4F2 File Offset: 0x000496F2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06001B6D RID: 7021 RVA: 0x0004B511 File Offset: 0x00049711
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06001B6E RID: 7022 RVA: 0x0004B530 File Offset: 0x00049730
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.y, this.z);
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06001B6F RID: 7023 RVA: 0x0004B54F File Offset: 0x0004974F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.y, this.w);
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x0004B56E File Offset: 0x0004976E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06001B71 RID: 7025 RVA: 0x0004B58D File Offset: 0x0004978D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06001B72 RID: 7026 RVA: 0x0004B5AC File Offset: 0x000497AC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06001B73 RID: 7027 RVA: 0x0004B5CB File Offset: 0x000497CB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.z, this.w);
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06001B74 RID: 7028 RVA: 0x0004B5EA File Offset: 0x000497EA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06001B75 RID: 7029 RVA: 0x0004B609 File Offset: 0x00049809
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.w, this.y);
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06001B76 RID: 7030 RVA: 0x0004B628 File Offset: 0x00049828
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.w, this.z);
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06001B77 RID: 7031 RVA: 0x0004B647 File Offset: 0x00049847
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.w, this.w);
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06001B78 RID: 7032 RVA: 0x0004B666 File Offset: 0x00049866
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06001B79 RID: 7033 RVA: 0x0004B685 File Offset: 0x00049885
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06001B7A RID: 7034 RVA: 0x0004B6A4 File Offset: 0x000498A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.x, this.z);
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06001B7B RID: 7035 RVA: 0x0004B6C3 File Offset: 0x000498C3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.x, this.w);
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06001B7C RID: 7036 RVA: 0x0004B6E2 File Offset: 0x000498E2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x0004B701 File Offset: 0x00049901
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06001B7E RID: 7038 RVA: 0x0004B720 File Offset: 0x00049920
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06001B7F RID: 7039 RVA: 0x0004B73F File Offset: 0x0004993F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.y, this.w);
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06001B80 RID: 7040 RVA: 0x0004B75E File Offset: 0x0004995E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06001B81 RID: 7041 RVA: 0x0004B77D File Offset: 0x0004997D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06001B82 RID: 7042 RVA: 0x0004B79C File Offset: 0x0004999C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06001B83 RID: 7043 RVA: 0x0004B7BB File Offset: 0x000499BB
		// (set) Token: 0x06001B84 RID: 7044 RVA: 0x0004B7DA File Offset: 0x000499DA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.z = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06001B85 RID: 7045 RVA: 0x0004B80C File Offset: 0x00049A0C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.w, this.x);
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06001B86 RID: 7046 RVA: 0x0004B82B File Offset: 0x00049A2B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.w, this.y);
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06001B87 RID: 7047 RVA: 0x0004B84A File Offset: 0x00049A4A
		// (set) Token: 0x06001B88 RID: 7048 RVA: 0x0004B869 File Offset: 0x00049A69
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.w = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06001B89 RID: 7049 RVA: 0x0004B89B File Offset: 0x00049A9B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.w, this.w);
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06001B8A RID: 7050 RVA: 0x0004B8BA File Offset: 0x00049ABA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.x, this.x);
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x0004B8D9 File Offset: 0x00049AD9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.x, this.y);
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06001B8C RID: 7052 RVA: 0x0004B8F8 File Offset: 0x00049AF8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.x, this.z);
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06001B8D RID: 7053 RVA: 0x0004B917 File Offset: 0x00049B17
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.x, this.w);
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06001B8E RID: 7054 RVA: 0x0004B936 File Offset: 0x00049B36
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.y, this.x);
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06001B8F RID: 7055 RVA: 0x0004B955 File Offset: 0x00049B55
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.y, this.y);
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06001B90 RID: 7056 RVA: 0x0004B974 File Offset: 0x00049B74
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06001B91 RID: 7057 RVA: 0x0004B993 File Offset: 0x00049B93
		// (set) Token: 0x06001B92 RID: 7058 RVA: 0x0004B9B2 File Offset: 0x00049BB2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.y = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06001B93 RID: 7059 RVA: 0x0004B9E4 File Offset: 0x00049BE4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x0004BA03 File Offset: 0x00049C03
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06001B95 RID: 7061 RVA: 0x0004BA22 File Offset: 0x00049C22
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06001B96 RID: 7062 RVA: 0x0004BA41 File Offset: 0x00049C41
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.z, this.w);
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06001B97 RID: 7063 RVA: 0x0004BA60 File Offset: 0x00049C60
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.w, this.x);
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06001B98 RID: 7064 RVA: 0x0004BA7F File Offset: 0x00049C7F
		// (set) Token: 0x06001B99 RID: 7065 RVA: 0x0004BA9E File Offset: 0x00049C9E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.w = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06001B9A RID: 7066 RVA: 0x0004BAD0 File Offset: 0x00049CD0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.w, this.z);
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06001B9B RID: 7067 RVA: 0x0004BAEF File Offset: 0x00049CEF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.w, this.w);
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06001B9C RID: 7068 RVA: 0x0004BB0E File Offset: 0x00049D0E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.x, this.x);
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06001B9D RID: 7069 RVA: 0x0004BB2D File Offset: 0x00049D2D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.x, this.y);
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06001B9E RID: 7070 RVA: 0x0004BB4C File Offset: 0x00049D4C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.x, this.z);
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x0004BB6B File Offset: 0x00049D6B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.x, this.w);
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x0004BB8A File Offset: 0x00049D8A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.y, this.x);
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x0004BBA9 File Offset: 0x00049DA9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.y, this.y);
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x0004BBC8 File Offset: 0x00049DC8
		// (set) Token: 0x06001BA3 RID: 7075 RVA: 0x0004BBE7 File Offset: 0x00049DE7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.y = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x0004BC19 File Offset: 0x00049E19
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.y, this.w);
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x0004BC38 File Offset: 0x00049E38
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.z, this.x);
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06001BA6 RID: 7078 RVA: 0x0004BC57 File Offset: 0x00049E57
		// (set) Token: 0x06001BA7 RID: 7079 RVA: 0x0004BC76 File Offset: 0x00049E76
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.z = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x0004BCA8 File Offset: 0x00049EA8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.z, this.z);
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x0004BCC7 File Offset: 0x00049EC7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06001BAA RID: 7082 RVA: 0x0004BCE6 File Offset: 0x00049EE6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06001BAB RID: 7083 RVA: 0x0004BD05 File Offset: 0x00049F05
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06001BAC RID: 7084 RVA: 0x0004BD24 File Offset: 0x00049F24
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06001BAD RID: 7085 RVA: 0x0004BD43 File Offset: 0x00049F43
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.w, this.w);
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06001BAE RID: 7086 RVA: 0x0004BD62 File Offset: 0x00049F62
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06001BAF RID: 7087 RVA: 0x0004BD81 File Offset: 0x00049F81
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x0004BDA0 File Offset: 0x00049FA0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.x, this.z);
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06001BB1 RID: 7089 RVA: 0x0004BDBF File Offset: 0x00049FBF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.x, this.w);
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06001BB2 RID: 7090 RVA: 0x0004BDDE File Offset: 0x00049FDE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06001BB3 RID: 7091 RVA: 0x0004BDFD File Offset: 0x00049FFD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06001BB4 RID: 7092 RVA: 0x0004BE1C File Offset: 0x0004A01C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.y, this.z);
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06001BB5 RID: 7093 RVA: 0x0004BE3B File Offset: 0x0004A03B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.y, this.w);
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06001BB6 RID: 7094 RVA: 0x0004BE5A File Offset: 0x0004A05A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06001BB7 RID: 7095 RVA: 0x0004BE79 File Offset: 0x0004A079
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06001BB8 RID: 7096 RVA: 0x0004BE98 File Offset: 0x0004A098
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06001BB9 RID: 7097 RVA: 0x0004BEB7 File Offset: 0x0004A0B7
		// (set) Token: 0x06001BBA RID: 7098 RVA: 0x0004BED6 File Offset: 0x0004A0D6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.z = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06001BBB RID: 7099 RVA: 0x0004BF08 File Offset: 0x0004A108
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06001BBC RID: 7100 RVA: 0x0004BF27 File Offset: 0x0004A127
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.w, this.y);
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06001BBD RID: 7101 RVA: 0x0004BF46 File Offset: 0x0004A146
		// (set) Token: 0x06001BBE RID: 7102 RVA: 0x0004BF65 File Offset: 0x0004A165
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.w = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06001BBF RID: 7103 RVA: 0x0004BF97 File Offset: 0x0004A197
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.w, this.w);
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06001BC0 RID: 7104 RVA: 0x0004BFB6 File Offset: 0x0004A1B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06001BC1 RID: 7105 RVA: 0x0004BFD5 File Offset: 0x0004A1D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06001BC2 RID: 7106 RVA: 0x0004BFF4 File Offset: 0x0004A1F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.x, this.z);
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06001BC3 RID: 7107 RVA: 0x0004C013 File Offset: 0x0004A213
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.x, this.w);
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06001BC4 RID: 7108 RVA: 0x0004C032 File Offset: 0x0004A232
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06001BC5 RID: 7109 RVA: 0x0004C051 File Offset: 0x0004A251
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x0004C070 File Offset: 0x0004A270
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.y, this.z);
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06001BC7 RID: 7111 RVA: 0x0004C08F File Offset: 0x0004A28F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.y, this.w);
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x0004C0AE File Offset: 0x0004A2AE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.z, this.x);
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06001BC9 RID: 7113 RVA: 0x0004C0CD File Offset: 0x0004A2CD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.z, this.y);
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06001BCA RID: 7114 RVA: 0x0004C0EC File Offset: 0x0004A2EC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.z, this.z);
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06001BCB RID: 7115 RVA: 0x0004C10B File Offset: 0x0004A30B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.z, this.w);
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06001BCC RID: 7116 RVA: 0x0004C12A File Offset: 0x0004A32A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.w, this.x);
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06001BCD RID: 7117 RVA: 0x0004C149 File Offset: 0x0004A349
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.w, this.y);
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x0004C168 File Offset: 0x0004A368
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.w, this.z);
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06001BCF RID: 7119 RVA: 0x0004C187 File Offset: 0x0004A387
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.w, this.w);
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x0004C1A6 File Offset: 0x0004A3A6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.x, this.x);
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06001BD1 RID: 7121 RVA: 0x0004C1C5 File Offset: 0x0004A3C5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.x, this.y);
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x0004C1E4 File Offset: 0x0004A3E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.x, this.z);
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06001BD3 RID: 7123 RVA: 0x0004C203 File Offset: 0x0004A403
		// (set) Token: 0x06001BD4 RID: 7124 RVA: 0x0004C222 File Offset: 0x0004A422
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.x = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06001BD5 RID: 7125 RVA: 0x0004C254 File Offset: 0x0004A454
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.y, this.x);
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x0004C273 File Offset: 0x0004A473
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.y, this.y);
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x0004C292 File Offset: 0x0004A492
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.y, this.z);
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06001BD8 RID: 7128 RVA: 0x0004C2B1 File Offset: 0x0004A4B1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.y, this.w);
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06001BD9 RID: 7129 RVA: 0x0004C2D0 File Offset: 0x0004A4D0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.z, this.x);
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x0004C2EF File Offset: 0x0004A4EF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.z, this.y);
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06001BDB RID: 7131 RVA: 0x0004C30E File Offset: 0x0004A50E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.z, this.z);
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06001BDC RID: 7132 RVA: 0x0004C32D File Offset: 0x0004A52D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.z, this.w);
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x0004C34C File Offset: 0x0004A54C
		// (set) Token: 0x06001BDE RID: 7134 RVA: 0x0004C36B File Offset: 0x0004A56B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.w = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06001BDF RID: 7135 RVA: 0x0004C39D File Offset: 0x0004A59D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.w, this.y);
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x0004C3BC File Offset: 0x0004A5BC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.w, this.z);
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x0004C3DB File Offset: 0x0004A5DB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.w, this.w);
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x0004C3FA File Offset: 0x0004A5FA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.x, this.x);
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06001BE3 RID: 7139 RVA: 0x0004C419 File Offset: 0x0004A619
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.x, this.y);
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x0004C438 File Offset: 0x0004A638
		// (set) Token: 0x06001BE5 RID: 7141 RVA: 0x0004C457 File Offset: 0x0004A657
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.x = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06001BE6 RID: 7142 RVA: 0x0004C489 File Offset: 0x0004A689
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.x, this.w);
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06001BE7 RID: 7143 RVA: 0x0004C4A8 File Offset: 0x0004A6A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.y, this.x);
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06001BE8 RID: 7144 RVA: 0x0004C4C7 File Offset: 0x0004A6C7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.y, this.y);
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06001BE9 RID: 7145 RVA: 0x0004C4E6 File Offset: 0x0004A6E6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.y, this.z);
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06001BEA RID: 7146 RVA: 0x0004C505 File Offset: 0x0004A705
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.y, this.w);
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06001BEB RID: 7147 RVA: 0x0004C524 File Offset: 0x0004A724
		// (set) Token: 0x06001BEC RID: 7148 RVA: 0x0004C543 File Offset: 0x0004A743
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.z = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06001BED RID: 7149 RVA: 0x0004C575 File Offset: 0x0004A775
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.z, this.y);
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06001BEE RID: 7150 RVA: 0x0004C594 File Offset: 0x0004A794
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.z, this.z);
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06001BEF RID: 7151 RVA: 0x0004C5B3 File Offset: 0x0004A7B3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.z, this.w);
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x0004C5D2 File Offset: 0x0004A7D2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.w, this.x);
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x0004C5F1 File Offset: 0x0004A7F1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.w, this.y);
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x0004C610 File Offset: 0x0004A810
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.w, this.z);
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06001BF3 RID: 7155 RVA: 0x0004C62F File Offset: 0x0004A82F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.w, this.w);
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x0004C64E File Offset: 0x0004A84E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.x, this.x);
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06001BF5 RID: 7157 RVA: 0x0004C66D File Offset: 0x0004A86D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.x, this.y);
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06001BF6 RID: 7158 RVA: 0x0004C68C File Offset: 0x0004A88C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.x, this.z);
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x0004C6AB File Offset: 0x0004A8AB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.x, this.w);
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x0004C6CA File Offset: 0x0004A8CA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.y, this.x);
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x0004C6E9 File Offset: 0x0004A8E9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.y, this.y);
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06001BFA RID: 7162 RVA: 0x0004C708 File Offset: 0x0004A908
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.y, this.z);
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06001BFB RID: 7163 RVA: 0x0004C727 File Offset: 0x0004A927
		// (set) Token: 0x06001BFC RID: 7164 RVA: 0x0004C746 File Offset: 0x0004A946
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.y = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06001BFD RID: 7165 RVA: 0x0004C778 File Offset: 0x0004A978
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.z, this.x);
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06001BFE RID: 7166 RVA: 0x0004C797 File Offset: 0x0004A997
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.z, this.y);
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06001BFF RID: 7167 RVA: 0x0004C7B6 File Offset: 0x0004A9B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.z, this.z);
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06001C00 RID: 7168 RVA: 0x0004C7D5 File Offset: 0x0004A9D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.z, this.w);
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06001C01 RID: 7169 RVA: 0x0004C7F4 File Offset: 0x0004A9F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.w, this.x);
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x0004C813 File Offset: 0x0004AA13
		// (set) Token: 0x06001C03 RID: 7171 RVA: 0x0004C832 File Offset: 0x0004AA32
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.w = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06001C04 RID: 7172 RVA: 0x0004C864 File Offset: 0x0004AA64
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.w, this.z);
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x0004C883 File Offset: 0x0004AA83
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.w, this.w);
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06001C06 RID: 7174 RVA: 0x0004C8A2 File Offset: 0x0004AAA2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.x, this.x);
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06001C07 RID: 7175 RVA: 0x0004C8C1 File Offset: 0x0004AAC1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.x, this.y);
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06001C08 RID: 7176 RVA: 0x0004C8E0 File Offset: 0x0004AAE0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.x, this.z);
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06001C09 RID: 7177 RVA: 0x0004C8FF File Offset: 0x0004AAFF
		// (set) Token: 0x06001C0A RID: 7178 RVA: 0x0004C91E File Offset: 0x0004AB1E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.x = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06001C0B RID: 7179 RVA: 0x0004C950 File Offset: 0x0004AB50
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.y, this.x);
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06001C0C RID: 7180 RVA: 0x0004C96F File Offset: 0x0004AB6F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.y, this.y);
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06001C0D RID: 7181 RVA: 0x0004C98E File Offset: 0x0004AB8E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.y, this.z);
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06001C0E RID: 7182 RVA: 0x0004C9AD File Offset: 0x0004ABAD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.y, this.w);
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06001C0F RID: 7183 RVA: 0x0004C9CC File Offset: 0x0004ABCC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.z, this.x);
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06001C10 RID: 7184 RVA: 0x0004C9EB File Offset: 0x0004ABEB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.z, this.y);
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x0004CA0A File Offset: 0x0004AC0A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.z, this.z);
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06001C12 RID: 7186 RVA: 0x0004CA29 File Offset: 0x0004AC29
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.z, this.w);
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06001C13 RID: 7187 RVA: 0x0004CA48 File Offset: 0x0004AC48
		// (set) Token: 0x06001C14 RID: 7188 RVA: 0x0004CA67 File Offset: 0x0004AC67
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.w = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06001C15 RID: 7189 RVA: 0x0004CA99 File Offset: 0x0004AC99
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.w, this.y);
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06001C16 RID: 7190 RVA: 0x0004CAB8 File Offset: 0x0004ACB8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.w, this.z);
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06001C17 RID: 7191 RVA: 0x0004CAD7 File Offset: 0x0004ACD7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.w, this.w);
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06001C18 RID: 7192 RVA: 0x0004CAF6 File Offset: 0x0004ACF6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.x, this.x);
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06001C19 RID: 7193 RVA: 0x0004CB15 File Offset: 0x0004AD15
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.x, this.y);
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06001C1A RID: 7194 RVA: 0x0004CB34 File Offset: 0x0004AD34
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.x, this.z);
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06001C1B RID: 7195 RVA: 0x0004CB53 File Offset: 0x0004AD53
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.x, this.w);
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06001C1C RID: 7196 RVA: 0x0004CB72 File Offset: 0x0004AD72
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.y, this.x);
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06001C1D RID: 7197 RVA: 0x0004CB91 File Offset: 0x0004AD91
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.y, this.y);
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06001C1E RID: 7198 RVA: 0x0004CBB0 File Offset: 0x0004ADB0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.y, this.z);
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06001C1F RID: 7199 RVA: 0x0004CBCF File Offset: 0x0004ADCF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.y, this.w);
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06001C20 RID: 7200 RVA: 0x0004CBEE File Offset: 0x0004ADEE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.z, this.x);
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x0004CC0D File Offset: 0x0004AE0D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.z, this.y);
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06001C22 RID: 7202 RVA: 0x0004CC2C File Offset: 0x0004AE2C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.z, this.z);
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06001C23 RID: 7203 RVA: 0x0004CC4B File Offset: 0x0004AE4B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.z, this.w);
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06001C24 RID: 7204 RVA: 0x0004CC6A File Offset: 0x0004AE6A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.w, this.x);
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06001C25 RID: 7205 RVA: 0x0004CC89 File Offset: 0x0004AE89
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.w, this.y);
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06001C26 RID: 7206 RVA: 0x0004CCA8 File Offset: 0x0004AEA8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.w, this.z);
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06001C27 RID: 7207 RVA: 0x0004CCC7 File Offset: 0x0004AEC7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.w, this.w);
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06001C28 RID: 7208 RVA: 0x0004CCE6 File Offset: 0x0004AEE6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.x, this.x);
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06001C29 RID: 7209 RVA: 0x0004CD05 File Offset: 0x0004AF05
		// (set) Token: 0x06001C2A RID: 7210 RVA: 0x0004CD24 File Offset: 0x0004AF24
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.x = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06001C2B RID: 7211 RVA: 0x0004CD56 File Offset: 0x0004AF56
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.x, this.z);
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06001C2C RID: 7212 RVA: 0x0004CD75 File Offset: 0x0004AF75
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.x, this.w);
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06001C2D RID: 7213 RVA: 0x0004CD94 File Offset: 0x0004AF94
		// (set) Token: 0x06001C2E RID: 7214 RVA: 0x0004CDB3 File Offset: 0x0004AFB3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.y = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06001C2F RID: 7215 RVA: 0x0004CDE5 File Offset: 0x0004AFE5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.y, this.y);
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06001C30 RID: 7216 RVA: 0x0004CE04 File Offset: 0x0004B004
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.y, this.z);
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06001C31 RID: 7217 RVA: 0x0004CE23 File Offset: 0x0004B023
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.y, this.w);
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06001C32 RID: 7218 RVA: 0x0004CE42 File Offset: 0x0004B042
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.z, this.x);
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06001C33 RID: 7219 RVA: 0x0004CE61 File Offset: 0x0004B061
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.z, this.y);
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06001C34 RID: 7220 RVA: 0x0004CE80 File Offset: 0x0004B080
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.z, this.z);
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06001C35 RID: 7221 RVA: 0x0004CE9F File Offset: 0x0004B09F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06001C36 RID: 7222 RVA: 0x0004CEBE File Offset: 0x0004B0BE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06001C37 RID: 7223 RVA: 0x0004CEDD File Offset: 0x0004B0DD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06001C38 RID: 7224 RVA: 0x0004CEFC File Offset: 0x0004B0FC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06001C39 RID: 7225 RVA: 0x0004CF1B File Offset: 0x0004B11B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.w, this.w);
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06001C3A RID: 7226 RVA: 0x0004CF3A File Offset: 0x0004B13A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.x, this.x);
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06001C3B RID: 7227 RVA: 0x0004CF59 File Offset: 0x0004B159
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.x, this.y);
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06001C3C RID: 7228 RVA: 0x0004CF78 File Offset: 0x0004B178
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.x, this.z);
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06001C3D RID: 7229 RVA: 0x0004CF97 File Offset: 0x0004B197
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.x, this.w);
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06001C3E RID: 7230 RVA: 0x0004CFB6 File Offset: 0x0004B1B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.y, this.x);
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06001C3F RID: 7231 RVA: 0x0004CFD5 File Offset: 0x0004B1D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.y, this.y);
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06001C40 RID: 7232 RVA: 0x0004CFF4 File Offset: 0x0004B1F4
		// (set) Token: 0x06001C41 RID: 7233 RVA: 0x0004D013 File Offset: 0x0004B213
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.y = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06001C42 RID: 7234 RVA: 0x0004D045 File Offset: 0x0004B245
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.y, this.w);
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06001C43 RID: 7235 RVA: 0x0004D064 File Offset: 0x0004B264
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06001C44 RID: 7236 RVA: 0x0004D083 File Offset: 0x0004B283
		// (set) Token: 0x06001C45 RID: 7237 RVA: 0x0004D0A2 File Offset: 0x0004B2A2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.z = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06001C46 RID: 7238 RVA: 0x0004D0D4 File Offset: 0x0004B2D4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06001C47 RID: 7239 RVA: 0x0004D0F3 File Offset: 0x0004B2F3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.z, this.w);
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06001C48 RID: 7240 RVA: 0x0004D112 File Offset: 0x0004B312
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06001C49 RID: 7241 RVA: 0x0004D131 File Offset: 0x0004B331
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.w, this.y);
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06001C4A RID: 7242 RVA: 0x0004D150 File Offset: 0x0004B350
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.w, this.z);
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06001C4B RID: 7243 RVA: 0x0004D16F File Offset: 0x0004B36F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.w, this.w);
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06001C4C RID: 7244 RVA: 0x0004D18E File Offset: 0x0004B38E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.x, this.x);
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06001C4D RID: 7245 RVA: 0x0004D1AD File Offset: 0x0004B3AD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.x, this.y);
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06001C4E RID: 7246 RVA: 0x0004D1CC File Offset: 0x0004B3CC
		// (set) Token: 0x06001C4F RID: 7247 RVA: 0x0004D1EB File Offset: 0x0004B3EB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.x = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06001C50 RID: 7248 RVA: 0x0004D21D File Offset: 0x0004B41D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.x, this.w);
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06001C51 RID: 7249 RVA: 0x0004D23C File Offset: 0x0004B43C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.y, this.x);
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06001C52 RID: 7250 RVA: 0x0004D25B File Offset: 0x0004B45B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.y, this.y);
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06001C53 RID: 7251 RVA: 0x0004D27A File Offset: 0x0004B47A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06001C54 RID: 7252 RVA: 0x0004D299 File Offset: 0x0004B499
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.y, this.w);
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06001C55 RID: 7253 RVA: 0x0004D2B8 File Offset: 0x0004B4B8
		// (set) Token: 0x06001C56 RID: 7254 RVA: 0x0004D2D7 File Offset: 0x0004B4D7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.z = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06001C57 RID: 7255 RVA: 0x0004D309 File Offset: 0x0004B509
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06001C58 RID: 7256 RVA: 0x0004D328 File Offset: 0x0004B528
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06001C59 RID: 7257 RVA: 0x0004D347 File Offset: 0x0004B547
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.z, this.w);
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06001C5A RID: 7258 RVA: 0x0004D366 File Offset: 0x0004B566
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.w, this.x);
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06001C5B RID: 7259 RVA: 0x0004D385 File Offset: 0x0004B585
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.w, this.y);
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06001C5C RID: 7260 RVA: 0x0004D3A4 File Offset: 0x0004B5A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.w, this.z);
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06001C5D RID: 7261 RVA: 0x0004D3C3 File Offset: 0x0004B5C3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.w, this.w);
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06001C5E RID: 7262 RVA: 0x0004D3E2 File Offset: 0x0004B5E2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.x, this.x);
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06001C5F RID: 7263 RVA: 0x0004D401 File Offset: 0x0004B601
		// (set) Token: 0x06001C60 RID: 7264 RVA: 0x0004D420 File Offset: 0x0004B620
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.x = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06001C61 RID: 7265 RVA: 0x0004D452 File Offset: 0x0004B652
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.x, this.z);
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06001C62 RID: 7266 RVA: 0x0004D471 File Offset: 0x0004B671
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.x, this.w);
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06001C63 RID: 7267 RVA: 0x0004D490 File Offset: 0x0004B690
		// (set) Token: 0x06001C64 RID: 7268 RVA: 0x0004D4AF File Offset: 0x0004B6AF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.y = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06001C65 RID: 7269 RVA: 0x0004D4E1 File Offset: 0x0004B6E1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.y, this.y);
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06001C66 RID: 7270 RVA: 0x0004D500 File Offset: 0x0004B700
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06001C67 RID: 7271 RVA: 0x0004D51F File Offset: 0x0004B71F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.y, this.w);
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06001C68 RID: 7272 RVA: 0x0004D53E File Offset: 0x0004B73E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06001C69 RID: 7273 RVA: 0x0004D55D File Offset: 0x0004B75D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06001C6A RID: 7274 RVA: 0x0004D57C File Offset: 0x0004B77C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06001C6B RID: 7275 RVA: 0x0004D59B File Offset: 0x0004B79B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.z, this.w);
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06001C6C RID: 7276 RVA: 0x0004D5BA File Offset: 0x0004B7BA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.w, this.x);
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06001C6D RID: 7277 RVA: 0x0004D5D9 File Offset: 0x0004B7D9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.w, this.y);
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06001C6E RID: 7278 RVA: 0x0004D5F8 File Offset: 0x0004B7F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.w, this.z);
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06001C6F RID: 7279 RVA: 0x0004D617 File Offset: 0x0004B817
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.w, this.w);
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06001C70 RID: 7280 RVA: 0x0004D636 File Offset: 0x0004B836
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.x, this.x);
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06001C71 RID: 7281 RVA: 0x0004D655 File Offset: 0x0004B855
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.x, this.y);
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x0004D674 File Offset: 0x0004B874
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.x, this.z);
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06001C73 RID: 7283 RVA: 0x0004D693 File Offset: 0x0004B893
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.x, this.w);
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06001C74 RID: 7284 RVA: 0x0004D6B2 File Offset: 0x0004B8B2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.y, this.x);
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06001C75 RID: 7285 RVA: 0x0004D6D1 File Offset: 0x0004B8D1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.y, this.y);
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06001C76 RID: 7286 RVA: 0x0004D6F0 File Offset: 0x0004B8F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.y, this.z);
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06001C77 RID: 7287 RVA: 0x0004D70F File Offset: 0x0004B90F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.y, this.w);
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06001C78 RID: 7288 RVA: 0x0004D72E File Offset: 0x0004B92E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.z, this.x);
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06001C79 RID: 7289 RVA: 0x0004D74D File Offset: 0x0004B94D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.z, this.y);
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06001C7A RID: 7290 RVA: 0x0004D76C File Offset: 0x0004B96C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.z, this.z);
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06001C7B RID: 7291 RVA: 0x0004D78B File Offset: 0x0004B98B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06001C7C RID: 7292 RVA: 0x0004D7AA File Offset: 0x0004B9AA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06001C7D RID: 7293 RVA: 0x0004D7C9 File Offset: 0x0004B9C9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06001C7E RID: 7294 RVA: 0x0004D7E8 File Offset: 0x0004B9E8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06001C7F RID: 7295 RVA: 0x0004D807 File Offset: 0x0004BA07
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.w, this.w);
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x0004D826 File Offset: 0x0004BA26
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.x, this.x);
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06001C81 RID: 7297 RVA: 0x0004D83F File Offset: 0x0004BA3F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.x, this.y);
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x0004D858 File Offset: 0x0004BA58
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.x, this.z);
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x0004D871 File Offset: 0x0004BA71
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.x, this.w);
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06001C84 RID: 7300 RVA: 0x0004D88A File Offset: 0x0004BA8A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.y, this.x);
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06001C85 RID: 7301 RVA: 0x0004D8A3 File Offset: 0x0004BAA3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.y, this.y);
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06001C86 RID: 7302 RVA: 0x0004D8BC File Offset: 0x0004BABC
		// (set) Token: 0x06001C87 RID: 7303 RVA: 0x0004D8D5 File Offset: 0x0004BAD5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06001C88 RID: 7304 RVA: 0x0004D8FB File Offset: 0x0004BAFB
		// (set) Token: 0x06001C89 RID: 7305 RVA: 0x0004D914 File Offset: 0x0004BB14
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06001C8A RID: 7306 RVA: 0x0004D93A File Offset: 0x0004BB3A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.z, this.x);
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06001C8B RID: 7307 RVA: 0x0004D953 File Offset: 0x0004BB53
		// (set) Token: 0x06001C8C RID: 7308 RVA: 0x0004D96C File Offset: 0x0004BB6C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06001C8D RID: 7309 RVA: 0x0004D992 File Offset: 0x0004BB92
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.z, this.z);
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06001C8E RID: 7310 RVA: 0x0004D9AB File Offset: 0x0004BBAB
		// (set) Token: 0x06001C8F RID: 7311 RVA: 0x0004D9C4 File Offset: 0x0004BBC4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06001C90 RID: 7312 RVA: 0x0004D9EA File Offset: 0x0004BBEA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.w, this.x);
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06001C91 RID: 7313 RVA: 0x0004DA03 File Offset: 0x0004BC03
		// (set) Token: 0x06001C92 RID: 7314 RVA: 0x0004DA1C File Offset: 0x0004BC1C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06001C93 RID: 7315 RVA: 0x0004DA42 File Offset: 0x0004BC42
		// (set) Token: 0x06001C94 RID: 7316 RVA: 0x0004DA5B File Offset: 0x0004BC5B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06001C95 RID: 7317 RVA: 0x0004DA81 File Offset: 0x0004BC81
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.w, this.w);
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06001C96 RID: 7318 RVA: 0x0004DA9A File Offset: 0x0004BC9A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.x, this.x);
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06001C97 RID: 7319 RVA: 0x0004DAB3 File Offset: 0x0004BCB3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.x, this.y);
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06001C98 RID: 7320 RVA: 0x0004DACC File Offset: 0x0004BCCC
		// (set) Token: 0x06001C99 RID: 7321 RVA: 0x0004DAE5 File Offset: 0x0004BCE5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06001C9A RID: 7322 RVA: 0x0004DB0B File Offset: 0x0004BD0B
		// (set) Token: 0x06001C9B RID: 7323 RVA: 0x0004DB24 File Offset: 0x0004BD24
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06001C9C RID: 7324 RVA: 0x0004DB4A File Offset: 0x0004BD4A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.y, this.x);
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06001C9D RID: 7325 RVA: 0x0004DB63 File Offset: 0x0004BD63
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.y, this.y);
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06001C9E RID: 7326 RVA: 0x0004DB7C File Offset: 0x0004BD7C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.y, this.z);
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06001C9F RID: 7327 RVA: 0x0004DB95 File Offset: 0x0004BD95
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.y, this.w);
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06001CA0 RID: 7328 RVA: 0x0004DBAE File Offset: 0x0004BDAE
		// (set) Token: 0x06001CA1 RID: 7329 RVA: 0x0004DBC7 File Offset: 0x0004BDC7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06001CA2 RID: 7330 RVA: 0x0004DBED File Offset: 0x0004BDED
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.z, this.y);
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06001CA3 RID: 7331 RVA: 0x0004DC06 File Offset: 0x0004BE06
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.z, this.z);
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06001CA4 RID: 7332 RVA: 0x0004DC1F File Offset: 0x0004BE1F
		// (set) Token: 0x06001CA5 RID: 7333 RVA: 0x0004DC38 File Offset: 0x0004BE38
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06001CA6 RID: 7334 RVA: 0x0004DC5E File Offset: 0x0004BE5E
		// (set) Token: 0x06001CA7 RID: 7335 RVA: 0x0004DC77 File Offset: 0x0004BE77
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 ywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06001CA8 RID: 7336 RVA: 0x0004DC9D File Offset: 0x0004BE9D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 ywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.w, this.y);
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06001CA9 RID: 7337 RVA: 0x0004DCB6 File Offset: 0x0004BEB6
		// (set) Token: 0x06001CAA RID: 7338 RVA: 0x0004DCCF File Offset: 0x0004BECF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 ywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06001CAB RID: 7339 RVA: 0x0004DCF5 File Offset: 0x0004BEF5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.w, this.w);
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06001CAC RID: 7340 RVA: 0x0004DD0E File Offset: 0x0004BF0E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.x, this.x);
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06001CAD RID: 7341 RVA: 0x0004DD27 File Offset: 0x0004BF27
		// (set) Token: 0x06001CAE RID: 7342 RVA: 0x0004DD40 File Offset: 0x0004BF40
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06001CAF RID: 7343 RVA: 0x0004DD66 File Offset: 0x0004BF66
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.x, this.z);
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06001CB0 RID: 7344 RVA: 0x0004DD7F File Offset: 0x0004BF7F
		// (set) Token: 0x06001CB1 RID: 7345 RVA: 0x0004DD98 File Offset: 0x0004BF98
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06001CB2 RID: 7346 RVA: 0x0004DDBE File Offset: 0x0004BFBE
		// (set) Token: 0x06001CB3 RID: 7347 RVA: 0x0004DDD7 File Offset: 0x0004BFD7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06001CB4 RID: 7348 RVA: 0x0004DDFD File Offset: 0x0004BFFD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.y, this.y);
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06001CB5 RID: 7349 RVA: 0x0004DE16 File Offset: 0x0004C016
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.y, this.z);
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x0004DE2F File Offset: 0x0004C02F
		// (set) Token: 0x06001CB7 RID: 7351 RVA: 0x0004DE48 File Offset: 0x0004C048
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x0004DE6E File Offset: 0x0004C06E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.z, this.x);
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06001CB9 RID: 7353 RVA: 0x0004DE87 File Offset: 0x0004C087
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.z, this.y);
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x0004DEA0 File Offset: 0x0004C0A0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.z, this.z);
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06001CBB RID: 7355 RVA: 0x0004DEB9 File Offset: 0x0004C0B9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.z, this.w);
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06001CBC RID: 7356 RVA: 0x0004DED2 File Offset: 0x0004C0D2
		// (set) Token: 0x06001CBD RID: 7357 RVA: 0x0004DEEB File Offset: 0x0004C0EB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06001CBE RID: 7358 RVA: 0x0004DF11 File Offset: 0x0004C111
		// (set) Token: 0x06001CBF RID: 7359 RVA: 0x0004DF2A File Offset: 0x0004C12A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06001CC0 RID: 7360 RVA: 0x0004DF50 File Offset: 0x0004C150
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.w, this.z);
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06001CC1 RID: 7361 RVA: 0x0004DF69 File Offset: 0x0004C169
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.w, this.w);
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x0004DF82 File Offset: 0x0004C182
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.x, this.x);
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06001CC3 RID: 7363 RVA: 0x0004DF9B File Offset: 0x0004C19B
		// (set) Token: 0x06001CC4 RID: 7364 RVA: 0x0004DFB4 File Offset: 0x0004C1B4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06001CC5 RID: 7365 RVA: 0x0004DFDA File Offset: 0x0004C1DA
		// (set) Token: 0x06001CC6 RID: 7366 RVA: 0x0004DFF3 File Offset: 0x0004C1F3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06001CC7 RID: 7367 RVA: 0x0004E019 File Offset: 0x0004C219
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.x, this.w);
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x0004E032 File Offset: 0x0004C232
		// (set) Token: 0x06001CC9 RID: 7369 RVA: 0x0004E04B File Offset: 0x0004C24B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06001CCA RID: 7370 RVA: 0x0004E071 File Offset: 0x0004C271
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.y, this.y);
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06001CCB RID: 7371 RVA: 0x0004E08A File Offset: 0x0004C28A
		// (set) Token: 0x06001CCC RID: 7372 RVA: 0x0004E0A3 File Offset: 0x0004C2A3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06001CCD RID: 7373 RVA: 0x0004E0C9 File Offset: 0x0004C2C9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.y, this.w);
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06001CCE RID: 7374 RVA: 0x0004E0E2 File Offset: 0x0004C2E2
		// (set) Token: 0x06001CCF RID: 7375 RVA: 0x0004E0FB File Offset: 0x0004C2FB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x0004E121 File Offset: 0x0004C321
		// (set) Token: 0x06001CD1 RID: 7377 RVA: 0x0004E13A File Offset: 0x0004C33A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x0004E160 File Offset: 0x0004C360
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.z, this.z);
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x0004E179 File Offset: 0x0004C379
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.z, this.w);
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06001CD4 RID: 7380 RVA: 0x0004E192 File Offset: 0x0004C392
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.w, this.x);
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06001CD5 RID: 7381 RVA: 0x0004E1AB File Offset: 0x0004C3AB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.w, this.y);
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x0004E1C4 File Offset: 0x0004C3C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.w, this.z);
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06001CD7 RID: 7383 RVA: 0x0004E1DD File Offset: 0x0004C3DD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 www
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.w, this.w);
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06001CD8 RID: 7384 RVA: 0x0004E1F6 File Offset: 0x0004C3F6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.x, this.x);
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06001CD9 RID: 7385 RVA: 0x0004E209 File Offset: 0x0004C409
		// (set) Token: 0x06001CDA RID: 7386 RVA: 0x0004E21C File Offset: 0x0004C41C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 xy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06001CDB RID: 7387 RVA: 0x0004E236 File Offset: 0x0004C436
		// (set) Token: 0x06001CDC RID: 7388 RVA: 0x0004E249 File Offset: 0x0004C449
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 xz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06001CDD RID: 7389 RVA: 0x0004E263 File Offset: 0x0004C463
		// (set) Token: 0x06001CDE RID: 7390 RVA: 0x0004E276 File Offset: 0x0004C476
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 xw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06001CDF RID: 7391 RVA: 0x0004E290 File Offset: 0x0004C490
		// (set) Token: 0x06001CE0 RID: 7392 RVA: 0x0004E2A3 File Offset: 0x0004C4A3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 yx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06001CE1 RID: 7393 RVA: 0x0004E2BD File Offset: 0x0004C4BD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.y, this.y);
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06001CE2 RID: 7394 RVA: 0x0004E2D0 File Offset: 0x0004C4D0
		// (set) Token: 0x06001CE3 RID: 7395 RVA: 0x0004E2E3 File Offset: 0x0004C4E3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 yz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x0004E2FD File Offset: 0x0004C4FD
		// (set) Token: 0x06001CE5 RID: 7397 RVA: 0x0004E310 File Offset: 0x0004C510
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 yw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06001CE6 RID: 7398 RVA: 0x0004E32A File Offset: 0x0004C52A
		// (set) Token: 0x06001CE7 RID: 7399 RVA: 0x0004E33D File Offset: 0x0004C53D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 zx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06001CE8 RID: 7400 RVA: 0x0004E357 File Offset: 0x0004C557
		// (set) Token: 0x06001CE9 RID: 7401 RVA: 0x0004E36A File Offset: 0x0004C56A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 zy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06001CEA RID: 7402 RVA: 0x0004E384 File Offset: 0x0004C584
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 zz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.z, this.z);
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06001CEB RID: 7403 RVA: 0x0004E397 File Offset: 0x0004C597
		// (set) Token: 0x06001CEC RID: 7404 RVA: 0x0004E3AA File Offset: 0x0004C5AA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 zw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06001CED RID: 7405 RVA: 0x0004E3C4 File Offset: 0x0004C5C4
		// (set) Token: 0x06001CEE RID: 7406 RVA: 0x0004E3D7 File Offset: 0x0004C5D7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 wx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06001CEF RID: 7407 RVA: 0x0004E3F1 File Offset: 0x0004C5F1
		// (set) Token: 0x06001CF0 RID: 7408 RVA: 0x0004E404 File Offset: 0x0004C604
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 wy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06001CF1 RID: 7409 RVA: 0x0004E41E File Offset: 0x0004C61E
		// (set) Token: 0x06001CF2 RID: 7410 RVA: 0x0004E431 File Offset: 0x0004C631
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 wz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06001CF3 RID: 7411 RVA: 0x0004E44B File Offset: 0x0004C64B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 ww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.w, this.w);
			}
		}

		// Token: 0x17000999 RID: 2457
		public unsafe int this[int index]
		{
			get
			{
				fixed (int4* ptr = &this)
				{
					return ((int*)ptr)[index];
				}
			}
			set
			{
				fixed (int* ptr = &this.x)
				{
					ptr[index] = value;
				}
			}
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x0004E498 File Offset: 0x0004C698
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(int4 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y && this.z == rhs.z && this.w == rhs.w;
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x0004E4D4 File Offset: 0x0004C6D4
		public override bool Equals(object o)
		{
			if (o is int4)
			{
				int4 rhs = (int4)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x0004E4F9 File Offset: 0x0004C6F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x0004E508 File Offset: 0x0004C708
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("int4({0}, {1}, {2}, {3})", new object[]
			{
				this.x,
				this.y,
				this.z,
				this.w
			});
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x0004E560 File Offset: 0x0004C760
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("int4({0}, {1}, {2}, {3})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.z.ToString(format, formatProvider),
				this.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x040000D0 RID: 208
		public int x;

		// Token: 0x040000D1 RID: 209
		public int y;

		// Token: 0x040000D2 RID: 210
		public int z;

		// Token: 0x040000D3 RID: 211
		public int w;

		// Token: 0x040000D4 RID: 212
		public static readonly int4 zero;

		// Token: 0x0200005F RID: 95
		internal sealed class DebuggerProxy
		{
			// Token: 0x06002475 RID: 9333 RVA: 0x000676E0 File Offset: 0x000658E0
			public DebuggerProxy(int4 v)
			{
				this.x = v.x;
				this.y = v.y;
				this.z = v.z;
				this.w = v.w;
			}

			// Token: 0x0400015C RID: 348
			public int x;

			// Token: 0x0400015D RID: 349
			public int y;

			// Token: 0x0400015E RID: 350
			public int z;

			// Token: 0x0400015F RID: 351
			public int w;
		}
	}
}
