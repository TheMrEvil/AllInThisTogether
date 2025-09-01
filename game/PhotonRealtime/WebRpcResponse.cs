using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;

namespace Photon.Realtime
{
	// Token: 0x02000040 RID: 64
	public class WebRpcResponse
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000B428 File Offset: 0x00009628
		// (set) Token: 0x0600020F RID: 527 RVA: 0x0000B430 File Offset: 0x00009630
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000B439 File Offset: 0x00009639
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000B441 File Offset: 0x00009641
		public int ResultCode
		{
			[CompilerGenerated]
			get
			{
				return this.<ResultCode>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ResultCode>k__BackingField = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000B44A File Offset: 0x0000964A
		[Obsolete("Use ResultCode instead")]
		public int ReturnCode
		{
			get
			{
				return this.ResultCode;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000B452 File Offset: 0x00009652
		// (set) Token: 0x06000214 RID: 532 RVA: 0x0000B45A File Offset: 0x0000965A
		public string Message
		{
			[CompilerGenerated]
			get
			{
				return this.<Message>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Message>k__BackingField = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000B463 File Offset: 0x00009663
		[Obsolete("Use Message instead")]
		public string DebugMessage
		{
			get
			{
				return this.Message;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000B46B File Offset: 0x0000966B
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000B473 File Offset: 0x00009673
		public Dictionary<string, object> Parameters
		{
			[CompilerGenerated]
			get
			{
				return this.<Parameters>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Parameters>k__BackingField = value;
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000B47C File Offset: 0x0000967C
		public WebRpcResponse(OperationResponse response)
		{
			object obj;
			if (response.Parameters.TryGetValue(209, out obj))
			{
				this.Name = (obj as string);
			}
			this.ResultCode = -1;
			if (response.Parameters.TryGetValue(207, out obj))
			{
				this.ResultCode = (int)((byte)obj);
			}
			if (response.Parameters.TryGetValue(208, out obj))
			{
				this.Parameters = (obj as Dictionary<string, object>);
			}
			if (response.Parameters.TryGetValue(206, out obj))
			{
				this.Message = (obj as string);
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000B518 File Offset: 0x00009718
		public string ToStringFull()
		{
			return string.Format("{0}={2}: {1} \"{3}\"", new object[]
			{
				this.Name,
				SupportClass.DictionaryToString(this.Parameters, true),
				this.ResultCode,
				this.Message
			});
		}

		// Token: 0x04000211 RID: 529
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x04000212 RID: 530
		[CompilerGenerated]
		private int <ResultCode>k__BackingField;

		// Token: 0x04000213 RID: 531
		[CompilerGenerated]
		private string <Message>k__BackingField;

		// Token: 0x04000214 RID: 532
		[CompilerGenerated]
		private Dictionary<string, object> <Parameters>k__BackingField;
	}
}
