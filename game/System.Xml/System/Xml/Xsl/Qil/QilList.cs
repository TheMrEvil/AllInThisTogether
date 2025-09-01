using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004C1 RID: 1217
	internal class QilList : QilNode
	{
		// Token: 0x060030CD RID: 12493 RVA: 0x001216DF File Offset: 0x0011F8DF
		public QilList(QilNodeType nodeType) : base(nodeType)
		{
			this._members = new QilNode[4];
			this.xmlType = null;
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x060030CE RID: 12494 RVA: 0x001216FC File Offset: 0x0011F8FC
		public override XmlQueryType XmlType
		{
			get
			{
				if (this.xmlType == null)
				{
					XmlQueryType xmlQueryType = XmlQueryTypeFactory.Empty;
					if (this._count > 0)
					{
						if (this.nodeType == QilNodeType.Sequence)
						{
							for (int i = 0; i < this._count; i++)
							{
								xmlQueryType = XmlQueryTypeFactory.Sequence(xmlQueryType, this._members[i].XmlType);
							}
						}
						else if (this.nodeType == QilNodeType.BranchList)
						{
							xmlQueryType = this._members[0].XmlType;
							for (int j = 1; j < this._count; j++)
							{
								xmlQueryType = XmlQueryTypeFactory.Choice(xmlQueryType, this._members[j].XmlType);
							}
						}
					}
					this.xmlType = xmlQueryType;
				}
				return this.xmlType;
			}
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x001217A3 File Offset: 0x0011F9A3
		public override QilNode ShallowClone(QilFactory f)
		{
			QilList qilList = (QilList)base.MemberwiseClone();
			qilList._members = (QilNode[])this._members.Clone();
			return qilList;
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x060030D0 RID: 12496 RVA: 0x001217C6 File Offset: 0x0011F9C6
		public override int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x170008D5 RID: 2261
		public override QilNode this[int index]
		{
			get
			{
				if (index >= 0 && index < this._count)
				{
					return this._members[index];
				}
				throw new IndexOutOfRangeException();
			}
			set
			{
				if (index >= 0 && index < this._count)
				{
					this._members[index] = value;
					this.xmlType = null;
					return;
				}
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x00121814 File Offset: 0x0011FA14
		public override void Insert(int index, QilNode node)
		{
			if (index < 0 || index > this._count)
			{
				throw new IndexOutOfRangeException();
			}
			if (this._count == this._members.Length)
			{
				QilNode[] array = new QilNode[this._count * 2];
				Array.Copy(this._members, array, this._count);
				this._members = array;
			}
			if (index < this._count)
			{
				Array.Copy(this._members, index, this._members, index + 1, this._count - index);
			}
			this._count++;
			this._members[index] = node;
			this.xmlType = null;
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x001218B0 File Offset: 0x0011FAB0
		public override void RemoveAt(int index)
		{
			if (index < 0 || index >= this._count)
			{
				throw new IndexOutOfRangeException();
			}
			this._count--;
			if (index < this._count)
			{
				Array.Copy(this._members, index + 1, this._members, index, this._count - index);
			}
			this._members[this._count] = null;
			this.xmlType = null;
		}

		// Token: 0x040025CB RID: 9675
		private int _count;

		// Token: 0x040025CC RID: 9676
		private QilNode[] _members;
	}
}
