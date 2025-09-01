using System;
using System.Runtime.Serialization;

namespace System.Text
{
	/// <summary>The exception that is thrown when a decoder fallback operation fails. This class cannot be inherited.</summary>
	// Token: 0x02000395 RID: 917
	[Serializable]
	public sealed class DecoderFallbackException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderFallbackException" /> class.</summary>
		// Token: 0x060025B2 RID: 9650 RVA: 0x00085FAB File Offset: 0x000841AB
		public DecoderFallbackException() : base("Value does not fall within the expected range.")
		{
			base.HResult = -2147024809;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderFallbackException" /> class. A parameter specifies the error message.</summary>
		/// <param name="message">An error message.</param>
		// Token: 0x060025B3 RID: 9651 RVA: 0x00085FC3 File Offset: 0x000841C3
		public DecoderFallbackException(string message) : base(message)
		{
			base.HResult = -2147024809;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderFallbackException" /> class. Parameters specify the error message and the inner exception that is the cause of this exception.</summary>
		/// <param name="message">An error message.</param>
		/// <param name="innerException">The exception that caused this exception.</param>
		// Token: 0x060025B4 RID: 9652 RVA: 0x00085FD7 File Offset: 0x000841D7
		public DecoderFallbackException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147024809;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderFallbackException" /> class. Parameters specify the error message, the array of bytes being decoded, and the index of the byte that cannot be decoded.</summary>
		/// <param name="message">An error message.</param>
		/// <param name="bytesUnknown">The input byte array.</param>
		/// <param name="index">The index position in <paramref name="bytesUnknown" /> of the byte that cannot be decoded.</param>
		// Token: 0x060025B5 RID: 9653 RVA: 0x00085FEC File Offset: 0x000841EC
		public DecoderFallbackException(string message, byte[] bytesUnknown, int index) : base(message)
		{
			this._bytesUnknown = bytesUnknown;
			this._index = index;
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x0002110F File Offset: 0x0001F30F
		private DecoderFallbackException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Gets the input byte sequence that caused the exception.</summary>
		/// <returns>The input byte array that cannot be decoded.</returns>
		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060025B7 RID: 9655 RVA: 0x00086003 File Offset: 0x00084203
		public byte[] BytesUnknown
		{
			get
			{
				return this._bytesUnknown;
			}
		}

		/// <summary>Gets the index position in the input byte sequence of the byte that caused the exception.</summary>
		/// <returns>The index position in the input byte array of the byte that cannot be decoded. The index position is zero-based.</returns>
		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060025B8 RID: 9656 RVA: 0x0008600B File Offset: 0x0008420B
		public int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x04001D96 RID: 7574
		private byte[] _bytesUnknown;

		// Token: 0x04001D97 RID: 7575
		private int _index;
	}
}
