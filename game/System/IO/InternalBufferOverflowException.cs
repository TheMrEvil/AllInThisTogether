using System;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception thrown when the internal buffer overflows.</summary>
	// Token: 0x02000518 RID: 1304
	[Serializable]
	public class InternalBufferOverflowException : SystemException
	{
		/// <summary>Initializes a new default instance of the <see cref="T:System.IO.InternalBufferOverflowException" /> class.</summary>
		// Token: 0x06002A4C RID: 10828 RVA: 0x000918D5 File Offset: 0x0008FAD5
		public InternalBufferOverflowException() : base("Internal buffer overflow occurred.")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InternalBufferOverflowException" /> class with the error message to be displayed specified.</summary>
		/// <param name="message">The message to be given for the exception.</param>
		// Token: 0x06002A4D RID: 10829 RVA: 0x0002F15C File Offset: 0x0002D35C
		public InternalBufferOverflowException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.IO.InternalBufferOverflowException" /> class that is serializable using the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects.</summary>
		/// <param name="info">The information required to serialize the T:System.IO.InternalBufferOverflowException object.</param>
		/// <param name="context">The source and destination of the serialized stream associated with the T:System.IO.InternalBufferOverflowException object.</param>
		// Token: 0x06002A4E RID: 10830 RVA: 0x0005573F File Offset: 0x0005393F
		protected InternalBufferOverflowException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InternalBufferOverflowException" /> class with the message to be displayed and the generated inner exception specified.</summary>
		/// <param name="message">The message to be given for the exception.</param>
		/// <param name="inner">The inner exception.</param>
		// Token: 0x06002A4F RID: 10831 RVA: 0x0002F191 File Offset: 0x0002D391
		public InternalBufferOverflowException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
