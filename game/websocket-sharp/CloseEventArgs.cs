using System;

namespace WebSocketSharp
{
	// Token: 0x02000004 RID: 4
	public class CloseEventArgs : EventArgs
	{
		// Token: 0x0600006B RID: 107 RVA: 0x0000414C File Offset: 0x0000234C
		internal CloseEventArgs(PayloadData payloadData, bool clean)
		{
			this._payloadData = payloadData;
			this._clean = clean;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004164 File Offset: 0x00002364
		internal CloseEventArgs(ushort code, string reason, bool clean)
		{
			this._payloadData = new PayloadData(code, reason);
			this._clean = clean;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00004184 File Offset: 0x00002384
		public ushort Code
		{
			get
			{
				return this._payloadData.Code;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000041A4 File Offset: 0x000023A4
		public string Reason
		{
			get
			{
				return this._payloadData.Reason;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000041C4 File Offset: 0x000023C4
		public bool WasClean
		{
			get
			{
				return this._clean;
			}
		}

		// Token: 0x04000008 RID: 8
		private bool _clean;

		// Token: 0x04000009 RID: 9
		private PayloadData _payloadData;
	}
}
