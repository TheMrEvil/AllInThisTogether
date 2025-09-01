using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x02000211 RID: 529
	public class NewAnonymousType : New
	{
		// Token: 0x06001B17 RID: 6935 RVA: 0x00083992 File Offset: 0x00081B92
		public NewAnonymousType(List<AnonymousTypeParameter> parameters, TypeContainer parent, Location loc) : base(null, null, loc)
		{
			this.parameters = parameters;
			this.parent = parent;
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001B18 RID: 6936 RVA: 0x000839AB File Offset: 0x00081BAB
		public List<AnonymousTypeParameter> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x000839B4 File Offset: 0x00081BB4
		protected override void CloneTo(CloneContext clonectx, Expression target)
		{
			if (this.parameters == null)
			{
				return;
			}
			NewAnonymousType newAnonymousType = (NewAnonymousType)target;
			newAnonymousType.parameters = new List<AnonymousTypeParameter>(this.parameters.Count);
			foreach (AnonymousTypeParameter anonymousTypeParameter in this.parameters)
			{
				newAnonymousType.parameters.Add((AnonymousTypeParameter)anonymousTypeParameter.Clone(clonectx));
			}
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x00083A40 File Offset: 0x00081C40
		private AnonymousTypeClass CreateAnonymousType(ResolveContext ec, IList<AnonymousTypeParameter> parameters)
		{
			AnonymousTypeClass anonymousTypeClass = this.parent.Module.GetAnonymousType(parameters);
			if (anonymousTypeClass != null)
			{
				return anonymousTypeClass;
			}
			anonymousTypeClass = AnonymousTypeClass.Create(this.parent, parameters, this.loc);
			if (anonymousTypeClass == null)
			{
				return null;
			}
			int errors = ec.Report.Errors;
			anonymousTypeClass.CreateContainer();
			anonymousTypeClass.DefineContainer();
			anonymousTypeClass.Define();
			if (ec.Report.Errors - errors == 0)
			{
				this.parent.Module.AddAnonymousType(anonymousTypeClass);
				anonymousTypeClass.PrepareEmit();
			}
			return anonymousTypeClass;
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x00083AC4 File Offset: 0x00081CC4
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			if (this.parameters == null)
			{
				return base.CreateExpressionTree(ec);
			}
			ArrayInitializer arrayInitializer = new ArrayInitializer(this.parameters.Count, this.loc);
			foreach (MemberCore memberCore in this.anonymous_type.Members)
			{
				Property property = memberCore as Property;
				if (property != null)
				{
					arrayInitializer.Add(new TypeOfMethod(MemberCache.GetMember<MethodSpec>(this.type, property.Get.Spec), this.loc));
				}
			}
			ArrayInitializer arrayInitializer2 = new ArrayInitializer(this.arguments.Count, this.loc);
			foreach (Argument argument in this.arguments)
			{
				arrayInitializer2.Add(argument.CreateExpressionTree(ec));
			}
			Arguments arguments = new Arguments(3);
			arguments.Add(new Argument(new TypeOfMethod(this.method, this.loc)));
			arguments.Add(new Argument(new ArrayCreation(Expression.CreateExpressionTypeExpression(ec, this.loc), arrayInitializer2, this.loc)));
			arguments.Add(new Argument(new ImplicitlyTypedArrayCreation(arrayInitializer, this.loc)));
			return base.CreateExpressionFactoryCall(ec, "New", arguments);
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x00083C3C File Offset: 0x00081E3C
		protected override Expression DoResolve(ResolveContext ec)
		{
			if (ec.HasSet(ResolveContext.Options.ConstantScope))
			{
				ec.Report.Error(836, this.loc, "Anonymous types cannot be used in this expression");
				return null;
			}
			if (this.parameters == null)
			{
				this.anonymous_type = this.CreateAnonymousType(ec, NewAnonymousType.EmptyParameters);
				this.RequestedType = new TypeExpression(this.anonymous_type.Definition, this.loc);
				return base.DoResolve(ec);
			}
			bool flag = false;
			this.arguments = new Arguments(this.parameters.Count);
			TypeSpec[] array = new TypeSpec[this.parameters.Count];
			for (int i = 0; i < this.parameters.Count; i++)
			{
				Expression expression = this.parameters[i].Resolve(ec);
				if (expression == null)
				{
					flag = true;
				}
				else
				{
					this.arguments.Add(new Argument(expression));
					array[i] = expression.Type;
				}
			}
			if (flag)
			{
				return null;
			}
			this.anonymous_type = this.CreateAnonymousType(ec, this.parameters);
			if (this.anonymous_type == null)
			{
				return null;
			}
			this.type = this.anonymous_type.Definition.MakeGenericType(ec.Module, array);
			this.method = (MethodSpec)MemberCache.FindMember(this.type, MemberFilter.Constructor(null), BindingRestriction.DeclaredOnly);
			this.eclass = ExprClass.Value;
			return this;
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x00083D88 File Offset: 0x00081F88
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x00083D91 File Offset: 0x00081F91
		// Note: this type is marked as 'beforefieldinit'.
		static NewAnonymousType()
		{
		}

		// Token: 0x04000A14 RID: 2580
		private static readonly AnonymousTypeParameter[] EmptyParameters = new AnonymousTypeParameter[0];

		// Token: 0x04000A15 RID: 2581
		private List<AnonymousTypeParameter> parameters;

		// Token: 0x04000A16 RID: 2582
		private readonly TypeContainer parent;

		// Token: 0x04000A17 RID: 2583
		private AnonymousTypeClass anonymous_type;
	}
}
