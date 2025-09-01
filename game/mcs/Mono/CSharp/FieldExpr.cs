using System;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001C2 RID: 450
	public class FieldExpr : MemberExpr, IDynamicAssign, IAssignMethod, IMemoryLocation, IVariableReference, IFixedExpression
	{
		// Token: 0x06001791 RID: 6033 RVA: 0x00071B06 File Offset: 0x0006FD06
		protected FieldExpr(Location l)
		{
			this.loc = l;
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x00071B15 File Offset: 0x0006FD15
		public FieldExpr(FieldSpec spec, Location loc)
		{
			this.spec = spec;
			this.loc = loc;
			this.type = spec.MemberType;
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x00071B37 File Offset: 0x0006FD37
		public FieldExpr(FieldBase fi, Location l) : this(fi.Spec, l)
		{
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x00071B46 File Offset: 0x0006FD46
		public override string Name
		{
			get
			{
				return this.spec.Name;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001795 RID: 6037 RVA: 0x00071B54 File Offset: 0x0006FD54
		public bool IsHoisted
		{
			get
			{
				IVariableReference variableReference = this.InstanceExpression as IVariableReference;
				return variableReference != null && variableReference.IsHoisted;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001796 RID: 6038 RVA: 0x00071B78 File Offset: 0x0006FD78
		public override bool IsInstance
		{
			get
			{
				return !this.spec.IsStatic;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001797 RID: 6039 RVA: 0x00071B88 File Offset: 0x0006FD88
		public override bool IsStatic
		{
			get
			{
				return this.spec.IsStatic;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x00071B95 File Offset: 0x0006FD95
		public override string KindName
		{
			get
			{
				return "field";
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001799 RID: 6041 RVA: 0x00071B9C File Offset: 0x0006FD9C
		public FieldSpec Spec
		{
			get
			{
				return this.spec;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x00071BA4 File Offset: 0x0006FDA4
		protected override TypeSpec DeclaringType
		{
			get
			{
				return this.spec.DeclaringType;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x0600179B RID: 6043 RVA: 0x00071BB1 File Offset: 0x0006FDB1
		public VariableInfo VariableInfo
		{
			get
			{
				return this.variable_info;
			}
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x00071BB9 File Offset: 0x0006FDB9
		public override string GetSignatureForError()
		{
			return this.spec.GetSignatureForError();
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x00071BC8 File Offset: 0x0006FDC8
		public bool IsMarshalByRefAccess(ResolveContext rc)
		{
			return !this.spec.IsStatic && TypeSpec.IsValueType(this.spec.MemberType) && !(this.InstanceExpression is This) && rc.Module.PredefinedTypes.MarshalByRefObject.Define() && TypeSpec.IsBaseClass(this.spec.DeclaringType, rc.Module.PredefinedTypes.MarshalByRefObject.TypeSpec, false);
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x00071C40 File Offset: 0x0006FE40
		public void SetHasAddressTaken()
		{
			IVariableReference variableReference = this.InstanceExpression as IVariableReference;
			if (variableReference != null)
			{
				variableReference.SetHasAddressTaken();
			}
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x00071C64 File Offset: 0x0006FE64
		protected override void CloneTo(CloneContext clonectx, Expression target)
		{
			FieldExpr fieldExpr = (FieldExpr)target;
			if (this.InstanceExpression != null)
			{
				fieldExpr.InstanceExpression = this.InstanceExpression.Clone(clonectx);
			}
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x00071C92 File Offset: 0x0006FE92
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			if (base.ConditionalAccess)
			{
				base.Error_NullShortCircuitInsideExpressionTree(ec);
			}
			return this.CreateExpressionTree(ec, true);
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x00071CAC File Offset: 0x0006FEAC
		public Expression CreateExpressionTree(ResolveContext ec, bool convertInstance)
		{
			Expression expression;
			Arguments arguments;
			if (this.InstanceExpression == null)
			{
				expression = new NullLiteral(this.loc);
			}
			else if (convertInstance)
			{
				expression = this.InstanceExpression.CreateExpressionTree(ec);
			}
			else
			{
				arguments = new Arguments(1);
				arguments.Add(new Argument(this.InstanceExpression));
				expression = base.CreateExpressionFactoryCall(ec, "Constant", arguments);
			}
			arguments = Arguments.CreateForExpressionTree(ec, null, new Expression[]
			{
				expression,
				this.CreateTypeOfExpression()
			});
			return base.CreateExpressionFactoryCall(ec, "Field", arguments);
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x00071D2F File Offset: 0x0006FF2F
		public Expression CreateTypeOfExpression()
		{
			return new TypeOfField(this.spec, this.loc);
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x00071D42 File Offset: 0x0006FF42
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.spec.MemberDefinition.SetIsUsed();
			return this.DoResolve(ec, null);
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x00071D5C File Offset: 0x0006FF5C
		private Expression DoResolve(ResolveContext ec, Expression rhs)
		{
			bool flag = rhs != null && this.IsInstance && this.spec.DeclaringType.IsStruct;
			if (rhs != this)
			{
				base.ResolveConditionalAccessReceiver(ec);
				if (base.ResolveInstanceExpression(ec, rhs))
				{
					if (flag)
					{
						Expression right_side = (rhs == EmptyExpression.OutAccess || rhs == EmptyExpression.LValueMemberOutAccess) ? EmptyExpression.LValueMemberOutAccess : EmptyExpression.LValueMemberAccess;
						this.InstanceExpression = this.InstanceExpression.ResolveLValue(ec, right_side);
					}
					else
					{
						this.InstanceExpression = this.InstanceExpression.Resolve(ec, ResolveFlags.VariableOrValue);
					}
					if (this.InstanceExpression == null)
					{
						return null;
					}
				}
				base.DoBestMemberChecks<FieldSpec>(ec, this.spec);
				if (this.conditional_access_receiver)
				{
					ec.With(ResolveContext.Options.ConditionalAccessReceiver, false);
				}
			}
			FixedFieldSpec fixedFieldSpec = this.spec as FixedFieldSpec;
			IVariableReference variableReference = this.InstanceExpression as IVariableReference;
			if (fixedFieldSpec != null)
			{
				IFixedExpression fixedExpression = this.InstanceExpression as IFixedExpression;
				if (!ec.HasSet(ResolveContext.Options.FixedInitializerScope) && (fixedExpression == null || !fixedExpression.IsFixed))
				{
					ec.Report.Error(1666, this.loc, "You cannot use fixed size buffers contained in unfixed expressions. Try using the fixed statement");
				}
				if (this.InstanceExpression.eclass != ExprClass.Variable)
				{
					ec.Report.SymbolRelatedToPreviousError(this.spec);
					ec.Report.Error(1708, this.loc, "`{0}': Fixed size buffers can only be accessed through locals or fields", TypeManager.GetFullNameSignature(this.spec));
				}
				else if (variableReference != null && variableReference.IsHoisted)
				{
					AnonymousMethodExpression.Error_AddressOfCapturedVar(ec, variableReference, this.loc);
				}
				return new FixedBufferPtr(this, fixedFieldSpec.ElementType, this.loc).Resolve(ec);
			}
			if (variableReference != null && variableReference.VariableInfo != null && this.InstanceExpression.Type.IsStruct)
			{
				this.variable_info = variableReference.VariableInfo.GetStructFieldInfo(this.Name);
			}
			if (base.ConditionalAccess)
			{
				if (this.conditional_access_receiver)
				{
					this.type = Expression.LiftMemberType(ec, this.type);
				}
				if (this.InstanceExpression.IsNull)
				{
					return Constant.CreateConstantFromValue(this.type, null, this.loc);
				}
			}
			this.eclass = ExprClass.Variable;
			return this;
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x00071F70 File Offset: 0x00070170
		public void SetFieldAssigned(FlowAnalysisContext fc)
		{
			if (!this.IsInstance)
			{
				return;
			}
			if (this.spec.DeclaringType.IsStruct)
			{
				IVariableReference variableReference = this.InstanceExpression as IVariableReference;
				if (variableReference != null && variableReference.VariableInfo != null)
				{
					fc.SetStructFieldAssigned(variableReference.VariableInfo, this.Name);
				}
			}
			FieldExpr fieldExpr = this.InstanceExpression as FieldExpr;
			if (fieldExpr != null)
			{
				Expression instanceExpression;
				for (;;)
				{
					instanceExpression = fieldExpr.InstanceExpression;
					FieldExpr fieldExpr2 = instanceExpression as FieldExpr;
					if ((fieldExpr2 == null || fieldExpr2.IsStatic) && !(instanceExpression is LocalVariableReference))
					{
						break;
					}
					if (TypeSpec.IsReferenceType(fieldExpr.Type) && instanceExpression.Type.IsStruct)
					{
						IVariableReference variableReference2 = this.InstanceExpression as IVariableReference;
						if (variableReference2 != null && variableReference2.VariableInfo == null)
						{
							IVariableReference variableReference3 = instanceExpression as IVariableReference;
							if (variableReference3 == null || (variableReference3.VariableInfo != null && !fc.IsDefinitelyAssigned(variableReference3.VariableInfo)))
							{
								fc.Report.Warning(1060, 1, fieldExpr.loc, "Use of possibly unassigned field `{0}'", fieldExpr.Name);
							}
						}
					}
					if (fieldExpr2 == null)
					{
						break;
					}
					fieldExpr = fieldExpr2;
				}
				if (instanceExpression != null && TypeSpec.IsReferenceType(instanceExpression.Type))
				{
					instanceExpression.FlowAnalysis(fc);
					return;
				}
			}
			else if (TypeSpec.IsReferenceType(this.InstanceExpression.Type))
			{
				this.InstanceExpression.FlowAnalysis(fc);
			}
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x000720B4 File Offset: 0x000702B4
		private Expression Error_AssignToReadonly(ResolveContext rc, Expression right_side)
		{
			if (right_side == EmptyExpression.OutAccess)
			{
				if (this.IsStatic)
				{
					rc.Report.Error(199, this.loc, "A static readonly field `{0}' cannot be passed ref or out (except in a static constructor)", this.GetSignatureForError());
				}
				else
				{
					rc.Report.Error(192, this.loc, "A readonly field `{0}' cannot be passed ref or out (except in a constructor)", this.GetSignatureForError());
				}
				return null;
			}
			if (right_side == EmptyExpression.LValueMemberAccess)
			{
				return null;
			}
			if (right_side == EmptyExpression.LValueMemberOutAccess)
			{
				if (this.IsStatic)
				{
					rc.Report.Error(1651, this.loc, "Fields of static readonly field `{0}' cannot be passed ref or out (except in a static constructor)", this.GetSignatureForError());
				}
				else
				{
					rc.Report.Error(1649, this.loc, "Members of readonly field `{0}' cannot be passed ref or out (except in a constructor)", this.GetSignatureForError());
				}
				return null;
			}
			if (this.IsStatic)
			{
				rc.Report.Error(198, this.loc, "A static readonly field `{0}' cannot be assigned to (except in a static constructor or a variable initializer)", this.GetSignatureForError());
			}
			else
			{
				rc.Report.Error(191, this.loc, "A readonly field `{0}' cannot be assigned to (except in a constructor or a variable initializer)", this.GetSignatureForError());
			}
			return null;
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x000721C4 File Offset: 0x000703C4
		public override Expression DoResolveLValue(ResolveContext ec, Expression right_side)
		{
			if (this.HasConditionalAccess())
			{
				base.Error_NullPropagatingLValue(ec);
			}
			if (this.spec is FixedFieldSpec)
			{
				this.Error_ValueAssignment(ec, right_side);
			}
			if (this.DoResolve(ec, right_side) == null)
			{
				return null;
			}
			this.spec.MemberDefinition.SetIsAssigned();
			if ((right_side == EmptyExpression.UnaryAddress || right_side == EmptyExpression.OutAccess) && (this.spec.Modifiers & Modifiers.VOLATILE) != (Modifiers)0)
			{
				ec.Report.Warning(420, 1, this.loc, "`{0}': A volatile field references will not be treated as volatile", this.spec.GetSignatureForError());
			}
			if (this.spec.IsReadOnly)
			{
				if (!ec.HasAny(ResolveContext.Options.FieldInitializerScope | ResolveContext.Options.ConstructorScope))
				{
					return this.Error_AssignToReadonly(ec, right_side);
				}
				if (ec.HasSet(ResolveContext.Options.ConstructorScope))
				{
					if (ec.CurrentMemberDefinition.Parent.PartialContainer.Definition != this.spec.DeclaringType.GetDefinition())
					{
						return this.Error_AssignToReadonly(ec, right_side);
					}
					if (this.IsStatic && !ec.IsStatic)
					{
						return this.Error_AssignToReadonly(ec, right_side);
					}
					if (!this.IsStatic && !(this.InstanceExpression is This))
					{
						return this.Error_AssignToReadonly(ec, right_side);
					}
				}
			}
			if (right_side == EmptyExpression.OutAccess && this.IsMarshalByRefAccess(ec))
			{
				ec.Report.SymbolRelatedToPreviousError(this.spec.DeclaringType);
				ec.Report.Warning(197, 1, this.loc, "Passing `{0}' as ref or out or taking its address may cause a runtime exception because it is a field of a marshal-by-reference class", this.GetSignatureForError());
			}
			this.eclass = ExprClass.Variable;
			return this;
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x00072348 File Offset: 0x00070548
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			IVariableReference variableReference = this.InstanceExpression as IVariableReference;
			if (variableReference != null)
			{
				VariableInfo variableInfo = variableReference.VariableInfo;
				if (variableInfo != null && !fc.IsStructFieldDefinitelyAssigned(variableInfo, this.Name))
				{
					fc.Report.Error(170, this.loc, "Use of possibly unassigned field `{0}'", this.Name);
					return;
				}
				if (TypeSpec.IsValueType(this.InstanceExpression.Type))
				{
					Expression expression = FieldExpr.SkipLeftValueTypeAccess(this.InstanceExpression);
					if (expression != null)
					{
						expression.FlowAnalysis(fc);
					}
					return;
				}
			}
			base.FlowAnalysis(fc);
			if (this.conditional_access_receiver)
			{
				fc.ConditionalAccessEnd();
			}
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x000723E0 File Offset: 0x000705E0
		private static Expression SkipLeftValueTypeAccess(Expression expr)
		{
			if (!TypeSpec.IsValueType(expr.Type))
			{
				return expr;
			}
			if (expr is VariableReference)
			{
				return null;
			}
			FieldExpr fieldExpr = expr as FieldExpr;
			if (fieldExpr == null)
			{
				return expr;
			}
			if (fieldExpr.InstanceExpression == null)
			{
				return expr;
			}
			return FieldExpr.SkipLeftValueTypeAccess(fieldExpr.InstanceExpression);
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x00072427 File Offset: 0x00070627
		public override int GetHashCode()
		{
			return this.spec.GetHashCode();
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060017AB RID: 6059 RVA: 0x00072434 File Offset: 0x00070634
		public bool IsFixed
		{
			get
			{
				IVariableReference variableReference = this.InstanceExpression as IVariableReference;
				if (variableReference != null)
				{
					return this.InstanceExpression.Type.IsStruct && variableReference.IsFixed;
				}
				IFixedExpression fixedExpression = this.InstanceExpression as IFixedExpression;
				return fixedExpression != null && fixedExpression.IsFixed;
			}
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x00072484 File Offset: 0x00070684
		public override bool Equals(object obj)
		{
			FieldExpr fieldExpr = obj as FieldExpr;
			return fieldExpr != null && this.spec == fieldExpr.spec && (this.InstanceExpression == null || fieldExpr.InstanceExpression == null || this.InstanceExpression.Equals(fieldExpr.InstanceExpression));
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x000724D0 File Offset: 0x000706D0
		public void Emit(EmitContext ec, bool leave_copy)
		{
			bool flag = (this.spec.Modifiers & Modifiers.VOLATILE) > (Modifiers)0;
			if (this.IsStatic)
			{
				if (flag)
				{
					ec.Emit(OpCodes.Volatile);
				}
				ec.Emit(OpCodes.Ldsfld, this.spec);
			}
			else
			{
				if (!this.prepared)
				{
					if (this.conditional_access_receiver)
					{
						ec.ConditionalAccess = new ConditionalAccessContext(this.type, ec.DefineLabel());
					}
					base.EmitInstance(ec, false);
				}
				if (this.type.IsStruct && this.type == ec.CurrentType && this.InstanceExpression.Type == this.type)
				{
					ec.EmitLoadFromPtr(this.type);
				}
				else
				{
					FixedFieldSpec fixedFieldSpec = this.spec as FixedFieldSpec;
					if (fixedFieldSpec != null)
					{
						ec.Emit(OpCodes.Ldflda, this.spec);
						ec.Emit(OpCodes.Ldflda, fixedFieldSpec.Element);
					}
					else
					{
						if (flag)
						{
							ec.Emit(OpCodes.Volatile);
						}
						ec.Emit(OpCodes.Ldfld, this.spec);
					}
				}
				if (this.conditional_access_receiver)
				{
					ec.CloseConditionalAccess((this.type.IsNullableType && this.type != this.spec.MemberType) ? this.type : null);
				}
			}
			if (leave_copy)
			{
				ec.Emit(OpCodes.Dup);
				if (!this.IsStatic)
				{
					this.temp = new LocalTemporary(base.Type);
					this.temp.Store(ec);
				}
			}
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x00072648 File Offset: 0x00070848
		public void EmitAssign(EmitContext ec, Expression source, bool leave_copy, bool isCompound)
		{
			bool flag = ec.HasSet(BuilderContext.Options.AsyncBody) && source.ContainsEmitWithAwait();
			if (isCompound && !(source is DynamicExpressionStatement) && !flag)
			{
				this.prepared = true;
			}
			if (this.IsInstance)
			{
				if (base.ConditionalAccess)
				{
					throw new NotImplementedException("null operator assignment");
				}
				if (flag)
				{
					source = source.EmitToField(ec);
				}
				base.EmitInstance(ec, this.prepared);
			}
			source.Emit(ec);
			if (leave_copy || ec.NotifyEvaluatorOnStore)
			{
				ec.Emit(OpCodes.Dup);
				if (!this.IsStatic)
				{
					this.temp = new LocalTemporary(base.Type);
					this.temp.Store(ec);
				}
			}
			if ((this.spec.Modifiers & Modifiers.VOLATILE) != (Modifiers)0)
			{
				ec.Emit(OpCodes.Volatile);
			}
			this.spec.MemberDefinition.SetIsAssigned();
			if (this.IsStatic)
			{
				ec.Emit(OpCodes.Stsfld, this.spec);
			}
			else
			{
				ec.Emit(OpCodes.Stfld, this.spec);
			}
			if (ec.NotifyEvaluatorOnStore)
			{
				if (!this.IsStatic)
				{
					throw new NotImplementedException("instance field write");
				}
				if (leave_copy)
				{
					ec.Emit(OpCodes.Dup);
				}
				ec.Module.Evaluator.EmitValueChangedCallback(ec, this.Name, this.type, this.loc);
			}
			if (this.temp != null)
			{
				this.temp.Emit(ec);
				this.temp.Release(ec);
				this.temp = null;
			}
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x000727C2 File Offset: 0x000709C2
		public void EmitAssignFromStack(EmitContext ec)
		{
			if (this.IsStatic)
			{
				ec.Emit(OpCodes.Stsfld, this.spec);
				return;
			}
			ec.Emit(OpCodes.Stfld, this.spec);
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x000727EF File Offset: 0x000709EF
		public override void Emit(EmitContext ec)
		{
			this.Emit(ec, false);
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x000727F9 File Offset: 0x000709F9
		public override void EmitSideEffect(EmitContext ec)
		{
			if ((this.spec.Modifiers & Modifiers.VOLATILE) > (Modifiers)0)
			{
				base.EmitSideEffect(ec);
			}
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x00072818 File Offset: 0x00070A18
		public virtual void AddressOf(EmitContext ec, AddressOp mode)
		{
			if ((mode & AddressOp.Store) != (AddressOp)0)
			{
				this.spec.MemberDefinition.SetIsAssigned();
			}
			if ((mode & AddressOp.Load) != (AddressOp)0)
			{
				this.spec.MemberDefinition.SetIsUsed();
			}
			bool flag;
			if (this.spec.IsReadOnly)
			{
				flag = true;
				if (ec.HasSet(BuilderContext.Options.ConstructorScope) && this.spec.DeclaringType == ec.CurrentType)
				{
					if (this.IsStatic)
					{
						if (ec.IsStatic)
						{
							flag = false;
						}
					}
					else
					{
						flag = false;
					}
				}
			}
			else
			{
				flag = false;
			}
			if (flag)
			{
				this.Emit(ec);
				LocalBuilder temporaryLocal = ec.GetTemporaryLocal(this.type);
				ec.Emit(OpCodes.Stloc, temporaryLocal);
				ec.Emit(OpCodes.Ldloca, temporaryLocal);
				return;
			}
			if (this.IsStatic)
			{
				ec.Emit(OpCodes.Ldsflda, this.spec);
				return;
			}
			if (!this.prepared)
			{
				base.EmitInstance(ec, false);
			}
			ec.Emit(OpCodes.Ldflda, this.spec);
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x000728FF File Offset: 0x00070AFF
		public Expression MakeAssignExpression(BuilderContext ctx, Expression source)
		{
			return this.MakeExpression(ctx);
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x00072908 File Offset: 0x00070B08
		public override Expression MakeExpression(BuilderContext ctx)
		{
			return Expression.Field(this.IsStatic ? null : this.InstanceExpression.MakeExpression(ctx), this.spec.GetMetaInfo());
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x00072931 File Offset: 0x00070B31
		public override void SetTypeArguments(ResolveContext ec, TypeArguments ta)
		{
			Expression.Error_TypeArgumentsCannotBeUsed(ec, "field", this.GetSignatureForError(), this.loc);
		}

		// Token: 0x0400097F RID: 2431
		protected FieldSpec spec;

		// Token: 0x04000980 RID: 2432
		private VariableInfo variable_info;

		// Token: 0x04000981 RID: 2433
		private LocalTemporary temp;

		// Token: 0x04000982 RID: 2434
		private bool prepared;
	}
}
