using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200029F RID: 671
	internal class NameKey
	{
		// Token: 0x0600192B RID: 6443 RVA: 0x00090768 File Offset: 0x0008E968
		internal NameKey(string name, string ns)
		{
			this.name = name;
			this.ns = ns;
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x00090780 File Offset: 0x0008E980
		public override bool Equals(object other)
		{
			if (!(other is NameKey))
			{
				return false;
			}
			NameKey nameKey = (NameKey)other;
			return this.name == nameKey.name && this.ns == nameKey.ns;
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x000907C4 File Offset: 0x0008E9C4
		public override int GetHashCode()
		{
			return ((this.ns == null) ? "<null>".GetHashCode() : this.ns.GetHashCode()) ^ ((this.name == null) ? 0 : this.name.GetHashCode());
		}

		// Token: 0x0400191A RID: 6426
		private string ns;

		// Token: 0x0400191B RID: 6427
		private string name;
	}
}
