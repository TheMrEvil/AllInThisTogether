using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000651 RID: 1617
	internal sealed class XPathSortComparer : IComparer<SortKey>
	{
		// Token: 0x06004190 RID: 16784 RVA: 0x00167BB1 File Offset: 0x00165DB1
		public XPathSortComparer(int size)
		{
			if (size <= 0)
			{
				size = 3;
			}
			this._expressions = new Query[size];
			this._comparers = new IComparer[size];
		}

		// Token: 0x06004191 RID: 16785 RVA: 0x00167BD8 File Offset: 0x00165DD8
		public XPathSortComparer() : this(3)
		{
		}

		// Token: 0x06004192 RID: 16786 RVA: 0x00167BE4 File Offset: 0x00165DE4
		public void AddSort(Query evalQuery, IComparer comparer)
		{
			if (this._numSorts == this._expressions.Length)
			{
				Query[] array = new Query[this._numSorts * 2];
				IComparer[] array2 = new IComparer[this._numSorts * 2];
				for (int i = 0; i < this._numSorts; i++)
				{
					array[i] = this._expressions[i];
					array2[i] = this._comparers[i];
				}
				this._expressions = array;
				this._comparers = array2;
			}
			if (evalQuery.StaticType == XPathResultType.NodeSet || evalQuery.StaticType == XPathResultType.Any)
			{
				evalQuery = new StringFunctions(Function.FunctionType.FuncString, new Query[]
				{
					evalQuery
				});
			}
			this._expressions[this._numSorts] = evalQuery;
			this._comparers[this._numSorts] = comparer;
			this._numSorts++;
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06004193 RID: 16787 RVA: 0x00167CA0 File Offset: 0x00165EA0
		public int NumSorts
		{
			get
			{
				return this._numSorts;
			}
		}

		// Token: 0x06004194 RID: 16788 RVA: 0x00167CA8 File Offset: 0x00165EA8
		public Query Expression(int i)
		{
			return this._expressions[i];
		}

		// Token: 0x06004195 RID: 16789 RVA: 0x00167CB4 File Offset: 0x00165EB4
		int IComparer<SortKey>.Compare(SortKey x, SortKey y)
		{
			for (int i = 0; i < x.NumKeys; i++)
			{
				int num = this._comparers[i].Compare(x[i], y[i]);
				if (num != 0)
				{
					return num;
				}
			}
			return x.OriginalPosition - y.OriginalPosition;
		}

		// Token: 0x06004196 RID: 16790 RVA: 0x00167D04 File Offset: 0x00165F04
		internal XPathSortComparer Clone()
		{
			XPathSortComparer xpathSortComparer = new XPathSortComparer(this._numSorts);
			for (int i = 0; i < this._numSorts; i++)
			{
				xpathSortComparer._comparers[i] = this._comparers[i];
				xpathSortComparer._expressions[i] = (Query)this._expressions[i].Clone();
			}
			xpathSortComparer._numSorts = this._numSorts;
			return xpathSortComparer;
		}

		// Token: 0x04002E91 RID: 11921
		private const int minSize = 3;

		// Token: 0x04002E92 RID: 11922
		private Query[] _expressions;

		// Token: 0x04002E93 RID: 11923
		private IComparer[] _comparers;

		// Token: 0x04002E94 RID: 11924
		private int _numSorts;
	}
}
