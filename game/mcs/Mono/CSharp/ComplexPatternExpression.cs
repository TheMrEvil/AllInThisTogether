using System;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x020001D7 RID: 471
	internal abstract class ComplexPatternExpression : PatternExpression
	{
		// Token: 0x060018A0 RID: 6304 RVA: 0x000771D4 File Offset: 0x000753D4
		protected ComplexPatternExpression(ATypeNameExpression typeExpresion, Location loc) : base(loc)
		{
			this.TypeExpression = typeExpresion;
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060018A1 RID: 6305 RVA: 0x000771E4 File Offset: 0x000753E4
		// (set) Token: 0x060018A2 RID: 6306 RVA: 0x000771EC File Offset: 0x000753EC
		public ATypeNameExpression TypeExpression
		{
			[CompilerGenerated]
			get
			{
				return this.<TypeExpression>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<TypeExpression>k__BackingField = value;
			}
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x000771F5 File Offset: 0x000753F5
		public override void Emit(EmitContext ec)
		{
			this.EmitBranchable(ec, ec.RecursivePatternLabel, false);
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00077208 File Offset: 0x00075408
		public override void EmitBranchable(EmitContext ec, Label target, bool on_true)
		{
			if (this.comparisons != null)
			{
				Expression[] array = this.comparisons;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].EmitBranchable(ec, target, false);
				}
			}
		}

		// Token: 0x040009AF RID: 2479
		protected Expression[] comparisons;

		// Token: 0x040009B0 RID: 2480
		[CompilerGenerated]
		private ATypeNameExpression <TypeExpression>k__BackingField;
	}
}
