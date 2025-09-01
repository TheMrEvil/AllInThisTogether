using System;

namespace Mono.CSharp
{
	// Token: 0x02000165 RID: 357
	internal class DynamicExpressionStatement : ExpressionStatement
	{
		// Token: 0x06001181 RID: 4481 RVA: 0x00047E18 File Offset: 0x00046018
		public DynamicExpressionStatement(IDynamicBinder binder, Arguments args, Location loc)
		{
			this.binder = binder;
			this.arguments = args;
			this.loc = loc;
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x00047E35 File Offset: 0x00046035
		public Arguments Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x00047E3D File Offset: 0x0004603D
		public override bool ContainsEmitWithAwait()
		{
			return this.arguments.ContainsEmitWithAwait();
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x00047E4A File Offset: 0x0004604A
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			ec.Report.Error(1963, this.loc, "An expression tree cannot contain a dynamic operation");
			return null;
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00047E68 File Offset: 0x00046068
		protected override Expression DoResolve(ResolveContext rc)
		{
			if (this.DoResolveCore(rc))
			{
				this.binder_expr = this.binder.CreateCallSiteBinder(rc, this.arguments);
			}
			return this;
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x00047E8C File Offset: 0x0004608C
		protected bool DoResolveCore(ResolveContext rc)
		{
			foreach (Argument argument in this.arguments)
			{
				if (argument.Type == InternalType.VarOutType)
				{
					rc.Report.Error(8047, argument.Expr.Location, "Declaration expression cannot be used in this context");
				}
			}
			if (rc.CurrentTypeParameters != null && rc.CurrentTypeParameters[0].IsMethodTypeParameter)
			{
				this.context_mvars = rc.CurrentTypeParameters;
			}
			int errors = rc.Report.Errors;
			PredefinedTypes predefinedTypes = rc.Module.PredefinedTypes;
			this.binder_type = predefinedTypes.Binder.Resolve();
			predefinedTypes.CallSite.Resolve();
			predefinedTypes.CallSiteGeneric.Resolve();
			this.eclass = ExprClass.Value;
			if (this.type == null)
			{
				this.type = rc.BuiltinTypes.Dynamic;
			}
			if (rc.Report.Errors == errors)
			{
				return true;
			}
			rc.Report.Error(1969, this.loc, "Dynamic operation cannot be compiled without `Microsoft.CSharp.dll' assembly reference");
			return false;
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x00047FBC File Offset: 0x000461BC
		public override void Emit(EmitContext ec)
		{
			this.EmitCall(ec, this.binder_expr, this.arguments, false);
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00047FD2 File Offset: 0x000461D2
		public override void EmitStatement(EmitContext ec)
		{
			this.EmitCall(ec, this.binder_expr, this.arguments, true);
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x00047FE8 File Offset: 0x000461E8
		protected void EmitCall(EmitContext ec, Expression binder, Arguments arguments, bool isStatement)
		{
			int num = (arguments == null) ? 0 : arguments.Count;
			int num2 = isStatement ? 1 : 2;
			ModuleContainer module = ec.Module;
			bool flag = false;
			TypeExpression[] array = new TypeExpression[num + num2];
			array[0] = new TypeExpression(module.PredefinedTypes.CallSite.TypeSpec, this.loc);
			TypeExpression[] array2 = null;
			DynamicSiteClass dynamicSiteClass = ec.CreateDynamicSite();
			TypeParameterMutator typeParameterMutator;
			if (this.context_mvars != null)
			{
				TypeContainer typeContainer = dynamicSiteClass;
				TypeParameters currentTypeParameters;
				do
				{
					currentTypeParameters = typeContainer.CurrentTypeParameters;
					typeContainer = typeContainer.Parent;
				}
				while (currentTypeParameters == null);
				typeParameterMutator = new TypeParameterMutator(this.context_mvars, currentTypeParameters);
				if (!ec.IsAnonymousStoreyMutateRequired)
				{
					array2 = new TypeExpression[array.Length];
					array2[0] = array[0];
				}
			}
			else
			{
				typeParameterMutator = null;
			}
			for (int i = 0; i < num; i++)
			{
				Argument argument = arguments[i];
				if (argument.ArgType == Argument.AType.Out || argument.ArgType == Argument.AType.Ref)
				{
					flag = true;
				}
				TypeSpec typeSpec = argument.Type;
				if (typeSpec.Kind == MemberKind.InternalCompilerType)
				{
					typeSpec = ec.BuiltinTypes.Object;
				}
				if (array2 != null)
				{
					array2[i + 1] = new TypeExpression(typeSpec, this.loc);
				}
				if (typeParameterMutator != null)
				{
					typeSpec = typeSpec.Mutate(typeParameterMutator);
				}
				array[i + 1] = new TypeExpression(typeSpec, this.loc);
			}
			TypeExpr typeExpr = null;
			TypeExpr typeExpr2 = null;
			if (!flag)
			{
				string name = isStatement ? "Action" : "Func";
				TypeSpec typeSpec2 = null;
				Namespace @namespace = module.GlobalRootNamespace.GetNamespace("System", true);
				if (@namespace != null)
				{
					typeSpec2 = @namespace.LookupType(module, name, num + num2, LookupMode.Normal, this.loc);
				}
				if (typeSpec2 != null)
				{
					if (!isStatement)
					{
						TypeSpec typeSpec3 = this.type;
						if (typeSpec3.Kind == MemberKind.InternalCompilerType)
						{
							typeSpec3 = ec.BuiltinTypes.Object;
						}
						if (array2 != null)
						{
							TypeExpression[] array3 = array2;
							array3[array3.Length - 1] = new TypeExpression(typeSpec3, this.loc);
						}
						if (typeParameterMutator != null)
						{
							typeSpec3 = typeSpec3.Mutate(typeParameterMutator);
						}
						TypeExpression[] array4 = array;
						array4[array4.Length - 1] = new TypeExpression(typeSpec3, this.loc);
					}
					typeExpr = new GenericTypeExpr(typeSpec2, new TypeArguments(array), this.loc);
					if (array2 != null)
					{
						typeExpr2 = new GenericTypeExpr(typeSpec2, new TypeArguments(array2), this.loc);
					}
					else
					{
						typeExpr2 = typeExpr;
					}
				}
			}
			Delegate @delegate;
			if (typeExpr == null)
			{
				TypeSpec typeSpec4 = isStatement ? ec.BuiltinTypes.Void : this.type;
				Parameter[] array5 = new Parameter[num + 1];
				array5[0] = new Parameter(array[0], "p0", Parameter.Modifier.NONE, null, this.loc);
				DynamicSiteClass dynamicSiteClass2 = ec.CreateDynamicSite();
				int num3 = (dynamicSiteClass2.Containers == null) ? 0 : dynamicSiteClass2.Containers.Count;
				if (typeParameterMutator != null)
				{
					typeSpec4 = typeParameterMutator.Mutate(typeSpec4);
				}
				for (int j = 1; j < num + 1; j++)
				{
					array5[j] = new Parameter(array[j], "p" + j.ToString("X"), arguments[j - 1].Modifier, null, this.loc);
				}
				@delegate = new Delegate(dynamicSiteClass2, new TypeExpression(typeSpec4, this.loc), Modifiers.INTERNAL | Modifiers.COMPILER_GENERATED, new MemberName("Container" + num3.ToString("X")), new ParametersCompiled(array5), null);
				@delegate.CreateContainer();
				@delegate.DefineContainer();
				@delegate.Define();
				@delegate.PrepareEmit();
				dynamicSiteClass2.AddTypeContainer(@delegate);
				if (dynamicSiteClass2.CurrentType is InflatedTypeSpec && num3 > 0)
				{
					dynamicSiteClass2.CurrentType.MemberCache.AddMember(@delegate.CurrentType);
				}
				typeExpr = new TypeExpression(@delegate.CurrentType, this.loc);
				if (array2 != null)
				{
					typeExpr2 = null;
				}
				else
				{
					typeExpr2 = typeExpr;
				}
			}
			else
			{
				@delegate = null;
			}
			GenericTypeExpr type = new GenericTypeExpr(module.PredefinedTypes.CallSiteGeneric.TypeSpec, new TypeArguments(new FullNamedExpression[]
			{
				typeExpr
			}), this.loc);
			FieldSpec fieldSpec = dynamicSiteClass.CreateCallSiteField(type, this.loc);
			if (fieldSpec == null)
			{
				return;
			}
			if (typeExpr2 == null)
			{
				typeExpr2 = new TypeExpression(MemberCache.GetMember<TypeSpec>(@delegate.CurrentType.DeclaringType.MakeGenericType(module, this.context_mvars.Types), @delegate.CurrentType), this.loc);
			}
			GenericTypeExpr genericTypeExpr = new GenericTypeExpr(module.PredefinedTypes.CallSiteGeneric.TypeSpec, new TypeArguments(new FullNamedExpression[]
			{
				typeExpr2
			}), this.loc);
			if (genericTypeExpr.ResolveAsType(ec.MemberContext, false) == null)
			{
				return;
			}
			bool flag2 = this.context_mvars != null && ec.IsAnonymousStoreyMutateRequired;
			TypeSpec typeSpec5;
			if (flag2 || this.context_mvars == null)
			{
				typeSpec5 = dynamicSiteClass.CurrentType;
			}
			else
			{
				typeSpec5 = dynamicSiteClass.CurrentType.MakeGenericType(module, this.context_mvars.Types);
			}
			if (typeSpec5 is InflatedTypeSpec && dynamicSiteClass.AnonymousMethodsCounter > 1)
			{
				TypeParameterSpec[] tparams = (typeSpec5.MemberDefinition.TypeParametersCount > 0) ? typeSpec5.MemberDefinition.TypeParameters : TypeParameterSpec.EmptyTypes;
				TypeParameterInflator inflator = new TypeParameterInflator(module, typeSpec5, tparams, typeSpec5.TypeArguments);
				typeSpec5.MemberCache.AddMember(fieldSpec.InflateMember(inflator));
			}
			FieldExpr fieldExpr = new FieldExpr(MemberCache.GetMember<FieldSpec>(typeSpec5, fieldSpec), this.loc);
			BlockContext blockContext = new BlockContext(ec.MemberContext, null, ec.BuiltinTypes.Void);
			Arguments arguments2 = new Arguments(1);
			arguments2.Add(new Argument(binder));
			StatementExpression statementExpression = new StatementExpression(new SimpleAssign(fieldExpr, new Invocation(new MemberAccess(genericTypeExpr, "Create"), arguments2)));
			using (ec.With(BuilderContext.Options.OmitDebugInfo, true))
			{
				if (statementExpression.Resolve(blockContext))
				{
					new If(new Binary(Binary.Operator.Equality, fieldExpr, new NullLiteral(this.loc)), statementExpression, this.loc).Emit(ec);
				}
				arguments2 = new Arguments(1 + num);
				arguments2.Add(new Argument(fieldExpr));
				if (arguments != null)
				{
					int num4 = 1;
					foreach (Argument argument2 in arguments)
					{
						if (argument2 is NamedArgument)
						{
							arguments2.Add(new Argument(argument2.Expr, argument2.ArgType));
						}
						else
						{
							arguments2.Add(argument2);
						}
						if (flag2 && argument2.Type != array[num4].Type)
						{
							argument2.Expr.Type = array[num4].Type;
						}
						num4++;
					}
				}
				Expression expression = new DelegateInvocation(new MemberAccess(fieldExpr, "Target", this.loc).Resolve(blockContext), arguments2, false, this.loc).Resolve(blockContext);
				if (expression != null)
				{
					expression.Emit(ec);
				}
			}
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x000486C8 File Offset: 0x000468C8
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			this.arguments.FlowAnalysis(fc, null);
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x000486D7 File Offset: 0x000468D7
		public static MemberAccess GetBinderNamespace(Location loc)
		{
			return new MemberAccess(new MemberAccess(new QualifiedAliasMember(QualifiedAliasMember.GlobalAlias, "Microsoft", loc), "CSharp", loc), "RuntimeBinder", loc);
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x000486FF File Offset: 0x000468FF
		protected MemberAccess GetBinder(string name, Location loc)
		{
			return new MemberAccess(new TypeExpression(this.binder_type, loc), name, loc);
		}

		// Token: 0x0400077F RID: 1919
		private readonly Arguments arguments;

		// Token: 0x04000780 RID: 1920
		protected IDynamicBinder binder;

		// Token: 0x04000781 RID: 1921
		protected Expression binder_expr;

		// Token: 0x04000782 RID: 1922
		protected CSharpBinderFlags flags;

		// Token: 0x04000783 RID: 1923
		private TypeSpec binder_type;

		// Token: 0x04000784 RID: 1924
		private TypeParameters context_mvars;

		// Token: 0x02000390 RID: 912
		protected class BinderFlags : EnumConstant
		{
			// Token: 0x060026C6 RID: 9926 RVA: 0x000B6F6F File Offset: 0x000B516F
			public BinderFlags(CSharpBinderFlags flags, DynamicExpressionStatement statement) : base(statement.loc)
			{
				this.flags = flags;
				this.statement = statement;
				this.eclass = ExprClass.Unresolved;
			}

			// Token: 0x060026C7 RID: 9927 RVA: 0x000B6F94 File Offset: 0x000B5194
			protected override Expression DoResolve(ResolveContext ec)
			{
				this.Child = new IntConstant(ec.BuiltinTypes, (int)(this.flags | this.statement.flags), this.statement.loc);
				this.type = ec.Module.PredefinedTypes.BinderFlags.Resolve();
				this.eclass = this.Child.eclass;
				return this;
			}

			// Token: 0x04000F8B RID: 3979
			private readonly DynamicExpressionStatement statement;

			// Token: 0x04000F8C RID: 3980
			private readonly CSharpBinderFlags flags;
		}
	}
}
