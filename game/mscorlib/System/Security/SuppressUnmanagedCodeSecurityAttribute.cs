using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	/// <summary>Allows managed code to call into unmanaged code without a stack walk. This class cannot be inherited.</summary>
	// Token: 0x020003CE RID: 974
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	public sealed class SuppressUnmanagedCodeSecurityAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SuppressUnmanagedCodeSecurityAttribute" /> class.</summary>
		// Token: 0x06002861 RID: 10337 RVA: 0x00002050 File Offset: 0x00000250
		public SuppressUnmanagedCodeSecurityAttribute()
		{
		}
	}
}
