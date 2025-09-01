using System;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x02000194 RID: 404
	public abstract class DelegateCreation : Expression, OverloadResolver.IErrorHandler
	{
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060015CE RID: 5582 RVA: 0x00068539 File Offset: 0x00066739
		// (set) Token: 0x060015CF RID: 5583 RVA: 0x00068541 File Offset: 0x00066741
		public bool AllowSpecialMethodsInvocation
		{
			[CompilerGenerated]
			get
			{
				return this.<AllowSpecialMethodsInvocation>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AllowSpecialMethodsInvocation>k__BackingField = value;
			}
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x0006854C File Offset: 0x0006674C
		public override bool ContainsEmitWithAwait()
		{
			Expression instanceExpression = this.method_group.InstanceExpression;
			return instanceExpression != null && instanceExpression.ContainsEmitWithAwait();
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x00068570 File Offset: 0x00066770
		public static Arguments CreateDelegateMethodArguments(ResolveContext rc, AParametersCollection pd, TypeSpec[] types, Location loc)
		{
			Arguments arguments = new Arguments(pd.Count);
			for (int i = 0; i < pd.Count; i++)
			{
				Parameter.Modifier modifier = pd.FixedParameters[i].ModFlags & Parameter.Modifier.RefOutMask;
				Argument.AType type;
				if (modifier != Parameter.Modifier.REF)
				{
					if (modifier != Parameter.Modifier.OUT)
					{
						type = Argument.AType.None;
					}
					else
					{
						type = Argument.AType.Out;
					}
				}
				else
				{
					type = Argument.AType.Ref;
				}
				TypeSpec typeSpec = types[i];
				if (typeSpec.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
				{
					typeSpec = rc.BuiltinTypes.Object;
				}
				arguments.Add(new Argument(new TypeExpression(typeSpec, loc), type));
			}
			return arguments;
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x000685F0 File Offset: 0x000667F0
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			Expression expr = new MemberAccess(new MemberAccess(new QualifiedAliasMember("global", "System", this.loc), "Delegate", this.loc), "CreateDelegate", this.loc);
			Arguments arguments = new Arguments(3);
			arguments.Add(new Argument(new TypeOf(this.type, this.loc)));
			if (this.method_group.InstanceExpression == null)
			{
				arguments.Add(new Argument(new NullLiteral(this.loc)));
			}
			else
			{
				arguments.Add(new Argument(this.method_group.InstanceExpression));
			}
			arguments.Add(new Argument(this.method_group.CreateExpressionTree(ec)));
			Expression expression = new Invocation(expr, arguments).Resolve(ec);
			if (expression == null)
			{
				return null;
			}
			expression = Convert.ExplicitConversion(ec, expression, this.type, this.loc);
			if (expression == null)
			{
				return null;
			}
			return expression.CreateExpressionTree(ec);
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x000686DC File Offset: 0x000668DC
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.constructor_method = Delegate.GetConstructor(this.type);
			MethodSpec invokeMethod = Delegate.GetInvokeMethod(this.type);
			if (!ec.HasSet(ResolveContext.Options.ConditionalAccessReceiver) && this.method_group.HasConditionalAccess())
			{
				this.conditional_access_receiver = true;
				ec.Set(ResolveContext.Options.ConditionalAccessReceiver);
			}
			Arguments arguments = DelegateCreation.CreateDelegateMethodArguments(ec, invokeMethod.Parameters, invokeMethod.Parameters.Types, this.loc);
			this.method_group = this.method_group.OverloadResolve(ec, ref arguments, this, OverloadResolver.Restrictions.CovariantDelegate);
			if (this.conditional_access_receiver)
			{
				ec.With(ResolveContext.Options.ConditionalAccessReceiver, false);
			}
			if (this.method_group == null)
			{
				return null;
			}
			MethodSpec bestCandidate = this.method_group.BestCandidate;
			if (bestCandidate.DeclaringType.IsNullableType)
			{
				ec.Report.Error(1728, this.loc, "Cannot create delegate from method `{0}' because it is a member of System.Nullable<T> type", bestCandidate.GetSignatureForError());
				return null;
			}
			if (!this.AllowSpecialMethodsInvocation)
			{
				Invocation.IsSpecialMethodInvocation(ec, bestCandidate, this.loc);
			}
			ExtensionMethodGroupExpr extensionMethodGroupExpr = this.method_group as ExtensionMethodGroupExpr;
			if (extensionMethodGroupExpr != null)
			{
				this.method_group.InstanceExpression = extensionMethodGroupExpr.ExtensionExpression;
				TypeSpec type = extensionMethodGroupExpr.ExtensionExpression.Type;
				if (TypeSpec.IsValueType(type))
				{
					ec.Report.Error(1113, this.loc, "Extension method `{0}' of value type `{1}' cannot be used to create delegates", bestCandidate.GetSignatureForError(), type.GetSignatureForError());
				}
			}
			TypeSpec typeSpec = this.method_group.BestCandidateReturnType;
			if (typeSpec.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
			{
				typeSpec = ec.BuiltinTypes.Object;
			}
			if (!Delegate.IsTypeCovariant(ec, typeSpec, invokeMethod.ReturnType))
			{
				Expression return_type = new TypeExpression(bestCandidate.ReturnType, this.loc);
				this.Error_ConversionFailed(ec, bestCandidate, return_type);
			}
			if (this.method_group.IsConditionallyExcluded)
			{
				ec.Report.SymbolRelatedToPreviousError(bestCandidate);
				MethodOrOperator methodOrOperator = bestCandidate.MemberDefinition as MethodOrOperator;
				if (methodOrOperator != null && methodOrOperator.IsPartialDefinition)
				{
					ec.Report.Error(762, this.loc, "Cannot create delegate from partial method declaration `{0}'", bestCandidate.GetSignatureForError());
				}
				else
				{
					ec.Report.Error(1618, this.loc, "Cannot create delegate with `{0}' because it has a Conditional attribute", TypeManager.CSharpSignature(bestCandidate));
				}
			}
			Expression instanceExpression = this.method_group.InstanceExpression;
			if (instanceExpression != null && (instanceExpression.Type.IsGenericParameter || !TypeSpec.IsReferenceType(instanceExpression.Type)))
			{
				this.method_group.InstanceExpression = new BoxedCast(instanceExpression, ec.BuiltinTypes.Object);
			}
			this.eclass = ExprClass.Value;
			return this;
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x00068954 File Offset: 0x00066B54
		public override void Emit(EmitContext ec)
		{
			if (this.conditional_access_receiver)
			{
				ec.ConditionalAccess = new ConditionalAccessContext(this.type, ec.DefineLabel());
			}
			if (this.method_group.InstanceExpression == null)
			{
				ec.EmitNull();
			}
			else
			{
				InstanceEmitter instanceEmitter = new InstanceEmitter(this.method_group.InstanceExpression, false);
				instanceEmitter.Emit(ec, this.method_group.ConditionalAccess);
			}
			MethodSpec bestCandidate = this.method_group.BestCandidate;
			if (!bestCandidate.DeclaringType.IsDelegate && bestCandidate.IsVirtual && !this.method_group.IsBase)
			{
				ec.Emit(OpCodes.Dup);
				ec.Emit(OpCodes.Ldvirtftn, bestCandidate);
			}
			else
			{
				ec.Emit(OpCodes.Ldftn, bestCandidate);
			}
			ec.Emit(OpCodes.Newobj, this.constructor_method);
			if (this.conditional_access_receiver)
			{
				ec.CloseConditionalAccess(null);
			}
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x00068A2E File Offset: 0x00066C2E
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			base.FlowAnalysis(fc);
			this.method_group.FlowAnalysis(fc);
			if (this.conditional_access_receiver)
			{
				fc.ConditionalAccessEnd();
			}
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x00068A54 File Offset: 0x00066C54
		private void Error_ConversionFailed(ResolveContext ec, MethodSpec method, Expression return_type)
		{
			MethodSpec invokeMethod = Delegate.GetInvokeMethod(this.type);
			string text = (this.method_group.InstanceExpression != null) ? Delegate.FullDelegateDesc(method) : TypeManager.GetFullNameSignature(method);
			ec.Report.SymbolRelatedToPreviousError(this.type);
			ec.Report.SymbolRelatedToPreviousError(method);
			if (ec.Module.Compiler.Settings.Version == LanguageVersion.ISO_1)
			{
				ec.Report.Error(410, this.loc, "A method or delegate `{0} {1}' parameters and return type must be same as delegate `{2} {3}' parameters and return type", new string[]
				{
					method.ReturnType.GetSignatureForError(),
					text,
					invokeMethod.ReturnType.GetSignatureForError(),
					Delegate.FullDelegateDesc(invokeMethod)
				});
				return;
			}
			if (return_type == null)
			{
				ec.Report.Error(123, this.loc, "A method or delegate `{0}' parameters do not match delegate `{1}' parameters", text, Delegate.FullDelegateDesc(invokeMethod));
				return;
			}
			ec.Report.Error(407, this.loc, "A method or delegate `{0} {1}' return type does not match delegate `{2} {3}' return type", new string[]
			{
				return_type.GetSignatureForError(),
				text,
				invokeMethod.ReturnType.GetSignatureForError(),
				Delegate.FullDelegateDesc(invokeMethod)
			});
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x00068B74 File Offset: 0x00066D74
		public static bool ImplicitStandardConversionExists(ResolveContext ec, MethodGroupExpr mg, TypeSpec target_type)
		{
			MethodSpec invokeMethod = Delegate.GetInvokeMethod(target_type);
			Arguments arguments = DelegateCreation.CreateDelegateMethodArguments(ec, invokeMethod.Parameters, invokeMethod.Parameters.Types, mg.Location);
			mg = mg.OverloadResolve(ec, ref arguments, null, OverloadResolver.Restrictions.ProbingOnly | OverloadResolver.Restrictions.CovariantDelegate);
			return mg != null && Delegate.IsTypeCovariant(ec, mg.BestCandidateReturnType, invokeMethod.ReturnType);
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x000022F4 File Offset: 0x000004F4
		bool OverloadResolver.IErrorHandler.AmbiguousCandidates(ResolveContext ec, MemberSpec best, MemberSpec ambiguous)
		{
			return false;
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x00068BCA File Offset: 0x00066DCA
		bool OverloadResolver.IErrorHandler.ArgumentMismatch(ResolveContext rc, MemberSpec best, Argument arg, int index)
		{
			this.Error_ConversionFailed(rc, best as MethodSpec, null);
			return true;
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x00068BCA File Offset: 0x00066DCA
		bool OverloadResolver.IErrorHandler.NoArgumentMatch(ResolveContext rc, MemberSpec best)
		{
			this.Error_ConversionFailed(rc, best as MethodSpec, null);
			return true;
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x000022F4 File Offset: 0x000004F4
		bool OverloadResolver.IErrorHandler.TypeInferenceFailed(ResolveContext rc, MemberSpec best)
		{
			return false;
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x00068BDB File Offset: 0x00066DDB
		protected DelegateCreation()
		{
		}

		// Token: 0x0400091F RID: 2335
		private bool conditional_access_receiver;

		// Token: 0x04000920 RID: 2336
		protected MethodSpec constructor_method;

		// Token: 0x04000921 RID: 2337
		protected MethodGroupExpr method_group;

		// Token: 0x04000922 RID: 2338
		[CompilerGenerated]
		private bool <AllowSpecialMethodsInvocation>k__BackingField;
	}
}
