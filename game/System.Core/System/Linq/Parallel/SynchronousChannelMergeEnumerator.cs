using System;

namespace System.Linq.Parallel
{
	// Token: 0x0200010D RID: 269
	internal sealed class SynchronousChannelMergeEnumerator<T> : MergeEnumerator<T>
	{
		// Token: 0x060008C8 RID: 2248 RVA: 0x0001E3BB File Offset: 0x0001C5BB
		internal SynchronousChannelMergeEnumerator(QueryTaskGroupState taskGroupState, SynchronousChannel<T>[] channels) : base(taskGroupState)
		{
			this._channels = channels;
			this._channelIndex = -1;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x0001E3D2 File Offset: 0x0001C5D2
		public override T Current
		{
			get
			{
				if (this._channelIndex == -1 || this._channelIndex == this._channels.Length)
				{
					throw new InvalidOperationException("Enumeration has not started. MoveNext must be called to initiate enumeration.");
				}
				return this._currentElement;
			}
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0001E400 File Offset: 0x0001C600
		public override bool MoveNext()
		{
			if (this._channelIndex == -1)
			{
				this._channelIndex = 0;
			}
			while (this._channelIndex != this._channels.Length)
			{
				SynchronousChannel<T> synchronousChannel = this._channels[this._channelIndex];
				if (synchronousChannel.Count != 0)
				{
					this._currentElement = synchronousChannel.Dequeue();
					return true;
				}
				this._channelIndex++;
			}
			return false;
		}

		// Token: 0x04000622 RID: 1570
		private SynchronousChannel<T>[] _channels;

		// Token: 0x04000623 RID: 1571
		private int _channelIndex;

		// Token: 0x04000624 RID: 1572
		private T _currentElement;
	}
}
