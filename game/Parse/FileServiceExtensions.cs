using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Infrastructure.Utilities;
using Parse.Platform.Files;

namespace Parse
{
	// Token: 0x02000009 RID: 9
	public static class FileServiceExtensions
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000022DE File Offset: 0x000004DE
		public static Task SaveFileAsync(this IServiceHub serviceHub, ParseFile file, CancellationToken cancellationToken = default(CancellationToken))
		{
			return serviceHub.SaveFileAsync(file, null, cancellationToken);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022EC File Offset: 0x000004EC
		public static Task SaveFileAsync(this IServiceHub serviceHub, ParseFile file, IProgress<IDataTransferLevel> progress, CancellationToken cancellationToken = default(CancellationToken))
		{
			return file.TaskQueue.Enqueue<Task<FileState>>((Task toAwait) => serviceHub.FileController.SaveAsync(file.State, file.DataStream, serviceHub.GetCurrentSessionToken(), progress, cancellationToken), cancellationToken).OnSuccess((Task<FileState> task) => file.State = task.Result);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000234E File Offset: 0x0000054E
		public static Task SaveAsync(this ParseFile file, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken))
		{
			return serviceHub.SaveFileAsync(file, cancellationToken);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002358 File Offset: 0x00000558
		public static Task SaveAsync(this ParseFile file, IServiceHub serviceHub, IProgress<IDataTransferLevel> progress, CancellationToken cancellationToken = default(CancellationToken))
		{
			return serviceHub.SaveFileAsync(file, progress, cancellationToken);
		}

		// Token: 0x020000A1 RID: 161
		[CompilerGenerated]
		private sealed class <>c__DisplayClass1_0
		{
			// Token: 0x060005BD RID: 1469 RVA: 0x00012CBC File Offset: 0x00010EBC
			public <>c__DisplayClass1_0()
			{
			}

			// Token: 0x060005BE RID: 1470 RVA: 0x00012CC4 File Offset: 0x00010EC4
			internal Task<FileState> <SaveFileAsync>b__0(Task toAwait)
			{
				return this.serviceHub.FileController.SaveAsync(this.file.State, this.file.DataStream, this.serviceHub.GetCurrentSessionToken(), this.progress, this.cancellationToken);
			}

			// Token: 0x060005BF RID: 1471 RVA: 0x00012D04 File Offset: 0x00010F04
			internal FileState <SaveFileAsync>b__1(Task<FileState> task)
			{
				return this.file.State = task.Result;
			}

			// Token: 0x04000109 RID: 265
			public IServiceHub serviceHub;

			// Token: 0x0400010A RID: 266
			public ParseFile file;

			// Token: 0x0400010B RID: 267
			public IProgress<IDataTransferLevel> progress;

			// Token: 0x0400010C RID: 268
			public CancellationToken cancellationToken;
		}
	}
}
