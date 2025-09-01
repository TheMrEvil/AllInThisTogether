using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Buffers.Text
{
	// Token: 0x02000AFE RID: 2814
	[Obsolete("Types with embedded references are not supported in this version of your compiler.", true)]
	internal ref struct NumberBuffer
	{
		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x0600648C RID: 25740 RVA: 0x00155AB5 File Offset: 0x00153CB5
		public Span<byte> Digits
		{
			get
			{
				return new Span<byte>(Unsafe.AsPointer<byte>(ref this._b0), 51);
			}
		}

		// Token: 0x170011A9 RID: 4521
		// (get) Token: 0x0600648D RID: 25741 RVA: 0x00155AC9 File Offset: 0x00153CC9
		public unsafe byte* UnsafeDigits
		{
			get
			{
				return (byte*)Unsafe.AsPointer<byte>(ref this._b0);
			}
		}

		// Token: 0x170011AA RID: 4522
		// (get) Token: 0x0600648E RID: 25742 RVA: 0x00155AD6 File Offset: 0x00153CD6
		public int NumDigits
		{
			get
			{
				return this.Digits.IndexOf(0);
			}
		}

		// Token: 0x0600648F RID: 25743 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[Conditional("DEBUG")]
		public void CheckConsistency()
		{
		}

		// Token: 0x06006490 RID: 25744 RVA: 0x00155AE4 File Offset: 0x00153CE4
		public unsafe override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			stringBuilder.Append('"');
			Span<byte> digits = this.Digits;
			for (int i = 0; i < 51; i++)
			{
				byte b = *digits[i];
				if (b == 0)
				{
					break;
				}
				stringBuilder.Append((char)b);
			}
			stringBuilder.Append('"');
			stringBuilder.Append(", Scale = " + this.Scale.ToString());
			stringBuilder.Append(", IsNegative   = " + this.IsNegative.ToString());
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x04003AF4 RID: 15092
		public int Scale;

		// Token: 0x04003AF5 RID: 15093
		public bool IsNegative;

		// Token: 0x04003AF6 RID: 15094
		public const int BufferSize = 51;

		// Token: 0x04003AF7 RID: 15095
		private byte _b0;

		// Token: 0x04003AF8 RID: 15096
		private byte _b1;

		// Token: 0x04003AF9 RID: 15097
		private byte _b2;

		// Token: 0x04003AFA RID: 15098
		private byte _b3;

		// Token: 0x04003AFB RID: 15099
		private byte _b4;

		// Token: 0x04003AFC RID: 15100
		private byte _b5;

		// Token: 0x04003AFD RID: 15101
		private byte _b6;

		// Token: 0x04003AFE RID: 15102
		private byte _b7;

		// Token: 0x04003AFF RID: 15103
		private byte _b8;

		// Token: 0x04003B00 RID: 15104
		private byte _b9;

		// Token: 0x04003B01 RID: 15105
		private byte _b10;

		// Token: 0x04003B02 RID: 15106
		private byte _b11;

		// Token: 0x04003B03 RID: 15107
		private byte _b12;

		// Token: 0x04003B04 RID: 15108
		private byte _b13;

		// Token: 0x04003B05 RID: 15109
		private byte _b14;

		// Token: 0x04003B06 RID: 15110
		private byte _b15;

		// Token: 0x04003B07 RID: 15111
		private byte _b16;

		// Token: 0x04003B08 RID: 15112
		private byte _b17;

		// Token: 0x04003B09 RID: 15113
		private byte _b18;

		// Token: 0x04003B0A RID: 15114
		private byte _b19;

		// Token: 0x04003B0B RID: 15115
		private byte _b20;

		// Token: 0x04003B0C RID: 15116
		private byte _b21;

		// Token: 0x04003B0D RID: 15117
		private byte _b22;

		// Token: 0x04003B0E RID: 15118
		private byte _b23;

		// Token: 0x04003B0F RID: 15119
		private byte _b24;

		// Token: 0x04003B10 RID: 15120
		private byte _b25;

		// Token: 0x04003B11 RID: 15121
		private byte _b26;

		// Token: 0x04003B12 RID: 15122
		private byte _b27;

		// Token: 0x04003B13 RID: 15123
		private byte _b28;

		// Token: 0x04003B14 RID: 15124
		private byte _b29;

		// Token: 0x04003B15 RID: 15125
		private byte _b30;

		// Token: 0x04003B16 RID: 15126
		private byte _b31;

		// Token: 0x04003B17 RID: 15127
		private byte _b32;

		// Token: 0x04003B18 RID: 15128
		private byte _b33;

		// Token: 0x04003B19 RID: 15129
		private byte _b34;

		// Token: 0x04003B1A RID: 15130
		private byte _b35;

		// Token: 0x04003B1B RID: 15131
		private byte _b36;

		// Token: 0x04003B1C RID: 15132
		private byte _b37;

		// Token: 0x04003B1D RID: 15133
		private byte _b38;

		// Token: 0x04003B1E RID: 15134
		private byte _b39;

		// Token: 0x04003B1F RID: 15135
		private byte _b40;

		// Token: 0x04003B20 RID: 15136
		private byte _b41;

		// Token: 0x04003B21 RID: 15137
		private byte _b42;

		// Token: 0x04003B22 RID: 15138
		private byte _b43;

		// Token: 0x04003B23 RID: 15139
		private byte _b44;

		// Token: 0x04003B24 RID: 15140
		private byte _b45;

		// Token: 0x04003B25 RID: 15141
		private byte _b46;

		// Token: 0x04003B26 RID: 15142
		private byte _b47;

		// Token: 0x04003B27 RID: 15143
		private byte _b48;

		// Token: 0x04003B28 RID: 15144
		private byte _b49;

		// Token: 0x04003B29 RID: 15145
		private byte _b50;
	}
}
