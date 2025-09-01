using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200043B RID: 1083
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct DodSequenceMerge
	{
		// Token: 0x06002AE7 RID: 10983 RVA: 0x00102CBE File Offset: 0x00100EBE
		public void Create(XmlQueryRuntime runtime)
		{
			this.firstSequence = null;
			this.sequencesToMerge = null;
			this.nodeCount = 0;
			this.runtime = runtime;
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x00102CDC File Offset: 0x00100EDC
		public void AddSequence(IList<XPathNavigator> sequence)
		{
			if (sequence.Count == 0)
			{
				return;
			}
			if (this.firstSequence == null)
			{
				this.firstSequence = sequence;
				return;
			}
			if (this.sequencesToMerge == null)
			{
				this.sequencesToMerge = new List<IEnumerator<XPathNavigator>>();
				this.MoveAndInsertSequence(this.firstSequence.GetEnumerator());
				this.nodeCount = this.firstSequence.Count;
			}
			this.MoveAndInsertSequence(sequence.GetEnumerator());
			this.nodeCount += sequence.Count;
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x00102D58 File Offset: 0x00100F58
		public IList<XPathNavigator> MergeSequences()
		{
			if (this.firstSequence == null)
			{
				return XmlQueryNodeSequence.Empty;
			}
			if (this.sequencesToMerge == null || this.sequencesToMerge.Count <= 1)
			{
				return this.firstSequence;
			}
			XmlQueryNodeSequence xmlQueryNodeSequence = new XmlQueryNodeSequence(this.nodeCount);
			while (this.sequencesToMerge.Count != 1)
			{
				IEnumerator<XPathNavigator> enumerator = this.sequencesToMerge[this.sequencesToMerge.Count - 1];
				this.sequencesToMerge.RemoveAt(this.sequencesToMerge.Count - 1);
				xmlQueryNodeSequence.Add(enumerator.Current);
				this.MoveAndInsertSequence(enumerator);
			}
			do
			{
				xmlQueryNodeSequence.Add(this.sequencesToMerge[0].Current);
			}
			while (this.sequencesToMerge[0].MoveNext());
			return xmlQueryNodeSequence;
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x00102E1B File Offset: 0x0010101B
		private void MoveAndInsertSequence(IEnumerator<XPathNavigator> sequence)
		{
			if (sequence.MoveNext())
			{
				this.InsertSequence(sequence);
			}
		}

		// Token: 0x06002AEB RID: 10987 RVA: 0x00102E2C File Offset: 0x0010102C
		private void InsertSequence(IEnumerator<XPathNavigator> sequence)
		{
			for (int i = this.sequencesToMerge.Count - 1; i >= 0; i--)
			{
				int num = this.runtime.ComparePosition(sequence.Current, this.sequencesToMerge[i].Current);
				if (num == -1)
				{
					this.sequencesToMerge.Insert(i + 1, sequence);
					return;
				}
				if (num == 0 && !sequence.MoveNext())
				{
					return;
				}
			}
			this.sequencesToMerge.Insert(0, sequence);
		}

		// Token: 0x040021D1 RID: 8657
		private IList<XPathNavigator> firstSequence;

		// Token: 0x040021D2 RID: 8658
		private List<IEnumerator<XPathNavigator>> sequencesToMerge;

		// Token: 0x040021D3 RID: 8659
		private int nodeCount;

		// Token: 0x040021D4 RID: 8660
		private XmlQueryRuntime runtime;
	}
}
