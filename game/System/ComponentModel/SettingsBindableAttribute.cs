using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Specifies when a component property can be bound to an application setting.</summary>
	// Token: 0x020003EB RID: 1003
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class SettingsBindableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.SettingsBindableAttribute" /> class.</summary>
		/// <param name="bindable">
		///   <see langword="true" /> to specify that a property is appropriate to bind settings to; otherwise, <see langword="false" />.</param>
		// Token: 0x060020DC RID: 8412 RVA: 0x0007182D File Offset: 0x0006FA2D
		public SettingsBindableAttribute(bool bindable)
		{
			this.Bindable = bindable;
		}

		/// <summary>Gets a value indicating whether a property is appropriate to bind settings to.</summary>
		/// <returns>
		///   <see langword="true" /> if the property is appropriate to bind settings to; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x060020DD RID: 8413 RVA: 0x0007183C File Offset: 0x0006FA3C
		public bool Bindable
		{
			[CompilerGenerated]
			get
			{
				return this.<Bindable>k__BackingField;
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or a null reference (<see langword="Nothing" /> in Visual Basic).</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060020DE RID: 8414 RVA: 0x00071844 File Offset: 0x0006FA44
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is SettingsBindableAttribute && ((SettingsBindableAttribute)obj).Bindable == this.Bindable);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060020DF RID: 8415 RVA: 0x0007186C File Offset: 0x0006FA6C
		public override int GetHashCode()
		{
			return this.Bindable.GetHashCode();
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x00071887 File Offset: 0x0006FA87
		// Note: this type is marked as 'beforefieldinit'.
		static SettingsBindableAttribute()
		{
		}

		/// <summary>Specifies that a property is appropriate to bind settings to.</summary>
		// Token: 0x04000FE6 RID: 4070
		public static readonly SettingsBindableAttribute Yes = new SettingsBindableAttribute(true);

		/// <summary>Specifies that a property is not appropriate to bind settings to.</summary>
		// Token: 0x04000FE7 RID: 4071
		public static readonly SettingsBindableAttribute No = new SettingsBindableAttribute(false);

		// Token: 0x04000FE8 RID: 4072
		[CompilerGenerated]
		private readonly bool <Bindable>k__BackingField;
	}
}
