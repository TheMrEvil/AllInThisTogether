using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a DLL specified in a DLL import cannot be found.</summary>
	// Token: 0x02000111 RID: 273
	[Serializable]
	public class DllNotFoundException : TypeLoadException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DllNotFoundException" /> class with default properties.</summary>
		// Token: 0x06000A8F RID: 2703 RVA: 0x0002820D File Offset: 0x0002640D
		public DllNotFoundException() : base("Dll was not found.")
		{
			base.HResult = -2146233052;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DllNotFoundException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06000A90 RID: 2704 RVA: 0x00028225 File Offset: 0x00026425
		public DllNotFoundException(string message) : base(message)
		{
			base.HResult = -2146233052;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DllNotFoundException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000A91 RID: 2705 RVA: 0x00028239 File Offset: 0x00026439
		public DllNotFoundException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233052;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DllNotFoundException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x06000A92 RID: 2706 RVA: 0x0002824E File Offset: 0x0002644E
		protected DllNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
