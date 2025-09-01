using System;

namespace System.Reflection
{
	/// <summary>Specifies flags for the attributes of a method implementation.</summary>
	// Token: 0x020008AE RID: 2222
	public enum MethodImplAttributes
	{
		/// <summary>Specifies flags about code type.</summary>
		// Token: 0x04002EDE RID: 11998
		CodeTypeMask = 3,
		/// <summary>Specifies that the method implementation is in Microsoft intermediate language (MSIL).</summary>
		// Token: 0x04002EDF RID: 11999
		IL = 0,
		/// <summary>Specifies that the method implementation is native.</summary>
		// Token: 0x04002EE0 RID: 12000
		Native,
		/// <summary>Specifies that the method implementation is in Optimized Intermediate Language (OPTIL).</summary>
		// Token: 0x04002EE1 RID: 12001
		OPTIL,
		/// <summary>Specifies that the method implementation is provided by the runtime.</summary>
		// Token: 0x04002EE2 RID: 12002
		Runtime,
		/// <summary>Specifies whether the method is implemented in managed or unmanaged code.</summary>
		// Token: 0x04002EE3 RID: 12003
		ManagedMask,
		/// <summary>Specifies that the method is implemented in unmanaged code.</summary>
		// Token: 0x04002EE4 RID: 12004
		Unmanaged = 4,
		/// <summary>Specifies that the method is implemented in managed code.</summary>
		// Token: 0x04002EE5 RID: 12005
		Managed = 0,
		/// <summary>Specifies that the method is not defined.</summary>
		// Token: 0x04002EE6 RID: 12006
		ForwardRef = 16,
		/// <summary>Specifies that the method signature is exported exactly as declared.</summary>
		// Token: 0x04002EE7 RID: 12007
		PreserveSig = 128,
		/// <summary>Specifies an internal call.</summary>
		// Token: 0x04002EE8 RID: 12008
		InternalCall = 4096,
		/// <summary>Specifies that the method is single-threaded through the body. Static methods (<see langword="Shared" /> in Visual Basic) lock on the type, whereas instance methods lock on the instance. You can also use the C# lock statement or the Visual Basic SyncLock statement for this purpose.</summary>
		// Token: 0x04002EE9 RID: 12009
		Synchronized = 32,
		/// <summary>Specifies that the method cannot be inlined.</summary>
		// Token: 0x04002EEA RID: 12010
		NoInlining = 8,
		/// <summary>Specifies that the method should be inlined wherever possible.</summary>
		// Token: 0x04002EEB RID: 12011
		AggressiveInlining = 256,
		/// <summary>Specifies that the method is not optimized by the just-in-time (JIT) compiler or by native code generation (see Ngen.exe) when debugging possible code generation problems.</summary>
		// Token: 0x04002EEC RID: 12012
		NoOptimization = 64,
		/// <summary>Specifies a range check value.</summary>
		// Token: 0x04002EED RID: 12013
		MaxMethodImplVal = 65535,
		/// <summary>Specifies that the JIT compiler should look for security mitigation attributes, such as the user-defined <see langword="System.Runtime.CompilerServices.SecurityMitigationsAttribute" />. If found, the JIT compiler applies any related security mitigations. Available starting with .NET Framework 4.8.</summary>
		// Token: 0x04002EEE RID: 12014
		SecurityMitigations = 1024
	}
}
