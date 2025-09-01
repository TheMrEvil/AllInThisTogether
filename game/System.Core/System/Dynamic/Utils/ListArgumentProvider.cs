using System;
using System.Linq.Expressions;

namespace System.Dynamic.Utils
{
	// Token: 0x0200032D RID: 813
	internal sealed class ListArgumentProvider : ListProvider<Expression>
	{
		// Token: 0x0600188D RID: 6285 RVA: 0x00052B45 File Offset: 0x00050D45
		internal ListArgumentProvider(IArgumentProvider provider, Expression arg0)
		{
			this._provider = provider;
			this._arg0 = arg0;
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x0600188E RID: 6286 RVA: 0x00052B5B File Offset: 0x00050D5B
		protected override Expression First
		{
			get
			{
				return this._arg0;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x00052B63 File Offset: 0x00050D63
		protected override int ElementCount
		{
			get
			{
				return this._provider.ArgumentCount;
			}
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x00052B70 File Offset: 0x00050D70
		protected override Expression GetElement(int index)
		{
			return this._provider.GetArgument(index);
		}

		// Token: 0x04000BE4 RID: 3044
		private readonly IArgumentProvider _provider;

		// Token: 0x04000BE5 RID: 3045
		private readonly Expression _arg0;
	}
}
