using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003BE RID: 958
	internal class VariableAction : ContainerAction, IXsltContextVariable
	{
		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x060026D1 RID: 9937 RVA: 0x000E86CD File Offset: 0x000E68CD
		internal int Stylesheetid
		{
			get
			{
				return this.stylesheetid;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x060026D2 RID: 9938 RVA: 0x000E86D5 File Offset: 0x000E68D5
		internal XmlQualifiedName Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x060026D3 RID: 9939 RVA: 0x000E86DD File Offset: 0x000E68DD
		internal string NameStr
		{
			get
			{
				return this.nameStr;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x060026D4 RID: 9940 RVA: 0x000E86E5 File Offset: 0x000E68E5
		internal VariableType VarType
		{
			get
			{
				return this.varType;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x060026D5 RID: 9941 RVA: 0x000E86ED File Offset: 0x000E68ED
		internal int VarKey
		{
			get
			{
				return this.varKey;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x060026D6 RID: 9942 RVA: 0x000E86F5 File Offset: 0x000E68F5
		internal bool IsGlobal
		{
			get
			{
				return this.varType == VariableType.GlobalVariable || this.varType == VariableType.GlobalParameter;
			}
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x000E870A File Offset: 0x000E690A
		internal VariableAction(VariableType type)
		{
			this.varType = type;
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x000E8720 File Offset: 0x000E6920
		internal override void Compile(Compiler compiler)
		{
			this.stylesheetid = compiler.Stylesheetid;
			this.baseUri = compiler.Input.BaseURI;
			base.CompileAttributes(compiler);
			base.CheckRequiredAttribute(compiler, this.name, "name");
			if (compiler.Recurse())
			{
				base.CompileTemplate(compiler);
				compiler.ToParent();
				if (this.selectKey != -1 && this.containedActions != null)
				{
					throw XsltException.Create("The variable or parameter '{0}' cannot have both a 'select' attribute and non-empty content.", new string[]
					{
						this.nameStr
					});
				}
			}
			if (this.containedActions != null)
			{
				this.baseUri = this.baseUri + "#" + compiler.GetUnicRtfId();
			}
			else
			{
				this.baseUri = null;
			}
			this.varKey = compiler.InsertVariable(this);
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x000E87E0 File Offset: 0x000E69E0
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Name))
			{
				this.nameStr = value;
				this.name = compiler.CreateXPathQName(this.nameStr);
			}
			else
			{
				if (!Ref.Equal(localName, compiler.Atoms.Select))
				{
					return false;
				}
				this.selectKey = compiler.AddQuery(value);
			}
			return true;
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x000E8858 File Offset: 0x000E6A58
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			object obj = null;
			switch (frame.State)
			{
			case 0:
				if (this.IsGlobal)
				{
					if (frame.GetVariable(this.varKey) != null)
					{
						frame.Finished();
						return;
					}
					frame.SetVariable(this.varKey, VariableAction.BeingComputedMark);
				}
				if (this.varType == VariableType.GlobalParameter)
				{
					obj = processor.GetGlobalParameter(this.name);
				}
				else if (this.varType == VariableType.LocalParameter)
				{
					obj = processor.GetParameter(this.name);
				}
				if (obj == null)
				{
					if (this.selectKey != -1)
					{
						obj = processor.RunQuery(frame, this.selectKey);
					}
					else
					{
						if (this.containedActions != null)
						{
							NavigatorOutput output = new NavigatorOutput(this.baseUri);
							processor.PushOutput(output);
							processor.PushActionFrame(frame);
							frame.State = 1;
							return;
						}
						obj = string.Empty;
					}
				}
				break;
			case 1:
				obj = ((NavigatorOutput)processor.PopOutput()).Navigator;
				break;
			case 2:
				break;
			default:
				return;
			}
			frame.SetVariable(this.varKey, obj);
			frame.Finished();
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x060026DB RID: 9947 RVA: 0x0006AB76 File Offset: 0x00068D76
		XPathResultType IXsltContextVariable.VariableType
		{
			get
			{
				return XPathResultType.Any;
			}
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x000E894F File Offset: 0x000E6B4F
		object IXsltContextVariable.Evaluate(XsltContext xsltContext)
		{
			return ((XsltCompileContext)xsltContext).EvaluateVariable(this);
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x060026DD RID: 9949 RVA: 0x000E895D File Offset: 0x000E6B5D
		bool IXsltContextVariable.IsLocal
		{
			get
			{
				return this.varType == VariableType.LocalVariable || this.varType == VariableType.LocalParameter;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x060026DE RID: 9950 RVA: 0x000E8973 File Offset: 0x000E6B73
		bool IXsltContextVariable.IsParam
		{
			get
			{
				return this.varType == VariableType.LocalParameter || this.varType == VariableType.GlobalParameter;
			}
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x000E8989 File Offset: 0x000E6B89
		// Note: this type is marked as 'beforefieldinit'.
		static VariableAction()
		{
		}

		// Token: 0x04001E7E RID: 7806
		public static object BeingComputedMark = new object();

		// Token: 0x04001E7F RID: 7807
		private const int ValueCalculated = 2;

		// Token: 0x04001E80 RID: 7808
		protected XmlQualifiedName name;

		// Token: 0x04001E81 RID: 7809
		protected string nameStr;

		// Token: 0x04001E82 RID: 7810
		protected string baseUri;

		// Token: 0x04001E83 RID: 7811
		protected int selectKey = -1;

		// Token: 0x04001E84 RID: 7812
		protected int stylesheetid;

		// Token: 0x04001E85 RID: 7813
		protected VariableType varType;

		// Token: 0x04001E86 RID: 7814
		private int varKey;
	}
}
