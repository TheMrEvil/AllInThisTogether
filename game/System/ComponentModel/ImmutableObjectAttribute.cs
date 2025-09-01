using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Specifies that an object has no subproperties capable of being edited. This class cannot be inherited.</summary>
	// Token: 0x02000372 RID: 882
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ImmutableObjectAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ImmutableObjectAttribute" /> class.</summary>
		/// <param name="immutable">
		///   <see langword="true" /> if the object is immutable; otherwise, <see langword="false" />.</param>
		// Token: 0x06001D20 RID: 7456 RVA: 0x000684AB File Offset: 0x000666AB
		public ImmutableObjectAttribute(bool immutable)
		{
			this.Immutable = immutable;
		}

		/// <summary>Gets whether the object is immutable.</summary>
		/// <returns>
		///   <see langword="true" /> if the object is immutable; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001D21 RID: 7457 RVA: 0x000684BA File Offset: 0x000666BA
		public bool Immutable
		{
			[CompilerGenerated]
			get
			{
				return this.<Immutable>k__BackingField;
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or a null reference (<see langword="Nothing" /> in Visual Basic).</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D22 RID: 7458 RVA: 0x000684C4 File Offset: 0x000666C4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ImmutableObjectAttribute immutableObjectAttribute = obj as ImmutableObjectAttribute;
			bool? flag = (immutableObjectAttribute != null) ? new bool?(immutableObjectAttribute.Immutable) : null;
			bool immutable = this.Immutable;
			return flag.GetValueOrDefault() == immutable & flag != null;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ImmutableObjectAttribute" />.</returns>
		// Token: 0x06001D23 RID: 7459 RVA: 0x000678FA File Offset: 0x00065AFA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether the value of this instance is the default value.</summary>
		/// <returns>
		///   <see langword="true" /> if this instance is the default attribute for the class; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D24 RID: 7460 RVA: 0x00068510 File Offset: 0x00066710
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ImmutableObjectAttribute.Default);
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x0006851D File Offset: 0x0006671D
		// Note: this type is marked as 'beforefieldinit'.
		static ImmutableObjectAttribute()
		{
		}

		/// <summary>Specifies that an object has no subproperties that can be edited. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000EBD RID: 3773
		public static readonly ImmutableObjectAttribute Yes = new ImmutableObjectAttribute(true);

		/// <summary>Specifies that an object has at least one editable subproperty. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000EBE RID: 3774
		public static readonly ImmutableObjectAttribute No = new ImmutableObjectAttribute(false);

		/// <summary>Represents the default value for <see cref="T:System.ComponentModel.ImmutableObjectAttribute" />.</summary>
		// Token: 0x04000EBF RID: 3775
		public static readonly ImmutableObjectAttribute Default = ImmutableObjectAttribute.No;

		// Token: 0x04000EC0 RID: 3776
		[CompilerGenerated]
		private readonly bool <Immutable>k__BackingField;
	}
}
