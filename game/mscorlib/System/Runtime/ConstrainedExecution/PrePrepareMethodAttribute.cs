using System;

namespace System.Runtime.ConstrainedExecution
{
	/// <summary>Instructs the native image generation service to prepare a method for inclusion in a constrained execution region (CER).</summary>
	// Token: 0x020007D6 RID: 2006
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	public sealed class PrePrepareMethodAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.ConstrainedExecution.PrePrepareMethodAttribute" /> class.</summary>
		// Token: 0x060045B5 RID: 17845 RVA: 0x00002050 File Offset: 0x00000250
		public PrePrepareMethodAttribute()
		{
		}
	}
}
