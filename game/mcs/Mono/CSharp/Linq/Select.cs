using System;

namespace Mono.CSharp.Linq
{
	// Token: 0x020002F2 RID: 754
	public class Select : AQueryClause
	{
		// Token: 0x06002424 RID: 9252 RVA: 0x000AD8F8 File Offset: 0x000ABAF8
		public Select(QueryBlock block, Expression expr, Location loc) : base(block, expr, loc)
		{
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x000AD904 File Offset: 0x000ABB04
		public bool IsRequired(Parameter parameter)
		{
			SimpleName simpleName = this.expr as SimpleName;
			return simpleName == null || simpleName.Name != parameter.Name;
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06002426 RID: 9254 RVA: 0x000AD8E8 File Offset: 0x000ABAE8
		protected override string MethodName
		{
			get
			{
				return "Select";
			}
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x000AD933 File Offset: 0x000ABB33
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}
