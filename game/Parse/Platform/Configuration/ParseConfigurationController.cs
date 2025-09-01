using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Data;
using Parse.Abstractions.Infrastructure.Execution;
using Parse.Abstractions.Platform.Configuration;
using Parse.Infrastructure.Execution;
using Parse.Infrastructure.Utilities;

namespace Parse.Platform.Configuration
{
	// Token: 0x02000039 RID: 57
	internal class ParseConfigurationController : IParseConfigurationController
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000AB76 File Offset: 0x00008D76
		private IParseCommandRunner CommandRunner
		{
			[CompilerGenerated]
			get
			{
				return this.<CommandRunner>k__BackingField;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000AB7E File Offset: 0x00008D7E
		private IParseDataDecoder Decoder
		{
			[CompilerGenerated]
			get
			{
				return this.<Decoder>k__BackingField;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000AB86 File Offset: 0x00008D86
		public IParseCurrentConfigurationController CurrentConfigurationController
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentConfigurationController>k__BackingField;
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000AB8E File Offset: 0x00008D8E
		public ParseConfigurationController(IParseCommandRunner commandRunner, ICacheController storageController, IParseDataDecoder decoder)
		{
			this.CommandRunner = commandRunner;
			this.CurrentConfigurationController = new ParseCurrentConfigurationController(storageController, decoder);
			this.Decoder = decoder;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000ABB4 File Offset: 0x00008DB4
		public Task<ParseConfiguration> FetchConfigAsync(string sessionToken, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.CommandRunner.RunCommandAsync(new ParseCommand("config", "GET", sessionToken, null, null), null, null, cancellationToken).OnSuccess(delegate(Task<Tuple<HttpStatusCode, IDictionary<string, object>>> task)
			{
				cancellationToken.ThrowIfCancellationRequested();
				return this.Decoder.BuildConfiguration(task.Result.Item2, serviceHub);
			}).OnSuccess(delegate(Task<ParseConfiguration> task)
			{
				cancellationToken.ThrowIfCancellationRequested();
				this.CurrentConfigurationController.SetCurrentConfigAsync(task.Result);
				return task;
			}).Unwrap<ParseConfiguration>();
		}

		// Token: 0x0400007E RID: 126
		[CompilerGenerated]
		private readonly IParseCommandRunner <CommandRunner>k__BackingField;

		// Token: 0x0400007F RID: 127
		[CompilerGenerated]
		private readonly IParseDataDecoder <Decoder>k__BackingField;

		// Token: 0x04000080 RID: 128
		[CompilerGenerated]
		private readonly IParseCurrentConfigurationController <CurrentConfigurationController>k__BackingField;

		// Token: 0x0200010D RID: 269
		[CompilerGenerated]
		private sealed class <>c__DisplayClass10_0
		{
			// Token: 0x06000701 RID: 1793 RVA: 0x00015817 File Offset: 0x00013A17
			public <>c__DisplayClass10_0()
			{
			}

			// Token: 0x06000702 RID: 1794 RVA: 0x0001581F File Offset: 0x00013A1F
			internal ParseConfiguration <FetchConfigAsync>b__0(Task<Tuple<HttpStatusCode, IDictionary<string, object>>> task)
			{
				this.cancellationToken.ThrowIfCancellationRequested();
				return this.<>4__this.Decoder.BuildConfiguration(task.Result.Item2, this.serviceHub);
			}

			// Token: 0x06000703 RID: 1795 RVA: 0x0001584D File Offset: 0x00013A4D
			internal Task<ParseConfiguration> <FetchConfigAsync>b__1(Task<ParseConfiguration> task)
			{
				this.cancellationToken.ThrowIfCancellationRequested();
				this.<>4__this.CurrentConfigurationController.SetCurrentConfigAsync(task.Result);
				return task;
			}

			// Token: 0x04000231 RID: 561
			public CancellationToken cancellationToken;

			// Token: 0x04000232 RID: 562
			public ParseConfigurationController <>4__this;

			// Token: 0x04000233 RID: 563
			public IServiceHub serviceHub;
		}
	}
}
