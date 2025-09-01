using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200012E RID: 302
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[NativeHeader("Runtime/Camera/Camera.h")]
	[NativeHeader("Runtime/Camera/CameraUtil.h")]
	[StaticAccessor("GetGfxDevice()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/GfxDevice/GfxDevice.h")]
	public sealed class GL
	{
		// Token: 0x06000957 RID: 2391
		[NativeName("ImmediateVertex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Vertex3(float x, float y, float z);

		// Token: 0x06000958 RID: 2392 RVA: 0x0000E68C File Offset: 0x0000C88C
		public static void Vertex(Vector3 v)
		{
			GL.Vertex3(v.x, v.y, v.z);
		}

		// Token: 0x06000959 RID: 2393
		[NativeName("ImmediateVertices")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void Vertices(Vector3* v, Vector3* coords, Vector4* colors, int length);

		// Token: 0x0600095A RID: 2394
		[NativeName("ImmediateTexCoordAll")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void TexCoord3(float x, float y, float z);

		// Token: 0x0600095B RID: 2395 RVA: 0x0000E6A7 File Offset: 0x0000C8A7
		public static void TexCoord(Vector3 v)
		{
			GL.TexCoord3(v.x, v.y, v.z);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0000E6C2 File Offset: 0x0000C8C2
		public static void TexCoord2(float x, float y)
		{
			GL.TexCoord3(x, y, 0f);
		}

		// Token: 0x0600095D RID: 2397
		[NativeName("ImmediateTexCoord")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void MultiTexCoord3(int unit, float x, float y, float z);

		// Token: 0x0600095E RID: 2398 RVA: 0x0000E6D2 File Offset: 0x0000C8D2
		public static void MultiTexCoord(int unit, Vector3 v)
		{
			GL.MultiTexCoord3(unit, v.x, v.y, v.z);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0000E6EE File Offset: 0x0000C8EE
		public static void MultiTexCoord2(int unit, float x, float y)
		{
			GL.MultiTexCoord3(unit, x, y, 0f);
		}

		// Token: 0x06000960 RID: 2400
		[NativeName("ImmediateColor")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ImmediateColor(float r, float g, float b, float a);

		// Token: 0x06000961 RID: 2401 RVA: 0x0000E6FF File Offset: 0x0000C8FF
		public static void Color(Color c)
		{
			GL.ImmediateColor(c.r, c.g, c.b, c.a);
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000962 RID: 2402
		// (set) Token: 0x06000963 RID: 2403
		public static extern bool wireframe { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000964 RID: 2404
		// (set) Token: 0x06000965 RID: 2405
		public static extern bool sRGBWrite { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000966 RID: 2406
		// (set) Token: 0x06000967 RID: 2407
		[NativeProperty("UserBackfaceMode")]
		public static extern bool invertCulling { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000968 RID: 2408
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Flush();

		// Token: 0x06000969 RID: 2409
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RenderTargetBarrier();

		// Token: 0x0600096A RID: 2410 RVA: 0x0000E720 File Offset: 0x0000C920
		private static Matrix4x4 GetWorldViewMatrix()
		{
			Matrix4x4 result;
			GL.GetWorldViewMatrix_Injected(out result);
			return result;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0000E735 File Offset: 0x0000C935
		private static void SetViewMatrix(Matrix4x4 m)
		{
			GL.SetViewMatrix_Injected(ref m);
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x0000E740 File Offset: 0x0000C940
		// (set) Token: 0x0600096D RID: 2413 RVA: 0x0000E757 File Offset: 0x0000C957
		public static Matrix4x4 modelview
		{
			get
			{
				return GL.GetWorldViewMatrix();
			}
			set
			{
				GL.SetViewMatrix(value);
			}
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0000E761 File Offset: 0x0000C961
		[NativeName("SetWorldMatrix")]
		public static void MultMatrix(Matrix4x4 m)
		{
			GL.MultMatrix_Injected(ref m);
		}

		// Token: 0x0600096F RID: 2415
		[NativeName("InsertCustomMarker")]
		[Obsolete("IssuePluginEvent(eventID) is deprecated. Use IssuePluginEvent(callback, eventID) instead.", false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void IssuePluginEvent(int eventID);

		// Token: 0x06000970 RID: 2416
		[NativeName("SetUserBackfaceMode")]
		[Obsolete("SetRevertBackfacing(revertBackFaces) is deprecated. Use invertCulling property instead.", false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetRevertBackfacing(bool revertBackFaces);

		// Token: 0x06000971 RID: 2417
		[FreeFunction("GLPushMatrixScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PushMatrix();

		// Token: 0x06000972 RID: 2418
		[FreeFunction("GLPopMatrixScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PopMatrix();

		// Token: 0x06000973 RID: 2419
		[FreeFunction("GLLoadIdentityScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LoadIdentity();

		// Token: 0x06000974 RID: 2420
		[FreeFunction("GLLoadOrthoScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LoadOrtho();

		// Token: 0x06000975 RID: 2421
		[FreeFunction("GLLoadPixelMatrixScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LoadPixelMatrix();

		// Token: 0x06000976 RID: 2422 RVA: 0x0000E76A File Offset: 0x0000C96A
		[FreeFunction("GLLoadProjectionMatrixScript")]
		public static void LoadProjectionMatrix(Matrix4x4 mat)
		{
			GL.LoadProjectionMatrix_Injected(ref mat);
		}

		// Token: 0x06000977 RID: 2423
		[FreeFunction("GLInvalidateState")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void InvalidateState();

		// Token: 0x06000978 RID: 2424 RVA: 0x0000E774 File Offset: 0x0000C974
		[FreeFunction("GLGetGPUProjectionMatrix")]
		public static Matrix4x4 GetGPUProjectionMatrix(Matrix4x4 proj, bool renderIntoTexture)
		{
			Matrix4x4 result;
			GL.GetGPUProjectionMatrix_Injected(ref proj, renderIntoTexture, out result);
			return result;
		}

		// Token: 0x06000979 RID: 2425
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GLLoadPixelMatrixScript(float left, float right, float bottom, float top);

		// Token: 0x0600097A RID: 2426 RVA: 0x0000E78C File Offset: 0x0000C98C
		public static void LoadPixelMatrix(float left, float right, float bottom, float top)
		{
			GL.GLLoadPixelMatrixScript(left, right, bottom, top);
		}

		// Token: 0x0600097B RID: 2427
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GLIssuePluginEvent(IntPtr callback, int eventID);

		// Token: 0x0600097C RID: 2428 RVA: 0x0000E79C File Offset: 0x0000C99C
		public static void IssuePluginEvent(IntPtr callback, int eventID)
		{
			bool flag = callback == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("Null callback specified.", "callback");
			}
			GL.GLIssuePluginEvent(callback, eventID);
		}

		// Token: 0x0600097D RID: 2429
		[FreeFunction("GLBegin", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Begin(int mode);

		// Token: 0x0600097E RID: 2430
		[FreeFunction("GLEnd")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void End();

		// Token: 0x0600097F RID: 2431 RVA: 0x0000E7D1 File Offset: 0x0000C9D1
		[FreeFunction]
		private static void GLClear(bool clearDepth, bool clearColor, Color backgroundColor, float depth)
		{
			GL.GLClear_Injected(clearDepth, clearColor, ref backgroundColor, depth);
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0000E7DD File Offset: 0x0000C9DD
		public static void Clear(bool clearDepth, bool clearColor, Color backgroundColor, [DefaultValue("1.0f")] float depth)
		{
			GL.GLClear(clearDepth, clearColor, backgroundColor, depth);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0000E7EA File Offset: 0x0000C9EA
		public static void Clear(bool clearDepth, bool clearColor, Color backgroundColor)
		{
			GL.GLClear(clearDepth, clearColor, backgroundColor, 1f);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0000E7FB File Offset: 0x0000C9FB
		[FreeFunction("SetGLViewport")]
		public static void Viewport(Rect pixelRect)
		{
			GL.Viewport_Injected(ref pixelRect);
		}

		// Token: 0x06000983 RID: 2435
		[FreeFunction("ClearWithSkybox")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ClearWithSkybox(bool clearDepth, Camera camera);

		// Token: 0x06000984 RID: 2436 RVA: 0x00002072 File Offset: 0x00000272
		public GL()
		{
		}

		// Token: 0x06000985 RID: 2437
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetWorldViewMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x06000986 RID: 2438
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetViewMatrix_Injected(ref Matrix4x4 m);

		// Token: 0x06000987 RID: 2439
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MultMatrix_Injected(ref Matrix4x4 m);

		// Token: 0x06000988 RID: 2440
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LoadProjectionMatrix_Injected(ref Matrix4x4 mat);

		// Token: 0x06000989 RID: 2441
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGPUProjectionMatrix_Injected(ref Matrix4x4 proj, bool renderIntoTexture, out Matrix4x4 ret);

		// Token: 0x0600098A RID: 2442
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GLClear_Injected(bool clearDepth, bool clearColor, ref Color backgroundColor, float depth);

		// Token: 0x0600098B RID: 2443
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Viewport_Injected(ref Rect pixelRect);

		// Token: 0x040003CD RID: 973
		public const int TRIANGLES = 4;

		// Token: 0x040003CE RID: 974
		public const int TRIANGLE_STRIP = 5;

		// Token: 0x040003CF RID: 975
		public const int QUADS = 7;

		// Token: 0x040003D0 RID: 976
		public const int LINES = 1;

		// Token: 0x040003D1 RID: 977
		public const int LINE_STRIP = 2;
	}
}
