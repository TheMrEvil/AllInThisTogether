using System;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>The exception that is thrown when the number of parameters for an invocation does not match the number expected. This class cannot be inherited.</summary>
	// Token: 0x020008CC RID: 2252
	[Serializable]
	public sealed class TargetParameterCountException : ApplicationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetParameterCountException" /> class with an empty message string and the root cause of the exception.</summary>
		// Token: 0x06004AEC RID: 19180 RVA: 0x000EFDB6 File Offset: 0x000EDFB6
		public TargetParameterCountException() : base("Number of parameters specified does not match the expected number.")
		{
			base.HResult = -2147352562;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetParameterCountException" /> class with its message string set to the given message and the root cause exception.</summary>
		/// <param name="message">A <see langword="String" /> describing the reason this exception was thrown.</param>
		// Token: 0x06004AED RID: 19181 RVA: 0x000EFDCE File Offset: 0x000EDFCE
		public TargetParameterCountException(string message) : base(message)
		{
			base.HResult = -2147352562;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetParameterCountException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06004AEE RID: 19182 RVA: 0x000EFDE2 File Offset: 0x000EDFE2
		public TargetParameterCountException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2147352562;
		}

		// Token: 0x06004AEF RID: 19183 RVA: 0x0006E7BD File Offset: 0x0006C9BD
		internal TargetParameterCountException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
