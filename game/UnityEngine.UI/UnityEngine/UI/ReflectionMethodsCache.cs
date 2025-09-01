using System;
using System.Reflection;

namespace UnityEngine.UI
{
	// Token: 0x0200003C RID: 60
	internal class ReflectionMethodsCache
	{
		// Token: 0x0600048B RID: 1163 RVA: 0x00015C50 File Offset: 0x00013E50
		public ReflectionMethodsCache()
		{
			MethodInfo method = typeof(Physics).GetMethod("Raycast", new Type[]
			{
				typeof(Ray),
				typeof(RaycastHit).MakeByRefType(),
				typeof(float),
				typeof(int)
			});
			if (method != null)
			{
				this.raycast3D = (ReflectionMethodsCache.Raycast3DCallback)Delegate.CreateDelegate(typeof(ReflectionMethodsCache.Raycast3DCallback), method);
			}
			MethodInfo method2 = typeof(Physics).GetMethod("RaycastAll", new Type[]
			{
				typeof(Ray),
				typeof(float),
				typeof(int)
			});
			if (method2 != null)
			{
				this.raycast3DAll = (ReflectionMethodsCache.RaycastAllCallback)Delegate.CreateDelegate(typeof(ReflectionMethodsCache.RaycastAllCallback), method2);
			}
			MethodInfo method3 = typeof(Physics).GetMethod("RaycastNonAlloc", new Type[]
			{
				typeof(Ray),
				typeof(RaycastHit[]),
				typeof(float),
				typeof(int)
			});
			if (method3 != null)
			{
				this.getRaycastNonAlloc = (ReflectionMethodsCache.GetRaycastNonAllocCallback)Delegate.CreateDelegate(typeof(ReflectionMethodsCache.GetRaycastNonAllocCallback), method3);
			}
			MethodInfo method4 = typeof(Physics2D).GetMethod("Raycast", new Type[]
			{
				typeof(Vector2),
				typeof(Vector2),
				typeof(float),
				typeof(int)
			});
			if (method4 != null)
			{
				this.raycast2D = (ReflectionMethodsCache.Raycast2DCallback)Delegate.CreateDelegate(typeof(ReflectionMethodsCache.Raycast2DCallback), method4);
			}
			MethodInfo method5 = typeof(Physics2D).GetMethod("GetRayIntersectionAll", new Type[]
			{
				typeof(Ray),
				typeof(float),
				typeof(int)
			});
			if (method5 != null)
			{
				this.getRayIntersectionAll = (ReflectionMethodsCache.GetRayIntersectionAllCallback)Delegate.CreateDelegate(typeof(ReflectionMethodsCache.GetRayIntersectionAllCallback), method5);
			}
			MethodInfo method6 = typeof(Physics2D).GetMethod("GetRayIntersectionNonAlloc", new Type[]
			{
				typeof(Ray),
				typeof(RaycastHit2D[]),
				typeof(float),
				typeof(int)
			});
			if (method6 != null)
			{
				this.getRayIntersectionAllNonAlloc = (ReflectionMethodsCache.GetRayIntersectionAllNonAllocCallback)Delegate.CreateDelegate(typeof(ReflectionMethodsCache.GetRayIntersectionAllNonAllocCallback), method6);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x00015F06 File Offset: 0x00014106
		public static ReflectionMethodsCache Singleton
		{
			get
			{
				if (ReflectionMethodsCache.s_ReflectionMethodsCache == null)
				{
					ReflectionMethodsCache.s_ReflectionMethodsCache = new ReflectionMethodsCache();
				}
				return ReflectionMethodsCache.s_ReflectionMethodsCache;
			}
		}

		// Token: 0x0400017D RID: 381
		public ReflectionMethodsCache.Raycast3DCallback raycast3D;

		// Token: 0x0400017E RID: 382
		public ReflectionMethodsCache.RaycastAllCallback raycast3DAll;

		// Token: 0x0400017F RID: 383
		public ReflectionMethodsCache.GetRaycastNonAllocCallback getRaycastNonAlloc;

		// Token: 0x04000180 RID: 384
		public ReflectionMethodsCache.Raycast2DCallback raycast2D;

		// Token: 0x04000181 RID: 385
		public ReflectionMethodsCache.GetRayIntersectionAllCallback getRayIntersectionAll;

		// Token: 0x04000182 RID: 386
		public ReflectionMethodsCache.GetRayIntersectionAllNonAllocCallback getRayIntersectionAllNonAlloc;

		// Token: 0x04000183 RID: 387
		private static ReflectionMethodsCache s_ReflectionMethodsCache;

		// Token: 0x020000B2 RID: 178
		// (Invoke) Token: 0x060006F5 RID: 1781
		public delegate bool Raycast3DCallback(Ray r, out RaycastHit hit, float f, int i);

		// Token: 0x020000B3 RID: 179
		// (Invoke) Token: 0x060006F9 RID: 1785
		public delegate RaycastHit[] RaycastAllCallback(Ray r, float f, int i);

		// Token: 0x020000B4 RID: 180
		// (Invoke) Token: 0x060006FD RID: 1789
		public delegate int GetRaycastNonAllocCallback(Ray r, RaycastHit[] results, float f, int i);

		// Token: 0x020000B5 RID: 181
		// (Invoke) Token: 0x06000701 RID: 1793
		public delegate RaycastHit2D Raycast2DCallback(Vector2 p1, Vector2 p2, float f, int i);

		// Token: 0x020000B6 RID: 182
		// (Invoke) Token: 0x06000705 RID: 1797
		public delegate RaycastHit2D[] GetRayIntersectionAllCallback(Ray r, float f, int i);

		// Token: 0x020000B7 RID: 183
		// (Invoke) Token: 0x06000709 RID: 1801
		public delegate int GetRayIntersectionAllNonAllocCallback(Ray r, RaycastHit2D[] results, float f, int i);
	}
}
