using System.ComponentModel.DataAnnotations;

namespace Contacts.Models
{
    public class Person
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        [DataType(DataType.PhoneNumber)]
        public long PhoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }
        [DataType(DataType.Date)] 
        public DateTime Date { get; set; }

        public Person()
        {

        }
    }
}
