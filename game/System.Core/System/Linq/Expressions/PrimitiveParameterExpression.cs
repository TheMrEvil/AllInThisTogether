using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000294 RID: 660
	internal sealed class PrimitiveParameterExpression<T> : ParameterExpression
	{
		// Token: 0x06001311 RID: 4881 RVA: 0x0003C751 File Offset: 0x0003A951
		internal PrimitiveParameterExpression(string name) : base(name)
		{
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x0000BEAA File Offset: 0x0000A0AA
		public sealed override Type Type
		{
			get
			{
				return typeof(T);
			}
		}
	}
}
