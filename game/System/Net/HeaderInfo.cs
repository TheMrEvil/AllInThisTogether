using System;

namespace System.Net
{
	// Token: 0x02000628 RID: 1576
	internal class HeaderInfo
	{
		// Token: 0x060031D1 RID: 12753 RVA: 0x000ACBB9 File Offset: 0x000AADB9
		internal HeaderInfo(string name, bool requestRestricted, bool responseRestricted, bool multi, HeaderParser p)
		{
			this.HeaderName = name;
			this.IsRequestRestricted = requestRestricted;
			this.IsResponseRestricted = responseRestricted;
			this.Parser = p;
			this.AllowMultiValues = multi;
		}

		// Token: 0x04001D21 RID: 7457
		internal readonly bool IsRequestRestricted;

		// Token: 0x04001D22 RID: 7458
		internal readonly bool IsResponseRestricted;

		// Token: 0x04001D23 RID: 7459
		internal readonly HeaderParser Parser;

		// Token: 0x04001D24 RID: 7460
		internal readonly string HeaderName;

		// Token: 0x04001D25 RID: 7461
		internal readonly bool AllowMultiValues;
	}
}
