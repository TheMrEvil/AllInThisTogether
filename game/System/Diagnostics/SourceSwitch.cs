using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Provides a multilevel switch to control tracing and debug output without recompiling your code.</summary>
	// Token: 0x02000223 RID: 547
	public class SourceSwitch : Switch
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SourceSwitch" /> class, specifying the name of the source.</summary>
		/// <param name="name">The name of the source.</param>
		// Token: 0x06000FE0 RID: 4064 RVA: 0x0004645F File Offset: 0x0004465F
		public SourceSwitch(string name) : base(name, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SourceSwitch" /> class, specifying the display name and the default value for the source switch.</summary>
		/// <param name="displayName">The name of the source switch.</param>
		/// <param name="defaultSwitchValue">The default value for the switch.</param>
		// Token: 0x06000FE1 RID: 4065 RVA: 0x0004646D File Offset: 0x0004466D
		public SourceSwitch(string displayName, string defaultSwitchValue) : base(displayName, string.Empty, defaultSwitchValue)
		{
		}

		/// <summary>Gets or sets the level of the switch.</summary>
		/// <returns>One of the <see cref="T:System.Diagnostics.SourceLevels" /> values that represents the event level of the switch.</returns>
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x0004647C File Offset: 0x0004467C
		// (set) Token: 0x06000FE3 RID: 4067 RVA: 0x00046484 File Offset: 0x00044684
		public SourceLevels Level
		{
			get
			{
				return (SourceLevels)base.SwitchSetting;
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				base.SwitchSetting = (int)value;
			}
		}

		/// <summary>Determines if trace listeners should be called, based on the trace event type.</summary>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values.</param>
		/// <returns>
		///   <see langword="true" /> if the trace listeners should be called; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000FE4 RID: 4068 RVA: 0x0004648D File Offset: 0x0004468D
		public bool ShouldTrace(TraceEventType eventType)
		{
			return (base.SwitchSetting & (int)eventType) != 0;
		}

		/// <summary>Invoked when the value of the <see cref="P:System.Diagnostics.Switch.Value" /> property changes.</summary>
		/// <exception cref="T:System.ArgumentException">The new value of <see cref="P:System.Diagnostics.Switch.Value" /> is not one of the <see cref="T:System.Diagnostics.SourceLevels" /> values.</exception>
		// Token: 0x06000FE5 RID: 4069 RVA: 0x0004649A File Offset: 0x0004469A
		protected override void OnValueChanged()
		{
			base.SwitchSetting = (int)Enum.Parse(typeof(SourceLevels), base.Value, true);
		}
	}
}
