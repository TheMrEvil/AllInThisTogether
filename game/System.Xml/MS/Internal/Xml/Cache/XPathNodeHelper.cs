using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000672 RID: 1650
	internal abstract class XPathNodeHelper
	{
		// Token: 0x060042F7 RID: 17143 RVA: 0x0016CECE File Offset: 0x0016B0CE
		public static int GetLocalNamespaces(XPathNode[] pageElem, int idxElem, out XPathNode[] pageNmsp)
		{
			if (pageElem[idxElem].HasNamespaceDecls)
			{
				return pageElem[idxElem].Document.LookupNamespaces(pageElem, idxElem, out pageNmsp);
			}
			pageNmsp = null;
			return 0;
		}

		// Token: 0x060042F8 RID: 17144 RVA: 0x0016CEF8 File Offset: 0x0016B0F8
		public static int GetInScopeNamespaces(XPathNode[] pageElem, int idxElem, out XPathNode[] pageNmsp)
		{
			if (pageElem[idxElem].NodeType == XPathNodeType.Element)
			{
				XPathDocument document = pageElem[idxElem].Document;
				while (!pageElem[idxElem].HasNamespaceDecls)
				{
					idxElem = pageElem[idxElem].GetParent(out pageElem);
					if (idxElem == 0)
					{
						return document.GetXmlNamespaceNode(out pageNmsp);
					}
				}
				return document.LookupNamespaces(pageElem, idxElem, out pageNmsp);
			}
			pageNmsp = null;
			return 0;
		}

		// Token: 0x060042F9 RID: 17145 RVA: 0x0016CF5A File Offset: 0x0016B15A
		public static bool GetFirstAttribute(ref XPathNode[] pageNode, ref int idxNode)
		{
			if (pageNode[idxNode].HasAttribute)
			{
				XPathNodeHelper.GetChild(ref pageNode, ref idxNode);
				return true;
			}
			return false;
		}

		// Token: 0x060042FA RID: 17146 RVA: 0x0016CF78 File Offset: 0x0016B178
		public static bool GetNextAttribute(ref XPathNode[] pageNode, ref int idxNode)
		{
			XPathNode[] array;
			int sibling = pageNode[idxNode].GetSibling(out array);
			if (sibling != 0 && array[sibling].NodeType == XPathNodeType.Attribute)
			{
				pageNode = array;
				idxNode = sibling;
				return true;
			}
			return false;
		}

		// Token: 0x060042FB RID: 17147 RVA: 0x0016CFB4 File Offset: 0x0016B1B4
		public static bool GetContentChild(ref XPathNode[] pageNode, ref int idxNode)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			if (array[num].HasContentChild)
			{
				XPathNodeHelper.GetChild(ref array, ref num);
				while (array[num].NodeType == XPathNodeType.Attribute)
				{
					num = array[num].GetSibling(out array);
				}
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x060042FC RID: 17148 RVA: 0x0016D008 File Offset: 0x0016B208
		public static bool GetContentSibling(ref XPathNode[] pageNode, ref int idxNode)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			if (!array[num].IsAttrNmsp)
			{
				num = array[num].GetSibling(out array);
				if (num != 0)
				{
					pageNode = array;
					idxNode = num;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060042FD RID: 17149 RVA: 0x0016D044 File Offset: 0x0016B244
		public static bool GetParent(ref XPathNode[] pageNode, ref int idxNode)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			num = array[num].GetParent(out array);
			if (num != 0)
			{
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x060042FE RID: 17150 RVA: 0x0016D072 File Offset: 0x0016B272
		public static int GetLocation(XPathNode[] pageNode, int idxNode)
		{
			return pageNode[0].PageInfo.PageNumber << 16 | idxNode;
		}

		// Token: 0x060042FF RID: 17151 RVA: 0x0016D08C File Offset: 0x0016B28C
		public static bool GetElementChild(ref XPathNode[] pageNode, ref int idxNode, string localName, string namespaceName)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			if (array[num].HasElementChild)
			{
				XPathNodeHelper.GetChild(ref array, ref num);
				while (!array[num].ElementMatch(localName, namespaceName))
				{
					num = array[num].GetSibling(out array);
					if (num == 0)
					{
						return false;
					}
				}
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x06004300 RID: 17152 RVA: 0x0016D0E4 File Offset: 0x0016B2E4
		public static bool GetElementSibling(ref XPathNode[] pageNode, ref int idxNode, string localName, string namespaceName)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			if (array[num].NodeType != XPathNodeType.Attribute)
			{
				do
				{
					num = array[num].GetSibling(out array);
					if (num == 0)
					{
						return false;
					}
				}
				while (!array[num].ElementMatch(localName, namespaceName));
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x06004301 RID: 17153 RVA: 0x0016D134 File Offset: 0x0016B334
		public static bool GetContentChild(ref XPathNode[] pageNode, ref int idxNode, XPathNodeType typ)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			if (array[num].HasContentChild)
			{
				int contentKindMask = XPathNavigator.GetContentKindMask(typ);
				XPathNodeHelper.GetChild(ref array, ref num);
				while ((1 << (int)array[num].NodeType & contentKindMask) == 0)
				{
					num = array[num].GetSibling(out array);
					if (num == 0)
					{
						return false;
					}
				}
				if (typ == XPathNodeType.Attribute)
				{
					return false;
				}
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x06004302 RID: 17154 RVA: 0x0016D19C File Offset: 0x0016B39C
		public static bool GetContentSibling(ref XPathNode[] pageNode, ref int idxNode, XPathNodeType typ)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			int contentKindMask = XPathNavigator.GetContentKindMask(typ);
			if (array[num].NodeType != XPathNodeType.Attribute)
			{
				do
				{
					num = array[num].GetSibling(out array);
					if (num == 0)
					{
						return false;
					}
				}
				while ((1 << (int)array[num].NodeType & contentKindMask) == 0);
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x06004303 RID: 17155 RVA: 0x0016D1F8 File Offset: 0x0016B3F8
		public static bool GetPreviousContentSibling(ref XPathNode[] pageNode, ref int idxNode)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			num = array[num].GetParent(out array);
			if (num != 0)
			{
				int num2 = idxNode - 1;
				XPathNode[] array2;
				if (num2 == 0)
				{
					array2 = pageNode[0].PageInfo.PreviousPage;
					num2 = array2.Length - 1;
				}
				else
				{
					array2 = pageNode;
				}
				if (num == num2 && array == array2)
				{
					return false;
				}
				XPathNode[] array3 = array2;
				int num3 = num2;
				do
				{
					array2 = array3;
					num2 = num3;
					num3 = array3[num3].GetParent(out array3);
				}
				while (num3 != num || array3 != array);
				if (array2[num2].NodeType != XPathNodeType.Attribute)
				{
					pageNode = array2;
					idxNode = num2;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004304 RID: 17156 RVA: 0x0016D294 File Offset: 0x0016B494
		public static bool GetAttribute(ref XPathNode[] pageNode, ref int idxNode, string localName, string namespaceName)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			if (array[num].HasAttribute)
			{
				XPathNodeHelper.GetChild(ref array, ref num);
				while (!array[num].NameMatch(localName, namespaceName))
				{
					num = array[num].GetSibling(out array);
					if (num == 0 || array[num].NodeType != XPathNodeType.Attribute)
					{
						return false;
					}
				}
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x06004305 RID: 17157 RVA: 0x0016D2F8 File Offset: 0x0016B4F8
		public static bool GetElementFollowing(ref XPathNode[] pageCurrent, ref int idxCurrent, XPathNode[] pageEnd, int idxEnd, string localName, string namespaceName)
		{
			XPathNode[] array = pageCurrent;
			int i = idxCurrent;
			if (array[i].NodeType != XPathNodeType.Element || array[i].LocalName != localName)
			{
				i++;
				while (array != pageEnd || i > idxEnd)
				{
					while (i < array[0].PageInfo.NodeCount)
					{
						if (array[i].ElementMatch(localName, namespaceName))
						{
							goto IL_12C;
						}
						i++;
					}
					array = array[0].PageInfo.NextPage;
					i = 1;
					if (array == null)
					{
						return false;
					}
				}
				while (i != idxEnd)
				{
					if (array[i].ElementMatch(localName, namespaceName))
					{
						goto IL_12C;
					}
					i++;
				}
				return false;
			}
			int num = 0;
			if (pageEnd != null)
			{
				num = pageEnd[0].PageInfo.PageNumber;
				int pageNumber = array[0].PageInfo.PageNumber;
				if (pageNumber > num || (pageNumber == num && i >= idxEnd))
				{
					pageEnd = null;
				}
			}
			do
			{
				i = array[i].GetSimilarElement(out array);
				if (i == 0)
				{
					return false;
				}
				if (pageEnd != null)
				{
					int pageNumber = array[0].PageInfo.PageNumber;
					if (pageNumber > num || (pageNumber == num && i >= idxEnd))
					{
						return false;
					}
				}
			}
			while (array[i].LocalName != localName || !(array[i].NamespaceUri == namespaceName));
			IL_12C:
			pageCurrent = array;
			idxCurrent = i;
			return true;
		}

		// Token: 0x06004306 RID: 17158 RVA: 0x0016D438 File Offset: 0x0016B638
		public static bool GetContentFollowing(ref XPathNode[] pageCurrent, ref int idxCurrent, XPathNode[] pageEnd, int idxEnd, XPathNodeType typ)
		{
			XPathNode[] array = pageCurrent;
			int i = idxCurrent;
			int contentKindMask = XPathNavigator.GetContentKindMask(typ);
			i++;
			while (array != pageEnd || i > idxEnd)
			{
				while (i < array[0].PageInfo.NodeCount)
				{
					if ((1 << (int)array[i].NodeType & contentKindMask) != 0)
					{
						goto IL_81;
					}
					i++;
				}
				array = array[0].PageInfo.NextPage;
				i = 1;
				if (array == null)
				{
					return false;
				}
				continue;
				IL_81:
				pageCurrent = array;
				idxCurrent = i;
				return true;
			}
			while (i != idxEnd)
			{
				if ((1 << (int)array[i].NodeType & contentKindMask) != 0)
				{
					goto IL_81;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06004307 RID: 17159 RVA: 0x0016D4D0 File Offset: 0x0016B6D0
		public static bool GetTextFollowing(ref XPathNode[] pageCurrent, ref int idxCurrent, XPathNode[] pageEnd, int idxEnd)
		{
			XPathNode[] array = pageCurrent;
			int i = idxCurrent;
			i++;
			while (array != pageEnd || i > idxEnd)
			{
				while (i < array[0].PageInfo.NodeCount)
				{
					if (array[i].IsText || (array[i].NodeType == XPathNodeType.Element && array[i].HasCollapsedText))
					{
						goto IL_AB;
					}
					i++;
				}
				array = array[0].PageInfo.NextPage;
				i = 1;
				if (array == null)
				{
					return false;
				}
				continue;
				IL_AB:
				pageCurrent = array;
				idxCurrent = i;
				return true;
			}
			while (i != idxEnd)
			{
				if (array[i].IsText || (array[i].NodeType == XPathNodeType.Element && array[i].HasCollapsedText))
				{
					goto IL_AB;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06004308 RID: 17160 RVA: 0x0016D590 File Offset: 0x0016B790
		public static bool GetNonDescendant(ref XPathNode[] pageNode, ref int idxNode)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			while (!array[num].HasSibling)
			{
				num = array[num].GetParent(out array);
				if (num == 0)
				{
					return false;
				}
			}
			pageNode = array;
			idxNode = array[num].GetSibling(out pageNode);
			return true;
		}

		// Token: 0x06004309 RID: 17161 RVA: 0x0016D5D8 File Offset: 0x0016B7D8
		private static void GetChild(ref XPathNode[] pageNode, ref int idxNode)
		{
			int num = idxNode + 1;
			idxNode = num;
			if (num >= pageNode.Length)
			{
				pageNode = pageNode[0].PageInfo.NextPage;
				idxNode = 1;
			}
		}

		// Token: 0x0600430A RID: 17162 RVA: 0x0000216B File Offset: 0x0000036B
		protected XPathNodeHelper()
		{
		}
	}
}
