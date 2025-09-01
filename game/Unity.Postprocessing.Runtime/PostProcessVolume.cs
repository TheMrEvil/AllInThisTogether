using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000062 RID: 98
	[ExecuteAlways]
	[AddComponentMenu("Rendering/Post-process Volume", 1001)]
	public sealed class PostProcessVolume : MonoBehaviour
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000FC18 File Offset: 0x0000DE18
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x0000FCAC File Offset: 0x0000DEAC
		public PostProcessProfile profile
		{
			get
			{
				if (this.m_InternalProfile == null)
				{
					this.m_InternalProfile = ScriptableObject.CreateInstance<PostProcessProfile>();
					if (this.sharedProfile != null)
					{
						foreach (PostProcessEffectSettings original in this.sharedProfile.settings)
						{
							PostProcessEffectSettings item = Object.Instantiate<PostProcessEffectSettings>(original);
							this.m_InternalProfile.settings.Add(item);
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

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000FCB5 File Offset: 0x0000DEB5
		internal PostProcessProfile profileRef
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

		// Token: 0x060001E2 RID: 482 RVA: 0x0000FCD2 File Offset: 0x0000DED2
		public bool HasInstantiatedProfile()
		{
			return this.m_InternalProfile != null;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000FCE0 File Offset: 0x0000DEE0
		internal int previousLayer
		{
			get
			{
				return this.m_PreviousLayer;
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000FCE8 File Offset: 0x0000DEE8
		private void OnEnable()
		{
			PostProcessManager.instance.Register(this);
			this.m_PreviousLayer = base.gameObject.layer;
			this.m_TempColliders = new List<Collider>();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000FD11 File Offset: 0x0000DF11
		private void OnDisable()
		{
			PostProcessManager.instance.Unregister(this);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000FD20 File Offset: 0x0000DF20
		private void Update()
		{
			int layer = base.gameObject.layer;
			if (layer != this.m_PreviousLayer)
			{
				PostProcessManager.instance.UpdateVolumeLayer(this, this.m_PreviousLayer, layer);
				this.m_PreviousLayer = layer;
			}
			if (this.priority != this.m_PreviousPriority)
			{
				PostProcessManager.instance.SetLayerDirty(layer);
				this.m_PreviousPriority = this.priority;
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000FD80 File Offset: 0x0000DF80
		private void OnDrawGizmos()
		{
			List<Collider> tempColliders = this.m_TempColliders;
			base.GetComponents<Collider>(tempColliders);
			if (this.isGlobal || tempColliders == null)
			{
				return;
			}
			Vector3 lossyScale = base.transform.lossyScale;
			Vector3 vector = new Vector3(1f / lossyScale.x, 1f / lossyScale.y, 1f / lossyScale.z);
			Gizmos.matrix = Matrix4x4.TRS(base.transform.position, base.transform.rotation, lossyScale);
			foreach (Collider collider in tempColliders)
			{
				if (collider.enabled)
				{
					Type type = collider.GetType();
					if (type == typeof(BoxCollider))
					{
						BoxCollider boxCollider = (BoxCollider)collider;
						Gizmos.DrawCube(boxCollider.center, boxCollider.size);
						Gizmos.DrawWireCube(boxCollider.center, boxCollider.size + 4f * this.blendDistance * vector);
					}
					else if (type == typeof(SphereCollider))
					{
						SphereCollider sphereCollider = (SphereCollider)collider;
						Gizmos.DrawSphere(sphereCollider.center, sphereCollider.radius);
						Gizmos.DrawWireSphere(sphereCollider.center, sphereCollider.radius + vector.x * this.blendDistance * 2f);
					}
					else if (type == typeof(MeshCollider))
					{
						MeshCollider meshCollider = (MeshCollider)collider;
						if (!meshCollider.convex)
						{
							meshCollider.convex = true;
						}
						Gizmos.DrawMesh(meshCollider.sharedMesh);
						Gizmos.DrawWireMesh(meshCollider.sharedMesh, Vector3.zero, Quaternion.identity, Vector3.one + 4f * this.blendDistance * vector);
					}
				}
			}
			tempColliders.Clear();
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000FF88 File Offset: 0x0000E188
		public PostProcessVolume()
		{
		}

		// Token: 0x04000203 RID: 515
		public PostProcessProfile sharedProfile;

		// Token: 0x04000204 RID: 516
		[Tooltip("Check this box to mark this volume as global. This volume's Profile will be applied to the whole Scene.")]
		public bool isGlobal;

		// Token: 0x04000205 RID: 517
		[Min(0f)]
		[Tooltip("The distance (from the attached Collider) to start blending from. A value of 0 means there will be no blending and the Volume overrides will be applied immediatly upon entry to the attached Collider.")]
		public float blendDistance;

		// Token: 0x04000206 RID: 518
		[Range(0f, 1f)]
		[Tooltip("The total weight of this Volume in the Scene. A value of 0 signifies that it will have no effect, 1 signifies full effect.")]
		public float weight = 1f;

		// Token: 0x04000207 RID: 519
		[Tooltip("The volume priority in the stack. A higher value means higher priority. Negative values are supported.")]
		public float priority;

		// Token: 0x04000208 RID: 520
		private int m_PreviousLayer;

		// Token: 0x04000209 RID: 521
		private float m_PreviousPriority;

		// Token: 0x0400020A RID: 522
		private List<Collider> m_TempColliders;

		// Token: 0x0400020B RID: 523
		private PostProcessProfile m_InternalProfile;
	}
}
