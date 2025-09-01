using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003BB RID: 955
	internal class ValueOfAction : CompiledAction
	{
		// Token: 0x060026C9 RID: 9929 RVA: 0x000E854E File Offset: 0x000E674E
		internal static Action BuiltInRule()
		{
			return ValueOfAction.s_BuiltInRule;
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x000E8555 File Offset: 0x000E6755
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			base.CheckRequiredAttribute(compiler, this.selectKey != -1, "select");
			base.CheckEmpty(compiler);
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x000E8580 File Offset: 0x000E6780
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Select))
			{
				this.selectKey = compiler.AddQuery(value);
			}
			else
			{
				if (!Ref.Equal(localName, compiler.Atoms.DisableOutputEscaping))
				{
					return false;
				}
				this.disableOutputEscaping = compiler.GetYesNo(value);
			}
			return true;
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x000E85EC File Offset: 0x000E67EC
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			int state = frame.State;
			if (state != 0)
			{
				if (state != 2)
				{
					return;
				}
				processor.TextEvent(frame.StoredOutput);
				frame.Finished();
				return;
			}
			else
			{
				string text = processor.ValueOf(frame, this.selectKey);
				if (processor.TextEvent(text, this.disableOutputEscaping))
				{
					frame.Finished();
					return;
				}
				frame.StoredOutput = text;
				frame.State = 2;
				return;
			}
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x000E864E File Offset: 0x000E684E
		public ValueOfAction()
		{
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x000E865D File Offset: 0x000E685D
		// Note: this type is marked as 'beforefieldinit'.
		static ValueOfAction()
		{
		}

		// Token: 0x04001E73 RID: 7795
		private const int ResultStored = 2;

		// Token: 0x04001E74 RID: 7796
		private int selectKey = -1;

		// Token: 0x04001E75 RID: 7797
		private bool disableOutputEscaping;

		// Token: 0x04001E76 RID: 7798
		private static Action s_BuiltInRule = new BuiltInRuleTextAction();
	}
}
