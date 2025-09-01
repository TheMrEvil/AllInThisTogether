using System;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000EE RID: 238
	public sealed class OpCodes
	{
		// Token: 0x06000B99 RID: 2969 RVA: 0x00028F88 File Offset: 0x00027188
		internal static string GetName(int value)
		{
			switch (value)
			{
			case -512:
				return "arglist";
			case -511:
				return "ceq";
			case -510:
				return "cgt";
			case -509:
				return "cgt.un";
			case -508:
				return "clt";
			case -507:
				return "clt.un";
			case -506:
				return "ldftn";
			case -505:
				return "ldvirtftn";
			case -504:
			case -496:
			case -487:
			case -485:
				break;
			case -503:
				return "ldarg";
			case -502:
				return "ldarga";
			case -501:
				return "starg";
			case -500:
				return "ldloc";
			case -499:
				return "ldloca";
			case -498:
				return "stloc";
			case -497:
				return "localloc";
			case -495:
				return "endfilter";
			case -494:
				return "unaligned.";
			case -493:
				return "volatile.";
			case -492:
				return "tail.";
			case -491:
				return "initobj";
			case -490:
				return "constrained.";
			case -489:
				return "cpblk";
			case -488:
				return "initblk";
			case -486:
				return "rethrow";
			case -484:
				return "sizeof";
			case -483:
				return "refanytype";
			case -482:
				return "readonly.";
			default:
				switch (value)
				{
				case 0:
					return "nop";
				case 1:
					return "break";
				case 2:
					return "ldarg.0";
				case 3:
					return "ldarg.1";
				case 4:
					return "ldarg.2";
				case 5:
					return "ldarg.3";
				case 6:
					return "ldloc.0";
				case 7:
					return "ldloc.1";
				case 8:
					return "ldloc.2";
				case 9:
					return "ldloc.3";
				case 10:
					return "stloc.0";
				case 11:
					return "stloc.1";
				case 12:
					return "stloc.2";
				case 13:
					return "stloc.3";
				case 14:
					return "ldarg.s";
				case 15:
					return "ldarga.s";
				case 16:
					return "starg.s";
				case 17:
					return "ldloc.s";
				case 18:
					return "ldloca.s";
				case 19:
					return "stloc.s";
				case 20:
					return "ldnull";
				case 21:
					return "ldc.i4.m1";
				case 22:
					return "ldc.i4.0";
				case 23:
					return "ldc.i4.1";
				case 24:
					return "ldc.i4.2";
				case 25:
					return "ldc.i4.3";
				case 26:
					return "ldc.i4.4";
				case 27:
					return "ldc.i4.5";
				case 28:
					return "ldc.i4.6";
				case 29:
					return "ldc.i4.7";
				case 30:
					return "ldc.i4.8";
				case 31:
					return "ldc.i4.s";
				case 32:
					return "ldc.i4";
				case 33:
					return "ldc.i8";
				case 34:
					return "ldc.r4";
				case 35:
					return "ldc.r8";
				case 37:
					return "dup";
				case 38:
					return "pop";
				case 39:
					return "jmp";
				case 40:
					return "call";
				case 41:
					return "calli";
				case 42:
					return "ret";
				case 43:
					return "br.s";
				case 44:
					return "brfalse.s";
				case 45:
					return "brtrue.s";
				case 46:
					return "beq.s";
				case 47:
					return "bge.s";
				case 48:
					return "bgt.s";
				case 49:
					return "ble.s";
				case 50:
					return "blt.s";
				case 51:
					return "bne.un.s";
				case 52:
					return "bge.un.s";
				case 53:
					return "bgt.un.s";
				case 54:
					return "ble.un.s";
				case 55:
					return "blt.un.s";
				case 56:
					return "br";
				case 57:
					return "brfalse";
				case 58:
					return "brtrue";
				case 59:
					return "beq";
				case 60:
					return "bge";
				case 61:
					return "bgt";
				case 62:
					return "ble";
				case 63:
					return "blt";
				case 64:
					return "bne.un";
				case 65:
					return "bge.un";
				case 66:
					return "bgt.un";
				case 67:
					return "ble.un";
				case 68:
					return "blt.un";
				case 69:
					return "switch";
				case 70:
					return "ldind.i1";
				case 71:
					return "ldind.u1";
				case 72:
					return "ldind.i2";
				case 73:
					return "ldind.u2";
				case 74:
					return "ldind.i4";
				case 75:
					return "ldind.u4";
				case 76:
					return "ldind.i8";
				case 77:
					return "ldind.i";
				case 78:
					return "ldind.r4";
				case 79:
					return "ldind.r8";
				case 80:
					return "ldind.ref";
				case 81:
					return "stind.ref";
				case 82:
					return "stind.i1";
				case 83:
					return "stind.i2";
				case 84:
					return "stind.i4";
				case 85:
					return "stind.i8";
				case 86:
					return "stind.r4";
				case 87:
					return "stind.r8";
				case 88:
					return "add";
				case 89:
					return "sub";
				case 90:
					return "mul";
				case 91:
					return "div";
				case 92:
					return "div.un";
				case 93:
					return "rem";
				case 94:
					return "rem.un";
				case 95:
					return "and";
				case 96:
					return "or";
				case 97:
					return "xor";
				case 98:
					return "shl";
				case 99:
					return "shr";
				case 100:
					return "shr.un";
				case 101:
					return "neg";
				case 102:
					return "not";
				case 103:
					return "conv.i1";
				case 104:
					return "conv.i2";
				case 105:
					return "conv.i4";
				case 106:
					return "conv.i8";
				case 107:
					return "conv.r4";
				case 108:
					return "conv.r8";
				case 109:
					return "conv.u4";
				case 110:
					return "conv.u8";
				case 111:
					return "callvirt";
				case 112:
					return "cpobj";
				case 113:
					return "ldobj";
				case 114:
					return "ldstr";
				case 115:
					return "newobj";
				case 116:
					return "castclass";
				case 117:
					return "isinst";
				case 118:
					return "conv.r.un";
				case 121:
					return "unbox";
				case 122:
					return "throw";
				case 123:
					return "ldfld";
				case 124:
					return "ldflda";
				case 125:
					return "stfld";
				case 126:
					return "ldsfld";
				case 127:
					return "ldsflda";
				case 128:
					return "stsfld";
				case 129:
					return "stobj";
				case 130:
					return "conv.ovf.i1.un";
				case 131:
					return "conv.ovf.i2.un";
				case 132:
					return "conv.ovf.i4.un";
				case 133:
					return "conv.ovf.i8.un";
				case 134:
					return "conv.ovf.u1.un";
				case 135:
					return "conv.ovf.u2.un";
				case 136:
					return "conv.ovf.u4.un";
				case 137:
					return "conv.ovf.u8.un";
				case 138:
					return "conv.ovf.i.un";
				case 139:
					return "conv.ovf.u.un";
				case 140:
					return "box";
				case 141:
					return "newarr";
				case 142:
					return "ldlen";
				case 143:
					return "ldelema";
				case 144:
					return "ldelem.i1";
				case 145:
					return "ldelem.u1";
				case 146:
					return "ldelem.i2";
				case 147:
					return "ldelem.u2";
				case 148:
					return "ldelem.i4";
				case 149:
					return "ldelem.u4";
				case 150:
					return "ldelem.i8";
				case 151:
					return "ldelem.i";
				case 152:
					return "ldelem.r4";
				case 153:
					return "ldelem.r8";
				case 154:
					return "ldelem.ref";
				case 155:
					return "stelem.i";
				case 156:
					return "stelem.i1";
				case 157:
					return "stelem.i2";
				case 158:
					return "stelem.i4";
				case 159:
					return "stelem.i8";
				case 160:
					return "stelem.r4";
				case 161:
					return "stelem.r8";
				case 162:
					return "stelem.ref";
				case 163:
					return "ldelem";
				case 164:
					return "stelem";
				case 165:
					return "unbox.any";
				case 179:
					return "conv.ovf.i1";
				case 180:
					return "conv.ovf.u1";
				case 181:
					return "conv.ovf.i2";
				case 182:
					return "conv.ovf.u2";
				case 183:
					return "conv.ovf.i4";
				case 184:
					return "conv.ovf.u4";
				case 185:
					return "conv.ovf.i8";
				case 186:
					return "conv.ovf.u8";
				case 194:
					return "refanyval";
				case 195:
					return "ckfinite";
				case 198:
					return "mkrefany";
				case 208:
					return "ldtoken";
				case 209:
					return "conv.u2";
				case 210:
					return "conv.u1";
				case 211:
					return "conv.i";
				case 212:
					return "conv.ovf.i";
				case 213:
					return "conv.ovf.u";
				case 214:
					return "add.ovf";
				case 215:
					return "add.ovf.un";
				case 216:
					return "mul.ovf";
				case 217:
					return "mul.ovf.un";
				case 218:
					return "sub.ovf";
				case 219:
					return "sub.ovf.un";
				case 220:
					return "endfinally";
				case 221:
					return "leave";
				case 222:
					return "leave.s";
				case 223:
					return "stind.i";
				case 224:
					return "conv.u";
				case 248:
					return "prefix7";
				case 249:
					return "prefix6";
				case 250:
					return "prefix5";
				case 251:
					return "prefix4";
				case 252:
					return "prefix3";
				case 253:
					return "prefix2";
				case 254:
					return "prefix1";
				case 255:
					return "prefixref";
				}
				break;
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0002997C File Offset: 0x00027B7C
		public static bool TakesSingleByteArgument(OpCode inst)
		{
			short value = inst.Value;
			if (value <= 19)
			{
				if (value != -494)
				{
					switch (value)
					{
					case 14:
					case 15:
					case 16:
					case 17:
					case 18:
					case 19:
						break;
					default:
						return false;
					}
				}
			}
			else
			{
				switch (value)
				{
				case 31:
				case 43:
				case 44:
				case 45:
				case 46:
				case 47:
				case 48:
				case 49:
				case 50:
				case 51:
				case 52:
				case 53:
				case 54:
				case 55:
					break;
				case 32:
				case 33:
				case 34:
				case 35:
				case 36:
				case 37:
				case 38:
				case 39:
				case 40:
				case 41:
				case 42:
					return false;
				default:
					if (value != 222)
					{
						return false;
					}
					break;
				}
			}
			return true;
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00002CCC File Offset: 0x00000ECC
		public OpCodes()
		{
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00029A3C File Offset: 0x00027C3C
		// Note: this type is marked as 'beforefieldinit'.
		static OpCodes()
		{
		}

		// Token: 0x040004FB RID: 1275
		public static readonly OpCode Nop = new OpCode(4888);

		// Token: 0x040004FC RID: 1276
		public static readonly OpCode Break = new OpCode(4199116);

		// Token: 0x040004FD RID: 1277
		public static readonly OpCode Ldarg_0 = new OpCode(8492847);

		// Token: 0x040004FE RID: 1278
		public static readonly OpCode Ldarg_1 = new OpCode(12687151);

		// Token: 0x040004FF RID: 1279
		public static readonly OpCode Ldarg_2 = new OpCode(16881455);

		// Token: 0x04000500 RID: 1280
		public static readonly OpCode Ldarg_3 = new OpCode(21075759);

		// Token: 0x04000501 RID: 1281
		public static readonly OpCode Ldloc_0 = new OpCode(25270063);

		// Token: 0x04000502 RID: 1282
		public static readonly OpCode Ldloc_1 = new OpCode(29464367);

		// Token: 0x04000503 RID: 1283
		public static readonly OpCode Ldloc_2 = new OpCode(33658671);

		// Token: 0x04000504 RID: 1284
		public static readonly OpCode Ldloc_3 = new OpCode(37852975);

		// Token: 0x04000505 RID: 1285
		public static readonly OpCode Stloc_0 = new OpCode(41949467);

		// Token: 0x04000506 RID: 1286
		public static readonly OpCode Stloc_1 = new OpCode(46143771);

		// Token: 0x04000507 RID: 1287
		public static readonly OpCode Stloc_2 = new OpCode(50338075);

		// Token: 0x04000508 RID: 1288
		public static readonly OpCode Stloc_3 = new OpCode(54532379);

		// Token: 0x04000509 RID: 1289
		public static readonly OpCode Ldarg_S = new OpCode(58824508);

		// Token: 0x0400050A RID: 1290
		public static readonly OpCode Ldarga_S = new OpCode(63224012);

		// Token: 0x0400050B RID: 1291
		public static readonly OpCode Starg_S = new OpCode(67115304);

		// Token: 0x0400050C RID: 1292
		public static readonly OpCode Ldloc_S = new OpCode(71407420);

		// Token: 0x0400050D RID: 1293
		public static readonly OpCode Ldloca_S = new OpCode(75806924);

		// Token: 0x0400050E RID: 1294
		public static readonly OpCode Stloc_S = new OpCode(79698216);

		// Token: 0x0400050F RID: 1295
		public static readonly OpCode Ldnull = new OpCode(84609339);

		// Token: 0x04000510 RID: 1296
		public static readonly OpCode Ldc_I4_M1 = new OpCode(88389823);

		// Token: 0x04000511 RID: 1297
		public static readonly OpCode Ldc_I4_0 = new OpCode(92584127);

		// Token: 0x04000512 RID: 1298
		public static readonly OpCode Ldc_I4_1 = new OpCode(96778431);

		// Token: 0x04000513 RID: 1299
		public static readonly OpCode Ldc_I4_2 = new OpCode(100972735);

		// Token: 0x04000514 RID: 1300
		public static readonly OpCode Ldc_I4_3 = new OpCode(105167039);

		// Token: 0x04000515 RID: 1301
		public static readonly OpCode Ldc_I4_4 = new OpCode(109361343);

		// Token: 0x04000516 RID: 1302
		public static readonly OpCode Ldc_I4_5 = new OpCode(113555647);

		// Token: 0x04000517 RID: 1303
		public static readonly OpCode Ldc_I4_6 = new OpCode(117749951);

		// Token: 0x04000518 RID: 1304
		public static readonly OpCode Ldc_I4_7 = new OpCode(121944255);

		// Token: 0x04000519 RID: 1305
		public static readonly OpCode Ldc_I4_8 = new OpCode(126138559);

		// Token: 0x0400051A RID: 1306
		public static readonly OpCode Ldc_I4_S = new OpCode(130332874);

		// Token: 0x0400051B RID: 1307
		public static readonly OpCode Ldc_I4 = new OpCode(134530584);

		// Token: 0x0400051C RID: 1308
		public static readonly OpCode Ldc_I8 = new OpCode(138827489);

		// Token: 0x0400051D RID: 1309
		public static readonly OpCode Ldc_R4 = new OpCode(143124407);

		// Token: 0x0400051E RID: 1310
		public static readonly OpCode Ldc_R8 = new OpCode(147421301);

		// Token: 0x0400051F RID: 1311
		public static readonly OpCode Dup = new OpCode(155404637);

		// Token: 0x04000520 RID: 1312
		public static readonly OpCode Pop = new OpCode(159393399);

		// Token: 0x04000521 RID: 1313
		public static readonly OpCode Jmp = new OpCode(163582686);

		// Token: 0x04000522 RID: 1314
		public static readonly OpCode Call = new OpCode(168690130);

		// Token: 0x04000523 RID: 1315
		public static readonly OpCode Calli = new OpCode(172884439);

		// Token: 0x04000524 RID: 1316
		public static readonly OpCode Ret = new OpCode(176258034);

		// Token: 0x04000525 RID: 1317
		public static readonly OpCode Br_S = new OpCode(180356455);

		// Token: 0x04000526 RID: 1318
		public static readonly OpCode Brfalse_S = new OpCode(184566035);

		// Token: 0x04000527 RID: 1319
		public static readonly OpCode Brtrue_S = new OpCode(188760339);

		// Token: 0x04000528 RID: 1320
		public static readonly OpCode Beq_S = new OpCode(192949342);

		// Token: 0x04000529 RID: 1321
		public static readonly OpCode Bge_S = new OpCode(197143646);

		// Token: 0x0400052A RID: 1322
		public static readonly OpCode Bgt_S = new OpCode(201337950);

		// Token: 0x0400052B RID: 1323
		public static readonly OpCode Ble_S = new OpCode(205532254);

		// Token: 0x0400052C RID: 1324
		public static readonly OpCode Blt_S = new OpCode(209726558);

		// Token: 0x0400052D RID: 1325
		public static readonly OpCode Bne_Un_S = new OpCode(213920862);

		// Token: 0x0400052E RID: 1326
		public static readonly OpCode Bge_Un_S = new OpCode(218115166);

		// Token: 0x0400052F RID: 1327
		public static readonly OpCode Bgt_Un_S = new OpCode(222309470);

		// Token: 0x04000530 RID: 1328
		public static readonly OpCode Ble_Un_S = new OpCode(226503774);

		// Token: 0x04000531 RID: 1329
		public static readonly OpCode Blt_Un_S = new OpCode(230698078);

		// Token: 0x04000532 RID: 1330
		public static readonly OpCode Br = new OpCode(234885812);

		// Token: 0x04000533 RID: 1331
		public static readonly OpCode Brfalse = new OpCode(239095392);

		// Token: 0x04000534 RID: 1332
		public static readonly OpCode Brtrue = new OpCode(243289696);

		// Token: 0x04000535 RID: 1333
		public static readonly OpCode Beq = new OpCode(247475279);

		// Token: 0x04000536 RID: 1334
		public static readonly OpCode Bge = new OpCode(251669583);

		// Token: 0x04000537 RID: 1335
		public static readonly OpCode Bgt = new OpCode(255863887);

		// Token: 0x04000538 RID: 1336
		public static readonly OpCode Ble = new OpCode(260058191);

		// Token: 0x04000539 RID: 1337
		public static readonly OpCode Blt = new OpCode(264252495);

		// Token: 0x0400053A RID: 1338
		public static readonly OpCode Bne_Un = new OpCode(268446799);

		// Token: 0x0400053B RID: 1339
		public static readonly OpCode Bge_Un = new OpCode(272641103);

		// Token: 0x0400053C RID: 1340
		public static readonly OpCode Bgt_Un = new OpCode(276835407);

		// Token: 0x0400053D RID: 1341
		public static readonly OpCode Ble_Un = new OpCode(281029711);

		// Token: 0x0400053E RID: 1342
		public static readonly OpCode Blt_Un = new OpCode(285224015);

		// Token: 0x0400053F RID: 1343
		public static readonly OpCode Switch = new OpCode(289427051);

		// Token: 0x04000540 RID: 1344
		public static readonly OpCode Ldind_I1 = new OpCode(293929358);

		// Token: 0x04000541 RID: 1345
		public static readonly OpCode Ldind_U1 = new OpCode(298123662);

		// Token: 0x04000542 RID: 1346
		public static readonly OpCode Ldind_I2 = new OpCode(302317966);

		// Token: 0x04000543 RID: 1347
		public static readonly OpCode Ldind_U2 = new OpCode(306512270);

		// Token: 0x04000544 RID: 1348
		public static readonly OpCode Ldind_I4 = new OpCode(310706574);

		// Token: 0x04000545 RID: 1349
		public static readonly OpCode Ldind_U4 = new OpCode(314900878);

		// Token: 0x04000546 RID: 1350
		public static readonly OpCode Ldind_I8 = new OpCode(319197782);

		// Token: 0x04000547 RID: 1351
		public static readonly OpCode Ldind_I = new OpCode(323289486);

		// Token: 0x04000548 RID: 1352
		public static readonly OpCode Ldind_R4 = new OpCode(327688990);

		// Token: 0x04000549 RID: 1353
		public static readonly OpCode Ldind_R8 = new OpCode(331985894);

		// Token: 0x0400054A RID: 1354
		public static readonly OpCode Ldind_Ref = new OpCode(336282798);

		// Token: 0x0400054B RID: 1355
		public static readonly OpCode Stind_Ref = new OpCode(339768820);

		// Token: 0x0400054C RID: 1356
		public static readonly OpCode Stind_I1 = new OpCode(343963124);

		// Token: 0x0400054D RID: 1357
		public static readonly OpCode Stind_I2 = new OpCode(348157428);

		// Token: 0x0400054E RID: 1358
		public static readonly OpCode Stind_I4 = new OpCode(352351732);

		// Token: 0x0400054F RID: 1359
		public static readonly OpCode Stind_I8 = new OpCode(356551166);

		// Token: 0x04000550 RID: 1360
		public static readonly OpCode Stind_R4 = new OpCode(360755730);

		// Token: 0x04000551 RID: 1361
		public static readonly OpCode Stind_R8 = new OpCode(364955164);

		// Token: 0x04000552 RID: 1362
		public static readonly OpCode Add = new OpCode(369216329);

		// Token: 0x04000553 RID: 1363
		public static readonly OpCode Sub = new OpCode(373410633);

		// Token: 0x04000554 RID: 1364
		public static readonly OpCode Mul = new OpCode(377604937);

		// Token: 0x04000555 RID: 1365
		public static readonly OpCode Div = new OpCode(381799241);

		// Token: 0x04000556 RID: 1366
		public static readonly OpCode Div_Un = new OpCode(385993545);

		// Token: 0x04000557 RID: 1367
		public static readonly OpCode Rem = new OpCode(390187849);

		// Token: 0x04000558 RID: 1368
		public static readonly OpCode Rem_Un = new OpCode(394382153);

		// Token: 0x04000559 RID: 1369
		public static readonly OpCode And = new OpCode(398576457);

		// Token: 0x0400055A RID: 1370
		public static readonly OpCode Or = new OpCode(402770761);

		// Token: 0x0400055B RID: 1371
		public static readonly OpCode Xor = new OpCode(406965065);

		// Token: 0x0400055C RID: 1372
		public static readonly OpCode Shl = new OpCode(411159369);

		// Token: 0x0400055D RID: 1373
		public static readonly OpCode Shr = new OpCode(415353673);

		// Token: 0x0400055E RID: 1374
		public static readonly OpCode Shr_Un = new OpCode(419547977);

		// Token: 0x0400055F RID: 1375
		public static readonly OpCode Neg = new OpCode(423737322);

		// Token: 0x04000560 RID: 1376
		public static readonly OpCode Not = new OpCode(427931626);

		// Token: 0x04000561 RID: 1377
		public static readonly OpCode Conv_I1 = new OpCode(432331130);

		// Token: 0x04000562 RID: 1378
		public static readonly OpCode Conv_I2 = new OpCode(436525434);

		// Token: 0x04000563 RID: 1379
		public static readonly OpCode Conv_I4 = new OpCode(440719738);

		// Token: 0x04000564 RID: 1380
		public static readonly OpCode Conv_I8 = new OpCode(445016642);

		// Token: 0x04000565 RID: 1381
		public static readonly OpCode Conv_R4 = new OpCode(449313546);

		// Token: 0x04000566 RID: 1382
		public static readonly OpCode Conv_R8 = new OpCode(453610450);

		// Token: 0x04000567 RID: 1383
		public static readonly OpCode Conv_U4 = new OpCode(457496954);

		// Token: 0x04000568 RID: 1384
		public static readonly OpCode Conv_U8 = new OpCode(461793858);

		// Token: 0x04000569 RID: 1385
		public static readonly OpCode Callvirt = new OpCode(466484004);

		// Token: 0x0400056A RID: 1386
		public static readonly OpCode Cpobj = new OpCode(469790542);

		// Token: 0x0400056B RID: 1387
		public static readonly OpCode Ldobj = new OpCode(474077528);

		// Token: 0x0400056C RID: 1388
		public static readonly OpCode Ldstr = new OpCode(478872210);

		// Token: 0x0400056D RID: 1389
		public static readonly OpCode Newobj = new OpCode(483158791);

		// Token: 0x0400056E RID: 1390
		public static readonly OpCode Castclass = new OpCode(487311950);

		// Token: 0x0400056F RID: 1391
		public static readonly OpCode Isinst = new OpCode(491095854);

		// Token: 0x04000570 RID: 1392
		public static readonly OpCode Conv_R_Un = new OpCode(495553490);

		// Token: 0x04000571 RID: 1393
		public static readonly OpCode Unbox = new OpCode(507874780);

		// Token: 0x04000572 RID: 1394
		public static readonly OpCode Throw = new OpCode(511759452);

		// Token: 0x04000573 RID: 1395
		public static readonly OpCode Ldfld = new OpCode(516056466);

		// Token: 0x04000574 RID: 1396
		public static readonly OpCode Ldflda = new OpCode(520455970);

		// Token: 0x04000575 RID: 1397
		public static readonly OpCode Stfld = new OpCode(524347262);

		// Token: 0x04000576 RID: 1398
		public static readonly OpCode Ldsfld = new OpCode(528588249);

		// Token: 0x04000577 RID: 1399
		public static readonly OpCode Ldsflda = new OpCode(532987753);

		// Token: 0x04000578 RID: 1400
		public static readonly OpCode Stsfld = new OpCode(536879045);

		// Token: 0x04000579 RID: 1401
		public static readonly OpCode Stobj = new OpCode(541090290);

		// Token: 0x0400057A RID: 1402
		public static readonly OpCode Conv_Ovf_I1_Un = new OpCode(545577338);

		// Token: 0x0400057B RID: 1403
		public static readonly OpCode Conv_Ovf_I2_Un = new OpCode(549771642);

		// Token: 0x0400057C RID: 1404
		public static readonly OpCode Conv_Ovf_I4_Un = new OpCode(553965946);

		// Token: 0x0400057D RID: 1405
		public static readonly OpCode Conv_Ovf_I8_Un = new OpCode(558262850);

		// Token: 0x0400057E RID: 1406
		public static readonly OpCode Conv_Ovf_U1_Un = new OpCode(562354554);

		// Token: 0x0400057F RID: 1407
		public static readonly OpCode Conv_Ovf_U2_Un = new OpCode(566548858);

		// Token: 0x04000580 RID: 1408
		public static readonly OpCode Conv_Ovf_U4_Un = new OpCode(570743162);

		// Token: 0x04000581 RID: 1409
		public static readonly OpCode Conv_Ovf_U8_Un = new OpCode(575040066);

		// Token: 0x04000582 RID: 1410
		public static readonly OpCode Conv_Ovf_I_Un = new OpCode(579131770);

		// Token: 0x04000583 RID: 1411
		public static readonly OpCode Conv_Ovf_U_Un = new OpCode(583326074);

		// Token: 0x04000584 RID: 1412
		public static readonly OpCode Box = new OpCode(587930786);

		// Token: 0x04000585 RID: 1413
		public static readonly OpCode Newarr = new OpCode(592133640);

		// Token: 0x04000586 RID: 1414
		public static readonly OpCode Ldlen = new OpCode(595953446);

		// Token: 0x04000587 RID: 1415
		public static readonly OpCode Ldelema = new OpCode(600157847);

		// Token: 0x04000588 RID: 1416
		public static readonly OpCode Ldelem_I1 = new OpCode(604352143);

		// Token: 0x04000589 RID: 1417
		public static readonly OpCode Ldelem_U1 = new OpCode(608546447);

		// Token: 0x0400058A RID: 1418
		public static readonly OpCode Ldelem_I2 = new OpCode(612740751);

		// Token: 0x0400058B RID: 1419
		public static readonly OpCode Ldelem_U2 = new OpCode(616935055);

		// Token: 0x0400058C RID: 1420
		public static readonly OpCode Ldelem_I4 = new OpCode(621129359);

		// Token: 0x0400058D RID: 1421
		public static readonly OpCode Ldelem_U4 = new OpCode(625323663);

		// Token: 0x0400058E RID: 1422
		public static readonly OpCode Ldelem_I8 = new OpCode(629620567);

		// Token: 0x0400058F RID: 1423
		public static readonly OpCode Ldelem_I = new OpCode(633712271);

		// Token: 0x04000590 RID: 1424
		public static readonly OpCode Ldelem_R4 = new OpCode(638111775);

		// Token: 0x04000591 RID: 1425
		public static readonly OpCode Ldelem_R8 = new OpCode(642408679);

		// Token: 0x04000592 RID: 1426
		public static readonly OpCode Ldelem_Ref = new OpCode(646705583);

		// Token: 0x04000593 RID: 1427
		public static readonly OpCode Stelem_I = new OpCode(650186475);

		// Token: 0x04000594 RID: 1428
		public static readonly OpCode Stelem_I1 = new OpCode(654380779);

		// Token: 0x04000595 RID: 1429
		public static readonly OpCode Stelem_I2 = new OpCode(658575083);

		// Token: 0x04000596 RID: 1430
		public static readonly OpCode Stelem_I4 = new OpCode(662769387);

		// Token: 0x04000597 RID: 1431
		public static readonly OpCode Stelem_I8 = new OpCode(666968821);

		// Token: 0x04000598 RID: 1432
		public static readonly OpCode Stelem_R4 = new OpCode(671168255);

		// Token: 0x04000599 RID: 1433
		public static readonly OpCode Stelem_R8 = new OpCode(675367689);

		// Token: 0x0400059A RID: 1434
		public static readonly OpCode Stelem_Ref = new OpCode(679567123);

		// Token: 0x0400059B RID: 1435
		public static readonly OpCode Ldelem = new OpCode(683838727);

		// Token: 0x0400059C RID: 1436
		public static readonly OpCode Stelem = new OpCode(687965999);

		// Token: 0x0400059D RID: 1437
		public static readonly OpCode Unbox_Any = new OpCode(692217246);

		// Token: 0x0400059E RID: 1438
		public static readonly OpCode Conv_Ovf_I1 = new OpCode(751098234);

		// Token: 0x0400059F RID: 1439
		public static readonly OpCode Conv_Ovf_U1 = new OpCode(755292538);

		// Token: 0x040005A0 RID: 1440
		public static readonly OpCode Conv_Ovf_I2 = new OpCode(759486842);

		// Token: 0x040005A1 RID: 1441
		public static readonly OpCode Conv_Ovf_U2 = new OpCode(763681146);

		// Token: 0x040005A2 RID: 1442
		public static readonly OpCode Conv_Ovf_I4 = new OpCode(767875450);

		// Token: 0x040005A3 RID: 1443
		public static readonly OpCode Conv_Ovf_U4 = new OpCode(772069754);

		// Token: 0x040005A4 RID: 1444
		public static readonly OpCode Conv_Ovf_I8 = new OpCode(776366658);

		// Token: 0x040005A5 RID: 1445
		public static readonly OpCode Conv_Ovf_U8 = new OpCode(780560962);

		// Token: 0x040005A6 RID: 1446
		public static readonly OpCode Refanyval = new OpCode(814012802);

		// Token: 0x040005A7 RID: 1447
		public static readonly OpCode Ckfinite = new OpCode(818514898);

		// Token: 0x040005A8 RID: 1448
		public static readonly OpCode Mkrefany = new OpCode(830595078);

		// Token: 0x040005A9 RID: 1449
		public static readonly OpCode Ldtoken = new OpCode(872728098);

		// Token: 0x040005AA RID: 1450
		public static readonly OpCode Conv_U2 = new OpCode(876927354);

		// Token: 0x040005AB RID: 1451
		public static readonly OpCode Conv_U1 = new OpCode(881121658);

		// Token: 0x040005AC RID: 1452
		public static readonly OpCode Conv_I = new OpCode(885315962);

		// Token: 0x040005AD RID: 1453
		public static readonly OpCode Conv_Ovf_I = new OpCode(889510266);

		// Token: 0x040005AE RID: 1454
		public static readonly OpCode Conv_Ovf_U = new OpCode(893704570);

		// Token: 0x040005AF RID: 1455
		public static readonly OpCode Add_Ovf = new OpCode(897698633);

		// Token: 0x040005B0 RID: 1456
		public static readonly OpCode Add_Ovf_Un = new OpCode(901892937);

		// Token: 0x040005B1 RID: 1457
		public static readonly OpCode Mul_Ovf = new OpCode(906087241);

		// Token: 0x040005B2 RID: 1458
		public static readonly OpCode Mul_Ovf_Un = new OpCode(910281545);

		// Token: 0x040005B3 RID: 1459
		public static readonly OpCode Sub_Ovf = new OpCode(914475849);

		// Token: 0x040005B4 RID: 1460
		public static readonly OpCode Sub_Ovf_Un = new OpCode(918670153);

		// Token: 0x040005B5 RID: 1461
		public static readonly OpCode Endfinally = new OpCode(922751806);

		// Token: 0x040005B6 RID: 1462
		public static readonly OpCode Leave = new OpCode(926945972);

		// Token: 0x040005B7 RID: 1463
		public static readonly OpCode Leave_S = new OpCode(931140291);

		// Token: 0x040005B8 RID: 1464
		public static readonly OpCode Stind_I = new OpCode(935359988);

		// Token: 0x040005B9 RID: 1465
		public static readonly OpCode Conv_U = new OpCode(939841914);

		// Token: 0x040005BA RID: 1466
		public static readonly OpCode Prefix7 = new OpCode(1040189696);

		// Token: 0x040005BB RID: 1467
		public static readonly OpCode Prefix6 = new OpCode(1044384000);

		// Token: 0x040005BC RID: 1468
		public static readonly OpCode Prefix5 = new OpCode(1048578304);

		// Token: 0x040005BD RID: 1469
		public static readonly OpCode Prefix4 = new OpCode(1052772608);

		// Token: 0x040005BE RID: 1470
		public static readonly OpCode Prefix3 = new OpCode(1056966912);

		// Token: 0x040005BF RID: 1471
		public static readonly OpCode Prefix2 = new OpCode(1061161216);

		// Token: 0x040005C0 RID: 1472
		public static readonly OpCode Prefix1 = new OpCode(1065355520);

		// Token: 0x040005C1 RID: 1473
		public static readonly OpCode Prefixref = new OpCode(1069549824);

		// Token: 0x040005C2 RID: 1474
		public static readonly OpCode Arglist = new OpCode(-2147170789);

		// Token: 0x040005C3 RID: 1475
		public static readonly OpCode Ceq = new OpCode(-2142966567);

		// Token: 0x040005C4 RID: 1476
		public static readonly OpCode Cgt = new OpCode(-2138772263);

		// Token: 0x040005C5 RID: 1477
		public static readonly OpCode Cgt_Un = new OpCode(-2134577959);

		// Token: 0x040005C6 RID: 1478
		public static readonly OpCode Clt = new OpCode(-2130383655);

		// Token: 0x040005C7 RID: 1479
		public static readonly OpCode Clt_Un = new OpCode(-2126189351);

		// Token: 0x040005C8 RID: 1480
		public static readonly OpCode Ldftn = new OpCode(-2122004966);

		// Token: 0x040005C9 RID: 1481
		public static readonly OpCode Ldvirtftn = new OpCode(-2117759533);

		// Token: 0x040005CA RID: 1482
		public static readonly OpCode Ldarg = new OpCode(-2109627244);

		// Token: 0x040005CB RID: 1483
		public static readonly OpCode Ldarga = new OpCode(-2105227740);

		// Token: 0x040005CC RID: 1484
		public static readonly OpCode Starg = new OpCode(-2101336448);

		// Token: 0x040005CD RID: 1485
		public static readonly OpCode Ldloc = new OpCode(-2097044332);

		// Token: 0x040005CE RID: 1486
		public static readonly OpCode Ldloca = new OpCode(-2092644828);

		// Token: 0x040005CF RID: 1487
		public static readonly OpCode Stloc = new OpCode(-2088753536);

		// Token: 0x040005D0 RID: 1488
		public static readonly OpCode Localloc = new OpCode(-2084241010);

		// Token: 0x040005D1 RID: 1489
		public static readonly OpCode Endfilter = new OpCode(-2076160335);

		// Token: 0x040005D2 RID: 1490
		public static readonly OpCode Unaligned = new OpCode(-2071982151);

		// Token: 0x040005D3 RID: 1491
		public static readonly OpCode Volatile = new OpCode(-2067787858);

		// Token: 0x040005D4 RID: 1492
		public static readonly OpCode Tailcall = new OpCode(-2063593554);

		// Token: 0x040005D5 RID: 1493
		public static readonly OpCode Initobj = new OpCode(-2059384859);

		// Token: 0x040005D6 RID: 1494
		public static readonly OpCode Constrained = new OpCode(-2055204938);

		// Token: 0x040005D7 RID: 1495
		public static readonly OpCode Cpblk = new OpCode(-2050974371);

		// Token: 0x040005D8 RID: 1496
		public static readonly OpCode Initblk = new OpCode(-2046780067);

		// Token: 0x040005D9 RID: 1497
		public static readonly OpCode Rethrow = new OpCode(-2038428509);

		// Token: 0x040005DA RID: 1498
		public static readonly OpCode Sizeof = new OpCode(-2029730269);

		// Token: 0x040005DB RID: 1499
		public static readonly OpCode Refanytype = new OpCode(-2025531014);

		// Token: 0x040005DC RID: 1500
		public static readonly OpCode Readonly = new OpCode(-2021650514);
	}
}
