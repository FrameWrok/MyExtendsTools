using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace System.Net.Network
{
    /// <summary>
    /// 当前机器的网卡信息
    /// </summary>
    public partial class NetWorkCard
    {
        /// <summary>
        /// 网卡描述
        /// </summary>
        public string Description;

        /// <summary>
        /// 网卡标识符
        /// </summary>
        public string Id;

        /// <summary>
        /// 网卡名称
        /// </summary>
        public string Name;

        /// <summary>
        /// 网卡类型
        /// </summary>
        public NetworkInterfaceType NetworkInterfaceType;

        /// <summary>
        /// 速度（M）
        /// </summary>
        public double Speed;

        /// <summary>
        /// 操作状态
        /// </summary>
        public OperationalStatus OperationalStatus;

        /// <summary>
        /// 网卡地址
        /// </summary>
        public string MACAddress;

        /// <summary>
        /// 返回表示当前 System.Object 的 System.String。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("描述：" + this.Description);
            sb.AppendLine("标识符：" + this.Id);
            sb.AppendLine("名称：" + this.Name);
            sb.AppendLine("类型：" + this.NetworkInterfaceType);
            sb.AppendLine("速度：" + this.Speed + "M");
            sb.AppendLine("操作状态：" + this.OperationalStatus);
            sb.AppendLine("MAC 地址：" + this.MACAddress);
            return sb.ToString();
        }

        /// <summary>
        /// 获取当前机器的所有网卡信息
        /// </summary>
        /// <returns></returns>
        public static List<NetWorkCard> GetCurrentNetWorkList()
        {
            List<NetWorkCard> list = new List<NetWorkCard>();
            ////获取本地计算机上网络接口的对象
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                var card =
                new NetWorkCard()
                {
                    Description = adapter.Description,
                    Id = adapter.Id,
                    Name = adapter.Name,
                    NetworkInterfaceType = adapter.NetworkInterfaceType,
                    OperationalStatus = adapter.OperationalStatus,
                    Speed = adapter.Speed * 0.001 * 0.001
                };
                //// 格式化显示MAC地址
                ////获取适配器的媒体访问（MAC）地址
                PhysicalAddress pa = adapter.GetPhysicalAddress();
                ////返回当前实例的地址
                byte[] bytes = pa.GetAddressBytes();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    ////以十六进制格式化
                    sb.Append(bytes[i].ToString("X2"));
                    if (i != bytes.Length - 1)
                    {
                        sb.Append("-");
                    }
                }
                card.MACAddress = sb.ToString();
                list.Add(card);
            }
            return list;
        }

    }
}
