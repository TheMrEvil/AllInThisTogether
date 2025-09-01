using System;
using System.Text;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003AC RID: 940
	internal class StringOutput : SequentialOutput
	{
		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06002673 RID: 9843 RVA: 0x000E72DF File Offset: 0x000E54DF
		internal string Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x000E72E7 File Offset: 0x000E54E7
		internal StringOutput(Processor processor) : base(processor)
		{
			this.builder = new StringBuilder();
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x000E72FB File Offset: 0x000E54FB
		internal override void Write(char outputChar)
		{
			this.builder.Append(outputChar);
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x000E730A File Offset: 0x000E550A
		internal override void Write(string outputText)
		{
			this.builder.Append(outputText);
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x000E7319 File Offset: 0x000E5519
		internal override void Close()
		{
			this.result = this.builder.ToString();
		}

		// Token: 0x04001E4B RID: 7755
		private StringBuilder builder;

		// Token: 0x04001E4C RID: 7756
		private string result;
	}
}
