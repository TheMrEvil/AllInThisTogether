using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200005E RID: 94
	public sealed class PostProcessManager
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000E962 File Offset: 0x0000CB62
		public static PostProcessManager instance
		{
			get
			{
				if (PostProcessManager.s_Instance == null)
				{
					PostProcessManager.s_Instance = new PostProcessManager();
				}
				return PostProcessManager.s_Instance;
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000E97C File Offset: 0x0000CB7C
		private PostProcessManager()
		{
			this.m_SortedVolumes = new Dictionary<int, List<PostProcessVolume>>();
			this.m_Volumes = new List<PostProcessVolume>();
			this.m_SortNeeded = new Dictionary<int, bool>();
			this.m_BaseSettings = new List<PostProcessEffectSettings>();
			this.m_TempColliders = new List<Collider>(5);
			this.settingsTypes = new Dictionary<Type, PostProcessAttribute>();
			this.ReloadBaseTypes();
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000E9D8 File Offset: 0x0000CBD8
		private void CleanBaseTypes()
		{
			this.settingsTypes.Clear();
			foreach (PostProcessEffectSettings obj in this.m_BaseSettings)
			{
				RuntimeUtilities.Destroy(obj);
			}
			this.m_BaseSettings.Clear();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000EA40 File Offset: 0x0000CC40
		private void ReloadBaseTypes()
		{
			this.CleanBaseTypes();
			foreach (Type type in from t in RuntimeUtilities.GetAllTypesDerivedFrom<PostProcessEffectSettings>()
			where t.IsDefined(typeof(PostProcessAttribute), false) && !t.IsAbstract
			select t)
			{
				this.settingsTypes.Add(type, type.GetAttribute<PostProcessAttribute>());
				PostProcessEffectSettings postProcessEffectSettings = (PostProcessEffectSettings)ScriptableObject.CreateInstance(type);
				postProcessEffectSettings.SetAllOverridesTo(true, false);
				this.m_BaseSettings.Add(postProcessEffectSettings);
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000EAE4 File Offset: 0x0000CCE4
		public void GetActiveVolumes(PostProcessLayer layer, List<PostProcessVolume> results, bool skipDisabled = true, bool skipZeroWeight = true)
		{
			int value = layer.volumeLayer.value;
			Transform volumeTrigger = layer.volumeTrigger;
			bool flag = volumeTrigger == null;
			Vector3 vector = flag ? Vector3.zero : volumeTrigger.position;
			foreach (PostProcessVolume postProcessVolume in this.GrabVolumes(value))
			{
				if ((!skipDisabled || postProcessVolume.enabled) && !(postProcessVolume.profileRef == null) && (!skipZeroWeight || postProcessVolume.weight > 0f))
				{
					if (postProcessVolume.isGlobal)
					{
						results.Add(postProcessVolume);
					}
					else if (!flag)
					{
						List<Collider> tempColliders = this.m_TempColliders;
						postProcessVolume.GetComponents<Collider>(tempColliders);
						if (tempColliders.Count != 0)
						{
							float num = float.PositiveInfinity;
							foreach (Collider collider in tempColliders)
							{
								if (collider.enabled)
								{
									float sqrMagnitude = ((collider.ClosestPoint(vector) - vector) / 2f).sqrMagnitude;
									if (sqrMagnitude < num)
									{
										num = sqrMagnitude;
									}
								}
							}
							tempColliders.Clear();
							float num2 = postProcessVolume.blendDistance * postProcessVolume.blendDistance;
							if (num <= num2)
							{
								results.Add(postProcessVolume);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000EC90 File Offset: 0x0000CE90
		public PostProcessVolume GetHighestPriorityVolume(PostProcessLayer layer)
		{
			if (layer == null)
			{
				throw new ArgumentNullException("layer");
			}
			return this.GetHighestPriorityVolume(layer.volumeLayer);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000ECB4 File Offset: 0x0000CEB4
		public PostProcessVolume GetHighestPriorityVolume(LayerMask mask)
		{
			float num = float.NegativeInfinity;
			PostProcessVolume result = null;
			List<PostProcessVolume> list;
			if (this.m_SortedVolumes.TryGetValue(mask, out list))
			{
				foreach (PostProcessVolume postProcessVolume in list)
				{
					if (postProcessVolume.priority > num)
					{
						num = postProcessVolume.priority;
						result = postProcessVolume;
					}
				}
			}
			return result;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000ED30 File Offset: 0x0000CF30
		public PostProcessVolume QuickVolume(int layer, float priority, params PostProcessEffectSettings[] settings)
		{
			PostProcessVolume postProcessVolume = new GameObject
			{
				name = "Quick Volume",
				layer = layer,
				hideFlags = HideFlags.HideAndDontSave
			}.AddComponent<PostProcessVolume>();
			postProcessVolume.priority = priority;
			postProcessVolume.isGlobal = true;
			PostProcessProfile profile = postProcessVolume.profile;
			foreach (PostProcessEffectSettings effect in settings)
			{
				profile.AddSettings(effect);
			}
			return postProcessVolume;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000ED98 File Offset: 0x0000CF98
		internal void SetLayerDirty(int layer)
		{
			foreach (KeyValuePair<int, List<PostProcessVolume>> keyValuePair in this.m_SortedVolumes)
			{
				int key = keyValuePair.Key;
				if ((key & 1 << layer) != 0)
				{
					this.m_SortNeeded[key] = true;
				}
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000EE04 File Offset: 0x0000D004
		internal void UpdateVolumeLayer(PostProcessVolume volume, int prevLayer, int newLayer)
		{
			this.Unregister(volume, prevLayer);
			this.Unregister(volume, newLayer);
			this.Register(volume, newLayer);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000EE20 File Offset: 0x0000D020
		private void Register(PostProcessVolume volume, int layer)
		{
			this.m_Volumes.Add(volume);
			foreach (KeyValuePair<int, List<PostProcessVolume>> keyValuePair in this.m_SortedVolumes)
			{
				if ((keyValuePair.Key & 1 << layer) != 0)
				{
					keyValuePair.Value.Add(volume);
				}
			}
			this.SetLayerDirty(layer);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000EE9C File Offset: 0x0000D09C
		internal void Register(PostProcessVolume volume)
		{
			int layer = volume.gameObject.layer;
			this.Register(volume, layer);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000EEC0 File Offset: 0x0000D0C0
		private void Unregister(PostProcessVolume volume, int layer)
		{
			this.m_Volumes.Remove(volume);
			foreach (KeyValuePair<int, List<PostProcessVolume>> keyValuePair in this.m_SortedVolumes)
			{
				if ((keyValuePair.Key & 1 << layer) != 0)
				{
					keyValuePair.Value.Remove(volume);
				}
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000EF38 File Offset: 0x0000D138
		internal void Unregister(PostProcessVolume volume)
		{
			this.Unregister(volume, volume.previousLayer);
			this.Unregister(volume, volume.gameObject.layer);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000EF5C File Offset: 0x0000D15C
		private void ReplaceData(PostProcessLayer postProcessLayer)
		{
			foreach (PostProcessEffectSettings postProcessEffectSettings in this.m_BaseSettings)
			{
				PostProcessEffectSettings settings = postProcessLayer.GetBundle(postProcessEffectSettings.GetType()).settings;
				int count = postProcessEffectSettings.parameters.Count;
				for (int i = 0; i < count; i++)
				{
					settings.parameters[i].SetValue(postProcessEffectSettings.parameters[i]);
				}
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000EFF8 File Offset: 0x0000D1F8
		internal void UpdateSettings(PostProcessLayer postProcessLayer, Camera camera)
		{
			this.ReplaceData(postProcessLayer);
			int value = postProcessLayer.volumeLayer.value;
			Transform volumeTrigger = postProcessLayer.volumeTrigger;
			bool flag = volumeTrigger == null;
			Vector3 vector = flag ? Vector3.zero : volumeTrigger.position;
			foreach (PostProcessVolume postProcessVolume in this.GrabVolumes(value))
			{
				if (postProcessVolume.enabled && !(postProcessVolume.profileRef == null) && postProcessVolume.weight > 0f)
				{
					List<PostProcessEffectSettings> settings = postProcessVolume.profileRef.settings;
					if (postProcessVolume.isGlobal)
					{
						postProcessLayer.OverrideSettings(settings, Mathf.Clamp01(postProcessVolume.weight));
					}
					else if (!flag)
					{
						List<Collider> tempColliders = this.m_TempColliders;
						postProcessVolume.GetComponents<Collider>(tempColliders);
						if (tempColliders.Count != 0)
						{
							float num = float.PositiveInfinity;
							foreach (Collider collider in tempColliders)
							{
								if (collider.enabled)
								{
									float sqrMagnitude = ((collider.ClosestPoint(vector) - vector) / 2f).sqrMagnitude;
									if (sqrMagnitude < num)
									{
										num = sqrMagnitude;
									}
								}
							}
							tempColliders.Clear();
							float num2 = postProcessVolume.blendDistance * postProcessVolume.blendDistance;
							if (num <= num2)
							{
								float num3 = 1f;
								if (num2 > 0f)
								{
									num3 = 1f - num / num2;
								}
								postProcessLayer.OverrideSettings(settings, num3 * Mathf.Clamp01(postProcessVolume.weight));
							}
						}
					}
				}
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000F1E8 File Offset: 0x0000D3E8
		private List<PostProcessVolume> GrabVolumes(LayerMask mask)
		{
			List<PostProcessVolume> list;
			if (!this.m_SortedVolumes.TryGetValue(mask, out list))
			{
				list = new List<PostProcessVolume>();
				foreach (PostProcessVolume postProcessVolume in this.m_Volumes)
				{
					if ((mask & 1 << postProcessVolume.gameObject.layer) != 0)
					{
						list.Add(postProcessVolume);
						this.m_SortNeeded[mask] = true;
					}
				}
				this.m_SortedVolumes.Add(mask, list);
			}
			bool flag;
			if (this.m_SortNeeded.TryGetValue(mask, out flag) && flag)
			{
				this.m_SortNeeded[mask] = false;
				PostProcessManager.SortByPriority(list);
			}
			return list;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000F2C4 File Offset: 0x0000D4C4
		private static void SortByPriority(List<PostProcessVolume> volumes)
		{
			for (int i = 1; i < volumes.Count; i++)
			{
				PostProcessVolume postProcessVolume = volumes[i];
				int num = i - 1;
				while (num >= 0 && volumes[num].priority > postProcessVolume.priority)
				{
					volumes[num + 1] = volumes[num];
					num--;
				}
				volumes[num + 1] = postProcessVolume;
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000F326 File Offset: 0x0000D526
		private static bool IsVolumeRenderedByCamera(PostProcessVolume volume, Camera camera)
		{
			return true;
		}

		// Token: 0x040001D2 RID: 466
		private static PostProcessManager s_Instance;

		// Token: 0x040001D3 RID: 467
		private const int k_MaxLayerCount = 32;

		// Token: 0x040001D4 RID: 468
		private readonly Dictionary<int, List<PostProcessVolume>> m_SortedVolumes;

		// Token: 0x040001D5 RID: 469
		private readonly List<PostProcessVolume> m_Volumes;

		// Token: 0x040001D6 RID: 470
		private readonly Dictionary<int, bool> m_SortNeeded;

		// Token: 0x040001D7 RID: 471
		private readonly List<PostProcessEffectSettings> m_BaseSettings;

		// Token: 0x040001D8 RID: 472
		private readonly List<Collider> m_TempColliders;

		// Token: 0x040001D9 RID: 473
		public readonly Dictionary<Type, PostProcessAttribute> settingsTypes;

		// Token: 0x0200008E RID: 142
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000283 RID: 643 RVA: 0x0001329F File Offset: 0x0001149F
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000284 RID: 644 RVA: 0x000132AB File Offset: 0x000114AB
			public <>c()
			{
			}

			// Token: 0x06000285 RID: 645 RVA: 0x000132B3 File Offset: 0x000114B3
			internal bool <ReloadBaseTypes>b__12_0(Type t)
			{
				return t.IsDefined(typeof(PostProcessAttribute), false) && !t.IsAbstract;
			}

			// Token: 0x04000350 RID: 848
			public static readonly PostProcessManager.<>c <>9 = new PostProcessManager.<>c();

			// Token: 0x04000351 RID: 849
			public static Func<Type, bool> <>9__12_0;
		}
	}
}
