using System;
using System.Threading;

namespace System.Text
{
	// Token: 0x02000392 RID: 914
	internal sealed class InternalDecoderBestFitFallbackBuffer : DecoderFallbackBuffer
	{
		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x0600259E RID: 9630 RVA: 0x00085C94 File Offset: 0x00083E94
		private static object InternalSyncObject
		{
			get
			{
				if (InternalDecoderBestFitFallbackBuffer.s_InternalSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange<object>(ref InternalDecoderBestFitFallbackBuffer.s_InternalSyncObject, value, null);
				}
				return InternalDecoderBestFitFallbackBuffer.s_InternalSyncObject;
			}
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x00085CC0 File Offset: 0x00083EC0
		public InternalDecoderBestFitFallbackBuffer(InternalDecoderBestFitFallback fallback)
		{
			this._oFallback = fallback;
			if (this._oFallback._arrayBestFit == null)
			{
				object internalSyncObject = InternalDecoderBestFitFallbackBuffer.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (this._oFallback._arrayBestFit == null)
					{
						this._oFallback._arrayBestFit = fallback._encoding.GetBestFitBytesToUnicodeData();
					}
				}
			}
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x00085D40 File Offset: 0x00083F40
		public override bool Fallback(byte[] bytesUnknown, int index)
		{
			this._cBestFit = this.TryBestFit(bytesUnknown);
			if (this._cBestFit == '\0')
			{
				this._cBestFit = this._oFallback._cReplacement;
			}
			this._iCount = (this._iSize = 1);
			return true;
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x00085D84 File Offset: 0x00083F84
		public override char GetNextChar()
		{
			this._iCount--;
			if (this._iCount < 0)
			{
				return '\0';
			}
			if (this._iCount == 2147483647)
			{
				this._iCount = -1;
				return '\0';
			}
			return this._cBestFit;
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x00085DBB File Offset: 0x00083FBB
		public override bool MovePrevious()
		{
			if (this._iCount >= 0)
			{
				this._iCount++;
			}
			return this._iCount >= 0 && this._iCount <= this._iSize;
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060025A3 RID: 9635 RVA: 0x00085DF0 File Offset: 0x00083FF0
		public override int Remaining
		{
			get
			{
				if (this._iCount <= 0)
				{
					return 0;
				}
				return this._iCount;
			}
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x00085E03 File Offset: 0x00084003
		public override void Reset()
		{
			this._iCount = -1;
			this.byteStart = null;
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x000040F7 File Offset: 0x000022F7
		internal unsafe override int InternalFallback(byte[] bytes, byte* pBytes)
		{
			return 1;
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x00085E14 File Offset: 0x00084014
		private char TryBestFit(byte[] bytesCheck)
		{
			int num = 0;
			int num2 = this._oFallback._arrayBestFit.Length;
			if (num2 == 0)
			{
				return '\0';
			}
			if (bytesCheck.Length == 0 || bytesCheck.Length > 2)
			{
				return '\0';
			}
			char c;
			if (bytesCheck.Length == 1)
			{
				c = (char)bytesCheck[0];
			}
			else
			{
				c = (char)(((int)bytesCheck[0] << 8) + (int)bytesCheck[1]);
			}
			if (c < this._oFallback._arrayBestFit[0] || c > this._oFallback._arrayBestFit[num2 - 2])
			{
				return '\0';
			}
			int num3;
			while ((num3 = num2 - num) > 6)
			{
				int i = num3 / 2 + num & 65534;
				char c2 = this._oFallback._arrayBestFit[i];
				if (c2 == c)
				{
					return this._oFallback._arrayBestFit[i + 1];
				}
				if (c2 < c)
				{
					num = i;
				}
				else
				{
					num2 = i;
				}
			}
			for (int i = num; i < num2; i += 2)
			{
				if (this._oFallback._arrayBestFit[i] == c)
				{
					return this._oFallback._arrayBestFit[i + 1];
				}
			}
			return '\0';
		}

		// Token: 0x04001D91 RID: 7569
		private char _cBestFit;

		// Token: 0x04001D92 RID: 7570
		private int _iCount = -1;

		// Token: 0x04001D93 RID: 7571
		private int _iSize;

		// Token: 0x04001D94 RID: 7572
		private InternalDecoderBestFitFallback _oFallback;

		// Token: 0x04001D95 RID: 7573
		private static object s_InternalSyncObject;
	}
}
