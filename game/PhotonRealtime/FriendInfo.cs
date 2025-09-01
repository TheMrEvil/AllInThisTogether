using System;
using System.Runtime.CompilerServices;

namespace Photon.Realtime
{
	// Token: 0x02000006 RID: 6
	public class FriendInfo
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002EC1 File Offset: 0x000010C1
		[Obsolete("Use UserId.")]
		public string Name
		{
			get
			{
				return this.UserId;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002EC9 File Offset: 0x000010C9
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002ED1 File Offset: 0x000010D1
		public string UserId
		{
			[CompilerGenerated]
			get
			{
				return this.<UserId>k__BackingField;
			}
			[CompilerGenerated]
			protected internal set
			{
				this.<UserId>k__BackingField = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002EDA File Offset: 0x000010DA
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002EE2 File Offset: 0x000010E2
		public bool IsOnline
		{
			[CompilerGenerated]
			get
			{
				return this.<IsOnline>k__BackingField;
			}
			[CompilerGenerated]
			protected internal set
			{
				this.<IsOnline>k__BackingField = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002EEB File Offset: 0x000010EB
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002EF3 File Offset: 0x000010F3
		public string Room
		{
			[CompilerGenerated]
			get
			{
				return this.<Room>k__BackingField;
			}
			[CompilerGenerated]
			protected internal set
			{
				this.<Room>k__BackingField = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002EFC File Offset: 0x000010FC
		public bool IsInRoom
		{
			get
			{
				return this.IsOnline && !string.IsNullOrEmpty(this.Room);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002F16 File Offset: 0x00001116
		public override string ToString()
		{
			return string.Format("{0}\t is: {1}", this.UserId, (!this.IsOnline) ? "offline" : (this.IsInRoom ? "playing" : "on master"));
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002F4B File Offset: 0x0000114B
		public FriendInfo()
		{
		}

		// Token: 0x04000026 RID: 38
		[CompilerGenerated]
		private string <UserId>k__BackingField;

		// Token: 0x04000027 RID: 39
		[CompilerGenerated]
		private bool <IsOnline>k__BackingField;

		// Token: 0x04000028 RID: 40
		[CompilerGenerated]
		private string <Room>k__BackingField;
	}
}
