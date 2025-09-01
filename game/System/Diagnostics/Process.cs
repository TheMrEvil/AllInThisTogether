using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics
{
	/// <summary>Provides access to local and remote processes and enables you to start and stop local system processes.</summary>
	// Token: 0x0200023C RID: 572
	[DefaultEvent("Exited")]
	[Designer("System.Diagnostics.Design.ProcessDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[MonitoringDescription("Provides access to local and remote processes, enabling starting and stopping of local processes.")]
	[DefaultProperty("StartInfo")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true, Synchronization = true, ExternalProcessMgmt = true, SelfAffectingProcessMgmt = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class Process : Component
	{
		/// <summary>Occurs each time an application writes a line to its redirected <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream.</summary>
		// Token: 0x14000019 RID: 25
		// (add) Token: 0x0600113D RID: 4413 RVA: 0x0004BC10 File Offset: 0x00049E10
		// (remove) Token: 0x0600113E RID: 4414 RVA: 0x0004BC48 File Offset: 0x00049E48
		[Browsable(true)]
		[MonitoringDescription("Indicates if the process component is associated with a real process.")]
		public event DataReceivedEventHandler OutputDataReceived
		{
			[CompilerGenerated]
			add
			{
				DataReceivedEventHandler dataReceivedEventHandler = this.OutputDataReceived;
				DataReceivedEventHandler dataReceivedEventHandler2;
				do
				{
					dataReceivedEventHandler2 = dataReceivedEventHandler;
					DataReceivedEventHandler value2 = (DataReceivedEventHandler)Delegate.Combine(dataReceivedEventHandler2, value);
					dataReceivedEventHandler = Interlocked.CompareExchange<DataReceivedEventHandler>(ref this.OutputDataReceived, value2, dataReceivedEventHandler2);
				}
				while (dataReceivedEventHandler != dataReceivedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				DataReceivedEventHandler dataReceivedEventHandler = this.OutputDataReceived;
				DataReceivedEventHandler dataReceivedEventHandler2;
				do
				{
					dataReceivedEventHandler2 = dataReceivedEventHandler;
					DataReceivedEventHandler value2 = (DataReceivedEventHandler)Delegate.Remove(dataReceivedEventHandler2, value);
					dataReceivedEventHandler = Interlocked.CompareExchange<DataReceivedEventHandler>(ref this.OutputDataReceived, value2, dataReceivedEventHandler2);
				}
				while (dataReceivedEventHandler != dataReceivedEventHandler2);
			}
		}

		/// <summary>Occurs when an application writes to its redirected <see cref="P:System.Diagnostics.Process.StandardError" /> stream.</summary>
		// Token: 0x1400001A RID: 26
		// (add) Token: 0x0600113F RID: 4415 RVA: 0x0004BC80 File Offset: 0x00049E80
		// (remove) Token: 0x06001140 RID: 4416 RVA: 0x0004BCB8 File Offset: 0x00049EB8
		[MonitoringDescription("Indicates if the process component is associated with a real process.")]
		[Browsable(true)]
		public event DataReceivedEventHandler ErrorDataReceived
		{
			[CompilerGenerated]
			add
			{
				DataReceivedEventHandler dataReceivedEventHandler = this.ErrorDataReceived;
				DataReceivedEventHandler dataReceivedEventHandler2;
				do
				{
					dataReceivedEventHandler2 = dataReceivedEventHandler;
					DataReceivedEventHandler value2 = (DataReceivedEventHandler)Delegate.Combine(dataReceivedEventHandler2, value);
					dataReceivedEventHandler = Interlocked.CompareExchange<DataReceivedEventHandler>(ref this.ErrorDataReceived, value2, dataReceivedEventHandler2);
				}
				while (dataReceivedEventHandler != dataReceivedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				DataReceivedEventHandler dataReceivedEventHandler = this.ErrorDataReceived;
				DataReceivedEventHandler dataReceivedEventHandler2;
				do
				{
					dataReceivedEventHandler2 = dataReceivedEventHandler;
					DataReceivedEventHandler value2 = (DataReceivedEventHandler)Delegate.Remove(dataReceivedEventHandler2, value);
					dataReceivedEventHandler = Interlocked.CompareExchange<DataReceivedEventHandler>(ref this.ErrorDataReceived, value2, dataReceivedEventHandler2);
				}
				while (dataReceivedEventHandler != dataReceivedEventHandler2);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Process" /> class.</summary>
		// Token: 0x06001141 RID: 4417 RVA: 0x0004BCED File Offset: 0x00049EED
		public Process()
		{
			this.machineName = ".";
			this.outputStreamReadMode = Process.StreamReadMode.undefined;
			this.errorStreamReadMode = Process.StreamReadMode.undefined;
			this.m_processAccess = 2035711;
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0004BD19 File Offset: 0x00049F19
		private Process(string machineName, bool isRemoteMachine, int processId, ProcessInfo processInfo)
		{
			this.machineName = machineName;
			this.isRemoteMachine = isRemoteMachine;
			this.processId = processId;
			this.haveProcessId = true;
			this.outputStreamReadMode = Process.StreamReadMode.undefined;
			this.errorStreamReadMode = Process.StreamReadMode.undefined;
			this.m_processAccess = 2035711;
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x0004BD56 File Offset: 0x00049F56
		[MonitoringDescription("Indicates if the process component is associated with a real process.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		private bool Associated
		{
			get
			{
				return this.haveProcessId || this.haveProcessHandle;
			}
		}

		/// <summary>Gets the value that the associated process specified when it terminated.</summary>
		/// <returns>The code that the associated process specified when it terminated.</returns>
		/// <exception cref="T:System.InvalidOperationException">The process has not exited.  
		///  -or-  
		///  The process <see cref="P:System.Diagnostics.Process.Handle" /> is not valid.</exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.ExitCode" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x0004BD68 File Offset: 0x00049F68
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The value returned from the associated process when it terminated.")]
		public int ExitCode
		{
			get
			{
				this.EnsureState(Process.State.Exited);
				if (this.exitCode == -1 && !RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				{
					throw new InvalidOperationException("Cannot get the exit code from a non-child process on Unix");
				}
				return this.exitCode;
			}
		}

		/// <summary>Gets a value indicating whether the associated process has been terminated.</summary>
		/// <returns>
		///   <see langword="true" /> if the operating system process referenced by the <see cref="T:System.Diagnostics.Process" /> component has terminated; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">There is no process associated with the object.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The exit code for the process could not be retrieved.</exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.HasExited" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x0004BD98 File Offset: 0x00049F98
		[MonitoringDescription("Indicates if the associated process has been terminated.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool HasExited
		{
			get
			{
				if (!this.exited)
				{
					this.EnsureState(Process.State.Associated);
					SafeProcessHandle safeProcessHandle = null;
					try
					{
						safeProcessHandle = this.GetProcessHandle(1049600, false);
						int num;
						if (safeProcessHandle.IsInvalid)
						{
							this.exited = true;
						}
						else if (NativeMethods.GetExitCodeProcess(safeProcessHandle, out num) && num != 259)
						{
							this.exited = true;
							this.exitCode = num;
						}
						else
						{
							if (!this.signaled)
							{
								ProcessWaitHandle processWaitHandle = null;
								try
								{
									processWaitHandle = new ProcessWaitHandle(safeProcessHandle);
									this.signaled = processWaitHandle.WaitOne(0, false);
								}
								finally
								{
									if (processWaitHandle != null)
									{
										processWaitHandle.Close();
									}
								}
							}
							if (this.signaled)
							{
								if (!NativeMethods.GetExitCodeProcess(safeProcessHandle, out num))
								{
									throw new Win32Exception();
								}
								this.exited = true;
								this.exitCode = num;
							}
						}
					}
					finally
					{
						this.ReleaseProcessHandle(safeProcessHandle);
					}
					if (this.exited)
					{
						this.RaiseOnExited();
					}
				}
				return this.exited;
			}
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0004BE88 File Offset: 0x0004A088
		private ProcessThreadTimes GetProcessTimes()
		{
			ProcessThreadTimes processThreadTimes = new ProcessThreadTimes();
			SafeProcessHandle safeProcessHandle = null;
			try
			{
				int access = 1024;
				if (EnvironmentHelpers.IsWindowsVistaOrAbove())
				{
					access = 4096;
				}
				safeProcessHandle = this.GetProcessHandle(access, false);
				if (safeProcessHandle.IsInvalid)
				{
					throw new InvalidOperationException(SR.GetString("Cannot process request because the process ({0}) has exited.", new object[]
					{
						this.processId.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (!NativeMethods.GetProcessTimes(safeProcessHandle, out processThreadTimes.create, out processThreadTimes.exit, out processThreadTimes.kernel, out processThreadTimes.user))
				{
					throw new Win32Exception();
				}
			}
			finally
			{
				this.ReleaseProcessHandle(safeProcessHandle);
			}
			return processThreadTimes;
		}

		/// <summary>Gets the time that the associated process exited.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that indicates when the associated process was terminated.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.ExitTime" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06001147 RID: 4423 RVA: 0x0004BF2C File Offset: 0x0004A12C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The time that the associated process exited.")]
		public DateTime ExitTime
		{
			get
			{
				if (!this.haveExitTime)
				{
					this.EnsureState((Process.State)20);
					this.exitTime = this.GetProcessTimes().ExitTime;
					this.haveExitTime = true;
				}
				return this.exitTime;
			}
		}

		/// <summary>Gets the native handle of the associated process.</summary>
		/// <returns>The handle that the operating system assigned to the associated process when the process was started. The system uses this handle to keep track of process attributes.</returns>
		/// <exception cref="T:System.InvalidOperationException">The process has not been started or has exited. The <see cref="P:System.Diagnostics.Process.Handle" /> property cannot be read because there is no process associated with this <see cref="T:System.Diagnostics.Process" /> instance.  
		///  -or-  
		///  The <see cref="T:System.Diagnostics.Process" /> instance has been attached to a running process but you do not have the necessary permissions to get a handle with full access rights.</exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.Handle" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x0004BF5C File Offset: 0x0004A15C
		[MonitoringDescription("Returns the native handle for this process.   The handle is only available if the process was started using this component.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public IntPtr Handle
		{
			get
			{
				this.EnsureState(Process.State.Associated);
				return this.OpenProcessHandle(this.m_processAccess).DangerousGetHandle();
			}
		}

		/// <summary>Gets the native handle to this process.</summary>
		/// <returns>The native handle to this process.</returns>
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x0004BF77 File Offset: 0x0004A177
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SafeProcessHandle SafeHandle
		{
			get
			{
				this.EnsureState(Process.State.Associated);
				return this.OpenProcessHandle(this.m_processAccess);
			}
		}

		/// <summary>Gets the unique identifier for the associated process.</summary>
		/// <returns>The system-generated unique identifier of the process that is referenced by this <see cref="T:System.Diagnostics.Process" /> instance.</returns>
		/// <exception cref="T:System.InvalidOperationException">The process's <see cref="P:System.Diagnostics.Process.Id" /> property has not been set.  
		///  -or-  
		///  There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property to <see langword="false" /> to access this property on Windows 98 and Windows Me.</exception>
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x0004BF8D File Offset: 0x0004A18D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The unique identifier for the process.")]
		public int Id
		{
			get
			{
				this.EnsureState(Process.State.HaveId);
				return this.processId;
			}
		}

		/// <summary>Gets the name of the computer the associated process is running on.</summary>
		/// <returns>The name of the computer that the associated process is running on.</returns>
		/// <exception cref="T:System.InvalidOperationException">There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.</exception>
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x0004BF9C File Offset: 0x0004A19C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The name of the machine the running the process.")]
		public string MachineName
		{
			get
			{
				this.EnsureState(Process.State.Associated);
				return this.machineName;
			}
		}

		/// <summary>Gets or sets the maximum allowable working set size, in bytes, for the associated process.</summary>
		/// <returns>The maximum working set size that is allowed in memory for the process, in bytes.</returns>
		/// <exception cref="T:System.ArgumentException">The maximum working set size is invalid. It must be greater than or equal to the minimum working set size.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">Working set information cannot be retrieved from the associated process resource.  
		///  -or-  
		///  The process identifier or process handle is zero because the process has not been started.</exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.MaxWorkingSet" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> is not available.  
		///  -or-  
		///  The process has exited.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x0600114C RID: 4428 RVA: 0x0004BFAC File Offset: 0x0004A1AC
		// (set) Token: 0x0600114D RID: 4429 RVA: 0x0004BFBA File Offset: 0x0004A1BA
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The maximum amount of physical memory the process has required since it was started.")]
		public IntPtr MaxWorkingSet
		{
			get
			{
				this.EnsureWorkingSetLimits();
				return this.maxWorkingSet;
			}
			set
			{
				this.SetWorkingSetLimits(null, value);
			}
		}

		/// <summary>Gets or sets the minimum allowable working set size, in bytes, for the associated process.</summary>
		/// <returns>The minimum working set size that is required in memory for the process, in bytes.</returns>
		/// <exception cref="T:System.ArgumentException">The minimum working set size is invalid. It must be less than or equal to the maximum working set size.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">Working set information cannot be retrieved from the associated process resource.  
		///  -or-  
		///  The process identifier or process handle is zero because the process has not been started.</exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.MinWorkingSet" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> is not available.  
		///  -or-  
		///  The process has exited.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x170002EF RID: 751
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x0004BFC9 File Offset: 0x0004A1C9
		// (set) Token: 0x0600114F RID: 4431 RVA: 0x0004BFD7 File Offset: 0x0004A1D7
		[MonitoringDescription("The minimum amount of physical memory the process has required since it was started.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IntPtr MinWorkingSet
		{
			get
			{
				this.EnsureWorkingSetLimits();
				return this.minWorkingSet;
			}
			set
			{
				this.SetWorkingSetLimits(value, null);
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x0004BFE6 File Offset: 0x0004A1E6
		private OperatingSystem OperatingSystem
		{
			get
			{
				if (this.operatingSystem == null)
				{
					this.operatingSystem = Environment.OSVersion;
				}
				return this.operatingSystem;
			}
		}

		/// <summary>Gets or sets the overall priority category for the associated process.</summary>
		/// <returns>The priority category for the associated process, from which the <see cref="P:System.Diagnostics.Process.BasePriority" /> of the process is calculated.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">Process priority information could not be set or retrieved from the associated process resource.  
		///  -or-  
		///  The process identifier or process handle is zero. (The process has not been started.)</exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.PriorityClass" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> is not available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">You have set the <see cref="P:System.Diagnostics.Process.PriorityClass" /> to <see langword="AboveNormal" /> or <see langword="BelowNormal" /> when using Windows 98 or Windows Millennium Edition (Windows Me). These platforms do not support those values for the priority class.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Priority class cannot be set because it does not use a valid value, as defined in the <see cref="T:System.Diagnostics.ProcessPriorityClass" /> enumeration.</exception>
		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x0004C004 File Offset: 0x0004A204
		// (set) Token: 0x06001152 RID: 4434 RVA: 0x0004C068 File Offset: 0x0004A268
		[MonitoringDescription("The priority that the threads in the process run relative to.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ProcessPriorityClass PriorityClass
		{
			get
			{
				if (!this.havePriorityClass)
				{
					SafeProcessHandle handle = null;
					try
					{
						handle = this.GetProcessHandle(1024);
						int num = NativeMethods.GetPriorityClass(handle);
						if (num == 0)
						{
							throw new Win32Exception();
						}
						this.priorityClass = (ProcessPriorityClass)num;
						this.havePriorityClass = true;
					}
					finally
					{
						this.ReleaseProcessHandle(handle);
					}
				}
				return this.priorityClass;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ProcessPriorityClass), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ProcessPriorityClass));
				}
				SafeProcessHandle handle = null;
				try
				{
					handle = this.GetProcessHandle(512);
					if (!NativeMethods.SetPriorityClass(handle, (int)value))
					{
						throw new Win32Exception();
					}
					this.priorityClass = value;
					this.havePriorityClass = true;
				}
				finally
				{
					this.ReleaseProcessHandle(handle);
				}
			}
		}

		/// <summary>Gets the privileged processor time for this process.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that indicates the amount of time that the process has spent running code inside the operating system core.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.PrivilegedProcessorTime" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x0004C0E8 File Offset: 0x0004A2E8
		[MonitoringDescription("The amount of CPU time the process spent inside the operating system core.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TimeSpan PrivilegedProcessorTime
		{
			get
			{
				this.EnsureState(Process.State.IsNt);
				return this.GetProcessTimes().PrivilegedProcessorTime;
			}
		}

		/// <summary>Gets or sets the properties to pass to the <see cref="M:System.Diagnostics.Process.Start" /> method of the <see cref="T:System.Diagnostics.Process" />.</summary>
		/// <returns>The <see cref="T:System.Diagnostics.ProcessStartInfo" /> that represents the data with which to start the process. These arguments include the name of the executable file or document used to start the process.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value that specifies the <see cref="P:System.Diagnostics.Process.StartInfo" /> is <see langword="null" />.</exception>
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x0004C0FC File Offset: 0x0004A2FC
		// (set) Token: 0x06001155 RID: 4437 RVA: 0x0004C118 File Offset: 0x0004A318
		[MonitoringDescription("Specifies information used to start a process.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		public ProcessStartInfo StartInfo
		{
			get
			{
				if (this.startInfo == null)
				{
					this.startInfo = new ProcessStartInfo(this);
				}
				return this.startInfo;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.startInfo = value;
			}
		}

		/// <summary>Gets the time that the associated process was started.</summary>
		/// <returns>An object  that indicates when the process started. An exception is thrown if the process is not running.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.StartTime" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process has exited.  
		///  -or-  
		///  The process has not been started.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred in the call to the Windows function.</exception>
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x0004C12F File Offset: 0x0004A32F
		[MonitoringDescription("The time at which the process was started.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DateTime StartTime
		{
			get
			{
				this.EnsureState(Process.State.IsNt);
				return this.GetProcessTimes().StartTime;
			}
		}

		/// <summary>Gets or sets the object used to marshal the event handler calls that are issued as a result of a process exit event.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISynchronizeInvoke" /> used to marshal event handler calls that are issued as a result of an <see cref="E:System.Diagnostics.Process.Exited" /> event on the process.</returns>
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x0004C144 File Offset: 0x0004A344
		// (set) Token: 0x06001158 RID: 4440 RVA: 0x0004C19E File Offset: 0x0004A39E
		[DefaultValue(null)]
		[Browsable(false)]
		[MonitoringDescription("The object used to marshal the event handler calls issued as a result of a Process exit.")]
		public ISynchronizeInvoke SynchronizingObject
		{
			get
			{
				if (this.synchronizingObject == null && base.DesignMode)
				{
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					if (designerHost != null)
					{
						object rootComponent = designerHost.RootComponent;
						if (rootComponent != null && rootComponent is ISynchronizeInvoke)
						{
							this.synchronizingObject = (ISynchronizeInvoke)rootComponent;
						}
					}
				}
				return this.synchronizingObject;
			}
			set
			{
				this.synchronizingObject = value;
			}
		}

		/// <summary>Gets the total processor time for this process.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that indicates the amount of time that the associated process has spent utilizing the CPU. This value is the sum of the <see cref="P:System.Diagnostics.Process.UserProcessorTime" /> and the <see cref="P:System.Diagnostics.Process.PrivilegedProcessorTime" />.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.TotalProcessorTime" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x0004C1A7 File Offset: 0x0004A3A7
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The amount of CPU time the process has used.")]
		public TimeSpan TotalProcessorTime
		{
			get
			{
				this.EnsureState(Process.State.IsNt);
				return this.GetProcessTimes().TotalProcessorTime;
			}
		}

		/// <summary>Gets the user processor time for this process.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that indicates the amount of time that the associated process has spent running code inside the application portion of the process (not inside the operating system core).</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.UserProcessorTime" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x0004C1BB File Offset: 0x0004A3BB
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The amount of CPU time the process spent outside the operating system core.")]
		public TimeSpan UserProcessorTime
		{
			get
			{
				this.EnsureState(Process.State.IsNt);
				return this.GetProcessTimes().UserProcessorTime;
			}
		}

		/// <summary>Gets or sets whether the <see cref="E:System.Diagnostics.Process.Exited" /> event should be raised when the process terminates.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="E:System.Diagnostics.Process.Exited" /> event should be raised when the associated process is terminated (through either an exit or a call to <see cref="M:System.Diagnostics.Process.Kill" />); otherwise, <see langword="false" />. The default is <see langword="false" />. Note that the <see cref="E:System.Diagnostics.Process.Exited" /> event is raised even if the value of <see cref="P:System.Diagnostics.Process.EnableRaisingEvents" /> is <see langword="false" /> when the process exits during or before the user performs a <see cref="P:System.Diagnostics.Process.HasExited" /> check.</returns>
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x0004C1CF File Offset: 0x0004A3CF
		// (set) Token: 0x0600115C RID: 4444 RVA: 0x0004C1D7 File Offset: 0x0004A3D7
		[MonitoringDescription("Whether the process component should watch for the associated process to exit, and raise the Exited event.")]
		[Browsable(false)]
		[DefaultValue(false)]
		public bool EnableRaisingEvents
		{
			get
			{
				return this.watchForExit;
			}
			set
			{
				if (value != this.watchForExit)
				{
					if (this.Associated)
					{
						if (value)
						{
							this.OpenProcessHandle();
							this.EnsureWatchingForExit();
						}
						else
						{
							this.StopWatchingForExit();
						}
					}
					this.watchForExit = value;
				}
			}
		}

		/// <summary>Gets a stream used to write the input of the application.</summary>
		/// <returns>A <see cref="T:System.IO.StreamWriter" /> that can be used to write the standard input stream of the application.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.StandardInput" /> stream has not been defined because <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardInput" /> is set to <see langword="false" />.</exception>
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x0004C209 File Offset: 0x0004A409
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[MonitoringDescription("Standard input stream of the process.")]
		public StreamWriter StandardInput
		{
			get
			{
				if (this.standardInput == null)
				{
					throw new InvalidOperationException(SR.GetString("StandardIn has not been redirected."));
				}
				this.inputStreamReadMode = Process.StreamReadMode.syncMode;
				return this.standardInput;
			}
		}

		/// <summary>Gets a stream used to read the textual output of the application.</summary>
		/// <returns>A <see cref="T:System.IO.StreamReader" /> that can be used to read the standard output stream of the application.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream has not been defined for redirection; ensure <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardOutput" /> is set to <see langword="true" /> and <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> is set to <see langword="false" />.  
		/// -or-
		///  The <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream has been opened for asynchronous read operations with <see cref="M:System.Diagnostics.Process.BeginOutputReadLine" />.</exception>
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x0004C230 File Offset: 0x0004A430
		[MonitoringDescription("Standard output stream of the process.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public StreamReader StandardOutput
		{
			get
			{
				if (this.standardOutput == null)
				{
					throw new InvalidOperationException(SR.GetString("StandardOut has not been redirected or the process hasn't started yet."));
				}
				if (this.outputStreamReadMode == Process.StreamReadMode.undefined)
				{
					this.outputStreamReadMode = Process.StreamReadMode.syncMode;
				}
				else if (this.outputStreamReadMode != Process.StreamReadMode.syncMode)
				{
					throw new InvalidOperationException(SR.GetString("Cannot mix synchronous and asynchronous operation on process stream."));
				}
				return this.standardOutput;
			}
		}

		/// <summary>Gets a stream used to read the error output of the application.</summary>
		/// <returns>A <see cref="T:System.IO.StreamReader" /> that can be used to read the standard error stream of the application.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.StandardError" /> stream has not been defined for redirection; ensure <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardError" /> is set to <see langword="true" /> and <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> is set to <see langword="false" />.  
		/// -or-
		///  The <see cref="P:System.Diagnostics.Process.StandardError" /> stream has been opened for asynchronous read operations with <see cref="M:System.Diagnostics.Process.BeginErrorReadLine" />.</exception>
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x0004C288 File Offset: 0x0004A488
		[MonitoringDescription("Standard error stream of the process.")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public StreamReader StandardError
		{
			get
			{
				if (this.standardError == null)
				{
					throw new InvalidOperationException(SR.GetString("StandardError has not been redirected."));
				}
				if (this.errorStreamReadMode == Process.StreamReadMode.undefined)
				{
					this.errorStreamReadMode = Process.StreamReadMode.syncMode;
				}
				else if (this.errorStreamReadMode != Process.StreamReadMode.syncMode)
				{
					throw new InvalidOperationException(SR.GetString("Cannot mix synchronous and asynchronous operation on process stream."));
				}
				return this.standardError;
			}
		}

		/// <summary>Occurs when a process exits.</summary>
		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06001160 RID: 4448 RVA: 0x0004C2DD File Offset: 0x0004A4DD
		// (remove) Token: 0x06001161 RID: 4449 RVA: 0x0004C2F6 File Offset: 0x0004A4F6
		[MonitoringDescription("If the WatchForExit property is set to true, then this event is raised when the associated process exits.")]
		[Category("Behavior")]
		public event EventHandler Exited
		{
			add
			{
				this.onExited = (EventHandler)Delegate.Combine(this.onExited, value);
			}
			remove
			{
				this.onExited = (EventHandler)Delegate.Remove(this.onExited, value);
			}
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x0004C30F File Offset: 0x0004A50F
		private void ReleaseProcessHandle(SafeProcessHandle handle)
		{
			if (handle == null)
			{
				return;
			}
			if (this.haveProcessHandle && handle == this.m_processHandle)
			{
				return;
			}
			handle.Close();
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x0004C32D File Offset: 0x0004A52D
		private void CompletionCallback(object context, bool wasSignaled)
		{
			this.StopWatchingForExit();
			this.RaiseOnExited();
		}

		/// <summary>Release all resources used by this process.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06001164 RID: 4452 RVA: 0x0004C33B File Offset: 0x0004A53B
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					this.Close();
				}
				this.disposed = true;
				base.Dispose(disposing);
			}
		}

		/// <summary>Frees all the resources that are associated with this component.</summary>
		// Token: 0x06001165 RID: 4453 RVA: 0x0004C35C File Offset: 0x0004A55C
		public void Close()
		{
			if (this.Associated)
			{
				if (this.haveProcessHandle)
				{
					this.StopWatchingForExit();
					this.m_processHandle.Close();
					this.m_processHandle = null;
					this.haveProcessHandle = false;
				}
				this.haveProcessId = false;
				this.isRemoteMachine = false;
				this.machineName = ".";
				this.raisedOnExited = false;
				StreamWriter streamWriter = this.standardInput;
				this.standardInput = null;
				if (this.inputStreamReadMode == Process.StreamReadMode.undefined && streamWriter != null)
				{
					streamWriter.Close();
				}
				StreamReader streamReader = this.standardOutput;
				this.standardOutput = null;
				if (this.outputStreamReadMode == Process.StreamReadMode.undefined && streamReader != null)
				{
					streamReader.Close();
				}
				streamReader = this.standardError;
				this.standardError = null;
				if (this.errorStreamReadMode == Process.StreamReadMode.undefined && streamReader != null)
				{
					streamReader.Close();
				}
				AsyncStreamReader asyncStreamReader = this.output;
				this.output = null;
				if (this.outputStreamReadMode == Process.StreamReadMode.asyncMode && asyncStreamReader != null)
				{
					asyncStreamReader.CancelOperation();
					asyncStreamReader.Close();
				}
				asyncStreamReader = this.error;
				this.error = null;
				if (this.errorStreamReadMode == Process.StreamReadMode.asyncMode && asyncStreamReader != null)
				{
					asyncStreamReader.CancelOperation();
					asyncStreamReader.Close();
				}
				this.Refresh();
			}
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x0004C46C File Offset: 0x0004A66C
		private void EnsureState(Process.State state)
		{
			if ((state & Process.State.Associated) != (Process.State)0 && !this.Associated)
			{
				throw new InvalidOperationException(SR.GetString("No process is associated with this object."));
			}
			if ((state & Process.State.HaveId) != (Process.State)0 && !this.haveProcessId)
			{
				this.EnsureState(Process.State.Associated);
				throw new InvalidOperationException(SR.GetString("Feature requires a process identifier."));
			}
			if ((state & Process.State.IsLocal) != (Process.State)0 && this.isRemoteMachine)
			{
				throw new NotSupportedException(SR.GetString("Feature is not supported for remote machines."));
			}
			if ((state & Process.State.HaveProcessInfo) != (Process.State)0)
			{
				throw new InvalidOperationException(SR.GetString("Process has exited, so the requested information is not available."));
			}
			if ((state & Process.State.Exited) != (Process.State)0)
			{
				if (!this.HasExited)
				{
					throw new InvalidOperationException(SR.GetString("Process must exit before requested information can be determined."));
				}
				if (!this.haveProcessHandle)
				{
					throw new InvalidOperationException(SR.GetString("Process was not started by this object, so requested information cannot be determined."));
				}
			}
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x0004C524 File Offset: 0x0004A724
		private void EnsureWatchingForExit()
		{
			if (!this.watchingForExit)
			{
				lock (this)
				{
					if (!this.watchingForExit)
					{
						this.watchingForExit = true;
						try
						{
							this.waitHandle = new ProcessWaitHandle(this.m_processHandle);
							this.registeredWaitHandle = ThreadPool.RegisterWaitForSingleObject(this.waitHandle, new WaitOrTimerCallback(this.CompletionCallback), null, -1, true);
						}
						catch
						{
							this.watchingForExit = false;
							throw;
						}
					}
				}
			}
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x0004C5BC File Offset: 0x0004A7BC
		private void EnsureWorkingSetLimits()
		{
			this.EnsureState(Process.State.IsNt);
			if (!this.haveWorkingSetLimits)
			{
				SafeProcessHandle handle = null;
				try
				{
					handle = this.GetProcessHandle(1024);
					IntPtr intPtr;
					IntPtr intPtr2;
					if (!NativeMethods.GetProcessWorkingSetSize(handle, out intPtr, out intPtr2))
					{
						throw new Win32Exception();
					}
					this.minWorkingSet = intPtr;
					this.maxWorkingSet = intPtr2;
					this.haveWorkingSetLimits = true;
				}
				finally
				{
					this.ReleaseProcessHandle(handle);
				}
			}
		}

		/// <summary>Puts a <see cref="T:System.Diagnostics.Process" /> component in state to interact with operating system processes that run in a special mode by enabling the native property <see langword="SeDebugPrivilege" /> on the current thread.</summary>
		// Token: 0x06001169 RID: 4457 RVA: 0x00003917 File Offset: 0x00001B17
		public static void EnterDebugMode()
		{
		}

		/// <summary>Takes a <see cref="T:System.Diagnostics.Process" /> component out of the state that lets it interact with operating system processes that run in a special mode.</summary>
		// Token: 0x0600116A RID: 4458 RVA: 0x00003917 File Offset: 0x00001B17
		public static void LeaveDebugMode()
		{
		}

		/// <summary>Returns a new <see cref="T:System.Diagnostics.Process" /> component, given the identifier of a process on the local computer.</summary>
		/// <param name="processId">The system-unique identifier of a process resource.</param>
		/// <returns>A <see cref="T:System.Diagnostics.Process" /> component that is associated with the local process resource identified by the <paramref name="processId" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">The process specified by the <paramref name="processId" /> parameter is not running. The identifier might be expired.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process was not started by this object.</exception>
		// Token: 0x0600116B RID: 4459 RVA: 0x0004C628 File Offset: 0x0004A828
		public static Process GetProcessById(int processId)
		{
			return Process.GetProcessById(processId, ".");
		}

		/// <summary>Creates an array of new <see cref="T:System.Diagnostics.Process" /> components and associates them with all the process resources on the local computer that share the specified process name.</summary>
		/// <param name="processName">The friendly name of the process.</param>
		/// <returns>An array of type <see cref="T:System.Diagnostics.Process" /> that represents the process resources running the specified application or file.</returns>
		/// <exception cref="T:System.InvalidOperationException">There are problems accessing the performance counter API's used to get process information. This exception is specific to Windows NT, Windows 2000, and Windows XP.</exception>
		// Token: 0x0600116C RID: 4460 RVA: 0x0004C635 File Offset: 0x0004A835
		public static Process[] GetProcessesByName(string processName)
		{
			return Process.GetProcessesByName(processName, ".");
		}

		/// <summary>Creates a new <see cref="T:System.Diagnostics.Process" /> component for each process resource on the local computer.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.Process" /> that represents all the process resources running on the local computer.</returns>
		// Token: 0x0600116D RID: 4461 RVA: 0x0004C642 File Offset: 0x0004A842
		public static Process[] GetProcesses()
		{
			return Process.GetProcesses(".");
		}

		/// <summary>Gets a new <see cref="T:System.Diagnostics.Process" /> component and associates it with the currently active process.</summary>
		/// <returns>A new <see cref="T:System.Diagnostics.Process" /> component associated with the process resource that is running the calling application.</returns>
		// Token: 0x0600116E RID: 4462 RVA: 0x0004C64E File Offset: 0x0004A84E
		public static Process GetCurrentProcess()
		{
			return new Process(".", false, NativeMethods.GetCurrentProcessId(), null);
		}

		/// <summary>Raises the <see cref="E:System.Diagnostics.Process.Exited" /> event.</summary>
		// Token: 0x0600116F RID: 4463 RVA: 0x0004C664 File Offset: 0x0004A864
		protected void OnExited()
		{
			EventHandler eventHandler = this.onExited;
			if (eventHandler != null)
			{
				if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
				{
					this.SynchronizingObject.BeginInvoke(eventHandler, new object[]
					{
						this,
						EventArgs.Empty
					});
					return;
				}
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x0004C6BC File Offset: 0x0004A8BC
		private SafeProcessHandle GetProcessHandle(int access, bool throwIfExited)
		{
			if (this.haveProcessHandle)
			{
				if (throwIfExited)
				{
					ProcessWaitHandle processWaitHandle = null;
					try
					{
						processWaitHandle = new ProcessWaitHandle(this.m_processHandle);
						if (processWaitHandle.WaitOne(0, false))
						{
							if (this.haveProcessId)
							{
								throw new InvalidOperationException(SR.GetString("Cannot process request because the process ({0}) has exited.", new object[]
								{
									this.processId.ToString(CultureInfo.CurrentCulture)
								}));
							}
							throw new InvalidOperationException(SR.GetString("Cannot process request because the process has exited."));
						}
					}
					finally
					{
						if (processWaitHandle != null)
						{
							processWaitHandle.Close();
						}
					}
				}
				return this.m_processHandle;
			}
			this.EnsureState((Process.State)3);
			SafeProcessHandle invalidHandle = SafeProcessHandle.InvalidHandle;
			IntPtr currentProcess = NativeMethods.GetCurrentProcess();
			if (!NativeMethods.DuplicateHandle(new HandleRef(this, currentProcess), new HandleRef(this, currentProcess), new HandleRef(this, currentProcess), out invalidHandle, 0, false, 3))
			{
				throw new Win32Exception();
			}
			if (throwIfExited && (access & 1024) != 0 && NativeMethods.GetExitCodeProcess(invalidHandle, out this.exitCode) && this.exitCode != 259)
			{
				throw new InvalidOperationException(SR.GetString("Cannot process request because the process ({0}) has exited.", new object[]
				{
					this.processId.ToString(CultureInfo.CurrentCulture)
				}));
			}
			return invalidHandle;
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0004C7DC File Offset: 0x0004A9DC
		private SafeProcessHandle GetProcessHandle(int access)
		{
			return this.GetProcessHandle(access, true);
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x0004C7E6 File Offset: 0x0004A9E6
		private SafeProcessHandle OpenProcessHandle()
		{
			return this.OpenProcessHandle(2035711);
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x0004C7F3 File Offset: 0x0004A9F3
		private SafeProcessHandle OpenProcessHandle(int access)
		{
			if (!this.haveProcessHandle)
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				this.SetProcessHandle(this.GetProcessHandle(access));
			}
			return this.m_processHandle;
		}

		/// <summary>Discards any information about the associated process that has been cached inside the process component.</summary>
		// Token: 0x06001174 RID: 4468 RVA: 0x0004C829 File Offset: 0x0004AA29
		public void Refresh()
		{
			this.threads = null;
			this.modules = null;
			this.exited = false;
			this.signaled = false;
			this.haveWorkingSetLimits = false;
			this.havePriorityClass = false;
			this.haveExitTime = false;
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x0004C85C File Offset: 0x0004AA5C
		private void SetProcessHandle(SafeProcessHandle processHandle)
		{
			this.m_processHandle = processHandle;
			this.haveProcessHandle = true;
			if (this.watchForExit)
			{
				this.EnsureWatchingForExit();
			}
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x0004C87A File Offset: 0x0004AA7A
		private void SetProcessId(int processId)
		{
			this.processId = processId;
			this.haveProcessId = true;
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x0004C88C File Offset: 0x0004AA8C
		private void SetWorkingSetLimits(object newMin, object newMax)
		{
			this.EnsureState(Process.State.IsNt);
			SafeProcessHandle handle = null;
			try
			{
				handle = this.GetProcessHandle(1280);
				IntPtr intPtr;
				IntPtr intPtr2;
				if (!NativeMethods.GetProcessWorkingSetSize(handle, out intPtr, out intPtr2))
				{
					throw new Win32Exception();
				}
				if (newMin != null)
				{
					intPtr = (IntPtr)newMin;
				}
				if (newMax != null)
				{
					intPtr2 = (IntPtr)newMax;
				}
				if ((long)intPtr > (long)intPtr2)
				{
					if (newMin != null)
					{
						throw new ArgumentException(SR.GetString("Minimum working set size is invalid. It must be less than or equal to the maximum working set size."));
					}
					throw new ArgumentException(SR.GetString("Maximum working set size is invalid. It must be greater than or equal to the minimum working set size."));
				}
				else
				{
					if (!NativeMethods.SetProcessWorkingSetSize(handle, intPtr, intPtr2))
					{
						throw new Win32Exception();
					}
					if (!NativeMethods.GetProcessWorkingSetSize(handle, out intPtr, out intPtr2))
					{
						throw new Win32Exception();
					}
					this.minWorkingSet = intPtr;
					this.maxWorkingSet = intPtr2;
					this.haveWorkingSetLimits = true;
				}
			}
			finally
			{
				this.ReleaseProcessHandle(handle);
			}
		}

		/// <summary>Starts (or reuses) the process resource that is specified by the <see cref="P:System.Diagnostics.Process.StartInfo" /> property of this <see cref="T:System.Diagnostics.Process" /> component and associates it with the component.</summary>
		/// <returns>
		///   <see langword="true" /> if a process resource is started; <see langword="false" /> if no new process resource is started (for example, if an existing process is reused).</returns>
		/// <exception cref="T:System.InvalidOperationException">No file name was specified in the <see cref="T:System.Diagnostics.Process" /> component's <see cref="P:System.Diagnostics.Process.StartInfo" />.
		///  -or-
		///  The <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> member of the <see cref="P:System.Diagnostics.Process.StartInfo" /> property is <see langword="true" /> while <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardInput" />, <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardOutput" />, or <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardError" /> is <see langword="true" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">There was an error in opening the associated file.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The process object has already been disposed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">Method not supported on operating systems without shell support such as Nano Server (.NET Core only).</exception>
		// Token: 0x06001178 RID: 4472 RVA: 0x0004C958 File Offset: 0x0004AB58
		public bool Start()
		{
			this.Close();
			ProcessStartInfo processStartInfo = this.StartInfo;
			if (processStartInfo.FileName.Length == 0)
			{
				throw new InvalidOperationException(SR.GetString("Cannot start process because a file name has not been provided."));
			}
			if (processStartInfo.UseShellExecute)
			{
				return this.StartWithShellExecuteEx(processStartInfo);
			}
			return this.StartWithCreateProcess(processStartInfo);
		}

		/// <summary>Starts a process resource by specifying the name of an application, a user name, a password, and a domain and associates the resource with a new <see cref="T:System.Diagnostics.Process" /> component.</summary>
		/// <param name="fileName">The name of an application file to run in the process.</param>
		/// <param name="userName">The user name to use when starting the process.</param>
		/// <param name="password">A <see cref="T:System.Security.SecureString" /> that contains the password to use when starting the process.</param>
		/// <param name="domain">The domain to use when starting the process.</param>
		/// <returns>A new <see cref="T:System.Diagnostics.Process" /> that is associated with the process resource, or <see langword="null" /> if no process resource is started. Note that a new process that's started alongside already running instances of the same process will be independent from the others. In addition, Start may return a non-null Process with its <see cref="P:System.Diagnostics.Process.HasExited" /> property already set to <see langword="true" />. In this case, the started process may have activated an existing instance of itself and then exited.</returns>
		/// <exception cref="T:System.InvalidOperationException">No file name was specified.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">There was an error in opening the associated file.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The process object has already been disposed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">Method not supported on Linux or macOS (.NET Core only).</exception>
		// Token: 0x06001179 RID: 4473 RVA: 0x0004C9A6 File Offset: 0x0004ABA6
		public static Process Start(string fileName, string userName, SecureString password, string domain)
		{
			return Process.Start(new ProcessStartInfo(fileName)
			{
				UserName = userName,
				Password = password,
				Domain = domain,
				UseShellExecute = false
			});
		}

		/// <summary>Starts a process resource by specifying the name of an application, a set of command-line arguments, a user name, a password, and a domain and associates the resource with a new <see cref="T:System.Diagnostics.Process" /> component.</summary>
		/// <param name="fileName">The name of an application file to run in the process.</param>
		/// <param name="arguments">Command-line arguments to pass when starting the process.</param>
		/// <param name="userName">The user name to use when starting the process.</param>
		/// <param name="password">A <see cref="T:System.Security.SecureString" /> that contains the password to use when starting the process.</param>
		/// <param name="domain">The domain to use when starting the process.</param>
		/// <returns>A new <see cref="T:System.Diagnostics.Process" /> that is associated with the process resource, or <see langword="null" /> if no process resource is started. Note that a new process that's started alongside already running instances of the same process will be independent from the others. In addition, Start may return a non-null Process with its <see cref="P:System.Diagnostics.Process.HasExited" /> property already set to <see langword="true" />. In this case, the started process may have activated an existing instance of itself and then exited.</returns>
		/// <exception cref="T:System.InvalidOperationException">No file name was specified.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when opening the associated file.  
		///  -or-  
		///  The sum of the length of the arguments and the length of the full path to the associated file exceeds 2080. The error message associated with this exception can be one of the following: "The data area passed to a system call is too small." or "Access is denied."</exception>
		/// <exception cref="T:System.ObjectDisposedException">The process object has already been disposed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">Method not supported on Linux or macOS (.NET Core only).</exception>
		// Token: 0x0600117A RID: 4474 RVA: 0x0004C9CF File Offset: 0x0004ABCF
		public static Process Start(string fileName, string arguments, string userName, SecureString password, string domain)
		{
			return Process.Start(new ProcessStartInfo(fileName, arguments)
			{
				UserName = userName,
				Password = password,
				Domain = domain,
				UseShellExecute = false
			});
		}

		/// <summary>Starts a process resource by specifying the name of a document or application file and associates the resource with a new <see cref="T:System.Diagnostics.Process" /> component.</summary>
		/// <param name="fileName">The name of a document or application file to run in the process.</param>
		/// <returns>A new <see cref="T:System.Diagnostics.Process" /> that is associated with the process resource, or <see langword="null" /> if no process resource is started. Note that a new process that's started alongside already running instances of the same process will be independent from the others. In addition, Start may return a non-null Process with its <see cref="P:System.Diagnostics.Process.HasExited" /> property already set to <see langword="true" />. In this case, the started process may have activated an existing instance of itself and then exited.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when opening the associated file.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The process object has already been disposed.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The PATH environment variable has a string containing quotes.</exception>
		// Token: 0x0600117B RID: 4475 RVA: 0x0004C9FA File Offset: 0x0004ABFA
		public static Process Start(string fileName)
		{
			return Process.Start(new ProcessStartInfo(fileName));
		}

		/// <summary>Starts a process resource by specifying the name of an application and a set of command-line arguments, and associates the resource with a new <see cref="T:System.Diagnostics.Process" /> component.</summary>
		/// <param name="fileName">The name of an application file to run in the process.</param>
		/// <param name="arguments">Command-line arguments to pass when starting the process.</param>
		/// <returns>A new <see cref="T:System.Diagnostics.Process" /> that is associated with the process resource, or <see langword="null" /> if no process resource is started. Note that a new process that's started alongside already running instances of the same process will be independent from the others. In addition, Start may return a non-null Process with its <see cref="P:System.Diagnostics.Process.HasExited" /> property already set to <see langword="true" />. In this case, the started process may have activated an existing instance of itself and then exited.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="fileName" /> or <paramref name="arguments" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when opening the associated file.  
		///  -or-  
		///  The sum of the length of the arguments and the length of the full path to the process exceeds 2080. The error message associated with this exception can be one of the following: "The data area passed to a system call is too small." or "Access is denied."</exception>
		/// <exception cref="T:System.ObjectDisposedException">The process object has already been disposed.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The PATH environment variable has a string containing quotes.</exception>
		// Token: 0x0600117C RID: 4476 RVA: 0x0004CA07 File Offset: 0x0004AC07
		public static Process Start(string fileName, string arguments)
		{
			return Process.Start(new ProcessStartInfo(fileName, arguments));
		}

		/// <summary>Starts the process resource that is specified by the parameter containing process start information (for example, the file name of the process to start) and associates the resource with a new <see cref="T:System.Diagnostics.Process" /> component.</summary>
		/// <param name="startInfo">The <see cref="T:System.Diagnostics.ProcessStartInfo" /> that contains the information that is used to start the process, including the file name and any command-line arguments.</param>
		/// <returns>A new <see cref="T:System.Diagnostics.Process" /> that is associated with the process resource, or <see langword="null" /> if no process resource is started. Note that a new process that's started alongside already running instances of the same process will be independent from the others. In addition, Start may return a non-null Process with its <see cref="P:System.Diagnostics.Process.HasExited" /> property already set to <see langword="true" />. In this case, the started process may have activated an existing instance of itself and then exited.</returns>
		/// <exception cref="T:System.InvalidOperationException">No file name was specified in the <paramref name="startInfo" /> parameter's <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property.  
		///  -or-  
		///  The <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property of the <paramref name="startInfo" /> parameter is <see langword="true" /> and the <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardInput" />, <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardOutput" />, or <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardError" /> property is also <see langword="true" />.  
		///  -or-  
		///  The <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property of the <paramref name="startInfo" /> parameter is <see langword="true" /> and the <see cref="P:System.Diagnostics.ProcessStartInfo.UserName" /> property is not <see langword="null" /> or empty or the <see cref="P:System.Diagnostics.ProcessStartInfo.Password" /> property is not <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="startInfo" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The process object has already been disposed.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in the <paramref name="startInfo" /> parameter's <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property could not be found.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when opening the associated file.  
		///  -or-  
		///  The sum of the length of the arguments and the length of the full path to the process exceeds 2080. The error message associated with this exception can be one of the following: "The data area passed to a system call is too small." or "Access is denied."</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">Method not supported on operating systems without shell support such as Nano Server (.NET Core only).</exception>
		// Token: 0x0600117D RID: 4477 RVA: 0x0004CA18 File Offset: 0x0004AC18
		public static Process Start(ProcessStartInfo startInfo)
		{
			Process process = new Process();
			if (startInfo == null)
			{
				throw new ArgumentNullException("startInfo");
			}
			process.StartInfo = startInfo;
			if (process.Start())
			{
				return process;
			}
			return null;
		}

		/// <summary>Immediately stops the associated process.</summary>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The associated process could not be terminated.  
		///  -or-  
		///  The process is terminating.  
		///  -or-  
		///  The associated process is a Win16 executable.</exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to call <see cref="M:System.Diagnostics.Process.Kill" /> for a process that is running on a remote computer. The method is available only for processes running on the local computer.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process has already exited.  
		///  -or-  
		///  There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.</exception>
		// Token: 0x0600117E RID: 4478 RVA: 0x0004CA4C File Offset: 0x0004AC4C
		public void Kill()
		{
			SafeProcessHandle safeProcessHandle = null;
			try
			{
				safeProcessHandle = this.GetProcessHandle(1);
				if (!NativeMethods.TerminateProcess(safeProcessHandle, -1))
				{
					throw new Win32Exception();
				}
			}
			finally
			{
				this.ReleaseProcessHandle(safeProcessHandle);
			}
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x0004CA8C File Offset: 0x0004AC8C
		private void StopWatchingForExit()
		{
			if (this.watchingForExit)
			{
				lock (this)
				{
					if (this.watchingForExit)
					{
						this.watchingForExit = false;
						this.registeredWaitHandle.Unregister(null);
						this.waitHandle.Close();
						this.waitHandle = null;
						this.registeredWaitHandle = null;
					}
				}
			}
		}

		/// <summary>Formats the process's name as a string, combined with the parent component type, if applicable.</summary>
		/// <returns>The <see cref="P:System.Diagnostics.Process.ProcessName" />, combined with the base component's <see cref="M:System.Object.ToString" /> return value.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">
		///   <see cref="M:System.Diagnostics.Process.ToString" /> is not supported on Windows 98.</exception>
		// Token: 0x06001180 RID: 4480 RVA: 0x0004CB00 File Offset: 0x0004AD00
		public override string ToString()
		{
			if (!this.Associated)
			{
				return base.ToString();
			}
			string text = string.Empty;
			try
			{
				text = this.ProcessName;
			}
			catch (PlatformNotSupportedException)
			{
			}
			if (text.Length != 0)
			{
				return string.Format(CultureInfo.CurrentCulture, "{0} ({1})", base.ToString(), text);
			}
			return base.ToString();
		}

		/// <summary>Instructs the <see cref="T:System.Diagnostics.Process" /> component to wait the specified number of milliseconds for the associated process to exit.</summary>
		/// <param name="milliseconds">The amount of time, in milliseconds, to wait for the associated process to exit. The maximum is the largest possible value of a 32-bit integer, which represents infinity to the operating system.</param>
		/// <returns>
		///   <see langword="true" /> if the associated process has exited; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The wait setting could not be accessed.</exception>
		/// <exception cref="T:System.SystemException">No process <see cref="P:System.Diagnostics.Process.Id" /> has been set, and a <see cref="P:System.Diagnostics.Process.Handle" /> from which the <see cref="P:System.Diagnostics.Process.Id" /> property can be determined does not exist.  
		///  -or-  
		///  There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.  
		///  -or-  
		///  You are attempting to call <see cref="M:System.Diagnostics.Process.WaitForExit(System.Int32)" /> for a process that is running on a remote computer. This method is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="milliseconds" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		// Token: 0x06001181 RID: 4481 RVA: 0x0004CB64 File Offset: 0x0004AD64
		public bool WaitForExit(int milliseconds)
		{
			SafeProcessHandle safeProcessHandle = null;
			ProcessWaitHandle processWaitHandle = null;
			bool flag;
			try
			{
				safeProcessHandle = this.GetProcessHandle(1048576, false);
				if (safeProcessHandle.IsInvalid)
				{
					flag = true;
				}
				else
				{
					processWaitHandle = new ProcessWaitHandle(safeProcessHandle);
					if (processWaitHandle.WaitOne(milliseconds, false))
					{
						flag = true;
						this.signaled = true;
					}
					else
					{
						flag = false;
						this.signaled = false;
					}
				}
				if (this.output != null && milliseconds == -1)
				{
					this.output.WaitUtilEOF();
				}
				if (this.error != null && milliseconds == -1)
				{
					this.error.WaitUtilEOF();
				}
			}
			finally
			{
				if (processWaitHandle != null)
				{
					processWaitHandle.Close();
				}
				this.ReleaseProcessHandle(safeProcessHandle);
			}
			if (flag && this.watchForExit)
			{
				this.RaiseOnExited();
			}
			return flag;
		}

		/// <summary>Instructs the <see cref="T:System.Diagnostics.Process" /> component to wait indefinitely for the associated process to exit.</summary>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The wait setting could not be accessed.</exception>
		/// <exception cref="T:System.SystemException">No process <see cref="P:System.Diagnostics.Process.Id" /> has been set, and a <see cref="P:System.Diagnostics.Process.Handle" /> from which the <see cref="P:System.Diagnostics.Process.Id" /> property can be determined does not exist.  
		///  -or-  
		///  There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.  
		///  -or-  
		///  You are attempting to call <see cref="M:System.Diagnostics.Process.WaitForExit" /> for a process that is running on a remote computer. This method is available only for processes that are running on the local computer.</exception>
		// Token: 0x06001182 RID: 4482 RVA: 0x0004CC18 File Offset: 0x0004AE18
		public void WaitForExit()
		{
			this.WaitForExit(-1);
		}

		/// <summary>Causes the <see cref="T:System.Diagnostics.Process" /> component to wait the specified number of milliseconds for the associated process to enter an idle state. This overload applies only to processes with a user interface and, therefore, a message loop.</summary>
		/// <param name="milliseconds">A value of 1 to <see cref="F:System.Int32.MaxValue" /> that specifies the amount of time, in milliseconds, to wait for the associated process to become idle. A value of 0 specifies an immediate return, and a value of -1 specifies an infinite wait.</param>
		/// <returns>
		///   <see langword="true" /> if the associated process has reached an idle state; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The process does not have a graphical interface.  
		///  -or-  
		///  An unknown error occurred. The process failed to enter an idle state.  
		///  -or-  
		///  The process has already exited.  
		///  -or-  
		///  No process is associated with this <see cref="T:System.Diagnostics.Process" /> object.</exception>
		// Token: 0x06001183 RID: 4483 RVA: 0x0004CC24 File Offset: 0x0004AE24
		public bool WaitForInputIdle(int milliseconds)
		{
			SafeProcessHandle handle = null;
			try
			{
				handle = this.GetProcessHandle(1049600);
				int num = NativeMethods.WaitForInputIdle(handle, milliseconds);
				if (num != -1)
				{
					if (num == 0)
					{
						return true;
					}
					if (num == 258)
					{
						return false;
					}
				}
				throw new InvalidOperationException(SR.GetString("WaitForInputIdle failed.  This could be because the process does not have a graphical interface."));
			}
			finally
			{
				this.ReleaseProcessHandle(handle);
			}
			bool result;
			return result;
		}

		/// <summary>Causes the <see cref="T:System.Diagnostics.Process" /> component to wait indefinitely for the associated process to enter an idle state. This overload applies only to processes with a user interface and, therefore, a message loop.</summary>
		/// <returns>
		///   <see langword="true" /> if the associated process has reached an idle state.</returns>
		/// <exception cref="T:System.InvalidOperationException">The process does not have a graphical interface.  
		///  -or-  
		///  An unknown error occurred. The process failed to enter an idle state.  
		///  -or-  
		///  The process has already exited.  
		///  -or-  
		///  No process is associated with this <see cref="T:System.Diagnostics.Process" /> object.</exception>
		// Token: 0x06001184 RID: 4484 RVA: 0x0004CC8C File Offset: 0x0004AE8C
		public bool WaitForInputIdle()
		{
			return this.WaitForInputIdle(int.MaxValue);
		}

		/// <summary>Begins asynchronous read operations on the redirected <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream of the application.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardOutput" /> property is <see langword="false" />.  
		/// -or-
		///  An asynchronous read operation is already in progress on the <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream.  
		/// -or-
		///  The <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream has been used by a synchronous read operation.</exception>
		// Token: 0x06001185 RID: 4485 RVA: 0x0004CC9C File Offset: 0x0004AE9C
		[ComVisible(false)]
		public void BeginOutputReadLine()
		{
			if (this.outputStreamReadMode == Process.StreamReadMode.undefined)
			{
				this.outputStreamReadMode = Process.StreamReadMode.asyncMode;
			}
			else if (this.outputStreamReadMode != Process.StreamReadMode.asyncMode)
			{
				throw new InvalidOperationException(SR.GetString("Cannot mix synchronous and asynchronous operation on process stream."));
			}
			if (this.pendingOutputRead)
			{
				throw new InvalidOperationException(SR.GetString("An async read operation has already been started on the stream."));
			}
			this.pendingOutputRead = true;
			if (this.output == null)
			{
				if (this.standardOutput == null)
				{
					throw new InvalidOperationException(SR.GetString("StandardOut has not been redirected or the process hasn't started yet."));
				}
				Stream baseStream = this.standardOutput.BaseStream;
				this.output = new AsyncStreamReader(this, baseStream, new UserCallBack(this.OutputReadNotifyUser), this.standardOutput.CurrentEncoding);
			}
			this.output.BeginReadLine();
		}

		/// <summary>Begins asynchronous read operations on the redirected <see cref="P:System.Diagnostics.Process.StandardError" /> stream of the application.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardError" /> property is <see langword="false" />.  
		/// -or-
		///  An asynchronous read operation is already in progress on the <see cref="P:System.Diagnostics.Process.StandardError" /> stream.  
		/// -or-
		///  The <see cref="P:System.Diagnostics.Process.StandardError" /> stream has been used by a synchronous read operation.</exception>
		// Token: 0x06001186 RID: 4486 RVA: 0x0004CD50 File Offset: 0x0004AF50
		[ComVisible(false)]
		public void BeginErrorReadLine()
		{
			if (this.errorStreamReadMode == Process.StreamReadMode.undefined)
			{
				this.errorStreamReadMode = Process.StreamReadMode.asyncMode;
			}
			else if (this.errorStreamReadMode != Process.StreamReadMode.asyncMode)
			{
				throw new InvalidOperationException(SR.GetString("Cannot mix synchronous and asynchronous operation on process stream."));
			}
			if (this.pendingErrorRead)
			{
				throw new InvalidOperationException(SR.GetString("An async read operation has already been started on the stream."));
			}
			this.pendingErrorRead = true;
			if (this.error == null)
			{
				if (this.standardError == null)
				{
					throw new InvalidOperationException(SR.GetString("StandardError has not been redirected."));
				}
				Stream baseStream = this.standardError.BaseStream;
				this.error = new AsyncStreamReader(this, baseStream, new UserCallBack(this.ErrorReadNotifyUser), this.standardError.CurrentEncoding);
			}
			this.error.BeginReadLine();
		}

		/// <summary>Cancels the asynchronous read operation on the redirected <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream of an application.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream is not enabled for asynchronous read operations.</exception>
		// Token: 0x06001187 RID: 4487 RVA: 0x0004CE01 File Offset: 0x0004B001
		[ComVisible(false)]
		public void CancelOutputRead()
		{
			if (this.output != null)
			{
				this.output.CancelOperation();
				this.pendingOutputRead = false;
				return;
			}
			throw new InvalidOperationException(SR.GetString("No async read operation is in progress on the stream."));
		}

		/// <summary>Cancels the asynchronous read operation on the redirected <see cref="P:System.Diagnostics.Process.StandardError" /> stream of an application.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.StandardError" /> stream is not enabled for asynchronous read operations.</exception>
		// Token: 0x06001188 RID: 4488 RVA: 0x0004CE2F File Offset: 0x0004B02F
		[ComVisible(false)]
		public void CancelErrorRead()
		{
			if (this.error != null)
			{
				this.error.CancelOperation();
				this.pendingErrorRead = false;
				return;
			}
			throw new InvalidOperationException(SR.GetString("No async read operation is in progress on the stream."));
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x0004CE60 File Offset: 0x0004B060
		internal void OutputReadNotifyUser(string data)
		{
			DataReceivedEventHandler outputDataReceived = this.OutputDataReceived;
			if (outputDataReceived != null)
			{
				DataReceivedEventArgs dataReceivedEventArgs = new DataReceivedEventArgs(data);
				if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
				{
					this.SynchronizingObject.Invoke(outputDataReceived, new object[]
					{
						this,
						dataReceivedEventArgs
					});
					return;
				}
				outputDataReceived(this, dataReceivedEventArgs);
			}
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0004CEB8 File Offset: 0x0004B0B8
		internal void ErrorReadNotifyUser(string data)
		{
			DataReceivedEventHandler errorDataReceived = this.ErrorDataReceived;
			if (errorDataReceived != null)
			{
				DataReceivedEventArgs dataReceivedEventArgs = new DataReceivedEventArgs(data);
				if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
				{
					this.SynchronizingObject.Invoke(errorDataReceived, new object[]
					{
						this,
						dataReceivedEventArgs
					});
					return;
				}
				errorDataReceived(this, dataReceivedEventArgs);
			}
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x0004CF0F File Offset: 0x0004B10F
		private Process(SafeProcessHandle handle, int id)
		{
			this.SetProcessHandle(handle);
			this.SetProcessId(id);
		}

		/// <summary>Gets the base priority of the associated process.</summary>
		/// <returns>The base priority, which is computed from the <see cref="P:System.Diagnostics.Process.PriorityClass" /> of the associated process.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property to <see langword="false" /> to access this property on Windows 98 and Windows Me.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process has exited.  
		///  -or-  
		///  The process has not started, so there is no process ID.</exception>
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x00003062 File Offset: 0x00001262
		[MonitoringDescription("Base process priority.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonoTODO]
		public int BasePriority
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the number of handles opened by the process.</summary>
		/// <returns>The number of operating system handles the process has opened.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property to <see langword="false" /> to access this property on Windows 98 and Windows Me.</exception>
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x00003062 File Offset: 0x00001262
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("Handles for this process.")]
		[MonoTODO]
		public int HandleCount
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the main module for the associated process.</summary>
		/// <returns>The <see cref="T:System.Diagnostics.ProcessModule" /> that was used to start the process.</returns>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.MainModule" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A 32-bit process is trying to access the modules of a 64-bit process.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to <see langword="false" /> to access this property on Windows 98 and Windows Me.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> is not available.  
		///  -or-  
		///  The process has exited.</exception>
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x0004CF25 File Offset: 0x0004B125
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The main module of the process.")]
		[Browsable(false)]
		public ProcessModule MainModule
		{
			get
			{
				if (this.processId == NativeMethods.GetCurrentProcessId())
				{
					if (Process.current_main_module == null)
					{
						Process.current_main_module = this.Modules[0];
					}
					return Process.current_main_module;
				}
				return this.Modules[0];
			}
		}

		// Token: 0x0600118F RID: 4495
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr MainWindowHandle_icall(int pid);

		/// <summary>Gets the window handle of the main window of the associated process.</summary>
		/// <returns>The system-generated window handle of the main window of the associated process.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.MainWindowHandle" /> is not defined because the process has exited.</exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.MainWindowHandle" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to <see langword="false" /> to access this property on Windows 98 and Windows Me.</exception>
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x0004CF5E File Offset: 0x0004B15E
		[MonitoringDescription("The handle of the main window of the process.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IntPtr MainWindowHandle
		{
			get
			{
				return Process.MainWindowHandle_icall(this.processId);
			}
		}

		/// <summary>Gets the caption of the main window of the process.</summary>
		/// <returns>The main window title of the process.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.MainWindowTitle" /> property is not defined because the process has exited.</exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.MainWindowTitle" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to <see langword="false" /> to access this property on Windows 98 and Windows Me.</exception>
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x000186CD File Offset: 0x000168CD
		[MonitoringDescription("The title of the main window of the process.")]
		[MonoTODO]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string MainWindowTitle
		{
			get
			{
				return "null";
			}
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x0004CF6C File Offset: 0x0004B16C
		private static void AppendArguments(StringBuilder stringBuilder, Collection<string> argumentList)
		{
			if (argumentList.Count > 0)
			{
				foreach (string argument in argumentList)
				{
					PasteArguments.AppendArgument(stringBuilder, argument);
				}
			}
		}

		// Token: 0x06001193 RID: 4499
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern ProcessModule[] GetModules_icall(IntPtr handle);

		// Token: 0x06001194 RID: 4500 RVA: 0x0004CFC0 File Offset: 0x0004B1C0
		private ProcessModule[] GetModules_internal(SafeProcessHandle handle)
		{
			bool flag = false;
			ProcessModule[] modules_icall;
			try
			{
				handle.DangerousAddRef(ref flag);
				modules_icall = this.GetModules_icall(handle.DangerousGetHandle());
			}
			finally
			{
				if (flag)
				{
					handle.DangerousRelease();
				}
			}
			return modules_icall;
		}

		/// <summary>Gets the modules that have been loaded by the associated process.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.ProcessModule" /> that represents the modules that have been loaded by the associated process.</returns>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.Modules" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> is not available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to <see langword="false" /> to access this property on Windows 98 and Windows Me.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">You are attempting to access the <see cref="P:System.Diagnostics.Process.Modules" /> property for either the system process or the idle process. These processes do not have modules.</exception>
		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x0004D004 File Offset: 0x0004B204
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The modules that are loaded as part of this process.")]
		public ProcessModuleCollection Modules
		{
			get
			{
				if (this.modules == null)
				{
					SafeProcessHandle handle = null;
					try
					{
						handle = this.GetProcessHandle(1024);
						this.modules = new ProcessModuleCollection(this.GetModules_internal(handle));
					}
					finally
					{
						this.ReleaseProcessHandle(handle);
					}
				}
				return this.modules;
			}
		}

		// Token: 0x06001196 RID: 4502
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetProcessData(int pid, int data_type, out int error);

		/// <summary>Gets the amount of nonpaged system memory, in bytes, allocated for the associated process.</summary>
		/// <returns>The amount of memory, in bytes, the system has allocated for the associated process that cannot be written to the virtual memory paging file.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x00003062 File Offset: 0x00001262
		[MonitoringDescription("The number of bytes that are not pageable.")]
		[Obsolete("Use NonpagedSystemMemorySize64")]
		[MonoTODO]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int NonpagedSystemMemorySize
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the amount of paged memory, in bytes, allocated for the associated process.</summary>
		/// <returns>The amount of memory, in bytes, allocated by the associated process that can be written to the virtual memory paging file.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x0004D05C File Offset: 0x0004B25C
		[Obsolete("Use PagedMemorySize64")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The number of bytes that are paged.")]
		public int PagedMemorySize
		{
			get
			{
				return (int)this.PagedMemorySize64;
			}
		}

		/// <summary>Gets the amount of pageable system memory, in bytes, allocated for the associated process.</summary>
		/// <returns>The amount of memory, in bytes, the system has allocated for the associated process that can be written to the virtual memory paging file.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x0004D05C File Offset: 0x0004B25C
		[Obsolete("Use PagedSystemMemorySize64")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The amount of paged system memory in bytes.")]
		public int PagedSystemMemorySize
		{
			get
			{
				return (int)this.PagedMemorySize64;
			}
		}

		/// <summary>Gets the maximum amount of memory in the virtual memory paging file, in bytes, used by the associated process.</summary>
		/// <returns>The maximum amount of memory, in bytes, allocated by the associated process that could be written to the virtual memory paging file.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x00003062 File Offset: 0x00001262
		[MonoTODO]
		[Obsolete("Use PeakPagedMemorySize64")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The maximum amount of paged memory used by this process.")]
		public int PeakPagedMemorySize
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the maximum amount of virtual memory, in bytes, used by the associated process.</summary>
		/// <returns>The maximum amount of virtual memory, in bytes, that the associated process has requested.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x0004D068 File Offset: 0x0004B268
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The maximum amount of virtual memory used by this process.")]
		[Obsolete("Use PeakVirtualMemorySize64")]
		public int PeakVirtualMemorySize
		{
			get
			{
				int num;
				return (int)Process.GetProcessData(this.processId, 8, out num);
			}
		}

		/// <summary>Gets the peak working set size for the associated process, in bytes.</summary>
		/// <returns>The maximum amount of physical memory that the associated process has required all at once, in bytes.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x0004D084 File Offset: 0x0004B284
		[Obsolete("Use PeakWorkingSet64")]
		[MonitoringDescription("The maximum amount of system memory used by this process.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int PeakWorkingSet
		{
			get
			{
				int num;
				return (int)Process.GetProcessData(this.processId, 5, out num);
			}
		}

		/// <summary>Gets the amount of nonpaged system memory, in bytes, allocated for the associated process.</summary>
		/// <returns>The amount of system memory, in bytes, allocated for the associated process that cannot be written to the virtual memory paging file.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x0004D0A0 File Offset: 0x0004B2A0
		[ComVisible(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The number of bytes that are not pageable.")]
		[MonoTODO]
		public long NonpagedSystemMemorySize64
		{
			get
			{
				return 0L;
			}
		}

		/// <summary>Gets the amount of paged memory, in bytes, allocated for the associated process.</summary>
		/// <returns>The amount of memory, in bytes, allocated in the virtual memory paging file for the associated process.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x0004D0A4 File Offset: 0x0004B2A4
		[ComVisible(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The number of bytes that are paged.")]
		public long PagedMemorySize64
		{
			get
			{
				int num;
				return Process.GetProcessData(this.processId, 12, out num);
			}
		}

		/// <summary>Gets the amount of pageable system memory, in bytes, allocated for the associated process.</summary>
		/// <returns>The amount of system memory, in bytes, allocated for the associated process that can be written to the virtual memory paging file.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x1700030A RID: 778
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x0004D0C0 File Offset: 0x0004B2C0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The amount of paged system memory in bytes.")]
		[ComVisible(false)]
		public long PagedSystemMemorySize64
		{
			get
			{
				return this.PagedMemorySize64;
			}
		}

		/// <summary>Gets the maximum amount of memory in the virtual memory paging file, in bytes, used by the associated process.</summary>
		/// <returns>The maximum amount of memory, in bytes, allocated in the virtual memory paging file for the associated process since it was started.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x0004D0A0 File Offset: 0x0004B2A0
		[MonitoringDescription("The maximum amount of paged memory used by this process.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ComVisible(false)]
		[MonoTODO]
		public long PeakPagedMemorySize64
		{
			get
			{
				return 0L;
			}
		}

		/// <summary>Gets the maximum amount of virtual memory, in bytes, used by the associated process.</summary>
		/// <returns>The maximum amount of virtual memory, in bytes, allocated for the associated process since it was started.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x0004D0C8 File Offset: 0x0004B2C8
		[MonitoringDescription("The maximum amount of virtual memory used by this process.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ComVisible(false)]
		public long PeakVirtualMemorySize64
		{
			get
			{
				int num;
				return Process.GetProcessData(this.processId, 8, out num);
			}
		}

		/// <summary>Gets the maximum amount of physical memory, in bytes, used by the associated process.</summary>
		/// <returns>The maximum amount of physical memory, in bytes, allocated for the associated process since it was started.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x0004D0E4 File Offset: 0x0004B2E4
		[ComVisible(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The maximum amount of system memory used by this process.")]
		public long PeakWorkingSet64
		{
			get
			{
				int num;
				return Process.GetProcessData(this.processId, 5, out num);
			}
		}

		/// <summary>Gets or sets a value indicating whether the associated process priority should temporarily be boosted by the operating system when the main window has the focus.</summary>
		/// <returns>
		///   <see langword="true" /> if dynamic boosting of the process priority should take place for a process when it is taken out of the wait state; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">Priority boost information could not be retrieved from the associated process resource.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.  
		///  -or-  
		///  The process identifier or process handle is zero. (The process has not been started.)</exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.PriorityBoostEnabled" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> is not available.</exception>
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060011A3 RID: 4515 RVA: 0x00003062 File Offset: 0x00001262
		// (set) Token: 0x060011A4 RID: 4516 RVA: 0x00003917 File Offset: 0x00001B17
		[MonitoringDescription("Process will be of higher priority while it is actively used.")]
		[MonoTODO]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool PriorityBoostEnabled
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/// <summary>Gets the amount of private memory, in bytes, allocated for the associated process.</summary>
		/// <returns>The number of bytes allocated by the associated process that cannot be shared with other processes.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060011A5 RID: 4517 RVA: 0x0004D100 File Offset: 0x0004B300
		[Obsolete("Use PrivateMemorySize64")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The amount of memory exclusively used by this process.")]
		public int PrivateMemorySize
		{
			get
			{
				int num;
				return (int)Process.GetProcessData(this.processId, 6, out num);
			}
		}

		/// <summary>Gets the Terminal Services session identifier for the associated process.</summary>
		/// <returns>The Terminal Services session identifier for the associated process.</returns>
		/// <exception cref="T:System.NullReferenceException">There is no session associated with this process.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no process associated with this session identifier.  
		///  -or-  
		///  The associated process is not on this machine.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <see cref="P:System.Diagnostics.Process.SessionId" /> property is not supported on Windows 98.</exception>
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060011A6 RID: 4518 RVA: 0x00003062 File Offset: 0x00001262
		[MonoNotSupported("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The session ID for this process.")]
		public int SessionId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060011A7 RID: 4519
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string ProcessName_icall(IntPtr handle);

		// Token: 0x060011A8 RID: 4520 RVA: 0x0004D11C File Offset: 0x0004B31C
		private static string ProcessName_internal(SafeProcessHandle handle)
		{
			bool flag = false;
			string result;
			try
			{
				handle.DangerousAddRef(ref flag);
				result = Process.ProcessName_icall(handle.DangerousGetHandle());
			}
			finally
			{
				if (flag)
				{
					handle.DangerousRelease();
				}
			}
			return result;
		}

		/// <summary>Gets the name of the process.</summary>
		/// <returns>The name that the system uses to identify the process to the user.</returns>
		/// <exception cref="T:System.InvalidOperationException">The process does not have an identifier, or no process is associated with the <see cref="T:System.Diagnostics.Process" />.  
		///  -or-  
		///  The associated process has exited.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to <see langword="false" /> to access this property on Windows 98 and Windows Me.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is not on this computer.</exception>
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x0004D15C File Offset: 0x0004B35C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The name of this process.")]
		public string ProcessName
		{
			get
			{
				if (this.process_name == null)
				{
					SafeProcessHandle handle = null;
					try
					{
						handle = this.GetProcessHandle(1024);
						this.process_name = Process.ProcessName_internal(handle);
						if (this.process_name == null)
						{
							throw new InvalidOperationException("Process has exited or is inaccessible, so the requested information is not available.");
						}
						if (this.process_name.EndsWith(".exe") || this.process_name.EndsWith(".bat") || this.process_name.EndsWith(".com"))
						{
							this.process_name = this.process_name.Substring(0, this.process_name.Length - 4);
						}
					}
					finally
					{
						this.ReleaseProcessHandle(handle);
					}
				}
				return this.process_name;
			}
		}

		/// <summary>Gets or sets the processors on which the threads in this process can be scheduled to run.</summary>
		/// <returns>A bitmask representing the processors that the threads in the associated process can run on. The default depends on the number of processors on the computer. The default value is 2 n -1, where n is the number of processors.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">
		///   <see cref="P:System.Diagnostics.Process.ProcessorAffinity" /> information could not be set or retrieved from the associated process resource.  
		/// -or-  
		/// The process identifier or process handle is zero. (The process has not been started.)</exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.ProcessorAffinity" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> was not available.  
		///  -or-  
		///  The process has exited.</exception>
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x0004D218 File Offset: 0x0004B418
		// (set) Token: 0x060011AB RID: 4523 RVA: 0x00003917 File Offset: 0x00001B17
		[MonoTODO]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("Allowed processor that can be used by this process.")]
		public IntPtr ProcessorAffinity
		{
			get
			{
				return (IntPtr)0;
			}
			set
			{
			}
		}

		/// <summary>Gets a value indicating whether the user interface of the process is responding.</summary>
		/// <returns>
		///   <see langword="true" /> if the user interface of the associated process is responding to the system; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to <see langword="false" /> to access this property on Windows 98 and Windows Me.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.</exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.Responding" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x00003062 File Offset: 0x00001262
		[MonitoringDescription("Is this process responsive.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonoTODO]
		public bool Responding
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the set of threads that are running in the associated process.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.ProcessThread" /> representing the operating system threads currently running in the associated process.</returns>
		/// <exception cref="T:System.SystemException">The process does not have an <see cref="P:System.Diagnostics.Process.Id" />, or no process is associated with the <see cref="T:System.Diagnostics.Process" /> instance.  
		///  -or-  
		///  The associated process has exited.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to <see langword="false" /> to access this property on Windows 98 and Windows Me.</exception>
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x0004D220 File Offset: 0x0004B420
		[MonoTODO]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The number of threads of this process.")]
		public ProcessThreadCollection Threads
		{
			get
			{
				if (this.threads == null)
				{
					int num;
					this.threads = new ProcessThreadCollection(new ProcessThread[Process.GetProcessData(this.processId, 0, out num)]);
				}
				return this.threads;
			}
		}

		/// <summary>Gets the size of the process's virtual memory, in bytes.</summary>
		/// <returns>The amount of virtual memory, in bytes, that the associated process has requested.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x0004D25C File Offset: 0x0004B45C
		[MonitoringDescription("The amount of virtual memory currently used for this process.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Obsolete("Use VirtualMemorySize64")]
		public int VirtualMemorySize
		{
			get
			{
				int num;
				return (int)Process.GetProcessData(this.processId, 7, out num);
			}
		}

		/// <summary>Gets the associated process's physical memory usage, in bytes.</summary>
		/// <returns>The total amount of physical memory the associated process is using, in bytes.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x0004D278 File Offset: 0x0004B478
		[MonitoringDescription("The amount of physical memory currently used for this process.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Obsolete("Use WorkingSet64")]
		public int WorkingSet
		{
			get
			{
				int num;
				return (int)Process.GetProcessData(this.processId, 4, out num);
			}
		}

		/// <summary>Gets the amount of private memory, in bytes, allocated for the associated process.</summary>
		/// <returns>The amount of memory, in bytes, allocated for the associated process that cannot be shared with other processes.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x0004D294 File Offset: 0x0004B494
		[ComVisible(false)]
		[MonitoringDescription("The amount of memory exclusively used by this process.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public long PrivateMemorySize64
		{
			get
			{
				int num;
				return Process.GetProcessData(this.processId, 6, out num);
			}
		}

		/// <summary>Gets the amount of the virtual memory, in bytes, allocated for the associated process.</summary>
		/// <returns>The amount of virtual memory, in bytes, allocated for the associated process.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x0004D2B0 File Offset: 0x0004B4B0
		[MonitoringDescription("The amount of virtual memory currently used for this process.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ComVisible(false)]
		public long VirtualMemorySize64
		{
			get
			{
				int num;
				return Process.GetProcessData(this.processId, 7, out num);
			}
		}

		/// <summary>Gets the amount of physical memory, in bytes, allocated for the associated process.</summary>
		/// <returns>The amount of physical memory, in bytes, allocated for the associated process.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x0004D2CC File Offset: 0x0004B4CC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ComVisible(false)]
		[MonitoringDescription("The amount of physical memory currently used for this process.")]
		public long WorkingSet64
		{
			get
			{
				int num;
				return Process.GetProcessData(this.processId, 4, out num);
			}
		}

		/// <summary>Closes a process that has a user interface by sending a close message to its main window.</summary>
		/// <returns>
		///   <see langword="true" /> if the close message was successfully sent; <see langword="false" /> if the associated process does not have a main window or if the main window is disabled (for example if a modal dialog is being shown).</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property to <see langword="false" /> to access this property on Windows 98 and Windows Me.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process has already exited.  
		///  -or-  
		///  No process is associated with this <see cref="T:System.Diagnostics.Process" /> object.</exception>
		// Token: 0x060011B3 RID: 4531 RVA: 0x0004D2E8 File Offset: 0x0004B4E8
		public bool CloseMainWindow()
		{
			SafeProcessHandle safeProcessHandle = null;
			bool result;
			try
			{
				safeProcessHandle = this.GetProcessHandle(1);
				result = NativeMethods.TerminateProcess(safeProcessHandle, -2);
			}
			finally
			{
				this.ReleaseProcessHandle(safeProcessHandle);
			}
			return result;
		}

		// Token: 0x060011B4 RID: 4532
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetProcess_internal(int pid);

		/// <summary>Returns a new <see cref="T:System.Diagnostics.Process" /> component, given a process identifier and the name of a computer on the network.</summary>
		/// <param name="processId">The system-unique identifier of a process resource.</param>
		/// <param name="machineName">The name of a computer on the network.</param>
		/// <returns>A <see cref="T:System.Diagnostics.Process" /> component that is associated with a remote process resource identified by the <paramref name="processId" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">The process specified by the <paramref name="processId" /> parameter is not running. The identifier might be expired.  
		///  -or-  
		///  The <paramref name="machineName" /> parameter syntax is invalid. The name might have length zero (0).</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="machineName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process was not started by this object.</exception>
		// Token: 0x060011B5 RID: 4533 RVA: 0x0004D324 File Offset: 0x0004B524
		[MonoTODO("There is no support for retrieving process information from a remote machine")]
		public static Process GetProcessById(int processId, string machineName)
		{
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			if (!Process.IsLocalMachine(machineName))
			{
				throw new NotImplementedException();
			}
			IntPtr process_internal = Process.GetProcess_internal(processId);
			if (process_internal == IntPtr.Zero)
			{
				throw new ArgumentException("Can't find process with ID " + processId.ToString());
			}
			return new Process(new SafeProcessHandle(process_internal, true), processId);
		}

		/// <summary>Creates an array of new <see cref="T:System.Diagnostics.Process" /> components and associates them with all the process resources on a remote computer that share the specified process name.</summary>
		/// <param name="processName">The friendly name of the process.</param>
		/// <param name="machineName">The name of a computer on the network.</param>
		/// <returns>An array of type <see cref="T:System.Diagnostics.Process" /> that represents the process resources running the specified application or file.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> parameter syntax is invalid. It might have length zero (0).</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="machineName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system platform does not support this operation on remote computers.</exception>
		/// <exception cref="T:System.InvalidOperationException">The attempt to connect to <paramref name="machineName" /> has failed.
		///  -or- 
		/// There are problems accessing the performance counter API's used to get process information. This exception is specific to Windows NT, Windows 2000, and Windows XP.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A problem occurred accessing an underlying system API.</exception>
		// Token: 0x060011B6 RID: 4534 RVA: 0x0004D384 File Offset: 0x0004B584
		public static Process[] GetProcessesByName(string processName, string machineName)
		{
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			if (!Process.IsLocalMachine(machineName))
			{
				throw new NotImplementedException();
			}
			Process[] processes = Process.GetProcesses();
			if (processes.Length == 0)
			{
				return processes;
			}
			int newSize = 0;
			foreach (Process process in processes)
			{
				try
				{
					if (string.Compare(processName, process.ProcessName, true) == 0)
					{
						processes[newSize++] = process;
					}
					else
					{
						process.Dispose();
					}
				}
				catch (SystemException)
				{
				}
			}
			Array.Resize<Process>(ref processes, newSize);
			return processes;
		}

		// Token: 0x060011B7 RID: 4535
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int[] GetProcesses_internal();

		/// <summary>Creates a new <see cref="T:System.Diagnostics.Process" /> component for each process resource on the specified computer.</summary>
		/// <param name="machineName">The computer from which to read the list of processes.</param>
		/// <returns>An array of type <see cref="T:System.Diagnostics.Process" /> that represents all the process resources running on the specified computer.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> parameter syntax is invalid. It might have length zero (0).</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="machineName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system platform does not support this operation on remote computers.</exception>
		/// <exception cref="T:System.InvalidOperationException">There are problems accessing the performance counter API's used to get process information. This exception is specific to Windows NT, Windows 2000, and Windows XP.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A problem occurred accessing an underlying system API.</exception>
		// Token: 0x060011B8 RID: 4536 RVA: 0x0004D40C File Offset: 0x0004B60C
		[MonoTODO("There is no support for retrieving process information from a remote machine")]
		public static Process[] GetProcesses(string machineName)
		{
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			if (!Process.IsLocalMachine(machineName))
			{
				throw new NotImplementedException();
			}
			int[] processes_internal = Process.GetProcesses_internal();
			if (processes_internal == null)
			{
				return new Process[0];
			}
			List<Process> list = new List<Process>(processes_internal.Length);
			for (int i = 0; i < processes_internal.Length; i++)
			{
				try
				{
					list.Add(Process.GetProcessById(processes_internal[i]));
				}
				catch (SystemException)
				{
				}
			}
			return list.ToArray();
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0004D488 File Offset: 0x0004B688
		private static bool IsLocalMachine(string machineName)
		{
			return machineName == "." || machineName.Length == 0 || string.Compare(machineName, Environment.MachineName, true) == 0;
		}

		// Token: 0x060011BA RID: 4538
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ShellExecuteEx_internal(ProcessStartInfo startInfo, ref Process.ProcInfo procInfo);

		// Token: 0x060011BB RID: 4539
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateProcess_internal(ProcessStartInfo startInfo, IntPtr stdin, IntPtr stdout, IntPtr stderr, ref Process.ProcInfo procInfo);

		// Token: 0x060011BC RID: 4540 RVA: 0x0004D4B0 File Offset: 0x0004B6B0
		private bool StartWithShellExecuteEx(ProcessStartInfo startInfo)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!string.IsNullOrEmpty(startInfo.UserName) || startInfo.Password != null)
			{
				throw new InvalidOperationException(SR.GetString("The Process object must have the UseShellExecute property set to false in order to start a process as a user."));
			}
			if (startInfo.RedirectStandardInput || startInfo.RedirectStandardOutput || startInfo.RedirectStandardError)
			{
				throw new InvalidOperationException(SR.GetString("The Process object must have the UseShellExecute property set to false in order to redirect IO streams."));
			}
			if (startInfo.StandardErrorEncoding != null)
			{
				throw new InvalidOperationException(SR.GetString("StandardErrorEncoding is only supported when standard error is redirected."));
			}
			if (startInfo.StandardOutputEncoding != null)
			{
				throw new InvalidOperationException(SR.GetString("StandardOutputEncoding is only supported when standard output is redirected."));
			}
			if (startInfo.environmentVariables != null)
			{
				throw new InvalidOperationException(SR.GetString("The Process object must have the UseShellExecute property set to false in order to use environment variables."));
			}
			Process.ProcInfo procInfo = default(Process.ProcInfo);
			Process.FillUserInfo(startInfo, ref procInfo);
			bool flag;
			try
			{
				flag = Process.ShellExecuteEx_internal(startInfo, ref procInfo);
			}
			finally
			{
				if (procInfo.Password != IntPtr.Zero)
				{
					Marshal.ZeroFreeBSTR(procInfo.Password);
				}
				procInfo.Password = IntPtr.Zero;
			}
			if (!flag)
			{
				throw new Win32Exception(-procInfo.pid);
			}
			this.SetProcessHandle(new SafeProcessHandle(procInfo.process_handle, true));
			this.SetProcessId(procInfo.pid);
			return flag;
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0004D5F0 File Offset: 0x0004B7F0
		private static void CreatePipe(out IntPtr read, out IntPtr write, bool writeDirection)
		{
			MonoIOError monoIOError;
			if (!MonoIO.CreatePipe(out read, out write, out monoIOError))
			{
				throw MonoIO.GetException(monoIOError);
			}
			if (Process.IsWindows)
			{
				IntPtr intPtr = writeDirection ? write : read;
				if (!MonoIO.DuplicateHandle(Process.GetCurrentProcess().Handle, intPtr, Process.GetCurrentProcess().Handle, out intPtr, 0, 0, 2, out monoIOError))
				{
					throw MonoIO.GetException(monoIOError);
				}
				if (writeDirection)
				{
					if (!MonoIO.Close(write, out monoIOError))
					{
						throw MonoIO.GetException(monoIOError);
					}
					write = intPtr;
					return;
				}
				else
				{
					if (!MonoIO.Close(read, out monoIOError))
					{
						throw MonoIO.GetException(monoIOError);
					}
					read = intPtr;
				}
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060011BE RID: 4542 RVA: 0x0004D678 File Offset: 0x0004B878
		private static bool IsWindows
		{
			get
			{
				PlatformID platform = Environment.OSVersion.Platform;
				return platform == PlatformID.Win32S || platform == PlatformID.Win32Windows || platform == PlatformID.Win32NT || platform == PlatformID.WinCE;
			}
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0004D6A4 File Offset: 0x0004B8A4
		private bool StartWithCreateProcess(ProcessStartInfo startInfo)
		{
			if (startInfo.StandardOutputEncoding != null && !startInfo.RedirectStandardOutput)
			{
				throw new InvalidOperationException(SR.GetString("StandardOutputEncoding is only supported when standard output is redirected."));
			}
			if (startInfo.StandardErrorEncoding != null && !startInfo.RedirectStandardError)
			{
				throw new InvalidOperationException(SR.GetString("StandardErrorEncoding is only supported when standard error is redirected."));
			}
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			Process.ProcInfo procInfo = default(Process.ProcInfo);
			if (startInfo.HaveEnvVars)
			{
				List<string> list = new List<string>();
				foreach (object obj in startInfo.EnvironmentVariables)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (dictionaryEntry.Value != null)
					{
						list.Add((string)dictionaryEntry.Key + "=" + (string)dictionaryEntry.Value);
					}
				}
				procInfo.envVariables = list.ToArray();
			}
			if (startInfo.ArgumentList.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string argument in startInfo.ArgumentList)
				{
					PasteArguments.AppendArgument(stringBuilder, argument);
				}
				startInfo.Arguments = stringBuilder.ToString();
			}
			IntPtr intPtr = IntPtr.Zero;
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr zero3 = IntPtr.Zero;
			IntPtr intPtr3 = IntPtr.Zero;
			try
			{
				if (startInfo.RedirectStandardInput)
				{
					Process.CreatePipe(out intPtr, out zero, true);
				}
				else
				{
					intPtr = MonoIO.ConsoleInput;
					zero = IntPtr.Zero;
				}
				if (startInfo.RedirectStandardOutput)
				{
					Process.CreatePipe(out zero2, out intPtr2, false);
				}
				else
				{
					zero2 = IntPtr.Zero;
					intPtr2 = MonoIO.ConsoleOutput;
				}
				if (startInfo.RedirectStandardError)
				{
					Process.CreatePipe(out zero3, out intPtr3, false);
				}
				else
				{
					zero3 = IntPtr.Zero;
					intPtr3 = MonoIO.ConsoleError;
				}
				Process.FillUserInfo(startInfo, ref procInfo);
				if (!Process.CreateProcess_internal(startInfo, intPtr, intPtr2, intPtr3, ref procInfo))
				{
					throw new Win32Exception(-procInfo.pid, string.Concat(new string[]
					{
						"ApplicationName='",
						startInfo.FileName,
						"', CommandLine='",
						startInfo.Arguments,
						"', CurrentDirectory='",
						startInfo.WorkingDirectory,
						"', Native error= ",
						Win32Exception.GetErrorMessage(-procInfo.pid)
					}));
				}
			}
			catch
			{
				if (startInfo.RedirectStandardInput)
				{
					if (intPtr != IntPtr.Zero)
					{
						MonoIOError monoIOError;
						MonoIO.Close(intPtr, out monoIOError);
					}
					if (zero != IntPtr.Zero)
					{
						MonoIOError monoIOError;
						MonoIO.Close(zero, out monoIOError);
					}
				}
				if (startInfo.RedirectStandardOutput)
				{
					if (zero2 != IntPtr.Zero)
					{
						MonoIOError monoIOError;
						MonoIO.Close(zero2, out monoIOError);
					}
					if (intPtr2 != IntPtr.Zero)
					{
						MonoIOError monoIOError;
						MonoIO.Close(intPtr2, out monoIOError);
					}
				}
				if (startInfo.RedirectStandardError)
				{
					if (zero3 != IntPtr.Zero)
					{
						MonoIOError monoIOError;
						MonoIO.Close(zero3, out monoIOError);
					}
					if (intPtr3 != IntPtr.Zero)
					{
						MonoIOError monoIOError;
						MonoIO.Close(intPtr3, out monoIOError);
					}
				}
				throw;
			}
			finally
			{
				if (procInfo.Password != IntPtr.Zero)
				{
					Marshal.ZeroFreeBSTR(procInfo.Password);
					procInfo.Password = IntPtr.Zero;
				}
			}
			this.SetProcessHandle(new SafeProcessHandle(procInfo.process_handle, true));
			this.SetProcessId(procInfo.pid);
			if (startInfo.RedirectStandardInput)
			{
				MonoIOError monoIOError;
				MonoIO.Close(intPtr, out monoIOError);
				Encoding encoding = startInfo.StandardInputEncoding ?? Console.InputEncoding;
				this.standardInput = new StreamWriter(new FileStream(zero, FileAccess.Write, true, 8192), encoding)
				{
					AutoFlush = true
				};
			}
			if (startInfo.RedirectStandardOutput)
			{
				MonoIOError monoIOError;
				MonoIO.Close(intPtr2, out monoIOError);
				Encoding encoding2 = startInfo.StandardOutputEncoding ?? Console.OutputEncoding;
				this.standardOutput = new StreamReader(new FileStream(zero2, FileAccess.Read, true, 8192), encoding2, true);
			}
			if (startInfo.RedirectStandardError)
			{
				MonoIOError monoIOError;
				MonoIO.Close(intPtr3, out monoIOError);
				Encoding encoding3 = startInfo.StandardErrorEncoding ?? Console.OutputEncoding;
				this.standardError = new StreamReader(new FileStream(zero3, FileAccess.Read, true, 8192), encoding3, true);
			}
			return true;
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0004DB1C File Offset: 0x0004BD1C
		private static void FillUserInfo(ProcessStartInfo startInfo, ref Process.ProcInfo procInfo)
		{
			if (startInfo.UserName.Length != 0)
			{
				procInfo.UserName = startInfo.UserName;
				procInfo.Domain = startInfo.Domain;
				if (startInfo.Password != null)
				{
					procInfo.Password = Marshal.SecureStringToBSTR(startInfo.Password);
				}
				else
				{
					procInfo.Password = IntPtr.Zero;
				}
				procInfo.LoadUserProfile = startInfo.LoadUserProfile;
			}
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x0004DB80 File Offset: 0x0004BD80
		private void RaiseOnExited()
		{
			if (!this.watchForExit)
			{
				return;
			}
			if (!this.raisedOnExited)
			{
				lock (this)
				{
					if (!this.raisedOnExited)
					{
						this.raisedOnExited = true;
						this.OnExited();
					}
				}
			}
		}

		// Token: 0x04000A2D RID: 2605
		private bool haveProcessId;

		// Token: 0x04000A2E RID: 2606
		private int processId;

		// Token: 0x04000A2F RID: 2607
		private bool haveProcessHandle;

		// Token: 0x04000A30 RID: 2608
		private SafeProcessHandle m_processHandle;

		// Token: 0x04000A31 RID: 2609
		private bool isRemoteMachine;

		// Token: 0x04000A32 RID: 2610
		private string machineName;

		// Token: 0x04000A33 RID: 2611
		private int m_processAccess;

		// Token: 0x04000A34 RID: 2612
		private ProcessThreadCollection threads;

		// Token: 0x04000A35 RID: 2613
		private ProcessModuleCollection modules;

		// Token: 0x04000A36 RID: 2614
		private bool haveWorkingSetLimits;

		// Token: 0x04000A37 RID: 2615
		private IntPtr minWorkingSet;

		// Token: 0x04000A38 RID: 2616
		private IntPtr maxWorkingSet;

		// Token: 0x04000A39 RID: 2617
		private bool havePriorityClass;

		// Token: 0x04000A3A RID: 2618
		private ProcessPriorityClass priorityClass;

		// Token: 0x04000A3B RID: 2619
		private ProcessStartInfo startInfo;

		// Token: 0x04000A3C RID: 2620
		private bool watchForExit;

		// Token: 0x04000A3D RID: 2621
		private bool watchingForExit;

		// Token: 0x04000A3E RID: 2622
		private EventHandler onExited;

		// Token: 0x04000A3F RID: 2623
		private bool exited;

		// Token: 0x04000A40 RID: 2624
		private int exitCode;

		// Token: 0x04000A41 RID: 2625
		private bool signaled;

		// Token: 0x04000A42 RID: 2626
		private DateTime exitTime;

		// Token: 0x04000A43 RID: 2627
		private bool haveExitTime;

		// Token: 0x04000A44 RID: 2628
		private bool raisedOnExited;

		// Token: 0x04000A45 RID: 2629
		private RegisteredWaitHandle registeredWaitHandle;

		// Token: 0x04000A46 RID: 2630
		private WaitHandle waitHandle;

		// Token: 0x04000A47 RID: 2631
		private ISynchronizeInvoke synchronizingObject;

		// Token: 0x04000A48 RID: 2632
		private StreamReader standardOutput;

		// Token: 0x04000A49 RID: 2633
		private StreamWriter standardInput;

		// Token: 0x04000A4A RID: 2634
		private StreamReader standardError;

		// Token: 0x04000A4B RID: 2635
		private OperatingSystem operatingSystem;

		// Token: 0x04000A4C RID: 2636
		private bool disposed;

		// Token: 0x04000A4D RID: 2637
		private Process.StreamReadMode outputStreamReadMode;

		// Token: 0x04000A4E RID: 2638
		private Process.StreamReadMode errorStreamReadMode;

		// Token: 0x04000A4F RID: 2639
		private Process.StreamReadMode inputStreamReadMode;

		// Token: 0x04000A50 RID: 2640
		[CompilerGenerated]
		private DataReceivedEventHandler OutputDataReceived;

		// Token: 0x04000A51 RID: 2641
		[CompilerGenerated]
		private DataReceivedEventHandler ErrorDataReceived;

		// Token: 0x04000A52 RID: 2642
		internal AsyncStreamReader output;

		// Token: 0x04000A53 RID: 2643
		internal AsyncStreamReader error;

		// Token: 0x04000A54 RID: 2644
		internal bool pendingOutputRead;

		// Token: 0x04000A55 RID: 2645
		internal bool pendingErrorRead;

		// Token: 0x04000A56 RID: 2646
		internal static TraceSwitch processTracing;

		// Token: 0x04000A57 RID: 2647
		private string process_name;

		// Token: 0x04000A58 RID: 2648
		private static ProcessModule current_main_module;

		// Token: 0x0200023D RID: 573
		private enum StreamReadMode
		{
			// Token: 0x04000A5A RID: 2650
			undefined,
			// Token: 0x04000A5B RID: 2651
			syncMode,
			// Token: 0x04000A5C RID: 2652
			asyncMode
		}

		// Token: 0x0200023E RID: 574
		private enum State
		{
			// Token: 0x04000A5E RID: 2654
			HaveId = 1,
			// Token: 0x04000A5F RID: 2655
			IsLocal,
			// Token: 0x04000A60 RID: 2656
			IsNt = 4,
			// Token: 0x04000A61 RID: 2657
			HaveProcessInfo = 8,
			// Token: 0x04000A62 RID: 2658
			Exited = 16,
			// Token: 0x04000A63 RID: 2659
			Associated = 32,
			// Token: 0x04000A64 RID: 2660
			IsWin2k = 64,
			// Token: 0x04000A65 RID: 2661
			HaveNtProcessInfo = 12
		}

		// Token: 0x0200023F RID: 575
		private struct ProcInfo
		{
			// Token: 0x04000A66 RID: 2662
			public IntPtr process_handle;

			// Token: 0x04000A67 RID: 2663
			public int pid;

			// Token: 0x04000A68 RID: 2664
			public string[] envVariables;

			// Token: 0x04000A69 RID: 2665
			public string UserName;

			// Token: 0x04000A6A RID: 2666
			public string Domain;

			// Token: 0x04000A6B RID: 2667
			public IntPtr Password;

			// Token: 0x04000A6C RID: 2668
			public bool LoadUserProfile;
		}
	}
}
