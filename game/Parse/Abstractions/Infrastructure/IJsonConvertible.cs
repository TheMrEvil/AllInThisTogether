using System;
using System.Collections.Generic;

namespace Parse.Abstractions.Infrastructure
{
	// Token: 0x02000093 RID: 147
	public interface IJsonConvertible
	{
		// Token: 0x06000576 RID: 1398
		IDictionary<string, object> ConvertToJSON();
	}
}
