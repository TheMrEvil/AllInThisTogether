using System;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x02000804 RID: 2052
	internal class MimeBasePart
	{
		// Token: 0x06004159 RID: 16729 RVA: 0x0000219B File Offset: 0x0000039B
		internal MimeBasePart()
		{
		}

		// Token: 0x0600415A RID: 16730 RVA: 0x000E1439 File Offset: 0x000DF639
		internal static bool ShouldUseBase64Encoding(Encoding encoding)
		{
			return encoding == Encoding.Unicode || encoding == Encoding.UTF8 || encoding == Encoding.UTF32 || encoding == Encoding.BigEndianUnicode;
		}

		// Token: 0x0600415B RID: 16731 RVA: 0x000E145D File Offset: 0x000DF65D
		internal static string EncodeHeaderValue(string value, Encoding encoding, bool base64Encoding)
		{
			return MimeBasePart.EncodeHeaderValue(value, encoding, base64Encoding, 0);
		}

		// Token: 0x0600415C RID: 16732 RVA: 0x000E1468 File Offset: 0x000DF668
		internal static string EncodeHeaderValue(string value, Encoding encoding, bool base64Encoding, int headerLength)
		{
			if (MimeBasePart.IsAscii(value, false))
			{
				return value;
			}
			if (encoding == null)
			{
				encoding = Encoding.GetEncoding("utf-8");
			}
			IEncodableStream encoderForHeader = new EncodedStreamFactory().GetEncoderForHeader(encoding, base64Encoding, headerLength);
			byte[] bytes = encoding.GetBytes(value);
			encoderForHeader.EncodeBytes(bytes, 0, bytes.Length);
			return encoderForHeader.GetEncodedString();
		}

		// Token: 0x0600415D RID: 16733 RVA: 0x000E14B8 File Offset: 0x000DF6B8
		internal static string DecodeHeaderValue(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			string text = string.Empty;
			string[] array = value.Split(MimeBasePart.s_headerValueSplitChars, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(MimeBasePart.s_questionMarkSplitChars);
				if (array2.Length != 5 || array2[0] != "=" || array2[4] != "=")
				{
					return value;
				}
				string name = array2[1];
				bool useBase64Encoding = array2[2] == "B";
				byte[] bytes = Encoding.ASCII.GetBytes(array2[3]);
				int count = new EncodedStreamFactory().GetEncoderForHeader(Encoding.GetEncoding(name), useBase64Encoding, 0).DecodeBytes(bytes, 0, bytes.Length);
				Encoding encoding = Encoding.GetEncoding(name);
				text += encoding.GetString(bytes, 0, count);
			}
			return text;
		}

		// Token: 0x0600415E RID: 16734 RVA: 0x000E1590 File Offset: 0x000DF790
		internal static Encoding DecodeEncoding(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return null;
			}
			string[] array = value.Split(MimeBasePart.s_decodeEncodingSplitChars);
			if (array.Length < 5 || array[0] != "=" || array[4] != "=")
			{
				return null;
			}
			return Encoding.GetEncoding(array[1]);
		}

		// Token: 0x0600415F RID: 16735 RVA: 0x000E15E4 File Offset: 0x000DF7E4
		internal static bool IsAscii(string value, bool permitCROrLF)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			foreach (char c in value)
			{
				if (c > '\u007f')
				{
					return false;
				}
				if (!permitCROrLF && (c == '\r' || c == '\n'))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x06004160 RID: 16736 RVA: 0x000E1631 File Offset: 0x000DF831
		// (set) Token: 0x06004161 RID: 16737 RVA: 0x000E1644 File Offset: 0x000DF844
		internal string ContentID
		{
			get
			{
				return this.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentID)];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.Headers.Remove(MailHeaderInfo.GetString(MailHeaderID.ContentID));
					return;
				}
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentID)] = value;
			}
		}

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x06004162 RID: 16738 RVA: 0x000E1672 File Offset: 0x000DF872
		// (set) Token: 0x06004163 RID: 16739 RVA: 0x000E1685 File Offset: 0x000DF885
		internal string ContentLocation
		{
			get
			{
				return this.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentLocation)];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.Headers.Remove(MailHeaderInfo.GetString(MailHeaderID.ContentLocation));
					return;
				}
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentLocation)] = value;
			}
		}

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06004164 RID: 16740 RVA: 0x000E16B4 File Offset: 0x000DF8B4
		internal NameValueCollection Headers
		{
			get
			{
				if (this._headers == null)
				{
					this._headers = new HeaderCollection();
				}
				if (this._contentType == null)
				{
					this._contentType = new ContentType();
				}
				this._contentType.PersistIfNeeded(this._headers, false);
				if (this._contentDisposition != null)
				{
					this._contentDisposition.PersistIfNeeded(this._headers, false);
				}
				return this._headers;
			}
		}

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x06004165 RID: 16741 RVA: 0x000E171C File Offset: 0x000DF91C
		// (set) Token: 0x06004166 RID: 16742 RVA: 0x000E1741 File Offset: 0x000DF941
		internal ContentType ContentType
		{
			get
			{
				ContentType result;
				if ((result = this._contentType) == null)
				{
					result = (this._contentType = new ContentType());
				}
				return result;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._contentType = value;
				this._contentType.PersistIfNeeded((HeaderCollection)this.Headers, true);
			}
		}

		// Token: 0x06004167 RID: 16743 RVA: 0x000E1770 File Offset: 0x000DF970
		internal void PrepareHeaders(bool allowUnicode)
		{
			this._contentType.PersistIfNeeded((HeaderCollection)this.Headers, false);
			this._headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentType), this._contentType.Encode(allowUnicode));
			if (this._contentDisposition != null)
			{
				this._contentDisposition.PersistIfNeeded((HeaderCollection)this.Headers, false);
				this._headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentDisposition), this._contentDisposition.Encode(allowUnicode));
			}
		}

		// Token: 0x06004168 RID: 16744 RVA: 0x0000829A File Offset: 0x0000649A
		internal virtual void Send(BaseWriter writer, bool allowUnicode)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004169 RID: 16745 RVA: 0x0000829A File Offset: 0x0000649A
		internal virtual IAsyncResult BeginSend(BaseWriter writer, AsyncCallback callback, bool allowUnicode, object state)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600416A RID: 16746 RVA: 0x000E17F0 File Offset: 0x000DF9F0
		internal void EndSend(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			LazyAsyncResult lazyAsyncResult = asyncResult as MimeBasePart.MimePartAsyncResult;
			if (lazyAsyncResult == null || lazyAsyncResult.AsyncObject != this)
			{
				throw new ArgumentException("The IAsyncResult object was not returned from the corresponding asynchronous method on this class.", "asyncResult");
			}
			if (lazyAsyncResult.EndCalled)
			{
				throw new InvalidOperationException(SR.Format("{0} can only be called once for each asynchronous operation.", "EndSend"));
			}
			lazyAsyncResult.InternalWaitForCompletion();
			lazyAsyncResult.EndCalled = true;
			if (lazyAsyncResult.Result is Exception)
			{
				throw (Exception)lazyAsyncResult.Result;
			}
		}

		// Token: 0x0600416B RID: 16747 RVA: 0x000E1872 File Offset: 0x000DFA72
		// Note: this type is marked as 'beforefieldinit'.
		static MimeBasePart()
		{
		}

		// Token: 0x040027AB RID: 10155
		internal const string DefaultCharSet = "utf-8";

		// Token: 0x040027AC RID: 10156
		private static readonly char[] s_decodeEncodingSplitChars = new char[]
		{
			'?',
			'\r',
			'\n'
		};

		// Token: 0x040027AD RID: 10157
		protected ContentType _contentType;

		// Token: 0x040027AE RID: 10158
		protected ContentDisposition _contentDisposition;

		// Token: 0x040027AF RID: 10159
		private HeaderCollection _headers;

		// Token: 0x040027B0 RID: 10160
		private static readonly char[] s_headerValueSplitChars = new char[]
		{
			'\r',
			'\n',
			' '
		};

		// Token: 0x040027B1 RID: 10161
		private static readonly char[] s_questionMarkSplitChars = new char[]
		{
			'?'
		};

		// Token: 0x02000805 RID: 2053
		internal class MimePartAsyncResult : LazyAsyncResult
		{
			// Token: 0x0600416C RID: 16748 RVA: 0x000E18B0 File Offset: 0x000DFAB0
			internal MimePartAsyncResult(MimeBasePart part, object state, AsyncCallback callback) : base(part, state, callback)
			{
			}
		}
	}
}
