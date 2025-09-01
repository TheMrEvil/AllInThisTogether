using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is insufficient execution stack available to allow most methods to execute.</summary>
	// Token: 0x02000147 RID: 327
	[Serializable]
	public sealed class InsufficientExecutionStackException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.InsufficientExecutionStackException" /> class.</summary>
		// Token: 0x06000C1A RID: 3098 RVA: 0x00031F4E File Offset: 0x0003014E
		public InsufficientExecutionStackException() : base("Insufficient stack to continue executing the program safely. This can happen from having too many functions on the call stack or function on the stack using too much stack space.")
		{
			base.HResult = -2146232968;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InsufficientExecutionStackException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06000C1B RID: 3099 RVA: 0x00031F66 File Offset: 0x00030166
		public InsufficientExecutionStackException(string message) : base(message)
		{
			base.HResult = -2146232968;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InsufficientExecutionStackException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the inner parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000C1C RID: 3100 RVA: 0x00031F7A File Offset: 0x0003017A
		public InsufficientExecutionStackException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232968;
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00020A65 File Offset: 0x0001EC65
		internal InsufficientExecutionStackException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
