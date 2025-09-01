using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using Mono.CSharp.Nullable;

namespace Mono.CSharp
{
	// Token: 0x020001A4 RID: 420
	public abstract class Expression
	{
		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x0600162A RID: 5674 RVA: 0x0006AD14 File Offset: 0x00068F14
		// (set) Token: 0x0600162B RID: 5675 RVA: 0x0006AD1C File Offset: 0x00068F1C
		public TypeSpec Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x0600162C RID: 5676 RVA: 0x000022F4 File Offset: 0x000004F4
		public virtual bool IsSideEffectFree
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x0600162D RID: 5677 RVA: 0x00032993 File Offset: 0x00030B93
		public Location Location
		{
			get
			{
				return this.loc;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x0600162E RID: 5678 RVA: 0x000022F4 File Offset: 0x000004F4
		public virtual bool IsNull
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x0600162F RID: 5679 RVA: 0x00032993 File Offset: 0x00030B93
		public virtual Location StartLocation
		{
			get
			{
				return this.loc;
			}
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual MethodGroupExpr CanReduceLambda(AnonymousMethodBody body)
		{
			return null;
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x000022F4 File Offset: 0x000004F4
		public virtual bool ContainsEmitWithAwait()
		{
			return false;
		}

		// Token: 0x06001632 RID: 5682
		protected abstract Expression DoResolve(ResolveContext rc);

		// Token: 0x06001633 RID: 5683 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual Expression DoResolveLValue(ResolveContext rc, Expression right_side)
		{
			return null;
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x0006AD28 File Offset: 0x00068F28
		public virtual TypeSpec ResolveAsType(IMemberContext mc, bool allowUnboundTypeArguments = false)
		{
			ResolveContext resolveContext = (mc as ResolveContext) ?? new ResolveContext(mc);
			Expression expression = this.Resolve(resolveContext);
			if (expression != null)
			{
				expression.Error_UnexpectedKind(resolveContext, ResolveFlags.Type, this.loc);
			}
			return null;
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x0006AD60 File Offset: 0x00068F60
		public static void ErrorIsInaccesible(IMemberContext rc, string member, Location loc)
		{
			rc.Module.Compiler.Report.Error(122, loc, "`{0}' is inaccessible due to its protection level", member);
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x0006AD80 File Offset: 0x00068F80
		public void Error_ExpressionMustBeConstant(ResolveContext rc, Location loc, string e_name)
		{
			rc.Report.Error(133, loc, "The expression being assigned to `{0}' must be constant", e_name);
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x0006AD99 File Offset: 0x00068F99
		public void Error_ConstantCanBeInitializedWithNullOnly(ResolveContext rc, TypeSpec type, Location loc, string name)
		{
			rc.Report.Error(134, loc, "A constant `{0}' of reference type `{1}' can only be initialized with null", name, type.GetSignatureForError());
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x0006ADB9 File Offset: 0x00068FB9
		protected virtual void Error_InvalidExpressionStatement(Report report, Location loc)
		{
			report.Error(201, loc, "Only assignment, call, increment, decrement, await, and new object expressions can be used as a statement");
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x0006ADCC File Offset: 0x00068FCC
		public void Error_InvalidExpressionStatement(BlockContext bc)
		{
			this.Error_InvalidExpressionStatement(bc.Report, this.loc);
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x0006ADE0 File Offset: 0x00068FE0
		public void Error_InvalidExpressionStatement(Report report)
		{
			this.Error_InvalidExpressionStatement(report, this.loc);
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x0006ADEF File Offset: 0x00068FEF
		public static void Error_VoidInvalidInTheContext(Location loc, Report Report)
		{
			Report.Error(1547, loc, "Keyword `void' cannot be used in this context");
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x0006AE02 File Offset: 0x00069002
		public virtual void Error_ValueCannotBeConverted(ResolveContext ec, TypeSpec target, bool expl)
		{
			this.Error_ValueCannotBeConvertedCore(ec, this.loc, target, expl);
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x0006AE14 File Offset: 0x00069014
		protected void Error_ValueCannotBeConvertedCore(ResolveContext ec, Location loc, TypeSpec target, bool expl)
		{
			if (this.type == InternalType.AnonymousMethod)
			{
				return;
			}
			if (this.type == InternalType.ErrorType || target == InternalType.ErrorType)
			{
				return;
			}
			string text = this.type.GetSignatureForError();
			string text2 = target.GetSignatureForError();
			if (text == text2)
			{
				text = this.type.GetSignatureForErrorIncludingAssemblyName();
				text2 = target.GetSignatureForErrorIncludingAssemblyName();
			}
			if (expl)
			{
				ec.Report.Error(30, loc, "Cannot convert type `{0}' to `{1}'", text, text2);
				return;
			}
			ec.Report.DisableReporting();
			bool flag = Convert.ExplicitConversion(ec, this, target, Location.Null) != null;
			ec.Report.EnableReporting();
			if (flag)
			{
				ec.Report.Error(266, loc, "Cannot implicitly convert type `{0}' to `{1}'. An explicit conversion exists (are you missing a cast?)", text, text2);
				return;
			}
			ec.Report.Error(29, loc, "Cannot implicitly convert type `{0}' to `{1}'", text, text2);
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x0006AEE4 File Offset: 0x000690E4
		public void Error_TypeArgumentsCannotBeUsed(IMemberContext context, MemberSpec member, Location loc)
		{
			if (member == null || (member.Kind & MemberKind.GenericMask) == (MemberKind)0)
			{
				Expression.Error_TypeArgumentsCannotBeUsed(context, this.ExprClassName, this.GetSignatureForError(), loc);
				return;
			}
			Report report = context.Module.Compiler.Report;
			report.SymbolRelatedToPreviousError(member);
			if (member is TypeSpec)
			{
				member = ((TypeSpec)member).GetDefinition();
			}
			else
			{
				member = ((MethodSpec)member).GetGenericMethodDefinition();
			}
			string text = (member.Kind == MemberKind.Method) ? "method" : "type";
			if (member.IsGeneric)
			{
				report.Error(305, loc, "Using the generic {0} `{1}' requires `{2}' type argument(s)", new string[]
				{
					text,
					member.GetSignatureForError(),
					member.Arity.ToString()
				});
				return;
			}
			report.Error(308, loc, "The non-generic {0} `{1}' cannot be used with the type arguments", text, member.GetSignatureForError());
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x0006AFC4 File Offset: 0x000691C4
		public static void Error_TypeArgumentsCannotBeUsed(IMemberContext context, string exprType, string name, Location loc)
		{
			context.Module.Compiler.Report.Error(307, loc, "The {0} `{1}' cannot be used with type arguments", exprType, name);
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x0006AFE8 File Offset: 0x000691E8
		public virtual void Error_TypeDoesNotContainDefinition(ResolveContext ec, TypeSpec type, string name)
		{
			Expression.Error_TypeDoesNotContainDefinition(ec, this.loc, type, name);
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x0006AFF8 File Offset: 0x000691F8
		public static void Error_TypeDoesNotContainDefinition(ResolveContext ec, Location loc, TypeSpec type, string name)
		{
			ec.Report.SymbolRelatedToPreviousError(type);
			ec.Report.Error(117, loc, "`{0}' does not contain a definition for `{1}'", type.GetSignatureForError(), name);
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x0006B020 File Offset: 0x00069220
		public virtual void Error_ValueAssignment(ResolveContext rc, Expression rhs)
		{
			if (rhs != EmptyExpression.LValueMemberAccess && rhs != EmptyExpression.LValueMemberOutAccess)
			{
				if (rhs == EmptyExpression.OutAccess)
				{
					rc.Report.Error(1510, this.loc, "A ref or out argument must be an assignable variable");
					return;
				}
				rc.Report.Error(131, this.loc, "The left-hand side of an assignment must be a variable, a property or an indexer");
			}
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x0006B07C File Offset: 0x0006927C
		protected void Error_VoidPointerOperation(ResolveContext rc)
		{
			rc.Report.Error(242, this.loc, "The operation in question is undefined on void pointers");
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x0006B099 File Offset: 0x00069299
		public static void Warning_UnreachableExpression(ResolveContext rc, Location loc)
		{
			rc.Report.Warning(429, 4, loc, "Unreachable expression code detected");
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x0006B0B4 File Offset: 0x000692B4
		public ResolveFlags ExprClassToResolveFlags
		{
			get
			{
				switch (this.eclass)
				{
				case ExprClass.Value:
				case ExprClass.Variable:
				case ExprClass.PropertyAccess:
				case ExprClass.EventAccess:
				case ExprClass.IndexerAccess:
					return ResolveFlags.VariableOrValue;
				case ExprClass.Namespace:
				case ExprClass.Type:
					return ResolveFlags.Type;
				case ExprClass.TypeParameter:
					return ResolveFlags.TypeParameter;
				case ExprClass.MethodGroup:
					return ResolveFlags.MethodGroup;
				default:
					throw new InternalErrorException(string.Concat(new object[]
					{
						this.loc.ToString(),
						" ",
						base.GetType(),
						" ExprClass is Invalid after resolve"
					}));
				}
			}
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x0006B13C File Offset: 0x0006933C
		public Expression ProbeIdenticalTypeName(ResolveContext rc, Expression left, SimpleName name)
		{
			TypeSpec typeSpec = left.Type;
			if (typeSpec.Kind == MemberKind.InternalCompilerType || typeSpec is ElementTypeSpec || typeSpec.Arity > 0)
			{
				return left;
			}
			if (left is MemberExpr || left is VariableReference)
			{
				TypeExpr typeExpr = rc.LookupNamespaceOrType(name.Name, 0, LookupMode.Probing, this.loc) as TypeExpr;
				if (typeExpr != null && typeExpr.Type == left.Type)
				{
					return typeExpr;
				}
			}
			return left;
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x0006B1AE File Offset: 0x000693AE
		public virtual string GetSignatureForError()
		{
			return this.type.GetDefinition().GetSignatureForError();
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x0006B1C0 File Offset: 0x000693C0
		public static bool IsNeverNull(Expression expr)
		{
			if (expr is This || expr is New || expr is ArrayCreation || expr is DelegateCreation || expr is ConditionalMemberAccess)
			{
				return true;
			}
			Constant constant = expr as Constant;
			if (constant != null)
			{
				return !constant.IsNull;
			}
			TypeCast typeCast = expr as TypeCast;
			return typeCast != null && Expression.IsNeverNull(typeCast.Child);
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x0006B224 File Offset: 0x00069424
		protected static bool IsNullPropagatingValid(TypeSpec type)
		{
			MemberKind kind = type.Kind;
			if (kind <= MemberKind.TypeParameter)
			{
				if (kind == MemberKind.Struct)
				{
					return type.IsNullableType;
				}
				if (kind != MemberKind.Enum)
				{
					if (kind != MemberKind.TypeParameter)
					{
						return true;
					}
					return !((TypeParameterSpec)type).IsValueType;
				}
			}
			else if (kind != MemberKind.PointerType)
			{
				if (kind == MemberKind.InternalCompilerType)
				{
					return type.BuiltinType == BuiltinTypeSpec.Type.Dynamic;
				}
				if (kind != MemberKind.Void)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x000022F4 File Offset: 0x000004F4
		public virtual bool HasConditionalAccess()
		{
			return false;
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x0006B298 File Offset: 0x00069498
		protected static TypeSpec LiftMemberType(ResolveContext rc, TypeSpec type)
		{
			if (!TypeSpec.IsValueType(type) || type.IsNullableType)
			{
				return type;
			}
			return NullableInfo.MakeType(rc.Module, type);
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x0006B2B8 File Offset: 0x000694B8
		public Expression Resolve(ResolveContext ec, ResolveFlags flags)
		{
			if (this.eclass == ExprClass.Unresolved)
			{
				Expression result;
				try
				{
					Expression expression = this.DoResolve(ec);
					if (expression == null)
					{
						result = null;
					}
					else if ((flags & expression.ExprClassToResolveFlags) == (ResolveFlags)0)
					{
						expression.Error_UnexpectedKind(ec, flags, this.loc);
						result = null;
					}
					else
					{
						if (expression.type == null)
						{
							throw new InternalErrorException("Expression `{0}' didn't set its type in DoResolve", new object[]
							{
								expression.GetType()
							});
						}
						result = expression;
					}
				}
				catch (Exception ex)
				{
					if (this.loc.IsNull || ec.Module.Compiler.Settings.BreakOnInternalError || ex is CompletionResult || ec.Report.IsDisabled || ex is FatalException || ec.Report.Printer is NullReportPrinter)
					{
						throw;
					}
					ec.Report.Error(584, this.loc, "Internal compiler error: {0}", ex.Message);
					result = ErrorExpression.Instance;
				}
				return result;
			}
			if ((flags & this.ExprClassToResolveFlags) == (ResolveFlags)0)
			{
				this.Error_UnexpectedKind(ec, flags, this.loc);
				return null;
			}
			return this;
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x0006B3D4 File Offset: 0x000695D4
		public Expression Resolve(ResolveContext rc)
		{
			return this.Resolve(rc, ResolveFlags.VariableOrValue | ResolveFlags.MethodGroup);
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x0006B3E0 File Offset: 0x000695E0
		public Expression ResolveLValue(ResolveContext ec, Expression right_side)
		{
			int errors = ec.Report.Errors;
			bool flag = right_side == EmptyExpression.OutAccess;
			Expression expression = this.DoResolveLValue(ec, right_side);
			if (expression != null && flag && !(expression is IMemoryLocation))
			{
				expression = null;
			}
			if (expression == null)
			{
				if (errors == ec.Report.Errors)
				{
					this.Error_ValueAssignment(ec, right_side);
				}
				return null;
			}
			if (expression.eclass == ExprClass.Unresolved)
			{
				throw new Exception("Expression " + expression + " ExprClass is Invalid after resolve");
			}
			if (expression.type == null && !(expression is GenericTypeExpr))
			{
				throw new Exception("Expression " + expression + " did not set its type after Resolve");
			}
			return expression;
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x0006B480 File Offset: 0x00069680
		public Constant ResolveLabelConstant(ResolveContext rc)
		{
			Expression expression = this.Resolve(rc);
			if (expression == null)
			{
				return null;
			}
			Constant constant = expression as Constant;
			if (constant == null)
			{
				if (expression.type != InternalType.ErrorType)
				{
					rc.Report.Error(150, expression.StartLocation, "A constant value is expected");
				}
				return null;
			}
			return constant;
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x0006B4D0 File Offset: 0x000696D0
		public virtual void EncodeAttributeValue(IMemberContext rc, AttributeEncoder enc, TypeSpec targetType, TypeSpec parameterType)
		{
			if (Attribute.IsValidArgumentType(parameterType))
			{
				rc.Module.Compiler.Report.Error(182, this.loc, "An attribute argument must be a constant expression, typeof expression or array creation expression");
				return;
			}
			rc.Module.Compiler.Report.Error(181, this.loc, "Attribute constructor parameter has type `{0}', which is not a valid attribute parameter type", targetType.GetSignatureForError());
		}

		// Token: 0x06001651 RID: 5713
		public abstract void Emit(EmitContext ec);

		// Token: 0x06001652 RID: 5714 RVA: 0x0006B537 File Offset: 0x00069737
		public virtual void EmitBranchable(EmitContext ec, Label target, bool on_true)
		{
			this.Emit(ec);
			ec.Emit(on_true ? OpCodes.Brtrue : OpCodes.Brfalse, target);
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x0006B556 File Offset: 0x00069756
		public virtual void EmitSideEffect(EmitContext ec)
		{
			this.Emit(ec);
			ec.Emit(OpCodes.Pop);
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x0006B56C File Offset: 0x0006976C
		public virtual Expression EmitToField(EmitContext ec)
		{
			if (this.IsSideEffectFree)
			{
				return this;
			}
			bool flag = this.ContainsEmitWithAwait();
			if (!flag)
			{
				ec.EmitThis();
			}
			FieldExpr fieldExpr = this.EmitToFieldSource(ec);
			if (fieldExpr == null)
			{
				fieldExpr = ec.GetTemporaryField(this.type, false);
				if (flag)
				{
					LocalBuilder temporaryLocal = ec.GetTemporaryLocal(this.type);
					ec.Emit(OpCodes.Stloc, temporaryLocal);
					ec.EmitThis();
					ec.Emit(OpCodes.Ldloc, temporaryLocal);
					fieldExpr.EmitAssignFromStack(ec);
					ec.FreeTemporaryLocal(temporaryLocal, this.type);
				}
				else
				{
					fieldExpr.EmitAssignFromStack(ec);
				}
			}
			return fieldExpr;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x0006B5F8 File Offset: 0x000697F8
		protected virtual FieldExpr EmitToFieldSource(EmitContext ec)
		{
			this.Emit(ec);
			return null;
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x0006B604 File Offset: 0x00069804
		protected static void EmitExpressionsList(EmitContext ec, List<Expression> expressions)
		{
			if (ec.HasSet(BuilderContext.Options.AsyncBody))
			{
				bool flag = false;
				for (int i = 1; i < expressions.Count; i++)
				{
					if (expressions[i].ContainsEmitWithAwait())
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					for (int j = 0; j < expressions.Count; j++)
					{
						expressions[j] = expressions[j].EmitToField(ec);
					}
				}
			}
			for (int k = 0; k < expressions.Count; k++)
			{
				expressions[k].Emit(ec);
			}
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x00002CCC File Offset: 0x00000ECC
		protected Expression()
		{
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x0006B688 File Offset: 0x00069888
		private static Expression ExprClassFromMemberInfo(MemberSpec spec, Location loc)
		{
			if (spec is EventSpec)
			{
				return new EventExpr((EventSpec)spec, loc);
			}
			if (spec is ConstSpec)
			{
				return new ConstantExpr((ConstSpec)spec, loc);
			}
			if (spec is FieldSpec)
			{
				return new FieldExpr((FieldSpec)spec, loc);
			}
			if (spec is PropertySpec)
			{
				return new PropertyExpr((PropertySpec)spec, loc);
			}
			if (spec is TypeSpec)
			{
				return new TypeExpression((TypeSpec)spec, loc);
			}
			return null;
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x0006B700 File Offset: 0x00069900
		public static MethodSpec ConstructorLookup(ResolveContext rc, TypeSpec type, ref Arguments args, Location loc)
		{
			IList<MemberSpec> list = MemberCache.FindMembers(type, Constructor.ConstructorName, true);
			if (list == null)
			{
				MemberKind kind = type.Kind;
				if (kind != MemberKind.Struct)
				{
					if (kind != MemberKind.InternalCompilerType && kind != MemberKind.MissingType)
					{
						rc.Report.SymbolRelatedToPreviousError(type);
						rc.Report.Error(143, loc, "The class `{0}' has no constructors defined", type.GetSignatureForError());
					}
				}
				else
				{
					if (args == null)
					{
						return null;
					}
					rc.Report.SymbolRelatedToPreviousError(type);
					OverloadResolver.Error_ConstructorMismatch(rc, type, (args == null) ? 0 : args.Count, loc);
				}
				return null;
			}
			if (args == null && type.IsStruct)
			{
				bool flag = false;
				using (IEnumerator<MemberSpec> enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((MethodSpec)enumerator.Current).Parameters.IsEmpty)
						{
							flag = true;
						}
					}
				}
				if (!flag)
				{
					return null;
				}
			}
			OverloadResolver overloadResolver = new OverloadResolver(list, OverloadResolver.Restrictions.NoBaseMembers, loc);
			if (!rc.HasSet(ResolveContext.Options.BaseInitializer))
			{
				overloadResolver.InstanceQualifier = new ConstructorInstanceQualifier(type);
			}
			return overloadResolver.ResolveMember<MethodSpec>(rc, ref args);
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x0006B824 File Offset: 0x00069A24
		public static Expression MemberLookup(IMemberContext rc, bool errorMode, TypeSpec queried_type, string name, int arity, Expression.MemberLookupRestrictions restrictions, Location loc)
		{
			IList<MemberSpec> list = MemberCache.FindMembers(queried_type, name, false);
			if (list == null)
			{
				return null;
			}
			Expression expression;
			for (;;)
			{
				expression = Expression.MemberLookupToExpression(rc, list, errorMode, queried_type, name, arity, restrictions, loc);
				if (expression != null)
				{
					break;
				}
				if (list[0].DeclaringType.BaseType == null)
				{
					list = null;
				}
				else
				{
					list = MemberCache.FindMembers(list[0].DeclaringType.BaseType, name, false);
				}
				if (list == null)
				{
					return expression;
				}
			}
			return expression;
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x0006B88C File Offset: 0x00069A8C
		public static Expression MemberLookupToExpression(IMemberContext rc, IList<MemberSpec> members, bool errorMode, TypeSpec queried_type, string name, int arity, Expression.MemberLookupRestrictions restrictions, Location loc)
		{
			MemberSpec memberSpec = null;
			MemberSpec memberSpec2 = null;
			for (int i = 0; i < members.Count; i++)
			{
				MemberSpec memberSpec3 = members[i];
				if (((memberSpec3.Modifiers & Modifiers.OVERRIDE) == (Modifiers)0 || memberSpec3.Kind == MemberKind.Event) && (memberSpec3.Modifiers & Modifiers.BACKING_FIELD) == (Modifiers)0 && memberSpec3.Kind != MemberKind.Operator && ((arity <= 0 && (restrictions & Expression.MemberLookupRestrictions.ExactArity) == Expression.MemberLookupRestrictions.None) || memberSpec3.Arity == arity) && (errorMode || (memberSpec3.IsAccessible(rc) && (!rc.Module.Compiler.IsRuntimeBinder || memberSpec3.DeclaringType.IsAccessible(rc)))))
				{
					if ((restrictions & Expression.MemberLookupRestrictions.InvocableOnly) != Expression.MemberLookupRestrictions.None)
					{
						if (memberSpec3 is MethodSpec)
						{
							TypeParameterSpec typeParameterSpec = queried_type as TypeParameterSpec;
							if (typeParameterSpec != null && typeParameterSpec.HasTypeConstraint)
							{
								members = Expression.RemoveHiddenTypeParameterMethods(members);
							}
							return new MethodGroupExpr(members, queried_type, loc);
						}
						if (!Invocation.IsMemberInvocable(memberSpec3))
						{
							goto IL_143;
						}
					}
					if (memberSpec == null || memberSpec3 is MethodSpec || memberSpec.IsNotCSharpCompatible)
					{
						memberSpec = memberSpec3;
					}
					else if (!errorMode && !memberSpec3.IsNotCSharpCompatible)
					{
						TypeParameterSpec typeParameterSpec2 = queried_type as TypeParameterSpec;
						if (typeParameterSpec2 != null && typeParameterSpec2.HasTypeConstraint)
						{
							if (memberSpec.DeclaringType.IsClass && memberSpec3.DeclaringType.IsInterface)
							{
								goto IL_143;
							}
							if (memberSpec.DeclaringType.IsInterface && memberSpec3.DeclaringType.IsInterface)
							{
								memberSpec = memberSpec3;
								goto IL_143;
							}
						}
						memberSpec2 = memberSpec3;
					}
				}
				IL_143:;
			}
			if (memberSpec == null)
			{
				return null;
			}
			if (memberSpec2 != null && rc != null && (restrictions & Expression.MemberLookupRestrictions.IgnoreAmbiguity) == Expression.MemberLookupRestrictions.None)
			{
				Report report = rc.Module.Compiler.Report;
				report.SymbolRelatedToPreviousError(memberSpec);
				report.SymbolRelatedToPreviousError(memberSpec2);
				report.Error(229, loc, "Ambiguity between `{0}' and `{1}'", memberSpec.GetSignatureForError(), memberSpec2.GetSignatureForError());
			}
			if (memberSpec is MethodSpec)
			{
				return new MethodGroupExpr(members, queried_type, loc);
			}
			return Expression.ExprClassFromMemberInfo(memberSpec, loc);
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x0006BA54 File Offset: 0x00069C54
		private static IList<MemberSpec> RemoveHiddenTypeParameterMethods(IList<MemberSpec> members)
		{
			if (members.Count < 2)
			{
				return members;
			}
			bool flag = false;
			for (int i = 0; i < members.Count; i++)
			{
				MethodSpec methodSpec = members[i] as MethodSpec;
				if (methodSpec == null)
				{
					if (!flag)
					{
						flag = true;
						members = new List<MemberSpec>(members);
					}
					members.RemoveAt(i--);
				}
				else if (methodSpec.DeclaringType.IsInterface)
				{
					for (int j = 0; j < members.Count; j++)
					{
						MethodSpec methodSpec2 = members[j] as MethodSpec;
						if (methodSpec2 != null && methodSpec2.DeclaringType.IsClass && TypeSpecComparer.Override.IsEqual(methodSpec2.Parameters, methodSpec.Parameters) && AParametersCollection.HasSameParameterDefaults(methodSpec2.Parameters, methodSpec.Parameters))
						{
							if (!flag)
							{
								flag = true;
								members = new List<MemberSpec>(members);
							}
							members.RemoveAt(i--);
							break;
						}
					}
				}
			}
			return members;
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x0006BB33 File Offset: 0x00069D33
		protected static void Error_NamedArgument(NamedArgument na, Report Report)
		{
			Report.Error(1742, na.Location, "An element access expression cannot use named argument");
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x00023DF4 File Offset: 0x00021FF4
		protected virtual void Error_NegativeArrayIndex(ResolveContext ec, Location loc)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x0006BB4B File Offset: 0x00069D4B
		public virtual void Error_OperatorCannotBeApplied(ResolveContext rc, Location loc, string oper, TypeSpec t)
		{
			if (t == InternalType.ErrorType)
			{
				return;
			}
			rc.Report.Error(23, loc, "The `{0}' operator cannot be applied to operand of type `{1}'", oper, t.GetSignatureForError());
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x0006BB72 File Offset: 0x00069D72
		protected void Error_PointerInsideExpressionTree(ResolveContext ec)
		{
			ec.Report.Error(1944, this.loc, "An expression tree cannot contain an unsafe pointer operation");
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x0006BB8F File Offset: 0x00069D8F
		protected void Error_NullShortCircuitInsideExpressionTree(ResolveContext rc)
		{
			rc.Report.Error(8072, this.loc, "An expression tree cannot contain a null propagating operator");
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x0006BBAC File Offset: 0x00069DAC
		protected void Error_NullPropagatingLValue(ResolveContext rc)
		{
			rc.Report.Error(-1030, this.loc, "The left-hand side of an assignment cannot contain a null propagating operator");
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x0000AF70 File Offset: 0x00009170
		public virtual void FlowAnalysis(FlowAnalysisContext fc)
		{
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x0006BBCC File Offset: 0x00069DCC
		public virtual void FlowAnalysisConditional(FlowAnalysisContext fc)
		{
			this.FlowAnalysis(fc);
			fc.DefiniteAssignmentOnTrue = (fc.DefiniteAssignmentOnFalse = fc.DefiniteAssignment);
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x0006BBF5 File Offset: 0x00069DF5
		protected static Expression GetOperatorTrue(ResolveContext ec, Expression e, Location loc)
		{
			return Expression.GetOperatorTrueOrFalse(ec, e, true, loc);
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x0006BC00 File Offset: 0x00069E00
		protected static Expression GetOperatorFalse(ResolveContext ec, Expression e, Location loc)
		{
			return Expression.GetOperatorTrueOrFalse(ec, e, false, loc);
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x0006BC0C File Offset: 0x00069E0C
		private static Expression GetOperatorTrueOrFalse(ResolveContext ec, Expression e, bool is_true, Location loc)
		{
			Operator.OpType op = is_true ? Operator.OpType.True : Operator.OpType.False;
			TypeSpec underlyingType = e.type;
			if (underlyingType.IsNullableType)
			{
				underlyingType = NullableInfo.GetUnderlyingType(underlyingType);
			}
			IList<MemberSpec> userOperator = MemberCache.GetUserOperator(underlyingType, op, false);
			if (userOperator == null)
			{
				return null;
			}
			Arguments arguments = new Arguments(1);
			arguments.Add(new Argument(e));
			OverloadResolver overloadResolver = new OverloadResolver(userOperator, OverloadResolver.Restrictions.NoBaseMembers | OverloadResolver.Restrictions.BaseMembersIncluded, loc);
			MethodSpec methodSpec = overloadResolver.ResolveOperator(ec, ref arguments);
			if (methodSpec == null)
			{
				return null;
			}
			return new UserOperatorCall(methodSpec, arguments, null, loc);
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001668 RID: 5736 RVA: 0x0006BC80 File Offset: 0x00069E80
		public virtual string ExprClassName
		{
			get
			{
				switch (this.eclass)
				{
				case ExprClass.Unresolved:
					return "Unresolved";
				case ExprClass.Value:
					return "value";
				case ExprClass.Variable:
					return "variable";
				case ExprClass.Namespace:
					return "namespace";
				case ExprClass.Type:
					return "type";
				case ExprClass.TypeParameter:
					return "type parameter";
				case ExprClass.MethodGroup:
					return "method group";
				case ExprClass.PropertyAccess:
					return "property access";
				case ExprClass.EventAccess:
					return "event access";
				case ExprClass.IndexerAccess:
					return "indexer access";
				case ExprClass.Nothing:
					return "null";
				default:
					throw new Exception("Should not happen");
				}
			}
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x0006BD14 File Offset: 0x00069F14
		public static void Error_UnexpectedKind(IMemberContext ctx, Expression memberExpr, string expected, string was, Location loc)
		{
			string signatureForError = memberExpr.GetSignatureForError();
			ctx.Module.Compiler.Report.Error(118, loc, "`{0}' is a `{1}' but a `{2}' was expected", new string[]
			{
				signatureForError,
				was,
				expected
			});
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x0006BD58 File Offset: 0x00069F58
		public virtual void Error_UnexpectedKind(ResolveContext ec, ResolveFlags flags, Location loc)
		{
			string[] array = new string[4];
			int num = 0;
			if ((flags & ResolveFlags.VariableOrValue) != (ResolveFlags)0)
			{
				array[num++] = "variable";
				array[num++] = "value";
			}
			if ((flags & ResolveFlags.Type) != (ResolveFlags)0)
			{
				array[num++] = "type";
			}
			if ((flags & ResolveFlags.MethodGroup) != (ResolveFlags)0)
			{
				array[num++] = "method group";
			}
			if (num == 0)
			{
				array[num++] = "unknown";
			}
			StringBuilder stringBuilder = new StringBuilder(array[0]);
			for (int i = 1; i < num - 1; i++)
			{
				stringBuilder.Append("', `");
				stringBuilder.Append(array[i]);
			}
			if (num > 1)
			{
				stringBuilder.Append("' or `");
				stringBuilder.Append(array[num - 1]);
			}
			ec.Report.Error(119, loc, "Expression denotes a `{0}', where a `{1}' was expected", this.ExprClassName, stringBuilder.ToString());
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x0006BE24 File Offset: 0x0006A024
		public static void UnsafeError(ResolveContext ec, Location loc)
		{
			Expression.UnsafeError(ec.Report, loc);
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x0006BE32 File Offset: 0x0006A032
		public static void UnsafeError(Report Report, Location loc)
		{
			Report.Error(214, loc, "Pointers and fixed size buffers may only be used in an unsafe context");
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x0006BE48 File Offset: 0x0006A048
		protected Expression ConvertExpressionToArrayIndex(ResolveContext ec, Expression source, bool pointerArray = false)
		{
			BuiltinTypes builtinTypes = ec.BuiltinTypes;
			if (source.type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
			{
				Arguments arguments = new Arguments(1);
				arguments.Add(new Argument(source));
				return new DynamicConversion(builtinTypes.Int, CSharpBinderFlags.ConvertArrayIndex, arguments, source.loc).Resolve(ec);
			}
			Expression expression;
			using (ec.Set(ResolveContext.Options.CheckedScope))
			{
				expression = Convert.ImplicitConversion(ec, source, builtinTypes.Int, source.loc);
				if (expression == null)
				{
					expression = Convert.ImplicitConversion(ec, source, builtinTypes.UInt, source.loc);
				}
				if (expression == null)
				{
					expression = Convert.ImplicitConversion(ec, source, builtinTypes.Long, source.loc);
				}
				if (expression == null)
				{
					expression = Convert.ImplicitConversion(ec, source, builtinTypes.ULong, source.loc);
				}
				if (expression == null)
				{
					source.Error_ValueCannotBeConverted(ec, builtinTypes.Int, false);
					return null;
				}
			}
			if (pointerArray)
			{
				return expression;
			}
			Constant constant = expression as Constant;
			if (constant != null && constant.IsNegative)
			{
				this.Error_NegativeArrayIndex(ec, source.loc);
			}
			if (expression.Type.BuiltinType == BuiltinTypeSpec.Type.Int)
			{
				return expression;
			}
			return new ArrayIndexCast(expression, builtinTypes.Int).Resolve(ec);
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x0006BF7C File Offset: 0x0006A17C
		public Expression MakePointerAccess(ResolveContext rc, TypeSpec type, Arguments args)
		{
			if (args.Count != 1)
			{
				rc.Report.Error(196, this.loc, "A pointer must be indexed by only one value");
				return null;
			}
			Argument argument = args[0];
			if (argument is NamedArgument)
			{
				Expression.Error_NamedArgument((NamedArgument)argument, rc.Report);
			}
			Expression expression = argument.Expr.Resolve(rc);
			if (expression == null)
			{
				return null;
			}
			expression = this.ConvertExpressionToArrayIndex(rc, expression, true);
			return new Indirection(new PointerArithmetic(Binary.Operator.Addition, this, expression, type, this.loc), this.loc);
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x0006C00A File Offset: 0x0006A20A
		protected virtual void CloneTo(CloneContext clonectx, Expression target)
		{
			throw new NotImplementedException(string.Format("CloneTo not implemented for expression {0}", base.GetType()));
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x0006C024 File Offset: 0x0006A224
		public virtual Expression Clone(CloneContext clonectx)
		{
			Expression expression = (Expression)base.MemberwiseClone();
			this.CloneTo(clonectx, expression);
			return expression;
		}

		// Token: 0x06001671 RID: 5745
		public abstract Expression CreateExpressionTree(ResolveContext ec);

		// Token: 0x06001672 RID: 5746 RVA: 0x0006C046 File Offset: 0x0006A246
		protected Expression CreateExpressionFactoryCall(ResolveContext ec, string name, Arguments args)
		{
			return Expression.CreateExpressionFactoryCall(ec, name, null, args, this.loc);
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x0006C057 File Offset: 0x0006A257
		protected Expression CreateExpressionFactoryCall(ResolveContext ec, string name, TypeArguments typeArguments, Arguments args)
		{
			return Expression.CreateExpressionFactoryCall(ec, name, typeArguments, args, this.loc);
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x0006C069 File Offset: 0x0006A269
		public static Expression CreateExpressionFactoryCall(ResolveContext ec, string name, TypeArguments typeArguments, Arguments args, Location loc)
		{
			return new Invocation(new MemberAccess(Expression.CreateExpressionTypeExpression(ec, loc), name, typeArguments, loc), args);
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x0006C084 File Offset: 0x0006A284
		protected static TypeExpr CreateExpressionTypeExpression(ResolveContext ec, Location loc)
		{
			TypeSpec typeSpec = ec.Module.PredefinedTypes.Expression.Resolve();
			if (typeSpec == null)
			{
				return null;
			}
			return new TypeExpression(typeSpec, loc);
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x0006C0B3 File Offset: 0x0006A2B3
		public virtual Expression MakeExpression(BuilderContext ctx)
		{
			throw new NotImplementedException("MakeExpression for " + base.GetType());
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x00032736 File Offset: 0x00030936
		public virtual object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000950 RID: 2384
		public ExprClass eclass;

		// Token: 0x04000951 RID: 2385
		protected TypeSpec type;

		// Token: 0x04000952 RID: 2386
		protected Location loc;

		// Token: 0x020003A3 RID: 931
		[Flags]
		public enum MemberLookupRestrictions
		{
			// Token: 0x04001003 RID: 4099
			None = 0,
			// Token: 0x04001004 RID: 4100
			InvocableOnly = 1,
			// Token: 0x04001005 RID: 4101
			ExactArity = 4,
			// Token: 0x04001006 RID: 4102
			ReadAccess = 8,
			// Token: 0x04001007 RID: 4103
			EmptyArguments = 16,
			// Token: 0x04001008 RID: 4104
			IgnoreArity = 32,
			// Token: 0x04001009 RID: 4105
			IgnoreAmbiguity = 64,
			// Token: 0x0400100A RID: 4106
			NameOfExcluded = 128
		}
	}
}
