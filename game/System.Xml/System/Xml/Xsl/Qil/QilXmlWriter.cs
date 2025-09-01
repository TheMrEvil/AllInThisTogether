using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004D8 RID: 1240
	internal class QilXmlWriter : QilScopedVisitor
	{
		// Token: 0x06003325 RID: 13093 RVA: 0x0012436C File Offset: 0x0012256C
		public QilXmlWriter(XmlWriter writer) : this(writer, QilXmlWriter.Options.Annotations | QilXmlWriter.Options.TypeInfo | QilXmlWriter.Options.LineInfo | QilXmlWriter.Options.NodeIdentity | QilXmlWriter.Options.NodeLocation)
		{
		}

		// Token: 0x06003326 RID: 13094 RVA: 0x00124377 File Offset: 0x00122577
		public QilXmlWriter(XmlWriter writer, QilXmlWriter.Options options)
		{
			this.writer = writer;
			this._ngen = new QilXmlWriter.NameGenerator();
			this.options = options;
		}

		// Token: 0x06003327 RID: 13095 RVA: 0x00124398 File Offset: 0x00122598
		public void ToXml(QilNode node)
		{
			this.VisitAssumeReference(node);
		}

		// Token: 0x06003328 RID: 13096 RVA: 0x001243A4 File Offset: 0x001225A4
		protected virtual void WriteAnnotations(object ann)
		{
			string text = null;
			string text2 = null;
			if (ann == null)
			{
				return;
			}
			if (ann is string)
			{
				text = (ann as string);
			}
			else if (ann is IQilAnnotation)
			{
				text2 = (ann as IQilAnnotation).Name;
				text = ann.ToString();
			}
			else if (ann is IList<object>)
			{
				foreach (object ann2 in ((IList<object>)ann))
				{
					this.WriteAnnotations(ann2);
				}
				return;
			}
			if (text != null && text.Length != 0)
			{
				this.writer.WriteComment((text2 != null && text2.Length != 0) ? (text2 + ": " + text) : text);
			}
		}

		// Token: 0x06003329 RID: 13097 RVA: 0x00124460 File Offset: 0x00122660
		protected virtual void WriteLineInfo(QilNode node)
		{
			this.writer.WriteAttributeString("lineInfo", string.Format(CultureInfo.InvariantCulture, "[{0},{1} -- {2},{3}]", new object[]
			{
				node.SourceLine.Start.Line,
				node.SourceLine.Start.Pos,
				node.SourceLine.End.Line,
				node.SourceLine.End.Pos
			}));
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x001244FE File Offset: 0x001226FE
		protected virtual void WriteXmlType(QilNode node)
		{
			this.writer.WriteAttributeString("xmlType", node.XmlType.ToString(((this.options & QilXmlWriter.Options.RoundTripTypeInfo) != QilXmlWriter.Options.None) ? "S" : "G"));
		}

		// Token: 0x0600332B RID: 13099 RVA: 0x00124534 File Offset: 0x00122734
		protected override QilNode VisitChildren(QilNode node)
		{
			if (node is QilLiteral)
			{
				this.writer.WriteValue(Convert.ToString(((QilLiteral)node).Value, CultureInfo.InvariantCulture));
				return node;
			}
			if (node is QilReference)
			{
				QilReference qilReference = (QilReference)node;
				this.writer.WriteAttributeString("id", this._ngen.NameOf(node));
				if (qilReference.DebugName != null)
				{
					this.writer.WriteAttributeString("name", qilReference.DebugName.ToString());
				}
				if (node.NodeType == QilNodeType.Parameter)
				{
					QilParameter qilParameter = (QilParameter)node;
					if (qilParameter.DefaultValue != null)
					{
						this.VisitAssumeReference(qilParameter.DefaultValue);
					}
					return node;
				}
			}
			return base.VisitChildren(node);
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x001245E8 File Offset: 0x001227E8
		protected override QilNode VisitReference(QilNode node)
		{
			QilReference qilReference = (QilReference)node;
			string text = this._ngen.NameOf(node);
			if (text == null)
			{
				text = "OUT-OF-SCOPE REFERENCE";
			}
			this.writer.WriteStartElement("RefTo");
			this.writer.WriteAttributeString("id", text);
			if (qilReference.DebugName != null)
			{
				this.writer.WriteAttributeString("name", qilReference.DebugName.ToString());
			}
			this.writer.WriteEndElement();
			return node;
		}

		// Token: 0x0600332D RID: 13101 RVA: 0x00124664 File Offset: 0x00122864
		protected override QilNode VisitQilExpression(QilExpression qil)
		{
			IList<QilNode> list = new QilXmlWriter.ForwardRefFinder().Find(qil);
			if (list != null && list.Count > 0)
			{
				this.writer.WriteStartElement("ForwardDecls");
				foreach (QilNode qilNode in list)
				{
					this.writer.WriteStartElement(Enum.GetName(typeof(QilNodeType), qilNode.NodeType));
					this.writer.WriteAttributeString("id", this._ngen.NameOf(qilNode));
					this.WriteXmlType(qilNode);
					if (qilNode.NodeType == QilNodeType.Function)
					{
						this.Visit(qilNode[0]);
						this.Visit(qilNode[2]);
					}
					this.writer.WriteEndElement();
				}
				this.writer.WriteEndElement();
			}
			return this.VisitChildren(qil);
		}

		// Token: 0x0600332E RID: 13102 RVA: 0x00124764 File Offset: 0x00122964
		protected override QilNode VisitLiteralType(QilLiteral value)
		{
			this.writer.WriteString(value.ToString(((this.options & QilXmlWriter.Options.TypeInfo) != QilXmlWriter.Options.None) ? "G" : "S"));
			return value;
		}

		// Token: 0x0600332F RID: 13103 RVA: 0x00124793 File Offset: 0x00122993
		protected override QilNode VisitLiteralQName(QilName value)
		{
			this.writer.WriteAttributeString("name", value.ToString());
			return value;
		}

		// Token: 0x06003330 RID: 13104 RVA: 0x001247AC File Offset: 0x001229AC
		protected override void BeginScope(QilNode node)
		{
			this._ngen.NameOf(node);
		}

		// Token: 0x06003331 RID: 13105 RVA: 0x001247BB File Offset: 0x001229BB
		protected override void EndScope(QilNode node)
		{
			this._ngen.ClearName(node);
		}

		// Token: 0x06003332 RID: 13106 RVA: 0x001247CC File Offset: 0x001229CC
		protected override void BeforeVisit(QilNode node)
		{
			base.BeforeVisit(node);
			if ((this.options & QilXmlWriter.Options.Annotations) != QilXmlWriter.Options.None)
			{
				this.WriteAnnotations(node.Annotation);
			}
			this.writer.WriteStartElement("", Enum.GetName(typeof(QilNodeType), node.NodeType), "");
			if ((this.options & (QilXmlWriter.Options.TypeInfo | QilXmlWriter.Options.RoundTripTypeInfo)) != QilXmlWriter.Options.None)
			{
				this.WriteXmlType(node);
			}
			if ((this.options & QilXmlWriter.Options.LineInfo) != QilXmlWriter.Options.None && node.SourceLine != null)
			{
				this.WriteLineInfo(node);
			}
		}

		// Token: 0x06003333 RID: 13107 RVA: 0x0012484F File Offset: 0x00122A4F
		protected override void AfterVisit(QilNode node)
		{
			this.writer.WriteEndElement();
			base.AfterVisit(node);
		}

		// Token: 0x04002654 RID: 9812
		protected XmlWriter writer;

		// Token: 0x04002655 RID: 9813
		protected QilXmlWriter.Options options;

		// Token: 0x04002656 RID: 9814
		private QilXmlWriter.NameGenerator _ngen;

		// Token: 0x020004D9 RID: 1241
		[Flags]
		public enum Options
		{
			// Token: 0x04002658 RID: 9816
			None = 0,
			// Token: 0x04002659 RID: 9817
			Annotations = 1,
			// Token: 0x0400265A RID: 9818
			TypeInfo = 2,
			// Token: 0x0400265B RID: 9819
			RoundTripTypeInfo = 4,
			// Token: 0x0400265C RID: 9820
			LineInfo = 8,
			// Token: 0x0400265D RID: 9821
			NodeIdentity = 16,
			// Token: 0x0400265E RID: 9822
			NodeLocation = 32
		}

		// Token: 0x020004DA RID: 1242
		internal class ForwardRefFinder : QilVisitor
		{
			// Token: 0x06003334 RID: 13108 RVA: 0x00124863 File Offset: 0x00122A63
			public IList<QilNode> Find(QilExpression qil)
			{
				this.Visit(qil);
				return this._fwdrefs;
			}

			// Token: 0x06003335 RID: 13109 RVA: 0x00124873 File Offset: 0x00122A73
			protected override QilNode Visit(QilNode node)
			{
				if (node is QilIterator || node is QilFunction)
				{
					this._backrefs.Add(node);
				}
				return base.Visit(node);
			}

			// Token: 0x06003336 RID: 13110 RVA: 0x00124898 File Offset: 0x00122A98
			protected override QilNode VisitReference(QilNode node)
			{
				if (!this._backrefs.Contains(node) && !this._fwdrefs.Contains(node))
				{
					this._fwdrefs.Add(node);
				}
				return node;
			}

			// Token: 0x06003337 RID: 13111 RVA: 0x001248C3 File Offset: 0x00122AC3
			public ForwardRefFinder()
			{
			}

			// Token: 0x0400265F RID: 9823
			private List<QilNode> _fwdrefs = new List<QilNode>();

			// Token: 0x04002660 RID: 9824
			private List<QilNode> _backrefs = new List<QilNode>();
		}

		// Token: 0x020004DB RID: 1243
		private sealed class NameGenerator
		{
			// Token: 0x06003338 RID: 13112 RVA: 0x001248E4 File Offset: 0x00122AE4
			public NameGenerator()
			{
				string text = "$";
				this._len = (this._zero = text.Length);
				this._start = 'a';
				this._end = 'z';
				this._name = new StringBuilder(text, this._len + 2);
				this._name.Append(this._start);
			}

			// Token: 0x06003339 RID: 13113 RVA: 0x00124948 File Offset: 0x00122B48
			public string NextName()
			{
				string result = this._name.ToString();
				char c = this._name[this._len];
				if (c == this._end)
				{
					this._name[this._len] = this._start;
					int len = this._len;
					while (len-- > this._zero && this._name[len] == this._end)
					{
						this._name[len] = this._start;
					}
					if (len < this._zero)
					{
						this._len++;
						this._name.Append(this._start);
					}
					else
					{
						StringBuilder name = this._name;
						int index = len;
						char c2 = name[index];
						name[index] = c2 + '\u0001';
					}
				}
				else
				{
					this._name[this._len] = c + '\u0001';
				}
				return result;
			}

			// Token: 0x0600333A RID: 13114 RVA: 0x00124A34 File Offset: 0x00122C34
			public string NameOf(QilNode n)
			{
				object annotation = n.Annotation;
				QilXmlWriter.NameGenerator.NameAnnotation nameAnnotation = annotation as QilXmlWriter.NameGenerator.NameAnnotation;
				string text;
				if (nameAnnotation == null)
				{
					text = this.NextName();
					n.Annotation = new QilXmlWriter.NameGenerator.NameAnnotation(text, annotation);
				}
				else
				{
					text = nameAnnotation.Name;
				}
				return text;
			}

			// Token: 0x0600333B RID: 13115 RVA: 0x00124A72 File Offset: 0x00122C72
			public void ClearName(QilNode n)
			{
				if (n.Annotation is QilXmlWriter.NameGenerator.NameAnnotation)
				{
					n.Annotation = ((QilXmlWriter.NameGenerator.NameAnnotation)n.Annotation).PriorAnnotation;
				}
			}

			// Token: 0x04002661 RID: 9825
			private StringBuilder _name;

			// Token: 0x04002662 RID: 9826
			private int _len;

			// Token: 0x04002663 RID: 9827
			private int _zero;

			// Token: 0x04002664 RID: 9828
			private char _start;

			// Token: 0x04002665 RID: 9829
			private char _end;

			// Token: 0x020004DC RID: 1244
			private class NameAnnotation : ListBase<object>
			{
				// Token: 0x0600333C RID: 13116 RVA: 0x00124A97 File Offset: 0x00122C97
				public NameAnnotation(string s, object a)
				{
					this.Name = s;
					this.PriorAnnotation = a;
				}

				// Token: 0x170008FF RID: 2303
				// (get) Token: 0x0600333D RID: 13117 RVA: 0x0001222F File Offset: 0x0001042F
				public override int Count
				{
					get
					{
						return 1;
					}
				}

				// Token: 0x17000900 RID: 2304
				public override object this[int index]
				{
					get
					{
						if (index == 0)
						{
							return this.PriorAnnotation;
						}
						throw new IndexOutOfRangeException();
					}
					set
					{
						throw new NotSupportedException();
					}
				}

				// Token: 0x04002666 RID: 9830
				public string Name;

				// Token: 0x04002667 RID: 9831
				public object PriorAnnotation;
			}
		}
	}
}
