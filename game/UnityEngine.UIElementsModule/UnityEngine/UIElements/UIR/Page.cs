using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000345 RID: 837
	internal class Page : IDisposable
	{
		// Token: 0x06001AF2 RID: 6898 RVA: 0x000777DA File Offset: 0x000759DA
		public Page(uint vertexMaxCount, uint indexMaxCount, uint maxQueuedFrameCount, bool mockPage)
		{
			vertexMaxCount = Math.Min(vertexMaxCount, 65536U);
			this.vertices = new Page.DataSet<Vertex>(Utility.GPUBufferType.Vertex, vertexMaxCount, maxQueuedFrameCount, 32U, mockPage);
			this.indices = new Page.DataSet<ushort>(Utility.GPUBufferType.Index, indexMaxCount, maxQueuedFrameCount, 32U, mockPage);
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x00077815 File Offset: 0x00075A15
		// (set) Token: 0x06001AF4 RID: 6900 RVA: 0x0007781D File Offset: 0x00075A1D
		private protected bool disposed
		{
			[CompilerGenerated]
			protected get
			{
				return this.<disposed>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<disposed>k__BackingField = value;
			}
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x00077826 File Offset: 0x00075A26
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x00077838 File Offset: 0x00075A38
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.indices.Dispose();
					this.vertices.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x0007787C File Offset: 0x00075A7C
		public bool isEmpty
		{
			get
			{
				return this.vertices.allocator.isEmpty && this.indices.allocator.isEmpty;
			}
		}

		// Token: 0x04000CD6 RID: 3286
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <disposed>k__BackingField;

		// Token: 0x04000CD7 RID: 3287
		public Page.DataSet<Vertex> vertices;

		// Token: 0x04000CD8 RID: 3288
		public Page.DataSet<ushort> indices;

		// Token: 0x04000CD9 RID: 3289
		public Page next;

		// Token: 0x04000CDA RID: 3290
		public int framesEmpty;

		// Token: 0x02000346 RID: 838
		public class DataSet<T> : IDisposable where T : struct
		{
			// Token: 0x06001AF8 RID: 6904 RVA: 0x000778B4 File Offset: 0x00075AB4
			public DataSet(Utility.GPUBufferType bufferType, uint totalCount, uint maxQueuedFrameCount, uint updateRangePoolSize, bool mockBuffer)
			{
				bool flag = !mockBuffer;
				if (flag)
				{
					this.gpuData = new Utility.GPUBuffer<T>((int)totalCount, bufferType);
				}
				this.cpuData = new NativeArray<T>((int)totalCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				this.allocator = new GPUBufferAllocator(totalCount);
				bool flag2 = !mockBuffer;
				if (flag2)
				{
					this.m_ElemStride = (uint)this.gpuData.ElementStride;
				}
				this.m_UpdateRangePoolSize = updateRangePoolSize;
				uint length = this.m_UpdateRangePoolSize * maxQueuedFrameCount;
				this.updateRanges = new NativeArray<GfxUpdateBufferRange>((int)length, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				this.m_UpdateRangeMin = uint.MaxValue;
				this.m_UpdateRangeMax = 0U;
				this.m_UpdateRangesEnqueued = 0U;
				this.m_UpdateRangesBatchStart = 0U;
			}

			// Token: 0x1700066E RID: 1646
			// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x0007794E File Offset: 0x00075B4E
			// (set) Token: 0x06001AFA RID: 6906 RVA: 0x00077956 File Offset: 0x00075B56
			private protected bool disposed
			{
				[CompilerGenerated]
				protected get
				{
					return this.<disposed>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<disposed>k__BackingField = value;
				}
			}

			// Token: 0x06001AFB RID: 6907 RVA: 0x0007795F File Offset: 0x00075B5F
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x06001AFC RID: 6908 RVA: 0x00077974 File Offset: 0x00075B74
			public void Dispose(bool disposing)
			{
				bool disposed = this.disposed;
				if (!disposed)
				{
					if (disposing)
					{
						Utility.GPUBuffer<T> gpubuffer = this.gpuData;
						if (gpubuffer != null)
						{
							gpubuffer.Dispose();
						}
						this.cpuData.Dispose();
						this.updateRanges.Dispose();
					}
					this.disposed = true;
				}
			}

			// Token: 0x06001AFD RID: 6909 RVA: 0x000779CC File Offset: 0x00075BCC
			public void RegisterUpdate(uint start, uint size)
			{
				Debug.Assert((ulong)(start + size) <= (ulong)((long)this.cpuData.Length));
				int num = (int)(this.m_UpdateRangesBatchStart + this.m_UpdateRangesEnqueued);
				bool flag = this.m_UpdateRangesEnqueued > 0U;
				if (flag)
				{
					int index = num - 1;
					GfxUpdateBufferRange gfxUpdateBufferRange = this.updateRanges[index];
					uint num2 = start * this.m_ElemStride;
					bool flag2 = gfxUpdateBufferRange.offsetFromWriteStart + gfxUpdateBufferRange.size == num2;
					if (flag2)
					{
						this.updateRanges[index] = new GfxUpdateBufferRange
						{
							source = gfxUpdateBufferRange.source,
							offsetFromWriteStart = gfxUpdateBufferRange.offsetFromWriteStart,
							size = gfxUpdateBufferRange.size + size * this.m_ElemStride
						};
						this.m_UpdateRangeMax = Math.Max(this.m_UpdateRangeMax, start + size);
						return;
					}
				}
				this.m_UpdateRangeMin = Math.Min(this.m_UpdateRangeMin, start);
				this.m_UpdateRangeMax = Math.Max(this.m_UpdateRangeMax, start + size);
				bool flag3 = this.m_UpdateRangesEnqueued == this.m_UpdateRangePoolSize;
				if (flag3)
				{
					this.m_UpdateRangesSaturated = true;
				}
				else
				{
					UIntPtr source = new UIntPtr(this.cpuData.Slice((int)start, (int)size).GetUnsafeReadOnlyPtr<T>());
					this.updateRanges[num] = new GfxUpdateBufferRange
					{
						source = source,
						offsetFromWriteStart = start * this.m_ElemStride,
						size = size * this.m_ElemStride
					};
					this.m_UpdateRangesEnqueued += 1U;
				}
			}

			// Token: 0x06001AFE RID: 6910 RVA: 0x00077B58 File Offset: 0x00075D58
			private bool HasMappedBufferRange()
			{
				return Utility.HasMappedBufferRange();
			}

			// Token: 0x06001AFF RID: 6911 RVA: 0x00077B70 File Offset: 0x00075D70
			public void SendUpdates()
			{
				bool flag = this.HasMappedBufferRange();
				if (flag)
				{
					this.SendPartialRanges();
				}
				else
				{
					this.SendFullRange();
				}
			}

			// Token: 0x06001B00 RID: 6912 RVA: 0x00077B98 File Offset: 0x00075D98
			public void SendFullRange()
			{
				uint num = (uint)((long)this.cpuData.Length * (long)((ulong)this.m_ElemStride));
				this.updateRanges[(int)this.m_UpdateRangesBatchStart] = new GfxUpdateBufferRange
				{
					source = new UIntPtr(this.cpuData.GetUnsafeReadOnlyPtr<T>()),
					offsetFromWriteStart = 0U,
					size = num
				};
				Utility.GPUBuffer<T> gpubuffer = this.gpuData;
				if (gpubuffer != null)
				{
					gpubuffer.UpdateRanges(this.updateRanges.Slice((int)this.m_UpdateRangesBatchStart, 1), 0, (int)num);
				}
				this.ResetUpdateState();
			}

			// Token: 0x06001B01 RID: 6913 RVA: 0x00077C2C File Offset: 0x00075E2C
			public void SendPartialRanges()
			{
				bool flag = this.m_UpdateRangesEnqueued == 0U;
				if (!flag)
				{
					bool updateRangesSaturated = this.m_UpdateRangesSaturated;
					if (updateRangesSaturated)
					{
						uint num = this.m_UpdateRangeMax - this.m_UpdateRangeMin;
						this.m_UpdateRangesEnqueued = 1U;
						this.updateRanges[(int)this.m_UpdateRangesBatchStart] = new GfxUpdateBufferRange
						{
							source = new UIntPtr(this.cpuData.Slice((int)this.m_UpdateRangeMin, (int)num).GetUnsafeReadOnlyPtr<T>()),
							offsetFromWriteStart = this.m_UpdateRangeMin * this.m_ElemStride,
							size = num * this.m_ElemStride
						};
					}
					uint num2 = this.m_UpdateRangeMin * this.m_ElemStride;
					uint rangesMax = this.m_UpdateRangeMax * this.m_ElemStride;
					bool flag2 = num2 > 0U;
					if (flag2)
					{
						for (uint num3 = 0U; num3 < this.m_UpdateRangesEnqueued; num3 += 1U)
						{
							int index = (int)(num3 + this.m_UpdateRangesBatchStart);
							this.updateRanges[index] = new GfxUpdateBufferRange
							{
								source = this.updateRanges[index].source,
								offsetFromWriteStart = this.updateRanges[index].offsetFromWriteStart - num2,
								size = this.updateRanges[index].size
							};
						}
					}
					Utility.GPUBuffer<T> gpubuffer = this.gpuData;
					if (gpubuffer != null)
					{
						gpubuffer.UpdateRanges(this.updateRanges.Slice((int)this.m_UpdateRangesBatchStart, (int)this.m_UpdateRangesEnqueued), (int)num2, (int)rangesMax);
					}
					this.ResetUpdateState();
				}
			}

			// Token: 0x06001B02 RID: 6914 RVA: 0x00077DC0 File Offset: 0x00075FC0
			private void ResetUpdateState()
			{
				this.m_UpdateRangeMin = uint.MaxValue;
				this.m_UpdateRangeMax = 0U;
				this.m_UpdateRangesEnqueued = 0U;
				this.m_UpdateRangesBatchStart += this.m_UpdateRangePoolSize;
				bool flag = (ulong)this.m_UpdateRangesBatchStart >= (ulong)((long)this.updateRanges.Length);
				if (flag)
				{
					this.m_UpdateRangesBatchStart = 0U;
				}
				this.m_UpdateRangesSaturated = false;
			}

			// Token: 0x04000CDB RID: 3291
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private bool <disposed>k__BackingField;

			// Token: 0x04000CDC RID: 3292
			public Utility.GPUBuffer<T> gpuData;

			// Token: 0x04000CDD RID: 3293
			public NativeArray<T> cpuData;

			// Token: 0x04000CDE RID: 3294
			public NativeArray<GfxUpdateBufferRange> updateRanges;

			// Token: 0x04000CDF RID: 3295
			public GPUBufferAllocator allocator;

			// Token: 0x04000CE0 RID: 3296
			private readonly uint m_UpdateRangePoolSize;

			// Token: 0x04000CE1 RID: 3297
			private uint m_ElemStride;

			// Token: 0x04000CE2 RID: 3298
			private uint m_UpdateRangeMin;

			// Token: 0x04000CE3 RID: 3299
			private uint m_UpdateRangeMax;

			// Token: 0x04000CE4 RID: 3300
			private uint m_UpdateRangesEnqueued;

			// Token: 0x04000CE5 RID: 3301
			private uint m_UpdateRangesBatchStart;

			// Token: 0x04000CE6 RID: 3302
			private bool m_UpdateRangesSaturated;
		}
	}
}
