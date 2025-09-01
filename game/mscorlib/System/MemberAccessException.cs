using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt to access a class member fails.</summary>
	// Token: 0x02000156 RID: 342
	[Serializable]
	public class MemberAccessException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MemberAccessException" /> class.</summary>
		// Token: 0x06000D49 RID: 3401 RVA: 0x00033813 File Offset: 0x00031A13
		public MemberAccessException() : base("Cannot access member.")
		{
			base.HResult = -2146233062;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MemberAccessException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x06000D4A RID: 3402 RVA: 0x0003382B File Offset: 0x00031A2B
		public MemberAccessException(string message) : base(message)
		{
			base.HResult = -2146233062;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MemberAccessException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000D4B RID: 3403 RVA: 0x0003383F File Offset: 0x00031A3F
		public MemberAccessException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233062;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MemberAccessException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000D4C RID: 3404 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected MemberAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
