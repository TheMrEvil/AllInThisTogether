using System;
using System.Collections.Generic;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x0200005D RID: 93
	public class MagicaCloth : ClothBehaviour, IValid
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000D7E4 File Offset: 0x0000B9E4
		public ClothSerializeData SerializeData
		{
			get
			{
				return this.serializeData;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000D7EC File Offset: 0x0000B9EC
		public ClothProcess Process
		{
			get
			{
				this.process.cloth = this;
				return this.process;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000D800 File Offset: 0x0000BA00
		public Transform ClothTransform
		{
			get
			{
				return base.transform;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000D808 File Offset: 0x0000BA08
		public MagicaCloth SyncCloth
		{
			get
			{
				return this.SerializeData.selfCollisionConstraint.GetSyncPartner();
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000D81A File Offset: 0x0000BA1A
		public bool IsValid()
		{
			return MagicaManager.IsPlaying() && this.Process.IsValid() && this.Process.TeamId > 0;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000D840 File Offset: 0x0000BA40
		private void OnValidate()
		{
			this.Process.DataUpdate();
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000D84D File Offset: 0x0000BA4D
		private void Awake()
		{
			this.Process.Init();
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000D85A File Offset: 0x0000BA5A
		private void OnEnable()
		{
			this.Process.StartUse();
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000D867 File Offset: 0x0000BA67
		private void OnDisable()
		{
			this.Process.EndUse();
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000D874 File Offset: 0x0000BA74
		private void Start()
		{
			this.Process.AutoBuild();
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000D882 File Offset: 0x0000BA82
		private void OnDestroy()
		{
			this.Process.Dispose();
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000D88F File Offset: 0x0000BA8F
		public override int GetHashCode()
		{
			return this.SerializeData.GetHashCode() + (base.isActiveAndEnabled ? base.GetInstanceID() : 0);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000D8AE File Offset: 0x0000BAAE
		public ClothSerializeData2 GetSerializeData2()
		{
			return this.serializeData2;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000D8B6 File Offset: 0x0000BAB6
		public void Initialize()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			this.Process.Init();
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000D8CB File Offset: 0x0000BACB
		public void DisableAutoBuild()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			this.Process.SetState(6, true);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000D8E4 File Offset: 0x0000BAE4
		public bool BuildAndRun()
		{
			if (!Application.isPlaying)
			{
				return false;
			}
			this.DisableAutoBuild();
			if (this.Process.IsState(4))
			{
				object obj = "Already built.:" + base.name;
				Develop.LogError(obj);
				return false;
			}
			if (!this.Process.GenerateInitialization())
			{
				return false;
			}
			if (this.serializeData.clothType == ClothProcess.ClothType.BoneCloth && !this.Process.GenerateBoneClothSelection())
			{
				return false;
			}
			this.Process.StartBuild();
			return true;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000D964 File Offset: 0x0000BB64
		public void ReplaceTransform(Dictionary<string, Transform> targetTransformDict)
		{
			HashSet<Transform> hashSet = new HashSet<Transform>();
			this.Process.GetUsedTransform(hashSet);
			Dictionary<int, Transform> dictionary = new Dictionary<int, Transform>();
			foreach (Transform transform in hashSet)
			{
				if (targetTransformDict.ContainsKey(transform.name))
				{
					dictionary.Add(transform.GetInstanceID(), targetTransformDict[transform.name]);
				}
			}
			this.Process.ReplaceTransform(dictionary);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000D9F8 File Offset: 0x0000BBF8
		public void SetParameterChange()
		{
			if (this.IsValid())
			{
				this.Process.DataUpdate();
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000DA10 File Offset: 0x0000BC10
		public void SetTimeScale(float timeScale)
		{
			if (this.IsValid())
			{
				TeamManager.TeamData teamData = MagicaManager.Team.GetTeamData(this.Process.TeamId);
				teamData.timeScale = Mathf.Clamp01(timeScale);
				MagicaManager.Team.SetTeamData(this.Process.TeamId, teamData);
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000DA5E File Offset: 0x0000BC5E
		public float GetTimeScale()
		{
			if (this.IsValid())
			{
				return MagicaManager.Team.GetTeamData(this.Process.TeamId).timeScale;
			}
			return 1f;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000DA88 File Offset: 0x0000BC88
		public void ResetCloth()
		{
			if (this.IsValid())
			{
				TeamManager.TeamData teamData = MagicaManager.Team.GetTeamData(this.Process.TeamId);
				teamData.flag.SetBits(2, true);
				teamData.flag.SetBits(3, true);
				MagicaManager.Team.SetTeamData(this.Process.TeamId, teamData);
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000DAE8 File Offset: 0x0000BCE8
		public Vector3 GetCenterPosition()
		{
			if (this.IsValid())
			{
				InertiaConstraint.CenterData centerData = MagicaManager.Team.GetCenterData(this.Process.TeamId);
				return this.ClothTransform.TransformPoint(centerData.frameLocalPosition);
			}
			return Vector3.zero;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000DB2F File Offset: 0x0000BD2F
		public MagicaCloth()
		{
		}

		// Token: 0x04000228 RID: 552
		[SerializeField]
		private ClothSerializeData serializeData = new ClothSerializeData();

		// Token: 0x04000229 RID: 553
		[SerializeField]
		private ClothSerializeData2 serializeData2 = new ClothSerializeData2();

		// Token: 0x0400022A RID: 554
		private ClothProcess process = new ClothProcess();

		// Token: 0x0400022B RID: 555
		public Action<bool> OnBuildComplete;
	}
}
