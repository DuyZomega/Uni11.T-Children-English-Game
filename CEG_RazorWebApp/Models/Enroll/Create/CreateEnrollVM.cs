namespace CEG_RazorWebApp.Models.Enroll.Create
{
    public class CreateEnrollVM
    {
        public string? ParentFullname { get; set; }
        public string? StudentFullname { get; set; }
        public string? Classname { get; set; }
        public int EnrollFee { get; set; }
        public string? TransactionType { get; set; }
    }
}
