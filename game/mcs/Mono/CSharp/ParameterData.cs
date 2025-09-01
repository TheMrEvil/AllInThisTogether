using System;

namespace Mono.CSharp
{
	// Token: 0x0200026D RID: 621
	public class ParameterData : IParameterData
	{
		// Token: 0x06001E89 RID: 7817 RVA: 0x000966FC File Offset: 0x000948FC
		public ParameterData(string name, Parameter.Modifier modifiers)
		{
			this.name = name;
			this.modifiers = modifiers;
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x00096712 File Offset: 0x00094912
		public ParameterData(string name, Parameter.Modifier modifiers, Expression defaultValue) : this(name, modifiers)
		{
			this.default_value = defaultValue;
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001E8B RID: 7819 RVA: 0x00096723 File Offset: 0x00094923
		public Expression DefaultValue
		{
			get
			{
				return this.default_value;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001E8C RID: 7820 RVA: 0x0009672B File Offset: 0x0009492B
		public bool HasExtensionMethodModifier
		{
			get
			{
				return (this.modifiers & Parameter.Modifier.This) > Parameter.Modifier.NONE;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001E8D RID: 7821 RVA: 0x00096738 File Offset: 0x00094938
		public bool HasDefaultValue
		{
			get
			{
				return this.default_value != null;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001E8E RID: 7822 RVA: 0x00096743 File Offset: 0x00094943
		public Parameter.Modifier ModFlags
		{
			get
			{
				return this.modifiers;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001E8F RID: 7823 RVA: 0x0009674B File Offset: 0x0009494B
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000B4B RID: 2891
		private readonly string name;

		// Token: 0x04000B4C RID: 2892
		private readonly Parameter.Modifier modifiers;

		// Token: 0x04000B4D RID: 2893
		private readonly Expression default_value;
	}
}
