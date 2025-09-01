using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Stores sink provider data for sink providers.</summary>
	// Token: 0x020005C9 RID: 1481
	[ComVisible(true)]
	public class SinkProviderData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> class.</summary>
		/// <param name="name">The name of the sink provider that the data in the current <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> object is associated with.</param>
		// Token: 0x060038B4 RID: 14516 RVA: 0x000CA83C File Offset: 0x000C8A3C
		public SinkProviderData(string name)
		{
			this.sinkName = name;
			this.children = new ArrayList();
			this.properties = new Hashtable();
		}

		/// <summary>Gets a list of the child <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> nodes.</summary>
		/// <returns>A <see cref="T:System.Collections.IList" /> of the child <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> nodes.</returns>
		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x060038B5 RID: 14517 RVA: 0x000CA861 File Offset: 0x000C8A61
		public IList Children
		{
			get
			{
				return this.children;
			}
		}

		/// <summary>Gets the name of the sink provider that the data in the current <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> object is associated with.</summary>
		/// <returns>A <see cref="T:System.String" /> with the name of the XML node that the data in the current <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> object is associated with.</returns>
		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060038B6 RID: 14518 RVA: 0x000CA869 File Offset: 0x000C8A69
		public string Name
		{
			get
			{
				return this.sinkName;
			}
		}

		/// <summary>Gets a dictionary through which properties on the sink provider can be accessed.</summary>
		/// <returns>A dictionary through which properties on the sink provider can be accessed.</returns>
		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060038B7 RID: 14519 RVA: 0x000CA871 File Offset: 0x000C8A71
		public IDictionary Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x040025EE RID: 9710
		private string sinkName;

		// Token: 0x040025EF RID: 9711
		private ArrayList children;

		// Token: 0x040025F0 RID: 9712
		private Hashtable properties;
	}
}
