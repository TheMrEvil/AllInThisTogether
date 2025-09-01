using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.IPersistFile" /> instead.</summary>
	// Token: 0x0200075D RID: 1885
	[Obsolete]
	[Guid("0000010b-0000-0000-c000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIPersistFile
	{
		/// <summary>Retrieves the class identifier (CLSID) of an object.</summary>
		/// <param name="pClassID">On successful return, a reference to the CLSID.</param>
		// Token: 0x0600427E RID: 17022
		void GetClassID(out Guid pClassID);

		/// <summary>Checks an object for changes since it was last saved to its current file.</summary>
		/// <returns>
		///   <see langword="S_OK" /> if the file has changed since it was last saved; <see langword="S_FALSE" /> if the file has not changed since it was last saved.</returns>
		// Token: 0x0600427F RID: 17023
		[PreserveSig]
		int IsDirty();

		/// <summary>Opens the specified file and initializes an object from the file contents.</summary>
		/// <param name="pszFileName">A zero-terminated string containing the absolute path of the file to open.</param>
		/// <param name="dwMode">A combination of values from the <see langword="STGM" /> enumeration to indicate the access mode in which to open <paramref name="pszFileName" />.</param>
		// Token: 0x06004280 RID: 17024
		void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, int dwMode);

		/// <summary>Saves a copy of the object into the specified file.</summary>
		/// <param name="pszFileName">A zero-terminated string containing the absolute path of the file to which the object is saved.</param>
		/// <param name="fRemember">Indicates whether <paramref name="pszFileName" /> is to be used as the current working file.</param>
		// Token: 0x06004281 RID: 17025
		void Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [MarshalAs(UnmanagedType.Bool)] bool fRemember);

		/// <summary>Notifies the object that it can write to its file.</summary>
		/// <param name="pszFileName">The absolute path of the file where the object was previously saved.</param>
		// Token: 0x06004282 RID: 17026
		void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

		/// <summary>Retrieves either the absolute path to current working file of the object, or if there is no current working file, the default filename prompt of the object.</summary>
		/// <param name="ppszFileName">The address of a pointer to a zero-terminated string containing the path for the current file, or the default filename prompt (such as *.txt).</param>
		// Token: 0x06004283 RID: 17027
		void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
	}
}
