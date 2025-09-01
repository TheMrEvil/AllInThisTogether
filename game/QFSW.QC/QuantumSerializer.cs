using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using QFSW.QC.Utilities;

namespace QFSW.QC
{
	// Token: 0x0200003C RID: 60
	public class QuantumSerializer
	{
		// Token: 0x06000155 RID: 341 RVA: 0x0000746C File Offset: 0x0000566C
		public QuantumSerializer(IEnumerable<IQcSerializer> serializers)
		{
			this._recursiveSerializer = new Func<object, QuantumTheme, string>(this.SerializeFormatted);
			this._serializers = (from x in serializers
			orderby x.Priority descending
			select x).ToArray<IQcSerializer>();
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000074D7 File Offset: 0x000056D7
		public QuantumSerializer() : this(new InjectionLoader<IQcSerializer>().GetInjectedInstances(false))
		{
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000074EC File Offset: 0x000056EC
		public string SerializeFormatted(object value, QuantumTheme theme = null)
		{
			QuantumSerializer.<>c__DisplayClass6_0 CS$<>8__locals1;
			CS$<>8__locals1.value = value;
			CS$<>8__locals1.theme = theme;
			CS$<>8__locals1.<>4__this = this;
			if (CS$<>8__locals1.value == null)
			{
				return string.Empty;
			}
			CS$<>8__locals1.type = CS$<>8__locals1.value.GetType();
			string text = string.Empty;
			if (this._serializerLookup.ContainsKey(CS$<>8__locals1.type))
			{
				text = this.<SerializeFormatted>g__SerializeInternal|6_0(this._serializerLookup[CS$<>8__locals1.type], ref CS$<>8__locals1);
			}
			else if (this._unserializableLookup.Contains(CS$<>8__locals1.type))
			{
				text = CS$<>8__locals1.value.ToString();
			}
			else
			{
				bool flag = false;
				foreach (IQcSerializer qcSerializer in this._serializers)
				{
					if (qcSerializer.CanSerialize(CS$<>8__locals1.type))
					{
						text = this.<SerializeFormatted>g__SerializeInternal|6_0(qcSerializer, ref CS$<>8__locals1);
						this._serializerLookup[CS$<>8__locals1.type] = qcSerializer;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					text = CS$<>8__locals1.value.ToString();
					this._unserializableLookup.Add(CS$<>8__locals1.type);
				}
			}
			if (CS$<>8__locals1.theme && !string.IsNullOrWhiteSpace(text))
			{
				text = CS$<>8__locals1.theme.ColorizeReturn(text, CS$<>8__locals1.type);
			}
			return text;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00007628 File Offset: 0x00005828
		[CompilerGenerated]
		private string <SerializeFormatted>g__SerializeInternal|6_0(IQcSerializer serializer, ref QuantumSerializer.<>c__DisplayClass6_0 A_2)
		{
			string result;
			try
			{
				result = serializer.SerializeFormatted(A_2.value, A_2.theme, this._recursiveSerializer);
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("Serialization of {0} via {1} failed:\n{2}", A_2.type.GetDisplayName(false), serializer, ex.Message), ex);
			}
			return result;
		}

		// Token: 0x04000106 RID: 262
		private readonly IQcSerializer[] _serializers;

		// Token: 0x04000107 RID: 263
		private readonly Dictionary<Type, IQcSerializer> _serializerLookup = new Dictionary<Type, IQcSerializer>();

		// Token: 0x04000108 RID: 264
		private readonly HashSet<Type> _unserializableLookup = new HashSet<Type>();

		// Token: 0x04000109 RID: 265
		private readonly Func<object, QuantumTheme, string> _recursiveSerializer;

		// Token: 0x02000097 RID: 151
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060002F9 RID: 761 RVA: 0x0000BAE3 File Offset: 0x00009CE3
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060002FA RID: 762 RVA: 0x0000BAEF File Offset: 0x00009CEF
			public <>c()
			{
			}

			// Token: 0x060002FB RID: 763 RVA: 0x0000BAF7 File Offset: 0x00009CF7
			internal int <.ctor>b__4_0(IQcSerializer x)
			{
				return x.Priority;
			}

			// Token: 0x040001CE RID: 462
			public static readonly QuantumSerializer.<>c <>9 = new QuantumSerializer.<>c();

			// Token: 0x040001CF RID: 463
			public static Func<IQcSerializer, int> <>9__4_0;
		}

		// Token: 0x02000098 RID: 152
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <>c__DisplayClass6_0
		{
			// Token: 0x040001D0 RID: 464
			public object value;

			// Token: 0x040001D1 RID: 465
			public QuantumTheme theme;

			// Token: 0x040001D2 RID: 466
			public QuantumSerializer <>4__this;

			// Token: 0x040001D3 RID: 467
			public Type type;
		}
	}
}
