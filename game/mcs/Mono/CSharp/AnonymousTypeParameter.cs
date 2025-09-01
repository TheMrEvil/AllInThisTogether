using System;

namespace Mono.CSharp
{
	// Token: 0x02000212 RID: 530
	public class AnonymousTypeParameter : ShimExpression
	{
		// Token: 0x06001B1F RID: 6943 RVA: 0x00083D9E File Offset: 0x00081F9E
		public AnonymousTypeParameter(Expression initializer, string name, Location loc) : base(initializer)
		{
			this.Name = name;
			this.loc = loc;
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x00083DB5 File Offset: 0x00081FB5
		public AnonymousTypeParameter(Parameter parameter) : base(new SimpleName(parameter.Name, parameter.Location))
		{
			this.Name = parameter.Name;
			this.loc = parameter.Location;
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x00083DE8 File Offset: 0x00081FE8
		public override bool Equals(object o)
		{
			AnonymousTypeParameter anonymousTypeParameter = o as AnonymousTypeParameter;
			return anonymousTypeParameter != null && this.Name == anonymousTypeParameter.Name;
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x00083E12 File Offset: 0x00082012
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x00083E20 File Offset: 0x00082020
		protected override Expression DoResolve(ResolveContext ec)
		{
			Expression expression = this.expr.Resolve(ec);
			if (expression == null)
			{
				return null;
			}
			if (expression.eclass == ExprClass.MethodGroup)
			{
				this.Error_InvalidInitializer(ec, expression.ExprClassName);
				return null;
			}
			this.type = expression.Type;
			if (this.type.Kind == MemberKind.Void || this.type == InternalType.NullLiteral || this.type == InternalType.AnonymousMethod || this.type.IsPointer)
			{
				this.Error_InvalidInitializer(ec, this.type.GetSignatureForError());
				return null;
			}
			return expression;
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x00083EB1 File Offset: 0x000820B1
		protected virtual void Error_InvalidInitializer(ResolveContext ec, string initializer)
		{
			ec.Report.Error(828, this.loc, "An anonymous type property `{0}' cannot be initialized with `{1}'", this.Name, initializer);
		}

		// Token: 0x04000A18 RID: 2584
		public readonly string Name;
	}
}
