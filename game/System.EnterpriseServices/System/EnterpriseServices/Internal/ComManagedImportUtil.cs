using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.Internal
{
	/// <summary>Identifies and installs components in the COM+ catalog.</summary>
	// Token: 0x0200005B RID: 91
	[Guid("3b0398c9-7812-4007-85cb-18c771f2206f")]
	public class ComManagedImportUtil : IComManagedImportUtil
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.Internal.ComManagedImportUtil" /> class.</summary>
		// Token: 0x0600015F RID: 351 RVA: 0x00002078 File Offset: 0x00000278
		[MonoTODO]
		public ComManagedImportUtil()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the component information from the assembly.</summary>
		/// <param name="assemblyPath">The path to the assembly.</param>
		/// <param name="numComponents">When this method returns, this parameter contains the number of components in the assembly.</param>
		/// <param name="componentInfo">When this method returns, this parameter contains the information about the components.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyPath" /> is an empty string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.  
		/// -or-  
		/// The system could not retrieve the absolute path.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permissions.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyPath" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="assemblyPath" /> contains a colon (":").</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		// Token: 0x06000160 RID: 352 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void GetComponentInfo(string assemblyPath, out string numComponents, out string componentInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Installs an assembly into a COM+ application.</summary>
		/// <param name="asmpath">The path for the assembly.</param>
		/// <param name="parname">The COM+ partition name.</param>
		/// <param name="appname">The COM+ application name.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.EnterpriseServices.RegistrationException">The input assembly does not have a strong name.</exception>
		// Token: 0x06000161 RID: 353 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void InstallAssembly(string asmpath, string parname, string appname)
		{
			throw new NotImplementedException();
		}
	}
}
