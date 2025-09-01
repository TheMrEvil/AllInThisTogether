using System;

namespace WebSocketSharp.Net
{
	// Token: 0x0200003C RID: 60
	internal sealed class HttpListenerPrefix
	{
		// Token: 0x060003E8 RID: 1000 RVA: 0x00017FD5 File Offset: 0x000161D5
		internal HttpListenerPrefix(string uriPrefix, HttpListener listener)
		{
			this._original = uriPrefix;
			this._listener = listener;
			this.parse(uriPrefix);
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00017FF8 File Offset: 0x000161F8
		public string Host
		{
			get
			{
				return this._host;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x00018010 File Offset: 0x00016210
		public bool IsSecure
		{
			get
			{
				return this._secure;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x00018028 File Offset: 0x00016228
		public HttpListener Listener
		{
			get
			{
				return this._listener;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x00018040 File Offset: 0x00016240
		public string Original
		{
			get
			{
				return this._original;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00018058 File Offset: 0x00016258
		public string Path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00018070 File Offset: 0x00016270
		public string Port
		{
			get
			{
				return this._port;
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00018088 File Offset: 0x00016288
		private void parse(string uriPrefix)
		{
			bool flag = uriPrefix.StartsWith("https");
			if (flag)
			{
				this._secure = true;
			}
			int length = uriPrefix.Length;
			int num = uriPrefix.IndexOf(':') + 3;
			int num2 = uriPrefix.IndexOf('/', num + 1, length - num - 1);
			int num3 = uriPrefix.LastIndexOf(':', num2 - 1, num2 - num - 1);
			bool flag2 = uriPrefix[num2 - 1] != ']' && num3 > num;
			if (flag2)
			{
				this._host = uriPrefix.Substring(num, num3 - num);
				this._port = uriPrefix.Substring(num3 + 1, num2 - num3 - 1);
			}
			else
			{
				this._host = uriPrefix.Substring(num, num2 - num);
				this._port = (this._secure ? "443" : "80");
			}
			this._path = uriPrefix.Substring(num2);
			this._prefix = string.Format("{0}://{1}:{2}{3}", new object[]
			{
				this._secure ? "https" : "http",
				this._host,
				this._port,
				this._path
			});
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x000181A8 File Offset: 0x000163A8
		public static void CheckPrefix(string uriPrefix)
		{
			bool flag = uriPrefix == null;
			if (flag)
			{
				throw new ArgumentNullException("uriPrefix");
			}
			int length = uriPrefix.Length;
			bool flag2 = length == 0;
			if (flag2)
			{
				string message = "An empty string.";
				throw new ArgumentException(message, "uriPrefix");
			}
			bool flag3 = uriPrefix.StartsWith("http://") || uriPrefix.StartsWith("https://");
			bool flag4 = !flag3;
			if (flag4)
			{
				string message2 = "The scheme is not 'http' or 'https'.";
				throw new ArgumentException(message2, "uriPrefix");
			}
			int num = length - 1;
			bool flag5 = uriPrefix[num] != '/';
			if (flag5)
			{
				string message3 = "It ends without '/'.";
				throw new ArgumentException(message3, "uriPrefix");
			}
			int num2 = uriPrefix.IndexOf(':') + 3;
			bool flag6 = num2 >= num;
			if (flag6)
			{
				string message4 = "No host is specified.";
				throw new ArgumentException(message4, "uriPrefix");
			}
			bool flag7 = uriPrefix[num2] == ':';
			if (flag7)
			{
				string message5 = "No host is specified.";
				throw new ArgumentException(message5, "uriPrefix");
			}
			int num3 = uriPrefix.IndexOf('/', num2, length - num2);
			bool flag8 = num3 == num2;
			if (flag8)
			{
				string message6 = "No host is specified.";
				throw new ArgumentException(message6, "uriPrefix");
			}
			bool flag9 = uriPrefix[num3 - 1] == ':';
			if (flag9)
			{
				string message7 = "No port is specified.";
				throw new ArgumentException(message7, "uriPrefix");
			}
			bool flag10 = num3 == num - 1;
			if (flag10)
			{
				string message8 = "No path is specified.";
				throw new ArgumentException(message8, "uriPrefix");
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00018328 File Offset: 0x00016528
		public override bool Equals(object obj)
		{
			HttpListenerPrefix httpListenerPrefix = obj as HttpListenerPrefix;
			return httpListenerPrefix != null && this._prefix.Equals(httpListenerPrefix._prefix);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00018358 File Offset: 0x00016558
		public override int GetHashCode()
		{
			return this._prefix.GetHashCode();
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00018378 File Offset: 0x00016578
		public override string ToString()
		{
			return this._prefix;
		}

		// Token: 0x0400019A RID: 410
		private string _host;

		// Token: 0x0400019B RID: 411
		private HttpListener _listener;

		// Token: 0x0400019C RID: 412
		private string _original;

		// Token: 0x0400019D RID: 413
		private string _path;

		// Token: 0x0400019E RID: 414
		private string _port;

		// Token: 0x0400019F RID: 415
		private string _prefix;

		// Token: 0x040001A0 RID: 416
		private bool _secure;
	}
}
