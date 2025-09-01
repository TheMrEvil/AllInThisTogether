using System;
using System.Globalization;
using System.Text;

namespace WebSocketSharp.Net
{
	// Token: 0x0200001A RID: 26
	[Serializable]
	public sealed class Cookie
	{
		// Token: 0x060001A5 RID: 421 RVA: 0x0000B85F File Offset: 0x00009A5F
		static Cookie()
		{
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000B882 File Offset: 0x00009A82
		internal Cookie()
		{
			this.init(string.Empty, string.Empty, string.Empty, string.Empty);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000B8A7 File Offset: 0x00009AA7
		public Cookie(string name, string value) : this(name, value, string.Empty, string.Empty)
		{
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000B8BD File Offset: 0x00009ABD
		public Cookie(string name, string value, string path) : this(name, value, path, string.Empty)
		{
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000B8D0 File Offset: 0x00009AD0
		public Cookie(string name, string value, string path, string domain)
		{
			bool flag = name == null;
			if (flag)
			{
				throw new ArgumentNullException("name");
			}
			bool flag2 = name.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("An empty string.", "name");
			}
			bool flag3 = name[0] == '$';
			if (flag3)
			{
				string message = "It starts with a dollar sign.";
				throw new ArgumentException(message, "name");
			}
			bool flag4 = !name.IsToken();
			if (flag4)
			{
				string message2 = "It contains an invalid character.";
				throw new ArgumentException(message2, "name");
			}
			bool flag5 = value == null;
			if (flag5)
			{
				value = string.Empty;
			}
			bool flag6 = value.Contains(Cookie._reservedCharsForValue);
			if (flag6)
			{
				bool flag7 = !value.IsEnclosedIn('"');
				if (flag7)
				{
					string message3 = "A string not enclosed in double quotes.";
					throw new ArgumentException(message3, "value");
				}
			}
			this.init(name, value, path ?? string.Empty, domain ?? string.Empty);
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000B9C4 File Offset: 0x00009BC4
		internal bool ExactDomain
		{
			get
			{
				return this._domain.Length == 0 || this._domain[0] != '.';
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000B9FC File Offset: 0x00009BFC
		// (set) Token: 0x060001AC RID: 428 RVA: 0x0000BA6C File Offset: 0x00009C6C
		internal int MaxAge
		{
			get
			{
				bool flag = this._expires == DateTime.MinValue;
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					DateTime d = (this._expires.Kind != DateTimeKind.Local) ? this._expires.ToLocalTime() : this._expires;
					TimeSpan t = d - DateTime.Now;
					result = ((t > TimeSpan.Zero) ? ((int)t.TotalSeconds) : 0);
				}
				return result;
			}
			set
			{
				this._expires = ((value > 0) ? DateTime.Now.AddSeconds((double)value) : DateTime.Now);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000BA9C File Offset: 0x00009C9C
		internal int[] Ports
		{
			get
			{
				return this._ports ?? Cookie._emptyPorts;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000BAC0 File Offset: 0x00009CC0
		// (set) Token: 0x060001AF RID: 431 RVA: 0x0000BAD8 File Offset: 0x00009CD8
		internal string SameSite
		{
			get
			{
				return this._sameSite;
			}
			set
			{
				this._sameSite = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000BAE4 File Offset: 0x00009CE4
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x0000BAFC File Offset: 0x00009CFC
		public string Comment
		{
			get
			{
				return this._comment;
			}
			internal set
			{
				this._comment = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000BB08 File Offset: 0x00009D08
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0000BB20 File Offset: 0x00009D20
		public Uri CommentUri
		{
			get
			{
				return this._commentUri;
			}
			internal set
			{
				this._commentUri = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000BB2C File Offset: 0x00009D2C
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x0000BB44 File Offset: 0x00009D44
		public bool Discard
		{
			get
			{
				return this._discard;
			}
			internal set
			{
				this._discard = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000BB50 File Offset: 0x00009D50
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x0000BB68 File Offset: 0x00009D68
		public string Domain
		{
			get
			{
				return this._domain;
			}
			set
			{
				this._domain = (value ?? string.Empty);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000BB7C File Offset: 0x00009D7C
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x0000BBB3 File Offset: 0x00009DB3
		public bool Expired
		{
			get
			{
				return this._expires != DateTime.MinValue && this._expires <= DateTime.Now;
			}
			set
			{
				this._expires = (value ? DateTime.Now : DateTime.MinValue);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000BBCC File Offset: 0x00009DCC
		// (set) Token: 0x060001BB RID: 443 RVA: 0x0000BBE4 File Offset: 0x00009DE4
		public DateTime Expires
		{
			get
			{
				return this._expires;
			}
			set
			{
				this._expires = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000BBF0 File Offset: 0x00009DF0
		// (set) Token: 0x060001BD RID: 445 RVA: 0x0000BC08 File Offset: 0x00009E08
		public bool HttpOnly
		{
			get
			{
				return this._httpOnly;
			}
			set
			{
				this._httpOnly = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001BE RID: 446 RVA: 0x0000BC14 File Offset: 0x00009E14
		// (set) Token: 0x060001BF RID: 447 RVA: 0x0000BC2C File Offset: 0x00009E2C
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					throw new ArgumentNullException("value");
				}
				bool flag2 = value.Length == 0;
				if (flag2)
				{
					throw new ArgumentException("An empty string.", "value");
				}
				bool flag3 = value[0] == '$';
				if (flag3)
				{
					string message = "It starts with a dollar sign.";
					throw new ArgumentException(message, "value");
				}
				bool flag4 = !value.IsToken();
				if (flag4)
				{
					string message2 = "It contains an invalid character.";
					throw new ArgumentException(message2, "value");
				}
				this._name = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000BCB8 File Offset: 0x00009EB8
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		public string Path
		{
			get
			{
				return this._path;
			}
			set
			{
				this._path = (value ?? string.Empty);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000BCE4 File Offset: 0x00009EE4
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x0000BCFC File Offset: 0x00009EFC
		public string Port
		{
			get
			{
				return this._port;
			}
			internal set
			{
				int[] ports;
				bool flag = !Cookie.tryCreatePorts(value, out ports);
				if (!flag)
				{
					this._port = value;
					this._ports = ports;
				}
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000BD2C File Offset: 0x00009F2C
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x0000BD44 File Offset: 0x00009F44
		public bool Secure
		{
			get
			{
				return this._secure;
			}
			set
			{
				this._secure = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000BD50 File Offset: 0x00009F50
		public DateTime TimeStamp
		{
			get
			{
				return this._timeStamp;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000BD68 File Offset: 0x00009F68
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x0000BD80 File Offset: 0x00009F80
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					value = string.Empty;
				}
				bool flag2 = value.Contains(Cookie._reservedCharsForValue);
				if (flag2)
				{
					bool flag3 = !value.IsEnclosedIn('"');
					if (flag3)
					{
						string message = "A string not enclosed in double quotes.";
						throw new ArgumentException(message, "value");
					}
				}
				this._value = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000BDD8 File Offset: 0x00009FD8
		// (set) Token: 0x060001CA RID: 458 RVA: 0x0000BDF0 File Offset: 0x00009FF0
		public int Version
		{
			get
			{
				return this._version;
			}
			internal set
			{
				bool flag = value < 0 || value > 1;
				if (!flag)
				{
					this._version = value;
				}
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000BE18 File Offset: 0x0000A018
		private static int hash(int i, int j, int k, int l, int m)
		{
			return i ^ (j << 13 | j >> 19) ^ (k << 26 | k >> 6) ^ (l << 7 | l >> 25) ^ (m << 20 | m >> 12);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000BE53 File Offset: 0x0000A053
		private void init(string name, string value, string path, string domain)
		{
			this._name = name;
			this._value = value;
			this._path = path;
			this._domain = domain;
			this._expires = DateTime.MinValue;
			this._timeStamp = DateTime.Now;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000BE8C File Offset: 0x0000A08C
		private string toResponseStringVersion0()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.AppendFormat("{0}={1}", this._name, this._value);
			bool flag = this._expires != DateTime.MinValue;
			if (flag)
			{
				stringBuilder.AppendFormat("; Expires={0}", this._expires.ToUniversalTime().ToString("ddd, dd'-'MMM'-'yyyy HH':'mm':'ss 'GMT'", CultureInfo.CreateSpecificCulture("en-US")));
			}
			bool flag2 = !this._path.IsNullOrEmpty();
			if (flag2)
			{
				stringBuilder.AppendFormat("; Path={0}", this._path);
			}
			bool flag3 = !this._domain.IsNullOrEmpty();
			if (flag3)
			{
				stringBuilder.AppendFormat("; Domain={0}", this._domain);
			}
			bool flag4 = !this._sameSite.IsNullOrEmpty();
			if (flag4)
			{
				stringBuilder.AppendFormat("; SameSite={0}", this._sameSite);
			}
			bool secure = this._secure;
			if (secure)
			{
				stringBuilder.Append("; Secure");
			}
			bool httpOnly = this._httpOnly;
			if (httpOnly)
			{
				stringBuilder.Append("; HttpOnly");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000BFAC File Offset: 0x0000A1AC
		private string toResponseStringVersion1()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.AppendFormat("{0}={1}; Version={2}", this._name, this._value, this._version);
			bool flag = this._expires != DateTime.MinValue;
			if (flag)
			{
				stringBuilder.AppendFormat("; Max-Age={0}", this.MaxAge);
			}
			bool flag2 = !this._path.IsNullOrEmpty();
			if (flag2)
			{
				stringBuilder.AppendFormat("; Path={0}", this._path);
			}
			bool flag3 = !this._domain.IsNullOrEmpty();
			if (flag3)
			{
				stringBuilder.AppendFormat("; Domain={0}", this._domain);
			}
			bool flag4 = this._port != null;
			if (flag4)
			{
				bool flag5 = this._port != "\"\"";
				if (flag5)
				{
					stringBuilder.AppendFormat("; Port={0}", this._port);
				}
				else
				{
					stringBuilder.Append("; Port");
				}
			}
			bool flag6 = this._comment != null;
			if (flag6)
			{
				stringBuilder.AppendFormat("; Comment={0}", HttpUtility.UrlEncode(this._comment));
			}
			bool flag7 = this._commentUri != null;
			if (flag7)
			{
				string originalString = this._commentUri.OriginalString;
				stringBuilder.AppendFormat("; CommentURL={0}", (!originalString.IsToken()) ? originalString.Quote() : originalString);
			}
			bool discard = this._discard;
			if (discard)
			{
				stringBuilder.Append("; Discard");
			}
			bool secure = this._secure;
			if (secure)
			{
				stringBuilder.Append("; Secure");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000C144 File Offset: 0x0000A344
		private static bool tryCreatePorts(string value, out int[] result)
		{
			result = null;
			string[] array = value.Trim(new char[]
			{
				'"'
			}).Split(new char[]
			{
				','
			});
			int num = array.Length;
			int[] array2 = new int[num];
			for (int i = 0; i < num; i++)
			{
				string text = array[i].Trim();
				bool flag = text.Length == 0;
				if (flag)
				{
					array2[i] = int.MinValue;
				}
				else
				{
					bool flag2 = !int.TryParse(text, out array2[i]);
					if (flag2)
					{
						return false;
					}
				}
			}
			result = array2;
			return true;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000C1E4 File Offset: 0x0000A3E4
		internal bool EqualsWithoutValue(Cookie cookie)
		{
			StringComparison comparisonType = StringComparison.InvariantCulture;
			StringComparison comparisonType2 = StringComparison.InvariantCultureIgnoreCase;
			return this._name.Equals(cookie._name, comparisonType2) && this._path.Equals(cookie._path, comparisonType) && this._domain.Equals(cookie._domain, comparisonType2) && this._version == cookie._version;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000C248 File Offset: 0x0000A448
		internal bool EqualsWithoutValueAndVersion(Cookie cookie)
		{
			StringComparison comparisonType = StringComparison.InvariantCulture;
			StringComparison comparisonType2 = StringComparison.InvariantCultureIgnoreCase;
			return this._name.Equals(cookie._name, comparisonType2) && this._path.Equals(cookie._path, comparisonType) && this._domain.Equals(cookie._domain, comparisonType2);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000C29C File Offset: 0x0000A49C
		internal string ToRequestString(Uri uri)
		{
			bool flag = this._name.Length == 0;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				bool flag2 = this._version == 0;
				if (flag2)
				{
					result = string.Format("{0}={1}", this._name, this._value);
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder(64);
					stringBuilder.AppendFormat("$Version={0}; {1}={2}", this._version, this._name, this._value);
					bool flag3 = !this._path.IsNullOrEmpty();
					if (flag3)
					{
						stringBuilder.AppendFormat("; $Path={0}", this._path);
					}
					else
					{
						bool flag4 = uri != null;
						if (flag4)
						{
							stringBuilder.AppendFormat("; $Path={0}", uri.GetAbsolutePath());
						}
						else
						{
							stringBuilder.Append("; $Path=/");
						}
					}
					bool flag5 = !this._domain.IsNullOrEmpty();
					if (flag5)
					{
						bool flag6 = uri == null || uri.Host != this._domain;
						if (flag6)
						{
							stringBuilder.AppendFormat("; $Domain={0}", this._domain);
						}
					}
					bool flag7 = this._port != null;
					if (flag7)
					{
						bool flag8 = this._port != "\"\"";
						if (flag8)
						{
							stringBuilder.AppendFormat("; $Port={0}", this._port);
						}
						else
						{
							stringBuilder.Append("; $Port");
						}
					}
					result = stringBuilder.ToString();
				}
			}
			return result;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000C40C File Offset: 0x0000A60C
		internal string ToResponseString()
		{
			return (this._name.Length == 0) ? string.Empty : ((this._version == 0) ? this.toResponseStringVersion0() : this.toResponseStringVersion1());
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000C448 File Offset: 0x0000A648
		internal static bool TryCreate(string name, string value, out Cookie result)
		{
			result = null;
			try
			{
				result = new Cookie(name, value);
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000C484 File Offset: 0x0000A684
		public override bool Equals(object comparand)
		{
			Cookie cookie = comparand as Cookie;
			bool flag = cookie == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				StringComparison comparisonType = StringComparison.InvariantCulture;
				StringComparison comparisonType2 = StringComparison.InvariantCultureIgnoreCase;
				result = (this._name.Equals(cookie._name, comparisonType2) && this._value.Equals(cookie._value, comparisonType) && this._path.Equals(cookie._path, comparisonType) && this._domain.Equals(cookie._domain, comparisonType2) && this._version == cookie._version);
			}
			return result;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000C514 File Offset: 0x0000A714
		public override int GetHashCode()
		{
			return Cookie.hash(StringComparer.InvariantCultureIgnoreCase.GetHashCode(this._name), this._value.GetHashCode(), this._path.GetHashCode(), StringComparer.InvariantCultureIgnoreCase.GetHashCode(this._domain), this._version);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000C568 File Offset: 0x0000A768
		public override string ToString()
		{
			return this.ToRequestString(null);
		}

		// Token: 0x040000A5 RID: 165
		private string _comment;

		// Token: 0x040000A6 RID: 166
		private Uri _commentUri;

		// Token: 0x040000A7 RID: 167
		private bool _discard;

		// Token: 0x040000A8 RID: 168
		private string _domain;

		// Token: 0x040000A9 RID: 169
		private static readonly int[] _emptyPorts = new int[0];

		// Token: 0x040000AA RID: 170
		private DateTime _expires;

		// Token: 0x040000AB RID: 171
		private bool _httpOnly;

		// Token: 0x040000AC RID: 172
		private string _name;

		// Token: 0x040000AD RID: 173
		private string _path;

		// Token: 0x040000AE RID: 174
		private string _port;

		// Token: 0x040000AF RID: 175
		private int[] _ports;

		// Token: 0x040000B0 RID: 176
		private static readonly char[] _reservedCharsForValue = new char[]
		{
			';',
			','
		};

		// Token: 0x040000B1 RID: 177
		private string _sameSite;

		// Token: 0x040000B2 RID: 178
		private bool _secure;

		// Token: 0x040000B3 RID: 179
		private DateTime _timeStamp;

		// Token: 0x040000B4 RID: 180
		private string _value;

		// Token: 0x040000B5 RID: 181
		private int _version;
	}
}
