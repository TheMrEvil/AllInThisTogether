using System;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x0200021D RID: 541
	public class SpecialContraintExpr : FullNamedExpression
	{
		// Token: 0x06001B68 RID: 7016 RVA: 0x00084EAB File Offset: 0x000830AB
		public SpecialContraintExpr(SpecialConstraint constraint, Location loc)
		{
			this.loc = loc;
			this.Constraint = constraint;
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001B69 RID: 7017 RVA: 0x00084EC1 File Offset: 0x000830C1
		// (set) Token: 0x06001B6A RID: 7018 RVA: 0x00084EC9 File Offset: 0x000830C9
		public SpecialConstraint Constraint
		{
			[CompilerGenerated]
			get
			{
				return this.<Constraint>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Constraint>k__BackingField = value;
			}
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x00023DF4 File Offset: 0x00021FF4
		protected override Expression DoResolve(ResolveContext rc)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override FullNamedExpression ResolveAsTypeOrNamespace(IMemberContext mc, bool allowUnboundTypeArguments)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000A3D RID: 2621
		[CompilerGenerated]
		private SpecialConstraint <Constraint>k__BackingField;
	}
}
