using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace System.Net.Mime
{
	// Token: 0x02000806 RID: 2054
	internal class MimeMultiPart : MimeBasePart
	{
		// Token: 0x0600416D RID: 16749 RVA: 0x000E18BB File Offset: 0x000DFABB
		internal MimeMultiPart(MimeMultiPartType type)
		{
			this.MimeMultiPartType = type;
		}

		// Token: 0x17000ECF RID: 3791
		// (set) Token: 0x0600416E RID: 16750 RVA: 0x000E18CA File Offset: 0x000DFACA
		internal MimeMultiPartType MimeMultiPartType
		{
			set
			{
				if (value > MimeMultiPartType.Related || value < MimeMultiPartType.Mixed)
				{
					throw new NotSupportedException(value.ToString());
				}
				this.SetType(value);
			}
		}

		// Token: 0x0600416F RID: 16751 RVA: 0x000E18EE File Offset: 0x000DFAEE
		private void SetType(MimeMultiPartType type)
		{
			base.ContentType.MediaType = "multipart/" + type.ToString().ToLower(CultureInfo.InvariantCulture);
			base.ContentType.Boundary = this.GetNextBoundary();
		}

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x06004170 RID: 16752 RVA: 0x000E192D File Offset: 0x000DFB2D
		internal Collection<MimeBasePart> Parts
		{
			get
			{
				if (this._parts == null)
				{
					this._parts = new Collection<MimeBasePart>();
				}
				return this._parts;
			}
		}

		// Token: 0x06004171 RID: 16753 RVA: 0x000E1948 File Offset: 0x000DFB48
		internal void Complete(IAsyncResult result, Exception e)
		{
			MimeMultiPart.MimePartContext mimePartContext = (MimeMultiPart.MimePartContext)result.AsyncState;
			if (mimePartContext._completed)
			{
				ExceptionDispatchInfo.Throw(e);
			}
			try
			{
				mimePartContext._outputStream.Close();
			}
			catch (Exception ex)
			{
				if (e == null)
				{
					e = ex;
				}
			}
			mimePartContext._completed = true;
			mimePartContext._result.InvokeCallback(e);
		}

		// Token: 0x06004172 RID: 16754 RVA: 0x000E19AC File Offset: 0x000DFBAC
		internal void MimeWriterCloseCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimeMultiPart.MimePartContext)result.AsyncState)._completedSynchronously = false;
			try
			{
				this.MimeWriterCloseCallbackHandler(result);
			}
			catch (Exception e)
			{
				this.Complete(result, e);
			}
		}

		// Token: 0x06004173 RID: 16755 RVA: 0x000E19F8 File Offset: 0x000DFBF8
		private void MimeWriterCloseCallbackHandler(IAsyncResult result)
		{
			((MimeWriter)((MimeMultiPart.MimePartContext)result.AsyncState)._writer).EndClose(result);
			this.Complete(result, null);
		}

		// Token: 0x06004174 RID: 16756 RVA: 0x000E1A20 File Offset: 0x000DFC20
		internal void MimePartSentCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimeMultiPart.MimePartContext)result.AsyncState)._completedSynchronously = false;
			try
			{
				this.MimePartSentCallbackHandler(result);
			}
			catch (Exception e)
			{
				this.Complete(result, e);
			}
		}

		// Token: 0x06004175 RID: 16757 RVA: 0x000E1A6C File Offset: 0x000DFC6C
		private void MimePartSentCallbackHandler(IAsyncResult result)
		{
			MimeMultiPart.MimePartContext mimePartContext = (MimeMultiPart.MimePartContext)result.AsyncState;
			mimePartContext._partsEnumerator.Current.EndSend(result);
			if (mimePartContext._partsEnumerator.MoveNext())
			{
				IAsyncResult asyncResult = mimePartContext._partsEnumerator.Current.BeginSend(mimePartContext._writer, this._mimePartSentCallback, this._allowUnicode, mimePartContext);
				if (asyncResult.CompletedSynchronously)
				{
					this.MimePartSentCallbackHandler(asyncResult);
				}
				return;
			}
			IAsyncResult asyncResult2 = ((MimeWriter)mimePartContext._writer).BeginClose(new AsyncCallback(this.MimeWriterCloseCallback), mimePartContext);
			if (asyncResult2.CompletedSynchronously)
			{
				this.MimeWriterCloseCallbackHandler(asyncResult2);
			}
		}

		// Token: 0x06004176 RID: 16758 RVA: 0x000E1B04 File Offset: 0x000DFD04
		internal void ContentStreamCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimeMultiPart.MimePartContext)result.AsyncState)._completedSynchronously = false;
			try
			{
				this.ContentStreamCallbackHandler(result);
			}
			catch (Exception e)
			{
				this.Complete(result, e);
			}
		}

		// Token: 0x06004177 RID: 16759 RVA: 0x000E1B50 File Offset: 0x000DFD50
		private void ContentStreamCallbackHandler(IAsyncResult result)
		{
			MimeMultiPart.MimePartContext mimePartContext = (MimeMultiPart.MimePartContext)result.AsyncState;
			mimePartContext._outputStream = mimePartContext._writer.EndGetContentStream(result);
			mimePartContext._writer = new MimeWriter(mimePartContext._outputStream, base.ContentType.Boundary);
			if (mimePartContext._partsEnumerator.MoveNext())
			{
				MimeBasePart mimeBasePart = mimePartContext._partsEnumerator.Current;
				this._mimePartSentCallback = new AsyncCallback(this.MimePartSentCallback);
				IAsyncResult asyncResult = mimeBasePart.BeginSend(mimePartContext._writer, this._mimePartSentCallback, this._allowUnicode, mimePartContext);
				if (asyncResult.CompletedSynchronously)
				{
					this.MimePartSentCallbackHandler(asyncResult);
				}
				return;
			}
			IAsyncResult asyncResult2 = ((MimeWriter)mimePartContext._writer).BeginClose(new AsyncCallback(this.MimeWriterCloseCallback), mimePartContext);
			if (asyncResult2.CompletedSynchronously)
			{
				this.MimeWriterCloseCallbackHandler(asyncResult2);
			}
		}

		// Token: 0x06004178 RID: 16760 RVA: 0x000E1C18 File Offset: 0x000DFE18
		internal override IAsyncResult BeginSend(BaseWriter writer, AsyncCallback callback, bool allowUnicode, object state)
		{
			this._allowUnicode = allowUnicode;
			base.PrepareHeaders(allowUnicode);
			writer.WriteHeaders(base.Headers, allowUnicode);
			MimeBasePart.MimePartAsyncResult result = new MimeBasePart.MimePartAsyncResult(this, state, callback);
			MimeMultiPart.MimePartContext state2 = new MimeMultiPart.MimePartContext(writer, result, this.Parts.GetEnumerator());
			IAsyncResult asyncResult = writer.BeginGetContentStream(new AsyncCallback(this.ContentStreamCallback), state2);
			if (asyncResult.CompletedSynchronously)
			{
				this.ContentStreamCallbackHandler(asyncResult);
			}
			return result;
		}

		// Token: 0x06004179 RID: 16761 RVA: 0x000E1C84 File Offset: 0x000DFE84
		internal override void Send(BaseWriter writer, bool allowUnicode)
		{
			base.PrepareHeaders(allowUnicode);
			writer.WriteHeaders(base.Headers, allowUnicode);
			Stream contentStream = writer.GetContentStream();
			MimeWriter mimeWriter = new MimeWriter(contentStream, base.ContentType.Boundary);
			foreach (MimeBasePart mimeBasePart in this.Parts)
			{
				mimeBasePart.Send(mimeWriter, allowUnicode);
			}
			mimeWriter.Close();
			contentStream.Close();
		}

		// Token: 0x0600417A RID: 16762 RVA: 0x000E1D0C File Offset: 0x000DFF0C
		internal string GetNextBoundary()
		{
			return "--boundary_" + (Interlocked.Increment(ref MimeMultiPart.s_boundary) - 1).ToString(CultureInfo.InvariantCulture) + "_" + Guid.NewGuid().ToString(null, CultureInfo.InvariantCulture);
		}

		// Token: 0x040027B2 RID: 10162
		private Collection<MimeBasePart> _parts;

		// Token: 0x040027B3 RID: 10163
		private static int s_boundary;

		// Token: 0x040027B4 RID: 10164
		private AsyncCallback _mimePartSentCallback;

		// Token: 0x040027B5 RID: 10165
		private bool _allowUnicode;

		// Token: 0x02000807 RID: 2055
		internal class MimePartContext
		{
			// Token: 0x0600417B RID: 16763 RVA: 0x000E1D54 File Offset: 0x000DFF54
			internal MimePartContext(BaseWriter writer, LazyAsyncResult result, IEnumerator<MimeBasePart> partsEnumerator)
			{
				this._writer = writer;
				this._result = result;
				this._partsEnumerator = partsEnumerator;
			}

			// Token: 0x040027B6 RID: 10166
			internal IEnumerator<MimeBasePart> _partsEnumerator;

			// Token: 0x040027B7 RID: 10167
			internal Stream _outputStream;

			// Token: 0x040027B8 RID: 10168
			internal LazyAsyncResult _result;

			// Token: 0x040027B9 RID: 10169
			internal BaseWriter _writer;

			// Token: 0x040027BA RID: 10170
			internal bool _completed;

			// Token: 0x040027BB RID: 10171
			internal bool _completedSynchronously = true;
		}
	}
}
