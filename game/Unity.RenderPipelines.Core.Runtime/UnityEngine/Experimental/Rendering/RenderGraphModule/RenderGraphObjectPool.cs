using System;
using System.Collections.Generic;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000028 RID: 40
	public sealed class RenderGraphObjectPool
	{
		// Token: 0x06000175 RID: 373 RVA: 0x00009FC7 File Offset: 0x000081C7
		internal RenderGraphObjectPool()
		{
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00009FF0 File Offset: 0x000081F0
		public T[] GetTempArray<T>(int size)
		{
			Stack<object> stack;
			if (!this.m_ArrayPool.TryGetValue(new ValueTuple<Type, int>(typeof(T), size), out stack))
			{
				stack = new Stack<object>();
				this.m_ArrayPool.Add(new ValueTuple<Type, int>(typeof(T), size), stack);
			}
			T[] array = (stack.Count > 0) ? ((T[])stack.Pop()) : new T[size];
			this.m_AllocatedArrays.Add(new ValueTuple<object, ValueTuple<Type, int>>(array, new ValueTuple<Type, int>(typeof(T), size)));
			return array;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000A080 File Offset: 0x00008280
		public MaterialPropertyBlock GetTempMaterialPropertyBlock()
		{
			MaterialPropertyBlock materialPropertyBlock = RenderGraphObjectPool.SharedObjectPool<MaterialPropertyBlock>.sharedPool.Get();
			materialPropertyBlock.Clear();
			this.m_AllocatedMaterialPropertyBlocks.Add(materialPropertyBlock);
			return materialPropertyBlock;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000A0AC File Offset: 0x000082AC
		internal void ReleaseAllTempAlloc()
		{
			foreach (ValueTuple<object, ValueTuple<Type, int>> valueTuple in this.m_AllocatedArrays)
			{
				Stack<object> stack;
				this.m_ArrayPool.TryGetValue(valueTuple.Item2, out stack);
				stack.Push(valueTuple.Item1);
			}
			this.m_AllocatedArrays.Clear();
			foreach (MaterialPropertyBlock value in this.m_AllocatedMaterialPropertyBlocks)
			{
				RenderGraphObjectPool.SharedObjectPool<MaterialPropertyBlock>.sharedPool.Release(value);
			}
			this.m_AllocatedMaterialPropertyBlocks.Clear();
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000A178 File Offset: 0x00008378
		internal T Get<T>() where T : new()
		{
			return RenderGraphObjectPool.SharedObjectPool<T>.sharedPool.Get();
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000A184 File Offset: 0x00008384
		internal void Release<T>(T value) where T : new()
		{
			RenderGraphObjectPool.SharedObjectPool<T>.sharedPool.Release(value);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000A191 File Offset: 0x00008391
		internal void Cleanup()
		{
			this.m_AllocatedArrays.Clear();
			this.m_AllocatedMaterialPropertyBlocks.Clear();
			this.m_ArrayPool.Clear();
			RenderGraphObjectPool.SharedObjectPoolBase.ClearAll();
		}

		// Token: 0x04000114 RID: 276
		private Dictionary<ValueTuple<Type, int>, Stack<object>> m_ArrayPool = new Dictionary<ValueTuple<Type, int>, Stack<object>>();

		// Token: 0x04000115 RID: 277
		private List<ValueTuple<object, ValueTuple<Type, int>>> m_AllocatedArrays = new List<ValueTuple<object, ValueTuple<Type, int>>>();

		// Token: 0x04000116 RID: 278
		private List<MaterialPropertyBlock> m_AllocatedMaterialPropertyBlocks = new List<MaterialPropertyBlock>();

		// Token: 0x02000132 RID: 306
		private abstract class SharedObjectPoolBase
		{
			// Token: 0x0600081E RID: 2078
			protected abstract void Clear();

			// Token: 0x0600081F RID: 2079 RVA: 0x00022944 File Offset: 0x00020B44
			public static void ClearAll()
			{
				foreach (RenderGraphObjectPool.SharedObjectPoolBase sharedObjectPoolBase in RenderGraphObjectPool.SharedObjectPoolBase.s_AllocatedPools)
				{
					sharedObjectPoolBase.Clear();
				}
			}

			// Token: 0x06000820 RID: 2080 RVA: 0x00022994 File Offset: 0x00020B94
			protected SharedObjectPoolBase()
			{
			}

			// Token: 0x06000821 RID: 2081 RVA: 0x0002299C File Offset: 0x00020B9C
			// Note: this type is marked as 'beforefieldinit'.
			static SharedObjectPoolBase()
			{
			}

			// Token: 0x040004EF RID: 1263
			protected static List<RenderGraphObjectPool.SharedObjectPoolBase> s_AllocatedPools = new List<RenderGraphObjectPool.SharedObjectPoolBase>();
		}

		// Token: 0x02000133 RID: 307
		private class SharedObjectPool<T> : RenderGraphObjectPool.SharedObjectPoolBase where T : new()
		{
			// Token: 0x06000822 RID: 2082 RVA: 0x000229A8 File Offset: 0x00020BA8
			public T Get()
			{
				if (this.m_Pool.Count != 0)
				{
					return this.m_Pool.Pop();
				}
				return Activator.CreateInstance<T>();
			}

			// Token: 0x06000823 RID: 2083 RVA: 0x000229C8 File Offset: 0x00020BC8
			public void Release(T value)
			{
				this.m_Pool.Push(value);
			}

			// Token: 0x06000824 RID: 2084 RVA: 0x000229D8 File Offset: 0x00020BD8
			private static RenderGraphObjectPool.SharedObjectPool<T> AllocatePool()
			{
				RenderGraphObjectPool.SharedObjectPool<T> sharedObjectPool = new RenderGraphObjectPool.SharedObjectPool<T>();
				RenderGraphObjectPool.SharedObjectPoolBase.s_AllocatedPools.Add(sharedObjectPool);
				return sharedObjectPool;
			}

			// Token: 0x06000825 RID: 2085 RVA: 0x000229F7 File Offset: 0x00020BF7
			protected override void Clear()
			{
				this.m_Pool.Clear();
			}

			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x06000826 RID: 2086 RVA: 0x00022A04 File Offset: 0x00020C04
			public static RenderGraphObjectPool.SharedObjectPool<T> sharedPool
			{
				get
				{
					return RenderGraphObjectPool.SharedObjectPool<T>.s_Instance.Value;
				}
			}

			// Token: 0x06000827 RID: 2087 RVA: 0x00022A10 File Offset: 0x00020C10
			public SharedObjectPool()
			{
			}

			// Token: 0x06000828 RID: 2088 RVA: 0x00022A23 File Offset: 0x00020C23
			// Note: this type is marked as 'beforefieldinit'.
			static SharedObjectPool()
			{
			}

			// Token: 0x040004F0 RID: 1264
			private Stack<T> m_Pool = new Stack<T>();

			// Token: 0x040004F1 RID: 1265
			private static readonly Lazy<RenderGraphObjectPool.SharedObjectPool<T>> s_Instance = new Lazy<RenderGraphObjectPool.SharedObjectPool<T>>(new Func<RenderGraphObjectPool.SharedObjectPool<T>>(RenderGraphObjectPool.SharedObjectPool<T>.AllocatePool));
		}
	}
}
