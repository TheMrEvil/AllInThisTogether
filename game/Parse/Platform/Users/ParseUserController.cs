using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Control;
using Parse.Abstractions.Infrastructure.Data;
using Parse.Abstractions.Infrastructure.Execution;
using Parse.Abstractions.Platform.Objects;
using Parse.Abstractions.Platform.Users;
using Parse.Infrastructure.Data;
using Parse.Infrastructure.Execution;
using Parse.Infrastructure.Utilities;
using Parse.Platform.Objects;

namespace Parse.Platform.Users
{
	// Token: 0x02000026 RID: 38
	public class ParseUserController : IParseUserController
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00008CCA File Offset: 0x00006ECA
		private IParseCommandRunner CommandRunner
		{
			[CompilerGenerated]
			get
			{
				return this.<CommandRunner>k__BackingField;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00008CD2 File Offset: 0x00006ED2
		private IParseDataDecoder Decoder
		{
			[CompilerGenerated]
			get
			{
				return this.<Decoder>k__BackingField;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00008CDA File Offset: 0x00006EDA
		// (set) Token: 0x06000204 RID: 516 RVA: 0x00008CE2 File Offset: 0x00006EE2
		public bool RevocableSessionEnabled
		{
			[CompilerGenerated]
			get
			{
				return this.<RevocableSessionEnabled>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RevocableSessionEnabled>k__BackingField = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00008CEB File Offset: 0x00006EEB
		public object RevocableSessionEnabledMutex
		{
			[CompilerGenerated]
			get
			{
				return this.<RevocableSessionEnabledMutex>k__BackingField;
			}
		} = new object();

		// Token: 0x06000206 RID: 518 RVA: 0x00008CF4 File Offset: 0x00006EF4
		public ParseUserController(IParseCommandRunner commandRunner, IParseDataDecoder decoder)
		{
			this.CommandRunner = commandRunner;
			this.Decoder = decoder;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00008D24 File Offset: 0x00006F24
		public Task<IObjectState> SignUpAsync(IObjectState state, IDictionary<string, IParseFieldOperation> operations, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.CommandRunner.RunCommandAsync(new ParseCommand("classes/_User", "POST", null, null, serviceHub.GenerateJSONObjectForSaving(operations)), null, null, cancellationToken).OnSuccess((Task<Tuple<HttpStatusCode, IDictionary<string, object>>> task) => ParseObjectCoder.Instance.Decode(task.Result.Item2, this.Decoder, serviceHub).MutatedClone(delegate(MutableObjectState mutableClone)
			{
				mutableClone.IsNew = true;
			}));
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00008D84 File Offset: 0x00006F84
		public Task<IObjectState> LogInAsync(string username, string password, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken))
		{
			IParseCommandRunner commandRunner = this.CommandRunner;
			string str = "login?";
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["username"] = username;
			dictionary["password"] = password;
			return commandRunner.RunCommandAsync(new ParseCommand(str + ParseClient.BuildQueryString(dictionary), "GET", null, null, null), null, null, cancellationToken).OnSuccess((Task<Tuple<HttpStatusCode, IDictionary<string, object>>> task) => ParseObjectCoder.Instance.Decode(task.Result.Item2, this.Decoder, serviceHub).MutatedClone(delegate(MutableObjectState mutableClone)
			{
				mutableClone.IsNew = (task.Result.Item1 == HttpStatusCode.Created);
			}));
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00008E00 File Offset: 0x00007000
		public Task<IObjectState> LogInAsync(string authType, IDictionary<string, object> data, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken))
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary[authType] = data;
			Dictionary<string, object> value = dictionary;
			IParseCommandRunner commandRunner = this.CommandRunner;
			string relativeUri = "users";
			string method = "POST";
			string sessionToken = null;
			IList<KeyValuePair<string, string>> headers = null;
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			dictionary2["authData"] = value;
			return commandRunner.RunCommandAsync(new ParseCommand(relativeUri, method, sessionToken, headers, dictionary2), null, null, cancellationToken).OnSuccess((Task<Tuple<HttpStatusCode, IDictionary<string, object>>> task) => ParseObjectCoder.Instance.Decode(task.Result.Item2, this.Decoder, serviceHub).MutatedClone(delegate(MutableObjectState mutableClone)
			{
				mutableClone.IsNew = (task.Result.Item1 == HttpStatusCode.Created);
			}));
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00008E74 File Offset: 0x00007074
		public Task<IObjectState> GetUserAsync(string sessionToken, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.CommandRunner.RunCommandAsync(new ParseCommand("users/me", "GET", sessionToken, null, null), null, null, cancellationToken).OnSuccess((Task<Tuple<HttpStatusCode, IDictionary<string, object>>> task) => ParseObjectCoder.Instance.Decode(task.Result.Item2, this.Decoder, serviceHub));
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00008EC8 File Offset: 0x000070C8
		public Task RequestPasswordResetAsync(string email, CancellationToken cancellationToken = default(CancellationToken))
		{
			IParseCommandRunner commandRunner = this.CommandRunner;
			string relativeUri = "requestPasswordReset";
			string method = "POST";
			string sessionToken = null;
			IList<KeyValuePair<string, string>> headers = null;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["email"] = email;
			return commandRunner.RunCommandAsync(new ParseCommand(relativeUri, method, sessionToken, headers, dictionary), null, null, cancellationToken);
		}

		// Token: 0x04000041 RID: 65
		[CompilerGenerated]
		private readonly IParseCommandRunner <CommandRunner>k__BackingField;

		// Token: 0x04000042 RID: 66
		[CompilerGenerated]
		private readonly IParseDataDecoder <Decoder>k__BackingField;

		// Token: 0x04000043 RID: 67
		[CompilerGenerated]
		private bool <RevocableSessionEnabled>k__BackingField;

		// Token: 0x04000044 RID: 68
		[CompilerGenerated]
		private readonly object <RevocableSessionEnabledMutex>k__BackingField;

		// Token: 0x020000EC RID: 236
		[CompilerGenerated]
		private sealed class <>c__DisplayClass14_0
		{
			// Token: 0x060006A5 RID: 1701 RVA: 0x00014B79 File Offset: 0x00012D79
			public <>c__DisplayClass14_0()
			{
			}

			// Token: 0x060006A6 RID: 1702 RVA: 0x00014B84 File Offset: 0x00012D84
			internal IObjectState <SignUpAsync>b__0(Task<Tuple<HttpStatusCode, IDictionary<string, object>>> task)
			{
				return ParseObjectCoder.Instance.Decode(task.Result.Item2, this.<>4__this.Decoder, this.serviceHub).MutatedClone(new Action<MutableObjectState>(ParseUserController.<>c.<>9.<SignUpAsync>b__14_1));
			}

			// Token: 0x040001E4 RID: 484
			public ParseUserController <>4__this;

			// Token: 0x040001E5 RID: 485
			public IServiceHub serviceHub;
		}

		// Token: 0x020000ED RID: 237
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060006A7 RID: 1703 RVA: 0x00014BDB File Offset: 0x00012DDB
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060006A8 RID: 1704 RVA: 0x00014BE7 File Offset: 0x00012DE7
			public <>c()
			{
			}

			// Token: 0x060006A9 RID: 1705 RVA: 0x00014BEF File Offset: 0x00012DEF
			internal void <SignUpAsync>b__14_1(MutableObjectState mutableClone)
			{
				mutableClone.IsNew = true;
			}

			// Token: 0x040001E6 RID: 486
			public static readonly ParseUserController.<>c <>9 = new ParseUserController.<>c();

			// Token: 0x040001E7 RID: 487
			public static Action<MutableObjectState> <>9__14_1;
		}

		// Token: 0x020000EE RID: 238
		[CompilerGenerated]
		private sealed class <>c__DisplayClass15_0
		{
			// Token: 0x060006AA RID: 1706 RVA: 0x00014BF8 File Offset: 0x00012DF8
			public <>c__DisplayClass15_0()
			{
			}

			// Token: 0x060006AB RID: 1707 RVA: 0x00014C00 File Offset: 0x00012E00
			internal IObjectState <LogInAsync>b__0(Task<Tuple<HttpStatusCode, IDictionary<string, object>>> task)
			{
				ParseUserController.<>c__DisplayClass15_1 CS$<>8__locals1 = new ParseUserController.<>c__DisplayClass15_1();
				CS$<>8__locals1.task = task;
				return ParseObjectCoder.Instance.Decode(CS$<>8__locals1.task.Result.Item2, this.<>4__this.Decoder, this.serviceHub).MutatedClone(new Action<MutableObjectState>(CS$<>8__locals1.<LogInAsync>b__1));
			}

			// Token: 0x040001E8 RID: 488
			public ParseUserController <>4__this;

			// Token: 0x040001E9 RID: 489
			public IServiceHub serviceHub;
		}

		// Token: 0x020000EF RID: 239
		[CompilerGenerated]
		private sealed class <>c__DisplayClass15_1
		{
			// Token: 0x060006AC RID: 1708 RVA: 0x00014C56 File Offset: 0x00012E56
			public <>c__DisplayClass15_1()
			{
			}

			// Token: 0x060006AD RID: 1709 RVA: 0x00014C5E File Offset: 0x00012E5E
			internal void <LogInAsync>b__1(MutableObjectState mutableClone)
			{
				mutableClone.IsNew = (this.task.Result.Item1 == HttpStatusCode.Created);
			}

			// Token: 0x040001EA RID: 490
			public Task<Tuple<HttpStatusCode, IDictionary<string, object>>> task;
		}

		// Token: 0x020000F0 RID: 240
		[CompilerGenerated]
		private sealed class <>c__DisplayClass16_0
		{
			// Token: 0x060006AE RID: 1710 RVA: 0x00014C7D File Offset: 0x00012E7D
			public <>c__DisplayClass16_0()
			{
			}

			// Token: 0x060006AF RID: 1711 RVA: 0x00014C88 File Offset: 0x00012E88
			internal IObjectState <LogInAsync>b__0(Task<Tuple<HttpStatusCode, IDictionary<string, object>>> task)
			{
				ParseUserController.<>c__DisplayClass16_1 CS$<>8__locals1 = new ParseUserController.<>c__DisplayClass16_1();
				CS$<>8__locals1.task = task;
				return ParseObjectCoder.Instance.Decode(CS$<>8__locals1.task.Result.Item2, this.<>4__this.Decoder, this.serviceHub).MutatedClone(new Action<MutableObjectState>(CS$<>8__locals1.<LogInAsync>b__1));
			}

			// Token: 0x040001EB RID: 491
			public ParseUserController <>4__this;

			// Token: 0x040001EC RID: 492
			public IServiceHub serviceHub;
		}

		// Token: 0x020000F1 RID: 241
		[CompilerGenerated]
		private sealed class <>c__DisplayClass16_1
		{
			// Token: 0x060006B0 RID: 1712 RVA: 0x00014CDE File Offset: 0x00012EDE
			public <>c__DisplayClass16_1()
			{
			}

			// Token: 0x060006B1 RID: 1713 RVA: 0x00014CE6 File Offset: 0x00012EE6
			internal void <LogInAsync>b__1(MutableObjectState mutableClone)
			{
				mutableClone.IsNew = (this.task.Result.Item1 == HttpStatusCode.Created);
			}

			// Token: 0x040001ED RID: 493
			public Task<Tuple<HttpStatusCode, IDictionary<string, object>>> task;
		}

		// Token: 0x020000F2 RID: 242
		[CompilerGenerated]
		private sealed class <>c__DisplayClass17_0
		{
			// Token: 0x060006B2 RID: 1714 RVA: 0x00014D05 File Offset: 0x00012F05
			public <>c__DisplayClass17_0()
			{
			}

			// Token: 0x060006B3 RID: 1715 RVA: 0x00014D0D File Offset: 0x00012F0D
			internal IObjectState <GetUserAsync>b__0(Task<Tuple<HttpStatusCode, IDictionary<string, object>>> task)
			{
				return ParseObjectCoder.Instance.Decode(task.Result.Item2, this.<>4__this.Decoder, this.serviceHub);
			}

			// Token: 0x040001EE RID: 494
			public ParseUserController <>4__this;

			// Token: 0x040001EF RID: 495
			public IServiceHub serviceHub;
		}
	}
}
