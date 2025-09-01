using System;
using System.Runtime.Serialization;

namespace System.ComponentModel
{
	/// <summary>Thrown when a thread on which an operation should execute no longer exists or has no message loop.</summary>
	// Token: 0x020003C3 RID: 963
	[Serializable]
	public class InvalidAsynchronousStateException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidAsynchronousStateException" /> class.</summary>
		// Token: 0x06001F3B RID: 7995 RVA: 0x0006CDF5 File Offset: 0x0006AFF5
		public InvalidAsynchronousStateException() : this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidAsynchronousStateException" /> class with the specified detailed description.</summary>
		/// <param name="message">A detailed description of the error.</param>
		// Token: 0x06001F3C RID: 7996 RVA: 0x0006855F File Offset: 0x0006675F
		public InvalidAsynchronousStateException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidAsynchronousStateException" /> class with the specified detailed description and the specified exception.</summary>
		/// <param name="message">A detailed description of the error.</param>
		/// <param name="innerException">A reference to the inner exception that is the cause of this exception.</param>
		// Token: 0x06001F3D RID: 7997 RVA: 0x00068568 File Offset: 0x00066768
		public InvalidAsynchronousStateException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidAsynchronousStateException" /> class with the given <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		// Token: 0x06001F3E RID: 7998 RVA: 0x00068598 File Offset: 0x00066798
		protected InvalidAsynchronousStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
