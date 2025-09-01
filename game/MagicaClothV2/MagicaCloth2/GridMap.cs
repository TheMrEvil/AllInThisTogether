using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Mathematics;

namespace MagicaCloth2
{
	// Token: 0x020000C3 RID: 195
	public class GridMap<[IsUnmanaged] T> : IDisposable where T : struct, ValueType, IEquatable<T>
	{
		// Token: 0x060002F6 RID: 758 RVA: 0x0001D364 File Offset: 0x0001B564
		public GridMap(int capacity = 0)
		{
			this.gridMap = new NativeParallelMultiHashMap<int3, T>(capacity, Allocator.Persistent);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0001D37E File Offset: 0x0001B57E
		public void Dispose()
		{
			if (this.gridMap.IsCreated)
			{
				this.gridMap.Dispose();
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0001D398 File Offset: 0x0001B598
		public NativeParallelMultiHashMap<int3, T> GetMultiHashMap()
		{
			return this.gridMap;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0001D3A0 File Offset: 0x0001B5A0
		public int DataCount
		{
			get
			{
				return this.gridMap.Count();
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0001D3B0 File Offset: 0x0001B5B0
		public static GridMap<T>.GridEnumerator GetArea(int3 startGrid, int3 endGrid, NativeParallelMultiHashMap<int3, T> gridMap)
		{
			return new GridMap<T>.GridEnumerator
			{
				gridMap = gridMap,
				startGrid = math.min(startGrid, endGrid),
				endGrid = math.max(startGrid, endGrid),
				currentGrid = math.min(startGrid, endGrid),
				isFirst = true
			};
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0001D400 File Offset: 0x0001B600
		public static GridMap<T>.GridEnumerator GetArea(float3 pos, float radius, NativeParallelMultiHashMap<int3, T> gridMap, float gridSize)
		{
			int3 grid = GridMap<T>.GetGrid(pos - radius, gridSize);
			int3 grid2 = GridMap<T>.GetGrid(pos + radius, gridSize);
			return GridMap<T>.GetArea(grid, grid2, gridMap);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0001D42F File Offset: 0x0001B62F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 GetGrid(float3 pos, float gridSize)
		{
			return new int3(math.floor(pos / gridSize));
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0001D442 File Offset: 0x0001B642
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddGrid(int3 grid, T data, NativeParallelMultiHashMap<int3, T> gridMap)
		{
			gridMap.Add(grid, data);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0001D450 File Offset: 0x0001B650
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 AddGrid(float3 pos, T data, NativeParallelMultiHashMap<int3, T> gridMap, float gridSize)
		{
			int3 grid = GridMap<T>.GetGrid(pos, gridSize);
			gridMap.Add(grid, data);
			return grid;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0001D470 File Offset: 0x0001B670
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 AddGrid(float3 pos, T data, NativeParallelMultiHashMap<int3, T>.ParallelWriter gridMap, float gridSize)
		{
			int3 grid = GridMap<T>.GetGrid(pos, gridSize);
			gridMap.Add(grid, data);
			return grid;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0001D490 File Offset: 0x0001B690
		public static bool RemoveGrid(int3 grid, T data, NativeParallelMultiHashMap<int3, T> gridMap)
		{
			T t;
			NativeParallelMultiHashMapIterator<int3> it;
			if (gridMap.ContainsKey(grid) && gridMap.TryGetFirstValue(grid, out t, out it))
			{
				while (!t.Equals(data))
				{
					if (!gridMap.TryGetNextValue(out t, ref it))
					{
						return false;
					}
				}
				gridMap.Remove(it);
				return true;
			}
			return false;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0001D4DD File Offset: 0x0001B6DD
		public static bool MoveGrid(int3 fromGrid, int3 toGrid, T data, NativeParallelMultiHashMap<int3, T> gridMap)
		{
			if (fromGrid.Equals(toGrid))
			{
				return false;
			}
			GridMap<T>.RemoveGrid(fromGrid, data, gridMap);
			GridMap<T>.AddGrid(toGrid, data, gridMap);
			return true;
		}

		// Token: 0x040005E8 RID: 1512
		private NativeParallelMultiHashMap<int3, T> gridMap;

		// Token: 0x020000C4 RID: 196
		public struct GridEnumerator : IEnumerator<int3>, IEnumerator, IDisposable
		{
			// Token: 0x1700004E RID: 78
			// (get) Token: 0x06000302 RID: 770 RVA: 0x0001D4FD File Offset: 0x0001B6FD
			public int3 Current
			{
				get
				{
					return this.currentGrid;
				}
			}

			// Token: 0x1700004F RID: 79
			// (get) Token: 0x06000303 RID: 771 RVA: 0x0001D505 File Offset: 0x0001B705
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000304 RID: 772 RVA: 0x00005305 File Offset: 0x00003505
			public void Dispose()
			{
			}

			// Token: 0x06000305 RID: 773 RVA: 0x0001D514 File Offset: 0x0001B714
			public bool MoveNext()
			{
				if (this.isFirst)
				{
					this.isFirst = false;
					return true;
				}
				this.currentGrid.x = this.currentGrid.x + 1;
				if (this.currentGrid.x > this.endGrid.x)
				{
					this.currentGrid.x = this.startGrid.x;
					this.currentGrid.y = this.currentGrid.y + 1;
					if (this.currentGrid.y > this.endGrid.y)
					{
						this.currentGrid.y = this.startGrid.y;
						this.currentGrid.z = this.currentGrid.z + 1;
						if (this.currentGrid.z > this.endGrid.z)
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x06000306 RID: 774 RVA: 0x0001D5D9 File Offset: 0x0001B7D9
			public void Reset()
			{
				this.currentGrid = this.startGrid;
				this.isFirst = true;
			}

			// Token: 0x06000307 RID: 775 RVA: 0x0001D5EE File Offset: 0x0001B7EE
			public GridMap<T>.GridEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x040005E9 RID: 1513
			internal NativeParallelMultiHashMap<int3, T> gridMap;

			// Token: 0x040005EA RID: 1514
			internal int3 startGrid;

			// Token: 0x040005EB RID: 1515
			internal int3 endGrid;

			// Token: 0x040005EC RID: 1516
			internal int3 currentGrid;

			// Token: 0x040005ED RID: 1517
			internal bool isFirst;
		}
	}
}
