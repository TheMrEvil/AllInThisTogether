using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000015 RID: 21
public class LightProbeRuntime : MonoBehaviour
{
	// Token: 0x0600004C RID: 76 RVA: 0x00003343 File Offset: 0x00001543
	private IEnumerator Start()
	{
		yield return null;
		this.m_Lights = UnityEngine.Object.FindObjectsOfType<Light>();
		SphericalHarmonicsL2[] bakedProbes = LightmapSettings.lightProbes.bakedProbes;
		Vector3[] positions = LightmapSettings.lightProbes.positions;
		int count = LightmapSettings.lightProbes.count;
		for (int i = 0; i < count; i++)
		{
			bakedProbes[i].Clear();
		}
		for (int j = 0; j < count; j++)
		{
			bakedProbes[j].AddAmbientLight(this.m_Ambient);
		}
		foreach (Light light in this.m_Lights)
		{
			if (light.type == LightType.Directional)
			{
				for (int l = 0; l < count; l++)
				{
					bakedProbes[l].AddDirectionalLight(-light.transform.forward, light.color, light.intensity);
				}
			}
			else if (light.type == LightType.Point)
			{
				for (int m = 0; m < count; m++)
				{
					this.SHAddPointLight(positions[m], light.transform.position, light.range, light.color, light.intensity, ref bakedProbes[m]);
				}
			}
		}
		LightmapSettings.lightProbes.bakedProbes = bakedProbes;
		yield break;
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00003354 File Offset: 0x00001554
	private void SHAddPointLight(Vector3 probePosition, Vector3 position, float range, Color color, float intensity, ref SphericalHarmonicsL2 sh)
	{
		Vector3 vector = position - probePosition;
		float num = 1f / (1f + 25f * vector.sqrMagnitude / (range * range));
		sh.AddDirectionalLight(vector.normalized, color, intensity * num);
	}

	// Token: 0x0600004E RID: 78 RVA: 0x0000339B File Offset: 0x0000159B
	public LightProbeRuntime()
	{
	}

	// Token: 0x04000047 RID: 71
	public Color m_Ambient;

	// Token: 0x04000048 RID: 72
	private Light[] m_Lights;

	// Token: 0x0200018F RID: 399
	[CompilerGenerated]
	private sealed class <Start>d__2 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06000EAC RID: 3756 RVA: 0x0005F494 File Offset: 0x0005D694
		[DebuggerHidden]
		public <Start>d__2(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0005F4A3 File Offset: 0x0005D6A3
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0005F4A8 File Offset: 0x0005D6A8
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			LightProbeRuntime lightProbeRuntime = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = null;
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			lightProbeRuntime.m_Lights = UnityEngine.Object.FindObjectsOfType<Light>();
			SphericalHarmonicsL2[] bakedProbes = LightmapSettings.lightProbes.bakedProbes;
			Vector3[] positions = LightmapSettings.lightProbes.positions;
			int count = LightmapSettings.lightProbes.count;
			for (int i = 0; i < count; i++)
			{
				bakedProbes[i].Clear();
			}
			for (int j = 0; j < count; j++)
			{
				bakedProbes[j].AddAmbientLight(lightProbeRuntime.m_Ambient);
			}
			foreach (Light light in lightProbeRuntime.m_Lights)
			{
				if (light.type == LightType.Directional)
				{
					for (int l = 0; l < count; l++)
					{
						bakedProbes[l].AddDirectionalLight(-light.transform.forward, light.color, light.intensity);
					}
				}
				else if (light.type == LightType.Point)
				{
					for (int m = 0; m < count; m++)
					{
						lightProbeRuntime.SHAddPointLight(positions[m], light.transform.position, light.range, light.color, light.intensity, ref bakedProbes[m]);
					}
				}
			}
			LightmapSettings.lightProbes.bakedProbes = bakedProbes;
			return false;
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000EAF RID: 3759 RVA: 0x0005F628 File Offset: 0x0005D828
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0005F630 File Offset: 0x0005D830
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x0005F637 File Offset: 0x0005D837
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04000C58 RID: 3160
		private int <>1__state;

		// Token: 0x04000C59 RID: 3161
		private object <>2__current;

		// Token: 0x04000C5A RID: 3162
		public LightProbeRuntime <>4__this;
	}
}
