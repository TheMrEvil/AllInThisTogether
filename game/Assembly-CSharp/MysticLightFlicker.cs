using System;
using UnityEngine;

// Token: 0x02000012 RID: 18
public class MysticLightFlicker : MonoBehaviour
{
	// Token: 0x06000055 RID: 85 RVA: 0x00005136 File Offset: 0x00003336
	private void Start()
	{
		this.originalColor = base.GetComponent<Light>().color;
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00005149 File Offset: 0x00003349
	private void Update()
	{
		base.GetComponent<Light>().color = this.originalColor * this.EvalWave();
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00005168 File Offset: 0x00003368
	private float EvalWave()
	{
		float num = (Time.time + this.phase) * this.frequency;
		num -= Mathf.Floor(num);
		float num2;
		if (this.waveFunction == "sin")
		{
			num2 = Mathf.Sin(num * 2f * 3.1415927f);
		}
		else if (this.waveFunction == "tri")
		{
			if (num < 0.5f)
			{
				num2 = 4f * num - 1f;
			}
			else
			{
				num2 = -4f * num + 3f;
			}
		}
		else if (this.waveFunction == "sqr")
		{
			if (num < 0.5f)
			{
				num2 = 1f;
			}
			else
			{
				num2 = -1f;
			}
		}
		else if (this.waveFunction == "saw")
		{
			num2 = num;
		}
		else if (this.waveFunction == "inv")
		{
			num2 = 1f - num;
		}
		else if (this.waveFunction == "noise")
		{
			num2 = 1f - UnityEngine.Random.value * 2f;
		}
		else
		{
			num2 = 1f;
		}
		return num2 * this.amplitude + this.startValue;
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00005293 File Offset: 0x00003493
	public MysticLightFlicker()
	{
	}

	// Token: 0x04000041 RID: 65
	public string waveFunction = "sin";

	// Token: 0x04000042 RID: 66
	public float startValue;

	// Token: 0x04000043 RID: 67
	public float amplitude = 1f;

	// Token: 0x04000044 RID: 68
	public float phase;

	// Token: 0x04000045 RID: 69
	public float frequency = 0.5f;

	// Token: 0x04000046 RID: 70
	private Color originalColor;
}
