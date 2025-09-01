using System;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001B1 RID: 433
	public class ReducedExpression : Expression
	{
		// Token: 0x060016C8 RID: 5832 RVA: 0x0006D28E File Offset: 0x0006B48E
		private ReducedExpression(Expression expr, Expression orig_expr)
		{
			this.expr = expr;
			this.eclass = expr.eclass;
			this.type = expr.Type;
			this.orig_expr = orig_expr;
			this.loc = orig_expr.Location;
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x0006D2C8 File Offset: 0x0006B4C8
		public override bool IsSideEffectFree
		{
			get
			{
				return this.expr.IsSideEffectFree;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x0006D2D5 File Offset: 0x0006B4D5
		public Expression OriginalExpression
		{
			get
			{
				return this.orig_expr;
			}
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x0006D2DD File Offset: 0x0006B4DD
		public override bool ContainsEmitWithAwait()
		{
			return this.expr.ContainsEmitWithAwait();
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x0006D2EA File Offset: 0x0006B4EA
		public static Constant Create(Constant expr, Expression original_expr)
		{
			if (expr.eclass == ExprClass.Unresolved)
			{
				throw new ArgumentException("Unresolved expression");
			}
			return new ReducedExpression.ReducedConstantExpression(expr, original_expr);
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x0006D306 File Offset: 0x0006B506
		public static ExpressionStatement Create(ExpressionStatement s, Expression orig)
		{
			return new ReducedExpression.ReducedExpressionStatement(s, orig);
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x0006D30F File Offset: 0x0006B50F
		public static Expression Create(Expression expr, Expression original_expr)
		{
			return ReducedExpression.Create(expr, original_expr, true);
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x0006D31C File Offset: 0x0006B51C
		public static Expression Create(Expression expr, Expression original_expr, bool canBeConstant)
		{
			if (canBeConstant)
			{
				Constant constant = expr as Constant;
				if (constant != null)
				{
					return ReducedExpression.Create(constant, original_expr);
				}
			}
			ExpressionStatement expressionStatement = expr as ExpressionStatement;
			if (expressionStatement != null)
			{
				return ReducedExpression.Create(expressionStatement, original_expr);
			}
			if (expr.eclass == ExprClass.Unresolved)
			{
				throw new ArgumentException("Unresolved expression");
			}
			return new ReducedExpression(expr, original_expr);
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x0006D36A File Offset: 0x0006B56A
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			return this.orig_expr.CreateExpressionTree(ec);
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x00005936 File Offset: 0x00003B36
		protected override Expression DoResolve(ResolveContext ec)
		{
			return this;
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x0006D378 File Offset: 0x0006B578
		public override void Emit(EmitContext ec)
		{
			this.expr.Emit(ec);
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x0006D386 File Offset: 0x0006B586
		public override Expression EmitToField(EmitContext ec)
		{
			return this.expr.EmitToField(ec);
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x0006D394 File Offset: 0x0006B594
		public override void EmitBranchable(EmitContext ec, Label target, bool on_true)
		{
			this.expr.EmitBranchable(ec, target, on_true);
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x0006D3A4 File Offset: 0x0006B5A4
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			this.expr.FlowAnalysis(fc);
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x0006D3B2 File Offset: 0x0006B5B2
		public override Expression MakeExpression(BuilderContext ctx)
		{
			return this.orig_expr.MakeExpression(ctx);
		}

		// Token: 0x0400095B RID: 2395
		private readonly Expression expr;

		// Token: 0x0400095C RID: 2396
		private readonly Expression orig_expr;

		// Token: 0x020003A5 RID: 933
		public sealed class ReducedConstantExpression : EmptyConstantCast
		{
			// Token: 0x060026E7 RID: 9959 RVA: 0x000BAC26 File Offset: 0x000B8E26
			public ReducedConstantExpression(Constant expr, Expression orig_expr) : base(expr, expr.Type)
			{
				this.orig_expr = orig_expr;
			}

			// Token: 0x170008DC RID: 2268
			// (get) Token: 0x060026E8 RID: 9960 RVA: 0x000BAC3C File Offset: 0x000B8E3C
			public Expression OriginalExpression
			{
				get
				{
					return this.orig_expr;
				}
			}

			// Token: 0x060026E9 RID: 9961 RVA: 0x000BAC44 File Offset: 0x000B8E44
			public override Constant ConvertImplicitly(TypeSpec target_type)
			{
				Constant constant = base.ConvertImplicitly(target_type);
				if (constant != null)
				{
					constant = new ReducedExpression.ReducedConstantExpression(constant, this.orig_expr);
				}
				return constant;
			}

			// Token: 0x060026EA RID: 9962 RVA: 0x000BAC6A File Offset: 0x000B8E6A
			public override Expression CreateExpressionTree(ResolveContext ec)
			{
				return this.orig_expr.CreateExpressionTree(ec);
			}

			// Token: 0x060026EB RID: 9963 RVA: 0x000BAC78 File Offset: 0x000B8E78
			public override Constant ConvertExplicitly(bool in_checked_context, TypeSpec target_type)
			{
				Constant constant = base.ConvertExplicitly(in_checked_context, target_type);
				if (constant != null)
				{
					constant = new ReducedExpression.ReducedConstantExpression(constant, this.orig_expr);
				}
				return constant;
			}

			// Token: 0x060026EC RID: 9964 RVA: 0x000BAC9F File Offset: 0x000B8E9F
			public override void EncodeAttributeValue(IMemberContext rc, AttributeEncoder enc, TypeSpec targetType, TypeSpec parameterType)
			{
				if (this.orig_expr is Conditional)
				{
					this.child.EncodeAttributeValue(rc, enc, targetType, parameterType);
					return;
				}
				base.EncodeAttributeValue(rc, enc, targetType, parameterType);
			}

			// Token: 0x04001053 RID: 4179
			private readonly Expression orig_expr;
		}

		// Token: 0x020003A6 RID: 934
		private sealed class ReducedExpressionStatement : ExpressionStatement
		{
			// Token: 0x060026ED RID: 9965 RVA: 0x000BACCA File Offset: 0x000B8ECA
			public ReducedExpressionStatement(ExpressionStatement stm, Expression orig)
			{
				this.orig_expr = orig;
				this.stm = stm;
				this.eclass = stm.eclass;
				this.type = stm.Type;
				this.loc = orig.Location;
			}

			// Token: 0x060026EE RID: 9966 RVA: 0x000BAD04 File Offset: 0x000B8F04
			public override bool ContainsEmitWithAwait()
			{
				return this.stm.ContainsEmitWithAwait();
			}

			// Token: 0x060026EF RID: 9967 RVA: 0x000BAD11 File Offset: 0x000B8F11
			public override Expression CreateExpressionTree(ResolveContext ec)
			{
				return this.orig_expr.CreateExpressionTree(ec);
			}

			// Token: 0x060026F0 RID: 9968 RVA: 0x00005936 File Offset: 0x00003B36
			protected override Expression DoResolve(ResolveContext ec)
			{
				return this;
			}

			// Token: 0x060026F1 RID: 9969 RVA: 0x000BAD1F File Offset: 0x000B8F1F
			public override void Emit(EmitContext ec)
			{
				this.stm.Emit(ec);
			}

			// Token: 0x060026F2 RID: 9970 RVA: 0x000BAD2D File Offset: 0x000B8F2D
			public override void EmitStatement(EmitContext ec)
			{
				this.stm.EmitStatement(ec);
			}

			// Token: 0x060026F3 RID: 9971 RVA: 0x000BAD3B File Offset: 0x000B8F3B
			public override void FlowAnalysis(FlowAnalysisContext fc)
			{
				this.stm.FlowAnalysis(fc);
			}

			// Token: 0x04001054 RID: 4180
			private readonly Expression orig_expr;

			// Token: 0x04001055 RID: 4181
			private readonly ExpressionStatement stm;
		}
	}
}
