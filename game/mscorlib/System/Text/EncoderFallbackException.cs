using System;
using System.Runtime.Serialization;

namespace System.Text
{
	/// <summary>The exception that is thrown when an encoder fallback operation fails. This class cannot be inherited.</summary>
	// Token: 0x020003A0 RID: 928
	[Serializable]
	public sealed class EncoderFallbackException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderFallbackException" /> class.</summary>
		// Token: 0x0600260F RID: 9743 RVA: 0x00085FAB File Offset: 0x000841AB
		public EncoderFallbackException() : base("Value does not fall within the expected range.")
		{
			base.HResult = -2147024809;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderFallbackException" /> class. A parameter specifies the error message.</summary>
		/// <param name="message">An error message.</param>
		// Token: 0x06002610 RID: 9744 RVA: 0x00085FC3 File Offset: 0x000841C3
		public EncoderFallbackException(string message) : base(message)
		{
			base.HResult = -2147024809;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderFallbackException" /> class. Parameters specify the error message and the inner exception that is the cause of this exception.</summary>
		/// <param name="message">An error message.</param>
		/// <param name="innerException">The exception that caused this exception.</param>
		// Token: 0x06002611 RID: 9745 RVA: 0x00085FD7 File Offset: 0x000841D7
		public EncoderFallbackException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147024809;
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x000870E1 File Offset: 0x000852E1
		internal EncoderFallbackException(string message, char charUnknown, int index) : base(message)
		{
			this._charUnknown = charUnknown;
			this._index = index;
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x000870F8 File Offset: 0x000852F8
		internal EncoderFallbackException(string message, char charUnknownHigh, char charUnknownLow, int index) : base(message)
		{
			if (!char.IsHighSurrogate(charUnknownHigh))
			{
				throw new ArgumentOutOfRangeException("charUnknownHigh", SR.Format("Valid values are between {0} and {1}, inclusive.", 55296, 56319));
			}
			if (!char.IsLowSurrogate(charUnknownLow))
			{
				throw new ArgumentOutOfRangeException("CharUnknownLow", SR.Format("Valid values are between {0} and {1}, inclusive.", 56320, 57343));
			}
			this._charUnknownHigh = charUnknownHigh;
			this._charUnknownLow = charUnknownLow;
			this._index = index;
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x0002110F File Offset: 0x0001F30F
		private EncoderFallbackException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Gets the input character that caused the exception.</summary>
		/// <returns>The character that cannot be encoded.</returns>
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06002615 RID: 9749 RVA: 0x00087184 File Offset: 0x00085384
		public char CharUnknown
		{
			get
			{
				return this._charUnknown;
			}
		}

		/// <summary>Gets the high component character of the surrogate pair that caused the exception.</summary>
		/// <returns>The high component character of the surrogate pair that cannot be encoded.</returns>
		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06002616 RID: 9750 RVA: 0x0008718C File Offset: 0x0008538C
		public char CharUnknownHigh
		{
			get
			{
				return this._charUnknownHigh;
			}
		}

		/// <summary>Gets the low component character of the surrogate pair that caused the exception.</summary>
		/// <returns>The low component character of the surrogate pair that cannot be encoded.</returns>
		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06002617 RID: 9751 RVA: 0x00087194 File Offset: 0x00085394
		public char CharUnknownLow
		{
			get
			{
				return this._charUnknownLow;
			}
		}

		/// <summary>Gets the index position in the input buffer of the character that caused the exception.</summary>
		/// <returns>The index position in the input buffer of the character that cannot be encoded.</returns>
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06002618 RID: 9752 RVA: 0x0008719C File Offset: 0x0008539C
		public int Index
		{
			get
			{
				return this._index;
			}
		}

		/// <summary>Indicates whether the input that caused the exception is a surrogate pair.</summary>
		/// <returns>
		///   <see langword="true" /> if the input was a surrogate pair; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002619 RID: 9753 RVA: 0x000871A4 File Offset: 0x000853A4
		public bool IsUnknownSurrogate()
		{
			return this._charUnknownHigh > '\0';
		}

		// Token: 0x04001DAD RID: 7597
		private char _charUnknown;

		// Token: 0x04001DAE RID: 7598
		private char _charUnknownHigh;

		// Token: 0x04001DAF RID: 7599
		private char _charUnknownLow;

		// Token: 0x04001DB0 RID: 7600
		private int _index;
	}
}
