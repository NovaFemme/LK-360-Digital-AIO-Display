using System;
using System.Runtime.InteropServices;

namespace HardwareHelp
{
    internal class CPUIDSDK
    {
      public const string szDllFilename = "cpuidsdk64.dll";
      public const string szDllPath = "";
      protected const string szDllName = "cpuidsdk64.dll";
      public const uint CPUIDSDK_ERROR_NO_ERROR = 0;
      public const uint CPUIDSDK_ERROR_EVALUATION = 1;
      public const uint CPUIDSDK_ERROR_DRIVER = 2;
      public const uint CPUIDSDK_ERROR_VM_RUNNING = 4;
      public const uint CPUIDSDK_ERROR_LOCKED = 8;
      public const uint CPUIDSDK_ERROR_INVALID_DLL = 16 /*0x10*/;
      public const uint CPUIDSDK_EXT_ERROR_EVAL_1 = 1;
      public const uint CPUIDSDK_EXT_ERROR_EVAL_2 = 2;
      public const uint CPUIDSDK_CONFIG_USE_SOFTWARE = 2;
      public const uint CPUIDSDK_CONFIG_USE_DMI = 4;
      public const uint CPUIDSDK_CONFIG_USE_PCI = 8;
      public const uint CPUIDSDK_CONFIG_USE_ACPI = 16 /*0x10*/;
      public const uint CPUIDSDK_CONFIG_USE_CHIPSET = 32 /*0x20*/;
      public const uint CPUIDSDK_CONFIG_USE_SMBUS = 64 /*0x40*/;
      public const uint CPUIDSDK_CONFIG_USE_SPD = 128 /*0x80*/;
      public const uint CPUIDSDK_CONFIG_USE_STORAGE = 256 /*0x0100*/;
      public const uint CPUIDSDK_CONFIG_USE_GRAPHICS = 512 /*0x0200*/;
      public const uint CPUIDSDK_CONFIG_USE_HWMONITORING = 1024 /*0x0400*/;
      public const uint CPUIDSDK_CONFIG_USE_PROCESSOR = 2048 /*0x0800*/;
      public const uint CPUIDSDK_CONFIG_USE_DISPLAY_API = 4096 /*0x1000*/;
      public const uint CPUIDSDK_CONFIG_USE_PDH = 8192 /*0x2000*/;
      public const uint CPUIDSDK_CONFIG_USE_ACPI_TIMER = 16384 /*0x4000*/;
      public const uint CPUIDSDK_CONFIG_UPDATE_PROCESSOR = 65536 /*0x010000*/;
      public const uint CPUIDSDK_CONFIG_UPDATE_GRAPHICS = 131072 /*0x020000*/;
      public const uint CPUIDSDK_CONFIG_UPDATE_STORAGE = 262144 /*0x040000*/;
      public const uint CPUIDSDK_CONFIG_UPDATE_LPCIO = 524288 /*0x080000*/;
      public const uint CPUIDSDK_CONFIG_UPDATE_DRAM = 1048576 /*0x100000*/;
      public const uint CPUIDSDK_CONFIG_CHECK_VM = 16777216 /*0x01000000*/;
      public const uint CPUIDSDK_CONFIG_WAKEUP_HDD = 33554432 /*0x02000000*/;
      public const uint CPUIDSDK_CONFIG_SCAN_USB_HDD = 67108864 /*0x04000000*/;
      public const uint CPUIDSDK_CONFIG_USE_USBXPRESS = 134217728 /*0x08000000*/;
      public const uint CPUIDSDK_CONFIG_USE_LPCIO = 268435456 /*0x10000000*/;
      public const uint CPUIDSDK_CONFIG_SERVER_SAFE = 2147483648 /*0x80000000*/;
      public const uint CPUIDSDK_CONFIG_USE_EVERYTHING = 2147483647 /*0x7FFFFFFF*/;
      public static int I_UNDEFINED_VALUE = -1;
      public static float F_UNDEFINED_VALUE = -1f;
      public static uint MAX_INTEGER = uint.MaxValue;
      public static float MAX_FLOAT = (float) CPUIDSDK.MAX_INTEGER;
      public const uint CLASS_DEVICE_UNKNOWN = 0;
      public const uint CLASS_DEVICE_PCI = 1;
      public const uint CLASS_DEVICE_SMBUS = 2;
      public const uint CLASS_DEVICE_PROCESSOR = 4;
      public const uint CLASS_DEVICE_LPCIO = 8;
      public const uint CLASS_DEVICE_DRIVE = 16 /*0x10*/;
      public const uint CLASS_DEVICE_DISPLAY_ADAPTER = 32 /*0x20*/;
      public const uint CLASS_DEVICE_HID = 64 /*0x40*/;
      public const uint CLASS_DEVICE_BATTERY = 128 /*0x80*/;
      public const uint CLASS_DEVICE_EVBOT = 256 /*0x0100*/;
      public const uint CLASS_DEVICE_NETWORK = 512 /*0x0200*/;
      public const uint CLASS_DEVICE_MAINBOARD = 1024 /*0x0400*/;
      public const uint CLASS_DEVICE_MEMORY_MODULE = 2048 /*0x0800*/;
      public const uint CLASS_DEVICE_PSU = 4096 /*0x1000*/;
      public const uint CLASS_DEVICE_NPU = 32768 /*0x8000*/;
      public const uint CLASS_DEVICE_TYPE_MASK = 2147483647 /*0x7FFFFFFF*/;
      public const uint CLASS_DEVICE_COMPOSITE = 2147483648 /*0x80000000*/;
      public const uint CPU_MANUFACTURER_MASK = 4278190080 /*0xFF000000*/;
      public const uint CPU_FAMILY_MASK = 4294967040;
      public const uint CPU_MODEL_MASK = 4294967295 /*0xFFFFFFFF*/;
      public const uint CPU_UNKNOWN = 0;
      public const uint CPU_INTEL = 16777216 /*0x01000000*/;
      public const uint CPU_AMD = 33554432 /*0x02000000*/;
      public const uint CPU_CYRIX = 67108864 /*0x04000000*/;
      public const uint CPU_VIA = 134217728 /*0x08000000*/;
      public const uint CPU_TRANSMETA = 268435456 /*0x10000000*/;
      public const uint CPU_DMP = 536870912 /*0x20000000*/;
      public const uint CPU_UMC = 1073741824 /*0x40000000*/;
      public const uint CPU_IBM = 2181038080 /*0x82000000*/;
      public const uint CPU_QUALCOMM = 2197815296 /*0x83000000*/;
      public const uint CPU_HYGON = 2214592512 /*0x84000000*/;
      public const uint CPU_INTEL_386 = 16777472 /*0x01000100*/;
      public const uint CPU_INTEL_486 = 16777728 /*0x01000200*/;
      public const uint CPU_INTEL_P5 = 16778240 /*0x01000400*/;
      public const uint CPU_INTEL_P6 = 16779264 /*0x01000800*/;
      public const uint CPU_INTEL_NETBURST = 16781312 /*0x01001000*/;
      public const uint CPU_INTEL_MOBILE = 16785408 /*0x01002000*/;
      public const uint CPU_INTEL_CORE = 16793600 /*0x01004000*/;
      public const uint CPU_INTEL_CORE_2 = 16809984 /*0x01008000*/;
      public const uint CPU_INTEL_BONNELL = 16842752 /*0x01010000*/;
      public const uint CPU_INTEL_SALTWELL = 16843008 /*0x01010100*/;
      public const uint CPU_INTEL_SILVERMONT = 16843264 /*0x01010200*/;
      public const uint CPU_INTEL_GOLDMONT = 16843776 /*0x01010400*/;
      public const uint CPU_INTEL_NEHALEM = 16908288 /*0x01020000*/;
      public const uint CPU_INTEL_SANDY_BRIDGE = 16908544 /*0x01020100*/;
      public const uint CPU_INTEL_HASWELL = 16908800 /*0x01020200*/;
      public const uint CPU_INTEL_SKYLAKE = 17039360 /*0x01040000*/;
      public const uint CPU_INTEL_ITANIUM = 17825792 /*0x01100000*/;
      public const uint CPU_INTEL_ITANIUM_2 = 17826048 /*0x01100100*/;
      public const uint CPU_INTEL_MIC = 18874368 /*0x01200000*/;
      public const uint CPU_PENTIUM = 16778241 /*0x01000401*/;
      public const uint CPU_PENTIUM_MMX = 16778242 /*0x01000402*/;
      public const uint CPU_PENTIUM_PRO = 16779265 /*0x01000801*/;
      public const uint CPU_PENTIUM_2 = 16779266 /*0x01000802*/;
      public const uint CPU_PENTIUM_2_M = 16779267 /*0x01000803*/;
      public const uint CPU_CELERON_P2 = 16779268 /*0x01000804*/;
      public const uint CPU_XEON_P2 = 16779269 /*0x01000805*/;
      public const uint CPU_PENTIUM_3 = 16779270 /*0x01000806*/;
      public const uint CPU_PENTIUM_3_M = 16779271 /*0x01000807*/;
      public const uint CPU_PENTIUM_3_S = 16779272 /*0x01000808*/;
      public const uint CPU_CELERON_P3 = 16779273 /*0x01000809*/;
      public const uint CPU_XEON_P3 = 16779274 /*0x0100080A*/;
      public const uint CPU_PENTIUM_4 = 16781313 /*0x01001001*/;
      public const uint CPU_PENTIUM_4_M = 16781314 /*0x01001002*/;
      public const uint CPU_PENTIUM_4_HT = 16781315 /*0x01001003*/;
      public const uint CPU_PENTIUM_4_EE = 16781316 /*0x01001004*/;
      public const uint CPU_CELERON_P4 = 16781317 /*0x01001005*/;
      public const uint CPU_CELERON_D = 16781318 /*0x01001006*/;
      public const uint CPU_XEON_P4 = 16781319 /*0x01001007*/;
      public const uint CPU_PENTIUM_D = 16781320 /*0x01001008*/;
      public const uint CPU_PENTIUM_XE = 16781321 /*0x01001009*/;
      public const uint CPU_PENTIUM_M = 16785409 /*0x01002001*/;
      public const uint CPU_CELERON_M = 16785410 /*0x01002002*/;
      public const uint CPU_CORE_SOLO = 16793601 /*0x01004001*/;
      public const uint CPU_CORE_DUO = 16793602 /*0x01004002*/;
      public const uint CPU_CORE_CELERON_M = 16793603 /*0x01004003*/;
      public const uint CPU_CORE_CELERON = 16793604 /*0x01004004*/;
      public const uint CPU_CORE_2_DUO = 16809985 /*0x01008001*/;
      public const uint CPU_CORE_2_EE = 16809986 /*0x01008002*/;
      public const uint CPU_CORE_2_XEON = 16809987 /*0x01008003*/;
      public const uint CPU_CORE_2_CELERON = 16809988 /*0x01008004*/;
      public const uint CPU_CORE_2_QUAD = 16809989 /*0x01008005*/;
      public const uint CPU_CORE_2_PENTIUM = 16809990 /*0x01008006*/;
      public const uint CPU_CORE_2_CELERON_DC = 16809991 /*0x01008007*/;
      public const uint CPU_CORE_2_SOLO = 16809992 /*0x01008008*/;
      public const uint CPU_BONNELL_ATOM = 16842753 /*0x01010001*/;
      public const uint CPU_SALTWELL_ATOM = 16843009;
      public const uint CPU_SILVERMONT_ATOM = 16843265;
      public const uint CPU_SILVERMONT_CELERON = 16843266;
      public const uint CPU_SILVERMONT_PENTIUM = 16843267;
      public const uint CPU_SILVERMONT_ATOM_X7 = 16843268;
      public const uint CPU_SILVERMONT_ATOM_X5 = 16843269;
      public const uint CPU_SILVERMONT_ATOM_X3 = 16843270;
      public const uint CPU_GOLDMONT_ATOM = 16843777;
      public const uint CPU_GOLDMONT_CELERON = 16843778;
      public const uint CPU_GOLDMONT_PENTIUM = 16843779;
      public const uint CPU_NEHALEM_CORE_I7 = 16908289 /*0x01020001*/;
      public const uint CPU_NEHALEM_CORE_I7E = 16908290 /*0x01020002*/;
      public const uint CPU_NEHALEM_XEON = 16908291 /*0x01020003*/;
      public const uint CPU_NEHALEM_CORE_I3 = 16908292 /*0x01020004*/;
      public const uint CPU_NEHALEM_CORE_I5 = 16908293 /*0x01020005*/;
      public const uint CPU_NEHALEM_PENTIUM = 16908295 /*0x01020007*/;
      public const uint CPU_NEHALEM_CELERON = 16908296 /*0x01020008*/;
      public const uint CPU_SANDY_BRIDGE_CORE_I7 = 16908545;
      public const uint CPU_SANDY_BRIDGE_CORE_I7E = 16908546;
      public const uint CPU_SANDY_BRIDGE_XEON = 16908547;
      public const uint CPU_SANDY_BRIDGE_CORE_I3 = 16908548;
      public const uint CPU_SANDY_BRIDGE_CORE_I5 = 16908549;
      public const uint CPU_SANDY_BRIDGE_PENTIUM = 16908551;
      public const uint CPU_SANDY_BRIDGE_CELERON = 16908552;
      public const uint CPU_HASWELL_CORE_I7 = 16908801;
      public const uint CPU_HASWELL_CORE_I7E = 16908802;
      public const uint CPU_HASWELL_XEON = 16908803;
      public const uint CPU_HASWELL_CORE_I3 = 16908804;
      public const uint CPU_HASWELL_CORE_I5 = 16908805;
      public const uint CPU_HASWELL_PENTIUM = 16908807;
      public const uint CPU_HASWELL_CELERON = 16908808;
      public const uint CPU_HASWELL_CORE_M = 16908809;
      public const uint CPU_SKYLAKE_XEON = 17039361 /*0x01040001*/;
      public const uint CPU_SKYLAKE_CORE_I7 = 17039362 /*0x01040002*/;
      public const uint CPU_SKYLAKE_CORE_I5 = 17039363 /*0x01040003*/;
      public const uint CPU_SKYLAKE_CORE_I3 = 17039364 /*0x01040004*/;
      public const uint CPU_SKYLAKE_PENTIUM = 17039365 /*0x01040005*/;
      public const uint CPU_SKYLAKE_CELERON = 17039366 /*0x01040006*/;
      public const uint CPU_SKYLAKE_CORE_M7 = 17039367 /*0x01040007*/;
      public const uint CPU_SKYLAKE_CORE_M5 = 17039368 /*0x01040008*/;
      public const uint CPU_SKYLAKE_CORE_M3 = 17039369 /*0x01040009*/;
      public const uint CPU_SKYLAKE_CORE_I9EX = 17039370 /*0x0104000A*/;
      public const uint CPU_SKYLAKE_CORE_I9X = 17039371 /*0x0104000B*/;
      public const uint CPU_SKYLAKE_CORE_I7X = 17039372 /*0x0104000C*/;
      public const uint CPU_SKYLAKE_CORE_I5X = 17039373 /*0x0104000D*/;
      public const uint CPU_SKYLAKE_XEON_BRONZE = 17039374 /*0x0104000E*/;
      public const uint CPU_SKYLAKE_XEON_SILVER = 17039375 /*0x0104000F*/;
      public const uint CPU_SKYLAKE_XEON_GOLD = 17039376 /*0x01040010*/;
      public const uint CPU_SKYLAKE_XEON_PLATINIUM = 17039377;
      public const uint CPU_SKYLAKE_PENTIUM_GOLD = 17039378;
      public const uint CPU_SKYLAKE_CORE_I9_GEN9 = 17039392 /*0x01040020*/;
      public const uint CPU_SKYLAKE_CORE_I7_GEN9 = 17039393;
      public const uint CPU_SKYLAKE_CORE_I5_GEN9 = 17039394;
      public const uint CPU_SKYLAKE_CORE_I3_GEN9 = 17039395;
      public const uint CPU_SKYLAKE_CORE_I9_GEN10 = 17039396;
      public const uint CPU_SKYLAKE_CORE_I7_GEN10 = 17039397;
      public const uint CPU_SKYLAKE_CORE_I5_GEN10 = 17039398;
      public const uint CPU_SKYLAKE_CORE_I3_GEN10 = 17039399;
      public const uint CPU_SKYLAKE_CORE_I9_GEN11 = 17039400;
      public const uint CPU_SKYLAKE_CORE_I7_GEN11 = 17039401;
      public const uint CPU_SKYLAKE_CORE_I5_GEN11 = 17039402;
      public const uint CPU_SKYLAKE_CORE_I3_GEN11 = 17039403;
      public const uint CPU_SKYLAKE_CORE_I9_GEN12 = 17039404;
      public const uint CPU_SKYLAKE_CORE_I7_GEN12 = 17039405;
      public const uint CPU_SKYLAKE_CORE_I5_GEN12 = 17039406;
      public const uint CPU_SKYLAKE_CORE_I3_GEN12 = 17039407;
      public const uint CPU_SKYLAKE_CELERON_GEN12 = 17039408 /*0x01040030*/;
      public const uint CPU_SKYLAKE_PENTIUM_GEN12 = 17039409;
      public const uint CPU_SKYLAKE_XEON_GEN10 = 17039410;
      public const uint CPU_SKYLAKE_XEON_SILVER_GEN10 = 17039411;
      public const uint CPU_SKYLAKE_XEON_GOLD_GEN10 = 17039412;
      public const uint CPU_SKYLAKE_XEON_PLATINUM_GEN10 = 17039413;
      public const uint CPU_SKYLAKE_CORE_N_GEN12 = 17039414;
      public const uint CPU_INTEL_CORE_GEN15 = 17039415;
      public const uint CPU_INTEL_CORE_3_GEN15 = 17039416;
      public const uint CPU_INTEL_CORE_5_GEN15 = 17039417;
      public const uint CPU_INTEL_CORE_7_GEN15 = 17039418;
      public const uint CPU_INTEL_CORE_ULTRA_GEN15 = 17039425;
      public const uint CPU_INTEL_CORE_ULTRA_5_GEN15 = 17039426;
      public const uint CPU_INTEL_CORE_ULTRA_7_GEN15 = 17039427;
      public const uint CPU_INTEL_CORE_ULTRA_9_GEN15 = 17039428;
      public const uint CPU_SKYLAKE_PENTIUM_GOLD_GEN12 = 17039429;
      public const uint CPU_AMD_386 = 33554688 /*0x02000100*/;
      public const uint CPU_AMD_486 = 33554944 /*0x02000200*/;
      public const uint CPU_AMD_K5 = 33555456 /*0x02000400*/;
      public const uint CPU_AMD_K6 = 33556480 /*0x02000800*/;
      public const uint CPU_AMD_K7 = 33558528 /*0x02001000*/;
      public const uint CPU_AMD_K8 = 33562624 /*0x02002000*/;
      public const uint CPU_AMD_K10 = 33570816 /*0x02004000*/;
      public const uint CPU_AMD_K12 = 33619968 /*0x02010000*/;
      public const uint CPU_AMD_K14 = 33685504 /*0x02020000*/;
      public const uint CPU_AMD_K15 = 33816576 /*0x02040000*/;
      public const uint CPU_AMD_K16 = 34078720 /*0x02080000*/;
      public const uint CPU_AMD_K17 = 34603008 /*0x02100000*/;
      public const uint CPU_K5 = 33555457 /*0x02000401*/;
      public const uint CPU_K5_GEODE = 33555458 /*0x02000402*/;
      public const uint CPU_K6 = 33556481 /*0x02000801*/;
      public const uint CPU_K6_2 = 33556482 /*0x02000802*/;
      public const uint CPU_K6_3 = 33556483 /*0x02000803*/;
      public const uint CPU_K7_ATHLON = 33558529 /*0x02001001*/;
      public const uint CPU_K7_ATHLON_XP = 33558530 /*0x02001002*/;
      public const uint CPU_K7_ATHLON_MP = 33558531 /*0x02001003*/;
      public const uint CPU_K7_DURON = 33558532 /*0x02001004*/;
      public const uint CPU_K7_SEMPRON = 33558533 /*0x02001005*/;
      public const uint CPU_K7_SEMPRON_M = 33558534 /*0x02001006*/;
      public const uint CPU_K8_ATHLON_64 = 33562625 /*0x02002001*/;
      public const uint CPU_K8_ATHLON_64_M = 33562626 /*0x02002002*/;
      public const uint CPU_K8_ATHLON_64_FX = 33562627 /*0x02002003*/;
      public const uint CPU_K8_OPTERON = 33562628 /*0x02002004*/;
      public const uint CPU_K8_TURION_64 = 33562629 /*0x02002005*/;
      public const uint CPU_K8_SEMPRON = 33562630 /*0x02002006*/;
      public const uint CPU_K8_SEMPRON_M = 33562631 /*0x02002007*/;
      public const uint CPU_K8_ATHLON_64_X2 = 33562632 /*0x02002008*/;
      public const uint CPU_K8_TURION_64_X2 = 33562633 /*0x02002009*/;
      public const uint CPU_K8_ATHLON_NEO = 33562634 /*0x0200200A*/;
      public const uint CPU_K10_PHENOM = 33570817 /*0x02004001*/;
      public const uint CPU_K10_PHENOM_X3 = 33570818 /*0x02004002*/;
      public const uint CPU_K10_PHENOM_FX = 33570819 /*0x02004003*/;
      public const uint CPU_K10_OPTERON = 33570820 /*0x02004004*/;
      public const uint CPU_K10_TURION_64 = 33570821 /*0x02004005*/;
      public const uint CPU_K10_TURION_64_ULTRA = 33570822 /*0x02004006*/;
      public const uint CPU_K10_ATHLON_64 = 33570823 /*0x02004007*/;
      public const uint CPU_K10_SEMPRON = 33570824 /*0x02004008*/;
      public const uint CPU_K10_ATHLON_2 = 33570833;
      public const uint CPU_K10_ATHLON_2_X2 = 33570827 /*0x0200400B*/;
      public const uint CPU_K10_ATHLON_2_X3 = 33570829 /*0x0200400D*/;
      public const uint CPU_K10_ATHLON_2_X4 = 33570828 /*0x0200400C*/;
      public const uint CPU_K10_PHENOM_II = 33570825 /*0x02004009*/;
      public const uint CPU_K10_PHENOM_II_X2 = 33570826 /*0x0200400A*/;
      public const uint CPU_K10_PHENOM_II_X3 = 33570830 /*0x0200400E*/;
      public const uint CPU_K10_PHENOM_II_X4 = 33570831 /*0x0200400F*/;
      public const uint CPU_K10_PHENOM_II_X6 = 33570832 /*0x02004010*/;
      public const uint CPU_K15_FXB = 33816577 /*0x02040001*/;
      public const uint CPU_K15_OPTERON = 33816578 /*0x02040002*/;
      public const uint CPU_K15_A10T = 33816579 /*0x02040003*/;
      public const uint CPU_K15_A8T = 33816580 /*0x02040004*/;
      public const uint CPU_K15_A6T = 33816581 /*0x02040005*/;
      public const uint CPU_K15_A4T = 33816582 /*0x02040006*/;
      public const uint CPU_K15_ATHLON_X4 = 33816583 /*0x02040007*/;
      public const uint CPU_K15_FXV = 33816584 /*0x02040008*/;
      public const uint CPU_K15_A10R = 33816585 /*0x02040009*/;
      public const uint CPU_K15_A8R = 33816586 /*0x0204000A*/;
      public const uint CPU_K15_A6R = 33816587 /*0x0204000B*/;
      public const uint CPU_K15_A4R = 33816588 /*0x0204000C*/;
      public const uint CPU_K15_SEMPRON = 33816589 /*0x0204000D*/;
      public const uint CPU_K15_ATHLON_X2 = 33816590 /*0x0204000E*/;
      public const uint CPU_K15_FXC = 33816591 /*0x0204000F*/;
      public const uint CPU_K15_A10C = 33816592 /*0x02040010*/;
      public const uint CPU_K15_A8C = 33816593;
      public const uint CPU_K15_A6C = 33816594;
      public const uint CPU_K15_A4C = 33816595;
      public const uint CPU_K15_A12 = 33816596;
      public const uint CPU_K15_RX = 33816597;
      public const uint CPU_K15_GX = 33816598;
      public const uint CPU_K15_A9 = 33816599;
      public const uint CPU_K15_E2 = 33816600;
      public const uint CPU_K16_A6 = 34078721 /*0x02080001*/;
      public const uint CPU_K16_A4 = 34078722 /*0x02080002*/;
      public const uint CPU_K16_OPTERON = 34078725 /*0x02080005*/;
      public const uint CPU_K16_ATHLON = 34078726 /*0x02080006*/;
      public const uint CPU_K16_SEMPRON = 34078727 /*0x02080007*/;
      public const uint CPU_K16_E1 = 34078728 /*0x02080008*/;
      public const uint CPU_K16_E2 = 34078729 /*0x02080009*/;
      public const uint CPU_K16_A8 = 34078730 /*0x0208000A*/;
      public const uint CPU_K16_A10 = 34078731 /*0x0208000B*/;
      public const uint CPU_K16_GX = 34078732 /*0x0208000C*/;
      public const uint CPU_RYZEN = 34603009 /*0x02100001*/;
      public const uint CPU_RYZEN_7 = 34603010 /*0x02100002*/;
      public const uint CPU_RYZEN_5 = 34603011 /*0x02100003*/;
      public const uint CPU_RYZEN_3 = 34603012 /*0x02100004*/;
      public const uint CPU_RYZEN_TR = 34603013 /*0x02100005*/;
      public const uint CPU_RYZEN_EPYC = 34603014 /*0x02100006*/;
      public const uint CPU_RYZEN_M = 34603015 /*0x02100007*/;
      public const uint CPU_RYZEN_7_M = 34603016 /*0x02100008*/;
      public const uint CPU_RYZEN_5_M = 34603017 /*0x02100009*/;
      public const uint CPU_RYZEN_3_M = 34603018 /*0x0210000A*/;
      public const uint CPU_RYZEN_ATHLON = 34603019 /*0x0210000B*/;
      public const uint CPU_RYZEN_9 = 34603020 /*0x0210000C*/;
      public const uint CPU_RYZEN_9_M = 34603021 /*0x0210000D*/;
      public const uint CPU_RYZEN_Z1 = 34603022 /*0x0210000E*/;
      public const uint CPU_RYZEN_Z1_EXTREME = 34603023 /*0x0210000F*/;
      public const uint CPU_RYZEN_AI_9HX = 34603024 /*0x02100010*/;
      public const uint CPU_RYZEN_AI_9 = 34603025;
      public const uint CPU_RYZEN_AI_7 = 34603026;
      public const uint CPU_RYZEN_AI_5 = 34603027;
      public const uint CPU_RYZEN_GEN9 = 34603028;
      public const uint CPU_RYZEN_9_GEN9 = 34603029;
      public const uint CPU_RYZEN_7_GEN9 = 34603030;
      public const uint CPU_RYZEN_5_GEN9 = 34603031;
      public const uint CPU_RYZEN_3_GEN9 = 34603032;
      public const uint CPU_CX486 = 67109888 /*0x04000400*/;
      public const uint CPU_CX5X86 = 67110144 /*0x04000500*/;
      public const uint CPU_CX6X86 = 67110400 /*0x04000600*/;
      public const uint CPU_VIA_WINCHIP = 134218752 /*0x08000400*/;
      public const uint CPU_VIA_C3 = 134219776 /*0x08000800*/;
      public const uint CPU_VIA_C7 = 134221824 /*0x08001000*/;
      public const uint CPU_VIA_NANO = 134225920 /*0x08002000*/;
      public const uint CPU_VIA_CHA = 134234112 /*0x08004000*/;
      public const uint CPU_ZHAOXIN_KX = 134488064 /*0x08042000*/;
      public const uint CPU_C3 = 134219777 /*0x08000801*/;
      public const uint CPU_C7 = 134221825 /*0x08001001*/;
      public const uint CPU_C7_M = 134221826 /*0x08001002*/;
      public const uint CPU_EDEN = 134221827 /*0x08001003*/;
      public const uint CPU_C7_D = 134221828 /*0x08001004*/;
      public const uint CPU_NANO_X2 = 134225921 /*0x08002001*/;
      public const uint CPU_EDEN_X2 = 134225922 /*0x08002002*/;
      public const uint CPU_NANO_X3 = 134225923 /*0x08002003*/;
      public const uint CPU_EDEN_X4 = 134225924 /*0x08002004*/;
      public const uint CPU_QUADCORE = 134225925 /*0x08002005*/;
      public const uint CPU_CX6X86L = 67110401 /*0x04000601*/;
      public const uint CPU_MEDIAGX = 67110402 /*0x04000602*/;
      public const uint CPU_CX6X86MX = 67110403 /*0x04000603*/;
      public const uint CPU_MII = 67110404 /*0x04000604*/;
      public const uint CPU_CRUSOE = 268435457 /*0x10000001*/;
      public const uint CPU_EFFICEON = 268435458 /*0x10000002*/;
      public const uint CPU_VORTEX86_SX = 536870913 /*0x20000001*/;
      public const uint CPU_VORTEX86_EX = 536870914 /*0x20000002*/;
      public const uint CPU_VORTEX86_DX = 536870915 /*0x20000003*/;
      public const uint CPU_VORTEX86_MX = 536870916 /*0x20000004*/;
      public const uint CPU_VORTEX86_DX3 = 536870917 /*0x20000005*/;
      public const uint CPU_HYGON_C86 = 2214592768 /*0x84000100*/;
      public const int HYBRID_CORE_TYPE_UNKNOWN = 0;
      public const int HYBRID_CORE_TYPE_EFFICIENCY_LP = 16 /*0x10*/;
      public const int HYBRID_CORE_TYPE_EFFICIENCY = 32 /*0x20*/;
      public const int HYBRID_CORE_TYPE_PERFORMANCE = 64 /*0x40*/;
      public const int CACHE_TYPE_DATA = 1;
      public const int CACHE_TYPE_INSTRUCTION = 2;
      public const int CACHE_TYPE_UNIFIED = 3;
      public const int CACHE_TYPE_TRACE_CACHE = 4;
      public const int ISET_MMX = 1;
      public const int ISET_EXTENDED_MMX = 2;
      public const int ISET_3DNOW = 3;
      public const int ISET_EXTENDED_3DNOW = 4;
      public const int ISET_SSE = 5;
      public const int ISET_SSE2 = 6;
      public const int ISET_SSE3 = 7;
      public const int ISET_SSSE3 = 8;
      public const int ISET_SSE4_1 = 9;
      public const int ISET_SSE4_2 = 12;
      public const int ISET_SSE4A = 13;
      public const int ISET_XOP = 14;
      public const int ISET_X86_64 = 16 /*0x10*/;
      public const int ISET_NX = 17;
      public const int ISET_VMX = 18;
      public const int ISET_AES = 19;
      public const int ISET_AVX = 20;
      public const int ISET_AVX2 = 21;
      public const int ISET_FMA3 = 22;
      public const int ISET_FMA4 = 23;
      public const int ISET_RTM = 24;
      public const int ISET_HLE = 25;
      public const int ISET_SHA = 27;
      public const int ISET_AMX_TILE = 31 /*0x1F*/;
      public const int ISET_AMX_INT8 = 32 /*0x20*/;
      public const int ISET_AMX_BF16 = 33;
      public const int ISET_SM2 = 34;
      public const int ISET_SM3 = 35;
      public const int ISET_SM4 = 36;
      public const int ISET_AVX512F = 26;
      public const int ISET_AVX512DQ = 28;
      public const int ISET_AVX512BW = 29;
      public const int ISET_AVX512VL = 30;
      public const int ISET_AVX512_IFMA = 37;
      public const int ISET_AVX512CD = 38;
      public const int ISET_AVX512_VBMI = 39;
      public const int ISET_AVX512_VBMI2 = 40;
      public const int ISET_AVX512_VNNI = 41;
      public const int ISET_AVX512_BITALG = 42;
      public const int ISET_AVX512_VPOPCNTDQ = 43;
      public const int ISET_AVX512_VP2INTERSECT = 44;
      public const int ISET_AVX512_BF16 = 45;
      public const int ISET_AVX512_FP16 = 46;
      public const int ISET_AVX_VNNI = 47;
      public const int ISET_SHA256 = 48 /*0x30*/;
      public const int ISET_AVX10 = 49;
      public const int HWM_CLASS_LPC = 1;
      public const int HWM_CLASS_CPU = 2;
      public const int HWM_CLASS_HDD = 4;
      public const int HWM_CLASS_DISPLAYADAPTER = 8;
      public const int HWM_CLASS_PSU = 16 /*0x10*/;
      public const int HWM_CLASS_ACPI = 32 /*0x20*/;
      public const int HWM_CLASS_RAM = 64 /*0x40*/;
      public const int HWM_CLASS_CHASSIS = 128 /*0x80*/;
      public const int HWM_CLASS_WATERCOOLER = 256 /*0x0100*/;
      public const int HWM_CLASS_BATTERY = 512 /*0x0200*/;
      public const int SENSOR_CLASS_VOLTAGE = 4096 /*0x1000*/;
      public const int SENSOR_CLASS_TEMPERATURE = 8192 /*0x2000*/;
      public const int SENSOR_CLASS_FAN = 12288 /*0x3000*/;
      public const int SENSOR_CLASS_CURRENT = 16384 /*0x4000*/;
      public const int SENSOR_CLASS_POWER = 20480 /*0x5000*/;
      public const int SENSOR_CLASS_FAN_PWM = 24576 /*0x6000*/;
      public const int SENSOR_CLASS_PUMP_PWM = 28672 /*0x7000*/;
      public const int SENSOR_CLASS_WATER_LEVEL = 32768 /*0x8000*/;
      public const int SENSOR_CLASS_POSITION = 36864 /*0x9000*/;
      public const int SENSOR_CLASS_CAPACITY = 40960 /*0xA000*/;
      public const int SENSOR_CLASS_CASEOPEN = 45056 /*0xB000*/;
      public const int SENSOR_CLASS_LEVEL = 49152 /*0xC000*/;
      public const int SENSOR_CLASS_COUNTER = 53248 /*0xD000*/;
      public const int SENSOR_CLASS_UTILIZATION = 57344 /*0xE000*/;
      public const int SENSOR_CLASS_CLOCK_SPEED = 61440 /*0xF000*/;
      public const int SENSOR_CLASS_BANDWIDTH = 65536 /*0x010000*/;
      public const int SENSOR_CLASS_PERF_LIMITER = 69632 /*0x011000*/;
      public const int SENSOR_VOLTAGE_VCORE = 4198400 /*0x401000*/;
      public const int SENSOR_VOLTAGE_3V3 = 8392704 /*0x801000*/;
      public const int SENSOR_VOLTAGE_P5V = 12587008 /*0xC01000*/;
      public const int SENSOR_VOLTAGE_P12V = 16781312 /*0x01001000*/;
      public const int SENSOR_VOLTAGE_M5V = 20975616 /*0x01401000*/;
      public const int SENSOR_VOLTAGE_M12V = 25169920 /*0x01801000*/;
      public const int SENSOR_VOLTAGE_5VSB = 29364224 /*0x01C01000*/;
      public const int SENSOR_VOLTAGE_DRAM = 33558528 /*0x02001000*/;
      public const int SENSOR_VOLTAGE_CPU_VTT = 37752832 /*0x02401000*/;
      public const int SENSOR_VOLTAGE_IOH_VCORE = 41947136 /*0x02801000*/;
      public const int SENSOR_VOLTAGE_IOH_PLL = 46141440 /*0x02C01000*/;
      public const int SENSOR_VOLTAGE_CPU_PLL = 50335744 /*0x03001000*/;
      public const int SENSOR_VOLTAGE_PCH = 54530048 /*0x03401000*/;
      public const int SENSOR_VOLTAGE_CPU_VID = 58724352 /*0x03801000*/;
      public const int SENSOR_VOLTAGE_MAX_CPU_VID = 62918656 /*0x03C01000*/;
      public const int SENSOR_VOLTAGE_MAX_CORESET_CPU_VID = 67112960 /*0x04001000*/;
      public const int SENSOR_VOLTAGE_GPU = 71307264 /*0x04401000*/;
      public const int SENSOR_TEMPERATURE_CPU = 4202496 /*0x402000*/;
      public const int SENSOR_TEMPERATURE_VREG = 8396800 /*0x802000*/;
      public const int SENSOR_TEMPERATURE_DRAM = 12591104 /*0xC02000*/;
      public const int SENSOR_TEMPERATURE_PCH = 16785408 /*0x01002000*/;
      public const int SENSOR_TEMPERATURE_CPU_PACKAGE = 25174016 /*0x01802000*/;
      public const int SENSOR_TEMPERATURE_CPU_NODE = 29368320 /*0x01C02000*/;
      public const int SENSOR_TEMPERATURE_CPU_CCD = 33562624 /*0x02002000*/;
      public const int SENSOR_TEMPERATURE_CPU_CORE = 37756928 /*0x02402000*/;
      public const int SENSOR_TEMPERATURE_MAX_CPU_CORE = 41951232 /*0x02802000*/;
      public const int SENSOR_TEMPERATURE_MAX_CORESET_CPU_CORE = 46145536 /*0x02C02000*/;
      public const int SENSOR_TEMPERATURE_CPU_VRM = 50339840 /*0x03002000*/;
      public const int SENSOR_TEMPERATURE_GPU = 67117056 /*0x04002000*/;
      public const int SENSOR_TEMPERATURE_GPU_HOTSPOT = 71311360 /*0x04402000*/;
      public const int SENSOR_FAN_CPU = 4206592 /*0x403000*/;
      public const int SENSOR_FAN_PUMP = 8400896 /*0x803000*/;
      public const int SENSOR_FAN_GPU = 25178112 /*0x01803000*/;
      public const int SENSOR_POWER_CPU_PACKAGE = 4214784 /*0x405000*/;
      public const int SENSOR_POWER_CPU_ALL_CORES = 8409088 /*0x805000*/;
      public const int SENSOR_POWER_CPU_CORE = 12603392 /*0xC05000*/;
      public const int SENSOR_POWER_CPU_GT = 20992000 /*0x01405000*/;
      public const int SENSOR_POWER_GPU = 29380608 /*0x01C05000*/;
      public const int SENSOR_UTILIZATION_CPU_PACKAGE = 4251648 /*0x40E000*/;
      public const int SENSOR_UTILIZATION_CPU_CORESET = 8445952 /*0x80E000*/;
      public const int SENSOR_UTILIZATION_CPU_CCX = 12640256 /*0xC0E000*/;
      public const int SENSOR_UTILIZATION_CPU_CORE = 16834560 /*0x0100E000*/;
      public const int SENSOR_UTILIZATION_CPU_THREAD = 21028864 /*0x0140E000*/;
      public const int SENSOR_UTILIZATION_GPU = 25223168 /*0x0180E000*/;
      public const int SENSOR_UTILIZATION_MEMORY = 29417472 /*0x01C0E000*/;
      public const int SENSOR_CLOCK_CPU_BCLK = 4255744 /*0x40F000*/;
      public const int SENSOR_CLOCK_SOC_BCLK = 8450048 /*0x80F000*/;
      public const int MEMORY_TYPE_SPM_RAM = 1;
      public const int MEMORY_TYPE_RDRAM = 2;
      public const int MEMORY_TYPE_EDO_RAM = 3;
      public const int MEMORY_TYPE_FPM_RAM = 4;
      public const int MEMORY_TYPE_SDRAM = 5;
      public const int MEMORY_TYPE_DDR_SDRAM = 6;
      public const int MEMORY_TYPE_DDR2_SDRAM = 7;
      public const int MEMORY_TYPE_DDR2_SDRAM_FB = 8;
      public const int MEMORY_TYPE_DDR3_SDRAM = 9;
      public const int MEMORY_TYPE_DDR4_SDRAM = 10;
      public const int MEMORY_TYPE_DDR5_SDRAM = 11;
      public const int DISPLAY_CLOCK_DOMAIN_GRAPHICS = 0;
      public const int DISPLAY_CLOCK_DOMAIN_MEMORY = 1;
      public const int DISPLAY_CLOCK_DOMAIN_PROCESSOR = 2;
      public const int MEMORY_TYPE_SDR = 1;
      public const int MEMORY_TYPE_DDR = 2;
      public const int MEMORY_TYPE_LPDDR2 = 9;
      public const int MEMORY_TYPE_DDR2 = 3;
      public const int MEMORY_TYPE_DDR3 = 7;
      public const int MEMORY_TYPE_GDDR2 = 4;
      public const int MEMORY_TYPE_GDDR3 = 5;
      public const int MEMORY_TYPE_GDDR4 = 6;
      public const int MEMORY_TYPE_GDDR5 = 8;
      public const int MEMORY_TYPE_GDDR5X = 10;
      public const int MEMORY_TYPE_HBM1 = 11;
      public const int MEMORY_TYPE_HBM2 = 12;
      public const int MEMORY_TYPE_SDDR4 = 13;
      public const int MEMORY_TYPE_GDDR6 = 14;
      public const int MEMORY_TYPE_GDDR6X = 15;
      public const int MEMORY_TYPE_DDR4 = 34;
      public const int MEMORY_TYPE_DDR5 = 35;
      public const int MEMORY_TYPE_LPDDR = 40;
      public const int MEMORY_TYPE_LPDDR3 = 43;
      public const int MEMORY_TYPE_LPDDR4 = 44;
      public const int MEMORY_TYPE_LPDDR5 = 45;
      public const int GRAPHIC_BUS_ISA = 0;
      public const int GRAPHIC_BUS_VLB = 1;
      public const int GRAPHIC_BUS_PCI = 2;
      public const int GRAPHIC_BUS_AGP = 3;
      public const int GRAPHIC_BUS_PCIE = 4;
      public const int DRIVE_FEATURE_IS_SSD = 1;
      public const int DRIVE_FEATURE_SMART = 2;
      public const int DRIVE_FEATURE_TRIM = 4;
      public const int DRIVE_FEATURE_IS_REMOVABLE = 16 /*0x10*/;
      public const int BUS_TYPE_SCSI = 1;
      public const int BUS_TYPE_ATAPI = 2;
      public const int BUS_TYPE_ATA = 3;
      public const int BUS_TYPE_IEEE1394 = 4;
      public const int BUS_TYPE_SSA = 5;
      public const int BUS_TYPE_FIBRE = 6;
      public const int BUS_TYPE_USB = 7;
      public const int BUS_TYPE_RAID = 8;
      public const int BUS_TYPE_ISCSI = 9;
      public const int BUS_TYPE_SAS = 10;
      public const int BUS_TYPE_SATA = 11;
      public const int BUS_TYPE_SD = 12;
      public const int BUS_TYPE_MMC = 13;
      public const int BUS_TYPE_VIRTUAL = 14;
      public const int BUS_TYPE_FILEBACKEDVIRTUAL = 15;
      public const int BUS_TYPE_SPACES = 16 /*0x10*/;
      public const int BUS_TYPE_NVME = 17;
      protected IntPtr objptr = IntPtr.Zero;

      public bool IS_F_DEFINED(float _f) => (double) _f > 0.0;

      public bool IS_F_DEFINED(double _f) => _f > 0.0;

      public bool IS_I_DEFINED(int _i) => _i != CPUIDSDK.I_UNDEFINED_VALUE;

      public bool IS_I_DEFINED(uint _i) => (int) _i != CPUIDSDK.I_UNDEFINED_VALUE;

      public bool IS_I_DEFINED(short _i) => (int) _i != (int) (short) CPUIDSDK.I_UNDEFINED_VALUE;

      public bool IS_I_DEFINED(ushort _i) => (int) _i != (int) (ushort) CPUIDSDK.I_UNDEFINED_VALUE;

      public bool IS_I_DEFINED(sbyte _i) => (int) _i != (int) (sbyte) CPUIDSDK.I_UNDEFINED_VALUE;

      public bool IS_I_DEFINED(byte _i) => (int) _i != (int) (byte) CPUIDSDK.I_UNDEFINED_VALUE;

      [DllImport("cpuidsdk64.dll", EntryPoint = "QueryInterface")]
      protected static extern IntPtr CPUIDSDK_fp_QueryInterface(uint _code);

      public bool CreateInstance()
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2707899086U);
          if (ptr != IntPtr.Zero)
          {
            this.objptr = ((CPUIDSDK.CPUIDSDK_fp_CreateInstance) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_CreateInstance)))();
            return this.objptr != IntPtr.Zero;
          }
          this.objptr = IntPtr.Zero;
          return false;
        }
        catch (Exception ex)
        {
          string message = ex.Message;
          return false;
        }
      }

      public void DestroyInstance()
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3488849895U);
          if (!(ptr != IntPtr.Zero))
            return;
          ((CPUIDSDK.CPUIDSDK_fp_DestroyInstance) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_DestroyInstance)))(this.objptr);
        }
        catch
        {
        }
      }

      public bool Init(
        string _szDllPath,
        string _szDllFilename,
        uint _config_flag,
        ref int _errorcode,
        ref int _extended_errorcode)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(837051336U);
          if (ptr != IntPtr.Zero)
            return ((CPUIDSDK.CPUIDSDK_fp_Init) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_Init)))(this.objptr, _szDllPath, _szDllFilename, (int) _config_flag, ref _errorcode, ref _extended_errorcode) == 1;
          _errorcode = 16 /*0x10*/;
          return false;
        }
        catch
        {
          return false;
        }
      }

      public bool Init2(uint _config_flag, ref int _errorcode, ref int _extended_errorcode)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2208892754U);
          if (ptr != IntPtr.Zero)
            return ((CPUIDSDK.CPUIDSDK_fp_Init2) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_Init2)))(this.objptr, (int) _config_flag, ref _errorcode, ref _extended_errorcode) == 1;
          _errorcode = 16 /*0x10*/;
          return false;
        }
        catch
        {
          return false;
        }
      }

      public void Close()
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1074429973U);
          if (!(ptr != IntPtr.Zero))
            return;
          ((CPUIDSDK.CPUIDSDK_fp_Close) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_Close)))(this.objptr);
        }
        catch
        {
        }
      }

      public void RefreshInformation()
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2290159874U);
          if (!(ptr != IntPtr.Zero))
            return;
          ((CPUIDSDK.CPUIDSDK_fp_RefreshInformation) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_RefreshInformation)))(this.objptr);
        }
        catch
        {
        }
      }

      public void GetDllVersion(ref int _version)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(627198660U);
          if (!(ptr != IntPtr.Zero))
            return;
          ((CPUIDSDK.CPUIDSDK_fp_GetDllVersion) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetDllVersion)))(this.objptr, ref _version);
        }
        catch
        {
        }
      }

      public int GetNumberOfProcessors()
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1327930957U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetNbProcessors) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetNbProcessors)))(this.objptr) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetProcessorFamily(int _proc_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(284959224U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetProcessorFamily) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetProcessorFamily)))(this.objptr, _proc_index) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetProcessorCoreSetCount(int _proc_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2013851665U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetProcessorCoreSetCount) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetProcessorCoreSetCount)))(this.objptr, _proc_index) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetProcessorCoreCount(int _proc_index, int _core_set)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1357685209U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetProcessorCoreCount) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetProcessorCoreCount)))(this.objptr, _proc_index, _core_set) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetProcessorThreadCount(int _proc_index, int _core_set)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3707746815U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetProcessorThreadCount) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetProcessorThreadCount)))(this.objptr, _proc_index, _core_set) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetProcessorCoreSetType(int _proc_index, int _core_set)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(531775332U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetProcessorCoreSetType) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetProcessorCoreSetType)))(this.objptr, _proc_index, _core_set) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetProcessorCoreThreadCount(int _proc_index, int _core_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(113249664U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetProcessorCoreThreadCount) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetProcessorCoreThreadCount)))(this.objptr, _proc_index, _core_index) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetProcessorThreadAPICID(
        IntPtr objptr,
        int _proc_index,
        int _core_index,
        int _thread_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3709844031U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetProcessorThreadAPICID) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetProcessorThreadAPICID)))(objptr, _proc_index, _core_index, _thread_index) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public string GetProcessorName(int _proc_index)
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1975315321U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetProcessorName) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetProcessorName)))(this.objptr, _proc_index);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetProcessorCodeName(int _proc_index)
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(4006862247U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetProcessorCodeName) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetProcessorCodeName)))(this.objptr, _proc_index);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetProcessorSpecification(int _proc_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1860754897U);
          return ptr != IntPtr.Zero ? Marshal.PtrToStringAnsi(((CPUIDSDK.CPUIDSDK_fp_GetProcessorSpecification) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetProcessorSpecification)))(this.objptr, _proc_index)) : (string) null;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetProcessorPackage(int _proc_index)
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2673163946U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetProcessorPackage) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetProcessorPackage)))(this.objptr, _proc_index);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetProcessorStepping(int _proc_index)
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(511982856U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetProcessorStepping) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetProcessorStepping)))(this.objptr, _proc_index);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public float GetProcessorTDP(int _proc_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(4141739451U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetProcessorTDP) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetProcessorTDP)))(this.objptr, _proc_index) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public float GetProcessorTjmax(int _proc_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1419159853U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetProcessorTjmax) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetProcessorTjmax)))(this.objptr, _proc_index) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public float GetProcessorManufacturingProcess(int _proc_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3379663587U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetProcessorManufacturingProcess) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetProcessorManufacturingProcess)))(this.objptr, _proc_index) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public bool IsProcessorInstructionSetAvailable(int _proc_index, int _iset)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2711831366U);
          return ptr != IntPtr.Zero && ((CPUIDSDK.CPUIDSDK_fp_IsProcessorInstructionSetAvailable) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_IsProcessorInstructionSetAvailable)))(this.objptr, _proc_index, _iset) == 1;
        }
        catch
        {
          return false;
        }
      }

      public float GetProcessorCoreClockFrequency(int _proc_index, int _core_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2224752950U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetProcessorCoreClockFrequency) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetProcessorCoreClockFrequency)))(this.objptr, _proc_index, _core_index) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public float GetProcessorCoreClockMultiplier(int _proc_index, int _core_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2736080426U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetProcessorCoreClockMultiplier) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetProcessorCoreClockMultiplier)))(this.objptr, _proc_index, _core_index) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public float GetProcessorCoreTemperature(int _proc_index, int _core_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2419662962U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetProcessorCoreTemperature) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetProcessorCoreTemperature)))(this.objptr, _proc_index, _core_index) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public float GetBusFrequency()
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3575622207U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetBusFrequency) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetBusFrequency)))(this.objptr) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public float GetProcessorRatedBusFrequency(int _proc_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1111524481U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetProcessorRatedBusFrequency) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetProcessorRatedBusFrequency)))(this.objptr, _proc_index) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public float GetProcessorStockClockFrequency(int _proc_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(4111460895U);
          return ptr != IntPtr.Zero ? (float) ((CPUIDSDK.CPUIDSDK_fp_GetProcessorStockClockFrequency) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetProcessorStockClockFrequency)))(this.objptr, _proc_index) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public float GetProcessorStockBusFrequency(int _proc_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(4016561871U);
          return ptr != IntPtr.Zero ? (float) ((CPUIDSDK.CPUIDSDK_fp_GetProcessorStockBusFrequency) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetProcessorStockBusFrequency)))(this.objptr, _proc_index) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public int GetProcessorMaxCacheLevel(int _proc_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(470562840U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetProcessorMaxCacheLevel) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetProcessorMaxCacheLevel)))(this.objptr, _proc_index) : 0;
        }
        catch
        {
          return 0;
        }
      }

      public void GetProcessorCacheParameters(
        int _proc_index,
        int _core_set,
        int _cache_level,
        int _cache_type,
        ref int _NbCaches,
        ref int _size)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(997095132U);
          if (!(ptr != IntPtr.Zero))
            return;
          ((CPUIDSDK.CPUIDSDK_fp_GetProcessorCacheParameters) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetProcessorCacheParameters)))(this.objptr, _proc_index, _core_set, _cache_level, _cache_type, ref _NbCaches, ref _size);
        }
        catch
        {
        }
      }

      public uint GetProcessorID(int _proc_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1757204857U);
          return ptr != IntPtr.Zero ? (uint) ((CPUIDSDK.CPUIDSDK_fp_GetProcessorID) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetProcessorID)))(this.objptr, _proc_index) : 0U;
        }
        catch
        {
          return 0;
        }
      }

      public float GetProcessorVoltageID(int _proc_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3696605355U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetProcessorVoltageID) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetProcessorVoltageID)))(this.objptr, _proc_index) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public int GetMemoryType()
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(906783768U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetMemoryType) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetMemoryType)))(this.objptr) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetMemorySize()
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1339858873U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetMemorySize) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetMemorySize)))(this.objptr) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public float GetMemoryClockFrequency()
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3296037099U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetMemoryClockFrequency) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetMemoryClockFrequency)))(this.objptr) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public int GetMemoryNumberOfChannels()
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(4077381135U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetMemoryNumberOfChannels) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetMemoryNumberOfChannels)))(this.objptr) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public float GetMemoryCASLatency()
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2097740305U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetMemoryCASLatency) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetMemoryCASLatency)))(this.objptr) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public int GetMemoryRAStoCASDelay()
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2950258610U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetMemoryRAStoCASDelay) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetMemoryRAStoCASDelay)))(this.objptr) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetMemoryRASPrecharge()
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3629625519U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetMemoryRASPrecharge) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetMemoryRASPrecharge)))(this.objptr) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetMemoryTRAS()
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2364742118U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetMemoryTRAS) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetMemoryTRAS)))(this.objptr) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetMemoryTRC()
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2558079218U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetMemoryTRC) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetMemoryTRC)))(this.objptr) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetMemoryCommandRate()
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(932474664U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetMemoryCommandRate) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetMemoryCommandRate)))(this.objptr) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public string GetNorthBridgeVendor()
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2840285846U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetNorthBridgeVendor) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetNorthBridgeVendor)))(this.objptr);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetNorthBridgeModel()
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(4123650963U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetNorthBridgeModel) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetNorthBridgeModel)))(this.objptr);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetNorthBridgeRevision()
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(4290510711U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetNorthBridgeRevision) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetNorthBridgeRevision)))(this.objptr);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetSouthBridgeVendor()
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2645244758U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetSouthBridgeVendor) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetSouthBridgeVendor)))(this.objptr);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetSouthBridgeModel()
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(630868788U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetSouthBridgeModel) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetSouthBridgeModel)))(this.objptr);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetSouthBridgeRevision()
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1727319529U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetSouthBridgeRevision) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetSouthBridgeRevision)))(this.objptr);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public void GetMemorySlotsConfig(
        ref int _nbslots,
        ref int _nbusedslots,
        ref int _slotmap_h,
        ref int _slotmap_l,
        ref int _maxmodulesize)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3219488714U);
          if (!(ptr != IntPtr.Zero))
            return;
          ((CPUIDSDK.CPUIDSDK_fp_GetMemorySlotsConfig) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetMemorySlotsConfig)))(this.objptr, ref _nbslots, ref _nbusedslots, ref _slotmap_h, ref _slotmap_l, ref _maxmodulesize);
        }
        catch
        {
        }
      }

      public string GetBIOSVendor()
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(869558184U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetBIOSVendor) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetBIOSVendor)))(this.objptr);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetBIOSVersion()
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2149908554U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetBIOSVersion) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetBIOSVersion)))(this.objptr);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetBIOSDate()
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3313470207U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetBIOSDate) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetBIOSDate)))(this.objptr);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetMainboardVendor()
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3110302406U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetMainboardVendor) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetMainboardVendor)))(this.objptr);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetMainboardModel()
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(137498724U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetMainboardModel) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetMainboardModel)))(this.objptr);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetMainboardRevision()
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2975556278U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetMainboardRevision) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetMainboardRevision)))(this.objptr);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetMainboardSerialNumber()
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(497826648U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetMainboardSerialNumber) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetMainboardSerialNumber)))(this.objptr);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetSystemManufacturer()
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1265276629U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetSystemManufacturer) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetSystemManufacturer)))(this.objptr);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetSystemProductName()
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3843279399U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetSystemProductName) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetSystemProductName)))(this.objptr);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetSystemVersion()
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2199062054U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetSystemVersion) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetSystemVersion)))(this.objptr);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetSystemSerialNumber()
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2212038578U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetSystemSerialNumber) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetSystemSerialNumber)))(this.objptr);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetSystemUUID()
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2928631070U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetSystemUUID) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetSystemUUID)))(this.objptr);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetChassisManufacturer()
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2865714590U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetChassisManufacturer) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetChassisManufacturer)))(this.objptr);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetChassisType()
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(749754720U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetChassisType) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetChassisType)))(this.objptr);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetChassisSerialNumber()
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3403912647U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetChassisSerialNumber) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetChassisSerialNumber)))(this.objptr);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public bool GetMemoryInfosExt(
        ref string _szLocation,
        ref string _szUsage,
        ref string _szCorrection)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1994714569U);
          if (!(ptr != IntPtr.Zero))
            return false;
          IntPtr zero1 = IntPtr.Zero;
          IntPtr zero2 = IntPtr.Zero;
          IntPtr zero3 = IntPtr.Zero;
          if (((CPUIDSDK.CPUIDSDK_fp_GetMemoryInfosExt) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetMemoryInfosExt)))(this.objptr, ref zero1, ref zero2, ref zero3) != 1)
            return false;
          _szLocation = Marshal.PtrToStringAnsi(zero1);
          Marshal.FreeBSTR(zero1);
          _szUsage = Marshal.PtrToStringAnsi(zero2);
          Marshal.FreeBSTR(zero2);
          _szCorrection = Marshal.PtrToStringAnsi(zero3);
          Marshal.FreeBSTR(zero3);
          return true;
        }
        catch
        {
          return false;
        }
      }

      public int GetNumberOfMemoryDevices()
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(422720100U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetNumberOfMemoryDevices) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetNumberOfMemoryDevices)))(this.objptr) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public bool GetMemoryDeviceInfos(int _device_index, ref int _size, ref string _szFormat)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(4087998291U);
          if (!(ptr != IntPtr.Zero))
            return false;
          IntPtr zero = IntPtr.Zero;
          if (((CPUIDSDK.CPUIDSDK_fp_GetMemoryDeviceInfos) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetMemoryDeviceInfos)))(this.objptr, _device_index, ref _size, ref zero) != 1)
            return false;
          _szFormat = Marshal.PtrToStringAnsi(zero);
          Marshal.FreeBSTR(zero);
          return true;
        }
        catch
        {
          return false;
        }
      }

      public bool GetMemoryDeviceInfosExt(
        int _device_index,
        ref string _szDesignation,
        ref string _szType,
        ref int _total_width,
        ref int _data_width,
        ref int _speed)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2945539874U);
          if (!(ptr != IntPtr.Zero))
            return false;
          IntPtr zero1 = IntPtr.Zero;
          IntPtr zero2 = IntPtr.Zero;
          if (((CPUIDSDK.CPUIDSDK_fp_GetMemoryDeviceInfosExt) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetMemoryDeviceInfosExt)))(this.objptr, _device_index, ref zero1, ref zero2, ref _total_width, ref _data_width, ref _speed) != 1)
            return false;
          _szDesignation = Marshal.PtrToStringAnsi(zero1);
          Marshal.FreeBSTR(zero1);
          _szType = Marshal.PtrToStringAnsi(zero2);
          Marshal.FreeBSTR(zero2);
          return true;
        }
        catch
        {
          return false;
        }
      }

      public int GetMemoryMaxCapacity()
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(114822576U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetMemoryMaxCapacity) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetMemoryMaxCapacity)))(this.objptr) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetMemoryMaxNumberOfDevices()
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(663506712U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetMemoryMaxNumberOfDevices) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetMemoryMaxNumberOfDevices)))(this.objptr) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetProcessorSockets()
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1816451209U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetProcessorSockets) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetProcessorSockets)))(this.objptr) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public string GetSystemSKU()
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3887058783U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetSystemSKU) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetSystemSKU)))(this.objptr);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetSystemFamily()
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2416779290U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetSystemFamily) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetSystemFamily)))(this.objptr);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public int GetNumberOfSPDModules()
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2380078010U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetNumberOfSPDModules) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetNumberOfSPDModules)))(this.objptr) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetSPDModuleType(int _spd_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(268967952U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleType) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleType)))(this.objptr, _spd_index) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetSPDModuleSize(int _spd_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2138636017U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleSize) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleSize)))(this.objptr, _spd_index) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public string GetSPDModuleFormat(int _spd_index)
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3410859675U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleFormat) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleFormat)))(this.objptr, _spd_index);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetSPDModuleManufacturer(int _spd_index)
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(4275568047U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleManufacturer) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleManufacturer)))(this.objptr, _spd_index);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public bool GetSPDModuleManufacturerID(int _spd_index, byte[] _id)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2125659493U);
          return ptr != IntPtr.Zero && ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleManufacturerID) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleManufacturerID)))(this.objptr, _spd_index, _id) == 1;
        }
        catch
        {
          return false;
        }
      }

      public string GetSPDModuleDRAMManufacturer(int _spd_index)
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(117444096U /*0x07000E00*/);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleDRAMManufacturer) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleDRAMManufacturer)))(this.objptr, _spd_index);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public int GetSPDModuleMaxFrequency(int _spd_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1741606813U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleMaxFrequency) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleMaxFrequency)))(this.objptr, _spd_index) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public string GetSPDModuleSpecification(int _spd_index)
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(275914980U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleSpecification) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleSpecification)))(this.objptr, _spd_index);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetSPDModulePartNumber(int _spd_index)
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3057216626U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetSPDModulePartNumber) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModulePartNumber)))(this.objptr, _spd_index);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetSPDModuleSerialNumber(int _spd_index)
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3779969691U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleSerialNumber) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleSerialNumber)))(this.objptr, _spd_index);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public float GetSPDModuleMinTRCD(int _spd_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1297783477U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleMinTRCD) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleMinTRCD)))(this.objptr, _spd_index) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public float GetSPDModuleMinTRP(int _spd_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1530705529U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleMinTRP) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleMinTRP)))(this.objptr, _spd_index) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public float GetSPDModuleMinTRAS(int _spd_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(30933936U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleMinTRAS) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleMinTRAS)))(this.objptr, _spd_index) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public float GetSPDModuleMinTRC(int _spd_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(4253547279U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleMinTRC) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleMinTRC)))(this.objptr, _spd_index) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public int GetSPDModuleManufacturingDate(int _spd_index, ref int _year, ref int _week)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(478951704U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleManufacturingDate) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleManufacturingDate)))(this.objptr, _spd_index, ref _year, ref _week) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetSPDModuleNumberOfBanks(int _spd_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2136014497U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleNumberOfBanks) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleNumberOfBanks)))(this.objptr, _spd_index) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetSPDModuleDataWidth(int _spd_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(401616864U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleDataWidth) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleDataWidth)))(this.objptr, _spd_index) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public float GetSPDModuleTemperature(int _spd_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1202097997U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleTemperature) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleTemperature)))(this.objptr, _spd_index) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public int GetSPDModuleNumberOfProfiles(int _spd_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(592332444U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleNumberOfProfiles) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleNumberOfProfiles)))(this.objptr, _spd_index) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public void GetSPDModuleProfileInfos(
        int _spd_index,
        int _profile_index,
        ref float _frequency,
        ref float _tCL,
        ref float _nominal_vdd)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(430977888U);
          if (!(ptr != IntPtr.Zero))
            return;
          ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleProfileInfos) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleProfileInfos)))(this.objptr, _spd_index, _profile_index, ref _frequency, ref _tCL, ref _nominal_vdd);
        }
        catch
        {
        }
      }

      public int GetSPDModuleNumberOfEPPProfiles(int _spd_index, ref int _epp_revision)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3565136127U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleNumberOfEPPProfiles) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleNumberOfEPPProfiles)))(this.objptr, _spd_index, ref _epp_revision) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public void GetSPDModuleEPPProfileInfos(
        int _spd_index,
        int _profile_index,
        ref float _frequency,
        ref float _tCL,
        ref float _tRCD,
        ref float _tRAS,
        ref float _tRP,
        ref float _tRC,
        ref float _nominal_vdd)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1384949017U);
          if (!(ptr != IntPtr.Zero))
            return;
          ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleEPPProfileInfos) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleEPPProfileInfos)))(this.objptr, _spd_index, _profile_index, ref _frequency, ref _tCL, ref _tRCD, ref _tRAS, ref _tRP, ref _tRC, ref _nominal_vdd);
        }
        catch
        {
        }
      }

      public int GetSPDModuleNumberOfXMPProfiles(int _spd_index, ref int _xmp_revision)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1814485069U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleNumberOfXMPProfiles) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleNumberOfXMPProfiles)))(this.objptr, _spd_index, ref _xmp_revision) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetSPDModuleXMPProfileNumberOfCL(int _spd_index, int _profile_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1391240665U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleXMPProfileNumberOfCL) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleXMPProfileNumberOfCL)))(this.objptr, _spd_index, _profile_index) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public void GetSPDModuleXMPProfileCLInfos(
        int _spd_index,
        int _profile_index,
        int _cl_index,
        ref float _frequency,
        ref float _CL)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3459619947U);
          if (!(ptr != IntPtr.Zero))
            return;
          ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleXMPProfileCLInfos) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleXMPProfileCLInfos)))(this.objptr, _spd_index, _profile_index, _cl_index, ref _frequency, ref _CL);
        }
        catch
        {
        }
      }

      public void GetSPDModuleXMPProfileInfos(
        int _spd_index,
        int _profile_index,
        ref float _tRCD,
        ref float _tRAS,
        ref float _tRP,
        ref float _tRC,
        ref float _nominal_vdd,
        ref int _max_freq,
        ref float _max_CL)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(502414308U);
          if (!(ptr != IntPtr.Zero))
            return;
          ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleXMPProfileInfos) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleXMPProfileInfos)))(this.objptr, _spd_index, _profile_index, ref _tRCD, ref _tRAS, ref _tRP, ref _tRC, ref _nominal_vdd, ref _max_freq, ref _max_CL);
        }
        catch
        {
        }
      }

      public int GetSPDModuleNumberOfAMPProfiles(int _spd_index, ref int _amp_revision)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3156179006U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleNumberOfAMPProfiles) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleNumberOfAMPProfiles)))(this.objptr, _spd_index, ref _amp_revision) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public void GetSPDModuleAMPProfileInfos(
        int _spd_index,
        int _profile_index,
        ref int _frequency,
        ref float _min_cycle_time,
        ref float _tCL,
        ref float _tRCD,
        ref float _tRAS,
        ref float _tRP,
        ref float _tRC)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1126073917U);
          if (!(ptr != IntPtr.Zero))
            return;
          ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleAMPProfileInfos) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleAMPProfileInfos)))(this.objptr, _spd_index, _profile_index, ref _frequency, ref _min_cycle_time, ref _tCL, ref _tRCD, ref _tRAS, ref _tRP, ref _tRC);
        }
        catch
        {
        }
      }

      public int GetSPDModuleRawData(int _spd_index, int _offset)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1927079353U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetSPDModuleRawData) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetSPDModuleRawData)))(this.objptr, _spd_index, _offset) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetNumberOfDisplayAdapter()
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2145189817U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetNumberOfDisplayAdapter) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetNumberOfDisplayAdapter)))(this.objptr) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public string GetDisplayAdapterName(int _adapter_index)
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3828861039U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterName) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterName)))(this.objptr, _adapter_index);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetDisplayAdapterCodeName(int _adapter_index)
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1997598241U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterCodeName) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterCodeName)))(this.objptr, _adapter_index);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public int GetDisplayAdapterNumberOfPerformanceLevels(int _adapter_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1032354576U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterNumberOfPerformanceLevels) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterNumberOfPerformanceLevels)))(this.objptr, _adapter_index) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetDisplayAdapterCurrentPerformanceLevel(int _adapter_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(140644548U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterCurrentPerformanceLevel) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterCurrentPerformanceLevel)))(this.objptr, _adapter_index) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public string GetDisplayAdapterPerformanceLevelName(int _adapter_index, int _perf_level)
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3273754179U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterPerformanceLevelName) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterPerformanceLevelName)))(this.objptr, _adapter_index, _perf_level);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public float GetDisplayAdapterClock(int _adapter_index, int _perf_level, int _domain)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(561267432U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterClock) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterClock)))(this.objptr, _adapter_index, _perf_level, _domain) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public float GetDisplayAdapterStockClock(int _adapter_index, int _perf_level, int _domain)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(678842604U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterStockClock) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterStockClock)))(this.objptr, _adapter_index, _perf_level, _domain) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public float GetDisplayAdapterManufacturingProcess(int _adapter_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1050574140U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterManufacturingProcess) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterManufacturingProcess)))(this.objptr, _adapter_index) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public float GetDisplayAdapterTemperature(int _adapter_index, int _domain)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3018549206U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterTemperature) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterTemperature)))(this.objptr, _adapter_index, _domain) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public int GetDisplayAdapterFanSpeed(int _adapter_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1565833897U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterFanSpeed) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterFanSpeed)))(this.objptr, _adapter_index) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetDisplayAdapterFanPWM(int _adapter_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3092082842U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterFanPWM) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterFanPWM)))(this.objptr, _adapter_index) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public bool GetDisplayAdapterMemoryType(int _adapter_index, ref int _type)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3260515503U);
          return ptr != IntPtr.Zero && ((CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterMemoryType) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterMemoryType)))(this.objptr, _adapter_index, ref _type) == 1;
        }
        catch
        {
          return false;
        }
      }

      public bool GetDisplayAdapterMemorySize(int _adapter_index, ref int _size)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2020012237U);
          return ptr != IntPtr.Zero && ((CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterMemorySize) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterMemorySize)))(this.objptr, _adapter_index, ref _size) == 1;
        }
        catch
        {
          return false;
        }
      }

      public bool GetDisplayAdapterMemoryBusWidth(int _adapter_index, ref int _bus_width)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2830586222U);
          return ptr != IntPtr.Zero && ((CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterMemoryBusWidth) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterMemoryBusWidth)))(this.objptr, _adapter_index, ref _bus_width) == 1;
        }
        catch
        {
          return false;
        }
      }

      public string GetDisplayAdapterMemoryVendor(int _adapter_index)
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2176648058U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterMemoryVendor) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterMemoryVendor)))(this.objptr, _adapter_index);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetDisplayAdaterDriverVersion(int _adapter_index)
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2308117286U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterDriverVersion) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterDriverVersion)))(this.objptr, _adapter_index);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetDirectXVersion()
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1463070313U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetDirectXVersion) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetDirectXVersion)))(this.objptr);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public bool GetDisplayAdapterBusInfos(int _adapter_index, ref int _bus_type, ref int _multi_vpu)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1247712445U);
          return ptr != IntPtr.Zero && ((CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterBusInfos) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterBusInfos)))(this.objptr, _adapter_index, ref _bus_type, ref _multi_vpu) == 1;
        }
        catch
        {
          return false;
        }
      }

      public string GetDisplayAdapterCoreFamily(int _adapter_index, ref int _core)
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2291994938U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterCoreFamily) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterCoreFamily)))(this.objptr, _adapter_index, ref _core);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public bool GetDisplayAdapterPCIAddress(
        int _adapter_index,
        ref int _bus,
        ref int _device,
        ref int _function)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3712334475U);
          return ptr != IntPtr.Zero && ((CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterPCIAddress) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetDisplayAdapterPCIAddress)))(this.objptr, _adapter_index, ref _bus, ref _device, ref _function) == 1;
        }
        catch
        {
          return false;
        }
      }

      public int GetNumberOfMonitors()
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(365964192U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetNumberOfMonitors) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetNumberOfMonitors)))(this.objptr) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public string GetMonitorName(int _monitor_index)
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3507724839U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetMonitorName) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetMonitorName)))(this.objptr, _monitor_index);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetMonitorVendor(int _monitor_index)
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1900733077U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetMonitorVendor) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetMonitorVendor)))(this.objptr, _monitor_index);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetMonitorID(int _monitor_index)
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2025910657U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetMonitorID) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetMonitorID)))(this.objptr, _monitor_index);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetMonitorSerial(int _monitor_index)
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(672550956U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetMonitorSerial) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetMonitorSerial)))(this.objptr, _monitor_index);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public bool GetMonitorManufacturingDate(int _monitor_index, ref int _week, ref int _year)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(4213044795U);
          return ptr != IntPtr.Zero && ((CPUIDSDK.CPUIDSDK_fp_GetMonitorManufacturingDate) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetMonitorManufacturingDate)))(this.objptr, _monitor_index, ref _week, ref _year) == 1;
        }
        catch
        {
          return false;
        }
      }

      public float GetMonitorSize(int _monitor_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2811580202U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetMonitorSize) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetMonitorSize)))(this.objptr, _monitor_index) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public bool GetMonitorResolution(
        int _monitor_index,
        ref int _width,
        ref int _height,
        ref int _frequency)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(328476456U);
          return ptr != IntPtr.Zero && ((CPUIDSDK.CPUIDSDK_fp_GetMonitorResolution) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetMonitorResolution)))(this.objptr, _monitor_index, ref _width, ref _height, ref _frequency) == 1;
        }
        catch
        {
          return false;
        }
      }

      public int GetMonitorMaxPixelClock(int _monitor_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1398318769U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetMonitorMaxPixelClock) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetMonitorMaxPixelClock)))(this.objptr, _monitor_index) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public float GetMonitorGamma(int _monitor_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1318493485U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetMonitorGamma) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetMonitorGamma)))(this.objptr, _monitor_index) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public int GetNumberOfStorageDevice()
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2639215262U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetNumberOfStorageDevice) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetNumberOfStorageDevice)))(this.objptr) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetStorageDriveNumber(int _index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2088696061U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetStorageDriveNumber) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetStorageDriveNumber)))(this.objptr, _index) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public string GetStorageDeviceName(int _index)
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1731382885U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetStorageDeviceName) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetStorageDeviceName)))(this.objptr, _index);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetStorageDeviceRevision(int _index)
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3784295199U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetStorageDeviceRevision) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetStorageDeviceRevision)))(this.objptr, _index);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetStorageDeviceSerialNumber(int _index)
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(4004896107U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetStorageDeviceSerialNumber) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetStorageDeviceSerialNumber)))(this.objptr, _index);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public int GetStorageDeviceBusType(int _index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(4025999343U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetStorageDeviceBusType) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetStorageDeviceBusType)))(this.objptr, _index) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetStorageDeviceRotationSpeed(int _index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2073753397U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetStorageDeviceRotationSpeed) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetStorageDeviceRotationSpeed)))(this.objptr, _index) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public uint GetStorageDeviceFeatureFlag(int _index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(508312728U);
          return ptr != IntPtr.Zero ? (uint) ((CPUIDSDK.CPUIDSDK_fp_GetStorageDeviceFeatureFlag) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetStorageDeviceFeatureFlag)))(this.objptr, _index) : (uint) CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return (uint) CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetStorageDeviceNumberOfVolumes(int _index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2798210450U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetStorageDeviceNumberOfVolumes) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetStorageDeviceNumberOfVolumes)))(this.objptr, _index) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public string GetStorageDeviceVolumeLetter(int _index, int _volume_index)
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3096670502U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetStorageDeviceVolumeLetter) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetStorageDeviceVolumeLetter)))(this.objptr, _index, _volume_index);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public float GetStorageDeviceVolumeTotalCapacity(int _index, int _volume_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3791242227U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetStorageDeviceVolumeTotalCapacity) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetStorageDeviceVolumeTotalCapacity)))(this.objptr, _index, _volume_index) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public float GetStorageDeviceVolumeAvailableCapacity(int _index, int _volume_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(4189057887U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetStorageDeviceVolumeAvailableCapacity) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetStorageDeviceVolumeAvailableCapacity)))(this.objptr, _index, _volume_index) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public bool GetStorageDeviceSmartAttribute(
        int _hdd_index,
        int _attrib_index,
        ref int _id,
        ref int _flags,
        ref int _value,
        ref int _worst,
        byte[] _data)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3390018591U);
          return ptr != IntPtr.Zero && ((CPUIDSDK.CPUIDSDK_fp_GetStorageDeviceSmartAttribute) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetStorageDeviceSmartAttribute)))(this.objptr, _hdd_index, _attrib_index, ref _id, ref _flags, ref _value, ref _worst, _data) == 1;
        }
        catch
        {
          return false;
        }
      }

      public int GetStorageDevicePowerOnHours(int _index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(822764052U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetStorageDevicePowerOnHours) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetStorageDevicePowerOnHours)))(this.objptr, _index) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetStorageDevicePowerCycleCount(int _index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2752333850U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetStorageDevicePowerCycleCount) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetStorageDevicePowerCycleCount)))(this.objptr, _index) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public float GetStorageDeviceTotalCapacity(int _index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(4169265411U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetStorageDeviceTotalCapacity) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetStorageDeviceTotalCapacity)))(this.objptr, _index) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public int GetNumberOfDevices()
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3867266307U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetNumberOfDevices) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetNumberOfDevices)))(this.objptr) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetDeviceClass(int _device_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2393054534U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetDeviceClass) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetDeviceClass)))(this.objptr, _device_index) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public string GetDeviceName(int _device_index)
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3510739587U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetDeviceName) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetDeviceName)))(this.objptr, _device_index);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public string GetDeviceSerialNumber(int _device_index)
      {
        try
        {
          IntPtr ptr1 = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2305233614U);
          if (!(ptr1 != IntPtr.Zero))
            return (string) null;
          IntPtr ptr2 = ((CPUIDSDK.CPUIDSDK_fp_GetDeviceSerialNumber) Marshal.GetDelegateForFunctionPointer(ptr1, typeof (CPUIDSDK.CPUIDSDK_fp_GetDeviceSerialNumber)))(this.objptr, _device_index);
          string stringAnsi = Marshal.PtrToStringAnsi(ptr2);
          Marshal.FreeBSTR(ptr2);
          return stringAnsi;
        }
        catch
        {
          return (string) null;
        }
      }

      public int GetDeviceIndexNumber(int _device_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(1096057513U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetDeviceIndexNumber) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetDeviceIndexNumber)))(this.objptr, _device_index) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public int GetNumberOfSensors(int _device_index, int _sensor_class)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(4190106495U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetNumberOfSensors) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetNumberOfSensors)))(this.objptr, _device_index, _sensor_class) : CPUIDSDK.I_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.I_UNDEFINED_VALUE;
        }
      }

      public bool GetSensorInfos(
        int _device_index,
        int _sensor_index,
        int _sensor_class,
        ref int _sensor_id,
        ref string _szName,
        ref int _raw_value,
        ref float _value,
        ref float _min_value,
        ref float _max_value)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(952660368U);
          if (!(ptr != IntPtr.Zero))
            return false;
          IntPtr zero = IntPtr.Zero;
          if (((CPUIDSDK.CPUIDSDK_fp_GetSensorInfos) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetSensorInfos)))(this.objptr, _device_index, _sensor_index, _sensor_class, ref _sensor_id, ref zero, ref _raw_value, ref _value, ref _min_value, ref _max_value) != 1)
            return false;
          _szName = Marshal.PtrToStringAnsi(zero);
          Marshal.FreeBSTR(zero);
          return true;
        }
        catch
        {
          return false;
        }
      }

      public void SensorClearMinMax(
        int _device_index,
        int _sensor_index,
        int _sensor_class,
        ref int _sensor_id,
        ref string _szName,
        ref int _raw_value,
        ref float _value,
        ref float _min_value,
        ref float _max_value)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3440089623U);
          if (!(ptr != IntPtr.Zero))
            return;
          IntPtr zero = IntPtr.Zero;
          ((CPUIDSDK.CPUIDSDK_fp_SensorClearMinMax) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_SensorClearMinMax)))(this.objptr, _device_index, _sensor_index, _sensor_class);
        }
        catch
        {
        }
      }

      public float GetSensorTypeValue(int _sensor_type, ref int _device_index, ref int _sensor_index)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(3384906627U);
          return ptr != IntPtr.Zero ? ((CPUIDSDK.CPUIDSDK_fp_GetSensorTypeValue) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetSensorTypeValue)))(this.objptr, _sensor_type, ref _device_index, ref _sensor_index) : CPUIDSDK.F_UNDEFINED_VALUE;
        }
        catch
        {
          return CPUIDSDK.F_UNDEFINED_VALUE;
        }
      }

      public bool GetSensorExtendedInfos(
        int _device_index,
        int _sensor_index,
        int _sensor_class,
        ref string _szUnit,
        ref int _flag)
      {
        try
        {
          IntPtr ptr = CPUIDSDK.CPUIDSDK_fp_QueryInterface(2592158978U);
          if (!(ptr != IntPtr.Zero))
            return false;
          IntPtr zero = IntPtr.Zero;
          if (((CPUIDSDK.CPUIDSDK_fp_GetSensorExtendedInfos) Marshal.GetDelegateForFunctionPointer(ptr, typeof (CPUIDSDK.CPUIDSDK_fp_GetSensorExtendedInfos)))(this.objptr, _device_index, _sensor_index, _sensor_class, ref zero, ref _flag) != 1)
            return false;
          _szUnit = Marshal.PtrToStringAnsi(zero);
          Marshal.FreeBSTR(zero);
          return true;
        }
        catch
        {
          return false;
        }
      }

      private enum PTR : uint
      {
        PTR191 = 26215200, // 0x01900320
        PTR113 = 30933936, // 0x01D803B0
        PTR230 = 34472988, // 0x020E041C
        PTR352 = 63834012, // 0x03CE079C
        PTR10 = 69863508, // 0x042A0854
        PTR16 = 113249664, // 0x06C00D80
        PTR95 = 114822576, // 0x06D80DB0
        PTR130 = 117444096, // 0x07000E00
        PTR76 = 137498724, // 0x08321064
        PTR145 = 140644548, // 0x086210C4
        PTR199 = 151786008, // 0x090C1218
        PTR336 = 152048160, // 0x09101220
        PTR243 = 168039432, // 0x0A041408
        PTR259 = 183768552, // 0x0AF415E8
        PTR351 = 188749440, // 0x0B401680
        PTR296 = 197138304, // 0x0BC01780
        PTR341 = 222304896, // 0x0D401A80
        PTR102 = 268967952, // 0x10082010
        PTR107 = 275914980, // 0x107220E4
        PTR221 = 278798652, // 0x109E213C
        PTR9 = 284959224, // 0x10FC21F8
        PTR253 = 303178788, // 0x12122424
        PTR265 = 305800308, // 0x123A2474
        PTR264 = 305931384, // 0x123C2478
        PTR167 = 328476456, // 0x13942728
        PTR333 = 348400008, // 0x14C42988
        PTR160 = 365964192, // 0x15D02BA0
        PTR45 = 398471040, // 0x17C02F80
        PTR117 = 401616864, // 0x17F02FE0
        PTR315 = 405155916, // 0x1826304C
        PTR380 = 408432816, // 0x185830B0
        PTR89 = 422720100, // 0x19323264
        PTR120 = 430977888, // 0x19B03360
        PTR325 = 454309416, // 0x1B143628
        PTR310 = 455489100, // 0x1B26364C
        PTR98 = 462305052, // 0x1B8E371C
        PTR307 = 469514232, // 0x1BFC37F8
        PTR34 = 470562840, // 0x1C0C3818
        PTR115 = 478951704, // 0x1C8C3918
        PTR247 = 485505504, // 0x1CF039E0
        PTR78 = 497826648, // 0x1DAC3B58
        PTR126 = 502414308, // 0x1DF23BE4
        PTR182 = 508312728, // 0x1E4C3C98
        PTR22 = 511982856, // 0x1E843D08
        PTR49 = 531775332, // 0x1FB23F64
        PTR381 = 538984512, // 0x20204040
        PTR147 = 561267432, // 0x217442E8
        PTR308 = 563888952, // 0x219C4338
        PTR119 = 592332444, // 0x234E469C
        PTR270 = 596133648, // 0x23884710
        PTR42 = 615795048, // 0x24B44968
        PTR206 = 616188276, // 0x24BA4974
        PTR5 = 627198660, // 0x25624AC4
        PTR192 = 627854040, // 0x256C4AD8
        PTR64 = 630868788, // 0x259A4B34
        PTR96 = 663506712, // 0x278C4F18
        PTR164 = 672550956, // 0x2816502C
        PTR148 = 678842604, // 0x287650EC
        PTR232 = 682905960, // 0x28B45168
        PTR231 = 688935456, // 0x29105220
        PTR212 = 706761792, // 0x2A205440
        PTR297 = 710431920, // 0x2A5854B0
        PTR85 = 749754720, // 0x2CB05960
        PTR188 = 822764052, // 0x310A6214
        PTR237 = 830628612, // 0x31826304
        PTR347 = 835740576, // 0x31D063A0
        PTR2 = 837051336, // 0x31E463C8
        PTR70 = 869558184, // 0x33D467A8
        PTR173 = 873359388, // 0x340E681C
        PTR31 = 898919208, // 0x35946B28
        PTR51 = 906783768, // 0x360C6C18
        PTR74 = 916221240, // 0x369C6D38
        PTR277 = 921857508, // 0x36F26DE4
        PTR282 = 930115296, // 0x37706EE0
        PTR59 = 932474664, // 0x37946F28
        PTR209 = 952660368, // 0x38C87190
        PTR252 = 976254048, // 0x3A307460
        PTR327 = 987133356, // 0x3AD675AC
        PTR195 = 991852092, // 0x3B1E763C
        PTR11 = 994997916, // 0x3B4E769C
        PTR35 = 997095132, // 0x3B6E76DC
        PTR370 = 1031043816, // 0x3D747AE8
        PTR144 = 1032354576, // 0x3D887B10
        PTR159 = 1050574140, // 0x3E9E7D3C
        PTR3 = 1074429973, // 0x400A8015
        PTR350 = 1075740733, // 0x401E803D
        PTR326 = 1081508077, // 0x407680ED
        PTR215 = 1096057513, // 0x415482A9
        PTR248 = 1098023653, // 0x417282E5
        PTR372 = 1107592201, // 0x42048409
        PTR29 = 1111524481, // 0x42408481
        PTR135 = 1112048785, // 0x42488491
        PTR375 = 1113883849, // 0x426484C9
        PTR354 = 1122928093, // 0x42EE85DD
        PTR128 = 1126073917, // 0x431E863D
        PTR225 = 1171557289, // 0x45D48BA9
        PTR132 = 1202097997, // 0x47A68F4D
        PTR280 = 1244959849, // 0x4A349469
        PTR157 = 1247712445, // 0x4A5E94BD
        PTR287 = 1264228021, // 0x4B5A96B5
        PTR79 = 1265276629, // 0x4B6A96D5
        PTR262 = 1283365117, // 0x4C7E98FD
        PTR229 = 1296865945, // 0x4D4C9A99
        PTR111 = 1297783477, // 0x4D5A9AB5
        PTR169 = 1318493485, // 0x4E969D2D
        PTR8 = 1327930957, // 0x4F269E4D
        PTR50 = 1339858873, // 0x4FDC9FB9
        PTR14 = 1357685209, // 0x50ECA1D9
        PTR7 = 1369482049, // 0x51A0A341
        PTR122 = 1384949017, // 0x528CA519
        PTR273 = 1386390853, // 0x52A2A545
        PTR124 = 1391240665, // 0x52ECA5D9
        PTR168 = 1398318769, // 0x5358A6B1
        PTR305 = 1399236301, // 0x5366A6CD
        PTR217 = 1419159853, // 0x5496A92D
        PTR222 = 1426106881, // 0x5500AA01
        PTR379 = 1452584233, // 0x5694AD29
        PTR156 = 1463070313, // 0x5734AE69
        PTR207 = 1475260381, // 0x57EEAFDD
        PTR214 = 1478144053, // 0x581AB035
        PTR271 = 1492431337, // 0x58F4B1E9
        PTR112 = 1530705529, // 0x5B3CB679
        PTR46 = 1544599585, // 0x5C10B821
        PTR304 = 1546565725, // 0x5C2EB85D
        PTR376 = 1563736681, // 0x5D34BA69
        PTR150 = 1565833897, // 0x5D54BAA9
        PTR68 = 1583922385, // 0x5E68BCD1
        PTR314 = 1608302521, // 0x5FDCBFB9
        PTR241 = 1633862341, // 0x6162C2C5
        PTR316 = 1637925697, // 0x61A0C341
        PTR254 = 1659684313, // 0x62ECC5D9
        PTR294 = 1669121785, // 0x637CC6F9
        PTR285 = 1692453313, // 0x64E0C9C1
        PTR65 = 1727319529, // 0x66F4CDE9
        PTR177 = 1731382885, // 0x6732CE65
        PTR110 = 1741606813, // 0x67CECF9D
        PTR353 = 1745014789, // 0x6802D005
        PTR39 = 1757204857, // 0x68BCD179
        PTR364 = 1778045941, // 0x69FAD3F5
        PTR223 = 1780798537, // 0x6A24D449
        PTR343 = 1808193421, // 0x6BC6D78D
        PTR123 = 1814485069, // 0x6C26D84D
        PTR279 = 1815926905, // 0x6C3CD879
        PTR92 = 1816451209, // 0x6C44D889
        PTR289 = 1823529313, // 0x6CB0D961
        PTR313 = 1828116973, // 0x6CF6D9ED
        PTR323 = 1842535333, // 0x6DD2DBA5
        PTR224 = 1850268817, // 0x6E48DC91
        PTR12 = 1860099517, // 0x6EDEDDBD
        PTR20 = 1860754897, // 0x6EE8DDD1
        PTR378 = 1873207117, // 0x6FA6DF4D
        PTR369 = 1874780029, // 0x6FBEDF7D
        PTR158 = 1887625477, // 0x7082E105
        PTR162 = 1900733077, // 0x714AE295
        PTR359 = 1905058585, // 0x718CE319
        PTR131 = 1905320737, // 0x7190E321
        PTR129 = 1927079353, // 0x72DCE5B9
        PTR335 = 1948444741, // 0x7422E845
        PTR276 = 1960503733, // 0x74DAE9B5
        PTR290 = 1970989813, // 0x757AEAF5
        PTR18 = 1975315321, // 0x75BCEB79
        PTR133 = 1976757157, // 0x75D2EBA5
        PTR88 = 1994714569, // 0x76E4EDC9
        PTR143 = 1997598241, // 0x7710EE21
        PTR134 = 2007822169, // 0x77ACEF59
        PTR266 = 2009132929, // 0x77C0EF81
        PTR48 = 2013851665, // 0x7808F011
        PTR153 = 2020012237, // 0x7866F0CD
        PTR141 = 2024730973, // 0x78AEF15D
        PTR163 = 2025910657, // 0x78C0F181
        PTR356 = 2028663253, // 0x78EAF1D5
        PTR118 = 2030105089, // 0x7900F201
        PTR291 = 2037707497, // 0x7974F2E9
        PTR235 = 2045178829, // 0x79E6F3CD
        PTR234 = 2058679657, // 0x7AB4F569
        PTR278 = 2058941809, // 0x7AB8F571
        PTR181 = 2073753397, // 0x7B9AF735
        PTR176 = 2088696061, // 0x7C7EF8FD
        PTR54 = 2097740305, // 0x7D08FA11
        PTR106 = 2125659493, // 0x7EB2FD65
        PTR116 = 2136014497, // 0x7F50FEA1
        PTR103 = 2138636017, // 0x7F78FEF1
        PTR140 = 2145189817, // 0x7FDCFFB9
        PTR71 = 2149908554, // 0x8025004A
        PTR171 = 2176648058, // 0x81BD037A
        PTR373 = 2179531730, // 0x81E903D2
        PTR244 = 2179793882, // 0x81ED03DA
        PTR155 = 2187658442, // 0x826504CA
        PTR81 = 2199062054, // 0x83130626
        PTR345 = 2202470030, // 0x8347068E
        PTR6 = 2208892754, // 0x83A90752
        PTR82 = 2212038578, // 0x83D907B2
        PTR332 = 2221082822, // 0x846308C6
        PTR25 = 2224752950, // 0x849B0936
        PTR348 = 2272071386, // 0x876D0EDA
        PTR4 = 2290159874, // 0x88811102
        PTR170 = 2291994938, // 0x889D113A
        PTR274 = 2296320446, // 0x88DF11BE
        PTR203 = 2305233614, // 0x896712CE
        PTR295 = 2306020070, // 0x897312E6
        PTR172 = 2308117286, // 0x89931326
        PTR383 = 2352289898, // 0x8C35186A
        PTR338 = 2362382750, // 0x8CCF199E
        PTR57 = 2364742118, // 0x8CF319E6
        PTR268 = 2372213450, // 0x8D651ACA
        PTR100 = 2380078010, // 0x8DDD1BBA
        PTR38 = 2382699530, // 0x8E051C0A
        PTR267 = 2387680418, // 0x8E511CA2
        PTR275 = 2390039786, // 0x8E751CEA
        PTR201 = 2393054534, // 0x8EA31D46
        PTR256 = 2404195994, // 0x8F4D1E9A
        PTR94 = 2416779290, // 0x900D201A
        PTR32 = 2419662962, // 0x90392072
        PTR300 = 2423464166, // 0x907320E6
        PTR66 = 2429886890, // 0x90D521AA
        PTR238 = 2436309614, // 0x9137226E
        PTR193 = 2447975378, // 0x91E923D2
        PTR361 = 2469865070, // 0x9337266E
        PTR337 = 2475763490, // 0x93912722
        PTR216 = 2479302542, // 0x93C7278E
        PTR233 = 2488084634, // 0x944D289A
        PTR328 = 2517576734, // 0x960F2C1E
        PTR329 = 2540646110, // 0x976F2EDE
        PTR58 = 2558079218, // 0x987930F2
        PTR228 = 2567385614, // 0x9907320E
        PTR137 = 2584163342, // 0x9A07340E
        PTR358 = 2589537458, // 0x9A5934B2
        PTR213 = 2592158978, // 0x9A813502
        PTR218 = 2605659806, // 0x9B4F369E
        PTR69 = 2627287346, // 0x9C993932
        PTR36 = 2629384562, // 0x9CB93972
        PTR175 = 2639215262, // 0x9D4F3A9E
        PTR63 = 2645244758, // 0x9DAB3B56
        PTR21 = 2673163946, // 0x9F553EAA
        PTR67 = 2684960786, // 0xA0094012
        PTR0 = 2707899086, // 0xA16742CE
        PTR220 = 2708423390, // 0xA16F42DE
        PTR194 = 2709471998, // 0xA17F42FE
        PTR33 = 2711831366, // 0xA1A34346
        PTR303 = 2721268838, // 0xA2334466
        PTR302 = 2734900742, // 0xA3034606
        PTR30 = 2736080426, // 0xA315462A
        PTR363 = 2742765302, // 0xA37B46F6
        PTR368 = 2744993594, // 0xA39D473A
        PTR189 = 2752333850, // 0xA40D481A
        PTR320 = 2755479674, // 0xA43D487A
        PTR258 = 2757183662, // 0xA45748AE
        PTR334 = 2770553414, // 0xA5234A46
        PTR183 = 2798210450, // 0xA6C94D92
        PTR166 = 2811580202, // 0xA7954F2A
        PTR154 = 2830586222, // 0xA8B7516E
        PTR239 = 2833338818, // 0xA8E151C2
        PTR60 = 2840285846, // 0xA94B5296
        PTR84 = 2865714590, // 0xAACF559E
        PTR324 = 2867418578, // 0xAAE955D2
        PTR204 = 2886162446, // 0xAC07580E
        PTR13 = 2891536562, // 0xAC5958B2
        PTR355 = 2894420234, // 0xAC85590A
        PTR344 = 2913164102, // 0xADA35B46
        PTR83 = 2928631070, // 0xAE8F5D1E
        PTR261 = 2937151010, // 0xAF115E22
        PTR91 = 2945539874, // 0xAF915F22
        PTR55 = 2950258610, // 0xAFD95FB2
        PTR77 = 2975556278, // 0xB15B62B6
        PTR360 = 2976998114, // 0xB17162E2
        PTR149 = 3018549206, // 0xB3EB67D6
        PTR246 = 3031394654, // 0xB4AF695E
        PTR226 = 3032705414, // 0xB4C36986
        PTR269 = 3038079530, // 0xB5156A2A
        PTR108 = 3057216626, // 0xB6396C72
        PTR138 = 3060886754, // 0xB6716CE2
        PTR236 = 3067047326, // 0xB6CF6D9E
        PTR251 = 3079630622, // 0xB78F6F1E
        PTR311 = 3086708726, // 0xB7FB6FF6
        PTR286 = 3087757334, // 0xB80B7016
        PTR205 = 3090772082, // 0xB8397072
        PTR151 = 3092082842, // 0xB84D709A
        PTR184 = 3096670502, // 0xB8937126
        PTR75 = 3110302406, // 0xB96372C6
        PTR101 = 3119215574, // 0xB9EB73D6
        PTR339 = 3146741534, // 0xBB8F771E
        PTR127 = 3156179006, // 0xBC1F783E
        PTR99 = 3164305718, // 0xBC9B7936
        PTR288 = 3165747554, // 0xBCB17962
        PTR87 = 3219488714, // 0xBFE57FCA
        PTR281 = 3235742139, // 0xC0DD81BB
        PTR299 = 3248587587, // 0xC1A18343
        PTR73 = 3259466895, // 0xC247848F
        PTR152 = 3260515503, // 0xC25784AF
        PTR97 = 3268248987, // 0xC2CD859B
        PTR362 = 3272443419, // 0xC30D861B
        PTR146 = 3273754179, // 0xC3218643
        PTR318 = 3279390447, // 0xC37786EF
        PTR242 = 3279652599, // 0xC37B86F7
        PTR53 = 3296037099, // 0xC47588EB
        PTR72 = 3313470207, // 0xC57F8AFF
        PTR293 = 3321859071, // 0xC5FF8BFF
        PTR272 = 3323431983, // 0xC6178C2F
        PTR47 = 3323563059, // 0xC6198C33
        PTR24 = 3379663587, // 0xC97192E3
        PTR211 = 3384906627, // 0xC9C19383
        PTR306 = 3385168779, // 0xC9C5938B
        PTR187 = 3390018591, // 0xCA0F941F
        PTR86 = 3403912647, // 0xCAE395C7
        PTR301 = 3408238155, // 0xCB25964B
        PTR104 = 3410859675, // 0xCB4D969B
        PTR340 = 3412563663, // 0xCB6796CF
        PTR210 = 3440089623, // 0xCD0B9A17
        PTR374 = 3444939435, // 0xCD559AAB
        PTR125 = 3459619947, // 0xCE359C6B
        PTR1 = 3488849895, // 0xCFF39FE7
        PTR366 = 3494486163, // 0xD049A093
        PTR161 = 3507724839, // 0xD113A227
        PTR342 = 3509297751, // 0xD12BA257
        PTR202 = 3510739587, // 0xD141A283
        PTR219 = 3535906179, // 0xD2C1A583
        PTR257 = 3538658775, // 0xD2EBA5D7
        PTR227 = 3558844479, // 0xD41FA83F
        PTR121 = 3565136127, // 0xD47FA8FF
        PTR28 = 3575622207, // 0xD51FAA3F
        PTR284 = 3619794819, // 0xD7C1AF83
        PTR56 = 3629625519, // 0xD857B0AF
        PTR196 = 3657151479, // 0xD9FBB3F7
        PTR40 = 3696605355, // 0xDC55B8AB
        PTR15 = 3707746815, // 0xDCFFB9FF
        PTR17 = 3709844031, // 0xDD1FBA3F
        PTR174 = 3712334475, // 0xDD45BA8B
        PTR365 = 3734879547, // 0xDE9DBD3B
        PTR198 = 3736321383, // 0xDEB3BD67
        PTR263 = 3738418599, // 0xDED3BDA7
        PTR260 = 3739729359, // 0xDEE7BDCF
        PTR298 = 3753623415, // 0xDFBBBF77
        PTR109 = 3779969691, // 0xE14DC29B
        PTR330 = 3783115515, // 0xE17DC2FB
        PTR178 = 3784295199, // 0xE18FC31F
        PTR185 = 3791242227, // 0xE1F9C3F3
        PTR322 = 3825846291, // 0xE409C813
        PTR142 = 3828861039, // 0xE437C86F
        PTR80 = 3843279399, // 0xE513CA27
        PTR309 = 3843934779, // 0xE51DCA3B
        PTR43 = 3856386999, // 0xE5DBCBB7
        PTR200 = 3867266307, // 0xE681CD03
        PTR93 = 3887058783, // 0xE7AFCF5F
        PTR349 = 3894005811, // 0xE819D033
        PTR331 = 3897282711, // 0xE84BD097
        PTR44 = 3897544863, // 0xE84FD09F
        PTR321 = 3948926655, // 0xEB5FD6BF
        PTR139 = 3967670523, // 0xEC7DD8FB
        PTR371 = 3994934331, // 0xEE1DDC3B
        PTR179 = 4004896107, // 0xEEB5DD6B
        PTR19 = 4006862247, // 0xEED3DDA7
        PTR382 = 4007124399, // 0xEED7DDAF
        PTR27 = 4016561871, // 0xEF67DECF
        PTR180 = 4025999343, // 0xEFF7DFEF
        PTR37 = 4063487079, // 0xF233E467
        PTR317 = 4068730119, // 0xF283E507
        PTR312 = 4073842083, // 0xF2D1E5A3
        PTR52 = 4077381135, // 0xF307E60F
        PTR90 = 4087998291, // 0xF3A9E753
        PTR249 = 4093634559, // 0xF3FFE7FF
        PTR346 = 4096125003, // 0xF425E84B
        PTR197 = 4107397539, // 0xF4D1E9A3
        PTR377 = 4110674439, // 0xF503EA07
        PTR26 = 4111460895, // 0xF50FEA1F
        PTR61 = 4123650963, // 0xF5C9EB93
        PTR240 = 4132433055, // 0xF64FEC9F
        PTR136 = 4134923499, // 0xF675ECEB
        PTR23 = 4141739451, // 0xF6DDEDBB
        PTR283 = 4144360971, // 0xF705EE0B
        PTR190 = 4169265411, // 0xF881F103
        PTR357 = 4171624779, // 0xF8A5F14B
        PTR255 = 4188271431, // 0xF9A3F347
        PTR186 = 4189057887, // 0xF9AFF35F
        PTR208 = 4190106495, // 0xF9BFF37F
        PTR319 = 4194694155, // 0xFA05F40B
        PTR41 = 4203345171, // 0xFA89F513
        PTR245 = 4205835615, // 0xFAAFF55F
        PTR165 = 4213044795, // 0xFB1DF63B
        PTR367 = 4239653223, // 0xFCB3F967
        PTR292 = 4247648859, // 0xFD2DFA5B
        PTR114 = 4253547279, // 0xFD87FB0F
        PTR250 = 4267703487, // 0xFE5FFCBF
        PTR105 = 4275568047, // 0xFED7FDAF
        PTR62 = 4290510711, // 0xFFBBFF77
      }

      private delegate IntPtr CPUIDSDK_fp_CreateInstance();

      private delegate void CPUIDSDK_fp_DestroyInstance(IntPtr objptr);

      private delegate int CPUIDSDK_fp_Init(
        IntPtr objptr,
        string _szDllPath,
        string _szDllFilename,
        int _config_flag,
        ref int _errorcode,
        ref int _extended_errorcode);

      private delegate int CPUIDSDK_fp_Init2(
        IntPtr objptr,
        int _config_flag,
        ref int _errorcode,
        ref int _extended_errorcode);

      private delegate void CPUIDSDK_fp_Close(IntPtr objptr);

      private delegate void CPUIDSDK_fp_RefreshInformation(IntPtr objptr);

      private delegate void CPUIDSDK_fp_GetDllVersion(IntPtr objptr, ref int _version);

      private delegate int CPUIDSDK_fp_GetNbProcessors(IntPtr objptr);

      private delegate int CPUIDSDK_fp_GetProcessorFamily(IntPtr objptr, int _proc_index);

      private delegate int CPUIDSDK_fp_GetProcessorCoreSetCount(IntPtr objptr, int _proc_index);

      private delegate int CPUIDSDK_fp_GetProcessorCoreCount(
        IntPtr objptr,
        int _proc_index,
        int _core_set);

      private delegate int CPUIDSDK_fp_GetProcessorThreadCount(
        IntPtr objptr,
        int _proc_index,
        int _core_set);

      private delegate int CPUIDSDK_fp_GetProcessorCoreSetType(
        IntPtr objptr,
        int _proc_index,
        int _core_set);

      private delegate int CPUIDSDK_fp_GetProcessorCoreThreadCount(
        IntPtr objptr,
        int _proc_index,
        int _core_index);

      private delegate int CPUIDSDK_fp_GetProcessorThreadAPICID(
        IntPtr objptr,
        int _proc_index,
        int _core_index,
        int _thread_index);

      private delegate IntPtr CPUIDSDK_fp_GetProcessorName(IntPtr objptr, int _proc_index);

      private delegate IntPtr CPUIDSDK_fp_GetProcessorCodeName(IntPtr objptr, int _proc_index);

      private delegate IntPtr CPUIDSDK_fp_GetProcessorSpecification(IntPtr objptr, int _proc_index);

      private delegate IntPtr CPUIDSDK_fp_GetProcessorPackage(IntPtr objptr, int _proc_index);

      private delegate IntPtr CPUIDSDK_fp_GetProcessorStepping(IntPtr objptr, int _proc_index);

      private delegate float CPUIDSDK_fp_GetProcessorTDP(IntPtr objptr, int _proc_index);

      private delegate float CPUIDSDK_fp_GetProcessorTjmax(IntPtr objptr, int _proc_index);

      private delegate float CPUIDSDK_fp_GetProcessorManufacturingProcess(
        IntPtr objptr,
        int _proc_index);

      private delegate int CPUIDSDK_fp_IsProcessorInstructionSetAvailable(
        IntPtr objptr,
        int _proc_index,
        int _iset);

      private delegate float CPUIDSDK_fp_GetProcessorCoreClockFrequency(
        IntPtr objptr,
        int _proc_index,
        int _core_index);

      private delegate float CPUIDSDK_fp_GetProcessorCoreClockMultiplier(
        IntPtr objptr,
        int _proc_index,
        int _core_index);

      private delegate float CPUIDSDK_fp_GetProcessorCoreTemperature(
        IntPtr objptr,
        int _proc_index,
        int _core_index);

      private delegate float CPUIDSDK_fp_GetBusFrequency(IntPtr objptr);

      private delegate float CPUIDSDK_fp_GetProcessorRatedBusFrequency(IntPtr objptr, int _proc_index);

      private delegate int CPUIDSDK_fp_GetProcessorStockClockFrequency(IntPtr objptr, int _proc_index);

      private delegate int CPUIDSDK_fp_GetProcessorStockBusFrequency(IntPtr objptr, int _proc_index);

      private delegate int CPUIDSDK_fp_GetProcessorMaxCacheLevel(IntPtr objptr, int _proc_index);

      private delegate void CPUIDSDK_fp_GetProcessorCacheParameters(
        IntPtr objptr,
        int _proc_index,
        int _core_set,
        int _cache_level,
        int _cache_type,
        ref int _NbCaches,
        ref int _size);

      private delegate int CPUIDSDK_fp_GetProcessorID(IntPtr objptr, int _proc_index);

      private delegate float CPUIDSDK_fp_GetProcessorVoltageID(IntPtr objptr, int _proc_index);

      private delegate int CPUIDSDK_fp_GetMemoryType(IntPtr objptr);

      private delegate int CPUIDSDK_fp_GetMemorySize(IntPtr objptr);

      private delegate float CPUIDSDK_fp_GetMemoryClockFrequency(IntPtr objptr);

      private delegate int CPUIDSDK_fp_GetMemoryNumberOfChannels(IntPtr objptr);

      private delegate float CPUIDSDK_fp_GetMemoryCASLatency(IntPtr objptr);

      private delegate int CPUIDSDK_fp_GetMemoryRAStoCASDelay(IntPtr objptr);

      private delegate int CPUIDSDK_fp_GetMemoryRASPrecharge(IntPtr objptr);

      private delegate int CPUIDSDK_fp_GetMemoryTRAS(IntPtr objptr);

      private delegate int CPUIDSDK_fp_GetMemoryTRC(IntPtr objptr);

      private delegate int CPUIDSDK_fp_GetMemoryCommandRate(IntPtr objptr);

      private delegate IntPtr CPUIDSDK_fp_GetNorthBridgeVendor(IntPtr objptr);

      private delegate IntPtr CPUIDSDK_fp_GetNorthBridgeModel(IntPtr objptr);

      private delegate IntPtr CPUIDSDK_fp_GetNorthBridgeRevision(IntPtr objptr);

      private delegate IntPtr CPUIDSDK_fp_GetSouthBridgeVendor(IntPtr objptr);

      private delegate IntPtr CPUIDSDK_fp_GetSouthBridgeModel(IntPtr objptr);

      private delegate IntPtr CPUIDSDK_fp_GetSouthBridgeRevision(IntPtr objptr);

      private delegate void CPUIDSDK_fp_GetGraphicBusLinkParameters(
        IntPtr objptr,
        ref int _bus_type,
        ref int _link_width);

      private delegate void CPUIDSDK_fp_GetMemorySlotsConfig(
        IntPtr objptr,
        ref int _nbslots,
        ref int _nbusedslots,
        ref int _slotmap_h,
        ref int _slotmap_l,
        ref int _maxmodulesize);

      private delegate IntPtr CPUIDSDK_fp_GetBIOSVendor(IntPtr objptr);

      private delegate IntPtr CPUIDSDK_fp_GetBIOSVersion(IntPtr objptr);

      private delegate IntPtr CPUIDSDK_fp_GetBIOSDate(IntPtr objptr);

      private delegate IntPtr CPUIDSDK_fp_GetMainboardVendor(IntPtr objptr);

      private delegate IntPtr CPUIDSDK_fp_GetMainboardModel(IntPtr objptr);

      private delegate IntPtr CPUIDSDK_fp_GetMainboardRevision(IntPtr objptr);

      private delegate IntPtr CPUIDSDK_fp_GetMainboardSerialNumber(IntPtr objptr);

      private delegate IntPtr CPUIDSDK_fp_GetSystemManufacturer(IntPtr objptr);

      private delegate IntPtr CPUIDSDK_fp_GetSystemProductName(IntPtr objptr);

      private delegate IntPtr CPUIDSDK_fp_GetSystemVersion(IntPtr objptr);

      private delegate IntPtr CPUIDSDK_fp_GetSystemSerialNumber(IntPtr objptr);

      private delegate IntPtr CPUIDSDK_fp_GetSystemUUID(IntPtr objptr);

      private delegate IntPtr CPUIDSDK_fp_GetSystemSKU(IntPtr objptr);

      private delegate IntPtr CPUIDSDK_fp_GetSystemFamily(IntPtr objptr);

      private delegate IntPtr CPUIDSDK_fp_GetChassisManufacturer(IntPtr objptr);

      private delegate IntPtr CPUIDSDK_fp_GetChassisType(IntPtr objptr);

      private delegate IntPtr CPUIDSDK_fp_GetChassisSerialNumber(IntPtr objptr);

      private delegate int CPUIDSDK_fp_GetMemoryInfosExt(
        IntPtr objptr,
        ref IntPtr _szLocation,
        ref IntPtr _szUsage,
        ref IntPtr _szCorrection);

      private delegate int CPUIDSDK_fp_GetNumberOfMemoryDevices(IntPtr objptr);

      private delegate int CPUIDSDK_fp_GetMemoryDeviceInfos(
        IntPtr objptr,
        int _device_index,
        ref int _size,
        ref IntPtr _szFormat);

      private delegate int CPUIDSDK_fp_GetMemoryDeviceInfosExt(
        IntPtr objptr,
        int _device_index,
        ref IntPtr _szDesignation,
        ref IntPtr _szType,
        ref int _total_width,
        ref int _data_width,
        ref int _speed);

      private delegate int CPUIDSDK_fp_GetProcessorSockets(IntPtr objptr);

      private delegate int CPUIDSDK_fp_GetMemoryMaxCapacity(IntPtr objptr);

      private delegate int CPUIDSDK_fp_GetMemoryMaxNumberOfDevices(IntPtr objptr);

      private delegate int CPUIDSDK_fp_GetNumberOfSPDModules(IntPtr objptr);

      private delegate int CPUIDSDK_fp_GetSPDModuleType(IntPtr objptr, int _spd_index);

      private delegate int CPUIDSDK_fp_GetSPDModuleSize(IntPtr objptr, int _spd_index);

      private delegate IntPtr CPUIDSDK_fp_GetSPDModuleFormat(IntPtr objptr, int _spd_index);

      private delegate IntPtr CPUIDSDK_fp_GetSPDModuleManufacturer(IntPtr objptr, int _spd_index);

      private delegate int CPUIDSDK_fp_GetSPDModuleManufacturerID(
        IntPtr objptr,
        int _spd_index,
        byte[] _id);

      private delegate IntPtr CPUIDSDK_fp_GetSPDModuleDRAMManufacturer(IntPtr objptr, int _spd_index);

      private delegate int CPUIDSDK_fp_GetSPDModuleMaxFrequency(IntPtr objptr, int _spd_index);

      private delegate IntPtr CPUIDSDK_fp_GetSPDModuleSpecification(IntPtr objptr, int _spd_index);

      private delegate IntPtr CPUIDSDK_fp_GetSPDModulePartNumber(IntPtr objptr, int _spd_index);

      private delegate IntPtr CPUIDSDK_fp_GetSPDModuleSerialNumber(IntPtr objptr, int _spd_index);

      private delegate float CPUIDSDK_fp_GetSPDModuleMinTRCD(IntPtr objptr, int _spd_index);

      private delegate float CPUIDSDK_fp_GetSPDModuleMinTRP(IntPtr objptr, int _spd_index);

      private delegate float CPUIDSDK_fp_GetSPDModuleMinTRAS(IntPtr objptr, int _spd_index);

      private delegate float CPUIDSDK_fp_GetSPDModuleMinTRC(IntPtr objptr, int _spd_index);

      private delegate int CPUIDSDK_fp_GetSPDModuleManufacturingDate(
        IntPtr objptr,
        int _spd_index,
        ref int _year,
        ref int _week);

      private delegate int CPUIDSDK_fp_GetSPDModuleNumberOfBanks(IntPtr objptr, int _spd_index);

      private delegate int CPUIDSDK_fp_GetSPDModuleDataWidth(IntPtr objptr, int _spd_index);

      private delegate float CPUIDSDK_fp_GetSPDModuleTemperature(IntPtr objptr, int _spd_index);

      private delegate int CPUIDSDK_fp_GetSPDModuleNumberOfProfiles(IntPtr objptr, int _spd_index);

      private delegate void CPUIDSDK_fp_GetSPDModuleProfileInfos(
        IntPtr objptr,
        int _spd_index,
        int _profile_index,
        ref float _frequency,
        ref float _tCL,
        ref float _nominal_vdd);

      private delegate int CPUIDSDK_fp_GetSPDModuleNumberOfEPPProfiles(
        IntPtr objptr,
        int _spd_index,
        ref int _epp_revision);

      private delegate void CPUIDSDK_fp_GetSPDModuleEPPProfileInfos(
        IntPtr objptr,
        int _spd_index,
        int _profile_index,
        ref float _frequency,
        ref float _tCL,
        ref float _tRCD,
        ref float _tRAS,
        ref float _tRP,
        ref float _tRC,
        ref float _nominal_vdd);

      private delegate int CPUIDSDK_fp_GetSPDModuleNumberOfXMPProfiles(
        IntPtr objptr,
        int _spd_index,
        ref int _xmp_revision);

      private delegate int CPUIDSDK_fp_GetSPDModuleXMPProfileNumberOfCL(
        IntPtr objptr,
        int _spd_index,
        int _profile_index);

      private delegate void CPUIDSDK_fp_GetSPDModuleXMPProfileCLInfos(
        IntPtr objptr,
        int _spd_index,
        int _profile_index,
        int _cl_index,
        ref float _frequency,
        ref float _CL);

      private delegate void CPUIDSDK_fp_GetSPDModuleXMPProfileInfos(
        IntPtr objptr,
        int _spd_index,
        int _profile_index,
        ref float _tRCD,
        ref float _tRAS,
        ref float _tRP,
        ref float _tRC,
        ref float _nominal_vdd,
        ref int _max_freq,
        ref float _max_CL);

      private delegate int CPUIDSDK_fp_GetSPDModuleNumberOfAMPProfiles(
        IntPtr objptr,
        int _spd_index,
        ref int _amp_revision);

      private delegate void CPUIDSDK_fp_GetSPDModuleAMPProfileInfos(
        IntPtr objptr,
        int _spd_index,
        int _profile_index,
        ref int _frequency,
        ref float _min_cycle_time,
        ref float _tCL,
        ref float _tRCD,
        ref float _tRAS,
        ref float _tRP,
        ref float _tRC);

      private delegate int CPUIDSDK_fp_GetSPDModuleRawData(IntPtr objptr, int _spd_index, int _offset);

      private delegate int CPUIDSDK_fp_GetNumberOfDisplayAdapter(IntPtr objptr);

      private delegate int CPUIDSDK_fp_GetDisplayAdapterID(IntPtr objptr, int _adapter_index);

      private delegate IntPtr CPUIDSDK_fp_GetDisplayAdapterName(IntPtr objptr, int _adapter_index);

      private delegate IntPtr CPUIDSDK_fp_GetDisplayAdapterCodeName(IntPtr objptr, int _adapter_index);

      private delegate int CPUIDSDK_fp_GetDisplayAdapterNumberOfPerformanceLevels(
        IntPtr objptr,
        int _adapter_index);

      private delegate int CPUIDSDK_fp_GetDisplayAdapterCurrentPerformanceLevel(
        IntPtr objptr,
        int _adapter_index);

      private delegate IntPtr CPUIDSDK_fp_GetDisplayAdapterPerformanceLevelName(
        IntPtr objptr,
        int _adapter_index,
        int _perf_level);

      private delegate float CPUIDSDK_fp_GetDisplayAdapterClock(
        IntPtr objptr,
        int _perf_level,
        int _adapter_index,
        int _domain);

      private delegate float CPUIDSDK_fp_GetDisplayAdapterStockClock(
        IntPtr objptr,
        int _perf_level,
        int _adapter_index,
        int _domain);

      private delegate float CPUIDSDK_fp_GetDisplayAdapterTemperature(
        IntPtr objptr,
        int _adapter_index,
        int _domain);

      private delegate int CPUIDSDK_fp_GetDisplayAdapterFanSpeed(IntPtr objptr, int _adapter_index);

      private delegate int CPUIDSDK_fp_GetDisplayAdapterFanPWM(IntPtr objptr, int _adapter_index);

      private delegate int CPUIDSDK_fp_GetDisplayAdapterMemoryType(
        IntPtr objptr,
        int _adapter_index,
        ref int _type);

      private delegate int CPUIDSDK_fp_GetDisplayAdapterMemorySize(
        IntPtr objptr,
        int _adapter_index,
        ref int _size);

      private delegate int CPUIDSDK_fp_GetDisplayAdapterMemoryBusWidth(
        IntPtr objptr,
        int _adapter_index,
        ref int _bus_width);

      private delegate IntPtr CPUIDSDK_fp_GetDisplayAdapterMemoryVendor(
        IntPtr objptr,
        int _adapter_index);

      private delegate IntPtr CPUIDSDK_fp_GetDirectXVersion(IntPtr objptr);

      private delegate int CPUIDSDK_fp_GetDisplayAdapterBusInfos(
        IntPtr objptr,
        int _adapter_index,
        ref int _bus_type,
        ref int _multi_vpu);

      private delegate float CPUIDSDK_fp_GetDisplayAdapterManufacturingProcess(
        IntPtr objptr,
        int _adapter_index);

      private delegate IntPtr CPUIDSDK_fp_GetDisplayAdapterCoreFamily(
        IntPtr objptr,
        int _adapter_index,
        ref int _core);

      private delegate IntPtr CPUIDSDK_fp_GetDisplayAdapterDriverVersion(
        IntPtr objptr,
        int _adapter_index);

      private delegate int CPUIDSDK_fp_GetDisplayAdapterPCIAddress(
        IntPtr objptr,
        int _adapter_index,
        ref int _bus,
        ref int _device,
        ref int _function);

      private delegate int CPUIDSDK_fp_GetNumberOfMonitors(IntPtr objptr);

      private delegate IntPtr CPUIDSDK_fp_GetMonitorName(IntPtr objptr, int _monitor_index);

      private delegate IntPtr CPUIDSDK_fp_GetMonitorVendor(IntPtr objptr, int _monitor_index);

      private delegate IntPtr CPUIDSDK_fp_GetMonitorID(IntPtr objptr, int _monitor_index);

      private delegate IntPtr CPUIDSDK_fp_GetMonitorSerial(IntPtr objptr, int _monitor_index);

      private delegate int CPUIDSDK_fp_GetMonitorManufacturingDate(
        IntPtr objptr,
        int _monitor_index,
        ref int _week,
        ref int _year);

      private delegate float CPUIDSDK_fp_GetMonitorSize(IntPtr objptr, int _monitor_index);

      private delegate int CPUIDSDK_fp_GetMonitorResolution(
        IntPtr objptr,
        int _monitor_index,
        ref int _width,
        ref int _height,
        ref int _frequency);

      private delegate int CPUIDSDK_fp_GetMonitorMaxPixelClock(IntPtr objptr, int _monitor_index);

      private delegate float CPUIDSDK_fp_GetMonitorGamma(IntPtr objptr, int _monitor_index);

      private delegate int CPUIDSDK_fp_GetNumberOfStorageDevice(IntPtr objptr);

      private delegate int CPUIDSDK_fp_GetStorageDriveNumber(IntPtr objptr, int _index);

      private delegate IntPtr CPUIDSDK_fp_GetStorageDeviceName(IntPtr objptr, int _index);

      private delegate IntPtr CPUIDSDK_fp_GetStorageDeviceRevision(IntPtr objptr, int _index);

      private delegate IntPtr CPUIDSDK_fp_GetStorageDeviceSerialNumber(IntPtr objptr, int _index);

      private delegate int CPUIDSDK_fp_GetStorageDeviceBusType(IntPtr objptr, int _index);

      private delegate int CPUIDSDK_fp_GetStorageDeviceRotationSpeed(IntPtr objptr, int _index);

      private delegate int CPUIDSDK_fp_GetStorageDeviceFeatureFlag(IntPtr objptr, int _index);

      private delegate int CPUIDSDK_fp_GetStorageDeviceNumberOfVolumes(IntPtr objptr, int _index);

      private delegate IntPtr CPUIDSDK_fp_GetStorageDeviceVolumeLetter(
        IntPtr objptr,
        int _index,
        int _volume_index);

      private delegate float CPUIDSDK_fp_GetStorageDeviceVolumeTotalCapacity(
        IntPtr objptr,
        int _index,
        int _volume_index);

      private delegate float CPUIDSDK_fp_GetStorageDeviceVolumeAvailableCapacity(
        IntPtr objptr,
        int _index,
        int _volume_index);

      private delegate int CPUIDSDK_fp_GetStorageDeviceSmartAttribute(
        IntPtr objptr,
        int _index,
        int _attrib_index,
        ref int _id,
        ref int _flags,
        ref int _value,
        ref int _worst,
        byte[] _data);

      private delegate int CPUIDSDK_fp_GetStorageDevicePowerOnHours(IntPtr objptr, int _index);

      private delegate int CPUIDSDK_fp_GetStorageDevicePowerCycleCount(IntPtr objptr, int _index);

      private delegate float CPUIDSDK_fp_GetStorageDeviceTotalCapacity(IntPtr objptr, int _index);

      private delegate int CPUIDSDK_fp_GetNumberOfDevices(IntPtr objptr);

      private delegate int CPUIDSDK_fp_GetDeviceClass(IntPtr objptr, int _device_index);

      private delegate IntPtr CPUIDSDK_fp_GetDeviceName(IntPtr objptr, int _device_index);

      private delegate IntPtr CPUIDSDK_fp_GetDeviceSerialNumber(IntPtr objptr, int _device_index);

      private delegate int CPUIDSDK_fp_GetDeviceIndexNumber(IntPtr objptr, int _device_index);

      private delegate int CPUIDSDK_fp_GetNumberOfSensors(
        IntPtr objptr,
        int _device_index,
        int _sensor_class);

      private delegate int CPUIDSDK_fp_GetSensorInfos(
        IntPtr objptr,
        int _device_index,
        int _sensor_index,
        int _sensor_class,
        ref int _sensor_id,
        ref IntPtr _szNamePtr,
        ref int _raw_value,
        ref float _value,
        ref float _min_value,
        ref float _max_value);

      private delegate void CPUIDSDK_fp_SensorClearMinMax(
        IntPtr objptr,
        int _device_index,
        int _sensor_index,
        int _sensor_class);

      private delegate float CPUIDSDK_fp_GetSensorTypeValue(
        IntPtr objptr,
        int _sensor_type,
        ref int _device_index,
        ref int _sensor_index);

      private delegate int CPUIDSDK_fp_GetSensorExtendedInfos(
        IntPtr objptr,
        int _device_index,
        int _sensor_index,
        int _sensor_class,
        ref IntPtr _szUnitPtr,
        ref int _flag);
    }
}
