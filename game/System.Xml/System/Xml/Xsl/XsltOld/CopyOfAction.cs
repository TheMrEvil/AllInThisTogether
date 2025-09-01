using System;
using System.Xml.XPath;
using MS.Internal.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000369 RID: 873
	internal class CopyOfAction : CompiledAction
	{
		// Token: 0x06002423 RID: 9251 RVA: 0x000DF389 File Offset: 0x000DD589
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			base.CheckRequiredAttribute(compiler, this.selectKey != -1, "select");
			base.CheckEmpty(compiler);
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x000DF3B4 File Offset: 0x000DD5B4
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

		// Token: 0x06002425 RID: 9253 RVA: 0x000DF3FC File Offset: 0x000DD5FC
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			switch (frame.State)
			{
			case 0:
			{
				Query valueQuery = processor.GetValueQuery(this.selectKey);
				object obj = valueQuery.Evaluate(frame.NodeSet);
				if (obj is XPathNodeIterator)
				{
					processor.PushActionFrame(CopyNodeSetAction.GetAction(), new XPathArrayIterator(valueQuery));
					frame.State = 3;
					return;
				}
				XPathNavigator xpathNavigator = obj as XPathNavigator;
				if (xpathNavigator != null)
				{
					processor.PushActionFrame(CopyNodeSetAction.GetAction(), new XPathSingletonIterator(xpathNavigator));
					frame.State = 3;
					return;
				}
				string text = XmlConvert.ToXPathString(obj);
				if (processor.TextEvent(text))
				{
					frame.Finished();
					return;
				}
				frame.StoredOutput = text;
				frame.State = 2;
				return;
			}
			case 1:
				break;
			case 2:
				processor.TextEvent(frame.StoredOutput);
				frame.Finished();
				return;
			case 3:
				frame.Finished();
				break;
			default:
				return;
			}
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x000DF4C7 File Offset: 0x000DD6C7
		public CopyOfAction()
		{
		}

		// Token: 0x04001CDD RID: 7389
		private const int ResultStored = 2;

		// Token: 0x04001CDE RID: 7390
		private const int NodeSetCopied = 3;

		// Token: 0x04001CDF RID: 7391
		private int selectKey = -1;
	}
}
