using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Specifies the <see cref="T:System.Diagnostics.DebuggerHiddenAttribute" />. This class cannot be inherited.</summary>
	// Token: 0x020009B6 RID: 2486
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, Inherited = false)]
	[Serializable]
	public sealed class DebuggerHiddenAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerHiddenAttribute" /> class.</summary>
		// Token: 0x0600599B RID: 22939 RVA: 0x00002050 File Offset: 0x00000250
		public DebuggerHiddenAttribute()
		{
		}
	}
}
