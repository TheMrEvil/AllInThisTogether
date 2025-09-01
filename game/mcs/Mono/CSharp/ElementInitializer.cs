using System;

namespace Mono.CSharp
{
	// Token: 0x0200020C RID: 524
	public class ElementInitializer : Assign
	{
		// Token: 0x06001AEF RID: 6895 RVA: 0x00082BF4 File Offset: 0x00080DF4
		public ElementInitializer(string name, Expression initializer, Location loc) : base(null, initializer, loc)
		{
			this.Name = name;
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x00082C06 File Offset: 0x00080E06
		public bool IsDictionaryInitializer
		{
			get
			{
				return this.Name == null;
			}
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x00082C11 File Offset: 0x00080E11
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			((ElementInitializer)t).source = this.source.Clone(clonectx);
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x00082C2C File Offset: 0x00080E2C
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			Arguments arguments = new Arguments(2);
			FieldExpr fieldExpr = this.target as FieldExpr;
			if (fieldExpr != null)
			{
				arguments.Add(new Argument(fieldExpr.CreateTypeOfExpression()));
			}
			else
			{
				arguments.Add(new Argument(((PropertyExpr)this.target).CreateSetterTypeOfExpression(ec)));
			}
			CollectionOrObjectInitializers collectionOrObjectInitializers = this.source as CollectionOrObjectInitializers;
			string name;
			Expression expr;
			if (collectionOrObjectInitializers == null)
			{
				name = "Bind";
				expr = this.source.CreateExpressionTree(ec);
			}
			else
			{
				name = ((collectionOrObjectInitializers.IsEmpty || collectionOrObjectInitializers.Initializers[0] is ElementInitializer) ? "MemberBind" : "ListBind");
				expr = collectionOrObjectInitializers.CreateExpressionTree(ec, !collectionOrObjectInitializers.IsEmpty);
			}
			arguments.Add(new Argument(expr));
			return base.CreateExpressionFactoryCall(ec, name, arguments);
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x00082CF8 File Offset: 0x00080EF8
		protected override Expression DoResolve(ResolveContext ec)
		{
			if (this.source == null)
			{
				return EmptyExpressionStatement.Instance;
			}
			if (!this.ResolveElement(ec))
			{
				return null;
			}
			if (!(this.source is CollectionOrObjectInitializers))
			{
				return base.DoResolve(ec);
			}
			Expression currentInitializerVariable = ec.CurrentInitializerVariable;
			ec.CurrentInitializerVariable = this.target;
			this.source = this.source.Resolve(ec);
			ec.CurrentInitializerVariable = currentInitializerVariable;
			if (this.source == null)
			{
				return null;
			}
			this.eclass = this.source.eclass;
			this.type = this.source.Type;
			return this;
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x00082D8C File Offset: 0x00080F8C
		public override void EmitStatement(EmitContext ec)
		{
			if (this.source is CollectionOrObjectInitializers)
			{
				this.source.Emit(ec);
				return;
			}
			base.EmitStatement(ec);
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x00082DB0 File Offset: 0x00080FB0
		protected virtual bool ResolveElement(ResolveContext rc)
		{
			TypeSpec type = rc.CurrentInitializerVariable.Type;
			if (type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
			{
				Arguments arguments = new Arguments(1);
				arguments.Add(new Argument(rc.CurrentInitializerVariable));
				this.target = new DynamicMemberBinder(this.Name, arguments, this.loc);
			}
			else
			{
				Expression expression = Expression.MemberLookup(rc, false, type, this.Name, 0, Expression.MemberLookupRestrictions.ExactArity, this.loc);
				if (expression == null)
				{
					expression = Expression.MemberLookup(rc, true, type, this.Name, 0, Expression.MemberLookupRestrictions.ExactArity, this.loc);
					if (expression != null)
					{
						Expression.ErrorIsInaccesible(rc, expression.GetSignatureForError(), this.loc);
						return false;
					}
				}
				if (expression == null)
				{
					Expression.Error_TypeDoesNotContainDefinition(rc, this.loc, type, this.Name);
					return false;
				}
				MemberExpr memberExpr = expression as MemberExpr;
				if (memberExpr is EventExpr)
				{
					memberExpr = memberExpr.ResolveMemberAccess(rc, null, null);
				}
				else if (!(expression is PropertyExpr) && !(expression is FieldExpr))
				{
					rc.Report.Error(1913, this.loc, "Member `{0}' cannot be initialized. An object initializer may only be used for fields, or properties", expression.GetSignatureForError());
					return false;
				}
				if (memberExpr.IsStatic)
				{
					rc.Report.Error(1914, this.loc, "Static field or property `{0}' cannot be assigned in an object initializer", memberExpr.GetSignatureForError());
				}
				this.target = memberExpr;
				memberExpr.InstanceExpression = rc.CurrentInitializerVariable;
			}
			return true;
		}

		// Token: 0x04000A0D RID: 2573
		public readonly string Name;
	}
}
