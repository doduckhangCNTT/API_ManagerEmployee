namespace MISA.WebFresher032023.Practice.Model
{
    public class EmployeeFilter
    {
        public int TotalPage { get; set; }
        public int TotalRecord { get; set; }
        public int CurrentPage { get; set; }
        public int CurrentPageRecords { get; set; }
        public IEnumerable<Employee> Data { get; set; }
    }
}
