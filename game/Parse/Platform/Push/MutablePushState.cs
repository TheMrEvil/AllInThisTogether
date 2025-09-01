using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Platform.Push;
using Parse.Infrastructure.Utilities;

namespace Parse.Platform.Push
{
	// Token: 0x02000029 RID: 41
	public class MutablePushState : IPushState
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600021A RID: 538 RVA: 0x000091F8 File Offset: 0x000073F8
		// (set) Token: 0x0600021B RID: 539 RVA: 0x00009200 File Offset: 0x00007400
		public ParseQuery<ParseInstallation> Query
		{
			[CompilerGenerated]
			get
			{
				return this.<Query>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Query>k__BackingField = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00009209 File Offset: 0x00007409
		// (set) Token: 0x0600021D RID: 541 RVA: 0x00009211 File Offset: 0x00007411
		public IEnumerable<string> Channels
		{
			[CompilerGenerated]
			get
			{
				return this.<Channels>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Channels>k__BackingField = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000921A File Offset: 0x0000741A
		// (set) Token: 0x0600021F RID: 543 RVA: 0x00009222 File Offset: 0x00007422
		public DateTime? Expiration
		{
			[CompilerGenerated]
			get
			{
				return this.<Expiration>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Expiration>k__BackingField = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000922B File Offset: 0x0000742B
		// (set) Token: 0x06000221 RID: 545 RVA: 0x00009233 File Offset: 0x00007433
		public TimeSpan? ExpirationInterval
		{
			[CompilerGenerated]
			get
			{
				return this.<ExpirationInterval>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ExpirationInterval>k__BackingField = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000923C File Offset: 0x0000743C
		// (set) Token: 0x06000223 RID: 547 RVA: 0x00009244 File Offset: 0x00007444
		public DateTime? PushTime
		{
			[CompilerGenerated]
			get
			{
				return this.<PushTime>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PushTime>k__BackingField = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000924D File Offset: 0x0000744D
		// (set) Token: 0x06000225 RID: 549 RVA: 0x00009255 File Offset: 0x00007455
		public IDictionary<string, object> Data
		{
			[CompilerGenerated]
			get
			{
				return this.<Data>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Data>k__BackingField = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000925E File Offset: 0x0000745E
		// (set) Token: 0x06000227 RID: 551 RVA: 0x00009266 File Offset: 0x00007466
		public string Alert
		{
			[CompilerGenerated]
			get
			{
				return this.<Alert>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Alert>k__BackingField = value;
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00009270 File Offset: 0x00007470
		public IPushState MutatedClone(Action<MutablePushState> func)
		{
			MutablePushState mutablePushState = this.MutableClone();
			func(mutablePushState);
			return mutablePushState;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000928C File Offset: 0x0000748C
		protected virtual MutablePushState MutableClone()
		{
			return new MutablePushState
			{
				Query = this.Query,
				Channels = ((this.Channels == null) ? null : new List<string>(this.Channels)),
				Expiration = this.Expiration,
				ExpirationInterval = this.ExpirationInterval,
				PushTime = this.PushTime,
				Data = ((this.Data == null) ? null : new Dictionary<string, object>(this.Data)),
				Alert = this.Alert
			};
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00009314 File Offset: 0x00007514
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is MutablePushState))
			{
				return false;
			}
			MutablePushState mutablePushState = obj as MutablePushState;
			return object.Equals(this.Query, mutablePushState.Query) && this.Channels.CollectionsEqual(mutablePushState.Channels) && object.Equals(this.Expiration, mutablePushState.Expiration) && object.Equals(this.ExpirationInterval, mutablePushState.ExpirationInterval) && object.Equals(this.PushTime, mutablePushState.PushTime) && this.Data.CollectionsEqual(mutablePushState.Data) && object.Equals(this.Alert, mutablePushState.Alert);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000093DB File Offset: 0x000075DB
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000093DE File Offset: 0x000075DE
		public MutablePushState()
		{
		}

		// Token: 0x04000049 RID: 73
		[CompilerGenerated]
		private ParseQuery<ParseInstallation> <Query>k__BackingField;

		// Token: 0x0400004A RID: 74
		[CompilerGenerated]
		private IEnumerable<string> <Channels>k__BackingField;

		// Token: 0x0400004B RID: 75
		[CompilerGenerated]
		private DateTime? <Expiration>k__BackingField;

		// Token: 0x0400004C RID: 76
		[CompilerGenerated]
		private TimeSpan? <ExpirationInterval>k__BackingField;

		// Token: 0x0400004D RID: 77
		[CompilerGenerated]
		private DateTime? <PushTime>k__BackingField;

		// Token: 0x0400004E RID: 78
		[CompilerGenerated]
		private IDictionary<string, object> <Data>k__BackingField;

		// Token: 0x0400004F RID: 79
		[CompilerGenerated]
		private string <Alert>k__BackingField;
	}
}
