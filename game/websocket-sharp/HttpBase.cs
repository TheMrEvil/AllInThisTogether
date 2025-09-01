using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using WebSocketSharp.Net;

namespace WebSocketSharp
{
	// Token: 0x02000015 RID: 21
	internal abstract class HttpBase
	{
		// Token: 0x0600016D RID: 365 RVA: 0x0000A4F4 File Offset: 0x000086F4
		protected HttpBase(Version version, NameValueCollection headers)
		{
			this._version = version;
			this._headers = headers;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000A50C File Offset: 0x0000870C
		public string EntityBody
		{
			get
			{
				bool flag = this.EntityBodyData == null || (long)this.EntityBodyData.Length == 0L;
				string result;
				if (flag)
				{
					result = string.Empty;
				}
				else
				{
					Encoding encoding = null;
					string text = this._headers["Content-Type"];
					bool flag2 = text != null && text.Length > 0;
					if (flag2)
					{
						encoding = HttpUtility.GetEncoding(text);
					}
					result = (encoding ?? Encoding.UTF8).GetString(this.EntityBodyData);
				}
				return result;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000A588 File Offset: 0x00008788
		public NameValueCollection Headers
		{
			get
			{
				return this._headers;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000A5A0 File Offset: 0x000087A0
		public Version ProtocolVersion
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000A5B8 File Offset: 0x000087B8
		private static byte[] readEntityBody(Stream stream, string length)
		{
			long num;
			bool flag = !long.TryParse(length, out num);
			if (flag)
			{
				throw new ArgumentException("Cannot be parsed.", "length");
			}
			bool flag2 = num < 0L;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("length", "Less than zero.");
			}
			return (num > 1024L) ? stream.ReadBytes(num, 1024) : ((num > 0L) ? stream.ReadBytes((int)num) : null);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000A62C File Offset: 0x0000882C
		private static string[] readHeaders(Stream stream, int maxLength)
		{
			List<byte> buff = new List<byte>();
			int cnt = 0;
			Action<int> beforeComparing = delegate(int i)
			{
				bool flag4 = i == -1;
				if (flag4)
				{
					throw new EndOfStreamException("The header cannot be read from the data source.");
				}
				buff.Add((byte)i);
				int cnt = cnt;
				cnt++;
			};
			bool flag = false;
			while (cnt < maxLength)
			{
				bool flag2 = stream.ReadByte().IsEqualTo('\r', beforeComparing) && stream.ReadByte().IsEqualTo('\n', beforeComparing) && stream.ReadByte().IsEqualTo('\r', beforeComparing) && stream.ReadByte().IsEqualTo('\n', beforeComparing);
				if (flag2)
				{
					flag = true;
					break;
				}
			}
			bool flag3 = !flag;
			if (flag3)
			{
				throw new WebSocketException("The length of header part is greater than the max length.");
			}
			return Encoding.UTF8.GetString(buff.ToArray()).Replace("\r\n ", " ").Replace("\r\n\t", " ").Split(new string[]
			{
				"\r\n"
			}, StringSplitOptions.RemoveEmptyEntries);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000A720 File Offset: 0x00008920
		protected static T Read<T>(Stream stream, Func<string[], T> parser, int millisecondsTimeout) where T : HttpBase
		{
			bool timeout = false;
			Timer timer = new Timer(delegate(object state)
			{
				timeout = true;
				stream.Close();
			}, null, millisecondsTimeout, -1);
			T t = default(T);
			Exception ex = null;
			try
			{
				t = parser(HttpBase.readHeaders(stream, 8192));
				string text = t.Headers["Content-Length"];
				bool flag = text != null && text.Length > 0;
				if (flag)
				{
					t.EntityBodyData = HttpBase.readEntityBody(stream, text);
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			finally
			{
				timer.Change(-1, -1);
				timer.Dispose();
			}
			string text2 = timeout ? "A timeout has occurred while reading an HTTP request/response." : ((ex != null) ? "An exception has occurred while reading an HTTP request/response." : null);
			bool flag2 = text2 != null;
			if (flag2)
			{
				throw new WebSocketException(text2, ex);
			}
			return t;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000A834 File Offset: 0x00008A34
		public byte[] ToByteArray()
		{
			return Encoding.UTF8.GetBytes(this.ToString());
		}

		// Token: 0x0400008A RID: 138
		private NameValueCollection _headers;

		// Token: 0x0400008B RID: 139
		private const int _headersMaxLength = 8192;

		// Token: 0x0400008C RID: 140
		private Version _version;

		// Token: 0x0400008D RID: 141
		internal byte[] EntityBodyData;

		// Token: 0x0400008E RID: 142
		protected const string CrLf = "\r\n";

		// Token: 0x0200006B RID: 107
		[CompilerGenerated]
		private sealed class <>c__DisplayClass13_0
		{
			// Token: 0x060005B1 RID: 1457 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass13_0()
			{
			}

			// Token: 0x060005B2 RID: 1458 RVA: 0x0001EC68 File Offset: 0x0001CE68
			internal void <readHeaders>b__0(int i)
			{
				bool flag = i == -1;
				if (flag)
				{
					throw new EndOfStreamException("The header cannot be read from the data source.");
				}
				this.buff.Add((byte)i);
				int num = this.cnt;
				this.cnt = num + 1;
			}

			// Token: 0x040002CF RID: 719
			public List<byte> buff;

			// Token: 0x040002D0 RID: 720
			public int cnt;
		}

		// Token: 0x0200006C RID: 108
		[CompilerGenerated]
		private sealed class <>c__DisplayClass14_0<T> where T : HttpBase
		{
			// Token: 0x060005B3 RID: 1459 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass14_0()
			{
			}

			// Token: 0x060005B4 RID: 1460 RVA: 0x0001ECA7 File Offset: 0x0001CEA7
			internal void <Read>b__0(object state)
			{
				this.timeout = true;
				this.stream.Close();
			}

			// Token: 0x040002D1 RID: 721
			public bool timeout;

			// Token: 0x040002D2 RID: 722
			public Stream stream;
		}
	}
}
