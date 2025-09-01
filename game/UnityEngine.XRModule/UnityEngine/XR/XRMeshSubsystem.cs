using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000031 RID: 49
	[NativeConditional("ENABLE_XR")]
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshingSubsystem.h")]
	[UsedByNativeCode]
	[NativeHeader("Modules/XR/XRPrefix.h")]
	public class XRMeshSubsystem : IntegratedSubsystem<XRMeshSubsystemDescriptor>
	{
		// Token: 0x06000165 RID: 357 RVA: 0x00004CA8 File Offset: 0x00002EA8
		public bool TryGetMeshInfos(List<MeshInfo> meshInfosOut)
		{
			bool flag = meshInfosOut == null;
			if (flag)
			{
				throw new ArgumentNullException("meshInfosOut");
			}
			return this.GetMeshInfosAsList(meshInfosOut);
		}

		// Token: 0x06000166 RID: 358
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetMeshInfosAsList(List<MeshInfo> meshInfos);

		// Token: 0x06000167 RID: 359
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern MeshInfo[] GetMeshInfosAsFixedArray();

		// Token: 0x06000168 RID: 360 RVA: 0x00004CD4 File Offset: 0x00002ED4
		public void GenerateMeshAsync(MeshId meshId, Mesh mesh, MeshCollider meshCollider, MeshVertexAttributes attributes, Action<MeshGenerationResult> onMeshGenerationComplete)
		{
			this.GenerateMeshAsync(meshId, mesh, meshCollider, attributes, onMeshGenerationComplete, MeshGenerationOptions.None);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00004CE6 File Offset: 0x00002EE6
		public void GenerateMeshAsync(MeshId meshId, Mesh mesh, MeshCollider meshCollider, MeshVertexAttributes attributes, Action<MeshGenerationResult> onMeshGenerationComplete, MeshGenerationOptions options)
		{
			this.GenerateMeshAsync_Injected(ref meshId, mesh, meshCollider, attributes, onMeshGenerationComplete, options);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00004CF8 File Offset: 0x00002EF8
		[RequiredByNativeCode]
		private void InvokeMeshReadyDelegate(MeshGenerationResult result, Action<MeshGenerationResult> onMeshGenerationComplete)
		{
			bool flag = onMeshGenerationComplete != null;
			if (flag)
			{
				onMeshGenerationComplete(result);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600016B RID: 363
		// (set) Token: 0x0600016C RID: 364
		public extern float meshDensity { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600016D RID: 365 RVA: 0x00004D16 File Offset: 0x00002F16
		public bool SetBoundingVolume(Vector3 origin, Vector3 extents)
		{
			return this.SetBoundingVolume_Injected(ref origin, ref extents);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00004D24 File Offset: 0x00002F24
		public NativeArray<MeshTransform> GetUpdatedMeshTransforms(Allocator allocator)
		{
			NativeArray<MeshTransform> result;
			using (XRMeshSubsystem.MeshTransformList meshTransformList = new XRMeshSubsystem.MeshTransformList(this.GetUpdatedMeshTransforms()))
			{
				NativeArray<MeshTransform> nativeArray = new NativeArray<MeshTransform>(meshTransformList.Count, allocator, NativeArrayOptions.UninitializedMemory);
				UnsafeUtility.MemCpy(nativeArray.GetUnsafePtr<MeshTransform>(), meshTransformList.Data.ToPointer(), (long)(meshTransformList.Count * sizeof(MeshTransform)));
				result = nativeArray;
			}
			return result;
		}

		// Token: 0x0600016F RID: 367
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetUpdatedMeshTransforms();

		// Token: 0x06000170 RID: 368 RVA: 0x00004DA0 File Offset: 0x00002FA0
		public XRMeshSubsystem()
		{
		}

		// Token: 0x06000171 RID: 369
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GenerateMeshAsync_Injected(ref MeshId meshId, Mesh mesh, MeshCollider meshCollider, MeshVertexAttributes attributes, Action<MeshGenerationResult> onMeshGenerationComplete, MeshGenerationOptions options);

		// Token: 0x06000172 RID: 370
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool SetBoundingVolume_Injected(ref Vector3 origin, ref Vector3 extents);

		// Token: 0x02000032 RID: 50
		[NativeConditional("ENABLE_XR")]
		private readonly struct MeshTransformList : IDisposable
		{
			// Token: 0x06000173 RID: 371 RVA: 0x00004DA9 File Offset: 0x00002FA9
			public MeshTransformList(IntPtr self)
			{
				this.m_Self = self;
			}

			// Token: 0x17000048 RID: 72
			// (get) Token: 0x06000174 RID: 372 RVA: 0x00004DB2 File Offset: 0x00002FB2
			public int Count
			{
				get
				{
					return XRMeshSubsystem.MeshTransformList.GetLength(this.m_Self);
				}
			}

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x06000175 RID: 373 RVA: 0x00004DBF File Offset: 0x00002FBF
			public IntPtr Data
			{
				get
				{
					return XRMeshSubsystem.MeshTransformList.GetData(this.m_Self);
				}
			}

			// Token: 0x06000176 RID: 374 RVA: 0x00004DCC File Offset: 0x00002FCC
			public void Dispose()
			{
				XRMeshSubsystem.MeshTransformList.Dispose(this.m_Self);
			}

			// Token: 0x06000177 RID: 375
			[FreeFunction("UnityXRMeshTransformList_get_Length")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetLength(IntPtr self);

			// Token: 0x06000178 RID: 376
			[FreeFunction("UnityXRMeshTransformList_get_Data")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern IntPtr GetData(IntPtr self);

			// Token: 0x06000179 RID: 377
			[FreeFunction("UnityXRMeshTransformList_Dispose")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void Dispose(IntPtr self);

			// Token: 0x04000111 RID: 273
			private readonly IntPtr m_Self;
		}
	}
}
