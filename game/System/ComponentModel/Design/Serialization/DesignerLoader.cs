using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides a basic designer loader interface that can be used to implement a custom designer loader.</summary>
	// Token: 0x02000483 RID: 1155
	public abstract class DesignerLoader
	{
		/// <summary>Gets a value indicating whether the loader is currently loading a document.</summary>
		/// <returns>
		///   <see langword="true" /> if the loader is currently loading a document; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x0600250A RID: 9482 RVA: 0x00003062 File Offset: 0x00001262
		public virtual bool Loading
		{
			get
			{
				return false;
			}
		}

		/// <summary>Begins loading a designer.</summary>
		/// <param name="host">The loader host through which this loader loads components.</param>
		// Token: 0x0600250B RID: 9483
		public abstract void BeginLoad(IDesignerLoaderHost host);

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.Serialization.DesignerLoader" />.</summary>
		// Token: 0x0600250C RID: 9484
		public abstract void Dispose();

		/// <summary>Writes cached changes to the location that the designer was loaded from.</summary>
		// Token: 0x0600250D RID: 9485 RVA: 0x00003917 File Offset: 0x00001B17
		public virtual void Flush()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DesignerLoader" /> class.</summary>
		// Token: 0x0600250E RID: 9486 RVA: 0x0000219B File Offset: 0x0000039B
		protected DesignerLoader()
		{
		}
	}
}
