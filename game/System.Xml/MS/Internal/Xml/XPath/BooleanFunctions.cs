using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200061C RID: 1564
	internal sealed class BooleanFunctions : ValueQuery
	{
		// Token: 0x06004016 RID: 16406 RVA: 0x00163ADA File Offset: 0x00161CDA
		public BooleanFunctions(Function.FunctionType funcType, Query arg)
		{
			this._arg = arg;
			this._funcType = funcType;
		}

		// Token: 0x06004017 RID: 16407 RVA: 0x00163AF0 File Offset: 0x00161CF0
		private BooleanFunctions(BooleanFunctions other) : base(other)
		{
			this._arg = Query.Clone(other._arg);
			this._funcType = other._funcType;
		}

		// Token: 0x06004018 RID: 16408 RVA: 0x00163B16 File Offset: 0x00161D16
		public override void SetXsltContext(XsltContext context)
		{
			if (this._arg != null)
			{
				this._arg.SetXsltContext(context);
			}
		}

		// Token: 0x06004019 RID: 16409 RVA: 0x00163B2C File Offset: 0x00161D2C
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			Function.FunctionType funcType = this._funcType;
			switch (funcType)
			{
			case Function.FunctionType.FuncBoolean:
				return this.toBoolean(nodeIterator);
			case Function.FunctionType.FuncNumber:
				break;
			case Function.FunctionType.FuncTrue:
				return true;
			case Function.FunctionType.FuncFalse:
				return false;
			case Function.FunctionType.FuncNot:
				return this.Not(nodeIterator);
			default:
				if (funcType == Function.FunctionType.FuncLang)
				{
					return this.Lang(nodeIterator);
				}
				break;
			}
			return false;
		}

		// Token: 0x0600401A RID: 16410 RVA: 0x00163B9E File Offset: 0x00161D9E
		internal static bool toBoolean(double number)
		{
			return number != 0.0 && !double.IsNaN(number);
		}

		// Token: 0x0600401B RID: 16411 RVA: 0x00163BB7 File Offset: 0x00161DB7
		internal static bool toBoolean(string str)
		{
			return str.Length > 0;
		}

		// Token: 0x0600401C RID: 16412 RVA: 0x00163BC4 File Offset: 0x00161DC4
		internal bool toBoolean(XPathNodeIterator nodeIterator)
		{
			object obj = this._arg.Evaluate(nodeIterator);
			if (obj is XPathNodeIterator)
			{
				return this._arg.Advance() != null;
			}
			string text = obj as string;
			if (text != null)
			{
				return BooleanFunctions.toBoolean(text);
			}
			if (obj is double)
			{
				return BooleanFunctions.toBoolean((double)obj);
			}
			return !(obj is bool) || (bool)obj;
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x0600401D RID: 16413 RVA: 0x00066748 File Offset: 0x00064948
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.Boolean;
			}
		}

		// Token: 0x0600401E RID: 16414 RVA: 0x00163C2A File Offset: 0x00161E2A
		private bool Not(XPathNodeIterator nodeIterator)
		{
			return !(bool)this._arg.Evaluate(nodeIterator);
		}

		// Token: 0x0600401F RID: 16415 RVA: 0x00163C40 File Offset: 0x00161E40
		private bool Lang(XPathNodeIterator nodeIterator)
		{
			string text = this._arg.Evaluate(nodeIterator).ToString();
			string xmlLang = nodeIterator.Current.XmlLang;
			return xmlLang.StartsWith(text, StringComparison.OrdinalIgnoreCase) && (xmlLang.Length == text.Length || xmlLang[text.Length] == '-');
		}

		// Token: 0x06004020 RID: 16416 RVA: 0x00163C97 File Offset: 0x00161E97
		public override XPathNodeIterator Clone()
		{
			return new BooleanFunctions(this);
		}

		// Token: 0x04002DF9 RID: 11769
		private Query _arg;

		// Token: 0x04002DFA RID: 11770
		private Function.FunctionType _funcType;
	}
}
