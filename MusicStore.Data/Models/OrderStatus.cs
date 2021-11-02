using System.Runtime.Serialization;

namespace MusicStore.Data.Models
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Sended")]
        Sended,
        [EnumMember(Value = "Accepted")]
        Accepted,
        [EnumMember(Value = "Finished")]
        Finished
    }
}