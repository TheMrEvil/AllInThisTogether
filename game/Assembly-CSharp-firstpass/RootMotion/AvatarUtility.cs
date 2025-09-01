using System;
using System.Reflection;
using UnityEngine;

namespace RootMotion
{
	// Token: 0x020000AA RID: 170
	public class AvatarUtility
	{
		// Token: 0x060007AC RID: 1964 RVA: 0x00034CBC File Offset: 0x00032EBC
		public static Quaternion GetPostRotation(Avatar avatar, AvatarIKGoal avatarIKGoal)
		{
			int num = (int)AvatarUtility.HumanIDFromAvatarIKGoal(avatarIKGoal);
			if (num == 55)
			{
				throw new InvalidOperationException("Invalid human id.");
			}
			MethodInfo method = typeof(Avatar).GetMethod("GetPostRotation", BindingFlags.Instance | BindingFlags.NonPublic);
			if (method == null)
			{
				throw new InvalidOperationException("Cannot find GetPostRotation method.");
			}
			return (Quaternion)method.Invoke(avatar, new object[]
			{
				num
			});
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00034D24 File Offset: 0x00032F24
		public static TQ GetIKGoalTQ(Avatar avatar, float humanScale, AvatarIKGoal avatarIKGoal, TQ bodyPositionRotation, TQ boneTQ)
		{
			int num = (int)AvatarUtility.HumanIDFromAvatarIKGoal(avatarIKGoal);
			if (num == 55)
			{
				throw new InvalidOperationException("Invalid human id.");
			}
			MethodInfo method = typeof(Avatar).GetMethod("GetAxisLength", BindingFlags.Instance | BindingFlags.NonPublic);
			if (method == null)
			{
				throw new InvalidOperationException("Cannot find GetAxisLength method.");
			}
			MethodInfo method2 = typeof(Avatar).GetMethod("GetPostRotation", BindingFlags.Instance | BindingFlags.NonPublic);
			if (method2 == null)
			{
				throw new InvalidOperationException("Cannot find GetPostRotation method.");
			}
			Quaternion rhs = (Quaternion)method2.Invoke(avatar, new object[]
			{
				num
			});
			TQ tq = new TQ(boneTQ.t, boneTQ.q * rhs);
			if (avatarIKGoal == AvatarIKGoal.LeftFoot || avatarIKGoal == AvatarIKGoal.RightFoot)
			{
				float x = (float)method.Invoke(avatar, new object[]
				{
					num
				});
				Vector3 point = new Vector3(x, 0f, 0f);
				tq.t += tq.q * point;
			}
			Quaternion quaternion = Quaternion.Inverse(bodyPositionRotation.q);
			tq.t = quaternion * (tq.t - bodyPositionRotation.t);
			tq.q = quaternion * tq.q;
			tq.t /= humanScale;
			tq.q = Quaternion.LookRotation(tq.q * Vector3.forward, tq.q * Vector3.up);
			return tq;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00034EA4 File Offset: 0x000330A4
		public static HumanBodyBones HumanIDFromAvatarIKGoal(AvatarIKGoal avatarIKGoal)
		{
			HumanBodyBones result = HumanBodyBones.LastBone;
			switch (avatarIKGoal)
			{
			case AvatarIKGoal.LeftFoot:
				result = HumanBodyBones.LeftFoot;
				break;
			case AvatarIKGoal.RightFoot:
				result = HumanBodyBones.RightFoot;
				break;
			case AvatarIKGoal.LeftHand:
				result = HumanBodyBones.LeftHand;
				break;
			case AvatarIKGoal.RightHand:
				result = HumanBodyBones.RightHand;
				break;
			}
			return result;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00034EDD File Offset: 0x000330DD
		public AvatarUtility()
		{
		}
	}
}
