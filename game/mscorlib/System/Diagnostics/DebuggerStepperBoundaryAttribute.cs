using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Indicates the code following the attribute is to be executed in run, not step, mode.</summary>
	// Token: 0x020009B5 RID: 2485
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class DebuggerStepperBoundaryAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerStepperBoundaryAttribute" /> class.</summary>
		// Token: 0x0600599A RID: 22938 RVA: 0x00002050 File Offset: 0x00000250
		public DebuggerStepperBoundaryAttribute()
		{
		}
	}
}
