using System;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x02000215 RID: 533
	public class InterpolatedStringInsert : CompositeExpression
	{
		// Token: 0x06001B2E RID: 6958 RVA: 0x00084369 File Offset: 0x00082569
		public InterpolatedStringInsert(Expression expr) : base(expr)
		{
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001B2F RID: 6959 RVA: 0x00084372 File Offset: 0x00082572
		// (set) Token: 0x06001B30 RID: 6960 RVA: 0x0008437A File Offset: 0x0008257A
		public Expression Alignment
		{
			[CompilerGenerated]
			get
			{
				return this.<Alignment>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Alignment>k__BackingField = value;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x00084383 File Offset: 0x00082583
		// (set) Token: 0x06001B32 RID: 6962 RVA: 0x0008438B File Offset: 0x0008258B
		public string Format
		{
			[CompilerGenerated]
			get
			{
				return this.<Format>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Format>k__BackingField = value;
			}
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x00084394 File Offset: 0x00082594
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			InterpolatedStringInsert interpolatedStringInsert = (InterpolatedStringInsert)t;
			if (this.Alignment != null)
			{
				interpolatedStringInsert.Alignment = this.Alignment.Clone(clonectx);
			}
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x000843C4 File Offset: 0x000825C4
		protected override Expression DoResolve(ResolveContext rc)
		{
			Expression expression = base.DoResolve(rc);
			if (expression == null)
			{
				return null;
			}
			return Convert.ImplicitConversionRequired(rc, expression, rc.BuiltinTypes.Object, expression.Location);
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x000843F8 File Offset: 0x000825F8
		public int? ResolveAligment(ResolveContext rc)
		{
			Constant constant = this.Alignment.ResolveLabelConstant(rc);
			if (constant == null)
			{
				return null;
			}
			constant = constant.ImplicitConversionRequired(rc, rc.BuiltinTypes.Int);
			if (constant == null)
			{
				return null;
			}
			int num = (int)constant.GetValueAsLong();
			if (num > 32767 || num < -32767)
			{
				rc.Report.Warning(8094, 1, this.Alignment.Location, "Alignment value has a magnitude greater than 32767 and may result in a large formatted string");
			}
			return new int?(num);
		}

		// Token: 0x04000A1D RID: 2589
		[CompilerGenerated]
		private Expression <Alignment>k__BackingField;

		// Token: 0x04000A1E RID: 2590
		[CompilerGenerated]
		private string <Format>k__BackingField;
	}
}
