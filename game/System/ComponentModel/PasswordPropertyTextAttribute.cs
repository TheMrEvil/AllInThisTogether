using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Indicates that an object's text representation is obscured by characters such as asterisks. This class cannot be inherited.</summary>
	// Token: 0x020003DE RID: 990
	[AttributeUsage(AttributeTargets.All)]
	public sealed class PasswordPropertyTextAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> class.</summary>
		// Token: 0x06002045 RID: 8261 RVA: 0x0006FCAA File Offset: 0x0006DEAA
		public PasswordPropertyTextAttribute() : this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> class, optionally showing password text.</summary>
		/// <param name="password">
		///   <see langword="true" /> to indicate that the property should be shown as password text; otherwise, <see langword="false" />. The default is <see langword="false" />.</param>
		// Token: 0x06002046 RID: 8262 RVA: 0x0006FCB3 File Offset: 0x0006DEB3
		public PasswordPropertyTextAttribute(bool password)
		{
			this.Password = password;
		}

		/// <summary>Gets a value indicating if the property for which the <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> is defined should be shown as password text.</summary>
		/// <returns>
		///   <see langword="true" /> if the property should be shown as password text; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06002047 RID: 8263 RVA: 0x0006FCC2 File Offset: 0x0006DEC2
		public bool Password
		{
			[CompilerGenerated]
			get
			{
				return this.<Password>k__BackingField;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> instances are equal.</summary>
		/// <param name="o">The <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> to compare with the current <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> is equal to the current <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002048 RID: 8264 RVA: 0x0006FCCA File Offset: 0x0006DECA
		public override bool Equals(object o)
		{
			return o is PasswordPropertyTextAttribute && ((PasswordPropertyTextAttribute)o).Password == this.Password;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" />.</returns>
		// Token: 0x06002049 RID: 8265 RVA: 0x000678FA File Offset: 0x00065AFA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns an indication whether the value of this instance is the default value.</summary>
		/// <returns>
		///   <see langword="true" /> if this instance is the default attribute for the class; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600204A RID: 8266 RVA: 0x0006FCE9 File Offset: 0x0006DEE9
		public override bool IsDefaultAttribute()
		{
			return this.Equals(PasswordPropertyTextAttribute.Default);
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x0006FCF6 File Offset: 0x0006DEF6
		// Note: this type is marked as 'beforefieldinit'.
		static PasswordPropertyTextAttribute()
		{
		}

		/// <summary>Specifies that a text property is used as a password. This <see langword="static" /> (<see langword="Shared" /> in Visual Basic) field is read-only.</summary>
		// Token: 0x04000FBB RID: 4027
		public static readonly PasswordPropertyTextAttribute Yes = new PasswordPropertyTextAttribute(true);

		/// <summary>Specifies that a text property is not used as a password. This <see langword="static" /> (<see langword="Shared" /> in Visual Basic) field is read-only.</summary>
		// Token: 0x04000FBC RID: 4028
		public static readonly PasswordPropertyTextAttribute No = new PasswordPropertyTextAttribute(false);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" />.</summary>
		// Token: 0x04000FBD RID: 4029
		public static readonly PasswordPropertyTextAttribute Default = PasswordPropertyTextAttribute.No;

		// Token: 0x04000FBE RID: 4030
		[CompilerGenerated]
		private readonly bool <Password>k__BackingField;
	}
}
