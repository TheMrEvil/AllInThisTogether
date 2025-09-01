using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to combine two delegates based on the <see cref="T:System.Delegate" /> type instead of the <see cref="T:System.MulticastDelegate" /> type. This class cannot be inherited.</summary>
	// Token: 0x0200015D RID: 349
	[Serializable]
	public sealed class MulticastNotSupportedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MulticastNotSupportedException" /> class.</summary>
		// Token: 0x06000DCE RID: 3534 RVA: 0x00035E3C File Offset: 0x0003403C
		public MulticastNotSupportedException() : base("Attempted to add multiple callbacks to a delegate that does not support multicast.")
		{
			base.HResult = -2146233068;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MulticastNotSupportedException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x06000DCF RID: 3535 RVA: 0x00035E54 File Offset: 0x00034054
		public MulticastNotSupportedException(string message) : base(message)
		{
			base.HResult = -2146233068;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MulticastNotSupportedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000DD0 RID: 3536 RVA: 0x00035E68 File Offset: 0x00034068
		public MulticastNotSupportedException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233068;
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x00020A65 File Offset: 0x0001EC65
		internal MulticastNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
