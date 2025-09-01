using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x020000E6 RID: 230
	public sealed class VolumeStack : IDisposable
	{
		// Token: 0x060006D5 RID: 1749 RVA: 0x0001E68B File Offset: 0x0001C88B
		internal VolumeStack()
		{
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001E6A8 File Offset: 0x0001C8A8
		internal void Reload(List<VolumeComponent> componentDefaultStates)
		{
			this.components.Clear();
			this.requiresReset = true;
			List<ValueTuple<VolumeParameter, VolumeParameter>> list = new List<ValueTuple<VolumeParameter, VolumeParameter>>();
			foreach (VolumeComponent volumeComponent in componentDefaultStates)
			{
				Type type = volumeComponent.GetType();
				VolumeComponent volumeComponent2 = (VolumeComponent)ScriptableObject.CreateInstance(type);
				this.components.Add(type, volumeComponent2);
				int count = volumeComponent2.parameters.Count;
				for (int i = 0; i < count; i++)
				{
					list.Add(new ValueTuple<VolumeParameter, VolumeParameter>
					{
						Item1 = volumeComponent2.parameters[i],
						Item2 = volumeComponent.parameters[i]
					});
				}
			}
			this.defaultParameters = list.ToArray();
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001E794 File Offset: 0x0001C994
		public T GetComponent<T>() where T : VolumeComponent
		{
			return (T)((object)this.GetComponent(typeof(T)));
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001E7AC File Offset: 0x0001C9AC
		public VolumeComponent GetComponent(Type type)
		{
			VolumeComponent result;
			this.components.TryGetValue(type, out result);
			return result;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001E7CC File Offset: 0x0001C9CC
		public void Dispose()
		{
			foreach (KeyValuePair<Type, VolumeComponent> keyValuePair in this.components)
			{
				CoreUtils.Destroy(keyValuePair.Value);
			}
			this.components.Clear();
		}

		// Token: 0x040003C9 RID: 969
		internal readonly Dictionary<Type, VolumeComponent> components = new Dictionary<Type, VolumeComponent>();

		// Token: 0x040003CA RID: 970
		[TupleElementNames(new string[]
		{
			"parameter",
			"defaultValue"
		})]
		internal ValueTuple<VolumeParameter, VolumeParameter>[] defaultParameters;

		// Token: 0x040003CB RID: 971
		internal bool requiresReset = true;
	}
}
