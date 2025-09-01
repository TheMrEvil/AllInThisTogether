using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Specifies that the designer for a class belongs to a certain category.</summary>
	// Token: 0x02000368 RID: 872
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class DesignerCategoryAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerCategoryAttribute" /> class with an empty string ("").</summary>
		// Token: 0x06001CF0 RID: 7408 RVA: 0x00068145 File Offset: 0x00066345
		public DesignerCategoryAttribute()
		{
			this.Category = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerCategoryAttribute" /> class with the given category name.</summary>
		/// <param name="category">The name of the category.</param>
		// Token: 0x06001CF1 RID: 7409 RVA: 0x00068158 File Offset: 0x00066358
		public DesignerCategoryAttribute(string category)
		{
			this.Category = category;
		}

		/// <summary>Gets the name of the category.</summary>
		/// <returns>The name of the category.</returns>
		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001CF2 RID: 7410 RVA: 0x00068167 File Offset: 0x00066367
		public string Category
		{
			[CompilerGenerated]
			get
			{
				return this.<Category>k__BackingField;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DesignOnlyAttribute" />.</summary>
		/// <param name="obj">The object to test the value equality of.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the given object is equal to that of the current; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001CF3 RID: 7411 RVA: 0x00068170 File Offset: 0x00066370
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignerCategoryAttribute designerCategoryAttribute = obj as DesignerCategoryAttribute;
			return designerCategoryAttribute != null && designerCategoryAttribute.Category == this.Category;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001CF4 RID: 7412 RVA: 0x000681A0 File Offset: 0x000663A0
		public override int GetHashCode()
		{
			return this.Category.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>
		///   <see langword="true" /> if the attribute is the default value for this attribute class; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001CF5 RID: 7413 RVA: 0x000681AD File Offset: 0x000663AD
		public override bool IsDefaultAttribute()
		{
			return this.Category.Equals(DesignerCategoryAttribute.Default.Category);
		}

		/// <summary>Gets a unique identifier for this attribute.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is a unique identifier for the attribute.</returns>
		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001CF6 RID: 7414 RVA: 0x000681C4 File Offset: 0x000663C4
		public override object TypeId
		{
			get
			{
				return base.GetType().FullName + this.Category;
			}
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x000681DC File Offset: 0x000663DC
		// Note: this type is marked as 'beforefieldinit'.
		static DesignerCategoryAttribute()
		{
		}

		/// <summary>Specifies that a component marked with this category use a component designer. This field is read-only.</summary>
		// Token: 0x04000EA8 RID: 3752
		public static readonly DesignerCategoryAttribute Component = new DesignerCategoryAttribute("Component");

		/// <summary>Specifies that a component marked with this category cannot use a visual designer. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000EA9 RID: 3753
		public static readonly DesignerCategoryAttribute Default = new DesignerCategoryAttribute();

		/// <summary>Specifies that a component marked with this category use a form designer. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000EAA RID: 3754
		public static readonly DesignerCategoryAttribute Form = new DesignerCategoryAttribute("Form");

		/// <summary>Specifies that a component marked with this category use a generic designer. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000EAB RID: 3755
		public static readonly DesignerCategoryAttribute Generic = new DesignerCategoryAttribute("Designer");

		// Token: 0x04000EAC RID: 3756
		[CompilerGenerated]
		private readonly string <Category>k__BackingField;
	}
}
