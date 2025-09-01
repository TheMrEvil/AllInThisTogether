using System;
using System.Collections;
using System.Text;
using QFSW.QC.Pooling;

namespace QFSW.QC.Serializers
{
	// Token: 0x02000005 RID: 5
	public abstract class IEnumerableSerializer<T> : PolymorphicQcSerializer<T> where T : class, IEnumerable
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000020C0 File Offset: 0x000002C0
		public override string SerializeFormatted(T value, QuantumTheme theme)
		{
			Type type = value.GetType();
			StringBuilder stringBuilder = this._builderPool.GetStringBuilder(0);
			string value2 = "[";
			string value3 = ",";
			string value4 = "]";
			if (theme)
			{
				theme.GetCollectionFormatting(type, out value2, out value3, out value4);
			}
			stringBuilder.Append(value2);
			bool flag = true;
			foreach (object value5 in this.GetObjectStream(value))
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(value3);
				}
				stringBuilder.Append(base.SerializeRecursive(value5, theme));
			}
			stringBuilder.Append(value4);
			return this._builderPool.ReleaseAndToString(stringBuilder);
		}

		// Token: 0x06000009 RID: 9
		protected abstract IEnumerable GetObjectStream(T value);

		// Token: 0x0600000A RID: 10 RVA: 0x00002198 File Offset: 0x00000398
		protected IEnumerableSerializer()
		{
		}

		// Token: 0x04000001 RID: 1
		private readonly StringBuilderPool _builderPool = new StringBuilderPool();
	}
}
