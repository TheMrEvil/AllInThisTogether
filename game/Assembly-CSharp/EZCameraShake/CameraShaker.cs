using System;
using System.Collections.Generic;
using UnityEngine;

namespace EZCameraShake
{
	// Token: 0x020003BB RID: 955
	[AddComponentMenu("EZ Camera Shake/Camera Shaker")]
	public class CameraShaker : MonoBehaviour
	{
		// Token: 0x06001F84 RID: 8068 RVA: 0x000BBFC0 File Offset: 0x000BA1C0
		private void Start()
		{
			if (CameraShaker.Instance != null)
			{
				return;
			}
			CameraShaker.Instance = this;
			if (!CameraShaker.instanceList.ContainsKey(base.gameObject.name))
			{
				CameraShaker.instanceList.Add(base.gameObject.name, this);
			}
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x000BC010 File Offset: 0x000BA210
		private void Update()
		{
			this.posAddShake = Vector3.zero;
			this.rotAddShake = Vector3.zero;
			int num = 0;
			while (num < this.cameraShakeInstances.Count && num < this.cameraShakeInstances.Count)
			{
				CameraShakeInstance cameraShakeInstance = this.cameraShakeInstances[num];
				if (cameraShakeInstance.CurrentState == CameraShakeState.Inactive && cameraShakeInstance.DeleteOnInactive)
				{
					this.cameraShakeInstances.RemoveAt(num);
					num--;
				}
				else if (cameraShakeInstance.CurrentState != CameraShakeState.Inactive)
				{
					this.posAddShake += CameraUtilities.MultiplyVectors(cameraShakeInstance.UpdateShake(), cameraShakeInstance.PositionInfluence);
					this.rotAddShake += CameraUtilities.MultiplyVectors(cameraShakeInstance.UpdateShake(), cameraShakeInstance.RotationInfluence);
				}
				num++;
			}
			if (Settings.GetBool(SystemSetting.ScreenShake, true))
			{
				base.transform.localPosition = this.posAddShake + this.RestPositionOffset;
				base.transform.localEulerAngles = this.rotAddShake + this.RestRotationOffset;
			}
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x000BC120 File Offset: 0x000BA320
		public static CameraShaker GetInstance(string name)
		{
			CameraShaker result;
			if (CameraShaker.instanceList.TryGetValue(name, out result))
			{
				return result;
			}
			Debug.LogError("CameraShake " + name + " not found!");
			return null;
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x000BC154 File Offset: 0x000BA354
		public CameraShakeInstance Shake(CameraShakeInstance shake)
		{
			this.cameraShakeInstances.Add(shake);
			return shake;
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x000BC164 File Offset: 0x000BA364
		public CameraShakeInstance ShakeOnce(float magnitude, float roughness, float fadeInTime, float fadeOutTime)
		{
			CameraShakeInstance cameraShakeInstance = new CameraShakeInstance(magnitude, roughness, fadeInTime, fadeOutTime);
			cameraShakeInstance.PositionInfluence = this.DefaultPosInfluence;
			cameraShakeInstance.RotationInfluence = this.DefaultRotInfluence;
			this.cameraShakeInstances.Add(cameraShakeInstance);
			return cameraShakeInstance;
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x000BC1A4 File Offset: 0x000BA3A4
		public CameraShakeInstance ShakeOnce(CameraEffectNode node)
		{
			CameraShakeInstance cameraShakeInstance = new CameraShakeInstance(node);
			cameraShakeInstance.PositionInfluence = this.DefaultPosInfluence;
			cameraShakeInstance.RotationInfluence = this.DefaultRotInfluence;
			this.cameraShakeInstances.Add(cameraShakeInstance);
			return cameraShakeInstance;
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x000BC1E0 File Offset: 0x000BA3E0
		public CameraShakeInstance ShakeOnce(float magnitude, float roughness, float fadeInTime, float fadeOutTime, Vector3 posInfluence, Vector3 rotInfluence)
		{
			CameraShakeInstance cameraShakeInstance = new CameraShakeInstance(magnitude, roughness, fadeInTime, fadeOutTime);
			cameraShakeInstance.PositionInfluence = posInfluence;
			cameraShakeInstance.RotationInfluence = rotInfluence;
			this.cameraShakeInstances.Add(cameraShakeInstance);
			return cameraShakeInstance;
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x000BC218 File Offset: 0x000BA418
		public CameraShakeInstance StartShake(float magnitude, float roughness, float fadeInTime)
		{
			CameraShakeInstance cameraShakeInstance = new CameraShakeInstance(magnitude, roughness);
			cameraShakeInstance.PositionInfluence = this.DefaultPosInfluence;
			cameraShakeInstance.RotationInfluence = this.DefaultRotInfluence;
			cameraShakeInstance.StartFadeIn(fadeInTime);
			this.cameraShakeInstances.Add(cameraShakeInstance);
			return cameraShakeInstance;
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x000BC25C File Offset: 0x000BA45C
		public CameraShakeInstance StartShake(float magnitude, float roughness, float fadeInTime, Vector3 posInfluence, Vector3 rotInfluence)
		{
			CameraShakeInstance cameraShakeInstance = new CameraShakeInstance(magnitude, roughness);
			cameraShakeInstance.PositionInfluence = posInfluence;
			cameraShakeInstance.RotationInfluence = rotInfluence;
			cameraShakeInstance.StartFadeIn(fadeInTime);
			this.cameraShakeInstances.Add(cameraShakeInstance);
			return cameraShakeInstance;
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06001F8D RID: 8077 RVA: 0x000BC295 File Offset: 0x000BA495
		public List<CameraShakeInstance> ShakeInstances
		{
			get
			{
				return new List<CameraShakeInstance>(this.cameraShakeInstances);
			}
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x000BC2A2 File Offset: 0x000BA4A2
		private void OnDestroy()
		{
			CameraShaker.instanceList.Remove(base.gameObject.name);
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x000BC2BC File Offset: 0x000BA4BC
		public CameraShaker()
		{
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x000BC342 File Offset: 0x000BA542
		// Note: this type is marked as 'beforefieldinit'.
		static CameraShaker()
		{
		}

		// Token: 0x04001FC5 RID: 8133
		public static CameraShaker Instance;

		// Token: 0x04001FC6 RID: 8134
		private static Dictionary<string, CameraShaker> instanceList = new Dictionary<string, CameraShaker>();

		// Token: 0x04001FC7 RID: 8135
		public Vector3 DefaultPosInfluence = new Vector3(0.15f, 0.15f, 0.15f);

		// Token: 0x04001FC8 RID: 8136
		public Vector3 DefaultRotInfluence = new Vector3(1f, 1f, 1f);

		// Token: 0x04001FC9 RID: 8137
		public Vector3 RestPositionOffset = new Vector3(0f, 0f, 0f);

		// Token: 0x04001FCA RID: 8138
		public Vector3 RestRotationOffset = new Vector3(0f, 0f, 0f);

		// Token: 0x04001FCB RID: 8139
		private Vector3 posAddShake;

		// Token: 0x04001FCC RID: 8140
		private Vector3 rotAddShake;

		// Token: 0x04001FCD RID: 8141
		private List<CameraShakeInstance> cameraShakeInstances = new List<CameraShakeInstance>();
	}
}
