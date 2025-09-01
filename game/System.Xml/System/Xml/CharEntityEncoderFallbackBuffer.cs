using System;
using System.Globalization;
using System.Text;

namespace System.Xml
{
	// Token: 0x0200002D RID: 45
	internal class CharEntityEncoderFallbackBuffer : EncoderFallbackBuffer
	{
		// Token: 0x0600016F RID: 367 RVA: 0x0000B336 File Offset: 0x00009536
		internal CharEntityEncoderFallbackBuffer(CharEntityEncoderFallback parent)
		{
			this.parent = parent;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000B358 File Offset: 0x00009558
		public override bool Fallback(char charUnknown, int index)
		{
			if (this.charEntityIndex >= 0)
			{
				new EncoderExceptionFallback().CreateFallbackBuffer().Fallback(charUnknown, index);
			}
			if (this.parent.CanReplaceAt(index))
			{
				this.charEntity = string.Format(CultureInfo.InvariantCulture, "&#x{0:X};", new object[]
				{
					(int)charUnknown
				});
				this.charEntityIndex = 0;
				return true;
			}
			new EncoderExceptionFallback().CreateFallbackBuffer().Fallback(charUnknown, index);
			return false;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000B3D0 File Offset: 0x000095D0
		public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
		{
			if (!char.IsSurrogatePair(charUnknownHigh, charUnknownLow))
			{
				throw XmlConvert.CreateInvalidSurrogatePairException(charUnknownHigh, charUnknownLow);
			}
			if (this.charEntityIndex >= 0)
			{
				new EncoderExceptionFallback().CreateFallbackBuffer().Fallback(charUnknownHigh, charUnknownLow, index);
			}
			if (this.parent.CanReplaceAt(index))
			{
				this.charEntity = string.Format(CultureInfo.InvariantCulture, "&#x{0:X};", new object[]
				{
					this.SurrogateCharToUtf32(charUnknownHigh, charUnknownLow)
				});
				this.charEntityIndex = 0;
				return true;
			}
			new EncoderExceptionFallback().CreateFallbackBuffer().Fallback(charUnknownHigh, charUnknownLow, index);
			return false;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000B460 File Offset: 0x00009660
		public override char GetNextChar()
		{
			if (this.charEntityIndex == this.charEntity.Length)
			{
				this.charEntityIndex = -1;
			}
			if (this.charEntityIndex == -1)
			{
				return '\0';
			}
			string text = this.charEntity;
			int num = this.charEntityIndex;
			this.charEntityIndex = num + 1;
			return text[num];
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000B4AE File Offset: 0x000096AE
		public override bool MovePrevious()
		{
			if (this.charEntityIndex == -1)
			{
				return false;
			}
			if (this.charEntityIndex > 0)
			{
				this.charEntityIndex--;
				return true;
			}
			return false;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000B4D5 File Offset: 0x000096D5
		public override int Remaining
		{
			get
			{
				if (this.charEntityIndex == -1)
				{
					return 0;
				}
				return this.charEntity.Length - this.charEntityIndex;
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000B4F4 File Offset: 0x000096F4
		public override void Reset()
		{
			this.charEntityIndex = -1;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000B4FD File Offset: 0x000096FD
		private int SurrogateCharToUtf32(char highSurrogate, char lowSurrogate)
		{
			return XmlCharType.CombineSurrogateChar((int)lowSurrogate, (int)highSurrogate);
		}

		// Token: 0x040005DC RID: 1500
		private CharEntityEncoderFallback parent;

		// Token: 0x040005DD RID: 1501
		private string charEntity = string.Empty;

		// Token: 0x040005DE RID: 1502
		private int charEntityIndex = -1;
	}
}
