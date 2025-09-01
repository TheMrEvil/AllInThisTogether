using System;
using System.Runtime.CompilerServices;

namespace Mono.Net.Security
{
	// Token: 0x02000090 RID: 144
	internal abstract class AsyncReadOrWriteRequest : AsyncProtocolRequest
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00006AB7 File Offset: 0x00004CB7
		protected BufferOffsetSize UserBuffer
		{
			[CompilerGenerated]
			get
			{
				return this.<UserBuffer>k__BackingField;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00006ABF File Offset: 0x00004CBF
		// (set) Token: 0x0600023C RID: 572 RVA: 0x00006AC7 File Offset: 0x00004CC7
		protected int CurrentSize
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentSize>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CurrentSize>k__BackingField = value;
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00006AD0 File Offset: 0x00004CD0
		public AsyncReadOrWriteRequest(MobileAuthenticatedStream parent, bool sync, byte[] buffer, int offset, int size) : base(parent, sync)
		{
			this.UserBuffer = new BufferOffsetSize(buffer, offset, size);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00006AEA File Offset: 0x00004CEA
		public override string ToString()
		{
			return string.Format("[{0}: {1}]", base.Name, this.UserBuffer);
		}

		// Token: 0x04000224 RID: 548
		[CompilerGenerated]
		private readonly BufferOffsetSize <UserBuffer>k__BackingField;

		// Token: 0x04000225 RID: 549
		[CompilerGenerated]
		private int <CurrentSize>k__BackingField;
	}
}
