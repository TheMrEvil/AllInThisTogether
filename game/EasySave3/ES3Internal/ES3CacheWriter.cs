using System;
using System.ComponentModel;

namespace ES3Internal
{
	// Token: 0x020000E9 RID: 233
	internal class ES3CacheWriter : ES3Writer
	{
		// Token: 0x060004FF RID: 1279 RVA: 0x0001EF2D File Offset: 0x0001D12D
		internal ES3CacheWriter(ES3Settings settings, bool writeHeaderAndFooter, bool mergeKeys) : base(settings, writeHeaderAndFooter, mergeKeys)
		{
			this.es3File = new ES3File(settings);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001EF44 File Offset: 0x0001D144
		public override void Write<T>(string key, object value)
		{
			this.es3File.Save<T>(key, (T)((object)value));
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0001EF58 File Offset: 0x0001D158
		internal override void Write(string key, Type type, byte[] value)
		{
			ES3Debug.LogError("Not implemented", null, 0);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0001EF66 File Offset: 0x0001D166
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Write(Type type, string key, object value)
		{
			this.es3File.Save<object>(key, value);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001EF75 File Offset: 0x0001D175
		internal override void WritePrimitive(int value)
		{
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001EF77 File Offset: 0x0001D177
		internal override void WritePrimitive(float value)
		{
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001EF79 File Offset: 0x0001D179
		internal override void WritePrimitive(bool value)
		{
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001EF7B File Offset: 0x0001D17B
		internal override void WritePrimitive(decimal value)
		{
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001EF7D File Offset: 0x0001D17D
		internal override void WritePrimitive(double value)
		{
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001EF7F File Offset: 0x0001D17F
		internal override void WritePrimitive(long value)
		{
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001EF81 File Offset: 0x0001D181
		internal override void WritePrimitive(ulong value)
		{
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001EF83 File Offset: 0x0001D183
		internal override void WritePrimitive(uint value)
		{
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001EF85 File Offset: 0x0001D185
		internal override void WritePrimitive(byte value)
		{
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001EF87 File Offset: 0x0001D187
		internal override void WritePrimitive(sbyte value)
		{
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001EF89 File Offset: 0x0001D189
		internal override void WritePrimitive(short value)
		{
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0001EF8B File Offset: 0x0001D18B
		internal override void WritePrimitive(ushort value)
		{
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0001EF8D File Offset: 0x0001D18D
		internal override void WritePrimitive(char value)
		{
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001EF8F File Offset: 0x0001D18F
		internal override void WritePrimitive(byte[] value)
		{
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0001EF91 File Offset: 0x0001D191
		internal override void WritePrimitive(string value)
		{
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001EF93 File Offset: 0x0001D193
		internal override void WriteNull()
		{
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001EF95 File Offset: 0x0001D195
		private static bool CharacterRequiresEscaping(char c)
		{
			return false;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0001EF98 File Offset: 0x0001D198
		private void WriteCommaIfRequired()
		{
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001EF9A File Offset: 0x0001D19A
		internal override void WriteRawProperty(string name, byte[] value)
		{
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001EF9C File Offset: 0x0001D19C
		internal override void StartWriteFile()
		{
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0001EF9E File Offset: 0x0001D19E
		internal override void EndWriteFile()
		{
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0001EFA0 File Offset: 0x0001D1A0
		internal override void StartWriteProperty(string name)
		{
			base.StartWriteProperty(name);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0001EFA9 File Offset: 0x0001D1A9
		internal override void EndWriteProperty(string name)
		{
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001EFAB File Offset: 0x0001D1AB
		internal override void StartWriteObject(string name)
		{
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001EFAD File Offset: 0x0001D1AD
		internal override void EndWriteObject(string name)
		{
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001EFAF File Offset: 0x0001D1AF
		internal override void StartWriteCollection()
		{
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001EFB1 File Offset: 0x0001D1B1
		internal override void EndWriteCollection()
		{
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0001EFB3 File Offset: 0x0001D1B3
		internal override void StartWriteCollectionItem(int index)
		{
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0001EFB5 File Offset: 0x0001D1B5
		internal override void EndWriteCollectionItem(int index)
		{
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0001EFB7 File Offset: 0x0001D1B7
		internal override void StartWriteDictionary()
		{
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0001EFB9 File Offset: 0x0001D1B9
		internal override void EndWriteDictionary()
		{
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0001EFBB File Offset: 0x0001D1BB
		internal override void StartWriteDictionaryKey(int index)
		{
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0001EFBD File Offset: 0x0001D1BD
		internal override void EndWriteDictionaryKey(int index)
		{
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0001EFBF File Offset: 0x0001D1BF
		internal override void StartWriteDictionaryValue(int index)
		{
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001EFC1 File Offset: 0x0001D1C1
		internal override void EndWriteDictionaryValue(int index)
		{
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001EFC3 File Offset: 0x0001D1C3
		public override void Dispose()
		{
		}

		// Token: 0x04000184 RID: 388
		private ES3File es3File;
	}
}
