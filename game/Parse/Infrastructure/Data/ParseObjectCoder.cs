using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Control;
using Parse.Abstractions.Infrastructure.Data;
using Parse.Abstractions.Platform.Objects;
using Parse.Platform.Objects;

namespace Parse.Infrastructure.Data
{
	// Token: 0x02000067 RID: 103
	public class ParseObjectCoder
	{
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x000101AC File Offset: 0x0000E3AC
		public static ParseObjectCoder Instance
		{
			[CompilerGenerated]
			get
			{
				return ParseObjectCoder.<Instance>k__BackingField;
			}
		} = new ParseObjectCoder();

		// Token: 0x06000493 RID: 1171 RVA: 0x000101B3 File Offset: 0x0000E3B3
		private ParseObjectCoder()
		{
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000101BC File Offset: 0x0000E3BC
		public IDictionary<string, object> Encode<T>(T state, IDictionary<string, IParseFieldOperation> operations, ParseDataEncoder encoder, IServiceHub serviceHub) where T : IObjectState
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (KeyValuePair<string, IParseFieldOperation> keyValuePair in operations)
			{
				IParseFieldOperation value = keyValuePair.Value;
				dictionary[keyValuePair.Key] = encoder.Encode(value, serviceHub);
			}
			return dictionary;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00010224 File Offset: 0x0000E424
		public IObjectState Decode(IDictionary<string, object> data, IParseDataDecoder decoder, IServiceHub serviceHub)
		{
			IDictionary<string, object> dictionary = new Dictionary<string, object>();
			IDictionary<string, object> dictionary2 = new Dictionary<string, object>(data);
			string objectId = this.Extract<string>(dictionary2, "objectId", (object obj) => obj as string);
			DateTime? dateTime = this.Extract<DateTime?>(dictionary2, "createdAt", (object obj) => new DateTime?(ParseDataDecoder.ParseDate(obj as string)));
			DateTime? updatedAt = this.Extract<DateTime?>(dictionary2, "updatedAt", (object obj) => new DateTime?(ParseDataDecoder.ParseDate(obj as string)));
			if (dictionary2.ContainsKey("ACL"))
			{
				dictionary["ACL"] = this.Extract<ParseACL>(dictionary2, "ACL", (object obj) => new ParseACL(obj as IDictionary<string, object>));
			}
			if (dateTime != null && updatedAt == null)
			{
				updatedAt = dateTime;
			}
			foreach (KeyValuePair<string, object> keyValuePair in dictionary2)
			{
				if (!(keyValuePair.Key == "__type") && !(keyValuePair.Key == "className"))
				{
					dictionary[keyValuePair.Key] = decoder.Decode(keyValuePair.Value, serviceHub);
				}
			}
			return new MutableObjectState
			{
				ObjectId = objectId,
				CreatedAt = dateTime,
				UpdatedAt = updatedAt,
				ServerData = dictionary
			};
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000103B8 File Offset: 0x0000E5B8
		private T Extract<T>(IDictionary<string, object> data, string key, Func<object, T> action)
		{
			T result = default(T);
			if (data.ContainsKey(key))
			{
				result = action(data[key]);
				data.Remove(key);
			}
			return result;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x000103ED File Offset: 0x0000E5ED
		// Note: this type is marked as 'beforefieldinit'.
		static ParseObjectCoder()
		{
		}

		// Token: 0x040000F0 RID: 240
		[CompilerGenerated]
		private static readonly ParseObjectCoder <Instance>k__BackingField;

		// Token: 0x02000138 RID: 312
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060007B9 RID: 1977 RVA: 0x00017705 File Offset: 0x00015905
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060007BA RID: 1978 RVA: 0x00017711 File Offset: 0x00015911
			public <>c()
			{
			}

			// Token: 0x060007BB RID: 1979 RVA: 0x00017719 File Offset: 0x00015919
			internal string <Decode>b__5_0(object obj)
			{
				return obj as string;
			}

			// Token: 0x060007BC RID: 1980 RVA: 0x00017721 File Offset: 0x00015921
			internal DateTime? <Decode>b__5_1(object obj)
			{
				return new DateTime?(ParseDataDecoder.ParseDate(obj as string));
			}

			// Token: 0x060007BD RID: 1981 RVA: 0x00017733 File Offset: 0x00015933
			internal DateTime? <Decode>b__5_2(object obj)
			{
				return new DateTime?(ParseDataDecoder.ParseDate(obj as string));
			}

			// Token: 0x060007BE RID: 1982 RVA: 0x00017745 File Offset: 0x00015945
			internal ParseACL <Decode>b__5_3(object obj)
			{
				return new ParseACL(obj as IDictionary<string, object>);
			}

			// Token: 0x040002DD RID: 733
			public static readonly ParseObjectCoder.<>c <>9 = new ParseObjectCoder.<>c();

			// Token: 0x040002DE RID: 734
			public static Func<object, string> <>9__5_0;

			// Token: 0x040002DF RID: 735
			public static Func<object, DateTime?> <>9__5_1;

			// Token: 0x040002E0 RID: 736
			public static Func<object, DateTime?> <>9__5_2;

			// Token: 0x040002E1 RID: 737
			public static Func<object, ParseACL> <>9__5_3;
		}
	}
}
