using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x02000075 RID: 117
	public class RenderManager : IManager, IDisposable, IValid
	{
		// Token: 0x060001AA RID: 426 RVA: 0x0000F736 File Offset: 0x0000D936
		public void Initialize()
		{
			this.Dispose();
			MagicaManager.afterDelayedDelegate = (MagicaManager.UpdateMethod)Delegate.Combine(MagicaManager.afterDelayedDelegate, new MagicaManager.UpdateMethod(this.PreRenderingUpdate));
			this.isValid = true;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000F765 File Offset: 0x0000D965
		public void EnterdEditMode()
		{
			this.Dispose();
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000F770 File Offset: 0x0000D970
		public void Dispose()
		{
			this.isValid = false;
			Dictionary<int, RenderData> obj = this.renderDataDict;
			lock (obj)
			{
				foreach (RenderData renderData in this.renderDataDict.Values)
				{
					if (renderData != null)
					{
						renderData.Dispose();
					}
				}
			}
			this.renderDataDict.Clear();
			MagicaManager.afterDelayedDelegate = (MagicaManager.UpdateMethod)Delegate.Remove(MagicaManager.afterDelayedDelegate, new MagicaManager.UpdateMethod(this.PreRenderingUpdate));
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000F824 File Offset: 0x0000DA24
		public bool IsValid()
		{
			return this.isValid;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000F82C File Offset: 0x0000DA2C
		public int AddRenderer(Renderer ren)
		{
			if (!this.isValid)
			{
				return 0;
			}
			int instanceID = ren.GetInstanceID();
			Dictionary<int, RenderData> obj = this.renderDataDict;
			lock (obj)
			{
				if (!this.renderDataDict.ContainsKey(instanceID))
				{
					RenderData renderData = new RenderData();
					renderData.Initialize(ren);
					this.renderDataDict.Add(instanceID, renderData);
				}
				this.renderDataDict[instanceID].AddReferenceCount();
			}
			return instanceID;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000F8B4 File Offset: 0x0000DAB4
		public bool RemoveRenderer(int handle)
		{
			if (!this.isValid)
			{
				return false;
			}
			bool result = false;
			Dictionary<int, RenderData> obj = this.renderDataDict;
			lock (obj)
			{
				if (this.renderDataDict.ContainsKey(handle))
				{
					RenderData renderData = this.renderDataDict[handle];
					if (renderData.RemoveReferenceCount() == 0)
					{
						renderData.Dispose();
						this.renderDataDict.Remove(handle);
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000F934 File Offset: 0x0000DB34
		public RenderData GetRendererData(int handle)
		{
			if (!this.isValid)
			{
				return null;
			}
			Dictionary<int, RenderData> obj = this.renderDataDict;
			RenderData result;
			lock (obj)
			{
				if (this.renderDataDict.ContainsKey(handle))
				{
					result = this.renderDataDict[handle];
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000F998 File Offset: 0x0000DB98
		public void StartUse(ClothProcess cprocess, int handle)
		{
			RenderData rendererData = this.GetRendererData(handle);
			if (rendererData == null)
			{
				return;
			}
			rendererData.StartUse(cprocess);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000F9AC File Offset: 0x0000DBAC
		public void EndUse(ClothProcess cprocess, int handle)
		{
			RenderData rendererData = this.GetRendererData(handle);
			if (rendererData == null)
			{
				return;
			}
			rendererData.EndUse(cprocess);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000F9C0 File Offset: 0x0000DBC0
		private void PreRenderingUpdate()
		{
			foreach (RenderData renderData in this.renderDataDict.Values)
			{
				if (renderData != null)
				{
					renderData.WriteMesh();
				}
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000FA1C File Offset: 0x0000DC1C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("Render Manager. Count({0})", this.renderDataDict.Count));
			foreach (KeyValuePair<int, RenderData> keyValuePair in this.renderDataDict)
			{
				stringBuilder.AppendLine(keyValuePair.Value.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000FAA8 File Offset: 0x0000DCA8
		public RenderManager()
		{
		}

		// Token: 0x040002F5 RID: 757
		private Dictionary<int, RenderData> renderDataDict = new Dictionary<int, RenderData>();

		// Token: 0x040002F6 RID: 758
		private bool isValid;
	}
}
