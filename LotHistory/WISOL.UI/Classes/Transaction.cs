using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisol.MES.Classes
{
    public class Transaction
    {
        public const string PROPERTIES_CHANGE_FLAG = "PROPERTIES_CHANGE_FLAG";
        public const string TRUE = "Y";
        public const string FALSE = "";
        public static string[] LotStatusArray;
        public static string[] TxCodeArray;
        public static string[] TxServiceArray;

        public Transaction()
        {

        }

        public string IsMergeDifferentOperation { get; set; }
        public string IsMergeDifferentStartFlagAllow { get; set; }
        public string IsCombineDifferentMaterial { get; set; }
        public string IsSplitDifferentMaterial { get; set; }
        public string IsCombineDifferentStartFlagAllow { get; set; }
        public string IsMergeDifferentMaterial { get; set; }
        public string IsSplitDifferentOperation { get; set; }
        public string IsCombineDifferentOperation { get; set; }
        public string IsCreateZeroQtyLot { get; set; }
        public string IsMultiHoldFlag { get; set; }
        public string IsEquipmentRequiredInput { get; set; }
        public string IsBatchLotPortionRelease { get; set; }
        public string IsPullOperationMultiEnd { get; set; }
        public string IsSameRouteOperEnd { get; set; }
        public string IsMakeSummaryTempHistory { get; set; }

        public static string GetLotStatus(LotStatus lotStatus)
        {
            return lotStatus.ToString();
        }
        public static string GetTxCode(TxCode txCode)
        {
            return txCode.ToString();
        }
        public static string GetTxService(TxCode txCode)
        {
            return txCode.ToString();
        }

        public enum LotStatus
        {
            LOT_WAIT = 0,
            LOT_RUN = 1
        }
        public enum TxCode
        {
            TX_CREATE = 0,
            TX_INV_TO_LOT = 1,
            TX_LOT_TO_INV = 2,
            TX_START = 3,
            TX_END = 4,
            TX_REWORK = 5,
            TX_REPAIR = 6,
            TX_REPAIR_END = 7,
            TX_LOSS = 8,
            TX_BONUS = 9,
            TX_TERMINATED = 10,
            TX_MOVE = 11,
            TX_SKIP = 12,
            TX_SHIP = 13,
            TX_RECEIVED = 14,
            TX_SPLIT = 15,
            TX_MERGE = 16,
            TX_COMBINE = 17,
            TX_HOLD = 18,
            TX_RELEASE = 19,
            TX_ADAPT = 20,
            TX_EDC = 21,
            TX_DELETE_HISTORY = 22,
            TX_ADAPT_UDF = 23
        }
    }
}
