using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.Internal
{
	/// <summary>Creates a Web.config file for a SOAP-enabled COM+ application and adds component entries to the file for COM interfaces being published in the application.</summary>
	// Token: 0x02000064 RID: 100
	[Guid("6261e4b5-572a-4142-a2f9-1fe1a0c97097")]
	public interface IServerWebConfig
	{
		/// <summary>Adds XML elements to a Web.config file for a COM interface being published in a SOAP-enabled COM+ application.</summary>
		/// <param name="FilePath">The path for the existing Web.config file.</param>
		/// <param name="AssemblyName">The name of the assembly that contains the type being added.</param>
		/// <param name="TypeName">The name of the type being added.</param>
		/// <param name="ProgId">The programmatic identifier for the type being added.</param>
		/// <param name="Mode">A string constant that corresponds to the name of a member from the <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> enumeration, which indicates how a well-known object is activated.</param>
		/// <param name="Error">A string to which an error message can be written.</param>
		// Token: 0x06000182 RID: 386
		[DispId(1)]
		void AddElement([MarshalAs(UnmanagedType.BStr)] string FilePath, [MarshalAs(UnmanagedType.BStr)] string AssemblyName, [MarshalAs(UnmanagedType.BStr)] string TypeName, [MarshalAs(UnmanagedType.BStr)] string ProgId, [MarshalAs(UnmanagedType.BStr)] string Mode, [MarshalAs(UnmanagedType.BStr)] out string Error);

		/// <summary>Creates a Web.config file for a SOAP-enabled COM+ application so that the file is ready to have XML elements added for COM interfaces being published.</summary>
		/// <param name="FilePath">The folder in which to create the configuration file.</param>
		/// <param name="FileRootName">The string value to which a config extension can be added (for example, Web for Web.config).</param>
		/// <param name="Error">A string to which an error message can be written.</param>
		// Token: 0x06000183 RID: 387
		[DispId(2)]
		void Create([MarshalAs(UnmanagedType.BStr)] string FilePath, [MarshalAs(UnmanagedType.BStr)] string FileRootName, [MarshalAs(UnmanagedType.BStr)] out string Error);
	}
}
