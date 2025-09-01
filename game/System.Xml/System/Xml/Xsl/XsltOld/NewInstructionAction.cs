using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000395 RID: 917
	internal class NewInstructionAction : ContainerAction
	{
		// Token: 0x0600252C RID: 9516 RVA: 0x000E1834 File Offset: 0x000DFA34
		internal override void Compile(Compiler compiler)
		{
			XPathNavigator xpathNavigator = compiler.Input.Navigator.Clone();
			this.name = xpathNavigator.Name;
			xpathNavigator.MoveToParent();
			this.parent = xpathNavigator.Name;
			if (compiler.Recurse())
			{
				this.CompileSelectiveTemplate(compiler);
				compiler.ToParent();
			}
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x000E1888 File Offset: 0x000DFA88
		internal void CompileSelectiveTemplate(Compiler compiler)
		{
			NavigatorInput input = compiler.Input;
			do
			{
				if (Ref.Equal(input.NamespaceURI, input.Atoms.UriXsl) && Ref.Equal(input.LocalName, input.Atoms.Fallback))
				{
					this.fallback = true;
					if (compiler.Recurse())
					{
						base.CompileTemplate(compiler);
						compiler.ToParent();
					}
				}
			}
			while (compiler.Advance());
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x000E18F4 File Offset: 0x000DFAF4
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			int state = frame.State;
			if (state != 0)
			{
				if (state != 1)
				{
					return;
				}
			}
			else
			{
				if (!this.fallback)
				{
					throw XsltException.Create("'{0}' is not a recognized extension element.", new string[]
					{
						this.name
					});
				}
				if (this.containedActions != null && this.containedActions.Count > 0)
				{
					processor.PushActionFrame(frame);
					frame.State = 1;
					return;
				}
			}
			frame.Finished();
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x000DB75C File Offset: 0x000D995C
		public NewInstructionAction()
		{
		}

		// Token: 0x04001D3C RID: 7484
		private string name;

		// Token: 0x04001D3D RID: 7485
		private string parent;

		// Token: 0x04001D3E RID: 7486
		private bool fallback;
	}
}
