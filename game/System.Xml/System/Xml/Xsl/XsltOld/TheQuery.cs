using System;
using MS.Internal.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003B9 RID: 953
	internal sealed class TheQuery
	{
		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x060026C3 RID: 9923 RVA: 0x000E8417 File Offset: 0x000E6617
		internal CompiledXpathExpr CompiledQuery
		{
			get
			{
				return this._CompiledQuery;
			}
		}

		// Token: 0x060026C4 RID: 9924 RVA: 0x000E841F File Offset: 0x000E661F
		internal TheQuery(CompiledXpathExpr compiledQuery, InputScopeManager manager)
		{
			this._CompiledQuery = compiledQuery;
			this._ScopeManager = manager.Clone();
		}

		// Token: 0x04001E6E RID: 7790
		internal InputScopeManager _ScopeManager;

		// Token: 0x04001E6F RID: 7791
		private CompiledXpathExpr _CompiledQuery;
	}
}
