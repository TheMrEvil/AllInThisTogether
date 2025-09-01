using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Describes the original settings of the <see cref="T:System.Runtime.InteropServices.TYPEFLAGS" /> in the COM type library from which the type was imported.</summary>
	// Token: 0x020006F6 RID: 1782
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum TypeLibTypeFlags
	{
		/// <summary>A type description that describes an <see langword="Application" /> object.</summary>
		// Token: 0x04002A46 RID: 10822
		FAppObject = 1,
		/// <summary>Instances of the type can be created by <see langword="ITypeInfo::CreateInstance" />.</summary>
		// Token: 0x04002A47 RID: 10823
		FCanCreate = 2,
		/// <summary>The type is licensed.</summary>
		// Token: 0x04002A48 RID: 10824
		FLicensed = 4,
		/// <summary>The type is predefined. The client application should automatically create a single instance of the object that has this attribute. The name of the variable that points to the object is the same as the class name of the object.</summary>
		// Token: 0x04002A49 RID: 10825
		FPreDeclId = 8,
		/// <summary>The type should not be displayed to browsers.</summary>
		// Token: 0x04002A4A RID: 10826
		FHidden = 16,
		/// <summary>The type is a control from which other types will be derived, and should not be displayed to users.</summary>
		// Token: 0x04002A4B RID: 10827
		FControl = 32,
		/// <summary>The interface supplies both <see langword="IDispatch" /> and V-table binding.</summary>
		// Token: 0x04002A4C RID: 10828
		FDual = 64,
		/// <summary>The interface cannot add members at run time.</summary>
		// Token: 0x04002A4D RID: 10829
		FNonExtensible = 128,
		/// <summary>The types used in the interface are fully compatible with Automation, including vtable binding support.</summary>
		// Token: 0x04002A4E RID: 10830
		FOleAutomation = 256,
		/// <summary>This flag is intended for system-level types or types that type browsers should not display.</summary>
		// Token: 0x04002A4F RID: 10831
		FRestricted = 512,
		/// <summary>The class supports aggregation.</summary>
		// Token: 0x04002A50 RID: 10832
		FAggregatable = 1024,
		/// <summary>The object supports <see langword="IConnectionPointWithDefault" />, and has default behaviors.</summary>
		// Token: 0x04002A51 RID: 10833
		FReplaceable = 2048,
		/// <summary>Indicates that the interface derives from <see langword="IDispatch" />, either directly or indirectly.</summary>
		// Token: 0x04002A52 RID: 10834
		FDispatchable = 4096,
		/// <summary>Indicates base interfaces should be checked for name resolution before checking child interfaces. This is the reverse of the default behavior.</summary>
		// Token: 0x04002A53 RID: 10835
		FReverseBind = 8192
	}
}
