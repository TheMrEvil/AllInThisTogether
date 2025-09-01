using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004C7 RID: 1223
	internal class QilParameter : QilIterator
	{
		// Token: 0x0600310B RID: 12555 RVA: 0x00121CCC File Offset: 0x0011FECC
		public QilParameter(QilNodeType nodeType, QilNode defaultValue, QilNode name, XmlQueryType xmlType) : base(nodeType, defaultValue)
		{
			this._name = name;
			this.xmlType = xmlType;
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x0600310C RID: 12556 RVA: 0x00066748 File Offset: 0x00064948
		public override int Count
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170008E5 RID: 2277
		public override QilNode this[int index]
		{
			get
			{
				if (index == 0)
				{
					return base.Binding;
				}
				if (index != 1)
				{
					throw new IndexOutOfRangeException();
				}
				return this._name;
			}
			set
			{
				if (index == 0)
				{
					base.Binding = value;
					return;
				}
				if (index != 1)
				{
					throw new IndexOutOfRangeException();
				}
				this._name = value;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x0600310F RID: 12559 RVA: 0x00121D23 File Offset: 0x0011FF23
		// (set) Token: 0x06003110 RID: 12560 RVA: 0x00121D2B File Offset: 0x0011FF2B
		public QilNode DefaultValue
		{
			get
			{
				return base.Binding;
			}
			set
			{
				base.Binding = value;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06003111 RID: 12561 RVA: 0x00121D34 File Offset: 0x0011FF34
		// (set) Token: 0x06003112 RID: 12562 RVA: 0x00121D41 File Offset: 0x0011FF41
		public QilName Name
		{
			get
			{
				return (QilName)this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x04002641 RID: 9793
		private QilNode _name;
	}
}
