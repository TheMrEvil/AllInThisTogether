using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.BindingSource.AddingNew" /> event.</summary>
	// Token: 0x02000378 RID: 888
	public class AddingNewEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AddingNewEventArgs" /> class using no parameters.</summary>
		// Token: 0x06001D3F RID: 7487 RVA: 0x0000C759 File Offset: 0x0000A959
		public AddingNewEventArgs()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AddingNewEventArgs" /> class using the specified object as the new item.</summary>
		/// <param name="newObject">An <see cref="T:System.Object" /> to use as the new item value.</param>
		// Token: 0x06001D40 RID: 7488 RVA: 0x0006876E File Offset: 0x0006696E
		public AddingNewEventArgs(object newObject)
		{
			this.NewObject = newObject;
		}

		/// <summary>Gets or sets the object to be added to the binding list.</summary>
		/// <returns>The <see cref="T:System.Object" /> to be added as a new item to the associated collection.</returns>
		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001D41 RID: 7489 RVA: 0x0006877D File Offset: 0x0006697D
		// (set) Token: 0x06001D42 RID: 7490 RVA: 0x00068785 File Offset: 0x00066985
		public object NewObject
		{
			[CompilerGenerated]
			get
			{
				return this.<NewObject>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<NewObject>k__BackingField = value;
			}
		}

		// Token: 0x04000ECE RID: 3790
		[CompilerGenerated]
		private object <NewObject>k__BackingField;
	}
}
