using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Threading;

namespace System.Timers
{
	/// <summary>Generates an event after a set interval, with an option to generate recurring events.</summary>
	// Token: 0x02000193 RID: 403
	[DefaultProperty("Interval")]
	[DefaultEvent("Elapsed")]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class Timer : Component, ISupportInitialize
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Timers.Timer" /> class, and sets all the properties to their initial values.</summary>
		// Token: 0x06000A8A RID: 2698 RVA: 0x0002D760 File Offset: 0x0002B960
		public Timer()
		{
			this.interval = 100.0;
			this.enabled = false;
			this.autoReset = true;
			this.initializing = false;
			this.delayedEnable = false;
			this.callback = new TimerCallback(this.MyTimerCallback);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Timers.Timer" /> class, and sets the <see cref="P:System.Timers.Timer.Interval" /> property to the specified number of milliseconds.</summary>
		/// <param name="interval">The time, in milliseconds, between events. The value must be greater than zero and less than or equal to <see cref="F:System.Int32.MaxValue" />.</param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="interval" /> parameter is less than or equal to zero, or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06000A8B RID: 2699 RVA: 0x0002D7B0 File Offset: 0x0002B9B0
		public Timer(double interval) : this()
		{
			if (interval <= 0.0)
			{
				throw new ArgumentException(SR.GetString("Invalid value '{1}' for parameter '{0}'.", new object[]
				{
					"interval",
					interval
				}));
			}
			this.interval = (double)Timer.CalculateRoundedInterval(interval, true);
		}

		/// <summary>Gets or sets a Boolean indicating whether the <see cref="T:System.Timers.Timer" /> should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event only once (<see langword="false" />) or repeatedly (<see langword="true" />).</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Timers.Timer" /> should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event each time the interval elapses; <see langword="false" /> if it should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event only once, after the first time the interval elapses. The default is <see langword="true" />.</returns>
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x0002D804 File Offset: 0x0002BA04
		// (set) Token: 0x06000A8D RID: 2701 RVA: 0x0002D80C File Offset: 0x0002BA0C
		[TimersDescription("Indicates whether the timer will be restarted when it is enabled.")]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool AutoReset
		{
			get
			{
				return this.autoReset;
			}
			set
			{
				if (base.DesignMode)
				{
					this.autoReset = value;
					return;
				}
				if (this.autoReset != value)
				{
					this.autoReset = value;
					if (this.timer != null)
					{
						this.UpdateTimer();
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Timers.Timer" /> should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Timers.Timer" /> should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This property cannot be set because the timer has been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Timers.Timer.Interval" /> property was set to a value greater than <see cref="F:System.Int32.MaxValue" /> before the timer was enabled.</exception>
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x0002D83C File Offset: 0x0002BA3C
		// (set) Token: 0x06000A8F RID: 2703 RVA: 0x0002D844 File Offset: 0x0002BA44
		[DefaultValue(false)]
		[Category("Behavior")]
		[TimersDescription("Indicates whether the timer is enabled to fire events at a defined interval.")]
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				if (base.DesignMode)
				{
					this.delayedEnable = value;
					this.enabled = value;
					return;
				}
				if (this.initializing)
				{
					this.delayedEnable = value;
					return;
				}
				if (this.enabled != value)
				{
					if (!value)
					{
						if (this.timer != null)
						{
							this.cookie = null;
							this.timer.Dispose();
							this.timer = null;
						}
						this.enabled = value;
						return;
					}
					this.enabled = value;
					if (this.timer == null)
					{
						if (this.disposed)
						{
							throw new ObjectDisposedException(base.GetType().Name);
						}
						int num = Timer.CalculateRoundedInterval(this.interval, false);
						this.cookie = new object();
						this.timer = new Timer(this.callback, this.cookie, num, this.autoReset ? num : -1);
						return;
					}
					else
					{
						this.UpdateTimer();
					}
				}
			}
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0002D91C File Offset: 0x0002BB1C
		private static int CalculateRoundedInterval(double interval, bool argumentCheck = false)
		{
			double num = Math.Ceiling(interval);
			if (num <= 2147483647.0 && num > 0.0)
			{
				return (int)num;
			}
			if (argumentCheck)
			{
				throw new ArgumentException(SR.GetString("Invalid value '{1}' for parameter '{0}'.", new object[]
				{
					"interval",
					interval
				}));
			}
			throw new ArgumentOutOfRangeException(SR.GetString("Invalid value '{1}' for parameter '{0}'.", new object[]
			{
				"interval",
				interval
			}));
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0002D99C File Offset: 0x0002BB9C
		private void UpdateTimer()
		{
			int num = Timer.CalculateRoundedInterval(this.interval, false);
			this.timer.Change(num, this.autoReset ? num : -1);
		}

		/// <summary>Gets or sets the interval, expressed in milliseconds, at which to raise the <see cref="E:System.Timers.Timer.Elapsed" /> event.</summary>
		/// <returns>The time, in milliseconds, between <see cref="E:System.Timers.Timer.Elapsed" /> events. The value must be greater than zero, and less than or equal to <see cref="F:System.Int32.MaxValue" />. The default is 100 milliseconds.</returns>
		/// <exception cref="T:System.ArgumentException">The interval is less than or equal to zero.  
		///  -or-  
		///  The interval is greater than <see cref="F:System.Int32.MaxValue" />, and the timer is currently enabled. (If the timer is not currently enabled, no exception is thrown until it becomes enabled.)</exception>
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x0002D9CF File Offset: 0x0002BBCF
		// (set) Token: 0x06000A93 RID: 2707 RVA: 0x0002D9D8 File Offset: 0x0002BBD8
		[Category("Behavior")]
		[TimersDescription("The number of milliseconds between timer events.")]
		[DefaultValue(100.0)]
		[SettingsBindable(true)]
		public double Interval
		{
			get
			{
				return this.interval;
			}
			set
			{
				if (value <= 0.0)
				{
					throw new ArgumentException(SR.GetString("'{0}' is not a valid value for 'Interval'. 'Interval' must be greater than {1}.", new object[]
					{
						value,
						0
					}));
				}
				this.interval = value;
				if (this.timer != null)
				{
					this.UpdateTimer();
				}
			}
		}

		/// <summary>Occurs when the interval elapses.</summary>
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000A94 RID: 2708 RVA: 0x0002DA2E File Offset: 0x0002BC2E
		// (remove) Token: 0x06000A95 RID: 2709 RVA: 0x0002DA47 File Offset: 0x0002BC47
		[TimersDescription("Occurs when the Interval has elapsed.")]
		[Category("Behavior")]
		public event ElapsedEventHandler Elapsed
		{
			add
			{
				this.onIntervalElapsed = (ElapsedEventHandler)Delegate.Combine(this.onIntervalElapsed, value);
			}
			remove
			{
				this.onIntervalElapsed = (ElapsedEventHandler)Delegate.Remove(this.onIntervalElapsed, value);
			}
		}

		/// <summary>Gets or sets the site that binds the <see cref="T:System.Timers.Timer" /> to its container in design mode.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ISite" /> interface representing the site that binds the <see cref="T:System.Timers.Timer" /> object to its container.</returns>
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x0002DA78 File Offset: 0x0002BC78
		// (set) Token: 0x06000A96 RID: 2710 RVA: 0x0002DA60 File Offset: 0x0002BC60
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				base.Site = value;
				if (base.DesignMode)
				{
					this.enabled = true;
				}
			}
		}

		/// <summary>Gets or sets the object used to marshal event-handler calls that are issued when an interval has elapsed.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISynchronizeInvoke" /> representing the object used to marshal the event-handler calls that are issued when an interval has elapsed. The default is <see langword="null" />.</returns>
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x0002DA80 File Offset: 0x0002BC80
		// (set) Token: 0x06000A99 RID: 2713 RVA: 0x0002DADA File Offset: 0x0002BCDA
		[Browsable(false)]
		[DefaultValue(null)]
		[TimersDescription("The object used to marshal the event handler calls issued when an interval has elapsed.")]
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

		/// <summary>Begins the run-time initialization of a <see cref="T:System.Timers.Timer" /> that is used on a form or by another component.</summary>
		// Token: 0x06000A9A RID: 2714 RVA: 0x0002DAE3 File Offset: 0x0002BCE3
		public void BeginInit()
		{
			this.Close();
			this.initializing = true;
		}

		/// <summary>Releases the resources used by the <see cref="T:System.Timers.Timer" />.</summary>
		// Token: 0x06000A9B RID: 2715 RVA: 0x0002DAF2 File Offset: 0x0002BCF2
		public void Close()
		{
			this.initializing = false;
			this.delayedEnable = false;
			this.enabled = false;
			if (this.timer != null)
			{
				this.timer.Dispose();
				this.timer = null;
			}
		}

		/// <summary>Releases all resources used by the current <see cref="T:System.Timers.Timer" />.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06000A9C RID: 2716 RVA: 0x0002DB23 File Offset: 0x0002BD23
		protected override void Dispose(bool disposing)
		{
			this.Close();
			this.disposed = true;
			base.Dispose(disposing);
		}

		/// <summary>Ends the run-time initialization of a <see cref="T:System.Timers.Timer" /> that is used on a form or by another component.</summary>
		// Token: 0x06000A9D RID: 2717 RVA: 0x0002DB39 File Offset: 0x0002BD39
		public void EndInit()
		{
			this.initializing = false;
			this.Enabled = this.delayedEnable;
		}

		/// <summary>Starts raising the <see cref="E:System.Timers.Timer.Elapsed" /> event by setting <see cref="P:System.Timers.Timer.Enabled" /> to <see langword="true" />.</summary>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="T:System.Timers.Timer" /> is created with an interval equal to or greater than <see cref="F:System.Int32.MaxValue" /> + 1, or set to an interval less than zero.</exception>
		// Token: 0x06000A9E RID: 2718 RVA: 0x0002DB4E File Offset: 0x0002BD4E
		public void Start()
		{
			this.Enabled = true;
		}

		/// <summary>Stops raising the <see cref="E:System.Timers.Timer.Elapsed" /> event by setting <see cref="P:System.Timers.Timer.Enabled" /> to <see langword="false" />.</summary>
		// Token: 0x06000A9F RID: 2719 RVA: 0x0002DB57 File Offset: 0x0002BD57
		public void Stop()
		{
			this.Enabled = false;
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0002DB60 File Offset: 0x0002BD60
		private void MyTimerCallback(object state)
		{
			if (state != this.cookie)
			{
				return;
			}
			if (!this.autoReset)
			{
				this.enabled = false;
			}
			ElapsedEventArgs elapsedEventArgs = new ElapsedEventArgs(DateTime.Now);
			try
			{
				ElapsedEventHandler elapsedEventHandler = this.onIntervalElapsed;
				if (elapsedEventHandler != null)
				{
					if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
					{
						this.SynchronizingObject.BeginInvoke(elapsedEventHandler, new object[]
						{
							this,
							elapsedEventArgs
						});
					}
					else
					{
						elapsedEventHandler(this, elapsedEventArgs);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x04000715 RID: 1813
		private double interval;

		// Token: 0x04000716 RID: 1814
		private bool enabled;

		// Token: 0x04000717 RID: 1815
		private bool initializing;

		// Token: 0x04000718 RID: 1816
		private bool delayedEnable;

		// Token: 0x04000719 RID: 1817
		private ElapsedEventHandler onIntervalElapsed;

		// Token: 0x0400071A RID: 1818
		private bool autoReset;

		// Token: 0x0400071B RID: 1819
		private ISynchronizeInvoke synchronizingObject;

		// Token: 0x0400071C RID: 1820
		private bool disposed;

		// Token: 0x0400071D RID: 1821
		private Timer timer;

		// Token: 0x0400071E RID: 1822
		private TimerCallback callback;

		// Token: 0x0400071F RID: 1823
		private object cookie;
	}
}
