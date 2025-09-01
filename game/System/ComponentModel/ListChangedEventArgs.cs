using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> event.</summary>
	// Token: 0x020003CD RID: 973
	public class ListChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListChangedEventArgs" /> class given the type of change and the index of the affected item.</summary>
		/// <param name="listChangedType">A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</param>
		/// <param name="newIndex">The index of the item that was added, changed, or removed.</param>
		// Token: 0x06001F73 RID: 8051 RVA: 0x0006D55A File Offset: 0x0006B75A
		public ListChangedEventArgs(ListChangedType listChangedType, int newIndex) : this(listChangedType, newIndex, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListChangedEventArgs" /> class given the type of change, the index of the affected item, and a <see cref="T:System.ComponentModel.PropertyDescriptor" /> describing the affected item.</summary>
		/// <param name="listChangedType">A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</param>
		/// <param name="newIndex">The index of the item that was added or changed.</param>
		/// <param name="propDesc">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> describing the item.</param>
		// Token: 0x06001F74 RID: 8052 RVA: 0x0006D565 File Offset: 0x0006B765
		public ListChangedEventArgs(ListChangedType listChangedType, int newIndex, PropertyDescriptor propDesc) : this(listChangedType, newIndex)
		{
			this.PropertyDescriptor = propDesc;
			this.OldIndex = newIndex;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListChangedEventArgs" /> class given the type of change and the <see cref="T:System.ComponentModel.PropertyDescriptor" /> affected.</summary>
		/// <param name="listChangedType">A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</param>
		/// <param name="propDesc">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> that was added, removed, or changed.</param>
		// Token: 0x06001F75 RID: 8053 RVA: 0x0006D57D File Offset: 0x0006B77D
		public ListChangedEventArgs(ListChangedType listChangedType, PropertyDescriptor propDesc)
		{
			this.ListChangedType = listChangedType;
			this.PropertyDescriptor = propDesc;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListChangedEventArgs" /> class given the type of change and the old and new index of the item that was moved.</summary>
		/// <param name="listChangedType">A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</param>
		/// <param name="newIndex">The new index of the item that was moved.</param>
		/// <param name="oldIndex">The old index of the item that was moved.</param>
		// Token: 0x06001F76 RID: 8054 RVA: 0x0006D593 File Offset: 0x0006B793
		public ListChangedEventArgs(ListChangedType listChangedType, int newIndex, int oldIndex)
		{
			this.ListChangedType = listChangedType;
			this.NewIndex = newIndex;
			this.OldIndex = oldIndex;
		}

		/// <summary>Gets the type of change.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</returns>
		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001F77 RID: 8055 RVA: 0x0006D5B0 File Offset: 0x0006B7B0
		public ListChangedType ListChangedType
		{
			[CompilerGenerated]
			get
			{
				return this.<ListChangedType>k__BackingField;
			}
		}

		/// <summary>Gets the index of the item affected by the change.</summary>
		/// <returns>The index of the affected by the change.</returns>
		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001F78 RID: 8056 RVA: 0x0006D5B8 File Offset: 0x0006B7B8
		public int NewIndex
		{
			[CompilerGenerated]
			get
			{
				return this.<NewIndex>k__BackingField;
			}
		}

		/// <summary>Gets the old index of an item that has been moved.</summary>
		/// <returns>The old index of the moved item.</returns>
		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001F79 RID: 8057 RVA: 0x0006D5C0 File Offset: 0x0006B7C0
		public int OldIndex
		{
			[CompilerGenerated]
			get
			{
				return this.<OldIndex>k__BackingField;
			}
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that was added, changed, or deleted.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> affected by the change.</returns>
		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001F7A RID: 8058 RVA: 0x0006D5C8 File Offset: 0x0006B7C8
		public PropertyDescriptor PropertyDescriptor
		{
			[CompilerGenerated]
			get
			{
				return this.<PropertyDescriptor>k__BackingField;
			}
		}

		// Token: 0x04000F5E RID: 3934
		[CompilerGenerated]
		private readonly ListChangedType <ListChangedType>k__BackingField;

		// Token: 0x04000F5F RID: 3935
		[CompilerGenerated]
		private readonly int <NewIndex>k__BackingField;

		// Token: 0x04000F60 RID: 3936
		[CompilerGenerated]
		private readonly int <OldIndex>k__BackingField;

		// Token: 0x04000F61 RID: 3937
		[CompilerGenerated]
		private readonly PropertyDescriptor <PropertyDescriptor>k__BackingField;
	}
}
