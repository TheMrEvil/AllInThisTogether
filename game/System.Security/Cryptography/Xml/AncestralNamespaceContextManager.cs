using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200001A RID: 26
	internal abstract class AncestralNamespaceContextManager
	{
		// Token: 0x06000065 RID: 101 RVA: 0x000030FD File Offset: 0x000012FD
		internal NamespaceFrame GetScopeAt(int i)
		{
			return (NamespaceFrame)this._ancestorStack[i];
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003110 File Offset: 0x00001310
		internal NamespaceFrame GetCurrentScope()
		{
			return this.GetScopeAt(this._ancestorStack.Count - 1);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003128 File Offset: 0x00001328
		protected XmlAttribute GetNearestRenderedNamespaceWithMatchingPrefix(string nsPrefix, out int depth)
		{
			depth = -1;
			for (int i = this._ancestorStack.Count - 1; i >= 0; i--)
			{
				XmlAttribute rendered;
				if ((rendered = this.GetScopeAt(i).GetRendered(nsPrefix)) != null)
				{
					depth = i;
					return rendered;
				}
			}
			return null;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000316C File Offset: 0x0000136C
		protected XmlAttribute GetNearestUnrenderedNamespaceWithMatchingPrefix(string nsPrefix, out int depth)
		{
			depth = -1;
			for (int i = this._ancestorStack.Count - 1; i >= 0; i--)
			{
				XmlAttribute unrendered;
				if ((unrendered = this.GetScopeAt(i).GetUnrendered(nsPrefix)) != null)
				{
					depth = i;
					return unrendered;
				}
			}
			return null;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000031AD File Offset: 0x000013AD
		internal void EnterElementContext()
		{
			this._ancestorStack.Add(new NamespaceFrame());
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000031C0 File Offset: 0x000013C0
		internal void ExitElementContext()
		{
			this._ancestorStack.RemoveAt(this._ancestorStack.Count - 1);
		}

		// Token: 0x0600006B RID: 107
		internal abstract void TrackNamespaceNode(XmlAttribute attr, SortedList nsListToRender, Hashtable nsLocallyDeclared);

		// Token: 0x0600006C RID: 108
		internal abstract void TrackXmlNamespaceNode(XmlAttribute attr, SortedList nsListToRender, SortedList attrListToRender, Hashtable nsLocallyDeclared);

		// Token: 0x0600006D RID: 109
		internal abstract void GetNamespacesToRender(XmlElement element, SortedList attrListToRender, SortedList nsListToRender, Hashtable nsLocallyDeclared);

		// Token: 0x0600006E RID: 110 RVA: 0x000031DC File Offset: 0x000013DC
		internal void LoadUnrenderedNamespaces(Hashtable nsLocallyDeclared)
		{
			object[] array = new object[nsLocallyDeclared.Count];
			nsLocallyDeclared.Values.CopyTo(array, 0);
			foreach (object obj in array)
			{
				this.AddUnrendered((XmlAttribute)obj);
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003224 File Offset: 0x00001424
		internal void LoadRenderedNamespaces(SortedList nsRenderedList)
		{
			foreach (object obj in nsRenderedList.GetKeyList())
			{
				this.AddRendered((XmlAttribute)obj);
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003280 File Offset: 0x00001480
		internal void AddRendered(XmlAttribute attr)
		{
			this.GetCurrentScope().AddRendered(attr);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000328E File Offset: 0x0000148E
		internal void AddUnrendered(XmlAttribute attr)
		{
			this.GetCurrentScope().AddUnrendered(attr);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000329C File Offset: 0x0000149C
		protected AncestralNamespaceContextManager()
		{
		}

		// Token: 0x04000140 RID: 320
		internal ArrayList _ancestorStack = new ArrayList();
	}
}
