using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x0200080B RID: 2059
	internal class MimeWriter : BaseWriter
	{
		// Token: 0x06004192 RID: 16786 RVA: 0x000E23D2 File Offset: 0x000E05D2
		internal MimeWriter(Stream stream, string boundary) : base(stream, false)
		{
			if (boundary == null)
			{
				throw new ArgumentNullException("boundary");
			}
			this._boundaryBytes = Encoding.ASCII.GetBytes(boundary);
		}

		// Token: 0x06004193 RID: 16787 RVA: 0x000E2404 File Offset: 0x000E0604
		internal override void WriteHeaders(NameValueCollection headers, bool allowUnicode)
		{
			if (headers == null)
			{
				throw new ArgumentNullException("headers");
			}
			foreach (object obj in headers)
			{
				string name = (string)obj;
				base.WriteHeader(name, headers[name], allowUnicode);
			}
		}

		// Token: 0x06004194 RID: 16788 RVA: 0x000E2470 File Offset: 0x000E0670
		internal IAsyncResult BeginClose(AsyncCallback callback, object state)
		{
			MultiAsyncResult multiAsyncResult = new MultiAsyncResult(this, callback, state);
			this.Close(multiAsyncResult);
			multiAsyncResult.CompleteSequence();
			return multiAsyncResult;
		}

		// Token: 0x06004195 RID: 16789 RVA: 0x000E2494 File Offset: 0x000E0694
		internal void EndClose(IAsyncResult result)
		{
			MultiAsyncResult.End(result);
			this._stream.Close();
		}

		// Token: 0x06004196 RID: 16790 RVA: 0x000E24A8 File Offset: 0x000E06A8
		internal override void Close()
		{
			this.Close(null);
			this._stream.Close();
		}

		// Token: 0x06004197 RID: 16791 RVA: 0x000E24BC File Offset: 0x000E06BC
		private void Close(MultiAsyncResult multiResult)
		{
			this._bufferBuilder.Append(BaseWriter.s_crlf);
			this._bufferBuilder.Append(MimeWriter.s_DASHDASH);
			this._bufferBuilder.Append(this._boundaryBytes);
			this._bufferBuilder.Append(MimeWriter.s_DASHDASH);
			this._bufferBuilder.Append(BaseWriter.s_crlf);
			base.Flush(multiResult);
		}

		// Token: 0x06004198 RID: 16792 RVA: 0x000E2521 File Offset: 0x000E0721
		protected override void OnClose(object sender, EventArgs args)
		{
			if (this._contentStream != sender)
			{
				return;
			}
			this._contentStream.Flush();
			this._contentStream = null;
			this._writeBoundary = true;
			this._isInContent = false;
		}

		// Token: 0x06004199 RID: 16793 RVA: 0x000E2550 File Offset: 0x000E0750
		protected override void CheckBoundary()
		{
			if (this._writeBoundary)
			{
				this._bufferBuilder.Append(BaseWriter.s_crlf);
				this._bufferBuilder.Append(MimeWriter.s_DASHDASH);
				this._bufferBuilder.Append(this._boundaryBytes);
				this._bufferBuilder.Append(BaseWriter.s_crlf);
				this._writeBoundary = false;
			}
		}

		// Token: 0x0600419A RID: 16794 RVA: 0x000E25AD File Offset: 0x000E07AD
		// Note: this type is marked as 'beforefieldinit'.
		static MimeWriter()
		{
		}

		// Token: 0x040027CF RID: 10191
		private static byte[] s_DASHDASH = new byte[]
		{
			45,
			45
		};

		// Token: 0x040027D0 RID: 10192
		private byte[] _boundaryBytes;

		// Token: 0x040027D1 RID: 10193
		private bool _writeBoundary = true;
	}
}
