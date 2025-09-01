using System;
using System.IO;

namespace Mono.Security.X509
{
	// Token: 0x0200001C RID: 28
	public class X509Stores
	{
		// Token: 0x0600017A RID: 378 RVA: 0x0000B703 File Offset: 0x00009903
		internal X509Stores(string path, bool newFormat)
		{
			this._storePath = path;
			this._newFormat = newFormat;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000B71C File Offset: 0x0000991C
		public X509Store Personal
		{
			get
			{
				if (this._personal == null)
				{
					string path = Path.Combine(this._storePath, "My");
					this._personal = new X509Store(path, false, false);
				}
				return this._personal;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000B758 File Offset: 0x00009958
		public X509Store OtherPeople
		{
			get
			{
				if (this._other == null)
				{
					string path = Path.Combine(this._storePath, "AddressBook");
					this._other = new X509Store(path, false, false);
				}
				return this._other;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000B794 File Offset: 0x00009994
		public X509Store IntermediateCA
		{
			get
			{
				if (this._intermediate == null)
				{
					string path = Path.Combine(this._storePath, "CA");
					this._intermediate = new X509Store(path, true, this._newFormat);
				}
				return this._intermediate;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000B7D4 File Offset: 0x000099D4
		public X509Store TrustedRoot
		{
			get
			{
				if (this._trusted == null)
				{
					string path = Path.Combine(this._storePath, "Trust");
					this._trusted = new X509Store(path, true, this._newFormat);
				}
				return this._trusted;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000B814 File Offset: 0x00009A14
		public X509Store Untrusted
		{
			get
			{
				if (this._untrusted == null)
				{
					string path = Path.Combine(this._storePath, "Disallowed");
					this._untrusted = new X509Store(path, false, this._newFormat);
				}
				return this._untrusted;
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000B854 File Offset: 0x00009A54
		public void Clear()
		{
			if (this._personal != null)
			{
				this._personal.Clear();
			}
			this._personal = null;
			if (this._other != null)
			{
				this._other.Clear();
			}
			this._other = null;
			if (this._intermediate != null)
			{
				this._intermediate.Clear();
			}
			this._intermediate = null;
			if (this._trusted != null)
			{
				this._trusted.Clear();
			}
			this._trusted = null;
			if (this._untrusted != null)
			{
				this._untrusted.Clear();
			}
			this._untrusted = null;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000B8E4 File Offset: 0x00009AE4
		public X509Store Open(string storeName, bool create)
		{
			if (storeName == null)
			{
				throw new ArgumentNullException("storeName");
			}
			string path = Path.Combine(this._storePath, storeName);
			if (!create && !Directory.Exists(path))
			{
				return null;
			}
			return new X509Store(path, true, false);
		}

		// Token: 0x040000D0 RID: 208
		private string _storePath;

		// Token: 0x040000D1 RID: 209
		private bool _newFormat;

		// Token: 0x040000D2 RID: 210
		private X509Store _personal;

		// Token: 0x040000D3 RID: 211
		private X509Store _other;

		// Token: 0x040000D4 RID: 212
		private X509Store _intermediate;

		// Token: 0x040000D5 RID: 213
		private X509Store _trusted;

		// Token: 0x040000D6 RID: 214
		private X509Store _untrusted;

		// Token: 0x02000088 RID: 136
		public class Names
		{
			// Token: 0x06000539 RID: 1337 RVA: 0x000198D8 File Offset: 0x00017AD8
			public Names()
			{
			}

			// Token: 0x040003D1 RID: 977
			public const string Personal = "My";

			// Token: 0x040003D2 RID: 978
			public const string OtherPeople = "AddressBook";

			// Token: 0x040003D3 RID: 979
			public const string IntermediateCA = "CA";

			// Token: 0x040003D4 RID: 980
			public const string TrustedRoot = "Trust";

			// Token: 0x040003D5 RID: 981
			public const string Untrusted = "Disallowed";
		}
	}
}
