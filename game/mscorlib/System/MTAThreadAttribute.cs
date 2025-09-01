using System;

namespace System
{
	/// <summary>Indicates that the COM threading model for an application is multithreaded apartment (MTA).</summary>
	// Token: 0x02000190 RID: 400
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class MTAThreadAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MTAThreadAttribute" /> class.</summary>
		// Token: 0x06000FCD RID: 4045 RVA: 0x00002050 File Offset: 0x00000250
		public MTAThreadAttribute()
		{
		}
	}
}
