using System;

namespace System.ComponentModel
{
	/// <summary>Defines the mechanism for querying the object for changes and resetting of the changed status.</summary>
	// Token: 0x020003FC RID: 1020
	public interface IChangeTracking
	{
		/// <summary>Gets the object's changed status.</summary>
		/// <returns>
		///   <see langword="true" /> if the object's content has changed since the last call to <see cref="M:System.ComponentModel.IChangeTracking.AcceptChanges" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06002139 RID: 8505
		bool IsChanged { get; }

		/// <summary>Resets the object's state to unchanged by accepting the modifications.</summary>
		// Token: 0x0600213A RID: 8506
		void AcceptChanges();
	}
}
