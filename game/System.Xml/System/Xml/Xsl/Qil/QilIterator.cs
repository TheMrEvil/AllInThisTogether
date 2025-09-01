using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004C0 RID: 1216
	internal class QilIterator : QilReference
	{
		// Token: 0x060030C7 RID: 12487 RVA: 0x0012169B File Offset: 0x0011F89B
		public QilIterator(QilNodeType nodeType, QilNode binding) : base(nodeType)
		{
			this.Binding = binding;
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x060030C8 RID: 12488 RVA: 0x0001222F File Offset: 0x0001042F
		public override int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170008D1 RID: 2257
		public override QilNode this[int index]
		{
			get
			{
				if (index != 0)
				{
					throw new IndexOutOfRangeException();
				}
				return this._binding;
			}
			set
			{
				if (index != 0)
				{
					throw new IndexOutOfRangeException();
				}
				this._binding = value;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x060030CB RID: 12491 RVA: 0x001216CE File Offset: 0x0011F8CE
		// (set) Token: 0x060030CC RID: 12492 RVA: 0x001216D6 File Offset: 0x0011F8D6
		public QilNode Binding
		{
			get
			{
				return this._binding;
			}
			set
			{
				this._binding = value;
			}
		}

		// Token: 0x040025CA RID: 9674
		private QilNode _binding;
	}
}
