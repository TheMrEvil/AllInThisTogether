using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000018 RID: 24
	[Serializable]
	public class ProbeVolumeSceneData : ISerializationCallbackReceiver
	{
		// Token: 0x060000BF RID: 191 RVA: 0x0000661D File Offset: 0x0000481D
		private string GetSceneGUID(Scene scene)
		{
			return (string)ProbeVolumeSceneData.s_SceneGUID.GetValue(scene);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00006634 File Offset: 0x00004834
		public ProbeVolumeSceneData(Object parentAsset, string parentSceneDataPropertyName)
		{
			this.parentAsset = parentAsset;
			this.parentSceneDataPropertyName = parentSceneDataPropertyName;
			this.sceneBounds = new Dictionary<string, Bounds>();
			this.hasProbeVolumes = new Dictionary<string, bool>();
			this.sceneProfiles = new Dictionary<string, ProbeReferenceVolumeProfile>();
			this.sceneBakingSettings = new Dictionary<string, ProbeVolumeBakingProcessSettings>();
			this.bakingSets = new List<ProbeVolumeSceneData.BakingSet>();
			this.serializedBounds = new List<ProbeVolumeSceneData.SerializableBoundItem>();
			this.serializedHasVolumes = new List<ProbeVolumeSceneData.SerializableHasPVItem>();
			this.serializedProfiles = new List<ProbeVolumeSceneData.SerializablePVProfile>();
			this.serializedBakeSettings = new List<ProbeVolumeSceneData.SerializablePVBakeSettings>();
			this.UpdateBakingSets();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000066BE File Offset: 0x000048BE
		public void SetParentObject(Object parent, string parentSceneDataPropertyName)
		{
			this.parentAsset = parent;
			this.parentSceneDataPropertyName = parentSceneDataPropertyName;
			this.UpdateBakingSets();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000066D4 File Offset: 0x000048D4
		public void OnAfterDeserialize()
		{
			if (this.serializedBounds == null || this.serializedHasVolumes == null || this.serializedProfiles == null || this.serializedBakeSettings == null)
			{
				return;
			}
			this.sceneBounds = new Dictionary<string, Bounds>();
			this.hasProbeVolumes = new Dictionary<string, bool>();
			this.sceneProfiles = new Dictionary<string, ProbeReferenceVolumeProfile>();
			this.sceneBakingSettings = new Dictionary<string, ProbeVolumeBakingProcessSettings>();
			this.bakingSets = new List<ProbeVolumeSceneData.BakingSet>();
			foreach (ProbeVolumeSceneData.SerializableBoundItem serializableBoundItem in this.serializedBounds)
			{
				this.sceneBounds.Add(serializableBoundItem.sceneGUID, serializableBoundItem.bounds);
			}
			foreach (ProbeVolumeSceneData.SerializableHasPVItem serializableHasPVItem in this.serializedHasVolumes)
			{
				this.hasProbeVolumes.Add(serializableHasPVItem.sceneGUID, serializableHasPVItem.hasProbeVolumes);
			}
			foreach (ProbeVolumeSceneData.SerializablePVProfile serializablePVProfile in this.serializedProfiles)
			{
				this.sceneProfiles.Add(serializablePVProfile.sceneGUID, serializablePVProfile.profile);
			}
			foreach (ProbeVolumeSceneData.SerializablePVBakeSettings serializablePVBakeSettings in this.serializedBakeSettings)
			{
				this.sceneBakingSettings.Add(serializablePVBakeSettings.sceneGUID, serializablePVBakeSettings.settings);
			}
			foreach (ProbeVolumeSceneData.BakingSet item in this.serializedBakingSets)
			{
				this.bakingSets.Add(item);
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000068D8 File Offset: 0x00004AD8
		private void UpdateBakingSets()
		{
			foreach (ProbeVolumeSceneData.BakingSet bakingSet in this.serializedBakingSets)
			{
				if (bakingSet.profile == null)
				{
					this.InitializeBakingSet(bakingSet, bakingSet.name);
				}
			}
			if (this.bakingSets.Count == 0)
			{
				this.CreateNewBakingSet("Default").sceneGUIDs = (from s in this.serializedProfiles
				select s.sceneGUID).ToList<string>();
			}
			this.SyncBakingSetSettings();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00006994 File Offset: 0x00004B94
		public void OnBeforeSerialize()
		{
			if (this.sceneBounds == null || this.hasProbeVolumes == null || this.sceneBakingSettings == null || this.sceneProfiles == null || this.serializedBounds == null || this.serializedHasVolumes == null || this.serializedBakeSettings == null || this.serializedProfiles == null || this.serializedBakingSets == null)
			{
				return;
			}
			this.serializedBounds.Clear();
			this.serializedHasVolumes.Clear();
			this.serializedProfiles.Clear();
			this.serializedBakeSettings.Clear();
			this.serializedBakingSets.Clear();
			foreach (string text in this.sceneBounds.Keys)
			{
				ProbeVolumeSceneData.SerializableBoundItem item;
				item.sceneGUID = text;
				item.bounds = this.sceneBounds[text];
				this.serializedBounds.Add(item);
			}
			foreach (string text2 in this.hasProbeVolumes.Keys)
			{
				ProbeVolumeSceneData.SerializableHasPVItem item2;
				item2.sceneGUID = text2;
				item2.hasProbeVolumes = this.hasProbeVolumes[text2];
				this.serializedHasVolumes.Add(item2);
			}
			foreach (string text3 in this.sceneBakingSettings.Keys)
			{
				ProbeVolumeSceneData.SerializablePVBakeSettings item3;
				item3.sceneGUID = text3;
				item3.settings = this.sceneBakingSettings[text3];
				this.serializedBakeSettings.Add(item3);
			}
			foreach (string text4 in this.sceneProfiles.Keys)
			{
				ProbeVolumeSceneData.SerializablePVProfile item4;
				item4.sceneGUID = text4;
				item4.profile = this.sceneProfiles[text4];
				this.serializedProfiles.Add(item4);
			}
			foreach (ProbeVolumeSceneData.BakingSet item5 in this.bakingSets)
			{
				this.serializedBakingSets.Add(item5);
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00006C1C File Offset: 0x00004E1C
		internal ProbeVolumeSceneData.BakingSet CreateNewBakingSet(string name)
		{
			ProbeVolumeSceneData.BakingSet bakingSet = new ProbeVolumeSceneData.BakingSet();
			this.InitializeBakingSet(bakingSet, name);
			this.bakingSets.Add(bakingSet);
			return bakingSet;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00006C44 File Offset: 0x00004E44
		private void InitializeBakingSet(ProbeVolumeSceneData.BakingSet set, string name)
		{
			ProbeReferenceVolumeProfile profile = ScriptableObject.CreateInstance<ProbeReferenceVolumeProfile>();
			set.name = name;
			set.profile = profile;
			set.settings = new ProbeVolumeBakingProcessSettings
			{
				dilationSettings = new ProbeDilationSettings
				{
					enableDilation = true,
					dilationDistance = 1f,
					dilationValidityThreshold = 0.25f,
					dilationIterations = 1,
					squaredDistWeighting = true
				},
				virtualOffsetSettings = new VirtualOffsetSettings
				{
					useVirtualOffset = true,
					outOfGeoOffset = 0.01f,
					searchMultiplier = 0.2f
				}
			};
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00006CE4 File Offset: 0x00004EE4
		internal void SyncBakingSetSettings()
		{
			foreach (ProbeVolumeSceneData.BakingSet bakingSet in this.bakingSets)
			{
				foreach (string key in bakingSet.sceneGUIDs)
				{
					this.sceneBakingSettings[key] = bakingSet.settings;
					this.sceneProfiles[key] = bakingSet.profile;
				}
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00006D90 File Offset: 0x00004F90
		// Note: this type is marked as 'beforefieldinit'.
		static ProbeVolumeSceneData()
		{
		}

		// Token: 0x040000A2 RID: 162
		private static PropertyInfo s_SceneGUID = typeof(Scene).GetProperty("guid", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x040000A3 RID: 163
		[SerializeField]
		private List<ProbeVolumeSceneData.SerializableBoundItem> serializedBounds;

		// Token: 0x040000A4 RID: 164
		[SerializeField]
		private List<ProbeVolumeSceneData.SerializableHasPVItem> serializedHasVolumes;

		// Token: 0x040000A5 RID: 165
		[SerializeField]
		private List<ProbeVolumeSceneData.SerializablePVProfile> serializedProfiles;

		// Token: 0x040000A6 RID: 166
		[SerializeField]
		private List<ProbeVolumeSceneData.SerializablePVBakeSettings> serializedBakeSettings;

		// Token: 0x040000A7 RID: 167
		[SerializeField]
		private List<ProbeVolumeSceneData.BakingSet> serializedBakingSets;

		// Token: 0x040000A8 RID: 168
		internal Object parentAsset;

		// Token: 0x040000A9 RID: 169
		internal string parentSceneDataPropertyName;

		// Token: 0x040000AA RID: 170
		public Dictionary<string, Bounds> sceneBounds;

		// Token: 0x040000AB RID: 171
		internal Dictionary<string, bool> hasProbeVolumes;

		// Token: 0x040000AC RID: 172
		internal Dictionary<string, ProbeReferenceVolumeProfile> sceneProfiles;

		// Token: 0x040000AD RID: 173
		internal Dictionary<string, ProbeVolumeBakingProcessSettings> sceneBakingSettings;

		// Token: 0x040000AE RID: 174
		internal List<ProbeVolumeSceneData.BakingSet> bakingSets;

		// Token: 0x02000123 RID: 291
		[Serializable]
		private struct SerializableBoundItem
		{
			// Token: 0x040004BD RID: 1213
			[SerializeField]
			public string sceneGUID;

			// Token: 0x040004BE RID: 1214
			[SerializeField]
			public Bounds bounds;
		}

		// Token: 0x02000124 RID: 292
		[Serializable]
		private struct SerializableHasPVItem
		{
			// Token: 0x040004BF RID: 1215
			[SerializeField]
			public string sceneGUID;

			// Token: 0x040004C0 RID: 1216
			[SerializeField]
			public bool hasProbeVolumes;
		}

		// Token: 0x02000125 RID: 293
		[Serializable]
		private struct SerializablePVProfile
		{
			// Token: 0x040004C1 RID: 1217
			[SerializeField]
			public string sceneGUID;

			// Token: 0x040004C2 RID: 1218
			[SerializeField]
			public ProbeReferenceVolumeProfile profile;
		}

		// Token: 0x02000126 RID: 294
		[Serializable]
		private struct SerializablePVBakeSettings
		{
			// Token: 0x040004C3 RID: 1219
			[SerializeField]
			public string sceneGUID;

			// Token: 0x040004C4 RID: 1220
			[SerializeField]
			public ProbeVolumeBakingProcessSettings settings;
		}

		// Token: 0x02000127 RID: 295
		[Serializable]
		internal class BakingSet
		{
			// Token: 0x06000809 RID: 2057 RVA: 0x000226C2 File Offset: 0x000208C2
			public BakingSet()
			{
			}

			// Token: 0x040004C5 RID: 1221
			public string name;

			// Token: 0x040004C6 RID: 1222
			public List<string> sceneGUIDs = new List<string>();

			// Token: 0x040004C7 RID: 1223
			public ProbeVolumeBakingProcessSettings settings;

			// Token: 0x040004C8 RID: 1224
			public ProbeReferenceVolumeProfile profile;
		}

		// Token: 0x02000128 RID: 296
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600080A RID: 2058 RVA: 0x000226D5 File Offset: 0x000208D5
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600080B RID: 2059 RVA: 0x000226E1 File Offset: 0x000208E1
			public <>c()
			{
			}

			// Token: 0x0600080C RID: 2060 RVA: 0x000226E9 File Offset: 0x000208E9
			internal string <UpdateBakingSets>b__22_0(ProbeVolumeSceneData.SerializablePVProfile s)
			{
				return s.sceneGUID;
			}

			// Token: 0x040004C9 RID: 1225
			public static readonly ProbeVolumeSceneData.<>c <>9 = new ProbeVolumeSceneData.<>c();

			// Token: 0x040004CA RID: 1226
			public static Func<ProbeVolumeSceneData.SerializablePVProfile, string> <>9__22_0;
		}
	}
}
