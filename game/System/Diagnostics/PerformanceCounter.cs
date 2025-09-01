using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System.Diagnostics
{
	/// <summary>Represents a Windows NT performance counter component.</summary>
	// Token: 0x02000270 RID: 624
	[InstallerType(typeof(PerformanceCounterInstaller))]
	public sealed class PerformanceCounter : Component, ISupportInitialize
	{
		/// <summary>Initializes a new, read-only instance of the <see cref="T:System.Diagnostics.PerformanceCounter" /> class, without associating the instance with any system or custom performance counter.</summary>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		// Token: 0x060013BD RID: 5053 RVA: 0x00052310 File Offset: 0x00050510
		public PerformanceCounter()
		{
			this.categoryName = (this.counterName = (this.instanceName = ""));
			this.machineName = ".";
		}

		/// <summary>Initializes a new, read-only instance of the <see cref="T:System.Diagnostics.PerformanceCounter" /> class and associates it with the specified system or custom performance counter on the local computer. This constructor requires that the category have a single instance.</summary>
		/// <param name="categoryName">The name of the performance counter category (performance object) with which this performance counter is associated.</param>
		/// <param name="counterName">The name of the performance counter.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="categoryName" /> is an empty string ("").  
		/// -or-  
		/// <paramref name="counterName" /> is an empty string ("").  
		/// -or-  
		/// The category specified does not exist.  
		/// -or-  
		/// The category specified is marked as multi-instance and requires the performance counter to be created with an instance name.  
		/// -or-  
		/// <paramref name="categoryName" /> and <paramref name="counterName" /> have been localized into different languages.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> or <paramref name="counterName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x060013BE RID: 5054 RVA: 0x0005234B File Offset: 0x0005054B
		public PerformanceCounter(string categoryName, string counterName) : this(categoryName, counterName, false)
		{
		}

		/// <summary>Initializes a new, read-only or read/write instance of the <see cref="T:System.Diagnostics.PerformanceCounter" /> class and associates it with the specified system or custom performance counter on the local computer. This constructor requires that the category contain a single instance.</summary>
		/// <param name="categoryName">The name of the performance counter category (performance object) with which this performance counter is associated.</param>
		/// <param name="counterName">The name of the performance counter.</param>
		/// <param name="readOnly">
		///   <see langword="true" /> to access the counter in read-only mode (although the counter itself could be read/write); <see langword="false" /> to access the counter in read/write mode.</param>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="categoryName" /> is an empty string ("").  
		///  -or-  
		///  The <paramref name="counterName" /> is an empty string ("").  
		///  -or-  
		///  The category specified does not exist. (if <paramref name="readOnly" /> is <see langword="true" />).  
		///  -or-  
		///  The category specified is not a .NET Framework custom category (if <paramref name="readOnly" /> is <see langword="false" />).  
		///  -or-  
		///  The category specified is marked as multi-instance and requires the performance counter to be created with an instance name.  
		///  -or-  
		///  <paramref name="categoryName" /> and <paramref name="counterName" /> have been localized into different languages.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> or <paramref name="counterName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x060013BF RID: 5055 RVA: 0x00052356 File Offset: 0x00050556
		public PerformanceCounter(string categoryName, string counterName, bool readOnly) : this(categoryName, counterName, "", readOnly)
		{
		}

		/// <summary>Initializes a new, read-only instance of the <see cref="T:System.Diagnostics.PerformanceCounter" /> class and associates it with the specified system or custom performance counter and category instance on the local computer.</summary>
		/// <param name="categoryName">The name of the performance counter category (performance object) with which this performance counter is associated.</param>
		/// <param name="counterName">The name of the performance counter.</param>
		/// <param name="instanceName">The name of the performance counter category instance, or an empty string (""), if the category contains a single instance.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="categoryName" /> is an empty string ("").  
		/// -or-  
		/// <paramref name="counterName" /> is an empty string ("").  
		/// -or-  
		/// The category specified is not valid.  
		/// -or-  
		/// The category specified is marked as multi-instance and requires the performance counter to be created with an instance name.  
		/// -or-  
		/// <paramref name="instanceName" /> is longer than 127 characters.  
		/// -or-  
		/// <paramref name="categoryName" /> and <paramref name="counterName" /> have been localized into different languages.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> or <paramref name="counterName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x060013C0 RID: 5056 RVA: 0x00052366 File Offset: 0x00050566
		public PerformanceCounter(string categoryName, string counterName, string instanceName) : this(categoryName, counterName, instanceName, false)
		{
		}

		/// <summary>Initializes a new, read-only or read/write instance of the <see cref="T:System.Diagnostics.PerformanceCounter" /> class and associates it with the specified system or custom performance counter and category instance on the local computer.</summary>
		/// <param name="categoryName">The name of the performance counter category (performance object) with which this performance counter is associated.</param>
		/// <param name="counterName">The name of the performance counter.</param>
		/// <param name="instanceName">The name of the performance counter category instance, or an empty string (""), if the category contains a single instance.</param>
		/// <param name="readOnly">
		///   <see langword="true" /> to access a counter in read-only mode; <see langword="false" /> to access a counter in read/write mode.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="categoryName" /> is an empty string ("").  
		/// -or-  
		/// <paramref name="counterName" /> is an empty string ("").  
		/// -or-  
		/// The read/write permission setting requested is invalid for this counter.  
		/// -or-  
		/// The category specified does not exist (if <paramref name="readOnly" /> is <see langword="true" />).  
		/// -or-  
		/// The category specified is not a .NET Framework custom category (if <paramref name="readOnly" /> is <see langword="false" />).  
		/// -or-  
		/// The category specified is marked as multi-instance and requires the performance counter to be created with an instance name.  
		/// -or-  
		/// <paramref name="instanceName" /> is longer than 127 characters.  
		/// -or-  
		/// <paramref name="categoryName" /> and <paramref name="counterName" /> have been localized into different languages.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> or <paramref name="counterName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x060013C1 RID: 5057 RVA: 0x00052374 File Offset: 0x00050574
		public PerformanceCounter(string categoryName, string counterName, string instanceName, bool readOnly)
		{
			if (categoryName == null)
			{
				throw new ArgumentNullException("categoryName");
			}
			if (counterName == null)
			{
				throw new ArgumentNullException("counterName");
			}
			if (instanceName == null)
			{
				throw new ArgumentNullException("instanceName");
			}
			this.CategoryName = categoryName;
			this.CounterName = counterName;
			if (categoryName == "" || counterName == "")
			{
				throw new InvalidOperationException();
			}
			this.InstanceName = instanceName;
			this.instanceName = instanceName;
			this.machineName = ".";
			this.readOnly = readOnly;
			this.changed = true;
		}

		/// <summary>Initializes a new, read-only instance of the <see cref="T:System.Diagnostics.PerformanceCounter" /> class and associates it with the specified system or custom performance counter and category instance, on the specified computer.</summary>
		/// <param name="categoryName">The name of the performance counter category (performance object) with which this performance counter is associated.</param>
		/// <param name="counterName">The name of the performance counter.</param>
		/// <param name="instanceName">The name of the performance counter category instance, or an empty string (""), if the category contains a single instance.</param>
		/// <param name="machineName">The computer on which the performance counter and its associated category exist.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="categoryName" /> is an empty string ("").  
		/// -or-  
		/// <paramref name="counterName" /> is an empty string ("").  
		/// -or-  
		/// The read/write permission setting requested is invalid for this counter.  
		/// -or-  
		/// The counter does not exist on the specified computer.  
		/// -or-  
		/// The category specified is marked as multi-instance and requires the performance counter to be created with an instance name.  
		/// -or-  
		/// <paramref name="instanceName" /> is longer than 127 characters.  
		/// -or-  
		/// <paramref name="categoryName" /> and <paramref name="counterName" /> have been localized into different languages.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> parameter is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> or <paramref name="counterName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x060013C2 RID: 5058 RVA: 0x00052407 File Offset: 0x00050607
		public PerformanceCounter(string categoryName, string counterName, string instanceName, string machineName) : this(categoryName, counterName, instanceName, false)
		{
			this.machineName = machineName;
		}

		// Token: 0x060013C3 RID: 5059
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr GetImpl_icall(char* category, int category_length, char* counter, int counter_length, char* instance, int instance_length, out PerformanceCounterType ctype, out bool custom);

		// Token: 0x060013C4 RID: 5060 RVA: 0x0005241C File Offset: 0x0005061C
		private unsafe static IntPtr GetImpl(string category, string counter, string instance, out PerformanceCounterType ctype, out bool custom)
		{
			char* ptr = category;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = counter;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr3 = instance;
			if (ptr3 != null)
			{
				ptr3 += RuntimeHelpers.OffsetToStringData / 2;
			}
			return PerformanceCounter.GetImpl_icall(ptr, (category != null) ? category.Length : 0, ptr2, (counter != null) ? counter.Length : 0, ptr3, (instance != null) ? instance.Length : 0, out ctype, out custom);
		}

		// Token: 0x060013C5 RID: 5061
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetSample(IntPtr impl, bool only_value, out CounterSample sample);

		// Token: 0x060013C6 RID: 5062
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long UpdateValue(IntPtr impl, bool do_incr, long value);

		// Token: 0x060013C7 RID: 5063
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FreeData(IntPtr impl);

		// Token: 0x060013C8 RID: 5064 RVA: 0x0005248C File Offset: 0x0005068C
		private static bool IsValidMachine(string machine)
		{
			return machine == ".";
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0005249C File Offset: 0x0005069C
		private void UpdateInfo()
		{
			if (this.impl != IntPtr.Zero)
			{
				this.Close();
			}
			if (PerformanceCounter.IsValidMachine(this.machineName))
			{
				this.impl = PerformanceCounter.GetImpl(this.categoryName, this.counterName, this.instanceName, out this.type, out this.is_custom);
			}
			if (!this.is_custom)
			{
				this.readOnly = true;
			}
			this.changed = false;
		}

		/// <summary>Gets or sets the name of the performance counter category for this performance counter.</summary>
		/// <returns>The name of the performance counter category (performance object) with which this performance counter is associated.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Diagnostics.PerformanceCounter.CategoryName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060013CA RID: 5066 RVA: 0x0005250D File Offset: 0x0005070D
		// (set) Token: 0x060013CB RID: 5067 RVA: 0x00052515 File Offset: 0x00050715
		[SRDescription("The category name for this performance counter.")]
		[ReadOnly(true)]
		[DefaultValue("")]
		[TypeConverter("System.Diagnostics.Design.CategoryValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SettingsBindable(true)]
		public string CategoryName
		{
			get
			{
				return this.categoryName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("categoryName");
				}
				this.categoryName = value;
				this.changed = true;
			}
		}

		/// <summary>Gets the description for this performance counter.</summary>
		/// <returns>A description of the item or quantity that this performance counter measures.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Diagnostics.PerformanceCounter" /> instance is not associated with a performance counter.  
		///  -or-  
		///  The <see cref="P:System.Diagnostics.PerformanceCounter.InstanceLifetime" /> property is set to <see cref="F:System.Diagnostics.PerformanceCounterInstanceLifetime.Process" /> when using global shared memory.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x00052533 File Offset: 0x00050733
		[MonitoringDescription("A description describing the counter.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ReadOnly(true)]
		[MonoTODO]
		public string CounterHelp
		{
			get
			{
				return "";
			}
		}

		/// <summary>Gets or sets the name of the performance counter that is associated with this <see cref="T:System.Diagnostics.PerformanceCounter" /> instance.</summary>
		/// <returns>The name of the counter, which generally describes the quantity being counted. This name is displayed in the list of counters of the Performance Counter Manager MMC snap in's Add Counters dialog box.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Diagnostics.PerformanceCounter.CounterName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x0005253A File Offset: 0x0005073A
		// (set) Token: 0x060013CE RID: 5070 RVA: 0x00052542 File Offset: 0x00050742
		[TypeConverter("System.Diagnostics.Design.CounterNameConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		[SRDescription("The name of this performance counter.")]
		[SettingsBindable(true)]
		[ReadOnly(true)]
		public string CounterName
		{
			get
			{
				return this.counterName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("counterName");
				}
				this.counterName = value;
				this.changed = true;
			}
		}

		/// <summary>Gets the counter type of the associated performance counter.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterType" /> that describes both how the counter interacts with a monitoring application and the nature of the values it contains (for example, calculated or uncalculated).</returns>
		/// <exception cref="T:System.InvalidOperationException">The instance is not correctly associated with a performance counter.  
		///  -or-  
		///  The <see cref="P:System.Diagnostics.PerformanceCounter.InstanceLifetime" /> property is set to <see cref="F:System.Diagnostics.PerformanceCounterInstanceLifetime.Process" /> when using global shared memory.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x00052560 File Offset: 0x00050760
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The type of the counter.")]
		public PerformanceCounterType CounterType
		{
			get
			{
				if (this.changed)
				{
					this.UpdateInfo();
				}
				return this.type;
			}
		}

		/// <summary>Gets or sets the lifetime of a process.</summary>
		/// <returns>One of the <see cref="T:System.Diagnostics.PerformanceCounterInstanceLifetime" /> values. The default is <see cref="F:System.Diagnostics.PerformanceCounterInstanceLifetime.Global" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value set is not a member of the <see cref="T:System.Diagnostics.PerformanceCounterInstanceLifetime" /> enumeration.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Diagnostics.PerformanceCounter.InstanceLifetime" /> is set after the <see cref="T:System.Diagnostics.PerformanceCounter" /> has been initialized.</exception>
		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x00052576 File Offset: 0x00050776
		// (set) Token: 0x060013D1 RID: 5073 RVA: 0x0005257E File Offset: 0x0005077E
		[MonoTODO]
		[DefaultValue(PerformanceCounterInstanceLifetime.Global)]
		public PerformanceCounterInstanceLifetime InstanceLifetime
		{
			get
			{
				return this.lifetime;
			}
			set
			{
				this.lifetime = value;
			}
		}

		/// <summary>Gets or sets an instance name for this performance counter.</summary>
		/// <returns>The name of the performance counter category instance, or an empty string (""), if the counter is a single-instance counter.</returns>
		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x00052587 File Offset: 0x00050787
		// (set) Token: 0x060013D3 RID: 5075 RVA: 0x0005258F File Offset: 0x0005078F
		[DefaultValue("")]
		[ReadOnly(true)]
		[SettingsBindable(true)]
		[TypeConverter("System.Diagnostics.Design.InstanceNameConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SRDescription("The instance name for this performance counter.")]
		public string InstanceName
		{
			get
			{
				return this.instanceName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.instanceName = value;
				this.changed = true;
			}
		}

		/// <summary>Gets or sets the computer name for this performance counter</summary>
		/// <returns>The server on which the performance counter and its associated category reside.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.PerformanceCounter.MachineName" /> format is invalid.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x000525AD File Offset: 0x000507AD
		// (set) Token: 0x060013D5 RID: 5077 RVA: 0x000525B8 File Offset: 0x000507B8
		[SRDescription("The machine where this performance counter resides.")]
		[SettingsBindable(true)]
		[DefaultValue(".")]
		[MonoTODO("What's the machine name format?")]
		[Browsable(false)]
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value == "" || value == ".")
				{
					this.machineName = ".";
					this.changed = true;
					return;
				}
				throw new PlatformNotSupportedException();
			}
		}

		/// <summary>Gets or sets the raw, or uncalculated, value of this counter.</summary>
		/// <returns>The raw value of the counter.</returns>
		/// <exception cref="T:System.InvalidOperationException">You are trying to set the counter's raw value, but the counter is read-only.  
		///  -or-  
		///  The instance is not correctly associated with a performance counter.  
		///  -or-  
		///  The <see cref="P:System.Diagnostics.PerformanceCounter.InstanceLifetime" /> property is set to <see cref="F:System.Diagnostics.PerformanceCounterInstanceLifetime.Process" /> when using global shared memory.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x00052608 File Offset: 0x00050808
		// (set) Token: 0x060013D7 RID: 5079 RVA: 0x00052639 File Offset: 0x00050839
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The raw value of the counter.")]
		public long RawValue
		{
			get
			{
				if (this.changed)
				{
					this.UpdateInfo();
				}
				CounterSample counterSample;
				PerformanceCounter.GetSample(this.impl, true, out counterSample);
				return counterSample.RawValue;
			}
			set
			{
				if (this.changed)
				{
					this.UpdateInfo();
				}
				if (this.readOnly)
				{
					throw new InvalidOperationException();
				}
				PerformanceCounter.UpdateValue(this.impl, false, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether this <see cref="T:System.Diagnostics.PerformanceCounter" /> instance is in read-only mode.</summary>
		/// <returns>
		///   <see langword="true" />, if the <see cref="T:System.Diagnostics.PerformanceCounter" /> instance is in read-only mode (even if the counter itself is a custom .NET Framework counter); <see langword="false" /> if it is in read/write mode. The default is the value set by the constructor.</returns>
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x00052665 File Offset: 0x00050865
		// (set) Token: 0x060013D9 RID: 5081 RVA: 0x0005266D File Offset: 0x0005086D
		[Browsable(false)]
		[MonitoringDescription("The accessability level of the counter.")]
		[DefaultValue(true)]
		public bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
			set
			{
				this.readOnly = value;
			}
		}

		/// <summary>Begins the initialization of a <see cref="T:System.Diagnostics.PerformanceCounter" /> instance used on a form or by another component. The initialization occurs at runtime.</summary>
		// Token: 0x060013DA RID: 5082 RVA: 0x00003917 File Offset: 0x00001B17
		public void BeginInit()
		{
		}

		/// <summary>Ends the initialization of a <see cref="T:System.Diagnostics.PerformanceCounter" /> instance that is used on a form or by another component. The initialization occurs at runtime.</summary>
		// Token: 0x060013DB RID: 5083 RVA: 0x00003917 File Offset: 0x00001B17
		public void EndInit()
		{
		}

		/// <summary>Closes the performance counter and frees all the resources allocated by this performance counter instance.</summary>
		// Token: 0x060013DC RID: 5084 RVA: 0x00052678 File Offset: 0x00050878
		public void Close()
		{
			IntPtr value = this.impl;
			this.impl = IntPtr.Zero;
			if (value != IntPtr.Zero)
			{
				PerformanceCounter.FreeData(value);
			}
		}

		/// <summary>Frees the performance counter library shared state allocated by the counters.</summary>
		// Token: 0x060013DD RID: 5085 RVA: 0x00003917 File Offset: 0x00001B17
		public static void CloseSharedResources()
		{
		}

		/// <summary>Decrements the associated performance counter by one through an efficient atomic operation.</summary>
		/// <returns>The decremented counter value.</returns>
		/// <exception cref="T:System.InvalidOperationException">The counter is read-only, so the application cannot decrement it.  
		///  -or-  
		///  The instance is not correctly associated with a performance counter.  
		///  -or-  
		///  The <see cref="P:System.Diagnostics.PerformanceCounter.InstanceLifetime" /> property is set to <see cref="F:System.Diagnostics.PerformanceCounterInstanceLifetime.Process" /> when using global shared memory.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		// Token: 0x060013DE RID: 5086 RVA: 0x000526AA File Offset: 0x000508AA
		public long Decrement()
		{
			return this.IncrementBy(-1L);
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x000526B4 File Offset: 0x000508B4
		protected override void Dispose(bool disposing)
		{
			this.Close();
		}

		/// <summary>Increments the associated performance counter by one through an efficient atomic operation.</summary>
		/// <returns>The incremented counter value.</returns>
		/// <exception cref="T:System.InvalidOperationException">The counter is read-only, so the application cannot increment it.  
		///  -or-  
		///  The instance is not correctly associated with a performance counter.  
		///  -or-  
		///  The <see cref="P:System.Diagnostics.PerformanceCounter.InstanceLifetime" /> property is set to <see cref="F:System.Diagnostics.PerformanceCounterInstanceLifetime.Process" /> when using global shared memory.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		// Token: 0x060013E0 RID: 5088 RVA: 0x000526BC File Offset: 0x000508BC
		public long Increment()
		{
			return this.IncrementBy(1L);
		}

		/// <summary>Increments or decrements the value of the associated performance counter by a specified amount through an efficient atomic operation.</summary>
		/// <param name="value">The value to increment by. (A negative value decrements the counter.)</param>
		/// <returns>The new counter value.</returns>
		/// <exception cref="T:System.InvalidOperationException">The counter is read-only, so the application cannot increment it.  
		///  -or-  
		///  The instance is not correctly associated with a performance counter.  
		///  -or-  
		///  The <see cref="P:System.Diagnostics.PerformanceCounter.InstanceLifetime" /> property is set to <see cref="F:System.Diagnostics.PerformanceCounterInstanceLifetime.Process" /> when using global shared memory.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		// Token: 0x060013E1 RID: 5089 RVA: 0x000526C6 File Offset: 0x000508C6
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public long IncrementBy(long value)
		{
			if (this.changed)
			{
				this.UpdateInfo();
			}
			if (this.readOnly)
			{
				return 0L;
			}
			return PerformanceCounter.UpdateValue(this.impl, true, value);
		}

		/// <summary>Obtains a counter sample, and returns the raw, or uncalculated, value for it.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.CounterSample" /> that represents the next raw value that the system obtains for this counter.</returns>
		/// <exception cref="T:System.InvalidOperationException">The instance is not correctly associated with a performance counter.  
		///  -or-  
		///  The <see cref="P:System.Diagnostics.PerformanceCounter.InstanceLifetime" /> property is set to <see cref="F:System.Diagnostics.PerformanceCounterInstanceLifetime.Process" /> when using global shared memory.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x060013E2 RID: 5090 RVA: 0x000526F0 File Offset: 0x000508F0
		public CounterSample NextSample()
		{
			if (this.changed)
			{
				this.UpdateInfo();
			}
			CounterSample result;
			PerformanceCounter.GetSample(this.impl, false, out result);
			this.valid_old = true;
			this.old_sample = result;
			return result;
		}

		/// <summary>Obtains a counter sample and returns the calculated value for it.</summary>
		/// <returns>The next calculated value that the system obtains for this counter.</returns>
		/// <exception cref="T:System.InvalidOperationException">The instance is not correctly associated with a performance counter.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x060013E3 RID: 5091 RVA: 0x0005272C File Offset: 0x0005092C
		public float NextValue()
		{
			if (this.changed)
			{
				this.UpdateInfo();
			}
			CounterSample newSample;
			PerformanceCounter.GetSample(this.impl, false, out newSample);
			float result;
			if (this.valid_old)
			{
				result = CounterSampleCalculator.ComputeCounterValue(this.old_sample, newSample);
			}
			else
			{
				result = CounterSampleCalculator.ComputeCounterValue(newSample);
			}
			this.valid_old = true;
			this.old_sample = newSample;
			return result;
		}

		/// <summary>Deletes the category instance specified by the <see cref="T:System.Diagnostics.PerformanceCounter" /> object <see cref="P:System.Diagnostics.PerformanceCounter.InstanceName" /> property.</summary>
		/// <exception cref="T:System.InvalidOperationException">This counter is read-only, so any instance that is associated with the category cannot be removed.  
		///  -or-  
		///  The instance is not correctly associated with a performance counter.  
		///  -or-  
		///  The <see cref="P:System.Diagnostics.PerformanceCounter.InstanceLifetime" /> property is set to <see cref="F:System.Diagnostics.PerformanceCounterInstanceLifetime.Process" /> when using global shared memory.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		// Token: 0x060013E4 RID: 5092 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public void RemoveInstance()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x00052783 File Offset: 0x00050983
		// Note: this type is marked as 'beforefieldinit'.
		static PerformanceCounter()
		{
		}

		// Token: 0x04000B0C RID: 2828
		private string categoryName;

		// Token: 0x04000B0D RID: 2829
		private string counterName;

		// Token: 0x04000B0E RID: 2830
		private string instanceName;

		// Token: 0x04000B0F RID: 2831
		private string machineName;

		// Token: 0x04000B10 RID: 2832
		private IntPtr impl;

		// Token: 0x04000B11 RID: 2833
		private PerformanceCounterType type;

		// Token: 0x04000B12 RID: 2834
		private CounterSample old_sample;

		// Token: 0x04000B13 RID: 2835
		private bool readOnly;

		// Token: 0x04000B14 RID: 2836
		private bool valid_old;

		// Token: 0x04000B15 RID: 2837
		private bool changed;

		// Token: 0x04000B16 RID: 2838
		private bool is_custom;

		// Token: 0x04000B17 RID: 2839
		private PerformanceCounterInstanceLifetime lifetime;

		/// <summary>Specifies the size, in bytes, of the global memory shared by performance counters. The default size is 524,288 bytes.</summary>
		// Token: 0x04000B18 RID: 2840
		[Obsolete]
		public static int DefaultFileMappingSize = 524288;
	}
}
