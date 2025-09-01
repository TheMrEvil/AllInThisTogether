using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using ExitGames.Client.Photon.StructWrapping;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000038 RID: 56
	public class SupportClass
	{
		// Token: 0x060002DC RID: 732 RVA: 0x00017980 File Offset: 0x00015B80
		public static List<MethodInfo> GetMethods(Type type, Type attribute)
		{
			List<MethodInfo> list = new List<MethodInfo>();
			bool flag = type == null;
			List<MethodInfo> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				foreach (MethodInfo methodInfo in methods)
				{
					bool flag2 = attribute == null || methodInfo.IsDefined(attribute, false);
					if (flag2)
					{
						list.Add(methodInfo);
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x000179F8 File Offset: 0x00015BF8
		[Obsolete("Use a Stopwatch (or equivalent) instead.")]
		public static int GetTickCount()
		{
			return SupportClass.IntegerMilliseconds();
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00017A14 File Offset: 0x00015C14
		public static byte StartBackgroundCalls(Func<bool> myThread, int millisecondsInterval = 100, string taskName = "")
		{
			object threadListLock = SupportClass.ThreadListLock;
			byte result;
			lock (threadListLock)
			{
				bool flag2 = SupportClass.threadList == null;
				if (flag2)
				{
					SupportClass.threadList = new List<Thread>();
				}
				Thread thread = new Thread(delegate()
				{
					try
					{
						while (myThread())
						{
							Thread.Sleep(millisecondsInterval);
						}
					}
					catch (ThreadAbortException)
					{
					}
				});
				bool flag3 = !string.IsNullOrEmpty(taskName);
				if (flag3)
				{
					thread.Name = taskName;
				}
				thread.IsBackground = true;
				thread.Start();
				for (int i = 0; i < SupportClass.threadList.Count; i++)
				{
					bool flag4 = SupportClass.threadList[i] == null;
					if (flag4)
					{
						SupportClass.threadList[i] = thread;
						return (byte)i;
					}
				}
				bool flag5 = SupportClass.threadList.Count >= 255;
				if (flag5)
				{
					throw new NotSupportedException("StartBackgroundCalls() can run a maximum of 255 threads.");
				}
				SupportClass.threadList.Add(thread);
				result = (byte)(SupportClass.threadList.Count - 1);
			}
			return result;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00017B48 File Offset: 0x00015D48
		public static bool StopBackgroundCalls(byte id)
		{
			object threadListLock = SupportClass.ThreadListLock;
			bool result;
			lock (threadListLock)
			{
				bool flag2 = SupportClass.threadList == null || (int)id >= SupportClass.threadList.Count || SupportClass.threadList[(int)id] == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					SupportClass.threadList[(int)id].Abort();
					SupportClass.threadList[(int)id] = null;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00017BD4 File Offset: 0x00015DD4
		public static bool StopAllBackgroundCalls()
		{
			object threadListLock = SupportClass.ThreadListLock;
			lock (threadListLock)
			{
				bool flag2 = SupportClass.threadList == null;
				if (flag2)
				{
					return false;
				}
				foreach (Thread thread in SupportClass.threadList)
				{
					bool flag3 = thread != null;
					if (flag3)
					{
						thread.Abort();
					}
				}
				SupportClass.threadList.Clear();
			}
			return true;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00017C88 File Offset: 0x00015E88
		public static void WriteStackTrace(Exception throwable, TextWriter stream)
		{
			bool flag = stream != null;
			if (flag)
			{
				stream.WriteLine(throwable.ToString());
				stream.WriteLine(throwable.StackTrace);
				stream.Flush();
			}
			else
			{
				Debug.WriteLine(throwable.ToString());
				Debug.WriteLine(throwable.StackTrace);
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00017CDD File Offset: 0x00015EDD
		public static void WriteStackTrace(Exception throwable)
		{
			SupportClass.WriteStackTrace(throwable, null);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00017CE8 File Offset: 0x00015EE8
		public static string DictionaryToString(IDictionary dictionary, bool includeTypes = true)
		{
			bool flag = dictionary == null;
			string result;
			if (flag)
			{
				result = "null";
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("{");
				foreach (object obj in dictionary.Keys)
				{
					bool flag2 = stringBuilder.Length > 1;
					if (flag2)
					{
						stringBuilder.Append(", ");
					}
					bool flag3 = dictionary[obj] == null;
					Type type;
					string text;
					if (flag3)
					{
						type = typeof(object);
						text = "null";
					}
					else
					{
						type = dictionary[obj].GetType();
						text = dictionary[obj].ToString();
					}
					bool flag4 = type == typeof(IDictionary) || type == typeof(Hashtable);
					if (flag4)
					{
						text = SupportClass.DictionaryToString((IDictionary)dictionary[obj], true);
					}
					else
					{
						bool flag5 = type == typeof(NonAllocDictionary<byte, object>);
						if (flag5)
						{
							text = SupportClass.DictionaryToString((NonAllocDictionary<byte, object>)dictionary[obj], true);
						}
						else
						{
							bool flag6 = type == typeof(string[]);
							if (flag6)
							{
								text = string.Format("{{{0}}}", string.Join(",", (string[])dictionary[obj]));
							}
							else
							{
								bool flag7 = type == typeof(byte[]);
								if (flag7)
								{
									text = string.Format("byte[{0}]", ((byte[])dictionary[obj]).Length);
								}
								else
								{
									StructWrapper structWrapper = dictionary[obj] as StructWrapper;
									bool flag8 = structWrapper != null;
									if (flag8)
									{
										stringBuilder.AppendFormat("{0}={1}", obj, structWrapper.ToString(includeTypes));
										continue;
									}
								}
							}
						}
					}
					if (includeTypes)
					{
						stringBuilder.AppendFormat("({0}){1}=({2}){3}", new object[]
						{
							obj.GetType().Name,
							obj,
							type.Name,
							text
						});
					}
					else
					{
						stringBuilder.AppendFormat("{0}={1}", obj, text);
					}
				}
				stringBuilder.Append("}");
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00017F64 File Offset: 0x00016164
		public static string DictionaryToString(NonAllocDictionary<byte, object> dictionary, bool includeTypes = true)
		{
			bool flag = dictionary == null;
			string result;
			if (flag)
			{
				result = "null";
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("{");
				foreach (byte b in dictionary.Keys)
				{
					bool flag2 = stringBuilder.Length > 1;
					if (flag2)
					{
						stringBuilder.Append(", ");
					}
					bool flag3 = dictionary[b] == null;
					Type type;
					string text;
					if (flag3)
					{
						type = typeof(object);
						text = "null";
					}
					else
					{
						type = dictionary[b].GetType();
						text = dictionary[b].ToString();
					}
					bool flag4 = type == typeof(IDictionary) || type == typeof(Hashtable);
					if (flag4)
					{
						text = SupportClass.DictionaryToString((IDictionary)dictionary[b], true);
					}
					else
					{
						bool flag5 = type == typeof(NonAllocDictionary<byte, object>);
						if (flag5)
						{
							text = SupportClass.DictionaryToString((NonAllocDictionary<byte, object>)dictionary[b], true);
						}
						else
						{
							bool flag6 = type == typeof(string[]);
							if (flag6)
							{
								text = string.Format("{{{0}}}", string.Join(",", (string[])dictionary[b]));
							}
							else
							{
								bool flag7 = type == typeof(byte[]);
								if (flag7)
								{
									text = string.Format("byte[{0}]", ((byte[])dictionary[b]).Length);
								}
								else
								{
									StructWrapper structWrapper = dictionary[b] as StructWrapper;
									bool flag8 = structWrapper != null;
									if (flag8)
									{
										stringBuilder.AppendFormat("{0}={1}", b, structWrapper.ToString(includeTypes));
										continue;
									}
								}
							}
						}
					}
					if (includeTypes)
					{
						stringBuilder.AppendFormat("({0}){1}=({2}){3}", new object[]
						{
							b.GetType().Name,
							b,
							type.Name,
							text
						});
					}
					else
					{
						stringBuilder.AppendFormat("{0}={1}", b, text);
					}
				}
				stringBuilder.Append("}");
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x000181F4 File Offset: 0x000163F4
		[Obsolete("Use DictionaryToString() instead.")]
		public static string HashtableToString(Hashtable hash)
		{
			return SupportClass.DictionaryToString(hash, true);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00018210 File Offset: 0x00016410
		public static string ByteArrayToString(byte[] list, int length = -1)
		{
			bool flag = list == null;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				bool flag2 = length < 0 || length > list.Length;
				if (flag2)
				{
					length = list.Length;
				}
				result = BitConverter.ToString(list, 0, length);
			}
			return result;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00018254 File Offset: 0x00016454
		private static uint[] InitializeTable(uint polynomial)
		{
			uint[] array = new uint[256];
			for (int i = 0; i < 256; i++)
			{
				uint num = (uint)i;
				for (int j = 0; j < 8; j++)
				{
					bool flag = (num & 1U) == 1U;
					if (flag)
					{
						num = (num >> 1 ^ polynomial);
					}
					else
					{
						num >>= 1;
					}
				}
				array[i] = num;
			}
			return array;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000182C4 File Offset: 0x000164C4
		public static uint CalculateCrc(byte[] buffer, int length)
		{
			uint num = uint.MaxValue;
			uint polynomial = 3988292384U;
			bool flag = SupportClass.crcLookupTable == null;
			if (flag)
			{
				SupportClass.crcLookupTable = SupportClass.InitializeTable(polynomial);
			}
			for (int i = 0; i < length; i++)
			{
				num = (num >> 8 ^ SupportClass.crcLookupTable[(int)((uint)buffer[i] ^ (num & 255U))]);
			}
			return num;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00018324 File Offset: 0x00016524
		public SupportClass()
		{
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0001832D File Offset: 0x0001652D
		// Note: this type is marked as 'beforefieldinit'.
		static SupportClass()
		{
		}

		// Token: 0x040001A8 RID: 424
		private static List<Thread> threadList;

		// Token: 0x040001A9 RID: 425
		private static readonly object ThreadListLock = new object();

		// Token: 0x040001AA RID: 426
		[Obsolete("Use a Stopwatch (or equivalent) instead.")]
		protected internal static SupportClass.IntegerMillisecondsDelegate IntegerMilliseconds = () => Environment.TickCount;

		// Token: 0x040001AB RID: 427
		private static uint[] crcLookupTable;

		// Token: 0x0200005B RID: 91
		// (Invoke) Token: 0x06000443 RID: 1091
		[Obsolete("Use a Stopwatch (or equivalent) instead.")]
		public delegate int IntegerMillisecondsDelegate();

		// Token: 0x0200005C RID: 92
		public class ThreadSafeRandom
		{
			// Token: 0x06000446 RID: 1094 RVA: 0x00020FE4 File Offset: 0x0001F1E4
			public static int Next()
			{
				Random r = SupportClass.ThreadSafeRandom._r;
				int result;
				lock (r)
				{
					result = SupportClass.ThreadSafeRandom._r.Next();
				}
				return result;
			}

			// Token: 0x06000447 RID: 1095 RVA: 0x00021030 File Offset: 0x0001F230
			public ThreadSafeRandom()
			{
			}

			// Token: 0x06000448 RID: 1096 RVA: 0x00021039 File Offset: 0x0001F239
			// Note: this type is marked as 'beforefieldinit'.
			static ThreadSafeRandom()
			{
			}

			// Token: 0x0400027B RID: 635
			private static readonly Random _r = new Random();
		}

		// Token: 0x0200005D RID: 93
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000449 RID: 1097 RVA: 0x00021045 File Offset: 0x0001F245
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600044A RID: 1098 RVA: 0x00021051 File Offset: 0x0001F251
			public <>c()
			{
			}

			// Token: 0x0600044B RID: 1099 RVA: 0x0002105A File Offset: 0x0001F25A
			internal int <.cctor>b__20_0()
			{
				return Environment.TickCount;
			}

			// Token: 0x0400027C RID: 636
			public static readonly SupportClass.<>c <>9 = new SupportClass.<>c();
		}

		// Token: 0x0200005E RID: 94
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_0
		{
			// Token: 0x0600044C RID: 1100 RVA: 0x00021061 File Offset: 0x0001F261
			public <>c__DisplayClass6_0()
			{
			}

			// Token: 0x0600044D RID: 1101 RVA: 0x0002106C File Offset: 0x0001F26C
			internal void <StartBackgroundCalls>b__0()
			{
				try
				{
					while (this.myThread())
					{
						Thread.Sleep(this.millisecondsInterval);
					}
				}
				catch (ThreadAbortException)
				{
				}
			}

			// Token: 0x0400027D RID: 637
			public int millisecondsInterval;

			// Token: 0x0400027E RID: 638
			public Func<bool> myThread;
		}
	}
}
