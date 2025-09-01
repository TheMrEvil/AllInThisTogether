using System;

namespace System.Configuration
{
	/// <summary>Declaratively instructs the .NET Framework to perform time validation on a configuration property. This class cannot be inherited.</summary>
	// Token: 0x02000057 RID: 87
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class PositiveTimeSpanValidatorAttribute : ConfigurationValidatorAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.PositiveTimeSpanValidatorAttribute" /> class.</summary>
		// Token: 0x060002E2 RID: 738 RVA: 0x00007C67 File Offset: 0x00005E67
		public PositiveTimeSpanValidatorAttribute()
		{
		}

		/// <summary>Gets an instance of the <see cref="T:System.Configuration.PositiveTimeSpanValidator" /> class.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationValidatorBase" /> validator instance.</returns>
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x000087DA File Offset: 0x000069DA
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (this.instance == null)
				{
					this.instance = new PositiveTimeSpanValidator();
				}
				return this.instance;
			}
		}

		// Token: 0x0400010F RID: 271
		private ConfigurationValidatorBase instance;
	}
}
