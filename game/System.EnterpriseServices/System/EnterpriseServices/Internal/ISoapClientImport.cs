using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.Internal
{
	/// <summary>Imports authenticated, encrypted SOAP client proxies.</summary>
	// Token: 0x02000065 RID: 101
	[Guid("E7F0F021-9201-47e4-94DA-1D1416DEC27A")]
	public interface ISoapClientImport
	{
		/// <summary>Creates a .NET remoting client configuration file that includes security and authentication options.</summary>
		/// <param name="progId">The programmatic identifier of the class. If an empty string (""), this method returns without doing anything.</param>
		/// <param name="virtualRoot">The name of the virtual root.</param>
		/// <param name="baseUrl">The base URL that contains the virtual root.</param>
		/// <param name="authentication">The type of ASP.NET authentication to use.</param>
		/// <param name="assemblyName">The name of the assembly.</param>
		/// <param name="typeName">The name of the type.</param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06000184 RID: 388
		[DispId(1)]
		void ProcessClientTlbEx([MarshalAs(UnmanagedType.BStr)] string progId, [MarshalAs(UnmanagedType.BStr)] string virtualRoot, [MarshalAs(UnmanagedType.BStr)] string baseUrl, [MarshalAs(UnmanagedType.BStr)] string authentication, [MarshalAs(UnmanagedType.BStr)] string assemblyName, [MarshalAs(UnmanagedType.BStr)] string typeName);
	}
}
