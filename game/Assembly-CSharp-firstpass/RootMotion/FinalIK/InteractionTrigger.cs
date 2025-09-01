using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x02000105 RID: 261
	[HelpURL("https://www.youtube.com/watch?v=-TDZpNjt2mk&index=15&list=PLVxSIA1OaTOu8Nos3CalXbJ2DrKnntMv6")]
	[AddComponentMenu("Scripts/RootMotion.FinalIK/Interaction System/Interaction Trigger")]
	public class InteractionTrigger : MonoBehaviour
	{
		// Token: 0x06000BB3 RID: 2995 RVA: 0x0004F641 File Offset: 0x0004D841
		[ContextMenu("User Manual")]
		private void OpenUserManual()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/page10.html");
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0004F64D File Offset: 0x0004D84D
		[ContextMenu("Scrpt Reference")]
		private void OpenScriptReference()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/class_root_motion_1_1_final_i_k_1_1_interaction_trigger.html");
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0004F659 File Offset: 0x0004D859
		[ContextMenu("TUTORIAL VIDEO")]
		private void OpenTutorial4()
		{
			Application.OpenURL("https://www.youtube.com/watch?v=-TDZpNjt2mk&index=15&list=PLVxSIA1OaTOu8Nos3CalXbJ2DrKnntMv6");
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0004F665 File Offset: 0x0004D865
		[ContextMenu("Support Group")]
		private void SupportGroup()
		{
			Application.OpenURL("https://groups.google.com/forum/#!forum/final-ik");
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0004F671 File Offset: 0x0004D871
		[ContextMenu("Asset Store Thread")]
		private void ASThread()
		{
			Application.OpenURL("http://forum.unity3d.com/threads/final-ik-full-body-ik-aim-look-at-fabrik-ccd-ik-1-0-released.222685/");
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0004F680 File Offset: 0x0004D880
		public int GetBestRangeIndex(Transform character, Transform raycastFrom, RaycastHit raycastHit)
		{
			if (base.GetComponent<Collider>() == null)
			{
				Warning.Log("Using the InteractionTrigger requires a Collider component.", base.transform, false);
				return -1;
			}
			int result = -1;
			float num = 180f;
			float num2 = 0f;
			for (int i = 0; i < this.ranges.Length; i++)
			{
				if (this.ranges[i].IsInRange(character, raycastFrom, raycastHit, base.transform, out num2) && num2 <= num)
				{
					num = num2;
					result = i;
				}
			}
			return result;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0004F6F2 File Offset: 0x0004D8F2
		public InteractionTrigger()
		{
		}

		// Token: 0x0400092A RID: 2346
		[Tooltip("The valid ranges of the character's and/or it's camera's position for triggering interaction when the character is in contact with the collider of this trigger.")]
		public InteractionTrigger.Range[] ranges = new InteractionTrigger.Range[0];

		// Token: 0x0200020F RID: 527
		[Serializable]
		public class CharacterPosition
		{
			// Token: 0x17000242 RID: 578
			// (get) Token: 0x06001122 RID: 4386 RVA: 0x0006B6C3 File Offset: 0x000698C3
			public Vector3 offset3D
			{
				get
				{
					return new Vector3(this.offset.x, 0f, this.offset.y);
				}
			}

			// Token: 0x17000243 RID: 579
			// (get) Token: 0x06001123 RID: 4387 RVA: 0x0006B6E5 File Offset: 0x000698E5
			public Vector3 direction3D
			{
				get
				{
					return Quaternion.AngleAxis(this.angleOffset, Vector3.up) * Vector3.forward;
				}
			}

			// Token: 0x06001124 RID: 4388 RVA: 0x0006B704 File Offset: 0x00069904
			public bool IsInRange(Transform character, Transform trigger, out float error)
			{
				error = 0f;
				if (!this.use)
				{
					return true;
				}
				error = 180f;
				if (this.radius <= 0f)
				{
					return false;
				}
				if (this.maxAngle <= 0f)
				{
					return false;
				}
				Vector3 forward = trigger.forward;
				if (this.fixYAxis)
				{
					forward.y = 0f;
				}
				if (forward == Vector3.zero)
				{
					return false;
				}
				Vector3 vector = this.fixYAxis ? Vector3.up : trigger.up;
				Quaternion rotation = Quaternion.LookRotation(forward, vector);
				Vector3 vector2 = trigger.position + rotation * this.offset3D;
				Vector3 b = this.orbit ? trigger.position : vector2;
				Vector3 vector3 = character.position - b;
				Vector3.OrthoNormalize(ref vector, ref vector3);
				vector3 *= Vector3.Project(character.position - b, vector3).magnitude;
				if (this.orbit)
				{
					float magnitude = this.offset.magnitude;
					float magnitude2 = vector3.magnitude;
					if (magnitude2 < magnitude - this.radius || magnitude2 > magnitude + this.radius)
					{
						return false;
					}
				}
				else if (vector3.magnitude > this.radius)
				{
					return false;
				}
				Vector3 vector4 = rotation * this.direction3D;
				Vector3.OrthoNormalize(ref vector, ref vector4);
				if (this.orbit)
				{
					Vector3 vector5 = vector2 - trigger.position;
					if (vector5 == Vector3.zero)
					{
						vector5 = Vector3.forward;
					}
					vector3 = Quaternion.Inverse(Quaternion.LookRotation(vector5, vector)) * vector3;
					vector4 = Quaternion.AngleAxis(Mathf.Atan2(vector3.x, vector3.z) * 57.29578f, vector) * vector4;
				}
				float num = Vector3.Angle(vector4, character.forward);
				if (num > this.maxAngle)
				{
					return false;
				}
				error = num / this.maxAngle * 180f;
				return true;
			}

			// Token: 0x06001125 RID: 4389 RVA: 0x0006B8F0 File Offset: 0x00069AF0
			public CharacterPosition()
			{
			}

			// Token: 0x04000FC5 RID: 4037
			[Tooltip("If false, will not care where the character stands, as long as it is in contact with the trigger collider.")]
			public bool use;

			// Token: 0x04000FC6 RID: 4038
			[Tooltip("The offset of the character's position relative to the trigger in XZ plane. Y position of the character is unlimited as long as it is contact with the collider.")]
			public Vector2 offset;

			// Token: 0x04000FC7 RID: 4039
			[Tooltip("Angle offset from the default forward direction.")]
			[Range(-180f, 180f)]
			public float angleOffset;

			// Token: 0x04000FC8 RID: 4040
			[Tooltip("Max angular offset of the character's forward from the direction of this trigger.")]
			[Range(0f, 180f)]
			public float maxAngle = 45f;

			// Token: 0x04000FC9 RID: 4041
			[Tooltip("Max offset of the character's position from this range's center.")]
			public float radius = 0.5f;

			// Token: 0x04000FCA RID: 4042
			[Tooltip("If true, will rotate the trigger around it's Y axis relative to the position of the character, so the object can be interacted with from all sides.")]
			public bool orbit;

			// Token: 0x04000FCB RID: 4043
			[Tooltip("Fixes the Y axis of the trigger to Vector3.up. This makes the trigger symmetrical relative to the object. For example a gun will be able to be picked up from the same direction relative to the barrel no matter which side the gun is resting on.")]
			public bool fixYAxis;
		}

		// Token: 0x02000210 RID: 528
		[Serializable]
		public class CameraPosition
		{
			// Token: 0x06001126 RID: 4390 RVA: 0x0006B910 File Offset: 0x00069B10
			public Quaternion GetRotation()
			{
				Vector3 forward = this.lookAtTarget.transform.forward;
				if (this.fixYAxis)
				{
					forward.y = 0f;
				}
				if (forward == Vector3.zero)
				{
					return Quaternion.identity;
				}
				Vector3 upwards = this.fixYAxis ? Vector3.up : this.lookAtTarget.transform.up;
				return Quaternion.LookRotation(forward, upwards);
			}

			// Token: 0x06001127 RID: 4391 RVA: 0x0006B97C File Offset: 0x00069B7C
			public bool IsInRange(Transform raycastFrom, RaycastHit hit, Transform trigger, out float error)
			{
				error = 0f;
				if (this.lookAtTarget == null)
				{
					return true;
				}
				error = 180f;
				if (raycastFrom == null)
				{
					return false;
				}
				if (hit.collider != this.lookAtTarget)
				{
					return false;
				}
				if (hit.distance > this.maxDistance)
				{
					return false;
				}
				if (this.direction == Vector3.zero)
				{
					return false;
				}
				if (this.maxDistance <= 0f)
				{
					return false;
				}
				if (this.maxAngle <= 0f)
				{
					return false;
				}
				Vector3 to = this.GetRotation() * this.direction;
				float num = Vector3.Angle(raycastFrom.position - hit.point, to);
				if (num > this.maxAngle)
				{
					return false;
				}
				error = num / this.maxAngle * 180f;
				return true;
			}

			// Token: 0x06001128 RID: 4392 RVA: 0x0006BA55 File Offset: 0x00069C55
			public CameraPosition()
			{
			}

			// Token: 0x04000FCC RID: 4044
			[Tooltip("What the camera should be looking at to trigger the interaction? If null, this camera position will not be used.")]
			public Collider lookAtTarget;

			// Token: 0x04000FCD RID: 4045
			[Tooltip("The direction from the lookAtTarget towards the camera (in lookAtTarget's space).")]
			public Vector3 direction = -Vector3.forward;

			// Token: 0x04000FCE RID: 4046
			[Tooltip("Max distance from the lookAtTarget to the camera.")]
			public float maxDistance = 0.5f;

			// Token: 0x04000FCF RID: 4047
			[Tooltip("Max angle between the direction and the direction towards the camera.")]
			[Range(0f, 180f)]
			public float maxAngle = 45f;

			// Token: 0x04000FD0 RID: 4048
			[Tooltip("Fixes the Y axis of the trigger to Vector3.up. This makes the trigger symmetrical relative to the object.")]
			public bool fixYAxis;
		}

		// Token: 0x02000211 RID: 529
		[Serializable]
		public class Range
		{
			// Token: 0x06001129 RID: 4393 RVA: 0x0006BA84 File Offset: 0x00069C84
			public bool IsInRange(Transform character, Transform raycastFrom, RaycastHit raycastHit, Transform trigger, out float maxError)
			{
				maxError = 0f;
				float a = 0f;
				float b = 0f;
				if (!this.characterPosition.IsInRange(character, trigger, out a))
				{
					return false;
				}
				if (!this.cameraPosition.IsInRange(raycastFrom, raycastHit, trigger, out b))
				{
					return false;
				}
				maxError = Mathf.Max(a, b);
				return true;
			}

			// Token: 0x0600112A RID: 4394 RVA: 0x0006BAD9 File Offset: 0x00069CD9
			public Range()
			{
			}

			// Token: 0x04000FD1 RID: 4049
			[HideInInspector]
			public string name;

			// Token: 0x04000FD2 RID: 4050
			[HideInInspector]
			public bool show = true;

			// Token: 0x04000FD3 RID: 4051
			[Tooltip("The range for the character's position and rotation.")]
			public InteractionTrigger.CharacterPosition characterPosition;

			// Token: 0x04000FD4 RID: 4052
			[Tooltip("The range for the character camera's position and rotation.")]
			public InteractionTrigger.CameraPosition cameraPosition;

			// Token: 0x04000FD5 RID: 4053
			[Tooltip("Definitions of the interactions associated with this range.")]
			public InteractionTrigger.Range.Interaction[] interactions;

			// Token: 0x02000249 RID: 585
			[Serializable]
			public class Interaction
			{
				// Token: 0x060011D5 RID: 4565 RVA: 0x0006E71B File Offset: 0x0006C91B
				public Interaction()
				{
				}

				// Token: 0x04001106 RID: 4358
				[Tooltip("The InteractionObject to interact with.")]
				public InteractionObject interactionObject;

				// Token: 0x04001107 RID: 4359
				[Tooltip("The effectors to interact with.")]
				public FullBodyBipedEffector[] effectors;
			}
		}
	}
}
