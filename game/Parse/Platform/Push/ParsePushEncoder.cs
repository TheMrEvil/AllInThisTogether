using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Platform.Push;
using Parse.Infrastructure.Utilities;

namespace Parse.Platform.Push
{
	// Token: 0x0200002C RID: 44
	public class ParsePushEncoder
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000950C File Offset: 0x0000770C
		public static ParsePushEncoder Instance
		{
			[CompilerGenerated]
			get
			{
				return ParsePushEncoder.<Instance>k__BackingField;
			}
		} = new ParsePushEncoder();

		// Token: 0x06000236 RID: 566 RVA: 0x00009513 File Offset: 0x00007713
		private ParsePushEncoder()
		{
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000951C File Offset: 0x0000771C
		public IDictionary<string, object> Encode(IPushState state)
		{
			if (state.Alert == null && state.Data == null)
			{
				throw new InvalidOperationException("A push must have either an Alert or Data");
			}
			if (state.Channels == null && state.Query == null)
			{
				throw new InvalidOperationException("A push must have either Channels or a Query");
			}
			IDictionary<string, object> dictionary;
			if ((dictionary = state.Data) == null)
			{
				(dictionary = new Dictionary<string, object>())["alert"] = state.Alert;
			}
			IDictionary<string, object> value = dictionary;
			ParseQuery<ParseInstallation> parseQuery = state.Query ?? new ParseQuery<ParseInstallation>(null, "_Installation");
			if (state.Channels != null)
			{
				parseQuery = parseQuery.WhereContainedIn<string>("channels", state.Channels);
			}
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			dictionary2["data"] = value;
			dictionary2["where"] = parseQuery.BuildParameters(false).GetOrDefault("where", new Dictionary<string, object>());
			Dictionary<string, object> dictionary3 = dictionary2;
			if (state.Expiration != null)
			{
				dictionary3["expiration_time"] = state.Expiration.Value.ToString("yyyy-MM-ddTHH:mm:ssZ");
			}
			else if (state.ExpirationInterval != null)
			{
				dictionary3["expiration_interval"] = state.ExpirationInterval.Value.TotalSeconds;
			}
			if (state.PushTime != null)
			{
				dictionary3["push_time"] = state.PushTime.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
			}
			return dictionary3;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00009696 File Offset: 0x00007896
		// Note: this type is marked as 'beforefieldinit'.
		static ParsePushEncoder()
		{
		}

		// Token: 0x04000053 RID: 83
		[CompilerGenerated]
		private static readonly ParsePushEncoder <Instance>k__BackingField;
	}
}
