using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>The exception that is thrown when an error is detected in a serviced component.</summary>
	// Token: 0x02000049 RID: 73
	[ComVisible(false)]
	[Serializable]
	public sealed class ServicedComponentException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ServicedComponentException" /> class.</summary>
		// Token: 0x0600013A RID: 314 RVA: 0x000025B5 File Offset: 0x000007B5
		public ServicedComponentException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ServicedComponentException" /> class with a specified error message.</summary>
		/// <param name="message">The message displayed to the client when the exception is thrown.</param>
		// Token: 0x0600013B RID: 315 RVA: 0x000024D4 File Offset: 0x000006D4
		public ServicedComponentException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ServicedComponentException" /> class.</summary>
		/// <param name="message">The message displayed to the client when the exception is thrown.</param>
		/// <param name="innerException">The <see cref="P:System.Exception.InnerException" />, if any, that threw the current exception.</param>
		// Token: 0x0600013C RID: 316 RVA: 0x000024EA File Offset: 0x000006EA
		public ServicedComponentException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
