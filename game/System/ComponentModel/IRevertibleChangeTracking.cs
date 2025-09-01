using System;

namespace System.ComponentModel
{
	/// <summary>Provides support for rolling back the changes</summary>
	// Token: 0x020003FE RID: 1022
	public interface IRevertibleChangeTracking : IChangeTracking
	{
		/// <summary>Resets the object's state to unchanged by rejecting the modifications.</summary>
		// Token: 0x0600213E RID: 8510
		void RejectChanges();
	}
}
