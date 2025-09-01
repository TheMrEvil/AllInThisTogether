using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.Internal
{
	/// <summary>Identifies and installs components in the COM+ catalog.</summary>
	// Token: 0x0200005F RID: 95
	[Guid("c3f8f66b-91be-4c99-a94f-ce3b0a951039")]
	public interface IComManagedImportUtil
	{
		/// <summary>Gets the component information from the assembly.</summary>
		/// <param name="assemblyPath">The path to the assembly.</param>
		/// <param name="numComponents">When this method returns, this parameter contains the number of components in the assembly.</param>
		/// <param name="componentInfo">When this method returns, this parameter contains the information about the components.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyPath" /> is an empty string (""), contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.  
		/// -or-  
		/// The system could not retrieve the absolute path.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permissions.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyPath" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="assemblyPath" /> contains a colon (":").</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		// Token: 0x0600016D RID: 365
		[DispId(4)]
		void GetComponentInfo([MarshalAs(UnmanagedType.BStr)] string assemblyPath, [MarshalAs(UnmanagedType.BStr)] out string numComponents, [MarshalAs(UnmanagedType.BStr)] out string componentInfo);

		/// <summary>Installs an assembly into a COM+ application.</summary>
		/// <param name="filename">The path for the assembly.</param>
		/// <param name="parname">The COM+ partition name.</param>
		/// <param name="appname">The COM+ application name.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.EnterpriseServices.RegistrationException">The input assembly does not have a strong name.</exception>
		// Token: 0x0600016E RID: 366
		[DispId(5)]
		void InstallAssembly([MarshalAs(UnmanagedType.BStr)] string filename, [MarshalAs(UnmanagedType.BStr)] string parname, [MarshalAs(UnmanagedType.BStr)] string appname);
	}
}
