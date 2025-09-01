using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Modifies code generation for runtime just-in-time (JIT) debugging. This class cannot be inherited.</summary>
	// Token: 0x020009B8 RID: 2488
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module, AllowMultiple = false)]
	public sealed class DebuggableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggableAttribute" /> class, using the specified tracking and optimization options for the just-in-time (JIT) compiler.</summary>
		/// <param name="isJITTrackingEnabled">
		///   <see langword="true" /> to enable debugging; otherwise, <see langword="false" />.</param>
		/// <param name="isJITOptimizerDisabled">
		///   <see langword="true" /> to disable the optimizer for execution; otherwise, <see langword="false" />.</param>
		// Token: 0x0600599D RID: 22941 RVA: 0x00132F8A File Offset: 0x0013118A
		public DebuggableAttribute(bool isJITTrackingEnabled, bool isJITOptimizerDisabled)
		{
			this.m_debuggingModes = DebuggableAttribute.DebuggingModes.None;
			if (isJITTrackingEnabled)
			{
				this.m_debuggingModes |= DebuggableAttribute.DebuggingModes.Default;
			}
			if (isJITOptimizerDisabled)
			{
				this.m_debuggingModes |= DebuggableAttribute.DebuggingModes.DisableOptimizations;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggableAttribute" /> class, using the specified debugging modes for the just-in-time (JIT) compiler.</summary>
		/// <param name="modes">A bitwise combination of the <see cref="T:System.Diagnostics.DebuggableAttribute.DebuggingModes" /> values specifying the debugging mode for the JIT compiler.</param>
		// Token: 0x0600599E RID: 22942 RVA: 0x00132FBF File Offset: 0x001311BF
		public DebuggableAttribute(DebuggableAttribute.DebuggingModes modes)
		{
			this.m_debuggingModes = modes;
		}

		/// <summary>Gets a value that indicates whether the runtime will track information during code generation for the debugger.</summary>
		/// <returns>
		///   <see langword="true" /> if the runtime will track information during code generation for the debugger; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x0600599F RID: 22943 RVA: 0x00132FCE File Offset: 0x001311CE
		public bool IsJITTrackingEnabled
		{
			get
			{
				return (this.m_debuggingModes & DebuggableAttribute.DebuggingModes.Default) > DebuggableAttribute.DebuggingModes.None;
			}
		}

		/// <summary>Gets a value that indicates whether the runtime optimizer is disabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the runtime optimizer is disabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x060059A0 RID: 22944 RVA: 0x00132FDB File Offset: 0x001311DB
		public bool IsJITOptimizerDisabled
		{
			get
			{
				return (this.m_debuggingModes & DebuggableAttribute.DebuggingModes.DisableOptimizations) > DebuggableAttribute.DebuggingModes.None;
			}
		}

		/// <summary>Gets the debugging modes for the attribute.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Diagnostics.DebuggableAttribute.DebuggingModes" /> values describing the debugging mode for the just-in-time (JIT) compiler. The default is <see cref="F:System.Diagnostics.DebuggableAttribute.DebuggingModes.Default" />.</returns>
		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x060059A1 RID: 22945 RVA: 0x00132FEC File Offset: 0x001311EC
		public DebuggableAttribute.DebuggingModes DebuggingFlags
		{
			get
			{
				return this.m_debuggingModes;
			}
		}

		// Token: 0x04003774 RID: 14196
		private DebuggableAttribute.DebuggingModes m_debuggingModes;

		/// <summary>Specifies the debugging mode for the just-in-time (JIT) compiler.</summary>
		// Token: 0x020009B9 RID: 2489
		[Flags]
		[ComVisible(true)]
		public enum DebuggingModes
		{
			/// <summary>Starting with the .NET Framework version 2.0, JIT tracking information is always generated, and this flag has the same effect as <see cref="F:System.Diagnostics.DebuggableAttribute.DebuggingModes.Default" />, except that it sets the <see cref="P:System.Diagnostics.DebuggableAttribute.IsJITTrackingEnabled" /> property to <see langword="false" />. However, because JIT tracking is always enabled, the property value is ignored in version 2.0 or later.  
			///  Note that, unlike the <see cref="F:System.Diagnostics.DebuggableAttribute.DebuggingModes.DisableOptimizations" /> flag, the <see cref="F:System.Diagnostics.DebuggableAttribute.DebuggingModes.None" /> flag cannot be used to disable JIT optimizations.</summary>
			// Token: 0x04003776 RID: 14198
			None = 0,
			/// <summary>Instructs the just-in-time (JIT) compiler to use its default behavior, which includes enabling optimizations, disabling Edit and Continue support, and using symbol store sequence points if present. Starting with the .NET Framework version 2.0, JIT tracking information, the Microsoft intermediate language (MSIL) offset to the native-code offset within a method, is always generated.</summary>
			// Token: 0x04003777 RID: 14199
			Default = 1,
			/// <summary>Disable optimizations performed by the compiler to make your output file smaller, faster, and more efficient. Optimizations result in code rearrangement in the output file, which can make debugging difficult. Typically optimization should be disabled while debugging. In versions 2.0 or later, combine this value with Default (Default | DisableOptimizations) to enable JIT tracking and disable optimizations.</summary>
			// Token: 0x04003778 RID: 14200
			DisableOptimizations = 256,
			/// <summary>Use the implicit MSIL sequence points, not the program database (PDB) sequence points. The symbolic information normally includes at least one Microsoft intermediate language (MSIL) offset for each source line. When the just-in-time (JIT) compiler is about to compile a method, it asks the profiling services for a list of MSIL offsets that should be preserved. These MSIL offsets are called sequence points.</summary>
			// Token: 0x04003779 RID: 14201
			IgnoreSymbolStoreSequencePoints = 2,
			/// <summary>Enable edit and continue. Edit and continue enables you to make changes to your source code while your program is in break mode. The ability to edit and continue is compiler dependent.</summary>
			// Token: 0x0400377A RID: 14202
			EnableEditAndContinue = 4
		}
	}
}
