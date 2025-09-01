using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Unity.Burst.Intrinsics
{
	// Token: 0x02000022 RID: 34
	[BurstCompile]
	public static class X86
	{
		// Token: 0x0600011B RID: 283 RVA: 0x000078A7 File Offset: 0x00005AA7
		private unsafe static v128 GenericCSharpLoad(void* ptr)
		{
			return *(v128*)ptr;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000078AF File Offset: 0x00005AAF
		private unsafe static void GenericCSharpStore(void* ptr, v128 val)
		{
			*(v128*)ptr = val;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000078B8 File Offset: 0x00005AB8
		private static sbyte Saturate_To_Int8(int val)
		{
			if (val > 127)
			{
				return sbyte.MaxValue;
			}
			if (val < -128)
			{
				return sbyte.MinValue;
			}
			return (sbyte)val;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000078CC File Offset: 0x00005ACC
		private static byte Saturate_To_UnsignedInt8(int val)
		{
			if (val > 255)
			{
				return byte.MaxValue;
			}
			if (val < 0)
			{
				return 0;
			}
			return (byte)val;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000078E4 File Offset: 0x00005AE4
		private static short Saturate_To_Int16(int val)
		{
			if (val > 32767)
			{
				return short.MaxValue;
			}
			if (val < -32768)
			{
				return short.MinValue;
			}
			return (short)val;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00007904 File Offset: 0x00005B04
		private static ushort Saturate_To_UnsignedInt16(int val)
		{
			if (val > 65535)
			{
				return ushort.MaxValue;
			}
			if (val < 0)
			{
				return 0;
			}
			return (ushort)val;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000791C File Offset: 0x00005B1C
		private static bool IsNaN(uint v)
		{
			return (v & 2147483647U) > 2139095040U;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000792C File Offset: 0x00005B2C
		private static bool IsNaN(ulong v)
		{
			return (v & 9223372036854775807UL) > 9218868437227405312UL;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00007944 File Offset: 0x00005B44
		private static void BurstIntrinsicSetCSRFromManaged(int _)
		{
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00007946 File Offset: 0x00005B46
		private static int BurstIntrinsicGetCSRFromManaged()
		{
			return 0;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00007949 File Offset: 0x00005B49
		internal static int getcsr_raw()
		{
			return X86.DoGetCSRTrampoline();
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00007950 File Offset: 0x00005B50
		internal static void setcsr_raw(int bits)
		{
			X86.DoSetCSRTrampoline(bits);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00007958 File Offset: 0x00005B58
		[BurstCompile(CompileSynchronously = true)]
		private static void DoSetCSRTrampoline(int bits)
		{
			if (X86.Sse.IsSseSupported)
			{
				X86.BurstIntrinsicSetCSRFromManaged(bits);
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00007967 File Offset: 0x00005B67
		[BurstCompile(CompileSynchronously = true)]
		private static int DoGetCSRTrampoline()
		{
			if (X86.Sse.IsSseSupported)
			{
				return X86.BurstIntrinsicGetCSRFromManaged();
			}
			return 0;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00007977 File Offset: 0x00005B77
		// (set) Token: 0x0600012A RID: 298 RVA: 0x0000797E File Offset: 0x00005B7E
		public static X86.MXCSRBits MXCSR
		{
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			get
			{
				return (X86.MXCSRBits)X86.getcsr_raw();
			}
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			set
			{
				X86.setcsr_raw((int)value);
			}
		}

		// Token: 0x0200003E RID: 62
		public static class Avx
		{
			// Token: 0x17000041 RID: 65
			// (get) Token: 0x06000ACC RID: 2764 RVA: 0x0000C3F6 File Offset: 0x0000A5F6
			public static bool IsAvxSupported
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000ACD RID: 2765 RVA: 0x0000C3F9 File Offset: 0x0000A5F9
			[DebuggerStepThrough]
			public static v256 mm256_add_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse2.add_pd(a.Lo128, b.Lo128), X86.Sse2.add_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000ACE RID: 2766 RVA: 0x0000C422 File Offset: 0x0000A622
			[DebuggerStepThrough]
			public static v256 mm256_add_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse.add_ps(a.Lo128, b.Lo128), X86.Sse.add_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000ACF RID: 2767 RVA: 0x0000C44B File Offset: 0x0000A64B
			[DebuggerStepThrough]
			public static v256 mm256_addsub_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse3.addsub_pd(a.Lo128, b.Lo128), X86.Sse3.addsub_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AD0 RID: 2768 RVA: 0x0000C474 File Offset: 0x0000A674
			[DebuggerStepThrough]
			public static v256 mm256_addsub_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse3.addsub_ps(a.Lo128, b.Lo128), X86.Sse3.addsub_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AD1 RID: 2769 RVA: 0x0000C49D File Offset: 0x0000A69D
			[DebuggerStepThrough]
			public static v256 mm256_and_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse2.and_pd(a.Lo128, b.Lo128), X86.Sse2.and_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AD2 RID: 2770 RVA: 0x0000C4C6 File Offset: 0x0000A6C6
			[DebuggerStepThrough]
			public static v256 mm256_and_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse.and_ps(a.Lo128, b.Lo128), X86.Sse.and_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AD3 RID: 2771 RVA: 0x0000C4EF File Offset: 0x0000A6EF
			[DebuggerStepThrough]
			public static v256 mm256_andnot_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse2.andnot_pd(a.Lo128, b.Lo128), X86.Sse2.andnot_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AD4 RID: 2772 RVA: 0x0000C518 File Offset: 0x0000A718
			[DebuggerStepThrough]
			public static v256 mm256_andnot_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse.andnot_ps(a.Lo128, b.Lo128), X86.Sse.andnot_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AD5 RID: 2773 RVA: 0x0000C541 File Offset: 0x0000A741
			[DebuggerStepThrough]
			public static v256 mm256_blend_pd(v256 a, v256 b, int imm8)
			{
				return new v256(X86.Sse4_1.blend_pd(a.Lo128, b.Lo128, imm8 & 3), X86.Sse4_1.blend_pd(a.Hi128, b.Hi128, imm8 >> 2));
			}

			// Token: 0x06000AD6 RID: 2774 RVA: 0x0000C570 File Offset: 0x0000A770
			[DebuggerStepThrough]
			public static v256 mm256_blend_ps(v256 a, v256 b, int imm8)
			{
				return new v256(X86.Sse4_1.blend_ps(a.Lo128, b.Lo128, imm8 & 15), X86.Sse4_1.blend_ps(a.Hi128, b.Hi128, imm8 >> 4));
			}

			// Token: 0x06000AD7 RID: 2775 RVA: 0x0000C5A0 File Offset: 0x0000A7A0
			[DebuggerStepThrough]
			public static v256 mm256_blendv_pd(v256 a, v256 b, v256 mask)
			{
				return new v256(X86.Sse4_1.blendv_pd(a.Lo128, b.Lo128, mask.Lo128), X86.Sse4_1.blendv_pd(a.Hi128, b.Hi128, mask.Hi128));
			}

			// Token: 0x06000AD8 RID: 2776 RVA: 0x0000C5D5 File Offset: 0x0000A7D5
			[DebuggerStepThrough]
			public static v256 mm256_blendv_ps(v256 a, v256 b, v256 mask)
			{
				return new v256(X86.Sse4_1.blendv_ps(a.Lo128, b.Lo128, mask.Lo128), X86.Sse4_1.blendv_ps(a.Hi128, b.Hi128, mask.Hi128));
			}

			// Token: 0x06000AD9 RID: 2777 RVA: 0x0000C60A File Offset: 0x0000A80A
			[DebuggerStepThrough]
			public static v256 mm256_div_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse2.div_pd(a.Lo128, b.Lo128), X86.Sse2.div_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000ADA RID: 2778 RVA: 0x0000C633 File Offset: 0x0000A833
			[DebuggerStepThrough]
			public static v256 mm256_div_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse.div_ps(a.Lo128, b.Lo128), X86.Sse.div_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000ADB RID: 2779 RVA: 0x0000C65C File Offset: 0x0000A85C
			[DebuggerStepThrough]
			public static v256 mm256_dp_ps(v256 a, v256 b, int imm8)
			{
				return new v256(X86.Sse4_1.dp_ps(a.Lo128, b.Lo128, imm8), X86.Sse4_1.dp_ps(a.Hi128, b.Hi128, imm8));
			}

			// Token: 0x06000ADC RID: 2780 RVA: 0x0000C687 File Offset: 0x0000A887
			[DebuggerStepThrough]
			public static v256 mm256_hadd_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse3.hadd_pd(a.Lo128, b.Lo128), X86.Sse3.hadd_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000ADD RID: 2781 RVA: 0x0000C6B0 File Offset: 0x0000A8B0
			[DebuggerStepThrough]
			public static v256 mm256_hadd_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse3.hadd_ps(a.Lo128, b.Lo128), X86.Sse3.hadd_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000ADE RID: 2782 RVA: 0x0000C6D9 File Offset: 0x0000A8D9
			[DebuggerStepThrough]
			public static v256 mm256_hsub_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse3.hsub_pd(a.Lo128, b.Lo128), X86.Sse3.hsub_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000ADF RID: 2783 RVA: 0x0000C702 File Offset: 0x0000A902
			[DebuggerStepThrough]
			public static v256 mm256_hsub_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse3.hsub_ps(a.Lo128, b.Lo128), X86.Sse3.hsub_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AE0 RID: 2784 RVA: 0x0000C72B File Offset: 0x0000A92B
			[DebuggerStepThrough]
			public static v256 mm256_max_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse2.max_pd(a.Lo128, b.Lo128), X86.Sse2.max_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AE1 RID: 2785 RVA: 0x0000C754 File Offset: 0x0000A954
			[DebuggerStepThrough]
			public static v256 mm256_max_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse.max_ps(a.Lo128, b.Lo128), X86.Sse.max_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AE2 RID: 2786 RVA: 0x0000C77D File Offset: 0x0000A97D
			[DebuggerStepThrough]
			public static v256 mm256_min_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse2.min_pd(a.Lo128, b.Lo128), X86.Sse2.min_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AE3 RID: 2787 RVA: 0x0000C7A6 File Offset: 0x0000A9A6
			[DebuggerStepThrough]
			public static v256 mm256_min_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse.min_ps(a.Lo128, b.Lo128), X86.Sse.min_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AE4 RID: 2788 RVA: 0x0000C7CF File Offset: 0x0000A9CF
			[DebuggerStepThrough]
			public static v256 mm256_mul_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse2.mul_pd(a.Lo128, b.Lo128), X86.Sse2.mul_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AE5 RID: 2789 RVA: 0x0000C7F8 File Offset: 0x0000A9F8
			[DebuggerStepThrough]
			public static v256 mm256_mul_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse.mul_ps(a.Lo128, b.Lo128), X86.Sse.mul_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AE6 RID: 2790 RVA: 0x0000C821 File Offset: 0x0000AA21
			[DebuggerStepThrough]
			public static v256 mm256_or_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse2.or_pd(a.Lo128, b.Lo128), X86.Sse2.or_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AE7 RID: 2791 RVA: 0x0000C84A File Offset: 0x0000AA4A
			[DebuggerStepThrough]
			public static v256 mm256_or_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse.or_ps(a.Lo128, b.Lo128), X86.Sse.or_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AE8 RID: 2792 RVA: 0x0000C873 File Offset: 0x0000AA73
			[DebuggerStepThrough]
			public static v256 mm256_shuffle_pd(v256 a, v256 b, int imm8)
			{
				return new v256(X86.Sse2.shuffle_pd(a.Lo128, b.Lo128, imm8 & 3), X86.Sse2.shuffle_pd(a.Hi128, b.Hi128, imm8 >> 2));
			}

			// Token: 0x06000AE9 RID: 2793 RVA: 0x0000C8A2 File Offset: 0x0000AAA2
			[DebuggerStepThrough]
			public static v256 mm256_shuffle_ps(v256 a, v256 b, int imm8)
			{
				return new v256(X86.Sse.shuffle_ps(a.Lo128, b.Lo128, imm8), X86.Sse.shuffle_ps(a.Hi128, b.Hi128, imm8));
			}

			// Token: 0x06000AEA RID: 2794 RVA: 0x0000C8CD File Offset: 0x0000AACD
			[DebuggerStepThrough]
			public static v256 mm256_sub_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse2.sub_pd(a.Lo128, b.Lo128), X86.Sse2.sub_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AEB RID: 2795 RVA: 0x0000C8F6 File Offset: 0x0000AAF6
			[DebuggerStepThrough]
			public static v256 mm256_sub_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse.sub_ps(a.Lo128, b.Lo128), X86.Sse.sub_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AEC RID: 2796 RVA: 0x0000C91F File Offset: 0x0000AB1F
			[DebuggerStepThrough]
			public static v256 mm256_xor_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse2.xor_pd(a.Lo128, b.Lo128), X86.Sse2.xor_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AED RID: 2797 RVA: 0x0000C948 File Offset: 0x0000AB48
			[DebuggerStepThrough]
			public static v256 mm256_xor_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse.xor_ps(a.Lo128, b.Lo128), X86.Sse.xor_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000AEE RID: 2798 RVA: 0x0000C974 File Offset: 0x0000AB74
			[DebuggerStepThrough]
			public static v128 cmp_pd(v128 a, v128 b, int imm8)
			{
				switch (imm8 & 31)
				{
				case 0:
					return X86.Sse2.cmpeq_pd(a, b);
				case 1:
					return X86.Sse2.cmplt_pd(a, b);
				case 2:
					return X86.Sse2.cmple_pd(a, b);
				case 3:
					return X86.Sse2.cmpunord_pd(a, b);
				case 4:
					return X86.Sse2.cmpneq_pd(a, b);
				case 5:
					return X86.Sse2.cmpnlt_pd(a, b);
				case 6:
					return X86.Sse2.cmpnle_pd(a, b);
				case 7:
					return X86.Sse2.cmpord_pd(a, b);
				case 8:
					return X86.Sse2.or_pd(X86.Sse2.cmpeq_pd(a, b), X86.Sse2.cmpunord_pd(a, b));
				case 9:
					return X86.Sse2.or_pd(X86.Sse2.cmpnge_pd(a, b), X86.Sse2.cmpunord_pd(a, b));
				case 10:
					return X86.Sse2.or_pd(X86.Sse2.cmpngt_pd(a, b), X86.Sse2.cmpunord_pd(a, b));
				case 11:
					return default(v128);
				case 12:
					return X86.Sse2.and_pd(X86.Sse2.cmpneq_pd(a, b), X86.Sse2.cmpord_pd(a, b));
				case 13:
					return X86.Sse2.and_pd(X86.Sse2.cmpge_pd(a, b), X86.Sse2.cmpord_pd(a, b));
				case 14:
					return X86.Sse2.and_pd(X86.Sse2.cmpgt_pd(a, b), X86.Sse2.cmpord_pd(a, b));
				case 15:
					return new v128(-1);
				case 16:
					return X86.Sse2.and_pd(X86.Sse2.cmpeq_pd(a, b), X86.Sse2.cmpord_pd(a, b));
				case 17:
					return X86.Sse2.and_pd(X86.Sse2.cmplt_pd(a, b), X86.Sse2.cmpord_pd(a, b));
				case 18:
					return X86.Sse2.and_pd(X86.Sse2.cmple_pd(a, b), X86.Sse2.cmpord_pd(a, b));
				case 19:
					return X86.Sse2.cmpunord_pd(a, b);
				case 20:
					return X86.Sse2.cmpneq_pd(a, b);
				case 21:
					return X86.Sse2.or_pd(X86.Sse2.cmpnlt_pd(a, b), X86.Sse2.cmpunord_pd(a, b));
				case 22:
					return X86.Sse2.or_pd(X86.Sse2.cmpnle_pd(a, b), X86.Sse2.cmpunord_pd(a, b));
				case 23:
					return X86.Sse2.cmpord_pd(a, b);
				case 24:
					return X86.Sse2.or_pd(X86.Sse2.cmpeq_pd(a, b), X86.Sse2.cmpunord_pd(a, b));
				case 25:
					return X86.Sse2.or_pd(X86.Sse2.cmpnge_pd(a, b), X86.Sse2.cmpunord_pd(a, b));
				case 26:
					return X86.Sse2.or_pd(X86.Sse2.cmpngt_pd(a, b), X86.Sse2.cmpunord_pd(a, b));
				case 27:
					return default(v128);
				case 28:
					return X86.Sse2.and_pd(X86.Sse2.cmpneq_pd(a, b), X86.Sse2.cmpord_pd(a, b));
				case 29:
					return X86.Sse2.and_pd(X86.Sse2.cmpge_pd(a, b), X86.Sse2.cmpord_pd(a, b));
				case 30:
					return X86.Sse2.and_pd(X86.Sse2.cmpgt_pd(a, b), X86.Sse2.cmpord_pd(a, b));
				default:
					return new v128(-1);
				}
			}

			// Token: 0x06000AEF RID: 2799 RVA: 0x0000CBDA File Offset: 0x0000ADDA
			[DebuggerStepThrough]
			public static v256 mm256_cmp_pd(v256 a, v256 b, int imm8)
			{
				return new v256(X86.Avx.cmp_pd(a.Lo128, b.Lo128, imm8), X86.Avx.cmp_pd(a.Hi128, b.Hi128, imm8));
			}

			// Token: 0x06000AF0 RID: 2800 RVA: 0x0000CC08 File Offset: 0x0000AE08
			[DebuggerStepThrough]
			public static v128 cmp_ps(v128 a, v128 b, int imm8)
			{
				switch (imm8 & 31)
				{
				case 0:
					return X86.Sse.cmpeq_ps(a, b);
				case 1:
					return X86.Sse.cmplt_ps(a, b);
				case 2:
					return X86.Sse.cmple_ps(a, b);
				case 3:
					return X86.Sse.cmpunord_ps(a, b);
				case 4:
					return X86.Sse.cmpneq_ps(a, b);
				case 5:
					return X86.Sse.cmpnlt_ps(a, b);
				case 6:
					return X86.Sse.cmpnle_ps(a, b);
				case 7:
					return X86.Sse.cmpord_ps(a, b);
				case 8:
					return X86.Sse.or_ps(X86.Sse.cmpeq_ps(a, b), X86.Sse.cmpunord_ps(a, b));
				case 9:
					return X86.Sse.or_ps(X86.Sse.cmpnge_ps(a, b), X86.Sse.cmpunord_ps(a, b));
				case 10:
					return X86.Sse.or_ps(X86.Sse.cmpngt_ps(a, b), X86.Sse.cmpunord_ps(a, b));
				case 11:
					return default(v128);
				case 12:
					return X86.Sse.and_ps(X86.Sse.cmpneq_ps(a, b), X86.Sse.cmpord_ps(a, b));
				case 13:
					return X86.Sse.and_ps(X86.Sse.cmpge_ps(a, b), X86.Sse.cmpord_ps(a, b));
				case 14:
					return X86.Sse.and_ps(X86.Sse.cmpgt_ps(a, b), X86.Sse.cmpord_ps(a, b));
				case 15:
					return new v128(-1);
				case 16:
					return X86.Sse.and_ps(X86.Sse.cmpeq_ps(a, b), X86.Sse.cmpord_ps(a, b));
				case 17:
					return X86.Sse.and_ps(X86.Sse.cmplt_ps(a, b), X86.Sse.cmpord_ps(a, b));
				case 18:
					return X86.Sse.and_ps(X86.Sse.cmple_ps(a, b), X86.Sse.cmpord_ps(a, b));
				case 19:
					return X86.Sse.cmpunord_ps(a, b);
				case 20:
					return X86.Sse.cmpneq_ps(a, b);
				case 21:
					return X86.Sse.or_ps(X86.Sse.cmpnlt_ps(a, b), X86.Sse.cmpunord_ps(a, b));
				case 22:
					return X86.Sse.or_ps(X86.Sse.cmpnle_ps(a, b), X86.Sse.cmpunord_ps(a, b));
				case 23:
					return X86.Sse.cmpord_ps(a, b);
				case 24:
					return X86.Sse.or_ps(X86.Sse.cmpeq_ps(a, b), X86.Sse.cmpunord_ps(a, b));
				case 25:
					return X86.Sse.or_ps(X86.Sse.cmpnge_ps(a, b), X86.Sse.cmpunord_ps(a, b));
				case 26:
					return X86.Sse.or_ps(X86.Sse.cmpngt_ps(a, b), X86.Sse.cmpunord_ps(a, b));
				case 27:
					return default(v128);
				case 28:
					return X86.Sse.and_ps(X86.Sse.cmpneq_ps(a, b), X86.Sse.cmpord_ps(a, b));
				case 29:
					return X86.Sse.and_ps(X86.Sse.cmpge_ps(a, b), X86.Sse.cmpord_ps(a, b));
				case 30:
					return X86.Sse.and_ps(X86.Sse.cmpgt_ps(a, b), X86.Sse.cmpord_ps(a, b));
				default:
					return new v128(-1);
				}
			}

			// Token: 0x06000AF1 RID: 2801 RVA: 0x0000CE6E File Offset: 0x0000B06E
			[DebuggerStepThrough]
			public static v256 mm256_cmp_ps(v256 a, v256 b, int imm8)
			{
				return new v256(X86.Avx.cmp_ps(a.Lo128, b.Lo128, imm8), X86.Avx.cmp_ps(a.Hi128, b.Hi128, imm8));
			}

			// Token: 0x06000AF2 RID: 2802 RVA: 0x0000CE99 File Offset: 0x0000B099
			[DebuggerStepThrough]
			public static v128 cmp_sd(v128 a, v128 b, int imm8)
			{
				return new v128(X86.Avx.cmp_pd(a, b, imm8).ULong0, a.ULong1);
			}

			// Token: 0x06000AF3 RID: 2803 RVA: 0x0000CEB3 File Offset: 0x0000B0B3
			[DebuggerStepThrough]
			public static v128 cmp_ss(v128 a, v128 b, int imm8)
			{
				return new v128(X86.Avx.cmp_ps(a, b, imm8).UInt0, a.UInt1, a.UInt2, a.UInt3);
			}

			// Token: 0x06000AF4 RID: 2804 RVA: 0x0000CED9 File Offset: 0x0000B0D9
			[DebuggerStepThrough]
			public static v256 mm256_cvtepi32_pd(v128 a)
			{
				return new v256((double)a.SInt0, (double)a.SInt1, (double)a.SInt2, (double)a.SInt3);
			}

			// Token: 0x06000AF5 RID: 2805 RVA: 0x0000CEFC File Offset: 0x0000B0FC
			[DebuggerStepThrough]
			public static v256 mm256_cvtepi32_ps(v256 a)
			{
				return new v256(X86.Sse2.cvtepi32_ps(a.Lo128), X86.Sse2.cvtepi32_ps(a.Hi128));
			}

			// Token: 0x06000AF6 RID: 2806 RVA: 0x0000CF1C File Offset: 0x0000B11C
			[DebuggerStepThrough]
			public static v128 mm256_cvtpd_ps(v256 a)
			{
				v128 v = X86.Sse2.cvtpd_ps(a.Lo128);
				v128 v2 = X86.Sse2.cvtpd_ps(a.Hi128);
				return new v128(v.Float0, v.Float1, v2.Float0, v2.Float1);
			}

			// Token: 0x06000AF7 RID: 2807 RVA: 0x0000CF5E File Offset: 0x0000B15E
			[DebuggerStepThrough]
			public static v256 mm256_cvtps_epi32(v256 a)
			{
				return new v256(X86.Sse2.cvtps_epi32(a.Lo128), X86.Sse2.cvtps_epi32(a.Hi128));
			}

			// Token: 0x06000AF8 RID: 2808 RVA: 0x0000CF7B File Offset: 0x0000B17B
			[DebuggerStepThrough]
			public static v256 mm256_cvtps_pd(v128 a)
			{
				return new v256((double)a.Float0, (double)a.Float1, (double)a.Float2, (double)a.Float3);
			}

			// Token: 0x06000AF9 RID: 2809 RVA: 0x0000CF9E File Offset: 0x0000B19E
			[DebuggerStepThrough]
			public static v128 mm256_cvttpd_epi32(v256 a)
			{
				return new v128((int)a.Double0, (int)a.Double1, (int)a.Double2, (int)a.Double3);
			}

			// Token: 0x06000AFA RID: 2810 RVA: 0x0000CFC4 File Offset: 0x0000B1C4
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public static v128 mm256_cvtpd_epi32(v256 a)
			{
				v128 v = X86.Sse2.cvtpd_epi32(new v128(a.Double0, a.Double1));
				v128 v2 = X86.Sse2.cvtpd_epi32(new v128(a.Double2, a.Double3));
				return new v128(v.SInt0, v.SInt1, v2.SInt0, v2.SInt1);
			}

			// Token: 0x06000AFB RID: 2811 RVA: 0x0000D01C File Offset: 0x0000B21C
			[DebuggerStepThrough]
			public static v256 mm256_cvttps_epi32(v256 a)
			{
				return new v256(X86.Sse2.cvttps_epi32(a.Lo128), X86.Sse2.cvttps_epi32(a.Hi128));
			}

			// Token: 0x06000AFC RID: 2812 RVA: 0x0000D039 File Offset: 0x0000B239
			[DebuggerStepThrough]
			public static float mm256_cvtss_f32(v256 a)
			{
				return a.Float0;
			}

			// Token: 0x06000AFD RID: 2813 RVA: 0x0000D041 File Offset: 0x0000B241
			[DebuggerStepThrough]
			public static v128 mm256_extractf128_ps(v256 a, int imm8)
			{
				if (imm8 == 0)
				{
					return a.Lo128;
				}
				return a.Hi128;
			}

			// Token: 0x06000AFE RID: 2814 RVA: 0x0000D053 File Offset: 0x0000B253
			[DebuggerStepThrough]
			public static v128 mm256_extractf128_pd(v256 a, int imm8)
			{
				if (imm8 == 0)
				{
					return a.Lo128;
				}
				return a.Hi128;
			}

			// Token: 0x06000AFF RID: 2815 RVA: 0x0000D065 File Offset: 0x0000B265
			[DebuggerStepThrough]
			public static v128 mm256_extractf128_si256(v256 a, int imm8)
			{
				if (imm8 == 0)
				{
					return a.Lo128;
				}
				return a.Hi128;
			}

			// Token: 0x06000B00 RID: 2816 RVA: 0x0000D077 File Offset: 0x0000B277
			[DebuggerStepThrough]
			public static void mm256_zeroall()
			{
			}

			// Token: 0x06000B01 RID: 2817 RVA: 0x0000D079 File Offset: 0x0000B279
			[DebuggerStepThrough]
			public static void mm256_zeroupper()
			{
			}

			// Token: 0x06000B02 RID: 2818 RVA: 0x0000D07C File Offset: 0x0000B27C
			[DebuggerStepThrough]
			public unsafe static v128 permutevar_ps(v128 a, v128 b)
			{
				v128 result = default(v128);
				uint* ptr = &result.UInt0;
				uint* ptr2 = &a.UInt0;
				int* ptr3 = &b.SInt0;
				for (int i = 0; i < 4; i++)
				{
					int num = ptr3[i] & 3;
					ptr[i] = ptr2[num];
				}
				return result;
			}

			// Token: 0x06000B03 RID: 2819 RVA: 0x0000D0D9 File Offset: 0x0000B2D9
			[DebuggerStepThrough]
			public static v256 mm256_permutevar_ps(v256 a, v256 b)
			{
				return new v256(X86.Avx.permutevar_ps(a.Lo128, b.Lo128), X86.Avx.permutevar_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B04 RID: 2820 RVA: 0x0000D102 File Offset: 0x0000B302
			[DebuggerStepThrough]
			public static v128 permute_ps(v128 a, int imm8)
			{
				return X86.Sse2.shuffle_epi32(a, imm8);
			}

			// Token: 0x06000B05 RID: 2821 RVA: 0x0000D10B File Offset: 0x0000B30B
			[DebuggerStepThrough]
			public static v256 mm256_permute_ps(v256 a, int imm8)
			{
				return new v256(X86.Avx.permute_ps(a.Lo128, imm8), X86.Avx.permute_ps(a.Hi128, imm8));
			}

			// Token: 0x06000B06 RID: 2822 RVA: 0x0000D12C File Offset: 0x0000B32C
			[DebuggerStepThrough]
			public unsafe static v128 permutevar_pd(v128 a, v128 b)
			{
				v128 result = default(v128);
				double* ptr = &result.Double0;
				double* ptr2 = &a.Double0;
				*ptr = ptr2[(int)(b.SLong0 & 2L) >> 1];
				ptr[1] = ptr2[(int)(b.SLong1 & 2L) >> 1];
				return result;
			}

			// Token: 0x06000B07 RID: 2823 RVA: 0x0000D17C File Offset: 0x0000B37C
			[DebuggerStepThrough]
			public unsafe static v256 mm256_permutevar_pd(v256 a, v256 b)
			{
				v256 result = default(v256);
				double* ptr = &result.Double0;
				double* ptr2 = &a.Double0;
				*ptr = ptr2[(int)(b.SLong0 & 2L) >> 1];
				ptr[1] = ptr2[(int)(b.SLong1 & 2L) >> 1];
				ptr[2] = ptr2[2 + ((int)(b.SLong2 & 2L) >> 1)];
				ptr[3] = ptr2[2 + ((int)(b.SLong3 & 2L) >> 1)];
				return result;
			}

			// Token: 0x06000B08 RID: 2824 RVA: 0x0000D202 File Offset: 0x0000B402
			[DebuggerStepThrough]
			public static v256 mm256_permute_pd(v256 a, int imm8)
			{
				return new v256(X86.Avx.permute_pd(a.Lo128, imm8 & 3), X86.Avx.permute_pd(a.Hi128, imm8 >> 2));
			}

			// Token: 0x06000B09 RID: 2825 RVA: 0x0000D228 File Offset: 0x0000B428
			[DebuggerStepThrough]
			public unsafe static v128 permute_pd(v128 a, int imm8)
			{
				v128 result = default(v128);
				double* ptr = &result.Double0;
				double* ptr2 = &a.Double0;
				*ptr = ptr2[imm8 & 1];
				ptr[1] = ptr2[imm8 >> 1 & 1];
				return result;
			}

			// Token: 0x06000B0A RID: 2826 RVA: 0x0000D268 File Offset: 0x0000B468
			private static v128 Select4(v256 src1, v256 src2, int control)
			{
				switch (control & 3)
				{
				case 0:
					return src1.Lo128;
				case 1:
					return src1.Hi128;
				case 2:
					return src2.Lo128;
				default:
					return src2.Hi128;
				}
			}

			// Token: 0x06000B0B RID: 2827 RVA: 0x0000D2A8 File Offset: 0x0000B4A8
			[DebuggerStepThrough]
			public static v256 mm256_permute2f128_ps(v256 a, v256 b, int imm8)
			{
				return new v256(X86.Avx.Select4(a, b, imm8), X86.Avx.Select4(a, b, imm8 >> 4));
			}

			// Token: 0x06000B0C RID: 2828 RVA: 0x0000D2C1 File Offset: 0x0000B4C1
			[DebuggerStepThrough]
			public static v256 mm256_permute2f128_pd(v256 a, v256 b, int imm8)
			{
				return X86.Avx.mm256_permute2f128_ps(a, b, imm8);
			}

			// Token: 0x06000B0D RID: 2829 RVA: 0x0000D2CB File Offset: 0x0000B4CB
			[DebuggerStepThrough]
			public static v256 mm256_permute2f128_si256(v256 a, v256 b, int imm8)
			{
				return X86.Avx.mm256_permute2f128_ps(a, b, imm8);
			}

			// Token: 0x06000B0E RID: 2830 RVA: 0x0000D2D5 File Offset: 0x0000B4D5
			[DebuggerStepThrough]
			public unsafe static v256 mm256_broadcast_ss(void* ptr)
			{
				return new v256(*(uint*)ptr);
			}

			// Token: 0x06000B0F RID: 2831 RVA: 0x0000D2DE File Offset: 0x0000B4DE
			[DebuggerStepThrough]
			public unsafe static v128 broadcast_ss(void* ptr)
			{
				return new v128(*(uint*)ptr);
			}

			// Token: 0x06000B10 RID: 2832 RVA: 0x0000D2E7 File Offset: 0x0000B4E7
			[DebuggerStepThrough]
			public unsafe static v256 mm256_broadcast_sd(void* ptr)
			{
				return new v256(*(double*)ptr);
			}

			// Token: 0x06000B11 RID: 2833 RVA: 0x0000D2F0 File Offset: 0x0000B4F0
			[DebuggerStepThrough]
			public unsafe static v256 mm256_broadcast_ps(void* ptr)
			{
				v128 v = X86.Sse.loadu_ps(ptr);
				return new v256(v, v);
			}

			// Token: 0x06000B12 RID: 2834 RVA: 0x0000D2FE File Offset: 0x0000B4FE
			[DebuggerStepThrough]
			public unsafe static v256 mm256_broadcast_pd(void* ptr)
			{
				return X86.Avx.mm256_broadcast_ps(ptr);
			}

			// Token: 0x06000B13 RID: 2835 RVA: 0x0000D306 File Offset: 0x0000B506
			[DebuggerStepThrough]
			public static v256 mm256_insertf128_ps(v256 a, v128 b, int imm8)
			{
				if ((imm8 & 1) == 0)
				{
					return new v256(b, a.Hi128);
				}
				return new v256(a.Lo128, b);
			}

			// Token: 0x06000B14 RID: 2836 RVA: 0x0000D326 File Offset: 0x0000B526
			[DebuggerStepThrough]
			public static v256 mm256_insertf128_pd(v256 a, v128 b, int imm8)
			{
				return X86.Avx.mm256_insertf128_ps(a, b, imm8);
			}

			// Token: 0x06000B15 RID: 2837 RVA: 0x0000D330 File Offset: 0x0000B530
			[DebuggerStepThrough]
			public static v256 mm256_insertf128_si256(v256 a, v128 b, int imm8)
			{
				return X86.Avx.mm256_insertf128_ps(a, b, imm8);
			}

			// Token: 0x06000B16 RID: 2838 RVA: 0x0000D33A File Offset: 0x0000B53A
			[DebuggerStepThrough]
			public unsafe static v256 mm256_load_ps(void* ptr)
			{
				return *(v256*)ptr;
			}

			// Token: 0x06000B17 RID: 2839 RVA: 0x0000D342 File Offset: 0x0000B542
			[DebuggerStepThrough]
			public unsafe static void mm256_store_ps(void* ptr, v256 val)
			{
				*(v256*)ptr = val;
			}

			// Token: 0x06000B18 RID: 2840 RVA: 0x0000D34B File Offset: 0x0000B54B
			[DebuggerStepThrough]
			public unsafe static v256 mm256_load_pd(void* ptr)
			{
				return X86.Avx.mm256_load_ps(ptr);
			}

			// Token: 0x06000B19 RID: 2841 RVA: 0x0000D353 File Offset: 0x0000B553
			[DebuggerStepThrough]
			public unsafe static void mm256_store_pd(void* ptr, v256 a)
			{
				X86.Avx.mm256_store_ps(ptr, a);
			}

			// Token: 0x06000B1A RID: 2842 RVA: 0x0000D35C File Offset: 0x0000B55C
			[DebuggerStepThrough]
			public unsafe static v256 mm256_loadu_pd(void* ptr)
			{
				return X86.Avx.mm256_load_ps(ptr);
			}

			// Token: 0x06000B1B RID: 2843 RVA: 0x0000D364 File Offset: 0x0000B564
			[DebuggerStepThrough]
			public unsafe static void mm256_storeu_pd(void* ptr, v256 a)
			{
				X86.Avx.mm256_store_ps(ptr, a);
			}

			// Token: 0x06000B1C RID: 2844 RVA: 0x0000D36D File Offset: 0x0000B56D
			[DebuggerStepThrough]
			public unsafe static v256 mm256_loadu_ps(void* ptr)
			{
				return X86.Avx.mm256_load_ps(ptr);
			}

			// Token: 0x06000B1D RID: 2845 RVA: 0x0000D375 File Offset: 0x0000B575
			[DebuggerStepThrough]
			public unsafe static void mm256_storeu_ps(void* ptr, v256 a)
			{
				X86.Avx.mm256_store_ps(ptr, a);
			}

			// Token: 0x06000B1E RID: 2846 RVA: 0x0000D37E File Offset: 0x0000B57E
			[DebuggerStepThrough]
			public unsafe static v256 mm256_load_si256(void* ptr)
			{
				return X86.Avx.mm256_load_ps(ptr);
			}

			// Token: 0x06000B1F RID: 2847 RVA: 0x0000D386 File Offset: 0x0000B586
			[DebuggerStepThrough]
			public unsafe static void mm256_store_si256(void* ptr, v256 v)
			{
				X86.Avx.mm256_store_ps(ptr, v);
			}

			// Token: 0x06000B20 RID: 2848 RVA: 0x0000D38F File Offset: 0x0000B58F
			[DebuggerStepThrough]
			public unsafe static v256 mm256_loadu_si256(void* ptr)
			{
				return X86.Avx.mm256_load_ps(ptr);
			}

			// Token: 0x06000B21 RID: 2849 RVA: 0x0000D397 File Offset: 0x0000B597
			[DebuggerStepThrough]
			public unsafe static void mm256_storeu_si256(void* ptr, v256 v)
			{
				X86.Avx.mm256_store_ps(ptr, v);
			}

			// Token: 0x06000B22 RID: 2850 RVA: 0x0000D3A0 File Offset: 0x0000B5A0
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public unsafe static v256 mm256_loadu2_m128(void* hiaddr, void* loaddr)
			{
				return X86.Avx.mm256_set_m128(X86.Sse.loadu_ps(hiaddr), X86.Sse.loadu_ps(loaddr));
			}

			// Token: 0x06000B23 RID: 2851 RVA: 0x0000D3B3 File Offset: 0x0000B5B3
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public unsafe static v256 mm256_loadu2_m128d(void* hiaddr, void* loaddr)
			{
				return X86.Avx.mm256_loadu2_m128(hiaddr, loaddr);
			}

			// Token: 0x06000B24 RID: 2852 RVA: 0x0000D3BC File Offset: 0x0000B5BC
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public unsafe static v256 mm256_loadu2_m128i(void* hiaddr, void* loaddr)
			{
				return X86.Avx.mm256_loadu2_m128(hiaddr, loaddr);
			}

			// Token: 0x06000B25 RID: 2853 RVA: 0x0000D3C5 File Offset: 0x0000B5C5
			[DebuggerStepThrough]
			public static v256 mm256_set_m128(v128 hi, v128 lo)
			{
				return new v256(lo, hi);
			}

			// Token: 0x06000B26 RID: 2854 RVA: 0x0000D3CE File Offset: 0x0000B5CE
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public unsafe static void mm256_storeu2_m128(void* hiaddr, void* loaddr, v256 val)
			{
				X86.Sse.storeu_ps(hiaddr, val.Hi128);
				X86.Sse.storeu_ps(loaddr, val.Lo128);
			}

			// Token: 0x06000B27 RID: 2855 RVA: 0x0000D3E8 File Offset: 0x0000B5E8
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public unsafe static void mm256_storeu2_m128d(void* hiaddr, void* loaddr, v256 val)
			{
				X86.Sse.storeu_ps(hiaddr, val.Hi128);
				X86.Sse.storeu_ps(loaddr, val.Lo128);
			}

			// Token: 0x06000B28 RID: 2856 RVA: 0x0000D402 File Offset: 0x0000B602
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public unsafe static void mm256_storeu2_m128i(void* hiaddr, void* loaddr, v256 val)
			{
				X86.Sse.storeu_ps(hiaddr, val.Hi128);
				X86.Sse.storeu_ps(loaddr, val.Lo128);
			}

			// Token: 0x06000B29 RID: 2857 RVA: 0x0000D41C File Offset: 0x0000B61C
			[DebuggerStepThrough]
			public unsafe static v128 maskload_pd(void* mem_addr, v128 mask)
			{
				v128 result = default(v128);
				if (mask.SLong0 < 0L)
				{
					result.ULong0 = (ulong)(*(long*)mem_addr);
				}
				if (mask.SLong1 < 0L)
				{
					result.ULong1 = (ulong)(*(long*)((byte*)mem_addr + 8));
				}
				return result;
			}

			// Token: 0x06000B2A RID: 2858 RVA: 0x0000D45C File Offset: 0x0000B65C
			[DebuggerStepThrough]
			public unsafe static v256 mm256_maskload_pd(void* mem_addr, v256 mask)
			{
				return new v256(X86.Avx.maskload_pd(mem_addr, mask.Lo128), X86.Avx.maskload_pd((void*)((byte*)mem_addr + 16), mask.Hi128));
			}

			// Token: 0x06000B2B RID: 2859 RVA: 0x0000D480 File Offset: 0x0000B680
			[DebuggerStepThrough]
			public unsafe static void maskstore_pd(void* mem_addr, v128 mask, v128 a)
			{
				if (mask.SLong0 < 0L)
				{
					*(long*)mem_addr = (long)a.ULong0;
				}
				if (mask.SLong1 < 0L)
				{
					*(long*)((byte*)mem_addr + 8) = (long)a.ULong1;
				}
			}

			// Token: 0x06000B2C RID: 2860 RVA: 0x0000D4B5 File Offset: 0x0000B6B5
			[DebuggerStepThrough]
			public unsafe static void mm256_maskstore_pd(void* mem_addr, v256 mask, v256 a)
			{
				X86.Avx.maskstore_pd(mem_addr, mask.Lo128, a.Lo128);
				X86.Avx.maskstore_pd((void*)((byte*)mem_addr + 16), mask.Hi128, a.Hi128);
			}

			// Token: 0x06000B2D RID: 2861 RVA: 0x0000D4E0 File Offset: 0x0000B6E0
			[DebuggerStepThrough]
			public unsafe static v128 maskload_ps(void* mem_addr, v128 mask)
			{
				v128 result = default(v128);
				if (mask.SInt0 < 0)
				{
					result.UInt0 = *(uint*)mem_addr;
				}
				if (mask.SInt1 < 0)
				{
					result.UInt1 = *(uint*)((byte*)mem_addr + 4);
				}
				if (mask.SInt2 < 0)
				{
					result.UInt2 = *(uint*)((byte*)mem_addr + (IntPtr)2 * 4);
				}
				if (mask.SInt3 < 0)
				{
					result.UInt3 = *(uint*)((byte*)mem_addr + (IntPtr)3 * 4);
				}
				return result;
			}

			// Token: 0x06000B2E RID: 2862 RVA: 0x0000D54C File Offset: 0x0000B74C
			[DebuggerStepThrough]
			public unsafe static v256 mm256_maskload_ps(void* mem_addr, v256 mask)
			{
				return new v256(X86.Avx.maskload_ps(mem_addr, mask.Lo128), X86.Avx.maskload_ps((void*)((byte*)mem_addr + 16), mask.Hi128));
			}

			// Token: 0x06000B2F RID: 2863 RVA: 0x0000D570 File Offset: 0x0000B770
			[DebuggerStepThrough]
			public unsafe static void maskstore_ps(void* mem_addr, v128 mask, v128 a)
			{
				if (mask.SInt0 < 0)
				{
					*(int*)mem_addr = (int)a.UInt0;
				}
				if (mask.SInt1 < 0)
				{
					*(int*)((byte*)mem_addr + 4) = (int)a.UInt1;
				}
				if (mask.SInt2 < 0)
				{
					*(int*)((byte*)mem_addr + (IntPtr)2 * 4) = (int)a.UInt2;
				}
				if (mask.SInt3 < 0)
				{
					*(int*)((byte*)mem_addr + (IntPtr)3 * 4) = (int)a.UInt3;
				}
			}

			// Token: 0x06000B30 RID: 2864 RVA: 0x0000D5CF File Offset: 0x0000B7CF
			[DebuggerStepThrough]
			public unsafe static void mm256_maskstore_ps(void* mem_addr, v256 mask, v256 a)
			{
				X86.Avx.maskstore_ps(mem_addr, mask.Lo128, a.Lo128);
				X86.Avx.maskstore_ps((void*)((byte*)mem_addr + 16), mask.Hi128, a.Hi128);
			}

			// Token: 0x06000B31 RID: 2865 RVA: 0x0000D5F8 File Offset: 0x0000B7F8
			[DebuggerStepThrough]
			public static v256 mm256_movehdup_ps(v256 a)
			{
				return new v256(a.UInt1, a.UInt1, a.UInt3, a.UInt3, a.UInt5, a.UInt5, a.UInt7, a.UInt7);
			}

			// Token: 0x06000B32 RID: 2866 RVA: 0x0000D62F File Offset: 0x0000B82F
			[DebuggerStepThrough]
			public static v256 mm256_moveldup_ps(v256 a)
			{
				return new v256(a.UInt0, a.UInt0, a.UInt2, a.UInt2, a.UInt4, a.UInt4, a.UInt6, a.UInt6);
			}

			// Token: 0x06000B33 RID: 2867 RVA: 0x0000D666 File Offset: 0x0000B866
			[DebuggerStepThrough]
			public static v256 mm256_movedup_pd(v256 a)
			{
				return new v256(a.Double0, a.Double0, a.Double2, a.Double2);
			}

			// Token: 0x06000B34 RID: 2868 RVA: 0x0000D685 File Offset: 0x0000B885
			[DebuggerStepThrough]
			public unsafe static v256 mm256_lddqu_si256(void* mem_addr)
			{
				return *(v256*)mem_addr;
			}

			// Token: 0x06000B35 RID: 2869 RVA: 0x0000D68D File Offset: 0x0000B88D
			[DebuggerStepThrough]
			public unsafe static void mm256_stream_si256(void* mem_addr, v256 a)
			{
				*(v256*)mem_addr = a;
			}

			// Token: 0x06000B36 RID: 2870 RVA: 0x0000D696 File Offset: 0x0000B896
			[DebuggerStepThrough]
			public unsafe static void mm256_stream_pd(void* mem_addr, v256 a)
			{
				*(v256*)mem_addr = a;
			}

			// Token: 0x06000B37 RID: 2871 RVA: 0x0000D69F File Offset: 0x0000B89F
			[DebuggerStepThrough]
			public unsafe static void mm256_stream_ps(void* mem_addr, v256 a)
			{
				*(v256*)mem_addr = a;
			}

			// Token: 0x06000B38 RID: 2872 RVA: 0x0000D6A8 File Offset: 0x0000B8A8
			[DebuggerStepThrough]
			public static v256 mm256_rcp_ps(v256 a)
			{
				return new v256(X86.Sse.rcp_ps(a.Lo128), X86.Sse.rcp_ps(a.Hi128));
			}

			// Token: 0x06000B39 RID: 2873 RVA: 0x0000D6C5 File Offset: 0x0000B8C5
			[DebuggerStepThrough]
			public static v256 mm256_rsqrt_ps(v256 a)
			{
				return new v256(X86.Sse.rsqrt_ps(a.Lo128), X86.Sse.rsqrt_ps(a.Hi128));
			}

			// Token: 0x06000B3A RID: 2874 RVA: 0x0000D6E2 File Offset: 0x0000B8E2
			[DebuggerStepThrough]
			public static v256 mm256_sqrt_pd(v256 a)
			{
				return new v256(X86.Sse2.sqrt_pd(a.Lo128), X86.Sse2.sqrt_pd(a.Hi128));
			}

			// Token: 0x06000B3B RID: 2875 RVA: 0x0000D6FF File Offset: 0x0000B8FF
			[DebuggerStepThrough]
			public static v256 mm256_sqrt_ps(v256 a)
			{
				return new v256(X86.Sse.sqrt_ps(a.Lo128), X86.Sse.sqrt_ps(a.Hi128));
			}

			// Token: 0x06000B3C RID: 2876 RVA: 0x0000D71C File Offset: 0x0000B91C
			[DebuggerStepThrough]
			public static v256 mm256_round_pd(v256 a, int rounding)
			{
				return new v256(X86.Sse4_1.round_pd(a.Lo128, rounding), X86.Sse4_1.round_pd(a.Hi128, rounding));
			}

			// Token: 0x06000B3D RID: 2877 RVA: 0x0000D73B File Offset: 0x0000B93B
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public static v256 mm256_ceil_pd(v256 val)
			{
				return X86.Avx.mm256_round_pd(val, 2);
			}

			// Token: 0x06000B3E RID: 2878 RVA: 0x0000D744 File Offset: 0x0000B944
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public static v256 mm256_floor_pd(v256 val)
			{
				return X86.Avx.mm256_round_pd(val, 1);
			}

			// Token: 0x06000B3F RID: 2879 RVA: 0x0000D74D File Offset: 0x0000B94D
			[DebuggerStepThrough]
			public static v256 mm256_round_ps(v256 a, int rounding)
			{
				return new v256(X86.Sse4_1.round_ps(a.Lo128, rounding), X86.Sse4_1.round_ps(a.Hi128, rounding));
			}

			// Token: 0x06000B40 RID: 2880 RVA: 0x0000D76C File Offset: 0x0000B96C
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public static v256 mm256_ceil_ps(v256 val)
			{
				return X86.Avx.mm256_round_ps(val, 2);
			}

			// Token: 0x06000B41 RID: 2881 RVA: 0x0000D775 File Offset: 0x0000B975
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public static v256 mm256_floor_ps(v256 val)
			{
				return X86.Avx.mm256_round_ps(val, 1);
			}

			// Token: 0x06000B42 RID: 2882 RVA: 0x0000D77E File Offset: 0x0000B97E
			[DebuggerStepThrough]
			public static v256 mm256_unpackhi_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse2.unpackhi_pd(a.Lo128, b.Lo128), X86.Sse2.unpackhi_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B43 RID: 2883 RVA: 0x0000D7A7 File Offset: 0x0000B9A7
			[DebuggerStepThrough]
			public static v256 mm256_unpacklo_pd(v256 a, v256 b)
			{
				return new v256(X86.Sse2.unpacklo_pd(a.Lo128, b.Lo128), X86.Sse2.unpacklo_pd(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B44 RID: 2884 RVA: 0x0000D7D0 File Offset: 0x0000B9D0
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public static v256 mm256_unpackhi_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse.unpackhi_ps(a.Lo128, b.Lo128), X86.Sse.unpackhi_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B45 RID: 2885 RVA: 0x0000D7F9 File Offset: 0x0000B9F9
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public static v256 mm256_unpacklo_ps(v256 a, v256 b)
			{
				return new v256(X86.Sse.unpacklo_ps(a.Lo128, b.Lo128), X86.Sse.unpacklo_ps(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B46 RID: 2886 RVA: 0x0000D822 File Offset: 0x0000BA22
			[DebuggerStepThrough]
			public static int mm256_testz_si256(v256 a, v256 b)
			{
				return X86.Sse4_1.testz_si128(a.Lo128, b.Lo128) & X86.Sse4_1.testz_si128(a.Hi128, b.Hi128);
			}

			// Token: 0x06000B47 RID: 2887 RVA: 0x0000D847 File Offset: 0x0000BA47
			[DebuggerStepThrough]
			public static int mm256_testc_si256(v256 a, v256 b)
			{
				return X86.Sse4_1.testc_si128(a.Lo128, b.Lo128) & X86.Sse4_1.testc_si128(a.Hi128, b.Hi128);
			}

			// Token: 0x06000B48 RID: 2888 RVA: 0x0000D86C File Offset: 0x0000BA6C
			[DebuggerStepThrough]
			public static int mm256_testnzc_si256(v256 a, v256 b)
			{
				int num = X86.Avx.mm256_testz_si256(a, b);
				int num2 = X86.Avx.mm256_testc_si256(a, b);
				return 1 - (num | num2);
			}

			// Token: 0x06000B49 RID: 2889 RVA: 0x0000D890 File Offset: 0x0000BA90
			[DebuggerStepThrough]
			public unsafe static int mm256_testz_pd(v256 a, v256 b)
			{
				ulong* ptr = &a.ULong0;
				ulong* ptr2 = &b.ULong0;
				for (int i = 0; i < 4; i++)
				{
					if ((ptr[i] & ptr2[i] & 9223372036854775808UL) != 0UL)
					{
						return 0;
					}
				}
				return 1;
			}

			// Token: 0x06000B4A RID: 2890 RVA: 0x0000D8DC File Offset: 0x0000BADC
			[DebuggerStepThrough]
			public unsafe static int mm256_testc_pd(v256 a, v256 b)
			{
				ulong* ptr = &a.ULong0;
				ulong* ptr2 = &b.ULong0;
				for (int i = 0; i < 4; i++)
				{
					if ((~ptr[i] & ptr2[i] & 9223372036854775808UL) != 0UL)
					{
						return 0;
					}
				}
				return 1;
			}

			// Token: 0x06000B4B RID: 2891 RVA: 0x0000D926 File Offset: 0x0000BB26
			[DebuggerStepThrough]
			public static int mm256_testnzc_pd(v256 a, v256 b)
			{
				return 1 - (X86.Avx.mm256_testz_pd(a, b) | X86.Avx.mm256_testc_pd(a, b));
			}

			// Token: 0x06000B4C RID: 2892 RVA: 0x0000D93C File Offset: 0x0000BB3C
			[DebuggerStepThrough]
			public unsafe static int testz_pd(v128 a, v128 b)
			{
				ulong* ptr = &a.ULong0;
				ulong* ptr2 = &b.ULong0;
				for (int i = 0; i < 2; i++)
				{
					if ((ptr[i] & ptr2[i] & 9223372036854775808UL) != 0UL)
					{
						return 0;
					}
				}
				return 1;
			}

			// Token: 0x06000B4D RID: 2893 RVA: 0x0000D988 File Offset: 0x0000BB88
			[DebuggerStepThrough]
			public unsafe static int testc_pd(v128 a, v128 b)
			{
				ulong* ptr = &a.ULong0;
				ulong* ptr2 = &b.ULong0;
				for (int i = 0; i < 2; i++)
				{
					if ((~ptr[i] & ptr2[i] & 9223372036854775808UL) != 0UL)
					{
						return 0;
					}
				}
				return 1;
			}

			// Token: 0x06000B4E RID: 2894 RVA: 0x0000D9D2 File Offset: 0x0000BBD2
			[DebuggerStepThrough]
			public static int testnzc_pd(v128 a, v128 b)
			{
				return 1 - (X86.Avx.testz_pd(a, b) | X86.Avx.testc_pd(a, b));
			}

			// Token: 0x06000B4F RID: 2895 RVA: 0x0000D9E8 File Offset: 0x0000BBE8
			[DebuggerStepThrough]
			public unsafe static int mm256_testz_ps(v256 a, v256 b)
			{
				uint* ptr = &a.UInt0;
				uint* ptr2 = &b.UInt0;
				for (int i = 0; i < 8; i++)
				{
					if ((ptr[i] & ptr2[i] & 2147483648U) != 0U)
					{
						return 0;
					}
				}
				return 1;
			}

			// Token: 0x06000B50 RID: 2896 RVA: 0x0000DA30 File Offset: 0x0000BC30
			[DebuggerStepThrough]
			public unsafe static int mm256_testc_ps(v256 a, v256 b)
			{
				uint* ptr = &a.UInt0;
				uint* ptr2 = &b.UInt0;
				for (int i = 0; i < 8; i++)
				{
					if ((~ptr[i] & ptr2[i] & 2147483648U) != 0U)
					{
						return 0;
					}
				}
				return 1;
			}

			// Token: 0x06000B51 RID: 2897 RVA: 0x0000DA76 File Offset: 0x0000BC76
			[DebuggerStepThrough]
			public static int mm256_testnzc_ps(v256 a, v256 b)
			{
				return 1 - (X86.Avx.mm256_testz_ps(a, b) | X86.Avx.mm256_testc_ps(a, b));
			}

			// Token: 0x06000B52 RID: 2898 RVA: 0x0000DA8C File Offset: 0x0000BC8C
			[DebuggerStepThrough]
			public unsafe static int testz_ps(v128 a, v128 b)
			{
				uint* ptr = &a.UInt0;
				uint* ptr2 = &b.UInt0;
				for (int i = 0; i < 4; i++)
				{
					if ((ptr[i] & ptr2[i] & 2147483648U) != 0U)
					{
						return 0;
					}
				}
				return 1;
			}

			// Token: 0x06000B53 RID: 2899 RVA: 0x0000DAD4 File Offset: 0x0000BCD4
			[DebuggerStepThrough]
			public unsafe static int testc_ps(v128 a, v128 b)
			{
				uint* ptr = &a.UInt0;
				uint* ptr2 = &b.UInt0;
				for (int i = 0; i < 4; i++)
				{
					if ((~ptr[i] & ptr2[i] & 2147483648U) != 0U)
					{
						return 0;
					}
				}
				return 1;
			}

			// Token: 0x06000B54 RID: 2900 RVA: 0x0000DB1A File Offset: 0x0000BD1A
			[DebuggerStepThrough]
			public static int testnzc_ps(v128 a, v128 b)
			{
				return 1 - (X86.Avx.testz_ps(a, b) | X86.Avx.testc_ps(a, b));
			}

			// Token: 0x06000B55 RID: 2901 RVA: 0x0000DB2D File Offset: 0x0000BD2D
			[DebuggerStepThrough]
			public static int mm256_movemask_pd(v256 a)
			{
				return X86.Sse2.movemask_pd(a.Lo128) | X86.Sse2.movemask_pd(a.Hi128) << 2;
			}

			// Token: 0x06000B56 RID: 2902 RVA: 0x0000DB48 File Offset: 0x0000BD48
			[DebuggerStepThrough]
			public static int mm256_movemask_ps(v256 a)
			{
				return X86.Sse.movemask_ps(a.Lo128) | X86.Sse.movemask_ps(a.Hi128) << 4;
			}

			// Token: 0x06000B57 RID: 2903 RVA: 0x0000DB64 File Offset: 0x0000BD64
			[DebuggerStepThrough]
			public static v256 mm256_setzero_pd()
			{
				return default(v256);
			}

			// Token: 0x06000B58 RID: 2904 RVA: 0x0000DB7C File Offset: 0x0000BD7C
			[DebuggerStepThrough]
			public static v256 mm256_setzero_ps()
			{
				return default(v256);
			}

			// Token: 0x06000B59 RID: 2905 RVA: 0x0000DB94 File Offset: 0x0000BD94
			[DebuggerStepThrough]
			public static v256 mm256_setzero_si256()
			{
				return default(v256);
			}

			// Token: 0x06000B5A RID: 2906 RVA: 0x0000DBAA File Offset: 0x0000BDAA
			[DebuggerStepThrough]
			public static v256 mm256_set_pd(double d, double c, double b, double a)
			{
				return new v256(a, b, c, d);
			}

			// Token: 0x06000B5B RID: 2907 RVA: 0x0000DBB5 File Offset: 0x0000BDB5
			[DebuggerStepThrough]
			public static v256 mm256_set_ps(float e7, float e6, float e5, float e4, float e3, float e2, float e1, float e0)
			{
				return new v256(e0, e1, e2, e3, e4, e5, e6, e7);
			}

			// Token: 0x06000B5C RID: 2908 RVA: 0x0000DBC8 File Offset: 0x0000BDC8
			[DebuggerStepThrough]
			public static v256 mm256_set_epi8(byte e31_, byte e30_, byte e29_, byte e28_, byte e27_, byte e26_, byte e25_, byte e24_, byte e23_, byte e22_, byte e21_, byte e20_, byte e19_, byte e18_, byte e17_, byte e16_, byte e15_, byte e14_, byte e13_, byte e12_, byte e11_, byte e10_, byte e9_, byte e8_, byte e7_, byte e6_, byte e5_, byte e4_, byte e3_, byte e2_, byte e1_, byte e0_)
			{
				return new v256(e0_, e1_, e2_, e3_, e4_, e5_, e6_, e7_, e8_, e9_, e10_, e11_, e12_, e13_, e14_, e15_, e16_, e17_, e18_, e19_, e20_, e21_, e22_, e23_, e24_, e25_, e26_, e27_, e28_, e29_, e30_, e31_);
			}

			// Token: 0x06000B5D RID: 2909 RVA: 0x0000DC18 File Offset: 0x0000BE18
			[DebuggerStepThrough]
			public static v256 mm256_set_epi16(short e15_, short e14_, short e13_, short e12_, short e11_, short e10_, short e9_, short e8_, short e7_, short e6_, short e5_, short e4_, short e3_, short e2_, short e1_, short e0_)
			{
				return new v256(e0_, e1_, e2_, e3_, e4_, e5_, e6_, e7_, e8_, e9_, e10_, e11_, e12_, e13_, e14_, e15_);
			}

			// Token: 0x06000B5E RID: 2910 RVA: 0x0000DC46 File Offset: 0x0000BE46
			[DebuggerStepThrough]
			public static v256 mm256_set_epi32(int e7, int e6, int e5, int e4, int e3, int e2, int e1, int e0)
			{
				return new v256(e0, e1, e2, e3, e4, e5, e6, e7);
			}

			// Token: 0x06000B5F RID: 2911 RVA: 0x0000DC59 File Offset: 0x0000BE59
			[DebuggerStepThrough]
			public static v256 mm256_set_epi64x(long e3, long e2, long e1, long e0)
			{
				return new v256(e0, e1, e2, e3);
			}

			// Token: 0x06000B60 RID: 2912 RVA: 0x0000DC64 File Offset: 0x0000BE64
			[DebuggerStepThrough]
			public static v256 mm256_set_m128d(v128 hi, v128 lo)
			{
				return new v256(lo, hi);
			}

			// Token: 0x06000B61 RID: 2913 RVA: 0x0000DC6D File Offset: 0x0000BE6D
			[DebuggerStepThrough]
			public static v256 mm256_set_m128i(v128 hi, v128 lo)
			{
				return new v256(lo, hi);
			}

			// Token: 0x06000B62 RID: 2914 RVA: 0x0000DC76 File Offset: 0x0000BE76
			[DebuggerStepThrough]
			public static v256 mm256_setr_pd(double d, double c, double b, double a)
			{
				return new v256(d, c, b, a);
			}

			// Token: 0x06000B63 RID: 2915 RVA: 0x0000DC81 File Offset: 0x0000BE81
			[DebuggerStepThrough]
			public static v256 mm256_setr_ps(float e7, float e6, float e5, float e4, float e3, float e2, float e1, float e0)
			{
				return new v256(e7, e6, e5, e4, e3, e2, e1, e0);
			}

			// Token: 0x06000B64 RID: 2916 RVA: 0x0000DC94 File Offset: 0x0000BE94
			[DebuggerStepThrough]
			public static v256 mm256_setr_epi8(byte e31_, byte e30_, byte e29_, byte e28_, byte e27_, byte e26_, byte e25_, byte e24_, byte e23_, byte e22_, byte e21_, byte e20_, byte e19_, byte e18_, byte e17_, byte e16_, byte e15_, byte e14_, byte e13_, byte e12_, byte e11_, byte e10_, byte e9_, byte e8_, byte e7_, byte e6_, byte e5_, byte e4_, byte e3_, byte e2_, byte e1_, byte e0_)
			{
				return new v256(e31_, e30_, e29_, e28_, e27_, e26_, e25_, e24_, e23_, e22_, e21_, e20_, e19_, e18_, e17_, e16_, e15_, e14_, e13_, e12_, e11_, e10_, e9_, e8_, e7_, e6_, e5_, e4_, e3_, e2_, e1_, e0_);
			}

			// Token: 0x06000B65 RID: 2917 RVA: 0x0000DCE4 File Offset: 0x0000BEE4
			[DebuggerStepThrough]
			public static v256 mm256_setr_epi16(short e15_, short e14_, short e13_, short e12_, short e11_, short e10_, short e9_, short e8_, short e7_, short e6_, short e5_, short e4_, short e3_, short e2_, short e1_, short e0_)
			{
				return new v256(e15_, e14_, e13_, e12_, e11_, e10_, e9_, e8_, e7_, e6_, e5_, e4_, e3_, e2_, e1_, e0_);
			}

			// Token: 0x06000B66 RID: 2918 RVA: 0x0000DD12 File Offset: 0x0000BF12
			[DebuggerStepThrough]
			public static v256 mm256_setr_epi32(int e7, int e6, int e5, int e4, int e3, int e2, int e1, int e0)
			{
				return new v256(e7, e6, e5, e4, e3, e2, e1, e0);
			}

			// Token: 0x06000B67 RID: 2919 RVA: 0x0000DD25 File Offset: 0x0000BF25
			[DebuggerStepThrough]
			public static v256 mm256_setr_epi64x(long e3, long e2, long e1, long e0)
			{
				return new v256(e3, e2, e1, e0);
			}

			// Token: 0x06000B68 RID: 2920 RVA: 0x0000DD30 File Offset: 0x0000BF30
			[DebuggerStepThrough]
			public static v256 mm256_setr_m128(v128 hi, v128 lo)
			{
				return new v256(hi, lo);
			}

			// Token: 0x06000B69 RID: 2921 RVA: 0x0000DD39 File Offset: 0x0000BF39
			[DebuggerStepThrough]
			public static v256 mm256_setr_m128d(v128 hi, v128 lo)
			{
				return new v256(hi, lo);
			}

			// Token: 0x06000B6A RID: 2922 RVA: 0x0000DD42 File Offset: 0x0000BF42
			[DebuggerStepThrough]
			public static v256 mm256_setr_m128i(v128 hi, v128 lo)
			{
				return new v256(hi, lo);
			}

			// Token: 0x06000B6B RID: 2923 RVA: 0x0000DD4B File Offset: 0x0000BF4B
			[DebuggerStepThrough]
			public static v256 mm256_set1_pd(double a)
			{
				return new v256(a);
			}

			// Token: 0x06000B6C RID: 2924 RVA: 0x0000DD53 File Offset: 0x0000BF53
			[DebuggerStepThrough]
			public static v256 mm256_set1_ps(float a)
			{
				return new v256(a);
			}

			// Token: 0x06000B6D RID: 2925 RVA: 0x0000DD5B File Offset: 0x0000BF5B
			[DebuggerStepThrough]
			public static v256 mm256_set1_epi8(byte a)
			{
				return new v256(a);
			}

			// Token: 0x06000B6E RID: 2926 RVA: 0x0000DD63 File Offset: 0x0000BF63
			[DebuggerStepThrough]
			public static v256 mm256_set1_epi16(short a)
			{
				return new v256(a);
			}

			// Token: 0x06000B6F RID: 2927 RVA: 0x0000DD6B File Offset: 0x0000BF6B
			[DebuggerStepThrough]
			public static v256 mm256_set1_epi32(int a)
			{
				return new v256(a);
			}

			// Token: 0x06000B70 RID: 2928 RVA: 0x0000DD73 File Offset: 0x0000BF73
			[DebuggerStepThrough]
			public static v256 mm256_set1_epi64x(long a)
			{
				return new v256(a);
			}

			// Token: 0x06000B71 RID: 2929 RVA: 0x0000DD7B File Offset: 0x0000BF7B
			[DebuggerStepThrough]
			public static v256 mm256_castpd_ps(v256 a)
			{
				return a;
			}

			// Token: 0x06000B72 RID: 2930 RVA: 0x0000DD7E File Offset: 0x0000BF7E
			[DebuggerStepThrough]
			public static v256 mm256_castps_pd(v256 a)
			{
				return a;
			}

			// Token: 0x06000B73 RID: 2931 RVA: 0x0000DD81 File Offset: 0x0000BF81
			[DebuggerStepThrough]
			public static v256 mm256_castps_si256(v256 a)
			{
				return a;
			}

			// Token: 0x06000B74 RID: 2932 RVA: 0x0000DD84 File Offset: 0x0000BF84
			[DebuggerStepThrough]
			public static v256 mm256_castpd_si256(v256 a)
			{
				return a;
			}

			// Token: 0x06000B75 RID: 2933 RVA: 0x0000DD87 File Offset: 0x0000BF87
			[DebuggerStepThrough]
			public static v256 mm256_castsi256_ps(v256 a)
			{
				return a;
			}

			// Token: 0x06000B76 RID: 2934 RVA: 0x0000DD8A File Offset: 0x0000BF8A
			[DebuggerStepThrough]
			public static v256 mm256_castsi256_pd(v256 a)
			{
				return a;
			}

			// Token: 0x06000B77 RID: 2935 RVA: 0x0000DD8D File Offset: 0x0000BF8D
			[DebuggerStepThrough]
			public static v128 mm256_castps256_ps128(v256 a)
			{
				return a.Lo128;
			}

			// Token: 0x06000B78 RID: 2936 RVA: 0x0000DD95 File Offset: 0x0000BF95
			[DebuggerStepThrough]
			public static v128 mm256_castpd256_pd128(v256 a)
			{
				return a.Lo128;
			}

			// Token: 0x06000B79 RID: 2937 RVA: 0x0000DD9D File Offset: 0x0000BF9D
			[DebuggerStepThrough]
			public static v128 mm256_castsi256_si128(v256 a)
			{
				return a.Lo128;
			}

			// Token: 0x06000B7A RID: 2938 RVA: 0x0000DDA5 File Offset: 0x0000BFA5
			[DebuggerStepThrough]
			public static v256 mm256_castps128_ps256(v128 a)
			{
				return new v256(a, X86.Sse.setzero_ps());
			}

			// Token: 0x06000B7B RID: 2939 RVA: 0x0000DDB2 File Offset: 0x0000BFB2
			[DebuggerStepThrough]
			public static v256 mm256_castpd128_pd256(v128 a)
			{
				return new v256(a, X86.Sse.setzero_ps());
			}

			// Token: 0x06000B7C RID: 2940 RVA: 0x0000DDBF File Offset: 0x0000BFBF
			[DebuggerStepThrough]
			public static v256 mm256_castsi128_si256(v128 a)
			{
				return new v256(a, X86.Sse.setzero_ps());
			}

			// Token: 0x06000B7D RID: 2941 RVA: 0x0000DDCC File Offset: 0x0000BFCC
			[DebuggerStepThrough]
			public static v128 undefined_ps()
			{
				return default(v128);
			}

			// Token: 0x06000B7E RID: 2942 RVA: 0x0000DDE2 File Offset: 0x0000BFE2
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public static v128 undefined_pd()
			{
				return X86.Avx.undefined_ps();
			}

			// Token: 0x06000B7F RID: 2943 RVA: 0x0000DDE9 File Offset: 0x0000BFE9
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public static v128 undefined_si128()
			{
				return X86.Avx.undefined_ps();
			}

			// Token: 0x06000B80 RID: 2944 RVA: 0x0000DDF0 File Offset: 0x0000BFF0
			[DebuggerStepThrough]
			public static v256 mm256_undefined_ps()
			{
				return default(v256);
			}

			// Token: 0x06000B81 RID: 2945 RVA: 0x0000DE06 File Offset: 0x0000C006
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public static v256 mm256_undefined_pd()
			{
				return X86.Avx.mm256_undefined_ps();
			}

			// Token: 0x06000B82 RID: 2946 RVA: 0x0000DE0D File Offset: 0x0000C00D
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public static v256 mm256_undefined_si256()
			{
				return X86.Avx.mm256_undefined_ps();
			}

			// Token: 0x06000B83 RID: 2947 RVA: 0x0000DE14 File Offset: 0x0000C014
			[DebuggerStepThrough]
			public static v256 mm256_zextps128_ps256(v128 a)
			{
				return new v256(a, X86.Sse.setzero_ps());
			}

			// Token: 0x06000B84 RID: 2948 RVA: 0x0000DE21 File Offset: 0x0000C021
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public static v256 mm256_zextpd128_pd256(v128 a)
			{
				return X86.Avx.mm256_zextps128_ps256(a);
			}

			// Token: 0x06000B85 RID: 2949 RVA: 0x0000DE29 File Offset: 0x0000C029
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.AVX)]
			public static v256 mm256_zextsi128_si256(v128 a)
			{
				return X86.Avx.mm256_zextps128_ps256(a);
			}

			// Token: 0x06000B86 RID: 2950 RVA: 0x0000DE34 File Offset: 0x0000C034
			[DebuggerStepThrough]
			public unsafe static v256 mm256_insert_epi8(v256 a, int i, int index)
			{
				v256 result = a;
				(&result.Byte0)[index & 31] = (byte)i;
				return result;
			}

			// Token: 0x06000B87 RID: 2951 RVA: 0x0000DE54 File Offset: 0x0000C054
			[DebuggerStepThrough]
			public unsafe static v256 mm256_insert_epi16(v256 a, int i, int index)
			{
				v256 result = a;
				(&result.SShort0)[index & 15] = (short)i;
				return result;
			}

			// Token: 0x06000B88 RID: 2952 RVA: 0x0000DE78 File Offset: 0x0000C078
			[DebuggerStepThrough]
			public unsafe static v256 mm256_insert_epi32(v256 a, int i, int index)
			{
				v256 result = a;
				(&result.SInt0)[index & 7] = i;
				return result;
			}

			// Token: 0x06000B89 RID: 2953 RVA: 0x0000DE9C File Offset: 0x0000C09C
			[DebuggerStepThrough]
			public unsafe static v256 mm256_insert_epi64(v256 a, long i, int index)
			{
				v256 result = a;
				(&result.SLong0)[index & 3] = i;
				return result;
			}

			// Token: 0x06000B8A RID: 2954 RVA: 0x0000DEBD File Offset: 0x0000C0BD
			[DebuggerStepThrough]
			public unsafe static int mm256_extract_epi32(v256 a, int index)
			{
				return (&a.SInt0)[index & 7];
			}

			// Token: 0x06000B8B RID: 2955 RVA: 0x0000DECF File Offset: 0x0000C0CF
			[DebuggerStepThrough]
			public unsafe static long mm256_extract_epi64(v256 a, int index)
			{
				return (&a.SLong0)[index & 3];
			}

			// Token: 0x02000055 RID: 85
			public enum CMP
			{
				// Token: 0x04000294 RID: 660
				EQ_OQ,
				// Token: 0x04000295 RID: 661
				LT_OS,
				// Token: 0x04000296 RID: 662
				LE_OS,
				// Token: 0x04000297 RID: 663
				UNORD_Q,
				// Token: 0x04000298 RID: 664
				NEQ_UQ,
				// Token: 0x04000299 RID: 665
				NLT_US,
				// Token: 0x0400029A RID: 666
				NLE_US,
				// Token: 0x0400029B RID: 667
				ORD_Q,
				// Token: 0x0400029C RID: 668
				EQ_UQ,
				// Token: 0x0400029D RID: 669
				NGE_US,
				// Token: 0x0400029E RID: 670
				NGT_US,
				// Token: 0x0400029F RID: 671
				FALSE_OQ,
				// Token: 0x040002A0 RID: 672
				NEQ_OQ,
				// Token: 0x040002A1 RID: 673
				GE_OS,
				// Token: 0x040002A2 RID: 674
				GT_OS,
				// Token: 0x040002A3 RID: 675
				TRUE_UQ,
				// Token: 0x040002A4 RID: 676
				EQ_OS,
				// Token: 0x040002A5 RID: 677
				LT_OQ,
				// Token: 0x040002A6 RID: 678
				LE_OQ,
				// Token: 0x040002A7 RID: 679
				UNORD_S,
				// Token: 0x040002A8 RID: 680
				NEQ_US,
				// Token: 0x040002A9 RID: 681
				NLT_UQ,
				// Token: 0x040002AA RID: 682
				NLE_UQ,
				// Token: 0x040002AB RID: 683
				ORD_S,
				// Token: 0x040002AC RID: 684
				EQ_US,
				// Token: 0x040002AD RID: 685
				NGE_UQ,
				// Token: 0x040002AE RID: 686
				NGT_UQ,
				// Token: 0x040002AF RID: 687
				FALSE_OS,
				// Token: 0x040002B0 RID: 688
				NEQ_OS,
				// Token: 0x040002B1 RID: 689
				GE_OQ,
				// Token: 0x040002B2 RID: 690
				GT_OQ,
				// Token: 0x040002B3 RID: 691
				TRUE_US
			}
		}

		// Token: 0x0200003F RID: 63
		public static class Avx2
		{
			// Token: 0x17000042 RID: 66
			// (get) Token: 0x06000B8C RID: 2956 RVA: 0x0000DEE1 File Offset: 0x0000C0E1
			public static bool IsAvx2Supported
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000B8D RID: 2957 RVA: 0x0000DEE4 File Offset: 0x0000C0E4
			[DebuggerStepThrough]
			public unsafe static int mm256_movemask_epi8(v256 a)
			{
				uint num = 0U;
				byte* ptr = &a.Byte0;
				uint num2 = 1U;
				int i = 0;
				while (i < 32)
				{
					num |= ((uint)ptr[i] >> 7) * num2;
					i++;
					num2 <<= 1;
				}
				return (int)num;
			}

			// Token: 0x06000B8E RID: 2958 RVA: 0x0000DF1B File Offset: 0x0000C11B
			[DebuggerStepThrough]
			public unsafe static int mm256_extract_epi8(v256 a, int index)
			{
				return (int)(&a.Byte0)[index & 31];
			}

			// Token: 0x06000B8F RID: 2959 RVA: 0x0000DF2B File Offset: 0x0000C12B
			[DebuggerStepThrough]
			public unsafe static int mm256_extract_epi16(v256 a, int index)
			{
				return (int)(&a.UShort0)[index & 15];
			}

			// Token: 0x06000B90 RID: 2960 RVA: 0x0000DF3E File Offset: 0x0000C13E
			[DebuggerStepThrough]
			public static double mm256_cvtsd_f64(v256 a)
			{
				return a.Double0;
			}

			// Token: 0x06000B91 RID: 2961 RVA: 0x0000DF46 File Offset: 0x0000C146
			[DebuggerStepThrough]
			public static int mm256_cvtsi256_si32(v256 a)
			{
				return a.SInt0;
			}

			// Token: 0x06000B92 RID: 2962 RVA: 0x0000DF4E File Offset: 0x0000C14E
			[DebuggerStepThrough]
			public static long mm256_cvtsi256_si64(v256 a)
			{
				return a.SLong0;
			}

			// Token: 0x06000B93 RID: 2963 RVA: 0x0000DF56 File Offset: 0x0000C156
			[DebuggerStepThrough]
			public static v256 mm256_cmpeq_epi8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.cmpeq_epi8(a.Lo128, b.Lo128), X86.Sse2.cmpeq_epi8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B94 RID: 2964 RVA: 0x0000DF7F File Offset: 0x0000C17F
			[DebuggerStepThrough]
			public static v256 mm256_cmpeq_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.cmpeq_epi16(a.Lo128, b.Lo128), X86.Sse2.cmpeq_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B95 RID: 2965 RVA: 0x0000DFA8 File Offset: 0x0000C1A8
			[DebuggerStepThrough]
			public static v256 mm256_cmpeq_epi32(v256 a, v256 b)
			{
				return new v256(X86.Sse2.cmpeq_epi32(a.Lo128, b.Lo128), X86.Sse2.cmpeq_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B96 RID: 2966 RVA: 0x0000DFD1 File Offset: 0x0000C1D1
			[DebuggerStepThrough]
			public static v256 mm256_cmpeq_epi64(v256 a, v256 b)
			{
				return new v256(X86.Sse4_1.cmpeq_epi64(a.Lo128, b.Lo128), X86.Sse4_1.cmpeq_epi64(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B97 RID: 2967 RVA: 0x0000DFFA File Offset: 0x0000C1FA
			[DebuggerStepThrough]
			public static v256 mm256_cmpgt_epi8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.cmpgt_epi8(a.Lo128, b.Lo128), X86.Sse2.cmpgt_epi8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B98 RID: 2968 RVA: 0x0000E023 File Offset: 0x0000C223
			[DebuggerStepThrough]
			public static v256 mm256_cmpgt_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.cmpgt_epi16(a.Lo128, b.Lo128), X86.Sse2.cmpgt_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B99 RID: 2969 RVA: 0x0000E04C File Offset: 0x0000C24C
			[DebuggerStepThrough]
			public static v256 mm256_cmpgt_epi32(v256 a, v256 b)
			{
				return new v256(X86.Sse2.cmpgt_epi32(a.Lo128, b.Lo128), X86.Sse2.cmpgt_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B9A RID: 2970 RVA: 0x0000E075 File Offset: 0x0000C275
			[DebuggerStepThrough]
			public static v256 mm256_cmpgt_epi64(v256 a, v256 b)
			{
				return new v256(X86.Sse4_2.cmpgt_epi64(a.Lo128, b.Lo128), X86.Sse4_2.cmpgt_epi64(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B9B RID: 2971 RVA: 0x0000E09E File Offset: 0x0000C29E
			[DebuggerStepThrough]
			public static v256 mm256_max_epi8(v256 a, v256 b)
			{
				return new v256(X86.Sse4_1.max_epi8(a.Lo128, b.Lo128), X86.Sse4_1.max_epi8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B9C RID: 2972 RVA: 0x0000E0C7 File Offset: 0x0000C2C7
			[DebuggerStepThrough]
			public static v256 mm256_max_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.max_epi16(a.Lo128, b.Lo128), X86.Sse2.max_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B9D RID: 2973 RVA: 0x0000E0F0 File Offset: 0x0000C2F0
			[DebuggerStepThrough]
			public static v256 mm256_max_epi32(v256 a, v256 b)
			{
				return new v256(X86.Sse4_1.max_epi32(a.Lo128, b.Lo128), X86.Sse4_1.max_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B9E RID: 2974 RVA: 0x0000E119 File Offset: 0x0000C319
			[DebuggerStepThrough]
			public static v256 mm256_max_epu8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.max_epu8(a.Lo128, b.Lo128), X86.Sse2.max_epu8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000B9F RID: 2975 RVA: 0x0000E142 File Offset: 0x0000C342
			[DebuggerStepThrough]
			public static v256 mm256_max_epu16(v256 a, v256 b)
			{
				return new v256(X86.Sse4_1.max_epu16(a.Lo128, b.Lo128), X86.Sse4_1.max_epu16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BA0 RID: 2976 RVA: 0x0000E16B File Offset: 0x0000C36B
			[DebuggerStepThrough]
			public static v256 mm256_max_epu32(v256 a, v256 b)
			{
				return new v256(X86.Sse4_1.max_epu32(a.Lo128, b.Lo128), X86.Sse4_1.max_epu32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BA1 RID: 2977 RVA: 0x0000E194 File Offset: 0x0000C394
			[DebuggerStepThrough]
			public static v256 mm256_min_epi8(v256 a, v256 b)
			{
				return new v256(X86.Sse4_1.min_epi8(a.Lo128, b.Lo128), X86.Sse4_1.min_epi8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BA2 RID: 2978 RVA: 0x0000E1BD File Offset: 0x0000C3BD
			[DebuggerStepThrough]
			public static v256 mm256_min_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.min_epi16(a.Lo128, b.Lo128), X86.Sse2.min_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BA3 RID: 2979 RVA: 0x0000E1E6 File Offset: 0x0000C3E6
			[DebuggerStepThrough]
			public static v256 mm256_min_epi32(v256 a, v256 b)
			{
				return new v256(X86.Sse4_1.min_epi32(a.Lo128, b.Lo128), X86.Sse4_1.min_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BA4 RID: 2980 RVA: 0x0000E20F File Offset: 0x0000C40F
			[DebuggerStepThrough]
			public static v256 mm256_min_epu8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.min_epu8(a.Lo128, b.Lo128), X86.Sse2.min_epu8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BA5 RID: 2981 RVA: 0x0000E238 File Offset: 0x0000C438
			[DebuggerStepThrough]
			public static v256 mm256_min_epu16(v256 a, v256 b)
			{
				return new v256(X86.Sse4_1.min_epu16(a.Lo128, b.Lo128), X86.Sse4_1.min_epu16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BA6 RID: 2982 RVA: 0x0000E261 File Offset: 0x0000C461
			[DebuggerStepThrough]
			public static v256 mm256_min_epu32(v256 a, v256 b)
			{
				return new v256(X86.Sse4_1.min_epu32(a.Lo128, b.Lo128), X86.Sse4_1.min_epu32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BA7 RID: 2983 RVA: 0x0000E28A File Offset: 0x0000C48A
			[DebuggerStepThrough]
			public static v256 mm256_and_si256(v256 a, v256 b)
			{
				return new v256(X86.Sse2.and_si128(a.Lo128, b.Lo128), X86.Sse2.and_si128(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BA8 RID: 2984 RVA: 0x0000E2B3 File Offset: 0x0000C4B3
			[DebuggerStepThrough]
			public static v256 mm256_andnot_si256(v256 a, v256 b)
			{
				return new v256(X86.Sse2.andnot_si128(a.Lo128, b.Lo128), X86.Sse2.andnot_si128(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BA9 RID: 2985 RVA: 0x0000E2DC File Offset: 0x0000C4DC
			[DebuggerStepThrough]
			public static v256 mm256_or_si256(v256 a, v256 b)
			{
				return new v256(X86.Sse2.or_si128(a.Lo128, b.Lo128), X86.Sse2.or_si128(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BAA RID: 2986 RVA: 0x0000E305 File Offset: 0x0000C505
			[DebuggerStepThrough]
			public static v256 mm256_xor_si256(v256 a, v256 b)
			{
				return new v256(X86.Sse2.xor_si128(a.Lo128, b.Lo128), X86.Sse2.xor_si128(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BAB RID: 2987 RVA: 0x0000E32E File Offset: 0x0000C52E
			[DebuggerStepThrough]
			public static v256 mm256_abs_epi8(v256 a)
			{
				return new v256(X86.Ssse3.abs_epi8(a.Lo128), X86.Ssse3.abs_epi8(a.Hi128));
			}

			// Token: 0x06000BAC RID: 2988 RVA: 0x0000E34B File Offset: 0x0000C54B
			[DebuggerStepThrough]
			public static v256 mm256_abs_epi16(v256 a)
			{
				return new v256(X86.Ssse3.abs_epi16(a.Lo128), X86.Ssse3.abs_epi16(a.Hi128));
			}

			// Token: 0x06000BAD RID: 2989 RVA: 0x0000E368 File Offset: 0x0000C568
			[DebuggerStepThrough]
			public static v256 mm256_abs_epi32(v256 a)
			{
				return new v256(X86.Ssse3.abs_epi32(a.Lo128), X86.Ssse3.abs_epi32(a.Hi128));
			}

			// Token: 0x06000BAE RID: 2990 RVA: 0x0000E385 File Offset: 0x0000C585
			[DebuggerStepThrough]
			public static v256 mm256_add_epi8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.add_epi8(a.Lo128, b.Lo128), X86.Sse2.add_epi8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BAF RID: 2991 RVA: 0x0000E3AE File Offset: 0x0000C5AE
			[DebuggerStepThrough]
			public static v256 mm256_add_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.add_epi16(a.Lo128, b.Lo128), X86.Sse2.add_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BB0 RID: 2992 RVA: 0x0000E3D7 File Offset: 0x0000C5D7
			[DebuggerStepThrough]
			public static v256 mm256_add_epi32(v256 a, v256 b)
			{
				return new v256(X86.Sse2.add_epi32(a.Lo128, b.Lo128), X86.Sse2.add_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BB1 RID: 2993 RVA: 0x0000E400 File Offset: 0x0000C600
			[DebuggerStepThrough]
			public static v256 mm256_add_epi64(v256 a, v256 b)
			{
				return new v256(X86.Sse2.add_epi64(a.Lo128, b.Lo128), X86.Sse2.add_epi64(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BB2 RID: 2994 RVA: 0x0000E429 File Offset: 0x0000C629
			[DebuggerStepThrough]
			public static v256 mm256_adds_epi8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.adds_epi8(a.Lo128, b.Lo128), X86.Sse2.adds_epi8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BB3 RID: 2995 RVA: 0x0000E452 File Offset: 0x0000C652
			[DebuggerStepThrough]
			public static v256 mm256_adds_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.adds_epi16(a.Lo128, b.Lo128), X86.Sse2.adds_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BB4 RID: 2996 RVA: 0x0000E47B File Offset: 0x0000C67B
			[DebuggerStepThrough]
			public static v256 mm256_adds_epu8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.adds_epu8(a.Lo128, b.Lo128), X86.Sse2.adds_epu8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BB5 RID: 2997 RVA: 0x0000E4A4 File Offset: 0x0000C6A4
			[DebuggerStepThrough]
			public static v256 mm256_adds_epu16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.adds_epu16(a.Lo128, b.Lo128), X86.Sse2.adds_epu16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BB6 RID: 2998 RVA: 0x0000E4CD File Offset: 0x0000C6CD
			[DebuggerStepThrough]
			public static v256 mm256_sub_epi8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.sub_epi8(a.Lo128, b.Lo128), X86.Sse2.sub_epi8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BB7 RID: 2999 RVA: 0x0000E4F6 File Offset: 0x0000C6F6
			[DebuggerStepThrough]
			public static v256 mm256_sub_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.sub_epi16(a.Lo128, b.Lo128), X86.Sse2.sub_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BB8 RID: 3000 RVA: 0x0000E51F File Offset: 0x0000C71F
			[DebuggerStepThrough]
			public static v256 mm256_sub_epi32(v256 a, v256 b)
			{
				return new v256(X86.Sse2.sub_epi32(a.Lo128, b.Lo128), X86.Sse2.sub_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BB9 RID: 3001 RVA: 0x0000E548 File Offset: 0x0000C748
			[DebuggerStepThrough]
			public static v256 mm256_sub_epi64(v256 a, v256 b)
			{
				return new v256(X86.Sse2.sub_epi64(a.Lo128, b.Lo128), X86.Sse2.sub_epi64(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BBA RID: 3002 RVA: 0x0000E571 File Offset: 0x0000C771
			[DebuggerStepThrough]
			public static v256 mm256_subs_epi8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.subs_epi8(a.Lo128, b.Lo128), X86.Sse2.subs_epi8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BBB RID: 3003 RVA: 0x0000E59A File Offset: 0x0000C79A
			[DebuggerStepThrough]
			public static v256 mm256_subs_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.subs_epi16(a.Lo128, b.Lo128), X86.Sse2.subs_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BBC RID: 3004 RVA: 0x0000E5C3 File Offset: 0x0000C7C3
			[DebuggerStepThrough]
			public static v256 mm256_subs_epu8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.subs_epu8(a.Lo128, b.Lo128), X86.Sse2.subs_epu8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BBD RID: 3005 RVA: 0x0000E5EC File Offset: 0x0000C7EC
			[DebuggerStepThrough]
			public static v256 mm256_subs_epu16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.subs_epu16(a.Lo128, b.Lo128), X86.Sse2.subs_epu16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BBE RID: 3006 RVA: 0x0000E615 File Offset: 0x0000C815
			[DebuggerStepThrough]
			public static v256 mm256_avg_epu8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.avg_epu8(a.Lo128, b.Lo128), X86.Sse2.avg_epu8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BBF RID: 3007 RVA: 0x0000E63E File Offset: 0x0000C83E
			[DebuggerStepThrough]
			public static v256 mm256_avg_epu16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.avg_epu16(a.Lo128, b.Lo128), X86.Sse2.avg_epu16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BC0 RID: 3008 RVA: 0x0000E667 File Offset: 0x0000C867
			[DebuggerStepThrough]
			public static v256 mm256_hadd_epi16(v256 a, v256 b)
			{
				return new v256(X86.Ssse3.hadd_epi16(a.Lo128, b.Lo128), X86.Ssse3.hadd_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BC1 RID: 3009 RVA: 0x0000E690 File Offset: 0x0000C890
			[DebuggerStepThrough]
			public static v256 mm256_hadd_epi32(v256 a, v256 b)
			{
				return new v256(X86.Ssse3.hadd_epi32(a.Lo128, b.Lo128), X86.Ssse3.hadd_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BC2 RID: 3010 RVA: 0x0000E6B9 File Offset: 0x0000C8B9
			[DebuggerStepThrough]
			public static v256 mm256_hadds_epi16(v256 a, v256 b)
			{
				return new v256(X86.Ssse3.hadds_epi16(a.Lo128, b.Lo128), X86.Ssse3.hadds_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BC3 RID: 3011 RVA: 0x0000E6E2 File Offset: 0x0000C8E2
			[DebuggerStepThrough]
			public static v256 mm256_hsub_epi16(v256 a, v256 b)
			{
				return new v256(X86.Ssse3.hsub_epi16(a.Lo128, b.Lo128), X86.Ssse3.hsub_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BC4 RID: 3012 RVA: 0x0000E70B File Offset: 0x0000C90B
			[DebuggerStepThrough]
			public static v256 mm256_hsub_epi32(v256 a, v256 b)
			{
				return new v256(X86.Ssse3.hsub_epi32(a.Lo128, b.Lo128), X86.Ssse3.hsub_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BC5 RID: 3013 RVA: 0x0000E734 File Offset: 0x0000C934
			[DebuggerStepThrough]
			public static v256 mm256_hsubs_epi16(v256 a, v256 b)
			{
				return new v256(X86.Ssse3.hsubs_epi16(a.Lo128, b.Lo128), X86.Ssse3.hsubs_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BC6 RID: 3014 RVA: 0x0000E75D File Offset: 0x0000C95D
			[DebuggerStepThrough]
			public static v256 mm256_madd_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.madd_epi16(a.Lo128, b.Lo128), X86.Sse2.madd_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BC7 RID: 3015 RVA: 0x0000E786 File Offset: 0x0000C986
			[DebuggerStepThrough]
			public static v256 mm256_maddubs_epi16(v256 a, v256 b)
			{
				return new v256(X86.Ssse3.maddubs_epi16(a.Lo128, b.Lo128), X86.Ssse3.maddubs_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BC8 RID: 3016 RVA: 0x0000E7AF File Offset: 0x0000C9AF
			[DebuggerStepThrough]
			public static v256 mm256_mulhi_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.mulhi_epi16(a.Lo128, b.Lo128), X86.Sse2.mulhi_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BC9 RID: 3017 RVA: 0x0000E7D8 File Offset: 0x0000C9D8
			[DebuggerStepThrough]
			public static v256 mm256_mulhi_epu16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.mulhi_epu16(a.Lo128, b.Lo128), X86.Sse2.mulhi_epu16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BCA RID: 3018 RVA: 0x0000E801 File Offset: 0x0000CA01
			[DebuggerStepThrough]
			public static v256 mm256_mullo_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.mullo_epi16(a.Lo128, b.Lo128), X86.Sse2.mullo_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BCB RID: 3019 RVA: 0x0000E82A File Offset: 0x0000CA2A
			[DebuggerStepThrough]
			public static v256 mm256_mullo_epi32(v256 a, v256 b)
			{
				return new v256(X86.Sse4_1.mullo_epi32(a.Lo128, b.Lo128), X86.Sse4_1.mullo_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BCC RID: 3020 RVA: 0x0000E853 File Offset: 0x0000CA53
			[DebuggerStepThrough]
			public static v256 mm256_mul_epu32(v256 a, v256 b)
			{
				return new v256(X86.Sse2.mul_epu32(a.Lo128, b.Lo128), X86.Sse2.mul_epu32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BCD RID: 3021 RVA: 0x0000E87C File Offset: 0x0000CA7C
			[DebuggerStepThrough]
			public static v256 mm256_mul_epi32(v256 a, v256 b)
			{
				return new v256(X86.Sse4_1.mul_epi32(a.Lo128, b.Lo128), X86.Sse4_1.mul_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BCE RID: 3022 RVA: 0x0000E8A5 File Offset: 0x0000CAA5
			[DebuggerStepThrough]
			public static v256 mm256_sign_epi8(v256 a, v256 b)
			{
				return new v256(X86.Ssse3.sign_epi8(a.Lo128, b.Lo128), X86.Ssse3.sign_epi8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BCF RID: 3023 RVA: 0x0000E8CE File Offset: 0x0000CACE
			[DebuggerStepThrough]
			public static v256 mm256_sign_epi16(v256 a, v256 b)
			{
				return new v256(X86.Ssse3.sign_epi16(a.Lo128, b.Lo128), X86.Ssse3.sign_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BD0 RID: 3024 RVA: 0x0000E8F7 File Offset: 0x0000CAF7
			[DebuggerStepThrough]
			public static v256 mm256_sign_epi32(v256 a, v256 b)
			{
				return new v256(X86.Ssse3.sign_epi32(a.Lo128, b.Lo128), X86.Ssse3.sign_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BD1 RID: 3025 RVA: 0x0000E920 File Offset: 0x0000CB20
			[DebuggerStepThrough]
			public static v256 mm256_mulhrs_epi16(v256 a, v256 b)
			{
				return new v256(X86.Ssse3.mulhrs_epi16(a.Lo128, b.Lo128), X86.Ssse3.mulhrs_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BD2 RID: 3026 RVA: 0x0000E949 File Offset: 0x0000CB49
			[DebuggerStepThrough]
			public static v256 mm256_sad_epu8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.sad_epu8(a.Lo128, b.Lo128), X86.Sse2.sad_epu8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BD3 RID: 3027 RVA: 0x0000E972 File Offset: 0x0000CB72
			[DebuggerStepThrough]
			public static v256 mm256_mpsadbw_epu8(v256 a, v256 b, int imm8)
			{
				return new v256(X86.Sse4_1.mpsadbw_epu8(a.Lo128, b.Lo128, imm8 & 7), X86.Sse4_1.mpsadbw_epu8(a.Hi128, b.Hi128, imm8 >> 3 & 7));
			}

			// Token: 0x06000BD4 RID: 3028 RVA: 0x0000E9A3 File Offset: 0x0000CBA3
			[DebuggerStepThrough]
			public static v256 mm256_slli_si256(v256 a, int imm8)
			{
				return new v256(X86.Sse2.slli_si128(a.Lo128, imm8), X86.Sse2.slli_si128(a.Hi128, imm8));
			}

			// Token: 0x06000BD5 RID: 3029 RVA: 0x0000E9C2 File Offset: 0x0000CBC2
			[DebuggerStepThrough]
			public static v256 mm256_bslli_epi128(v256 a, int imm8)
			{
				return X86.Avx2.mm256_slli_si256(a, imm8);
			}

			// Token: 0x06000BD6 RID: 3030 RVA: 0x0000E9CB File Offset: 0x0000CBCB
			[DebuggerStepThrough]
			public static v256 mm256_srli_si256(v256 a, int imm8)
			{
				return new v256(X86.Sse2.srli_si128(a.Lo128, imm8), X86.Sse2.srli_si128(a.Hi128, imm8));
			}

			// Token: 0x06000BD7 RID: 3031 RVA: 0x0000E9EA File Offset: 0x0000CBEA
			[DebuggerStepThrough]
			public static v256 mm256_bsrli_epi128(v256 a, int imm8)
			{
				return X86.Avx2.mm256_srli_si256(a, imm8);
			}

			// Token: 0x06000BD8 RID: 3032 RVA: 0x0000E9F3 File Offset: 0x0000CBF3
			[DebuggerStepThrough]
			public static v256 mm256_sll_epi16(v256 a, v128 count)
			{
				return new v256(X86.Sse2.sll_epi16(a.Lo128, count), X86.Sse2.sll_epi16(a.Hi128, count));
			}

			// Token: 0x06000BD9 RID: 3033 RVA: 0x0000EA12 File Offset: 0x0000CC12
			[DebuggerStepThrough]
			public static v256 mm256_sll_epi32(v256 a, v128 count)
			{
				return new v256(X86.Sse2.sll_epi32(a.Lo128, count), X86.Sse2.sll_epi32(a.Hi128, count));
			}

			// Token: 0x06000BDA RID: 3034 RVA: 0x0000EA31 File Offset: 0x0000CC31
			[DebuggerStepThrough]
			public static v256 mm256_sll_epi64(v256 a, v128 count)
			{
				return new v256(X86.Sse2.sll_epi64(a.Lo128, count), X86.Sse2.sll_epi64(a.Hi128, count));
			}

			// Token: 0x06000BDB RID: 3035 RVA: 0x0000EA50 File Offset: 0x0000CC50
			[DebuggerStepThrough]
			public static v256 mm256_slli_epi16(v256 a, int imm8)
			{
				return new v256(X86.Sse2.slli_epi16(a.Lo128, imm8), X86.Sse2.slli_epi16(a.Hi128, imm8));
			}

			// Token: 0x06000BDC RID: 3036 RVA: 0x0000EA6F File Offset: 0x0000CC6F
			[DebuggerStepThrough]
			public static v256 mm256_slli_epi32(v256 a, int imm8)
			{
				return new v256(X86.Sse2.slli_epi32(a.Lo128, imm8), X86.Sse2.slli_epi32(a.Hi128, imm8));
			}

			// Token: 0x06000BDD RID: 3037 RVA: 0x0000EA8E File Offset: 0x0000CC8E
			[DebuggerStepThrough]
			public static v256 mm256_slli_epi64(v256 a, int imm8)
			{
				return new v256(X86.Sse2.slli_epi64(a.Lo128, imm8), X86.Sse2.slli_epi64(a.Hi128, imm8));
			}

			// Token: 0x06000BDE RID: 3038 RVA: 0x0000EAAD File Offset: 0x0000CCAD
			[DebuggerStepThrough]
			public static v256 mm256_sllv_epi32(v256 a, v256 count)
			{
				return new v256(X86.Avx2.sllv_epi32(a.Lo128, count.Lo128), X86.Avx2.sllv_epi32(a.Hi128, count.Hi128));
			}

			// Token: 0x06000BDF RID: 3039 RVA: 0x0000EAD6 File Offset: 0x0000CCD6
			[DebuggerStepThrough]
			public static v256 mm256_sllv_epi64(v256 a, v256 count)
			{
				return new v256(X86.Avx2.sllv_epi64(a.Lo128, count.Lo128), X86.Avx2.sllv_epi64(a.Hi128, count.Hi128));
			}

			// Token: 0x06000BE0 RID: 3040 RVA: 0x0000EB00 File Offset: 0x0000CD00
			[DebuggerStepThrough]
			public unsafe static v128 sllv_epi32(v128 a, v128 count)
			{
				v128 result = default(v128);
				uint* ptr = &a.UInt0;
				uint* ptr2 = &result.UInt0;
				int* ptr3 = &count.SInt0;
				for (int i = 0; i < 4; i++)
				{
					int num = ptr3[i];
					if (num >= 0 && num <= 31)
					{
						ptr2[i] = ptr[i] << num;
					}
					else
					{
						ptr2[i] = 0U;
					}
				}
				return result;
			}

			// Token: 0x06000BE1 RID: 3041 RVA: 0x0000EB78 File Offset: 0x0000CD78
			[DebuggerStepThrough]
			public unsafe static v128 sllv_epi64(v128 a, v128 count)
			{
				v128 result = default(v128);
				ulong* ptr = &a.ULong0;
				ulong* ptr2 = &result.ULong0;
				long* ptr3 = &count.SLong0;
				for (int i = 0; i < 2; i++)
				{
					int num = (int)ptr3[i];
					if (num >= 0 && num <= 63)
					{
						ptr2[i] = ptr[i] << num;
					}
					else
					{
						ptr2[i] = 0UL;
					}
				}
				return result;
			}

			// Token: 0x06000BE2 RID: 3042 RVA: 0x0000EBF1 File Offset: 0x0000CDF1
			[DebuggerStepThrough]
			public static v256 mm256_sra_epi16(v256 a, v128 count)
			{
				return new v256(X86.Sse2.sra_epi16(a.Lo128, count), X86.Sse2.sra_epi16(a.Hi128, count));
			}

			// Token: 0x06000BE3 RID: 3043 RVA: 0x0000EC10 File Offset: 0x0000CE10
			[DebuggerStepThrough]
			public static v256 mm256_sra_epi32(v256 a, v128 count)
			{
				return new v256(X86.Sse2.sra_epi32(a.Lo128, count), X86.Sse2.sra_epi32(a.Hi128, count));
			}

			// Token: 0x06000BE4 RID: 3044 RVA: 0x0000EC2F File Offset: 0x0000CE2F
			[DebuggerStepThrough]
			public static v256 mm256_srai_epi16(v256 a, int imm8)
			{
				return new v256(X86.Sse2.srai_epi16(a.Lo128, imm8), X86.Sse2.srai_epi16(a.Hi128, imm8));
			}

			// Token: 0x06000BE5 RID: 3045 RVA: 0x0000EC4E File Offset: 0x0000CE4E
			[DebuggerStepThrough]
			public static v256 mm256_srai_epi32(v256 a, int imm8)
			{
				return new v256(X86.Sse2.srai_epi32(a.Lo128, imm8), X86.Sse2.srai_epi32(a.Hi128, imm8));
			}

			// Token: 0x06000BE6 RID: 3046 RVA: 0x0000EC6D File Offset: 0x0000CE6D
			[DebuggerStepThrough]
			public static v256 mm256_srav_epi32(v256 a, v256 count)
			{
				return new v256(X86.Avx2.srav_epi32(a.Lo128, count.Lo128), X86.Avx2.srav_epi32(a.Hi128, count.Hi128));
			}

			// Token: 0x06000BE7 RID: 3047 RVA: 0x0000EC98 File Offset: 0x0000CE98
			[DebuggerStepThrough]
			public unsafe static v128 srav_epi32(v128 a, v128 count)
			{
				v128 result = default(v128);
				int* ptr = &a.SInt0;
				int* ptr2 = &result.SInt0;
				int* ptr3 = &count.SInt0;
				for (int i = 0; i < 4; i++)
				{
					int num = Math.Min(ptr3[i] & 255, 32);
					int num2 = 0;
					if (num >= 16)
					{
						num -= 16;
						num2 += 16;
					}
					ptr2[i] = ptr[i] >> num >> num2;
				}
				return result;
			}

			// Token: 0x06000BE8 RID: 3048 RVA: 0x0000ED23 File Offset: 0x0000CF23
			[DebuggerStepThrough]
			public static v256 mm256_srl_epi16(v256 a, v128 count)
			{
				return new v256(X86.Sse2.srl_epi16(a.Lo128, count), X86.Sse2.srl_epi16(a.Hi128, count));
			}

			// Token: 0x06000BE9 RID: 3049 RVA: 0x0000ED42 File Offset: 0x0000CF42
			[DebuggerStepThrough]
			public static v256 mm256_srl_epi32(v256 a, v128 count)
			{
				return new v256(X86.Sse2.srl_epi32(a.Lo128, count), X86.Sse2.srl_epi32(a.Hi128, count));
			}

			// Token: 0x06000BEA RID: 3050 RVA: 0x0000ED61 File Offset: 0x0000CF61
			[DebuggerStepThrough]
			public static v256 mm256_srl_epi64(v256 a, v128 count)
			{
				return new v256(X86.Sse2.srl_epi64(a.Lo128, count), X86.Sse2.srl_epi64(a.Hi128, count));
			}

			// Token: 0x06000BEB RID: 3051 RVA: 0x0000ED80 File Offset: 0x0000CF80
			[DebuggerStepThrough]
			public static v256 mm256_srli_epi16(v256 a, int imm8)
			{
				return new v256(X86.Sse2.srli_epi16(a.Lo128, imm8), X86.Sse2.srli_epi16(a.Hi128, imm8));
			}

			// Token: 0x06000BEC RID: 3052 RVA: 0x0000ED9F File Offset: 0x0000CF9F
			[DebuggerStepThrough]
			public static v256 mm256_srli_epi32(v256 a, int imm8)
			{
				return new v256(X86.Sse2.srli_epi32(a.Lo128, imm8), X86.Sse2.srli_epi32(a.Hi128, imm8));
			}

			// Token: 0x06000BED RID: 3053 RVA: 0x0000EDBE File Offset: 0x0000CFBE
			[DebuggerStepThrough]
			public static v256 mm256_srli_epi64(v256 a, int imm8)
			{
				return new v256(X86.Sse2.srli_epi64(a.Lo128, imm8), X86.Sse2.srli_epi64(a.Hi128, imm8));
			}

			// Token: 0x06000BEE RID: 3054 RVA: 0x0000EDDD File Offset: 0x0000CFDD
			[DebuggerStepThrough]
			public static v256 mm256_srlv_epi32(v256 a, v256 count)
			{
				return new v256(X86.Avx2.srlv_epi32(a.Lo128, count.Lo128), X86.Avx2.srlv_epi32(a.Hi128, count.Hi128));
			}

			// Token: 0x06000BEF RID: 3055 RVA: 0x0000EE06 File Offset: 0x0000D006
			[DebuggerStepThrough]
			public static v256 mm256_srlv_epi64(v256 a, v256 count)
			{
				return new v256(X86.Avx2.srlv_epi64(a.Lo128, count.Lo128), X86.Avx2.srlv_epi64(a.Hi128, count.Hi128));
			}

			// Token: 0x06000BF0 RID: 3056 RVA: 0x0000EE30 File Offset: 0x0000D030
			[DebuggerStepThrough]
			public unsafe static v128 srlv_epi32(v128 a, v128 count)
			{
				v128 result = default(v128);
				uint* ptr = &a.UInt0;
				uint* ptr2 = &result.UInt0;
				int* ptr3 = &count.SInt0;
				for (int i = 0; i < 4; i++)
				{
					int num = ptr3[i];
					if (num >= 0 && num <= 31)
					{
						ptr2[i] = ptr[i] >> num;
					}
					else
					{
						ptr2[i] = 0U;
					}
				}
				return result;
			}

			// Token: 0x06000BF1 RID: 3057 RVA: 0x0000EEA8 File Offset: 0x0000D0A8
			[DebuggerStepThrough]
			public unsafe static v128 srlv_epi64(v128 a, v128 count)
			{
				v128 result = default(v128);
				ulong* ptr = &a.ULong0;
				ulong* ptr2 = &result.ULong0;
				long* ptr3 = &count.SLong0;
				for (int i = 0; i < 2; i++)
				{
					int num = (int)ptr3[i];
					if (num >= 0 && num <= 63)
					{
						ptr2[i] = ptr[i] >> num;
					}
					else
					{
						ptr2[i] = 0UL;
					}
				}
				return result;
			}

			// Token: 0x06000BF2 RID: 3058 RVA: 0x0000EF21 File Offset: 0x0000D121
			[DebuggerStepThrough]
			public static v128 blend_epi32(v128 a, v128 b, int imm8)
			{
				return X86.Sse4_1.blend_ps(a, b, imm8);
			}

			// Token: 0x06000BF3 RID: 3059 RVA: 0x0000EF2B File Offset: 0x0000D12B
			[DebuggerStepThrough]
			public static v256 mm256_blend_epi32(v256 a, v256 b, int imm8)
			{
				return X86.Avx.mm256_blend_ps(a, b, imm8);
			}

			// Token: 0x06000BF4 RID: 3060 RVA: 0x0000EF35 File Offset: 0x0000D135
			[DebuggerStepThrough]
			public static v256 mm256_alignr_epi8(v256 a, v256 b, int imm8)
			{
				return new v256(X86.Ssse3.alignr_epi8(a.Lo128, b.Lo128, imm8), X86.Ssse3.alignr_epi8(a.Hi128, b.Hi128, imm8));
			}

			// Token: 0x06000BF5 RID: 3061 RVA: 0x0000EF60 File Offset: 0x0000D160
			[DebuggerStepThrough]
			public static v256 mm256_blendv_epi8(v256 a, v256 b, v256 mask)
			{
				return new v256(X86.Sse4_1.blendv_epi8(a.Lo128, b.Lo128, mask.Lo128), X86.Sse4_1.blendv_epi8(a.Hi128, b.Hi128, mask.Hi128));
			}

			// Token: 0x06000BF6 RID: 3062 RVA: 0x0000EF95 File Offset: 0x0000D195
			[DebuggerStepThrough]
			public static v256 mm256_blend_epi16(v256 a, v256 b, int imm8)
			{
				return new v256(X86.Sse4_1.blend_epi16(a.Lo128, b.Lo128, imm8), X86.Sse4_1.blend_epi16(a.Hi128, b.Hi128, imm8));
			}

			// Token: 0x06000BF7 RID: 3063 RVA: 0x0000EFC0 File Offset: 0x0000D1C0
			[DebuggerStepThrough]
			public static v256 mm256_packs_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.packs_epi16(a.Lo128, b.Lo128), X86.Sse2.packs_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BF8 RID: 3064 RVA: 0x0000EFE9 File Offset: 0x0000D1E9
			[DebuggerStepThrough]
			public static v256 mm256_packs_epi32(v256 a, v256 b)
			{
				return new v256(X86.Sse2.packs_epi32(a.Lo128, b.Lo128), X86.Sse2.packs_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BF9 RID: 3065 RVA: 0x0000F012 File Offset: 0x0000D212
			[DebuggerStepThrough]
			public static v256 mm256_packus_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.packus_epi16(a.Lo128, b.Lo128), X86.Sse2.packus_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BFA RID: 3066 RVA: 0x0000F03B File Offset: 0x0000D23B
			[DebuggerStepThrough]
			public static v256 mm256_packus_epi32(v256 a, v256 b)
			{
				return new v256(X86.Sse4_1.packus_epi32(a.Lo128, b.Lo128), X86.Sse4_1.packus_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BFB RID: 3067 RVA: 0x0000F064 File Offset: 0x0000D264
			[DebuggerStepThrough]
			public static v256 mm256_unpackhi_epi8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.unpackhi_epi8(a.Lo128, b.Lo128), X86.Sse2.unpackhi_epi8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BFC RID: 3068 RVA: 0x0000F08D File Offset: 0x0000D28D
			[DebuggerStepThrough]
			public static v256 mm256_unpackhi_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.unpackhi_epi16(a.Lo128, b.Lo128), X86.Sse2.unpackhi_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BFD RID: 3069 RVA: 0x0000F0B6 File Offset: 0x0000D2B6
			[DebuggerStepThrough]
			public static v256 mm256_unpackhi_epi32(v256 a, v256 b)
			{
				return new v256(X86.Sse2.unpackhi_epi32(a.Lo128, b.Lo128), X86.Sse2.unpackhi_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BFE RID: 3070 RVA: 0x0000F0DF File Offset: 0x0000D2DF
			[DebuggerStepThrough]
			public static v256 mm256_unpackhi_epi64(v256 a, v256 b)
			{
				return new v256(X86.Sse2.unpackhi_epi64(a.Lo128, b.Lo128), X86.Sse2.unpackhi_epi64(a.Hi128, b.Hi128));
			}

			// Token: 0x06000BFF RID: 3071 RVA: 0x0000F108 File Offset: 0x0000D308
			[DebuggerStepThrough]
			public static v256 mm256_unpacklo_epi8(v256 a, v256 b)
			{
				return new v256(X86.Sse2.unpacklo_epi8(a.Lo128, b.Lo128), X86.Sse2.unpacklo_epi8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000C00 RID: 3072 RVA: 0x0000F131 File Offset: 0x0000D331
			[DebuggerStepThrough]
			public static v256 mm256_unpacklo_epi16(v256 a, v256 b)
			{
				return new v256(X86.Sse2.unpacklo_epi16(a.Lo128, b.Lo128), X86.Sse2.unpacklo_epi16(a.Hi128, b.Hi128));
			}

			// Token: 0x06000C01 RID: 3073 RVA: 0x0000F15A File Offset: 0x0000D35A
			[DebuggerStepThrough]
			public static v256 mm256_unpacklo_epi32(v256 a, v256 b)
			{
				return new v256(X86.Sse2.unpacklo_epi32(a.Lo128, b.Lo128), X86.Sse2.unpacklo_epi32(a.Hi128, b.Hi128));
			}

			// Token: 0x06000C02 RID: 3074 RVA: 0x0000F183 File Offset: 0x0000D383
			[DebuggerStepThrough]
			public static v256 mm256_unpacklo_epi64(v256 a, v256 b)
			{
				return new v256(X86.Sse2.unpacklo_epi64(a.Lo128, b.Lo128), X86.Sse2.unpacklo_epi64(a.Hi128, b.Hi128));
			}

			// Token: 0x06000C03 RID: 3075 RVA: 0x0000F1AC File Offset: 0x0000D3AC
			[DebuggerStepThrough]
			public static v256 mm256_shuffle_epi8(v256 a, v256 b)
			{
				return new v256(X86.Ssse3.shuffle_epi8(a.Lo128, b.Lo128), X86.Ssse3.shuffle_epi8(a.Hi128, b.Hi128));
			}

			// Token: 0x06000C04 RID: 3076 RVA: 0x0000F1D5 File Offset: 0x0000D3D5
			[DebuggerStepThrough]
			public static v256 mm256_shuffle_epi32(v256 a, int imm8)
			{
				return new v256(X86.Sse2.shuffle_epi32(a.Lo128, imm8), X86.Sse2.shuffle_epi32(a.Hi128, imm8));
			}

			// Token: 0x06000C05 RID: 3077 RVA: 0x0000F1F4 File Offset: 0x0000D3F4
			[DebuggerStepThrough]
			public static v256 mm256_shufflehi_epi16(v256 a, int imm8)
			{
				return new v256(X86.Sse2.shufflehi_epi16(a.Lo128, imm8), X86.Sse2.shufflehi_epi16(a.Hi128, imm8));
			}

			// Token: 0x06000C06 RID: 3078 RVA: 0x0000F213 File Offset: 0x0000D413
			[DebuggerStepThrough]
			public static v256 mm256_shufflelo_epi16(v256 a, int imm8)
			{
				return new v256(X86.Sse2.shufflelo_epi16(a.Lo128, imm8), X86.Sse2.shufflelo_epi16(a.Hi128, imm8));
			}

			// Token: 0x06000C07 RID: 3079 RVA: 0x0000F232 File Offset: 0x0000D432
			[DebuggerStepThrough]
			public static v128 mm256_extracti128_si256(v256 a, int imm8)
			{
				return X86.Avx.mm256_extractf128_si256(a, imm8);
			}

			// Token: 0x06000C08 RID: 3080 RVA: 0x0000F23B File Offset: 0x0000D43B
			[DebuggerStepThrough]
			public static v256 mm256_inserti128_si256(v256 a, v128 b, int imm8)
			{
				return X86.Avx.mm256_insertf128_ps(a, b, imm8);
			}

			// Token: 0x06000C09 RID: 3081 RVA: 0x0000F245 File Offset: 0x0000D445
			[DebuggerStepThrough]
			public static v128 broadcastss_ps(v128 a)
			{
				return new v128(a.Float0);
			}

			// Token: 0x06000C0A RID: 3082 RVA: 0x0000F252 File Offset: 0x0000D452
			[DebuggerStepThrough]
			public static v256 mm256_broadcastss_ps(v128 a)
			{
				return new v256(a.Float0);
			}

			// Token: 0x06000C0B RID: 3083 RVA: 0x0000F25F File Offset: 0x0000D45F
			[DebuggerStepThrough]
			public static v128 broadcastsd_pd(v128 a)
			{
				return new v128(a.Double0);
			}

			// Token: 0x06000C0C RID: 3084 RVA: 0x0000F26C File Offset: 0x0000D46C
			[DebuggerStepThrough]
			public static v256 mm256_broadcastsd_pd(v128 a)
			{
				return new v256(a.Double0);
			}

			// Token: 0x06000C0D RID: 3085 RVA: 0x0000F279 File Offset: 0x0000D479
			[DebuggerStepThrough]
			public static v128 broadcastb_epi8(v128 a)
			{
				return new v128(a.Byte0);
			}

			// Token: 0x06000C0E RID: 3086 RVA: 0x0000F286 File Offset: 0x0000D486
			[DebuggerStepThrough]
			public static v128 broadcastw_epi16(v128 a)
			{
				return new v128(a.SShort0);
			}

			// Token: 0x06000C0F RID: 3087 RVA: 0x0000F293 File Offset: 0x0000D493
			[DebuggerStepThrough]
			public static v128 broadcastd_epi32(v128 a)
			{
				return new v128(a.SInt0);
			}

			// Token: 0x06000C10 RID: 3088 RVA: 0x0000F2A0 File Offset: 0x0000D4A0
			[DebuggerStepThrough]
			public static v128 broadcastq_epi64(v128 a)
			{
				return new v128(a.SLong0);
			}

			// Token: 0x06000C11 RID: 3089 RVA: 0x0000F2AD File Offset: 0x0000D4AD
			[DebuggerStepThrough]
			public static v256 mm256_broadcastb_epi8(v128 a)
			{
				return new v256(a.Byte0);
			}

			// Token: 0x06000C12 RID: 3090 RVA: 0x0000F2BA File Offset: 0x0000D4BA
			[DebuggerStepThrough]
			public static v256 mm256_broadcastw_epi16(v128 a)
			{
				return new v256(a.SShort0);
			}

			// Token: 0x06000C13 RID: 3091 RVA: 0x0000F2C7 File Offset: 0x0000D4C7
			[DebuggerStepThrough]
			public static v256 mm256_broadcastd_epi32(v128 a)
			{
				return new v256(a.SInt0);
			}

			// Token: 0x06000C14 RID: 3092 RVA: 0x0000F2D4 File Offset: 0x0000D4D4
			[DebuggerStepThrough]
			public static v256 mm256_broadcastq_epi64(v128 a)
			{
				return new v256(a.SLong0);
			}

			// Token: 0x06000C15 RID: 3093 RVA: 0x0000F2E1 File Offset: 0x0000D4E1
			[DebuggerStepThrough]
			public static v256 mm256_broadcastsi128_si256(v128 a)
			{
				return new v256(a, a);
			}

			// Token: 0x06000C16 RID: 3094 RVA: 0x0000F2EC File Offset: 0x0000D4EC
			[DebuggerStepThrough]
			public unsafe static v256 mm256_cvtepi8_epi16(v128 a)
			{
				v256 result = default(v256);
				short* ptr = &result.SShort0;
				sbyte* ptr2 = &a.SByte0;
				for (int i = 0; i <= 15; i++)
				{
					ptr[i] = (short)ptr2[i];
				}
				return result;
			}

			// Token: 0x06000C17 RID: 3095 RVA: 0x0000F32C File Offset: 0x0000D52C
			[DebuggerStepThrough]
			public unsafe static v256 mm256_cvtepi8_epi32(v128 a)
			{
				v256 result = default(v256);
				int* ptr = &result.SInt0;
				sbyte* ptr2 = &a.SByte0;
				for (int i = 0; i <= 7; i++)
				{
					ptr[i] = (int)ptr2[i];
				}
				return result;
			}

			// Token: 0x06000C18 RID: 3096 RVA: 0x0000F36C File Offset: 0x0000D56C
			[DebuggerStepThrough]
			public unsafe static v256 mm256_cvtepi8_epi64(v128 a)
			{
				v256 result = default(v256);
				long* ptr = &result.SLong0;
				sbyte* ptr2 = &a.SByte0;
				for (int i = 0; i <= 3; i++)
				{
					ptr[i] = (long)ptr2[i];
				}
				return result;
			}

			// Token: 0x06000C19 RID: 3097 RVA: 0x0000F3AC File Offset: 0x0000D5AC
			[DebuggerStepThrough]
			public unsafe static v256 mm256_cvtepi16_epi32(v128 a)
			{
				v256 result = default(v256);
				int* ptr = &result.SInt0;
				short* ptr2 = &a.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					ptr[i] = (int)ptr2[i];
				}
				return result;
			}

			// Token: 0x06000C1A RID: 3098 RVA: 0x0000F3F0 File Offset: 0x0000D5F0
			[DebuggerStepThrough]
			public unsafe static v256 mm256_cvtepi16_epi64(v128 a)
			{
				v256 result = default(v256);
				long* ptr = &result.SLong0;
				short* ptr2 = &a.SShort0;
				for (int i = 0; i <= 3; i++)
				{
					ptr[i] = (long)ptr2[i];
				}
				return result;
			}

			// Token: 0x06000C1B RID: 3099 RVA: 0x0000F434 File Offset: 0x0000D634
			[DebuggerStepThrough]
			public unsafe static v256 mm256_cvtepi32_epi64(v128 a)
			{
				v256 result = default(v256);
				long* ptr = &result.SLong0;
				int* ptr2 = &a.SInt0;
				for (int i = 0; i <= 3; i++)
				{
					ptr[i] = (long)ptr2[i];
				}
				return result;
			}

			// Token: 0x06000C1C RID: 3100 RVA: 0x0000F478 File Offset: 0x0000D678
			[DebuggerStepThrough]
			public unsafe static v256 mm256_cvtepu8_epi16(v128 a)
			{
				v256 result = default(v256);
				short* ptr = &result.SShort0;
				byte* ptr2 = &a.Byte0;
				for (int i = 0; i <= 15; i++)
				{
					ptr[i] = (short)ptr2[i];
				}
				return result;
			}

			// Token: 0x06000C1D RID: 3101 RVA: 0x0000F4B8 File Offset: 0x0000D6B8
			[DebuggerStepThrough]
			public unsafe static v256 mm256_cvtepu8_epi32(v128 a)
			{
				v256 result = default(v256);
				int* ptr = &result.SInt0;
				byte* ptr2 = &a.Byte0;
				for (int i = 0; i <= 7; i++)
				{
					ptr[i] = (int)ptr2[i];
				}
				return result;
			}

			// Token: 0x06000C1E RID: 3102 RVA: 0x0000F4F8 File Offset: 0x0000D6F8
			[DebuggerStepThrough]
			public unsafe static v256 mm256_cvtepu8_epi64(v128 a)
			{
				v256 result = default(v256);
				long* ptr = &result.SLong0;
				byte* ptr2 = &a.Byte0;
				for (int i = 0; i <= 3; i++)
				{
					ptr[i] = (long)((ulong)ptr2[i]);
				}
				return result;
			}

			// Token: 0x06000C1F RID: 3103 RVA: 0x0000F538 File Offset: 0x0000D738
			[DebuggerStepThrough]
			public unsafe static v256 mm256_cvtepu16_epi32(v128 a)
			{
				v256 result = default(v256);
				int* ptr = &result.SInt0;
				ushort* ptr2 = &a.UShort0;
				for (int i = 0; i <= 7; i++)
				{
					ptr[i] = (int)ptr2[i];
				}
				return result;
			}

			// Token: 0x06000C20 RID: 3104 RVA: 0x0000F57C File Offset: 0x0000D77C
			[DebuggerStepThrough]
			public unsafe static v256 mm256_cvtepu16_epi64(v128 a)
			{
				v256 result = default(v256);
				long* ptr = &result.SLong0;
				ushort* ptr2 = &a.UShort0;
				for (int i = 0; i <= 3; i++)
				{
					ptr[i] = (long)((ulong)ptr2[i]);
				}
				return result;
			}

			// Token: 0x06000C21 RID: 3105 RVA: 0x0000F5C0 File Offset: 0x0000D7C0
			[DebuggerStepThrough]
			public unsafe static v256 mm256_cvtepu32_epi64(v128 a)
			{
				v256 result = default(v256);
				long* ptr = &result.SLong0;
				uint* ptr2 = &a.UInt0;
				for (int i = 0; i <= 3; i++)
				{
					ptr[i] = (long)((ulong)ptr2[i]);
				}
				return result;
			}

			// Token: 0x06000C22 RID: 3106 RVA: 0x0000F604 File Offset: 0x0000D804
			[DebuggerStepThrough]
			public unsafe static v128 maskload_epi32(void* mem_addr, v128 mask)
			{
				v128 result = default(v128);
				int* ptr = &mask.SInt0;
				int* ptr2 = &result.SInt0;
				for (int i = 0; i < 4; i++)
				{
					if (ptr[i] < 0)
					{
						ptr2[i] = *(int*)((byte*)mem_addr + (IntPtr)i * 4);
					}
				}
				return result;
			}

			// Token: 0x06000C23 RID: 3107 RVA: 0x0000F65C File Offset: 0x0000D85C
			[DebuggerStepThrough]
			public unsafe static v128 maskload_epi64(void* mem_addr, v128 mask)
			{
				v128 result = default(v128);
				long* ptr = &mask.SLong0;
				long* ptr2 = &result.SLong0;
				for (int i = 0; i < 2; i++)
				{
					if (ptr[i] < 0L)
					{
						ptr2[i] = *(long*)((byte*)mem_addr + (IntPtr)i * 8);
					}
				}
				return result;
			}

			// Token: 0x06000C24 RID: 3108 RVA: 0x0000F6B4 File Offset: 0x0000D8B4
			[DebuggerStepThrough]
			public unsafe static void maskstore_epi32(void* mem_addr, v128 mask, v128 a)
			{
				int* ptr = &mask.SInt0;
				int* ptr2 = &a.SInt0;
				for (int i = 0; i < 4; i++)
				{
					if (ptr[i] < 0)
					{
						*(int*)((byte*)mem_addr + (IntPtr)i * 4) = ptr2[i];
					}
				}
			}

			// Token: 0x06000C25 RID: 3109 RVA: 0x0000F6FC File Offset: 0x0000D8FC
			[DebuggerStepThrough]
			public unsafe static void maskstore_epi64(void* mem_addr, v128 mask, v128 a)
			{
				long* ptr = &mask.SLong0;
				long* ptr2 = &a.SLong0;
				for (int i = 0; i < 2; i++)
				{
					if (ptr[i] < 0L)
					{
						*(long*)((byte*)mem_addr + (IntPtr)i * 8) = ptr2[i];
					}
				}
			}

			// Token: 0x06000C26 RID: 3110 RVA: 0x0000F744 File Offset: 0x0000D944
			[DebuggerStepThrough]
			public unsafe static v256 mm256_maskload_epi32(void* mem_addr, v256 mask)
			{
				v256 result = default(v256);
				int* ptr = &mask.SInt0;
				int* ptr2 = &result.SInt0;
				for (int i = 0; i < 8; i++)
				{
					if (ptr[i] < 0)
					{
						ptr2[i] = *(int*)((byte*)mem_addr + (IntPtr)i * 4);
					}
				}
				return result;
			}

			// Token: 0x06000C27 RID: 3111 RVA: 0x0000F79C File Offset: 0x0000D99C
			[DebuggerStepThrough]
			public unsafe static v256 mm256_maskload_epi64(void* mem_addr, v256 mask)
			{
				v256 result = default(v256);
				long* ptr = &mask.SLong0;
				long* ptr2 = &result.SLong0;
				for (int i = 0; i < 4; i++)
				{
					if (ptr[i] < 0L)
					{
						ptr2[i] = *(long*)((byte*)mem_addr + (IntPtr)i * 8);
					}
				}
				return result;
			}

			// Token: 0x06000C28 RID: 3112 RVA: 0x0000F7F4 File Offset: 0x0000D9F4
			[DebuggerStepThrough]
			public unsafe static void mm256_maskstore_epi32(void* mem_addr, v256 mask, v256 a)
			{
				int* ptr = &mask.SInt0;
				int* ptr2 = &a.SInt0;
				for (int i = 0; i < 8; i++)
				{
					if (ptr[i] < 0)
					{
						*(int*)((byte*)mem_addr + (IntPtr)i * 4) = ptr2[i];
					}
				}
			}

			// Token: 0x06000C29 RID: 3113 RVA: 0x0000F83C File Offset: 0x0000DA3C
			[DebuggerStepThrough]
			public unsafe static void mm256_maskstore_epi64(void* mem_addr, v256 mask, v256 a)
			{
				long* ptr = &mask.SLong0;
				long* ptr2 = &a.SLong0;
				for (int i = 0; i < 4; i++)
				{
					if (ptr[i] < 0L)
					{
						*(long*)((byte*)mem_addr + (IntPtr)i * 8) = ptr2[i];
					}
				}
			}

			// Token: 0x06000C2A RID: 3114 RVA: 0x0000F884 File Offset: 0x0000DA84
			[DebuggerStepThrough]
			public unsafe static v256 mm256_permutevar8x32_epi32(v256 a, v256 idx)
			{
				v256 result = default(v256);
				int* ptr = &idx.SInt0;
				int* ptr2 = &a.SInt0;
				int* ptr3 = &result.SInt0;
				for (int i = 0; i < 8; i++)
				{
					int num = ptr[i] & 7;
					ptr3[i] = ptr2[num];
				}
				return result;
			}

			// Token: 0x06000C2B RID: 3115 RVA: 0x0000F8E1 File Offset: 0x0000DAE1
			[DebuggerStepThrough]
			public static v256 mm256_permutevar8x32_ps(v256 a, v256 idx)
			{
				return X86.Avx2.mm256_permutevar8x32_epi32(a, idx);
			}

			// Token: 0x06000C2C RID: 3116 RVA: 0x0000F8EC File Offset: 0x0000DAEC
			[DebuggerStepThrough]
			public unsafe static v256 mm256_permute4x64_epi64(v256 a, int imm8)
			{
				v256 result = default(v256);
				long* ptr = &a.SLong0;
				long* ptr2 = &result.SLong0;
				int i = 0;
				while (i < 4)
				{
					ptr2[i] = ptr[imm8 & 3];
					i++;
					imm8 >>= 2;
				}
				return result;
			}

			// Token: 0x06000C2D RID: 3117 RVA: 0x0000F935 File Offset: 0x0000DB35
			[DebuggerStepThrough]
			public static v256 mm256_permute4x64_pd(v256 a, int imm8)
			{
				return X86.Avx2.mm256_permute4x64_epi64(a, imm8);
			}

			// Token: 0x06000C2E RID: 3118 RVA: 0x0000F93E File Offset: 0x0000DB3E
			[DebuggerStepThrough]
			public static v256 mm256_permute2x128_si256(v256 a, v256 b, int imm8)
			{
				return X86.Avx.mm256_permute2f128_si256(a, b, imm8);
			}

			// Token: 0x06000C2F RID: 3119 RVA: 0x0000F948 File Offset: 0x0000DB48
			[DebuggerStepThrough]
			public unsafe static v256 mm256_stream_load_si256(void* mem_addr)
			{
				return *(v256*)mem_addr;
			}

			// Token: 0x06000C30 RID: 3120 RVA: 0x0000F950 File Offset: 0x0000DB50
			private unsafe static void EmulatedGather<[IsUnmanaged] T, [IsUnmanaged] U>(T* dptr, void* base_addr, long* indexPtr, int scale, int n, U* mask) where T : struct, ValueType where U : struct, ValueType, IComparable<U>
			{
				U other = default(U);
				for (int i = 0; i < n; i++)
				{
					long num = indexPtr[i] * (long)scale;
					T* ptr = (T*)((byte*)base_addr + num);
					if (mask == null || mask[(IntPtr)i * (IntPtr)sizeof(U) / (IntPtr)sizeof(U)].CompareTo(other) < 0)
					{
						dptr[(IntPtr)i * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = *ptr;
					}
				}
			}

			// Token: 0x06000C31 RID: 3121 RVA: 0x0000F9BC File Offset: 0x0000DBBC
			private unsafe static void EmulatedGather<[IsUnmanaged] T, [IsUnmanaged] U>(T* dptr, void* base_addr, int* indexPtr, int scale, int n, U* mask) where T : struct, ValueType where U : struct, ValueType, IComparable<U>
			{
				U other = default(U);
				for (int i = 0; i < n; i++)
				{
					long num = (long)indexPtr[i] * (long)scale;
					T* ptr = (T*)((byte*)base_addr + num);
					if (mask == null || mask[(IntPtr)i * (IntPtr)sizeof(U) / (IntPtr)sizeof(U)].CompareTo(other) < 0)
					{
						dptr[(IntPtr)i * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = *ptr;
					}
				}
			}

			// Token: 0x06000C32 RID: 3122 RVA: 0x0000FA28 File Offset: 0x0000DC28
			[DebuggerStepThrough]
			public unsafe static v256 mm256_i32gather_epi32(void* base_addr, v256 vindex, int scale)
			{
				v256 result = default(v256);
				X86.Avx2.EmulatedGather<int, int>(&result.SInt0, base_addr, &vindex.SInt0, scale, sizeof(v256) / 4, null);
				return result;
			}

			// Token: 0x06000C33 RID: 3123 RVA: 0x0000FA60 File Offset: 0x0000DC60
			[DebuggerStepThrough]
			public unsafe static v256 mm256_i32gather_pd(void* base_addr, v128 vindex, int scale)
			{
				v256 result = default(v256);
				X86.Avx2.EmulatedGather<double, long>(&result.Double0, base_addr, &vindex.SInt0, scale, 4, null);
				return result;
			}

			// Token: 0x06000C34 RID: 3124 RVA: 0x0000FA90 File Offset: 0x0000DC90
			[DebuggerStepThrough]
			public unsafe static v256 mm256_i32gather_ps(void* base_addr, v256 vindex, int scale)
			{
				v256 result = default(v256);
				X86.Avx2.EmulatedGather<float, int>(&result.Float0, base_addr, &vindex.SInt0, scale, 8, null);
				return result;
			}

			// Token: 0x06000C35 RID: 3125 RVA: 0x0000FAC0 File Offset: 0x0000DCC0
			[DebuggerStepThrough]
			public unsafe static v256 mm256_i64gather_pd(void* base_addr, v256 vindex, int scale)
			{
				v256 result = default(v256);
				X86.Avx2.EmulatedGather<double, long>(&result.Double0, base_addr, &vindex.SLong0, scale, 4, null);
				return result;
			}

			// Token: 0x06000C36 RID: 3126 RVA: 0x0000FAF0 File Offset: 0x0000DCF0
			[DebuggerStepThrough]
			public unsafe static v128 mm256_i64gather_ps(void* base_addr, v256 vindex, int scale)
			{
				v128 result = default(v128);
				X86.Avx2.EmulatedGather<float, int>(&result.Float0, base_addr, &vindex.SLong0, scale, 4, null);
				return result;
			}

			// Token: 0x06000C37 RID: 3127 RVA: 0x0000FB20 File Offset: 0x0000DD20
			[DebuggerStepThrough]
			public unsafe static v128 i32gather_pd(void* base_addr, v128 vindex, int scale)
			{
				v128 result = default(v128);
				X86.Avx2.EmulatedGather<double, long>(&result.Double0, base_addr, &vindex.SInt0, scale, 2, null);
				return result;
			}

			// Token: 0x06000C38 RID: 3128 RVA: 0x0000FB50 File Offset: 0x0000DD50
			[DebuggerStepThrough]
			public unsafe static v128 i32gather_ps(void* base_addr, v128 vindex, int scale)
			{
				v128 result = default(v128);
				X86.Avx2.EmulatedGather<float, int>(&result.Float0, base_addr, &vindex.SInt0, scale, 4, null);
				return result;
			}

			// Token: 0x06000C39 RID: 3129 RVA: 0x0000FB80 File Offset: 0x0000DD80
			[DebuggerStepThrough]
			public unsafe static v128 i64gather_pd(void* base_addr, v128 vindex, int scale)
			{
				v128 result = default(v128);
				X86.Avx2.EmulatedGather<double, long>(&result.Double0, base_addr, &vindex.SLong0, scale, 2, null);
				return result;
			}

			// Token: 0x06000C3A RID: 3130 RVA: 0x0000FBB0 File Offset: 0x0000DDB0
			[DebuggerStepThrough]
			public unsafe static v128 i64gather_ps(void* base_addr, v128 vindex, int scale)
			{
				v128 result = default(v128);
				X86.Avx2.EmulatedGather<float, int>(&result.Float0, base_addr, &vindex.SLong0, scale, 2, null);
				return result;
			}

			// Token: 0x06000C3B RID: 3131 RVA: 0x0000FBE0 File Offset: 0x0000DDE0
			[DebuggerStepThrough]
			public unsafe static v256 mm256_i32gather_epi64(void* base_addr, v128 vindex, int scale)
			{
				v256 result = default(v256);
				X86.Avx2.EmulatedGather<long, long>(&result.SLong0, base_addr, &vindex.SInt0, scale, 4, null);
				return result;
			}

			// Token: 0x06000C3C RID: 3132 RVA: 0x0000FC10 File Offset: 0x0000DE10
			[DebuggerStepThrough]
			public unsafe static v128 mm256_i64gather_epi32(void* base_addr, v256 vindex, int scale)
			{
				v128 result = default(v128);
				X86.Avx2.EmulatedGather<int, int>(&result.SInt0, base_addr, &vindex.SLong0, scale, 4, null);
				return result;
			}

			// Token: 0x06000C3D RID: 3133 RVA: 0x0000FC40 File Offset: 0x0000DE40
			[DebuggerStepThrough]
			public unsafe static v256 mm256_i64gather_epi64(void* base_addr, v256 vindex, int scale)
			{
				v256 result = default(v256);
				X86.Avx2.EmulatedGather<long, long>(&result.SLong0, base_addr, &vindex.SLong0, scale, 4, null);
				return result;
			}

			// Token: 0x06000C3E RID: 3134 RVA: 0x0000FC70 File Offset: 0x0000DE70
			[DebuggerStepThrough]
			public unsafe static v128 i32gather_epi32(void* base_addr, v128 vindex, int scale)
			{
				v128 result = default(v128);
				X86.Avx2.EmulatedGather<int, int>(&result.SInt0, base_addr, &vindex.SInt0, scale, 4, null);
				return result;
			}

			// Token: 0x06000C3F RID: 3135 RVA: 0x0000FCA0 File Offset: 0x0000DEA0
			[DebuggerStepThrough]
			public unsafe static v128 i32gather_epi64(void* base_addr, v128 vindex, int scale)
			{
				v128 result = default(v128);
				X86.Avx2.EmulatedGather<long, long>(&result.SLong0, base_addr, &vindex.SInt0, scale, 2, null);
				return result;
			}

			// Token: 0x06000C40 RID: 3136 RVA: 0x0000FCD0 File Offset: 0x0000DED0
			[DebuggerStepThrough]
			public unsafe static v128 i64gather_epi32(void* base_addr, v128 vindex, int scale)
			{
				v128 result = default(v128);
				X86.Avx2.EmulatedGather<int, int>(&result.SInt0, base_addr, &vindex.SLong0, scale, 2, null);
				return result;
			}

			// Token: 0x06000C41 RID: 3137 RVA: 0x0000FD00 File Offset: 0x0000DF00
			[DebuggerStepThrough]
			public unsafe static v128 i64gather_epi64(void* base_addr, v128 vindex, int scale)
			{
				v128 result = default(v128);
				X86.Avx2.EmulatedGather<long, long>(&result.SLong0, base_addr, &vindex.SLong0, scale, 2, null);
				return result;
			}

			// Token: 0x06000C42 RID: 3138 RVA: 0x0000FD30 File Offset: 0x0000DF30
			[DebuggerStepThrough]
			public unsafe static v256 mm256_mask_i32gather_pd(v256 src, void* base_addr, v128 vindex, v256 mask, int scale)
			{
				v256 result = src;
				X86.Avx2.EmulatedGather<double, long>(&result.Double0, base_addr, &vindex.SInt0, scale, 4, &mask.SLong0);
				return result;
			}

			// Token: 0x06000C43 RID: 3139 RVA: 0x0000FD64 File Offset: 0x0000DF64
			[DebuggerStepThrough]
			public unsafe static v256 mm256_mask_i32gather_ps(v256 src, void* base_addr, v256 vindex, v256 mask, int scale)
			{
				v256 result = src;
				X86.Avx2.EmulatedGather<float, int>(&result.Float0, base_addr, &vindex.SInt0, scale, 8, &mask.SInt0);
				return result;
			}

			// Token: 0x06000C44 RID: 3140 RVA: 0x0000FD98 File Offset: 0x0000DF98
			[DebuggerStepThrough]
			public unsafe static v256 mm256_mask_i64gather_pd(v256 src, void* base_addr, v256 vindex, v256 mask, int scale)
			{
				v256 result = src;
				X86.Avx2.EmulatedGather<double, long>(&result.Double0, base_addr, &vindex.SLong0, scale, 4, &mask.SLong0);
				return result;
			}

			// Token: 0x06000C45 RID: 3141 RVA: 0x0000FDCC File Offset: 0x0000DFCC
			[DebuggerStepThrough]
			public unsafe static v128 mm256_mask_i64gather_ps(v128 src, void* base_addr, v256 vindex, v128 mask, int scale)
			{
				v128 result = src;
				X86.Avx2.EmulatedGather<float, int>(&result.Float0, base_addr, &vindex.SLong0, scale, 4, &mask.SInt0);
				return result;
			}

			// Token: 0x06000C46 RID: 3142 RVA: 0x0000FE00 File Offset: 0x0000E000
			[DebuggerStepThrough]
			public unsafe static v256 mm256_mask_i32gather_epi32(v256 src, void* base_addr, v256 vindex, v256 mask, int scale)
			{
				v256 result = src;
				X86.Avx2.EmulatedGather<int, int>(&result.SInt0, base_addr, &vindex.SInt0, scale, 8, &mask.SInt0);
				return result;
			}

			// Token: 0x06000C47 RID: 3143 RVA: 0x0000FE34 File Offset: 0x0000E034
			[DebuggerStepThrough]
			public unsafe static v256 mm256_mask_i32gather_epi64(v256 src, void* base_addr, v128 vindex, v256 mask, int scale)
			{
				v256 result = src;
				X86.Avx2.EmulatedGather<long, long>(&result.SLong0, base_addr, &vindex.SInt0, scale, 4, &mask.SLong0);
				return result;
			}

			// Token: 0x06000C48 RID: 3144 RVA: 0x0000FE68 File Offset: 0x0000E068
			[DebuggerStepThrough]
			public unsafe static v256 mm256_mask_i64gather_epi64(v256 src, void* base_addr, v256 vindex, v256 mask, int scale)
			{
				v256 result = src;
				X86.Avx2.EmulatedGather<long, long>(&result.SLong0, base_addr, &vindex.SLong0, scale, 4, &mask.SLong0);
				return result;
			}

			// Token: 0x06000C49 RID: 3145 RVA: 0x0000FE9C File Offset: 0x0000E09C
			[DebuggerStepThrough]
			public unsafe static v128 mm256_mask_i64gather_epi32(v128 src, void* base_addr, v256 vindex, v128 mask, int scale)
			{
				v128 result = src;
				X86.Avx2.EmulatedGather<int, int>(&result.SInt0, base_addr, &vindex.SLong0, scale, 4, &mask.SInt0);
				return result;
			}

			// Token: 0x06000C4A RID: 3146 RVA: 0x0000FED0 File Offset: 0x0000E0D0
			[DebuggerStepThrough]
			public unsafe static v128 mask_i32gather_pd(v128 src, void* base_addr, v128 vindex, v128 mask, int scale)
			{
				v128 result = src;
				X86.Avx2.EmulatedGather<double, long>(&result.Double0, base_addr, &vindex.SInt0, scale, 2, &mask.SLong0);
				return result;
			}

			// Token: 0x06000C4B RID: 3147 RVA: 0x0000FF04 File Offset: 0x0000E104
			[DebuggerStepThrough]
			public unsafe static v128 mask_i32gather_ps(v128 src, void* base_addr, v128 vindex, v128 mask, int scale)
			{
				v128 result = src;
				X86.Avx2.EmulatedGather<float, int>(&result.Float0, base_addr, &vindex.SInt0, scale, 4, &mask.SInt0);
				return result;
			}

			// Token: 0x06000C4C RID: 3148 RVA: 0x0000FF38 File Offset: 0x0000E138
			[DebuggerStepThrough]
			public unsafe static v128 mask_i64gather_pd(v128 src, void* base_addr, v128 vindex, v128 mask, int scale)
			{
				v128 result = src;
				X86.Avx2.EmulatedGather<double, long>(&result.Double0, base_addr, &vindex.SLong0, scale, 2, &mask.SLong0);
				return result;
			}

			// Token: 0x06000C4D RID: 3149 RVA: 0x0000FF6C File Offset: 0x0000E16C
			[DebuggerStepThrough]
			public unsafe static v128 mask_i64gather_ps(v128 src, void* base_addr, v128 vindex, v128 mask, int scale)
			{
				v128 result = src;
				result.UInt2 = (result.UInt3 = 0U);
				X86.Avx2.EmulatedGather<float, int>(&result.Float0, base_addr, &vindex.SLong0, scale, 2, &mask.SInt0);
				return result;
			}

			// Token: 0x06000C4E RID: 3150 RVA: 0x0000FFB0 File Offset: 0x0000E1B0
			[DebuggerStepThrough]
			public unsafe static v128 mask_i32gather_epi32(v128 src, void* base_addr, v128 vindex, v128 mask, int scale)
			{
				v128 result = src;
				X86.Avx2.EmulatedGather<int, int>(&result.SInt0, base_addr, &vindex.SInt0, scale, 4, &mask.SInt0);
				return result;
			}

			// Token: 0x06000C4F RID: 3151 RVA: 0x0000FFE4 File Offset: 0x0000E1E4
			[DebuggerStepThrough]
			public unsafe static v128 mask_i32gather_epi64(v128 src, void* base_addr, v128 vindex, v128 mask, int scale)
			{
				v128 result = src;
				X86.Avx2.EmulatedGather<long, long>(&result.SLong0, base_addr, &vindex.SInt0, scale, 2, &mask.SLong0);
				return result;
			}

			// Token: 0x06000C50 RID: 3152 RVA: 0x00010018 File Offset: 0x0000E218
			[DebuggerStepThrough]
			public unsafe static v128 mask_i64gather_epi32(v128 src, void* base_addr, v128 vindex, v128 mask, int scale)
			{
				v128 result = src;
				result.UInt2 = (result.UInt3 = 0U);
				X86.Avx2.EmulatedGather<int, int>(&result.SInt0, base_addr, &vindex.SLong0, scale, 2, &mask.SInt0);
				return result;
			}

			// Token: 0x06000C51 RID: 3153 RVA: 0x0001005C File Offset: 0x0000E25C
			[DebuggerStepThrough]
			public unsafe static v128 mask_i64gather_epi64(v128 src, void* base_addr, v128 vindex, v128 mask, int scale)
			{
				v128 result = src;
				X86.Avx2.EmulatedGather<long, long>(&result.SLong0, base_addr, &vindex.SLong0, scale, 2, &mask.SLong0);
				return result;
			}
		}

		// Token: 0x02000040 RID: 64
		public static class Bmi1
		{
			// Token: 0x17000043 RID: 67
			// (get) Token: 0x06000C52 RID: 3154 RVA: 0x0001008D File Offset: 0x0000E28D
			public static bool IsBmi1Supported
			{
				get
				{
					return X86.Avx2.IsAvx2Supported;
				}
			}

			// Token: 0x06000C53 RID: 3155 RVA: 0x00010094 File Offset: 0x0000E294
			[DebuggerStepThrough]
			public static uint andn_u32(uint a, uint b)
			{
				return ~a & b;
			}

			// Token: 0x06000C54 RID: 3156 RVA: 0x0001009A File Offset: 0x0000E29A
			[DebuggerStepThrough]
			public static ulong andn_u64(ulong a, ulong b)
			{
				return ~a & b;
			}

			// Token: 0x06000C55 RID: 3157 RVA: 0x000100A0 File Offset: 0x0000E2A0
			[DebuggerStepThrough]
			public static uint bextr_u32(uint a, uint start, uint len)
			{
				start &= 255U;
				if (start >= 32U)
				{
					return 0U;
				}
				uint num = a >> (int)start;
				len &= 255U;
				if (len >= 32U)
				{
					return num;
				}
				return num & (1U << (int)len) - 1U;
			}

			// Token: 0x06000C56 RID: 3158 RVA: 0x000100E0 File Offset: 0x0000E2E0
			[DebuggerStepThrough]
			public static ulong bextr_u64(ulong a, uint start, uint len)
			{
				start &= 255U;
				if (start >= 64U)
				{
					return 0UL;
				}
				ulong num = a >> (int)start;
				len &= 255U;
				if (len >= 64U)
				{
					return num;
				}
				return num & (1UL << (int)len) - 1UL;
			}

			// Token: 0x06000C57 RID: 3159 RVA: 0x00010124 File Offset: 0x0000E324
			[DebuggerStepThrough]
			public static uint bextr2_u32(uint a, uint control)
			{
				uint start = control & 255U;
				uint len = control >> 8 & 255U;
				return X86.Bmi1.bextr_u32(a, start, len);
			}

			// Token: 0x06000C58 RID: 3160 RVA: 0x0001014C File Offset: 0x0000E34C
			[DebuggerStepThrough]
			public static ulong bextr2_u64(ulong a, ulong control)
			{
				uint start = (uint)(control & 255UL);
				uint len = (uint)(control >> 8 & 255UL);
				return X86.Bmi1.bextr_u64(a, start, len);
			}

			// Token: 0x06000C59 RID: 3161 RVA: 0x00010177 File Offset: 0x0000E377
			[DebuggerStepThrough]
			public static uint blsi_u32(uint a)
			{
				return -a & a;
			}

			// Token: 0x06000C5A RID: 3162 RVA: 0x0001017D File Offset: 0x0000E37D
			[DebuggerStepThrough]
			public static ulong blsi_u64(ulong a)
			{
				return -a & a;
			}

			// Token: 0x06000C5B RID: 3163 RVA: 0x00010183 File Offset: 0x0000E383
			[DebuggerStepThrough]
			public static uint blsmsk_u32(uint a)
			{
				return a - 1U ^ a;
			}

			// Token: 0x06000C5C RID: 3164 RVA: 0x0001018A File Offset: 0x0000E38A
			[DebuggerStepThrough]
			public static ulong blsmsk_u64(ulong a)
			{
				return a - 1UL ^ a;
			}

			// Token: 0x06000C5D RID: 3165 RVA: 0x00010192 File Offset: 0x0000E392
			[DebuggerStepThrough]
			public static uint blsr_u32(uint a)
			{
				return a - 1U & a;
			}

			// Token: 0x06000C5E RID: 3166 RVA: 0x00010199 File Offset: 0x0000E399
			[DebuggerStepThrough]
			public static ulong blsr_u64(ulong a)
			{
				return a - 1UL & a;
			}

			// Token: 0x06000C5F RID: 3167 RVA: 0x000101A4 File Offset: 0x0000E3A4
			[DebuggerStepThrough]
			public static uint tzcnt_u32(uint a)
			{
				uint num = 32U;
				a &= -a;
				if (a != 0U)
				{
					num -= 1U;
				}
				if ((a & 65535U) != 0U)
				{
					num -= 16U;
				}
				if ((a & 16711935U) != 0U)
				{
					num -= 8U;
				}
				if ((a & 252645135U) != 0U)
				{
					num -= 4U;
				}
				if ((a & 858993459U) != 0U)
				{
					num -= 2U;
				}
				if ((a & 1431655765U) != 0U)
				{
					num -= 1U;
				}
				return num;
			}

			// Token: 0x06000C60 RID: 3168 RVA: 0x00010204 File Offset: 0x0000E404
			[DebuggerStepThrough]
			public static ulong tzcnt_u64(ulong a)
			{
				ulong num = 64UL;
				a &= -a;
				if (a != 0UL)
				{
					num -= 1UL;
				}
				if ((a & (ulong)-1) != 0UL)
				{
					num -= 32UL;
				}
				if ((a & 281470681808895UL) != 0UL)
				{
					num -= 16UL;
				}
				if ((a & 71777214294589695UL) != 0UL)
				{
					num -= 8UL;
				}
				if ((a & 1085102592571150095UL) != 0UL)
				{
					num -= 4UL;
				}
				if ((a & 3689348814741910323UL) != 0UL)
				{
					num -= 2UL;
				}
				if ((a & 6148914691236517205UL) != 0UL)
				{
					num -= 1UL;
				}
				return num;
			}
		}

		// Token: 0x02000041 RID: 65
		public static class Bmi2
		{
			// Token: 0x17000044 RID: 68
			// (get) Token: 0x06000C61 RID: 3169 RVA: 0x0001028B File Offset: 0x0000E48B
			public static bool IsBmi2Supported
			{
				get
				{
					return X86.Avx2.IsAvx2Supported;
				}
			}

			// Token: 0x06000C62 RID: 3170 RVA: 0x00010292 File Offset: 0x0000E492
			[DebuggerStepThrough]
			public static uint bzhi_u32(uint a, uint index)
			{
				index &= 255U;
				if (index >= 32U)
				{
					return a;
				}
				return a & (1U << (int)index) - 1U;
			}

			// Token: 0x06000C63 RID: 3171 RVA: 0x000102AE File Offset: 0x0000E4AE
			[DebuggerStepThrough]
			public static ulong bzhi_u64(ulong a, ulong index)
			{
				index &= 255UL;
				if (index >= 64UL)
				{
					return a;
				}
				return a & (1UL << (int)index) - 1UL;
			}

			// Token: 0x06000C64 RID: 3172 RVA: 0x000102D0 File Offset: 0x0000E4D0
			[DebuggerStepThrough]
			public static uint mulx_u32(uint a, uint b, out uint hi)
			{
				ulong num = (ulong)a;
				ulong num2 = (ulong)b;
				ulong num3 = num * num2;
				hi = (uint)(num3 >> 32);
				return (uint)(num3 & (ulong)-1);
			}

			// Token: 0x06000C65 RID: 3173 RVA: 0x000102F1 File Offset: 0x0000E4F1
			[DebuggerStepThrough]
			public static ulong mulx_u64(ulong a, ulong b, out ulong hi)
			{
				return Common.umul128(a, b, out hi);
			}

			// Token: 0x06000C66 RID: 3174 RVA: 0x000102FC File Offset: 0x0000E4FC
			[DebuggerStepThrough]
			public static uint pdep_u32(uint a, uint mask)
			{
				uint num = 0U;
				int num2 = 0;
				for (int i = 0; i < 32; i++)
				{
					if ((mask & 1U << i) != 0U)
					{
						num |= (a >> num2 & 1U) << i;
						num2++;
					}
				}
				return num;
			}

			// Token: 0x06000C67 RID: 3175 RVA: 0x0001033C File Offset: 0x0000E53C
			[DebuggerStepThrough]
			public static ulong pdep_u64(ulong a, ulong mask)
			{
				ulong num = 0UL;
				int num2 = 0;
				for (int i = 0; i < 64; i++)
				{
					if ((mask & 1UL << i) != 0UL)
					{
						num |= (a >> num2 & 1UL) << i;
						num2++;
					}
				}
				return num;
			}

			// Token: 0x06000C68 RID: 3176 RVA: 0x0001037C File Offset: 0x0000E57C
			[DebuggerStepThrough]
			public static uint pext_u32(uint a, uint mask)
			{
				uint num = 0U;
				int num2 = 0;
				for (int i = 0; i < 32; i++)
				{
					if ((mask & 1U << i) != 0U)
					{
						num |= (a >> i & 1U) << num2;
						num2++;
					}
				}
				return num;
			}

			// Token: 0x06000C69 RID: 3177 RVA: 0x000103BC File Offset: 0x0000E5BC
			[DebuggerStepThrough]
			public static ulong pext_u64(ulong a, ulong mask)
			{
				ulong num = 0UL;
				int num2 = 0;
				for (int i = 0; i < 64; i++)
				{
					if ((mask & 1UL << i) != 0UL)
					{
						num |= (a >> i & 1UL) << num2;
						num2++;
					}
				}
				return num;
			}
		}

		// Token: 0x02000042 RID: 66
		[Flags]
		public enum MXCSRBits
		{
			// Token: 0x04000266 RID: 614
			FlushToZero = 32768,
			// Token: 0x04000267 RID: 615
			RoundingControlMask = 24576,
			// Token: 0x04000268 RID: 616
			RoundToNearest = 0,
			// Token: 0x04000269 RID: 617
			RoundDown = 8192,
			// Token: 0x0400026A RID: 618
			RoundUp = 16384,
			// Token: 0x0400026B RID: 619
			RoundTowardZero = 24576,
			// Token: 0x0400026C RID: 620
			PrecisionMask = 4096,
			// Token: 0x0400026D RID: 621
			UnderflowMask = 2048,
			// Token: 0x0400026E RID: 622
			OverflowMask = 1024,
			// Token: 0x0400026F RID: 623
			DivideByZeroMask = 512,
			// Token: 0x04000270 RID: 624
			DenormalOperationMask = 256,
			// Token: 0x04000271 RID: 625
			InvalidOperationMask = 128,
			// Token: 0x04000272 RID: 626
			ExceptionMask = 8064,
			// Token: 0x04000273 RID: 627
			DenormalsAreZeroes = 64,
			// Token: 0x04000274 RID: 628
			PrecisionFlag = 32,
			// Token: 0x04000275 RID: 629
			UnderflowFlag = 16,
			// Token: 0x04000276 RID: 630
			OverflowFlag = 8,
			// Token: 0x04000277 RID: 631
			DivideByZeroFlag = 4,
			// Token: 0x04000278 RID: 632
			DenormalFlag = 2,
			// Token: 0x04000279 RID: 633
			InvalidOperationFlag = 1,
			// Token: 0x0400027A RID: 634
			FlagMask = 63
		}

		// Token: 0x02000043 RID: 67
		[Flags]
		public enum RoundingMode
		{
			// Token: 0x0400027C RID: 636
			FROUND_TO_NEAREST_INT = 0,
			// Token: 0x0400027D RID: 637
			FROUND_TO_NEG_INF = 1,
			// Token: 0x0400027E RID: 638
			FROUND_TO_POS_INF = 2,
			// Token: 0x0400027F RID: 639
			FROUND_TO_ZERO = 3,
			// Token: 0x04000280 RID: 640
			FROUND_CUR_DIRECTION = 4,
			// Token: 0x04000281 RID: 641
			FROUND_RAISE_EXC = 0,
			// Token: 0x04000282 RID: 642
			FROUND_NO_EXC = 8,
			// Token: 0x04000283 RID: 643
			FROUND_NINT = 0,
			// Token: 0x04000284 RID: 644
			FROUND_FLOOR = 1,
			// Token: 0x04000285 RID: 645
			FROUND_CEIL = 2,
			// Token: 0x04000286 RID: 646
			FROUND_TRUNC = 3,
			// Token: 0x04000287 RID: 647
			FROUND_RINT = 4,
			// Token: 0x04000288 RID: 648
			FROUND_NEARBYINT = 12,
			// Token: 0x04000289 RID: 649
			FROUND_NINT_NOEXC = 8,
			// Token: 0x0400028A RID: 650
			FROUND_FLOOR_NOEXC = 9,
			// Token: 0x0400028B RID: 651
			FROUND_CEIL_NOEXC = 10,
			// Token: 0x0400028C RID: 652
			FROUND_TRUNC_NOEXC = 11,
			// Token: 0x0400028D RID: 653
			FROUND_RINT_NOEXC = 12
		}

		// Token: 0x02000044 RID: 68
		internal struct RoundingScope : IDisposable
		{
			// Token: 0x06000C6A RID: 3178 RVA: 0x000103FC File Offset: 0x0000E5FC
			public RoundingScope(X86.MXCSRBits roundingMode)
			{
				this.OldBits = X86.MXCSR;
				X86.MXCSR = ((this.OldBits & ~X86.MXCSRBits.RoundingControlMask) | roundingMode);
			}

			// Token: 0x06000C6B RID: 3179 RVA: 0x0001041C File Offset: 0x0000E61C
			public void Dispose()
			{
				X86.MXCSR = this.OldBits;
			}

			// Token: 0x0400028E RID: 654
			private X86.MXCSRBits OldBits;
		}

		// Token: 0x02000045 RID: 69
		public static class F16C
		{
			// Token: 0x17000045 RID: 69
			// (get) Token: 0x06000C6C RID: 3180 RVA: 0x00010429 File Offset: 0x0000E629
			public static bool IsF16CSupported
			{
				get
				{
					return X86.Avx2.IsAvx2Supported;
				}
			}

			// Token: 0x06000C6D RID: 3181 RVA: 0x00010430 File Offset: 0x0000E630
			[DebuggerStepThrough]
			private static uint HalfToFloat(ushort h)
			{
				bool flag = (h & 32768) > 0;
				long num = (long)(h >> 10) & 31L;
				uint num2 = (uint)(h & 1023);
				uint num3 = flag ? 2147483648U : 0U;
				if (num != 0L || num2 != 0U)
				{
					if (num == 0L)
					{
						num = -1L;
						do
						{
							num += 1L;
							num2 <<= 1;
						}
						while ((num2 & 1024U) == 0U);
						num3 |= (uint)((uint)(112L - num) << 23);
						num3 |= (num2 & 1023U) << 13;
					}
					else
					{
						bool flag2 = num == 31L;
						num3 |= (uint)(flag2 ? 255L : ((uint)(112L + num) << 23));
						num3 |= num2 << 13;
					}
				}
				return num3;
			}

			// Token: 0x06000C6E RID: 3182 RVA: 0x000104C3 File Offset: 0x0000E6C3
			[DebuggerStepThrough]
			public static v128 cvtph_ps(v128 a)
			{
				return new v128(X86.F16C.HalfToFloat(a.UShort0), X86.F16C.HalfToFloat(a.UShort1), X86.F16C.HalfToFloat(a.UShort2), X86.F16C.HalfToFloat(a.UShort3));
			}

			// Token: 0x06000C6F RID: 3183 RVA: 0x000104F8 File Offset: 0x0000E6F8
			[DebuggerStepThrough]
			public static v256 mm256_cvtph_ps(v128 a)
			{
				return new v256(X86.F16C.HalfToFloat(a.UShort0), X86.F16C.HalfToFloat(a.UShort1), X86.F16C.HalfToFloat(a.UShort2), X86.F16C.HalfToFloat(a.UShort3), X86.F16C.HalfToFloat(a.UShort4), X86.F16C.HalfToFloat(a.UShort5), X86.F16C.HalfToFloat(a.UShort6), X86.F16C.HalfToFloat(a.UShort7));
			}

			// Token: 0x06000C70 RID: 3184 RVA: 0x00010564 File Offset: 0x0000E764
			[DebuggerStepThrough]
			private static ushort FloatToHalf(uint f, int rounding)
			{
				uint num = f >> 23;
				sbyte b = X86.F16C.ShiftTable[(int)num];
				uint num2 = (uint)(X86.F16C.BaseTable[(int)num] + (ushort)((f & 8388607U) >> (int)b));
				bool flag = (num2 & 31744U) != 31744U;
				bool flag2 = (num2 & 32768U) > 0U;
				if (rounding == 8)
				{
					uint num3 = (f & 8388607U) >> (int)(b - 1);
					if ((num & 255U) == 102U)
					{
						num2 += 1U;
					}
					if (flag && (num3 & 1U) != 0U)
					{
						num2 += 1U;
					}
				}
				else if (rounding == 11)
				{
					if (!flag)
					{
						num2 -= (uint)(~b & 1);
					}
				}
				else if (rounding == 10)
				{
					if (flag && !flag2)
					{
						if (num <= 102U && num != 0U)
						{
							num2 += 1U;
						}
						else if ((f & 8388607U & (1U << (int)b) - 1U) != 0U)
						{
							num2 += 1U;
						}
					}
					bool flag3 = num2 == 64512U;
					bool flag4 = num != 511U;
					if (flag3 && flag4)
					{
						num2 -= 1U;
					}
				}
				else if (rounding == 9)
				{
					if (flag && flag2)
					{
						if (num <= 358U && num != 256U)
						{
							num2 += 1U;
						}
						else if ((f & 8388607U & (1U << (int)b) - 1U) != 0U)
						{
							num2 += 1U;
						}
					}
					bool flag5 = num2 == 31744U;
					bool flag6 = num != 255U;
					if (flag5 && flag6)
					{
						num2 -= 1U;
					}
				}
				return (ushort)num2;
			}

			// Token: 0x06000C71 RID: 3185 RVA: 0x000106AC File Offset: 0x0000E8AC
			[DebuggerStepThrough]
			public static v128 cvtps_ph(v128 a, int rounding)
			{
				if (rounding == 12)
				{
					X86.MXCSRBits mxcsrbits = X86.MXCSR & X86.MXCSRBits.RoundingControlMask;
					if (mxcsrbits <= X86.MXCSRBits.RoundDown)
					{
						if (mxcsrbits != X86.MXCSRBits.RoundToNearest)
						{
							if (mxcsrbits == X86.MXCSRBits.RoundDown)
							{
								rounding = 9;
							}
						}
						else
						{
							rounding = 8;
						}
					}
					else if (mxcsrbits != X86.MXCSRBits.RoundUp)
					{
						if (mxcsrbits == X86.MXCSRBits.RoundingControlMask)
						{
							rounding = 11;
						}
					}
					else
					{
						rounding = 10;
					}
				}
				return new v128(X86.F16C.FloatToHalf(a.UInt0, rounding), X86.F16C.FloatToHalf(a.UInt1, rounding), X86.F16C.FloatToHalf(a.UInt2, rounding), X86.F16C.FloatToHalf(a.UInt3, rounding), 0, 0, 0, 0);
			}

			// Token: 0x06000C72 RID: 3186 RVA: 0x00010740 File Offset: 0x0000E940
			[DebuggerStepThrough]
			public static v128 mm256_cvtps_ph(v256 a, int rounding)
			{
				if (rounding == 12)
				{
					X86.MXCSRBits mxcsrbits = X86.MXCSR & X86.MXCSRBits.RoundingControlMask;
					if (mxcsrbits <= X86.MXCSRBits.RoundDown)
					{
						if (mxcsrbits != X86.MXCSRBits.RoundToNearest)
						{
							if (mxcsrbits == X86.MXCSRBits.RoundDown)
							{
								rounding = 9;
							}
						}
						else
						{
							rounding = 8;
						}
					}
					else if (mxcsrbits != X86.MXCSRBits.RoundUp)
					{
						if (mxcsrbits == X86.MXCSRBits.RoundingControlMask)
						{
							rounding = 11;
						}
					}
					else
					{
						rounding = 10;
					}
				}
				return new v128(X86.F16C.FloatToHalf(a.UInt0, rounding), X86.F16C.FloatToHalf(a.UInt1, rounding), X86.F16C.FloatToHalf(a.UInt2, rounding), X86.F16C.FloatToHalf(a.UInt3, rounding), X86.F16C.FloatToHalf(a.UInt4, rounding), X86.F16C.FloatToHalf(a.UInt5, rounding), X86.F16C.FloatToHalf(a.UInt6, rounding), X86.F16C.FloatToHalf(a.UInt7, rounding));
			}

			// Token: 0x06000C73 RID: 3187 RVA: 0x000107FF File Offset: 0x0000E9FF
			// Note: this type is marked as 'beforefieldinit'.
			static F16C()
			{
			}

			// Token: 0x0400028F RID: 655
			private static readonly ushort[] BaseTable = new ushort[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				2,
				4,
				8,
				16,
				32,
				64,
				128,
				256,
				512,
				1024,
				2048,
				3072,
				4096,
				5120,
				6144,
				7168,
				8192,
				9216,
				10240,
				11264,
				12288,
				13312,
				14336,
				15360,
				16384,
				17408,
				18432,
				19456,
				20480,
				21504,
				22528,
				23552,
				24576,
				25600,
				26624,
				27648,
				28672,
				29696,
				30720,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				31744,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32768,
				32769,
				32770,
				32772,
				32776,
				32784,
				32800,
				32832,
				32896,
				33024,
				33280,
				33792,
				34816,
				35840,
				36864,
				37888,
				38912,
				39936,
				40960,
				41984,
				43008,
				44032,
				45056,
				46080,
				47104,
				48128,
				49152,
				50176,
				51200,
				52224,
				53248,
				54272,
				55296,
				56320,
				57344,
				58368,
				59392,
				60416,
				61440,
				62464,
				63488,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512,
				64512
			};

			// Token: 0x04000290 RID: 656
			private static readonly sbyte[] ShiftTable = new sbyte[]
			{
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				23,
				22,
				21,
				20,
				19,
				18,
				17,
				16,
				15,
				14,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				13,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				23,
				22,
				21,
				20,
				19,
				18,
				17,
				16,
				15,
				14,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				13,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				13
			};
		}

		// Token: 0x02000046 RID: 70
		public static class Fma
		{
			// Token: 0x17000046 RID: 70
			// (get) Token: 0x06000C74 RID: 3188 RVA: 0x00010835 File Offset: 0x0000EA35
			public static bool IsFmaSupported
			{
				get
				{
					return X86.Avx2.IsAvx2Supported;
				}
			}

			// Token: 0x06000C75 RID: 3189 RVA: 0x0001083C File Offset: 0x0000EA3C
			[DebuggerStepThrough]
			private static float FmaHelper(float a, float b, float c)
			{
				return (float)((double)a * (double)b + (double)c);
			}

			// Token: 0x06000C76 RID: 3190 RVA: 0x00010847 File Offset: 0x0000EA47
			[DebuggerStepThrough]
			private static float FnmaHelper(float a, float b, float c)
			{
				return X86.Fma.FmaHelper(-a, b, c);
			}

			// Token: 0x06000C77 RID: 3191 RVA: 0x00010852 File Offset: 0x0000EA52
			[DebuggerStepThrough]
			public static v128 fmadd_pd(v128 a, v128 b, v128 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C78 RID: 3192 RVA: 0x0001085E File Offset: 0x0000EA5E
			[DebuggerStepThrough]
			public static v256 mm256_fmadd_pd(v256 a, v256 b, v256 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C79 RID: 3193 RVA: 0x0001086C File Offset: 0x0000EA6C
			[DebuggerStepThrough]
			public static v128 fmadd_ps(v128 a, v128 b, v128 c)
			{
				return new v128(X86.Fma.FmaHelper(a.Float0, b.Float0, c.Float0), X86.Fma.FmaHelper(a.Float1, b.Float1, c.Float1), X86.Fma.FmaHelper(a.Float2, b.Float2, c.Float2), X86.Fma.FmaHelper(a.Float3, b.Float3, c.Float3));
			}

			// Token: 0x06000C7A RID: 3194 RVA: 0x000108DC File Offset: 0x0000EADC
			[DebuggerStepThrough]
			public static v256 mm256_fmadd_ps(v256 a, v256 b, v256 c)
			{
				return new v256(X86.Fma.FmaHelper(a.Float0, b.Float0, c.Float0), X86.Fma.FmaHelper(a.Float1, b.Float1, c.Float1), X86.Fma.FmaHelper(a.Float2, b.Float2, c.Float2), X86.Fma.FmaHelper(a.Float3, b.Float3, c.Float3), X86.Fma.FmaHelper(a.Float4, b.Float4, c.Float4), X86.Fma.FmaHelper(a.Float5, b.Float5, c.Float5), X86.Fma.FmaHelper(a.Float6, b.Float6, c.Float6), X86.Fma.FmaHelper(a.Float7, b.Float7, c.Float7));
			}

			// Token: 0x06000C7B RID: 3195 RVA: 0x000109A6 File Offset: 0x0000EBA6
			[DebuggerStepThrough]
			public static v128 fmadd_sd(v128 a, v128 b, v128 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C7C RID: 3196 RVA: 0x000109B4 File Offset: 0x0000EBB4
			[DebuggerStepThrough]
			public static v128 fmadd_ss(v128 a, v128 b, v128 c)
			{
				v128 result = a;
				result.Float0 = X86.Fma.FmaHelper(a.Float0, b.Float0, c.Float0);
				return result;
			}

			// Token: 0x06000C7D RID: 3197 RVA: 0x000109E2 File Offset: 0x0000EBE2
			[DebuggerStepThrough]
			public static v128 fmaddsub_pd(v128 a, v128 b, v128 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C7E RID: 3198 RVA: 0x000109EE File Offset: 0x0000EBEE
			[DebuggerStepThrough]
			public static v256 mm256_fmaddsub_pd(v256 a, v256 b, v256 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C7F RID: 3199 RVA: 0x000109FC File Offset: 0x0000EBFC
			[DebuggerStepThrough]
			public static v128 fmaddsub_ps(v128 a, v128 b, v128 c)
			{
				return new v128(X86.Fma.FmaHelper(a.Float0, b.Float0, -c.Float0), X86.Fma.FmaHelper(a.Float1, b.Float1, c.Float1), X86.Fma.FmaHelper(a.Float2, b.Float2, -c.Float2), X86.Fma.FmaHelper(a.Float3, b.Float3, c.Float3));
			}

			// Token: 0x06000C80 RID: 3200 RVA: 0x00010A6C File Offset: 0x0000EC6C
			[DebuggerStepThrough]
			public static v256 mm256_fmaddsub_ps(v256 a, v256 b, v256 c)
			{
				return new v256(X86.Fma.FmaHelper(a.Float0, b.Float0, -c.Float0), X86.Fma.FmaHelper(a.Float1, b.Float1, c.Float1), X86.Fma.FmaHelper(a.Float2, b.Float2, -c.Float2), X86.Fma.FmaHelper(a.Float3, b.Float3, c.Float3), X86.Fma.FmaHelper(a.Float4, b.Float4, -c.Float4), X86.Fma.FmaHelper(a.Float5, b.Float5, c.Float5), X86.Fma.FmaHelper(a.Float6, b.Float6, -c.Float6), X86.Fma.FmaHelper(a.Float7, b.Float7, c.Float7));
			}

			// Token: 0x06000C81 RID: 3201 RVA: 0x00010B3A File Offset: 0x0000ED3A
			[DebuggerStepThrough]
			public static v128 fmsub_pd(v128 a, v128 b, v128 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C82 RID: 3202 RVA: 0x00010B46 File Offset: 0x0000ED46
			[DebuggerStepThrough]
			public static v256 mm256_fmsub_pd(v256 a, v256 b, v256 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C83 RID: 3203 RVA: 0x00010B54 File Offset: 0x0000ED54
			[DebuggerStepThrough]
			public static v128 fmsub_ps(v128 a, v128 b, v128 c)
			{
				return new v128(X86.Fma.FmaHelper(a.Float0, b.Float0, -c.Float0), X86.Fma.FmaHelper(a.Float1, b.Float1, -c.Float1), X86.Fma.FmaHelper(a.Float2, b.Float2, -c.Float2), X86.Fma.FmaHelper(a.Float3, b.Float3, -c.Float3));
			}

			// Token: 0x06000C84 RID: 3204 RVA: 0x00010BC8 File Offset: 0x0000EDC8
			[DebuggerStepThrough]
			public static v256 mm256_fmsub_ps(v256 a, v256 b, v256 c)
			{
				return new v256(X86.Fma.FmaHelper(a.Float0, b.Float0, -c.Float0), X86.Fma.FmaHelper(a.Float1, b.Float1, -c.Float1), X86.Fma.FmaHelper(a.Float2, b.Float2, -c.Float2), X86.Fma.FmaHelper(a.Float3, b.Float3, -c.Float3), X86.Fma.FmaHelper(a.Float4, b.Float4, -c.Float4), X86.Fma.FmaHelper(a.Float5, b.Float5, -c.Float5), X86.Fma.FmaHelper(a.Float6, b.Float6, -c.Float6), X86.Fma.FmaHelper(a.Float7, b.Float7, -c.Float7));
			}

			// Token: 0x06000C85 RID: 3205 RVA: 0x00010C9A File Offset: 0x0000EE9A
			[DebuggerStepThrough]
			public static v128 fmsub_sd(v128 a, v128 b, v128 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C86 RID: 3206 RVA: 0x00010CA8 File Offset: 0x0000EEA8
			[DebuggerStepThrough]
			public static v128 fmsub_ss(v128 a, v128 b, v128 c)
			{
				v128 result = a;
				result.Float0 = X86.Fma.FmaHelper(a.Float0, b.Float0, -c.Float0);
				return result;
			}

			// Token: 0x06000C87 RID: 3207 RVA: 0x00010CD7 File Offset: 0x0000EED7
			[DebuggerStepThrough]
			public static v128 fmsubadd_pd(v128 a, v128 b, v128 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C88 RID: 3208 RVA: 0x00010CE3 File Offset: 0x0000EEE3
			[DebuggerStepThrough]
			public static v256 mm256_fmsubadd_pd(v256 a, v256 b, v256 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C89 RID: 3209 RVA: 0x00010CF0 File Offset: 0x0000EEF0
			[DebuggerStepThrough]
			public static v128 fmsubadd_ps(v128 a, v128 b, v128 c)
			{
				return new v128(X86.Fma.FmaHelper(a.Float0, b.Float0, c.Float0), X86.Fma.FmaHelper(a.Float1, b.Float1, -c.Float1), X86.Fma.FmaHelper(a.Float2, b.Float2, c.Float2), X86.Fma.FmaHelper(a.Float3, b.Float3, -c.Float3));
			}

			// Token: 0x06000C8A RID: 3210 RVA: 0x00010D60 File Offset: 0x0000EF60
			[DebuggerStepThrough]
			public static v256 mm256_fmsubadd_ps(v256 a, v256 b, v256 c)
			{
				return new v256(X86.Fma.FmaHelper(a.Float0, b.Float0, c.Float0), X86.Fma.FmaHelper(a.Float1, b.Float1, -c.Float1), X86.Fma.FmaHelper(a.Float2, b.Float2, c.Float2), X86.Fma.FmaHelper(a.Float3, b.Float3, -c.Float3), X86.Fma.FmaHelper(a.Float4, b.Float4, c.Float4), X86.Fma.FmaHelper(a.Float5, b.Float5, -c.Float5), X86.Fma.FmaHelper(a.Float6, b.Float6, c.Float6), X86.Fma.FmaHelper(a.Float7, b.Float7, -c.Float7));
			}

			// Token: 0x06000C8B RID: 3211 RVA: 0x00010E2E File Offset: 0x0000F02E
			[DebuggerStepThrough]
			public static v128 fnmadd_pd(v128 a, v128 b, v128 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C8C RID: 3212 RVA: 0x00010E3A File Offset: 0x0000F03A
			[DebuggerStepThrough]
			public static v256 mm256_fnmadd_pd(v256 a, v256 b, v256 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C8D RID: 3213 RVA: 0x00010E48 File Offset: 0x0000F048
			[DebuggerStepThrough]
			public static v128 fnmadd_ps(v128 a, v128 b, v128 c)
			{
				return new v128(X86.Fma.FnmaHelper(a.Float0, b.Float0, c.Float0), X86.Fma.FnmaHelper(a.Float1, b.Float1, c.Float1), X86.Fma.FnmaHelper(a.Float2, b.Float2, c.Float2), X86.Fma.FnmaHelper(a.Float3, b.Float3, c.Float3));
			}

			// Token: 0x06000C8E RID: 3214 RVA: 0x00010EB8 File Offset: 0x0000F0B8
			[DebuggerStepThrough]
			public static v256 mm256_fnmadd_ps(v256 a, v256 b, v256 c)
			{
				return new v256(X86.Fma.FnmaHelper(a.Float0, b.Float0, c.Float0), X86.Fma.FnmaHelper(a.Float1, b.Float1, c.Float1), X86.Fma.FnmaHelper(a.Float2, b.Float2, c.Float2), X86.Fma.FnmaHelper(a.Float3, b.Float3, c.Float3), X86.Fma.FnmaHelper(a.Float4, b.Float4, c.Float4), X86.Fma.FnmaHelper(a.Float5, b.Float5, c.Float5), X86.Fma.FnmaHelper(a.Float6, b.Float6, c.Float6), X86.Fma.FnmaHelper(a.Float7, b.Float7, c.Float7));
			}

			// Token: 0x06000C8F RID: 3215 RVA: 0x00010F82 File Offset: 0x0000F182
			[DebuggerStepThrough]
			public static v128 fnmadd_sd(v128 a, v128 b, v128 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C90 RID: 3216 RVA: 0x00010F90 File Offset: 0x0000F190
			[DebuggerStepThrough]
			public static v128 fnmadd_ss(v128 a, v128 b, v128 c)
			{
				v128 result = a;
				result.Float0 = X86.Fma.FnmaHelper(a.Float0, b.Float0, c.Float0);
				return result;
			}

			// Token: 0x06000C91 RID: 3217 RVA: 0x00010FBE File Offset: 0x0000F1BE
			[DebuggerStepThrough]
			public static v128 fnmsub_pd(v128 a, v128 b, v128 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C92 RID: 3218 RVA: 0x00010FCA File Offset: 0x0000F1CA
			[DebuggerStepThrough]
			public static v256 mm256_fnmsub_pd(v256 a, v256 b, v256 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C93 RID: 3219 RVA: 0x00010FD8 File Offset: 0x0000F1D8
			[DebuggerStepThrough]
			public static v128 fnmsub_ps(v128 a, v128 b, v128 c)
			{
				return new v128(X86.Fma.FnmaHelper(a.Float0, b.Float0, -c.Float0), X86.Fma.FnmaHelper(a.Float1, b.Float1, -c.Float1), X86.Fma.FnmaHelper(a.Float2, b.Float2, -c.Float2), X86.Fma.FnmaHelper(a.Float3, b.Float3, -c.Float3));
			}

			// Token: 0x06000C94 RID: 3220 RVA: 0x0001104C File Offset: 0x0000F24C
			[DebuggerStepThrough]
			public static v256 mm256_fnmsub_ps(v256 a, v256 b, v256 c)
			{
				return new v256(X86.Fma.FnmaHelper(a.Float0, b.Float0, -c.Float0), X86.Fma.FnmaHelper(a.Float1, b.Float1, -c.Float1), X86.Fma.FnmaHelper(a.Float2, b.Float2, -c.Float2), X86.Fma.FnmaHelper(a.Float3, b.Float3, -c.Float3), X86.Fma.FnmaHelper(a.Float4, b.Float4, -c.Float4), X86.Fma.FnmaHelper(a.Float5, b.Float5, -c.Float5), X86.Fma.FnmaHelper(a.Float6, b.Float6, -c.Float6), X86.Fma.FnmaHelper(a.Float7, b.Float7, -c.Float7));
			}

			// Token: 0x06000C95 RID: 3221 RVA: 0x0001111E File Offset: 0x0000F31E
			[DebuggerStepThrough]
			public static v128 fnmsub_sd(v128 a, v128 b, v128 c)
			{
				throw new Exception("Double-precision FMA not emulated in C#");
			}

			// Token: 0x06000C96 RID: 3222 RVA: 0x0001112C File Offset: 0x0000F32C
			[DebuggerStepThrough]
			public static v128 fnmsub_ss(v128 a, v128 b, v128 c)
			{
				v128 result = a;
				result.Float0 = X86.Fma.FnmaHelper(a.Float0, b.Float0, -c.Float0);
				return result;
			}

			// Token: 0x02000056 RID: 86
			[StructLayout(LayoutKind.Explicit)]
			private struct Union
			{
				// Token: 0x040002B4 RID: 692
				[FieldOffset(0)]
				public float f;

				// Token: 0x040002B5 RID: 693
				[FieldOffset(0)]
				public uint u;
			}
		}

		// Token: 0x02000047 RID: 71
		public static class Popcnt
		{
			// Token: 0x17000047 RID: 71
			// (get) Token: 0x06000C97 RID: 3223 RVA: 0x0001115B File Offset: 0x0000F35B
			public static bool IsPopcntSupported
			{
				get
				{
					return X86.Sse4_2.IsSse42Supported;
				}
			}

			// Token: 0x06000C98 RID: 3224 RVA: 0x00011164 File Offset: 0x0000F364
			[DebuggerStepThrough]
			public static int popcnt_u32(uint v)
			{
				int num = 0;
				for (uint num2 = 2147483648U; num2 != 0U; num2 >>= 1)
				{
					num += (((v & num2) != 0U) ? 1 : 0);
				}
				return num;
			}

			// Token: 0x06000C99 RID: 3225 RVA: 0x00011190 File Offset: 0x0000F390
			[DebuggerStepThrough]
			public static int popcnt_u64(ulong v)
			{
				int num = 0;
				for (ulong num2 = 9223372036854775808UL; num2 != 0UL; num2 >>= 1)
				{
					num += (((v & num2) != 0UL) ? 1 : 0);
				}
				return num;
			}
		}

		// Token: 0x02000048 RID: 72
		public static class Sse
		{
			// Token: 0x17000048 RID: 72
			// (get) Token: 0x06000C9A RID: 3226 RVA: 0x000111BF File Offset: 0x0000F3BF
			public static bool IsSseSupported
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000C9B RID: 3227 RVA: 0x000111C2 File Offset: 0x0000F3C2
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public unsafe static v128 load_ps(void* ptr)
			{
				return X86.GenericCSharpLoad(ptr);
			}

			// Token: 0x06000C9C RID: 3228 RVA: 0x000111CA File Offset: 0x0000F3CA
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public unsafe static v128 loadu_ps(void* ptr)
			{
				return X86.GenericCSharpLoad(ptr);
			}

			// Token: 0x06000C9D RID: 3229 RVA: 0x000111D2 File Offset: 0x0000F3D2
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public unsafe static void store_ps(void* ptr, v128 val)
			{
				X86.GenericCSharpStore(ptr, val);
			}

			// Token: 0x06000C9E RID: 3230 RVA: 0x000111DB File Offset: 0x0000F3DB
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public unsafe static void storeu_ps(void* ptr, v128 val)
			{
				X86.GenericCSharpStore(ptr, val);
			}

			// Token: 0x06000C9F RID: 3231 RVA: 0x000111E4 File Offset: 0x0000F3E4
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public unsafe static void stream_ps(void* mem_addr, v128 a)
			{
				X86.GenericCSharpStore(mem_addr, a);
			}

			// Token: 0x06000CA0 RID: 3232 RVA: 0x000111F0 File Offset: 0x0000F3F0
			[DebuggerStepThrough]
			public static v128 cvtsi32_ss(v128 a, int b)
			{
				v128 result = a;
				result.Float0 = (float)b;
				return result;
			}

			// Token: 0x06000CA1 RID: 3233 RVA: 0x0001120C File Offset: 0x0000F40C
			[DebuggerStepThrough]
			public static v128 cvtsi64_ss(v128 a, long b)
			{
				v128 result = a;
				result.Float0 = (float)b;
				return result;
			}

			// Token: 0x06000CA2 RID: 3234 RVA: 0x00011228 File Offset: 0x0000F428
			[DebuggerStepThrough]
			public static v128 add_ss(v128 a, v128 b)
			{
				v128 v = a;
				v.Float0 += b.Float0;
				return v;
			}

			// Token: 0x06000CA3 RID: 3235 RVA: 0x0001124C File Offset: 0x0000F44C
			[DebuggerStepThrough]
			public static v128 add_ps(v128 a, v128 b)
			{
				v128 result = a;
				result.Float0 += b.Float0;
				result.Float1 += b.Float1;
				result.Float2 += b.Float2;
				result.Float3 += b.Float3;
				return result;
			}

			// Token: 0x06000CA4 RID: 3236 RVA: 0x000112A0 File Offset: 0x0000F4A0
			[DebuggerStepThrough]
			public static v128 sub_ss(v128 a, v128 b)
			{
				v128 result = a;
				result.Float0 = a.Float0 - b.Float0;
				return result;
			}

			// Token: 0x06000CA5 RID: 3237 RVA: 0x000112C4 File Offset: 0x0000F4C4
			[DebuggerStepThrough]
			public static v128 sub_ps(v128 a, v128 b)
			{
				v128 result = a;
				result.Float0 -= b.Float0;
				result.Float1 -= b.Float1;
				result.Float2 -= b.Float2;
				result.Float3 -= b.Float3;
				return result;
			}

			// Token: 0x06000CA6 RID: 3238 RVA: 0x00011318 File Offset: 0x0000F518
			[DebuggerStepThrough]
			public static v128 mul_ss(v128 a, v128 b)
			{
				v128 result = a;
				result.Float0 = a.Float0 * b.Float0;
				return result;
			}

			// Token: 0x06000CA7 RID: 3239 RVA: 0x0001133C File Offset: 0x0000F53C
			[DebuggerStepThrough]
			public static v128 mul_ps(v128 a, v128 b)
			{
				v128 result = a;
				result.Float0 *= b.Float0;
				result.Float1 *= b.Float1;
				result.Float2 *= b.Float2;
				result.Float3 *= b.Float3;
				return result;
			}

			// Token: 0x06000CA8 RID: 3240 RVA: 0x00011390 File Offset: 0x0000F590
			[DebuggerStepThrough]
			public static v128 div_ss(v128 a, v128 b)
			{
				v128 result = a;
				result.Float0 = a.Float0 / b.Float0;
				return result;
			}

			// Token: 0x06000CA9 RID: 3241 RVA: 0x000113B4 File Offset: 0x0000F5B4
			[DebuggerStepThrough]
			public static v128 div_ps(v128 a, v128 b)
			{
				v128 result = a;
				result.Float0 /= b.Float0;
				result.Float1 /= b.Float1;
				result.Float2 /= b.Float2;
				result.Float3 /= b.Float3;
				return result;
			}

			// Token: 0x06000CAA RID: 3242 RVA: 0x00011408 File Offset: 0x0000F608
			[DebuggerStepThrough]
			public static v128 sqrt_ss(v128 a)
			{
				v128 result = a;
				result.Float0 = (float)Math.Sqrt((double)a.Float0);
				return result;
			}

			// Token: 0x06000CAB RID: 3243 RVA: 0x0001142C File Offset: 0x0000F62C
			[DebuggerStepThrough]
			public static v128 sqrt_ps(v128 a)
			{
				return new v128
				{
					Float0 = (float)Math.Sqrt((double)a.Float0),
					Float1 = (float)Math.Sqrt((double)a.Float1),
					Float2 = (float)Math.Sqrt((double)a.Float2),
					Float3 = (float)Math.Sqrt((double)a.Float3)
				};
			}

			// Token: 0x06000CAC RID: 3244 RVA: 0x00011494 File Offset: 0x0000F694
			[DebuggerStepThrough]
			public static v128 rcp_ss(v128 a)
			{
				v128 result = a;
				result.Float0 = 1f / a.Float0;
				return result;
			}

			// Token: 0x06000CAD RID: 3245 RVA: 0x000114B8 File Offset: 0x0000F6B8
			[DebuggerStepThrough]
			public static v128 rcp_ps(v128 a)
			{
				return new v128
				{
					Float0 = 1f / a.Float0,
					Float1 = 1f / a.Float1,
					Float2 = 1f / a.Float2,
					Float3 = 1f / a.Float3
				};
			}

			// Token: 0x06000CAE RID: 3246 RVA: 0x0001151C File Offset: 0x0000F71C
			[DebuggerStepThrough]
			public static v128 rsqrt_ss(v128 a)
			{
				v128 result = a;
				result.Float0 = 1f / (float)Math.Sqrt((double)a.Float0);
				return result;
			}

			// Token: 0x06000CAF RID: 3247 RVA: 0x00011548 File Offset: 0x0000F748
			[DebuggerStepThrough]
			public static v128 rsqrt_ps(v128 a)
			{
				return new v128
				{
					Float0 = 1f / (float)Math.Sqrt((double)a.Float0),
					Float1 = 1f / (float)Math.Sqrt((double)a.Float1),
					Float2 = 1f / (float)Math.Sqrt((double)a.Float2),
					Float3 = 1f / (float)Math.Sqrt((double)a.Float3)
				};
			}

			// Token: 0x06000CB0 RID: 3248 RVA: 0x000115C8 File Offset: 0x0000F7C8
			[DebuggerStepThrough]
			public static v128 min_ss(v128 a, v128 b)
			{
				v128 result = a;
				result.Float0 = Math.Min(a.Float0, b.Float0);
				return result;
			}

			// Token: 0x06000CB1 RID: 3249 RVA: 0x000115F0 File Offset: 0x0000F7F0
			[DebuggerStepThrough]
			public static v128 min_ps(v128 a, v128 b)
			{
				return new v128
				{
					Float0 = Math.Min(a.Float0, b.Float0),
					Float1 = Math.Min(a.Float1, b.Float1),
					Float2 = Math.Min(a.Float2, b.Float2),
					Float3 = Math.Min(a.Float3, b.Float3)
				};
			}

			// Token: 0x06000CB2 RID: 3250 RVA: 0x00011668 File Offset: 0x0000F868
			[DebuggerStepThrough]
			public static v128 max_ss(v128 a, v128 b)
			{
				v128 result = a;
				result.Float0 = Math.Max(a.Float0, b.Float0);
				return result;
			}

			// Token: 0x06000CB3 RID: 3251 RVA: 0x00011690 File Offset: 0x0000F890
			[DebuggerStepThrough]
			public static v128 max_ps(v128 a, v128 b)
			{
				return new v128
				{
					Float0 = Math.Max(a.Float0, b.Float0),
					Float1 = Math.Max(a.Float1, b.Float1),
					Float2 = Math.Max(a.Float2, b.Float2),
					Float3 = Math.Max(a.Float3, b.Float3)
				};
			}

			// Token: 0x06000CB4 RID: 3252 RVA: 0x00011708 File Offset: 0x0000F908
			[DebuggerStepThrough]
			public static v128 and_ps(v128 a, v128 b)
			{
				v128 result = a;
				result.UInt0 &= b.UInt0;
				result.UInt1 &= b.UInt1;
				result.UInt2 &= b.UInt2;
				result.UInt3 &= b.UInt3;
				return result;
			}

			// Token: 0x06000CB5 RID: 3253 RVA: 0x0001175C File Offset: 0x0000F95C
			[DebuggerStepThrough]
			public static v128 andnot_ps(v128 a, v128 b)
			{
				return new v128
				{
					UInt0 = (~a.UInt0 & b.UInt0),
					UInt1 = (~a.UInt1 & b.UInt1),
					UInt2 = (~a.UInt2 & b.UInt2),
					UInt3 = (~a.UInt3 & b.UInt3)
				};
			}

			// Token: 0x06000CB6 RID: 3254 RVA: 0x000117C8 File Offset: 0x0000F9C8
			[DebuggerStepThrough]
			public static v128 or_ps(v128 a, v128 b)
			{
				return new v128
				{
					UInt0 = (a.UInt0 | b.UInt0),
					UInt1 = (a.UInt1 | b.UInt1),
					UInt2 = (a.UInt2 | b.UInt2),
					UInt3 = (a.UInt3 | b.UInt3)
				};
			}

			// Token: 0x06000CB7 RID: 3255 RVA: 0x00011830 File Offset: 0x0000FA30
			[DebuggerStepThrough]
			public static v128 xor_ps(v128 a, v128 b)
			{
				return new v128
				{
					UInt0 = (a.UInt0 ^ b.UInt0),
					UInt1 = (a.UInt1 ^ b.UInt1),
					UInt2 = (a.UInt2 ^ b.UInt2),
					UInt3 = (a.UInt3 ^ b.UInt3)
				};
			}

			// Token: 0x06000CB8 RID: 3256 RVA: 0x00011898 File Offset: 0x0000FA98
			[DebuggerStepThrough]
			public static v128 cmpeq_ss(v128 a, v128 b)
			{
				v128 result = a;
				result.UInt0 = ((a.Float0 == b.Float0) ? uint.MaxValue : 0U);
				return result;
			}

			// Token: 0x06000CB9 RID: 3257 RVA: 0x000118C4 File Offset: 0x0000FAC4
			[DebuggerStepThrough]
			public static v128 cmpeq_ps(v128 a, v128 b)
			{
				return new v128
				{
					UInt0 = ((a.Float0 == b.Float0) ? uint.MaxValue : 0U),
					UInt1 = ((a.Float1 == b.Float1) ? uint.MaxValue : 0U),
					UInt2 = ((a.Float2 == b.Float2) ? uint.MaxValue : 0U),
					UInt3 = ((a.Float3 == b.Float3) ? uint.MaxValue : 0U)
				};
			}

			// Token: 0x06000CBA RID: 3258 RVA: 0x00011940 File Offset: 0x0000FB40
			[DebuggerStepThrough]
			public static v128 cmplt_ss(v128 a, v128 b)
			{
				v128 result = a;
				result.UInt0 = ((a.Float0 < b.Float0) ? uint.MaxValue : 0U);
				return result;
			}

			// Token: 0x06000CBB RID: 3259 RVA: 0x0001196C File Offset: 0x0000FB6C
			[DebuggerStepThrough]
			public static v128 cmplt_ps(v128 a, v128 b)
			{
				return new v128
				{
					UInt0 = ((a.Float0 < b.Float0) ? uint.MaxValue : 0U),
					UInt1 = ((a.Float1 < b.Float1) ? uint.MaxValue : 0U),
					UInt2 = ((a.Float2 < b.Float2) ? uint.MaxValue : 0U),
					UInt3 = ((a.Float3 < b.Float3) ? uint.MaxValue : 0U)
				};
			}

			// Token: 0x06000CBC RID: 3260 RVA: 0x000119E8 File Offset: 0x0000FBE8
			[DebuggerStepThrough]
			public static v128 cmple_ss(v128 a, v128 b)
			{
				v128 result = a;
				result.UInt0 = ((a.Float0 <= b.Float0) ? uint.MaxValue : 0U);
				return result;
			}

			// Token: 0x06000CBD RID: 3261 RVA: 0x00011A14 File Offset: 0x0000FC14
			[DebuggerStepThrough]
			public static v128 cmple_ps(v128 a, v128 b)
			{
				return new v128
				{
					UInt0 = ((a.Float0 <= b.Float0) ? uint.MaxValue : 0U),
					UInt1 = ((a.Float1 <= b.Float1) ? uint.MaxValue : 0U),
					UInt2 = ((a.Float2 <= b.Float2) ? uint.MaxValue : 0U),
					UInt3 = ((a.Float3 <= b.Float3) ? uint.MaxValue : 0U)
				};
			}

			// Token: 0x06000CBE RID: 3262 RVA: 0x00011A8E File Offset: 0x0000FC8E
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmpgt_ss(v128 a, v128 b)
			{
				return X86.Sse.cmplt_ss(b, a);
			}

			// Token: 0x06000CBF RID: 3263 RVA: 0x00011A97 File Offset: 0x0000FC97
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmpgt_ps(v128 a, v128 b)
			{
				return X86.Sse.cmplt_ps(b, a);
			}

			// Token: 0x06000CC0 RID: 3264 RVA: 0x00011AA0 File Offset: 0x0000FCA0
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmpge_ss(v128 a, v128 b)
			{
				return X86.Sse.cmple_ss(b, a);
			}

			// Token: 0x06000CC1 RID: 3265 RVA: 0x00011AA9 File Offset: 0x0000FCA9
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmpge_ps(v128 a, v128 b)
			{
				return X86.Sse.cmple_ps(b, a);
			}

			// Token: 0x06000CC2 RID: 3266 RVA: 0x00011AB4 File Offset: 0x0000FCB4
			[DebuggerStepThrough]
			public static v128 cmpneq_ss(v128 a, v128 b)
			{
				v128 result = a;
				result.UInt0 = ((a.Float0 != b.Float0) ? uint.MaxValue : 0U);
				return result;
			}

			// Token: 0x06000CC3 RID: 3267 RVA: 0x00011AE0 File Offset: 0x0000FCE0
			[DebuggerStepThrough]
			public static v128 cmpneq_ps(v128 a, v128 b)
			{
				return new v128
				{
					UInt0 = ((a.Float0 != b.Float0) ? uint.MaxValue : 0U),
					UInt1 = ((a.Float1 != b.Float1) ? uint.MaxValue : 0U),
					UInt2 = ((a.Float2 != b.Float2) ? uint.MaxValue : 0U),
					UInt3 = ((a.Float3 != b.Float3) ? uint.MaxValue : 0U)
				};
			}

			// Token: 0x06000CC4 RID: 3268 RVA: 0x00011B5C File Offset: 0x0000FD5C
			[DebuggerStepThrough]
			public static v128 cmpnlt_ss(v128 a, v128 b)
			{
				v128 result = a;
				result.UInt0 = ((a.Float0 >= b.Float0) ? uint.MaxValue : 0U);
				return result;
			}

			// Token: 0x06000CC5 RID: 3269 RVA: 0x00011B88 File Offset: 0x0000FD88
			[DebuggerStepThrough]
			public static v128 cmpnlt_ps(v128 a, v128 b)
			{
				return new v128
				{
					UInt0 = ((a.Float0 >= b.Float0) ? uint.MaxValue : 0U),
					UInt1 = ((a.Float1 >= b.Float1) ? uint.MaxValue : 0U),
					UInt2 = ((a.Float2 >= b.Float2) ? uint.MaxValue : 0U),
					UInt3 = ((a.Float3 >= b.Float3) ? uint.MaxValue : 0U)
				};
			}

			// Token: 0x06000CC6 RID: 3270 RVA: 0x00011C04 File Offset: 0x0000FE04
			[DebuggerStepThrough]
			public static v128 cmpnle_ss(v128 a, v128 b)
			{
				v128 result = a;
				result.UInt0 = ((a.Float0 > b.Float0) ? uint.MaxValue : 0U);
				return result;
			}

			// Token: 0x06000CC7 RID: 3271 RVA: 0x00011C30 File Offset: 0x0000FE30
			[DebuggerStepThrough]
			public static v128 cmpnle_ps(v128 a, v128 b)
			{
				return new v128
				{
					UInt0 = ((a.Float0 > b.Float0) ? uint.MaxValue : 0U),
					UInt1 = ((a.Float1 > b.Float1) ? uint.MaxValue : 0U),
					UInt2 = ((a.Float2 > b.Float2) ? uint.MaxValue : 0U),
					UInt3 = ((a.Float3 > b.Float3) ? uint.MaxValue : 0U)
				};
			}

			// Token: 0x06000CC8 RID: 3272 RVA: 0x00011CAA File Offset: 0x0000FEAA
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmpngt_ss(v128 a, v128 b)
			{
				return X86.Sse.cmpnlt_ss(b, a);
			}

			// Token: 0x06000CC9 RID: 3273 RVA: 0x00011CB3 File Offset: 0x0000FEB3
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmpngt_ps(v128 a, v128 b)
			{
				return X86.Sse.cmpnlt_ps(b, a);
			}

			// Token: 0x06000CCA RID: 3274 RVA: 0x00011CBC File Offset: 0x0000FEBC
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmpnge_ss(v128 a, v128 b)
			{
				return X86.Sse.cmpnle_ss(b, a);
			}

			// Token: 0x06000CCB RID: 3275 RVA: 0x00011CC5 File Offset: 0x0000FEC5
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmpnge_ps(v128 a, v128 b)
			{
				return X86.Sse.cmpnle_ps(b, a);
			}

			// Token: 0x06000CCC RID: 3276 RVA: 0x00011CD0 File Offset: 0x0000FED0
			[DebuggerStepThrough]
			public static v128 cmpord_ss(v128 a, v128 b)
			{
				v128 result = a;
				result.UInt0 = ((X86.IsNaN(a.UInt0) || X86.IsNaN(b.UInt0)) ? 0U : uint.MaxValue);
				return result;
			}

			// Token: 0x06000CCD RID: 3277 RVA: 0x00011D08 File Offset: 0x0000FF08
			[DebuggerStepThrough]
			public static v128 cmpord_ps(v128 a, v128 b)
			{
				return new v128
				{
					UInt0 = ((X86.IsNaN(a.UInt0) || X86.IsNaN(b.UInt0)) ? 0U : uint.MaxValue),
					UInt1 = ((X86.IsNaN(a.UInt1) || X86.IsNaN(b.UInt1)) ? 0U : uint.MaxValue),
					UInt2 = ((X86.IsNaN(a.UInt2) || X86.IsNaN(b.UInt2)) ? 0U : uint.MaxValue),
					UInt3 = ((X86.IsNaN(a.UInt3) || X86.IsNaN(b.UInt3)) ? 0U : uint.MaxValue)
				};
			}

			// Token: 0x06000CCE RID: 3278 RVA: 0x00011DB4 File Offset: 0x0000FFB4
			[DebuggerStepThrough]
			public static v128 cmpunord_ss(v128 a, v128 b)
			{
				v128 result = a;
				result.UInt0 = ((X86.IsNaN(a.UInt0) || X86.IsNaN(b.UInt0)) ? uint.MaxValue : 0U);
				return result;
			}

			// Token: 0x06000CCF RID: 3279 RVA: 0x00011DEC File Offset: 0x0000FFEC
			[DebuggerStepThrough]
			public static v128 cmpunord_ps(v128 a, v128 b)
			{
				return new v128
				{
					UInt0 = ((X86.IsNaN(a.UInt0) || X86.IsNaN(b.UInt0)) ? uint.MaxValue : 0U),
					UInt1 = ((X86.IsNaN(a.UInt1) || X86.IsNaN(b.UInt1)) ? uint.MaxValue : 0U),
					UInt2 = ((X86.IsNaN(a.UInt2) || X86.IsNaN(b.UInt2)) ? uint.MaxValue : 0U),
					UInt3 = ((X86.IsNaN(a.UInt3) || X86.IsNaN(b.UInt3)) ? uint.MaxValue : 0U)
				};
			}

			// Token: 0x06000CD0 RID: 3280 RVA: 0x00011E96 File Offset: 0x00010096
			[DebuggerStepThrough]
			public static int comieq_ss(v128 a, v128 b)
			{
				if (a.Float0 != b.Float0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000CD1 RID: 3281 RVA: 0x00011EA9 File Offset: 0x000100A9
			[DebuggerStepThrough]
			public static int comilt_ss(v128 a, v128 b)
			{
				if (a.Float0 >= b.Float0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000CD2 RID: 3282 RVA: 0x00011EBC File Offset: 0x000100BC
			[DebuggerStepThrough]
			public static int comile_ss(v128 a, v128 b)
			{
				if (a.Float0 > b.Float0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000CD3 RID: 3283 RVA: 0x00011ECF File Offset: 0x000100CF
			[DebuggerStepThrough]
			public static int comigt_ss(v128 a, v128 b)
			{
				if (a.Float0 <= b.Float0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000CD4 RID: 3284 RVA: 0x00011EE2 File Offset: 0x000100E2
			[DebuggerStepThrough]
			public static int comige_ss(v128 a, v128 b)
			{
				if (a.Float0 < b.Float0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000CD5 RID: 3285 RVA: 0x00011EF5 File Offset: 0x000100F5
			[DebuggerStepThrough]
			public static int comineq_ss(v128 a, v128 b)
			{
				if (a.Float0 == b.Float0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000CD6 RID: 3286 RVA: 0x00011F08 File Offset: 0x00010108
			[DebuggerStepThrough]
			public static int ucomieq_ss(v128 a, v128 b)
			{
				if (a.Float0 != b.Float0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000CD7 RID: 3287 RVA: 0x00011F1B File Offset: 0x0001011B
			[DebuggerStepThrough]
			public static int ucomilt_ss(v128 a, v128 b)
			{
				if (a.Float0 >= b.Float0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000CD8 RID: 3288 RVA: 0x00011F2E File Offset: 0x0001012E
			[DebuggerStepThrough]
			public static int ucomile_ss(v128 a, v128 b)
			{
				if (a.Float0 > b.Float0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000CD9 RID: 3289 RVA: 0x00011F41 File Offset: 0x00010141
			[DebuggerStepThrough]
			public static int ucomigt_ss(v128 a, v128 b)
			{
				if (a.Float0 <= b.Float0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000CDA RID: 3290 RVA: 0x00011F54 File Offset: 0x00010154
			[DebuggerStepThrough]
			public static int ucomige_ss(v128 a, v128 b)
			{
				if (a.Float0 < b.Float0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000CDB RID: 3291 RVA: 0x00011F67 File Offset: 0x00010167
			[DebuggerStepThrough]
			public static int ucomineq_ss(v128 a, v128 b)
			{
				if (a.Float0 == b.Float0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000CDC RID: 3292 RVA: 0x00011F7A File Offset: 0x0001017A
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static int cvtss_si32(v128 a)
			{
				return X86.Sse.cvt_ss2si(a);
			}

			// Token: 0x06000CDD RID: 3293 RVA: 0x00011F82 File Offset: 0x00010182
			[DebuggerStepThrough]
			public static int cvt_ss2si(v128 a)
			{
				return (int)Math.Round((double)a.Float0, MidpointRounding.ToEven);
			}

			// Token: 0x06000CDE RID: 3294 RVA: 0x00011F92 File Offset: 0x00010192
			[DebuggerStepThrough]
			public static long cvtss_si64(v128 a)
			{
				return (long)Math.Round((double)a.Float0, MidpointRounding.ToEven);
			}

			// Token: 0x06000CDF RID: 3295 RVA: 0x00011FA2 File Offset: 0x000101A2
			[DebuggerStepThrough]
			public static float cvtss_f32(v128 a)
			{
				return a.Float0;
			}

			// Token: 0x06000CE0 RID: 3296 RVA: 0x00011FAC File Offset: 0x000101AC
			[DebuggerStepThrough]
			public static int cvttss_si32(v128 a)
			{
				int result;
				using (new X86.RoundingScope(X86.MXCSRBits.RoundingControlMask))
				{
					result = (int)a.Float0;
				}
				return result;
			}

			// Token: 0x06000CE1 RID: 3297 RVA: 0x00011FF0 File Offset: 0x000101F0
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static int cvtt_ss2si(v128 a)
			{
				return X86.Sse.cvttss_si32(a);
			}

			// Token: 0x06000CE2 RID: 3298 RVA: 0x00011FF8 File Offset: 0x000101F8
			[DebuggerStepThrough]
			public static long cvttss_si64(v128 a)
			{
				long result;
				using (new X86.RoundingScope(X86.MXCSRBits.RoundingControlMask))
				{
					result = (long)a.Float0;
				}
				return result;
			}

			// Token: 0x06000CE3 RID: 3299 RVA: 0x0001203C File Offset: 0x0001023C
			[DebuggerStepThrough]
			public static v128 set_ss(float a)
			{
				return new v128(a, 0f, 0f, 0f);
			}

			// Token: 0x06000CE4 RID: 3300 RVA: 0x00012053 File Offset: 0x00010253
			[DebuggerStepThrough]
			public static v128 set1_ps(float a)
			{
				return new v128(a, a, a, a);
			}

			// Token: 0x06000CE5 RID: 3301 RVA: 0x0001205E File Offset: 0x0001025E
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 set_ps1(float a)
			{
				return X86.Sse.set1_ps(a);
			}

			// Token: 0x06000CE6 RID: 3302 RVA: 0x00012066 File Offset: 0x00010266
			[DebuggerStepThrough]
			public static v128 set_ps(float e3, float e2, float e1, float e0)
			{
				return new v128(e0, e1, e2, e3);
			}

			// Token: 0x06000CE7 RID: 3303 RVA: 0x00012071 File Offset: 0x00010271
			[DebuggerStepThrough]
			public static v128 setr_ps(float e3, float e2, float e1, float e0)
			{
				return new v128(e3, e2, e1, e0);
			}

			// Token: 0x06000CE8 RID: 3304 RVA: 0x0001207C File Offset: 0x0001027C
			[DebuggerStepThrough]
			public static v128 move_ss(v128 a, v128 b)
			{
				v128 result = a;
				result.Float0 = b.Float0;
				return result;
			}

			// Token: 0x06000CE9 RID: 3305 RVA: 0x00012099 File Offset: 0x00010299
			public static int SHUFFLE(int d, int c, int b, int a)
			{
				return (a & 3) | (b & 3) << 2 | (c & 3) << 4 | (d & 3) << 6;
			}

			// Token: 0x06000CEA RID: 3306 RVA: 0x000120B0 File Offset: 0x000102B0
			[DebuggerStepThrough]
			public unsafe static v128 shuffle_ps(v128 a, v128 b, int imm8)
			{
				v128 result = default(v128);
				uint* ptr = &a.UInt0;
				uint* ptr2 = &b.UInt0;
				result.UInt0 = ptr[imm8 & 3];
				result.UInt1 = ptr[imm8 >> 2 & 3];
				result.UInt2 = ptr2[imm8 >> 4 & 3];
				result.UInt3 = ptr2[imm8 >> 6 & 3];
				return result;
			}

			// Token: 0x06000CEB RID: 3307 RVA: 0x00012120 File Offset: 0x00010320
			[DebuggerStepThrough]
			public static v128 unpackhi_ps(v128 a, v128 b)
			{
				return new v128
				{
					Float0 = a.Float2,
					Float1 = b.Float2,
					Float2 = a.Float3,
					Float3 = b.Float3
				};
			}

			// Token: 0x06000CEC RID: 3308 RVA: 0x0001216C File Offset: 0x0001036C
			[DebuggerStepThrough]
			public static v128 unpacklo_ps(v128 a, v128 b)
			{
				return new v128
				{
					Float0 = a.Float0,
					Float1 = b.Float0,
					Float2 = a.Float1,
					Float3 = b.Float1
				};
			}

			// Token: 0x06000CED RID: 3309 RVA: 0x000121B8 File Offset: 0x000103B8
			[DebuggerStepThrough]
			public static v128 movehl_ps(v128 a, v128 b)
			{
				return new v128
				{
					Float0 = b.Float2,
					Float1 = b.Float3,
					Float2 = a.Float2,
					Float3 = a.Float3
				};
			}

			// Token: 0x06000CEE RID: 3310 RVA: 0x00012204 File Offset: 0x00010404
			[DebuggerStepThrough]
			public static v128 movelh_ps(v128 a, v128 b)
			{
				return new v128
				{
					Float0 = a.Float0,
					Float1 = a.Float1,
					Float2 = b.Float0,
					Float3 = b.Float1
				};
			}

			// Token: 0x06000CEF RID: 3311 RVA: 0x00012250 File Offset: 0x00010450
			[DebuggerStepThrough]
			public static int movemask_ps(v128 a)
			{
				int num = 0;
				if ((a.UInt0 & 2147483648U) != 0U)
				{
					num |= 1;
				}
				if ((a.UInt1 & 2147483648U) != 0U)
				{
					num |= 2;
				}
				if ((a.UInt2 & 2147483648U) != 0U)
				{
					num |= 4;
				}
				if ((a.UInt3 & 2147483648U) != 0U)
				{
					num |= 8;
				}
				return num;
			}

			// Token: 0x06000CF0 RID: 3312 RVA: 0x000122A8 File Offset: 0x000104A8
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static void TRANSPOSE4_PS(ref v128 row0, ref v128 row1, ref v128 row2, ref v128 row3)
			{
				v128 a = X86.Sse.shuffle_ps(row0, row1, 68);
				v128 a2 = X86.Sse.shuffle_ps(row0, row1, 238);
				v128 b = X86.Sse.shuffle_ps(row2, row3, 68);
				v128 b2 = X86.Sse.shuffle_ps(row2, row3, 238);
				row0 = X86.Sse.shuffle_ps(a, b, 136);
				row1 = X86.Sse.shuffle_ps(a, b, 221);
				row2 = X86.Sse.shuffle_ps(a2, b2, 136);
				row3 = X86.Sse.shuffle_ps(a2, b2, 221);
			}

			// Token: 0x06000CF1 RID: 3313 RVA: 0x00012354 File Offset: 0x00010554
			[DebuggerStepThrough]
			public static v128 setzero_ps()
			{
				return default(v128);
			}

			// Token: 0x06000CF2 RID: 3314 RVA: 0x0001236A File Offset: 0x0001056A
			[DebuggerStepThrough]
			public unsafe static v128 loadu_si16(void* mem_addr)
			{
				return new v128(*(short*)mem_addr, 0, 0, 0, 0, 0, 0, 0);
			}

			// Token: 0x06000CF3 RID: 3315 RVA: 0x0001237A File Offset: 0x0001057A
			public unsafe static void storeu_si16(void* mem_addr, v128 a)
			{
				*(short*)mem_addr = a.SShort0;
			}

			// Token: 0x06000CF4 RID: 3316 RVA: 0x00012384 File Offset: 0x00010584
			[DebuggerStepThrough]
			public unsafe static v128 loadu_si64(void* mem_addr)
			{
				return new v128(*(long*)mem_addr, 0L);
			}

			// Token: 0x06000CF5 RID: 3317 RVA: 0x0001238F File Offset: 0x0001058F
			[DebuggerStepThrough]
			public unsafe static void storeu_si64(void* mem_addr, v128 a)
			{
				*(long*)mem_addr = a.SLong0;
			}
		}

		// Token: 0x02000049 RID: 73
		public static class Sse2
		{
			// Token: 0x17000049 RID: 73
			// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x00012399 File Offset: 0x00010599
			public static bool IsSse2Supported
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000CF7 RID: 3319 RVA: 0x0001239C File Offset: 0x0001059C
			[DebuggerStepThrough]
			public static int SHUFFLE2(int x, int y)
			{
				return y | x << 1;
			}

			// Token: 0x06000CF8 RID: 3320 RVA: 0x000123A3 File Offset: 0x000105A3
			[DebuggerStepThrough]
			public unsafe static void stream_si32(int* mem_addr, int a)
			{
				*mem_addr = a;
			}

			// Token: 0x06000CF9 RID: 3321 RVA: 0x000123A8 File Offset: 0x000105A8
			[DebuggerStepThrough]
			public unsafe static void stream_si64(long* mem_addr, long a)
			{
				*mem_addr = a;
			}

			// Token: 0x06000CFA RID: 3322 RVA: 0x000123AD File Offset: 0x000105AD
			[DebuggerStepThrough]
			public unsafe static void stream_pd(void* mem_addr, v128 a)
			{
				X86.GenericCSharpStore(mem_addr, a);
			}

			// Token: 0x06000CFB RID: 3323 RVA: 0x000123B6 File Offset: 0x000105B6
			[DebuggerStepThrough]
			public unsafe static void stream_si128(void* mem_addr, v128 a)
			{
				X86.GenericCSharpStore(mem_addr, a);
			}

			// Token: 0x06000CFC RID: 3324 RVA: 0x000123C0 File Offset: 0x000105C0
			[DebuggerStepThrough]
			public unsafe static v128 add_epi8(v128 a, v128 b)
			{
				v128 result = default(v128);
				sbyte* ptr = &result.SByte0;
				sbyte* ptr2 = &a.SByte0;
				sbyte* ptr3 = &b.SByte0;
				for (int i = 0; i <= 15; i++)
				{
					ptr[i] = ptr2[i] + ptr3[i];
				}
				return result;
			}

			// Token: 0x06000CFD RID: 3325 RVA: 0x00012414 File Offset: 0x00010614
			[DebuggerStepThrough]
			public unsafe static v128 add_epi16(v128 a, v128 b)
			{
				v128 result = default(v128);
				short* ptr = &result.SShort0;
				short* ptr2 = &a.SShort0;
				short* ptr3 = &b.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					ptr[i] = ptr2[i] + ptr3[i];
				}
				return result;
			}

			// Token: 0x06000CFE RID: 3326 RVA: 0x00012470 File Offset: 0x00010670
			[DebuggerStepThrough]
			public static v128 add_epi32(v128 a, v128 b)
			{
				return new v128
				{
					SInt0 = a.SInt0 + b.SInt0,
					SInt1 = a.SInt1 + b.SInt1,
					SInt2 = a.SInt2 + b.SInt2,
					SInt3 = a.SInt3 + b.SInt3
				};
			}

			// Token: 0x06000CFF RID: 3327 RVA: 0x000124D8 File Offset: 0x000106D8
			[DebuggerStepThrough]
			public static v128 add_epi64(v128 a, v128 b)
			{
				return new v128
				{
					SLong0 = a.SLong0 + b.SLong0,
					SLong1 = a.SLong1 + b.SLong1
				};
			}

			// Token: 0x06000D00 RID: 3328 RVA: 0x00012518 File Offset: 0x00010718
			[DebuggerStepThrough]
			public unsafe static v128 adds_epi8(v128 a, v128 b)
			{
				v128 result = default(v128);
				sbyte* ptr = &result.SByte0;
				sbyte* ptr2 = &a.SByte0;
				sbyte* ptr3 = &b.SByte0;
				for (int i = 0; i <= 15; i++)
				{
					ptr[i] = X86.Saturate_To_Int8((int)(ptr2[i] + ptr3[i]));
				}
				return result;
			}

			// Token: 0x06000D01 RID: 3329 RVA: 0x00012570 File Offset: 0x00010770
			[DebuggerStepThrough]
			public unsafe static v128 adds_epi16(v128 a, v128 b)
			{
				v128 result = default(v128);
				short* ptr = &result.SShort0;
				short* ptr2 = &a.SShort0;
				short* ptr3 = &b.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					ptr[i] = X86.Saturate_To_Int16((int)(ptr2[i] + ptr3[i]));
				}
				return result;
			}

			// Token: 0x06000D02 RID: 3330 RVA: 0x000125D0 File Offset: 0x000107D0
			[DebuggerStepThrough]
			public unsafe static v128 adds_epu8(v128 a, v128 b)
			{
				v128 result = default(v128);
				byte* ptr = &result.Byte0;
				byte* ptr2 = &a.Byte0;
				byte* ptr3 = &b.Byte0;
				for (int i = 0; i <= 15; i++)
				{
					ptr[i] = X86.Saturate_To_UnsignedInt8((int)(ptr2[i] + ptr3[i]));
				}
				return result;
			}

			// Token: 0x06000D03 RID: 3331 RVA: 0x00012628 File Offset: 0x00010828
			[DebuggerStepThrough]
			public unsafe static v128 adds_epu16(v128 a, v128 b)
			{
				v128 result = default(v128);
				ushort* ptr = &result.UShort0;
				ushort* ptr2 = &a.UShort0;
				ushort* ptr3 = &b.UShort0;
				for (int i = 0; i <= 7; i++)
				{
					ptr[i] = X86.Saturate_To_UnsignedInt16((int)(ptr2[i] + ptr3[i]));
				}
				return result;
			}

			// Token: 0x06000D04 RID: 3332 RVA: 0x00012688 File Offset: 0x00010888
			[DebuggerStepThrough]
			public unsafe static v128 avg_epu8(v128 a, v128 b)
			{
				v128 result = default(v128);
				byte* ptr = &result.Byte0;
				byte* ptr2 = &a.Byte0;
				byte* ptr3 = &b.Byte0;
				for (int i = 0; i <= 15; i++)
				{
					ptr[i] = (byte)(ptr2[i] + ptr3[i] + 1 >> 1);
				}
				return result;
			}

			// Token: 0x06000D05 RID: 3333 RVA: 0x000126E0 File Offset: 0x000108E0
			[DebuggerStepThrough]
			public unsafe static v128 avg_epu16(v128 a, v128 b)
			{
				v128 result = default(v128);
				ushort* ptr = &result.UShort0;
				ushort* ptr2 = &a.UShort0;
				ushort* ptr3 = &b.UShort0;
				for (int i = 0; i <= 7; i++)
				{
					ptr[i] = (ushort)(ptr2[i] + ptr3[i] + 1 >> 1);
				}
				return result;
			}

			// Token: 0x06000D06 RID: 3334 RVA: 0x00012740 File Offset: 0x00010940
			[DebuggerStepThrough]
			public unsafe static v128 madd_epi16(v128 a, v128 b)
			{
				v128 result = default(v128);
				int* ptr = &result.SInt0;
				short* ptr2 = &a.SShort0;
				short* ptr3 = &b.SShort0;
				for (int i = 0; i <= 3; i++)
				{
					int num = 2 * i;
					int num2 = (int)(ptr2[num + 1] * ptr3[num + 1]);
					int num3 = (int)(ptr2[num] * ptr3[num]);
					ptr[i] = num2 + num3;
				}
				return result;
			}

			// Token: 0x06000D07 RID: 3335 RVA: 0x000127C0 File Offset: 0x000109C0
			[DebuggerStepThrough]
			public unsafe static v128 max_epi16(v128 a, v128 b)
			{
				v128 result = default(v128);
				short* ptr = &result.SShort0;
				short* ptr2 = &a.SShort0;
				short* ptr3 = &b.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					ptr[i] = Math.Max(ptr2[i], ptr3[i]);
				}
				return result;
			}

			// Token: 0x06000D08 RID: 3336 RVA: 0x00012820 File Offset: 0x00010A20
			[DebuggerStepThrough]
			public unsafe static v128 max_epu8(v128 a, v128 b)
			{
				v128 result = default(v128);
				byte* ptr = &result.Byte0;
				byte* ptr2 = &a.Byte0;
				byte* ptr3 = &b.Byte0;
				for (int i = 0; i <= 15; i++)
				{
					ptr[i] = Math.Max(ptr2[i], ptr3[i]);
				}
				return result;
			}

			// Token: 0x06000D09 RID: 3337 RVA: 0x00012878 File Offset: 0x00010A78
			[DebuggerStepThrough]
			public unsafe static v128 min_epi16(v128 a, v128 b)
			{
				v128 result = default(v128);
				short* ptr = &result.SShort0;
				short* ptr2 = &a.SShort0;
				short* ptr3 = &b.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					ptr[i] = Math.Min(ptr2[i], ptr3[i]);
				}
				return result;
			}

			// Token: 0x06000D0A RID: 3338 RVA: 0x000128D8 File Offset: 0x00010AD8
			[DebuggerStepThrough]
			public unsafe static v128 min_epu8(v128 a, v128 b)
			{
				v128 result = default(v128);
				byte* ptr = &result.Byte0;
				byte* ptr2 = &a.Byte0;
				byte* ptr3 = &b.Byte0;
				for (int i = 0; i <= 15; i++)
				{
					ptr[i] = Math.Min(ptr2[i], ptr3[i]);
				}
				return result;
			}

			// Token: 0x06000D0B RID: 3339 RVA: 0x00012930 File Offset: 0x00010B30
			[DebuggerStepThrough]
			public unsafe static v128 mulhi_epi16(v128 a, v128 b)
			{
				v128 result = default(v128);
				short* ptr = &result.SShort0;
				short* ptr2 = &a.SShort0;
				short* ptr3 = &b.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					int num = (int)(ptr2[i] * ptr3[i]);
					ptr[i] = (short)(num >> 16);
				}
				return result;
			}

			// Token: 0x06000D0C RID: 3340 RVA: 0x00012994 File Offset: 0x00010B94
			[DebuggerStepThrough]
			public unsafe static v128 mulhi_epu16(v128 a, v128 b)
			{
				v128 result = default(v128);
				ushort* ptr = &result.UShort0;
				ushort* ptr2 = &a.UShort0;
				ushort* ptr3 = &b.UShort0;
				for (int i = 0; i <= 7; i++)
				{
					uint num = (uint)(ptr2[i] * ptr3[i]);
					ptr[i] = (ushort)(num >> 16);
				}
				return result;
			}

			// Token: 0x06000D0D RID: 3341 RVA: 0x000129F8 File Offset: 0x00010BF8
			[DebuggerStepThrough]
			public unsafe static v128 mullo_epi16(v128 a, v128 b)
			{
				v128 result = default(v128);
				short* ptr = &result.SShort0;
				short* ptr2 = &a.SShort0;
				short* ptr3 = &b.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					int num = (int)(ptr2[i] * ptr3[i]);
					ptr[i] = (short)num;
				}
				return result;
			}

			// Token: 0x06000D0E RID: 3342 RVA: 0x00012A58 File Offset: 0x00010C58
			[DebuggerStepThrough]
			public static v128 mul_epu32(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = (ulong)a.UInt0 * (ulong)b.UInt0,
					ULong1 = (ulong)a.UInt2 * (ulong)b.UInt2
				};
			}

			// Token: 0x06000D0F RID: 3343 RVA: 0x00012A9C File Offset: 0x00010C9C
			[DebuggerStepThrough]
			public unsafe static v128 sad_epu8(v128 a, v128 b)
			{
				v128 v;
				byte* ptr = &v.Byte0;
				byte* ptr2 = &a.Byte0;
				byte* ptr3 = &b.Byte0;
				for (int i = 0; i <= 15; i++)
				{
					ptr[i] = (byte)Math.Abs((int)(ptr2[i] - ptr3[i]));
				}
				v128 result = default(v128);
				ushort* ptr4 = &result.UShort0;
				for (int j = 0; j <= 1; j++)
				{
					int num = j * 8;
					ptr4[4 * j] = (ushort)(ptr[num] + ptr[num + 1] + ptr[num + 2] + ptr[num + 3] + ptr[num + 4] + ptr[num + 5] + ptr[num + 6] + ptr[num + 7]);
				}
				return result;
			}

			// Token: 0x06000D10 RID: 3344 RVA: 0x00012B60 File Offset: 0x00010D60
			[DebuggerStepThrough]
			public unsafe static v128 sub_epi8(v128 a, v128 b)
			{
				v128 result = default(v128);
				sbyte* ptr = &result.SByte0;
				sbyte* ptr2 = &a.SByte0;
				sbyte* ptr3 = &b.SByte0;
				for (int i = 0; i <= 15; i++)
				{
					ptr[i] = ptr2[i] - ptr3[i];
				}
				return result;
			}

			// Token: 0x06000D11 RID: 3345 RVA: 0x00012BB4 File Offset: 0x00010DB4
			[DebuggerStepThrough]
			public unsafe static v128 sub_epi16(v128 a, v128 b)
			{
				v128 result = default(v128);
				short* ptr = &result.SShort0;
				short* ptr2 = &a.SShort0;
				short* ptr3 = &b.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					ptr[i] = ptr2[i] - ptr3[i];
				}
				return result;
			}

			// Token: 0x06000D12 RID: 3346 RVA: 0x00012C10 File Offset: 0x00010E10
			[DebuggerStepThrough]
			public unsafe static v128 sub_epi32(v128 a, v128 b)
			{
				v128 result = default(v128);
				int* ptr = &result.SInt0;
				int* ptr2 = &a.SInt0;
				int* ptr3 = &b.SInt0;
				for (int i = 0; i <= 3; i++)
				{
					ptr[i] = ptr2[i] - ptr3[i];
				}
				return result;
			}

			// Token: 0x06000D13 RID: 3347 RVA: 0x00012C6C File Offset: 0x00010E6C
			[DebuggerStepThrough]
			public unsafe static v128 sub_epi64(v128 a, v128 b)
			{
				v128 result = default(v128);
				long* ptr = &result.SLong0;
				long* ptr2 = &a.SLong0;
				long* ptr3 = &b.SLong0;
				for (int i = 0; i <= 1; i++)
				{
					ptr[i] = ptr2[i] - ptr3[i];
				}
				return result;
			}

			// Token: 0x06000D14 RID: 3348 RVA: 0x00012CC8 File Offset: 0x00010EC8
			[DebuggerStepThrough]
			public unsafe static v128 subs_epi8(v128 a, v128 b)
			{
				v128 result = default(v128);
				sbyte* ptr = &result.SByte0;
				sbyte* ptr2 = &a.SByte0;
				sbyte* ptr3 = &b.SByte0;
				for (int i = 0; i <= 15; i++)
				{
					ptr[i] = X86.Saturate_To_Int8((int)(ptr2[i] - ptr3[i]));
				}
				return result;
			}

			// Token: 0x06000D15 RID: 3349 RVA: 0x00012D20 File Offset: 0x00010F20
			[DebuggerStepThrough]
			public unsafe static v128 subs_epi16(v128 a, v128 b)
			{
				v128 result = default(v128);
				short* ptr = &result.SShort0;
				short* ptr2 = &a.SShort0;
				short* ptr3 = &b.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					ptr[i] = X86.Saturate_To_Int16((int)(ptr2[i] - ptr3[i]));
				}
				return result;
			}

			// Token: 0x06000D16 RID: 3350 RVA: 0x00012D80 File Offset: 0x00010F80
			[DebuggerStepThrough]
			public unsafe static v128 subs_epu8(v128 a, v128 b)
			{
				v128 result = default(v128);
				byte* ptr = &result.Byte0;
				byte* ptr2 = &a.Byte0;
				byte* ptr3 = &b.Byte0;
				for (int i = 0; i <= 15; i++)
				{
					ptr[i] = X86.Saturate_To_UnsignedInt8((int)(ptr2[i] - ptr3[i]));
				}
				return result;
			}

			// Token: 0x06000D17 RID: 3351 RVA: 0x00012DD8 File Offset: 0x00010FD8
			[DebuggerStepThrough]
			public unsafe static v128 subs_epu16(v128 a, v128 b)
			{
				v128 result = default(v128);
				ushort* ptr = &result.UShort0;
				ushort* ptr2 = &a.UShort0;
				ushort* ptr3 = &b.UShort0;
				for (int i = 0; i <= 7; i++)
				{
					ptr[i] = X86.Saturate_To_UnsignedInt16((int)(ptr2[i] - ptr3[i]));
				}
				return result;
			}

			// Token: 0x06000D18 RID: 3352 RVA: 0x00012E38 File Offset: 0x00011038
			[DebuggerStepThrough]
			public unsafe static v128 slli_si128(v128 a, int imm8)
			{
				int num = Math.Min(imm8 & 255, 16);
				v128 result = default(v128);
				byte* ptr = &result.Byte0;
				byte* ptr2 = &a.Byte0;
				for (int i = 0; i < num; i++)
				{
					ptr[i] = 0;
				}
				for (int j = num; j < 16; j++)
				{
					ptr[j] = ptr2[j - num];
				}
				return result;
			}

			// Token: 0x06000D19 RID: 3353 RVA: 0x00012EA2 File Offset: 0x000110A2
			[DebuggerStepThrough]
			public static v128 bslli_si128(v128 a, int imm8)
			{
				return X86.Sse2.slli_si128(a, imm8);
			}

			// Token: 0x06000D1A RID: 3354 RVA: 0x00012EAC File Offset: 0x000110AC
			[DebuggerStepThrough]
			public unsafe static v128 bsrli_si128(v128 a, int imm8)
			{
				int num = Math.Min(imm8 & 255, 16);
				v128 result = default(v128);
				byte* ptr = &result.Byte0;
				byte* ptr2 = &a.Byte0;
				for (int i = 0; i < 16 - num; i++)
				{
					ptr[i] = ptr2[num + i];
				}
				for (int j = 16 - num; j < 16; j++)
				{
					ptr[j] = 0;
				}
				return result;
			}

			// Token: 0x06000D1B RID: 3355 RVA: 0x00012F1C File Offset: 0x0001111C
			[DebuggerStepThrough]
			public unsafe static v128 slli_epi16(v128 a, int imm8)
			{
				v128 result = default(v128);
				int num = imm8 & 255;
				ushort* ptr = &result.UShort0;
				ushort* ptr2 = &a.UShort0;
				for (int i = 0; i <= 7; i++)
				{
					if (num > 15)
					{
						ptr[i] = 0;
					}
					else
					{
						ptr[i] = (ushort)(ptr2[i] << num);
					}
				}
				return result;
			}

			// Token: 0x06000D1C RID: 3356 RVA: 0x00012F84 File Offset: 0x00011184
			[DebuggerStepThrough]
			public unsafe static v128 sll_epi16(v128 a, v128 count)
			{
				v128 result = default(v128);
				int num = (int)Math.Min(count.ULong0, 16UL);
				ushort* ptr = &result.UShort0;
				ushort* ptr2 = &a.UShort0;
				for (int i = 0; i <= 7; i++)
				{
					if (num > 15)
					{
						ptr[i] = 0;
					}
					else
					{
						ptr[i] = (ushort)(ptr2[i] << num);
					}
				}
				return result;
			}

			// Token: 0x06000D1D RID: 3357 RVA: 0x00012FF4 File Offset: 0x000111F4
			[DebuggerStepThrough]
			public unsafe static v128 slli_epi32(v128 a, int imm8)
			{
				v128 result = default(v128);
				int num = Math.Min(imm8 & 255, 32);
				uint* ptr = &result.UInt0;
				uint* ptr2 = &a.UInt0;
				for (int i = 0; i <= 3; i++)
				{
					if (num > 31)
					{
						ptr[i] = 0U;
					}
					else
					{
						ptr[i] = ptr2[i] << num;
					}
				}
				return result;
			}

			// Token: 0x06000D1E RID: 3358 RVA: 0x00013060 File Offset: 0x00011260
			[DebuggerStepThrough]
			public unsafe static v128 sll_epi32(v128 a, v128 count)
			{
				v128 result = default(v128);
				int num = (int)Math.Min(count.ULong0, 32UL);
				uint* ptr = &result.UInt0;
				uint* ptr2 = &a.UInt0;
				for (int i = 0; i <= 3; i++)
				{
					if (num > 31)
					{
						ptr[i] = 0U;
					}
					else
					{
						ptr[i] = ptr2[i] << num;
					}
				}
				return result;
			}

			// Token: 0x06000D1F RID: 3359 RVA: 0x000130D0 File Offset: 0x000112D0
			[DebuggerStepThrough]
			public unsafe static v128 slli_epi64(v128 a, int imm8)
			{
				v128 result = default(v128);
				int num = Math.Min(imm8 & 255, 64);
				ulong* ptr = &result.ULong0;
				ulong* ptr2 = &a.ULong0;
				for (int i = 0; i <= 1; i++)
				{
					if (num > 63)
					{
						ptr[i] = 0UL;
					}
					else
					{
						ptr[i] = ptr2[i] << num;
					}
				}
				return result;
			}

			// Token: 0x06000D20 RID: 3360 RVA: 0x00013140 File Offset: 0x00011340
			[DebuggerStepThrough]
			public unsafe static v128 sll_epi64(v128 a, v128 count)
			{
				v128 result = default(v128);
				int num = (int)Math.Min(count.ULong0, 64UL);
				ulong* ptr = &result.ULong0;
				ulong* ptr2 = &a.ULong0;
				for (int i = 0; i <= 1; i++)
				{
					if (num > 63)
					{
						ptr[i] = 0UL;
					}
					else
					{
						ptr[i] = ptr2[i] << num;
					}
				}
				return result;
			}

			// Token: 0x06000D21 RID: 3361 RVA: 0x000131B0 File Offset: 0x000113B0
			[DebuggerStepThrough]
			public unsafe static v128 srai_epi16(v128 a, int imm8)
			{
				int num = Math.Min(imm8 & 255, 16);
				v128 result = a;
				short* ptr = &result.SShort0;
				if (num > 0)
				{
					num--;
					for (int i = 0; i <= 7; i++)
					{
						short* ptr2 = ptr + i;
						*ptr2 = (short)(*ptr2 >> 1);
						short* ptr3 = ptr + i;
						*ptr3 = (short)(*ptr3 >> num);
					}
				}
				return result;
			}

			// Token: 0x06000D22 RID: 3362 RVA: 0x00013208 File Offset: 0x00011408
			[DebuggerStepThrough]
			public unsafe static v128 sra_epi16(v128 a, v128 count)
			{
				int num = (int)Math.Min(count.ULong0, 16UL);
				v128 result = a;
				short* ptr = &result.SShort0;
				if (num > 0)
				{
					num--;
					for (int i = 0; i <= 7; i++)
					{
						short* ptr2 = ptr + i;
						*ptr2 = (short)(*ptr2 >> 1);
						short* ptr3 = ptr + i;
						*ptr3 = (short)(*ptr3 >> num);
					}
				}
				return result;
			}

			// Token: 0x06000D23 RID: 3363 RVA: 0x00013260 File Offset: 0x00011460
			[DebuggerStepThrough]
			public unsafe static v128 srai_epi32(v128 a, int imm8)
			{
				int num = Math.Min(imm8 & 255, 32);
				v128 result = a;
				int* ptr = &result.SInt0;
				if (num > 0)
				{
					num--;
					for (int i = 0; i <= 3; i++)
					{
						ptr[i] >>= 1;
						ptr[i] >>= num;
					}
				}
				return result;
			}

			// Token: 0x06000D24 RID: 3364 RVA: 0x000132B8 File Offset: 0x000114B8
			[DebuggerStepThrough]
			public unsafe static v128 sra_epi32(v128 a, v128 count)
			{
				int num = (int)Math.Min(count.ULong0, 32UL);
				v128 result = a;
				int* ptr = &result.SInt0;
				if (num > 0)
				{
					num--;
					for (int i = 0; i <= 3; i++)
					{
						ptr[i] >>= 1;
						ptr[i] >>= num;
					}
				}
				return result;
			}

			// Token: 0x06000D25 RID: 3365 RVA: 0x0001330E File Offset: 0x0001150E
			[DebuggerStepThrough]
			public static v128 srli_si128(v128 a, int imm8)
			{
				return X86.Sse2.bsrli_si128(a, imm8);
			}

			// Token: 0x06000D26 RID: 3366 RVA: 0x00013318 File Offset: 0x00011518
			[DebuggerStepThrough]
			public unsafe static v128 srli_epi16(v128 a, int imm8)
			{
				int num = Math.Min(imm8 & 255, 16);
				v128 result = a;
				ushort* ptr = &result.UShort0;
				if (num > 0)
				{
					num--;
					for (int i = 0; i <= 7; i++)
					{
						ushort* ptr2 = ptr + i;
						*ptr2 = (ushort)(*ptr2 >> 1);
						ushort* ptr3 = ptr + i;
						*ptr3 = (ushort)(*ptr3 >> num);
					}
				}
				return result;
			}

			// Token: 0x06000D27 RID: 3367 RVA: 0x00013370 File Offset: 0x00011570
			[DebuggerStepThrough]
			public unsafe static v128 srl_epi16(v128 a, v128 count)
			{
				int num = (int)Math.Min(count.ULong0, 16UL);
				v128 result = a;
				ushort* ptr = &result.UShort0;
				if (num > 0)
				{
					num--;
					for (int i = 0; i <= 7; i++)
					{
						ushort* ptr2 = ptr + i;
						*ptr2 = (ushort)(*ptr2 >> 1);
						ushort* ptr3 = ptr + i;
						*ptr3 = (ushort)(*ptr3 >> num);
					}
				}
				return result;
			}

			// Token: 0x06000D28 RID: 3368 RVA: 0x000133C8 File Offset: 0x000115C8
			[DebuggerStepThrough]
			public unsafe static v128 srli_epi32(v128 a, int imm8)
			{
				int num = Math.Min(imm8 & 255, 32);
				v128 result = a;
				uint* ptr = &result.UInt0;
				if (num > 0)
				{
					num--;
					for (int i = 0; i <= 3; i++)
					{
						ptr[i] >>= 1;
						ptr[i] >>= num;
					}
				}
				return result;
			}

			// Token: 0x06000D29 RID: 3369 RVA: 0x00013420 File Offset: 0x00011620
			[DebuggerStepThrough]
			public unsafe static v128 srl_epi32(v128 a, v128 count)
			{
				int num = (int)Math.Min(count.ULong0, 32UL);
				v128 result = a;
				uint* ptr = &result.UInt0;
				if (num > 0)
				{
					num--;
					for (int i = 0; i <= 3; i++)
					{
						ptr[i] >>= 1;
						ptr[i] >>= num;
					}
				}
				return result;
			}

			// Token: 0x06000D2A RID: 3370 RVA: 0x00013478 File Offset: 0x00011678
			[DebuggerStepThrough]
			public unsafe static v128 srli_epi64(v128 a, int imm8)
			{
				int num = Math.Min(imm8 & 255, 64);
				v128 result = a;
				ulong* ptr = &result.ULong0;
				if (num > 0)
				{
					num--;
					for (int i = 0; i <= 1; i++)
					{
						ptr[i] >>= 1;
						ptr[i] >>= num;
					}
				}
				return result;
			}

			// Token: 0x06000D2B RID: 3371 RVA: 0x000134D0 File Offset: 0x000116D0
			[DebuggerStepThrough]
			public unsafe static v128 srl_epi64(v128 a, v128 count)
			{
				int num = (int)Math.Min(count.ULong0, 64UL);
				v128 result = a;
				ulong* ptr = &result.ULong0;
				if (num > 0)
				{
					num--;
					for (int i = 0; i <= 1; i++)
					{
						ptr[i] >>= 1;
						ptr[i] >>= num;
					}
				}
				return result;
			}

			// Token: 0x06000D2C RID: 3372 RVA: 0x00013528 File Offset: 0x00011728
			[DebuggerStepThrough]
			public static v128 and_si128(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = (a.ULong0 & b.ULong0),
					ULong1 = (a.ULong1 & b.ULong1)
				};
			}

			// Token: 0x06000D2D RID: 3373 RVA: 0x00013568 File Offset: 0x00011768
			[DebuggerStepThrough]
			public static v128 andnot_si128(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = (~a.ULong0 & b.ULong0),
					ULong1 = (~a.ULong1 & b.ULong1)
				};
			}

			// Token: 0x06000D2E RID: 3374 RVA: 0x000135A8 File Offset: 0x000117A8
			[DebuggerStepThrough]
			public static v128 or_si128(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = (a.ULong0 | b.ULong0),
					ULong1 = (a.ULong1 | b.ULong1)
				};
			}

			// Token: 0x06000D2F RID: 3375 RVA: 0x000135E8 File Offset: 0x000117E8
			[DebuggerStepThrough]
			public static v128 xor_si128(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = (a.ULong0 ^ b.ULong0),
					ULong1 = (a.ULong1 ^ b.ULong1)
				};
			}

			// Token: 0x06000D30 RID: 3376 RVA: 0x00013628 File Offset: 0x00011828
			[DebuggerStepThrough]
			public unsafe static v128 cmpeq_epi8(v128 a, v128 b)
			{
				v128 result = default(v128);
				byte* ptr = &a.Byte0;
				byte* ptr2 = &b.Byte0;
				byte* ptr3 = &result.Byte0;
				for (int i = 0; i <= 15; i++)
				{
					ptr3[i] = ((ptr[i] == ptr2[i]) ? byte.MaxValue : 0);
				}
				return result;
			}

			// Token: 0x06000D31 RID: 3377 RVA: 0x00013684 File Offset: 0x00011884
			[DebuggerStepThrough]
			public unsafe static v128 cmpeq_epi16(v128 a, v128 b)
			{
				v128 result = default(v128);
				ushort* ptr = &a.UShort0;
				ushort* ptr2 = &b.UShort0;
				ushort* ptr3 = &result.UShort0;
				for (int i = 0; i <= 7; i++)
				{
					ptr3[i] = ((ptr[i] == ptr2[i]) ? ushort.MaxValue : 0);
				}
				return result;
			}

			// Token: 0x06000D32 RID: 3378 RVA: 0x000136E8 File Offset: 0x000118E8
			[DebuggerStepThrough]
			public unsafe static v128 cmpeq_epi32(v128 a, v128 b)
			{
				v128 result = default(v128);
				uint* ptr = &a.UInt0;
				uint* ptr2 = &b.UInt0;
				uint* ptr3 = &result.UInt0;
				for (int i = 0; i <= 3; i++)
				{
					ptr3[i] = ((ptr[i] == ptr2[i]) ? uint.MaxValue : 0U);
				}
				return result;
			}

			// Token: 0x06000D33 RID: 3379 RVA: 0x00013748 File Offset: 0x00011948
			[DebuggerStepThrough]
			public unsafe static v128 cmpgt_epi8(v128 a, v128 b)
			{
				v128 result = default(v128);
				sbyte* ptr = &a.SByte0;
				sbyte* ptr2 = &b.SByte0;
				sbyte* ptr3 = &result.SByte0;
				for (int i = 0; i <= 15; i++)
				{
					ptr3[i] = ((ptr[i] > ptr2[i]) ? -1 : 0);
				}
				return result;
			}

			// Token: 0x06000D34 RID: 3380 RVA: 0x000137A0 File Offset: 0x000119A0
			[DebuggerStepThrough]
			public unsafe static v128 cmpgt_epi16(v128 a, v128 b)
			{
				v128 result = default(v128);
				short* ptr = &a.SShort0;
				short* ptr2 = &b.SShort0;
				short* ptr3 = &result.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					ptr3[i] = ((ptr[i] > ptr2[i]) ? -1 : 0);
				}
				return result;
			}

			// Token: 0x06000D35 RID: 3381 RVA: 0x00013800 File Offset: 0x00011A00
			[DebuggerStepThrough]
			public unsafe static v128 cmpgt_epi32(v128 a, v128 b)
			{
				v128 result = default(v128);
				int* ptr = &a.SInt0;
				int* ptr2 = &b.SInt0;
				int* ptr3 = &result.SInt0;
				for (int i = 0; i <= 3; i++)
				{
					ptr3[i] = ((ptr[i] > ptr2[i]) ? -1 : 0);
				}
				return result;
			}

			// Token: 0x06000D36 RID: 3382 RVA: 0x0001385F File Offset: 0x00011A5F
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmplt_epi8(v128 a, v128 b)
			{
				return X86.Sse2.cmpgt_epi8(b, a);
			}

			// Token: 0x06000D37 RID: 3383 RVA: 0x00013868 File Offset: 0x00011A68
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmplt_epi16(v128 a, v128 b)
			{
				return X86.Sse2.cmpgt_epi16(b, a);
			}

			// Token: 0x06000D38 RID: 3384 RVA: 0x00013871 File Offset: 0x00011A71
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmplt_epi32(v128 a, v128 b)
			{
				return X86.Sse2.cmpgt_epi32(b, a);
			}

			// Token: 0x06000D39 RID: 3385 RVA: 0x0001387C File Offset: 0x00011A7C
			[DebuggerStepThrough]
			public static v128 cvtepi32_pd(v128 a)
			{
				return new v128
				{
					Double0 = (double)a.SInt0,
					Double1 = (double)a.SInt1
				};
			}

			// Token: 0x06000D3A RID: 3386 RVA: 0x000138B0 File Offset: 0x00011AB0
			[DebuggerStepThrough]
			public static v128 cvtsi32_sd(v128 a, int b)
			{
				v128 result = a;
				result.Double0 = (double)b;
				return result;
			}

			// Token: 0x06000D3B RID: 3387 RVA: 0x000138CC File Offset: 0x00011ACC
			[DebuggerStepThrough]
			public static v128 cvtsi64_sd(v128 a, long b)
			{
				v128 result = a;
				result.Double0 = (double)b;
				return result;
			}

			// Token: 0x06000D3C RID: 3388 RVA: 0x000138E5 File Offset: 0x00011AE5
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cvtsi64x_sd(v128 a, long b)
			{
				return X86.Sse2.cvtsi64_sd(a, b);
			}

			// Token: 0x06000D3D RID: 3389 RVA: 0x000138F0 File Offset: 0x00011AF0
			[DebuggerStepThrough]
			public static v128 cvtepi32_ps(v128 a)
			{
				return new v128
				{
					Float0 = (float)a.SInt0,
					Float1 = (float)a.SInt1,
					Float2 = (float)a.SInt2,
					Float3 = (float)a.SInt3
				};
			}

			// Token: 0x06000D3E RID: 3390 RVA: 0x00013940 File Offset: 0x00011B40
			[DebuggerStepThrough]
			public static v128 cvtsi32_si128(int a)
			{
				return new v128
				{
					SInt0 = a
				};
			}

			// Token: 0x06000D3F RID: 3391 RVA: 0x00013960 File Offset: 0x00011B60
			[DebuggerStepThrough]
			public static v128 cvtsi64_si128(long a)
			{
				return new v128
				{
					SLong0 = a
				};
			}

			// Token: 0x06000D40 RID: 3392 RVA: 0x0001397E File Offset: 0x00011B7E
			[DebuggerStepThrough]
			public static v128 cvtsi64x_si128(long a)
			{
				return X86.Sse2.cvtsi64_si128(a);
			}

			// Token: 0x06000D41 RID: 3393 RVA: 0x00013986 File Offset: 0x00011B86
			[DebuggerStepThrough]
			public static int cvtsi128_si32(v128 a)
			{
				return a.SInt0;
			}

			// Token: 0x06000D42 RID: 3394 RVA: 0x0001398E File Offset: 0x00011B8E
			[DebuggerStepThrough]
			public static long cvtsi128_si64(v128 a)
			{
				return a.SLong0;
			}

			// Token: 0x06000D43 RID: 3395 RVA: 0x00013996 File Offset: 0x00011B96
			[DebuggerStepThrough]
			public static long cvtsi128_si64x(v128 a)
			{
				return a.SLong0;
			}

			// Token: 0x06000D44 RID: 3396 RVA: 0x000139A0 File Offset: 0x00011BA0
			[DebuggerStepThrough]
			public static v128 set_epi64x(long e1, long e0)
			{
				return new v128
				{
					SLong0 = e0,
					SLong1 = e1
				};
			}

			// Token: 0x06000D45 RID: 3397 RVA: 0x000139C8 File Offset: 0x00011BC8
			[DebuggerStepThrough]
			public static v128 set_epi32(int e3, int e2, int e1, int e0)
			{
				return new v128
				{
					SInt0 = e0,
					SInt1 = e1,
					SInt2 = e2,
					SInt3 = e3
				};
			}

			// Token: 0x06000D46 RID: 3398 RVA: 0x00013A00 File Offset: 0x00011C00
			[DebuggerStepThrough]
			public static v128 set_epi16(short e7, short e6, short e5, short e4, short e3, short e2, short e1, short e0)
			{
				return new v128
				{
					SShort0 = e0,
					SShort1 = e1,
					SShort2 = e2,
					SShort3 = e3,
					SShort4 = e4,
					SShort5 = e5,
					SShort6 = e6,
					SShort7 = e7
				};
			}

			// Token: 0x06000D47 RID: 3399 RVA: 0x00013A5C File Offset: 0x00011C5C
			[DebuggerStepThrough]
			public static v128 set_epi8(sbyte e15_, sbyte e14_, sbyte e13_, sbyte e12_, sbyte e11_, sbyte e10_, sbyte e9_, sbyte e8_, sbyte e7_, sbyte e6_, sbyte e5_, sbyte e4_, sbyte e3_, sbyte e2_, sbyte e1_, sbyte e0_)
			{
				return new v128
				{
					SByte0 = e0_,
					SByte1 = e1_,
					SByte2 = e2_,
					SByte3 = e3_,
					SByte4 = e4_,
					SByte5 = e5_,
					SByte6 = e6_,
					SByte7 = e7_,
					SByte8 = e8_,
					SByte9 = e9_,
					SByte10 = e10_,
					SByte11 = e11_,
					SByte12 = e12_,
					SByte13 = e13_,
					SByte14 = e14_,
					SByte15 = e15_
				};
			}

			// Token: 0x06000D48 RID: 3400 RVA: 0x00013B00 File Offset: 0x00011D00
			[DebuggerStepThrough]
			public static v128 set1_epi64x(long a)
			{
				return new v128
				{
					SLong0 = a,
					SLong1 = a
				};
			}

			// Token: 0x06000D49 RID: 3401 RVA: 0x00013B28 File Offset: 0x00011D28
			[DebuggerStepThrough]
			public static v128 set1_epi32(int a)
			{
				return new v128
				{
					SInt0 = a,
					SInt1 = a,
					SInt2 = a,
					SInt3 = a
				};
			}

			// Token: 0x06000D4A RID: 3402 RVA: 0x00013B60 File Offset: 0x00011D60
			[DebuggerStepThrough]
			public unsafe static v128 set1_epi16(short a)
			{
				v128 result = default(v128);
				short* ptr = &result.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					ptr[i] = a;
				}
				return result;
			}

			// Token: 0x06000D4B RID: 3403 RVA: 0x00013B94 File Offset: 0x00011D94
			[DebuggerStepThrough]
			public unsafe static v128 set1_epi8(sbyte a)
			{
				v128 result = default(v128);
				sbyte* ptr = &result.SByte0;
				for (int i = 0; i <= 15; i++)
				{
					ptr[i] = a;
				}
				return result;
			}

			// Token: 0x06000D4C RID: 3404 RVA: 0x00013BC8 File Offset: 0x00011DC8
			[DebuggerStepThrough]
			public static v128 setr_epi32(int e3, int e2, int e1, int e0)
			{
				return new v128
				{
					SInt0 = e3,
					SInt1 = e2,
					SInt2 = e1,
					SInt3 = e0
				};
			}

			// Token: 0x06000D4D RID: 3405 RVA: 0x00013C00 File Offset: 0x00011E00
			[DebuggerStepThrough]
			public static v128 setr_epi16(short e7, short e6, short e5, short e4, short e3, short e2, short e1, short e0)
			{
				return new v128
				{
					SShort0 = e7,
					SShort1 = e6,
					SShort2 = e5,
					SShort3 = e4,
					SShort4 = e3,
					SShort5 = e2,
					SShort6 = e1,
					SShort7 = e0
				};
			}

			// Token: 0x06000D4E RID: 3406 RVA: 0x00013C5C File Offset: 0x00011E5C
			[DebuggerStepThrough]
			public static v128 setr_epi8(sbyte e15_, sbyte e14_, sbyte e13_, sbyte e12_, sbyte e11_, sbyte e10_, sbyte e9_, sbyte e8_, sbyte e7_, sbyte e6_, sbyte e5_, sbyte e4_, sbyte e3_, sbyte e2_, sbyte e1_, sbyte e0_)
			{
				return new v128
				{
					SByte0 = e15_,
					SByte1 = e14_,
					SByte2 = e13_,
					SByte3 = e12_,
					SByte4 = e11_,
					SByte5 = e10_,
					SByte6 = e9_,
					SByte7 = e8_,
					SByte8 = e7_,
					SByte9 = e6_,
					SByte10 = e5_,
					SByte11 = e4_,
					SByte12 = e3_,
					SByte13 = e2_,
					SByte14 = e1_,
					SByte15 = e0_
				};
			}

			// Token: 0x06000D4F RID: 3407 RVA: 0x00013D00 File Offset: 0x00011F00
			[DebuggerStepThrough]
			public static v128 setzero_si128()
			{
				return default(v128);
			}

			// Token: 0x06000D50 RID: 3408 RVA: 0x00013D18 File Offset: 0x00011F18
			[DebuggerStepThrough]
			public static v128 move_epi64(v128 a)
			{
				return new v128
				{
					ULong0 = a.ULong0,
					ULong1 = 0UL
				};
			}

			// Token: 0x06000D51 RID: 3409 RVA: 0x00013D44 File Offset: 0x00011F44
			[DebuggerStepThrough]
			public unsafe static v128 packs_epi16(v128 a, v128 b)
			{
				v128 result = default(v128);
				short* ptr = &a.SShort0;
				short* ptr2 = &b.SShort0;
				sbyte* ptr3 = &result.SByte0;
				for (int i = 0; i < 8; i++)
				{
					ptr3[i] = X86.Saturate_To_Int8((int)ptr[i]);
				}
				for (int j = 0; j < 8; j++)
				{
					ptr3[j + 8] = X86.Saturate_To_Int8((int)ptr2[j]);
				}
				return result;
			}

			// Token: 0x06000D52 RID: 3410 RVA: 0x00013DBC File Offset: 0x00011FBC
			[DebuggerStepThrough]
			public unsafe static v128 packs_epi32(v128 a, v128 b)
			{
				v128 result = default(v128);
				int* ptr = &a.SInt0;
				int* ptr2 = &b.SInt0;
				short* ptr3 = &result.SShort0;
				for (int i = 0; i < 4; i++)
				{
					ptr3[i] = X86.Saturate_To_Int16(ptr[i]);
				}
				for (int j = 0; j < 4; j++)
				{
					ptr3[j + 4] = X86.Saturate_To_Int16(ptr2[j]);
				}
				return result;
			}

			// Token: 0x06000D53 RID: 3411 RVA: 0x00013E3C File Offset: 0x0001203C
			[DebuggerStepThrough]
			public unsafe static v128 packus_epi16(v128 a, v128 b)
			{
				v128 result = default(v128);
				short* ptr = &a.SShort0;
				short* ptr2 = &b.SShort0;
				byte* ptr3 = &result.Byte0;
				for (int i = 0; i < 8; i++)
				{
					ptr3[i] = X86.Saturate_To_UnsignedInt8((int)ptr[i]);
				}
				for (int j = 0; j < 8; j++)
				{
					ptr3[j + 8] = X86.Saturate_To_UnsignedInt8((int)ptr2[j]);
				}
				return result;
			}

			// Token: 0x06000D54 RID: 3412 RVA: 0x00013EB3 File Offset: 0x000120B3
			[DebuggerStepThrough]
			public unsafe static ushort extract_epi16(v128 a, int imm8)
			{
				return (&a.UShort0)[imm8 & 7];
			}

			// Token: 0x06000D55 RID: 3413 RVA: 0x00013EC8 File Offset: 0x000120C8
			[DebuggerStepThrough]
			public unsafe static v128 insert_epi16(v128 a, int i, int imm8)
			{
				v128 result = a;
				(&result.SShort0)[imm8 & 7] = (short)i;
				return result;
			}

			// Token: 0x06000D56 RID: 3414 RVA: 0x00013EEC File Offset: 0x000120EC
			[DebuggerStepThrough]
			public unsafe static int movemask_epi8(v128 a)
			{
				int num = 0;
				byte* ptr = &a.Byte0;
				for (int i = 0; i <= 15; i++)
				{
					if ((ptr[i] & 128) != 0)
					{
						num |= 1 << i;
					}
				}
				return num;
			}

			// Token: 0x06000D57 RID: 3415 RVA: 0x00013F28 File Offset: 0x00012128
			[DebuggerStepThrough]
			public unsafe static v128 shuffle_epi32(v128 a, int imm8)
			{
				v128 result = default(v128);
				uint* ptr = &result.UInt0;
				uint* ptr2 = &a.UInt0;
				*ptr = ptr2[imm8 & 3];
				ptr[1] = ptr2[imm8 >> 2 & 3];
				ptr[2] = ptr2[imm8 >> 4 & 3];
				ptr[3] = ptr2[imm8 >> 6 & 3];
				return result;
			}

			// Token: 0x06000D58 RID: 3416 RVA: 0x00013F8C File Offset: 0x0001218C
			[DebuggerStepThrough]
			public unsafe static v128 shufflehi_epi16(v128 a, int imm8)
			{
				v128 result = a;
				short* ptr = &result.SShort0;
				short* ptr2 = &a.SShort0;
				ptr[4] = ptr2[4 + (imm8 & 3)];
				ptr[5] = ptr2[4 + (imm8 >> 2 & 3)];
				ptr[6] = ptr2[4 + (imm8 >> 4 & 3)];
				ptr[7] = ptr2[4 + (imm8 >> 6 & 3)];
				return result;
			}

			// Token: 0x06000D59 RID: 3417 RVA: 0x00013FFC File Offset: 0x000121FC
			[DebuggerStepThrough]
			public unsafe static v128 shufflelo_epi16(v128 a, int imm8)
			{
				v128 result = a;
				short* ptr = &result.SShort0;
				short* ptr2 = &a.SShort0;
				*ptr = ptr2[imm8 & 3];
				ptr[1] = ptr2[imm8 >> 2 & 3];
				ptr[2] = ptr2[imm8 >> 4 & 3];
				ptr[3] = ptr2[imm8 >> 6 & 3];
				return result;
			}

			// Token: 0x06000D5A RID: 3418 RVA: 0x0001405C File Offset: 0x0001225C
			[DebuggerStepThrough]
			public unsafe static v128 unpackhi_epi8(v128 a, v128 b)
			{
				v128 result = default(v128);
				byte* ptr = &result.Byte0;
				byte* ptr2 = &a.Byte0;
				byte* ptr3 = &b.Byte0;
				for (int i = 0; i <= 7; i++)
				{
					ptr[2 * i] = ptr2[i + 8];
					ptr[2 * i + 1] = ptr3[i + 8];
				}
				return result;
			}

			// Token: 0x06000D5B RID: 3419 RVA: 0x000140BC File Offset: 0x000122BC
			[DebuggerStepThrough]
			public unsafe static v128 unpackhi_epi16(v128 a, v128 b)
			{
				v128 result = default(v128);
				ushort* ptr = &result.UShort0;
				ushort* ptr2 = &a.UShort0;
				ushort* ptr3 = &b.UShort0;
				for (int i = 0; i <= 3; i++)
				{
					ptr[2 * i] = ptr2[i + 4];
					ptr[2 * i + 1] = ptr3[i + 4];
				}
				return result;
			}

			// Token: 0x06000D5C RID: 3420 RVA: 0x00014128 File Offset: 0x00012328
			[DebuggerStepThrough]
			public static v128 unpackhi_epi32(v128 a, v128 b)
			{
				return new v128
				{
					UInt0 = a.UInt2,
					UInt1 = b.UInt2,
					UInt2 = a.UInt3,
					UInt3 = b.UInt3
				};
			}

			// Token: 0x06000D5D RID: 3421 RVA: 0x00014174 File Offset: 0x00012374
			[DebuggerStepThrough]
			public static v128 unpackhi_epi64(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = a.ULong1,
					ULong1 = b.ULong1
				};
			}

			// Token: 0x06000D5E RID: 3422 RVA: 0x000141A4 File Offset: 0x000123A4
			[DebuggerStepThrough]
			public unsafe static v128 unpacklo_epi8(v128 a, v128 b)
			{
				v128 result = default(v128);
				byte* ptr = &result.Byte0;
				byte* ptr2 = &a.Byte0;
				byte* ptr3 = &b.Byte0;
				for (int i = 0; i <= 7; i++)
				{
					ptr[2 * i] = ptr2[i];
					ptr[2 * i + 1] = ptr3[i];
				}
				return result;
			}

			// Token: 0x06000D5F RID: 3423 RVA: 0x00014200 File Offset: 0x00012400
			[DebuggerStepThrough]
			public unsafe static v128 unpacklo_epi16(v128 a, v128 b)
			{
				v128 result = default(v128);
				ushort* ptr = &result.UShort0;
				ushort* ptr2 = &a.UShort0;
				ushort* ptr3 = &b.UShort0;
				for (int i = 0; i <= 3; i++)
				{
					ptr[2 * i] = ptr2[i];
					ptr[2 * i + 1] = ptr3[i];
				}
				return result;
			}

			// Token: 0x06000D60 RID: 3424 RVA: 0x00014268 File Offset: 0x00012468
			[DebuggerStepThrough]
			public static v128 unpacklo_epi32(v128 a, v128 b)
			{
				return new v128
				{
					UInt0 = a.UInt0,
					UInt1 = b.UInt0,
					UInt2 = a.UInt1,
					UInt3 = b.UInt1
				};
			}

			// Token: 0x06000D61 RID: 3425 RVA: 0x000142B4 File Offset: 0x000124B4
			[DebuggerStepThrough]
			public static v128 unpacklo_epi64(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = a.ULong0,
					ULong1 = b.ULong0
				};
			}

			// Token: 0x06000D62 RID: 3426 RVA: 0x000142E4 File Offset: 0x000124E4
			[DebuggerStepThrough]
			public static v128 add_sd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = a.Double0 + b.Double0,
					Double1 = a.Double1
				};
			}

			// Token: 0x06000D63 RID: 3427 RVA: 0x0001431C File Offset: 0x0001251C
			[DebuggerStepThrough]
			public static v128 add_pd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = a.Double0 + b.Double0,
					Double1 = a.Double1 + b.Double1
				};
			}

			// Token: 0x06000D64 RID: 3428 RVA: 0x0001435C File Offset: 0x0001255C
			[DebuggerStepThrough]
			public static v128 div_sd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = a.Double0 / b.Double0,
					Double1 = a.Double1
				};
			}

			// Token: 0x06000D65 RID: 3429 RVA: 0x00014394 File Offset: 0x00012594
			[DebuggerStepThrough]
			public static v128 div_pd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = a.Double0 / b.Double0,
					Double1 = a.Double1 / b.Double1
				};
			}

			// Token: 0x06000D66 RID: 3430 RVA: 0x000143D4 File Offset: 0x000125D4
			[DebuggerStepThrough]
			public static v128 max_sd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = Math.Max(a.Double0, b.Double0),
					Double1 = a.Double1
				};
			}

			// Token: 0x06000D67 RID: 3431 RVA: 0x00014410 File Offset: 0x00012610
			[DebuggerStepThrough]
			public static v128 max_pd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = Math.Max(a.Double0, b.Double0),
					Double1 = Math.Max(a.Double1, b.Double1)
				};
			}

			// Token: 0x06000D68 RID: 3432 RVA: 0x00014458 File Offset: 0x00012658
			[DebuggerStepThrough]
			public static v128 min_sd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = Math.Min(a.Double0, b.Double0),
					Double1 = a.Double1
				};
			}

			// Token: 0x06000D69 RID: 3433 RVA: 0x00014494 File Offset: 0x00012694
			[DebuggerStepThrough]
			public static v128 min_pd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = Math.Min(a.Double0, b.Double0),
					Double1 = Math.Min(a.Double1, b.Double1)
				};
			}

			// Token: 0x06000D6A RID: 3434 RVA: 0x000144DC File Offset: 0x000126DC
			[DebuggerStepThrough]
			public static v128 mul_sd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = a.Double0 * b.Double0,
					Double1 = a.Double1
				};
			}

			// Token: 0x06000D6B RID: 3435 RVA: 0x00014514 File Offset: 0x00012714
			[DebuggerStepThrough]
			public static v128 mul_pd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = a.Double0 * b.Double0,
					Double1 = a.Double1 * b.Double1
				};
			}

			// Token: 0x06000D6C RID: 3436 RVA: 0x00014554 File Offset: 0x00012754
			[DebuggerStepThrough]
			public static v128 sqrt_sd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = Math.Sqrt(b.Double0),
					Double1 = a.Double1
				};
			}

			// Token: 0x06000D6D RID: 3437 RVA: 0x0001458C File Offset: 0x0001278C
			[DebuggerStepThrough]
			public static v128 sqrt_pd(v128 a)
			{
				return new v128
				{
					Double0 = Math.Sqrt(a.Double0),
					Double1 = Math.Sqrt(a.Double1)
				};
			}

			// Token: 0x06000D6E RID: 3438 RVA: 0x000145C8 File Offset: 0x000127C8
			[DebuggerStepThrough]
			public static v128 sub_sd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = a.Double0 - b.Double0,
					Double1 = a.Double1
				};
			}

			// Token: 0x06000D6F RID: 3439 RVA: 0x00014600 File Offset: 0x00012800
			[DebuggerStepThrough]
			public static v128 sub_pd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = a.Double0 - b.Double0,
					Double1 = a.Double1 - b.Double1
				};
			}

			// Token: 0x06000D70 RID: 3440 RVA: 0x00014640 File Offset: 0x00012840
			[DebuggerStepThrough]
			public static v128 and_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = (a.ULong0 & b.ULong0),
					ULong1 = (a.ULong1 & b.ULong1)
				};
			}

			// Token: 0x06000D71 RID: 3441 RVA: 0x00014680 File Offset: 0x00012880
			[DebuggerStepThrough]
			public static v128 andnot_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = (~a.ULong0 & b.ULong0),
					ULong1 = (~a.ULong1 & b.ULong1)
				};
			}

			// Token: 0x06000D72 RID: 3442 RVA: 0x000146C0 File Offset: 0x000128C0
			[DebuggerStepThrough]
			public static v128 or_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = (a.ULong0 | b.ULong0),
					ULong1 = (a.ULong1 | b.ULong1)
				};
			}

			// Token: 0x06000D73 RID: 3443 RVA: 0x00014700 File Offset: 0x00012900
			[DebuggerStepThrough]
			public static v128 xor_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = (a.ULong0 ^ b.ULong0),
					ULong1 = (a.ULong1 ^ b.ULong1)
				};
			}

			// Token: 0x06000D74 RID: 3444 RVA: 0x00014740 File Offset: 0x00012940
			[DebuggerStepThrough]
			public static v128 cmpeq_sd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 == b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = a.ULong1
				};
			}

			// Token: 0x06000D75 RID: 3445 RVA: 0x00014780 File Offset: 0x00012980
			[DebuggerStepThrough]
			public static v128 cmplt_sd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 < b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = a.ULong1
				};
			}

			// Token: 0x06000D76 RID: 3446 RVA: 0x000147C0 File Offset: 0x000129C0
			[DebuggerStepThrough]
			public static v128 cmple_sd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 <= b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = a.ULong1
				};
			}

			// Token: 0x06000D77 RID: 3447 RVA: 0x000147FE File Offset: 0x000129FE
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmpgt_sd(v128 a, v128 b)
			{
				return X86.Sse2.cmple_sd(b, a);
			}

			// Token: 0x06000D78 RID: 3448 RVA: 0x00014807 File Offset: 0x00012A07
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmpge_sd(v128 a, v128 b)
			{
				return X86.Sse2.cmplt_sd(b, a);
			}

			// Token: 0x06000D79 RID: 3449 RVA: 0x00014810 File Offset: 0x00012A10
			[DebuggerStepThrough]
			public static v128 cmpord_sd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((X86.IsNaN(a.ULong0) || X86.IsNaN(b.ULong0)) ? 0UL : ulong.MaxValue),
					ULong1 = a.ULong1
				};
			}

			// Token: 0x06000D7A RID: 3450 RVA: 0x0001485C File Offset: 0x00012A5C
			[DebuggerStepThrough]
			public static v128 cmpunord_sd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((X86.IsNaN(a.ULong0) || X86.IsNaN(b.ULong0)) ? ulong.MaxValue : 0UL),
					ULong1 = a.ULong1
				};
			}

			// Token: 0x06000D7B RID: 3451 RVA: 0x000148A8 File Offset: 0x00012AA8
			[DebuggerStepThrough]
			public static v128 cmpneq_sd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 != b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = a.ULong1
				};
			}

			// Token: 0x06000D7C RID: 3452 RVA: 0x000148E8 File Offset: 0x00012AE8
			[DebuggerStepThrough]
			public static v128 cmpnlt_sd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 >= b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = a.ULong1
				};
			}

			// Token: 0x06000D7D RID: 3453 RVA: 0x00014928 File Offset: 0x00012B28
			[DebuggerStepThrough]
			public static v128 cmpnle_sd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 > b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = a.ULong1
				};
			}

			// Token: 0x06000D7E RID: 3454 RVA: 0x00014966 File Offset: 0x00012B66
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmpngt_sd(v128 a, v128 b)
			{
				return X86.Sse2.cmpnlt_sd(b, a);
			}

			// Token: 0x06000D7F RID: 3455 RVA: 0x0001496F File Offset: 0x00012B6F
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 cmpnge_sd(v128 a, v128 b)
			{
				return X86.Sse2.cmpnle_sd(b, a);
			}

			// Token: 0x06000D80 RID: 3456 RVA: 0x00014978 File Offset: 0x00012B78
			[DebuggerStepThrough]
			public static v128 cmpeq_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 == b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = ((a.Double1 == b.Double1) ? ulong.MaxValue : 0UL)
				};
			}

			// Token: 0x06000D81 RID: 3457 RVA: 0x000149C4 File Offset: 0x00012BC4
			[DebuggerStepThrough]
			public static v128 cmplt_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 < b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = ((a.Double1 < b.Double1) ? ulong.MaxValue : 0UL)
				};
			}

			// Token: 0x06000D82 RID: 3458 RVA: 0x00014A10 File Offset: 0x00012C10
			[DebuggerStepThrough]
			public static v128 cmple_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 <= b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = ((a.Double1 <= b.Double1) ? ulong.MaxValue : 0UL)
				};
			}

			// Token: 0x06000D83 RID: 3459 RVA: 0x00014A5C File Offset: 0x00012C5C
			[DebuggerStepThrough]
			public static v128 cmpgt_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 > b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = ((a.Double1 > b.Double1) ? ulong.MaxValue : 0UL)
				};
			}

			// Token: 0x06000D84 RID: 3460 RVA: 0x00014AA8 File Offset: 0x00012CA8
			[DebuggerStepThrough]
			public static v128 cmpge_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 >= b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = ((a.Double1 >= b.Double1) ? ulong.MaxValue : 0UL)
				};
			}

			// Token: 0x06000D85 RID: 3461 RVA: 0x00014AF4 File Offset: 0x00012CF4
			[DebuggerStepThrough]
			public static v128 cmpord_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((X86.IsNaN(a.ULong0) || X86.IsNaN(b.ULong0)) ? 0UL : ulong.MaxValue),
					ULong1 = ((X86.IsNaN(a.ULong1) || X86.IsNaN(b.ULong1)) ? 0UL : ulong.MaxValue)
				};
			}

			// Token: 0x06000D86 RID: 3462 RVA: 0x00014B58 File Offset: 0x00012D58
			[DebuggerStepThrough]
			public static v128 cmpunord_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((X86.IsNaN(a.ULong0) || X86.IsNaN(b.ULong0)) ? ulong.MaxValue : 0UL),
					ULong1 = ((X86.IsNaN(a.ULong1) || X86.IsNaN(b.ULong1)) ? ulong.MaxValue : 0UL)
				};
			}

			// Token: 0x06000D87 RID: 3463 RVA: 0x00014BBC File Offset: 0x00012DBC
			[DebuggerStepThrough]
			public static v128 cmpneq_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 != b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = ((a.Double1 != b.Double1) ? ulong.MaxValue : 0UL)
				};
			}

			// Token: 0x06000D88 RID: 3464 RVA: 0x00014C08 File Offset: 0x00012E08
			[DebuggerStepThrough]
			public static v128 cmpnlt_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 >= b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = ((a.Double1 >= b.Double1) ? ulong.MaxValue : 0UL)
				};
			}

			// Token: 0x06000D89 RID: 3465 RVA: 0x00014C54 File Offset: 0x00012E54
			[DebuggerStepThrough]
			public static v128 cmpnle_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 > b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = ((a.Double1 > b.Double1) ? ulong.MaxValue : 0UL)
				};
			}

			// Token: 0x06000D8A RID: 3466 RVA: 0x00014CA0 File Offset: 0x00012EA0
			[DebuggerStepThrough]
			public static v128 cmpngt_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 <= b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = ((a.Double1 <= b.Double1) ? ulong.MaxValue : 0UL)
				};
			}

			// Token: 0x06000D8B RID: 3467 RVA: 0x00014CEC File Offset: 0x00012EEC
			[DebuggerStepThrough]
			public static v128 cmpnge_pd(v128 a, v128 b)
			{
				return new v128
				{
					ULong0 = ((a.Double0 < b.Double0) ? ulong.MaxValue : 0UL),
					ULong1 = ((a.Double1 < b.Double1) ? ulong.MaxValue : 0UL)
				};
			}

			// Token: 0x06000D8C RID: 3468 RVA: 0x00014D38 File Offset: 0x00012F38
			[DebuggerStepThrough]
			public static int comieq_sd(v128 a, v128 b)
			{
				if (a.Double0 != b.Double0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000D8D RID: 3469 RVA: 0x00014D4B File Offset: 0x00012F4B
			[DebuggerStepThrough]
			public static int comilt_sd(v128 a, v128 b)
			{
				if (a.Double0 >= b.Double0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000D8E RID: 3470 RVA: 0x00014D5E File Offset: 0x00012F5E
			[DebuggerStepThrough]
			public static int comile_sd(v128 a, v128 b)
			{
				if (a.Double0 > b.Double0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000D8F RID: 3471 RVA: 0x00014D71 File Offset: 0x00012F71
			[DebuggerStepThrough]
			public static int comigt_sd(v128 a, v128 b)
			{
				if (a.Double0 <= b.Double0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000D90 RID: 3472 RVA: 0x00014D84 File Offset: 0x00012F84
			[DebuggerStepThrough]
			public static int comige_sd(v128 a, v128 b)
			{
				if (a.Double0 < b.Double0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000D91 RID: 3473 RVA: 0x00014D97 File Offset: 0x00012F97
			[DebuggerStepThrough]
			public static int comineq_sd(v128 a, v128 b)
			{
				if (a.Double0 == b.Double0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000D92 RID: 3474 RVA: 0x00014DAA File Offset: 0x00012FAA
			[DebuggerStepThrough]
			public static int ucomieq_sd(v128 a, v128 b)
			{
				if (a.Double0 != b.Double0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000D93 RID: 3475 RVA: 0x00014DBD File Offset: 0x00012FBD
			[DebuggerStepThrough]
			public static int ucomilt_sd(v128 a, v128 b)
			{
				if (a.Double0 >= b.Double0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000D94 RID: 3476 RVA: 0x00014DD0 File Offset: 0x00012FD0
			[DebuggerStepThrough]
			public static int ucomile_sd(v128 a, v128 b)
			{
				if (a.Double0 > b.Double0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000D95 RID: 3477 RVA: 0x00014DE3 File Offset: 0x00012FE3
			[DebuggerStepThrough]
			public static int ucomigt_sd(v128 a, v128 b)
			{
				if (a.Double0 <= b.Double0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000D96 RID: 3478 RVA: 0x00014DF6 File Offset: 0x00012FF6
			[DebuggerStepThrough]
			public static int ucomige_sd(v128 a, v128 b)
			{
				if (a.Double0 < b.Double0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000D97 RID: 3479 RVA: 0x00014E09 File Offset: 0x00013009
			[DebuggerStepThrough]
			public static int ucomineq_sd(v128 a, v128 b)
			{
				if (a.Double0 == b.Double0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000D98 RID: 3480 RVA: 0x00014E1C File Offset: 0x0001301C
			[DebuggerStepThrough]
			public static v128 cvtpd_ps(v128 a)
			{
				return new v128
				{
					Float0 = (float)a.Double0,
					Float1 = (float)a.Double1,
					Float2 = 0f,
					Float3 = 0f
				};
			}

			// Token: 0x06000D99 RID: 3481 RVA: 0x00014E68 File Offset: 0x00013068
			[DebuggerStepThrough]
			public static v128 cvtps_pd(v128 a)
			{
				return new v128
				{
					Double0 = (double)a.Float0,
					Double1 = (double)a.Float1
				};
			}

			// Token: 0x06000D9A RID: 3482 RVA: 0x00014E9C File Offset: 0x0001309C
			[DebuggerStepThrough]
			public static v128 cvtpd_epi32(v128 a)
			{
				return new v128
				{
					SInt0 = (int)Math.Round(a.Double0),
					SInt1 = (int)Math.Round(a.Double1)
				};
			}

			// Token: 0x06000D9B RID: 3483 RVA: 0x00014ED8 File Offset: 0x000130D8
			[DebuggerStepThrough]
			public static int cvtsd_si32(v128 a)
			{
				return (int)Math.Round(a.Double0);
			}

			// Token: 0x06000D9C RID: 3484 RVA: 0x00014EE6 File Offset: 0x000130E6
			[DebuggerStepThrough]
			public static long cvtsd_si64(v128 a)
			{
				return (long)Math.Round(a.Double0);
			}

			// Token: 0x06000D9D RID: 3485 RVA: 0x00014EF4 File Offset: 0x000130F4
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static long cvtsd_si64x(v128 a)
			{
				return X86.Sse2.cvtsd_si64(a);
			}

			// Token: 0x06000D9E RID: 3486 RVA: 0x00014EFC File Offset: 0x000130FC
			[DebuggerStepThrough]
			public static v128 cvtsd_ss(v128 a, v128 b)
			{
				v128 result = a;
				result.Float0 = (float)b.Double0;
				return result;
			}

			// Token: 0x06000D9F RID: 3487 RVA: 0x00014F1A File Offset: 0x0001311A
			[DebuggerStepThrough]
			public static double cvtsd_f64(v128 a)
			{
				return a.Double0;
			}

			// Token: 0x06000DA0 RID: 3488 RVA: 0x00014F24 File Offset: 0x00013124
			[DebuggerStepThrough]
			public static v128 cvtss_sd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = (double)b.Float0,
					Double1 = (double)a.Float0
				};
			}

			// Token: 0x06000DA1 RID: 3489 RVA: 0x00014F58 File Offset: 0x00013158
			[DebuggerStepThrough]
			public static v128 cvttpd_epi32(v128 a)
			{
				return new v128
				{
					SInt0 = (int)a.Double0,
					SInt1 = (int)a.Double1
				};
			}

			// Token: 0x06000DA2 RID: 3490 RVA: 0x00014F8A File Offset: 0x0001318A
			[DebuggerStepThrough]
			public static int cvttsd_si32(v128 a)
			{
				return (int)a.Double0;
			}

			// Token: 0x06000DA3 RID: 3491 RVA: 0x00014F93 File Offset: 0x00013193
			[DebuggerStepThrough]
			public static long cvttsd_si64(v128 a)
			{
				return (long)a.Double0;
			}

			// Token: 0x06000DA4 RID: 3492 RVA: 0x00014F9C File Offset: 0x0001319C
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static long cvttsd_si64x(v128 a)
			{
				return X86.Sse2.cvttsd_si64(a);
			}

			// Token: 0x06000DA5 RID: 3493 RVA: 0x00014FA4 File Offset: 0x000131A4
			[DebuggerStepThrough]
			public static v128 cvtps_epi32(v128 a)
			{
				return new v128
				{
					SInt0 = (int)Math.Round((double)a.Float0),
					SInt1 = (int)Math.Round((double)a.Float1),
					SInt2 = (int)Math.Round((double)a.Float2),
					SInt3 = (int)Math.Round((double)a.Float3)
				};
			}

			// Token: 0x06000DA6 RID: 3494 RVA: 0x0001500C File Offset: 0x0001320C
			[DebuggerStepThrough]
			public static v128 cvttps_epi32(v128 a)
			{
				return new v128
				{
					SInt0 = (int)a.Float0,
					SInt1 = (int)a.Float1,
					SInt2 = (int)a.Float2,
					SInt3 = (int)a.Float3
				};
			}

			// Token: 0x06000DA7 RID: 3495 RVA: 0x0001505C File Offset: 0x0001325C
			[DebuggerStepThrough]
			public static v128 set_sd(double a)
			{
				return new v128
				{
					Double0 = a,
					Double1 = 0.0
				};
			}

			// Token: 0x06000DA8 RID: 3496 RVA: 0x0001508C File Offset: 0x0001328C
			[DebuggerStepThrough]
			public static v128 set1_pd(double a)
			{
				return new v128
				{
					Double1 = a,
					Double0 = a
				};
			}

			// Token: 0x06000DA9 RID: 3497 RVA: 0x000150B4 File Offset: 0x000132B4
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public static v128 set_pd1(double a)
			{
				return X86.Sse2.set1_pd(a);
			}

			// Token: 0x06000DAA RID: 3498 RVA: 0x000150BC File Offset: 0x000132BC
			[DebuggerStepThrough]
			public static v128 set_pd(double e1, double e0)
			{
				return new v128
				{
					Double0 = e0,
					Double1 = e1
				};
			}

			// Token: 0x06000DAB RID: 3499 RVA: 0x000150E4 File Offset: 0x000132E4
			[DebuggerStepThrough]
			public static v128 setr_pd(double e1, double e0)
			{
				return new v128
				{
					Double0 = e1,
					Double1 = e0
				};
			}

			// Token: 0x06000DAC RID: 3500 RVA: 0x0001510C File Offset: 0x0001330C
			[DebuggerStepThrough]
			public static v128 unpackhi_pd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = a.Double1,
					Double1 = b.Double1
				};
			}

			// Token: 0x06000DAD RID: 3501 RVA: 0x0001513C File Offset: 0x0001333C
			[DebuggerStepThrough]
			public static v128 unpacklo_pd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = a.Double0,
					Double1 = b.Double0
				};
			}

			// Token: 0x06000DAE RID: 3502 RVA: 0x0001516C File Offset: 0x0001336C
			[DebuggerStepThrough]
			public static int movemask_pd(v128 a)
			{
				int num = 0;
				if ((a.ULong0 & 9223372036854775808UL) != 0UL)
				{
					num |= 1;
				}
				if ((a.ULong1 & 9223372036854775808UL) != 0UL)
				{
					num |= 2;
				}
				return num;
			}

			// Token: 0x06000DAF RID: 3503 RVA: 0x000151A8 File Offset: 0x000133A8
			[DebuggerStepThrough]
			public unsafe static v128 shuffle_pd(v128 a, v128 b, int imm8)
			{
				v128 result = default(v128);
				double* ptr = &a.Double0;
				double* ptr2 = &b.Double0;
				result.Double0 = ptr[imm8 & 1];
				result.Double1 = ptr2[imm8 >> 1 & 1];
				return result;
			}

			// Token: 0x06000DB0 RID: 3504 RVA: 0x000151F4 File Offset: 0x000133F4
			[DebuggerStepThrough]
			public static v128 move_sd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = b.Double0,
					Double1 = a.Double1
				};
			}

			// Token: 0x06000DB1 RID: 3505 RVA: 0x00015224 File Offset: 0x00013424
			public unsafe static v128 loadu_si32(void* mem_addr)
			{
				return new v128(*(int*)mem_addr, 0, 0, 0);
			}

			// Token: 0x06000DB2 RID: 3506 RVA: 0x00015230 File Offset: 0x00013430
			public unsafe static void storeu_si32(void* mem_addr, v128 a)
			{
				*(int*)mem_addr = a.SInt0;
			}

			// Token: 0x06000DB3 RID: 3507 RVA: 0x0001523A File Offset: 0x0001343A
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public unsafe static v128 load_si128(void* ptr)
			{
				return X86.GenericCSharpLoad(ptr);
			}

			// Token: 0x06000DB4 RID: 3508 RVA: 0x00015242 File Offset: 0x00013442
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public unsafe static v128 loadu_si128(void* ptr)
			{
				return X86.GenericCSharpLoad(ptr);
			}

			// Token: 0x06000DB5 RID: 3509 RVA: 0x0001524A File Offset: 0x0001344A
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public unsafe static void store_si128(void* ptr, v128 val)
			{
				X86.GenericCSharpStore(ptr, val);
			}

			// Token: 0x06000DB6 RID: 3510 RVA: 0x00015253 File Offset: 0x00013453
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE2)]
			public unsafe static void storeu_si128(void* ptr, v128 val)
			{
				X86.GenericCSharpStore(ptr, val);
			}

			// Token: 0x06000DB7 RID: 3511 RVA: 0x0001525C File Offset: 0x0001345C
			[DebuggerStepThrough]
			public unsafe static void clflush(void* ptr)
			{
			}
		}

		// Token: 0x0200004A RID: 74
		public static class Sse3
		{
			// Token: 0x1700004A RID: 74
			// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x0001525E File Offset: 0x0001345E
			public static bool IsSse3Supported
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000DB9 RID: 3513 RVA: 0x00015264 File Offset: 0x00013464
			[DebuggerStepThrough]
			public static v128 addsub_ps(v128 a, v128 b)
			{
				return new v128
				{
					Float0 = a.Float0 - b.Float0,
					Float1 = a.Float1 + b.Float1,
					Float2 = a.Float2 - b.Float2,
					Float3 = a.Float3 + b.Float3
				};
			}

			// Token: 0x06000DBA RID: 3514 RVA: 0x000152CC File Offset: 0x000134CC
			[DebuggerStepThrough]
			public static v128 addsub_pd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = a.Double0 - b.Double0,
					Double1 = a.Double1 + b.Double1
				};
			}

			// Token: 0x06000DBB RID: 3515 RVA: 0x0001530C File Offset: 0x0001350C
			[DebuggerStepThrough]
			public static v128 hadd_pd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = a.Double0 + a.Double1,
					Double1 = b.Double0 + b.Double1
				};
			}

			// Token: 0x06000DBC RID: 3516 RVA: 0x0001534C File Offset: 0x0001354C
			[DebuggerStepThrough]
			public static v128 hadd_ps(v128 a, v128 b)
			{
				return new v128
				{
					Float0 = a.Float0 + a.Float1,
					Float1 = a.Float2 + a.Float3,
					Float2 = b.Float0 + b.Float1,
					Float3 = b.Float2 + b.Float3
				};
			}

			// Token: 0x06000DBD RID: 3517 RVA: 0x000153B4 File Offset: 0x000135B4
			[DebuggerStepThrough]
			public static v128 hsub_pd(v128 a, v128 b)
			{
				return new v128
				{
					Double0 = a.Double0 - a.Double1,
					Double1 = b.Double0 - b.Double1
				};
			}

			// Token: 0x06000DBE RID: 3518 RVA: 0x000153F4 File Offset: 0x000135F4
			[DebuggerStepThrough]
			public static v128 hsub_ps(v128 a, v128 b)
			{
				return new v128
				{
					Float0 = a.Float0 - a.Float1,
					Float1 = a.Float2 - a.Float3,
					Float2 = b.Float0 - b.Float1,
					Float3 = b.Float2 - b.Float3
				};
			}

			// Token: 0x06000DBF RID: 3519 RVA: 0x0001545C File Offset: 0x0001365C
			[DebuggerStepThrough]
			public static v128 movedup_pd(v128 a)
			{
				return new v128
				{
					Double0 = a.Double0,
					Double1 = a.Double0
				};
			}

			// Token: 0x06000DC0 RID: 3520 RVA: 0x0001548C File Offset: 0x0001368C
			[DebuggerStepThrough]
			public static v128 movehdup_ps(v128 a)
			{
				return new v128
				{
					Float0 = a.Float1,
					Float1 = a.Float1,
					Float2 = a.Float3,
					Float3 = a.Float3
				};
			}

			// Token: 0x06000DC1 RID: 3521 RVA: 0x000154D8 File Offset: 0x000136D8
			[DebuggerStepThrough]
			public static v128 moveldup_ps(v128 a)
			{
				return new v128
				{
					Float0 = a.Float0,
					Float1 = a.Float0,
					Float2 = a.Float2,
					Float3 = a.Float2
				};
			}
		}

		// Token: 0x0200004B RID: 75
		public static class Sse4_1
		{
			// Token: 0x1700004B RID: 75
			// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x00015522 File Offset: 0x00013722
			public static bool IsSse41Supported
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000DC3 RID: 3523 RVA: 0x00015525 File Offset: 0x00013725
			[DebuggerStepThrough]
			public unsafe static v128 stream_load_si128(void* mem_addr)
			{
				return X86.GenericCSharpLoad(mem_addr);
			}

			// Token: 0x06000DC4 RID: 3524 RVA: 0x00015530 File Offset: 0x00013730
			[DebuggerStepThrough]
			public unsafe static v128 blend_pd(v128 a, v128 b, int imm8)
			{
				v128 result = default(v128);
				double* ptr = &result.Double0;
				double* ptr2 = &a.Double0;
				double* ptr3 = &b.Double0;
				for (int i = 0; i <= 1; i++)
				{
					if ((imm8 & 1 << i) != 0)
					{
						ptr[i] = ptr3[i];
					}
					else
					{
						ptr[i] = ptr2[i];
					}
				}
				return result;
			}

			// Token: 0x06000DC5 RID: 3525 RVA: 0x00015598 File Offset: 0x00013798
			[DebuggerStepThrough]
			public unsafe static v128 blend_ps(v128 a, v128 b, int imm8)
			{
				v128 result = default(v128);
				uint* ptr = &result.UInt0;
				uint* ptr2 = &a.UInt0;
				uint* ptr3 = &b.UInt0;
				for (int i = 0; i <= 3; i++)
				{
					if ((imm8 & 1 << i) != 0)
					{
						ptr[i] = ptr3[i];
					}
					else
					{
						ptr[i] = ptr2[i];
					}
				}
				return result;
			}

			// Token: 0x06000DC6 RID: 3526 RVA: 0x00015600 File Offset: 0x00013800
			[DebuggerStepThrough]
			public unsafe static v128 blendv_pd(v128 a, v128 b, v128 mask)
			{
				v128 result = default(v128);
				double* ptr = &result.Double0;
				double* ptr2 = &a.Double0;
				double* ptr3 = &b.Double0;
				long* ptr4 = &mask.SLong0;
				for (int i = 0; i <= 1; i++)
				{
					if (ptr4[i] < 0L)
					{
						ptr[i] = ptr3[i];
					}
					else
					{
						ptr[i] = ptr2[i];
					}
				}
				return result;
			}

			// Token: 0x06000DC7 RID: 3527 RVA: 0x00015674 File Offset: 0x00013874
			[DebuggerStepThrough]
			public unsafe static v128 blendv_ps(v128 a, v128 b, v128 mask)
			{
				v128 result = default(v128);
				uint* ptr = &result.UInt0;
				uint* ptr2 = &a.UInt0;
				uint* ptr3 = &b.UInt0;
				int* ptr4 = &mask.SInt0;
				for (int i = 0; i <= 3; i++)
				{
					if (ptr4[i] < 0)
					{
						ptr[i] = ptr3[i];
					}
					else
					{
						ptr[i] = ptr2[i];
					}
				}
				return result;
			}

			// Token: 0x06000DC8 RID: 3528 RVA: 0x000156E8 File Offset: 0x000138E8
			[DebuggerStepThrough]
			public unsafe static v128 blendv_epi8(v128 a, v128 b, v128 mask)
			{
				v128 result = default(v128);
				byte* ptr = &result.Byte0;
				byte* ptr2 = &a.Byte0;
				byte* ptr3 = &b.Byte0;
				sbyte* ptr4 = &mask.SByte0;
				for (int i = 0; i <= 15; i++)
				{
					if (ptr4[i] < 0)
					{
						ptr[i] = ptr3[i];
					}
					else
					{
						ptr[i] = ptr2[i];
					}
				}
				return result;
			}

			// Token: 0x06000DC9 RID: 3529 RVA: 0x0001574C File Offset: 0x0001394C
			[DebuggerStepThrough]
			public unsafe static v128 blend_epi16(v128 a, v128 b, int imm8)
			{
				v128 result = default(v128);
				short* ptr = &result.SShort0;
				short* ptr2 = &a.SShort0;
				short* ptr3 = &b.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					if ((imm8 >> i & 1) != 0)
					{
						ptr[i] = ptr3[i];
					}
					else
					{
						ptr[i] = ptr2[i];
					}
				}
				return result;
			}

			// Token: 0x06000DCA RID: 3530 RVA: 0x000157B4 File Offset: 0x000139B4
			[DebuggerStepThrough]
			public static v128 dp_pd(v128 a, v128 b, int imm8)
			{
				double num = ((imm8 & 16) != 0) ? (a.Double0 * b.Double0) : 0.0;
				double num2 = ((imm8 & 32) != 0) ? (a.Double1 * b.Double1) : 0.0;
				double num3 = num + num2;
				return new v128
				{
					Double0 = (((imm8 & 1) != 0) ? num3 : 0.0),
					Double1 = (((imm8 & 2) != 0) ? num3 : 0.0)
				};
			}

			// Token: 0x06000DCB RID: 3531 RVA: 0x0001583C File Offset: 0x00013A3C
			[DebuggerStepThrough]
			public static v128 dp_ps(v128 a, v128 b, int imm8)
			{
				float num = ((imm8 & 16) != 0) ? (a.Float0 * b.Float0) : 0f;
				float num2 = ((imm8 & 32) != 0) ? (a.Float1 * b.Float1) : 0f;
				float num3 = ((imm8 & 64) != 0) ? (a.Float2 * b.Float2) : 0f;
				float num4 = ((imm8 & 128) != 0) ? (a.Float3 * b.Float3) : 0f;
				float num5 = num + num2 + num3 + num4;
				return new v128
				{
					Float0 = (((imm8 & 1) != 0) ? num5 : 0f),
					Float1 = (((imm8 & 2) != 0) ? num5 : 0f),
					Float2 = (((imm8 & 4) != 0) ? num5 : 0f),
					Float3 = (((imm8 & 8) != 0) ? num5 : 0f)
				};
			}

			// Token: 0x06000DCC RID: 3532 RVA: 0x00015918 File Offset: 0x00013B18
			[DebuggerStepThrough]
			public unsafe static int extract_ps(v128 a, int imm8)
			{
				return (&a.SInt0)[imm8 & 3];
			}

			// Token: 0x06000DCD RID: 3533 RVA: 0x0001592A File Offset: 0x00013B2A
			[DebuggerStepThrough]
			public unsafe static float extractf_ps(v128 a, int imm8)
			{
				return (&a.Float0)[imm8 & 3];
			}

			// Token: 0x06000DCE RID: 3534 RVA: 0x0001593C File Offset: 0x00013B3C
			[DebuggerStepThrough]
			public unsafe static byte extract_epi8(v128 a, int imm8)
			{
				return (&a.Byte0)[imm8 & 15];
			}

			// Token: 0x06000DCF RID: 3535 RVA: 0x0001594C File Offset: 0x00013B4C
			[DebuggerStepThrough]
			public unsafe static int extract_epi32(v128 a, int imm8)
			{
				return (&a.SInt0)[imm8 & 3];
			}

			// Token: 0x06000DD0 RID: 3536 RVA: 0x0001595E File Offset: 0x00013B5E
			[DebuggerStepThrough]
			public unsafe static long extract_epi64(v128 a, int imm8)
			{
				return (&a.SLong0)[imm8 & 1];
			}

			// Token: 0x06000DD1 RID: 3537 RVA: 0x00015970 File Offset: 0x00013B70
			[DebuggerStepThrough]
			public unsafe static v128 insert_ps(v128 a, v128 b, int imm8)
			{
				v128 result = a;
				(&result.Float0)[imm8 >> 4 & 3] = (&b.Float0)[imm8 >> 6 & 3];
				for (int i = 0; i < 4; i++)
				{
					if ((imm8 & 1 << i) != 0)
					{
						(&result.Float0)[i] = 0f;
					}
				}
				return result;
			}

			// Token: 0x06000DD2 RID: 3538 RVA: 0x000159D0 File Offset: 0x00013BD0
			[DebuggerStepThrough]
			public unsafe static v128 insert_epi8(v128 a, byte i, int imm8)
			{
				v128 result = a;
				(&result.Byte0)[imm8 & 15] = i;
				return result;
			}

			// Token: 0x06000DD3 RID: 3539 RVA: 0x000159F0 File Offset: 0x00013BF0
			[DebuggerStepThrough]
			public unsafe static v128 insert_epi32(v128 a, int i, int imm8)
			{
				v128 result = a;
				(&result.SInt0)[imm8 & 3] = i;
				return result;
			}

			// Token: 0x06000DD4 RID: 3540 RVA: 0x00015A14 File Offset: 0x00013C14
			[DebuggerStepThrough]
			public unsafe static v128 insert_epi64(v128 a, long i, int imm8)
			{
				v128 result = a;
				(&result.SLong0)[imm8 & 1] = i;
				return result;
			}

			// Token: 0x06000DD5 RID: 3541 RVA: 0x00015A38 File Offset: 0x00013C38
			[DebuggerStepThrough]
			public unsafe static v128 max_epi8(v128 a, v128 b)
			{
				v128 result = default(v128);
				sbyte* ptr = &result.SByte0;
				sbyte* ptr2 = &a.SByte0;
				sbyte* ptr3 = &b.SByte0;
				for (int i = 0; i <= 15; i++)
				{
					ptr[i] = Math.Max(ptr2[i], ptr3[i]);
				}
				return result;
			}

			// Token: 0x06000DD6 RID: 3542 RVA: 0x00015A90 File Offset: 0x00013C90
			[DebuggerStepThrough]
			public unsafe static v128 max_epi32(v128 a, v128 b)
			{
				v128 result = default(v128);
				int* ptr = &result.SInt0;
				int* ptr2 = &a.SInt0;
				int* ptr3 = &b.SInt0;
				for (int i = 0; i <= 3; i++)
				{
					ptr[i] = Math.Max(ptr2[i], ptr3[i]);
				}
				return result;
			}

			// Token: 0x06000DD7 RID: 3543 RVA: 0x00015AF0 File Offset: 0x00013CF0
			[DebuggerStepThrough]
			public unsafe static v128 max_epu32(v128 a, v128 b)
			{
				v128 result = default(v128);
				uint* ptr = &result.UInt0;
				uint* ptr2 = &a.UInt0;
				uint* ptr3 = &b.UInt0;
				for (int i = 0; i <= 3; i++)
				{
					ptr[i] = Math.Max(ptr2[i], ptr3[i]);
				}
				return result;
			}

			// Token: 0x06000DD8 RID: 3544 RVA: 0x00015B50 File Offset: 0x00013D50
			[DebuggerStepThrough]
			public unsafe static v128 max_epu16(v128 a, v128 b)
			{
				v128 result = default(v128);
				ushort* ptr = &result.UShort0;
				ushort* ptr2 = &a.UShort0;
				ushort* ptr3 = &b.UShort0;
				for (int i = 0; i <= 7; i++)
				{
					ptr[i] = Math.Max(ptr2[i], ptr3[i]);
				}
				return result;
			}

			// Token: 0x06000DD9 RID: 3545 RVA: 0x00015BB0 File Offset: 0x00013DB0
			[DebuggerStepThrough]
			public unsafe static v128 min_epi8(v128 a, v128 b)
			{
				v128 result = default(v128);
				sbyte* ptr = &result.SByte0;
				sbyte* ptr2 = &a.SByte0;
				sbyte* ptr3 = &b.SByte0;
				for (int i = 0; i <= 15; i++)
				{
					ptr[i] = Math.Min(ptr2[i], ptr3[i]);
				}
				return result;
			}

			// Token: 0x06000DDA RID: 3546 RVA: 0x00015C08 File Offset: 0x00013E08
			[DebuggerStepThrough]
			public unsafe static v128 min_epi32(v128 a, v128 b)
			{
				v128 result = default(v128);
				int* ptr = &result.SInt0;
				int* ptr2 = &a.SInt0;
				int* ptr3 = &b.SInt0;
				for (int i = 0; i <= 3; i++)
				{
					ptr[i] = Math.Min(ptr2[i], ptr3[i]);
				}
				return result;
			}

			// Token: 0x06000DDB RID: 3547 RVA: 0x00015C68 File Offset: 0x00013E68
			[DebuggerStepThrough]
			public unsafe static v128 min_epu32(v128 a, v128 b)
			{
				v128 result = default(v128);
				uint* ptr = &result.UInt0;
				uint* ptr2 = &a.UInt0;
				uint* ptr3 = &b.UInt0;
				for (int i = 0; i <= 3; i++)
				{
					ptr[i] = Math.Min(ptr2[i], ptr3[i]);
				}
				return result;
			}

			// Token: 0x06000DDC RID: 3548 RVA: 0x00015CC8 File Offset: 0x00013EC8
			[DebuggerStepThrough]
			public unsafe static v128 min_epu16(v128 a, v128 b)
			{
				v128 result = default(v128);
				ushort* ptr = &result.UShort0;
				ushort* ptr2 = &a.UShort0;
				ushort* ptr3 = &b.UShort0;
				for (int i = 0; i <= 7; i++)
				{
					ptr[i] = Math.Min(ptr2[i], ptr3[i]);
				}
				return result;
			}

			// Token: 0x06000DDD RID: 3549 RVA: 0x00015D28 File Offset: 0x00013F28
			[DebuggerStepThrough]
			public static v128 packus_epi32(v128 a, v128 b)
			{
				return new v128
				{
					UShort0 = X86.Saturate_To_UnsignedInt16(a.SInt0),
					UShort1 = X86.Saturate_To_UnsignedInt16(a.SInt1),
					UShort2 = X86.Saturate_To_UnsignedInt16(a.SInt2),
					UShort3 = X86.Saturate_To_UnsignedInt16(a.SInt3),
					UShort4 = X86.Saturate_To_UnsignedInt16(b.SInt0),
					UShort5 = X86.Saturate_To_UnsignedInt16(b.SInt1),
					UShort6 = X86.Saturate_To_UnsignedInt16(b.SInt2),
					UShort7 = X86.Saturate_To_UnsignedInt16(b.SInt3)
				};
			}

			// Token: 0x06000DDE RID: 3550 RVA: 0x00015DD0 File Offset: 0x00013FD0
			[DebuggerStepThrough]
			public static v128 cmpeq_epi64(v128 a, v128 b)
			{
				return new v128
				{
					SLong0 = ((a.SLong0 == b.SLong0) ? -1L : 0L),
					SLong1 = ((a.SLong1 == b.SLong1) ? -1L : 0L)
				};
			}

			// Token: 0x06000DDF RID: 3551 RVA: 0x00015E1C File Offset: 0x0001401C
			[DebuggerStepThrough]
			public unsafe static v128 cvtepi8_epi16(v128 a)
			{
				v128 result = default(v128);
				short* ptr = &result.SShort0;
				sbyte* ptr2 = &a.SByte0;
				for (int i = 0; i <= 7; i++)
				{
					ptr[i] = (short)ptr2[i];
				}
				return result;
			}

			// Token: 0x06000DE0 RID: 3552 RVA: 0x00015E5C File Offset: 0x0001405C
			[DebuggerStepThrough]
			public unsafe static v128 cvtepi8_epi32(v128 a)
			{
				v128 result = default(v128);
				int* ptr = &result.SInt0;
				sbyte* ptr2 = &a.SByte0;
				for (int i = 0; i <= 3; i++)
				{
					ptr[i] = (int)ptr2[i];
				}
				return result;
			}

			// Token: 0x06000DE1 RID: 3553 RVA: 0x00015E9C File Offset: 0x0001409C
			[DebuggerStepThrough]
			public unsafe static v128 cvtepi8_epi64(v128 a)
			{
				v128 result = default(v128);
				long* ptr = &result.SLong0;
				sbyte* ptr2 = &a.SByte0;
				for (int i = 0; i <= 1; i++)
				{
					ptr[i] = (long)ptr2[i];
				}
				return result;
			}

			// Token: 0x06000DE2 RID: 3554 RVA: 0x00015EDC File Offset: 0x000140DC
			[DebuggerStepThrough]
			public unsafe static v128 cvtepi16_epi32(v128 a)
			{
				v128 result = default(v128);
				int* ptr = &result.SInt0;
				short* ptr2 = &a.SShort0;
				for (int i = 0; i <= 3; i++)
				{
					ptr[i] = (int)ptr2[i];
				}
				return result;
			}

			// Token: 0x06000DE3 RID: 3555 RVA: 0x00015F20 File Offset: 0x00014120
			[DebuggerStepThrough]
			public unsafe static v128 cvtepi16_epi64(v128 a)
			{
				v128 result = default(v128);
				long* ptr = &result.SLong0;
				short* ptr2 = &a.SShort0;
				for (int i = 0; i <= 1; i++)
				{
					ptr[i] = (long)ptr2[i];
				}
				return result;
			}

			// Token: 0x06000DE4 RID: 3556 RVA: 0x00015F64 File Offset: 0x00014164
			[DebuggerStepThrough]
			public unsafe static v128 cvtepi32_epi64(v128 a)
			{
				v128 result = default(v128);
				long* ptr = &result.SLong0;
				int* ptr2 = &a.SInt0;
				for (int i = 0; i <= 1; i++)
				{
					ptr[i] = (long)ptr2[i];
				}
				return result;
			}

			// Token: 0x06000DE5 RID: 3557 RVA: 0x00015FA8 File Offset: 0x000141A8
			[DebuggerStepThrough]
			public unsafe static v128 cvtepu8_epi16(v128 a)
			{
				v128 result = default(v128);
				short* ptr = &result.SShort0;
				byte* ptr2 = &a.Byte0;
				for (int i = 0; i <= 7; i++)
				{
					ptr[i] = (short)ptr2[i];
				}
				return result;
			}

			// Token: 0x06000DE6 RID: 3558 RVA: 0x00015FE8 File Offset: 0x000141E8
			[DebuggerStepThrough]
			public unsafe static v128 cvtepu8_epi32(v128 a)
			{
				v128 result = default(v128);
				int* ptr = &result.SInt0;
				byte* ptr2 = &a.Byte0;
				for (int i = 0; i <= 3; i++)
				{
					ptr[i] = (int)ptr2[i];
				}
				return result;
			}

			// Token: 0x06000DE7 RID: 3559 RVA: 0x00016028 File Offset: 0x00014228
			[DebuggerStepThrough]
			public unsafe static v128 cvtepu8_epi64(v128 a)
			{
				v128 result = default(v128);
				long* ptr = &result.SLong0;
				byte* ptr2 = &a.Byte0;
				for (int i = 0; i <= 1; i++)
				{
					ptr[i] = (long)((ulong)ptr2[i]);
				}
				return result;
			}

			// Token: 0x06000DE8 RID: 3560 RVA: 0x00016068 File Offset: 0x00014268
			[DebuggerStepThrough]
			public unsafe static v128 cvtepu16_epi32(v128 a)
			{
				v128 result = default(v128);
				int* ptr = &result.SInt0;
				ushort* ptr2 = &a.UShort0;
				for (int i = 0; i <= 3; i++)
				{
					ptr[i] = (int)ptr2[i];
				}
				return result;
			}

			// Token: 0x06000DE9 RID: 3561 RVA: 0x000160AC File Offset: 0x000142AC
			[DebuggerStepThrough]
			public unsafe static v128 cvtepu16_epi64(v128 a)
			{
				v128 result = default(v128);
				long* ptr = &result.SLong0;
				ushort* ptr2 = &a.UShort0;
				for (int i = 0; i <= 1; i++)
				{
					ptr[i] = (long)((ulong)ptr2[i]);
				}
				return result;
			}

			// Token: 0x06000DEA RID: 3562 RVA: 0x000160F0 File Offset: 0x000142F0
			[DebuggerStepThrough]
			public unsafe static v128 cvtepu32_epi64(v128 a)
			{
				v128 result = default(v128);
				long* ptr = &result.SLong0;
				uint* ptr2 = &a.UInt0;
				for (int i = 0; i <= 1; i++)
				{
					ptr[i] = (long)((ulong)ptr2[i]);
				}
				return result;
			}

			// Token: 0x06000DEB RID: 3563 RVA: 0x00016134 File Offset: 0x00014334
			[DebuggerStepThrough]
			public static v128 mul_epi32(v128 a, v128 b)
			{
				return new v128
				{
					SLong0 = (long)a.SInt0 * (long)b.SInt0,
					SLong1 = (long)a.SInt2 * (long)b.SInt2
				};
			}

			// Token: 0x06000DEC RID: 3564 RVA: 0x00016178 File Offset: 0x00014378
			[DebuggerStepThrough]
			public unsafe static v128 mullo_epi32(v128 a, v128 b)
			{
				v128 result = default(v128);
				int* ptr = &result.SInt0;
				int* ptr2 = &a.SInt0;
				int* ptr3 = &b.SInt0;
				for (int i = 0; i <= 3; i++)
				{
					ptr[i] = ptr2[i] * ptr3[i];
				}
				return result;
			}

			// Token: 0x06000DED RID: 3565 RVA: 0x000161D2 File Offset: 0x000143D2
			[DebuggerStepThrough]
			public static int testz_si128(v128 a, v128 b)
			{
				if ((a.SLong0 & b.SLong0) != 0L || (a.SLong1 & b.SLong1) != 0L)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000DEE RID: 3566 RVA: 0x000161F5 File Offset: 0x000143F5
			[DebuggerStepThrough]
			public static int testc_si128(v128 a, v128 b)
			{
				if ((~(a.SLong0 != 0L) & b.SLong0) != 0L || (~(a.SLong1 != 0L) & b.SLong1) != 0L)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000DEF RID: 3567 RVA: 0x0001621C File Offset: 0x0001441C
			[DebuggerStepThrough]
			public static int testnzc_si128(v128 a, v128 b)
			{
				int num = ((a.SLong0 & b.SLong0) == 0L && (a.SLong1 & b.SLong1) == 0L) ? 1 : 0;
				int num2 = ((~(a.SLong0 != 0L) & b.SLong0) == 0L && (~(a.SLong1 != 0L) & b.SLong1) == 0L) ? 1 : 0;
				return 1 - (num | num2);
			}

			// Token: 0x06000DF0 RID: 3568 RVA: 0x00016276 File Offset: 0x00014476
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE4)]
			public static int test_all_zeros(v128 a, v128 mask)
			{
				return X86.Sse4_1.testz_si128(a, mask);
			}

			// Token: 0x06000DF1 RID: 3569 RVA: 0x0001627F File Offset: 0x0001447F
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE4)]
			public static int test_mix_ones_zeroes(v128 a, v128 mask)
			{
				return X86.Sse4_1.testnzc_si128(a, mask);
			}

			// Token: 0x06000DF2 RID: 3570 RVA: 0x00016288 File Offset: 0x00014488
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE4)]
			public static int test_all_ones(v128 a)
			{
				return X86.Sse4_1.testc_si128(a, X86.Sse2.cmpeq_epi32(a, a));
			}

			// Token: 0x06000DF3 RID: 3571 RVA: 0x00016298 File Offset: 0x00014498
			private static double RoundDImpl(double d, int roundingMode)
			{
				switch (roundingMode & 7)
				{
				case 0:
					return Math.Round(d);
				case 1:
					return Math.Floor(d);
				case 2:
				{
					double num = Math.Ceiling(d);
					if (num == 0.0 && d < 0.0)
					{
						return new v128(9223372036854775808UL).Double0;
					}
					return num;
				}
				case 3:
					return Math.Truncate(d);
				default:
				{
					X86.MXCSRBits mxcsrbits = X86.MXCSR & X86.MXCSRBits.RoundingControlMask;
					if (mxcsrbits == X86.MXCSRBits.RoundToNearest)
					{
						return Math.Round(d);
					}
					if (mxcsrbits == X86.MXCSRBits.RoundDown)
					{
						return Math.Floor(d);
					}
					if (mxcsrbits != X86.MXCSRBits.RoundUp)
					{
						return Math.Truncate(d);
					}
					return Math.Ceiling(d);
				}
				}
			}

			// Token: 0x06000DF4 RID: 3572 RVA: 0x00016348 File Offset: 0x00014548
			[DebuggerStepThrough]
			public static v128 round_pd(v128 a, int rounding)
			{
				return new v128
				{
					Double0 = X86.Sse4_1.RoundDImpl(a.Double0, rounding),
					Double1 = X86.Sse4_1.RoundDImpl(a.Double1, rounding)
				};
			}

			// Token: 0x06000DF5 RID: 3573 RVA: 0x00016384 File Offset: 0x00014584
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE4)]
			public static v128 floor_pd(v128 a)
			{
				return X86.Sse4_1.round_pd(a, 1);
			}

			// Token: 0x06000DF6 RID: 3574 RVA: 0x0001638D File Offset: 0x0001458D
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE4)]
			public static v128 ceil_pd(v128 a)
			{
				return X86.Sse4_1.round_pd(a, 2);
			}

			// Token: 0x06000DF7 RID: 3575 RVA: 0x00016398 File Offset: 0x00014598
			[DebuggerStepThrough]
			public static v128 round_ps(v128 a, int rounding)
			{
				return new v128
				{
					Float0 = (float)X86.Sse4_1.RoundDImpl((double)a.Float0, rounding),
					Float1 = (float)X86.Sse4_1.RoundDImpl((double)a.Float1, rounding),
					Float2 = (float)X86.Sse4_1.RoundDImpl((double)a.Float2, rounding),
					Float3 = (float)X86.Sse4_1.RoundDImpl((double)a.Float3, rounding)
				};
			}

			// Token: 0x06000DF8 RID: 3576 RVA: 0x00016402 File Offset: 0x00014602
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE4)]
			public static v128 floor_ps(v128 a)
			{
				return X86.Sse4_1.round_ps(a, 1);
			}

			// Token: 0x06000DF9 RID: 3577 RVA: 0x0001640B File Offset: 0x0001460B
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE4)]
			public static v128 ceil_ps(v128 a)
			{
				return X86.Sse4_1.round_ps(a, 2);
			}

			// Token: 0x06000DFA RID: 3578 RVA: 0x00016414 File Offset: 0x00014614
			[DebuggerStepThrough]
			public static v128 round_sd(v128 a, v128 b, int rounding)
			{
				return new v128
				{
					Double0 = X86.Sse4_1.RoundDImpl(b.Double0, rounding),
					Double1 = a.Double1
				};
			}

			// Token: 0x06000DFB RID: 3579 RVA: 0x0001644A File Offset: 0x0001464A
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE4)]
			public static v128 floor_sd(v128 a, v128 b)
			{
				return X86.Sse4_1.round_sd(a, b, 1);
			}

			// Token: 0x06000DFC RID: 3580 RVA: 0x00016454 File Offset: 0x00014654
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE4)]
			public static v128 ceil_sd(v128 a, v128 b)
			{
				return X86.Sse4_1.round_sd(a, b, 2);
			}

			// Token: 0x06000DFD RID: 3581 RVA: 0x00016460 File Offset: 0x00014660
			[DebuggerStepThrough]
			public static v128 round_ss(v128 a, v128 b, int rounding)
			{
				v128 result = a;
				result.Float0 = (float)X86.Sse4_1.RoundDImpl((double)b.Float0, rounding);
				return result;
			}

			// Token: 0x06000DFE RID: 3582 RVA: 0x00016485 File Offset: 0x00014685
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE4)]
			public static v128 floor_ss(v128 a, v128 b)
			{
				return X86.Sse4_1.round_ss(a, b, 1);
			}

			// Token: 0x06000DFF RID: 3583 RVA: 0x0001648F File Offset: 0x0001468F
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.X64_SSE4)]
			public static v128 ceil_ss(v128 a, v128 b)
			{
				return X86.Sse4_1.round_ss(a, b, 2);
			}

			// Token: 0x06000E00 RID: 3584 RVA: 0x0001649C File Offset: 0x0001469C
			[DebuggerStepThrough]
			public unsafe static v128 minpos_epu16(v128 a)
			{
				int num = 0;
				ushort num2 = a.UShort0;
				ushort* ptr = &a.UShort0;
				for (int i = 1; i <= 7; i++)
				{
					if (ptr[i] < num2)
					{
						num = i;
						num2 = ptr[i];
					}
				}
				return new v128
				{
					UShort0 = num2,
					UShort1 = (ushort)num
				};
			}

			// Token: 0x06000E01 RID: 3585 RVA: 0x000164FC File Offset: 0x000146FC
			[DebuggerStepThrough]
			public unsafe static v128 mpsadbw_epu8(v128 a, v128 b, int imm8)
			{
				v128 result = default(v128);
				ushort* ptr = &result.UShort0;
				byte* ptr2 = &a.Byte0 + (imm8 >> 2 & 1) * 4;
				byte* ptr3 = &b.Byte0 + (imm8 & 3) * 4;
				byte b2 = *ptr3;
				byte b3 = ptr3[1];
				byte b4 = ptr3[2];
				byte b5 = ptr3[3];
				for (int i = 0; i <= 7; i++)
				{
					ptr[i] = (ushort)(Math.Abs((int)(ptr2[i] - b2)) + Math.Abs((int)(ptr2[i + 1] - b3)) + Math.Abs((int)(ptr2[i + 2] - b4)) + Math.Abs((int)(ptr2[i + 3] - b5)));
				}
				return result;
			}

			// Token: 0x06000E02 RID: 3586 RVA: 0x000165A3 File Offset: 0x000147A3
			[DebuggerStepThrough]
			public static int MK_INSERTPS_NDX(int srcField, int dstField, int zeroMask)
			{
				return srcField << 6 | dstField << 4 | zeroMask;
			}
		}

		// Token: 0x0200004C RID: 76
		public static class Sse4_2
		{
			// Token: 0x1700004C RID: 76
			// (get) Token: 0x06000E03 RID: 3587 RVA: 0x000165AE File Offset: 0x000147AE
			public static bool IsSse42Supported
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000E04 RID: 3588 RVA: 0x000165B4 File Offset: 0x000147B4
			private unsafe static v128 cmpistrm_emulation<[IsUnmanaged] T>(T* a, T* b, int len, int imm8, int allOnes, T allOnesT) where T : struct, ValueType, IComparable<T>, IEquatable<T>
			{
				int intRes = X86.Sse4_2.ComputeStrCmpIntRes2<T>(a, X86.Sse4_2.ComputeStringLength<T>(a, len), b, X86.Sse4_2.ComputeStringLength<T>(b, len), len, imm8, allOnes);
				return X86.Sse4_2.ComputeStrmOutput<T>(len, imm8, allOnesT, intRes);
			}

			// Token: 0x06000E05 RID: 3589 RVA: 0x000165E8 File Offset: 0x000147E8
			private unsafe static v128 cmpestrm_emulation<[IsUnmanaged] T>(T* a, int alen, T* b, int blen, int len, int imm8, int allOnes, T allOnesT) where T : struct, ValueType, IComparable<T>, IEquatable<T>
			{
				int intRes = X86.Sse4_2.ComputeStrCmpIntRes2<T>(a, alen, b, blen, len, imm8, allOnes);
				return X86.Sse4_2.ComputeStrmOutput<T>(len, imm8, allOnesT, intRes);
			}

			// Token: 0x06000E06 RID: 3590 RVA: 0x00016614 File Offset: 0x00014814
			private unsafe static v128 ComputeStrmOutput<[IsUnmanaged] T>(int len, int imm8, T allOnesT, int intRes2) where T : struct, ValueType, IComparable<T>, IEquatable<T>
			{
				v128 result = default(v128);
				if ((imm8 & 64) != 0)
				{
					T* ptr = (T*)(&result.Byte0);
					for (int i = 0; i < len; i++)
					{
						if ((intRes2 & 1 << i) != 0)
						{
							ptr[(IntPtr)i * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = allOnesT;
						}
						else
						{
							ptr[(IntPtr)i * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = default(T);
						}
					}
				}
				else
				{
					result.SInt0 = intRes2;
				}
				return result;
			}

			// Token: 0x06000E07 RID: 3591 RVA: 0x00016680 File Offset: 0x00014880
			private unsafe static int cmpistri_emulation<[IsUnmanaged] T>(T* a, T* b, int len, int imm8, int allOnes, T allOnesT) where T : struct, ValueType, IComparable<T>, IEquatable<T>
			{
				int intRes = X86.Sse4_2.ComputeStrCmpIntRes2<T>(a, X86.Sse4_2.ComputeStringLength<T>(a, len), b, X86.Sse4_2.ComputeStringLength<T>(b, len), len, imm8, allOnes);
				return X86.Sse4_2.ComputeStriOutput(len, imm8, intRes);
			}

			// Token: 0x06000E08 RID: 3592 RVA: 0x000166B0 File Offset: 0x000148B0
			private unsafe static int cmpestri_emulation<[IsUnmanaged] T>(T* a, int alen, T* b, int blen, int len, int imm8, int allOnes, T allOnesT) where T : struct, ValueType, IComparable<T>, IEquatable<T>
			{
				int intRes = X86.Sse4_2.ComputeStrCmpIntRes2<T>(a, alen, b, blen, len, imm8, allOnes);
				return X86.Sse4_2.ComputeStriOutput(len, imm8, intRes);
			}

			// Token: 0x06000E09 RID: 3593 RVA: 0x000166D8 File Offset: 0x000148D8
			private static int ComputeStriOutput(int len, int imm8, int intRes2)
			{
				if ((imm8 & 64) == 0)
				{
					for (int i = 0; i < len; i++)
					{
						if ((intRes2 & 1 << i) != 0)
						{
							return i;
						}
					}
				}
				else
				{
					for (int j = len - 1; j >= 0; j--)
					{
						if ((intRes2 & 1 << j) != 0)
						{
							return j;
						}
					}
				}
				return len;
			}

			// Token: 0x06000E0A RID: 3594 RVA: 0x00016720 File Offset: 0x00014920
			private unsafe static int ComputeStringLength<[IsUnmanaged] T>(T* ptr, int max) where T : struct, ValueType, IEquatable<T>
			{
				for (int i = 0; i < max; i++)
				{
					if (EqualityComparer<T>.Default.Equals(ptr[(IntPtr)i * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)], default(T)))
					{
						return i;
					}
				}
				return max;
			}

			// Token: 0x06000E0B RID: 3595 RVA: 0x00016764 File Offset: 0x00014964
			private unsafe static int ComputeStrCmpIntRes2<[IsUnmanaged] T>(T* a, int alen, T* b, int blen, int len, int imm8, int allOnes) where T : struct, ValueType, IComparable<T>, IEquatable<T>
			{
				bool flag = false;
				X86.Sse4_2.StrBoolArray strBoolArray = default(X86.Sse4_2.StrBoolArray);
				bool flag2;
				for (int i = 0; i < len; i++)
				{
					T t = a[(IntPtr)i * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
					if (i == alen)
					{
						flag = true;
					}
					flag2 = false;
					for (int j = 0; j < len; j++)
					{
						T t2 = b[(IntPtr)j * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
						if (j == blen)
						{
							flag2 = true;
						}
						bool val;
						switch (imm8 >> 2 & 3)
						{
						case 0:
							val = EqualityComparer<T>.Default.Equals(t, t2);
							if (!flag && flag2)
							{
								val = false;
							}
							else if (flag && !flag2)
							{
								val = false;
							}
							else if (flag && flag2)
							{
								val = false;
							}
							break;
						case 1:
							if ((i & 1) == 0)
							{
								val = (Comparer<T>.Default.Compare(t2, t) >= 0);
							}
							else
							{
								val = (Comparer<T>.Default.Compare(t2, t) <= 0);
							}
							if (!flag && flag2)
							{
								val = false;
							}
							else if (flag && !flag2)
							{
								val = false;
							}
							else if (flag && flag2)
							{
								val = false;
							}
							break;
						case 2:
							val = EqualityComparer<T>.Default.Equals(t, t2);
							if (!flag && flag2)
							{
								val = false;
							}
							else if (flag && !flag2)
							{
								val = false;
							}
							else if (flag && flag2)
							{
								val = true;
							}
							break;
						default:
							val = EqualityComparer<T>.Default.Equals(t, t2);
							if (!flag && flag2)
							{
								val = false;
							}
							else if (flag && !flag2)
							{
								val = true;
							}
							else if (flag && flag2)
							{
								val = true;
							}
							break;
						}
						strBoolArray.SetBit(i, j, val);
					}
				}
				int num = 0;
				switch (imm8 >> 2 & 3)
				{
				case 0:
					for (int i = 0; i < len; i++)
					{
						for (int j = 0; j < len; j++)
						{
							num |= (strBoolArray.GetBit(j, i) ? 1 : 0) << i;
						}
					}
					break;
				case 1:
					for (int i = 0; i < len; i++)
					{
						for (int j = 0; j < len; j += 2)
						{
							num |= ((strBoolArray.GetBit(j, i) && strBoolArray.GetBit(j + 1, i)) ? 1 : 0) << i;
						}
					}
					break;
				case 2:
					for (int i = 0; i < len; i++)
					{
						num |= (strBoolArray.GetBit(i, i) ? 1 : 0) << i;
					}
					break;
				case 3:
					num = allOnes;
					for (int i = 0; i < len; i++)
					{
						int num2 = i;
						for (int j = 0; j < len - i; j++)
						{
							if (!strBoolArray.GetBit(j, num2))
							{
								num &= ~(1 << i);
							}
							num2++;
						}
					}
					break;
				}
				int num3 = 0;
				flag2 = false;
				for (int i = 0; i < len; i++)
				{
					if ((imm8 & 16) != 0)
					{
						if ((imm8 & 32) != 0)
						{
							if (EqualityComparer<T>.Default.Equals(b[(IntPtr)i * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)], default(T)))
							{
								flag2 = true;
							}
							if (flag2)
							{
								num3 |= (num & 1 << i);
							}
							else
							{
								num3 |= (~num & 1 << i);
							}
						}
						else
						{
							num3 |= (~num & 1 << i);
						}
					}
					else
					{
						num3 |= (num & 1 << i);
					}
				}
				return num3;
			}

			// Token: 0x06000E0C RID: 3596 RVA: 0x00016AA8 File Offset: 0x00014CA8
			[DebuggerStepThrough]
			public unsafe static v128 cmpistrm(v128 a, v128 b, int imm8)
			{
				v128 result;
				if ((imm8 & 1) == 0)
				{
					if ((imm8 & 2) == 0)
					{
						result = X86.Sse4_2.cmpistrm_emulation<byte>(&a.Byte0, &b.Byte0, 16, imm8, 65535, byte.MaxValue);
					}
					else
					{
						result = X86.Sse4_2.cmpistrm_emulation<sbyte>(&a.SByte0, &b.SByte0, 16, imm8, 65535, -1);
					}
				}
				else if ((imm8 & 2) == 0)
				{
					result = X86.Sse4_2.cmpistrm_emulation<ushort>(&a.UShort0, &b.UShort0, 8, imm8, 255, ushort.MaxValue);
				}
				else
				{
					result = X86.Sse4_2.cmpistrm_emulation<short>(&a.SShort0, &b.SShort0, 8, imm8, 255, -1);
				}
				return result;
			}

			// Token: 0x06000E0D RID: 3597 RVA: 0x00016B50 File Offset: 0x00014D50
			[DebuggerStepThrough]
			public unsafe static int cmpistri(v128 a, v128 b, int imm8)
			{
				if ((imm8 & 1) == 0)
				{
					if ((imm8 & 2) == 0)
					{
						return X86.Sse4_2.cmpistri_emulation<byte>(&a.Byte0, &b.Byte0, 16, imm8, 65535, byte.MaxValue);
					}
					return X86.Sse4_2.cmpistri_emulation<sbyte>(&a.SByte0, &b.SByte0, 16, imm8, 65535, -1);
				}
				else
				{
					if ((imm8 & 2) == 0)
					{
						return X86.Sse4_2.cmpistri_emulation<ushort>(&a.UShort0, &b.UShort0, 8, imm8, 255, ushort.MaxValue);
					}
					return X86.Sse4_2.cmpistri_emulation<short>(&a.SShort0, &b.SShort0, 8, imm8, 255, -1);
				}
			}

			// Token: 0x06000E0E RID: 3598 RVA: 0x00016BF0 File Offset: 0x00014DF0
			[DebuggerStepThrough]
			public unsafe static v128 cmpestrm(v128 a, int la, v128 b, int lb, int imm8)
			{
				v128 result;
				if ((imm8 & 1) == 0)
				{
					if ((imm8 & 2) == 0)
					{
						result = X86.Sse4_2.cmpestrm_emulation<byte>(&a.Byte0, la, &b.Byte0, lb, 16, imm8, 65535, byte.MaxValue);
					}
					else
					{
						result = X86.Sse4_2.cmpestrm_emulation<sbyte>(&a.SByte0, la, &b.SByte0, lb, 16, imm8, 65535, -1);
					}
				}
				else if ((imm8 & 2) == 0)
				{
					result = X86.Sse4_2.cmpestrm_emulation<ushort>(&a.UShort0, la, &b.UShort0, lb, 8, imm8, 255, ushort.MaxValue);
				}
				else
				{
					result = X86.Sse4_2.cmpestrm_emulation<short>(&a.SShort0, la, &b.SShort0, lb, 8, imm8, 255, -1);
				}
				return result;
			}

			// Token: 0x06000E0F RID: 3599 RVA: 0x00016CA4 File Offset: 0x00014EA4
			[DebuggerStepThrough]
			public unsafe static int cmpestri(v128 a, int la, v128 b, int lb, int imm8)
			{
				if ((imm8 & 1) == 0)
				{
					if ((imm8 & 2) == 0)
					{
						return X86.Sse4_2.cmpestri_emulation<byte>(&a.Byte0, la, &b.Byte0, lb, 16, imm8, 65535, byte.MaxValue);
					}
					return X86.Sse4_2.cmpestri_emulation<sbyte>(&a.SByte0, la, &b.SByte0, lb, 16, imm8, 65535, -1);
				}
				else
				{
					if ((imm8 & 2) == 0)
					{
						return X86.Sse4_2.cmpestri_emulation<ushort>(&a.UShort0, la, &b.UShort0, lb, 8, imm8, 255, ushort.MaxValue);
					}
					return X86.Sse4_2.cmpestri_emulation<short>(&a.SShort0, la, &b.SShort0, lb, 8, imm8, 255, -1);
				}
			}

			// Token: 0x06000E10 RID: 3600 RVA: 0x00016D50 File Offset: 0x00014F50
			[DebuggerStepThrough]
			public unsafe static int cmpistrz(v128 a, v128 b, int imm8)
			{
				if ((imm8 & 1) == 0)
				{
					if (X86.Sse4_2.ComputeStringLength<byte>(&b.Byte0, 16) >= 16)
					{
						return 0;
					}
					return 1;
				}
				else
				{
					if (X86.Sse4_2.ComputeStringLength<ushort>(&b.UShort0, 8) >= 8)
					{
						return 0;
					}
					return 1;
				}
			}

			// Token: 0x06000E11 RID: 3601 RVA: 0x00016D84 File Offset: 0x00014F84
			[DebuggerStepThrough]
			public static int cmpistrc(v128 a, v128 b, int imm8)
			{
				v128 v = X86.Sse4_2.cmpistrm(a, b, imm8);
				if (v.SInt0 != 0 || v.SInt1 != 0 || v.SInt2 != 0 || v.SInt3 != 0)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x06000E12 RID: 3602 RVA: 0x00016DBD File Offset: 0x00014FBD
			[DebuggerStepThrough]
			public unsafe static int cmpistrs(v128 a, v128 b, int imm8)
			{
				if ((imm8 & 1) == 0)
				{
					if (X86.Sse4_2.ComputeStringLength<byte>(&a.Byte0, 16) >= 16)
					{
						return 0;
					}
					return 1;
				}
				else
				{
					if (X86.Sse4_2.ComputeStringLength<ushort>(&a.UShort0, 8) >= 8)
					{
						return 0;
					}
					return 1;
				}
			}

			// Token: 0x06000E13 RID: 3603 RVA: 0x00016DF0 File Offset: 0x00014FF0
			[DebuggerStepThrough]
			public unsafe static int cmpistro(v128 a, v128 b, int imm8)
			{
				int num;
				if ((imm8 & 1) == 0)
				{
					int alen = X86.Sse4_2.ComputeStringLength<byte>(&a.Byte0, 16);
					int blen = X86.Sse4_2.ComputeStringLength<byte>(&b.Byte0, 16);
					if ((imm8 & 2) == 0)
					{
						num = X86.Sse4_2.ComputeStrCmpIntRes2<byte>(&a.Byte0, alen, &b.Byte0, blen, 16, imm8, 65535);
					}
					else
					{
						num = X86.Sse4_2.ComputeStrCmpIntRes2<sbyte>(&a.SByte0, alen, &b.SByte0, blen, 16, imm8, 65535);
					}
				}
				else
				{
					int alen2 = X86.Sse4_2.ComputeStringLength<ushort>(&a.UShort0, 8);
					int blen2 = X86.Sse4_2.ComputeStringLength<ushort>(&b.UShort0, 8);
					if ((imm8 & 2) == 0)
					{
						num = X86.Sse4_2.ComputeStrCmpIntRes2<ushort>(&a.UShort0, alen2, &b.UShort0, blen2, 8, imm8, 255);
					}
					else
					{
						num = X86.Sse4_2.ComputeStrCmpIntRes2<short>(&a.SShort0, alen2, &b.SShort0, blen2, 8, imm8, 255);
					}
				}
				return num & 1;
			}

			// Token: 0x06000E14 RID: 3604 RVA: 0x00016ED7 File Offset: 0x000150D7
			[DebuggerStepThrough]
			public static int cmpistra(v128 a, v128 b, int imm8)
			{
				return ~X86.Sse4_2.cmpistrc(a, b, imm8) & ~X86.Sse4_2.cmpistrz(a, b, imm8) & 1;
			}

			// Token: 0x06000E15 RID: 3605 RVA: 0x00016EF0 File Offset: 0x000150F0
			[DebuggerStepThrough]
			public static int cmpestrz(v128 a, int la, v128 b, int lb, int imm8)
			{
				int num = ((imm8 & 1) == 1) ? 16 : 8;
				int num2 = 128 / num - 1;
				if (lb > num2)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000E16 RID: 3606 RVA: 0x00016F1C File Offset: 0x0001511C
			[DebuggerStepThrough]
			public unsafe static int cmpestrc(v128 a, int la, v128 b, int lb, int imm8)
			{
				int num;
				if ((imm8 & 1) == 0)
				{
					if ((imm8 & 2) == 0)
					{
						num = X86.Sse4_2.ComputeStrCmpIntRes2<byte>(&a.Byte0, la, &b.Byte0, lb, 16, imm8, 65535);
					}
					else
					{
						num = X86.Sse4_2.ComputeStrCmpIntRes2<sbyte>(&a.SByte0, la, &b.SByte0, lb, 16, imm8, 65535);
					}
				}
				else if ((imm8 & 2) == 0)
				{
					num = X86.Sse4_2.ComputeStrCmpIntRes2<ushort>(&a.UShort0, la, &b.UShort0, lb, 8, imm8, 255);
				}
				else
				{
					num = X86.Sse4_2.ComputeStrCmpIntRes2<short>(&a.SShort0, la, &b.SShort0, lb, 8, imm8, 255);
				}
				if (num == 0)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000E17 RID: 3607 RVA: 0x00016FCC File Offset: 0x000151CC
			[DebuggerStepThrough]
			public static int cmpestrs(v128 a, int la, v128 b, int lb, int imm8)
			{
				int num = ((imm8 & 1) == 1) ? 16 : 8;
				int num2 = 128 / num - 1;
				if (la > num2)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000E18 RID: 3608 RVA: 0x00016FF8 File Offset: 0x000151F8
			[DebuggerStepThrough]
			public unsafe static int cmpestro(v128 a, int la, v128 b, int lb, int imm8)
			{
				int num;
				if ((imm8 & 1) == 0)
				{
					if ((imm8 & 2) == 0)
					{
						num = X86.Sse4_2.ComputeStrCmpIntRes2<byte>(&a.Byte0, la, &b.Byte0, lb, 16, imm8, 65535);
					}
					else
					{
						num = X86.Sse4_2.ComputeStrCmpIntRes2<sbyte>(&a.SByte0, la, &b.SByte0, lb, 16, imm8, 65535);
					}
				}
				else if ((imm8 & 2) == 0)
				{
					num = X86.Sse4_2.ComputeStrCmpIntRes2<ushort>(&a.UShort0, la, &b.UShort0, lb, 8, imm8, 255);
				}
				else
				{
					num = X86.Sse4_2.ComputeStrCmpIntRes2<short>(&a.SShort0, la, &b.SShort0, lb, 8, imm8, 255);
				}
				return num & 1;
			}

			// Token: 0x06000E19 RID: 3609 RVA: 0x000170A2 File Offset: 0x000152A2
			[DebuggerStepThrough]
			public static int cmpestra(v128 a, int la, v128 b, int lb, int imm8)
			{
				return ~X86.Sse4_2.cmpestrc(a, la, b, lb, imm8) & ~X86.Sse4_2.cmpestrz(a, la, b, lb, imm8) & 1;
			}

			// Token: 0x06000E1A RID: 3610 RVA: 0x000170C0 File Offset: 0x000152C0
			[DebuggerStepThrough]
			public static v128 cmpgt_epi64(v128 val1, v128 val2)
			{
				return new v128
				{
					SLong0 = ((val1.SLong0 > val2.SLong0) ? -1L : 0L),
					SLong1 = ((val1.SLong1 > val2.SLong1) ? -1L : 0L)
				};
			}

			// Token: 0x06000E1B RID: 3611 RVA: 0x0001710A File Offset: 0x0001530A
			[DebuggerStepThrough]
			public static uint crc32_u32(uint crc, uint v)
			{
				crc = X86.Sse4_2.crc32_u8(crc, (byte)v);
				v >>= 8;
				crc = X86.Sse4_2.crc32_u8(crc, (byte)v);
				v >>= 8;
				crc = X86.Sse4_2.crc32_u8(crc, (byte)v);
				v >>= 8;
				crc = X86.Sse4_2.crc32_u8(crc, (byte)v);
				return crc;
			}

			// Token: 0x06000E1C RID: 3612 RVA: 0x00017144 File Offset: 0x00015344
			[DebuggerStepThrough]
			public static uint crc32_u8(uint crc, byte v)
			{
				crc = (crc >> 8 ^ X86.Sse4_2.crctab[(int)((crc ^ (uint)v) & 255U)]);
				return crc;
			}

			// Token: 0x06000E1D RID: 3613 RVA: 0x0001715C File Offset: 0x0001535C
			[DebuggerStepThrough]
			public static uint crc32_u16(uint crc, ushort v)
			{
				crc = X86.Sse4_2.crc32_u8(crc, (byte)v);
				v = (ushort)(v >> 8);
				crc = X86.Sse4_2.crc32_u8(crc, (byte)v);
				return crc;
			}

			// Token: 0x06000E1E RID: 3614 RVA: 0x00017179 File Offset: 0x00015379
			[DebuggerStepThrough]
			[Obsolete("Use the ulong version of this intrinsic instead.")]
			public static ulong crc32_u64(ulong crc_ul, long v)
			{
				return X86.Sse4_2.crc32_u64(crc_ul, (ulong)v);
			}

			// Token: 0x06000E1F RID: 3615 RVA: 0x00017184 File Offset: 0x00015384
			[DebuggerStepThrough]
			public static ulong crc32_u64(ulong crc_ul, ulong v)
			{
				uint crc = X86.Sse4_2.crc32_u8((uint)crc_ul, (byte)v);
				v >>= 8;
				uint crc2 = X86.Sse4_2.crc32_u8(crc, (byte)v);
				v >>= 8;
				uint crc3 = X86.Sse4_2.crc32_u8(crc2, (byte)v);
				v >>= 8;
				uint crc4 = X86.Sse4_2.crc32_u8(crc3, (byte)v);
				v >>= 8;
				uint crc5 = X86.Sse4_2.crc32_u8(crc4, (byte)v);
				v >>= 8;
				uint crc6 = X86.Sse4_2.crc32_u8(crc5, (byte)v);
				v >>= 8;
				uint crc7 = X86.Sse4_2.crc32_u8(crc6, (byte)v);
				v >>= 8;
				return (ulong)X86.Sse4_2.crc32_u8(crc7, (byte)v);
			}

			// Token: 0x06000E20 RID: 3616 RVA: 0x000171EF File Offset: 0x000153EF
			// Note: this type is marked as 'beforefieldinit'.
			static Sse4_2()
			{
			}

			// Token: 0x04000291 RID: 657
			private static readonly uint[] crctab = new uint[]
			{
				0U,
				4067132163U,
				3778769143U,
				324072436U,
				3348797215U,
				904991772U,
				648144872U,
				3570033899U,
				2329499855U,
				2024987596U,
				1809983544U,
				2575936315U,
				1296289744U,
				3207089363U,
				2893594407U,
				1578318884U,
				274646895U,
				3795141740U,
				4049975192U,
				51262619U,
				3619967088U,
				632279923U,
				922689671U,
				3298075524U,
				2592579488U,
				1760304291U,
				2075979607U,
				2312596564U,
				1562183871U,
				2943781820U,
				3156637768U,
				1313733451U,
				549293790U,
				3537243613U,
				3246849577U,
				871202090U,
				3878099393U,
				357341890U,
				102525238U,
				4101499445U,
				2858735121U,
				1477399826U,
				1264559846U,
				3107202533U,
				1845379342U,
				2677391885U,
				2361733625U,
				2125378298U,
				820201905U,
				3263744690U,
				3520608582U,
				598981189U,
				4151959214U,
				85089709U,
				373468761U,
				3827903834U,
				3124367742U,
				1213305469U,
				1526817161U,
				2842354314U,
				2107672161U,
				2412447074U,
				2627466902U,
				1861252501U,
				1098587580U,
				3004210879U,
				2688576843U,
				1378610760U,
				2262928035U,
				1955203488U,
				1742404180U,
				2511436119U,
				3416409459U,
				969524848U,
				714683780U,
				3639785095U,
				205050476U,
				4266873199U,
				3976438427U,
				526918040U,
				1361435347U,
				2739821008U,
				2954799652U,
				1114974503U,
				2529119692U,
				1691668175U,
				2005155131U,
				2247081528U,
				3690758684U,
				697762079U,
				986182379U,
				3366744552U,
				476452099U,
				3993867776U,
				4250756596U,
				255256311U,
				1640403810U,
				2477592673U,
				2164122517U,
				1922457750U,
				2791048317U,
				1412925310U,
				1197962378U,
				3037525897U,
				3944729517U,
				427051182U,
				170179418U,
				4165941337U,
				746937522U,
				3740196785U,
				3451792453U,
				1070968646U,
				1905808397U,
				2213795598U,
				2426610938U,
				1657317369U,
				3053634322U,
				1147748369U,
				1463399397U,
				2773627110U,
				4215344322U,
				153784257U,
				444234805U,
				3893493558U,
				1021025245U,
				3467647198U,
				3722505002U,
				797665321U,
				2197175160U,
				1889384571U,
				1674398607U,
				2443626636U,
				1164749927U,
				3070701412U,
				2757221520U,
				1446797203U,
				137323447U,
				4198817972U,
				3910406976U,
				461344835U,
				3484808360U,
				1037989803U,
				781091935U,
				3705997148U,
				2460548119U,
				1623424788U,
				1939049696U,
				2180517859U,
				1429367560U,
				2807687179U,
				3020495871U,
				1180866812U,
				410100952U,
				3927582683U,
				4182430767U,
				186734380U,
				3756733383U,
				763408580U,
				1053836080U,
				3434856499U,
				2722870694U,
				1344288421U,
				1131464017U,
				2971354706U,
				1708204729U,
				2545590714U,
				2229949006U,
				1988219213U,
				680717673U,
				3673779818U,
				3383336350U,
				1002577565U,
				4010310262U,
				493091189U,
				238226049U,
				4233660802U,
				2987750089U,
				1082061258U,
				1395524158U,
				2705686845U,
				1972364758U,
				2279892693U,
				2494862625U,
				1725896226U,
				952904198U,
				3399985413U,
				3656866545U,
				731699698U,
				4283874585U,
				222117402U,
				510512622U,
				3959836397U,
				3280807620U,
				837199303U,
				582374963U,
				3504198960U,
				68661723U,
				4135334616U,
				3844915500U,
				390545967U,
				1230274059U,
				3141532936U,
				2825850620U,
				1510247935U,
				2395924756U,
				2091215383U,
				1878366691U,
				2644384480U,
				3553878443U,
				565732008U,
				854102364U,
				3229815391U,
				340358836U,
				3861050807U,
				4117890627U,
				119113024U,
				1493875044U,
				2875275879U,
				3090270611U,
				1247431312U,
				2660249211U,
				1828433272U,
				2141937292U,
				2378227087U,
				3811616794U,
				291187481U,
				34330861U,
				4032846830U,
				615137029U,
				3603020806U,
				3314634738U,
				939183345U,
				1776939221U,
				2609017814U,
				2295496738U,
				2058945313U,
				2926798794U,
				1545135305U,
				1330124605U,
				3173225534U,
				4084100981U,
				17165430U,
				307568514U,
				3762199681U,
				888469610U,
				3332340585U,
				3587147933U,
				665062302U,
				2042050490U,
				2346497209U,
				2559330125U,
				1793573966U,
				3190661285U,
				1279665062U,
				1595330642U,
				2910671697U
			};

			// Token: 0x02000057 RID: 87
			[Flags]
			public enum SIDD
			{
				// Token: 0x040002B7 RID: 695
				UBYTE_OPS = 0,
				// Token: 0x040002B8 RID: 696
				UWORD_OPS = 1,
				// Token: 0x040002B9 RID: 697
				SBYTE_OPS = 2,
				// Token: 0x040002BA RID: 698
				SWORD_OPS = 3,
				// Token: 0x040002BB RID: 699
				CMP_EQUAL_ANY = 0,
				// Token: 0x040002BC RID: 700
				CMP_RANGES = 4,
				// Token: 0x040002BD RID: 701
				CMP_EQUAL_EACH = 8,
				// Token: 0x040002BE RID: 702
				CMP_EQUAL_ORDERED = 12,
				// Token: 0x040002BF RID: 703
				POSITIVE_POLARITY = 0,
				// Token: 0x040002C0 RID: 704
				NEGATIVE_POLARITY = 16,
				// Token: 0x040002C1 RID: 705
				MASKED_POSITIVE_POLARITY = 32,
				// Token: 0x040002C2 RID: 706
				MASKED_NEGATIVE_POLARITY = 48,
				// Token: 0x040002C3 RID: 707
				LEAST_SIGNIFICANT = 0,
				// Token: 0x040002C4 RID: 708
				MOST_SIGNIFICANT = 64,
				// Token: 0x040002C5 RID: 709
				BIT_MASK = 0,
				// Token: 0x040002C6 RID: 710
				UNIT_MASK = 64
			}

			// Token: 0x02000058 RID: 88
			private struct StrBoolArray
			{
				// Token: 0x06000E36 RID: 3638 RVA: 0x00017918 File Offset: 0x00015B18
				public unsafe void SetBit(int aindex, int bindex, bool val)
				{
					fixed (ushort* ptr = &this.Bits.FixedElementField)
					{
						ushort* ptr2 = ptr;
						if (val)
						{
							ushort* ptr3 = ptr2 + aindex;
							*ptr3 |= (ushort)(1 << bindex);
						}
						else
						{
							ushort* ptr4 = ptr2 + aindex;
							*ptr4 &= (ushort)(~(ushort)(1 << bindex));
						}
					}
				}

				// Token: 0x06000E37 RID: 3639 RVA: 0x00017964 File Offset: 0x00015B64
				public unsafe bool GetBit(int aindex, int bindex)
				{
					fixed (ushort* ptr = &this.Bits.FixedElementField)
					{
						return ((int)ptr[aindex] & 1 << bindex) != 0;
					}
				}

				// Token: 0x040002C7 RID: 711
				[FixedBuffer(typeof(ushort), 16)]
				public X86.Sse4_2.StrBoolArray.<Bits>e__FixedBuffer Bits;

				// Token: 0x02000059 RID: 89
				[CompilerGenerated]
				[UnsafeValueType]
				[StructLayout(LayoutKind.Sequential, Size = 32)]
				public struct <Bits>e__FixedBuffer
				{
					// Token: 0x040002C8 RID: 712
					public ushort FixedElementField;
				}
			}
		}

		// Token: 0x0200004D RID: 77
		public static class Ssse3
		{
			// Token: 0x1700004D RID: 77
			// (get) Token: 0x06000E21 RID: 3617 RVA: 0x0001720B File Offset: 0x0001540B
			public static bool IsSsse3Supported
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000E22 RID: 3618 RVA: 0x00017210 File Offset: 0x00015410
			[DebuggerStepThrough]
			public unsafe static v128 abs_epi8(v128 a)
			{
				v128 result = default(v128);
				byte* ptr = &result.Byte0;
				sbyte* ptr2 = &a.SByte0;
				for (int i = 0; i <= 15; i++)
				{
					ptr[i] = (byte)Math.Abs((int)ptr2[i]);
				}
				return result;
			}

			// Token: 0x06000E23 RID: 3619 RVA: 0x00017254 File Offset: 0x00015454
			[DebuggerStepThrough]
			public unsafe static v128 abs_epi16(v128 a)
			{
				v128 result = default(v128);
				ushort* ptr = &result.UShort0;
				short* ptr2 = &a.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					ptr[i] = (ushort)Math.Abs((int)ptr2[i]);
				}
				return result;
			}

			// Token: 0x06000E24 RID: 3620 RVA: 0x0001729C File Offset: 0x0001549C
			[DebuggerStepThrough]
			public unsafe static v128 abs_epi32(v128 a)
			{
				v128 result = default(v128);
				uint* ptr = &result.UInt0;
				int* ptr2 = &a.SInt0;
				for (int i = 0; i <= 3; i++)
				{
					ptr[i] = (uint)Math.Abs((long)ptr2[i]);
				}
				return result;
			}

			// Token: 0x06000E25 RID: 3621 RVA: 0x000172E8 File Offset: 0x000154E8
			[DebuggerStepThrough]
			public unsafe static v128 shuffle_epi8(v128 a, v128 b)
			{
				v128 result = default(v128);
				byte* ptr = &result.Byte0;
				byte* ptr2 = &a.Byte0;
				byte* ptr3 = &b.Byte0;
				for (int i = 0; i <= 15; i++)
				{
					if ((ptr3[i] & 128) != 0)
					{
						ptr[i] = 0;
					}
					else
					{
						ptr[i] = ptr2[ptr3[i] & 15];
					}
				}
				return result;
			}

			// Token: 0x06000E26 RID: 3622 RVA: 0x00017350 File Offset: 0x00015550
			[DebuggerStepThrough]
			public unsafe static v128 alignr_epi8(v128 a, v128 b, int count)
			{
				v128 result = default(v128);
				byte* ptr = &result.Byte0;
				byte* ptr2 = &a.Byte0 + count;
				byte* ptr3 = &b.Byte0;
				int i;
				for (i = 0; i < 16 - count; i++)
				{
					*(ptr++) = *(ptr2++);
				}
				while (i < 16)
				{
					*(ptr++) = *(ptr3++);
					i++;
				}
				return result;
			}

			// Token: 0x06000E27 RID: 3623 RVA: 0x000173BC File Offset: 0x000155BC
			[DebuggerStepThrough]
			public unsafe static v128 hadd_epi16(v128 a, v128 b)
			{
				v128 result = default(v128);
				short* ptr = &result.SShort0;
				short* ptr2 = &a.SShort0;
				short* ptr3 = &b.SShort0;
				for (int i = 0; i <= 3; i++)
				{
					ptr[i] = ptr2[2 * i + 1] + ptr2[2 * i];
					ptr[i + 4] = ptr3[2 * i + 1] + ptr3[2 * i];
				}
				return result;
			}

			// Token: 0x06000E28 RID: 3624 RVA: 0x00017440 File Offset: 0x00015640
			[DebuggerStepThrough]
			public unsafe static v128 hadds_epi16(v128 a, v128 b)
			{
				v128 result = default(v128);
				short* ptr = &result.SShort0;
				short* ptr2 = &a.SShort0;
				short* ptr3 = &b.SShort0;
				for (int i = 0; i <= 3; i++)
				{
					ptr[i] = X86.Saturate_To_Int16((int)(ptr2[2 * i + 1] + ptr2[2 * i]));
					ptr[i + 4] = X86.Saturate_To_Int16((int)(ptr3[2 * i + 1] + ptr3[2 * i]));
				}
				return result;
			}

			// Token: 0x06000E29 RID: 3625 RVA: 0x000174CC File Offset: 0x000156CC
			[DebuggerStepThrough]
			public static v128 hadd_epi32(v128 a, v128 b)
			{
				return new v128
				{
					SInt0 = a.SInt1 + a.SInt0,
					SInt1 = a.SInt3 + a.SInt2,
					SInt2 = b.SInt1 + b.SInt0,
					SInt3 = b.SInt3 + b.SInt2
				};
			}

			// Token: 0x06000E2A RID: 3626 RVA: 0x00017534 File Offset: 0x00015734
			[DebuggerStepThrough]
			public unsafe static v128 hsub_epi16(v128 a, v128 b)
			{
				v128 result = default(v128);
				short* ptr = &result.SShort0;
				short* ptr2 = &a.SShort0;
				short* ptr3 = &b.SShort0;
				for (int i = 0; i <= 3; i++)
				{
					ptr[i] = ptr2[2 * i] - ptr2[2 * i + 1];
					ptr[i + 4] = ptr3[2 * i] - ptr3[2 * i + 1];
				}
				return result;
			}

			// Token: 0x06000E2B RID: 3627 RVA: 0x000175B8 File Offset: 0x000157B8
			[DebuggerStepThrough]
			public unsafe static v128 hsubs_epi16(v128 a, v128 b)
			{
				v128 result = default(v128);
				short* ptr = &result.SShort0;
				short* ptr2 = &a.SShort0;
				short* ptr3 = &b.SShort0;
				for (int i = 0; i <= 3; i++)
				{
					ptr[i] = X86.Saturate_To_Int16((int)(ptr2[2 * i] - ptr2[2 * i + 1]));
					ptr[i + 4] = X86.Saturate_To_Int16((int)(ptr3[2 * i] - ptr3[2 * i + 1]));
				}
				return result;
			}

			// Token: 0x06000E2C RID: 3628 RVA: 0x00017644 File Offset: 0x00015844
			[DebuggerStepThrough]
			public static v128 hsub_epi32(v128 a, v128 b)
			{
				return new v128
				{
					SInt0 = a.SInt0 - a.SInt1,
					SInt1 = a.SInt2 - a.SInt3,
					SInt2 = b.SInt0 - b.SInt1,
					SInt3 = b.SInt2 - b.SInt3
				};
			}

			// Token: 0x06000E2D RID: 3629 RVA: 0x000176AC File Offset: 0x000158AC
			[DebuggerStepThrough]
			public unsafe static v128 maddubs_epi16(v128 a, v128 b)
			{
				v128 result = default(v128);
				short* ptr = &result.SShort0;
				byte* ptr2 = &a.Byte0;
				sbyte* ptr3 = &b.SByte0;
				for (int i = 0; i <= 7; i++)
				{
					int val = (int)(ptr2[2 * i + 1] * (byte)ptr3[2 * i + 1] + ptr2[2 * i] * (byte)ptr3[2 * i]);
					ptr[i] = X86.Saturate_To_Int16(val);
				}
				return result;
			}

			// Token: 0x06000E2E RID: 3630 RVA: 0x00017724 File Offset: 0x00015924
			[DebuggerStepThrough]
			public unsafe static v128 mulhrs_epi16(v128 a, v128 b)
			{
				v128 result = default(v128);
				short* ptr = &result.SShort0;
				short* ptr2 = &a.SShort0;
				short* ptr3 = &b.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					int num = (int)(ptr2[i] * ptr3[i]);
					num >>= 14;
					num++;
					num >>= 1;
					ptr[i] = (short)num;
				}
				return result;
			}

			// Token: 0x06000E2F RID: 3631 RVA: 0x00017798 File Offset: 0x00015998
			[DebuggerStepThrough]
			public unsafe static v128 sign_epi8(v128 a, v128 b)
			{
				v128 result = default(v128);
				sbyte* ptr = &result.SByte0;
				sbyte* ptr2 = &a.SByte0;
				sbyte* ptr3 = &b.SByte0;
				for (int i = 0; i <= 15; i++)
				{
					if (ptr3[i] < 0)
					{
						ptr[i] = -ptr2[i];
					}
					else if (ptr3[i] == 0)
					{
						ptr[i] = 0;
					}
					else
					{
						ptr[i] = ptr2[i];
					}
				}
				return result;
			}

			// Token: 0x06000E30 RID: 3632 RVA: 0x0001780C File Offset: 0x00015A0C
			[DebuggerStepThrough]
			public unsafe static v128 sign_epi16(v128 a, v128 b)
			{
				v128 result = default(v128);
				short* ptr = &result.SShort0;
				short* ptr2 = &a.SShort0;
				short* ptr3 = &b.SShort0;
				for (int i = 0; i <= 7; i++)
				{
					if (ptr3[i] < 0)
					{
						ptr[i] = -ptr2[i];
					}
					else if (ptr3[i] == 0)
					{
						ptr[i] = 0;
					}
					else
					{
						ptr[i] = ptr2[i];
					}
				}
				return result;
			}

			// Token: 0x06000E31 RID: 3633 RVA: 0x00017894 File Offset: 0x00015A94
			[DebuggerStepThrough]
			public unsafe static v128 sign_epi32(v128 a, v128 b)
			{
				v128 result = default(v128);
				int* ptr = &result.SInt0;
				int* ptr2 = &a.SInt0;
				int* ptr3 = &b.SInt0;
				for (int i = 0; i <= 3; i++)
				{
					if (ptr3[i] < 0)
					{
						ptr[i] = -ptr2[i];
					}
					else if (ptr3[i] == 0)
					{
						ptr[i] = 0;
					}
					else
					{
						ptr[i] = ptr2[i];
					}
				}
				return result;
			}
		}
	}
}
