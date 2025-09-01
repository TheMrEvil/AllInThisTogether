using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a member is typically used for binding. This class cannot be inherited.</summary>
	// Token: 0x02000382 RID: 898
	[AttributeUsage(AttributeTargets.All)]
	public sealed class BindableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindableAttribute" /> class with a Boolean value.</summary>
		/// <param name="bindable">
		///   <see langword="true" /> to use property for binding; otherwise, <see langword="false" />.</param>
		// Token: 0x06001D81 RID: 7553 RVA: 0x00069135 File Offset: 0x00067335
		public BindableAttribute(bool bindable) : this(bindable, BindingDirection.OneWay)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindableAttribute" /> class.</summary>
		/// <param name="bindable">
		///   <see langword="true" /> to use property for binding; otherwise, <see langword="false" />.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.BindingDirection" /> values.</param>
		// Token: 0x06001D82 RID: 7554 RVA: 0x0006913F File Offset: 0x0006733F
		public BindableAttribute(bool bindable, BindingDirection direction)
		{
			this.Bindable = bindable;
			this.Direction = direction;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindableAttribute" /> class with one of the <see cref="T:System.ComponentModel.BindableSupport" /> values.</summary>
		/// <param name="flags">One of the <see cref="T:System.ComponentModel.BindableSupport" /> values.</param>
		// Token: 0x06001D83 RID: 7555 RVA: 0x00069155 File Offset: 0x00067355
		public BindableAttribute(BindableSupport flags) : this(flags, BindingDirection.OneWay)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindableAttribute" /> class.</summary>
		/// <param name="flags">One of the <see cref="T:System.ComponentModel.BindableSupport" /> values.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.BindingDirection" /> values.</param>
		// Token: 0x06001D84 RID: 7556 RVA: 0x0006915F File Offset: 0x0006735F
		public BindableAttribute(BindableSupport flags, BindingDirection direction)
		{
			this.Bindable = (flags > BindableSupport.No);
			this._isDefault = (flags == BindableSupport.Default);
			this.Direction = direction;
		}

		/// <summary>Gets a value indicating that a property is typically used for binding.</summary>
		/// <returns>
		///   <see langword="true" /> if the property is typically used for binding; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001D85 RID: 7557 RVA: 0x00069182 File Offset: 0x00067382
		public bool Bindable
		{
			[CompilerGenerated]
			get
			{
				return this.<Bindable>k__BackingField;
			}
		}

		/// <summary>Gets a value indicating the direction or directions of this property's data binding.</summary>
		/// <returns>The direction of this property's data binding.</returns>
		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001D86 RID: 7558 RVA: 0x0006918A File Offset: 0x0006738A
		public BindingDirection Direction
		{
			[CompilerGenerated]
			get
			{
				return this.<Direction>k__BackingField;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.BindableAttribute" /> objects are equal.</summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.ComponentModel.BindableAttribute" /> is equal to the current <see cref="T:System.ComponentModel.BindableAttribute" />; <see langword="false" /> if it is not equal.</returns>
		// Token: 0x06001D87 RID: 7559 RVA: 0x00069192 File Offset: 0x00067392
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is BindableAttribute && ((BindableAttribute)obj).Bindable == this.Bindable);
		}

		/// <summary>Serves as a hash function for the <see cref="T:System.ComponentModel.BindableAttribute" /> class.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.BindableAttribute" />.</returns>
		// Token: 0x06001D88 RID: 7560 RVA: 0x000691BC File Offset: 0x000673BC
		public override int GetHashCode()
		{
			return this.Bindable.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>
		///   <see langword="true" /> if the attribute is the default value for this attribute class; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D89 RID: 7561 RVA: 0x000691D7 File Offset: 0x000673D7
		public override bool IsDefaultAttribute()
		{
			return this.Equals(BindableAttribute.Default) || this._isDefault;
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x000691EE File Offset: 0x000673EE
		// Note: this type is marked as 'beforefieldinit'.
		static BindableAttribute()
		{
		}

		/// <summary>Specifies that a property is typically used for binding. This field is read-only.</summary>
		// Token: 0x04000EDC RID: 3804
		public static readonly BindableAttribute Yes = new BindableAttribute(true);

		/// <summary>Specifies that a property is not typically used for binding. This field is read-only.</summary>
		// Token: 0x04000EDD RID: 3805
		public static readonly BindableAttribute No = new BindableAttribute(false);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.BindableAttribute" />, which is <see cref="F:System.ComponentModel.BindableAttribute.No" />. This field is read-only.</summary>
		// Token: 0x04000EDE RID: 3806
		public static readonly BindableAttribute Default = BindableAttribute.No;

		// Token: 0x04000EDF RID: 3807
		private bool _isDefault;

		// Token: 0x04000EE0 RID: 3808
		[CompilerGenerated]
		private readonly bool <Bindable>k__BackingField;

		// Token: 0x04000EE1 RID: 3809
		[CompilerGenerated]
		private readonly BindingDirection <Direction>k__BackingField;
	}
}
