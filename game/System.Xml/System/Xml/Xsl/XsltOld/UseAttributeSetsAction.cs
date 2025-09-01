using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003BA RID: 954
	internal class UseAttributeSetsAction : CompiledAction
	{
		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x060026C5 RID: 9925 RVA: 0x000E843A File Offset: 0x000E663A
		internal XmlQualifiedName[] UsedSets
		{
			get
			{
				return this.useAttributeSets;
			}
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x000E8444 File Offset: 0x000E6644
		internal override void Compile(Compiler compiler)
		{
			this.useString = compiler.Input.Value;
			if (this.useString.Length == 0)
			{
				this.useAttributeSets = new XmlQualifiedName[0];
				return;
			}
			string[] array = XmlConvert.SplitString(this.useString);
			try
			{
				this.useAttributeSets = new XmlQualifiedName[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this.useAttributeSets[i] = compiler.CreateXPathQName(array[i]);
				}
			}
			catch (XsltException)
			{
				if (!compiler.ForwardCompatibility)
				{
					throw;
				}
				this.useAttributeSets = new XmlQualifiedName[0];
			}
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x000E84E0 File Offset: 0x000E66E0
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			int state = frame.State;
			if (state != 0)
			{
				if (state != 2)
				{
					return;
				}
			}
			else
			{
				frame.Counter = 0;
				frame.State = 2;
			}
			if (frame.Counter < this.useAttributeSets.Length)
			{
				AttributeSetAction attributeSet = processor.RootAction.GetAttributeSet(this.useAttributeSets[frame.Counter]);
				frame.IncrementCounter();
				processor.PushActionFrame(attributeSet, frame.NodeSet);
				return;
			}
			frame.Finished();
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x000DB152 File Offset: 0x000D9352
		public UseAttributeSetsAction()
		{
		}

		// Token: 0x04001E70 RID: 7792
		private XmlQualifiedName[] useAttributeSets;

		// Token: 0x04001E71 RID: 7793
		private string useString;

		// Token: 0x04001E72 RID: 7794
		private const int ProcessingSets = 2;
	}
}
