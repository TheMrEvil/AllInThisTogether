using System;
using System.Collections.Generic;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200061F RID: 1567
	internal abstract class CacheOutputQuery : Query
	{
		// Token: 0x06004031 RID: 16433 RVA: 0x00163FB0 File Offset: 0x001621B0
		public CacheOutputQuery(Query input)
		{
			this.input = input;
			this.outputBuffer = new List<XPathNavigator>();
			this.count = 0;
		}

		// Token: 0x06004032 RID: 16434 RVA: 0x00163FD1 File Offset: 0x001621D1
		protected CacheOutputQuery(CacheOutputQuery other) : base(other)
		{
			this.input = Query.Clone(other.input);
			this.outputBuffer = new List<XPathNavigator>(other.outputBuffer);
			this.count = other.count;
		}

		// Token: 0x06004033 RID: 16435 RVA: 0x00163CE4 File Offset: 0x00161EE4
		public override void Reset()
		{
			this.count = 0;
		}

		// Token: 0x06004034 RID: 16436 RVA: 0x00164008 File Offset: 0x00162208
		public override void SetXsltContext(XsltContext context)
		{
			this.input.SetXsltContext(context);
		}

		// Token: 0x06004035 RID: 16437 RVA: 0x00164016 File Offset: 0x00162216
		public override object Evaluate(XPathNodeIterator context)
		{
			this.outputBuffer.Clear();
			this.count = 0;
			return this.input.Evaluate(context);
		}

		// Token: 0x06004036 RID: 16438 RVA: 0x00164038 File Offset: 0x00162238
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

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x06004037 RID: 16439 RVA: 0x00164076 File Offset: 0x00162276
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

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06004038 RID: 16440 RVA: 0x000708A9 File Offset: 0x0006EAA9
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06004039 RID: 16441 RVA: 0x00163D61 File Offset: 0x00161F61
		public override int CurrentPosition
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x0600403A RID: 16442 RVA: 0x00164095 File Offset: 0x00162295
		public override int Count
		{
			get
			{
				return this.outputBuffer.Count;
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x0600403B RID: 16443 RVA: 0x0012B969 File Offset: 0x00129B69
		public override QueryProps Properties
		{
			get
			{
				return (QueryProps)23;
			}
		}

		// Token: 0x04002E00 RID: 11776
		internal Query input;

		// Token: 0x04002E01 RID: 11777
		protected List<XPathNavigator> outputBuffer;
	}
}
