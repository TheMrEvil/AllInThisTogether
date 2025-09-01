using System;

namespace System.Xml.Schema
{
	// Token: 0x020004E4 RID: 1252
	internal class ForwardAxis
	{
		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06003366 RID: 13158 RVA: 0x001252B6 File Offset: 0x001234B6
		internal DoubleLinkAxis RootNode
		{
			get
			{
				return this._rootNode;
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06003367 RID: 13159 RVA: 0x001252BE File Offset: 0x001234BE
		internal DoubleLinkAxis TopNode
		{
			get
			{
				return this._topNode;
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06003368 RID: 13160 RVA: 0x001252C6 File Offset: 0x001234C6
		internal bool IsAttribute
		{
			get
			{
				return this._isAttribute;
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06003369 RID: 13161 RVA: 0x001252CE File Offset: 0x001234CE
		internal bool IsDss
		{
			get
			{
				return this._isDss;
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x0600336A RID: 13162 RVA: 0x001252D6 File Offset: 0x001234D6
		internal bool IsSelfAxis
		{
			get
			{
				return this._isSelfAxis;
			}
		}

		// Token: 0x0600336B RID: 13163 RVA: 0x001252E0 File Offset: 0x001234E0
		public ForwardAxis(DoubleLinkAxis axis, bool isdesorself)
		{
			this._isDss = isdesorself;
			this._isAttribute = Asttree.IsAttribute(axis);
			this._topNode = axis;
			this._rootNode = axis;
			while (this._rootNode.Input != null)
			{
				this._rootNode = (DoubleLinkAxis)this._rootNode.Input;
			}
			this._isSelfAxis = Asttree.IsSelf(this._topNode);
		}

		// Token: 0x0400267C RID: 9852
		private DoubleLinkAxis _topNode;

		// Token: 0x0400267D RID: 9853
		private DoubleLinkAxis _rootNode;

		// Token: 0x0400267E RID: 9854
		private bool _isAttribute;

		// Token: 0x0400267F RID: 9855
		private bool _isDss;

		// Token: 0x04002680 RID: 9856
		private bool _isSelfAxis;
	}
}
