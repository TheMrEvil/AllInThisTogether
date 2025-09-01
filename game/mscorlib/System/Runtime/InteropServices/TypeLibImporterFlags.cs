using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates how an assembly should be produced.</summary>
	// Token: 0x02000754 RID: 1876
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum TypeLibImporterFlags
	{
		/// <summary>Generates a primary interop assembly. For more information, see the <see cref="T:System.Runtime.InteropServices.PrimaryInteropAssemblyAttribute" /> attribute. A keyfile must be specified.</summary>
		// Token: 0x04002C0F RID: 11279
		PrimaryInteropAssembly = 1,
		/// <summary>Imports all interfaces as interfaces that suppress the common language runtime's stack crawl for <see cref="F:System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode" /> permission. Be sure you understand the responsibilities associated with suppressing this security check.</summary>
		// Token: 0x04002C10 RID: 11280
		UnsafeInterfaces = 2,
		/// <summary>Imports all <see langword="SAFEARRAY" /> instances as <see cref="T:System.Array" /> instead of typed, single-dimensional, zero-based managed arrays. This option is useful when dealing with multi-dimensional, non-zero-based <see langword="SAFEARRAY" /> instances, which otherwise cannot be accessed unless you edit the resulting assembly by using the MSIL Disassembler (Ildasm.exe) and MSIL Assembler (Ilasm.exe) tools.</summary>
		// Token: 0x04002C11 RID: 11281
		SafeArrayAsSystemArray = 4,
		/// <summary>Transforms <see langword="[out, retval]" /> parameters of methods on dispatch-only interfaces (dispinterface) into return values.</summary>
		// Token: 0x04002C12 RID: 11282
		TransformDispRetVals = 8,
		/// <summary>No special settings. This is the default.</summary>
		// Token: 0x04002C13 RID: 11283
		None = 0,
		/// <summary>Not used.</summary>
		// Token: 0x04002C14 RID: 11284
		PreventClassMembers = 16,
		/// <summary>Imports a type library for any platform.</summary>
		// Token: 0x04002C15 RID: 11285
		ImportAsAgnostic = 2048,
		/// <summary>Imports a type library for the Itanium platform.</summary>
		// Token: 0x04002C16 RID: 11286
		ImportAsItanium = 1024,
		/// <summary>Imports a type library for the x86 64-bit platform.</summary>
		// Token: 0x04002C17 RID: 11287
		ImportAsX64 = 512,
		/// <summary>Imports a type library for the x86 platform.</summary>
		// Token: 0x04002C18 RID: 11288
		ImportAsX86 = 256,
		/// <summary>Uses reflection-only loading.</summary>
		// Token: 0x04002C19 RID: 11289
		ReflectionOnlyLoading = 4096,
		/// <summary>Uses serializable classes.</summary>
		// Token: 0x04002C1A RID: 11290
		SerializableValueClasses = 32,
		/// <summary>Prevents inclusion of a version resource in the interop assembly. For more information, see the <see cref="M:System.Reflection.Emit.AssemblyBuilder.DefineVersionInfoResource" /> method.</summary>
		// Token: 0x04002C1B RID: 11291
		NoDefineVersionResource = 8192,
		/// <summary>Imports a library for the ARM platform.</summary>
		// Token: 0x04002C1C RID: 11292
		ImportAsArm = 16384
	}
}
