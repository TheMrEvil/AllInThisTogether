using System;
using System.Collections.Generic;
using UnityEngine.Profiling;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x0200003E RID: 62
	internal class TexturePool : RenderGraphResourcePool<RTHandle>
	{
		// Token: 0x0600023E RID: 574 RVA: 0x0000C498 File Offset: 0x0000A698
		protected override void ReleaseInternalResource(RTHandle res)
		{
			res.Release();
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000C4A0 File Offset: 0x0000A6A0
		protected override string GetResourceName(RTHandle res)
		{
			return res.rt.name;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000C4AD File Offset: 0x0000A6AD
		protected override long GetResourceSize(RTHandle res)
		{
			return Profiler.GetRuntimeMemorySizeLong(res.rt);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000C4BA File Offset: 0x0000A6BA
		protected override string GetResourceTypeName()
		{
			return "Texture";
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000C4C1 File Offset: 0x0000A6C1
		protected override int GetSortIndex(RTHandle res)
		{
			return res.GetInstanceID();
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000C4CC File Offset: 0x0000A6CC
		public override void PurgeUnusedResources(int currentFrameIndex)
		{
			RenderGraphResourcePool<RTHandle>.s_CurrentFrameIndex = currentFrameIndex;
			this.m_RemoveList.Clear();
			foreach (KeyValuePair<int, SortedList<int, ValueTuple<RTHandle, int>>> keyValuePair in this.m_ResourcePool)
			{
				SortedList<int, ValueTuple<RTHandle, int>> value = keyValuePair.Value;
				IList<int> keys = value.Keys;
				IList<ValueTuple<RTHandle, int>> values = value.Values;
				for (int i = 0; i < value.Count; i++)
				{
					ValueTuple<RTHandle, int> valueTuple = values[i];
					if (RenderGraphResourcePool<RTHandle>.ShouldReleaseResource(valueTuple.Item2, RenderGraphResourcePool<RTHandle>.s_CurrentFrameIndex))
					{
						valueTuple.Item1.Release();
						this.m_RemoveList.Add(keys[i]);
					}
				}
				foreach (int key in this.m_RemoveList)
				{
					value.Remove(key);
				}
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000C5E0 File Offset: 0x0000A7E0
		public TexturePool()
		{
		}
	}
}
