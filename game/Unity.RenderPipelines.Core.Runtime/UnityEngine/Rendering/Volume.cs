using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering
{
	// Token: 0x020000B6 RID: 182
	[ExecuteAlways]
	[AddComponentMenu("Miscellaneous/Volume")]
	public class Volume : MonoBehaviour, IVolume
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0001C700 File Offset: 0x0001A900
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x0001C708 File Offset: 0x0001A908
		[Tooltip("When enabled, the Volume is applied to the entire Scene.")]
		public bool isGlobal
		{
			get
			{
				return this.m_IsGlobal;
			}
			set
			{
				this.m_IsGlobal = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0001C714 File Offset: 0x0001A914
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x0001C7C0 File Offset: 0x0001A9C0
		public VolumeProfile profile
		{
			get
			{
				if (this.m_InternalProfile == null)
				{
					this.m_InternalProfile = ScriptableObject.CreateInstance<VolumeProfile>();
					if (this.sharedProfile != null)
					{
						this.m_InternalProfile.name = this.sharedProfile.name;
						foreach (VolumeComponent original in this.sharedProfile.components)
						{
							VolumeComponent item = Object.Instantiate<VolumeComponent>(original);
							this.m_InternalProfile.components.Add(item);
						}
					}
				}
				return this.m_InternalProfile;
			}
			set
			{
				this.m_InternalProfile = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x0001C7C9 File Offset: 0x0001A9C9
		public List<Collider> colliders
		{
			get
			{
				return this.m_Colliders;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x0001C7D1 File Offset: 0x0001A9D1
		internal VolumeProfile profileRef
		{
			get
			{
				if (!(this.m_InternalProfile == null))
				{
					return this.m_InternalProfile;
				}
				return this.sharedProfile;
			}
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0001C7EE File Offset: 0x0001A9EE
		public bool HasInstantiatedProfile()
		{
			return this.m_InternalProfile != null;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0001C7FC File Offset: 0x0001A9FC
		private void OnEnable()
		{
			this.m_PreviousLayer = base.gameObject.layer;
			VolumeManager.instance.Register(this, this.m_PreviousLayer);
			base.GetComponents<Collider>(this.m_Colliders);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0001C82C File Offset: 0x0001AA2C
		private void OnDisable()
		{
			VolumeManager.instance.Unregister(this, base.gameObject.layer);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0001C844 File Offset: 0x0001AA44
		private void Update()
		{
			this.UpdateLayer();
			if (this.priority != this.m_PreviousPriority)
			{
				VolumeManager.instance.SetLayerDirty(base.gameObject.layer);
				this.m_PreviousPriority = this.priority;
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001C87C File Offset: 0x0001AA7C
		internal void UpdateLayer()
		{
			int layer = base.gameObject.layer;
			if (layer != this.m_PreviousLayer)
			{
				VolumeManager.instance.UpdateVolumeLayer(this, this.m_PreviousLayer, layer);
				this.m_PreviousLayer = layer;
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001C8B7 File Offset: 0x0001AAB7
		public Volume()
		{
		}

		// Token: 0x04000390 RID: 912
		[SerializeField]
		[FormerlySerializedAs("isGlobal")]
		private bool m_IsGlobal = true;

		// Token: 0x04000391 RID: 913
		[Tooltip("When multiple Volumes affect the same settings, Unity uses this value to determine which Volume to use. A Volume with the highest Priority value takes precedence.")]
		public float priority;

		// Token: 0x04000392 RID: 914
		[Tooltip("Sets the outer distance to start blending from. A value of 0 means no blending and Unity applies the Volume overrides immediately upon entry.")]
		public float blendDistance;

		// Token: 0x04000393 RID: 915
		[Range(0f, 1f)]
		[Tooltip("Sets the total weight of this Volume in the Scene. 0 means no effect and 1 means full effect.")]
		public float weight = 1f;

		// Token: 0x04000394 RID: 916
		public VolumeProfile sharedProfile;

		// Token: 0x04000395 RID: 917
		internal List<Collider> m_Colliders = new List<Collider>();

		// Token: 0x04000396 RID: 918
		private int m_PreviousLayer;

		// Token: 0x04000397 RID: 919
		private float m_PreviousPriority;

		// Token: 0x04000398 RID: 920
		private VolumeProfile m_InternalProfile;
	}
}
