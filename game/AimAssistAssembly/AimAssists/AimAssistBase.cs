using System;
using Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.Target;
using Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.TargetSelection;
using UnityEngine;

namespace Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.AimAssists
{
	// Token: 0x0200000F RID: 15
	[RequireComponent(typeof(TargetSelector))]
	public abstract class AimAssistBase : MonoBehaviour
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002636 File Offset: 0x00000836
		public AimAssistTarget Target
		{
			get
			{
				return this.targetSelector.Target;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002643 File Offset: 0x00000843
		public float AimAssistRadius
		{
			get
			{
				return this.targetSelector.aimAssistRadius;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002650 File Offset: 0x00000850
		public float NearClipDistance
		{
			get
			{
				return this.targetSelector.nearClipDistance;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000265D File Offset: 0x0000085D
		public float FarClipDistance
		{
			get
			{
				return this.targetSelector.farClipDistance;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000266A File Offset: 0x0000086A
		public Transform PlayerCamera
		{
			get
			{
				return this.targetSelector.playerCamera;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002677 File Offset: 0x00000877
		public NotifyTargetFound OnTargetFound
		{
			get
			{
				return this.targetSelector.OnTargetSelected;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002684 File Offset: 0x00000884
		public NotifyTargetFound OnTargetLost
		{
			get
			{
				return this.targetSelector.OnTargetLost;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002691 File Offset: 0x00000891
		public bool Initialized
		{
			get
			{
				return this.targetSelector != null;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000269F File Offset: 0x0000089F
		protected virtual void Awake()
		{
			this.SetUpTargetSelector();
			this.CheckPlayerCamera();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000026AD File Offset: 0x000008AD
		private void SetUpTargetSelector()
		{
			this.targetSelector = base.GetComponent<TargetSelector>();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000026BB File Offset: 0x000008BB
		private void CheckPlayerCamera()
		{
			if (this.PlayerCamera)
			{
				return;
			}
			throw new MissingComponentException("Player camera transform is missing for Aim Assist Script.");
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000026D5 File Offset: 0x000008D5
		protected AimAssistBase()
		{
		}

		// Token: 0x0400001E RID: 30
		[Header("Master switch")]
		[Tooltip("Enable aim assist")]
		public bool aimAssistEnabled = true;

		// Token: 0x0400001F RID: 31
		private TargetSelector targetSelector;
	}
}
