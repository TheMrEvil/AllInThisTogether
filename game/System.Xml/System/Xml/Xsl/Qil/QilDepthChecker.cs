using System;
using System.Collections.Generic;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004D6 RID: 1238
	internal class QilDepthChecker
	{
		// Token: 0x06003321 RID: 13089 RVA: 0x001242D2 File Offset: 0x001224D2
		public static void Check(QilNode input)
		{
			if (LocalAppContextSwitches.LimitXPathComplexity)
			{
				new QilDepthChecker().Check(input, 0);
			}
		}

		// Token: 0x06003322 RID: 13090 RVA: 0x001242E8 File Offset: 0x001224E8
		private void Check(QilNode input, int depth)
		{
			if (depth > 800)
			{
				throw XsltException.Create("The stylesheet is too complex.", Array.Empty<string>());
			}
			if (input is QilReference)
			{
				if (this._visitedRef.ContainsKey(input))
				{
					return;
				}
				this._visitedRef[input] = true;
			}
			int depth2 = depth + 1;
			for (int i = 0; i < input.Count; i++)
			{
				QilNode qilNode = input[i];
				if (qilNode != null)
				{
					this.Check(qilNode, depth2);
				}
			}
		}

		// Token: 0x06003323 RID: 13091 RVA: 0x00124359 File Offset: 0x00122559
		public QilDepthChecker()
		{
		}

		// Token: 0x04002652 RID: 9810
		private const int MAX_QIL_DEPTH = 800;

		// Token: 0x04002653 RID: 9811
		private Dictionary<QilNode, bool> _visitedRef = new Dictionary<QilNode, bool>();
	}
}
