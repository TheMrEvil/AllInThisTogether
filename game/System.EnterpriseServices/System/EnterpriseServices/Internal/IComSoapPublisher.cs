using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.Internal
{
	/// <summary>Publishes COM interfaces for SOAP-enabled COM+ applications.</summary>
	// Token: 0x02000062 RID: 98
	[Guid("d8013eee-730b-45e2-ba24-874b7242c425")]
	public interface IComSoapPublisher
	{
		/// <summary>Creates a SOAP-enabled COM+ application mailbox at a specified URL. Not fully implemented.</summary>
		/// <param name="RootMailServer">The URL for the root mail server.</param>
		/// <param name="MailBox">The mailbox to create.</param>
		/// <param name="SmtpName">When this method returns, this parameter contains the name of the Simple Mail Transfer Protocol (SMTP) server containing the mailbox.</param>
		/// <param name="Domain">When this method returns, this parameter contains the domain of the SMTP server.</param>
		/// <param name="PhysicalPath">When this method returns, this parameter contains the file system path for the mailbox.</param>
		/// <param name="Error">When this method returns, this parameter contains an error message if a problem was encountered.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		// Token: 0x06000173 RID: 371
		[DispId(6)]
		void CreateMailBox([MarshalAs(UnmanagedType.BStr)] string RootMailServer, [MarshalAs(UnmanagedType.BStr)] string MailBox, [MarshalAs(UnmanagedType.BStr)] out string SmtpName, [MarshalAs(UnmanagedType.BStr)] out string Domain, [MarshalAs(UnmanagedType.BStr)] out string PhysicalPath, [MarshalAs(UnmanagedType.BStr)] out string Error);

		/// <summary>Creates a SOAP-enabled COM+ application virtual root.</summary>
		/// <param name="Operation">The operation to perform.</param>
		/// <param name="FullUrl">The complete URL address for the virtual root.</param>
		/// <param name="BaseUrl">When this method returns, this parameter contains the base URL address.</param>
		/// <param name="VirtualRoot">When this method returns, this parameter contains the name of the virtual root.</param>
		/// <param name="PhysicalPath">When this method returns, this parameter contains the file path for the virtual root.</param>
		/// <param name="Error">When this method returns, this parameter contains an error message if a problem was encountered.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.  
		///  -or-  
		///  The caller does not have permission to access Domain Name System (DNS) information.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="FullUrl" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving the local host name.</exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="FullUrl" /> is empty.  
		/// -or-  
		/// The scheme specified in <paramref name="FullUrl" /> is invalid.  
		/// -or-  
		/// <paramref name="FullUrl" /> contains more than two consecutive slashes.  
		/// -or-  
		/// The password specified in <paramref name="FullUrl" /> is invalid.  
		/// -or-  
		/// The host name specified in <paramref name="FullUrl" /> is invalid.  
		/// -or-  
		/// The file name specified in <paramref name="FullUrl" /> is invalid.</exception>
		// Token: 0x06000174 RID: 372
		[DispId(4)]
		void CreateVirtualRoot([MarshalAs(UnmanagedType.BStr)] string Operation, [MarshalAs(UnmanagedType.BStr)] string FullUrl, [MarshalAs(UnmanagedType.BStr)] out string BaseUrl, [MarshalAs(UnmanagedType.BStr)] out string VirtualRoot, [MarshalAs(UnmanagedType.BStr)] out string PhysicalPath, [MarshalAs(UnmanagedType.BStr)] out string Error);

		/// <summary>Deletes a SOAP-enabled COM+ application mailbox at a specified URL. Not fully implemented.</summary>
		/// <param name="RootMailServer">The URL for the root mail server.</param>
		/// <param name="MailBox">The mailbox to delete.</param>
		/// <param name="Error">When this method returns, this parameter contains an error message if a problem was encountered.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		// Token: 0x06000175 RID: 373
		[DispId(7)]
		void DeleteMailBox([MarshalAs(UnmanagedType.BStr)] string RootMailServer, [MarshalAs(UnmanagedType.BStr)] string MailBox, [MarshalAs(UnmanagedType.BStr)] out string Error);

		/// <summary>Deletes a SOAP-enabled COM+ application virtual root. Not fully implemented.</summary>
		/// <param name="RootWebServer">The root Web server.</param>
		/// <param name="FullUrl">The complete URL address for the virtual root.</param>
		/// <param name="Error">When this method returns, this parameter contains an error message if a problem was encountered.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		// Token: 0x06000176 RID: 374
		[DispId(5)]
		void DeleteVirtualRoot([MarshalAs(UnmanagedType.BStr)] string RootWebServer, [MarshalAs(UnmanagedType.BStr)] string FullUrl, [MarshalAs(UnmanagedType.BStr)] out string Error);

		/// <summary>Installs an assembly in the global assembly cache.</summary>
		/// <param name="AssemblyPath">The file system path for the assembly.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		// Token: 0x06000177 RID: 375
		[DispId(13)]
		void GacInstall([MarshalAs(UnmanagedType.BStr)] string AssemblyPath);

		/// <summary>Removes an assembly from the global assembly cache.</summary>
		/// <param name="AssemblyPath">The file system path for the assembly.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="AssemblyPath" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="AssemblyPath" /> is empty.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="AssemblyPath" /> is not found.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="AssemblyPath" /> is not a valid assembly.</exception>
		// Token: 0x06000178 RID: 376
		[DispId(14)]
		void GacRemove([MarshalAs(UnmanagedType.BStr)] string AssemblyPath);

		/// <summary>Returns the full path for a strong-named signed generated assembly in the SoapCache directory.</summary>
		/// <param name="TypeLibPath">The path for the file that contains the typelib.</param>
		/// <param name="CachePath">When this method returns, this parameter contains the full path of the proxy assembly in the SoapCache directory.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="TypeLibPath" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">The file name is empty, contains only white spaces, or contains invalid characters.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Access to <paramref name="TypeLibPath" /> is denied.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="TypeLibPath" /> contains a colon (:) in the middle of the string.</exception>
		// Token: 0x06000179 RID: 377
		[DispId(15)]
		void GetAssemblyNameForCache([MarshalAs(UnmanagedType.BStr)] string TypeLibPath, [MarshalAs(UnmanagedType.BStr)] out string CachePath);

		/// <summary>Reflects over an assembly and returns the type name that matches the ProgID.</summary>
		/// <param name="AssemblyPath">The file system path for the assembly.</param>
		/// <param name="ProgId">The programmatic identifier of the class.</param>
		/// <returns>The type name that matches the ProgID.</returns>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		// Token: 0x0600017A RID: 378
		[DispId(10)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string GetTypeNameFromProgId([MarshalAs(UnmanagedType.BStr)] string AssemblyPath, [MarshalAs(UnmanagedType.BStr)] string ProgId);

		/// <summary>Processes a client type library, creating a configuration file on the client.</summary>
		/// <param name="ProgId">The programmatic identifier of the class.</param>
		/// <param name="SrcTlbPath">The path for the file that contains the typelib.</param>
		/// <param name="PhysicalPath">The Web application directory.</param>
		/// <param name="VRoot">The name of the virtual root.</param>
		/// <param name="BaseUrl">The base URL that contains the virtual root.</param>
		/// <param name="Mode">The activation mode.</param>
		/// <param name="Transport">Not used. Specify <see langword="null" /> for this parameter.</param>
		/// <param name="AssemblyName">When this method returns, this parameter contains the display name of the assembly.</param>
		/// <param name="TypeName">When this method returns, this parameter contains the fully-qualified type name of the assembly.</param>
		/// <param name="Error">When this method returns, this parameter contains an error message if a problem was encountered.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		// Token: 0x0600017B RID: 379
		[DispId(9)]
		void ProcessClientTlb([MarshalAs(UnmanagedType.BStr)] string ProgId, [MarshalAs(UnmanagedType.BStr)] string SrcTlbPath, [MarshalAs(UnmanagedType.BStr)] string PhysicalPath, [MarshalAs(UnmanagedType.BStr)] string VRoot, [MarshalAs(UnmanagedType.BStr)] string BaseUrl, [MarshalAs(UnmanagedType.BStr)] string Mode, [MarshalAs(UnmanagedType.BStr)] string Transport, [MarshalAs(UnmanagedType.BStr)] out string AssemblyName, [MarshalAs(UnmanagedType.BStr)] out string TypeName, [MarshalAs(UnmanagedType.BStr)] out string Error);

		/// <summary>Processes a server type library, either adding or deleting component entries to the Web.config and Default.disco files. Generates a proxy if necessary.</summary>
		/// <param name="ProgId">The programmatic identifier of the class.</param>
		/// <param name="SrcTlbPath">The path for the file that contains the type library.</param>
		/// <param name="PhysicalPath">The Web application directory.</param>
		/// <param name="Operation">The operation to perform.</param>
		/// <param name="AssemblyName">When this method returns, this parameter contains the display name of the assembly.</param>
		/// <param name="TypeName">When this method returns, this parameter contains the fully-qualified type name of the assembly.</param>
		/// <param name="Error">When this method returns, this parameter contains an error message if a problem was encountered.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.EnterpriseServices.ServicedComponentException">The <paramref name="SrcTlbPath" /> parameter referenced scrobj.dll; therefore, SOAP publication of script components is not supported.</exception>
		// Token: 0x0600017C RID: 380
		[DispId(8)]
		void ProcessServerTlb([MarshalAs(UnmanagedType.BStr)] string ProgId, [MarshalAs(UnmanagedType.BStr)] string SrcTlbPath, [MarshalAs(UnmanagedType.BStr)] string PhysicalPath, [MarshalAs(UnmanagedType.BStr)] string Operation, [MarshalAs(UnmanagedType.BStr)] out string AssemblyName, [MarshalAs(UnmanagedType.BStr)] out string TypeName, [MarshalAs(UnmanagedType.BStr)] out string Error);

		/// <summary>Registers an assembly for COM interop.</summary>
		/// <param name="AssemblyPath">The file system path for the assembly.</param>
		/// <exception cref="T:System.EnterpriseServices.RegistrationException">The input assembly does not have a strong name.</exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.  
		///  -or-  
		///  A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="AssemblyPath" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="AssemblyPath" /> is not found, or a file name extension is not specified.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="AssemblyPath" /> is not a valid assembly.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences, or the assembly name exceeds the system-defined maximum length.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> is not <see langword="static" />.  
		///  -or-  
		///  There is more than one method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> at a given level of the hierarchy.  
		///  -or-  
		///  The signature of the method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> is not valid.</exception>
		// Token: 0x0600017D RID: 381
		[DispId(11)]
		void RegisterAssembly([MarshalAs(UnmanagedType.BStr)] string AssemblyPath);

		/// <summary>Unregisters a COM interop assembly.</summary>
		/// <param name="AssemblyPath">The file system path for the assembly.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.  
		///  -or-  
		///  A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="AssemblyPath" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="AssemblyPath" /> is not found, or a file name extension is not specified.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="AssemblyPath" /> is not a valid assembly.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences, or the assembly name exceeds the syste-defined maximum length.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> is not <see langword="static" />.  
		///  -or-  
		///  There is more than one method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> at a given level of the hierarchy.  
		///  -or-  
		///  The signature of the method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> is not valid.</exception>
		// Token: 0x0600017E RID: 382
		[DispId(12)]
		void UnRegisterAssembly([MarshalAs(UnmanagedType.BStr)] string AssemblyPath);
	}
}
