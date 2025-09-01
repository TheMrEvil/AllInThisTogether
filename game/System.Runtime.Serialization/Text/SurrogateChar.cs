using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Text
{
	// Token: 0x020000A6 RID: 166
	internal struct SurrogateChar
	{
		// Token: 0x060008FA RID: 2298 RVA: 0x0002539C File Offset: 0x0002359C
		public SurrogateChar(int ch)
		{
			if (ch < 65536 || ch > 1114111)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString("Surrogate char '0x{0}' not valid. Surrogate chars range from 0x10000 to 0x10FFFF.", new object[]
				{
					ch.ToString("X", CultureInfo.InvariantCulture)
				}), "ch"));
			}
			this.lowChar = (char)((ch - 65536 & 1023) + 56320);
			this.highChar = (char)((ch - 65536 >> 10 & 1023) + 55296);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00025424 File Offset: 0x00023624
		public SurrogateChar(char lowChar, char highChar)
		{
			if (lowChar < '\udc00' || lowChar > '\udfff')
			{
				string name = "Low surrogate char '0x{0}' not valid. Low surrogate chars range from 0xDC00 to 0xDFFF.";
				object[] array = new object[1];
				int num = 0;
				int num2 = (int)lowChar;
				array[num] = num2.ToString("X", CultureInfo.InvariantCulture);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString(name, array), "lowChar"));
			}
			if (highChar < '\ud800' || highChar > '\udbff')
			{
				string name2 = "High surrogate char '0x{0}' not valid. High surrogate chars range from 0xD800 to 0xDBFF.";
				object[] array2 = new object[1];
				int num3 = 0;
				int num2 = (int)highChar;
				array2[num3] = num2.ToString("X", CultureInfo.InvariantCulture);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString(name2, array2), "highChar"));
			}
			this.lowChar = lowChar;
			this.highChar = highChar;
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x000254CB File Offset: 0x000236CB
		public char LowChar
		{
			get
			{
				return this.lowChar;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x000254D3 File Offset: 0x000236D3
		public char HighChar
		{
			get
			{
				return this.highChar;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x000254DB File Offset: 0x000236DB
		public int Char
		{
			get
			{
				return (int)(this.lowChar - '\udc00') | (int)((int)(this.highChar - '\ud800') << 10) + 65536;
			}
		}

		// Token: 0x040003DE RID: 990
		private char lowChar;

		// Token: 0x040003DF RID: 991
		private char highChar;

		// Token: 0x040003E0 RID: 992
		public const int MinValue = 65536;

		// Token: 0x040003E1 RID: 993
		public const int MaxValue = 1114111;

		// Token: 0x040003E2 RID: 994
		private const char surHighMin = '\ud800';

		// Token: 0x040003E3 RID: 995
		private const char surHighMax = '\udbff';

		// Token: 0x040003E4 RID: 996
		private const char surLowMin = '\udc00';

		// Token: 0x040003E5 RID: 997
		private const char surLowMax = '\udfff';
	}
}
