using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.AI
{
	// Token: 0x02000015 RID: 21
	[NativeHeader("Modules/AI/NavMeshManager.h")]
	[NativeHeader("Modules/AI/NavMesh/NavMesh.bindings.h")]
	[StaticAccessor("NavMeshBindings", StaticAccessorType.DoubleColon)]
	[MovedFrom("UnityEngine")]
	public static class NavMesh
	{
		// Token: 0x060000ED RID: 237 RVA: 0x000029D4 File Offset: 0x00000BD4
		[RequiredByNativeCode]
		private static void Internal_CallOnNavMeshPreUpdate()
		{
			bool flag = NavMesh.onPreUpdate != null;
			if (flag)
			{
				NavMesh.onPreUpdate();
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000029F9 File Offset: 0x00000BF9
		public static bool Raycast(Vector3 sourcePosition, Vector3 targetPosition, out NavMeshHit hit, int areaMask)
		{
			return NavMesh.Raycast_Injected(ref sourcePosition, ref targetPosition, out hit, areaMask);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00002A08 File Offset: 0x00000C08
		public static bool CalculatePath(Vector3 sourcePosition, Vector3 targetPosition, int areaMask, NavMeshPath path)
		{
			path.ClearCorners();
			return NavMesh.CalculatePathInternal(sourcePosition, targetPosition, areaMask, path);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00002A2A File Offset: 0x00000C2A
		private static bool CalculatePathInternal(Vector3 sourcePosition, Vector3 targetPosition, int areaMask, NavMeshPath path)
		{
			return NavMesh.CalculatePathInternal_Injected(ref sourcePosition, ref targetPosition, areaMask, path);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00002A37 File Offset: 0x00000C37
		public static bool FindClosestEdge(Vector3 sourcePosition, out NavMeshHit hit, int areaMask)
		{
			return NavMesh.FindClosestEdge_Injected(ref sourcePosition, out hit, areaMask);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00002A42 File Offset: 0x00000C42
		public static bool SamplePosition(Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int areaMask)
		{
			return NavMesh.SamplePosition_Injected(ref sourcePosition, out hit, maxDistance, areaMask);
		}

		// Token: 0x060000F3 RID: 243
		[Obsolete("Use SetAreaCost instead.")]
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[NativeName("SetAreaCost")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetLayerCost(int layer, float cost);

		// Token: 0x060000F4 RID: 244
		[Obsolete("Use GetAreaCost instead.")]
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[NativeName("GetAreaCost")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetLayerCost(int layer);

		// Token: 0x060000F5 RID: 245
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[Obsolete("Use GetAreaFromName instead.")]
		[NativeName("GetAreaFromName")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetNavMeshLayerFromName(string layerName);

		// Token: 0x060000F6 RID: 246
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[NativeName("SetAreaCost")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetAreaCost(int areaIndex, float cost);

		// Token: 0x060000F7 RID: 247
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[NativeName("GetAreaCost")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetAreaCost(int areaIndex);

		// Token: 0x060000F8 RID: 248
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[NativeName("GetAreaFromName")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetAreaFromName(string areaName);

		// Token: 0x060000F9 RID: 249 RVA: 0x00002A50 File Offset: 0x00000C50
		public static NavMeshTriangulation CalculateTriangulation()
		{
			NavMeshTriangulation result;
			NavMesh.CalculateTriangulation_Injected(out result);
			return result;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00002A68 File Offset: 0x00000C68
		[Obsolete("use NavMesh.CalculateTriangulation() instead.")]
		public static void Triangulate(out Vector3[] vertices, out int[] indices)
		{
			NavMeshTriangulation navMeshTriangulation = NavMesh.CalculateTriangulation();
			vertices = navMeshTriangulation.vertices;
			indices = navMeshTriangulation.indices;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00002A8C File Offset: 0x00000C8C
		[Obsolete("AddOffMeshLinks has no effect and is deprecated.")]
		public static void AddOffMeshLinks()
		{
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00002A8C File Offset: 0x00000C8C
		[Obsolete("RestoreNavMesh has no effect and is deprecated.")]
		public static void RestoreNavMesh()
		{
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000FD RID: 253
		// (set) Token: 0x060000FE RID: 254
		[StaticAccessor("GetNavMeshManager()")]
		public static extern float avoidancePredictionTime { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000FF RID: 255
		// (set) Token: 0x06000100 RID: 256
		[StaticAccessor("GetNavMeshManager()")]
		public static extern int pathfindingIterationsPerFrame { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000101 RID: 257 RVA: 0x00002A90 File Offset: 0x00000C90
		public static NavMeshDataInstance AddNavMeshData(NavMeshData navMeshData)
		{
			bool flag = navMeshData == null;
			if (flag)
			{
				throw new ArgumentNullException("navMeshData");
			}
			return new NavMeshDataInstance
			{
				id = NavMesh.AddNavMeshDataInternal(navMeshData)
			};
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00002AD0 File Offset: 0x00000CD0
		public static NavMeshDataInstance AddNavMeshData(NavMeshData navMeshData, Vector3 position, Quaternion rotation)
		{
			bool flag = navMeshData == null;
			if (flag)
			{
				throw new ArgumentNullException("navMeshData");
			}
			return new NavMeshDataInstance
			{
				id = NavMesh.AddNavMeshDataTransformedInternal(navMeshData, position, rotation)
			};
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00002B11 File Offset: 0x00000D11
		public static void RemoveNavMeshData(NavMeshDataInstance handle)
		{
			NavMesh.RemoveNavMeshDataInternal(handle.id);
		}

		// Token: 0x06000104 RID: 260
		[StaticAccessor("GetNavMeshManager()")]
		[NativeName("IsValidSurfaceID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsValidNavMeshDataHandle(int handle);

		// Token: 0x06000105 RID: 261
		[StaticAccessor("GetNavMeshManager()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsValidLinkHandle(int handle);

		// Token: 0x06000106 RID: 262
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Object InternalGetOwner(int dataID);

		// Token: 0x06000107 RID: 263
		[StaticAccessor("GetNavMeshManager()")]
		[NativeName("SetSurfaceUserID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool InternalSetOwner(int dataID, int ownerID);

		// Token: 0x06000108 RID: 264
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Object InternalGetLinkOwner(int linkID);

		// Token: 0x06000109 RID: 265
		[StaticAccessor("GetNavMeshManager()")]
		[NativeName("SetLinkUserID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool InternalSetLinkOwner(int linkID, int ownerID);

		// Token: 0x0600010A RID: 266
		[StaticAccessor("GetNavMeshManager()")]
		[NativeName("LoadData")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int AddNavMeshDataInternal(NavMeshData navMeshData);

		// Token: 0x0600010B RID: 267 RVA: 0x00002B21 File Offset: 0x00000D21
		[NativeName("LoadData")]
		[StaticAccessor("GetNavMeshManager()")]
		internal static int AddNavMeshDataTransformedInternal(NavMeshData navMeshData, Vector3 position, Quaternion rotation)
		{
			return NavMesh.AddNavMeshDataTransformedInternal_Injected(navMeshData, ref position, ref rotation);
		}

		// Token: 0x0600010C RID: 268
		[NativeName("UnloadData")]
		[StaticAccessor("GetNavMeshManager()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void RemoveNavMeshDataInternal(int handle);

		// Token: 0x0600010D RID: 269 RVA: 0x00002B30 File Offset: 0x00000D30
		public static NavMeshLinkInstance AddLink(NavMeshLinkData link)
		{
			return new NavMeshLinkInstance
			{
				id = NavMesh.AddLinkInternal(link, Vector3.zero, Quaternion.identity)
			};
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00002B64 File Offset: 0x00000D64
		public static NavMeshLinkInstance AddLink(NavMeshLinkData link, Vector3 position, Quaternion rotation)
		{
			return new NavMeshLinkInstance
			{
				id = NavMesh.AddLinkInternal(link, position, rotation)
			};
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00002B8F File Offset: 0x00000D8F
		public static void RemoveLink(NavMeshLinkInstance handle)
		{
			NavMesh.RemoveLinkInternal(handle.id);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00002B9F File Offset: 0x00000D9F
		[StaticAccessor("GetNavMeshManager()")]
		[NativeName("AddLink")]
		internal static int AddLinkInternal(NavMeshLinkData link, Vector3 position, Quaternion rotation)
		{
			return NavMesh.AddLinkInternal_Injected(ref link, ref position, ref rotation);
		}

		// Token: 0x06000111 RID: 273
		[StaticAccessor("GetNavMeshManager()")]
		[NativeName("RemoveLink")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void RemoveLinkInternal(int handle);

		// Token: 0x06000112 RID: 274 RVA: 0x00002BAC File Offset: 0x00000DAC
		public static bool SamplePosition(Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, NavMeshQueryFilter filter)
		{
			return NavMesh.SamplePositionFilter(sourcePosition, out hit, maxDistance, filter.agentTypeID, filter.areaMask);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00002BD4 File Offset: 0x00000DD4
		private static bool SamplePositionFilter(Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int type, int mask)
		{
			return NavMesh.SamplePositionFilter_Injected(ref sourcePosition, out hit, maxDistance, type, mask);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00002BE4 File Offset: 0x00000DE4
		public static bool FindClosestEdge(Vector3 sourcePosition, out NavMeshHit hit, NavMeshQueryFilter filter)
		{
			return NavMesh.FindClosestEdgeFilter(sourcePosition, out hit, filter.agentTypeID, filter.areaMask);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00002C0B File Offset: 0x00000E0B
		private static bool FindClosestEdgeFilter(Vector3 sourcePosition, out NavMeshHit hit, int type, int mask)
		{
			return NavMesh.FindClosestEdgeFilter_Injected(ref sourcePosition, out hit, type, mask);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00002C18 File Offset: 0x00000E18
		public static bool Raycast(Vector3 sourcePosition, Vector3 targetPosition, out NavMeshHit hit, NavMeshQueryFilter filter)
		{
			return NavMesh.RaycastFilter(sourcePosition, targetPosition, out hit, filter.agentTypeID, filter.areaMask);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00002C40 File Offset: 0x00000E40
		private static bool RaycastFilter(Vector3 sourcePosition, Vector3 targetPosition, out NavMeshHit hit, int type, int mask)
		{
			return NavMesh.RaycastFilter_Injected(ref sourcePosition, ref targetPosition, out hit, type, mask);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00002C50 File Offset: 0x00000E50
		public static bool CalculatePath(Vector3 sourcePosition, Vector3 targetPosition, NavMeshQueryFilter filter, NavMeshPath path)
		{
			path.ClearCorners();
			return NavMesh.CalculatePathFilterInternal(sourcePosition, targetPosition, path, filter.agentTypeID, filter.areaMask, filter.costs);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00002C86 File Offset: 0x00000E86
		private static bool CalculatePathFilterInternal(Vector3 sourcePosition, Vector3 targetPosition, NavMeshPath path, int type, int mask, float[] costs)
		{
			return NavMesh.CalculatePathFilterInternal_Injected(ref sourcePosition, ref targetPosition, path, type, mask, costs);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00002C98 File Offset: 0x00000E98
		[StaticAccessor("GetNavMeshProjectSettings()")]
		public static NavMeshBuildSettings CreateSettings()
		{
			NavMeshBuildSettings result;
			NavMesh.CreateSettings_Injected(out result);
			return result;
		}

		// Token: 0x0600011B RID: 283
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RemoveSettings(int agentTypeID);

		// Token: 0x0600011C RID: 284 RVA: 0x00002CB0 File Offset: 0x00000EB0
		public static NavMeshBuildSettings GetSettingsByID(int agentTypeID)
		{
			NavMeshBuildSettings result;
			NavMesh.GetSettingsByID_Injected(agentTypeID, out result);
			return result;
		}

		// Token: 0x0600011D RID: 285
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetSettingsCount();

		// Token: 0x0600011E RID: 286 RVA: 0x00002CC8 File Offset: 0x00000EC8
		public static NavMeshBuildSettings GetSettingsByIndex(int index)
		{
			NavMeshBuildSettings result;
			NavMesh.GetSettingsByIndex_Injected(index, out result);
			return result;
		}

		// Token: 0x0600011F RID: 287
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetSettingsNameFromID(int agentTypeID);

		// Token: 0x06000120 RID: 288
		[StaticAccessor("GetNavMeshManager()")]
		[NativeName("CleanupAfterCarving")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RemoveAllNavMeshData();

		// Token: 0x06000121 RID: 289
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Raycast_Injected(ref Vector3 sourcePosition, ref Vector3 targetPosition, out NavMeshHit hit, int areaMask);

		// Token: 0x06000122 RID: 290
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CalculatePathInternal_Injected(ref Vector3 sourcePosition, ref Vector3 targetPosition, int areaMask, NavMeshPath path);

		// Token: 0x06000123 RID: 291
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool FindClosestEdge_Injected(ref Vector3 sourcePosition, out NavMeshHit hit, int areaMask);

		// Token: 0x06000124 RID: 292
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SamplePosition_Injected(ref Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int areaMask);

		// Token: 0x06000125 RID: 293
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CalculateTriangulation_Injected(out NavMeshTriangulation ret);

		// Token: 0x06000126 RID: 294
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddNavMeshDataTransformedInternal_Injected(NavMeshData navMeshData, ref Vector3 position, ref Quaternion rotation);

		// Token: 0x06000127 RID: 295
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddLinkInternal_Injected(ref NavMeshLinkData link, ref Vector3 position, ref Quaternion rotation);

		// Token: 0x06000128 RID: 296
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SamplePositionFilter_Injected(ref Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int type, int mask);

		// Token: 0x06000129 RID: 297
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool FindClosestEdgeFilter_Injected(ref Vector3 sourcePosition, out NavMeshHit hit, int type, int mask);

		// Token: 0x0600012A RID: 298
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RaycastFilter_Injected(ref Vector3 sourcePosition, ref Vector3 targetPosition, out NavMeshHit hit, int type, int mask);

		// Token: 0x0600012B RID: 299
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CalculatePathFilterInternal_Injected(ref Vector3 sourcePosition, ref Vector3 targetPosition, NavMeshPath path, int type, int mask, float[] costs);

		// Token: 0x0600012C RID: 300
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateSettings_Injected(out NavMeshBuildSettings ret);

		// Token: 0x0600012D RID: 301
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSettingsByID_Injected(int agentTypeID, out NavMeshBuildSettings ret);

		// Token: 0x0600012E RID: 302
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSettingsByIndex_Injected(int index, out NavMeshBuildSettings ret);

		// Token: 0x0400002F RID: 47
		public const int AllAreas = -1;

		// Token: 0x04000030 RID: 48
		public static NavMesh.OnNavMeshPreUpdate onPreUpdate;

		// Token: 0x02000016 RID: 22
		// (Invoke) Token: 0x06000130 RID: 304
		public delegate void OnNavMeshPreUpdate();
	}
}
