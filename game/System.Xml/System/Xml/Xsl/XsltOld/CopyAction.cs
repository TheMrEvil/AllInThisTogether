using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000364 RID: 868
	internal class CopyAction : ContainerAction
	{
		// Token: 0x06002407 RID: 9223 RVA: 0x000DEDA3 File Offset: 0x000DCFA3
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			if (compiler.Recurse())
			{
				base.CompileTemplate(compiler);
				compiler.ToParent();
			}
			if (this.containedActions == null)
			{
				this.empty = true;
			}
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x000DEDD4 File Offset: 0x000DCFD4
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.UseAttributeSets))
			{
				this.useAttributeSets = value;
				base.AddAction(compiler.CreateUseAttributeSetsAction());
				return true;
			}
			return false;
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x000DEE24 File Offset: 0x000DD024
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			while (processor.CanContinue)
			{
				switch (frame.State)
				{
				case 0:
					if (Processor.IsRoot(frame.Node))
					{
						processor.PushActionFrame(frame);
						frame.State = 8;
						return;
					}
					if (!processor.CopyBeginEvent(frame.Node, this.empty))
					{
						return;
					}
					frame.State = 5;
					break;
				case 1:
				case 2:
				case 3:
				case 4:
					return;
				case 5:
					frame.State = 6;
					if (frame.Node.NodeType == XPathNodeType.Element)
					{
						processor.PushActionFrame(CopyNamespacesAction.GetAction(), frame.NodeSet);
						return;
					}
					break;
				case 6:
					if (frame.Node.NodeType == XPathNodeType.Element && !this.empty)
					{
						processor.PushActionFrame(frame);
						frame.State = 7;
						return;
					}
					if (!processor.CopyTextEvent(frame.Node))
					{
						return;
					}
					frame.State = 7;
					break;
				case 7:
					if (processor.CopyEndEvent(frame.Node))
					{
						frame.Finished();
						return;
					}
					return;
				case 8:
					frame.Finished();
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x000DB75C File Offset: 0x000D995C
		public CopyAction()
		{
		}

		// Token: 0x04001CC3 RID: 7363
		private const int CopyText = 4;

		// Token: 0x04001CC4 RID: 7364
		private const int NamespaceCopy = 5;

		// Token: 0x04001CC5 RID: 7365
		private const int ContentsCopy = 6;

		// Token: 0x04001CC6 RID: 7366
		private const int ProcessChildren = 7;

		// Token: 0x04001CC7 RID: 7367
		private const int ChildrenOnly = 8;

		// Token: 0x04001CC8 RID: 7368
		private string useAttributeSets;

		// Token: 0x04001CC9 RID: 7369
		private bool empty;
	}
}
