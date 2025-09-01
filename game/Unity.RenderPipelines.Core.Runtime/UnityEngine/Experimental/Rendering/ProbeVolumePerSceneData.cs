using System;
using System.Collections.Generic;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000017 RID: 23
	[ExecuteAlways]
	[AddComponentMenu("")]
	internal class ProbeVolumePerSceneData : MonoBehaviour, ISerializationCallbackReceiver
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x0000630C File Offset: 0x0000450C
		public void OnAfterDeserialize()
		{
			if (this.serializedAssets == null)
			{
				return;
			}
			this.assets = new Dictionary<ProbeVolumeState, ProbeVolumeAsset>();
			foreach (ProbeVolumePerSceneData.SerializableAssetItem serializableAssetItem in this.serializedAssets)
			{
				this.assets.Add(serializableAssetItem.state, serializableAssetItem.asset);
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00006384 File Offset: 0x00004584
		public void OnBeforeSerialize()
		{
			if (this.assets == null || this.serializedAssets == null)
			{
				return;
			}
			this.serializedAssets.Clear();
			foreach (ProbeVolumeState probeVolumeState in this.assets.Keys)
			{
				ProbeVolumePerSceneData.SerializableAssetItem item;
				item.state = probeVolumeState;
				item.asset = this.assets[probeVolumeState];
				this.serializedAssets.Add(item);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00006418 File Offset: 0x00004618
		internal void StoreAssetForState(ProbeVolumeState state, ProbeVolumeAsset asset)
		{
			this.assets[state] = asset;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00006428 File Offset: 0x00004628
		internal void InvalidateAllAssets()
		{
			foreach (ProbeVolumeAsset probeVolumeAsset in this.assets.Values)
			{
				if (probeVolumeAsset != null)
				{
					ProbeReferenceVolume.instance.AddPendingAssetRemoval(probeVolumeAsset);
				}
			}
			this.assets.Clear();
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00006498 File Offset: 0x00004698
		internal ProbeVolumeAsset GetCurrentStateAsset()
		{
			if (this.assets.ContainsKey(this.m_CurrentState))
			{
				return this.assets[this.m_CurrentState];
			}
			return null;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000064C0 File Offset: 0x000046C0
		internal void QueueAssetLoading()
		{
			ProbeReferenceVolume instance = ProbeReferenceVolume.instance;
			if (this.assets.ContainsKey(this.m_CurrentState) && this.assets[this.m_CurrentState] != null)
			{
				instance.AddPendingAssetLoading(this.assets[this.m_CurrentState]);
				this.m_PreviousState = this.m_CurrentState;
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00006524 File Offset: 0x00004724
		internal void QueueAssetRemoval()
		{
			if (this.assets.ContainsKey(this.m_CurrentState) && this.assets[this.m_CurrentState] != null)
			{
				ProbeReferenceVolume.instance.AddPendingAssetRemoval(this.assets[this.m_CurrentState]);
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00006578 File Offset: 0x00004778
		private void OnEnable()
		{
			this.QueueAssetLoading();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00006580 File Offset: 0x00004780
		private void OnDisable()
		{
			this.QueueAssetRemoval();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00006588 File Offset: 0x00004788
		private void OnDestroy()
		{
			this.QueueAssetRemoval();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00006590 File Offset: 0x00004790
		private void Update()
		{
			this.m_CurrentState = ProbeVolumeState.Default;
			if (this.m_PreviousState != this.m_CurrentState)
			{
				if (this.assets.ContainsKey(this.m_PreviousState) && this.assets[this.m_PreviousState] != null)
				{
					ProbeReferenceVolume.instance.AddPendingAssetRemoval(this.assets[this.m_PreviousState]);
				}
				this.QueueAssetLoading();
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000065FF File Offset: 0x000047FF
		public ProbeVolumePerSceneData()
		{
		}

		// Token: 0x0400009E RID: 158
		internal Dictionary<ProbeVolumeState, ProbeVolumeAsset> assets = new Dictionary<ProbeVolumeState, ProbeVolumeAsset>();

		// Token: 0x0400009F RID: 159
		[SerializeField]
		private List<ProbeVolumePerSceneData.SerializableAssetItem> serializedAssets;

		// Token: 0x040000A0 RID: 160
		private ProbeVolumeState m_CurrentState;

		// Token: 0x040000A1 RID: 161
		private ProbeVolumeState m_PreviousState = ProbeVolumeState.Invalid;

		// Token: 0x02000122 RID: 290
		[Serializable]
		private struct SerializableAssetItem
		{
			// Token: 0x040004BB RID: 1211
			[SerializeField]
			public ProbeVolumeState state;

			// Token: 0x040004BC RID: 1212
			[SerializeField]
			public ProbeVolumeAsset asset;
		}
	}
}
