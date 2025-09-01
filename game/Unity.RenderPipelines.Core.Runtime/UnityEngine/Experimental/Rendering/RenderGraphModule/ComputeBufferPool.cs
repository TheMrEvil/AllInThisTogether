using System;
using System.Collections.Generic;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x0200002F RID: 47
	internal class ComputeBufferPool : RenderGraphResourcePool<ComputeBuffer>
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x0000A81D File Offset: 0x00008A1D
		protected override void ReleaseInternalResource(ComputeBuffer res)
		{
			res.Release();
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000A825 File Offset: 0x00008A25
		protected override string GetResourceName(ComputeBuffer res)
		{
			return "ComputeBufferNameNotAvailable";
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000A82C File Offset: 0x00008A2C
		protected override long GetResourceSize(ComputeBuffer res)
		{
			return (long)(res.count * res.stride);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000A83C File Offset: 0x00008A3C
		protected override string GetResourceTypeName()
		{
			return "ComputeBuffer";
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000A843 File Offset: 0x00008A43
		protected override int GetSortIndex(ComputeBuffer res)
		{
			return res.GetHashCode();
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000A84C File Offset: 0x00008A4C
		public override void PurgeUnusedResources(int currentFrameIndex)
		{
			RenderGraphResourcePool<ComputeBuffer>.s_CurrentFrameIndex = currentFrameIndex;
			this.m_RemoveList.Clear();
			foreach (KeyValuePair<int, SortedList<int, ValueTuple<ComputeBuffer, int>>> keyValuePair in this.m_ResourcePool)
			{
				SortedList<int, ValueTuple<ComputeBuffer, int>> value = keyValuePair.Value;
				IList<int> keys = value.Keys;
				IList<ValueTuple<ComputeBuffer, int>> values = value.Values;
				for (int i = 0; i < value.Count; i++)
				{
					ValueTuple<ComputeBuffer, int> valueTuple = values[i];
					if (RenderGraphResourcePool<ComputeBuffer>.ShouldReleaseResource(valueTuple.Item2, RenderGraphResourcePool<ComputeBuffer>.s_CurrentFrameIndex))
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

		// Token: 0x060001BE RID: 446 RVA: 0x0000A960 File Offset: 0x00008B60
		public ComputeBufferPool()
		{
		}
	}
}
