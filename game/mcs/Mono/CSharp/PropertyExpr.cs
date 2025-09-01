using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001C3 RID: 451
	internal sealed class PropertyExpr : PropertyOrIndexerExpr<PropertySpec>
	{
		// Token: 0x060017B6 RID: 6070 RVA: 0x0007294A File Offset: 0x00070B4A
		public PropertyExpr(PropertySpec spec, Location l) : base(l)
		{
			this.best_candidate = spec;
			this.type = spec.MemberType;
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x00072966 File Offset: 0x00070B66
		// (set) Token: 0x060017B8 RID: 6072 RVA: 0x0007296E File Offset: 0x00070B6E
		protected override Arguments Arguments
		{
			get
			{
				return this.arguments;
			}
			set
			{
				this.arguments = value;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x00072977 File Offset: 0x00070B77
		protected override TypeSpec DeclaringType
		{
			get
			{
				return this.best_candidate.DeclaringType;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060017BA RID: 6074 RVA: 0x00072984 File Offset: 0x00070B84
		public override string Name
		{
			get
			{
				return this.best_candidate.Name;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x00072994 File Offset: 0x00070B94
		public bool IsAutoPropertyAccess
		{
			get
			{
				Property property = this.best_candidate.MemberDefinition as Property;
				return property != null && property.BackingField != null;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x00071A7A File Offset: 0x0006FC7A
		public override bool IsInstance
		{
			get
			{
				return !this.IsStatic;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x000729C0 File Offset: 0x00070BC0
		public override bool IsStatic
		{
			get
			{
				return this.best_candidate.IsStatic;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x000729CD File Offset: 0x00070BCD
		public override string KindName
		{
			get
			{
				return "property";
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x000729D4 File Offset: 0x00070BD4
		public PropertySpec PropertyInfo
		{
			get
			{
				return this.best_candidate;
			}
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x000729DC File Offset: 0x00070BDC
		public override MethodGroupExpr CanReduceLambda(AnonymousMethodBody body)
		{
			if (this.best_candidate == null || (!this.best_candidate.IsStatic && !(this.InstanceExpression is This)))
			{
				return null;
			}
			int num = (this.arguments == null) ? 0 : this.arguments.Count;
			if (num != body.Parameters.Count && num == 0)
			{
				return null;
			}
			MethodGroupExpr methodGroupExpr = MethodGroupExpr.CreatePredefined(this.best_candidate.Get, this.DeclaringType, this.loc);
			methodGroupExpr.InstanceExpression = this.InstanceExpression;
			return methodGroupExpr;
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x00072A5F File Offset: 0x00070C5F
		public static PropertyExpr CreatePredefined(PropertySpec spec, Location loc)
		{
			return new PropertyExpr(spec, loc)
			{
				Getter = spec.Get,
				Setter = spec.Set
			};
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x00072A80 File Offset: 0x00070C80
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			if (base.ConditionalAccess)
			{
				base.Error_NullShortCircuitInsideExpressionTree(ec);
			}
			Arguments arguments;
			if (this.IsSingleDimensionalArrayLength())
			{
				arguments = new Arguments(1);
				arguments.Add(new Argument(this.InstanceExpression.CreateExpressionTree(ec)));
				return base.CreateExpressionFactoryCall(ec, "ArrayLength", arguments);
			}
			arguments = new Arguments(2);
			if (this.InstanceExpression == null)
			{
				arguments.Add(new Argument(new NullLiteral(this.loc)));
			}
			else
			{
				arguments.Add(new Argument(this.InstanceExpression.CreateExpressionTree(ec)));
			}
			arguments.Add(new Argument(new TypeOfMethod(base.Getter, this.loc)));
			return base.CreateExpressionFactoryCall(ec, "Property", arguments);
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x00072B37 File Offset: 0x00070D37
		public Expression CreateSetterTypeOfExpression(ResolveContext rc)
		{
			this.DoResolveLValue(rc, null);
			return new TypeOfMethod(base.Setter, this.loc);
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x00072B53 File Offset: 0x00070D53
		public override string GetSignatureForError()
		{
			return this.best_candidate.GetSignatureForError();
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x00072B60 File Offset: 0x00070D60
		public override Expression MakeAssignExpression(BuilderContext ctx, Expression source)
		{
			return Expression.Property(this.InstanceExpression.MakeExpression(ctx), (MethodInfo)base.Setter.GetMetaInfo());
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x00072B83 File Offset: 0x00070D83
		public override Expression MakeExpression(BuilderContext ctx)
		{
			return Expression.Property(this.InstanceExpression.MakeExpression(ctx), (MethodInfo)base.Getter.GetMetaInfo());
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x00072BA6 File Offset: 0x00070DA6
		private void Error_PropertyNotValid(ResolveContext ec)
		{
			ec.Report.SymbolRelatedToPreviousError(this.best_candidate);
			ec.Report.Error(1546, this.loc, "Property or event `{0}' is not supported by the C# language", this.GetSignatureForError());
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x00072BDC File Offset: 0x00070DDC
		private bool IsSingleDimensionalArrayLength()
		{
			if (this.best_candidate.DeclaringType.BuiltinType != BuiltinTypeSpec.Type.Array || !this.best_candidate.HasGet || this.Name != "Length")
			{
				return false;
			}
			ArrayContainer arrayContainer = this.InstanceExpression.Type as ArrayContainer;
			return arrayContainer != null && arrayContainer.Rank == 1;
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x00072C40 File Offset: 0x00070E40
		public override void Emit(EmitContext ec, bool leave_copy)
		{
			if (this.IsSingleDimensionalArrayLength())
			{
				if (this.conditional_access_receiver)
				{
					ec.ConditionalAccess = new ConditionalAccessContext(this.type, ec.DefineLabel());
				}
				base.EmitInstance(ec, false);
				ec.Emit(OpCodes.Ldlen);
				ec.Emit(OpCodes.Conv_I4);
				if (this.conditional_access_receiver)
				{
					ec.CloseConditionalAccess(this.type);
				}
				return;
			}
			base.Emit(ec, leave_copy);
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x00072CB0 File Offset: 0x00070EB0
		public override void EmitAssign(EmitContext ec, Expression source, bool leave_copy, bool isCompound)
		{
			if (this.backing_field != null)
			{
				this.backing_field.EmitAssign(ec, source, false, false);
				return;
			}
			LocalTemporary localTemporary = null;
			Arguments arguments;
			if (isCompound && !(source is DynamicExpressionStatement))
			{
				this.emitting_compound_assignment = true;
				source.Emit(ec);
				if (this.has_await_arguments)
				{
					localTemporary = new LocalTemporary(base.Type);
					localTemporary.Store(ec);
					arguments = new Arguments(1);
					arguments.Add(new Argument(localTemporary));
					if (leave_copy)
					{
						this.temp = localTemporary;
					}
					this.has_await_arguments = false;
				}
				else
				{
					arguments = null;
					if (leave_copy)
					{
						ec.Emit(OpCodes.Dup);
						this.temp = new LocalTemporary(base.Type);
						this.temp.Store(ec);
					}
				}
			}
			else
			{
				arguments = (this.arguments ?? new Arguments(1));
				if (leave_copy)
				{
					source.Emit(ec);
					this.temp = new LocalTemporary(base.Type);
					this.temp.Store(ec);
					arguments.Add(new Argument(this.temp));
				}
				else
				{
					arguments.Add(new Argument(source));
				}
			}
			this.emitting_compound_assignment = false;
			CallEmitter callEmitter = default(CallEmitter);
			callEmitter.InstanceExpression = this.InstanceExpression;
			if (arguments == null)
			{
				callEmitter.InstanceExpressionOnStack = true;
			}
			if (base.ConditionalAccess)
			{
				callEmitter.ConditionalAccess = true;
			}
			if (leave_copy)
			{
				callEmitter.Emit(ec, base.Setter, arguments, this.loc);
			}
			else
			{
				callEmitter.EmitStatement(ec, base.Setter, arguments, this.loc);
			}
			if (this.temp != null)
			{
				this.temp.Emit(ec);
				this.temp.Release(ec);
			}
			if (localTemporary != null)
			{
				localTemporary.Release(ec);
			}
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x00072E54 File Offset: 0x00071054
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			Property property = this.best_candidate.MemberDefinition as Property;
			if (property != null && property.BackingField != null)
			{
				IVariableReference variableReference = this.InstanceExpression as IVariableReference;
				if (variableReference != null)
				{
					VariableInfo variableInfo = variableReference.VariableInfo;
					if (variableInfo != null && !fc.IsStructFieldDefinitelyAssigned(variableInfo, property.BackingField.Name))
					{
						fc.Report.Error(8079, this.loc, "Use of possibly unassigned auto-implemented property `{0}'", this.Name);
						return;
					}
					if (TypeSpec.IsValueType(this.InstanceExpression.Type) && this.InstanceExpression is VariableReference)
					{
						return;
					}
				}
			}
			base.FlowAnalysis(fc);
			if (this.conditional_access_receiver)
			{
				fc.ConditionalAccessEnd();
			}
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x00072F04 File Offset: 0x00071104
		protected override Expression OverloadResolve(ResolveContext rc, Expression right_side)
		{
			this.eclass = ExprClass.PropertyAccess;
			if (this.best_candidate.IsNotCSharpCompatible)
			{
				this.Error_PropertyNotValid(rc);
			}
			base.ResolveInstanceExpression(rc, right_side);
			if ((this.best_candidate.Modifiers & (Modifiers.ABSTRACT | Modifiers.VIRTUAL)) != (Modifiers)0 && this.best_candidate.DeclaringType != this.InstanceExpression.Type)
			{
				MemberFilter filter = new MemberFilter(this.best_candidate.Name, 0, MemberKind.Property, null, null);
				PropertySpec propertySpec = MemberCache.FindMember(this.InstanceExpression.Type, filter, BindingRestriction.InstanceOnly | BindingRestriction.OverrideOnly) as PropertySpec;
				if (propertySpec != null)
				{
					this.type = propertySpec.MemberType;
				}
			}
			base.DoBestMemberChecks<PropertySpec>(rc, this.best_candidate);
			if (this.best_candidate.HasGet && !this.best_candidate.Get.Parameters.IsEmpty)
			{
				AParametersCollection parameters = this.best_candidate.Get.Parameters;
				this.arguments = new Arguments(parameters.Count);
				for (int i = 0; i < parameters.Count; i++)
				{
					this.arguments.Add(new Argument(OverloadResolver.ResolveDefaultValueArgument(rc, parameters.Types[i], parameters.FixedParameters[i].DefaultValue, this.loc)));
				}
			}
			else if (this.best_candidate.HasSet && this.best_candidate.Set.Parameters.Count > 1)
			{
				AParametersCollection parameters2 = this.best_candidate.Set.Parameters;
				this.arguments = new Arguments(parameters2.Count - 1);
				for (int j = 0; j < parameters2.Count - 1; j++)
				{
					this.arguments.Add(new Argument(OverloadResolver.ResolveDefaultValueArgument(rc, parameters2.Types[j], parameters2.FixedParameters[j].DefaultValue, this.loc)));
				}
			}
			return this;
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x000730D8 File Offset: 0x000712D8
		protected override bool ResolveAutopropertyAssignment(ResolveContext rc, Expression rhs)
		{
			Property property = this.best_candidate.MemberDefinition as Property;
			if (property == null)
			{
				return false;
			}
			if (!rc.HasSet(ResolveContext.Options.ConstructorScope))
			{
				return false;
			}
			if (property.Parent.PartialContainer != rc.CurrentMemberDefinition.Parent.PartialContainer)
			{
				PropertySpec propertySpec = MemberCache.FindMember(rc.CurrentType, MemberFilter.Property(property.ShortName, property.MemberType), BindingRestriction.DeclaredOnly) as PropertySpec;
				if (propertySpec == null)
				{
					return false;
				}
				property = (Property)propertySpec.MemberDefinition;
			}
			Property.BackingFieldDeclaration backingField = property.BackingField;
			if (backingField == null)
			{
				return false;
			}
			if (rc.IsStatic != backingField.IsStatic)
			{
				return false;
			}
			if (!backingField.IsStatic && (!(this.InstanceExpression is This) || this.InstanceExpression is BaseThis))
			{
				return false;
			}
			this.backing_field = new FieldExpr(property.BackingField, this.loc);
			this.backing_field.ResolveLValue(rc, rhs);
			return true;
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x000731C4 File Offset: 0x000713C4
		public void SetBackingFieldAssigned(FlowAnalysisContext fc)
		{
			if (this.backing_field != null)
			{
				this.backing_field.SetFieldAssigned(fc);
				return;
			}
			if (!this.IsAutoPropertyAccess)
			{
				return;
			}
			Property property = this.best_candidate.MemberDefinition as Property;
			if (property != null && property.BackingField != null && this.best_candidate.DeclaringType.IsStruct)
			{
				IVariableReference variableReference = this.InstanceExpression as IVariableReference;
				if (variableReference != null && variableReference.VariableInfo != null)
				{
					fc.SetStructFieldAssigned(variableReference.VariableInfo, property.BackingField.Name);
				}
			}
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x0007324B File Offset: 0x0007144B
		public override void SetTypeArguments(ResolveContext ec, TypeArguments ta)
		{
			Expression.Error_TypeArgumentsCannotBeUsed(ec, "property", this.GetSignatureForError(), this.loc);
		}

		// Token: 0x04000983 RID: 2435
		private Arguments arguments;

		// Token: 0x04000984 RID: 2436
		private FieldExpr backing_field;
	}
}
