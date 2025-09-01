using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.Internal
{
	/// <summary>Imports authenticated, encrypted SOAP client proxies. This class cannot be inherited.</summary>
	// Token: 0x0200006B RID: 107
	[Guid("346D5B9F-45E1-45c0-AADF-1B7D221E9063")]
	public sealed class SoapClientImport : ISoapClientImport
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.Internal.SoapClientImport" /> class.</summary>
		// Token: 0x0600019F RID: 415 RVA: 0x00002078 File Offset: 0x00000278
		[MonoTODO]
		public SoapClientImport()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a .NET remoting client configuration file that includes security and authentication options.</summary>
		/// <param name="progId">The programmatic identifier of the class. If an empty string (""), this method returns without doing anything.</param>
		/// <param name="virtualRoot">The name of the virtual root.</param>
		/// <param name="baseUrl">The base URL that contains the virtual root.</param>
		/// <param name="authentication">The type of ASP.NET authentication to use.</param>
		/// <param name="assemblyName">The name of the assembly.</param>
		/// <param name="typeName">The name of the type.</param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x060001A0 RID: 416 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void ProcessClientTlbEx(string progId, string virtualRoot, string baseUrl, string authentication, string assemblyName, string typeName)
		{
			throw new NotImplementedException();
		}
	}
}
