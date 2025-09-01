using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020006BA RID: 1722
	[Serializable]
	internal sealed class SizedArray : ICloneable
	{
		// Token: 0x06003FB3 RID: 16307 RVA: 0x000DF4FE File Offset: 0x000DD6FE
		internal SizedArray()
		{
			this.objects = new object[16];
			this.negObjects = new object[4];
		}

		// Token: 0x06003FB4 RID: 16308 RVA: 0x000DF51F File Offset: 0x000DD71F
		internal SizedArray(int length)
		{
			this.objects = new object[length];
			this.negObjects = new object[length];
		}

		// Token: 0x06003FB5 RID: 16309 RVA: 0x000DF540 File Offset: 0x000DD740
		private SizedArray(SizedArray sizedArray)
		{
			this.objects = new object[sizedArray.objects.Length];
			sizedArray.objects.CopyTo(this.objects, 0);
			this.negObjects = new object[sizedArray.negObjects.Length];
			sizedArray.negObjects.CopyTo(this.negObjects, 0);
		}

		// Token: 0x06003FB6 RID: 16310 RVA: 0x000DF59D File Offset: 0x000DD79D
		public object Clone()
		{
			return new SizedArray(this);
		}

		// Token: 0x170009B6 RID: 2486
		internal object this[int index]
		{
			get
			{
				if (index < 0)
				{
					if (-index > this.negObjects.Length - 1)
					{
						return null;
					}
					return this.negObjects[-index];
				}
				else
				{
					if (index > this.objects.Length - 1)
					{
						return null;
					}
					return this.objects[index];
				}
			}
			set
			{
				if (index < 0)
				{
					if (-index > this.negObjects.Length - 1)
					{
						this.IncreaseCapacity(index);
					}
					this.negObjects[-index] = value;
					return;
				}
				if (index > this.objects.Length - 1)
				{
					this.IncreaseCapacity(index);
				}
				object obj = this.objects[index];
				this.objects[index] = value;
			}
		}

		// Token: 0x06003FB9 RID: 16313 RVA: 0x000DF634 File Offset: 0x000DD834
		internal void IncreaseCapacity(int index)
		{
			try
			{
				if (index < 0)
				{
					object[] destinationArray = new object[Math.Max(this.negObjects.Length * 2, -index + 1)];
					Array.Copy(this.negObjects, 0, destinationArray, 0, this.negObjects.Length);
					this.negObjects = destinationArray;
				}
				else
				{
					object[] destinationArray2 = new object[Math.Max(this.objects.Length * 2, index + 1)];
					Array.Copy(this.objects, 0, destinationArray2, 0, this.objects.Length);
					this.objects = destinationArray2;
				}
			}
			catch (Exception)
			{
				throw new SerializationException(Environment.GetResourceString("Invalid BinaryFormatter stream."));
			}
		}

		// Token: 0x040029AD RID: 10669
		internal object[] objects;

		// Token: 0x040029AE RID: 10670
		internal object[] negObjects;
	}
}
