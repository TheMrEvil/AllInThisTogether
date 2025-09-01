using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>
	///   <see cref="T:System.ComponentModel.DesignTimeVisibleAttribute" /> marks a component's visibility. If <see cref="F:System.ComponentModel.DesignTimeVisibleAttribute.Yes" /> is present, a visual designer can show this component on a designer.</summary>
	// Token: 0x020003A3 RID: 931
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
	public sealed class DesignTimeVisibleAttribute : Attribute
	{
		/// <summary>Creates a new <see cref="T:System.ComponentModel.DesignTimeVisibleAttribute" /> with the <see cref="P:System.ComponentModel.DesignTimeVisibleAttribute.Visible" /> property set to the given value in <paramref name="visible" />.</summary>
		/// <param name="visible">The value that the <see cref="P:System.ComponentModel.DesignTimeVisibleAttribute.Visible" /> property will be set against.</param>
		// Token: 0x06001E6D RID: 7789 RVA: 0x0006C0B4 File Offset: 0x0006A2B4
		public DesignTimeVisibleAttribute(bool visible)
		{
			this.Visible = visible;
		}

		/// <summary>Creates a new <see cref="T:System.ComponentModel.DesignTimeVisibleAttribute" /> set to the default value of <see langword="false" />.</summary>
		// Token: 0x06001E6E RID: 7790 RVA: 0x00003D9F File Offset: 0x00001F9F
		public DesignTimeVisibleAttribute()
		{
		}

		/// <summary>Gets or sets whether the component should be shown at design time.</summary>
		/// <returns>
		///   <see langword="true" /> if this component should be shown at design time, or <see langword="false" /> if it shouldn't.</returns>
		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001E6F RID: 7791 RVA: 0x0006C0C3 File Offset: 0x0006A2C3
		public bool Visible
		{
			[CompilerGenerated]
			get
			{
				return this.<Visible>k__BackingField;
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An Object to compare with this instance or a null reference (<see langword="Nothing" /> in Visual Basic).</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001E70 RID: 7792 RVA: 0x0006C0CC File Offset: 0x0006A2CC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignTimeVisibleAttribute designTimeVisibleAttribute = obj as DesignTimeVisibleAttribute;
			return designTimeVisibleAttribute != null && designTimeVisibleAttribute.Visible == this.Visible;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001E71 RID: 7793 RVA: 0x0006C0F9 File Offset: 0x0006A2F9
		public override int GetHashCode()
		{
			return typeof(DesignTimeVisibleAttribute).GetHashCode() ^ (this.Visible ? -1 : 0);
		}

		/// <summary>Gets a value indicating if this instance is equal to the <see cref="F:System.ComponentModel.DesignTimeVisibleAttribute.Default" /> value.</summary>
		/// <returns>
		///   <see langword="true" />, if this instance is equal to the <see cref="F:System.ComponentModel.DesignTimeVisibleAttribute.Default" /> value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001E72 RID: 7794 RVA: 0x0006C117 File Offset: 0x0006A317
		public override bool IsDefaultAttribute()
		{
			return this.Visible == DesignTimeVisibleAttribute.Default.Visible;
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x0006C12B File Offset: 0x0006A32B
		// Note: this type is marked as 'beforefieldinit'.
		static DesignTimeVisibleAttribute()
		{
		}

		// Token: 0x04000F32 RID: 3890
		[CompilerGenerated]
		private readonly bool <Visible>k__BackingField;

		/// <summary>Marks a component as visible in a visual designer.</summary>
		// Token: 0x04000F33 RID: 3891
		public static readonly DesignTimeVisibleAttribute Yes = new DesignTimeVisibleAttribute(true);

		/// <summary>Marks a component as not visible in a visual designer.</summary>
		// Token: 0x04000F34 RID: 3892
		public static readonly DesignTimeVisibleAttribute No = new DesignTimeVisibleAttribute(false);

		/// <summary>The default visibility which is <see langword="Yes" />.</summary>
		// Token: 0x04000F35 RID: 3893
		public static readonly DesignTimeVisibleAttribute Default = DesignTimeVisibleAttribute.Yes;
	}
}
