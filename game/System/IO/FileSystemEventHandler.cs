using System;

namespace System.IO
{
	/// <summary>Represents the method that will handle the <see cref="E:System.IO.FileSystemWatcher.Changed" />, <see cref="E:System.IO.FileSystemWatcher.Created" />, or <see cref="E:System.IO.FileSystemWatcher.Deleted" /> event of a <see cref="T:System.IO.FileSystemWatcher" /> class.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The <see cref="T:System.IO.FileSystemEventArgs" /> that contains the event data.</param>
	// Token: 0x020004F7 RID: 1271
	// (Invoke) Token: 0x060029A0 RID: 10656
	public delegate void FileSystemEventHandler(object sender, FileSystemEventArgs e);
}
