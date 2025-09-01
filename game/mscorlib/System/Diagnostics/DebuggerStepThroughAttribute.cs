using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Instructs the debugger to step through the code instead of stepping into the code. This class cannot be inherited.</summary>
	// Token: 0x020009B4 RID: 2484
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class DebuggerStepThroughAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerStepThroughAttribute" /> class.</summary>
		// Token: 0x06005999 RID: 22937 RVA: 0x00002050 File Offset: 0x00000250
		public DebuggerStepThroughAttribute()
		{
		}
	}
}
