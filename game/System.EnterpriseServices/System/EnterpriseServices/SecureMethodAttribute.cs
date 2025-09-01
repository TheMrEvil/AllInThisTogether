using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Ensures that the infrastructure calls through an interface for a method or for each method in a class when using the security service. Classes need to use interfaces to use security services. This class cannot be inherited.</summary>
	// Token: 0x02000041 RID: 65
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	[ComVisible(false)]
	public sealed class SecureMethodAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.SecureMethodAttribute" /> class.</summary>
		// Token: 0x060000E2 RID: 226 RVA: 0x00002050 File Offset: 0x00000250
		public SecureMethodAttribute()
		{
		}
	}
}
