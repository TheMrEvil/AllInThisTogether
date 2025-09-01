using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004CC RID: 1228
	internal abstract class QilReplaceVisitor : QilVisitor
	{
		// Token: 0x06003208 RID: 12808 RVA: 0x001227F6 File Offset: 0x001209F6
		public QilReplaceVisitor(QilFactory f)
		{
			this.f = f;
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x00122808 File Offset: 0x00120A08
		protected override QilNode VisitChildren(QilNode parent)
		{
			XmlQueryType xmlType = parent.XmlType;
			bool flag = false;
			for (int i = 0; i < parent.Count; i++)
			{
				QilNode qilNode = parent[i];
				XmlQueryType xmlQueryType = (qilNode != null) ? qilNode.XmlType : null;
				QilNode qilNode2;
				if (this.IsReference(parent, i))
				{
					qilNode2 = this.VisitReference(qilNode);
				}
				else
				{
					qilNode2 = this.Visit(qilNode);
				}
				if (qilNode != qilNode2 || (qilNode2 != null && xmlQueryType != qilNode2.XmlType))
				{
					flag = true;
					parent[i] = qilNode2;
				}
			}
			if (flag)
			{
				this.RecalculateType(parent, xmlType);
			}
			return parent;
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x00122890 File Offset: 0x00120A90
		protected virtual void RecalculateType(QilNode node, XmlQueryType oldType)
		{
			XmlQueryType xmlType = this.f.TypeChecker.Check(node);
			node.XmlType = xmlType;
		}

		// Token: 0x0400264B RID: 9803
		protected QilFactory f;
	}
}
