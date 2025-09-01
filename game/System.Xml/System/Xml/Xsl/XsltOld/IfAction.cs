using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200038C RID: 908
	internal class IfAction : ContainerAction
	{
		// Token: 0x060024DC RID: 9436 RVA: 0x000E0DAB File Offset: 0x000DEFAB
		internal IfAction(IfAction.ConditionType type)
		{
			this.type = type;
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x000E0DC1 File Offset: 0x000DEFC1
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			if (this.type != IfAction.ConditionType.ConditionOtherwise)
			{
				base.CheckRequiredAttribute(compiler, this.testKey != -1, "test");
			}
			if (compiler.Recurse())
			{
				base.CompileTemplate(compiler);
				compiler.ToParent();
			}
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x000E0E04 File Offset: 0x000DF004
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (!Ref.Equal(localName, compiler.Atoms.Test))
			{
				return false;
			}
			if (this.type == IfAction.ConditionType.ConditionOtherwise)
			{
				return false;
			}
			this.testKey = compiler.AddBooleanQuery(value);
			return true;
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x000E0E58 File Offset: 0x000DF058
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			int state = frame.State;
			if (state != 0)
			{
				if (state != 1)
				{
					return;
				}
				if (this.type == IfAction.ConditionType.ConditionWhen || this.type == IfAction.ConditionType.ConditionOtherwise)
				{
					frame.Exit();
				}
				frame.Finished();
				return;
			}
			else
			{
				if ((this.type == IfAction.ConditionType.ConditionIf || this.type == IfAction.ConditionType.ConditionWhen) && !processor.EvaluateBoolean(frame, this.testKey))
				{
					frame.Finished();
					return;
				}
				processor.PushActionFrame(frame);
				frame.State = 1;
				return;
			}
		}

		// Token: 0x04001D1D RID: 7453
		private IfAction.ConditionType type;

		// Token: 0x04001D1E RID: 7454
		private int testKey = -1;

		// Token: 0x0200038D RID: 909
		internal enum ConditionType
		{
			// Token: 0x04001D20 RID: 7456
			ConditionIf,
			// Token: 0x04001D21 RID: 7457
			ConditionWhen,
			// Token: 0x04001D22 RID: 7458
			ConditionOtherwise
		}
	}
}
