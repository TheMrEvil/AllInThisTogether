using System;

namespace Mono.CSharp.yyParser
{
	// Token: 0x02000308 RID: 776
	public interface yyInput
	{
		// Token: 0x060024A8 RID: 9384
		bool advance();

		// Token: 0x060024A9 RID: 9385
		int token();

		// Token: 0x060024AA RID: 9386
		object value();
	}
}
