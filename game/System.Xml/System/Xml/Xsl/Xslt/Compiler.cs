using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Xml.XPath;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020003D5 RID: 981
	internal class Compiler
	{
		// Token: 0x06002744 RID: 10052 RVA: 0x000EA3C4 File Offset: 0x000E85C4
		public Compiler(XsltSettings settings, bool debug, string scriptAssemblyPath)
		{
			TempFileCollection tempFiles = settings.TempFiles ?? new TempFileCollection();
			this.Settings = settings;
			this.IsDebug = (settings.IncludeDebugInformation || debug);
			this.ScriptAssemblyPath = scriptAssemblyPath;
			this.CompilerResults = new CompilerResults(tempFiles);
			this.Scripts = new Scripts(this);
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x000EA4B1 File Offset: 0x000E86B1
		public CompilerResults Compile(object stylesheet, XmlResolver xmlResolver, out QilExpression qil)
		{
			new XsltLoader().Load(this, stylesheet, xmlResolver);
			qil = QilGenerator.CompileStylesheet(this);
			this.SortErrors();
			return this.CompilerResults;
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x000EA4D4 File Offset: 0x000E86D4
		public Stylesheet CreateStylesheet()
		{
			Stylesheet stylesheet = new Stylesheet(this, this.CurrentPrecedence);
			int currentPrecedence = this.CurrentPrecedence;
			this.CurrentPrecedence = currentPrecedence - 1;
			if (currentPrecedence == 0)
			{
				this.Root = new RootLevel(stylesheet);
			}
			return stylesheet;
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x000EA50E File Offset: 0x000E870E
		public void AddModule(string baseUri)
		{
			if (!this.moduleOrder.ContainsKey(baseUri))
			{
				this.moduleOrder[baseUri] = this.moduleOrder.Count;
			}
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x000EA538 File Offset: 0x000E8738
		public void ApplyNsAliases(ref string prefix, ref string nsUri)
		{
			NsAlias nsAlias;
			if (this.NsAliases.TryGetValue(nsUri, out nsAlias))
			{
				nsUri = nsAlias.ResultNsUri;
				prefix = nsAlias.ResultPrefix;
			}
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x000EA568 File Offset: 0x000E8768
		public bool SetNsAlias(string ssheetNsUri, string resultNsUri, string resultPrefix, int importPrecedence)
		{
			NsAlias nsAlias;
			if (this.NsAliases.TryGetValue(ssheetNsUri, out nsAlias) && (importPrecedence < nsAlias.ImportPrecedence || resultNsUri == nsAlias.ResultNsUri))
			{
				return false;
			}
			this.NsAliases[ssheetNsUri] = new NsAlias(resultNsUri, resultPrefix, importPrecedence);
			return nsAlias != null;
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x000EA5B8 File Offset: 0x000E87B8
		private void MergeWhitespaceRules(Stylesheet sheet)
		{
			for (int i = 0; i <= 2; i++)
			{
				sheet.WhitespaceRules[i].Reverse();
				this.WhitespaceRules.AddRange(sheet.WhitespaceRules[i]);
			}
			sheet.WhitespaceRules = null;
		}

		// Token: 0x0600274B RID: 10059 RVA: 0x000EA5F8 File Offset: 0x000E87F8
		private void MergeAttributeSets(Stylesheet sheet)
		{
			foreach (QilName key in sheet.AttributeSets.Keys)
			{
				AttributeSet attributeSet;
				if (!this.AttributeSets.TryGetValue(key, out attributeSet))
				{
					this.AttributeSets[key] = sheet.AttributeSets[key];
				}
				else
				{
					attributeSet.MergeContent(sheet.AttributeSets[key]);
				}
			}
			sheet.AttributeSets = null;
		}

		// Token: 0x0600274C RID: 10060 RVA: 0x000EA68C File Offset: 0x000E888C
		private void MergeGlobalVarPars(Stylesheet sheet)
		{
			foreach (XslNode xslNode in sheet.GlobalVarPars)
			{
				VarPar varPar = (VarPar)xslNode;
				if (!this.AllGlobalVarPars.ContainsKey(varPar.Name))
				{
					if (varPar.NodeType == XslNodeType.Variable)
					{
						this.GlobalVars.Add(varPar);
					}
					else
					{
						this.ExternalPars.Add(varPar);
					}
					this.AllGlobalVarPars[varPar.Name] = varPar;
				}
			}
			sheet.GlobalVarPars = null;
		}

		// Token: 0x0600274D RID: 10061 RVA: 0x000EA730 File Offset: 0x000E8930
		public void MergeWithStylesheet(Stylesheet sheet)
		{
			this.MergeWhitespaceRules(sheet);
			this.MergeAttributeSets(sheet);
			this.MergeGlobalVarPars(sheet);
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x00080531 File Offset: 0x0007E731
		public static string ConstructQName(string prefix, string localName)
		{
			if (prefix.Length == 0)
			{
				return localName;
			}
			return prefix + ":" + localName;
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x000EA748 File Offset: 0x000E8948
		public bool ParseQName(string qname, out string prefix, out string localName, IErrorHelper errorHelper)
		{
			bool result;
			try
			{
				ValidateNames.ParseQNameThrow(qname, out prefix, out localName);
				result = true;
			}
			catch (XmlException ex)
			{
				errorHelper.ReportError(ex.Message, null);
				prefix = this.PhantomNCName;
				localName = this.PhantomNCName;
				result = false;
			}
			return result;
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x000EA798 File Offset: 0x000E8998
		public bool ParseNameTest(string nameTest, out string prefix, out string localName, IErrorHelper errorHelper)
		{
			bool result;
			try
			{
				ValidateNames.ParseNameTestThrow(nameTest, out prefix, out localName);
				result = true;
			}
			catch (XmlException ex)
			{
				errorHelper.ReportError(ex.Message, null);
				prefix = this.PhantomNCName;
				localName = this.PhantomNCName;
				result = false;
			}
			return result;
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x000EA7E8 File Offset: 0x000E89E8
		public void ValidatePiName(string name, IErrorHelper errorHelper)
		{
			try
			{
				ValidateNames.ValidateNameThrow(string.Empty, name, string.Empty, XPathNodeType.ProcessingInstruction, ValidateNames.Flags.AllExceptPrefixMapping);
			}
			catch (XmlException ex)
			{
				errorHelper.ReportError(ex.Message, null);
			}
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x000EA82C File Offset: 0x000E8A2C
		public string CreatePhantomNamespace()
		{
			string str = "\0namespace";
			int num = this.phantomNsCounter;
			this.phantomNsCounter = num + 1;
			return str + num.ToString();
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x000EA85A File Offset: 0x000E8A5A
		public bool IsPhantomNamespace(string namespaceName)
		{
			return namespaceName.Length > 0 && namespaceName[0] == '\0';
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x000EA874 File Offset: 0x000E8A74
		public bool IsPhantomName(QilName qname)
		{
			string namespaceUri = qname.NamespaceUri;
			return namespaceUri.Length > 0 && namespaceUri[0] == '\0';
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06002755 RID: 10069 RVA: 0x000EA89D File Offset: 0x000E8A9D
		// (set) Token: 0x06002756 RID: 10070 RVA: 0x000EA8B0 File Offset: 0x000E8AB0
		private int ErrorCount
		{
			get
			{
				return this.CompilerResults.Errors.Count;
			}
			set
			{
				for (int i = this.ErrorCount - 1; i >= value; i--)
				{
					this.CompilerResults.Errors.RemoveAt(i);
				}
			}
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x000EA8E1 File Offset: 0x000E8AE1
		public void EnterForwardsCompatible()
		{
			this.savedErrorCount = this.ErrorCount;
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x000EA8EF File Offset: 0x000E8AEF
		public bool ExitForwardsCompatible(bool fwdCompat)
		{
			if (fwdCompat && this.ErrorCount > this.savedErrorCount)
			{
				this.ErrorCount = this.savedErrorCount;
				return false;
			}
			return true;
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x000EA914 File Offset: 0x000E8B14
		public CompilerError CreateError(ISourceLineInfo lineInfo, string res, params string[] args)
		{
			this.AddModule(lineInfo.Uri);
			return new CompilerError(lineInfo.Uri, lineInfo.Start.Line, lineInfo.Start.Pos, string.Empty, XslTransformException.CreateMessage(res, args));
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x000EA960 File Offset: 0x000E8B60
		public void ReportError(ISourceLineInfo lineInfo, string res, params string[] args)
		{
			CompilerError value = this.CreateError(lineInfo, res, args);
			this.CompilerResults.Errors.Add(value);
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x000EA98C File Offset: 0x000E8B8C
		public void ReportWarning(ISourceLineInfo lineInfo, string res, params string[] args)
		{
			int num = 1;
			if (0 <= this.Settings.WarningLevel && this.Settings.WarningLevel < num)
			{
				return;
			}
			CompilerError compilerError = this.CreateError(lineInfo, res, args);
			if (this.Settings.TreatWarningsAsErrors)
			{
				compilerError.ErrorText = XslTransformException.CreateMessage("Warning as Error: {0}", new string[]
				{
					compilerError.ErrorText
				});
				this.CompilerResults.Errors.Add(compilerError);
				return;
			}
			compilerError.IsWarning = true;
			this.CompilerResults.Errors.Add(compilerError);
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x000EAA1C File Offset: 0x000E8C1C
		private void SortErrors()
		{
			CompilerErrorCollection errors = this.CompilerResults.Errors;
			if (errors.Count > 1)
			{
				CompilerError[] array = new CompilerError[errors.Count];
				errors.CopyTo(array, 0);
				Array.Sort<CompilerError>(array, new Compiler.CompilerErrorComparer(this.moduleOrder));
				errors.Clear();
				errors.AddRange(array);
			}
		}

		// Token: 0x04001EB4 RID: 7860
		public XsltSettings Settings;

		// Token: 0x04001EB5 RID: 7861
		public bool IsDebug;

		// Token: 0x04001EB6 RID: 7862
		public string ScriptAssemblyPath;

		// Token: 0x04001EB7 RID: 7863
		public int Version;

		// Token: 0x04001EB8 RID: 7864
		public string inputTypeAnnotations;

		// Token: 0x04001EB9 RID: 7865
		public CompilerResults CompilerResults;

		// Token: 0x04001EBA RID: 7866
		public int CurrentPrecedence;

		// Token: 0x04001EBB RID: 7867
		public XslNode StartApplyTemplates;

		// Token: 0x04001EBC RID: 7868
		public RootLevel Root;

		// Token: 0x04001EBD RID: 7869
		public Scripts Scripts;

		// Token: 0x04001EBE RID: 7870
		public Output Output = new Output();

		// Token: 0x04001EBF RID: 7871
		public List<VarPar> ExternalPars = new List<VarPar>();

		// Token: 0x04001EC0 RID: 7872
		public List<VarPar> GlobalVars = new List<VarPar>();

		// Token: 0x04001EC1 RID: 7873
		public List<WhitespaceRule> WhitespaceRules = new List<WhitespaceRule>();

		// Token: 0x04001EC2 RID: 7874
		public DecimalFormats DecimalFormats = new DecimalFormats();

		// Token: 0x04001EC3 RID: 7875
		public Keys Keys = new Keys();

		// Token: 0x04001EC4 RID: 7876
		public List<ProtoTemplate> AllTemplates = new List<ProtoTemplate>();

		// Token: 0x04001EC5 RID: 7877
		public Dictionary<QilName, VarPar> AllGlobalVarPars = new Dictionary<QilName, VarPar>();

		// Token: 0x04001EC6 RID: 7878
		public Dictionary<QilName, Template> NamedTemplates = new Dictionary<QilName, Template>();

		// Token: 0x04001EC7 RID: 7879
		public Dictionary<QilName, AttributeSet> AttributeSets = new Dictionary<QilName, AttributeSet>();

		// Token: 0x04001EC8 RID: 7880
		public Dictionary<string, NsAlias> NsAliases = new Dictionary<string, NsAlias>();

		// Token: 0x04001EC9 RID: 7881
		private Dictionary<string, int> moduleOrder = new Dictionary<string, int>();

		// Token: 0x04001ECA RID: 7882
		public readonly string PhantomNCName = "error";

		// Token: 0x04001ECB RID: 7883
		private int phantomNsCounter;

		// Token: 0x04001ECC RID: 7884
		private int savedErrorCount = -1;

		// Token: 0x020003D6 RID: 982
		private class CompilerErrorComparer : IComparer<CompilerError>
		{
			// Token: 0x0600275D RID: 10077 RVA: 0x000EAA70 File Offset: 0x000E8C70
			public CompilerErrorComparer(Dictionary<string, int> moduleOrder)
			{
				this.moduleOrder = moduleOrder;
			}

			// Token: 0x0600275E RID: 10078 RVA: 0x000EAA80 File Offset: 0x000E8C80
			public int Compare(CompilerError x, CompilerError y)
			{
				if (x == y)
				{
					return 0;
				}
				if (x == null)
				{
					return -1;
				}
				if (y == null)
				{
					return 1;
				}
				int num = this.moduleOrder[x.FileName].CompareTo(this.moduleOrder[y.FileName]);
				if (num != 0)
				{
					return num;
				}
				num = x.Line.CompareTo(y.Line);
				if (num != 0)
				{
					return num;
				}
				num = x.Column.CompareTo(y.Column);
				if (num != 0)
				{
					return num;
				}
				num = x.IsWarning.CompareTo(y.IsWarning);
				if (num != 0)
				{
					return num;
				}
				num = string.CompareOrdinal(x.ErrorNumber, y.ErrorNumber);
				if (num != 0)
				{
					return num;
				}
				return string.CompareOrdinal(x.ErrorText, y.ErrorText);
			}

			// Token: 0x04001ECD RID: 7885
			private Dictionary<string, int> moduleOrder;
		}
	}
}
