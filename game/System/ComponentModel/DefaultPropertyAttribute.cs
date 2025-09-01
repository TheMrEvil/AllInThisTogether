using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Specifies the default property for a component.</summary>
	// Token: 0x0200039C RID: 924
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DefaultPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultPropertyAttribute" /> class.</summary>
		/// <param name="name">The name of the default property for the component this attribute is bound to.</param>
		// Token: 0x06001E3D RID: 7741 RVA: 0x0006BA43 File Offset: 0x00069C43
		public DefaultPropertyAttribute(string name)
		{
			this.Name = name;
		}

		/// <summary>Gets the name of the default property for the component this attribute is bound to.</summary>
		/// <returns>The name of the default property for the component this attribute is bound to. The default value is <see langword="null" />.</returns>
		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001E3E RID: 7742 RVA: 0x0006BA52 File Offset: 0x00069C52
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DefaultPropertyAttribute" />.</summary>
		/// <param name="obj">The object to test the value equality of.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the given object is equal to that of the current; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001E3F RID: 7743 RVA: 0x0006BA5C File Offset: 0x00069C5C
		public override bool Equals(object obj)
		{
			DefaultPropertyAttribute defaultPropertyAttribute = obj as DefaultPropertyAttribute;
			return defaultPropertyAttribute != null && defaultPropertyAttribute.Name == this.Name;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001E40 RID: 7744 RVA: 0x000678FA File Offset: 0x00065AFA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x0006BA86 File Offset: 0x00069C86
		// Note: this type is marked as 'beforefieldinit'.
		static DefaultPropertyAttribute()
		{
		}

		// Token: 0x04000F1A RID: 3866
		[CompilerGenerated]
		private readonly string <Name>k__BackingField;

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DefaultPropertyAttribute" />, which is <see langword="null" />. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000F1B RID: 3867
		public static readonly DefaultPropertyAttribute Default = new DefaultPropertyAttribute(null);
	}
}
