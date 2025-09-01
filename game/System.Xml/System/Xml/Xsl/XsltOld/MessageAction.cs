using System;
using System.Globalization;
using System.IO;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000390 RID: 912
	internal class MessageAction : ContainerAction
	{
		// Token: 0x06002500 RID: 9472 RVA: 0x000DC119 File Offset: 0x000DA319
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			if (compiler.Recurse())
			{
				base.CompileTemplate(compiler);
				compiler.ToParent();
			}
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x000E129C File Offset: 0x000DF49C
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Terminate))
			{
				this._Terminate = compiler.GetYesNo(value);
				return true;
			}
			return false;
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x000E12E4 File Offset: 0x000DF4E4
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			int state = frame.State;
			if (state == 0)
			{
				TextOnlyOutput output = new TextOnlyOutput(processor, new StringWriter(CultureInfo.InvariantCulture));
				processor.PushOutput(output);
				processor.PushActionFrame(frame);
				frame.State = 1;
				return;
			}
			if (state != 1)
			{
				return;
			}
			TextOnlyOutput textOnlyOutput = processor.PopOutput() as TextOnlyOutput;
			Console.WriteLine(textOnlyOutput.Writer.ToString());
			if (this._Terminate)
			{
				throw XsltException.Create("Transform terminated: '{0}'.", new string[]
				{
					textOnlyOutput.Writer.ToString()
				});
			}
			frame.Finished();
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x000DB75C File Offset: 0x000D995C
		public MessageAction()
		{
		}

		// Token: 0x04001D2C RID: 7468
		private bool _Terminate;
	}
}
