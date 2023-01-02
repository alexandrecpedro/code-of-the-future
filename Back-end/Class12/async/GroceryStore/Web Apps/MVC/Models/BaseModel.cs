using System.Runtime.Serialization;

namespace Models
{
    [DataContract]
    public abstract class BaseModel
    {
        [DataMember]
        public int Id { get; set; }
    }
}
