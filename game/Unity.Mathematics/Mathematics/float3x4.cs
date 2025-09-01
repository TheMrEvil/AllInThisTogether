using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000023 RID: 35
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct float3x4 : IEquatable<float3x4>, IFormattable
	{
		// Token: 0x06001299 RID: 4761 RVA: 0x00035B24 File Offset: 0x00033D24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x4(float3 c0, float3 c1, float3 c2, float3 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00035B44 File Offset: 0x00033D44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x4(float m00, float m01, float m02, float m03, float m10, float m11, float m12, float m13, float m20, float m21, float m22, float m23)
		{
			this.c0 = new float3(m00, m10, m20);
			this.c1 = new float3(m01, m11, m21);
			this.c2 = new float3(m02, m12, m22);
			this.c3 = new float3(m03, m13, m23);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00035B92 File Offset: 0x00033D92
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x4(float v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00035BC4 File Offset: 0x00033DC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x4(bool v)
		{
			this.c0 = math.select(new float3(0f), new float3(1f), v);
			this.c1 = math.select(new float3(0f), new float3(1f), v);
			this.c2 = math.select(new float3(0f), new float3(1f), v);
			this.c3 = math.select(new float3(0f), new float3(1f), v);
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00035C54 File Offset: 0x00033E54
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x4(bool3x4 v)
		{
			this.c0 = math.select(new float3(0f), new float3(1f), v.c0);
			this.c1 = math.select(new float3(0f), new float3(1f), v.c1);
			this.c2 = math.select(new float3(0f), new float3(1f), v.c2);
			this.c3 = math.select(new float3(0f), new float3(1f), v.c3);
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00035CF5 File Offset: 0x00033EF5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x4(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00035D28 File Offset: 0x00033F28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x4(int3x4 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
			this.c3 = v.c3;
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00035D79 File Offset: 0x00033F79
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x4(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x00035DAC File Offset: 0x00033FAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x4(uint3x4 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
			this.c3 = v.c3;
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00035DFD File Offset: 0x00033FFD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x4(double v)
		{
			this.c0 = (float3)v;
			this.c1 = (float3)v;
			this.c2 = (float3)v;
			this.c3 = (float3)v;
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00035E30 File Offset: 0x00034030
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x4(double3x4 v)
		{
			this.c0 = (float3)v.c0;
			this.c1 = (float3)v.c1;
			this.c2 = (float3)v.c2;
			this.c3 = (float3)v.c3;
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x00035E81 File Offset: 0x00034081
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3x4(float v)
		{
			return new float3x4(v);
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x00035E89 File Offset: 0x00034089
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3x4(bool v)
		{
			return new float3x4(v);
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x00035E91 File Offset: 0x00034091
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3x4(bool3x4 v)
		{
			return new float3x4(v);
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x00035E99 File Offset: 0x00034099
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3x4(int v)
		{
			return new float3x4(v);
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x00035EA1 File Offset: 0x000340A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3x4(int3x4 v)
		{
			return new float3x4(v);
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x00035EA9 File Offset: 0x000340A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3x4(uint v)
		{
			return new float3x4(v);
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x00035EB1 File Offset: 0x000340B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3x4(uint3x4 v)
		{
			return new float3x4(v);
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x00035EB9 File Offset: 0x000340B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3x4(double v)
		{
			return new float3x4(v);
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00035EC1 File Offset: 0x000340C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3x4(double3x4 v)
		{
			return new float3x4(v);
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00035ECC File Offset: 0x000340CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator *(float3x4 lhs, float3x4 rhs)
		{
			return new float3x4(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2, lhs.c3 * rhs.c3);
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x00035F22 File Offset: 0x00034122
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator *(float3x4 lhs, float rhs)
		{
			return new float3x4(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs, lhs.c3 * rhs);
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x00035F59 File Offset: 0x00034159
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator *(float lhs, float3x4 rhs)
		{
			return new float3x4(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2, lhs * rhs.c3);
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x00035F90 File Offset: 0x00034190
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator +(float3x4 lhs, float3x4 rhs)
		{
			return new float3x4(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2, lhs.c3 + rhs.c3);
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x00035FE6 File Offset: 0x000341E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator +(float3x4 lhs, float rhs)
		{
			return new float3x4(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs, lhs.c3 + rhs);
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x0003601D File Offset: 0x0003421D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator +(float lhs, float3x4 rhs)
		{
			return new float3x4(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2, lhs + rhs.c3);
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x00036054 File Offset: 0x00034254
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator -(float3x4 lhs, float3x4 rhs)
		{
			return new float3x4(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2, lhs.c3 - rhs.c3);
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x000360AA File Offset: 0x000342AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator -(float3x4 lhs, float rhs)
		{
			return new float3x4(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs, lhs.c3 - rhs);
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x000360E1 File Offset: 0x000342E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator -(float lhs, float3x4 rhs)
		{
			return new float3x4(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2, lhs - rhs.c3);
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00036118 File Offset: 0x00034318
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator /(float3x4 lhs, float3x4 rhs)
		{
			return new float3x4(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2, lhs.c3 / rhs.c3);
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x0003616E File Offset: 0x0003436E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator /(float3x4 lhs, float rhs)
		{
			return new float3x4(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs, lhs.c3 / rhs);
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x000361A5 File Offset: 0x000343A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator /(float lhs, float3x4 rhs)
		{
			return new float3x4(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2, lhs / rhs.c3);
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x000361DC File Offset: 0x000343DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator %(float3x4 lhs, float3x4 rhs)
		{
			return new float3x4(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2, lhs.c3 % rhs.c3);
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00036232 File Offset: 0x00034432
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator %(float3x4 lhs, float rhs)
		{
			return new float3x4(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs, lhs.c3 % rhs);
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00036269 File Offset: 0x00034469
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator %(float lhs, float3x4 rhs)
		{
			return new float3x4(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2, lhs % rhs.c3);
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x000362A0 File Offset: 0x000344A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator ++(float3x4 val)
		{
			float3 @float = ++val.c0;
			val.c0 = @float;
			float3 float2 = @float;
			@float = ++val.c1;
			val.c1 = @float;
			float3 float3 = @float;
			@float = ++val.c2;
			val.c2 = @float;
			float3 float4 = @float;
			@float = ++val.c3;
			val.c3 = @float;
			return new float3x4(float2, float3, float4, @float);
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x0003631C File Offset: 0x0003451C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator --(float3x4 val)
		{
			float3 @float = --val.c0;
			val.c0 = @float;
			float3 float2 = @float;
			@float = --val.c1;
			val.c1 = @float;
			float3 float3 = @float;
			@float = --val.c2;
			val.c2 = @float;
			float3 float4 = @float;
			@float = --val.c3;
			val.c3 = @float;
			return new float3x4(float2, float3, float4, @float);
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00036398 File Offset: 0x00034598
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <(float3x4 lhs, float3x4 rhs)
		{
			return new bool3x4(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2, lhs.c3 < rhs.c3);
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x000363EE File Offset: 0x000345EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <(float3x4 lhs, float rhs)
		{
			return new bool3x4(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs, lhs.c3 < rhs);
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x00036425 File Offset: 0x00034625
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <(float lhs, float3x4 rhs)
		{
			return new bool3x4(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2, lhs < rhs.c3);
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x0003645C File Offset: 0x0003465C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <=(float3x4 lhs, float3x4 rhs)
		{
			return new bool3x4(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2, lhs.c3 <= rhs.c3);
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x000364B2 File Offset: 0x000346B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <=(float3x4 lhs, float rhs)
		{
			return new bool3x4(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs, lhs.c3 <= rhs);
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x000364E9 File Offset: 0x000346E9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <=(float lhs, float3x4 rhs)
		{
			return new bool3x4(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2, lhs <= rhs.c3);
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00036520 File Offset: 0x00034720
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >(float3x4 lhs, float3x4 rhs)
		{
			return new bool3x4(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2, lhs.c3 > rhs.c3);
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x00036576 File Offset: 0x00034776
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >(float3x4 lhs, float rhs)
		{
			return new bool3x4(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs, lhs.c3 > rhs);
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x000365AD File Offset: 0x000347AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >(float lhs, float3x4 rhs)
		{
			return new bool3x4(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2, lhs > rhs.c3);
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x000365E4 File Offset: 0x000347E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >=(float3x4 lhs, float3x4 rhs)
		{
			return new bool3x4(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2, lhs.c3 >= rhs.c3);
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x0003663A File Offset: 0x0003483A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >=(float3x4 lhs, float rhs)
		{
			return new bool3x4(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs, lhs.c3 >= rhs);
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x00036671 File Offset: 0x00034871
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >=(float lhs, float3x4 rhs)
		{
			return new bool3x4(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2, lhs >= rhs.c3);
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x000366A8 File Offset: 0x000348A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator -(float3x4 val)
		{
			return new float3x4(-val.c0, -val.c1, -val.c2, -val.c3);
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x000366DB File Offset: 0x000348DB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator +(float3x4 val)
		{
			return new float3x4(+val.c0, +val.c1, +val.c2, +val.c3);
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x00036710 File Offset: 0x00034910
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(float3x4 lhs, float3x4 rhs)
		{
			return new bool3x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00036766 File Offset: 0x00034966
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(float3x4 lhs, float rhs)
		{
			return new bool3x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x0003679D File Offset: 0x0003499D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(float lhs, float3x4 rhs)
		{
			return new bool3x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x000367D4 File Offset: 0x000349D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(float3x4 lhs, float3x4 rhs)
		{
			return new bool3x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0003682A File Offset: 0x00034A2A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(float3x4 lhs, float rhs)
		{
			return new bool3x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x00036861 File Offset: 0x00034A61
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(float lhs, float3x4 rhs)
		{
			return new bool3x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x17000473 RID: 1139
		public unsafe float3 this[int index]
		{
			get
			{
				fixed (float3x4* ptr = &this)
				{
					return ref *(float3*)(ptr + (IntPtr)index * (IntPtr)sizeof(float3) / (IntPtr)sizeof(float3x4));
				}
			}
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x000368B4 File Offset: 0x00034AB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(float3x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x00036910 File Offset: 0x00034B10
		public override bool Equals(object o)
		{
			if (o is float3x4)
			{
				float3x4 rhs = (float3x4)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x00036935 File Offset: 0x00034B35
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x00036944 File Offset: 0x00034B44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("float3x4({0}f, {1}f, {2}f, {3}f,  {4}f, {5}f, {6}f, {7}f,  {8}f, {9}f, {10}f, {11}f)", new object[]
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

		// Token: 0x060012D7 RID: 4823 RVA: 0x00036A4C File Offset: 0x00034C4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("float3x4({0}f, {1}f, {2}f, {3}f,  {4}f, {5}f, {6}f, {7}f,  {8}f, {9}f, {10}f, {11}f)", new object[]
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

		// Token: 0x0400008A RID: 138
		public float3 c0;

		// Token: 0x0400008B RID: 139
		public float3 c1;

		// Token: 0x0400008C RID: 140
		public float3 c2;

		// Token: 0x0400008D RID: 141
		public float3 c3;

		// Token: 0x0400008E RID: 142
		public static readonly float3x4 zero;
	}
}
