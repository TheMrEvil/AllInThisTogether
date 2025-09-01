using System;
using System.Net.Sockets;

namespace System.Net
{
	/// <summary>Identifies a network address. This is an <see langword="abstract" /> class.</summary>
	// Token: 0x020005CB RID: 1483
	[Serializable]
	public abstract class EndPoint
	{
		/// <summary>Gets the address family to which the endpoint belongs.</summary>
		/// <returns>One of the <see cref="T:System.Net.Sockets.AddressFamily" /> values.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property when the property is not overridden in a descendant class.</exception>
		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06003006 RID: 12294 RVA: 0x000A5C43 File Offset: 0x000A3E43
		public virtual AddressFamily AddressFamily
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>Serializes endpoint information into a <see cref="T:System.Net.SocketAddress" /> instance.</summary>
		/// <returns>A <see cref="T:System.Net.SocketAddress" /> instance that contains the endpoint information.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method when the method is not overridden in a descendant class.</exception>
		// Token: 0x06003007 RID: 12295 RVA: 0x000A5C4A File Offset: 0x000A3E4A
		public virtual SocketAddress Serialize()
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>Creates an <see cref="T:System.Net.EndPoint" /> instance from a <see cref="T:System.Net.SocketAddress" /> instance.</summary>
		/// <param name="socketAddress">The socket address that serves as the endpoint for a connection.</param>
		/// <returns>A new <see cref="T:System.Net.EndPoint" /> instance that is initialized from the specified <see cref="T:System.Net.SocketAddress" /> instance.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method when the method is not overridden in a descendant class.</exception>
		// Token: 0x06003008 RID: 12296 RVA: 0x000A5C4A File Offset: 0x000A3E4A
		public virtual EndPoint Create(SocketAddress socketAddress)
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.EndPoint" /> class.</summary>
		// Token: 0x06003009 RID: 12297 RVA: 0x0000219B File Offset: 0x0000039B
		protected EndPoint()
		{
		}
	}
}
