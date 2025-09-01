using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates the COM alias for a parameter or field type.</summary>
	// Token: 0x02000709 RID: 1801
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	public sealed class ComAliasNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComAliasNameAttribute" /> class with the alias for the attributed field or parameter.</summary>
		/// <param name="alias">The alias for the field or parameter as found in the type library when it was imported.</param>
		// Token: 0x060040AD RID: 16557 RVA: 0x000E150C File Offset: 0x000DF70C
		public ComAliasNameAttribute(string alias)
		{
			this._val = alias;
		}

		/// <summary>Gets the alias for the field or parameter as found in the type library when it was imported.</summary>
		/// <returns>The alias for the field or parameter as found in the type library when it was imported.</returns>
		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x060040AE RID: 16558 RVA: 0x000E151B File Offset: 0x000DF71B
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002AE0 RID: 10976
		internal string _val;
	}
}
