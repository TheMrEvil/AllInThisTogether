using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003B2 RID: 946
	internal class TemplateLookupActionDbg : TemplateLookupAction
	{
		// Token: 0x060026A4 RID: 9892 RVA: 0x000E7F78 File Offset: 0x000E6178
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			if (this.mode == Compiler.BuiltInMode)
			{
				this.mode = processor.GetPrevioseMode();
			}
			processor.SetCurrentMode(this.mode);
			Action action;
			if (this.mode != null)
			{
				action = ((this.importsOf == null) ? processor.Stylesheet.FindTemplate(processor, frame.Node, this.mode) : this.importsOf.FindTemplateImports(processor, frame.Node, this.mode));
			}
			else
			{
				action = ((this.importsOf == null) ? processor.Stylesheet.FindTemplate(processor, frame.Node) : this.importsOf.FindTemplateImports(processor, frame.Node));
			}
			if (action == null && processor.RootAction.builtInSheet != null)
			{
				action = processor.RootAction.builtInSheet.FindTemplate(processor, frame.Node, Compiler.BuiltInMode);
			}
			if (action == null)
			{
				action = base.BuiltInTemplate(frame.Node);
			}
			if (action != null)
			{
				frame.SetAction(action);
				return;
			}
			frame.Finished();
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x000E8079 File Offset: 0x000E6279
		public TemplateLookupActionDbg()
		{
		}
	}
}
