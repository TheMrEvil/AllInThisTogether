using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace WebSocketSharp.Net
{
	// Token: 0x0200001B RID: 27
	[Serializable]
	public class CookieCollection : ICollection<Cookie>, IEnumerable<Cookie>, IEnumerable
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x0000C581 File Offset: 0x0000A781
		public CookieCollection()
		{
			this._list = new List<Cookie>();
			this._sync = ((ICollection)this._list).SyncRoot;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000C5A8 File Offset: 0x0000A7A8
		internal IList<Cookie> List
		{
			get
			{
				return this._list;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000C5C0 File Offset: 0x0000A7C0
		internal IEnumerable<Cookie> Sorted
		{
			get
			{
				List<Cookie> list = new List<Cookie>(this._list);
				bool flag = list.Count > 1;
				if (flag)
				{
					list.Sort(new Comparison<Cookie>(CookieCollection.compareForSorted));
				}
				return list;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000C600 File Offset: 0x0000A800
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000C620 File Offset: 0x0000A820
		// (set) Token: 0x060001DD RID: 477 RVA: 0x0000C638 File Offset: 0x0000A838
		public bool IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
			internal set
			{
				this._readOnly = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000C644 File Offset: 0x0000A844
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000074 RID: 116
		public Cookie this[int index]
		{
			get
			{
				bool flag = index < 0 || index >= this._list.Count;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this._list[index];
			}
		}

		// Token: 0x17000075 RID: 117
		public Cookie this[string name]
		{
			get
			{
				bool flag = name == null;
				if (flag)
				{
					throw new ArgumentNullException("name");
				}
				StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase;
				foreach (Cookie cookie in this.Sorted)
				{
					bool flag2 = cookie.Name.Equals(name, comparisonType);
					if (flag2)
					{
						return cookie;
					}
				}
				return null;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000C720 File Offset: 0x0000A920
		public object SyncRoot
		{
			get
			{
				return this._sync;
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000C738 File Offset: 0x0000A938
		private void add(Cookie cookie)
		{
			int num = this.search(cookie);
			bool flag = num == -1;
			if (flag)
			{
				this._list.Add(cookie);
			}
			else
			{
				this._list[num] = cookie;
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000C774 File Offset: 0x0000A974
		private static int compareForSort(Cookie x, Cookie y)
		{
			return x.Name.Length + x.Value.Length - (y.Name.Length + y.Value.Length);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000C7B8 File Offset: 0x0000A9B8
		private static int compareForSorted(Cookie x, Cookie y)
		{
			int num = x.Version - y.Version;
			return (num != 0) ? num : (((num = x.Name.CompareTo(y.Name)) != 0) ? num : (y.Path.Length - x.Path.Length));
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000C810 File Offset: 0x0000AA10
		private static CookieCollection parseRequest(string value)
		{
			CookieCollection cookieCollection = new CookieCollection();
			Cookie cookie = null;
			int num = 0;
			StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase;
			List<string> list = value.SplitHeaderValue(new char[]
			{
				',',
				';'
			}).ToList<string>();
			for (int i = 0; i < list.Count; i++)
			{
				string text = list[i].Trim();
				bool flag = text.Length == 0;
				if (!flag)
				{
					int num2 = text.IndexOf('=');
					bool flag2 = num2 == -1;
					if (flag2)
					{
						bool flag3 = cookie == null;
						if (!flag3)
						{
							bool flag4 = text.Equals("$port", comparisonType);
							if (flag4)
							{
								cookie.Port = "\"\"";
							}
						}
					}
					else
					{
						bool flag5 = num2 == 0;
						if (flag5)
						{
							bool flag6 = cookie != null;
							if (flag6)
							{
								cookieCollection.add(cookie);
								cookie = null;
							}
						}
						else
						{
							string text2 = text.Substring(0, num2).TrimEnd(new char[]
							{
								' '
							});
							string text3 = (num2 < text.Length - 1) ? text.Substring(num2 + 1).TrimStart(new char[]
							{
								' '
							}) : string.Empty;
							bool flag7 = text2.Equals("$version", comparisonType);
							if (flag7)
							{
								bool flag8 = text3.Length == 0;
								if (!flag8)
								{
									int num3;
									bool flag9 = !int.TryParse(text3.Unquote(), out num3);
									if (!flag9)
									{
										num = num3;
									}
								}
							}
							else
							{
								bool flag10 = text2.Equals("$path", comparisonType);
								if (flag10)
								{
									bool flag11 = cookie == null;
									if (!flag11)
									{
										bool flag12 = text3.Length == 0;
										if (!flag12)
										{
											cookie.Path = text3;
										}
									}
								}
								else
								{
									bool flag13 = text2.Equals("$domain", comparisonType);
									if (flag13)
									{
										bool flag14 = cookie == null;
										if (!flag14)
										{
											bool flag15 = text3.Length == 0;
											if (!flag15)
											{
												cookie.Domain = text3;
											}
										}
									}
									else
									{
										bool flag16 = text2.Equals("$port", comparisonType);
										if (flag16)
										{
											bool flag17 = cookie == null;
											if (!flag17)
											{
												bool flag18 = text3.Length == 0;
												if (!flag18)
												{
													cookie.Port = text3;
												}
											}
										}
										else
										{
											bool flag19 = cookie != null;
											if (flag19)
											{
												cookieCollection.add(cookie);
											}
											bool flag20 = !Cookie.TryCreate(text2, text3, out cookie);
											if (!flag20)
											{
												bool flag21 = num != 0;
												if (flag21)
												{
													cookie.Version = num;
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			bool flag22 = cookie != null;
			if (flag22)
			{
				cookieCollection.add(cookie);
			}
			return cookieCollection;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000CABC File Offset: 0x0000ACBC
		private static CookieCollection parseResponse(string value)
		{
			CookieCollection cookieCollection = new CookieCollection();
			Cookie cookie = null;
			StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase;
			List<string> list = value.SplitHeaderValue(new char[]
			{
				',',
				';'
			}).ToList<string>();
			for (int i = 0; i < list.Count; i++)
			{
				string text = list[i].Trim();
				bool flag = text.Length == 0;
				if (!flag)
				{
					int num = text.IndexOf('=');
					bool flag2 = num == -1;
					if (flag2)
					{
						bool flag3 = cookie == null;
						if (!flag3)
						{
							bool flag4 = text.Equals("port", comparisonType);
							if (flag4)
							{
								cookie.Port = "\"\"";
							}
							else
							{
								bool flag5 = text.Equals("discard", comparisonType);
								if (flag5)
								{
									cookie.Discard = true;
								}
								else
								{
									bool flag6 = text.Equals("secure", comparisonType);
									if (flag6)
									{
										cookie.Secure = true;
									}
									else
									{
										bool flag7 = text.Equals("httponly", comparisonType);
										if (flag7)
										{
											cookie.HttpOnly = true;
										}
									}
								}
							}
						}
					}
					else
					{
						bool flag8 = num == 0;
						if (flag8)
						{
							bool flag9 = cookie != null;
							if (flag9)
							{
								cookieCollection.add(cookie);
								cookie = null;
							}
						}
						else
						{
							string text2 = text.Substring(0, num).TrimEnd(new char[]
							{
								' '
							});
							string text3 = (num < text.Length - 1) ? text.Substring(num + 1).TrimStart(new char[]
							{
								' '
							}) : string.Empty;
							bool flag10 = text2.Equals("version", comparisonType);
							if (flag10)
							{
								bool flag11 = cookie == null;
								if (!flag11)
								{
									bool flag12 = text3.Length == 0;
									if (!flag12)
									{
										int version;
										bool flag13 = !int.TryParse(text3.Unquote(), out version);
										if (!flag13)
										{
											cookie.Version = version;
										}
									}
								}
							}
							else
							{
								bool flag14 = text2.Equals("expires", comparisonType);
								if (flag14)
								{
									bool flag15 = text3.Length == 0;
									if (!flag15)
									{
										bool flag16 = i == list.Count - 1;
										if (flag16)
										{
											break;
										}
										i++;
										bool flag17 = cookie == null;
										if (!flag17)
										{
											bool flag18 = cookie.Expires != DateTime.MinValue;
											if (!flag18)
											{
												StringBuilder stringBuilder = new StringBuilder(text3, 32);
												stringBuilder.AppendFormat(", {0}", list[i].Trim());
												DateTime dateTime;
												bool flag19 = !DateTime.TryParseExact(stringBuilder.ToString(), new string[]
												{
													"ddd, dd'-'MMM'-'yyyy HH':'mm':'ss 'GMT'",
													"r"
												}, CultureInfo.CreateSpecificCulture("en-US"), DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out dateTime);
												if (!flag19)
												{
													cookie.Expires = dateTime.ToLocalTime();
												}
											}
										}
									}
								}
								else
								{
									bool flag20 = text2.Equals("max-age", comparisonType);
									if (flag20)
									{
										bool flag21 = cookie == null;
										if (!flag21)
										{
											bool flag22 = text3.Length == 0;
											if (!flag22)
											{
												int maxAge;
												bool flag23 = !int.TryParse(text3.Unquote(), out maxAge);
												if (!flag23)
												{
													cookie.MaxAge = maxAge;
												}
											}
										}
									}
									else
									{
										bool flag24 = text2.Equals("path", comparisonType);
										if (flag24)
										{
											bool flag25 = cookie == null;
											if (!flag25)
											{
												bool flag26 = text3.Length == 0;
												if (!flag26)
												{
													cookie.Path = text3;
												}
											}
										}
										else
										{
											bool flag27 = text2.Equals("domain", comparisonType);
											if (flag27)
											{
												bool flag28 = cookie == null;
												if (!flag28)
												{
													bool flag29 = text3.Length == 0;
													if (!flag29)
													{
														cookie.Domain = text3;
													}
												}
											}
											else
											{
												bool flag30 = text2.Equals("port", comparisonType);
												if (flag30)
												{
													bool flag31 = cookie == null;
													if (!flag31)
													{
														bool flag32 = text3.Length == 0;
														if (!flag32)
														{
															cookie.Port = text3;
														}
													}
												}
												else
												{
													bool flag33 = text2.Equals("comment", comparisonType);
													if (flag33)
													{
														bool flag34 = cookie == null;
														if (!flag34)
														{
															bool flag35 = text3.Length == 0;
															if (!flag35)
															{
																cookie.Comment = CookieCollection.urlDecode(text3, Encoding.UTF8);
															}
														}
													}
													else
													{
														bool flag36 = text2.Equals("commenturl", comparisonType);
														if (flag36)
														{
															bool flag37 = cookie == null;
															if (!flag37)
															{
																bool flag38 = text3.Length == 0;
																if (!flag38)
																{
																	cookie.CommentUri = text3.Unquote().ToUri();
																}
															}
														}
														else
														{
															bool flag39 = text2.Equals("samesite", comparisonType);
															if (flag39)
															{
																bool flag40 = cookie == null;
																if (!flag40)
																{
																	bool flag41 = text3.Length == 0;
																	if (!flag41)
																	{
																		cookie.SameSite = text3.Unquote();
																	}
																}
															}
															else
															{
																bool flag42 = cookie != null;
																if (flag42)
																{
																	cookieCollection.add(cookie);
																}
																Cookie.TryCreate(text2, text3, out cookie);
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			bool flag43 = cookie != null;
			if (flag43)
			{
				cookieCollection.add(cookie);
			}
			return cookieCollection;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000CFF4 File Offset: 0x0000B1F4
		private int search(Cookie cookie)
		{
			for (int i = this._list.Count - 1; i >= 0; i--)
			{
				bool flag = this._list[i].EqualsWithoutValue(cookie);
				if (flag)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000D044 File Offset: 0x0000B244
		private static string urlDecode(string s, Encoding encoding)
		{
			bool flag = s.IndexOfAny(new char[]
			{
				'%',
				'+'
			}) == -1;
			string result;
			if (flag)
			{
				result = s;
			}
			else
			{
				try
				{
					result = HttpUtility.UrlDecode(s, encoding);
				}
				catch
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000D098 File Offset: 0x0000B298
		internal static CookieCollection Parse(string value, bool response)
		{
			CookieCollection result;
			try
			{
				result = (response ? CookieCollection.parseResponse(value) : CookieCollection.parseRequest(value));
			}
			catch (Exception innerException)
			{
				throw new CookieException("It could not be parsed.", innerException);
			}
			return result;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000D0DC File Offset: 0x0000B2DC
		internal void SetOrRemove(Cookie cookie)
		{
			int num = this.search(cookie);
			bool flag = num == -1;
			if (flag)
			{
				bool expired = cookie.Expired;
				if (!expired)
				{
					this._list.Add(cookie);
				}
			}
			else
			{
				bool expired2 = cookie.Expired;
				if (expired2)
				{
					this._list.RemoveAt(num);
				}
				else
				{
					this._list[num] = cookie;
				}
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000D140 File Offset: 0x0000B340
		internal void SetOrRemove(CookieCollection cookies)
		{
			foreach (Cookie orRemove in cookies._list)
			{
				this.SetOrRemove(orRemove);
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000D198 File Offset: 0x0000B398
		internal void Sort()
		{
			bool flag = this._list.Count > 1;
			if (flag)
			{
				this._list.Sort(new Comparison<Cookie>(CookieCollection.compareForSort));
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
		public void Add(Cookie cookie)
		{
			bool readOnly = this._readOnly;
			if (readOnly)
			{
				string message = "The collection is read-only.";
				throw new InvalidOperationException(message);
			}
			bool flag = cookie == null;
			if (flag)
			{
				throw new ArgumentNullException("cookie");
			}
			this.add(cookie);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000D214 File Offset: 0x0000B414
		public void Add(CookieCollection cookies)
		{
			bool readOnly = this._readOnly;
			if (readOnly)
			{
				string message = "The collection is read-only.";
				throw new InvalidOperationException(message);
			}
			bool flag = cookies == null;
			if (flag)
			{
				throw new ArgumentNullException("cookies");
			}
			foreach (Cookie cookie in cookies._list)
			{
				this.add(cookie);
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000D298 File Offset: 0x0000B498
		public void Clear()
		{
			bool readOnly = this._readOnly;
			if (readOnly)
			{
				string message = "The collection is read-only.";
				throw new InvalidOperationException(message);
			}
			this._list.Clear();
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000D2CC File Offset: 0x0000B4CC
		public bool Contains(Cookie cookie)
		{
			bool flag = cookie == null;
			if (flag)
			{
				throw new ArgumentNullException("cookie");
			}
			return this.search(cookie) > -1;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000D2FC File Offset: 0x0000B4FC
		public void CopyTo(Cookie[] array, int index)
		{
			bool flag = array == null;
			if (flag)
			{
				throw new ArgumentNullException("array");
			}
			bool flag2 = index < 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("index", "Less than zero.");
			}
			bool flag3 = array.Length - index < this._list.Count;
			if (flag3)
			{
				string message = "The available space of the array is not enough to copy to.";
				throw new ArgumentException(message);
			}
			this._list.CopyTo(array, index);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000D368 File Offset: 0x0000B568
		public IEnumerator<Cookie> GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000D38C File Offset: 0x0000B58C
		public bool Remove(Cookie cookie)
		{
			bool readOnly = this._readOnly;
			if (readOnly)
			{
				string message = "The collection is read-only.";
				throw new InvalidOperationException(message);
			}
			bool flag = cookie == null;
			if (flag)
			{
				throw new ArgumentNullException("cookie");
			}
			int num = this.search(cookie);
			bool flag2 = num == -1;
			bool result;
			if (flag2)
			{
				result = false;
			}
			else
			{
				this._list.RemoveAt(num);
				result = true;
			}
			return result;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000D3F0 File Offset: 0x0000B5F0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x040000B6 RID: 182
		private List<Cookie> _list;

		// Token: 0x040000B7 RID: 183
		private bool _readOnly;

		// Token: 0x040000B8 RID: 184
		private object _sync;
	}
}
