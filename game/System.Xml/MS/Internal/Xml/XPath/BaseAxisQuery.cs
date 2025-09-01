using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200061A RID: 1562
	internal abstract class BaseAxisQuery : Query
	{
		// Token: 0x06004001 RID: 16385 RVA: 0x00163786 File Offset: 0x00161986
		protected BaseAxisQuery(Query qyInput)
		{
			this._name = string.Empty;
			this._prefix = string.Empty;
			this._nsUri = string.Empty;
			this.qyInput = qyInput;
		}

		// Token: 0x06004002 RID: 16386 RVA: 0x001637B8 File Offset: 0x001619B8
		protected BaseAxisQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest)
		{
			this.qyInput = qyInput;
			this._name = name;
			this._prefix = prefix;
			this._typeTest = typeTest;
			this._nameTest = (prefix.Length != 0 || name.Length != 0);
			this._nsUri = string.Empty;
		}

		// Token: 0x06004003 RID: 16387 RVA: 0x00163810 File Offset: 0x00161A10
		protected BaseAxisQuery(BaseAxisQuery other) : base(other)
		{
			this.qyInput = Query.Clone(other.qyInput);
			this._name = other._name;
			this._prefix = other._prefix;
			this._nsUri = other._nsUri;
			this._typeTest = other._typeTest;
			this._nameTest = other._nameTest;
			this.position = other.position;
			this.currentNode = other.currentNode;
		}

		// Token: 0x06004004 RID: 16388 RVA: 0x00163889 File Offset: 0x00161A89
		public override void Reset()
		{
			this.position = 0;
			this.currentNode = null;
			this.qyInput.Reset();
		}

		// Token: 0x06004005 RID: 16389 RVA: 0x001638A4 File Offset: 0x00161AA4
		public override void SetXsltContext(XsltContext context)
		{
			this._nsUri = context.LookupNamespace(this._prefix);
			this.qyInput.SetXsltContext(context);
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x06004006 RID: 16390 RVA: 0x001638C4 File Offset: 0x00161AC4
		protected string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06004007 RID: 16391 RVA: 0x001638CC File Offset: 0x00161ACC
		protected string Namespace
		{
			get
			{
				return this._nsUri;
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06004008 RID: 16392 RVA: 0x001638D4 File Offset: 0x00161AD4
		protected bool NameTest
		{
			get
			{
				return this._nameTest;
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06004009 RID: 16393 RVA: 0x001638DC File Offset: 0x00161ADC
		protected XPathNodeType TypeTest
		{
			get
			{
				return this._typeTest;
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x0600400A RID: 16394 RVA: 0x001638E4 File Offset: 0x00161AE4
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x0600400B RID: 16395 RVA: 0x001638EC File Offset: 0x00161AEC
		public override XPathNavigator Current
		{
			get
			{
				return this.currentNode;
			}
		}

		// Token: 0x0600400C RID: 16396 RVA: 0x001638F4 File Offset: 0x00161AF4
		public virtual bool matches(XPathNavigator e)
		{
			if (this.TypeTest == e.NodeType || this.TypeTest == XPathNodeType.All || (this.TypeTest == XPathNodeType.Text && (e.NodeType == XPathNodeType.Whitespace || e.NodeType == XPathNodeType.SignificantWhitespace)))
			{
				if (!this.NameTest)
				{
					return true;
				}
				if ((this._name.Equals(e.LocalName) || this._name.Length == 0) && this._nsUri.Equals(e.NamespaceURI))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600400D RID: 16397 RVA: 0x00163974 File Offset: 0x00161B74
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			base.ResetCount();
			this.Reset();
			this.qyInput.Evaluate(nodeIterator);
			return this;
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x0600400E RID: 16398 RVA: 0x00163990 File Offset: 0x00161B90
		public override double XsltDefaultPriority
		{
			get
			{
				if (this.qyInput.GetType() != typeof(ContextQuery))
				{
					return 0.5;
				}
				if (this._name.Length != 0)
				{
					return 0.0;
				}
				if (this._prefix.Length != 0)
				{
					return -0.25;
				}
				return -0.5;
			}
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x0600400F RID: 16399 RVA: 0x000708A9 File Offset: 0x0006EAA9
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x04002DEE RID: 11758
		internal Query qyInput;

		// Token: 0x04002DEF RID: 11759
		private bool _nameTest;

		// Token: 0x04002DF0 RID: 11760
		private string _name;

		// Token: 0x04002DF1 RID: 11761
		private string _prefix;

		// Token: 0x04002DF2 RID: 11762
		private string _nsUri;

		// Token: 0x04002DF3 RID: 11763
		private XPathNodeType _typeTest;

		// Token: 0x04002DF4 RID: 11764
		protected XPathNavigator currentNode;

		// Token: 0x04002DF5 RID: 11765
		protected int position;
	}
}
