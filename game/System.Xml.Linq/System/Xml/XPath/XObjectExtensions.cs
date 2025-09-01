using System;
using System.Xml.Linq;

namespace System.Xml.XPath
{
	// Token: 0x02000009 RID: 9
	internal static class XObjectExtensions
	{
		// Token: 0x06000055 RID: 85 RVA: 0x000034D0 File Offset: 0x000016D0
		public static XContainer GetParent(this XObject obj)
		{
			XContainer xcontainer = obj.Parent;
			if (xcontainer == null)
			{
				xcontainer = obj.Document;
			}
			if (xcontainer == obj)
			{
				return null;
			}
			return xcontainer;
		}
	}
}
