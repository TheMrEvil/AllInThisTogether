using System;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x020004A9 RID: 1193
	internal class XmlILStateAnalyzer
	{
		// Token: 0x06002EB2 RID: 11954 RVA: 0x001106E9 File Offset: 0x0010E8E9
		public XmlILStateAnalyzer(QilFactory fac)
		{
			this.fac = fac;
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x001106F8 File Offset: 0x0010E8F8
		public virtual QilNode Analyze(QilNode ndConstr, QilNode ndContent)
		{
			if (ndConstr == null)
			{
				this.parentInfo = null;
				this.xstates = PossibleXmlStates.WithinSequence;
				this.withinElem = false;
				ndContent = this.AnalyzeContent(ndContent);
			}
			else
			{
				this.parentInfo = XmlILConstructInfo.Write(ndConstr);
				if (ndConstr.NodeType == QilNodeType.Function)
				{
					this.parentInfo.ConstructMethod = XmlILConstructMethod.Writer;
					PossibleXmlStates possibleXmlStates = PossibleXmlStates.None;
					foreach (object obj in this.parentInfo.CallersInfo)
					{
						XmlILConstructInfo xmlILConstructInfo = (XmlILConstructInfo)obj;
						if (possibleXmlStates == PossibleXmlStates.None)
						{
							possibleXmlStates = xmlILConstructInfo.InitialStates;
						}
						else if (possibleXmlStates != xmlILConstructInfo.InitialStates)
						{
							possibleXmlStates = PossibleXmlStates.Any;
						}
						xmlILConstructInfo.PushToWriterFirst = true;
					}
					this.parentInfo.InitialStates = possibleXmlStates;
				}
				else
				{
					if (ndConstr.NodeType != QilNodeType.Choice)
					{
						this.parentInfo.InitialStates = (this.parentInfo.FinalStates = PossibleXmlStates.WithinSequence);
					}
					if (ndConstr.NodeType != QilNodeType.RtfCtor)
					{
						this.parentInfo.ConstructMethod = XmlILConstructMethod.WriterThenIterator;
					}
				}
				this.withinElem = (ndConstr.NodeType == QilNodeType.ElementCtor);
				QilNodeType nodeType = ndConstr.NodeType;
				if (nodeType <= QilNodeType.Function)
				{
					if (nodeType != QilNodeType.Choice)
					{
						if (nodeType == QilNodeType.Function)
						{
							this.xstates = this.parentInfo.InitialStates;
						}
					}
					else
					{
						this.xstates = PossibleXmlStates.Any;
					}
				}
				else
				{
					switch (nodeType)
					{
					case QilNodeType.ElementCtor:
						this.xstates = PossibleXmlStates.EnumAttrs;
						break;
					case QilNodeType.AttributeCtor:
						this.xstates = PossibleXmlStates.WithinAttr;
						break;
					case QilNodeType.CommentCtor:
						this.xstates = PossibleXmlStates.WithinComment;
						break;
					case QilNodeType.PICtor:
						this.xstates = PossibleXmlStates.WithinPI;
						break;
					case QilNodeType.TextCtor:
					case QilNodeType.RawTextCtor:
					case QilNodeType.NamespaceDecl:
						break;
					case QilNodeType.DocumentCtor:
						this.xstates = PossibleXmlStates.WithinContent;
						break;
					case QilNodeType.RtfCtor:
						this.xstates = PossibleXmlStates.WithinContent;
						break;
					default:
						if (nodeType != QilNodeType.XsltCopy)
						{
							if (nodeType != QilNodeType.XsltCopyOf)
							{
							}
						}
						else
						{
							this.xstates = PossibleXmlStates.Any;
						}
						break;
					}
				}
				if (ndContent != null)
				{
					ndContent = this.AnalyzeContent(ndContent);
				}
				if (ndConstr.NodeType == QilNodeType.Choice)
				{
					this.AnalyzeChoice(ndConstr as QilChoice, this.parentInfo);
				}
				if (ndConstr.NodeType == QilNodeType.Function)
				{
					this.parentInfo.FinalStates = this.xstates;
				}
			}
			return ndContent;
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x0011090C File Offset: 0x0010EB0C
		protected virtual QilNode AnalyzeContent(QilNode nd)
		{
			QilNodeType nodeType = nd.NodeType;
			if (nodeType - QilNodeType.For <= 2)
			{
				nd = this.fac.Nop(nd);
			}
			XmlILConstructInfo xmlILConstructInfo = XmlILConstructInfo.Write(nd);
			xmlILConstructInfo.ParentInfo = this.parentInfo;
			xmlILConstructInfo.PushToWriterLast = true;
			xmlILConstructInfo.InitialStates = this.xstates;
			nodeType = nd.NodeType;
			if (nodeType <= QilNodeType.Warning)
			{
				if (nodeType != QilNodeType.Nop)
				{
					if (nodeType - QilNodeType.Error <= 1)
					{
						xmlILConstructInfo.ConstructMethod = XmlILConstructMethod.Writer;
						goto IL_FF;
					}
				}
				else
				{
					QilNode child = (nd as QilUnary).Child;
					QilNodeType nodeType2 = child.NodeType;
					if (nodeType2 - QilNodeType.For <= 2)
					{
						this.AnalyzeCopy(nd, xmlILConstructInfo);
						goto IL_FF;
					}
					xmlILConstructInfo.ConstructMethod = XmlILConstructMethod.Writer;
					this.AnalyzeContent(child);
					goto IL_FF;
				}
			}
			else
			{
				switch (nodeType)
				{
				case QilNodeType.Conditional:
					this.AnalyzeConditional(nd as QilTernary, xmlILConstructInfo);
					goto IL_FF;
				case QilNodeType.Choice:
					this.AnalyzeChoice(nd as QilChoice, xmlILConstructInfo);
					goto IL_FF;
				case QilNodeType.Length:
					break;
				case QilNodeType.Sequence:
					this.AnalyzeSequence(nd as QilList, xmlILConstructInfo);
					goto IL_FF;
				default:
					if (nodeType == QilNodeType.Loop)
					{
						this.AnalyzeLoop(nd as QilLoop, xmlILConstructInfo);
						goto IL_FF;
					}
					break;
				}
			}
			this.AnalyzeCopy(nd, xmlILConstructInfo);
			IL_FF:
			xmlILConstructInfo.FinalStates = this.xstates;
			return nd;
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x00110A28 File Offset: 0x0010EC28
		protected virtual void AnalyzeLoop(QilLoop ndLoop, XmlILConstructInfo info)
		{
			XmlQueryType xmlType = ndLoop.XmlType;
			info.ConstructMethod = XmlILConstructMethod.Writer;
			if (!xmlType.IsSingleton)
			{
				this.StartLoop(xmlType, info);
			}
			ndLoop.Body = this.AnalyzeContent(ndLoop.Body);
			if (!xmlType.IsSingleton)
			{
				this.EndLoop(xmlType, info);
			}
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x00110A78 File Offset: 0x0010EC78
		protected virtual void AnalyzeSequence(QilList ndSeq, XmlILConstructInfo info)
		{
			info.ConstructMethod = XmlILConstructMethod.Writer;
			for (int i = 0; i < ndSeq.Count; i++)
			{
				ndSeq[i] = this.AnalyzeContent(ndSeq[i]);
			}
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x00110AB4 File Offset: 0x0010ECB4
		protected virtual void AnalyzeConditional(QilTernary ndCond, XmlILConstructInfo info)
		{
			info.ConstructMethod = XmlILConstructMethod.Writer;
			ndCond.Center = this.AnalyzeContent(ndCond.Center);
			PossibleXmlStates possibleXmlStates = this.xstates;
			this.xstates = info.InitialStates;
			ndCond.Right = this.AnalyzeContent(ndCond.Right);
			if (possibleXmlStates != this.xstates)
			{
				this.xstates = PossibleXmlStates.Any;
			}
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x00110B10 File Offset: 0x0010ED10
		protected virtual void AnalyzeChoice(QilChoice ndChoice, XmlILConstructInfo info)
		{
			int num = ndChoice.Branches.Count - 1;
			ndChoice.Branches[num] = this.AnalyzeContent(ndChoice.Branches[num]);
			PossibleXmlStates possibleXmlStates = this.xstates;
			while (--num >= 0)
			{
				this.xstates = info.InitialStates;
				ndChoice.Branches[num] = this.AnalyzeContent(ndChoice.Branches[num]);
				if (possibleXmlStates != this.xstates)
				{
					possibleXmlStates = PossibleXmlStates.Any;
				}
			}
			this.xstates = possibleXmlStates;
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x00110B98 File Offset: 0x0010ED98
		protected virtual void AnalyzeCopy(QilNode ndCopy, XmlILConstructInfo info)
		{
			XmlQueryType xmlType = ndCopy.XmlType;
			if (!xmlType.IsSingleton)
			{
				this.StartLoop(xmlType, info);
			}
			if (this.MaybeContent(xmlType))
			{
				if (this.MaybeAttrNmsp(xmlType))
				{
					if (this.xstates == PossibleXmlStates.EnumAttrs)
					{
						this.xstates = PossibleXmlStates.Any;
					}
				}
				else if (this.xstates == PossibleXmlStates.EnumAttrs || this.withinElem)
				{
					this.xstates = PossibleXmlStates.WithinContent;
				}
			}
			if (!xmlType.IsSingleton)
			{
				this.EndLoop(xmlType, info);
			}
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x00110C08 File Offset: 0x0010EE08
		private void StartLoop(XmlQueryType typ, XmlILConstructInfo info)
		{
			info.BeginLoopStates = this.xstates;
			if (typ.MaybeMany && this.xstates == PossibleXmlStates.EnumAttrs && this.MaybeContent(typ))
			{
				info.BeginLoopStates = (this.xstates = PossibleXmlStates.Any);
			}
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x00110C4B File Offset: 0x0010EE4B
		private void EndLoop(XmlQueryType typ, XmlILConstructInfo info)
		{
			info.EndLoopStates = this.xstates;
			if (typ.MaybeEmpty && info.InitialStates != this.xstates)
			{
				this.xstates = PossibleXmlStates.Any;
			}
		}

		// Token: 0x06002EBC RID: 11964 RVA: 0x00110C76 File Offset: 0x0010EE76
		private bool MaybeAttrNmsp(XmlQueryType typ)
		{
			return (typ.NodeKinds & (XmlNodeKindFlags.Attribute | XmlNodeKindFlags.Namespace)) > XmlNodeKindFlags.None;
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x00110C84 File Offset: 0x0010EE84
		private bool MaybeContent(XmlQueryType typ)
		{
			return !typ.IsNode || (typ.NodeKinds & ~(XmlNodeKindFlags.Attribute | XmlNodeKindFlags.Namespace)) > XmlNodeKindFlags.None;
		}

		// Token: 0x040024F9 RID: 9465
		protected XmlILConstructInfo parentInfo;

		// Token: 0x040024FA RID: 9466
		protected QilFactory fac;

		// Token: 0x040024FB RID: 9467
		protected PossibleXmlStates xstates;

		// Token: 0x040024FC RID: 9468
		protected bool withinElem;
	}
}
