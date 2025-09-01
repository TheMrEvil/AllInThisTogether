using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Provides a managed definition of the <see langword="ITypeLib2" /> interface.</summary>
	// Token: 0x020007CF RID: 1999
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("00020411-0000-0000-C000-000000000046")]
	[ComImport]
	public interface ITypeLib2 : ITypeLib
	{
		/// <summary>Returns the number of type descriptions in the type library.</summary>
		/// <returns>The number of type descriptions in the type library.</returns>
		// Token: 0x0600459A RID: 17818
		[PreserveSig]
		int GetTypeInfoCount();

		/// <summary>Retrieves the specified type description in the library.</summary>
		/// <param name="index">An index of the <see langword="ITypeInfo" /> interface to return.</param>
		/// <param name="ppTI">When this method returns, contains an <see langword="ITypeInfo" /> describing the type referenced by <paramref name="index" />. This parameter is passed uninitialized.</param>
		// Token: 0x0600459B RID: 17819
		void GetTypeInfo(int index, out ITypeInfo ppTI);

		/// <summary>Retrieves the type of a type description.</summary>
		/// <param name="index">The index of the type description within the type library.</param>
		/// <param name="pTKind">When this method returns, contains a reference to the <see langword="TYPEKIND" /> enumeration for the type description. This parameter is passed uninitialized.</param>
		// Token: 0x0600459C RID: 17820
		void GetTypeInfoType(int index, out TYPEKIND pTKind);

		/// <summary>Retrieves the type description that corresponds to the specified GUID.</summary>
		/// <param name="guid">The <see cref="T:System.Guid" />, passed by reference, that represents the IID of the <see langword="CLSID" /> interface of the class whose type info is requested.</param>
		/// <param name="ppTInfo">When this method returns, contains the requested <see langword="ITypeInfo" /> interface. This parameter is passed uninitialized.</param>
		// Token: 0x0600459D RID: 17821
		void GetTypeInfoOfGuid(ref Guid guid, out ITypeInfo ppTInfo);

		/// <summary>Retrieves the structure that contains the library's attributes.</summary>
		/// <param name="ppTLibAttr">When this method returns, contains a structure that contains the library's attributes. This parameter is passed uninitialized.</param>
		// Token: 0x0600459E RID: 17822
		void GetLibAttr(out IntPtr ppTLibAttr);

		/// <summary>Enables a client compiler to bind to a library's types, variables, constants, and global functions.</summary>
		/// <param name="ppTComp">When this method returns, contains an <see langword="ITypeComp" /> instance for this <see langword="ITypeLib" />. This parameter is passed uninitialized.</param>
		// Token: 0x0600459F RID: 17823
		void GetTypeComp(out ITypeComp ppTComp);

		/// <summary>Retrieves the library's documentation string, the complete Help file name and path, and the context identifier for the library Help topic in the Help file.</summary>
		/// <param name="index">An index of the type description whose documentation is to be returned.</param>
		/// <param name="strName">When this method returns, contains a string that specifies the name of the specified item. This parameter is passed uninitialized.</param>
		/// <param name="strDocString">When this method returns, contains the documentation string for the specified item. This parameter is passed uninitialized.</param>
		/// <param name="dwHelpContext">When this method returns, contains the Help context identifier associated with the specified item. This parameter is passed uninitialized.</param>
		/// <param name="strHelpFile">When this method returns, contains a string that specifies the fully qualified name of the Help file. This parameter is passed uninitialized.</param>
		// Token: 0x060045A0 RID: 17824
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		/// <summary>Indicates whether a passed-in string contains the name of a type or member described in the library.</summary>
		/// <param name="szNameBuf">The string to test.</param>
		/// <param name="lHashVal">The hash value of <paramref name="szNameBuf" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="szNameBuf" /> was found in the type library; otherwise, <see langword="false" />.</returns>
		// Token: 0x060045A1 RID: 17825
		[return: MarshalAs(UnmanagedType.Bool)]
		bool IsName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal);

		/// <summary>Finds occurrences of a type description in a type library.</summary>
		/// <param name="szNameBuf">The name to search for.</param>
		/// <param name="lHashVal">A hash value to speed up the search, computed by the <see langword="LHashValOfNameSys" /> function. If <paramref name="lHashVal" /> is 0, a value is computed.</param>
		/// <param name="ppTInfo">When this method returns, contains an array of pointers to the type descriptions that contain the name specified in <paramref name="szNameBuf" />. This parameter is passed uninitialized.</param>
		/// <param name="rgMemId">When this method returns, contains an array of the <see langword="MEMBERID" />s of the found items; <paramref name="rgMemId" /> [i] is the <see langword="MEMBERID" /> that indexes into the type description specified by <paramref name="ppTInfo" /> [i]. This parameter cannot be <see langword="null" />. This parameter is passed uninitialized.</param>
		/// <param name="pcFound">On entry, a value, passed by reference, that indicates how many instances to look for. For example, <paramref name="pcFound" /> = 1 can be called to find the first occurrence. The search stops when one instance is found.  
		///  On exit, indicates the number of instances that were found. If the <see langword="in" /> and <see langword="out" /> values of <paramref name="pcFound" /> are identical, there might be more type descriptions that contain the name.</param>
		// Token: 0x060045A2 RID: 17826
		void FindName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal, [MarshalAs(UnmanagedType.LPArray)] [Out] ITypeInfo[] ppTInfo, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] rgMemId, ref short pcFound);

		/// <summary>Releases the <see cref="T:System.Runtime.InteropServices.TYPELIBATTR" /> structure originally obtained from the <see cref="M:System.Runtime.InteropServices.ComTypes.ITypeLib.GetLibAttr(System.IntPtr@)" /> method.</summary>
		/// <param name="pTLibAttr">The <see langword="TLIBATTR" /> structure to release.</param>
		// Token: 0x060045A3 RID: 17827
		[PreserveSig]
		void ReleaseTLibAttr(IntPtr pTLibAttr);

		/// <summary>Gets the custom data.</summary>
		/// <param name="guid">A <see cref="T:System.Guid" /> , passed by reference, that is used to identify the data.</param>
		/// <param name="pVarVal">When this method returns, contains an object that specifies where to put the retrieved data. This parameter is passed uninitialized.</param>
		// Token: 0x060045A4 RID: 17828
		void GetCustData(ref Guid guid, out object pVarVal);

		/// <summary>Retrieves the library's documentation string, the complete Help file name and path, the localization context to use, and the context ID for the library Help topic in the Help file.</summary>
		/// <param name="index">An index of the type description whose documentation is to be returned; if <paramref name="index" /> is -1, the documentation for the library is returned.</param>
		/// <param name="pbstrHelpString">When this method returns, contains a BSTR that specifies the name of the specified item. If the caller does not need the item name, <paramref name="pbstrHelpString" /> can be <see langword="null" />. This parameter is passed uninitialized.</param>
		/// <param name="pdwHelpStringContext">When this method returns, contains the Help localization context. If the caller does not need the Help context, <paramref name="pdwHelpStringContext" /> can be <see langword="null" />. This parameter is passed uninitialized.</param>
		/// <param name="pbstrHelpStringDll">When this method returns, contains a BSTR that specifies the fully qualified name of the file containing the DLL used for Help file. If the caller does not need the file name, <paramref name="pbstrHelpStringDll" /> can be <see langword="null" />. This parameter is passed uninitialized.</param>
		// Token: 0x060045A5 RID: 17829
		[LCIDConversion(1)]
		void GetDocumentation2(int index, out string pbstrHelpString, out int pdwHelpStringContext, out string pbstrHelpStringDll);

		/// <summary>Returns statistics about a type library that are required for efficient sizing of hash tables.</summary>
		/// <param name="pcUniqueNames">A pointer to a count of unique names. If the caller does not need this information, set to <see langword="null" />.</param>
		/// <param name="pcchUniqueNames">When this method returns, contains a pointer to a change in the count of unique names. This parameter is passed uninitialized.</param>
		// Token: 0x060045A6 RID: 17830
		void GetLibStatistics(IntPtr pcUniqueNames, out int pcchUniqueNames);

		/// <summary>Gets all custom data items for the library.</summary>
		/// <param name="pCustData">A pointer to <see langword="CUSTDATA" />, which holds all custom data items.</param>
		// Token: 0x060045A7 RID: 17831
		void GetAllCustData(IntPtr pCustData);
	}
}
