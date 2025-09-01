using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.Internal
{
	/// <summary>Provides utilities to support the exporting of COM+ SOAP-enabled application proxies by the server and the importing of the proxies by the client.</summary>
	// Token: 0x02000068 RID: 104
	[Guid("5AC4CB7E-F89F-429b-926B-C7F940936BF4")]
	public interface ISoapUtility
	{
		/// <summary>Returns the path for the SOAP virtual root bin directory.</summary>
		/// <param name="rootWebServer">The root Web server.</param>
		/// <param name="inBaseUrl">The base URL address.</param>
		/// <param name="inVirtualRoot">The name of the virtual root.</param>
		/// <param name="binPath">When this method returns, this parameter contains the file path for the SOAP virtual root bin directory.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The SOAP utility is not available.</exception>
		/// <exception cref="T:System.EnterpriseServices.ServicedComponentException">The call to get the system directory failed.</exception>
		// Token: 0x0600018A RID: 394
		[DispId(2)]
		void GetServerBinPath([MarshalAs(UnmanagedType.BStr)] string rootWebServer, [MarshalAs(UnmanagedType.BStr)] string inBaseUrl, [MarshalAs(UnmanagedType.BStr)] string inVirtualRoot, [MarshalAs(UnmanagedType.BStr)] out string binPath);

		/// <summary>Returns the path for the SOAP virtual root.</summary>
		/// <param name="rootWebServer">The root Web server.</param>
		/// <param name="inBaseUrl">The base URL address.</param>
		/// <param name="inVirtualRoot">The name of the virtual root.</param>
		/// <param name="physicalPath">When this method returns, this parameter contains the file path for the SOAP virtual root.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The SOAP utility is not available.</exception>
		/// <exception cref="T:System.EnterpriseServices.ServicedComponentException">The call to get the system directory failed.</exception>
		// Token: 0x0600018B RID: 395
		[DispId(1)]
		void GetServerPhysicalPath([MarshalAs(UnmanagedType.BStr)] string rootWebServer, [MarshalAs(UnmanagedType.BStr)] string inBaseUrl, [MarshalAs(UnmanagedType.BStr)] string inVirtualRoot, [MarshalAs(UnmanagedType.BStr)] out string physicalPath);

		/// <summary>Determines whether authenticated, encrypted SOAP interfaces are present.</summary>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The SOAP utility is not available.</exception>
		// Token: 0x0600018C RID: 396
		[DispId(3)]
		void Present();
	}
}
