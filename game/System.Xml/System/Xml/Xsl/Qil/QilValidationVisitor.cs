using System;
using System.Diagnostics;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004D4 RID: 1236
	internal class QilValidationVisitor : QilScopedVisitor
	{
		// Token: 0x060032A8 RID: 12968 RVA: 0x001239F3 File Offset: 0x00121BF3
		[Conditional("DEBUG")]
		public static void Validate(QilNode node)
		{
			new QilValidationVisitor().VisitAssumeReference(node);
		}

		// Token: 0x060032A9 RID: 12969 RVA: 0x00123A01 File Offset: 0x00121C01
		protected QilValidationVisitor()
		{
		}

		// Token: 0x060032AA RID: 12970 RVA: 0x00123A20 File Offset: 0x00121C20
		[Conditional("DEBUG")]
		internal static void SetError(QilNode n, string message)
		{
			message = SR.Format("QIL Validation Error! '{0}'.", message);
			string text = n.Annotation as string;
			if (text != null)
			{
				message = text + "\n" + message;
			}
			n.Annotation = message;
		}

		// Token: 0x04002650 RID: 9808
		private SubstitutionList _subs = new SubstitutionList();

		// Token: 0x04002651 RID: 9809
		private QilTypeChecker _typeCheck = new QilTypeChecker();
	}
}
