using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000622 RID: 1570
	internal class CompiledXpathExpr : XPathExpression
	{
		// Token: 0x06004048 RID: 16456 RVA: 0x0016424C File Offset: 0x0016244C
		internal CompiledXpathExpr(Query query, string expression, bool needContext)
		{
			this._query = query;
			this._expr = expression;
			this._needContext = needContext;
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06004049 RID: 16457 RVA: 0x00164269 File Offset: 0x00162469
		internal Query QueryTree
		{
			get
			{
				if (this._needContext)
				{
					throw XPathException.Create("Namespace Manager or XsltContext needed. This query has a prefix, variable, or user-defined function.");
				}
				return this._query;
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x0600404A RID: 16458 RVA: 0x00164284 File Offset: 0x00162484
		public override string Expression
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x0600404B RID: 16459 RVA: 0x0000B528 File Offset: 0x00009728
		public virtual void CheckErrors()
		{
		}

		// Token: 0x0600404C RID: 16460 RVA: 0x0016428C File Offset: 0x0016248C
		public override void AddSort(object expr, IComparer comparer)
		{
			string text = expr as string;
			Query evalQuery;
			if (text != null)
			{
				evalQuery = new QueryBuilder().Build(text, out this._needContext);
			}
			else
			{
				CompiledXpathExpr compiledXpathExpr = expr as CompiledXpathExpr;
				if (compiledXpathExpr == null)
				{
					throw XPathException.Create("This is an invalid object. Only objects returned from Compile() can be passed as input.");
				}
				evalQuery = compiledXpathExpr.QueryTree;
			}
			SortQuery sortQuery = this._query as SortQuery;
			if (sortQuery == null)
			{
				sortQuery = (this._query = new SortQuery(this._query));
			}
			sortQuery.AddSort(evalQuery, comparer);
		}

		// Token: 0x0600404D RID: 16461 RVA: 0x001642FF File Offset: 0x001624FF
		public override void AddSort(object expr, XmlSortOrder order, XmlCaseOrder caseOrder, string lang, XmlDataType dataType)
		{
			this.AddSort(expr, new XPathComparerHelper(order, caseOrder, lang, dataType));
		}

		// Token: 0x0600404E RID: 16462 RVA: 0x00164313 File Offset: 0x00162513
		public override XPathExpression Clone()
		{
			return new CompiledXpathExpr(Query.Clone(this._query), this._expr, this._needContext);
		}

		// Token: 0x0600404F RID: 16463 RVA: 0x00164331 File Offset: 0x00162531
		public override void SetContext(XmlNamespaceManager nsManager)
		{
			this.SetContext(nsManager);
		}

		// Token: 0x06004050 RID: 16464 RVA: 0x0016433C File Offset: 0x0016253C
		public override void SetContext(IXmlNamespaceResolver nsResolver)
		{
			XsltContext xsltContext = nsResolver as XsltContext;
			if (xsltContext == null)
			{
				if (nsResolver == null)
				{
					nsResolver = new XmlNamespaceManager(new NameTable());
				}
				xsltContext = new CompiledXpathExpr.UndefinedXsltContext(nsResolver);
			}
			this._query.SetXsltContext(xsltContext);
			this._needContext = false;
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06004051 RID: 16465 RVA: 0x0016437C File Offset: 0x0016257C
		public override XPathResultType ReturnType
		{
			get
			{
				return this._query.StaticType;
			}
		}

		// Token: 0x04002E03 RID: 11779
		private Query _query;

		// Token: 0x04002E04 RID: 11780
		private string _expr;

		// Token: 0x04002E05 RID: 11781
		private bool _needContext;

		// Token: 0x02000623 RID: 1571
		private class UndefinedXsltContext : XsltContext
		{
			// Token: 0x06004052 RID: 16466 RVA: 0x00164389 File Offset: 0x00162589
			public UndefinedXsltContext(IXmlNamespaceResolver nsResolver) : base(false)
			{
				this._nsResolver = nsResolver;
			}

			// Token: 0x17000C38 RID: 3128
			// (get) Token: 0x06004053 RID: 16467 RVA: 0x0001E51E File Offset: 0x0001C71E
			public override string DefaultNamespace
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x06004054 RID: 16468 RVA: 0x00164399 File Offset: 0x00162599
			public override string LookupNamespace(string prefix)
			{
				if (prefix.Length == 0)
				{
					return string.Empty;
				}
				string text = this._nsResolver.LookupNamespace(prefix);
				if (text == null)
				{
					throw XPathException.Create("Namespace prefix '{0}' is not defined.", prefix);
				}
				return text;
			}

			// Token: 0x06004055 RID: 16469 RVA: 0x001643C4 File Offset: 0x001625C4
			public override IXsltContextVariable ResolveVariable(string prefix, string name)
			{
				throw XPathException.Create("XsltContext is needed for this query because of an unknown function.");
			}

			// Token: 0x06004056 RID: 16470 RVA: 0x001643C4 File Offset: 0x001625C4
			public override IXsltContextFunction ResolveFunction(string prefix, string name, XPathResultType[] ArgTypes)
			{
				throw XPathException.Create("XsltContext is needed for this query because of an unknown function.");
			}

			// Token: 0x17000C39 RID: 3129
			// (get) Token: 0x06004057 RID: 16471 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
			public override bool Whitespace
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06004058 RID: 16472 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
			public override bool PreserveWhitespace(XPathNavigator node)
			{
				return false;
			}

			// Token: 0x06004059 RID: 16473 RVA: 0x001643D0 File Offset: 0x001625D0
			public override int CompareDocument(string baseUri, string nextbaseUri)
			{
				return string.CompareOrdinal(baseUri, nextbaseUri);
			}

			// Token: 0x04002E06 RID: 11782
			private IXmlNamespaceResolver _nsResolver;
		}
	}
}
