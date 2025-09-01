using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a primitive data type value.</summary>
	// Token: 0x02000323 RID: 803
	[Serializable]
	public class CodePrimitiveExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodePrimitiveExpression" /> class.</summary>
		// Token: 0x060019A3 RID: 6563 RVA: 0x0005EE88 File Offset: 0x0005D088
		public CodePrimitiveExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodePrimitiveExpression" /> class using the specified object.</summary>
		/// <param name="value">The object to represent.</param>
		// Token: 0x060019A4 RID: 6564 RVA: 0x00060B98 File Offset: 0x0005ED98
		public CodePrimitiveExpression(object value)
		{
			this.Value = value;
		}

		/// <summary>Gets or sets the primitive data type to represent.</summary>
		/// <returns>The primitive data type instance to represent the value of.</returns>
		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060019A5 RID: 6565 RVA: 0x00060BA7 File Offset: 0x0005EDA7
		// (set) Token: 0x060019A6 RID: 6566 RVA: 0x00060BAF File Offset: 0x0005EDAF
		public object Value
		{
			[CompilerGenerated]
			get
			{
				return this.<Value>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Value>k__BackingField = value;
			}
		}

		// Token: 0x04000DCB RID: 3531
		[CompilerGenerated]
		private object <Value>k__BackingField;
	}
}
