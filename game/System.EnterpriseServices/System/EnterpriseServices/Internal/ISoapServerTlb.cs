using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.Internal
{
	/// <summary>Processes authenticated, encrypted SOAP components on servers.</summary>
	// Token: 0x02000066 RID: 102
	[Guid("1E7BA9F7-21DB-4482-929E-21BDE2DFE51C")]
	public interface ISoapServerTlb
	{
		/// <summary>Adds the entries for a server type library to the Web.config and Default.disco files, depending on security options, and generates a proxy if necessary.</summary>
		/// <param name="progId">The programmatic identifier of the class.</param>
		/// <param name="classId">The class identifier (CLSID) for the type library.</param>
		/// <param name="interfaceId">The IID for the type library.</param>
		/// <param name="srcTlbPath">The path for the file containing the type library.</param>
		/// <param name="rootWebServer">The root Web server.</param>
		/// <param name="baseUrl">The base URL that contains the virtual root.</param>
		/// <param name="virtualRoot">The name of the virtual root.</param>
		/// <param name="clientActivated">
		///   <see langword="true" /> if client activated; otherwise, <see langword="false" />.</param>
		/// <param name="wellKnown">
		///   <see langword="true" /> if well-known; otherwise, <see langword="false" />.</param>
		/// <param name="discoFile">
		///   <see langword="true" /> if a discovery file; otherwise, <see langword="false" />.</param>
		/// <param name="operation">The operation to perform. Specify either "delete" or an empty string.</param>
		/// <param name="assemblyName">When this method returns, contains the name of the assembly.</param>
		/// <param name="typeName">When this method returns, contains the type of the assembly.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The SOAP utility is not available.</exception>
		/// <exception cref="T:System.EnterpriseServices.ServicedComponentException">The call to get the system directory failed.</exception>
		// Token: 0x06000185 RID: 389
		[DispId(1)]
		void AddServerTlb([MarshalAs(UnmanagedType.BStr)] string progId, [MarshalAs(UnmanagedType.BStr)] string classId, [MarshalAs(UnmanagedType.BStr)] string interfaceId, [MarshalAs(UnmanagedType.BStr)] string srcTlbPath, [MarshalAs(UnmanagedType.BStr)] string rootWebServer, [MarshalAs(UnmanagedType.BStr)] string baseUrl, [MarshalAs(UnmanagedType.BStr)] string virtualRoot, [MarshalAs(UnmanagedType.BStr)] string clientActivated, [MarshalAs(UnmanagedType.BStr)] string wellKnown, [MarshalAs(UnmanagedType.BStr)] string discoFile, [MarshalAs(UnmanagedType.BStr)] string operation, [MarshalAs(UnmanagedType.BStr)] out string assemblyName, [MarshalAs(UnmanagedType.BStr)] out string typeName);

		/// <summary>Removes entries for a server type library from the Web.config and Default.disco files, depending on security options.</summary>
		/// <param name="progId">The programmatic identifier of the class.</param>
		/// <param name="classId">The class identifier (CLSID) for the type library.</param>
		/// <param name="interfaceId">The IID for the type library.</param>
		/// <param name="srcTlbPath">The path for the file containing the type library.</param>
		/// <param name="rootWebServer">The root Web server.</param>
		/// <param name="baseUrl">The base URL that contains the virtual root.</param>
		/// <param name="virtualRoot">The name of the virtual root.</param>
		/// <param name="operation">Not used. Specify <see langword="null" /> for this parameter.</param>
		/// <param name="assemblyName">The name of the assembly.</param>
		/// <param name="typeName">The type of the assembly.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The SOAP utility is not available.</exception>
		// Token: 0x06000186 RID: 390
		[DispId(2)]
		void DeleteServerTlb([MarshalAs(UnmanagedType.BStr)] string progId, [MarshalAs(UnmanagedType.BStr)] string classId, [MarshalAs(UnmanagedType.BStr)] string interfaceId, [MarshalAs(UnmanagedType.BStr)] string srcTlbPath, [MarshalAs(UnmanagedType.BStr)] string rootWebServer, [MarshalAs(UnmanagedType.BStr)] string baseUrl, [MarshalAs(UnmanagedType.BStr)] string virtualRoot, [MarshalAs(UnmanagedType.BStr)] string operation, [MarshalAs(UnmanagedType.BStr)] string assemblyName, [MarshalAs(UnmanagedType.BStr)] string typeName);
	}
}
