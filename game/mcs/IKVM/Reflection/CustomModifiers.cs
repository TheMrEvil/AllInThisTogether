using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Reader;

namespace IKVM.Reflection
{
	// Token: 0x02000010 RID: 16
	public struct CustomModifiers : IEquatable<CustomModifiers>, IEnumerable<CustomModifiers.Entry>, IEnumerable
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00004FB1 File Offset: 0x000031B1
		private static Type Initial
		{
			get
			{
				return MarkerType.ModOpt;
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004FB8 File Offset: 0x000031B8
		internal CustomModifiers(List<CustomModifiersBuilder.Item> list)
		{
			bool flag = CustomModifiers.Initial == MarkerType.ModReq;
			int num = list.Count;
			foreach (CustomModifiersBuilder.Item item in list)
			{
				if (item.required != flag)
				{
					flag = item.required;
					num++;
				}
			}
			this.types = new Type[num];
			flag = (CustomModifiers.Initial == MarkerType.ModReq);
			int num2 = 0;
			foreach (CustomModifiersBuilder.Item item2 in list)
			{
				if (item2.required != flag)
				{
					flag = item2.required;
					this.types[num2++] = (flag ? MarkerType.ModReq : MarkerType.ModOpt);
				}
				this.types[num2++] = item2.type;
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000050C4 File Offset: 0x000032C4
		private CustomModifiers(Type[] types)
		{
			this.types = types;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000050CD File Offset: 0x000032CD
		public CustomModifiers.Enumerator GetEnumerator()
		{
			return new CustomModifiers.Enumerator(this.types);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000050DA File Offset: 0x000032DA
		IEnumerator<CustomModifiers.Entry> IEnumerable<CustomModifiers.Entry>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000050DA File Offset: 0x000032DA
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x000050E7 File Offset: 0x000032E7
		public bool IsEmpty
		{
			get
			{
				return this.types == null;
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000050F2 File Offset: 0x000032F2
		public bool Equals(CustomModifiers other)
		{
			return Util.ArrayEquals(this.types, other.types);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005108 File Offset: 0x00003308
		public override bool Equals(object obj)
		{
			CustomModifiers? customModifiers = obj as CustomModifiers?;
			return customModifiers != null && this.Equals(customModifiers.Value);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005139 File Offset: 0x00003339
		public override int GetHashCode()
		{
			return Util.GetHashCode(this.types);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005148 File Offset: 0x00003348
		public override string ToString()
		{
			if (this.types == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			string value = "";
			foreach (CustomModifiers.Entry entry in this)
			{
				stringBuilder.Append(value).Append(entry.IsRequired ? "modreq(" : "modopt(").Append(entry.Type.FullName).Append(')');
				value = " ";
			}
			return stringBuilder.ToString();
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000EE RID: 238 RVA: 0x000051F0 File Offset: 0x000033F0
		public bool ContainsMissingType
		{
			get
			{
				return Type.ContainsMissingType(this.types);
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005200 File Offset: 0x00003400
		private Type[] GetRequiredOrOptional(bool required)
		{
			if (this.types == null)
			{
				return Type.EmptyTypes;
			}
			int num = 0;
			foreach (CustomModifiers.Entry entry in this)
			{
				if (entry.IsRequired == required)
				{
					num++;
				}
			}
			Type[] array = new Type[num];
			foreach (CustomModifiers.Entry entry2 in this)
			{
				if (entry2.IsRequired == required)
				{
					array[--num] = entry2.Type;
				}
			}
			return array;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000052BC File Offset: 0x000034BC
		internal Type[] GetRequired()
		{
			return this.GetRequiredOrOptional(true);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000052C5 File Offset: 0x000034C5
		internal Type[] GetOptional()
		{
			return this.GetRequiredOrOptional(false);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000052D0 File Offset: 0x000034D0
		internal CustomModifiers Bind(IGenericBinder binder)
		{
			if (this.types == null)
			{
				return this;
			}
			Type[] array = this.types;
			for (int i = 0; i < this.types.Length; i++)
			{
				if (!(this.types[i] == MarkerType.ModOpt) && !(this.types[i] == MarkerType.ModReq))
				{
					Type type = this.types[i].BindTypeParameters(binder);
					if (type != this.types[i])
					{
						if (array == this.types)
						{
							array = (Type[])this.types.Clone();
						}
						array[i] = type;
					}
				}
			}
			return new CustomModifiers(array);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000536C File Offset: 0x0000356C
		internal static CustomModifiers Read(ModuleReader module, ByteReader br, IGenericContext context)
		{
			byte b = br.PeekByte();
			if (!CustomModifiers.IsCustomModifier(b))
			{
				return default(CustomModifiers);
			}
			List<Type> list = new List<Type>();
			Type type = CustomModifiers.Initial;
			do
			{
				Type type2 = (br.ReadByte() == 31) ? MarkerType.ModReq : MarkerType.ModOpt;
				if (type != type2)
				{
					type = type2;
					list.Add(type);
				}
				list.Add(Signature.ReadTypeDefOrRefEncoded(module, br, context));
				b = br.PeekByte();
			}
			while (CustomModifiers.IsCustomModifier(b));
			return new CustomModifiers(list.ToArray());
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000053F4 File Offset: 0x000035F4
		internal static void Skip(ByteReader br)
		{
			byte b = br.PeekByte();
			while (CustomModifiers.IsCustomModifier(b))
			{
				br.ReadByte();
				br.ReadCompressedUInt();
				b = br.PeekByte();
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005428 File Offset: 0x00003628
		internal static CustomModifiers FromReqOpt(Type[] req, Type[] opt)
		{
			List<Type> list = null;
			if (opt != null && opt.Length != 0)
			{
				list = new List<Type>(opt);
			}
			if (req != null && req.Length != 0)
			{
				if (list == null)
				{
					list = new List<Type>();
				}
				list.Add(MarkerType.ModReq);
				list.AddRange(req);
			}
			if (list == null)
			{
				return default(CustomModifiers);
			}
			return new CustomModifiers(list.ToArray());
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000547F File Offset: 0x0000367F
		private static bool IsCustomModifier(byte b)
		{
			return b == 32 || b == 31;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005490 File Offset: 0x00003690
		internal static CustomModifiers Combine(CustomModifiers mods1, CustomModifiers mods2)
		{
			if (mods1.IsEmpty)
			{
				return mods2;
			}
			if (mods2.IsEmpty)
			{
				return mods1;
			}
			Type[] destinationArray = new Type[mods1.types.Length + mods2.types.Length];
			Array.Copy(mods1.types, destinationArray, mods1.types.Length);
			Array.Copy(mods2.types, 0, destinationArray, mods1.types.Length, mods2.types.Length);
			return new CustomModifiers(destinationArray);
		}

		// Token: 0x0400003F RID: 63
		private readonly Type[] types;

		// Token: 0x02000322 RID: 802
		public struct Enumerator : IEnumerator<CustomModifiers.Entry>, IEnumerator, IDisposable
		{
			// Token: 0x0600257B RID: 9595 RVA: 0x000B3C08 File Offset: 0x000B1E08
			internal Enumerator(Type[] types)
			{
				this.types = types;
				this.index = -1;
				this.required = (CustomModifiers.Initial == MarkerType.ModReq);
			}

			// Token: 0x0600257C RID: 9596 RVA: 0x000B3C2D File Offset: 0x000B1E2D
			void IEnumerator.Reset()
			{
				this.index = -1;
				this.required = (CustomModifiers.Initial == MarkerType.ModReq);
			}

			// Token: 0x1700087D RID: 2173
			// (get) Token: 0x0600257D RID: 9597 RVA: 0x000B3C4B File Offset: 0x000B1E4B
			public CustomModifiers.Entry Current
			{
				get
				{
					return new CustomModifiers.Entry(this.types[this.index], this.required);
				}
			}

			// Token: 0x0600257E RID: 9598 RVA: 0x000B3C68 File Offset: 0x000B1E68
			public bool MoveNext()
			{
				if (this.types == null || this.index == this.types.Length)
				{
					return false;
				}
				this.index++;
				if (this.index == this.types.Length)
				{
					return false;
				}
				if (this.types[this.index] == MarkerType.ModOpt)
				{
					this.required = false;
					this.index++;
				}
				else if (this.types[this.index] == MarkerType.ModReq)
				{
					this.required = true;
					this.index++;
				}
				return true;
			}

			// Token: 0x1700087E RID: 2174
			// (get) Token: 0x0600257F RID: 9599 RVA: 0x000B3D0E File Offset: 0x000B1F0E
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06002580 RID: 9600 RVA: 0x0000AF70 File Offset: 0x00009170
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000E3B RID: 3643
			private readonly Type[] types;

			// Token: 0x04000E3C RID: 3644
			private int index;

			// Token: 0x04000E3D RID: 3645
			private bool required;
		}

		// Token: 0x02000323 RID: 803
		public struct Entry
		{
			// Token: 0x06002581 RID: 9601 RVA: 0x000B3D1B File Offset: 0x000B1F1B
			internal Entry(Type type, bool required)
			{
				this.type = type;
				this.required = required;
			}

			// Token: 0x1700087F RID: 2175
			// (get) Token: 0x06002582 RID: 9602 RVA: 0x000B3D2B File Offset: 0x000B1F2B
			public Type Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x17000880 RID: 2176
			// (get) Token: 0x06002583 RID: 9603 RVA: 0x000B3D33 File Offset: 0x000B1F33
			public bool IsRequired
			{
				get
				{
					return this.required;
				}
			}

			// Token: 0x04000E3E RID: 3646
			private readonly Type type;

			// Token: 0x04000E3F RID: 3647
			private readonly bool required;
		}
	}
}
