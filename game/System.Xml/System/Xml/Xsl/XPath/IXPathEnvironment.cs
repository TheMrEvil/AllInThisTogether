using System;
using System.Collections.Generic;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.XPath
{
	// Token: 0x02000421 RID: 1057
	internal interface IXPathEnvironment : IFocus
	{
		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06002A23 RID: 10787
		XPathQilFactory Factory { get; }

		// Token: 0x06002A24 RID: 10788
		QilNode ResolveVariable(string prefix, string name);

		// Token: 0x06002A25 RID: 10789
		QilNode ResolveFunction(string prefix, string name, IList<QilNode> args, IFocus env);

		// Token: 0x06002A26 RID: 10790
		string ResolvePrefix(string prefix);
	}
}
