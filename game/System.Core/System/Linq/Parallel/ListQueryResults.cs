using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x0200017D RID: 381
	internal class ListQueryResults<T> : QueryResults<T>
	{
		// Token: 0x06000A34 RID: 2612 RVA: 0x000247F7 File Offset: 0x000229F7
		internal ListQueryResults(IList<T> source, int partitionCount, bool useStriping)
		{
			this._source = source;
			this._partitionCount = partitionCount;
			this._useStriping = useStriping;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00024814 File Offset: 0x00022A14
		internal override void GivePartitionedStream(IPartitionedStreamRecipient<T> recipient)
		{
			PartitionedStream<T, int> partitionedStream = this.GetPartitionedStream();
			recipient.Receive<int>(partitionedStream);
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x00007E1D File Offset: 0x0000601D
		internal override bool IsIndexible
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x0002482F File Offset: 0x00022A2F
		internal override int ElementsCount
		{
			get
			{
				return this._source.Count;
			}
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0002483C File Offset: 0x00022A3C
		internal override T GetElement(int index)
		{
			return this._source[index];
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0002484A File Offset: 0x00022A4A
		internal PartitionedStream<T, int> GetPartitionedStream()
		{
			return ExchangeUtilities.PartitionDataSource<T>(this._source, this._partitionCount, this._useStriping);
		}

		// Token: 0x04000726 RID: 1830
		private IList<T> _source;

		// Token: 0x04000727 RID: 1831
		private int _partitionCount;

		// Token: 0x04000728 RID: 1832
		private bool _useStriping;
	}
}
