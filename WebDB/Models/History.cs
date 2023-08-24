namespace WebDB.Models
{
    public class History
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string TypeOfChange { get; set; }
        public string TableDate { get; set; }
        public string DatetimeOfChange { get; set; }
    }
}
