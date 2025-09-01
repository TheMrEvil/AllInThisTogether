using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Allows an unmanaged method to call a managed method.</summary>
	// Token: 0x020006E4 RID: 1764
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public sealed class AllowReversePInvokeCallsAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.AllowReversePInvokeCallsAttribute" /> class.</summary>
		// Token: 0x06004061 RID: 16481 RVA: 0x00002050 File Offset: 0x00000250
		public AllowReversePInvokeCallsAttribute()
		{
		}
	}
}
