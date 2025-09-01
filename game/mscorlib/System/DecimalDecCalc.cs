using System;

namespace System
{
	// Token: 0x020001C3 RID: 451
	internal static class DecimalDecCalc
	{
		// Token: 0x06001342 RID: 4930 RVA: 0x0004D98C File Offset: 0x0004BB8C
		private static uint D32DivMod1E9(uint hi32, ref uint lo32)
		{
			ulong num = (ulong)hi32 << 32 | (ulong)lo32;
			lo32 = (uint)(num / 1000000000UL);
			return (uint)(num % 1000000000UL);
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x0004D9B7 File Offset: 0x0004BBB7
		internal static uint DecDivMod1E9(ref MutableDecimal value)
		{
			return DecimalDecCalc.D32DivMod1E9(DecimalDecCalc.D32DivMod1E9(DecimalDecCalc.D32DivMod1E9(0U, ref value.High), ref value.Mid), ref value.Low);
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0004D9DB File Offset: 0x0004BBDB
		internal static void DecAddInt32(ref MutableDecimal value, uint i)
		{
			if (DecimalDecCalc.D32AddCarry(ref value.Low, i) && DecimalDecCalc.D32AddCarry(ref value.Mid, 1U))
			{
				DecimalDecCalc.D32AddCarry(ref value.High, 1U);
			}
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x0004DA08 File Offset: 0x0004BC08
		private static bool D32AddCarry(ref uint value, uint i)
		{
			uint num = value;
			uint num2 = num + i;
			value = num2;
			return num2 < num || num2 < i;
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x0004DA2C File Offset: 0x0004BC2C
		internal static void DecMul10(ref MutableDecimal value)
		{
			MutableDecimal d = value;
			DecimalDecCalc.DecShiftLeft(ref value);
			DecimalDecCalc.DecShiftLeft(ref value);
			DecimalDecCalc.DecAdd(ref value, d);
			DecimalDecCalc.DecShiftLeft(ref value);
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x0004DA5C File Offset: 0x0004BC5C
		private static void DecShiftLeft(ref MutableDecimal value)
		{
			uint num = ((value.Low & 2147483648U) != 0U) ? 1U : 0U;
			uint num2 = ((value.Mid & 2147483648U) != 0U) ? 1U : 0U;
			value.Low <<= 1;
			value.Mid = (value.Mid << 1 | num);
			value.High = (value.High << 1 | num2);
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x0004DAC0 File Offset: 0x0004BCC0
		private static void DecAdd(ref MutableDecimal value, MutableDecimal d)
		{
			if (DecimalDecCalc.D32AddCarry(ref value.Low, d.Low) && DecimalDecCalc.D32AddCarry(ref value.Mid, 1U))
			{
				DecimalDecCalc.D32AddCarry(ref value.High, 1U);
			}
			if (DecimalDecCalc.D32AddCarry(ref value.Mid, d.Mid))
			{
				DecimalDecCalc.D32AddCarry(ref value.High, 1U);
			}
			DecimalDecCalc.D32AddCarry(ref value.High, d.High);
		}
	}
}
