using System;

namespace System.Xml
{
	// Token: 0x020001C4 RID: 452
	internal class XmlElementListListener
	{
		// Token: 0x06001158 RID: 4440 RVA: 0x0006A8B8 File Offset: 0x00068AB8
		internal XmlElementListListener(XmlDocument doc, XmlElementList elemList)
		{
			this.doc = doc;
			this.elemList = new WeakReference(elemList);
			this.nodeChangeHandler = new XmlNodeChangedEventHandler(this.OnListChanged);
			doc.NodeInserted += this.nodeChangeHandler;
			doc.NodeRemoved += this.nodeChangeHandler;
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x0006A908 File Offset: 0x00068B08
		private void OnListChanged(object sender, XmlNodeChangedEventArgs args)
		{
			lock (this)
			{
				if (this.elemList != null)
				{
					XmlElementList xmlElementList = (XmlElementList)this.elemList.Target;
					if (xmlElementList != null)
					{
						xmlElementList.ConcurrencyCheck(args);
					}
					else
					{
						this.doc.NodeInserted -= this.nodeChangeHandler;
						this.doc.NodeRemoved -= this.nodeChangeHandler;
						this.elemList = null;
					}
				}
			}
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x0006A98C File Offset: 0x00068B8C
		internal void Unregister()
		{
			lock (this)
			{
				if (this.elemList != null)
				{
					this.doc.NodeInserted -= this.nodeChangeHandler;
					this.doc.NodeRemoved -= this.nodeChangeHandler;
					this.elemList = null;
				}
			}
		}

		// Token: 0x0400108E RID: 4238
		private WeakReference elemList;

		// Token: 0x0400108F RID: 4239
		private XmlDocument doc;

		// Token: 0x04001090 RID: 4240
		private XmlNodeChangedEventHandler nodeChangeHandler;
	}
}
