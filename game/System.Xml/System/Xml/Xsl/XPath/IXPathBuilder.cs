using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace System.Xml.Xsl.XPath
{
	// Token: 0x02000422 RID: 1058
	internal interface IXPathBuilder<Node>
	{
		// Token: 0x06002A27 RID: 10791
		void StartBuild();

		// Token: 0x06002A28 RID: 10792
		Node EndBuild(Node result);

		// Token: 0x06002A29 RID: 10793
		Node String(string value);

		// Token: 0x06002A2A RID: 10794
		Node Number(double value);

		// Token: 0x06002A2B RID: 10795
		Node Operator(XPathOperator op, Node left, Node right);

		// Token: 0x06002A2C RID: 10796
		Node Axis(XPathAxis xpathAxis, XPathNodeType nodeType, string prefix, string name);

		// Token: 0x06002A2D RID: 10797
		Node JoinStep(Node left, Node right);

		// Token: 0x06002A2E RID: 10798
		Node Predicate(Node node, Node condition, bool reverseStep);

		// Token: 0x06002A2F RID: 10799
		Node Variable(string prefix, string name);

		// Token: 0x06002A30 RID: 10800
		Node Function(string prefix, string name, IList<Node> args);
	}
}
