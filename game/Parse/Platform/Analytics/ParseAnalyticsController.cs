using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Execution;
using Parse.Abstractions.Platform.Analytics;
using Parse.Infrastructure.Data;
using Parse.Infrastructure.Execution;

namespace Parse.Platform.Analytics
{
	// Token: 0x0200003C RID: 60
	public class ParseAnalyticsController : IParseAnalyticsController
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000AE74 File Offset: 0x00009074
		private IParseCommandRunner Runner
		{
			[CompilerGenerated]
			get
			{
				return this.<Runner>k__BackingField;
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000AE7C File Offset: 0x0000907C
		public ParseAnalyticsController(IParseCommandRunner commandRunner)
		{
			this.Runner = commandRunner;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000AE8C File Offset: 0x0000908C
		public Task TrackEventAsync(string name, IDictionary<string, string> dimensions, string sessionToken, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken))
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["at"] = DateTime.Now;
			dictionary["name"] = name;
			IDictionary<string, object> dictionary2 = dictionary;
			if (dimensions != null)
			{
				dictionary2["dimensions"] = dimensions;
			}
			return this.Runner.RunCommandAsync(new ParseCommand("events/" + name, "POST", sessionToken, null, PointerOrLocalIdEncoder.Instance.Encode(dictionary2, serviceHub) as IDictionary<string, object>), null, null, cancellationToken);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000AF08 File Offset: 0x00009108
		public Task TrackAppOpenedAsync(string pushHash, string sessionToken, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken))
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["at"] = DateTime.Now;
			IDictionary<string, object> dictionary2 = dictionary;
			if (pushHash != null)
			{
				dictionary2["push_hash"] = pushHash;
			}
			return this.Runner.RunCommandAsync(new ParseCommand("events/AppOpened", "POST", sessionToken, null, PointerOrLocalIdEncoder.Instance.Encode(dictionary2, serviceHub) as IDictionary<string, object>), null, null, cancellationToken);
		}

		// Token: 0x04000088 RID: 136
		[CompilerGenerated]
		private readonly IParseCommandRunner <Runner>k__BackingField;
	}
}
