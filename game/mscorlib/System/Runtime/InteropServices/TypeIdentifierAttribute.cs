using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides support for type equivalence.</summary>
	// Token: 0x020006E3 RID: 1763
	[ComVisible(false)]
	[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	public sealed class TypeIdentifierAttribute : Attribute
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Runtime.InteropServices.TypeIdentifierAttribute" /> class.</summary>
		// Token: 0x0600405D RID: 16477 RVA: 0x00002050 File Offset: 0x00000250
		public TypeIdentifierAttribute()
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Runtime.InteropServices.TypeIdentifierAttribute" /> class with the specified scope and identifier.</summary>
		/// <param name="scope">The first type equivalence string.</param>
		/// <param name="identifier">The second type equivalence string.</param>
		// Token: 0x0600405E RID: 16478 RVA: 0x000E0F19 File Offset: 0x000DF119
		public TypeIdentifierAttribute(string scope, string identifier)
		{
			this.Scope_ = scope;
			this.Identifier_ = identifier;
		}

		/// <summary>Gets the value of the <paramref name="scope" /> parameter that was passed to the <see cref="M:System.Runtime.InteropServices.TypeIdentifierAttribute.#ctor(System.String,System.String)" /> constructor.</summary>
		/// <returns>The value of the constructor's <paramref name="scope" /> parameter.</returns>
		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x0600405F RID: 16479 RVA: 0x000E0F2F File Offset: 0x000DF12F
		public string Scope
		{
			get
			{
				return this.Scope_;
			}
		}

		/// <summary>Gets the value of the <paramref name="identifier" /> parameter that was passed to the <see cref="M:System.Runtime.InteropServices.TypeIdentifierAttribute.#ctor(System.String,System.String)" /> constructor.</summary>
		/// <returns>The value of the constructor's <paramref name="identifier" /> parameter.</returns>
		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06004060 RID: 16480 RVA: 0x000E0F37 File Offset: 0x000DF137
		public string Identifier
		{
			get
			{
				return this.Identifier_;
			}
		}

		// Token: 0x04002A2B RID: 10795
		internal string Scope_;

		// Token: 0x04002A2C RID: 10796
		internal string Identifier_;
	}
}
