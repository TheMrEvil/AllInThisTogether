using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Parse.Infrastructure.Execution;

namespace Parse.Abstractions.Infrastructure.Execution
{
	// Token: 0x0200009C RID: 156
	public interface IParseCommandRunner
	{
		// Token: 0x060005B6 RID: 1462
		Task<Tuple<HttpStatusCode, IDictionary<string, object>>> RunCommandAsync(ParseCommand command, IProgress<IDataTransferLevel> uploadProgress = null, IProgress<IDataTransferLevel> downloadProgress = null, CancellationToken cancellationToken = default(CancellationToken));
	}
}
