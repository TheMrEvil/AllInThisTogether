using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Sockets
{
	/// <summary>This class contains extension methods to the <see cref="T:System.Net.Sockets.Socket" /> class.</summary>
	// Token: 0x020007C7 RID: 1991
	public static class SocketTaskExtensions
	{
		/// <summary>Performs an asynchronous operation on to accept an incoming connection attempt on the socket.</summary>
		/// <param name="socket">The socket that is listening for connections.</param>
		/// <returns>An asynchronous task that completes with a <see cref="T:System.Net.Sockets.Socket" /> to handle communication with the remote host.</returns>
		// Token: 0x06003FAA RID: 16298 RVA: 0x000D92EC File Offset: 0x000D74EC
		public static Task<Socket> AcceptAsync(this Socket socket)
		{
			return Task<Socket>.Factory.FromAsync((AsyncCallback callback, object state) => ((Socket)state).BeginAccept(callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndAccept(asyncResult), socket);
		}

		/// <summary>Performs an asynchronous operation on to accept an incoming connection attempt on the socket.</summary>
		/// <param name="socket">The socket that is listening for incoming connections.</param>
		/// <param name="acceptSocket">The accepted <see cref="T:System.Net.Sockets.Socket" /> object. This value may be <see langword="null" />.</param>
		/// <returns>An asynchronous task that completes with a <see cref="T:System.Net.Sockets.Socket" /> to handle communication with the remote host.</returns>
		// Token: 0x06003FAB RID: 16299 RVA: 0x000D9344 File Offset: 0x000D7544
		public static Task<Socket> AcceptAsync(this Socket socket, Socket acceptSocket)
		{
			return Task<Socket>.Factory.FromAsync<Socket, int>((Socket socketForAccept, int receiveSize, AsyncCallback callback, object state) => ((Socket)state).BeginAccept(socketForAccept, receiveSize, callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndAccept(asyncResult), acceptSocket, 0, socket);
		}

		/// <summary>Establishes a connection to a remote host.</summary>
		/// <param name="socket">The socket that is used for establishing a connection.</param>
		/// <param name="remoteEP">An EndPoint that represents the remote device.</param>
		/// <returns>An asynchronous Task.</returns>
		// Token: 0x06003FAC RID: 16300 RVA: 0x000D939C File Offset: 0x000D759C
		public static Task ConnectAsync(this Socket socket, EndPoint remoteEP)
		{
			return Task.Factory.FromAsync<EndPoint>((EndPoint targetEndPoint, AsyncCallback callback, object state) => ((Socket)state).BeginConnect(targetEndPoint, callback, state), delegate(IAsyncResult asyncResult)
			{
				((Socket)asyncResult.AsyncState).EndConnect(asyncResult);
			}, remoteEP, socket);
		}

		/// <summary>Establishes a connection to a remote host. The host is specified by an IP address and a port number.</summary>
		/// <param name="socket">The socket to perform the connect operation on.</param>
		/// <param name="address">The IP address of the remote host.</param>
		/// <param name="port">The port number of the remote host.</param>
		// Token: 0x06003FAD RID: 16301 RVA: 0x000D93F4 File Offset: 0x000D75F4
		public static Task ConnectAsync(this Socket socket, IPAddress address, int port)
		{
			return Task.Factory.FromAsync<IPAddress, int>((IPAddress targetAddress, int targetPort, AsyncCallback callback, object state) => ((Socket)state).BeginConnect(targetAddress, targetPort, callback, state), delegate(IAsyncResult asyncResult)
			{
				((Socket)asyncResult.AsyncState).EndConnect(asyncResult);
			}, address, port, socket);
		}

		/// <summary>Establishes a connection to a remote host. The host is specified by an array of IP addresses and a port number.</summary>
		/// <param name="socket">The socket that the connect operation is performed on.</param>
		/// <param name="addresses">The IP addresses of the remote host.</param>
		/// <param name="port">The port number of the remote host.</param>
		/// <returns>A task that represents the asynchronous connect operation.</returns>
		// Token: 0x06003FAE RID: 16302 RVA: 0x000D944C File Offset: 0x000D764C
		public static Task ConnectAsync(this Socket socket, IPAddress[] addresses, int port)
		{
			return Task.Factory.FromAsync<IPAddress[], int>((IPAddress[] targetAddresses, int targetPort, AsyncCallback callback, object state) => ((Socket)state).BeginConnect(targetAddresses, targetPort, callback, state), delegate(IAsyncResult asyncResult)
			{
				((Socket)asyncResult.AsyncState).EndConnect(asyncResult);
			}, addresses, port, socket);
		}

		/// <summary>Establishes a connection to a remote host. The host is specified by a host name and a port number.</summary>
		/// <param name="socket">The socket to perform the connect operation on.</param>
		/// <param name="host">The name of the remote host.</param>
		/// <param name="port">The port number of the remote host.</param>
		/// <returns>An asynchronous task.</returns>
		// Token: 0x06003FAF RID: 16303 RVA: 0x000D94A4 File Offset: 0x000D76A4
		public static Task ConnectAsync(this Socket socket, string host, int port)
		{
			return Task.Factory.FromAsync<string, int>((string targetHost, int targetPort, AsyncCallback callback, object state) => ((Socket)state).BeginConnect(targetHost, targetPort, callback, state), delegate(IAsyncResult asyncResult)
			{
				((Socket)asyncResult.AsyncState).EndConnect(asyncResult);
			}, host, port, socket);
		}

		/// <summary>Receives data from a connected socket.</summary>
		/// <param name="socket">The socket to perform the receive operation on.</param>
		/// <param name="buffer">An array that is the storage location for the received data.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <returns>A task that represents the asynchronous receive operation. The value of the <paramref name="TResult" /> parameter contains the number of bytes received.</returns>
		// Token: 0x06003FB0 RID: 16304 RVA: 0x000D94FC File Offset: 0x000D76FC
		public static Task<int> ReceiveAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags)
		{
			return Task<int>.Factory.FromAsync<ArraySegment<byte>, SocketFlags>((ArraySegment<byte> targetBuffer, SocketFlags flags, AsyncCallback callback, object state) => ((Socket)state).BeginReceive(targetBuffer.Array, targetBuffer.Offset, targetBuffer.Count, flags, callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndReceive(asyncResult), buffer, socketFlags, socket);
		}

		/// <summary>Receives data from a connected socket.</summary>
		/// <param name="socket">The socket to perform the receive operation on.</param>
		/// <param name="buffers">An array that is the storage location for the received data.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <returns>A task that represents the asynchronous receive operation. The value of the <paramref name="TResult" /> parameter contains the number of bytes received.</returns>
		// Token: 0x06003FB1 RID: 16305 RVA: 0x000D9554 File Offset: 0x000D7754
		public static Task<int> ReceiveAsync(this Socket socket, IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
		{
			return Task<int>.Factory.FromAsync<IList<ArraySegment<byte>>, SocketFlags>((IList<ArraySegment<byte>> targetBuffers, SocketFlags flags, AsyncCallback callback, object state) => ((Socket)state).BeginReceive(targetBuffers, flags, callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndReceive(asyncResult), buffers, socketFlags, socket);
		}

		/// <summary>Receives data from a specified network device.</summary>
		/// <param name="socket">The socket to perform the ReceiveFrom operation on.</param>
		/// <param name="buffer">An array of type Byte that is the storage location for the received data.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="remoteEndPoint">An EndPoint that represents the source of the data.</param>
		/// <returns>An asynchronous Task that completes with a SocketReceiveFromResult struct.</returns>
		// Token: 0x06003FB2 RID: 16306 RVA: 0x000D95AC File Offset: 0x000D77AC
		public static Task<SocketReceiveFromResult> ReceiveFromAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags, EndPoint remoteEndPoint)
		{
			object[] state2 = new object[]
			{
				socket,
				remoteEndPoint
			};
			return Task<SocketReceiveFromResult>.Factory.FromAsync<ArraySegment<byte>, SocketFlags>(delegate(ArraySegment<byte> targetBuffer, SocketFlags flags, AsyncCallback callback, object state)
			{
				object[] array = (object[])state;
				Socket socket2 = (Socket)array[0];
				EndPoint endPoint = (EndPoint)array[1];
				IAsyncResult result = socket2.BeginReceiveFrom(targetBuffer.Array, targetBuffer.Offset, targetBuffer.Count, flags, ref endPoint, callback, state);
				array[1] = endPoint;
				return result;
			}, delegate(IAsyncResult asyncResult)
			{
				object[] array = (object[])asyncResult.AsyncState;
				Socket socket2 = (Socket)array[0];
				EndPoint remoteEndPoint2 = (EndPoint)array[1];
				int receivedBytes = socket2.EndReceiveFrom(asyncResult, ref remoteEndPoint2);
				return new SocketReceiveFromResult
				{
					ReceivedBytes = receivedBytes,
					RemoteEndPoint = remoteEndPoint2
				};
			}, buffer, socketFlags, state2);
		}

		/// <summary>Receives the specified number of bytes of data into the specified location of the data buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />, and stores the endpoint and packet information.</summary>
		/// <param name="socket">The socket to perform the operation on.</param>
		/// <param name="buffer">An array that is the storage location for received data.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="remoteEndPoint">An <see cref="T:System.Net.EndPoint" />, that represents the remote server.</param>
		/// <returns>An asynchronous Task that completes with a <see cref="T:System.Net.Sockets.SocketReceiveMessageFromResult" /> struct.</returns>
		// Token: 0x06003FB3 RID: 16307 RVA: 0x000D9614 File Offset: 0x000D7814
		public static Task<SocketReceiveMessageFromResult> ReceiveMessageFromAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags, EndPoint remoteEndPoint)
		{
			object[] state2 = new object[]
			{
				socket,
				socketFlags,
				remoteEndPoint
			};
			return Task<SocketReceiveMessageFromResult>.Factory.FromAsync<ArraySegment<byte>>(delegate(ArraySegment<byte> targetBuffer, AsyncCallback callback, object state)
			{
				object[] array = (object[])state;
				Socket socket2 = (Socket)array[0];
				SocketFlags socketFlags2 = (SocketFlags)array[1];
				EndPoint endPoint = (EndPoint)array[2];
				IAsyncResult result = socket2.BeginReceiveMessageFrom(targetBuffer.Array, targetBuffer.Offset, targetBuffer.Count, socketFlags2, ref endPoint, callback, state);
				array[2] = endPoint;
				return result;
			}, delegate(IAsyncResult asyncResult)
			{
				object[] array = (object[])asyncResult.AsyncState;
				Socket socket2 = (Socket)array[0];
				SocketFlags socketFlags2 = (SocketFlags)array[1];
				EndPoint remoteEndPoint2 = (EndPoint)array[2];
				IPPacketInformation packetInformation;
				int receivedBytes = socket2.EndReceiveMessageFrom(asyncResult, ref socketFlags2, ref remoteEndPoint2, out packetInformation);
				return new SocketReceiveMessageFromResult
				{
					PacketInformation = packetInformation,
					ReceivedBytes = receivedBytes,
					RemoteEndPoint = remoteEndPoint2,
					SocketFlags = socketFlags2
				};
			}, buffer, state2);
		}

		/// <summary>Sends data to a connected socket.</summary>
		/// <param name="socket">The socket to perform the operation on.</param>
		/// <param name="buffer">An array of type Byte that contains the data to send.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <returns>An asynchronous task that completes with number of bytes sent to the socket if the operation was successful. Otherwise, the task will complete with an invalid socket error.</returns>
		// Token: 0x06003FB4 RID: 16308 RVA: 0x000D9684 File Offset: 0x000D7884
		public static Task<int> SendAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags)
		{
			return Task<int>.Factory.FromAsync<ArraySegment<byte>, SocketFlags>((ArraySegment<byte> targetBuffer, SocketFlags flags, AsyncCallback callback, object state) => ((Socket)state).BeginSend(targetBuffer.Array, targetBuffer.Offset, targetBuffer.Count, flags, callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndSend(asyncResult), buffer, socketFlags, socket);
		}

		/// <summary>Sends data to a connected socket.</summary>
		/// <param name="socket">The socket to perform the operation on.</param>
		/// <param name="buffers">An array that contains the data to send.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <returns>An asynchronous task that completes with number of bytes sent to the socket if the operation was successful. Otherwise, the task will complete with an invalid socket error.</returns>
		// Token: 0x06003FB5 RID: 16309 RVA: 0x000D96DC File Offset: 0x000D78DC
		public static Task<int> SendAsync(this Socket socket, IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
		{
			return Task<int>.Factory.FromAsync<IList<ArraySegment<byte>>, SocketFlags>((IList<ArraySegment<byte>> targetBuffers, SocketFlags flags, AsyncCallback callback, object state) => ((Socket)state).BeginSend(targetBuffers, flags, callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndSend(asyncResult), buffers, socketFlags, socket);
		}

		/// <summary>Sends data asynchronously to a specific remote host.</summary>
		/// <param name="socket">The socket to perform the operation on.</param>
		/// <param name="buffer">An array that contains the data to send.</param>
		/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
		/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" /> that represents the remote device.</param>
		/// <returns>An asynchronous task that completes with number of bytes sent if the operation was successful. Otherwise, the task will complete with an invalid socket error.</returns>
		// Token: 0x06003FB6 RID: 16310 RVA: 0x000D9734 File Offset: 0x000D7934
		public static Task<int> SendToAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags, EndPoint remoteEP)
		{
			return Task<int>.Factory.FromAsync<ArraySegment<byte>, SocketFlags, EndPoint>((ArraySegment<byte> targetBuffer, SocketFlags flags, EndPoint endPoint, AsyncCallback callback, object state) => ((Socket)state).BeginSendTo(targetBuffer.Array, targetBuffer.Offset, targetBuffer.Count, flags, endPoint, callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndSendTo(asyncResult), buffer, socketFlags, remoteEP, socket);
		}

		// Token: 0x06003FB7 RID: 16311 RVA: 0x000D978D File Offset: 0x000D798D
		public static ValueTask<int> SendAsync(this Socket socket, ReadOnlyMemory<byte> buffer, SocketFlags socketFlags, CancellationToken cancellationToken = default(CancellationToken))
		{
			return socket.SendAsync(buffer, socketFlags, cancellationToken);
		}

		// Token: 0x06003FB8 RID: 16312 RVA: 0x000D9798 File Offset: 0x000D7998
		public static ValueTask<int> ReceiveAsync(this Socket socket, Memory<byte> memory, SocketFlags socketFlags, CancellationToken cancellationToken = default(CancellationToken))
		{
			TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>(socket);
			byte[] buffer = memory.ToArray();
			socket.BeginReceive(buffer, 0, memory.Length, socketFlags, delegate(IAsyncResult iar)
			{
				cancellationToken.ThrowIfCancellationRequested();
				Memory<byte> memory2 = new Memory<byte>(buffer);
				memory2.CopyTo(memory);
				TaskCompletionSource<int> taskCompletionSource2 = (TaskCompletionSource<int>)iar.AsyncState;
				Socket socket2 = (Socket)taskCompletionSource2.Task.AsyncState;
				try
				{
					taskCompletionSource2.TrySetResult(socket2.EndReceive(iar));
				}
				catch (Exception exception)
				{
					taskCompletionSource2.TrySetException(exception);
				}
			}, taskCompletionSource);
			cancellationToken.ThrowIfCancellationRequested();
			return new ValueTask<int>(taskCompletionSource.Task);
		}

		// Token: 0x020007C8 RID: 1992
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06003FB9 RID: 16313 RVA: 0x000D980E File Offset: 0x000D7A0E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06003FBA RID: 16314 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c()
			{
			}

			// Token: 0x06003FBB RID: 16315 RVA: 0x000D981A File Offset: 0x000D7A1A
			internal IAsyncResult <AcceptAsync>b__0_0(AsyncCallback callback, object state)
			{
				return ((Socket)state).BeginAccept(callback, state);
			}

			// Token: 0x06003FBC RID: 16316 RVA: 0x000D9829 File Offset: 0x000D7A29
			internal Socket <AcceptAsync>b__0_1(IAsyncResult asyncResult)
			{
				return ((Socket)asyncResult.AsyncState).EndAccept(asyncResult);
			}

			// Token: 0x06003FBD RID: 16317 RVA: 0x000D983C File Offset: 0x000D7A3C
			internal IAsyncResult <AcceptAsync>b__1_0(Socket socketForAccept, int receiveSize, AsyncCallback callback, object state)
			{
				return ((Socket)state).BeginAccept(socketForAccept, receiveSize, callback, state);
			}

			// Token: 0x06003FBE RID: 16318 RVA: 0x000D9829 File Offset: 0x000D7A29
			internal Socket <AcceptAsync>b__1_1(IAsyncResult asyncResult)
			{
				return ((Socket)asyncResult.AsyncState).EndAccept(asyncResult);
			}

			// Token: 0x06003FBF RID: 16319 RVA: 0x000C3060 File Offset: 0x000C1260
			internal IAsyncResult <ConnectAsync>b__2_0(EndPoint targetEndPoint, AsyncCallback callback, object state)
			{
				return ((Socket)state).BeginConnect(targetEndPoint, callback, state);
			}

			// Token: 0x06003FC0 RID: 16320 RVA: 0x000C3070 File Offset: 0x000C1270
			internal void <ConnectAsync>b__2_1(IAsyncResult asyncResult)
			{
				((Socket)asyncResult.AsyncState).EndConnect(asyncResult);
			}

			// Token: 0x06003FC1 RID: 16321 RVA: 0x000D984F File Offset: 0x000D7A4F
			internal IAsyncResult <ConnectAsync>b__3_0(IPAddress targetAddress, int targetPort, AsyncCallback callback, object state)
			{
				return ((Socket)state).BeginConnect(targetAddress, targetPort, callback, state);
			}

			// Token: 0x06003FC2 RID: 16322 RVA: 0x000C3070 File Offset: 0x000C1270
			internal void <ConnectAsync>b__3_1(IAsyncResult asyncResult)
			{
				((Socket)asyncResult.AsyncState).EndConnect(asyncResult);
			}

			// Token: 0x06003FC3 RID: 16323 RVA: 0x000D9862 File Offset: 0x000D7A62
			internal IAsyncResult <ConnectAsync>b__4_0(IPAddress[] targetAddresses, int targetPort, AsyncCallback callback, object state)
			{
				return ((Socket)state).BeginConnect(targetAddresses, targetPort, callback, state);
			}

			// Token: 0x06003FC4 RID: 16324 RVA: 0x000C3070 File Offset: 0x000C1270
			internal void <ConnectAsync>b__4_1(IAsyncResult asyncResult)
			{
				((Socket)asyncResult.AsyncState).EndConnect(asyncResult);
			}

			// Token: 0x06003FC5 RID: 16325 RVA: 0x000D9875 File Offset: 0x000D7A75
			internal IAsyncResult <ConnectAsync>b__5_0(string targetHost, int targetPort, AsyncCallback callback, object state)
			{
				return ((Socket)state).BeginConnect(targetHost, targetPort, callback, state);
			}

			// Token: 0x06003FC6 RID: 16326 RVA: 0x000C3070 File Offset: 0x000C1270
			internal void <ConnectAsync>b__5_1(IAsyncResult asyncResult)
			{
				((Socket)asyncResult.AsyncState).EndConnect(asyncResult);
			}

			// Token: 0x06003FC7 RID: 16327 RVA: 0x000D9888 File Offset: 0x000D7A88
			internal IAsyncResult <ReceiveAsync>b__6_0(ArraySegment<byte> targetBuffer, SocketFlags flags, AsyncCallback callback, object state)
			{
				return ((Socket)state).BeginReceive(targetBuffer.Array, targetBuffer.Offset, targetBuffer.Count, flags, callback, state);
			}

			// Token: 0x06003FC8 RID: 16328 RVA: 0x000D98AF File Offset: 0x000D7AAF
			internal int <ReceiveAsync>b__6_1(IAsyncResult asyncResult)
			{
				return ((Socket)asyncResult.AsyncState).EndReceive(asyncResult);
			}

			// Token: 0x06003FC9 RID: 16329 RVA: 0x000D98C2 File Offset: 0x000D7AC2
			internal IAsyncResult <ReceiveAsync>b__7_0(IList<ArraySegment<byte>> targetBuffers, SocketFlags flags, AsyncCallback callback, object state)
			{
				return ((Socket)state).BeginReceive(targetBuffers, flags, callback, state);
			}

			// Token: 0x06003FCA RID: 16330 RVA: 0x000D98AF File Offset: 0x000D7AAF
			internal int <ReceiveAsync>b__7_1(IAsyncResult asyncResult)
			{
				return ((Socket)asyncResult.AsyncState).EndReceive(asyncResult);
			}

			// Token: 0x06003FCB RID: 16331 RVA: 0x000D98D8 File Offset: 0x000D7AD8
			internal IAsyncResult <ReceiveFromAsync>b__8_0(ArraySegment<byte> targetBuffer, SocketFlags flags, AsyncCallback callback, object state)
			{
				object[] array = (object[])state;
				Socket socket = (Socket)array[0];
				EndPoint endPoint = (EndPoint)array[1];
				IAsyncResult result = socket.BeginReceiveFrom(targetBuffer.Array, targetBuffer.Offset, targetBuffer.Count, flags, ref endPoint, callback, state);
				array[1] = endPoint;
				return result;
			}

			// Token: 0x06003FCC RID: 16332 RVA: 0x000D9924 File Offset: 0x000D7B24
			internal SocketReceiveFromResult <ReceiveFromAsync>b__8_1(IAsyncResult asyncResult)
			{
				object[] array = (object[])asyncResult.AsyncState;
				Socket socket = (Socket)array[0];
				EndPoint remoteEndPoint = (EndPoint)array[1];
				int receivedBytes = socket.EndReceiveFrom(asyncResult, ref remoteEndPoint);
				return new SocketReceiveFromResult
				{
					ReceivedBytes = receivedBytes,
					RemoteEndPoint = remoteEndPoint
				};
			}

			// Token: 0x06003FCD RID: 16333 RVA: 0x000D9970 File Offset: 0x000D7B70
			internal IAsyncResult <ReceiveMessageFromAsync>b__9_0(ArraySegment<byte> targetBuffer, AsyncCallback callback, object state)
			{
				object[] array = (object[])state;
				Socket socket = (Socket)array[0];
				SocketFlags socketFlags = (SocketFlags)array[1];
				EndPoint endPoint = (EndPoint)array[2];
				IAsyncResult result = socket.BeginReceiveMessageFrom(targetBuffer.Array, targetBuffer.Offset, targetBuffer.Count, socketFlags, ref endPoint, callback, state);
				array[2] = endPoint;
				return result;
			}

			// Token: 0x06003FCE RID: 16334 RVA: 0x000D99C4 File Offset: 0x000D7BC4
			internal SocketReceiveMessageFromResult <ReceiveMessageFromAsync>b__9_1(IAsyncResult asyncResult)
			{
				object[] array = (object[])asyncResult.AsyncState;
				Socket socket = (Socket)array[0];
				SocketFlags socketFlags = (SocketFlags)array[1];
				EndPoint remoteEndPoint = (EndPoint)array[2];
				IPPacketInformation packetInformation;
				int receivedBytes = socket.EndReceiveMessageFrom(asyncResult, ref socketFlags, ref remoteEndPoint, out packetInformation);
				return new SocketReceiveMessageFromResult
				{
					PacketInformation = packetInformation,
					ReceivedBytes = receivedBytes,
					RemoteEndPoint = remoteEndPoint,
					SocketFlags = socketFlags
				};
			}

			// Token: 0x06003FCF RID: 16335 RVA: 0x000D9A30 File Offset: 0x000D7C30
			internal IAsyncResult <SendAsync>b__10_0(ArraySegment<byte> targetBuffer, SocketFlags flags, AsyncCallback callback, object state)
			{
				return ((Socket)state).BeginSend(targetBuffer.Array, targetBuffer.Offset, targetBuffer.Count, flags, callback, state);
			}

			// Token: 0x06003FD0 RID: 16336 RVA: 0x000D9A57 File Offset: 0x000D7C57
			internal int <SendAsync>b__10_1(IAsyncResult asyncResult)
			{
				return ((Socket)asyncResult.AsyncState).EndSend(asyncResult);
			}

			// Token: 0x06003FD1 RID: 16337 RVA: 0x000D9A6A File Offset: 0x000D7C6A
			internal IAsyncResult <SendAsync>b__11_0(IList<ArraySegment<byte>> targetBuffers, SocketFlags flags, AsyncCallback callback, object state)
			{
				return ((Socket)state).BeginSend(targetBuffers, flags, callback, state);
			}

			// Token: 0x06003FD2 RID: 16338 RVA: 0x000D9A57 File Offset: 0x000D7C57
			internal int <SendAsync>b__11_1(IAsyncResult asyncResult)
			{
				return ((Socket)asyncResult.AsyncState).EndSend(asyncResult);
			}

			// Token: 0x06003FD3 RID: 16339 RVA: 0x000D9A7D File Offset: 0x000D7C7D
			internal IAsyncResult <SendToAsync>b__12_0(ArraySegment<byte> targetBuffer, SocketFlags flags, EndPoint endPoint, AsyncCallback callback, object state)
			{
				return ((Socket)state).BeginSendTo(targetBuffer.Array, targetBuffer.Offset, targetBuffer.Count, flags, endPoint, callback, state);
			}

			// Token: 0x06003FD4 RID: 16340 RVA: 0x000D9AA6 File Offset: 0x000D7CA6
			internal int <SendToAsync>b__12_1(IAsyncResult asyncResult)
			{
				return ((Socket)asyncResult.AsyncState).EndSendTo(asyncResult);
			}

			// Token: 0x04002619 RID: 9753
			public static readonly SocketTaskExtensions.<>c <>9 = new SocketTaskExtensions.<>c();

			// Token: 0x0400261A RID: 9754
			public static Func<AsyncCallback, object, IAsyncResult> <>9__0_0;

			// Token: 0x0400261B RID: 9755
			public static Func<IAsyncResult, Socket> <>9__0_1;

			// Token: 0x0400261C RID: 9756
			public static Func<Socket, int, AsyncCallback, object, IAsyncResult> <>9__1_0;

			// Token: 0x0400261D RID: 9757
			public static Func<IAsyncResult, Socket> <>9__1_1;

			// Token: 0x0400261E RID: 9758
			public static Func<EndPoint, AsyncCallback, object, IAsyncResult> <>9__2_0;

			// Token: 0x0400261F RID: 9759
			public static Action<IAsyncResult> <>9__2_1;

			// Token: 0x04002620 RID: 9760
			public static Func<IPAddress, int, AsyncCallback, object, IAsyncResult> <>9__3_0;

			// Token: 0x04002621 RID: 9761
			public static Action<IAsyncResult> <>9__3_1;

			// Token: 0x04002622 RID: 9762
			public static Func<IPAddress[], int, AsyncCallback, object, IAsyncResult> <>9__4_0;

			// Token: 0x04002623 RID: 9763
			public static Action<IAsyncResult> <>9__4_1;

			// Token: 0x04002624 RID: 9764
			public static Func<string, int, AsyncCallback, object, IAsyncResult> <>9__5_0;

			// Token: 0x04002625 RID: 9765
			public static Action<IAsyncResult> <>9__5_1;

			// Token: 0x04002626 RID: 9766
			public static Func<ArraySegment<byte>, SocketFlags, AsyncCallback, object, IAsyncResult> <>9__6_0;

			// Token: 0x04002627 RID: 9767
			public static Func<IAsyncResult, int> <>9__6_1;

			// Token: 0x04002628 RID: 9768
			public static Func<IList<ArraySegment<byte>>, SocketFlags, AsyncCallback, object, IAsyncResult> <>9__7_0;

			// Token: 0x04002629 RID: 9769
			public static Func<IAsyncResult, int> <>9__7_1;

			// Token: 0x0400262A RID: 9770
			public static Func<ArraySegment<byte>, SocketFlags, AsyncCallback, object, IAsyncResult> <>9__8_0;

			// Token: 0x0400262B RID: 9771
			public static Func<IAsyncResult, SocketReceiveFromResult> <>9__8_1;

			// Token: 0x0400262C RID: 9772
			public static Func<ArraySegment<byte>, AsyncCallback, object, IAsyncResult> <>9__9_0;

			// Token: 0x0400262D RID: 9773
			public static Func<IAsyncResult, SocketReceiveMessageFromResult> <>9__9_1;

			// Token: 0x0400262E RID: 9774
			public static Func<ArraySegment<byte>, SocketFlags, AsyncCallback, object, IAsyncResult> <>9__10_0;

			// Token: 0x0400262F RID: 9775
			public static Func<IAsyncResult, int> <>9__10_1;

			// Token: 0x04002630 RID: 9776
			public static Func<IList<ArraySegment<byte>>, SocketFlags, AsyncCallback, object, IAsyncResult> <>9__11_0;

			// Token: 0x04002631 RID: 9777
			public static Func<IAsyncResult, int> <>9__11_1;

			// Token: 0x04002632 RID: 9778
			public static Func<ArraySegment<byte>, SocketFlags, EndPoint, AsyncCallback, object, IAsyncResult> <>9__12_0;

			// Token: 0x04002633 RID: 9779
			public static Func<IAsyncResult, int> <>9__12_1;
		}

		// Token: 0x020007C9 RID: 1993
		[CompilerGenerated]
		private sealed class <>c__DisplayClass14_0
		{
			// Token: 0x06003FD5 RID: 16341 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass14_0()
			{
			}

			// Token: 0x06003FD6 RID: 16342 RVA: 0x000D9ABC File Offset: 0x000D7CBC
			internal void <ReceiveAsync>b__0(IAsyncResult iar)
			{
				this.cancellationToken.ThrowIfCancellationRequested();
				Memory<byte> memory = new Memory<byte>(this.buffer);
				memory.CopyTo(this.memory);
				TaskCompletionSource<int> taskCompletionSource = (TaskCompletionSource<int>)iar.AsyncState;
				Socket socket = (Socket)taskCompletionSource.Task.AsyncState;
				try
				{
					taskCompletionSource.TrySetResult(socket.EndReceive(iar));
				}
				catch (Exception exception)
				{
					taskCompletionSource.TrySetException(exception);
				}
			}

			// Token: 0x04002634 RID: 9780
			public CancellationToken cancellationToken;

			// Token: 0x04002635 RID: 9781
			public byte[] buffer;

			// Token: 0x04002636 RID: 9782
			public Memory<byte> memory;
		}
	}
}
