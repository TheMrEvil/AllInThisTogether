using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Mono.CSharp
{
	// Token: 0x020001E7 RID: 487
	public class Invocation : ExpressionStatement
	{
		// Token: 0x0600196F RID: 6511 RVA: 0x0007D2AF File Offset: 0x0007B4AF
		public Invocation(Expression expr, Arguments arguments)
		{
			this.expr = expr;
			this.arguments = arguments;
			if (expr != null)
			{
				this.loc = expr.Location;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x0007D2D4 File Offset: 0x0007B4D4
		public Arguments Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001971 RID: 6513 RVA: 0x0007D2DC File Offset: 0x0007B4DC
		public Expression Exp
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x0007D2E4 File Offset: 0x0007B4E4
		public MethodGroupExpr MethodGroup
		{
			get
			{
				return this.mg;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001973 RID: 6515 RVA: 0x0007D2EC File Offset: 0x0007B4EC
		public override Location StartLocation
		{
			get
			{
				return this.expr.StartLocation;
			}
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x0007D2FC File Offset: 0x0007B4FC
		public override MethodGroupExpr CanReduceLambda(AnonymousMethodBody body)
		{
			if (this.MethodGroup == null)
			{
				return null;
			}
			MethodSpec bestCandidate = this.MethodGroup.BestCandidate;
			if (bestCandidate == null || (!bestCandidate.IsStatic && !(this.Exp is This)))
			{
				return null;
			}
			int num = (this.arguments == null) ? 0 : this.arguments.Count;
			if (num != body.Parameters.Count)
			{
				return null;
			}
			IParameterData[] fixedParameters = body.Block.Parameters.FixedParameters;
			for (int i = 0; i < num; i++)
			{
				ParameterReference parameterReference = this.arguments[i].Expr as ParameterReference;
				if (parameterReference == null)
				{
					return null;
				}
				if (fixedParameters[i] != parameterReference.Parameter)
				{
					return null;
				}
				if ((fixedParameters[i].ModFlags & Parameter.Modifier.RefOutMask) != (parameterReference.Parameter.ModFlags & Parameter.Modifier.RefOutMask))
				{
					return null;
				}
			}
			if (this.MethodGroup is ExtensionMethodGroupExpr)
			{
				MethodSpec methodSpec = bestCandidate;
				MethodGroupExpr methodGroupExpr = MethodGroupExpr.CreatePredefined(methodSpec, methodSpec.DeclaringType, this.MethodGroup.Location);
				if (bestCandidate.IsGeneric)
				{
					TypeExpression[] array = new TypeExpression[bestCandidate.Arity];
					for (int j = 0; j < array.Length; j++)
					{
						array[j] = new TypeExpression(bestCandidate.TypeArguments[j], this.MethodGroup.Location);
					}
					methodGroupExpr.SetTypeArguments(null, new TypeArguments(array));
				}
				return methodGroupExpr;
			}
			return this.MethodGroup;
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x0007D44C File Offset: 0x0007B64C
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			Invocation invocation = (Invocation)t;
			if (this.arguments != null)
			{
				invocation.arguments = this.arguments.Clone(clonectx);
			}
			invocation.expr = this.expr.Clone(clonectx);
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0007D48C File Offset: 0x0007B68C
		public override bool ContainsEmitWithAwait()
		{
			return (this.arguments != null && this.arguments.ContainsEmitWithAwait()) || this.mg.ContainsEmitWithAwait();
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x0007D4B0 File Offset: 0x0007B6B0
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			Expression expression = this.mg.IsInstance ? this.mg.InstanceExpression.CreateExpressionTree(ec) : new NullLiteral(this.loc);
			Arguments args = Arguments.CreateForExpressionTree(ec, this.arguments, new Expression[]
			{
				expression,
				this.mg.CreateExpressionTree(ec)
			});
			return base.CreateExpressionFactoryCall(ec, "Call", args);
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0007D51C File Offset: 0x0007B71C
		protected override Expression DoResolve(ResolveContext rc)
		{
			if (!rc.HasSet(ResolveContext.Options.ConditionalAccessReceiver) && this.expr.HasConditionalAccess())
			{
				this.conditional_access_receiver = true;
				using (rc.Set(ResolveContext.Options.ConditionalAccessReceiver))
				{
					return this.DoResolveInvocation(rc);
				}
			}
			return this.DoResolveInvocation(rc);
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x0007D588 File Offset: 0x0007B788
		private Expression DoResolveInvocation(ResolveContext ec)
		{
			ATypeNameExpression atypeNameExpression = this.expr as ATypeNameExpression;
			Expression expression;
			if (atypeNameExpression != null)
			{
				expression = atypeNameExpression.LookupNameExpression(ec, Expression.MemberLookupRestrictions.InvocableOnly | Expression.MemberLookupRestrictions.ReadAccess);
				if (expression != null)
				{
					NameOf nameOf = expression as NameOf;
					if (nameOf != null)
					{
						return nameOf.ResolveOverload(ec, this.arguments);
					}
					expression = expression.Resolve(ec);
				}
			}
			else
			{
				expression = this.expr.Resolve(ec);
			}
			if (expression == null)
			{
				return null;
			}
			bool flag = false;
			if (this.arguments != null)
			{
				this.arguments.Resolve(ec, out flag);
			}
			TypeSpec type = expression.Type;
			if (type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
			{
				return this.DoResolveDynamic(ec, expression);
			}
			this.mg = (expression as MethodGroupExpr);
			Expression expression2 = null;
			if (this.mg == null)
			{
				if (type != null && type.IsDelegate)
				{
					expression2 = new DelegateInvocation(expression, this.arguments, this.conditional_access_receiver, this.loc);
					expression2 = expression2.Resolve(ec);
					if (expression2 == null || !flag)
					{
						return expression2;
					}
				}
				else
				{
					if (expression is RuntimeValueExpression)
					{
						ec.Report.Error(10000, this.loc, "Cannot invoke a non-delegate type `{0}'", expression.Type.GetSignatureForError());
						return null;
					}
					if (!(expression is MemberExpr))
					{
						expression.Error_UnexpectedKind(ec, ResolveFlags.MethodGroup, this.loc);
						return null;
					}
					ec.Report.Error(1955, this.loc, "The member `{0}' cannot be used as method or delegate", expression.GetSignatureForError());
					return null;
				}
			}
			if (expression2 == null)
			{
				this.mg = this.DoResolveOverload(ec);
				if (this.mg == null)
				{
					return null;
				}
			}
			if (flag)
			{
				return this.DoResolveDynamic(ec, expression);
			}
			MethodSpec bestCandidate = this.mg.BestCandidate;
			this.type = this.mg.BestCandidateReturnType;
			if (this.conditional_access_receiver)
			{
				this.type = Expression.LiftMemberType(ec, this.type);
			}
			if (this.arguments == null && bestCandidate.DeclaringType.BuiltinType == BuiltinTypeSpec.Type.Object && bestCandidate.Name == Destructor.MetadataName)
			{
				if (this.mg.IsBase)
				{
					ec.Report.Error(250, this.loc, "Do not directly call your base class Finalize method. It is called automatically from your destructor");
				}
				else
				{
					ec.Report.Error(245, this.loc, "Destructors and object.Finalize cannot be called directly. Consider calling IDisposable.Dispose if available");
				}
				return null;
			}
			Invocation.IsSpecialMethodInvocation(ec, bestCandidate, this.loc);
			this.eclass = ExprClass.Value;
			return this;
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x0007D7C0 File Offset: 0x0007B9C0
		protected virtual Expression DoResolveDynamic(ResolveContext ec, Expression memberExpr)
		{
			DynamicMemberBinder dynamicMemberBinder = memberExpr as DynamicMemberBinder;
			Arguments arguments;
			if (dynamicMemberBinder != null)
			{
				arguments = dynamicMemberBinder.Arguments;
				if (this.arguments != null)
				{
					arguments.AddRange(this.arguments);
				}
			}
			else if (this.mg == null)
			{
				if (this.arguments == null)
				{
					arguments = new Arguments(1);
				}
				else
				{
					arguments = this.arguments;
				}
				arguments.Insert(0, new Argument(memberExpr));
				this.expr = null;
			}
			else
			{
				if (this.mg.IsBase)
				{
					ec.Report.Error(1971, this.loc, "The base call to method `{0}' cannot be dynamically dispatched. Consider casting the dynamic arguments or eliminating the base access", this.mg.Name);
					return null;
				}
				if (this.arguments == null)
				{
					arguments = new Arguments(1);
				}
				else
				{
					arguments = this.arguments;
				}
				if (this.expr is MemberAccess)
				{
					Expression instanceExpression = this.mg.InstanceExpression;
					TypeExpr typeExpr = instanceExpression as TypeExpr;
					if (typeExpr != null)
					{
						arguments.Insert(0, new Argument(new TypeOf(typeExpr.Type, this.loc).Resolve(ec), Argument.AType.DynamicTypeName));
					}
					else if (instanceExpression != null)
					{
						Argument.AType type = (instanceExpression is IMemoryLocation && TypeSpec.IsValueType(instanceExpression.Type)) ? Argument.AType.Ref : Argument.AType.None;
						arguments.Insert(0, new Argument(instanceExpression.Resolve(ec), type));
					}
				}
				else if (ec.IsStatic)
				{
					arguments.Insert(0, new Argument(new TypeOf(ec.CurrentType, this.loc).Resolve(ec), Argument.AType.DynamicTypeName));
				}
				else
				{
					arguments.Insert(0, new Argument(new This(this.loc).Resolve(ec)));
				}
			}
			return new DynamicInvocation(this.expr as ATypeNameExpression, arguments, this.loc).Resolve(ec);
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x0007D969 File Offset: 0x0007BB69
		protected virtual MethodGroupExpr DoResolveOverload(ResolveContext ec)
		{
			return this.mg.OverloadResolve(ec, ref this.arguments, null, OverloadResolver.Restrictions.None);
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x0007D97F File Offset: 0x0007BB7F
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			if (this.mg.IsConditionallyExcluded)
			{
				return;
			}
			this.mg.FlowAnalysis(fc);
			if (this.arguments != null)
			{
				this.arguments.FlowAnalysis(fc, null);
			}
			if (this.conditional_access_receiver)
			{
				fc.ConditionalAccessEnd();
			}
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x0007D9BE File Offset: 0x0007BBBE
		public override string GetSignatureForError()
		{
			return this.mg.GetSignatureForError();
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x0007D9CB File Offset: 0x0007BBCB
		public override bool HasConditionalAccess()
		{
			return this.expr.HasConditionalAccess();
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x0007D9D8 File Offset: 0x0007BBD8
		public static bool IsMemberInvocable(MemberSpec member)
		{
			MemberKind kind = member.Kind;
			if (kind == MemberKind.Event)
			{
				return true;
			}
			if (kind != MemberKind.Field && kind != MemberKind.Property)
			{
				return false;
			}
			IInterfaceMemberSpec interfaceMemberSpec = member as IInterfaceMemberSpec;
			return interfaceMemberSpec.MemberType.IsDelegate || interfaceMemberSpec.MemberType.BuiltinType == BuiltinTypeSpec.Type.Dynamic;
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x0007DA24 File Offset: 0x0007BC24
		public static bool IsSpecialMethodInvocation(ResolveContext ec, MethodSpec method, Location loc)
		{
			if (!method.IsReservedMethod)
			{
				return false;
			}
			if (ec.HasSet(ResolveContext.Options.InvokeSpecialName) || ec.CurrentMemberDefinition.IsCompilerGenerated)
			{
				return false;
			}
			ec.Report.SymbolRelatedToPreviousError(method);
			ec.Report.Error(571, loc, "`{0}': cannot explicitly call operator or accessor", method.GetSignatureForError());
			return true;
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x0007DA80 File Offset: 0x0007BC80
		public override void Emit(EmitContext ec)
		{
			if (this.mg.IsConditionallyExcluded)
			{
				return;
			}
			if (this.conditional_access_receiver)
			{
				this.mg.EmitCall(ec, this.arguments, this.type, false);
				return;
			}
			this.mg.EmitCall(ec, this.arguments, false);
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0007DAD0 File Offset: 0x0007BCD0
		public override void EmitStatement(EmitContext ec)
		{
			if (this.mg.IsConditionallyExcluded)
			{
				return;
			}
			if (this.conditional_access_receiver)
			{
				this.mg.EmitCall(ec, this.arguments, this.type, true);
				return;
			}
			this.mg.EmitCall(ec, this.arguments, true);
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x0007DB20 File Offset: 0x0007BD20
		public override Expression MakeExpression(BuilderContext ctx)
		{
			return Invocation.MakeExpression(ctx, this.mg.InstanceExpression, this.mg.BestCandidate, this.arguments);
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x0007DB44 File Offset: 0x0007BD44
		public static Expression MakeExpression(BuilderContext ctx, Expression instance, MethodSpec mi, Arguments args)
		{
			return Expression.Call((instance == null) ? null : instance.MakeExpression(ctx), (MethodInfo)mi.GetMetaInfo(), Arguments.MakeExpression(args, ctx));
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x0007DB6A File Offset: 0x0007BD6A
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040009C9 RID: 2505
		protected Arguments arguments;

		// Token: 0x040009CA RID: 2506
		protected Expression expr;

		// Token: 0x040009CB RID: 2507
		protected MethodGroupExpr mg;

		// Token: 0x040009CC RID: 2508
		private bool conditional_access_receiver;

		// Token: 0x020003B9 RID: 953
		public class Predefined : Invocation
		{
			// Token: 0x06002729 RID: 10025 RVA: 0x000BBB87 File Offset: 0x000B9D87
			public Predefined(MethodGroupExpr expr, Arguments arguments) : base(expr, arguments)
			{
				this.mg = expr;
			}

			// Token: 0x0600272A RID: 10026 RVA: 0x000BBB98 File Offset: 0x000B9D98
			protected override MethodGroupExpr DoResolveOverload(ResolveContext rc)
			{
				if (!rc.IsObsolete)
				{
					MethodSpec bestCandidate = this.mg.BestCandidate;
					ObsoleteAttribute attributeObsolete = bestCandidate.GetAttributeObsolete();
					if (attributeObsolete != null)
					{
						AttributeTester.Report_ObsoleteMessage(attributeObsolete, bestCandidate.GetSignatureForError(), this.loc, rc.Report);
					}
				}
				return this.mg;
			}
		}
	}
}
