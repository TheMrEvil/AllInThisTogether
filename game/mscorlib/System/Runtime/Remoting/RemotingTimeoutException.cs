using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
	/// <summary>The exception that is thrown when the server or the client cannot be reached for a previously specified period of time.</summary>
	// Token: 0x0200056D RID: 1389
	[ComVisible(true)]
	[Serializable]
	public class RemotingTimeoutException : RemotingException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.RemotingTimeoutException" /> class with default properties.</summary>
		// Token: 0x060036A8 RID: 13992 RVA: 0x000C5945 File Offset: 0x000C3B45
		public RemotingTimeoutException()
		{
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Runtime.Remoting.RemotingTimeoutException" /> class with a specified message.</summary>
		/// <param name="message">The message that indicates the reason why the exception occurred.</param>
		// Token: 0x060036A9 RID: 13993 RVA: 0x000C594D File Offset: 0x000C3B4D
		public RemotingTimeoutException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.RemotingTimeoutException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="InnerException">The exception that is the cause of the current exception. If the <paramref name="InnerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060036AA RID: 13994 RVA: 0x000C5956 File Offset: 0x000C3B56
		public RemotingTimeoutException(string message, Exception InnerException) : base(message, InnerException)
		{
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x000C5960 File Offset: 0x000C3B60
		internal RemotingTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
