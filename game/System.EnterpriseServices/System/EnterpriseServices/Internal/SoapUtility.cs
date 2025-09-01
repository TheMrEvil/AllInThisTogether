using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.Internal
{
	/// <summary>Provides utilities to support the exporting of COM+ SOAP-enabled application proxies by the server and the importing of the proxies by the client. This class cannot be inherited.</summary>
	// Token: 0x0200006E RID: 110
	[Guid("5F9A955F-AA55-4127-A32B-33496AA8A44E")]
	public sealed class SoapUtility : ISoapUtility
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.Internal.SoapUtility" /> class.</summary>
		// Token: 0x060001A8 RID: 424 RVA: 0x00002078 File Offset: 0x00000278
		[MonoTODO]
		public SoapUtility()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the path for the SOAP bin directory.</summary>
		/// <param name="rootWebServer">The root Web server.</param>
		/// <param name="inBaseUrl">The base URL address.</param>
		/// <param name="inVirtualRoot">The name of the virtual root.</param>
		/// <param name="binPath">When this method returns, this parameter contains the file path for the SOAP virtual root bin directory.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The SOAP utility is not available.</exception>
		/// <exception cref="T:System.EnterpriseServices.ServicedComponentException">The call to get the system directory failed.</exception>
		// Token: 0x060001A9 RID: 425 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void GetServerBinPath(string rootWebServer, string inBaseUrl, string inVirtualRoot, out string binPath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the path for the SOAP virtual root.</summary>
		/// <param name="rootWebServer">The root Web server.</param>
		/// <param name="inBaseUrl">The base URL address.</param>
		/// <param name="inVirtualRoot">The name of the virtual root.</param>
		/// <param name="physicalPath">When this method returns, this parameter contains the file path for the SOAP virtual root.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The SOAP utility is not available.</exception>
		/// <exception cref="T:System.EnterpriseServices.ServicedComponentException">The call to get the system directory failed.</exception>
		// Token: 0x060001AA RID: 426 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void GetServerPhysicalPath(string rootWebServer, string inBaseUrl, string inVirtualRoot, out string physicalPath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether authenticated, encrypted SOAP interfaces are present.</summary>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The SOAP utility is not available.</exception>
		// Token: 0x060001AB RID: 427 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void Present()
		{
			throw new NotImplementedException();
		}
	}
}
