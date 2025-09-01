using System;

namespace Mono.CSharp
{
	// Token: 0x020001C5 RID: 453
	public class EventExpr : MemberExpr, IAssignMethod
	{
		// Token: 0x060017E2 RID: 6114 RVA: 0x000737EB File Offset: 0x000719EB
		public EventExpr(EventSpec spec, Location loc)
		{
			this.spec = spec;
			this.loc = loc;
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060017E3 RID: 6115 RVA: 0x00073801 File Offset: 0x00071A01
		protected override TypeSpec DeclaringType
		{
			get
			{
				return this.spec.DeclaringType;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060017E4 RID: 6116 RVA: 0x0007380E File Offset: 0x00071A0E
		public override string Name
		{
			get
			{
				return this.spec.Name;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060017E5 RID: 6117 RVA: 0x0007381B File Offset: 0x00071A1B
		public override bool IsInstance
		{
			get
			{
				return !this.spec.IsStatic;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x0007382B File Offset: 0x00071A2B
		public override bool IsStatic
		{
			get
			{
				return this.spec.IsStatic;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x00073838 File Offset: 0x00071A38
		public override string KindName
		{
			get
			{
				return "event";
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060017E8 RID: 6120 RVA: 0x0007383F File Offset: 0x00071A3F
		public MethodSpec Operator
		{
			get
			{
				return this.op;
			}
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x00073848 File Offset: 0x00071A48
		public override MemberExpr ResolveMemberAccess(ResolveContext ec, Expression left, SimpleName original)
		{
			if (!ec.HasSet(ResolveContext.Options.CompoundAssignmentScope) && this.spec.BackingField != null && (this.spec.DeclaringType == ec.CurrentType || TypeManager.IsNestedChildOf(ec.CurrentType, this.spec.DeclaringType.MemberDefinition)))
			{
				this.spec.MemberDefinition.SetIsUsed();
				if (!ec.IsObsolete)
				{
					ObsoleteAttribute attributeObsolete = this.spec.GetAttributeObsolete();
					if (attributeObsolete != null)
					{
						AttributeTester.Report_ObsoleteMessage(attributeObsolete, this.spec.GetSignatureForError(), this.loc, ec.Report);
					}
				}
				if ((this.spec.Modifiers & (Modifiers.ABSTRACT | Modifiers.EXTERN)) != (Modifiers)0)
				{
					this.Error_AssignmentEventOnly(ec);
				}
				MemberExpr memberExpr = new FieldExpr(this.spec.BackingField, this.loc);
				this.InstanceExpression = null;
				return memberExpr.ResolveMemberAccess(ec, left, original);
			}
			return base.ResolveMemberAccess(ec, left, original);
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x0006D4AD File Offset: 0x0006B6AD
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			throw new NotSupportedException("ET");
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x00073934 File Offset: 0x00071B34
		public override Expression DoResolveLValue(ResolveContext ec, Expression right_side)
		{
			if (right_side == EmptyExpression.EventAddition)
			{
				this.op = this.spec.AccessorAdd;
			}
			else if (right_side == EmptyExpression.EventSubtraction)
			{
				this.op = this.spec.AccessorRemove;
			}
			if (this.op == null)
			{
				this.Error_AssignmentEventOnly(ec);
				return null;
			}
			if (this.HasConditionalAccess())
			{
				base.Error_NullPropagatingLValue(ec);
			}
			this.op = base.CandidateToBaseOverride(ec, this.op);
			return this;
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x000739AC File Offset: 0x00071BAC
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.eclass = ExprClass.EventAccess;
			this.type = this.spec.MemberType;
			base.ResolveInstanceExpression(ec, null);
			if (!ec.HasSet(ResolveContext.Options.CompoundAssignmentScope))
			{
				this.Error_AssignmentEventOnly(ec);
			}
			base.DoBestMemberChecks<EventSpec>(ec, this.spec);
			return this;
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x0000225C File Offset: 0x0000045C
		public override void Emit(EmitContext ec)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public void Emit(EmitContext ec, bool leave_copy)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x000739FC File Offset: 0x00071BFC
		public void EmitAssign(EmitContext ec, Expression source, bool leave_copy, bool isCompound)
		{
			if (leave_copy || !isCompound)
			{
				throw new NotImplementedException("EventExpr::EmitAssign");
			}
			Arguments arguments = new Arguments(1);
			arguments.Add(new Argument(source));
			CallEmitter callEmitter = default(CallEmitter);
			callEmitter.InstanceExpression = this.InstanceExpression;
			callEmitter.ConditionalAccess = base.ConditionalAccess;
			callEmitter.EmitStatement(ec, this.op, arguments, this.loc);
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x00073A68 File Offset: 0x00071C68
		private void Error_AssignmentEventOnly(ResolveContext ec)
		{
			if (this.spec.DeclaringType == ec.CurrentType || TypeManager.IsNestedChildOf(ec.CurrentType, this.spec.DeclaringType.MemberDefinition))
			{
				ec.Report.Error(79, this.loc, "The event `{0}' can only appear on the left hand side of `+=' or `-=' operator", this.GetSignatureForError());
				return;
			}
			ec.Report.Error(70, this.loc, "The event `{0}' can only appear on the left hand side of += or -= when used outside of the type `{1}'", this.GetSignatureForError(), this.spec.DeclaringType.GetSignatureForError());
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x00073AF2 File Offset: 0x00071CF2
		protected override void Error_CannotCallAbstractBase(ResolveContext rc, string name)
		{
			name = name.Substring(0, name.LastIndexOf('.'));
			base.Error_CannotCallAbstractBase(rc, name);
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x00073B0D File Offset: 0x00071D0D
		public override string GetSignatureForError()
		{
			return TypeManager.CSharpSignature(this.spec);
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x00073B1A File Offset: 0x00071D1A
		public override void SetTypeArguments(ResolveContext ec, TypeArguments ta)
		{
			Expression.Error_TypeArgumentsCannotBeUsed(ec, "event", this.GetSignatureForError(), this.loc);
		}

		// Token: 0x0400098B RID: 2443
		private readonly EventSpec spec;

		// Token: 0x0400098C RID: 2444
		private MethodSpec op;
	}
}
