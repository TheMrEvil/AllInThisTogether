using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Windows.WebCam
{
	// Token: 0x020002A6 RID: 678
	[MovedFrom("UnityEngine.XR.WSA.WebCam")]
	[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
	[NativeHeader("PlatformDependent/Win/Webcam/PhotoCaptureFrame.h")]
	public sealed class PhotoCaptureFrame : IDisposable
	{
		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001CC1 RID: 7361 RVA: 0x0002E177 File Offset: 0x0002C377
		// (set) Token: 0x06001CC2 RID: 7362 RVA: 0x0002E17F File Offset: 0x0002C37F
		public int dataLength
		{
			[CompilerGenerated]
			get
			{
				return this.<dataLength>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<dataLength>k__BackingField = value;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001CC3 RID: 7363 RVA: 0x0002E188 File Offset: 0x0002C388
		// (set) Token: 0x06001CC4 RID: 7364 RVA: 0x0002E190 File Offset: 0x0002C390
		public bool hasLocationData
		{
			[CompilerGenerated]
			get
			{
				return this.<hasLocationData>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<hasLocationData>k__BackingField = value;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001CC5 RID: 7365 RVA: 0x0002E199 File Offset: 0x0002C399
		// (set) Token: 0x06001CC6 RID: 7366 RVA: 0x0002E1A1 File Offset: 0x0002C3A1
		public CapturePixelFormat pixelFormat
		{
			[CompilerGenerated]
			get
			{
				return this.<pixelFormat>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<pixelFormat>k__BackingField = value;
			}
		}

		// Token: 0x06001CC7 RID: 7367
		[ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetDataLength();

		// Token: 0x06001CC8 RID: 7368
		[ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetHasLocationData();

		// Token: 0x06001CC9 RID: 7369
		[ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern CapturePixelFormat GetCapturePixelFormat();

		// Token: 0x06001CCA RID: 7370 RVA: 0x0002E1AC File Offset: 0x0002C3AC
		public bool TryGetCameraToWorldMatrix(out Matrix4x4 cameraToWorldMatrix)
		{
			cameraToWorldMatrix = Matrix4x4.identity;
			bool hasLocationData = this.hasLocationData;
			bool result;
			if (hasLocationData)
			{
				cameraToWorldMatrix = this.GetCameraToWorldMatrix();
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x0002E1E8 File Offset: 0x0002C3E8
		[NativeConditional("PLATFORM_WIN && !PLATFORM_XBOXONE", "Matrix4x4f()")]
		[NativeName("GetCameraToWorld")]
		[ThreadAndSerializationSafe]
		private Matrix4x4 GetCameraToWorldMatrix()
		{
			Matrix4x4 result;
			this.GetCameraToWorldMatrix_Injected(out result);
			return result;
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x0002E200 File Offset: 0x0002C400
		public bool TryGetProjectionMatrix(out Matrix4x4 projectionMatrix)
		{
			bool hasLocationData = this.hasLocationData;
			bool result;
			if (hasLocationData)
			{
				projectionMatrix = this.GetProjection();
				result = true;
			}
			else
			{
				projectionMatrix = Matrix4x4.identity;
				result = false;
			}
			return result;
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x0002E23C File Offset: 0x0002C43C
		public bool TryGetProjectionMatrix(float nearClipPlane, float farClipPlane, out Matrix4x4 projectionMatrix)
		{
			bool hasLocationData = this.hasLocationData;
			bool result;
			if (hasLocationData)
			{
				float num = 0.01f;
				bool flag = nearClipPlane < num;
				if (flag)
				{
					nearClipPlane = num;
				}
				bool flag2 = farClipPlane < nearClipPlane + num;
				if (flag2)
				{
					farClipPlane = nearClipPlane + num;
				}
				projectionMatrix = this.GetProjection();
				float num2 = 1f / (farClipPlane - nearClipPlane);
				float m = -(farClipPlane + nearClipPlane) * num2;
				float m2 = -(2f * farClipPlane * nearClipPlane) * num2;
				projectionMatrix.m22 = m;
				projectionMatrix.m23 = m2;
				result = true;
			}
			else
			{
				projectionMatrix = Matrix4x4.identity;
				result = false;
			}
			return result;
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x0002E2D0 File Offset: 0x0002C4D0
		[NativeConditional("PLATFORM_WIN && !PLATFORM_XBOXONE", "Matrix4x4f()")]
		[ThreadAndSerializationSafe]
		private Matrix4x4 GetProjection()
		{
			Matrix4x4 result;
			this.GetProjection_Injected(out result);
			return result;
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x0002E2E8 File Offset: 0x0002C4E8
		public void UploadImageDataToTexture(Texture2D targetTexture)
		{
			bool flag = targetTexture == null;
			if (flag)
			{
				throw new ArgumentNullException("targetTexture");
			}
			bool flag2 = this.pixelFormat > CapturePixelFormat.BGRA32;
			if (flag2)
			{
				throw new ArgumentException("Uploading PhotoCaptureFrame to a texture is only supported with BGRA32 CameraFrameFormat!");
			}
			this.UploadImageDataToTexture_Internal(targetTexture);
		}

		// Token: 0x06001CD0 RID: 7376
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeName("UploadImageDataToTexture")]
		[ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void UploadImageDataToTexture_Internal(Texture2D targetTexture);

		// Token: 0x06001CD1 RID: 7377
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern IntPtr GetUnsafePointerToBuffer();

		// Token: 0x06001CD2 RID: 7378 RVA: 0x0002E330 File Offset: 0x0002C530
		public void CopyRawImageDataIntoBuffer(List<byte> byteBuffer)
		{
			bool flag = byteBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("byteBuffer");
			}
			byte[] array = new byte[this.dataLength];
			this.CopyRawImageDataIntoBuffer_Internal(array);
			bool flag2 = byteBuffer.Capacity < array.Length;
			if (flag2)
			{
				byteBuffer.Capacity = array.Length;
			}
			byteBuffer.Clear();
			byteBuffer.AddRange(array);
		}

		// Token: 0x06001CD3 RID: 7379
		[ThreadAndSerializationSafe]
		[NativeName("CopyRawImageDataIntoBuffer")]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void CopyRawImageDataIntoBuffer_Internal([Out] byte[] byteArray);

		// Token: 0x06001CD4 RID: 7380 RVA: 0x0002E390 File Offset: 0x0002C590
		internal PhotoCaptureFrame(IntPtr nativePtr)
		{
			this.m_NativePtr = nativePtr;
			this.dataLength = this.GetDataLength();
			this.hasLocationData = this.GetHasLocationData();
			this.pixelFormat = this.GetCapturePixelFormat();
			GC.AddMemoryPressure((long)this.dataLength);
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x0002E3E0 File Offset: 0x0002C5E0
		private void Cleanup()
		{
			bool flag = this.m_NativePtr != IntPtr.Zero;
			if (flag)
			{
				GC.RemoveMemoryPressure((long)this.dataLength);
				this.Dispose_Internal();
				this.m_NativePtr = IntPtr.Zero;
			}
		}

		// Token: 0x06001CD6 RID: 7382
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeName("Dispose")]
		[ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Dispose_Internal();

		// Token: 0x06001CD7 RID: 7383 RVA: 0x0002E423 File Offset: 0x0002C623
		public void Dispose()
		{
			this.Cleanup();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x0002E434 File Offset: 0x0002C634
		~PhotoCaptureFrame()
		{
			this.Cleanup();
		}

		// Token: 0x06001CD9 RID: 7385
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetCameraToWorldMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x06001CDA RID: 7386
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetProjection_Injected(out Matrix4x4 ret);

		// Token: 0x04000967 RID: 2407
		private IntPtr m_NativePtr;

		// Token: 0x04000968 RID: 2408
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <dataLength>k__BackingField;

		// Token: 0x04000969 RID: 2409
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <hasLocationData>k__BackingField;

		// Token: 0x0400096A RID: 2410
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private CapturePixelFormat <pixelFormat>k__BackingField;
	}
}
