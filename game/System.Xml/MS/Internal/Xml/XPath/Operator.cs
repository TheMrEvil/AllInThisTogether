using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000642 RID: 1602
	internal class Operator : AstNode
	{
		// Token: 0x06004135 RID: 16693 RVA: 0x001668F5 File Offset: 0x00164AF5
		public static Operator.Op InvertOperator(Operator.Op op)
		{
			return Operator.s_invertOp[(int)op];
		}

		// Token: 0x06004136 RID: 16694 RVA: 0x001668FE File Offset: 0x00164AFE
		public Operator(Operator.Op op, AstNode opnd1, AstNode opnd2)
		{
			this._opType = op;
			this._opnd1 = opnd1;
			this._opnd2 = opnd2;
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06004137 RID: 16695 RVA: 0x0001222F File Offset: 0x0001042F
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Operator;
			}
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06004138 RID: 16696 RVA: 0x0016691B File Offset: 0x00164B1B
		public override XPathResultType ReturnType
		{
			get
			{
				if (this._opType <= Operator.Op.GE)
				{
					return XPathResultType.Boolean;
				}
				if (this._opType <= Operator.Op.MOD)
				{
					return XPathResultType.Number;
				}
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06004139 RID: 16697 RVA: 0x00166935 File Offset: 0x00164B35
		public Operator.Op OperatorType
		{
			get
			{
				return this._opType;
			}
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x0600413A RID: 16698 RVA: 0x0016693D File Offset: 0x00164B3D
		public AstNode Operand1
		{
			get
			{
				return this._opnd1;
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x0600413B RID: 16699 RVA: 0x00166945 File Offset: 0x00164B45
		public AstNode Operand2
		{
			get
			{
				return this._opnd2;
			}
		}

		// Token: 0x0600413C RID: 16700 RVA: 0x0016694D File Offset: 0x00164B4D
		// Note: this type is marked as 'beforefieldinit'.
		static Operator()
		{
		}

		// Token: 0x04002E59 RID: 11865
		private static Operator.Op[] s_invertOp = new Operator.Op[]
		{
			Operator.Op.INVALID,
			Operator.Op.INVALID,
			Operator.Op.INVALID,
			Operator.Op.EQ,
			Operator.Op.NE,
			Operator.Op.GT,
			Operator.Op.GE,
			Operator.Op.LT,
			Operator.Op.LE
		};

		// Token: 0x04002E5A RID: 11866
		private Operator.Op _opType;

		// Token: 0x04002E5B RID: 11867
		private AstNode _opnd1;

		// Token: 0x04002E5C RID: 11868
		private AstNode _opnd2;

		// Token: 0x02000643 RID: 1603
		public enum Op
		{
			// Token: 0x04002E5E RID: 11870
			INVALID,
			// Token: 0x04002E5F RID: 11871
			OR,
			// Token: 0x04002E60 RID: 11872
			AND,
			// Token: 0x04002E61 RID: 11873
			EQ,
			// Token: 0x04002E62 RID: 11874
			NE,
			// Token: 0x04002E63 RID: 11875
			LT,
			// Token: 0x04002E64 RID: 11876
			LE,
			// Token: 0x04002E65 RID: 11877
			GT,
			// Token: 0x04002E66 RID: 11878
			GE,
			// Token: 0x04002E67 RID: 11879
			PLUS,
			// Token: 0x04002E68 RID: 11880
			MINUS,
			// Token: 0x04002E69 RID: 11881
			MUL,
			// Token: 0x04002E6A RID: 11882
			DIV,
			// Token: 0x04002E6B RID: 11883
			MOD,
			// Token: 0x04002E6C RID: 11884
			UNION
		}
	}
}
