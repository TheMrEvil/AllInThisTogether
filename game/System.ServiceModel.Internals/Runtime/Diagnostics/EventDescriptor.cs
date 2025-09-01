using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.Diagnostics
{
	// Token: 0x02000045 RID: 69
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	internal struct EventDescriptor
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x0000F1F0 File Offset: 0x0000D3F0
		public EventDescriptor(int id, byte version, byte channel, byte level, byte opcode, int task, long keywords)
		{
			if (id < 0)
			{
				throw Fx.Exception.ArgumentOutOfRange("id", id, "Value Must Be Non Negative");
			}
			if (id > 65535)
			{
				throw Fx.Exception.ArgumentOutOfRange("id", id, string.Empty);
			}
			this.m_id = (ushort)id;
			this.m_version = version;
			this.m_channel = channel;
			this.m_level = level;
			this.m_opcode = opcode;
			this.m_keywords = keywords;
			if (task < 0)
			{
				throw Fx.Exception.ArgumentOutOfRange("task", task, "Value Must Be Non Negative");
			}
			if (task > 65535)
			{
				throw Fx.Exception.ArgumentOutOfRange("task", task, string.Empty);
			}
			this.m_task = (ushort)task;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000F2BC File Offset: 0x0000D4BC
		public int EventId
		{
			get
			{
				return (int)this.m_id;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000F2C4 File Offset: 0x0000D4C4
		public byte Version
		{
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000F2CC File Offset: 0x0000D4CC
		public byte Channel
		{
			get
			{
				return this.m_channel;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000F2D4 File Offset: 0x0000D4D4
		public byte Level
		{
			get
			{
				return this.m_level;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000F2DC File Offset: 0x0000D4DC
		public byte Opcode
		{
			get
			{
				return this.m_opcode;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000F2E4 File Offset: 0x0000D4E4
		public int Task
		{
			get
			{
				return (int)this.m_task;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000F2EC File Offset: 0x0000D4EC
		public long Keywords
		{
			get
			{
				return this.m_keywords;
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000F2F4 File Offset: 0x0000D4F4
		public override bool Equals(object obj)
		{
			return obj is EventDescriptor && this.Equals((EventDescriptor)obj);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000F30C File Offset: 0x0000D50C
		public override int GetHashCode()
		{
			return (int)(this.m_id ^ (ushort)this.m_version ^ (ushort)this.m_channel ^ (ushort)this.m_level ^ (ushort)this.m_opcode ^ this.m_task) ^ (int)this.m_keywords;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000F340 File Offset: 0x0000D540
		public bool Equals(EventDescriptor other)
		{
			return this.m_id == other.m_id && this.m_version == other.m_version && this.m_channel == other.m_channel && this.m_level == other.m_level && this.m_opcode == other.m_opcode && this.m_task == other.m_task && this.m_keywords == other.m_keywords;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000F3B2 File Offset: 0x0000D5B2
		public static bool operator ==(EventDescriptor event1, EventDescriptor event2)
		{
			return event1.Equals(event2);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000F3BC File Offset: 0x0000D5BC
		public static bool operator !=(EventDescriptor event1, EventDescriptor event2)
		{
			return !event1.Equals(event2);
		}

		// Token: 0x0400016A RID: 362
		[FieldOffset(0)]
		private ushort m_id;

		// Token: 0x0400016B RID: 363
		[FieldOffset(2)]
		private byte m_version;

		// Token: 0x0400016C RID: 364
		[FieldOffset(3)]
		private byte m_channel;

		// Token: 0x0400016D RID: 365
		[FieldOffset(4)]
		private byte m_level;

		// Token: 0x0400016E RID: 366
		[FieldOffset(5)]
		private byte m_opcode;

		// Token: 0x0400016F RID: 367
		[FieldOffset(6)]
		private ushort m_task;

		// Token: 0x04000170 RID: 368
		[FieldOffset(8)]
		private long m_keywords;
	}
}
