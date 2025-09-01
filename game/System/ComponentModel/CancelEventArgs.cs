using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Provides data for a cancelable event.</summary>
	// Token: 0x020003FB RID: 1019
	public class CancelEventArgs : EventArgs
	{
		/// <summary>Gets or sets a value indicating whether the event should be canceled.</summary>
		/// <returns>
		///   <see langword="true" /> if the event should be canceled; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06002135 RID: 8501 RVA: 0x00072151 File Offset: 0x00070351
		// (set) Token: 0x06002136 RID: 8502 RVA: 0x00072159 File Offset: 0x00070359
		public bool Cancel
		{
			[CompilerGenerated]
			get
			{
				return this.<Cancel>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Cancel>k__BackingField = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CancelEventArgs" /> class with the <see cref="P:System.ComponentModel.CancelEventArgs.Cancel" /> property set to <see langword="false" />.</summary>
		// Token: 0x06002137 RID: 8503 RVA: 0x0000C759 File Offset: 0x0000A959
		public CancelEventArgs()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CancelEventArgs" /> class with the <see cref="P:System.ComponentModel.CancelEventArgs.Cancel" /> property set to the given value.</summary>
		/// <param name="cancel">
		///   <see langword="true" /> to cancel the event; otherwise, <see langword="false" />.</param>
		// Token: 0x06002138 RID: 8504 RVA: 0x00072162 File Offset: 0x00070362
		public CancelEventArgs(bool cancel)
		{
			this.Cancel = cancel;
		}

		// Token: 0x04000FFA RID: 4090
		[CompilerGenerated]
		private bool <Cancel>k__BackingField;
	}
}
