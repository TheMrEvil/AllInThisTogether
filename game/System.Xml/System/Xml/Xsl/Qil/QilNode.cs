using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004C5 RID: 1221
	internal class QilNode : IList<QilNode>, ICollection<QilNode>, IEnumerable<QilNode>, IEnumerable
	{
		// Token: 0x060030F0 RID: 12528 RVA: 0x00121B38 File Offset: 0x0011FD38
		public QilNode(QilNodeType nodeType)
		{
			this.nodeType = nodeType;
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x00121B47 File Offset: 0x0011FD47
		public QilNode(QilNodeType nodeType, XmlQueryType xmlType)
		{
			this.nodeType = nodeType;
			this.xmlType = xmlType;
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x060030F2 RID: 12530 RVA: 0x00121B5D File Offset: 0x0011FD5D
		// (set) Token: 0x060030F3 RID: 12531 RVA: 0x00121B65 File Offset: 0x0011FD65
		public QilNodeType NodeType
		{
			get
			{
				return this.nodeType;
			}
			set
			{
				this.nodeType = value;
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x060030F4 RID: 12532 RVA: 0x00121B6E File Offset: 0x0011FD6E
		// (set) Token: 0x060030F5 RID: 12533 RVA: 0x00121B76 File Offset: 0x0011FD76
		public virtual XmlQueryType XmlType
		{
			get
			{
				return this.xmlType;
			}
			set
			{
				this.xmlType = value;
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x060030F6 RID: 12534 RVA: 0x00121B7F File Offset: 0x0011FD7F
		// (set) Token: 0x060030F7 RID: 12535 RVA: 0x00121B87 File Offset: 0x0011FD87
		public ISourceLineInfo SourceLine
		{
			get
			{
				return this.sourceLine;
			}
			set
			{
				this.sourceLine = value;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x060030F8 RID: 12536 RVA: 0x00121B90 File Offset: 0x0011FD90
		// (set) Token: 0x060030F9 RID: 12537 RVA: 0x00121B98 File Offset: 0x0011FD98
		public object Annotation
		{
			get
			{
				return this.annotation;
			}
			set
			{
				this.annotation = value;
			}
		}

		// Token: 0x060030FA RID: 12538 RVA: 0x00121BA1 File Offset: 0x0011FDA1
		public virtual QilNode DeepClone(QilFactory f)
		{
			return new QilCloneVisitor(f).Clone(this);
		}

		// Token: 0x060030FB RID: 12539 RVA: 0x00121BAF File Offset: 0x0011FDAF
		public virtual QilNode ShallowClone(QilFactory f)
		{
			return (QilNode)base.MemberwiseClone();
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x060030FC RID: 12540 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public virtual int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170008E2 RID: 2274
		public virtual QilNode this[int index]
		{
			get
			{
				throw new IndexOutOfRangeException();
			}
			set
			{
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x060030FF RID: 12543 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public virtual void Insert(int index, QilNode node)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003100 RID: 12544 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public virtual void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003101 RID: 12545 RVA: 0x00121BC3 File Offset: 0x0011FDC3
		public IEnumerator<QilNode> GetEnumerator()
		{
			return new IListEnumerator<QilNode>(this);
		}

		// Token: 0x06003102 RID: 12546 RVA: 0x00121BC3 File Offset: 0x0011FDC3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new IListEnumerator<QilNode>(this);
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06003103 RID: 12547 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003104 RID: 12548 RVA: 0x00121BD0 File Offset: 0x0011FDD0
		public virtual void Add(QilNode node)
		{
			this.Insert(this.Count, node);
		}

		// Token: 0x06003105 RID: 12549 RVA: 0x00121BE0 File Offset: 0x0011FDE0
		public virtual void Add(IList<QilNode> list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				this.Insert(this.Count, list[i]);
			}
		}

		// Token: 0x06003106 RID: 12550 RVA: 0x00121C14 File Offset: 0x0011FE14
		public virtual void Clear()
		{
			for (int i = this.Count - 1; i >= 0; i--)
			{
				this.RemoveAt(i);
			}
		}

		// Token: 0x06003107 RID: 12551 RVA: 0x00121C3B File Offset: 0x0011FE3B
		public virtual bool Contains(QilNode node)
		{
			return this.IndexOf(node) != -1;
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x00121C4C File Offset: 0x0011FE4C
		public virtual void CopyTo(QilNode[] array, int index)
		{
			for (int i = 0; i < this.Count; i++)
			{
				array[index + i] = this[i];
			}
		}

		// Token: 0x06003109 RID: 12553 RVA: 0x00121C78 File Offset: 0x0011FE78
		public virtual bool Remove(QilNode node)
		{
			int num = this.IndexOf(node);
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x0600310A RID: 12554 RVA: 0x00121C9C File Offset: 0x0011FE9C
		public virtual int IndexOf(QilNode node)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (node.Equals(this[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x040025D1 RID: 9681
		protected QilNodeType nodeType;

		// Token: 0x040025D2 RID: 9682
		protected XmlQueryType xmlType;

		// Token: 0x040025D3 RID: 9683
		protected ISourceLineInfo sourceLine;

		// Token: 0x040025D4 RID: 9684
		protected object annotation;
	}
}
