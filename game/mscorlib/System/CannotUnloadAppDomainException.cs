using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt to unload an application domain fails.</summary>
	// Token: 0x020001C9 RID: 457
	[Serializable]
	public class CannotUnloadAppDomainException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CannotUnloadAppDomainException" /> class.</summary>
		// Token: 0x060013B5 RID: 5045 RVA: 0x0004E7CC File Offset: 0x0004C9CC
		public CannotUnloadAppDomainException() : base("Attempt to unload the AppDomain failed.")
		{
			base.HResult = -2146234347;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CannotUnloadAppDomainException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		// Token: 0x060013B6 RID: 5046 RVA: 0x0004E7E4 File Offset: 0x0004C9E4
		public CannotUnloadAppDomainException(string message) : base(message)
		{
			base.HResult = -2146234347;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CannotUnloadAppDomainException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060013B7 RID: 5047 RVA: 0x0004E7F8 File Offset: 0x0004C9F8
		public CannotUnloadAppDomainException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146234347;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CannotUnloadAppDomainException" /> class from serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x060013B8 RID: 5048 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected CannotUnloadAppDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0400144C RID: 5196
		internal const int COR_E_CANNOTUNLOADAPPDOMAIN = -2146234347;
	}
}
