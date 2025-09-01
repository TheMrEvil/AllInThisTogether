using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200005F RID: 95
	public sealed class PostProcessProfile : ScriptableObject
	{
		// Token: 0x06000190 RID: 400 RVA: 0x0000F329 File Offset: 0x0000D529
		private void OnEnable()
		{
			this.settings.RemoveAll((PostProcessEffectSettings x) => x == null);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000F356 File Offset: 0x0000D556
		public T AddSettings<T>() where T : PostProcessEffectSettings
		{
			return (T)((object)this.AddSettings(typeof(T)));
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000F370 File Offset: 0x0000D570
		public PostProcessEffectSettings AddSettings(Type type)
		{
			if (this.HasSettings(type))
			{
				throw new InvalidOperationException("Effect already exists in the stack");
			}
			PostProcessEffectSettings postProcessEffectSettings = (PostProcessEffectSettings)ScriptableObject.CreateInstance(type);
			postProcessEffectSettings.hideFlags = (HideFlags.HideInHierarchy | HideFlags.HideInInspector);
			postProcessEffectSettings.name = type.Name;
			postProcessEffectSettings.enabled.value = true;
			this.settings.Add(postProcessEffectSettings);
			this.isDirty = true;
			return postProcessEffectSettings;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000F3D0 File Offset: 0x0000D5D0
		public PostProcessEffectSettings AddSettings(PostProcessEffectSettings effect)
		{
			if (this.HasSettings(this.settings.GetType()))
			{
				throw new InvalidOperationException("Effect already exists in the stack");
			}
			this.settings.Add(effect);
			this.isDirty = true;
			return effect;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000F404 File Offset: 0x0000D604
		public void RemoveSettings<T>() where T : PostProcessEffectSettings
		{
			this.RemoveSettings(typeof(T));
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000F418 File Offset: 0x0000D618
		public void RemoveSettings(Type type)
		{
			int num = -1;
			for (int i = 0; i < this.settings.Count; i++)
			{
				if (this.settings[i].GetType() == type)
				{
					num = i;
					break;
				}
			}
			if (num < 0)
			{
				throw new InvalidOperationException("Effect doesn't exist in the profile");
			}
			this.settings.RemoveAt(num);
			this.isDirty = true;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000F47C File Offset: 0x0000D67C
		public bool HasSettings<T>() where T : PostProcessEffectSettings
		{
			return this.HasSettings(typeof(T));
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000F490 File Offset: 0x0000D690
		public bool HasSettings(Type type)
		{
			using (List<PostProcessEffectSettings>.Enumerator enumerator = this.settings.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.GetType() == type)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000F4F0 File Offset: 0x0000D6F0
		public T GetSetting<T>() where T : PostProcessEffectSettings
		{
			foreach (PostProcessEffectSettings postProcessEffectSettings in this.settings)
			{
				if (postProcessEffectSettings is T)
				{
					return postProcessEffectSettings as T;
				}
			}
			return default(T);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000F560 File Offset: 0x0000D760
		public bool TryGetSettings<T>(out T outSetting) where T : PostProcessEffectSettings
		{
			Type typeFromHandle = typeof(T);
			outSetting = default(T);
			foreach (PostProcessEffectSettings postProcessEffectSettings in this.settings)
			{
				if (postProcessEffectSettings.GetType() == typeFromHandle)
				{
					outSetting = (T)((object)postProcessEffectSettings);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000F5E0 File Offset: 0x0000D7E0
		public PostProcessProfile()
		{
		}

		// Token: 0x040001DA RID: 474
		[Tooltip("A list of all settings currently stored in this profile.")]
		public List<PostProcessEffectSettings> settings = new List<PostProcessEffectSettings>();

		// Token: 0x040001DB RID: 475
		[NonSerialized]
		public bool isDirty = true;

		// Token: 0x0200008F RID: 143
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000286 RID: 646 RVA: 0x000132D3 File Offset: 0x000114D3
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000287 RID: 647 RVA: 0x000132DF File Offset: 0x000114DF
			public <>c()
			{
			}

			// Token: 0x06000288 RID: 648 RVA: 0x000132E7 File Offset: 0x000114E7
			internal bool <OnEnable>b__2_0(PostProcessEffectSettings x)
			{
				return x == null;
			}

			// Token: 0x04000352 RID: 850
			public static readonly PostProcessProfile.<>c <>9 = new PostProcessProfile.<>c();

			// Token: 0x04000353 RID: 851
			public static Predicate<PostProcessEffectSettings> <>9__2_0;
		}
	}
}
