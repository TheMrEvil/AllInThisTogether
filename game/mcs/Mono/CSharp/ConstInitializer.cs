using System;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x0200013F RID: 319
	public class ConstInitializer : ShimExpression
	{
		// Token: 0x06000FED RID: 4077 RVA: 0x00041799 File Offset: 0x0003F999
		public ConstInitializer(FieldBase field, Expression value, Location loc) : base(value)
		{
			this.loc = loc;
			this.field = field;
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x000417B0 File Offset: 0x0003F9B0
		// (set) Token: 0x06000FEF RID: 4079 RVA: 0x000417B8 File Offset: 0x0003F9B8
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x000417C4 File Offset: 0x0003F9C4
		protected override Expression DoResolve(ResolveContext unused)
		{
			if (this.type != null)
			{
				return this.expr;
			}
			ResolveContext.Options options = ResolveContext.Options.ConstantScope;
			if (this.field is EnumMember)
			{
				options |= ResolveContext.Options.EnumScope;
			}
			ResolveContext rc = new ResolveContext(this.field, options);
			this.expr = this.DoResolveInitializer(rc);
			this.type = this.expr.Type;
			return this.expr;
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0004182C File Offset: 0x0003FA2C
		protected virtual Expression DoResolveInitializer(ResolveContext rc)
		{
			if (this.in_transit)
			{
				this.field.Compiler.Report.Error(110, this.expr.Location, "The evaluation of the constant value for `{0}' involves a circular definition", this.GetSignatureForError());
				this.expr = null;
			}
			else
			{
				this.in_transit = true;
				this.expr = this.expr.Resolve(rc);
			}
			this.in_transit = false;
			if (this.expr != null)
			{
				Constant constant = this.expr as Constant;
				if (constant != null)
				{
					constant = this.field.ConvertInitializer(rc, constant);
				}
				if (constant == null)
				{
					if (TypeSpec.IsReferenceType(this.field.MemberType))
					{
						base.Error_ConstantCanBeInitializedWithNullOnly(rc, this.field.MemberType, this.expr.Location, this.GetSignatureForError());
					}
					else if (!(this.expr is Constant))
					{
						base.Error_ExpressionMustBeConstant(rc, this.expr.Location, this.GetSignatureForError());
					}
					else
					{
						this.expr.Error_ValueCannotBeConverted(rc, this.field.MemberType, false);
					}
				}
				this.expr = constant;
			}
			if (this.expr == null)
			{
				this.expr = New.Constantify(this.field.MemberType, base.Location);
				if (this.expr == null)
				{
					this.expr = Constant.CreateConstantFromValue(this.field.MemberType, null, base.Location);
				}
				this.expr = this.expr.Resolve(rc);
			}
			return this.expr;
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x000419A0 File Offset: 0x0003FBA0
		public override string GetSignatureForError()
		{
			if (this.Name == null)
			{
				return this.field.GetSignatureForError();
			}
			return this.field.Parent.GetSignatureForError() + "." + this.Name;
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x000419D6 File Offset: 0x0003FBD6
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000732 RID: 1842
		private bool in_transit;

		// Token: 0x04000733 RID: 1843
		private readonly FieldBase field;

		// Token: 0x04000734 RID: 1844
		[CompilerGenerated]
		private string <Name>k__BackingField;
	}
}
