using System;
using System.IO;

namespace System.Net
{
	// Token: 0x02000659 RID: 1625
	internal sealed class FileWebStream : FileStream, ICloseEx
	{
		// Token: 0x06003345 RID: 13125 RVA: 0x000B2EA4 File Offset: 0x000B10A4
		public FileWebStream(FileWebRequest request, string path, FileMode mode, FileAccess access, FileShare sharing) : base(path, mode, access, sharing)
		{
			this.m_request = request;
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x000B2EB9 File Offset: 0x000B10B9
		public FileWebStream(FileWebRequest request, string path, FileMode mode, FileAccess access, FileShare sharing, int length, bool async) : base(path, mode, access, sharing, length, async)
		{
			this.m_request = request;
		}

		// Token: 0x06003347 RID: 13127 RVA: 0x000B2ED4 File Offset: 0x000B10D4
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.m_request != null)
				{
					this.m_request.UnblockReader();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x000B2F14 File Offset: 0x000B1114
		void ICloseEx.CloseEx(CloseExState closeState)
		{
			if ((closeState & CloseExState.Abort) != CloseExState.Normal)
			{
				this.SafeFileHandle.Close();
				return;
			}
			this.Close();
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x000B2F30 File Offset: 0x000B1130
		public override int Read(byte[] buffer, int offset, int size)
		{
			this.CheckError();
			int result;
			try
			{
				result = base.Read(buffer, offset, size);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return result;
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x000B2F6C File Offset: 0x000B116C
		public override void Write(byte[] buffer, int offset, int size)
		{
			this.CheckError();
			try
			{
				base.Write(buffer, offset, size);
			}
			catch
			{
				this.CheckError();
				throw;
			}
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x000B2FA4 File Offset: 0x000B11A4
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.CheckError();
			IAsyncResult result;
			try
			{
				result = base.BeginRead(buffer, offset, size, callback, state);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return result;
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x000B2FE4 File Offset: 0x000B11E4
		public override int EndRead(IAsyncResult ar)
		{
			int result;
			try
			{
				result = base.EndRead(ar);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return result;
		}

		// Token: 0x0600334D RID: 13133 RVA: 0x000B3018 File Offset: 0x000B1218
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.CheckError();
			IAsyncResult result;
			try
			{
				result = base.BeginWrite(buffer, offset, size, callback, state);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return result;
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x000B3058 File Offset: 0x000B1258
		public override void EndWrite(IAsyncResult ar)
		{
			try
			{
				base.EndWrite(ar);
			}
			catch
			{
				this.CheckError();
				throw;
			}
		}

		// Token: 0x0600334F RID: 13135 RVA: 0x000B3088 File Offset: 0x000B1288
		private void CheckError()
		{
			if (this.m_request.Aborted)
			{
				throw new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
			}
		}

		// Token: 0x04001E11 RID: 7697
		private FileWebRequest m_request;
	}
}
