using System;

namespace System.IO
{
	/// <summary>Provides data for the <see cref="E:System.IO.FileSystemWatcher.Renamed" /> event.</summary>
	// Token: 0x020004F8 RID: 1272
	public class RenamedEventArgs : FileSystemEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.RenamedEventArgs" /> class.</summary>
		/// <param name="changeType">One of the <see cref="T:System.IO.WatcherChangeTypes" /> values.</param>
		/// <param name="directory">The name of the affected file or directory.</param>
		/// <param name="name">The name of the affected file or directory.</param>
		/// <param name="oldName">The old name of the affected file or directory.</param>
		// Token: 0x060029A3 RID: 10659 RVA: 0x0008F1C0 File Offset: 0x0008D3C0
		public RenamedEventArgs(WatcherChangeTypes changeType, string directory, string name, string oldName) : base(changeType, directory, name)
		{
			this._oldName = oldName;
			this._oldFullPath = FileSystemEventArgs.Combine(directory, oldName);
		}

		/// <summary>Gets the previous fully qualified path of the affected file or directory.</summary>
		/// <returns>The previous fully qualified path of the affected file or directory.</returns>
		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x060029A4 RID: 10660 RVA: 0x0008F1E1 File Offset: 0x0008D3E1
		public string OldFullPath
		{
			get
			{
				return this._oldFullPath;
			}
		}

		/// <summary>Gets the old name of the affected file or directory.</summary>
		/// <returns>The previous name of the affected file or directory.</returns>
		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x060029A5 RID: 10661 RVA: 0x0008F1E9 File Offset: 0x0008D3E9
		public string OldName
		{
			get
			{
				return this._oldName;
			}
		}

		// Token: 0x040015F8 RID: 5624
		private readonly string _oldName;

		// Token: 0x040015F9 RID: 5625
		private readonly string _oldFullPath;
	}
}
