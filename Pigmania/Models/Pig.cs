using System.ComponentModel.DataAnnotations;

namespace Pigmania.Models
{
    public class Pig
    {
        public Pig() {}
        
        public Pig(string name, int speed)
        {
            Name = name;
            Speed = speed;
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public int Speed { get; set; }

    }
}