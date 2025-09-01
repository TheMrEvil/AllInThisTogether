using System;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000416 RID: 1046
	internal abstract class XslVisitor<T>
	{
		// Token: 0x0600293A RID: 10554 RVA: 0x000F71D8 File Offset: 0x000F53D8
		protected virtual T Visit(XslNode node)
		{
			switch (node.NodeType)
			{
			case XslNodeType.ApplyImports:
				return this.VisitApplyImports(node);
			case XslNodeType.ApplyTemplates:
				return this.VisitApplyTemplates(node);
			case XslNodeType.Attribute:
				return this.VisitAttribute((NodeCtor)node);
			case XslNodeType.AttributeSet:
				return this.VisitAttributeSet((AttributeSet)node);
			case XslNodeType.CallTemplate:
				return this.VisitCallTemplate(node);
			case XslNodeType.Choose:
				return this.VisitChoose(node);
			case XslNodeType.Comment:
				return this.VisitComment(node);
			case XslNodeType.Copy:
				return this.VisitCopy(node);
			case XslNodeType.CopyOf:
				return this.VisitCopyOf(node);
			case XslNodeType.Element:
				return this.VisitElement((NodeCtor)node);
			case XslNodeType.Error:
				return this.VisitError(node);
			case XslNodeType.ForEach:
				return this.VisitForEach(node);
			case XslNodeType.If:
				return this.VisitIf(node);
			case XslNodeType.Key:
				return this.VisitKey((Key)node);
			case XslNodeType.List:
				return this.VisitList(node);
			case XslNodeType.LiteralAttribute:
				return this.VisitLiteralAttribute(node);
			case XslNodeType.LiteralElement:
				return this.VisitLiteralElement(node);
			case XslNodeType.Message:
				return this.VisitMessage(node);
			case XslNodeType.Nop:
				return this.VisitNop(node);
			case XslNodeType.Number:
				return this.VisitNumber((Number)node);
			case XslNodeType.Otherwise:
				return this.VisitOtherwise(node);
			case XslNodeType.Param:
				return this.VisitParam((VarPar)node);
			case XslNodeType.PI:
				return this.VisitPI(node);
			case XslNodeType.Sort:
				return this.VisitSort((Sort)node);
			case XslNodeType.Template:
				return this.VisitTemplate((Template)node);
			case XslNodeType.Text:
				return this.VisitText((Text)node);
			case XslNodeType.UseAttributeSet:
				return this.VisitUseAttributeSet(node);
			case XslNodeType.ValueOf:
				return this.VisitValueOf(node);
			case XslNodeType.ValueOfDoe:
				return this.VisitValueOfDoe(node);
			case XslNodeType.Variable:
				return this.VisitVariable((VarPar)node);
			case XslNodeType.WithParam:
				return this.VisitWithParam((VarPar)node);
			default:
				return this.VisitUnknown(node);
			}
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitApplyImports(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitApplyTemplates(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitAttribute(NodeCtor node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitAttributeSet(AttributeSet node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitCallTemplate(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitChoose(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitComment(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitCopy(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitCopyOf(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitElement(NodeCtor node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitError(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitForEach(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06002947 RID: 10567 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitIf(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitKey(Key node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitList(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitLiteralAttribute(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitLiteralElement(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitMessage(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x0600294D RID: 10573 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitNop(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x0600294E RID: 10574 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitNumber(Number node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x0600294F RID: 10575 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitOtherwise(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06002950 RID: 10576 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitParam(VarPar node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06002951 RID: 10577 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitPI(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06002952 RID: 10578 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitSort(Sort node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitTemplate(Template node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitText(Text node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitUseAttributeSet(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06002956 RID: 10582 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitValueOf(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitValueOfDoe(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06002958 RID: 10584 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitVariable(VarPar node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06002959 RID: 10585 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitWithParam(VarPar node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x0600295A RID: 10586 RVA: 0x000F73AB File Offset: 0x000F55AB
		protected virtual T VisitUnknown(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x0600295B RID: 10587 RVA: 0x000F73B4 File Offset: 0x000F55B4
		protected virtual T VisitChildren(XslNode node)
		{
			foreach (XslNode node2 in node.Content)
			{
				this.Visit(node2);
			}
			return default(T);
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x0000216B File Offset: 0x0000036B
		protected XslVisitor()
		{
		}
	}
}
