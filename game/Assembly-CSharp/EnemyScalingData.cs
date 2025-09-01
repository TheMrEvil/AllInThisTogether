using System;
using UnityEngine;

// Token: 0x0200005F RID: 95
[Serializable]
public class EnemyScalingData
{
	// Token: 0x060002EE RID: 750 RVA: 0x00018E3C File Offset: 0x0001703C
	public float BaseScaling()
	{
		float num = Mathf.Max(1f, this.AppendixCurve.Evaluate((float)WaveManager.instance.AppendixLevel));
		float num2 = this.PerPlayerMultiplier * (float)Mathf.Clamp(PlayerControl.PlayerCount, 1, 3) + this.BaseScaleAdd;
		return Mathf.Max(1f, num2 * num * this.CoreScalar * (float)WaveManager.instance.WavesCompleted);
	}

	// Token: 0x060002EF RID: 751 RVA: 0x00018EA8 File Offset: 0x000170A8
	public float BindingHPScaling()
	{
		float num = this.BindingLevelHPCurve.Evaluate((float)GameplayManager.BindingLevel);
		float num2 = this.BindingWaveHPCurve.Evaluate((float)WaveManager.instance.WavesCompleted) * this.BindingProgressHPMult;
		num2 *= num;
		return (float)GameplayManager.BindingLevel * this.BindingFlatHPMult + num2;
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x00018EF8 File Offset: 0x000170F8
	public float BindingDamageScaling()
	{
		return 1f + this.BindingDamageCurve.Evaluate((float)GameplayManager.BindingLevel) * this.BindingChapterDamageCurve.Evaluate((float)WaveManager.instance.WavesCompleted);
	}

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x060002F1 RID: 753 RVA: 0x00018F28 File Offset: 0x00017128
	public float AppendixDamageIncrease
	{
		get
		{
			return this.AppendixDamageAdd.Evaluate((float)WaveManager.instance.AppendixLevel);
		}
	}

	// Token: 0x17000039 RID: 57
	// (get) Token: 0x060002F2 RID: 754 RVA: 0x00018F40 File Offset: 0x00017140
	public float AppendixDamageScale
	{
		get
		{
			return this.AppendixDamageCurve.Evaluate((float)WaveManager.instance.AppendixLevel);
		}
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x00018F58 File Offset: 0x00017158
	public float GetUnboundScale()
	{
		int num = 0;
		foreach (WaveDB.AppendixBinding appendixBinding in AIManager.instance.Waves.AppendixSimpleBindings)
		{
			if (GameplayManager.instance.GenreBindings.TreeIDs.Contains(appendixBinding.Binding.ID))
			{
				num += appendixBinding.Value * Mathf.Max(1, GameplayManager.instance.GenreBindings.trees[appendixBinding.Binding.Root]);
			}
		}
		return this.AppendixUnboundCurve.Evaluate((float)num);
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x0001900C File Offset: 0x0001720C
	public float GetSpeedMult()
	{
		float num = this.BindingSpeedCurve.Evaluate((float)GameplayManager.BindingLevel);
		float num2 = this.AppendixSpeedCurve.Evaluate((float)WaveManager.instance.AppendixLevel);
		return num * num2;
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x00019044 File Offset: 0x00017244
	public float GetCooldownMult()
	{
		float num = this.BindingCooldownCurve.Evaluate((float)GameplayManager.BindingLevel);
		float num2 = this.AppendixCooldownCurve.Evaluate((float)WaveManager.instance.AppendixLevel);
		return num * num2;
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x0001907B File Offset: 0x0001727B
	public EnemyScalingData()
	{
	}

	// Token: 0x040002E3 RID: 739
	public float CoreScalar = 1f;

	// Token: 0x040002E4 RID: 740
	public float BaseScaleAdd = 0.25f;

	// Token: 0x040002E5 RID: 741
	public float PerPlayerMultiplier = 0.03f;

	// Token: 0x040002E6 RID: 742
	public AnimationCurve AppendixCurve;

	// Token: 0x040002E7 RID: 743
	public AnimationCurve BindingWaveHPCurve;

	// Token: 0x040002E8 RID: 744
	public AnimationCurve BindingLevelHPCurve;

	// Token: 0x040002E9 RID: 745
	public float BindingProgressHPMult = 0.03f;

	// Token: 0x040002EA RID: 746
	public float BindingFlatHPMult = 0.0175f;

	// Token: 0x040002EB RID: 747
	public AnimationCurve BindingDamageCurve;

	// Token: 0x040002EC RID: 748
	public AnimationCurve BindingChapterDamageCurve;

	// Token: 0x040002ED RID: 749
	public AnimationCurve BindingSpeedCurve;

	// Token: 0x040002EE RID: 750
	public AnimationCurve BindingCooldownCurve;

	// Token: 0x040002EF RID: 751
	public AnimationCurve AppendixUnboundCurve;

	// Token: 0x040002F0 RID: 752
	public AnimationCurve AppendixDamageAdd;

	// Token: 0x040002F1 RID: 753
	public AnimationCurve AppendixDamageCurve;

	// Token: 0x040002F2 RID: 754
	public AnimationCurve AppendixSpeedCurve;

	// Token: 0x040002F3 RID: 755
	public AnimationCurve AppendixCooldownCurve;
}
