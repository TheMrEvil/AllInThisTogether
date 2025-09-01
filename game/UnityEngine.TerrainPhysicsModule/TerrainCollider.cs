using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("Modules/Terrain/Public/TerrainData.h")]
	[NativeHeader("Modules/TerrainPhysics/TerrainCollider.h")]
	public class TerrainCollider : Collider
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		// (set) Token: 0x06000002 RID: 2
		public extern TerrainData terrainData { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000003 RID: 3 RVA: 0x00002050 File Offset: 0x00000250
		private RaycastHit Raycast(Ray ray, float maxDistance, bool hitHoles, ref bool hasHit)
		{
			RaycastHit result;
			this.Raycast_Injected(ref ray, maxDistance, hitHoles, ref hasHit, out result);
			return result;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000206C File Offset: 0x0000026C
		internal bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance, bool hitHoles)
		{
			bool result = false;
			hitInfo = this.Raycast(ray, maxDistance, hitHoles, ref result);
			return result;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002093 File Offset: 0x00000293
		public TerrainCollider()
		{
		}

		// Token: 0x06000006 RID: 6
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Raycast_Injected(ref Ray ray, float maxDistance, bool hitHoles, ref bool hasHit, out RaycastHit ret);
	}
}
