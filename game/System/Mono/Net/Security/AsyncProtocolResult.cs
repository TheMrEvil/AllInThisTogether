using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace Mono.Net.Security
{
	// Token: 0x0200008A RID: 138
	internal class AsyncProtocolResult
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600021F RID: 543 RVA: 0x000063C0 File Offset: 0x000045C0
		public int UserResult
		{
			[CompilerGenerated]
			get
			{
				return this.<UserResult>k__BackingField;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000220 RID: 544 RVA: 0x000063C8 File Offset: 0x000045C8
		public ExceptionDispatchInfo Error
		{
			[CompilerGenerated]
			get
			{
				return this.<Error>k__BackingField;
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000063D0 File Offset: 0x000045D0
		public AsyncProtocolResult(int result)
		{
			this.UserResult = result;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000063DF File Offset: 0x000045DF
		public AsyncProtocolResult(ExceptionDispatchInfo error)
		{
			this.Error = error;
		}

		// Token: 0x04000206 RID: 518
		[CompilerGenerated]
		private readonly int <UserResult>k__BackingField;

		// Token: 0x04000207 RID: 519
		[CompilerGenerated]
		private readonly ExceptionDispatchInfo <Error>k__BackingField;
	}
}
