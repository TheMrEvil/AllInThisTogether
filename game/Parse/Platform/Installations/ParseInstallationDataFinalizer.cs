using System;
using System.Threading.Tasks;
using Parse.Abstractions.Platform.Installations;

namespace Parse.Platform.Installations
{
	// Token: 0x02000035 RID: 53
	public class ParseInstallationDataFinalizer : IParseInstallationDataFinalizer
	{
		// Token: 0x0600029F RID: 671 RVA: 0x0000A8C8 File Offset: 0x00008AC8
		public Task FinalizeAsync(ParseInstallation installation)
		{
			return Task.FromResult<object>(null);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000A8D0 File Offset: 0x00008AD0
		public void Initialize()
		{
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000A8D2 File Offset: 0x00008AD2
		public ParseInstallationDataFinalizer()
		{
		}
	}
}
