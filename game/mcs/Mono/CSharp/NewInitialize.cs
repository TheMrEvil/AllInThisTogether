using System;

namespace Mono.CSharp
{
	// Token: 0x02000210 RID: 528
	public class NewInitialize : New
	{
		// Token: 0x06001B0C RID: 6924 RVA: 0x00083644 File Offset: 0x00081844
		public NewInitialize(FullNamedExpression requested_type, Arguments arguments, CollectionOrObjectInitializers initializers, Location l) : base(requested_type, arguments, l)
		{
			this.initializers = initializers;
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x00083657 File Offset: 0x00081857
		public CollectionOrObjectInitializers Initializers
		{
			get
			{
				return this.initializers;
			}
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x0008365F File Offset: 0x0008185F
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			base.CloneTo(clonectx, t);
			((NewInitialize)t).initializers = (CollectionOrObjectInitializers)this.initializers.Clone(clonectx);
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x00083685 File Offset: 0x00081885
		public override bool ContainsEmitWithAwait()
		{
			return base.ContainsEmitWithAwait() || this.initializers.ContainsEmitWithAwait();
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x0008369C File Offset: 0x0008189C
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			Arguments arguments = new Arguments(2);
			arguments.Add(new Argument(base.CreateExpressionTree(ec)));
			if (!this.initializers.IsEmpty)
			{
				arguments.Add(new Argument(this.initializers.CreateExpressionTree(ec, this.initializers.IsCollectionInitializer)));
			}
			return base.CreateExpressionFactoryCall(ec, this.initializers.IsCollectionInitializer ? "ListInit" : "MemberInit", arguments);
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x00083714 File Offset: 0x00081914
		protected override Expression DoResolve(ResolveContext ec)
		{
			Expression expression = base.DoResolve(ec);
			if (this.type == null)
			{
				return null;
			}
			if (this.type.IsDelegate)
			{
				ec.Report.Error(1958, this.Initializers.Location, "Object and collection initializers cannot be used to instantiate a delegate");
			}
			Expression currentInitializerVariable = ec.CurrentInitializerVariable;
			ec.CurrentInitializerVariable = new NewInitialize.InitializerTargetExpression(this);
			this.initializers.Resolve(ec);
			ec.CurrentInitializerVariable = currentInitializerVariable;
			this.dynamic = (expression as DynamicExpressionStatement);
			if (this.dynamic != null)
			{
				return this;
			}
			return expression;
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x000837A0 File Offset: 0x000819A0
		public override void Emit(EmitContext ec)
		{
			if (this.method == null && TypeSpec.IsValueType(this.type) && this.initializers.Initializers.Count > 1 && ec.HasSet(BuilderContext.Options.AsyncBody) && this.initializers.ContainsEmitWithAwait())
			{
				StackFieldExpr temporaryField = ec.GetTemporaryField(this.type, false);
				if (!this.Emit(ec, temporaryField))
				{
					temporaryField.Emit(ec);
				}
				return;
			}
			base.Emit(ec);
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x00083814 File Offset: 0x00081A14
		public override bool Emit(EmitContext ec, IMemoryLocation target)
		{
			IMemoryLocation memoryLocation = target;
			LocalTemporary localTemporary = null;
			bool flag = false;
			if (!this.initializers.IsEmpty)
			{
				memoryLocation = (target as LocalTemporary);
				if (memoryLocation == null)
				{
					memoryLocation = (target as StackFieldExpr);
				}
				if (memoryLocation == null)
				{
					VariableReference variableReference = target as VariableReference;
					if (variableReference != null && variableReference.IsRef)
					{
						variableReference.EmitLoad(ec);
						flag = true;
					}
				}
				if (memoryLocation == null)
				{
					localTemporary = (memoryLocation = new LocalTemporary(this.type));
				}
			}
			bool flag2;
			if (this.dynamic != null)
			{
				this.dynamic.Emit(ec);
				flag2 = true;
			}
			else
			{
				flag2 = base.Emit(ec, memoryLocation);
			}
			if (this.initializers.IsEmpty)
			{
				return flag2;
			}
			StackFieldExpr stackFieldExpr = null;
			if (flag2)
			{
				if (flag)
				{
					localTemporary = (memoryLocation = new LocalTemporary(this.type));
				}
				if (localTemporary != null)
				{
					localTemporary.Store(ec);
				}
				if (ec.HasSet(BuilderContext.Options.AsyncBody) && this.initializers.ContainsEmitWithAwait())
				{
					if (localTemporary == null)
					{
						throw new NotImplementedException();
					}
					stackFieldExpr = ec.GetTemporaryField(this.type, false);
					stackFieldExpr.EmitAssign(ec, localTemporary, false, false);
					memoryLocation = stackFieldExpr;
					localTemporary.Release(ec);
				}
			}
			this.instance = memoryLocation;
			this.initializers.Emit(ec);
			((Expression)memoryLocation).Emit(ec);
			if (localTemporary != null)
			{
				localTemporary.Release(ec);
			}
			if (stackFieldExpr != null)
			{
				stackFieldExpr.IsAvailableForReuse = true;
			}
			return true;
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x00083945 File Offset: 0x00081B45
		protected override IMemoryLocation EmitAddressOf(EmitContext ec, AddressOp Mode)
		{
			this.instance = base.EmitAddressOf(ec, Mode);
			if (!this.initializers.IsEmpty)
			{
				this.initializers.Emit(ec);
			}
			return this.instance;
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x00083974 File Offset: 0x00081B74
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			base.FlowAnalysis(fc);
			this.initializers.FlowAnalysis(fc);
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x00083989 File Offset: 0x00081B89
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000A11 RID: 2577
		private CollectionOrObjectInitializers initializers;

		// Token: 0x04000A12 RID: 2578
		private IMemoryLocation instance;

		// Token: 0x04000A13 RID: 2579
		private DynamicExpressionStatement dynamic;

		// Token: 0x020003BE RID: 958
		private sealed class InitializerTargetExpression : Expression, IMemoryLocation
		{
			// Token: 0x06002735 RID: 10037 RVA: 0x000BBC3F File Offset: 0x000B9E3F
			public InitializerTargetExpression(NewInitialize newInstance)
			{
				this.type = newInstance.type;
				this.loc = newInstance.loc;
				this.eclass = newInstance.eclass;
				this.new_instance = newInstance;
			}

			// Token: 0x06002736 RID: 10038 RVA: 0x000022F4 File Offset: 0x000004F4
			public override bool ContainsEmitWithAwait()
			{
				return false;
			}

			// Token: 0x06002737 RID: 10039 RVA: 0x0006D4AD File Offset: 0x0006B6AD
			public override Expression CreateExpressionTree(ResolveContext ec)
			{
				throw new NotSupportedException("ET");
			}

			// Token: 0x06002738 RID: 10040 RVA: 0x00005936 File Offset: 0x00003B36
			protected override Expression DoResolve(ResolveContext ec)
			{
				return this;
			}

			// Token: 0x06002739 RID: 10041 RVA: 0x00005936 File Offset: 0x00003B36
			public override Expression DoResolveLValue(ResolveContext ec, Expression right_side)
			{
				return this;
			}

			// Token: 0x0600273A RID: 10042 RVA: 0x000BBC72 File Offset: 0x000B9E72
			public override void Emit(EmitContext ec)
			{
				((Expression)this.new_instance.instance).Emit(ec);
			}

			// Token: 0x0600273B RID: 10043 RVA: 0x000BBC8A File Offset: 0x000B9E8A
			public override Expression EmitToField(EmitContext ec)
			{
				return (Expression)this.new_instance.instance;
			}

			// Token: 0x0600273C RID: 10044 RVA: 0x000BBC9C File Offset: 0x000B9E9C
			public void AddressOf(EmitContext ec, AddressOp mode)
			{
				this.new_instance.instance.AddressOf(ec, mode);
			}

			// Token: 0x040010A3 RID: 4259
			private NewInitialize new_instance;
		}
	}
}
