using System;
using System.IO;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000575 RID: 1397
	internal class ClosableStream : DelegatedStream
	{
		// Token: 0x06002D24 RID: 11556 RVA: 0x0009AE83 File Offset: 0x00099083
		internal ClosableStream(Stream stream, EventHandler onClose) : base(stream)
		{
			this._onClose = onClose;
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x0009AE93 File Offset: 0x00099093
		public override void Close()
		{
			if (Interlocked.Increment(ref this._closed) == 1)
			{
				EventHandler onClose = this._onClose;
				if (onClose == null)
				{
					return;
				}
				onClose(this, new EventArgs());
			}
		}

		// Token: 0x04001894 RID: 6292
		private readonly EventHandler _onClose;

		// Token: 0x04001895 RID: 6293
		private int _closed;
	}
}
