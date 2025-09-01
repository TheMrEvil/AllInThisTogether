using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200010C RID: 268
	[Obsolete("This storage will no longer be used. (RemovedAfter 2021-06-01)")]
	public struct NumberedWords
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x0001DEB4 File Offset: 0x0001C0B4
		// (set) Token: 0x060009EC RID: 2540 RVA: 0x0001DEC1 File Offset: 0x0001C0C1
		private int LeadingZeroes
		{
			get
			{
				return this.Suffix >> 29 & 7;
			}
			set
			{
				this.Suffix &= 536870911;
				this.Suffix |= (value & 7) << 29;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x0001DEE8 File Offset: 0x0001C0E8
		// (set) Token: 0x060009EE RID: 2542 RVA: 0x0001DEF6 File Offset: 0x0001C0F6
		private int PositiveNumericSuffix
		{
			get
			{
				return this.Suffix & 536870911;
			}
			set
			{
				this.Suffix &= -536870912;
				this.Suffix |= (value & 536870911);
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x0001DF1E File Offset: 0x0001C11E
		private bool HasPositiveNumericSuffix
		{
			get
			{
				return this.PositiveNumericSuffix != 0;
			}
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0001DF2C File Offset: 0x0001C12C
		[NotBurstCompatible]
		private string NewString(char c, int count)
		{
			char[] array = new char[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = c;
			}
			return new string(array, 0, count);
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0001DF58 File Offset: 0x0001C158
		[NotBurstCompatible]
		public unsafe int ToFixedString<T>(ref T result) where T : IUTF8Bytes, INativeList<byte>
		{
			int i = this.PositiveNumericSuffix;
			int leadingZeroes = this.LeadingZeroes;
			WordStorage.Instance.GetFixedString<T>(this.Index, ref result);
			if (i == 0 && leadingZeroes == 0)
			{
				return 0;
			}
			byte* ptr = stackalloc byte[(UIntPtr)17];
			int j = 17;
			while (i > 0)
			{
				ptr[--j] = (byte)(48 + i % 10);
				i /= 10;
			}
			while (leadingZeroes-- > 0)
			{
				ptr[--j] = 48;
			}
			byte* ptr2 = result.GetUnsafePtr() + result.Length;
			result.Length += 17 - j;
			while (j < 17)
			{
				*(ptr2++) = ptr[j++];
			}
			return 0;
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0001E014 File Offset: 0x0001C214
		[NotBurstCompatible]
		public override string ToString()
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			this.ToFixedString<FixedString512Bytes>(ref fixedString512Bytes);
			return fixedString512Bytes.ToString();
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0001E03F File Offset: 0x0001C23F
		private bool IsDigit(byte b)
		{
			return b >= 48 && b <= 57;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0001E050 File Offset: 0x0001C250
		[NotBurstCompatible]
		public void SetString<T>(ref T value) where T : IUTF8Bytes, INativeList<byte>
		{
			int num = value.Length;
			while (num > 0 && this.IsDigit(value[num - 1]))
			{
				num--;
			}
			int num2 = num;
			while (num2 < value.Length && value[num2] == 48)
			{
				num2++;
			}
			int num3 = num2 - num;
			if (num3 > 7)
			{
				int num4 = num3 - 7;
				num += num4;
				num3 -= num4;
			}
			this.PositiveNumericSuffix = 0;
			int num5 = 0;
			for (int i = num2; i < value.Length; i++)
			{
				num5 *= 10;
				num5 += (int)(value[i] - 48);
			}
			if (num5 <= 536870911)
			{
				this.PositiveNumericSuffix = num5;
			}
			else
			{
				num = value.Length;
				num3 = 0;
			}
			this.LeadingZeroes = num3;
			T t = value;
			int length = t.Length;
			if (num != t.Length)
			{
				t.Length = num;
			}
			this.Index = WordStorage.Instance.GetOrCreateIndex<T>(ref t);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0001E17C File Offset: 0x0001C37C
		[NotBurstCompatible]
		public void SetString(string value)
		{
			FixedString512Bytes fixedString512Bytes = value;
			this.SetString<FixedString512Bytes>(ref fixedString512Bytes);
		}

		// Token: 0x0400034C RID: 844
		private int Index;

		// Token: 0x0400034D RID: 845
		private int Suffix;

		// Token: 0x0400034E RID: 846
		private const int kPositiveNumericSuffixShift = 0;

		// Token: 0x0400034F RID: 847
		private const int kPositiveNumericSuffixBits = 29;

		// Token: 0x04000350 RID: 848
		private const int kMaxPositiveNumericSuffix = 536870911;

		// Token: 0x04000351 RID: 849
		private const int kPositiveNumericSuffixMask = 536870911;

		// Token: 0x04000352 RID: 850
		private const int kLeadingZeroesShift = 29;

		// Token: 0x04000353 RID: 851
		private const int kLeadingZeroesBits = 3;

		// Token: 0x04000354 RID: 852
		private const int kMaxLeadingZeroes = 7;

		// Token: 0x04000355 RID: 853
		private const int kLeadingZeroesMask = 7;
	}
}
