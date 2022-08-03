using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisol.MES.Classes
{
    public class OIComnConstants
    {
        public const string SITE_ID = "WHCSP";
        // 바코드(라벨)프린터 정보 저장용 하위 섹션명
        public const string SAVE_BARCODEPRINT = "BarcodePrint";
        public const string SAVE_BARCODEPRINT2 = "BarcodePrint2";
        public const string SAVE_BARCODELABEL = "BarcodeLabel";

        // 플립본딩에서 START 가능한 WAFER 최대 개수
        public const int LIMITED_START_WAFER_COUNT = 16;
        public const int LIMITED_START_PACKAGE_COUNT = 24;
        // 라미네이팅에서 START 가능한 Epoxy Lot 최대 개수
        public const int LIMITED_START_Epoxy_COUNT = 3;

        // FA Rework Max Count
        public const int MAX_FA_REWORK_COUNT = 3;
        public const int MAX_CI_REWORK_COUNT = 3;
        public const int MAX_OQC_REWORK_COUNT = 3;


        // Material Type 정의
        public const string MAT_TYPE_RAW_CODE = "ROH";           // 원자재
        public const string MAT_TYPE_SUB_CODE = "ROH2";          // 부자재
        public const string MAT_TYPE_PRODUCT_CODE = "FERT";      // 제품
        public const string MAT_TYPE_SEMI_PRODUCT_CODE = "HALB"; // 반제품
        public const string MAT_TYPE_HAWA_CODE = "HAWA";         // 상품

        // Material Group 정의
        public const string MAT_GROUP1_MOLDING_MOLD = "MG010";
        public const string MAT_GROUP1_MOLDING_GPS = "MG014";
        public const string MAT_GROUP1_MOLDING_SLM = "MG015";
        public const string MAT_GROUP1_MODULE_GPS = "MG020";
        public const string MAT_GROUP1_MOLDING_DPX = "MG030";
        public const string MAT_GROUP1_MODULE_RSM = "MG040";
        public const string MAT_GROUP1_MOLDING_TC_FILTER = "MG016";  // TC SAW
        public const string MAT_GROUP1_MOLDING_TC_DPX = "MG031";  // TC SAW
        public const string MAT_GROUP1_MOLDING_TRIPLE = "MG017";  // Triple
        public const string MAT_GROUP1_EPOXY = "MG050";
        public const string MAT_GROUP1_PCB = "MG090";
        public const string MAT_GROUP1_PACKAGE = "MG100";
        public const string MAT_GROUP1_SOLDER_CREAM = "MG120";
        public const string MAT_GROUP1_WAFER = "MG130";
        public const string MAT_GROUP1_BLADE = "MG140";
        public const string MAT_GROUP1_AU_WIRE = "MG150";
        public const string MAT_GROUP1_CAPILLARY = "MG160";

        //点击机种按钮时需要显示的机种分组.
        public const string MAT_GROUP1_MOLDING_WAFER_FOR_SELECT = "'" + OIComnConstants.MAT_GROUP1_MOLDING_DPX + "', '" +
                    OIComnConstants.MAT_GROUP1_MOLDING_MOLD + "', '" +
                    OIComnConstants.MAT_GROUP1_MOLDING_GPS + "', '" +
                    OIComnConstants.MAT_GROUP1_MOLDING_SLM + "', '" +
                    OIComnConstants.MAT_GROUP1_WAFER + "', '" +
                    OIComnConstants.MAT_GROUP1_MODULE_GPS + "', '" +
                    OIComnConstants.MAT_GROUP1_MOLDING_TC_DPX + "', '" +
                    OIComnConstants.MAT_GROUP1_MOLDING_TC_FILTER + "', '" +
                    OIComnConstants.MAT_GROUP1_MOLDING_TRIPLE + "', '" +
                    OIComnConstants.MAT_GROUP1_MODULE_RSM + "'";

        //MOLDING的GROUP1组合 ViewStartLotList用
        public const string MAT_GROUP1_MOLDING_FOR_SELECT = "'" + OIComnConstants.MAT_GROUP1_MOLDING_MOLD + "', '"
                            + OIComnConstants.MAT_GROUP1_MOLDING_GPS + "', '"
                            + OIComnConstants.MAT_GROUP1_MOLDING_SLM + "', '"
                            + OIComnConstants.MAT_GROUP1_MOLDING_TC_FILTER + "', '"
                            + OIComnConstants.MAT_GROUP1_MOLDING_TC_DPX + "', '"
                            + OIComnConstants.MAT_GROUP1_MOLDING_TRIPLE + "', '"
                            + OIComnConstants.MAT_GROUP1_MOLDING_DPX + "'";

        //ViewAssyLotInfo molding list
        public const string MAT_GROUP1_MOLDING_FOR_ADD_STRING = OIComnConstants.MAT_GROUP1_MOLDING_MOLD + ","
                    + OIComnConstants.MAT_GROUP1_MOLDING_GPS + ","
                    + OIComnConstants.MAT_GROUP1_MOLDING_SLM + ","
                    + OIComnConstants.MAT_GROUP1_MOLDING_TC_DPX + ","
                    + OIComnConstants.MAT_GROUP1_MOLDING_TC_FILTER + ","
                    + OIComnConstants.MAT_GROUP1_MOLDING_TRIPLE + ","
                    + OIComnConstants.MAT_GROUP1_MOLDING_DPX;


        // Material Group2 WAFER 부분 정의 20140312 kimyh
        public const string MAT_GROUP2_WAFER_SAW = "WAFER_SAW";
        public const string MAT_GROUP2_WAFER_LNA = "WAFER_LNA";
        public const string MAT_GROUP2_WAFER_EXTERNAL = "WAFER_EXTERNAL";
        public const string MAT_GROUP2_WAFER_TQ = "WAFER_TQ";
        public const string MAT_GROUP2_WAFER_BONDED_SAW_NO_GRINDING = "WAFER_BONDED_SAW";
        public const string MAT_GROUP2_WAFER_BONDED_SAW2_GRINDING = "WAFER_BONDED_SAW2";
        public const string MAT_GROUP2_WAFER_TC_SAW = "WAFER_TC_SAW"; //BackGrinding Wafer TC SAW
        public const string MAT_GROUP2_WAFER_TC_SAW2 = "WAFER_TC_SAW2"; //No BackGrinding TC SAW Wafer
        public const string MAT_GROUP2_LIQUID_EPOXY = "LIQUID_EPOXY";
        public const string MAT_GROUP2_1511 = "1511";
        public const string MAT_GROUP2_1511M = "1511M";
        public const string MAT_GROUP2_1511GPS = "1511GPS";
        public const string MAT_GROUP2_1511SLM = "1511SLM";
        public const string MAT_GROUP2_CAPILLARY = "CAPILLARY";


        // Material Group3 机种生产模式. 20170316 dyj
        public const string MAT_GROUP3_PRODUCTION = "M";          // 量产模式
        public const string MAT_GROUP3_LIMIT_PRODUCTION = "L";    // 限度生产模式


        // 외주lot 입고시 로트체계 구분자 정의 20141028 kimyh ( 첫구분자는 E , 두번째는 코드테이블 MAT_VENDOR에서 DATA3번을 사용함. )
        public const string LOT_EXTERNAL = "E";

        // Material Package Type 정의
        public const string MAT_PACKAGE_TYPE_2CHIP = "2CHIP"; // Two Chip

        // EDC PPM 상수 정의
        public const double PPM_CONSTANT = 1000000;

        // EDC SUMMARY TYPE 정의
        public const string SUM = "SUM";
        public const string AVERAGE = "AVERAGE";
        public const string PPM = "PPM";

        // Lot 판정 정의
        public const string LOT_PASS = "PASS";
        public const string LOT_FAIL = "FAIL";
        public const string LOT_HOLD = "HOLD";
        public const string LOT_PASS_FLAG = "P";
        //public const string LOT_HOLD_FLAG = "H";
        public const string LOT_FAIL_FLAG = "F";

        // 바코드 라벨 ID
        public const string WAFER_LOT_LABEL = "WAFER_LOT_LABEL";     // Wafer Lot Label
        public const string WAFER_LOT_LABEL_S = "WAFER_LOT_LABEL_S";     // Wafer Lot Label
        public const string WAFER_LOT_LABEL_NO_QR_S = "WAFER_LOT_LABEL_NO_QR_S";     // Wafer DC Inspection Lot Label
        public const string INVENTORY_LABEL = "INVENTORY_LABEL";     // 자재 Label
        public const string REEL_LOT_RE_LABEL = "REEL_LOT_RE_LABEL"; // Reel Lot Label(재발행)
        public const string REEL_LOT_RE_LABEL_S = "REEL_LOT_RE_LABEL_S"; // Reel Lot Label(재발행 Small)
        public const string REEL_LOT_LABEL = "REEL_LOT_LABEL";       // Reel Lot Label(생성)
        public const string REEL_LOT_LABEL_S = "REEL_LOT_LABEL_S";       // Reel Lot Label(생성 Small)
        public const string FA_ID_LABEL = "FA_ID_LABEL";             // FA ID Label
        public const string FA_ID_LABEL_S = "FA_ID_LABEL_S";             // FA ID Label Small
        public const string QC_SAMPLE_LABEL = "QC_SAMPLE_LABEL";             // QC Sample Label
        public const string QC_SAMPLE_LABEL_S = "QC_SAMPLE_LABEL_S";             // QC Sample Label Small
        public const string SUB_LOT_LABEL = "SUB_LOT_LABEL";         // 자재 Lot Label
        public const string SUB_LOT_LABEL_S = "SUB_LOT_LABEL_S";         // 자재 Lot Label
        public const string EPOXY_LOT_LABEL = "EPOXY_LOT_LABEL";         // Epoxy Lot Label
        public const string EPOXY_LOT_LABEL_S = "EPOXY_LOT_LABEL_S";         // Epoxy Lot Label Small
        public const string ASSY_LOT_LABEL = "ASSY_LOT_LABEL";       // Assy Lot Label
        public const string ASSY_LOT_LABEL_SMALL = "ASSY_LOT_LABEL_S"; // Assy Lot Label Small
        public const string ASSY_LOT_NEW_LABEL_SMALL = "ASSY_LOT_NEW_LABEL_S"; // Assy Lot New Label Small
        public const string ASSY_LOT_PD_LABEL = "ASSY_LOT_PD_LABEL"; // Assy Lot Label(Package Dicing)
        public const string ASSY_LOT_PD_LABEL_S = "ASSY_LOT_PD_LABEL_S"; // Assy Lot Label(Package Dicing) Small
        public const string LOT_LABEL = "LOT_LABEL";      // Normal Lot Label       
        public const string LOT_LABEL_SMALL = "LOT_LABEL_S";       // Normal Lot Label Small
        public const string MATERIAL_ID_LABEL = "MATERIAL_ID_LABEL";         // MATERIAL_ID_LABEL (INTERNAL 용 MATERIAL ID) 
        public const string MATERIAL_ID_LABEL_SMALL = "MATERIAL_ID_LABEL_S";         // MATERIAL_ID_LABEL (INTERNAL 용 MATERIAL ID) 
        public const string FRAME_LOT_LABEL = "FRAME_LOT_LABEL";         // MATERIAL_ID_LABEL (INTERNAL 용 MATERIAL ID) 
        public const string FRAME_LOT_LABEL_S = "FRAME_LOT_LABEL_S";
        public const string AL_REEL_LOT_LABEL = "AL_REEL_LOT_LABEL";
        public const string AL_REEL_LOT_LABEL_SMALL = "AL_REEL_LOT_LABEL_S";
        public const string DST_WAFER_LOT_LABEL_SMALL = "DST_WAFER_LOT_LABEL_S";


        // Lot Category
        public const string LOT_CATEGORY_NORMAL = "N";   // Normal
        public const string LOT_CATEGORY_RMA_TEMP = "Q"; // 임시 RMA Lot(RMA 입고 시 발행 되는 임시 Lot)
        public const string LOT_CATEGORY_RMA = "R";      // RMA
        public const string LOT_CATEGORY_STOCK = "0";    // Stock
        public const string LOT_CATEGORY_FA = "F";       // FA ID
        public const string LOT_CATEGORY_CI_REWORK = "C";       //  CI REWORK(Assy) Lot
        public const string LOT_CATEGORY_REWORK = "G";   // Rework
        public const string LOT_CATEGORY_NOGOOD = "D";   // No-Good(불량품 Assy LOT)
        public const string LOT_CATEGORY_ELREELSPLIT = "E";   // EL 잔량 LOT SPLIT 
        public const string LOT_CATEGORY_WAFER_TRAY = "F";       // Wafer Tray ID
        public const string LOT_CATEGORY_TESTROOM_REWORK = "T";   // NEW Rework(Assy) Lot

        // Lot Type
        public const string LOT_TYPE_PRODUCTION = "M"; // 양산LOT


        public const string LOT_TYPE_DEV = "N";        // 개발LOT
        public const string LOT_TYPE_UNKNOWN = "X";    // 웨이퍼 LOT 대기공정 투입 시 LOT 생성을 위한 LOT TYPE 정의
        public const string LOT_TYPE_EXTERNAL = "E";   // 외주AssyLot
        public const string LOT_TYPE_LIMIT_PRODUCTION = "L"; // LIMIT PRODUCTION
        public const string LOT_TYPE_SAMPLE_PRODUCTION = "S"; // SAMPLE PRODUCTION

        /* ROUTE 정의 */
        public const string ROUTE_CODE_REPROCESS = "REPRO"; // Reprocess Route

        // 공정 정의
        public const string REPROCESS_OPERATION = "OR001";               // REPROCESS 공정
        public const string WAITING_OPERATION = "OC010";                 // 대기공정
        public const string BUMP_PLASMA_ASHING_OPERATION = "OC095";      // BUMP PA공정
        public const string BUMP_OPERATION = "OC100";                    // BUMP 공정
        public const string BUMP_INSPECTION_OPERATION = "OC105";         // BUMP 검사 공정
        public const string WAFER_MOUNTING_OPERATION = "OC110";          // WAFER MOUNTING 공정
        public const string LAMINATOR_OPERATION = "OC113";               // LAMINATOR 공정
        public const string BACK_GRINDING_OPERATION = "OC114";           // BACK GRINDING 공정
        public const string MDS_OPERATION = "OC115";                     // MDS 공정
        public const string WAFER_DICING_OPERATION = "OC120";            // 웨이퍼 다이싱 공정
        public const string PR_STRIP_OPERATION = "OC124";                // 웨이퍼 PR Strip 공정
        public const string WAFER_DICING_INSPECTION_OPERATION = "OC126"; // 웨이퍼 다이싱 검사 공정
        public const string PLASMA_ASHING_OPERATION = "OC130";           // PA공정
        public const string PRE_FLIP_BONDING_OPERATION = "OC140";        // 전 플립본딩
        public const string PRE_FLIP_BONDING_2ND_OPERATION = "OC141";    // 전 플립본딩2
        public const string PRE_FLIP_BONDING_3RD_OPERATION = "OC142";    // 전 플립본딩3
        public const string FLIP_BONDING_OPERATION = "OC150";            // 플립본딩
        public const string FLIP_BONDING_MULTI_OPERATION = "OC154";      // 4chip 플립본딩 공정
        public const string AIR_BLOWER_OPERATION = "OC156";      // Air Blower 공정
        public const string FLIP_BONDING_VISUAL_INSPECTION_OPERATION = "OC158";      // chip crack 공정
        public const string FLIP_BONDING_INSPECTION_OPERATION = "OC160"; // 플립본딩 검사
        public const string ASSY_SUBSTRATE_INFO_OPERATION = "OC165";     // AssySubstrateMatch
        public const string OVEN_OPERATION = "OC180";                    // OVEN 공정
        public const string PCB_BAKING_OPERATION = "OC190";              // PCB BAKING
        public const string SMT_OPERATION = "OC200";                     // SMT공정
        public const string SMT_MERGE_OPERATION = "OC210";               // SMT MERGE 공정
        public const string MODULE_DAM_OVEN_OPERATION = "OC215";         // Module Dam Oven 공정
        public const string MODULE_MOLDING_OPERATION = "OC230";          // Module Molding 공정
        public const string MODULE_OVEN_OPERATION = "OC240";             // Module Oven 공정
        public const string GRINDING_INSPECTION_OPERATION = "OC260";     // Grinding Inspection (두께 측정) 공정
        public const string DOTTING_OPERATION = "OC285";                 // dotting 공정
        public const string MARKING_OPERATION = "OC290";                 // Marking 공정
        public const string SHEET_CRACK_INSPECTION_OPERATION = "OC295";  // Sheet Crack Inspection 공정
        public const string PKGDICING_OPERATION = "OC300";               // PKGDICING 공정
        public const string PKGDICING_INSPECTION_OPERATION = "OC310";    // PKGDICING INSPECTION 공정
        public const string BAKING_OPERATION = "OC320";                  // BAKING 공정
        public const string BAKING2ND_OPERATION = "OC350";               // BAKING 2nd 공정
        public const string LEAK_TEST_OPERATION = "OC355";               // LEAK TEST 공정
        public const string LEAK_TEST2_OPERATION = "OC35A";              // LEAT TEST2 공정
        public const string END_LINE_OPERATION = "OC360";                // EL 공정
        public const string FAIL_ANALISYS_LOT_MERGE_OPERATION = "OC370"; // FA LOT 병합
        public const string FAIL_ANALISYS_OPERATION = "OC380";           // 불량분석
        public const string CRACK_INSPECTION_OPERATION = "OC390";        // 크랙분석
        public const string VISUAL_INSPECTION_OPERATION = "OC410";       // VI OPERATION 20151008 Add by DYJ
        public const string OQC_OPERATION = "OC420";                     // 출하검사
        public const string PACKING_WAIT_OPERATION = "OC430";            // OC430:포장대기
        public const string PACKING_OPERATION = "OC440";                 // OC440:포장
        public const string PACKING_INSPECTION_OPERATION = "OC450";      // 포장검사
        public const string PACKING_LADING_OPERATION = "OC457";           // 포장적재
        public const string SHIP_OPERATION = "OC460";                    // 출하
        public const string RMA_OPERATION = "OS100";                     // RMA 창고 공정

        // 창고 공정 정의
        public const string CLEAN_ROOM_INVENTORY = "OS200"; // Clean Room 자재 창고(현장 창고)
        public const string WAREHOUSE_INVENTORY = "OS000";  // 원자재 창고

        // 화면 Argument 정의
        public const string FORM_START = "START";
        public const string FORM_END = "END";

        // 执行方式 tx_??? 20151023 by dyj
        public const string TX_START = "TX_START";
        public const string TX_END = "TX_END";
        public const string TX_EDC = "TX_EDC";
        public const string TX_BONUS = "TX_BONUS";
        public const string TX_MERGE = "TX_MERGE";
        public const string TX_COMBINE = "TX_COMBINE";

        // Code Table 정의
        public const string CODE_TABLE_MAT_GROUP1 = "MAT_GROUP1";
        public const string CODE_TABLE_MAT_GROUP2 = "MAT_GROUP2";
        public const string CODE_TABLE_RMA_CODE = "RMA_CODE";
        public const string CODE_TABLE_CHANGE_WATER_TYPE = "CHANGE_WATER_TYPE";
        public const string CODE_TABLE_ERP_BOM_CODE = "ERP_BOM_CODE";
        public const string CODE_TABLE_INITIAL_PRODUCT_TYPE = "INITIAL_PRODUCT_TYPE";
        public const string CODE_TALBE_MAT_VENDOR = "MAT_VENDOR";
        public const string CODE_TABLE_CUSTOM_TIME_TYPE = "CUSTOM_TIME_TYPE";
        public const string CODE_TABLE_JOB_STATUS = "JOB_STATUS";
        public const string CODE_TABLE_CUSTOM_FIELDS_OPERATION_TIME = "CUSTOM_FIELDS_OPERATION_TIME";
        public const string CODE_TABLE_EQUIPMENT_GROUP2 = "EQUIPMENT_GROUP2";
        public const string CODE_TABLE_DEFECT_CODE = "DEFECT_CODE";
        public const string CODE_TABLE_LOT_TYPE = "LOT_TYPE";
        public const string CODE_TABLE_ERP_MONTHLY_INVENTORY = "ERP_MONTHLY_INVENTORY";
        public const string CODE_TABLE_MAT_ADAPT_INTERFACE = "MAT_ADAPT_INTERFACE";
        public const string CODE_TABLE_HOLD_REASON = "HOLD_REASON";
        public const string CODE_TABLE_VIEW_DB_SOURCE = "VIEW_DB_SOURCE";
        public const string CODE_TABLE_MATERIAL_CATEGORY_FIELD = "MATERIAL_CATEGORY_FIELD";
        public const string CODE_TABLE_SAP_INTERFACE_OPERATION = "SAP_INTERFACE_OPERATION";
        public const string CODE_TABLE_SAP_MONTHLY_INVENTORY = "SAP_MONTHLY_INVENTORY";
        public const string CODE_TABLE_LABEL_PRINT_REASON = "LABEL_PRINT_REASON";
        public const string CODE_TABLE_CUSTOMER_MATERIAL_DIGIT_LIMIT = "CUSTOMER_MATERIAL_DIGIT_LIMIT";

        // General Constants
        public const int DST_FAIL_LIMITED_COUNT = 5;
        public const int BST_FAIL_LIMITED_COUNT = 10;
        public const string BST_DELIMIT = "BST_";

        // 포장&출하상태(릴 상태 상태: B/S -> S(Start) -> T(Test) -> Y)
        public const string PACKING_SEQ_START = "S";        // 포장대기
        public const string PACKING_SEQ_TEST = "T";         // 포장
        public const string PACKING_SEQ_SHIPPING = "Y";     // 출하

        // FA Lot 병합 내부용/외부용
        public const string INTERNAL_USE = "INTERNAL"; // 내부용
        public const string EXTERNAL_USE = "EXTERNAL"; // 외부용

        // Hold Code
        public const string HOLD_CODE_FA = "HD001"; // FA HOLD
        public const string HOLD_CODE_CI = "HD002"; // CI HOLD
        public const string HOLD_CODE_OQC = "HD003"; // OQC HOLD
        public const string HOLD_CODE_DST = "HD004"; // DST HOLD
        public const string HOLD_CODE_MAVERICK = "HD005"; // Maverick HOLD
        public const string HOLD_CODE_PROCESS = "HD007"; // Time Out Hold
        public const string HOLD_CODE_TIMEOUT = "HD008"; // Time Out Hold
        public const string HOLD_CODE_BST = "HD009"; // BST HOLD
        public const string HOLD_CODE_PRSTRIP_REWORK = "HD014"; // FB inspection hold
        public const string HOLD_CODE_FB_INSPECTION_MAVERICK = "HD015"; // Time Out Hold
        public const string HOLD_CODE_REWORK_HOLD = "HD018"; // Rework Hold
        public const string HOLD_CODE_FB_VI_REWORK = "HD013"; // FB inspection hold
        public const string HOLD_CODE_EL_AUTO = "HD998"; // EndLine Auto Hold Reel Qty < DefualtQTY
        public const string HOLD_CODE_REMOTE = "HD999"; // Remote Machine Hold
        public const string HOLD_CODE_REMOTE_TIME_OUT = "HDQ01"; // Remote Machine Time Out Hold

        // Release Code Table
        public const string CODE_TABLE_RELEASE_PRE_FLIP_DST = "RELEASE_OC140"; // Pre Flip Bonding Release Table
        public const string CODE_TABLE_RELEASE_FLIP_DST = "RELEASE_OC150"; // Flip Bonding Release Table
        public const string CODE_TABLE_RELEASE_FB_VI = "RELEASE_OC160"; // Flip Bonding Visual Inspection Release Table
        public const string CODE_TABLE_RELEASE_MAVERICK = "RELEASE_OC360"; // Maverick Release Table
        public const string CODE_TABLE_RELEASE_FA = "RELEASE_OC380"; // FA Release Table
        public const string CODE_TABLE_RELEASE_CI = "RELEASE_OC390"; // CI Release Table
        public const string CODE_TABLE_RELEASE_OQC = "RELEASE_OC420"; // OQC Release Table
        public const string CODE_TABLE_RELEASE_REMOTE = "RELEASE_REMOTEMACHINE"; // Remote Machine Holde -> Release Table

        // Retest NG Reason Table
        public const string CODE_TABLE_RETEST_NG = "RETEST_NG_CODE"; //Retest NG Reason Table
        public const string CODE_TABLE_PACKING_LABEL_CONTROL = "PACKING_LABEL_CONTROL"; //Retest NG Reason Table


        // Release Code
        public const string RELEASE_CODE_RESTART = "RL001"; // 재가동
        public const string RELEASE_CODE_SCRAP = "RL002"; // 폐기
        public const string RELEASE_CODE_REWORK = "RL003"; // 재작업
        public const string RELEASE_CODE_COERCION_PROCESS = "RL004"; // 강제 재가동(TIME OUT HOLD일 경우)

        // RMA Code
        public const string RMA_CODE_PASS = "RMA000"; // 정상

        // Rework Code Table
        public const string CODE_TABLE_REWORK_REPROCESS = "REWORK_OR001"; // FA, CI, OQC HOLD 시 재작업 코드

        // Rework Code
        public const string REWORK_CODE_RW020 = "RW020"; // 그라인딩 검사


        // Change Water Type
        public const string CHANGE_TYPE_CHANGE_LIQUID = "C";
        public const string CHANGE_TYPE_REPLENISH_LIQUID = "R";
        public const string CHANGE_TYPE_USED_LIQUID = "U";

        // Comment Subject
        public const string COMMON_COMMENT = "[Common Comment] : ";
        public const string REEL_COMMENT = "[Reel Comment] : ";

        // Raw Material Out Type From Inventory
        public const string TO_CLEAN_ROOM_INV = "TO_CLEAN_ROOM_INV";
        public const string NEED_TO_OUT_FROM_WAIT = "NEED_TO_OUT_FROM_WAIT";

        // MRO
        public const string MRO = "M";
        public const string MO = "MO";
        public const string RO = "R";
        public const string O = "O";

        // Bump BST Sample Test
        public const string BST_SAMPLE_TEST = "BS001";


        public const string EQUIPMENT_LOCAL_MODE = "LOCAL";
        public const string EQUIPMENT_REMOTE_MODE = "REMOTE";

        // Job Status
        public const string JOB_STATUS_PREPARING = "PREPARING";
        public const string JOB_STATUS_LOAD = "LOAD";
        public const string JOB_STATUS_START_COMMAND = "START_COMMAND";
        public const string JOB_STATUS_START = "START";
        public const string JOB_STATUS_END = "END";

        // COUSTOMERS
        public const string KOREA_WISOL = "100000";
        public const string SEHZ = "101554";
        public const string TSTC_SEC_CN = "112102";
        public const string SEVT_VIETNAM = "113080";
        public const string SEV_VIETNAM = "113081";
        public const string SIEL = "113082";
        public const string LG = "133086";
        public const string UASCENT = "133120";
        public const string Haluya = "113108";
        public const string K1_GUMI_E1 = "102480-E1";
        public const string K1_GUMI_E3 = "102480-E3";

        // Equipment Group2
        public const string EQUIPMENT_GROUP2_AMB_6000F_4CHIP = "AMB-6000F";
        public const string EQUIPMENT_GROUP2_DC_TWO_BLADE = "DFD6341";

        // Korea Wisol SMT Material
        public const string MATERIAL_LAST_7 = "7";
        public const string MATERIAL_LAST_8 = "8";
        public const string MATERIAL_LAST_K = "K";


        // View Select DB Source
        public const string MESDB = "MESDB"; // MES源数据库,发布服务器
        public const string MESDB2 = "MESDB2"; // MES订阅服务器

        // Material Change Barcode Print
        public const string FB_MATERIAL_CHANGE = "FB_MATERIAL_CHANGE";

        // Get Lot Information Process Module
        public const string PROCESS_MODULE_LOT_INFO = "LOT_INFO";
        public const string PROCESS_MODULE_LOT_LIST = "LOT_LIST";

        // OCR
        public const string OCR_VI_OPERATION = "VI";
        public const string OCR_OQC_OPERATION = "OQC";
        public const string OCR_PACKING_OPERATION = "PACKING";

        /* MES LOSS CODE */
        public const string DEFECT_CODE_SAMPLE = "SA001";
        public const string DEFECT_CODE_QC_SAMPLE = "SA002";

        #region SAP Constants

        /* SAP MRP Code */
        public const string SAP_MRP_CODE_610 = "610";
        public const string SAP_MRP_CODE_600 = "600";

        /* SAP Product Order Type Definition */
        public const string SAP_ORDER_TYPE_NORMAL = "ZP01";
        public const string SAP_ORDER_TYPE_URGENCY = "ZP02";
        public const string SAP_ORDER_TYPE_REWORK = "ZP99";
        public const string MES_ORDER_TYPE_VIRTUAL_NORMAL = "VP00";
        public const string MES_ORDER_TYPE_VIRTUAL_REWORK = "VP99";

        /* SAP Material Group Defined */
        public const string SAP_MATERIAL_GROUP_HALB = "4000"; // 반제품
        public const string SAP_MATERIAL_GROUP_FERT = "5000"; // 완제품

        #endregion
    }
}
