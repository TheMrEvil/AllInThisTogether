using System;
using System.Globalization;

namespace System.Text
{
	/// <summary>Provides a buffer that allows a fallback handler to return an alternate string to a decoder when it cannot decode an input byte sequence.</summary>
	// Token: 0x02000397 RID: 919
	public abstract class DecoderFallbackBuffer
	{
		/// <summary>When overridden in a derived class, prepares the fallback buffer to handle the specified input byte sequence.</summary>
		/// <param name="bytesUnknown">An input array of bytes.</param>
		/// <param name="index">The index position of a byte in <paramref name="bytesUnknown" />.</param>
		/// <returns>
		///   <see langword="true" /> if the fallback buffer can process <paramref name="bytesUnknown" />; <see langword="false" /> if the fallback buffer ignores <paramref name="bytesUnknown" />.</returns>
		// Token: 0x060025BE RID: 9662
		public abstract bool Fallback(byte[] bytesUnknown, int index);

		/// <summary>When overridden in a derived class, retrieves the next character in the fallback buffer.</summary>
		/// <returns>The next character in the fallback buffer.</returns>
		// Token: 0x060025BF RID: 9663
		public abstract char GetNextChar();

		/// <summary>When overridden in a derived class, causes the next call to the <see cref="M:System.Text.DecoderFallbackBuffer.GetNextChar" /> method to access the data buffer character position that is prior to the current character position.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="M:System.Text.DecoderFallbackBuffer.MovePrevious" /> operation was successful; otherwise, <see langword="false" />.</returns>
		// Token: 0x060025C0 RID: 9664
		public abstract bool MovePrevious();

		/// <summary>When overridden in a derived class, gets the number of characters in the current <see cref="T:System.Text.DecoderFallbackBuffer" /> object that remain to be processed.</summary>
		/// <returns>The number of characters in the current fallback buffer that have not yet been processed.</returns>
		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060025C1 RID: 9665
		public abstract int Remaining { get; }

		/// <summary>Initializes all data and state information pertaining to this fallback buffer.</summary>
		// Token: 0x060025C2 RID: 9666 RVA: 0x0008605B File Offset: 0x0008425B
		public virtual void Reset()
		{
			while (this.GetNextChar() != '\0')
			{
			}
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x00086065 File Offset: 0x00084265
		internal void InternalReset()
		{
			this.byteStart = null;
			this.Reset();
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x00086075 File Offset: 0x00084275
		internal unsafe void InternalInitialize(byte* byteStart, char* charEnd)
		{
			this.byteStart = byteStart;
			this.charEnd = charEnd;
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x00086088 File Offset: 0x00084288
		internal unsafe virtual bool InternalFallback(byte[] bytes, byte* pBytes, ref char* chars)
		{
			if (this.Fallback(bytes, (int)((long)(pBytes - this.byteStart) - (long)bytes.Length)))
			{
				char* ptr = chars;
				bool flag = false;
				char nextChar;
				while ((nextChar = this.GetNextChar()) != '\0')
				{
					if (char.IsSurrogate(nextChar))
					{
						if (char.IsHighSurrogate(nextChar))
						{
							if (flag)
							{
								throw new ArgumentException("String contains invalid Unicode code points.");
							}
							flag = true;
						}
						else
						{
							if (!flag)
							{
								throw new ArgumentException("String contains invalid Unicode code points.");
							}
							flag = false;
						}
					}
					if (ptr >= this.charEnd)
					{
						return false;
					}
					*(ptr++) = nextChar;
				}
				if (flag)
				{
					throw new ArgumentException("String contains invalid Unicode code points.");
				}
				chars = ptr;
			}
			return true;
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x00086118 File Offset: 0x00084318
		internal unsafe virtual int InternalFallback(byte[] bytes, byte* pBytes)
		{
			if (!this.Fallback(bytes, (int)((long)(pBytes - this.byteStart) - (long)bytes.Length)))
			{
				return 0;
			}
			int num = 0;
			bool flag = false;
			char nextChar;
			while ((nextChar = this.GetNextChar()) != '\0')
			{
				if (char.IsSurrogate(nextChar))
				{
					if (char.IsHighSurrogate(nextChar))
					{
						if (flag)
						{
							throw new ArgumentException("String contains invalid Unicode code points.");
						}
						flag = true;
					}
					else
					{
						if (!flag)
						{
							throw new ArgumentException("String contains invalid Unicode code points.");
						}
						flag = false;
					}
				}
				num++;
			}
			if (flag)
			{
				throw new ArgumentException("String contains invalid Unicode code points.");
			}
			return num;
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x00086198 File Offset: 0x00084398
		internal void ThrowLastBytesRecursive(byte[] bytesUnknown)
		{
			StringBuilder stringBuilder = new StringBuilder(bytesUnknown.Length * 3);
			int num = 0;
			while (num < bytesUnknown.Length && num < 20)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "\\x{0:X2}", bytesUnknown[num]);
				num++;
			}
			if (num == 20)
			{
				stringBuilder.Append(" ...");
			}
			throw new ArgumentException(SR.Format("Recursive fallback not allowed for bytes {0}.", stringBuilder.ToString()), "bytesUnknown");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderFallbackBuffer" /> class.</summary>
		// Token: 0x060025C8 RID: 9672 RVA: 0x0000259F File Offset: 0x0000079F
		protected DecoderFallbackBuffer()
		{
		}

		// Token: 0x04001D9A RID: 7578
		internal unsafe byte* byteStart;

		// Token: 0x04001D9B RID: 7579
		internal unsafe char* charEnd;
	}
}
