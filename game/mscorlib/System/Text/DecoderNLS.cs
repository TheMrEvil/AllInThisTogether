using System;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x02000398 RID: 920
	internal class DecoderNLS : Decoder
	{
		// Token: 0x060025C9 RID: 9673 RVA: 0x00086219 File Offset: 0x00084419
		internal DecoderNLS(Encoding encoding)
		{
			this._encoding = encoding;
			this._fallback = this._encoding.DecoderFallback;
			this.Reset();
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x0008623F File Offset: 0x0008443F
		internal DecoderNLS()
		{
			this._encoding = null;
			this.Reset();
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x00086254 File Offset: 0x00084454
		public override void Reset()
		{
			DecoderFallbackBuffer fallbackBuffer = this._fallbackBuffer;
			if (fallbackBuffer == null)
			{
				return;
			}
			fallbackBuffer.Reset();
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x00086266 File Offset: 0x00084466
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return this.GetCharCount(bytes, index, count, false);
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x00086274 File Offset: 0x00084474
		public unsafe override int GetCharCount(byte[] bytes, int index, int count, bool flush)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", "Array cannot be null.");
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
			}
			if (bytes.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("bytes", "Index and count must refer to a location within the buffer.");
			}
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
			{
				byte* ptr = reference;
				return this.GetCharCount(ptr + index, count, flush);
			}
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x000862EC File Offset: 0x000844EC
		public unsafe override int GetCharCount(byte* bytes, int count, bool flush)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", "Array cannot be null.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			this._mustFlush = flush;
			this._throwOnOverflow = true;
			return this._encoding.GetCharCount(bytes, count, this);
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x0008633E File Offset: 0x0008453E
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			return this.GetChars(bytes, byteIndex, byteCount, chars, charIndex, false);
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x00086350 File Offset: 0x00084550
		public unsafe override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool flush)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", "Array cannot be null.");
			}
			if (byteIndex < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteIndex < 0) ? "byteIndex" : "byteCount", "Non-negative number required.");
			}
			if (bytes.Length - byteIndex < byteCount)
			{
				throw new ArgumentOutOfRangeException("bytes", "Index and count must refer to a location within the buffer.");
			}
			if (charIndex < 0 || charIndex > chars.Length)
			{
				throw new ArgumentOutOfRangeException("charIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			int charCount = chars.Length - charIndex;
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
			{
				byte* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(chars))
				{
					char* ptr2 = reference2;
					return this.GetChars(ptr + byteIndex, byteCount, ptr2 + charIndex, charCount, flush);
				}
			}
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x00086414 File Offset: 0x00084614
		public unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, bool flush)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", "Array cannot be null.");
			}
			if (byteCount < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteCount < 0) ? "byteCount" : "charCount", "Non-negative number required.");
			}
			this._mustFlush = flush;
			this._throwOnOverflow = true;
			return this._encoding.GetChars(bytes, byteCount, chars, charCount, this);
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x0008648C File Offset: 0x0008468C
		public unsafe override void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", "Array cannot be null.");
			}
			if (byteIndex < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteIndex < 0) ? "byteIndex" : "byteCount", "Non-negative number required.");
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", "Non-negative number required.");
			}
			if (bytes.Length - byteIndex < byteCount)
			{
				throw new ArgumentOutOfRangeException("bytes", "Index and count must refer to a location within the buffer.");
			}
			if (chars.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("chars", "Index and count must refer to a location within the buffer.");
			}
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
			{
				byte* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(chars))
				{
					char* ptr2 = reference2;
					this.Convert(ptr + byteIndex, byteCount, ptr2 + charIndex, charCount, flush, out bytesUsed, out charsUsed, out completed);
				}
			}
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x00086578 File Offset: 0x00084778
		public unsafe override void Convert(byte* bytes, int byteCount, char* chars, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", "Array cannot be null.");
			}
			if (byteCount < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteCount < 0) ? "byteCount" : "charCount", "Non-negative number required.");
			}
			this._mustFlush = flush;
			this._throwOnOverflow = false;
			this._bytesUsed = 0;
			charsUsed = this._encoding.GetChars(bytes, byteCount, chars, charCount, this);
			bytesUsed = this._bytesUsed;
			completed = (bytesUsed == byteCount && (!flush || !this.HasState) && (this._fallbackBuffer == null || this._fallbackBuffer.Remaining == 0));
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060025D4 RID: 9684 RVA: 0x00086633 File Offset: 0x00084833
		public bool MustFlush
		{
			get
			{
				return this._mustFlush;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060025D5 RID: 9685 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		internal virtual bool HasState
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x0008663B File Offset: 0x0008483B
		internal void ClearMustFlush()
		{
			this._mustFlush = false;
		}

		// Token: 0x04001D9C RID: 7580
		private Encoding _encoding;

		// Token: 0x04001D9D RID: 7581
		private bool _mustFlush;

		// Token: 0x04001D9E RID: 7582
		internal bool _throwOnOverflow;

		// Token: 0x04001D9F RID: 7583
		internal int _bytesUsed;
	}
}
