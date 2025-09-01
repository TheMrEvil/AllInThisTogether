using System;

namespace System.Text
{
	// Token: 0x0200039C RID: 924
	[Serializable]
	internal class InternalEncoderBestFitFallback : EncoderFallback
	{
		// Token: 0x060025F6 RID: 9718 RVA: 0x00086D10 File Offset: 0x00084F10
		internal InternalEncoderBestFitFallback(Encoding encoding)
		{
			this._encoding = encoding;
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x00086D1F File Offset: 0x00084F1F
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new InternalEncoderBestFitFallbackBuffer(this);
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060025F8 RID: 9720 RVA: 0x000040F7 File Offset: 0x000022F7
		public override int MaxCharCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x00086D28 File Offset: 0x00084F28
		public override bool Equals(object value)
		{
			InternalEncoderBestFitFallback internalEncoderBestFitFallback = value as InternalEncoderBestFitFallback;
			return internalEncoderBestFitFallback != null && this._encoding.CodePage == internalEncoderBestFitFallback._encoding.CodePage;
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x00086D59 File Offset: 0x00084F59
		public override int GetHashCode()
		{
			return this._encoding.CodePage;
		}

		// Token: 0x04001DA6 RID: 7590
		internal Encoding _encoding;

		// Token: 0x04001DA7 RID: 7591
		internal char[] _arrayBestFit;
	}
}
