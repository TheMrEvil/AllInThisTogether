using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Data;
using Parse.Abstractions.Infrastructure.Execution;
using Parse.Abstractions.Platform.Cloud;
using Parse.Infrastructure.Data;
using Parse.Infrastructure.Execution;
using Parse.Infrastructure.Utilities;

namespace Parse.Platform.Cloud
{
	// Token: 0x0200003B RID: 59
	public class ParseCloudCodeController : IParseCloudCodeController
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000ADC7 File Offset: 0x00008FC7
		private IParseCommandRunner CommandRunner
		{
			[CompilerGenerated]
			get
			{
				return this.<CommandRunner>k__BackingField;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000ADCF File Offset: 0x00008FCF
		private IParseDataDecoder Decoder
		{
			[CompilerGenerated]
			get
			{
				return this.<Decoder>k__BackingField;
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000ADD8 File Offset: 0x00008FD8
		public ParseCloudCodeController(IParseCommandRunner commandRunner, IParseDataDecoder decoder)
		{
			this.CommandRunner = commandRunner;
			this.Decoder = decoder;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000AE00 File Offset: 0x00009000
		public Task<T> CallFunctionAsync<T>(string name, IDictionary<string, object> parameters, string sessionToken, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.CommandRunner.RunCommandAsync(new ParseCommand("functions/" + Uri.EscapeUriString(name), "POST", sessionToken, null, NoObjectsEncoder.Instance.Encode(parameters, serviceHub) as IDictionary<string, object>), null, null, cancellationToken).OnSuccess(delegate(Task<Tuple<HttpStatusCode, IDictionary<string, object>>> task)
			{
				IDictionary<string, object> dictionary = this.Decoder.Decode(task.Result.Item2, serviceHub) as IDictionary<string, object>;
				if (dictionary.ContainsKey("result"))
				{
					return Conversion.To<T>(dictionary["result"]);
				}
				return default(T);
			});
		}

		// Token: 0x04000086 RID: 134
		[CompilerGenerated]
		private readonly IParseCommandRunner <CommandRunner>k__BackingField;

		// Token: 0x04000087 RID: 135
		[CompilerGenerated]
		private readonly IParseDataDecoder <Decoder>k__BackingField;

		// Token: 0x02000111 RID: 273
		[CompilerGenerated]
		private sealed class <>c__DisplayClass7_0<T>
		{
			// Token: 0x0600070F RID: 1807 RVA: 0x00015A52 File Offset: 0x00013C52
			public <>c__DisplayClass7_0()
			{
			}

			// Token: 0x06000710 RID: 1808 RVA: 0x00015A5C File Offset: 0x00013C5C
			internal T <CallFunctionAsync>b__0(Task<Tuple<HttpStatusCode, IDictionary<string, object>>> task)
			{
				IDictionary<string, object> dictionary = this.<>4__this.Decoder.Decode(task.Result.Item2, this.serviceHub) as IDictionary<string, object>;
				if (dictionary.ContainsKey("result"))
				{
					return Conversion.To<T>(dictionary["result"]);
				}
				return default(T);
			}

			// Token: 0x0400023E RID: 574
			public ParseCloudCodeController <>4__this;

			// Token: 0x0400023F RID: 575
			public IServiceHub serviceHub;
		}
	}
}
