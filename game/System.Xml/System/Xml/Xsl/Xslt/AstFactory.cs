using System;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x0200040F RID: 1039
	internal static class AstFactory
	{
		// Token: 0x060028CE RID: 10446 RVA: 0x000F5328 File Offset: 0x000F3528
		public static XslNode XslNode(XslNodeType nodeType, QilName name, string arg, XslVersion xslVer)
		{
			return new XslNode(nodeType, name, arg, xslVer);
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x000F5333 File Offset: 0x000F3533
		public static XslNode ApplyImports(QilName mode, Stylesheet sheet, XslVersion xslVer)
		{
			return new XslNode(XslNodeType.ApplyImports, mode, sheet, xslVer);
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x000F533E File Offset: 0x000F353E
		public static XslNodeEx ApplyTemplates(QilName mode, string select, XsltInput.ContextInfo ctxInfo, XslVersion xslVer)
		{
			return new XslNodeEx(XslNodeType.ApplyTemplates, mode, select, ctxInfo, xslVer);
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x000F534A File Offset: 0x000F354A
		public static XslNodeEx ApplyTemplates(QilName mode)
		{
			return new XslNodeEx(XslNodeType.ApplyTemplates, mode, null, XslVersion.Version10);
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x000F5355 File Offset: 0x000F3555
		public static NodeCtor Attribute(string nameAvt, string nsAvt, XslVersion xslVer)
		{
			return new NodeCtor(XslNodeType.Attribute, nameAvt, nsAvt, xslVer);
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x000F5360 File Offset: 0x000F3560
		public static AttributeSet AttributeSet(QilName name)
		{
			return new AttributeSet(name, XslVersion.Version10);
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x000F5369 File Offset: 0x000F3569
		public static XslNodeEx CallTemplate(QilName name, XsltInput.ContextInfo ctxInfo)
		{
			return new XslNodeEx(XslNodeType.CallTemplate, name, null, ctxInfo, XslVersion.Version10);
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x000F5375 File Offset: 0x000F3575
		public static XslNode Choose()
		{
			return new XslNode(XslNodeType.Choose);
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x000F537D File Offset: 0x000F357D
		public static XslNode Comment()
		{
			return new XslNode(XslNodeType.Comment);
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x000F5385 File Offset: 0x000F3585
		public static XslNode Copy()
		{
			return new XslNode(XslNodeType.Copy);
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x000F538D File Offset: 0x000F358D
		public static XslNode CopyOf(string select, XslVersion xslVer)
		{
			return new XslNode(XslNodeType.CopyOf, null, select, xslVer);
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x000F5399 File Offset: 0x000F3599
		public static NodeCtor Element(string nameAvt, string nsAvt, XslVersion xslVer)
		{
			return new NodeCtor(XslNodeType.Element, nameAvt, nsAvt, xslVer);
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x000F53A5 File Offset: 0x000F35A5
		public static XslNode Error(string message)
		{
			return new XslNode(XslNodeType.Error, null, message, XslVersion.Version10);
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x000F53B1 File Offset: 0x000F35B1
		public static XslNodeEx ForEach(string select, XsltInput.ContextInfo ctxInfo, XslVersion xslVer)
		{
			return new XslNodeEx(XslNodeType.ForEach, null, select, ctxInfo, xslVer);
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x000F53BE File Offset: 0x000F35BE
		public static XslNode If(string test, XslVersion xslVer)
		{
			return new XslNode(XslNodeType.If, null, test, xslVer);
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x000F53CA File Offset: 0x000F35CA
		public static Key Key(QilName name, string match, string use, XslVersion xslVer)
		{
			return new Key(name, match, use, xslVer);
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x000F53D5 File Offset: 0x000F35D5
		public static XslNode List()
		{
			return new XslNode(XslNodeType.List);
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x000F53DE File Offset: 0x000F35DE
		public static XslNode LiteralAttribute(QilName name, string value, XslVersion xslVer)
		{
			return new XslNode(XslNodeType.LiteralAttribute, name, value, xslVer);
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x000F53EA File Offset: 0x000F35EA
		public static XslNode LiteralElement(QilName name)
		{
			return new XslNode(XslNodeType.LiteralElement, name, null, XslVersion.Version10);
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x000F53F6 File Offset: 0x000F35F6
		public static XslNode Message(bool term)
		{
			return new XslNode(XslNodeType.Message, null, term, XslVersion.Version10);
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x000F5407 File Offset: 0x000F3607
		public static XslNode Nop()
		{
			return new XslNode(XslNodeType.Nop);
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x000F5410 File Offset: 0x000F3610
		public static Number Number(NumberLevel level, string count, string from, string value, string format, string lang, string letterValue, string groupingSeparator, string groupingSize, XslVersion xslVer)
		{
			return new Number(level, count, from, value, format, lang, letterValue, groupingSeparator, groupingSize, xslVer);
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x000F5432 File Offset: 0x000F3632
		public static XslNode Otherwise()
		{
			return new XslNode(XslNodeType.Otherwise);
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x000F543B File Offset: 0x000F363B
		public static XslNode PI(string name, XslVersion xslVer)
		{
			return new XslNode(XslNodeType.PI, null, name, xslVer);
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x000F5447 File Offset: 0x000F3647
		public static Sort Sort(string select, string lang, string dataType, string order, string caseOrder, XslVersion xslVer)
		{
			return new Sort(select, lang, dataType, order, caseOrder, xslVer);
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x000F5456 File Offset: 0x000F3656
		public static Template Template(QilName name, string match, QilName mode, double priority, XslVersion xslVer)
		{
			return new Template(name, match, mode, priority, xslVer);
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x000F5463 File Offset: 0x000F3663
		public static XslNode Text(string data)
		{
			return new Text(data, SerializationHints.None, XslVersion.Version10);
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x000F546D File Offset: 0x000F366D
		public static XslNode Text(string data, SerializationHints hints)
		{
			return new Text(data, hints, XslVersion.Version10);
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x000F5477 File Offset: 0x000F3677
		public static XslNode UseAttributeSet(QilName name)
		{
			return new XslNode(XslNodeType.UseAttributeSet, name, null, XslVersion.Version10);
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x000F5483 File Offset: 0x000F3683
		public static VarPar VarPar(XslNodeType nt, QilName name, string select, XslVersion xslVer)
		{
			return new VarPar(nt, name, select, xslVer);
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x000F548E File Offset: 0x000F368E
		public static VarPar WithParam(QilName name)
		{
			return AstFactory.VarPar(XslNodeType.WithParam, name, null, XslVersion.Version10);
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x000F549A File Offset: 0x000F369A
		public static QilName QName(string local, string uri, string prefix)
		{
			return AstFactory.f.LiteralQName(local, uri, prefix);
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x000F54A9 File Offset: 0x000F36A9
		public static QilName QName(string local)
		{
			return AstFactory.f.LiteralQName(local);
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x000F54B6 File Offset: 0x000F36B6
		// Note: this type is marked as 'beforefieldinit'.
		static AstFactory()
		{
		}

		// Token: 0x04002073 RID: 8307
		private static QilFactory f = new QilFactory();
	}
}
