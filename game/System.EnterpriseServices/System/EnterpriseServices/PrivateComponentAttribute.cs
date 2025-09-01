using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Identifies a component as a private component that is only seen and activated by components in the same application. This class cannot be inherited.</summary>
	// Token: 0x02000037 RID: 55
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(false)]
	public sealed class PrivateComponentAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.PrivateComponentAttribute" /> class.</summary>
		// Token: 0x060000B0 RID: 176 RVA: 0x00002050 File Offset: 0x00000250
		public PrivateComponentAttribute()
		{
		}
	}
}
