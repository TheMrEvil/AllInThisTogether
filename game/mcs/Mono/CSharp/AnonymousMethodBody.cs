using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x0200010E RID: 270
	public class AnonymousMethodBody : AnonymousExpression
	{
		// Token: 0x06000D73 RID: 3443 RVA: 0x00031332 File Offset: 0x0002F532
		public AnonymousMethodBody(ParametersCompiled parameters, ParametersBlock block, TypeSpec return_type, TypeSpec delegate_type, Location loc) : base(block, return_type, loc)
		{
			this.type = delegate_type;
			this.parameters = parameters;
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x000305E0 File Offset: 0x0002E7E0
		public override string ContainerType
		{
			get
			{
				return "anonymous method";
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x0003134D File Offset: 0x0002F54D
		// (set) Token: 0x06000D76 RID: 3446 RVA: 0x00031355 File Offset: 0x0002F555
		public MethodGroupExpr DirectMethodGroupConversion
		{
			[CompilerGenerated]
			get
			{
				return this.<DirectMethodGroupConversion>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DirectMethodGroupConversion>k__BackingField = value;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool IsIterator
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x0003135E File Offset: 0x0002F55E
		public ParametersCompiled Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x00031366 File Offset: 0x0002F566
		// (set) Token: 0x06000D7A RID: 3450 RVA: 0x0003136E File Offset: 0x0002F56E
		public TypeInferenceContext ReturnTypeInference
		{
			get
			{
				return this.return_inference;
			}
			set
			{
				this.return_inference = value;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x00031377 File Offset: 0x0002F577
		public override AnonymousMethodStorey Storey
		{
			get
			{
				return this.storey;
			}
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0003137F File Offset: 0x0002F57F
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			ec.Report.Error(1945, this.loc, "An expression tree cannot contain an anonymous method expression");
			return null;
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x000313A0 File Offset: 0x0002F5A0
		private bool Define(ResolveContext ec)
		{
			if (!base.Block.Resolved && base.Compatible(ec) == null)
			{
				return false;
			}
			if (this.block_name == null)
			{
				MemberCore memberCore = (MemberCore)ec.MemberContext;
				this.block_name = memberCore.MemberName.Basename;
			}
			return true;
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x000313EC File Offset: 0x0002F5EC
		private AnonymousExpression.AnonymousMethodMethod DoCreateMethodHost(EmitContext ec)
		{
			TypeDefinition typeDefinition = null;
			TypeParameters typeParameters = null;
			ParametersCompiled parametersCompiled = this.parameters;
			ExplicitBlock @explicit = base.Block.Original.Explicit;
			Modifiers mod;
			if (@explicit.HasCapturedVariable || @explicit.HasCapturedThis)
			{
				typeDefinition = (this.storey = this.FindBestMethodStorey());
				if (this.storey == null)
				{
					ToplevelBlock topBlock = @explicit.ParametersBlock.TopBlock;
					StateMachine stateMachine = topBlock.StateMachine;
					if (@explicit.HasCapturedThis)
					{
						ParametersBlock parametersBlock = @explicit.ParametersBlock;
						StateMachine stateMachine2;
						do
						{
							stateMachine2 = parametersBlock.StateMachine;
							parametersBlock = ((parametersBlock.Parent == null) ? null : parametersBlock.Parent.ParametersBlock);
						}
						while (stateMachine2 == null && parametersBlock != null);
						if (stateMachine2 == null)
						{
							topBlock.RemoveThisReferenceFromChildrenBlock(@explicit);
						}
						else if (stateMachine2.Kind == MemberKind.Struct)
						{
							typeDefinition = stateMachine2.Parent.PartialContainer;
							typeParameters = stateMachine2.OriginalTypeParameters;
						}
						else if (stateMachine is IteratorStorey)
						{
							typeDefinition = (this.storey = stateMachine);
						}
					}
				}
				mod = ((this.storey != null) ? Modifiers.INTERNAL : Modifiers.PRIVATE);
			}
			else
			{
				if (ec.CurrentAnonymousMethod != null)
				{
					typeDefinition = (this.storey = ec.CurrentAnonymousMethod.Storey);
				}
				mod = (Modifiers.PRIVATE | Modifiers.STATIC);
			}
			if (this.storey == null && typeParameters == null)
			{
				typeParameters = ec.CurrentTypeParameters;
			}
			if (typeDefinition == null)
			{
				typeDefinition = ec.CurrentTypeDefinition.Parent.PartialContainer;
			}
			string host = (typeDefinition != this.storey) ? this.block_name : null;
			string typePrefix = "m";
			string name = null;
			TypeDefinition partialContainer = typeDefinition.PartialContainer;
			int counterAnonymousMethods = partialContainer.CounterAnonymousMethods;
			partialContainer.CounterAnonymousMethods = counterAnonymousMethods + 1;
			string name2 = CompilerGeneratedContainer.MakeName(host, typePrefix, name, counterAnonymousMethods);
			MemberName name3;
			if (typeParameters != null)
			{
				TypeParameters typeParameters2 = new TypeParameters(typeParameters.Count);
				for (int i = 0; i < typeParameters.Count; i++)
				{
					typeParameters2.Add(typeParameters[i].CreateHoistedCopy(null));
				}
				name3 = new MemberName(name2, typeParameters2, base.Location);
			}
			else
			{
				name3 = new MemberName(name2, base.Location);
			}
			return new AnonymousExpression.AnonymousMethodMethod(typeDefinition, this, this.storey, new TypeExpression(this.ReturnType, base.Location), mod, name3, parametersCompiled);
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x000315F7 File Offset: 0x0002F7F7
		protected override Expression DoResolve(ResolveContext ec)
		{
			if (!this.Define(ec))
			{
				return null;
			}
			this.eclass = ExprClass.Value;
			return this;
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0003160C File Offset: 0x0002F80C
		public override void Emit(EmitContext ec)
		{
			if (this.method == null)
			{
				this.method = this.DoCreateMethodHost(ec);
				this.method.Define();
				this.method.PrepareEmit();
			}
			bool flag = (this.method.ModFlags & Modifiers.STATIC) > (Modifiers)0;
			if (flag && this.am_cache == null && !ec.IsStaticConstructor && !this.method.MemberName.IsGeneric)
			{
				TypeDefinition partialContainer = this.method.Parent.PartialContainer;
				TypeDefinition typeDefinition = partialContainer;
				int anonymousMethodsCounter = typeDefinition.AnonymousMethodsCounter;
				typeDefinition.AnonymousMethodsCounter = anonymousMethodsCounter + 1;
				int id = anonymousMethodsCounter;
				TypeSpec t = (this.storey != null && this.storey.Mutator != null) ? this.storey.Mutator.Mutate(this.type) : this.type;
				this.am_cache = new Field(partialContainer, new TypeExpression(t, this.loc), Modifiers.PRIVATE | Modifiers.STATIC | Modifiers.COMPILER_GENERATED, new MemberName(CompilerGeneratedContainer.MakeName(null, "f", "am$cache", id), this.loc), null);
				this.am_cache.Define();
				partialContainer.AddField(this.am_cache);
			}
			Label label = ec.DefineLabel();
			if (this.am_cache != null)
			{
				ec.Emit(OpCodes.Ldsfld, this.am_cache.Spec);
				ec.Emit(OpCodes.Brtrue_S, label);
			}
			if (flag)
			{
				ec.EmitNull();
			}
			else if (this.storey != null)
			{
				Expression expression = this.storey.GetStoreyInstanceExpression(ec).Resolve(new ResolveContext(ec.MemberContext));
				if (expression != null)
				{
					expression.Emit(ec);
				}
			}
			else
			{
				ec.EmitThis();
				if (ec.CurrentAnonymousMethod != null && ec.AsyncTaskStorey != null)
				{
					ec.Emit(OpCodes.Ldfld, ec.AsyncTaskStorey.HoistedThis.Field.Spec);
				}
			}
			MethodSpec methodSpec = this.method.Spec;
			if (this.storey != null && this.storey.MemberName.IsGeneric)
			{
				if (ec.IsAnonymousStoreyMutateRequired)
				{
					ec.Emit(OpCodes.Ldftn, methodSpec);
				}
				else
				{
					TypeSpec type = this.storey.Instance.Type;
					ec.Emit(OpCodes.Ldftn, TypeBuilder.GetMethod(type.GetMetaInfo(), (MethodInfo)methodSpec.GetMetaInfo()));
				}
			}
			else
			{
				if (methodSpec.IsGeneric)
				{
					StateMachine stateMachine = (ec.CurrentAnonymousMethod == null) ? null : (ec.CurrentAnonymousMethod.Storey as StateMachine);
					TypeParameterSpec[] targs;
					if (stateMachine != null && stateMachine.OriginalTypeParameters != null)
					{
						targs = stateMachine.CurrentTypeParameters.Types;
					}
					else
					{
						targs = this.method.TypeParameters;
					}
					methodSpec = methodSpec.MakeGenericMethod(ec.MemberContext, targs);
				}
				ec.Emit(OpCodes.Ldftn, methodSpec);
			}
			MethodSpec constructor = Delegate.GetConstructor(this.type);
			ec.Emit(OpCodes.Newobj, constructor);
			if (this.am_cache != null)
			{
				ec.Emit(OpCodes.Stsfld, this.am_cache.Spec);
				ec.MarkLabel(label);
				ec.Emit(OpCodes.Ldsfld, this.am_cache.Spec);
			}
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override void EmitStatement(EmitContext ec)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00031914 File Offset: 0x0002FB14
		private AnonymousMethodStorey FindBestMethodStorey()
		{
			for (Block parent = base.Block.Parent; parent != null; parent = parent.Parent)
			{
				AnonymousMethodStorey anonymousMethodStorey = parent.Explicit.AnonymousMethodStorey;
				if (anonymousMethodStorey != null)
				{
					return anonymousMethodStorey;
				}
			}
			return null;
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0003194B File Offset: 0x0002FB4B
		public override string GetSignatureForError()
		{
			return this.type.GetSignatureForError();
		}

		// Token: 0x04000657 RID: 1623
		protected readonly ParametersCompiled parameters;

		// Token: 0x04000658 RID: 1624
		private AnonymousMethodStorey storey;

		// Token: 0x04000659 RID: 1625
		private AnonymousExpression.AnonymousMethodMethod method;

		// Token: 0x0400065A RID: 1626
		private Field am_cache;

		// Token: 0x0400065B RID: 1627
		private string block_name;

		// Token: 0x0400065C RID: 1628
		private TypeInferenceContext return_inference;

		// Token: 0x0400065D RID: 1629
		[CompilerGenerated]
		private MethodGroupExpr <DirectMethodGroupConversion>k__BackingField;
	}
}
