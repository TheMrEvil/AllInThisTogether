using System;
using System.Data;
using System.Threading;

namespace System.Xml
{
	// Token: 0x0200007E RID: 126
	internal sealed class XmlBoundElement : XmlElement
	{
		// Token: 0x0600057B RID: 1403 RVA: 0x000144B3 File Offset: 0x000126B3
		internal XmlBoundElement(string prefix, string localName, string namespaceURI, XmlDocument doc) : base(prefix, localName, namespaceURI, doc)
		{
			this._state = ElementState.None;
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x000144C7 File Offset: 0x000126C7
		public override XmlAttributeCollection Attributes
		{
			get
			{
				this.AutoFoliate();
				return base.Attributes;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x000144D5 File Offset: 0x000126D5
		public override bool HasAttributes
		{
			get
			{
				return this.Attributes.Count > 0;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x000144E5 File Offset: 0x000126E5
		public override XmlNode FirstChild
		{
			get
			{
				this.AutoFoliate();
				return base.FirstChild;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x000144F3 File Offset: 0x000126F3
		internal XmlNode SafeFirstChild
		{
			get
			{
				return base.FirstChild;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x000144FB File Offset: 0x000126FB
		public override XmlNode LastChild
		{
			get
			{
				this.AutoFoliate();
				return base.LastChild;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x0001450C File Offset: 0x0001270C
		public override XmlNode PreviousSibling
		{
			get
			{
				XmlNode previousSibling = base.PreviousSibling;
				if (previousSibling == null)
				{
					XmlBoundElement xmlBoundElement = this.ParentNode as XmlBoundElement;
					if (xmlBoundElement != null)
					{
						xmlBoundElement.AutoFoliate();
						return base.PreviousSibling;
					}
				}
				return previousSibling;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x00014540 File Offset: 0x00012740
		internal XmlNode SafePreviousSibling
		{
			get
			{
				return base.PreviousSibling;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x00014548 File Offset: 0x00012748
		public override XmlNode NextSibling
		{
			get
			{
				XmlNode nextSibling = base.NextSibling;
				if (nextSibling == null)
				{
					XmlBoundElement xmlBoundElement = this.ParentNode as XmlBoundElement;
					if (xmlBoundElement != null)
					{
						xmlBoundElement.AutoFoliate();
						return base.NextSibling;
					}
				}
				return nextSibling;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0001457C File Offset: 0x0001277C
		internal XmlNode SafeNextSibling
		{
			get
			{
				return base.NextSibling;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x00014584 File Offset: 0x00012784
		public override bool HasChildNodes
		{
			get
			{
				this.AutoFoliate();
				return base.HasChildNodes;
			}
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00014592 File Offset: 0x00012792
		public override XmlNode InsertBefore(XmlNode newChild, XmlNode refChild)
		{
			this.AutoFoliate();
			return base.InsertBefore(newChild, refChild);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x000145A2 File Offset: 0x000127A2
		public override XmlNode InsertAfter(XmlNode newChild, XmlNode refChild)
		{
			this.AutoFoliate();
			return base.InsertAfter(newChild, refChild);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x000145B2 File Offset: 0x000127B2
		public override XmlNode ReplaceChild(XmlNode newChild, XmlNode oldChild)
		{
			this.AutoFoliate();
			return base.ReplaceChild(newChild, oldChild);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x000145C2 File Offset: 0x000127C2
		public override XmlNode AppendChild(XmlNode newChild)
		{
			this.AutoFoliate();
			return base.AppendChild(newChild);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x000145D4 File Offset: 0x000127D4
		internal void RemoveAllChildren()
		{
			XmlNode nextSibling;
			for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = nextSibling)
			{
				nextSibling = xmlNode.NextSibling;
				this.RemoveChild(xmlNode);
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x000145FC File Offset: 0x000127FC
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x00014604 File Offset: 0x00012804
		public override string InnerXml
		{
			get
			{
				return base.InnerXml;
			}
			set
			{
				this.RemoveAllChildren();
				XmlDataDocument xmlDataDocument = (XmlDataDocument)this.OwnerDocument;
				bool ignoreXmlEvents = xmlDataDocument.IgnoreXmlEvents;
				bool ignoreDataSetEvents = xmlDataDocument.IgnoreDataSetEvents;
				xmlDataDocument.IgnoreXmlEvents = true;
				xmlDataDocument.IgnoreDataSetEvents = true;
				base.InnerXml = value;
				xmlDataDocument.SyncTree(this);
				xmlDataDocument.IgnoreDataSetEvents = ignoreDataSetEvents;
				xmlDataDocument.IgnoreXmlEvents = ignoreXmlEvents;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x00014659 File Offset: 0x00012859
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x00014661 File Offset: 0x00012861
		internal DataRow Row
		{
			get
			{
				return this._row;
			}
			set
			{
				this._row = value;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x0001466A File Offset: 0x0001286A
		internal bool IsFoliated
		{
			get
			{
				while (this._state == ElementState.Foliating || this._state == ElementState.Defoliating)
				{
					Thread.Sleep(0);
				}
				return this._state != ElementState.Defoliated;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x00014692 File Offset: 0x00012892
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x0001469A File Offset: 0x0001289A
		internal ElementState ElementState
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x000146A4 File Offset: 0x000128A4
		internal void Foliate(ElementState newState)
		{
			XmlDataDocument xmlDataDocument = (XmlDataDocument)this.OwnerDocument;
			if (xmlDataDocument != null)
			{
				xmlDataDocument.Foliate(this, newState);
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x000146C8 File Offset: 0x000128C8
		private void AutoFoliate()
		{
			XmlDataDocument xmlDataDocument = (XmlDataDocument)this.OwnerDocument;
			if (xmlDataDocument != null)
			{
				xmlDataDocument.Foliate(this, xmlDataDocument.AutoFoliationState);
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x000146F4 File Offset: 0x000128F4
		public override XmlNode CloneNode(bool deep)
		{
			XmlDataDocument xmlDataDocument = (XmlDataDocument)this.OwnerDocument;
			ElementState autoFoliationState = xmlDataDocument.AutoFoliationState;
			xmlDataDocument.AutoFoliationState = ElementState.WeakFoliation;
			XmlElement result;
			try
			{
				this.Foliate(ElementState.WeakFoliation);
				result = (XmlElement)base.CloneNode(deep);
			}
			finally
			{
				xmlDataDocument.AutoFoliationState = autoFoliationState;
			}
			return result;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001474C File Offset: 0x0001294C
		public override void WriteContentTo(XmlWriter w)
		{
			DataPointer dataPointer = new DataPointer((XmlDataDocument)this.OwnerDocument, this);
			try
			{
				dataPointer.AddPointer();
				XmlBoundElement.WriteBoundElementContentTo(dataPointer, w);
			}
			finally
			{
				dataPointer.SetNoLongerUse();
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00014794 File Offset: 0x00012994
		public override void WriteTo(XmlWriter w)
		{
			DataPointer dataPointer = new DataPointer((XmlDataDocument)this.OwnerDocument, this);
			try
			{
				dataPointer.AddPointer();
				this.WriteRootBoundElementTo(dataPointer, w);
			}
			finally
			{
				dataPointer.SetNoLongerUse();
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x000147DC File Offset: 0x000129DC
		private void WriteRootBoundElementTo(DataPointer dp, XmlWriter w)
		{
			XmlDataDocument xmlDataDocument = (XmlDataDocument)this.OwnerDocument;
			w.WriteStartElement(dp.Prefix, dp.LocalName, dp.NamespaceURI);
			int attributeCount = dp.AttributeCount;
			bool flag = false;
			if (attributeCount > 0)
			{
				for (int i = 0; i < attributeCount; i++)
				{
					dp.MoveToAttribute(i);
					if (dp.Prefix == "xmlns" && dp.LocalName == "xsi")
					{
						flag = true;
					}
					XmlBoundElement.WriteTo(dp, w);
					dp.MoveToOwnerElement();
				}
			}
			if (!flag && xmlDataDocument._bLoadFromDataSet && xmlDataDocument._bHasXSINIL)
			{
				w.WriteAttributeString("xmlns", "xsi", "http://www.w3.org/2000/xmlns/", "http://www.w3.org/2001/XMLSchema-instance");
			}
			XmlBoundElement.WriteBoundElementContentTo(dp, w);
			if (dp.IsEmptyElement)
			{
				w.WriteEndElement();
				return;
			}
			w.WriteFullEndElement();
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x000148AC File Offset: 0x00012AAC
		private static void WriteBoundElementTo(DataPointer dp, XmlWriter w)
		{
			w.WriteStartElement(dp.Prefix, dp.LocalName, dp.NamespaceURI);
			int attributeCount = dp.AttributeCount;
			if (attributeCount > 0)
			{
				for (int i = 0; i < attributeCount; i++)
				{
					dp.MoveToAttribute(i);
					XmlBoundElement.WriteTo(dp, w);
					dp.MoveToOwnerElement();
				}
			}
			XmlBoundElement.WriteBoundElementContentTo(dp, w);
			if (dp.IsEmptyElement)
			{
				w.WriteEndElement();
				return;
			}
			w.WriteFullEndElement();
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0001491A File Offset: 0x00012B1A
		private static void WriteBoundElementContentTo(DataPointer dp, XmlWriter w)
		{
			if (!dp.IsEmptyElement && dp.MoveToFirstChild())
			{
				do
				{
					XmlBoundElement.WriteTo(dp, w);
				}
				while (dp.MoveToNextSibling());
				dp.MoveToParent();
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00014944 File Offset: 0x00012B44
		private static void WriteTo(DataPointer dp, XmlWriter w)
		{
			switch (dp.NodeType)
			{
			case XmlNodeType.Element:
				XmlBoundElement.WriteBoundElementTo(dp, w);
				return;
			case XmlNodeType.Attribute:
				if (!dp.IsDefault)
				{
					w.WriteStartAttribute(dp.Prefix, dp.LocalName, dp.NamespaceURI);
					if (dp.MoveToFirstChild())
					{
						do
						{
							XmlBoundElement.WriteTo(dp, w);
						}
						while (dp.MoveToNextSibling());
						dp.MoveToParent();
					}
					w.WriteEndAttribute();
					return;
				}
				break;
			case XmlNodeType.Text:
				w.WriteString(dp.Value);
				return;
			default:
				if (dp.GetNode() != null)
				{
					dp.GetNode().WriteTo(w);
				}
				break;
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x000149DC File Offset: 0x00012BDC
		public override XmlNodeList GetElementsByTagName(string name)
		{
			XmlNodeList elementsByTagName = base.GetElementsByTagName(name);
			int count = elementsByTagName.Count;
			return elementsByTagName;
		}

		// Token: 0x04000653 RID: 1619
		private DataRow _row;

		// Token: 0x04000654 RID: 1620
		private ElementState _state;
	}
}
