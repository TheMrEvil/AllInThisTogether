using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200047A RID: 1146
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class XmlQueryOutput : XmlWriter
	{
		// Token: 0x06002C3D RID: 11325 RVA: 0x0010668A File Offset: 0x0010488A
		internal XmlQueryOutput(XmlQueryRuntime runtime, XmlSequenceWriter seqwrt)
		{
			this.runtime = runtime;
			this.seqwrt = seqwrt;
			this.xstate = XmlState.WithinSequence;
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x001066B2 File Offset: 0x001048B2
		internal XmlQueryOutput(XmlQueryRuntime runtime, XmlEventCache xwrt)
		{
			this.runtime = runtime;
			this.xwrt = xwrt;
			this.xstate = XmlState.WithinContent;
			this.depth = 1;
			this.rootType = XPathNodeType.Root;
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06002C3F RID: 11327 RVA: 0x001066E8 File Offset: 0x001048E8
		internal XmlSequenceWriter SequenceWriter
		{
			get
			{
				return this.seqwrt;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06002C40 RID: 11328 RVA: 0x001066F0 File Offset: 0x001048F0
		// (set) Token: 0x06002C41 RID: 11329 RVA: 0x001066F8 File Offset: 0x001048F8
		internal XmlRawWriter Writer
		{
			get
			{
				return this.xwrt;
			}
			set
			{
				IRemovableWriter removableWriter = value as IRemovableWriter;
				if (removableWriter != null)
				{
					removableWriter.OnRemoveWriterEvent = new OnRemoveWriter(this.SetWrappedWriter);
				}
				this.xwrt = value;
			}
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x00106728 File Offset: 0x00104928
		private void SetWrappedWriter(XmlRawWriter writer)
		{
			if (this.Writer is XmlAttributeCache)
			{
				this.attrCache = (XmlAttributeCache)this.Writer;
			}
			this.Writer = writer;
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public override void WriteStartDocument()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public override void WriteStartDocument(bool standalone)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public override void WriteEndDocument()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x00106750 File Offset: 0x00104950
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.ConstructWithinContent(XPathNodeType.Element);
			this.WriteStartElementUnchecked(prefix, localName, ns);
			this.WriteNamespaceDeclarationUnchecked(prefix, ns);
			if (this.attrCache == null)
			{
				this.attrCache = new XmlAttributeCache();
			}
			this.attrCache.Init(this.Writer);
			this.Writer = this.attrCache;
			this.attrCache = null;
			this.PushElementNames(prefix, localName, ns);
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x001067B8 File Offset: 0x001049B8
		public override void WriteEndElement()
		{
			if (this.xstate == XmlState.EnumAttrs)
			{
				this.StartElementContentUnchecked();
			}
			string prefix;
			string localName;
			string ns;
			this.PopElementNames(out prefix, out localName, out ns);
			this.WriteEndElementUnchecked(prefix, localName, ns);
			if (this.depth == 0)
			{
				this.EndTree();
			}
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x00065A63 File Offset: 0x00063C63
		public override void WriteFullEndElement()
		{
			this.WriteEndElement();
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x001067F8 File Offset: 0x001049F8
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (prefix.Length == 5 && prefix == "xmlns")
			{
				this.WriteStartNamespace(localName);
				return;
			}
			this.ConstructInEnumAttrs(XPathNodeType.Attribute);
			if (ns.Length != 0 && this.depth != 0)
			{
				prefix = this.CheckAttributePrefix(prefix, ns);
			}
			this.WriteStartAttributeUnchecked(prefix, localName, ns);
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x0010684D File Offset: 0x00104A4D
		public override void WriteEndAttribute()
		{
			if (this.xstate == XmlState.WithinNmsp)
			{
				this.WriteEndNamespace();
				return;
			}
			this.WriteEndAttributeUnchecked();
			if (this.depth == 0)
			{
				this.EndTree();
			}
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x00106873 File Offset: 0x00104A73
		public override void WriteComment(string text)
		{
			this.WriteStartComment();
			this.WriteCommentString(text);
			this.WriteEndComment();
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x00106888 File Offset: 0x00104A88
		public override void WriteProcessingInstruction(string target, string text)
		{
			this.WriteStartProcessingInstruction(target);
			this.WriteProcessingInstructionString(text);
			this.WriteEndProcessingInstruction();
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public override void WriteEntityRef(string name)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public override void WriteCharEntity(char ch)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public override void WriteWhitespace(string ws)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x0010689E File Offset: 0x00104A9E
		public override void WriteString(string text)
		{
			this.WriteString(text, false);
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public override void WriteChars(char[] buffer, int index, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x001068A8 File Offset: 0x00104AA8
		public override void WriteRaw(string data)
		{
			this.WriteString(data, true);
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x0010689E File Offset: 0x00104A9E
		public override void WriteCData(string text)
		{
			this.WriteString(text, false);
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06002C58 RID: 11352 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public override WriteState WriteState
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x0000B528 File Offset: 0x00009728
		public override void Close()
		{
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x0000B528 File Offset: 0x00009728
		public override void Flush()
		{
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public override string LookupPrefix(string ns)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06002C5C RID: 11356 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public override XmlSpace XmlSpace
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06002C5D RID: 11357 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public override string XmlLang
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x001068B2 File Offset: 0x00104AB2
		public void StartTree(XPathNodeType rootType)
		{
			this.Writer = this.seqwrt.StartTree(rootType, this.nsmgr, this.runtime.NameTable);
			this.rootType = rootType;
			this.xstate = ((rootType == XPathNodeType.Attribute || rootType == XPathNodeType.Namespace) ? XmlState.EnumAttrs : XmlState.WithinContent);
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x001068F0 File Offset: 0x00104AF0
		public void EndTree()
		{
			this.seqwrt.EndTree();
			this.xstate = XmlState.WithinSequence;
			this.Writer = null;
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x0010690C File Offset: 0x00104B0C
		public void WriteStartElementUnchecked(string prefix, string localName, string ns)
		{
			if (this.nsmgr != null)
			{
				this.nsmgr.PushScope();
			}
			this.Writer.WriteStartElement(prefix, localName, ns);
			this.usedPrefixes.Clear();
			this.usedPrefixes[prefix] = ns;
			this.xstate = XmlState.EnumAttrs;
			this.depth++;
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x00106967 File Offset: 0x00104B67
		public void WriteStartElementUnchecked(string localName)
		{
			this.WriteStartElementUnchecked(string.Empty, localName, string.Empty);
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x0010697A File Offset: 0x00104B7A
		public void StartElementContentUnchecked()
		{
			if (this.cntNmsp != 0)
			{
				this.WriteCachedNamespaces();
			}
			this.Writer.StartElementContent();
			this.xstate = XmlState.WithinContent;
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x0010699C File Offset: 0x00104B9C
		public void WriteEndElementUnchecked(string prefix, string localName, string ns)
		{
			this.Writer.WriteEndElement(prefix, localName, ns);
			this.xstate = XmlState.WithinContent;
			this.depth--;
			if (this.nsmgr != null)
			{
				this.nsmgr.PopScope();
			}
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x001069D5 File Offset: 0x00104BD5
		public void WriteEndElementUnchecked(string localName)
		{
			this.WriteEndElementUnchecked(string.Empty, localName, string.Empty);
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x001069E8 File Offset: 0x00104BE8
		public void WriteStartAttributeUnchecked(string prefix, string localName, string ns)
		{
			this.Writer.WriteStartAttribute(prefix, localName, ns);
			this.xstate = XmlState.WithinAttr;
			this.depth++;
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x00106A0D File Offset: 0x00104C0D
		public void WriteStartAttributeUnchecked(string localName)
		{
			this.WriteStartAttributeUnchecked(string.Empty, localName, string.Empty);
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x00106A20 File Offset: 0x00104C20
		public void WriteEndAttributeUnchecked()
		{
			this.Writer.WriteEndAttribute();
			this.xstate = XmlState.EnumAttrs;
			this.depth--;
		}

		// Token: 0x06002C68 RID: 11368 RVA: 0x00106A44 File Offset: 0x00104C44
		public void WriteNamespaceDeclarationUnchecked(string prefix, string ns)
		{
			if (this.depth == 0)
			{
				this.Writer.WriteNamespaceDeclaration(prefix, ns);
				return;
			}
			if (this.nsmgr == null)
			{
				if (ns.Length == 0 && prefix.Length == 0)
				{
					return;
				}
				this.nsmgr = new XmlNamespaceManager(this.runtime.NameTable);
				this.nsmgr.PushScope();
			}
			if (this.nsmgr.LookupNamespace(prefix) != ns)
			{
				this.AddNamespace(prefix, ns);
			}
			this.usedPrefixes[prefix] = ns;
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x00106ACA File Offset: 0x00104CCA
		public void WriteStringUnchecked(string text)
		{
			this.Writer.WriteString(text);
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x00106AD8 File Offset: 0x00104CD8
		public void WriteRawUnchecked(string text)
		{
			this.Writer.WriteRaw(text);
		}

		// Token: 0x06002C6B RID: 11371 RVA: 0x00106AE6 File Offset: 0x00104CE6
		public void WriteStartRoot()
		{
			if (this.xstate != XmlState.WithinSequence)
			{
				this.ThrowInvalidStateError(XPathNodeType.Root);
			}
			this.StartTree(XPathNodeType.Root);
			this.depth++;
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x00106B0C File Offset: 0x00104D0C
		public void WriteEndRoot()
		{
			this.depth--;
			this.EndTree();
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x00106B22 File Offset: 0x00104D22
		public void WriteStartElementLocalName(string localName)
		{
			this.WriteStartElement(string.Empty, localName, string.Empty);
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x00106B35 File Offset: 0x00104D35
		public void WriteStartAttributeLocalName(string localName)
		{
			this.WriteStartAttribute(string.Empty, localName, string.Empty);
		}

		// Token: 0x06002C6F RID: 11375 RVA: 0x00106B48 File Offset: 0x00104D48
		public void WriteStartElementComputed(string tagName, int prefixMappingsIndex)
		{
			this.WriteStartComputed(XPathNodeType.Element, tagName, prefixMappingsIndex);
		}

		// Token: 0x06002C70 RID: 11376 RVA: 0x00106B53 File Offset: 0x00104D53
		public void WriteStartElementComputed(string tagName, string ns)
		{
			this.WriteStartComputed(XPathNodeType.Element, tagName, ns);
		}

		// Token: 0x06002C71 RID: 11377 RVA: 0x00106B5E File Offset: 0x00104D5E
		public void WriteStartElementComputed(XPathNavigator navigator)
		{
			this.WriteStartComputed(XPathNodeType.Element, navigator);
		}

		// Token: 0x06002C72 RID: 11378 RVA: 0x00106B68 File Offset: 0x00104D68
		public void WriteStartElementComputed(XmlQualifiedName name)
		{
			this.WriteStartComputed(XPathNodeType.Element, name);
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x00106B72 File Offset: 0x00104D72
		public void WriteStartAttributeComputed(string tagName, int prefixMappingsIndex)
		{
			this.WriteStartComputed(XPathNodeType.Attribute, tagName, prefixMappingsIndex);
		}

		// Token: 0x06002C74 RID: 11380 RVA: 0x00106B7D File Offset: 0x00104D7D
		public void WriteStartAttributeComputed(string tagName, string ns)
		{
			this.WriteStartComputed(XPathNodeType.Attribute, tagName, ns);
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x00106B88 File Offset: 0x00104D88
		public void WriteStartAttributeComputed(XPathNavigator navigator)
		{
			this.WriteStartComputed(XPathNodeType.Attribute, navigator);
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x00106B92 File Offset: 0x00104D92
		public void WriteStartAttributeComputed(XmlQualifiedName name)
		{
			this.WriteStartComputed(XPathNodeType.Attribute, name);
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x00106B9C File Offset: 0x00104D9C
		public void WriteNamespaceDeclaration(string prefix, string ns)
		{
			this.ConstructInEnumAttrs(XPathNodeType.Namespace);
			if (this.nsmgr == null)
			{
				this.WriteNamespaceDeclarationUnchecked(prefix, ns);
			}
			else
			{
				string text = this.nsmgr.LookupNamespace(prefix);
				if (ns != text)
				{
					if (text != null && this.usedPrefixes.ContainsKey(prefix))
					{
						throw new XslTransformException("Cannot construct namespace declaration xmlns{0}{1}='{2}'. Prefix '{1}' is already mapped to namespace '{3}'.", new string[]
						{
							(prefix.Length == 0) ? "" : ":",
							prefix,
							ns,
							text
						});
					}
					this.AddNamespace(prefix, ns);
				}
			}
			if (this.depth == 0)
			{
				this.EndTree();
			}
			this.usedPrefixes[prefix] = ns;
		}

		// Token: 0x06002C78 RID: 11384 RVA: 0x00106C40 File Offset: 0x00104E40
		public void WriteStartNamespace(string prefix)
		{
			this.ConstructInEnumAttrs(XPathNodeType.Namespace);
			this.piTarget = prefix;
			this.nodeText.Clear();
			this.xstate = XmlState.WithinNmsp;
			this.depth++;
		}

		// Token: 0x06002C79 RID: 11385 RVA: 0x00106C70 File Offset: 0x00104E70
		public void WriteNamespaceString(string text)
		{
			this.nodeText.ConcatNoDelimiter(text);
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x00106C7E File Offset: 0x00104E7E
		public void WriteEndNamespace()
		{
			this.xstate = XmlState.EnumAttrs;
			this.depth--;
			this.WriteNamespaceDeclaration(this.piTarget, this.nodeText.GetResult());
			if (this.depth == 0)
			{
				this.EndTree();
			}
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x00106CBA File Offset: 0x00104EBA
		public void WriteStartComment()
		{
			this.ConstructWithinContent(XPathNodeType.Comment);
			this.nodeText.Clear();
			this.xstate = XmlState.WithinComment;
			this.depth++;
		}

		// Token: 0x06002C7C RID: 11388 RVA: 0x00106C70 File Offset: 0x00104E70
		public void WriteCommentString(string text)
		{
			this.nodeText.ConcatNoDelimiter(text);
		}

		// Token: 0x06002C7D RID: 11389 RVA: 0x00106CE3 File Offset: 0x00104EE3
		public void WriteEndComment()
		{
			this.Writer.WriteComment(this.nodeText.GetResult());
			this.xstate = XmlState.WithinContent;
			this.depth--;
			if (this.depth == 0)
			{
				this.EndTree();
			}
		}

		// Token: 0x06002C7E RID: 11390 RVA: 0x00106D20 File Offset: 0x00104F20
		public void WriteStartProcessingInstruction(string target)
		{
			this.ConstructWithinContent(XPathNodeType.ProcessingInstruction);
			ValidateNames.ValidateNameThrow("", target, "", XPathNodeType.ProcessingInstruction, ValidateNames.Flags.AllExceptPrefixMapping);
			this.piTarget = target;
			this.nodeText.Clear();
			this.xstate = XmlState.WithinPI;
			this.depth++;
		}

		// Token: 0x06002C7F RID: 11391 RVA: 0x00106C70 File Offset: 0x00104E70
		public void WriteProcessingInstructionString(string text)
		{
			this.nodeText.ConcatNoDelimiter(text);
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x00106D70 File Offset: 0x00104F70
		public void WriteEndProcessingInstruction()
		{
			this.Writer.WriteProcessingInstruction(this.piTarget, this.nodeText.GetResult());
			this.xstate = XmlState.WithinContent;
			this.depth--;
			if (this.depth == 0)
			{
				this.EndTree();
			}
		}

		// Token: 0x06002C81 RID: 11393 RVA: 0x00106DBC File Offset: 0x00104FBC
		public void WriteItem(XPathItem item)
		{
			if (!item.IsNode)
			{
				this.seqwrt.WriteItem(item);
				return;
			}
			XPathNavigator xpathNavigator = (XPathNavigator)item;
			if (this.xstate == XmlState.WithinSequence)
			{
				this.seqwrt.WriteItem(xpathNavigator);
				return;
			}
			this.CopyNode(xpathNavigator);
		}

		// Token: 0x06002C82 RID: 11394 RVA: 0x00106E04 File Offset: 0x00105004
		public void XsltCopyOf(XPathNavigator navigator)
		{
			RtfNavigator rtfNavigator = navigator as RtfNavigator;
			if (rtfNavigator != null)
			{
				rtfNavigator.CopyToWriter(this);
				return;
			}
			if (navigator.NodeType == XPathNodeType.Root)
			{
				if (navigator.MoveToFirstChild())
				{
					do
					{
						this.CopyNode(navigator);
					}
					while (navigator.MoveToNext());
					navigator.MoveToParent();
					return;
				}
			}
			else
			{
				this.CopyNode(navigator);
			}
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x00106E51 File Offset: 0x00105051
		public bool StartCopy(XPathNavigator navigator)
		{
			if (navigator.NodeType == XPathNodeType.Root)
			{
				return true;
			}
			if (this.StartCopy(navigator, true))
			{
				this.CopyNamespaces(navigator, XPathNamespaceScope.ExcludeXml);
				return true;
			}
			return false;
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x00106E72 File Offset: 0x00105072
		public void EndCopy(XPathNavigator navigator)
		{
			if (navigator.NodeType == XPathNodeType.Element)
			{
				this.WriteEndElement();
			}
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x00106E83 File Offset: 0x00105083
		private void AddNamespace(string prefix, string ns)
		{
			this.nsmgr.AddNamespace(prefix, ns);
			this.cntNmsp++;
			this.usedPrefixes[prefix] = ns;
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x00106EB0 File Offset: 0x001050B0
		private void WriteString(string text, bool disableOutputEscaping)
		{
			switch (this.xstate)
			{
			case XmlState.WithinSequence:
				this.StartTree(XPathNodeType.Text);
				break;
			case XmlState.EnumAttrs:
				this.StartElementContentUnchecked();
				break;
			case XmlState.WithinContent:
				break;
			case XmlState.WithinAttr:
				this.WriteStringUnchecked(text);
				goto IL_71;
			case XmlState.WithinNmsp:
				this.WriteNamespaceString(text);
				goto IL_71;
			case XmlState.WithinComment:
				this.WriteCommentString(text);
				goto IL_71;
			case XmlState.WithinPI:
				this.WriteProcessingInstructionString(text);
				goto IL_71;
			default:
				goto IL_71;
			}
			if (disableOutputEscaping)
			{
				this.WriteRawUnchecked(text);
			}
			else
			{
				this.WriteStringUnchecked(text);
			}
			IL_71:
			if (this.depth == 0)
			{
				this.EndTree();
			}
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x00106F3C File Offset: 0x0010513C
		private void CopyNode(XPathNavigator navigator)
		{
			int num = this.depth;
			for (;;)
			{
				IL_07:
				if (this.StartCopy(navigator, this.depth == num))
				{
					XPathNodeType nodeType = navigator.NodeType;
					if (navigator.MoveToFirstAttribute())
					{
						do
						{
							this.StartCopy(navigator, false);
						}
						while (navigator.MoveToNextAttribute());
						navigator.MoveToParent();
					}
					this.CopyNamespaces(navigator, (this.depth - 1 == num) ? XPathNamespaceScope.ExcludeXml : XPathNamespaceScope.Local);
					this.StartElementContentUnchecked();
					if (navigator.MoveToFirstChild())
					{
						continue;
					}
					this.EndCopy(navigator, this.depth - 1 == num);
				}
				while (this.depth != num)
				{
					if (navigator.MoveToNext())
					{
						goto IL_07;
					}
					navigator.MoveToParent();
					this.EndCopy(navigator, this.depth - 1 == num);
				}
				break;
			}
		}

		// Token: 0x06002C88 RID: 11400 RVA: 0x00106FF0 File Offset: 0x001051F0
		private bool StartCopy(XPathNavigator navigator, bool callChk)
		{
			bool result = false;
			switch (navigator.NodeType)
			{
			case XPathNodeType.Root:
				this.ThrowInvalidStateError(XPathNodeType.Root);
				break;
			case XPathNodeType.Element:
				if (callChk)
				{
					this.WriteStartElement(navigator.Prefix, navigator.LocalName, navigator.NamespaceURI);
				}
				else
				{
					this.WriteStartElementUnchecked(navigator.Prefix, navigator.LocalName, navigator.NamespaceURI);
				}
				result = true;
				break;
			case XPathNodeType.Attribute:
				if (callChk)
				{
					this.WriteStartAttribute(navigator.Prefix, navigator.LocalName, navigator.NamespaceURI);
				}
				else
				{
					this.WriteStartAttributeUnchecked(navigator.Prefix, navigator.LocalName, navigator.NamespaceURI);
				}
				this.WriteString(navigator.Value);
				if (callChk)
				{
					this.WriteEndAttribute();
				}
				else
				{
					this.WriteEndAttributeUnchecked();
				}
				break;
			case XPathNodeType.Namespace:
				if (callChk)
				{
					XmlAttributeCache xmlAttributeCache = this.Writer as XmlAttributeCache;
					if (xmlAttributeCache != null && xmlAttributeCache.Count != 0)
					{
						throw new XslTransformException("Namespace nodes cannot be added to the parent element after an attribute node has already been added.", new string[]
						{
							string.Empty
						});
					}
					this.WriteNamespaceDeclaration(navigator.LocalName, navigator.Value);
				}
				else
				{
					this.WriteNamespaceDeclarationUnchecked(navigator.LocalName, navigator.Value);
				}
				break;
			case XPathNodeType.Text:
			case XPathNodeType.SignificantWhitespace:
			case XPathNodeType.Whitespace:
				if (callChk)
				{
					this.WriteString(navigator.Value, false);
				}
				else
				{
					this.WriteStringUnchecked(navigator.Value);
				}
				break;
			case XPathNodeType.ProcessingInstruction:
				this.WriteStartProcessingInstruction(navigator.LocalName);
				this.WriteProcessingInstructionString(navigator.Value);
				this.WriteEndProcessingInstruction();
				break;
			case XPathNodeType.Comment:
				this.WriteStartComment();
				this.WriteCommentString(navigator.Value);
				this.WriteEndComment();
				break;
			}
			return result;
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x00107188 File Offset: 0x00105388
		private void EndCopy(XPathNavigator navigator, bool callChk)
		{
			if (callChk)
			{
				this.WriteEndElement();
				return;
			}
			this.WriteEndElementUnchecked(navigator.Prefix, navigator.LocalName, navigator.NamespaceURI);
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x001071AC File Offset: 0x001053AC
		private void CopyNamespaces(XPathNavigator navigator, XPathNamespaceScope nsScope)
		{
			if (navigator.NamespaceURI.Length == 0)
			{
				this.WriteNamespaceDeclarationUnchecked(string.Empty, string.Empty);
			}
			if (navigator.MoveToFirstNamespace(nsScope))
			{
				this.CopyNamespacesHelper(navigator, nsScope);
				navigator.MoveToParent();
			}
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x001071E4 File Offset: 0x001053E4
		private void CopyNamespacesHelper(XPathNavigator navigator, XPathNamespaceScope nsScope)
		{
			string localName = navigator.LocalName;
			string value = navigator.Value;
			if (navigator.MoveToNextNamespace(nsScope))
			{
				this.CopyNamespacesHelper(navigator, nsScope);
			}
			this.WriteNamespaceDeclarationUnchecked(localName, value);
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x00107218 File Offset: 0x00105418
		private void ConstructWithinContent(XPathNodeType rootType)
		{
			switch (this.xstate)
			{
			case XmlState.WithinSequence:
				this.StartTree(rootType);
				this.xstate = XmlState.WithinContent;
				return;
			case XmlState.EnumAttrs:
				this.StartElementContentUnchecked();
				return;
			case XmlState.WithinContent:
				break;
			default:
				this.ThrowInvalidStateError(rootType);
				break;
			}
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x00107260 File Offset: 0x00105460
		private void ConstructInEnumAttrs(XPathNodeType rootType)
		{
			XmlState xmlState = this.xstate;
			if (xmlState != XmlState.WithinSequence)
			{
				if (xmlState != XmlState.EnumAttrs)
				{
					this.ThrowInvalidStateError(rootType);
				}
				return;
			}
			this.StartTree(rootType);
			this.xstate = XmlState.EnumAttrs;
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x00107294 File Offset: 0x00105494
		private void WriteCachedNamespaces()
		{
			while (this.cntNmsp != 0)
			{
				this.cntNmsp--;
				string prefix;
				string ns;
				this.nsmgr.GetNamespaceDeclaration(this.cntNmsp, out prefix, out ns);
				this.Writer.WriteNamespaceDeclaration(prefix, ns);
			}
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x001072DC File Offset: 0x001054DC
		private XPathNodeType XmlStateToNodeType(XmlState xstate)
		{
			switch (xstate)
			{
			case XmlState.EnumAttrs:
				return XPathNodeType.Element;
			case XmlState.WithinContent:
				return XPathNodeType.Element;
			case XmlState.WithinAttr:
				return XPathNodeType.Attribute;
			case XmlState.WithinComment:
				return XPathNodeType.Comment;
			case XmlState.WithinPI:
				return XPathNodeType.ProcessingInstruction;
			}
			return XPathNodeType.Element;
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x0010730C File Offset: 0x0010550C
		private string CheckAttributePrefix(string prefix, string ns)
		{
			if (this.nsmgr == null)
			{
				this.WriteNamespaceDeclarationUnchecked(prefix, ns);
			}
			else
			{
				for (;;)
				{
					string text = this.nsmgr.LookupNamespace(prefix);
					if (!(text != ns))
					{
						return prefix;
					}
					if (text == null)
					{
						break;
					}
					prefix = this.RemapPrefix(prefix, ns, false);
				}
				this.AddNamespace(prefix, ns);
			}
			return prefix;
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x0010735C File Offset: 0x0010555C
		private string RemapPrefix(string prefix, string ns, bool isElemPrefix)
		{
			if (this.conflictPrefixes == null)
			{
				this.conflictPrefixes = new Dictionary<string, string>(16);
			}
			if (this.nsmgr == null)
			{
				this.nsmgr = new XmlNamespaceManager(this.runtime.NameTable);
				this.nsmgr.PushScope();
			}
			string text = this.nsmgr.LookupPrefix(ns);
			if ((text == null || (!isElemPrefix && text.Length == 0)) && (!this.conflictPrefixes.TryGetValue(ns, out text) || !(text != prefix) || (!isElemPrefix && text.Length == 0)))
			{
				string str = "xp_";
				int num = this.prefixIndex;
				this.prefixIndex = num + 1;
				text = str + num.ToString(CultureInfo.InvariantCulture);
			}
			this.conflictPrefixes[ns] = text;
			return text;
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x0010741C File Offset: 0x0010561C
		private void WriteStartComputed(XPathNodeType nodeType, string tagName, int prefixMappingsIndex)
		{
			string prefix;
			string localName;
			string ns;
			this.runtime.ParseTagName(tagName, prefixMappingsIndex, out prefix, out localName, out ns);
			prefix = this.EnsureValidName(prefix, localName, ns, nodeType);
			if (nodeType == XPathNodeType.Element)
			{
				this.WriteStartElement(prefix, localName, ns);
				return;
			}
			this.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x06002C93 RID: 11411 RVA: 0x00107460 File Offset: 0x00105660
		private void WriteStartComputed(XPathNodeType nodeType, string tagName, string ns)
		{
			string prefix;
			string localName;
			ValidateNames.ParseQNameThrow(tagName, out prefix, out localName);
			prefix = this.EnsureValidName(prefix, localName, ns, nodeType);
			if (nodeType == XPathNodeType.Element)
			{
				this.WriteStartElement(prefix, localName, ns);
				return;
			}
			this.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x0010749C File Offset: 0x0010569C
		private void WriteStartComputed(XPathNodeType nodeType, XPathNavigator navigator)
		{
			string prefix = navigator.Prefix;
			string localName = navigator.LocalName;
			string namespaceURI = navigator.NamespaceURI;
			if (navigator.NodeType != nodeType)
			{
				prefix = this.EnsureValidName(prefix, localName, namespaceURI, nodeType);
			}
			if (nodeType == XPathNodeType.Element)
			{
				this.WriteStartElement(prefix, localName, namespaceURI);
				return;
			}
			this.WriteStartAttribute(prefix, localName, namespaceURI);
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x001074EC File Offset: 0x001056EC
		private void WriteStartComputed(XPathNodeType nodeType, XmlQualifiedName name)
		{
			string prefix = (name.Namespace.Length != 0) ? this.RemapPrefix(string.Empty, name.Namespace, nodeType == XPathNodeType.Element) : string.Empty;
			prefix = this.EnsureValidName(prefix, name.Name, name.Namespace, nodeType);
			if (nodeType == XPathNodeType.Element)
			{
				this.WriteStartElement(prefix, name.Name, name.Namespace);
				return;
			}
			this.WriteStartAttribute(prefix, name.Name, name.Namespace);
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x00107563 File Offset: 0x00105763
		private string EnsureValidName(string prefix, string localName, string ns, XPathNodeType nodeType)
		{
			if (!ValidateNames.ValidateName(prefix, localName, ns, nodeType, ValidateNames.Flags.AllExceptNCNames))
			{
				prefix = ((ns.Length != 0) ? this.RemapPrefix(string.Empty, ns, nodeType == XPathNodeType.Element) : string.Empty);
				ValidateNames.ValidateNameThrow(prefix, localName, ns, nodeType, ValidateNames.Flags.AllExceptNCNames);
			}
			return prefix;
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x001075A0 File Offset: 0x001057A0
		private void PushElementNames(string prefix, string localName, string ns)
		{
			if (this.stkNames == null)
			{
				this.stkNames = new Stack<string>(15);
			}
			this.stkNames.Push(prefix);
			this.stkNames.Push(localName);
			this.stkNames.Push(ns);
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x001075DB File Offset: 0x001057DB
		private void PopElementNames(out string prefix, out string localName, out string ns)
		{
			ns = this.stkNames.Pop();
			localName = this.stkNames.Pop();
			prefix = this.stkNames.Pop();
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x00107604 File Offset: 0x00105804
		private void ThrowInvalidStateError(XPathNodeType constructorType)
		{
			switch (constructorType)
			{
			case XPathNodeType.Root:
			case XPathNodeType.Element:
			case XPathNodeType.Text:
			case XPathNodeType.ProcessingInstruction:
			case XPathNodeType.Comment:
				break;
			case XPathNodeType.Attribute:
			case XPathNodeType.Namespace:
				if (this.depth == 1)
				{
					throw new XslTransformException("An item of type '{0}' cannot be constructed within a node of type '{1}'.", new string[]
					{
						constructorType.ToString(),
						this.rootType.ToString()
					});
				}
				if (this.xstate == XmlState.WithinContent)
				{
					throw new XslTransformException("Attribute and namespace nodes cannot be added to the parent element after a text, comment, pi, or sub-element node has already been added.", new string[]
					{
						string.Empty
					});
				}
				break;
			case XPathNodeType.SignificantWhitespace:
			case XPathNodeType.Whitespace:
				goto IL_D0;
			default:
				goto IL_D0;
			}
			throw new XslTransformException("An item of type '{0}' cannot be constructed within a node of type '{1}'.", new string[]
			{
				constructorType.ToString(),
				this.XmlStateToNodeType(this.xstate).ToString()
			});
			IL_D0:
			throw new XslTransformException("An item of type '{0}' cannot be constructed within a node of type '{1}'.", new string[]
			{
				"Unknown",
				this.XmlStateToNodeType(this.xstate).ToString()
			});
		}

		// Token: 0x040022EB RID: 8939
		private XmlRawWriter xwrt;

		// Token: 0x040022EC RID: 8940
		private XmlQueryRuntime runtime;

		// Token: 0x040022ED RID: 8941
		private XmlAttributeCache attrCache;

		// Token: 0x040022EE RID: 8942
		private int depth;

		// Token: 0x040022EF RID: 8943
		private XmlState xstate;

		// Token: 0x040022F0 RID: 8944
		private XmlSequenceWriter seqwrt;

		// Token: 0x040022F1 RID: 8945
		private XmlNamespaceManager nsmgr;

		// Token: 0x040022F2 RID: 8946
		private int cntNmsp;

		// Token: 0x040022F3 RID: 8947
		private Dictionary<string, string> conflictPrefixes;

		// Token: 0x040022F4 RID: 8948
		private int prefixIndex;

		// Token: 0x040022F5 RID: 8949
		private string piTarget;

		// Token: 0x040022F6 RID: 8950
		private StringConcat nodeText;

		// Token: 0x040022F7 RID: 8951
		private Stack<string> stkNames;

		// Token: 0x040022F8 RID: 8952
		private XPathNodeType rootType;

		// Token: 0x040022F9 RID: 8953
		private Dictionary<string, string> usedPrefixes = new Dictionary<string, string>();
	}
}
