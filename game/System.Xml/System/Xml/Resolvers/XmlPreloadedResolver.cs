using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace System.Xml.Resolvers
{
	/// <summary>Represents a class that is used to prepopulate the cache with DTDs or XML streams.</summary>
	// Token: 0x0200060F RID: 1551
	public class XmlPreloadedResolver : XmlResolver
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> class.</summary>
		// Token: 0x06003FC6 RID: 16326 RVA: 0x00162E95 File Offset: 0x00161095
		public XmlPreloadedResolver() : this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> class with the specified preloaded well-known DTDs.</summary>
		/// <param name="preloadedDtds">The well-known DTDs that should be prepopulated into the cache.</param>
		// Token: 0x06003FC7 RID: 16327 RVA: 0x00162E9E File Offset: 0x0016109E
		public XmlPreloadedResolver(XmlKnownDtds preloadedDtds) : this(null, preloadedDtds, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> class with the specified fallback resolver.</summary>
		/// <param name="fallbackResolver">The <see langword="XmlResolver" />, <see langword="XmlXapResolver" />, or your own resolver.</param>
		// Token: 0x06003FC8 RID: 16328 RVA: 0x00162EA9 File Offset: 0x001610A9
		public XmlPreloadedResolver(XmlResolver fallbackResolver) : this(fallbackResolver, XmlKnownDtds.All, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> class with the specified fallback resolver and preloaded well-known DTDs.</summary>
		/// <param name="fallbackResolver">The <see langword="XmlResolver" />, <see langword="XmlXapResolver" />, or your own resolver.</param>
		/// <param name="preloadedDtds">The well-known DTDs that should be prepopulated into the cache.</param>
		// Token: 0x06003FC9 RID: 16329 RVA: 0x00162EB8 File Offset: 0x001610B8
		public XmlPreloadedResolver(XmlResolver fallbackResolver, XmlKnownDtds preloadedDtds) : this(fallbackResolver, preloadedDtds, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> class with the specified fallback resolver, preloaded well-known DTDs, and URI equality comparer.</summary>
		/// <param name="fallbackResolver">The <see langword="XmlResolver" />, <see langword="XmlXapResolver" />, or your own resolver.</param>
		/// <param name="preloadedDtds">The well-known DTDs that should be prepopulated into cache.</param>
		/// <param name="uriComparer">The implementation of the <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> generic interface to use when you compare URIs.</param>
		// Token: 0x06003FCA RID: 16330 RVA: 0x00162EC4 File Offset: 0x001610C4
		public XmlPreloadedResolver(XmlResolver fallbackResolver, XmlKnownDtds preloadedDtds, IEqualityComparer<Uri> uriComparer)
		{
			this._fallbackResolver = fallbackResolver;
			this._mappings = new Dictionary<Uri, XmlPreloadedResolver.PreloadedData>(16, uriComparer);
			this._preloadedDtds = preloadedDtds;
			if (preloadedDtds != XmlKnownDtds.None)
			{
				if ((preloadedDtds & XmlKnownDtds.Xhtml10) != XmlKnownDtds.None)
				{
					this.AddKnownDtd(XmlPreloadedResolver.s_xhtml10_Dtd);
				}
				if ((preloadedDtds & XmlKnownDtds.Rss091) != XmlKnownDtds.None)
				{
					this.AddKnownDtd(XmlPreloadedResolver.s_rss091_Dtd);
				}
			}
		}

		/// <summary>Resolves the absolute URI from the base and relative URIs.</summary>
		/// <param name="baseUri">The base URI used to resolve the relative URI.</param>
		/// <param name="relativeUri">The URI to resolve. The URI can be absolute or relative. If absolute, this value effectively replaces the <paramref name="baseUri" /> value. If relative, it combines with the <paramref name="baseUri" /> to make an absolute URI.</param>
		/// <returns>The <see cref="T:System.Uri" /> representing the absolute URI or <see langword="null" /> if the relative URI cannot be resolved.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="uri" /> is <see langword="null" />.</exception>
		// Token: 0x06003FCB RID: 16331 RVA: 0x00162F18 File Offset: 0x00161118
		public override Uri ResolveUri(Uri baseUri, string relativeUri)
		{
			if (relativeUri != null && relativeUri.StartsWith("-//", StringComparison.CurrentCulture))
			{
				if ((this._preloadedDtds & XmlKnownDtds.Xhtml10) != XmlKnownDtds.None && relativeUri.StartsWith("-//W3C//", StringComparison.CurrentCulture))
				{
					for (int i = 0; i < XmlPreloadedResolver.s_xhtml10_Dtd.Length; i++)
					{
						if (relativeUri == XmlPreloadedResolver.s_xhtml10_Dtd[i].publicId)
						{
							return new Uri(relativeUri, UriKind.Relative);
						}
					}
				}
				if ((this._preloadedDtds & XmlKnownDtds.Rss091) != XmlKnownDtds.None && relativeUri == XmlPreloadedResolver.s_rss091_Dtd[0].publicId)
				{
					return new Uri(relativeUri, UriKind.Relative);
				}
			}
			return base.ResolveUri(baseUri, relativeUri);
		}

		/// <summary>Maps a URI to an object that contains the actual resource.</summary>
		/// <param name="absoluteUri">The URI returned from <see cref="M:System.Xml.XmlResolver.ResolveUri(System.Uri,System.String)" />.</param>
		/// <param name="role">The current version of the .NET Framework for Silverlight does not use this parameter when resolving URIs. This parameter is provided for future extensibility purposes. For example, this parameter can be mapped to the xlink:role and used as an implementation-specific argument in other scenarios.</param>
		/// <param name="ofObjectToReturn">The type of object to return. The <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> supports <see cref="T:System.IO.Stream" /> objects and <see cref="T:System.IO.TextReader" /> objects for URIs that were added as <see langword="String" />. If the requested type is not supported by the resolver, an exception will be thrown. Use the <see cref="M:System.Xml.Resolvers.XmlPreloadedResolver.SupportsType(System.Uri,System.Type)" /> method to determine whether a certain <see langword="Type" /> is supported by this resolver.</param>
		/// <returns>A <see cref="T:System.IO.Stream" /> or <see cref="T:System.IO.TextReader" /> object that corresponds to the actual source.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="absoluteUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Xml.XmlException">Cannot resolve URI passed in <paramref name="absoluteUri" />.-or-
		///         <paramref name="ofObjectToReturn" /> is not of a supported type.</exception>
		// Token: 0x06003FCC RID: 16332 RVA: 0x00162FAC File Offset: 0x001611AC
		public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			if (absoluteUri == null)
			{
				throw new ArgumentNullException("absoluteUri");
			}
			XmlPreloadedResolver.PreloadedData preloadedData;
			if (!this._mappings.TryGetValue(absoluteUri, out preloadedData))
			{
				if (this._fallbackResolver != null)
				{
					return this._fallbackResolver.GetEntity(absoluteUri, role, ofObjectToReturn);
				}
				throw new XmlException(SR.Format("Cannot resolve '{0}'.", absoluteUri.ToString()));
			}
			else
			{
				if (ofObjectToReturn == null || ofObjectToReturn == typeof(Stream) || ofObjectToReturn == typeof(object))
				{
					return preloadedData.AsStream();
				}
				if (ofObjectToReturn == typeof(TextReader))
				{
					return preloadedData.AsTextReader();
				}
				throw new XmlException("Object type is not supported.");
			}
		}

		/// <summary>Sets the credentials that are used to authenticate the underlying <see cref="T:System.Net.WebRequest" />.</summary>
		/// <returns>The credentials that are used to authenticate the underlying web request.</returns>
		// Token: 0x17000C15 RID: 3093
		// (set) Token: 0x06003FCD RID: 16333 RVA: 0x00163061 File Offset: 0x00161261
		public override ICredentials Credentials
		{
			set
			{
				if (this._fallbackResolver != null)
				{
					this._fallbackResolver.Credentials = value;
				}
			}
		}

		/// <summary>Determines whether the resolver supports other <see cref="T:System.Type" />s than just <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="absoluteUri">The absolute URI to check.</param>
		/// <param name="type">The <see cref="T:System.Type" /> to return.</param>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Type" /> is supported; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="uri" /> is <see langword="null" />.</exception>
		// Token: 0x06003FCE RID: 16334 RVA: 0x00163078 File Offset: 0x00161278
		public override bool SupportsType(Uri absoluteUri, Type type)
		{
			if (absoluteUri == null)
			{
				throw new ArgumentNullException("absoluteUri");
			}
			XmlPreloadedResolver.PreloadedData preloadedData;
			if (this._mappings.TryGetValue(absoluteUri, out preloadedData))
			{
				return preloadedData.SupportsType(type);
			}
			if (this._fallbackResolver != null)
			{
				return this._fallbackResolver.SupportsType(absoluteUri, type);
			}
			return base.SupportsType(absoluteUri, type);
		}

		/// <summary>Adds a byte array to the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> store and maps it to a URI. If the store already contains a mapping for the same URI, the existing mapping is overridden.</summary>
		/// <param name="uri">The URI of the data that is being added to the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> store.</param>
		/// <param name="value">A byte array with the data that corresponds to the provided URI.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="uri" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003FCF RID: 16335 RVA: 0x001630CF File Offset: 0x001612CF
		public void Add(Uri uri, byte[] value)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.Add(uri, new XmlPreloadedResolver.ByteArrayChunk(value, 0, value.Length));
		}

		/// <summary>Adds a byte array to the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> store and maps it to a URI. If the store already contains a mapping for the same URI, the existing mapping is overridden.</summary>
		/// <param name="uri">The URI of the data that is being added to the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> store.</param>
		/// <param name="value">A byte array with the data that corresponds to the provided URI.</param>
		/// <param name="offset">The offset in the provided byte array where the data starts.</param>
		/// <param name="count">The number of bytes to read from the byte array, starting at the provided offset.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="uri" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="offset" /> or <paramref name="count" /> is less than 0.-or-The length of the <paramref name="value" /> minus <paramref name="offset" /> is less than <paramref name="count." /></exception>
		// Token: 0x06003FD0 RID: 16336 RVA: 0x00163104 File Offset: 0x00161304
		public void Add(Uri uri, byte[] value, int offset, int count)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (value.Length - offset < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.Add(uri, new XmlPreloadedResolver.ByteArrayChunk(value, offset, count));
		}

		/// <summary>Adds a <see cref="T:System.IO.Stream" /> to the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> store and maps it to a URI. If the store already contains a mapping for the same URI, the existing mapping is overridden.</summary>
		/// <param name="uri">The URI of the data that is being added to the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> store.</param>
		/// <param name="value">A <see cref="T:System.IO.Stream" /> with the data that corresponds to the provided URI.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="uri" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003FD1 RID: 16337 RVA: 0x00163178 File Offset: 0x00161378
		public void Add(Uri uri, Stream value)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			checked
			{
				if (value.CanSeek)
				{
					int num = (int)value.Length;
					byte[] array = new byte[num];
					value.Read(array, 0, num);
					this.Add(uri, new XmlPreloadedResolver.ByteArrayChunk(array));
					return;
				}
				MemoryStream memoryStream = new MemoryStream();
				byte[] array2 = new byte[4096];
				int count;
				while ((count = value.Read(array2, 0, array2.Length)) > 0)
				{
					memoryStream.Write(array2, 0, count);
				}
				int num2 = (int)memoryStream.Position;
				byte[] array3 = new byte[num2];
				Array.Copy(memoryStream.ToArray(), array3, num2);
				this.Add(uri, new XmlPreloadedResolver.ByteArrayChunk(array3));
			}
		}

		/// <summary>Adds a string with preloaded data to the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> store and maps it to a URI. If the store already contains a mapping for the same URI, the existing mapping is overridden.</summary>
		/// <param name="uri">The URI of the data that is being added to the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> store.</param>
		/// <param name="value">A <see langword="String" /> with the data that corresponds to the provided URI.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="uri" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003FD2 RID: 16338 RVA: 0x00163233 File Offset: 0x00161433
		public void Add(Uri uri, string value)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.Add(uri, new XmlPreloadedResolver.StringData(value));
		}

		/// <summary>Gets a collection of preloaded URIs.</summary>
		/// <returns>The collection of preloaded URIs.</returns>
		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06003FD3 RID: 16339 RVA: 0x00163264 File Offset: 0x00161464
		public IEnumerable<Uri> PreloadedUris
		{
			get
			{
				return this._mappings.Keys;
			}
		}

		/// <summary>Removes the data that corresponds to the URI from the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" />.</summary>
		/// <param name="uri">The URI of the data that should be removed from the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> store.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="uri" /> is <see langword="null" />.</exception>
		// Token: 0x06003FD4 RID: 16340 RVA: 0x00163271 File Offset: 0x00161471
		public void Remove(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			this._mappings.Remove(uri);
		}

		// Token: 0x06003FD5 RID: 16341 RVA: 0x00163294 File Offset: 0x00161494
		private void Add(Uri uri, XmlPreloadedResolver.PreloadedData data)
		{
			if (this._mappings.ContainsKey(uri))
			{
				this._mappings[uri] = data;
				return;
			}
			this._mappings.Add(uri, data);
		}

		// Token: 0x06003FD6 RID: 16342 RVA: 0x001632C0 File Offset: 0x001614C0
		private void AddKnownDtd(XmlPreloadedResolver.XmlKnownDtdData[] dtdSet)
		{
			foreach (XmlPreloadedResolver.XmlKnownDtdData xmlKnownDtdData in dtdSet)
			{
				this._mappings.Add(new Uri(xmlKnownDtdData.publicId, UriKind.RelativeOrAbsolute), xmlKnownDtdData);
				this._mappings.Add(new Uri(xmlKnownDtdData.systemId, UriKind.RelativeOrAbsolute), xmlKnownDtdData);
			}
		}

		/// <summary>Asynchronously maps a URI to an object that contains the actual resource.</summary>
		/// <param name="absoluteUri">The URI returned from <see cref="M:System.Xml.XmlResolver.ResolveUri(System.Uri,System.String)" />.</param>
		/// <param name="role">The current version of the .NET Framework for Silverlight does not use this parameter when resolving URIs. This parameter is provided for future extensibility purposes. For example, this parameter can be mapped to the xlink:role and used as an implementation-specific argument in other scenarios.</param>
		/// <param name="ofObjectToReturn">The type of object to return. The <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> supports <see cref="T:System.IO.Stream" /> objects and <see cref="T:System.IO.TextReader" /> objects for URIs that were added as <see langword="String" />. If the requested type is not supported by the resolver, an exception will be thrown. Use the <see cref="M:System.Xml.Resolvers.XmlPreloadedResolver.SupportsType(System.Uri,System.Type)" /> method to determine whether a certain <see langword="Type" /> is supported by this resolver.</param>
		/// <returns>A <see cref="T:System.IO.Stream" /> or <see cref="T:System.IO.TextReader" /> object that corresponds to the actual source.</returns>
		// Token: 0x06003FD7 RID: 16343 RVA: 0x00163310 File Offset: 0x00161510
		public override Task<object> GetEntityAsync(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			if (absoluteUri == null)
			{
				throw new ArgumentNullException("absoluteUri");
			}
			XmlPreloadedResolver.PreloadedData preloadedData;
			if (!this._mappings.TryGetValue(absoluteUri, out preloadedData))
			{
				if (this._fallbackResolver != null)
				{
					return this._fallbackResolver.GetEntityAsync(absoluteUri, role, ofObjectToReturn);
				}
				throw new XmlException(SR.Format("Cannot resolve '{0}'.", absoluteUri.ToString()));
			}
			else
			{
				if (ofObjectToReturn == null || ofObjectToReturn == typeof(Stream) || ofObjectToReturn == typeof(object))
				{
					return Task.FromResult<object>(preloadedData.AsStream());
				}
				if (ofObjectToReturn == typeof(TextReader))
				{
					return Task.FromResult<object>(preloadedData.AsTextReader());
				}
				throw new XmlException("Object type is not supported.");
			}
		}

		// Token: 0x06003FD8 RID: 16344 RVA: 0x001633D0 File Offset: 0x001615D0
		// Note: this type is marked as 'beforefieldinit'.
		static XmlPreloadedResolver()
		{
		}

		// Token: 0x04002DC1 RID: 11713
		private XmlResolver _fallbackResolver;

		// Token: 0x04002DC2 RID: 11714
		private Dictionary<Uri, XmlPreloadedResolver.PreloadedData> _mappings;

		// Token: 0x04002DC3 RID: 11715
		private XmlKnownDtds _preloadedDtds;

		// Token: 0x04002DC4 RID: 11716
		private static XmlPreloadedResolver.XmlKnownDtdData[] s_xhtml10_Dtd = new XmlPreloadedResolver.XmlKnownDtdData[]
		{
			new XmlPreloadedResolver.XmlKnownDtdData("-//W3C//DTD XHTML 1.0 Strict//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd", "xhtml1-strict.dtd"),
			new XmlPreloadedResolver.XmlKnownDtdData("-//W3C//DTD XHTML 1.0 Transitional//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd", "xhtml1-transitional.dtd"),
			new XmlPreloadedResolver.XmlKnownDtdData("-//W3C//DTD XHTML 1.0 Frameset//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd", "xhtml1-frameset.dtd"),
			new XmlPreloadedResolver.XmlKnownDtdData("-//W3C//ENTITIES Latin 1 for XHTML//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml-lat1.ent", "xhtml-lat1.ent"),
			new XmlPreloadedResolver.XmlKnownDtdData("-//W3C//ENTITIES Symbols for XHTML//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml-symbol.ent", "xhtml-symbol.ent"),
			new XmlPreloadedResolver.XmlKnownDtdData("-//W3C//ENTITIES Special for XHTML//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml-special.ent", "xhtml-special.ent")
		};

		// Token: 0x04002DC5 RID: 11717
		private static XmlPreloadedResolver.XmlKnownDtdData[] s_rss091_Dtd = new XmlPreloadedResolver.XmlKnownDtdData[]
		{
			new XmlPreloadedResolver.XmlKnownDtdData("-//Netscape Communications//DTD RSS 0.91//EN", "http://my.netscape.com/publish/formats/rss-0.91.dtd", "rss-0.91.dtd")
		};

		// Token: 0x02000610 RID: 1552
		private abstract class PreloadedData
		{
			// Token: 0x06003FD9 RID: 16345
			internal abstract Stream AsStream();

			// Token: 0x06003FDA RID: 16346 RVA: 0x00163494 File Offset: 0x00161694
			internal virtual TextReader AsTextReader()
			{
				throw new XmlException("Object type is not supported.");
			}

			// Token: 0x06003FDB RID: 16347 RVA: 0x001634A0 File Offset: 0x001616A0
			internal virtual bool SupportsType(Type type)
			{
				return type == null || type == typeof(Stream);
			}

			// Token: 0x06003FDC RID: 16348 RVA: 0x0000216B File Offset: 0x0000036B
			protected PreloadedData()
			{
			}
		}

		// Token: 0x02000611 RID: 1553
		private class XmlKnownDtdData : XmlPreloadedResolver.PreloadedData
		{
			// Token: 0x06003FDD RID: 16349 RVA: 0x001634C0 File Offset: 0x001616C0
			internal XmlKnownDtdData(string publicId, string systemId, string resourceName)
			{
				this.publicId = publicId;
				this.systemId = systemId;
				this._resourceName = resourceName;
			}

			// Token: 0x06003FDE RID: 16350 RVA: 0x001634DD File Offset: 0x001616DD
			internal override Stream AsStream()
			{
				return base.GetType().Assembly.GetManifestResourceStream(this._resourceName);
			}

			// Token: 0x04002DC6 RID: 11718
			internal string publicId;

			// Token: 0x04002DC7 RID: 11719
			internal string systemId;

			// Token: 0x04002DC8 RID: 11720
			private string _resourceName;
		}

		// Token: 0x02000612 RID: 1554
		private class ByteArrayChunk : XmlPreloadedResolver.PreloadedData
		{
			// Token: 0x06003FDF RID: 16351 RVA: 0x001634F5 File Offset: 0x001616F5
			internal ByteArrayChunk(byte[] array) : this(array, 0, array.Length)
			{
			}

			// Token: 0x06003FE0 RID: 16352 RVA: 0x00163502 File Offset: 0x00161702
			internal ByteArrayChunk(byte[] array, int offset, int length)
			{
				this._array = array;
				this._offset = offset;
				this._length = length;
			}

			// Token: 0x06003FE1 RID: 16353 RVA: 0x0016351F File Offset: 0x0016171F
			internal override Stream AsStream()
			{
				return new MemoryStream(this._array, this._offset, this._length);
			}

			// Token: 0x04002DC9 RID: 11721
			private byte[] _array;

			// Token: 0x04002DCA RID: 11722
			private int _offset;

			// Token: 0x04002DCB RID: 11723
			private int _length;
		}

		// Token: 0x02000613 RID: 1555
		private class StringData : XmlPreloadedResolver.PreloadedData
		{
			// Token: 0x06003FE2 RID: 16354 RVA: 0x00163538 File Offset: 0x00161738
			internal StringData(string str)
			{
				this._str = str;
			}

			// Token: 0x06003FE3 RID: 16355 RVA: 0x00163547 File Offset: 0x00161747
			internal override Stream AsStream()
			{
				return new MemoryStream(Encoding.Unicode.GetBytes(this._str));
			}

			// Token: 0x06003FE4 RID: 16356 RVA: 0x0016355E File Offset: 0x0016175E
			internal override TextReader AsTextReader()
			{
				return new StringReader(this._str);
			}

			// Token: 0x06003FE5 RID: 16357 RVA: 0x0016356B File Offset: 0x0016176B
			internal override bool SupportsType(Type type)
			{
				return type == typeof(TextReader) || base.SupportsType(type);
			}

			// Token: 0x04002DCC RID: 11724
			private string _str;
		}
	}
}
