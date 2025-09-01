using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000368 RID: 872
	internal sealed class CopyNodeSetAction : Action
	{
		// Token: 0x0600241C RID: 9244 RVA: 0x000DF221 File Offset: 0x000DD421
		internal static CopyNodeSetAction GetAction()
		{
			return CopyNodeSetAction.s_Action;
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x000DF228 File Offset: 0x000DD428
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			while (processor.CanContinue)
			{
				switch (frame.State)
				{
				case 0:
					if (!frame.NextNode(processor))
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
				{
					XPathNodeType nodeType = frame.Node.NodeType;
					if (nodeType == XPathNodeType.Element || nodeType == XPathNodeType.Root)
					{
						processor.PushActionFrame(CopyNamespacesAction.GetAction(), frame.NodeSet);
						frame.State = 4;
						return;
					}
					if (CopyNodeSetAction.SendTextEvent(processor, frame.Node))
					{
						frame.State = 7;
						continue;
					}
					return;
				}
				case 4:
					processor.PushActionFrame(CopyAttributesAction.GetAction(), frame.NodeSet);
					frame.State = 5;
					return;
				case 5:
					if (frame.Node.HasChildren)
					{
						processor.PushActionFrame(CopyNodeSetAction.GetAction(), frame.Node.SelectChildren(XPathNodeType.All));
						frame.State = 6;
						return;
					}
					frame.State = 7;
					goto IL_107;
				case 6:
					frame.State = 7;
					continue;
				case 7:
					goto IL_107;
				default:
					return;
				}
				if (CopyNodeSetAction.SendBeginEvent(processor, frame.Node))
				{
					frame.State = 3;
					continue;
				}
				break;
				IL_107:
				if (!CopyNodeSetAction.SendEndEvent(processor, frame.Node))
				{
					break;
				}
				frame.State = 0;
			}
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x000DF35C File Offset: 0x000DD55C
		private static bool SendBeginEvent(Processor processor, XPathNavigator node)
		{
			return processor.CopyBeginEvent(node, node.IsEmptyElement);
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x000DF36B File Offset: 0x000DD56B
		private static bool SendTextEvent(Processor processor, XPathNavigator node)
		{
			return processor.CopyTextEvent(node);
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x000DF374 File Offset: 0x000DD574
		private static bool SendEndEvent(Processor processor, XPathNavigator node)
		{
			return processor.CopyEndEvent(node);
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x000DC2C5 File Offset: 0x000DA4C5
		public CopyNodeSetAction()
		{
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x000DF37D File Offset: 0x000DD57D
		// Note: this type is marked as 'beforefieldinit'.
		static CopyNodeSetAction()
		{
		}

		// Token: 0x04001CD6 RID: 7382
		private const int BeginEvent = 2;

		// Token: 0x04001CD7 RID: 7383
		private const int Contents = 3;

		// Token: 0x04001CD8 RID: 7384
		private const int Namespaces = 4;

		// Token: 0x04001CD9 RID: 7385
		private const int Attributes = 5;

		// Token: 0x04001CDA RID: 7386
		private const int Subtree = 6;

		// Token: 0x04001CDB RID: 7387
		private const int EndEvent = 7;

		// Token: 0x04001CDC RID: 7388
		private static CopyNodeSetAction s_Action = new CopyNodeSetAction();
	}
}
