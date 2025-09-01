using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.ComponentModel.Design
{
	/// <summary>The exception that is thrown when an attempt to check out a file that is checked into a source code management program is canceled or fails.</summary>
	// Token: 0x0200043E RID: 1086
	[Serializable]
	public class CheckoutException : ExternalException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class with no associated message or error code.</summary>
		// Token: 0x06002381 RID: 9089 RVA: 0x00080FE3 File Offset: 0x0007F1E3
		public CheckoutException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class with the specified message.</summary>
		/// <param name="message">A message describing the exception.</param>
		// Token: 0x06002382 RID: 9090 RVA: 0x00080FEB File Offset: 0x0007F1EB
		public CheckoutException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class with the specified message and error code.</summary>
		/// <param name="message">A message describing the exception.</param>
		/// <param name="errorCode">The error code to pass.</param>
		// Token: 0x06002383 RID: 9091 RVA: 0x00080FF4 File Offset: 0x0007F1F4
		public CheckoutException(string message, int errorCode) : base(message, errorCode)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class using the specified serialization data and context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		// Token: 0x06002384 RID: 9092 RVA: 0x00080FFE File Offset: 0x0007F1FE
		protected CheckoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class with the specified detailed description and the specified exception.</summary>
		/// <param name="message">A detailed description of the error.</param>
		/// <param name="innerException">A reference to the inner exception that is the cause of this exception.</param>
		// Token: 0x06002385 RID: 9093 RVA: 0x00081008 File Offset: 0x0007F208
		public CheckoutException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x00081012 File Offset: 0x0007F212
		// Note: this type is marked as 'beforefieldinit'.
		static CheckoutException()
		{
		}

		// Token: 0x040010AC RID: 4268
		private const int E_ABORT = -2147467260;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class that specifies that the check out was canceled. This field is read-only.</summary>
		// Token: 0x040010AD RID: 4269
		public static readonly CheckoutException Canceled = new CheckoutException("The checkout was canceled by the user.", -2147467260);
	}
}
