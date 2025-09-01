using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020006BB RID: 1723
	[Serializable]
	internal sealed class IntSizedArray : ICloneable
	{
		// Token: 0x06003FBA RID: 16314 RVA: 0x000DF6D8 File Offset: 0x000DD8D8
		public IntSizedArray()
		{
		}

		// Token: 0x06003FBB RID: 16315 RVA: 0x000DF6FC File Offset: 0x000DD8FC
		private IntSizedArray(IntSizedArray sizedArray)
		{
			this.objects = new int[sizedArray.objects.Length];
			sizedArray.objects.CopyTo(this.objects, 0);
			this.negObjects = new int[sizedArray.negObjects.Length];
			sizedArray.negObjects.CopyTo(this.negObjects, 0);
		}

		// Token: 0x06003FBC RID: 16316 RVA: 0x000DF772 File Offset: 0x000DD972
		public object Clone()
		{
			return new IntSizedArray(this);
		}

		// Token: 0x170009B7 RID: 2487
		internal int this[int index]
		{
			get
			{
				if (index < 0)
				{
					if (-index > this.negObjects.Length - 1)
					{
						return 0;
					}
					return this.negObjects[-index];
				}
				else
				{
					if (index > this.objects.Length - 1)
					{
						return 0;
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
				this.objects[index] = value;
			}
		}

		// Token: 0x06003FBF RID: 16319 RVA: 0x000DF804 File Offset: 0x000DDA04
		internal void IncreaseCapacity(int index)
		{
			try
			{
				if (index < 0)
				{
					int[] destinationArray = new int[Math.Max(this.negObjects.Length * 2, -index + 1)];
					Array.Copy(this.negObjects, 0, destinationArray, 0, this.negObjects.Length);
					this.negObjects = destinationArray;
				}
				else
				{
					int[] destinationArray2 = new int[Math.Max(this.objects.Length * 2, index + 1)];
					Array.Copy(this.objects, 0, destinationArray2, 0, this.objects.Length);
					this.objects = destinationArray2;
				}
			}
			catch (Exception)
			{
				throw new SerializationException(Environment.GetResourceString("Invalid BinaryFormatter stream."));
			}
		}

		// Token: 0x040029AF RID: 10671
		internal int[] objects = new int[16];

		// Token: 0x040029B0 RID: 10672
		internal int[] negObjects = new int[4];
	}
}
