using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>Provides a response from a Uniform Resource Identifier (URI). This is an <see langword="abstract" /> class.</summary>
	// Token: 0x0200061C RID: 1564
	[Serializable]
	public abstract class WebResponse : MarshalByRefObject, ISerializable, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebResponse" /> class.</summary>
		// Token: 0x06003195 RID: 12693 RVA: 0x0002D758 File Offset: 0x0002B958
		protected WebResponse()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebResponse" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">An instance of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class that contains the information required to serialize the new <see cref="T:System.Net.WebRequest" /> instance.</param>
		/// <param name="streamingContext">An instance of the <see cref="T:System.Runtime.Serialization.StreamingContext" /> class that indicates the source of the serialized stream that is associated with the new <see cref="T:System.Net.WebRequest" /> instance.</param>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to access the constructor, when the constructor is not overridden in a descendant class.</exception>
		// Token: 0x06003196 RID: 12694 RVA: 0x0002D758 File Offset: 0x0002B958
		protected WebResponse(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data that is needed to serialize <see cref="T:System.Net.WebResponse" />.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that will hold the serialized data for the <see cref="T:System.Net.WebResponse" />.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the destination of the serialized stream that is associated with the new <see cref="T:System.Net.WebResponse" />.</param>
		// Token: 0x06003197 RID: 12695 RVA: 0x000ABB1C File Offset: 0x000A9D1C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data that is needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06003198 RID: 12696 RVA: 0x00003917 File Offset: 0x00001B17
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected virtual void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
		}

		/// <summary>When overridden by a descendant class, closes the response stream.</summary>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to access the method, when the method is not overridden in a descendant class.</exception>
		// Token: 0x06003199 RID: 12697 RVA: 0x00003917 File Offset: 0x00001B17
		public virtual void Close()
		{
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.WebResponse" /> object.</summary>
		// Token: 0x0600319A RID: 12698 RVA: 0x000ABB26 File Offset: 0x000A9D26
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.WebResponse" /> object, and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to releases only unmanaged resources.</param>
		// Token: 0x0600319B RID: 12699 RVA: 0x000ABB38 File Offset: 0x000A9D38
		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			try
			{
				this.Close();
			}
			catch
			{
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this response was obtained from the cache.</summary>
		/// <returns>
		///   <see langword="true" /> if the response was taken from the cache; otherwise, <see langword="false" />.</returns>
		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x0600319C RID: 12700 RVA: 0x000ABB68 File Offset: 0x000A9D68
		public virtual bool IsFromCache
		{
			get
			{
				return this.m_IsFromCache;
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (set) Token: 0x0600319D RID: 12701 RVA: 0x000ABB70 File Offset: 0x000A9D70
		internal bool InternalSetFromCache
		{
			set
			{
				this.m_IsFromCache = value;
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x0600319E RID: 12702 RVA: 0x000ABB79 File Offset: 0x000A9D79
		internal virtual bool IsCacheFresh
		{
			get
			{
				return this.m_IsCacheFresh;
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (set) Token: 0x0600319F RID: 12703 RVA: 0x000ABB81 File Offset: 0x000A9D81
		internal bool InternalSetIsCacheFresh
		{
			set
			{
				this.m_IsCacheFresh = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether mutual authentication occurred.</summary>
		/// <returns>
		///   <see langword="true" /> if both client and server were authenticated; otherwise, <see langword="false" />.</returns>
		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x060031A0 RID: 12704 RVA: 0x00003062 File Offset: 0x00001262
		public virtual bool IsMutuallyAuthenticated
		{
			get
			{
				return false;
			}
		}

		/// <summary>When overridden in a descendant class, gets or sets the content length of data being received.</summary>
		/// <returns>The number of bytes returned from the Internet resource.</returns>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x060031A1 RID: 12705 RVA: 0x000A5C43 File Offset: 0x000A3E43
		// (set) Token: 0x060031A2 RID: 12706 RVA: 0x000A5C43 File Offset: 0x000A3E43
		public virtual long ContentLength
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the content type of the data being received.</summary>
		/// <returns>A string that contains the content type of the response.</returns>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x060031A3 RID: 12707 RVA: 0x000A5C43 File Offset: 0x000A3E43
		// (set) Token: 0x060031A4 RID: 12708 RVA: 0x000A5C43 File Offset: 0x000A3E43
		public virtual string ContentType
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a descendant class, returns the data stream from the Internet resource.</summary>
		/// <returns>An instance of the <see cref="T:System.IO.Stream" /> class for reading data from the Internet resource.</returns>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to access the method, when the method is not overridden in a descendant class.</exception>
		// Token: 0x060031A5 RID: 12709 RVA: 0x000A5C4A File Offset: 0x000A3E4A
		public virtual Stream GetResponseStream()
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>When overridden in a derived class, gets the URI of the Internet resource that actually responded to the request.</summary>
		/// <returns>An instance of the <see cref="T:System.Uri" /> class that contains the URI of the Internet resource that actually responded to the request.</returns>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x060031A6 RID: 12710 RVA: 0x000A5C43 File Offset: 0x000A3E43
		public virtual Uri ResponseUri
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a derived class, gets a collection of header name-value pairs associated with this request.</summary>
		/// <returns>An instance of the <see cref="T:System.Net.WebHeaderCollection" /> class that contains header values associated with this response.</returns>
		/// <exception cref="T:System.NotSupportedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x060031A7 RID: 12711 RVA: 0x000A5C43 File Offset: 0x000A3E43
		public virtual WebHeaderCollection Headers
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>Gets a value that indicates if headers are supported.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.  
		///  <see langword="true" /> if headers are supported; otherwise, <see langword="false" />.</returns>
		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x060031A8 RID: 12712 RVA: 0x00003062 File Offset: 0x00001262
		public virtual bool SupportsHeaders
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001CC7 RID: 7367
		private bool m_IsCacheFresh;

		// Token: 0x04001CC8 RID: 7368
		private bool m_IsFromCache;
	}
}
