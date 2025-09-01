using System;

namespace Parse.Abstractions.Infrastructure.Control
{
	// Token: 0x0200009F RID: 159
	public interface IParseFieldOperation
	{
		// Token: 0x060005B9 RID: 1465
		object Encode(IServiceHub serviceHub);

		// Token: 0x060005BA RID: 1466
		IParseFieldOperation MergeWithPrevious(IParseFieldOperation previous);

		// Token: 0x060005BB RID: 1467
		object Apply(object oldValue, string key);
	}
}
