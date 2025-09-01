using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the method to call when you unregister an assembly for use from COM; this allows for the execution of user-written code during the unregistration process.</summary>
	// Token: 0x020006EF RID: 1775
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComUnregisterFunctionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> class.</summary>
		// Token: 0x06004073 RID: 16499 RVA: 0x00002050 File Offset: 0x00000250
		public ComUnregisterFunctionAttribute()
		{
		}
	}
}
