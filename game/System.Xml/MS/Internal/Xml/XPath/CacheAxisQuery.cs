using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200061D RID: 1565
	internal abstract class CacheAxisQuery : BaseAxisQuery
	{
		// Token: 0x06004021 RID: 16417 RVA: 0x00163C9F File Offset: 0x00161E9F
		public CacheAxisQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest) : base(qyInput, name, prefix, typeTest)
		{
			this.outputBuffer = new List<XPathNavigator>();
			this.count = 0;
		}

		// Token: 0x06004022 RID: 16418 RVA: 0x00163CBE File Offset: 0x00161EBE
		protected CacheAxisQuery(CacheAxisQuery other) : base(other)
		{
			this.outputBuffer = new List<XPathNavigator>(other.outputBuffer);
			this.count = other.count;
		}

		// Token: 0x06004023 RID: 16419 RVA: 0x00163CE4 File Offset: 0x00161EE4
		public override void Reset()
		{
			this.count = 0;
		}

		// Token: 0x06004024 RID: 16420 RVA: 0x00163CED File Offset: 0x00161EED
		public override object Evaluate(XPathNodeIterator context)
		{
			base.Evaluate(context);
			this.outputBuffer.Clear();
			return this;
		}

		// Token: 0x06004025 RID: 16421 RVA: 0x00163D04 File Offset: 0x00161F04
		public override XPathNavigator Advance()
		{
			if (this.count < this.outputBuffer.Count)
			{
				List<XPathNavigator> list = this.outputBuffer;
				int count = this.count;
				this.count = count + 1;
				return list[count];
			}
			return null;
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06004026 RID: 16422 RVA: 0x00163D42 File Offset: 0x00161F42
		public override XPathNavigator Current
		{
			get
			{
				if (this.count == 0)
				{
					return null;
				}
				return this.outputBuffer[this.count - 1];
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06004027 RID: 16423 RVA: 0x00163D61 File Offset: 0x00161F61
		public override int CurrentPosition
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06004028 RID: 16424 RVA: 0x00163D69 File Offset: 0x00161F69
		public override int Count
		{
			get
			{
				return this.outputBuffer.Count;
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x06004029 RID: 16425 RVA: 0x0012B969 File Offset: 0x00129B69
		public override QueryProps Properties
		{
			get
			{
				return (QueryProps)23;
			}
		}

		// Token: 0x04002DFB RID: 11771
		protected List<XPathNavigator> outputBuffer;
	}
}
