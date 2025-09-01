using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000021 RID: 33
	[NativeHeader("Modules/Animation/Animator.h")]
	[UsedByNativeCode]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimatorControllerParameter.bindings.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/Animator.bindings.h")]
	public class Animator : Behaviour
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000C4 RID: 196
		public extern bool isOptimizable { [NativeMethod("IsOptimizable")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000C5 RID: 197
		public extern bool isHuman { [NativeMethod("IsHuman")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000C6 RID: 198
		public extern bool hasRootMotion { [NativeMethod("HasRootMotion")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000C7 RID: 199
		internal extern bool isRootPositionOrRotationControlledByCurves { [NativeMethod("IsRootTranslationOrRotationControllerByCurves")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000C8 RID: 200
		public extern float humanScale { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000C9 RID: 201
		public extern bool isInitialized { [NativeMethod("IsInitialized")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060000CA RID: 202 RVA: 0x00002944 File Offset: 0x00000B44
		public float GetFloat(string name)
		{
			return this.GetFloatString(name);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00002960 File Offset: 0x00000B60
		public float GetFloat(int id)
		{
			return this.GetFloatID(id);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00002979 File Offset: 0x00000B79
		public void SetFloat(string name, float value)
		{
			this.SetFloatString(name, value);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00002985 File Offset: 0x00000B85
		public void SetFloat(string name, float value, float dampTime, float deltaTime)
		{
			this.SetFloatStringDamp(name, value, dampTime, deltaTime);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00002994 File Offset: 0x00000B94
		public void SetFloat(int id, float value)
		{
			this.SetFloatID(id, value);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000029A0 File Offset: 0x00000BA0
		public void SetFloat(int id, float value, float dampTime, float deltaTime)
		{
			this.SetFloatIDDamp(id, value, dampTime, deltaTime);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000029B0 File Offset: 0x00000BB0
		public bool GetBool(string name)
		{
			return this.GetBoolString(name);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool GetBool(int id)
		{
			return this.GetBoolID(id);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000029E5 File Offset: 0x00000BE5
		public void SetBool(string name, bool value)
		{
			this.SetBoolString(name, value);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000029F1 File Offset: 0x00000BF1
		public void SetBool(int id, bool value)
		{
			this.SetBoolID(id, value);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00002A00 File Offset: 0x00000C00
		public int GetInteger(string name)
		{
			return this.GetIntegerString(name);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00002A1C File Offset: 0x00000C1C
		public int GetInteger(int id)
		{
			return this.GetIntegerID(id);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00002A35 File Offset: 0x00000C35
		public void SetInteger(string name, int value)
		{
			this.SetIntegerString(name, value);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00002A41 File Offset: 0x00000C41
		public void SetInteger(int id, int value)
		{
			this.SetIntegerID(id, value);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00002A4D File Offset: 0x00000C4D
		public void SetTrigger(string name)
		{
			this.SetTriggerString(name);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00002A58 File Offset: 0x00000C58
		public void SetTrigger(int id)
		{
			this.SetTriggerID(id);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00002A63 File Offset: 0x00000C63
		public void ResetTrigger(string name)
		{
			this.ResetTriggerString(name);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00002A6E File Offset: 0x00000C6E
		public void ResetTrigger(int id)
		{
			this.ResetTriggerID(id);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00002A7C File Offset: 0x00000C7C
		public bool IsParameterControlledByCurve(string name)
		{
			return this.IsParameterControlledByCurveString(name);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00002A98 File Offset: 0x00000C98
		public bool IsParameterControlledByCurve(int id)
		{
			return this.IsParameterControlledByCurveID(id);
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00002AB4 File Offset: 0x00000CB4
		public Vector3 deltaPosition
		{
			get
			{
				Vector3 result;
				this.get_deltaPosition_Injected(out result);
				return result;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00002ACC File Offset: 0x00000CCC
		public Quaternion deltaRotation
		{
			get
			{
				Quaternion result;
				this.get_deltaRotation_Injected(out result);
				return result;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00002AE4 File Offset: 0x00000CE4
		public Vector3 velocity
		{
			get
			{
				Vector3 result;
				this.get_velocity_Injected(out result);
				return result;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00002AFC File Offset: 0x00000CFC
		public Vector3 angularVelocity
		{
			get
			{
				Vector3 result;
				this.get_angularVelocity_Injected(out result);
				return result;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00002B14 File Offset: 0x00000D14
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00002B2A File Offset: 0x00000D2A
		public Vector3 rootPosition
		{
			[NativeMethod("GetAvatarPosition")]
			get
			{
				Vector3 result;
				this.get_rootPosition_Injected(out result);
				return result;
			}
			[NativeMethod("SetAvatarPosition")]
			set
			{
				this.set_rootPosition_Injected(ref value);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00002B34 File Offset: 0x00000D34
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00002B4A File Offset: 0x00000D4A
		public Quaternion rootRotation
		{
			[NativeMethod("GetAvatarRotation")]
			get
			{
				Quaternion result;
				this.get_rootRotation_Injected(out result);
				return result;
			}
			[NativeMethod("SetAvatarRotation")]
			set
			{
				this.set_rootRotation_Injected(ref value);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000E6 RID: 230
		// (set) Token: 0x060000E7 RID: 231
		public extern bool applyRootMotion { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000E8 RID: 232
		// (set) Token: 0x060000E9 RID: 233
		[Obsolete("Animator.linearVelocityBlending is no longer used and has been deprecated.")]
		public extern bool linearVelocityBlending { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00002B54 File Offset: 0x00000D54
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00002B6F File Offset: 0x00000D6F
		[Obsolete("Animator.animatePhysics has been deprecated. Use Animator.updateMode instead.")]
		public bool animatePhysics
		{
			get
			{
				return this.updateMode == AnimatorUpdateMode.AnimatePhysics;
			}
			set
			{
				this.updateMode = (value ? AnimatorUpdateMode.AnimatePhysics : AnimatorUpdateMode.Normal);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000EC RID: 236
		// (set) Token: 0x060000ED RID: 237
		public extern AnimatorUpdateMode updateMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000EE RID: 238
		public extern bool hasTransformHierarchy { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000EF RID: 239
		// (set) Token: 0x060000F0 RID: 240
		internal extern bool allowConstantClipSamplingOptimization { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000F1 RID: 241
		public extern float gravityWeight { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00002B80 File Offset: 0x00000D80
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00002B9F File Offset: 0x00000D9F
		public Vector3 bodyPosition
		{
			get
			{
				this.CheckIfInIKPass();
				return this.bodyPositionInternal;
			}
			set
			{
				this.CheckIfInIKPass();
				this.bodyPositionInternal = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00002BB4 File Offset: 0x00000DB4
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00002BCA File Offset: 0x00000DCA
		internal Vector3 bodyPositionInternal
		{
			[NativeMethod("GetBodyPosition")]
			get
			{
				Vector3 result;
				this.get_bodyPositionInternal_Injected(out result);
				return result;
			}
			[NativeMethod("SetBodyPosition")]
			set
			{
				this.set_bodyPositionInternal_Injected(ref value);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00002BD4 File Offset: 0x00000DD4
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00002BF3 File Offset: 0x00000DF3
		public Quaternion bodyRotation
		{
			get
			{
				this.CheckIfInIKPass();
				return this.bodyRotationInternal;
			}
			set
			{
				this.CheckIfInIKPass();
				this.bodyRotationInternal = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00002C08 File Offset: 0x00000E08
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00002C1E File Offset: 0x00000E1E
		internal Quaternion bodyRotationInternal
		{
			[NativeMethod("GetBodyRotation")]
			get
			{
				Quaternion result;
				this.get_bodyRotationInternal_Injected(out result);
				return result;
			}
			[NativeMethod("SetBodyRotation")]
			set
			{
				this.set_bodyRotationInternal_Injected(ref value);
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00002C28 File Offset: 0x00000E28
		public Vector3 GetIKPosition(AvatarIKGoal goal)
		{
			this.CheckIfInIKPass();
			return this.GetGoalPosition(goal);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00002C48 File Offset: 0x00000E48
		private Vector3 GetGoalPosition(AvatarIKGoal goal)
		{
			Vector3 result;
			this.GetGoalPosition_Injected(goal, out result);
			return result;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00002C5F File Offset: 0x00000E5F
		public void SetIKPosition(AvatarIKGoal goal, Vector3 goalPosition)
		{
			this.CheckIfInIKPass();
			this.SetGoalPosition(goal, goalPosition);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00002C72 File Offset: 0x00000E72
		private void SetGoalPosition(AvatarIKGoal goal, Vector3 goalPosition)
		{
			this.SetGoalPosition_Injected(goal, ref goalPosition);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00002C80 File Offset: 0x00000E80
		public Quaternion GetIKRotation(AvatarIKGoal goal)
		{
			this.CheckIfInIKPass();
			return this.GetGoalRotation(goal);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00002CA0 File Offset: 0x00000EA0
		private Quaternion GetGoalRotation(AvatarIKGoal goal)
		{
			Quaternion result;
			this.GetGoalRotation_Injected(goal, out result);
			return result;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00002CB7 File Offset: 0x00000EB7
		public void SetIKRotation(AvatarIKGoal goal, Quaternion goalRotation)
		{
			this.CheckIfInIKPass();
			this.SetGoalRotation(goal, goalRotation);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00002CCA File Offset: 0x00000ECA
		private void SetGoalRotation(AvatarIKGoal goal, Quaternion goalRotation)
		{
			this.SetGoalRotation_Injected(goal, ref goalRotation);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00002CD8 File Offset: 0x00000ED8
		public float GetIKPositionWeight(AvatarIKGoal goal)
		{
			this.CheckIfInIKPass();
			return this.GetGoalWeightPosition(goal);
		}

		// Token: 0x06000103 RID: 259
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetGoalWeightPosition(AvatarIKGoal goal);

		// Token: 0x06000104 RID: 260 RVA: 0x00002CF8 File Offset: 0x00000EF8
		public void SetIKPositionWeight(AvatarIKGoal goal, float value)
		{
			this.CheckIfInIKPass();
			this.SetGoalWeightPosition(goal, value);
		}

		// Token: 0x06000105 RID: 261
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetGoalWeightPosition(AvatarIKGoal goal, float value);

		// Token: 0x06000106 RID: 262 RVA: 0x00002D0C File Offset: 0x00000F0C
		public float GetIKRotationWeight(AvatarIKGoal goal)
		{
			this.CheckIfInIKPass();
			return this.GetGoalWeightRotation(goal);
		}

		// Token: 0x06000107 RID: 263
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetGoalWeightRotation(AvatarIKGoal goal);

		// Token: 0x06000108 RID: 264 RVA: 0x00002D2C File Offset: 0x00000F2C
		public void SetIKRotationWeight(AvatarIKGoal goal, float value)
		{
			this.CheckIfInIKPass();
			this.SetGoalWeightRotation(goal, value);
		}

		// Token: 0x06000109 RID: 265
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetGoalWeightRotation(AvatarIKGoal goal, float value);

		// Token: 0x0600010A RID: 266 RVA: 0x00002D40 File Offset: 0x00000F40
		public Vector3 GetIKHintPosition(AvatarIKHint hint)
		{
			this.CheckIfInIKPass();
			return this.GetHintPosition(hint);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00002D60 File Offset: 0x00000F60
		private Vector3 GetHintPosition(AvatarIKHint hint)
		{
			Vector3 result;
			this.GetHintPosition_Injected(hint, out result);
			return result;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00002D77 File Offset: 0x00000F77
		public void SetIKHintPosition(AvatarIKHint hint, Vector3 hintPosition)
		{
			this.CheckIfInIKPass();
			this.SetHintPosition(hint, hintPosition);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00002D8A File Offset: 0x00000F8A
		private void SetHintPosition(AvatarIKHint hint, Vector3 hintPosition)
		{
			this.SetHintPosition_Injected(hint, ref hintPosition);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00002D98 File Offset: 0x00000F98
		public float GetIKHintPositionWeight(AvatarIKHint hint)
		{
			this.CheckIfInIKPass();
			return this.GetHintWeightPosition(hint);
		}

		// Token: 0x0600010F RID: 271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetHintWeightPosition(AvatarIKHint hint);

		// Token: 0x06000110 RID: 272 RVA: 0x00002DB8 File Offset: 0x00000FB8
		public void SetIKHintPositionWeight(AvatarIKHint hint, float value)
		{
			this.CheckIfInIKPass();
			this.SetHintWeightPosition(hint, value);
		}

		// Token: 0x06000111 RID: 273
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetHintWeightPosition(AvatarIKHint hint, float value);

		// Token: 0x06000112 RID: 274 RVA: 0x00002DCB File Offset: 0x00000FCB
		public void SetLookAtPosition(Vector3 lookAtPosition)
		{
			this.CheckIfInIKPass();
			this.SetLookAtPositionInternal(lookAtPosition);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00002DDD File Offset: 0x00000FDD
		[NativeMethod("SetLookAtPosition")]
		private void SetLookAtPositionInternal(Vector3 lookAtPosition)
		{
			this.SetLookAtPositionInternal_Injected(ref lookAtPosition);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00002DE7 File Offset: 0x00000FE7
		public void SetLookAtWeight(float weight)
		{
			this.CheckIfInIKPass();
			this.SetLookAtWeightInternal(weight, 0f, 1f, 0f, 0.5f);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00002E0D File Offset: 0x0000100D
		public void SetLookAtWeight(float weight, float bodyWeight)
		{
			this.CheckIfInIKPass();
			this.SetLookAtWeightInternal(weight, bodyWeight, 1f, 0f, 0.5f);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00002E2F File Offset: 0x0000102F
		public void SetLookAtWeight(float weight, float bodyWeight, float headWeight)
		{
			this.CheckIfInIKPass();
			this.SetLookAtWeightInternal(weight, bodyWeight, headWeight, 0f, 0.5f);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00002E4D File Offset: 0x0000104D
		public void SetLookAtWeight(float weight, float bodyWeight, float headWeight, float eyesWeight)
		{
			this.CheckIfInIKPass();
			this.SetLookAtWeightInternal(weight, bodyWeight, headWeight, eyesWeight, 0.5f);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00002E68 File Offset: 0x00001068
		public void SetLookAtWeight(float weight, [UnityEngine.Internal.DefaultValue("0.0f")] float bodyWeight, [UnityEngine.Internal.DefaultValue("1.0f")] float headWeight, [UnityEngine.Internal.DefaultValue("0.0f")] float eyesWeight, [UnityEngine.Internal.DefaultValue("0.5f")] float clampWeight)
		{
			this.CheckIfInIKPass();
			this.SetLookAtWeightInternal(weight, bodyWeight, headWeight, eyesWeight, clampWeight);
		}

		// Token: 0x06000119 RID: 281
		[NativeMethod("SetLookAtWeight")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetLookAtWeightInternal(float weight, float bodyWeight, float headWeight, float eyesWeight, float clampWeight);

		// Token: 0x0600011A RID: 282 RVA: 0x00002E80 File Offset: 0x00001080
		public void SetBoneLocalRotation(HumanBodyBones humanBoneId, Quaternion rotation)
		{
			this.CheckIfInIKPass();
			this.SetBoneLocalRotationInternal(HumanTrait.GetBoneIndexFromMono((int)humanBoneId), rotation);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00002E98 File Offset: 0x00001098
		[NativeMethod("SetBoneLocalRotation")]
		private void SetBoneLocalRotationInternal(int humanBoneId, Quaternion rotation)
		{
			this.SetBoneLocalRotationInternal_Injected(humanBoneId, ref rotation);
		}

		// Token: 0x0600011C RID: 284
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern ScriptableObject GetBehaviour([NotNull("ArgumentNullException")] Type type);

		// Token: 0x0600011D RID: 285 RVA: 0x00002EA4 File Offset: 0x000010A4
		public T GetBehaviour<T>() where T : StateMachineBehaviour
		{
			return this.GetBehaviour(typeof(T)) as T;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00002ED0 File Offset: 0x000010D0
		private static T[] ConvertStateMachineBehaviour<T>(ScriptableObject[] rawObjects) where T : StateMachineBehaviour
		{
			bool flag = rawObjects == null;
			T[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				T[] array = new T[rawObjects.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (T)((object)rawObjects[i]);
				}
				result = array;
			}
			return result;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00002F1C File Offset: 0x0000111C
		public T[] GetBehaviours<T>() where T : StateMachineBehaviour
		{
			return Animator.ConvertStateMachineBehaviour<T>(this.InternalGetBehaviours(typeof(T)));
		}

		// Token: 0x06000120 RID: 288
		[FreeFunction(Name = "AnimatorBindings::InternalGetBehaviours", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern ScriptableObject[] InternalGetBehaviours([NotNull("ArgumentNullException")] Type type);

		// Token: 0x06000121 RID: 289 RVA: 0x00002F44 File Offset: 0x00001144
		public StateMachineBehaviour[] GetBehaviours(int fullPathHash, int layerIndex)
		{
			return this.InternalGetBehavioursByKey(fullPathHash, layerIndex, typeof(StateMachineBehaviour)) as StateMachineBehaviour[];
		}

		// Token: 0x06000122 RID: 290
		[FreeFunction(Name = "AnimatorBindings::InternalGetBehavioursByKey", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern ScriptableObject[] InternalGetBehavioursByKey(int fullPathHash, int layerIndex, [NotNull("ArgumentNullException")] Type type);

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000123 RID: 291
		// (set) Token: 0x06000124 RID: 292
		public extern bool stabilizeFeet { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000125 RID: 293
		public extern int layerCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000126 RID: 294
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetLayerName(int layerIndex);

		// Token: 0x06000127 RID: 295
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetLayerIndex(string layerName);

		// Token: 0x06000128 RID: 296
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetLayerWeight(int layerIndex);

		// Token: 0x06000129 RID: 297
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetLayerWeight(int layerIndex, float weight);

		// Token: 0x0600012A RID: 298
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetAnimatorStateInfo(int layerIndex, StateInfoIndex stateInfoIndex, out AnimatorStateInfo info);

		// Token: 0x0600012B RID: 299 RVA: 0x00002F70 File Offset: 0x00001170
		public AnimatorStateInfo GetCurrentAnimatorStateInfo(int layerIndex)
		{
			AnimatorStateInfo result;
			this.GetAnimatorStateInfo(layerIndex, StateInfoIndex.CurrentState, out result);
			return result;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00002F90 File Offset: 0x00001190
		public AnimatorStateInfo GetNextAnimatorStateInfo(int layerIndex)
		{
			AnimatorStateInfo result;
			this.GetAnimatorStateInfo(layerIndex, StateInfoIndex.NextState, out result);
			return result;
		}

		// Token: 0x0600012D RID: 301
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetAnimatorTransitionInfo(int layerIndex, out AnimatorTransitionInfo info);

		// Token: 0x0600012E RID: 302 RVA: 0x00002FB0 File Offset: 0x000011B0
		public AnimatorTransitionInfo GetAnimatorTransitionInfo(int layerIndex)
		{
			AnimatorTransitionInfo result;
			this.GetAnimatorTransitionInfo(layerIndex, out result);
			return result;
		}

		// Token: 0x0600012F RID: 303
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetAnimatorClipInfoCount(int layerIndex, bool current);

		// Token: 0x06000130 RID: 304 RVA: 0x00002FD0 File Offset: 0x000011D0
		public int GetCurrentAnimatorClipInfoCount(int layerIndex)
		{
			return this.GetAnimatorClipInfoCount(layerIndex, true);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00002FEC File Offset: 0x000011EC
		public int GetNextAnimatorClipInfoCount(int layerIndex)
		{
			return this.GetAnimatorClipInfoCount(layerIndex, false);
		}

		// Token: 0x06000132 RID: 306
		[FreeFunction(Name = "AnimatorBindings::GetCurrentAnimatorClipInfo", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimatorClipInfo[] GetCurrentAnimatorClipInfo(int layerIndex);

		// Token: 0x06000133 RID: 307
		[FreeFunction(Name = "AnimatorBindings::GetNextAnimatorClipInfo", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimatorClipInfo[] GetNextAnimatorClipInfo(int layerIndex);

		// Token: 0x06000134 RID: 308 RVA: 0x00003008 File Offset: 0x00001208
		public void GetCurrentAnimatorClipInfo(int layerIndex, List<AnimatorClipInfo> clips)
		{
			bool flag = clips == null;
			if (flag)
			{
				throw new ArgumentNullException("clips");
			}
			this.GetAnimatorClipInfoInternal(layerIndex, true, clips);
		}

		// Token: 0x06000135 RID: 309
		[FreeFunction(Name = "AnimatorBindings::GetAnimatorClipInfoInternal", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetAnimatorClipInfoInternal(int layerIndex, bool isCurrent, object clips);

		// Token: 0x06000136 RID: 310 RVA: 0x00003034 File Offset: 0x00001234
		public void GetNextAnimatorClipInfo(int layerIndex, List<AnimatorClipInfo> clips)
		{
			bool flag = clips == null;
			if (flag)
			{
				throw new ArgumentNullException("clips");
			}
			this.GetAnimatorClipInfoInternal(layerIndex, false, clips);
		}

		// Token: 0x06000137 RID: 311
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsInTransition(int layerIndex);

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000138 RID: 312
		public extern AnimatorControllerParameter[] parameters { [FreeFunction(Name = "AnimatorBindings::GetParameters", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000139 RID: 313
		public extern int parameterCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600013A RID: 314
		[FreeFunction(Name = "AnimatorBindings::GetParameterInternal", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AnimatorControllerParameter GetParameterInternal(int index);

		// Token: 0x0600013B RID: 315 RVA: 0x00003060 File Offset: 0x00001260
		public AnimatorControllerParameter GetParameter(int index)
		{
			AnimatorControllerParameter parameterInternal = this.GetParameterInternal(index);
			bool flag = parameterInternal.m_Type == (AnimatorControllerParameterType)0;
			if (flag)
			{
				throw new IndexOutOfRangeException("Index must be between 0 and " + this.parameterCount.ToString());
			}
			return parameterInternal;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600013C RID: 316
		// (set) Token: 0x0600013D RID: 317
		public extern float feetPivotActive { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600013E RID: 318
		public extern float pivotWeight { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000030A8 File Offset: 0x000012A8
		public Vector3 pivotPosition
		{
			get
			{
				Vector3 result;
				this.get_pivotPosition_Injected(out result);
				return result;
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000030BE File Offset: 0x000012BE
		private void MatchTarget(Vector3 matchPosition, Quaternion matchRotation, int targetBodyPart, MatchTargetWeightMask weightMask, float startNormalizedTime, float targetNormalizedTime, bool completeMatch)
		{
			this.MatchTarget_Injected(ref matchPosition, ref matchRotation, targetBodyPart, ref weightMask, startNormalizedTime, targetNormalizedTime, completeMatch);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000030D3 File Offset: 0x000012D3
		public void MatchTarget(Vector3 matchPosition, Quaternion matchRotation, AvatarTarget targetBodyPart, MatchTargetWeightMask weightMask, float startNormalizedTime)
		{
			this.MatchTarget(matchPosition, matchRotation, (int)targetBodyPart, weightMask, startNormalizedTime, 1f, true);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000030EA File Offset: 0x000012EA
		public void MatchTarget(Vector3 matchPosition, Quaternion matchRotation, AvatarTarget targetBodyPart, MatchTargetWeightMask weightMask, float startNormalizedTime, [UnityEngine.Internal.DefaultValue("1")] float targetNormalizedTime)
		{
			this.MatchTarget(matchPosition, matchRotation, (int)targetBodyPart, weightMask, startNormalizedTime, targetNormalizedTime, true);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000030FE File Offset: 0x000012FE
		public void MatchTarget(Vector3 matchPosition, Quaternion matchRotation, AvatarTarget targetBodyPart, MatchTargetWeightMask weightMask, float startNormalizedTime, [UnityEngine.Internal.DefaultValue("1")] float targetNormalizedTime, [UnityEngine.Internal.DefaultValue("true")] bool completeMatch)
		{
			this.MatchTarget(matchPosition, matchRotation, (int)targetBodyPart, weightMask, startNormalizedTime, targetNormalizedTime, completeMatch);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00003113 File Offset: 0x00001313
		public void InterruptMatchTarget()
		{
			this.InterruptMatchTarget(true);
		}

		// Token: 0x06000145 RID: 325
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InterruptMatchTarget([UnityEngine.Internal.DefaultValue("true")] bool completeMatch);

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000146 RID: 326
		public extern bool isMatchingTarget { [NativeMethod("IsMatchingTarget")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000147 RID: 327
		// (set) Token: 0x06000148 RID: 328
		public extern float speed { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000149 RID: 329 RVA: 0x0000311E File Offset: 0x0000131E
		[Obsolete("ForceStateNormalizedTime is deprecated. Please use Play or CrossFade instead.")]
		public void ForceStateNormalizedTime(float normalizedTime)
		{
			this.Play(0, 0, normalizedTime);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000312C File Offset: 0x0000132C
		public void CrossFadeInFixedTime(string stateName, float fixedTransitionDuration)
		{
			float normalizedTransitionTime = 0f;
			float fixedTimeOffset = 0f;
			int layer = -1;
			this.CrossFadeInFixedTime(Animator.StringToHash(stateName), fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000315C File Offset: 0x0000135C
		public void CrossFadeInFixedTime(string stateName, float fixedTransitionDuration, int layer)
		{
			float normalizedTransitionTime = 0f;
			float fixedTimeOffset = 0f;
			this.CrossFadeInFixedTime(Animator.StringToHash(stateName), fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00003188 File Offset: 0x00001388
		public void CrossFadeInFixedTime(string stateName, float fixedTransitionDuration, int layer, float fixedTimeOffset)
		{
			float normalizedTransitionTime = 0f;
			this.CrossFadeInFixedTime(Animator.StringToHash(stateName), fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000031AE File Offset: 0x000013AE
		public void CrossFadeInFixedTime(string stateName, float fixedTransitionDuration, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("0.0f")] float fixedTimeOffset, [UnityEngine.Internal.DefaultValue("0.0f")] float normalizedTransitionTime)
		{
			this.CrossFadeInFixedTime(Animator.StringToHash(stateName), fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000031C4 File Offset: 0x000013C4
		public void CrossFadeInFixedTime(int stateHashName, float fixedTransitionDuration, int layer, float fixedTimeOffset)
		{
			float normalizedTransitionTime = 0f;
			this.CrossFadeInFixedTime(stateHashName, fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000031E8 File Offset: 0x000013E8
		public void CrossFadeInFixedTime(int stateHashName, float fixedTransitionDuration, int layer)
		{
			float normalizedTransitionTime = 0f;
			float fixedTimeOffset = 0f;
			this.CrossFadeInFixedTime(stateHashName, fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00003210 File Offset: 0x00001410
		public void CrossFadeInFixedTime(int stateHashName, float fixedTransitionDuration)
		{
			float normalizedTransitionTime = 0f;
			float fixedTimeOffset = 0f;
			int layer = -1;
			this.CrossFadeInFixedTime(stateHashName, fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x06000151 RID: 337
		[FreeFunction(Name = "AnimatorBindings::CrossFadeInFixedTime", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CrossFadeInFixedTime(int stateHashName, float fixedTransitionDuration, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("0.0f")] float fixedTimeOffset, [UnityEngine.Internal.DefaultValue("0.0f")] float normalizedTransitionTime);

		// Token: 0x06000152 RID: 338
		[FreeFunction(Name = "AnimatorBindings::WriteDefaultValues", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void WriteDefaultValues();

		// Token: 0x06000153 RID: 339 RVA: 0x00003238 File Offset: 0x00001438
		public void CrossFade(string stateName, float normalizedTransitionDuration, int layer, float normalizedTimeOffset)
		{
			float normalizedTransitionTime = 0f;
			this.CrossFade(stateName, normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000325C File Offset: 0x0000145C
		public void CrossFade(string stateName, float normalizedTransitionDuration, int layer)
		{
			float normalizedTransitionTime = 0f;
			float negativeInfinity = float.NegativeInfinity;
			this.CrossFade(stateName, normalizedTransitionDuration, layer, negativeInfinity, normalizedTransitionTime);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00003284 File Offset: 0x00001484
		public void CrossFade(string stateName, float normalizedTransitionDuration)
		{
			float normalizedTransitionTime = 0f;
			float negativeInfinity = float.NegativeInfinity;
			int layer = -1;
			this.CrossFade(stateName, normalizedTransitionDuration, layer, negativeInfinity, normalizedTransitionTime);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000032AC File Offset: 0x000014AC
		public void CrossFade(string stateName, float normalizedTransitionDuration, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float normalizedTimeOffset, [UnityEngine.Internal.DefaultValue("0.0f")] float normalizedTransitionTime)
		{
			this.CrossFade(Animator.StringToHash(stateName), normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x06000157 RID: 343
		[FreeFunction(Name = "AnimatorBindings::CrossFade", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CrossFade(int stateHashName, float normalizedTransitionDuration, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("0.0f")] float normalizedTimeOffset, [UnityEngine.Internal.DefaultValue("0.0f")] float normalizedTransitionTime);

		// Token: 0x06000158 RID: 344 RVA: 0x000032C4 File Offset: 0x000014C4
		public void CrossFade(int stateHashName, float normalizedTransitionDuration, int layer, float normalizedTimeOffset)
		{
			float normalizedTransitionTime = 0f;
			this.CrossFade(stateHashName, normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000032E8 File Offset: 0x000014E8
		public void CrossFade(int stateHashName, float normalizedTransitionDuration, int layer)
		{
			float normalizedTransitionTime = 0f;
			float negativeInfinity = float.NegativeInfinity;
			this.CrossFade(stateHashName, normalizedTransitionDuration, layer, negativeInfinity, normalizedTransitionTime);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00003310 File Offset: 0x00001510
		public void CrossFade(int stateHashName, float normalizedTransitionDuration)
		{
			float normalizedTransitionTime = 0f;
			float negativeInfinity = float.NegativeInfinity;
			int layer = -1;
			this.CrossFade(stateHashName, normalizedTransitionDuration, layer, negativeInfinity, normalizedTransitionTime);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00003338 File Offset: 0x00001538
		public void PlayInFixedTime(string stateName, int layer)
		{
			float negativeInfinity = float.NegativeInfinity;
			this.PlayInFixedTime(stateName, layer, negativeInfinity);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00003358 File Offset: 0x00001558
		public void PlayInFixedTime(string stateName)
		{
			float negativeInfinity = float.NegativeInfinity;
			int layer = -1;
			this.PlayInFixedTime(stateName, layer, negativeInfinity);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00003378 File Offset: 0x00001578
		public void PlayInFixedTime(string stateName, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float fixedTime)
		{
			this.PlayInFixedTime(Animator.StringToHash(stateName), layer, fixedTime);
		}

		// Token: 0x0600015E RID: 350
		[FreeFunction(Name = "AnimatorBindings::PlayInFixedTime", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void PlayInFixedTime(int stateNameHash, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float fixedTime);

		// Token: 0x0600015F RID: 351 RVA: 0x0000338C File Offset: 0x0000158C
		public void PlayInFixedTime(int stateNameHash, int layer)
		{
			float negativeInfinity = float.NegativeInfinity;
			this.PlayInFixedTime(stateNameHash, layer, negativeInfinity);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000033AC File Offset: 0x000015AC
		public void PlayInFixedTime(int stateNameHash)
		{
			float negativeInfinity = float.NegativeInfinity;
			int layer = -1;
			this.PlayInFixedTime(stateNameHash, layer, negativeInfinity);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000033CC File Offset: 0x000015CC
		public void Play(string stateName, int layer)
		{
			float negativeInfinity = float.NegativeInfinity;
			this.Play(stateName, layer, negativeInfinity);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000033EC File Offset: 0x000015EC
		public void Play(string stateName)
		{
			float negativeInfinity = float.NegativeInfinity;
			int layer = -1;
			this.Play(stateName, layer, negativeInfinity);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000340C File Offset: 0x0000160C
		public void Play(string stateName, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float normalizedTime)
		{
			this.Play(Animator.StringToHash(stateName), layer, normalizedTime);
		}

		// Token: 0x06000164 RID: 356
		[FreeFunction(Name = "AnimatorBindings::Play", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Play(int stateNameHash, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float normalizedTime);

		// Token: 0x06000165 RID: 357 RVA: 0x00003420 File Offset: 0x00001620
		public void Play(int stateNameHash, int layer)
		{
			float negativeInfinity = float.NegativeInfinity;
			this.Play(stateNameHash, layer, negativeInfinity);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00003440 File Offset: 0x00001640
		public void Play(int stateNameHash)
		{
			float negativeInfinity = float.NegativeInfinity;
			int layer = -1;
			this.Play(stateNameHash, layer, negativeInfinity);
		}

		// Token: 0x06000167 RID: 359
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTarget(AvatarTarget targetIndex, float targetNormalizedTime);

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00003460 File Offset: 0x00001660
		public Vector3 targetPosition
		{
			get
			{
				Vector3 result;
				this.get_targetPosition_Injected(out result);
				return result;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00003478 File Offset: 0x00001678
		public Quaternion targetRotation
		{
			get
			{
				Quaternion result;
				this.get_targetRotation_Injected(out result);
				return result;
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00003490 File Offset: 0x00001690
		[Obsolete("Use mask and layers to control subset of transfroms in a skeleton.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool IsControlled(Transform transform)
		{
			return false;
		}

		// Token: 0x0600016B RID: 363
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsBoneTransform(Transform transform);

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600016C RID: 364
		internal extern Transform avatarRoot { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600016D RID: 365 RVA: 0x000034A4 File Offset: 0x000016A4
		public Transform GetBoneTransform(HumanBodyBones humanBoneId)
		{
			bool flag = humanBoneId < HumanBodyBones.Hips || humanBoneId >= HumanBodyBones.LastBone;
			if (flag)
			{
				throw new IndexOutOfRangeException("humanBoneId must be between 0 and " + HumanBodyBones.LastBone.ToString());
			}
			return this.GetBoneTransformInternal(HumanTrait.GetBoneIndexFromMono((int)humanBoneId));
		}

		// Token: 0x0600016E RID: 366
		[NativeMethod("GetBoneTransform")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Transform GetBoneTransformInternal(int humanBoneId);

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600016F RID: 367
		// (set) Token: 0x06000170 RID: 368
		public extern AnimatorCullingMode cullingMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000171 RID: 369
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StartPlayback();

		// Token: 0x06000172 RID: 370
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopPlayback();

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000173 RID: 371
		// (set) Token: 0x06000174 RID: 372
		public extern float playbackTime { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000175 RID: 373
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StartRecording(int frameCount);

		// Token: 0x06000176 RID: 374
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopRecording();

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000177 RID: 375 RVA: 0x000034F8 File Offset: 0x000016F8
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00002059 File Offset: 0x00000259
		public float recorderStartTime
		{
			get
			{
				return this.GetRecorderStartTime();
			}
			set
			{
			}
		}

		// Token: 0x06000179 RID: 377
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetRecorderStartTime();

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00003510 File Offset: 0x00001710
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00002059 File Offset: 0x00000259
		public float recorderStopTime
		{
			get
			{
				return this.GetRecorderStopTime();
			}
			set
			{
			}
		}

		// Token: 0x0600017C RID: 380
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetRecorderStopTime();

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600017D RID: 381
		public extern AnimatorRecorderMode recorderMode { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600017E RID: 382
		// (set) Token: 0x0600017F RID: 383
		public extern RuntimeAnimatorController runtimeAnimatorController { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000180 RID: 384
		public extern bool hasBoundPlayables { [NativeMethod("HasBoundPlayables")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000181 RID: 385
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void ClearInternalControllerPlayable();

		// Token: 0x06000182 RID: 386
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasState(int layerIndex, int stateID);

		// Token: 0x06000183 RID: 387
		[NativeMethod(Name = "ScriptingStringToCRC32", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int StringToHash(string name);

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000184 RID: 388
		// (set) Token: 0x06000185 RID: 389
		public extern Avatar avatar { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000186 RID: 390
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string GetStats();

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00003528 File Offset: 0x00001728
		public PlayableGraph playableGraph
		{
			get
			{
				PlayableGraph result = default(PlayableGraph);
				this.GetCurrentGraph(ref result);
				return result;
			}
		}

		// Token: 0x06000188 RID: 392
		[FreeFunction(Name = "AnimatorBindings::GetCurrentGraph", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetCurrentGraph(ref PlayableGraph graph);

		// Token: 0x06000189 RID: 393 RVA: 0x0000354C File Offset: 0x0000174C
		private void CheckIfInIKPass()
		{
			bool flag = this.logWarnings && !this.IsInIKPass();
			if (flag)
			{
				Debug.LogWarning("Setting and getting Body Position/Rotation, IK Goals, Lookat and BoneLocalRotation should only be done in OnAnimatorIK or OnStateIK");
			}
		}

		// Token: 0x0600018A RID: 394
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsInIKPass();

		// Token: 0x0600018B RID: 395
		[FreeFunction(Name = "AnimatorBindings::SetFloatString", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatString(string name, float value);

		// Token: 0x0600018C RID: 396
		[FreeFunction(Name = "AnimatorBindings::SetFloatID", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatID(int id, float value);

		// Token: 0x0600018D RID: 397
		[FreeFunction(Name = "AnimatorBindings::GetFloatString", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetFloatString(string name);

		// Token: 0x0600018E RID: 398
		[FreeFunction(Name = "AnimatorBindings::GetFloatID", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetFloatID(int id);

		// Token: 0x0600018F RID: 399
		[FreeFunction(Name = "AnimatorBindings::SetBoolString", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBoolString(string name, bool value);

		// Token: 0x06000190 RID: 400
		[FreeFunction(Name = "AnimatorBindings::SetBoolID", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBoolID(int id, bool value);

		// Token: 0x06000191 RID: 401
		[FreeFunction(Name = "AnimatorBindings::GetBoolString", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetBoolString(string name);

		// Token: 0x06000192 RID: 402
		[FreeFunction(Name = "AnimatorBindings::GetBoolID", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetBoolID(int id);

		// Token: 0x06000193 RID: 403
		[FreeFunction(Name = "AnimatorBindings::SetIntegerString", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetIntegerString(string name, int value);

		// Token: 0x06000194 RID: 404
		[FreeFunction(Name = "AnimatorBindings::SetIntegerID", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetIntegerID(int id, int value);

		// Token: 0x06000195 RID: 405
		[FreeFunction(Name = "AnimatorBindings::GetIntegerString", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetIntegerString(string name);

		// Token: 0x06000196 RID: 406
		[FreeFunction(Name = "AnimatorBindings::GetIntegerID", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetIntegerID(int id);

		// Token: 0x06000197 RID: 407
		[FreeFunction(Name = "AnimatorBindings::SetTriggerString", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTriggerString(string name);

		// Token: 0x06000198 RID: 408
		[FreeFunction(Name = "AnimatorBindings::SetTriggerID", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTriggerID(int id);

		// Token: 0x06000199 RID: 409
		[FreeFunction(Name = "AnimatorBindings::ResetTriggerString", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ResetTriggerString(string name);

		// Token: 0x0600019A RID: 410
		[FreeFunction(Name = "AnimatorBindings::ResetTriggerID", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ResetTriggerID(int id);

		// Token: 0x0600019B RID: 411
		[FreeFunction(Name = "AnimatorBindings::IsParameterControlledByCurveString", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsParameterControlledByCurveString(string name);

		// Token: 0x0600019C RID: 412
		[FreeFunction(Name = "AnimatorBindings::IsParameterControlledByCurveID", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsParameterControlledByCurveID(int id);

		// Token: 0x0600019D RID: 413
		[FreeFunction(Name = "AnimatorBindings::SetFloatStringDamp", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatStringDamp(string name, float value, float dampTime, float deltaTime);

		// Token: 0x0600019E RID: 414
		[FreeFunction(Name = "AnimatorBindings::SetFloatIDDamp", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatIDDamp(int id, float value, float dampTime, float deltaTime);

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600019F RID: 415
		// (set) Token: 0x060001A0 RID: 416
		public extern bool layersAffectMassCenter { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001A1 RID: 417
		public extern float leftFeetBottomHeight { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001A2 RID: 418
		public extern float rightFeetBottomHeight { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001A3 RID: 419
		[NativeConditional("UNITY_EDITOR")]
		internal extern bool supportsOnAnimatorMove { [NativeMethod("SupportsOnAnimatorMove")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060001A4 RID: 420
		[NativeConditional("UNITY_EDITOR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void OnUpdateModeChanged();

		// Token: 0x060001A5 RID: 421
		[NativeConditional("UNITY_EDITOR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void OnCullingModeChanged();

		// Token: 0x060001A6 RID: 422
		[NativeConditional("UNITY_EDITOR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void WriteDefaultPose();

		// Token: 0x060001A7 RID: 423
		[NativeMethod("UpdateWithDelta")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Update(float deltaTime);

		// Token: 0x060001A8 RID: 424 RVA: 0x0000357D File Offset: 0x0000177D
		public void Rebind()
		{
			this.Rebind(true);
		}

		// Token: 0x060001A9 RID: 425
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Rebind(bool writeDefaultValues);

		// Token: 0x060001AA RID: 426
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ApplyBuiltinRootMotion();

		// Token: 0x060001AB RID: 427 RVA: 0x00003588 File Offset: 0x00001788
		[NativeConditional("UNITY_EDITOR")]
		internal void EvaluateController()
		{
			this.EvaluateController(0f);
		}

		// Token: 0x060001AC RID: 428
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void EvaluateController(float deltaTime);

		// Token: 0x060001AD RID: 429 RVA: 0x00003598 File Offset: 0x00001798
		[NativeConditional("UNITY_EDITOR")]
		internal string GetCurrentStateName(int layerIndex)
		{
			return this.GetAnimatorStateName(layerIndex, true);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000035B4 File Offset: 0x000017B4
		[NativeConditional("UNITY_EDITOR")]
		internal string GetNextStateName(int layerIndex)
		{
			return this.GetAnimatorStateName(layerIndex, false);
		}

		// Token: 0x060001AF RID: 431
		[NativeConditional("UNITY_EDITOR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string GetAnimatorStateName(int layerIndex, bool current);

		// Token: 0x060001B0 RID: 432
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string ResolveHash(int hash);

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001B1 RID: 433
		// (set) Token: 0x060001B2 RID: 434
		public extern bool logWarnings { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001B3 RID: 435
		// (set) Token: 0x060001B4 RID: 436
		public extern bool fireEvents { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x000035D0 File Offset: 0x000017D0
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x000035E8 File Offset: 0x000017E8
		[Obsolete("keepAnimatorControllerStateOnDisable is deprecated, use keepAnimatorStateOnDisable instead. (UnityUpgradable) -> keepAnimatorStateOnDisable", false)]
		public bool keepAnimatorControllerStateOnDisable
		{
			get
			{
				return this.keepAnimatorStateOnDisable;
			}
			set
			{
				this.keepAnimatorStateOnDisable = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001B7 RID: 439
		// (set) Token: 0x060001B8 RID: 440
		public extern bool keepAnimatorStateOnDisable { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001B9 RID: 441
		// (set) Token: 0x060001BA RID: 442
		public extern bool writeDefaultValuesOnDisable { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060001BB RID: 443 RVA: 0x000035F4 File Offset: 0x000017F4
		[Obsolete("GetVector is deprecated.")]
		public Vector3 GetVector(string name)
		{
			return Vector3.zero;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000360C File Offset: 0x0000180C
		[Obsolete("GetVector is deprecated.")]
		public Vector3 GetVector(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SetVector is deprecated.")]
		public void SetVector(string name, Vector3 value)
		{
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SetVector is deprecated.")]
		public void SetVector(int id, Vector3 value)
		{
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00003624 File Offset: 0x00001824
		[Obsolete("GetQuaternion is deprecated.")]
		public Quaternion GetQuaternion(string name)
		{
			return Quaternion.identity;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000363C File Offset: 0x0000183C
		[Obsolete("GetQuaternion is deprecated.")]
		public Quaternion GetQuaternion(int id)
		{
			return Quaternion.identity;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SetQuaternion is deprecated.")]
		public void SetQuaternion(string name, Quaternion value)
		{
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SetQuaternion is deprecated.")]
		public void SetQuaternion(int id, Quaternion value)
		{
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00002288 File Offset: 0x00000488
		public Animator()
		{
		}

		// Token: 0x060001C4 RID: 452
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_deltaPosition_Injected(out Vector3 ret);

		// Token: 0x060001C5 RID: 453
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_deltaRotation_Injected(out Quaternion ret);

		// Token: 0x060001C6 RID: 454
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_velocity_Injected(out Vector3 ret);

		// Token: 0x060001C7 RID: 455
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_angularVelocity_Injected(out Vector3 ret);

		// Token: 0x060001C8 RID: 456
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rootPosition_Injected(out Vector3 ret);

		// Token: 0x060001C9 RID: 457
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_rootPosition_Injected(ref Vector3 value);

		// Token: 0x060001CA RID: 458
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rootRotation_Injected(out Quaternion ret);

		// Token: 0x060001CB RID: 459
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_rootRotation_Injected(ref Quaternion value);

		// Token: 0x060001CC RID: 460
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_bodyPositionInternal_Injected(out Vector3 ret);

		// Token: 0x060001CD RID: 461
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_bodyPositionInternal_Injected(ref Vector3 value);

		// Token: 0x060001CE RID: 462
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_bodyRotationInternal_Injected(out Quaternion ret);

		// Token: 0x060001CF RID: 463
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_bodyRotationInternal_Injected(ref Quaternion value);

		// Token: 0x060001D0 RID: 464
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetGoalPosition_Injected(AvatarIKGoal goal, out Vector3 ret);

		// Token: 0x060001D1 RID: 465
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetGoalPosition_Injected(AvatarIKGoal goal, ref Vector3 goalPosition);

		// Token: 0x060001D2 RID: 466
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetGoalRotation_Injected(AvatarIKGoal goal, out Quaternion ret);

		// Token: 0x060001D3 RID: 467
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetGoalRotation_Injected(AvatarIKGoal goal, ref Quaternion goalRotation);

		// Token: 0x060001D4 RID: 468
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetHintPosition_Injected(AvatarIKHint hint, out Vector3 ret);

		// Token: 0x060001D5 RID: 469
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetHintPosition_Injected(AvatarIKHint hint, ref Vector3 hintPosition);

		// Token: 0x060001D6 RID: 470
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetLookAtPositionInternal_Injected(ref Vector3 lookAtPosition);

		// Token: 0x060001D7 RID: 471
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBoneLocalRotationInternal_Injected(int humanBoneId, ref Quaternion rotation);

		// Token: 0x060001D8 RID: 472
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_pivotPosition_Injected(out Vector3 ret);

		// Token: 0x060001D9 RID: 473
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void MatchTarget_Injected(ref Vector3 matchPosition, ref Quaternion matchRotation, int targetBodyPart, ref MatchTargetWeightMask weightMask, float startNormalizedTime, float targetNormalizedTime, bool completeMatch);

		// Token: 0x060001DA RID: 474
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_targetPosition_Injected(out Vector3 ret);

		// Token: 0x060001DB RID: 475
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_targetRotation_Injected(out Quaternion ret);
	}
}
