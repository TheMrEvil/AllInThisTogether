using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000365 RID: 869
	internal sealed class CopyAttributesAction : Action
	{
		// Token: 0x0600240B RID: 9227 RVA: 0x000DEF2E File Offset: 0x000DD12E
		internal static CopyAttributesAction GetAction()
		{
			return CopyAttributesAction.s_Action;
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x000DEF38 File Offset: 0x000DD138
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			while (processor.CanContinue)
			{
				switch (frame.State)
				{
				case 0:
					if (!frame.Node.HasAttributes || !frame.Node.MoveToFirstAttribute())
					{
						frame.Finished();
						return;
					}
					frame.State = 2;
					break;
				case 1:
					return;
				case 2:
					break;
				case 3:
					if (CopyAttributesAction.SendTextEvent(processor, frame.Node))
					{
						frame.State = 4;
						continue;
					}
					return;
				case 4:
					if (CopyAttributesAction.SendEndEvent(processor, frame.Node))
					{
						frame.State = 5;
						continue;
					}
					return;
				case 5:
					if (frame.Node.MoveToNextAttribute())
					{
						frame.State = 2;
						continue;
					}
					frame.Node.MoveToParent();
					frame.Finished();
					return;
				default:
					return;
				}
				if (!CopyAttributesAction.SendBeginEvent(processor, frame.Node))
				{
					break;
				}
				frame.State = 3;
			}
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x000DF011 File Offset: 0x000DD211
		private static bool SendBeginEvent(Processor processor, XPathNavigator node)
		{
			return processor.BeginEvent(XPathNodeType.Attribute, node.Prefix, node.LocalName, node.NamespaceURI, false);
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x000DF02D File Offset: 0x000DD22D
		private static bool SendTextEvent(Processor processor, XPathNavigator node)
		{
			return processor.TextEvent(node.Value);
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x000DF03B File Offset: 0x000DD23B
		private static bool SendEndEvent(Processor processor, XPathNavigator node)
		{
			return processor.EndEvent(XPathNodeType.Attribute);
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x000DC2C5 File Offset: 0x000DA4C5
		public CopyAttributesAction()
		{
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x000DF044 File Offset: 0x000DD244
		// Note: this type is marked as 'beforefieldinit'.
		static CopyAttributesAction()
		{
		}

		// Token: 0x04001CCA RID: 7370
		private const int BeginEvent = 2;

		// Token: 0x04001CCB RID: 7371
		private const int TextEvent = 3;

		// Token: 0x04001CCC RID: 7372
		private const int EndEvent = 4;

		// Token: 0x04001CCD RID: 7373
		private const int Advance = 5;

		// Token: 0x04001CCE RID: 7374
		private static CopyAttributesAction s_Action = new CopyAttributesAction();
	}
}
