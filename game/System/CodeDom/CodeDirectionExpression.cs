using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents an expression used as a method invoke parameter along with a reference direction indicator.</summary>
	// Token: 0x02000308 RID: 776
	[Serializable]
	public class CodeDirectionExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDirectionExpression" /> class.</summary>
		// Token: 0x060018C3 RID: 6339 RVA: 0x0005EE88 File Offset: 0x0005D088
		public CodeDirectionExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDirectionExpression" /> class using the specified field direction and expression.</summary>
		/// <param name="direction">A <see cref="T:System.CodeDom.FieldDirection" /> that indicates the field direction of the expression.</param>
		/// <param name="expression">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the code expression to represent.</param>
		// Token: 0x060018C4 RID: 6340 RVA: 0x0005FA92 File Offset: 0x0005DC92
		public CodeDirectionExpression(FieldDirection direction, CodeExpression expression)
		{
			this.Expression = expression;
			this.Direction = direction;
		}

		/// <summary>Gets or sets the code expression to represent.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression to represent.</returns>
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060018C5 RID: 6341 RVA: 0x0005FAA8 File Offset: 0x0005DCA8
		// (set) Token: 0x060018C6 RID: 6342 RVA: 0x0005FAB0 File Offset: 0x0005DCB0
		public CodeExpression Expression
		{
			[CompilerGenerated]
			get
			{
				return this.<Expression>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Expression>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the field direction for this direction expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.FieldDirection" /> that indicates the field direction for this direction expression.</returns>
		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x0005FAB9 File Offset: 0x0005DCB9
		// (set) Token: 0x060018C8 RID: 6344 RVA: 0x0005FAC1 File Offset: 0x0005DCC1
		public FieldDirection Direction
		{
			[CompilerGenerated]
			get
			{
				return this.<Direction>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Direction>k__BackingField = value;
			}
		}

		// Token: 0x04000D86 RID: 3462
		[CompilerGenerated]
		private CodeExpression <Expression>k__BackingField;

		// Token: 0x04000D87 RID: 3463
		[CompilerGenerated]
		private FieldDirection <Direction>k__BackingField;
	}
}
