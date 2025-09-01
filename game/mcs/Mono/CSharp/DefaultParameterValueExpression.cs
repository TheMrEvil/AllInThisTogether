using System;
using Mono.CSharp.Nullable;

namespace Mono.CSharp
{
	// Token: 0x02000271 RID: 625
	public class DefaultParameterValueExpression : CompositeExpression
	{
		// Token: 0x06001EBC RID: 7868 RVA: 0x00084369 File Offset: 0x00082569
		public DefaultParameterValueExpression(Expression expr) : base(expr)
		{
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x000972F8 File Offset: 0x000954F8
		public void Resolve(ResolveContext rc, Parameter p)
		{
			if (base.Resolve(rc) == null)
			{
				this.expr = ErrorExpression.Instance;
				return;
			}
			Expression child = base.Child;
			if (!(child is Constant) && !(child is DefaultValueExpression) && (!(child is New) || !((New)child).IsGeneratedStructConstructor))
			{
				if (!(child is ErrorExpression))
				{
					rc.Report.Error(1736, base.Location, "The expression being assigned to optional parameter `{0}' must be a constant or default value", p.Name);
				}
				return;
			}
			TypeSpec type = p.Type;
			if (this.type == type)
			{
				return;
			}
			Expression expression = Convert.ImplicitConversionStandard(rc, child, type, base.Location);
			if (expression == null)
			{
				rc.Report.Error(1750, base.Location, "Optional parameter expression of type `{0}' cannot be converted to parameter type `{1}'", this.type.GetSignatureForError(), type.GetSignatureForError());
				this.expr = ErrorExpression.Instance;
				return;
			}
			if (type.IsNullableType && expression is Wrap)
			{
				expression = ((Wrap)expression).Child;
				if (!(expression is Constant))
				{
					rc.Report.Error(1770, base.Location, "The expression being assigned to nullable optional parameter `{0}' must be default value", p.Name);
					return;
				}
			}
			if (!child.IsNull && TypeSpec.IsReferenceType(type) && type.BuiltinType != BuiltinTypeSpec.Type.String)
			{
				rc.Report.Error(1763, base.Location, "Optional parameter `{0}' of type `{1}' can only be initialized with `null'", p.Name, type.GetSignatureForError());
				return;
			}
			this.expr = expression;
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x0009745F File Offset: 0x0009565F
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}
