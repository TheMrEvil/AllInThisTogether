using System;

namespace System.Net
{
	// Token: 0x020006A1 RID: 1697
	internal sealed class ListenerPrefix
	{
		// Token: 0x06003656 RID: 13910 RVA: 0x000BE8AB File Offset: 0x000BCAAB
		public ListenerPrefix(string prefix)
		{
			this.original = prefix;
			this.Parse(prefix);
		}

		// Token: 0x06003657 RID: 13911 RVA: 0x000BE8C1 File Offset: 0x000BCAC1
		public override string ToString()
		{
			return this.original;
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06003658 RID: 13912 RVA: 0x000BE8C9 File Offset: 0x000BCAC9
		// (set) Token: 0x06003659 RID: 13913 RVA: 0x000BE8D1 File Offset: 0x000BCAD1
		public IPAddress[] Addresses
		{
			get
			{
				return this.addresses;
			}
			set
			{
				this.addresses = value;
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x0600365A RID: 13914 RVA: 0x000BE8DA File Offset: 0x000BCADA
		public bool Secure
		{
			get
			{
				return this.secure;
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x0600365B RID: 13915 RVA: 0x000BE8E2 File Offset: 0x000BCAE2
		public string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x0600365C RID: 13916 RVA: 0x000BE8EA File Offset: 0x000BCAEA
		public int Port
		{
			get
			{
				return (int)this.port;
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x0600365D RID: 13917 RVA: 0x000BE8F2 File Offset: 0x000BCAF2
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x0600365E RID: 13918 RVA: 0x000BE8FC File Offset: 0x000BCAFC
		public override bool Equals(object o)
		{
			ListenerPrefix listenerPrefix = o as ListenerPrefix;
			return listenerPrefix != null && this.original == listenerPrefix.original;
		}

		// Token: 0x0600365F RID: 13919 RVA: 0x000BE926 File Offset: 0x000BCB26
		public override int GetHashCode()
		{
			return this.original.GetHashCode();
		}

		// Token: 0x06003660 RID: 13920 RVA: 0x000BE934 File Offset: 0x000BCB34
		private void Parse(string uri)
		{
			ushort num = 80;
			if (uri.StartsWith("https://"))
			{
				num = 443;
				this.secure = true;
			}
			int length = uri.Length;
			int num2 = uri.IndexOf(':') + 3;
			if (num2 >= length)
			{
				throw new ArgumentException("No host specified.");
			}
			int num3 = uri.IndexOf(':', num2, length - num2);
			if (uri[num2] == '[')
			{
				num3 = uri.IndexOf("]:") + 1;
			}
			if (num2 == num3)
			{
				throw new ArgumentException("No host specified.");
			}
			int num4 = uri.IndexOf('/', num2, length - num2);
			if (num4 == -1)
			{
				throw new ArgumentException("No path specified.");
			}
			if (num3 > 0)
			{
				this.host = uri.Substring(num2, num3 - num2).Trim(new char[]
				{
					'[',
					']'
				});
				this.port = ushort.Parse(uri.Substring(num3 + 1, num4 - num3 - 1));
			}
			else
			{
				this.host = uri.Substring(num2, num4 - num2).Trim(new char[]
				{
					'[',
					']'
				});
				this.port = num;
			}
			this.path = uri.Substring(num4);
			if (this.path.Length != 1)
			{
				this.path = this.path.Substring(0, this.path.Length - 1);
			}
		}

		// Token: 0x06003661 RID: 13921 RVA: 0x000BEA80 File Offset: 0x000BCC80
		public static void CheckUri(string uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uriPrefix");
			}
			if (!uri.StartsWith("http://") && !uri.StartsWith("https://"))
			{
				throw new ArgumentException("Only 'http' and 'https' schemes are supported.");
			}
			int length = uri.Length;
			int num = uri.IndexOf(':') + 3;
			if (num >= length)
			{
				throw new ArgumentException("No host specified.");
			}
			int num2 = uri.IndexOf(':', num, length - num);
			if (uri[num] == '[')
			{
				num2 = uri.IndexOf("]:") + 1;
			}
			if (num == num2)
			{
				throw new ArgumentException("No host specified.");
			}
			int num3 = uri.IndexOf('/', num, length - num);
			if (num3 == -1)
			{
				throw new ArgumentException("No path specified.");
			}
			if (num2 > 0)
			{
				try
				{
					int num4 = int.Parse(uri.Substring(num2 + 1, num3 - num2 - 1));
					if (num4 <= 0 || num4 >= 65536)
					{
						throw new Exception();
					}
				}
				catch
				{
					throw new ArgumentException("Invalid port.");
				}
			}
			if (uri[uri.Length - 1] != '/')
			{
				throw new ArgumentException("The prefix must end with '/'");
			}
		}

		// Token: 0x04001FA5 RID: 8101
		private string original;

		// Token: 0x04001FA6 RID: 8102
		private string host;

		// Token: 0x04001FA7 RID: 8103
		private ushort port;

		// Token: 0x04001FA8 RID: 8104
		private string path;

		// Token: 0x04001FA9 RID: 8105
		private bool secure;

		// Token: 0x04001FAA RID: 8106
		private IPAddress[] addresses;

		// Token: 0x04001FAB RID: 8107
		public HttpListener Listener;
	}
}
