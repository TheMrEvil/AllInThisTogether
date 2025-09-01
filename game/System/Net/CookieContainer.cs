using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

namespace System.Net
{
	/// <summary>Provides a container for a collection of <see cref="T:System.Net.CookieCollection" /> objects.</summary>
	// Token: 0x02000653 RID: 1619
	[Serializable]
	public class CookieContainer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.CookieContainer" /> class.</summary>
		// Token: 0x060032F1 RID: 13041 RVA: 0x000B0F9C File Offset: 0x000AF19C
		public CookieContainer()
		{
			string domainName = IPGlobalProperties.InternalGetIPGlobalProperties().DomainName;
			if (domainName != null && domainName.Length > 1)
			{
				this.m_fqdnMyDomain = "." + domainName;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.CookieContainer" /> class with a specified value for the number of <see cref="T:System.Net.Cookie" /> instances that the container can hold.</summary>
		/// <param name="capacity">The number of <see cref="T:System.Net.Cookie" /> instances that the <see cref="T:System.Net.CookieContainer" /> can hold.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="capacity" /> is less than or equal to zero.</exception>
		// Token: 0x060032F2 RID: 13042 RVA: 0x000B100B File Offset: 0x000AF20B
		public CookieContainer(int capacity) : this()
		{
			if (capacity <= 0)
			{
				throw new ArgumentException(SR.GetString("The specified value must be greater than 0."), "Capacity");
			}
			this.m_maxCookies = capacity;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.CookieContainer" /> class with specific properties.</summary>
		/// <param name="capacity">The number of <see cref="T:System.Net.Cookie" /> instances that the <see cref="T:System.Net.CookieContainer" /> can hold.</param>
		/// <param name="perDomainCapacity">The number of <see cref="T:System.Net.Cookie" /> instances per domain.</param>
		/// <param name="maxCookieSize">The maximum size in bytes for any single <see cref="T:System.Net.Cookie" /> in a <see cref="T:System.Net.CookieContainer" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="perDomainCapacity" /> is not equal to <see cref="F:System.Int32.MaxValue" />.  
		/// and  
		/// <paramref name="(perDomainCapacity" /> is less than or equal to zero or <paramref name="perDomainCapacity" /> is greater than <paramref name="capacity)" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="maxCookieSize" /> is less than or equal to zero.</exception>
		// Token: 0x060032F3 RID: 13043 RVA: 0x000B1034 File Offset: 0x000AF234
		public CookieContainer(int capacity, int perDomainCapacity, int maxCookieSize) : this(capacity)
		{
			if (perDomainCapacity != 2147483647 && (perDomainCapacity <= 0 || perDomainCapacity > capacity))
			{
				throw new ArgumentOutOfRangeException("perDomainCapacity", SR.GetString("'{0}' has to be greater than '{1}' and less than '{2}'.", new object[]
				{
					"PerDomainCapacity",
					0,
					capacity
				}));
			}
			this.m_maxCookiesPerDomain = perDomainCapacity;
			if (maxCookieSize <= 0)
			{
				throw new ArgumentException(SR.GetString("The specified value must be greater than 0."), "MaxCookieSize");
			}
			this.m_maxCookieSize = maxCookieSize;
		}

		/// <summary>Gets or sets the number of <see cref="T:System.Net.Cookie" /> instances that a <see cref="T:System.Net.CookieContainer" /> can hold.</summary>
		/// <returns>The number of <see cref="T:System.Net.Cookie" /> instances that a <see cref="T:System.Net.CookieContainer" /> can hold. This is a hard limit and cannot be exceeded by adding a <see cref="T:System.Net.Cookie" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="Capacity" /> is less than or equal to zero or (value is less than <see cref="P:System.Net.CookieContainer.PerDomainCapacity" /> and <see cref="P:System.Net.CookieContainer.PerDomainCapacity" /> is not equal to <see cref="F:System.Int32.MaxValue" />).</exception>
		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x060032F4 RID: 13044 RVA: 0x000B10B4 File Offset: 0x000AF2B4
		// (set) Token: 0x060032F5 RID: 13045 RVA: 0x000B10BC File Offset: 0x000AF2BC
		public int Capacity
		{
			get
			{
				return this.m_maxCookies;
			}
			set
			{
				if (value <= 0 || (value < this.m_maxCookiesPerDomain && this.m_maxCookiesPerDomain != 2147483647))
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("'{0}' has to be greater than '{1}' and less than '{2}'.", new object[]
					{
						"Capacity",
						0,
						this.m_maxCookiesPerDomain
					}));
				}
				if (value < this.m_maxCookies)
				{
					this.m_maxCookies = value;
					this.AgeCookies(null);
				}
				this.m_maxCookies = value;
			}
		}

		/// <summary>Gets the number of <see cref="T:System.Net.Cookie" /> instances that a <see cref="T:System.Net.CookieContainer" /> currently holds.</summary>
		/// <returns>The number of <see cref="T:System.Net.Cookie" /> instances that a <see cref="T:System.Net.CookieContainer" /> currently holds. This is the total of <see cref="T:System.Net.Cookie" /> instances in all domains.</returns>
		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x060032F6 RID: 13046 RVA: 0x000B113C File Offset: 0x000AF33C
		public int Count
		{
			get
			{
				return this.m_count;
			}
		}

		/// <summary>Represents the maximum allowed length of a <see cref="T:System.Net.Cookie" />.</summary>
		/// <returns>The maximum allowed length, in bytes, of a <see cref="T:System.Net.Cookie" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="MaxCookieSize" /> is less than or equal to zero.</exception>
		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x060032F7 RID: 13047 RVA: 0x000B1144 File Offset: 0x000AF344
		// (set) Token: 0x060032F8 RID: 13048 RVA: 0x000B114C File Offset: 0x000AF34C
		public int MaxCookieSize
		{
			get
			{
				return this.m_maxCookieSize;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.m_maxCookieSize = value;
			}
		}

		/// <summary>Gets or sets the number of <see cref="T:System.Net.Cookie" /> instances that a <see cref="T:System.Net.CookieContainer" /> can hold per domain.</summary>
		/// <returns>The number of <see cref="T:System.Net.Cookie" /> instances that are allowed per domain.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="PerDomainCapacity" /> is less than or equal to zero.  
		/// -or-  
		/// <paramref name="(PerDomainCapacity" /> is greater than the maximum allowable number of cookies instances, 300, and is not equal to <see cref="F:System.Int32.MaxValue" />).</exception>
		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x060032F9 RID: 13049 RVA: 0x000B1164 File Offset: 0x000AF364
		// (set) Token: 0x060032FA RID: 13050 RVA: 0x000B116C File Offset: 0x000AF36C
		public int PerDomainCapacity
		{
			get
			{
				return this.m_maxCookiesPerDomain;
			}
			set
			{
				if (value <= 0 || (value > this.m_maxCookies && value != 2147483647))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value < this.m_maxCookiesPerDomain)
				{
					this.m_maxCookiesPerDomain = value;
					this.AgeCookies(null);
				}
				this.m_maxCookiesPerDomain = value;
			}
		}

		/// <summary>Adds a <see cref="T:System.Net.Cookie" /> to a <see cref="T:System.Net.CookieContainer" />. This method uses the domain from the <see cref="T:System.Net.Cookie" /> to determine which domain collection to associate the <see cref="T:System.Net.Cookie" /> with.</summary>
		/// <param name="cookie">The <see cref="T:System.Net.Cookie" /> to be added to the <see cref="T:System.Net.CookieContainer" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cookie" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The domain for <paramref name="cookie" /> is <see langword="null" /> or the empty string ("").</exception>
		/// <exception cref="T:System.Net.CookieException">
		///   <paramref name="cookie" /> is larger than <paramref name="maxCookieSize" />.  
		/// -or-  
		/// the domain for <paramref name="cookie" /> is not a valid URI.</exception>
		// Token: 0x060032FB RID: 13051 RVA: 0x000B11B8 File Offset: 0x000AF3B8
		public void Add(Cookie cookie)
		{
			if (cookie == null)
			{
				throw new ArgumentNullException("cookie");
			}
			if (cookie.Domain.Length == 0)
			{
				throw new ArgumentException(SR.GetString("The parameter '{0}' cannot be an empty string."), "cookie.Domain");
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(cookie.Secure ? Uri.UriSchemeHttps : Uri.UriSchemeHttp).Append(Uri.SchemeDelimiter);
			if (!cookie.DomainImplicit && cookie.Domain[0] == '.')
			{
				stringBuilder.Append("0");
			}
			stringBuilder.Append(cookie.Domain);
			if (cookie.PortList != null)
			{
				stringBuilder.Append(":").Append(cookie.PortList[0]);
			}
			stringBuilder.Append(cookie.Path);
			Uri uri;
			if (!Uri.TryCreate(stringBuilder.ToString(), UriKind.Absolute, out uri))
			{
				throw new CookieException(SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[]
				{
					"Domain",
					cookie.Domain
				}));
			}
			Cookie cookie2 = cookie.Clone();
			cookie2.VerifySetDefaults(cookie2.Variant, uri, this.IsLocalDomain(uri.Host), this.m_fqdnMyDomain, true, true);
			this.Add(cookie2, true);
		}

		// Token: 0x060032FC RID: 13052 RVA: 0x000B12E8 File Offset: 0x000AF4E8
		private void AddRemoveDomain(string key, PathList value)
		{
			object syncRoot = this.m_domainTable.SyncRoot;
			lock (syncRoot)
			{
				if (value == null)
				{
					this.m_domainTable.Remove(key);
				}
				else
				{
					this.m_domainTable[key] = value;
				}
			}
		}

		// Token: 0x060032FD RID: 13053 RVA: 0x000B1348 File Offset: 0x000AF548
		internal void Add(Cookie cookie, bool throwOnError)
		{
			if (cookie.Value.Length <= this.m_maxCookieSize)
			{
				try
				{
					object syncRoot = this.m_domainTable.SyncRoot;
					PathList pathList;
					lock (syncRoot)
					{
						pathList = (PathList)this.m_domainTable[cookie.DomainKey];
						if (pathList == null)
						{
							pathList = new PathList();
							this.AddRemoveDomain(cookie.DomainKey, pathList);
						}
					}
					int cookiesCount = pathList.GetCookiesCount();
					syncRoot = pathList.SyncRoot;
					CookieCollection cookieCollection;
					lock (syncRoot)
					{
						cookieCollection = (CookieCollection)pathList[cookie.Path];
						if (cookieCollection == null)
						{
							cookieCollection = new CookieCollection();
							pathList[cookie.Path] = cookieCollection;
						}
					}
					if (cookie.Expired)
					{
						CookieCollection obj = cookieCollection;
						lock (obj)
						{
							int num = cookieCollection.IndexOf(cookie);
							if (num != -1)
							{
								cookieCollection.RemoveAt(num);
								this.m_count--;
							}
							goto IL_194;
						}
					}
					if (cookiesCount < this.m_maxCookiesPerDomain || this.AgeCookies(cookie.DomainKey))
					{
						if (this.m_count < this.m_maxCookies || this.AgeCookies(null))
						{
							CookieCollection obj = cookieCollection;
							lock (obj)
							{
								this.m_count += cookieCollection.InternalAdd(cookie, true);
							}
						}
					}
					IL_194:;
				}
				catch (Exception ex)
				{
					if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
					{
						throw;
					}
					if (throwOnError)
					{
						throw new CookieException(SR.GetString("An error occurred when adding a cookie to the container."), ex);
					}
				}
				return;
			}
			if (throwOnError)
			{
				throw new CookieException(SR.GetString("The value size of the cookie is '{0}'. This exceeds the configured maximum size, which is '{1}'.", new object[]
				{
					cookie.ToString(),
					this.m_maxCookieSize
				}));
			}
		}

		// Token: 0x060032FE RID: 13054 RVA: 0x000B15A0 File Offset: 0x000AF7A0
		private bool AgeCookies(string domain)
		{
			if (this.m_maxCookies == 0 || this.m_maxCookiesPerDomain == 0)
			{
				this.m_domainTable = new Hashtable();
				this.m_count = 0;
				return false;
			}
			int num = 0;
			DateTime dateTime = DateTime.MaxValue;
			CookieCollection cookieCollection = null;
			int num2 = 0;
			int num3 = 0;
			float num4 = 1f;
			if (this.m_count > this.m_maxCookies)
			{
				num4 = (float)this.m_maxCookies / (float)this.m_count;
			}
			object syncRoot = this.m_domainTable.SyncRoot;
			CookieCollection obj4;
			lock (syncRoot)
			{
				foreach (object obj in this.m_domainTable)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					PathList pathList;
					if (domain == null)
					{
						string text = (string)dictionaryEntry.Key;
						pathList = (PathList)dictionaryEntry.Value;
					}
					else
					{
						pathList = (PathList)this.m_domainTable[domain];
					}
					num2 = 0;
					object syncRoot2 = pathList.SyncRoot;
					lock (syncRoot2)
					{
						foreach (object obj2 in pathList.Values)
						{
							CookieCollection cookieCollection2 = (CookieCollection)obj2;
							num3 = this.ExpireCollection(cookieCollection2);
							num += num3;
							this.m_count -= num3;
							num2 += cookieCollection2.Count;
							DateTime dateTime2;
							if (cookieCollection2.Count > 0 && (dateTime2 = cookieCollection2.TimeStamp(CookieCollection.Stamp.Check)) < dateTime)
							{
								cookieCollection = cookieCollection2;
								dateTime = dateTime2;
							}
						}
					}
					int num5 = Math.Min((int)((float)num2 * num4), Math.Min(this.m_maxCookiesPerDomain, this.m_maxCookies) - 1);
					if (num2 > num5)
					{
						syncRoot2 = pathList.SyncRoot;
						Array array;
						Array array2;
						lock (syncRoot2)
						{
							array = Array.CreateInstance(typeof(CookieCollection), pathList.Count);
							array2 = Array.CreateInstance(typeof(DateTime), pathList.Count);
							foreach (object obj3 in pathList.Values)
							{
								CookieCollection cookieCollection3 = (CookieCollection)obj3;
								array2.SetValue(cookieCollection3.TimeStamp(CookieCollection.Stamp.Check), num3);
								array.SetValue(cookieCollection3, num3);
								num3++;
							}
						}
						Array.Sort(array2, array);
						num3 = 0;
						for (int i = 0; i < array.Length; i++)
						{
							CookieCollection cookieCollection4 = (CookieCollection)array.GetValue(i);
							obj4 = cookieCollection4;
							lock (obj4)
							{
								while (num2 > num5 && cookieCollection4.Count > 0)
								{
									cookieCollection4.RemoveAt(0);
									num2--;
									this.m_count--;
									num++;
								}
							}
							if (num2 <= num5)
							{
								break;
							}
						}
						if (num2 > num5 && domain != null)
						{
							return false;
						}
					}
				}
			}
			if (domain != null)
			{
				return true;
			}
			if (num != 0)
			{
				return true;
			}
			if (dateTime == DateTime.MaxValue)
			{
				return false;
			}
			obj4 = cookieCollection;
			lock (obj4)
			{
				while (this.m_count >= this.m_maxCookies && cookieCollection.Count > 0)
				{
					cookieCollection.RemoveAt(0);
					this.m_count--;
				}
			}
			return true;
		}

		// Token: 0x060032FF RID: 13055 RVA: 0x000B19F4 File Offset: 0x000AFBF4
		private int ExpireCollection(CookieCollection cc)
		{
			int result;
			lock (cc)
			{
				int count = cc.Count;
				for (int i = count - 1; i >= 0; i--)
				{
					if (cc[i].Expired)
					{
						cc.RemoveAt(i);
					}
				}
				result = count - cc.Count;
			}
			return result;
		}

		/// <summary>Adds the contents of a <see cref="T:System.Net.CookieCollection" /> to the <see cref="T:System.Net.CookieContainer" />.</summary>
		/// <param name="cookies">The <see cref="T:System.Net.CookieCollection" /> to be added to the <see cref="T:System.Net.CookieContainer" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cookies" /> is <see langword="null" />.</exception>
		// Token: 0x06003300 RID: 13056 RVA: 0x000B1A60 File Offset: 0x000AFC60
		public void Add(CookieCollection cookies)
		{
			if (cookies == null)
			{
				throw new ArgumentNullException("cookies");
			}
			foreach (object obj in cookies)
			{
				Cookie cookie = (Cookie)obj;
				this.Add(cookie);
			}
		}

		// Token: 0x06003301 RID: 13057 RVA: 0x000B1AC4 File Offset: 0x000AFCC4
		internal bool IsLocalDomain(string host)
		{
			int num = host.IndexOf('.');
			if (num == -1)
			{
				return true;
			}
			if (host == "127.0.0.1" || host == "::1" || host == "0:0:0:0:0:0:0:1")
			{
				return true;
			}
			if (string.Compare(this.m_fqdnMyDomain, 0, host, num, this.m_fqdnMyDomain.Length, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			string[] array = host.Split('.', StringSplitOptions.None);
			if (array != null && array.Length == 4 && array[0] == "127")
			{
				int i = 1;
				while (i < 4)
				{
					switch (array[i].Length)
					{
					case 1:
						break;
					case 2:
						goto IL_BB;
					case 3:
						if (array[i][2] >= '0' && array[i][2] <= '9')
						{
							goto IL_BB;
						}
						goto IL_F7;
					default:
						goto IL_F7;
					}
					IL_D5:
					if (array[i][0] >= '0' && array[i][0] <= '9')
					{
						i++;
						continue;
					}
					break;
					IL_BB:
					if (array[i][1] >= '0' && array[i][1] <= '9')
					{
						goto IL_D5;
					}
					break;
				}
				IL_F7:
				if (i == 4)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Adds a <see cref="T:System.Net.Cookie" /> to the <see cref="T:System.Net.CookieContainer" /> for a particular URI.</summary>
		/// <param name="uri">The URI of the <see cref="T:System.Net.Cookie" /> to be added to the <see cref="T:System.Net.CookieContainer" />.</param>
		/// <param name="cookie">The <see cref="T:System.Net.Cookie" /> to be added to the <see cref="T:System.Net.CookieContainer" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uri" /> is <see langword="null" /> or <paramref name="cookie" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.CookieException">
		///   <paramref name="cookie" /> is larger than <paramref name="maxCookieSize" />.  
		/// -or-  
		/// The domain for <paramref name="cookie" /> is not a valid URI.</exception>
		// Token: 0x06003302 RID: 13058 RVA: 0x000B1BD0 File Offset: 0x000AFDD0
		public void Add(Uri uri, Cookie cookie)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (cookie == null)
			{
				throw new ArgumentNullException("cookie");
			}
			Cookie cookie2 = cookie.Clone();
			cookie2.VerifySetDefaults(cookie2.Variant, uri, this.IsLocalDomain(uri.Host), this.m_fqdnMyDomain, true, true);
			this.Add(cookie2, true);
		}

		/// <summary>Adds the contents of a <see cref="T:System.Net.CookieCollection" /> to the <see cref="T:System.Net.CookieContainer" /> for a particular URI.</summary>
		/// <param name="uri">The URI of the <see cref="T:System.Net.CookieCollection" /> to be added to the <see cref="T:System.Net.CookieContainer" />.</param>
		/// <param name="cookies">The <see cref="T:System.Net.CookieCollection" /> to be added to the <see cref="T:System.Net.CookieContainer" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cookies" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The domain for one of the cookies in <paramref name="cookies" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.CookieException">One of the cookies in <paramref name="cookies" /> contains an invalid domain.</exception>
		// Token: 0x06003303 RID: 13059 RVA: 0x000B1C30 File Offset: 0x000AFE30
		public void Add(Uri uri, CookieCollection cookies)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (cookies == null)
			{
				throw new ArgumentNullException("cookies");
			}
			bool isLocalDomain = this.IsLocalDomain(uri.Host);
			foreach (object obj in cookies)
			{
				Cookie cookie = ((Cookie)obj).Clone();
				cookie.VerifySetDefaults(cookie.Variant, uri, isLocalDomain, this.m_fqdnMyDomain, true, true);
				this.Add(cookie, true);
			}
		}

		// Token: 0x06003304 RID: 13060 RVA: 0x000B1CD0 File Offset: 0x000AFED0
		internal CookieCollection CookieCutter(Uri uri, string headerName, string setCookieHeader, bool isThrow)
		{
			CookieCollection cookieCollection = new CookieCollection();
			CookieVariant variant = CookieVariant.Unknown;
			if (headerName == null)
			{
				variant = CookieVariant.Rfc2109;
			}
			else
			{
				for (int i = 0; i < CookieContainer.HeaderInfo.Length; i++)
				{
					if (string.Compare(headerName, CookieContainer.HeaderInfo[i].Name, StringComparison.OrdinalIgnoreCase) == 0)
					{
						variant = CookieContainer.HeaderInfo[i].Variant;
					}
				}
			}
			bool isLocalDomain = this.IsLocalDomain(uri.Host);
			try
			{
				CookieParser cookieParser = new CookieParser(setCookieHeader);
				for (;;)
				{
					Cookie cookie = cookieParser.Get();
					if (cookie == null)
					{
						goto IL_B0;
					}
					if (ValidationHelper.IsBlankString(cookie.Name))
					{
						if (isThrow)
						{
							break;
						}
					}
					else if (cookie.VerifySetDefaults(variant, uri, isLocalDomain, this.m_fqdnMyDomain, true, isThrow))
					{
						cookieCollection.InternalAdd(cookie, true);
					}
				}
				throw new CookieException(SR.GetString("Cookie format error."));
				IL_B0:;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (isThrow)
				{
					throw new CookieException(SR.GetString("An error occurred when parsing the Cookie header for Uri '{0}'.", new object[]
					{
						uri.AbsoluteUri
					}), ex);
				}
			}
			foreach (object obj in cookieCollection)
			{
				Cookie cookie2 = (Cookie)obj;
				this.Add(cookie2, isThrow);
			}
			return cookieCollection;
		}

		/// <summary>Gets a <see cref="T:System.Net.CookieCollection" /> that contains the <see cref="T:System.Net.Cookie" /> instances that are associated with a specific URI.</summary>
		/// <param name="uri">The URI of the <see cref="T:System.Net.Cookie" /> instances desired.</param>
		/// <returns>A <see cref="T:System.Net.CookieCollection" /> that contains the <see cref="T:System.Net.Cookie" /> instances that are associated with a specific URI.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uri" /> is <see langword="null" />.</exception>
		// Token: 0x06003305 RID: 13061 RVA: 0x000B1E34 File Offset: 0x000B0034
		public CookieCollection GetCookies(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			return this.InternalGetCookies(uri);
		}

		// Token: 0x06003306 RID: 13062 RVA: 0x000B1E54 File Offset: 0x000B0054
		internal CookieCollection InternalGetCookies(Uri uri)
		{
			bool isSecure = uri.Scheme == Uri.UriSchemeHttps;
			int port = uri.Port;
			CookieCollection cookieCollection = new CookieCollection();
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			string host = uri.Host;
			list.Add(host);
			list.Add("." + host);
			int num = host.IndexOf('.');
			if (num == -1)
			{
				if (this.m_fqdnMyDomain != null && this.m_fqdnMyDomain.Length != 0)
				{
					list.Add(host + this.m_fqdnMyDomain);
					list.Add(this.m_fqdnMyDomain);
				}
			}
			else
			{
				list.Add(host.Substring(num));
				if (host.Length > 2)
				{
					int num2 = host.LastIndexOf('.', host.Length - 2);
					if (num2 > 0)
					{
						num2 = host.LastIndexOf('.', num2 - 1);
					}
					if (num2 != -1)
					{
						while (num < num2 && (num = host.IndexOf('.', num + 1)) != -1)
						{
							list2.Add(host.Substring(num));
						}
					}
				}
			}
			this.BuildCookieCollectionFromDomainMatches(uri, isSecure, port, cookieCollection, list, false);
			this.BuildCookieCollectionFromDomainMatches(uri, isSecure, port, cookieCollection, list2, true);
			return cookieCollection;
		}

		// Token: 0x06003307 RID: 13063 RVA: 0x000B1F88 File Offset: 0x000B0188
		private void BuildCookieCollectionFromDomainMatches(Uri uri, bool isSecure, int port, CookieCollection cookies, List<string> domainAttribute, bool matchOnlyPlainCookie)
		{
			for (int i = 0; i < domainAttribute.Count; i++)
			{
				bool flag = false;
				bool flag2 = false;
				object syncRoot = this.m_domainTable.SyncRoot;
				PathList pathList;
				lock (syncRoot)
				{
					pathList = (PathList)this.m_domainTable[domainAttribute[i]];
				}
				if (pathList != null)
				{
					syncRoot = pathList.SyncRoot;
					lock (syncRoot)
					{
						foreach (object obj in pathList)
						{
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
							string text = (string)dictionaryEntry.Key;
							if (uri.AbsolutePath.StartsWith(CookieParser.CheckQuoted(text)))
							{
								flag = true;
								CookieCollection cookieCollection = (CookieCollection)dictionaryEntry.Value;
								cookieCollection.TimeStamp(CookieCollection.Stamp.Set);
								this.MergeUpdateCollections(cookies, cookieCollection, port, isSecure, matchOnlyPlainCookie);
								if (text == "/")
								{
									flag2 = true;
								}
							}
							else if (flag)
							{
								break;
							}
						}
					}
					if (!flag2)
					{
						CookieCollection cookieCollection2 = (CookieCollection)pathList["/"];
						if (cookieCollection2 != null)
						{
							cookieCollection2.TimeStamp(CookieCollection.Stamp.Set);
							this.MergeUpdateCollections(cookies, cookieCollection2, port, isSecure, matchOnlyPlainCookie);
						}
					}
					if (pathList.Count == 0)
					{
						this.AddRemoveDomain(domainAttribute[i], null);
					}
				}
			}
		}

		// Token: 0x06003308 RID: 13064 RVA: 0x000B2120 File Offset: 0x000B0320
		private void MergeUpdateCollections(CookieCollection destination, CookieCollection source, int port, bool isSecure, bool isPlainOnly)
		{
			lock (source)
			{
				for (int i = 0; i < source.Count; i++)
				{
					bool flag2 = false;
					Cookie cookie = source[i];
					if (cookie.Expired)
					{
						source.RemoveAt(i);
						this.m_count--;
						i--;
					}
					else
					{
						if (!isPlainOnly || cookie.Variant == CookieVariant.Plain)
						{
							if (cookie.PortList != null)
							{
								int[] portList = cookie.PortList;
								for (int j = 0; j < portList.Length; j++)
								{
									if (portList[j] == port)
									{
										flag2 = true;
										break;
									}
								}
							}
							else
							{
								flag2 = true;
							}
						}
						if (cookie.Secure && !isSecure)
						{
							flag2 = false;
						}
						if (flag2)
						{
							destination.InternalAdd(cookie, false);
						}
					}
				}
			}
		}

		/// <summary>Gets the HTTP cookie header that contains the HTTP cookies that represent the <see cref="T:System.Net.Cookie" /> instances that are associated with a specific URI.</summary>
		/// <param name="uri">The URI of the <see cref="T:System.Net.Cookie" /> instances desired.</param>
		/// <returns>An HTTP cookie header, with strings representing <see cref="T:System.Net.Cookie" /> instances delimited by semicolons.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uri" /> is <see langword="null" />.</exception>
		// Token: 0x06003309 RID: 13065 RVA: 0x000B21FC File Offset: 0x000B03FC
		public string GetCookieHeader(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			string text;
			return this.GetCookieHeader(uri, out text);
		}

		// Token: 0x0600330A RID: 13066 RVA: 0x000B2228 File Offset: 0x000B0428
		internal string GetCookieHeader(Uri uri, out string optCookie2)
		{
			CookieCollection cookieCollection = this.InternalGetCookies(uri);
			string text = string.Empty;
			string str = string.Empty;
			foreach (object obj in cookieCollection)
			{
				Cookie cookie = (Cookie)obj;
				text = text + str + cookie.ToString();
				str = "; ";
			}
			optCookie2 = (cookieCollection.IsOtherVersionSeen ? ("$Version=" + 1.ToString(NumberFormatInfo.InvariantInfo)) : string.Empty);
			return text;
		}

		/// <summary>Adds <see cref="T:System.Net.Cookie" /> instances for one or more cookies from an HTTP cookie header to the <see cref="T:System.Net.CookieContainer" /> for a specific URI.</summary>
		/// <param name="uri">The URI of the <see cref="T:System.Net.CookieCollection" />.</param>
		/// <param name="cookieHeader">The contents of an HTTP set-cookie header as returned by a HTTP server, with <see cref="T:System.Net.Cookie" /> instances delimited by commas.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uri" /> or <paramref name="cookieHeader" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.CookieException">One of the cookies is invalid.  
		///  -or-  
		///  An error occurred while adding one of the cookies to the container.</exception>
		// Token: 0x0600330B RID: 13067 RVA: 0x000B22D0 File Offset: 0x000B04D0
		public void SetCookies(Uri uri, string cookieHeader)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (cookieHeader == null)
			{
				throw new ArgumentNullException("cookieHeader");
			}
			this.CookieCutter(uri, null, cookieHeader, true);
		}

		// Token: 0x0600330C RID: 13068 RVA: 0x000B22FF File Offset: 0x000B04FF
		// Note: this type is marked as 'beforefieldinit'.
		static CookieContainer()
		{
		}

		/// <summary>Represents the default maximum number of <see cref="T:System.Net.Cookie" /> instances that the <see cref="T:System.Net.CookieContainer" /> can hold. This field is constant.</summary>
		// Token: 0x04001DEF RID: 7663
		public const int DefaultCookieLimit = 300;

		/// <summary>Represents the default maximum number of <see cref="T:System.Net.Cookie" /> instances that the <see cref="T:System.Net.CookieContainer" /> can reference per domain. This field is constant.</summary>
		// Token: 0x04001DF0 RID: 7664
		public const int DefaultPerDomainCookieLimit = 20;

		/// <summary>Represents the default maximum size, in bytes, of the <see cref="T:System.Net.Cookie" /> instances that the <see cref="T:System.Net.CookieContainer" /> can hold. This field is constant.</summary>
		// Token: 0x04001DF1 RID: 7665
		public const int DefaultCookieLengthLimit = 4096;

		// Token: 0x04001DF2 RID: 7666
		private static readonly HeaderVariantInfo[] HeaderInfo = new HeaderVariantInfo[]
		{
			new HeaderVariantInfo("Set-Cookie", CookieVariant.Rfc2109),
			new HeaderVariantInfo("Set-Cookie2", CookieVariant.Rfc2965)
		};

		// Token: 0x04001DF3 RID: 7667
		private Hashtable m_domainTable = new Hashtable();

		// Token: 0x04001DF4 RID: 7668
		private int m_maxCookieSize = 4096;

		// Token: 0x04001DF5 RID: 7669
		private int m_maxCookies = 300;

		// Token: 0x04001DF6 RID: 7670
		private int m_maxCookiesPerDomain = 20;

		// Token: 0x04001DF7 RID: 7671
		private int m_count;

		// Token: 0x04001DF8 RID: 7672
		private string m_fqdnMyDomain = string.Empty;
	}
}
