using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x0200010C RID: 268
	public class AnonymousMethodExpression : Expression
	{
		// Token: 0x06000D4C RID: 3404 RVA: 0x000305C6 File Offset: 0x0002E7C6
		public AnonymousMethodExpression(Location loc)
		{
			this.loc = loc;
			this.compatibles = new Dictionary<TypeSpec, Expression>();
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x000305E0 File Offset: 0x0002E7E0
		public override string ExprClassName
		{
			get
			{
				return "anonymous method";
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x000305E7 File Offset: 0x0002E7E7
		public virtual bool HasExplicitParameters
		{
			get
			{
				return this.Parameters != ParametersCompiled.Undefined;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsSideEffectFree
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x000305F9 File Offset: 0x0002E7F9
		public ParametersCompiled Parameters
		{
			get
			{
				return this.Block.Parameters;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x00030606 File Offset: 0x0002E806
		// (set) Token: 0x06000D52 RID: 3410 RVA: 0x0003060E File Offset: 0x0002E80E
		public ReportPrinter TypeInferenceReportPrinter
		{
			[CompilerGenerated]
			get
			{
				return this.<TypeInferenceReportPrinter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TypeInferenceReportPrinter>k__BackingField = value;
			}
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00030618 File Offset: 0x0002E818
		public bool ImplicitStandardConversionExists(ResolveContext ec, TypeSpec delegate_type)
		{
			bool result;
			using (ec.With(ResolveContext.Options.InferReturnType, false))
			{
				using (ec.Set(ResolveContext.Options.ProbingMode))
				{
					ReportPrinter printer = ec.Report.SetPrinter(this.TypeInferenceReportPrinter ?? new NullReportPrinter());
					bool flag = this.Compatible(ec, delegate_type) != null;
					ec.Report.SetPrinter(printer);
					result = flag;
				}
			}
			return result;
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x000306B0 File Offset: 0x0002E8B0
		private TypeSpec CompatibleChecks(ResolveContext ec, TypeSpec delegate_type)
		{
			if (delegate_type.IsDelegate)
			{
				return delegate_type;
			}
			if (!delegate_type.IsExpressionTreeType)
			{
				ec.Report.Error(1660, this.loc, "Cannot convert `{0}' to non-delegate type `{1}'", this.GetSignatureForError(), delegate_type.GetSignatureForError());
				return null;
			}
			delegate_type = delegate_type.TypeArguments[0];
			if (delegate_type.IsDelegate)
			{
				return delegate_type;
			}
			ec.Report.Error(835, this.loc, "Cannot convert `{0}' to an expression tree of non-delegate type `{1}'", this.GetSignatureForError(), delegate_type.GetSignatureForError());
			return null;
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00030734 File Offset: 0x0002E934
		protected bool VerifyExplicitParameters(ResolveContext ec, TypeInferenceContext tic, TypeSpec delegate_type, AParametersCollection parameters)
		{
			if (this.VerifyParameterCompatibility(ec, tic, delegate_type, parameters, ec.IsInProbingMode))
			{
				return true;
			}
			if (!ec.IsInProbingMode)
			{
				ec.Report.Error(1661, this.loc, "Cannot convert `{0}' to delegate type `{1}' since there is a parameter mismatch", this.GetSignatureForError(), delegate_type.GetSignatureForError());
			}
			return false;
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x00030788 File Offset: 0x0002E988
		protected bool VerifyParameterCompatibility(ResolveContext ec, TypeInferenceContext tic, TypeSpec delegate_type, AParametersCollection invoke_pd, bool ignore_errors)
		{
			if (this.Parameters.Count == invoke_pd.Count)
			{
				bool flag = !this.HasExplicitParameters;
				bool flag2 = false;
				for (int i = 0; i < this.Parameters.Count; i++)
				{
					Parameter.Modifier modFlags = invoke_pd.FixedParameters[i].ModFlags;
					if (this.Parameters.FixedParameters[i].ModFlags != modFlags && modFlags != Parameter.Modifier.PARAMS)
					{
						if (ignore_errors)
						{
							return false;
						}
						if (modFlags == Parameter.Modifier.NONE)
						{
							ec.Report.Error(1677, this.Parameters[i].Location, "Parameter `{0}' should not be declared with the `{1}' keyword", (i + 1).ToString(), Parameter.GetModifierSignature(this.Parameters[i].ModFlags));
						}
						else
						{
							ec.Report.Error(1676, this.Parameters[i].Location, "Parameter `{0}' must be declared with the `{1}' keyword", (i + 1).ToString(), Parameter.GetModifierSignature(modFlags));
						}
						flag2 = true;
					}
					if (!flag)
					{
						TypeSpec typeSpec = invoke_pd.Types[i];
						if (tic != null)
						{
							typeSpec = tic.InflateGenericArgument(ec, typeSpec);
						}
						if (!TypeSpecComparer.IsEqual(typeSpec, this.Parameters.Types[i]))
						{
							if (ignore_errors)
							{
								return false;
							}
							ec.Report.Error(1678, this.Parameters[i].Location, "Parameter `{0}' is declared as type `{1}' but should be `{2}'", new string[]
							{
								(i + 1).ToString(),
								this.Parameters.Types[i].GetSignatureForError(),
								invoke_pd.Types[i].GetSignatureForError()
							});
							flag2 = true;
						}
					}
				}
				return !flag2;
			}
			if (ignore_errors)
			{
				return false;
			}
			ec.Report.Error(1593, this.loc, "Delegate `{0}' does not take `{1}' arguments", delegate_type.GetSignatureForError(), this.Parameters.Count.ToString());
			return false;
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0003096C File Offset: 0x0002EB6C
		public bool ExplicitTypeInference(TypeInferenceContext type_inference, TypeSpec delegate_type)
		{
			if (!this.HasExplicitParameters)
			{
				return false;
			}
			if (!delegate_type.IsDelegate)
			{
				if (!delegate_type.IsExpressionTreeType)
				{
					return false;
				}
				delegate_type = TypeManager.GetTypeArguments(delegate_type)[0];
				if (!delegate_type.IsDelegate)
				{
					return false;
				}
			}
			AParametersCollection parameters = Delegate.GetParameters(delegate_type);
			if (parameters.Count != this.Parameters.Count)
			{
				return false;
			}
			TypeSpec[] types = this.Parameters.Types;
			TypeSpec[] types2 = parameters.Types;
			for (int i = 0; i < this.Parameters.Count; i++)
			{
				if (type_inference.ExactInference(types[i], types2[i]) == 0 && types[i] != types2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00030A08 File Offset: 0x0002EC08
		public TypeSpec InferReturnType(ResolveContext ec, TypeInferenceContext tic, TypeSpec delegate_type)
		{
			Expression expression;
			if (this.compatibles.TryGetValue(delegate_type, out expression))
			{
				AnonymousExpression anonymousExpression = expression as AnonymousExpression;
				if (anonymousExpression != null)
				{
					return anonymousExpression.ReturnType;
				}
				return null;
			}
			else
			{
				AnonymousExpression anonymousExpression;
				using (ec.Set(ResolveContext.Options.ProbingMode | ResolveContext.Options.InferReturnType))
				{
					ReportPrinter printer;
					if (this.TypeInferenceReportPrinter != null)
					{
						printer = ec.Report.SetPrinter(this.TypeInferenceReportPrinter);
					}
					else
					{
						printer = null;
					}
					AnonymousMethodBody anonymousMethodBody = this.CompatibleMethodBody(ec, tic, null, delegate_type);
					if (anonymousMethodBody != null)
					{
						anonymousExpression = anonymousMethodBody.Compatible(ec, anonymousMethodBody);
					}
					else
					{
						anonymousExpression = null;
					}
					if (this.TypeInferenceReportPrinter != null)
					{
						ec.Report.SetPrinter(printer);
					}
				}
				if (anonymousExpression == null)
				{
					return null;
				}
				return anonymousExpression.ReturnType;
			}
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool ContainsEmitWithAwait()
		{
			return false;
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00030AC4 File Offset: 0x0002ECC4
		public Expression Compatible(ResolveContext ec, TypeSpec type)
		{
			Expression expression;
			if (this.compatibles.TryGetValue(type, out expression))
			{
				return expression;
			}
			if (type == InternalType.ErrorType)
			{
				return null;
			}
			TypeSpec typeSpec = this.CompatibleChecks(ec, type);
			if (typeSpec == null)
			{
				return null;
			}
			TypeSpec returnType = Delegate.GetInvokeMethod(typeSpec).ReturnType;
			AnonymousMethodBody anonymousMethodBody = this.CompatibleMethodBody(ec, null, returnType, typeSpec);
			if (anonymousMethodBody == null)
			{
				return null;
			}
			bool flag = typeSpec != type;
			try
			{
				if (flag)
				{
					if (ec.HasSet(ResolveContext.Options.ExpressionTreeConversion))
					{
						expression = anonymousMethodBody.Compatible(ec, ec.CurrentAnonymousMethod);
						if (expression != null)
						{
							expression = new AnonymousMethodExpression.Quote(expression);
						}
					}
					else
					{
						int errors = ec.Report.Errors;
						if (this.Block.IsAsync)
						{
							ec.Report.Error(1989, this.loc, "Async lambda expressions cannot be converted to expression trees");
						}
						using (ec.Set(ResolveContext.Options.ExpressionTreeConversion))
						{
							expression = anonymousMethodBody.Compatible(ec);
						}
						if (expression != null && errors == ec.Report.Errors)
						{
							expression = this.CreateExpressionTree(ec, typeSpec);
						}
					}
				}
				else
				{
					expression = anonymousMethodBody.Compatible(ec);
					if (anonymousMethodBody.DirectMethodGroupConversion != null)
					{
						SessionReportPrinter sessionReportPrinter = new SessionReportPrinter();
						ReportPrinter printer = ec.Report.SetPrinter(sessionReportPrinter);
						Expression expression2 = new ImplicitDelegateCreation(typeSpec, anonymousMethodBody.DirectMethodGroupConversion, this.loc)
						{
							AllowSpecialMethodsInvocation = true
						}.Resolve(ec);
						ec.Report.SetPrinter(printer);
						if (expression2 != null && sessionReportPrinter.ErrorsCount == 0)
						{
							expression = expression2;
						}
					}
				}
			}
			catch (CompletionResult)
			{
				throw;
			}
			catch (FatalException)
			{
				throw;
			}
			catch (Exception e)
			{
				throw new InternalErrorException(e, this.loc);
			}
			if (!ec.IsInProbingMode && !flag)
			{
				this.compatibles.Add(type, expression ?? EmptyExpression.Null);
			}
			return expression;
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00030CCC File Offset: 0x0002EECC
		protected virtual Expression CreateExpressionTree(ResolveContext ec, TypeSpec delegate_type)
		{
			return this.CreateExpressionTree(ec);
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x00030CD5 File Offset: 0x0002EED5
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			ec.Report.Error(1946, this.loc, "An anonymous method cannot be converted to an expression tree");
			return null;
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00030CF4 File Offset: 0x0002EEF4
		protected virtual ParametersCompiled ResolveParameters(ResolveContext ec, TypeInferenceContext tic, TypeSpec delegate_type)
		{
			AParametersCollection parameters = Delegate.GetParameters(delegate_type);
			if (this.Parameters == ParametersCompiled.Undefined)
			{
				Parameter[] array = new Parameter[parameters.Count];
				for (int i = 0; i < parameters.Count; i++)
				{
					if ((parameters.FixedParameters[i].ModFlags & Parameter.Modifier.OUT) != Parameter.Modifier.NONE)
					{
						if (!ec.IsInProbingMode)
						{
							ec.Report.Error(1688, this.loc, "Cannot convert anonymous method block without a parameter list to delegate type `{0}' because it has one or more `out' parameters", delegate_type.GetSignatureForError());
						}
						return null;
					}
					array[i] = new Parameter(new TypeExpression(parameters.Types[i], this.loc), null, parameters.FixedParameters[i].ModFlags, null, this.loc);
				}
				return ParametersCompiled.CreateFullyResolved(array, parameters.Types);
			}
			if (!this.VerifyExplicitParameters(ec, tic, delegate_type, parameters))
			{
				return null;
			}
			return this.Parameters;
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00030DC4 File Offset: 0x0002EFC4
		protected override Expression DoResolve(ResolveContext rc)
		{
			if (rc.HasSet(ResolveContext.Options.ConstantScope))
			{
				rc.Report.Error(1706, this.loc, "Anonymous methods and lambda expressions cannot be used in the current context");
				return null;
			}
			if (rc.HasAny(ResolveContext.Options.FieldInitializerScope | ResolveContext.Options.BaseInitializer) && rc.CurrentMemberDefinition.Parent.PartialContainer.PrimaryConstructorParameters != null)
			{
				ToplevelBlock topBlock = rc.ConstructorBlock.ParametersBlock.TopBlock;
				if (this.Block.TopBlock != topBlock)
				{
					Block block = this.Block;
					while (block.Parent != this.Block.TopBlock && block != this.Block.TopBlock)
					{
						block = block.Parent;
					}
					block.Parent = topBlock;
					topBlock.IncludeBlock(this.Block, this.Block.TopBlock);
					block.ParametersBlock.TopBlock = topBlock;
				}
			}
			this.eclass = ExprClass.Value;
			this.type = InternalType.AnonymousMethod;
			if (!this.DoResolveParameters(rc))
			{
				return null;
			}
			return this;
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00030EBA File Offset: 0x0002F0BA
		protected virtual bool DoResolveParameters(ResolveContext rc)
		{
			return this.Parameters.Resolve(rc);
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0000AF70 File Offset: 0x00009170
		public override void Emit(EmitContext ec)
		{
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00030EC8 File Offset: 0x0002F0C8
		public static void Error_AddressOfCapturedVar(ResolveContext rc, IVariableReference var, Location loc)
		{
			if (rc.CurrentAnonymousMethod is AsyncInitializer)
			{
				return;
			}
			rc.Report.Error(1686, loc, "Local variable or parameter `{0}' cannot have their address taken and be used inside an anonymous method, lambda expression or query expression", var.Name);
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x00030EF4 File Offset: 0x0002F0F4
		public override string GetSignatureForError()
		{
			return this.ExprClassName;
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00030EFC File Offset: 0x0002F0FC
		private AnonymousMethodBody CompatibleMethodBody(ResolveContext ec, TypeInferenceContext tic, TypeSpec return_type, TypeSpec delegate_type)
		{
			ParametersCompiled parametersCompiled = this.ResolveParameters(ec, tic, delegate_type);
			if (parametersCompiled == null)
			{
				return null;
			}
			ParametersBlock parametersBlock = ec.IsInProbingMode ? ((ParametersBlock)this.Block.PerformClone()) : this.Block;
			if (parametersBlock.IsAsync)
			{
				if (return_type != null && return_type.Kind != MemberKind.Void && return_type != ec.Module.PredefinedTypes.Task.TypeSpec && !return_type.IsGenericTask)
				{
					ec.Report.Error(4010, this.loc, "Cannot convert async {0} to delegate type `{1}'", this.GetSignatureForError(), delegate_type.GetSignatureForError());
					return null;
				}
				parametersBlock = parametersBlock.ConvertToAsyncTask(ec, ec.CurrentMemberDefinition.Parent.PartialContainer, parametersCompiled, return_type, delegate_type, this.loc);
			}
			return this.CompatibleMethodFactory(return_type ?? InternalType.ErrorType, delegate_type, parametersCompiled, parametersBlock);
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x00030FD4 File Offset: 0x0002F1D4
		protected virtual AnonymousMethodBody CompatibleMethodFactory(TypeSpec return_type, TypeSpec delegate_type, ParametersCompiled p, ParametersBlock b)
		{
			return new AnonymousMethodBody(p, b, return_type, delegate_type, this.loc);
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00030FE6 File Offset: 0x0002F1E6
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			((AnonymousMethodExpression)t).Block = (ParametersBlock)clonectx.LookupBlock(this.Block);
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00031004 File Offset: 0x0002F204
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000652 RID: 1618
		private readonly Dictionary<TypeSpec, Expression> compatibles;

		// Token: 0x04000653 RID: 1619
		public ParametersBlock Block;

		// Token: 0x04000654 RID: 1620
		[CompilerGenerated]
		private ReportPrinter <TypeInferenceReportPrinter>k__BackingField;

		// Token: 0x0200037E RID: 894
		private class Quote : ShimExpression
		{
			// Token: 0x06002690 RID: 9872 RVA: 0x000B6A33 File Offset: 0x000B4C33
			public Quote(Expression expr) : base(expr)
			{
			}

			// Token: 0x06002691 RID: 9873 RVA: 0x000B6A3C File Offset: 0x000B4C3C
			public override Expression CreateExpressionTree(ResolveContext ec)
			{
				Arguments arguments = new Arguments(1);
				arguments.Add(new Argument(this.expr.CreateExpressionTree(ec)));
				return base.CreateExpressionFactoryCall(ec, "Quote", arguments);
			}

			// Token: 0x06002692 RID: 9874 RVA: 0x000B6A74 File Offset: 0x000B4C74
			protected override Expression DoResolve(ResolveContext rc)
			{
				this.expr = this.expr.Resolve(rc);
				if (this.expr == null)
				{
					return null;
				}
				this.eclass = this.expr.eclass;
				this.type = this.expr.Type;
				return this;
			}
		}
	}
}
