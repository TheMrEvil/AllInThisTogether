using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Principal;
using System.Threading.Tasks;

namespace System.Net.Security
{
	/// <summary>Provides a stream that uses the Negotiate security protocol to authenticate the client, and optionally the server, in client-server communication.</summary>
	// Token: 0x0200085B RID: 2139
	public class NegotiateStream : AuthenticatedStream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Security.NegotiateStream" /> class using the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="innerStream">A <see cref="T:System.IO.Stream" /> object used by the <see cref="T:System.Net.Security.NegotiateStream" /> for sending and receiving data.</param>
		// Token: 0x06004402 RID: 17410 RVA: 0x000EC80C File Offset: 0x000EAA0C
		[MonoTODO]
		public NegotiateStream(Stream innerStream) : base(innerStream, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Security.NegotiateStream" /> class using the specified <see cref="T:System.IO.Stream" /> and stream closure behavior.</summary>
		/// <param name="innerStream">A <see cref="T:System.IO.Stream" /> object used by the <see cref="T:System.Net.Security.NegotiateStream" /> for sending and receiving data.</param>
		/// <param name="leaveInnerStreamOpen">
		///   <see langword="true" /> to indicate that closing this <see cref="T:System.Net.Security.NegotiateStream" /> has no effect on <paramref name="innerStream" />; <see langword="false" /> to indicate that closing this <see cref="T:System.Net.Security.NegotiateStream" /> also closes <paramref name="innerStream" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="innerStream" /> is <see langword="null" />.  
		/// -or-
		///  <paramref name="innerStream" /> is equal to <see cref="F:System.IO.Stream.Null" />.</exception>
		// Token: 0x06004403 RID: 17411 RVA: 0x000EC816 File Offset: 0x000EAA16
		[MonoTODO]
		public NegotiateStream(Stream innerStream, bool leaveInnerStreamOpen) : base(innerStream, leaveInnerStreamOpen)
		{
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the underlying stream is readable.</summary>
		/// <returns>
		///   <see langword="true" /> if authentication has occurred and the underlying stream is readable; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06004404 RID: 17412 RVA: 0x000EC820 File Offset: 0x000EAA20
		public override bool CanRead
		{
			get
			{
				return base.InnerStream.CanRead;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the underlying stream is seekable.</summary>
		/// <returns>This property always returns <see langword="false" />.</returns>
		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06004405 RID: 17413 RVA: 0x000EC82D File Offset: 0x000EAA2D
		public override bool CanSeek
		{
			get
			{
				return base.InnerStream.CanSeek;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the underlying stream supports time-outs.</summary>
		/// <returns>
		///   <see langword="true" /> if the underlying stream supports time-outs; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06004406 RID: 17414 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public override bool CanTimeout
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the underlying stream is writable.</summary>
		/// <returns>
		///   <see langword="true" /> if authentication has occurred and the underlying stream is writable; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06004407 RID: 17415 RVA: 0x000EC83A File Offset: 0x000EAA3A
		public override bool CanWrite
		{
			get
			{
				return base.InnerStream.CanWrite;
			}
		}

		/// <summary>Gets a value that indicates how the server can use the client's credentials.</summary>
		/// <returns>One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values.</returns>
		/// <exception cref="T:System.InvalidOperationException">Authentication failed or has not occurred.</exception>
		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06004408 RID: 17416 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public virtual TokenImpersonationLevel ImpersonationLevel
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether authentication was successful.</summary>
		/// <returns>
		///   <see langword="true" /> if successful authentication occurred; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06004409 RID: 17417 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public override bool IsAuthenticated
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this <see cref="T:System.Net.Security.NegotiateStream" /> uses data encryption.</summary>
		/// <returns>
		///   <see langword="true" /> if data is encrypted before being transmitted over the network and decrypted when it reaches the remote endpoint; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x0600440A RID: 17418 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public override bool IsEncrypted
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether both the server and the client have been authenticated.</summary>
		/// <returns>
		///   <see langword="true" /> if the server has been authenticated; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x0600440B RID: 17419 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public override bool IsMutuallyAuthenticated
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the local side of the connection used by this <see cref="T:System.Net.Security.NegotiateStream" /> was authenticated as the server.</summary>
		/// <returns>
		///   <see langword="true" /> if the local endpoint was successfully authenticated as the server side of the authenticated connection; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x0600440C RID: 17420 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public override bool IsServer
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the data sent using this stream is signed.</summary>
		/// <returns>
		///   <see langword="true" /> if the data is signed before being transmitted; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x0600440D RID: 17421 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public override bool IsSigned
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the length of the underlying stream.</summary>
		/// <returns>A <see cref="T:System.Int64" /> that specifies the length of the underlying stream.</returns>
		/// <exception cref="T:System.NotSupportedException">Getting the value of this property is not supported when the underlying stream is a <see cref="T:System.Net.Sockets.NetworkStream" />.</exception>
		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x0600440E RID: 17422 RVA: 0x00008083 File Offset: 0x00006283
		public override long Length
		{
			get
			{
				return base.InnerStream.Length;
			}
		}

		/// <summary>Gets or sets the current position in the underlying stream.</summary>
		/// <returns>A <see cref="T:System.Int64" /> that specifies the current position in the underlying stream.</returns>
		/// <exception cref="T:System.NotSupportedException">Setting this property is not supported.  
		/// -or-
		///  Getting the value of this property is not supported when the underlying stream is a <see cref="T:System.Net.Sockets.NetworkStream" />.</exception>
		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x0600440F RID: 17423 RVA: 0x00008090 File Offset: 0x00006290
		// (set) Token: 0x06004410 RID: 17424 RVA: 0x000EC847 File Offset: 0x000EAA47
		public override long Position
		{
			get
			{
				return base.InnerStream.Position;
			}
			set
			{
				base.InnerStream.Position = value;
			}
		}

		/// <summary>Gets or sets the amount of time a read operation blocks waiting for data.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that specifies the amount of time that will elapse before a read operation fails.</returns>
		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06004411 RID: 17425 RVA: 0x000EC855 File Offset: 0x000EAA55
		// (set) Token: 0x06004412 RID: 17426 RVA: 0x000EC85D File Offset: 0x000EAA5D
		public override int ReadTimeout
		{
			get
			{
				return this.readTimeout;
			}
			set
			{
				this.readTimeout = value;
			}
		}

		/// <summary>Gets information about the identity of the remote party sharing this authenticated stream.</summary>
		/// <returns>An <see cref="T:System.Security.Principal.IIdentity" /> object that describes the identity of the remote endpoint.</returns>
		/// <exception cref="T:System.InvalidOperationException">Authentication failed or has not occurred.</exception>
		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06004413 RID: 17427 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public virtual IIdentity RemoteIdentity
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the amount of time a write operation blocks waiting for data.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that specifies the amount of time that will elapse before a write operation fails.</returns>
		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06004414 RID: 17428 RVA: 0x000EC866 File Offset: 0x000EAA66
		// (set) Token: 0x06004415 RID: 17429 RVA: 0x000EC86E File Offset: 0x000EAA6E
		public override int WriteTimeout
		{
			get
			{
				return this.writeTimeout;
			}
			set
			{
				this.writeTimeout = value;
			}
		}

		/// <summary>Called by clients to begin an asynchronous operation to authenticate the client, and optionally the server, in a client-server connection. This method does not block.</summary>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object containing information about the operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		// Token: 0x06004416 RID: 17430 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public virtual IAsyncResult BeginAuthenticateAsClient(AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by clients to begin an asynchronous operation to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified credentials and channel binding. This method does not block.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="binding">The <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> that is used for extended protection.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object containing information about the write operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="credential" /> is <see langword="null" />.  
		/// -or-
		///  <paramref name="targetName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x06004417 RID: 17431 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public virtual IAsyncResult BeginAuthenticateAsClient(NetworkCredential credential, ChannelBinding binding, string targetName, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by clients to begin an asynchronous operation to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified credentials. This method does not block.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object containing information about the write operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="credential" /> is <see langword="null" />.  
		/// -or-
		///  <paramref name="targetName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		// Token: 0x06004418 RID: 17432 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public virtual IAsyncResult BeginAuthenticateAsClient(NetworkCredential credential, string targetName, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by clients to begin an asynchronous operation to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified credentials and authentication options. This method does not block.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="allowedImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object containing information about the write operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="credential" /> is <see langword="null" />.  
		/// -or-
		///  <paramref name="targetName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		// Token: 0x06004419 RID: 17433 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public virtual IAsyncResult BeginAuthenticateAsClient(NetworkCredential credential, string targetName, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel allowedImpersonationLevel, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by clients to begin an asynchronous operation to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified credentials, authentication options, and channel binding. This method does not block.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="binding">The <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> that is used for extended protection.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="allowedImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object containing information about the write operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="credential" /> is <see langword="null" />.  
		/// -or-
		///  <paramref name="targetName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x0600441A RID: 17434 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public virtual IAsyncResult BeginAuthenticateAsClient(NetworkCredential credential, ChannelBinding binding, string targetName, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel allowedImpersonationLevel, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		/// <summary>Begins an asynchronous read operation that reads data from the stream and stores it in the specified array.</summary>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that receives the bytes read from the stream.</param>
		/// <param name="offset">The zero-based location in <paramref name="buffer" /> at which to begin storing the data read from this stream.</param>
		/// <param name="count">The maximum number of bytes to read from the stream.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the read operation is complete.</param>
		/// <param name="asyncState">A user-defined object containing information about the read operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> is less than 0.  
		/// -or-
		///  <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.  
		/// -or-
		///  <paramref name="offset" /> plus <paramref name="count" /> is greater than the length of <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.IO.IOException">The read operation failed.  
		/// -or-
		///  Encryption is in use, but the data could not be decrypted.</exception>
		/// <exception cref="T:System.NotSupportedException">There is already a read operation in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has not occurred.</exception>
		// Token: 0x0600441B RID: 17435 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by servers to begin an asynchronous operation to authenticate the client, and optionally the server, in a client-server connection. This method does not block.</summary>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object containing information about the operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		// Token: 0x0600441C RID: 17436 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public virtual IAsyncResult BeginAuthenticateAsServer(AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by servers to begin an asynchronous operation to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified server credentials, authentication options, and extended protection policy. This method does not block.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="policy">The <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> that is used for extended protection.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="requiredImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object containing information about the write operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomChannelBinding" /> and <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomServiceNames" /> on the extended protection policy passed in the <paramref name="policy" /> parameter are both <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="credential" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="requiredImpersonationLevel" /> must be <see cref="F:System.Security.Principal.TokenImpersonationLevel.Identification" />, <see cref="F:System.Security.Principal.TokenImpersonationLevel.Impersonation" />, or <see cref="F:System.Security.Principal.TokenImpersonationLevel.Delegation" />,</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the client. You cannot use the stream to retry authentication as the server.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <paramref name="policy" /> parameter was set to <see cref="F:System.Security.Authentication.ExtendedProtection.PolicyEnforcement.Always" /> on a platform that does not support extended protection.</exception>
		// Token: 0x0600441D RID: 17437 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public virtual IAsyncResult BeginAuthenticateAsServer(NetworkCredential credential, ExtendedProtectionPolicy policy, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel requiredImpersonationLevel, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by servers to begin an asynchronous operation to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified server credentials and authentication options. This method does not block.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="requiredImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object containing information about the operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="credential" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="requiredImpersonationLevel" /> must be <see cref="F:System.Security.Principal.TokenImpersonationLevel.Identification" />, <see cref="F:System.Security.Principal.TokenImpersonationLevel.Impersonation" />, or <see cref="F:System.Security.Principal.TokenImpersonationLevel.Delegation" />,</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the client. You cannot use the stream to retry authentication as the server.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		// Token: 0x0600441E RID: 17438 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public virtual IAsyncResult BeginAuthenticateAsServer(NetworkCredential credential, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel requiredImpersonationLevel, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by servers to begin an asynchronous operation to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified extended protection policy. This method does not block.</summary>
		/// <param name="policy">The <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> that is used for extended protection.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object containing information about the write operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomChannelBinding" /> and <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomServiceNames" /> on the extended protection policy passed in the <paramref name="policy" /> parameter are both <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <paramref name="policy" /> parameter was set to <see cref="F:System.Security.Authentication.ExtendedProtection.PolicyEnforcement.Always" /> on a platform that does not support extended protection.</exception>
		// Token: 0x0600441F RID: 17439 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public virtual IAsyncResult BeginAuthenticateAsServer(ExtendedProtectionPolicy policy, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		/// <summary>Begins an asynchronous write operation that writes <see cref="T:System.Byte" />s from the specified buffer to the stream.</summary>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that supplies the bytes to be written to the stream.</param>
		/// <param name="offset">The zero-based location in <paramref name="buffer" /> at which to begin reading bytes to be written to the stream.</param>
		/// <param name="count">An <see cref="T:System.Int32" /> value that specifies the number of bytes to read from <paramref name="buffer" />.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the write operation is complete.</param>
		/// <param name="asyncState">A user-defined object containing information about the write operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset is less than 0" />.  
		/// -or-
		///  <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.  
		/// -or-
		///  <paramref name="offset" /> plus count is greater than the length of <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.IO.IOException">The write operation failed.  
		/// -or-
		///  Encryption is in use, but the data could not be encrypted.</exception>
		/// <exception cref="T:System.NotSupportedException">There is already a write operation in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has not occurred.</exception>
		// Token: 0x06004420 RID: 17440 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by clients to authenticate the client, and optionally the server, in a client-server connection.</summary>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		// Token: 0x06004421 RID: 17441 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public virtual void AuthenticateAsClient()
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by clients to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified client credential.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetName" /> is <see langword="null" />.</exception>
		// Token: 0x06004422 RID: 17442 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public virtual void AuthenticateAsClient(NetworkCredential credential, string targetName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by clients to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified client credential and the channel binding.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="binding">The <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> that is used for extended protection.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetName" /> is <see langword="null" />.  
		/// -or-
		///  <paramref name="credential" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x06004423 RID: 17443 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public virtual void AuthenticateAsClient(NetworkCredential credential, ChannelBinding binding, string targetName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by clients to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified credential, authentication options, and channel binding.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="binding">The <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> that is used for extended protection.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="allowedImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetName" /> is <see langword="null" />.  
		/// -or-
		///  <paramref name="credential" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="allowedImpersonationLevel" /> is not a valid value.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x06004424 RID: 17444 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public virtual void AuthenticateAsClient(NetworkCredential credential, ChannelBinding binding, string targetName, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel allowedImpersonationLevel)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by clients to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified credentials and authentication options.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="allowedImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="allowedImpersonationLevel" /> is not a valid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetName" /> is null.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		// Token: 0x06004425 RID: 17445 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public virtual void AuthenticateAsClient(NetworkCredential credential, string targetName, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel allowedImpersonationLevel)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by servers to authenticate the client, and optionally the server, in a client-server connection.</summary>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		// Token: 0x06004426 RID: 17446 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public virtual void AuthenticateAsServer()
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by servers to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified extended protection policy.</summary>
		/// <param name="policy">The <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> that is used for extended protection.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomChannelBinding" /> and <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomServiceNames" /> on the extended protection policy passed in the <paramref name="policy" /> parameter are both <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <paramref name="policy" /> parameter was set to <see cref="F:System.Security.Authentication.ExtendedProtection.PolicyEnforcement.Always" /> on a platform that does not support extended protection.</exception>
		// Token: 0x06004427 RID: 17447 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public virtual void AuthenticateAsServer(ExtendedProtectionPolicy policy)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by servers to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified server credentials, authentication options, and extended protection policy.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="policy">The <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> that is used for extended protection.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="requiredImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomChannelBinding" /> and <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomServiceNames" /> on the extended protection policy passed in the <paramref name="policy" /> parameter are both <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="credential" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="requiredImpersonationLevel" /> must be <see cref="F:System.Security.Principal.TokenImpersonationLevel.Identification" />, <see cref="F:System.Security.Principal.TokenImpersonationLevel.Impersonation" />, or <see cref="F:System.Security.Principal.TokenImpersonationLevel.Delegation" />,</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to try to r-authenticate.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the client. You cannot use the stream to retry authentication as the server.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <paramref name="policy" /> parameter was set to <see cref="F:System.Security.Authentication.ExtendedProtection.PolicyEnforcement.Always" /> on a platform that does not support extended protection.</exception>
		// Token: 0x06004428 RID: 17448 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public virtual void AuthenticateAsServer(NetworkCredential credential, ExtendedProtectionPolicy policy, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel requiredImpersonationLevel)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by servers to authenticate the client, and optionally the server, in a client-server connection. The authentication process uses the specified server credentials and authentication options.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the server.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="requiredImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="credential" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="requiredImpersonationLevel" /> must be <see cref="F:System.Security.Principal.TokenImpersonationLevel.Identification" />, <see cref="F:System.Security.Principal.TokenImpersonationLevel.Impersonation" />, or <see cref="F:System.Security.Principal.TokenImpersonationLevel.Delegation" />,</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to try to r-authenticate.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the client. You cannot use the stream to retry authentication as the server.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		// Token: 0x06004429 RID: 17449 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public virtual void AuthenticateAsServer(NetworkCredential credential, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel requiredImpersonationLevel)
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Security.NegotiateStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x0600442A RID: 17450 RVA: 0x000EC877 File Offset: 0x000EAA77
		[MonoTODO]
		protected override void Dispose(bool disposing)
		{
		}

		/// <summary>Ends a pending asynchronous client authentication operation that was started with a call to <see cref="Overload:System.Net.Security.NegotiateStream.BeginAuthenticateAsClient" />.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance returned by a call to <see cref="Overload:System.Net.Security.NegotiateStream.BeginAuthenticateAsClient" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not created by a call to <see cref="Overload:System.Net.Security.NegotiateStream.BeginAuthenticateAsClient" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no pending client authentication to complete.</exception>
		// Token: 0x0600442B RID: 17451 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public virtual void EndAuthenticateAsClient(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		/// <summary>Ends an asynchronous read operation that was started with a call to <see cref="M:System.Net.Security.NegotiateStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" />.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance returned by a call to <see cref="M:System.Net.Security.NegotiateStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /></param>
		/// <returns>A <see cref="T:System.Int32" /> value that specifies the number of bytes read from the underlying stream.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The asyncResult was not created by a call to <see cref="M:System.Net.Security.NegotiateStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no pending read operation to complete.
		/// -or-
		/// Authentication has not occurred.</exception>
		/// <exception cref="T:System.IO.IOException">The read operation failed.</exception>
		// Token: 0x0600442C RID: 17452 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public override int EndRead(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		/// <summary>Ends a pending asynchronous client authentication operation that was started with a call to <see cref="Overload:System.Net.Security.NegotiateStream.BeginAuthenticateAsServer" />.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance returned by a call to <see cref="Overload:System.Net.Security.NegotiateStream.BeginAuthenticateAsServer" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not created by a call to <see cref="Overload:System.Net.Security.NegotiateStream.BeginAuthenticateAsServer" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no pending authentication to complete.</exception>
		// Token: 0x0600442D RID: 17453 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public virtual void EndAuthenticateAsServer(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		/// <summary>Ends an asynchronous write operation that was started with a call to <see cref="M:System.Net.Security.NegotiateStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" />.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance returned by a call to <see cref="M:System.Net.Security.NegotiateStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /></param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The asyncResult was not created by a call to <see cref="M:System.Net.Security.NegotiateStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no pending write operation to complete.
		/// -or-
		/// Authentication has not occurred.</exception>
		/// <exception cref="T:System.IO.IOException">The write operation failed.</exception>
		// Token: 0x0600442E RID: 17454 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public override void EndWrite(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		/// <summary>Causes any buffered data to be written to the underlying device.</summary>
		// Token: 0x0600442F RID: 17455 RVA: 0x00007E6C File Offset: 0x0000606C
		[MonoTODO]
		public override void Flush()
		{
			base.InnerStream.Flush();
		}

		/// <summary>Reads data from this stream and stores it in the specified array.</summary>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that receives the bytes read from the stream.</param>
		/// <param name="offset">A <see cref="T:System.Int32" /> containing the zero-based location in <paramref name="buffer" /> at which to begin storing the data read from this stream.</param>
		/// <param name="count">A <see cref="T:System.Int32" /> containing the maximum number of bytes to read from the stream.</param>
		/// <returns>A <see cref="T:System.Int32" /> value that specifies the number of bytes read from the underlying stream. When there is no more data to be read, returns 0.</returns>
		/// <exception cref="T:System.IO.IOException">The read operation failed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has not occurred.</exception>
		/// <exception cref="T:System.NotSupportedException">A <see cref="M:System.Net.Security.NegotiateStream.Read(System.Byte[],System.Int32,System.Int32)" /> operation is already in progress.</exception>
		// Token: 0x06004430 RID: 17456 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		/// <summary>Throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="offset">This value is ignored.</param>
		/// <param name="origin">This value is ignored.</param>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">Seeking is not supported on <see cref="T:System.Net.Security.NegotiateStream" />.</exception>
		// Token: 0x06004431 RID: 17457 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the length of the underlying stream.</summary>
		/// <param name="value">An <see cref="T:System.Int64" /> value that specifies the length of the stream.</param>
		// Token: 0x06004432 RID: 17458 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Write the specified number of <see cref="T:System.Byte" />s to the underlying stream using the specified buffer and offset.</summary>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that supplies the bytes written to the stream.</param>
		/// <param name="offset">An <see cref="T:System.Int32" /> containing the zero-based location in <paramref name="buffer" /> at which to begin reading bytes to be written to the stream.</param>
		/// <param name="count">A <see cref="T:System.Int32" /> containing the number of bytes to read from <paramref name="buffer" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset is less than 0" />.  
		/// -or-
		///  <paramref name="offset" /> is greater than the length of <paramref name="buffer" />.  
		/// -or-
		///  <paramref name="offset" /> plus count is greater than the length of <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.IO.IOException">The write operation failed.  
		/// -or-
		///  Encryption is in use, but the data could not be encrypted.</exception>
		/// <exception cref="T:System.NotSupportedException">There is already a write operation in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has not occurred.</exception>
		// Token: 0x06004433 RID: 17459 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by clients to authenticate the client, and optionally the server, in a client-server connection as an asynchronous operation.</summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		// Token: 0x06004434 RID: 17460 RVA: 0x000EC87B File Offset: 0x000EAA7B
		public virtual Task AuthenticateAsClientAsync()
		{
			return Task.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginAuthenticateAsClient), new Action<IAsyncResult>(this.EndAuthenticateAsClient), null);
		}

		/// <summary>Called by clients to authenticate the client, and optionally the server, in a client-server connection as an asynchronous operation. The authentication process uses the specified client credential.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetName" /> is <see langword="null" />.</exception>
		// Token: 0x06004435 RID: 17461 RVA: 0x000EC8A2 File Offset: 0x000EAAA2
		public virtual Task AuthenticateAsClientAsync(NetworkCredential credential, string targetName)
		{
			return Task.Factory.FromAsync<NetworkCredential, string>(new Func<NetworkCredential, string, AsyncCallback, object, IAsyncResult>(this.BeginAuthenticateAsClient), new Action<IAsyncResult>(this.EndAuthenticateAsClient), credential, targetName, null);
		}

		/// <summary>Called by clients to authenticate the client, and optionally the server, in a client-server connection as an asynchronous operation. The authentication process uses the specified credentials and authentication options.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="allowedImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="allowedImpersonationLevel" /> is not a valid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetName" /> is null.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		// Token: 0x06004436 RID: 17462 RVA: 0x000EC8CC File Offset: 0x000EAACC
		public virtual Task AuthenticateAsClientAsync(NetworkCredential credential, string targetName, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel allowedImpersonationLevel)
		{
			return Task.Factory.FromAsync((AsyncCallback callback, object state) => this.BeginAuthenticateAsClient(credential, targetName, requiredProtectionLevel, allowedImpersonationLevel, callback, state), new Action<IAsyncResult>(this.EndAuthenticateAsClient), null);
		}

		/// <summary>Called by clients to authenticate the client, and optionally the server, in a client-server connection as an asynchronous operation. The authentication process uses the specified client credential and the channel binding.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="binding">The <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> that is used for extended protection.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetName" /> is <see langword="null" />.  
		/// -or-
		///  <paramref name="credential" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x06004437 RID: 17463 RVA: 0x0000829A File Offset: 0x0000649A
		public virtual Task AuthenticateAsClientAsync(NetworkCredential credential, ChannelBinding binding, string targetName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by clients to authenticate the client, and optionally the server, in a client-server connection as an asynchronous operation. The authentication process uses the specified credential, authentication options, and channel binding.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="binding">The <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> that is used for extended protection.</param>
		/// <param name="targetName">The Service Principal Name (SPN) that uniquely identifies the server to authenticate.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="allowedImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetName" /> is <see langword="null" />.  
		/// -or-
		///  <paramref name="credential" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="allowedImpersonationLevel" /> is not a valid value.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the server. You cannot use the stream to retry authentication as the client.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x06004438 RID: 17464 RVA: 0x0000829A File Offset: 0x0000649A
		public virtual Task AuthenticateAsClientAsync(NetworkCredential credential, ChannelBinding binding, string targetName, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel allowedImpersonationLevel)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by servers to authenticate the client, and optionally the server, in a client-server connection as an asynchronous operation.</summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		// Token: 0x06004439 RID: 17465 RVA: 0x000EC927 File Offset: 0x000EAB27
		public virtual Task AuthenticateAsServerAsync()
		{
			return Task.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginAuthenticateAsServer), new Action<IAsyncResult>(this.EndAuthenticateAsServer), null);
		}

		/// <summary>Called by servers to authenticate the client, and optionally the server, in a client-server connection as an asynchronous operation. The authentication process uses the specified extended protection policy.</summary>
		/// <param name="policy">The <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> that is used for extended protection.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomChannelBinding" /> and <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomServiceNames" /> on the extended protection policy passed in the <paramref name="policy" /> parameter are both <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <paramref name="policy" /> parameter was set to <see cref="F:System.Security.Authentication.ExtendedProtection.PolicyEnforcement.Always" /> on a platform that does not support extended protection.</exception>
		// Token: 0x0600443A RID: 17466 RVA: 0x0000829A File Offset: 0x0000649A
		public virtual Task AuthenticateAsServerAsync(ExtendedProtectionPolicy policy)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by servers to authenticate the client, and optionally the server, in a client-server connection as an asynchronous operation. The authentication process uses the specified server credentials and authentication options.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the server.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="requiredImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="credential" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="requiredImpersonationLevel" /> must be <see cref="F:System.Security.Principal.TokenImpersonationLevel.Identification" />, <see cref="F:System.Security.Principal.TokenImpersonationLevel.Impersonation" />, or <see cref="F:System.Security.Principal.TokenImpersonationLevel.Delegation" />,</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to try to r-authenticate.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the client. You cannot use the stream to retry authentication as the server.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		// Token: 0x0600443B RID: 17467 RVA: 0x0000829A File Offset: 0x0000649A
		public virtual Task AuthenticateAsServerAsync(NetworkCredential credential, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel requiredImpersonationLevel)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by servers to authenticate the client, and optionally the server, in a client-server connection as an asynchronous operation. The authentication process uses the specified server credentials, authentication options, and extended protection policy.</summary>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that is used to establish the identity of the client.</param>
		/// <param name="policy">The <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> that is used for extended protection.</param>
		/// <param name="requiredProtectionLevel">One of the <see cref="T:System.Net.Security.ProtectionLevel" /> values, indicating the security services for the stream.</param>
		/// <param name="requiredImpersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values, indicating how the server can use the client's credentials to access resources.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomChannelBinding" /> and <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomServiceNames" /> on the extended protection policy passed in the <paramref name="policy" /> parameter are both <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="credential" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="requiredImpersonationLevel" /> must be <see cref="F:System.Security.Principal.TokenImpersonationLevel.Identification" />, <see cref="F:System.Security.Principal.TokenImpersonationLevel.Impersonation" />, or <see cref="F:System.Security.Principal.TokenImpersonationLevel.Delegation" />,</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed. You can use this object to try to r-authenticate.</exception>
		/// <exception cref="T:System.Security.Authentication.InvalidCredentialException">The authentication failed. You can use this object to retry the authentication.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		/// -or-
		///  This stream was used previously to attempt authentication as the client. You cannot use the stream to retry authentication as the server.</exception>
		/// <exception cref="T:System.NotSupportedException">Windows 95 and Windows 98 are not supported.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <paramref name="policy" /> parameter was set to <see cref="F:System.Security.Authentication.ExtendedProtection.PolicyEnforcement.Always" /> on a platform that does not support extended protection.</exception>
		// Token: 0x0600443C RID: 17468 RVA: 0x0000829A File Offset: 0x0000649A
		public virtual Task AuthenticateAsServerAsync(NetworkCredential credential, ExtendedProtectionPolicy policy, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel requiredImpersonationLevel)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400291E RID: 10526
		private int readTimeout;

		// Token: 0x0400291F RID: 10527
		private int writeTimeout;

		// Token: 0x0200085C RID: 2140
		[CompilerGenerated]
		private sealed class <>c__DisplayClass69_0
		{
			// Token: 0x0600443D RID: 17469 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass69_0()
			{
			}

			// Token: 0x0600443E RID: 17470 RVA: 0x000EC94E File Offset: 0x000EAB4E
			internal IAsyncResult <AuthenticateAsClientAsync>b__0(AsyncCallback callback, object state)
			{
				return this.<>4__this.BeginAuthenticateAsClient(this.credential, this.targetName, this.requiredProtectionLevel, this.allowedImpersonationLevel, callback, state);
			}

			// Token: 0x04002920 RID: 10528
			public NegotiateStream <>4__this;

			// Token: 0x04002921 RID: 10529
			public NetworkCredential credential;

			// Token: 0x04002922 RID: 10530
			public string targetName;

			// Token: 0x04002923 RID: 10531
			public ProtectionLevel requiredProtectionLevel;

			// Token: 0x04002924 RID: 10532
			public TokenImpersonationLevel allowedImpersonationLevel;
		}
	}
}
