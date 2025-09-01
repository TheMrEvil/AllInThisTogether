using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	// Token: 0x0200003E RID: 62
	// (Invoke) Token: 0x06000226 RID: 550
	internal delegate bool TryParseListDelegate<T>(string value, int minimalCount, out List<T> result);
}
