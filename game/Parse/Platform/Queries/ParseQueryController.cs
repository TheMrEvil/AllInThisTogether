using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure.Data;
using Parse.Abstractions.Infrastructure.Execution;
using Parse.Abstractions.Platform.Objects;
using Parse.Abstractions.Platform.Queries;
using Parse.Infrastructure.Data;
using Parse.Infrastructure.Execution;
using Parse.Infrastructure.Utilities;

namespace Parse.Platform.Queries
{
	// Token: 0x02000028 RID: 40
	internal class ParseQueryController : IParseQueryController
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000901B File Offset: 0x0000721B
		private IParseCommandRunner CommandRunner
		{
			[CompilerGenerated]
			get
			{
				return this.<CommandRunner>k__BackingField;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00009023 File Offset: 0x00007223
		private IParseDataDecoder Decoder
		{
			[CompilerGenerated]
			get
			{
				return this.<Decoder>k__BackingField;
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000902C File Offset: 0x0000722C
		public ParseQueryController(IParseCommandRunner commandRunner, IParseDataDecoder decoder)
		{
			this.CommandRunner = commandRunner;
			this.Decoder = decoder;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00009054 File Offset: 0x00007254
		public Task<IEnumerable<IObjectState>> FindAsync<T>(ParseQuery<T> query, ParseUser user, CancellationToken cancellationToken = default(CancellationToken)) where T : ParseObject
		{
			string className = query.ClassName;
			IDictionary<string, object> parameters = query.BuildParameters(false);
			ParseUser user2 = user;
			Func<object, IObjectState> <>9__1;
			return this.FindAsync(className, parameters, (user2 != null) ? user2.SessionToken : null, cancellationToken).OnSuccess(delegate(Task<IDictionary<string, object>> t)
			{
				IEnumerable<object> source = t.Result["results"] as IList<object>;
				Func<object, IObjectState> selector;
				if ((selector = <>9__1) == null)
				{
					selector = (<>9__1 = ((object item) => ParseObjectCoder.Instance.Decode(item as IDictionary<string, object>, this.Decoder, user.Services)));
				}
				return source.Select(selector);
			});
		}

		// Token: 0x06000217 RID: 535 RVA: 0x000090AC File Offset: 0x000072AC
		public Task<int> CountAsync<T>(ParseQuery<T> query, ParseUser user, CancellationToken cancellationToken = default(CancellationToken)) where T : ParseObject
		{
			IDictionary<string, object> dictionary = query.BuildParameters(false);
			dictionary["limit"] = 0;
			dictionary["count"] = 1;
			return this.FindAsync(query.ClassName, dictionary, (user != null) ? user.SessionToken : null, cancellationToken).OnSuccess((Task<IDictionary<string, object>> task) => Convert.ToInt32(task.Result["count"]));
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00009124 File Offset: 0x00007324
		public Task<IObjectState> FirstAsync<T>(ParseQuery<T> query, ParseUser user, CancellationToken cancellationToken = default(CancellationToken)) where T : ParseObject
		{
			IDictionary<string, object> dictionary = query.BuildParameters(false);
			dictionary["limit"] = 1;
			string className = query.ClassName;
			IDictionary<string, object> parameters = dictionary;
			ParseUser user2 = user;
			return this.FindAsync(className, parameters, (user2 != null) ? user2.SessionToken : null, cancellationToken).OnSuccess(delegate(Task<IDictionary<string, object>> task)
			{
				Dictionary<string, object> dictionary2 = ((task.Result["results"] as IList<object>).FirstOrDefault<object>() as IDictionary<string, object>) as Dictionary<string, object>;
				if (dictionary2 == null || dictionary2 == null)
				{
					return null;
				}
				return ParseObjectCoder.Instance.Decode(dictionary2, this.Decoder, user.Services);
			});
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00009190 File Offset: 0x00007390
		private Task<IDictionary<string, object>> FindAsync(string className, IDictionary<string, object> parameters, string sessionToken, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.CommandRunner.RunCommandAsync(new ParseCommand("classes/" + Uri.EscapeDataString(className) + "?" + ParseClient.BuildQueryString(parameters), "GET", sessionToken, null, null), null, null, cancellationToken).OnSuccess((Task<Tuple<HttpStatusCode, IDictionary<string, object>>> t) => t.Result.Item2);
		}

		// Token: 0x04000047 RID: 71
		[CompilerGenerated]
		private readonly IParseCommandRunner <CommandRunner>k__BackingField;

		// Token: 0x04000048 RID: 72
		[CompilerGenerated]
		private readonly IParseDataDecoder <Decoder>k__BackingField;

		// Token: 0x020000F5 RID: 245
		[CompilerGenerated]
		private sealed class <>c__DisplayClass7_0<T> where T : ParseObject
		{
			// Token: 0x060006B8 RID: 1720 RVA: 0x00014D95 File Offset: 0x00012F95
			public <>c__DisplayClass7_0()
			{
			}

			// Token: 0x060006B9 RID: 1721 RVA: 0x00014DA0 File Offset: 0x00012FA0
			internal IEnumerable<IObjectState> <FindAsync>b__0(Task<IDictionary<string, object>> t)
			{
				IEnumerable<object> source = t.Result["results"] as IList<object>;
				Func<object, IObjectState> selector;
				if ((selector = this.<>9__1) == null)
				{
					selector = (this.<>9__1 = ((object item) => ParseObjectCoder.Instance.Decode(item as IDictionary<string, object>, this.<>4__this.Decoder, this.user.Services)));
				}
				return source.Select(selector);
			}

			// Token: 0x060006BA RID: 1722 RVA: 0x00014DE6 File Offset: 0x00012FE6
			internal IObjectState <FindAsync>b__1(object item)
			{
				return ParseObjectCoder.Instance.Decode(item as IDictionary<string, object>, this.<>4__this.Decoder, this.user.Services);
			}

			// Token: 0x040001F4 RID: 500
			public ParseQueryController <>4__this;

			// Token: 0x040001F5 RID: 501
			public ParseUser user;

			// Token: 0x040001F6 RID: 502
			public Func<object, IObjectState> <>9__1;
		}

		// Token: 0x020000F6 RID: 246
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__8<T> where T : ParseObject
		{
			// Token: 0x060006BB RID: 1723 RVA: 0x00014E0E File Offset: 0x0001300E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__8()
			{
			}

			// Token: 0x060006BC RID: 1724 RVA: 0x00014E1A File Offset: 0x0001301A
			public <>c__8()
			{
			}

			// Token: 0x060006BD RID: 1725 RVA: 0x00014E22 File Offset: 0x00013022
			internal int <CountAsync>b__8_0(Task<IDictionary<string, object>> task)
			{
				return Convert.ToInt32(task.Result["count"]);
			}

			// Token: 0x040001F7 RID: 503
			public static readonly ParseQueryController.<>c__8<T> <>9 = new ParseQueryController.<>c__8<T>();

			// Token: 0x040001F8 RID: 504
			public static Func<Task<IDictionary<string, object>>, int> <>9__8_0;
		}

		// Token: 0x020000F7 RID: 247
		[CompilerGenerated]
		private sealed class <>c__DisplayClass9_0<T> where T : ParseObject
		{
			// Token: 0x060006BE RID: 1726 RVA: 0x00014E39 File Offset: 0x00013039
			public <>c__DisplayClass9_0()
			{
			}

			// Token: 0x060006BF RID: 1727 RVA: 0x00014E44 File Offset: 0x00013044
			internal IObjectState <FirstAsync>b__0(Task<IDictionary<string, object>> task)
			{
				Dictionary<string, object> dictionary = ((task.Result["results"] as IList<object>).FirstOrDefault<object>() as IDictionary<string, object>) as Dictionary<string, object>;
				if (dictionary == null || dictionary == null)
				{
					return null;
				}
				return ParseObjectCoder.Instance.Decode(dictionary, this.<>4__this.Decoder, this.user.Services);
			}

			// Token: 0x040001F9 RID: 505
			public ParseQueryController <>4__this;

			// Token: 0x040001FA RID: 506
			public ParseUser user;
		}

		// Token: 0x020000F8 RID: 248
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060006C0 RID: 1728 RVA: 0x00014E9F File Offset: 0x0001309F
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060006C1 RID: 1729 RVA: 0x00014EAB File Offset: 0x000130AB
			public <>c()
			{
			}

			// Token: 0x060006C2 RID: 1730 RVA: 0x00014EB3 File Offset: 0x000130B3
			internal IDictionary<string, object> <FindAsync>b__10_0(Task<Tuple<HttpStatusCode, IDictionary<string, object>>> t)
			{
				return t.Result.Item2;
			}

			// Token: 0x040001FB RID: 507
			public static readonly ParseQueryController.<>c <>9 = new ParseQueryController.<>c();

			// Token: 0x040001FC RID: 508
			public static Func<Task<Tuple<HttpStatusCode, IDictionary<string, object>>>, IDictionary<string, object>> <>9__10_0;
		}
	}
}
