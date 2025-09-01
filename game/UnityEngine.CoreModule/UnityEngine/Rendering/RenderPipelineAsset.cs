using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000406 RID: 1030
	public abstract class RenderPipelineAsset : ScriptableObject
	{
		// Token: 0x0600230A RID: 8970 RVA: 0x0003B054 File Offset: 0x00039254
		internal RenderPipeline InternalCreatePipeline()
		{
			RenderPipeline result = null;
			try
			{
				result = this.CreatePipeline();
			}
			catch (Exception ex)
			{
				bool flag = !ex.Data.Contains("InvalidImport") || !(ex.Data["InvalidImport"] is int) || (int)ex.Data["InvalidImport"] != 1;
				if (flag)
				{
					Debug.LogException(ex);
				}
			}
			return result;
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x0600230B RID: 8971 RVA: 0x000311B6 File Offset: 0x0002F3B6
		public virtual string[] renderingLayerMaskNames
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x0600230C RID: 8972 RVA: 0x000311B6 File Offset: 0x0002F3B6
		public virtual string[] prefixedRenderingLayerMaskNames
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x0600230D RID: 8973 RVA: 0x000311B6 File Offset: 0x0002F3B6
		public virtual Material defaultMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x0600230E RID: 8974 RVA: 0x000311B6 File Offset: 0x0002F3B6
		public virtual Shader autodeskInteractiveShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x0600230F RID: 8975 RVA: 0x000311B6 File Offset: 0x0002F3B6
		public virtual Shader autodeskInteractiveTransparentShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06002310 RID: 8976 RVA: 0x000311B6 File Offset: 0x0002F3B6
		public virtual Shader autodeskInteractiveMaskedShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06002311 RID: 8977 RVA: 0x000311B6 File Offset: 0x0002F3B6
		public virtual Shader terrainDetailLitShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06002312 RID: 8978 RVA: 0x000311B6 File Offset: 0x0002F3B6
		public virtual Shader terrainDetailGrassShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06002313 RID: 8979 RVA: 0x000311B6 File Offset: 0x0002F3B6
		public virtual Shader terrainDetailGrassBillboardShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06002314 RID: 8980 RVA: 0x000311B6 File Offset: 0x0002F3B6
		public virtual Material defaultParticleMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06002315 RID: 8981 RVA: 0x000311B6 File Offset: 0x0002F3B6
		public virtual Material defaultLineMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06002316 RID: 8982 RVA: 0x000311B6 File Offset: 0x0002F3B6
		public virtual Material defaultTerrainMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06002317 RID: 8983 RVA: 0x000311B6 File Offset: 0x0002F3B6
		public virtual Material defaultUIMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06002318 RID: 8984 RVA: 0x000311B6 File Offset: 0x0002F3B6
		public virtual Material defaultUIOverdrawMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06002319 RID: 8985 RVA: 0x000311B6 File Offset: 0x0002F3B6
		public virtual Material defaultUIETC1SupportedMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x0600231A RID: 8986 RVA: 0x000311B6 File Offset: 0x0002F3B6
		public virtual Material default2DMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x0600231B RID: 8987 RVA: 0x000311B6 File Offset: 0x0002F3B6
		public virtual Material default2DMaskMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x0600231C RID: 8988 RVA: 0x000311B6 File Offset: 0x0002F3B6
		public virtual Shader defaultShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x0600231D RID: 8989 RVA: 0x000311B6 File Offset: 0x0002F3B6
		public virtual Shader defaultSpeedTree7Shader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x0600231E RID: 8990 RVA: 0x000311B6 File Offset: 0x0002F3B6
		public virtual Shader defaultSpeedTree8Shader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600231F RID: 8991
		protected abstract RenderPipeline CreatePipeline();

		// Token: 0x06002320 RID: 8992 RVA: 0x0003B0DC File Offset: 0x000392DC
		protected virtual void OnValidate()
		{
			bool flag = RenderPipelineManager.s_CurrentPipelineAsset == this;
			if (flag)
			{
				RenderPipelineManager.CleanupRenderPipeline();
				RenderPipelineManager.PrepareRenderPipeline(this);
			}
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x0003B108 File Offset: 0x00039308
		protected virtual void OnDisable()
		{
			RenderPipelineManager.CleanupRenderPipeline();
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x0003B111 File Offset: 0x00039311
		protected RenderPipelineAsset()
		{
		}
	}
}
