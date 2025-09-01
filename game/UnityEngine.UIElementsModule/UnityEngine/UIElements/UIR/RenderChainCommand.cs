using System;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200034B RID: 843
	internal class RenderChainCommand : LinkedPoolItem<RenderChainCommand>
	{
		// Token: 0x06001B06 RID: 6918 RVA: 0x00077EF8 File Offset: 0x000760F8
		internal void Reset()
		{
			this.owner = null;
			this.prev = (this.next = null);
			this.closing = false;
			this.type = CommandType.Draw;
			this.state = default(State);
			this.mesh = null;
			this.indexOffset = (this.indexCount = 0);
			this.callback = null;
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x00077F58 File Offset: 0x00076158
		internal void ExecuteNonDrawMesh(DrawParams drawParams, float pixelsPerPoint, ref Exception immediateException)
		{
			switch (this.type)
			{
			case CommandType.ImmediateCull:
			{
				bool flag = !RenderChainCommand.RectPointsToPixelsAndFlipYAxis(this.owner.worldBound, pixelsPerPoint).Overlaps(Utility.GetActiveViewport());
				if (flag)
				{
					return;
				}
				break;
			}
			case CommandType.Immediate:
				break;
			case CommandType.PushView:
			{
				drawParams.view.Push(this.owner.worldTransform);
				GL.modelview = this.owner.worldTransform;
				VisualElement parent = this.owner.hierarchy.parent;
				bool flag2 = parent != null;
				Rect rect;
				if (flag2)
				{
					rect = parent.worldClip;
				}
				else
				{
					rect = DrawParams.k_FullNormalizedRect;
				}
				drawParams.scissor.Push(rect);
				Utility.SetScissorRect(RenderChainCommand.RectPointsToPixelsAndFlipYAxis(rect, pixelsPerPoint));
				return;
			}
			case CommandType.PopView:
			{
				drawParams.view.Pop();
				GL.modelview = drawParams.view.Peek();
				drawParams.scissor.Pop();
				Rect rect2 = drawParams.scissor.Peek();
				bool flag3 = rect2.x == DrawParams.k_UnlimitedRect.x;
				if (flag3)
				{
					Utility.DisableScissor();
				}
				else
				{
					Utility.SetScissorRect(RenderChainCommand.RectPointsToPixelsAndFlipYAxis(rect2, pixelsPerPoint));
				}
				return;
			}
			case CommandType.PushScissor:
			{
				Rect rect3 = RenderChainCommand.CombineScissorRects(this.owner.worldClip, drawParams.scissor.Peek());
				drawParams.scissor.Push(rect3);
				Utility.SetScissorRect(RenderChainCommand.RectPointsToPixelsAndFlipYAxis(rect3, pixelsPerPoint));
				return;
			}
			case CommandType.PopScissor:
			{
				drawParams.scissor.Pop();
				Rect rect4 = drawParams.scissor.Peek();
				bool flag4 = rect4.x == DrawParams.k_UnlimitedRect.x;
				if (flag4)
				{
					Utility.DisableScissor();
				}
				else
				{
					Utility.SetScissorRect(RenderChainCommand.RectPointsToPixelsAndFlipYAxis(rect4, pixelsPerPoint));
				}
				return;
			}
			case CommandType.PushRenderTexture:
			{
				RectInt activeViewport = Utility.GetActiveViewport();
				RenderTexture temporary = RenderTexture.GetTemporary(activeViewport.width, activeViewport.height, 24, RenderTextureFormat.ARGBHalf);
				RenderTexture.active = temporary;
				GL.Clear(true, true, new Color(0f, 0f, 0f, 0f), 0.99f);
				drawParams.renderTexture.Add(RenderTexture.active);
				return;
			}
			case CommandType.PopRenderTexture:
			{
				int num = drawParams.renderTexture.Count - 1;
				Debug.Assert(num > 0);
				Debug.Assert(drawParams.renderTexture[num - 1] == RenderTexture.active, "Content of previous render texture was probably not blitted");
				RenderTexture renderTexture = drawParams.renderTexture[num];
				bool flag5 = renderTexture != null;
				if (flag5)
				{
					RenderTexture.ReleaseTemporary(renderTexture);
				}
				drawParams.renderTexture.RemoveAt(num);
				return;
			}
			case CommandType.BlitToPreviousRT:
			{
				RenderTexture renderTexture2 = drawParams.renderTexture[drawParams.renderTexture.Count - 1];
				RenderTexture destination = drawParams.renderTexture[drawParams.renderTexture.Count - 2];
				Debug.Assert(renderTexture2 == RenderTexture.active, "Unexpected render target change: Current renderTarget is not the one on the top of the stack");
				this.Blit(renderTexture2, destination, 0f);
				return;
			}
			case CommandType.PushDefaultMaterial:
				return;
			case CommandType.PopDefaultMaterial:
				return;
			default:
				return;
			}
			bool flag6 = immediateException != null;
			if (!flag6)
			{
				RenderChainCommand.s_ImmediateOverheadMarker.Begin();
				Matrix4x4 unityProjectionMatrix = Utility.GetUnityProjectionMatrix();
				Camera current = Camera.current;
				RenderTexture active = RenderTexture.active;
				bool flag7 = drawParams.scissor.Count > 1;
				bool flag8 = flag7;
				if (flag8)
				{
					Utility.DisableScissor();
				}
				using (new GUIClip.ParentClipScope(this.owner.worldTransform, this.owner.worldClip))
				{
					RenderChainCommand.s_ImmediateOverheadMarker.End();
					try
					{
						this.callback();
					}
					catch (Exception ex)
					{
						immediateException = ex;
					}
					RenderChainCommand.s_ImmediateOverheadMarker.Begin();
				}
				Camera.SetupCurrent(current);
				RenderTexture.active = active;
				GL.modelview = drawParams.view.Peek();
				GL.LoadProjectionMatrix(unityProjectionMatrix);
				bool flag9 = flag7;
				if (flag9)
				{
					Utility.SetScissorRect(RenderChainCommand.RectPointsToPixelsAndFlipYAxis(drawParams.scissor.Peek(), pixelsPerPoint));
				}
				RenderChainCommand.s_ImmediateOverheadMarker.End();
			}
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x000783B4 File Offset: 0x000765B4
		private void Blit(Texture source, RenderTexture destination, float depth)
		{
			GL.PushMatrix();
			GL.LoadOrtho();
			RenderTexture.active = destination;
			this.state.material.SetTexture(RenderChainCommand.k_ID_MainTex, source);
			this.state.material.SetPass(0);
			GL.Begin(7);
			GL.TexCoord2(0f, 0f);
			GL.Vertex3(0f, 0f, depth);
			GL.TexCoord2(0f, 1f);
			GL.Vertex3(0f, 1f, depth);
			GL.TexCoord2(1f, 1f);
			GL.Vertex3(1f, 1f, depth);
			GL.TexCoord2(1f, 0f);
			GL.Vertex3(1f, 0f, depth);
			GL.End();
			GL.PopMatrix();
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x00078498 File Offset: 0x00076698
		private static Vector4 RectToClipSpace(Rect rc)
		{
			Matrix4x4 deviceProjectionMatrix = Utility.GetDeviceProjectionMatrix();
			Vector3 vector = deviceProjectionMatrix.MultiplyPoint(new Vector3(rc.xMin, rc.yMin, 0f));
			Vector3 vector2 = deviceProjectionMatrix.MultiplyPoint(new Vector3(rc.xMax, rc.yMax, 0f));
			return new Vector4(Mathf.Min(vector.x, vector2.x), Mathf.Min(vector.y, vector2.y), Mathf.Max(vector.x, vector2.x), Mathf.Max(vector.y, vector2.y));
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x0007853C File Offset: 0x0007673C
		private static Rect CombineScissorRects(Rect r0, Rect r1)
		{
			Rect result = new Rect(0f, 0f, 0f, 0f);
			result.x = Math.Max(r0.x, r1.x);
			result.y = Math.Max(r0.y, r1.y);
			result.xMax = Math.Max(result.x, Math.Min(r0.xMax, r1.xMax));
			result.yMax = Math.Max(result.y, Math.Min(r0.yMax, r1.yMax));
			return result;
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x000785F0 File Offset: 0x000767F0
		private static RectInt RectPointsToPixelsAndFlipYAxis(Rect rect, float pixelsPerPoint)
		{
			float num = (float)Utility.GetActiveViewport().height;
			return new RectInt(0, 0, 0, 0)
			{
				x = Mathf.RoundToInt(rect.x * pixelsPerPoint),
				y = Mathf.RoundToInt(num - rect.yMax * pixelsPerPoint),
				width = Mathf.RoundToInt(rect.width * pixelsPerPoint),
				height = Mathf.RoundToInt(rect.height * pixelsPerPoint)
			};
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x00078677 File Offset: 0x00076877
		public RenderChainCommand()
		{
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x00078680 File Offset: 0x00076880
		// Note: this type is marked as 'beforefieldinit'.
		static RenderChainCommand()
		{
		}

		// Token: 0x04000D07 RID: 3335
		internal VisualElement owner;

		// Token: 0x04000D08 RID: 3336
		internal RenderChainCommand prev;

		// Token: 0x04000D09 RID: 3337
		internal RenderChainCommand next;

		// Token: 0x04000D0A RID: 3338
		internal bool closing;

		// Token: 0x04000D0B RID: 3339
		internal CommandType type;

		// Token: 0x04000D0C RID: 3340
		internal State state;

		// Token: 0x04000D0D RID: 3341
		internal MeshHandle mesh;

		// Token: 0x04000D0E RID: 3342
		internal int indexOffset;

		// Token: 0x04000D0F RID: 3343
		internal int indexCount;

		// Token: 0x04000D10 RID: 3344
		internal Action callback;

		// Token: 0x04000D11 RID: 3345
		private static readonly int k_ID_MainTex = Shader.PropertyToID("_MainTex");

		// Token: 0x04000D12 RID: 3346
		private static ProfilerMarker s_ImmediateOverheadMarker = new ProfilerMarker("UIR.ImmediateOverhead");
	}
}
