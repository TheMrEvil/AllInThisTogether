using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Platform.Push;
using Parse.Platform.Push;

namespace Parse
{
	// Token: 0x02000010 RID: 16
	public class ParsePush
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000051E2 File Offset: 0x000033E2
		private object Mutex
		{
			[CompilerGenerated]
			get
			{
				return this.<Mutex>k__BackingField;
			}
		} = new object();

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000051EA File Offset: 0x000033EA
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x000051F2 File Offset: 0x000033F2
		private IPushState State
		{
			[CompilerGenerated]
			get
			{
				return this.<State>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<State>k__BackingField = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000051FB File Offset: 0x000033FB
		private IServiceHub Services
		{
			[CompilerGenerated]
			get
			{
				return this.<Services>k__BackingField;
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005203 File Offset: 0x00003403
		public ParsePush(IServiceHub serviceHub)
		{
			this.Services = (serviceHub ?? ParseClient.Instance);
			this.State = new MutablePushState
			{
				Query = this.Services.GetInstallationQuery()
			};
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00005242 File Offset: 0x00003442
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00005250 File Offset: 0x00003450
		public ParseQuery<ParseInstallation> Query
		{
			get
			{
				return this.State.Query;
			}
			set
			{
				this.MutateState(delegate(MutablePushState state)
				{
					if (state.Channels != null && value != null && value.GetConstraint("channels") != null)
					{
						throw new InvalidOperationException("A push may not have both Channels and a Query with a channels constraint.");
					}
					state.Query = value;
				});
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000CD RID: 205 RVA: 0x0000527C File Offset: 0x0000347C
		// (set) Token: 0x060000CE RID: 206 RVA: 0x0000528C File Offset: 0x0000348C
		public IEnumerable<string> Channels
		{
			get
			{
				return this.State.Channels;
			}
			set
			{
				this.MutateState(delegate(MutablePushState state)
				{
					if (value != null && state.Query != null && state.Query.GetConstraint("channels") != null)
					{
						throw new InvalidOperationException("A push may not have both Channels and a Query with a channels constraint.");
					}
					state.Channels = value;
				});
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000052B8 File Offset: 0x000034B8
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x000052C8 File Offset: 0x000034C8
		public DateTime? Expiration
		{
			get
			{
				return this.State.Expiration;
			}
			set
			{
				this.MutateState(delegate(MutablePushState state)
				{
					if (state.ExpirationInterval != null)
					{
						throw new InvalidOperationException("Cannot set Expiration after setting ExpirationInterval.");
					}
					state.Expiration = value;
				});
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x000052F4 File Offset: 0x000034F4
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00005304 File Offset: 0x00003504
		public DateTime? PushTime
		{
			get
			{
				return this.State.PushTime;
			}
			set
			{
				this.MutateState(delegate(MutablePushState state)
				{
					DateTime now = DateTime.Now;
					if (value < now || value > now.AddDays(14.0))
					{
						throw new InvalidOperationException("Cannot set PushTime in the past or more than two weeks later than now.");
					}
					state.PushTime = value;
				});
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00005330 File Offset: 0x00003530
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00005340 File Offset: 0x00003540
		public TimeSpan? ExpirationInterval
		{
			get
			{
				return this.State.ExpirationInterval;
			}
			set
			{
				this.MutateState(delegate(MutablePushState state)
				{
					if (state.Expiration != null)
					{
						throw new InvalidOperationException("Cannot set ExpirationInterval after setting Expiration.");
					}
					state.ExpirationInterval = value;
				});
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x0000536C File Offset: 0x0000356C
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x0000537C File Offset: 0x0000357C
		public IDictionary<string, object> Data
		{
			get
			{
				return this.State.Data;
			}
			set
			{
				this.MutateState(delegate(MutablePushState state)
				{
					if (state.Alert != null && value != null)
					{
						throw new InvalidOperationException("A push may not have both an Alert and Data.");
					}
					state.Data = value;
				});
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x000053A8 File Offset: 0x000035A8
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x000053B8 File Offset: 0x000035B8
		public string Alert
		{
			get
			{
				return this.State.Alert;
			}
			set
			{
				this.MutateState(delegate(MutablePushState state)
				{
					if (state.Data != null && value != null)
					{
						throw new InvalidOperationException("A push may not have both an Alert and Data.");
					}
					state.Alert = value;
				});
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000053E4 File Offset: 0x000035E4
		internal IDictionary<string, object> Encode()
		{
			return ParsePushEncoder.Instance.Encode(this.State);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000053F8 File Offset: 0x000035F8
		private void MutateState(Action<MutablePushState> func)
		{
			object mutex = this.Mutex;
			lock (mutex)
			{
				this.State = this.State.MutatedClone(func);
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005444 File Offset: 0x00003644
		public Task SendAsync()
		{
			return this.SendAsync(CancellationToken.None);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005451 File Offset: 0x00003651
		public Task SendAsync(CancellationToken cancellationToken)
		{
			return this.Services.PushController.SendPushNotificationAsync(this.State, this.Services, cancellationToken);
		}

		// Token: 0x04000022 RID: 34
		[CompilerGenerated]
		private readonly object <Mutex>k__BackingField;

		// Token: 0x04000023 RID: 35
		[CompilerGenerated]
		private IPushState <State>k__BackingField;

		// Token: 0x04000024 RID: 36
		[CompilerGenerated]
		private readonly IServiceHub <Services>k__BackingField;

		// Token: 0x020000B1 RID: 177
		[CompilerGenerated]
		private sealed class <>c__DisplayClass13_0
		{
			// Token: 0x060005F4 RID: 1524 RVA: 0x0001314B File Offset: 0x0001134B
			public <>c__DisplayClass13_0()
			{
			}

			// Token: 0x060005F5 RID: 1525 RVA: 0x00013153 File Offset: 0x00011353
			internal void <set_Query>b__0(MutablePushState state)
			{
				if (state.Channels != null && this.value != null && this.value.GetConstraint("channels") != null)
				{
					throw new InvalidOperationException("A push may not have both Channels and a Query with a channels constraint.");
				}
				state.Query = this.value;
			}

			// Token: 0x0400013C RID: 316
			public ParseQuery<ParseInstallation> value;
		}

		// Token: 0x020000B2 RID: 178
		[CompilerGenerated]
		private sealed class <>c__DisplayClass16_0
		{
			// Token: 0x060005F6 RID: 1526 RVA: 0x0001318E File Offset: 0x0001138E
			public <>c__DisplayClass16_0()
			{
			}

			// Token: 0x060005F7 RID: 1527 RVA: 0x00013196 File Offset: 0x00011396
			internal void <set_Channels>b__0(MutablePushState state)
			{
				if (this.value != null && state.Query != null && state.Query.GetConstraint("channels") != null)
				{
					throw new InvalidOperationException("A push may not have both Channels and a Query with a channels constraint.");
				}
				state.Channels = this.value;
			}

			// Token: 0x0400013D RID: 317
			public IEnumerable<string> value;
		}

		// Token: 0x020000B3 RID: 179
		[CompilerGenerated]
		private sealed class <>c__DisplayClass19_0
		{
			// Token: 0x060005F8 RID: 1528 RVA: 0x000131D1 File Offset: 0x000113D1
			public <>c__DisplayClass19_0()
			{
			}

			// Token: 0x060005F9 RID: 1529 RVA: 0x000131DC File Offset: 0x000113DC
			internal void <set_Expiration>b__0(MutablePushState state)
			{
				if (state.ExpirationInterval != null)
				{
					throw new InvalidOperationException("Cannot set Expiration after setting ExpirationInterval.");
				}
				state.Expiration = this.value;
			}

			// Token: 0x0400013E RID: 318
			public DateTime? value;
		}

		// Token: 0x020000B4 RID: 180
		[CompilerGenerated]
		private sealed class <>c__DisplayClass22_0
		{
			// Token: 0x060005FA RID: 1530 RVA: 0x00013210 File Offset: 0x00011410
			public <>c__DisplayClass22_0()
			{
			}

			// Token: 0x060005FB RID: 1531 RVA: 0x00013218 File Offset: 0x00011418
			internal void <set_PushTime>b__0(MutablePushState state)
			{
				DateTime now = DateTime.Now;
				if (this.value < now || this.value > now.AddDays(14.0))
				{
					throw new InvalidOperationException("Cannot set PushTime in the past or more than two weeks later than now.");
				}
				state.PushTime = this.value;
			}

			// Token: 0x0400013F RID: 319
			public DateTime? value;
		}

		// Token: 0x020000B5 RID: 181
		[CompilerGenerated]
		private sealed class <>c__DisplayClass25_0
		{
			// Token: 0x060005FC RID: 1532 RVA: 0x00013299 File Offset: 0x00011499
			public <>c__DisplayClass25_0()
			{
			}

			// Token: 0x060005FD RID: 1533 RVA: 0x000132A4 File Offset: 0x000114A4
			internal void <set_ExpirationInterval>b__0(MutablePushState state)
			{
				if (state.Expiration != null)
				{
					throw new InvalidOperationException("Cannot set ExpirationInterval after setting Expiration.");
				}
				state.ExpirationInterval = this.value;
			}

			// Token: 0x04000140 RID: 320
			public TimeSpan? value;
		}

		// Token: 0x020000B6 RID: 182
		[CompilerGenerated]
		private sealed class <>c__DisplayClass28_0
		{
			// Token: 0x060005FE RID: 1534 RVA: 0x000132D8 File Offset: 0x000114D8
			public <>c__DisplayClass28_0()
			{
			}

			// Token: 0x060005FF RID: 1535 RVA: 0x000132E0 File Offset: 0x000114E0
			internal void <set_Data>b__0(MutablePushState state)
			{
				if (state.Alert != null && this.value != null)
				{
					throw new InvalidOperationException("A push may not have both an Alert and Data.");
				}
				state.Data = this.value;
			}

			// Token: 0x04000141 RID: 321
			public IDictionary<string, object> value;
		}

		// Token: 0x020000B7 RID: 183
		[CompilerGenerated]
		private sealed class <>c__DisplayClass31_0
		{
			// Token: 0x06000600 RID: 1536 RVA: 0x00013309 File Offset: 0x00011509
			public <>c__DisplayClass31_0()
			{
			}

			// Token: 0x06000601 RID: 1537 RVA: 0x00013311 File Offset: 0x00011511
			internal void <set_Alert>b__0(MutablePushState state)
			{
				if (state.Data != null && this.value != null)
				{
					throw new InvalidOperationException("A push may not have both an Alert and Data.");
				}
				state.Alert = this.value;
			}

			// Token: 0x04000142 RID: 322
			public string value;
		}
	}
}
