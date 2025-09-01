using System;

namespace WebSocketSharp
{
	// Token: 0x02000006 RID: 6
	public class ErrorEventArgs : EventArgs
	{
		// Token: 0x06000070 RID: 112 RVA: 0x000041DC File Offset: 0x000023DC
		internal ErrorEventArgs(string message) : this(message, null)
		{
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000041E8 File Offset: 0x000023E8
		internal ErrorEventArgs(string message, Exception exception)
		{
			this._message = message;
			this._exception = exception;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00004200 File Offset: 0x00002400
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00004218 File Offset: 0x00002418
		public string Message
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x0400000D RID: 13
		private Exception _exception;

		// Token: 0x0400000E RID: 14
		private string _message;
	}
}
