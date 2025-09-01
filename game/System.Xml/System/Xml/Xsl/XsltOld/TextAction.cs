using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003B5 RID: 949
	internal class TextAction : CompiledAction
	{
		// Token: 0x060026AE RID: 9902 RVA: 0x000E818C File Offset: 0x000E638C
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			this.CompileContent(compiler);
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x000E819C File Offset: 0x000E639C
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.DisableOutputEscaping))
			{
				this.disableOutputEscaping = compiler.GetYesNo(value);
				return true;
			}
			return false;
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x000E81E4 File Offset: 0x000E63E4
		private void CompileContent(Compiler compiler)
		{
			if (compiler.Recurse())
			{
				NavigatorInput input = compiler.Input;
				this.text = string.Empty;
				for (;;)
				{
					XPathNodeType nodeType = input.NodeType;
					if (nodeType - XPathNodeType.Text > 2)
					{
						if (nodeType - XPathNodeType.ProcessingInstruction > 1)
						{
							break;
						}
					}
					else
					{
						this.text += input.Value;
					}
					if (!compiler.Advance())
					{
						goto Block_4;
					}
				}
				throw compiler.UnexpectedKeyword();
				Block_4:
				compiler.ToParent();
			}
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x000E824F File Offset: 0x000E644F
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			if (frame.State == 0 && processor.TextEvent(this.text, this.disableOutputEscaping))
			{
				frame.Finished();
			}
		}

		// Token: 0x060026B2 RID: 9906 RVA: 0x000DB152 File Offset: 0x000D9352
		public TextAction()
		{
		}

		// Token: 0x04001E68 RID: 7784
		private bool disableOutputEscaping;

		// Token: 0x04001E69 RID: 7785
		private string text;
	}
}
