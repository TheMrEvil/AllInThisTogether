using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x020000BB RID: 187
	public sealed class VolumeManager
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x0001CCD9 File Offset: 0x0001AED9
		public static VolumeManager instance
		{
			get
			{
				return VolumeManager.s_Instance.Value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x0001CCE5 File Offset: 0x0001AEE5
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x0001CCED File Offset: 0x0001AEED
		public VolumeStack stack
		{
			[CompilerGenerated]
			get
			{
				return this.<stack>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<stack>k__BackingField = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0001CCF6 File Offset: 0x0001AEF6
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x0001CCFE File Offset: 0x0001AEFE
		[Obsolete("Please use baseComponentTypeArray instead.")]
		public IEnumerable<Type> baseComponentTypes
		{
			get
			{
				return this.baseComponentTypeArray;
			}
			private set
			{
				this.baseComponentTypeArray = value.ToArray<Type>();
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0001CD0C File Offset: 0x0001AF0C
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x0001CD14 File Offset: 0x0001AF14
		public Type[] baseComponentTypeArray
		{
			[CompilerGenerated]
			get
			{
				return this.<baseComponentTypeArray>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<baseComponentTypeArray>k__BackingField = value;
			}
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001CD20 File Offset: 0x0001AF20
		private VolumeManager()
		{
			this.m_SortedVolumes = new Dictionary<int, List<Volume>>();
			this.m_Volumes = new List<Volume>();
			this.m_SortNeeded = new Dictionary<int, bool>();
			this.m_TempColliders = new List<Collider>(8);
			this.m_ComponentsDefaultState = new List<VolumeComponent>();
			this.ReloadBaseTypes();
			this.m_DefaultStack = this.CreateStack();
			this.stack = this.m_DefaultStack;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001CD89 File Offset: 0x0001AF89
		public VolumeStack CreateStack()
		{
			VolumeStack volumeStack = new VolumeStack();
			volumeStack.Reload(this.m_ComponentsDefaultState);
			return volumeStack;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001CD9C File Offset: 0x0001AF9C
		public void ResetMainStack()
		{
			this.stack = this.m_DefaultStack;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001CDAA File Offset: 0x0001AFAA
		public void DestroyStack(VolumeStack stack)
		{
			stack.Dispose();
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001CDB4 File Offset: 0x0001AFB4
		private void ReloadBaseTypes()
		{
			this.m_ComponentsDefaultState.Clear();
			this.baseComponentTypeArray = (from t in CoreUtils.GetAllTypesDerivedFrom<VolumeComponent>()
			where !t.IsAbstract
			select t).ToArray<Type>();
			BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			foreach (Type type in this.baseComponentTypeArray)
			{
				MethodInfo method = type.GetMethod("Init", bindingAttr);
				if (method != null)
				{
					method.Invoke(null, null);
				}
				VolumeComponent item = (VolumeComponent)ScriptableObject.CreateInstance(type);
				this.m_ComponentsDefaultState.Add(item);
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001CE4C File Offset: 0x0001B04C
		public void Register(Volume volume, int layer)
		{
			if (this.m_Volumes.Contains(volume))
			{
				return;
			}
			this.m_Volumes.Add(volume);
			foreach (KeyValuePair<int, List<Volume>> keyValuePair in this.m_SortedVolumes)
			{
				if ((keyValuePair.Key & 1 << layer) != 0 && !keyValuePair.Value.Contains(volume))
				{
					keyValuePair.Value.Add(volume);
				}
			}
			this.SetLayerDirty(layer);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001CEE8 File Offset: 0x0001B0E8
		public void Unregister(Volume volume, int layer)
		{
			if (this.m_Volumes.Remove(volume))
			{
				foreach (KeyValuePair<int, List<Volume>> keyValuePair in this.m_SortedVolumes)
				{
					if ((keyValuePair.Key & 1 << layer) != 0)
					{
						keyValuePair.Value.Remove(volume);
					}
				}
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001CF60 File Offset: 0x0001B160
		public bool IsComponentActiveInMask<T>(LayerMask layerMask) where T : VolumeComponent
		{
			int value = layerMask.value;
			foreach (KeyValuePair<int, List<Volume>> keyValuePair in this.m_SortedVolumes)
			{
				if (keyValuePair.Key == value)
				{
					foreach (Volume volume in keyValuePair.Value)
					{
						T t;
						if (volume.enabled && !(volume.profileRef == null) && volume.profileRef.TryGet<T>(out t) && t.active)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001D040 File Offset: 0x0001B240
		internal void SetLayerDirty(int layer)
		{
			foreach (KeyValuePair<int, List<Volume>> keyValuePair in this.m_SortedVolumes)
			{
				int key = keyValuePair.Key;
				if ((key & 1 << layer) != 0)
				{
					this.m_SortNeeded[key] = true;
				}
			}
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001D0AC File Offset: 0x0001B2AC
		internal void UpdateVolumeLayer(Volume volume, int prevLayer, int newLayer)
		{
			this.Unregister(volume, prevLayer);
			this.Register(volume, newLayer);
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001D0C0 File Offset: 0x0001B2C0
		private void OverrideData(VolumeStack stack, List<VolumeComponent> components, float interpFactor)
		{
			foreach (VolumeComponent volumeComponent in components)
			{
				if (volumeComponent.active)
				{
					VolumeComponent component = stack.GetComponent(volumeComponent.GetType());
					volumeComponent.Override(component, interpFactor);
				}
			}
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001D124 File Offset: 0x0001B324
		private void ReplaceData(VolumeStack stack)
		{
			ValueTuple<VolumeParameter, VolumeParameter>[] defaultParameters = stack.defaultParameters;
			int num = defaultParameters.Length;
			for (int i = 0; i < num; i++)
			{
				ValueTuple<VolumeParameter, VolumeParameter> valueTuple = defaultParameters[i];
				VolumeParameter item = valueTuple.Item1;
				item.overrideState = false;
				item.SetValue(valueTuple.Item2);
			}
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001D168 File Offset: 0x0001B368
		[Conditional("UNITY_EDITOR")]
		public void CheckBaseTypes()
		{
			if (this.m_ComponentsDefaultState == null || (this.m_ComponentsDefaultState.Count > 0 && this.m_ComponentsDefaultState[0] == null))
			{
				this.ReloadBaseTypes();
			}
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0001D19C File Offset: 0x0001B39C
		[Conditional("UNITY_EDITOR")]
		public void CheckStack(VolumeStack stack)
		{
			Dictionary<Type, VolumeComponent> components = stack.components;
			if (components == null)
			{
				stack.Reload(this.m_ComponentsDefaultState);
				return;
			}
			foreach (KeyValuePair<Type, VolumeComponent> keyValuePair in components)
			{
				if (keyValuePair.Key == null || keyValuePair.Value == null)
				{
					stack.Reload(this.m_ComponentsDefaultState);
					break;
				}
			}
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001D228 File Offset: 0x0001B428
		private bool CheckUpdateRequired(VolumeStack stack)
		{
			if (this.m_Volumes.Count != 0)
			{
				stack.requiresReset = true;
				return true;
			}
			if (stack.requiresReset)
			{
				stack.requiresReset = false;
				return true;
			}
			return false;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001D252 File Offset: 0x0001B452
		public void Update(Transform trigger, LayerMask layerMask)
		{
			this.Update(this.stack, trigger, layerMask);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001D264 File Offset: 0x0001B464
		public void Update(VolumeStack stack, Transform trigger, LayerMask layerMask)
		{
			if (!this.CheckUpdateRequired(stack))
			{
				return;
			}
			this.ReplaceData(stack);
			bool flag = trigger == null;
			Vector3 vector = flag ? Vector3.zero : trigger.position;
			List<Volume> list = this.GrabVolumes(layerMask);
			Camera camera = null;
			if (!flag)
			{
				trigger.TryGetComponent<Camera>(out camera);
			}
			foreach (Volume volume in list)
			{
				if (!(volume == null) && volume.enabled && !(volume.profileRef == null) && volume.weight > 0f)
				{
					if (volume.isGlobal)
					{
						this.OverrideData(stack, volume.profileRef.components, Mathf.Clamp01(volume.weight));
					}
					else if (!flag)
					{
						List<Collider> tempColliders = this.m_TempColliders;
						volume.GetComponents<Collider>(tempColliders);
						if (tempColliders.Count != 0)
						{
							float num = float.PositiveInfinity;
							foreach (Collider collider in tempColliders)
							{
								if (collider.enabled)
								{
									float sqrMagnitude = (collider.ClosestPoint(vector) - vector).sqrMagnitude;
									if (sqrMagnitude < num)
									{
										num = sqrMagnitude;
									}
								}
							}
							tempColliders.Clear();
							float num2 = volume.blendDistance * volume.blendDistance;
							if (num <= num2)
							{
								float num3 = 1f;
								if (num2 > 0f)
								{
									num3 = 1f - num / num2;
								}
								this.OverrideData(stack, volume.profileRef.components, num3 * Mathf.Clamp01(volume.weight));
							}
						}
					}
				}
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001D460 File Offset: 0x0001B660
		public Volume[] GetVolumes(LayerMask layerMask)
		{
			List<Volume> list = this.GrabVolumes(layerMask);
			list.RemoveAll((Volume v) => v == null);
			return list.ToArray();
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001D494 File Offset: 0x0001B694
		private List<Volume> GrabVolumes(LayerMask mask)
		{
			List<Volume> list;
			if (!this.m_SortedVolumes.TryGetValue(mask, out list))
			{
				list = new List<Volume>();
				foreach (Volume volume in this.m_Volumes)
				{
					if ((mask & 1 << volume.gameObject.layer) != 0)
					{
						list.Add(volume);
						this.m_SortNeeded[mask] = true;
					}
				}
				this.m_SortedVolumes.Add(mask, list);
			}
			bool flag;
			if (this.m_SortNeeded.TryGetValue(mask, out flag) && flag)
			{
				this.m_SortNeeded[mask] = false;
				VolumeManager.SortByPriority(list);
			}
			return list;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001D570 File Offset: 0x0001B770
		private static void SortByPriority(List<Volume> volumes)
		{
			for (int i = 1; i < volumes.Count; i++)
			{
				Volume volume = volumes[i];
				int num = i - 1;
				while (num >= 0 && volumes[num].priority > volume.priority)
				{
					volumes[num + 1] = volumes[num];
					num--;
				}
				volumes[num + 1] = volume;
			}
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001D5D2 File Offset: 0x0001B7D2
		private static bool IsVolumeRenderedByCamera(Volume volume, Camera camera)
		{
			return true;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001D5D5 File Offset: 0x0001B7D5
		// Note: this type is marked as 'beforefieldinit'.
		static VolumeManager()
		{
		}

		// Token: 0x0400039E RID: 926
		private static readonly Lazy<VolumeManager> s_Instance = new Lazy<VolumeManager>(() => new VolumeManager());

		// Token: 0x0400039F RID: 927
		[CompilerGenerated]
		private VolumeStack <stack>k__BackingField;

		// Token: 0x040003A0 RID: 928
		[CompilerGenerated]
		private Type[] <baseComponentTypeArray>k__BackingField;

		// Token: 0x040003A1 RID: 929
		private const int k_MaxLayerCount = 32;

		// Token: 0x040003A2 RID: 930
		private readonly Dictionary<int, List<Volume>> m_SortedVolumes;

		// Token: 0x040003A3 RID: 931
		private readonly List<Volume> m_Volumes;

		// Token: 0x040003A4 RID: 932
		private readonly Dictionary<int, bool> m_SortNeeded;

		// Token: 0x040003A5 RID: 933
		private readonly List<VolumeComponent> m_ComponentsDefaultState;

		// Token: 0x040003A6 RID: 934
		private readonly List<Collider> m_TempColliders;

		// Token: 0x040003A7 RID: 935
		private VolumeStack m_DefaultStack;

		// Token: 0x02000180 RID: 384
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600091B RID: 2331 RVA: 0x00024A26 File Offset: 0x00022C26
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600091C RID: 2332 RVA: 0x00024A32 File Offset: 0x00022C32
			public <>c()
			{
			}

			// Token: 0x0600091D RID: 2333 RVA: 0x00024A3A File Offset: 0x00022C3A
			internal bool <ReloadBaseTypes>b__25_0(Type t)
			{
				return !t.IsAbstract;
			}

			// Token: 0x0600091E RID: 2334 RVA: 0x00024A45 File Offset: 0x00022C45
			internal bool <GetVolumes>b__38_0(Volume v)
			{
				return v == null;
			}

			// Token: 0x0600091F RID: 2335 RVA: 0x00024A4E File Offset: 0x00022C4E
			internal VolumeManager <.cctor>b__42_0()
			{
				return new VolumeManager();
			}

			// Token: 0x040005C4 RID: 1476
			public static readonly VolumeManager.<>c <>9 = new VolumeManager.<>c();

			// Token: 0x040005C5 RID: 1477
			public static Func<Type, bool> <>9__25_0;

			// Token: 0x040005C6 RID: 1478
			public static Predicate<Volume> <>9__38_0;
		}
	}
}
