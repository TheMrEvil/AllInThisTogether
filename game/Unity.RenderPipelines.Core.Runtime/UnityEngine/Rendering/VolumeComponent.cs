using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x020000BA RID: 186
	[Serializable]
	public class VolumeComponent : ScriptableObject
	{
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x0001C966 File Offset: 0x0001AB66
		// (set) Token: 0x06000622 RID: 1570 RVA: 0x0001C96E File Offset: 0x0001AB6E
		public string displayName
		{
			[CompilerGenerated]
			get
			{
				return this.<displayName>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<displayName>k__BackingField = value;
			}
		} = "";

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x0001C977 File Offset: 0x0001AB77
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x0001C97F File Offset: 0x0001AB7F
		public ReadOnlyCollection<VolumeParameter> parameters
		{
			[CompilerGenerated]
			get
			{
				return this.<parameters>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<parameters>k__BackingField = value;
			}
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0001C988 File Offset: 0x0001AB88
		internal static void FindParameters(object o, List<VolumeParameter> parameters, Func<FieldInfo, bool> filter = null)
		{
			if (o == null)
			{
				return;
			}
			foreach (FieldInfo fieldInfo in from t in o.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
			orderby t.MetadataToken
			select t)
			{
				if (fieldInfo.FieldType.IsSubclassOf(typeof(VolumeParameter)))
				{
					if (filter == null || filter(fieldInfo))
					{
						parameters.Add((VolumeParameter)fieldInfo.GetValue(o));
					}
				}
				else if (!fieldInfo.FieldType.IsArray && fieldInfo.FieldType.IsClass)
				{
					VolumeComponent.FindParameters(fieldInfo.GetValue(o), parameters, filter);
				}
			}
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001CA60 File Offset: 0x0001AC60
		protected virtual void OnEnable()
		{
			List<VolumeParameter> list = new List<VolumeParameter>();
			VolumeComponent.FindParameters(this, list, null);
			this.parameters = list.AsReadOnly();
			foreach (VolumeParameter volumeParameter in this.parameters)
			{
				if (volumeParameter != null)
				{
					volumeParameter.OnEnable();
				}
				else
				{
					Debug.LogWarning("Volume Component " + base.GetType().Name + " contains a null parameter; please make sure all parameters are initialized to a default value. Until this is fixed the null parameters will not be considered by the system.");
				}
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001CAEC File Offset: 0x0001ACEC
		protected virtual void OnDisable()
		{
			if (this.parameters == null)
			{
				return;
			}
			foreach (VolumeParameter volumeParameter in this.parameters)
			{
				if (volumeParameter != null)
				{
					volumeParameter.OnDisable();
				}
			}
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001CB44 File Offset: 0x0001AD44
		public virtual void Override(VolumeComponent state, float interpFactor)
		{
			int count = this.parameters.Count;
			for (int i = 0; i < count; i++)
			{
				VolumeParameter volumeParameter = state.parameters[i];
				VolumeParameter volumeParameter2 = this.parameters[i];
				if (volumeParameter2.overrideState)
				{
					volumeParameter.overrideState = volumeParameter2.overrideState;
					volumeParameter.Interp(volumeParameter, volumeParameter2, interpFactor);
				}
			}
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001CBA0 File Offset: 0x0001ADA0
		public void SetAllOverridesTo(bool state)
		{
			this.SetOverridesTo(this.parameters, state);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001CBB0 File Offset: 0x0001ADB0
		internal void SetOverridesTo(IEnumerable<VolumeParameter> enumerable, bool state)
		{
			foreach (VolumeParameter volumeParameter in enumerable)
			{
				volumeParameter.overrideState = state;
				Type type = volumeParameter.GetType();
				if (VolumeParameter.IsObjectParameter(type))
				{
					ReadOnlyCollection<VolumeParameter> readOnlyCollection = (ReadOnlyCollection<VolumeParameter>)type.GetProperty("parameters", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(volumeParameter, null);
					if (readOnlyCollection != null)
					{
						this.SetOverridesTo(readOnlyCollection, state);
					}
				}
			}
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001CC2C File Offset: 0x0001AE2C
		public override int GetHashCode()
		{
			int num = 17;
			for (int i = 0; i < this.parameters.Count; i++)
			{
				num = num * 23 + this.parameters[i].GetHashCode();
			}
			return num;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0001CC6A File Offset: 0x0001AE6A
		protected virtual void OnDestroy()
		{
			this.Release();
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001CC74 File Offset: 0x0001AE74
		public void Release()
		{
			if (this.parameters == null)
			{
				return;
			}
			for (int i = 0; i < this.parameters.Count; i++)
			{
				if (this.parameters[i] != null)
				{
					this.parameters[i].Release();
				}
			}
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0001CCBF File Offset: 0x0001AEBF
		public VolumeComponent()
		{
		}

		// Token: 0x0400039B RID: 923
		public bool active = true;

		// Token: 0x0400039C RID: 924
		[CompilerGenerated]
		private string <displayName>k__BackingField;

		// Token: 0x0400039D RID: 925
		[CompilerGenerated]
		private ReadOnlyCollection<VolumeParameter> <parameters>k__BackingField;

		// Token: 0x0200017E RID: 382
		public sealed class Indent : PropertyAttribute
		{
			// Token: 0x06000917 RID: 2327 RVA: 0x000249FB File Offset: 0x00022BFB
			public Indent(int relativeAmount = 1)
			{
				this.relativeAmount = relativeAmount;
			}

			// Token: 0x040005C1 RID: 1473
			public readonly int relativeAmount;
		}

		// Token: 0x0200017F RID: 383
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000918 RID: 2328 RVA: 0x00024A0A File Offset: 0x00022C0A
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000919 RID: 2329 RVA: 0x00024A16 File Offset: 0x00022C16
			public <>c()
			{
			}

			// Token: 0x0600091A RID: 2330 RVA: 0x00024A1E File Offset: 0x00022C1E
			internal int <FindParameters>b__10_0(FieldInfo t)
			{
				return t.MetadataToken;
			}

			// Token: 0x040005C2 RID: 1474
			public static readonly VolumeComponent.<>c <>9 = new VolumeComponent.<>c();

			// Token: 0x040005C3 RID: 1475
			public static Func<FieldInfo, int> <>9__10_0;
		}
	}
}
