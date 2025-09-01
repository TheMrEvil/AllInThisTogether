using System;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.XPath
{
	// Token: 0x02000420 RID: 1056
	internal interface IFocus
	{
		// Token: 0x06002A20 RID: 10784
		QilNode GetCurrent();

		// Token: 0x06002A21 RID: 10785
		QilNode GetPosition();

		// Token: 0x06002A22 RID: 10786
		QilNode GetLast();
	}
}
