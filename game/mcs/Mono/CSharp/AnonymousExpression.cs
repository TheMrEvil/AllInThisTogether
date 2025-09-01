using System;
using System.Reflection.Emit;
using Mono.CompilerServices.SymbolWriter;

namespace Mono.CSharp
{
	// Token: 0x0200010D RID: 269
	public abstract class AnonymousExpression : ExpressionStatement
	{
		// Token: 0x06000D67 RID: 3431 RVA: 0x0003100D File Offset: 0x0002F20D
		protected AnonymousExpression(ParametersBlock block, TypeSpec return_type, Location loc)
		{
			this.ReturnType = return_type;
			this.block = block;
			this.loc = loc;
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000D68 RID: 3432
		public abstract string ContainerType { get; }

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000D69 RID: 3433
		public abstract bool IsIterator { get; }

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000D6A RID: 3434
		public abstract AnonymousMethodStorey Storey { get; }

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000D6B RID: 3435 RVA: 0x0003102A File Offset: 0x0002F22A
		public ParametersBlock Block
		{
			get
			{
				return this.block;
			}
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00031032 File Offset: 0x0002F232
		public AnonymousExpression Compatible(ResolveContext ec)
		{
			return this.Compatible(ec, this);
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0003103C File Offset: 0x0002F23C
		public AnonymousExpression Compatible(ResolveContext ec, AnonymousExpression ae)
		{
			if (this.block.Resolved)
			{
				return this;
			}
			BlockContext blockContext = new BlockContext(ec, this.block, this.ReturnType);
			blockContext.CurrentAnonymousMethod = ae;
			AnonymousMethodBody anonymousMethodBody = this as AnonymousMethodBody;
			if (ec.HasSet(ResolveContext.Options.InferReturnType) && anonymousMethodBody != null)
			{
				anonymousMethodBody.ReturnTypeInference = new TypeInferenceContext();
			}
			BlockContext blockContext2 = ec as BlockContext;
			if (blockContext2 != null)
			{
				blockContext.AssignmentInfoOffset = blockContext2.AssignmentInfoOffset;
				blockContext.EnclosingLoop = blockContext2.EnclosingLoop;
				blockContext.EnclosingLoopOrSwitch = blockContext2.EnclosingLoopOrSwitch;
				blockContext.Switch = blockContext2.Switch;
			}
			int errors = ec.Report.Errors;
			bool flag = this.Block.Resolve(blockContext);
			if (flag && errors == ec.Report.Errors)
			{
				this.MarkReachable(default(Reachability));
				if (!this.CheckReachableExit(ec.Report))
				{
					return null;
				}
				if (blockContext2 != null)
				{
					blockContext2.AssignmentInfoOffset = blockContext.AssignmentInfoOffset;
				}
			}
			if (anonymousMethodBody != null && anonymousMethodBody.ReturnTypeInference != null)
			{
				anonymousMethodBody.ReturnTypeInference.FixAllTypes(ec);
				this.ReturnType = anonymousMethodBody.ReturnTypeInference.InferredTypeArguments[0];
				anonymousMethodBody.ReturnTypeInference = null;
				if (this.block.IsAsync && this.ReturnType != null)
				{
					this.ReturnType = ((this.ReturnType.Kind == MemberKind.Void) ? ec.Module.PredefinedTypes.Task.TypeSpec : ec.Module.PredefinedTypes.TaskGeneric.TypeSpec.MakeGenericType(ec, new TypeSpec[]
					{
						this.ReturnType
					}));
				}
			}
			if (flag && errors != ec.Report.Errors)
			{
				return null;
			}
			if (!flag)
			{
				return null;
			}
			return this;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool ContainsEmitWithAwait()
		{
			return false;
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x000311EC File Offset: 0x0002F3EC
		private bool CheckReachableExit(Report report)
		{
			if (this.block.HasReachableClosingBrace && this.ReturnType.Kind != MemberKind.Void && !this.IsIterator)
			{
				report.Error(1643, this.StartLocation, "Not all code paths return a value in anonymous method of type `{0}'", this.GetSignatureForError());
				return false;
			}
			return true;
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00031240 File Offset: 0x0002F440
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			this.MarkReachable(default(Reachability));
			this.CheckReachableExit(fc.Report);
			DefiniteAssignmentBitSet definiteAssignment = fc.BranchDefiniteAssignment();
			ParametersBlock parametersBlock = fc.ParametersBlock;
			fc.ParametersBlock = this.Block;
			DefiniteAssignmentBitSet definiteAssignmentOnTrue = fc.DefiniteAssignmentOnTrue;
			DefiniteAssignmentBitSet definiteAssignmentOnFalse = fc.DefiniteAssignmentOnFalse;
			TryFinally tryFinally = fc.TryFinally;
			fc.DefiniteAssignmentOnTrue = (fc.DefiniteAssignmentOnFalse = null);
			fc.TryFinally = null;
			this.block.FlowAnalysis(fc);
			fc.ParametersBlock = parametersBlock;
			fc.DefiniteAssignment = definiteAssignment;
			fc.DefiniteAssignmentOnTrue = definiteAssignmentOnTrue;
			fc.DefiniteAssignmentOnFalse = definiteAssignmentOnFalse;
			fc.TryFinally = tryFinally;
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x000312E4 File Offset: 0x0002F4E4
		public override void MarkReachable(Reachability rc)
		{
			this.block.MarkReachable(rc);
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x000312F4 File Offset: 0x0002F4F4
		public void SetHasThisAccess()
		{
			ExplicitBlock explicitBlock = this.block;
			while (!explicitBlock.HasCapturedThis)
			{
				explicitBlock.HasCapturedThis = true;
				explicitBlock = ((explicitBlock.Parent == null) ? null : explicitBlock.Parent.Explicit);
				if (explicitBlock == null)
				{
					return;
				}
			}
		}

		// Token: 0x04000655 RID: 1621
		protected readonly ParametersBlock block;

		// Token: 0x04000656 RID: 1622
		public TypeSpec ReturnType;

		// Token: 0x0200037F RID: 895
		protected class AnonymousMethodMethod : Method
		{
			// Token: 0x06002693 RID: 9875 RVA: 0x000B6AC0 File Offset: 0x000B4CC0
			public AnonymousMethodMethod(TypeDefinition parent, AnonymousExpression am, AnonymousMethodStorey storey, TypeExpr return_type, Modifiers mod, MemberName name, ParametersCompiled parameters) : base(parent, return_type, mod | Modifiers.COMPILER_GENERATED, name, parameters, null)
			{
				this.AnonymousMethod = am;
				this.Storey = storey;
				this.Parent.PartialContainer.Members.Add(this);
				base.Block = new ToplevelBlock(am.block, parameters);
			}

			// Token: 0x06002694 RID: 9876 RVA: 0x000B6B1A File Offset: 0x000B4D1A
			public override EmitContext CreateEmitContext(ILGenerator ig, SourceMethodBuilder sourceMethod)
			{
				return new EmitContext(this, ig, base.ReturnType, sourceMethod)
				{
					CurrentAnonymousMethod = this.AnonymousMethod
				};
			}

			// Token: 0x06002695 RID: 9877 RVA: 0x0000AF70 File Offset: 0x00009170
			protected override void DefineTypeParameters()
			{
			}

			// Token: 0x06002696 RID: 9878 RVA: 0x000B6B38 File Offset: 0x000B4D38
			protected override bool ResolveMemberType()
			{
				if (!base.ResolveMemberType())
				{
					return false;
				}
				if (this.Storey != null && this.Storey.Mutator != null)
				{
					if (!this.parameters.IsEmpty)
					{
						TypeSpec[] array = this.Storey.Mutator.Mutate(this.parameters.Types);
						if (array != this.parameters.Types)
						{
							this.parameters = ParametersCompiled.CreateFullyResolved((Parameter[])this.parameters.FixedParameters, array);
						}
					}
					this.member_type = this.Storey.Mutator.Mutate(this.member_type);
				}
				return true;
			}

			// Token: 0x06002697 RID: 9879 RVA: 0x000B6BD4 File Offset: 0x000B4DD4
			public override void Emit()
			{
				if (base.MethodBuilder == null)
				{
					this.Define();
				}
				base.Emit();
			}

			// Token: 0x04000F49 RID: 3913
			public readonly AnonymousExpression AnonymousMethod;

			// Token: 0x04000F4A RID: 3914
			public readonly AnonymousMethodStorey Storey;
		}
	}
}
