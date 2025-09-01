using System;
using System.Collections.Generic;
using Parse.Abstractions.Infrastructure;

namespace Parse.Abstractions.Platform.Installations
{
	// Token: 0x02000082 RID: 130
	public interface IParseInstallationCoder
	{
		// Token: 0x06000530 RID: 1328
		IDictionary<string, object> Encode(ParseInstallation installation);

		// Token: 0x06000531 RID: 1329
		ParseInstallation Decode(IDictionary<string, object> data, IServiceHub serviceHub);
	}
}
