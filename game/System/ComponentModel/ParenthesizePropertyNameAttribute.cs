using System;

namespace System.ComponentModel
{
	/// <summary>Indicates whether the name of the associated property is displayed with parentheses in the Properties window. This class cannot be inherited.</summary>
	// Token: 0x02000435 RID: 1077
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ParenthesizePropertyNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ParenthesizePropertyNameAttribute" /> class that indicates that the associated property should not be shown with parentheses.</summary>
		// Token: 0x06002360 RID: 9056 RVA: 0x00080CBC File Offset: 0x0007EEBC
		public ParenthesizePropertyNameAttribute() : this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ParenthesizePropertyNameAttribute" /> class, using the specified value to indicate whether the attribute is displayed with parentheses.</summary>
		/// <param name="needParenthesis">
		///   <see langword="true" /> if the name should be enclosed in parentheses; otherwise, <see langword="false" />.</param>
		// Token: 0x06002361 RID: 9057 RVA: 0x00080CC5 File Offset: 0x0007EEC5
		public ParenthesizePropertyNameAttribute(bool needParenthesis)
		{
			this.needParenthesis = needParenthesis;
		}

		/// <summary>Gets a value indicating whether the Properties window displays the name of the property in parentheses in the Properties window.</summary>
		/// <returns>
		///   <see langword="true" /> if the property is displayed with parentheses; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06002362 RID: 9058 RVA: 0x00080CD4 File Offset: 0x0007EED4
		public bool NeedParenthesis
		{
			get
			{
				return this.needParenthesis;
			}
		}

		/// <summary>Compares the specified object to this object and tests for equality.</summary>
		/// <param name="o">The object to be compared.</param>
		/// <returns>
		///   <see langword="true" /> if equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002363 RID: 9059 RVA: 0x00080CDC File Offset: 0x0007EEDC
		public override bool Equals(object o)
		{
			return o is ParenthesizePropertyNameAttribute && ((ParenthesizePropertyNameAttribute)o).NeedParenthesis == this.needParenthesis;
		}

		/// <summary>Gets the hash code for this object.</summary>
		/// <returns>The hash code for the object the attribute belongs to.</returns>
		// Token: 0x06002364 RID: 9060 RVA: 0x000678FA File Offset: 0x00065AFA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>
		///   <see langword="true" /> if the current value of the attribute is the default value of the attribute; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002365 RID: 9061 RVA: 0x00080CFB File Offset: 0x0007EEFB
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ParenthesizePropertyNameAttribute.Default);
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x00080D08 File Offset: 0x0007EF08
		// Note: this type is marked as 'beforefieldinit'.
		static ParenthesizePropertyNameAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ParenthesizePropertyNameAttribute" /> class with a default value that indicates that the associated property should not be shown with parentheses. This field is read-only.</summary>
		// Token: 0x0400109C RID: 4252
		public static readonly ParenthesizePropertyNameAttribute Default = new ParenthesizePropertyNameAttribute();

		// Token: 0x0400109D RID: 4253
		private bool needParenthesis;
	}
}
