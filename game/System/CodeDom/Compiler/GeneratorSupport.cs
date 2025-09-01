using System;

namespace System.CodeDom.Compiler
{
	/// <summary>Defines identifiers used to determine whether a code generator supports certain types of code elements.</summary>
	// Token: 0x02000350 RID: 848
	[Flags]
	public enum GeneratorSupport
	{
		/// <summary>Indicates the generator supports arrays of arrays.</summary>
		// Token: 0x04000E51 RID: 3665
		ArraysOfArrays = 1,
		/// <summary>Indicates the generator supports a program entry point method designation. This is used when building executables.</summary>
		// Token: 0x04000E52 RID: 3666
		EntryPointMethod = 2,
		/// <summary>Indicates the generator supports goto statements.</summary>
		// Token: 0x04000E53 RID: 3667
		GotoStatements = 4,
		/// <summary>Indicates the generator supports referencing multidimensional arrays. Currently, the CodeDom cannot be used to instantiate multidimensional arrays.</summary>
		// Token: 0x04000E54 RID: 3668
		MultidimensionalArrays = 8,
		/// <summary>Indicates the generator supports static constructors.</summary>
		// Token: 0x04000E55 RID: 3669
		StaticConstructors = 16,
		/// <summary>Indicates the generator supports <see langword="try...catch" /> statements.</summary>
		// Token: 0x04000E56 RID: 3670
		TryCatchStatements = 32,
		/// <summary>Indicates the generator supports return type attribute declarations.</summary>
		// Token: 0x04000E57 RID: 3671
		ReturnTypeAttributes = 64,
		/// <summary>Indicates the generator supports value type declarations.</summary>
		// Token: 0x04000E58 RID: 3672
		DeclareValueTypes = 128,
		/// <summary>Indicates the generator supports enumeration declarations.</summary>
		// Token: 0x04000E59 RID: 3673
		DeclareEnums = 256,
		/// <summary>Indicates the generator supports delegate declarations.</summary>
		// Token: 0x04000E5A RID: 3674
		DeclareDelegates = 512,
		/// <summary>Indicates the generator supports interface declarations.</summary>
		// Token: 0x04000E5B RID: 3675
		DeclareInterfaces = 1024,
		/// <summary>Indicates the generator supports event declarations.</summary>
		// Token: 0x04000E5C RID: 3676
		DeclareEvents = 2048,
		/// <summary>Indicates the generator supports assembly attributes.</summary>
		// Token: 0x04000E5D RID: 3677
		AssemblyAttributes = 4096,
		/// <summary>Indicates the generator supports parameter attributes.</summary>
		// Token: 0x04000E5E RID: 3678
		ParameterAttributes = 8192,
		/// <summary>Indicates the generator supports reference and out parameters.</summary>
		// Token: 0x04000E5F RID: 3679
		ReferenceParameters = 16384,
		/// <summary>Indicates the generator supports chained constructor arguments.</summary>
		// Token: 0x04000E60 RID: 3680
		ChainedConstructorArguments = 32768,
		/// <summary>Indicates the generator supports the declaration of nested types.</summary>
		// Token: 0x04000E61 RID: 3681
		NestedTypes = 65536,
		/// <summary>Indicates the generator supports the declaration of members that implement multiple interfaces.</summary>
		// Token: 0x04000E62 RID: 3682
		MultipleInterfaceMembers = 131072,
		/// <summary>Indicates the generator supports public static members.</summary>
		// Token: 0x04000E63 RID: 3683
		PublicStaticMembers = 262144,
		/// <summary>Indicates the generator supports complex expressions.</summary>
		// Token: 0x04000E64 RID: 3684
		ComplexExpressions = 524288,
		/// <summary>Indicates the generator supports compilation with Win32 resources.</summary>
		// Token: 0x04000E65 RID: 3685
		Win32Resources = 1048576,
		/// <summary>Indicates the generator supports compilation with .NET Framework resources. These can be default resources compiled directly into an assembly, or resources referenced in a satellite assembly.</summary>
		// Token: 0x04000E66 RID: 3686
		Resources = 2097152,
		/// <summary>Indicates the generator supports partial type declarations.</summary>
		// Token: 0x04000E67 RID: 3687
		PartialTypes = 4194304,
		/// <summary>Indicates the generator supports generic type references.</summary>
		// Token: 0x04000E68 RID: 3688
		GenericTypeReference = 8388608,
		/// <summary>Indicates the generator supports generic type declarations.</summary>
		// Token: 0x04000E69 RID: 3689
		GenericTypeDeclaration = 16777216,
		/// <summary>Indicates the generator supports the declaration of indexer properties.</summary>
		// Token: 0x04000E6A RID: 3690
		DeclareIndexerProperties = 33554432
	}
}
