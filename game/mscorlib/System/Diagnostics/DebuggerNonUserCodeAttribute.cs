using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Identifies a type or member that is not part of the user code for an application.</summary>
	// Token: 0x020009B7 RID: 2487
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, Inherited = false)]
	[Serializable]
	public sealed class DebuggerNonUserCodeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerNonUserCodeAttribute" /> class.</summary>
		// Token: 0x0600599C RID: 22940 RVA: 0x00002050 File Offset: 0x00000250
		public DebuggerNonUserCodeAttribute()
		{
		}
	}
}
