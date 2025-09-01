using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Defines the counter type, name, and Help string for a custom counter.</summary>
	// Token: 0x0200024C RID: 588
	[TypeConverter("System.Diagnostics.Design.CounterCreationDataConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Serializable]
	public class CounterCreationData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterCreationData" /> class, to a counter of type <see langword="NumberOfItems32" />, and with empty name and help strings.</summary>
		// Token: 0x06001215 RID: 4629 RVA: 0x0004E299 File Offset: 0x0004C499
		public CounterCreationData()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterCreationData" /> class, to a counter of the specified type, using the specified counter name and Help strings.</summary>
		/// <param name="counterName">The name of the counter, which must be unique within its category.</param>
		/// <param name="counterHelp">The text that describes the counter's behavior.</param>
		/// <param name="counterType">A <see cref="T:System.Diagnostics.PerformanceCounterType" /> that identifies the counter's behavior.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">You have specified a value for <paramref name="counterType" /> that is not a member of the <see cref="T:System.Diagnostics.PerformanceCounterType" /> enumeration.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="counterHelp" /> is <see langword="null" />.</exception>
		// Token: 0x06001216 RID: 4630 RVA: 0x0004E2AC File Offset: 0x0004C4AC
		public CounterCreationData(string counterName, string counterHelp, PerformanceCounterType counterType)
		{
			this.CounterName = counterName;
			this.CounterHelp = counterHelp;
			this.CounterType = counterType;
		}

		/// <summary>Gets or sets the custom counter's description.</summary>
		/// <returns>The text that describes the counter's behavior.</returns>
		/// <exception cref="T:System.ArgumentNullException">The specified value is <see langword="null" />.</exception>
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x0004E2D4 File Offset: 0x0004C4D4
		// (set) Token: 0x06001218 RID: 4632 RVA: 0x0004E2DC File Offset: 0x0004C4DC
		[DefaultValue("")]
		[MonitoringDescription("Description of this counter.")]
		public string CounterHelp
		{
			get
			{
				return this.help;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.help = value;
			}
		}

		/// <summary>Gets or sets the name of the custom counter.</summary>
		/// <returns>A name for the counter, which is unique in its category.</returns>
		/// <exception cref="T:System.ArgumentNullException">The specified value is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The specified value is not between 1 and 80 characters long or contains double quotes, control characters or leading or trailing spaces.</exception>
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x0004E2F3 File Offset: 0x0004C4F3
		// (set) Token: 0x0600121A RID: 4634 RVA: 0x0004E2FB File Offset: 0x0004C4FB
		[DefaultValue("")]
		[MonitoringDescription("Name of this counter.")]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string CounterName
		{
			get
			{
				return this.name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value == "")
				{
					throw new ArgumentException("value");
				}
				this.name = value;
			}
		}

		/// <summary>Gets or sets the performance counter type of the custom counter.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterType" /> that defines the behavior of the performance counter.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">You have specified a type that is not a member of the <see cref="T:System.Diagnostics.PerformanceCounterType" /> enumeration.</exception>
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x0004E32A File Offset: 0x0004C52A
		// (set) Token: 0x0600121C RID: 4636 RVA: 0x0004E332 File Offset: 0x0004C532
		[MonitoringDescription("Type of this counter.")]
		[DefaultValue(typeof(PerformanceCounterType), "NumberOfItems32")]
		public PerformanceCounterType CounterType
		{
			get
			{
				return this.type;
			}
			set
			{
				if (!Enum.IsDefined(typeof(PerformanceCounterType), value))
				{
					throw new InvalidEnumArgumentException();
				}
				this.type = value;
			}
		}

		// Token: 0x04000A8C RID: 2700
		private string help = string.Empty;

		// Token: 0x04000A8D RID: 2701
		private string name;

		// Token: 0x04000A8E RID: 2702
		private PerformanceCounterType type;
	}
}
