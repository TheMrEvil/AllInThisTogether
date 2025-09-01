using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Specifies that this property can be combined with properties belonging to other objects in a Properties window.</summary>
	// Token: 0x02000376 RID: 886
	[AttributeUsage(AttributeTargets.All)]
	public sealed class MergablePropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MergablePropertyAttribute" /> class.</summary>
		/// <param name="allowMerge">
		///   <see langword="true" /> if this property can be combined with properties belonging to other objects in a Properties window; otherwise, <see langword="false" />.</param>
		// Token: 0x06001D33 RID: 7475 RVA: 0x0006863E File Offset: 0x0006683E
		public MergablePropertyAttribute(bool allowMerge)
		{
			this.AllowMerge = allowMerge;
		}

		/// <summary>Gets a value indicating whether this property can be combined with properties belonging to other objects in a Properties window.</summary>
		/// <returns>
		///   <see langword="true" /> if this property can be combined with properties belonging to other objects in a Properties window; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001D34 RID: 7476 RVA: 0x0006864D File Offset: 0x0006684D
		public bool AllowMerge
		{
			[CompilerGenerated]
			get
			{
				return this.<AllowMerge>k__BackingField;
			}
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">Another object to compare to.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is equal to this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D35 RID: 7477 RVA: 0x00068658 File Offset: 0x00066858
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			MergablePropertyAttribute mergablePropertyAttribute = obj as MergablePropertyAttribute;
			bool? flag = (mergablePropertyAttribute != null) ? new bool?(mergablePropertyAttribute.AllowMerge) : null;
			bool allowMerge = this.AllowMerge;
			return flag.GetValueOrDefault() == allowMerge & flag != null;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.MergablePropertyAttribute" />.</returns>
		// Token: 0x06001D36 RID: 7478 RVA: 0x000678FA File Offset: 0x00065AFA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>
		///   <see langword="true" /> if the attribute is the default value for this attribute class; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D37 RID: 7479 RVA: 0x000686A4 File Offset: 0x000668A4
		public override bool IsDefaultAttribute()
		{
			return this.Equals(MergablePropertyAttribute.Default);
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x000686B1 File Offset: 0x000668B1
		// Note: this type is marked as 'beforefieldinit'.
		static MergablePropertyAttribute()
		{
		}

		/// <summary>Specifies that a property can be combined with properties belonging to other objects in a Properties window. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000EC6 RID: 3782
		public static readonly MergablePropertyAttribute Yes = new MergablePropertyAttribute(true);

		/// <summary>Specifies that a property cannot be combined with properties belonging to other objects in a Properties window. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000EC7 RID: 3783
		public static readonly MergablePropertyAttribute No = new MergablePropertyAttribute(false);

		/// <summary>Specifies the default value, which is <see cref="F:System.ComponentModel.MergablePropertyAttribute.Yes" />, that is a property can be combined with properties belonging to other objects in a Properties window. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000EC8 RID: 3784
		public static readonly MergablePropertyAttribute Default = MergablePropertyAttribute.Yes;

		// Token: 0x04000EC9 RID: 3785
		[CompilerGenerated]
		private readonly bool <AllowMerge>k__BackingField;
	}
}
