using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the method to call when you register an assembly for use from COM; this enables the execution of user-written code during the registration process.</summary>
	// Token: 0x020006EE RID: 1774
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComRegisterFunctionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> class.</summary>
		// Token: 0x06004072 RID: 16498 RVA: 0x00002050 File Offset: 0x00000250
		public ComRegisterFunctionAttribute()
		{
		}
	}
}
