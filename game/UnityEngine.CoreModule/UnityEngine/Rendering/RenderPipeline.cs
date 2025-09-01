using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x02000405 RID: 1029
	public abstract class RenderPipeline
	{
		// Token: 0x060022F9 RID: 8953
		protected abstract void Render(ScriptableRenderContext context, Camera[] cameras);

		// Token: 0x060022FA RID: 8954 RVA: 0x00004563 File Offset: 0x00002763
		protected virtual void ProcessRenderRequests(ScriptableRenderContext context, Camera camera, List<Camera.RenderRequest> renderRequests)
		{
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x0003AF39 File Offset: 0x00039139
		protected static void BeginFrameRendering(ScriptableRenderContext context, Camera[] cameras)
		{
			RenderPipelineManager.BeginContextRendering(context, new List<Camera>(cameras));
		}

		// Token: 0x060022FC RID: 8956 RVA: 0x0003AF49 File Offset: 0x00039149
		protected static void BeginContextRendering(ScriptableRenderContext context, List<Camera> cameras)
		{
			RenderPipelineManager.BeginContextRendering(context, cameras);
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x0003AF54 File Offset: 0x00039154
		protected static void BeginCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			RenderPipelineManager.BeginCameraRendering(context, camera);
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x0003AF5F File Offset: 0x0003915F
		protected static void EndContextRendering(ScriptableRenderContext context, List<Camera> cameras)
		{
			RenderPipelineManager.EndContextRendering(context, cameras);
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x0003AF6A File Offset: 0x0003916A
		protected static void EndFrameRendering(ScriptableRenderContext context, Camera[] cameras)
		{
			RenderPipelineManager.EndContextRendering(context, new List<Camera>(cameras));
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x0003AF7A File Offset: 0x0003917A
		protected static void EndCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			RenderPipelineManager.EndCameraRendering(context, camera);
		}

		// Token: 0x06002301 RID: 8961 RVA: 0x0003AF85 File Offset: 0x00039185
		protected virtual void Render(ScriptableRenderContext context, List<Camera> cameras)
		{
			this.Render(context, cameras.ToArray());
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x0003AF98 File Offset: 0x00039198
		internal void InternalRender(ScriptableRenderContext context, List<Camera> cameras)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				throw new ObjectDisposedException(string.Format("{0} has been disposed. Do not call Render on disposed a RenderPipeline.", this));
			}
			this.Render(context, cameras);
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x0003AFCC File Offset: 0x000391CC
		internal void InternalRenderWithRequests(ScriptableRenderContext context, List<Camera> cameras, List<Camera.RenderRequest> renderRequests)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				throw new ObjectDisposedException(string.Format("{0} has been disposed. Do not call Render on disposed a RenderPipeline.", this));
			}
			this.ProcessRenderRequests(context, (cameras == null || cameras.Count == 0) ? null : cameras[0], renderRequests);
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06002304 RID: 8964 RVA: 0x0003B013 File Offset: 0x00039213
		// (set) Token: 0x06002305 RID: 8965 RVA: 0x0003B01B File Offset: 0x0003921B
		public bool disposed
		{
			[CompilerGenerated]
			get
			{
				return this.<disposed>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<disposed>k__BackingField = value;
			}
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x0003B024 File Offset: 0x00039224
		internal void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
			this.disposed = true;
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x00004563 File Offset: 0x00002763
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06002308 RID: 8968 RVA: 0x0003B040 File Offset: 0x00039240
		public virtual RenderPipelineGlobalSettings defaultSettings
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x00002072 File Offset: 0x00000272
		protected RenderPipeline()
		{
		}

		// Token: 0x04000D0B RID: 3339
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <disposed>k__BackingField;
	}
}
