using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000060 RID: 96
	[NativeHeader("Modules/Animation/ScriptBindings/AnimatorControllerPlayable.bindings.h")]
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/RuntimeAnimatorController.h")]
	[NativeHeader("Modules/Animation/AnimatorInfo.h")]
	[StaticAccessor("AnimatorControllerPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Animation/Director/AnimatorControllerPlayable.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/Animator.bindings.h")]
	public struct AnimatorControllerPlayable : IPlayable, IEquatable<AnimatorControllerPlayable>
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x000071D4 File Offset: 0x000053D4
		public static AnimatorControllerPlayable Null
		{
			get
			{
				return AnimatorControllerPlayable.m_NullPlayable;
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000071EC File Offset: 0x000053EC
		public static AnimatorControllerPlayable Create(PlayableGraph graph, RuntimeAnimatorController controller)
		{
			PlayableHandle handle = AnimatorControllerPlayable.CreateHandle(graph, controller);
			return new AnimatorControllerPlayable(handle);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000720C File Offset: 0x0000540C
		private static PlayableHandle CreateHandle(PlayableGraph graph, RuntimeAnimatorController controller)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimatorControllerPlayable.CreateHandleInternal(graph, controller, ref @null);
			PlayableHandle result;
			if (flag)
			{
				result = PlayableHandle.Null;
			}
			else
			{
				result = @null;
			}
			return result;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000723D File Offset: 0x0000543D
		internal AnimatorControllerPlayable(PlayableHandle handle)
		{
			this.m_Handle = PlayableHandle.Null;
			this.SetHandle(handle);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00007254 File Offset: 0x00005454
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000726C File Offset: 0x0000546C
		public void SetHandle(PlayableHandle handle)
		{
			bool flag = this.m_Handle.IsValid();
			if (flag)
			{
				throw new InvalidOperationException("Cannot call IPlayable.SetHandle on an instance that already contains a valid handle.");
			}
			bool flag2 = handle.IsValid();
			if (flag2)
			{
				bool flag3 = !handle.IsPlayableOfType<AnimatorControllerPlayable>();
				if (flag3)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimatorControllerPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x000072C4 File Offset: 0x000054C4
		public static implicit operator Playable(AnimatorControllerPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000072E4 File Offset: 0x000054E4
		public static explicit operator AnimatorControllerPlayable(Playable playable)
		{
			return new AnimatorControllerPlayable(playable.GetHandle());
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00007304 File Offset: 0x00005504
		public bool Equals(AnimatorControllerPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00007328 File Offset: 0x00005528
		public float GetFloat(string name)
		{
			return AnimatorControllerPlayable.GetFloatString(ref this.m_Handle, name);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00007348 File Offset: 0x00005548
		public float GetFloat(int id)
		{
			return AnimatorControllerPlayable.GetFloatID(ref this.m_Handle, id);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00007366 File Offset: 0x00005566
		public void SetFloat(string name, float value)
		{
			AnimatorControllerPlayable.SetFloatString(ref this.m_Handle, name, value);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00007377 File Offset: 0x00005577
		public void SetFloat(int id, float value)
		{
			AnimatorControllerPlayable.SetFloatID(ref this.m_Handle, id, value);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00007388 File Offset: 0x00005588
		public bool GetBool(string name)
		{
			return AnimatorControllerPlayable.GetBoolString(ref this.m_Handle, name);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x000073A8 File Offset: 0x000055A8
		public bool GetBool(int id)
		{
			return AnimatorControllerPlayable.GetBoolID(ref this.m_Handle, id);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x000073C6 File Offset: 0x000055C6
		public void SetBool(string name, bool value)
		{
			AnimatorControllerPlayable.SetBoolString(ref this.m_Handle, name, value);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x000073D7 File Offset: 0x000055D7
		public void SetBool(int id, bool value)
		{
			AnimatorControllerPlayable.SetBoolID(ref this.m_Handle, id, value);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000073E8 File Offset: 0x000055E8
		public int GetInteger(string name)
		{
			return AnimatorControllerPlayable.GetIntegerString(ref this.m_Handle, name);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00007408 File Offset: 0x00005608
		public int GetInteger(int id)
		{
			return AnimatorControllerPlayable.GetIntegerID(ref this.m_Handle, id);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00007426 File Offset: 0x00005626
		public void SetInteger(string name, int value)
		{
			AnimatorControllerPlayable.SetIntegerString(ref this.m_Handle, name, value);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00007437 File Offset: 0x00005637
		public void SetInteger(int id, int value)
		{
			AnimatorControllerPlayable.SetIntegerID(ref this.m_Handle, id, value);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00007448 File Offset: 0x00005648
		public void SetTrigger(string name)
		{
			AnimatorControllerPlayable.SetTriggerString(ref this.m_Handle, name);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00007458 File Offset: 0x00005658
		public void SetTrigger(int id)
		{
			AnimatorControllerPlayable.SetTriggerID(ref this.m_Handle, id);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00007468 File Offset: 0x00005668
		public void ResetTrigger(string name)
		{
			AnimatorControllerPlayable.ResetTriggerString(ref this.m_Handle, name);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00007478 File Offset: 0x00005678
		public void ResetTrigger(int id)
		{
			AnimatorControllerPlayable.ResetTriggerID(ref this.m_Handle, id);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00007488 File Offset: 0x00005688
		public bool IsParameterControlledByCurve(string name)
		{
			return AnimatorControllerPlayable.IsParameterControlledByCurveString(ref this.m_Handle, name);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x000074A8 File Offset: 0x000056A8
		public bool IsParameterControlledByCurve(int id)
		{
			return AnimatorControllerPlayable.IsParameterControlledByCurveID(ref this.m_Handle, id);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x000074C8 File Offset: 0x000056C8
		public int GetLayerCount()
		{
			return AnimatorControllerPlayable.GetLayerCountInternal(ref this.m_Handle);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x000074E8 File Offset: 0x000056E8
		public string GetLayerName(int layerIndex)
		{
			return AnimatorControllerPlayable.GetLayerNameInternal(ref this.m_Handle, layerIndex);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00007508 File Offset: 0x00005708
		public int GetLayerIndex(string layerName)
		{
			return AnimatorControllerPlayable.GetLayerIndexInternal(ref this.m_Handle, layerName);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00007528 File Offset: 0x00005728
		public float GetLayerWeight(int layerIndex)
		{
			return AnimatorControllerPlayable.GetLayerWeightInternal(ref this.m_Handle, layerIndex);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00007546 File Offset: 0x00005746
		public void SetLayerWeight(int layerIndex, float weight)
		{
			AnimatorControllerPlayable.SetLayerWeightInternal(ref this.m_Handle, layerIndex, weight);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00007558 File Offset: 0x00005758
		public AnimatorStateInfo GetCurrentAnimatorStateInfo(int layerIndex)
		{
			return AnimatorControllerPlayable.GetCurrentAnimatorStateInfoInternal(ref this.m_Handle, layerIndex);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00007578 File Offset: 0x00005778
		public AnimatorStateInfo GetNextAnimatorStateInfo(int layerIndex)
		{
			return AnimatorControllerPlayable.GetNextAnimatorStateInfoInternal(ref this.m_Handle, layerIndex);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00007598 File Offset: 0x00005798
		public AnimatorTransitionInfo GetAnimatorTransitionInfo(int layerIndex)
		{
			return AnimatorControllerPlayable.GetAnimatorTransitionInfoInternal(ref this.m_Handle, layerIndex);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x000075B8 File Offset: 0x000057B8
		public AnimatorClipInfo[] GetCurrentAnimatorClipInfo(int layerIndex)
		{
			return AnimatorControllerPlayable.GetCurrentAnimatorClipInfoInternal(ref this.m_Handle, layerIndex);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x000075D8 File Offset: 0x000057D8
		public void GetCurrentAnimatorClipInfo(int layerIndex, List<AnimatorClipInfo> clips)
		{
			bool flag = clips == null;
			if (flag)
			{
				throw new ArgumentNullException("clips");
			}
			AnimatorControllerPlayable.GetAnimatorClipInfoInternal(ref this.m_Handle, layerIndex, true, clips);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00007608 File Offset: 0x00005808
		public void GetNextAnimatorClipInfo(int layerIndex, List<AnimatorClipInfo> clips)
		{
			bool flag = clips == null;
			if (flag)
			{
				throw new ArgumentNullException("clips");
			}
			AnimatorControllerPlayable.GetAnimatorClipInfoInternal(ref this.m_Handle, layerIndex, false, clips);
		}

		// Token: 0x0600050B RID: 1291
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAnimatorClipInfoInternal(ref PlayableHandle handle, int layerIndex, bool isCurrent, object clips);

		// Token: 0x0600050C RID: 1292 RVA: 0x00007638 File Offset: 0x00005838
		public int GetCurrentAnimatorClipInfoCount(int layerIndex)
		{
			return AnimatorControllerPlayable.GetAnimatorClipInfoCountInternal(ref this.m_Handle, layerIndex, true);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00007658 File Offset: 0x00005858
		public int GetNextAnimatorClipInfoCount(int layerIndex)
		{
			return AnimatorControllerPlayable.GetAnimatorClipInfoCountInternal(ref this.m_Handle, layerIndex, false);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00007678 File Offset: 0x00005878
		public AnimatorClipInfo[] GetNextAnimatorClipInfo(int layerIndex)
		{
			return AnimatorControllerPlayable.GetNextAnimatorClipInfoInternal(ref this.m_Handle, layerIndex);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00007698 File Offset: 0x00005898
		public bool IsInTransition(int layerIndex)
		{
			return AnimatorControllerPlayable.IsInTransitionInternal(ref this.m_Handle, layerIndex);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x000076B8 File Offset: 0x000058B8
		public int GetParameterCount()
		{
			return AnimatorControllerPlayable.GetParameterCountInternal(ref this.m_Handle);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x000076D8 File Offset: 0x000058D8
		public AnimatorControllerParameter GetParameter(int index)
		{
			AnimatorControllerParameter parameterInternal = AnimatorControllerPlayable.GetParameterInternal(ref this.m_Handle, index);
			bool flag = parameterInternal.m_Type == (AnimatorControllerParameterType)0;
			if (flag)
			{
				throw new IndexOutOfRangeException("Invalid parameter index.");
			}
			return parameterInternal;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00007710 File Offset: 0x00005910
		public void CrossFadeInFixedTime(string stateName, float transitionDuration)
		{
			AnimatorControllerPlayable.CrossFadeInFixedTimeInternal(ref this.m_Handle, AnimatorControllerPlayable.StringToHash(stateName), transitionDuration, -1, 0f);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000772C File Offset: 0x0000592C
		public void CrossFadeInFixedTime(string stateName, float transitionDuration, int layer)
		{
			AnimatorControllerPlayable.CrossFadeInFixedTimeInternal(ref this.m_Handle, AnimatorControllerPlayable.StringToHash(stateName), transitionDuration, layer, 0f);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00007748 File Offset: 0x00005948
		public void CrossFadeInFixedTime(string stateName, float transitionDuration, [DefaultValue("-1")] int layer, [DefaultValue("0.0f")] float fixedTime)
		{
			AnimatorControllerPlayable.CrossFadeInFixedTimeInternal(ref this.m_Handle, AnimatorControllerPlayable.StringToHash(stateName), transitionDuration, layer, fixedTime);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00007761 File Offset: 0x00005961
		public void CrossFadeInFixedTime(int stateNameHash, float transitionDuration)
		{
			AnimatorControllerPlayable.CrossFadeInFixedTimeInternal(ref this.m_Handle, stateNameHash, transitionDuration, -1, 0f);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00007778 File Offset: 0x00005978
		public void CrossFadeInFixedTime(int stateNameHash, float transitionDuration, int layer)
		{
			AnimatorControllerPlayable.CrossFadeInFixedTimeInternal(ref this.m_Handle, stateNameHash, transitionDuration, layer, 0f);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000778F File Offset: 0x0000598F
		public void CrossFadeInFixedTime(int stateNameHash, float transitionDuration, [DefaultValue("-1")] int layer, [DefaultValue("0.0f")] float fixedTime)
		{
			AnimatorControllerPlayable.CrossFadeInFixedTimeInternal(ref this.m_Handle, stateNameHash, transitionDuration, layer, fixedTime);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x000077A3 File Offset: 0x000059A3
		public void CrossFade(string stateName, float transitionDuration)
		{
			AnimatorControllerPlayable.CrossFadeInternal(ref this.m_Handle, AnimatorControllerPlayable.StringToHash(stateName), transitionDuration, -1, float.NegativeInfinity);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x000077BF File Offset: 0x000059BF
		public void CrossFade(string stateName, float transitionDuration, int layer)
		{
			AnimatorControllerPlayable.CrossFadeInternal(ref this.m_Handle, AnimatorControllerPlayable.StringToHash(stateName), transitionDuration, layer, float.NegativeInfinity);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x000077DB File Offset: 0x000059DB
		public void CrossFade(string stateName, float transitionDuration, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTime)
		{
			AnimatorControllerPlayable.CrossFadeInternal(ref this.m_Handle, AnimatorControllerPlayable.StringToHash(stateName), transitionDuration, layer, normalizedTime);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x000077F4 File Offset: 0x000059F4
		public void CrossFade(int stateNameHash, float transitionDuration)
		{
			AnimatorControllerPlayable.CrossFadeInternal(ref this.m_Handle, stateNameHash, transitionDuration, -1, float.NegativeInfinity);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0000780B File Offset: 0x00005A0B
		public void CrossFade(int stateNameHash, float transitionDuration, int layer)
		{
			AnimatorControllerPlayable.CrossFadeInternal(ref this.m_Handle, stateNameHash, transitionDuration, layer, float.NegativeInfinity);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00007822 File Offset: 0x00005A22
		public void CrossFade(int stateNameHash, float transitionDuration, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTime)
		{
			AnimatorControllerPlayable.CrossFadeInternal(ref this.m_Handle, stateNameHash, transitionDuration, layer, normalizedTime);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00007836 File Offset: 0x00005A36
		public void PlayInFixedTime(string stateName)
		{
			AnimatorControllerPlayable.PlayInFixedTimeInternal(ref this.m_Handle, AnimatorControllerPlayable.StringToHash(stateName), -1, float.NegativeInfinity);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00007851 File Offset: 0x00005A51
		public void PlayInFixedTime(string stateName, int layer)
		{
			AnimatorControllerPlayable.PlayInFixedTimeInternal(ref this.m_Handle, AnimatorControllerPlayable.StringToHash(stateName), layer, float.NegativeInfinity);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000786C File Offset: 0x00005A6C
		public void PlayInFixedTime(string stateName, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float fixedTime)
		{
			AnimatorControllerPlayable.PlayInFixedTimeInternal(ref this.m_Handle, AnimatorControllerPlayable.StringToHash(stateName), layer, fixedTime);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00007883 File Offset: 0x00005A83
		public void PlayInFixedTime(int stateNameHash)
		{
			AnimatorControllerPlayable.PlayInFixedTimeInternal(ref this.m_Handle, stateNameHash, -1, float.NegativeInfinity);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00007899 File Offset: 0x00005A99
		public void PlayInFixedTime(int stateNameHash, int layer)
		{
			AnimatorControllerPlayable.PlayInFixedTimeInternal(ref this.m_Handle, stateNameHash, layer, float.NegativeInfinity);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x000078AF File Offset: 0x00005AAF
		public void PlayInFixedTime(int stateNameHash, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float fixedTime)
		{
			AnimatorControllerPlayable.PlayInFixedTimeInternal(ref this.m_Handle, stateNameHash, layer, fixedTime);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x000078C1 File Offset: 0x00005AC1
		public void Play(string stateName)
		{
			AnimatorControllerPlayable.PlayInternal(ref this.m_Handle, AnimatorControllerPlayable.StringToHash(stateName), -1, float.NegativeInfinity);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x000078DC File Offset: 0x00005ADC
		public void Play(string stateName, int layer)
		{
			AnimatorControllerPlayable.PlayInternal(ref this.m_Handle, AnimatorControllerPlayable.StringToHash(stateName), layer, float.NegativeInfinity);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x000078F7 File Offset: 0x00005AF7
		public void Play(string stateName, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTime)
		{
			AnimatorControllerPlayable.PlayInternal(ref this.m_Handle, AnimatorControllerPlayable.StringToHash(stateName), layer, normalizedTime);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000790E File Offset: 0x00005B0E
		public void Play(int stateNameHash)
		{
			AnimatorControllerPlayable.PlayInternal(ref this.m_Handle, stateNameHash, -1, float.NegativeInfinity);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00007924 File Offset: 0x00005B24
		public void Play(int stateNameHash, int layer)
		{
			AnimatorControllerPlayable.PlayInternal(ref this.m_Handle, stateNameHash, layer, float.NegativeInfinity);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000793A File Offset: 0x00005B3A
		public void Play(int stateNameHash, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTime)
		{
			AnimatorControllerPlayable.PlayInternal(ref this.m_Handle, stateNameHash, layer, normalizedTime);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0000794C File Offset: 0x00005B4C
		public bool HasState(int layerIndex, int stateID)
		{
			return AnimatorControllerPlayable.HasStateInternal(ref this.m_Handle, layerIndex, stateID);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0000796C File Offset: 0x00005B6C
		internal string ResolveHash(int hash)
		{
			return AnimatorControllerPlayable.ResolveHashInternal(ref this.m_Handle, hash);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0000798A File Offset: 0x00005B8A
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, RuntimeAnimatorController controller, ref PlayableHandle handle)
		{
			return AnimatorControllerPlayable.CreateHandleInternal_Injected(ref graph, controller, ref handle);
		}

		// Token: 0x0600052D RID: 1325
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RuntimeAnimatorController GetAnimatorControllerInternal(ref PlayableHandle handle);

		// Token: 0x0600052E RID: 1326
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetLayerCountInternal(ref PlayableHandle handle);

		// Token: 0x0600052F RID: 1327
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetLayerNameInternal(ref PlayableHandle handle, int layerIndex);

		// Token: 0x06000530 RID: 1328
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetLayerIndexInternal(ref PlayableHandle handle, string layerName);

		// Token: 0x06000531 RID: 1329
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetLayerWeightInternal(ref PlayableHandle handle, int layerIndex);

		// Token: 0x06000532 RID: 1330
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLayerWeightInternal(ref PlayableHandle handle, int layerIndex, float weight);

		// Token: 0x06000533 RID: 1331 RVA: 0x00007998 File Offset: 0x00005B98
		[NativeThrows]
		private static AnimatorStateInfo GetCurrentAnimatorStateInfoInternal(ref PlayableHandle handle, int layerIndex)
		{
			AnimatorStateInfo result;
			AnimatorControllerPlayable.GetCurrentAnimatorStateInfoInternal_Injected(ref handle, layerIndex, out result);
			return result;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000079B0 File Offset: 0x00005BB0
		[NativeThrows]
		private static AnimatorStateInfo GetNextAnimatorStateInfoInternal(ref PlayableHandle handle, int layerIndex)
		{
			AnimatorStateInfo result;
			AnimatorControllerPlayable.GetNextAnimatorStateInfoInternal_Injected(ref handle, layerIndex, out result);
			return result;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000079C8 File Offset: 0x00005BC8
		[NativeThrows]
		private static AnimatorTransitionInfo GetAnimatorTransitionInfoInternal(ref PlayableHandle handle, int layerIndex)
		{
			AnimatorTransitionInfo result;
			AnimatorControllerPlayable.GetAnimatorTransitionInfoInternal_Injected(ref handle, layerIndex, out result);
			return result;
		}

		// Token: 0x06000536 RID: 1334
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimatorClipInfo[] GetCurrentAnimatorClipInfoInternal(ref PlayableHandle handle, int layerIndex);

		// Token: 0x06000537 RID: 1335
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetAnimatorClipInfoCountInternal(ref PlayableHandle handle, int layerIndex, bool current);

		// Token: 0x06000538 RID: 1336
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimatorClipInfo[] GetNextAnimatorClipInfoInternal(ref PlayableHandle handle, int layerIndex);

		// Token: 0x06000539 RID: 1337
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string ResolveHashInternal(ref PlayableHandle handle, int hash);

		// Token: 0x0600053A RID: 1338
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsInTransitionInternal(ref PlayableHandle handle, int layerIndex);

		// Token: 0x0600053B RID: 1339
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimatorControllerParameter[] GetParametersArrayInternal(ref PlayableHandle handle);

		// Token: 0x0600053C RID: 1340
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimatorControllerParameter GetParameterInternal(ref PlayableHandle handle, int index);

		// Token: 0x0600053D RID: 1341
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetParameterCountInternal(ref PlayableHandle handle);

		// Token: 0x0600053E RID: 1342
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int StringToHash(string name);

		// Token: 0x0600053F RID: 1343
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CrossFadeInFixedTimeInternal(ref PlayableHandle handle, int stateNameHash, float transitionDuration, int layer, float fixedTime);

		// Token: 0x06000540 RID: 1344
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CrossFadeInternal(ref PlayableHandle handle, int stateNameHash, float transitionDuration, int layer, float normalizedTime);

		// Token: 0x06000541 RID: 1345
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PlayInFixedTimeInternal(ref PlayableHandle handle, int stateNameHash, int layer, float fixedTime);

		// Token: 0x06000542 RID: 1346
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PlayInternal(ref PlayableHandle handle, int stateNameHash, int layer, float normalizedTime);

		// Token: 0x06000543 RID: 1347
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasStateInternal(ref PlayableHandle handle, int layerIndex, int stateID);

		// Token: 0x06000544 RID: 1348
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFloatString(ref PlayableHandle handle, string name, float value);

		// Token: 0x06000545 RID: 1349
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFloatID(ref PlayableHandle handle, int id, float value);

		// Token: 0x06000546 RID: 1350
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetFloatString(ref PlayableHandle handle, string name);

		// Token: 0x06000547 RID: 1351
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetFloatID(ref PlayableHandle handle, int id);

		// Token: 0x06000548 RID: 1352
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBoolString(ref PlayableHandle handle, string name, bool value);

		// Token: 0x06000549 RID: 1353
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBoolID(ref PlayableHandle handle, int id, bool value);

		// Token: 0x0600054A RID: 1354
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetBoolString(ref PlayableHandle handle, string name);

		// Token: 0x0600054B RID: 1355
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetBoolID(ref PlayableHandle handle, int id);

		// Token: 0x0600054C RID: 1356
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIntegerString(ref PlayableHandle handle, string name, int value);

		// Token: 0x0600054D RID: 1357
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIntegerID(ref PlayableHandle handle, int id, int value);

		// Token: 0x0600054E RID: 1358
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetIntegerString(ref PlayableHandle handle, string name);

		// Token: 0x0600054F RID: 1359
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetIntegerID(ref PlayableHandle handle, int id);

		// Token: 0x06000550 RID: 1360
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTriggerString(ref PlayableHandle handle, string name);

		// Token: 0x06000551 RID: 1361
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTriggerID(ref PlayableHandle handle, int id);

		// Token: 0x06000552 RID: 1362
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetTriggerString(ref PlayableHandle handle, string name);

		// Token: 0x06000553 RID: 1363
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetTriggerID(ref PlayableHandle handle, int id);

		// Token: 0x06000554 RID: 1364
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsParameterControlledByCurveString(ref PlayableHandle handle, string name);

		// Token: 0x06000555 RID: 1365
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsParameterControlledByCurveID(ref PlayableHandle handle, int id);

		// Token: 0x06000556 RID: 1366 RVA: 0x000079DF File Offset: 0x00005BDF
		// Note: this type is marked as 'beforefieldinit'.
		static AnimatorControllerPlayable()
		{
		}

		// Token: 0x06000557 RID: 1367
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, RuntimeAnimatorController controller, ref PlayableHandle handle);

		// Token: 0x06000558 RID: 1368
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCurrentAnimatorStateInfoInternal_Injected(ref PlayableHandle handle, int layerIndex, out AnimatorStateInfo ret);

		// Token: 0x06000559 RID: 1369
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetNextAnimatorStateInfoInternal_Injected(ref PlayableHandle handle, int layerIndex, out AnimatorStateInfo ret);

		// Token: 0x0600055A RID: 1370
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAnimatorTransitionInfoInternal_Injected(ref PlayableHandle handle, int layerIndex, out AnimatorTransitionInfo ret);

		// Token: 0x04000176 RID: 374
		private PlayableHandle m_Handle;

		// Token: 0x04000177 RID: 375
		private static readonly AnimatorControllerPlayable m_NullPlayable = new AnimatorControllerPlayable(PlayableHandle.Null);
	}
}
