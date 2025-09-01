using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200003C RID: 60
	internal class ExcAncestralNamespaceContextManager : AncestralNamespaceContextManager
	{
		// Token: 0x06000190 RID: 400 RVA: 0x00007B19 File Offset: 0x00005D19
		internal ExcAncestralNamespaceContextManager(string inclusiveNamespacesPrefixList)
		{
			this._inclusivePrefixSet = Utils.TokenizePrefixListString(inclusiveNamespacesPrefixList);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00007B30 File Offset: 0x00005D30
		private bool HasNonRedundantInclusivePrefix(XmlAttribute attr)
		{
			string namespacePrefix = Utils.GetNamespacePrefix(attr);
			int num;
			return this._inclusivePrefixSet.ContainsKey(namespacePrefix) && Utils.IsNonRedundantNamespaceDecl(attr, base.GetNearestRenderedNamespaceWithMatchingPrefix(namespacePrefix, out num));
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00007B64 File Offset: 0x00005D64
		private void GatherNamespaceToRender(string nsPrefix, SortedList nsListToRender, Hashtable nsLocallyDeclared)
		{
			using (IEnumerator enumerator = nsListToRender.GetKeyList().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (Utils.HasNamespacePrefix((XmlAttribute)enumerator.Current, nsPrefix))
					{
						return;
					}
				}
			}
			XmlAttribute xmlAttribute = (XmlAttribute)nsLocallyDeclared[nsPrefix];
			int num;
			XmlAttribute nearestRenderedNamespaceWithMatchingPrefix = base.GetNearestRenderedNamespaceWithMatchingPrefix(nsPrefix, out num);
			if (xmlAttribute != null)
			{
				if (Utils.IsNonRedundantNamespaceDecl(xmlAttribute, nearestRenderedNamespaceWithMatchingPrefix))
				{
					nsLocallyDeclared.Remove(nsPrefix);
					nsListToRender.Add(xmlAttribute, null);
					return;
				}
			}
			else
			{
				int num2;
				XmlAttribute nearestUnrenderedNamespaceWithMatchingPrefix = base.GetNearestUnrenderedNamespaceWithMatchingPrefix(nsPrefix, out num2);
				if (nearestUnrenderedNamespaceWithMatchingPrefix != null && num2 > num && Utils.IsNonRedundantNamespaceDecl(nearestUnrenderedNamespaceWithMatchingPrefix, nearestRenderedNamespaceWithMatchingPrefix))
				{
					nsListToRender.Add(nearestUnrenderedNamespaceWithMatchingPrefix, null);
				}
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00007C1C File Offset: 0x00005E1C
		internal override void GetNamespacesToRender(XmlElement element, SortedList attrListToRender, SortedList nsListToRender, Hashtable nsLocallyDeclared)
		{
			this.GatherNamespaceToRender(element.Prefix, nsListToRender, nsLocallyDeclared);
			foreach (object obj in attrListToRender.GetKeyList())
			{
				string prefix = ((XmlAttribute)obj).Prefix;
				if (prefix.Length > 0)
				{
					this.GatherNamespaceToRender(prefix, nsListToRender, nsLocallyDeclared);
				}
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00007C98 File Offset: 0x00005E98
		internal override void TrackNamespaceNode(XmlAttribute attr, SortedList nsListToRender, Hashtable nsLocallyDeclared)
		{
			if (!Utils.IsXmlPrefixDefinitionNode(attr))
			{
				if (this.HasNonRedundantInclusivePrefix(attr))
				{
					nsListToRender.Add(attr, null);
					return;
				}
				nsLocallyDeclared.Add(Utils.GetNamespacePrefix(attr), attr);
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00007CC1 File Offset: 0x00005EC1
		internal override void TrackXmlNamespaceNode(XmlAttribute attr, SortedList nsListToRender, SortedList attrListToRender, Hashtable nsLocallyDeclared)
		{
			attrListToRender.Add(attr, null);
		}

		// Token: 0x040001A1 RID: 417
		private Hashtable _inclusivePrefixSet;
	}
}
