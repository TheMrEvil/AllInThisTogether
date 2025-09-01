using System;
using System.Runtime.CompilerServices;
using Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.Helper.Caching;
using Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.Target;
using UnityEngine;

namespace Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.TargetSelection
{
	// Token: 0x02000003 RID: 3
	public class TargetSelector : MonoBehaviour
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public NotifyTargetFound OnTargetSelected
		{
			[CompilerGenerated]
			get
			{
				return this.<OnTargetSelected>k__BackingField;
			}
		} = new NotifyTargetFound();

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
		public NotifyTargetFound OnTargetLost
		{
			[CompilerGenerated]
			get
			{
				return this.<OnTargetLost>k__BackingField;
			}
		} = new NotifyTargetFound();

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002068 File Offset: 0x00000268
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002070 File Offset: 0x00000270
		public AimAssistTarget Target
		{
			[CompilerGenerated]
			get
			{
				return this.<Target>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Target>k__BackingField = value;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002079 File Offset: 0x00000279
		private void Awake()
		{
			this.CheckPlayerCamera();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002084 File Offset: 0x00000284
		private void FixedUpdate()
		{
			if (!TargetSelector.IsEnabled)
			{
				this.Target = null;
				return;
			}
			AimAssistTarget aimAssistTarget = this.SelectClosestTarget();
			if (aimAssistTarget != null)
			{
				this.NotifyOnTargetFound(aimAssistTarget);
			}
			else
			{
				this.NotifyOnTargetLost();
			}
			this.Target = aimAssistTarget;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020C6 File Offset: 0x000002C6
		private void NotifyOnTargetFound(AimAssistTarget foundTarget)
		{
			if (foundTarget != this.Target)
			{
				NotifyTargetFound onTargetSelected = this.OnTargetSelected;
				if (onTargetSelected == null)
				{
					return;
				}
				onTargetSelected.Invoke(foundTarget);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020E7 File Offset: 0x000002E7
		private void NotifyOnTargetLost()
		{
			if (this.Target != null)
			{
				NotifyTargetFound onTargetLost = this.OnTargetLost;
				if (onTargetLost == null)
				{
					return;
				}
				onTargetLost.Invoke(this.Target);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002110 File Offset: 0x00000310
		private AimAssistTarget SelectClosestTarget()
		{
			AimAssistTarget aimAssistTarget = this.SelectTarget();
			this.selectedTargetStore.ProcessTarget(aimAssistTarget);
			return aimAssistTarget;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002134 File Offset: 0x00000334
		private AimAssistTarget SelectTarget()
		{
			Vector3 forward = this.playerCamera.transform.forward;
			Vector3 origin = this.playerCamera.position + this.playerCamera.forward * this.nearClipDistance;
			RaycastHit raycastHit;
			if (!Physics.SphereCast(origin, this.aimAssistRadius, forward, out raycastHit, this.farClipDistance))
			{
				return null;
			}
			AimAssistTarget aimAssistTarget = this.targetCache.FindOrInsert(raycastHit.collider);
			if (aimAssistTarget)
			{
				return aimAssistTarget;
			}
			RaycastHit raycastHit2;
			if (!Physics.Raycast(origin, forward, out raycastHit2, this.farClipDistance, this.layerMask))
			{
				return null;
			}
			return this.targetCache.FindOrInsert(raycastHit2.collider);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021DF File Offset: 0x000003DF
		private void CheckPlayerCamera()
		{
			if (this.playerCamera)
			{
				return;
			}
			throw new MissingComponentException("Player camera transform is missing for Aim Assist Script.");
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021FC File Offset: 0x000003FC
		public TargetSelector()
		{
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000225C File Offset: 0x0000045C
		// Note: this type is marked as 'beforefieldinit'.
		static TargetSelector()
		{
		}

		// Token: 0x04000001 RID: 1
		[Header("Data for aim assist")]
		[Tooltip("The player's camera that will be aim assisted")]
		public Transform playerCamera;

		// Token: 0x04000002 RID: 2
		[Tooltip("The radius of the aim assist in metres.")]
		public float aimAssistRadius = 0.5f;

		// Token: 0x04000003 RID: 3
		[Tooltip("The near clip distance in metres. Aim assist doesn't work for targets closer than this.")]
		public float nearClipDistance = 0.5f;

		// Token: 0x04000004 RID: 4
		[Tooltip("The far clip distance in metres. Aim assist doesn't work for target further than this. Increasing this takes more computing power.")]
		public float farClipDistance = 50f;

		// Token: 0x04000005 RID: 5
		[Header("Layers")]
		[Tooltip("Layers to take into account during the aim assist.")]
		public LayerMask layerMask;

		// Token: 0x04000006 RID: 6
		[CompilerGenerated]
		private readonly NotifyTargetFound <OnTargetSelected>k__BackingField;

		// Token: 0x04000007 RID: 7
		[CompilerGenerated]
		private readonly NotifyTargetFound <OnTargetLost>k__BackingField;

		// Token: 0x04000008 RID: 8
		[CompilerGenerated]
		private AimAssistTarget <Target>k__BackingField;

		// Token: 0x04000009 RID: 9
		private readonly Cache<AimAssistTarget> targetCache = Cache<AimAssistTarget>.Instance;

		// Token: 0x0400000A RID: 10
		private readonly SelectedTargetStore selectedTargetStore = new SelectedTargetStore();

		// Token: 0x0400000B RID: 11
		public static bool IsEnabled = true;
	}
}
