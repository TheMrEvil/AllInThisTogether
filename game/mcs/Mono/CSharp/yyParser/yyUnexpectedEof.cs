using System;

namespace Mono.CSharp.yyParser
{
	// Token: 0x02000307 RID: 775
	public class yyUnexpectedEof : yyException
	{
		// Token: 0x060024A6 RID: 9382 RVA: 0x000AF9C7 File Offset: 0x000ADBC7
		public yyUnexpectedEof(string message) : base(message)
		{
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x000AF9D0 File Offset: 0x000ADBD0
		public yyUnexpectedEof() : base("")
		{
		}
	}
}
