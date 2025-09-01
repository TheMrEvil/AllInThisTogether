using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates how to marshal the array elements when an array is marshaled from managed to unmanaged code as a <see cref="F:System.Runtime.InteropServices.UnmanagedType.SafeArray" />.</summary>
	// Token: 0x020006FC RID: 1788
	[ComVisible(true)]
	[Serializable]
	public enum VarEnum
	{
		/// <summary>Indicates that a value was not specified.</summary>
		// Token: 0x04002A74 RID: 10868
		VT_EMPTY,
		/// <summary>Indicates a null value, similar to a null value in SQL.</summary>
		// Token: 0x04002A75 RID: 10869
		VT_NULL,
		/// <summary>Indicates a <see langword="short" /> integer.</summary>
		// Token: 0x04002A76 RID: 10870
		VT_I2,
		/// <summary>Indicates a <see langword="long" /> integer.</summary>
		// Token: 0x04002A77 RID: 10871
		VT_I4,
		/// <summary>Indicates a <see langword="float" /> value.</summary>
		// Token: 0x04002A78 RID: 10872
		VT_R4,
		/// <summary>Indicates a <see langword="double" /> value.</summary>
		// Token: 0x04002A79 RID: 10873
		VT_R8,
		/// <summary>Indicates a currency value.</summary>
		// Token: 0x04002A7A RID: 10874
		VT_CY,
		/// <summary>Indicates a DATE value.</summary>
		// Token: 0x04002A7B RID: 10875
		VT_DATE,
		/// <summary>Indicates a BSTR string.</summary>
		// Token: 0x04002A7C RID: 10876
		VT_BSTR,
		/// <summary>Indicates an <see langword="IDispatch" /> pointer.</summary>
		// Token: 0x04002A7D RID: 10877
		VT_DISPATCH,
		/// <summary>Indicates an SCODE.</summary>
		// Token: 0x04002A7E RID: 10878
		VT_ERROR,
		/// <summary>Indicates a Boolean value.</summary>
		// Token: 0x04002A7F RID: 10879
		VT_BOOL,
		/// <summary>Indicates a VARIANT <see langword="far" /> pointer.</summary>
		// Token: 0x04002A80 RID: 10880
		VT_VARIANT,
		/// <summary>Indicates an <see langword="IUnknown" /> pointer.</summary>
		// Token: 0x04002A81 RID: 10881
		VT_UNKNOWN,
		/// <summary>Indicates a <see langword="decimal" /> value.</summary>
		// Token: 0x04002A82 RID: 10882
		VT_DECIMAL,
		/// <summary>Indicates a <see langword="char" /> value.</summary>
		// Token: 0x04002A83 RID: 10883
		VT_I1 = 16,
		/// <summary>Indicates a <see langword="byte" />.</summary>
		// Token: 0x04002A84 RID: 10884
		VT_UI1,
		/// <summary>Indicates an <see langword="unsigned" /><see langword="short" />.</summary>
		// Token: 0x04002A85 RID: 10885
		VT_UI2,
		/// <summary>Indicates an <see langword="unsigned" /><see langword="long" />.</summary>
		// Token: 0x04002A86 RID: 10886
		VT_UI4,
		/// <summary>Indicates a 64-bit integer.</summary>
		// Token: 0x04002A87 RID: 10887
		VT_I8,
		/// <summary>Indicates an 64-bit unsigned integer.</summary>
		// Token: 0x04002A88 RID: 10888
		VT_UI8,
		/// <summary>Indicates an integer value.</summary>
		// Token: 0x04002A89 RID: 10889
		VT_INT,
		/// <summary>Indicates an <see langword="unsigned" /> integer value.</summary>
		// Token: 0x04002A8A RID: 10890
		VT_UINT,
		/// <summary>Indicates a C style <see langword="void" />.</summary>
		// Token: 0x04002A8B RID: 10891
		VT_VOID,
		/// <summary>Indicates an HRESULT.</summary>
		// Token: 0x04002A8C RID: 10892
		VT_HRESULT,
		/// <summary>Indicates a pointer type.</summary>
		// Token: 0x04002A8D RID: 10893
		VT_PTR,
		/// <summary>Indicates a SAFEARRAY. Not valid in a VARIANT.</summary>
		// Token: 0x04002A8E RID: 10894
		VT_SAFEARRAY,
		/// <summary>Indicates a C style array.</summary>
		// Token: 0x04002A8F RID: 10895
		VT_CARRAY,
		/// <summary>Indicates a user defined type.</summary>
		// Token: 0x04002A90 RID: 10896
		VT_USERDEFINED,
		/// <summary>Indicates a null-terminated string.</summary>
		// Token: 0x04002A91 RID: 10897
		VT_LPSTR,
		/// <summary>Indicates a wide string terminated by <see langword="null" />.</summary>
		// Token: 0x04002A92 RID: 10898
		VT_LPWSTR,
		/// <summary>Indicates a user defined type.</summary>
		// Token: 0x04002A93 RID: 10899
		VT_RECORD = 36,
		/// <summary>Indicates a FILETIME value.</summary>
		// Token: 0x04002A94 RID: 10900
		VT_FILETIME = 64,
		/// <summary>Indicates length prefixed bytes.</summary>
		// Token: 0x04002A95 RID: 10901
		VT_BLOB,
		/// <summary>Indicates that the name of a stream follows.</summary>
		// Token: 0x04002A96 RID: 10902
		VT_STREAM,
		/// <summary>Indicates that the name of a storage follows.</summary>
		// Token: 0x04002A97 RID: 10903
		VT_STORAGE,
		/// <summary>Indicates that a stream contains an object.</summary>
		// Token: 0x04002A98 RID: 10904
		VT_STREAMED_OBJECT,
		/// <summary>Indicates that a storage contains an object.</summary>
		// Token: 0x04002A99 RID: 10905
		VT_STORED_OBJECT,
		/// <summary>Indicates that a blob contains an object.</summary>
		// Token: 0x04002A9A RID: 10906
		VT_BLOB_OBJECT,
		/// <summary>Indicates the clipboard format.</summary>
		// Token: 0x04002A9B RID: 10907
		VT_CF,
		/// <summary>Indicates a class ID.</summary>
		// Token: 0x04002A9C RID: 10908
		VT_CLSID,
		/// <summary>Indicates a simple, counted array.</summary>
		// Token: 0x04002A9D RID: 10909
		VT_VECTOR = 4096,
		/// <summary>Indicates a <see langword="SAFEARRAY" /> pointer.</summary>
		// Token: 0x04002A9E RID: 10910
		VT_ARRAY = 8192,
		/// <summary>Indicates that a value is a reference.</summary>
		// Token: 0x04002A9F RID: 10911
		VT_BYREF = 16384
	}
}
