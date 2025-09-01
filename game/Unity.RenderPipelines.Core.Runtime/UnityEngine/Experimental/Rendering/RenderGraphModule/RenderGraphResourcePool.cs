using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000031 RID: 49
	internal abstract class RenderGraphResourcePool<Type> : IRenderGraphResourcePool where Type : class
	{
		// Token: 0x060001C4 RID: 452
		protected abstract void ReleaseInternalResource(Type res);

		// Token: 0x060001C5 RID: 453
		protected abstract string GetResourceName(Type res);

		// Token: 0x060001C6 RID: 454
		protected abstract long GetResourceSize(Type res);

		// Token: 0x060001C7 RID: 455
		protected abstract string GetResourceTypeName();

		// Token: 0x060001C8 RID: 456
		protected abstract int GetSortIndex(Type res);

		// Token: 0x060001C9 RID: 457 RVA: 0x0000A970 File Offset: 0x00008B70
		public void ReleaseResource(int hash, Type resource, int currentFrameIndex)
		{
			SortedList<int, ValueTuple<Type, int>> sortedList;
			if (!this.m_ResourcePool.TryGetValue(hash, out sortedList))
			{
				sortedList = new SortedList<int, ValueTuple<Type, int>>();
				this.m_ResourcePool.Add(hash, sortedList);
			}
			sortedList.Add(this.GetSortIndex(resource), new ValueTuple<Type, int>(resource, currentFrameIndex));
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000A9B4 File Offset: 0x00008BB4
		public bool TryGetResource(int hashCode, out Type resource)
		{
			SortedList<int, ValueTuple<Type, int>> sortedList;
			if (this.m_ResourcePool.TryGetValue(hashCode, out sortedList) && sortedList.Count > 0)
			{
				resource = sortedList.Values[sortedList.Count - 1].Item1;
				sortedList.RemoveAt(sortedList.Count - 1);
				return true;
			}
			resource = default(Type);
			return false;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000AA10 File Offset: 0x00008C10
		public override void Cleanup()
		{
			foreach (KeyValuePair<int, SortedList<int, ValueTuple<Type, int>>> keyValuePair in this.m_ResourcePool)
			{
				foreach (KeyValuePair<int, ValueTuple<Type, int>> keyValuePair2 in keyValuePair.Value)
				{
					this.ReleaseInternalResource(keyValuePair2.Value.Item1);
				}
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000AAA4 File Offset: 0x00008CA4
		public void RegisterFrameAllocation(int hash, Type value)
		{
			if (hash != -1)
			{
				this.m_FrameAllocatedResources.Add(new ValueTuple<int, Type>(hash, value));
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000AABC File Offset: 0x00008CBC
		public void UnregisterFrameAllocation(int hash, Type value)
		{
			if (hash != -1)
			{
				this.m_FrameAllocatedResources.Remove(new ValueTuple<int, Type>(hash, value));
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000AAD8 File Offset: 0x00008CD8
		public override void CheckFrameAllocation(bool onException, int frameIndex)
		{
			if (this.m_FrameAllocatedResources.Count != 0)
			{
				string text = "";
				if (!onException)
				{
					text = "RenderGraph: Not all resources of type " + this.GetResourceTypeName() + " were released. This can be caused by a resources being allocated but never read by any pass.";
				}
				foreach (ValueTuple<int, Type> valueTuple in this.m_FrameAllocatedResources)
				{
					if (!onException)
					{
						text = text + "\n\t" + this.GetResourceName(valueTuple.Item2);
					}
					this.ReleaseResource(valueTuple.Item1, valueTuple.Item2, frameIndex);
				}
				Debug.LogWarning(text);
			}
			this.m_FrameAllocatedResources.Clear();
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000AB94 File Offset: 0x00008D94
		public override void LogResources(RenderGraphLogger logger)
		{
			List<RenderGraphResourcePool<Type>.ResourceLogInfo> list = new List<RenderGraphResourcePool<Type>.ResourceLogInfo>();
			foreach (KeyValuePair<int, SortedList<int, ValueTuple<Type, int>>> keyValuePair in this.m_ResourcePool)
			{
				foreach (KeyValuePair<int, ValueTuple<Type, int>> keyValuePair2 in keyValuePair.Value)
				{
					list.Add(new RenderGraphResourcePool<Type>.ResourceLogInfo
					{
						name = this.GetResourceName(keyValuePair2.Value.Item1),
						size = this.GetResourceSize(keyValuePair2.Value.Item1)
					});
				}
			}
			logger.LogLine("== " + this.GetResourceTypeName() + " Resources ==", Array.Empty<object>());
			list.Sort(delegate(RenderGraphResourcePool<Type>.ResourceLogInfo a, RenderGraphResourcePool<Type>.ResourceLogInfo b)
			{
				if (a.size >= b.size)
				{
					return -1;
				}
				return 1;
			});
			int num = 0;
			float num2 = 0f;
			foreach (RenderGraphResourcePool<Type>.ResourceLogInfo resourceLogInfo in list)
			{
				float num3 = (float)resourceLogInfo.size / 1048576f;
				num2 += num3;
				logger.LogLine(string.Format("[{0:D2}]\t[{1:0.00} MB]\t{2}", num++, num3, resourceLogInfo.name), Array.Empty<object>());
			}
			logger.LogLine(string.Format("\nTotal Size [{0:0.00}]", num2), Array.Empty<object>());
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000AD4C File Offset: 0x00008F4C
		protected static bool ShouldReleaseResource(int lastUsedFrameIndex, int currentFrameIndex)
		{
			return lastUsedFrameIndex + 10 < currentFrameIndex;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000AD55 File Offset: 0x00008F55
		protected RenderGraphResourcePool()
		{
		}

		// Token: 0x04000134 RID: 308
		[TupleElementNames(new string[]
		{
			"resource",
			"frameIndex"
		})]
		protected Dictionary<int, SortedList<int, ValueTuple<Type, int>>> m_ResourcePool = new Dictionary<int, SortedList<int, ValueTuple<Type, int>>>();

		// Token: 0x04000135 RID: 309
		protected List<int> m_RemoveList = new List<int>(32);

		// Token: 0x04000136 RID: 310
		private List<ValueTuple<int, Type>> m_FrameAllocatedResources = new List<ValueTuple<int, Type>>();

		// Token: 0x04000137 RID: 311
		protected static int s_CurrentFrameIndex;

		// Token: 0x04000138 RID: 312
		private const int kStaleResourceLifetime = 10;

		// Token: 0x02000134 RID: 308
		private struct ResourceLogInfo
		{
			// Token: 0x040004F2 RID: 1266
			public string name;

			// Token: 0x040004F3 RID: 1267
			public long size;
		}

		// Token: 0x02000135 RID: 309
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000829 RID: 2089 RVA: 0x00022A3B File Offset: 0x00020C3B
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600082A RID: 2090 RVA: 0x00022A47 File Offset: 0x00020C47
			public <>c()
			{
			}

			// Token: 0x0600082B RID: 2091 RVA: 0x00022A4F File Offset: 0x00020C4F
			internal int <LogResources>b__17_0(RenderGraphResourcePool<Type>.ResourceLogInfo a, RenderGraphResourcePool<Type>.ResourceLogInfo b)
			{
				if (a.size >= b.size)
				{
					return -1;
				}
				return 1;
			}

			// Token: 0x040004F4 RID: 1268
			public static readonly RenderGraphResourcePool<Type>.<>c <>9 = new RenderGraphResourcePool<Type>.<>c();

			// Token: 0x040004F5 RID: 1269
			public static Comparison<RenderGraphResourcePool<Type>.ResourceLogInfo> <>9__17_0;
		}
	}
}
