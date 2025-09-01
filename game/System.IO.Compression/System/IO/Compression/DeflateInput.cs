using System;
using System.Runtime.CompilerServices;

namespace System.IO.Compression
{
	// Token: 0x02000013 RID: 19
	internal sealed class DeflateInput
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003543 File Offset: 0x00001743
		// (set) Token: 0x0600005E RID: 94 RVA: 0x0000354B File Offset: 0x0000174B
		internal byte[] Buffer
		{
			[CompilerGenerated]
			get
			{
				return this.<Buffer>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Buffer>k__BackingField = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00003554 File Offset: 0x00001754
		// (set) Token: 0x06000060 RID: 96 RVA: 0x0000355C File Offset: 0x0000175C
		internal int Count
		{
			[CompilerGenerated]
			get
			{
				return this.<Count>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Count>k__BackingField = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003565 File Offset: 0x00001765
		// (set) Token: 0x06000062 RID: 98 RVA: 0x0000356D File Offset: 0x0000176D
		internal int StartIndex
		{
			[CompilerGenerated]
			get
			{
				return this.<StartIndex>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<StartIndex>k__BackingField = value;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003576 File Offset: 0x00001776
		internal void ConsumeBytes(int n)
		{
			this.StartIndex += n;
			this.Count -= n;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003594 File Offset: 0x00001794
		internal DeflateInput.InputState DumpState()
		{
			return new DeflateInput.InputState(this.Count, this.StartIndex);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000035A7 File Offset: 0x000017A7
		internal void RestoreState(DeflateInput.InputState state)
		{
			this.Count = state._count;
			this.StartIndex = state._startIndex;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000353B File Offset: 0x0000173B
		public DeflateInput()
		{
		}

		// Token: 0x040000B6 RID: 182
		[CompilerGenerated]
		private byte[] <Buffer>k__BackingField;

		// Token: 0x040000B7 RID: 183
		[CompilerGenerated]
		private int <Count>k__BackingField;

		// Token: 0x040000B8 RID: 184
		[CompilerGenerated]
		private int <StartIndex>k__BackingField;

		// Token: 0x02000014 RID: 20
		internal readonly struct InputState
		{
			// Token: 0x06000067 RID: 103 RVA: 0x000035C1 File Offset: 0x000017C1
			internal InputState(int count, int startIndex)
			{
				this._count = count;
				this._startIndex = startIndex;
			}

			// Token: 0x040000B9 RID: 185
			internal readonly int _count;

			// Token: 0x040000BA RID: 186
			internal readonly int _startIndex;
		}
	}
}
