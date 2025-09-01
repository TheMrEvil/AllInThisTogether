using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security;
using System.Xml.XPath;
using System.Xml.Xsl.Runtime;
using MS.Internal.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003A8 RID: 936
	internal class RootAction : TemplateBaseAction
	{
		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06002634 RID: 9780 RVA: 0x000E58E4 File Offset: 0x000E3AE4
		internal XsltOutput Output
		{
			get
			{
				if (this.output == null)
				{
					this.output = new XsltOutput();
				}
				return this.output;
			}
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x000E58FF File Offset: 0x000E3AFF
		internal override void Compile(Compiler compiler)
		{
			base.CompileDocument(compiler, false);
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x000E5909 File Offset: 0x000E3B09
		internal void InsertKey(XmlQualifiedName name, int MatchKey, int UseKey)
		{
			if (this.keyList == null)
			{
				this.keyList = new List<Key>();
			}
			this.keyList.Add(new Key(name, MatchKey, UseKey));
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x000E5934 File Offset: 0x000E3B34
		internal AttributeSetAction GetAttributeSet(XmlQualifiedName name)
		{
			AttributeSetAction attributeSetAction = (AttributeSetAction)this.attributeSetTable[name];
			if (attributeSetAction == null)
			{
				throw XsltException.Create("A reference to attribute set '{0}' cannot be resolved. An 'xsl:attribute-set' of this name must be declared at the top level of the stylesheet.", new string[]
				{
					name.ToString()
				});
			}
			return attributeSetAction;
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x000E5974 File Offset: 0x000E3B74
		public void PorcessAttributeSets(Stylesheet rootStylesheet)
		{
			this.MirgeAttributeSets(rootStylesheet);
			foreach (object obj in this.attributeSetTable.Values)
			{
				AttributeSetAction attributeSetAction = (AttributeSetAction)obj;
				if (attributeSetAction.containedActions != null)
				{
					attributeSetAction.containedActions.Reverse();
				}
			}
			this.CheckAttributeSets_RecurceInList(new Hashtable(), this.attributeSetTable.Keys);
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x000E59FC File Offset: 0x000E3BFC
		private void MirgeAttributeSets(Stylesheet stylesheet)
		{
			if (stylesheet.AttributeSetTable != null)
			{
				foreach (object obj in stylesheet.AttributeSetTable.Values)
				{
					AttributeSetAction attributeSetAction = (AttributeSetAction)obj;
					ArrayList containedActions = attributeSetAction.containedActions;
					AttributeSetAction attributeSetAction2 = (AttributeSetAction)this.attributeSetTable[attributeSetAction.Name];
					if (attributeSetAction2 == null)
					{
						attributeSetAction2 = new AttributeSetAction();
						attributeSetAction2.name = attributeSetAction.Name;
						attributeSetAction2.containedActions = new ArrayList();
						this.attributeSetTable[attributeSetAction.Name] = attributeSetAction2;
					}
					ArrayList containedActions2 = attributeSetAction2.containedActions;
					if (containedActions != null)
					{
						int num = containedActions.Count - 1;
						while (0 <= num)
						{
							containedActions2.Add(containedActions[num]);
							num--;
						}
					}
				}
			}
			foreach (object obj2 in stylesheet.Imports)
			{
				Stylesheet stylesheet2 = (Stylesheet)obj2;
				this.MirgeAttributeSets(stylesheet2);
			}
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x000E5B38 File Offset: 0x000E3D38
		private void CheckAttributeSets_RecurceInList(Hashtable markTable, ICollection setQNames)
		{
			foreach (object obj in setQNames)
			{
				XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
				object obj2 = markTable[xmlQualifiedName];
				if (obj2 == "P")
				{
					throw XsltException.Create("Circular reference in the definition of attribute set '{0}'.", new string[]
					{
						xmlQualifiedName.ToString()
					});
				}
				if (obj2 != "D")
				{
					markTable[xmlQualifiedName] = "P";
					this.CheckAttributeSets_RecurceInContainer(markTable, this.GetAttributeSet(xmlQualifiedName));
					markTable[xmlQualifiedName] = "D";
				}
			}
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x000E5BE0 File Offset: 0x000E3DE0
		private void CheckAttributeSets_RecurceInContainer(Hashtable markTable, ContainerAction container)
		{
			if (container.containedActions == null)
			{
				return;
			}
			foreach (object obj in container.containedActions)
			{
				Action action = (Action)obj;
				if (action is UseAttributeSetsAction)
				{
					this.CheckAttributeSets_RecurceInList(markTable, ((UseAttributeSetsAction)action).UsedSets);
				}
				else if (action is ContainerAction)
				{
					this.CheckAttributeSets_RecurceInContainer(markTable, (ContainerAction)action);
				}
			}
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x000E5C6C File Offset: 0x000E3E6C
		internal void AddDecimalFormat(XmlQualifiedName name, DecimalFormat formatinfo)
		{
			DecimalFormat decimalFormat = (DecimalFormat)this.decimalFormatTable[name];
			if (decimalFormat != null)
			{
				NumberFormatInfo info = decimalFormat.info;
				NumberFormatInfo info2 = formatinfo.info;
				if (info.NumberDecimalSeparator != info2.NumberDecimalSeparator || info.NumberGroupSeparator != info2.NumberGroupSeparator || info.PositiveInfinitySymbol != info2.PositiveInfinitySymbol || info.NegativeSign != info2.NegativeSign || info.NaNSymbol != info2.NaNSymbol || info.PercentSymbol != info2.PercentSymbol || info.PerMilleSymbol != info2.PerMilleSymbol || decimalFormat.zeroDigit != formatinfo.zeroDigit || decimalFormat.digit != formatinfo.digit || decimalFormat.patternSeparator != formatinfo.patternSeparator)
				{
					throw XsltException.Create("Decimal format '{0}' has a duplicate declaration.", new string[]
					{
						name.ToString()
					});
				}
			}
			this.decimalFormatTable[name] = formatinfo;
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x000E5D7B File Offset: 0x000E3F7B
		internal DecimalFormat GetDecimalFormat(XmlQualifiedName name)
		{
			return this.decimalFormatTable[name] as DecimalFormat;
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x0600263E RID: 9790 RVA: 0x000E5D8E File Offset: 0x000E3F8E
		internal List<Key> KeyList
		{
			get
			{
				return this.keyList;
			}
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x000E5D98 File Offset: 0x000E3F98
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			switch (frame.State)
			{
			case 0:
			{
				frame.AllocateVariables(this.variableCount);
				XPathNavigator xpathNavigator = processor.Document.Clone();
				xpathNavigator.MoveToRoot();
				frame.InitNodeSet(new XPathSingletonIterator(xpathNavigator));
				if (this.containedActions != null && this.containedActions.Count > 0)
				{
					processor.PushActionFrame(frame);
				}
				frame.State = 2;
				return;
			}
			case 1:
				break;
			case 2:
				frame.NextNode(processor);
				if (processor.Debugger != null)
				{
					processor.PopDebuggerStack();
				}
				processor.PushTemplateLookup(frame.NodeSet, null, null);
				frame.State = 3;
				return;
			case 3:
				frame.Finished();
				break;
			default:
				return;
			}
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x000E5E44 File Offset: 0x000E4044
		public RootAction()
		{
		}

		// Token: 0x04001DDB RID: 7643
		private const int QueryInitialized = 2;

		// Token: 0x04001DDC RID: 7644
		private const int RootProcessed = 3;

		// Token: 0x04001DDD RID: 7645
		private Hashtable attributeSetTable = new Hashtable();

		// Token: 0x04001DDE RID: 7646
		private Hashtable decimalFormatTable = new Hashtable();

		// Token: 0x04001DDF RID: 7647
		private List<Key> keyList;

		// Token: 0x04001DE0 RID: 7648
		private XsltOutput output;

		// Token: 0x04001DE1 RID: 7649
		public Stylesheet builtInSheet;

		// Token: 0x04001DE2 RID: 7650
		public PermissionSet permissions;
	}
}
