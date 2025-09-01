using System;

namespace System.Net
{
	// Token: 0x02000674 RID: 1652
	internal class DigestHeaderParser
	{
		// Token: 0x060033F4 RID: 13300 RVA: 0x000B5198 File Offset: 0x000B3398
		public DigestHeaderParser(string header)
		{
			this.header = header.Trim();
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x060033F5 RID: 13301 RVA: 0x000B51BE File Offset: 0x000B33BE
		public string Realm
		{
			get
			{
				return this.values[0];
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x060033F6 RID: 13302 RVA: 0x000B51C8 File Offset: 0x000B33C8
		public string Opaque
		{
			get
			{
				return this.values[1];
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x060033F7 RID: 13303 RVA: 0x000B51D2 File Offset: 0x000B33D2
		public string Nonce
		{
			get
			{
				return this.values[2];
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x060033F8 RID: 13304 RVA: 0x000B51DC File Offset: 0x000B33DC
		public string Algorithm
		{
			get
			{
				return this.values[3];
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x060033F9 RID: 13305 RVA: 0x000B51E6 File Offset: 0x000B33E6
		public string QOP
		{
			get
			{
				return this.values[4];
			}
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x000B51F0 File Offset: 0x000B33F0
		public bool Parse()
		{
			if (!this.header.ToLower().StartsWith("digest "))
			{
				return false;
			}
			this.pos = 6;
			this.length = this.header.Length;
			while (this.pos < this.length)
			{
				string value;
				string text;
				if (!this.GetKeywordAndValue(out value, out text))
				{
					return false;
				}
				this.SkipWhitespace();
				if (this.pos < this.length && this.header[this.pos] == ',')
				{
					this.pos++;
				}
				int num = Array.IndexOf<string>(DigestHeaderParser.keywords, value);
				if (num != -1)
				{
					if (this.values[num] != null)
					{
						return false;
					}
					this.values[num] = text;
				}
			}
			return this.Realm != null && this.Nonce != null;
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x000B52BC File Offset: 0x000B34BC
		private void SkipWhitespace()
		{
			char c = ' ';
			while (this.pos < this.length && (c == ' ' || c == '\t' || c == '\r' || c == '\n'))
			{
				string text = this.header;
				int num = this.pos;
				this.pos = num + 1;
				c = text[num];
			}
			this.pos--;
		}

		// Token: 0x060033FC RID: 13308 RVA: 0x000B531C File Offset: 0x000B351C
		private string GetKey()
		{
			this.SkipWhitespace();
			int num = this.pos;
			while (this.pos < this.length && this.header[this.pos] != '=')
			{
				this.pos++;
			}
			return this.header.Substring(num, this.pos - num).Trim().ToLower();
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x000B5388 File Offset: 0x000B3588
		private bool GetKeywordAndValue(out string key, out string value)
		{
			key = null;
			value = null;
			key = this.GetKey();
			if (this.pos >= this.length)
			{
				return false;
			}
			this.SkipWhitespace();
			if (this.pos + 1 < this.length)
			{
				string text = this.header;
				int num = this.pos;
				this.pos = num + 1;
				if (text[num] == '=')
				{
					this.SkipWhitespace();
					if (this.pos + 1 >= this.length)
					{
						return false;
					}
					bool flag = false;
					if (this.header[this.pos] == '"')
					{
						this.pos++;
						flag = true;
					}
					int num2 = this.pos;
					if (flag)
					{
						this.pos = this.header.IndexOf('"', this.pos);
						if (this.pos == -1)
						{
							return false;
						}
					}
					else
					{
						do
						{
							char c = this.header[this.pos];
							if (c == ',' || c == ' ' || c == '\t' || c == '\r' || c == '\n')
							{
								break;
							}
							num = this.pos + 1;
							this.pos = num;
						}
						while (num < this.length);
						if (this.pos >= this.length && num2 == this.pos)
						{
							return false;
						}
					}
					value = this.header.Substring(num2, this.pos - num2);
					this.pos += (flag ? 2 : 1);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060033FE RID: 13310 RVA: 0x000B54E0 File Offset: 0x000B36E0
		// Note: this type is marked as 'beforefieldinit'.
		static DigestHeaderParser()
		{
		}

		// Token: 0x04001E77 RID: 7799
		private string header;

		// Token: 0x04001E78 RID: 7800
		private int length;

		// Token: 0x04001E79 RID: 7801
		private int pos;

		// Token: 0x04001E7A RID: 7802
		private static string[] keywords = new string[]
		{
			"realm",
			"opaque",
			"nonce",
			"algorithm",
			"qop"
		};

		// Token: 0x04001E7B RID: 7803
		private string[] values = new string[DigestHeaderParser.keywords.Length];
	}
}
