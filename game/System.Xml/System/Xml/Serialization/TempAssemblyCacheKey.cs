using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000278 RID: 632
	internal class TempAssemblyCacheKey
	{
		// Token: 0x060017FC RID: 6140 RVA: 0x0008CFE7 File Offset: 0x0008B1E7
		internal TempAssemblyCacheKey(string ns, object type)
		{
			this.type = type;
			this.ns = ns;
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x0008D000 File Offset: 0x0008B200
		public override bool Equals(object o)
		{
			TempAssemblyCacheKey tempAssemblyCacheKey = o as TempAssemblyCacheKey;
			return tempAssemblyCacheKey != null && tempAssemblyCacheKey.type == this.type && tempAssemblyCacheKey.ns == this.ns;
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x0008D03A File Offset: 0x0008B23A
		public override int GetHashCode()
		{
			return ((this.ns != null) ? this.ns.GetHashCode() : 0) ^ ((this.type != null) ? this.type.GetHashCode() : 0);
		}

		// Token: 0x0400189B RID: 6299
		private string ns;

		// Token: 0x0400189C RID: 6300
		private object type;
	}
}
