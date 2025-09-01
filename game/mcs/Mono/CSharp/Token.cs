using System;

namespace Mono.CSharp
{
	// Token: 0x02000187 RID: 391
	public class Token
	{
		// Token: 0x060014E0 RID: 5344 RVA: 0x00002CCC File Offset: 0x00000ECC
		public Token()
		{
		}

		// Token: 0x040007FD RID: 2045
		public const int EOF = 257;

		// Token: 0x040007FE RID: 2046
		public const int NONE = 258;

		// Token: 0x040007FF RID: 2047
		public const int ERROR = 259;

		// Token: 0x04000800 RID: 2048
		public const int FIRST_KEYWORD = 260;

		// Token: 0x04000801 RID: 2049
		public const int ABSTRACT = 261;

		// Token: 0x04000802 RID: 2050
		public const int AS = 262;

		// Token: 0x04000803 RID: 2051
		public const int ADD = 263;

		// Token: 0x04000804 RID: 2052
		public const int BASE = 264;

		// Token: 0x04000805 RID: 2053
		public const int BOOL = 265;

		// Token: 0x04000806 RID: 2054
		public const int BREAK = 266;

		// Token: 0x04000807 RID: 2055
		public const int BYTE = 267;

		// Token: 0x04000808 RID: 2056
		public const int CASE = 268;

		// Token: 0x04000809 RID: 2057
		public const int CATCH = 269;

		// Token: 0x0400080A RID: 2058
		public const int CHAR = 270;

		// Token: 0x0400080B RID: 2059
		public const int CHECKED = 271;

		// Token: 0x0400080C RID: 2060
		public const int CLASS = 272;

		// Token: 0x0400080D RID: 2061
		public const int CONST = 273;

		// Token: 0x0400080E RID: 2062
		public const int CONTINUE = 274;

		// Token: 0x0400080F RID: 2063
		public const int DECIMAL = 275;

		// Token: 0x04000810 RID: 2064
		public const int DEFAULT = 276;

		// Token: 0x04000811 RID: 2065
		public const int DELEGATE = 277;

		// Token: 0x04000812 RID: 2066
		public const int DO = 278;

		// Token: 0x04000813 RID: 2067
		public const int DOUBLE = 279;

		// Token: 0x04000814 RID: 2068
		public const int ELSE = 280;

		// Token: 0x04000815 RID: 2069
		public const int ENUM = 281;

		// Token: 0x04000816 RID: 2070
		public const int EVENT = 282;

		// Token: 0x04000817 RID: 2071
		public const int EXPLICIT = 283;

		// Token: 0x04000818 RID: 2072
		public const int EXTERN = 284;

		// Token: 0x04000819 RID: 2073
		public const int FALSE = 285;

		// Token: 0x0400081A RID: 2074
		public const int FINALLY = 286;

		// Token: 0x0400081B RID: 2075
		public const int FIXED = 287;

		// Token: 0x0400081C RID: 2076
		public const int FLOAT = 288;

		// Token: 0x0400081D RID: 2077
		public const int FOR = 289;

		// Token: 0x0400081E RID: 2078
		public const int FOREACH = 290;

		// Token: 0x0400081F RID: 2079
		public const int GOTO = 291;

		// Token: 0x04000820 RID: 2080
		public const int IF = 292;

		// Token: 0x04000821 RID: 2081
		public const int IMPLICIT = 293;

		// Token: 0x04000822 RID: 2082
		public const int IN = 294;

		// Token: 0x04000823 RID: 2083
		public const int INT = 295;

		// Token: 0x04000824 RID: 2084
		public const int INTERFACE = 296;

		// Token: 0x04000825 RID: 2085
		public const int INTERNAL = 297;

		// Token: 0x04000826 RID: 2086
		public const int IS = 298;

		// Token: 0x04000827 RID: 2087
		public const int LOCK = 299;

		// Token: 0x04000828 RID: 2088
		public const int LONG = 300;

		// Token: 0x04000829 RID: 2089
		public const int NAMESPACE = 301;

		// Token: 0x0400082A RID: 2090
		public const int NEW = 302;

		// Token: 0x0400082B RID: 2091
		public const int NULL = 303;

		// Token: 0x0400082C RID: 2092
		public const int OBJECT = 304;

		// Token: 0x0400082D RID: 2093
		public const int OPERATOR = 305;

		// Token: 0x0400082E RID: 2094
		public const int OUT = 306;

		// Token: 0x0400082F RID: 2095
		public const int OVERRIDE = 307;

		// Token: 0x04000830 RID: 2096
		public const int PARAMS = 308;

		// Token: 0x04000831 RID: 2097
		public const int PRIVATE = 309;

		// Token: 0x04000832 RID: 2098
		public const int PROTECTED = 310;

		// Token: 0x04000833 RID: 2099
		public const int PUBLIC = 311;

		// Token: 0x04000834 RID: 2100
		public const int READONLY = 312;

		// Token: 0x04000835 RID: 2101
		public const int REF = 313;

		// Token: 0x04000836 RID: 2102
		public const int RETURN = 314;

		// Token: 0x04000837 RID: 2103
		public const int REMOVE = 315;

		// Token: 0x04000838 RID: 2104
		public const int SBYTE = 316;

		// Token: 0x04000839 RID: 2105
		public const int SEALED = 317;

		// Token: 0x0400083A RID: 2106
		public const int SHORT = 318;

		// Token: 0x0400083B RID: 2107
		public const int SIZEOF = 319;

		// Token: 0x0400083C RID: 2108
		public const int STACKALLOC = 320;

		// Token: 0x0400083D RID: 2109
		public const int STATIC = 321;

		// Token: 0x0400083E RID: 2110
		public const int STRING = 322;

		// Token: 0x0400083F RID: 2111
		public const int STRUCT = 323;

		// Token: 0x04000840 RID: 2112
		public const int SWITCH = 324;

		// Token: 0x04000841 RID: 2113
		public const int THIS = 325;

		// Token: 0x04000842 RID: 2114
		public const int THROW = 326;

		// Token: 0x04000843 RID: 2115
		public const int TRUE = 327;

		// Token: 0x04000844 RID: 2116
		public const int TRY = 328;

		// Token: 0x04000845 RID: 2117
		public const int TYPEOF = 329;

		// Token: 0x04000846 RID: 2118
		public const int UINT = 330;

		// Token: 0x04000847 RID: 2119
		public const int ULONG = 331;

		// Token: 0x04000848 RID: 2120
		public const int UNCHECKED = 332;

		// Token: 0x04000849 RID: 2121
		public const int UNSAFE = 333;

		// Token: 0x0400084A RID: 2122
		public const int USHORT = 334;

		// Token: 0x0400084B RID: 2123
		public const int USING = 335;

		// Token: 0x0400084C RID: 2124
		public const int VIRTUAL = 336;

		// Token: 0x0400084D RID: 2125
		public const int VOID = 337;

		// Token: 0x0400084E RID: 2126
		public const int VOLATILE = 338;

		// Token: 0x0400084F RID: 2127
		public const int WHERE = 339;

		// Token: 0x04000850 RID: 2128
		public const int WHILE = 340;

		// Token: 0x04000851 RID: 2129
		public const int ARGLIST = 341;

		// Token: 0x04000852 RID: 2130
		public const int PARTIAL = 342;

		// Token: 0x04000853 RID: 2131
		public const int ARROW = 343;

		// Token: 0x04000854 RID: 2132
		public const int FROM = 344;

		// Token: 0x04000855 RID: 2133
		public const int FROM_FIRST = 345;

		// Token: 0x04000856 RID: 2134
		public const int JOIN = 346;

		// Token: 0x04000857 RID: 2135
		public const int ON = 347;

		// Token: 0x04000858 RID: 2136
		public const int EQUALS = 348;

		// Token: 0x04000859 RID: 2137
		public const int SELECT = 349;

		// Token: 0x0400085A RID: 2138
		public const int GROUP = 350;

		// Token: 0x0400085B RID: 2139
		public const int BY = 351;

		// Token: 0x0400085C RID: 2140
		public const int LET = 352;

		// Token: 0x0400085D RID: 2141
		public const int ORDERBY = 353;

		// Token: 0x0400085E RID: 2142
		public const int ASCENDING = 354;

		// Token: 0x0400085F RID: 2143
		public const int DESCENDING = 355;

		// Token: 0x04000860 RID: 2144
		public const int INTO = 356;

		// Token: 0x04000861 RID: 2145
		public const int INTERR_NULLABLE = 357;

		// Token: 0x04000862 RID: 2146
		public const int EXTERN_ALIAS = 358;

		// Token: 0x04000863 RID: 2147
		public const int REFVALUE = 359;

		// Token: 0x04000864 RID: 2148
		public const int REFTYPE = 360;

		// Token: 0x04000865 RID: 2149
		public const int MAKEREF = 361;

		// Token: 0x04000866 RID: 2150
		public const int ASYNC = 362;

		// Token: 0x04000867 RID: 2151
		public const int AWAIT = 363;

		// Token: 0x04000868 RID: 2152
		public const int INTERR_OPERATOR = 364;

		// Token: 0x04000869 RID: 2153
		public const int WHEN = 365;

		// Token: 0x0400086A RID: 2154
		public const int INTERPOLATED_STRING = 366;

		// Token: 0x0400086B RID: 2155
		public const int INTERPOLATED_STRING_END = 367;

		// Token: 0x0400086C RID: 2156
		public const int GET = 368;

		// Token: 0x0400086D RID: 2157
		public const int SET = 369;

		// Token: 0x0400086E RID: 2158
		public const int LAST_KEYWORD = 370;

		// Token: 0x0400086F RID: 2159
		public const int OPEN_BRACE = 371;

		// Token: 0x04000870 RID: 2160
		public const int CLOSE_BRACE = 372;

		// Token: 0x04000871 RID: 2161
		public const int OPEN_BRACKET = 373;

		// Token: 0x04000872 RID: 2162
		public const int CLOSE_BRACKET = 374;

		// Token: 0x04000873 RID: 2163
		public const int OPEN_PARENS = 375;

		// Token: 0x04000874 RID: 2164
		public const int CLOSE_PARENS = 376;

		// Token: 0x04000875 RID: 2165
		public const int DOT = 377;

		// Token: 0x04000876 RID: 2166
		public const int COMMA = 378;

		// Token: 0x04000877 RID: 2167
		public const int COLON = 379;

		// Token: 0x04000878 RID: 2168
		public const int SEMICOLON = 380;

		// Token: 0x04000879 RID: 2169
		public const int TILDE = 381;

		// Token: 0x0400087A RID: 2170
		public const int PLUS = 382;

		// Token: 0x0400087B RID: 2171
		public const int MINUS = 383;

		// Token: 0x0400087C RID: 2172
		public const int BANG = 384;

		// Token: 0x0400087D RID: 2173
		public const int ASSIGN = 385;

		// Token: 0x0400087E RID: 2174
		public const int OP_LT = 386;

		// Token: 0x0400087F RID: 2175
		public const int OP_GT = 387;

		// Token: 0x04000880 RID: 2176
		public const int BITWISE_AND = 388;

		// Token: 0x04000881 RID: 2177
		public const int BITWISE_OR = 389;

		// Token: 0x04000882 RID: 2178
		public const int STAR = 390;

		// Token: 0x04000883 RID: 2179
		public const int PERCENT = 391;

		// Token: 0x04000884 RID: 2180
		public const int DIV = 392;

		// Token: 0x04000885 RID: 2181
		public const int CARRET = 393;

		// Token: 0x04000886 RID: 2182
		public const int INTERR = 394;

		// Token: 0x04000887 RID: 2183
		public const int DOUBLE_COLON = 395;

		// Token: 0x04000888 RID: 2184
		public const int OP_INC = 396;

		// Token: 0x04000889 RID: 2185
		public const int OP_DEC = 397;

		// Token: 0x0400088A RID: 2186
		public const int OP_SHIFT_LEFT = 398;

		// Token: 0x0400088B RID: 2187
		public const int OP_SHIFT_RIGHT = 399;

		// Token: 0x0400088C RID: 2188
		public const int OP_LE = 400;

		// Token: 0x0400088D RID: 2189
		public const int OP_GE = 401;

		// Token: 0x0400088E RID: 2190
		public const int OP_EQ = 402;

		// Token: 0x0400088F RID: 2191
		public const int OP_NE = 403;

		// Token: 0x04000890 RID: 2192
		public const int OP_AND = 404;

		// Token: 0x04000891 RID: 2193
		public const int OP_OR = 405;

		// Token: 0x04000892 RID: 2194
		public const int OP_MULT_ASSIGN = 406;

		// Token: 0x04000893 RID: 2195
		public const int OP_DIV_ASSIGN = 407;

		// Token: 0x04000894 RID: 2196
		public const int OP_MOD_ASSIGN = 408;

		// Token: 0x04000895 RID: 2197
		public const int OP_ADD_ASSIGN = 409;

		// Token: 0x04000896 RID: 2198
		public const int OP_SUB_ASSIGN = 410;

		// Token: 0x04000897 RID: 2199
		public const int OP_SHIFT_LEFT_ASSIGN = 411;

		// Token: 0x04000898 RID: 2200
		public const int OP_SHIFT_RIGHT_ASSIGN = 412;

		// Token: 0x04000899 RID: 2201
		public const int OP_AND_ASSIGN = 413;

		// Token: 0x0400089A RID: 2202
		public const int OP_XOR_ASSIGN = 414;

		// Token: 0x0400089B RID: 2203
		public const int OP_OR_ASSIGN = 415;

		// Token: 0x0400089C RID: 2204
		public const int OP_PTR = 416;

		// Token: 0x0400089D RID: 2205
		public const int OP_COALESCING = 417;

		// Token: 0x0400089E RID: 2206
		public const int OP_GENERICS_LT = 418;

		// Token: 0x0400089F RID: 2207
		public const int OP_GENERICS_LT_DECL = 419;

		// Token: 0x040008A0 RID: 2208
		public const int OP_GENERICS_GT = 420;

		// Token: 0x040008A1 RID: 2209
		public const int LITERAL = 421;

		// Token: 0x040008A2 RID: 2210
		public const int IDENTIFIER = 422;

		// Token: 0x040008A3 RID: 2211
		public const int OPEN_PARENS_LAMBDA = 423;

		// Token: 0x040008A4 RID: 2212
		public const int OPEN_PARENS_CAST = 424;

		// Token: 0x040008A5 RID: 2213
		public const int GENERIC_DIMENSION = 425;

		// Token: 0x040008A6 RID: 2214
		public const int DEFAULT_COLON = 426;

		// Token: 0x040008A7 RID: 2215
		public const int OPEN_BRACKET_EXPR = 427;

		// Token: 0x040008A8 RID: 2216
		public const int EVAL_STATEMENT_PARSER = 428;

		// Token: 0x040008A9 RID: 2217
		public const int EVAL_COMPILATION_UNIT_PARSER = 429;

		// Token: 0x040008AA RID: 2218
		public const int EVAL_USING_DECLARATIONS_UNIT_PARSER = 430;

		// Token: 0x040008AB RID: 2219
		public const int DOC_SEE = 431;

		// Token: 0x040008AC RID: 2220
		public const int GENERATE_COMPLETION = 432;

		// Token: 0x040008AD RID: 2221
		public const int COMPLETE_COMPLETION = 433;

		// Token: 0x040008AE RID: 2222
		public const int UMINUS = 434;

		// Token: 0x040008AF RID: 2223
		public const int yyErrorCode = 256;
	}
}
