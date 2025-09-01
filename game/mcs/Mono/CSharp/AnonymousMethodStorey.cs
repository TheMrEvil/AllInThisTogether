using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x02000107 RID: 263
	public class AnonymousMethodStorey : HoistedStoreyClass
	{
		// Token: 0x06000D24 RID: 3364 RVA: 0x0002F90C File Offset: 0x0002DB0C
		public AnonymousMethodStorey(ExplicitBlock block, TypeDefinition parent, MemberBase host, TypeParameters tparams, string name, MemberKind kind) : base(parent, CompilerGeneratedContainer.MakeMemberName(host, name, parent.PartialContainer.CounterAnonymousContainers, tparams, block.StartLocation), tparams, (Modifiers)0, kind)
		{
			this.OriginalSourceBlock = block;
			TypeDefinition partialContainer = parent.PartialContainer;
			int counterAnonymousContainers = partialContainer.CounterAnonymousContainers;
			partialContainer.CounterAnonymousContainers = counterAnonymousContainers + 1;
			this.ID = counterAnonymousContainers;
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0002F964 File Offset: 0x0002DB64
		public void AddCapturedThisField(EmitContext ec, AnonymousMethodStorey parent)
		{
			TypeExpr type = new TypeExpression(ec.CurrentType, base.Location);
			Field field = this.AddCompilerGeneratedField("$this", type);
			this.hoisted_this = new HoistedThis(this, field);
			this.initialize_hoisted_this = true;
			this.hoisted_this_parent = parent;
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0002F9AC File Offset: 0x0002DBAC
		public Field AddCapturedVariable(string name, TypeSpec type)
		{
			base.CheckMembersDefined();
			FullNamedExpression type2 = new TypeExpression(type, base.Location);
			if (!this.spec.IsGenericOrParentIsGeneric)
			{
				return this.AddCompilerGeneratedField(name, type2);
			}
			Field field = new HoistedStoreyClass.HoistedField(this, type2, Modifiers.INTERNAL | Modifiers.COMPILER_GENERATED, name, null, base.Location);
			base.AddField(field);
			return field;
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0002FA00 File Offset: 0x0002DC00
		protected Field AddCompilerGeneratedField(string name, FullNamedExpression type)
		{
			return this.AddCompilerGeneratedField(name, type, false);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0002FA0C File Offset: 0x0002DC0C
		protected Field AddCompilerGeneratedField(string name, FullNamedExpression type, bool privateAccess)
		{
			Modifiers mod = Modifiers.COMPILER_GENERATED | (privateAccess ? Modifiers.PRIVATE : Modifiers.INTERNAL);
			Field field = new Field(this, type, mod, new MemberName(name, base.Location), null);
			base.AddField(field);
			return field;
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0002FA46 File Offset: 0x0002DC46
		public void AddReferenceFromChildrenBlock(ExplicitBlock block)
		{
			if (this.children_references == null)
			{
				this.children_references = new List<ExplicitBlock>();
			}
			if (!this.children_references.Contains(block))
			{
				this.children_references.Add(block);
			}
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x0002FA78 File Offset: 0x0002DC78
		public void AddParentStoreyReference(EmitContext ec, AnonymousMethodStorey storey)
		{
			base.CheckMembersDefined();
			if (this.used_parent_storeys == null)
			{
				this.used_parent_storeys = new List<AnonymousMethodStorey.StoreyFieldPair>();
			}
			else if (this.used_parent_storeys.Exists((AnonymousMethodStorey.StoreyFieldPair i) => i.Storey == storey))
			{
				return;
			}
			TypeExpr type = storey.CreateStoreyTypeExpression(ec);
			Field field = this.AddCompilerGeneratedField("<>f__ref$" + storey.ID, type);
			this.used_parent_storeys.Add(new AnonymousMethodStorey.StoreyFieldPair(storey, field));
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0002FB10 File Offset: 0x0002DD10
		public void CaptureLocalVariable(ResolveContext ec, LocalVariable localVariable)
		{
			if (this is StateMachine)
			{
				if (ec.CurrentBlock.ParametersBlock != localVariable.Block.ParametersBlock)
				{
					ec.CurrentBlock.Explicit.HasCapturedVariable = true;
				}
			}
			else
			{
				ec.CurrentBlock.Explicit.HasCapturedVariable = true;
			}
			HoistedVariable hoistedVariable = localVariable.HoistedVariant;
			if (hoistedVariable != null && hoistedVariable.Storey != this && hoistedVariable.Storey is StateMachine)
			{
				hoistedVariable.Storey.hoisted_locals.Remove(hoistedVariable);
				hoistedVariable.Storey.Members.Remove(hoistedVariable.Field);
				hoistedVariable = null;
			}
			if (hoistedVariable == null)
			{
				hoistedVariable = new HoistedLocalVariable(this, localVariable, this.GetVariableMangledName(localVariable));
				localVariable.HoistedVariant = hoistedVariable;
				if (this.hoisted_locals == null)
				{
					this.hoisted_locals = new List<HoistedVariable>();
				}
				this.hoisted_locals.Add(hoistedVariable);
			}
			if (ec.CurrentBlock.Explicit != localVariable.Block.Explicit && !(hoistedVariable.Storey is StateMachine) && hoistedVariable.Storey != null)
			{
				hoistedVariable.Storey.AddReferenceFromChildrenBlock(ec.CurrentBlock.Explicit);
			}
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0002FC28 File Offset: 0x0002DE28
		public void CaptureParameter(ResolveContext ec, ParametersBlock.ParameterInfo parameterInfo, ParameterReference parameterReference)
		{
			if (!(this is StateMachine))
			{
				ec.CurrentBlock.Explicit.HasCapturedVariable = true;
			}
			HoistedParameter hoistedParameter = parameterInfo.Parameter.HoistedVariant;
			if (parameterInfo.Block.StateMachine != null)
			{
				if (hoistedParameter == null && parameterInfo.Block.StateMachine != this)
				{
					StateMachine stateMachine = parameterInfo.Block.StateMachine;
					hoistedParameter = new HoistedParameter(stateMachine, parameterReference);
					parameterInfo.Parameter.HoistedVariant = hoistedParameter;
					if (stateMachine.hoisted_params == null)
					{
						stateMachine.hoisted_params = new List<HoistedParameter>();
					}
					stateMachine.hoisted_params.Add(hoistedParameter);
				}
				if (hoistedParameter != null && hoistedParameter.Storey != this && hoistedParameter.Storey is StateMachine)
				{
					if (this.hoisted_local_params == null)
					{
						this.hoisted_local_params = new List<HoistedParameter>();
					}
					this.hoisted_local_params.Add(hoistedParameter);
					hoistedParameter = null;
				}
			}
			if (hoistedParameter == null)
			{
				hoistedParameter = new HoistedParameter(this, parameterReference);
				parameterInfo.Parameter.HoistedVariant = hoistedParameter;
				if (this.hoisted_params == null)
				{
					this.hoisted_params = new List<HoistedParameter>();
				}
				this.hoisted_params.Add(hoistedParameter);
			}
			if (ec.CurrentBlock.Explicit != parameterInfo.Block)
			{
				hoistedParameter.Storey.AddReferenceFromChildrenBlock(ec.CurrentBlock.Explicit);
			}
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0002FD54 File Offset: 0x0002DF54
		private TypeExpr CreateStoreyTypeExpression(EmitContext ec)
		{
			TypeExpr result;
			if (this.CurrentTypeParameters != null)
			{
				TypeParameters typeParameters = (ec.CurrentAnonymousMethod != null && ec.CurrentAnonymousMethod.Storey != null) ? ec.CurrentAnonymousMethod.Storey.CurrentTypeParameters : ec.CurrentTypeParameters;
				TypeArguments typeArguments = new TypeArguments(new FullNamedExpression[0]);
				for (int i = 0; i < typeParameters.Count; i++)
				{
					typeArguments.Add(new SimpleName(typeParameters[i].Name, base.Location));
				}
				result = new GenericTypeExpr(base.Definition, typeArguments, base.Location);
			}
			else
			{
				result = new TypeExpression(this.CurrentType, base.Location);
			}
			return result;
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0002FDF9 File Offset: 0x0002DFF9
		public void SetNestedStoryParent(AnonymousMethodStorey parentStorey)
		{
			this.Parent = parentStorey;
			this.spec.IsGeneric = false;
			this.spec.DeclaringType = parentStorey.CurrentType;
			base.MemberName.TypeParameters = null;
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0002FE2C File Offset: 0x0002E02C
		protected override bool DoResolveTypeParameters()
		{
			if (this.CurrentTypeParameters != null)
			{
				for (int i = 0; i < this.CurrentTypeParameters.Count; i++)
				{
					TypeParameterSpec type = this.CurrentTypeParameters[i].Type;
					type.BaseType = this.mutator.Mutate(type.BaseType);
					if (type.InterfacesDefined != null)
					{
						TypeSpec[] array = new TypeSpec[type.InterfacesDefined.Length];
						for (int j = 0; j < array.Length; j++)
						{
							array[j] = this.mutator.Mutate(type.InterfacesDefined[j]);
						}
						type.InterfacesDefined = array;
					}
					if (type.TypeArguments != null)
					{
						type.TypeArguments = this.mutator.Mutate(type.TypeArguments);
					}
				}
			}
			this.Parent.CurrentType.MemberCache.AddMember(this.spec);
			return true;
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0002FF08 File Offset: 0x0002E108
		public void EmitStoreyInstantiation(EmitContext ec, ExplicitBlock block)
		{
			if (this.Instance != null)
			{
				throw new InternalErrorException();
			}
			ResolveContext resolveContext = new ResolveContext(ec.MemberContext);
			resolveContext.CurrentBlock = block;
			TypeExpr typeExpr = this.CreateStoreyTypeExpression(ec);
			Expression expression = new New(typeExpr, null, base.Location).Resolve(resolveContext);
			if (ec.CurrentAnonymousMethod is StateMachineInitializer && (block.HasYield || block.HasAwait))
			{
				Field field = ec.CurrentAnonymousMethod.Storey.AddCompilerGeneratedField(LocalVariable.GetCompilerGeneratedName(block), typeExpr, true);
				field.Define();
				field.Emit();
				FieldExpr fieldExpr = new FieldExpr(field, base.Location);
				fieldExpr.InstanceExpression = new CompilerGeneratedThis(ec.CurrentType, base.Location);
				fieldExpr.EmitAssign(ec, expression, false, false);
				this.Instance = fieldExpr;
			}
			else
			{
				TemporaryVariableReference temporaryVariableReference = TemporaryVariableReference.Create(expression.Type, block, base.Location);
				if (expression.Type.IsStruct)
				{
					temporaryVariableReference.LocalInfo.CreateBuilder(ec);
				}
				else
				{
					temporaryVariableReference.EmitAssign(ec, expression);
				}
				this.Instance = temporaryVariableReference;
			}
			this.EmitHoistedFieldsInitialization(resolveContext, ec);
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00030014 File Offset: 0x0002E214
		private void EmitHoistedFieldsInitialization(ResolveContext rc, EmitContext ec)
		{
			if (this.used_parent_storeys != null)
			{
				foreach (AnonymousMethodStorey.StoreyFieldPair storeyFieldPair in this.used_parent_storeys)
				{
					Expression storeyInstanceExpression = this.GetStoreyInstanceExpression(ec);
					FieldSpec spec = storeyFieldPair.Field.Spec;
					if (TypeManager.IsGenericType(storeyInstanceExpression.Type))
					{
						spec = MemberCache.GetMember<FieldSpec>(storeyInstanceExpression.Type, spec);
					}
					SimpleAssign simpleAssign = new SimpleAssign(new FieldExpr(spec, base.Location)
					{
						InstanceExpression = storeyInstanceExpression
					}, storeyFieldPair.Storey.GetStoreyInstanceExpression(ec));
					if (simpleAssign.Resolve(rc) != null)
					{
						simpleAssign.EmitStatement(ec);
					}
				}
			}
			if (this.initialize_hoisted_this)
			{
				rc.CurrentBlock.AddScopeStatement(new AnonymousMethodStorey.ThisInitializer(this.hoisted_this, this.hoisted_this_parent));
			}
			AnonymousExpression currentAnonymousMethod = ec.CurrentAnonymousMethod;
			ec.CurrentAnonymousMethod = null;
			if (this.hoisted_params != null)
			{
				this.EmitHoistedParameters(ec, this.hoisted_params);
			}
			ec.CurrentAnonymousMethod = currentAnonymousMethod;
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00030124 File Offset: 0x0002E324
		protected virtual void EmitHoistedParameters(EmitContext ec, List<HoistedParameter> hoisted)
		{
			using (List<HoistedParameter>.Enumerator enumerator = hoisted.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					HoistedParameter hp = enumerator.Current;
					if (hp != null)
					{
						if (this.hoisted_local_params != null)
						{
							FieldExpr fieldExpr = new FieldExpr(this.hoisted_local_params.Find((HoistedParameter l) => l.Parameter.Parameter == hp.Parameter.Parameter).Field, base.Location);
							fieldExpr.InstanceExpression = new CompilerGeneratedThis(this.CurrentType, base.Location);
							hp.EmitAssign(ec, fieldExpr, false, false);
						}
						else
						{
							hp.EmitHoistingAssignment(ec);
						}
					}
				}
			}
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x000301E8 File Offset: 0x0002E3E8
		private Field GetReferencedStoreyField(AnonymousMethodStorey storey)
		{
			if (this.used_parent_storeys == null)
			{
				return null;
			}
			foreach (AnonymousMethodStorey.StoreyFieldPair storeyFieldPair in this.used_parent_storeys)
			{
				if (storeyFieldPair.Storey == storey)
				{
					return storeyFieldPair.Field;
				}
			}
			return null;
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00030254 File Offset: 0x0002E454
		public Expression GetStoreyInstanceExpression(EmitContext ec)
		{
			AnonymousExpression currentAnonymousMethod = ec.CurrentAnonymousMethod;
			if (currentAnonymousMethod == null)
			{
				return this.Instance;
			}
			if (currentAnonymousMethod.Storey == null)
			{
				return this.Instance;
			}
			Field referencedStoreyField = currentAnonymousMethod.Storey.GetReferencedStoreyField(this);
			if (referencedStoreyField != null)
			{
				return new FieldExpr(referencedStoreyField, base.Location)
				{
					InstanceExpression = new CompilerGeneratedThis(this.CurrentType, base.Location)
				};
			}
			if (currentAnonymousMethod.Storey == this)
			{
				return new CompilerGeneratedThis(this.CurrentType, base.Location);
			}
			return this.Instance;
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x000302D6 File Offset: 0x0002E4D6
		protected virtual string GetVariableMangledName(LocalVariable local_info)
		{
			return local_info.Name;
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x000302DE File Offset: 0x0002E4DE
		// (set) Token: 0x06000D37 RID: 3383 RVA: 0x000302E6 File Offset: 0x0002E4E6
		public HoistedThis HoistedThis
		{
			get
			{
				return this.hoisted_this;
			}
			set
			{
				this.hoisted_this = value;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x000302EF File Offset: 0x0002E4EF
		public IList<ExplicitBlock> ReferencesFromChildrenBlock
		{
			get
			{
				return this.children_references;
			}
		}

		// Token: 0x04000641 RID: 1601
		public readonly int ID;

		// Token: 0x04000642 RID: 1602
		public readonly ExplicitBlock OriginalSourceBlock;

		// Token: 0x04000643 RID: 1603
		private List<AnonymousMethodStorey.StoreyFieldPair> used_parent_storeys;

		// Token: 0x04000644 RID: 1604
		private List<ExplicitBlock> children_references;

		// Token: 0x04000645 RID: 1605
		protected List<HoistedParameter> hoisted_params;

		// Token: 0x04000646 RID: 1606
		private List<HoistedParameter> hoisted_local_params;

		// Token: 0x04000647 RID: 1607
		protected List<HoistedVariable> hoisted_locals;

		// Token: 0x04000648 RID: 1608
		protected HoistedThis hoisted_this;

		// Token: 0x04000649 RID: 1609
		public Expression Instance;

		// Token: 0x0400064A RID: 1610
		private bool initialize_hoisted_this;

		// Token: 0x0400064B RID: 1611
		private AnonymousMethodStorey hoisted_this_parent;

		// Token: 0x02000378 RID: 888
		private struct StoreyFieldPair
		{
			// Token: 0x06002680 RID: 9856 RVA: 0x000B68DF File Offset: 0x000B4ADF
			public StoreyFieldPair(AnonymousMethodStorey storey, Field field)
			{
				this.Storey = storey;
				this.Field = field;
			}

			// Token: 0x04000F42 RID: 3906
			public readonly AnonymousMethodStorey Storey;

			// Token: 0x04000F43 RID: 3907
			public readonly Field Field;
		}

		// Token: 0x02000379 RID: 889
		private sealed class ThisInitializer : Statement
		{
			// Token: 0x06002681 RID: 9857 RVA: 0x000B68EF File Offset: 0x000B4AEF
			public ThisInitializer(HoistedThis hoisted_this, AnonymousMethodStorey parent)
			{
				this.hoisted_this = hoisted_this;
				this.parent = parent;
			}

			// Token: 0x06002682 RID: 9858 RVA: 0x000B6908 File Offset: 0x000B4B08
			protected override void DoEmit(EmitContext ec)
			{
				Expression source;
				if (this.parent == null)
				{
					source = new CompilerGeneratedThis(ec.CurrentType, this.loc);
				}
				else
				{
					source = new FieldExpr(this.parent.HoistedThis.Field, Location.Null)
					{
						InstanceExpression = new CompilerGeneratedThis(ec.CurrentType, Location.Null)
					};
				}
				this.hoisted_this.EmitAssign(ec, source, false, false);
			}

			// Token: 0x06002683 RID: 9859 RVA: 0x000022F4 File Offset: 0x000004F4
			protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
			{
				return false;
			}

			// Token: 0x06002684 RID: 9860 RVA: 0x0000AF70 File Offset: 0x00009170
			protected override void CloneTo(CloneContext clonectx, Statement target)
			{
			}

			// Token: 0x04000F44 RID: 3908
			private readonly HoistedThis hoisted_this;

			// Token: 0x04000F45 RID: 3909
			private readonly AnonymousMethodStorey parent;
		}

		// Token: 0x0200037A RID: 890
		[CompilerGenerated]
		private sealed class <>c__DisplayClass19_0
		{
			// Token: 0x06002685 RID: 9861 RVA: 0x00002CCC File Offset: 0x00000ECC
			public <>c__DisplayClass19_0()
			{
			}

			// Token: 0x06002686 RID: 9862 RVA: 0x000B6971 File Offset: 0x000B4B71
			internal bool <AddParentStoreyReference>b__0(AnonymousMethodStorey.StoreyFieldPair i)
			{
				return i.Storey == this.storey;
			}

			// Token: 0x04000F46 RID: 3910
			public AnonymousMethodStorey storey;
		}

		// Token: 0x0200037B RID: 891
		[CompilerGenerated]
		private sealed class <>c__DisplayClass27_0
		{
			// Token: 0x06002687 RID: 9863 RVA: 0x00002CCC File Offset: 0x00000ECC
			public <>c__DisplayClass27_0()
			{
			}

			// Token: 0x06002688 RID: 9864 RVA: 0x000B6981 File Offset: 0x000B4B81
			internal bool <EmitHoistedParameters>b__0(HoistedParameter l)
			{
				return l.Parameter.Parameter == this.hp.Parameter.Parameter;
			}

			// Token: 0x04000F47 RID: 3911
			public HoistedParameter hp;
		}
	}
}
