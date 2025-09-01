using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200064F RID: 1615
	internal sealed class SortQuery : Query
	{
		// Token: 0x0600417C RID: 16764 RVA: 0x0016796C File Offset: 0x00165B6C
		public SortQuery(Query qyInput)
		{
			this._results = new List<SortKey>();
			this._comparer = new XPathSortComparer();
			this._qyInput = qyInput;
			this.count = 0;
		}

		// Token: 0x0600417D RID: 16765 RVA: 0x00167998 File Offset: 0x00165B98
		private SortQuery(SortQuery other) : base(other)
		{
			this._results = new List<SortKey>(other._results);
			this._comparer = other._comparer.Clone();
			this._qyInput = Query.Clone(other._qyInput);
			this.count = 0;
		}

		// Token: 0x0600417E RID: 16766 RVA: 0x00163CE4 File Offset: 0x00161EE4
		public override void Reset()
		{
			this.count = 0;
		}

		// Token: 0x0600417F RID: 16767 RVA: 0x001679E6 File Offset: 0x00165BE6
		public override void SetXsltContext(XsltContext xsltContext)
		{
			this._qyInput.SetXsltContext(xsltContext);
			if (this._qyInput.StaticType != XPathResultType.NodeSet && this._qyInput.StaticType != XPathResultType.Any)
			{
				throw XPathException.Create("Expression must evaluate to a node-set.");
			}
		}

		// Token: 0x06004180 RID: 16768 RVA: 0x00167A1C File Offset: 0x00165C1C
		private void BuildResultsList()
		{
			int numSorts = this._comparer.NumSorts;
			XPathNavigator xpathNavigator;
			while ((xpathNavigator = this._qyInput.Advance()) != null)
			{
				SortKey sortKey = new SortKey(numSorts, this._results.Count, xpathNavigator.Clone());
				for (int i = 0; i < numSorts; i++)
				{
					sortKey[i] = this._comparer.Expression(i).Evaluate(this._qyInput);
				}
				this._results.Add(sortKey);
			}
			this._results.Sort(this._comparer);
		}

		// Token: 0x06004181 RID: 16769 RVA: 0x00167AA5 File Offset: 0x00165CA5
		public override object Evaluate(XPathNodeIterator context)
		{
			this._qyInput.Evaluate(context);
			this._results.Clear();
			this.BuildResultsList();
			this.count = 0;
			return this;
		}

		// Token: 0x06004182 RID: 16770 RVA: 0x00167AD0 File Offset: 0x00165CD0
		public override XPathNavigator Advance()
		{
			if (this.count < this._results.Count)
			{
				List<SortKey> results = this._results;
				int count = this.count;
				this.count = count + 1;
				return results[count].Node;
			}
			return null;
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06004183 RID: 16771 RVA: 0x00167B13 File Offset: 0x00165D13
		public override XPathNavigator Current
		{
			get
			{
				if (this.count == 0)
				{
					return null;
				}
				return this._results[this.count - 1].Node;
			}
		}

		// Token: 0x06004184 RID: 16772 RVA: 0x00167B37 File Offset: 0x00165D37
		internal void AddSort(Query evalQuery, IComparer comparer)
		{
			this._comparer.AddSort(evalQuery, comparer);
		}

		// Token: 0x06004185 RID: 16773 RVA: 0x00167B46 File Offset: 0x00165D46
		public override XPathNodeIterator Clone()
		{
			return new SortQuery(this);
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06004186 RID: 16774 RVA: 0x000708A9 File Offset: 0x0006EAA9
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06004187 RID: 16775 RVA: 0x00163D61 File Offset: 0x00161F61
		public override int CurrentPosition
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06004188 RID: 16776 RVA: 0x00167B4E File Offset: 0x00165D4E
		public override int Count
		{
			get
			{
				return this._results.Count;
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06004189 RID: 16777 RVA: 0x0007076D File Offset: 0x0006E96D
		public override QueryProps Properties
		{
			get
			{
				return (QueryProps)7;
			}
		}

		// Token: 0x04002E8A RID: 11914
		private List<SortKey> _results;

		// Token: 0x04002E8B RID: 11915
		private XPathSortComparer _comparer;

		// Token: 0x04002E8C RID: 11916
		private Query _qyInput;
	}
}
