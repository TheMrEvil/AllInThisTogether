using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Provides data for events that can be handled completely in an event handler.</summary>
	// Token: 0x020003AD RID: 941
	public class HandledEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.HandledEventArgs" /> class with a default <see cref="P:System.ComponentModel.HandledEventArgs.Handled" /> property value of <see langword="false" />.</summary>
		// Token: 0x06001ED6 RID: 7894 RVA: 0x0006CC82 File Offset: 0x0006AE82
		public HandledEventArgs() : this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.HandledEventArgs" /> class with the specified default value for the <see cref="P:System.ComponentModel.HandledEventArgs.Handled" /> property.</summary>
		/// <param name="defaultHandledValue">The default value for the <see cref="P:System.ComponentModel.HandledEventArgs.Handled" /> property.</param>
		// Token: 0x06001ED7 RID: 7895 RVA: 0x0006CC8B File Offset: 0x0006AE8B
		public HandledEventArgs(bool defaultHandledValue)
		{
			this.Handled = defaultHandledValue;
		}

		/// <summary>Gets or sets a value that indicates whether the event handler has completely handled the event or whether the system should continue its own processing.</summary>
		/// <returns>
		///   <see langword="true" /> if the event has been completely handled; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001ED8 RID: 7896 RVA: 0x0006CC9A File Offset: 0x0006AE9A
		// (set) Token: 0x06001ED9 RID: 7897 RVA: 0x0006CCA2 File Offset: 0x0006AEA2
		public bool Handled
		{
			[CompilerGenerated]
			get
			{
				return this.<Handled>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Handled>k__BackingField = value;
			}
		}

		// Token: 0x04000F49 RID: 3913
		[CompilerGenerated]
		private bool <Handled>k__BackingField;
	}
}
