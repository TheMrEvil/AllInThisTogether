using System;
using System.Collections.Generic;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020003E8 RID: 1000
	internal class TemplateMatch
	{
		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x060027B3 RID: 10163 RVA: 0x000EC3DF File Offset: 0x000EA5DF
		public XmlNodeKindFlags NodeKind
		{
			get
			{
				return this.nodeKind;
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x060027B4 RID: 10164 RVA: 0x000EC3E7 File Offset: 0x000EA5E7
		public QilName QName
		{
			get
			{
				return this.qname;
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x060027B5 RID: 10165 RVA: 0x000EC3EF File Offset: 0x000EA5EF
		public QilIterator Iterator
		{
			get
			{
				return this.iterator;
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x060027B6 RID: 10166 RVA: 0x000EC3F7 File Offset: 0x000EA5F7
		public QilNode Condition
		{
			get
			{
				return this.condition;
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x060027B7 RID: 10167 RVA: 0x000EC3FF File Offset: 0x000EA5FF
		public QilFunction TemplateFunction
		{
			get
			{
				return this.template.Function;
			}
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x000EC40C File Offset: 0x000EA60C
		public TemplateMatch(Template template, QilLoop filter)
		{
			this.template = template;
			this.priority = (double.IsNaN(template.Priority) ? XPathPatternBuilder.GetPriority(filter) : template.Priority);
			this.iterator = filter.Variable;
			this.condition = filter.Body;
			XPathPatternBuilder.CleanAnnotation(filter);
			this.NipOffTypeNameCheck();
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x000EC46C File Offset: 0x000EA66C
		private void NipOffTypeNameCheck()
		{
			QilBinary[] array = new QilBinary[4];
			int num = -1;
			QilNode left = this.condition;
			this.nodeKind = XmlNodeKindFlags.None;
			this.qname = null;
			while (left.NodeType == QilNodeType.And)
			{
				left = (array[++num & 3] = (QilBinary)left).Left;
			}
			if (left.NodeType != QilNodeType.IsType)
			{
				return;
			}
			QilBinary qilBinary = (QilBinary)left;
			if (qilBinary.Left != this.iterator || qilBinary.Right.NodeType != QilNodeType.LiteralType)
			{
				return;
			}
			XmlNodeKindFlags nodeKinds = qilBinary.Right.XmlType.NodeKinds;
			if (!Bits.ExactlyOne((uint)nodeKinds))
			{
				return;
			}
			this.nodeKind = nodeKinds;
			QilBinary qilBinary2 = array[num & 3];
			if (qilBinary2 != null && qilBinary2.Right.NodeType == QilNodeType.Eq)
			{
				QilBinary qilBinary3 = (QilBinary)qilBinary2.Right;
				if (qilBinary3.Left.NodeType == QilNodeType.NameOf && ((QilUnary)qilBinary3.Left).Child == this.iterator && qilBinary3.Right.NodeType == QilNodeType.LiteralQName)
				{
					this.qname = (QilName)((QilLiteral)qilBinary3.Right).Value;
					num--;
				}
			}
			QilBinary qilBinary4 = array[num & 3];
			QilBinary qilBinary5 = array[num - 1 & 3];
			if (qilBinary5 != null)
			{
				qilBinary5.Left = qilBinary4.Right;
				return;
			}
			if (qilBinary4 != null)
			{
				this.condition = qilBinary4.Right;
				return;
			}
			this.condition = null;
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x000EC5D1 File Offset: 0x000EA7D1
		// Note: this type is marked as 'beforefieldinit'.
		static TemplateMatch()
		{
		}

		// Token: 0x04001F8F RID: 8079
		public static readonly TemplateMatch.TemplateMatchComparer Comparer = new TemplateMatch.TemplateMatchComparer();

		// Token: 0x04001F90 RID: 8080
		private Template template;

		// Token: 0x04001F91 RID: 8081
		private double priority;

		// Token: 0x04001F92 RID: 8082
		private XmlNodeKindFlags nodeKind;

		// Token: 0x04001F93 RID: 8083
		private QilName qname;

		// Token: 0x04001F94 RID: 8084
		private QilIterator iterator;

		// Token: 0x04001F95 RID: 8085
		private QilNode condition;

		// Token: 0x020003E9 RID: 1001
		internal class TemplateMatchComparer : IComparer<TemplateMatch>
		{
			// Token: 0x060027BB RID: 10171 RVA: 0x000EC5DD File Offset: 0x000EA7DD
			public int Compare(TemplateMatch x, TemplateMatch y)
			{
				if (x.priority > y.priority)
				{
					return 1;
				}
				if (x.priority >= y.priority)
				{
					return x.template.OrderNumber - y.template.OrderNumber;
				}
				return -1;
			}

			// Token: 0x060027BC RID: 10172 RVA: 0x0000216B File Offset: 0x0000036B
			public TemplateMatchComparer()
			{
			}
		}
	}
}
