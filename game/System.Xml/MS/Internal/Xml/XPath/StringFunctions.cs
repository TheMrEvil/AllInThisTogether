using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000652 RID: 1618
	internal sealed class StringFunctions : ValueQuery
	{
		// Token: 0x06004197 RID: 16791 RVA: 0x00167D65 File Offset: 0x00165F65
		public StringFunctions(Function.FunctionType funcType, IList<Query> argList)
		{
			this._funcType = funcType;
			this._argList = argList;
		}

		// Token: 0x06004198 RID: 16792 RVA: 0x00167D7C File Offset: 0x00165F7C
		private StringFunctions(StringFunctions other) : base(other)
		{
			this._funcType = other._funcType;
			Query[] array = new Query[other._argList.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Query.Clone(other._argList[i]);
			}
			this._argList = array;
		}

		// Token: 0x06004199 RID: 16793 RVA: 0x00167DD8 File Offset: 0x00165FD8
		public override void SetXsltContext(XsltContext context)
		{
			for (int i = 0; i < this._argList.Count; i++)
			{
				this._argList[i].SetXsltContext(context);
			}
		}

		// Token: 0x0600419A RID: 16794 RVA: 0x00167E10 File Offset: 0x00166010
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			switch (this._funcType)
			{
			case Function.FunctionType.FuncString:
				return this.toString(nodeIterator);
			case Function.FunctionType.FuncConcat:
				return this.Concat(nodeIterator);
			case Function.FunctionType.FuncStartsWith:
				return this.StartsWith(nodeIterator);
			case Function.FunctionType.FuncContains:
				return this.Contains(nodeIterator);
			case Function.FunctionType.FuncSubstringBefore:
				return this.SubstringBefore(nodeIterator);
			case Function.FunctionType.FuncSubstringAfter:
				return this.SubstringAfter(nodeIterator);
			case Function.FunctionType.FuncSubstring:
				return this.Substring(nodeIterator);
			case Function.FunctionType.FuncStringLength:
				return this.StringLength(nodeIterator);
			case Function.FunctionType.FuncNormalize:
				return this.Normalize(nodeIterator);
			case Function.FunctionType.FuncTranslate:
				return this.Translate(nodeIterator);
			}
			return string.Empty;
		}

		// Token: 0x0600419B RID: 16795 RVA: 0x00167ECE File Offset: 0x001660CE
		internal static string toString(double num)
		{
			return num.ToString("R", NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600419C RID: 16796 RVA: 0x000816D2 File Offset: 0x0007F8D2
		internal static string toString(bool b)
		{
			if (!b)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x0600419D RID: 16797 RVA: 0x00167EE4 File Offset: 0x001660E4
		private string toString(XPathNodeIterator nodeIterator)
		{
			if (this._argList.Count <= 0)
			{
				return nodeIterator.Current.Value;
			}
			object obj = this._argList[0].Evaluate(nodeIterator);
			switch (base.GetXPathType(obj))
			{
			case XPathResultType.String:
				return (string)obj;
			case XPathResultType.Boolean:
				if (!(bool)obj)
				{
					return "false";
				}
				return "true";
			case XPathResultType.NodeSet:
			{
				XPathNavigator xpathNavigator = this._argList[0].Advance();
				if (xpathNavigator == null)
				{
					return string.Empty;
				}
				return xpathNavigator.Value;
			}
			case (XPathResultType)4:
				return ((XPathNavigator)obj).Value;
			default:
				return StringFunctions.toString((double)obj);
			}
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x0600419E RID: 16798 RVA: 0x00167F97 File Offset: 0x00166197
		public override XPathResultType StaticType
		{
			get
			{
				if (this._funcType == Function.FunctionType.FuncStringLength)
				{
					return XPathResultType.Number;
				}
				if (this._funcType == Function.FunctionType.FuncStartsWith || this._funcType == Function.FunctionType.FuncContains)
				{
					return XPathResultType.Boolean;
				}
				return XPathResultType.String;
			}
		}

		// Token: 0x0600419F RID: 16799 RVA: 0x00167FBC File Offset: 0x001661BC
		private string Concat(XPathNodeIterator nodeIterator)
		{
			int i = 0;
			StringBuilder stringBuilder = new StringBuilder();
			while (i < this._argList.Count)
			{
				stringBuilder.Append(this._argList[i++].Evaluate(nodeIterator).ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060041A0 RID: 16800 RVA: 0x0016800C File Offset: 0x0016620C
		private bool StartsWith(XPathNodeIterator nodeIterator)
		{
			string text = this._argList[0].Evaluate(nodeIterator).ToString();
			string text2 = this._argList[1].Evaluate(nodeIterator).ToString();
			return text.Length >= text2.Length && string.CompareOrdinal(text, 0, text2, 0, text2.Length) == 0;
		}

		// Token: 0x060041A1 RID: 16801 RVA: 0x0016806C File Offset: 0x0016626C
		private bool Contains(XPathNodeIterator nodeIterator)
		{
			string source = this._argList[0].Evaluate(nodeIterator).ToString();
			string value = this._argList[1].Evaluate(nodeIterator).ToString();
			return StringFunctions.s_compareInfo.IndexOf(source, value, CompareOptions.Ordinal) >= 0;
		}

		// Token: 0x060041A2 RID: 16802 RVA: 0x001680C0 File Offset: 0x001662C0
		private string SubstringBefore(XPathNodeIterator nodeIterator)
		{
			string text = this._argList[0].Evaluate(nodeIterator).ToString();
			string text2 = this._argList[1].Evaluate(nodeIterator).ToString();
			if (text2.Length == 0)
			{
				return text2;
			}
			int num = StringFunctions.s_compareInfo.IndexOf(text, text2, CompareOptions.Ordinal);
			if (num >= 1)
			{
				return text.Substring(0, num);
			}
			return string.Empty;
		}

		// Token: 0x060041A3 RID: 16803 RVA: 0x0016812C File Offset: 0x0016632C
		private string SubstringAfter(XPathNodeIterator nodeIterator)
		{
			string text = this._argList[0].Evaluate(nodeIterator).ToString();
			string text2 = this._argList[1].Evaluate(nodeIterator).ToString();
			if (text2.Length == 0)
			{
				return text;
			}
			int num = StringFunctions.s_compareInfo.IndexOf(text, text2, CompareOptions.Ordinal);
			if (num >= 0)
			{
				return text.Substring(num + text2.Length);
			}
			return string.Empty;
		}

		// Token: 0x060041A4 RID: 16804 RVA: 0x001681A0 File Offset: 0x001663A0
		private string Substring(XPathNodeIterator nodeIterator)
		{
			string text = this._argList[0].Evaluate(nodeIterator).ToString();
			double num = XmlConvert.XPathRound(XmlConvert.ToXPathDouble(this._argList[1].Evaluate(nodeIterator))) - 1.0;
			if (double.IsNaN(num) || (double)text.Length <= num)
			{
				return string.Empty;
			}
			if (this._argList.Count != 3)
			{
				if (num < 0.0)
				{
					num = 0.0;
				}
				return text.Substring((int)num);
			}
			double num2 = XmlConvert.XPathRound(XmlConvert.ToXPathDouble(this._argList[2].Evaluate(nodeIterator)));
			if (double.IsNaN(num2))
			{
				return string.Empty;
			}
			if (num < 0.0 || num2 < 0.0)
			{
				num2 = num + num2;
				if (num2 <= 0.0)
				{
					return string.Empty;
				}
				num = 0.0;
			}
			double num3 = (double)text.Length - num;
			if (num2 > num3)
			{
				num2 = num3;
			}
			return text.Substring((int)num, (int)num2);
		}

		// Token: 0x060041A5 RID: 16805 RVA: 0x001682AE File Offset: 0x001664AE
		private double StringLength(XPathNodeIterator nodeIterator)
		{
			if (this._argList.Count > 0)
			{
				return (double)this._argList[0].Evaluate(nodeIterator).ToString().Length;
			}
			return (double)nodeIterator.Current.Value.Length;
		}

		// Token: 0x060041A6 RID: 16806 RVA: 0x001682F0 File Offset: 0x001664F0
		private string Normalize(XPathNodeIterator nodeIterator)
		{
			string text;
			if (this._argList.Count > 0)
			{
				text = this._argList[0].Evaluate(nodeIterator).ToString();
			}
			else
			{
				text = nodeIterator.Current.Value;
			}
			int num = -1;
			char[] array = text.ToCharArray();
			bool flag = false;
			XmlCharType instance = XmlCharType.Instance;
			for (int i = 0; i < array.Length; i++)
			{
				if (!instance.IsWhiteSpace(array[i]))
				{
					flag = true;
					num++;
					array[num] = array[i];
				}
				else if (flag)
				{
					flag = false;
					num++;
					array[num] = ' ';
				}
			}
			if (num > -1 && array[num] == ' ')
			{
				num--;
			}
			return new string(array, 0, num + 1);
		}

		// Token: 0x060041A7 RID: 16807 RVA: 0x00168398 File Offset: 0x00166598
		private string Translate(XPathNodeIterator nodeIterator)
		{
			string text = this._argList[0].Evaluate(nodeIterator).ToString();
			string text2 = this._argList[1].Evaluate(nodeIterator).ToString();
			string text3 = this._argList[2].Evaluate(nodeIterator).ToString();
			int num = -1;
			char[] array = text.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				int num2 = text2.IndexOf(array[i]);
				if (num2 != -1)
				{
					if (num2 < text3.Length)
					{
						num++;
						array[num] = text3[num2];
					}
				}
				else
				{
					num++;
					array[num] = array[i];
				}
			}
			return new string(array, 0, num + 1);
		}

		// Token: 0x060041A8 RID: 16808 RVA: 0x00168447 File Offset: 0x00166647
		public override XPathNodeIterator Clone()
		{
			return new StringFunctions(this);
		}

		// Token: 0x060041A9 RID: 16809 RVA: 0x0016844F File Offset: 0x0016664F
		// Note: this type is marked as 'beforefieldinit'.
		static StringFunctions()
		{
		}

		// Token: 0x04002E95 RID: 11925
		private Function.FunctionType _funcType;

		// Token: 0x04002E96 RID: 11926
		private IList<Query> _argList;

		// Token: 0x04002E97 RID: 11927
		private static readonly CompareInfo s_compareInfo = CultureInfo.InvariantCulture.CompareInfo;
	}
}
