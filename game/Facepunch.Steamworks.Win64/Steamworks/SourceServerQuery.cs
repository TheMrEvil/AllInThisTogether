using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x020000BB RID: 187
	internal static class SourceServerQuery
	{
		// Token: 0x060009E0 RID: 2528 RVA: 0x000122C4 File Offset: 0x000104C4
		internal static Task<Dictionary<string, string>> GetRules(ServerInfo server)
		{
			IPEndPoint endpoint = new IPEndPoint(server.Address, server.QueryPort);
			Dictionary<IPEndPoint, Task<Dictionary<string, string>>> pendingQueries = SourceServerQuery.PendingQueries;
			Task<Dictionary<string, string>> result;
			lock (pendingQueries)
			{
				Task<Dictionary<string, string>> task;
				bool flag2 = SourceServerQuery.PendingQueries.TryGetValue(endpoint, out task);
				if (flag2)
				{
					result = task;
				}
				else
				{
					Task<Dictionary<string, string>> task2 = SourceServerQuery.GetRulesImpl(endpoint).ContinueWith<Task<Dictionary<string, string>>>(delegate(Task<Dictionary<string, string>> t)
					{
						Dictionary<IPEndPoint, Task<Dictionary<string, string>>> pendingQueries2 = SourceServerQuery.PendingQueries;
						lock (pendingQueries2)
						{
							SourceServerQuery.PendingQueries.Remove(endpoint);
						}
						return t;
					}).Unwrap<Dictionary<string, string>>();
					SourceServerQuery.PendingQueries.Add(endpoint, task2);
					result = task2;
				}
			}
			return result;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0001237C File Offset: 0x0001057C
		private static async Task<Dictionary<string, string>> GetRulesImpl(IPEndPoint endpoint)
		{
			Dictionary<string, string> result;
			try
			{
				using (UdpClient client = new UdpClient())
				{
					client.Client.SendTimeout = 3000;
					client.Client.ReceiveTimeout = 3000;
					client.Connect(endpoint);
					Dictionary<string, string> dictionary = await SourceServerQuery.GetRules(client);
					result = dictionary;
				}
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x000123C4 File Offset: 0x000105C4
		private static async Task<Dictionary<string, string>> GetRules(UdpClient client)
		{
			byte[] array = await SourceServerQuery.GetChallengeData(client);
			byte[] challengeBytes = array;
			array = null;
			challengeBytes[0] = 86;
			await SourceServerQuery.Send(client, challengeBytes);
			byte[] array2 = await SourceServerQuery.Receive(client);
			byte[] ruleData = array2;
			array2 = null;
			Dictionary<string, string> rules = new Dictionary<string, string>();
			using (BinaryReader br = new BinaryReader(new MemoryStream(ruleData)))
			{
				if (br.ReadByte() != 69)
				{
					throw new Exception("Invalid data received in response to A2S_RULES request");
				}
				ushort numRules = br.ReadUInt16();
				for (int index = 0; index < (int)numRules; index++)
				{
					rules.Add(br.ReadNullTerminatedUTF8String(), br.ReadNullTerminatedUTF8String());
				}
			}
			BinaryReader br = null;
			return rules;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0001240C File Offset: 0x0001060C
		private static async Task<byte[]> Receive(UdpClient client)
		{
			byte[][] packets = null;
			byte packetNumber = 0;
			byte packetCount = 1;
			do
			{
				UdpReceiveResult udpReceiveResult = await client.ReceiveAsync();
				UdpReceiveResult result = udpReceiveResult;
				udpReceiveResult = default(UdpReceiveResult);
				byte[] buffer = result.Buffer;
				using (BinaryReader br = new BinaryReader(new MemoryStream(buffer)))
				{
					int header = br.ReadInt32();
					if (header == -1)
					{
						byte[] unsplitdata = new byte[(long)buffer.Length - br.BaseStream.Position];
						Buffer.BlockCopy(buffer, (int)br.BaseStream.Position, unsplitdata, 0, unsplitdata.Length);
						return unsplitdata;
					}
					if (header != -2)
					{
						throw new Exception("Invalid Header");
					}
					int requestId = br.ReadInt32();
					packetNumber = br.ReadByte();
					packetCount = br.ReadByte();
					int splitSize = br.ReadInt32();
					if (packets == null)
					{
						packets = new byte[(int)packetCount][];
					}
					byte[] data = new byte[(long)buffer.Length - br.BaseStream.Position];
					Buffer.BlockCopy(buffer, (int)br.BaseStream.Position, data, 0, data.Length);
					packets[(int)packetNumber] = data;
					data = null;
				}
				BinaryReader br = null;
				result = default(UdpReceiveResult);
				buffer = null;
			}
			while (packets.Any((byte[] p) => p == null));
			return SourceServerQuery.Combine(packets);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00012454 File Offset: 0x00010654
		private static async Task<byte[]> GetChallengeData(UdpClient client)
		{
			await SourceServerQuery.Send(client, SourceServerQuery.A2S_SERVERQUERY_GETCHALLENGE);
			byte[] array = await SourceServerQuery.Receive(client);
			byte[] challengeData = array;
			array = null;
			if (challengeData[0] != 65)
			{
				throw new Exception("Invalid Challenge");
			}
			return challengeData;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0001249C File Offset: 0x0001069C
		private static async Task Send(UdpClient client, byte[] message)
		{
			byte[] sendBuffer = new byte[message.Length + 4];
			sendBuffer[0] = byte.MaxValue;
			sendBuffer[1] = byte.MaxValue;
			sendBuffer[2] = byte.MaxValue;
			sendBuffer[3] = byte.MaxValue;
			Buffer.BlockCopy(message, 0, sendBuffer, 4, message.Length);
			await client.SendAsync(sendBuffer, message.Length + 4);
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x000124EC File Offset: 0x000106EC
		private static byte[] Combine(byte[][] arrays)
		{
			byte[] array = new byte[arrays.Sum((byte[] a) => a.Length)];
			int num = 0;
			foreach (byte[] array2 in arrays)
			{
				Buffer.BlockCopy(array2, 0, array, num, array2.Length);
				num += array2.Length;
			}
			return array;
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0001255C File Offset: 0x0001075C
		// Note: this type is marked as 'beforefieldinit'.
		static SourceServerQuery()
		{
		}

		// Token: 0x04000777 RID: 1911
		private static readonly byte[] A2S_SERVERQUERY_GETCHALLENGE = new byte[]
		{
			85,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue
		};

		// Token: 0x04000778 RID: 1912
		private const byte A2S_RULES = 86;

		// Token: 0x04000779 RID: 1913
		private static readonly Dictionary<IPEndPoint, Task<Dictionary<string, string>>> PendingQueries = new Dictionary<IPEndPoint, Task<Dictionary<string, string>>>();

		// Token: 0x0200026C RID: 620
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x060011F8 RID: 4600 RVA: 0x00021582 File Offset: 0x0001F782
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x060011F9 RID: 4601 RVA: 0x0002158C File Offset: 0x0001F78C
			internal Task<Dictionary<string, string>> <GetRules>b__0(Task<Dictionary<string, string>> t)
			{
				Dictionary<IPEndPoint, Task<Dictionary<string, string>>> pendingQueries = SourceServerQuery.PendingQueries;
				lock (pendingQueries)
				{
					SourceServerQuery.PendingQueries.Remove(this.endpoint);
				}
				return t;
			}

			// Token: 0x04000E53 RID: 3667
			public IPEndPoint endpoint;
		}

		// Token: 0x0200026D RID: 621
		[CompilerGenerated]
		private sealed class <GetRulesImpl>d__4 : IAsyncStateMachine
		{
			// Token: 0x060011FA RID: 4602 RVA: 0x000215E0 File Offset: 0x0001F7E0
			public <GetRulesImpl>d__4()
			{
			}

			// Token: 0x060011FB RID: 4603 RVA: 0x000215EC File Offset: 0x0001F7EC
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				Dictionary<string, string> result;
				try
				{
					if (num != 0)
					{
					}
					try
					{
						if (num != 0)
						{
							client = new UdpClient();
						}
						try
						{
							TaskAwaiter<Dictionary<string, string>> taskAwaiter;
							if (num != 0)
							{
								client.Client.SendTimeout = 3000;
								client.Client.ReceiveTimeout = 3000;
								client.Connect(endpoint);
								taskAwaiter = SourceServerQuery.GetRules(client).GetAwaiter();
								if (!taskAwaiter.IsCompleted)
								{
									num = (num2 = 0);
									TaskAwaiter<Dictionary<string, string>> taskAwaiter2 = taskAwaiter;
									SourceServerQuery.<GetRulesImpl>d__4 <GetRulesImpl>d__ = this;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<Dictionary<string, string>>, SourceServerQuery.<GetRulesImpl>d__4>(ref taskAwaiter, ref <GetRulesImpl>d__);
									return;
								}
							}
							else
							{
								TaskAwaiter<Dictionary<string, string>> taskAwaiter2;
								taskAwaiter = taskAwaiter2;
								taskAwaiter2 = default(TaskAwaiter<Dictionary<string, string>>);
								num = (num2 = -1);
							}
							dictionary = taskAwaiter.GetResult();
							result = dictionary;
						}
						finally
						{
							if (num < 0 && client != null)
							{
								((IDisposable)client).Dispose();
							}
						}
					}
					catch (Exception)
					{
						result = null;
					}
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060011FC RID: 4604 RVA: 0x00021750 File Offset: 0x0001F950
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000E54 RID: 3668
			public int <>1__state;

			// Token: 0x04000E55 RID: 3669
			public AsyncTaskMethodBuilder<Dictionary<string, string>> <>t__builder;

			// Token: 0x04000E56 RID: 3670
			public IPEndPoint endpoint;

			// Token: 0x04000E57 RID: 3671
			private UdpClient <client>5__1;

			// Token: 0x04000E58 RID: 3672
			private Dictionary<string, string> <>s__2;

			// Token: 0x04000E59 RID: 3673
			private TaskAwaiter<Dictionary<string, string>> <>u__1;
		}

		// Token: 0x0200026E RID: 622
		[CompilerGenerated]
		private sealed class <GetRules>d__5 : IAsyncStateMachine
		{
			// Token: 0x060011FD RID: 4605 RVA: 0x00021752 File Offset: 0x0001F952
			public <GetRules>d__5()
			{
			}

			// Token: 0x060011FE RID: 4606 RVA: 0x0002175C File Offset: 0x0001F95C
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				Dictionary<string, string> result;
				try
				{
					TaskAwaiter<byte[]> taskAwaiter;
					TaskAwaiter taskAwaiter3;
					TaskAwaiter<byte[]> taskAwaiter5;
					switch (num)
					{
					case 0:
					{
						TaskAwaiter<byte[]> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<byte[]>);
						num = (num2 = -1);
						break;
					}
					case 1:
					{
						TaskAwaiter taskAwaiter4;
						taskAwaiter3 = taskAwaiter4;
						taskAwaiter4 = default(TaskAwaiter);
						num = (num2 = -1);
						goto IL_115;
					}
					case 2:
					{
						TaskAwaiter<byte[]> taskAwaiter2;
						taskAwaiter5 = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<byte[]>);
						num = (num2 = -1);
						goto IL_17D;
					}
					default:
						taskAwaiter = SourceServerQuery.GetChallengeData(client).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num = (num2 = 0);
							TaskAwaiter<byte[]> taskAwaiter2 = taskAwaiter;
							SourceServerQuery.<GetRules>d__5 <GetRules>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<byte[]>, SourceServerQuery.<GetRules>d__5>(ref taskAwaiter, ref <GetRules>d__);
							return;
						}
						break;
					}
					array = taskAwaiter.GetResult();
					challengeBytes = array;
					array = null;
					challengeBytes[0] = 86;
					taskAwaiter3 = SourceServerQuery.Send(client, challengeBytes).GetAwaiter();
					if (!taskAwaiter3.IsCompleted)
					{
						num = (num2 = 1);
						TaskAwaiter taskAwaiter4 = taskAwaiter3;
						SourceServerQuery.<GetRules>d__5 <GetRules>d__ = this;
						this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, SourceServerQuery.<GetRules>d__5>(ref taskAwaiter3, ref <GetRules>d__);
						return;
					}
					IL_115:
					taskAwaiter3.GetResult();
					taskAwaiter5 = SourceServerQuery.Receive(client).GetAwaiter();
					if (!taskAwaiter5.IsCompleted)
					{
						num = (num2 = 2);
						TaskAwaiter<byte[]> taskAwaiter2 = taskAwaiter5;
						SourceServerQuery.<GetRules>d__5 <GetRules>d__ = this;
						this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<byte[]>, SourceServerQuery.<GetRules>d__5>(ref taskAwaiter5, ref <GetRules>d__);
						return;
					}
					IL_17D:
					array2 = taskAwaiter5.GetResult();
					ruleData = array2;
					array2 = null;
					rules = new Dictionary<string, string>();
					br = new BinaryReader(new MemoryStream(ruleData));
					try
					{
						bool flag = br.ReadByte() != 69;
						if (flag)
						{
							throw new Exception("Invalid data received in response to A2S_RULES request");
						}
						numRules = br.ReadUInt16();
						int num3;
						for (index = 0; index < (int)numRules; index = num3 + 1)
						{
							rules.Add(br.ReadNullTerminatedUTF8String(), br.ReadNullTerminatedUTF8String());
							num3 = index;
						}
					}
					finally
					{
						if (num < 0 && br != null)
						{
							((IDisposable)br).Dispose();
						}
					}
					br = null;
					result = rules;
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060011FF RID: 4607 RVA: 0x00021A40 File Offset: 0x0001FC40
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000E5A RID: 3674
			public int <>1__state;

			// Token: 0x04000E5B RID: 3675
			public AsyncTaskMethodBuilder<Dictionary<string, string>> <>t__builder;

			// Token: 0x04000E5C RID: 3676
			public UdpClient client;

			// Token: 0x04000E5D RID: 3677
			private byte[] <challengeBytes>5__1;

			// Token: 0x04000E5E RID: 3678
			private byte[] <ruleData>5__2;

			// Token: 0x04000E5F RID: 3679
			private Dictionary<string, string> <rules>5__3;

			// Token: 0x04000E60 RID: 3680
			private byte[] <>s__4;

			// Token: 0x04000E61 RID: 3681
			private byte[] <>s__5;

			// Token: 0x04000E62 RID: 3682
			private BinaryReader <br>5__6;

			// Token: 0x04000E63 RID: 3683
			private ushort <numRules>5__7;

			// Token: 0x04000E64 RID: 3684
			private int <index>5__8;

			// Token: 0x04000E65 RID: 3685
			private TaskAwaiter<byte[]> <>u__1;

			// Token: 0x04000E66 RID: 3686
			private TaskAwaiter <>u__2;
		}

		// Token: 0x0200026F RID: 623
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001200 RID: 4608 RVA: 0x00021A42 File Offset: 0x0001FC42
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001201 RID: 4609 RVA: 0x00021A4E File Offset: 0x0001FC4E
			public <>c()
			{
			}

			// Token: 0x06001202 RID: 4610 RVA: 0x00021A57 File Offset: 0x0001FC57
			internal bool <Receive>b__6_0(byte[] p)
			{
				return p == null;
			}

			// Token: 0x06001203 RID: 4611 RVA: 0x00021A5D File Offset: 0x0001FC5D
			internal int <Combine>b__9_0(byte[] a)
			{
				return a.Length;
			}

			// Token: 0x04000E67 RID: 3687
			public static readonly SourceServerQuery.<>c <>9 = new SourceServerQuery.<>c();

			// Token: 0x04000E68 RID: 3688
			public static Func<byte[], bool> <>9__6_0;

			// Token: 0x04000E69 RID: 3689
			public static Func<byte[], int> <>9__9_0;
		}

		// Token: 0x02000270 RID: 624
		[CompilerGenerated]
		private sealed class <Receive>d__6 : IAsyncStateMachine
		{
			// Token: 0x06001204 RID: 4612 RVA: 0x00021A62 File Offset: 0x0001FC62
			public <Receive>d__6()
			{
			}

			// Token: 0x06001205 RID: 4613 RVA: 0x00021A6C File Offset: 0x0001FC6C
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				byte[] result2;
				try
				{
					TaskAwaiter<UdpReceiveResult> taskAwaiter;
					if (num == 0)
					{
						TaskAwaiter<UdpReceiveResult> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<UdpReceiveResult>);
						num = (num2 = -1);
						goto IL_82;
					}
					packets = null;
					packetNumber = 0;
					packetCount = 1;
					IL_24:
					taskAwaiter = client.ReceiveAsync().GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						num = (num2 = 0);
						TaskAwaiter<UdpReceiveResult> taskAwaiter2 = taskAwaiter;
						SourceServerQuery.<Receive>d__6 <Receive>d__ = this;
						this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<UdpReceiveResult>, SourceServerQuery.<Receive>d__6>(ref taskAwaiter, ref <Receive>d__);
						return;
					}
					IL_82:
					udpReceiveResult = taskAwaiter.GetResult();
					result = udpReceiveResult;
					udpReceiveResult = default(UdpReceiveResult);
					buffer = result.Buffer;
					br = new BinaryReader(new MemoryStream(buffer));
					try
					{
						header = br.ReadInt32();
						bool flag = header == -1;
						if (flag)
						{
							unsplitdata = new byte[(long)buffer.Length - br.BaseStream.Position];
							Buffer.BlockCopy(buffer, (int)br.BaseStream.Position, unsplitdata, 0, unsplitdata.Length);
							result2 = unsplitdata;
							goto IL_2DC;
						}
						bool flag2 = header == -2;
						if (!flag2)
						{
							throw new Exception("Invalid Header");
						}
						requestId = br.ReadInt32();
						packetNumber = br.ReadByte();
						packetCount = br.ReadByte();
						splitSize = br.ReadInt32();
						bool flag3 = packets == null;
						if (flag3)
						{
							packets = new byte[(int)packetCount][];
						}
						data = new byte[(long)buffer.Length - br.BaseStream.Position];
						Buffer.BlockCopy(buffer, (int)br.BaseStream.Position, data, 0, data.Length);
						packets[(int)packetNumber] = data;
						data = null;
					}
					finally
					{
						if (num < 0 && br != null)
						{
							((IDisposable)br).Dispose();
						}
					}
					br = null;
					result = default(UdpReceiveResult);
					buffer = null;
					if (packets.Any(new Func<byte[], bool>(SourceServerQuery.<>c.<>9.<Receive>b__6_0)))
					{
						goto IL_24;
					}
					byte[] combinedData = SourceServerQuery.Combine(packets);
					result2 = combinedData;
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_2DC:
				num2 = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x06001206 RID: 4614 RVA: 0x00021DA0 File Offset: 0x0001FFA0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000E6A RID: 3690
			public int <>1__state;

			// Token: 0x04000E6B RID: 3691
			public AsyncTaskMethodBuilder<byte[]> <>t__builder;

			// Token: 0x04000E6C RID: 3692
			public UdpClient client;

			// Token: 0x04000E6D RID: 3693
			private byte[][] <packets>5__1;

			// Token: 0x04000E6E RID: 3694
			private byte <packetNumber>5__2;

			// Token: 0x04000E6F RID: 3695
			private byte <packetCount>5__3;

			// Token: 0x04000E70 RID: 3696
			private byte[] <combinedData>5__4;

			// Token: 0x04000E71 RID: 3697
			private UdpReceiveResult <result>5__5;

			// Token: 0x04000E72 RID: 3698
			private byte[] <buffer>5__6;

			// Token: 0x04000E73 RID: 3699
			private UdpReceiveResult <>s__7;

			// Token: 0x04000E74 RID: 3700
			private BinaryReader <br>5__8;

			// Token: 0x04000E75 RID: 3701
			private int <header>5__9;

			// Token: 0x04000E76 RID: 3702
			private byte[] <data>5__10;

			// Token: 0x04000E77 RID: 3703
			private byte[] <unsplitdata>5__11;

			// Token: 0x04000E78 RID: 3704
			private int <requestId>5__12;

			// Token: 0x04000E79 RID: 3705
			private int <splitSize>5__13;

			// Token: 0x04000E7A RID: 3706
			private TaskAwaiter<UdpReceiveResult> <>u__1;
		}

		// Token: 0x02000271 RID: 625
		[CompilerGenerated]
		private sealed class <GetChallengeData>d__7 : IAsyncStateMachine
		{
			// Token: 0x06001207 RID: 4615 RVA: 0x00021DA2 File Offset: 0x0001FFA2
			public <GetChallengeData>d__7()
			{
			}

			// Token: 0x06001208 RID: 4616 RVA: 0x00021DAC File Offset: 0x0001FFAC
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				byte[] result;
				try
				{
					TaskAwaiter<byte[]> taskAwaiter;
					TaskAwaiter taskAwaiter3;
					if (num != 0)
					{
						if (num == 1)
						{
							TaskAwaiter<byte[]> taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter<byte[]>);
							num2 = -1;
							goto IL_E4;
						}
						taskAwaiter3 = SourceServerQuery.Send(client, SourceServerQuery.A2S_SERVERQUERY_GETCHALLENGE).GetAwaiter();
						if (!taskAwaiter3.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter taskAwaiter4 = taskAwaiter3;
							SourceServerQuery.<GetChallengeData>d__7 <GetChallengeData>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, SourceServerQuery.<GetChallengeData>d__7>(ref taskAwaiter3, ref <GetChallengeData>d__);
							return;
						}
					}
					else
					{
						TaskAwaiter taskAwaiter4;
						taskAwaiter3 = taskAwaiter4;
						taskAwaiter4 = default(TaskAwaiter);
						num2 = -1;
					}
					taskAwaiter3.GetResult();
					taskAwaiter = SourceServerQuery.Receive(client).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						num2 = 1;
						TaskAwaiter<byte[]> taskAwaiter2 = taskAwaiter;
						SourceServerQuery.<GetChallengeData>d__7 <GetChallengeData>d__ = this;
						this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<byte[]>, SourceServerQuery.<GetChallengeData>d__7>(ref taskAwaiter, ref <GetChallengeData>d__);
						return;
					}
					IL_E4:
					array = taskAwaiter.GetResult();
					challengeData = array;
					array = null;
					bool flag = challengeData[0] != 65;
					if (flag)
					{
						throw new Exception("Invalid Challenge");
					}
					result = challengeData;
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001209 RID: 4617 RVA: 0x00021F34 File Offset: 0x00020134
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000E7B RID: 3707
			public int <>1__state;

			// Token: 0x04000E7C RID: 3708
			public AsyncTaskMethodBuilder<byte[]> <>t__builder;

			// Token: 0x04000E7D RID: 3709
			public UdpClient client;

			// Token: 0x04000E7E RID: 3710
			private byte[] <challengeData>5__1;

			// Token: 0x04000E7F RID: 3711
			private byte[] <>s__2;

			// Token: 0x04000E80 RID: 3712
			private TaskAwaiter <>u__1;

			// Token: 0x04000E81 RID: 3713
			private TaskAwaiter<byte[]> <>u__2;
		}

		// Token: 0x02000272 RID: 626
		[CompilerGenerated]
		private sealed class <Send>d__8 : IAsyncStateMachine
		{
			// Token: 0x0600120A RID: 4618 RVA: 0x00021F36 File Offset: 0x00020136
			public <Send>d__8()
			{
			}

			// Token: 0x0600120B RID: 4619 RVA: 0x00021F40 File Offset: 0x00020140
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				try
				{
					TaskAwaiter<int> taskAwaiter;
					if (num != 0)
					{
						sendBuffer = new byte[message.Length + 4];
						sendBuffer[0] = byte.MaxValue;
						sendBuffer[1] = byte.MaxValue;
						sendBuffer[2] = byte.MaxValue;
						sendBuffer[3] = byte.MaxValue;
						Buffer.BlockCopy(message, 0, sendBuffer, 4, message.Length);
						taskAwaiter = client.SendAsync(sendBuffer, message.Length + 4).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter<int> taskAwaiter2 = taskAwaiter;
							SourceServerQuery.<Send>d__8 <Send>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<int>, SourceServerQuery.<Send>d__8>(ref taskAwaiter, ref <Send>d__);
							return;
						}
					}
					else
					{
						TaskAwaiter<int> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<int>);
						num2 = -1;
					}
					taskAwaiter.GetResult();
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600120C RID: 4620 RVA: 0x00022074 File Offset: 0x00020274
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000E82 RID: 3714
			public int <>1__state;

			// Token: 0x04000E83 RID: 3715
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000E84 RID: 3716
			public UdpClient client;

			// Token: 0x04000E85 RID: 3717
			public byte[] message;

			// Token: 0x04000E86 RID: 3718
			private byte[] <sendBuffer>5__1;

			// Token: 0x04000E87 RID: 3719
			private TaskAwaiter<int> <>u__1;
		}
	}
}
