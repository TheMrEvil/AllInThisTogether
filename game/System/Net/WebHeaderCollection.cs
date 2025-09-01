using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace System.Net
{
	/// <summary>Contains protocol headers associated with a request or response.</summary>
	// Token: 0x0200060A RID: 1546
	[ComVisible(true)]
	[Serializable]
	public class WebHeaderCollection : NameValueCollection, ISerializable
	{
		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x060030BD RID: 12477 RVA: 0x000A78A3 File Offset: 0x000A5AA3
		internal string ContentLength
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[1]);
				}
				return this.m_CommonHeaders[1];
			}
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x060030BE RID: 12478 RVA: 0x000A78C3 File Offset: 0x000A5AC3
		internal string CacheControl
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[2]);
				}
				return this.m_CommonHeaders[2];
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x060030BF RID: 12479 RVA: 0x000A78E3 File Offset: 0x000A5AE3
		internal string ContentType
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[3]);
				}
				return this.m_CommonHeaders[3];
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x060030C0 RID: 12480 RVA: 0x000A7903 File Offset: 0x000A5B03
		internal string Date
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[4]);
				}
				return this.m_CommonHeaders[4];
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x060030C1 RID: 12481 RVA: 0x000A7923 File Offset: 0x000A5B23
		internal string Expires
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[5]);
				}
				return this.m_CommonHeaders[5];
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x060030C2 RID: 12482 RVA: 0x000A7943 File Offset: 0x000A5B43
		internal string ETag
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[6]);
				}
				return this.m_CommonHeaders[6];
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x060030C3 RID: 12483 RVA: 0x000A7963 File Offset: 0x000A5B63
		internal string LastModified
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[7]);
				}
				return this.m_CommonHeaders[7];
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x060030C4 RID: 12484 RVA: 0x000A7983 File Offset: 0x000A5B83
		internal string Location
		{
			get
			{
				return WebHeaderCollection.HeaderEncoding.DecodeUtf8FromString((this.m_CommonHeaders != null) ? this.m_CommonHeaders[8] : this.Get(WebHeaderCollection.s_CommonHeaderNames[8]));
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x060030C5 RID: 12485 RVA: 0x000A79A9 File Offset: 0x000A5BA9
		internal string ProxyAuthenticate
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[9]);
				}
				return this.m_CommonHeaders[9];
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x060030C6 RID: 12486 RVA: 0x000A79CB File Offset: 0x000A5BCB
		internal string SetCookie2
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[11]);
				}
				return this.m_CommonHeaders[11];
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x060030C7 RID: 12487 RVA: 0x000A79ED File Offset: 0x000A5BED
		internal string SetCookie
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[12]);
				}
				return this.m_CommonHeaders[12];
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x060030C8 RID: 12488 RVA: 0x000A7A0F File Offset: 0x000A5C0F
		internal string Server
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[13]);
				}
				return this.m_CommonHeaders[13];
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x060030C9 RID: 12489 RVA: 0x000A7A31 File Offset: 0x000A5C31
		internal string Via
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[14]);
				}
				return this.m_CommonHeaders[14];
			}
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x000A7A54 File Offset: 0x000A5C54
		private void NormalizeCommonHeaders()
		{
			if (this.m_CommonHeaders == null)
			{
				return;
			}
			for (int i = 0; i < this.m_CommonHeaders.Length; i++)
			{
				if (this.m_CommonHeaders[i] != null)
				{
					this.InnerCollection.Add(WebHeaderCollection.s_CommonHeaderNames[i], this.m_CommonHeaders[i]);
				}
			}
			this.m_CommonHeaders = null;
			this.m_NumCommonHeaders = 0;
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x060030CB RID: 12491 RVA: 0x000A7AAF File Offset: 0x000A5CAF
		private NameValueCollection InnerCollection
		{
			get
			{
				if (this.m_InnerCollection == null)
				{
					this.m_InnerCollection = new NameValueCollection(16, CaseInsensitiveAscii.StaticInstance);
				}
				return this.m_InnerCollection;
			}
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x000A7AD4 File Offset: 0x000A5CD4
		internal static bool AllowMultiValues(string name)
		{
			HeaderInfo headerInfo = WebHeaderCollection.HInfo[name];
			return headerInfo.AllowMultiValues || headerInfo.HeaderName == "";
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x060030CD RID: 12493 RVA: 0x000A7B07 File Offset: 0x000A5D07
		private bool AllowHttpRequestHeader
		{
			get
			{
				if (this.m_Type == WebHeaderCollectionType.Unknown)
				{
					this.m_Type = WebHeaderCollectionType.WebRequest;
				}
				return this.m_Type == WebHeaderCollectionType.WebRequest || this.m_Type == WebHeaderCollectionType.HttpWebRequest || this.m_Type == WebHeaderCollectionType.HttpListenerRequest;
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x060030CE RID: 12494 RVA: 0x000A7B35 File Offset: 0x000A5D35
		internal bool AllowHttpResponseHeader
		{
			get
			{
				if (this.m_Type == WebHeaderCollectionType.Unknown)
				{
					this.m_Type = WebHeaderCollectionType.WebResponse;
				}
				return this.m_Type == WebHeaderCollectionType.WebResponse || this.m_Type == WebHeaderCollectionType.HttpWebResponse || this.m_Type == WebHeaderCollectionType.HttpListenerResponse;
			}
		}

		/// <summary>Gets or sets the specified request header.</summary>
		/// <param name="header">The request header value.</param>
		/// <returns>A <see cref="T:System.String" /> instance containing the specified header value.</returns>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpRequestHeader" />.</exception>
		// Token: 0x170009CD RID: 2509
		public string this[HttpRequestHeader header]
		{
			get
			{
				if (!this.AllowHttpRequestHeader)
				{
					throw new InvalidOperationException(SR.GetString("This collection holds response headers and cannot contain the specified request header."));
				}
				return base[UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.ToString((int)header)];
			}
			set
			{
				if (!this.AllowHttpRequestHeader)
				{
					throw new InvalidOperationException(SR.GetString("This collection holds response headers and cannot contain the specified request header."));
				}
				base[UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.ToString((int)header)] = value;
			}
		}

		/// <summary>Gets or sets the specified response header.</summary>
		/// <param name="header">The response header value.</param>
		/// <returns>A <see cref="T:System.String" /> instance containing the specified header.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpResponseHeader" />.</exception>
		// Token: 0x170009CE RID: 2510
		public string this[HttpResponseHeader header]
		{
			get
			{
				if (!this.AllowHttpResponseHeader)
				{
					throw new InvalidOperationException(SR.GetString("This collection holds request headers and cannot contain the specified response header."));
				}
				if (this.m_CommonHeaders != null)
				{
					if (header == HttpResponseHeader.ProxyAuthenticate)
					{
						return this.m_CommonHeaders[9];
					}
					if (header == HttpResponseHeader.WwwAuthenticate)
					{
						return this.m_CommonHeaders[15];
					}
				}
				return base[UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.ToString((int)header)];
			}
			set
			{
				if (!this.AllowHttpResponseHeader)
				{
					throw new InvalidOperationException(SR.GetString("This collection holds request headers and cannot contain the specified response header."));
				}
				if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.GetString("Header values cannot be longer than {0} characters.", new object[]
					{
						ushort.MaxValue
					}));
				}
				base[UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.ToString((int)header)] = value;
			}
		}

		/// <summary>Inserts the specified header with the specified value into the collection.</summary>
		/// <param name="header">The header to add to the collection.</param>
		/// <param name="value">The content of the header.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpRequestHeader" />.</exception>
		// Token: 0x060030D3 RID: 12499 RVA: 0x000A7C80 File Offset: 0x000A5E80
		public void Add(HttpRequestHeader header, string value)
		{
			if (!this.AllowHttpRequestHeader)
			{
				throw new InvalidOperationException(SR.GetString("This collection holds response headers and cannot contain the specified request header."));
			}
			this.Add(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.ToString((int)header), value);
		}

		/// <summary>Inserts the specified header with the specified value into the collection.</summary>
		/// <param name="header">The header to add to the collection.</param>
		/// <param name="value">The content of the header.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpResponseHeader" />.</exception>
		// Token: 0x060030D4 RID: 12500 RVA: 0x000A7CA8 File Offset: 0x000A5EA8
		public void Add(HttpResponseHeader header, string value)
		{
			if (!this.AllowHttpResponseHeader)
			{
				throw new InvalidOperationException(SR.GetString("This collection holds request headers and cannot contain the specified response header."));
			}
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", value, SR.GetString("Header values cannot be longer than {0} characters.", new object[]
				{
					ushort.MaxValue
				}));
			}
			this.Add(UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.ToString((int)header), value);
		}

		/// <summary>Sets the specified header to the specified value.</summary>
		/// <param name="header">The <see cref="T:System.Net.HttpRequestHeader" /> value to set.</param>
		/// <param name="value">The content of the header to set.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpRequestHeader" />.</exception>
		// Token: 0x060030D5 RID: 12501 RVA: 0x000A7D1C File Offset: 0x000A5F1C
		public void Set(HttpRequestHeader header, string value)
		{
			if (!this.AllowHttpRequestHeader)
			{
				throw new InvalidOperationException(SR.GetString("This collection holds response headers and cannot contain the specified request header."));
			}
			this.Set(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.ToString((int)header), value);
		}

		/// <summary>Sets the specified header to the specified value.</summary>
		/// <param name="header">The <see cref="T:System.Net.HttpResponseHeader" /> value to set.</param>
		/// <param name="value">The content of the header to set.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpResponseHeader" />.</exception>
		// Token: 0x060030D6 RID: 12502 RVA: 0x000A7D44 File Offset: 0x000A5F44
		public void Set(HttpResponseHeader header, string value)
		{
			if (!this.AllowHttpResponseHeader)
			{
				throw new InvalidOperationException(SR.GetString("This collection holds request headers and cannot contain the specified response header."));
			}
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", value, SR.GetString("Header values cannot be longer than {0} characters.", new object[]
				{
					ushort.MaxValue
				}));
			}
			this.Set(UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.ToString((int)header), value);
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x000A7DB8 File Offset: 0x000A5FB8
		internal void SetInternal(HttpResponseHeader header, string value)
		{
			if (!this.AllowHttpResponseHeader)
			{
				throw new InvalidOperationException(SR.GetString("This collection holds request headers and cannot contain the specified response header."));
			}
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", value, SR.GetString("Header values cannot be longer than {0} characters.", new object[]
				{
					ushort.MaxValue
				}));
			}
			this.SetInternal(UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.ToString((int)header), value);
		}

		/// <summary>Removes the specified header from the collection.</summary>
		/// <param name="header">The <see cref="T:System.Net.HttpRequestHeader" /> instance to remove from the collection.</param>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpRequestHeader" />.</exception>
		// Token: 0x060030D8 RID: 12504 RVA: 0x000A7E2C File Offset: 0x000A602C
		public void Remove(HttpRequestHeader header)
		{
			if (!this.AllowHttpRequestHeader)
			{
				throw new InvalidOperationException(SR.GetString("This collection holds response headers and cannot contain the specified request header."));
			}
			this.Remove(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.ToString((int)header));
		}

		/// <summary>Removes the specified header from the collection.</summary>
		/// <param name="header">The <see cref="T:System.Net.HttpResponseHeader" /> instance to remove from the collection.</param>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpResponseHeader" />.</exception>
		// Token: 0x060030D9 RID: 12505 RVA: 0x000A7E52 File Offset: 0x000A6052
		public void Remove(HttpResponseHeader header)
		{
			if (!this.AllowHttpResponseHeader)
			{
				throw new InvalidOperationException(SR.GetString("This collection holds request headers and cannot contain the specified response header."));
			}
			this.Remove(UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.ToString((int)header));
		}

		/// <summary>Inserts a header into the collection without checking whether the header is on the restricted header list.</summary>
		/// <param name="headerName">The header to add to the collection.</param>
		/// <param name="headerValue">The content of the header.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="headerName" /> is <see langword="null" />, <see cref="F:System.String.Empty" />, or contains invalid characters.  
		/// -or-  
		/// <paramref name="headerValue" /> contains invalid characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="headerName" /> is not <see langword="null" /> and the length of <paramref name="headerValue" /> is too long (greater than 65,535 characters).</exception>
		// Token: 0x060030DA RID: 12506 RVA: 0x000A7E78 File Offset: 0x000A6078
		protected void AddWithoutValidate(string headerName, string headerValue)
		{
			headerName = WebHeaderCollection.CheckBadChars(headerName, false);
			headerValue = WebHeaderCollection.CheckBadChars(headerValue, true);
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && headerValue != null && headerValue.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("headerValue", headerValue, SR.GetString("Header values cannot be longer than {0} characters.", new object[]
				{
					ushort.MaxValue
				}));
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Add(headerName, headerValue);
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x000A7EF4 File Offset: 0x000A60F4
		internal void SetAddVerified(string name, string value)
		{
			if (WebHeaderCollection.HInfo[name].AllowMultiValues)
			{
				this.NormalizeCommonHeaders();
				base.InvalidateCachedArrays();
				this.InnerCollection.Add(name, value);
				return;
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Set(name, value);
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x000A7F46 File Offset: 0x000A6146
		internal void AddInternal(string name, string value)
		{
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Add(name, value);
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x000A7F61 File Offset: 0x000A6161
		internal void ChangeInternal(string name, string value)
		{
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Set(name, value);
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x000A7F7C File Offset: 0x000A617C
		internal void RemoveInternal(string name)
		{
			this.NormalizeCommonHeaders();
			if (this.m_InnerCollection != null)
			{
				base.InvalidateCachedArrays();
				this.m_InnerCollection.Remove(name);
			}
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x000A7F9E File Offset: 0x000A619E
		internal void CheckUpdate(string name, string value)
		{
			value = WebHeaderCollection.CheckBadChars(value, true);
			this.ChangeInternal(name, value);
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x000A7FB1 File Offset: 0x000A61B1
		private void AddInternalNotCommon(string name, string value)
		{
			base.InvalidateCachedArrays();
			this.InnerCollection.Add(name, value);
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x000A7FC8 File Offset: 0x000A61C8
		internal static string CheckBadChars(string name, bool isHeaderValue)
		{
			if (name != null && name.Length != 0)
			{
				if (isHeaderValue)
				{
					name = name.Trim(WebHeaderCollection.HttpTrimCharacters);
					int num = 0;
					for (int i = 0; i < name.Length; i++)
					{
						char c = 'ÿ' & name[i];
						switch (num)
						{
						case 0:
							if (c == '\r')
							{
								num = 1;
							}
							else if (c == '\n')
							{
								num = 2;
							}
							else if (c == '\u007f' || (c < ' ' && c != '\t'))
							{
								throw new ArgumentException(SR.GetString("Specified value has invalid Control characters."), "value");
							}
							break;
						case 1:
							if (c != '\n')
							{
								throw new ArgumentException(SR.GetString("Specified value has invalid CRLF characters."), "value");
							}
							num = 2;
							break;
						case 2:
							if (c != ' ' && c != '\t')
							{
								throw new ArgumentException(SR.GetString("Specified value has invalid CRLF characters."), "value");
							}
							num = 0;
							break;
						}
					}
					if (num != 0)
					{
						throw new ArgumentException(SR.GetString("Specified value has invalid CRLF characters."), "value");
					}
				}
				else
				{
					if (name.IndexOfAny(ValidationHelper.InvalidParamChars) != -1)
					{
						throw new ArgumentException(SR.GetString("Specified value has invalid HTTP Header characters."), "name");
					}
					if (WebHeaderCollection.ContainsNonAsciiChars(name))
					{
						throw new ArgumentException(SR.GetString("Specified value has invalid non-ASCII characters."), "name");
					}
				}
				return name;
			}
			if (!isHeaderValue)
			{
				throw (name == null) ? new ArgumentNullException("name") : new ArgumentException(SR.GetString("The parameter '{0}' cannot be an empty string.", new object[]
				{
					"name"
				}), "name");
			}
			return string.Empty;
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x000A813A File Offset: 0x000A633A
		internal static bool IsValidToken(string token)
		{
			return token.Length > 0 && token.IndexOfAny(ValidationHelper.InvalidParamChars) == -1 && !WebHeaderCollection.ContainsNonAsciiChars(token);
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x000A8160 File Offset: 0x000A6360
		internal static bool ContainsNonAsciiChars(string token)
		{
			for (int i = 0; i < token.Length; i++)
			{
				if (token[i] < ' ' || token[i] > '~')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x000A8198 File Offset: 0x000A6398
		internal void ThrowOnRestrictedHeader(string headerName)
		{
			if (this.m_Type == WebHeaderCollectionType.HttpWebRequest)
			{
				if (WebHeaderCollection.HInfo[headerName].IsRequestRestricted)
				{
					throw new ArgumentException(SR.GetString("The '{0}' header must be modified using the appropriate property or method.", new object[]
					{
						headerName
					}), "name");
				}
			}
			else if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && WebHeaderCollection.HInfo[headerName].IsResponseRestricted)
			{
				throw new ArgumentException(SR.GetString("The '{0}' header must be modified using the appropriate property or method.", new object[]
				{
					headerName
				}), "name");
			}
		}

		/// <summary>Inserts a header with the specified name and value into the collection.</summary>
		/// <param name="name">The header to add to the collection.</param>
		/// <param name="value">The content of the header.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is <see langword="null" />, <see cref="F:System.String.Empty" />, or contains invalid characters.  
		/// -or-  
		/// <paramref name="name" /> is a restricted header that must be set with a property setting.  
		/// -or-  
		/// <paramref name="value" /> contains invalid characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535.</exception>
		// Token: 0x060030E5 RID: 12517 RVA: 0x000A821C File Offset: 0x000A641C
		public override void Add(string name, string value)
		{
			name = WebHeaderCollection.CheckBadChars(name, false);
			this.ThrowOnRestrictedHeader(name);
			value = WebHeaderCollection.CheckBadChars(value, true);
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", value, SR.GetString("Header values cannot be longer than {0} characters.", new object[]
				{
					ushort.MaxValue
				}));
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Add(name, value);
		}

		/// <summary>Inserts the specified header into the collection.</summary>
		/// <param name="header">The header to add, with the name and value separated by a colon.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="header" /> is <see langword="null" /> or <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="header" /> does not contain a colon (:) character.  
		/// The length of <paramref name="value" /> is greater than 65535.  
		/// -or-  
		/// The name part of <paramref name="header" /> is <see cref="F:System.String.Empty" /> or contains invalid characters.  
		/// -or-  
		/// <paramref name="header" /> is a restricted header that should be set with a property.  
		/// -or-  
		/// The value part of <paramref name="header" /> contains invalid characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length the string after the colon (:) is greater than 65535.</exception>
		// Token: 0x060030E6 RID: 12518 RVA: 0x000A82A0 File Offset: 0x000A64A0
		public void Add(string header)
		{
			if (ValidationHelper.IsBlankString(header))
			{
				throw new ArgumentNullException("header");
			}
			int num = header.IndexOf(':');
			if (num < 0)
			{
				throw new ArgumentException(SR.GetString("Specified value does not have a ':' separator."), "header");
			}
			string text = header.Substring(0, num);
			string text2 = header.Substring(num + 1);
			text = WebHeaderCollection.CheckBadChars(text, false);
			this.ThrowOnRestrictedHeader(text);
			text2 = WebHeaderCollection.CheckBadChars(text2, true);
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && text2 != null && text2.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", text2, SR.GetString("Header values cannot be longer than {0} characters.", new object[]
				{
					ushort.MaxValue
				}));
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Add(text, text2);
		}

		/// <summary>Sets the specified header to the specified value.</summary>
		/// <param name="name">The header to set.</param>
		/// <param name="value">The content of the header to set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" /> or <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is a restricted header.  
		/// -or-  
		/// <paramref name="name" /> or <paramref name="value" /> contain invalid characters.</exception>
		// Token: 0x060030E7 RID: 12519 RVA: 0x000A8368 File Offset: 0x000A6568
		public override void Set(string name, string value)
		{
			if (ValidationHelper.IsBlankString(name))
			{
				throw new ArgumentNullException("name");
			}
			name = WebHeaderCollection.CheckBadChars(name, false);
			this.ThrowOnRestrictedHeader(name);
			value = WebHeaderCollection.CheckBadChars(value, true);
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", value, SR.GetString("Header values cannot be longer than {0} characters.", new object[]
				{
					ushort.MaxValue
				}));
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Set(name, value);
		}

		// Token: 0x060030E8 RID: 12520 RVA: 0x000A83FC File Offset: 0x000A65FC
		internal void SetInternal(string name, string value)
		{
			if (ValidationHelper.IsBlankString(name))
			{
				throw new ArgumentNullException("name");
			}
			name = WebHeaderCollection.CheckBadChars(name, false);
			value = WebHeaderCollection.CheckBadChars(value, true);
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", value, SR.GetString("Header values cannot be longer than {0} characters.", new object[]
				{
					ushort.MaxValue
				}));
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Set(name, value);
		}

		/// <summary>Removes the specified header from the collection.</summary>
		/// <param name="name">The name of the header to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" /><see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is a restricted header.  
		/// -or-  
		/// <paramref name="name" /> contains invalid characters.</exception>
		// Token: 0x060030E9 RID: 12521 RVA: 0x000A848C File Offset: 0x000A668C
		public override void Remove(string name)
		{
			if (ValidationHelper.IsBlankString(name))
			{
				throw new ArgumentNullException("name");
			}
			this.ThrowOnRestrictedHeader(name);
			name = WebHeaderCollection.CheckBadChars(name, false);
			this.NormalizeCommonHeaders();
			if (this.m_InnerCollection != null)
			{
				base.InvalidateCachedArrays();
				this.m_InnerCollection.Remove(name);
			}
		}

		/// <summary>Gets an array of header values stored in a header.</summary>
		/// <param name="header">The header to return.</param>
		/// <returns>An array of header strings.</returns>
		// Token: 0x060030EA RID: 12522 RVA: 0x000A84DC File Offset: 0x000A66DC
		public override string[] GetValues(string header)
		{
			this.NormalizeCommonHeaders();
			HeaderInfo headerInfo = WebHeaderCollection.HInfo[header];
			string[] values = this.InnerCollection.GetValues(header);
			if (headerInfo == null || values == null || !headerInfo.AllowMultiValues)
			{
				return values;
			}
			ArrayList arrayList = null;
			for (int i = 0; i < values.Length; i++)
			{
				string[] array = headerInfo.Parser(values[i]);
				if (arrayList == null)
				{
					if (array.Length > 1)
					{
						arrayList = new ArrayList(values);
						arrayList.RemoveRange(i, values.Length - i);
						arrayList.AddRange(array);
					}
				}
				else
				{
					arrayList.AddRange(array);
				}
			}
			if (arrayList != null)
			{
				string[] array2 = new string[arrayList.Count];
				arrayList.CopyTo(array2);
				return array2;
			}
			return values;
		}

		/// <summary>This method is obsolete.</summary>
		/// <returns>The <see cref="T:System.String" /> representation of the collection.</returns>
		// Token: 0x060030EB RID: 12523 RVA: 0x000A8586 File Offset: 0x000A6786
		public override string ToString()
		{
			return WebHeaderCollection.GetAsString(this, false, false);
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x000A8590 File Offset: 0x000A6790
		internal string ToString(bool forTrace)
		{
			return WebHeaderCollection.GetAsString(this, false, true);
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x000A859C File Offset: 0x000A679C
		internal static string GetAsString(NameValueCollection cc, bool winInetCompat, bool forTrace)
		{
			if (winInetCompat)
			{
				throw new InvalidOperationException();
			}
			if (cc == null || cc.Count == 0)
			{
				return "\r\n";
			}
			StringBuilder stringBuilder = new StringBuilder(30 * cc.Count);
			string text = cc[string.Empty];
			if (text != null)
			{
				stringBuilder.Append(text).Append("\r\n");
			}
			for (int i = 0; i < cc.Count; i++)
			{
				string key = cc.GetKey(i);
				string value = cc.Get(i);
				if (!ValidationHelper.IsBlankString(key))
				{
					stringBuilder.Append(key);
					if (winInetCompat)
					{
						stringBuilder.Append(':');
					}
					else
					{
						stringBuilder.Append(": ");
					}
					stringBuilder.Append(value).Append("\r\n");
				}
			}
			if (!forTrace)
			{
				stringBuilder.Append("\r\n");
			}
			return stringBuilder.ToString();
		}

		/// <summary>Converts the <see cref="T:System.Net.WebHeaderCollection" /> to a byte array.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array holding the header collection.</returns>
		// Token: 0x060030EE RID: 12526 RVA: 0x000A8667 File Offset: 0x000A6867
		public byte[] ToByteArray()
		{
			return WebHeaderCollection.HeaderEncoding.GetBytes(this.ToString());
		}

		/// <summary>Tests whether the specified HTTP header can be set for the request.</summary>
		/// <param name="headerName">The header to test.</param>
		/// <returns>
		///   <see langword="true" /> if the header is restricted; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="headerName" /> is <see langword="null" /> or <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="headerName" /> contains invalid characters.</exception>
		// Token: 0x060030EF RID: 12527 RVA: 0x000A8674 File Offset: 0x000A6874
		public static bool IsRestricted(string headerName)
		{
			return WebHeaderCollection.IsRestricted(headerName, false);
		}

		/// <summary>Tests whether the specified HTTP header can be set for the request or the response.</summary>
		/// <param name="headerName">The header to test.</param>
		/// <param name="response">Does the Framework test the response or the request?</param>
		/// <returns>
		///   <see langword="true" /> if the header is restricted; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="headerName" /> is <see langword="null" /> or <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="headerName" /> contains invalid characters.</exception>
		// Token: 0x060030F0 RID: 12528 RVA: 0x000A867D File Offset: 0x000A687D
		public static bool IsRestricted(string headerName, bool response)
		{
			if (!response)
			{
				return WebHeaderCollection.HInfo[WebHeaderCollection.CheckBadChars(headerName, false)].IsRequestRestricted;
			}
			return WebHeaderCollection.HInfo[WebHeaderCollection.CheckBadChars(headerName, false)].IsResponseRestricted;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebHeaderCollection" /> class.</summary>
		// Token: 0x060030F1 RID: 12529 RVA: 0x000A86AF File Offset: 0x000A68AF
		public WebHeaderCollection() : base(DBNull.Value)
		{
		}

		// Token: 0x060030F2 RID: 12530 RVA: 0x000A86BC File Offset: 0x000A68BC
		internal WebHeaderCollection(WebHeaderCollectionType type) : base(DBNull.Value)
		{
			this.m_Type = type;
			if (type == WebHeaderCollectionType.HttpWebResponse)
			{
				this.m_CommonHeaders = new string[WebHeaderCollection.s_CommonHeaderNames.Length - 1];
			}
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x000A86E8 File Offset: 0x000A68E8
		internal WebHeaderCollection(NameValueCollection cc) : base(DBNull.Value)
		{
			this.m_InnerCollection = new NameValueCollection(cc.Count + 2, CaseInsensitiveAscii.StaticInstance);
			int count = cc.Count;
			for (int i = 0; i < count; i++)
			{
				string key = cc.GetKey(i);
				string[] values = cc.GetValues(i);
				if (values != null)
				{
					for (int j = 0; j < values.Length; j++)
					{
						this.InnerCollection.Add(key, values[j]);
					}
				}
				else
				{
					this.InnerCollection.Add(key, null);
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebHeaderCollection" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> containing the information required to serialize the <see cref="T:System.Net.WebHeaderCollection" />.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> containing the source of the serialized stream associated with the new <see cref="T:System.Net.WebHeaderCollection" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="headerName" /> contains invalid characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="headerName" /> is a null reference or <see cref="F:System.String.Empty" />.</exception>
		// Token: 0x060030F4 RID: 12532 RVA: 0x000A8770 File Offset: 0x000A6970
		protected WebHeaderCollection(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(DBNull.Value)
		{
			int @int = serializationInfo.GetInt32("Count");
			this.m_InnerCollection = new NameValueCollection(@int + 2, CaseInsensitiveAscii.StaticInstance);
			for (int i = 0; i < @int; i++)
			{
				string @string = serializationInfo.GetString(i.ToString(NumberFormatInfo.InvariantInfo));
				string string2 = serializationInfo.GetString((i + @int).ToString(NumberFormatInfo.InvariantInfo));
				this.InnerCollection.Add(@string, string2);
			}
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		// Token: 0x060030F5 RID: 12533 RVA: 0x00003917 File Offset: 0x00001B17
		public override void OnDeserialization(object sender)
		{
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x060030F6 RID: 12534 RVA: 0x000A87EC File Offset: 0x000A69EC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.NormalizeCommonHeaders();
			serializationInfo.AddValue("Count", this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				serializationInfo.AddValue(i.ToString(NumberFormatInfo.InvariantInfo), this.GetKey(i));
				serializationInfo.AddValue((i + this.Count).ToString(NumberFormatInfo.InvariantInfo), this.Get(i));
			}
		}

		// Token: 0x060030F7 RID: 12535 RVA: 0x000A885C File Offset: 0x000A6A5C
		internal unsafe DataParseStatus ParseHeaders(byte[] buffer, int size, ref int unparsed, ref int totalResponseHeadersLength, int maximumResponseHeadersLength, ref WebParseError parseError)
		{
			byte* ptr;
			if (buffer == null || buffer.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &buffer[0];
			}
			if (buffer.Length < size)
			{
				return DataParseStatus.NeedMoreData;
			}
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			int i = unparsed;
			int num4 = totalResponseHeadersLength;
			WebParseErrorCode code = WebParseErrorCode.Generic;
			for (;;)
			{
				string text = string.Empty;
				string text2 = string.Empty;
				bool flag = false;
				string text3 = null;
				if (this.Count == 0)
				{
					while (i < size)
					{
						char c = (char)ptr[i];
						if (c != ' ' && c != '\t')
						{
							break;
						}
						i++;
						if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
						{
							goto Block_6;
						}
					}
					if (i == size)
					{
						goto Block_7;
					}
				}
				int num5 = i;
				while (i < size)
				{
					char c = (char)ptr[i];
					if (c != ':' && c != '\n')
					{
						if (c > ' ')
						{
							num = i;
						}
						i++;
						if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
						{
							goto Block_12;
						}
					}
					else
					{
						if (c != ':')
						{
							break;
						}
						i++;
						if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
						{
							goto Block_15;
						}
						break;
					}
				}
				if (i == size)
				{
					goto Block_16;
				}
				int num6;
				for (;;)
				{
					num6 = ((this.Count == 0 && num < 0) ? 1 : 0);
					char c;
					while (i < size && num6 < 2)
					{
						c = (char)ptr[i];
						if (c > ' ')
						{
							break;
						}
						if (c == '\n')
						{
							num6++;
							if (num6 == 1)
							{
								if (i + 1 == size)
								{
									goto Block_21;
								}
								flag = (ptr[i + 1] == 32 || ptr[i + 1] == 9);
							}
						}
						i++;
						if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
						{
							goto Block_24;
						}
					}
					if (num6 != 2 && (num6 != 1 || flag))
					{
						if (i == size)
						{
							goto Block_28;
						}
						num2 = i;
						while (i < size)
						{
							c = (char)ptr[i];
							if (c == '\n')
							{
								break;
							}
							if (c > ' ')
							{
								num3 = i;
							}
							i++;
							if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
							{
								goto Block_32;
							}
						}
						if (i == size)
						{
							goto Block_33;
						}
						num6 = 0;
						while (i < size && num6 < 2)
						{
							c = (char)ptr[i];
							if (c != '\r' && c != '\n')
							{
								break;
							}
							if (c == '\n')
							{
								num6++;
							}
							i++;
							if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
							{
								goto Block_37;
							}
						}
						if (i == size && num6 < 2)
						{
							goto Block_40;
						}
					}
					if (num2 >= 0 && num2 > num && num3 >= num2)
					{
						text2 = WebHeaderCollection.HeaderEncoding.GetString(ptr + num2, num3 - num2 + 1);
					}
					text3 = ((text3 == null) ? text2 : (text3 + " " + text2));
					if (i >= size || num6 != 1)
					{
						break;
					}
					c = (char)ptr[i];
					if (c != ' ' && c != '\t')
					{
						break;
					}
					i++;
					if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
					{
						goto Block_49;
					}
				}
				if (num5 >= 0 && num >= num5)
				{
					text = WebHeaderCollection.HeaderEncoding.GetString(ptr + num5, num - num5 + 1);
				}
				if (text.Length > 0)
				{
					this.AddInternal(text, text3);
				}
				totalResponseHeadersLength = num4;
				unparsed = i;
				if (num6 == 2)
				{
					goto Block_53;
				}
			}
			Block_6:
			DataParseStatus dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_30A;
			Block_7:
			dataParseStatus = DataParseStatus.NeedMoreData;
			goto IL_30A;
			Block_12:
			dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_30A;
			Block_15:
			dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_30A;
			Block_16:
			dataParseStatus = DataParseStatus.NeedMoreData;
			goto IL_30A;
			Block_21:
			dataParseStatus = DataParseStatus.NeedMoreData;
			goto IL_30A;
			Block_24:
			dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_30A;
			Block_28:
			dataParseStatus = DataParseStatus.NeedMoreData;
			goto IL_30A;
			Block_32:
			dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_30A;
			Block_33:
			dataParseStatus = DataParseStatus.NeedMoreData;
			goto IL_30A;
			Block_37:
			dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_30A;
			Block_40:
			dataParseStatus = DataParseStatus.NeedMoreData;
			goto IL_30A;
			Block_49:
			dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_30A;
			Block_53:
			dataParseStatus = DataParseStatus.Done;
			IL_30A:
			if (dataParseStatus == DataParseStatus.Invalid)
			{
				parseError.Section = WebParseErrorSection.ResponseHeader;
				parseError.Code = code;
			}
			return dataParseStatus;
		}

		// Token: 0x060030F8 RID: 12536 RVA: 0x000A8B8C File Offset: 0x000A6D8C
		internal unsafe DataParseStatus ParseHeadersStrict(byte[] buffer, int size, ref int unparsed, ref int totalResponseHeadersLength, int maximumResponseHeadersLength, ref WebParseError parseError)
		{
			WebParseErrorCode code = WebParseErrorCode.Generic;
			DataParseStatus dataParseStatus = DataParseStatus.Invalid;
			int num = unparsed;
			int num2 = (maximumResponseHeadersLength <= 0) ? int.MaxValue : (maximumResponseHeadersLength - totalResponseHeadersLength + num);
			DataParseStatus dataParseStatus2 = DataParseStatus.DataTooBig;
			if (size < num2)
			{
				num2 = size;
				dataParseStatus2 = DataParseStatus.NeedMoreData;
			}
			if (num >= num2)
			{
				dataParseStatus = dataParseStatus2;
			}
			else
			{
				try
				{
					fixed (byte[] array = buffer)
					{
						byte* ptr;
						if (buffer == null || array.Length == 0)
						{
							ptr = null;
						}
						else
						{
							ptr = &array[0];
						}
						while (ptr[num] != 13)
						{
							int num3 = num;
							while (num < num2 && ((ptr[num] > 127) ? WebHeaderCollection.RfcChar.High : WebHeaderCollection.RfcCharMap[(int)ptr[num]]) == WebHeaderCollection.RfcChar.Reg)
							{
								num++;
							}
							if (num == num2)
							{
								dataParseStatus = dataParseStatus2;
								goto IL_416;
							}
							if (num == num3)
							{
								dataParseStatus = DataParseStatus.Invalid;
								code = WebParseErrorCode.InvalidHeaderName;
								goto IL_416;
							}
							int num4 = num - 1;
							int num5 = 0;
							WebHeaderCollection.RfcChar rfcChar;
							while (num < num2 && (rfcChar = ((ptr[num] > 127) ? WebHeaderCollection.RfcChar.High : WebHeaderCollection.RfcCharMap[(int)ptr[num]])) != WebHeaderCollection.RfcChar.Colon)
							{
								switch (rfcChar)
								{
								case WebHeaderCollection.RfcChar.CR:
									if (num5 != 0)
									{
										goto IL_11D;
									}
									num5 = 1;
									break;
								case WebHeaderCollection.RfcChar.LF:
									if (num5 != 1)
									{
										goto IL_11D;
									}
									num5 = 2;
									break;
								case WebHeaderCollection.RfcChar.WS:
									if (num5 == 1)
									{
										goto IL_11D;
									}
									num5 = 0;
									break;
								default:
									goto IL_11D;
								}
								num++;
								continue;
								IL_11D:
								dataParseStatus = DataParseStatus.Invalid;
								code = WebParseErrorCode.CrLfError;
								goto IL_416;
							}
							if (num == num2)
							{
								dataParseStatus = dataParseStatus2;
								goto IL_416;
							}
							if (num5 != 0)
							{
								dataParseStatus = DataParseStatus.Invalid;
								code = WebParseErrorCode.IncompleteHeaderLine;
								goto IL_416;
							}
							if (++num == num2)
							{
								dataParseStatus = dataParseStatus2;
								goto IL_416;
							}
							int num6 = -1;
							int num7 = -1;
							StringBuilder stringBuilder = null;
							while (num < num2 && ((rfcChar = ((ptr[num] > 127) ? WebHeaderCollection.RfcChar.High : WebHeaderCollection.RfcCharMap[(int)ptr[num]])) == WebHeaderCollection.RfcChar.WS || num5 != 2))
							{
								switch (rfcChar)
								{
								case WebHeaderCollection.RfcChar.High:
								case WebHeaderCollection.RfcChar.Reg:
								case WebHeaderCollection.RfcChar.Colon:
								case WebHeaderCollection.RfcChar.Delim:
									if (num5 == 1)
									{
										goto IL_23E;
									}
									if (num5 == 3)
									{
										num5 = 0;
										if (num6 != -1)
										{
											string @string = WebHeaderCollection.HeaderEncoding.GetString(ptr + num6, num7 - num6 + 1);
											if (stringBuilder == null)
											{
												stringBuilder = new StringBuilder(@string, @string.Length * 5);
											}
											else
											{
												stringBuilder.Append(" ");
												stringBuilder.Append(@string);
											}
										}
										num6 = -1;
									}
									if (num6 == -1)
									{
										num6 = num;
									}
									num7 = num;
									break;
								case WebHeaderCollection.RfcChar.Ctl:
									goto IL_23E;
								case WebHeaderCollection.RfcChar.CR:
									if (num5 != 0)
									{
										goto IL_23E;
									}
									num5 = 1;
									break;
								case WebHeaderCollection.RfcChar.LF:
									if (num5 != 1)
									{
										goto IL_23E;
									}
									num5 = 2;
									break;
								case WebHeaderCollection.RfcChar.WS:
									if (num5 == 1)
									{
										goto IL_23E;
									}
									if (num5 == 2)
									{
										num5 = 3;
									}
									break;
								default:
									goto IL_23E;
								}
								num++;
								continue;
								IL_23E:
								dataParseStatus = DataParseStatus.Invalid;
								code = WebParseErrorCode.CrLfError;
								goto IL_416;
							}
							if (num == num2)
							{
								dataParseStatus = dataParseStatus2;
								goto IL_416;
							}
							string text = (num6 == -1) ? "" : WebHeaderCollection.HeaderEncoding.GetString(ptr + num6, num7 - num6 + 1);
							if (stringBuilder != null)
							{
								if (text.Length != 0)
								{
									stringBuilder.Append(" ");
									stringBuilder.Append(text);
								}
								text = stringBuilder.ToString();
							}
							string text2 = null;
							int num8 = num4 - num3 + 1;
							if (this.m_CommonHeaders != null)
							{
								int num9 = (int)WebHeaderCollection.s_CommonHeaderHints[(int)(ptr[num3] & 31)];
								if (num9 >= 0)
								{
									string text3;
									for (;;)
									{
										text3 = WebHeaderCollection.s_CommonHeaderNames[num9++];
										if (text3.Length < num8 || CaseInsensitiveAscii.AsciiToLower[(int)ptr[num3]] != CaseInsensitiveAscii.AsciiToLower[(int)text3[0]])
										{
											goto IL_3E3;
										}
										if (text3.Length <= num8)
										{
											byte* ptr2 = ptr + num3 + 1;
											int num10 = 1;
											while (num10 < text3.Length && ((char)(*(ptr2++)) == text3[num10] || CaseInsensitiveAscii.AsciiToLower[(int)(*(ptr2 - 1))] == CaseInsensitiveAscii.AsciiToLower[(int)text3[num10]]))
											{
												num10++;
											}
											if (num10 == text3.Length)
											{
												break;
											}
										}
									}
									this.m_NumCommonHeaders++;
									num9--;
									if (this.m_CommonHeaders[num9] == null)
									{
										this.m_CommonHeaders[num9] = text;
									}
									else
									{
										this.NormalizeCommonHeaders();
										this.AddInternalNotCommon(text3, text);
									}
									text2 = text3;
								}
							}
							IL_3E3:
							if (text2 == null)
							{
								text2 = WebHeaderCollection.HeaderEncoding.GetString(ptr + num3, num8);
								this.AddInternalNotCommon(text2, text);
							}
							totalResponseHeadersLength += num - unparsed;
							unparsed = num;
						}
						if (++num == num2)
						{
							dataParseStatus = dataParseStatus2;
						}
						else if (ptr[num++] == 10)
						{
							totalResponseHeadersLength += num - unparsed;
							unparsed = num;
							dataParseStatus = DataParseStatus.Done;
						}
						else
						{
							dataParseStatus = DataParseStatus.Invalid;
							code = WebParseErrorCode.CrLfError;
						}
					}
				}
				finally
				{
					byte[] array = null;
				}
			}
			IL_416:
			if (dataParseStatus == DataParseStatus.Invalid)
			{
				parseError.Section = WebParseErrorSection.ResponseHeader;
				parseError.Code = code;
			}
			return dataParseStatus;
		}

		/// <summary>Serializes this instance into the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</summary>
		/// <param name="serializationInfo">The object into which this <see cref="T:System.Net.WebHeaderCollection" /> will be serialized.</param>
		/// <param name="streamingContext">The destination of the serialization.</param>
		// Token: 0x060030F9 RID: 12537 RVA: 0x000A8FE0 File Offset: 0x000A71E0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Gets the value of a particular header in the collection, specified by the name of the header.</summary>
		/// <param name="name">The name of the Web header.</param>
		/// <returns>A <see cref="T:System.String" /> holding the value of the specified header.</returns>
		// Token: 0x060030FA RID: 12538 RVA: 0x000A8FEC File Offset: 0x000A71EC
		public override string Get(string name)
		{
			if (this.m_CommonHeaders != null && name != null && name.Length > 0 && name[0] < 'Ā')
			{
				int num = (int)WebHeaderCollection.s_CommonHeaderHints[(int)(name[0] & '\u001f')];
				if (num >= 0)
				{
					for (;;)
					{
						string text = WebHeaderCollection.s_CommonHeaderNames[num++];
						if (text.Length < name.Length || CaseInsensitiveAscii.AsciiToLower[(int)name[0]] != CaseInsensitiveAscii.AsciiToLower[(int)text[0]])
						{
							goto IL_EF;
						}
						if (text.Length <= name.Length)
						{
							int num2 = 1;
							while (num2 < text.Length && (name[num2] == text[num2] || (name[num2] <= 'ÿ' && CaseInsensitiveAscii.AsciiToLower[(int)name[num2]] == CaseInsensitiveAscii.AsciiToLower[(int)text[num2]])))
							{
								num2++;
							}
							if (num2 == text.Length)
							{
								break;
							}
						}
					}
					return this.m_CommonHeaders[num - 1];
				}
			}
			IL_EF:
			if (this.m_InnerCollection == null)
			{
				return null;
			}
			return this.m_InnerCollection.Get(name);
		}

		/// <summary>Returns an enumerator that can iterate through the <see cref="T:System.Net.WebHeaderCollection" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Net.WebHeaderCollection" />.</returns>
		// Token: 0x060030FB RID: 12539 RVA: 0x000A90FE File Offset: 0x000A72FE
		public override IEnumerator GetEnumerator()
		{
			this.NormalizeCommonHeaders();
			return new NameObjectCollectionBase.NameObjectKeysEnumerator(this.InnerCollection);
		}

		/// <summary>Gets the number of headers in the collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> indicating the number of headers in a request.</returns>
		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x060030FC RID: 12540 RVA: 0x000A9111 File Offset: 0x000A7311
		public override int Count
		{
			get
			{
				return ((this.m_InnerCollection == null) ? 0 : this.m_InnerCollection.Count) + this.m_NumCommonHeaders;
			}
		}

		/// <summary>Gets the collection of header names (keys) in the collection.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> containing all header names in a Web request.</returns>
		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x060030FD RID: 12541 RVA: 0x000A9130 File Offset: 0x000A7330
		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				this.NormalizeCommonHeaders();
				return this.InnerCollection.Keys;
			}
		}

		// Token: 0x060030FE RID: 12542 RVA: 0x000A9143 File Offset: 0x000A7343
		internal override bool InternalHasKeys()
		{
			this.NormalizeCommonHeaders();
			return this.m_InnerCollection != null && this.m_InnerCollection.HasKeys();
		}

		/// <summary>Gets the value of a particular header in the collection, specified by an index into the collection.</summary>
		/// <param name="index">The zero-based index of the key to get from the collection.</param>
		/// <returns>A <see cref="T:System.String" /> containing the value of the specified header.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is negative.  
		/// -or-  
		/// <paramref name="index" /> exceeds the size of the collection.</exception>
		// Token: 0x060030FF RID: 12543 RVA: 0x000A9160 File Offset: 0x000A7360
		public override string Get(int index)
		{
			this.NormalizeCommonHeaders();
			return this.InnerCollection.Get(index);
		}

		/// <summary>Gets an array of header values stored in the <paramref name="index" /> position of the header collection.</summary>
		/// <param name="index">The header index to return.</param>
		/// <returns>An array of header strings.</returns>
		// Token: 0x06003100 RID: 12544 RVA: 0x000A9174 File Offset: 0x000A7374
		public override string[] GetValues(int index)
		{
			this.NormalizeCommonHeaders();
			return this.InnerCollection.GetValues(index);
		}

		/// <summary>Gets the header name at the specified position in the collection.</summary>
		/// <param name="index">The zero-based index of the key to get from the collection.</param>
		/// <returns>A <see cref="T:System.String" /> holding the header name.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is negative.  
		/// -or-  
		/// <paramref name="index" /> exceeds the size of the collection.</exception>
		// Token: 0x06003101 RID: 12545 RVA: 0x000A9188 File Offset: 0x000A7388
		public override string GetKey(int index)
		{
			this.NormalizeCommonHeaders();
			return this.InnerCollection.GetKey(index);
		}

		/// <summary>Gets all header names (keys) in the collection.</summary>
		/// <returns>An array of type <see cref="T:System.String" /> containing all header names in a Web request.</returns>
		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06003102 RID: 12546 RVA: 0x000A919C File Offset: 0x000A739C
		public override string[] AllKeys
		{
			get
			{
				this.NormalizeCommonHeaders();
				return this.InnerCollection.AllKeys;
			}
		}

		/// <summary>Removes all headers from the collection.</summary>
		// Token: 0x06003103 RID: 12547 RVA: 0x000A91AF File Offset: 0x000A73AF
		public override void Clear()
		{
			this.m_CommonHeaders = null;
			this.m_NumCommonHeaders = 0;
			base.InvalidateCachedArrays();
			if (this.m_InnerCollection != null)
			{
				this.m_InnerCollection.Clear();
			}
		}

		// Token: 0x06003104 RID: 12548 RVA: 0x000A91D8 File Offset: 0x000A73D8
		// Note: this type is marked as 'beforefieldinit'.
		static WebHeaderCollection()
		{
		}

		// Token: 0x04001C6B RID: 7275
		private const int ApproxAveHeaderLineSize = 30;

		// Token: 0x04001C6C RID: 7276
		private const int ApproxHighAvgNumHeaders = 16;

		// Token: 0x04001C6D RID: 7277
		private static readonly HeaderInfoTable HInfo = new HeaderInfoTable();

		// Token: 0x04001C6E RID: 7278
		private string[] m_CommonHeaders;

		// Token: 0x04001C6F RID: 7279
		private int m_NumCommonHeaders;

		// Token: 0x04001C70 RID: 7280
		private static readonly string[] s_CommonHeaderNames = new string[]
		{
			"Accept-Ranges",
			"Content-Length",
			"Cache-Control",
			"Content-Type",
			"Date",
			"Expires",
			"ETag",
			"Last-Modified",
			"Location",
			"Proxy-Authenticate",
			"P3P",
			"Set-Cookie2",
			"Set-Cookie",
			"Server",
			"Via",
			"WWW-Authenticate",
			"X-AspNet-Version",
			"X-Powered-By",
			"["
		};

		// Token: 0x04001C71 RID: 7281
		private static readonly sbyte[] s_CommonHeaderHints = new sbyte[]
		{
			-1,
			0,
			-1,
			1,
			4,
			5,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			7,
			-1,
			-1,
			-1,
			9,
			-1,
			-1,
			11,
			-1,
			-1,
			14,
			15,
			16,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1
		};

		// Token: 0x04001C72 RID: 7282
		private const int c_AcceptRanges = 0;

		// Token: 0x04001C73 RID: 7283
		private const int c_ContentLength = 1;

		// Token: 0x04001C74 RID: 7284
		private const int c_CacheControl = 2;

		// Token: 0x04001C75 RID: 7285
		private const int c_ContentType = 3;

		// Token: 0x04001C76 RID: 7286
		private const int c_Date = 4;

		// Token: 0x04001C77 RID: 7287
		private const int c_Expires = 5;

		// Token: 0x04001C78 RID: 7288
		private const int c_ETag = 6;

		// Token: 0x04001C79 RID: 7289
		private const int c_LastModified = 7;

		// Token: 0x04001C7A RID: 7290
		private const int c_Location = 8;

		// Token: 0x04001C7B RID: 7291
		private const int c_ProxyAuthenticate = 9;

		// Token: 0x04001C7C RID: 7292
		private const int c_P3P = 10;

		// Token: 0x04001C7D RID: 7293
		private const int c_SetCookie2 = 11;

		// Token: 0x04001C7E RID: 7294
		private const int c_SetCookie = 12;

		// Token: 0x04001C7F RID: 7295
		private const int c_Server = 13;

		// Token: 0x04001C80 RID: 7296
		private const int c_Via = 14;

		// Token: 0x04001C81 RID: 7297
		private const int c_WwwAuthenticate = 15;

		// Token: 0x04001C82 RID: 7298
		private const int c_XAspNetVersion = 16;

		// Token: 0x04001C83 RID: 7299
		private const int c_XPoweredBy = 17;

		// Token: 0x04001C84 RID: 7300
		private NameValueCollection m_InnerCollection;

		// Token: 0x04001C85 RID: 7301
		private WebHeaderCollectionType m_Type;

		// Token: 0x04001C86 RID: 7302
		private static readonly char[] HttpTrimCharacters = new char[]
		{
			'\t',
			'\n',
			'\v',
			'\f',
			'\r',
			' '
		};

		// Token: 0x04001C87 RID: 7303
		private static WebHeaderCollection.RfcChar[] RfcCharMap = new WebHeaderCollection.RfcChar[]
		{
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.WS,
			WebHeaderCollection.RfcChar.LF,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.CR,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.WS,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Colon,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Ctl
		};

		// Token: 0x0200060B RID: 1547
		internal static class HeaderEncoding
		{
			// Token: 0x06003105 RID: 12549 RVA: 0x000A92E4 File Offset: 0x000A74E4
			internal unsafe static string GetString(byte[] bytes, int byteIndex, int byteCount)
			{
				byte* ptr;
				if (bytes == null || bytes.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &bytes[0];
				}
				return WebHeaderCollection.HeaderEncoding.GetString(ptr + byteIndex, byteCount);
			}

			// Token: 0x06003106 RID: 12550 RVA: 0x000A9314 File Offset: 0x000A7514
			internal unsafe static string GetString(byte* pBytes, int byteCount)
			{
				if (byteCount < 1)
				{
					return "";
				}
				string text = new string('\0', byteCount);
				fixed (string text2 = text)
				{
					char* ptr = text2;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr2 = ptr;
					while (byteCount >= 8)
					{
						*ptr2 = (char)(*pBytes);
						ptr2[1] = (char)pBytes[1];
						ptr2[2] = (char)pBytes[2];
						ptr2[3] = (char)pBytes[3];
						ptr2[4] = (char)pBytes[4];
						ptr2[5] = (char)pBytes[5];
						ptr2[6] = (char)pBytes[6];
						ptr2[7] = (char)pBytes[7];
						ptr2 += 8;
						pBytes += 8;
						byteCount -= 8;
					}
					for (int i = 0; i < byteCount; i++)
					{
						ptr2[i] = (char)pBytes[i];
					}
				}
				return text;
			}

			// Token: 0x06003107 RID: 12551 RVA: 0x000A93CA File Offset: 0x000A75CA
			internal static int GetByteCount(string myString)
			{
				return myString.Length;
			}

			// Token: 0x06003108 RID: 12552 RVA: 0x000A93D4 File Offset: 0x000A75D4
			internal unsafe static void GetBytes(string myString, int charIndex, int charCount, byte[] bytes, int byteIndex)
			{
				if (myString.Length == 0)
				{
					return;
				}
				fixed (byte[] array = bytes)
				{
					byte* ptr;
					if (bytes == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					byte* ptr2 = ptr + byteIndex;
					int num = charIndex + charCount;
					while (charIndex < num)
					{
						*(ptr2++) = (byte)myString[charIndex++];
					}
				}
			}

			// Token: 0x06003109 RID: 12553 RVA: 0x000A9428 File Offset: 0x000A7628
			internal static byte[] GetBytes(string myString)
			{
				byte[] array = new byte[myString.Length];
				if (myString.Length != 0)
				{
					WebHeaderCollection.HeaderEncoding.GetBytes(myString, 0, myString.Length, array, 0);
				}
				return array;
			}

			// Token: 0x0600310A RID: 12554 RVA: 0x000A945C File Offset: 0x000A765C
			[FriendAccessAllowed]
			internal static string DecodeUtf8FromString(string input)
			{
				if (string.IsNullOrWhiteSpace(input))
				{
					return input;
				}
				bool flag = false;
				for (int i = 0; i < input.Length; i++)
				{
					if (input[i] > 'ÿ')
					{
						return input;
					}
					if (input[i] > '\u007f')
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					byte[] array = new byte[input.Length];
					for (int j = 0; j < input.Length; j++)
					{
						if (input[j] > 'ÿ')
						{
							return input;
						}
						array[j] = (byte)input[j];
					}
					try
					{
						return Encoding.GetEncoding("utf-8", EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback).GetString(array);
					}
					catch (ArgumentException)
					{
					}
					return input;
				}
				return input;
			}
		}

		// Token: 0x0200060C RID: 1548
		private enum RfcChar : byte
		{
			// Token: 0x04001C89 RID: 7305
			High,
			// Token: 0x04001C8A RID: 7306
			Reg,
			// Token: 0x04001C8B RID: 7307
			Ctl,
			// Token: 0x04001C8C RID: 7308
			CR,
			// Token: 0x04001C8D RID: 7309
			LF,
			// Token: 0x04001C8E RID: 7310
			WS,
			// Token: 0x04001C8F RID: 7311
			Colon,
			// Token: 0x04001C90 RID: 7312
			Delim
		}
	}
}
