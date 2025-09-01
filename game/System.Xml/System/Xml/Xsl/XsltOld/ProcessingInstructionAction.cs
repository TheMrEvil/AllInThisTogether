using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200039D RID: 925
	internal class ProcessingInstructionAction : ContainerAction
	{
		// Token: 0x06002574 RID: 9588 RVA: 0x000DB75C File Offset: 0x000D995C
		internal ProcessingInstructionAction()
		{
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x000E2DD8 File Offset: 0x000E0FD8
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			base.CheckRequiredAttribute(compiler, this.nameAvt, "name");
			if (this.nameAvt.IsConstant)
			{
				this.name = this.nameAvt.Evaluate(null, null);
				this.nameAvt = null;
				if (!ProcessingInstructionAction.IsProcessingInstructionName(this.name))
				{
					this.name = null;
				}
			}
			if (compiler.Recurse())
			{
				base.CompileTemplate(compiler);
				compiler.ToParent();
			}
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x000E2E50 File Offset: 0x000E1050
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Name))
			{
				this.nameAvt = Avt.CompileAvt(compiler, value);
				return true;
			}
			return false;
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x000E2E98 File Offset: 0x000E1098
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			switch (frame.State)
			{
			case 0:
				if (this.nameAvt == null)
				{
					frame.StoredOutput = this.name;
					if (this.name == null)
					{
						frame.Finished();
						return;
					}
				}
				else
				{
					frame.StoredOutput = this.nameAvt.Evaluate(processor, frame);
					if (!ProcessingInstructionAction.IsProcessingInstructionName(frame.StoredOutput))
					{
						frame.Finished();
						return;
					}
				}
				break;
			case 1:
				if (!processor.EndEvent(XPathNodeType.ProcessingInstruction))
				{
					frame.State = 1;
					return;
				}
				frame.Finished();
				return;
			case 2:
				goto IL_B5;
			case 3:
				break;
			default:
				goto IL_B5;
			}
			if (!processor.BeginEvent(XPathNodeType.ProcessingInstruction, string.Empty, frame.StoredOutput, string.Empty, false))
			{
				frame.State = 3;
				return;
			}
			processor.PushActionFrame(frame);
			frame.State = 1;
			return;
			IL_B5:
			frame.Finished();
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x000E2F60 File Offset: 0x000E1160
		internal static bool IsProcessingInstructionName(string name)
		{
			if (name == null)
			{
				return false;
			}
			int length = name.Length;
			int num = 0;
			XmlCharType instance = XmlCharType.Instance;
			while (num < length && instance.IsWhiteSpace(name[num]))
			{
				num++;
			}
			if (num >= length)
			{
				return false;
			}
			int num2 = ValidateNames.ParseNCName(name, num);
			if (num2 == 0)
			{
				return false;
			}
			num += num2;
			while (num < length && instance.IsWhiteSpace(name[num]))
			{
				num++;
			}
			return num >= length && (length != 3 || (name[0] != 'X' && name[0] != 'x') || (name[1] != 'M' && name[1] != 'm') || (name[2] != 'L' && name[2] != 'l'));
		}

		// Token: 0x04001D79 RID: 7545
		private const int NameEvaluated = 2;

		// Token: 0x04001D7A RID: 7546
		private const int NameReady = 3;

		// Token: 0x04001D7B RID: 7547
		private Avt nameAvt;

		// Token: 0x04001D7C RID: 7548
		private string name;

		// Token: 0x04001D7D RID: 7549
		private const char CharX = 'X';

		// Token: 0x04001D7E RID: 7550
		private const char Charx = 'x';

		// Token: 0x04001D7F RID: 7551
		private const char CharM = 'M';

		// Token: 0x04001D80 RID: 7552
		private const char Charm = 'm';

		// Token: 0x04001D81 RID: 7553
		private const char CharL = 'L';

		// Token: 0x04001D82 RID: 7554
		private const char Charl = 'l';
	}
}
