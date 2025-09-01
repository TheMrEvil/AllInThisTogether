using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003BF RID: 959
	internal class WithParamAction : VariableAction
	{
		// Token: 0x060026E0 RID: 9952 RVA: 0x000E8995 File Offset: 0x000E6B95
		internal WithParamAction() : base(VariableType.WithParameter)
		{
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x000E89A0 File Offset: 0x000E6BA0
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			base.CheckRequiredAttribute(compiler, this.name, "name");
			if (compiler.Recurse())
			{
				base.CompileTemplate(compiler);
				compiler.ToParent();
				if (this.selectKey != -1 && this.containedActions != null)
				{
					throw XsltException.Create("The variable or parameter '{0}' cannot have both a 'select' attribute and non-empty content.", new string[]
					{
						this.nameStr
					});
				}
			}
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x000E8A08 File Offset: 0x000E6C08
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			int state = frame.State;
			if (state != 0)
			{
				if (state != 1)
				{
					return;
				}
				RecordOutput recordOutput = processor.PopOutput();
				processor.SetParameter(this.name, ((NavigatorOutput)recordOutput).Navigator);
				frame.Finished();
				return;
			}
			else
			{
				if (this.selectKey != -1)
				{
					object value = processor.RunQuery(frame, this.selectKey);
					processor.SetParameter(this.name, value);
					frame.Finished();
					return;
				}
				if (this.containedActions == null)
				{
					processor.SetParameter(this.name, string.Empty);
					frame.Finished();
					return;
				}
				NavigatorOutput output = new NavigatorOutput(this.baseUri);
				processor.PushOutput(output);
				processor.PushActionFrame(frame);
				frame.State = 1;
				return;
			}
		}
	}
}
