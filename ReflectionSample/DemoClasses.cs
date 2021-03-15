using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionSample
{
    public interface ITalk
    {
        void Talk(string sentence);
    }

    public class EmployeeMarkerAttribute : Attribute
    {
    }

    [EmployeeMarker]
    public class Employee : Person
    {
        public string Company { get; set; }
    }

    public class Alien : ITalk
    {
        public void Talk(string sentence)
        {
            // talk...
            Console.WriteLine($"Alien talking...: {sentence}");
        }
    }

    public class Person : ITalk
    {
        public string Name { get; set; }
        public int age;
        private string _aPrivateField = "initial private field value";

        public Person()
        {
            Console.WriteLine("A person is being created.");
        }

        public Person(string name)
        {
            Console.WriteLine($"A person with name {name} is being created.");
            Name = name;
        }

        private Person(string name, int age)
        {
            Console.WriteLine($"A person with name {name} and age {age} " +
                $"is being created using a private constructor.");
            Name = name;
            this.age = age;
        }

        public void Talk(string sentence)
        {
            // talk...
            Console.WriteLine($"Talking...: {sentence}");
        }

        protected void Yell(string sentence)
        {
            // yell...
            Console.WriteLine($"YELLING! {sentence}");
        }

        public override string ToString()
        {
            return $"{Name} {age} {_aPrivateField}";
        }
    }

}
