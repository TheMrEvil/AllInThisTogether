using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to read or write protected memory.</summary>
	// Token: 0x020000DB RID: 219
	[Serializable]
	public class AccessViolationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.AccessViolationException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x0600068E RID: 1678 RVA: 0x00020A24 File Offset: 0x0001EC24
		public AccessViolationException() : base("Attempted to read or write protected memory. This is often an indication that other memory is corrupt.")
		{
			base.HResult = -2147467261;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AccessViolationException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x0600068F RID: 1679 RVA: 0x00020A3C File Offset: 0x0001EC3C
		public AccessViolationException(string message) : base(message)
		{
			base.HResult = -2147467261;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AccessViolationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000690 RID: 1680 RVA: 0x00020A50 File Offset: 0x0001EC50
		public AccessViolationException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AccessViolationException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x06000691 RID: 1681 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected AccessViolationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04001044 RID: 4164
		private IntPtr _ip;

		// Token: 0x04001045 RID: 4165
		private IntPtr _target;

		// Token: 0x04001046 RID: 4166
		private int _accessType;
	}
}
