using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x0200039D RID: 925
	[StaticAccessor("AsyncGPUReadbackManager::GetInstance()", StaticAccessorType.Dot)]
	public static class AsyncGPUReadback
	{
		// Token: 0x06001F26 RID: 7974 RVA: 0x00032B38 File Offset: 0x00030D38
		internal static void ValidateFormat(Texture src, GraphicsFormat dstformat)
		{
			GraphicsFormat format = GraphicsFormatUtility.GetFormat(src);
			bool flag = !SystemInfo.IsFormatSupported(format, FormatUsage.ReadPixels);
			if (flag)
			{
				Debug.LogError(string.Format("'{0}' doesn't support ReadPixels usage on this platform. Async GPU readback failed.", format));
			}
		}

		// Token: 0x06001F27 RID: 7975
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void WaitAllRequests();

		// Token: 0x06001F28 RID: 7976 RVA: 0x00032B74 File Offset: 0x00030D74
		public static AsyncGPUReadbackRequest Request(ComputeBuffer src, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadbackRequest result = AsyncGPUReadback.Request_Internal_ComputeBuffer_1(src, null);
			result.SetScriptingCallback(callback);
			return result;
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x00032B9C File Offset: 0x00030D9C
		public static AsyncGPUReadbackRequest Request(ComputeBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadbackRequest result = AsyncGPUReadback.Request_Internal_ComputeBuffer_2(src, size, offset, null);
			result.SetScriptingCallback(callback);
			return result;
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x00032BC4 File Offset: 0x00030DC4
		public static AsyncGPUReadbackRequest Request(GraphicsBuffer src, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadbackRequest result = AsyncGPUReadback.Request_Internal_GraphicsBuffer_1(src, null);
			result.SetScriptingCallback(callback);
			return result;
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x00032BEC File Offset: 0x00030DEC
		public static AsyncGPUReadbackRequest Request(GraphicsBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadbackRequest result = AsyncGPUReadback.Request_Internal_GraphicsBuffer_2(src, size, offset, null);
			result.SetScriptingCallback(callback);
			return result;
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x00032C14 File Offset: 0x00030E14
		public static AsyncGPUReadbackRequest Request(Texture src, int mipIndex = 0, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadbackRequest result = AsyncGPUReadback.Request_Internal_Texture_1(src, mipIndex, null);
			result.SetScriptingCallback(callback);
			return result;
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x00032C3C File Offset: 0x00030E3C
		public static AsyncGPUReadbackRequest Request(Texture src, int mipIndex, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null)
		{
			return AsyncGPUReadback.Request(src, mipIndex, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback);
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x00032C64 File Offset: 0x00030E64
		public static AsyncGPUReadbackRequest Request(Texture src, int mipIndex, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadback.ValidateFormat(src, dstFormat);
			AsyncGPUReadbackRequest result = AsyncGPUReadback.Request_Internal_Texture_2(src, mipIndex, dstFormat, null);
			result.SetScriptingCallback(callback);
			return result;
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x00032C94 File Offset: 0x00030E94
		public static AsyncGPUReadbackRequest Request(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadbackRequest result = AsyncGPUReadback.Request_Internal_Texture_3(src, mipIndex, x, width, y, height, z, depth, null);
			result.SetScriptingCallback(callback);
			return result;
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x00032CC8 File Offset: 0x00030EC8
		public static AsyncGPUReadbackRequest Request(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null)
		{
			return AsyncGPUReadback.Request(src, mipIndex, x, width, y, height, z, depth, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback);
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x00032CFC File Offset: 0x00030EFC
		public static AsyncGPUReadbackRequest Request(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadback.ValidateFormat(src, dstFormat);
			AsyncGPUReadbackRequest result = AsyncGPUReadback.Request_Internal_Texture_4(src, mipIndex, x, width, y, height, z, depth, dstFormat, null);
			result.SetScriptingCallback(callback);
			return result;
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x00032D3C File Offset: 0x00030F3C
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeArray<T>(ref NativeArray<T> output, ComputeBuffer src, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest result = AsyncGPUReadback.Request_Internal_ComputeBuffer_1(src, &asyncRequestNativeArrayData);
			result.SetScriptingCallback(callback);
			return result;
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x00032D70 File Offset: 0x00030F70
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeArray<T>(ref NativeArray<T> output, ComputeBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest result = AsyncGPUReadback.Request_Internal_ComputeBuffer_2(src, size, offset, &asyncRequestNativeArrayData);
			result.SetScriptingCallback(callback);
			return result;
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x00032DA8 File Offset: 0x00030FA8
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeArray<T>(ref NativeArray<T> output, GraphicsBuffer src, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest result = AsyncGPUReadback.Request_Internal_GraphicsBuffer_1(src, &asyncRequestNativeArrayData);
			result.SetScriptingCallback(callback);
			return result;
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x00032DDC File Offset: 0x00030FDC
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeArray<T>(ref NativeArray<T> output, GraphicsBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest result = AsyncGPUReadback.Request_Internal_GraphicsBuffer_2(src, size, offset, &asyncRequestNativeArrayData);
			result.SetScriptingCallback(callback);
			return result;
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x00032E14 File Offset: 0x00031014
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex = 0, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest result = AsyncGPUReadback.Request_Internal_Texture_1(src, mipIndex, &asyncRequestNativeArrayData);
			result.SetScriptingCallback(callback);
			return result;
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x00032E48 File Offset: 0x00031048
		public static AsyncGPUReadbackRequest RequestIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			return AsyncGPUReadback.RequestIntoNativeArray<T>(ref output, src, mipIndex, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback);
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x00032E74 File Offset: 0x00031074
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncGPUReadback.ValidateFormat(src, dstFormat);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest result = AsyncGPUReadback.Request_Internal_Texture_2(src, mipIndex, dstFormat, &asyncRequestNativeArrayData);
			result.SetScriptingCallback(callback);
			return result;
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x00032EB4 File Offset: 0x000310B4
		public static AsyncGPUReadbackRequest RequestIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			return AsyncGPUReadback.RequestIntoNativeArray<T>(ref output, src, mipIndex, x, width, y, height, z, depth, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback);
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x00032EEC File Offset: 0x000310EC
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncGPUReadback.ValidateFormat(src, dstFormat);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest result = AsyncGPUReadback.Request_Internal_Texture_4(src, mipIndex, x, width, y, height, z, depth, dstFormat, &asyncRequestNativeArrayData);
			result.SetScriptingCallback(callback);
			return result;
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x00032F38 File Offset: 0x00031138
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeSlice<T>(ref NativeSlice<T> output, ComputeBuffer src, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest result = AsyncGPUReadback.Request_Internal_ComputeBuffer_1(src, &asyncRequestNativeArrayData);
			result.SetScriptingCallback(callback);
			return result;
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x00032F6C File Offset: 0x0003116C
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeSlice<T>(ref NativeSlice<T> output, ComputeBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest result = AsyncGPUReadback.Request_Internal_ComputeBuffer_2(src, size, offset, &asyncRequestNativeArrayData);
			result.SetScriptingCallback(callback);
			return result;
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x00032FA4 File Offset: 0x000311A4
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeSlice<T>(ref NativeSlice<T> output, GraphicsBuffer src, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest result = AsyncGPUReadback.Request_Internal_GraphicsBuffer_1(src, &asyncRequestNativeArrayData);
			result.SetScriptingCallback(callback);
			return result;
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x00032FD8 File Offset: 0x000311D8
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeSlice<T>(ref NativeSlice<T> output, GraphicsBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest result = AsyncGPUReadback.Request_Internal_GraphicsBuffer_2(src, size, offset, &asyncRequestNativeArrayData);
			result.SetScriptingCallback(callback);
			return result;
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x00033010 File Offset: 0x00031210
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeSlice<T>(ref NativeSlice<T> output, Texture src, int mipIndex = 0, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest result = AsyncGPUReadback.Request_Internal_Texture_1(src, mipIndex, &asyncRequestNativeArrayData);
			result.SetScriptingCallback(callback);
			return result;
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x00033044 File Offset: 0x00031244
		public static AsyncGPUReadbackRequest RequestIntoNativeSlice<T>(ref NativeSlice<T> output, Texture src, int mipIndex, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			return AsyncGPUReadback.RequestIntoNativeSlice<T>(ref output, src, mipIndex, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback);
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x00033070 File Offset: 0x00031270
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeSlice<T>(ref NativeSlice<T> output, Texture src, int mipIndex, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncGPUReadback.ValidateFormat(src, dstFormat);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest result = AsyncGPUReadback.Request_Internal_Texture_2(src, mipIndex, dstFormat, &asyncRequestNativeArrayData);
			result.SetScriptingCallback(callback);
			return result;
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x000330B0 File Offset: 0x000312B0
		public static AsyncGPUReadbackRequest RequestIntoNativeSlice<T>(ref NativeSlice<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			return AsyncGPUReadback.RequestIntoNativeSlice<T>(ref output, src, mipIndex, x, width, y, height, z, depth, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback);
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x000330E8 File Offset: 0x000312E8
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeSlice<T>(ref NativeSlice<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncGPUReadback.ValidateFormat(src, dstFormat);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest result = AsyncGPUReadback.Request_Internal_Texture_4(src, mipIndex, x, width, y, height, z, depth, dstFormat, &asyncRequestNativeArrayData);
			result.SetScriptingCallback(callback);
			return result;
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x00033134 File Offset: 0x00031334
		[NativeMethod("Request")]
		private unsafe static AsyncGPUReadbackRequest Request_Internal_ComputeBuffer_1([NotNull("ArgumentNullException")] ComputeBuffer buffer, AsyncRequestNativeArrayData* data)
		{
			AsyncGPUReadbackRequest result;
			AsyncGPUReadback.Request_Internal_ComputeBuffer_1_Injected(buffer, data, out result);
			return result;
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x0003314C File Offset: 0x0003134C
		[NativeMethod("Request")]
		private unsafe static AsyncGPUReadbackRequest Request_Internal_ComputeBuffer_2([NotNull("ArgumentNullException")] ComputeBuffer src, int size, int offset, AsyncRequestNativeArrayData* data)
		{
			AsyncGPUReadbackRequest result;
			AsyncGPUReadback.Request_Internal_ComputeBuffer_2_Injected(src, size, offset, data, out result);
			return result;
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x00033168 File Offset: 0x00031368
		[NativeMethod("Request")]
		private unsafe static AsyncGPUReadbackRequest Request_Internal_GraphicsBuffer_1([NotNull("ArgumentNullException")] GraphicsBuffer buffer, AsyncRequestNativeArrayData* data)
		{
			AsyncGPUReadbackRequest result;
			AsyncGPUReadback.Request_Internal_GraphicsBuffer_1_Injected(buffer, data, out result);
			return result;
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x00033180 File Offset: 0x00031380
		[NativeMethod("Request")]
		private unsafe static AsyncGPUReadbackRequest Request_Internal_GraphicsBuffer_2([NotNull("ArgumentNullException")] GraphicsBuffer src, int size, int offset, AsyncRequestNativeArrayData* data)
		{
			AsyncGPUReadbackRequest result;
			AsyncGPUReadback.Request_Internal_GraphicsBuffer_2_Injected(src, size, offset, data, out result);
			return result;
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x0003319C File Offset: 0x0003139C
		[NativeMethod("Request")]
		private unsafe static AsyncGPUReadbackRequest Request_Internal_Texture_1([NotNull("ArgumentNullException")] Texture src, int mipIndex, AsyncRequestNativeArrayData* data)
		{
			AsyncGPUReadbackRequest result;
			AsyncGPUReadback.Request_Internal_Texture_1_Injected(src, mipIndex, data, out result);
			return result;
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x000331B4 File Offset: 0x000313B4
		[NativeMethod("Request")]
		private unsafe static AsyncGPUReadbackRequest Request_Internal_Texture_2([NotNull("ArgumentNullException")] Texture src, int mipIndex, GraphicsFormat dstFormat, AsyncRequestNativeArrayData* data)
		{
			AsyncGPUReadbackRequest result;
			AsyncGPUReadback.Request_Internal_Texture_2_Injected(src, mipIndex, dstFormat, data, out result);
			return result;
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x000331D0 File Offset: 0x000313D0
		[NativeMethod("Request")]
		private unsafe static AsyncGPUReadbackRequest Request_Internal_Texture_3([NotNull("ArgumentNullException")] Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, AsyncRequestNativeArrayData* data)
		{
			AsyncGPUReadbackRequest result;
			AsyncGPUReadback.Request_Internal_Texture_3_Injected(src, mipIndex, x, width, y, height, z, depth, data, out result);
			return result;
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x000331F4 File Offset: 0x000313F4
		[NativeMethod("Request")]
		private unsafe static AsyncGPUReadbackRequest Request_Internal_Texture_4([NotNull("ArgumentNullException")] Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, AsyncRequestNativeArrayData* data)
		{
			AsyncGPUReadbackRequest result;
			AsyncGPUReadback.Request_Internal_Texture_4_Injected(src, mipIndex, x, width, y, height, z, depth, dstFormat, data, out result);
			return result;
		}

		// Token: 0x06001F4C RID: 8012
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Request_Internal_ComputeBuffer_1_Injected(ComputeBuffer buffer, AsyncRequestNativeArrayData* data, out AsyncGPUReadbackRequest ret);

		// Token: 0x06001F4D RID: 8013
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Request_Internal_ComputeBuffer_2_Injected(ComputeBuffer src, int size, int offset, AsyncRequestNativeArrayData* data, out AsyncGPUReadbackRequest ret);

		// Token: 0x06001F4E RID: 8014
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Request_Internal_GraphicsBuffer_1_Injected(GraphicsBuffer buffer, AsyncRequestNativeArrayData* data, out AsyncGPUReadbackRequest ret);

		// Token: 0x06001F4F RID: 8015
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Request_Internal_GraphicsBuffer_2_Injected(GraphicsBuffer src, int size, int offset, AsyncRequestNativeArrayData* data, out AsyncGPUReadbackRequest ret);

		// Token: 0x06001F50 RID: 8016
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Request_Internal_Texture_1_Injected(Texture src, int mipIndex, AsyncRequestNativeArrayData* data, out AsyncGPUReadbackRequest ret);

		// Token: 0x06001F51 RID: 8017
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Request_Internal_Texture_2_Injected(Texture src, int mipIndex, GraphicsFormat dstFormat, AsyncRequestNativeArrayData* data, out AsyncGPUReadbackRequest ret);

		// Token: 0x06001F52 RID: 8018
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Request_Internal_Texture_3_Injected(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, AsyncRequestNativeArrayData* data, out AsyncGPUReadbackRequest ret);

		// Token: 0x06001F53 RID: 8019
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Request_Internal_Texture_4_Injected(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, AsyncRequestNativeArrayData* data, out AsyncGPUReadbackRequest ret);
	}
}
