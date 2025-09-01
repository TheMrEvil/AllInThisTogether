using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.TYPEFLAGS" /> instead.</summary>
	// Token: 0x02000722 RID: 1826
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.TYPEFLAGS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Flags]
	[Serializable]
	public enum TYPEFLAGS : short
	{
		/// <summary>A type description that describes an Application object.</summary>
		// Token: 0x04002B18 RID: 11032
		TYPEFLAG_FAPPOBJECT = 1,
		/// <summary>Instances of the type can be created by <see langword="ITypeInfo::CreateInstance" />.</summary>
		// Token: 0x04002B19 RID: 11033
		TYPEFLAG_FCANCREATE = 2,
		/// <summary>The type is licensed.</summary>
		// Token: 0x04002B1A RID: 11034
		TYPEFLAG_FLICENSED = 4,
		/// <summary>The type is predefined. The client application should automatically create a single instance of the object that has this attribute. The name of the variable that points to the object is the same as the class name of the object.</summary>
		// Token: 0x04002B1B RID: 11035
		TYPEFLAG_FPREDECLID = 8,
		/// <summary>The type should not be displayed to browsers.</summary>
		// Token: 0x04002B1C RID: 11036
		TYPEFLAG_FHIDDEN = 16,
		/// <summary>The type is a control from which other types will be derived, and should not be displayed to users.</summary>
		// Token: 0x04002B1D RID: 11037
		TYPEFLAG_FCONTROL = 32,
		/// <summary>The interface supplies both <see langword="IDispatch" /> and VTBL binding.</summary>
		// Token: 0x04002B1E RID: 11038
		TYPEFLAG_FDUAL = 64,
		/// <summary>The interface cannot add members at run time.</summary>
		// Token: 0x04002B1F RID: 11039
		TYPEFLAG_FNONEXTENSIBLE = 128,
		/// <summary>The types used in the interface are fully compatible with Automation, including VTBL binding support. Setting dual on an interface sets this flag in addition to <see cref="F:System.Runtime.InteropServices.TYPEFLAGS.TYPEFLAG_FDUAL" />. Not allowed on dispinterfaces.</summary>
		// Token: 0x04002B20 RID: 11040
		TYPEFLAG_FOLEAUTOMATION = 256,
		/// <summary>Should not be accessible from macro languages. This flag is intended for system-level types or types that type browsers should not display.</summary>
		// Token: 0x04002B21 RID: 11041
		TYPEFLAG_FRESTRICTED = 512,
		/// <summary>The class supports aggregation.</summary>
		// Token: 0x04002B22 RID: 11042
		TYPEFLAG_FAGGREGATABLE = 1024,
		/// <summary>The object supports <see langword="IConnectionPointWithDefault" />, and has default behaviors.</summary>
		// Token: 0x04002B23 RID: 11043
		TYPEFLAG_FREPLACEABLE = 2048,
		/// <summary>Indicates that the interface derives from <see langword="IDispatch" />, either directly or indirectly. This flag is computed, there is no Object Description Language for the flag.</summary>
		// Token: 0x04002B24 RID: 11044
		TYPEFLAG_FDISPATCHABLE = 4096,
		/// <summary>Indicates base interfaces should be checked for name resolution before checking children, the reverse of the default behavior.</summary>
		// Token: 0x04002B25 RID: 11045
		TYPEFLAG_FREVERSEBIND = 8192,
		/// <summary>Indicates that the interface will be using a proxy/stub dynamic link library. This flag specifies that the type library proxy should not be unregistered when the type library is unregistered.</summary>
		// Token: 0x04002B26 RID: 11046
		TYPEFLAG_FPROXY = 16384
	}
}
