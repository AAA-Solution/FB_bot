namespace FB_Connector
{
    public class BaseResponse<T>
    {
        public string RespCode { get; set; }
        public string RespMsg { get; set; }
        public T RespObj { get; set; }
    }
}
