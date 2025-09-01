using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.Internal
{
	/// <summary>Activates SOAP-enabled COM+ application proxies from a client.</summary>
	// Token: 0x0200005E RID: 94
	[Guid("ecabafd2-7f19-11d2-978e-0000f8757e2a")]
	public interface IClrObjectFactory
	{
		/// <summary>Activates a remote assembly through .NET remoting, using the assembly's configuration file.</summary>
		/// <param name="assembly">The name of the assembly to activate.</param>
		/// <param name="type">The name of the type to activate.</param>
		/// <param name="mode">Not used.</param>
		/// <returns>An instance of the <see cref="T:System.Object" /> representing the type, with culture, arguments, and binding and activation attributes set to <see langword="null" />, or <see langword="null" /> if the <paramref name="type" /> parameter is not found.</returns>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The class is not registered.</exception>
		// Token: 0x06000169 RID: 361
		[DispId(1)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object CreateFromAssembly(string assembly, string type, string mode);

		/// <summary>Activates a remote assembly through .NET remoting, using the remote assembly's mailbox. Currently not implemented; throws a <see cref="T:System.Runtime.InteropServices.COMException" /> if called.</summary>
		/// <param name="Mailbox">A mailbox on the Web service.</param>
		/// <param name="Mode">Not used.</param>
		/// <returns>This method throws an exception if called.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">Simple Mail Transfer Protocol (SMTP) is not implemented.</exception>
		// Token: 0x0600016A RID: 362
		[DispId(4)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object CreateFromMailbox(string Mailbox, string Mode);

		/// <summary>Activates a remote assembly through .NET remoting, using the virtual root URL of the remote assembly.</summary>
		/// <param name="VrootUrl">The virtual root URL of the remote object.</param>
		/// <param name="Mode">Not used.</param>
		/// <returns>An instance of the <see cref="T:System.Object" /> representing the type, with culture, arguments, and binding and activation attributes set to <see langword="null" />, or <see langword="null" /> if the assembly identified by the <paramref name="VrootUrl" /> parameter is not found.</returns>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The thread token could not be opened.</exception>
		// Token: 0x0600016B RID: 363
		[DispId(2)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object CreateFromVroot(string VrootUrl, string Mode);

		/// <summary>Activates a remote assembly through .NET remoting, using the Web Services Description Language (WSDL) of the XML Web service.</summary>
		/// <param name="WsdlUrl">The WSDL URL of the Web service.</param>
		/// <param name="Mode">Not used.</param>
		/// <returns>An instance of the <see cref="T:System.Object" /> representing the type, with culture, arguments, and binding and activation attributes set to <see langword="null" />, or <see langword="null" /> if the assembly identified by the <paramref name="WsdlUrl" /> parameter is not found.</returns>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The thread token could not be opened.</exception>
		// Token: 0x0600016C RID: 364
		[DispId(3)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object CreateFromWsdl(string WsdlUrl, string Mode);
	}
}
