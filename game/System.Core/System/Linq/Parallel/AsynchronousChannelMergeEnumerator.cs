using System;

namespace System.Linq.Parallel
{
	// Token: 0x02000102 RID: 258
	internal sealed class AsynchronousChannelMergeEnumerator<T> : MergeEnumerator<T>
	{
		// Token: 0x0600089A RID: 2202 RVA: 0x0001D79C File Offset: 0x0001B99C
		internal AsynchronousChannelMergeEnumerator(QueryTaskGroupState taskGroupState, AsynchronousChannel<T>[] channels, IntValueEvent consumerEvent) : base(taskGroupState)
		{
			this._channels = channels;
			this._channelIndex = -1;
			this._done = new bool[this._channels.Length];
			this._consumerEvent = consumerEvent;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x0001D7CD File Offset: 0x0001B9CD
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

		// Token: 0x0600089C RID: 2204 RVA: 0x0001D7FC File Offset: 0x0001B9FC
		public override bool MoveNext()
		{
			int num = this._channelIndex;
			if (num == -1)
			{
				num = (this._channelIndex = 0);
			}
			if (num == this._channels.Length)
			{
				return false;
			}
			if (!this._done[num] && this._channels[num].TryDequeue(ref this._currentElement))
			{
				this._channelIndex = (num + 1) % this._channels.Length;
				return true;
			}
			return this.MoveNextSlowPath();
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0001D864 File Offset: 0x0001BA64
		private bool MoveNextSlowPath()
		{
			int num = 0;
			int num2 = this._channelIndex;
			int num3;
			while ((num3 = this._channelIndex) != this._channels.Length)
			{
				AsynchronousChannel<T> asynchronousChannel = this._channels[num3];
				bool flag = this._done[num3];
				if (!flag && asynchronousChannel.TryDequeue(ref this._currentElement))
				{
					this._channelIndex = (num3 + 1) % this._channels.Length;
					return true;
				}
				if (!flag && asynchronousChannel.IsDone)
				{
					if (!asynchronousChannel.IsChunkBufferEmpty)
					{
						asynchronousChannel.TryDequeue(ref this._currentElement);
						return true;
					}
					this._done[num3] = true;
					flag = true;
					asynchronousChannel.Dispose();
				}
				if (flag && ++num == this._channels.Length)
				{
					this._channelIndex = this._channels.Length;
					break;
				}
				num3 = (this._channelIndex = (num3 + 1) % this._channels.Length);
				if (num3 == num2)
				{
					try
					{
						num = 0;
						for (int i = 0; i < this._channels.Length; i++)
						{
							bool flag2 = false;
							if (!this._done[i] && this._channels[i].TryDequeue(ref this._currentElement, ref flag2))
							{
								return true;
							}
							if (flag2)
							{
								if (!this._done[i])
								{
									this._done[i] = true;
								}
								if (++num == this._channels.Length)
								{
									num3 = (this._channelIndex = this._channels.Length);
									break;
								}
							}
						}
						if (num3 == this._channels.Length)
						{
							break;
						}
						this._consumerEvent.Wait();
						num3 = (this._channelIndex = this._consumerEvent.Value);
						this._consumerEvent.Reset();
						num2 = num3;
						num = 0;
					}
					finally
					{
						for (int j = 0; j < this._channels.Length; j++)
						{
							if (!this._done[j])
							{
								this._channels[j].DoneWithDequeueWait();
							}
						}
					}
					continue;
				}
			}
			this._taskGroupState.QueryEnd(false);
			return false;
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0001DA54 File Offset: 0x0001BC54
		public override void Dispose()
		{
			if (this._consumerEvent != null)
			{
				base.Dispose();
				this._consumerEvent.Dispose();
				this._consumerEvent = null;
			}
		}

		// Token: 0x040005FB RID: 1531
		private AsynchronousChannel<T>[] _channels;

		// Token: 0x040005FC RID: 1532
		private IntValueEvent _consumerEvent;

		// Token: 0x040005FD RID: 1533
		private bool[] _done;

		// Token: 0x040005FE RID: 1534
		private int _channelIndex;

		// Token: 0x040005FF RID: 1535
		private T _currentElement;
	}
}
