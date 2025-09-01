using System;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x020001DB RID: 475
	public class DeclarationExpression : Expression, IMemoryLocation
	{
		// Token: 0x060018B2 RID: 6322 RVA: 0x0007765F File Offset: 0x0007585F
		public DeclarationExpression(FullNamedExpression variableType, LocalVariable variable)
		{
			this.VariableType = variableType;
			this.Variable = variable;
			this.loc = variable.Location;
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060018B3 RID: 6323 RVA: 0x00077681 File Offset: 0x00075881
		// (set) Token: 0x060018B4 RID: 6324 RVA: 0x00077689 File Offset: 0x00075889
		public LocalVariable Variable
		{
			[CompilerGenerated]
			get
			{
				return this.<Variable>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Variable>k__BackingField = value;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060018B5 RID: 6325 RVA: 0x00077692 File Offset: 0x00075892
		// (set) Token: 0x060018B6 RID: 6326 RVA: 0x0007769A File Offset: 0x0007589A
		public Expression Initializer
		{
			[CompilerGenerated]
			get
			{
				return this.<Initializer>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Initializer>k__BackingField = value;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060018B7 RID: 6327 RVA: 0x000776A3 File Offset: 0x000758A3
		// (set) Token: 0x060018B8 RID: 6328 RVA: 0x000776AB File Offset: 0x000758AB
		public FullNamedExpression VariableType
		{
			[CompilerGenerated]
			get
			{
				return this.<VariableType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<VariableType>k__BackingField = value;
			}
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x000776B4 File Offset: 0x000758B4
		public void AddressOf(EmitContext ec, AddressOp mode)
		{
			this.Variable.CreateBuilder(ec);
			if (this.Initializer != null)
			{
				this.lvr.EmitAssign(ec, this.Initializer, false, false);
			}
			this.lvr.AddressOf(ec, mode);
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x000776EC File Offset: 0x000758EC
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			DeclarationExpression declarationExpression = (DeclarationExpression)t;
			declarationExpression.VariableType = (FullNamedExpression)this.VariableType.Clone(clonectx);
			if (this.Initializer != null)
			{
				declarationExpression.Initializer = this.Initializer.Clone(clonectx);
			}
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x00077731 File Offset: 0x00075931
		public override Expression CreateExpressionTree(ResolveContext rc)
		{
			rc.Report.Error(8046, this.loc, "An expression tree cannot contain a declaration expression");
			return null;
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x00077750 File Offset: 0x00075950
		private bool DoResolveCommon(ResolveContext rc)
		{
			VarExpr varExpr = this.VariableType as VarExpr;
			if (varExpr != null)
			{
				this.type = InternalType.VarOutType;
			}
			else
			{
				this.type = this.VariableType.ResolveAsType(rc, false);
				if (this.type == null)
				{
					return false;
				}
			}
			if (this.Initializer != null)
			{
				this.Initializer = this.Initializer.Resolve(rc);
				if (varExpr != null && this.Initializer != null && varExpr.InferType(rc, this.Initializer))
				{
					this.type = varExpr.Type;
				}
			}
			this.Variable.Type = this.type;
			this.lvr = new LocalVariableReference(this.Variable, this.loc);
			this.eclass = ExprClass.Variable;
			return true;
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x00077806 File Offset: 0x00075A06
		protected override Expression DoResolve(ResolveContext rc)
		{
			if (this.DoResolveCommon(rc))
			{
				this.lvr.Resolve(rc);
			}
			return this;
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x0007781F File Offset: 0x00075A1F
		public override Expression DoResolveLValue(ResolveContext rc, Expression right_side)
		{
			if (this.lvr == null && this.DoResolveCommon(rc))
			{
				this.lvr.ResolveLValue(rc, right_side);
			}
			return this;
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override void Emit(EmitContext ec)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040009B3 RID: 2483
		private LocalVariableReference lvr;

		// Token: 0x040009B4 RID: 2484
		[CompilerGenerated]
		private LocalVariable <Variable>k__BackingField;

		// Token: 0x040009B5 RID: 2485
		[CompilerGenerated]
		private Expression <Initializer>k__BackingField;

		// Token: 0x040009B6 RID: 2486
		[CompilerGenerated]
		private FullNamedExpression <VariableType>k__BackingField;
	}
}
