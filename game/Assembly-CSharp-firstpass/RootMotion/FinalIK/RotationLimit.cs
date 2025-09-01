using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x0200010A RID: 266
	public abstract class RotationLimit : MonoBehaviour
	{
		// Token: 0x06000BE2 RID: 3042 RVA: 0x000501C4 File Offset: 0x0004E3C4
		public void SetDefaultLocalRotation()
		{
			this.defaultLocalRotation = base.transform.localRotation;
			this.defaultLocalRotationSet = true;
			this.defaultLocalRotationOverride = false;
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x000501E5 File Offset: 0x0004E3E5
		public void SetDefaultLocalRotation(Quaternion localRotation)
		{
			this.defaultLocalRotation = localRotation;
			this.defaultLocalRotationSet = true;
			this.defaultLocalRotationOverride = true;
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x000501FC File Offset: 0x0004E3FC
		public Quaternion GetLimitedLocalRotation(Quaternion localRotation, out bool changed)
		{
			if (!this.initiated)
			{
				this.Awake();
			}
			Quaternion quaternion = Quaternion.Inverse(this.defaultLocalRotation) * localRotation;
			Quaternion quaternion2 = this.LimitRotation(quaternion);
			quaternion2 = Quaternion.Normalize(quaternion2);
			changed = (quaternion2 != quaternion);
			if (!changed)
			{
				return localRotation;
			}
			return this.defaultLocalRotation * quaternion2;
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00050254 File Offset: 0x0004E454
		public bool Apply()
		{
			bool result = false;
			base.transform.localRotation = this.GetLimitedLocalRotation(base.transform.localRotation, out result);
			return result;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00050282 File Offset: 0x0004E482
		public void Disable()
		{
			if (this.initiated)
			{
				base.enabled = false;
				return;
			}
			this.Awake();
			base.enabled = false;
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x000502A1 File Offset: 0x0004E4A1
		public Vector3 secondaryAxis
		{
			get
			{
				return new Vector3(this.axis.y, this.axis.z, this.axis.x);
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x000502C9 File Offset: 0x0004E4C9
		public Vector3 crossAxis
		{
			get
			{
				return Vector3.Cross(this.axis, this.secondaryAxis);
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x000502DC File Offset: 0x0004E4DC
		// (set) Token: 0x06000BEA RID: 3050 RVA: 0x000502E4 File Offset: 0x0004E4E4
		public bool defaultLocalRotationOverride
		{
			[CompilerGenerated]
			get
			{
				return this.<defaultLocalRotationOverride>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<defaultLocalRotationOverride>k__BackingField = value;
			}
		}

		// Token: 0x06000BEB RID: 3051
		protected abstract Quaternion LimitRotation(Quaternion rotation);

		// Token: 0x06000BEC RID: 3052 RVA: 0x000502ED File Offset: 0x0004E4ED
		private void Awake()
		{
			if (!this.defaultLocalRotationSet)
			{
				this.SetDefaultLocalRotation();
			}
			if (this.axis == Vector3.zero)
			{
				Debug.LogError("Axis is Vector3.zero.");
			}
			this.initiated = true;
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00050320 File Offset: 0x0004E520
		private void LateUpdate()
		{
			this.Apply();
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00050329 File Offset: 0x0004E529
		public void LogWarning(string message)
		{
			Warning.Log(message, base.transform, false);
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00050338 File Offset: 0x0004E538
		protected static Quaternion Limit1DOF(Quaternion rotation, Vector3 axis)
		{
			return Quaternion.FromToRotation(rotation * axis, axis) * rotation;
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00050350 File Offset: 0x0004E550
		protected static Quaternion LimitTwist(Quaternion rotation, Vector3 axis, Vector3 orthoAxis, float twistLimit)
		{
			twistLimit = Mathf.Clamp(twistLimit, 0f, 180f);
			if (twistLimit >= 180f)
			{
				return rotation;
			}
			Vector3 vector = rotation * axis;
			Vector3 toDirection = orthoAxis;
			Vector3.OrthoNormalize(ref vector, ref toDirection);
			Vector3 fromDirection = rotation * orthoAxis;
			Vector3.OrthoNormalize(ref vector, ref fromDirection);
			Quaternion quaternion = Quaternion.FromToRotation(fromDirection, toDirection) * rotation;
			if (twistLimit <= 0f)
			{
				return quaternion;
			}
			return Quaternion.RotateTowards(quaternion, rotation, twistLimit);
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x000503BD File Offset: 0x0004E5BD
		protected static float GetOrthogonalAngle(Vector3 v1, Vector3 v2, Vector3 normal)
		{
			Vector3.OrthoNormalize(ref normal, ref v1);
			Vector3.OrthoNormalize(ref normal, ref v2);
			return Vector3.Angle(v1, v2);
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x000503D8 File Offset: 0x0004E5D8
		protected RotationLimit()
		{
		}

		// Token: 0x04000946 RID: 2374
		public Vector3 axis = Vector3.forward;

		// Token: 0x04000947 RID: 2375
		[HideInInspector]
		public Quaternion defaultLocalRotation;

		// Token: 0x04000948 RID: 2376
		[CompilerGenerated]
		private bool <defaultLocalRotationOverride>k__BackingField;

		// Token: 0x04000949 RID: 2377
		private bool initiated;

		// Token: 0x0400094A RID: 2378
		private bool applicationQuit;

		// Token: 0x0400094B RID: 2379
		private bool defaultLocalRotationSet;
	}
}
