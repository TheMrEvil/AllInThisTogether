using System;

namespace Mono.CSharp
{
	// Token: 0x020001E5 RID: 485
	public class LocalVariableReference : VariableReference
	{
		// Token: 0x06001948 RID: 6472 RVA: 0x0007CD13 File Offset: 0x0007AF13
		public LocalVariableReference(LocalVariable li, Location l)
		{
			this.local_info = li;
			this.loc = l;
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001949 RID: 6473 RVA: 0x0007CD29 File Offset: 0x0007AF29
		public override VariableInfo VariableInfo
		{
			get
			{
				return this.local_info.VariableInfo;
			}
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x0007CD36 File Offset: 0x0007AF36
		public override HoistedVariable GetHoistedVariable(AnonymousExpression ae)
		{
			return this.local_info.HoistedVariant;
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x0600194B RID: 6475 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x0600194C RID: 6476 RVA: 0x0007CD43 File Offset: 0x0007AF43
		// (set) Token: 0x0600194D RID: 6477 RVA: 0x0007CD50 File Offset: 0x0007AF50
		public override bool IsLockedByStatement
		{
			get
			{
				return this.local_info.IsLocked;
			}
			set
			{
				this.local_info.IsLocked = value;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x0600194E RID: 6478 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool IsRef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x0600194F RID: 6479 RVA: 0x0007CD5E File Offset: 0x0007AF5E
		public override string Name
		{
			get
			{
				return this.local_info.Name;
			}
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x0007CD6C File Offset: 0x0007AF6C
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
			fc.Report.Error(165, this.loc, "Use of unassigned local variable `{0}'", this.Name);
			variableInfo.SetAssigned(fc.DefiniteAssignment, true);
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x0007CDBC File Offset: 0x0007AFBC
		public override void SetHasAddressTaken()
		{
			this.local_info.SetHasAddressTaken();
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x0007CDCC File Offset: 0x0007AFCC
		private void DoResolveBase(ResolveContext ec)
		{
			this.eclass = ExprClass.Variable;
			this.type = this.local_info.Type;
			if (ec.MustCaptureVariable(this.local_info))
			{
				if (this.local_info.AddressTaken)
				{
					AnonymousMethodExpression.Error_AddressOfCapturedVar(ec, this, this.loc);
				}
				else if (this.local_info.IsFixed)
				{
					ec.Report.Error(1764, this.loc, "Cannot use fixed local `{0}' inside an anonymous method, lambda expression or query expression", this.GetSignatureForError());
				}
				if (ec.IsVariableCapturingRequired)
				{
					this.local_info.Block.Explicit.CreateAnonymousMethodStorey(ec).CaptureLocalVariable(ec, this.local_info);
				}
			}
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x0007CE74 File Offset: 0x0007B074
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.local_info.SetIsUsed();
			this.DoResolveBase(ec);
			if (this.local_info.Type == InternalType.VarOutType)
			{
				ec.Report.Error(8048, this.loc, "Cannot use uninitialized variable `{0}'", this.GetSignatureForError());
				this.type = InternalType.ErrorType;
			}
			return this;
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x0007CED4 File Offset: 0x0007B0D4
		public override Expression DoResolveLValue(ResolveContext ec, Expression rhs)
		{
			if (rhs == EmptyExpression.OutAccess || rhs.eclass == ExprClass.PropertyAccess || rhs.eclass == ExprClass.IndexerAccess)
			{
				this.local_info.SetIsUsed();
			}
			if (this.local_info.IsReadonly && !ec.HasAny(ResolveContext.Options.FieldInitializerScope | ResolveContext.Options.UsingInitializerScope) && rhs != EmptyExpression.LValueMemberAccess)
			{
				int code;
				string format;
				if (rhs == EmptyExpression.OutAccess)
				{
					code = 1657;
					format = "Cannot pass `{0}' as a ref or out argument because it is a `{1}'";
				}
				else if (rhs == EmptyExpression.LValueMemberOutAccess)
				{
					code = 1655;
					format = "Cannot pass members of `{0}' as ref or out arguments because it is a `{1}'";
				}
				else if (rhs == EmptyExpression.UnaryAddress)
				{
					code = 459;
					format = "Cannot take the address of {1} `{0}'";
				}
				else
				{
					code = 1656;
					format = "Cannot assign to `{0}' because it is a `{1}'";
				}
				ec.Report.Error(code, this.loc, format, this.Name, this.local_info.GetReadOnlyContext());
			}
			if (this.eclass == ExprClass.Unresolved)
			{
				this.DoResolveBase(ec);
			}
			return base.DoResolveLValue(ec, rhs);
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x0007CFB5 File Offset: 0x0007B1B5
		public override int GetHashCode()
		{
			return this.local_info.GetHashCode();
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x0007CFC4 File Offset: 0x0007B1C4
		public override bool Equals(object obj)
		{
			LocalVariableReference localVariableReference = obj as LocalVariableReference;
			return localVariableReference != null && this.local_info == localVariableReference.local_info;
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001957 RID: 6487 RVA: 0x0007CFEB File Offset: 0x0007B1EB
		protected override ILocalVariable Variable
		{
			get
			{
				return this.local_info;
			}
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x0007CFF3 File Offset: 0x0007B1F3
		public override string ToString()
		{
			return string.Format("{0} ({1}:{2})", base.GetType(), this.Name, this.loc);
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x0000AF70 File Offset: 0x00009170
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
		}

		// Token: 0x040009C7 RID: 2503
		public LocalVariable local_info;
	}
}
