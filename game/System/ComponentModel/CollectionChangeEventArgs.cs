using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.Data.DataColumnCollection.CollectionChanged" /> event.</summary>
	// Token: 0x0200038B RID: 907
	public class CollectionChangeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.ComponentModel.CollectionChangeAction" /> values that specifies how the collection changed.</param>
		/// <param name="element">An <see cref="T:System.Object" /> that specifies the instance of the collection where the change occurred.</param>
		// Token: 0x06001DD9 RID: 7641 RVA: 0x00069A2C File Offset: 0x00067C2C
		public CollectionChangeEventArgs(CollectionChangeAction action, object element)
		{
			this.Action = action;
			this.Element = element;
		}

		/// <summary>Gets an action that specifies how the collection changed.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.CollectionChangeAction" /> values.</returns>
		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001DDA RID: 7642 RVA: 0x00069A42 File Offset: 0x00067C42
		public virtual CollectionChangeAction Action
		{
			[CompilerGenerated]
			get
			{
				return this.<Action>k__BackingField;
			}
		}

		/// <summary>Gets the instance of the collection with the change.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the instance of the collection with the change, or <see langword="null" /> if you refresh the collection.</returns>
		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001DDB RID: 7643 RVA: 0x00069A4A File Offset: 0x00067C4A
		public virtual object Element
		{
			[CompilerGenerated]
			get
			{
				return this.<Element>k__BackingField;
			}
		}

		// Token: 0x04000EFA RID: 3834
		[CompilerGenerated]
		private readonly CollectionChangeAction <Action>k__BackingField;

		// Token: 0x04000EFB RID: 3835
		[CompilerGenerated]
		private readonly object <Element>k__BackingField;
	}
}
