using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Model
{
    public class Category : BaseModel
    {
        public Category() { }

        public Category(string name)
        {
            Name = name;
        }

        [Required]
        public string Name { get; private set; }
    }
}
