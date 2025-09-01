using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.Internal
{
	/// <summary>Publishes authenticated, encrypted SOAP virtual roots on servers. This class cannot be inherited.</summary>
	// Token: 0x0200006D RID: 109
	[Guid("CAA817CC-0C04-4d22-A05C-2B7E162F4E8F")]
	public sealed class SoapServerVRoot : ISoapServerVRoot
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.Internal.SoapServerVRoot" /> class.</summary>
		// Token: 0x060001A4 RID: 420 RVA: 0x00002078 File Offset: 0x00000278
		[MonoTODO]
		public SoapServerVRoot()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a SOAP virtual root with security options.</summary>
		/// <param name="rootWebServer">The root Web server. The default is "IIS://localhost/W3SVC/1/ROOT".</param>
		/// <param name="inBaseUrl">The base URL that contains the virtual root.</param>
		/// <param name="inVirtualRoot">The name of the virtual root.</param>
		/// <param name="homePage">The URL of the home page.</param>
		/// <param name="discoFile">
		///   <see langword="true" /> if a default discovery file is to be created; <see langword="false" /> if there is to be no discovery file. If <see langword="false" /> and a Default.disco file exists, the file is deleted.</param>
		/// <param name="secureSockets">
		///   <see langword="true" /> if SSL encryption is required; otherwise, <see langword="false" />.</param>
		/// <param name="authentication">Specify "anonymous" if no authentication is to be used (anonymous user). Otherwise, specify an empty string.</param>
		/// <param name="operation">Not used. Specify <see langword="null" /> for this parameter.</param>
		/// <param name="baseUrl">When this method returns, this parameter contains the base URL.</param>
		/// <param name="virtualRoot">When this method returns, this parameter contains the name of the virtual root.</param>
		/// <param name="physicalPath">When this method returns, this parameter contains the disk address of the Virtual Root directory.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The SOAP utility is not available.</exception>
		// Token: 0x060001A5 RID: 421 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void CreateVirtualRootEx(string rootWebServer, string inBaseUrl, string inVirtualRoot, string homePage, string discoFile, string secureSockets, string authentication, string operation, out string baseUrl, out string virtualRoot, out string physicalPath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Deletes a virtual root. Not fully implemented.</summary>
		/// <param name="rootWebServer">The root Web server. The default is "IIS://localhost/W3SVC/1/ROOT".</param>
		/// <param name="inBaseUrl">The base URL that contains the virtual root.</param>
		/// <param name="inVirtualRoot">The name of the virtual root.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The SOAP utility is not available.</exception>
		/// <exception cref="T:System.EnterpriseServices.ServicedComponentException">The call to get the system directory failed.</exception>
		// Token: 0x060001A6 RID: 422 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void DeleteVirtualRootEx(string rootWebServer, string inBaseUrl, string inVirtualRoot)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the security status of an existing SOAP virtual root.</summary>
		/// <param name="RootWebServer">The root Web server. The default is "IIS://localhost/W3SVC/1/ROOT".</param>
		/// <param name="inBaseUrl">The base URL that contains the virtual root.</param>
		/// <param name="inVirtualRoot">The name of the virtual root.</param>
		/// <param name="Exists">When this method returns, this parameter contains a <see langword="true" /> if the virtual directory exists; otherwise, <see langword="false" />.</param>
		/// <param name="SSL">When this method returns, this parameter contains a <see langword="true" /> if SSL encryption is required; otherwise, <see langword="false" />.</param>
		/// <param name="WindowsAuth">When this method returns, this parameter contains <see langword="true" /> if Windows authentication is set, otherwise, <see langword="false" />.</param>
		/// <param name="Anonymous">When this method returns, this parameter contains <see langword="true" /> if no authentication is set (anonymous user); otherwise, <see langword="false" />.</param>
		/// <param name="HomePage">When this method returns, this parameter contains a <see langword="true" /> if the Virtual Root's <see langword="EnableDefaultDoc" /> property is set; otherwise, <see langword="false" />.</param>
		/// <param name="DiscoFile">When this method returns, this parameter contains a <see langword="true" /> if a Default.disco file exists; otherwise, <see langword="false" />.</param>
		/// <param name="PhysicalPath">When this method returns, this parameter contains the disk address of the virtual root directory.</param>
		/// <param name="BaseUrl">When this method returns, this parameter contains the base URL.</param>
		/// <param name="VirtualRoot">When this method returns, this parameter contains the name of the virtual root.</param>
		// Token: 0x060001A7 RID: 423 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void GetVirtualRootStatus(string RootWebServer, string inBaseUrl, string inVirtualRoot, out string Exists, out string SSL, out string WindowsAuth, out string Anonymous, out string HomePage, out string DiscoFile, out string PhysicalPath, out string BaseUrl, out string VirtualRoot)
		{
			throw new NotImplementedException();
		}
	}
}
