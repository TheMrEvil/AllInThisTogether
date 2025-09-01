using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a requested method or operation is not implemented.</summary>
	// Token: 0x02000160 RID: 352
	[Serializable]
	public class NotImplementedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.NotImplementedException" /> class with default properties.</summary>
		// Token: 0x06000DDC RID: 3548 RVA: 0x00035F7B File Offset: 0x0003417B
		public NotImplementedException() : base("The method or operation is not implemented.")
		{
			base.HResult = -2147467263;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotImplementedException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06000DDD RID: 3549 RVA: 0x00035F93 File Offset: 0x00034193
		public NotImplementedException(string message) : base(message)
		{
			base.HResult = -2147467263;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotImplementedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000DDE RID: 3550 RVA: 0x00035FA7 File Offset: 0x000341A7
		public NotImplementedException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2147467263;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotImplementedException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x06000DDF RID: 3551 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected NotImplementedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
