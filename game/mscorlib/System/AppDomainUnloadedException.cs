using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt is made to access an unloaded application domain.</summary>
	// Token: 0x020001C7 RID: 455
	[Serializable]
	public class AppDomainUnloadedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainUnloadedException" /> class.</summary>
		// Token: 0x060013A5 RID: 5029 RVA: 0x0004E4A5 File Offset: 0x0004C6A5
		public AppDomainUnloadedException() : base("Attempted to access an unloaded AppDomain.")
		{
			base.HResult = -2146234348;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainUnloadedException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x060013A6 RID: 5030 RVA: 0x0004E4BD File Offset: 0x0004C6BD
		public AppDomainUnloadedException(string message) : base(message)
		{
			base.HResult = -2146234348;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainUnloadedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060013A7 RID: 5031 RVA: 0x0004E4D1 File Offset: 0x0004C6D1
		public AppDomainUnloadedException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146234348;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainUnloadedException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x060013A8 RID: 5032 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected AppDomainUnloadedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04001446 RID: 5190
		internal const int COR_E_APPDOMAINUNLOADED = -2146234348;
	}
}
