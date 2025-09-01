using System;

namespace Mono.CSharp
{
	// Token: 0x020001E6 RID: 486
	public class ParameterReference : VariableReference
	{
		// Token: 0x0600195A RID: 6490 RVA: 0x0007D016 File Offset: 0x0007B216
		public ParameterReference(ParametersBlock.ParameterInfo pi, Location loc)
		{
			this.pi = pi;
			this.loc = loc;
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x0600195B RID: 6491 RVA: 0x0007D02C File Offset: 0x0007B22C
		// (set) Token: 0x0600195C RID: 6492 RVA: 0x0007D039 File Offset: 0x0007B239
		public override bool IsLockedByStatement
		{
			get
			{
				return this.pi.IsLocked;
			}
			set
			{
				this.pi.IsLocked = value;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x0007D047 File Offset: 0x0007B247
		public override bool IsRef
		{
			get
			{
				return (this.pi.Parameter.ModFlags & Parameter.Modifier.RefOutMask) > Parameter.Modifier.NONE;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x0600195E RID: 6494 RVA: 0x0007D05E File Offset: 0x0007B25E
		private bool HasOutModifier
		{
			get
			{
				return (this.pi.Parameter.ModFlags & Parameter.Modifier.OUT) > Parameter.Modifier.NONE;
			}
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x0007D075 File Offset: 0x0007B275
		public override HoistedVariable GetHoistedVariable(AnonymousExpression ae)
		{
			return this.pi.Parameter.HoistedVariant;
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001960 RID: 6496 RVA: 0x0007D087 File Offset: 0x0007B287
		public override bool IsFixed
		{
			get
			{
				return !this.IsRef;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001961 RID: 6497 RVA: 0x0007D092 File Offset: 0x0007B292
		public override string Name
		{
			get
			{
				return this.Parameter.Name;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001962 RID: 6498 RVA: 0x0007D09F File Offset: 0x0007B29F
		public Parameter Parameter
		{
			get
			{
				return this.pi.Parameter;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001963 RID: 6499 RVA: 0x0007D0AC File Offset: 0x0007B2AC
		public override VariableInfo VariableInfo
		{
			get
			{
				return this.pi.VariableInfo;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001964 RID: 6500 RVA: 0x0007D0B9 File Offset: 0x0007B2B9
		protected override ILocalVariable Variable
		{
			get
			{
				return this.Parameter;
			}
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0007D0C1 File Offset: 0x0007B2C1
		public override void AddressOf(EmitContext ec, AddressOp mode)
		{
			if (this.IsRef)
			{
				base.EmitLoad(ec);
				return;
			}
			base.AddressOf(ec, mode);
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x0007D0DB File Offset: 0x0007B2DB
		public override void SetHasAddressTaken()
		{
			this.Parameter.HasAddressTaken = true;
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x0007D0EC File Offset: 0x0007B2EC
		private bool DoResolveBase(ResolveContext ec)
		{
			if (this.eclass != ExprClass.Unresolved)
			{
				return true;
			}
			this.type = this.pi.ParameterType;
			this.eclass = ExprClass.Variable;
			if (ec.MustCaptureVariable(this.pi))
			{
				if (this.Parameter.HasAddressTaken)
				{
					AnonymousMethodExpression.Error_AddressOfCapturedVar(ec, this, this.loc);
				}
				if (this.IsRef)
				{
					ec.Report.Error(1628, this.loc, "Parameter `{0}' cannot be used inside `{1}' when using `ref' or `out' modifier", this.Name, ec.CurrentAnonymousMethod.ContainerType);
				}
				if (ec.IsVariableCapturingRequired && !this.pi.Block.ParametersBlock.IsExpressionTree)
				{
					this.pi.Block.Explicit.CreateAnonymousMethodStorey(ec).CaptureParameter(ec, this.pi, this);
				}
			}
			return true;
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x0007D1BD File Offset: 0x0007B3BD
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x0007D1CC File Offset: 0x0007B3CC
		public override bool Equals(object obj)
		{
			ParameterReference parameterReference = obj as ParameterReference;
			return parameterReference != null && this.Name == parameterReference.Name;
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x0000AF70 File Offset: 0x00009170
		protected override void CloneTo(CloneContext clonectx, Expression target)
		{
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x0007D1F8 File Offset: 0x0007B3F8
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			HoistedVariable hoistedVariable = base.GetHoistedVariable(ec);
			if (hoistedVariable != null)
			{
				return hoistedVariable.CreateExpressionTree();
			}
			return this.Parameter.ExpressionTreeVariableReference();
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x0007D222 File Offset: 0x0007B422
		protected override Expression DoResolve(ResolveContext ec)
		{
			if (!this.DoResolveBase(ec))
			{
				return null;
			}
			return this;
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x0007D230 File Offset: 0x0007B430
		public override Expression DoResolveLValue(ResolveContext ec, Expression right_side)
		{
			if (!this.DoResolveBase(ec))
			{
				return null;
			}
			if (this.Parameter.HoistedVariant != null)
			{
				this.Parameter.HoistedVariant.IsAssigned = true;
			}
			return base.DoResolveLValue(ec, right_side);
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x0007D264 File Offset: 0x0007B464
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			VariableInfo variableInfo = this.VariableInfo;
			if (variableInfo == null)
			{
				return;
			}
			if (fc.IsDefinitelyAssigned(variableInfo))
			{
				return;
			}
			fc.Report.Error(269, this.loc, "Use of unassigned out parameter `{0}'", this.Name);
			fc.SetVariableAssigned(variableInfo, false);
		}

		// Token: 0x040009C8 RID: 2504
		protected ParametersBlock.ParameterInfo pi;
	}
}
