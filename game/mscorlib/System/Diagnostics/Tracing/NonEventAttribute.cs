using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Identifies a method that is not generating an event.</summary>
	// Token: 0x020009FD RID: 2557
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class NonEventAttribute : Attribute
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Diagnostics.Tracing.NonEventAttribute" /> class.</summary>
		// Token: 0x06005B3B RID: 23355 RVA: 0x00002050 File Offset: 0x00000250
		public NonEventAttribute()
		{
		}
	}
}
