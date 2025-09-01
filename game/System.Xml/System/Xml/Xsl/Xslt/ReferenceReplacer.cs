using System;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020003EF RID: 1007
	internal class ReferenceReplacer : QilReplaceVisitor
	{
		// Token: 0x060027D6 RID: 10198 RVA: 0x000ED041 File Offset: 0x000EB241
		public ReferenceReplacer(QilFactory f) : base(f)
		{
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x000ED04A File Offset: 0x000EB24A
		public QilNode Replace(QilNode expr, QilReference lookFor, QilReference replaceBy)
		{
			QilDepthChecker.Check(expr);
			this.lookFor = lookFor;
			this.replaceBy = replaceBy;
			return this.VisitAssumeReference(expr);
		}

		// Token: 0x060027D8 RID: 10200 RVA: 0x000ED067 File Offset: 0x000EB267
		protected override QilNode VisitReference(QilNode n)
		{
			if (n != this.lookFor)
			{
				return n;
			}
			return this.replaceBy;
		}

		// Token: 0x04001FAE RID: 8110
		private QilReference lookFor;

		// Token: 0x04001FAF RID: 8111
		private QilReference replaceBy;
	}
}
