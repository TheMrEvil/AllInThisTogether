using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Net.Sockets
{
	// Token: 0x02000072 RID: 114
	public sealed class UnixDomainSocketEndPoint : EndPoint
	{
		// Token: 0x06000274 RID: 628 RVA: 0x00007C34 File Offset: 0x00005E34
		private SocketAddress CreateSocketAddressForSerialize()
		{
			return new SocketAddress(AddressFamily.Unix, UnixDomainSocketEndPoint.s_nativeAddressSize);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00007C44 File Offset: 0x00005E44
		public UnixDomainSocketEndPoint(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			bool flag = UnixDomainSocketEndPoint.IsAbstract(path);
			int num = UnixDomainSocketEndPoint.s_pathEncoding.GetByteCount(path);
			if (!flag)
			{
				num++;
			}
			if (path.Length == 0 || num > UnixDomainSocketEndPoint.s_nativePathLength)
			{
				throw new ArgumentOutOfRangeException("path", path, SR.Format("The path '{0}' is of an invalid length for use with domain sockets on this platform.  The length must be between 1 and {1} characters, inclusive.", path, UnixDomainSocketEndPoint.s_nativePathLength));
			}
			this._path = path;
			this._encodedPath = new byte[num];
			UnixDomainSocketEndPoint.s_pathEncoding.GetBytes(path, 0, path.Length, this._encodedPath, 0);
			if (!UnixDomainSocketEndPoint.s_udsSupported.Value)
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00007CF0 File Offset: 0x00005EF0
		internal UnixDomainSocketEndPoint(SocketAddress socketAddress)
		{
			if (socketAddress == null)
			{
				throw new ArgumentNullException("socketAddress");
			}
			if (socketAddress.Family != AddressFamily.Unix || socketAddress.Size > UnixDomainSocketEndPoint.s_nativeAddressSize)
			{
				throw new ArgumentOutOfRangeException("socketAddress");
			}
			if (socketAddress.Size > UnixDomainSocketEndPoint.s_nativePathOffset)
			{
				this._encodedPath = new byte[socketAddress.Size - UnixDomainSocketEndPoint.s_nativePathOffset];
				for (int i = 0; i < this._encodedPath.Length; i++)
				{
					this._encodedPath[i] = socketAddress[UnixDomainSocketEndPoint.s_nativePathOffset + i];
				}
				int num = this._encodedPath.Length;
				if (!UnixDomainSocketEndPoint.IsAbstract(this._encodedPath))
				{
					while (this._encodedPath[num - 1] == 0)
					{
						num--;
					}
				}
				this._path = UnixDomainSocketEndPoint.s_pathEncoding.GetString(this._encodedPath, 0, num);
				return;
			}
			this._encodedPath = Array.Empty<byte>();
			this._path = string.Empty;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00007DD8 File Offset: 0x00005FD8
		public override SocketAddress Serialize()
		{
			SocketAddress socketAddress = this.CreateSocketAddressForSerialize();
			for (int i = 0; i < this._encodedPath.Length; i++)
			{
				socketAddress[UnixDomainSocketEndPoint.s_nativePathOffset + i] = this._encodedPath[i];
			}
			return socketAddress;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00007E15 File Offset: 0x00006015
		public override EndPoint Create(SocketAddress socketAddress)
		{
			return new UnixDomainSocketEndPoint(socketAddress);
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000279 RID: 633 RVA: 0x00007E1D File Offset: 0x0000601D
		public override AddressFamily AddressFamily
		{
			get
			{
				return AddressFamily.Unix;
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00007E20 File Offset: 0x00006020
		public override string ToString()
		{
			if (UnixDomainSocketEndPoint.IsAbstract(this._path))
			{
				return "@" + this._path.Substring(1);
			}
			return this._path;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00007E4C File Offset: 0x0000604C
		private static bool IsAbstract(string path)
		{
			return path.Length > 0 && path[0] == '\0';
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00007E63 File Offset: 0x00006063
		private static bool IsAbstract(byte[] encodedPath)
		{
			return encodedPath.Length != 0 && encodedPath[0] == 0;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00007E74 File Offset: 0x00006074
		// Note: this type is marked as 'beforefieldinit'.
		static UnixDomainSocketEndPoint()
		{
		}

		// Token: 0x040003A3 RID: 931
		private static readonly int s_nativePathOffset = 2;

		// Token: 0x040003A4 RID: 932
		private static readonly int s_nativePathLength = 108;

		// Token: 0x040003A5 RID: 933
		private static readonly int s_nativeAddressSize = UnixDomainSocketEndPoint.s_nativePathOffset + UnixDomainSocketEndPoint.s_nativePathLength;

		// Token: 0x040003A6 RID: 934
		private const AddressFamily EndPointAddressFamily = AddressFamily.Unix;

		// Token: 0x040003A7 RID: 935
		private static readonly Encoding s_pathEncoding = Encoding.UTF8;

		// Token: 0x040003A8 RID: 936
		private static readonly Lazy<bool> s_udsSupported = new Lazy<bool>(delegate()
		{
			bool result;
			try
			{
				new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.IP).Dispose();
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		});

		// Token: 0x040003A9 RID: 937
		private readonly string _path;

		// Token: 0x040003AA RID: 938
		private readonly byte[] _encodedPath;

		// Token: 0x02000073 RID: 115
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600027E RID: 638 RVA: 0x00007EC2 File Offset: 0x000060C2
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600027F RID: 639 RVA: 0x00002162 File Offset: 0x00000362
			public <>c()
			{
			}

			// Token: 0x06000280 RID: 640 RVA: 0x00007ED0 File Offset: 0x000060D0
			internal bool <.cctor>b__18_0()
			{
				bool result;
				try
				{
					new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.IP).Dispose();
					result = true;
				}
				catch
				{
					result = false;
				}
				return result;
			}

			// Token: 0x040003AB RID: 939
			public static readonly UnixDomainSocketEndPoint.<>c <>9 = new UnixDomainSocketEndPoint.<>c();
		}
	}
}
