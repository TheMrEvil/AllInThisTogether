using System;
using System.IO;

namespace System.Xml
{
	// Token: 0x0200022D RID: 557
	internal class XmlRegisteredNonCachedStream : Stream
	{
		// Token: 0x06001505 RID: 5381 RVA: 0x00082F26 File Offset: 0x00081126
		internal XmlRegisteredNonCachedStream(Stream stream, XmlDownloadManager downloadManager, string host)
		{
			this.stream = stream;
			this.downloadManager = downloadManager;
			this.host = host;
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x00082F44 File Offset: 0x00081144
		~XmlRegisteredNonCachedStream()
		{
			if (this.downloadManager != null)
			{
				this.downloadManager.Remove(this.host);
			}
			this.stream = null;
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x00082F8C File Offset: 0x0008118C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.stream != null)
				{
					if (this.downloadManager != null)
					{
						this.downloadManager.Remove(this.host);
					}
					this.stream.Close();
				}
				this.stream = null;
				GC.SuppressFinalize(this);
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x00082FF0 File Offset: 0x000811F0
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.stream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x00083004 File Offset: 0x00081204
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.stream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x00083018 File Offset: 0x00081218
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this.stream.EndRead(asyncResult);
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x00083026 File Offset: 0x00081226
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.stream.EndWrite(asyncResult);
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x00083034 File Offset: 0x00081234
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x00083041 File Offset: 0x00081241
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.stream.Read(buffer, offset, count);
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x00083051 File Offset: 0x00081251
		public override int ReadByte()
		{
			return this.stream.ReadByte();
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0008305E File Offset: 0x0008125E
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.stream.Seek(offset, origin);
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x0008306D File Offset: 0x0008126D
		public override void SetLength(long value)
		{
			this.stream.SetLength(value);
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x0008307B File Offset: 0x0008127B
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x0008308B File Offset: 0x0008128B
		public override void WriteByte(byte value)
		{
			this.stream.WriteByte(value);
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001513 RID: 5395 RVA: 0x00083099 File Offset: 0x00081299
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001514 RID: 5396 RVA: 0x000830A6 File Offset: 0x000812A6
		public override bool CanSeek
		{
			get
			{
				return this.stream.CanSeek;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001515 RID: 5397 RVA: 0x000830B3 File Offset: 0x000812B3
		public override bool CanWrite
		{
			get
			{
				return this.stream.CanWrite;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001516 RID: 5398 RVA: 0x000830C0 File Offset: 0x000812C0
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x000830CD File Offset: 0x000812CD
		// (set) Token: 0x06001518 RID: 5400 RVA: 0x000830DA File Offset: 0x000812DA
		public override long Position
		{
			get
			{
				return this.stream.Position;
			}
			set
			{
				this.stream.Position = value;
			}
		}

		// Token: 0x040012CA RID: 4810
		protected Stream stream;

		// Token: 0x040012CB RID: 4811
		private XmlDownloadManager downloadManager;

		// Token: 0x040012CC RID: 4812
		private string host;
	}
}
