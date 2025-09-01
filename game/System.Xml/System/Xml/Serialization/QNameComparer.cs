using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x020002A7 RID: 679
	internal class QNameComparer : IComparer
	{
		// Token: 0x06001980 RID: 6528 RVA: 0x00091CA4 File Offset: 0x0008FEA4
		public int Compare(object o1, object o2)
		{
			XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)o1;
			XmlQualifiedName xmlQualifiedName2 = (XmlQualifiedName)o2;
			int num = string.Compare(xmlQualifiedName.Namespace, xmlQualifiedName2.Namespace, StringComparison.Ordinal);
			if (num == 0)
			{
				return string.Compare(xmlQualifiedName.Name, xmlQualifiedName2.Name, StringComparison.Ordinal);
			}
			return num;
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x0000216B File Offset: 0x0000036B
		public QNameComparer()
		{
		}
	}
}
