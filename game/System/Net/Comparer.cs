using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x0200064E RID: 1614
	internal class Comparer : IComparer
	{
		// Token: 0x060032D6 RID: 13014 RVA: 0x000B0AB0 File Offset: 0x000AECB0
		int IComparer.Compare(object ol, object or)
		{
			Cookie cookie = (Cookie)ol;
			Cookie cookie2 = (Cookie)or;
			int result;
			if ((result = string.Compare(cookie.Name, cookie2.Name, StringComparison.OrdinalIgnoreCase)) != 0)
			{
				return result;
			}
			if ((result = string.Compare(cookie.Domain, cookie2.Domain, StringComparison.OrdinalIgnoreCase)) != 0)
			{
				return result;
			}
			if ((result = string.Compare(cookie.Path, cookie2.Path, StringComparison.Ordinal)) != 0)
			{
				return result;
			}
			return 0;
		}

		// Token: 0x060032D7 RID: 13015 RVA: 0x0000219B File Offset: 0x0000039B
		public Comparer()
		{
		}
	}
}
