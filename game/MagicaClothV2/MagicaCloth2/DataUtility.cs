using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x020000C1 RID: 193
	public class DataUtility
	{
		// Token: 0x060002CF RID: 719 RVA: 0x0001CD8B File Offset: 0x0001AF8B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 PackInt2(int d0, int d1)
		{
			if (d0 >= d1)
			{
				return new int2(d1, d0);
			}
			return new int2(d0, d1);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0001CDA0 File Offset: 0x0001AFA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 PackInt2(in int2 d)
		{
			return DataUtility.PackInt2(d.x, d.y);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0001CDB4 File Offset: 0x0001AFB4
		public static int3 PackInt3(int d0, int d1, int d2)
		{
			if (d0 < d1 && d0 < d2)
			{
				if (d1 < d2)
				{
					return new int3(d0, d1, d2);
				}
				return new int3(d0, d2, d1);
			}
			else if (d1 < d2)
			{
				if (d0 < d2)
				{
					return new int3(d1, d0, d2);
				}
				return new int3(d1, d2, d0);
			}
			else
			{
				if (d0 < d1)
				{
					return new int3(d2, d0, d1);
				}
				return new int3(d2, d1, d0);
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0001CE0E File Offset: 0x0001B00E
		public static int3 PackInt3(in int3 d)
		{
			return DataUtility.PackInt3(d.x, d.y, d.z);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0001CE27 File Offset: 0x0001B027
		public static int4 PackInt4(int d0, int d1, int d2, int d3)
		{
			if (d0 > d3)
			{
				int num = d0;
				d0 = d3;
				d3 = num;
			}
			if (d1 > d2)
			{
				int num2 = d1;
				d1 = d2;
				d2 = num2;
			}
			if (d0 > d1)
			{
				int num3 = d0;
				d0 = d1;
				d1 = num3;
			}
			if (d2 > d3)
			{
				int num4 = d2;
				d2 = d3;
				d3 = num4;
			}
			if (d1 > d2)
			{
				int num5 = d1;
				d1 = d2;
				d2 = num5;
			}
			return new int4(d0, d1, d2, d3);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0001CE64 File Offset: 0x0001B064
		public static int4 PackInt4(int4 d)
		{
			return DataUtility.PackInt4(d.x, d.y, d.z, d.w);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0001CE83 File Offset: 0x0001B083
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint Pack32(int hi, int low)
		{
			return (uint)(hi << 16 | (low & 65535));
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0001CE91 File Offset: 0x0001B091
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint Pack32Sort(int a, int b)
		{
			if (a > b)
			{
				return (uint)(b << 16 | (a & 65535));
			}
			return (uint)(a << 16 | (b & 65535));
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0001CEB0 File Offset: 0x0001B0B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Unpack32Hi(uint pack)
		{
			return (int)(pack >> 16 & 65535U);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0001CEBC File Offset: 0x0001B0BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Unpack32Low(uint pack)
		{
			return (int)(pack & 65535U);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0001CEC5 File Offset: 0x0001B0C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint Pack10_22(int hi, int low)
		{
			return (uint)(hi << 22 | (low & 4194303));
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0001CED3 File Offset: 0x0001B0D3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Unpack10_22Hi(uint pack)
		{
			return (int)(pack >> 22 & 1023U);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0001CEDF File Offset: 0x0001B0DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Unpack10_22Low(uint pack)
		{
			return (int)(pack & 4194303U);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0001CEE8 File Offset: 0x0001B0E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Unpack10_22(uint pack, out int hi, out int low)
		{
			hi = (int)(pack >> 22 & 1023U);
			low = (int)(pack & 4194303U);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0001CEFF File Offset: 0x0001B0FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong Pack64(int x, int y, int z, int w)
		{
			return (ulong)(((long)x & 65535L) << 48 | ((long)y & 65535L) << 32 | ((long)z & 65535L) << 16 | ((long)w & 65535L));
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0001CF31 File Offset: 0x0001B131
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong Pack64(in int4 a)
		{
			return DataUtility.Pack64(a.x, a.y, a.z, a.w);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0001CF50 File Offset: 0x0001B150
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 Unpack64(in ulong pack)
		{
			return new int4((int)(pack >> 48 & 65535UL), (int)(pack >> 32 & 65535UL), (int)(pack >> 16 & 65535UL), (int)(pack & 65535UL));
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0001CF88 File Offset: 0x0001B188
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Unpack64X(in ulong pack)
		{
			return (int)(pack >> 48 & 65535UL);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0001CF97 File Offset: 0x0001B197
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Unpack64Y(in ulong pack)
		{
			return (int)(pack >> 32 & 65535UL);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0001CFA6 File Offset: 0x0001B1A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Unpack64Z(in ulong pack)
		{
			return (int)(pack >> 16 & 65535UL);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0001CFB5 File Offset: 0x0001B1B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Unpack64W(in ulong pack)
		{
			return (int)(pack & 65535UL);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0001CFC1 File Offset: 0x0001B1C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint Pack32(int x, int y, int z, int w)
		{
			return (uint)((x & 255) << 24 | (y & 255) << 16 | (z & 255) << 8 | (w & 255));
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0001CF31 File Offset: 0x0001B131
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong Pack32(in int4 a)
		{
			return DataUtility.Pack64(a.x, a.y, a.z, a.w);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0001CFEA File Offset: 0x0001B1EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 Unpack32(in uint pack)
		{
			return new int4((int)(pack >> 24 & 255U), (int)(pack >> 16 & 255U), (int)(pack >> 8 & 255U), (int)(pack & 255U));
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0001D01C File Offset: 0x0001B21C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int RemainingData(in int3 data, in int2 use)
		{
			if (data.x != use.x && data.x != use.y)
			{
				return data.x;
			}
			if (data.y != use.x && data.y != use.y)
			{
				return data.y;
			}
			return data.z;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0001D078 File Offset: 0x0001B278
		public static float4x4 ConvertAnimationCurve(AnimationCurve curve)
		{
			float4x4 result = 0;
			for (int i = 0; i < 16; i++)
			{
				float time = (float)i / 15f;
				float value = curve.Evaluate(time);
				ref result.SetValue(i, value);
			}
			return result;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0001D0B4 File Offset: 0x0001B2B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float EvaluateCurve(in float4x4 curve, float time)
		{
			int num = (int)(math.saturate(time) * 15f);
			time -= (float)num * 0.06666667f;
			float s = time / 0.06666667f;
			return math.lerp(curve.GetValue(num), curve.GetValue(num + 1), s);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0001D0F9 File Offset: 0x0001B2F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ColliderManager.ColliderType GetColliderType(in ExBitFlag8 flag)
		{
			return (ColliderManager.ColliderType)(flag.Value & 15);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0001D105 File Offset: 0x0001B305
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ExBitFlag8 SetColliderType(ExBitFlag8 flag, ColliderManager.ColliderType ctype)
		{
			flag.Value = ((flag.Value & 240) | (byte)ctype);
			return flag;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00002058 File Offset: 0x00000258
		public DataUtility()
		{
		}
	}
}
