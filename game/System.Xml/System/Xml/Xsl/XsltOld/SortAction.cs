using System;
using System.Globalization;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003AA RID: 938
	internal class SortAction : CompiledAction
	{
		// Token: 0x06002661 RID: 9825 RVA: 0x000E6C24 File Offset: 0x000E4E24
		private string ParseLang(string value)
		{
			if (value == null)
			{
				return null;
			}
			if (XmlComplianceUtil.IsValidLanguageID(value.ToCharArray(), 0, value.Length) || (value.Length != 0 && CultureInfo.GetCultureInfo(value) != null))
			{
				return value;
			}
			if (this.forwardCompatibility)
			{
				return null;
			}
			throw XsltException.Create("'{1}' is an invalid value for the '{0}' attribute.", new string[]
			{
				"lang",
				value
			});
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x000E6C84 File Offset: 0x000E4E84
		private XmlDataType ParseDataType(string value, InputScopeManager manager)
		{
			if (value == null)
			{
				return XmlDataType.Text;
			}
			if (value == "text")
			{
				return XmlDataType.Text;
			}
			if (value == "number")
			{
				return XmlDataType.Number;
			}
			string text;
			string text2;
			PrefixQName.ParseQualifiedName(value, out text, out text2);
			manager.ResolveXmlNamespace(text);
			if (text.Length == 0 && !this.forwardCompatibility)
			{
				throw XsltException.Create("'{1}' is an invalid value for the '{0}' attribute.", new string[]
				{
					"data-type",
					value
				});
			}
			return XmlDataType.Text;
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x000E6CF4 File Offset: 0x000E4EF4
		private XmlSortOrder ParseOrder(string value)
		{
			if (value == null)
			{
				return XmlSortOrder.Ascending;
			}
			if (value == "ascending")
			{
				return XmlSortOrder.Ascending;
			}
			if (value == "descending")
			{
				return XmlSortOrder.Descending;
			}
			if (this.forwardCompatibility)
			{
				return XmlSortOrder.Ascending;
			}
			throw XsltException.Create("'{1}' is an invalid value for the '{0}' attribute.", new string[]
			{
				"order",
				value
			});
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x000E6D4C File Offset: 0x000E4F4C
		private XmlCaseOrder ParseCaseOrder(string value)
		{
			if (value == null)
			{
				return XmlCaseOrder.None;
			}
			if (value == "upper-first")
			{
				return XmlCaseOrder.UpperFirst;
			}
			if (value == "lower-first")
			{
				return XmlCaseOrder.LowerFirst;
			}
			if (this.forwardCompatibility)
			{
				return XmlCaseOrder.None;
			}
			throw XsltException.Create("'{1}' is an invalid value for the '{0}' attribute.", new string[]
			{
				"case-order",
				value
			});
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x000E6DA4 File Offset: 0x000E4FA4
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			base.CheckEmpty(compiler);
			if (this.selectKey == -1)
			{
				this.selectKey = compiler.AddQuery(".");
			}
			this.forwardCompatibility = compiler.ForwardCompatibility;
			this.manager = compiler.CloneScopeManager();
			this.lang = this.ParseLang(CompiledAction.PrecalculateAvt(ref this.langAvt));
			this.dataType = this.ParseDataType(CompiledAction.PrecalculateAvt(ref this.dataTypeAvt), this.manager);
			this.order = this.ParseOrder(CompiledAction.PrecalculateAvt(ref this.orderAvt));
			this.caseOrder = this.ParseCaseOrder(CompiledAction.PrecalculateAvt(ref this.caseOrderAvt));
			if (this.langAvt == null && this.dataTypeAvt == null && this.orderAvt == null && this.caseOrderAvt == null)
			{
				this.sort = new Sort(this.selectKey, this.lang, this.dataType, this.order, this.caseOrder);
			}
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x000E6E9C File Offset: 0x000E509C
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Select))
			{
				this.selectKey = compiler.AddQuery(value);
			}
			else if (Ref.Equal(localName, compiler.Atoms.Lang))
			{
				this.langAvt = Avt.CompileAvt(compiler, value);
			}
			else if (Ref.Equal(localName, compiler.Atoms.DataType))
			{
				this.dataTypeAvt = Avt.CompileAvt(compiler, value);
			}
			else if (Ref.Equal(localName, compiler.Atoms.Order))
			{
				this.orderAvt = Avt.CompileAvt(compiler, value);
			}
			else
			{
				if (!Ref.Equal(localName, compiler.Atoms.CaseOrder))
				{
					return false;
				}
				this.caseOrderAvt = Avt.CompileAvt(compiler, value);
			}
			return true;
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x000E6F74 File Offset: 0x000E5174
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			processor.AddSort((this.sort != null) ? this.sort : new Sort(this.selectKey, (this.langAvt == null) ? this.lang : this.ParseLang(this.langAvt.Evaluate(processor, frame)), (this.dataTypeAvt == null) ? this.dataType : this.ParseDataType(this.dataTypeAvt.Evaluate(processor, frame), this.manager), (this.orderAvt == null) ? this.order : this.ParseOrder(this.orderAvt.Evaluate(processor, frame)), (this.caseOrderAvt == null) ? this.caseOrder : this.ParseCaseOrder(this.caseOrderAvt.Evaluate(processor, frame))));
			frame.Finished();
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x000E703D File Offset: 0x000E523D
		public SortAction()
		{
		}

		// Token: 0x04001E19 RID: 7705
		private int selectKey = -1;

		// Token: 0x04001E1A RID: 7706
		private Avt langAvt;

		// Token: 0x04001E1B RID: 7707
		private Avt dataTypeAvt;

		// Token: 0x04001E1C RID: 7708
		private Avt orderAvt;

		// Token: 0x04001E1D RID: 7709
		private Avt caseOrderAvt;

		// Token: 0x04001E1E RID: 7710
		private string lang;

		// Token: 0x04001E1F RID: 7711
		private XmlDataType dataType = XmlDataType.Text;

		// Token: 0x04001E20 RID: 7712
		private XmlSortOrder order = XmlSortOrder.Ascending;

		// Token: 0x04001E21 RID: 7713
		private XmlCaseOrder caseOrder;

		// Token: 0x04001E22 RID: 7714
		private Sort sort;

		// Token: 0x04001E23 RID: 7715
		private bool forwardCompatibility;

		// Token: 0x04001E24 RID: 7716
		private InputScopeManager manager;
	}
}
