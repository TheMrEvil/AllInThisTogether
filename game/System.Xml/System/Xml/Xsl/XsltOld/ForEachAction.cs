using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000389 RID: 905
	internal class ForEachAction : ContainerAction
	{
		// Token: 0x060024C3 RID: 9411 RVA: 0x000E0228 File Offset: 0x000DE428
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			base.CheckRequiredAttribute(compiler, this.selectKey != -1, "select");
			compiler.CanHaveApplyImports = false;
			if (compiler.Recurse())
			{
				this.CompileSortElements(compiler);
				base.CompileTemplate(compiler);
				compiler.ToParent();
			}
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x000E0278 File Offset: 0x000DE478
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Select))
			{
				this.selectKey = compiler.AddQuery(value);
				return true;
			}
			return false;
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x000E02C0 File Offset: 0x000DE4C0
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			switch (frame.State)
			{
			case 0:
				if (this.sortContainer != null)
				{
					processor.InitSortArray();
					processor.PushActionFrame(this.sortContainer, frame.NodeSet);
					frame.State = 2;
					return;
				}
				break;
			case 1:
				return;
			case 2:
				break;
			case 3:
				goto IL_82;
			case 4:
				goto IL_9B;
			case 5:
				frame.State = 3;
				goto IL_82;
			default:
				return;
			}
			frame.InitNewNodeSet(processor.StartQuery(frame.NodeSet, this.selectKey));
			if (this.sortContainer != null)
			{
				frame.SortNewNodeSet(processor, processor.SortArray);
			}
			frame.State = 3;
			IL_82:
			if (!frame.NewNextNode(processor))
			{
				frame.Finished();
				return;
			}
			frame.State = 4;
			IL_9B:
			processor.PushActionFrame(frame, frame.NewNodeSet);
			frame.State = 5;
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x000E0388 File Offset: 0x000DE588
		protected void CompileSortElements(Compiler compiler)
		{
			NavigatorInput input = compiler.Input;
			for (;;)
			{
				switch (input.NodeType)
				{
				case XPathNodeType.Element:
					if (!Ref.Equal(input.NamespaceURI, input.Atoms.UriXsl) || !Ref.Equal(input.LocalName, input.Atoms.Sort))
					{
						return;
					}
					if (this.sortContainer == null)
					{
						this.sortContainer = new ContainerAction();
					}
					this.sortContainer.AddAction(compiler.CreateSortAction());
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

		// Token: 0x060024C7 RID: 9415 RVA: 0x000E0430 File Offset: 0x000DE630
		public ForEachAction()
		{
		}

		// Token: 0x04001D0B RID: 7435
		private const int ProcessedSort = 2;

		// Token: 0x04001D0C RID: 7436
		private const int ProcessNextNode = 3;

		// Token: 0x04001D0D RID: 7437
		private const int PositionAdvanced = 4;

		// Token: 0x04001D0E RID: 7438
		private const int ContentsProcessed = 5;

		// Token: 0x04001D0F RID: 7439
		private int selectKey = -1;

		// Token: 0x04001D10 RID: 7440
		private ContainerAction sortContainer;
	}
}
