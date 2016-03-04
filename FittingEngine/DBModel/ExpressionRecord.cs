namespace FittingEngine.DBModel
{
    //TODO interface fuer fucking verschiedene inegabedatenformate machen , weil in sqlite alles long? is, oder hart andere typen und expression factories supplien ,die das umwandeln ://
    public class ExpressionRecord
    {
        public int expressionID { get; set; }
        public long? operandID { get; set; }
        public long? arg1 { get; set; }
        public long? arg2 { get; set; }
        public long? expressionTypeID { get; set; }
        public long? expressionGroupID { get; set; }
        public long? expressionAttributeID { get; set; }
        public string expressionValue { get; set; }
    }
}