using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000460 RID: 1120
	internal class WhitespaceRuleLookup
	{
		// Token: 0x06002B65 RID: 11109 RVA: 0x001041AF File Offset: 0x001023AF
		public WhitespaceRuleLookup()
		{
			this.qnames = new Hashtable();
			this.wildcards = new ArrayList();
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x001041D0 File Offset: 0x001023D0
		public WhitespaceRuleLookup(IList<WhitespaceRule> rules) : this()
		{
			for (int i = rules.Count - 1; i >= 0; i--)
			{
				WhitespaceRule whitespaceRule = rules[i];
				WhitespaceRuleLookup.InternalWhitespaceRule internalWhitespaceRule = new WhitespaceRuleLookup.InternalWhitespaceRule(whitespaceRule.LocalName, whitespaceRule.NamespaceName, whitespaceRule.PreserveSpace, -i);
				if (whitespaceRule.LocalName == null || whitespaceRule.NamespaceName == null)
				{
					this.wildcards.Add(internalWhitespaceRule);
				}
				else
				{
					this.qnames[internalWhitespaceRule] = internalWhitespaceRule;
				}
			}
			this.ruleTemp = new WhitespaceRuleLookup.InternalWhitespaceRule();
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x00104250 File Offset: 0x00102450
		public void Atomize(XmlNameTable nameTable)
		{
			if (nameTable != this.nameTable)
			{
				this.nameTable = nameTable;
				foreach (object obj in this.qnames.Values)
				{
					((WhitespaceRuleLookup.InternalWhitespaceRule)obj).Atomize(nameTable);
				}
				foreach (object obj2 in this.wildcards)
				{
					((WhitespaceRuleLookup.InternalWhitespaceRule)obj2).Atomize(nameTable);
				}
			}
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x00104308 File Offset: 0x00102508
		public bool ShouldStripSpace(string localName, string namespaceName)
		{
			this.ruleTemp.Init(localName, namespaceName, false, 0);
			WhitespaceRuleLookup.InternalWhitespaceRule internalWhitespaceRule = this.qnames[this.ruleTemp] as WhitespaceRuleLookup.InternalWhitespaceRule;
			int count = this.wildcards.Count;
			while (count-- != 0)
			{
				WhitespaceRuleLookup.InternalWhitespaceRule internalWhitespaceRule2 = this.wildcards[count] as WhitespaceRuleLookup.InternalWhitespaceRule;
				if (internalWhitespaceRule != null)
				{
					if (internalWhitespaceRule.Priority > internalWhitespaceRule2.Priority)
					{
						return !internalWhitespaceRule.PreserveSpace;
					}
					if (internalWhitespaceRule.PreserveSpace == internalWhitespaceRule2.PreserveSpace)
					{
						continue;
					}
				}
				if ((internalWhitespaceRule2.LocalName == null || internalWhitespaceRule2.LocalName == localName) && (internalWhitespaceRule2.NamespaceName == null || internalWhitespaceRule2.NamespaceName == namespaceName))
				{
					return !internalWhitespaceRule2.PreserveSpace;
				}
			}
			return internalWhitespaceRule != null && !internalWhitespaceRule.PreserveSpace;
		}

		// Token: 0x04002281 RID: 8833
		private Hashtable qnames;

		// Token: 0x04002282 RID: 8834
		private ArrayList wildcards;

		// Token: 0x04002283 RID: 8835
		private WhitespaceRuleLookup.InternalWhitespaceRule ruleTemp;

		// Token: 0x04002284 RID: 8836
		private XmlNameTable nameTable;

		// Token: 0x02000461 RID: 1121
		private class InternalWhitespaceRule : WhitespaceRule
		{
			// Token: 0x06002B69 RID: 11113 RVA: 0x001043C5 File Offset: 0x001025C5
			public InternalWhitespaceRule()
			{
			}

			// Token: 0x06002B6A RID: 11114 RVA: 0x001043CD File Offset: 0x001025CD
			public InternalWhitespaceRule(string localName, string namespaceName, bool preserveSpace, int priority)
			{
				this.Init(localName, namespaceName, preserveSpace, priority);
			}

			// Token: 0x06002B6B RID: 11115 RVA: 0x001043E0 File Offset: 0x001025E0
			public void Init(string localName, string namespaceName, bool preserveSpace, int priority)
			{
				base.Init(localName, namespaceName, preserveSpace);
				this.priority = priority;
				if (localName != null && namespaceName != null)
				{
					this.hashCode = localName.GetHashCode();
				}
			}

			// Token: 0x06002B6C RID: 11116 RVA: 0x00104405 File Offset: 0x00102605
			public void Atomize(XmlNameTable nameTable)
			{
				if (base.LocalName != null)
				{
					base.LocalName = nameTable.Add(base.LocalName);
				}
				if (base.NamespaceName != null)
				{
					base.NamespaceName = nameTable.Add(base.NamespaceName);
				}
			}

			// Token: 0x1700082A RID: 2090
			// (get) Token: 0x06002B6D RID: 11117 RVA: 0x0010443B File Offset: 0x0010263B
			public int Priority
			{
				get
				{
					return this.priority;
				}
			}

			// Token: 0x06002B6E RID: 11118 RVA: 0x00104443 File Offset: 0x00102643
			public override int GetHashCode()
			{
				return this.hashCode;
			}

			// Token: 0x06002B6F RID: 11119 RVA: 0x0010444C File Offset: 0x0010264C
			public override bool Equals(object obj)
			{
				WhitespaceRuleLookup.InternalWhitespaceRule internalWhitespaceRule = obj as WhitespaceRuleLookup.InternalWhitespaceRule;
				return base.LocalName == internalWhitespaceRule.LocalName && base.NamespaceName == internalWhitespaceRule.NamespaceName;
			}

			// Token: 0x04002285 RID: 8837
			private int priority;

			// Token: 0x04002286 RID: 8838
			private int hashCode;
		}
	}
}
