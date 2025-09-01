using System;

namespace System.Net.Sockets
{
	/// <summary>Represents an element in a <see cref="T:System.Net.Sockets.SendPacketsElement" /> array.</summary>
	// Token: 0x020007B0 RID: 1968
	public class SendPacketsElement
	{
		// Token: 0x06003EBA RID: 16058 RVA: 0x0000219B File Offset: 0x0000039B
		private SendPacketsElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified file.</summary>
		/// <param name="filepath">The filename of the file to be transmitted using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="filepath" /> parameter cannot be null</exception>
		// Token: 0x06003EBB RID: 16059 RVA: 0x000D693C File Offset: 0x000D4B3C
		public SendPacketsElement(string filepath) : this(filepath, 0, 0, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified filename path, offset, and count.</summary>
		/// <param name="filepath">The filename of the file to be transmitted using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <param name="offset">The offset, in bytes, from the beginning of the file to the location in the file to start sending the file specified in the <paramref name="filepath" /> parameter.</param>
		/// <param name="count">The number of bytes to send starting from the <paramref name="offset" /> parameter. If <paramref name="count" /> is zero, the entire file is sent.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="filepath" /> parameter cannot be null</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> and <paramref name="count" /> parameters must be greater than or equal to zero.  
		///  The <paramref name="offset" /> and <paramref name="count" /> must be less than the size of the file indicated by the <paramref name="filepath" /> parameter.</exception>
		// Token: 0x06003EBC RID: 16060 RVA: 0x000D6948 File Offset: 0x000D4B48
		public SendPacketsElement(string filepath, int offset, int count) : this(filepath, offset, count, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified filename path, buffer offset, and count with an option to combine this element with the next element in a single send request from the sockets layer to the transport.</summary>
		/// <param name="filepath">The filename of the file to be transmitted using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <param name="offset">The offset, in bytes, from the beginning of the file to the location in the file to start sending the file specified in the <paramref name="filepath" /> parameter.</param>
		/// <param name="count">The number of bytes to send starting from the <paramref name="offset" /> parameter. If <paramref name="count" /> is zero, the entire file is sent.</param>
		/// <param name="endOfPacket">A Boolean value that specifies that this element should not be combined with the next element in a single send request from the sockets layer to the transport. This flag is used for granular control of the content of each message on a datagram or message-oriented socket.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="filepath" /> parameter cannot be null</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> and <paramref name="count" /> parameters must be greater than or equal to zero.  
		///  The <paramref name="offset" /> and <paramref name="count" /> must be less than the size of the file indicated by the <paramref name="filepath" /> parameter.</exception>
		// Token: 0x06003EBD RID: 16061 RVA: 0x000D6954 File Offset: 0x000D4B54
		public SendPacketsElement(string filepath, int offset, int count, bool endOfPacket)
		{
			if (filepath == null)
			{
				throw new ArgumentNullException("filepath");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.Initialize(filepath, null, offset, count, endOfPacket);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified buffer.</summary>
		/// <param name="buffer">A byte array of data to send using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter cannot be null</exception>
		// Token: 0x06003EBE RID: 16062 RVA: 0x000D6994 File Offset: 0x000D4B94
		public SendPacketsElement(byte[] buffer) : this(buffer, 0, (buffer != null) ? buffer.Length : 0, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified buffer, buffer offset, and count.</summary>
		/// <param name="buffer">A byte array of data to send using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <param name="offset">The offset, in bytes, from the beginning of the <paramref name="buffer" /> to the location in the <paramref name="buffer" /> to start sending the data specified in the <paramref name="buffer" /> parameter.</param>
		/// <param name="count">The number of bytes to send starting from the <paramref name="offset" /> parameter. If <paramref name="count" /> is zero, no bytes are sent.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter cannot be null</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> and <paramref name="count" /> parameters must be greater than or equal to zero.  
		///  The <paramref name="offset" /> and <paramref name="count" /> must be less than the size of the buffer</exception>
		// Token: 0x06003EBF RID: 16063 RVA: 0x000D69A8 File Offset: 0x000D4BA8
		public SendPacketsElement(byte[] buffer, int offset, int count) : this(buffer, offset, count, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified buffer, buffer offset, and count with an option to combine this element with the next element in a single send request from the sockets layer to the transport.</summary>
		/// <param name="buffer">A byte array of data to send using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <param name="offset">The offset, in bytes, from the beginning of the <paramref name="buffer" /> to the location in the <paramref name="buffer" /> to start sending the data specified in the <paramref name="buffer" /> parameter.</param>
		/// <param name="count">The number bytes to send starting from the <paramref name="offset" /> parameter. If <paramref name="count" /> is zero, no bytes are sent.</param>
		/// <param name="endOfPacket">A Boolean value that specifies that this element should not be combined with the next element in a single send request from the sockets layer to the transport. This flag is used for granular control of the content of each message on a datagram or message-oriented socket.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter cannot be null</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> and <paramref name="count" /> parameters must be greater than or equal to zero.  
		///  The <paramref name="offset" /> and <paramref name="count" /> must be less than the size of the buffer</exception>
		// Token: 0x06003EC0 RID: 16064 RVA: 0x000D69B4 File Offset: 0x000D4BB4
		public SendPacketsElement(byte[] buffer, int offset, int count, bool endOfPacket)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.Initialize(null, buffer, offset, count, endOfPacket);
		}

		// Token: 0x06003EC1 RID: 16065 RVA: 0x000D6A0D File Offset: 0x000D4C0D
		private void Initialize(string filePath, byte[] buffer, int offset, int count, bool endOfPacket)
		{
			this.m_FilePath = filePath;
			this.m_Buffer = buffer;
			this.m_Offset = offset;
			this.m_Count = count;
			this.m_endOfPacket = endOfPacket;
		}

		/// <summary>Gets the filename of the file to send if the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class was initialized with a <paramref name="filepath" /> parameter.</summary>
		/// <returns>The filename of the file to send if the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class was initialized with a <paramref name="filepath" /> parameter.</returns>
		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x06003EC2 RID: 16066 RVA: 0x000D6A34 File Offset: 0x000D4C34
		public string FilePath
		{
			get
			{
				return this.m_FilePath;
			}
		}

		/// <summary>Gets the buffer to be sent if the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class was initialized with a <paramref name="buffer" /> parameter.</summary>
		/// <returns>The byte buffer to send if the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class was initialized with a <paramref name="buffer" /> parameter.</returns>
		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x06003EC3 RID: 16067 RVA: 0x000D6A3C File Offset: 0x000D4C3C
		public byte[] Buffer
		{
			get
			{
				return this.m_Buffer;
			}
		}

		/// <summary>Gets the count of bytes to be sent.</summary>
		/// <returns>The count of bytes to send if the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class was initialized with a <paramref name="count" /> parameter.</returns>
		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x06003EC4 RID: 16068 RVA: 0x000D6A44 File Offset: 0x000D4C44
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		/// <summary>Gets the offset, in bytes, from the beginning of the data buffer or file to the location in the buffer or file to start sending the data.</summary>
		/// <returns>The offset, in bytes, from the beginning of the data buffer or file to the location in the buffer or file to start sending the data.</returns>
		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x06003EC5 RID: 16069 RVA: 0x000D6A4C File Offset: 0x000D4C4C
		public int Offset
		{
			get
			{
				return this.m_Offset;
			}
		}

		/// <summary>Gets a Boolean value that indicates if this element should not be combined with the next element in a single send request from the sockets layer to the transport.</summary>
		/// <returns>A Boolean value that indicates if this element should not be combined with the next element in a single send request.</returns>
		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x06003EC6 RID: 16070 RVA: 0x000D6A54 File Offset: 0x000D4C54
		public bool EndOfPacket
		{
			get
			{
				return this.m_endOfPacket;
			}
		}

		// Token: 0x0400252B RID: 9515
		internal string m_FilePath;

		// Token: 0x0400252C RID: 9516
		internal byte[] m_Buffer;

		// Token: 0x0400252D RID: 9517
		internal int m_Offset;

		// Token: 0x0400252E RID: 9518
		internal int m_Count;

		// Token: 0x0400252F RID: 9519
		private bool m_endOfPacket;
	}
}
