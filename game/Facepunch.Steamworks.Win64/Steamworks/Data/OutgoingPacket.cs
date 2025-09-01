using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Steamworks.Data
{
	// Token: 0x02000200 RID: 512
	public struct OutgoingPacket
	{
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06001010 RID: 4112 RVA: 0x0001A9A2 File Offset: 0x00018BA2
		// (set) Token: 0x06001011 RID: 4113 RVA: 0x0001A9AA File Offset: 0x00018BAA
		public uint Address
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Address>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Address>k__BackingField = value;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06001012 RID: 4114 RVA: 0x0001A9B3 File Offset: 0x00018BB3
		// (set) Token: 0x06001013 RID: 4115 RVA: 0x0001A9BB File Offset: 0x00018BBB
		public ushort Port
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Port>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Port>k__BackingField = value;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x0001A9C4 File Offset: 0x00018BC4
		// (set) Token: 0x06001015 RID: 4117 RVA: 0x0001A9CC File Offset: 0x00018BCC
		public byte[] Data
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Data>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Data>k__BackingField = value;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x0001A9D5 File Offset: 0x00018BD5
		// (set) Token: 0x06001017 RID: 4119 RVA: 0x0001A9DD File Offset: 0x00018BDD
		public int Size
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Size>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Size>k__BackingField = value;
			}
		}

		// Token: 0x04000C17 RID: 3095
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private uint <Address>k__BackingField;

		// Token: 0x04000C18 RID: 3096
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ushort <Port>k__BackingField;

		// Token: 0x04000C19 RID: 3097
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private byte[] <Data>k__BackingField;

		// Token: 0x04000C1A RID: 3098
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <Size>k__BackingField;
	}
}
