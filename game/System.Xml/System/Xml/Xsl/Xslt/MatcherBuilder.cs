using System;
using System.Collections.Generic;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.XPath;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020003EC RID: 1004
	internal class MatcherBuilder
	{
		// Token: 0x060027C1 RID: 10177 RVA: 0x000EC6D8 File Offset: 0x000EA8D8
		public MatcherBuilder(XPathQilFactory f, ReferenceReplacer refReplacer, InvokeGenerator invkGen)
		{
			this.f = f;
			this.refReplacer = refReplacer;
			this.invkGen = invkGen;
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x000EC760 File Offset: 0x000EA960
		private void Clear()
		{
			this.priority = -1;
			this.elementPatterns.Clear();
			this.attributePatterns.Clear();
			this.textPatterns.Clear();
			this.documentPatterns.Clear();
			this.commentPatterns.Clear();
			this.piPatterns.Clear();
			this.heterogenousPatterns.Clear();
			this.allMatches.Clear();
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x000EC7CC File Offset: 0x000EA9CC
		private void AddPatterns(List<TemplateMatch> matches)
		{
			foreach (TemplateMatch templateMatch in matches)
			{
				TemplateMatch match = templateMatch;
				int num = this.priority + 1;
				this.priority = num;
				Pattern pattern = new Pattern(match, num);
				XmlNodeKindFlags nodeKind = templateMatch.NodeKind;
				if (nodeKind <= XmlNodeKindFlags.Text)
				{
					switch (nodeKind)
					{
					case XmlNodeKindFlags.Document:
						this.documentPatterns.Add(pattern);
						continue;
					case XmlNodeKindFlags.Element:
						this.elementPatterns.Add(pattern);
						continue;
					case XmlNodeKindFlags.Document | XmlNodeKindFlags.Element:
						break;
					case XmlNodeKindFlags.Attribute:
						this.attributePatterns.Add(pattern);
						continue;
					default:
						if (nodeKind == XmlNodeKindFlags.Text)
						{
							this.textPatterns.Add(pattern);
							continue;
						}
						break;
					}
				}
				else
				{
					if (nodeKind == XmlNodeKindFlags.Comment)
					{
						this.commentPatterns.Add(pattern);
						continue;
					}
					if (nodeKind == XmlNodeKindFlags.PI)
					{
						this.piPatterns.Add(pattern);
						continue;
					}
				}
				this.heterogenousPatterns.Add(pattern);
			}
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x000EC8D0 File Offset: 0x000EAAD0
		private void CollectPatternsInternal(Stylesheet sheet, QilName mode)
		{
			foreach (Stylesheet sheet2 in sheet.Imports)
			{
				this.CollectPatternsInternal(sheet2, mode);
			}
			List<TemplateMatch> list;
			if (sheet.TemplateMatches.TryGetValue(mode, out list))
			{
				this.AddPatterns(list);
				this.allMatches.Add(list);
			}
		}

		// Token: 0x060027C5 RID: 10181 RVA: 0x000EC924 File Offset: 0x000EAB24
		public void CollectPatterns(StylesheetLevel sheet, QilName mode)
		{
			this.Clear();
			foreach (Stylesheet sheet2 in sheet.Imports)
			{
				this.CollectPatternsInternal(sheet2, mode);
			}
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x000EC958 File Offset: 0x000EAB58
		private QilNode MatchPattern(QilIterator it, TemplateMatch match)
		{
			QilNode qilNode = match.Condition;
			if (qilNode == null)
			{
				return this.f.True();
			}
			qilNode = qilNode.DeepClone(this.f.BaseFactory);
			return this.refReplacer.Replace(qilNode, match.Iterator, it);
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x000EC9A0 File Offset: 0x000EABA0
		private QilNode MatchPatterns(QilIterator it, List<Pattern> patternList)
		{
			QilNode qilNode = this.f.Int32(-1);
			foreach (Pattern pattern in patternList)
			{
				qilNode = this.f.Conditional(this.MatchPattern(it, pattern.Match), this.f.Int32(pattern.Priority), qilNode);
			}
			return qilNode;
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x000ECA20 File Offset: 0x000EAC20
		private QilNode MatchPatterns(QilIterator it, XmlQueryType xt, List<Pattern> patternList, QilNode otherwise)
		{
			if (patternList.Count == 0)
			{
				return otherwise;
			}
			return this.f.Conditional(this.f.IsType(it, xt), this.MatchPatterns(it, patternList), otherwise);
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x000ECA4F File Offset: 0x000EAC4F
		private bool IsNoMatch(QilNode matcher)
		{
			return matcher.NodeType == QilNodeType.LiteralInt32;
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x000ECA60 File Offset: 0x000EAC60
		private QilNode MatchPatternsWhosePriorityGreater(QilIterator it, List<Pattern> patternList, QilNode matcher)
		{
			if (patternList.Count == 0)
			{
				return matcher;
			}
			if (this.IsNoMatch(matcher))
			{
				return this.MatchPatterns(it, patternList);
			}
			QilIterator qilIterator = this.f.Let(matcher);
			QilNode qilNode = this.f.Int32(-1);
			int num = -1;
			foreach (Pattern pattern in patternList)
			{
				if (pattern.Priority > num + 1)
				{
					qilNode = this.f.Conditional(this.f.Gt(qilIterator, this.f.Int32(num)), qilIterator, qilNode);
				}
				qilNode = this.f.Conditional(this.MatchPattern(it, pattern.Match), this.f.Int32(pattern.Priority), qilNode);
				num = pattern.Priority;
			}
			if (num != this.priority)
			{
				qilNode = this.f.Conditional(this.f.Gt(qilIterator, this.f.Int32(num)), qilIterator, qilNode);
			}
			return this.f.Loop(qilIterator, qilNode);
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x000ECB84 File Offset: 0x000EAD84
		private QilNode MatchPatterns(QilIterator it, XmlQueryType xt, PatternBag patternBag, QilNode otherwise)
		{
			if (patternBag.FixedNamePatternsNames.Count == 0)
			{
				return this.MatchPatterns(it, xt, patternBag.NonFixedNamePatterns, otherwise);
			}
			QilNode qilNode = this.f.Int32(-1);
			foreach (QilName qilName in patternBag.FixedNamePatternsNames)
			{
				qilNode = this.f.Conditional(this.f.Eq(this.f.NameOf(it), qilName.ShallowClone(this.f.BaseFactory)), this.MatchPatterns(it, patternBag.FixedNamePatterns[qilName]), qilNode);
			}
			qilNode = this.MatchPatternsWhosePriorityGreater(it, patternBag.NonFixedNamePatterns, qilNode);
			return this.f.Conditional(this.f.IsType(it, xt), qilNode, otherwise);
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x000ECC70 File Offset: 0x000EAE70
		public QilNode BuildMatcher(QilIterator it, IList<XslNode> actualArgs, QilNode otherwise)
		{
			QilNode qilNode = this.f.Int32(-1);
			qilNode = this.MatchPatterns(it, XmlQueryTypeFactory.PI, this.piPatterns, qilNode);
			qilNode = this.MatchPatterns(it, XmlQueryTypeFactory.Comment, this.commentPatterns, qilNode);
			qilNode = this.MatchPatterns(it, XmlQueryTypeFactory.Document, this.documentPatterns, qilNode);
			qilNode = this.MatchPatterns(it, XmlQueryTypeFactory.Text, this.textPatterns, qilNode);
			qilNode = this.MatchPatterns(it, XmlQueryTypeFactory.Attribute, this.attributePatterns, qilNode);
			qilNode = this.MatchPatterns(it, XmlQueryTypeFactory.Element, this.elementPatterns, qilNode);
			qilNode = this.MatchPatternsWhosePriorityGreater(it, this.heterogenousPatterns, qilNode);
			if (this.IsNoMatch(qilNode))
			{
				return otherwise;
			}
			QilNode[] array = new QilNode[this.priority + 2];
			int num = -1;
			foreach (List<TemplateMatch> list in this.allMatches)
			{
				foreach (TemplateMatch templateMatch in list)
				{
					array[++num] = this.invkGen.GenerateInvoke(templateMatch.TemplateFunction, actualArgs);
				}
			}
			array[++num] = otherwise;
			return this.f.Choice(qilNode, this.f.BranchList(array));
		}

		// Token: 0x04001F9B RID: 8091
		private XPathQilFactory f;

		// Token: 0x04001F9C RID: 8092
		private ReferenceReplacer refReplacer;

		// Token: 0x04001F9D RID: 8093
		private InvokeGenerator invkGen;

		// Token: 0x04001F9E RID: 8094
		private const int NoMatch = -1;

		// Token: 0x04001F9F RID: 8095
		private int priority = -1;

		// Token: 0x04001FA0 RID: 8096
		private PatternBag elementPatterns = new PatternBag();

		// Token: 0x04001FA1 RID: 8097
		private PatternBag attributePatterns = new PatternBag();

		// Token: 0x04001FA2 RID: 8098
		private List<Pattern> textPatterns = new List<Pattern>();

		// Token: 0x04001FA3 RID: 8099
		private List<Pattern> documentPatterns = new List<Pattern>();

		// Token: 0x04001FA4 RID: 8100
		private List<Pattern> commentPatterns = new List<Pattern>();

		// Token: 0x04001FA5 RID: 8101
		private PatternBag piPatterns = new PatternBag();

		// Token: 0x04001FA6 RID: 8102
		private List<Pattern> heterogenousPatterns = new List<Pattern>();

		// Token: 0x04001FA7 RID: 8103
		private List<List<TemplateMatch>> allMatches = new List<List<TemplateMatch>>();
	}
}
