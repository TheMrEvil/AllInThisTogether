using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000656 RID: 1622
	internal sealed class VariableQuery : ExtensionQuery
	{
		// Token: 0x060041C4 RID: 16836 RVA: 0x0016870B File Offset: 0x0016690B
		public VariableQuery(string name, string prefix) : base(prefix, name)
		{
		}

		// Token: 0x060041C5 RID: 16837 RVA: 0x00168715 File Offset: 0x00166915
		private VariableQuery(VariableQuery other) : base(other)
		{
			this._variable = other._variable;
		}

		// Token: 0x060041C6 RID: 16838 RVA: 0x0016872C File Offset: 0x0016692C
		public override void SetXsltContext(XsltContext context)
		{
			if (context == null)
			{
				throw XPathException.Create("Namespace Manager or XsltContext needed. This query has a prefix, variable, or user-defined function.");
			}
			if (this.xsltContext != context)
			{
				this.xsltContext = context;
				this._variable = this.xsltContext.ResolveVariable(this.prefix, this.name);
				if (this._variable == null)
				{
					throw XPathException.Create("The variable '{0}' is undefined.", base.QName);
				}
			}
		}

		// Token: 0x060041C7 RID: 16839 RVA: 0x0016878D File Offset: 0x0016698D
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			if (this.xsltContext == null)
			{
				throw XPathException.Create("Namespace Manager or XsltContext needed. This query has a prefix, variable, or user-defined function.");
			}
			return base.ProcessResult(this._variable.Evaluate(this.xsltContext));
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x060041C8 RID: 16840 RVA: 0x001687BC File Offset: 0x001669BC
		public override XPathResultType StaticType
		{
			get
			{
				if (this._variable != null)
				{
					return base.GetXPathType(this.Evaluate(null));
				}
				XPathResultType xpathResultType = (this._variable != null) ? this._variable.VariableType : XPathResultType.Any;
				if (xpathResultType == XPathResultType.Error)
				{
					xpathResultType = XPathResultType.Any;
				}
				return xpathResultType;
			}
		}

		// Token: 0x060041C9 RID: 16841 RVA: 0x001687FD File Offset: 0x001669FD
		public override XPathNodeIterator Clone()
		{
			return new VariableQuery(this);
		}

		// Token: 0x04002EA0 RID: 11936
		private IXsltContextVariable _variable;
	}
}
