using System;

namespace System.Net.Sockets
{
	/// <summary>Presents UDP receive result information from a call to the <see cref="M:System.Net.Sockets.UdpClient.ReceiveAsync" /> method.</summary>
	// Token: 0x020007BF RID: 1983
	public struct UdpReceiveResult : IEquatable<UdpReceiveResult>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.UdpReceiveResult" /> class.</summary>
		/// <param name="buffer">A buffer for data to receive in the UDP packet.</param>
		/// <param name="remoteEndPoint">The remote endpoint of the UDP packet.</param>
		// Token: 0x06003F4E RID: 16206 RVA: 0x000D8651 File Offset: 0x000D6851
		public UdpReceiveResult(byte[] buffer, IPEndPoint remoteEndPoint)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (remoteEndPoint == null)
			{
				throw new ArgumentNullException("remoteEndPoint");
			}
			this.m_buffer = buffer;
			this.m_remoteEndPoint = remoteEndPoint;
		}

		/// <summary>Gets a buffer with the data received in the UDP packet.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array with the data received in the UDP packet.</returns>
		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x06003F4F RID: 16207 RVA: 0x000D867D File Offset: 0x000D687D
		public byte[] Buffer
		{
			get
			{
				return this.m_buffer;
			}
		}

		/// <summary>Gets the remote endpoint from which the UDP packet was received.</summary>
		/// <returns>The remote endpoint from which the UDP packet was received.</returns>
		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06003F50 RID: 16208 RVA: 0x000D8685 File Offset: 0x000D6885
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.m_remoteEndPoint;
			}
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>The hash code.</returns>
		// Token: 0x06003F51 RID: 16209 RVA: 0x000D868D File Offset: 0x000D688D
		public override int GetHashCode()
		{
			if (this.m_buffer == null)
			{
				return 0;
			}
			return this.m_buffer.GetHashCode() ^ this.m_remoteEndPoint.GetHashCode();
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">The object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see cref="T:System.Net.Sockets.UdpReceiveResult" /> and equals the value of the instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003F52 RID: 16210 RVA: 0x000D86B0 File Offset: 0x000D68B0
		public override bool Equals(object obj)
		{
			return obj is UdpReceiveResult && this.Equals((UdpReceiveResult)obj);
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="other">The object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="other" /> is an instance of <see cref="T:System.Net.Sockets.UdpReceiveResult" /> and equals the value of the instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003F53 RID: 16211 RVA: 0x000D86C8 File Offset: 0x000D68C8
		public bool Equals(UdpReceiveResult other)
		{
			return object.Equals(this.m_buffer, other.m_buffer) && object.Equals(this.m_remoteEndPoint, other.m_remoteEndPoint);
		}

		/// <summary>Tests whether two specified <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instances are equivalent.</summary>
		/// <param name="left">The <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instance that is to the left of the equality operator.</param>
		/// <param name="right">The <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instance that is to the right of the equality operator.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003F54 RID: 16212 RVA: 0x000D86F0 File Offset: 0x000D68F0
		public static bool operator ==(UdpReceiveResult left, UdpReceiveResult right)
		{
			return left.Equals(right);
		}

		/// <summary>Tests whether two specified <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instances are not equal.</summary>
		/// <param name="left">The <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instance that is to the left of the not equal operator.</param>
		/// <param name="right">The <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instance that is to the right of the not equal operator.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are unequal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003F55 RID: 16213 RVA: 0x000D86FA File Offset: 0x000D68FA
		public static bool operator !=(UdpReceiveResult left, UdpReceiveResult right)
		{
			return !left.Equals(right);
		}

		// Token: 0x040025D2 RID: 9682
		private byte[] m_buffer;

		// Token: 0x040025D3 RID: 9683
		private IPEndPoint m_remoteEndPoint;
	}
}
