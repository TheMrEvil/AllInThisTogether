using System;
using System.Runtime.Serialization;

namespace System.EnterpriseServices
{
	/// <summary>The exception that is thrown when a registration error is detected.</summary>
	// Token: 0x0200003C RID: 60
	[Serializable]
	public sealed class RegistrationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.RegistrationException" /> class with a specified error message.</summary>
		/// <param name="msg">The message displayed to the client when the exception is thrown.</param>
		// Token: 0x060000C5 RID: 197 RVA: 0x000024D4 File Offset: 0x000006D4
		[MonoTODO]
		public RegistrationException(string msg) : base(msg)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.RegistrationException" /> class.</summary>
		// Token: 0x060000C6 RID: 198 RVA: 0x000024DD File Offset: 0x000006DD
		public RegistrationException() : this("Registration error")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.RegistrationException" /> class with a specified error message and nested exception.</summary>
		/// <param name="msg">The message displayed to the client when the exception is thrown.</param>
		/// <param name="inner">The nested exception.</param>
		// Token: 0x060000C7 RID: 199 RVA: 0x000024EA File Offset: 0x000006EA
		public RegistrationException(string msg, Exception inner) : base(msg, inner)
		{
		}

		/// <summary>Gets an array of <see cref="T:System.EnterpriseServices.RegistrationErrorInfo" /> objects that describe registration errors.</summary>
		/// <returns>The array of <see cref="T:System.EnterpriseServices.RegistrationErrorInfo" /> objects.</returns>
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000024F4 File Offset: 0x000006F4
		public RegistrationErrorInfo[] ErrorInfo
		{
			get
			{
				return this.errorInfo;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the error information in <see cref="T:System.EnterpriseServices.RegistrationErrorInfo" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains serialized object data.</param>
		/// <param name="ctx">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="info" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060000C9 RID: 201 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public override void GetObjectData(SerializationInfo info, StreamingContext ctx)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000082 RID: 130
		private RegistrationErrorInfo[] errorInfo;
	}
}
