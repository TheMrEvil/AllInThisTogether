using System;
using System.Xml.XPath;
using MS.Internal.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003AF RID: 943
	internal class TemplateAction : TemplateBaseAction
	{
		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x0600268E RID: 9870 RVA: 0x000E79D1 File Offset: 0x000E5BD1
		internal int MatchKey
		{
			get
			{
				return this.matchKey;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x0600268F RID: 9871 RVA: 0x000E79D9 File Offset: 0x000E5BD9
		internal XmlQualifiedName Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06002690 RID: 9872 RVA: 0x000E79E1 File Offset: 0x000E5BE1
		internal double Priority
		{
			get
			{
				return this.priority;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06002691 RID: 9873 RVA: 0x000E79E9 File Offset: 0x000E5BE9
		internal XmlQualifiedName Mode
		{
			get
			{
				return this.mode;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06002692 RID: 9874 RVA: 0x000E79F1 File Offset: 0x000E5BF1
		// (set) Token: 0x06002693 RID: 9875 RVA: 0x000E79F9 File Offset: 0x000E5BF9
		internal int TemplateId
		{
			get
			{
				return this.templateId;
			}
			set
			{
				this.templateId = value;
			}
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x000E7A04 File Offset: 0x000E5C04
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			if (this.matchKey == -1)
			{
				if (this.name == null)
				{
					throw XsltException.Create("The 'xsl:template' instruction must have the 'match' and/or 'name' attribute present.", Array.Empty<string>());
				}
				if (this.mode != null)
				{
					throw XsltException.Create("An 'xsl:template' element without a 'match' attribute cannot have a 'mode' attribute.", Array.Empty<string>());
				}
			}
			compiler.BeginTemplate(this);
			if (compiler.Recurse())
			{
				this.CompileParameters(compiler);
				base.CompileTemplate(compiler);
				compiler.ToParent();
			}
			compiler.EndTemplate();
			this.AnalyzePriority(compiler);
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x000E7A8E File Offset: 0x000E5C8E
		internal virtual void CompileSingle(Compiler compiler)
		{
			this.matchKey = compiler.AddQuery("/", false, true, true);
			this.priority = 0.5;
			base.CompileOnceTemplate(compiler);
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x000E7ABC File Offset: 0x000E5CBC
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Match))
			{
				this.matchKey = compiler.AddQuery(value, false, true, true);
			}
			else if (Ref.Equal(localName, compiler.Atoms.Name))
			{
				this.name = compiler.CreateXPathQName(value);
			}
			else if (Ref.Equal(localName, compiler.Atoms.Priority))
			{
				this.priority = XmlConvert.ToXPathDouble(value);
				if (double.IsNaN(this.priority) && !compiler.ForwardCompatibility)
				{
					throw XsltException.Create("'{1}' is an invalid value for the '{0}' attribute.", new string[]
					{
						"priority",
						value
					});
				}
			}
			else
			{
				if (!Ref.Equal(localName, compiler.Atoms.Mode))
				{
					return false;
				}
				if (compiler.AllowBuiltInMode && value == "*")
				{
					this.mode = Compiler.BuiltInMode;
				}
				else
				{
					this.mode = compiler.CreateXPathQName(value);
				}
			}
			return true;
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x000E7BC8 File Offset: 0x000E5DC8
		private void AnalyzePriority(Compiler compiler)
		{
			NavigatorInput input = compiler.Input;
			if (!double.IsNaN(this.priority) || this.matchKey == -1)
			{
				return;
			}
			TheQuery theQuery = compiler.QueryStore[this.MatchKey];
			CompiledXpathExpr compiledQuery = theQuery.CompiledQuery;
			Query query = compiledQuery.QueryTree;
			UnionExpr unionExpr;
			while ((unionExpr = (query as UnionExpr)) != null)
			{
				TemplateAction templateAction = this.CloneWithoutName();
				compiler.QueryStore.Add(new TheQuery(new CompiledXpathExpr(unionExpr.qy2, compiledQuery.Expression, false), theQuery._ScopeManager));
				templateAction.matchKey = compiler.QueryStore.Count - 1;
				templateAction.priority = unionExpr.qy2.XsltDefaultPriority;
				compiler.AddTemplate(templateAction);
				query = unionExpr.qy1;
			}
			if (compiledQuery.QueryTree != query)
			{
				compiler.QueryStore[this.MatchKey] = new TheQuery(new CompiledXpathExpr(query, compiledQuery.Expression, false), theQuery._ScopeManager);
			}
			this.priority = query.XsltDefaultPriority;
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x000E7CC4 File Offset: 0x000E5EC4
		protected void CompileParameters(Compiler compiler)
		{
			NavigatorInput input = compiler.Input;
			for (;;)
			{
				switch (input.NodeType)
				{
				case XPathNodeType.Element:
					if (!Ref.Equal(input.NamespaceURI, input.Atoms.UriXsl) || !Ref.Equal(input.LocalName, input.Atoms.Param))
					{
						return;
					}
					compiler.PushNamespaceScope();
					base.AddAction(compiler.CreateVariableAction(VariableType.LocalParameter));
					compiler.PopScope();
					break;
				case XPathNodeType.Text:
					return;
				case XPathNodeType.SignificantWhitespace:
					base.AddEvent(compiler.CreateTextEvent());
					break;
				}
				if (!input.Advance())
				{
					return;
				}
			}
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x000E7D61 File Offset: 0x000E5F61
		private TemplateAction CloneWithoutName()
		{
			return new TemplateAction
			{
				containedActions = this.containedActions,
				mode = this.mode,
				variableCount = this.variableCount,
				replaceNSAliasesDone = true
			};
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x000E7D93 File Offset: 0x000E5F93
		internal override void ReplaceNamespaceAlias(Compiler compiler)
		{
			if (!this.replaceNSAliasesDone)
			{
				base.ReplaceNamespaceAlias(compiler);
				this.replaceNSAliasesDone = true;
			}
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x000E7DAC File Offset: 0x000E5FAC
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			int state = frame.State;
			if (state != 0)
			{
				if (state != 1)
				{
					return;
				}
				frame.Finished();
				return;
			}
			else
			{
				if (this.variableCount > 0)
				{
					frame.AllocateVariables(this.variableCount);
				}
				if (this.containedActions != null && this.containedActions.Count > 0)
				{
					processor.PushActionFrame(frame);
					frame.State = 1;
					return;
				}
				frame.Finished();
				return;
			}
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x000E7E0F File Offset: 0x000E600F
		public TemplateAction()
		{
		}

		// Token: 0x04001E5A RID: 7770
		private int matchKey = -1;

		// Token: 0x04001E5B RID: 7771
		private XmlQualifiedName name;

		// Token: 0x04001E5C RID: 7772
		private double priority = double.NaN;

		// Token: 0x04001E5D RID: 7773
		private XmlQualifiedName mode;

		// Token: 0x04001E5E RID: 7774
		private int templateId;

		// Token: 0x04001E5F RID: 7775
		private bool replaceNSAliasesDone;
	}
}
