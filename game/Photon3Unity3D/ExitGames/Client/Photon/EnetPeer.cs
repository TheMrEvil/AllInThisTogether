using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000009 RID: 9
	internal class EnetPeer : PeerBase
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000035F0 File Offset: 0x000017F0
		internal override int QueuedIncomingCommandsCount
		{
			get
			{
				int num = 0;
				EnetChannel[] obj = this.channelArray;
				lock (obj)
				{
					for (int i = 0; i < this.channelArray.Length; i++)
					{
						EnetChannel enetChannel = this.channelArray[i];
						num += enetChannel.incomingReliableCommandsList.Count;
						num += enetChannel.incomingUnreliableCommandsList.Count;
					}
				}
				return num;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004F RID: 79 RVA: 0x0000367C File Offset: 0x0000187C
		internal override int QueuedOutgoingCommandsCount
		{
			get
			{
				int num = 0;
				EnetChannel[] obj = this.channelArray;
				lock (obj)
				{
					for (int i = 0; i < this.channelArray.Length; i++)
					{
						EnetChannel enetChannel = this.channelArray[i];
						EnetChannel obj2 = enetChannel;
						lock (obj2)
						{
							num += enetChannel.outgoingReliableCommandsList.Count;
							num += enetChannel.outgoingUnreliableCommandsList.Count;
						}
					}
				}
				return num;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003738 File Offset: 0x00001938
		internal override int SentReliableCommandsCount
		{
			get
			{
				return this.sentReliableCommands.Count;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003758 File Offset: 0x00001958
		// (set) Token: 0x06000052 RID: 82 RVA: 0x0000377C File Offset: 0x0000197C
		private bool sendWindowUpdateRequired
		{
			get
			{
				return Interlocked.CompareExchange(ref this.sendWindowUpdateRequiredBackValue, 1, 1) == 1;
			}
			set
			{
				if (value)
				{
					Interlocked.CompareExchange(ref this.sendWindowUpdateRequiredBackValue, 1, 0);
				}
				else
				{
					Interlocked.CompareExchange(ref this.sendWindowUpdateRequiredBackValue, 0, 1);
				}
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000037B0 File Offset: 0x000019B0
		internal EnetPeer()
		{
			this.TrafficPackageHeaderSize = 12;
			this.messageHeader = EnetPeer.udpHeader0xF3;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000384C File Offset: 0x00001A4C
		internal override bool IsTransportEncrypted()
		{
			return this.datagramEncryptedConnection;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003864 File Offset: 0x00001A64
		internal override void Reset()
		{
			base.Reset();
			bool flag = this.photonPeer.PayloadEncryptionSecret != null && this.usedTransportProtocol == ConnectionProtocol.Udp;
			if (flag)
			{
				this.InitEncryption(this.photonPeer.PayloadEncryptionSecret);
			}
			bool flag2 = this.photonPeer.Encryptor != null;
			if (flag2)
			{
				this.isEncryptionAvailable = true;
			}
			this.peerID = (this.photonPeer.EnableServerTracing ? -2 : -1);
			this.challenge = SupportClass.ThreadSafeRandom.Next();
			bool flag3 = this.udpBuffer == null || this.udpBuffer.Length != base.mtu;
			if (flag3)
			{
				this.udpBuffer = new byte[base.mtu];
			}
			this.reliableCommandsSent = 0;
			this.reliableCommandsRepeated = 0;
			this.timeoutInt = 0;
			this.outgoingUnsequencedGroupNumber = 0;
			this.incomingUnsequencedGroupNumber = 0;
			for (int i = 0; i < this.unsequencedWindow.Length; i++)
			{
				this.unsequencedWindow[i] = 0;
			}
			EnetChannel[] obj = this.channelArray;
			lock (obj)
			{
				EnetChannel[] array = this.channelArray;
				bool flag5 = array.Length != (int)(base.ChannelCount + 1);
				if (flag5)
				{
					array = new EnetChannel[(int)(base.ChannelCount + 1)];
				}
				for (byte b = 0; b < base.ChannelCount; b += 1)
				{
					array[(int)b] = new EnetChannel(b, this.commandBufferSize);
				}
				array[(int)base.ChannelCount] = new EnetChannel(byte.MaxValue, this.commandBufferSize);
				this.channelArray = array;
			}
			List<NCommand> obj2 = this.sentReliableCommands;
			lock (obj2)
			{
				this.sentReliableCommands.Clear();
			}
			this.outgoingAcknowledgementsPool = new StreamBuffer(0);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003A68 File Offset: 0x00001C68
		internal void ApplyRandomizedSequenceNumbers()
		{
			bool flag = !this.photonPeer.RandomizeSequenceNumbers;
			if (!flag)
			{
				EnetChannel[] obj = this.channelArray;
				lock (obj)
				{
					for (int i = 0; i < this.channelArray.Length; i++)
					{
						EnetChannel enetChannel = this.channelArray[i];
						int num = (int)this.photonPeer.RandomizedSequenceNumbers[i % this.photonPeer.RandomizedSequenceNumbers.Length];
						bool gcmDatagramEncryption = this.photonPeer.GcmDatagramEncryption;
						if (gcmDatagramEncryption)
						{
							enetChannel.incomingReliableSequenceNumber += num;
							enetChannel.outgoingReliableSequenceNumber += num;
							enetChannel.highestReceivedAck += num;
							enetChannel.outgoingReliableUnsequencedNumber += num;
						}
						else
						{
							enetChannel.incomingReliableSequenceNumber = num;
							enetChannel.outgoingReliableSequenceNumber = num;
							enetChannel.highestReceivedAck = num;
							enetChannel.outgoingReliableUnsequencedNumber = num;
						}
					}
				}
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003B84 File Offset: 0x00001D84
		private EnetChannel GetChannel(byte channelNumber)
		{
			return (channelNumber == byte.MaxValue) ? this.channelArray[this.channelArray.Length - 1] : this.channelArray[(int)channelNumber];
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003BBC File Offset: 0x00001DBC
		internal override bool Connect(string ipport, string proxyServerAddress, string appID, object photonToken)
		{
			bool flag = this.PhotonSocket.Connect();
			bool result;
			if (flag)
			{
				bool trafficStatsEnabled = base.TrafficStatsEnabled;
				if (trafficStatsEnabled)
				{
					base.TrafficStatsOutgoing.ControlCommandBytes += 44;
					base.TrafficStatsOutgoing.ControlCommandCount++;
				}
				this.peerConnectionState = ConnectionStateValue.Connecting;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003C1F File Offset: 0x00001E1F
		public override void OnConnect()
		{
			this.QueueOutgoingReliableCommand(this.nCommandPool.Acquire(this, 2, null, byte.MaxValue));
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003C3C File Offset: 0x00001E3C
		internal override void Disconnect()
		{
			bool flag = this.peerConnectionState == ConnectionStateValue.Disconnected || this.peerConnectionState == ConnectionStateValue.Disconnecting;
			if (!flag)
			{
				bool flag2 = this.sentReliableCommands != null;
				if (flag2)
				{
					List<NCommand> obj = this.sentReliableCommands;
					lock (obj)
					{
						this.sentReliableCommands.Clear();
					}
				}
				EnetChannel[] obj2 = this.channelArray;
				lock (obj2)
				{
					foreach (EnetChannel enetChannel in this.channelArray)
					{
						enetChannel.clearAll();
					}
				}
				bool isSimulationEnabled = base.NetworkSimulationSettings.IsSimulationEnabled;
				base.NetworkSimulationSettings.IsSimulationEnabled = false;
				NCommand ncommand = this.nCommandPool.Acquire(this, 4, null, byte.MaxValue);
				this.peerConnectionState = ConnectionStateValue.Disconnecting;
				this.QueueOutgoingReliableCommand(ncommand);
				this.SendOutgoingCommands();
				bool trafficStatsEnabled = base.TrafficStatsEnabled;
				if (trafficStatsEnabled)
				{
					base.TrafficStatsOutgoing.CountControlCommand(ncommand.Size);
				}
				base.NetworkSimulationSettings.IsSimulationEnabled = isSimulationEnabled;
				this.PhotonSocket.Disconnect();
				this.peerConnectionState = ConnectionStateValue.Disconnected;
				base.EnqueueStatusCallback(StatusCode.Disconnect);
				byte[] obj3 = this.udpBuffer;
				lock (obj3)
				{
					this.datagramEncryptedConnection = false;
				}
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003DE0 File Offset: 0x00001FE0
		internal override void StopConnection()
		{
			bool flag = this.PhotonSocket != null;
			if (flag)
			{
				this.PhotonSocket.Disconnect();
			}
			this.peerConnectionState = ConnectionStateValue.Disconnected;
			bool flag2 = base.Listener != null;
			if (flag2)
			{
				base.Listener.OnStatusChanged(StatusCode.Disconnect);
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003E30 File Offset: 0x00002030
		internal override void FetchServerTimestamp()
		{
			bool flag = this.peerConnectionState != ConnectionStateValue.Connected || !this.ApplicationIsInitialized;
			if (flag)
			{
				bool flag2 = base.debugOut >= DebugLevel.INFO;
				if (flag2)
				{
					base.EnqueueDebugReturn(DebugLevel.INFO, "FetchServerTimestamp() was skipped, as the client is not connected. Current ConnectionState: " + this.peerConnectionState.ToString());
				}
			}
			else
			{
				this.CreateAndEnqueueCommand(12, null, byte.MaxValue);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003EA0 File Offset: 0x000020A0
		internal override bool DispatchIncomingCommands()
		{
			int count = this.CommandQueue.Count;
			bool flag = count > 0;
			if (flag)
			{
				for (int i = 0; i < count; i++)
				{
					Queue<NCommand> commandQueue = this.CommandQueue;
					lock (commandQueue)
					{
						NCommand command = this.CommandQueue.Dequeue();
						this.ExecuteCommand(command);
					}
				}
			}
			for (;;)
			{
				Queue<PeerBase.MyAction> actionQueue = this.ActionQueue;
				PeerBase.MyAction myAction;
				lock (actionQueue)
				{
					bool flag4 = this.ActionQueue.Count <= 0;
					if (flag4)
					{
						break;
					}
					myAction = this.ActionQueue.Dequeue();
				}
				myAction();
			}
			NCommand ncommand = null;
			EnetChannel[] obj = this.channelArray;
			lock (obj)
			{
				for (int j = 0; j < this.channelArray.Length; j++)
				{
					EnetChannel enetChannel = this.channelArray[j];
					bool flag6 = enetChannel.incomingUnsequencedCommandsList.Count > 0;
					if (flag6)
					{
						ncommand = enetChannel.incomingUnsequencedCommandsList.Dequeue();
						break;
					}
					bool flag7 = enetChannel.incomingUnreliableCommandsList.Count > 0;
					if (flag7)
					{
						int num = int.MaxValue;
						foreach (int num2 in enetChannel.incomingUnreliableCommandsList.Keys)
						{
							NCommand ncommand2 = enetChannel.incomingUnreliableCommandsList[num2];
							bool flag8 = num2 < enetChannel.incomingUnreliableSequenceNumber || ncommand2.reliableSequenceNumber < enetChannel.incomingReliableSequenceNumber;
							if (flag8)
							{
								PhotonPeer photonPeer = this.photonPeer;
								int countDiscarded = photonPeer.CountDiscarded;
								photonPeer.CountDiscarded = countDiscarded + 1;
								this.commandsToRemove.Enqueue(num2);
							}
							else
							{
								bool flag9 = num2 < num;
								if (flag9)
								{
									bool flag10 = ncommand2.reliableSequenceNumber > enetChannel.incomingReliableSequenceNumber;
									if (!flag10)
									{
										num = num2;
									}
								}
							}
						}
						NonAllocDictionary<int, NCommand> incomingUnreliableCommandsList = enetChannel.incomingUnreliableCommandsList;
						while (this.commandsToRemove.Count > 0)
						{
							int key = this.commandsToRemove.Dequeue();
							NCommand ncommand3 = incomingUnreliableCommandsList[key];
							incomingUnreliableCommandsList.Remove(key);
							ncommand3.FreePayload();
							ncommand3.Release();
						}
						bool flag11 = num < int.MaxValue;
						if (flag11)
						{
							this.photonPeer.DeltaUnreliableNumber = num - enetChannel.incomingUnreliableSequenceNumber;
							ncommand = enetChannel.incomingUnreliableCommandsList[num];
						}
						bool flag12 = ncommand != null;
						if (flag12)
						{
							enetChannel.incomingUnreliableCommandsList.Remove(ncommand.unreliableSequenceNumber);
							enetChannel.incomingUnreliableSequenceNumber = ncommand.unreliableSequenceNumber;
							break;
						}
					}
					bool flag13 = ncommand == null && enetChannel.incomingReliableCommandsList.Count > 0;
					if (flag13)
					{
						enetChannel.incomingReliableCommandsList.TryGetValue(enetChannel.incomingReliableSequenceNumber + 1, out ncommand);
						bool flag14 = ncommand == null;
						if (!flag14)
						{
							bool flag15 = ncommand.commandType != 8;
							if (flag15)
							{
								enetChannel.incomingReliableSequenceNumber = ncommand.reliableSequenceNumber;
								enetChannel.incomingReliableCommandsList.Remove(ncommand.reliableSequenceNumber);
								break;
							}
							bool flag16 = ncommand.fragmentsRemaining > 0;
							if (flag16)
							{
								ncommand = null;
							}
							else
							{
								enetChannel.incomingReliableSequenceNumber = ncommand.reliableSequenceNumber + ncommand.fragmentCount - 1;
								enetChannel.incomingReliableCommandsList.Remove(ncommand.reliableSequenceNumber);
							}
							break;
						}
					}
				}
			}
			bool flag17 = ncommand != null && ncommand.Payload != null;
			bool result;
			if (flag17)
			{
				this.ByteCountCurrentDispatch = ncommand.Size;
				this.CommandInCurrentDispatch = ncommand;
				bool flag18 = this.DeserializeMessageAndCallback(ncommand.Payload);
				this.CommandInCurrentDispatch = null;
				ncommand.FreePayload();
				ncommand.Release();
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004314 File Offset: 0x00002514
		private int GetFragmentLength()
		{
			bool flag = this.fragmentLength == 0 || base.mtu != this.fragmentLengthMtuValue;
			if (flag)
			{
				this.fragmentLengthMtuValue = base.mtu;
				this.fragmentLength = base.mtu - 12 - 36;
				this.fragmentLengthDatagramEncrypt = ((this.photonPeer.Encryptor == null) ? 0 : this.photonPeer.Encryptor.CalculateFragmentLength());
			}
			return this.datagramEncryptedConnection ? this.fragmentLengthDatagramEncrypt : this.fragmentLength;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000043A4 File Offset: 0x000025A4
		private int CalculatePacketSize(int inSize)
		{
			bool flag = this.datagramEncryptedConnection;
			int result;
			if (flag)
			{
				result = this.photonPeer.Encryptor.CalculateEncryptedSize(inSize + 7);
			}
			else
			{
				result = inSize;
			}
			return result;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000043D8 File Offset: 0x000025D8
		private int CalculateInitialOffset()
		{
			bool flag = this.datagramEncryptedConnection;
			int result;
			if (flag)
			{
				result = 5;
			}
			else
			{
				int num = 12;
				bool crcEnabled = this.photonPeer.CrcEnabled;
				if (crcEnabled)
				{
					num += 4;
				}
				result = num;
			}
			return result;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004414 File Offset: 0x00002614
		internal override bool SendAcksOnly()
		{
			bool flag = this.peerConnectionState == ConnectionStateValue.Disconnected;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.PhotonSocket == null || !this.PhotonSocket.Connected;
				if (flag2)
				{
					result = false;
				}
				else
				{
					byte[] obj = this.udpBuffer;
					lock (obj)
					{
						int num = 0;
						this.udpBufferIndex = this.CalculateInitialOffset();
						this.udpCommandCount = 0;
						StreamBuffer obj2 = this.outgoingAcknowledgementsPool;
						lock (obj2)
						{
							num = this.SerializeAckToBuffer();
							this.timeLastSendAck = base.timeInt;
						}
						bool flag5 = base.timeInt > this.timeoutInt && this.sentReliableCommands.Count > 0;
						if (flag5)
						{
							int num2 = base.timeInt + 100;
							List<NCommand> obj3 = this.sentReliableCommands;
							lock (obj3)
							{
								int num3 = 0;
								for (int i = 0; i < this.sentReliableCommands.Count; i++)
								{
									NCommand ncommand = this.sentReliableCommands[i];
									int num4 = ncommand.commandSentTime + ncommand.roundTripTimeout;
									bool flag7 = base.timeInt > num4;
									if (flag7)
									{
										bool flag8 = this.SerializeCommandToBuffer(ncommand, true);
										bool flag9 = flag8;
										if (flag9)
										{
											bool flag10 = base.debugOut >= DebugLevel.ALL;
											if (flag10)
											{
												base.Listener.DebugReturn(DebugLevel.ALL, string.Format("Resending: {0}. now: {1} rtt/var: {2}/{3} last recv: {4} didFit: {5}", new object[]
												{
													ncommand,
													base.timeInt,
													this.roundTripTime,
													this.roundTripTimeVariance,
													base.timeInt - this.timestampOfLastReceive,
													flag8
												}));
											}
											this.reliableCommandsRepeated++;
										}
										else
										{
											num3++;
											num2 = this.timeoutInt;
											bool flag11 = base.mtu - this.udpBufferIndex < 80;
											if (flag11)
											{
												break;
											}
										}
									}
									else
									{
										bool flag12 = num4 < num2;
										if (flag12)
										{
											num2 = num4;
										}
									}
								}
								num += num3;
								this.timeoutInt = num2;
							}
						}
						bool flag13 = this.udpCommandCount <= 0;
						if (flag13)
						{
							result = false;
						}
						else
						{
							bool trafficStatsEnabled = base.TrafficStatsEnabled;
							if (trafficStatsEnabled)
							{
								TrafficStats trafficStatsOutgoing = base.TrafficStatsOutgoing;
								int totalPacketCount = trafficStatsOutgoing.TotalPacketCount;
								trafficStatsOutgoing.TotalPacketCount = totalPacketCount + 1;
								base.TrafficStatsOutgoing.TotalCommandsInPackets += (int)this.udpCommandCount;
							}
							this.SendData(this.udpBuffer, this.udpBufferIndex);
							result = (num > 0);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000473C File Offset: 0x0000293C
		internal override bool SendOutgoingCommands()
		{
			bool flag = this.peerConnectionState == ConnectionStateValue.Disconnected;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = !this.PhotonSocket.Connected;
				if (flag2)
				{
					result = false;
				}
				else
				{
					byte[] obj = this.udpBuffer;
					lock (obj)
					{
						int num = 0;
						this.udpBufferIndex = this.CalculateInitialOffset();
						this.udpCommandCount = 0;
						this.timeLastSendOutgoing = base.timeInt;
						StreamBuffer obj2 = this.outgoingAcknowledgementsPool;
						lock (obj2)
						{
							bool flag5 = this.outgoingAcknowledgementsPool.Length > 0;
							if (flag5)
							{
								num = this.SerializeAckToBuffer();
								this.timeLastSendAck = base.timeInt;
							}
						}
						bool flag6 = base.timeInt > this.timeoutInt && this.sentReliableCommands.Count > 0;
						if (flag6)
						{
							int num2 = base.timeInt + 100;
							List<NCommand> obj3 = this.sentReliableCommands;
							lock (obj3)
							{
								int num3 = 0;
								for (int i = 0; i < this.sentReliableCommands.Count; i++)
								{
									NCommand ncommand = this.sentReliableCommands[i];
									int num4 = ncommand.commandSentTime + ncommand.roundTripTimeout;
									bool flag8 = base.timeInt > num4;
									if (flag8)
									{
										bool flag9 = (int)ncommand.commandSentCount > this.photonPeer.SentCountAllowance || base.timeInt > ncommand.timeoutTime;
										if (flag9)
										{
											bool flag10 = base.debugOut >= DebugLevel.WARNING;
											if (flag10)
											{
												base.Listener.DebugReturn(DebugLevel.WARNING, string.Format("Timeout-disconnect! Command: {0} now: {1} challenge: {2}", ncommand, base.timeInt, Convert.ToString(this.challenge, 16)));
												bool flag11 = base.debugOut >= DebugLevel.INFO;
												if (flag11)
												{
													base.Listener.DebugReturn(DebugLevel.INFO, string.Format("QueuedOutgoing: {0} channel.LowestUnAckd: {1} sentReliableCommands: {2}", this.QueuedOutgoingCommandsCount, this.GetChannel(ncommand.commandChannelID).lowestUnacknowledgedSequenceNumber, this.sentReliableCommands.Count));
												}
											}
											this.peerConnectionState = ConnectionStateValue.Zombie;
											base.EnqueueStatusCallback(StatusCode.TimeoutDisconnect);
											this.Disconnect();
											ncommand.Release();
											return false;
										}
										bool flag12 = this.SerializeCommandToBuffer(ncommand, true);
										bool flag13 = flag12;
										if (flag13)
										{
											bool flag14 = base.debugOut >= DebugLevel.ALL;
											if (flag14)
											{
												base.Listener.DebugReturn(DebugLevel.ALL, string.Format("Resending: {0}. now: {1} rtt/var: {2}/{3} last recv: {4}", new object[]
												{
													ncommand,
													base.timeInt,
													this.roundTripTime,
													this.roundTripTimeVariance,
													base.timeInt - this.timestampOfLastReceive
												}));
											}
											this.reliableCommandsRepeated++;
										}
										else
										{
											num3++;
											num2 = this.timeoutInt;
											bool flag15 = base.mtu - this.udpBufferIndex < 80;
											if (flag15)
											{
												break;
											}
										}
									}
									else
									{
										bool flag16 = num4 < num2;
										if (flag16)
										{
											num2 = num4;
										}
									}
								}
								num += num3;
								this.timeoutInt = num2;
							}
						}
						bool flag17 = this.peerConnectionState == ConnectionStateValue.Connected && base.timePingInterval > 0 && this.sentReliableCommands.Count == 0 && base.timeInt - this.timeLastAckReceive > base.timePingInterval && this.CalculatePacketSize(this.udpBufferIndex + 12) <= base.mtu;
						if (flag17)
						{
							NCommand ncommand2 = this.nCommandPool.Acquire(this, 5, null, byte.MaxValue);
							this.QueueOutgoingReliableCommand(ncommand2);
							bool trafficStatsEnabled = base.TrafficStatsEnabled;
							if (trafficStatsEnabled)
							{
								base.TrafficStatsOutgoing.CountControlCommand(ncommand2.Size);
							}
						}
						bool sendWindowUpdateRequired = this.sendWindowUpdateRequired;
						if (sendWindowUpdateRequired)
						{
							this.UpdateSendWindow();
						}
						EnetChannel[] obj4 = this.channelArray;
						lock (obj4)
						{
							for (int j = 0; j < this.channelArray.Length; j++)
							{
								EnetChannel enetChannel = this.channelArray[j];
								EnetChannel obj5 = enetChannel;
								lock (obj5)
								{
									int channelSequenceLimit = enetChannel.lowestUnacknowledgedSequenceNumber + this.photonPeer.SendWindowSize;
									num += this.SerializeToBuffer(enetChannel.outgoingReliableCommandsList, channelSequenceLimit);
									num += this.SerializeToBuffer(enetChannel.outgoingUnreliableCommandsList, channelSequenceLimit);
								}
							}
						}
						bool flag20 = this.udpCommandCount <= 0;
						if (flag20)
						{
							result = false;
						}
						else
						{
							bool trafficStatsEnabled2 = base.TrafficStatsEnabled;
							if (trafficStatsEnabled2)
							{
								TrafficStats trafficStatsOutgoing = base.TrafficStatsOutgoing;
								int totalPacketCount = trafficStatsOutgoing.TotalPacketCount;
								trafficStatsOutgoing.TotalPacketCount = totalPacketCount + 1;
								base.TrafficStatsOutgoing.TotalCommandsInPackets += (int)this.udpCommandCount;
							}
							this.SendData(this.udpBuffer, this.udpBufferIndex);
							result = (num > 0);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004CFC File Offset: 0x00002EFC
		private void UpdateSendWindow()
		{
			this.sendWindowUpdateRequired = false;
			bool flag = this.photonPeer.SendWindowSize <= 0;
			if (!flag)
			{
				bool flag2 = this.sentReliableCommands.Count == 0;
				if (flag2)
				{
					EnetChannel[] obj = this.channelArray;
					lock (obj)
					{
						for (int i = 0; i < this.channelArray.Length; i++)
						{
							EnetChannel enetChannel = this.channelArray[i];
							enetChannel.reliableCommandsInFlight = 0;
							enetChannel.lowestUnacknowledgedSequenceNumber = enetChannel.highestReceivedAck + 1;
						}
					}
				}
				else
				{
					this.channelsToUpdateLowestSent.Clear();
					EnetChannel[] obj2 = this.channelArray;
					lock (obj2)
					{
						for (int j = 0; j < this.channelArray.Length; j++)
						{
							EnetChannel enetChannel2 = this.channelArray[j];
							bool flag5 = enetChannel2.ChannelNumber != byte.MaxValue && enetChannel2.reliableCommandsInFlight > 0;
							if (flag5)
							{
								this.channelsToUpdateLowestSent.Add(enetChannel2.ChannelNumber);
							}
						}
					}
					bool flag6 = this.lowestSentSequenceNumber == null || this.lowestSentSequenceNumber.Length != this.channelArray.Length;
					if (flag6)
					{
						this.lowestSentSequenceNumber = new int[this.channelArray.Length];
					}
					else
					{
						for (int k = 0; k < this.lowestSentSequenceNumber.Length; k++)
						{
							this.lowestSentSequenceNumber[k] = 0;
						}
					}
					List<NCommand> obj3 = this.sentReliableCommands;
					lock (obj3)
					{
						for (int l = 0; l < this.sentReliableCommands.Count; l++)
						{
							NCommand ncommand = this.sentReliableCommands[l];
							bool flag8 = ncommand.IsFlaggedUnsequenced || ncommand.commandChannelID == byte.MaxValue;
							if (!flag8)
							{
								int commandChannelID = (int)ncommand.commandChannelID;
								bool flag9 = this.channelsToUpdateLowestSent.Contains(ncommand.commandChannelID);
								if (flag9)
								{
									bool flag10 = this.lowestSentSequenceNumber[commandChannelID] == 0;
									if (flag10)
									{
										this.lowestSentSequenceNumber[commandChannelID] = ncommand.reliableSequenceNumber;
									}
									this.channelsToUpdateLowestSent.Remove(ncommand.commandChannelID);
									bool flag11 = this.channelsToUpdateLowestSent.Count == 0;
									if (flag11)
									{
										break;
									}
								}
							}
						}
					}
					EnetChannel[] obj4 = this.channelArray;
					lock (obj4)
					{
						for (int m = 0; m < this.channelArray.Length; m++)
						{
							EnetChannel enetChannel3 = this.channelArray[m];
							enetChannel3.lowestUnacknowledgedSequenceNumber = ((this.lowestSentSequenceNumber[m] > 0) ? this.lowestSentSequenceNumber[m] : (enetChannel3.highestReceivedAck + 1));
						}
					}
				}
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00005040 File Offset: 0x00003240
		internal override bool EnqueuePhotonMessage(StreamBuffer opBytes, SendOptions sendParams)
		{
			byte commandType = 7;
			bool flag = sendParams.DeliveryMode == DeliveryMode.UnreliableUnsequenced;
			if (flag)
			{
				commandType = 11;
			}
			else
			{
				bool flag2 = sendParams.DeliveryMode == DeliveryMode.ReliableUnsequenced;
				if (flag2)
				{
					commandType = 14;
				}
				else
				{
					bool flag3 = sendParams.DeliveryMode == DeliveryMode.Reliable;
					if (flag3)
					{
						commandType = 6;
					}
				}
			}
			return this.CreateAndEnqueueCommand(commandType, opBytes, sendParams.Channel);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000050A0 File Offset: 0x000032A0
		internal bool CreateAndEnqueueCommand(byte commandType, StreamBuffer payload, byte channelNumber)
		{
			EnetChannel channel = this.GetChannel(channelNumber);
			this.ByteCountLastOperation = 0;
			int num = this.GetFragmentLength();
			bool flag = num == 0;
			if (flag)
			{
				num = 1000;
				base.EnqueueDebugReturn(DebugLevel.WARNING, "Value of currentFragmentSize should not be 0. Corrected to 1000.");
			}
			bool flag2 = payload == null || payload.Length <= num;
			if (flag2)
			{
				NCommand ncommand = this.nCommandPool.Acquire(this, commandType, payload, channel.ChannelNumber);
				bool isFlaggedReliable = ncommand.IsFlaggedReliable;
				if (isFlaggedReliable)
				{
					this.QueueOutgoingReliableCommand(ncommand);
					this.ByteCountLastOperation = ncommand.Size;
					bool trafficStatsEnabled = base.TrafficStatsEnabled;
					if (trafficStatsEnabled)
					{
						base.TrafficStatsOutgoing.CountReliableOpCommand(ncommand.Size);
						base.TrafficStatsGameLevel.CountOperation(ncommand.Size);
					}
				}
				else
				{
					this.QueueOutgoingUnreliableCommand(ncommand);
					this.ByteCountLastOperation = ncommand.Size;
					bool trafficStatsEnabled2 = base.TrafficStatsEnabled;
					if (trafficStatsEnabled2)
					{
						base.TrafficStatsOutgoing.CountUnreliableOpCommand(ncommand.Size);
						base.TrafficStatsGameLevel.CountOperation(ncommand.Size);
					}
				}
			}
			else
			{
				bool flag3 = commandType == 14 || commandType == 11;
				int fragmentCount = (payload.Length + num - 1) / num;
				int startSequenceNumber = (flag3 ? channel.outgoingReliableUnsequencedNumber : channel.outgoingReliableSequenceNumber) + 1;
				byte[] buffer = payload.GetBuffer();
				int num2 = 0;
				for (int i = 0; i < payload.Length; i += num)
				{
					bool flag4 = payload.Length - i < num;
					if (flag4)
					{
						num = payload.Length - i;
					}
					StreamBuffer streamBuffer = PeerBase.MessageBufferPoolGet();
					streamBuffer.Write(buffer, i, num);
					NCommand ncommand2 = this.nCommandPool.Acquire(this, flag3 ? 15 : 8, streamBuffer, channel.ChannelNumber);
					ncommand2.fragmentNumber = num2;
					ncommand2.startSequenceNumber = startSequenceNumber;
					ncommand2.fragmentCount = fragmentCount;
					ncommand2.totalLength = payload.Length;
					ncommand2.fragmentOffset = i;
					this.QueueOutgoingReliableCommand(ncommand2);
					this.ByteCountLastOperation += ncommand2.Size;
					bool trafficStatsEnabled3 = base.TrafficStatsEnabled;
					if (trafficStatsEnabled3)
					{
						base.TrafficStatsOutgoing.CountFragmentOpCommand(ncommand2.Size);
						base.TrafficStatsGameLevel.CountOperation(ncommand2.Size);
					}
					num2++;
				}
				PeerBase.MessageBufferPoolPut(payload);
			}
			return true;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00005308 File Offset: 0x00003508
		internal int SerializeAckToBuffer()
		{
			this.outgoingAcknowledgementsPool.Seek(0L, SeekOrigin.Begin);
			while (this.outgoingAcknowledgementsPool.Position + 20 <= this.outgoingAcknowledgementsPool.Length)
			{
				bool flag = this.CalculatePacketSize(this.udpBufferIndex + 20) > base.mtu;
				if (flag)
				{
					bool flag2 = base.debugOut >= DebugLevel.ALL;
					if (flag2)
					{
						base.Listener.DebugReturn(DebugLevel.ALL, "UDP package is full. Commands in Package: " + this.udpCommandCount.ToString() + ". bytes left in queue: " + this.outgoingAcknowledgementsPool.Position.ToString());
					}
					break;
				}
				int srcOffset;
				byte[] bufferAndAdvance = this.outgoingAcknowledgementsPool.GetBufferAndAdvance(20, out srcOffset);
				Buffer.BlockCopy(bufferAndAdvance, srcOffset, this.udpBuffer, this.udpBufferIndex, 20);
				this.udpBufferIndex += 20;
				this.udpCommandCount += 1;
			}
			this.outgoingAcknowledgementsPool.Compact();
			this.outgoingAcknowledgementsPool.Position = this.outgoingAcknowledgementsPool.Length;
			return this.outgoingAcknowledgementsPool.Length / 20;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000543C File Offset: 0x0000363C
		internal int SerializeToBuffer(Queue<NCommand> commandList, int channelSequenceLimit)
		{
			while (commandList.Count > 0)
			{
				NCommand ncommand = commandList.Peek();
				bool flag = ncommand == null;
				if (flag)
				{
					commandList.Dequeue();
				}
				else
				{
					bool flag2 = ncommand.IsFlaggedReliable && ncommand.commandChannelID != byte.MaxValue && this.photonPeer.SendWindowSize > 0;
					if (flag2)
					{
						bool flag3 = ncommand.reliableSequenceNumber >= channelSequenceLimit;
						if (flag3)
						{
							return 0;
						}
					}
					bool flag4 = this.SerializeCommandToBuffer(ncommand, false);
					bool flag5 = flag4;
					if (!flag5)
					{
						bool flag6 = base.debugOut >= DebugLevel.ALL;
						if (flag6)
						{
							base.Listener.DebugReturn(DebugLevel.ALL, "UDP package is full. Commands in Package: " + this.udpCommandCount.ToString() + " commandList.Count: " + commandList.Count.ToString());
						}
						break;
					}
					commandList.Dequeue();
				}
			}
			return commandList.Count;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000553C File Offset: 0x0000373C
		private bool SerializeCommandToBuffer(NCommand command, bool commandIsInSentQueue = false)
		{
			bool flag = command == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = this.CalculatePacketSize(this.udpBufferIndex + command.Size) > base.mtu;
				if (flag2)
				{
					result = false;
				}
				else
				{
					command.SerializeHeader(this.udpBuffer, ref this.udpBufferIndex);
					bool flag3 = command.SizeOfPayload > 0;
					if (flag3)
					{
						Buffer.BlockCopy(command.Serialize(), 0, this.udpBuffer, this.udpBufferIndex, command.SizeOfPayload);
						this.udpBufferIndex += command.SizeOfPayload;
					}
					this.udpCommandCount += 1;
					bool isFlaggedReliable = command.IsFlaggedReliable;
					if (isFlaggedReliable)
					{
						this.QueueSentCommand(command, commandIsInSentQueue);
					}
					else
					{
						command.FreePayload();
						command.Release();
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00005614 File Offset: 0x00003814
		internal void SendData(byte[] data, int length)
		{
			try
			{
				bool flag = this.datagramEncryptedConnection;
				if (flag)
				{
					this.SendDataEncrypted(data, length);
				}
				else
				{
					int num = 0;
					Protocol.Serialize(this.peerID, data, ref num);
					data[2] = (this.photonPeer.CrcEnabled ? 204 : 0);
					data[3] = this.udpCommandCount;
					num = 4;
					Protocol.Serialize(base.timeInt, data, ref num);
					Protocol.Serialize(this.challenge, data, ref num);
					bool crcEnabled = this.photonPeer.CrcEnabled;
					if (crcEnabled)
					{
						Protocol.Serialize(0, data, ref num);
						uint value = SupportClass.CalculateCrc(data, length);
						num -= 4;
						Protocol.Serialize((int)value, data, ref num);
					}
					this.SendToSocket(data, length);
				}
			}
			catch (Exception ex)
			{
				bool flag2 = base.debugOut >= DebugLevel.ERROR;
				if (flag2)
				{
					base.Listener.DebugReturn(DebugLevel.ERROR, ex.ToString());
				}
				SupportClass.WriteStackTrace(ex);
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00005714 File Offset: 0x00003914
		private void SendToSocket(byte[] data, int length)
		{
			this.bytesOut += (long)length;
			ITrafficRecorder trafficRecorder = this.photonPeer.TrafficRecorder;
			bool flag = trafficRecorder != null && trafficRecorder.Enabled;
			if (flag)
			{
				trafficRecorder.Record(data, length, false, this.peerID, this.PhotonSocket);
			}
			bool isSimulationEnabled = base.NetworkSimulationSettings.IsSimulationEnabled;
			if (isSimulationEnabled)
			{
				byte[] array = new byte[length];
				Buffer.BlockCopy(data, 0, array, 0, length);
				base.SendNetworkSimulated(array);
			}
			else
			{
				int timeInt = base.timeInt;
				this.PhotonSocket.Send(data, length);
				int num = base.timeInt - timeInt;
				bool flag2 = num > this.longestSentCall;
				if (flag2)
				{
					this.longestSentCall = num;
				}
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000057D0 File Offset: 0x000039D0
		private void SendDataEncrypted(byte[] data, int length)
		{
			bool flag = this.bufferForEncryption == null || this.bufferForEncryption.Length != base.mtu;
			if (flag)
			{
				this.bufferForEncryption = new byte[base.mtu];
			}
			byte[] array = this.bufferForEncryption;
			int num = 0;
			Protocol.Serialize(this.peerID, array, ref num);
			array[2] = 1;
			num++;
			Protocol.Serialize(this.challenge, array, ref num);
			data[0] = this.udpCommandCount;
			int num2 = 1;
			Protocol.Serialize(base.timeInt, data, ref num2);
			int num3 = array.Length - num;
			this.photonPeer.Encryptor.Encrypt2(data, length, array, array, num, ref num3);
			this.SendToSocket(array, num3 + num);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00005888 File Offset: 0x00003A88
		internal void QueueSentCommand(NCommand command, bool commandIsAlreadyInSentQueue = false)
		{
			command.commandSentTime = base.timeInt;
			bool flag = command.roundTripTimeout == 0;
			if (flag)
			{
				command.roundTripTimeout = Math.Min(this.roundTripTime + 4 * this.roundTripTimeVariance, this.photonPeer.InitialResendTimeMax);
				command.timeoutTime = base.timeInt + base.DisconnectTimeout;
				this.reliableCommandsSent++;
			}
			else
			{
				bool flag2 = command.commandSentCount <= this.photonPeer.QuickResendAttempts && this.sentReliableCommands.Count < 25;
				if (!flag2)
				{
					command.roundTripTimeout *= 2;
				}
			}
			command.commandSentCount += 1;
			int num = command.commandSentTime + command.roundTripTimeout;
			bool flag3 = num < this.timeoutInt;
			if (flag3)
			{
				this.timeoutInt = num;
			}
			bool flag4 = !commandIsAlreadyInSentQueue;
			if (flag4)
			{
				EnetChannel channel = this.GetChannel(command.commandChannelID);
				channel.reliableCommandsInFlight++;
				List<NCommand> obj = this.sentReliableCommands;
				lock (obj)
				{
					this.sentReliableCommands.Add(command);
				}
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000059D8 File Offset: 0x00003BD8
		internal void QueueOutgoingReliableCommand(NCommand command)
		{
			EnetChannel channel = this.GetChannel(command.commandChannelID);
			EnetChannel obj = channel;
			lock (obj)
			{
				bool flag2 = command.reliableSequenceNumber == 0;
				if (flag2)
				{
					bool isFlaggedUnsequenced = command.IsFlaggedUnsequenced;
					if (isFlaggedUnsequenced)
					{
						EnetChannel enetChannel = channel;
						int num = enetChannel.outgoingReliableUnsequencedNumber + 1;
						enetChannel.outgoingReliableUnsequencedNumber = num;
						command.reliableSequenceNumber = num;
					}
					else
					{
						EnetChannel enetChannel2 = channel;
						int num = enetChannel2.outgoingReliableSequenceNumber + 1;
						enetChannel2.outgoingReliableSequenceNumber = num;
						command.reliableSequenceNumber = num;
					}
				}
				channel.outgoingReliableCommandsList.Enqueue(command);
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00005A84 File Offset: 0x00003C84
		internal void QueueOutgoingUnreliableCommand(NCommand command)
		{
			EnetChannel channel = this.GetChannel(command.commandChannelID);
			EnetChannel obj = channel;
			lock (obj)
			{
				bool isFlaggedUnsequenced = command.IsFlaggedUnsequenced;
				if (isFlaggedUnsequenced)
				{
					command.reliableSequenceNumber = 0;
					int num = this.outgoingUnsequencedGroupNumber + 1;
					this.outgoingUnsequencedGroupNumber = num;
					command.unsequencedGroupNumber = num;
				}
				else
				{
					command.reliableSequenceNumber = channel.outgoingReliableSequenceNumber;
					EnetChannel enetChannel = channel;
					int num = enetChannel.outgoingUnreliableSequenceNumber + 1;
					enetChannel.outgoingUnreliableSequenceNumber = num;
					command.unreliableSequenceNumber = num;
				}
				bool flag2 = !this.photonPeer.SendInCreationOrder;
				if (flag2)
				{
					channel.outgoingUnreliableCommandsList.Enqueue(command);
				}
				else
				{
					channel.outgoingReliableCommandsList.Enqueue(command);
				}
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00005B58 File Offset: 0x00003D58
		internal void QueueOutgoingAcknowledgement(NCommand readCommand, int sendTime)
		{
			StreamBuffer obj = this.outgoingAcknowledgementsPool;
			lock (obj)
			{
				int offset;
				byte[] bufferAndAdvance = this.outgoingAcknowledgementsPool.GetBufferAndAdvance(20, out offset);
				NCommand.CreateAck(bufferAndAdvance, offset, readCommand, sendTime);
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00005BB4 File Offset: 0x00003DB4
		internal override void ReceiveIncomingCommands(byte[] inBuff, int inDataLength)
		{
			this.timestampOfLastReceive = base.timeInt;
			bool flag = this.peerConnectionState == ConnectionStateValue.Disconnected;
			if (!flag)
			{
				try
				{
					int num = 0;
					short num2;
					Protocol.Deserialize(out num2, inBuff, ref num);
					byte b = inBuff[num++];
					bool flag2 = b == 1;
					byte b2;
					if (flag2)
					{
						bool flag3 = this.photonPeer.Encryptor == null;
						if (flag3)
						{
							return;
						}
						int num3;
						Protocol.Deserialize(out num3, inBuff, ref num);
						bool flag4 = num3 != this.challenge;
						if (flag4)
						{
							this.packetLossByChallenge++;
							return;
						}
						int num4;
						inBuff = this.photonPeer.Encryptor.Decrypt2(inBuff, num, inDataLength - num, inBuff, out num4);
						bool flag5 = !this.datagramEncryptedConnection;
						if (flag5)
						{
							byte[] obj = this.udpBuffer;
							lock (obj)
							{
								this.datagramEncryptedConnection = true;
								this.fragmentLength = 0;
							}
						}
						num = 0;
						b2 = inBuff[num++];
						Protocol.Deserialize(out this.serverSentTime, inBuff, ref num);
						this.bytesIn += (long)inDataLength;
					}
					else
					{
						bool flag7 = this.datagramEncryptedConnection;
						if (flag7)
						{
							bool flag8 = base.debugOut >= DebugLevel.WARNING;
							if (flag8)
							{
								base.EnqueueDebugReturn(DebugLevel.WARNING, "Ignored received package. Connection requires Datagram Encryption but received unencrypted datagram.");
							}
							return;
						}
						b2 = inBuff[num++];
						Protocol.Deserialize(out this.serverSentTime, inBuff, ref num);
						int num3;
						Protocol.Deserialize(out num3, inBuff, ref num);
						bool flag9 = num3 != this.challenge;
						if (flag9)
						{
							this.packetLossByChallenge++;
							bool flag10 = this.peerConnectionState != ConnectionStateValue.Disconnected && base.debugOut >= DebugLevel.ALL;
							if (flag10)
							{
								base.EnqueueDebugReturn(DebugLevel.ALL, "Ignored received package due to wrong challenge. Received:" + num3.ToString() + " local: " + this.challenge.ToString());
							}
							return;
						}
						bool flag11 = b == 204;
						if (flag11)
						{
							int num5;
							Protocol.Deserialize(out num5, inBuff, ref num);
							this.bytesIn += 4L;
							num -= 4;
							Protocol.Serialize(0, inBuff, ref num);
							uint num6 = SupportClass.CalculateCrc(inBuff, inDataLength);
							bool flag12 = num5 != (int)num6;
							if (flag12)
							{
								this.packetLossByCrc++;
								bool flag13 = this.peerConnectionState != ConnectionStateValue.Disconnected && base.debugOut >= DebugLevel.INFO;
								if (flag13)
								{
									base.EnqueueDebugReturn(DebugLevel.INFO, string.Format("Ignored package due to wrong CRC. Incoming:  {0:X} Local: {1:X}", (uint)num5, num6));
								}
								return;
							}
						}
						this.bytesIn += 12L;
					}
					bool trafficStatsEnabled = base.TrafficStatsEnabled;
					if (trafficStatsEnabled)
					{
						TrafficStats trafficStatsIncoming = base.TrafficStatsIncoming;
						int totalPacketCount = trafficStatsIncoming.TotalPacketCount;
						trafficStatsIncoming.TotalPacketCount = totalPacketCount + 1;
						base.TrafficStatsIncoming.TotalCommandsInPackets += (int)b2;
					}
					bool flag14 = (int)b2 > this.commandBufferSize || b2 <= 0;
					if (flag14)
					{
						base.EnqueueDebugReturn(DebugLevel.ERROR, "too many/few incoming commands in package: " + b2.ToString() + " > " + this.commandBufferSize.ToString());
					}
					bool flag15 = false;
					for (int i = 0; i < (int)b2; i++)
					{
						NCommand ncommand = this.nCommandPool.Acquire(this, inBuff, ref num);
						bool flag16 = ncommand.commandType == 1 || ncommand.commandType == 16;
						if (flag16)
						{
							this.ExecuteCommand(ncommand);
							flag15 = true;
						}
						else
						{
							Queue<NCommand> commandQueue = this.CommandQueue;
							lock (commandQueue)
							{
								this.CommandQueue.Enqueue(ncommand);
							}
						}
						bool isFlaggedReliable = ncommand.IsFlaggedReliable;
						if (isFlaggedReliable)
						{
							this.QueueOutgoingAcknowledgement(ncommand, this.serverSentTime);
							bool trafficStatsEnabled2 = base.TrafficStatsEnabled;
							if (trafficStatsEnabled2)
							{
								base.TrafficStatsIncoming.TimestampOfLastReliableCommand = base.timeInt;
								base.TrafficStatsOutgoing.CountControlCommand(20);
							}
						}
					}
					bool flag18 = flag15;
					if (flag18)
					{
						this.sendWindowUpdateRequired = true;
					}
				}
				catch (Exception ex)
				{
					bool flag19 = base.debugOut >= DebugLevel.ERROR;
					if (flag19)
					{
						base.EnqueueDebugReturn(DebugLevel.ERROR, string.Format("Exception while reading commands from incoming data: {0}", ex));
					}
					SupportClass.WriteStackTrace(ex);
				}
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00006058 File Offset: 0x00004258
		internal void ExecuteCommand(NCommand command)
		{
			switch (command.commandType)
			{
			case 1:
			case 16:
			{
				bool trafficStatsEnabled = base.TrafficStatsEnabled;
				if (trafficStatsEnabled)
				{
					base.TrafficStatsIncoming.TimestampOfLastAck = base.timeInt;
					base.TrafficStatsIncoming.CountControlCommand(command.Size);
				}
				this.timeLastAckReceive = base.timeInt;
				this.lastRoundTripTime = base.timeInt - command.ackReceivedSentTime;
				bool flag = this.lastRoundTripTime < 0 || this.lastRoundTripTime > 10000;
				if (flag)
				{
					bool flag2 = base.debugOut >= DebugLevel.INFO;
					if (flag2)
					{
						base.EnqueueDebugReturn(DebugLevel.INFO, "Measured lastRoundtripTime is suspicious: " + this.lastRoundTripTime.ToString() + " for command: " + ((command != null) ? command.ToString() : null));
					}
					this.lastRoundTripTime = this.roundTripTime * 4;
				}
				NCommand ncommand = this.RemoveSentReliableCommand(command.ackReceivedReliableSequenceNumber, (int)command.commandChannelID, command.commandType == 16);
				command.Release();
				bool flag3 = ncommand != null;
				if (flag3)
				{
					ncommand.FreePayload();
					EnetChannel channel = this.GetChannel(ncommand.commandChannelID);
					EnetChannel obj = channel;
					lock (obj)
					{
						bool flag5 = ncommand.reliableSequenceNumber > channel.highestReceivedAck;
						if (flag5)
						{
							channel.highestReceivedAck = ncommand.reliableSequenceNumber;
						}
						channel.reliableCommandsInFlight--;
					}
					bool flag6 = ncommand.commandType == 12;
					if (flag6)
					{
						bool flag7 = this.lastRoundTripTime <= this.roundTripTime;
						if (flag7)
						{
							this.serverTimeOffset = this.serverSentTime + (this.lastRoundTripTime >> 1) - base.timeInt;
							this.serverTimeOffsetIsAvailable = true;
						}
						else
						{
							this.FetchServerTimestamp();
						}
					}
					else
					{
						base.UpdateRoundTripTimeAndVariance(this.lastRoundTripTime);
						bool flag8 = ncommand.commandType == 4 && this.peerConnectionState == ConnectionStateValue.Disconnecting;
						if (flag8)
						{
							bool flag9 = base.debugOut >= DebugLevel.INFO;
							if (flag9)
							{
								base.EnqueueDebugReturn(DebugLevel.INFO, "Received ACK for previously sent Disconnect command.");
							}
							base.EnqueueActionForDispatch(delegate
							{
								this.PhotonSocket.Disconnect();
							});
						}
						else
						{
							bool flag10 = ncommand.commandType == 2;
							if (flag10)
							{
								bool flag11 = this.lastRoundTripTime < 0;
								if (!flag11)
								{
									bool flag12 = this.lastRoundTripTime <= 15;
									if (flag12)
									{
										this.roundTripTime = 15;
										this.roundTripTimeVariance = 5;
									}
									else
									{
										this.roundTripTime = this.lastRoundTripTime;
									}
								}
							}
						}
					}
					ncommand.Release();
				}
				break;
			}
			case 2:
			case 5:
			{
				bool trafficStatsEnabled2 = base.TrafficStatsEnabled;
				if (trafficStatsEnabled2)
				{
					base.TrafficStatsIncoming.CountControlCommand(command.Size);
				}
				command.Release();
				break;
			}
			case 3:
			{
				bool trafficStatsEnabled3 = base.TrafficStatsEnabled;
				if (trafficStatsEnabled3)
				{
					base.TrafficStatsIncoming.CountControlCommand(command.Size);
				}
				bool flag13 = this.peerConnectionState == ConnectionStateValue.Connecting;
				if (flag13)
				{
					byte[] buf = base.WriteInitRequest();
					this.CreateAndEnqueueCommand(6, new StreamBuffer(buf), 0);
					bool randomizeSequenceNumbers = this.photonPeer.RandomizeSequenceNumbers;
					if (randomizeSequenceNumbers)
					{
						this.ApplyRandomizedSequenceNumbers();
					}
					this.peerConnectionState = ConnectionStateValue.Connected;
				}
				command.Release();
				break;
			}
			case 4:
			{
				bool trafficStatsEnabled4 = base.TrafficStatsEnabled;
				if (trafficStatsEnabled4)
				{
					base.TrafficStatsIncoming.CountControlCommand(command.Size);
				}
				StatusCode statusValue = StatusCode.DisconnectByServerReasonUnknown;
				bool flag14 = command.reservedByte == 1;
				if (flag14)
				{
					statusValue = StatusCode.DisconnectByServerLogic;
				}
				else
				{
					bool flag15 = command.reservedByte == 2;
					if (flag15)
					{
						statusValue = StatusCode.DisconnectByServerTimeout;
					}
					else
					{
						bool flag16 = command.reservedByte == 3;
						if (flag16)
						{
							statusValue = StatusCode.DisconnectByServerUserLimit;
						}
					}
				}
				bool flag17 = base.debugOut >= DebugLevel.INFO;
				if (flag17)
				{
					base.Listener.DebugReturn(DebugLevel.INFO, string.Concat(new string[]
					{
						"Server ",
						base.ServerAddress,
						" sent disconnect. PeerId: ",
						((ushort)this.peerID).ToString(),
						" RTT/Variance:",
						this.roundTripTime.ToString(),
						"/",
						this.roundTripTimeVariance.ToString(),
						" reason byte: ",
						command.reservedByte.ToString(),
						" peerConnectionState: ",
						this.peerConnectionState.ToString()
					}));
				}
				bool flag18 = this.peerConnectionState != ConnectionStateValue.Disconnected && this.peerConnectionState != ConnectionStateValue.Disconnecting;
				if (flag18)
				{
					base.EnqueueStatusCallback(statusValue);
					this.Disconnect();
				}
				command.Release();
				break;
			}
			case 6:
			case 7:
			case 11:
			case 14:
			{
				bool trafficStatsEnabled5 = base.TrafficStatsEnabled;
				if (trafficStatsEnabled5)
				{
					bool isFlaggedReliable = command.IsFlaggedReliable;
					if (isFlaggedReliable)
					{
						base.TrafficStatsIncoming.CountReliableOpCommand(command.Size);
					}
					else
					{
						base.TrafficStatsIncoming.CountUnreliableOpCommand(command.Size);
					}
				}
				bool flag19 = this.peerConnectionState == ConnectionStateValue.Connected && this.QueueIncomingCommand(command);
				bool flag20 = !flag19;
				if (flag20)
				{
					command.Release();
				}
				break;
			}
			case 8:
			case 15:
			{
				bool flag21 = this.peerConnectionState != ConnectionStateValue.Connected;
				if (flag21)
				{
					command.Release();
				}
				else
				{
					bool trafficStatsEnabled6 = base.TrafficStatsEnabled;
					if (trafficStatsEnabled6)
					{
						base.TrafficStatsIncoming.CountFragmentOpCommand(command.Size);
					}
					bool flag22 = command.fragmentNumber > command.fragmentCount || command.fragmentOffset >= command.totalLength || command.fragmentOffset + command.Payload.Length > command.totalLength;
					if (flag22)
					{
						bool flag23 = base.debugOut >= DebugLevel.ERROR;
						if (flag23)
						{
							base.Listener.DebugReturn(DebugLevel.ERROR, "Received fragment has bad size: " + ((command != null) ? command.ToString() : null));
						}
						command.Release();
					}
					else
					{
						bool flag24 = command.commandType == 8;
						EnetChannel channel2 = this.GetChannel(command.commandChannelID);
						NCommand ncommand2 = null;
						bool flag25 = channel2.TryGetFragment(command.startSequenceNumber, flag24, out ncommand2);
						bool flag26 = flag25 && ncommand2.fragmentsRemaining <= 0;
						if (flag26)
						{
							command.Release();
						}
						else
						{
							bool flag27 = this.QueueIncomingCommand(command);
							bool flag28 = !flag27;
							if (flag28)
							{
								command.Release();
							}
							else
							{
								bool flag29 = command.reliableSequenceNumber != command.startSequenceNumber;
								if (flag29)
								{
									bool flag30 = flag25;
									if (flag30)
									{
										ncommand2.fragmentsRemaining--;
									}
								}
								else
								{
									ncommand2 = command;
									ncommand2.fragmentsRemaining--;
									NCommand ncommand3 = null;
									int num = command.startSequenceNumber + 1;
									while (ncommand2.fragmentsRemaining > 0 && num < ncommand2.startSequenceNumber + ncommand2.fragmentCount)
									{
										bool flag31 = channel2.TryGetFragment(num++, flag24, out ncommand3);
										if (flag31)
										{
											ncommand2.fragmentsRemaining--;
										}
									}
								}
								bool flag32 = ncommand2 == null || ncommand2.fragmentsRemaining > 0;
								if (!flag32)
								{
									StreamBuffer streamBuffer = PeerBase.MessageBufferPoolGet();
									streamBuffer.Position = 0;
									streamBuffer.SetCapacityMinimum(ncommand2.totalLength);
									byte[] buffer = streamBuffer.GetBuffer();
									for (int i = ncommand2.startSequenceNumber; i < ncommand2.startSequenceNumber + ncommand2.fragmentCount; i++)
									{
										NCommand ncommand4;
										bool flag33 = channel2.TryGetFragment(i, flag24, out ncommand4);
										if (!flag33)
										{
											throw new Exception("startCommand.fragmentsRemaining was 0 but not all fragments were found to be combined!");
										}
										Buffer.BlockCopy(ncommand4.Payload.GetBuffer(), 0, buffer, ncommand4.fragmentOffset, ncommand4.Payload.Length);
										ncommand4.FreePayload();
										channel2.RemoveFragment(ncommand4.reliableSequenceNumber, flag24);
										bool flag34 = ncommand4.fragmentNumber > 0;
										if (flag34)
										{
											ncommand4.Release();
										}
									}
									streamBuffer.SetLength((long)ncommand2.totalLength);
									ncommand2.Payload = streamBuffer;
									ncommand2.Size = 12 * ncommand2.fragmentCount + ncommand2.totalLength;
									bool flag35 = flag24;
									if (flag35)
									{
										channel2.incomingReliableCommandsList.Add(ncommand2.startSequenceNumber, ncommand2);
									}
									else
									{
										channel2.incomingUnsequencedCommandsList.Enqueue(ncommand2);
									}
								}
							}
						}
					}
				}
				break;
			}
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00006908 File Offset: 0x00004B08
		internal bool QueueIncomingCommand(NCommand command)
		{
			EnetChannel channel = this.GetChannel(command.commandChannelID);
			bool flag = channel == null;
			bool result;
			if (flag)
			{
				bool flag2 = base.debugOut >= DebugLevel.ERROR;
				if (flag2)
				{
					base.Listener.DebugReturn(DebugLevel.ERROR, "Received command for non-existing channel: " + command.commandChannelID.ToString());
				}
				result = false;
			}
			else
			{
				bool isFlaggedReliable = command.IsFlaggedReliable;
				if (isFlaggedReliable)
				{
					bool isFlaggedUnsequenced = command.IsFlaggedUnsequenced;
					if (isFlaggedUnsequenced)
					{
						result = channel.QueueIncomingReliableUnsequenced(command);
					}
					else
					{
						bool flag3 = command.reliableSequenceNumber <= channel.incomingReliableSequenceNumber;
						if (flag3)
						{
							bool flag4 = base.debugOut >= DebugLevel.ALL;
							if (flag4)
							{
								base.Listener.DebugReturn(DebugLevel.ALL, "incoming command " + ((command != null) ? command.ToString() : null) + " is old (not saving it). Dispatched incomingReliableSequenceNumber: " + channel.incomingReliableSequenceNumber.ToString());
							}
							result = false;
						}
						else
						{
							bool flag5 = channel.ContainsReliableSequenceNumber(command.reliableSequenceNumber);
							if (flag5)
							{
								bool flag6 = base.debugOut >= DebugLevel.ALL;
								if (flag6)
								{
									IPhotonPeerListener listener = base.Listener;
									DebugLevel level = DebugLevel.ALL;
									string[] array = new string[6];
									array[0] = "Info: command was received before! Old/New: ";
									int num = 1;
									NCommand ncommand = channel.FetchReliableSequenceNumber(command.reliableSequenceNumber);
									array[num] = ((ncommand != null) ? ncommand.ToString() : null);
									array[2] = "/";
									array[3] = ((command != null) ? command.ToString() : null);
									array[4] = " inReliableSeq#: ";
									array[5] = channel.incomingReliableSequenceNumber.ToString();
									listener.DebugReturn(level, string.Concat(array));
								}
								result = false;
							}
							else
							{
								channel.incomingReliableCommandsList.Add(command.reliableSequenceNumber, command);
								result = true;
							}
						}
					}
				}
				else
				{
					bool flag7 = command.commandFlags == 0;
					if (flag7)
					{
						bool flag8 = command.reliableSequenceNumber < channel.incomingReliableSequenceNumber;
						if (flag8)
						{
							PhotonPeer photonPeer = this.photonPeer;
							int countDiscarded = photonPeer.CountDiscarded;
							photonPeer.CountDiscarded = countDiscarded + 1;
							bool flag9 = base.debugOut >= DebugLevel.ALL;
							if (flag9)
							{
								base.Listener.DebugReturn(DebugLevel.ALL, "incoming reliable-seq# < Dispatched-rel-seq#. not saved.");
							}
							result = false;
						}
						else
						{
							bool flag10 = command.unreliableSequenceNumber <= channel.incomingUnreliableSequenceNumber;
							if (flag10)
							{
								PhotonPeer photonPeer2 = this.photonPeer;
								int countDiscarded = photonPeer2.CountDiscarded;
								photonPeer2.CountDiscarded = countDiscarded + 1;
								bool flag11 = base.debugOut >= DebugLevel.ALL;
								if (flag11)
								{
									base.Listener.DebugReturn(DebugLevel.ALL, "incoming unreliable-seq# < Dispatched-unrel-seq#. not saved.");
								}
								result = false;
							}
							else
							{
								bool flag12 = channel.ContainsUnreliableSequenceNumber(command.unreliableSequenceNumber);
								if (flag12)
								{
									bool flag13 = base.debugOut >= DebugLevel.ALL;
									if (flag13)
									{
										IPhotonPeerListener listener2 = base.Listener;
										DebugLevel level2 = DebugLevel.ALL;
										string str = "command was received before! Old/New: ";
										NCommand ncommand2 = channel.incomingUnreliableCommandsList[command.unreliableSequenceNumber];
										listener2.DebugReturn(level2, str + ((ncommand2 != null) ? ncommand2.ToString() : null) + "/" + ((command != null) ? command.ToString() : null));
									}
									result = false;
								}
								else
								{
									channel.incomingUnreliableCommandsList.Add(command.unreliableSequenceNumber, command);
									result = true;
								}
							}
						}
					}
					else
					{
						bool flag14 = command.commandFlags == 2;
						if (flag14)
						{
							int unsequencedGroupNumber = command.unsequencedGroupNumber;
							int num2 = command.unsequencedGroupNumber % 128;
							bool flag15 = unsequencedGroupNumber >= this.incomingUnsequencedGroupNumber + 128;
							if (flag15)
							{
								this.incomingUnsequencedGroupNumber = unsequencedGroupNumber - num2;
								for (int i = 0; i < this.unsequencedWindow.Length; i++)
								{
									this.unsequencedWindow[i] = 0;
								}
							}
							else
							{
								bool flag16 = unsequencedGroupNumber < this.incomingUnsequencedGroupNumber || (this.unsequencedWindow[num2 / 32] & 1 << num2 % 32) != 0;
								if (flag16)
								{
									return false;
								}
							}
							this.unsequencedWindow[num2 / 32] |= 1 << num2 % 32;
							channel.incomingUnsequencedCommandsList.Enqueue(command);
							result = true;
						}
						else
						{
							result = false;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00006CE8 File Offset: 0x00004EE8
		internal NCommand RemoveSentReliableCommand(int ackReceivedReliableSequenceNumber, int ackReceivedChannel, bool isUnsequenced)
		{
			NCommand ncommand = null;
			List<NCommand> obj = this.sentReliableCommands;
			lock (obj)
			{
				foreach (NCommand ncommand2 in this.sentReliableCommands)
				{
					bool flag2 = ncommand2 != null && ncommand2.reliableSequenceNumber == ackReceivedReliableSequenceNumber && (int)ncommand2.commandChannelID == ackReceivedChannel && ncommand2.IsFlaggedUnsequenced == isUnsequenced;
					if (flag2)
					{
						ncommand = ncommand2;
						break;
					}
				}
				bool flag3 = ncommand != null;
				if (flag3)
				{
					this.sentReliableCommands.Remove(ncommand);
				}
				else
				{
					bool flag4 = base.debugOut >= DebugLevel.ALL && this.peerConnectionState != ConnectionStateValue.Connected && this.peerConnectionState != ConnectionStateValue.Disconnecting;
					if (flag4)
					{
						base.EnqueueDebugReturn(DebugLevel.ALL, string.Format("No sent command for ACK (Ch: {0} Sq#: {1}). PeerState: {2}.", ackReceivedReliableSequenceNumber, ackReceivedChannel, this.peerConnectionState));
					}
				}
			}
			return ncommand;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00006E10 File Offset: 0x00005010
		internal string CommandListToString(NCommand[] list)
		{
			bool flag = base.debugOut < DebugLevel.ALL;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < list.Length; i++)
				{
					stringBuilder.Append(i.ToString() + "=");
					stringBuilder.Append(list[i]);
					stringBuilder.Append(" # ");
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00006E88 File Offset: 0x00005088
		// Note: this type is marked as 'beforefieldinit'.
		static EnetPeer()
		{
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00006EA1 File Offset: 0x000050A1
		[CompilerGenerated]
		private void <ExecuteCommand>b__73_0()
		{
			this.PhotonSocket.Disconnect();
		}

		// Token: 0x04000029 RID: 41
		private const int CRC_LENGTH = 4;

		// Token: 0x0400002A RID: 42
		private const int EncryptedDataGramHeaderSize = 7;

		// Token: 0x0400002B RID: 43
		private const int EncryptedHeaderSize = 5;

		// Token: 0x0400002C RID: 44
		private const int QUICK_RESEND_QUEUELIMIT = 25;

		// Token: 0x0400002D RID: 45
		internal NCommandPool nCommandPool = new NCommandPool();

		// Token: 0x0400002E RID: 46
		private List<NCommand> sentReliableCommands = new List<NCommand>();

		// Token: 0x0400002F RID: 47
		private int sendWindowUpdateRequiredBackValue = 0;

		// Token: 0x04000030 RID: 48
		private StreamBuffer outgoingAcknowledgementsPool;

		// Token: 0x04000031 RID: 49
		internal const int UnsequencedWindowSize = 128;

		// Token: 0x04000032 RID: 50
		internal readonly int[] unsequencedWindow = new int[4];

		// Token: 0x04000033 RID: 51
		internal int outgoingUnsequencedGroupNumber;

		// Token: 0x04000034 RID: 52
		internal int incomingUnsequencedGroupNumber;

		// Token: 0x04000035 RID: 53
		private byte udpCommandCount;

		// Token: 0x04000036 RID: 54
		private byte[] udpBuffer;

		// Token: 0x04000037 RID: 55
		private int udpBufferIndex;

		// Token: 0x04000038 RID: 56
		private byte[] bufferForEncryption;

		// Token: 0x04000039 RID: 57
		private int commandBufferSize = 100;

		// Token: 0x0400003A RID: 58
		internal int challenge;

		// Token: 0x0400003B RID: 59
		internal int reliableCommandsRepeated;

		// Token: 0x0400003C RID: 60
		internal int reliableCommandsSent;

		// Token: 0x0400003D RID: 61
		internal int serverSentTime;

		// Token: 0x0400003E RID: 62
		internal static readonly byte[] udpHeader0xF3 = new byte[]
		{
			243,
			2
		};

		// Token: 0x0400003F RID: 63
		protected bool datagramEncryptedConnection;

		// Token: 0x04000040 RID: 64
		private EnetChannel[] channelArray = new EnetChannel[0];

		// Token: 0x04000041 RID: 65
		private const byte ControlChannelNumber = 255;

		// Token: 0x04000042 RID: 66
		protected internal const short PeerIdForConnect = -1;

		// Token: 0x04000043 RID: 67
		protected internal const short PeerIdForConnectTrace = -2;

		// Token: 0x04000044 RID: 68
		private Queue<int> commandsToRemove = new Queue<int>();

		// Token: 0x04000045 RID: 69
		private int fragmentLength = 0;

		// Token: 0x04000046 RID: 70
		private int fragmentLengthDatagramEncrypt = 0;

		// Token: 0x04000047 RID: 71
		private int fragmentLengthMtuValue = 0;

		// Token: 0x04000048 RID: 72
		private Queue<NCommand> CommandQueue = new Queue<NCommand>();

		// Token: 0x04000049 RID: 73
		private readonly HashSet<byte> channelsToUpdateLowestSent = new HashSet<byte>();

		// Token: 0x0400004A RID: 74
		private int[] lowestSentSequenceNumber;
	}
}
