using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Provides a description of the sort operation applied to a data source.</summary>
	// Token: 0x020003D0 RID: 976
	public class ListSortDescription
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListSortDescription" /> class with the specified property description and direction.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> that describes the property by which the data source is sorted.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDescription" /> values.</param>
		// Token: 0x06001F7F RID: 8063 RVA: 0x0006D5D0 File Offset: 0x0006B7D0
		public ListSortDescription(PropertyDescriptor property, ListSortDirection direction)
		{
			this.PropertyDescriptor = property;
			this.SortDirection = direction;
		}

		/// <summary>Gets or sets the abstract description of a class property associated with this <see cref="T:System.ComponentModel.ListSortDescription" /></summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> associated with this <see cref="T:System.ComponentModel.ListSortDescription" />.</returns>
		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001F80 RID: 8064 RVA: 0x0006D5E6 File Offset: 0x0006B7E6
		// (set) Token: 0x06001F81 RID: 8065 RVA: 0x0006D5EE File Offset: 0x0006B7EE
		public PropertyDescriptor PropertyDescriptor
		{
			[CompilerGenerated]
			get
			{
				return this.<PropertyDescriptor>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PropertyDescriptor>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the direction of the sort operation associated with this <see cref="T:System.ComponentModel.ListSortDescription" />.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values.</returns>
		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001F82 RID: 8066 RVA: 0x0006D5F7 File Offset: 0x0006B7F7
		// (set) Token: 0x06001F83 RID: 8067 RVA: 0x0006D5FF File Offset: 0x0006B7FF
		public ListSortDirection SortDirection
		{
			[CompilerGenerated]
			get
			{
				return this.<SortDirection>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SortDirection>k__BackingField = value;
			}
		}

		// Token: 0x04000F6B RID: 3947
		[CompilerGenerated]
		private PropertyDescriptor <PropertyDescriptor>k__BackingField;

		// Token: 0x04000F6C RID: 3948
		[CompilerGenerated]
		private ListSortDirection <SortDirection>k__BackingField;
	}
}
