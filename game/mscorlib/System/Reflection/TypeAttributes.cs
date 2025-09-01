using System;

namespace System.Reflection
{
	/// <summary>Specifies type attributes.</summary>
	// Token: 0x020008CD RID: 2253
	[Flags]
	public enum TypeAttributes
	{
		/// <summary>Specifies type visibility information.</summary>
		// Token: 0x04002F36 RID: 12086
		VisibilityMask = 7,
		/// <summary>Specifies that the class is not public.</summary>
		// Token: 0x04002F37 RID: 12087
		NotPublic = 0,
		/// <summary>Specifies that the class is public.</summary>
		// Token: 0x04002F38 RID: 12088
		Public = 1,
		/// <summary>Specifies that the class is nested with public visibility.</summary>
		// Token: 0x04002F39 RID: 12089
		NestedPublic = 2,
		/// <summary>Specifies that the class is nested with private visibility.</summary>
		// Token: 0x04002F3A RID: 12090
		NestedPrivate = 3,
		/// <summary>Specifies that the class is nested with family visibility, and is thus accessible only by methods within its own type and any derived types.</summary>
		// Token: 0x04002F3B RID: 12091
		NestedFamily = 4,
		/// <summary>Specifies that the class is nested with assembly visibility, and is thus accessible only by methods within its assembly.</summary>
		// Token: 0x04002F3C RID: 12092
		NestedAssembly = 5,
		/// <summary>Specifies that the class is nested with assembly and family visibility, and is thus accessible only by methods lying in the intersection of its family and assembly.</summary>
		// Token: 0x04002F3D RID: 12093
		NestedFamANDAssem = 6,
		/// <summary>Specifies that the class is nested with family or assembly visibility, and is thus accessible only by methods lying in the union of its family and assembly.</summary>
		// Token: 0x04002F3E RID: 12094
		NestedFamORAssem = 7,
		/// <summary>Specifies class layout information.</summary>
		// Token: 0x04002F3F RID: 12095
		LayoutMask = 24,
		/// <summary>Specifies that class fields are automatically laid out by the common language runtime.</summary>
		// Token: 0x04002F40 RID: 12096
		AutoLayout = 0,
		/// <summary>Specifies that class fields are laid out sequentially, in the order that the fields were emitted to the metadata.</summary>
		// Token: 0x04002F41 RID: 12097
		SequentialLayout = 8,
		/// <summary>Specifies that class fields are laid out at the specified offsets.</summary>
		// Token: 0x04002F42 RID: 12098
		ExplicitLayout = 16,
		/// <summary>Specifies class semantics information; the current class is contextful (else agile).</summary>
		// Token: 0x04002F43 RID: 12099
		ClassSemanticsMask = 32,
		/// <summary>Specifies that the type is a class.</summary>
		// Token: 0x04002F44 RID: 12100
		Class = 0,
		/// <summary>Specifies that the type is an interface.</summary>
		// Token: 0x04002F45 RID: 12101
		Interface = 32,
		/// <summary>Specifies that the type is abstract.</summary>
		// Token: 0x04002F46 RID: 12102
		Abstract = 128,
		/// <summary>Specifies that the class is concrete and cannot be extended.</summary>
		// Token: 0x04002F47 RID: 12103
		Sealed = 256,
		/// <summary>Specifies that the class is special in a way denoted by the name.</summary>
		// Token: 0x04002F48 RID: 12104
		SpecialName = 1024,
		/// <summary>Specifies that the class or interface is imported from another module.</summary>
		// Token: 0x04002F49 RID: 12105
		Import = 4096,
		/// <summary>Specifies that the class can be serialized.</summary>
		// Token: 0x04002F4A RID: 12106
		Serializable = 8192,
		/// <summary>Specifies a Windows Runtime type.</summary>
		// Token: 0x04002F4B RID: 12107
		WindowsRuntime = 16384,
		/// <summary>Used to retrieve string information for native interoperability.</summary>
		// Token: 0x04002F4C RID: 12108
		StringFormatMask = 196608,
		/// <summary>LPTSTR is interpreted as ANSI.</summary>
		// Token: 0x04002F4D RID: 12109
		AnsiClass = 0,
		/// <summary>LPTSTR is interpreted as UNICODE.</summary>
		// Token: 0x04002F4E RID: 12110
		UnicodeClass = 65536,
		/// <summary>LPTSTR is interpreted automatically.</summary>
		// Token: 0x04002F4F RID: 12111
		AutoClass = 131072,
		/// <summary>LPSTR is interpreted by some implementation-specific means, which includes the possibility of throwing a <see cref="T:System.NotSupportedException" />. Not used in the Microsoft implementation of the .NET Framework.</summary>
		// Token: 0x04002F50 RID: 12112
		CustomFormatClass = 196608,
		/// <summary>Used to retrieve non-standard encoding information for native interop. The meaning of the values of these 2 bits is unspecified. Not used in the Microsoft implementation of the .NET Framework.</summary>
		// Token: 0x04002F51 RID: 12113
		CustomFormatMask = 12582912,
		/// <summary>Specifies that calling static methods of the type does not force the system to initialize the type.</summary>
		// Token: 0x04002F52 RID: 12114
		BeforeFieldInit = 1048576,
		/// <summary>Runtime should check name encoding.</summary>
		// Token: 0x04002F53 RID: 12115
		RTSpecialName = 2048,
		/// <summary>Type has security associate with it.</summary>
		// Token: 0x04002F54 RID: 12116
		HasSecurity = 262144,
		/// <summary>Attributes reserved for runtime use.</summary>
		// Token: 0x04002F55 RID: 12117
		ReservedMask = 264192
	}
}
