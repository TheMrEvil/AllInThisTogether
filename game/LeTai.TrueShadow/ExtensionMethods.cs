using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace LeTai
{
	// Token: 0x02000002 RID: 2
	public static class ExtensionMethods
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static Vector4 ToMinMaxVector(this Rect self)
		{
			return new Vector4(self.xMin, self.yMin, self.xMax, self.yMax);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002074 File Offset: 0x00000274
		private static Mesh FullscreenTriangle
		{
			get
			{
				if (ExtensionMethods.fullscreenTriangle != null)
				{
					return ExtensionMethods.fullscreenTriangle;
				}
				ExtensionMethods.fullscreenTriangle = new Mesh
				{
					name = "Fullscreen Triangle"
				};
				ExtensionMethods.fullscreenTriangle.SetVertices(new List<Vector3>
				{
					new Vector3(-1f, -1f, 0f),
					new Vector3(-1f, 3f, 0f),
					new Vector3(3f, -1f, 0f)
				});
				ExtensionMethods.fullscreenTriangle.SetIndices(new int[]
				{
					0,
					1,
					2
				}, MeshTopology.Triangles, 0, false);
				ExtensionMethods.fullscreenTriangle.UploadMeshData(false);
				return ExtensionMethods.fullscreenTriangle;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002131 File Offset: 0x00000331
		public static void BlitFullscreenTriangle(this CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, Material material, int pass = 0)
		{
			cmd.SetGlobalTexture("_MainTex", source);
			cmd.SetRenderTarget(destination, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store);
			cmd.DrawMesh(ExtensionMethods.FullscreenTriangle, Matrix4x4.identity, material, 0, pass);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000215C File Offset: 0x0000035C
		internal static bool Approximately(this Rect self, Rect other)
		{
			return ExtensionMethods.QuickApproximate(self.x, other.x) && ExtensionMethods.QuickApproximate(self.y, other.y) && ExtensionMethods.QuickApproximate(self.width, other.width) && ExtensionMethods.QuickApproximate(self.height, other.height);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021BD File Offset: 0x000003BD
		private static bool QuickApproximate(float a, float b)
		{
			return Mathf.Abs(b - a) < 1.175494E-38f;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021CE File Offset: 0x000003CE
		public static Vector3 WithZ(this Vector2 self, float z)
		{
			return new Vector3(self.x, self.y, z);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021E2 File Offset: 0x000003E2
		public static Color WithA(this Color self, float a)
		{
			return new Color(self.r, self.g, self.b, a);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021FC File Offset: 0x000003FC
		public static void NextFrames(this MonoBehaviour behaviour, Action action, int nFrames = 1)
		{
			behaviour.StartCoroutine(ExtensionMethods.NextFrame(action, nFrames));
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000220C File Offset: 0x0000040C
		private static IEnumerator NextFrame(Action action, int nFrames)
		{
			int num;
			for (int i = 0; i < nFrames; i = num + 1)
			{
				yield return null;
				num = i;
			}
			action();
			yield break;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002222 File Offset: 0x00000422
		public static void SetKeyword(this Material material, string keyword, bool enabled)
		{
			if (enabled)
			{
				material.EnableKeyword(keyword);
				return;
			}
			material.DisableKeyword(keyword);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002236 File Offset: 0x00000436
		public static Vector2 Frac(this Vector2 vec)
		{
			return new Vector2(vec.x - Mathf.Floor(vec.x), vec.y - Mathf.Floor(vec.y));
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002261 File Offset: 0x00000461
		public static Vector2 LocalToScreenPoint(this RectTransform rt, Vector3 localPoint, Camera referenceCamera = null)
		{
			return RectTransformUtility.WorldToScreenPoint(referenceCamera, rt.TransformPoint(localPoint));
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002270 File Offset: 0x00000470
		public static Vector2 ScreenToCanvasSize(this RectTransform rt, Vector2 size, Camera referenceCamera = null)
		{
			Vector2 b;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, Vector2.zero, referenceCamera, out b);
			Vector2 a;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, size, referenceCamera, out a);
			return a - b;
		}

		// Token: 0x04000001 RID: 1
		private static Mesh fullscreenTriangle;

		// Token: 0x0200002C RID: 44
		[CompilerGenerated]
		private sealed class <NextFrame>d__10 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06000144 RID: 324 RVA: 0x00006C93 File Offset: 0x00004E93
			[DebuggerHidden]
			public <NextFrame>d__10(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000145 RID: 325 RVA: 0x00006CA2 File Offset: 0x00004EA2
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000146 RID: 326 RVA: 0x00006CA4 File Offset: 0x00004EA4
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					int num2 = i;
					i = num2 + 1;
				}
				else
				{
					this.<>1__state = -1;
					i = 0;
				}
				if (i >= nFrames)
				{
					action();
					return false;
				}
				this.<>2__current = null;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x06000147 RID: 327 RVA: 0x00006D12 File Offset: 0x00004F12
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000148 RID: 328 RVA: 0x00006D1A File Offset: 0x00004F1A
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x06000149 RID: 329 RVA: 0x00006D21 File Offset: 0x00004F21
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x040000C2 RID: 194
			private int <>1__state;

			// Token: 0x040000C3 RID: 195
			private object <>2__current;

			// Token: 0x040000C4 RID: 196
			public int nFrames;

			// Token: 0x040000C5 RID: 197
			public Action action;

			// Token: 0x040000C6 RID: 198
			private int <i>5__2;
		}
	}
}
