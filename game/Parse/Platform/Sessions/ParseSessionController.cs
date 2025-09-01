using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Data;
using Parse.Abstractions.Infrastructure.Execution;
using Parse.Abstractions.Platform.Objects;
using Parse.Abstractions.Platform.Sessions;
using Parse.Infrastructure.Data;
using Parse.Infrastructure.Execution;
using Parse.Infrastructure.Utilities;

namespace Parse.Platform.Sessions
{
	// Token: 0x02000027 RID: 39
	public class ParseSessionController : IParseSessionController
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00008F05 File Offset: 0x00007105
		private IParseCommandRunner CommandRunner
		{
			[CompilerGenerated]
			get
			{
				return this.<CommandRunner>k__BackingField;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00008F0D File Offset: 0x0000710D
		private IParseDataDecoder Decoder
		{
			[CompilerGenerated]
			get
			{
				return this.<Decoder>k__BackingField;
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00008F18 File Offset: 0x00007118
		public ParseSessionController(IParseCommandRunner commandRunner, IParseDataDecoder decoder)
		{
			this.CommandRunner = commandRunner;
			this.Decoder = decoder;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00008F40 File Offset: 0x00007140
		public Task<IObjectState> GetSessionAsync(string sessionToken, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.CommandRunner.RunCommandAsync(new ParseCommand("sessions/me", "GET", sessionToken, null, null), null, null, cancellationToken).OnSuccess((Task<Tuple<HttpStatusCode, IDictionary<string, object>>> task) => ParseObjectCoder.Instance.Decode(task.Result.Item2, this.Decoder, serviceHub));
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00008F92 File Offset: 0x00007192
		public Task RevokeAsync(string sessionToken, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.CommandRunner.RunCommandAsync(new ParseCommand("logout", "POST", sessionToken, null, new Dictionary<string, object>()), null, null, cancellationToken);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00008FB8 File Offset: 0x000071B8
		public Task<IObjectState> UpgradeToRevocableSessionAsync(string sessionToken, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.CommandRunner.RunCommandAsync(new ParseCommand("upgradeToRevocableSession", "POST", sessionToken, null, new Dictionary<string, object>()), null, null, cancellationToken).OnSuccess((Task<Tuple<HttpStatusCode, IDictionary<string, object>>> task) => ParseObjectCoder.Instance.Decode(task.Result.Item2, this.Decoder, serviceHub));
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000900E File Offset: 0x0000720E
		public bool IsRevocableSessionToken(string sessionToken)
		{
			return sessionToken.Contains("r:");
		}

		// Token: 0x04000045 RID: 69
		[CompilerGenerated]
		private readonly IParseCommandRunner <CommandRunner>k__BackingField;

		// Token: 0x04000046 RID: 70
		[CompilerGenerated]
		private readonly IParseDataDecoder <Decoder>k__BackingField;

		// Token: 0x020000F3 RID: 243
		[CompilerGenerated]
		private sealed class <>c__DisplayClass7_0
		{
			// Token: 0x060006B4 RID: 1716 RVA: 0x00014D35 File Offset: 0x00012F35
			public <>c__DisplayClass7_0()
			{
			}

			// Token: 0x060006B5 RID: 1717 RVA: 0x00014D3D File Offset: 0x00012F3D
			internal IObjectState <GetSessionAsync>b__0(Task<Tuple<HttpStatusCode, IDictionary<string, object>>> task)
			{
				return ParseObjectCoder.Instance.Decode(task.Result.Item2, this.<>4__this.Decoder, this.serviceHub);
			}

			// Token: 0x040001F0 RID: 496
			public ParseSessionController <>4__this;

			// Token: 0x040001F1 RID: 497
			public IServiceHub serviceHub;
		}

		// Token: 0x020000F4 RID: 244
		[CompilerGenerated]
		private sealed class <>c__DisplayClass9_0
		{
			// Token: 0x060006B6 RID: 1718 RVA: 0x00014D65 File Offset: 0x00012F65
			public <>c__DisplayClass9_0()
			{
			}

			// Token: 0x060006B7 RID: 1719 RVA: 0x00014D6D File Offset: 0x00012F6D
			internal IObjectState <UpgradeToRevocableSessionAsync>b__0(Task<Tuple<HttpStatusCode, IDictionary<string, object>>> task)
			{
				return ParseObjectCoder.Instance.Decode(task.Result.Item2, this.<>4__this.Decoder, this.serviceHub);
			}

			// Token: 0x040001F2 RID: 498
			public ParseSessionController <>4__this;

			// Token: 0x040001F3 RID: 499
			public IServiceHub serviceHub;
		}
	}
}
