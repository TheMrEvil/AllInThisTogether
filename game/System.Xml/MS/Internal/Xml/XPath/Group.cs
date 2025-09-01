using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000634 RID: 1588
	internal class Group : AstNode
	{
		// Token: 0x060040C1 RID: 16577 RVA: 0x0016561F File Offset: 0x0016381F
		public Group(AstNode groupNode)
		{
			this._groupNode = groupNode;
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x060040C2 RID: 16578 RVA: 0x0006AB76 File Offset: 0x00068D76
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Group;
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x060040C3 RID: 16579 RVA: 0x000708A9 File Offset: 0x0006EAA9
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x060040C4 RID: 16580 RVA: 0x0016562E File Offset: 0x0016382E
		public AstNode GroupNode
		{
			get
			{
				return this._groupNode;
			}
		}

		// Token: 0x04002E41 RID: 11841
		private AstNode _groupNode;
	}
}
