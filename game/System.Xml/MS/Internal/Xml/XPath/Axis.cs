using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000618 RID: 1560
	internal class Axis : AstNode
	{
		// Token: 0x06003FF4 RID: 16372 RVA: 0x001636E7 File Offset: 0x001618E7
		public Axis(Axis.AxisType axisType, AstNode input, string prefix, string name, XPathNodeType nodetype)
		{
			this._axisType = axisType;
			this._input = input;
			this._prefix = prefix;
			this._name = name;
			this._nodeType = nodetype;
		}

		// Token: 0x06003FF5 RID: 16373 RVA: 0x0016371F File Offset: 0x0016191F
		public Axis(Axis.AxisType axisType, AstNode input) : this(axisType, input, string.Empty, string.Empty, XPathNodeType.All)
		{
			this.abbrAxis = true;
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06003FF6 RID: 16374 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Axis;
			}
		}

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06003FF7 RID: 16375 RVA: 0x000708A9 File Offset: 0x0006EAA9
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06003FF8 RID: 16376 RVA: 0x0016373C File Offset: 0x0016193C
		// (set) Token: 0x06003FF9 RID: 16377 RVA: 0x00163744 File Offset: 0x00161944
		public AstNode Input
		{
			get
			{
				return this._input;
			}
			set
			{
				this._input = value;
			}
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06003FFA RID: 16378 RVA: 0x0016374D File Offset: 0x0016194D
		public string Prefix
		{
			get
			{
				return this._prefix;
			}
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06003FFB RID: 16379 RVA: 0x00163755 File Offset: 0x00161955
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06003FFC RID: 16380 RVA: 0x0016375D File Offset: 0x0016195D
		public XPathNodeType NodeType
		{
			get
			{
				return this._nodeType;
			}
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x06003FFD RID: 16381 RVA: 0x00163765 File Offset: 0x00161965
		public Axis.AxisType TypeOfAxis
		{
			get
			{
				return this._axisType;
			}
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x06003FFE RID: 16382 RVA: 0x0016376D File Offset: 0x0016196D
		public bool AbbrAxis
		{
			get
			{
				return this.abbrAxis;
			}
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x06003FFF RID: 16383 RVA: 0x00163775 File Offset: 0x00161975
		// (set) Token: 0x06004000 RID: 16384 RVA: 0x0016377D File Offset: 0x0016197D
		public string Urn
		{
			get
			{
				return this._urn;
			}
			set
			{
				this._urn = value;
			}
		}

		// Token: 0x04002DD8 RID: 11736
		private Axis.AxisType _axisType;

		// Token: 0x04002DD9 RID: 11737
		private AstNode _input;

		// Token: 0x04002DDA RID: 11738
		private string _prefix;

		// Token: 0x04002DDB RID: 11739
		private string _name;

		// Token: 0x04002DDC RID: 11740
		private XPathNodeType _nodeType;

		// Token: 0x04002DDD RID: 11741
		protected bool abbrAxis;

		// Token: 0x04002DDE RID: 11742
		private string _urn = string.Empty;

		// Token: 0x02000619 RID: 1561
		public enum AxisType
		{
			// Token: 0x04002DE0 RID: 11744
			Ancestor,
			// Token: 0x04002DE1 RID: 11745
			AncestorOrSelf,
			// Token: 0x04002DE2 RID: 11746
			Attribute,
			// Token: 0x04002DE3 RID: 11747
			Child,
			// Token: 0x04002DE4 RID: 11748
			Descendant,
			// Token: 0x04002DE5 RID: 11749
			DescendantOrSelf,
			// Token: 0x04002DE6 RID: 11750
			Following,
			// Token: 0x04002DE7 RID: 11751
			FollowingSibling,
			// Token: 0x04002DE8 RID: 11752
			Namespace,
			// Token: 0x04002DE9 RID: 11753
			Parent,
			// Token: 0x04002DEA RID: 11754
			Preceding,
			// Token: 0x04002DEB RID: 11755
			PrecedingSibling,
			// Token: 0x04002DEC RID: 11756
			Self,
			// Token: 0x04002DED RID: 11757
			None
		}
	}
}
