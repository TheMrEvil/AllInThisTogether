using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Identifies how to marshal parameters or fields to unmanaged code.</summary>
	// Token: 0x020006FD RID: 1789
	[ComVisible(true)]
	[Serializable]
	public enum UnmanagedType
	{
		/// <summary>A 4-byte Boolean value (<see langword="true" /> != 0, <see langword="false" /> = 0). This is the Win32 BOOL type.</summary>
		// Token: 0x04002AA1 RID: 10913
		Bool = 2,
		/// <summary>A 1-byte signed integer. You can use this member to transform a Boolean value into a 1-byte, C-style <see langword="bool" /> (<see langword="true" /> = 1, <see langword="false" /> = 0).</summary>
		// Token: 0x04002AA2 RID: 10914
		I1,
		/// <summary>A 1-byte unsigned integer.</summary>
		// Token: 0x04002AA3 RID: 10915
		U1,
		/// <summary>A 2-byte signed integer.</summary>
		// Token: 0x04002AA4 RID: 10916
		I2,
		/// <summary>A 2-byte unsigned integer.</summary>
		// Token: 0x04002AA5 RID: 10917
		U2,
		/// <summary>A 4-byte signed integer.</summary>
		// Token: 0x04002AA6 RID: 10918
		I4,
		/// <summary>A 4-byte unsigned integer.</summary>
		// Token: 0x04002AA7 RID: 10919
		U4,
		/// <summary>An 8-byte signed integer.</summary>
		// Token: 0x04002AA8 RID: 10920
		I8,
		/// <summary>An 8-byte unsigned integer.</summary>
		// Token: 0x04002AA9 RID: 10921
		U8,
		/// <summary>A 4-byte floating-point number.</summary>
		// Token: 0x04002AAA RID: 10922
		R4,
		/// <summary>An 8-byte floating-point number.</summary>
		// Token: 0x04002AAB RID: 10923
		R8,
		/// <summary>A currency type. Used on a <see cref="T:System.Decimal" /> to marshal the decimal value as a COM currency type instead of as a <see langword="Decimal" />.</summary>
		// Token: 0x04002AAC RID: 10924
		Currency = 15,
		/// <summary>A Unicode character string that is a length-prefixed double byte. You can use this member, which is the default string in COM, on the <see cref="T:System.String" /> data type.</summary>
		// Token: 0x04002AAD RID: 10925
		BStr = 19,
		/// <summary>A single byte, null-terminated ANSI character string. You can use this member on the <see cref="T:System.String" /> and <see cref="T:System.Text.StringBuilder" /> data types.</summary>
		// Token: 0x04002AAE RID: 10926
		LPStr,
		/// <summary>A 2-byte, null-terminated Unicode character string.</summary>
		// Token: 0x04002AAF RID: 10927
		LPWStr,
		/// <summary>A platform-dependent character string: ANSI on Windows 98, and Unicode on Windows NT and Windows XP. This value is supported only for platform invoke and not for COM interop, because exporting a string of type <see langword="LPTStr" /> is not supported.</summary>
		// Token: 0x04002AB0 RID: 10928
		LPTStr,
		/// <summary>Used for in-line, fixed-length character arrays that appear within a structure. The character type used with <see cref="F:System.Runtime.InteropServices.UnmanagedType.ByValTStr" /> is determined by the <see cref="T:System.Runtime.InteropServices.CharSet" /> argument of the <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" /> attribute applied to the containing structure. Always use the <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.SizeConst" /> field to indicate the size of the array.</summary>
		// Token: 0x04002AB1 RID: 10929
		ByValTStr,
		/// <summary>A COM <see langword="IUnknown" /> pointer. You can use this member on the <see cref="T:System.Object" /> data type.</summary>
		// Token: 0x04002AB2 RID: 10930
		IUnknown = 25,
		/// <summary>A COM <see langword="IDispatch" /> pointer (<see langword="Object" /> in Microsoft Visual Basic 6.0).</summary>
		// Token: 0x04002AB3 RID: 10931
		IDispatch,
		/// <summary>A VARIANT, which is used to marshal managed formatted classes and value types.</summary>
		// Token: 0x04002AB4 RID: 10932
		Struct,
		/// <summary>A COM interface pointer. The <see cref="T:System.Guid" /> of the interface is obtained from the class metadata. Use this member to specify the exact interface type or the default interface type if you apply it to a class. This member produces the same behavior as <see cref="F:System.Runtime.InteropServices.UnmanagedType.IUnknown" /> when you apply it to the <see cref="T:System.Object" /> data type.</summary>
		// Token: 0x04002AB5 RID: 10933
		Interface,
		/// <summary>A <see langword="SafeArray" />, which is a self-describing array that carries the type, rank, and bounds of the associated array data. You can use this member with the <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.SafeArraySubType" /> field to override the default element type.</summary>
		// Token: 0x04002AB6 RID: 10934
		SafeArray,
		/// <summary>When the <see cref="P:System.Runtime.InteropServices.MarshalAsAttribute.Value" /> property is set to <see langword="ByValArray" />, the <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.SizeConst" /> field must be set to indicate the number of elements in the array. The <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.ArraySubType" /> field can optionally contain the <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> of the array elements when it is necessary to differentiate among string types. You can use this <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> only on an array that whose elements appear as fields in a structure.</summary>
		// Token: 0x04002AB7 RID: 10935
		ByValArray,
		/// <summary>A platform-dependent, signed integer: 4 bytes on 32-bit Windows, 8 bytes on 64-bit Windows.</summary>
		// Token: 0x04002AB8 RID: 10936
		SysInt,
		/// <summary>A platform-dependent, unsigned integer: 4 bytes on 32-bit Windows, 8 bytes on 64-bit Windows.</summary>
		// Token: 0x04002AB9 RID: 10937
		SysUInt,
		/// <summary>A value that enables Visual Basic to change a string in unmanaged code and have the results reflected in managed code. This value is only supported for platform invoke.</summary>
		// Token: 0x04002ABA RID: 10938
		VBByRefStr = 34,
		/// <summary>An ANSI character string that is a length-prefixed single byte. You can use this member on the <see cref="T:System.String" /> data type.</summary>
		// Token: 0x04002ABB RID: 10939
		AnsiBStr,
		/// <summary>A length-prefixed, platform-dependent <see langword="char" /> string: ANSI on Windows 98, Unicode on Windows NT. You rarely use this BSTR-like member.</summary>
		// Token: 0x04002ABC RID: 10940
		TBStr,
		/// <summary>A 2-byte, OLE-defined VARIANT_BOOL type (<see langword="true" /> = -1, <see langword="false" /> = 0).</summary>
		// Token: 0x04002ABD RID: 10941
		VariantBool,
		/// <summary>An integer that can be used as a C-style function pointer. You can use this member on a <see cref="T:System.Delegate" /> data type or on a type that inherits from a <see cref="T:System.Delegate" />.</summary>
		// Token: 0x04002ABE RID: 10942
		FunctionPtr,
		/// <summary>A dynamic type that determines the type of an object at run time and marshals the object as that type. This member is valid for platform invoke methods only.</summary>
		// Token: 0x04002ABF RID: 10943
		AsAny = 40,
		/// <summary>A pointer to the first element of a C-style array. When marshaling from managed to unmanaged code, the length of the array is determined by the length of the managed array. When marshaling from unmanaged to managed code, the length of the array is determined from the <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.SizeConst" /> and <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.SizeParamIndex" /> fields, optionally followed by the unmanaged type of the elements within the array when it is necessary to differentiate among string types.</summary>
		// Token: 0x04002AC0 RID: 10944
		LPArray = 42,
		/// <summary>A pointer to a C-style structure that you use to marshal managed formatted classes. This member is valid for platform invoke methods only.</summary>
		// Token: 0x04002AC1 RID: 10945
		LPStruct,
		/// <summary>Specifies the custom marshaler class when used with the <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.MarshalType" /> or <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.MarshalTypeRef" /> field. The <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.MarshalCookie" /> field can be used to pass additional information to the custom marshaler. You can use this member on any reference type. This member is valid for parameters and return values only. It cannot be used on fields.</summary>
		// Token: 0x04002AC2 RID: 10946
		CustomMarshaler,
		/// <summary>A native type that is associated with an <see cref="F:System.Runtime.InteropServices.UnmanagedType.I4" /> or an <see cref="F:System.Runtime.InteropServices.UnmanagedType.U4" /> and that causes the parameter to be exported as an HRESULT in the exported type library.</summary>
		// Token: 0x04002AC3 RID: 10947
		Error,
		/// <summary>A Windows Runtime interface pointer. You can use this member on the <see cref="T:System.Object" /> data type.</summary>
		// Token: 0x04002AC4 RID: 10948
		[ComVisible(false)]
		IInspectable,
		/// <summary>A Windows Runtime string. You can use this member on the <see cref="T:System.String" /> data type.</summary>
		// Token: 0x04002AC5 RID: 10949
		[ComVisible(false)]
		HString,
		/// <summary>A pointer to a UTF-8 encoded string.</summary>
		// Token: 0x04002AC6 RID: 10950
		[ComVisible(false)]
		LPUTF8Str
	}
}
