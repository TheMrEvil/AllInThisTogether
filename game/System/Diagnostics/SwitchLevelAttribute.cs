using System;

namespace System.Diagnostics
{
	/// <summary>Identifies the level type for a switch.</summary>
	// Token: 0x02000228 RID: 552
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class SwitchLevelAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SwitchLevelAttribute" /> class, specifying the type that determines whether a trace should be written.</summary>
		/// <param name="switchLevelType">The <see cref="T:System.Type" /> that determines whether a trace should be written.</param>
		// Token: 0x06001014 RID: 4116 RVA: 0x00046DBD File Offset: 0x00044FBD
		public SwitchLevelAttribute(Type switchLevelType)
		{
			this.SwitchLevelType = switchLevelType;
		}

		/// <summary>Gets or sets the type that determines whether a trace should be written.</summary>
		/// <returns>The <see cref="T:System.Type" /> that determines whether a trace should be written.</returns>
		/// <exception cref="T:System.ArgumentNullException">The set operation failed because the value is <see langword="null" />.</exception>
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x00046DCC File Offset: 0x00044FCC
		// (set) Token: 0x06001016 RID: 4118 RVA: 0x00046DD4 File Offset: 0x00044FD4
		public Type SwitchLevelType
		{
			get
			{
				return this.type;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.type = value;
			}
		}

		// Token: 0x040009C8 RID: 2504
		private Type type;
	}
}
