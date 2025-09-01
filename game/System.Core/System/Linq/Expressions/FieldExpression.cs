using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000279 RID: 633
	internal sealed class FieldExpression : MemberExpression
	{
		// Token: 0x06001276 RID: 4726 RVA: 0x0003B63F File Offset: 0x0003983F
		public FieldExpression(Expression expression, FieldInfo member) : base(expression)
		{
			this._field = member;
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0003B64F File Offset: 0x0003984F
		internal override MemberInfo GetMember()
		{
			return this._field;
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x0003B657 File Offset: 0x00039857
		public sealed override Type Type
		{
			get
			{
				return this._field.FieldType;
			}
		}

		// Token: 0x04000A24 RID: 2596
		private readonly FieldInfo _field;
	}
}
