using System;
using System.Globalization;
using System.Net.Sockets;
using System.Text;

namespace System.Net
{
	/// <summary>Stores serialized information from <see cref="T:System.Net.EndPoint" /> derived classes.</summary>
	// Token: 0x02000601 RID: 1537
	public class SocketAddress
	{
		/// <summary>Gets the <see cref="T:System.Net.Sockets.AddressFamily" /> enumerated value of the current <see cref="T:System.Net.SocketAddress" />.</summary>
		/// <returns>One of the <see cref="T:System.Net.Sockets.AddressFamily" /> enumerated values.</returns>
		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06003097 RID: 12439 RVA: 0x000A71A1 File Offset: 0x000A53A1
		public AddressFamily Family
		{
			get
			{
				return (AddressFamily)((int)this.m_Buffer[0] | (int)this.m_Buffer[1] << 8);
			}
		}

		/// <summary>Gets the underlying buffer size of the <see cref="T:System.Net.SocketAddress" />.</summary>
		/// <returns>The underlying buffer size of the <see cref="T:System.Net.SocketAddress" />.</returns>
		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06003098 RID: 12440 RVA: 0x000A71B6 File Offset: 0x000A53B6
		public int Size
		{
			get
			{
				return this.m_Size;
			}
		}

		/// <summary>Gets or sets the specified index element in the underlying buffer.</summary>
		/// <param name="offset">The array index element of the desired information.</param>
		/// <returns>The value of the specified index element in the underlying buffer.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The specified index does not exist in the buffer.</exception>
		// Token: 0x170009B9 RID: 2489
		public byte this[int offset]
		{
			get
			{
				if (offset < 0 || offset >= this.Size)
				{
					throw new IndexOutOfRangeException();
				}
				return this.m_Buffer[offset];
			}
			set
			{
				if (offset < 0 || offset >= this.Size)
				{
					throw new IndexOutOfRangeException();
				}
				if (this.m_Buffer[offset] != value)
				{
					this.m_changed = true;
				}
				this.m_Buffer[offset] = value;
			}
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.SocketAddress" /> class for the given address family.</summary>
		/// <param name="family">An <see cref="T:System.Net.Sockets.AddressFamily" /> enumerated value.</param>
		// Token: 0x0600309B RID: 12443 RVA: 0x000A720B File Offset: 0x000A540B
		public SocketAddress(AddressFamily family) : this(family, 32)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.SocketAddress" /> class using the specified address family and buffer size.</summary>
		/// <param name="family">An <see cref="T:System.Net.Sockets.AddressFamily" /> enumerated value.</param>
		/// <param name="size">The number of bytes to allocate for the underlying buffer.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="size" /> is less than 2. These 2 bytes are needed to store <paramref name="family" />.</exception>
		// Token: 0x0600309C RID: 12444 RVA: 0x000A7218 File Offset: 0x000A5418
		public SocketAddress(AddressFamily family, int size)
		{
			if (size < 2)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			this.m_Size = size;
			this.m_Buffer = new byte[(size / IntPtr.Size + 2) * IntPtr.Size];
			this.m_Buffer[0] = (byte)family;
			this.m_Buffer[1] = (byte)(family >> 8);
		}

		// Token: 0x0600309D RID: 12445 RVA: 0x000A7278 File Offset: 0x000A5478
		internal SocketAddress(IPAddress ipAddress) : this(ipAddress.AddressFamily, (ipAddress.AddressFamily == AddressFamily.InterNetwork) ? 16 : 28)
		{
			this.m_Buffer[2] = 0;
			this.m_Buffer[3] = 0;
			if (ipAddress.AddressFamily == AddressFamily.InterNetworkV6)
			{
				this.m_Buffer[4] = 0;
				this.m_Buffer[5] = 0;
				this.m_Buffer[6] = 0;
				this.m_Buffer[7] = 0;
				long scopeId = ipAddress.ScopeId;
				this.m_Buffer[24] = (byte)scopeId;
				this.m_Buffer[25] = (byte)(scopeId >> 8);
				this.m_Buffer[26] = (byte)(scopeId >> 16);
				this.m_Buffer[27] = (byte)(scopeId >> 24);
				byte[] addressBytes = ipAddress.GetAddressBytes();
				for (int i = 0; i < addressBytes.Length; i++)
				{
					this.m_Buffer[8 + i] = addressBytes[i];
				}
				return;
			}
			int num;
			ipAddress.TryWriteBytes(this.m_Buffer.AsSpan(4), out num);
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x000A7356 File Offset: 0x000A5556
		internal SocketAddress(IPAddress ipaddress, int port) : this(ipaddress)
		{
			this.m_Buffer[2] = (byte)(port >> 8);
			this.m_Buffer[3] = (byte)port;
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x000A7378 File Offset: 0x000A5578
		internal IPAddress GetIPAddress()
		{
			if (this.Family == AddressFamily.InterNetworkV6)
			{
				byte[] array = new byte[16];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.m_Buffer[i + 8];
				}
				long scopeid = (long)(((int)this.m_Buffer[27] << 24) + ((int)this.m_Buffer[26] << 16) + ((int)this.m_Buffer[25] << 8) + (int)this.m_Buffer[24]);
				return new IPAddress(array, scopeid);
			}
			if (this.Family == AddressFamily.InterNetwork)
			{
				return new IPAddress((long)((int)(this.m_Buffer[4] & byte.MaxValue) | ((int)this.m_Buffer[5] << 8 & 65280) | ((int)this.m_Buffer[6] << 16 & 16711680) | (int)this.m_Buffer[7] << 24) & (long)((ulong)-1));
			}
			throw new SocketException(SocketError.AddressFamilyNotSupported);
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x000A7448 File Offset: 0x000A5648
		internal IPEndPoint GetIPEndPoint()
		{
			IPAddress ipaddress = this.GetIPAddress();
			int port = ((int)this.m_Buffer[2] << 8 & 65280) | (int)this.m_Buffer[3];
			return new IPEndPoint(ipaddress, port);
		}

		// Token: 0x060030A1 RID: 12449 RVA: 0x000A747C File Offset: 0x000A567C
		internal void CopyAddressSizeIntoBuffer()
		{
			this.m_Buffer[this.m_Buffer.Length - IntPtr.Size] = (byte)this.m_Size;
			this.m_Buffer[this.m_Buffer.Length - IntPtr.Size + 1] = (byte)(this.m_Size >> 8);
			this.m_Buffer[this.m_Buffer.Length - IntPtr.Size + 2] = (byte)(this.m_Size >> 16);
			this.m_Buffer[this.m_Buffer.Length - IntPtr.Size + 3] = (byte)(this.m_Size >> 24);
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x000A7507 File Offset: 0x000A5707
		internal int GetAddressSizeOffset()
		{
			return this.m_Buffer.Length - IntPtr.Size;
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x000A7517 File Offset: 0x000A5717
		internal unsafe void SetSize(IntPtr ptr)
		{
			this.m_Size = *(int*)((void*)ptr);
		}

		/// <summary>Determines whether the specified <see langword="Object" /> is equal to the current <see langword="Object" />.</summary>
		/// <param name="comparand">The <see cref="T:System.Object" /> to compare with the current <see langword="Object" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see langword="Object" /> is equal to the current <see langword="Object" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060030A4 RID: 12452 RVA: 0x000A7528 File Offset: 0x000A5728
		public override bool Equals(object comparand)
		{
			SocketAddress socketAddress = comparand as SocketAddress;
			if (socketAddress == null || this.Size != socketAddress.Size)
			{
				return false;
			}
			for (int i = 0; i < this.Size; i++)
			{
				if (this[i] != socketAddress[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Serves as a hash function for a particular type, suitable for use in hashing algorithms and data structures like a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
		// Token: 0x060030A5 RID: 12453 RVA: 0x000A7574 File Offset: 0x000A5774
		public override int GetHashCode()
		{
			if (this.m_changed)
			{
				this.m_changed = false;
				this.m_hash = 0;
				int num = this.Size & -4;
				int i;
				for (i = 0; i < num; i += 4)
				{
					this.m_hash ^= ((int)this.m_Buffer[i] | (int)this.m_Buffer[i + 1] << 8 | (int)this.m_Buffer[i + 2] << 16 | (int)this.m_Buffer[i + 3] << 24);
				}
				if ((this.Size & 3) != 0)
				{
					int num2 = 0;
					int num3 = 0;
					while (i < this.Size)
					{
						num2 |= (int)this.m_Buffer[i] << num3;
						num3 += 8;
						i++;
					}
					this.m_hash ^= num2;
				}
			}
			return this.m_hash;
		}

		/// <summary>Returns information about the socket address.</summary>
		/// <returns>A string that contains information about the <see cref="T:System.Net.SocketAddress" />.</returns>
		// Token: 0x060030A6 RID: 12454 RVA: 0x000A7634 File Offset: 0x000A5834
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 2; i < this.Size; i++)
			{
				if (i > 2)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(this[i].ToString(NumberFormatInfo.InvariantInfo));
			}
			return string.Concat(new string[]
			{
				this.Family.ToString(),
				":",
				this.Size.ToString(NumberFormatInfo.InvariantInfo),
				":{",
				stringBuilder.ToString(),
				"}"
			});
		}

		// Token: 0x04001C31 RID: 7217
		internal const int IPv6AddressSize = 28;

		// Token: 0x04001C32 RID: 7218
		internal const int IPv4AddressSize = 16;

		// Token: 0x04001C33 RID: 7219
		internal int m_Size;

		// Token: 0x04001C34 RID: 7220
		internal byte[] m_Buffer;

		// Token: 0x04001C35 RID: 7221
		private const int WriteableOffset = 2;

		// Token: 0x04001C36 RID: 7222
		private const int MaxSize = 32;

		// Token: 0x04001C37 RID: 7223
		private bool m_changed = true;

		// Token: 0x04001C38 RID: 7224
		private int m_hash;
	}
}
