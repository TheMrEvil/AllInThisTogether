using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x020001D3 RID: 467
	internal class RecursivePattern : ComplexPatternExpression
	{
		// Token: 0x06001888 RID: 6280 RVA: 0x00076B50 File Offset: 0x00074D50
		public RecursivePattern(ATypeNameExpression typeExpresion, Arguments arguments, Location loc) : base(typeExpresion, loc)
		{
			this.Arguments = arguments;
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001889 RID: 6281 RVA: 0x00076B61 File Offset: 0x00074D61
		// (set) Token: 0x0600188A RID: 6282 RVA: 0x00076B69 File Offset: 0x00074D69
		public Arguments Arguments
		{
			[CompilerGenerated]
			get
			{
				return this.<Arguments>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Arguments>k__BackingField = value;
			}
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x00076B74 File Offset: 0x00074D74
		protected override Expression DoResolve(ResolveContext rc)
		{
			this.type = base.TypeExpression.ResolveAsType(rc, false);
			if (this.type == null)
			{
				return null;
			}
			IList<MemberSpec> userOperator = MemberCache.GetUserOperator(this.type, Operator.OpType.Is, true);
			if (userOperator == null)
			{
				this.Error_TypeDoesNotContainDefinition(rc, this.type, Operator.GetName(Operator.OpType.Is) + " operator");
				return null;
			}
			List<MethodSpec> list = this.FindMatchingOverloads(userOperator);
			if (list == null)
			{
				this.Error_TypeDoesNotContainDefinition(rc, this.type, Operator.GetName(Operator.OpType.Is) + " operator");
				return null;
			}
			bool flag;
			this.Arguments.Resolve(rc, out flag);
			if (flag)
			{
				throw new NotImplementedException("dynamic argument");
			}
			MethodSpec methodSpec = this.FindBestOverload(rc, list);
			if (methodSpec == null)
			{
				this.Error_TypeDoesNotContainDefinition(rc, this.type, Operator.GetName(Operator.OpType.Is) + " operator");
				return null;
			}
			TypeSpec[] types = methodSpec.Parameters.Types;
			this.operator_args = new Arguments(types.Length);
			this.operator_args.Add(new Argument(new EmptyExpression(this.type)));
			for (int i = 0; i < this.Arguments.Count; i++)
			{
				LocalTemporary localTemporary = new LocalTemporary(types[i + 1]);
				this.operator_args.Add(new Argument(localTemporary, Argument.AType.Out));
				if (this.comparisons == null)
				{
					this.comparisons = new Expression[this.Arguments.Count];
				}
				Argument argument = this.Arguments[i];
				NamedArgument namedArgument = argument as NamedArgument;
				int num;
				Expression expr;
				if (namedArgument != null)
				{
					num = methodSpec.Parameters.GetParameterIndexByName(namedArgument.Name) - 1;
					expr = this.Arguments[num].Expr;
				}
				else
				{
					num = i;
					expr = argument.Expr;
				}
				this.comparisons[num] = RecursivePattern.ResolveComparison(rc, expr, localTemporary);
			}
			this.operator_mg = MethodGroupExpr.CreatePredefined(methodSpec, this.type, this.loc);
			this.eclass = ExprClass.Value;
			return this;
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x00076D64 File Offset: 0x00074F64
		private List<MethodSpec> FindMatchingOverloads(IList<MemberSpec> members)
		{
			int num = this.Arguments.Count + 1;
			List<MethodSpec> list = null;
			foreach (MemberSpec memberSpec in members)
			{
				MethodSpec methodSpec = (MethodSpec)memberSpec;
				AParametersCollection parameters = methodSpec.Parameters;
				if (parameters.Count == num)
				{
					bool flag = true;
					for (int i = 1; i < parameters.Count; i++)
					{
						if ((parameters.FixedParameters[i].ModFlags & Parameter.Modifier.OUT) == Parameter.Modifier.NONE)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						if (list == null)
						{
							list = new List<MethodSpec>();
						}
						list.Add(methodSpec);
					}
				}
			}
			return list;
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x00076E14 File Offset: 0x00075014
		private MethodSpec FindBestOverload(ResolveContext rc, List<MethodSpec> methods)
		{
			for (int i = 0; i < this.Arguments.Count; i++)
			{
				Argument argument = this.Arguments[i];
				Expression expr = argument.Expr;
				if (!(expr is WildcardPattern))
				{
					NamedArgument namedArgument = argument as NamedArgument;
					int j = 0;
					while (j < methods.Count)
					{
						AParametersCollection parameters = methods[j].Parameters;
						int num;
						if (namedArgument == null)
						{
							num = i + 1;
							goto IL_69;
						}
						num = parameters.GetParameterIndexByName(namedArgument.Name);
						if (num >= 1)
						{
							goto IL_69;
						}
						methods.RemoveAt(j--);
						IL_8D:
						j++;
						continue;
						IL_69:
						TypeSpec target_type = parameters.Types[num];
						if (!Convert.ImplicitConversionExists(rc, expr, target_type))
						{
							methods.RemoveAt(j--);
							goto IL_8D;
						}
						goto IL_8D;
					}
				}
			}
			if (methods.Count != 1)
			{
				return null;
			}
			return methods[0];
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x00076EE5 File Offset: 0x000750E5
		public override void EmitBranchable(EmitContext ec, Label target, bool on_true)
		{
			this.operator_mg.EmitCall(ec, this.operator_args, false);
			ec.Emit(OpCodes.Brfalse, target);
			base.EmitBranchable(ec, target, on_true);
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x00076F10 File Offset: 0x00075110
		private static Expression ResolveComparison(ResolveContext rc, Expression expr, LocalTemporary lt)
		{
			if (expr is WildcardPattern)
			{
				return new EmptyExpression(expr.Type);
			}
			RecursivePattern recursivePattern = expr as RecursivePattern;
			expr = Convert.ImplicitConversionRequired(rc, expr, lt.Type, expr.Location);
			if (expr == null)
			{
				return null;
			}
			if (recursivePattern != null)
			{
				recursivePattern.SetParentInstance(lt);
				return expr;
			}
			Binary.Operator oper = Binary.Operator.Equality;
			Expression expression = expr;
			return new Binary(oper, lt, expression, expression.Location).Resolve(rc);
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x00076F76 File Offset: 0x00075176
		public void SetParentInstance(Expression instance)
		{
			this.operator_args[0] = new Argument(instance);
		}

		// Token: 0x040009A7 RID: 2471
		private MethodGroupExpr operator_mg;

		// Token: 0x040009A8 RID: 2472
		private Arguments operator_args;

		// Token: 0x040009A9 RID: 2473
		[CompilerGenerated]
		private Arguments <Arguments>k__BackingField;
	}
}
