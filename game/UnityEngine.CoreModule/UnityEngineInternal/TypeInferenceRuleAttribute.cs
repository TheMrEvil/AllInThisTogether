using System;

namespace UnityEngineInternal
{
	// Token: 0x02000011 RID: 17
	[AttributeUsage(AttributeTargets.Method)]
	[Serializable]
	public class TypeInferenceRuleAttribute : Attribute
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000020F3 File Offset: 0x000002F3
		public TypeInferenceRuleAttribute(TypeInferenceRules rule) : this(rule.ToString())
		{
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000210A File Offset: 0x0000030A
		public TypeInferenceRuleAttribute(string rule)
		{
			this._rule = rule;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000211C File Offset: 0x0000031C
		public override string ToString()
		{
			return this._rule;
		}

		// Token: 0x04000024 RID: 36
		private readonly string _rule;
	}
}
