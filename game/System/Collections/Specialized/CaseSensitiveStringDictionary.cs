using System;

namespace System.Collections.Specialized
{
	// Token: 0x020004B8 RID: 1208
	internal class CaseSensitiveStringDictionary : StringDictionary
	{
		// Token: 0x06002716 RID: 10006 RVA: 0x00087B3E File Offset: 0x00085D3E
		public CaseSensitiveStringDictionary()
		{
		}

		// Token: 0x170007F5 RID: 2037
		public override string this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				return (string)this.contents[key];
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				this.contents[key] = value;
			}
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x00087B84 File Offset: 0x00085D84
		public override void Add(string key, string value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Add(key, value);
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x00087BA1 File Offset: 0x00085DA1
		public override bool ContainsKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.contents.ContainsKey(key);
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x00087BBD File Offset: 0x00085DBD
		public override void Remove(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Remove(key);
		}
	}
}
