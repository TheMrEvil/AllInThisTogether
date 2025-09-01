using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Parse.Infrastructure.Execution;

namespace Parse.Abstractions.Infrastructure.Execution
{
	// Token: 0x0200009D RID: 157
	public interface IWebClient
	{
		// Token: 0x060005B7 RID: 1463
		Task<Tuple<HttpStatusCode, string>> ExecuteAsync(WebRequest httpRequest, IProgress<IDataTransferLevel> uploadProgress, IProgress<IDataTransferLevel> downloadProgress, CancellationToken cancellationToken = default(CancellationToken));
	}
}
