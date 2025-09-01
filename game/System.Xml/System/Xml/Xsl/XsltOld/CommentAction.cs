using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200035C RID: 860
	internal class CommentAction : ContainerAction
	{
		// Token: 0x0600236F RID: 9071 RVA: 0x000DC119 File Offset: 0x000DA319
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			if (compiler.Recurse())
			{
				base.CompileTemplate(compiler);
				compiler.ToParent();
			}
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x000DC138 File Offset: 0x000DA338
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			int state = frame.State;
			if (state != 0)
			{
				if (state != 1)
				{
					return;
				}
				if (processor.EndEvent(XPathNodeType.Comment))
				{
					frame.Finished();
				}
			}
			else if (processor.BeginEvent(XPathNodeType.Comment, string.Empty, string.Empty, string.Empty, false))
			{
				processor.PushActionFrame(frame);
				frame.State = 1;
				return;
			}
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x000DB75C File Offset: 0x000D995C
		public CommentAction()
		{
		}
	}
}
