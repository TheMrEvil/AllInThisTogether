using System;
using System.Linq.Expressions;

namespace System.Dynamic.Utils
{
	// Token: 0x0200032E RID: 814
	internal sealed class ListParameterProvider : ListProvider<ParameterExpression>
	{
		// Token: 0x06001891 RID: 6289 RVA: 0x00052B7E File Offset: 0x00050D7E
		internal ListParameterProvider(IParameterProvider provider, ParameterExpression arg0)
		{
			this._provider = provider;
			this._arg0 = arg0;
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x00052B94 File Offset: 0x00050D94
		protected override ParameterExpression First
		{
			get
			{
				return this._arg0;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x00052B9C File Offset: 0x00050D9C
		protected override int ElementCount
		{
			get
			{
				return this._provider.ParameterCount;
			}
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x00052BA9 File Offset: 0x00050DA9
		protected override ParameterExpression GetElement(int index)
		{
			return this._provider.GetParameter(index);
		}

		// Token: 0x04000BE6 RID: 3046
		private readonly IParameterProvider _provider;

		// Token: 0x04000BE7 RID: 3047
		private readonly ParameterExpression _arg0;
	}
}
