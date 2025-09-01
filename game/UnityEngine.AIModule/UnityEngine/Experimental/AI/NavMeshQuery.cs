using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.AI;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.AI
{
	// Token: 0x02000023 RID: 35
	[StaticAccessor("NavMeshQueryBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/AI/Public/NavMeshBindingTypes.h")]
	[NativeHeader("Modules/AI/NavMeshExperimental.bindings.h")]
	[NativeContainer]
	[NativeHeader("Runtime/Math/Matrix4x4.h")]
	public struct NavMeshQuery : IDisposable
	{
		// Token: 0x06000178 RID: 376 RVA: 0x00003223 File Offset: 0x00001423
		public NavMeshQuery(NavMeshWorld world, Allocator allocator, int pathNodePoolSize = 0)
		{
			this.m_NavMeshQuery = NavMeshQuery.Create(world, pathNodePoolSize);
			UnsafeUtility.LeakRecord(this.m_NavMeshQuery, LeakCategory.NavMeshQuery, 0);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00003241 File Offset: 0x00001441
		public void Dispose()
		{
			UnsafeUtility.LeakErase(this.m_NavMeshQuery, LeakCategory.NavMeshQuery);
			NavMeshQuery.Destroy(this.m_NavMeshQuery);
			this.m_NavMeshQuery = IntPtr.Zero;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00003268 File Offset: 0x00001468
		private static IntPtr Create(NavMeshWorld world, int nodePoolSize)
		{
			return NavMeshQuery.Create_Injected(ref world, nodePoolSize);
		}

		// Token: 0x0600017B RID: 379
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy(IntPtr navMeshQuery);

		// Token: 0x0600017C RID: 380 RVA: 0x00003274 File Offset: 0x00001474
		public unsafe PathQueryStatus BeginFindPath(NavMeshLocation start, NavMeshLocation end, int areaMask = -1, NativeArray<float> costs = default(NativeArray<float>))
		{
			void* costs2 = (costs.Length > 0) ? costs.GetUnsafePtr<float>() : null;
			return NavMeshQuery.BeginFindPath(this.m_NavMeshQuery, start, end, areaMask, costs2);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000032AC File Offset: 0x000014AC
		public PathQueryStatus UpdateFindPath(int iterations, out int iterationsPerformed)
		{
			return NavMeshQuery.UpdateFindPath(this.m_NavMeshQuery, iterations, out iterationsPerformed);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000032CC File Offset: 0x000014CC
		public PathQueryStatus EndFindPath(out int pathSize)
		{
			return NavMeshQuery.EndFindPath(this.m_NavMeshQuery, out pathSize);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000032EC File Offset: 0x000014EC
		public int GetPathResult(NativeSlice<PolygonId> path)
		{
			return NavMeshQuery.GetPathResult(this.m_NavMeshQuery, path.GetUnsafePtr<PolygonId>(), path.Length);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00003316 File Offset: 0x00001516
		[ThreadSafe]
		private unsafe static PathQueryStatus BeginFindPath(IntPtr navMeshQuery, NavMeshLocation start, NavMeshLocation end, int areaMask, void* costs)
		{
			return NavMeshQuery.BeginFindPath_Injected(navMeshQuery, ref start, ref end, areaMask, costs);
		}

		// Token: 0x06000181 RID: 385
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PathQueryStatus UpdateFindPath(IntPtr navMeshQuery, int iterations, out int iterationsPerformed);

		// Token: 0x06000182 RID: 386
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PathQueryStatus EndFindPath(IntPtr navMeshQuery, out int pathSize);

		// Token: 0x06000183 RID: 387
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern int GetPathResult(IntPtr navMeshQuery, void* path, int maxPath);

		// Token: 0x06000184 RID: 388 RVA: 0x00003325 File Offset: 0x00001525
		[ThreadSafe]
		private static bool IsValidPolygon(IntPtr navMeshQuery, PolygonId polygon)
		{
			return NavMeshQuery.IsValidPolygon_Injected(navMeshQuery, ref polygon);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00003330 File Offset: 0x00001530
		public bool IsValid(PolygonId polygon)
		{
			return polygon.polyRef != 0UL && NavMeshQuery.IsValidPolygon(this.m_NavMeshQuery, polygon);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000335C File Offset: 0x0000155C
		public bool IsValid(NavMeshLocation location)
		{
			return this.IsValid(location.polygon);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000337B File Offset: 0x0000157B
		[ThreadSafe]
		private static int GetAgentTypeIdForPolygon(IntPtr navMeshQuery, PolygonId polygon)
		{
			return NavMeshQuery.GetAgentTypeIdForPolygon_Injected(navMeshQuery, ref polygon);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00003388 File Offset: 0x00001588
		public int GetAgentTypeIdForPolygon(PolygonId polygon)
		{
			return NavMeshQuery.GetAgentTypeIdForPolygon(this.m_NavMeshQuery, polygon);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000033A6 File Offset: 0x000015A6
		[ThreadSafe]
		private static bool IsPositionInPolygon(IntPtr navMeshQuery, Vector3 position, PolygonId polygon)
		{
			return NavMeshQuery.IsPositionInPolygon_Injected(navMeshQuery, ref position, ref polygon);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000033B2 File Offset: 0x000015B2
		[ThreadSafe]
		private static PathQueryStatus GetClosestPointOnPoly(IntPtr navMeshQuery, PolygonId polygon, Vector3 position, out Vector3 nearest)
		{
			return NavMeshQuery.GetClosestPointOnPoly_Injected(navMeshQuery, ref polygon, ref position, out nearest);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000033C0 File Offset: 0x000015C0
		public NavMeshLocation CreateLocation(Vector3 position, PolygonId polygon)
		{
			Vector3 position2;
			PathQueryStatus closestPointOnPoly = NavMeshQuery.GetClosestPointOnPoly(this.m_NavMeshQuery, polygon, position, out position2);
			return ((closestPointOnPoly & PathQueryStatus.Success) != (PathQueryStatus)0) ? new NavMeshLocation(position2, polygon) : default(NavMeshLocation);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00003400 File Offset: 0x00001600
		[ThreadSafe]
		private static NavMeshLocation MapLocation(IntPtr navMeshQuery, Vector3 position, Vector3 extents, int agentTypeID, int areaMask = -1)
		{
			NavMeshLocation result;
			NavMeshQuery.MapLocation_Injected(navMeshQuery, ref position, ref extents, agentTypeID, areaMask, out result);
			return result;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00003420 File Offset: 0x00001620
		public NavMeshLocation MapLocation(Vector3 position, Vector3 extents, int agentTypeID, int areaMask = -1)
		{
			return NavMeshQuery.MapLocation(this.m_NavMeshQuery, position, extents, agentTypeID, areaMask);
		}

		// Token: 0x0600018E RID: 398
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void MoveLocations(IntPtr navMeshQuery, void* locations, void* targets, void* areaMasks, int count);

		// Token: 0x0600018F RID: 399 RVA: 0x00003442 File Offset: 0x00001642
		public void MoveLocations(NativeSlice<NavMeshLocation> locations, NativeSlice<Vector3> targets, NativeSlice<int> areaMasks)
		{
			NavMeshQuery.MoveLocations(this.m_NavMeshQuery, locations.GetUnsafePtr<NavMeshLocation>(), targets.GetUnsafeReadOnlyPtr<Vector3>(), areaMasks.GetUnsafeReadOnlyPtr<int>(), locations.Length);
		}

		// Token: 0x06000190 RID: 400
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void MoveLocationsInSameAreas(IntPtr navMeshQuery, void* locations, void* targets, int count, int areaMask);

		// Token: 0x06000191 RID: 401 RVA: 0x0000346A File Offset: 0x0000166A
		public void MoveLocationsInSameAreas(NativeSlice<NavMeshLocation> locations, NativeSlice<Vector3> targets, int areaMask = -1)
		{
			NavMeshQuery.MoveLocationsInSameAreas(this.m_NavMeshQuery, locations.GetUnsafePtr<NavMeshLocation>(), targets.GetUnsafeReadOnlyPtr<Vector3>(), locations.Length, areaMask);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00003490 File Offset: 0x00001690
		[ThreadSafe]
		private static NavMeshLocation MoveLocation(IntPtr navMeshQuery, NavMeshLocation location, Vector3 target, int areaMask)
		{
			NavMeshLocation result;
			NavMeshQuery.MoveLocation_Injected(navMeshQuery, ref location, ref target, areaMask, out result);
			return result;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000034AC File Offset: 0x000016AC
		public NavMeshLocation MoveLocation(NavMeshLocation location, Vector3 target, int areaMask = -1)
		{
			return NavMeshQuery.MoveLocation(this.m_NavMeshQuery, location, target, areaMask);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000034CC File Offset: 0x000016CC
		[ThreadSafe]
		private static bool GetPortalPoints(IntPtr navMeshQuery, PolygonId polygon, PolygonId neighbourPolygon, out Vector3 left, out Vector3 right)
		{
			return NavMeshQuery.GetPortalPoints_Injected(navMeshQuery, ref polygon, ref neighbourPolygon, out left, out right);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000034DC File Offset: 0x000016DC
		public bool GetPortalPoints(PolygonId polygon, PolygonId neighbourPolygon, out Vector3 left, out Vector3 right)
		{
			return NavMeshQuery.GetPortalPoints(this.m_NavMeshQuery, polygon, neighbourPolygon, out left, out right);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00003500 File Offset: 0x00001700
		[ThreadSafe]
		private static Matrix4x4 PolygonLocalToWorldMatrix(IntPtr navMeshQuery, PolygonId polygon)
		{
			Matrix4x4 result;
			NavMeshQuery.PolygonLocalToWorldMatrix_Injected(navMeshQuery, ref polygon, out result);
			return result;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00003518 File Offset: 0x00001718
		public Matrix4x4 PolygonLocalToWorldMatrix(PolygonId polygon)
		{
			return NavMeshQuery.PolygonLocalToWorldMatrix(this.m_NavMeshQuery, polygon);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00003538 File Offset: 0x00001738
		[ThreadSafe]
		private static Matrix4x4 PolygonWorldToLocalMatrix(IntPtr navMeshQuery, PolygonId polygon)
		{
			Matrix4x4 result;
			NavMeshQuery.PolygonWorldToLocalMatrix_Injected(navMeshQuery, ref polygon, out result);
			return result;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00003550 File Offset: 0x00001750
		public Matrix4x4 PolygonWorldToLocalMatrix(PolygonId polygon)
		{
			return NavMeshQuery.PolygonWorldToLocalMatrix(this.m_NavMeshQuery, polygon);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000356E File Offset: 0x0000176E
		[ThreadSafe]
		private static NavMeshPolyTypes GetPolygonType(IntPtr navMeshQuery, PolygonId polygon)
		{
			return NavMeshQuery.GetPolygonType_Injected(navMeshQuery, ref polygon);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00003578 File Offset: 0x00001778
		public NavMeshPolyTypes GetPolygonType(PolygonId polygon)
		{
			return NavMeshQuery.GetPolygonType(this.m_NavMeshQuery, polygon);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00003598 File Offset: 0x00001798
		[ThreadSafe]
		private unsafe static PathQueryStatus Raycast(IntPtr navMeshQuery, NavMeshLocation start, Vector3 targetPosition, int areaMask, void* costs, out NavMeshHit hit, void* path, out int pathCount, int maxPath)
		{
			return NavMeshQuery.Raycast_Injected(navMeshQuery, ref start, ref targetPosition, areaMask, costs, out hit, path, out pathCount, maxPath);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000035BC File Offset: 0x000017BC
		public unsafe PathQueryStatus Raycast(out NavMeshHit hit, NavMeshLocation start, Vector3 targetPosition, int areaMask = -1, NativeArray<float> costs = default(NativeArray<float>))
		{
			void* costs2 = (costs.Length == 32) ? costs.GetUnsafePtr<float>() : null;
			int num;
			PathQueryStatus pathQueryStatus = NavMeshQuery.Raycast(this.m_NavMeshQuery, start, targetPosition, areaMask, costs2, out hit, null, out num, 0);
			return pathQueryStatus & ~PathQueryStatus.BufferTooSmall;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00003604 File Offset: 0x00001804
		public unsafe PathQueryStatus Raycast(out NavMeshHit hit, NativeSlice<PolygonId> path, out int pathCount, NavMeshLocation start, Vector3 targetPosition, int areaMask = -1, NativeArray<float> costs = default(NativeArray<float>))
		{
			void* costs2 = (costs.Length == 32) ? costs.GetUnsafePtr<float>() : null;
			void* ptr = (path.Length > 0) ? path.GetUnsafePtr<PolygonId>() : null;
			int maxPath = (ptr != null) ? path.Length : 0;
			return NavMeshQuery.Raycast(this.m_NavMeshQuery, start, targetPosition, areaMask, costs2, out hit, ptr, out pathCount, maxPath);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000366C File Offset: 0x0000186C
		[ThreadSafe]
		private unsafe static PathQueryStatus GetEdgesAndNeighbors(IntPtr navMeshQuery, PolygonId node, int maxVerts, int maxNei, void* verts, void* neighbors, void* edgeIndices, out int vertCount, out int neighborsCount)
		{
			return NavMeshQuery.GetEdgesAndNeighbors_Injected(navMeshQuery, ref node, maxVerts, maxNei, verts, neighbors, edgeIndices, out vertCount, out neighborsCount);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00003690 File Offset: 0x00001890
		public unsafe PathQueryStatus GetEdgesAndNeighbors(PolygonId node, NativeSlice<Vector3> edgeVertices, NativeSlice<PolygonId> neighbors, NativeSlice<byte> edgeIndices, out int verticesCount, out int neighborsCount)
		{
			void* verts = (edgeVertices.Length > 0) ? edgeVertices.GetUnsafePtr<Vector3>() : null;
			void* neighbors2 = (neighbors.Length > 0) ? neighbors.GetUnsafePtr<PolygonId>() : null;
			void* edgeIndices2 = (edgeIndices.Length > 0) ? edgeIndices.GetUnsafePtr<byte>() : null;
			int length = edgeVertices.Length;
			int maxNei = (neighbors.Length > 0) ? neighbors.Length : edgeIndices.Length;
			return NavMeshQuery.GetEdgesAndNeighbors(this.m_NavMeshQuery, node, length, maxNei, verts, neighbors2, edgeIndices2, out verticesCount, out neighborsCount);
		}

		// Token: 0x060001A1 RID: 417
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create_Injected(ref NavMeshWorld world, int nodePoolSize);

		// Token: 0x060001A2 RID: 418
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern PathQueryStatus BeginFindPath_Injected(IntPtr navMeshQuery, ref NavMeshLocation start, ref NavMeshLocation end, int areaMask, void* costs);

		// Token: 0x060001A3 RID: 419
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsValidPolygon_Injected(IntPtr navMeshQuery, ref PolygonId polygon);

		// Token: 0x060001A4 RID: 420
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetAgentTypeIdForPolygon_Injected(IntPtr navMeshQuery, ref PolygonId polygon);

		// Token: 0x060001A5 RID: 421
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsPositionInPolygon_Injected(IntPtr navMeshQuery, ref Vector3 position, ref PolygonId polygon);

		// Token: 0x060001A6 RID: 422
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PathQueryStatus GetClosestPointOnPoly_Injected(IntPtr navMeshQuery, ref PolygonId polygon, ref Vector3 position, out Vector3 nearest);

		// Token: 0x060001A7 RID: 423
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MapLocation_Injected(IntPtr navMeshQuery, ref Vector3 position, ref Vector3 extents, int agentTypeID, int areaMask = -1, out NavMeshLocation ret);

		// Token: 0x060001A8 RID: 424
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MoveLocation_Injected(IntPtr navMeshQuery, ref NavMeshLocation location, ref Vector3 target, int areaMask, out NavMeshLocation ret);

		// Token: 0x060001A9 RID: 425
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetPortalPoints_Injected(IntPtr navMeshQuery, ref PolygonId polygon, ref PolygonId neighbourPolygon, out Vector3 left, out Vector3 right);

		// Token: 0x060001AA RID: 426
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PolygonLocalToWorldMatrix_Injected(IntPtr navMeshQuery, ref PolygonId polygon, out Matrix4x4 ret);

		// Token: 0x060001AB RID: 427
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PolygonWorldToLocalMatrix_Injected(IntPtr navMeshQuery, ref PolygonId polygon, out Matrix4x4 ret);

		// Token: 0x060001AC RID: 428
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern NavMeshPolyTypes GetPolygonType_Injected(IntPtr navMeshQuery, ref PolygonId polygon);

		// Token: 0x060001AD RID: 429
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern PathQueryStatus Raycast_Injected(IntPtr navMeshQuery, ref NavMeshLocation start, ref Vector3 targetPosition, int areaMask, void* costs, out NavMeshHit hit, void* path, out int pathCount, int maxPath);

		// Token: 0x060001AE RID: 430
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern PathQueryStatus GetEdgesAndNeighbors_Injected(IntPtr navMeshQuery, ref PolygonId node, int maxVerts, int maxNei, void* verts, void* neighbors, void* edgeIndices, out int vertCount, out int neighborsCount);

		// Token: 0x04000073 RID: 115
		[NativeDisableUnsafePtrRestriction]
		internal IntPtr m_NavMeshQuery;
	}
}
