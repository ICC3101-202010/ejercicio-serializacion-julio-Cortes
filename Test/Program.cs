using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> personas = new List<Person>();
            string switcher="0";
            string stopper="5";
            IFormatter formatter = new BinaryFormatter();
            while (switcher != stopper)
            {
                Console.WriteLine("(1)Crear Persona\n(2)Mostrar Personas\n(3)Serializar\n(4)Cargar Personas\n(5)Salir");
                switcher = Console.ReadLine();
                switch (switcher)
                {

                    case "1":
                        Console.WriteLine("Ingrese el nombre de la persona");
                        string name = Console.ReadLine();
                        Console.WriteLine("Ingrese el apellido de la persona");
                        string lastname = Console.ReadLine();
                        Console.WriteLine("Ingrese la edad de la persona");
                        try
                        {
                            int age = Int32.Parse(Console.ReadLine());
                            Person persona = new Person(name, lastname, age);
                            personas.Add(persona);
                            break;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("El valor ingresado en edad no es un numero, no se creara la persona");
                            break;
                        }
                    case "2":
                        if (personas.Count==0)
                        {
                            Console.WriteLine("No existen personas en la lista");
                        }
                        else
                        {
                            foreach (Person person in personas)
                            {
                                Console.WriteLine("Nombre: {0}\nApellido: {1}\nEdad: {2}\n",person.Name,person.LastName,person.Age);
                            }
                        }
                        break;
                    case "3":
                        if (personas.Count()==0)
                        {
                            Console.WriteLine("No existen personas para serializa\nr");
                        }
                        else
                        {
                            Stream streamwriter = new FileStream("File.bin", FileMode.Create, FileAccess.Write, FileShare.None);
                            int num = personas.Count();
                            formatter.Serialize(streamwriter, num);
                            foreach (Person person in personas)
                            {
                                formatter.Serialize(streamwriter, person);
                            }
                            streamwriter.Close();
                            Console.WriteLine("Archivo creado\n");
                        }
                        break;
                    case "4":                
                        Stream stream = new FileStream("File.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
                        int num1 = (int)formatter.Deserialize(stream);
                        int counter = 0;
                        while (counter != num1)
                        {
                            Person person1 = (Person)formatter.Deserialize(stream);
                            Console.WriteLine("Nombre: {0}\nApellido: {1}\nEdad: {2}\n", person1.Name, person1.LastName, person1.Age);
                            counter++;
                        }

                        stream.Close();
                        break;
                    case "5":
                        break;
                    default:
                        Console.WriteLine("Ingrese una opcion valida\n");
                        break;
                }





            }  

        }
    }
}
