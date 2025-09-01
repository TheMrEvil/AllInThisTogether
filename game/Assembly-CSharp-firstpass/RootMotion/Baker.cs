using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Playables;

namespace RootMotion
{
	// Token: 0x020000A7 RID: 167
	[HelpURL("http://www.root-motion.com/finalikdox/html/page3.html")]
	[AddComponentMenu("Scripts/RootMotion/Baker")]
	public abstract class Baker : MonoBehaviour
	{
		// Token: 0x0600078F RID: 1935 RVA: 0x000349BC File Offset: 0x00032BBC
		[ContextMenu("User Manual")]
		private void OpenUserManual()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/page3.html");
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x000349C8 File Offset: 0x00032BC8
		[ContextMenu("Scrpt Reference")]
		private void OpenScriptReference()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/class_root_motion_1_1_baker.html");
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x000349D4 File Offset: 0x00032BD4
		[ContextMenu("Support Group")]
		private void SupportGroup()
		{
			Application.OpenURL("https://groups.google.com/forum/#!forum/final-ik");
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x000349E0 File Offset: 0x00032BE0
		[ContextMenu("Asset Store Thread")]
		private void ASThread()
		{
			Application.OpenURL("http://forum.unity3d.com/threads/final-ik-full-body-ik-aim-look-at-fabrik-ccd-ik-1-0-released.222685/");
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x000349EC File Offset: 0x00032BEC
		// (set) Token: 0x06000794 RID: 1940 RVA: 0x000349F4 File Offset: 0x00032BF4
		public bool isBaking
		{
			[CompilerGenerated]
			get
			{
				return this.<isBaking>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<isBaking>k__BackingField = value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x000349FD File Offset: 0x00032BFD
		// (set) Token: 0x06000796 RID: 1942 RVA: 0x00034A05 File Offset: 0x00032C05
		public float bakingProgress
		{
			[CompilerGenerated]
			get
			{
				return this.<bakingProgress>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<bakingProgress>k__BackingField = value;
			}
		}

		// Token: 0x06000797 RID: 1943
		protected abstract Transform GetCharacterRoot();

		// Token: 0x06000798 RID: 1944
		protected abstract void OnStartBaking();

		// Token: 0x06000799 RID: 1945
		protected abstract void OnSetLoopFrame(float time);

		// Token: 0x0600079A RID: 1946
		protected abstract void OnSetCurves(ref AnimationClip clip);

		// Token: 0x0600079B RID: 1947
		protected abstract void OnSetKeyframes(float time, bool lastFrame);

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00034A0E File Offset: 0x00032C0E
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x00034A16 File Offset: 0x00032C16
		private protected float clipLength
		{
			[CompilerGenerated]
			protected get
			{
				return this.<clipLength>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<clipLength>k__BackingField = value;
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00034A1F File Offset: 0x00032C1F
		public void BakeClip()
		{
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00034A21 File Offset: 0x00032C21
		public void StartBaking()
		{
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00034A23 File Offset: 0x00032C23
		public void StopBaking()
		{
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00034A28 File Offset: 0x00032C28
		protected Baker()
		{
		}

		// Token: 0x04000605 RID: 1541
		[Tooltip("In AnimationClips, AnimationStates or PlayableDirector mode - the frame rate at which the animation clip will be sampled. In Realtime mode - the frame rate at which the pose will be sampled. With the latter, the frame rate is not guaranteed if the player is not able to reach it.")]
		[Range(1f, 90f)]
		public int frameRate = 30;

		// Token: 0x04000606 RID: 1542
		[Tooltip("Maximum allowed error for keyframe reduction.")]
		[Range(0f, 0.1f)]
		public float keyReductionError = 0.01f;

		// Token: 0x04000607 RID: 1543
		[Tooltip("AnimationClips mode can be used to bake a batch of AnimationClips directly without the need of setting up an AnimatorController. AnimationStates mode is useful for when you need to set up a more complex rig with layers and AvatarMasks in Mecanim. PlayableDirector mode bakes a Timeline. Realtime mode is for continuous baking of gameplay, ragdoll phsysics or PuppetMaster dynamics.")]
		public Baker.Mode mode;

		// Token: 0x04000608 RID: 1544
		[Tooltip("AnimationClips to bake.")]
		public AnimationClip[] animationClips = new AnimationClip[0];

		// Token: 0x04000609 RID: 1545
		[Tooltip("The name of the AnimationStates to bake (must be on the base layer) in the Animator above (Right-click on this component header and select 'Find Animation States' to have Baker fill those in automatically, required that state names match with the names of the clips used in them).")]
		public string[] animationStates = new string[0];

		// Token: 0x0400060A RID: 1546
		[Tooltip("Sets the baked animation clip to loop time and matches the last frame keys with the first. Note that when overwriting a previously baked clip, AnimationClipSettings will be copied from the existing clip.")]
		public bool loop = true;

		// Token: 0x0400060B RID: 1547
		[Tooltip("The folder to save the baked AnimationClips to.")]
		public string saveToFolder = "Assets";

		// Token: 0x0400060C RID: 1548
		[Tooltip("String that will be added to each clip or animation state name for the saved clip. For example if your animation state/clip names were 'Idle' and 'Walk', then with '_Baked' as Append Name, the Baker will create 'Idle_Baked' and 'Walk_Baked' animation clips.")]
		public string appendName = "_Baked";

		// Token: 0x0400060D RID: 1549
		[Tooltip("Name of the created AnimationClip file.")]
		public string saveName = "Baked Clip";

		// Token: 0x0400060E RID: 1550
		[CompilerGenerated]
		private bool <isBaking>k__BackingField;

		// Token: 0x0400060F RID: 1551
		[CompilerGenerated]
		private float <bakingProgress>k__BackingField;

		// Token: 0x04000610 RID: 1552
		[HideInInspector]
		public Animator animator;

		// Token: 0x04000611 RID: 1553
		[HideInInspector]
		public PlayableDirector director;

		// Token: 0x04000612 RID: 1554
		[CompilerGenerated]
		private float <clipLength>k__BackingField;

		// Token: 0x020001E0 RID: 480
		[Serializable]
		public enum Mode
		{
			// Token: 0x04000E52 RID: 3666
			AnimationClips,
			// Token: 0x04000E53 RID: 3667
			AnimationStates,
			// Token: 0x04000E54 RID: 3668
			PlayableDirector,
			// Token: 0x04000E55 RID: 3669
			Realtime
		}
	}
}
