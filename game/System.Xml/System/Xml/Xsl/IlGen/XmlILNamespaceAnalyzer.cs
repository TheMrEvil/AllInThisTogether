using System;
using System.Collections.Generic;
using System.Xml.XPath;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x020004AB RID: 1195
	internal class XmlILNamespaceAnalyzer
	{
		// Token: 0x06002EC4 RID: 11972 RVA: 0x00110EE0 File Offset: 0x0010F0E0
		public void Analyze(QilNode nd, bool defaultNmspInScope)
		{
			this.addInScopeNmsp = false;
			this.cntNmsp = 0;
			if (defaultNmspInScope)
			{
				this.nsmgr.PushScope();
				this.nsmgr.AddNamespace(string.Empty, string.Empty);
				this.cntNmsp++;
			}
			this.AnalyzeContent(nd);
			if (defaultNmspInScope)
			{
				this.nsmgr.PopScope();
			}
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x00110F44 File Offset: 0x0010F144
		private void AnalyzeContent(QilNode nd)
		{
			QilNodeType nodeType = nd.NodeType;
			if (nodeType <= QilNodeType.Loop)
			{
				if (nodeType != QilNodeType.Nop)
				{
					switch (nodeType)
					{
					case QilNodeType.Conditional:
						break;
					case QilNodeType.Choice:
					{
						this.addInScopeNmsp = false;
						QilList branches = (nd as QilChoice).Branches;
						for (int i = 0; i < branches.Count; i++)
						{
							this.AnalyzeContent(branches[i]);
						}
						return;
					}
					case QilNodeType.Length:
						goto IL_186;
					case QilNodeType.Sequence:
						using (IEnumerator<QilNode> enumerator = nd.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								QilNode nd2 = enumerator.Current;
								this.AnalyzeContent(nd2);
							}
							return;
						}
						break;
					default:
						if (nodeType != QilNodeType.Loop)
						{
							goto IL_186;
						}
						this.addInScopeNmsp = false;
						this.AnalyzeContent((nd as QilLoop).Body);
						return;
					}
					this.addInScopeNmsp = false;
					this.AnalyzeContent((nd as QilTernary).Center);
					this.AnalyzeContent((nd as QilTernary).Right);
					return;
				}
				this.AnalyzeContent((nd as QilUnary).Child);
				return;
			}
			else
			{
				if (nodeType == QilNodeType.ElementCtor)
				{
					this.addInScopeNmsp = true;
					this.nsmgr.PushScope();
					int num = this.cntNmsp;
					if (this.CheckNamespaceInScope(nd as QilBinary))
					{
						this.AnalyzeContent((nd as QilBinary).Right);
					}
					this.nsmgr.PopScope();
					this.addInScopeNmsp = false;
					this.cntNmsp = num;
					return;
				}
				if (nodeType == QilNodeType.AttributeCtor)
				{
					this.addInScopeNmsp = false;
					this.CheckNamespaceInScope(nd as QilBinary);
					return;
				}
				if (nodeType == QilNodeType.NamespaceDecl)
				{
					this.CheckNamespaceInScope(nd as QilBinary);
					return;
				}
			}
			IL_186:
			this.addInScopeNmsp = false;
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x001110F0 File Offset: 0x0010F2F0
		private bool CheckNamespaceInScope(QilBinary nd)
		{
			QilNodeType nodeType = nd.NodeType;
			string text;
			string text2;
			XPathNodeType nodeKind;
			if (nodeType - QilNodeType.ElementCtor <= 1)
			{
				QilName qilName = nd.Left as QilName;
				if (!(qilName != null))
				{
					return false;
				}
				text = qilName.Prefix;
				text2 = qilName.NamespaceUri;
				nodeKind = ((nd.NodeType == QilNodeType.ElementCtor) ? XPathNodeType.Element : XPathNodeType.Attribute);
			}
			else
			{
				text = (QilLiteral)nd.Left;
				text2 = (QilLiteral)nd.Right;
				nodeKind = XPathNodeType.Namespace;
			}
			if ((nd.NodeType == QilNodeType.AttributeCtor && text2.Length == 0) || (text == "xml" && text2 == "http://www.w3.org/XML/1998/namespace"))
			{
				XmlILConstructInfo.Write(nd).IsNamespaceInScope = true;
				return true;
			}
			if (!ValidateNames.ValidateName(text, string.Empty, text2, nodeKind, ValidateNames.Flags.CheckPrefixMapping))
			{
				return false;
			}
			text = this.nsmgr.NameTable.Add(text);
			text2 = this.nsmgr.NameTable.Add(text2);
			int i = 0;
			while (i < this.cntNmsp)
			{
				string text3;
				string text4;
				this.nsmgr.GetNamespaceDeclaration(i, out text3, out text4);
				if (text == text3)
				{
					if (text2 == text4)
					{
						XmlILConstructInfo.Write(nd).IsNamespaceInScope = true;
						break;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			if (this.addInScopeNmsp)
			{
				this.nsmgr.AddNamespace(text, text2);
				this.cntNmsp++;
			}
			return true;
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x0011123C File Offset: 0x0010F43C
		public XmlILNamespaceAnalyzer()
		{
		}

		// Token: 0x040024FF RID: 9471
		private XmlNamespaceManager nsmgr = new XmlNamespaceManager(new NameTable());

		// Token: 0x04002500 RID: 9472
		private bool addInScopeNmsp;

		// Token: 0x04002501 RID: 9473
		private int cntNmsp;
	}
}
