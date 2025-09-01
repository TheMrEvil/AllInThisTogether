using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000004 RID: 4
	[Il2CppEagerStaticClassConstruction]
	public static class math
	{
		// Token: 0x0600003A RID: 58 RVA: 0x0000263C File Offset: 0x0000083C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 bool2(bool x, bool y)
		{
			return new bool2(x, y);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002645 File Offset: 0x00000845
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 bool2(bool2 xy)
		{
			return new bool2(xy);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000264D File Offset: 0x0000084D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 bool2(bool v)
		{
			return new bool2(v);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002655 File Offset: 0x00000855
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(bool2 v)
		{
			return math.csum(math.select(math.uint2(2426570171U, 1561977301U), math.uint2(4205774813U, 1650214333U), v));
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002680 File Offset: 0x00000880
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(bool2 v)
		{
			return math.select(math.uint2(3388112843U, 1831150513U), math.uint2(1848374953U, 3430200247U), v);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000026A6 File Offset: 0x000008A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool shuffle(bool2 left, bool2 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000026B0 File Offset: 0x000008B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 shuffle(bool2 left, bool2 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.bool2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000026C7 File Offset: 0x000008C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 shuffle(bool2 left, bool2 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.bool3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000026E7 File Offset: 0x000008E7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 shuffle(bool2 left, bool2 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.bool4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002710 File Offset: 0x00000910
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool select_shuffle_component(bool2 a, bool2 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			}
			throw new ArgumentException("Invalid shuffle component: " + component.ToString());
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002775 File Offset: 0x00000975
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 bool2x2(bool2 c0, bool2 c1)
		{
			return new bool2x2(c0, c1);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000277E File Offset: 0x0000097E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 bool2x2(bool m00, bool m01, bool m10, bool m11)
		{
			return new bool2x2(m00, m01, m10, m11);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002789 File Offset: 0x00000989
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 bool2x2(bool v)
		{
			return new bool2x2(v);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002791 File Offset: 0x00000991
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 transpose(bool2x2 v)
		{
			return math.bool2x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000027C4 File Offset: 0x000009C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(bool2x2 v)
		{
			return math.csum(math.select(math.uint2(2062756937U, 2920485769U), math.uint2(1562056283U, 2265541847U), v.c0) + math.select(math.uint2(1283419601U, 1210229737U), math.uint2(2864955997U, 3525118277U), v.c1));
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002830 File Offset: 0x00000A30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(bool2x2 v)
		{
			return math.select(math.uint2(2298260269U, 1632478733U), math.uint2(1537393931U, 2353355467U), v.c0) + math.select(math.uint2(3441847433U, 4052036147U), math.uint2(2011389559U, 2252224297U), v.c1);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002894 File Offset: 0x00000A94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 bool2x3(bool2 c0, bool2 c1, bool2 c2)
		{
			return new bool2x3(c0, c1, c2);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000289E File Offset: 0x00000A9E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 bool2x3(bool m00, bool m01, bool m02, bool m10, bool m11, bool m12)
		{
			return new bool2x3(m00, m01, m02, m10, m11, m12);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000028AD File Offset: 0x00000AAD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 bool2x3(bool v)
		{
			return new bool2x3(v);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000028B8 File Offset: 0x00000AB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 transpose(bool2x3 v)
		{
			return math.bool3x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000290C File Offset: 0x00000B0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(bool2x3 v)
		{
			return math.csum(math.select(math.uint2(2078515003U, 4206465343U), math.uint2(3025146473U, 3763046909U), v.c0) + math.select(math.uint2(3678265601U, 2070747979U), math.uint2(1480171127U, 1588341193U), v.c1) + math.select(math.uint2(4234155257U, 1811310911U), math.uint2(2635799963U, 4165137857U), v.c2));
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000029A4 File Offset: 0x00000BA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(bool2x3 v)
		{
			return math.select(math.uint2(2759770933U, 2759319383U), math.uint2(3299952959U, 3121178323U), v.c0) + math.select(math.uint2(2948522579U, 1531026433U), math.uint2(1365086453U, 3969870067U), v.c1) + math.select(math.uint2(4192899797U, 3271228601U), math.uint2(1634639009U, 3318036811U), v.c2);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A36 File Offset: 0x00000C36
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 bool2x4(bool2 c0, bool2 c1, bool2 c2, bool2 c3)
		{
			return new bool2x4(c0, c1, c2, c3);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002A41 File Offset: 0x00000C41
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 bool2x4(bool m00, bool m01, bool m02, bool m03, bool m10, bool m11, bool m12, bool m13)
		{
			return new bool2x4(m00, m01, m02, m03, m10, m11, m12, m13);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002A54 File Offset: 0x00000C54
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 bool2x4(bool v)
		{
			return new bool2x4(v);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002A5C File Offset: 0x00000C5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 transpose(bool2x4 v)
		{
			return math.bool4x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y, v.c3.x, v.c3.y);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002AC8 File Offset: 0x00000CC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(bool2x4 v)
		{
			return math.csum(math.select(math.uint2(1168253063U, 4228926523U), math.uint2(1610574617U, 1584185147U), v.c0) + math.select(math.uint2(3041325733U, 3150930919U), math.uint2(3309258581U, 1770373673U), v.c1) + math.select(math.uint2(3778261171U, 3286279097U), math.uint2(4264629071U, 1898591447U), v.c2) + math.select(math.uint2(2641864091U, 1229113913U), math.uint2(3020867117U, 1449055807U), v.c3));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002B90 File Offset: 0x00000D90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(bool2x4 v)
		{
			return math.select(math.uint2(2479033387U, 3702457169U), math.uint2(1845824257U, 1963973621U), v.c0) + math.select(math.uint2(2134758553U, 1391111867U), math.uint2(1167706003U, 2209736489U), v.c1) + math.select(math.uint2(3261535807U, 1740411209U), math.uint2(2910609089U, 2183822701U), v.c2) + math.select(math.uint2(3029516053U, 3547472099U), math.uint2(2057487037U, 3781937309U), v.c3);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002C50 File Offset: 0x00000E50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 bool3(bool x, bool y, bool z)
		{
			return new bool3(x, y, z);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002C5A File Offset: 0x00000E5A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 bool3(bool x, bool2 yz)
		{
			return new bool3(x, yz);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002C63 File Offset: 0x00000E63
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 bool3(bool2 xy, bool z)
		{
			return new bool3(xy, z);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002C6C File Offset: 0x00000E6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 bool3(bool3 xyz)
		{
			return new bool3(xyz);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002C74 File Offset: 0x00000E74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 bool3(bool v)
		{
			return new bool3(v);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002C7C File Offset: 0x00000E7C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(bool3 v)
		{
			return math.csum(math.select(math.uint3(2716413241U, 1166264321U, 2503385333U), math.uint3(2944493077U, 2599999021U, 3814721321U), v));
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002CB1 File Offset: 0x00000EB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(bool3 v)
		{
			return math.select(math.uint3(1595355149U, 1728931849U, 2062756937U), math.uint3(2920485769U, 1562056283U, 2265541847U), v);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002CE1 File Offset: 0x00000EE1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool shuffle(bool3 left, bool3 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002CEB File Offset: 0x00000EEB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 shuffle(bool3 left, bool3 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.bool2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002D02 File Offset: 0x00000F02
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 shuffle(bool3 left, bool3 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.bool3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002D22 File Offset: 0x00000F22
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 shuffle(bool3 left, bool3 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.bool4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002D4C File Offset: 0x00000F4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool select_shuffle_component(bool3 a, bool3 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.LeftZ:
				return a.z;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			case math.ShuffleComponent.RightZ:
				return b.z;
			}
			throw new ArgumentException("Invalid shuffle component: " + component.ToString());
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002DC3 File Offset: 0x00000FC3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 bool3x2(bool3 c0, bool3 c1)
		{
			return new bool3x2(c0, c1);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002DCC File Offset: 0x00000FCC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 bool3x2(bool m00, bool m01, bool m10, bool m11, bool m20, bool m21)
		{
			return new bool3x2(m00, m01, m10, m11, m20, m21);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002DDB File Offset: 0x00000FDB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 bool3x2(bool v)
		{
			return new bool3x2(v);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002DE4 File Offset: 0x00000FE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 transpose(bool3x2 v)
		{
			return math.bool2x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002E38 File Offset: 0x00001038
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(bool3x2 v)
		{
			return math.csum(math.select(math.uint3(2627668003U, 1520214331U, 2949502447U), math.uint3(2827819133U, 3480140317U, 2642994593U), v.c0) + math.select(math.uint3(3940484981U, 1954192763U, 1091696537U), math.uint3(3052428017U, 4253034763U, 2338696631U), v.c1));
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002EB8 File Offset: 0x000010B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(bool3x2 v)
		{
			return math.select(math.uint3(3757372771U, 1885959949U, 3508684087U), math.uint3(3919501043U, 1209161033U, 4007793211U), v.c0) + math.select(math.uint3(3819806693U, 3458005183U, 2078515003U), math.uint3(4206465343U, 3025146473U, 3763046909U), v.c1);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002F30 File Offset: 0x00001130
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 bool3x3(bool3 c0, bool3 c1, bool3 c2)
		{
			return new bool3x3(c0, c1, c2);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002F3C File Offset: 0x0000113C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 bool3x3(bool m00, bool m01, bool m02, bool m10, bool m11, bool m12, bool m20, bool m21, bool m22)
		{
			return new bool3x3(m00, m01, m02, m10, m11, m12, m20, m21, m22);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002F5C File Offset: 0x0000115C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 bool3x3(bool v)
		{
			return new bool3x3(v);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002F64 File Offset: 0x00001164
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 transpose(bool3x3 v)
		{
			return math.bool3x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002FDC File Offset: 0x000011DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(bool3x3 v)
		{
			return math.csum(math.select(math.uint3(3881277847U, 4017968839U, 1727237899U), math.uint3(1648514723U, 1385344481U, 3538260197U), v.c0) + math.select(math.uint3(4066109527U, 2613148903U, 3367528529U), math.uint3(1678332449U, 2918459647U, 2744611081U), v.c1) + math.select(math.uint3(1952372791U, 2631698677U, 4200781601U), math.uint3(2119021007U, 1760485621U, 3157985881U), v.c2));
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003094 File Offset: 0x00001294
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(bool3x3 v)
		{
			return math.select(math.uint3(2171534173U, 2723054263U, 1168253063U), math.uint3(4228926523U, 1610574617U, 1584185147U), v.c0) + math.select(math.uint3(3041325733U, 3150930919U, 3309258581U), math.uint3(1770373673U, 3778261171U, 3286279097U), v.c1) + math.select(math.uint3(4264629071U, 1898591447U, 2641864091U), math.uint3(1229113913U, 3020867117U, 1449055807U), v.c2);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003144 File Offset: 0x00001344
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 bool3x4(bool3 c0, bool3 c1, bool3 c2, bool3 c3)
		{
			return new bool3x4(c0, c1, c2, c3);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003150 File Offset: 0x00001350
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 bool3x4(bool m00, bool m01, bool m02, bool m03, bool m10, bool m11, bool m12, bool m13, bool m20, bool m21, bool m22, bool m23)
		{
			return new bool3x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003176 File Offset: 0x00001376
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 bool3x4(bool v)
		{
			return new bool3x4(v);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003180 File Offset: 0x00001380
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 transpose(bool3x4 v)
		{
			return math.bool4x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z, v.c3.x, v.c3.y, v.c3.z);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003218 File Offset: 0x00001418
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(bool3x4 v)
		{
			return math.csum(math.select(math.uint3(2209710647U, 2201894441U, 2849577407U), math.uint3(3287031191U, 3098675399U, 1564399943U), v.c0) + math.select(math.uint3(1148435377U, 3416333663U, 1750611407U), math.uint3(3285396193U, 3110507567U, 4271396531U), v.c1) + math.select(math.uint3(4198118021U, 2908068253U, 3705492289U), math.uint3(2497566569U, 2716413241U, 1166264321U), v.c2) + math.select(math.uint3(2503385333U, 2944493077U, 2599999021U), math.uint3(3814721321U, 1595355149U, 1728931849U), v.c3));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003308 File Offset: 0x00001508
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(bool3x4 v)
		{
			return math.select(math.uint3(2062756937U, 2920485769U, 1562056283U), math.uint3(2265541847U, 1283419601U, 1210229737U), v.c0) + math.select(math.uint3(2864955997U, 3525118277U, 2298260269U), math.uint3(1632478733U, 1537393931U, 2353355467U), v.c1) + math.select(math.uint3(3441847433U, 4052036147U, 2011389559U), math.uint3(2252224297U, 3784421429U, 1750626223U), v.c2) + math.select(math.uint3(3571447507U, 3412283213U, 2601761069U), math.uint3(1254033427U, 2248573027U, 3612677113U), v.c3);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000033F0 File Offset: 0x000015F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 bool4(bool x, bool y, bool z, bool w)
		{
			return new bool4(x, y, z, w);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000033FB File Offset: 0x000015FB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 bool4(bool x, bool y, bool2 zw)
		{
			return new bool4(x, y, zw);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003405 File Offset: 0x00001605
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 bool4(bool x, bool2 yz, bool w)
		{
			return new bool4(x, yz, w);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000340F File Offset: 0x0000160F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 bool4(bool x, bool3 yzw)
		{
			return new bool4(x, yzw);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003418 File Offset: 0x00001618
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 bool4(bool2 xy, bool z, bool w)
		{
			return new bool4(xy, z, w);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003422 File Offset: 0x00001622
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 bool4(bool2 xy, bool2 zw)
		{
			return new bool4(xy, zw);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000342B File Offset: 0x0000162B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 bool4(bool3 xyz, bool w)
		{
			return new bool4(xyz, w);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003434 File Offset: 0x00001634
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 bool4(bool4 xyzw)
		{
			return new bool4(xyzw);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000343C File Offset: 0x0000163C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 bool4(bool v)
		{
			return new bool4(v);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003444 File Offset: 0x00001644
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(bool4 v)
		{
			return math.csum(math.select(math.uint4(1610574617U, 1584185147U, 3041325733U, 3150930919U), math.uint4(3309258581U, 1770373673U, 3778261171U, 3286279097U), v));
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003483 File Offset: 0x00001683
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(bool4 v)
		{
			return math.select(math.uint4(4264629071U, 1898591447U, 2641864091U, 1229113913U), math.uint4(3020867117U, 1449055807U, 2479033387U, 3702457169U), v);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000034BD File Offset: 0x000016BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool shuffle(bool4 left, bool4 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000034C7 File Offset: 0x000016C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 shuffle(bool4 left, bool4 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.bool2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000034DE File Offset: 0x000016DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 shuffle(bool4 left, bool4 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.bool3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000034FE File Offset: 0x000016FE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 shuffle(bool4 left, bool4 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.bool4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003528 File Offset: 0x00001728
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool select_shuffle_component(bool4 a, bool4 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.LeftZ:
				return a.z;
			case math.ShuffleComponent.LeftW:
				return a.w;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			case math.ShuffleComponent.RightZ:
				return b.z;
			case math.ShuffleComponent.RightW:
				return b.w;
			default:
				throw new ArgumentException("Invalid shuffle component: " + component.ToString());
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000035B1 File Offset: 0x000017B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 bool4x2(bool4 c0, bool4 c1)
		{
			return new bool4x2(c0, c1);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000035BA File Offset: 0x000017BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 bool4x2(bool m00, bool m01, bool m10, bool m11, bool m20, bool m21, bool m30, bool m31)
		{
			return new bool4x2(m00, m01, m10, m11, m20, m21, m30, m31);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000035CD File Offset: 0x000017CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 bool4x2(bool v)
		{
			return new bool4x2(v);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000035D8 File Offset: 0x000017D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 transpose(bool4x2 v)
		{
			return math.bool2x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003644 File Offset: 0x00001844
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(bool4x2 v)
		{
			return math.csum(math.select(math.uint4(3516359879U, 3050356579U, 4178586719U, 2558655391U), math.uint4(1453413133U, 2152428077U, 1938706661U, 1338588197U), v.c0) + math.select(math.uint4(3439609253U, 3535343003U, 3546061613U, 2702024231U), math.uint4(1452124841U, 1966089551U, 2668168249U, 1587512777U), v.c1));
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000036D8 File Offset: 0x000018D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(bool4x2 v)
		{
			return math.select(math.uint4(2353831999U, 3101256173U, 2891822459U, 2837054189U), math.uint4(3016004371U, 4097481403U, 2229788699U, 2382715877U), v.c0) + math.select(math.uint4(1851936439U, 1938025801U, 3712598587U, 3956330501U), math.uint4(2437373431U, 1441286183U, 2426570171U, 1561977301U), v.c1);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003764 File Offset: 0x00001964
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 bool4x3(bool4 c0, bool4 c1, bool4 c2)
		{
			return new bool4x3(c0, c1, c2);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003770 File Offset: 0x00001970
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 bool4x3(bool m00, bool m01, bool m02, bool m10, bool m11, bool m12, bool m20, bool m21, bool m22, bool m30, bool m31, bool m32)
		{
			return new bool4x3(m00, m01, m02, m10, m11, m12, m20, m21, m22, m30, m31, m32);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003796 File Offset: 0x00001996
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 bool4x3(bool v)
		{
			return new bool4x3(v);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000037A0 File Offset: 0x000019A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 transpose(bool4x3 v)
		{
			return math.bool3x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003838 File Offset: 0x00001A38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(bool4x3 v)
		{
			return math.csum(math.select(math.uint4(3940484981U, 1954192763U, 1091696537U, 3052428017U), math.uint4(4253034763U, 2338696631U, 3757372771U, 1885959949U), v.c0) + math.select(math.uint4(3508684087U, 3919501043U, 1209161033U, 4007793211U), math.uint4(3819806693U, 3458005183U, 2078515003U, 4206465343U), v.c1) + math.select(math.uint4(3025146473U, 3763046909U, 3678265601U, 2070747979U), math.uint4(1480171127U, 1588341193U, 4234155257U, 1811310911U), v.c2));
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000390C File Offset: 0x00001B0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(bool4x3 v)
		{
			return math.select(math.uint4(2635799963U, 4165137857U, 2759770933U, 2759319383U), math.uint4(3299952959U, 3121178323U, 2948522579U, 1531026433U), v.c0) + math.select(math.uint4(1365086453U, 3969870067U, 4192899797U, 3271228601U), math.uint4(1634639009U, 3318036811U, 3404170631U, 2048213449U), v.c1) + math.select(math.uint4(4164671783U, 1780759499U, 1352369353U, 2446407751U), math.uint4(1391928079U, 3475533443U, 3777095341U, 3385463369U), v.c2);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000039DA File Offset: 0x00001BDA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 bool4x4(bool4 c0, bool4 c1, bool4 c2, bool4 c3)
		{
			return new bool4x4(c0, c1, c2, c3);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000039E8 File Offset: 0x00001BE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 bool4x4(bool m00, bool m01, bool m02, bool m03, bool m10, bool m11, bool m12, bool m13, bool m20, bool m21, bool m22, bool m23, bool m30, bool m31, bool m32, bool m33)
		{
			return new bool4x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23, m30, m31, m32, m33);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003A16 File Offset: 0x00001C16
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 bool4x4(bool v)
		{
			return new bool4x4(v);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003A20 File Offset: 0x00001C20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 transpose(bool4x4 v)
		{
			return math.bool4x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w, v.c3.x, v.c3.y, v.c3.z, v.c3.w);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003AE4 File Offset: 0x00001CE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(bool4x4 v)
		{
			return math.csum(math.select(math.uint4(3516359879U, 3050356579U, 4178586719U, 2558655391U), math.uint4(1453413133U, 2152428077U, 1938706661U, 1338588197U), v.c0) + math.select(math.uint4(3439609253U, 3535343003U, 3546061613U, 2702024231U), math.uint4(1452124841U, 1966089551U, 2668168249U, 1587512777U), v.c1) + math.select(math.uint4(2353831999U, 3101256173U, 2891822459U, 2837054189U), math.uint4(3016004371U, 4097481403U, 2229788699U, 2382715877U), v.c2) + math.select(math.uint4(1851936439U, 1938025801U, 3712598587U, 3956330501U), math.uint4(2437373431U, 1441286183U, 2426570171U, 1561977301U), v.c3));
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003BFC File Offset: 0x00001DFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(bool4x4 v)
		{
			return math.select(math.uint4(4205774813U, 1650214333U, 3388112843U, 1831150513U), math.uint4(1848374953U, 3430200247U, 2209710647U, 2201894441U), v.c0) + math.select(math.uint4(2849577407U, 3287031191U, 3098675399U, 1564399943U), math.uint4(1148435377U, 3416333663U, 1750611407U, 3285396193U), v.c1) + math.select(math.uint4(3110507567U, 4271396531U, 4198118021U, 2908068253U), math.uint4(3705492289U, 2497566569U, 2716413241U, 1166264321U), v.c2) + math.select(math.uint4(2503385333U, 2944493077U, 2599999021U, 3814721321U), math.uint4(1595355149U, 1728931849U, 2062756937U, 2920485769U), v.c3);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003D0C File Offset: 0x00001F0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 double2(double x, double y)
		{
			return new double2(x, y);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003D15 File Offset: 0x00001F15
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 double2(double2 xy)
		{
			return new double2(xy);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003D1D File Offset: 0x00001F1D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 double2(double v)
		{
			return new double2(v);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003D25 File Offset: 0x00001F25
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 double2(bool v)
		{
			return new double2(v);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003D2D File Offset: 0x00001F2D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 double2(bool2 v)
		{
			return new double2(v);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003D35 File Offset: 0x00001F35
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 double2(int v)
		{
			return new double2(v);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003D3D File Offset: 0x00001F3D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 double2(int2 v)
		{
			return new double2(v);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003D45 File Offset: 0x00001F45
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 double2(uint v)
		{
			return new double2(v);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003D4D File Offset: 0x00001F4D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 double2(uint2 v)
		{
			return new double2(v);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003D55 File Offset: 0x00001F55
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 double2(half v)
		{
			return new double2(v);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003D5D File Offset: 0x00001F5D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 double2(half2 v)
		{
			return new double2(v);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003D65 File Offset: 0x00001F65
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 double2(float v)
		{
			return new double2(v);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003D6D File Offset: 0x00001F6D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 double2(float2 v)
		{
			return new double2(v);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003D75 File Offset: 0x00001F75
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(double2 v)
		{
			return math.csum(math.fold_to_uint(v) * math.uint2(2503385333U, 2944493077U)) + 2599999021U;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003D9C File Offset: 0x00001F9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(double2 v)
		{
			return math.fold_to_uint(v) * math.uint2(3814721321U, 1595355149U) + 1728931849U;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003DC2 File Offset: 0x00001FC2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double shuffle(double2 left, double2 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003DCC File Offset: 0x00001FCC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 shuffle(double2 left, double2 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.double2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003DE3 File Offset: 0x00001FE3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 shuffle(double2 left, double2 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.double3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003E03 File Offset: 0x00002003
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 shuffle(double2 left, double2 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.double4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003E2C File Offset: 0x0000202C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static double select_shuffle_component(double2 a, double2 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			}
			throw new ArgumentException("Invalid shuffle component: " + component.ToString());
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003E91 File Offset: 0x00002091
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 double2x2(double2 c0, double2 c1)
		{
			return new double2x2(c0, c1);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003E9A File Offset: 0x0000209A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 double2x2(double m00, double m01, double m10, double m11)
		{
			return new double2x2(m00, m01, m10, m11);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003EA5 File Offset: 0x000020A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 double2x2(double v)
		{
			return new double2x2(v);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003EAD File Offset: 0x000020AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 double2x2(bool v)
		{
			return new double2x2(v);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003EB5 File Offset: 0x000020B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 double2x2(bool2x2 v)
		{
			return new double2x2(v);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003EBD File Offset: 0x000020BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 double2x2(int v)
		{
			return new double2x2(v);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003EC5 File Offset: 0x000020C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 double2x2(int2x2 v)
		{
			return new double2x2(v);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003ECD File Offset: 0x000020CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 double2x2(uint v)
		{
			return new double2x2(v);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003ED5 File Offset: 0x000020D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 double2x2(uint2x2 v)
		{
			return new double2x2(v);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003EDD File Offset: 0x000020DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 double2x2(float v)
		{
			return new double2x2(v);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003EE5 File Offset: 0x000020E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 double2x2(float2x2 v)
		{
			return new double2x2(v);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003EED File Offset: 0x000020ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 transpose(double2x2 v)
		{
			return math.double2x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003F20 File Offset: 0x00002120
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 inverse(double2x2 m)
		{
			double x = m.c0.x;
			double x2 = m.c1.x;
			double y = m.c0.y;
			double y2 = m.c1.y;
			double num = x * y2 - x2 * y;
			return math.double2x2(y2, -x2, -y, x) * (1.0 / num);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003F84 File Offset: 0x00002184
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double determinant(double2x2 m)
		{
			double x = m.c0.x;
			double x2 = m.c1.x;
			double y = m.c0.y;
			double y2 = m.c1.y;
			return x * y2 - x2 * y;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003FC8 File Offset: 0x000021C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(double2x2 v)
		{
			return math.csum(math.fold_to_uint(v.c0) * math.uint2(4253034763U, 2338696631U) + math.fold_to_uint(v.c1) * math.uint2(3757372771U, 1885959949U)) + 3508684087U;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004024 File Offset: 0x00002224
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(double2x2 v)
		{
			return math.fold_to_uint(v.c0) * math.uint2(3919501043U, 1209161033U) + math.fold_to_uint(v.c1) * math.uint2(4007793211U, 3819806693U) + 3458005183U;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000407E File Offset: 0x0000227E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 double2x3(double2 c0, double2 c1, double2 c2)
		{
			return new double2x3(c0, c1, c2);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004088 File Offset: 0x00002288
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 double2x3(double m00, double m01, double m02, double m10, double m11, double m12)
		{
			return new double2x3(m00, m01, m02, m10, m11, m12);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004097 File Offset: 0x00002297
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 double2x3(double v)
		{
			return new double2x3(v);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000409F File Offset: 0x0000229F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 double2x3(bool v)
		{
			return new double2x3(v);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000040A7 File Offset: 0x000022A7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 double2x3(bool2x3 v)
		{
			return new double2x3(v);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000040AF File Offset: 0x000022AF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 double2x3(int v)
		{
			return new double2x3(v);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000040B7 File Offset: 0x000022B7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 double2x3(int2x3 v)
		{
			return new double2x3(v);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000040BF File Offset: 0x000022BF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 double2x3(uint v)
		{
			return new double2x3(v);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000040C7 File Offset: 0x000022C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 double2x3(uint2x3 v)
		{
			return new double2x3(v);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000040CF File Offset: 0x000022CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 double2x3(float v)
		{
			return new double2x3(v);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000040D7 File Offset: 0x000022D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 double2x3(float2x3 v)
		{
			return new double2x3(v);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000040E0 File Offset: 0x000022E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 transpose(double2x3 v)
		{
			return math.double3x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004134 File Offset: 0x00002334
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(double2x3 v)
		{
			return math.csum(math.fold_to_uint(v.c0) * math.uint2(4066109527U, 2613148903U) + math.fold_to_uint(v.c1) * math.uint2(3367528529U, 1678332449U) + math.fold_to_uint(v.c2) * math.uint2(2918459647U, 2744611081U)) + 1952372791U;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000041B4 File Offset: 0x000023B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(double2x3 v)
		{
			return math.fold_to_uint(v.c0) * math.uint2(2631698677U, 4200781601U) + math.fold_to_uint(v.c1) * math.uint2(2119021007U, 1760485621U) + math.fold_to_uint(v.c2) * math.uint2(3157985881U, 2171534173U) + 2723054263U;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004232 File Offset: 0x00002432
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 double2x4(double2 c0, double2 c1, double2 c2, double2 c3)
		{
			return new double2x4(c0, c1, c2, c3);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000423D File Offset: 0x0000243D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 double2x4(double m00, double m01, double m02, double m03, double m10, double m11, double m12, double m13)
		{
			return new double2x4(m00, m01, m02, m03, m10, m11, m12, m13);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004250 File Offset: 0x00002450
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 double2x4(double v)
		{
			return new double2x4(v);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004258 File Offset: 0x00002458
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 double2x4(bool v)
		{
			return new double2x4(v);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004260 File Offset: 0x00002460
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 double2x4(bool2x4 v)
		{
			return new double2x4(v);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004268 File Offset: 0x00002468
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 double2x4(int v)
		{
			return new double2x4(v);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004270 File Offset: 0x00002470
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 double2x4(int2x4 v)
		{
			return new double2x4(v);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004278 File Offset: 0x00002478
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 double2x4(uint v)
		{
			return new double2x4(v);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004280 File Offset: 0x00002480
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 double2x4(uint2x4 v)
		{
			return new double2x4(v);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004288 File Offset: 0x00002488
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 double2x4(float v)
		{
			return new double2x4(v);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004290 File Offset: 0x00002490
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 double2x4(float2x4 v)
		{
			return new double2x4(v);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004298 File Offset: 0x00002498
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 transpose(double2x4 v)
		{
			return math.double4x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y, v.c3.x, v.c3.y);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004304 File Offset: 0x00002504
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(double2x4 v)
		{
			return math.csum(math.fold_to_uint(v.c0) * math.uint2(2437373431U, 1441286183U) + math.fold_to_uint(v.c1) * math.uint2(2426570171U, 1561977301U) + math.fold_to_uint(v.c2) * math.uint2(4205774813U, 1650214333U) + math.fold_to_uint(v.c3) * math.uint2(3388112843U, 1831150513U)) + 1848374953U;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000043A8 File Offset: 0x000025A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(double2x4 v)
		{
			return math.fold_to_uint(v.c0) * math.uint2(3430200247U, 2209710647U) + math.fold_to_uint(v.c1) * math.uint2(2201894441U, 2849577407U) + math.fold_to_uint(v.c2) * math.uint2(3287031191U, 3098675399U) + math.fold_to_uint(v.c3) * math.uint2(1564399943U, 1148435377U) + 3416333663U;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000444A File Offset: 0x0000264A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(double x, double y, double z)
		{
			return new double3(x, y, z);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004454 File Offset: 0x00002654
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(double x, double2 yz)
		{
			return new double3(x, yz);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000445D File Offset: 0x0000265D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(double2 xy, double z)
		{
			return new double3(xy, z);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004466 File Offset: 0x00002666
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(double3 xyz)
		{
			return new double3(xyz);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000446E File Offset: 0x0000266E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(double v)
		{
			return new double3(v);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004476 File Offset: 0x00002676
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(bool v)
		{
			return new double3(v);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000447E File Offset: 0x0000267E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(bool3 v)
		{
			return new double3(v);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004486 File Offset: 0x00002686
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(int v)
		{
			return new double3(v);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000448E File Offset: 0x0000268E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(int3 v)
		{
			return new double3(v);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004496 File Offset: 0x00002696
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(uint v)
		{
			return new double3(v);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000449E File Offset: 0x0000269E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(uint3 v)
		{
			return new double3(v);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000044A6 File Offset: 0x000026A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(half v)
		{
			return new double3(v);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000044AE File Offset: 0x000026AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(half3 v)
		{
			return new double3(v);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000044B6 File Offset: 0x000026B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(float v)
		{
			return new double3(v);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000044BE File Offset: 0x000026BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(float3 v)
		{
			return new double3(v);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000044C6 File Offset: 0x000026C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(double3 v)
		{
			return math.csum(math.fold_to_uint(v) * math.uint3(2937008387U, 3835713223U, 2216526373U)) + 3375971453U;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000044F2 File Offset: 0x000026F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(double3 v)
		{
			return math.fold_to_uint(v) * math.uint3(3559829411U, 3652178029U, 2544260129U) + 2013864031U;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000451D File Offset: 0x0000271D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double shuffle(double3 left, double3 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004527 File Offset: 0x00002727
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 shuffle(double3 left, double3 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.double2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000453E File Offset: 0x0000273E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 shuffle(double3 left, double3 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.double3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000455E File Offset: 0x0000275E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 shuffle(double3 left, double3 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.double4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004588 File Offset: 0x00002788
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static double select_shuffle_component(double3 a, double3 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.LeftZ:
				return a.z;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			case math.ShuffleComponent.RightZ:
				return b.z;
			}
			throw new ArgumentException("Invalid shuffle component: " + component.ToString());
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000045FF File Offset: 0x000027FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 double3x2(double3 c0, double3 c1)
		{
			return new double3x2(c0, c1);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004608 File Offset: 0x00002808
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 double3x2(double m00, double m01, double m10, double m11, double m20, double m21)
		{
			return new double3x2(m00, m01, m10, m11, m20, m21);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004617 File Offset: 0x00002817
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 double3x2(double v)
		{
			return new double3x2(v);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000461F File Offset: 0x0000281F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 double3x2(bool v)
		{
			return new double3x2(v);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004627 File Offset: 0x00002827
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 double3x2(bool3x2 v)
		{
			return new double3x2(v);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000462F File Offset: 0x0000282F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 double3x2(int v)
		{
			return new double3x2(v);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004637 File Offset: 0x00002837
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 double3x2(int3x2 v)
		{
			return new double3x2(v);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000463F File Offset: 0x0000283F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 double3x2(uint v)
		{
			return new double3x2(v);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004647 File Offset: 0x00002847
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 double3x2(uint3x2 v)
		{
			return new double3x2(v);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000464F File Offset: 0x0000284F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 double3x2(float v)
		{
			return new double3x2(v);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004657 File Offset: 0x00002857
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 double3x2(float3x2 v)
		{
			return new double3x2(v);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004660 File Offset: 0x00002860
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 transpose(double3x2 v)
		{
			return math.double2x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000046B4 File Offset: 0x000028B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(double3x2 v)
		{
			return math.csum(math.fold_to_uint(v.c0) * math.uint3(3996716183U, 2626301701U, 1306289417U) + math.fold_to_uint(v.c1) * math.uint3(2096137163U, 1548578029U, 4178800919U)) + 3898072289U;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000471C File Offset: 0x0000291C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(double3x2 v)
		{
			return math.fold_to_uint(v.c0) * math.uint3(4129428421U, 2631575897U, 2854656703U) + math.fold_to_uint(v.c1) * math.uint3(3578504047U, 4245178297U, 2173281923U) + 2973357649U;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004780 File Offset: 0x00002980
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 double3x3(double3 c0, double3 c1, double3 c2)
		{
			return new double3x3(c0, c1, c2);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000478C File Offset: 0x0000298C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 double3x3(double m00, double m01, double m02, double m10, double m11, double m12, double m20, double m21, double m22)
		{
			return new double3x3(m00, m01, m02, m10, m11, m12, m20, m21, m22);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000047AC File Offset: 0x000029AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 double3x3(double v)
		{
			return new double3x3(v);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000047B4 File Offset: 0x000029B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 double3x3(bool v)
		{
			return new double3x3(v);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000047BC File Offset: 0x000029BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 double3x3(bool3x3 v)
		{
			return new double3x3(v);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000047C4 File Offset: 0x000029C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 double3x3(int v)
		{
			return new double3x3(v);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000047CC File Offset: 0x000029CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 double3x3(int3x3 v)
		{
			return new double3x3(v);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000047D4 File Offset: 0x000029D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 double3x3(uint v)
		{
			return new double3x3(v);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000047DC File Offset: 0x000029DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 double3x3(uint3x3 v)
		{
			return new double3x3(v);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000047E4 File Offset: 0x000029E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 double3x3(float v)
		{
			return new double3x3(v);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000047EC File Offset: 0x000029EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 double3x3(float3x3 v)
		{
			return new double3x3(v);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000047F4 File Offset: 0x000029F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 transpose(double3x3 v)
		{
			return math.double3x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000486C File Offset: 0x00002A6C
		public static double3x3 inverse(double3x3 m)
		{
			double3 c = m.c0;
			double3 c2 = m.c1;
			double3 c3 = m.c2;
			double3 lhs = math.double3(c2.x, c3.x, c.x);
			double3 @double = math.double3(c2.y, c3.y, c.y);
			double3 rhs = math.double3(c2.z, c3.z, c.z);
			double3 double2 = @double * rhs.yzx - @double.yzx * rhs;
			double3 c4 = lhs.yzx * rhs - lhs * rhs.yzx;
			double3 c5 = lhs * @double.yzx - lhs.yzx * @double;
			double rhs2 = 1.0 / math.csum(lhs.zxy * double2);
			return math.double3x3(double2, c4, c5) * rhs2;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000496C File Offset: 0x00002B6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double determinant(double3x3 m)
		{
			double3 c = m.c0;
			double3 c2 = m.c1;
			double3 c3 = m.c2;
			double num = c2.y * c3.z - c2.z * c3.y;
			double num2 = c.y * c3.z - c.z * c3.y;
			double num3 = c.y * c2.z - c.z * c2.y;
			return c.x * num - c2.x * num2 + c3.x * num3;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004A00 File Offset: 0x00002C00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(double3x3 v)
		{
			return math.csum(math.fold_to_uint(v.c0) * math.uint3(2891822459U, 2837054189U, 3016004371U) + math.fold_to_uint(v.c1) * math.uint3(4097481403U, 2229788699U, 2382715877U) + math.fold_to_uint(v.c2) * math.uint3(1851936439U, 1938025801U, 3712598587U)) + 3956330501U;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004A90 File Offset: 0x00002C90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(double3x3 v)
		{
			return math.fold_to_uint(v.c0) * math.uint3(2437373431U, 1441286183U, 2426570171U) + math.fold_to_uint(v.c1) * math.uint3(1561977301U, 4205774813U, 1650214333U) + math.fold_to_uint(v.c2) * math.uint3(3388112843U, 1831150513U, 1848374953U) + 3430200247U;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004B1D File Offset: 0x00002D1D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 double3x4(double3 c0, double3 c1, double3 c2, double3 c3)
		{
			return new double3x4(c0, c1, c2, c3);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004B28 File Offset: 0x00002D28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 double3x4(double m00, double m01, double m02, double m03, double m10, double m11, double m12, double m13, double m20, double m21, double m22, double m23)
		{
			return new double3x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004B4E File Offset: 0x00002D4E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 double3x4(double v)
		{
			return new double3x4(v);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004B56 File Offset: 0x00002D56
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 double3x4(bool v)
		{
			return new double3x4(v);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004B5E File Offset: 0x00002D5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 double3x4(bool3x4 v)
		{
			return new double3x4(v);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004B66 File Offset: 0x00002D66
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 double3x4(int v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004B6E File Offset: 0x00002D6E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 double3x4(int3x4 v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004B76 File Offset: 0x00002D76
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 double3x4(uint v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004B7E File Offset: 0x00002D7E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 double3x4(uint3x4 v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004B86 File Offset: 0x00002D86
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 double3x4(float v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004B8E File Offset: 0x00002D8E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 double3x4(float3x4 v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004B98 File Offset: 0x00002D98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 transpose(double3x4 v)
		{
			return math.double4x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z, v.c3.x, v.c3.y, v.c3.z);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004C30 File Offset: 0x00002E30
		public static double3x4 fastinverse(double3x4 m)
		{
			double3 c = m.c0;
			double3 c2 = m.c1;
			double3 c3 = m.c2;
			double3 @double = m.c3;
			double3 double2 = math.double3(c.x, c2.x, c3.x);
			double3 double3 = math.double3(c.y, c2.y, c3.y);
			double3 double4 = math.double3(c.z, c2.z, c3.z);
			@double = -(double2 * @double.x + double3 * @double.y + double4 * @double.z);
			return math.double3x4(double2, double3, double4, @double);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004CE4 File Offset: 0x00002EE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(double3x4 v)
		{
			return math.csum(math.fold_to_uint(v.c0) * math.uint3(3996716183U, 2626301701U, 1306289417U) + math.fold_to_uint(v.c1) * math.uint3(2096137163U, 1548578029U, 4178800919U) + math.fold_to_uint(v.c2) * math.uint3(3898072289U, 4129428421U, 2631575897U) + math.fold_to_uint(v.c3) * math.uint3(2854656703U, 3578504047U, 4245178297U)) + 2173281923U;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004D9C File Offset: 0x00002F9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(double3x4 v)
		{
			return math.fold_to_uint(v.c0) * math.uint3(2973357649U, 3881277847U, 4017968839U) + math.fold_to_uint(v.c1) * math.uint3(1727237899U, 1648514723U, 1385344481U) + math.fold_to_uint(v.c2) * math.uint3(3538260197U, 4066109527U, 2613148903U) + math.fold_to_uint(v.c3) * math.uint3(3367528529U, 1678332449U, 2918459647U) + 2744611081U;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004E52 File Offset: 0x00003052
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(double x, double y, double z, double w)
		{
			return new double4(x, y, z, w);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004E5D File Offset: 0x0000305D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(double x, double y, double2 zw)
		{
			return new double4(x, y, zw);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004E67 File Offset: 0x00003067
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(double x, double2 yz, double w)
		{
			return new double4(x, yz, w);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004E71 File Offset: 0x00003071
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(double x, double3 yzw)
		{
			return new double4(x, yzw);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004E7A File Offset: 0x0000307A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(double2 xy, double z, double w)
		{
			return new double4(xy, z, w);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004E84 File Offset: 0x00003084
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(double2 xy, double2 zw)
		{
			return new double4(xy, zw);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004E8D File Offset: 0x0000308D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(double3 xyz, double w)
		{
			return new double4(xyz, w);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004E96 File Offset: 0x00003096
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(double4 xyzw)
		{
			return new double4(xyzw);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004E9E File Offset: 0x0000309E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(double v)
		{
			return new double4(v);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004EA6 File Offset: 0x000030A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(bool v)
		{
			return new double4(v);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004EAE File Offset: 0x000030AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(bool4 v)
		{
			return new double4(v);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004EB6 File Offset: 0x000030B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(int v)
		{
			return new double4(v);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004EBE File Offset: 0x000030BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(int4 v)
		{
			return new double4(v);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004EC6 File Offset: 0x000030C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(uint v)
		{
			return new double4(v);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004ECE File Offset: 0x000030CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(uint4 v)
		{
			return new double4(v);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004ED6 File Offset: 0x000030D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(half v)
		{
			return new double4(v);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00004EDE File Offset: 0x000030DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(half4 v)
		{
			return new double4(v);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004EE6 File Offset: 0x000030E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(float v)
		{
			return new double4(v);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004EEE File Offset: 0x000030EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(float4 v)
		{
			return new double4(v);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004EF6 File Offset: 0x000030F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(double4 v)
		{
			return math.csum(math.fold_to_uint(v) * math.uint4(2669441947U, 1260114311U, 2650080659U, 4052675461U)) + 2652487619U;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004F27 File Offset: 0x00003127
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(double4 v)
		{
			return math.fold_to_uint(v) * math.uint4(2174136431U, 3528391193U, 2105559227U, 1899745391U) + 1966790317U;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004F57 File Offset: 0x00003157
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double shuffle(double4 left, double4 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004F61 File Offset: 0x00003161
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 shuffle(double4 left, double4 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.double2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004F78 File Offset: 0x00003178
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 shuffle(double4 left, double4 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.double3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004F98 File Offset: 0x00003198
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 shuffle(double4 left, double4 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.double4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004FC4 File Offset: 0x000031C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static double select_shuffle_component(double4 a, double4 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.LeftZ:
				return a.z;
			case math.ShuffleComponent.LeftW:
				return a.w;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			case math.ShuffleComponent.RightZ:
				return b.z;
			case math.ShuffleComponent.RightW:
				return b.w;
			default:
				throw new ArgumentException("Invalid shuffle component: " + component.ToString());
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000504D File Offset: 0x0000324D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 double4x2(double4 c0, double4 c1)
		{
			return new double4x2(c0, c1);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00005056 File Offset: 0x00003256
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 double4x2(double m00, double m01, double m10, double m11, double m20, double m21, double m30, double m31)
		{
			return new double4x2(m00, m01, m10, m11, m20, m21, m30, m31);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005069 File Offset: 0x00003269
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 double4x2(double v)
		{
			return new double4x2(v);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005071 File Offset: 0x00003271
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 double4x2(bool v)
		{
			return new double4x2(v);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005079 File Offset: 0x00003279
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 double4x2(bool4x2 v)
		{
			return new double4x2(v);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005081 File Offset: 0x00003281
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 double4x2(int v)
		{
			return new double4x2(v);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005089 File Offset: 0x00003289
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 double4x2(int4x2 v)
		{
			return new double4x2(v);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005091 File Offset: 0x00003291
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 double4x2(uint v)
		{
			return new double4x2(v);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00005099 File Offset: 0x00003299
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 double4x2(uint4x2 v)
		{
			return new double4x2(v);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000050A1 File Offset: 0x000032A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 double4x2(float v)
		{
			return new double4x2(v);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000050A9 File Offset: 0x000032A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 double4x2(float4x2 v)
		{
			return new double4x2(v);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000050B4 File Offset: 0x000032B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 transpose(double4x2 v)
		{
			return math.double2x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005120 File Offset: 0x00003320
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(double4x2 v)
		{
			return math.csum(math.fold_to_uint(v.c0) * math.uint4(1521739981U, 1735296007U, 3010324327U, 1875523709U) + math.fold_to_uint(v.c1) * math.uint4(2937008387U, 3835713223U, 2216526373U, 3375971453U)) + 3559829411U;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005190 File Offset: 0x00003390
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(double4x2 v)
		{
			return math.fold_to_uint(v.c0) * math.uint4(3652178029U, 2544260129U, 2013864031U, 2627668003U) + math.fold_to_uint(v.c1) * math.uint4(1520214331U, 2949502447U, 2827819133U, 3480140317U) + 2642994593U;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000051FE File Offset: 0x000033FE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 double4x3(double4 c0, double4 c1, double4 c2)
		{
			return new double4x3(c0, c1, c2);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005208 File Offset: 0x00003408
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 double4x3(double m00, double m01, double m02, double m10, double m11, double m12, double m20, double m21, double m22, double m30, double m31, double m32)
		{
			return new double4x3(m00, m01, m02, m10, m11, m12, m20, m21, m22, m30, m31, m32);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000522E File Offset: 0x0000342E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 double4x3(double v)
		{
			return new double4x3(v);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00005236 File Offset: 0x00003436
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 double4x3(bool v)
		{
			return new double4x3(v);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000523E File Offset: 0x0000343E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 double4x3(bool4x3 v)
		{
			return new double4x3(v);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00005246 File Offset: 0x00003446
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 double4x3(int v)
		{
			return new double4x3(v);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000524E File Offset: 0x0000344E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 double4x3(int4x3 v)
		{
			return new double4x3(v);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00005256 File Offset: 0x00003456
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 double4x3(uint v)
		{
			return new double4x3(v);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000525E File Offset: 0x0000345E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 double4x3(uint4x3 v)
		{
			return new double4x3(v);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005266 File Offset: 0x00003466
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 double4x3(float v)
		{
			return new double4x3(v);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000526E File Offset: 0x0000346E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 double4x3(float4x3 v)
		{
			return new double4x3(v);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005278 File Offset: 0x00003478
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 transpose(double4x3 v)
		{
			return math.double3x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005310 File Offset: 0x00003510
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(double4x3 v)
		{
			return math.csum(math.fold_to_uint(v.c0) * math.uint4(2057338067U, 2942577577U, 2834440507U, 2671762487U) + math.fold_to_uint(v.c1) * math.uint4(2892026051U, 2455987759U, 3868600063U, 3170963179U) + math.fold_to_uint(v.c2) * math.uint4(2632835537U, 1136528209U, 2944626401U, 2972762423U)) + 1417889653U;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000053B0 File Offset: 0x000035B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(double4x3 v)
		{
			return math.fold_to_uint(v.c0) * math.uint4(2080514593U, 2731544287U, 2828498809U, 2669441947U) + math.fold_to_uint(v.c1) * math.uint4(1260114311U, 2650080659U, 4052675461U, 2652487619U) + math.fold_to_uint(v.c2) * math.uint4(2174136431U, 3528391193U, 2105559227U, 1899745391U) + 1966790317U;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000544C File Offset: 0x0000364C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 double4x4(double4 c0, double4 c1, double4 c2, double4 c3)
		{
			return new double4x4(c0, c1, c2, c3);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005458 File Offset: 0x00003658
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 double4x4(double m00, double m01, double m02, double m03, double m10, double m11, double m12, double m13, double m20, double m21, double m22, double m23, double m30, double m31, double m32, double m33)
		{
			return new double4x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23, m30, m31, m32, m33);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005486 File Offset: 0x00003686
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 double4x4(double v)
		{
			return new double4x4(v);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000548E File Offset: 0x0000368E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 double4x4(bool v)
		{
			return new double4x4(v);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005496 File Offset: 0x00003696
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 double4x4(bool4x4 v)
		{
			return new double4x4(v);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000549E File Offset: 0x0000369E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 double4x4(int v)
		{
			return new double4x4(v);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000054A6 File Offset: 0x000036A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 double4x4(int4x4 v)
		{
			return new double4x4(v);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000054AE File Offset: 0x000036AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 double4x4(uint v)
		{
			return new double4x4(v);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000054B6 File Offset: 0x000036B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 double4x4(uint4x4 v)
		{
			return new double4x4(v);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000054BE File Offset: 0x000036BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 double4x4(float v)
		{
			return new double4x4(v);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000054C6 File Offset: 0x000036C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 double4x4(float4x4 v)
		{
			return new double4x4(v);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000054D0 File Offset: 0x000036D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 rotate(double4x4 a, double3 b)
		{
			return (a.c0 * b.x + a.c1 * b.y + a.c2 * b.z).xyz;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005524 File Offset: 0x00003724
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 transform(double4x4 a, double3 b)
		{
			return (a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3).xyz;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005584 File Offset: 0x00003784
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 transpose(double4x4 v)
		{
			return math.double4x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w, v.c3.x, v.c3.y, v.c3.z, v.c3.w);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005648 File Offset: 0x00003848
		public static double4x4 inverse(double4x4 m)
		{
			double4 c = m.c0;
			double4 c2 = m.c1;
			double4 c3 = m.c2;
			double4 c4 = m.c3;
			double4 @double = math.movelh(c2, c);
			double4 double2 = math.movelh(c3, c4);
			double4 double3 = math.movehl(c, c2);
			double4 double4 = math.movehl(c4, c3);
			double4 lhs = math.shuffle(c2, c, math.ShuffleComponent.LeftY, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightY, math.ShuffleComponent.RightZ);
			double4 lhs2 = math.shuffle(c3, c4, math.ShuffleComponent.LeftY, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightY, math.ShuffleComponent.RightZ);
			double4 lhs3 = math.shuffle(c2, c, math.ShuffleComponent.LeftW, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightW, math.ShuffleComponent.RightX);
			double4 lhs4 = math.shuffle(c3, c4, math.ShuffleComponent.LeftW, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightW, math.ShuffleComponent.RightX);
			double4 lhs5 = math.shuffle(double2, @double, math.ShuffleComponent.LeftZ, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightX, math.ShuffleComponent.RightZ);
			double4 lhs6 = math.shuffle(double2, @double, math.ShuffleComponent.LeftW, math.ShuffleComponent.LeftY, math.ShuffleComponent.RightY, math.ShuffleComponent.RightW);
			double4 lhs7 = math.shuffle(double4, double3, math.ShuffleComponent.LeftZ, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightX, math.ShuffleComponent.RightZ);
			double4 lhs8 = math.shuffle(double4, double3, math.ShuffleComponent.LeftW, math.ShuffleComponent.LeftY, math.ShuffleComponent.RightY, math.ShuffleComponent.RightW);
			double4 lhs9 = math.shuffle(@double, double2, math.ShuffleComponent.LeftZ, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightX, math.ShuffleComponent.RightZ);
			double4 double5 = lhs * double4 - lhs2 * double3;
			double4 double6 = @double * double4 - double2 * double3;
			double4 double7 = lhs4 * @double - lhs3 * double2;
			double4 rhs = math.shuffle(double5, double5, math.ShuffleComponent.LeftX, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightZ, math.ShuffleComponent.RightX);
			double4 rhs2 = math.shuffle(double5, double5, math.ShuffleComponent.LeftY, math.ShuffleComponent.LeftW, math.ShuffleComponent.RightW, math.ShuffleComponent.RightY);
			double4 rhs3 = math.shuffle(double6, double6, math.ShuffleComponent.LeftX, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightZ, math.ShuffleComponent.RightX);
			double4 rhs4 = math.shuffle(double6, double6, math.ShuffleComponent.LeftY, math.ShuffleComponent.LeftW, math.ShuffleComponent.RightW, math.ShuffleComponent.RightY);
			double4 double8 = lhs8 * rhs - lhs7 * rhs4 + lhs6 * rhs2;
			double4 double9 = lhs9 * double8;
			double9 += math.shuffle(double9, double9, math.ShuffleComponent.LeftY, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightW, math.ShuffleComponent.RightZ);
			double9 -= math.shuffle(double9, double9, math.ShuffleComponent.LeftZ, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightX, math.ShuffleComponent.RightX);
			double4 rhs5 = math.double4(1.0) / double9;
			double4x4 result;
			result.c0 = double8 * rhs5;
			double4 rhs6 = math.shuffle(double7, double7, math.ShuffleComponent.LeftX, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightZ, math.ShuffleComponent.RightX);
			double4 rhs7 = math.shuffle(double7, double7, math.ShuffleComponent.LeftY, math.ShuffleComponent.LeftW, math.ShuffleComponent.RightW, math.ShuffleComponent.RightY);
			double4 lhs10 = lhs7 * rhs6 - lhs5 * rhs2 - lhs8 * rhs3;
			result.c1 = lhs10 * rhs5;
			double4 lhs11 = lhs5 * rhs4 - lhs6 * rhs6 - lhs8 * rhs7;
			result.c2 = lhs11 * rhs5;
			double4 lhs12 = lhs6 * rhs3 - lhs5 * rhs + lhs7 * rhs7;
			result.c3 = lhs12 * rhs5;
			return result;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000058DC File Offset: 0x00003ADC
		public static double4x4 fastinverse(double4x4 m)
		{
			double4 c = m.c0;
			double4 c2 = m.c1;
			double4 c3 = m.c2;
			double4 @double = m.c3;
			double4 b = math.double4(0);
			double4 a = math.unpacklo(c, c3);
			double4 b2 = math.unpacklo(c2, b);
			double4 a2 = math.unpackhi(c, c3);
			double4 b3 = math.unpackhi(c2, b);
			double4 double2 = math.unpacklo(a, b2);
			double4 double3 = math.unpackhi(a, b2);
			double4 double4 = math.unpacklo(a2, b3);
			@double = -(double2 * @double.x + double3 * @double.y + double4 * @double.z);
			@double.w = 1.0;
			return math.double4x4(double2, double3, double4, @double);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000059A0 File Offset: 0x00003BA0
		public static double determinant(double4x4 m)
		{
			double4 c = m.c0;
			double4 c2 = m.c1;
			double4 c3 = m.c2;
			double4 c4 = m.c3;
			double num = c2.y * (c3.z * c4.w - c3.w * c4.z) - c3.y * (c2.z * c4.w - c2.w * c4.z) + c4.y * (c2.z * c3.w - c2.w * c3.z);
			double num2 = c.y * (c3.z * c4.w - c3.w * c4.z) - c3.y * (c.z * c4.w - c.w * c4.z) + c4.y * (c.z * c3.w - c.w * c3.z);
			double num3 = c.y * (c2.z * c4.w - c2.w * c4.z) - c2.y * (c.z * c4.w - c.w * c4.z) + c4.y * (c.z * c2.w - c.w * c2.z);
			double num4 = c.y * (c2.z * c3.w - c2.w * c3.z) - c2.y * (c.z * c3.w - c.w * c3.z) + c3.y * (c.z * c2.w - c.w * c2.z);
			return c.x * num - c2.x * num2 + c3.x * num3 - c4.x * num4;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00005B98 File Offset: 0x00003D98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(double4x4 v)
		{
			return math.csum(math.fold_to_uint(v.c0) * math.uint4(1306289417U, 2096137163U, 1548578029U, 4178800919U) + math.fold_to_uint(v.c1) * math.uint4(3898072289U, 4129428421U, 2631575897U, 2854656703U) + math.fold_to_uint(v.c2) * math.uint4(3578504047U, 4245178297U, 2173281923U, 2973357649U) + math.fold_to_uint(v.c3) * math.uint4(3881277847U, 4017968839U, 1727237899U, 1648514723U)) + 1385344481U;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00005C64 File Offset: 0x00003E64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(double4x4 v)
		{
			return math.fold_to_uint(v.c0) * math.uint4(3538260197U, 4066109527U, 2613148903U, 3367528529U) + math.fold_to_uint(v.c1) * math.uint4(1678332449U, 2918459647U, 2744611081U, 1952372791U) + math.fold_to_uint(v.c2) * math.uint4(2631698677U, 4200781601U, 2119021007U, 1760485621U) + math.fold_to_uint(v.c3) * math.uint4(3157985881U, 2171534173U, 2723054263U, 1168253063U) + 4228926523U;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00005D2E File Offset: 0x00003F2E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 float2(float x, float y)
		{
			return new float2(x, y);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00005D37 File Offset: 0x00003F37
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 float2(float2 xy)
		{
			return new float2(xy);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005D3F File Offset: 0x00003F3F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 float2(float v)
		{
			return new float2(v);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00005D47 File Offset: 0x00003F47
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 float2(bool v)
		{
			return new float2(v);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00005D4F File Offset: 0x00003F4F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 float2(bool2 v)
		{
			return new float2(v);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00005D57 File Offset: 0x00003F57
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 float2(int v)
		{
			return new float2(v);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005D5F File Offset: 0x00003F5F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 float2(int2 v)
		{
			return new float2(v);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005D67 File Offset: 0x00003F67
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 float2(uint v)
		{
			return new float2(v);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00005D6F File Offset: 0x00003F6F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 float2(uint2 v)
		{
			return new float2(v);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005D77 File Offset: 0x00003F77
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 float2(half v)
		{
			return new float2(v);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005D7F File Offset: 0x00003F7F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 float2(half2 v)
		{
			return new float2(v);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005D87 File Offset: 0x00003F87
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 float2(double v)
		{
			return new float2(v);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005D8F File Offset: 0x00003F8F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 float2(double2 v)
		{
			return new float2(v);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005D97 File Offset: 0x00003F97
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(float2 v)
		{
			return math.csum(math.asuint(v) * math.uint2(4198118021U, 2908068253U)) + 3705492289U;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005DBE File Offset: 0x00003FBE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(float2 v)
		{
			return math.asuint(v) * math.uint2(2497566569U, 2716413241U) + 1166264321U;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005DE4 File Offset: 0x00003FE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float shuffle(float2 left, float2 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005DEE File Offset: 0x00003FEE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 shuffle(float2 left, float2 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.float2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005E05 File Offset: 0x00004005
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 shuffle(float2 left, float2 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.float3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00005E25 File Offset: 0x00004025
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 shuffle(float2 left, float2 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.float4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005E50 File Offset: 0x00004050
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static float select_shuffle_component(float2 a, float2 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			}
			throw new ArgumentException("Invalid shuffle component: " + component.ToString());
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005EB5 File Offset: 0x000040B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 float2x2(float2 c0, float2 c1)
		{
			return new float2x2(c0, c1);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005EBE File Offset: 0x000040BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 float2x2(float m00, float m01, float m10, float m11)
		{
			return new float2x2(m00, m01, m10, m11);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005EC9 File Offset: 0x000040C9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 float2x2(float v)
		{
			return new float2x2(v);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00005ED1 File Offset: 0x000040D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 float2x2(bool v)
		{
			return new float2x2(v);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005ED9 File Offset: 0x000040D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 float2x2(bool2x2 v)
		{
			return new float2x2(v);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00005EE1 File Offset: 0x000040E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 float2x2(int v)
		{
			return new float2x2(v);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005EE9 File Offset: 0x000040E9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 float2x2(int2x2 v)
		{
			return new float2x2(v);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005EF1 File Offset: 0x000040F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 float2x2(uint v)
		{
			return new float2x2(v);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005EF9 File Offset: 0x000040F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 float2x2(uint2x2 v)
		{
			return new float2x2(v);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00005F01 File Offset: 0x00004101
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 float2x2(double v)
		{
			return new float2x2(v);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00005F09 File Offset: 0x00004109
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 float2x2(double2x2 v)
		{
			return new float2x2(v);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005F11 File Offset: 0x00004111
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 transpose(float2x2 v)
		{
			return math.float2x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005F44 File Offset: 0x00004144
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 inverse(float2x2 m)
		{
			float x = m.c0.x;
			float x2 = m.c1.x;
			float y = m.c0.y;
			float y2 = m.c1.y;
			float num = x * y2 - x2 * y;
			return math.float2x2(y2, -x2, -y, x) * (1f / num);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005FA4 File Offset: 0x000041A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float determinant(float2x2 m)
		{
			float x = m.c0.x;
			float x2 = m.c1.x;
			float y = m.c0.y;
			float y2 = m.c1.y;
			return x * y2 - x2 * y;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00005FE8 File Offset: 0x000041E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(float2x2 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint2(2627668003U, 1520214331U) + math.asuint(v.c1) * math.uint2(2949502447U, 2827819133U)) + 3480140317U;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00006044 File Offset: 0x00004244
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(float2x2 v)
		{
			return math.asuint(v.c0) * math.uint2(2642994593U, 3940484981U) + math.asuint(v.c1) * math.uint2(1954192763U, 1091696537U) + 3052428017U;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000609E File Offset: 0x0000429E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 float2x3(float2 c0, float2 c1, float2 c2)
		{
			return new float2x3(c0, c1, c2);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000060A8 File Offset: 0x000042A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 float2x3(float m00, float m01, float m02, float m10, float m11, float m12)
		{
			return new float2x3(m00, m01, m02, m10, m11, m12);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000060B7 File Offset: 0x000042B7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 float2x3(float v)
		{
			return new float2x3(v);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000060BF File Offset: 0x000042BF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 float2x3(bool v)
		{
			return new float2x3(v);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000060C7 File Offset: 0x000042C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 float2x3(bool2x3 v)
		{
			return new float2x3(v);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000060CF File Offset: 0x000042CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 float2x3(int v)
		{
			return new float2x3(v);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000060D7 File Offset: 0x000042D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 float2x3(int2x3 v)
		{
			return new float2x3(v);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000060DF File Offset: 0x000042DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 float2x3(uint v)
		{
			return new float2x3(v);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000060E7 File Offset: 0x000042E7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 float2x3(uint2x3 v)
		{
			return new float2x3(v);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000060EF File Offset: 0x000042EF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 float2x3(double v)
		{
			return new float2x3(v);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000060F7 File Offset: 0x000042F7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 float2x3(double2x3 v)
		{
			return new float2x3(v);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00006100 File Offset: 0x00004300
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 transpose(float2x3 v)
		{
			return math.float3x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00006154 File Offset: 0x00004354
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(float2x3 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint2(3898072289U, 4129428421U) + math.asuint(v.c1) * math.uint2(2631575897U, 2854656703U) + math.asuint(v.c2) * math.uint2(3578504047U, 4245178297U)) + 2173281923U;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000061D4 File Offset: 0x000043D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(float2x3 v)
		{
			return math.asuint(v.c0) * math.uint2(2973357649U, 3881277847U) + math.asuint(v.c1) * math.uint2(4017968839U, 1727237899U) + math.asuint(v.c2) * math.uint2(1648514723U, 1385344481U) + 3538260197U;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006252 File Offset: 0x00004452
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 float2x4(float2 c0, float2 c1, float2 c2, float2 c3)
		{
			return new float2x4(c0, c1, c2, c3);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000625D File Offset: 0x0000445D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 float2x4(float m00, float m01, float m02, float m03, float m10, float m11, float m12, float m13)
		{
			return new float2x4(m00, m01, m02, m03, m10, m11, m12, m13);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00006270 File Offset: 0x00004470
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 float2x4(float v)
		{
			return new float2x4(v);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00006278 File Offset: 0x00004478
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 float2x4(bool v)
		{
			return new float2x4(v);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006280 File Offset: 0x00004480
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 float2x4(bool2x4 v)
		{
			return new float2x4(v);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00006288 File Offset: 0x00004488
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 float2x4(int v)
		{
			return new float2x4(v);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00006290 File Offset: 0x00004490
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 float2x4(int2x4 v)
		{
			return new float2x4(v);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00006298 File Offset: 0x00004498
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 float2x4(uint v)
		{
			return new float2x4(v);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000062A0 File Offset: 0x000044A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 float2x4(uint2x4 v)
		{
			return new float2x4(v);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000062A8 File Offset: 0x000044A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 float2x4(double v)
		{
			return new float2x4(v);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000062B0 File Offset: 0x000044B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 float2x4(double2x4 v)
		{
			return new float2x4(v);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000062B8 File Offset: 0x000044B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 transpose(float2x4 v)
		{
			return math.float4x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y, v.c3.x, v.c3.y);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00006324 File Offset: 0x00004524
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(float2x4 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint2(3546061613U, 2702024231U) + math.asuint(v.c1) * math.uint2(1452124841U, 1966089551U) + math.asuint(v.c2) * math.uint2(2668168249U, 1587512777U) + math.asuint(v.c3) * math.uint2(2353831999U, 3101256173U)) + 2891822459U;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000063C8 File Offset: 0x000045C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(float2x4 v)
		{
			return math.asuint(v.c0) * math.uint2(2837054189U, 3016004371U) + math.asuint(v.c1) * math.uint2(4097481403U, 2229788699U) + math.asuint(v.c2) * math.uint2(2382715877U, 1851936439U) + math.asuint(v.c3) * math.uint2(1938025801U, 3712598587U) + 3956330501U;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000646A File Offset: 0x0000466A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(float x, float y, float z)
		{
			return new float3(x, y, z);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00006474 File Offset: 0x00004674
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(float x, float2 yz)
		{
			return new float3(x, yz);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000647D File Offset: 0x0000467D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(float2 xy, float z)
		{
			return new float3(xy, z);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00006486 File Offset: 0x00004686
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(float3 xyz)
		{
			return new float3(xyz);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000648E File Offset: 0x0000468E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(float v)
		{
			return new float3(v);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006496 File Offset: 0x00004696
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(bool v)
		{
			return new float3(v);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000649E File Offset: 0x0000469E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(bool3 v)
		{
			return new float3(v);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000064A6 File Offset: 0x000046A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(int v)
		{
			return new float3(v);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000064AE File Offset: 0x000046AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(int3 v)
		{
			return new float3(v);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000064B6 File Offset: 0x000046B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(uint v)
		{
			return new float3(v);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000064BE File Offset: 0x000046BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(uint3 v)
		{
			return new float3(v);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000064C6 File Offset: 0x000046C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(half v)
		{
			return new float3(v);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000064CE File Offset: 0x000046CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(half3 v)
		{
			return new float3(v);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000064D6 File Offset: 0x000046D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(double v)
		{
			return new float3(v);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000064DE File Offset: 0x000046DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(double3 v)
		{
			return new float3(v);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000064E6 File Offset: 0x000046E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(float3 v)
		{
			return math.csum(math.asuint(v) * math.uint3(2601761069U, 1254033427U, 2248573027U)) + 3612677113U;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00006512 File Offset: 0x00004712
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(float3 v)
		{
			return math.asuint(v) * math.uint3(1521739981U, 1735296007U, 3010324327U) + 1875523709U;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000653D File Offset: 0x0000473D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float shuffle(float3 left, float3 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00006547 File Offset: 0x00004747
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 shuffle(float3 left, float3 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.float2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000655E File Offset: 0x0000475E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 shuffle(float3 left, float3 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.float3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000657E File Offset: 0x0000477E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 shuffle(float3 left, float3 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.float4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000065A8 File Offset: 0x000047A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static float select_shuffle_component(float3 a, float3 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.LeftZ:
				return a.z;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			case math.ShuffleComponent.RightZ:
				return b.z;
			}
			throw new ArgumentException("Invalid shuffle component: " + component.ToString());
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000661F File Offset: 0x0000481F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 float3x2(float3 c0, float3 c1)
		{
			return new float3x2(c0, c1);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00006628 File Offset: 0x00004828
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 float3x2(float m00, float m01, float m10, float m11, float m20, float m21)
		{
			return new float3x2(m00, m01, m10, m11, m20, m21);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00006637 File Offset: 0x00004837
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 float3x2(float v)
		{
			return new float3x2(v);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000663F File Offset: 0x0000483F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 float3x2(bool v)
		{
			return new float3x2(v);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00006647 File Offset: 0x00004847
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 float3x2(bool3x2 v)
		{
			return new float3x2(v);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000664F File Offset: 0x0000484F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 float3x2(int v)
		{
			return new float3x2(v);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00006657 File Offset: 0x00004857
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 float3x2(int3x2 v)
		{
			return new float3x2(v);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000665F File Offset: 0x0000485F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 float3x2(uint v)
		{
			return new float3x2(v);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00006667 File Offset: 0x00004867
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 float3x2(uint3x2 v)
		{
			return new float3x2(v);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000666F File Offset: 0x0000486F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 float3x2(double v)
		{
			return new float3x2(v);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00006677 File Offset: 0x00004877
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 float3x2(double3x2 v)
		{
			return new float3x2(v);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00006680 File Offset: 0x00004880
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 transpose(float3x2 v)
		{
			return math.float2x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000066D4 File Offset: 0x000048D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(float3x2 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint3(3777095341U, 3385463369U, 1773538433U) + math.asuint(v.c1) * math.uint3(3773525029U, 4131962539U, 1809525511U)) + 4016293529U;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000673C File Offset: 0x0000493C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(float3x2 v)
		{
			return math.asuint(v.c0) * math.uint3(2416021567U, 2828384717U, 2636362241U) + math.asuint(v.c1) * math.uint3(1258410977U, 1952565773U, 2037535609U) + 3592785499U;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000067A0 File Offset: 0x000049A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 float3x3(float3 c0, float3 c1, float3 c2)
		{
			return new float3x3(c0, c1, c2);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000067AC File Offset: 0x000049AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 float3x3(float m00, float m01, float m02, float m10, float m11, float m12, float m20, float m21, float m22)
		{
			return new float3x3(m00, m01, m02, m10, m11, m12, m20, m21, m22);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000067CC File Offset: 0x000049CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 float3x3(float v)
		{
			return new float3x3(v);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000067D4 File Offset: 0x000049D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 float3x3(bool v)
		{
			return new float3x3(v);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000067DC File Offset: 0x000049DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 float3x3(bool3x3 v)
		{
			return new float3x3(v);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000067E4 File Offset: 0x000049E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 float3x3(int v)
		{
			return new float3x3(v);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000067EC File Offset: 0x000049EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 float3x3(int3x3 v)
		{
			return new float3x3(v);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000067F4 File Offset: 0x000049F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 float3x3(uint v)
		{
			return new float3x3(v);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000067FC File Offset: 0x000049FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 float3x3(uint3x3 v)
		{
			return new float3x3(v);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00006804 File Offset: 0x00004A04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 float3x3(double v)
		{
			return new float3x3(v);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000680C File Offset: 0x00004A0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 float3x3(double3x3 v)
		{
			return new float3x3(v);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00006814 File Offset: 0x00004A14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 transpose(float3x3 v)
		{
			return math.float3x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000688C File Offset: 0x00004A8C
		public static float3x3 inverse(float3x3 m)
		{
			float3 c = m.c0;
			float3 c2 = m.c1;
			float3 c3 = m.c2;
			float3 lhs = math.float3(c2.x, c3.x, c.x);
			float3 @float = math.float3(c2.y, c3.y, c.y);
			float3 rhs = math.float3(c2.z, c3.z, c.z);
			float3 float2 = @float * rhs.yzx - @float.yzx * rhs;
			float3 c4 = lhs.yzx * rhs - lhs * rhs.yzx;
			float3 c5 = lhs * @float.yzx - lhs.yzx * @float;
			float rhs2 = 1f / math.csum(lhs.zxy * float2);
			return math.float3x3(float2, c4, c5) * rhs2;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00006988 File Offset: 0x00004B88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float determinant(float3x3 m)
		{
			float3 c = m.c0;
			float3 c2 = m.c1;
			float3 c3 = m.c2;
			float num = c2.y * c3.z - c2.z * c3.y;
			float num2 = c.y * c3.z - c.z * c3.y;
			float num3 = c.y * c2.z - c.z * c2.y;
			return c.x * num - c2.x * num2 + c3.x * num3;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00006A1C File Offset: 0x00004C1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(float3x3 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint3(1899745391U, 1966790317U, 3516359879U) + math.asuint(v.c1) * math.uint3(3050356579U, 4178586719U, 2558655391U) + math.asuint(v.c2) * math.uint3(1453413133U, 2152428077U, 1938706661U)) + 1338588197U;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00006AAC File Offset: 0x00004CAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(float3x3 v)
		{
			return math.asuint(v.c0) * math.uint3(3439609253U, 3535343003U, 3546061613U) + math.asuint(v.c1) * math.uint3(2702024231U, 1452124841U, 1966089551U) + math.asuint(v.c2) * math.uint3(2668168249U, 1587512777U, 2353831999U) + 3101256173U;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00006B39 File Offset: 0x00004D39
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 float3x4(float3 c0, float3 c1, float3 c2, float3 c3)
		{
			return new float3x4(c0, c1, c2, c3);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00006B44 File Offset: 0x00004D44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 float3x4(float m00, float m01, float m02, float m03, float m10, float m11, float m12, float m13, float m20, float m21, float m22, float m23)
		{
			return new float3x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00006B6A File Offset: 0x00004D6A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 float3x4(float v)
		{
			return new float3x4(v);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00006B72 File Offset: 0x00004D72
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 float3x4(bool v)
		{
			return new float3x4(v);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00006B7A File Offset: 0x00004D7A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 float3x4(bool3x4 v)
		{
			return new float3x4(v);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00006B82 File Offset: 0x00004D82
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 float3x4(int v)
		{
			return new float3x4(v);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00006B8A File Offset: 0x00004D8A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 float3x4(int3x4 v)
		{
			return new float3x4(v);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00006B92 File Offset: 0x00004D92
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 float3x4(uint v)
		{
			return new float3x4(v);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00006B9A File Offset: 0x00004D9A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 float3x4(uint3x4 v)
		{
			return new float3x4(v);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00006BA2 File Offset: 0x00004DA2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 float3x4(double v)
		{
			return new float3x4(v);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00006BAA File Offset: 0x00004DAA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 float3x4(double3x4 v)
		{
			return new float3x4(v);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006BB4 File Offset: 0x00004DB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 transpose(float3x4 v)
		{
			return math.float4x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z, v.c3.x, v.c3.y, v.c3.z);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00006C4C File Offset: 0x00004E4C
		public static float3x4 fastinverse(float3x4 m)
		{
			float3 c = m.c0;
			float3 c2 = m.c1;
			float3 c3 = m.c2;
			float3 @float = m.c3;
			float3 float2 = math.float3(c.x, c2.x, c3.x);
			float3 float3 = math.float3(c.y, c2.y, c3.y);
			float3 float4 = math.float3(c.z, c2.z, c3.z);
			@float = -(float2 * @float.x + float3 * @float.y + float4 * @float.z);
			return math.float3x4(float2, float3, float4, @float);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00006D00 File Offset: 0x00004F00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(float3x4 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint3(4192899797U, 3271228601U, 1634639009U) + math.asuint(v.c1) * math.uint3(3318036811U, 3404170631U, 2048213449U) + math.asuint(v.c2) * math.uint3(4164671783U, 1780759499U, 1352369353U) + math.asuint(v.c3) * math.uint3(2446407751U, 1391928079U, 3475533443U)) + 3777095341U;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00006DB8 File Offset: 0x00004FB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(float3x4 v)
		{
			return math.asuint(v.c0) * math.uint3(3385463369U, 1773538433U, 3773525029U) + math.asuint(v.c1) * math.uint3(4131962539U, 1809525511U, 4016293529U) + math.asuint(v.c2) * math.uint3(2416021567U, 2828384717U, 2636362241U) + math.asuint(v.c3) * math.uint3(1258410977U, 1952565773U, 2037535609U) + 3592785499U;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00006E6E File Offset: 0x0000506E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(float x, float y, float z, float w)
		{
			return new float4(x, y, z, w);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00006E79 File Offset: 0x00005079
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(float x, float y, float2 zw)
		{
			return new float4(x, y, zw);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00006E83 File Offset: 0x00005083
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(float x, float2 yz, float w)
		{
			return new float4(x, yz, w);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00006E8D File Offset: 0x0000508D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(float x, float3 yzw)
		{
			return new float4(x, yzw);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00006E96 File Offset: 0x00005096
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(float2 xy, float z, float w)
		{
			return new float4(xy, z, w);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00006EA0 File Offset: 0x000050A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(float2 xy, float2 zw)
		{
			return new float4(xy, zw);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00006EA9 File Offset: 0x000050A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(float3 xyz, float w)
		{
			return new float4(xyz, w);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00006EB2 File Offset: 0x000050B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(float4 xyzw)
		{
			return new float4(xyzw);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00006EBA File Offset: 0x000050BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(float v)
		{
			return new float4(v);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00006EC2 File Offset: 0x000050C2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(bool v)
		{
			return new float4(v);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00006ECA File Offset: 0x000050CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(bool4 v)
		{
			return new float4(v);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00006ED2 File Offset: 0x000050D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(int v)
		{
			return new float4(v);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00006EDA File Offset: 0x000050DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(int4 v)
		{
			return new float4(v);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006EE2 File Offset: 0x000050E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(uint v)
		{
			return new float4(v);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00006EEA File Offset: 0x000050EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(uint4 v)
		{
			return new float4(v);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00006EF2 File Offset: 0x000050F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(half v)
		{
			return new float4(v);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00006EFA File Offset: 0x000050FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(half4 v)
		{
			return new float4(v);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00006F02 File Offset: 0x00005102
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(double v)
		{
			return new float4(v);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00006F0A File Offset: 0x0000510A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(double4 v)
		{
			return new float4(v);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00006F12 File Offset: 0x00005112
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(float4 v)
		{
			return math.csum(math.asuint(v) * math.uint4(3868600063U, 3170963179U, 2632835537U, 1136528209U)) + 2944626401U;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00006F43 File Offset: 0x00005143
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(float4 v)
		{
			return math.asuint(v) * math.uint4(2972762423U, 1417889653U, 2080514593U, 2731544287U) + 2828498809U;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00006F73 File Offset: 0x00005173
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float shuffle(float4 left, float4 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006F7D File Offset: 0x0000517D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 shuffle(float4 left, float4 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.float2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00006F94 File Offset: 0x00005194
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 shuffle(float4 left, float4 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.float3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00006FB4 File Offset: 0x000051B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 shuffle(float4 left, float4 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.float4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00006FE0 File Offset: 0x000051E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static float select_shuffle_component(float4 a, float4 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.LeftZ:
				return a.z;
			case math.ShuffleComponent.LeftW:
				return a.w;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			case math.ShuffleComponent.RightZ:
				return b.z;
			case math.ShuffleComponent.RightW:
				return b.w;
			default:
				throw new ArgumentException("Invalid shuffle component: " + component.ToString());
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00007069 File Offset: 0x00005269
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 float4x2(float4 c0, float4 c1)
		{
			return new float4x2(c0, c1);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00007072 File Offset: 0x00005272
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 float4x2(float m00, float m01, float m10, float m11, float m20, float m21, float m30, float m31)
		{
			return new float4x2(m00, m01, m10, m11, m20, m21, m30, m31);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00007085 File Offset: 0x00005285
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 float4x2(float v)
		{
			return new float4x2(v);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000708D File Offset: 0x0000528D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 float4x2(bool v)
		{
			return new float4x2(v);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00007095 File Offset: 0x00005295
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 float4x2(bool4x2 v)
		{
			return new float4x2(v);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000709D File Offset: 0x0000529D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 float4x2(int v)
		{
			return new float4x2(v);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000070A5 File Offset: 0x000052A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 float4x2(int4x2 v)
		{
			return new float4x2(v);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000070AD File Offset: 0x000052AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 float4x2(uint v)
		{
			return new float4x2(v);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000070B5 File Offset: 0x000052B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 float4x2(uint4x2 v)
		{
			return new float4x2(v);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000070BD File Offset: 0x000052BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 float4x2(double v)
		{
			return new float4x2(v);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000070C5 File Offset: 0x000052C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 float4x2(double4x2 v)
		{
			return new float4x2(v);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000070D0 File Offset: 0x000052D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 transpose(float4x2 v)
		{
			return math.float2x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000713C File Offset: 0x0000533C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(float4x2 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint4(2864955997U, 3525118277U, 2298260269U, 1632478733U) + math.asuint(v.c1) * math.uint4(1537393931U, 2353355467U, 3441847433U, 4052036147U)) + 2011389559U;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000071AC File Offset: 0x000053AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(float4x2 v)
		{
			return math.asuint(v.c0) * math.uint4(2252224297U, 3784421429U, 1750626223U, 3571447507U) + math.asuint(v.c1) * math.uint4(3412283213U, 2601761069U, 1254033427U, 2248573027U) + 3612677113U;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000721A File Offset: 0x0000541A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 float4x3(float4 c0, float4 c1, float4 c2)
		{
			return new float4x3(c0, c1, c2);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00007224 File Offset: 0x00005424
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 float4x3(float m00, float m01, float m02, float m10, float m11, float m12, float m20, float m21, float m22, float m30, float m31, float m32)
		{
			return new float4x3(m00, m01, m02, m10, m11, m12, m20, m21, m22, m30, m31, m32);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000724A File Offset: 0x0000544A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 float4x3(float v)
		{
			return new float4x3(v);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00007252 File Offset: 0x00005452
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 float4x3(bool v)
		{
			return new float4x3(v);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000725A File Offset: 0x0000545A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 float4x3(bool4x3 v)
		{
			return new float4x3(v);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00007262 File Offset: 0x00005462
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 float4x3(int v)
		{
			return new float4x3(v);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000726A File Offset: 0x0000546A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 float4x3(int4x3 v)
		{
			return new float4x3(v);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00007272 File Offset: 0x00005472
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 float4x3(uint v)
		{
			return new float4x3(v);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000727A File Offset: 0x0000547A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 float4x3(uint4x3 v)
		{
			return new float4x3(v);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00007282 File Offset: 0x00005482
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 float4x3(double v)
		{
			return new float4x3(v);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000728A File Offset: 0x0000548A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 float4x3(double4x3 v)
		{
			return new float4x3(v);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00007294 File Offset: 0x00005494
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 transpose(float4x3 v)
		{
			return math.float3x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000732C File Offset: 0x0000552C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(float4x3 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint4(3309258581U, 1770373673U, 3778261171U, 3286279097U) + math.asuint(v.c1) * math.uint4(4264629071U, 1898591447U, 2641864091U, 1229113913U) + math.asuint(v.c2) * math.uint4(3020867117U, 1449055807U, 2479033387U, 3702457169U)) + 1845824257U;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000073CC File Offset: 0x000055CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(float4x3 v)
		{
			return math.asuint(v.c0) * math.uint4(1963973621U, 2134758553U, 1391111867U, 1167706003U) + math.asuint(v.c1) * math.uint4(2209736489U, 3261535807U, 1740411209U, 2910609089U) + math.asuint(v.c2) * math.uint4(2183822701U, 3029516053U, 3547472099U, 2057487037U) + 3781937309U;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007468 File Offset: 0x00005668
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(float4 c0, float4 c1, float4 c2, float4 c3)
		{
			return new float4x4(c0, c1, c2, c3);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00007474 File Offset: 0x00005674
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(float m00, float m01, float m02, float m03, float m10, float m11, float m12, float m13, float m20, float m21, float m22, float m23, float m30, float m31, float m32, float m33)
		{
			return new float4x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23, m30, m31, m32, m33);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000074A2 File Offset: 0x000056A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(float v)
		{
			return new float4x4(v);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000074AA File Offset: 0x000056AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(bool v)
		{
			return new float4x4(v);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000074B2 File Offset: 0x000056B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(bool4x4 v)
		{
			return new float4x4(v);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000074BA File Offset: 0x000056BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(int v)
		{
			return new float4x4(v);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000074C2 File Offset: 0x000056C2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(int4x4 v)
		{
			return new float4x4(v);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000074CA File Offset: 0x000056CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(uint v)
		{
			return new float4x4(v);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000074D2 File Offset: 0x000056D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(uint4x4 v)
		{
			return new float4x4(v);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000074DA File Offset: 0x000056DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(double v)
		{
			return new float4x4(v);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000074E2 File Offset: 0x000056E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(double4x4 v)
		{
			return new float4x4(v);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000074EC File Offset: 0x000056EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 rotate(float4x4 a, float3 b)
		{
			return (a.c0 * b.x + a.c1 * b.y + a.c2 * b.z).xyz;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00007540 File Offset: 0x00005740
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 transform(float4x4 a, float3 b)
		{
			return (a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3).xyz;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000075A0 File Offset: 0x000057A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 transpose(float4x4 v)
		{
			return math.float4x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w, v.c3.x, v.c3.y, v.c3.z, v.c3.w);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00007664 File Offset: 0x00005864
		public static float4x4 inverse(float4x4 m)
		{
			float4 c = m.c0;
			float4 c2 = m.c1;
			float4 c3 = m.c2;
			float4 c4 = m.c3;
			float4 @float = math.movelh(c2, c);
			float4 float2 = math.movelh(c3, c4);
			float4 float3 = math.movehl(c, c2);
			float4 float4 = math.movehl(c4, c3);
			float4 lhs = math.shuffle(c2, c, math.ShuffleComponent.LeftY, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightY, math.ShuffleComponent.RightZ);
			float4 lhs2 = math.shuffle(c3, c4, math.ShuffleComponent.LeftY, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightY, math.ShuffleComponent.RightZ);
			float4 lhs3 = math.shuffle(c2, c, math.ShuffleComponent.LeftW, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightW, math.ShuffleComponent.RightX);
			float4 lhs4 = math.shuffle(c3, c4, math.ShuffleComponent.LeftW, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightW, math.ShuffleComponent.RightX);
			float4 lhs5 = math.shuffle(float2, @float, math.ShuffleComponent.LeftZ, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightX, math.ShuffleComponent.RightZ);
			float4 lhs6 = math.shuffle(float2, @float, math.ShuffleComponent.LeftW, math.ShuffleComponent.LeftY, math.ShuffleComponent.RightY, math.ShuffleComponent.RightW);
			float4 lhs7 = math.shuffle(float4, float3, math.ShuffleComponent.LeftZ, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightX, math.ShuffleComponent.RightZ);
			float4 lhs8 = math.shuffle(float4, float3, math.ShuffleComponent.LeftW, math.ShuffleComponent.LeftY, math.ShuffleComponent.RightY, math.ShuffleComponent.RightW);
			float4 lhs9 = math.shuffle(@float, float2, math.ShuffleComponent.LeftZ, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightX, math.ShuffleComponent.RightZ);
			float4 float5 = lhs * float4 - lhs2 * float3;
			float4 float6 = @float * float4 - float2 * float3;
			float4 float7 = lhs4 * @float - lhs3 * float2;
			float4 rhs = math.shuffle(float5, float5, math.ShuffleComponent.LeftX, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightZ, math.ShuffleComponent.RightX);
			float4 rhs2 = math.shuffle(float5, float5, math.ShuffleComponent.LeftY, math.ShuffleComponent.LeftW, math.ShuffleComponent.RightW, math.ShuffleComponent.RightY);
			float4 rhs3 = math.shuffle(float6, float6, math.ShuffleComponent.LeftX, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightZ, math.ShuffleComponent.RightX);
			float4 rhs4 = math.shuffle(float6, float6, math.ShuffleComponent.LeftY, math.ShuffleComponent.LeftW, math.ShuffleComponent.RightW, math.ShuffleComponent.RightY);
			float4 float8 = lhs8 * rhs - lhs7 * rhs4 + lhs6 * rhs2;
			float4 float9 = lhs9 * float8;
			float9 += math.shuffle(float9, float9, math.ShuffleComponent.LeftY, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightW, math.ShuffleComponent.RightZ);
			float9 -= math.shuffle(float9, float9, math.ShuffleComponent.LeftZ, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightX, math.ShuffleComponent.RightX);
			float4 rhs5 = math.float4(1f) / float9;
			float4x4 result;
			result.c0 = float8 * rhs5;
			float4 rhs6 = math.shuffle(float7, float7, math.ShuffleComponent.LeftX, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightZ, math.ShuffleComponent.RightX);
			float4 rhs7 = math.shuffle(float7, float7, math.ShuffleComponent.LeftY, math.ShuffleComponent.LeftW, math.ShuffleComponent.RightW, math.ShuffleComponent.RightY);
			float4 lhs10 = lhs7 * rhs6 - lhs5 * rhs2 - lhs8 * rhs3;
			result.c1 = lhs10 * rhs5;
			float4 lhs11 = lhs5 * rhs4 - lhs6 * rhs6 - lhs8 * rhs7;
			result.c2 = lhs11 * rhs5;
			float4 lhs12 = lhs6 * rhs3 - lhs5 * rhs + lhs7 * rhs7;
			result.c3 = lhs12 * rhs5;
			return result;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000078F4 File Offset: 0x00005AF4
		public static float4x4 fastinverse(float4x4 m)
		{
			float4 c = m.c0;
			float4 c2 = m.c1;
			float4 c3 = m.c2;
			float4 @float = m.c3;
			float4 b = math.float4(0);
			float4 a = math.unpacklo(c, c3);
			float4 b2 = math.unpacklo(c2, b);
			float4 a2 = math.unpackhi(c, c3);
			float4 b3 = math.unpackhi(c2, b);
			float4 float2 = math.unpacklo(a, b2);
			float4 float3 = math.unpackhi(a, b2);
			float4 float4 = math.unpacklo(a2, b3);
			@float = -(float2 * @float.x + float3 * @float.y + float4 * @float.z);
			@float.w = 1f;
			return math.float4x4(float2, float3, float4, @float);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000079B4 File Offset: 0x00005BB4
		public static float determinant(float4x4 m)
		{
			float4 c = m.c0;
			float4 c2 = m.c1;
			float4 c3 = m.c2;
			float4 c4 = m.c3;
			float num = c2.y * (c3.z * c4.w - c3.w * c4.z) - c3.y * (c2.z * c4.w - c2.w * c4.z) + c4.y * (c2.z * c3.w - c2.w * c3.z);
			float num2 = c.y * (c3.z * c4.w - c3.w * c4.z) - c3.y * (c.z * c4.w - c.w * c4.z) + c4.y * (c.z * c3.w - c.w * c3.z);
			float num3 = c.y * (c2.z * c4.w - c2.w * c4.z) - c2.y * (c.z * c4.w - c.w * c4.z) + c4.y * (c.z * c2.w - c.w * c2.z);
			float num4 = c.y * (c2.z * c3.w - c2.w * c3.z) - c2.y * (c.z * c3.w - c.w * c3.z) + c3.y * (c.z * c2.w - c.w * c2.z);
			return c.x * num - c2.x * num2 + c3.x * num3 - c4.x * num4;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00007BAC File Offset: 0x00005DAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(float4x4 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint4(3299952959U, 3121178323U, 2948522579U, 1531026433U) + math.asuint(v.c1) * math.uint4(1365086453U, 3969870067U, 4192899797U, 3271228601U) + math.asuint(v.c2) * math.uint4(1634639009U, 3318036811U, 3404170631U, 2048213449U) + math.asuint(v.c3) * math.uint4(4164671783U, 1780759499U, 1352369353U, 2446407751U)) + 1391928079U;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00007C78 File Offset: 0x00005E78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(float4x4 v)
		{
			return math.asuint(v.c0) * math.uint4(3475533443U, 3777095341U, 3385463369U, 1773538433U) + math.asuint(v.c1) * math.uint4(3773525029U, 4131962539U, 1809525511U, 4016293529U) + math.asuint(v.c2) * math.uint4(2416021567U, 2828384717U, 2636362241U, 1258410977U) + math.asuint(v.c3) * math.uint4(1952565773U, 2037535609U, 3592785499U, 3996716183U) + 2626301701U;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00007D42 File Offset: 0x00005F42
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half half(half x)
		{
			return new half(x);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00007D4A File Offset: 0x00005F4A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half half(float v)
		{
			return new half(v);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00007D52 File Offset: 0x00005F52
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half half(double v)
		{
			return new half(v);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00007D5A File Offset: 0x00005F5A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(half v)
		{
			return (uint)v.value * 1952372791U + 2171534173U;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00007D6E File Offset: 0x00005F6E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half2 half2(half x, half y)
		{
			return new half2(x, y);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00007D77 File Offset: 0x00005F77
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half2 half2(half2 xy)
		{
			return new half2(xy);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00007D7F File Offset: 0x00005F7F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half2 half2(half v)
		{
			return new half2(v);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00007D87 File Offset: 0x00005F87
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half2 half2(float v)
		{
			return new half2(v);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00007D8F File Offset: 0x00005F8F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half2 half2(float2 v)
		{
			return new half2(v);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00007D97 File Offset: 0x00005F97
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half2 half2(double v)
		{
			return new half2(v);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00007D9F File Offset: 0x00005F9F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half2 half2(double2 v)
		{
			return new half2(v);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00007DA7 File Offset: 0x00005FA7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(half2 v)
		{
			return math.csum(math.uint2((uint)v.x.value, (uint)v.y.value) * math.uint2(1851936439U, 1938025801U)) + 3712598587U;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00007DE3 File Offset: 0x00005FE3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(half2 v)
		{
			return math.uint2((uint)v.x.value, (uint)v.y.value) * math.uint2(3956330501U, 2437373431U) + 1441286183U;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00007E1E File Offset: 0x0000601E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half3 half3(half x, half y, half z)
		{
			return new half3(x, y, z);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00007E28 File Offset: 0x00006028
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half3 half3(half x, half2 yz)
		{
			return new half3(x, yz);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00007E31 File Offset: 0x00006031
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half3 half3(half2 xy, half z)
		{
			return new half3(xy, z);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00007E3A File Offset: 0x0000603A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half3 half3(half3 xyz)
		{
			return new half3(xyz);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00007E42 File Offset: 0x00006042
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half3 half3(half v)
		{
			return new half3(v);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00007E4A File Offset: 0x0000604A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half3 half3(float v)
		{
			return new half3(v);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00007E52 File Offset: 0x00006052
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half3 half3(float3 v)
		{
			return new half3(v);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00007E5A File Offset: 0x0000605A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half3 half3(double v)
		{
			return new half3(v);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00007E62 File Offset: 0x00006062
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half3 half3(double3 v)
		{
			return new half3(v);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00007E6C File Offset: 0x0000606C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(half3 v)
		{
			return math.csum(math.uint3((uint)v.x.value, (uint)v.y.value, (uint)v.z.value) * math.uint3(1750611407U, 3285396193U, 3110507567U)) + 4271396531U;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00007EC4 File Offset: 0x000060C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(half3 v)
		{
			return math.uint3((uint)v.x.value, (uint)v.y.value, (uint)v.z.value) * math.uint3(4198118021U, 2908068253U, 3705492289U) + 2497566569U;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00007F1A File Offset: 0x0000611A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half4 half4(half x, half y, half z, half w)
		{
			return new half4(x, y, z, w);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00007F25 File Offset: 0x00006125
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half4 half4(half x, half y, half2 zw)
		{
			return new half4(x, y, zw);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00007F2F File Offset: 0x0000612F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half4 half4(half x, half2 yz, half w)
		{
			return new half4(x, yz, w);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00007F39 File Offset: 0x00006139
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half4 half4(half x, half3 yzw)
		{
			return new half4(x, yzw);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00007F42 File Offset: 0x00006142
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half4 half4(half2 xy, half z, half w)
		{
			return new half4(xy, z, w);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00007F4C File Offset: 0x0000614C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half4 half4(half2 xy, half2 zw)
		{
			return new half4(xy, zw);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00007F55 File Offset: 0x00006155
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half4 half4(half3 xyz, half w)
		{
			return new half4(xyz, w);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00007F5E File Offset: 0x0000615E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half4 half4(half4 xyzw)
		{
			return new half4(xyzw);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00007F66 File Offset: 0x00006166
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half4 half4(half v)
		{
			return new half4(v);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00007F6E File Offset: 0x0000616E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half4 half4(float v)
		{
			return new half4(v);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00007F76 File Offset: 0x00006176
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half4 half4(float4 v)
		{
			return new half4(v);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00007F7E File Offset: 0x0000617E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half4 half4(double v)
		{
			return new half4(v);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00007F86 File Offset: 0x00006186
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half4 half4(double4 v)
		{
			return new half4(v);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00007F90 File Offset: 0x00006190
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(half4 v)
		{
			return math.csum(math.uint4((uint)v.x.value, (uint)v.y.value, (uint)v.z.value, (uint)v.w.value) * math.uint4(1952372791U, 2631698677U, 4200781601U, 2119021007U)) + 1760485621U;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00007FF8 File Offset: 0x000061F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(half4 v)
		{
			return math.uint4((uint)v.x.value, (uint)v.y.value, (uint)v.z.value, (uint)v.w.value) * math.uint4(3157985881U, 2171534173U, 2723054263U, 1168253063U) + 4228926523U;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000805E File Offset: 0x0000625E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 int2(int x, int y)
		{
			return new int2(x, y);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00008067 File Offset: 0x00006267
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 int2(int2 xy)
		{
			return new int2(xy);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000806F File Offset: 0x0000626F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 int2(int v)
		{
			return new int2(v);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00008077 File Offset: 0x00006277
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 int2(bool v)
		{
			return new int2(v);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000807F File Offset: 0x0000627F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 int2(bool2 v)
		{
			return new int2(v);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00008087 File Offset: 0x00006287
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 int2(uint v)
		{
			return new int2(v);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000808F File Offset: 0x0000628F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 int2(uint2 v)
		{
			return new int2(v);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00008097 File Offset: 0x00006297
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 int2(float v)
		{
			return new int2(v);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000809F File Offset: 0x0000629F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 int2(float2 v)
		{
			return new int2(v);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x000080A7 File Offset: 0x000062A7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 int2(double v)
		{
			return new int2(v);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000080AF File Offset: 0x000062AF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 int2(double2 v)
		{
			return new int2(v);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000080B7 File Offset: 0x000062B7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(int2 v)
		{
			return math.csum(math.asuint(v) * math.uint2(2209710647U, 2201894441U)) + 2849577407U;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000080DE File Offset: 0x000062DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(int2 v)
		{
			return math.asuint(v) * math.uint2(3287031191U, 3098675399U) + 1564399943U;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00008104 File Offset: 0x00006304
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int shuffle(int2 left, int2 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000810E File Offset: 0x0000630E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 shuffle(int2 left, int2 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.int2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00008125 File Offset: 0x00006325
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 shuffle(int2 left, int2 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.int3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00008145 File Offset: 0x00006345
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 shuffle(int2 left, int2 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.int4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00008170 File Offset: 0x00006370
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static int select_shuffle_component(int2 a, int2 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			}
			throw new ArgumentException("Invalid shuffle component: " + component.ToString());
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000081D5 File Offset: 0x000063D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 int2x2(int2 c0, int2 c1)
		{
			return new int2x2(c0, c1);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000081DE File Offset: 0x000063DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 int2x2(int m00, int m01, int m10, int m11)
		{
			return new int2x2(m00, m01, m10, m11);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000081E9 File Offset: 0x000063E9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 int2x2(int v)
		{
			return new int2x2(v);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000081F1 File Offset: 0x000063F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 int2x2(bool v)
		{
			return new int2x2(v);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000081F9 File Offset: 0x000063F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 int2x2(bool2x2 v)
		{
			return new int2x2(v);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00008201 File Offset: 0x00006401
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 int2x2(uint v)
		{
			return new int2x2(v);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00008209 File Offset: 0x00006409
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 int2x2(uint2x2 v)
		{
			return new int2x2(v);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00008211 File Offset: 0x00006411
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 int2x2(float v)
		{
			return new int2x2(v);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00008219 File Offset: 0x00006419
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 int2x2(float2x2 v)
		{
			return new int2x2(v);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00008221 File Offset: 0x00006421
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 int2x2(double v)
		{
			return new int2x2(v);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00008229 File Offset: 0x00006429
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 int2x2(double2x2 v)
		{
			return new int2x2(v);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00008231 File Offset: 0x00006431
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 transpose(int2x2 v)
		{
			return math.int2x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00008264 File Offset: 0x00006464
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int determinant(int2x2 m)
		{
			int x = m.c0.x;
			int x2 = m.c1.x;
			int y = m.c0.y;
			int y2 = m.c1.y;
			return x * y2 - x2 * y;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x000082A8 File Offset: 0x000064A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(int2x2 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint2(3784421429U, 1750626223U) + math.asuint(v.c1) * math.uint2(3571447507U, 3412283213U)) + 2601761069U;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00008304 File Offset: 0x00006504
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(int2x2 v)
		{
			return math.asuint(v.c0) * math.uint2(1254033427U, 2248573027U) + math.asuint(v.c1) * math.uint2(3612677113U, 1521739981U) + 1735296007U;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000835E File Offset: 0x0000655E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 int2x3(int2 c0, int2 c1, int2 c2)
		{
			return new int2x3(c0, c1, c2);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00008368 File Offset: 0x00006568
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 int2x3(int m00, int m01, int m02, int m10, int m11, int m12)
		{
			return new int2x3(m00, m01, m02, m10, m11, m12);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00008377 File Offset: 0x00006577
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 int2x3(int v)
		{
			return new int2x3(v);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000837F File Offset: 0x0000657F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 int2x3(bool v)
		{
			return new int2x3(v);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00008387 File Offset: 0x00006587
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 int2x3(bool2x3 v)
		{
			return new int2x3(v);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000838F File Offset: 0x0000658F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 int2x3(uint v)
		{
			return new int2x3(v);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00008397 File Offset: 0x00006597
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 int2x3(uint2x3 v)
		{
			return new int2x3(v);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000839F File Offset: 0x0000659F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 int2x3(float v)
		{
			return new int2x3(v);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000083A7 File Offset: 0x000065A7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 int2x3(float2x3 v)
		{
			return new int2x3(v);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000083AF File Offset: 0x000065AF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 int2x3(double v)
		{
			return new int2x3(v);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x000083B7 File Offset: 0x000065B7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 int2x3(double2x3 v)
		{
			return new int2x3(v);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x000083C0 File Offset: 0x000065C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 transpose(int2x3 v)
		{
			return math.int3x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00008414 File Offset: 0x00006614
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(int2x3 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint2(3404170631U, 2048213449U) + math.asuint(v.c1) * math.uint2(4164671783U, 1780759499U) + math.asuint(v.c2) * math.uint2(1352369353U, 2446407751U)) + 1391928079U;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00008494 File Offset: 0x00006694
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(int2x3 v)
		{
			return math.asuint(v.c0) * math.uint2(3475533443U, 3777095341U) + math.asuint(v.c1) * math.uint2(3385463369U, 1773538433U) + math.asuint(v.c2) * math.uint2(3773525029U, 4131962539U) + 1809525511U;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00008512 File Offset: 0x00006712
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 int2x4(int2 c0, int2 c1, int2 c2, int2 c3)
		{
			return new int2x4(c0, c1, c2, c3);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000851D File Offset: 0x0000671D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 int2x4(int m00, int m01, int m02, int m03, int m10, int m11, int m12, int m13)
		{
			return new int2x4(m00, m01, m02, m03, m10, m11, m12, m13);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00008530 File Offset: 0x00006730
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 int2x4(int v)
		{
			return new int2x4(v);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00008538 File Offset: 0x00006738
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 int2x4(bool v)
		{
			return new int2x4(v);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00008540 File Offset: 0x00006740
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 int2x4(bool2x4 v)
		{
			return new int2x4(v);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00008548 File Offset: 0x00006748
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 int2x4(uint v)
		{
			return new int2x4(v);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00008550 File Offset: 0x00006750
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 int2x4(uint2x4 v)
		{
			return new int2x4(v);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00008558 File Offset: 0x00006758
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 int2x4(float v)
		{
			return new int2x4(v);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00008560 File Offset: 0x00006760
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 int2x4(float2x4 v)
		{
			return new int2x4(v);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00008568 File Offset: 0x00006768
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 int2x4(double v)
		{
			return new int2x4(v);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00008570 File Offset: 0x00006770
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 int2x4(double2x4 v)
		{
			return new int2x4(v);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00008578 File Offset: 0x00006778
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 transpose(int2x4 v)
		{
			return math.int4x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y, v.c3.x, v.c3.y);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x000085E4 File Offset: 0x000067E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(int2x4 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint2(2057338067U, 2942577577U) + math.asuint(v.c1) * math.uint2(2834440507U, 2671762487U) + math.asuint(v.c2) * math.uint2(2892026051U, 2455987759U) + math.asuint(v.c3) * math.uint2(3868600063U, 3170963179U)) + 2632835537U;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00008688 File Offset: 0x00006888
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(int2x4 v)
		{
			return math.asuint(v.c0) * math.uint2(1136528209U, 2944626401U) + math.asuint(v.c1) * math.uint2(2972762423U, 1417889653U) + math.asuint(v.c2) * math.uint2(2080514593U, 2731544287U) + math.asuint(v.c3) * math.uint2(2828498809U, 2669441947U) + 1260114311U;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000872A File Offset: 0x0000692A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 int3(int x, int y, int z)
		{
			return new int3(x, y, z);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00008734 File Offset: 0x00006934
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 int3(int x, int2 yz)
		{
			return new int3(x, yz);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000873D File Offset: 0x0000693D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 int3(int2 xy, int z)
		{
			return new int3(xy, z);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00008746 File Offset: 0x00006946
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 int3(int3 xyz)
		{
			return new int3(xyz);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000874E File Offset: 0x0000694E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 int3(int v)
		{
			return new int3(v);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00008756 File Offset: 0x00006956
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 int3(bool v)
		{
			return new int3(v);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000875E File Offset: 0x0000695E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 int3(bool3 v)
		{
			return new int3(v);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00008766 File Offset: 0x00006966
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 int3(uint v)
		{
			return new int3(v);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000876E File Offset: 0x0000696E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 int3(uint3 v)
		{
			return new int3(v);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00008776 File Offset: 0x00006976
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 int3(float v)
		{
			return new int3(v);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000877E File Offset: 0x0000697E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 int3(float3 v)
		{
			return new int3(v);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00008786 File Offset: 0x00006986
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 int3(double v)
		{
			return new int3(v);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000878E File Offset: 0x0000698E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 int3(double3 v)
		{
			return new int3(v);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00008796 File Offset: 0x00006996
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(int3 v)
		{
			return math.csum(math.asuint(v) * math.uint3(1283419601U, 1210229737U, 2864955997U)) + 3525118277U;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x000087C2 File Offset: 0x000069C2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(int3 v)
		{
			return math.asuint(v) * math.uint3(2298260269U, 1632478733U, 1537393931U) + 2353355467U;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x000087ED File Offset: 0x000069ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int shuffle(int3 left, int3 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000087F7 File Offset: 0x000069F7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 shuffle(int3 left, int3 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.int2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000880E File Offset: 0x00006A0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 shuffle(int3 left, int3 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.int3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000882E File Offset: 0x00006A2E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 shuffle(int3 left, int3 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.int4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00008858 File Offset: 0x00006A58
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static int select_shuffle_component(int3 a, int3 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.LeftZ:
				return a.z;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			case math.ShuffleComponent.RightZ:
				return b.z;
			}
			throw new ArgumentException("Invalid shuffle component: " + component.ToString());
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x000088CF File Offset: 0x00006ACF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 int3x2(int3 c0, int3 c1)
		{
			return new int3x2(c0, c1);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000088D8 File Offset: 0x00006AD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 int3x2(int m00, int m01, int m10, int m11, int m20, int m21)
		{
			return new int3x2(m00, m01, m10, m11, m20, m21);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x000088E7 File Offset: 0x00006AE7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 int3x2(int v)
		{
			return new int3x2(v);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x000088EF File Offset: 0x00006AEF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 int3x2(bool v)
		{
			return new int3x2(v);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000088F7 File Offset: 0x00006AF7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 int3x2(bool3x2 v)
		{
			return new int3x2(v);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x000088FF File Offset: 0x00006AFF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 int3x2(uint v)
		{
			return new int3x2(v);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00008907 File Offset: 0x00006B07
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 int3x2(uint3x2 v)
		{
			return new int3x2(v);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000890F File Offset: 0x00006B0F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 int3x2(float v)
		{
			return new int3x2(v);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00008917 File Offset: 0x00006B17
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 int3x2(float3x2 v)
		{
			return new int3x2(v);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000891F File Offset: 0x00006B1F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 int3x2(double v)
		{
			return new int3x2(v);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00008927 File Offset: 0x00006B27
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 int3x2(double3x2 v)
		{
			return new int3x2(v);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00008930 File Offset: 0x00006B30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 transpose(int3x2 v)
		{
			return math.int2x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00008984 File Offset: 0x00006B84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(int3x2 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint3(3678265601U, 2070747979U, 1480171127U) + math.asuint(v.c1) * math.uint3(1588341193U, 4234155257U, 1811310911U)) + 2635799963U;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x000089EC File Offset: 0x00006BEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(int3x2 v)
		{
			return math.asuint(v.c0) * math.uint3(4165137857U, 2759770933U, 2759319383U) + math.asuint(v.c1) * math.uint3(3299952959U, 3121178323U, 2948522579U) + 1531026433U;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00008A50 File Offset: 0x00006C50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 int3x3(int3 c0, int3 c1, int3 c2)
		{
			return new int3x3(c0, c1, c2);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00008A5C File Offset: 0x00006C5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 int3x3(int m00, int m01, int m02, int m10, int m11, int m12, int m20, int m21, int m22)
		{
			return new int3x3(m00, m01, m02, m10, m11, m12, m20, m21, m22);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00008A7C File Offset: 0x00006C7C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 int3x3(int v)
		{
			return new int3x3(v);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00008A84 File Offset: 0x00006C84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 int3x3(bool v)
		{
			return new int3x3(v);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00008A8C File Offset: 0x00006C8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 int3x3(bool3x3 v)
		{
			return new int3x3(v);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00008A94 File Offset: 0x00006C94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 int3x3(uint v)
		{
			return new int3x3(v);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00008A9C File Offset: 0x00006C9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 int3x3(uint3x3 v)
		{
			return new int3x3(v);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00008AA4 File Offset: 0x00006CA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 int3x3(float v)
		{
			return new int3x3(v);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00008AAC File Offset: 0x00006CAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 int3x3(float3x3 v)
		{
			return new int3x3(v);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00008AB4 File Offset: 0x00006CB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 int3x3(double v)
		{
			return new int3x3(v);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00008ABC File Offset: 0x00006CBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 int3x3(double3x3 v)
		{
			return new int3x3(v);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00008AC4 File Offset: 0x00006CC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 transpose(int3x3 v)
		{
			return math.int3x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00008B3C File Offset: 0x00006D3C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int determinant(int3x3 m)
		{
			int3 c = m.c0;
			int3 c2 = m.c1;
			int3 c3 = m.c2;
			int num = c2.y * c3.z - c2.z * c3.y;
			int num2 = c.y * c3.z - c.z * c3.y;
			int num3 = c.y * c2.z - c.z * c2.y;
			return c.x * num - c2.x * num2 + c3.x * num3;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00008BD0 File Offset: 0x00006DD0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(int3x3 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint3(2479033387U, 3702457169U, 1845824257U) + math.asuint(v.c1) * math.uint3(1963973621U, 2134758553U, 1391111867U) + math.asuint(v.c2) * math.uint3(1167706003U, 2209736489U, 3261535807U)) + 1740411209U;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00008C60 File Offset: 0x00006E60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(int3x3 v)
		{
			return math.asuint(v.c0) * math.uint3(2910609089U, 2183822701U, 3029516053U) + math.asuint(v.c1) * math.uint3(3547472099U, 2057487037U, 3781937309U) + math.asuint(v.c2) * math.uint3(2057338067U, 2942577577U, 2834440507U) + 2671762487U;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00008CED File Offset: 0x00006EED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 int3x4(int3 c0, int3 c1, int3 c2, int3 c3)
		{
			return new int3x4(c0, c1, c2, c3);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00008CF8 File Offset: 0x00006EF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 int3x4(int m00, int m01, int m02, int m03, int m10, int m11, int m12, int m13, int m20, int m21, int m22, int m23)
		{
			return new int3x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00008D1E File Offset: 0x00006F1E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 int3x4(int v)
		{
			return new int3x4(v);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00008D26 File Offset: 0x00006F26
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 int3x4(bool v)
		{
			return new int3x4(v);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00008D2E File Offset: 0x00006F2E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 int3x4(bool3x4 v)
		{
			return new int3x4(v);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00008D36 File Offset: 0x00006F36
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 int3x4(uint v)
		{
			return new int3x4(v);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00008D3E File Offset: 0x00006F3E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 int3x4(uint3x4 v)
		{
			return new int3x4(v);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00008D46 File Offset: 0x00006F46
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 int3x4(float v)
		{
			return new int3x4(v);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00008D4E File Offset: 0x00006F4E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 int3x4(float3x4 v)
		{
			return new int3x4(v);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00008D56 File Offset: 0x00006F56
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 int3x4(double v)
		{
			return new int3x4(v);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00008D5E File Offset: 0x00006F5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 int3x4(double3x4 v)
		{
			return new int3x4(v);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00008D68 File Offset: 0x00006F68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 transpose(int3x4 v)
		{
			return math.int4x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z, v.c3.x, v.c3.y, v.c3.z);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00008E00 File Offset: 0x00007000
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(int3x4 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint3(1521739981U, 1735296007U, 3010324327U) + math.asuint(v.c1) * math.uint3(1875523709U, 2937008387U, 3835713223U) + math.asuint(v.c2) * math.uint3(2216526373U, 3375971453U, 3559829411U) + math.asuint(v.c3) * math.uint3(3652178029U, 2544260129U, 2013864031U)) + 2627668003U;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00008EB8 File Offset: 0x000070B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(int3x4 v)
		{
			return math.asuint(v.c0) * math.uint3(1520214331U, 2949502447U, 2827819133U) + math.asuint(v.c1) * math.uint3(3480140317U, 2642994593U, 3940484981U) + math.asuint(v.c2) * math.uint3(1954192763U, 1091696537U, 3052428017U) + math.asuint(v.c3) * math.uint3(4253034763U, 2338696631U, 3757372771U) + 1885959949U;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00008F6E File Offset: 0x0000716E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(int x, int y, int z, int w)
		{
			return new int4(x, y, z, w);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00008F79 File Offset: 0x00007179
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(int x, int y, int2 zw)
		{
			return new int4(x, y, zw);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00008F83 File Offset: 0x00007183
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(int x, int2 yz, int w)
		{
			return new int4(x, yz, w);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00008F8D File Offset: 0x0000718D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(int x, int3 yzw)
		{
			return new int4(x, yzw);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00008F96 File Offset: 0x00007196
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(int2 xy, int z, int w)
		{
			return new int4(xy, z, w);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00008FA0 File Offset: 0x000071A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(int2 xy, int2 zw)
		{
			return new int4(xy, zw);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00008FA9 File Offset: 0x000071A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(int3 xyz, int w)
		{
			return new int4(xyz, w);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00008FB2 File Offset: 0x000071B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(int4 xyzw)
		{
			return new int4(xyzw);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00008FBA File Offset: 0x000071BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(int v)
		{
			return new int4(v);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00008FC2 File Offset: 0x000071C2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(bool v)
		{
			return new int4(v);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00008FCA File Offset: 0x000071CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(bool4 v)
		{
			return new int4(v);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00008FD2 File Offset: 0x000071D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(uint v)
		{
			return new int4(v);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00008FDA File Offset: 0x000071DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(uint4 v)
		{
			return new int4(v);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00008FE2 File Offset: 0x000071E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(float v)
		{
			return new int4(v);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00008FEA File Offset: 0x000071EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(float4 v)
		{
			return new int4(v);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00008FF2 File Offset: 0x000071F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(double v)
		{
			return new int4(v);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00008FFA File Offset: 0x000071FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(double4 v)
		{
			return new int4(v);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00009002 File Offset: 0x00007202
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(int4 v)
		{
			return math.csum(math.asuint(v) * math.uint4(1845824257U, 1963973621U, 2134758553U, 1391111867U)) + 1167706003U;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00009033 File Offset: 0x00007233
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(int4 v)
		{
			return math.asuint(v) * math.uint4(2209736489U, 3261535807U, 1740411209U, 2910609089U) + 2183822701U;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00009063 File Offset: 0x00007263
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int shuffle(int4 left, int4 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000906D File Offset: 0x0000726D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 shuffle(int4 left, int4 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.int2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00009084 File Offset: 0x00007284
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 shuffle(int4 left, int4 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.int3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x000090A4 File Offset: 0x000072A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 shuffle(int4 left, int4 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.int4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000090D0 File Offset: 0x000072D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static int select_shuffle_component(int4 a, int4 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.LeftZ:
				return a.z;
			case math.ShuffleComponent.LeftW:
				return a.w;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			case math.ShuffleComponent.RightZ:
				return b.z;
			case math.ShuffleComponent.RightW:
				return b.w;
			default:
				throw new ArgumentException("Invalid shuffle component: " + component.ToString());
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00009159 File Offset: 0x00007359
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 int4x2(int4 c0, int4 c1)
		{
			return new int4x2(c0, c1);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00009162 File Offset: 0x00007362
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 int4x2(int m00, int m01, int m10, int m11, int m20, int m21, int m30, int m31)
		{
			return new int4x2(m00, m01, m10, m11, m20, m21, m30, m31);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00009175 File Offset: 0x00007375
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 int4x2(int v)
		{
			return new int4x2(v);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000917D File Offset: 0x0000737D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 int4x2(bool v)
		{
			return new int4x2(v);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00009185 File Offset: 0x00007385
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 int4x2(bool4x2 v)
		{
			return new int4x2(v);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000918D File Offset: 0x0000738D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 int4x2(uint v)
		{
			return new int4x2(v);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00009195 File Offset: 0x00007395
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 int4x2(uint4x2 v)
		{
			return new int4x2(v);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000919D File Offset: 0x0000739D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 int4x2(float v)
		{
			return new int4x2(v);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x000091A5 File Offset: 0x000073A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 int4x2(float4x2 v)
		{
			return new int4x2(v);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x000091AD File Offset: 0x000073AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 int4x2(double v)
		{
			return new int4x2(v);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000091B5 File Offset: 0x000073B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 int4x2(double4x2 v)
		{
			return new int4x2(v);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x000091C0 File Offset: 0x000073C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 transpose(int4x2 v)
		{
			return math.int2x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000922C File Offset: 0x0000742C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(int4x2 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint4(4205774813U, 1650214333U, 3388112843U, 1831150513U) + math.asuint(v.c1) * math.uint4(1848374953U, 3430200247U, 2209710647U, 2201894441U)) + 2849577407U;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000929C File Offset: 0x0000749C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(int4x2 v)
		{
			return math.asuint(v.c0) * math.uint4(3287031191U, 3098675399U, 1564399943U, 1148435377U) + math.asuint(v.c1) * math.uint4(3416333663U, 1750611407U, 3285396193U, 3110507567U) + 4271396531U;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000930A File Offset: 0x0000750A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 int4x3(int4 c0, int4 c1, int4 c2)
		{
			return new int4x3(c0, c1, c2);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00009314 File Offset: 0x00007514
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 int4x3(int m00, int m01, int m02, int m10, int m11, int m12, int m20, int m21, int m22, int m30, int m31, int m32)
		{
			return new int4x3(m00, m01, m02, m10, m11, m12, m20, m21, m22, m30, m31, m32);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000933A File Offset: 0x0000753A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 int4x3(int v)
		{
			return new int4x3(v);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00009342 File Offset: 0x00007542
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 int4x3(bool v)
		{
			return new int4x3(v);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000934A File Offset: 0x0000754A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 int4x3(bool4x3 v)
		{
			return new int4x3(v);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00009352 File Offset: 0x00007552
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 int4x3(uint v)
		{
			return new int4x3(v);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000935A File Offset: 0x0000755A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 int4x3(uint4x3 v)
		{
			return new int4x3(v);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00009362 File Offset: 0x00007562
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 int4x3(float v)
		{
			return new int4x3(v);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000936A File Offset: 0x0000756A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 int4x3(float4x3 v)
		{
			return new int4x3(v);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00009372 File Offset: 0x00007572
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 int4x3(double v)
		{
			return new int4x3(v);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000937A File Offset: 0x0000757A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 int4x3(double4x3 v)
		{
			return new int4x3(v);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00009384 File Offset: 0x00007584
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 transpose(int4x3 v)
		{
			return math.int3x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000941C File Offset: 0x0000761C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(int4x3 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint4(1773538433U, 3773525029U, 4131962539U, 1809525511U) + math.asuint(v.c1) * math.uint4(4016293529U, 2416021567U, 2828384717U, 2636362241U) + math.asuint(v.c2) * math.uint4(1258410977U, 1952565773U, 2037535609U, 3592785499U)) + 3996716183U;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000094BC File Offset: 0x000076BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(int4x3 v)
		{
			return math.asuint(v.c0) * math.uint4(2626301701U, 1306289417U, 2096137163U, 1548578029U) + math.asuint(v.c1) * math.uint4(4178800919U, 3898072289U, 4129428421U, 2631575897U) + math.asuint(v.c2) * math.uint4(2854656703U, 3578504047U, 4245178297U, 2173281923U) + 2973357649U;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00009558 File Offset: 0x00007758
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 int4x4(int4 c0, int4 c1, int4 c2, int4 c3)
		{
			return new int4x4(c0, c1, c2, c3);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00009564 File Offset: 0x00007764
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 int4x4(int m00, int m01, int m02, int m03, int m10, int m11, int m12, int m13, int m20, int m21, int m22, int m23, int m30, int m31, int m32, int m33)
		{
			return new int4x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23, m30, m31, m32, m33);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00009592 File Offset: 0x00007792
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 int4x4(int v)
		{
			return new int4x4(v);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000959A File Offset: 0x0000779A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 int4x4(bool v)
		{
			return new int4x4(v);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x000095A2 File Offset: 0x000077A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 int4x4(bool4x4 v)
		{
			return new int4x4(v);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000095AA File Offset: 0x000077AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 int4x4(uint v)
		{
			return new int4x4(v);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000095B2 File Offset: 0x000077B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 int4x4(uint4x4 v)
		{
			return new int4x4(v);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x000095BA File Offset: 0x000077BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 int4x4(float v)
		{
			return new int4x4(v);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x000095C2 File Offset: 0x000077C2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 int4x4(float4x4 v)
		{
			return new int4x4(v);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x000095CA File Offset: 0x000077CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 int4x4(double v)
		{
			return new int4x4(v);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x000095D2 File Offset: 0x000077D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 int4x4(double4x4 v)
		{
			return new int4x4(v);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x000095DC File Offset: 0x000077DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 transpose(int4x4 v)
		{
			return math.int4x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w, v.c3.x, v.c3.y, v.c3.z, v.c3.w);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000096A0 File Offset: 0x000078A0
		public static int determinant(int4x4 m)
		{
			int4 c = m.c0;
			int4 c2 = m.c1;
			int4 c3 = m.c2;
			int4 c4 = m.c3;
			int num = c2.y * (c3.z * c4.w - c3.w * c4.z) - c3.y * (c2.z * c4.w - c2.w * c4.z) + c4.y * (c2.z * c3.w - c2.w * c3.z);
			int num2 = c.y * (c3.z * c4.w - c3.w * c4.z) - c3.y * (c.z * c4.w - c.w * c4.z) + c4.y * (c.z * c3.w - c.w * c3.z);
			int num3 = c.y * (c2.z * c4.w - c2.w * c4.z) - c2.y * (c.z * c4.w - c.w * c4.z) + c4.y * (c.z * c2.w - c.w * c2.z);
			int num4 = c.y * (c2.z * c3.w - c2.w * c3.z) - c2.y * (c.z * c3.w - c.w * c3.z) + c3.y * (c.z * c2.w - c.w * c2.z);
			return c.x * num - c2.x * num2 + c3.x * num3 - c4.x * num4;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00009898 File Offset: 0x00007A98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(int4x4 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint4(1562056283U, 2265541847U, 1283419601U, 1210229737U) + math.asuint(v.c1) * math.uint4(2864955997U, 3525118277U, 2298260269U, 1632478733U) + math.asuint(v.c2) * math.uint4(1537393931U, 2353355467U, 3441847433U, 4052036147U) + math.asuint(v.c3) * math.uint4(2011389559U, 2252224297U, 3784421429U, 1750626223U)) + 3571447507U;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00009964 File Offset: 0x00007B64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(int4x4 v)
		{
			return math.asuint(v.c0) * math.uint4(3412283213U, 2601761069U, 1254033427U, 2248573027U) + math.asuint(v.c1) * math.uint4(3612677113U, 1521739981U, 1735296007U, 3010324327U) + math.asuint(v.c2) * math.uint4(1875523709U, 2937008387U, 3835713223U, 2216526373U) + math.asuint(v.c3) * math.uint4(3375971453U, 3559829411U, 3652178029U, 2544260129U) + 2013864031U;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00009A2E File Offset: 0x00007C2E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int asint(uint x)
		{
			return (int)x;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00009A31 File Offset: 0x00007C31
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 asint(uint2 x)
		{
			return math.int2((int)x.x, (int)x.y);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00009A44 File Offset: 0x00007C44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 asint(uint3 x)
		{
			return math.int3((int)x.x, (int)x.y, (int)x.z);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00009A5D File Offset: 0x00007C5D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 asint(uint4 x)
		{
			return math.int4((int)x.x, (int)x.y, (int)x.z, (int)x.w);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00009A7C File Offset: 0x00007C7C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int asint(float x)
		{
			math.IntFloatUnion intFloatUnion;
			intFloatUnion.intValue = 0;
			intFloatUnion.floatValue = x;
			return intFloatUnion.intValue;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00009A9F File Offset: 0x00007C9F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 asint(float2 x)
		{
			return math.int2(math.asint(x.x), math.asint(x.y));
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00009ABC File Offset: 0x00007CBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 asint(float3 x)
		{
			return math.int3(math.asint(x.x), math.asint(x.y), math.asint(x.z));
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00009AE4 File Offset: 0x00007CE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 asint(float4 x)
		{
			return math.int4(math.asint(x.x), math.asint(x.y), math.asint(x.z), math.asint(x.w));
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00009B17 File Offset: 0x00007D17
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint asuint(int x)
		{
			return (uint)x;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00009B1A File Offset: 0x00007D1A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 asuint(int2 x)
		{
			return math.uint2((uint)x.x, (uint)x.y);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00009B2D File Offset: 0x00007D2D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 asuint(int3 x)
		{
			return math.uint3((uint)x.x, (uint)x.y, (uint)x.z);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00009B46 File Offset: 0x00007D46
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 asuint(int4 x)
		{
			return math.uint4((uint)x.x, (uint)x.y, (uint)x.z, (uint)x.w);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00009B65 File Offset: 0x00007D65
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint asuint(float x)
		{
			return (uint)math.asint(x);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00009B6D File Offset: 0x00007D6D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 asuint(float2 x)
		{
			return math.uint2(math.asuint(x.x), math.asuint(x.y));
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00009B8A File Offset: 0x00007D8A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 asuint(float3 x)
		{
			return math.uint3(math.asuint(x.x), math.asuint(x.y), math.asuint(x.z));
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00009BB2 File Offset: 0x00007DB2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 asuint(float4 x)
		{
			return math.uint4(math.asuint(x.x), math.asuint(x.y), math.asuint(x.z), math.asuint(x.w));
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00009BE5 File Offset: 0x00007DE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long aslong(ulong x)
		{
			return (long)x;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00009BE8 File Offset: 0x00007DE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long aslong(double x)
		{
			math.LongDoubleUnion longDoubleUnion;
			longDoubleUnion.longValue = 0L;
			longDoubleUnion.doubleValue = x;
			return longDoubleUnion.longValue;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00009C0C File Offset: 0x00007E0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong asulong(long x)
		{
			return (ulong)x;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00009C0F File Offset: 0x00007E0F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong asulong(double x)
		{
			return (ulong)math.aslong(x);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00009C18 File Offset: 0x00007E18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float asfloat(int x)
		{
			math.IntFloatUnion intFloatUnion;
			intFloatUnion.floatValue = 0f;
			intFloatUnion.intValue = x;
			return intFloatUnion.floatValue;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00009C3F File Offset: 0x00007E3F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 asfloat(int2 x)
		{
			return math.float2(math.asfloat(x.x), math.asfloat(x.y));
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00009C5C File Offset: 0x00007E5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 asfloat(int3 x)
		{
			return math.float3(math.asfloat(x.x), math.asfloat(x.y), math.asfloat(x.z));
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00009C84 File Offset: 0x00007E84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 asfloat(int4 x)
		{
			return math.float4(math.asfloat(x.x), math.asfloat(x.y), math.asfloat(x.z), math.asfloat(x.w));
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00009CB7 File Offset: 0x00007EB7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float asfloat(uint x)
		{
			return math.asfloat((int)x);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00009CBF File Offset: 0x00007EBF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 asfloat(uint2 x)
		{
			return math.float2(math.asfloat(x.x), math.asfloat(x.y));
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00009CDC File Offset: 0x00007EDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 asfloat(uint3 x)
		{
			return math.float3(math.asfloat(x.x), math.asfloat(x.y), math.asfloat(x.z));
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00009D04 File Offset: 0x00007F04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 asfloat(uint4 x)
		{
			return math.float4(math.asfloat(x.x), math.asfloat(x.y), math.asfloat(x.z), math.asfloat(x.w));
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00009D38 File Offset: 0x00007F38
		public static int bitmask(bool4 value)
		{
			int num = 0;
			if (value.x)
			{
				num |= 1;
			}
			if (value.y)
			{
				num |= 2;
			}
			if (value.z)
			{
				num |= 4;
			}
			if (value.w)
			{
				num |= 8;
			}
			return num;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00009D78 File Offset: 0x00007F78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double asdouble(long x)
		{
			math.LongDoubleUnion longDoubleUnion;
			longDoubleUnion.doubleValue = 0.0;
			longDoubleUnion.longValue = x;
			return longDoubleUnion.doubleValue;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00009DA3 File Offset: 0x00007FA3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double asdouble(ulong x)
		{
			return math.asdouble((long)x);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00009DAB File Offset: 0x00007FAB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool isfinite(float x)
		{
			return math.abs(x) < float.PositiveInfinity;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00009DBA File Offset: 0x00007FBA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 isfinite(float2 x)
		{
			return math.abs(x) < float.PositiveInfinity;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00009DCC File Offset: 0x00007FCC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 isfinite(float3 x)
		{
			return math.abs(x) < float.PositiveInfinity;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00009DDE File Offset: 0x00007FDE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 isfinite(float4 x)
		{
			return math.abs(x) < float.PositiveInfinity;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00009DF0 File Offset: 0x00007FF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool isfinite(double x)
		{
			return math.abs(x) < double.PositiveInfinity;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00009E03 File Offset: 0x00008003
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 isfinite(double2 x)
		{
			return math.abs(x) < double.PositiveInfinity;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00009E19 File Offset: 0x00008019
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 isfinite(double3 x)
		{
			return math.abs(x) < double.PositiveInfinity;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00009E2F File Offset: 0x0000802F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 isfinite(double4 x)
		{
			return math.abs(x) < double.PositiveInfinity;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00009E45 File Offset: 0x00008045
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool isinf(float x)
		{
			return math.abs(x) == float.PositiveInfinity;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00009E54 File Offset: 0x00008054
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 isinf(float2 x)
		{
			return math.abs(x) == float.PositiveInfinity;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00009E66 File Offset: 0x00008066
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 isinf(float3 x)
		{
			return math.abs(x) == float.PositiveInfinity;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00009E78 File Offset: 0x00008078
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 isinf(float4 x)
		{
			return math.abs(x) == float.PositiveInfinity;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00009E8A File Offset: 0x0000808A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool isinf(double x)
		{
			return math.abs(x) == double.PositiveInfinity;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00009E9D File Offset: 0x0000809D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 isinf(double2 x)
		{
			return math.abs(x) == double.PositiveInfinity;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00009EB3 File Offset: 0x000080B3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 isinf(double3 x)
		{
			return math.abs(x) == double.PositiveInfinity;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00009EC9 File Offset: 0x000080C9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 isinf(double4 x)
		{
			return math.abs(x) == double.PositiveInfinity;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00009EDF File Offset: 0x000080DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool isnan(float x)
		{
			return (math.asuint(x) & 2147483647U) > 2139095040U;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00009EF4 File Offset: 0x000080F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 isnan(float2 x)
		{
			return (math.asuint(x) & 2147483647U) > 2139095040U;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00009F10 File Offset: 0x00008110
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 isnan(float3 x)
		{
			return (math.asuint(x) & 2147483647U) > 2139095040U;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00009F2C File Offset: 0x0000812C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 isnan(float4 x)
		{
			return (math.asuint(x) & 2147483647U) > 2139095040U;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00009F48 File Offset: 0x00008148
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool isnan(double x)
		{
			return (math.asulong(x) & 9223372036854775807UL) > 9218868437227405312UL;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00009F68 File Offset: 0x00008168
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 isnan(double2 x)
		{
			return math.bool2((math.asulong(x.x) & 9223372036854775807UL) > 9218868437227405312UL, (math.asulong(x.y) & 9223372036854775807UL) > 9218868437227405312UL);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00009FBC File Offset: 0x000081BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 isnan(double3 x)
		{
			return math.bool3((math.asulong(x.x) & 9223372036854775807UL) > 9218868437227405312UL, (math.asulong(x.y) & 9223372036854775807UL) > 9218868437227405312UL, (math.asulong(x.z) & 9223372036854775807UL) > 9218868437227405312UL);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000A030 File Offset: 0x00008230
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 isnan(double4 x)
		{
			return math.bool4((math.asulong(x.x) & 9223372036854775807UL) > 9218868437227405312UL, (math.asulong(x.y) & 9223372036854775807UL) > 9218868437227405312UL, (math.asulong(x.z) & 9223372036854775807UL) > 9218868437227405312UL, (math.asulong(x.w) & 9223372036854775807UL) > 9218868437227405312UL);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000A0C2 File Offset: 0x000082C2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool ispow2(int x)
		{
			return x > 0 && (x & x - 1) == 0;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000A0D2 File Offset: 0x000082D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 ispow2(int2 x)
		{
			return new bool2(math.ispow2(x.x), math.ispow2(x.y));
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000A0EF File Offset: 0x000082EF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 ispow2(int3 x)
		{
			return new bool3(math.ispow2(x.x), math.ispow2(x.y), math.ispow2(x.z));
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000A117 File Offset: 0x00008317
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 ispow2(int4 x)
		{
			return new bool4(math.ispow2(x.x), math.ispow2(x.y), math.ispow2(x.z), math.ispow2(x.w));
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000A14A File Offset: 0x0000834A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool ispow2(uint x)
		{
			return x > 0U && (x & x - 1U) == 0U;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000A15A File Offset: 0x0000835A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 ispow2(uint2 x)
		{
			return new bool2(math.ispow2(x.x), math.ispow2(x.y));
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000A177 File Offset: 0x00008377
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 ispow2(uint3 x)
		{
			return new bool3(math.ispow2(x.x), math.ispow2(x.y), math.ispow2(x.z));
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000A19F File Offset: 0x0000839F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 ispow2(uint4 x)
		{
			return new bool4(math.ispow2(x.x), math.ispow2(x.y), math.ispow2(x.z), math.ispow2(x.w));
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000A1D2 File Offset: 0x000083D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int min(int x, int y)
		{
			if (x >= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000A1DB File Offset: 0x000083DB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 min(int2 x, int2 y)
		{
			return new int2(math.min(x.x, y.x), math.min(x.y, y.y));
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000A204 File Offset: 0x00008404
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 min(int3 x, int3 y)
		{
			return new int3(math.min(x.x, y.x), math.min(x.y, y.y), math.min(x.z, y.z));
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000A240 File Offset: 0x00008440
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 min(int4 x, int4 y)
		{
			return new int4(math.min(x.x, y.x), math.min(x.y, y.y), math.min(x.z, y.z), math.min(x.w, y.w));
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000A296 File Offset: 0x00008496
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint min(uint x, uint y)
		{
			if (x >= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000A29F File Offset: 0x0000849F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 min(uint2 x, uint2 y)
		{
			return new uint2(math.min(x.x, y.x), math.min(x.y, y.y));
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000A2C8 File Offset: 0x000084C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 min(uint3 x, uint3 y)
		{
			return new uint3(math.min(x.x, y.x), math.min(x.y, y.y), math.min(x.z, y.z));
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000A304 File Offset: 0x00008504
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 min(uint4 x, uint4 y)
		{
			return new uint4(math.min(x.x, y.x), math.min(x.y, y.y), math.min(x.z, y.z), math.min(x.w, y.w));
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000A35A File Offset: 0x0000855A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long min(long x, long y)
		{
			if (x >= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000A363 File Offset: 0x00008563
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong min(ulong x, ulong y)
		{
			if (x >= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000A36C File Offset: 0x0000856C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float min(float x, float y)
		{
			if (!float.IsNaN(y) && x >= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000A37D File Offset: 0x0000857D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 min(float2 x, float2 y)
		{
			return new float2(math.min(x.x, y.x), math.min(x.y, y.y));
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000A3A6 File Offset: 0x000085A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 min(float3 x, float3 y)
		{
			return new float3(math.min(x.x, y.x), math.min(x.y, y.y), math.min(x.z, y.z));
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000A3E0 File Offset: 0x000085E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 min(float4 x, float4 y)
		{
			return new float4(math.min(x.x, y.x), math.min(x.y, y.y), math.min(x.z, y.z), math.min(x.w, y.w));
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000A436 File Offset: 0x00008636
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double min(double x, double y)
		{
			if (!double.IsNaN(y) && x >= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000A447 File Offset: 0x00008647
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 min(double2 x, double2 y)
		{
			return new double2(math.min(x.x, y.x), math.min(x.y, y.y));
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000A470 File Offset: 0x00008670
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 min(double3 x, double3 y)
		{
			return new double3(math.min(x.x, y.x), math.min(x.y, y.y), math.min(x.z, y.z));
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000A4AC File Offset: 0x000086AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 min(double4 x, double4 y)
		{
			return new double4(math.min(x.x, y.x), math.min(x.y, y.y), math.min(x.z, y.z), math.min(x.w, y.w));
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000A502 File Offset: 0x00008702
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int max(int x, int y)
		{
			if (x <= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000A50B File Offset: 0x0000870B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 max(int2 x, int2 y)
		{
			return new int2(math.max(x.x, y.x), math.max(x.y, y.y));
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000A534 File Offset: 0x00008734
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 max(int3 x, int3 y)
		{
			return new int3(math.max(x.x, y.x), math.max(x.y, y.y), math.max(x.z, y.z));
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000A570 File Offset: 0x00008770
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 max(int4 x, int4 y)
		{
			return new int4(math.max(x.x, y.x), math.max(x.y, y.y), math.max(x.z, y.z), math.max(x.w, y.w));
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000A5C6 File Offset: 0x000087C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint max(uint x, uint y)
		{
			if (x <= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000A5CF File Offset: 0x000087CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 max(uint2 x, uint2 y)
		{
			return new uint2(math.max(x.x, y.x), math.max(x.y, y.y));
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000A5F8 File Offset: 0x000087F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 max(uint3 x, uint3 y)
		{
			return new uint3(math.max(x.x, y.x), math.max(x.y, y.y), math.max(x.z, y.z));
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000A634 File Offset: 0x00008834
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 max(uint4 x, uint4 y)
		{
			return new uint4(math.max(x.x, y.x), math.max(x.y, y.y), math.max(x.z, y.z), math.max(x.w, y.w));
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000A68A File Offset: 0x0000888A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long max(long x, long y)
		{
			if (x <= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000A693 File Offset: 0x00008893
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong max(ulong x, ulong y)
		{
			if (x <= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000A69C File Offset: 0x0000889C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float max(float x, float y)
		{
			if (!float.IsNaN(y) && x <= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000A6AD File Offset: 0x000088AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 max(float2 x, float2 y)
		{
			return new float2(math.max(x.x, y.x), math.max(x.y, y.y));
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000A6D6 File Offset: 0x000088D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 max(float3 x, float3 y)
		{
			return new float3(math.max(x.x, y.x), math.max(x.y, y.y), math.max(x.z, y.z));
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000A710 File Offset: 0x00008910
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 max(float4 x, float4 y)
		{
			return new float4(math.max(x.x, y.x), math.max(x.y, y.y), math.max(x.z, y.z), math.max(x.w, y.w));
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000A766 File Offset: 0x00008966
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double max(double x, double y)
		{
			if (!double.IsNaN(y) && x <= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000A777 File Offset: 0x00008977
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 max(double2 x, double2 y)
		{
			return new double2(math.max(x.x, y.x), math.max(x.y, y.y));
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000A7A0 File Offset: 0x000089A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 max(double3 x, double3 y)
		{
			return new double3(math.max(x.x, y.x), math.max(x.y, y.y), math.max(x.z, y.z));
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000A7DC File Offset: 0x000089DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 max(double4 x, double4 y)
		{
			return new double4(math.max(x.x, y.x), math.max(x.y, y.y), math.max(x.z, y.z), math.max(x.w, y.w));
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000A832 File Offset: 0x00008A32
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float lerp(float x, float y, float s)
		{
			return x + s * (y - x);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000A83B File Offset: 0x00008A3B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 lerp(float2 x, float2 y, float s)
		{
			return x + s * (y - x);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000A850 File Offset: 0x00008A50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 lerp(float3 x, float3 y, float s)
		{
			return x + s * (y - x);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000A865 File Offset: 0x00008A65
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 lerp(float4 x, float4 y, float s)
		{
			return x + s * (y - x);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000A87A File Offset: 0x00008A7A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 lerp(float2 x, float2 y, float2 s)
		{
			return x + s * (y - x);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000A88F File Offset: 0x00008A8F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 lerp(float3 x, float3 y, float3 s)
		{
			return x + s * (y - x);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000A8A4 File Offset: 0x00008AA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 lerp(float4 x, float4 y, float4 s)
		{
			return x + s * (y - x);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000A8B9 File Offset: 0x00008AB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double lerp(double x, double y, double s)
		{
			return x + s * (y - x);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000A8C2 File Offset: 0x00008AC2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 lerp(double2 x, double2 y, double s)
		{
			return x + s * (y - x);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000A8D7 File Offset: 0x00008AD7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 lerp(double3 x, double3 y, double s)
		{
			return x + s * (y - x);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000A8EC File Offset: 0x00008AEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 lerp(double4 x, double4 y, double s)
		{
			return x + s * (y - x);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000A901 File Offset: 0x00008B01
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 lerp(double2 x, double2 y, double2 s)
		{
			return x + s * (y - x);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000A916 File Offset: 0x00008B16
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 lerp(double3 x, double3 y, double3 s)
		{
			return x + s * (y - x);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000A92B File Offset: 0x00008B2B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 lerp(double4 x, double4 y, double4 s)
		{
			return x + s * (y - x);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000A940 File Offset: 0x00008B40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float unlerp(float a, float b, float x)
		{
			return (x - a) / (b - a);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000A949 File Offset: 0x00008B49
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 unlerp(float2 a, float2 b, float2 x)
		{
			return (x - a) / (b - a);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000A95E File Offset: 0x00008B5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 unlerp(float3 a, float3 b, float3 x)
		{
			return (x - a) / (b - a);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000A973 File Offset: 0x00008B73
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 unlerp(float4 a, float4 b, float4 x)
		{
			return (x - a) / (b - a);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000A988 File Offset: 0x00008B88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double unlerp(double a, double b, double x)
		{
			return (x - a) / (b - a);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000A991 File Offset: 0x00008B91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 unlerp(double2 a, double2 b, double2 x)
		{
			return (x - a) / (b - a);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000A9A6 File Offset: 0x00008BA6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 unlerp(double3 a, double3 b, double3 x)
		{
			return (x - a) / (b - a);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000A9BB File Offset: 0x00008BBB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 unlerp(double4 a, double4 b, double4 x)
		{
			return (x - a) / (b - a);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000A9D0 File Offset: 0x00008BD0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float remap(float a, float b, float c, float d, float x)
		{
			return math.lerp(c, d, math.unlerp(a, b, x));
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000A9E2 File Offset: 0x00008BE2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 remap(float2 a, float2 b, float2 c, float2 d, float2 x)
		{
			return math.lerp(c, d, math.unlerp(a, b, x));
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000A9F4 File Offset: 0x00008BF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 remap(float3 a, float3 b, float3 c, float3 d, float3 x)
		{
			return math.lerp(c, d, math.unlerp(a, b, x));
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000AA06 File Offset: 0x00008C06
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 remap(float4 a, float4 b, float4 c, float4 d, float4 x)
		{
			return math.lerp(c, d, math.unlerp(a, b, x));
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000AA18 File Offset: 0x00008C18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double remap(double a, double b, double c, double d, double x)
		{
			return math.lerp(c, d, math.unlerp(a, b, x));
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000AA2A File Offset: 0x00008C2A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 remap(double2 a, double2 b, double2 c, double2 d, double2 x)
		{
			return math.lerp(c, d, math.unlerp(a, b, x));
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000AA3C File Offset: 0x00008C3C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 remap(double3 a, double3 b, double3 c, double3 d, double3 x)
		{
			return math.lerp(c, d, math.unlerp(a, b, x));
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000AA4E File Offset: 0x00008C4E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 remap(double4 a, double4 b, double4 c, double4 d, double4 x)
		{
			return math.lerp(c, d, math.unlerp(a, b, x));
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000AA60 File Offset: 0x00008C60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int mad(int a, int b, int c)
		{
			return a * b + c;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000AA67 File Offset: 0x00008C67
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 mad(int2 a, int2 b, int2 c)
		{
			return a * b + c;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000AA76 File Offset: 0x00008C76
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 mad(int3 a, int3 b, int3 c)
		{
			return a * b + c;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000AA85 File Offset: 0x00008C85
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 mad(int4 a, int4 b, int4 c)
		{
			return a * b + c;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000AA94 File Offset: 0x00008C94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint mad(uint a, uint b, uint c)
		{
			return a * b + c;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000AA9B File Offset: 0x00008C9B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 mad(uint2 a, uint2 b, uint2 c)
		{
			return a * b + c;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000AAAA File Offset: 0x00008CAA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 mad(uint3 a, uint3 b, uint3 c)
		{
			return a * b + c;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000AAB9 File Offset: 0x00008CB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 mad(uint4 a, uint4 b, uint4 c)
		{
			return a * b + c;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000AAC8 File Offset: 0x00008CC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long mad(long a, long b, long c)
		{
			return a * b + c;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000AACF File Offset: 0x00008CCF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong mad(ulong a, ulong b, ulong c)
		{
			return a * b + c;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000AAD6 File Offset: 0x00008CD6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float mad(float a, float b, float c)
		{
			return a * b + c;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000AADD File Offset: 0x00008CDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 mad(float2 a, float2 b, float2 c)
		{
			return a * b + c;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000AAEC File Offset: 0x00008CEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 mad(float3 a, float3 b, float3 c)
		{
			return a * b + c;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000AAFB File Offset: 0x00008CFB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 mad(float4 a, float4 b, float4 c)
		{
			return a * b + c;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000AB0A File Offset: 0x00008D0A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double mad(double a, double b, double c)
		{
			return a * b + c;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000AB11 File Offset: 0x00008D11
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 mad(double2 a, double2 b, double2 c)
		{
			return a * b + c;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000AB20 File Offset: 0x00008D20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 mad(double3 a, double3 b, double3 c)
		{
			return a * b + c;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000AB2F File Offset: 0x00008D2F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 mad(double4 a, double4 b, double4 c)
		{
			return a * b + c;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000AB3E File Offset: 0x00008D3E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int clamp(int x, int a, int b)
		{
			return math.max(a, math.min(b, x));
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000AB4D File Offset: 0x00008D4D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 clamp(int2 x, int2 a, int2 b)
		{
			return math.max(a, math.min(b, x));
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000AB5C File Offset: 0x00008D5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 clamp(int3 x, int3 a, int3 b)
		{
			return math.max(a, math.min(b, x));
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000AB6B File Offset: 0x00008D6B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 clamp(int4 x, int4 a, int4 b)
		{
			return math.max(a, math.min(b, x));
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000AB7A File Offset: 0x00008D7A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint clamp(uint x, uint a, uint b)
		{
			return math.max(a, math.min(b, x));
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000AB89 File Offset: 0x00008D89
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 clamp(uint2 x, uint2 a, uint2 b)
		{
			return math.max(a, math.min(b, x));
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000AB98 File Offset: 0x00008D98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 clamp(uint3 x, uint3 a, uint3 b)
		{
			return math.max(a, math.min(b, x));
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000ABA7 File Offset: 0x00008DA7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 clamp(uint4 x, uint4 a, uint4 b)
		{
			return math.max(a, math.min(b, x));
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000ABB6 File Offset: 0x00008DB6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long clamp(long x, long a, long b)
		{
			return math.max(a, math.min(b, x));
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000ABC5 File Offset: 0x00008DC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong clamp(ulong x, ulong a, ulong b)
		{
			return math.max(a, math.min(b, x));
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000ABD4 File Offset: 0x00008DD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float clamp(float x, float a, float b)
		{
			return math.max(a, math.min(b, x));
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000ABE3 File Offset: 0x00008DE3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 clamp(float2 x, float2 a, float2 b)
		{
			return math.max(a, math.min(b, x));
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000ABF2 File Offset: 0x00008DF2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 clamp(float3 x, float3 a, float3 b)
		{
			return math.max(a, math.min(b, x));
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000AC01 File Offset: 0x00008E01
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 clamp(float4 x, float4 a, float4 b)
		{
			return math.max(a, math.min(b, x));
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000AC10 File Offset: 0x00008E10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double clamp(double x, double a, double b)
		{
			return math.max(a, math.min(b, x));
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000AC1F File Offset: 0x00008E1F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 clamp(double2 x, double2 a, double2 b)
		{
			return math.max(a, math.min(b, x));
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000AC2E File Offset: 0x00008E2E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 clamp(double3 x, double3 a, double3 b)
		{
			return math.max(a, math.min(b, x));
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000AC3D File Offset: 0x00008E3D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 clamp(double4 x, double4 a, double4 b)
		{
			return math.max(a, math.min(b, x));
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000AC4C File Offset: 0x00008E4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float saturate(float x)
		{
			return math.clamp(x, 0f, 1f);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000AC5E File Offset: 0x00008E5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 saturate(float2 x)
		{
			return math.clamp(x, new float2(0f), new float2(1f));
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000AC7A File Offset: 0x00008E7A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 saturate(float3 x)
		{
			return math.clamp(x, new float3(0f), new float3(1f));
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000AC96 File Offset: 0x00008E96
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 saturate(float4 x)
		{
			return math.clamp(x, new float4(0f), new float4(1f));
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000ACB2 File Offset: 0x00008EB2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double saturate(double x)
		{
			return math.clamp(x, 0.0, 1.0);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000ACCC File Offset: 0x00008ECC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 saturate(double2 x)
		{
			return math.clamp(x, new double2(0.0), new double2(1.0));
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000ACF0 File Offset: 0x00008EF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 saturate(double3 x)
		{
			return math.clamp(x, new double3(0.0), new double3(1.0));
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000AD14 File Offset: 0x00008F14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 saturate(double4 x)
		{
			return math.clamp(x, new double4(0.0), new double4(1.0));
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000AD38 File Offset: 0x00008F38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int abs(int x)
		{
			return math.max(-x, x);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000AD42 File Offset: 0x00008F42
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 abs(int2 x)
		{
			return math.max(-x, x);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000AD50 File Offset: 0x00008F50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 abs(int3 x)
		{
			return math.max(-x, x);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000AD5E File Offset: 0x00008F5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 abs(int4 x)
		{
			return math.max(-x, x);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000AD6C File Offset: 0x00008F6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long abs(long x)
		{
			return math.max(-x, x);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000AD76 File Offset: 0x00008F76
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float abs(float x)
		{
			return math.asfloat(math.asuint(x) & 2147483647U);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000AD89 File Offset: 0x00008F89
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 abs(float2 x)
		{
			return math.asfloat(math.asuint(x) & 2147483647U);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000ADA0 File Offset: 0x00008FA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 abs(float3 x)
		{
			return math.asfloat(math.asuint(x) & 2147483647U);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000ADB7 File Offset: 0x00008FB7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 abs(float4 x)
		{
			return math.asfloat(math.asuint(x) & 2147483647U);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000ADCE File Offset: 0x00008FCE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double abs(double x)
		{
			return math.asdouble(math.asulong(x) & 9223372036854775807UL);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000ADE5 File Offset: 0x00008FE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 abs(double2 x)
		{
			return math.double2(math.asdouble(math.asulong(x.x) & 9223372036854775807UL), math.asdouble(math.asulong(x.y) & 9223372036854775807UL));
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000AE20 File Offset: 0x00009020
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 abs(double3 x)
		{
			return math.double3(math.asdouble(math.asulong(x.x) & 9223372036854775807UL), math.asdouble(math.asulong(x.y) & 9223372036854775807UL), math.asdouble(math.asulong(x.z) & 9223372036854775807UL));
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000AE80 File Offset: 0x00009080
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 abs(double4 x)
		{
			return math.double4(math.asdouble(math.asulong(x.x) & 9223372036854775807UL), math.asdouble(math.asulong(x.y) & 9223372036854775807UL), math.asdouble(math.asulong(x.z) & 9223372036854775807UL), math.asdouble(math.asulong(x.w) & 9223372036854775807UL));
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000AEFA File Offset: 0x000090FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int dot(int x, int y)
		{
			return x * y;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000AEFF File Offset: 0x000090FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int dot(int2 x, int2 y)
		{
			return x.x * y.x + x.y * y.y;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000AF1C File Offset: 0x0000911C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int dot(int3 x, int3 y)
		{
			return x.x * y.x + x.y * y.y + x.z * y.z;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000AF47 File Offset: 0x00009147
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int dot(int4 x, int4 y)
		{
			return x.x * y.x + x.y * y.y + x.z * y.z + x.w * y.w;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000AF80 File Offset: 0x00009180
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint dot(uint x, uint y)
		{
			return x * y;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000AF85 File Offset: 0x00009185
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint dot(uint2 x, uint2 y)
		{
			return x.x * y.x + x.y * y.y;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000AFA2 File Offset: 0x000091A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint dot(uint3 x, uint3 y)
		{
			return x.x * y.x + x.y * y.y + x.z * y.z;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000AFCD File Offset: 0x000091CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint dot(uint4 x, uint4 y)
		{
			return x.x * y.x + x.y * y.y + x.z * y.z + x.w * y.w;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000B006 File Offset: 0x00009206
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float dot(float x, float y)
		{
			return x * y;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000B00B File Offset: 0x0000920B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float dot(float2 x, float2 y)
		{
			return x.x * y.x + x.y * y.y;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000B028 File Offset: 0x00009228
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float dot(float3 x, float3 y)
		{
			return x.x * y.x + x.y * y.y + x.z * y.z;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000B053 File Offset: 0x00009253
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float dot(float4 x, float4 y)
		{
			return x.x * y.x + x.y * y.y + x.z * y.z + x.w * y.w;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000B08C File Offset: 0x0000928C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double dot(double x, double y)
		{
			return x * y;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000B091 File Offset: 0x00009291
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double dot(double2 x, double2 y)
		{
			return x.x * y.x + x.y * y.y;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000B0AE File Offset: 0x000092AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double dot(double3 x, double3 y)
		{
			return x.x * y.x + x.y * y.y + x.z * y.z;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000B0D9 File Offset: 0x000092D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double dot(double4 x, double4 y)
		{
			return x.x * y.x + x.y * y.y + x.z * y.z + x.w * y.w;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000B112 File Offset: 0x00009312
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float tan(float x)
		{
			return (float)Math.Tan((double)x);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000B11C File Offset: 0x0000931C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 tan(float2 x)
		{
			return new float2(math.tan(x.x), math.tan(x.y));
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000B139 File Offset: 0x00009339
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 tan(float3 x)
		{
			return new float3(math.tan(x.x), math.tan(x.y), math.tan(x.z));
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000B161 File Offset: 0x00009361
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 tan(float4 x)
		{
			return new float4(math.tan(x.x), math.tan(x.y), math.tan(x.z), math.tan(x.w));
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000B194 File Offset: 0x00009394
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double tan(double x)
		{
			return Math.Tan(x);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000B19C File Offset: 0x0000939C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 tan(double2 x)
		{
			return new double2(math.tan(x.x), math.tan(x.y));
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000B1B9 File Offset: 0x000093B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 tan(double3 x)
		{
			return new double3(math.tan(x.x), math.tan(x.y), math.tan(x.z));
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000B1E1 File Offset: 0x000093E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 tan(double4 x)
		{
			return new double4(math.tan(x.x), math.tan(x.y), math.tan(x.z), math.tan(x.w));
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000B214 File Offset: 0x00009414
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float tanh(float x)
		{
			return (float)Math.Tanh((double)x);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000B21E File Offset: 0x0000941E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 tanh(float2 x)
		{
			return new float2(math.tanh(x.x), math.tanh(x.y));
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000B23B File Offset: 0x0000943B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 tanh(float3 x)
		{
			return new float3(math.tanh(x.x), math.tanh(x.y), math.tanh(x.z));
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000B263 File Offset: 0x00009463
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 tanh(float4 x)
		{
			return new float4(math.tanh(x.x), math.tanh(x.y), math.tanh(x.z), math.tanh(x.w));
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000B296 File Offset: 0x00009496
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double tanh(double x)
		{
			return Math.Tanh(x);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000B29E File Offset: 0x0000949E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 tanh(double2 x)
		{
			return new double2(math.tanh(x.x), math.tanh(x.y));
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000B2BB File Offset: 0x000094BB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 tanh(double3 x)
		{
			return new double3(math.tanh(x.x), math.tanh(x.y), math.tanh(x.z));
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000B2E3 File Offset: 0x000094E3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 tanh(double4 x)
		{
			return new double4(math.tanh(x.x), math.tanh(x.y), math.tanh(x.z), math.tanh(x.w));
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000B316 File Offset: 0x00009516
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float atan(float x)
		{
			return (float)Math.Atan((double)x);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000B320 File Offset: 0x00009520
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 atan(float2 x)
		{
			return new float2(math.atan(x.x), math.atan(x.y));
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000B33D File Offset: 0x0000953D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 atan(float3 x)
		{
			return new float3(math.atan(x.x), math.atan(x.y), math.atan(x.z));
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000B365 File Offset: 0x00009565
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 atan(float4 x)
		{
			return new float4(math.atan(x.x), math.atan(x.y), math.atan(x.z), math.atan(x.w));
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000B398 File Offset: 0x00009598
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double atan(double x)
		{
			return Math.Atan(x);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000B3A0 File Offset: 0x000095A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 atan(double2 x)
		{
			return new double2(math.atan(x.x), math.atan(x.y));
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000B3BD File Offset: 0x000095BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 atan(double3 x)
		{
			return new double3(math.atan(x.x), math.atan(x.y), math.atan(x.z));
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000B3E5 File Offset: 0x000095E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 atan(double4 x)
		{
			return new double4(math.atan(x.x), math.atan(x.y), math.atan(x.z), math.atan(x.w));
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000B418 File Offset: 0x00009618
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float atan2(float y, float x)
		{
			return (float)Math.Atan2((double)y, (double)x);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000B424 File Offset: 0x00009624
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 atan2(float2 y, float2 x)
		{
			return new float2(math.atan2(y.x, x.x), math.atan2(y.y, x.y));
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000B44D File Offset: 0x0000964D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 atan2(float3 y, float3 x)
		{
			return new float3(math.atan2(y.x, x.x), math.atan2(y.y, x.y), math.atan2(y.z, x.z));
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000B488 File Offset: 0x00009688
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 atan2(float4 y, float4 x)
		{
			return new float4(math.atan2(y.x, x.x), math.atan2(y.y, x.y), math.atan2(y.z, x.z), math.atan2(y.w, x.w));
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000B4DE File Offset: 0x000096DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double atan2(double y, double x)
		{
			return Math.Atan2(y, x);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000B4E7 File Offset: 0x000096E7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 atan2(double2 y, double2 x)
		{
			return new double2(math.atan2(y.x, x.x), math.atan2(y.y, x.y));
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000B510 File Offset: 0x00009710
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 atan2(double3 y, double3 x)
		{
			return new double3(math.atan2(y.x, x.x), math.atan2(y.y, x.y), math.atan2(y.z, x.z));
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000B54C File Offset: 0x0000974C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 atan2(double4 y, double4 x)
		{
			return new double4(math.atan2(y.x, x.x), math.atan2(y.y, x.y), math.atan2(y.z, x.z), math.atan2(y.w, x.w));
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000B5A2 File Offset: 0x000097A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float cos(float x)
		{
			return (float)Math.Cos((double)x);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000B5AC File Offset: 0x000097AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 cos(float2 x)
		{
			return new float2(math.cos(x.x), math.cos(x.y));
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000B5C9 File Offset: 0x000097C9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 cos(float3 x)
		{
			return new float3(math.cos(x.x), math.cos(x.y), math.cos(x.z));
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000B5F1 File Offset: 0x000097F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 cos(float4 x)
		{
			return new float4(math.cos(x.x), math.cos(x.y), math.cos(x.z), math.cos(x.w));
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000B624 File Offset: 0x00009824
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double cos(double x)
		{
			return Math.Cos(x);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000B62C File Offset: 0x0000982C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 cos(double2 x)
		{
			return new double2(math.cos(x.x), math.cos(x.y));
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000B649 File Offset: 0x00009849
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 cos(double3 x)
		{
			return new double3(math.cos(x.x), math.cos(x.y), math.cos(x.z));
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000B671 File Offset: 0x00009871
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 cos(double4 x)
		{
			return new double4(math.cos(x.x), math.cos(x.y), math.cos(x.z), math.cos(x.w));
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000B6A4 File Offset: 0x000098A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float cosh(float x)
		{
			return (float)Math.Cosh((double)x);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000B6AE File Offset: 0x000098AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 cosh(float2 x)
		{
			return new float2(math.cosh(x.x), math.cosh(x.y));
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000B6CB File Offset: 0x000098CB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 cosh(float3 x)
		{
			return new float3(math.cosh(x.x), math.cosh(x.y), math.cosh(x.z));
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000B6F3 File Offset: 0x000098F3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 cosh(float4 x)
		{
			return new float4(math.cosh(x.x), math.cosh(x.y), math.cosh(x.z), math.cosh(x.w));
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000B726 File Offset: 0x00009926
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double cosh(double x)
		{
			return Math.Cosh(x);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000B72E File Offset: 0x0000992E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 cosh(double2 x)
		{
			return new double2(math.cosh(x.x), math.cosh(x.y));
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000B74B File Offset: 0x0000994B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 cosh(double3 x)
		{
			return new double3(math.cosh(x.x), math.cosh(x.y), math.cosh(x.z));
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000B773 File Offset: 0x00009973
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 cosh(double4 x)
		{
			return new double4(math.cosh(x.x), math.cosh(x.y), math.cosh(x.z), math.cosh(x.w));
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000B7A6 File Offset: 0x000099A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float acos(float x)
		{
			return (float)Math.Acos((double)x);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000B7B1 File Offset: 0x000099B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 acos(float2 x)
		{
			return new float2(math.acos(x.x), math.acos(x.y));
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000B7CE File Offset: 0x000099CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 acos(float3 x)
		{
			return new float3(math.acos(x.x), math.acos(x.y), math.acos(x.z));
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000B7F6 File Offset: 0x000099F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 acos(float4 x)
		{
			return new float4(math.acos(x.x), math.acos(x.y), math.acos(x.z), math.acos(x.w));
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000B829 File Offset: 0x00009A29
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double acos(double x)
		{
			return Math.Acos(x);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000B831 File Offset: 0x00009A31
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 acos(double2 x)
		{
			return new double2(math.acos(x.x), math.acos(x.y));
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000B84E File Offset: 0x00009A4E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 acos(double3 x)
		{
			return new double3(math.acos(x.x), math.acos(x.y), math.acos(x.z));
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000B876 File Offset: 0x00009A76
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 acos(double4 x)
		{
			return new double4(math.acos(x.x), math.acos(x.y), math.acos(x.z), math.acos(x.w));
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000B8A9 File Offset: 0x00009AA9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float sin(float x)
		{
			return (float)Math.Sin((double)x);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000B8B4 File Offset: 0x00009AB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 sin(float2 x)
		{
			return new float2(math.sin(x.x), math.sin(x.y));
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000B8D1 File Offset: 0x00009AD1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 sin(float3 x)
		{
			return new float3(math.sin(x.x), math.sin(x.y), math.sin(x.z));
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000B8F9 File Offset: 0x00009AF9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 sin(float4 x)
		{
			return new float4(math.sin(x.x), math.sin(x.y), math.sin(x.z), math.sin(x.w));
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000B92C File Offset: 0x00009B2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double sin(double x)
		{
			return Math.Sin(x);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000B934 File Offset: 0x00009B34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 sin(double2 x)
		{
			return new double2(math.sin(x.x), math.sin(x.y));
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000B951 File Offset: 0x00009B51
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 sin(double3 x)
		{
			return new double3(math.sin(x.x), math.sin(x.y), math.sin(x.z));
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000B979 File Offset: 0x00009B79
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 sin(double4 x)
		{
			return new double4(math.sin(x.x), math.sin(x.y), math.sin(x.z), math.sin(x.w));
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000B9AC File Offset: 0x00009BAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float sinh(float x)
		{
			return (float)Math.Sinh((double)x);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000B9B7 File Offset: 0x00009BB7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 sinh(float2 x)
		{
			return new float2(math.sinh(x.x), math.sinh(x.y));
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000B9D4 File Offset: 0x00009BD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 sinh(float3 x)
		{
			return new float3(math.sinh(x.x), math.sinh(x.y), math.sinh(x.z));
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000B9FC File Offset: 0x00009BFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 sinh(float4 x)
		{
			return new float4(math.sinh(x.x), math.sinh(x.y), math.sinh(x.z), math.sinh(x.w));
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000BA2F File Offset: 0x00009C2F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double sinh(double x)
		{
			return Math.Sinh(x);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000BA37 File Offset: 0x00009C37
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 sinh(double2 x)
		{
			return new double2(math.sinh(x.x), math.sinh(x.y));
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000BA54 File Offset: 0x00009C54
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 sinh(double3 x)
		{
			return new double3(math.sinh(x.x), math.sinh(x.y), math.sinh(x.z));
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000BA7C File Offset: 0x00009C7C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 sinh(double4 x)
		{
			return new double4(math.sinh(x.x), math.sinh(x.y), math.sinh(x.z), math.sinh(x.w));
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000BAAF File Offset: 0x00009CAF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float asin(float x)
		{
			return (float)Math.Asin((double)x);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000BABA File Offset: 0x00009CBA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 asin(float2 x)
		{
			return new float2(math.asin(x.x), math.asin(x.y));
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000BAD7 File Offset: 0x00009CD7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 asin(float3 x)
		{
			return new float3(math.asin(x.x), math.asin(x.y), math.asin(x.z));
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000BAFF File Offset: 0x00009CFF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 asin(float4 x)
		{
			return new float4(math.asin(x.x), math.asin(x.y), math.asin(x.z), math.asin(x.w));
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000BB32 File Offset: 0x00009D32
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double asin(double x)
		{
			return Math.Asin(x);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000BB3A File Offset: 0x00009D3A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 asin(double2 x)
		{
			return new double2(math.asin(x.x), math.asin(x.y));
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000BB57 File Offset: 0x00009D57
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 asin(double3 x)
		{
			return new double3(math.asin(x.x), math.asin(x.y), math.asin(x.z));
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000BB7F File Offset: 0x00009D7F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 asin(double4 x)
		{
			return new double4(math.asin(x.x), math.asin(x.y), math.asin(x.z), math.asin(x.w));
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000BBB2 File Offset: 0x00009DB2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float floor(float x)
		{
			return (float)Math.Floor((double)x);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000BBBD File Offset: 0x00009DBD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 floor(float2 x)
		{
			return new float2(math.floor(x.x), math.floor(x.y));
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000BBDA File Offset: 0x00009DDA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 floor(float3 x)
		{
			return new float3(math.floor(x.x), math.floor(x.y), math.floor(x.z));
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000BC02 File Offset: 0x00009E02
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 floor(float4 x)
		{
			return new float4(math.floor(x.x), math.floor(x.y), math.floor(x.z), math.floor(x.w));
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000BC35 File Offset: 0x00009E35
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double floor(double x)
		{
			return Math.Floor(x);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000BC3D File Offset: 0x00009E3D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 floor(double2 x)
		{
			return new double2(math.floor(x.x), math.floor(x.y));
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000BC5A File Offset: 0x00009E5A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 floor(double3 x)
		{
			return new double3(math.floor(x.x), math.floor(x.y), math.floor(x.z));
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000BC82 File Offset: 0x00009E82
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 floor(double4 x)
		{
			return new double4(math.floor(x.x), math.floor(x.y), math.floor(x.z), math.floor(x.w));
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000BCB5 File Offset: 0x00009EB5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float ceil(float x)
		{
			return (float)Math.Ceiling((double)x);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000BCC0 File Offset: 0x00009EC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 ceil(float2 x)
		{
			return new float2(math.ceil(x.x), math.ceil(x.y));
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000BCDD File Offset: 0x00009EDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 ceil(float3 x)
		{
			return new float3(math.ceil(x.x), math.ceil(x.y), math.ceil(x.z));
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000BD05 File Offset: 0x00009F05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 ceil(float4 x)
		{
			return new float4(math.ceil(x.x), math.ceil(x.y), math.ceil(x.z), math.ceil(x.w));
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000BD38 File Offset: 0x00009F38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double ceil(double x)
		{
			return Math.Ceiling(x);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000BD40 File Offset: 0x00009F40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 ceil(double2 x)
		{
			return new double2(math.ceil(x.x), math.ceil(x.y));
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000BD5D File Offset: 0x00009F5D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 ceil(double3 x)
		{
			return new double3(math.ceil(x.x), math.ceil(x.y), math.ceil(x.z));
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000BD85 File Offset: 0x00009F85
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 ceil(double4 x)
		{
			return new double4(math.ceil(x.x), math.ceil(x.y), math.ceil(x.z), math.ceil(x.w));
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000BDB8 File Offset: 0x00009FB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float round(float x)
		{
			return (float)Math.Round((double)x);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000BDC3 File Offset: 0x00009FC3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 round(float2 x)
		{
			return new float2(math.round(x.x), math.round(x.y));
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000BDE0 File Offset: 0x00009FE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 round(float3 x)
		{
			return new float3(math.round(x.x), math.round(x.y), math.round(x.z));
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000BE08 File Offset: 0x0000A008
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 round(float4 x)
		{
			return new float4(math.round(x.x), math.round(x.y), math.round(x.z), math.round(x.w));
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000BE3B File Offset: 0x0000A03B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double round(double x)
		{
			return Math.Round(x);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000BE43 File Offset: 0x0000A043
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 round(double2 x)
		{
			return new double2(math.round(x.x), math.round(x.y));
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000BE60 File Offset: 0x0000A060
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 round(double3 x)
		{
			return new double3(math.round(x.x), math.round(x.y), math.round(x.z));
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000BE88 File Offset: 0x0000A088
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 round(double4 x)
		{
			return new double4(math.round(x.x), math.round(x.y), math.round(x.z), math.round(x.w));
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000BEBB File Offset: 0x0000A0BB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float trunc(float x)
		{
			return (float)Math.Truncate((double)x);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000BEC6 File Offset: 0x0000A0C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 trunc(float2 x)
		{
			return new float2(math.trunc(x.x), math.trunc(x.y));
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000BEE3 File Offset: 0x0000A0E3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 trunc(float3 x)
		{
			return new float3(math.trunc(x.x), math.trunc(x.y), math.trunc(x.z));
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000BF0B File Offset: 0x0000A10B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 trunc(float4 x)
		{
			return new float4(math.trunc(x.x), math.trunc(x.y), math.trunc(x.z), math.trunc(x.w));
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000BF3E File Offset: 0x0000A13E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double trunc(double x)
		{
			return Math.Truncate(x);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000BF46 File Offset: 0x0000A146
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 trunc(double2 x)
		{
			return new double2(math.trunc(x.x), math.trunc(x.y));
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000BF63 File Offset: 0x0000A163
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 trunc(double3 x)
		{
			return new double3(math.trunc(x.x), math.trunc(x.y), math.trunc(x.z));
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000BF8B File Offset: 0x0000A18B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 trunc(double4 x)
		{
			return new double4(math.trunc(x.x), math.trunc(x.y), math.trunc(x.z), math.trunc(x.w));
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000BFBE File Offset: 0x0000A1BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float frac(float x)
		{
			return x - math.floor(x);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000BFC8 File Offset: 0x0000A1C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 frac(float2 x)
		{
			return x - math.floor(x);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000BFD6 File Offset: 0x0000A1D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 frac(float3 x)
		{
			return x - math.floor(x);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000BFE4 File Offset: 0x0000A1E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 frac(float4 x)
		{
			return x - math.floor(x);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000BFF2 File Offset: 0x0000A1F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double frac(double x)
		{
			return x - math.floor(x);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000BFFC File Offset: 0x0000A1FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 frac(double2 x)
		{
			return x - math.floor(x);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000C00A File Offset: 0x0000A20A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 frac(double3 x)
		{
			return x - math.floor(x);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000C018 File Offset: 0x0000A218
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 frac(double4 x)
		{
			return x - math.floor(x);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000C026 File Offset: 0x0000A226
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float rcp(float x)
		{
			return 1f / x;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000C02F File Offset: 0x0000A22F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 rcp(float2 x)
		{
			return 1f / x;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000C03C File Offset: 0x0000A23C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 rcp(float3 x)
		{
			return 1f / x;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000C049 File Offset: 0x0000A249
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 rcp(float4 x)
		{
			return 1f / x;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000C056 File Offset: 0x0000A256
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double rcp(double x)
		{
			return 1.0 / x;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000C063 File Offset: 0x0000A263
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 rcp(double2 x)
		{
			return 1.0 / x;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000C074 File Offset: 0x0000A274
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 rcp(double3 x)
		{
			return 1.0 / x;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000C085 File Offset: 0x0000A285
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 rcp(double4 x)
		{
			return 1.0 / x;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000C096 File Offset: 0x0000A296
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float sign(float x)
		{
			return ((x > 0f) ? 1f : 0f) - ((x < 0f) ? 1f : 0f);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000C0C1 File Offset: 0x0000A2C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 sign(float2 x)
		{
			return new float2(math.sign(x.x), math.sign(x.y));
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000C0DE File Offset: 0x0000A2DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 sign(float3 x)
		{
			return new float3(math.sign(x.x), math.sign(x.y), math.sign(x.z));
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000C106 File Offset: 0x0000A306
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 sign(float4 x)
		{
			return new float4(math.sign(x.x), math.sign(x.y), math.sign(x.z), math.sign(x.w));
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000C13C File Offset: 0x0000A33C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double sign(double x)
		{
			if (x != 0.0)
			{
				return ((x > 0.0) ? 1.0 : 0.0) - ((x < 0.0) ? 1.0 : 0.0);
			}
			return 0.0;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000C1A0 File Offset: 0x0000A3A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 sign(double2 x)
		{
			return new double2(math.sign(x.x), math.sign(x.y));
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000C1BD File Offset: 0x0000A3BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 sign(double3 x)
		{
			return new double3(math.sign(x.x), math.sign(x.y), math.sign(x.z));
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000C1E5 File Offset: 0x0000A3E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 sign(double4 x)
		{
			return new double4(math.sign(x.x), math.sign(x.y), math.sign(x.z), math.sign(x.w));
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000C218 File Offset: 0x0000A418
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float pow(float x, float y)
		{
			return (float)Math.Pow((double)x, (double)y);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000C226 File Offset: 0x0000A426
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 pow(float2 x, float2 y)
		{
			return new float2(math.pow(x.x, y.x), math.pow(x.y, y.y));
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000C24F File Offset: 0x0000A44F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 pow(float3 x, float3 y)
		{
			return new float3(math.pow(x.x, y.x), math.pow(x.y, y.y), math.pow(x.z, y.z));
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000C28C File Offset: 0x0000A48C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 pow(float4 x, float4 y)
		{
			return new float4(math.pow(x.x, y.x), math.pow(x.y, y.y), math.pow(x.z, y.z), math.pow(x.w, y.w));
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000C2E2 File Offset: 0x0000A4E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double pow(double x, double y)
		{
			return Math.Pow(x, y);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000C2EB File Offset: 0x0000A4EB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 pow(double2 x, double2 y)
		{
			return new double2(math.pow(x.x, y.x), math.pow(x.y, y.y));
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000C314 File Offset: 0x0000A514
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 pow(double3 x, double3 y)
		{
			return new double3(math.pow(x.x, y.x), math.pow(x.y, y.y), math.pow(x.z, y.z));
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000C350 File Offset: 0x0000A550
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 pow(double4 x, double4 y)
		{
			return new double4(math.pow(x.x, y.x), math.pow(x.y, y.y), math.pow(x.z, y.z), math.pow(x.w, y.w));
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000C3A6 File Offset: 0x0000A5A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float exp(float x)
		{
			return (float)Math.Exp((double)x);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000C3B1 File Offset: 0x0000A5B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 exp(float2 x)
		{
			return new float2(math.exp(x.x), math.exp(x.y));
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000C3CE File Offset: 0x0000A5CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 exp(float3 x)
		{
			return new float3(math.exp(x.x), math.exp(x.y), math.exp(x.z));
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000C3F6 File Offset: 0x0000A5F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 exp(float4 x)
		{
			return new float4(math.exp(x.x), math.exp(x.y), math.exp(x.z), math.exp(x.w));
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000C429 File Offset: 0x0000A629
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double exp(double x)
		{
			return Math.Exp(x);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000C431 File Offset: 0x0000A631
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 exp(double2 x)
		{
			return new double2(math.exp(x.x), math.exp(x.y));
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000C44E File Offset: 0x0000A64E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 exp(double3 x)
		{
			return new double3(math.exp(x.x), math.exp(x.y), math.exp(x.z));
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000C476 File Offset: 0x0000A676
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 exp(double4 x)
		{
			return new double4(math.exp(x.x), math.exp(x.y), math.exp(x.z), math.exp(x.w));
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000C4A9 File Offset: 0x0000A6A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float exp2(float x)
		{
			return (float)Math.Exp((double)(x * 0.6931472f));
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000C4BA File Offset: 0x0000A6BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 exp2(float2 x)
		{
			return new float2(math.exp2(x.x), math.exp2(x.y));
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000C4D7 File Offset: 0x0000A6D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 exp2(float3 x)
		{
			return new float3(math.exp2(x.x), math.exp2(x.y), math.exp2(x.z));
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000C4FF File Offset: 0x0000A6FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 exp2(float4 x)
		{
			return new float4(math.exp2(x.x), math.exp2(x.y), math.exp2(x.z), math.exp2(x.w));
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000C532 File Offset: 0x0000A732
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double exp2(double x)
		{
			return Math.Exp(x * 0.6931471805599453);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000C544 File Offset: 0x0000A744
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 exp2(double2 x)
		{
			return new double2(math.exp2(x.x), math.exp2(x.y));
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000C561 File Offset: 0x0000A761
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 exp2(double3 x)
		{
			return new double3(math.exp2(x.x), math.exp2(x.y), math.exp2(x.z));
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000C589 File Offset: 0x0000A789
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 exp2(double4 x)
		{
			return new double4(math.exp2(x.x), math.exp2(x.y), math.exp2(x.z), math.exp2(x.w));
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000C5BC File Offset: 0x0000A7BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float exp10(float x)
		{
			return (float)Math.Exp((double)(x * 2.3025851f));
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000C5CD File Offset: 0x0000A7CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 exp10(float2 x)
		{
			return new float2(math.exp10(x.x), math.exp10(x.y));
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000C5EA File Offset: 0x0000A7EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 exp10(float3 x)
		{
			return new float3(math.exp10(x.x), math.exp10(x.y), math.exp10(x.z));
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000C612 File Offset: 0x0000A812
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 exp10(float4 x)
		{
			return new float4(math.exp10(x.x), math.exp10(x.y), math.exp10(x.z), math.exp10(x.w));
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000C645 File Offset: 0x0000A845
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double exp10(double x)
		{
			return Math.Exp(x * 2.302585092994046);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000C657 File Offset: 0x0000A857
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 exp10(double2 x)
		{
			return new double2(math.exp10(x.x), math.exp10(x.y));
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000C674 File Offset: 0x0000A874
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 exp10(double3 x)
		{
			return new double3(math.exp10(x.x), math.exp10(x.y), math.exp10(x.z));
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000C69C File Offset: 0x0000A89C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 exp10(double4 x)
		{
			return new double4(math.exp10(x.x), math.exp10(x.y), math.exp10(x.z), math.exp10(x.w));
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000C6CF File Offset: 0x0000A8CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float log(float x)
		{
			return (float)Math.Log((double)x);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000C6DA File Offset: 0x0000A8DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 log(float2 x)
		{
			return new float2(math.log(x.x), math.log(x.y));
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000C6F7 File Offset: 0x0000A8F7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 log(float3 x)
		{
			return new float3(math.log(x.x), math.log(x.y), math.log(x.z));
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000C71F File Offset: 0x0000A91F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 log(float4 x)
		{
			return new float4(math.log(x.x), math.log(x.y), math.log(x.z), math.log(x.w));
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000C752 File Offset: 0x0000A952
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double log(double x)
		{
			return Math.Log(x);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000C75A File Offset: 0x0000A95A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 log(double2 x)
		{
			return new double2(math.log(x.x), math.log(x.y));
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000C777 File Offset: 0x0000A977
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 log(double3 x)
		{
			return new double3(math.log(x.x), math.log(x.y), math.log(x.z));
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000C79F File Offset: 0x0000A99F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 log(double4 x)
		{
			return new double4(math.log(x.x), math.log(x.y), math.log(x.z), math.log(x.w));
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000C7D2 File Offset: 0x0000A9D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float log2(float x)
		{
			return (float)Math.Log((double)x, 2.0);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000C7E6 File Offset: 0x0000A9E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 log2(float2 x)
		{
			return new float2(math.log2(x.x), math.log2(x.y));
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000C803 File Offset: 0x0000AA03
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 log2(float3 x)
		{
			return new float3(math.log2(x.x), math.log2(x.y), math.log2(x.z));
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000C82B File Offset: 0x0000AA2B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 log2(float4 x)
		{
			return new float4(math.log2(x.x), math.log2(x.y), math.log2(x.z), math.log2(x.w));
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000C85E File Offset: 0x0000AA5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double log2(double x)
		{
			return Math.Log(x, 2.0);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000C86F File Offset: 0x0000AA6F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 log2(double2 x)
		{
			return new double2(math.log2(x.x), math.log2(x.y));
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000C88C File Offset: 0x0000AA8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 log2(double3 x)
		{
			return new double3(math.log2(x.x), math.log2(x.y), math.log2(x.z));
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000C8B4 File Offset: 0x0000AAB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 log2(double4 x)
		{
			return new double4(math.log2(x.x), math.log2(x.y), math.log2(x.z), math.log2(x.w));
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000C8E7 File Offset: 0x0000AAE7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float log10(float x)
		{
			return (float)Math.Log10((double)x);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000C8F2 File Offset: 0x0000AAF2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 log10(float2 x)
		{
			return new float2(math.log10(x.x), math.log10(x.y));
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000C90F File Offset: 0x0000AB0F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 log10(float3 x)
		{
			return new float3(math.log10(x.x), math.log10(x.y), math.log10(x.z));
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000C937 File Offset: 0x0000AB37
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 log10(float4 x)
		{
			return new float4(math.log10(x.x), math.log10(x.y), math.log10(x.z), math.log10(x.w));
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000C96A File Offset: 0x0000AB6A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double log10(double x)
		{
			return Math.Log10(x);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000C972 File Offset: 0x0000AB72
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 log10(double2 x)
		{
			return new double2(math.log10(x.x), math.log10(x.y));
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000C98F File Offset: 0x0000AB8F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 log10(double3 x)
		{
			return new double3(math.log10(x.x), math.log10(x.y), math.log10(x.z));
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000C9B7 File Offset: 0x0000ABB7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 log10(double4 x)
		{
			return new double4(math.log10(x.x), math.log10(x.y), math.log10(x.z), math.log10(x.w));
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000C9EA File Offset: 0x0000ABEA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float fmod(float x, float y)
		{
			return x % y;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000C9EF File Offset: 0x0000ABEF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 fmod(float2 x, float2 y)
		{
			return new float2(x.x % y.x, x.y % y.y);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000CA10 File Offset: 0x0000AC10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 fmod(float3 x, float3 y)
		{
			return new float3(x.x % y.x, x.y % y.y, x.z % y.z);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000CA3E File Offset: 0x0000AC3E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 fmod(float4 x, float4 y)
		{
			return new float4(x.x % y.x, x.y % y.y, x.z % y.z, x.w % y.w);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000CA79 File Offset: 0x0000AC79
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double fmod(double x, double y)
		{
			return x % y;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000CA7E File Offset: 0x0000AC7E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 fmod(double2 x, double2 y)
		{
			return new double2(x.x % y.x, x.y % y.y);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000CA9F File Offset: 0x0000AC9F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 fmod(double3 x, double3 y)
		{
			return new double3(x.x % y.x, x.y % y.y, x.z % y.z);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000CACD File Offset: 0x0000ACCD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 fmod(double4 x, double4 y)
		{
			return new double4(x.x % y.x, x.y % y.y, x.z % y.z, x.w % y.w);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000CB08 File Offset: 0x0000AD08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float modf(float x, out float i)
		{
			i = math.trunc(x);
			return x - i;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000CB16 File Offset: 0x0000AD16
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 modf(float2 x, out float2 i)
		{
			i = math.trunc(x);
			return x - i;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0000CB30 File Offset: 0x0000AD30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 modf(float3 x, out float3 i)
		{
			i = math.trunc(x);
			return x - i;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0000CB4A File Offset: 0x0000AD4A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 modf(float4 x, out float4 i)
		{
			i = math.trunc(x);
			return x - i;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0000CB64 File Offset: 0x0000AD64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double modf(double x, out double i)
		{
			i = math.trunc(x);
			return x - i;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000CB72 File Offset: 0x0000AD72
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 modf(double2 x, out double2 i)
		{
			i = math.trunc(x);
			return x - i;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000CB8C File Offset: 0x0000AD8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 modf(double3 x, out double3 i)
		{
			i = math.trunc(x);
			return x - i;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000CBA6 File Offset: 0x0000ADA6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 modf(double4 x, out double4 i)
		{
			i = math.trunc(x);
			return x - i;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000CBC0 File Offset: 0x0000ADC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float sqrt(float x)
		{
			return (float)Math.Sqrt((double)x);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000CBCB File Offset: 0x0000ADCB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 sqrt(float2 x)
		{
			return new float2(math.sqrt(x.x), math.sqrt(x.y));
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000CBE8 File Offset: 0x0000ADE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 sqrt(float3 x)
		{
			return new float3(math.sqrt(x.x), math.sqrt(x.y), math.sqrt(x.z));
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000CC10 File Offset: 0x0000AE10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 sqrt(float4 x)
		{
			return new float4(math.sqrt(x.x), math.sqrt(x.y), math.sqrt(x.z), math.sqrt(x.w));
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000CC43 File Offset: 0x0000AE43
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double sqrt(double x)
		{
			return Math.Sqrt(x);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0000CC4B File Offset: 0x0000AE4B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 sqrt(double2 x)
		{
			return new double2(math.sqrt(x.x), math.sqrt(x.y));
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000CC68 File Offset: 0x0000AE68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 sqrt(double3 x)
		{
			return new double3(math.sqrt(x.x), math.sqrt(x.y), math.sqrt(x.z));
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000CC90 File Offset: 0x0000AE90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 sqrt(double4 x)
		{
			return new double4(math.sqrt(x.x), math.sqrt(x.y), math.sqrt(x.z), math.sqrt(x.w));
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000CCC3 File Offset: 0x0000AEC3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float rsqrt(float x)
		{
			return 1f / math.sqrt(x);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000CCD1 File Offset: 0x0000AED1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 rsqrt(float2 x)
		{
			return 1f / math.sqrt(x);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0000CCE3 File Offset: 0x0000AEE3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 rsqrt(float3 x)
		{
			return 1f / math.sqrt(x);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000CCF5 File Offset: 0x0000AEF5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 rsqrt(float4 x)
		{
			return 1f / math.sqrt(x);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000CD07 File Offset: 0x0000AF07
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double rsqrt(double x)
		{
			return 1.0 / math.sqrt(x);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000CD19 File Offset: 0x0000AF19
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 rsqrt(double2 x)
		{
			return 1.0 / math.sqrt(x);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0000CD2F File Offset: 0x0000AF2F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 rsqrt(double3 x)
		{
			return 1.0 / math.sqrt(x);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000CD45 File Offset: 0x0000AF45
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 rsqrt(double4 x)
		{
			return 1.0 / math.sqrt(x);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000CD5B File Offset: 0x0000AF5B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 normalize(float2 x)
		{
			return math.rsqrt(math.dot(x, x)) * x;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0000CD6F File Offset: 0x0000AF6F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 normalize(float3 x)
		{
			return math.rsqrt(math.dot(x, x)) * x;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000CD83 File Offset: 0x0000AF83
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 normalize(float4 x)
		{
			return math.rsqrt(math.dot(x, x)) * x;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000CD97 File Offset: 0x0000AF97
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 normalize(double2 x)
		{
			return math.rsqrt(math.dot(x, x)) * x;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0000CDAB File Offset: 0x0000AFAB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 normalize(double3 x)
		{
			return math.rsqrt(math.dot(x, x)) * x;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000CDBF File Offset: 0x0000AFBF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 normalize(double4 x)
		{
			return math.rsqrt(math.dot(x, x)) * x;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000CDD4 File Offset: 0x0000AFD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 normalizesafe(float2 x, float2 defaultvalue = default(float2))
		{
			float num = math.dot(x, x);
			return math.select(defaultvalue, x * math.rsqrt(num), num > 1.1754944E-38f);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0000CE04 File Offset: 0x0000B004
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 normalizesafe(float3 x, float3 defaultvalue = default(float3))
		{
			float num = math.dot(x, x);
			return math.select(defaultvalue, x * math.rsqrt(num), num > 1.1754944E-38f);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000CE34 File Offset: 0x0000B034
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 normalizesafe(float4 x, float4 defaultvalue = default(float4))
		{
			float num = math.dot(x, x);
			return math.select(defaultvalue, x * math.rsqrt(num), num > 1.1754944E-38f);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000CE64 File Offset: 0x0000B064
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 normalizesafe(double2 x, double2 defaultvalue = default(double2))
		{
			double num = math.dot(x, x);
			return math.select(defaultvalue, x * math.rsqrt(num), num > 1.1754943508222875E-38);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000CE98 File Offset: 0x0000B098
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 normalizesafe(double3 x, double3 defaultvalue = default(double3))
		{
			double num = math.dot(x, x);
			return math.select(defaultvalue, x * math.rsqrt(num), num > 1.1754943508222875E-38);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000CECC File Offset: 0x0000B0CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 normalizesafe(double4 x, double4 defaultvalue = default(double4))
		{
			double num = math.dot(x, x);
			return math.select(defaultvalue, x * math.rsqrt(num), num > 1.1754943508222875E-38);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0000CEFF File Offset: 0x0000B0FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float length(float x)
		{
			return math.abs(x);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0000CF07 File Offset: 0x0000B107
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float length(float2 x)
		{
			return math.sqrt(math.dot(x, x));
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0000CF15 File Offset: 0x0000B115
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float length(float3 x)
		{
			return math.sqrt(math.dot(x, x));
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0000CF23 File Offset: 0x0000B123
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float length(float4 x)
		{
			return math.sqrt(math.dot(x, x));
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0000CF31 File Offset: 0x0000B131
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double length(double x)
		{
			return math.abs(x);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0000CF39 File Offset: 0x0000B139
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double length(double2 x)
		{
			return math.sqrt(math.dot(x, x));
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000CF47 File Offset: 0x0000B147
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double length(double3 x)
		{
			return math.sqrt(math.dot(x, x));
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000CF55 File Offset: 0x0000B155
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double length(double4 x)
		{
			return math.sqrt(math.dot(x, x));
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0000CF63 File Offset: 0x0000B163
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float lengthsq(float x)
		{
			return x * x;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0000CF68 File Offset: 0x0000B168
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float lengthsq(float2 x)
		{
			return math.dot(x, x);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000CF71 File Offset: 0x0000B171
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float lengthsq(float3 x)
		{
			return math.dot(x, x);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000CF7A File Offset: 0x0000B17A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float lengthsq(float4 x)
		{
			return math.dot(x, x);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000CF83 File Offset: 0x0000B183
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double lengthsq(double x)
		{
			return x * x;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000CF88 File Offset: 0x0000B188
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double lengthsq(double2 x)
		{
			return math.dot(x, x);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000CF91 File Offset: 0x0000B191
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double lengthsq(double3 x)
		{
			return math.dot(x, x);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000CF9A File Offset: 0x0000B19A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double lengthsq(double4 x)
		{
			return math.dot(x, x);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000CFA3 File Offset: 0x0000B1A3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float distance(float x, float y)
		{
			return math.abs(y - x);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000CFAD File Offset: 0x0000B1AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float distance(float2 x, float2 y)
		{
			return math.length(y - x);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000CFBB File Offset: 0x0000B1BB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float distance(float3 x, float3 y)
		{
			return math.length(y - x);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000CFC9 File Offset: 0x0000B1C9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float distance(float4 x, float4 y)
		{
			return math.length(y - x);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000CFD7 File Offset: 0x0000B1D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double distance(double x, double y)
		{
			return math.abs(y - x);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000CFE1 File Offset: 0x0000B1E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double distance(double2 x, double2 y)
		{
			return math.length(y - x);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000CFEF File Offset: 0x0000B1EF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double distance(double3 x, double3 y)
		{
			return math.length(y - x);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000CFFD File Offset: 0x0000B1FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double distance(double4 x, double4 y)
		{
			return math.length(y - x);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000D00B File Offset: 0x0000B20B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float distancesq(float x, float y)
		{
			return (y - x) * (y - x);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000D014 File Offset: 0x0000B214
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float distancesq(float2 x, float2 y)
		{
			return math.lengthsq(y - x);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000D022 File Offset: 0x0000B222
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float distancesq(float3 x, float3 y)
		{
			return math.lengthsq(y - x);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000D030 File Offset: 0x0000B230
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float distancesq(float4 x, float4 y)
		{
			return math.lengthsq(y - x);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000D03E File Offset: 0x0000B23E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double distancesq(double x, double y)
		{
			return (y - x) * (y - x);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000D047 File Offset: 0x0000B247
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double distancesq(double2 x, double2 y)
		{
			return math.lengthsq(y - x);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000D055 File Offset: 0x0000B255
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double distancesq(double3 x, double3 y)
		{
			return math.lengthsq(y - x);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0000D063 File Offset: 0x0000B263
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double distancesq(double4 x, double4 y)
		{
			return math.lengthsq(y - x);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000D074 File Offset: 0x0000B274
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 cross(float3 x, float3 y)
		{
			return (x * y.yzx - x.yzx * y).yzx;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0000D0A8 File Offset: 0x0000B2A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 cross(double3 x, double3 y)
		{
			return (x * y.yzx - x.yzx * y).yzx;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000D0DC File Offset: 0x0000B2DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float smoothstep(float a, float b, float x)
		{
			float num = math.saturate((x - a) / (b - a));
			return num * num * (3f - 2f * num);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0000D108 File Offset: 0x0000B308
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 smoothstep(float2 a, float2 b, float2 x)
		{
			float2 @float = math.saturate((x - a) / (b - a));
			return @float * @float * (3f - 2f * @float);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000D150 File Offset: 0x0000B350
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 smoothstep(float3 a, float3 b, float3 x)
		{
			float3 @float = math.saturate((x - a) / (b - a));
			return @float * @float * (3f - 2f * @float);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000D198 File Offset: 0x0000B398
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 smoothstep(float4 a, float4 b, float4 x)
		{
			float4 @float = math.saturate((x - a) / (b - a));
			return @float * @float * (3f - 2f * @float);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000D1E0 File Offset: 0x0000B3E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double smoothstep(double a, double b, double x)
		{
			double num = math.saturate((x - a) / (b - a));
			return num * num * (3.0 - 2.0 * num);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000D214 File Offset: 0x0000B414
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 smoothstep(double2 a, double2 b, double2 x)
		{
			double2 @double = math.saturate((x - a) / (b - a));
			return @double * @double * (3.0 - 2.0 * @double);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000D264 File Offset: 0x0000B464
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 smoothstep(double3 a, double3 b, double3 x)
		{
			double3 @double = math.saturate((x - a) / (b - a));
			return @double * @double * (3.0 - 2.0 * @double);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000D2B4 File Offset: 0x0000B4B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 smoothstep(double4 a, double4 b, double4 x)
		{
			double4 @double = math.saturate((x - a) / (b - a));
			return @double * @double * (3.0 - 2.0 * @double);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000D303 File Offset: 0x0000B503
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(bool2 x)
		{
			return x.x || x.y;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000D315 File Offset: 0x0000B515
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(bool3 x)
		{
			return x.x || x.y || x.z;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000D32F File Offset: 0x0000B52F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(bool4 x)
		{
			return x.x || x.y || x.z || x.w;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000D351 File Offset: 0x0000B551
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(int2 x)
		{
			return x.x != 0 || x.y != 0;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000D366 File Offset: 0x0000B566
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(int3 x)
		{
			return x.x != 0 || x.y != 0 || x.z != 0;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000D383 File Offset: 0x0000B583
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(int4 x)
		{
			return x.x != 0 || x.y != 0 || x.z != 0 || x.w != 0;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000D3A8 File Offset: 0x0000B5A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(uint2 x)
		{
			return x.x != 0U || x.y > 0U;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000D3BD File Offset: 0x0000B5BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(uint3 x)
		{
			return x.x != 0U || x.y != 0U || x.z > 0U;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000D3DA File Offset: 0x0000B5DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(uint4 x)
		{
			return x.x != 0U || x.y != 0U || x.z != 0U || x.w > 0U;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000D3FF File Offset: 0x0000B5FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(float2 x)
		{
			return x.x != 0f || x.y != 0f;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000D420 File Offset: 0x0000B620
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(float3 x)
		{
			return x.x != 0f || x.y != 0f || x.z != 0f;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000D44E File Offset: 0x0000B64E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(float4 x)
		{
			return x.x != 0f || x.y != 0f || x.z != 0f || x.w != 0f;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000D489 File Offset: 0x0000B689
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(double2 x)
		{
			return x.x != 0.0 || x.y != 0.0;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000D4B2 File Offset: 0x0000B6B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(double3 x)
		{
			return x.x != 0.0 || x.y != 0.0 || x.z != 0.0;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000D4EC File Offset: 0x0000B6EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(double4 x)
		{
			return x.x != 0.0 || x.y != 0.0 || x.z != 0.0 || x.w != 0.0;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000D542 File Offset: 0x0000B742
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(bool2 x)
		{
			return x.x && x.y;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000D554 File Offset: 0x0000B754
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(bool3 x)
		{
			return x.x && x.y && x.z;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000D56E File Offset: 0x0000B76E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(bool4 x)
		{
			return x.x && x.y && x.z && x.w;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000D590 File Offset: 0x0000B790
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(int2 x)
		{
			return x.x != 0 && x.y != 0;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000D5A5 File Offset: 0x0000B7A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(int3 x)
		{
			return x.x != 0 && x.y != 0 && x.z != 0;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000D5C2 File Offset: 0x0000B7C2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(int4 x)
		{
			return x.x != 0 && x.y != 0 && x.z != 0 && x.w != 0;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000D5E7 File Offset: 0x0000B7E7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(uint2 x)
		{
			return x.x != 0U && x.y > 0U;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000D5FC File Offset: 0x0000B7FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(uint3 x)
		{
			return x.x != 0U && x.y != 0U && x.z > 0U;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000D619 File Offset: 0x0000B819
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(uint4 x)
		{
			return x.x != 0U && x.y != 0U && x.z != 0U && x.w > 0U;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000D63E File Offset: 0x0000B83E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(float2 x)
		{
			return x.x != 0f && x.y != 0f;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000D65F File Offset: 0x0000B85F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(float3 x)
		{
			return x.x != 0f && x.y != 0f && x.z != 0f;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000D68D File Offset: 0x0000B88D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(float4 x)
		{
			return x.x != 0f && x.y != 0f && x.z != 0f && x.w != 0f;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000D6C8 File Offset: 0x0000B8C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(double2 x)
		{
			return x.x != 0.0 && x.y != 0.0;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000D6F1 File Offset: 0x0000B8F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(double3 x)
		{
			return x.x != 0.0 && x.y != 0.0 && x.z != 0.0;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000D72C File Offset: 0x0000B92C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(double4 x)
		{
			return x.x != 0.0 && x.y != 0.0 && x.z != 0.0 && x.w != 0.0;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000D782 File Offset: 0x0000B982
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int select(int a, int b, bool c)
		{
			if (!c)
			{
				return a;
			}
			return b;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000D78A File Offset: 0x0000B98A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 select(int2 a, int2 b, bool c)
		{
			if (!c)
			{
				return a;
			}
			return b;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000D792 File Offset: 0x0000B992
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 select(int3 a, int3 b, bool c)
		{
			if (!c)
			{
				return a;
			}
			return b;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0000D79A File Offset: 0x0000B99A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 select(int4 a, int4 b, bool c)
		{
			if (!c)
			{
				return a;
			}
			return b;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000D7A2 File Offset: 0x0000B9A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 select(int2 a, int2 b, bool2 c)
		{
			return new int2(c.x ? b.x : a.x, c.y ? b.y : a.y);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000D7D8 File Offset: 0x0000B9D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 select(int3 a, int3 b, bool3 c)
		{
			return new int3(c.x ? b.x : a.x, c.y ? b.y : a.y, c.z ? b.z : a.z);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000D82C File Offset: 0x0000BA2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 select(int4 a, int4 b, bool4 c)
		{
			return new int4(c.x ? b.x : a.x, c.y ? b.y : a.y, c.z ? b.z : a.z, c.w ? b.w : a.w);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000D896 File Offset: 0x0000BA96
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint select(uint a, uint b, bool c)
		{
			if (!c)
			{
				return a;
			}
			return b;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000D89E File Offset: 0x0000BA9E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 select(uint2 a, uint2 b, bool c)
		{
			if (!c)
			{
				return a;
			}
			return b;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0000D8A6 File Offset: 0x0000BAA6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 select(uint3 a, uint3 b, bool c)
		{
			if (!c)
			{
				return a;
			}
			return b;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0000D8AE File Offset: 0x0000BAAE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 select(uint4 a, uint4 b, bool c)
		{
			if (!c)
			{
				return a;
			}
			return b;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000D8B6 File Offset: 0x0000BAB6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 select(uint2 a, uint2 b, bool2 c)
		{
			return new uint2(c.x ? b.x : a.x, c.y ? b.y : a.y);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0000D8EC File Offset: 0x0000BAEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 select(uint3 a, uint3 b, bool3 c)
		{
			return new uint3(c.x ? b.x : a.x, c.y ? b.y : a.y, c.z ? b.z : a.z);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000D940 File Offset: 0x0000BB40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 select(uint4 a, uint4 b, bool4 c)
		{
			return new uint4(c.x ? b.x : a.x, c.y ? b.y : a.y, c.z ? b.z : a.z, c.w ? b.w : a.w);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000D9AA File Offset: 0x0000BBAA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long select(long a, long b, bool c)
		{
			if (!c)
			{
				return a;
			}
			return b;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0000D9B2 File Offset: 0x0000BBB2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong select(ulong a, ulong b, bool c)
		{
			if (!c)
			{
				return a;
			}
			return b;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0000D9BA File Offset: 0x0000BBBA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float select(float a, float b, bool c)
		{
			if (!c)
			{
				return a;
			}
			return b;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0000D9C2 File Offset: 0x0000BBC2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 select(float2 a, float2 b, bool c)
		{
			if (!c)
			{
				return a;
			}
			return b;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0000D9CA File Offset: 0x0000BBCA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 select(float3 a, float3 b, bool c)
		{
			if (!c)
			{
				return a;
			}
			return b;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0000D9D2 File Offset: 0x0000BBD2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 select(float4 a, float4 b, bool c)
		{
			if (!c)
			{
				return a;
			}
			return b;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0000D9DA File Offset: 0x0000BBDA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 select(float2 a, float2 b, bool2 c)
		{
			return new float2(c.x ? b.x : a.x, c.y ? b.y : a.y);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000DA10 File Offset: 0x0000BC10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 select(float3 a, float3 b, bool3 c)
		{
			return new float3(c.x ? b.x : a.x, c.y ? b.y : a.y, c.z ? b.z : a.z);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0000DA64 File Offset: 0x0000BC64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 select(float4 a, float4 b, bool4 c)
		{
			return new float4(c.x ? b.x : a.x, c.y ? b.y : a.y, c.z ? b.z : a.z, c.w ? b.w : a.w);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000DACE File Offset: 0x0000BCCE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double select(double a, double b, bool c)
		{
			if (!c)
			{
				return a;
			}
			return b;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0000DAD6 File Offset: 0x0000BCD6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 select(double2 a, double2 b, bool c)
		{
			if (!c)
			{
				return a;
			}
			return b;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0000DADE File Offset: 0x0000BCDE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 select(double3 a, double3 b, bool c)
		{
			if (!c)
			{
				return a;
			}
			return b;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0000DAE6 File Offset: 0x0000BCE6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 select(double4 a, double4 b, bool c)
		{
			if (!c)
			{
				return a;
			}
			return b;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0000DAEE File Offset: 0x0000BCEE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 select(double2 a, double2 b, bool2 c)
		{
			return new double2(c.x ? b.x : a.x, c.y ? b.y : a.y);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000DB24 File Offset: 0x0000BD24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 select(double3 a, double3 b, bool3 c)
		{
			return new double3(c.x ? b.x : a.x, c.y ? b.y : a.y, c.z ? b.z : a.z);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0000DB78 File Offset: 0x0000BD78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 select(double4 a, double4 b, bool4 c)
		{
			return new double4(c.x ? b.x : a.x, c.y ? b.y : a.y, c.z ? b.z : a.z, c.w ? b.w : a.w);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0000DBE2 File Offset: 0x0000BDE2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float step(float y, float x)
		{
			return math.select(0f, 1f, x >= y);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0000DBFA File Offset: 0x0000BDFA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 step(float2 y, float2 x)
		{
			return math.select(math.float2(0f), math.float2(1f), x >= y);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0000DC1C File Offset: 0x0000BE1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 step(float3 y, float3 x)
		{
			return math.select(math.float3(0f), math.float3(1f), x >= y);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0000DC3E File Offset: 0x0000BE3E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 step(float4 y, float4 x)
		{
			return math.select(math.float4(0f), math.float4(1f), x >= y);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0000DC60 File Offset: 0x0000BE60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double step(double y, double x)
		{
			return math.select(0.0, 1.0, x >= y);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0000DC80 File Offset: 0x0000BE80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 step(double2 y, double2 x)
		{
			return math.select(math.double2(0.0), math.double2(1.0), x >= y);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0000DCAA File Offset: 0x0000BEAA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 step(double3 y, double3 x)
		{
			return math.select(math.double3(0.0), math.double3(1.0), x >= y);
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0000DCD4 File Offset: 0x0000BED4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 step(double4 y, double4 x)
		{
			return math.select(math.double4(0.0), math.double4(1.0), x >= y);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0000DCFE File Offset: 0x0000BEFE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 reflect(float2 i, float2 n)
		{
			return i - 2f * n * math.dot(i, n);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0000DD1D File Offset: 0x0000BF1D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 reflect(float3 i, float3 n)
		{
			return i - 2f * n * math.dot(i, n);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0000DD3C File Offset: 0x0000BF3C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 reflect(float4 i, float4 n)
		{
			return i - 2f * n * math.dot(i, n);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0000DD5B File Offset: 0x0000BF5B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 reflect(double2 i, double2 n)
		{
			return i - 2.0 * n * math.dot(i, n);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0000DD7E File Offset: 0x0000BF7E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 reflect(double3 i, double3 n)
		{
			return i - 2.0 * n * math.dot(i, n);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0000DDA1 File Offset: 0x0000BFA1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 reflect(double4 i, double4 n)
		{
			return i - 2.0 * n * math.dot(i, n);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0000DDC4 File Offset: 0x0000BFC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 refract(float2 i, float2 n, float eta)
		{
			float num = math.dot(n, i);
			float num2 = 1f - eta * eta * (1f - num * num);
			return math.select(0f, eta * i - (eta * num + math.sqrt(num2)) * n, num2 >= 0f);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0000DE24 File Offset: 0x0000C024
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 refract(float3 i, float3 n, float eta)
		{
			float num = math.dot(n, i);
			float num2 = 1f - eta * eta * (1f - num * num);
			return math.select(0f, eta * i - (eta * num + math.sqrt(num2)) * n, num2 >= 0f);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0000DE84 File Offset: 0x0000C084
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 refract(float4 i, float4 n, float eta)
		{
			float num = math.dot(n, i);
			float num2 = 1f - eta * eta * (1f - num * num);
			return math.select(0f, eta * i - (eta * num + math.sqrt(num2)) * n, num2 >= 0f);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0000DEE4 File Offset: 0x0000C0E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 refract(double2 i, double2 n, double eta)
		{
			double num = math.dot(n, i);
			double num2 = 1.0 - eta * eta * (1.0 - num * num);
			return math.select(0f, eta * i - (eta * num + math.sqrt(num2)) * n, num2 >= 0.0);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0000DF50 File Offset: 0x0000C150
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 refract(double3 i, double3 n, double eta)
		{
			double num = math.dot(n, i);
			double num2 = 1.0 - eta * eta * (1.0 - num * num);
			return math.select(0f, eta * i - (eta * num + math.sqrt(num2)) * n, num2 >= 0.0);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0000DFBC File Offset: 0x0000C1BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 refract(double4 i, double4 n, double eta)
		{
			double num = math.dot(n, i);
			double num2 = 1.0 - eta * eta * (1.0 - num * num);
			return math.select(0f, eta * i - (eta * num + math.sqrt(num2)) * n, num2 >= 0.0);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0000E027 File Offset: 0x0000C227
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 project(float2 a, float2 b)
		{
			return math.dot(a, b) / math.dot(b, b) * b;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0000E03E File Offset: 0x0000C23E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 project(float3 a, float3 b)
		{
			return math.dot(a, b) / math.dot(b, b) * b;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0000E055 File Offset: 0x0000C255
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 project(float4 a, float4 b)
		{
			return math.dot(a, b) / math.dot(b, b) * b;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0000E06C File Offset: 0x0000C26C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 projectsafe(float2 a, float2 b, float2 defaultValue = default(float2))
		{
			float2 @float = math.project(a, b);
			return math.select(defaultValue, @float, math.all(math.isfinite(@float)));
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0000E094 File Offset: 0x0000C294
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 projectsafe(float3 a, float3 b, float3 defaultValue = default(float3))
		{
			float3 @float = math.project(a, b);
			return math.select(defaultValue, @float, math.all(math.isfinite(@float)));
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0000E0BC File Offset: 0x0000C2BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 projectsafe(float4 a, float4 b, float4 defaultValue = default(float4))
		{
			float4 @float = math.project(a, b);
			return math.select(defaultValue, @float, math.all(math.isfinite(@float)));
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0000E0E3 File Offset: 0x0000C2E3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 project(double2 a, double2 b)
		{
			return math.dot(a, b) / math.dot(b, b) * b;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0000E0FA File Offset: 0x0000C2FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 project(double3 a, double3 b)
		{
			return math.dot(a, b) / math.dot(b, b) * b;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0000E111 File Offset: 0x0000C311
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 project(double4 a, double4 b)
		{
			return math.dot(a, b) / math.dot(b, b) * b;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0000E128 File Offset: 0x0000C328
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 projectsafe(double2 a, double2 b, double2 defaultValue = default(double2))
		{
			double2 @double = math.project(a, b);
			return math.select(defaultValue, @double, math.all(math.isfinite(@double)));
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0000E150 File Offset: 0x0000C350
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 projectsafe(double3 a, double3 b, double3 defaultValue = default(double3))
		{
			double3 @double = math.project(a, b);
			return math.select(defaultValue, @double, math.all(math.isfinite(@double)));
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0000E178 File Offset: 0x0000C378
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 projectsafe(double4 a, double4 b, double4 defaultValue = default(double4))
		{
			double4 @double = math.project(a, b);
			return math.select(defaultValue, @double, math.all(math.isfinite(@double)));
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0000E19F File Offset: 0x0000C39F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 faceforward(float2 n, float2 i, float2 ng)
		{
			return math.select(n, -n, math.dot(ng, i) >= 0f);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0000E1BE File Offset: 0x0000C3BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 faceforward(float3 n, float3 i, float3 ng)
		{
			return math.select(n, -n, math.dot(ng, i) >= 0f);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0000E1DD File Offset: 0x0000C3DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 faceforward(float4 n, float4 i, float4 ng)
		{
			return math.select(n, -n, math.dot(ng, i) >= 0f);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0000E1FC File Offset: 0x0000C3FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 faceforward(double2 n, double2 i, double2 ng)
		{
			return math.select(n, -n, math.dot(ng, i) >= 0.0);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0000E21F File Offset: 0x0000C41F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 faceforward(double3 n, double3 i, double3 ng)
		{
			return math.select(n, -n, math.dot(ng, i) >= 0.0);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0000E242 File Offset: 0x0000C442
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 faceforward(double4 n, double4 i, double4 ng)
		{
			return math.select(n, -n, math.dot(ng, i) >= 0.0);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0000E265 File Offset: 0x0000C465
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void sincos(float x, out float s, out float c)
		{
			s = math.sin(x);
			c = math.cos(x);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0000E277 File Offset: 0x0000C477
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void sincos(float2 x, out float2 s, out float2 c)
		{
			s = math.sin(x);
			c = math.cos(x);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0000E291 File Offset: 0x0000C491
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void sincos(float3 x, out float3 s, out float3 c)
		{
			s = math.sin(x);
			c = math.cos(x);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0000E2AB File Offset: 0x0000C4AB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void sincos(float4 x, out float4 s, out float4 c)
		{
			s = math.sin(x);
			c = math.cos(x);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0000E2C5 File Offset: 0x0000C4C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void sincos(double x, out double s, out double c)
		{
			s = math.sin(x);
			c = math.cos(x);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0000E2D7 File Offset: 0x0000C4D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void sincos(double2 x, out double2 s, out double2 c)
		{
			s = math.sin(x);
			c = math.cos(x);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0000E2F1 File Offset: 0x0000C4F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void sincos(double3 x, out double3 s, out double3 c)
		{
			s = math.sin(x);
			c = math.cos(x);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0000E30B File Offset: 0x0000C50B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void sincos(double4 x, out double4 s, out double4 c)
		{
			s = math.sin(x);
			c = math.cos(x);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0000E325 File Offset: 0x0000C525
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int countbits(int x)
		{
			return math.countbits((uint)x);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0000E32D File Offset: 0x0000C52D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 countbits(int2 x)
		{
			return math.countbits((uint2)x);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0000E33A File Offset: 0x0000C53A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 countbits(int3 x)
		{
			return math.countbits((uint3)x);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0000E347 File Offset: 0x0000C547
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 countbits(int4 x)
		{
			return math.countbits((uint4)x);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0000E354 File Offset: 0x0000C554
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int countbits(uint x)
		{
			x -= (x >> 1 & 1431655765U);
			x = (x & 858993459U) + (x >> 2 & 858993459U);
			return (int)((x + (x >> 4) & 252645135U) * 16843009U >> 24);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0000E38C File Offset: 0x0000C58C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 countbits(uint2 x)
		{
			x -= (x >> 1 & 1431655765U);
			x = (x & 858993459U) + (x >> 2 & 858993459U);
			return math.int2((x + (x >> 4) & 252645135U) * 16843009U >> 24);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0000E404 File Offset: 0x0000C604
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 countbits(uint3 x)
		{
			x -= (x >> 1 & 1431655765U);
			x = (x & 858993459U) + (x >> 2 & 858993459U);
			return math.int3((x + (x >> 4) & 252645135U) * 16843009U >> 24);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0000E47C File Offset: 0x0000C67C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 countbits(uint4 x)
		{
			x -= (x >> 1 & 1431655765U);
			x = (x & 858993459U) + (x >> 2 & 858993459U);
			return math.int4((x + (x >> 4) & 252645135U) * 16843009U >> 24);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0000E4F4 File Offset: 0x0000C6F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int countbits(ulong x)
		{
			x -= (x >> 1 & 6148914691236517205UL);
			x = (x & 3689348814741910323UL) + (x >> 2 & 3689348814741910323UL);
			return (int)((x + (x >> 4) & 1085102592571150095UL) * 72340172838076673UL >> 56);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0000E54A File Offset: 0x0000C74A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int countbits(long x)
		{
			return math.countbits((ulong)x);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0000E552 File Offset: 0x0000C752
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int lzcnt(int x)
		{
			return math.lzcnt((uint)x);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0000E55A File Offset: 0x0000C75A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 lzcnt(int2 x)
		{
			return math.int2(math.lzcnt(x.x), math.lzcnt(x.y));
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0000E577 File Offset: 0x0000C777
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 lzcnt(int3 x)
		{
			return math.int3(math.lzcnt(x.x), math.lzcnt(x.y), math.lzcnt(x.z));
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0000E59F File Offset: 0x0000C79F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 lzcnt(int4 x)
		{
			return math.int4(math.lzcnt(x.x), math.lzcnt(x.y), math.lzcnt(x.z), math.lzcnt(x.w));
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0000E5D4 File Offset: 0x0000C7D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int lzcnt(uint x)
		{
			if (x == 0U)
			{
				return 32;
			}
			math.LongDoubleUnion longDoubleUnion;
			longDoubleUnion.doubleValue = 0.0;
			longDoubleUnion.longValue = (long)(4841369599423283200UL + (ulong)x);
			longDoubleUnion.doubleValue -= 4503599627370496.0;
			return 1054 - (int)(longDoubleUnion.longValue >> 52);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0000E62E File Offset: 0x0000C82E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 lzcnt(uint2 x)
		{
			return math.int2(math.lzcnt(x.x), math.lzcnt(x.y));
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0000E64B File Offset: 0x0000C84B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 lzcnt(uint3 x)
		{
			return math.int3(math.lzcnt(x.x), math.lzcnt(x.y), math.lzcnt(x.z));
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0000E673 File Offset: 0x0000C873
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 lzcnt(uint4 x)
		{
			return math.int4(math.lzcnt(x.x), math.lzcnt(x.y), math.lzcnt(x.z), math.lzcnt(x.w));
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0000E6A6 File Offset: 0x0000C8A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int lzcnt(long x)
		{
			return math.lzcnt((ulong)x);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0000E6B0 File Offset: 0x0000C8B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int lzcnt(ulong x)
		{
			if (x == 0UL)
			{
				return 64;
			}
			uint num = (uint)(x >> 32);
			uint num2 = (num != 0U) ? num : ((uint)x);
			int num3 = (num != 0U) ? 1054 : 1086;
			math.LongDoubleUnion longDoubleUnion;
			longDoubleUnion.doubleValue = 0.0;
			longDoubleUnion.longValue = (long)(4841369599423283200UL + (ulong)num2);
			longDoubleUnion.doubleValue -= 4503599627370496.0;
			return num3 - (int)(longDoubleUnion.longValue >> 52);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0000E723 File Offset: 0x0000C923
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int tzcnt(int x)
		{
			return math.tzcnt((uint)x);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000E72B File Offset: 0x0000C92B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 tzcnt(int2 x)
		{
			return math.int2(math.tzcnt(x.x), math.tzcnt(x.y));
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0000E748 File Offset: 0x0000C948
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 tzcnt(int3 x)
		{
			return math.int3(math.tzcnt(x.x), math.tzcnt(x.y), math.tzcnt(x.z));
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0000E770 File Offset: 0x0000C970
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 tzcnt(int4 x)
		{
			return math.int4(math.tzcnt(x.x), math.tzcnt(x.y), math.tzcnt(x.z), math.tzcnt(x.w));
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0000E7A4 File Offset: 0x0000C9A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int tzcnt(uint x)
		{
			if (x == 0U)
			{
				return 32;
			}
			x &= (uint)(-(uint)((ulong)x));
			math.LongDoubleUnion longDoubleUnion;
			longDoubleUnion.doubleValue = 0.0;
			longDoubleUnion.longValue = (long)(4841369599423283200UL + (ulong)x);
			longDoubleUnion.doubleValue -= 4503599627370496.0;
			return (int)(longDoubleUnion.longValue >> 52) - 1023;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0000E806 File Offset: 0x0000CA06
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 tzcnt(uint2 x)
		{
			return math.int2(math.tzcnt(x.x), math.tzcnt(x.y));
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0000E823 File Offset: 0x0000CA23
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 tzcnt(uint3 x)
		{
			return math.int3(math.tzcnt(x.x), math.tzcnt(x.y), math.tzcnt(x.z));
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0000E84B File Offset: 0x0000CA4B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 tzcnt(uint4 x)
		{
			return math.int4(math.tzcnt(x.x), math.tzcnt(x.y), math.tzcnt(x.z), math.tzcnt(x.w));
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0000E87E File Offset: 0x0000CA7E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int tzcnt(long x)
		{
			return math.tzcnt((ulong)x);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0000E888 File Offset: 0x0000CA88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int tzcnt(ulong x)
		{
			if (x == 0UL)
			{
				return 64;
			}
			x &= -x;
			uint num = (uint)x;
			uint num2 = (num != 0U) ? num : ((uint)(x >> 32));
			int num3 = (num != 0U) ? 1023 : 991;
			math.LongDoubleUnion longDoubleUnion;
			longDoubleUnion.doubleValue = 0.0;
			longDoubleUnion.longValue = (long)(4841369599423283200UL + (ulong)num2);
			longDoubleUnion.doubleValue -= 4503599627370496.0;
			return (int)(longDoubleUnion.longValue >> 52) - num3;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0000E903 File Offset: 0x0000CB03
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int reversebits(int x)
		{
			return (int)math.reversebits((uint)x);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0000E90B File Offset: 0x0000CB0B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 reversebits(int2 x)
		{
			return (int2)math.reversebits((uint2)x);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0000E91D File Offset: 0x0000CB1D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 reversebits(int3 x)
		{
			return (int3)math.reversebits((uint3)x);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0000E92F File Offset: 0x0000CB2F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 reversebits(int4 x)
		{
			return (int4)math.reversebits((uint4)x);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0000E944 File Offset: 0x0000CB44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint reversebits(uint x)
		{
			x = ((x >> 1 & 1431655765U) | (x & 1431655765U) << 1);
			x = ((x >> 2 & 858993459U) | (x & 858993459U) << 2);
			x = ((x >> 4 & 252645135U) | (x & 252645135U) << 4);
			x = ((x >> 8 & 16711935U) | (x & 16711935U) << 8);
			return x >> 16 | x << 16;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0000E9B0 File Offset: 0x0000CBB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 reversebits(uint2 x)
		{
			x = ((x >> 1 & 1431655765U) | (x & 1431655765U) << 1);
			x = ((x >> 2 & 858993459U) | (x & 858993459U) << 2);
			x = ((x >> 4 & 252645135U) | (x & 252645135U) << 4);
			x = ((x >> 8 & 16711935U) | (x & 16711935U) << 8);
			return x >> 16 | x << 16;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0000EA78 File Offset: 0x0000CC78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 reversebits(uint3 x)
		{
			x = ((x >> 1 & 1431655765U) | (x & 1431655765U) << 1);
			x = ((x >> 2 & 858993459U) | (x & 858993459U) << 2);
			x = ((x >> 4 & 252645135U) | (x & 252645135U) << 4);
			x = ((x >> 8 & 16711935U) | (x & 16711935U) << 8);
			return x >> 16 | x << 16;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0000EB40 File Offset: 0x0000CD40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 reversebits(uint4 x)
		{
			x = ((x >> 1 & 1431655765U) | (x & 1431655765U) << 1);
			x = ((x >> 2 & 858993459U) | (x & 858993459U) << 2);
			x = ((x >> 4 & 252645135U) | (x & 252645135U) << 4);
			x = ((x >> 8 & 16711935U) | (x & 16711935U) << 8);
			return x >> 16 | x << 16;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0000EC06 File Offset: 0x0000CE06
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long reversebits(long x)
		{
			return (long)math.reversebits((ulong)x);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0000EC10 File Offset: 0x0000CE10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong reversebits(ulong x)
		{
			x = ((x >> 1 & 6148914691236517205UL) | (x & 6148914691236517205UL) << 1);
			x = ((x >> 2 & 3689348814741910323UL) | (x & 3689348814741910323UL) << 2);
			x = ((x >> 4 & 1085102592571150095UL) | (x & 1085102592571150095UL) << 4);
			x = ((x >> 8 & 71777214294589695UL) | (x & 71777214294589695UL) << 8);
			x = ((x >> 16 & 281470681808895UL) | (x & 281470681808895UL) << 16);
			return x >> 32 | x << 32;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0000ECB9 File Offset: 0x0000CEB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int rol(int x, int n)
		{
			return (int)math.rol((uint)x, n);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0000ECC2 File Offset: 0x0000CEC2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 rol(int2 x, int n)
		{
			return (int2)math.rol((uint2)x, n);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0000ECD5 File Offset: 0x0000CED5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 rol(int3 x, int n)
		{
			return (int3)math.rol((uint3)x, n);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0000ECE8 File Offset: 0x0000CEE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 rol(int4 x, int n)
		{
			return (int4)math.rol((uint4)x, n);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0000ECFB File Offset: 0x0000CEFB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint rol(uint x, int n)
		{
			return x << n | x >> 32 - n;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0000ED0D File Offset: 0x0000CF0D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 rol(uint2 x, int n)
		{
			return x << n | x >> 32 - n;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0000ED25 File Offset: 0x0000CF25
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 rol(uint3 x, int n)
		{
			return x << n | x >> 32 - n;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0000ED3D File Offset: 0x0000CF3D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 rol(uint4 x, int n)
		{
			return x << n | x >> 32 - n;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0000ED55 File Offset: 0x0000CF55
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long rol(long x, int n)
		{
			return (long)math.rol((ulong)x, n);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0000ED5E File Offset: 0x0000CF5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong rol(ulong x, int n)
		{
			return x << n | x >> 64 - n;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0000ED70 File Offset: 0x0000CF70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ror(int x, int n)
		{
			return (int)math.ror((uint)x, n);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0000ED79 File Offset: 0x0000CF79
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 ror(int2 x, int n)
		{
			return (int2)math.ror((uint2)x, n);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0000ED8C File Offset: 0x0000CF8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 ror(int3 x, int n)
		{
			return (int3)math.ror((uint3)x, n);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0000ED9F File Offset: 0x0000CF9F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 ror(int4 x, int n)
		{
			return (int4)math.ror((uint4)x, n);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0000EDB2 File Offset: 0x0000CFB2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint ror(uint x, int n)
		{
			return x >> n | x << 32 - n;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0000EDC4 File Offset: 0x0000CFC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 ror(uint2 x, int n)
		{
			return x >> n | x << 32 - n;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0000EDDC File Offset: 0x0000CFDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 ror(uint3 x, int n)
		{
			return x >> n | x << 32 - n;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0000EDF4 File Offset: 0x0000CFF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 ror(uint4 x, int n)
		{
			return x >> n | x << 32 - n;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0000EE0C File Offset: 0x0000D00C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long ror(long x, int n)
		{
			return (long)math.ror((ulong)x, n);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0000EE15 File Offset: 0x0000D015
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong ror(ulong x, int n)
		{
			return x >> n | x << 64 - n;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0000EE27 File Offset: 0x0000D027
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ceilpow2(int x)
		{
			x--;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x + 1;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0000EE58 File Offset: 0x0000D058
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 ceilpow2(int2 x)
		{
			x -= 1;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x + 1;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0000EEC4 File Offset: 0x0000D0C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 ceilpow2(int3 x)
		{
			x -= 1;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x + 1;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0000EF30 File Offset: 0x0000D130
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 ceilpow2(int4 x)
		{
			x -= 1;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x + 1;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0000EF99 File Offset: 0x0000D199
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint ceilpow2(uint x)
		{
			x -= 1U;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x + 1U;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0000EFC8 File Offset: 0x0000D1C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 ceilpow2(uint2 x)
		{
			x -= 1U;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x + 1U;
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0000F034 File Offset: 0x0000D234
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 ceilpow2(uint3 x)
		{
			x -= 1U;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x + 1U;
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0000F0A0 File Offset: 0x0000D2A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 ceilpow2(uint4 x)
		{
			x -= 1U;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x + 1U;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0000F109 File Offset: 0x0000D309
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long ceilpow2(long x)
		{
			x -= 1L;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			x |= x >> 32;
			return x + 1L;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0000F141 File Offset: 0x0000D341
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong ceilpow2(ulong x)
		{
			x -= 1UL;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			x |= x >> 32;
			return x + 1UL;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0000F179 File Offset: 0x0000D379
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ceillog2(int x)
		{
			return 32 - math.lzcnt((uint)(x - 1));
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0000F186 File Offset: 0x0000D386
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 ceillog2(int2 x)
		{
			return new int2(math.ceillog2(x.x), math.ceillog2(x.y));
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0000F1A3 File Offset: 0x0000D3A3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 ceillog2(int3 x)
		{
			return new int3(math.ceillog2(x.x), math.ceillog2(x.y), math.ceillog2(x.z));
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0000F1CB File Offset: 0x0000D3CB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 ceillog2(int4 x)
		{
			return new int4(math.ceillog2(x.x), math.ceillog2(x.y), math.ceillog2(x.z), math.ceillog2(x.w));
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0000F1FE File Offset: 0x0000D3FE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ceillog2(uint x)
		{
			return 32 - math.lzcnt(x - 1U);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0000F20B File Offset: 0x0000D40B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 ceillog2(uint2 x)
		{
			return new int2(math.ceillog2(x.x), math.ceillog2(x.y));
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0000F228 File Offset: 0x0000D428
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 ceillog2(uint3 x)
		{
			return new int3(math.ceillog2(x.x), math.ceillog2(x.y), math.ceillog2(x.z));
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0000F250 File Offset: 0x0000D450
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 ceillog2(uint4 x)
		{
			return new int4(math.ceillog2(x.x), math.ceillog2(x.y), math.ceillog2(x.z), math.ceillog2(x.w));
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0000F283 File Offset: 0x0000D483
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int floorlog2(int x)
		{
			return 31 - math.lzcnt((uint)x);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0000F28E File Offset: 0x0000D48E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 floorlog2(int2 x)
		{
			return new int2(math.floorlog2(x.x), math.floorlog2(x.y));
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0000F2AB File Offset: 0x0000D4AB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 floorlog2(int3 x)
		{
			return new int3(math.floorlog2(x.x), math.floorlog2(x.y), math.floorlog2(x.z));
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0000F2D3 File Offset: 0x0000D4D3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 floorlog2(int4 x)
		{
			return new int4(math.floorlog2(x.x), math.floorlog2(x.y), math.floorlog2(x.z), math.floorlog2(x.w));
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0000F306 File Offset: 0x0000D506
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int floorlog2(uint x)
		{
			return 31 - math.lzcnt(x);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0000F311 File Offset: 0x0000D511
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 floorlog2(uint2 x)
		{
			return new int2(math.floorlog2(x.x), math.floorlog2(x.y));
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0000F32E File Offset: 0x0000D52E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 floorlog2(uint3 x)
		{
			return new int3(math.floorlog2(x.x), math.floorlog2(x.y), math.floorlog2(x.z));
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0000F356 File Offset: 0x0000D556
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 floorlog2(uint4 x)
		{
			return new int4(math.floorlog2(x.x), math.floorlog2(x.y), math.floorlog2(x.z), math.floorlog2(x.w));
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0000F389 File Offset: 0x0000D589
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float radians(float x)
		{
			return x * 0.017453292f;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0000F392 File Offset: 0x0000D592
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 radians(float2 x)
		{
			return x * 0.017453292f;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0000F39F File Offset: 0x0000D59F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 radians(float3 x)
		{
			return x * 0.017453292f;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0000F3AC File Offset: 0x0000D5AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 radians(float4 x)
		{
			return x * 0.017453292f;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0000F3B9 File Offset: 0x0000D5B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double radians(double x)
		{
			return x * 0.017453292519943295;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0000F3C6 File Offset: 0x0000D5C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 radians(double2 x)
		{
			return x * 0.017453292519943295;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0000F3D7 File Offset: 0x0000D5D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 radians(double3 x)
		{
			return x * 0.017453292519943295;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0000F3E8 File Offset: 0x0000D5E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 radians(double4 x)
		{
			return x * 0.017453292519943295;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0000F3F9 File Offset: 0x0000D5F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float degrees(float x)
		{
			return x * 57.29578f;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0000F402 File Offset: 0x0000D602
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 degrees(float2 x)
		{
			return x * 57.29578f;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0000F40F File Offset: 0x0000D60F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 degrees(float3 x)
		{
			return x * 57.29578f;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0000F41C File Offset: 0x0000D61C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 degrees(float4 x)
		{
			return x * 57.29578f;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0000F429 File Offset: 0x0000D629
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double degrees(double x)
		{
			return x * 57.29577951308232;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0000F436 File Offset: 0x0000D636
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 degrees(double2 x)
		{
			return x * 57.29577951308232;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0000F447 File Offset: 0x0000D647
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 degrees(double3 x)
		{
			return x * 57.29577951308232;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0000F458 File Offset: 0x0000D658
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 degrees(double4 x)
		{
			return x * 57.29577951308232;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0000F469 File Offset: 0x0000D669
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int cmin(int2 x)
		{
			return math.min(x.x, x.y);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0000F47C File Offset: 0x0000D67C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int cmin(int3 x)
		{
			return math.min(math.min(x.x, x.y), x.z);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0000F49A File Offset: 0x0000D69A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int cmin(int4 x)
		{
			return math.min(math.min(x.x, x.y), math.min(x.z, x.w));
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000F4C3 File Offset: 0x0000D6C3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint cmin(uint2 x)
		{
			return math.min(x.x, x.y);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0000F4D6 File Offset: 0x0000D6D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint cmin(uint3 x)
		{
			return math.min(math.min(x.x, x.y), x.z);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0000F4F4 File Offset: 0x0000D6F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint cmin(uint4 x)
		{
			return math.min(math.min(x.x, x.y), math.min(x.z, x.w));
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0000F51D File Offset: 0x0000D71D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float cmin(float2 x)
		{
			return math.min(x.x, x.y);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0000F530 File Offset: 0x0000D730
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float cmin(float3 x)
		{
			return math.min(math.min(x.x, x.y), x.z);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0000F54E File Offset: 0x0000D74E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float cmin(float4 x)
		{
			return math.min(math.min(x.x, x.y), math.min(x.z, x.w));
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0000F577 File Offset: 0x0000D777
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double cmin(double2 x)
		{
			return math.min(x.x, x.y);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0000F58A File Offset: 0x0000D78A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double cmin(double3 x)
		{
			return math.min(math.min(x.x, x.y), x.z);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0000F5A8 File Offset: 0x0000D7A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double cmin(double4 x)
		{
			return math.min(math.min(x.x, x.y), math.min(x.z, x.w));
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0000F5D1 File Offset: 0x0000D7D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int cmax(int2 x)
		{
			return math.max(x.x, x.y);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0000F5E4 File Offset: 0x0000D7E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int cmax(int3 x)
		{
			return math.max(math.max(x.x, x.y), x.z);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0000F602 File Offset: 0x0000D802
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int cmax(int4 x)
		{
			return math.max(math.max(x.x, x.y), math.max(x.z, x.w));
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0000F62B File Offset: 0x0000D82B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint cmax(uint2 x)
		{
			return math.max(x.x, x.y);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0000F63E File Offset: 0x0000D83E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint cmax(uint3 x)
		{
			return math.max(math.max(x.x, x.y), x.z);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0000F65C File Offset: 0x0000D85C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint cmax(uint4 x)
		{
			return math.max(math.max(x.x, x.y), math.max(x.z, x.w));
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0000F685 File Offset: 0x0000D885
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float cmax(float2 x)
		{
			return math.max(x.x, x.y);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0000F698 File Offset: 0x0000D898
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float cmax(float3 x)
		{
			return math.max(math.max(x.x, x.y), x.z);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0000F6B6 File Offset: 0x0000D8B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float cmax(float4 x)
		{
			return math.max(math.max(x.x, x.y), math.max(x.z, x.w));
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0000F6DF File Offset: 0x0000D8DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double cmax(double2 x)
		{
			return math.max(x.x, x.y);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0000F6F2 File Offset: 0x0000D8F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double cmax(double3 x)
		{
			return math.max(math.max(x.x, x.y), x.z);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0000F710 File Offset: 0x0000D910
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double cmax(double4 x)
		{
			return math.max(math.max(x.x, x.y), math.max(x.z, x.w));
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0000F739 File Offset: 0x0000D939
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int csum(int2 x)
		{
			return x.x + x.y;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0000F748 File Offset: 0x0000D948
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int csum(int3 x)
		{
			return x.x + x.y + x.z;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0000F75E File Offset: 0x0000D95E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int csum(int4 x)
		{
			return x.x + x.y + x.z + x.w;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0000F77B File Offset: 0x0000D97B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint csum(uint2 x)
		{
			return x.x + x.y;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0000F78A File Offset: 0x0000D98A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint csum(uint3 x)
		{
			return x.x + x.y + x.z;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0000F7A0 File Offset: 0x0000D9A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint csum(uint4 x)
		{
			return x.x + x.y + x.z + x.w;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0000F7BD File Offset: 0x0000D9BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float csum(float2 x)
		{
			return x.x + x.y;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0000F7CC File Offset: 0x0000D9CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float csum(float3 x)
		{
			return x.x + x.y + x.z;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0000F7E2 File Offset: 0x0000D9E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float csum(float4 x)
		{
			return x.x + x.y + (x.z + x.w);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0000F7FF File Offset: 0x0000D9FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double csum(double2 x)
		{
			return x.x + x.y;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0000F80E File Offset: 0x0000DA0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double csum(double3 x)
		{
			return x.x + x.y + x.z;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0000F824 File Offset: 0x0000DA24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double csum(double4 x)
		{
			return x.x + x.y + (x.z + x.w);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0000F844 File Offset: 0x0000DA44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int compress(int* output, int index, int4 val, bool4 mask)
		{
			if (mask.x)
			{
				output[index++] = val.x;
			}
			if (mask.y)
			{
				output[index++] = val.y;
			}
			if (mask.z)
			{
				output[index++] = val.z;
			}
			if (mask.w)
			{
				output[index++] = val.w;
			}
			return index;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0000F8BA File Offset: 0x0000DABA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int compress(uint* output, int index, uint4 val, bool4 mask)
		{
			return math.compress((int*)output, index, *(int4*)(&val), mask);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0000F8CC File Offset: 0x0000DACC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int compress(float* output, int index, float4 val, bool4 mask)
		{
			return math.compress((int*)output, index, *(int4*)(&val), mask);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0000F8E0 File Offset: 0x0000DAE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float f16tof32(uint x)
		{
			uint num = (x & 32767U) << 13;
			uint num2 = num & 260046848U;
			uint num3 = num + 939524096U + math.select(0U, 939524096U, num2 == 260046848U);
			return math.asfloat(math.select(num3, math.asuint(math.asfloat(num3 + 8388608U) - 6.1035156E-05f), num2 == 0U) | (x & 32768U) << 16);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0000F94C File Offset: 0x0000DB4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 f16tof32(uint2 x)
		{
			uint2 lhs = (x & 32767U) << 13;
			uint2 lhs2 = lhs & 260046848U;
			uint2 @uint = lhs + 939524096U + math.select(0U, 939524096U, lhs2 == 260046848U);
			return math.asfloat(math.select(@uint, math.asuint(math.asfloat(@uint + 8388608U) - 6.1035156E-05f), lhs2 == 0U) | (x & 32768U) << 16);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0000F9F0 File Offset: 0x0000DBF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 f16tof32(uint3 x)
		{
			uint3 lhs = (x & 32767U) << 13;
			uint3 lhs2 = lhs & 260046848U;
			uint3 @uint = lhs + 939524096U + math.select(0U, 939524096U, lhs2 == 260046848U);
			return math.asfloat(math.select(@uint, math.asuint(math.asfloat(@uint + 8388608U) - 6.1035156E-05f), lhs2 == 0U) | (x & 32768U) << 16);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0000FA94 File Offset: 0x0000DC94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 f16tof32(uint4 x)
		{
			uint4 lhs = (x & 32767U) << 13;
			uint4 lhs2 = lhs & 260046848U;
			uint4 @uint = lhs + 939524096U + math.select(0U, 939524096U, lhs2 == 260046848U);
			return math.asfloat(math.select(@uint, math.asuint(math.asfloat(@uint + 8388608U) - 6.1035156E-05f), lhs2 == 0U) | (x & 32768U) << 16);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0000FB38 File Offset: 0x0000DD38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint f32tof16(float x)
		{
			uint num = math.asuint(x);
			uint num2 = num & 2147479552U;
			return math.select(math.asuint(math.min(math.asfloat(num2) * 1.92593E-34f, 260042750f)) + 4096U >> 13, math.select(31744U, 32256U, num2 > 2139095040U), num2 >= 2139095040U) | (num & 2147487743U) >> 16;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0000FBAC File Offset: 0x0000DDAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 f32tof16(float2 x)
		{
			uint2 lhs = math.asuint(x);
			uint2 @uint = lhs & 2147479552U;
			return math.select((uint2)(math.asint(math.min(math.asfloat(@uint) * 1.92593E-34f, 260042750f)) + 4096) >> 13, math.select(31744U, 32256U, (int2)@uint > 2139095040), (int2)@uint >= 2139095040) | (lhs & 2147487743U) >> 16;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0000FC5C File Offset: 0x0000DE5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 f32tof16(float3 x)
		{
			uint3 lhs = math.asuint(x);
			uint3 @uint = lhs & 2147479552U;
			return math.select((uint3)(math.asint(math.min(math.asfloat(@uint) * 1.92593E-34f, 260042750f)) + 4096) >> 13, math.select(31744U, 32256U, (int3)@uint > 2139095040), (int3)@uint >= 2139095040) | (lhs & 2147487743U) >> 16;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0000FD0C File Offset: 0x0000DF0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 f32tof16(float4 x)
		{
			uint4 lhs = math.asuint(x);
			uint4 @uint = lhs & 2147479552U;
			return math.select((uint4)(math.asint(math.min(math.asfloat(@uint) * 1.92593E-34f, 260042750f)) + 4096) >> 13, math.select(31744U, 32256U, (int4)@uint > 2139095040), (int4)@uint >= 2139095040) | (lhs & 2147487743U) >> 16;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0000FDBC File Offset: 0x0000DFBC
		public unsafe static uint hash(void* pBuffer, int numBytes, uint seed = 0U)
		{
			uint4* ptr = (uint4*)pBuffer;
			uint num = seed + 374761393U;
			if (numBytes >= 16)
			{
				uint4 @uint = new uint4(606290984U, 2246822519U, 0U, 1640531535U) + seed;
				int num2 = numBytes >> 4;
				for (int i = 0; i < num2; i++)
				{
					@uint += *(ptr++) * 2246822519U;
					@uint = (@uint << 13 | @uint >> 19);
					@uint *= 2654435761U;
				}
				num = math.rol(@uint.x, 1) + math.rol(@uint.y, 7) + math.rol(@uint.z, 12) + math.rol(@uint.w, 18);
			}
			num += (uint)numBytes;
			uint* ptr2 = (uint*)ptr;
			for (int j = 0; j < (numBytes >> 2 & 3); j++)
			{
				num += *(ptr2++) * 3266489917U;
				num = math.rol(num, 17) * 668265263U;
			}
			byte* ptr3 = (byte*)ptr2;
			for (int k = 0; k < (numBytes & 3); k++)
			{
				num += (uint)(*(ptr3++)) * 374761393U;
				num = math.rol(num, 11) * 2654435761U;
			}
			num ^= num >> 15;
			num *= 2246822519U;
			num ^= num >> 13;
			num *= 3266489917U;
			return num ^ num >> 16;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0000FF21 File Offset: 0x0000E121
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 up()
		{
			return new float3(0f, 1f, 0f);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0000FF37 File Offset: 0x0000E137
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 down()
		{
			return new float3(0f, -1f, 0f);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0000FF4D File Offset: 0x0000E14D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 forward()
		{
			return new float3(0f, 0f, 1f);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0000FF63 File Offset: 0x0000E163
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 back()
		{
			return new float3(0f, 0f, -1f);
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0000FF79 File Offset: 0x0000E179
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 left()
		{
			return new float3(-1f, 0f, 0f);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0000FF8F File Offset: 0x0000E18F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 right()
		{
			return new float3(1f, 0f, 0f);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0000FFA5 File Offset: 0x0000E1A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static float4 unpacklo(float4 a, float4 b)
		{
			return math.shuffle(a, b, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightX, math.ShuffleComponent.LeftY, math.ShuffleComponent.RightY);
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0000FFB2 File Offset: 0x0000E1B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static double4 unpacklo(double4 a, double4 b)
		{
			return math.shuffle(a, b, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightX, math.ShuffleComponent.LeftY, math.ShuffleComponent.RightY);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0000FFBF File Offset: 0x0000E1BF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static float4 unpackhi(float4 a, float4 b)
		{
			return math.shuffle(a, b, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightZ, math.ShuffleComponent.LeftW, math.ShuffleComponent.RightW);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0000FFCC File Offset: 0x0000E1CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static double4 unpackhi(double4 a, double4 b)
		{
			return math.shuffle(a, b, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightZ, math.ShuffleComponent.LeftW, math.ShuffleComponent.RightW);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0000FFD9 File Offset: 0x0000E1D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static float4 movelh(float4 a, float4 b)
		{
			return math.shuffle(a, b, math.ShuffleComponent.LeftX, math.ShuffleComponent.LeftY, math.ShuffleComponent.RightX, math.ShuffleComponent.RightY);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0000FFE6 File Offset: 0x0000E1E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static double4 movelh(double4 a, double4 b)
		{
			return math.shuffle(a, b, math.ShuffleComponent.LeftX, math.ShuffleComponent.LeftY, math.ShuffleComponent.RightX, math.ShuffleComponent.RightY);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0000FFF3 File Offset: 0x0000E1F3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static float4 movehl(float4 a, float4 b)
		{
			return math.shuffle(b, a, math.ShuffleComponent.LeftZ, math.ShuffleComponent.LeftW, math.ShuffleComponent.RightZ, math.ShuffleComponent.RightW);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00010000 File Offset: 0x0000E200
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static double4 movehl(double4 a, double4 b)
		{
			return math.shuffle(b, a, math.ShuffleComponent.LeftZ, math.ShuffleComponent.LeftW, math.ShuffleComponent.RightZ, math.ShuffleComponent.RightW);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00010010 File Offset: 0x0000E210
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static uint fold_to_uint(double x)
		{
			math.LongDoubleUnion longDoubleUnion;
			longDoubleUnion.longValue = 0L;
			longDoubleUnion.doubleValue = x;
			return (uint)(longDoubleUnion.longValue >> 32) ^ (uint)longDoubleUnion.longValue;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00010040 File Offset: 0x0000E240
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static uint2 fold_to_uint(double2 x)
		{
			return math.uint2(math.fold_to_uint(x.x), math.fold_to_uint(x.y));
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001005D File Offset: 0x0000E25D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static uint3 fold_to_uint(double3 x)
		{
			return math.uint3(math.fold_to_uint(x.x), math.fold_to_uint(x.y), math.fold_to_uint(x.z));
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00010085 File Offset: 0x0000E285
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static uint4 fold_to_uint(double4 x)
		{
			return math.uint4(math.fold_to_uint(x.x), math.fold_to_uint(x.y), math.fold_to_uint(x.z), math.fold_to_uint(x.w));
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x000100B8 File Offset: 0x0000E2B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 float3x3(float4x4 f4x4)
		{
			return new float3x3(f4x4);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x000100C0 File Offset: 0x0000E2C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 float3x3(quaternion rotation)
		{
			return new float3x3(rotation);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x000100C8 File Offset: 0x0000E2C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(float3x3 rotation, float3 translation)
		{
			return new float4x4(rotation, translation);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x000100D1 File Offset: 0x0000E2D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(quaternion rotation, float3 translation)
		{
			return new float4x4(rotation, translation);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x000100DA File Offset: 0x0000E2DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(RigidTransform transform)
		{
			return new float4x4(transform);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x000100E4 File Offset: 0x0000E2E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 orthonormalize(float3x3 i)
		{
			float3 c = i.c0;
			float3 @float = i.c1 - i.c0 * math.dot(i.c1, i.c0);
			float num = math.length(c);
			float num2 = math.length(@float);
			bool c2 = num > 1E-30f && num2 > 1E-30f;
			float3x3 float3x;
			float3x.c0 = math.select(math.float3(1f, 0f, 0f), c / num, c2);
			float3x.c1 = math.select(math.float3(0f, 1f, 0f), @float / num2, c2);
			float3x.c2 = math.cross(float3x.c0, float3x.c1);
			return float3x;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x000101B1 File Offset: 0x0000E3B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float mul(float a, float b)
		{
			return a * b;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x000101B6 File Offset: 0x0000E3B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float mul(float2 a, float2 b)
		{
			return a.x * b.x + a.y * b.y;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x000101D4 File Offset: 0x0000E3D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 mul(float2 a, float2x2 b)
		{
			return math.float2(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00010230 File Offset: 0x0000E430
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 mul(float2 a, float2x3 b)
		{
			return math.float3(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y, a.x * b.c2.x + a.y * b.c2.y);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x000102B4 File Offset: 0x0000E4B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 mul(float2 a, float2x4 b)
		{
			return math.float4(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y, a.x * b.c2.x + a.y * b.c2.y, a.x * b.c3.x + a.y * b.c3.y);
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0001035A File Offset: 0x0000E55A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float mul(float3 a, float3 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00010388 File Offset: 0x0000E588
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 mul(float3 a, float3x2 b)
		{
			return math.float2(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0001040C File Offset: 0x0000E60C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 mul(float3 a, float3x3 b)
		{
			return math.float3(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x000104C8 File Offset: 0x0000E6C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 mul(float3 a, float3x4 b)
		{
			return math.float4(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z, a.x * b.c3.x + a.y * b.c3.y + a.z * b.c3.z);
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x000105BA File Offset: 0x0000E7BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float mul(float4 a, float4 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x000105F4 File Offset: 0x0000E7F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 mul(float4 a, float4x2 b)
		{
			return math.float2(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0001069C File Offset: 0x0000E89C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 mul(float4 a, float4x3 b)
		{
			return math.float3(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z + a.w * b.c2.w);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00010790 File Offset: 0x0000E990
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 mul(float4 a, float4x4 b)
		{
			return math.float4(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z + a.w * b.c2.w, a.x * b.c3.x + a.y * b.c3.y + a.z * b.c3.z + a.w * b.c3.w);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x000108CE File Offset: 0x0000EACE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 mul(float2x2 a, float2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x000108F8 File Offset: 0x0000EAF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 mul(float2x2 a, float2x2 b)
		{
			return math.float2x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001096C File Offset: 0x0000EB6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 mul(float2x2 a, float2x3 b)
		{
			return math.float2x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00010A14 File Offset: 0x0000EC14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 mul(float2x2 a, float2x4 b)
		{
			return math.float2x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00010AEA File Offset: 0x0000ECEA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 mul(float2x3 a, float3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00010B2C File Offset: 0x0000ED2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 mul(float2x3 a, float3x2 b)
		{
			return math.float2x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00010BD8 File Offset: 0x0000EDD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 mul(float2x3 a, float3x3 b)
		{
			return math.float2x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00010CD0 File Offset: 0x0000EED0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 mul(float2x3 a, float3x4 b)
		{
			return math.float2x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00010E14 File Offset: 0x0000F014
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 mul(float2x4 a, float4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00010E74 File Offset: 0x0000F074
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 mul(float2x4 a, float4x2 b)
		{
			return math.float2x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00010F54 File Offset: 0x0000F154
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 mul(float2x4 a, float4x3 b)
		{
			return math.float2x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0001109C File Offset: 0x0000F29C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 mul(float2x4 a, float4x4 b)
		{
			return math.float2x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0001124A File Offset: 0x0000F44A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 mul(float3x2 a, float2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00011274 File Offset: 0x0000F474
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 mul(float3x2 a, float2x2 b)
		{
			return math.float3x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x000112E8 File Offset: 0x0000F4E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 mul(float3x2 a, float2x3 b)
		{
			return math.float3x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00011390 File Offset: 0x0000F590
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 mul(float3x2 a, float2x4 b)
		{
			return math.float3x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00011466 File Offset: 0x0000F666
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 mul(float3x3 a, float3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x000114A8 File Offset: 0x0000F6A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 mul(float3x3 a, float3x2 b)
		{
			return math.float3x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00011554 File Offset: 0x0000F754
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 mul(float3x3 a, float3x3 b)
		{
			return math.float3x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0001164C File Offset: 0x0000F84C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 mul(float3x3 a, float3x4 b)
		{
			return math.float3x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00011790 File Offset: 0x0000F990
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 mul(float3x4 a, float4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x000117F0 File Offset: 0x0000F9F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 mul(float3x4 a, float4x2 b)
		{
			return math.float3x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x000118D0 File Offset: 0x0000FAD0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 mul(float3x4 a, float4x3 b)
		{
			return math.float3x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00011A18 File Offset: 0x0000FC18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 mul(float3x4 a, float4x4 b)
		{
			return math.float3x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00011BC6 File Offset: 0x0000FDC6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 mul(float4x2 a, float2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00011BF0 File Offset: 0x0000FDF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 mul(float4x2 a, float2x2 b)
		{
			return math.float4x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00011C64 File Offset: 0x0000FE64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 mul(float4x2 a, float2x3 b)
		{
			return math.float4x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00011D0C File Offset: 0x0000FF0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 mul(float4x2 a, float2x4 b)
		{
			return math.float4x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00011DE2 File Offset: 0x0000FFE2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 mul(float4x3 a, float3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00011E24 File Offset: 0x00010024
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 mul(float4x3 a, float3x2 b)
		{
			return math.float4x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00011ED0 File Offset: 0x000100D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 mul(float4x3 a, float3x3 b)
		{
			return math.float4x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00011FC8 File Offset: 0x000101C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 mul(float4x3 a, float3x4 b)
		{
			return math.float4x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001210C File Offset: 0x0001030C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 mul(float4x4 a, float4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001216C File Offset: 0x0001036C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 mul(float4x4 a, float4x2 b)
		{
			return math.float4x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001224C File Offset: 0x0001044C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 mul(float4x4 a, float4x3 b)
		{
			return math.float4x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00012394 File Offset: 0x00010594
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 mul(float4x4 a, float4x4 b)
		{
			return math.float4x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00012542 File Offset: 0x00010742
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double mul(double a, double b)
		{
			return a * b;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00012547 File Offset: 0x00010747
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double mul(double2 a, double2 b)
		{
			return a.x * b.x + a.y * b.y;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00012564 File Offset: 0x00010764
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 mul(double2 a, double2x2 b)
		{
			return math.double2(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y);
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x000125C0 File Offset: 0x000107C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 mul(double2 a, double2x3 b)
		{
			return math.double3(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y, a.x * b.c2.x + a.y * b.c2.y);
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00012644 File Offset: 0x00010844
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 mul(double2 a, double2x4 b)
		{
			return math.double4(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y, a.x * b.c2.x + a.y * b.c2.y, a.x * b.c3.x + a.y * b.c3.y);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x000126EA File Offset: 0x000108EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double mul(double3 a, double3 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00012718 File Offset: 0x00010918
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 mul(double3 a, double3x2 b)
		{
			return math.double2(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001279C File Offset: 0x0001099C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 mul(double3 a, double3x3 b)
		{
			return math.double3(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00012858 File Offset: 0x00010A58
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 mul(double3 a, double3x4 b)
		{
			return math.double4(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z, a.x * b.c3.x + a.y * b.c3.y + a.z * b.c3.z);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001294A File Offset: 0x00010B4A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double mul(double4 a, double4 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00012984 File Offset: 0x00010B84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 mul(double4 a, double4x2 b)
		{
			return math.double2(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w);
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00012A2C File Offset: 0x00010C2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 mul(double4 a, double4x3 b)
		{
			return math.double3(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z + a.w * b.c2.w);
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00012B20 File Offset: 0x00010D20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 mul(double4 a, double4x4 b)
		{
			return math.double4(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z + a.w * b.c2.w, a.x * b.c3.x + a.y * b.c3.y + a.z * b.c3.z + a.w * b.c3.w);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00012C5E File Offset: 0x00010E5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 mul(double2x2 a, double2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00012C88 File Offset: 0x00010E88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 mul(double2x2 a, double2x2 b)
		{
			return math.double2x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00012CFC File Offset: 0x00010EFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 mul(double2x2 a, double2x3 b)
		{
			return math.double2x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00012DA4 File Offset: 0x00010FA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 mul(double2x2 a, double2x4 b)
		{
			return math.double2x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00012E7A File Offset: 0x0001107A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 mul(double2x3 a, double3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00012EBC File Offset: 0x000110BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 mul(double2x3 a, double3x2 b)
		{
			return math.double2x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00012F68 File Offset: 0x00011168
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 mul(double2x3 a, double3x3 b)
		{
			return math.double2x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00013060 File Offset: 0x00011260
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 mul(double2x3 a, double3x4 b)
		{
			return math.double2x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x000131A4 File Offset: 0x000113A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 mul(double2x4 a, double4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00013204 File Offset: 0x00011404
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 mul(double2x4 a, double4x2 b)
		{
			return math.double2x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x000132E4 File Offset: 0x000114E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 mul(double2x4 a, double4x3 b)
		{
			return math.double2x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001342C File Offset: 0x0001162C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 mul(double2x4 a, double4x4 b)
		{
			return math.double2x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x000135DA File Offset: 0x000117DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 mul(double3x2 a, double2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00013604 File Offset: 0x00011804
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 mul(double3x2 a, double2x2 b)
		{
			return math.double3x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00013678 File Offset: 0x00011878
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 mul(double3x2 a, double2x3 b)
		{
			return math.double3x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00013720 File Offset: 0x00011920
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 mul(double3x2 a, double2x4 b)
		{
			return math.double3x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x000137F6 File Offset: 0x000119F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 mul(double3x3 a, double3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00013838 File Offset: 0x00011A38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 mul(double3x3 a, double3x2 b)
		{
			return math.double3x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x000138E4 File Offset: 0x00011AE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 mul(double3x3 a, double3x3 b)
		{
			return math.double3x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x000139DC File Offset: 0x00011BDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 mul(double3x3 a, double3x4 b)
		{
			return math.double3x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00013B20 File Offset: 0x00011D20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 mul(double3x4 a, double4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00013B80 File Offset: 0x00011D80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 mul(double3x4 a, double4x2 b)
		{
			return math.double3x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00013C60 File Offset: 0x00011E60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 mul(double3x4 a, double4x3 b)
		{
			return math.double3x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00013DA8 File Offset: 0x00011FA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 mul(double3x4 a, double4x4 b)
		{
			return math.double3x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00013F56 File Offset: 0x00012156
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 mul(double4x2 a, double2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00013F80 File Offset: 0x00012180
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 mul(double4x2 a, double2x2 b)
		{
			return math.double4x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00013FF4 File Offset: 0x000121F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 mul(double4x2 a, double2x3 b)
		{
			return math.double4x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001409C File Offset: 0x0001229C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 mul(double4x2 a, double2x4 b)
		{
			return math.double4x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00014172 File Offset: 0x00012372
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 mul(double4x3 a, double3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x000141B4 File Offset: 0x000123B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 mul(double4x3 a, double3x2 b)
		{
			return math.double4x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00014260 File Offset: 0x00012460
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 mul(double4x3 a, double3x3 b)
		{
			return math.double4x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00014358 File Offset: 0x00012558
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 mul(double4x3 a, double3x4 b)
		{
			return math.double4x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001449C File Offset: 0x0001269C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 mul(double4x4 a, double4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x000144FC File Offset: 0x000126FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 mul(double4x4 a, double4x2 b)
		{
			return math.double4x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x000145DC File Offset: 0x000127DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 mul(double4x4 a, double4x3 b)
		{
			return math.double4x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00014724 File Offset: 0x00012924
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 mul(double4x4 a, double4x4 b)
		{
			return math.double4x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x000148D2 File Offset: 0x00012AD2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int mul(int a, int b)
		{
			return a * b;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x000148D7 File Offset: 0x00012AD7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int mul(int2 a, int2 b)
		{
			return a.x * b.x + a.y * b.y;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x000148F4 File Offset: 0x00012AF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 mul(int2 a, int2x2 b)
		{
			return math.int2(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00014950 File Offset: 0x00012B50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 mul(int2 a, int2x3 b)
		{
			return math.int3(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y, a.x * b.c2.x + a.y * b.c2.y);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x000149D4 File Offset: 0x00012BD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 mul(int2 a, int2x4 b)
		{
			return math.int4(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y, a.x * b.c2.x + a.y * b.c2.y, a.x * b.c3.x + a.y * b.c3.y);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00014A7A File Offset: 0x00012C7A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int mul(int3 a, int3 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00014AA8 File Offset: 0x00012CA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 mul(int3 a, int3x2 b)
		{
			return math.int2(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00014B2C File Offset: 0x00012D2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 mul(int3 a, int3x3 b)
		{
			return math.int3(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00014BE8 File Offset: 0x00012DE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 mul(int3 a, int3x4 b)
		{
			return math.int4(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z, a.x * b.c3.x + a.y * b.c3.y + a.z * b.c3.z);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00014CDA File Offset: 0x00012EDA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int mul(int4 a, int4 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00014D14 File Offset: 0x00012F14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 mul(int4 a, int4x2 b)
		{
			return math.int2(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00014DBC File Offset: 0x00012FBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 mul(int4 a, int4x3 b)
		{
			return math.int3(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z + a.w * b.c2.w);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00014EB0 File Offset: 0x000130B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 mul(int4 a, int4x4 b)
		{
			return math.int4(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z + a.w * b.c2.w, a.x * b.c3.x + a.y * b.c3.y + a.z * b.c3.z + a.w * b.c3.w);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00014FEE File Offset: 0x000131EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 mul(int2x2 a, int2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00015018 File Offset: 0x00013218
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 mul(int2x2 a, int2x2 b)
		{
			return math.int2x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001508C File Offset: 0x0001328C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 mul(int2x2 a, int2x3 b)
		{
			return math.int2x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00015134 File Offset: 0x00013334
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 mul(int2x2 a, int2x4 b)
		{
			return math.int2x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001520A File Offset: 0x0001340A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 mul(int2x3 a, int3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001524C File Offset: 0x0001344C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 mul(int2x3 a, int3x2 b)
		{
			return math.int2x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x000152F8 File Offset: 0x000134F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 mul(int2x3 a, int3x3 b)
		{
			return math.int2x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x000153F0 File Offset: 0x000135F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 mul(int2x3 a, int3x4 b)
		{
			return math.int2x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00015534 File Offset: 0x00013734
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 mul(int2x4 a, int4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00015594 File Offset: 0x00013794
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 mul(int2x4 a, int4x2 b)
		{
			return math.int2x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00015674 File Offset: 0x00013874
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 mul(int2x4 a, int4x3 b)
		{
			return math.int2x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x000157BC File Offset: 0x000139BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 mul(int2x4 a, int4x4 b)
		{
			return math.int2x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001596A File Offset: 0x00013B6A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 mul(int3x2 a, int2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00015994 File Offset: 0x00013B94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 mul(int3x2 a, int2x2 b)
		{
			return math.int3x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00015A08 File Offset: 0x00013C08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 mul(int3x2 a, int2x3 b)
		{
			return math.int3x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00015AB0 File Offset: 0x00013CB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 mul(int3x2 a, int2x4 b)
		{
			return math.int3x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00015B86 File Offset: 0x00013D86
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 mul(int3x3 a, int3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00015BC8 File Offset: 0x00013DC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 mul(int3x3 a, int3x2 b)
		{
			return math.int3x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00015C74 File Offset: 0x00013E74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 mul(int3x3 a, int3x3 b)
		{
			return math.int3x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00015D6C File Offset: 0x00013F6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 mul(int3x3 a, int3x4 b)
		{
			return math.int3x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00015EB0 File Offset: 0x000140B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 mul(int3x4 a, int4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00015F10 File Offset: 0x00014110
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 mul(int3x4 a, int4x2 b)
		{
			return math.int3x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00015FF0 File Offset: 0x000141F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 mul(int3x4 a, int4x3 b)
		{
			return math.int3x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00016138 File Offset: 0x00014338
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 mul(int3x4 a, int4x4 b)
		{
			return math.int3x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x000162E6 File Offset: 0x000144E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 mul(int4x2 a, int2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00016310 File Offset: 0x00014510
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 mul(int4x2 a, int2x2 b)
		{
			return math.int4x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00016384 File Offset: 0x00014584
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 mul(int4x2 a, int2x3 b)
		{
			return math.int4x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001642C File Offset: 0x0001462C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 mul(int4x2 a, int2x4 b)
		{
			return math.int4x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00016502 File Offset: 0x00014702
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 mul(int4x3 a, int3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00016544 File Offset: 0x00014744
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 mul(int4x3 a, int3x2 b)
		{
			return math.int4x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x000165F0 File Offset: 0x000147F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 mul(int4x3 a, int3x3 b)
		{
			return math.int4x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x000166E8 File Offset: 0x000148E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 mul(int4x3 a, int3x4 b)
		{
			return math.int4x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001682C File Offset: 0x00014A2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 mul(int4x4 a, int4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001688C File Offset: 0x00014A8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 mul(int4x4 a, int4x2 b)
		{
			return math.int4x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001696C File Offset: 0x00014B6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 mul(int4x4 a, int4x3 b)
		{
			return math.int4x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00016AB4 File Offset: 0x00014CB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 mul(int4x4 a, int4x4 b)
		{
			return math.int4x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00016C62 File Offset: 0x00014E62
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint mul(uint a, uint b)
		{
			return a * b;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00016C67 File Offset: 0x00014E67
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint mul(uint2 a, uint2 b)
		{
			return a.x * b.x + a.y * b.y;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00016C84 File Offset: 0x00014E84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 mul(uint2 a, uint2x2 b)
		{
			return math.uint2(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00016CE0 File Offset: 0x00014EE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 mul(uint2 a, uint2x3 b)
		{
			return math.uint3(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y, a.x * b.c2.x + a.y * b.c2.y);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00016D64 File Offset: 0x00014F64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 mul(uint2 a, uint2x4 b)
		{
			return math.uint4(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y, a.x * b.c2.x + a.y * b.c2.y, a.x * b.c3.x + a.y * b.c3.y);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00016E0A File Offset: 0x0001500A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint mul(uint3 a, uint3 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00016E38 File Offset: 0x00015038
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 mul(uint3 a, uint3x2 b)
		{
			return math.uint2(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00016EBC File Offset: 0x000150BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 mul(uint3 a, uint3x3 b)
		{
			return math.uint3(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00016F78 File Offset: 0x00015178
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 mul(uint3 a, uint3x4 b)
		{
			return math.uint4(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z, a.x * b.c3.x + a.y * b.c3.y + a.z * b.c3.z);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001706A File Offset: 0x0001526A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint mul(uint4 a, uint4 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x000170A4 File Offset: 0x000152A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 mul(uint4 a, uint4x2 b)
		{
			return math.uint2(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0001714C File Offset: 0x0001534C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 mul(uint4 a, uint4x3 b)
		{
			return math.uint3(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z + a.w * b.c2.w);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00017240 File Offset: 0x00015440
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 mul(uint4 a, uint4x4 b)
		{
			return math.uint4(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z + a.w * b.c2.w, a.x * b.c3.x + a.y * b.c3.y + a.z * b.c3.z + a.w * b.c3.w);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001737E File Offset: 0x0001557E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 mul(uint2x2 a, uint2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x000173A8 File Offset: 0x000155A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 mul(uint2x2 a, uint2x2 b)
		{
			return math.uint2x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001741C File Offset: 0x0001561C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 mul(uint2x2 a, uint2x3 b)
		{
			return math.uint2x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x000174C4 File Offset: 0x000156C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 mul(uint2x2 a, uint2x4 b)
		{
			return math.uint2x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001759A File Offset: 0x0001579A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 mul(uint2x3 a, uint3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x000175DC File Offset: 0x000157DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 mul(uint2x3 a, uint3x2 b)
		{
			return math.uint2x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00017688 File Offset: 0x00015888
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 mul(uint2x3 a, uint3x3 b)
		{
			return math.uint2x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00017780 File Offset: 0x00015980
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 mul(uint2x3 a, uint3x4 b)
		{
			return math.uint2x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x000178C4 File Offset: 0x00015AC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 mul(uint2x4 a, uint4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00017924 File Offset: 0x00015B24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 mul(uint2x4 a, uint4x2 b)
		{
			return math.uint2x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00017A04 File Offset: 0x00015C04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 mul(uint2x4 a, uint4x3 b)
		{
			return math.uint2x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00017B4C File Offset: 0x00015D4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 mul(uint2x4 a, uint4x4 b)
		{
			return math.uint2x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00017CFA File Offset: 0x00015EFA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 mul(uint3x2 a, uint2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00017D24 File Offset: 0x00015F24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 mul(uint3x2 a, uint2x2 b)
		{
			return math.uint3x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00017D98 File Offset: 0x00015F98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 mul(uint3x2 a, uint2x3 b)
		{
			return math.uint3x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00017E40 File Offset: 0x00016040
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 mul(uint3x2 a, uint2x4 b)
		{
			return math.uint3x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00017F16 File Offset: 0x00016116
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 mul(uint3x3 a, uint3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00017F58 File Offset: 0x00016158
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 mul(uint3x3 a, uint3x2 b)
		{
			return math.uint3x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00018004 File Offset: 0x00016204
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 mul(uint3x3 a, uint3x3 b)
		{
			return math.uint3x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x000180FC File Offset: 0x000162FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 mul(uint3x3 a, uint3x4 b)
		{
			return math.uint3x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00018240 File Offset: 0x00016440
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 mul(uint3x4 a, uint4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x000182A0 File Offset: 0x000164A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 mul(uint3x4 a, uint4x2 b)
		{
			return math.uint3x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00018380 File Offset: 0x00016580
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 mul(uint3x4 a, uint4x3 b)
		{
			return math.uint3x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x000184C8 File Offset: 0x000166C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 mul(uint3x4 a, uint4x4 b)
		{
			return math.uint3x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00018676 File Offset: 0x00016876
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 mul(uint4x2 a, uint2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x000186A0 File Offset: 0x000168A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 mul(uint4x2 a, uint2x2 b)
		{
			return math.uint4x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00018714 File Offset: 0x00016914
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 mul(uint4x2 a, uint2x3 b)
		{
			return math.uint4x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x000187BC File Offset: 0x000169BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 mul(uint4x2 a, uint2x4 b)
		{
			return math.uint4x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00018892 File Offset: 0x00016A92
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 mul(uint4x3 a, uint3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x000188D4 File Offset: 0x00016AD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 mul(uint4x3 a, uint3x2 b)
		{
			return math.uint4x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00018980 File Offset: 0x00016B80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 mul(uint4x3 a, uint3x3 b)
		{
			return math.uint4x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00018A78 File Offset: 0x00016C78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 mul(uint4x3 a, uint3x4 b)
		{
			return math.uint4x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00018BBC File Offset: 0x00016DBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 mul(uint4x4 a, uint4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00018C1C File Offset: 0x00016E1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 mul(uint4x4 a, uint4x2 b)
		{
			return math.uint4x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00018CFC File Offset: 0x00016EFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 mul(uint4x4 a, uint4x3 b)
		{
			return math.uint4x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00018E44 File Offset: 0x00017044
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 mul(uint4x4 a, uint4x4 b)
		{
			return math.uint4x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00018FF2 File Offset: 0x000171F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion quaternion(float x, float y, float z, float w)
		{
			return new quaternion(x, y, z, w);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00018FFD File Offset: 0x000171FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion quaternion(float4 value)
		{
			return new quaternion(value);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00019005 File Offset: 0x00017205
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion quaternion(float3x3 m)
		{
			return new quaternion(m);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001900D File Offset: 0x0001720D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion quaternion(float4x4 m)
		{
			return new quaternion(m);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00019015 File Offset: 0x00017215
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion conjugate(quaternion q)
		{
			return math.quaternion(q.value * math.float4(-1f, -1f, -1f, 1f));
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00019040 File Offset: 0x00017240
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion inverse(quaternion q)
		{
			float4 value = q.value;
			return math.quaternion(math.rcp(math.dot(value, value)) * value * math.float4(-1f, -1f, -1f, 1f));
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00019089 File Offset: 0x00017289
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float dot(quaternion a, quaternion b)
		{
			return math.dot(a.value, b.value);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001909C File Offset: 0x0001729C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float length(quaternion q)
		{
			return math.sqrt(math.dot(q.value, q.value));
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x000190B4 File Offset: 0x000172B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float lengthsq(quaternion q)
		{
			return math.dot(q.value, q.value);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x000190C8 File Offset: 0x000172C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion normalize(quaternion q)
		{
			float4 value = q.value;
			return math.quaternion(math.rsqrt(math.dot(value, value)) * value);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x000190F4 File Offset: 0x000172F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion normalizesafe(quaternion q)
		{
			float4 value = q.value;
			float num = math.dot(value, value);
			return math.quaternion(math.select(Unity.Mathematics.quaternion.identity.value, value * math.rsqrt(num), num > 1.1754944E-38f));
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00019138 File Offset: 0x00017338
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion normalizesafe(quaternion q, quaternion defaultvalue)
		{
			float4 value = q.value;
			float num = math.dot(value, value);
			return math.quaternion(math.select(defaultvalue.value, value * math.rsqrt(num), num > 1.1754944E-38f));
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00019178 File Offset: 0x00017378
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion unitexp(quaternion q)
		{
			float num = math.rsqrt(math.dot(q.value.xyz, q.value.xyz));
			float rhs;
			float w;
			math.sincos(math.rcp(num), out rhs, out w);
			return math.quaternion(math.float4(q.value.xyz * num * rhs, w));
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x000191DC File Offset: 0x000173DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion exp(quaternion q)
		{
			float num = math.rsqrt(math.dot(q.value.xyz, q.value.xyz));
			float rhs;
			float w;
			math.sincos(math.rcp(num), out rhs, out w);
			return math.quaternion(math.float4(q.value.xyz * num * rhs, w) * math.exp(q.value.w));
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00019254 File Offset: 0x00017454
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion unitlog(quaternion q)
		{
			float num = math.clamp(q.value.w, -1f, 1f);
			float rhs = math.acos(num) * math.rsqrt(1f - num * num);
			return math.quaternion(math.float4(q.value.xyz * rhs, 0f));
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x000192B4 File Offset: 0x000174B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion log(quaternion q)
		{
			float num = math.dot(q.value.xyz, q.value.xyz);
			float x = num + q.value.w * q.value.w;
			float rhs = math.acos(math.clamp(q.value.w * math.rsqrt(x), -1f, 1f)) * math.rsqrt(num);
			return math.quaternion(math.float4(q.value.xyz * rhs, 0.5f * math.log(x)));
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00019350 File Offset: 0x00017550
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion mul(quaternion a, quaternion b)
		{
			return math.quaternion(a.value.wwww * b.value + (a.value.xyzx * b.value.wwwx + a.value.yzxy * b.value.zxyy) * math.float4(1f, 1f, 1f, -1f) - a.value.zxyz * b.value.yzxz);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00019400 File Offset: 0x00017600
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 mul(quaternion q, float3 v)
		{
			float3 @float = 2f * math.cross(q.value.xyz, v);
			return v + q.value.w * @float + math.cross(q.value.xyz, @float);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00019458 File Offset: 0x00017658
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 rotate(quaternion q, float3 v)
		{
			float3 @float = 2f * math.cross(q.value.xyz, v);
			return v + q.value.w * @float + math.cross(q.value.xyz, @float);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x000194B0 File Offset: 0x000176B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion nlerp(quaternion q1, quaternion q2, float t)
		{
			if (math.dot(q1, q2) < 0f)
			{
				q2.value = -q2.value;
			}
			return math.normalize(math.quaternion(math.lerp(q1.value, q2.value, t)));
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x000194F0 File Offset: 0x000176F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion slerp(quaternion q1, quaternion q2, float t)
		{
			float num = math.dot(q1, q2);
			if (num < 0f)
			{
				num = -num;
				q2.value = -q2.value;
			}
			if (num < 0.9995f)
			{
				float num2 = math.acos(num);
				float num3 = math.rsqrt(1f - num * num);
				float rhs = math.sin(num2 * (1f - t)) * num3;
				float rhs2 = math.sin(num2 * t) * num3;
				return math.quaternion(q1.value * rhs + q2.value * rhs2);
			}
			return math.nlerp(q1, q2, t);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00019585 File Offset: 0x00017785
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(quaternion q)
		{
			return math.hash(q.value);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00019592 File Offset: 0x00017792
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(quaternion q)
		{
			return math.hashwide(q.value);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001959F File Offset: 0x0001779F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 forward(quaternion q)
		{
			return math.mul(q, math.float3(0f, 0f, 1f));
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x000195BB File Offset: 0x000177BB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform RigidTransform(quaternion rot, float3 pos)
		{
			return new RigidTransform(rot, pos);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x000195C4 File Offset: 0x000177C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform RigidTransform(float3x3 rotation, float3 translation)
		{
			return new RigidTransform(rotation, translation);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x000195CD File Offset: 0x000177CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform RigidTransform(float4x4 transform)
		{
			return new RigidTransform(transform);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x000195D8 File Offset: 0x000177D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform inverse(RigidTransform t)
		{
			quaternion quaternion = math.inverse(t.rot);
			float3 translation = math.mul(quaternion, -t.pos);
			return new RigidTransform(quaternion, translation);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00019608 File Offset: 0x00017808
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform mul(RigidTransform a, RigidTransform b)
		{
			return new RigidTransform(math.mul(a.rot, b.rot), math.mul(a.rot, b.pos) + a.pos);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001963C File Offset: 0x0001783C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 mul(RigidTransform a, float4 pos)
		{
			return math.float4(math.mul(a.rot, pos.xyz) + a.pos * pos.w, pos.w);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00019671 File Offset: 0x00017871
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 rotate(RigidTransform a, float3 dir)
		{
			return math.mul(a.rot, dir);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001967F File Offset: 0x0001787F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 transform(RigidTransform a, float3 pos)
		{
			return math.mul(a.rot, pos) + a.pos;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00019698 File Offset: 0x00017898
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(RigidTransform t)
		{
			return math.hash(t.rot) + 3318036811U * math.hash(t.pos);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x000196B8 File Offset: 0x000178B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(RigidTransform t)
		{
			return math.hashwide(t.rot) + 3318036811U * math.hashwide(t.pos).xyzz;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x000196F2 File Offset: 0x000178F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 uint2(uint x, uint y)
		{
			return new uint2(x, y);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x000196FB File Offset: 0x000178FB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 uint2(uint2 xy)
		{
			return new uint2(xy);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00019703 File Offset: 0x00017903
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 uint2(uint v)
		{
			return new uint2(v);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001970B File Offset: 0x0001790B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 uint2(bool v)
		{
			return new uint2(v);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00019713 File Offset: 0x00017913
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 uint2(bool2 v)
		{
			return new uint2(v);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001971B File Offset: 0x0001791B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 uint2(int v)
		{
			return new uint2(v);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00019723 File Offset: 0x00017923
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 uint2(int2 v)
		{
			return new uint2(v);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001972B File Offset: 0x0001792B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 uint2(float v)
		{
			return new uint2(v);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00019733 File Offset: 0x00017933
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 uint2(float2 v)
		{
			return new uint2(v);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001973B File Offset: 0x0001793B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 uint2(double v)
		{
			return new uint2(v);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00019743 File Offset: 0x00017943
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 uint2(double2 v)
		{
			return new uint2(v);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001974B File Offset: 0x0001794B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(uint2 v)
		{
			return math.csum(v * math.uint2(1148435377U, 3416333663U)) + 1750611407U;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001976D File Offset: 0x0001796D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(uint2 v)
		{
			return v * math.uint2(3285396193U, 3110507567U) + 4271396531U;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001978E File Offset: 0x0001798E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint shuffle(uint2 left, uint2 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00019798 File Offset: 0x00017998
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 shuffle(uint2 left, uint2 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.uint2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x000197AF File Offset: 0x000179AF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 shuffle(uint2 left, uint2 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.uint3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x000197CF File Offset: 0x000179CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 shuffle(uint2 left, uint2 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.uint4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x000197F8 File Offset: 0x000179F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static uint select_shuffle_component(uint2 a, uint2 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			}
			throw new ArgumentException("Invalid shuffle component: " + component.ToString());
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001985D File Offset: 0x00017A5D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 uint2x2(uint2 c0, uint2 c1)
		{
			return new uint2x2(c0, c1);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00019866 File Offset: 0x00017A66
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 uint2x2(uint m00, uint m01, uint m10, uint m11)
		{
			return new uint2x2(m00, m01, m10, m11);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00019871 File Offset: 0x00017A71
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 uint2x2(uint v)
		{
			return new uint2x2(v);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00019879 File Offset: 0x00017A79
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 uint2x2(bool v)
		{
			return new uint2x2(v);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00019881 File Offset: 0x00017A81
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 uint2x2(bool2x2 v)
		{
			return new uint2x2(v);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00019889 File Offset: 0x00017A89
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 uint2x2(int v)
		{
			return new uint2x2(v);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00019891 File Offset: 0x00017A91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 uint2x2(int2x2 v)
		{
			return new uint2x2(v);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00019899 File Offset: 0x00017A99
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 uint2x2(float v)
		{
			return new uint2x2(v);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x000198A1 File Offset: 0x00017AA1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 uint2x2(float2x2 v)
		{
			return new uint2x2(v);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x000198A9 File Offset: 0x00017AA9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 uint2x2(double v)
		{
			return new uint2x2(v);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x000198B1 File Offset: 0x00017AB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 uint2x2(double2x2 v)
		{
			return new uint2x2(v);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x000198B9 File Offset: 0x00017AB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 transpose(uint2x2 v)
		{
			return math.uint2x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x000198EC File Offset: 0x00017AEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(uint2x2 v)
		{
			return math.csum(v.c0 * math.uint2(3010324327U, 1875523709U) + v.c1 * math.uint2(2937008387U, 3835713223U)) + 2216526373U;
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00019940 File Offset: 0x00017B40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(uint2x2 v)
		{
			return v.c0 * math.uint2(3375971453U, 3559829411U) + v.c1 * math.uint2(3652178029U, 2544260129U) + 2013864031U;
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00019990 File Offset: 0x00017B90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 uint2x3(uint2 c0, uint2 c1, uint2 c2)
		{
			return new uint2x3(c0, c1, c2);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001999A File Offset: 0x00017B9A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 uint2x3(uint m00, uint m01, uint m02, uint m10, uint m11, uint m12)
		{
			return new uint2x3(m00, m01, m02, m10, m11, m12);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x000199A9 File Offset: 0x00017BA9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 uint2x3(uint v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x000199B1 File Offset: 0x00017BB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 uint2x3(bool v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x000199B9 File Offset: 0x00017BB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 uint2x3(bool2x3 v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x000199C1 File Offset: 0x00017BC1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 uint2x3(int v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x000199C9 File Offset: 0x00017BC9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 uint2x3(int2x3 v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x000199D1 File Offset: 0x00017BD1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 uint2x3(float v)
		{
			return new uint2x3(v);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x000199D9 File Offset: 0x00017BD9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 uint2x3(float2x3 v)
		{
			return new uint2x3(v);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x000199E1 File Offset: 0x00017BE1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 uint2x3(double v)
		{
			return new uint2x3(v);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x000199E9 File Offset: 0x00017BE9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 uint2x3(double2x3 v)
		{
			return new uint2x3(v);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x000199F4 File Offset: 0x00017BF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 transpose(uint2x3 v)
		{
			return math.uint3x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00019A48 File Offset: 0x00017C48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(uint2x3 v)
		{
			return math.csum(v.c0 * math.uint2(4016293529U, 2416021567U) + v.c1 * math.uint2(2828384717U, 2636362241U) + v.c2 * math.uint2(1258410977U, 1952565773U)) + 2037535609U;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00019AB8 File Offset: 0x00017CB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(uint2x3 v)
		{
			return v.c0 * math.uint2(3592785499U, 3996716183U) + v.c1 * math.uint2(2626301701U, 1306289417U) + v.c2 * math.uint2(2096137163U, 1548578029U) + 4178800919U;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00019B27 File Offset: 0x00017D27
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 uint2x4(uint2 c0, uint2 c1, uint2 c2, uint2 c3)
		{
			return new uint2x4(c0, c1, c2, c3);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00019B32 File Offset: 0x00017D32
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 uint2x4(uint m00, uint m01, uint m02, uint m03, uint m10, uint m11, uint m12, uint m13)
		{
			return new uint2x4(m00, m01, m02, m03, m10, m11, m12, m13);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00019B45 File Offset: 0x00017D45
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 uint2x4(uint v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00019B4D File Offset: 0x00017D4D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 uint2x4(bool v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00019B55 File Offset: 0x00017D55
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 uint2x4(bool2x4 v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00019B5D File Offset: 0x00017D5D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 uint2x4(int v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00019B65 File Offset: 0x00017D65
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 uint2x4(int2x4 v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00019B6D File Offset: 0x00017D6D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 uint2x4(float v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00019B75 File Offset: 0x00017D75
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 uint2x4(float2x4 v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00019B7D File Offset: 0x00017D7D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 uint2x4(double v)
		{
			return new uint2x4(v);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00019B85 File Offset: 0x00017D85
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 uint2x4(double2x4 v)
		{
			return new uint2x4(v);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00019B90 File Offset: 0x00017D90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 transpose(uint2x4 v)
		{
			return math.uint4x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y, v.c3.x, v.c3.y);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00019BFC File Offset: 0x00017DFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(uint2x4 v)
		{
			return math.csum(v.c0 * math.uint2(2650080659U, 4052675461U) + v.c1 * math.uint2(2652487619U, 2174136431U) + v.c2 * math.uint2(3528391193U, 2105559227U) + v.c3 * math.uint2(1899745391U, 1966790317U)) + 3516359879U;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00019C8C File Offset: 0x00017E8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(uint2x4 v)
		{
			return v.c0 * math.uint2(3050356579U, 4178586719U) + v.c1 * math.uint2(2558655391U, 1453413133U) + v.c2 * math.uint2(2152428077U, 1938706661U) + v.c3 * math.uint2(1338588197U, 3439609253U) + 3535343003U;
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00019D1A File Offset: 0x00017F1A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 uint3(uint x, uint y, uint z)
		{
			return new uint3(x, y, z);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00019D24 File Offset: 0x00017F24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 uint3(uint x, uint2 yz)
		{
			return new uint3(x, yz);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00019D2D File Offset: 0x00017F2D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 uint3(uint2 xy, uint z)
		{
			return new uint3(xy, z);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00019D36 File Offset: 0x00017F36
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 uint3(uint3 xyz)
		{
			return new uint3(xyz);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00019D3E File Offset: 0x00017F3E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 uint3(uint v)
		{
			return new uint3(v);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00019D46 File Offset: 0x00017F46
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 uint3(bool v)
		{
			return new uint3(v);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00019D4E File Offset: 0x00017F4E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 uint3(bool3 v)
		{
			return new uint3(v);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00019D56 File Offset: 0x00017F56
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 uint3(int v)
		{
			return new uint3(v);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00019D5E File Offset: 0x00017F5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 uint3(int3 v)
		{
			return new uint3(v);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00019D66 File Offset: 0x00017F66
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 uint3(float v)
		{
			return new uint3(v);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00019D6E File Offset: 0x00017F6E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 uint3(float3 v)
		{
			return new uint3(v);
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00019D76 File Offset: 0x00017F76
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 uint3(double v)
		{
			return new uint3(v);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00019D7E File Offset: 0x00017F7E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 uint3(double3 v)
		{
			return new uint3(v);
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00019D86 File Offset: 0x00017F86
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(uint3 v)
		{
			return math.csum(v * math.uint3(3441847433U, 4052036147U, 2011389559U)) + 2252224297U;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00019DAD File Offset: 0x00017FAD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(uint3 v)
		{
			return v * math.uint3(3784421429U, 1750626223U, 3571447507U) + 3412283213U;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00019DD3 File Offset: 0x00017FD3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint shuffle(uint3 left, uint3 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00019DDD File Offset: 0x00017FDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 shuffle(uint3 left, uint3 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.uint2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00019DF4 File Offset: 0x00017FF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 shuffle(uint3 left, uint3 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.uint3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00019E14 File Offset: 0x00018014
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 shuffle(uint3 left, uint3 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.uint4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00019E40 File Offset: 0x00018040
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static uint select_shuffle_component(uint3 a, uint3 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.LeftZ:
				return a.z;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			case math.ShuffleComponent.RightZ:
				return b.z;
			}
			throw new ArgumentException("Invalid shuffle component: " + component.ToString());
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00019EB7 File Offset: 0x000180B7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 uint3x2(uint3 c0, uint3 c1)
		{
			return new uint3x2(c0, c1);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00019EC0 File Offset: 0x000180C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 uint3x2(uint m00, uint m01, uint m10, uint m11, uint m20, uint m21)
		{
			return new uint3x2(m00, m01, m10, m11, m20, m21);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00019ECF File Offset: 0x000180CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 uint3x2(uint v)
		{
			return new uint3x2(v);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00019ED7 File Offset: 0x000180D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 uint3x2(bool v)
		{
			return new uint3x2(v);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00019EDF File Offset: 0x000180DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 uint3x2(bool3x2 v)
		{
			return new uint3x2(v);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00019EE7 File Offset: 0x000180E7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 uint3x2(int v)
		{
			return new uint3x2(v);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00019EEF File Offset: 0x000180EF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 uint3x2(int3x2 v)
		{
			return new uint3x2(v);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00019EF7 File Offset: 0x000180F7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 uint3x2(float v)
		{
			return new uint3x2(v);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00019EFF File Offset: 0x000180FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 uint3x2(float3x2 v)
		{
			return new uint3x2(v);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00019F07 File Offset: 0x00018107
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 uint3x2(double v)
		{
			return new uint3x2(v);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00019F0F File Offset: 0x0001810F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 uint3x2(double3x2 v)
		{
			return new uint3x2(v);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00019F18 File Offset: 0x00018118
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 transpose(uint3x2 v)
		{
			return math.uint2x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00019F6C File Offset: 0x0001816C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(uint3x2 v)
		{
			return math.csum(v.c0 * math.uint3(1365086453U, 3969870067U, 4192899797U) + v.c1 * math.uint3(3271228601U, 1634639009U, 3318036811U)) + 3404170631U;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00019FC8 File Offset: 0x000181C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(uint3x2 v)
		{
			return v.c0 * math.uint3(2048213449U, 4164671783U, 1780759499U) + v.c1 * math.uint3(1352369353U, 2446407751U, 1391928079U) + 3475533443U;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0001A022 File Offset: 0x00018222
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 uint3x3(uint3 c0, uint3 c1, uint3 c2)
		{
			return new uint3x3(c0, c1, c2);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0001A02C File Offset: 0x0001822C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 uint3x3(uint m00, uint m01, uint m02, uint m10, uint m11, uint m12, uint m20, uint m21, uint m22)
		{
			return new uint3x3(m00, m01, m02, m10, m11, m12, m20, m21, m22);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0001A04C File Offset: 0x0001824C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 uint3x3(uint v)
		{
			return new uint3x3(v);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0001A054 File Offset: 0x00018254
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 uint3x3(bool v)
		{
			return new uint3x3(v);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0001A05C File Offset: 0x0001825C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 uint3x3(bool3x3 v)
		{
			return new uint3x3(v);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0001A064 File Offset: 0x00018264
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 uint3x3(int v)
		{
			return new uint3x3(v);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001A06C File Offset: 0x0001826C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 uint3x3(int3x3 v)
		{
			return new uint3x3(v);
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001A074 File Offset: 0x00018274
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 uint3x3(float v)
		{
			return new uint3x3(v);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0001A07C File Offset: 0x0001827C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 uint3x3(float3x3 v)
		{
			return new uint3x3(v);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001A084 File Offset: 0x00018284
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 uint3x3(double v)
		{
			return new uint3x3(v);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001A08C File Offset: 0x0001828C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 uint3x3(double3x3 v)
		{
			return new uint3x3(v);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001A094 File Offset: 0x00018294
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 transpose(uint3x3 v)
		{
			return math.uint3x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001A10C File Offset: 0x0001830C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(uint3x3 v)
		{
			return math.csum(v.c0 * math.uint3(2892026051U, 2455987759U, 3868600063U) + v.c1 * math.uint3(3170963179U, 2632835537U, 1136528209U) + v.c2 * math.uint3(2944626401U, 2972762423U, 1417889653U)) + 2080514593U;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001A18C File Offset: 0x0001838C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(uint3x3 v)
		{
			return v.c0 * math.uint3(2731544287U, 2828498809U, 2669441947U) + v.c1 * math.uint3(1260114311U, 2650080659U, 4052675461U) + v.c2 * math.uint3(2652487619U, 2174136431U, 3528391193U) + 2105559227U;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0001A20A File Offset: 0x0001840A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 uint3x4(uint3 c0, uint3 c1, uint3 c2, uint3 c3)
		{
			return new uint3x4(c0, c1, c2, c3);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001A218 File Offset: 0x00018418
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 uint3x4(uint m00, uint m01, uint m02, uint m03, uint m10, uint m11, uint m12, uint m13, uint m20, uint m21, uint m22, uint m23)
		{
			return new uint3x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001A23E File Offset: 0x0001843E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 uint3x4(uint v)
		{
			return new uint3x4(v);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001A246 File Offset: 0x00018446
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 uint3x4(bool v)
		{
			return new uint3x4(v);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001A24E File Offset: 0x0001844E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 uint3x4(bool3x4 v)
		{
			return new uint3x4(v);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001A256 File Offset: 0x00018456
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 uint3x4(int v)
		{
			return new uint3x4(v);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0001A25E File Offset: 0x0001845E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 uint3x4(int3x4 v)
		{
			return new uint3x4(v);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0001A266 File Offset: 0x00018466
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 uint3x4(float v)
		{
			return new uint3x4(v);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001A26E File Offset: 0x0001846E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 uint3x4(float3x4 v)
		{
			return new uint3x4(v);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0001A276 File Offset: 0x00018476
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 uint3x4(double v)
		{
			return new uint3x4(v);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001A27E File Offset: 0x0001847E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 uint3x4(double3x4 v)
		{
			return new uint3x4(v);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001A288 File Offset: 0x00018488
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 transpose(uint3x4 v)
		{
			return math.uint4x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z, v.c3.x, v.c3.y, v.c3.z);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0001A320 File Offset: 0x00018520
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(uint3x4 v)
		{
			return math.csum(v.c0 * math.uint3(3508684087U, 3919501043U, 1209161033U) + v.c1 * math.uint3(4007793211U, 3819806693U, 3458005183U) + v.c2 * math.uint3(2078515003U, 4206465343U, 3025146473U) + v.c3 * math.uint3(3763046909U, 3678265601U, 2070747979U)) + 1480171127U;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0001A3C4 File Offset: 0x000185C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(uint3x4 v)
		{
			return v.c0 * math.uint3(1588341193U, 4234155257U, 1811310911U) + v.c1 * math.uint3(2635799963U, 4165137857U, 2759770933U) + v.c2 * math.uint3(2759319383U, 3299952959U, 3121178323U) + v.c3 * math.uint3(2948522579U, 1531026433U, 1365086453U) + 3969870067U;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0001A466 File Offset: 0x00018666
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(uint x, uint y, uint z, uint w)
		{
			return new uint4(x, y, z, w);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0001A471 File Offset: 0x00018671
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(uint x, uint y, uint2 zw)
		{
			return new uint4(x, y, zw);
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001A47B File Offset: 0x0001867B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(uint x, uint2 yz, uint w)
		{
			return new uint4(x, yz, w);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001A485 File Offset: 0x00018685
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(uint x, uint3 yzw)
		{
			return new uint4(x, yzw);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001A48E File Offset: 0x0001868E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(uint2 xy, uint z, uint w)
		{
			return new uint4(xy, z, w);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0001A498 File Offset: 0x00018698
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(uint2 xy, uint2 zw)
		{
			return new uint4(xy, zw);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0001A4A1 File Offset: 0x000186A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(uint3 xyz, uint w)
		{
			return new uint4(xyz, w);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0001A4AA File Offset: 0x000186AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(uint4 xyzw)
		{
			return new uint4(xyzw);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0001A4B2 File Offset: 0x000186B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(uint v)
		{
			return new uint4(v);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001A4BA File Offset: 0x000186BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(bool v)
		{
			return new uint4(v);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001A4C2 File Offset: 0x000186C2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(bool4 v)
		{
			return new uint4(v);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001A4CA File Offset: 0x000186CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(int v)
		{
			return new uint4(v);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001A4D2 File Offset: 0x000186D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(int4 v)
		{
			return new uint4(v);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001A4DA File Offset: 0x000186DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(float v)
		{
			return new uint4(v);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001A4E2 File Offset: 0x000186E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(float4 v)
		{
			return new uint4(v);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0001A4EA File Offset: 0x000186EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(double v)
		{
			return new uint4(v);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001A4F2 File Offset: 0x000186F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(double4 v)
		{
			return new uint4(v);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001A4FA File Offset: 0x000186FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(uint4 v)
		{
			return math.csum(v * math.uint4(3029516053U, 3547472099U, 2057487037U, 3781937309U)) + 2057338067U;
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0001A526 File Offset: 0x00018726
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(uint4 v)
		{
			return v * math.uint4(2942577577U, 2834440507U, 2671762487U, 2892026051U) + 2455987759U;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0001A551 File Offset: 0x00018751
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint shuffle(uint4 left, uint4 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0001A55B File Offset: 0x0001875B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 shuffle(uint4 left, uint4 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.uint2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001A572 File Offset: 0x00018772
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 shuffle(uint4 left, uint4 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.uint3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001A592 File Offset: 0x00018792
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 shuffle(uint4 left, uint4 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.uint4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0001A5BC File Offset: 0x000187BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static uint select_shuffle_component(uint4 a, uint4 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.LeftZ:
				return a.z;
			case math.ShuffleComponent.LeftW:
				return a.w;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			case math.ShuffleComponent.RightZ:
				return b.z;
			case math.ShuffleComponent.RightW:
				return b.w;
			default:
				throw new ArgumentException("Invalid shuffle component: " + component.ToString());
			}
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0001A645 File Offset: 0x00018845
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 uint4x2(uint4 c0, uint4 c1)
		{
			return new uint4x2(c0, c1);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001A64E File Offset: 0x0001884E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 uint4x2(uint m00, uint m01, uint m10, uint m11, uint m20, uint m21, uint m30, uint m31)
		{
			return new uint4x2(m00, m01, m10, m11, m20, m21, m30, m31);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001A661 File Offset: 0x00018861
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 uint4x2(uint v)
		{
			return new uint4x2(v);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001A669 File Offset: 0x00018869
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 uint4x2(bool v)
		{
			return new uint4x2(v);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001A671 File Offset: 0x00018871
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 uint4x2(bool4x2 v)
		{
			return new uint4x2(v);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001A679 File Offset: 0x00018879
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 uint4x2(int v)
		{
			return new uint4x2(v);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001A681 File Offset: 0x00018881
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 uint4x2(int4x2 v)
		{
			return new uint4x2(v);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001A689 File Offset: 0x00018889
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 uint4x2(float v)
		{
			return new uint4x2(v);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001A691 File Offset: 0x00018891
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 uint4x2(float4x2 v)
		{
			return new uint4x2(v);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001A699 File Offset: 0x00018899
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 uint4x2(double v)
		{
			return new uint4x2(v);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001A6A1 File Offset: 0x000188A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 uint4x2(double4x2 v)
		{
			return new uint4x2(v);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0001A6AC File Offset: 0x000188AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 transpose(uint4x2 v)
		{
			return math.uint2x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001A718 File Offset: 0x00018918
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(uint4x2 v)
		{
			return math.csum(v.c0 * math.uint4(4198118021U, 2908068253U, 3705492289U, 2497566569U) + v.c1 * math.uint4(2716413241U, 1166264321U, 2503385333U, 2944493077U)) + 2599999021U;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001A780 File Offset: 0x00018980
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(uint4x2 v)
		{
			return v.c0 * math.uint4(3814721321U, 1595355149U, 1728931849U, 2062756937U) + v.c1 * math.uint4(2920485769U, 1562056283U, 2265541847U, 1283419601U) + 1210229737U;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001A7E4 File Offset: 0x000189E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 uint4x3(uint4 c0, uint4 c1, uint4 c2)
		{
			return new uint4x3(c0, c1, c2);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001A7F0 File Offset: 0x000189F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 uint4x3(uint m00, uint m01, uint m02, uint m10, uint m11, uint m12, uint m20, uint m21, uint m22, uint m30, uint m31, uint m32)
		{
			return new uint4x3(m00, m01, m02, m10, m11, m12, m20, m21, m22, m30, m31, m32);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0001A816 File Offset: 0x00018A16
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 uint4x3(uint v)
		{
			return new uint4x3(v);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0001A81E File Offset: 0x00018A1E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 uint4x3(bool v)
		{
			return new uint4x3(v);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0001A826 File Offset: 0x00018A26
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 uint4x3(bool4x3 v)
		{
			return new uint4x3(v);
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0001A82E File Offset: 0x00018A2E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 uint4x3(int v)
		{
			return new uint4x3(v);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0001A836 File Offset: 0x00018A36
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 uint4x3(int4x3 v)
		{
			return new uint4x3(v);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0001A83E File Offset: 0x00018A3E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 uint4x3(float v)
		{
			return new uint4x3(v);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0001A846 File Offset: 0x00018A46
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 uint4x3(float4x3 v)
		{
			return new uint4x3(v);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0001A84E File Offset: 0x00018A4E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 uint4x3(double v)
		{
			return new uint4x3(v);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001A856 File Offset: 0x00018A56
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 uint4x3(double4x3 v)
		{
			return new uint4x3(v);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0001A860 File Offset: 0x00018A60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 transpose(uint4x3 v)
		{
			return math.uint3x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0001A8F8 File Offset: 0x00018AF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(uint4x3 v)
		{
			return math.csum(v.c0 * math.uint4(3881277847U, 4017968839U, 1727237899U, 1648514723U) + v.c1 * math.uint4(1385344481U, 3538260197U, 4066109527U, 2613148903U) + v.c2 * math.uint4(3367528529U, 1678332449U, 2918459647U, 2744611081U)) + 1952372791U;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0001A988 File Offset: 0x00018B88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(uint4x3 v)
		{
			return v.c0 * math.uint4(2631698677U, 4200781601U, 2119021007U, 1760485621U) + v.c1 * math.uint4(3157985881U, 2171534173U, 2723054263U, 1168253063U) + v.c2 * math.uint4(4228926523U, 1610574617U, 1584185147U, 3041325733U) + 3150930919U;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001AA15 File Offset: 0x00018C15
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 uint4x4(uint4 c0, uint4 c1, uint4 c2, uint4 c3)
		{
			return new uint4x4(c0, c1, c2, c3);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0001AA20 File Offset: 0x00018C20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 uint4x4(uint m00, uint m01, uint m02, uint m03, uint m10, uint m11, uint m12, uint m13, uint m20, uint m21, uint m22, uint m23, uint m30, uint m31, uint m32, uint m33)
		{
			return new uint4x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23, m30, m31, m32, m33);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0001AA4E File Offset: 0x00018C4E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 uint4x4(uint v)
		{
			return new uint4x4(v);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001AA56 File Offset: 0x00018C56
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 uint4x4(bool v)
		{
			return new uint4x4(v);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001AA5E File Offset: 0x00018C5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 uint4x4(bool4x4 v)
		{
			return new uint4x4(v);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001AA66 File Offset: 0x00018C66
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 uint4x4(int v)
		{
			return new uint4x4(v);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001AA6E File Offset: 0x00018C6E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 uint4x4(int4x4 v)
		{
			return new uint4x4(v);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0001AA76 File Offset: 0x00018C76
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 uint4x4(float v)
		{
			return new uint4x4(v);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001AA7E File Offset: 0x00018C7E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 uint4x4(float4x4 v)
		{
			return new uint4x4(v);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0001AA86 File Offset: 0x00018C86
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 uint4x4(double v)
		{
			return new uint4x4(v);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0001AA8E File Offset: 0x00018C8E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 uint4x4(double4x4 v)
		{
			return new uint4x4(v);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0001AA98 File Offset: 0x00018C98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 transpose(uint4x4 v)
		{
			return math.uint4x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w, v.c3.x, v.c3.y, v.c3.z, v.c3.w);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001AB5C File Offset: 0x00018D5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(uint4x4 v)
		{
			return math.csum(v.c0 * math.uint4(2627668003U, 1520214331U, 2949502447U, 2827819133U) + v.c1 * math.uint4(3480140317U, 2642994593U, 3940484981U, 1954192763U) + v.c2 * math.uint4(1091696537U, 3052428017U, 4253034763U, 2338696631U) + v.c3 * math.uint4(3757372771U, 1885959949U, 3508684087U, 3919501043U)) + 1209161033U;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0001AC14 File Offset: 0x00018E14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(uint4x4 v)
		{
			return v.c0 * math.uint4(4007793211U, 3819806693U, 3458005183U, 2078515003U) + v.c1 * math.uint4(4206465343U, 3025146473U, 3763046909U, 3678265601U) + v.c2 * math.uint4(2070747979U, 1480171127U, 1588341193U, 4234155257U) + v.c3 * math.uint4(1811310911U, 2635799963U, 4165137857U, 2759770933U) + 2759319383U;
		}

		// Token: 0x04000003 RID: 3
		public const double E_DBL = 2.718281828459045;

		// Token: 0x04000004 RID: 4
		public const double LOG2E_DBL = 1.4426950408889634;

		// Token: 0x04000005 RID: 5
		public const double LOG10E_DBL = 0.4342944819032518;

		// Token: 0x04000006 RID: 6
		public const double LN2_DBL = 0.6931471805599453;

		// Token: 0x04000007 RID: 7
		public const double LN10_DBL = 2.302585092994046;

		// Token: 0x04000008 RID: 8
		public const double PI_DBL = 3.141592653589793;

		// Token: 0x04000009 RID: 9
		public const double SQRT2_DBL = 1.4142135623730951;

		// Token: 0x0400000A RID: 10
		public const double EPSILON_DBL = 2.220446049250313E-16;

		// Token: 0x0400000B RID: 11
		public const double INFINITY_DBL = double.PositiveInfinity;

		// Token: 0x0400000C RID: 12
		public const double NAN_DBL = double.NaN;

		// Token: 0x0400000D RID: 13
		public const float FLT_MIN_NORMAL = 1.1754944E-38f;

		// Token: 0x0400000E RID: 14
		public const double DBL_MIN_NORMAL = 2.2250738585072014E-308;

		// Token: 0x0400000F RID: 15
		public const float E = 2.7182817f;

		// Token: 0x04000010 RID: 16
		public const float LOG2E = 1.442695f;

		// Token: 0x04000011 RID: 17
		public const float LOG10E = 0.4342945f;

		// Token: 0x04000012 RID: 18
		public const float LN2 = 0.6931472f;

		// Token: 0x04000013 RID: 19
		public const float LN10 = 2.3025851f;

		// Token: 0x04000014 RID: 20
		public const float PI = 3.1415927f;

		// Token: 0x04000015 RID: 21
		public const float SQRT2 = 1.4142135f;

		// Token: 0x04000016 RID: 22
		public const float EPSILON = 1.1920929E-07f;

		// Token: 0x04000017 RID: 23
		public const float INFINITY = float.PositiveInfinity;

		// Token: 0x04000018 RID: 24
		public const float NAN = float.NaN;

		// Token: 0x0200004E RID: 78
		public enum RotationOrder : byte
		{
			// Token: 0x04000121 RID: 289
			XYZ,
			// Token: 0x04000122 RID: 290
			XZY,
			// Token: 0x04000123 RID: 291
			YXZ,
			// Token: 0x04000124 RID: 292
			YZX,
			// Token: 0x04000125 RID: 293
			ZXY,
			// Token: 0x04000126 RID: 294
			ZYX,
			// Token: 0x04000127 RID: 295
			Default = 4
		}

		// Token: 0x0200004F RID: 79
		public enum ShuffleComponent : byte
		{
			// Token: 0x04000129 RID: 297
			LeftX,
			// Token: 0x0400012A RID: 298
			LeftY,
			// Token: 0x0400012B RID: 299
			LeftZ,
			// Token: 0x0400012C RID: 300
			LeftW,
			// Token: 0x0400012D RID: 301
			RightX,
			// Token: 0x0400012E RID: 302
			RightY,
			// Token: 0x0400012F RID: 303
			RightZ,
			// Token: 0x04000130 RID: 304
			RightW
		}

		// Token: 0x02000050 RID: 80
		[StructLayout(LayoutKind.Explicit)]
		internal struct IntFloatUnion
		{
			// Token: 0x04000131 RID: 305
			[FieldOffset(0)]
			public int intValue;

			// Token: 0x04000132 RID: 306
			[FieldOffset(0)]
			public float floatValue;
		}

		// Token: 0x02000051 RID: 81
		[StructLayout(LayoutKind.Explicit)]
		internal struct LongDoubleUnion
		{
			// Token: 0x04000133 RID: 307
			[FieldOffset(0)]
			public long longValue;

			// Token: 0x04000134 RID: 308
			[FieldOffset(0)]
			public double doubleValue;
		}
	}
}
