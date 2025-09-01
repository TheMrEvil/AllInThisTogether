using System;

namespace System.ComponentModel
{
	/// <summary>Indicates that the parent property is notified when the value of the property that this attribute is applied to is modified. This class cannot be inherited.</summary>
	// Token: 0x02000434 RID: 1076
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class NotifyParentPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.NotifyParentPropertyAttribute" /> class, using the specified value to determine whether the parent property is notified of changes to the value of the property.</summary>
		/// <param name="notifyParent">
		///   <see langword="true" /> if the parent should be notified of changes; otherwise, <see langword="false" />.</param>
		// Token: 0x0600235A RID: 9050 RVA: 0x00080C4E File Offset: 0x0007EE4E
		public NotifyParentPropertyAttribute(bool notifyParent)
		{
			this.notifyParent = notifyParent;
		}

		/// <summary>Gets or sets a value indicating whether the parent property should be notified of changes to the value of the property.</summary>
		/// <returns>
		///   <see langword="true" /> if the parent property should be notified of changes; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x0600235B RID: 9051 RVA: 0x00080C5D File Offset: 0x0007EE5D
		public bool NotifyParent
		{
			get
			{
				return this.notifyParent;
			}
		}

		/// <summary>Gets a value indicating whether the specified object is the same as the current object.</summary>
		/// <param name="obj">The object to test for equality.</param>
		/// <returns>
		///   <see langword="true" /> if the object is the same as this object; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600235C RID: 9052 RVA: 0x00080C65 File Offset: 0x0007EE65
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is NotifyParentPropertyAttribute && ((NotifyParentPropertyAttribute)obj).NotifyParent == this.notifyParent);
		}

		/// <summary>Gets the hash code for this object.</summary>
		/// <returns>The hash code for the object the attribute belongs to.</returns>
		// Token: 0x0600235D RID: 9053 RVA: 0x000678FA File Offset: 0x00065AFA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>
		///   <see langword="true" /> if the current value of the attribute is the default value of the attribute; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600235E RID: 9054 RVA: 0x00080C8D File Offset: 0x0007EE8D
		public override bool IsDefaultAttribute()
		{
			return this.Equals(NotifyParentPropertyAttribute.Default);
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x00080C9A File Offset: 0x0007EE9A
		// Note: this type is marked as 'beforefieldinit'.
		static NotifyParentPropertyAttribute()
		{
		}

		/// <summary>Indicates that the parent property is notified of changes to the value of the property. This field is read-only.</summary>
		// Token: 0x04001098 RID: 4248
		public static readonly NotifyParentPropertyAttribute Yes = new NotifyParentPropertyAttribute(true);

		/// <summary>Indicates that the parent property is not be notified of changes to the value of the property. This field is read-only.</summary>
		// Token: 0x04001099 RID: 4249
		public static readonly NotifyParentPropertyAttribute No = new NotifyParentPropertyAttribute(false);

		/// <summary>Indicates the default attribute state, that the property should not notify the parent property of changes to its value. This field is read-only.</summary>
		// Token: 0x0400109A RID: 4250
		public static readonly NotifyParentPropertyAttribute Default = NotifyParentPropertyAttribute.No;

		// Token: 0x0400109B RID: 4251
		private bool notifyParent;
	}
}
