using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel.Design
{
	/// <summary>Specifies the context keyword for a class or member. This class cannot be inherited.</summary>
	// Token: 0x02000454 RID: 1108
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public sealed class HelpKeywordAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> class.</summary>
		// Token: 0x060023F4 RID: 9204 RVA: 0x00003D9F File Offset: 0x00001F9F
		public HelpKeywordAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> class.</summary>
		/// <param name="keyword">The Help keyword value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is <see langword="null" />.</exception>
		// Token: 0x060023F5 RID: 9205 RVA: 0x00081825 File Offset: 0x0007FA25
		public HelpKeywordAttribute(string keyword)
		{
			if (keyword == null)
			{
				throw new ArgumentNullException("keyword");
			}
			this.HelpKeyword = keyword;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> class from the given type.</summary>
		/// <param name="t">The type from which the Help keyword will be taken.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="t" /> is <see langword="null" />.</exception>
		// Token: 0x060023F6 RID: 9206 RVA: 0x00081842 File Offset: 0x0007FA42
		public HelpKeywordAttribute(Type t)
		{
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			this.HelpKeyword = t.FullName;
		}

		/// <summary>Gets the Help keyword supplied by this attribute.</summary>
		/// <returns>The Help keyword supplied by this attribute.</returns>
		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x060023F7 RID: 9207 RVA: 0x0008186A File Offset: 0x0007FA6A
		public string HelpKeyword
		{
			[CompilerGenerated]
			get
			{
				return this.<HelpKeyword>k__BackingField;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> instances are equal.</summary>
		/// <param name="obj">The <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> to compare with the current <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> is equal to the current <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060023F8 RID: 9208 RVA: 0x00081872 File Offset: 0x0007FA72
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is HelpKeywordAttribute && ((HelpKeywordAttribute)obj).HelpKeyword == this.HelpKeyword);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" />.</returns>
		// Token: 0x060023F9 RID: 9209 RVA: 0x000678FA File Offset: 0x00065AFA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determines whether the Help keyword is <see langword="null" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the Help keyword is <see langword="null" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060023FA RID: 9210 RVA: 0x0008189D File Offset: 0x0007FA9D
		public override bool IsDefaultAttribute()
		{
			return this.Equals(HelpKeywordAttribute.Default);
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x000818AA File Offset: 0x0007FAAA
		// Note: this type is marked as 'beforefieldinit'.
		static HelpKeywordAttribute()
		{
		}

		/// <summary>Represents the default value for <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" />. This field is read-only.</summary>
		// Token: 0x040010CB RID: 4299
		public static readonly HelpKeywordAttribute Default = new HelpKeywordAttribute();

		// Token: 0x040010CC RID: 4300
		[CompilerGenerated]
		private readonly string <HelpKeyword>k__BackingField;
	}
}
