using System;

namespace System.Text
{
	// Token: 0x02000391 RID: 913
	[Serializable]
	internal sealed class InternalDecoderBestFitFallback : DecoderFallback
	{
		// Token: 0x06002599 RID: 9625 RVA: 0x00085C34 File Offset: 0x00083E34
		internal InternalDecoderBestFitFallback(Encoding encoding)
		{
			this._encoding = encoding;
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x00085C4B File Offset: 0x00083E4B
		public override DecoderFallbackBuffer CreateFallbackBuffer()
		{
			return new InternalDecoderBestFitFallbackBuffer(this);
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x0600259B RID: 9627 RVA: 0x000040F7 File Offset: 0x000022F7
		public override int MaxCharCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x00085C54 File Offset: 0x00083E54
		public override bool Equals(object value)
		{
			InternalDecoderBestFitFallback internalDecoderBestFitFallback = value as InternalDecoderBestFitFallback;
			return internalDecoderBestFitFallback != null && this._encoding.CodePage == internalDecoderBestFitFallback._encoding.CodePage;
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x00085C85 File Offset: 0x00083E85
		public override int GetHashCode()
		{
			return this._encoding.CodePage;
		}

		// Token: 0x04001D8E RID: 7566
		internal Encoding _encoding;

		// Token: 0x04001D8F RID: 7567
		internal char[] _arrayBestFit;

		// Token: 0x04001D90 RID: 7568
		internal char _cReplacement = '?';
	}
}
