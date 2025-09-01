using System;
using System.Threading;

namespace System.Text
{
	// Token: 0x0200039D RID: 925
	internal sealed class InternalEncoderBestFitFallbackBuffer : EncoderFallbackBuffer
	{
		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060025FB RID: 9723 RVA: 0x00086D68 File Offset: 0x00084F68
		private static object InternalSyncObject
		{
			get
			{
				if (InternalEncoderBestFitFallbackBuffer.s_InternalSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange<object>(ref InternalEncoderBestFitFallbackBuffer.s_InternalSyncObject, value, null);
				}
				return InternalEncoderBestFitFallbackBuffer.s_InternalSyncObject;
			}
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x00086D94 File Offset: 0x00084F94
		public InternalEncoderBestFitFallbackBuffer(InternalEncoderBestFitFallback fallback)
		{
			this._oFallback = fallback;
			if (this._oFallback._arrayBestFit == null)
			{
				object internalSyncObject = InternalEncoderBestFitFallbackBuffer.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (this._oFallback._arrayBestFit == null)
					{
						this._oFallback._arrayBestFit = fallback._encoding.GetBestFitUnicodeToBytesData();
					}
				}
			}
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x00086E14 File Offset: 0x00085014
		public override bool Fallback(char charUnknown, int index)
		{
			this._iCount = (this._iSize = 1);
			this._cBestFit = this.TryBestFit(charUnknown);
			if (this._cBestFit == '\0')
			{
				this._cBestFit = '?';
			}
			return true;
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x00086E50 File Offset: 0x00085050
		public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
		{
			if (!char.IsHighSurrogate(charUnknownHigh))
			{
				throw new ArgumentOutOfRangeException("charUnknownHigh", SR.Format("Valid values are between {0} and {1}, inclusive.", 55296, 56319));
			}
			if (!char.IsLowSurrogate(charUnknownLow))
			{
				throw new ArgumentOutOfRangeException("charUnknownLow", SR.Format("Valid values are between {0} and {1}, inclusive.", 56320, 57343));
			}
			this._cBestFit = '?';
			this._iCount = (this._iSize = 2);
			return true;
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x00086ED8 File Offset: 0x000850D8
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

		// Token: 0x06002600 RID: 9728 RVA: 0x00086F0F File Offset: 0x0008510F
		public override bool MovePrevious()
		{
			if (this._iCount >= 0)
			{
				this._iCount++;
			}
			return this._iCount >= 0 && this._iCount <= this._iSize;
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06002601 RID: 9729 RVA: 0x00086F44 File Offset: 0x00085144
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

		// Token: 0x06002602 RID: 9730 RVA: 0x00086F57 File Offset: 0x00085157
		public override void Reset()
		{
			this._iCount = -1;
			this.charStart = null;
			this.bFallingBack = false;
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x00086F70 File Offset: 0x00085170
		private char TryBestFit(char cUnknown)
		{
			int num = 0;
			int num2 = this._oFallback._arrayBestFit.Length;
			int num3;
			while ((num3 = num2 - num) > 6)
			{
				int i = num3 / 2 + num & 65534;
				char c = this._oFallback._arrayBestFit[i];
				if (c == cUnknown)
				{
					return this._oFallback._arrayBestFit[i + 1];
				}
				if (c < cUnknown)
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
				if (this._oFallback._arrayBestFit[i] == cUnknown)
				{
					return this._oFallback._arrayBestFit[i + 1];
				}
			}
			return '\0';
		}

		// Token: 0x04001DA8 RID: 7592
		private char _cBestFit;

		// Token: 0x04001DA9 RID: 7593
		private InternalEncoderBestFitFallback _oFallback;

		// Token: 0x04001DAA RID: 7594
		private int _iCount = -1;

		// Token: 0x04001DAB RID: 7595
		private int _iSize;

		// Token: 0x04001DAC RID: 7596
		private static object s_InternalSyncObject;
	}
}
