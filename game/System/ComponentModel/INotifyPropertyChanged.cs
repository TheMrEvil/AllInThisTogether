using System;

namespace System.ComponentModel
{
	/// <summary>Notifies clients that a property value has changed.</summary>
	// Token: 0x02000401 RID: 1025
	public interface INotifyPropertyChanged
	{
		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06002145 RID: 8517
		// (remove) Token: 0x06002146 RID: 8518
		event PropertyChangedEventHandler PropertyChanged;
	}
}
