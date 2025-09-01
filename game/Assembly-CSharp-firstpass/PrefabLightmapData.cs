using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000016 RID: 22
[ExecuteAlways]
public class PrefabLightmapData : MonoBehaviour
{
	// Token: 0x0600004F RID: 79 RVA: 0x000033A3 File Offset: 0x000015A3
	private void Start()
	{
		this.Init();
	}

	// Token: 0x06000050 RID: 80 RVA: 0x000033AC File Offset: 0x000015AC
	private void Init()
	{
		if (this.m_RendererInfo == null || this.m_RendererInfo.Length == 0)
		{
			return;
		}
		LightmapData[] lightmaps = LightmapSettings.lightmaps;
		int[] array = new int[this.m_Lightmaps.Length];
		int num = lightmaps.Length;
		List<LightmapData> list = new List<LightmapData>();
		for (int i = 0; i < this.m_Lightmaps.Length; i++)
		{
			bool flag = false;
			for (int j = 0; j < lightmaps.Length; j++)
			{
				if (this.m_Lightmaps[i] == lightmaps[j].lightmapColor)
				{
					flag = true;
					array[i] = j;
				}
			}
			if (!flag)
			{
				array[i] = num;
				LightmapData item = new LightmapData
				{
					lightmapColor = this.m_Lightmaps[i],
					lightmapDir = ((this.m_LightmapsDir.Length == this.m_Lightmaps.Length) ? this.m_LightmapsDir[i] : null),
					shadowMask = ((this.m_ShadowMasks.Length == this.m_Lightmaps.Length) ? this.m_ShadowMasks[i] : null)
				};
				list.Add(item);
				num++;
			}
		}
		if (this.DisableLights)
		{
			PrefabLightmapData.LightInfo[] lightInfo = this.m_LightInfo;
			for (int k = 0; k < lightInfo.Length; k++)
			{
				lightInfo[k].light.enabled = false;
			}
		}
		LightmapData[] array2 = new LightmapData[num];
		lightmaps.CopyTo(array2, 0);
		list.ToArray().CopyTo(array2, lightmaps.Length);
		bool flag2 = true;
		Texture2D[] lightmapsDir = this.m_LightmapsDir;
		for (int k = 0; k < lightmapsDir.Length; k++)
		{
			if (lightmapsDir[k] == null)
			{
				flag2 = false;
				break;
			}
		}
		LightmapSettings.lightmapsMode = ((this.m_LightmapsDir.Length == this.m_Lightmaps.Length && flag2) ? LightmapsMode.CombinedDirectional : LightmapsMode.NonDirectional);
		this.ApplyRendererInfo(this.m_RendererInfo, array, this.m_LightInfo);
		LightmapSettings.lightmaps = array2;
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00003572 File Offset: 0x00001772
	private void OnEnable()
	{
		SceneManager.sceneLoaded += this.OnSceneLoaded;
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00003585 File Offset: 0x00001785
	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		this.Init();
	}

	// Token: 0x06000053 RID: 83 RVA: 0x0000358D File Offset: 0x0000178D
	private void OnDisable()
	{
		SceneManager.sceneLoaded -= this.OnSceneLoaded;
	}

	// Token: 0x06000054 RID: 84 RVA: 0x000035A0 File Offset: 0x000017A0
	private void ApplyRendererInfo(PrefabLightmapData.RendererInfo[] infos, int[] lightmapOffsetIndex, PrefabLightmapData.LightInfo[] lightsInfo)
	{
		foreach (PrefabLightmapData.RendererInfo rendererInfo in infos)
		{
			rendererInfo.renderer.lightmapIndex = lightmapOffsetIndex[rendererInfo.lightmapIndex];
			rendererInfo.renderer.lightmapScaleOffset = rendererInfo.lightmapOffsetScale;
			if (this.releaseShaders)
			{
				Material[] sharedMaterials = rendererInfo.renderer.sharedMaterials;
				for (int j = 0; j < sharedMaterials.Length; j++)
				{
					if (sharedMaterials[j] != null && Shader.Find(sharedMaterials[j].shader.name) != null)
					{
						sharedMaterials[j].shader = Shader.Find(sharedMaterials[j].shader.name);
					}
				}
			}
		}
		for (int k = 0; k < lightsInfo.Length; k++)
		{
			LightBakingOutput bakingOutput = default(LightBakingOutput);
			bakingOutput.isBaked = true;
			bakingOutput.lightmapBakeType = (LightmapBakeType)lightsInfo[k].lightmapBaketype;
			bakingOutput.mixedLightingMode = (MixedLightingMode)lightsInfo[k].mixedLightingMode;
			lightsInfo[k].light.bakingOutput = bakingOutput;
		}
	}

	// Token: 0x06000055 RID: 85 RVA: 0x000036AC File Offset: 0x000018AC
	public PrefabLightmapData()
	{
	}

	// Token: 0x04000049 RID: 73
	[Tooltip("Reassigns shaders when applying the baked lightmaps. Might conflict with some shaders like transparent HDRP.")]
	public bool releaseShaders = true;

	// Token: 0x0400004A RID: 74
	public bool DisableLights;

	// Token: 0x0400004B RID: 75
	[SerializeField]
	private PrefabLightmapData.RendererInfo[] m_RendererInfo;

	// Token: 0x0400004C RID: 76
	[SerializeField]
	private Texture2D[] m_Lightmaps;

	// Token: 0x0400004D RID: 77
	[SerializeField]
	private Texture2D[] m_LightmapsDir;

	// Token: 0x0400004E RID: 78
	[SerializeField]
	private Texture2D[] m_ShadowMasks;

	// Token: 0x0400004F RID: 79
	[SerializeField]
	private PrefabLightmapData.LightInfo[] m_LightInfo;

	// Token: 0x02000190 RID: 400
	[Serializable]
	private struct RendererInfo
	{
		// Token: 0x04000C5B RID: 3163
		public Renderer renderer;

		// Token: 0x04000C5C RID: 3164
		public int lightmapIndex;

		// Token: 0x04000C5D RID: 3165
		public Vector4 lightmapOffsetScale;
	}

	// Token: 0x02000191 RID: 401
	[Serializable]
	private struct LightInfo
	{
		// Token: 0x04000C5E RID: 3166
		public Light light;

		// Token: 0x04000C5F RID: 3167
		public int lightmapBaketype;

		// Token: 0x04000C60 RID: 3168
		public int mixedLightingMode;
	}
}
