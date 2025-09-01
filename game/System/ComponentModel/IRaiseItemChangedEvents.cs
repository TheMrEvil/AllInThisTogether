using System;

namespace System.ComponentModel
{
	/// <summary>Indicates whether a class converts property change events to <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> events.</summary>
	// Token: 0x020003BA RID: 954
	public interface IRaiseItemChangedEvents
	{
		/// <summary>Gets a value indicating whether the <see cref="T:System.ComponentModel.IRaiseItemChangedEvents" /> object raises <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> events.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.ComponentModel.IRaiseItemChangedEvents" /> object raises <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> events when one of its property values changes; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001F19 RID: 7961
		bool RaisesItemChangedEvents { get; }
	}
}
