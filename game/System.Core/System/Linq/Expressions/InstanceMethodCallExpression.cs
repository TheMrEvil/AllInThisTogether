using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x0200027F RID: 639
	internal class InstanceMethodCallExpression : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x060012A2 RID: 4770 RVA: 0x0003B976 File Offset: 0x00039B76
		public InstanceMethodCallExpression(MethodInfo method, Expression instance) : base(method)
		{
			this._instance = instance;
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x0003B986 File Offset: 0x00039B86
		internal override Expression GetInstance()
		{
			return this._instance;
		}

		// Token: 0x04000A2B RID: 2603
		private readonly Expression _instance;
	}
}
