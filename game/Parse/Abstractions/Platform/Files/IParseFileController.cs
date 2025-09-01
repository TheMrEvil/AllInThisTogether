using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Platform.Files;

namespace Parse.Abstractions.Platform.Files
{
	// Token: 0x02000085 RID: 133
	public interface IParseFileController
	{
		// Token: 0x06000537 RID: 1335
		Task<FileState> SaveAsync(FileState state, Stream dataStream, string sessionToken, IProgress<IDataTransferLevel> progress, CancellationToken cancellationToken);
	}
}
