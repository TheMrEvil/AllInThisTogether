using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Specifies the default event for a component.</summary>
	// Token: 0x0200039B RID: 923
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DefaultEventAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultEventAttribute" /> class.</summary>
		/// <param name="name">The name of the default event for the component this attribute is bound to.</param>
		// Token: 0x06001E38 RID: 7736 RVA: 0x0006B9F2 File Offset: 0x00069BF2
		public DefaultEventAttribute(string name)
		{
			this.Name = name;
		}

		/// <summary>Gets the name of the default event for the component this attribute is bound to.</summary>
		/// <returns>The name of the default event for the component this attribute is bound to. The default value is <see langword="null" />.</returns>
		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001E39 RID: 7737 RVA: 0x0006BA01 File Offset: 0x00069C01
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DefaultEventAttribute" />.</summary>
		/// <param name="obj">The object to test the value equality of.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the given object is equal to that of the current; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001E3A RID: 7738 RVA: 0x0006BA0C File Offset: 0x00069C0C
		public override bool Equals(object obj)
		{
			DefaultEventAttribute defaultEventAttribute = obj as DefaultEventAttribute;
			return defaultEventAttribute != null && defaultEventAttribute.Name == this.Name;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001E3B RID: 7739 RVA: 0x000678FA File Offset: 0x00065AFA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x0006BA36 File Offset: 0x00069C36
		// Note: this type is marked as 'beforefieldinit'.
		static DefaultEventAttribute()
		{
		}

		// Token: 0x04000F18 RID: 3864
		[CompilerGenerated]
		private readonly string <Name>k__BackingField;

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DefaultEventAttribute" />, which is <see langword="null" />. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000F19 RID: 3865
		public static readonly DefaultEventAttribute Default = new DefaultEventAttribute(null);
	}
}
