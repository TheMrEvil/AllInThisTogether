using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004BC RID: 1212
	internal class QilFunction : QilReference
	{
		// Token: 0x060030AC RID: 12460 RVA: 0x0012153E File Offset: 0x0011F73E
		public QilFunction(QilNodeType nodeType, QilNode arguments, QilNode definition, QilNode sideEffects, XmlQueryType resultType) : base(nodeType)
		{
			this._arguments = arguments;
			this._definition = definition;
			this._sideEffects = sideEffects;
			this.xmlType = resultType;
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x060030AD RID: 12461 RVA: 0x000708A9 File Offset: 0x0006EAA9
		public override int Count
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x170008C5 RID: 2245
		public override QilNode this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this._arguments;
				case 1:
					return this._definition;
				case 2:
					return this._sideEffects;
				default:
					throw new IndexOutOfRangeException();
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					this._arguments = value;
					return;
				case 1:
					this._definition = value;
					return;
				case 2:
					this._sideEffects = value;
					return;
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x060030B0 RID: 12464 RVA: 0x001215C8 File Offset: 0x0011F7C8
		// (set) Token: 0x060030B1 RID: 12465 RVA: 0x001215D5 File Offset: 0x0011F7D5
		public QilList Arguments
		{
			get
			{
				return (QilList)this._arguments;
			}
			set
			{
				this._arguments = value;
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x060030B2 RID: 12466 RVA: 0x001215DE File Offset: 0x0011F7DE
		// (set) Token: 0x060030B3 RID: 12467 RVA: 0x001215E6 File Offset: 0x0011F7E6
		public QilNode Definition
		{
			get
			{
				return this._definition;
			}
			set
			{
				this._definition = value;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x060030B4 RID: 12468 RVA: 0x001215EF File Offset: 0x0011F7EF
		// (set) Token: 0x060030B5 RID: 12469 RVA: 0x00121600 File Offset: 0x0011F800
		public bool MaybeSideEffects
		{
			get
			{
				return this._sideEffects.NodeType == QilNodeType.True;
			}
			set
			{
				this._sideEffects.NodeType = (value ? QilNodeType.True : QilNodeType.False);
			}
		}

		// Token: 0x040025C7 RID: 9671
		private QilNode _arguments;

		// Token: 0x040025C8 RID: 9672
		private QilNode _definition;

		// Token: 0x040025C9 RID: 9673
		private QilNode _sideEffects;
	}
}
