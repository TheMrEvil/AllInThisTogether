using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200005A RID: 90
	[Serializable]
	public class PostProcessEffectSettings : ScriptableObject
	{
		// Token: 0x06000148 RID: 328 RVA: 0x0000BF9C File Offset: 0x0000A19C
		private void OnEnable()
		{
			this.parameters = (from t in base.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)
			where t.FieldType.IsSubclassOf(typeof(ParameterOverride))
			orderby t.MetadataToken
			select (ParameterOverride)t.GetValue(this)).ToList<ParameterOverride>().AsReadOnly();
			foreach (ParameterOverride parameterOverride in this.parameters)
			{
				parameterOverride.OnEnable();
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000C05C File Offset: 0x0000A25C
		private void OnDisable()
		{
			if (this.parameters == null)
			{
				return;
			}
			foreach (ParameterOverride parameterOverride in this.parameters)
			{
				parameterOverride.OnDisable();
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000C0B0 File Offset: 0x0000A2B0
		public void SetAllOverridesTo(bool state, bool excludeEnabled = true)
		{
			foreach (ParameterOverride parameterOverride in this.parameters)
			{
				if (!excludeEnabled || parameterOverride != this.enabled)
				{
					parameterOverride.overrideState = state;
				}
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000C10C File Offset: 0x0000A30C
		public virtual bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000C11C File Offset: 0x0000A31C
		public int GetHash()
		{
			int num = 17;
			foreach (ParameterOverride parameterOverride in this.parameters)
			{
				num = num * 23 + parameterOverride.GetHash();
			}
			return num;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000C174 File Offset: 0x0000A374
		public PostProcessEffectSettings()
		{
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000C19C File Offset: 0x0000A39C
		[CompilerGenerated]
		private ParameterOverride <OnEnable>b__3_2(FieldInfo t)
		{
			return (ParameterOverride)t.GetValue(this);
		}

		// Token: 0x04000196 RID: 406
		public bool active = true;

		// Token: 0x04000197 RID: 407
		public BoolParameter enabled = new BoolParameter
		{
			overrideState = true,
			value = false
		};

		// Token: 0x04000198 RID: 408
		internal ReadOnlyCollection<ParameterOverride> parameters;

		// Token: 0x02000086 RID: 134
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000272 RID: 626 RVA: 0x0001316E File Offset: 0x0001136E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000273 RID: 627 RVA: 0x0001317A File Offset: 0x0001137A
			public <>c()
			{
			}

			// Token: 0x06000274 RID: 628 RVA: 0x00013182 File Offset: 0x00011382
			internal bool <OnEnable>b__3_0(FieldInfo t)
			{
				return t.FieldType.IsSubclassOf(typeof(ParameterOverride));
			}

			// Token: 0x06000275 RID: 629 RVA: 0x00013199 File Offset: 0x00011399
			internal int <OnEnable>b__3_1(FieldInfo t)
			{
				return t.MetadataToken;
			}

			// Token: 0x0400033A RID: 826
			public static readonly PostProcessEffectSettings.<>c <>9 = new PostProcessEffectSettings.<>c();

			// Token: 0x0400033B RID: 827
			public static Func<FieldInfo, bool> <>9__3_0;

			// Token: 0x0400033C RID: 828
			public static Func<FieldInfo, int> <>9__3_1;
		}
	}
}
