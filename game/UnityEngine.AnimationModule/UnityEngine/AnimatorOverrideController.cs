using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000024 RID: 36
	[NativeHeader("Modules/Animation/AnimatorOverrideController.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/Animation.bindings.h")]
	[UsedByNativeCode]
	public class AnimatorOverrideController : RuntimeAnimatorController
	{
		// Token: 0x060001EA RID: 490 RVA: 0x000037C2 File Offset: 0x000019C2
		public AnimatorOverrideController()
		{
			AnimatorOverrideController.Internal_Create(this, null);
			this.OnOverrideControllerDirty = null;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000037DB File Offset: 0x000019DB
		public AnimatorOverrideController(RuntimeAnimatorController controller)
		{
			AnimatorOverrideController.Internal_Create(this, controller);
			this.OnOverrideControllerDirty = null;
		}

		// Token: 0x060001EC RID: 492
		[FreeFunction("AnimationBindings::CreateAnimatorOverrideController")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] AnimatorOverrideController self, RuntimeAnimatorController controller);

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001ED RID: 493
		// (set) Token: 0x060001EE RID: 494
		public extern RuntimeAnimatorController runtimeAnimatorController { [NativeMethod("GetAnimatorController")] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeMethod("SetAnimatorController")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000085 RID: 133
		public AnimationClip this[string name]
		{
			get
			{
				return this.Internal_GetClipByName(name, true);
			}
			set
			{
				this.Internal_SetClipByName(name, value);
			}
		}

		// Token: 0x060001F1 RID: 497
		[NativeMethod("GetClip")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AnimationClip Internal_GetClipByName(string name, bool returnEffectiveClip);

		// Token: 0x060001F2 RID: 498
		[NativeMethod("SetClip")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetClipByName(string name, AnimationClip clip);

		// Token: 0x17000086 RID: 134
		public AnimationClip this[AnimationClip clip]
		{
			get
			{
				return this.GetClip(clip, true);
			}
			set
			{
				this.SetClip(clip, value, true);
			}
		}

		// Token: 0x060001F5 RID: 501
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AnimationClip GetClip(AnimationClip originalClip, bool returnEffectiveClip);

		// Token: 0x060001F6 RID: 502
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetClip(AnimationClip originalClip, AnimationClip overrideClip, bool notify);

		// Token: 0x060001F7 RID: 503
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SendNotification();

		// Token: 0x060001F8 RID: 504
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AnimationClip GetOriginalClip(int index);

		// Token: 0x060001F9 RID: 505
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AnimationClip GetOverrideClip(AnimationClip originalClip);

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001FA RID: 506
		public extern int overridesCount { [NativeMethod("GetOriginalClipsCount")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060001FB RID: 507 RVA: 0x00003844 File Offset: 0x00001A44
		public void GetOverrides(List<KeyValuePair<AnimationClip, AnimationClip>> overrides)
		{
			bool flag = overrides == null;
			if (flag)
			{
				throw new ArgumentNullException("overrides");
			}
			int overridesCount = this.overridesCount;
			bool flag2 = overrides.Capacity < overridesCount;
			if (flag2)
			{
				overrides.Capacity = overridesCount;
			}
			overrides.Clear();
			for (int i = 0; i < overridesCount; i++)
			{
				AnimationClip originalClip = this.GetOriginalClip(i);
				overrides.Add(new KeyValuePair<AnimationClip, AnimationClip>(originalClip, this.GetOverrideClip(originalClip)));
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000038BC File Offset: 0x00001ABC
		public void ApplyOverrides(IList<KeyValuePair<AnimationClip, AnimationClip>> overrides)
		{
			bool flag = overrides == null;
			if (flag)
			{
				throw new ArgumentNullException("overrides");
			}
			for (int i = 0; i < overrides.Count; i++)
			{
				this.SetClip(overrides[i].Key, overrides[i].Value, false);
			}
			this.SendNotification();
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00003920 File Offset: 0x00001B20
		// (set) Token: 0x060001FE RID: 510 RVA: 0x00003984 File Offset: 0x00001B84
		[Obsolete("AnimatorOverrideController.clips property is deprecated. Use AnimatorOverrideController.GetOverrides and AnimatorOverrideController.ApplyOverrides instead.")]
		public AnimationClipPair[] clips
		{
			get
			{
				int overridesCount = this.overridesCount;
				AnimationClipPair[] array = new AnimationClipPair[overridesCount];
				for (int i = 0; i < overridesCount; i++)
				{
					array[i] = new AnimationClipPair();
					array[i].originalClip = this.GetOriginalClip(i);
					array[i].overrideClip = this.GetOverrideClip(array[i].originalClip);
				}
				return array;
			}
			set
			{
				for (int i = 0; i < value.Length; i++)
				{
					this.SetClip(value[i].originalClip, value[i].overrideClip, false);
				}
				this.SendNotification();
			}
		}

		// Token: 0x060001FF RID: 511
		[NativeConditional("UNITY_EDITOR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void PerformOverrideClipListCleanup();

		// Token: 0x06000200 RID: 512 RVA: 0x000039C4 File Offset: 0x00001BC4
		[RequiredByNativeCode]
		[NativeConditional("UNITY_EDITOR")]
		internal static void OnInvalidateOverrideController(AnimatorOverrideController controller)
		{
			bool flag = controller.OnOverrideControllerDirty != null;
			if (flag)
			{
				controller.OnOverrideControllerDirty();
			}
		}

		// Token: 0x0400006F RID: 111
		internal AnimatorOverrideController.OnOverrideControllerDirtyCallback OnOverrideControllerDirty;

		// Token: 0x02000025 RID: 37
		// (Invoke) Token: 0x06000202 RID: 514
		internal delegate void OnOverrideControllerDirtyCallback();
	}
}
