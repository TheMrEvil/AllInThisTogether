using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.Internal
{
	/// <summary>Publishes authenticated, encrypted SOAP virtual roots on servers.</summary>
	// Token: 0x02000067 RID: 103
	[Guid("A31B6577-71D2-4344-AEDF-ADC1B0DC5347")]
	public interface ISoapServerVRoot
	{
		/// <summary>Creates a SOAP virtual root with security options.</summary>
		/// <param name="rootWebServer">The root Web server.</param>
		/// <param name="inBaseUrl">The base URL that contains the virtual root.</param>
		/// <param name="inVirtualRoot">The name of the virtual root.</param>
		/// <param name="homePage">
		///   <see langword="true" /> if the <see langword="EnableDefaultDoc" /> property is to be set; otherwise, <see langword="false" />.</param>
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
		// Token: 0x06000187 RID: 391
		[DispId(1)]
		void CreateVirtualRootEx([MarshalAs(UnmanagedType.BStr)] string rootWebServer, [MarshalAs(UnmanagedType.BStr)] string inBaseUrl, [MarshalAs(UnmanagedType.BStr)] string inVirtualRoot, [MarshalAs(UnmanagedType.BStr)] string homePage, [MarshalAs(UnmanagedType.BStr)] string discoFile, [MarshalAs(UnmanagedType.BStr)] string secureSockets, [MarshalAs(UnmanagedType.BStr)] string authentication, [MarshalAs(UnmanagedType.BStr)] string operation, [MarshalAs(UnmanagedType.BStr)] out string baseUrl, [MarshalAs(UnmanagedType.BStr)] out string virtualRoot, [MarshalAs(UnmanagedType.BStr)] out string physicalPath);

		/// <summary>Deletes a virtual root. Not fully implemented.</summary>
		/// <param name="rootWebServer">The root Web server.</param>
		/// <param name="baseUrl">The base URL that contains the virtual root.</param>
		/// <param name="virtualRoot">The name of the virtual root.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The SOAP utility is not available.</exception>
		/// <exception cref="T:System.EnterpriseServices.ServicedComponentException">The call to identify the system directory failed.</exception>
		// Token: 0x06000188 RID: 392
		[DispId(2)]
		void DeleteVirtualRootEx([MarshalAs(UnmanagedType.BStr)] string rootWebServer, [MarshalAs(UnmanagedType.BStr)] string baseUrl, [MarshalAs(UnmanagedType.BStr)] string virtualRoot);

		/// <summary>Returns the security status of an existing SOAP virtual root.</summary>
		/// <param name="rootWebServer">The root Web server.</param>
		/// <param name="inBaseUrl">The base URL that contains the virtual root.</param>
		/// <param name="inVirtualRoot">The name of the virtual root.</param>
		/// <param name="exists">When this method returns, this parameter contains a <see langword="true" /> if the virtual directory exists; otherwise, <see langword="false" />.</param>
		/// <param name="secureSockets">When this method returns, this parameter contains a <see langword="true" /> if SSL encryption is required; otherwise, <see langword="false" />.</param>
		/// <param name="windowsAuth">When this method returns, this parameter contains <see langword="true" /> if Windows authentication is set, otherwise, <see langword="false" />.</param>
		/// <param name="anonymous">When this method returns, this parameter contains <see langword="true" /> if no authentication is set (anonymous user); otherwise, <see langword="false" />.</param>
		/// <param name="homePage">When this method returns, this parameter contains a <see langword="true" /> if the Virtual Root directory's <see langword="EnableDefaultDoc" /> property is set; otherwise, <see langword="false" />.</param>
		/// <param name="discoFile">When this method returns, this parameter contains a <see langword="true" /> if a Default.disco file exists; otherwise, <see langword="false" />.</param>
		/// <param name="physicalPath">When this method returns, this parameter contains the disk address of the Virtual Root directory.</param>
		/// <param name="baseUrl">When this method returns, this parameter contains the base URL.</param>
		/// <param name="virtualRoot">When this method returns, this parameter contains the name of the virtual root.</param>
		// Token: 0x06000189 RID: 393
		[DispId(3)]
		void GetVirtualRootStatus([MarshalAs(UnmanagedType.BStr)] string rootWebServer, [MarshalAs(UnmanagedType.BStr)] string inBaseUrl, [MarshalAs(UnmanagedType.BStr)] string inVirtualRoot, [MarshalAs(UnmanagedType.BStr)] out string exists, [MarshalAs(UnmanagedType.BStr)] out string secureSockets, [MarshalAs(UnmanagedType.BStr)] out string windowsAuth, [MarshalAs(UnmanagedType.BStr)] out string anonymous, [MarshalAs(UnmanagedType.BStr)] out string homePage, [MarshalAs(UnmanagedType.BStr)] out string discoFile, [MarshalAs(UnmanagedType.BStr)] out string physicalPath, [MarshalAs(UnmanagedType.BStr)] out string baseUrl, [MarshalAs(UnmanagedType.BStr)] out string virtualRoot);
	}
}
