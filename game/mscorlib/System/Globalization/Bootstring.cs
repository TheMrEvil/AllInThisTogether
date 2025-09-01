using System;
using System.Text;

namespace System.Globalization
{
	// Token: 0x020009AB RID: 2475
	internal class Bootstring
	{
		// Token: 0x0600596C RID: 22892 RVA: 0x001327C8 File Offset: 0x001309C8
		public Bootstring(char delimiter, int baseNum, int tmin, int tmax, int skew, int damp, int initialBias, int initialN)
		{
			this.delimiter = delimiter;
			this.base_num = baseNum;
			this.tmin = tmin;
			this.tmax = tmax;
			this.skew = skew;
			this.damp = damp;
			this.initial_bias = initialBias;
			this.initial_n = initialN;
		}

		// Token: 0x0600596D RID: 22893 RVA: 0x00132818 File Offset: 0x00130A18
		public string Encode(string s, int offset)
		{
			int num = this.initial_n;
			int num2 = 0;
			int num3 = this.initial_bias;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] < '\u0080')
				{
					stringBuilder.Append(s[i]);
				}
			}
			int length;
			int j = length = stringBuilder.Length;
			if (length > 0)
			{
				stringBuilder.Append(this.delimiter);
			}
			while (j < s.Length)
			{
				int num4 = int.MaxValue;
				for (int k = 0; k < s.Length; k++)
				{
					if ((int)s[k] >= num && (int)s[k] < num4)
					{
						num4 = (int)s[k];
					}
				}
				checked
				{
					num2 += (num4 - num) * (j + 1);
					num = num4;
					foreach (char c in s)
					{
						if ((int)c < num || c < '\u0080')
						{
							num2++;
						}
						unchecked
						{
							if ((int)c == num)
							{
								int num5 = num2;
								int num6 = this.base_num;
								for (;;)
								{
									int num7 = (num6 <= num3 + this.tmin) ? this.tmin : ((num6 >= num3 + this.tmax) ? this.tmax : (num6 - num3));
									if (num5 < num7)
									{
										break;
									}
									stringBuilder.Append(this.EncodeDigit(num7 + (num5 - num7) % (this.base_num - num7)));
									num5 = (num5 - num7) / (this.base_num - num7);
									num6 += this.base_num;
								}
								stringBuilder.Append(this.EncodeDigit(num5));
								num3 = this.Adapt(num2, j + 1, j == length);
								num2 = 0;
								j++;
							}
						}
					}
				}
				num2++;
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600596E RID: 22894 RVA: 0x001329E7 File Offset: 0x00130BE7
		private char EncodeDigit(int d)
		{
			return (char)((d < 26) ? (d + 97) : (d - 26 + 48));
		}

		// Token: 0x0600596F RID: 22895 RVA: 0x001329FC File Offset: 0x00130BFC
		private int DecodeDigit(char c)
		{
			if (c - '0' < '\n')
			{
				return (int)(c - '\u0016');
			}
			if (c - 'A' < '\u001a')
			{
				return (int)(c - 'A');
			}
			if (c - 'a' >= '\u001a')
			{
				return this.base_num;
			}
			return (int)(c - 'a');
		}

		// Token: 0x06005970 RID: 22896 RVA: 0x00132A2C File Offset: 0x00130C2C
		private int Adapt(int delta, int numPoints, bool firstTime)
		{
			if (firstTime)
			{
				delta /= this.damp;
			}
			else
			{
				delta /= 2;
			}
			delta += delta / numPoints;
			int num = 0;
			while (delta > (this.base_num - this.tmin) * this.tmax / 2)
			{
				delta /= this.base_num - this.tmin;
				num += this.base_num;
			}
			return num + (this.base_num - this.tmin + 1) * delta / (delta + this.skew);
		}

		// Token: 0x06005971 RID: 22897 RVA: 0x00132AA8 File Offset: 0x00130CA8
		public string Decode(string s, int offset)
		{
			int num = this.initial_n;
			int num2 = 0;
			int num3 = this.initial_bias;
			int num4 = 0;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] == this.delimiter)
				{
					num4 = i;
				}
			}
			if (num4 < 0)
			{
				return s;
			}
			stringBuilder.Append(s, 0, num4);
			int j = (num4 > 0) ? (num4 + 1) : 0;
			while (j < s.Length)
			{
				int num5 = num2;
				int num6 = 1;
				int num7 = this.base_num;
				for (;;)
				{
					int num8 = this.DecodeDigit(s[j++]);
					num2 += num8 * num6;
					int num9 = (num7 <= num3 + this.tmin) ? this.tmin : ((num7 >= num3 + this.tmax) ? this.tmax : (num7 - num3));
					if (num8 < num9)
					{
						break;
					}
					num6 *= this.base_num - num9;
					num7 += this.base_num;
				}
				num3 = this.Adapt(num2 - num5, stringBuilder.Length + 1, num5 == 0);
				num += num2 / (stringBuilder.Length + 1);
				num2 %= stringBuilder.Length + 1;
				if (num < 128)
				{
					throw new ArgumentException(string.Format("Invalid Bootstring decode result, at {0}", offset + j));
				}
				stringBuilder.Insert(num2, (char)num);
				num2++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04003759 RID: 14169
		private readonly char delimiter;

		// Token: 0x0400375A RID: 14170
		private readonly int base_num;

		// Token: 0x0400375B RID: 14171
		private readonly int tmin;

		// Token: 0x0400375C RID: 14172
		private readonly int tmax;

		// Token: 0x0400375D RID: 14173
		private readonly int skew;

		// Token: 0x0400375E RID: 14174
		private readonly int damp;

		// Token: 0x0400375F RID: 14175
		private readonly int initial_bias;

		// Token: 0x04003760 RID: 14176
		private readonly int initial_n;
	}
}
