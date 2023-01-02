using System;
using System.ComponentModel.DataAnnotations;

namespace Models
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

        public override bool Equals(object obj)
        {
            var category = obj as Category;
            return category != null &&
                   Id == category.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }
}
