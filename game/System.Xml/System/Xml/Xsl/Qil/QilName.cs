using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004C4 RID: 1220
	internal class QilName : QilLiteral
	{
		// Token: 0x060030E3 RID: 12515 RVA: 0x00121995 File Offset: 0x0011FB95
		public QilName(QilNodeType nodeType, string local, string uri, string prefix) : base(nodeType, null)
		{
			this.LocalName = local;
			this.NamespaceUri = uri;
			this.Prefix = prefix;
			base.Value = this;
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x060030E4 RID: 12516 RVA: 0x001219BC File Offset: 0x0011FBBC
		// (set) Token: 0x060030E5 RID: 12517 RVA: 0x001219C4 File Offset: 0x0011FBC4
		public string LocalName
		{
			get
			{
				return this._local;
			}
			set
			{
				this._local = value;
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x060030E6 RID: 12518 RVA: 0x001219CD File Offset: 0x0011FBCD
		// (set) Token: 0x060030E7 RID: 12519 RVA: 0x001219D5 File Offset: 0x0011FBD5
		public string NamespaceUri
		{
			get
			{
				return this._uri;
			}
			set
			{
				this._uri = value;
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x060030E8 RID: 12520 RVA: 0x001219DE File Offset: 0x0011FBDE
		// (set) Token: 0x060030E9 RID: 12521 RVA: 0x001219E6 File Offset: 0x0011FBE6
		public string Prefix
		{
			get
			{
				return this._prefix;
			}
			set
			{
				this._prefix = value;
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x060030EA RID: 12522 RVA: 0x001219EF File Offset: 0x0011FBEF
		public string QualifiedName
		{
			get
			{
				if (this._prefix.Length == 0)
				{
					return this._local;
				}
				return this._prefix + ":" + this._local;
			}
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x00121A1B File Offset: 0x0011FC1B
		public override int GetHashCode()
		{
			return this._local.GetHashCode();
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x00121A28 File Offset: 0x0011FC28
		public override bool Equals(object other)
		{
			QilName qilName = other as QilName;
			return !(qilName == null) && this._local == qilName._local && this._uri == qilName._uri;
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x00121A6D File Offset: 0x0011FC6D
		public static bool operator ==(QilName a, QilName b)
		{
			return a == b || (a != null && b != null && a._local == b._local && a._uri == b._uri);
		}

		// Token: 0x060030EE RID: 12526 RVA: 0x00121AA3 File Offset: 0x0011FCA3
		public static bool operator !=(QilName a, QilName b)
		{
			return !(a == b);
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x00121AB0 File Offset: 0x0011FCB0
		public override string ToString()
		{
			if (this._prefix.Length != 0)
			{
				return string.Concat(new string[]
				{
					"{",
					this._uri,
					"}",
					this._prefix,
					":",
					this._local
				});
			}
			if (this._uri.Length == 0)
			{
				return this._local;
			}
			return "{" + this._uri + "}" + this._local;
		}

		// Token: 0x040025CE RID: 9678
		private string _local;

		// Token: 0x040025CF RID: 9679
		private string _uri;

		// Token: 0x040025D0 RID: 9680
		private string _prefix;
	}
}
