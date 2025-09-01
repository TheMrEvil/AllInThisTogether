using System;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x0200066F RID: 1647
	internal sealed class XPathDocumentNavigator : XPathNavigator, IXmlLineInfo
	{
		// Token: 0x06004299 RID: 17049 RVA: 0x0016BBED File Offset: 0x00169DED
		public XPathDocumentNavigator(XPathNode[] pageCurrent, int idxCurrent, XPathNode[] pageParent, int idxParent)
		{
			this._pageCurrent = pageCurrent;
			this._pageParent = pageParent;
			this._idxCurrent = idxCurrent;
			this._idxParent = idxParent;
		}

		// Token: 0x0600429A RID: 17050 RVA: 0x0016BC12 File Offset: 0x00169E12
		public XPathDocumentNavigator(XPathDocumentNavigator nav) : this(nav._pageCurrent, nav._idxCurrent, nav._pageParent, nav._idxParent)
		{
			this._atomizedLocalName = nav._atomizedLocalName;
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x0600429B RID: 17051 RVA: 0x0016BC40 File Offset: 0x00169E40
		public override string Value
		{
			get
			{
				string value = this._pageCurrent[this._idxCurrent].Value;
				if (value != null)
				{
					return value;
				}
				if (this._idxParent != 0)
				{
					return this._pageParent[this._idxParent].Value;
				}
				string text = string.Empty;
				StringBuilder stringBuilder = null;
				XPathNode[] pageCurrent;
				XPathNode[] pageEnd = pageCurrent = this._pageCurrent;
				int idxCurrent;
				int idxEnd = idxCurrent = this._idxCurrent;
				if (!XPathNodeHelper.GetNonDescendant(ref pageEnd, ref idxEnd))
				{
					pageEnd = null;
					idxEnd = 0;
				}
				while (XPathNodeHelper.GetTextFollowing(ref pageCurrent, ref idxCurrent, pageEnd, idxEnd))
				{
					if (text.Length == 0)
					{
						text = pageCurrent[idxCurrent].Value;
					}
					else
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder();
							stringBuilder.Append(text);
						}
						stringBuilder.Append(pageCurrent[idxCurrent].Value);
					}
				}
				if (stringBuilder == null)
				{
					return text;
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0600429C RID: 17052 RVA: 0x0016BD15 File Offset: 0x00169F15
		public override XPathNavigator Clone()
		{
			return new XPathDocumentNavigator(this._pageCurrent, this._idxCurrent, this._pageParent, this._idxParent);
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x0600429D RID: 17053 RVA: 0x0016BD34 File Offset: 0x00169F34
		public override XPathNodeType NodeType
		{
			get
			{
				return this._pageCurrent[this._idxCurrent].NodeType;
			}
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x0600429E RID: 17054 RVA: 0x0016BD4C File Offset: 0x00169F4C
		public override string LocalName
		{
			get
			{
				return this._pageCurrent[this._idxCurrent].LocalName;
			}
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x0600429F RID: 17055 RVA: 0x0016BD64 File Offset: 0x00169F64
		public override string NamespaceURI
		{
			get
			{
				return this._pageCurrent[this._idxCurrent].NamespaceUri;
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x060042A0 RID: 17056 RVA: 0x0016BD7C File Offset: 0x00169F7C
		public override string Name
		{
			get
			{
				return this._pageCurrent[this._idxCurrent].Name;
			}
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x060042A1 RID: 17057 RVA: 0x0016BD94 File Offset: 0x00169F94
		public override string Prefix
		{
			get
			{
				return this._pageCurrent[this._idxCurrent].Prefix;
			}
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x060042A2 RID: 17058 RVA: 0x0016BDAC File Offset: 0x00169FAC
		public override string BaseURI
		{
			get
			{
				XPathNode[] array;
				int num;
				if (this._idxParent != 0)
				{
					array = this._pageParent;
					num = this._idxParent;
				}
				else
				{
					array = this._pageCurrent;
					num = this._idxCurrent;
				}
				for (;;)
				{
					XPathNodeType nodeType = array[num].NodeType;
					if (nodeType <= XPathNodeType.Element || nodeType == XPathNodeType.ProcessingInstruction)
					{
						break;
					}
					num = array[num].GetParent(out array);
					if (num == 0)
					{
						goto Block_3;
					}
				}
				return array[num].BaseUri;
				Block_3:
				return string.Empty;
			}
		}

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x060042A3 RID: 17059 RVA: 0x0016BE18 File Offset: 0x0016A018
		public override bool IsEmptyElement
		{
			get
			{
				return this._pageCurrent[this._idxCurrent].AllowShortcutTag;
			}
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x060042A4 RID: 17060 RVA: 0x0016BE30 File Offset: 0x0016A030
		public override XmlNameTable NameTable
		{
			get
			{
				return this._pageCurrent[this._idxCurrent].Document.NameTable;
			}
		}

		// Token: 0x060042A5 RID: 17061 RVA: 0x0016BE50 File Offset: 0x0016A050
		public override bool MoveToFirstAttribute()
		{
			XPathNode[] pageCurrent = this._pageCurrent;
			int idxCurrent = this._idxCurrent;
			if (XPathNodeHelper.GetFirstAttribute(ref this._pageCurrent, ref this._idxCurrent))
			{
				this._pageParent = pageCurrent;
				this._idxParent = idxCurrent;
				return true;
			}
			return false;
		}

		// Token: 0x060042A6 RID: 17062 RVA: 0x0016BE8F File Offset: 0x0016A08F
		public override bool MoveToNextAttribute()
		{
			return XPathNodeHelper.GetNextAttribute(ref this._pageCurrent, ref this._idxCurrent);
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x060042A7 RID: 17063 RVA: 0x0016BEA2 File Offset: 0x0016A0A2
		public override bool HasAttributes
		{
			get
			{
				return this._pageCurrent[this._idxCurrent].HasAttribute;
			}
		}

		// Token: 0x060042A8 RID: 17064 RVA: 0x0016BEBC File Offset: 0x0016A0BC
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			XPathNode[] pageCurrent = this._pageCurrent;
			int idxCurrent = this._idxCurrent;
			if (localName != this._atomizedLocalName)
			{
				this._atomizedLocalName = ((localName != null) ? this.NameTable.Get(localName) : null);
			}
			if (XPathNodeHelper.GetAttribute(ref this._pageCurrent, ref this._idxCurrent, this._atomizedLocalName, namespaceURI))
			{
				this._pageParent = pageCurrent;
				this._idxParent = idxCurrent;
				return true;
			}
			return false;
		}

		// Token: 0x060042A9 RID: 17065 RVA: 0x0016BF24 File Offset: 0x0016A124
		public override bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope)
		{
			XPathNode[] array;
			int num;
			if (namespaceScope == XPathNamespaceScope.Local)
			{
				num = XPathNodeHelper.GetLocalNamespaces(this._pageCurrent, this._idxCurrent, out array);
			}
			else
			{
				num = XPathNodeHelper.GetInScopeNamespaces(this._pageCurrent, this._idxCurrent, out array);
			}
			while (num != 0)
			{
				if (namespaceScope != XPathNamespaceScope.ExcludeXml || !array[num].IsXmlNamespaceNode)
				{
					this._pageParent = this._pageCurrent;
					this._idxParent = this._idxCurrent;
					this._pageCurrent = array;
					this._idxCurrent = num;
					return true;
				}
				num = array[num].GetSibling(out array);
			}
			return false;
		}

		// Token: 0x060042AA RID: 17066 RVA: 0x0016BFB0 File Offset: 0x0016A1B0
		public override bool MoveToNextNamespace(XPathNamespaceScope scope)
		{
			XPathNode[] pageCurrent = this._pageCurrent;
			int num = this._idxCurrent;
			if (pageCurrent[num].NodeType != XPathNodeType.Namespace)
			{
				return false;
			}
			for (;;)
			{
				num = pageCurrent[num].GetSibling(out pageCurrent);
				if (num == 0)
				{
					break;
				}
				if (scope != XPathNamespaceScope.ExcludeXml)
				{
					goto Block_3;
				}
				if (!pageCurrent[num].IsXmlNamespaceNode)
				{
					goto IL_6A;
				}
			}
			return false;
			Block_3:
			XPathNode[] array;
			if (scope == XPathNamespaceScope.Local && (pageCurrent[num].GetParent(out array) != this._idxParent || array != this._pageParent))
			{
				return false;
			}
			IL_6A:
			this._pageCurrent = pageCurrent;
			this._idxCurrent = num;
			return true;
		}

		// Token: 0x060042AB RID: 17067 RVA: 0x0016C036 File Offset: 0x0016A236
		public override bool MoveToNext()
		{
			return XPathNodeHelper.GetContentSibling(ref this._pageCurrent, ref this._idxCurrent);
		}

		// Token: 0x060042AC RID: 17068 RVA: 0x0016C049 File Offset: 0x0016A249
		public override bool MoveToPrevious()
		{
			return this._idxParent == 0 && XPathNodeHelper.GetPreviousContentSibling(ref this._pageCurrent, ref this._idxCurrent);
		}

		// Token: 0x060042AD RID: 17069 RVA: 0x0016C068 File Offset: 0x0016A268
		public override bool MoveToFirstChild()
		{
			if (this._pageCurrent[this._idxCurrent].HasCollapsedText)
			{
				this._pageParent = this._pageCurrent;
				this._idxParent = this._idxCurrent;
				this._idxCurrent = this._pageCurrent[this._idxCurrent].Document.GetCollapsedTextNode(out this._pageCurrent);
				return true;
			}
			return XPathNodeHelper.GetContentChild(ref this._pageCurrent, ref this._idxCurrent);
		}

		// Token: 0x060042AE RID: 17070 RVA: 0x0016C0E0 File Offset: 0x0016A2E0
		public override bool MoveToParent()
		{
			if (this._idxParent != 0)
			{
				this._pageCurrent = this._pageParent;
				this._idxCurrent = this._idxParent;
				this._pageParent = null;
				this._idxParent = 0;
				return true;
			}
			return XPathNodeHelper.GetParent(ref this._pageCurrent, ref this._idxCurrent);
		}

		// Token: 0x060042AF RID: 17071 RVA: 0x0016C130 File Offset: 0x0016A330
		public override bool MoveTo(XPathNavigator other)
		{
			XPathDocumentNavigator xpathDocumentNavigator = other as XPathDocumentNavigator;
			if (xpathDocumentNavigator != null)
			{
				this._pageCurrent = xpathDocumentNavigator._pageCurrent;
				this._idxCurrent = xpathDocumentNavigator._idxCurrent;
				this._pageParent = xpathDocumentNavigator._pageParent;
				this._idxParent = xpathDocumentNavigator._idxParent;
				return true;
			}
			return false;
		}

		// Token: 0x060042B0 RID: 17072 RVA: 0x0016C17C File Offset: 0x0016A37C
		public override bool MoveToId(string id)
		{
			XPathNode[] pageCurrent;
			int num = this._pageCurrent[this._idxCurrent].Document.LookupIdElement(id, out pageCurrent);
			if (num != 0)
			{
				this._pageCurrent = pageCurrent;
				this._idxCurrent = num;
				this._pageParent = null;
				this._idxParent = 0;
				return true;
			}
			return false;
		}

		// Token: 0x060042B1 RID: 17073 RVA: 0x0016C1CC File Offset: 0x0016A3CC
		public override bool IsSamePosition(XPathNavigator other)
		{
			XPathDocumentNavigator xpathDocumentNavigator = other as XPathDocumentNavigator;
			return xpathDocumentNavigator != null && (this._idxCurrent == xpathDocumentNavigator._idxCurrent && this._pageCurrent == xpathDocumentNavigator._pageCurrent && this._idxParent == xpathDocumentNavigator._idxParent) && this._pageParent == xpathDocumentNavigator._pageParent;
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x060042B2 RID: 17074 RVA: 0x0016C21F File Offset: 0x0016A41F
		public override bool HasChildren
		{
			get
			{
				return this._pageCurrent[this._idxCurrent].HasContentChild;
			}
		}

		// Token: 0x060042B3 RID: 17075 RVA: 0x0016C237 File Offset: 0x0016A437
		public override void MoveToRoot()
		{
			if (this._idxParent != 0)
			{
				this._pageParent = null;
				this._idxParent = 0;
			}
			this._idxCurrent = this._pageCurrent[this._idxCurrent].GetRoot(out this._pageCurrent);
		}

		// Token: 0x060042B4 RID: 17076 RVA: 0x0016C271 File Offset: 0x0016A471
		public override bool MoveToChild(string localName, string namespaceURI)
		{
			if (localName != this._atomizedLocalName)
			{
				this._atomizedLocalName = ((localName != null) ? this.NameTable.Get(localName) : null);
			}
			return XPathNodeHelper.GetElementChild(ref this._pageCurrent, ref this._idxCurrent, this._atomizedLocalName, namespaceURI);
		}

		// Token: 0x060042B5 RID: 17077 RVA: 0x0016C2AC File Offset: 0x0016A4AC
		public override bool MoveToNext(string localName, string namespaceURI)
		{
			if (localName != this._atomizedLocalName)
			{
				this._atomizedLocalName = ((localName != null) ? this.NameTable.Get(localName) : null);
			}
			return XPathNodeHelper.GetElementSibling(ref this._pageCurrent, ref this._idxCurrent, this._atomizedLocalName, namespaceURI);
		}

		// Token: 0x060042B6 RID: 17078 RVA: 0x0016C2E8 File Offset: 0x0016A4E8
		public override bool MoveToChild(XPathNodeType type)
		{
			if (!this._pageCurrent[this._idxCurrent].HasCollapsedText)
			{
				return XPathNodeHelper.GetContentChild(ref this._pageCurrent, ref this._idxCurrent, type);
			}
			if (type != XPathNodeType.Text && type != XPathNodeType.All)
			{
				return false;
			}
			this._pageParent = this._pageCurrent;
			this._idxParent = this._idxCurrent;
			this._idxCurrent = this._pageCurrent[this._idxCurrent].Document.GetCollapsedTextNode(out this._pageCurrent);
			return true;
		}

		// Token: 0x060042B7 RID: 17079 RVA: 0x0016C36B File Offset: 0x0016A56B
		public override bool MoveToNext(XPathNodeType type)
		{
			return XPathNodeHelper.GetContentSibling(ref this._pageCurrent, ref this._idxCurrent, type);
		}

		// Token: 0x060042B8 RID: 17080 RVA: 0x0016C380 File Offset: 0x0016A580
		public override bool MoveToFollowing(string localName, string namespaceURI, XPathNavigator end)
		{
			if (localName != this._atomizedLocalName)
			{
				this._atomizedLocalName = ((localName != null) ? this.NameTable.Get(localName) : null);
			}
			XPathNode[] pageEnd;
			int followingEnd = this.GetFollowingEnd(end as XPathDocumentNavigator, false, out pageEnd);
			if (this._idxParent == 0)
			{
				return XPathNodeHelper.GetElementFollowing(ref this._pageCurrent, ref this._idxCurrent, pageEnd, followingEnd, this._atomizedLocalName, namespaceURI);
			}
			if (!XPathNodeHelper.GetElementFollowing(ref this._pageParent, ref this._idxParent, pageEnd, followingEnd, this._atomizedLocalName, namespaceURI))
			{
				return false;
			}
			this._pageCurrent = this._pageParent;
			this._idxCurrent = this._idxParent;
			this._pageParent = null;
			this._idxParent = 0;
			return true;
		}

		// Token: 0x060042B9 RID: 17081 RVA: 0x0016C428 File Offset: 0x0016A628
		public override bool MoveToFollowing(XPathNodeType type, XPathNavigator end)
		{
			XPathDocumentNavigator xpathDocumentNavigator = end as XPathDocumentNavigator;
			XPathNode[] array;
			int followingEnd;
			if (type == XPathNodeType.Text || type == XPathNodeType.All)
			{
				if (this._pageCurrent[this._idxCurrent].HasCollapsedText)
				{
					if (xpathDocumentNavigator != null && this._idxCurrent == xpathDocumentNavigator._idxParent && this._pageCurrent == xpathDocumentNavigator._pageParent)
					{
						return false;
					}
					this._pageParent = this._pageCurrent;
					this._idxParent = this._idxCurrent;
					this._idxCurrent = this._pageCurrent[this._idxCurrent].Document.GetCollapsedTextNode(out this._pageCurrent);
					return true;
				}
				else if (type == XPathNodeType.Text)
				{
					followingEnd = this.GetFollowingEnd(xpathDocumentNavigator, true, out array);
					XPathNode[] array2;
					int num;
					if (this._idxParent != 0)
					{
						array2 = this._pageParent;
						num = this._idxParent;
					}
					else
					{
						array2 = this._pageCurrent;
						num = this._idxCurrent;
					}
					if (xpathDocumentNavigator != null && xpathDocumentNavigator._idxParent != 0 && num == followingEnd && array2 == array)
					{
						return false;
					}
					if (!XPathNodeHelper.GetTextFollowing(ref array2, ref num, array, followingEnd))
					{
						return false;
					}
					if (array2[num].NodeType == XPathNodeType.Element)
					{
						this._idxCurrent = array2[num].Document.GetCollapsedTextNode(out this._pageCurrent);
						this._pageParent = array2;
						this._idxParent = num;
					}
					else
					{
						this._pageCurrent = array2;
						this._idxCurrent = num;
						this._pageParent = null;
						this._idxParent = 0;
					}
					return true;
				}
			}
			followingEnd = this.GetFollowingEnd(xpathDocumentNavigator, false, out array);
			if (this._idxParent == 0)
			{
				return XPathNodeHelper.GetContentFollowing(ref this._pageCurrent, ref this._idxCurrent, array, followingEnd, type);
			}
			if (!XPathNodeHelper.GetContentFollowing(ref this._pageParent, ref this._idxParent, array, followingEnd, type))
			{
				return false;
			}
			this._pageCurrent = this._pageParent;
			this._idxCurrent = this._idxParent;
			this._pageParent = null;
			this._idxParent = 0;
			return true;
		}

		// Token: 0x060042BA RID: 17082 RVA: 0x0016C5E5 File Offset: 0x0016A7E5
		public override XPathNodeIterator SelectChildren(XPathNodeType type)
		{
			return new XPathDocumentKindChildIterator(this, type);
		}

		// Token: 0x060042BB RID: 17083 RVA: 0x0016C5EE File Offset: 0x0016A7EE
		public override XPathNodeIterator SelectChildren(string name, string namespaceURI)
		{
			if (name == null || name.Length == 0)
			{
				return base.SelectChildren(name, namespaceURI);
			}
			return new XPathDocumentElementChildIterator(this, name, namespaceURI);
		}

		// Token: 0x060042BC RID: 17084 RVA: 0x0016C60C File Offset: 0x0016A80C
		public override XPathNodeIterator SelectDescendants(XPathNodeType type, bool matchSelf)
		{
			return new XPathDocumentKindDescendantIterator(this, type, matchSelf);
		}

		// Token: 0x060042BD RID: 17085 RVA: 0x0016C616 File Offset: 0x0016A816
		public override XPathNodeIterator SelectDescendants(string name, string namespaceURI, bool matchSelf)
		{
			if (name == null || name.Length == 0)
			{
				return base.SelectDescendants(name, namespaceURI, matchSelf);
			}
			return new XPathDocumentElementDescendantIterator(this, name, namespaceURI, matchSelf);
		}

		// Token: 0x060042BE RID: 17086 RVA: 0x0016C638 File Offset: 0x0016A838
		public override XmlNodeOrder ComparePosition(XPathNavigator other)
		{
			XPathDocumentNavigator xpathDocumentNavigator = other as XPathDocumentNavigator;
			if (xpathDocumentNavigator != null)
			{
				XPathDocument document = this._pageCurrent[this._idxCurrent].Document;
				XPathDocument document2 = xpathDocumentNavigator._pageCurrent[xpathDocumentNavigator._idxCurrent].Document;
				if (document == document2)
				{
					int num = this.GetPrimaryLocation();
					int num2 = xpathDocumentNavigator.GetPrimaryLocation();
					if (num == num2)
					{
						num = this.GetSecondaryLocation();
						num2 = xpathDocumentNavigator.GetSecondaryLocation();
						if (num == num2)
						{
							return XmlNodeOrder.Same;
						}
					}
					if (num >= num2)
					{
						return XmlNodeOrder.After;
					}
					return XmlNodeOrder.Before;
				}
			}
			return XmlNodeOrder.Unknown;
		}

		// Token: 0x060042BF RID: 17087 RVA: 0x0016C6B0 File Offset: 0x0016A8B0
		public override bool IsDescendant(XPathNavigator other)
		{
			XPathDocumentNavigator xpathDocumentNavigator = other as XPathDocumentNavigator;
			if (xpathDocumentNavigator != null)
			{
				XPathNode[] pageParent;
				int num;
				if (xpathDocumentNavigator._idxParent != 0)
				{
					pageParent = xpathDocumentNavigator._pageParent;
					num = xpathDocumentNavigator._idxParent;
				}
				else
				{
					num = xpathDocumentNavigator._pageCurrent[xpathDocumentNavigator._idxCurrent].GetParent(out pageParent);
				}
				while (num != 0)
				{
					if (num == this._idxCurrent && pageParent == this._pageCurrent)
					{
						return true;
					}
					num = pageParent[num].GetParent(out pageParent);
				}
			}
			return false;
		}

		// Token: 0x060042C0 RID: 17088 RVA: 0x0016C721 File Offset: 0x0016A921
		private int GetPrimaryLocation()
		{
			if (this._idxParent == 0)
			{
				return XPathNodeHelper.GetLocation(this._pageCurrent, this._idxCurrent);
			}
			return XPathNodeHelper.GetLocation(this._pageParent, this._idxParent);
		}

		// Token: 0x060042C1 RID: 17089 RVA: 0x0016C750 File Offset: 0x0016A950
		private int GetSecondaryLocation()
		{
			if (this._idxParent == 0)
			{
				return int.MinValue;
			}
			XPathNodeType nodeType = this._pageCurrent[this._idxCurrent].NodeType;
			if (nodeType == XPathNodeType.Attribute)
			{
				return XPathNodeHelper.GetLocation(this._pageCurrent, this._idxCurrent);
			}
			if (nodeType == XPathNodeType.Namespace)
			{
				return -2147483647 + XPathNodeHelper.GetLocation(this._pageCurrent, this._idxCurrent);
			}
			return int.MaxValue;
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x060042C2 RID: 17090 RVA: 0x0016C7BC File Offset: 0x0016A9BC
		internal override string UniqueId
		{
			get
			{
				char[] array = new char[16];
				int length = 0;
				array[length++] = XPathNavigator.NodeTypeLetter[(int)this._pageCurrent[this._idxCurrent].NodeType];
				int num;
				if (this._idxParent != 0)
				{
					num = (this._pageParent[0].PageInfo.PageNumber - 1 << 16 | this._idxParent - 1);
					do
					{
						array[length++] = XPathNavigator.UniqueIdTbl[num & 31];
						num >>= 5;
					}
					while (num != 0);
					array[length++] = '0';
				}
				num = (this._pageCurrent[0].PageInfo.PageNumber - 1 << 16 | this._idxCurrent - 1);
				do
				{
					array[length++] = XPathNavigator.UniqueIdTbl[num & 31];
					num >>= 5;
				}
				while (num != 0);
				return new string(array, 0, length);
			}
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x060042C3 RID: 17091 RVA: 0x00085B49 File Offset: 0x00083D49
		public override object UnderlyingObject
		{
			get
			{
				return this.Clone();
			}
		}

		// Token: 0x060042C4 RID: 17092 RVA: 0x0016C889 File Offset: 0x0016AA89
		public bool HasLineInfo()
		{
			return this._pageCurrent[this._idxCurrent].Document.HasLineInfo;
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x060042C5 RID: 17093 RVA: 0x0016C8A6 File Offset: 0x0016AAA6
		public int LineNumber
		{
			get
			{
				if (this._idxParent != 0 && this.NodeType == XPathNodeType.Text)
				{
					return this._pageParent[this._idxParent].LineNumber;
				}
				return this._pageCurrent[this._idxCurrent].LineNumber;
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x060042C6 RID: 17094 RVA: 0x0016C8E6 File Offset: 0x0016AAE6
		public int LinePosition
		{
			get
			{
				if (this._idxParent != 0 && this.NodeType == XPathNodeType.Text)
				{
					return this._pageParent[this._idxParent].CollapsedLinePosition;
				}
				return this._pageCurrent[this._idxCurrent].LinePosition;
			}
		}

		// Token: 0x060042C7 RID: 17095 RVA: 0x0016C926 File Offset: 0x0016AB26
		public int GetPositionHashCode()
		{
			return this._idxCurrent ^ this._idxParent;
		}

		// Token: 0x060042C8 RID: 17096 RVA: 0x0016C938 File Offset: 0x0016AB38
		public bool IsElementMatch(string localName, string namespaceURI)
		{
			if (localName != this._atomizedLocalName)
			{
				this._atomizedLocalName = ((localName != null) ? this.NameTable.Get(localName) : null);
			}
			return this._idxParent == 0 && this._pageCurrent[this._idxCurrent].ElementMatch(this._atomizedLocalName, namespaceURI);
		}

		// Token: 0x060042C9 RID: 17097 RVA: 0x0016C98D File Offset: 0x0016AB8D
		public bool IsKindMatch(XPathNodeType typ)
		{
			return (1 << (int)this._pageCurrent[this._idxCurrent].NodeType & XPathNavigator.GetKindMask(typ)) != 0;
		}

		// Token: 0x060042CA RID: 17098 RVA: 0x0016C9B4 File Offset: 0x0016ABB4
		private int GetFollowingEnd(XPathDocumentNavigator end, bool useParentOfVirtual, out XPathNode[] pageEnd)
		{
			if (end == null || this._pageCurrent[this._idxCurrent].Document != end._pageCurrent[end._idxCurrent].Document)
			{
				pageEnd = null;
				return 0;
			}
			if (end._idxParent == 0)
			{
				pageEnd = end._pageCurrent;
				return end._idxCurrent;
			}
			pageEnd = end._pageParent;
			if (!useParentOfVirtual)
			{
				return end._idxParent + 1;
			}
			return end._idxParent;
		}

		// Token: 0x04002F1F RID: 12063
		private XPathNode[] _pageCurrent;

		// Token: 0x04002F20 RID: 12064
		private XPathNode[] _pageParent;

		// Token: 0x04002F21 RID: 12065
		private int _idxCurrent;

		// Token: 0x04002F22 RID: 12066
		private int _idxParent;

		// Token: 0x04002F23 RID: 12067
		private string _atomizedLocalName;
	}
}
