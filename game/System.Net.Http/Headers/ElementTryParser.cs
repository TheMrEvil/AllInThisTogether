using System;

namespace System.Net.Http.Headers
{
	// Token: 0x02000037 RID: 55
	// (Invoke) Token: 0x060001D2 RID: 466
	internal delegate bool ElementTryParser<T>(Lexer lexer, out T parsedValue, out Token token);
}
