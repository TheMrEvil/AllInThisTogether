using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Mono.CSharp
{
	// Token: 0x020001DE RID: 478
	public class StringConcat : Expression
	{
		// Token: 0x0600190C RID: 6412 RVA: 0x0007BB51 File Offset: 0x00079D51
		private StringConcat(Location loc)
		{
			this.loc = loc;
			this.arguments = new Arguments(2);
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x0007BB6C File Offset: 0x00079D6C
		public override bool ContainsEmitWithAwait()
		{
			return this.arguments.ContainsEmitWithAwait();
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x0007BB7C File Offset: 0x00079D7C
		public static StringConcat Create(ResolveContext rc, Expression left, Expression right, Location loc)
		{
			if (left.eclass == ExprClass.Unresolved || right.eclass == ExprClass.Unresolved)
			{
				throw new ArgumentException();
			}
			StringConcat stringConcat = new StringConcat(loc);
			stringConcat.type = rc.BuiltinTypes.String;
			stringConcat.eclass = ExprClass.Value;
			stringConcat.Append(rc, left);
			stringConcat.Append(rc, right);
			return stringConcat;
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x0007BBD0 File Offset: 0x00079DD0
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			Argument argument = this.arguments[0];
			Argument argument2 = argument;
			return this.CreateExpressionAddCall(ec, argument2, argument2.CreateExpressionTree(ec), 1);
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x0007BBFC File Offset: 0x00079DFC
		private Expression CreateExpressionAddCall(ResolveContext ec, Argument left, Expression left_etree, int pos)
		{
			Arguments arguments = new Arguments(2);
			Arguments arguments2 = new Arguments(3);
			arguments.Add(left);
			arguments2.Add(new Argument(left_etree));
			arguments.Add(this.arguments[pos]);
			arguments2.Add(new Argument(this.arguments[pos].CreateExpressionTree(ec)));
			IList<MemberSpec> concatMethodCandidates = this.GetConcatMethodCandidates();
			if (concatMethodCandidates == null)
			{
				return null;
			}
			OverloadResolver overloadResolver = new OverloadResolver(concatMethodCandidates, OverloadResolver.Restrictions.NoBaseMembers, this.loc);
			MethodSpec methodSpec = overloadResolver.ResolveMember<MethodSpec>(ec, ref arguments);
			if (methodSpec == null)
			{
				return null;
			}
			arguments2.Add(new Argument(new TypeOfMethod(methodSpec, this.loc)));
			Expression expression = base.CreateExpressionFactoryCall(ec, "Add", arguments2);
			if (++pos == this.arguments.Count)
			{
				return expression;
			}
			left = new Argument(new EmptyExpression(methodSpec.ReturnType));
			return this.CreateExpressionAddCall(ec, left, expression, pos);
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x00005936 File Offset: 0x00003B36
		protected override Expression DoResolve(ResolveContext ec)
		{
			return this;
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x0007BCE8 File Offset: 0x00079EE8
		private void Append(ResolveContext rc, Expression operand)
		{
			StringConstant stringConstant = operand as StringConstant;
			if (stringConstant != null)
			{
				if (this.arguments.Count != 0)
				{
					Argument argument = this.arguments[this.arguments.Count - 1];
					StringConstant stringConstant2 = argument.Expr as StringConstant;
					if (stringConstant2 != null)
					{
						argument.Expr = new StringConstant(rc.BuiltinTypes, stringConstant2.Value + stringConstant.Value, stringConstant.Location);
						return;
					}
				}
			}
			else
			{
				StringConcat stringConcat = operand as StringConcat;
				if (stringConcat != null)
				{
					this.arguments.AddRange(stringConcat.arguments);
					return;
				}
			}
			this.arguments.Add(new Argument(operand));
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x0007BD8A File Offset: 0x00079F8A
		private IList<MemberSpec> GetConcatMethodCandidates()
		{
			return MemberCache.FindMembers(this.type, "Concat", true);
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x0007BDA0 File Offset: 0x00079FA0
		public override void Emit(EmitContext ec)
		{
			for (int i = 0; i < this.arguments.Count; i++)
			{
				if (this.arguments[i].Expr is NullConstant)
				{
					this.arguments.RemoveAt(i--);
				}
			}
			IList<MemberSpec> concatMethodCandidates = this.GetConcatMethodCandidates();
			OverloadResolver overloadResolver = new OverloadResolver(concatMethodCandidates, OverloadResolver.Restrictions.NoBaseMembers, this.loc);
			MethodSpec methodSpec = overloadResolver.ResolveMember<MethodSpec>(new ResolveContext(ec.MemberContext), ref this.arguments);
			if (methodSpec != null)
			{
				default(CallEmitter).EmitPredefined(ec, methodSpec, this.arguments, false, null);
			}
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x0007BE3F File Offset: 0x0007A03F
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			this.arguments.FlowAnalysis(fc, null);
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x0007BE50 File Offset: 0x0007A050
		public override Expression MakeExpression(BuilderContext ctx)
		{
			if (this.arguments.Count != 2)
			{
				throw new NotImplementedException("arguments.Count != 2");
			}
			MethodInfo method = typeof(string).GetMethod("Concat", new Type[]
			{
				typeof(object),
				typeof(object)
			});
			return Expression.Add(this.arguments[0].Expr.MakeExpression(ctx), this.arguments[1].Expr.MakeExpression(ctx), method);
		}

		// Token: 0x040009BD RID: 2493
		private Arguments arguments;
	}
}
