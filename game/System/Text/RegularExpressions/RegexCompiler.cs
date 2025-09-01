using System;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000200 RID: 512
	internal abstract class RegexCompiler
	{
		// Token: 0x06000DFA RID: 3578 RVA: 0x0003A54C File Offset: 0x0003874C
		static RegexCompiler()
		{
			RegexCompiler.s_isECMABoundaryM = RegexCompiler.RegexRunnerMethod("IsECMABoundary");
			RegexCompiler.s_crawlposM = RegexCompiler.RegexRunnerMethod("Crawlpos");
			RegexCompiler.s_checkTimeoutM = RegexCompiler.RegexRunnerMethod("CheckTimeout");
			RegexCompiler.s_chartolowerM = typeof(char).GetMethod("ToLower", new Type[]
			{
				typeof(char),
				typeof(CultureInfo)
			});
			RegexCompiler.s_getcharM = typeof(string).GetMethod("get_Chars", new Type[]
			{
				typeof(int)
			});
			RegexCompiler.s_getCurrentCulture = typeof(CultureInfo).GetMethod("get_CurrentCulture");
			RegexCompiler.s_getInvariantCulture = typeof(CultureInfo).GetMethod("get_InvariantCulture");
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0003A73A File Offset: 0x0003893A
		private static FieldInfo RegexRunnerField(string fieldname)
		{
			return typeof(RegexRunner).GetField(fieldname, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0003A74E File Offset: 0x0003894E
		private static MethodInfo RegexRunnerMethod(string methname)
		{
			return typeof(RegexRunner).GetMethod(methname, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0003A762 File Offset: 0x00038962
		internal static RegexRunnerFactory Compile(RegexCode code, RegexOptions options)
		{
			return new RegexLWCGCompiler().FactoryInstanceFromCode(code, options);
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0003A770 File Offset: 0x00038970
		private int AddBacktrackNote(int flags, Label l, int codepos)
		{
			if (this._notes == null || this._notecount >= this._notes.Length)
			{
				RegexCompiler.BacktrackNote[] array = new RegexCompiler.BacktrackNote[(this._notes == null) ? 16 : (this._notes.Length * 2)];
				if (this._notes != null)
				{
					Array.Copy(this._notes, 0, array, 0, this._notecount);
				}
				this._notes = array;
			}
			this._notes[this._notecount] = new RegexCompiler.BacktrackNote(flags, l, codepos);
			int notecount = this._notecount;
			this._notecount = notecount + 1;
			return notecount;
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0003A7FA File Offset: 0x000389FA
		private int AddTrack()
		{
			return this.AddTrack(128);
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0003A807 File Offset: 0x00038A07
		private int AddTrack(int flags)
		{
			return this.AddBacktrackNote(flags, this.DefineLabel(), this._codepos);
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0003A81C File Offset: 0x00038A1C
		private int AddGoto(int destpos)
		{
			if (this._goto[destpos] == -1)
			{
				this._goto[destpos] = this.AddBacktrackNote(0, this._labels[destpos], destpos);
			}
			return this._goto[destpos];
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0003A84D File Offset: 0x00038A4D
		private int AddUniqueTrack(int i)
		{
			return this.AddUniqueTrack(i, 128);
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0003A85B File Offset: 0x00038A5B
		private int AddUniqueTrack(int i, int flags)
		{
			if (this._uniquenote[i] == -1)
			{
				this._uniquenote[i] = this.AddTrack(flags);
			}
			return this._uniquenote[i];
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0003A87F File Offset: 0x00038A7F
		private Label DefineLabel()
		{
			return this._ilg.DefineLabel();
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0003A88C File Offset: 0x00038A8C
		private void MarkLabel(Label l)
		{
			this._ilg.MarkLabel(l);
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0003A89A File Offset: 0x00038A9A
		private int Operand(int i)
		{
			return this._codes[this._codepos + i + 1];
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0003A8AD File Offset: 0x00038AAD
		private bool IsRtl()
		{
			return (this._regexopcode & 64) != 0;
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0003A8BB File Offset: 0x00038ABB
		private bool IsCi()
		{
			return (this._regexopcode & 512) != 0;
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0003A8CC File Offset: 0x00038ACC
		private int Code()
		{
			return this._regexopcode & 63;
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x0003A8D7 File Offset: 0x00038AD7
		private void Ldstr(string str)
		{
			this._ilg.Emit(OpCodes.Ldstr, str);
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0003A8EA File Offset: 0x00038AEA
		private void Ldc(int i)
		{
			if (i <= 127 && i >= -128)
			{
				this._ilg.Emit(OpCodes.Ldc_I4_S, (byte)i);
				return;
			}
			this._ilg.Emit(OpCodes.Ldc_I4, i);
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0003A91A File Offset: 0x00038B1A
		private void LdcI8(long i)
		{
			if (i <= 2147483647L && i >= -2147483648L)
			{
				this.Ldc((int)i);
				this._ilg.Emit(OpCodes.Conv_I8);
				return;
			}
			this._ilg.Emit(OpCodes.Ldc_I8, i);
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0003A958 File Offset: 0x00038B58
		private void Dup()
		{
			this._ilg.Emit(OpCodes.Dup);
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0003A96A File Offset: 0x00038B6A
		private void Ret()
		{
			this._ilg.Emit(OpCodes.Ret);
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0003A97C File Offset: 0x00038B7C
		private void Pop()
		{
			this._ilg.Emit(OpCodes.Pop);
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0003A98E File Offset: 0x00038B8E
		private void Add()
		{
			this._ilg.Emit(OpCodes.Add);
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0003A9A0 File Offset: 0x00038BA0
		private void Add(bool negate)
		{
			if (negate)
			{
				this._ilg.Emit(OpCodes.Sub);
				return;
			}
			this._ilg.Emit(OpCodes.Add);
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0003A9C6 File Offset: 0x00038BC6
		private void Sub()
		{
			this._ilg.Emit(OpCodes.Sub);
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0003A9D8 File Offset: 0x00038BD8
		private void Sub(bool negate)
		{
			if (negate)
			{
				this._ilg.Emit(OpCodes.Add);
				return;
			}
			this._ilg.Emit(OpCodes.Sub);
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0003A9FE File Offset: 0x00038BFE
		private void Ldloc(LocalBuilder lt)
		{
			this._ilg.Emit(OpCodes.Ldloc_S, lt);
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x0003AA11 File Offset: 0x00038C11
		private void Stloc(LocalBuilder lt)
		{
			this._ilg.Emit(OpCodes.Stloc_S, lt);
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x0003AA24 File Offset: 0x00038C24
		private void Ldthis()
		{
			this._ilg.Emit(OpCodes.Ldarg_0);
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0003AA36 File Offset: 0x00038C36
		private void Ldthisfld(FieldInfo ft)
		{
			this.Ldthis();
			this._ilg.Emit(OpCodes.Ldfld, ft);
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0003AA4F File Offset: 0x00038C4F
		private void Mvfldloc(FieldInfo ft, LocalBuilder lt)
		{
			this.Ldthisfld(ft);
			this.Stloc(lt);
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0003AA5F File Offset: 0x00038C5F
		private void Mvlocfld(LocalBuilder lt, FieldInfo ft)
		{
			this.Ldthis();
			this.Ldloc(lt);
			this.Stfld(ft);
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x0003AA75 File Offset: 0x00038C75
		private void Stfld(FieldInfo ft)
		{
			this._ilg.Emit(OpCodes.Stfld, ft);
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0003AA88 File Offset: 0x00038C88
		private void Callvirt(MethodInfo mt)
		{
			this._ilg.Emit(OpCodes.Callvirt, mt);
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0003AA9B File Offset: 0x00038C9B
		private void Call(MethodInfo mt)
		{
			this._ilg.Emit(OpCodes.Call, mt);
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x0003AAAE File Offset: 0x00038CAE
		private void Newobj(ConstructorInfo ct)
		{
			this._ilg.Emit(OpCodes.Newobj, ct);
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0003AAC1 File Offset: 0x00038CC1
		private void BrfalseFar(Label l)
		{
			this._ilg.Emit(OpCodes.Brfalse, l);
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x0003AAD4 File Offset: 0x00038CD4
		private void BrtrueFar(Label l)
		{
			this._ilg.Emit(OpCodes.Brtrue, l);
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0003AAE7 File Offset: 0x00038CE7
		private void BrFar(Label l)
		{
			this._ilg.Emit(OpCodes.Br, l);
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0003AAFA File Offset: 0x00038CFA
		private void BleFar(Label l)
		{
			this._ilg.Emit(OpCodes.Ble, l);
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0003AB0D File Offset: 0x00038D0D
		private void BltFar(Label l)
		{
			this._ilg.Emit(OpCodes.Blt, l);
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0003AB20 File Offset: 0x00038D20
		private void BgeFar(Label l)
		{
			this._ilg.Emit(OpCodes.Bge, l);
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0003AB33 File Offset: 0x00038D33
		private void BgtFar(Label l)
		{
			this._ilg.Emit(OpCodes.Bgt, l);
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0003AB46 File Offset: 0x00038D46
		private void BneFar(Label l)
		{
			this._ilg.Emit(OpCodes.Bne_Un, l);
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x0003AB59 File Offset: 0x00038D59
		private void BeqFar(Label l)
		{
			this._ilg.Emit(OpCodes.Beq, l);
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x0003AB6C File Offset: 0x00038D6C
		private void Brfalse(Label l)
		{
			this._ilg.Emit(OpCodes.Brfalse_S, l);
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0003AB7F File Offset: 0x00038D7F
		private void Br(Label l)
		{
			this._ilg.Emit(OpCodes.Br_S, l);
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0003AB92 File Offset: 0x00038D92
		private void Ble(Label l)
		{
			this._ilg.Emit(OpCodes.Ble_S, l);
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0003ABA5 File Offset: 0x00038DA5
		private void Blt(Label l)
		{
			this._ilg.Emit(OpCodes.Blt_S, l);
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0003ABB8 File Offset: 0x00038DB8
		private void Bge(Label l)
		{
			this._ilg.Emit(OpCodes.Bge_S, l);
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0003ABCB File Offset: 0x00038DCB
		private void Bgt(Label l)
		{
			this._ilg.Emit(OpCodes.Bgt_S, l);
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0003ABDE File Offset: 0x00038DDE
		private void Bgtun(Label l)
		{
			this._ilg.Emit(OpCodes.Bgt_Un_S, l);
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0003ABF1 File Offset: 0x00038DF1
		private void Bne(Label l)
		{
			this._ilg.Emit(OpCodes.Bne_Un_S, l);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0003AC04 File Offset: 0x00038E04
		private void Beq(Label l)
		{
			this._ilg.Emit(OpCodes.Beq_S, l);
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0003AC17 File Offset: 0x00038E17
		private void Ldlen()
		{
			this._ilg.Emit(OpCodes.Ldlen);
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0003AC29 File Offset: 0x00038E29
		private void Rightchar()
		{
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Callvirt(RegexCompiler.s_getcharM);
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0003AC50 File Offset: 0x00038E50
		private void Rightcharnext()
		{
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Dup();
			this.Ldc(1);
			this.Add();
			this.Stloc(this._textposV);
			this.Callvirt(RegexCompiler.s_getcharM);
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0003AC9F File Offset: 0x00038E9F
		private void Leftchar()
		{
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Ldc(1);
			this.Sub();
			this.Callvirt(RegexCompiler.s_getcharM);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0003ACD4 File Offset: 0x00038ED4
		private void Leftcharnext()
		{
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Ldc(1);
			this.Sub();
			this.Dup();
			this.Stloc(this._textposV);
			this.Callvirt(RegexCompiler.s_getcharM);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0003AD23 File Offset: 0x00038F23
		private void Track()
		{
			this.ReadyPushTrack();
			this.Ldc(this.AddTrack());
			this.DoPush();
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0003AD3D File Offset: 0x00038F3D
		private void Trackagain()
		{
			this.ReadyPushTrack();
			this.Ldc(this._backpos);
			this.DoPush();
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0003AD57 File Offset: 0x00038F57
		private void PushTrack(LocalBuilder lt)
		{
			this.ReadyPushTrack();
			this.Ldloc(lt);
			this.DoPush();
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0003AD6C File Offset: 0x00038F6C
		private void TrackUnique(int i)
		{
			this.ReadyPushTrack();
			this.Ldc(this.AddUniqueTrack(i));
			this.DoPush();
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0003AD87 File Offset: 0x00038F87
		private void TrackUnique2(int i)
		{
			this.ReadyPushTrack();
			this.Ldc(this.AddUniqueTrack(i, 256));
			this.DoPush();
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0003ADA8 File Offset: 0x00038FA8
		private void ReadyPushTrack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackposV);
			this._ilg.Emit(OpCodes.Ldc_I4_1);
			this._ilg.Emit(OpCodes.Sub);
			this._ilg.Emit(OpCodes.Dup);
			this._ilg.Emit(OpCodes.Stloc_S, this._trackposV);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0003AE28 File Offset: 0x00039028
		private void PopTrack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackposV);
			this._ilg.Emit(OpCodes.Dup);
			this._ilg.Emit(OpCodes.Ldc_I4_1);
			this._ilg.Emit(OpCodes.Add);
			this._ilg.Emit(OpCodes.Stloc_S, this._trackposV);
			this._ilg.Emit(OpCodes.Ldelem_I4);
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0003AEB7 File Offset: 0x000390B7
		private void TopTrack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackposV);
			this._ilg.Emit(OpCodes.Ldelem_I4);
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0003AEF5 File Offset: 0x000390F5
		private void PushStack(LocalBuilder lt)
		{
			this.ReadyPushStack();
			this._ilg.Emit(OpCodes.Ldloc_S, lt);
			this.DoPush();
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0003AF14 File Offset: 0x00039114
		internal void ReadyReplaceStack(int i)
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackposV);
			if (i != 0)
			{
				this.Ldc(i);
				this._ilg.Emit(OpCodes.Add);
			}
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0003AF68 File Offset: 0x00039168
		private void ReadyPushStack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackposV);
			this._ilg.Emit(OpCodes.Ldc_I4_1);
			this._ilg.Emit(OpCodes.Sub);
			this._ilg.Emit(OpCodes.Dup);
			this._ilg.Emit(OpCodes.Stloc_S, this._stackposV);
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x0003AFE7 File Offset: 0x000391E7
		private void TopStack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackposV);
			this._ilg.Emit(OpCodes.Ldelem_I4);
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x0003B028 File Offset: 0x00039228
		private void PopStack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackposV);
			this._ilg.Emit(OpCodes.Dup);
			this._ilg.Emit(OpCodes.Ldc_I4_1);
			this._ilg.Emit(OpCodes.Add);
			this._ilg.Emit(OpCodes.Stloc_S, this._stackposV);
			this._ilg.Emit(OpCodes.Ldelem_I4);
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0003B0B7 File Offset: 0x000392B7
		private void PopDiscardStack()
		{
			this.PopDiscardStack(1);
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0003B0C0 File Offset: 0x000392C0
		private void PopDiscardStack(int i)
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackposV);
			this.Ldc(i);
			this._ilg.Emit(OpCodes.Add);
			this._ilg.Emit(OpCodes.Stloc_S, this._stackposV);
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x0003B110 File Offset: 0x00039310
		private void DoReplace()
		{
			this._ilg.Emit(OpCodes.Stelem_I4);
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x0003B110 File Offset: 0x00039310
		private void DoPush()
		{
			this._ilg.Emit(OpCodes.Stelem_I4);
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0003B122 File Offset: 0x00039322
		private void Back()
		{
			this._ilg.Emit(OpCodes.Br, this._backtrack);
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0003B13C File Offset: 0x0003933C
		private void Goto(int i)
		{
			if (i < this._codepos)
			{
				Label l = this.DefineLabel();
				this.Ldloc(this._trackposV);
				this.Ldc(this._trackcount * 4);
				this.Ble(l);
				this.Ldloc(this._stackposV);
				this.Ldc(this._trackcount * 3);
				this.BgtFar(this._labels[i]);
				this.MarkLabel(l);
				this.ReadyPushTrack();
				this.Ldc(this.AddGoto(i));
				this.DoPush();
				this.BrFar(this._backtrack);
				return;
			}
			this.BrFar(this._labels[i]);
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0003B1E8 File Offset: 0x000393E8
		private int NextCodepos()
		{
			return this._codepos + RegexCode.OpcodeSize(this._codes[this._codepos]);
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0003B203 File Offset: 0x00039403
		private Label AdvanceLabel()
		{
			return this._labels[this.NextCodepos()];
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0003B216 File Offset: 0x00039416
		private void Advance()
		{
			this._ilg.Emit(OpCodes.Br, this.AdvanceLabel());
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x0003B22E File Offset: 0x0003942E
		private void CallToLower()
		{
			if ((this._options & RegexOptions.CultureInvariant) != RegexOptions.None)
			{
				this.Call(RegexCompiler.s_getInvariantCulture);
			}
			else
			{
				this.Call(RegexCompiler.s_getCurrentCulture);
			}
			this.Call(RegexCompiler.s_chartolowerM);
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0003B264 File Offset: 0x00039464
		private void GenerateForwardSection()
		{
			this._labels = new Label[this._codes.Length];
			this._goto = new int[this._codes.Length];
			for (int i = 0; i < this._codes.Length; i += RegexCode.OpcodeSize(this._codes[i]))
			{
				this._goto[i] = -1;
				this._labels[i] = this._ilg.DefineLabel();
			}
			this._uniquenote = new int[10];
			for (int j = 0; j < 10; j++)
			{
				this._uniquenote[j] = -1;
			}
			this.Mvfldloc(RegexCompiler.s_textF, this._textV);
			this.Mvfldloc(RegexCompiler.s_textstartF, this._textstartV);
			this.Mvfldloc(RegexCompiler.s_textbegF, this._textbegV);
			this.Mvfldloc(RegexCompiler.s_textendF, this._textendV);
			this.Mvfldloc(RegexCompiler.s_textposF, this._textposV);
			this.Mvfldloc(RegexCompiler.s_trackF, this._trackV);
			this.Mvfldloc(RegexCompiler.s_trackposF, this._trackposV);
			this.Mvfldloc(RegexCompiler.s_stackF, this._stackV);
			this.Mvfldloc(RegexCompiler.s_stackposF, this._stackposV);
			this._backpos = -1;
			for (int i = 0; i < this._codes.Length; i += RegexCode.OpcodeSize(this._codes[i]))
			{
				this.MarkLabel(this._labels[i]);
				this._codepos = i;
				this._regexopcode = this._codes[i];
				this.GenerateOneCode();
			}
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0003B3E8 File Offset: 0x000395E8
		private void GenerateMiddleSection()
		{
			this.DefineLabel();
			this.MarkLabel(this._backtrack);
			this.Mvlocfld(this._trackposV, RegexCompiler.s_trackposF);
			this.Mvlocfld(this._stackposV, RegexCompiler.s_stackposF);
			this.Ldthis();
			this.Callvirt(RegexCompiler.s_ensurestorageM);
			this.Mvfldloc(RegexCompiler.s_trackposF, this._trackposV);
			this.Mvfldloc(RegexCompiler.s_stackposF, this._stackposV);
			this.Mvfldloc(RegexCompiler.s_trackF, this._trackV);
			this.Mvfldloc(RegexCompiler.s_stackF, this._stackV);
			this.PopTrack();
			Label[] array = new Label[this._notecount];
			for (int i = 0; i < this._notecount; i++)
			{
				array[i] = this._notes[i]._label;
			}
			this._ilg.Emit(OpCodes.Switch, array);
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0003B4C8 File Offset: 0x000396C8
		private void GenerateBacktrackSection()
		{
			for (int i = 0; i < this._notecount; i++)
			{
				RegexCompiler.BacktrackNote backtrackNote = this._notes[i];
				if (backtrackNote._flags != 0)
				{
					this._ilg.MarkLabel(backtrackNote._label);
					this._codepos = backtrackNote._codepos;
					this._backpos = i;
					this._regexopcode = (this._codes[backtrackNote._codepos] | backtrackNote._flags);
					this.GenerateOneCode();
				}
			}
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0003B53C File Offset: 0x0003973C
		protected void GenerateFindFirstChar()
		{
			this._textposV = this.DeclareInt();
			this._textV = this.DeclareString();
			this._tempV = this.DeclareInt();
			this._temp2V = this.DeclareInt();
			if ((this._anchors & 53) != 0)
			{
				if (!this._code.RightToLeft)
				{
					if ((this._anchors & 1) != 0)
					{
						Label l = this.DefineLabel();
						this.Ldthisfld(RegexCompiler.s_textposF);
						this.Ldthisfld(RegexCompiler.s_textbegF);
						this.Ble(l);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler.s_textendF);
						this.Stfld(RegexCompiler.s_textposF);
						this.Ldc(0);
						this.Ret();
						this.MarkLabel(l);
					}
					if ((this._anchors & 4) != 0)
					{
						Label l2 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler.s_textposF);
						this.Ldthisfld(RegexCompiler.s_textstartF);
						this.Ble(l2);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler.s_textendF);
						this.Stfld(RegexCompiler.s_textposF);
						this.Ldc(0);
						this.Ret();
						this.MarkLabel(l2);
					}
					if ((this._anchors & 16) != 0)
					{
						Label l3 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler.s_textposF);
						this.Ldthisfld(RegexCompiler.s_textendF);
						this.Ldc(1);
						this.Sub();
						this.Bge(l3);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler.s_textendF);
						this.Ldc(1);
						this.Sub();
						this.Stfld(RegexCompiler.s_textposF);
						this.MarkLabel(l3);
					}
					if ((this._anchors & 32) != 0)
					{
						Label l4 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler.s_textposF);
						this.Ldthisfld(RegexCompiler.s_textendF);
						this.Bge(l4);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler.s_textendF);
						this.Stfld(RegexCompiler.s_textposF);
						this.MarkLabel(l4);
					}
				}
				else
				{
					if ((this._anchors & 32) != 0)
					{
						Label l5 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler.s_textposF);
						this.Ldthisfld(RegexCompiler.s_textendF);
						this.Bge(l5);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler.s_textbegF);
						this.Stfld(RegexCompiler.s_textposF);
						this.Ldc(0);
						this.Ret();
						this.MarkLabel(l5);
					}
					if ((this._anchors & 16) != 0)
					{
						Label l6 = this.DefineLabel();
						Label l7 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler.s_textposF);
						this.Ldthisfld(RegexCompiler.s_textendF);
						this.Ldc(1);
						this.Sub();
						this.Blt(l6);
						this.Ldthisfld(RegexCompiler.s_textposF);
						this.Ldthisfld(RegexCompiler.s_textendF);
						this.Beq(l7);
						this.Ldthisfld(RegexCompiler.s_textF);
						this.Ldthisfld(RegexCompiler.s_textposF);
						this.Callvirt(RegexCompiler.s_getcharM);
						this.Ldc(10);
						this.Beq(l7);
						this.MarkLabel(l6);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler.s_textbegF);
						this.Stfld(RegexCompiler.s_textposF);
						this.Ldc(0);
						this.Ret();
						this.MarkLabel(l7);
					}
					if ((this._anchors & 4) != 0)
					{
						Label l8 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler.s_textposF);
						this.Ldthisfld(RegexCompiler.s_textstartF);
						this.Bge(l8);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler.s_textbegF);
						this.Stfld(RegexCompiler.s_textposF);
						this.Ldc(0);
						this.Ret();
						this.MarkLabel(l8);
					}
					if ((this._anchors & 1) != 0)
					{
						Label l9 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler.s_textposF);
						this.Ldthisfld(RegexCompiler.s_textbegF);
						this.Ble(l9);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler.s_textbegF);
						this.Stfld(RegexCompiler.s_textposF);
						this.MarkLabel(l9);
					}
				}
				this.Ldc(1);
				this.Ret();
				return;
			}
			if (this._bmPrefix != null && this._bmPrefix.NegativeUnicode == null)
			{
				LocalBuilder tempV = this._tempV;
				LocalBuilder tempV2 = this._tempV;
				LocalBuilder temp2V = this._temp2V;
				Label label = this.DefineLabel();
				Label l10 = this.DefineLabel();
				Label l11 = this.DefineLabel();
				Label l12 = this.DefineLabel();
				this.DefineLabel();
				Label l13 = this.DefineLabel();
				int num;
				int index;
				if (!this._code.RightToLeft)
				{
					num = -1;
					index = this._bmPrefix.Pattern.Length - 1;
				}
				else
				{
					num = this._bmPrefix.Pattern.Length;
					index = 0;
				}
				int i = (int)this._bmPrefix.Pattern[index];
				this.Mvfldloc(RegexCompiler.s_textF, this._textV);
				if (!this._code.RightToLeft)
				{
					this.Ldthisfld(RegexCompiler.s_textendF);
				}
				else
				{
					this.Ldthisfld(RegexCompiler.s_textbegF);
				}
				this.Stloc(temp2V);
				this.Ldthisfld(RegexCompiler.s_textposF);
				if (!this._code.RightToLeft)
				{
					this.Ldc(this._bmPrefix.Pattern.Length - 1);
					this.Add();
				}
				else
				{
					this.Ldc(this._bmPrefix.Pattern.Length);
					this.Sub();
				}
				this.Stloc(this._textposV);
				this.Br(l12);
				this.MarkLabel(label);
				if (!this._code.RightToLeft)
				{
					this.Ldc(this._bmPrefix.Pattern.Length);
				}
				else
				{
					this.Ldc(-this._bmPrefix.Pattern.Length);
				}
				this.MarkLabel(l10);
				this.Ldloc(this._textposV);
				this.Add();
				this.Stloc(this._textposV);
				this.MarkLabel(l12);
				this.Ldloc(this._textposV);
				this.Ldloc(temp2V);
				if (!this._code.RightToLeft)
				{
					this.BgeFar(l11);
				}
				else
				{
					this.BltFar(l11);
				}
				this.Rightchar();
				if (this._bmPrefix.CaseInsensitive)
				{
					this.CallToLower();
				}
				this.Dup();
				this.Stloc(tempV);
				this.Ldc(i);
				this.BeqFar(l13);
				this.Ldloc(tempV);
				this.Ldc(this._bmPrefix.LowASCII);
				this.Sub();
				this.Dup();
				this.Stloc(tempV);
				this.Ldc(this._bmPrefix.HighASCII - this._bmPrefix.LowASCII);
				this.Bgtun(label);
				Label[] array = new Label[this._bmPrefix.HighASCII - this._bmPrefix.LowASCII + 1];
				for (int j = this._bmPrefix.LowASCII; j <= this._bmPrefix.HighASCII; j++)
				{
					if (this._bmPrefix.NegativeASCII[j] == num)
					{
						array[j - this._bmPrefix.LowASCII] = label;
					}
					else
					{
						array[j - this._bmPrefix.LowASCII] = this.DefineLabel();
					}
				}
				this.Ldloc(tempV);
				this._ilg.Emit(OpCodes.Switch, array);
				for (int j = this._bmPrefix.LowASCII; j <= this._bmPrefix.HighASCII; j++)
				{
					if (this._bmPrefix.NegativeASCII[j] != num)
					{
						this.MarkLabel(array[j - this._bmPrefix.LowASCII]);
						this.Ldc(this._bmPrefix.NegativeASCII[j]);
						this.BrFar(l10);
					}
				}
				this.MarkLabel(l13);
				this.Ldloc(this._textposV);
				this.Stloc(tempV2);
				for (int j = this._bmPrefix.Pattern.Length - 2; j >= 0; j--)
				{
					Label l14 = this.DefineLabel();
					int num2;
					if (!this._code.RightToLeft)
					{
						num2 = j;
					}
					else
					{
						num2 = this._bmPrefix.Pattern.Length - 1 - j;
					}
					this.Ldloc(this._textV);
					this.Ldloc(tempV2);
					this.Ldc(1);
					this.Sub(this._code.RightToLeft);
					this.Dup();
					this.Stloc(tempV2);
					this.Callvirt(RegexCompiler.s_getcharM);
					if (this._bmPrefix.CaseInsensitive)
					{
						this.CallToLower();
					}
					this.Ldc((int)this._bmPrefix.Pattern[num2]);
					this.Beq(l14);
					this.Ldc(this._bmPrefix.Positive[num2]);
					this.BrFar(l10);
					this.MarkLabel(l14);
				}
				this.Ldthis();
				this.Ldloc(tempV2);
				if (this._code.RightToLeft)
				{
					this.Ldc(1);
					this.Add();
				}
				this.Stfld(RegexCompiler.s_textposF);
				this.Ldc(1);
				this.Ret();
				this.MarkLabel(l11);
				this.Ldthis();
				if (!this._code.RightToLeft)
				{
					this.Ldthisfld(RegexCompiler.s_textendF);
				}
				else
				{
					this.Ldthisfld(RegexCompiler.s_textbegF);
				}
				this.Stfld(RegexCompiler.s_textposF);
				this.Ldc(0);
				this.Ret();
				return;
			}
			if (this._fcPrefix == null)
			{
				this.Ldc(1);
				this.Ret();
				return;
			}
			LocalBuilder temp2V2 = this._temp2V;
			LocalBuilder tempV3 = this._tempV;
			Label l15 = this.DefineLabel();
			Label l16 = this.DefineLabel();
			Label l17 = this.DefineLabel();
			Label l18 = this.DefineLabel();
			Label l19 = this.DefineLabel();
			this.Mvfldloc(RegexCompiler.s_textposF, this._textposV);
			this.Mvfldloc(RegexCompiler.s_textF, this._textV);
			if (!this._code.RightToLeft)
			{
				this.Ldthisfld(RegexCompiler.s_textendF);
				this.Ldloc(this._textposV);
			}
			else
			{
				this.Ldloc(this._textposV);
				this.Ldthisfld(RegexCompiler.s_textbegF);
			}
			this.Sub();
			this.Stloc(temp2V2);
			this.Ldloc(temp2V2);
			this.Ldc(0);
			this.BleFar(l18);
			this.MarkLabel(l15);
			this.Ldloc(temp2V2);
			this.Ldc(1);
			this.Sub();
			this.Stloc(temp2V2);
			if (this._code.RightToLeft)
			{
				this.Leftcharnext();
			}
			else
			{
				this.Rightcharnext();
			}
			if (this._fcPrefix.GetValueOrDefault().CaseInsensitive)
			{
				this.CallToLower();
			}
			if (!RegexCharClass.IsSingleton(this._fcPrefix.GetValueOrDefault().Prefix))
			{
				this.Ldstr(this._fcPrefix.GetValueOrDefault().Prefix);
				this.Call(RegexCompiler.s_charInSetM);
				this.BrtrueFar(l16);
			}
			else
			{
				this.Ldc((int)RegexCharClass.SingletonChar(this._fcPrefix.GetValueOrDefault().Prefix));
				this.Beq(l16);
			}
			this.MarkLabel(l19);
			this.Ldloc(temp2V2);
			this.Ldc(0);
			if (!RegexCharClass.IsSingleton(this._fcPrefix.GetValueOrDefault().Prefix))
			{
				this.BgtFar(l15);
			}
			else
			{
				this.Bgt(l15);
			}
			this.Ldc(0);
			this.BrFar(l17);
			this.MarkLabel(l16);
			this.Ldloc(this._textposV);
			this.Ldc(1);
			this.Sub(this._code.RightToLeft);
			this.Stloc(this._textposV);
			this.Ldc(1);
			this.MarkLabel(l17);
			this.Mvlocfld(this._textposV, RegexCompiler.s_textposF);
			this.Ret();
			this.MarkLabel(l18);
			this.Ldc(0);
			this.Ret();
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x0003C0AB File Offset: 0x0003A2AB
		protected void GenerateInitTrackCount()
		{
			this.Ldthis();
			this.Ldc(this._trackcount);
			this.Stfld(RegexCompiler.s_trackcountF);
			this.Ret();
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x0003C0D0 File Offset: 0x0003A2D0
		private LocalBuilder DeclareInt()
		{
			return this._ilg.DeclareLocal(typeof(int));
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x0003C0E7 File Offset: 0x0003A2E7
		private LocalBuilder DeclareIntArray()
		{
			return this._ilg.DeclareLocal(typeof(int[]));
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0003C0FE File Offset: 0x0003A2FE
		private LocalBuilder DeclareString()
		{
			return this._ilg.DeclareLocal(typeof(string));
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x0003C118 File Offset: 0x0003A318
		protected void GenerateGo()
		{
			this._textposV = this.DeclareInt();
			this._textV = this.DeclareString();
			this._trackposV = this.DeclareInt();
			this._trackV = this.DeclareIntArray();
			this._stackposV = this.DeclareInt();
			this._stackV = this.DeclareIntArray();
			this._tempV = this.DeclareInt();
			this._temp2V = this.DeclareInt();
			this._temp3V = this.DeclareInt();
			this._textbegV = this.DeclareInt();
			this._textendV = this.DeclareInt();
			this._textstartV = this.DeclareInt();
			this._labels = null;
			this._notes = null;
			this._notecount = 0;
			this._backtrack = this.DefineLabel();
			this.GenerateForwardSection();
			this.GenerateMiddleSection();
			this.GenerateBacktrackSection();
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0003C1E8 File Offset: 0x0003A3E8
		private void GenerateOneCode()
		{
			this.Ldthis();
			this.Callvirt(RegexCompiler.s_checkTimeoutM);
			int regexopcode = this._regexopcode;
			if (regexopcode <= 285)
			{
				if (regexopcode <= 164)
				{
					switch (regexopcode)
					{
					case 0:
					case 1:
					case 2:
					case 64:
					case 65:
					case 66:
						goto IL_1438;
					case 3:
					case 4:
					case 5:
					case 67:
					case 68:
					case 69:
						goto IL_1604;
					case 6:
					case 7:
					case 8:
					case 70:
					case 71:
					case 72:
						goto IL_18EF;
					case 9:
					case 10:
					case 11:
					case 73:
					case 74:
					case 75:
						break;
					case 12:
						goto IL_1024;
					case 13:
					case 77:
						goto IL_11F6;
					case 14:
					{
						Label l = this._labels[this.NextCodepos()];
						this.Ldloc(this._textposV);
						this.Ldloc(this._textbegV);
						this.Ble(l);
						this.Leftchar();
						this.Ldc(10);
						this.BneFar(this._backtrack);
						return;
					}
					case 15:
					{
						Label l2 = this._labels[this.NextCodepos()];
						this.Ldloc(this._textposV);
						this.Ldloc(this._textendV);
						this.Bge(l2);
						this.Rightchar();
						this.Ldc(10);
						this.BneFar(this._backtrack);
						return;
					}
					case 16:
					case 17:
						this.Ldthis();
						this.Ldloc(this._textposV);
						this.Ldloc(this._textbegV);
						this.Ldloc(this._textendV);
						this.Callvirt(RegexCompiler.s_isboundaryM);
						if (this.Code() == 16)
						{
							this.BrfalseFar(this._backtrack);
							return;
						}
						this.BrtrueFar(this._backtrack);
						return;
					case 18:
						this.Ldloc(this._textposV);
						this.Ldloc(this._textbegV);
						this.BgtFar(this._backtrack);
						return;
					case 19:
						this.Ldloc(this._textposV);
						this.Ldthisfld(RegexCompiler.s_textstartF);
						this.BneFar(this._backtrack);
						return;
					case 20:
						this.Ldloc(this._textposV);
						this.Ldloc(this._textendV);
						this.Ldc(1);
						this.Sub();
						this.BltFar(this._backtrack);
						this.Ldloc(this._textposV);
						this.Ldloc(this._textendV);
						this.Bge(this._labels[this.NextCodepos()]);
						this.Rightchar();
						this.Ldc(10);
						this.BneFar(this._backtrack);
						return;
					case 21:
						this.Ldloc(this._textposV);
						this.Ldloc(this._textendV);
						this.BltFar(this._backtrack);
						return;
					case 22:
						this.Back();
						return;
					case 23:
						this.PushTrack(this._textposV);
						this.Track();
						return;
					case 24:
					{
						LocalBuilder tempV = this._tempV;
						Label l3 = this.DefineLabel();
						this.PopStack();
						this.Dup();
						this.Stloc(tempV);
						this.PushTrack(tempV);
						this.Ldloc(this._textposV);
						this.Beq(l3);
						this.PushTrack(this._textposV);
						this.PushStack(this._textposV);
						this.Track();
						this.Goto(this.Operand(0));
						this.MarkLabel(l3);
						this.TrackUnique2(5);
						return;
					}
					case 25:
					{
						LocalBuilder tempV2 = this._tempV;
						Label l4 = this.DefineLabel();
						Label l5 = this.DefineLabel();
						Label l6 = this.DefineLabel();
						this.PopStack();
						this.Dup();
						this.Stloc(tempV2);
						this.Ldloc(tempV2);
						this.Ldc(-1);
						this.Beq(l5);
						this.PushTrack(tempV2);
						this.Br(l6);
						this.MarkLabel(l5);
						this.PushTrack(this._textposV);
						this.MarkLabel(l6);
						this.Ldloc(this._textposV);
						this.Beq(l4);
						this.PushTrack(this._textposV);
						this.Track();
						this.Br(this.AdvanceLabel());
						this.MarkLabel(l4);
						this.ReadyPushStack();
						this.Ldloc(tempV2);
						this.DoPush();
						this.TrackUnique2(6);
						return;
					}
					case 26:
						this.ReadyPushStack();
						this.Ldc(-1);
						this.DoPush();
						this.ReadyPushStack();
						this.Ldc(this.Operand(0));
						this.DoPush();
						this.TrackUnique(1);
						return;
					case 27:
						this.PushStack(this._textposV);
						this.ReadyPushStack();
						this.Ldc(this.Operand(0));
						this.DoPush();
						this.TrackUnique(1);
						return;
					case 28:
					{
						LocalBuilder tempV3 = this._tempV;
						LocalBuilder temp2V = this._temp2V;
						Label l7 = this.DefineLabel();
						Label l8 = this.DefineLabel();
						this.PopStack();
						this.Stloc(tempV3);
						this.PopStack();
						this.Dup();
						this.Stloc(temp2V);
						this.PushTrack(temp2V);
						this.Ldloc(this._textposV);
						this.Bne(l7);
						this.Ldloc(tempV3);
						this.Ldc(0);
						this.Bge(l8);
						this.MarkLabel(l7);
						this.Ldloc(tempV3);
						this.Ldc(this.Operand(1));
						this.Bge(l8);
						this.PushStack(this._textposV);
						this.ReadyPushStack();
						this.Ldloc(tempV3);
						this.Ldc(1);
						this.Add();
						this.DoPush();
						this.Track();
						this.Goto(this.Operand(0));
						this.MarkLabel(l8);
						this.PushTrack(tempV3);
						this.TrackUnique2(7);
						return;
					}
					case 29:
					{
						LocalBuilder tempV4 = this._tempV;
						LocalBuilder temp2V2 = this._temp2V;
						Label l9 = this.DefineLabel();
						this.DefineLabel();
						Label[] labels = this._labels;
						this.NextCodepos();
						this.PopStack();
						this.Stloc(tempV4);
						this.PopStack();
						this.Stloc(temp2V2);
						this.Ldloc(tempV4);
						this.Ldc(0);
						this.Bge(l9);
						this.PushTrack(temp2V2);
						this.PushStack(this._textposV);
						this.ReadyPushStack();
						this.Ldloc(tempV4);
						this.Ldc(1);
						this.Add();
						this.DoPush();
						this.TrackUnique2(8);
						this.Goto(this.Operand(0));
						this.MarkLabel(l9);
						this.PushTrack(temp2V2);
						this.PushTrack(tempV4);
						this.PushTrack(this._textposV);
						this.Track();
						return;
					}
					case 30:
						this.ReadyPushStack();
						this.Ldc(-1);
						this.DoPush();
						this.TrackUnique(0);
						return;
					case 31:
						this.PushStack(this._textposV);
						this.TrackUnique(0);
						return;
					case 32:
						if (this.Operand(1) != -1)
						{
							this.Ldthis();
							this.Ldc(this.Operand(1));
							this.Callvirt(RegexCompiler.s_ismatchedM);
							this.BrfalseFar(this._backtrack);
						}
						this.PopStack();
						this.Stloc(this._tempV);
						if (this.Operand(1) != -1)
						{
							this.Ldthis();
							this.Ldc(this.Operand(0));
							this.Ldc(this.Operand(1));
							this.Ldloc(this._tempV);
							this.Ldloc(this._textposV);
							this.Callvirt(RegexCompiler.s_transferM);
						}
						else
						{
							this.Ldthis();
							this.Ldc(this.Operand(0));
							this.Ldloc(this._tempV);
							this.Ldloc(this._textposV);
							this.Callvirt(RegexCompiler.s_captureM);
						}
						this.PushTrack(this._tempV);
						if (this.Operand(0) != -1 && this.Operand(1) != -1)
						{
							this.TrackUnique(4);
							return;
						}
						this.TrackUnique(3);
						return;
					case 33:
						this.ReadyPushTrack();
						this.PopStack();
						this.Dup();
						this.Stloc(this._textposV);
						this.DoPush();
						this.Track();
						return;
					case 34:
						this.ReadyPushStack();
						this.Ldthisfld(RegexCompiler.s_trackF);
						this.Ldlen();
						this.Ldloc(this._trackposV);
						this.Sub();
						this.DoPush();
						this.ReadyPushStack();
						this.Ldthis();
						this.Callvirt(RegexCompiler.s_crawlposM);
						this.DoPush();
						this.TrackUnique(1);
						return;
					case 35:
					{
						Label l10 = this.DefineLabel();
						Label l11 = this.DefineLabel();
						this.PopStack();
						this.Ldthisfld(RegexCompiler.s_trackF);
						this.Ldlen();
						this.PopStack();
						this.Sub();
						this.Stloc(this._trackposV);
						this.Dup();
						this.Ldthis();
						this.Callvirt(RegexCompiler.s_crawlposM);
						this.Beq(l11);
						this.MarkLabel(l10);
						this.Ldthis();
						this.Callvirt(RegexCompiler.s_uncaptureM);
						this.Dup();
						this.Ldthis();
						this.Callvirt(RegexCompiler.s_crawlposM);
						this.Bne(l10);
						this.MarkLabel(l11);
						this.Pop();
						this.Back();
						return;
					}
					case 36:
						this.PopStack();
						this.Stloc(this._tempV);
						this.Ldthisfld(RegexCompiler.s_trackF);
						this.Ldlen();
						this.PopStack();
						this.Sub();
						this.Stloc(this._trackposV);
						this.PushTrack(this._tempV);
						this.TrackUnique(9);
						return;
					case 37:
						this.Ldthis();
						this.Ldc(this.Operand(0));
						this.Callvirt(RegexCompiler.s_ismatchedM);
						this.BrfalseFar(this._backtrack);
						return;
					case 38:
						this.Goto(this.Operand(0));
						return;
					case 39:
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
					case 56:
					case 57:
					case 58:
					case 59:
					case 60:
					case 61:
					case 62:
					case 63:
						goto IL_1AE4;
					case 40:
						this.Mvlocfld(this._textposV, RegexCompiler.s_textposF);
						this.Ret();
						return;
					case 41:
					case 42:
						this.Ldthis();
						this.Ldloc(this._textposV);
						this.Ldloc(this._textbegV);
						this.Ldloc(this._textendV);
						this.Callvirt(RegexCompiler.s_isECMABoundaryM);
						if (this.Code() == 41)
						{
							this.BrfalseFar(this._backtrack);
							return;
						}
						this.BrtrueFar(this._backtrack);
						return;
					case 76:
						goto IL_110B;
					default:
						switch (regexopcode)
						{
						case 131:
						case 132:
						case 133:
							goto IL_184F;
						case 134:
						case 135:
						case 136:
							goto IL_19D9;
						case 137:
						case 138:
						case 139:
						case 140:
						case 141:
						case 142:
						case 143:
						case 144:
						case 145:
						case 146:
						case 147:
						case 148:
						case 149:
						case 150:
						case 163:
							goto IL_1AE4;
						case 151:
							this.PopTrack();
							this.Stloc(this._textposV);
							this.Goto(this.Operand(0));
							return;
						case 152:
							this.PopTrack();
							this.Stloc(this._textposV);
							this.PopStack();
							this.Pop();
							this.TrackUnique2(5);
							this.Advance();
							return;
						case 153:
							this.PopTrack();
							this.Stloc(this._textposV);
							this.PushStack(this._textposV);
							this.TrackUnique2(6);
							this.Goto(this.Operand(0));
							return;
						case 154:
						case 155:
							this.PopDiscardStack(2);
							this.Back();
							return;
						case 156:
						{
							LocalBuilder tempV5 = this._tempV;
							Label l12 = this.DefineLabel();
							this.PopStack();
							this.Ldc(1);
							this.Sub();
							this.Dup();
							this.Stloc(tempV5);
							this.Ldc(0);
							this.Blt(l12);
							this.PopStack();
							this.Stloc(this._textposV);
							this.PushTrack(tempV5);
							this.TrackUnique2(7);
							this.Advance();
							this.MarkLabel(l12);
							this.ReadyReplaceStack(0);
							this.PopTrack();
							this.DoReplace();
							this.PushStack(tempV5);
							this.Back();
							return;
						}
						case 157:
						{
							Label l13 = this.DefineLabel();
							LocalBuilder tempV6 = this._tempV;
							this.PopTrack();
							this.Stloc(this._textposV);
							this.PopTrack();
							this.Dup();
							this.Stloc(tempV6);
							this.Ldc(this.Operand(1));
							this.Bge(l13);
							this.Ldloc(this._textposV);
							this.TopTrack();
							this.Beq(l13);
							this.PushStack(this._textposV);
							this.ReadyPushStack();
							this.Ldloc(tempV6);
							this.Ldc(1);
							this.Add();
							this.DoPush();
							this.TrackUnique2(8);
							this.Goto(this.Operand(0));
							this.MarkLabel(l13);
							this.ReadyPushStack();
							this.PopTrack();
							this.DoPush();
							this.PushStack(tempV6);
							this.Back();
							return;
						}
						case 158:
						case 159:
							this.PopDiscardStack();
							this.Back();
							return;
						case 160:
							this.ReadyPushStack();
							this.PopTrack();
							this.DoPush();
							this.Ldthis();
							this.Callvirt(RegexCompiler.s_uncaptureM);
							if (this.Operand(0) != -1 && this.Operand(1) != -1)
							{
								this.Ldthis();
								this.Callvirt(RegexCompiler.s_uncaptureM);
							}
							this.Back();
							return;
						case 161:
							this.ReadyPushStack();
							this.PopTrack();
							this.DoPush();
							this.Back();
							return;
						case 162:
							this.PopDiscardStack(2);
							this.Back();
							return;
						case 164:
						{
							Label l14 = this.DefineLabel();
							Label l15 = this.DefineLabel();
							this.PopTrack();
							this.Dup();
							this.Ldthis();
							this.Callvirt(RegexCompiler.s_crawlposM);
							this.Beq(l15);
							this.MarkLabel(l14);
							this.Ldthis();
							this.Callvirt(RegexCompiler.s_uncaptureM);
							this.Dup();
							this.Ldthis();
							this.Callvirt(RegexCompiler.s_crawlposM);
							this.Bne(l14);
							this.MarkLabel(l15);
							this.Pop();
							this.Back();
							return;
						}
						default:
							goto IL_1AE4;
						}
						break;
					}
				}
				else
				{
					if (regexopcode - 195 <= 2)
					{
						goto IL_184F;
					}
					if (regexopcode - 198 <= 2)
					{
						goto IL_19D9;
					}
					switch (regexopcode)
					{
					case 280:
						this.ReadyPushStack();
						this.PopTrack();
						this.DoPush();
						this.Back();
						return;
					case 281:
						this.ReadyReplaceStack(0);
						this.PopTrack();
						this.DoReplace();
						this.Back();
						return;
					case 282:
					case 283:
						goto IL_1AE4;
					case 284:
						this.PopTrack();
						this.Stloc(this._tempV);
						this.ReadyPushStack();
						this.PopTrack();
						this.DoPush();
						this.PushStack(this._tempV);
						this.Back();
						return;
					case 285:
						this.ReadyReplaceStack(1);
						this.PopTrack();
						this.DoReplace();
						this.ReadyReplaceStack(0);
						this.TopStack();
						this.Ldc(1);
						this.Sub();
						this.DoReplace();
						this.Back();
						return;
					default:
						goto IL_1AE4;
					}
				}
			}
			else if (regexopcode <= 645)
			{
				switch (regexopcode)
				{
				case 512:
				case 513:
				case 514:
					goto IL_1438;
				case 515:
				case 516:
				case 517:
					goto IL_1604;
				case 518:
				case 519:
				case 520:
					goto IL_18EF;
				case 521:
				case 522:
				case 523:
					break;
				case 524:
					goto IL_1024;
				case 525:
					goto IL_11F6;
				default:
					switch (regexopcode)
					{
					case 576:
					case 577:
					case 578:
						goto IL_1438;
					case 579:
					case 580:
					case 581:
						goto IL_1604;
					case 582:
					case 583:
					case 584:
						goto IL_18EF;
					case 585:
					case 586:
					case 587:
						break;
					case 588:
						goto IL_110B;
					case 589:
						goto IL_11F6;
					default:
						if (regexopcode - 643 > 2)
						{
							goto IL_1AE4;
						}
						goto IL_184F;
					}
					break;
				}
			}
			else
			{
				if (regexopcode - 646 <= 2)
				{
					goto IL_19D9;
				}
				if (regexopcode - 707 <= 2)
				{
					goto IL_184F;
				}
				if (regexopcode - 710 > 2)
				{
					goto IL_1AE4;
				}
				goto IL_19D9;
			}
			this.Ldloc(this._textposV);
			if (!this.IsRtl())
			{
				this.Ldloc(this._textendV);
				this.BgeFar(this._backtrack);
				this.Rightcharnext();
			}
			else
			{
				this.Ldloc(this._textbegV);
				this.BleFar(this._backtrack);
				this.Leftcharnext();
			}
			if (this.IsCi())
			{
				this.CallToLower();
			}
			if (this.Code() == 11)
			{
				this.Ldstr(this._strings[this.Operand(0)]);
				this.Call(RegexCompiler.s_charInSetM);
				this.BrfalseFar(this._backtrack);
				return;
			}
			this.Ldc(this.Operand(0));
			if (this.Code() == 9)
			{
				this.BneFar(this._backtrack);
				return;
			}
			this.BeqFar(this._backtrack);
			return;
			IL_1024:
			string text = this._strings[this.Operand(0)];
			this.Ldc(text.Length);
			this.Ldloc(this._textendV);
			this.Ldloc(this._textposV);
			this.Sub();
			this.BgtFar(this._backtrack);
			for (int i = 0; i < text.Length; i++)
			{
				this.Ldloc(this._textV);
				this.Ldloc(this._textposV);
				if (i != 0)
				{
					this.Ldc(i);
					this.Add();
				}
				this.Callvirt(RegexCompiler.s_getcharM);
				if (this.IsCi())
				{
					this.CallToLower();
				}
				this.Ldc((int)text[i]);
				this.BneFar(this._backtrack);
			}
			this.Ldloc(this._textposV);
			this.Ldc(text.Length);
			this.Add();
			this.Stloc(this._textposV);
			return;
			IL_110B:
			string text2 = this._strings[this.Operand(0)];
			this.Ldc(text2.Length);
			this.Ldloc(this._textposV);
			this.Ldloc(this._textbegV);
			this.Sub();
			this.BgtFar(this._backtrack);
			int j = text2.Length;
			while (j > 0)
			{
				j--;
				this.Ldloc(this._textV);
				this.Ldloc(this._textposV);
				this.Ldc(text2.Length - j);
				this.Sub();
				this.Callvirt(RegexCompiler.s_getcharM);
				if (this.IsCi())
				{
					this.CallToLower();
				}
				this.Ldc((int)text2[j]);
				this.BneFar(this._backtrack);
			}
			this.Ldloc(this._textposV);
			this.Ldc(text2.Length);
			this.Sub();
			this.Stloc(this._textposV);
			return;
			IL_11F6:
			LocalBuilder tempV7 = this._tempV;
			LocalBuilder temp2V3 = this._temp2V;
			Label l16 = this.DefineLabel();
			this.Ldthis();
			this.Ldc(this.Operand(0));
			this.Callvirt(RegexCompiler.s_ismatchedM);
			if ((this._options & RegexOptions.ECMAScript) != RegexOptions.None)
			{
				this.Brfalse(this.AdvanceLabel());
			}
			else
			{
				this.BrfalseFar(this._backtrack);
			}
			this.Ldthis();
			this.Ldc(this.Operand(0));
			this.Callvirt(RegexCompiler.s_matchlengthM);
			this.Dup();
			this.Stloc(tempV7);
			if (!this.IsRtl())
			{
				this.Ldloc(this._textendV);
				this.Ldloc(this._textposV);
			}
			else
			{
				this.Ldloc(this._textposV);
				this.Ldloc(this._textbegV);
			}
			this.Sub();
			this.BgtFar(this._backtrack);
			this.Ldthis();
			this.Ldc(this.Operand(0));
			this.Callvirt(RegexCompiler.s_matchindexM);
			if (!this.IsRtl())
			{
				this.Ldloc(tempV7);
				this.Add(this.IsRtl());
			}
			this.Stloc(temp2V3);
			this.Ldloc(this._textposV);
			this.Ldloc(tempV7);
			this.Add(this.IsRtl());
			this.Stloc(this._textposV);
			this.MarkLabel(l16);
			this.Ldloc(tempV7);
			this.Ldc(0);
			this.Ble(this.AdvanceLabel());
			this.Ldloc(this._textV);
			this.Ldloc(temp2V3);
			this.Ldloc(tempV7);
			if (this.IsRtl())
			{
				this.Ldc(1);
				this.Sub();
				this.Dup();
				this.Stloc(tempV7);
			}
			this.Sub(this.IsRtl());
			this.Callvirt(RegexCompiler.s_getcharM);
			if (this.IsCi())
			{
				this.CallToLower();
			}
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Ldloc(tempV7);
			if (!this.IsRtl())
			{
				this.Dup();
				this.Ldc(1);
				this.Sub();
				this.Stloc(tempV7);
			}
			this.Sub(this.IsRtl());
			this.Callvirt(RegexCompiler.s_getcharM);
			if (this.IsCi())
			{
				this.CallToLower();
			}
			this.Beq(l16);
			this.Back();
			return;
			IL_1438:
			LocalBuilder tempV8 = this._tempV;
			Label l17 = this.DefineLabel();
			int num = this.Operand(1);
			if (num == 0)
			{
				return;
			}
			this.Ldc(num);
			if (!this.IsRtl())
			{
				this.Ldloc(this._textendV);
				this.Ldloc(this._textposV);
			}
			else
			{
				this.Ldloc(this._textposV);
				this.Ldloc(this._textbegV);
			}
			this.Sub();
			this.BgtFar(this._backtrack);
			this.Ldloc(this._textposV);
			this.Ldc(num);
			this.Add(this.IsRtl());
			this.Stloc(this._textposV);
			this.Ldc(num);
			this.Stloc(tempV8);
			this.MarkLabel(l17);
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Ldloc(tempV8);
			if (this.IsRtl())
			{
				this.Ldc(1);
				this.Sub();
				this.Dup();
				this.Stloc(tempV8);
				this.Add();
			}
			else
			{
				this.Dup();
				this.Ldc(1);
				this.Sub();
				this.Stloc(tempV8);
				this.Sub();
			}
			this.Callvirt(RegexCompiler.s_getcharM);
			if (this.IsCi())
			{
				this.CallToLower();
			}
			if (this.Code() == 2)
			{
				this.Ldstr(this._strings[this.Operand(0)]);
				this.Call(RegexCompiler.s_charInSetM);
				this.BrfalseFar(this._backtrack);
			}
			else
			{
				this.Ldc(this.Operand(0));
				if (this.Code() == 0)
				{
					this.BneFar(this._backtrack);
				}
				else
				{
					this.BeqFar(this._backtrack);
				}
			}
			this.Ldloc(tempV8);
			this.Ldc(0);
			if (this.Code() == 2)
			{
				this.BgtFar(l17);
				return;
			}
			this.Bgt(l17);
			return;
			IL_1604:
			LocalBuilder tempV9 = this._tempV;
			LocalBuilder temp2V4 = this._temp2V;
			Label l18 = this.DefineLabel();
			Label l19 = this.DefineLabel();
			int num2 = this.Operand(1);
			if (num2 != 0)
			{
				if (!this.IsRtl())
				{
					this.Ldloc(this._textendV);
					this.Ldloc(this._textposV);
				}
				else
				{
					this.Ldloc(this._textposV);
					this.Ldloc(this._textbegV);
				}
				this.Sub();
				if (num2 != 2147483647)
				{
					Label l20 = this.DefineLabel();
					this.Dup();
					this.Ldc(num2);
					this.Blt(l20);
					this.Pop();
					this.Ldc(num2);
					this.MarkLabel(l20);
				}
				this.Dup();
				this.Stloc(temp2V4);
				this.Ldc(1);
				this.Add();
				this.Stloc(tempV9);
				this.MarkLabel(l18);
				this.Ldloc(tempV9);
				this.Ldc(1);
				this.Sub();
				this.Dup();
				this.Stloc(tempV9);
				this.Ldc(0);
				if (this.Code() == 5)
				{
					this.BleFar(l19);
				}
				else
				{
					this.Ble(l19);
				}
				if (this.IsRtl())
				{
					this.Leftcharnext();
				}
				else
				{
					this.Rightcharnext();
				}
				if (this.IsCi())
				{
					this.CallToLower();
				}
				if (this.Code() == 5)
				{
					this.Ldstr(this._strings[this.Operand(0)]);
					this.Call(RegexCompiler.s_charInSetM);
					this.BrtrueFar(l18);
				}
				else
				{
					this.Ldc(this.Operand(0));
					if (this.Code() == 3)
					{
						this.Beq(l18);
					}
					else
					{
						this.Bne(l18);
					}
				}
				this.Ldloc(this._textposV);
				this.Ldc(1);
				this.Sub(this.IsRtl());
				this.Stloc(this._textposV);
				this.MarkLabel(l19);
				this.Ldloc(temp2V4);
				this.Ldloc(tempV9);
				this.Ble(this.AdvanceLabel());
				this.ReadyPushTrack();
				this.Ldloc(temp2V4);
				this.Ldloc(tempV9);
				this.Sub();
				this.Ldc(1);
				this.Sub();
				this.DoPush();
				this.ReadyPushTrack();
				this.Ldloc(this._textposV);
				this.Ldc(1);
				this.Sub(this.IsRtl());
				this.DoPush();
				this.Track();
				return;
			}
			return;
			IL_184F:
			this.PopTrack();
			this.Stloc(this._textposV);
			this.PopTrack();
			this.Stloc(this._tempV);
			this.Ldloc(this._tempV);
			this.Ldc(0);
			this.BleFar(this.AdvanceLabel());
			this.ReadyPushTrack();
			this.Ldloc(this._tempV);
			this.Ldc(1);
			this.Sub();
			this.DoPush();
			this.ReadyPushTrack();
			this.Ldloc(this._textposV);
			this.Ldc(1);
			this.Sub(this.IsRtl());
			this.DoPush();
			this.Trackagain();
			this.Advance();
			return;
			IL_18EF:
			LocalBuilder tempV10 = this._tempV;
			int num3 = this.Operand(1);
			if (num3 != 0)
			{
				if (!this.IsRtl())
				{
					this.Ldloc(this._textendV);
					this.Ldloc(this._textposV);
				}
				else
				{
					this.Ldloc(this._textposV);
					this.Ldloc(this._textbegV);
				}
				this.Sub();
				if (num3 != 2147483647)
				{
					Label l21 = this.DefineLabel();
					this.Dup();
					this.Ldc(num3);
					this.Blt(l21);
					this.Pop();
					this.Ldc(num3);
					this.MarkLabel(l21);
				}
				this.Dup();
				this.Stloc(tempV10);
				this.Ldc(0);
				this.Ble(this.AdvanceLabel());
				this.ReadyPushTrack();
				this.Ldloc(tempV10);
				this.Ldc(1);
				this.Sub();
				this.DoPush();
				this.PushTrack(this._textposV);
				this.Track();
				return;
			}
			return;
			IL_19D9:
			this.PopTrack();
			this.Stloc(this._textposV);
			this.PopTrack();
			this.Stloc(this._temp2V);
			if (!this.IsRtl())
			{
				this.Rightcharnext();
			}
			else
			{
				this.Leftcharnext();
			}
			if (this.IsCi())
			{
				this.CallToLower();
			}
			if (this.Code() == 8)
			{
				this.Ldstr(this._strings[this.Operand(0)]);
				this.Call(RegexCompiler.s_charInSetM);
				this.BrfalseFar(this._backtrack);
			}
			else
			{
				this.Ldc(this.Operand(0));
				if (this.Code() == 6)
				{
					this.BneFar(this._backtrack);
				}
				else
				{
					this.BeqFar(this._backtrack);
				}
			}
			this.Ldloc(this._temp2V);
			this.Ldc(0);
			this.BleFar(this.AdvanceLabel());
			this.ReadyPushTrack();
			this.Ldloc(this._temp2V);
			this.Ldc(1);
			this.Sub();
			this.DoPush();
			this.PushTrack(this._textposV);
			this.Trackagain();
			this.Advance();
			return;
			IL_1AE4:
			throw new NotImplementedException("Unimplemented state.");
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0000219B File Offset: 0x0000039B
		protected RegexCompiler()
		{
		}

		// Token: 0x040008A9 RID: 2217
		private static FieldInfo s_textbegF = RegexCompiler.RegexRunnerField("runtextbeg");

		// Token: 0x040008AA RID: 2218
		private static FieldInfo s_textendF = RegexCompiler.RegexRunnerField("runtextend");

		// Token: 0x040008AB RID: 2219
		private static FieldInfo s_textstartF = RegexCompiler.RegexRunnerField("runtextstart");

		// Token: 0x040008AC RID: 2220
		private static FieldInfo s_textposF = RegexCompiler.RegexRunnerField("runtextpos");

		// Token: 0x040008AD RID: 2221
		private static FieldInfo s_textF = RegexCompiler.RegexRunnerField("runtext");

		// Token: 0x040008AE RID: 2222
		private static FieldInfo s_trackposF = RegexCompiler.RegexRunnerField("runtrackpos");

		// Token: 0x040008AF RID: 2223
		private static FieldInfo s_trackF = RegexCompiler.RegexRunnerField("runtrack");

		// Token: 0x040008B0 RID: 2224
		private static FieldInfo s_stackposF = RegexCompiler.RegexRunnerField("runstackpos");

		// Token: 0x040008B1 RID: 2225
		private static FieldInfo s_stackF = RegexCompiler.RegexRunnerField("runstack");

		// Token: 0x040008B2 RID: 2226
		private static FieldInfo s_trackcountF = RegexCompiler.RegexRunnerField("runtrackcount");

		// Token: 0x040008B3 RID: 2227
		private static MethodInfo s_ensurestorageM = RegexCompiler.RegexRunnerMethod("EnsureStorage");

		// Token: 0x040008B4 RID: 2228
		private static MethodInfo s_captureM = RegexCompiler.RegexRunnerMethod("Capture");

		// Token: 0x040008B5 RID: 2229
		private static MethodInfo s_transferM = RegexCompiler.RegexRunnerMethod("TransferCapture");

		// Token: 0x040008B6 RID: 2230
		private static MethodInfo s_uncaptureM = RegexCompiler.RegexRunnerMethod("Uncapture");

		// Token: 0x040008B7 RID: 2231
		private static MethodInfo s_ismatchedM = RegexCompiler.RegexRunnerMethod("IsMatched");

		// Token: 0x040008B8 RID: 2232
		private static MethodInfo s_matchlengthM = RegexCompiler.RegexRunnerMethod("MatchLength");

		// Token: 0x040008B9 RID: 2233
		private static MethodInfo s_matchindexM = RegexCompiler.RegexRunnerMethod("MatchIndex");

		// Token: 0x040008BA RID: 2234
		private static MethodInfo s_isboundaryM = RegexCompiler.RegexRunnerMethod("IsBoundary");

		// Token: 0x040008BB RID: 2235
		private static MethodInfo s_isECMABoundaryM;

		// Token: 0x040008BC RID: 2236
		private static MethodInfo s_chartolowerM;

		// Token: 0x040008BD RID: 2237
		private static MethodInfo s_getcharM;

		// Token: 0x040008BE RID: 2238
		private static MethodInfo s_crawlposM;

		// Token: 0x040008BF RID: 2239
		private static MethodInfo s_charInSetM = RegexCompiler.RegexRunnerMethod("CharInClass");

		// Token: 0x040008C0 RID: 2240
		private static MethodInfo s_getCurrentCulture;

		// Token: 0x040008C1 RID: 2241
		private static MethodInfo s_getInvariantCulture;

		// Token: 0x040008C2 RID: 2242
		private static MethodInfo s_checkTimeoutM;

		// Token: 0x040008C3 RID: 2243
		protected ILGenerator _ilg;

		// Token: 0x040008C4 RID: 2244
		private LocalBuilder _textstartV;

		// Token: 0x040008C5 RID: 2245
		private LocalBuilder _textbegV;

		// Token: 0x040008C6 RID: 2246
		private LocalBuilder _textendV;

		// Token: 0x040008C7 RID: 2247
		private LocalBuilder _textposV;

		// Token: 0x040008C8 RID: 2248
		private LocalBuilder _textV;

		// Token: 0x040008C9 RID: 2249
		private LocalBuilder _trackposV;

		// Token: 0x040008CA RID: 2250
		private LocalBuilder _trackV;

		// Token: 0x040008CB RID: 2251
		private LocalBuilder _stackposV;

		// Token: 0x040008CC RID: 2252
		private LocalBuilder _stackV;

		// Token: 0x040008CD RID: 2253
		private LocalBuilder _tempV;

		// Token: 0x040008CE RID: 2254
		private LocalBuilder _temp2V;

		// Token: 0x040008CF RID: 2255
		private LocalBuilder _temp3V;

		// Token: 0x040008D0 RID: 2256
		protected RegexCode _code;

		// Token: 0x040008D1 RID: 2257
		protected int[] _codes;

		// Token: 0x040008D2 RID: 2258
		protected string[] _strings;

		// Token: 0x040008D3 RID: 2259
		protected RegexPrefix? _fcPrefix;

		// Token: 0x040008D4 RID: 2260
		protected RegexBoyerMoore _bmPrefix;

		// Token: 0x040008D5 RID: 2261
		protected int _anchors;

		// Token: 0x040008D6 RID: 2262
		private Label[] _labels;

		// Token: 0x040008D7 RID: 2263
		private RegexCompiler.BacktrackNote[] _notes;

		// Token: 0x040008D8 RID: 2264
		private int _notecount;

		// Token: 0x040008D9 RID: 2265
		protected int _trackcount;

		// Token: 0x040008DA RID: 2266
		private Label _backtrack;

		// Token: 0x040008DB RID: 2267
		private int _regexopcode;

		// Token: 0x040008DC RID: 2268
		private int _codepos;

		// Token: 0x040008DD RID: 2269
		private int _backpos;

		// Token: 0x040008DE RID: 2270
		protected RegexOptions _options;

		// Token: 0x040008DF RID: 2271
		private int[] _uniquenote;

		// Token: 0x040008E0 RID: 2272
		private int[] _goto;

		// Token: 0x040008E1 RID: 2273
		private const int Stackpop = 0;

		// Token: 0x040008E2 RID: 2274
		private const int Stackpop2 = 1;

		// Token: 0x040008E3 RID: 2275
		private const int Stackpop3 = 2;

		// Token: 0x040008E4 RID: 2276
		private const int Capback = 3;

		// Token: 0x040008E5 RID: 2277
		private const int Capback2 = 4;

		// Token: 0x040008E6 RID: 2278
		private const int Branchmarkback2 = 5;

		// Token: 0x040008E7 RID: 2279
		private const int Lazybranchmarkback2 = 6;

		// Token: 0x040008E8 RID: 2280
		private const int Branchcountback2 = 7;

		// Token: 0x040008E9 RID: 2281
		private const int Lazybranchcountback2 = 8;

		// Token: 0x040008EA RID: 2282
		private const int Forejumpback = 9;

		// Token: 0x040008EB RID: 2283
		private const int Uniquecount = 10;

		// Token: 0x02000201 RID: 513
		private sealed class BacktrackNote
		{
			// Token: 0x06000E57 RID: 3671 RVA: 0x0003DCE4 File Offset: 0x0003BEE4
			public BacktrackNote(int flags, Label label, int codepos)
			{
				this._codepos = codepos;
				this._flags = flags;
				this._label = label;
			}

			// Token: 0x040008EC RID: 2284
			internal int _codepos;

			// Token: 0x040008ED RID: 2285
			internal int _flags;

			// Token: 0x040008EE RID: 2286
			internal Label _label;
		}
	}
}
