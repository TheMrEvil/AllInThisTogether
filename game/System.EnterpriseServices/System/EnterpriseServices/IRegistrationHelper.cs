using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Installs and configures assemblies in the COM+ catalog.</summary>
	// Token: 0x02000024 RID: 36
	[Guid("55e3ea25-55cb-4650-8887-18e8d30bb4bc")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IRegistrationHelper
	{
		/// <summary>Installs the assembly into the COM+ catalog.</summary>
		/// <param name="assembly">The assembly name as a file or the strong name of an assembly in the global assembly cache (GAC).</param>
		/// <param name="application">The application parameter can be <see langword="null" />. If it is, the name of the application is automatically generated based on the name of the assembly or the <see langword="ApplicationName" /> attribute. If the application contains an <see langword="ApplicationID" /> attribute, the attribute takes precedence.</param>
		/// <param name="tlb">The name of the output type library (TLB) file, or a string containing <see langword="null" /> if the registration helper is expected to generate the name. On call completion, the actual name used is placed in the parameter.</param>
		/// <param name="installFlags">The installation options specified in the enumeration.</param>
		// Token: 0x06000077 RID: 119
		void InstallAssembly([MarshalAs(UnmanagedType.BStr)] [In] string assembly, [MarshalAs(UnmanagedType.BStr)] [In] [Out] ref string application, [MarshalAs(UnmanagedType.BStr)] [In] [Out] ref string tlb, [In] InstallationFlags installFlags);

		/// <summary>Uninstalls the assembly from the COM+ catalog.</summary>
		/// <param name="assembly">The assembly name as a file or the strong name of an assembly in the global assembly cache (GAC).</param>
		/// <param name="application">The name of the COM+ application.</param>
		// Token: 0x06000078 RID: 120
		void UninstallAssembly([MarshalAs(UnmanagedType.BStr)] [In] string assembly, [MarshalAs(UnmanagedType.BStr)] [In] string application);
	}
}
