using System;
using UnityEngine;

namespace FIMSpace
{
	// Token: 0x02000036 RID: 54
	public static class FAnimatorMethods
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x000098A8 File Offset: 0x00007AA8
		public static void LerpFloatValue(this Animator animator, string name = "RunWalk", float value = 0f, float deltaSpeed = 8f)
		{
			float num = animator.GetFloat(name);
			num = Mathf.Lerp(num, value, Time.deltaTime * deltaSpeed);
			animator.SetFloat(name, num);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000098D4 File Offset: 0x00007AD4
		public static bool CheckAnimationEnd(this Animator animator, int layer = 0, bool reverse = false, bool checkAnimLoop = true)
		{
			AnimatorStateInfo currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(layer);
			if (!animator.IsInTransition(layer))
			{
				if (checkAnimLoop)
				{
					if (!currentAnimatorStateInfo.loop && !reverse)
					{
						if (currentAnimatorStateInfo.normalizedTime > 0.98f)
						{
							return true;
						}
						if (currentAnimatorStateInfo.normalizedTime < 0.02f)
						{
							return true;
						}
					}
				}
				else if (!reverse)
				{
					if (currentAnimatorStateInfo.normalizedTime > 0.98f)
					{
						return true;
					}
					if (currentAnimatorStateInfo.normalizedTime < 0.02f)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00009948 File Offset: 0x00007B48
		public static void ResetLayersWeights(this Animator animator, float speed = 10f)
		{
			for (int i = 1; i < animator.layerCount; i++)
			{
				animator.SetLayerWeight(i, animator.GetLayerWeight(i).Lerp(0f, Time.deltaTime * speed));
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00009988 File Offset: 0x00007B88
		public static void LerpLayerWeight(this Animator animator, int layer = 0, float newValue = 1f, float speed = 8f)
		{
			float num = animator.GetLayerWeight(layer);
			num.Lerp(newValue, Time.deltaTime * speed);
			if (newValue == 1f && num > 0.999f)
			{
				num = 1f;
			}
			if (newValue == 0f && num < 0.01f)
			{
				num = 0f;
			}
			animator.SetLayerWeight(layer, num);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000099E0 File Offset: 0x00007BE0
		public static bool StateExists(this Animator animator, string clipName, int layer = 0)
		{
			int stateID = Animator.StringToHash(clipName);
			return animator.HasState(layer, stateID);
		}
	}
}
