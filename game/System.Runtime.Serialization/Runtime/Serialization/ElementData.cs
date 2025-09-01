using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020000E0 RID: 224
	internal class ElementData
	{
		// Token: 0x06000CCD RID: 3277 RVA: 0x00034570 File Offset: 0x00032770
		public void AddAttribute(string prefix, string ns, string name, string value)
		{
			this.GrowAttributesIfNeeded();
			AttributeData attributeData = this.attributes[this.attributeCount];
			if (attributeData == null)
			{
				attributeData = (this.attributes[this.attributeCount] = new AttributeData());
			}
			attributeData.prefix = prefix;
			attributeData.ns = ns;
			attributeData.localName = name;
			attributeData.value = value;
			this.attributeCount++;
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x000345D4 File Offset: 0x000327D4
		private void GrowAttributesIfNeeded()
		{
			if (this.attributes == null)
			{
				this.attributes = new AttributeData[4];
				return;
			}
			if (this.attributes.Length == this.attributeCount)
			{
				AttributeData[] destinationArray = new AttributeData[this.attributes.Length * 2];
				Array.Copy(this.attributes, 0, destinationArray, 0, this.attributes.Length);
				this.attributes = destinationArray;
			}
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0000222F File Offset: 0x0000042F
		public ElementData()
		{
		}

		// Token: 0x04000552 RID: 1362
		public string localName;

		// Token: 0x04000553 RID: 1363
		public string ns;

		// Token: 0x04000554 RID: 1364
		public string prefix;

		// Token: 0x04000555 RID: 1365
		public int attributeCount;

		// Token: 0x04000556 RID: 1366
		public AttributeData[] attributes;

		// Token: 0x04000557 RID: 1367
		public IDataNode dataNode;

		// Token: 0x04000558 RID: 1368
		public int childElementIndex;
	}
}
