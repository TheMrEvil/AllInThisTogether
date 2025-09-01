using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FIMSpace.FTail
{
	// Token: 0x02000067 RID: 103
	[AddComponentMenu("FImpossible Creations/Tail Animator Utilities/Tail Animator Wind")]
	public class TailAnimatorWind : MonoBehaviour, IDropHandler, IEventSystemHandler, IFHierarchyIcon
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0001FC23 File Offset: 0x0001DE23
		public string EditorIconPath
		{
			get
			{
				return "Tail Animator/TailAnimatorWindIconSmall";
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0001FC2A File Offset: 0x0001DE2A
		public void OnDrop(PointerEventData data)
		{
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0001FC2C File Offset: 0x0001DE2C
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x0001FC33 File Offset: 0x0001DE33
		public static TailAnimatorWind Instance
		{
			[CompilerGenerated]
			get
			{
				return TailAnimatorWind.<Instance>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				TailAnimatorWind.<Instance>k__BackingField = value;
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0001FC3B File Offset: 0x0001DE3B
		private void Awake()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			TailAnimatorWind.Instance = this;
			if (this.persistThroughAllScenes)
			{
				UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0001FC5E File Offset: 0x0001DE5E
		public void OnValidate()
		{
			TailAnimatorWind.Instance = this;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0001FC66 File Offset: 0x0001DE66
		private void Update()
		{
			if (this.frameOffset > 0)
			{
				this.frameOffset--;
				return;
			}
			this.ComputeWind();
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001FC86 File Offset: 0x0001DE86
		public static void Refresh()
		{
			if (TailAnimatorWind.Instance == null)
			{
				Debug.Log("[Tail Animator Wind] No Tail Animator Wind component on the scene!");
				Debug.LogWarning("[Tail Animator Wind] No Tail Animator Wind component on the scene!");
			}
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0001FCAC File Offset: 0x0001DEAC
		public void AffectTailWithWind(TailAnimator2 t)
		{
			if (!t.UseWind)
			{
				return;
			}
			if (t.WindEffectPower <= 0f)
			{
				return;
			}
			if (t.TailSegments.Count <= 0)
			{
				return;
			}
			float num = 1f;
			if (this.powerDependOnTailLength)
			{
				num = t._TC_TailLength * t.TailSegments[0].transform.lossyScale.z / 5f;
				if (t.TailSegments.Count > 3)
				{
					num *= Mathf.Lerp(0.7f, 3f, (float)t.TailSegments.Count / 14f);
				}
			}
			if (t.WindWorldNoisePower > 0f)
			{
				float num2 = this.worldTurbSpeed;
				if (this.SyncWithUnityWindZone)
				{
					num2 *= this.SyncWithUnityWindZone.windTurbulence * this.UnityWindZoneTurbMul;
				}
				float num3 = 0.5f + Mathf.Sin(Time.time * num2 + t.TailSegments[0].ProceduralPosition.x * this.worldTurbScale) / 2f + (0.5f + Mathf.Cos(Time.time * num2 + t.TailSegments[0].ProceduralPosition.z * this.worldTurbScale) / 2f);
				num += num3 * this.worldTurb * t.WindWorldNoisePower;
			}
			num *= t.WindEffectPower;
			if (t.WindTurbulencePower > 0f)
			{
				t.WindEffect = new Vector3(this.targetWind.x * num + this.finalAddTurbulence.x * t.WindTurbulencePower, this.targetWind.y * num + this.finalAddTurbulence.y * t.WindTurbulencePower, this.targetWind.z * num + this.finalAddTurbulence.z * t.WindTurbulencePower);
				return;
			}
			t.WindEffect = new Vector3(this.targetWind.x * num, this.targetWind.y * num, this.targetWind.z * num);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0001FEB8 File Offset: 0x0001E0B8
		private void Start()
		{
			int num = 10;
			this.randNumbers = new float[num];
			this.randTimes = new float[num];
			this.randSpeeds = new float[num];
			for (int i = 0; i < 10; i++)
			{
				this.randNumbers[i] = UnityEngine.Random.Range(-1000f, 1000f);
				this.randTimes[i] = UnityEngine.Random.Range(-1000f, 1000f);
				this.randSpeeds[i] = UnityEngine.Random.Range(0.18f, 0.7f);
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0001FF40 File Offset: 0x0001E140
		private void ComputeWind()
		{
			Vector3 target;
			if (this.SyncWithUnityWindZone)
			{
				target = this.SyncWithUnityWindZone.transform.forward * this.SyncWithUnityWindZone.windMain * this.UnityWindZonePowerMul;
				base.transform.rotation = this.SyncWithUnityWindZone.transform.rotation;
			}
			else if (this.overrideWind != Vector3.zero)
			{
				target = this.overrideWind;
			}
			else
			{
				for (int i = 0; i < 4; i++)
				{
					this.randTimes[i] += Time.deltaTime * this.randSpeeds[i] * this.turbulenceSpeed;
				}
				Quaternion quaternion = this.windOrientation;
				float num = -1f + Mathf.PerlinNoise(this.randTimes[0], 256f + this.randTimes[1]) * 2f;
				float y = -1f + Mathf.PerlinNoise(-this.randTimes[1], 55f + this.randTimes[2]) * 2f;
				float num2 = -1f + Mathf.PerlinNoise(-this.randTimes[3], 55f + this.randTimes[0]) * 2f;
				quaternion *= Quaternion.Euler(new Vector3(0f, y, 0f) * this.changesPower);
				quaternion = Quaternion.Euler(num * (this.changesPower / 6f), quaternion.eulerAngles.y, num2 * (this.changesPower / 6f));
				this.smoothWindOrient = this.smoothWindOrient.SmoothDampRotation(quaternion, ref this.smoothWindOrientHelper, 1f - this.rapidness, Time.deltaTime);
				base.transform.rotation = this.smoothWindOrient;
				target = this.smoothWindOrient * Vector3.forward;
			}
			this.smoothAddTurbulence = Vector3.SmoothDamp(this.smoothAddTurbulence, this.GetAddTurbulence() * this.additionalTurbulence, ref this.addTurbHelper, 0.05f, float.PositiveInfinity, Time.deltaTime);
			this.smoothWind = Vector3.SmoothDamp(this.smoothWind, target, ref this.windVeloHelper, 0.1f, float.PositiveInfinity, Time.deltaTime);
			for (int j = 7; j < 10; j++)
			{
				this.randTimes[j] += Time.deltaTime * this.randSpeeds[j] * this.turbulenceSpeed;
			}
			float num3 = this.power * 0.015f;
			num3 *= 0.5f + Mathf.PerlinNoise(this.randTimes[7] * 2f, 25f + this.randTimes[8] * 0.5f);
			this.finalAddTurbulence = this.smoothAddTurbulence * num3;
			this.targetWind = this.smoothWind * num3;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00020214 File Offset: 0x0001E414
		private Vector3 GetAddTurbulence()
		{
			float num = this.additionalTurbSpeed;
			if (this.SyncWithUnityWindZone)
			{
				num *= this.SyncWithUnityWindZone.windTurbulence * this.UnityWindZoneTurbMul;
			}
			for (int i = 4; i < 7; i++)
			{
				this.randTimes[i] += Time.deltaTime * this.randSpeeds[i] * num;
			}
			float x = -1f + Mathf.PerlinNoise(this.randTimes[4] + 7.123f, -2.324f + Time.time * 0.24f) * 2f;
			float y = -1f + Mathf.PerlinNoise(this.randTimes[5] - 4.7523f, -25.324f + Time.time * 0.54f) * 2f;
			float z = -1f + Mathf.PerlinNoise(this.randTimes[6] + 1.123f, -63.324f + Time.time * -0.49f) * 2f;
			return new Vector3(x, y, z);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00020310 File Offset: 0x0001E510
		public TailAnimatorWind()
		{
		}

		// Token: 0x040003F2 RID: 1010
		[CompilerGenerated]
		private static TailAnimatorWind <Instance>k__BackingField;

		// Token: 0x040003F3 RID: 1011
		[Header("In playmode you will find this object in DontDestroyOnLoad")]
		[FPD_Header("Main Wind Setings", 2f, 4f, 2)]
		public float power = 1f;

		// Token: 0x040003F4 RID: 1012
		public float additionalTurbulence = 1f;

		// Token: 0x040003F5 RID: 1013
		public float additionalTurbSpeed = 1f;

		// Token: 0x040003F6 RID: 1014
		[Space(7f)]
		public WindZone SyncWithUnityWindZone;

		// Token: 0x040003F7 RID: 1015
		public float UnityWindZonePowerMul = 2f;

		// Token: 0x040003F8 RID: 1016
		public float UnityWindZoneTurbMul = 1f;

		// Token: 0x040003F9 RID: 1017
		[Header("Overriding wind if value below different than 0,0,0")]
		public Vector3 overrideWind = Vector3.zero;

		// Token: 0x040003FA RID: 1018
		[FPD_Header("Procedural Wind Settings (if not syncing and not overriding)", 6f, 4f, 2)]
		[Range(0.1f, 1f)]
		public float rapidness = 0.95f;

		// Token: 0x040003FB RID: 1019
		[FPD_Suffix(0f, 360f, FPD_SuffixAttribute.SuffixMode.FromMinToMaxRounded, "°", true, 0)]
		public float changesPower = 90f;

		// Token: 0x040003FC RID: 1020
		[Header("Extra")]
		[Range(0f, 10f)]
		public float turbulenceSpeed = 1f;

		// Token: 0x040003FD RID: 1021
		[FPD_Header("World Position Turbulence", 6f, 4f, 2)]
		[Tooltip("Increase to make objects next to each other wave in slightly different way")]
		public float worldTurb = 1f;

		// Token: 0x040003FE RID: 1022
		[Tooltip("If higher no performance cost, it is just a number")]
		public float worldTurbScale = 512f;

		// Token: 0x040003FF RID: 1023
		public float worldTurbSpeed = 5f;

		// Token: 0x04000400 RID: 1024
		[FPD_Header("Tail Compoenents Related", 6f, 4f, 2)]
		[Tooltip("When tail is longer then power of wind should be higher")]
		public bool powerDependOnTailLength = true;

		// Token: 0x04000401 RID: 1025
		[Tooltip("Don't destroy on load")]
		public bool persistThroughAllScenes;

		// Token: 0x04000402 RID: 1026
		private Vector3 targetWind = Vector3.zero;

		// Token: 0x04000403 RID: 1027
		private Vector3 smoothWind = Vector3.zero;

		// Token: 0x04000404 RID: 1028
		private Vector3 windVeloHelper = Vector3.zero;

		// Token: 0x04000405 RID: 1029
		private Quaternion windOrientation = Quaternion.identity;

		// Token: 0x04000406 RID: 1030
		private Quaternion smoothWindOrient = Quaternion.identity;

		// Token: 0x04000407 RID: 1031
		private Quaternion smoothWindOrientHelper = Quaternion.identity;

		// Token: 0x04000408 RID: 1032
		private float[] randNumbers;

		// Token: 0x04000409 RID: 1033
		private float[] randTimes;

		// Token: 0x0400040A RID: 1034
		private float[] randSpeeds;

		// Token: 0x0400040B RID: 1035
		private int frameOffset = 2;

		// Token: 0x0400040C RID: 1036
		private Vector3 finalAddTurbulence = Vector3.zero;

		// Token: 0x0400040D RID: 1037
		private Vector3 addTurbHelper = Vector3.zero;

		// Token: 0x0400040E RID: 1038
		private Vector3 smoothAddTurbulence = Vector3.zero;
	}
}
