using System;
using UnityEngine;

// Token: 0x02000158 RID: 344
public class WaveProgressPoint : MonoBehaviour
{
	// Token: 0x06000F2C RID: 3884 RVA: 0x0006040C File Offset: 0x0005E60C
	public void Setup(float atPoint, WaveProgressPoint.WavePointType pType, EnemyType eType)
	{
		float x = (base.transform.parent.GetComponent<RectTransform>().rect.width - 36f) * atPoint + 18f;
		base.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, 0f);
		this.ObjectiveDisplay.SetActive(pType == WaveProgressPoint.WavePointType.BonusObjective);
		this.Elite_Raving.SetActive(pType == WaveProgressPoint.WavePointType.Elite && eType.AnyFlagsMatch(EnemyType.Raving));
		this.Elite_Splice.SetActive(pType == WaveProgressPoint.WavePointType.Elite && eType.AnyFlagsMatch(EnemyType.Splice));
		this.Elite_Tangent.SetActive(pType == WaveProgressPoint.WavePointType.Elite && eType.AnyFlagsMatch(EnemyType.Tangent));
		this.AtPoint = atPoint;
		this.WaveType = pType;
	}

	// Token: 0x06000F2D RID: 3885 RVA: 0x000604C3 File Offset: 0x0005E6C3
	public void Pulse()
	{
		if (this.WaveType != WaveProgressPoint.WavePointType.Elite)
		{
			WaveProgressPoint.WavePointType waveType = this.WaveType;
		}
		this.anim.Play("WaveMarker_Elite");
	}

	// Token: 0x06000F2E RID: 3886 RVA: 0x000604E5 File Offset: 0x0005E6E5
	public WaveProgressPoint()
	{
	}

	// Token: 0x04000CE5 RID: 3301
	private const float OFFSET = 18f;

	// Token: 0x04000CE6 RID: 3302
	public GameObject ObjectiveDisplay;

	// Token: 0x04000CE7 RID: 3303
	public GameObject Elite_Raving;

	// Token: 0x04000CE8 RID: 3304
	public GameObject Elite_Splice;

	// Token: 0x04000CE9 RID: 3305
	public GameObject Elite_Tangent;

	// Token: 0x04000CEA RID: 3306
	public Animator anim;

	// Token: 0x04000CEB RID: 3307
	[NonSerialized]
	public float AtPoint;

	// Token: 0x04000CEC RID: 3308
	[NonSerialized]
	public WaveProgressPoint.WavePointType WaveType;

	// Token: 0x0200054E RID: 1358
	public enum WavePointType
	{
		// Token: 0x040026AC RID: 9900
		BonusObjective,
		// Token: 0x040026AD RID: 9901
		Elite
	}
}
