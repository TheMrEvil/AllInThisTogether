using System;
using System.Data;

namespace System.Xml
{
	// Token: 0x02000078 RID: 120
	internal interface IXmlDataVirtualNode
	{
		// Token: 0x06000531 RID: 1329
		bool IsOnNode(XmlNode nodeToCheck);

		// Token: 0x06000532 RID: 1330
		bool IsOnColumn(DataColumn col);

		// Token: 0x06000533 RID: 1331
		bool IsInUse();

		// Token: 0x06000534 RID: 1332
		void OnFoliated(XmlNode foliatedNode);
	}
}
