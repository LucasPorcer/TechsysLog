namespace TechsysLog.Application.Dtos.Pagination
{
    public class PagedResult<T>
    {
        public long Total { get; set; } 
        public List<T> Data { get; set; } 

        public PagedResult(long total, List<T> data)
        {
            Total = total;
            Data = data;
        }
    }

}
