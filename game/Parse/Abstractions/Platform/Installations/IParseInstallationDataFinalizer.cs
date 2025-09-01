using System;
using System.Threading.Tasks;

namespace Parse.Abstractions.Platform.Installations
{
	// Token: 0x02000084 RID: 132
	public interface IParseInstallationDataFinalizer
	{
		// Token: 0x06000535 RID: 1333
		Task FinalizeAsync(ParseInstallation installation);

		// Token: 0x06000536 RID: 1334
		void Initialize();
	}
}
