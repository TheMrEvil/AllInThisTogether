using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000352 RID: 850
	internal class ApplyImportsAction : CompiledAction
	{
		// Token: 0x06002330 RID: 9008 RVA: 0x000DB0D6 File Offset: 0x000D92D6
		internal override void Compile(Compiler compiler)
		{
			base.CheckEmpty(compiler);
			if (!compiler.CanHaveApplyImports)
			{
				throw XsltException.Create("The 'xsl:apply-imports' instruction cannot be included within the content of an 'xsl:for-each' instruction or within an 'xsl:template' instruction without the 'match' attribute.", Array.Empty<string>());
			}
			this.mode = compiler.CurrentMode;
			this.stylesheet = compiler.CompiledStylesheet;
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x000DB110 File Offset: 0x000D9310
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			int state = frame.State;
			if (state == 0)
			{
				processor.PushTemplateLookup(frame.NodeSet, this.mode, this.stylesheet);
				frame.State = 2;
				return;
			}
			if (state != 2)
			{
				return;
			}
			frame.Finished();
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x000DB152 File Offset: 0x000D9352
		public ApplyImportsAction()
		{
		}

		// Token: 0x04001C6C RID: 7276
		private XmlQualifiedName mode;

		// Token: 0x04001C6D RID: 7277
		private Stylesheet stylesheet;

		// Token: 0x04001C6E RID: 7278
		private const int TemplateProcessed = 2;
	}
}
