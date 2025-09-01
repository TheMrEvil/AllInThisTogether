using System;
using System.Collections.Generic;
using System.Text;

namespace Mono.CSharp
{
	// Token: 0x02000214 RID: 532
	public class InterpolatedString : Expression
	{
		// Token: 0x06001B26 RID: 6950 RVA: 0x00083EE5 File Offset: 0x000820E5
		public InterpolatedString(StringLiteral start, List<Expression> interpolations, StringLiteral end)
		{
			this.start = start;
			this.end = end;
			this.interpolations = interpolations;
			this.loc = start.Location;
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x00083F10 File Offset: 0x00082110
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			InterpolatedString interpolatedString = (InterpolatedString)t;
			if (this.interpolations != null)
			{
				interpolatedString.interpolations = new List<Expression>();
				foreach (Expression expression in this.interpolations)
				{
					interpolatedString.interpolations.Add(expression.Clone(clonectx));
				}
			}
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x00083F88 File Offset: 0x00082188
		public Expression ConvertTo(ResolveContext rc, TypeSpec type)
		{
			TypeSpec typeSpec = rc.Module.PredefinedTypes.FormattableStringFactory.Resolve();
			if (typeSpec == null)
			{
				return null;
			}
			Expression expression = new Invocation(new MemberAccess(new TypeExpression(typeSpec, this.loc), "Create", this.loc), this.arguments).Resolve(rc);
			if (expression != null && expression.Type != type)
			{
				expression = Convert.ExplicitConversion(rc, expression, type, this.loc);
			}
			return expression;
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x00083FFC File Offset: 0x000821FC
		public override bool ContainsEmitWithAwait()
		{
			if (this.interpolations == null)
			{
				return false;
			}
			using (List<Expression>.Enumerator enumerator = this.interpolations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.ContainsEmitWithAwait())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x00084060 File Offset: 0x00082260
		public override Expression CreateExpressionTree(ResolveContext rc)
		{
			MethodSpec methodSpec = this.ResolveBestFormatOverload(rc);
			if (methodSpec == null)
			{
				return null;
			}
			Expression expression = new NullLiteral(this.loc);
			Arguments args = Arguments.CreateForExpressionTree(rc, this.arguments, new Expression[]
			{
				expression,
				new TypeOfMethod(methodSpec, this.loc)
			});
			return base.CreateExpressionFactoryCall(rc, "Call", args);
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x000840BC File Offset: 0x000822BC
		protected override Expression DoResolve(ResolveContext rc)
		{
			string s;
			if (this.interpolations == null)
			{
				s = this.start.Value;
				this.arguments = new Arguments(1);
			}
			else
			{
				for (int i = 0; i < this.interpolations.Count; i += 2)
				{
					((InterpolatedStringInsert)this.interpolations[i]).Resolve(rc);
				}
				this.arguments = new Arguments(this.interpolations.Count);
				StringBuilder stringBuilder = new StringBuilder(this.start.Value);
				for (int j = 0; j < this.interpolations.Count; j++)
				{
					if (j % 2 == 0)
					{
						stringBuilder.Append('{').Append(j / 2);
						InterpolatedStringInsert interpolatedStringInsert = (InterpolatedStringInsert)this.interpolations[j];
						if (interpolatedStringInsert.Alignment != null)
						{
							stringBuilder.Append(',');
							int? num = interpolatedStringInsert.ResolveAligment(rc);
							if (num != null)
							{
								stringBuilder.Append(num.Value);
							}
						}
						if (interpolatedStringInsert.Format != null)
						{
							stringBuilder.Append(':');
							stringBuilder.Append(interpolatedStringInsert.Format);
						}
						stringBuilder.Append('}');
						this.arguments.Add(new Argument(this.interpolations[j]));
					}
					else
					{
						stringBuilder.Append(((StringLiteral)this.interpolations[j]).Value);
					}
				}
				stringBuilder.Append(this.end.Value);
				s = stringBuilder.ToString();
			}
			this.arguments.Insert(0, new Argument(new StringLiteral(rc.BuiltinTypes, s, this.start.Location)));
			this.eclass = ExprClass.Value;
			this.type = rc.BuiltinTypes.String;
			return this;
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x0008427C File Offset: 0x0008247C
		public override void Emit(EmitContext ec)
		{
			if (this.interpolations == null)
			{
				string text = this.start.Value.Replace("{{", "{").Replace("}}", "}");
				if (text != this.start.Value)
				{
					new StringConstant(ec.BuiltinTypes, text, this.loc).Emit(ec);
					return;
				}
				this.start.Emit(ec);
				return;
			}
			else
			{
				MethodSpec methodSpec = this.ResolveBestFormatOverload(new ResolveContext(ec.MemberContext));
				if (methodSpec == null)
				{
					return;
				}
				default(CallEmitter).Emit(ec, methodSpec, this.arguments, this.loc);
				return;
			}
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x00084328 File Offset: 0x00082528
		private MethodSpec ResolveBestFormatOverload(ResolveContext rc)
		{
			IList<MemberSpec> members = MemberCache.FindMembers(rc.BuiltinTypes.String, "Format", true);
			OverloadResolver overloadResolver = new OverloadResolver(members, OverloadResolver.Restrictions.NoBaseMembers, this.loc);
			return overloadResolver.ResolveMember<MethodSpec>(rc, ref this.arguments);
		}

		// Token: 0x04000A19 RID: 2585
		private readonly StringLiteral start;

		// Token: 0x04000A1A RID: 2586
		private readonly StringLiteral end;

		// Token: 0x04000A1B RID: 2587
		private List<Expression> interpolations;

		// Token: 0x04000A1C RID: 2588
		private Arguments arguments;
	}
}
