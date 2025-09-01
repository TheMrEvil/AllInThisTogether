using System;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200004F RID: 79
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class PolymorphicDrawerSettingsAttribute : Attribute
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00002EFC File Offset: 0x000010FC
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00002F09 File Offset: 0x00001109
		public bool ShowBaseType
		{
			get
			{
				return this.showBaseType.GetValueOrDefault();
			}
			set
			{
				this.showBaseType = new bool?(value);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00002F17 File Offset: 0x00001117
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00002F25 File Offset: 0x00001125
		public NonDefaultConstructorPreference NonDefaultConstructorPreference
		{
			get
			{
				return this.nonDefaultConstructorPreference.GetValueOrDefault(NonDefaultConstructorPreference.ConstructIdeal);
			}
			set
			{
				this.nonDefaultConstructorPreference = new NonDefaultConstructorPreference?(value);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00002F33 File Offset: 0x00001133
		public bool ShowBaseTypeIsSet
		{
			get
			{
				return this.showBaseType != null;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00002F40 File Offset: 0x00001140
		public bool NonDefaultConstructorPreferenceIsSet
		{
			get
			{
				return this.nonDefaultConstructorPreference != null;
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00002102 File Offset: 0x00000302
		public PolymorphicDrawerSettingsAttribute()
		{
		}

		// Token: 0x040000CB RID: 203
		public bool ReadOnlyIfNotNullReference;

		// Token: 0x040000CC RID: 204
		public string CreateInstanceFunction;

		// Token: 0x040000CD RID: 205
		[Obsolete("Use OnValueChangedAttribute instead.", false)]
		public string OnInstanceAssigned;

		// Token: 0x040000CE RID: 206
		private bool? showBaseType;

		// Token: 0x040000CF RID: 207
		private NonDefaultConstructorPreference? nonDefaultConstructorPreference;
	}
}
