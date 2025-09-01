using System;
using System.Linq.Expressions;

namespace Mono.CSharp
{
	// Token: 0x02000236 RID: 566
	public class NullLiteral : NullConstant
	{
		// Token: 0x06001C7D RID: 7293 RVA: 0x0008A7DD File Offset: 0x000889DD
		public NullLiteral(Location loc) : base(InternalType.NullLiteral, loc)
		{
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x0008A7EC File Offset: 0x000889EC
		public override void Error_ValueCannotBeConverted(ResolveContext ec, TypeSpec t, bool expl)
		{
			if (t.IsGenericParameter)
			{
				ec.Report.Error(403, this.loc, "Cannot convert null to the type parameter `{0}' because it could be a value type. Consider using `default ({0})' instead", t.Name);
				return;
			}
			if (TypeSpec.IsValueType(t))
			{
				ec.Report.Error(37, this.loc, "Cannot convert null to `{0}' because it is a value type", t.GetSignatureForError());
				return;
			}
			base.Error_ValueCannotBeConverted(ec, t, expl);
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x00044A9D File Offset: 0x00042C9D
		public override string GetValueAsLiteral()
		{
			return "null";
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsLiteral
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x0008A853 File Offset: 0x00088A53
		public override Expression MakeExpression(BuilderContext ctx)
		{
			return Expression.Constant(null);
		}
	}
}
