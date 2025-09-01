using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when the time allotted for a process or operation has expired.</summary>
	// Token: 0x02000195 RID: 405
	[Serializable]
	public class TimeoutException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.TimeoutException" /> class.</summary>
		// Token: 0x0600102F RID: 4143 RVA: 0x000425C3 File Offset: 0x000407C3
		public TimeoutException() : base("The operation has timed out.")
		{
			base.HResult = -2146233083;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeoutException" /> class with the specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x06001030 RID: 4144 RVA: 0x000425DB File Offset: 0x000407DB
		public TimeoutException(string message) : base(message)
		{
			base.HResult = -2146233083;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeoutException" /> class with the specified error message and inner exception.</summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06001031 RID: 4145 RVA: 0x000425EF File Offset: 0x000407EF
		public TimeoutException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233083;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeoutException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination. The <paramref name="context" /> parameter is reserved for future use, and can be specified as <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is <see langword="null" />, or <see cref="P:System.Exception.HResult" /> is zero (0).</exception>
		// Token: 0x06001032 RID: 4146 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected TimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
