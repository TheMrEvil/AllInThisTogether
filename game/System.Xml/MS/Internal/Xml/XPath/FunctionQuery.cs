using System;
using System.Collections.Generic;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000633 RID: 1587
	internal sealed class FunctionQuery : ExtensionQuery
	{
		// Token: 0x060040BA RID: 16570 RVA: 0x0016536E File Offset: 0x0016356E
		public FunctionQuery(string prefix, string name, List<Query> args) : base(prefix, name)
		{
			this._args = args;
		}

		// Token: 0x060040BB RID: 16571 RVA: 0x00165380 File Offset: 0x00163580
		private FunctionQuery(FunctionQuery other) : base(other)
		{
			this._function = other._function;
			Query[] array = new Query[other._args.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Query.Clone(other._args[i]);
			}
			this._args = array;
			this._args = array;
		}

		// Token: 0x060040BC RID: 16572 RVA: 0x001653E4 File Offset: 0x001635E4
		public override void SetXsltContext(XsltContext context)
		{
			if (context == null)
			{
				throw XPathException.Create("Namespace Manager or XsltContext needed. This query has a prefix, variable, or user-defined function.");
			}
			if (this.xsltContext != context)
			{
				this.xsltContext = context;
				foreach (Query query in this._args)
				{
					query.SetXsltContext(context);
				}
				XPathResultType[] array = new XPathResultType[this._args.Count];
				for (int i = 0; i < this._args.Count; i++)
				{
					array[i] = this._args[i].StaticType;
				}
				this._function = this.xsltContext.ResolveFunction(this.prefix, this.name, array);
				if (this._function == null)
				{
					throw XPathException.Create("The function '{0}()' is undefined.", base.QName);
				}
			}
		}

		// Token: 0x060040BD RID: 16573 RVA: 0x001654C4 File Offset: 0x001636C4
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			if (this.xsltContext == null)
			{
				throw XPathException.Create("Namespace Manager or XsltContext needed. This query has a prefix, variable, or user-defined function.");
			}
			object[] array = new object[this._args.Count];
			for (int i = 0; i < this._args.Count; i++)
			{
				array[i] = this._args[i].Evaluate(nodeIterator);
				if (array[i] is XPathNodeIterator)
				{
					array[i] = new XPathSelectionIterator(nodeIterator.Current, this._args[i]);
				}
			}
			object result;
			try
			{
				result = base.ProcessResult(this._function.Invoke(this.xsltContext, array, nodeIterator.Current));
			}
			catch (Exception innerException)
			{
				throw XPathException.Create("Function '{0}()' has failed.", base.QName, innerException);
			}
			return result;
		}

		// Token: 0x060040BE RID: 16574 RVA: 0x0016558C File Offset: 0x0016378C
		public override XPathNavigator MatchNode(XPathNavigator navigator)
		{
			if (this.name != "key" && this.prefix.Length != 0)
			{
				throw XPathException.Create("'{0}' is an invalid XSLT pattern.");
			}
			this.Evaluate(new XPathSingletonIterator(navigator, true));
			XPathNavigator xpathNavigator;
			while ((xpathNavigator = this.Advance()) != null)
			{
				if (xpathNavigator.IsSamePosition(navigator))
				{
					return xpathNavigator;
				}
			}
			return xpathNavigator;
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x060040BF RID: 16575 RVA: 0x001655EC File Offset: 0x001637EC
		public override XPathResultType StaticType
		{
			get
			{
				XPathResultType xpathResultType = (this._function != null) ? this._function.ReturnType : XPathResultType.Any;
				if (xpathResultType == XPathResultType.Error)
				{
					xpathResultType = XPathResultType.Any;
				}
				return xpathResultType;
			}
		}

		// Token: 0x060040C0 RID: 16576 RVA: 0x00165617 File Offset: 0x00163817
		public override XPathNodeIterator Clone()
		{
			return new FunctionQuery(this);
		}

		// Token: 0x04002E3F RID: 11839
		private IList<Query> _args;

		// Token: 0x04002E40 RID: 11840
		private IXsltContextFunction _function;
	}
}
