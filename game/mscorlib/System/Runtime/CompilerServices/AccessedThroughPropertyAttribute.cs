using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies the name of the property that accesses the attributed field.</summary>
	// Token: 0x020007D8 RID: 2008
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class AccessedThroughPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see langword="AccessedThroughPropertyAttribute" /> class with the name of the property used to access the attributed field.</summary>
		/// <param name="propertyName">The name of the property used to access the attributed field.</param>
		// Token: 0x060045B8 RID: 17848 RVA: 0x000E5148 File Offset: 0x000E3348
		public AccessedThroughPropertyAttribute(string propertyName)
		{
			this.PropertyName = propertyName;
		}

		/// <summary>Gets the name of the property used to access the attributed field.</summary>
		/// <returns>The name of the property used to access the attributed field.</returns>
		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x060045B9 RID: 17849 RVA: 0x000E5157 File Offset: 0x000E3357
		public string PropertyName
		{
			[CompilerGenerated]
			get
			{
				return this.<PropertyName>k__BackingField;
			}
		}

		// Token: 0x04002D21 RID: 11553
		[CompilerGenerated]
		private readonly string <PropertyName>k__BackingField;
	}
}
