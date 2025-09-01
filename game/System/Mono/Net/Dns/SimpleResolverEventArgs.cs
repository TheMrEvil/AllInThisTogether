using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Mono.Net.Dns
{
	// Token: 0x020000C9 RID: 201
	internal class SimpleResolverEventArgs : EventArgs
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060003EC RID: 1004 RVA: 0x0000C6EC File Offset: 0x0000A8EC
		// (remove) Token: 0x060003ED RID: 1005 RVA: 0x0000C724 File Offset: 0x0000A924
		public event EventHandler<SimpleResolverEventArgs> Completed
		{
			[CompilerGenerated]
			add
			{
				EventHandler<SimpleResolverEventArgs> eventHandler = this.Completed;
				EventHandler<SimpleResolverEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<SimpleResolverEventArgs> value2 = (EventHandler<SimpleResolverEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<SimpleResolverEventArgs>>(ref this.Completed, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<SimpleResolverEventArgs> eventHandler = this.Completed;
				EventHandler<SimpleResolverEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<SimpleResolverEventArgs> value2 = (EventHandler<SimpleResolverEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<SimpleResolverEventArgs>>(ref this.Completed, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000C759 File Offset: 0x0000A959
		public SimpleResolverEventArgs()
		{
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000C761 File Offset: 0x0000A961
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x0000C769 File Offset: 0x0000A969
		public ResolverError ResolverError
		{
			[CompilerGenerated]
			get
			{
				return this.<ResolverError>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ResolverError>k__BackingField = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000C772 File Offset: 0x0000A972
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x0000C77A File Offset: 0x0000A97A
		public string ErrorMessage
		{
			[CompilerGenerated]
			get
			{
				return this.<ErrorMessage>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ErrorMessage>k__BackingField = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000C783 File Offset: 0x0000A983
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x0000C78B File Offset: 0x0000A98B
		public string HostName
		{
			[CompilerGenerated]
			get
			{
				return this.<HostName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<HostName>k__BackingField = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0000C794 File Offset: 0x0000A994
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x0000C79C File Offset: 0x0000A99C
		public IPHostEntry HostEntry
		{
			[CompilerGenerated]
			get
			{
				return this.<HostEntry>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<HostEntry>k__BackingField = value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0000C7A5 File Offset: 0x0000A9A5
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x0000C7AD File Offset: 0x0000A9AD
		public object UserToken
		{
			[CompilerGenerated]
			get
			{
				return this.<UserToken>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UserToken>k__BackingField = value;
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000C7B6 File Offset: 0x0000A9B6
		internal void Reset(ResolverAsyncOperation op)
		{
			this.ResolverError = ResolverError.NoError;
			this.ErrorMessage = null;
			this.HostEntry = null;
			this.LastOperation = op;
			this.QueryID = 0;
			this.Retries = 0;
			this.PTRAddress = null;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000C7EC File Offset: 0x0000A9EC
		protected internal void OnCompleted(object sender)
		{
			EventHandler<SimpleResolverEventArgs> completed = this.Completed;
			if (completed != null)
			{
				completed(sender, this);
			}
		}

		// Token: 0x04000385 RID: 901
		[CompilerGenerated]
		private EventHandler<SimpleResolverEventArgs> Completed;

		// Token: 0x04000386 RID: 902
		[CompilerGenerated]
		private ResolverError <ResolverError>k__BackingField;

		// Token: 0x04000387 RID: 903
		[CompilerGenerated]
		private string <ErrorMessage>k__BackingField;

		// Token: 0x04000388 RID: 904
		public ResolverAsyncOperation LastOperation;

		// Token: 0x04000389 RID: 905
		[CompilerGenerated]
		private string <HostName>k__BackingField;

		// Token: 0x0400038A RID: 906
		[CompilerGenerated]
		private IPHostEntry <HostEntry>k__BackingField;

		// Token: 0x0400038B RID: 907
		[CompilerGenerated]
		private object <UserToken>k__BackingField;

		// Token: 0x0400038C RID: 908
		internal ushort QueryID;

		// Token: 0x0400038D RID: 909
		internal ushort Retries;

		// Token: 0x0400038E RID: 910
		internal Timer Timer;

		// Token: 0x0400038F RID: 911
		internal IPAddress PTRAddress;
	}
}
