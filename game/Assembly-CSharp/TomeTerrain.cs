using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200025E RID: 606
[RequireComponent(typeof(Terrain))]
public class TomeTerrain : MonoBehaviour
{
	// Token: 0x17000175 RID: 373
	// (get) Token: 0x0600185A RID: 6234 RVA: 0x000984B0 File Offset: 0x000966B0
	public static TomeTerrain instance
	{
		get
		{
			if (TomeTerrain._i != null)
			{
				return TomeTerrain._i;
			}
			TomeTerrain._i = UnityEngine.Object.FindObjectOfType<TomeTerrain>();
			if (TomeTerrain._i == null)
			{
				return TomeTerrain._i;
			}
			TomeTerrain._i.T = TomeTerrain._i.GetComponent<Terrain>();
			return TomeTerrain._i;
		}
	}

	// Token: 0x0600185B RID: 6235 RVA: 0x00098506 File Offset: 0x00096706
	private void Awake()
	{
		this.T = base.GetComponent<Terrain>();
		TomeTerrain._i = this;
		this.LoadGreyValues();
		Settings.OnSystemSettingsChanged = (Action<SystemSetting>)Delegate.Combine(Settings.OnSystemSettingsChanged, new Action<SystemSetting>(this.OnSettingsChanged));
		this.UpdateDetailSettings();
	}

	// Token: 0x0600185C RID: 6236 RVA: 0x00098546 File Offset: 0x00096746
	public void TestGrey()
	{
		this.SetGreyValues(this.GreyValues.x, this.GreyValues.y);
	}

	// Token: 0x0600185D RID: 6237 RVA: 0x00098564 File Offset: 0x00096764
	private void LoadGreyValues()
	{
		if (this.MType == TomeTerrain.MapType.Forest)
		{
			this.SetGreyValues(0f, 1f);
			return;
		}
		if (this.MType == TomeTerrain.MapType.Desert)
		{
			this.SetGreyValues(0.075f, 1f);
			return;
		}
		if (this.MType == TomeTerrain.MapType.Winter)
		{
			this.SetGreyValues(0.25f, 1f);
		}
	}

	// Token: 0x0600185E RID: 6238 RVA: 0x000985BD File Offset: 0x000967BD
	private void SetGreyValues(float min, float max)
	{
		Shader.SetGlobalFloat(TomeTerrain.TerrainGreyMin, min);
		Shader.SetGlobalFloat(TomeTerrain.TerrainGreyMax, max);
	}

	// Token: 0x0600185F RID: 6239 RVA: 0x000985D8 File Offset: 0x000967D8
	public static void ApplyLayer(float genreProgress)
	{
		if (TomeTerrain.instance == null)
		{
			return;
		}
		TomeTerrain.TerrainPreset terrainPreset = TomeTerrain.instance.Default;
		if (genreProgress > 0.6f)
		{
			terrainPreset = TomeTerrain.instance.EndGame;
		}
		else if (genreProgress > 0.3f)
		{
			terrainPreset = TomeTerrain.instance.MidGame;
		}
		terrainPreset.Apply(TomeTerrain.instance.T);
		TomeTerrain.instance.LoadGreyValues();
	}

	// Token: 0x06001860 RID: 6240 RVA: 0x00098640 File Offset: 0x00096840
	private void OnSettingsChanged(SystemSetting setting)
	{
		if (setting == SystemSetting.TerrainDetails)
		{
			this.UpdateDetailSettings();
		}
	}

	// Token: 0x06001861 RID: 6241 RVA: 0x00098650 File Offset: 0x00096850
	private void UpdateDetailSettings()
	{
		switch (Settings.GetInt(SystemSetting.TerrainDetails, 2))
		{
		case 0:
			this.T.detailObjectDensity = 0f;
			return;
		case 1:
			this.T.detailObjectDensity = 0.5f;
			return;
		case 2:
			this.T.detailObjectDensity = 1f;
			return;
		default:
			return;
		}
	}

	// Token: 0x06001862 RID: 6242 RVA: 0x000986AB File Offset: 0x000968AB
	private void OnDestroy()
	{
		Settings.OnSystemSettingsChanged = (Action<SystemSetting>)Delegate.Remove(Settings.OnSystemSettingsChanged, new Action<SystemSetting>(this.OnSettingsChanged));
	}

	// Token: 0x06001863 RID: 6243 RVA: 0x000986CD File Offset: 0x000968CD
	public TomeTerrain()
	{
	}

	// Token: 0x06001864 RID: 6244 RVA: 0x000986D5 File Offset: 0x000968D5
	// Note: this type is marked as 'beforefieldinit'.
	static TomeTerrain()
	{
	}

	// Token: 0x0400182F RID: 6191
	[HideInInspector]
	public Terrain T;

	// Token: 0x04001830 RID: 6192
	private static TomeTerrain _i;

	// Token: 0x04001831 RID: 6193
	public TomeTerrain.MapType MType;

	// Token: 0x04001832 RID: 6194
	public TomeTerrain.TerrainPreset Default;

	// Token: 0x04001833 RID: 6195
	public TomeTerrain.TerrainPreset MidGame;

	// Token: 0x04001834 RID: 6196
	public TomeTerrain.TerrainPreset EndGame;

	// Token: 0x04001835 RID: 6197
	private static readonly int TerrainGreyMax = Shader.PropertyToID("_TerrainGreyMax");

	// Token: 0x04001836 RID: 6198
	private static readonly int TerrainGreyMin = Shader.PropertyToID("_TerrainGreyMin");

	// Token: 0x04001837 RID: 6199
	public Vector2 GreyValues;

	// Token: 0x02000628 RID: 1576
	[Serializable]
	public class TerrainPreset
	{
		// Token: 0x0600278B RID: 10123 RVA: 0x000D6538 File Offset: 0x000D4738
		public void SaveTerrain()
		{
			TomeTerrain instance = TomeTerrain.instance;
			Terrain terrain = (instance != null) ? instance.T : null;
			if (terrain == null)
			{
				Debug.LogError("No Terrain found for TomeTerrain instance");
				return;
			}
			TerrainLayer[] terrainLayers = terrain.terrainData.terrainLayers;
			for (int i = 0; i < 8; i++)
			{
				TerrainLayer l = null;
				if (i < terrainLayers.Length)
				{
					l = terrainLayers[i];
				}
				this.SaveLayer(i, l);
			}
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x000D6598 File Offset: 0x000D4798
		public void DebugApply()
		{
			TomeTerrain instance = TomeTerrain.instance;
			Terrain terrain = (instance != null) ? instance.T : null;
			if (terrain == null)
			{
				Debug.LogError("No Terrain found for TomeTerrain instance");
				return;
			}
			this.Apply(terrain);
			TomeTerrain.instance.LoadGreyValues();
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x000D65DC File Offset: 0x000D47DC
		public void Apply(Terrain t)
		{
			if (t == null)
			{
				return;
			}
			TerrainLayer[] terrainLayers = t.terrainData.terrainLayers;
			for (int i = 0; i < terrainLayers.Length; i++)
			{
				TerrainLayer layer = this.GetLayer(i);
				terrainLayers[i] = layer;
			}
			t.terrainData.terrainLayers = terrainLayers;
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x000D6628 File Offset: 0x000D4828
		private void SaveLayer(int i, TerrainLayer l)
		{
			switch (i)
			{
			case 0:
				this.L1 = l;
				return;
			case 1:
				this.L2 = l;
				return;
			case 2:
				this.L3 = l;
				return;
			case 3:
				this.L4 = l;
				return;
			case 4:
				this.L5 = l;
				return;
			case 5:
				this.L6 = l;
				return;
			case 6:
				this.L7 = l;
				return;
			case 7:
				this.L8 = l;
				return;
			default:
				throw new SwitchExpressionException(i);
			}
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x000D66C8 File Offset: 0x000D48C8
		private TerrainLayer GetLayer(int index)
		{
			if (index < 0 || index >= 8)
			{
				return null;
			}
			TerrainLayer result;
			switch (index)
			{
			case 0:
				result = this.L1;
				break;
			case 1:
				result = this.L2;
				break;
			case 2:
				result = this.L3;
				break;
			case 3:
				result = this.L4;
				break;
			case 4:
				result = this.L5;
				break;
			case 5:
				result = this.L6;
				break;
			case 6:
				result = this.L7;
				break;
			case 7:
				result = this.L8;
				break;
			default:
				throw new SwitchExpressionException(index);
			}
			return result;
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x000D675C File Offset: 0x000D495C
		public TerrainPreset()
		{
		}

		// Token: 0x04002A0E RID: 10766
		public TerrainLayer L1;

		// Token: 0x04002A0F RID: 10767
		public TerrainLayer L2;

		// Token: 0x04002A10 RID: 10768
		public TerrainLayer L3;

		// Token: 0x04002A11 RID: 10769
		public TerrainLayer L4;

		// Token: 0x04002A12 RID: 10770
		public TerrainLayer L5;

		// Token: 0x04002A13 RID: 10771
		public TerrainLayer L6;

		// Token: 0x04002A14 RID: 10772
		public TerrainLayer L7;

		// Token: 0x04002A15 RID: 10773
		public TerrainLayer L8;
	}

	// Token: 0x02000629 RID: 1577
	public enum MapType
	{
		// Token: 0x04002A17 RID: 10775
		Forest,
		// Token: 0x04002A18 RID: 10776
		Desert,
		// Token: 0x04002A19 RID: 10777
		Winter
	}
}
