using System;

namespace Mono.CSharp
{
	// Token: 0x020001ED RID: 493
	public class This : VariableReference
	{
		// Token: 0x060019CB RID: 6603 RVA: 0x0007F6AC File Offset: 0x0007D8AC
		public This(Location loc)
		{
			this.loc = loc;
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060019CC RID: 6604 RVA: 0x0007F6BB File Offset: 0x0007D8BB
		public override string Name
		{
			get
			{
				return "this";
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x060019CD RID: 6605 RVA: 0x000022F4 File Offset: 0x000004F4
		// (set) Token: 0x060019CE RID: 6606 RVA: 0x0000AF70 File Offset: 0x00009170
		public override bool IsLockedByStatement
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x060019CF RID: 6607 RVA: 0x0007F6C2 File Offset: 0x0007D8C2
		public override bool IsRef
		{
			get
			{
				return this.type.IsStruct;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x060019D0 RID: 6608 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsSideEffectFree
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x060019D1 RID: 6609 RVA: 0x0007F6CF File Offset: 0x0007D8CF
		protected override ILocalVariable Variable
		{
			get
			{
				return This.ThisVariable.Instance;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x060019D2 RID: 6610 RVA: 0x0007F6D6 File Offset: 0x0007D8D6
		public override VariableInfo VariableInfo
		{
			get
			{
				return this.variable_info;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x060019D3 RID: 6611 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool IsFixed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x0007F6DE File Offset: 0x0007D8DE
		private void CheckStructThisDefiniteAssignment(FlowAnalysisContext fc)
		{
			if (this.variable_info == null)
			{
				return;
			}
			if (fc.IsDefinitelyAssigned(this.variable_info))
			{
				return;
			}
			fc.Report.Error(188, this.loc, "The `this' object cannot be used before all of its fields are assigned to");
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x0007F714 File Offset: 0x0007D914
		protected virtual void Error_ThisNotAvailable(ResolveContext ec)
		{
			if (ec.IsStatic && !ec.HasSet(ResolveContext.Options.ConstantScope))
			{
				ec.Report.Error(26, this.loc, "Keyword `this' is not valid in a static property, static method, or static field initializer");
				return;
			}
			if (ec.CurrentAnonymousMethod != null)
			{
				ec.Report.Error(1673, this.loc, "Anonymous methods inside structs cannot access instance members of `this'. Consider copying `this' to a local variable outside the anonymous method and using the local instead");
				return;
			}
			ec.Report.Error(27, this.loc, "Keyword `this' is not available in the current context");
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x0007F78B File Offset: 0x0007D98B
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			this.CheckStructThisDefiniteAssignment(fc);
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x0007F794 File Offset: 0x0007D994
		public override HoistedVariable GetHoistedVariable(AnonymousExpression ae)
		{
			if (ae == null)
			{
				return null;
			}
			AnonymousMethodStorey storey = ae.Storey;
			if (storey == null)
			{
				return null;
			}
			return storey.HoistedThis;
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x0007F7B8 File Offset: 0x0007D9B8
		public static bool IsThisAvailable(ResolveContext ec, bool ignoreAnonymous)
		{
			return !ec.IsStatic && !ec.HasAny(ResolveContext.Options.FieldInitializerScope | ResolveContext.Options.BaseInitializer | ResolveContext.Options.ConstantScope) && (ignoreAnonymous || ec.CurrentAnonymousMethod == null || !ec.CurrentType.IsStruct || ec.CurrentAnonymousMethod is StateMachineInitializer);
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x0007F808 File Offset: 0x0007DA08
		public virtual void ResolveBase(ResolveContext ec)
		{
			this.eclass = ExprClass.Variable;
			this.type = ec.CurrentType;
			if (!This.IsThisAvailable(ec, false))
			{
				this.Error_ThisNotAvailable(ec);
				return;
			}
			Block currentBlock = ec.CurrentBlock;
			if (currentBlock != null)
			{
				ToplevelBlock topBlock = currentBlock.ParametersBlock.TopBlock;
				if (topBlock.ThisVariable != null)
				{
					this.variable_info = topBlock.ThisVariable.VariableInfo;
				}
				AnonymousExpression currentAnonymousMethod = ec.CurrentAnonymousMethod;
				if (currentAnonymousMethod != null && ec.IsVariableCapturingRequired && !currentBlock.Explicit.HasCapturedThis)
				{
					topBlock.AddThisReferenceFromChildrenBlock(currentBlock.Explicit);
					currentAnonymousMethod.SetHasThisAccess();
				}
			}
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x0007F899 File Offset: 0x0007DA99
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.ResolveBase(ec);
			return this;
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x0007F8A4 File Offset: 0x0007DAA4
		public override Expression DoResolveLValue(ResolveContext ec, Expression right_side)
		{
			if (this.eclass == ExprClass.Unresolved)
			{
				this.ResolveBase(ec);
			}
			if (this.type.IsClass)
			{
				if (right_side == EmptyExpression.UnaryAddress)
				{
					ec.Report.Error(459, this.loc, "Cannot take the address of `this' because it is read-only");
				}
				else if (right_side == EmptyExpression.OutAccess)
				{
					ec.Report.Error(1605, this.loc, "Cannot pass `this' as a ref or out argument because it is read-only");
				}
				else
				{
					ec.Report.Error(1604, this.loc, "Cannot assign to `this' because it is read-only");
				}
			}
			return this;
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x0007F933 File Offset: 0x0007DB33
		public override bool Equals(object obj)
		{
			return obj is This;
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x0000AF70 File Offset: 0x00009170
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x0000AF70 File Offset: 0x00009170
		public override void SetHasAddressTaken()
		{
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x0007F940 File Offset: 0x0007DB40
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040009DE RID: 2526
		protected VariableInfo variable_info;

		// Token: 0x020003BA RID: 954
		private sealed class ThisVariable : ILocalVariable
		{
			// Token: 0x0600272B RID: 10027 RVA: 0x000BBBE1 File Offset: 0x000B9DE1
			public void Emit(EmitContext ec)
			{
				ec.EmitThis();
			}

			// Token: 0x0600272C RID: 10028 RVA: 0x00002CD4 File Offset: 0x00000ED4
			public void EmitAssign(EmitContext ec)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x0600272D RID: 10029 RVA: 0x000BBBE1 File Offset: 0x000B9DE1
			public void EmitAddressOf(EmitContext ec)
			{
				ec.EmitThis();
			}

			// Token: 0x0600272E RID: 10030 RVA: 0x00002CCC File Offset: 0x00000ECC
			public ThisVariable()
			{
			}

			// Token: 0x0600272F RID: 10031 RVA: 0x000BBBE9 File Offset: 0x000B9DE9
			// Note: this type is marked as 'beforefieldinit'.
			static ThisVariable()
			{
			}

			// Token: 0x040010A2 RID: 4258
			public static readonly ILocalVariable Instance = new This.ThisVariable();
		}
	}
}
