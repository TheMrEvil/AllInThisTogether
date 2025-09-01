using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x0200043D RID: 1085
	[AssetFileNameExtension("playable", new string[]
	{

	})]
	[RequiredByNativeCode]
	[Serializable]
	public abstract class PlayableAsset : ScriptableObject, IPlayableAsset
	{
		// Token: 0x0600259A RID: 9626
		public abstract Playable CreatePlayable(PlayableGraph graph, GameObject owner);

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x0600259B RID: 9627 RVA: 0x0003F77C File Offset: 0x0003D97C
		public virtual double duration
		{
			get
			{
				return PlayableBinding.DefaultDuration;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x0600259C RID: 9628 RVA: 0x0003F794 File Offset: 0x0003D994
		public virtual IEnumerable<PlayableBinding> outputs
		{
			get
			{
				return PlayableBinding.None;
			}
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x0003F7AC File Offset: 0x0003D9AC
		[RequiredByNativeCode]
		internal unsafe static void Internal_CreatePlayable(PlayableAsset asset, PlayableGraph graph, GameObject go, IntPtr ptr)
		{
			bool flag = asset == null;
			Playable playable;
			if (flag)
			{
				playable = Playable.Null;
			}
			else
			{
				playable = asset.CreatePlayable(graph, go);
			}
			Playable* ptr2 = (Playable*)ptr.ToPointer();
			*ptr2 = playable;
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x0003F7E8 File Offset: 0x0003D9E8
		[RequiredByNativeCode]
		internal unsafe static void Internal_GetPlayableAssetDuration(PlayableAsset asset, IntPtr ptrToDouble)
		{
			double duration = asset.duration;
			double* ptr = (double*)ptrToDouble.ToPointer();
			*ptr = duration;
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x0003B111 File Offset: 0x00039311
		protected PlayableAsset()
		{
		}
	}
}
