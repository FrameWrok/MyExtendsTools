namespace ErShouCheSqlServerExtendToken.App_Code.Models
{
    public class BaseResponse<T>
    {
        /// <summary>
        /// 接口返回码
        /// </summary>
        public int returncode { get; set; }
        /// <summary>
        /// 接口返回消息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 接口返回数据
        /// </summary>
        public T result { get; set; }
    }

}
