using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000367 RID: 871
	internal sealed class CopyNamespacesAction : Action
	{
		// Token: 0x06002418 RID: 9240 RVA: 0x000DF146 File Offset: 0x000DD346
		internal static CopyNamespacesAction GetAction()
		{
			return CopyNamespacesAction.s_Action;
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x000DF150 File Offset: 0x000DD350
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			while (processor.CanContinue)
			{
				switch (frame.State)
				{
				case 0:
					if (!frame.Node.MoveToFirstNamespace(XPathNamespaceScope.ExcludeXml))
					{
						frame.Finished();
						return;
					}
					frame.State = 2;
					break;
				case 1:
				case 3:
					return;
				case 2:
					break;
				case 4:
					if (processor.EndEvent(XPathNodeType.Namespace))
					{
						frame.State = 5;
						continue;
					}
					return;
				case 5:
					if (frame.Node.MoveToNextNamespace(XPathNamespaceScope.ExcludeXml))
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
				if (!processor.BeginEvent(XPathNodeType.Namespace, null, frame.Node.LocalName, frame.Node.Value, false))
				{
					break;
				}
				frame.State = 4;
			}
		}

		// Token: 0x0600241A RID: 9242 RVA: 0x000DC2C5 File Offset: 0x000DA4C5
		public CopyNamespacesAction()
		{
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x000DF215 File Offset: 0x000DD415
		// Note: this type is marked as 'beforefieldinit'.
		static CopyNamespacesAction()
		{
		}

		// Token: 0x04001CD1 RID: 7377
		private const int BeginEvent = 2;

		// Token: 0x04001CD2 RID: 7378
		private const int TextEvent = 3;

		// Token: 0x04001CD3 RID: 7379
		private const int EndEvent = 4;

		// Token: 0x04001CD4 RID: 7380
		private const int Advance = 5;

		// Token: 0x04001CD5 RID: 7381
		private static CopyNamespacesAction s_Action = new CopyNamespacesAction();
	}
}
