using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x020001E9 RID: 489
	public class ArrayInitializer : Expression
	{
		// Token: 0x06001999 RID: 6553 RVA: 0x0007E380 File Offset: 0x0007C580
		public ArrayInitializer(List<Expression> init, Location loc)
		{
			this.elements = init;
			this.loc = loc;
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x0007E396 File Offset: 0x0007C596
		public ArrayInitializer(int count, Location loc) : this(new List<Expression>(count), loc)
		{
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x0007E3A5 File Offset: 0x0007C5A5
		public ArrayInitializer(Location loc) : this(4, loc)
		{
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x0600199C RID: 6556 RVA: 0x0007E3AF File Offset: 0x0007C5AF
		public int Count
		{
			get
			{
				return this.elements.Count;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x0600199D RID: 6557 RVA: 0x0007E3BC File Offset: 0x0007C5BC
		public List<Expression> Elements
		{
			get
			{
				return this.elements;
			}
		}

		// Token: 0x170005F6 RID: 1526
		public Expression this[int index]
		{
			get
			{
				return this.elements[index];
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x0600199F RID: 6559 RVA: 0x0007E3D2 File Offset: 0x0007C5D2
		// (set) Token: 0x060019A0 RID: 6560 RVA: 0x0007E3DA File Offset: 0x0007C5DA
		public BlockVariable VariableDeclaration
		{
			get
			{
				return this.variable;
			}
			set
			{
				this.variable = value;
			}
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x0007E3E3 File Offset: 0x0007C5E3
		public void Add(Expression expr)
		{
			this.elements.Add(expr);
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x0000225C File Offset: 0x0000045C
		public override bool ContainsEmitWithAwait()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x0006D4AD File Offset: 0x0006B6AD
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			throw new NotSupportedException("ET");
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x0007E3F4 File Offset: 0x0007C5F4
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			ArrayInitializer arrayInitializer = (ArrayInitializer)t;
			arrayInitializer.elements = new List<Expression>(this.elements.Count);
			foreach (Expression expression in this.elements)
			{
				arrayInitializer.elements.Add(expression.Clone(clonectx));
			}
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x0007E470 File Offset: 0x0007C670
		protected override Expression DoResolve(ResolveContext rc)
		{
			FieldBase fieldBase = rc.CurrentMemberDefinition as FieldBase;
			TypeExpression requested_base_type;
			if (fieldBase != null && rc.CurrentAnonymousMethod == null)
			{
				requested_base_type = new TypeExpression(fieldBase.MemberType, fieldBase.Location);
			}
			else
			{
				if (this.variable == null)
				{
					throw new NotImplementedException("Unexpected array initializer context");
				}
				if (this.variable.TypeExpression is VarExpr)
				{
					rc.Report.Error(820, this.loc, "An implicitly typed local variable declarator cannot use an array initializer");
					return EmptyExpression.Null;
				}
				requested_base_type = new TypeExpression(this.variable.Variable.Type, this.variable.Variable.Location);
			}
			return new ArrayCreation(requested_base_type, this).Resolve(rc);
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x0006D4B9 File Offset: 0x0006B6B9
		public override void Emit(EmitContext ec)
		{
			throw new InternalErrorException("Missing Resolve call");
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x0006D4B9 File Offset: 0x0006B6B9
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			throw new InternalErrorException("Missing Resolve call");
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x0007E523 File Offset: 0x0007C723
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040009D0 RID: 2512
		private List<Expression> elements;

		// Token: 0x040009D1 RID: 2513
		private BlockVariable variable;
	}
}
