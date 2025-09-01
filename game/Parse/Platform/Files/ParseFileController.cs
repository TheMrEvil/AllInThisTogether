using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Execution;
using Parse.Abstractions.Platform.Files;
using Parse.Infrastructure.Execution;
using Parse.Infrastructure.Utilities;

namespace Parse.Platform.Files
{
	// Token: 0x02000037 RID: 55
	public class ParseFileController : IParseFileController
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000A97E File Offset: 0x00008B7E
		private IParseCommandRunner CommandRunner
		{
			[CompilerGenerated]
			get
			{
				return this.<CommandRunner>k__BackingField;
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000A986 File Offset: 0x00008B86
		public ParseFileController(IParseCommandRunner commandRunner)
		{
			this.CommandRunner = commandRunner;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000A998 File Offset: 0x00008B98
		public Task<FileState> SaveAsync(FileState state, Stream dataStream, string sessionToken, IProgress<IDataTransferLevel> progress, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (state.Location != null)
			{
				return Task.FromResult<FileState>(state);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<FileState>(cancellationToken);
			}
			long oldPosition = dataStream.Position;
			IParseCommandRunner commandRunner = this.CommandRunner;
			string relativeUri = "files/" + state.Name;
			string method = "POST";
			IList<KeyValuePair<string, string>> headers = null;
			string mediaType = state.MediaType;
			return commandRunner.RunCommandAsync(new ParseCommand(relativeUri, method, sessionToken, headers, dataStream, mediaType), progress, null, cancellationToken).OnSuccess(delegate(Task<Tuple<HttpStatusCode, IDictionary<string, object>>> uploadTask)
			{
				IDictionary<string, object> item = uploadTask.Result.Item2;
				cancellationToken.ThrowIfCancellationRequested();
				return new FileState
				{
					Name = (item["name"] as string),
					Location = new Uri(item["url"] as string, UriKind.Absolute),
					MediaType = state.MediaType
				};
			}).ContinueWith<Task<FileState>>(delegate(Task<FileState> task)
			{
				if ((task.IsFaulted || task.IsCanceled) && dataStream.CanSeek)
				{
					dataStream.Seek(oldPosition, SeekOrigin.Begin);
				}
				return task;
			}).Unwrap<FileState>();
		}

		// Token: 0x0400007B RID: 123
		[CompilerGenerated]
		private readonly IParseCommandRunner <CommandRunner>k__BackingField;

		// Token: 0x0200010C RID: 268
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x060006FE RID: 1790 RVA: 0x0001576D File Offset: 0x0001396D
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x060006FF RID: 1791 RVA: 0x00015778 File Offset: 0x00013978
			internal FileState <SaveAsync>b__0(Task<Tuple<HttpStatusCode, IDictionary<string, object>>> uploadTask)
			{
				IDictionary<string, object> item = uploadTask.Result.Item2;
				this.cancellationToken.ThrowIfCancellationRequested();
				return new FileState
				{
					Name = (item["name"] as string),
					Location = new Uri(item["url"] as string, UriKind.Absolute),
					MediaType = this.state.MediaType
				};
			}

			// Token: 0x06000700 RID: 1792 RVA: 0x000157E4 File Offset: 0x000139E4
			internal Task<FileState> <SaveAsync>b__1(Task<FileState> task)
			{
				if ((task.IsFaulted || task.IsCanceled) && this.dataStream.CanSeek)
				{
					this.dataStream.Seek(this.oldPosition, SeekOrigin.Begin);
				}
				return task;
			}

			// Token: 0x0400022D RID: 557
			public CancellationToken cancellationToken;

			// Token: 0x0400022E RID: 558
			public FileState state;

			// Token: 0x0400022F RID: 559
			public Stream dataStream;

			// Token: 0x04000230 RID: 560
			public long oldPosition;
		}
	}
}
