using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x020000E5 RID: 229
	public sealed class VolumeProfile : ScriptableObject
	{
		// Token: 0x060006C3 RID: 1731 RVA: 0x0001E206 File Offset: 0x0001C406
		private void OnEnable()
		{
			this.components.RemoveAll((VolumeComponent x) => x == null);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001E234 File Offset: 0x0001C434
		internal void OnDisable()
		{
			if (this.components == null)
			{
				return;
			}
			for (int i = 0; i < this.components.Count; i++)
			{
				if (this.components[i] != null)
				{
					this.components[i].Release();
				}
			}
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001E285 File Offset: 0x0001C485
		public void Reset()
		{
			this.isDirty = true;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001E28E File Offset: 0x0001C48E
		public T Add<T>(bool overrides = false) where T : VolumeComponent
		{
			return (T)((object)this.Add(typeof(T), overrides));
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001E2A8 File Offset: 0x0001C4A8
		public VolumeComponent Add(Type type, bool overrides = false)
		{
			if (this.Has(type))
			{
				throw new InvalidOperationException("Component already exists in the volume");
			}
			VolumeComponent volumeComponent = (VolumeComponent)ScriptableObject.CreateInstance(type);
			volumeComponent.SetAllOverridesTo(overrides);
			this.components.Add(volumeComponent);
			this.isDirty = true;
			return volumeComponent;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001E2F0 File Offset: 0x0001C4F0
		public void Remove<T>() where T : VolumeComponent
		{
			this.Remove(typeof(T));
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001E304 File Offset: 0x0001C504
		public void Remove(Type type)
		{
			int num = -1;
			for (int i = 0; i < this.components.Count; i++)
			{
				if (this.components[i].GetType() == type)
				{
					num = i;
					break;
				}
			}
			if (num >= 0)
			{
				this.components.RemoveAt(num);
				this.isDirty = true;
			}
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0001E35D File Offset: 0x0001C55D
		public bool Has<T>() where T : VolumeComponent
		{
			return this.Has(typeof(T));
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001E370 File Offset: 0x0001C570
		public bool Has(Type type)
		{
			using (List<VolumeComponent>.Enumerator enumerator = this.components.GetEnumerator())
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

		// Token: 0x060006CC RID: 1740 RVA: 0x0001E3D0 File Offset: 0x0001C5D0
		public bool HasSubclassOf(Type type)
		{
			using (List<VolumeComponent>.Enumerator enumerator = this.components.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.GetType().IsSubclassOf(type))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001E430 File Offset: 0x0001C630
		public bool TryGet<T>(out T component) where T : VolumeComponent
		{
			return this.TryGet<T>(typeof(T), out component);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0001E444 File Offset: 0x0001C644
		public bool TryGet<T>(Type type, out T component) where T : VolumeComponent
		{
			component = default(T);
			foreach (VolumeComponent volumeComponent in this.components)
			{
				if (volumeComponent.GetType() == type)
				{
					component = (T)((object)volumeComponent);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001E4B8 File Offset: 0x0001C6B8
		public bool TryGetSubclassOf<T>(Type type, out T component) where T : VolumeComponent
		{
			component = default(T);
			foreach (VolumeComponent volumeComponent in this.components)
			{
				if (volumeComponent.GetType().IsSubclassOf(type))
				{
					component = (T)((object)volumeComponent);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001E52C File Offset: 0x0001C72C
		public bool TryGetAllSubclassOf<T>(Type type, List<T> result) where T : VolumeComponent
		{
			int count = result.Count;
			foreach (VolumeComponent volumeComponent in this.components)
			{
				if (volumeComponent.GetType().IsSubclassOf(type))
				{
					result.Add((T)((object)volumeComponent));
				}
			}
			return count != result.Count;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001E5A8 File Offset: 0x0001C7A8
		public override int GetHashCode()
		{
			int num = 17;
			for (int i = 0; i < this.components.Count; i++)
			{
				num = num * 23 + this.components[i].GetHashCode();
			}
			return num;
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001E5E8 File Offset: 0x0001C7E8
		internal int GetComponentListHashCode()
		{
			int num = 17;
			for (int i = 0; i < this.components.Count; i++)
			{
				num = num * 23 + this.components[i].GetType().GetHashCode();
			}
			return num;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001E62C File Offset: 0x0001C82C
		internal void Sanitize()
		{
			for (int i = this.components.Count - 1; i >= 0; i--)
			{
				if (this.components[i] == null)
				{
					this.components.RemoveAt(i);
				}
			}
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001E671 File Offset: 0x0001C871
		public VolumeProfile()
		{
		}

		// Token: 0x040003C7 RID: 967
		public List<VolumeComponent> components = new List<VolumeComponent>();

		// Token: 0x040003C8 RID: 968
		[NonSerialized]
		public bool isDirty = true;

		// Token: 0x02000182 RID: 386
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000924 RID: 2340 RVA: 0x00024A88 File Offset: 0x00022C88
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000925 RID: 2341 RVA: 0x00024A94 File Offset: 0x00022C94
			public <>c()
			{
			}

			// Token: 0x06000926 RID: 2342 RVA: 0x00024A9C File Offset: 0x00022C9C
			internal bool <OnEnable>b__2_0(VolumeComponent x)
			{
				return x == null;
			}

			// Token: 0x040005CA RID: 1482
			public static readonly VolumeProfile.<>c <>9 = new VolumeProfile.<>c();

			// Token: 0x040005CB RID: 1483
			public static Predicate<VolumeComponent> <>9__2_0;
		}
	}
}
