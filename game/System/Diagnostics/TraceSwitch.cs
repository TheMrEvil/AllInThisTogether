using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Provides a multilevel switch to control tracing and debug output without recompiling your code.</summary>
	// Token: 0x02000236 RID: 566
	[SwitchLevel(typeof(TraceLevel))]
	public class TraceSwitch : Switch
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TraceSwitch" /> class, using the specified display name and description.</summary>
		/// <param name="displayName">The name to display on a user interface.</param>
		/// <param name="description">The description of the switch.</param>
		// Token: 0x060010FD RID: 4349 RVA: 0x00044ADF File Offset: 0x00042CDF
		public TraceSwitch(string displayName, string description) : base(displayName, description)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TraceSwitch" /> class, using the specified display name, description, and default value for the switch.</summary>
		/// <param name="displayName">The name to display on a user interface.</param>
		/// <param name="description">The description of the switch.</param>
		/// <param name="defaultSwitchValue">The default value of the switch.</param>
		// Token: 0x060010FE RID: 4350 RVA: 0x00044AE9 File Offset: 0x00042CE9
		public TraceSwitch(string displayName, string description, string defaultSwitchValue) : base(displayName, description, defaultSwitchValue)
		{
		}

		/// <summary>Gets or sets the trace level that determines the messages the switch allows.</summary>
		/// <returns>One of the <see cref="T:System.Diagnostics.TraceLevel" /> values that specifies the level of messages that are allowed by the switch.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Diagnostics.TraceSwitch.Level" /> is set to a value that is not one of the <see cref="T:System.Diagnostics.TraceLevel" /> values.</exception>
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x0004647C File Offset: 0x0004467C
		// (set) Token: 0x06001100 RID: 4352 RVA: 0x0004A759 File Offset: 0x00048959
		public TraceLevel Level
		{
			get
			{
				return (TraceLevel)base.SwitchSetting;
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				if (value < TraceLevel.Off || value > TraceLevel.Verbose)
				{
					throw new ArgumentException(SR.GetString("The Level must be set to a value in the enumeration TraceLevel."));
				}
				base.SwitchSetting = (int)value;
			}
		}

		/// <summary>Gets a value indicating whether the switch allows error-handling messages.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Diagnostics.TraceSwitch.Level" /> property is set to <see cref="F:System.Diagnostics.TraceLevel.Error" />, <see cref="F:System.Diagnostics.TraceLevel.Warning" />, <see cref="F:System.Diagnostics.TraceLevel.Info" />, or <see cref="F:System.Diagnostics.TraceLevel.Verbose" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06001101 RID: 4353 RVA: 0x0004A77A File Offset: 0x0004897A
		public bool TraceError
		{
			get
			{
				return this.Level >= TraceLevel.Error;
			}
		}

		/// <summary>Gets a value indicating whether the switch allows warning messages.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Diagnostics.TraceSwitch.Level" /> property is set to <see cref="F:System.Diagnostics.TraceLevel.Warning" />, <see cref="F:System.Diagnostics.TraceLevel.Info" />, or <see cref="F:System.Diagnostics.TraceLevel.Verbose" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06001102 RID: 4354 RVA: 0x0004A788 File Offset: 0x00048988
		public bool TraceWarning
		{
			get
			{
				return this.Level >= TraceLevel.Warning;
			}
		}

		/// <summary>Gets a value indicating whether the switch allows informational messages.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Diagnostics.TraceSwitch.Level" /> property is set to <see cref="F:System.Diagnostics.TraceLevel.Info" /> or <see cref="F:System.Diagnostics.TraceLevel.Verbose" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x0004A796 File Offset: 0x00048996
		public bool TraceInfo
		{
			get
			{
				return this.Level >= TraceLevel.Info;
			}
		}

		/// <summary>Gets a value indicating whether the switch allows all messages.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Diagnostics.TraceSwitch.Level" /> property is set to <see cref="F:System.Diagnostics.TraceLevel.Verbose" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06001104 RID: 4356 RVA: 0x0004A7A4 File Offset: 0x000489A4
		public bool TraceVerbose
		{
			get
			{
				return this.Level == TraceLevel.Verbose;
			}
		}

		/// <summary>Updates and corrects the level for this switch.</summary>
		// Token: 0x06001105 RID: 4357 RVA: 0x0004A7B0 File Offset: 0x000489B0
		protected override void OnSwitchSettingChanged()
		{
			int switchSetting = base.SwitchSetting;
			if (switchSetting < 0)
			{
				base.SwitchSetting = 0;
				return;
			}
			if (switchSetting > 4)
			{
				base.SwitchSetting = 4;
			}
		}

		/// <summary>Sets the <see cref="P:System.Diagnostics.Switch.SwitchSetting" /> property to the integer equivalent of the <see cref="P:System.Diagnostics.Switch.Value" /> property.</summary>
		// Token: 0x06001106 RID: 4358 RVA: 0x0004A7DB File Offset: 0x000489DB
		protected override void OnValueChanged()
		{
			base.SwitchSetting = (int)Enum.Parse(typeof(TraceLevel), base.Value, true);
		}
	}
}
