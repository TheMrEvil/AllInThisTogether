using System;
using System.Collections.Generic;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000008 RID: 8
	internal class EnetChannel
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00003314 File Offset: 0x00001514
		public EnetChannel(byte channelNumber, int commandBufferSize)
		{
			this.ChannelNumber = channelNumber;
			this.incomingReliableCommandsList = new NonAllocDictionary<int, NCommand>((uint)commandBufferSize);
			this.incomingUnreliableCommandsList = new NonAllocDictionary<int, NCommand>((uint)commandBufferSize);
			this.incomingUnsequencedCommandsList = new Queue<NCommand>();
			this.incomingUnsequencedFragments = new NonAllocDictionary<int, NCommand>(29U);
			this.outgoingReliableCommandsList = new Queue<NCommand>(commandBufferSize);
			this.outgoingUnreliableCommandsList = new Queue<NCommand>(commandBufferSize);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003384 File Offset: 0x00001584
		public bool ContainsUnreliableSequenceNumber(int unreliableSequenceNumber)
		{
			return this.incomingUnreliableCommandsList.ContainsKey(unreliableSequenceNumber);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000033A4 File Offset: 0x000015A4
		public NCommand FetchUnreliableSequenceNumber(int unreliableSequenceNumber)
		{
			return this.incomingUnreliableCommandsList[unreliableSequenceNumber];
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000033C4 File Offset: 0x000015C4
		public bool ContainsReliableSequenceNumber(int reliableSequenceNumber)
		{
			return this.incomingReliableCommandsList.ContainsKey(reliableSequenceNumber);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000033E4 File Offset: 0x000015E4
		public NCommand FetchReliableSequenceNumber(int reliableSequenceNumber)
		{
			return this.incomingReliableCommandsList[reliableSequenceNumber];
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003404 File Offset: 0x00001604
		public bool TryGetFragment(int reliableSequenceNumber, bool isSequenced, out NCommand fragment)
		{
			bool result;
			if (isSequenced)
			{
				result = this.incomingReliableCommandsList.TryGetValue(reliableSequenceNumber, out fragment);
			}
			else
			{
				result = this.incomingUnsequencedFragments.TryGetValue(reliableSequenceNumber, out fragment);
			}
			return result;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000343C File Offset: 0x0000163C
		public void RemoveFragment(int reliableSequenceNumber, bool isSequenced)
		{
			if (isSequenced)
			{
				this.incomingReliableCommandsList.Remove(reliableSequenceNumber);
			}
			else
			{
				this.incomingUnsequencedFragments.Remove(reliableSequenceNumber);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003470 File Offset: 0x00001670
		public void clearAll()
		{
			lock (this)
			{
				this.incomingReliableCommandsList.Clear();
				this.incomingUnreliableCommandsList.Clear();
				this.incomingUnsequencedCommandsList.Clear();
				this.incomingUnsequencedFragments.Clear();
				this.outgoingReliableCommandsList.Clear();
				this.outgoingUnreliableCommandsList.Clear();
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000034F4 File Offset: 0x000016F4
		public bool QueueIncomingReliableUnsequenced(NCommand command)
		{
			bool flag = command.reliableSequenceNumber <= this.reliableUnsequencedNumbersCompletelyReceived;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.reliableUnsequencedNumbersReceived.Contains(command.reliableSequenceNumber);
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = command.reliableSequenceNumber == this.reliableUnsequencedNumbersCompletelyReceived + 1;
					if (flag3)
					{
						this.reliableUnsequencedNumbersCompletelyReceived++;
					}
					else
					{
						this.reliableUnsequencedNumbersReceived.Add(command.reliableSequenceNumber);
					}
					while (this.reliableUnsequencedNumbersReceived.Contains(this.reliableUnsequencedNumbersCompletelyReceived + 1))
					{
						this.reliableUnsequencedNumbersCompletelyReceived++;
						this.reliableUnsequencedNumbersReceived.Remove(this.reliableUnsequencedNumbersCompletelyReceived);
					}
					bool flag4 = command.commandType == 15;
					if (flag4)
					{
						this.incomingUnsequencedFragments.Add(command.reliableSequenceNumber, command);
					}
					else
					{
						this.incomingUnsequencedCommandsList.Enqueue(command);
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x04000018 RID: 24
		internal byte ChannelNumber;

		// Token: 0x04000019 RID: 25
		internal NonAllocDictionary<int, NCommand> incomingReliableCommandsList;

		// Token: 0x0400001A RID: 26
		internal NonAllocDictionary<int, NCommand> incomingUnreliableCommandsList;

		// Token: 0x0400001B RID: 27
		internal Queue<NCommand> incomingUnsequencedCommandsList;

		// Token: 0x0400001C RID: 28
		internal NonAllocDictionary<int, NCommand> incomingUnsequencedFragments;

		// Token: 0x0400001D RID: 29
		internal Queue<NCommand> outgoingReliableCommandsList;

		// Token: 0x0400001E RID: 30
		internal Queue<NCommand> outgoingUnreliableCommandsList;

		// Token: 0x0400001F RID: 31
		internal int incomingReliableSequenceNumber;

		// Token: 0x04000020 RID: 32
		internal int incomingUnreliableSequenceNumber;

		// Token: 0x04000021 RID: 33
		internal int outgoingReliableSequenceNumber;

		// Token: 0x04000022 RID: 34
		internal int outgoingUnreliableSequenceNumber;

		// Token: 0x04000023 RID: 35
		internal int outgoingReliableUnsequencedNumber;

		// Token: 0x04000024 RID: 36
		private int reliableUnsequencedNumbersCompletelyReceived;

		// Token: 0x04000025 RID: 37
		private HashSet<int> reliableUnsequencedNumbersReceived = new HashSet<int>();

		// Token: 0x04000026 RID: 38
		internal int highestReceivedAck;

		// Token: 0x04000027 RID: 39
		internal int reliableCommandsInFlight;

		// Token: 0x04000028 RID: 40
		internal int lowestUnacknowledgedSequenceNumber;
	}
}
