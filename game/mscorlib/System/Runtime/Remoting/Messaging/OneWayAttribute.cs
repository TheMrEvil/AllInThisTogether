using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Marks a method as one way, without a return value and <see langword="out" /> or <see langword="ref" /> parameters.</summary>
	// Token: 0x02000631 RID: 1585
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Method)]
	public class OneWayAttribute : Attribute
	{
		/// <summary>Creates an instance of <see cref="T:System.Runtime.Remoting.Messaging.OneWayAttribute" />.</summary>
		// Token: 0x06003BE2 RID: 15330 RVA: 0x00002050 File Offset: 0x00000250
		public OneWayAttribute()
		{
		}
	}
}
