using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Represents an operating system process thread.</summary>
	// Token: 0x0200027E RID: 638
	[Designer("System.Diagnostics.Design.ProcessThreadDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class ProcessThread : Component
	{
		// Token: 0x06001448 RID: 5192 RVA: 0x000507D0 File Offset: 0x0004E9D0
		[MonoTODO("Parse parameters")]
		internal ProcessThread()
		{
		}

		/// <summary>Gets the base priority of the thread.</summary>
		/// <returns>The base priority of the thread, which the operating system computes by combining the process priority class with the priority level of the associated thread.</returns>
		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x00003062 File Offset: 0x00001262
		[MonitoringDescription("The base priority of this thread.")]
		[MonoTODO]
		public int BasePriority
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the current priority of the thread.</summary>
		/// <returns>The current priority of the thread, which may deviate from the base priority based on how the operating system is scheduling the thread. The priority may be temporarily boosted for an active thread.</returns>
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x00003062 File Offset: 0x00001262
		[MonoTODO]
		[MonitoringDescription("The current priority of this thread.")]
		public int CurrentPriority
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the unique identifier of the thread.</summary>
		/// <returns>The unique identifier associated with a specific thread.</returns>
		// Token: 0x170003CF RID: 975
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x00003062 File Offset: 0x00001262
		[MonitoringDescription("The ID of this thread.")]
		[MonoTODO]
		public int Id
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Sets the preferred processor for this thread to run on.</summary>
		/// <returns>The preferred processor for the thread, used when the system schedules threads, to determine which processor to run the thread on.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The system could not set the thread to start on the specified processor.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		// Token: 0x170003D0 RID: 976
		// (set) Token: 0x0600144C RID: 5196 RVA: 0x00003917 File Offset: 0x00001B17
		[Browsable(false)]
		[MonoTODO]
		public int IdealProcessor
		{
			set
			{
			}
		}

		/// <summary>Gets or sets a value indicating whether the operating system should temporarily boost the priority of the associated thread whenever the main window of the thread's process receives the focus.</summary>
		/// <returns>
		///   <see langword="true" /> to boost the thread's priority when the user interacts with the process's interface; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The priority boost information could not be retrieved.  
		///  -or-  
		///  The priority boost information could not be set.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x00003062 File Offset: 0x00001262
		// (set) Token: 0x0600144E RID: 5198 RVA: 0x00003917 File Offset: 0x00001B17
		[MonitoringDescription("Thread gets a priority boot when interactively used by a user.")]
		[MonoTODO]
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

		/// <summary>Gets or sets the priority level of the thread.</summary>
		/// <returns>One of the <see cref="T:System.Diagnostics.ThreadPriorityLevel" /> values, specifying a range that bounds the thread's priority.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The thread priority level information could not be retrieved.  
		///  -or-  
		///  The thread priority level could not be set.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x00053124 File Offset: 0x00051324
		// (set) Token: 0x06001450 RID: 5200 RVA: 0x00003917 File Offset: 0x00001B17
		[MonoTODO]
		[MonitoringDescription("The priority level of this thread.")]
		public ThreadPriorityLevel PriorityLevel
		{
			get
			{
				return ThreadPriorityLevel.Idle;
			}
			set
			{
			}
		}

		/// <summary>Gets the amount of time that the thread has spent running code inside the operating system core.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> indicating the amount of time that the thread has spent running code inside the operating system core.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The thread time could not be retrieved.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x00053128 File Offset: 0x00051328
		[MonitoringDescription("The amount of CPU time used in privileged mode.")]
		[MonoTODO]
		public TimeSpan PrivilegedProcessorTime
		{
			get
			{
				return new TimeSpan(0L);
			}
		}

		/// <summary>Sets the processors on which the associated thread can run.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> that points to a set of bits, each of which represents a processor that the thread can run on.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The processor affinity could not be set.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		// Token: 0x170003D4 RID: 980
		// (set) Token: 0x06001452 RID: 5202 RVA: 0x00003917 File Offset: 0x00001B17
		[Browsable(false)]
		[MonoTODO]
		public IntPtr ProcessorAffinity
		{
			set
			{
			}
		}

		/// <summary>Gets the memory address of the function that the operating system called that started this thread.</summary>
		/// <returns>The thread's starting address, which points to the application-defined function that the thread executes.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x0004D218 File Offset: 0x0004B418
		[MonitoringDescription("The start address in memory of this thread.")]
		[MonoTODO]
		public IntPtr StartAddress
		{
			get
			{
				return (IntPtr)0;
			}
		}

		/// <summary>Gets the time that the operating system started the thread.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> representing the time that was on the system when the operating system started the thread.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The thread time could not be retrieved.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x00053131 File Offset: 0x00051331
		[MonoTODO]
		[MonitoringDescription("The time this thread was started.")]
		public DateTime StartTime
		{
			get
			{
				return new DateTime(0L);
			}
		}

		/// <summary>Gets the current state of this thread.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.ThreadState" /> that indicates the thread's execution, for example, running, waiting, or terminated.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x00003062 File Offset: 0x00001262
		[MonitoringDescription("The current state of this thread.")]
		[MonoTODO]
		public ThreadState ThreadState
		{
			get
			{
				return ThreadState.Initialized;
			}
		}

		/// <summary>Gets the total amount of time that this thread has spent using the processor.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that indicates the amount of time that the thread has had control of the processor.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The thread time could not be retrieved.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x00053128 File Offset: 0x00051328
		[MonitoringDescription("The total amount of CPU time used.")]
		[MonoTODO]
		public TimeSpan TotalProcessorTime
		{
			get
			{
				return new TimeSpan(0L);
			}
		}

		/// <summary>Gets the amount of time that the associated thread has spent running code inside the application.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> indicating the amount of time that the thread has spent running code inside the application, as opposed to inside the operating system core.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The thread time could not be retrieved.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x00053128 File Offset: 0x00051328
		[MonoTODO]
		[MonitoringDescription("The amount of CPU time used in user mode.")]
		public TimeSpan UserProcessorTime
		{
			get
			{
				return new TimeSpan(0L);
			}
		}

		/// <summary>Gets the reason that the thread is waiting.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.ThreadWaitReason" /> representing the reason that the thread is in the wait state.</returns>
		/// <exception cref="T:System.InvalidOperationException">The thread is not in the wait state.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x00003062 File Offset: 0x00001262
		[MonoTODO]
		[MonitoringDescription("The reason why this thread is waiting.")]
		public ThreadWaitReason WaitReason
		{
			get
			{
				return ThreadWaitReason.Executive;
			}
		}

		/// <summary>Resets the ideal processor for this thread to indicate that there is no single ideal processor. In other words, so that any processor is ideal.</summary>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The ideal processor could not be reset.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		// Token: 0x06001459 RID: 5209 RVA: 0x00003917 File Offset: 0x00001B17
		[MonoTODO]
		public void ResetIdealProcessor()
		{
		}
	}
}
