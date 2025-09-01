using System;

namespace WebSocketSharp
{
	// Token: 0x02000003 RID: 3
	public class MessageEventArgs : EventArgs
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00003FEC File Offset: 0x000021EC
		internal MessageEventArgs(WebSocketFrame frame)
		{
			this._opcode = frame.Opcode;
			this._rawData = frame.PayloadData.ApplicationData;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004014 File Offset: 0x00002214
		internal MessageEventArgs(Opcode opcode, byte[] rawData)
		{
			bool flag = (long)rawData.Length > (long)PayloadData.MaxLength;
			if (flag)
			{
				throw new WebSocketException(CloseStatusCode.TooBig);
			}
			this._opcode = opcode;
			this._rawData = rawData;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00004050 File Offset: 0x00002250
		internal Opcode Opcode
		{
			get
			{
				return this._opcode;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00004068 File Offset: 0x00002268
		public string Data
		{
			get
			{
				this.setData();
				return this._data;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00004088 File Offset: 0x00002288
		public bool IsBinary
		{
			get
			{
				return this._opcode == Opcode.Binary;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000040A4 File Offset: 0x000022A4
		public bool IsPing
		{
			get
			{
				return this._opcode == Opcode.Ping;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000040C0 File Offset: 0x000022C0
		public bool IsText
		{
			get
			{
				return this._opcode == Opcode.Text;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000040DC File Offset: 0x000022DC
		public byte[] RawData
		{
			get
			{
				this.setData();
				return this._rawData;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000040FC File Offset: 0x000022FC
		private void setData()
		{
			bool dataSet = this._dataSet;
			if (!dataSet)
			{
				bool flag = this._opcode == Opcode.Binary;
				if (flag)
				{
					this._dataSet = true;
				}
				else
				{
					string data;
					bool flag2 = this._rawData.TryGetUTF8DecodedString(out data);
					if (flag2)
					{
						this._data = data;
					}
					this._dataSet = true;
				}
			}
		}

		// Token: 0x04000004 RID: 4
		private string _data;

		// Token: 0x04000005 RID: 5
		private bool _dataSet;

		// Token: 0x04000006 RID: 6
		private Opcode _opcode;

		// Token: 0x04000007 RID: 7
		private byte[] _rawData;
	}
}
