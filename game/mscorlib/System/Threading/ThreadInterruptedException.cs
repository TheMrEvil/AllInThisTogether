using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when a <see cref="T:System.Threading.Thread" /> is interrupted while it is in a waiting state.</summary>
	// Token: 0x020002D8 RID: 728
	[ComVisible(true)]
	[Serializable]
	public class ThreadInterruptedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadInterruptedException" /> class with default properties.</summary>
		// Token: 0x06001FF8 RID: 8184 RVA: 0x0007499F File Offset: 0x00072B9F
		public ThreadInterruptedException() : base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.ThreadInterrupted))
		{
			base.SetErrorCode(-2146233063);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadInterruptedException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06001FF9 RID: 8185 RVA: 0x000749B8 File Offset: 0x00072BB8
		public ThreadInterruptedException(string message) : base(message)
		{
			base.SetErrorCode(-2146233063);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadInterruptedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06001FFA RID: 8186 RVA: 0x000749CC File Offset: 0x00072BCC
		public ThreadInterruptedException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146233063);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadInterruptedException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x06001FFB RID: 8187 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected ThreadInterruptedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
