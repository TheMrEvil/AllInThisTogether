using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Listens to the file system change notifications and raises events when a directory, or file in a directory, changes.</summary>
	// Token: 0x02000512 RID: 1298
	[DefaultEvent("Changed")]
	[IODescription("")]
	public class FileSystemWatcher : Component, ISupportInitialize
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemWatcher" /> class.</summary>
		// Token: 0x06002A0B RID: 10763 RVA: 0x00090B98 File Offset: 0x0008ED98
		public FileSystemWatcher()
		{
			this.notifyFilter = (NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite);
			this.enableRaisingEvents = false;
			this.filter = "*";
			this.includeSubdirectories = false;
			this.internalBufferSize = 8192;
			this.path = "";
			this.InitWatcher();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemWatcher" /> class, given the specified directory to monitor.</summary>
		/// <param name="path">The directory to monitor, in standard or Universal Naming Convention (UNC) notation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter is an empty string ("").  
		///  -or-  
		///  The path specified through the <paramref name="path" /> parameter does not exist.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">
		///   <paramref name="path" /> is too long.</exception>
		// Token: 0x06002A0C RID: 10764 RVA: 0x00090BE8 File Offset: 0x0008EDE8
		public FileSystemWatcher(string path) : this(path, "*")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemWatcher" /> class, given the specified directory and type of files to monitor.</summary>
		/// <param name="path">The directory to monitor, in standard or Universal Naming Convention (UNC) notation.</param>
		/// <param name="filter">The type of files to watch. For example, "*.txt" watches for changes to all text files.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="filter" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter is an empty string ("").  
		///  -or-  
		///  The path specified through the <paramref name="path" /> parameter does not exist.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">
		///   <paramref name="path" /> is too long.</exception>
		// Token: 0x06002A0D RID: 10765 RVA: 0x00090BF8 File Offset: 0x0008EDF8
		public FileSystemWatcher(string path, string filter)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			if (path == string.Empty)
			{
				throw new ArgumentException("Empty path", "path");
			}
			if (!Directory.Exists(path))
			{
				throw new ArgumentException("Directory does not exist", "path");
			}
			this.inited = false;
			this.start_requested = false;
			this.enableRaisingEvents = false;
			this.filter = filter;
			if (this.filter == "*.*")
			{
				this.filter = "*";
			}
			this.includeSubdirectories = false;
			this.internalBufferSize = 8192;
			this.notifyFilter = (NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite);
			this.path = path;
			this.synchronizingObject = null;
			this.InitWatcher();
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x00090CC4 File Offset: 0x0008EEC4
		[EnvironmentPermission(SecurityAction.Assert, Read = "MONO_MANAGED_WATCHER")]
		private void InitWatcher()
		{
			object obj = FileSystemWatcher.lockobj;
			lock (obj)
			{
				if (this.watcher_handle == null)
				{
					string environmentVariable = Environment.GetEnvironmentVariable("MONO_MANAGED_WATCHER");
					int num = 0;
					bool flag2 = false;
					if (environmentVariable == null)
					{
						num = FileSystemWatcher.InternalSupportsFSW();
					}
					switch (num)
					{
					case 1:
						flag2 = DefaultWatcher.GetInstance(out this.watcher);
						this.watcher_handle = this;
						break;
					case 2:
						flag2 = FAMWatcher.GetInstance(out this.watcher, false);
						this.watcher_handle = this;
						break;
					case 3:
						flag2 = KeventWatcher.GetInstance(out this.watcher);
						this.watcher_handle = this;
						break;
					case 4:
						flag2 = FAMWatcher.GetInstance(out this.watcher, true);
						this.watcher_handle = this;
						break;
					case 6:
						flag2 = CoreFXFileSystemWatcherProxy.GetInstance(out this.watcher);
						this.watcher_handle = (this.watcher as CoreFXFileSystemWatcherProxy).NewWatcher(this);
						break;
					}
					if (num == 0 || !flag2)
					{
						if (string.Compare(environmentVariable, "disabled", true) == 0)
						{
							NullFileWatcher.GetInstance(out this.watcher);
						}
						else
						{
							DefaultWatcher.GetInstance(out this.watcher);
							this.watcher_handle = this;
						}
					}
					this.inited = true;
				}
			}
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x00090E0C File Offset: 0x0008F00C
		[Conditional("DEBUG")]
		[Conditional("TRACE")]
		private void ShowWatcherInfo()
		{
			Console.WriteLine("Watcher implementation: {0}", (this.watcher != null) ? this.watcher.GetType().ToString() : "<none>");
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06002A10 RID: 10768 RVA: 0x00090E37 File Offset: 0x0008F037
		// (set) Token: 0x06002A11 RID: 10769 RVA: 0x00090E3F File Offset: 0x0008F03F
		internal bool Waiting
		{
			get
			{
				return this.waiting;
			}
			set
			{
				this.waiting = value;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06002A12 RID: 10770 RVA: 0x00090E48 File Offset: 0x0008F048
		internal string MangledFilter
		{
			get
			{
				if (this.filter != "*.*")
				{
					return this.filter;
				}
				if (this.mangledFilter != null)
				{
					return this.mangledFilter;
				}
				return "*.*";
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06002A13 RID: 10771 RVA: 0x00090E78 File Offset: 0x0008F078
		internal SearchPattern2 Pattern
		{
			get
			{
				if (this.pattern == null)
				{
					IFileWatcher fileWatcher = this.watcher;
					if (((fileWatcher != null) ? fileWatcher.GetType() : null) == typeof(KeventWatcher))
					{
						this.pattern = new SearchPattern2(this.MangledFilter, true);
					}
					else
					{
						this.pattern = new SearchPattern2(this.MangledFilter);
					}
				}
				return this.pattern;
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06002A14 RID: 10772 RVA: 0x00090EDC File Offset: 0x0008F0DC
		internal string FullPath
		{
			get
			{
				if (this.fullpath == null)
				{
					if (this.path == null || this.path == "")
					{
						this.fullpath = Environment.CurrentDirectory;
					}
					else
					{
						this.fullpath = System.IO.Path.GetFullPath(this.path);
					}
				}
				return this.fullpath;
			}
		}

		/// <summary>Gets or sets a value indicating whether the component is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the component is enabled; otherwise, <see langword="false" />. The default is <see langword="false" />. If you are using the component on a designer in Visual Studio 2005, the default is <see langword="true" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.FileSystemWatcher" /> object has been disposed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The directory specified in <see cref="P:System.IO.FileSystemWatcher.Path" /> could not be found.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.IO.FileSystemWatcher.Path" /> has not been set or is invalid.</exception>
		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06002A15 RID: 10773 RVA: 0x00090F2F File Offset: 0x0008F12F
		// (set) Token: 0x06002A16 RID: 10774 RVA: 0x00090F38 File Offset: 0x0008F138
		[IODescription("Flag to indicate if this instance is active")]
		[DefaultValue(false)]
		public bool EnableRaisingEvents
		{
			get
			{
				return this.enableRaisingEvents;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				this.start_requested = true;
				if (!this.inited)
				{
					return;
				}
				if (value == this.enableRaisingEvents)
				{
					return;
				}
				this.enableRaisingEvents = value;
				if (value)
				{
					this.Start();
					return;
				}
				this.Stop();
				this.start_requested = false;
			}
		}

		/// <summary>Gets or sets the filter string used to determine what files are monitored in a directory.</summary>
		/// <returns>The filter string. The default is "*.*" (Watches all files.)</returns>
		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06002A17 RID: 10775 RVA: 0x00090F96 File Offset: 0x0008F196
		// (set) Token: 0x06002A18 RID: 10776 RVA: 0x00090FA0 File Offset: 0x0008F1A0
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("*.*")]
		[IODescription("File name filter pattern")]
		[SettingsBindable(true)]
		public string Filter
		{
			get
			{
				return this.filter;
			}
			set
			{
				if (value == null || value == "")
				{
					value = "*";
				}
				if (!string.Equals(this.filter, value, PathInternal.StringComparison))
				{
					this.filter = ((value == "*.*") ? "*" : value);
					this.pattern = null;
					this.mangledFilter = null;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether subdirectories within the specified path should be monitored.</summary>
		/// <returns>
		///   <see langword="true" /> if you want to monitor subdirectories; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06002A19 RID: 10777 RVA: 0x00091000 File Offset: 0x0008F200
		// (set) Token: 0x06002A1A RID: 10778 RVA: 0x00091008 File Offset: 0x0008F208
		[DefaultValue(false)]
		[IODescription("Flag to indicate we want to watch subdirectories")]
		public bool IncludeSubdirectories
		{
			get
			{
				return this.includeSubdirectories;
			}
			set
			{
				if (this.includeSubdirectories == value)
				{
					return;
				}
				this.includeSubdirectories = value;
				if (value && this.enableRaisingEvents)
				{
					this.Stop();
					this.Start();
				}
			}
		}

		/// <summary>Gets or sets the size (in bytes) of the internal buffer.</summary>
		/// <returns>The internal buffer size in bytes. The default is 8192 (8 KB).</returns>
		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06002A1B RID: 10779 RVA: 0x00091032 File Offset: 0x0008F232
		// (set) Token: 0x06002A1C RID: 10780 RVA: 0x0009103A File Offset: 0x0008F23A
		[Browsable(false)]
		[DefaultValue(8192)]
		public int InternalBufferSize
		{
			get
			{
				return this.internalBufferSize;
			}
			set
			{
				if (this.internalBufferSize == value)
				{
					return;
				}
				if (value < 4096)
				{
					value = 4096;
				}
				this.internalBufferSize = value;
				if (this.enableRaisingEvents)
				{
					this.Stop();
					this.Start();
				}
			}
		}

		/// <summary>Gets or sets the type of changes to watch for.</summary>
		/// <returns>One of the <see cref="T:System.IO.NotifyFilters" /> values. The default is the bitwise OR combination of <see langword="LastWrite" />, <see langword="FileName" />, and <see langword="DirectoryName" />.</returns>
		/// <exception cref="T:System.ArgumentException">The value is not a valid bitwise OR combination of the <see cref="T:System.IO.NotifyFilters" /> values.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value that is being set is not valid.</exception>
		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06002A1D RID: 10781 RVA: 0x00091070 File Offset: 0x0008F270
		// (set) Token: 0x06002A1E RID: 10782 RVA: 0x00091078 File Offset: 0x0008F278
		[DefaultValue(NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite)]
		[IODescription("Flag to indicate which change event we want to monitor")]
		public NotifyFilters NotifyFilter
		{
			get
			{
				return this.notifyFilter;
			}
			set
			{
				if (this.notifyFilter == value)
				{
					return;
				}
				this.notifyFilter = value;
				if (this.enableRaisingEvents)
				{
					this.Stop();
					this.Start();
				}
			}
		}

		/// <summary>Gets or sets the path of the directory to watch.</summary>
		/// <returns>The path to monitor. The default is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentException">The specified path does not exist or could not be found.  
		///  -or-  
		///  The specified path contains wildcard characters.  
		///  -or-  
		///  The specified path contains invalid path characters.</exception>
		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06002A1F RID: 10783 RVA: 0x0009109F File Offset: 0x0008F29F
		// (set) Token: 0x06002A20 RID: 10784 RVA: 0x000910A8 File Offset: 0x0008F2A8
		[DefaultValue("")]
		[IODescription("The directory to monitor")]
		[SettingsBindable(true)]
		[Editor("System.Diagnostics.Design.FSWPathEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string Path
		{
			get
			{
				return this.path;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				value = ((value == null) ? string.Empty : value);
				if (string.Equals(this.path, value, PathInternal.StringComparison))
				{
					return;
				}
				bool flag = false;
				Exception ex = null;
				try
				{
					flag = Directory.Exists(value);
				}
				catch (Exception ex)
				{
				}
				if (ex != null)
				{
					throw new ArgumentException(SR.Format("The directory name {0} is invalid.", value), "Path");
				}
				if (!flag)
				{
					throw new ArgumentException(SR.Format("The directory name '{0}' does not exist.", value), "Path");
				}
				this.path = value;
				this.fullpath = null;
				if (this.enableRaisingEvents)
				{
					this.Stop();
					this.Start();
				}
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.IO.FileSystemWatcher" />.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.IO.FileSystemWatcher" />.</returns>
		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06002A21 RID: 10785 RVA: 0x0002DA78 File Offset: 0x0002BC78
		// (set) Token: 0x06002A22 RID: 10786 RVA: 0x00091164 File Offset: 0x0008F364
		[Browsable(false)]
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				base.Site = value;
				if (this.Site != null && this.Site.DesignMode)
				{
					this.EnableRaisingEvents = true;
				}
			}
		}

		/// <summary>Gets or sets the object used to marshal the event handler calls issued as a result of a directory change.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISynchronizeInvoke" /> that represents the object used to marshal the event handler calls issued as a result of a directory change. The default is <see langword="null" />.</returns>
		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06002A23 RID: 10787 RVA: 0x00091189 File Offset: 0x0008F389
		// (set) Token: 0x06002A24 RID: 10788 RVA: 0x00091191 File Offset: 0x0008F391
		[Browsable(false)]
		[IODescription("The object used to marshal the event handler calls resulting from a directory change")]
		[DefaultValue(null)]
		public ISynchronizeInvoke SynchronizingObject
		{
			get
			{
				return this.synchronizingObject;
			}
			set
			{
				this.synchronizingObject = value;
			}
		}

		/// <summary>Begins the initialization of a <see cref="T:System.IO.FileSystemWatcher" /> used on a form or used by another component. The initialization occurs at run time.</summary>
		// Token: 0x06002A25 RID: 10789 RVA: 0x0009119A File Offset: 0x0008F39A
		public void BeginInit()
		{
			this.inited = false;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.FileSystemWatcher" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002A26 RID: 10790 RVA: 0x000911A4 File Offset: 0x0008F3A4
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			try
			{
				IFileWatcher fileWatcher = this.watcher;
				if (fileWatcher != null)
				{
					fileWatcher.StopDispatching(this.watcher_handle);
				}
				IFileWatcher fileWatcher2 = this.watcher;
				if (fileWatcher2 != null)
				{
					fileWatcher2.Dispose(this.watcher_handle);
				}
			}
			catch (Exception)
			{
			}
			this.watcher_handle = null;
			this.watcher = null;
			this.disposed = true;
			base.Dispose(disposing);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x00091220 File Offset: 0x0008F420
		~FileSystemWatcher()
		{
			if (!this.disposed)
			{
				this.Dispose(false);
			}
		}

		/// <summary>Ends the initialization of a <see cref="T:System.IO.FileSystemWatcher" /> used on a form or used by another component. The initialization occurs at run time.</summary>
		// Token: 0x06002A28 RID: 10792 RVA: 0x00091258 File Offset: 0x0008F458
		public void EndInit()
		{
			this.inited = true;
			if (this.start_requested)
			{
				this.EnableRaisingEvents = true;
			}
		}

		// Token: 0x06002A29 RID: 10793 RVA: 0x00091270 File Offset: 0x0008F470
		private void RaiseEvent(Delegate ev, EventArgs arg, FileSystemWatcher.EventType evtype)
		{
			if (this.disposed)
			{
				return;
			}
			if (ev == null)
			{
				return;
			}
			if (this.synchronizingObject == null)
			{
				foreach (Delegate @delegate in ev.GetInvocationList())
				{
					switch (evtype)
					{
					case FileSystemWatcher.EventType.FileSystemEvent:
						((FileSystemEventHandler)@delegate)(this, (FileSystemEventArgs)arg);
						break;
					case FileSystemWatcher.EventType.ErrorEvent:
						((ErrorEventHandler)@delegate)(this, (ErrorEventArgs)arg);
						break;
					case FileSystemWatcher.EventType.RenameEvent:
						((RenamedEventHandler)@delegate)(this, (RenamedEventArgs)arg);
						break;
					}
				}
				return;
			}
			this.synchronizingObject.BeginInvoke(ev, new object[]
			{
				this,
				arg
			});
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Changed" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.FileSystemEventArgs" /> that contains the event data.</param>
		// Token: 0x06002A2A RID: 10794 RVA: 0x00091315 File Offset: 0x0008F515
		protected void OnChanged(FileSystemEventArgs e)
		{
			this.RaiseEvent(this.Changed, e, FileSystemWatcher.EventType.FileSystemEvent);
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Created" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.FileSystemEventArgs" /> that contains the event data.</param>
		// Token: 0x06002A2B RID: 10795 RVA: 0x00091325 File Offset: 0x0008F525
		protected void OnCreated(FileSystemEventArgs e)
		{
			this.RaiseEvent(this.Created, e, FileSystemWatcher.EventType.FileSystemEvent);
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Deleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.FileSystemEventArgs" /> that contains the event data.</param>
		// Token: 0x06002A2C RID: 10796 RVA: 0x00091335 File Offset: 0x0008F535
		protected void OnDeleted(FileSystemEventArgs e)
		{
			this.RaiseEvent(this.Deleted, e, FileSystemWatcher.EventType.FileSystemEvent);
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Error" /> event.</summary>
		/// <param name="e">An <see cref="T:System.IO.ErrorEventArgs" /> that contains the event data.</param>
		// Token: 0x06002A2D RID: 10797 RVA: 0x00091345 File Offset: 0x0008F545
		protected void OnError(ErrorEventArgs e)
		{
			this.RaiseEvent(this.Error, e, FileSystemWatcher.EventType.ErrorEvent);
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Renamed" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.RenamedEventArgs" /> that contains the event data.</param>
		// Token: 0x06002A2E RID: 10798 RVA: 0x00091355 File Offset: 0x0008F555
		protected void OnRenamed(RenamedEventArgs e)
		{
			this.RaiseEvent(this.Renamed, e, FileSystemWatcher.EventType.RenameEvent);
		}

		/// <summary>A synchronous method that returns a structure that contains specific information on the change that occurred, given the type of change you want to monitor.</summary>
		/// <param name="changeType">The <see cref="T:System.IO.WatcherChangeTypes" /> to watch for.</param>
		/// <returns>A <see cref="T:System.IO.WaitForChangedResult" /> that contains specific information on the change that occurred.</returns>
		// Token: 0x06002A2F RID: 10799 RVA: 0x00091365 File Offset: 0x0008F565
		public WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType)
		{
			return this.WaitForChanged(changeType, -1);
		}

		/// <summary>A synchronous method that returns a structure that contains specific information on the change that occurred, given the type of change you want to monitor and the time (in milliseconds) to wait before timing out.</summary>
		/// <param name="changeType">The <see cref="T:System.IO.WatcherChangeTypes" /> to watch for.</param>
		/// <param name="timeout">The time (in milliseconds) to wait before timing out.</param>
		/// <returns>A <see cref="T:System.IO.WaitForChangedResult" /> that contains specific information on the change that occurred.</returns>
		// Token: 0x06002A30 RID: 10800 RVA: 0x00091370 File Offset: 0x0008F570
		public WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType, int timeout)
		{
			WaitForChangedResult result = default(WaitForChangedResult);
			bool flag = this.EnableRaisingEvents;
			if (!flag)
			{
				this.EnableRaisingEvents = true;
			}
			bool flag3;
			lock (this)
			{
				this.waiting = true;
				flag3 = Monitor.Wait(this, timeout);
				if (flag3)
				{
					result = this.lastData;
				}
			}
			this.EnableRaisingEvents = flag;
			if (!flag3)
			{
				result.TimedOut = true;
			}
			return result;
		}

		// Token: 0x06002A31 RID: 10801 RVA: 0x000913EC File Offset: 0x0008F5EC
		internal void DispatchErrorEvents(ErrorEventArgs args)
		{
			if (this.disposed)
			{
				return;
			}
			this.OnError(args);
		}

		// Token: 0x06002A32 RID: 10802 RVA: 0x00091400 File Offset: 0x0008F600
		internal void DispatchEvents(FileAction act, string filename, ref RenamedEventArgs renamed)
		{
			FileSystemWatcher.<>c__DisplayClass70_0 CS$<>8__locals1 = new FileSystemWatcher.<>c__DisplayClass70_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.filename = filename;
			if (this.disposed)
			{
				return;
			}
			if (this.waiting)
			{
				this.lastData = default(WaitForChangedResult);
			}
			switch (act)
			{
			case FileAction.Added:
				this.lastData.Name = CS$<>8__locals1.filename;
				this.lastData.ChangeType = WatcherChangeTypes.Created;
				Task.Run(delegate()
				{
					CS$<>8__locals1.<>4__this.OnCreated(new FileSystemEventArgs(WatcherChangeTypes.Created, CS$<>8__locals1.<>4__this.path, CS$<>8__locals1.filename));
				});
				return;
			case FileAction.Removed:
				this.lastData.Name = CS$<>8__locals1.filename;
				this.lastData.ChangeType = WatcherChangeTypes.Deleted;
				Task.Run(delegate()
				{
					CS$<>8__locals1.<>4__this.OnDeleted(new FileSystemEventArgs(WatcherChangeTypes.Deleted, CS$<>8__locals1.<>4__this.path, CS$<>8__locals1.filename));
				});
				return;
			case FileAction.Modified:
				this.lastData.Name = CS$<>8__locals1.filename;
				this.lastData.ChangeType = WatcherChangeTypes.Changed;
				Task.Run(delegate()
				{
					CS$<>8__locals1.<>4__this.OnChanged(new FileSystemEventArgs(WatcherChangeTypes.Changed, CS$<>8__locals1.<>4__this.path, CS$<>8__locals1.filename));
				});
				return;
			case FileAction.RenamedOldName:
				if (renamed != null)
				{
					this.OnRenamed(renamed);
				}
				this.lastData.OldName = CS$<>8__locals1.filename;
				this.lastData.ChangeType = WatcherChangeTypes.Renamed;
				renamed = new RenamedEventArgs(WatcherChangeTypes.Renamed, this.path, CS$<>8__locals1.filename, "");
				return;
			case FileAction.RenamedNewName:
			{
				this.lastData.Name = CS$<>8__locals1.filename;
				this.lastData.ChangeType = WatcherChangeTypes.Renamed;
				if (renamed == null)
				{
					renamed = new RenamedEventArgs(WatcherChangeTypes.Renamed, this.path, "", CS$<>8__locals1.filename);
				}
				RenamedEventArgs renamed_ref = renamed;
				Task.Run(delegate()
				{
					CS$<>8__locals1.<>4__this.OnRenamed(renamed_ref);
				});
				renamed = null;
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06002A33 RID: 10803 RVA: 0x000915C4 File Offset: 0x0008F7C4
		private void Start()
		{
			if (this.disposed)
			{
				return;
			}
			if (this.watcher_handle == null)
			{
				return;
			}
			IFileWatcher fileWatcher = this.watcher;
			if (fileWatcher == null)
			{
				return;
			}
			fileWatcher.StartDispatching(this.watcher_handle);
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x000915EE File Offset: 0x0008F7EE
		private void Stop()
		{
			if (this.disposed)
			{
				return;
			}
			if (this.watcher_handle == null)
			{
				return;
			}
			IFileWatcher fileWatcher = this.watcher;
			if (fileWatcher == null)
			{
				return;
			}
			fileWatcher.StopDispatching(this.watcher_handle);
		}

		/// <summary>Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path" /> is changed.</summary>
		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06002A35 RID: 10805 RVA: 0x00091618 File Offset: 0x0008F818
		// (remove) Token: 0x06002A36 RID: 10806 RVA: 0x00091650 File Offset: 0x0008F850
		[IODescription("Occurs when a file/directory change matches the filter")]
		public event FileSystemEventHandler Changed
		{
			[CompilerGenerated]
			add
			{
				FileSystemEventHandler fileSystemEventHandler = this.Changed;
				FileSystemEventHandler fileSystemEventHandler2;
				do
				{
					fileSystemEventHandler2 = fileSystemEventHandler;
					FileSystemEventHandler value2 = (FileSystemEventHandler)Delegate.Combine(fileSystemEventHandler2, value);
					fileSystemEventHandler = Interlocked.CompareExchange<FileSystemEventHandler>(ref this.Changed, value2, fileSystemEventHandler2);
				}
				while (fileSystemEventHandler != fileSystemEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				FileSystemEventHandler fileSystemEventHandler = this.Changed;
				FileSystemEventHandler fileSystemEventHandler2;
				do
				{
					fileSystemEventHandler2 = fileSystemEventHandler;
					FileSystemEventHandler value2 = (FileSystemEventHandler)Delegate.Remove(fileSystemEventHandler2, value);
					fileSystemEventHandler = Interlocked.CompareExchange<FileSystemEventHandler>(ref this.Changed, value2, fileSystemEventHandler2);
				}
				while (fileSystemEventHandler != fileSystemEventHandler2);
			}
		}

		/// <summary>Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path" /> is created.</summary>
		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06002A37 RID: 10807 RVA: 0x00091688 File Offset: 0x0008F888
		// (remove) Token: 0x06002A38 RID: 10808 RVA: 0x000916C0 File Offset: 0x0008F8C0
		[IODescription("Occurs when a file/directory creation matches the filter")]
		public event FileSystemEventHandler Created
		{
			[CompilerGenerated]
			add
			{
				FileSystemEventHandler fileSystemEventHandler = this.Created;
				FileSystemEventHandler fileSystemEventHandler2;
				do
				{
					fileSystemEventHandler2 = fileSystemEventHandler;
					FileSystemEventHandler value2 = (FileSystemEventHandler)Delegate.Combine(fileSystemEventHandler2, value);
					fileSystemEventHandler = Interlocked.CompareExchange<FileSystemEventHandler>(ref this.Created, value2, fileSystemEventHandler2);
				}
				while (fileSystemEventHandler != fileSystemEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				FileSystemEventHandler fileSystemEventHandler = this.Created;
				FileSystemEventHandler fileSystemEventHandler2;
				do
				{
					fileSystemEventHandler2 = fileSystemEventHandler;
					FileSystemEventHandler value2 = (FileSystemEventHandler)Delegate.Remove(fileSystemEventHandler2, value);
					fileSystemEventHandler = Interlocked.CompareExchange<FileSystemEventHandler>(ref this.Created, value2, fileSystemEventHandler2);
				}
				while (fileSystemEventHandler != fileSystemEventHandler2);
			}
		}

		/// <summary>Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path" /> is deleted.</summary>
		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06002A39 RID: 10809 RVA: 0x000916F8 File Offset: 0x0008F8F8
		// (remove) Token: 0x06002A3A RID: 10810 RVA: 0x00091730 File Offset: 0x0008F930
		[IODescription("Occurs when a file/directory deletion matches the filter")]
		public event FileSystemEventHandler Deleted
		{
			[CompilerGenerated]
			add
			{
				FileSystemEventHandler fileSystemEventHandler = this.Deleted;
				FileSystemEventHandler fileSystemEventHandler2;
				do
				{
					fileSystemEventHandler2 = fileSystemEventHandler;
					FileSystemEventHandler value2 = (FileSystemEventHandler)Delegate.Combine(fileSystemEventHandler2, value);
					fileSystemEventHandler = Interlocked.CompareExchange<FileSystemEventHandler>(ref this.Deleted, value2, fileSystemEventHandler2);
				}
				while (fileSystemEventHandler != fileSystemEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				FileSystemEventHandler fileSystemEventHandler = this.Deleted;
				FileSystemEventHandler fileSystemEventHandler2;
				do
				{
					fileSystemEventHandler2 = fileSystemEventHandler;
					FileSystemEventHandler value2 = (FileSystemEventHandler)Delegate.Remove(fileSystemEventHandler2, value);
					fileSystemEventHandler = Interlocked.CompareExchange<FileSystemEventHandler>(ref this.Deleted, value2, fileSystemEventHandler2);
				}
				while (fileSystemEventHandler != fileSystemEventHandler2);
			}
		}

		/// <summary>Occurs when the instance of <see cref="T:System.IO.FileSystemWatcher" /> is unable to continue monitoring changes or when the internal buffer overflows.</summary>
		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06002A3B RID: 10811 RVA: 0x00091768 File Offset: 0x0008F968
		// (remove) Token: 0x06002A3C RID: 10812 RVA: 0x000917A0 File Offset: 0x0008F9A0
		[Browsable(false)]
		public event ErrorEventHandler Error
		{
			[CompilerGenerated]
			add
			{
				ErrorEventHandler errorEventHandler = this.Error;
				ErrorEventHandler errorEventHandler2;
				do
				{
					errorEventHandler2 = errorEventHandler;
					ErrorEventHandler value2 = (ErrorEventHandler)Delegate.Combine(errorEventHandler2, value);
					errorEventHandler = Interlocked.CompareExchange<ErrorEventHandler>(ref this.Error, value2, errorEventHandler2);
				}
				while (errorEventHandler != errorEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				ErrorEventHandler errorEventHandler = this.Error;
				ErrorEventHandler errorEventHandler2;
				do
				{
					errorEventHandler2 = errorEventHandler;
					ErrorEventHandler value2 = (ErrorEventHandler)Delegate.Remove(errorEventHandler2, value);
					errorEventHandler = Interlocked.CompareExchange<ErrorEventHandler>(ref this.Error, value2, errorEventHandler2);
				}
				while (errorEventHandler != errorEventHandler2);
			}
		}

		/// <summary>Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path" /> is renamed.</summary>
		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06002A3D RID: 10813 RVA: 0x000917D8 File Offset: 0x0008F9D8
		// (remove) Token: 0x06002A3E RID: 10814 RVA: 0x00091810 File Offset: 0x0008FA10
		[IODescription("Occurs when a file/directory rename matches the filter")]
		public event RenamedEventHandler Renamed
		{
			[CompilerGenerated]
			add
			{
				RenamedEventHandler renamedEventHandler = this.Renamed;
				RenamedEventHandler renamedEventHandler2;
				do
				{
					renamedEventHandler2 = renamedEventHandler;
					RenamedEventHandler value2 = (RenamedEventHandler)Delegate.Combine(renamedEventHandler2, value);
					renamedEventHandler = Interlocked.CompareExchange<RenamedEventHandler>(ref this.Renamed, value2, renamedEventHandler2);
				}
				while (renamedEventHandler != renamedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				RenamedEventHandler renamedEventHandler = this.Renamed;
				RenamedEventHandler renamedEventHandler2;
				do
				{
					renamedEventHandler2 = renamedEventHandler;
					RenamedEventHandler value2 = (RenamedEventHandler)Delegate.Remove(renamedEventHandler2, value);
					renamedEventHandler = Interlocked.CompareExchange<RenamedEventHandler>(ref this.Renamed, value2, renamedEventHandler2);
				}
				while (renamedEventHandler != renamedEventHandler2);
			}
		}

		// Token: 0x06002A3F RID: 10815
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int InternalSupportsFSW();

		// Token: 0x06002A40 RID: 10816 RVA: 0x00091845 File Offset: 0x0008FA45
		// Note: this type is marked as 'beforefieldinit'.
		static FileSystemWatcher()
		{
		}

		// Token: 0x0400165B RID: 5723
		private bool inited;

		// Token: 0x0400165C RID: 5724
		private bool start_requested;

		// Token: 0x0400165D RID: 5725
		private bool enableRaisingEvents;

		// Token: 0x0400165E RID: 5726
		private string filter;

		// Token: 0x0400165F RID: 5727
		private bool includeSubdirectories;

		// Token: 0x04001660 RID: 5728
		private int internalBufferSize;

		// Token: 0x04001661 RID: 5729
		private NotifyFilters notifyFilter;

		// Token: 0x04001662 RID: 5730
		private string path;

		// Token: 0x04001663 RID: 5731
		private string fullpath;

		// Token: 0x04001664 RID: 5732
		private ISynchronizeInvoke synchronizingObject;

		// Token: 0x04001665 RID: 5733
		private WaitForChangedResult lastData;

		// Token: 0x04001666 RID: 5734
		private bool waiting;

		// Token: 0x04001667 RID: 5735
		private SearchPattern2 pattern;

		// Token: 0x04001668 RID: 5736
		private bool disposed;

		// Token: 0x04001669 RID: 5737
		private string mangledFilter;

		// Token: 0x0400166A RID: 5738
		private IFileWatcher watcher;

		// Token: 0x0400166B RID: 5739
		private object watcher_handle;

		// Token: 0x0400166C RID: 5740
		private static object lockobj = new object();

		// Token: 0x0400166D RID: 5741
		[CompilerGenerated]
		private FileSystemEventHandler Changed;

		// Token: 0x0400166E RID: 5742
		[CompilerGenerated]
		private FileSystemEventHandler Created;

		// Token: 0x0400166F RID: 5743
		[CompilerGenerated]
		private FileSystemEventHandler Deleted;

		// Token: 0x04001670 RID: 5744
		[CompilerGenerated]
		private ErrorEventHandler Error;

		// Token: 0x04001671 RID: 5745
		[CompilerGenerated]
		private RenamedEventHandler Renamed;

		// Token: 0x02000513 RID: 1299
		private enum EventType
		{
			// Token: 0x04001673 RID: 5747
			FileSystemEvent,
			// Token: 0x04001674 RID: 5748
			ErrorEvent,
			// Token: 0x04001675 RID: 5749
			RenameEvent
		}

		// Token: 0x02000514 RID: 1300
		[CompilerGenerated]
		private sealed class <>c__DisplayClass70_0
		{
			// Token: 0x06002A41 RID: 10817 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass70_0()
			{
			}

			// Token: 0x06002A42 RID: 10818 RVA: 0x00091851 File Offset: 0x0008FA51
			internal void <DispatchEvents>b__0()
			{
				this.<>4__this.OnCreated(new FileSystemEventArgs(WatcherChangeTypes.Created, this.<>4__this.path, this.filename));
			}

			// Token: 0x06002A43 RID: 10819 RVA: 0x00091875 File Offset: 0x0008FA75
			internal void <DispatchEvents>b__1()
			{
				this.<>4__this.OnDeleted(new FileSystemEventArgs(WatcherChangeTypes.Deleted, this.<>4__this.path, this.filename));
			}

			// Token: 0x06002A44 RID: 10820 RVA: 0x00091899 File Offset: 0x0008FA99
			internal void <DispatchEvents>b__2()
			{
				this.<>4__this.OnChanged(new FileSystemEventArgs(WatcherChangeTypes.Changed, this.<>4__this.path, this.filename));
			}

			// Token: 0x04001676 RID: 5750
			public FileSystemWatcher <>4__this;

			// Token: 0x04001677 RID: 5751
			public string filename;
		}

		// Token: 0x02000515 RID: 1301
		[CompilerGenerated]
		private sealed class <>c__DisplayClass70_1
		{
			// Token: 0x06002A45 RID: 10821 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass70_1()
			{
			}

			// Token: 0x06002A46 RID: 10822 RVA: 0x000918BD File Offset: 0x0008FABD
			internal void <DispatchEvents>b__3()
			{
				this.CS$<>8__locals1.<>4__this.OnRenamed(this.renamed_ref);
			}

			// Token: 0x04001678 RID: 5752
			public RenamedEventArgs renamed_ref;

			// Token: 0x04001679 RID: 5753
			public FileSystemWatcher.<>c__DisplayClass70_0 CS$<>8__locals1;
		}
	}
}
