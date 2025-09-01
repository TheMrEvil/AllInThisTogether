using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Specifies that a list can be used as a data source. A visual designer should use this attribute to determine whether to display a particular list in a data-binding picker. This class cannot be inherited.</summary>
	// Token: 0x020003CC RID: 972
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ListBindableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListBindableAttribute" /> class using a value to indicate whether the list is bindable.</summary>
		/// <param name="listBindable">
		///   <see langword="true" /> if the list is bindable; otherwise, <see langword="false" />.</param>
		// Token: 0x06001F6C RID: 8044 RVA: 0x0006D4BE File Offset: 0x0006B6BE
		public ListBindableAttribute(bool listBindable)
		{
			this.ListBindable = listBindable;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListBindableAttribute" /> class using <see cref="T:System.ComponentModel.BindableSupport" /> to indicate whether the list is bindable.</summary>
		/// <param name="flags">A <see cref="T:System.ComponentModel.BindableSupport" /> that indicates whether the list is bindable.</param>
		// Token: 0x06001F6D RID: 8045 RVA: 0x0006D4CD File Offset: 0x0006B6CD
		public ListBindableAttribute(BindableSupport flags)
		{
			this.ListBindable = (flags > BindableSupport.No);
			this._isDefault = (flags == BindableSupport.Default);
		}

		/// <summary>Gets whether the list is bindable.</summary>
		/// <returns>
		///   <see langword="true" /> if the list is bindable; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001F6E RID: 8046 RVA: 0x0006D4E9 File Offset: 0x0006B6E9
		public bool ListBindable
		{
			[CompilerGenerated]
			get
			{
				return this.<ListBindable>k__BackingField;
			}
		}

		/// <summary>Returns whether the object passed is equal to this <see cref="T:System.ComponentModel.ListBindableAttribute" />.</summary>
		/// <param name="obj">The object to test equality with.</param>
		/// <returns>
		///   <see langword="true" /> if the object passed is equal to this <see cref="T:System.ComponentModel.ListBindableAttribute" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001F6F RID: 8047 RVA: 0x0006D4F4 File Offset: 0x0006B6F4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ListBindableAttribute listBindableAttribute = obj as ListBindableAttribute;
			return listBindableAttribute != null && listBindableAttribute.ListBindable == this.ListBindable;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ListBindableAttribute" />.</returns>
		// Token: 0x06001F70 RID: 8048 RVA: 0x000678FA File Offset: 0x00065AFA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns whether <see cref="P:System.ComponentModel.ListBindableAttribute.ListBindable" /> is set to the default value.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="P:System.ComponentModel.ListBindableAttribute.ListBindable" /> is set to the default value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001F71 RID: 8049 RVA: 0x0006D521 File Offset: 0x0006B721
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ListBindableAttribute.Default) || this._isDefault;
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x0006D538 File Offset: 0x0006B738
		// Note: this type is marked as 'beforefieldinit'.
		static ListBindableAttribute()
		{
		}

		/// <summary>Specifies that the list is bindable. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000F59 RID: 3929
		public static readonly ListBindableAttribute Yes = new ListBindableAttribute(true);

		/// <summary>Specifies that the list is not bindable. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000F5A RID: 3930
		public static readonly ListBindableAttribute No = new ListBindableAttribute(false);

		/// <summary>Represents the default value for <see cref="T:System.ComponentModel.ListBindableAttribute" />.</summary>
		// Token: 0x04000F5B RID: 3931
		public static readonly ListBindableAttribute Default = ListBindableAttribute.Yes;

		// Token: 0x04000F5C RID: 3932
		private bool _isDefault;

		// Token: 0x04000F5D RID: 3933
		[CompilerGenerated]
		private readonly bool <ListBindable>k__BackingField;
	}
}
