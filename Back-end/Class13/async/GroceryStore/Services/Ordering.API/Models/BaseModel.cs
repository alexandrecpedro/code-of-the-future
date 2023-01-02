using System.Runtime.Serialization;

namespace Ordering.Models
{
    [DataContract]
    public abstract class BaseModel
    {
        [DataMember]
        public int Id { get; set; }
    }
}
