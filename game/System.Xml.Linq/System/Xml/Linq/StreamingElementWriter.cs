using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml.Linq
{
	// Token: 0x02000048 RID: 72
	internal struct StreamingElementWriter
	{
		// Token: 0x06000272 RID: 626 RVA: 0x0000C871 File Offset: 0x0000AA71
		public StreamingElementWriter(XmlWriter w)
		{
			this._writer = w;
			this._element = null;
			this._attributes = new List<XAttribute>();
			this._resolver = default(NamespaceResolver);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000C898 File Offset: 0x0000AA98
		private void FlushElement()
		{
			if (this._element != null)
			{
				this.PushElement();
				XNamespace @namespace = this._element.Name.Namespace;
				this._writer.WriteStartElement(this.GetPrefixOfNamespace(@namespace, true), this._element.Name.LocalName, @namespace.NamespaceName);
				foreach (XAttribute xattribute in this._attributes)
				{
					@namespace = xattribute.Name.Namespace;
					string localName = xattribute.Name.LocalName;
					string namespaceName = @namespace.NamespaceName;
					this._writer.WriteAttributeString(this.GetPrefixOfNamespace(@namespace, false), localName, (namespaceName.Length == 0 && localName == "xmlns") ? "http://www.w3.org/2000/xmlns/" : namespaceName, xattribute.Value);
				}
				this._element = null;
				this._attributes.Clear();
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000C99C File Offset: 0x0000AB9C
		private string GetPrefixOfNamespace(XNamespace ns, bool allowDefaultNamespace)
		{
			string namespaceName = ns.NamespaceName;
			if (namespaceName.Length == 0)
			{
				return string.Empty;
			}
			string prefixOfNamespace = this._resolver.GetPrefixOfNamespace(ns, allowDefaultNamespace);
			if (prefixOfNamespace != null)
			{
				return prefixOfNamespace;
			}
			if (namespaceName == "http://www.w3.org/XML/1998/namespace")
			{
				return "xml";
			}
			if (namespaceName == "http://www.w3.org/2000/xmlns/")
			{
				return "xmlns";
			}
			return null;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000C9F0 File Offset: 0x0000ABF0
		private void PushElement()
		{
			this._resolver.PushScope();
			foreach (XAttribute xattribute in this._attributes)
			{
				if (xattribute.IsNamespaceDeclaration)
				{
					this._resolver.Add((xattribute.Name.NamespaceName.Length == 0) ? string.Empty : xattribute.Name.LocalName, XNamespace.Get(xattribute.Value));
				}
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000CA8C File Offset: 0x0000AC8C
		private void Write(object content)
		{
			if (content == null)
			{
				return;
			}
			XNode xnode = content as XNode;
			if (xnode != null)
			{
				this.WriteNode(xnode);
				return;
			}
			string text = content as string;
			if (text != null)
			{
				this.WriteString(text);
				return;
			}
			XAttribute xattribute = content as XAttribute;
			if (xattribute != null)
			{
				this.WriteAttribute(xattribute);
				return;
			}
			XStreamingElement xstreamingElement = content as XStreamingElement;
			if (xstreamingElement != null)
			{
				this.WriteStreamingElement(xstreamingElement);
				return;
			}
			object[] array = content as object[];
			if (array != null)
			{
				foreach (object content2 in array)
				{
					this.Write(content2);
				}
				return;
			}
			IEnumerable enumerable = content as IEnumerable;
			if (enumerable != null)
			{
				foreach (object content3 in enumerable)
				{
					this.Write(content3);
				}
				return;
			}
			this.WriteString(XContainer.GetStringValue(content));
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000CB80 File Offset: 0x0000AD80
		private void WriteAttribute(XAttribute a)
		{
			if (this._element == null)
			{
				throw new InvalidOperationException("An attribute cannot be written after content.");
			}
			this._attributes.Add(a);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000CBA1 File Offset: 0x0000ADA1
		private void WriteNode(XNode n)
		{
			this.FlushElement();
			n.WriteTo(this._writer);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000CBB5 File Offset: 0x0000ADB5
		internal void WriteStreamingElement(XStreamingElement e)
		{
			this.FlushElement();
			this._element = e;
			this.Write(e.content);
			this.FlushElement();
			this._writer.WriteEndElement();
			this._resolver.PopScope();
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000CBEC File Offset: 0x0000ADEC
		private void WriteString(string s)
		{
			this.FlushElement();
			this._writer.WriteString(s);
		}

		// Token: 0x0400016D RID: 365
		private XmlWriter _writer;

		// Token: 0x0400016E RID: 366
		private XStreamingElement _element;

		// Token: 0x0400016F RID: 367
		private List<XAttribute> _attributes;

		// Token: 0x04000170 RID: 368
		private NamespaceResolver _resolver;
	}
}
