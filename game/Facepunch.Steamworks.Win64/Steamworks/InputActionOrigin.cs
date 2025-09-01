using System;

namespace Steamworks
{
	// Token: 0x02000071 RID: 113
	internal enum InputActionOrigin
	{
		// Token: 0x040003A6 RID: 934
		None,
		// Token: 0x040003A7 RID: 935
		SteamController_A,
		// Token: 0x040003A8 RID: 936
		SteamController_B,
		// Token: 0x040003A9 RID: 937
		SteamController_X,
		// Token: 0x040003AA RID: 938
		SteamController_Y,
		// Token: 0x040003AB RID: 939
		SteamController_LeftBumper,
		// Token: 0x040003AC RID: 940
		SteamController_RightBumper,
		// Token: 0x040003AD RID: 941
		SteamController_LeftGrip,
		// Token: 0x040003AE RID: 942
		SteamController_RightGrip,
		// Token: 0x040003AF RID: 943
		SteamController_Start,
		// Token: 0x040003B0 RID: 944
		SteamController_Back,
		// Token: 0x040003B1 RID: 945
		SteamController_LeftPad_Touch,
		// Token: 0x040003B2 RID: 946
		SteamController_LeftPad_Swipe,
		// Token: 0x040003B3 RID: 947
		SteamController_LeftPad_Click,
		// Token: 0x040003B4 RID: 948
		SteamController_LeftPad_DPadNorth,
		// Token: 0x040003B5 RID: 949
		SteamController_LeftPad_DPadSouth,
		// Token: 0x040003B6 RID: 950
		SteamController_LeftPad_DPadWest,
		// Token: 0x040003B7 RID: 951
		SteamController_LeftPad_DPadEast,
		// Token: 0x040003B8 RID: 952
		SteamController_RightPad_Touch,
		// Token: 0x040003B9 RID: 953
		SteamController_RightPad_Swipe,
		// Token: 0x040003BA RID: 954
		SteamController_RightPad_Click,
		// Token: 0x040003BB RID: 955
		SteamController_RightPad_DPadNorth,
		// Token: 0x040003BC RID: 956
		SteamController_RightPad_DPadSouth,
		// Token: 0x040003BD RID: 957
		SteamController_RightPad_DPadWest,
		// Token: 0x040003BE RID: 958
		SteamController_RightPad_DPadEast,
		// Token: 0x040003BF RID: 959
		SteamController_LeftTrigger_Pull,
		// Token: 0x040003C0 RID: 960
		SteamController_LeftTrigger_Click,
		// Token: 0x040003C1 RID: 961
		SteamController_RightTrigger_Pull,
		// Token: 0x040003C2 RID: 962
		SteamController_RightTrigger_Click,
		// Token: 0x040003C3 RID: 963
		SteamController_LeftStick_Move,
		// Token: 0x040003C4 RID: 964
		SteamController_LeftStick_Click,
		// Token: 0x040003C5 RID: 965
		SteamController_LeftStick_DPadNorth,
		// Token: 0x040003C6 RID: 966
		SteamController_LeftStick_DPadSouth,
		// Token: 0x040003C7 RID: 967
		SteamController_LeftStick_DPadWest,
		// Token: 0x040003C8 RID: 968
		SteamController_LeftStick_DPadEast,
		// Token: 0x040003C9 RID: 969
		SteamController_Gyro_Move,
		// Token: 0x040003CA RID: 970
		SteamController_Gyro_Pitch,
		// Token: 0x040003CB RID: 971
		SteamController_Gyro_Yaw,
		// Token: 0x040003CC RID: 972
		SteamController_Gyro_Roll,
		// Token: 0x040003CD RID: 973
		SteamController_Reserved0,
		// Token: 0x040003CE RID: 974
		SteamController_Reserved1,
		// Token: 0x040003CF RID: 975
		SteamController_Reserved2,
		// Token: 0x040003D0 RID: 976
		SteamController_Reserved3,
		// Token: 0x040003D1 RID: 977
		SteamController_Reserved4,
		// Token: 0x040003D2 RID: 978
		SteamController_Reserved5,
		// Token: 0x040003D3 RID: 979
		SteamController_Reserved6,
		// Token: 0x040003D4 RID: 980
		SteamController_Reserved7,
		// Token: 0x040003D5 RID: 981
		SteamController_Reserved8,
		// Token: 0x040003D6 RID: 982
		SteamController_Reserved9,
		// Token: 0x040003D7 RID: 983
		SteamController_Reserved10,
		// Token: 0x040003D8 RID: 984
		PS4_X,
		// Token: 0x040003D9 RID: 985
		PS4_Circle,
		// Token: 0x040003DA RID: 986
		PS4_Triangle,
		// Token: 0x040003DB RID: 987
		PS4_Square,
		// Token: 0x040003DC RID: 988
		PS4_LeftBumper,
		// Token: 0x040003DD RID: 989
		PS4_RightBumper,
		// Token: 0x040003DE RID: 990
		PS4_Options,
		// Token: 0x040003DF RID: 991
		PS4_Share,
		// Token: 0x040003E0 RID: 992
		PS4_LeftPad_Touch,
		// Token: 0x040003E1 RID: 993
		PS4_LeftPad_Swipe,
		// Token: 0x040003E2 RID: 994
		PS4_LeftPad_Click,
		// Token: 0x040003E3 RID: 995
		PS4_LeftPad_DPadNorth,
		// Token: 0x040003E4 RID: 996
		PS4_LeftPad_DPadSouth,
		// Token: 0x040003E5 RID: 997
		PS4_LeftPad_DPadWest,
		// Token: 0x040003E6 RID: 998
		PS4_LeftPad_DPadEast,
		// Token: 0x040003E7 RID: 999
		PS4_RightPad_Touch,
		// Token: 0x040003E8 RID: 1000
		PS4_RightPad_Swipe,
		// Token: 0x040003E9 RID: 1001
		PS4_RightPad_Click,
		// Token: 0x040003EA RID: 1002
		PS4_RightPad_DPadNorth,
		// Token: 0x040003EB RID: 1003
		PS4_RightPad_DPadSouth,
		// Token: 0x040003EC RID: 1004
		PS4_RightPad_DPadWest,
		// Token: 0x040003ED RID: 1005
		PS4_RightPad_DPadEast,
		// Token: 0x040003EE RID: 1006
		PS4_CenterPad_Touch,
		// Token: 0x040003EF RID: 1007
		PS4_CenterPad_Swipe,
		// Token: 0x040003F0 RID: 1008
		PS4_CenterPad_Click,
		// Token: 0x040003F1 RID: 1009
		PS4_CenterPad_DPadNorth,
		// Token: 0x040003F2 RID: 1010
		PS4_CenterPad_DPadSouth,
		// Token: 0x040003F3 RID: 1011
		PS4_CenterPad_DPadWest,
		// Token: 0x040003F4 RID: 1012
		PS4_CenterPad_DPadEast,
		// Token: 0x040003F5 RID: 1013
		PS4_LeftTrigger_Pull,
		// Token: 0x040003F6 RID: 1014
		PS4_LeftTrigger_Click,
		// Token: 0x040003F7 RID: 1015
		PS4_RightTrigger_Pull,
		// Token: 0x040003F8 RID: 1016
		PS4_RightTrigger_Click,
		// Token: 0x040003F9 RID: 1017
		PS4_LeftStick_Move,
		// Token: 0x040003FA RID: 1018
		PS4_LeftStick_Click,
		// Token: 0x040003FB RID: 1019
		PS4_LeftStick_DPadNorth,
		// Token: 0x040003FC RID: 1020
		PS4_LeftStick_DPadSouth,
		// Token: 0x040003FD RID: 1021
		PS4_LeftStick_DPadWest,
		// Token: 0x040003FE RID: 1022
		PS4_LeftStick_DPadEast,
		// Token: 0x040003FF RID: 1023
		PS4_RightStick_Move,
		// Token: 0x04000400 RID: 1024
		PS4_RightStick_Click,
		// Token: 0x04000401 RID: 1025
		PS4_RightStick_DPadNorth,
		// Token: 0x04000402 RID: 1026
		PS4_RightStick_DPadSouth,
		// Token: 0x04000403 RID: 1027
		PS4_RightStick_DPadWest,
		// Token: 0x04000404 RID: 1028
		PS4_RightStick_DPadEast,
		// Token: 0x04000405 RID: 1029
		PS4_DPad_North,
		// Token: 0x04000406 RID: 1030
		PS4_DPad_South,
		// Token: 0x04000407 RID: 1031
		PS4_DPad_West,
		// Token: 0x04000408 RID: 1032
		PS4_DPad_East,
		// Token: 0x04000409 RID: 1033
		PS4_Gyro_Move,
		// Token: 0x0400040A RID: 1034
		PS4_Gyro_Pitch,
		// Token: 0x0400040B RID: 1035
		PS4_Gyro_Yaw,
		// Token: 0x0400040C RID: 1036
		PS4_Gyro_Roll,
		// Token: 0x0400040D RID: 1037
		PS4_DPad_Move,
		// Token: 0x0400040E RID: 1038
		PS4_Reserved1,
		// Token: 0x0400040F RID: 1039
		PS4_Reserved2,
		// Token: 0x04000410 RID: 1040
		PS4_Reserved3,
		// Token: 0x04000411 RID: 1041
		PS4_Reserved4,
		// Token: 0x04000412 RID: 1042
		PS4_Reserved5,
		// Token: 0x04000413 RID: 1043
		PS4_Reserved6,
		// Token: 0x04000414 RID: 1044
		PS4_Reserved7,
		// Token: 0x04000415 RID: 1045
		PS4_Reserved8,
		// Token: 0x04000416 RID: 1046
		PS4_Reserved9,
		// Token: 0x04000417 RID: 1047
		PS4_Reserved10,
		// Token: 0x04000418 RID: 1048
		XBoxOne_A,
		// Token: 0x04000419 RID: 1049
		XBoxOne_B,
		// Token: 0x0400041A RID: 1050
		XBoxOne_X,
		// Token: 0x0400041B RID: 1051
		XBoxOne_Y,
		// Token: 0x0400041C RID: 1052
		XBoxOne_LeftBumper,
		// Token: 0x0400041D RID: 1053
		XBoxOne_RightBumper,
		// Token: 0x0400041E RID: 1054
		XBoxOne_Menu,
		// Token: 0x0400041F RID: 1055
		XBoxOne_View,
		// Token: 0x04000420 RID: 1056
		XBoxOne_LeftTrigger_Pull,
		// Token: 0x04000421 RID: 1057
		XBoxOne_LeftTrigger_Click,
		// Token: 0x04000422 RID: 1058
		XBoxOne_RightTrigger_Pull,
		// Token: 0x04000423 RID: 1059
		XBoxOne_RightTrigger_Click,
		// Token: 0x04000424 RID: 1060
		XBoxOne_LeftStick_Move,
		// Token: 0x04000425 RID: 1061
		XBoxOne_LeftStick_Click,
		// Token: 0x04000426 RID: 1062
		XBoxOne_LeftStick_DPadNorth,
		// Token: 0x04000427 RID: 1063
		XBoxOne_LeftStick_DPadSouth,
		// Token: 0x04000428 RID: 1064
		XBoxOne_LeftStick_DPadWest,
		// Token: 0x04000429 RID: 1065
		XBoxOne_LeftStick_DPadEast,
		// Token: 0x0400042A RID: 1066
		XBoxOne_RightStick_Move,
		// Token: 0x0400042B RID: 1067
		XBoxOne_RightStick_Click,
		// Token: 0x0400042C RID: 1068
		XBoxOne_RightStick_DPadNorth,
		// Token: 0x0400042D RID: 1069
		XBoxOne_RightStick_DPadSouth,
		// Token: 0x0400042E RID: 1070
		XBoxOne_RightStick_DPadWest,
		// Token: 0x0400042F RID: 1071
		XBoxOne_RightStick_DPadEast,
		// Token: 0x04000430 RID: 1072
		XBoxOne_DPad_North,
		// Token: 0x04000431 RID: 1073
		XBoxOne_DPad_South,
		// Token: 0x04000432 RID: 1074
		XBoxOne_DPad_West,
		// Token: 0x04000433 RID: 1075
		XBoxOne_DPad_East,
		// Token: 0x04000434 RID: 1076
		XBoxOne_DPad_Move,
		// Token: 0x04000435 RID: 1077
		XBoxOne_Reserved1,
		// Token: 0x04000436 RID: 1078
		XBoxOne_Reserved2,
		// Token: 0x04000437 RID: 1079
		XBoxOne_Reserved3,
		// Token: 0x04000438 RID: 1080
		XBoxOne_Reserved4,
		// Token: 0x04000439 RID: 1081
		XBoxOne_Reserved5,
		// Token: 0x0400043A RID: 1082
		XBoxOne_Reserved6,
		// Token: 0x0400043B RID: 1083
		XBoxOne_Reserved7,
		// Token: 0x0400043C RID: 1084
		XBoxOne_Reserved8,
		// Token: 0x0400043D RID: 1085
		XBoxOne_Reserved9,
		// Token: 0x0400043E RID: 1086
		XBoxOne_Reserved10,
		// Token: 0x0400043F RID: 1087
		XBox360_A,
		// Token: 0x04000440 RID: 1088
		XBox360_B,
		// Token: 0x04000441 RID: 1089
		XBox360_X,
		// Token: 0x04000442 RID: 1090
		XBox360_Y,
		// Token: 0x04000443 RID: 1091
		XBox360_LeftBumper,
		// Token: 0x04000444 RID: 1092
		XBox360_RightBumper,
		// Token: 0x04000445 RID: 1093
		XBox360_Start,
		// Token: 0x04000446 RID: 1094
		XBox360_Back,
		// Token: 0x04000447 RID: 1095
		XBox360_LeftTrigger_Pull,
		// Token: 0x04000448 RID: 1096
		XBox360_LeftTrigger_Click,
		// Token: 0x04000449 RID: 1097
		XBox360_RightTrigger_Pull,
		// Token: 0x0400044A RID: 1098
		XBox360_RightTrigger_Click,
		// Token: 0x0400044B RID: 1099
		XBox360_LeftStick_Move,
		// Token: 0x0400044C RID: 1100
		XBox360_LeftStick_Click,
		// Token: 0x0400044D RID: 1101
		XBox360_LeftStick_DPadNorth,
		// Token: 0x0400044E RID: 1102
		XBox360_LeftStick_DPadSouth,
		// Token: 0x0400044F RID: 1103
		XBox360_LeftStick_DPadWest,
		// Token: 0x04000450 RID: 1104
		XBox360_LeftStick_DPadEast,
		// Token: 0x04000451 RID: 1105
		XBox360_RightStick_Move,
		// Token: 0x04000452 RID: 1106
		XBox360_RightStick_Click,
		// Token: 0x04000453 RID: 1107
		XBox360_RightStick_DPadNorth,
		// Token: 0x04000454 RID: 1108
		XBox360_RightStick_DPadSouth,
		// Token: 0x04000455 RID: 1109
		XBox360_RightStick_DPadWest,
		// Token: 0x04000456 RID: 1110
		XBox360_RightStick_DPadEast,
		// Token: 0x04000457 RID: 1111
		XBox360_DPad_North,
		// Token: 0x04000458 RID: 1112
		XBox360_DPad_South,
		// Token: 0x04000459 RID: 1113
		XBox360_DPad_West,
		// Token: 0x0400045A RID: 1114
		XBox360_DPad_East,
		// Token: 0x0400045B RID: 1115
		XBox360_DPad_Move,
		// Token: 0x0400045C RID: 1116
		XBox360_Reserved1,
		// Token: 0x0400045D RID: 1117
		XBox360_Reserved2,
		// Token: 0x0400045E RID: 1118
		XBox360_Reserved3,
		// Token: 0x0400045F RID: 1119
		XBox360_Reserved4,
		// Token: 0x04000460 RID: 1120
		XBox360_Reserved5,
		// Token: 0x04000461 RID: 1121
		XBox360_Reserved6,
		// Token: 0x04000462 RID: 1122
		XBox360_Reserved7,
		// Token: 0x04000463 RID: 1123
		XBox360_Reserved8,
		// Token: 0x04000464 RID: 1124
		XBox360_Reserved9,
		// Token: 0x04000465 RID: 1125
		XBox360_Reserved10,
		// Token: 0x04000466 RID: 1126
		Switch_A,
		// Token: 0x04000467 RID: 1127
		Switch_B,
		// Token: 0x04000468 RID: 1128
		Switch_X,
		// Token: 0x04000469 RID: 1129
		Switch_Y,
		// Token: 0x0400046A RID: 1130
		Switch_LeftBumper,
		// Token: 0x0400046B RID: 1131
		Switch_RightBumper,
		// Token: 0x0400046C RID: 1132
		Switch_Plus,
		// Token: 0x0400046D RID: 1133
		Switch_Minus,
		// Token: 0x0400046E RID: 1134
		Switch_Capture,
		// Token: 0x0400046F RID: 1135
		Switch_LeftTrigger_Pull,
		// Token: 0x04000470 RID: 1136
		Switch_LeftTrigger_Click,
		// Token: 0x04000471 RID: 1137
		Switch_RightTrigger_Pull,
		// Token: 0x04000472 RID: 1138
		Switch_RightTrigger_Click,
		// Token: 0x04000473 RID: 1139
		Switch_LeftStick_Move,
		// Token: 0x04000474 RID: 1140
		Switch_LeftStick_Click,
		// Token: 0x04000475 RID: 1141
		Switch_LeftStick_DPadNorth,
		// Token: 0x04000476 RID: 1142
		Switch_LeftStick_DPadSouth,
		// Token: 0x04000477 RID: 1143
		Switch_LeftStick_DPadWest,
		// Token: 0x04000478 RID: 1144
		Switch_LeftStick_DPadEast,
		// Token: 0x04000479 RID: 1145
		Switch_RightStick_Move,
		// Token: 0x0400047A RID: 1146
		Switch_RightStick_Click,
		// Token: 0x0400047B RID: 1147
		Switch_RightStick_DPadNorth,
		// Token: 0x0400047C RID: 1148
		Switch_RightStick_DPadSouth,
		// Token: 0x0400047D RID: 1149
		Switch_RightStick_DPadWest,
		// Token: 0x0400047E RID: 1150
		Switch_RightStick_DPadEast,
		// Token: 0x0400047F RID: 1151
		Switch_DPad_North,
		// Token: 0x04000480 RID: 1152
		Switch_DPad_South,
		// Token: 0x04000481 RID: 1153
		Switch_DPad_West,
		// Token: 0x04000482 RID: 1154
		Switch_DPad_East,
		// Token: 0x04000483 RID: 1155
		Switch_ProGyro_Move,
		// Token: 0x04000484 RID: 1156
		Switch_ProGyro_Pitch,
		// Token: 0x04000485 RID: 1157
		Switch_ProGyro_Yaw,
		// Token: 0x04000486 RID: 1158
		Switch_ProGyro_Roll,
		// Token: 0x04000487 RID: 1159
		Switch_DPad_Move,
		// Token: 0x04000488 RID: 1160
		Switch_Reserved1,
		// Token: 0x04000489 RID: 1161
		Switch_Reserved2,
		// Token: 0x0400048A RID: 1162
		Switch_Reserved3,
		// Token: 0x0400048B RID: 1163
		Switch_Reserved4,
		// Token: 0x0400048C RID: 1164
		Switch_Reserved5,
		// Token: 0x0400048D RID: 1165
		Switch_Reserved6,
		// Token: 0x0400048E RID: 1166
		Switch_Reserved7,
		// Token: 0x0400048F RID: 1167
		Switch_Reserved8,
		// Token: 0x04000490 RID: 1168
		Switch_Reserved9,
		// Token: 0x04000491 RID: 1169
		Switch_Reserved10,
		// Token: 0x04000492 RID: 1170
		Switch_RightGyro_Move,
		// Token: 0x04000493 RID: 1171
		Switch_RightGyro_Pitch,
		// Token: 0x04000494 RID: 1172
		Switch_RightGyro_Yaw,
		// Token: 0x04000495 RID: 1173
		Switch_RightGyro_Roll,
		// Token: 0x04000496 RID: 1174
		Switch_LeftGyro_Move,
		// Token: 0x04000497 RID: 1175
		Switch_LeftGyro_Pitch,
		// Token: 0x04000498 RID: 1176
		Switch_LeftGyro_Yaw,
		// Token: 0x04000499 RID: 1177
		Switch_LeftGyro_Roll,
		// Token: 0x0400049A RID: 1178
		Switch_LeftGrip_Lower,
		// Token: 0x0400049B RID: 1179
		Switch_LeftGrip_Upper,
		// Token: 0x0400049C RID: 1180
		Switch_RightGrip_Lower,
		// Token: 0x0400049D RID: 1181
		Switch_RightGrip_Upper,
		// Token: 0x0400049E RID: 1182
		Switch_Reserved11,
		// Token: 0x0400049F RID: 1183
		Switch_Reserved12,
		// Token: 0x040004A0 RID: 1184
		Switch_Reserved13,
		// Token: 0x040004A1 RID: 1185
		Switch_Reserved14,
		// Token: 0x040004A2 RID: 1186
		Switch_Reserved15,
		// Token: 0x040004A3 RID: 1187
		Switch_Reserved16,
		// Token: 0x040004A4 RID: 1188
		Switch_Reserved17,
		// Token: 0x040004A5 RID: 1189
		Switch_Reserved18,
		// Token: 0x040004A6 RID: 1190
		Switch_Reserved19,
		// Token: 0x040004A7 RID: 1191
		Switch_Reserved20,
		// Token: 0x040004A8 RID: 1192
		Count,
		// Token: 0x040004A9 RID: 1193
		MaximumPossibleValue = 32767
	}
}
