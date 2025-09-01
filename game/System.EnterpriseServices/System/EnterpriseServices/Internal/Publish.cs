using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.Internal
{
	/// <summary>Publishes COM interfaces for SOAP-enabled COM+ applications.</summary>
	// Token: 0x02000069 RID: 105
	[Guid("d8013eef-730b-45e2-ba24-874b7242c425")]
	public class Publish : IComSoapPublisher
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.Internal.Publish" /> class.</summary>
		// Token: 0x0600018D RID: 397 RVA: 0x00002078 File Offset: 0x00000278
		[MonoTODO]
		public Publish()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a SOAP-enabled COM+ application mailbox at a specified URL. Not fully implemented.</summary>
		/// <param name="RootMailServer">The URL for the root mail server.</param>
		/// <param name="MailBox">The mailbox to create.</param>
		/// <param name="SmtpName">When this method returns, this parameter contains the name of the Simple Mail Transfer Protocol (SMTP) server containing the mailbox.</param>
		/// <param name="Domain">When this method returns, this parameter contains the domain of the SMTP server.</param>
		/// <param name="PhysicalPath">When this method returns, this parameter contains the file system path for the mailbox.</param>
		/// <param name="Error">When this method returns, this parameter contains an error message if a problem was encountered.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		// Token: 0x0600018E RID: 398 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void CreateMailBox(string RootMailServer, string MailBox, out string SmtpName, out string Domain, out string PhysicalPath, out string Error)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a SOAP-enabled COM+ application virtual root.</summary>
		/// <param name="Operation">The operation to perform.</param>
		/// <param name="FullUrl">The complete URL address for the virtual root.</param>
		/// <param name="BaseUrl">When this method returns, this parameter contains the base URL address.</param>
		/// <param name="VirtualRoot">When this method returns, this parameter contains the name of the virtual root.</param>
		/// <param name="PhysicalPath">When this method returns, this parameter contains the file path for the virtual root.</param>
		/// <param name="Error">When this method returns, this parameter contains an error message if a problem was encountered.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.  
		///  -or-  
		///  The caller does not have permission to access DNS information.</exception>
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
		// Token: 0x0600018F RID: 399 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void CreateVirtualRoot(string Operation, string FullUrl, out string BaseUrl, out string VirtualRoot, out string PhysicalPath, out string Error)
		{
			throw new NotImplementedException();
		}

		/// <summary>Deletes a SOAP-enabled COM+ application mailbox at a specified URL. Not fully implemented.</summary>
		/// <param name="RootMailServer">The URL for the root mail server.</param>
		/// <param name="MailBox">The mailbox to delete.</param>
		/// <param name="Error">When this method returns, this parameter contains an error message if a problem was encountered.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		// Token: 0x06000190 RID: 400 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void DeleteMailBox(string RootMailServer, string MailBox, out string Error)
		{
			throw new NotImplementedException();
		}

		/// <summary>Deletes a SOAP-enabled COM+ application virtual root. Not fully implemented.</summary>
		/// <param name="RootWebServer">The root Web server.</param>
		/// <param name="FullUrl">The complete URL address for the virtual root.</param>
		/// <param name="Error">When this method returns, this parameter contains an error message if a problem was encountered.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		// Token: 0x06000191 RID: 401 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void DeleteVirtualRoot(string RootWebServer, string FullUrl, out string Error)
		{
			throw new NotImplementedException();
		}

		/// <summary>Installs an assembly in the global assembly cache.</summary>
		/// <param name="AssemblyPath">The file system path for the assembly.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		// Token: 0x06000192 RID: 402 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void GacInstall(string AssemblyPath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes an assembly from the global assembly cache.</summary>
		/// <param name="AssemblyPath">The file system path for the assembly.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.  
		///  -or-  
		///  The caller does not have path discovery permission.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="AssemblyPath" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="AssemblyPath" /> is empty.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="AssemblyPath" /> is not found.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="AssemblyPath" /> is not a valid assembly.</exception>
		// Token: 0x06000193 RID: 403 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void GacRemove(string AssemblyPath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the full path for a strong-named signed generated assembly in the SoapCache directory.</summary>
		/// <param name="TypeLibPath">The path for the file that contains the typelib.</param>
		/// <param name="CachePath">When this method returns, this parameter contains the name of the SoapCache directory.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="TypeLibPath" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">The file name is empty, contains only white spaces, or contains invalid characters.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Access to <paramref name="TypeLibPath" /> is denied.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="TypeLibPath" /> contains a colon (:) in the middle of the string.</exception>
		// Token: 0x06000194 RID: 404 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void GetAssemblyNameForCache(string TypeLibPath, out string CachePath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the path for the directory for storing client configuration files.</summary>
		/// <param name="CreateDir">Set to <see langword="true" /> to create the directory, or <see langword="false" /> to return the path but not create the directory.</param>
		/// <returns>The path for the directory to contain the configuration files.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		// Token: 0x06000195 RID: 405 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public static string GetClientPhysicalPath(bool CreateDir)
		{
			throw new NotImplementedException();
		}

		/// <summary>Reflects over an assembly and returns the type name that matches the ProgID.</summary>
		/// <param name="AssemblyPath">The file system path for the assembly.</param>
		/// <param name="ProgId">The programmatic identifier of the class.</param>
		/// <returns>The type name that matches the ProgID.</returns>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		// Token: 0x06000196 RID: 406 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public string GetTypeNameFromProgId(string AssemblyPath, string ProgId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Parses a URL and returns the base URL and virtual root portions.</summary>
		/// <param name="FullUrl">The complete URL address for the virtual root.</param>
		/// <param name="BaseUrl">When this method returns, this parameter contains the base URL address.</param>
		/// <param name="VirtualRoot">When this method returns, this parameter contains the name of the virtual root.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="FullUrl" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving the local host name.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have permission to access DNS information.</exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="FullUrl" /> is empty.  
		/// -or-  
		/// The scheme specified in <paramref name="FullUrl" /> is invalid.  
		/// -or-  
		/// <paramref name="FullUrl" /> contains too many slashes.  
		/// -or-  
		/// The password specified in <paramref name="FullUrl" /> is invalid.  
		/// -or-  
		/// The host name specified in <paramref name="FullUrl" /> is invalid.  
		/// -or-  
		/// The file name specified in <paramref name="FullUrl" /> is invalid.</exception>
		// Token: 0x06000197 RID: 407 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public static void ParseUrl(string FullUrl, out string BaseUrl, out string VirtualRoot)
		{
			throw new NotImplementedException();
		}

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
		// Token: 0x06000198 RID: 408 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void ProcessClientTlb(string ProgId, string SrcTlbPath, string PhysicalPath, string VRoot, string BaseUrl, string Mode, string Transport, out string AssemblyName, out string TypeName, out string Error)
		{
			throw new NotImplementedException();
		}

		/// <summary>Processes a server type library, either adding or deleting component entries to the Web.config and Default.disco files. Generates a proxy if necessary.</summary>
		/// <param name="ProgId">The programmatic identifier of the class.</param>
		/// <param name="SrcTlbPath">The path for the file that contains the type library.</param>
		/// <param name="PhysicalPath">The Web application directory.</param>
		/// <param name="Operation">The operation to perform.</param>
		/// <param name="strAssemblyName">When this method returns, this parameter contains the display name of the assembly.</param>
		/// <param name="TypeName">When this method returns, this parameter contains the fully-qualified type name of the assembly.</param>
		/// <param name="Error">When this method returns, this parameter contains an error message if a problem was encountered.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.EnterpriseServices.ServicedComponentException">The <paramref name="SrcTlbPath" /> parameter referenced scrobj.dll; therefore, SOAP publication of script components is not supported.</exception>
		// Token: 0x06000199 RID: 409 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void ProcessServerTlb(string ProgId, string SrcTlbPath, string PhysicalPath, string Operation, out string strAssemblyName, out string TypeName, out string Error)
		{
			throw new NotImplementedException();
		}

		/// <summary>Registers an assembly for COM interop.</summary>
		/// <param name="AssemblyPath">The file system path for the assembly.</param>
		/// <exception cref="T:System.EnterpriseServices.RegistrationException">The input assembly does not have a strong name.</exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.  
		///  -or-  
		///  A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="AssemblyPath" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="AssemblyPath" /> is not found, or a filename extension is not specified.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="AssemblyPath" /> is not a valid assembly.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences, or the assembly name exceeds the system-defined maximum length.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> is not <see langword="static" />.  
		///  -or-  
		///  There is more than one method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> at a given level of the hierarchy.  
		///  -or-  
		///  The signature of the method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> is not valid.</exception>
		// Token: 0x0600019A RID: 410 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void RegisterAssembly(string AssemblyPath)
		{
			throw new NotImplementedException();
		}

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
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences, or the assembly name exceeds the system-defined maximum length.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> is not <see langword="static" />.  
		///  -or-  
		///  There is more than one method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> at a given level of the hierarchy.  
		///  -or-  
		///  The signature of the method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> is not valid.</exception>
		// Token: 0x0600019B RID: 411 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void UnRegisterAssembly(string AssemblyPath)
		{
			throw new NotImplementedException();
		}
	}
}
