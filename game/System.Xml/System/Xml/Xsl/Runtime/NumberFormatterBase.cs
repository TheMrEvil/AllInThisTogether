using System;
using System.Text;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200043F RID: 1087
	internal class NumberFormatterBase
	{
		// Token: 0x06002AF4 RID: 10996 RVA: 0x00102F84 File Offset: 0x00101184
		public static void ConvertToAlphabetic(StringBuilder sb, double val, char firstChar, int totalChars)
		{
			char[] array = new char[7];
			int num = 7;
			int i;
			int num2;
			for (i = (int)val; i > totalChars; i = num2)
			{
				num2 = --i / totalChars;
				array[--num] = (char)((int)firstChar + (i - num2 * totalChars));
			}
			array[--num] = (char)((int)firstChar + (i - 1));
			sb.Append(array, num, 7 - num);
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x00102FD8 File Offset: 0x001011D8
		public static void ConvertToRoman(StringBuilder sb, double val, bool upperCase)
		{
			int i = (int)val;
			string value = upperCase ? "IIVIXXLXCCDCM" : "iivixxlxccdcm";
			int num = NumberFormatterBase.RomanDigitValue.Length;
			while (num-- != 0)
			{
				while (i >= NumberFormatterBase.RomanDigitValue[num])
				{
					i -= NumberFormatterBase.RomanDigitValue[num];
					sb.Append(value, num, 1 + (num & 1));
				}
			}
		}

		// Token: 0x06002AF6 RID: 10998 RVA: 0x0000216B File Offset: 0x0000036B
		public NumberFormatterBase()
		{
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x0010302B File Offset: 0x0010122B
		// Note: this type is marked as 'beforefieldinit'.
		static NumberFormatterBase()
		{
		}

		// Token: 0x040021FC RID: 8700
		protected const int MaxAlphabeticValue = 2147483647;

		// Token: 0x040021FD RID: 8701
		private const int MaxAlphabeticLength = 7;

		// Token: 0x040021FE RID: 8702
		protected const int MaxRomanValue = 32767;

		// Token: 0x040021FF RID: 8703
		private const string RomanDigitsUC = "IIVIXXLXCCDCM";

		// Token: 0x04002200 RID: 8704
		private const string RomanDigitsLC = "iivixxlxccdcm";

		// Token: 0x04002201 RID: 8705
		private static readonly int[] RomanDigitValue = new int[]
		{
			1,
			4,
			5,
			9,
			10,
			40,
			50,
			90,
			100,
			400,
			500,
			900,
			1000
		};

		// Token: 0x04002202 RID: 8706
		private const string hiraganaAiueo = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらりるれろわをん";

		// Token: 0x04002203 RID: 8707
		private const string hiraganaIroha = "いろはにほへとちりぬるをわかよたれそつねならむうゐのおくやまけふこえてあさきゆめみしゑひもせす";

		// Token: 0x04002204 RID: 8708
		private const string katakanaAiueo = "アイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワヲン";

		// Token: 0x04002205 RID: 8709
		private const string katakanaIroha = "イロハニホヘトチリヌルヲワカヨタレソツネナラムウヰノオクヤマケフコエテアサキユメミシヱヒモセスン";

		// Token: 0x04002206 RID: 8710
		private const string katakanaAiueoHw = "ｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜｦﾝ";

		// Token: 0x04002207 RID: 8711
		private const string katakanaIrohaHw = "ｲﾛﾊﾆﾎﾍﾄﾁﾘﾇﾙｦﾜｶﾖﾀﾚｿﾂﾈﾅﾗﾑｳヰﾉｵｸﾔﾏｹﾌｺｴﾃｱｻｷﾕﾒﾐｼヱﾋﾓｾｽﾝ";

		// Token: 0x04002208 RID: 8712
		private const string cjkIdeographic = "〇一二三四五六七八九";
	}
}
