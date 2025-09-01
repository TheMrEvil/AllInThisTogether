using System;
using System.Collections.Generic;
using AtmosphericHeightFog;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.PostProcessing;

// Token: 0x0200012E RID: 302
public class Scene_Settings : MonoBehaviour
{
	// Token: 0x1700011F RID: 287
	// (get) Token: 0x06000E17 RID: 3607 RVA: 0x00059EDC File Offset: 0x000580DC
	// (set) Token: 0x06000E18 RID: 3608 RVA: 0x00059EFA File Offset: 0x000580FA
	public static Scene_Settings instance
	{
		get
		{
			if (Scene_Settings.instanceRef == null)
			{
				Scene_Settings.instanceRef = UnityEngine.Object.FindObjectOfType<Scene_Settings>();
			}
			return Scene_Settings.instanceRef;
		}
		private set
		{
			Scene_Settings.instanceRef = value;
		}
	}

	// Token: 0x06000E19 RID: 3609 RVA: 0x00059F02 File Offset: 0x00058102
	public void SceneButton()
	{
	}

	// Token: 0x06000E1A RID: 3610 RVA: 0x00059F04 File Offset: 0x00058104
	private void Awake()
	{
		Scene_Settings.instance = this;
		if (this.fogEffect == null)
		{
			this.fogEffect = base.GetComponentInChildren<HeightFogGlobal>();
		}
		this.fogMat = this.fogEffect.presetMaterial;
		this.fogEffect.renderPriority = -99;
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Combine(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.OnGameStateChanged));
		if (this.SceneTerrain == null)
		{
			this.SceneTerrain = UnityEngine.Object.FindObjectOfType<Terrain>();
		}
		if (this.Sun == null)
		{
			Sun sun = UnityEngine.Object.FindObjectOfType<Sun>();
			this.Sun = ((sun != null) ? sun.gameObject : null);
		}
		if (this.InkOcean != null)
		{
			NavMeshObstacle component = this.InkOcean.GetComponent<NavMeshObstacle>();
			if (component != null)
			{
				component.enabled = false;
			}
		}
	}

	// Token: 0x06000E1B RID: 3611 RVA: 0x00059FD8 File Offset: 0x000581D8
	private void Update()
	{
		if (PlayerControl.myInstance != null && this.LoadingCamera.gameObject.activeSelf && !MapManager.InLobbyScene)
		{
			this.LoadingCamera.gameObject.SetActive(false);
		}
		if (PlayerControl.MyCamera != null)
		{
			Vector3 position = PlayerControl.MyCamera.transform.position;
			foreach (ParticleLOD particleLOD in this.FXLODs)
			{
				if (particleLOD != null)
				{
					particleLOD.TickUpdate(position);
				}
			}
		}
	}

	// Token: 0x06000E1C RID: 3612 RVA: 0x0005A088 File Offset: 0x00058288
	private void OnGameStateChanged(GameState from, GameState to)
	{
		if (to == GameState.Reward_PreEnemy)
		{
			this.fogEffect.presetMaterial = PostFXManager.instance.desaturateFog;
			return;
		}
		if (to == GameState.PostRewards)
		{
			this.fogEffect.presetMaterial = this.fogMat;
		}
	}

	// Token: 0x06000E1D RID: 3613 RVA: 0x0005A0BB File Offset: 0x000582BB
	public void OverrideFog(Material mat, int importance)
	{
		if (this.fogOverrideImportance > importance)
		{
			return;
		}
		this.fogEffect.presetMaterial = mat;
		this.fogOverrideImportance = importance;
	}

	// Token: 0x06000E1E RID: 3614 RVA: 0x0005A0DA File Offset: 0x000582DA
	public void ReleaseFog(Material mat)
	{
		if (this.fogEffect.presetMaterial != mat)
		{
			return;
		}
		this.fogEffect.presetMaterial = this.fogMat;
		this.fogOverrideImportance = 0;
	}

	// Token: 0x06000E1F RID: 3615 RVA: 0x0005A108 File Offset: 0x00058308
	public static void AddParticleLOD(ParticleLOD ps)
	{
		if (Scene_Settings.instance == null || Scene_Settings.instance.FXLODs.Contains(ps))
		{
			return;
		}
		Scene_Settings.instance.FXLODs.Add(ps);
	}

	// Token: 0x06000E20 RID: 3616 RVA: 0x0005A13A File Offset: 0x0005833A
	public static void RemoveParticleLOD(ParticleLOD ps)
	{
		if (Scene_Settings.instance == null)
		{
			return;
		}
		Scene_Settings.instance.FXLODs.Remove(ps);
	}

	// Token: 0x06000E21 RID: 3617 RVA: 0x0005A15B File Offset: 0x0005835B
	public static bool IsTerrainObject(GameObject obj)
	{
		return (Scene_Settings.instance != null && Scene_Settings.instance.SceneTerrain != null && Scene_Settings.instance.SceneTerrain.gameObject == obj) || TerrainObject.IsTerrainObject(obj);
	}

	// Token: 0x06000E22 RID: 3618 RVA: 0x0005A19B File Offset: 0x0005839B
	private void OnDestroy()
	{
		if (Scene_Settings.instanceRef == this)
		{
			Scene_Settings.instance = null;
		}
	}

	// Token: 0x06000E23 RID: 3619 RVA: 0x0005A1B0 File Offset: 0x000583B0
	public Scene_Settings()
	{
	}

	// Token: 0x04000B8D RID: 2957
	public Camera LoadingCamera;

	// Token: 0x04000B8E RID: 2958
	public PostProcessProfile ScenePostFX;

	// Token: 0x04000B8F RID: 2959
	public AudioClip AmbientAudio;

	// Token: 0x04000B90 RID: 2960
	public Vector3 MapOrigin;

	// Token: 0x04000B91 RID: 2961
	public Terrain SceneTerrain;

	// Token: 0x04000B92 RID: 2962
	public GameObject Sun;

	// Token: 0x04000B93 RID: 2963
	public GameObject InkOcean;

	// Token: 0x04000B94 RID: 2964
	private Material fogMat;

	// Token: 0x04000B95 RID: 2965
	public HeightFogGlobal fogEffect;

	// Token: 0x04000B96 RID: 2966
	private int fogOverrideImportance;

	// Token: 0x04000B97 RID: 2967
	public int OverrideDrawDistance;

	// Token: 0x04000B98 RID: 2968
	private List<ParticleLOD> FXLODs = new List<ParticleLOD>();

	// Token: 0x04000B99 RID: 2969
	private static Scene_Settings instanceRef;
}
