using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200039B RID: 923
	[NativeHeader("Runtime/Shaders/ComputeShader.h")]
	[NativeHeader("Runtime/Graphics/Texture.h")]
	[NativeHeader("Runtime/Graphics/AsyncGPUReadbackManaged.h")]
	[UsedByNativeCode]
	public struct AsyncGPUReadbackRequest
	{
		// Token: 0x06001F06 RID: 7942 RVA: 0x000328FF File Offset: 0x00030AFF
		public void Update()
		{
			AsyncGPUReadbackRequest.Update_Injected(ref this);
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x00032907 File Offset: 0x00030B07
		public void WaitForCompletion()
		{
			AsyncGPUReadbackRequest.WaitForCompletion_Injected(ref this);
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x00032910 File Offset: 0x00030B10
		public unsafe NativeArray<T> GetData<T>(int layer = 0) where T : struct
		{
			bool flag = !this.done || this.hasError;
			if (flag)
			{
				throw new InvalidOperationException("Cannot access the data as it is not available");
			}
			bool flag2 = layer < 0 || layer >= this.layerCount;
			if (flag2)
			{
				throw new ArgumentException(string.Format("Layer index is out of range {0} / {1}", layer, this.layerCount));
			}
			int num = UnsafeUtility.SizeOf<T>();
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)this.GetDataRaw(layer), this.layerDataSize / num, Allocator.None);
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001F09 RID: 7945 RVA: 0x000329A0 File Offset: 0x00030BA0
		public bool done
		{
			get
			{
				return this.IsDone();
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001F0A RID: 7946 RVA: 0x000329B8 File Offset: 0x00030BB8
		public bool hasError
		{
			get
			{
				return this.HasError();
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001F0B RID: 7947 RVA: 0x000329D0 File Offset: 0x00030BD0
		public int layerCount
		{
			get
			{
				return this.GetLayerCount();
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001F0C RID: 7948 RVA: 0x000329E8 File Offset: 0x00030BE8
		public int layerDataSize
		{
			get
			{
				return this.GetLayerDataSize();
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001F0D RID: 7949 RVA: 0x00032A00 File Offset: 0x00030C00
		public int width
		{
			get
			{
				return this.GetWidth();
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001F0E RID: 7950 RVA: 0x00032A18 File Offset: 0x00030C18
		public int height
		{
			get
			{
				return this.GetHeight();
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001F0F RID: 7951 RVA: 0x00032A30 File Offset: 0x00030C30
		public int depth
		{
			get
			{
				return this.GetDepth();
			}
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x00032A48 File Offset: 0x00030C48
		private bool IsDone()
		{
			return AsyncGPUReadbackRequest.IsDone_Injected(ref this);
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x00032A50 File Offset: 0x00030C50
		private bool HasError()
		{
			return AsyncGPUReadbackRequest.HasError_Injected(ref this);
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x00032A58 File Offset: 0x00030C58
		private int GetLayerCount()
		{
			return AsyncGPUReadbackRequest.GetLayerCount_Injected(ref this);
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x00032A60 File Offset: 0x00030C60
		private int GetLayerDataSize()
		{
			return AsyncGPUReadbackRequest.GetLayerDataSize_Injected(ref this);
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x00032A68 File Offset: 0x00030C68
		private int GetWidth()
		{
			return AsyncGPUReadbackRequest.GetWidth_Injected(ref this);
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x00032A70 File Offset: 0x00030C70
		private int GetHeight()
		{
			return AsyncGPUReadbackRequest.GetHeight_Injected(ref this);
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x00032A78 File Offset: 0x00030C78
		private int GetDepth()
		{
			return AsyncGPUReadbackRequest.GetDepth_Injected(ref this);
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x00032A80 File Offset: 0x00030C80
		internal void SetScriptingCallback(Action<AsyncGPUReadbackRequest> callback)
		{
			AsyncGPUReadbackRequest.SetScriptingCallback_Injected(ref this, callback);
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x00032A89 File Offset: 0x00030C89
		private IntPtr GetDataRaw(int layer)
		{
			return AsyncGPUReadbackRequest.GetDataRaw_Injected(ref this, layer);
		}

		// Token: 0x06001F19 RID: 7961
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Update_Injected(ref AsyncGPUReadbackRequest _unity_self);

		// Token: 0x06001F1A RID: 7962
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WaitForCompletion_Injected(ref AsyncGPUReadbackRequest _unity_self);

		// Token: 0x06001F1B RID: 7963
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsDone_Injected(ref AsyncGPUReadbackRequest _unity_self);

		// Token: 0x06001F1C RID: 7964
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasError_Injected(ref AsyncGPUReadbackRequest _unity_self);

		// Token: 0x06001F1D RID: 7965
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetLayerCount_Injected(ref AsyncGPUReadbackRequest _unity_self);

		// Token: 0x06001F1E RID: 7966
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetLayerDataSize_Injected(ref AsyncGPUReadbackRequest _unity_self);

		// Token: 0x06001F1F RID: 7967
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetWidth_Injected(ref AsyncGPUReadbackRequest _unity_self);

		// Token: 0x06001F20 RID: 7968
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetHeight_Injected(ref AsyncGPUReadbackRequest _unity_self);

		// Token: 0x06001F21 RID: 7969
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetDepth_Injected(ref AsyncGPUReadbackRequest _unity_self);

		// Token: 0x06001F22 RID: 7970
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetScriptingCallback_Injected(ref AsyncGPUReadbackRequest _unity_self, Action<AsyncGPUReadbackRequest> callback);

		// Token: 0x06001F23 RID: 7971
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetDataRaw_Injected(ref AsyncGPUReadbackRequest _unity_self, int layer);

		// Token: 0x04000A37 RID: 2615
		internal IntPtr m_Ptr;

		// Token: 0x04000A38 RID: 2616
		internal int m_Version;
	}
}
