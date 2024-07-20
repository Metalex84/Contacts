using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

class Program
{
    /*
     * printMenu() imprime el menú de opciones en la consola
     */
    public static void printMenu()
    {
        Console.WriteLine("¿Qué operación deseas realizar?");
        Console.WriteLine("1. Buscar por numero de telefono");
        Console.WriteLine("2. Insertar contacto nuevo");
        Console.WriteLine("3. Actualizar contacto existente");
        Console.WriteLine("4. Eliminar contacto");
        Console.WriteLine("5. Salir");
    }

    /*
     * checkPhone(string) verifica que el número de teléfono no esté vacío, tenga 11 dígitos y conste solo de números
     */
    public static bool checkPhone(string phone)
    {
        if (phone == null || phone.Equals(""))
        {
            Console.WriteLine("ERROR: el número de teléfono no puede estar vacio.");
            return false;
        }
        if (phone.Length != 11)
        {
            Console.WriteLine("ERROR: el número de teléfono debe tener 11 dígitos.");
            return false;
        }
        for (int i = 0; i < phone.Length; i++)
        {
            if (phone[i] < '0' || phone[i] > '9')
            {
                Console.WriteLine("ERROR: el número de teléfono debe tener solo números.");
                return false;
            }
        }
        return true;
    }

    /*
     * printContact(Contact) imprime los datos de un contacto en la consola
     */
    public static void printContact(Contact contact)
    {
        Console.WriteLine("*************");
        Console.WriteLine("Nombre: " + contact.Name);
        Console.WriteLine("Telefono: " + contact.Phone);
        Console.WriteLine("*************");
    }

    /*
     * readContact() lee los datos de un contacto de la consola
     */
    public static Contact readContact()
    {
        Contact contacto = new Contact();
        do
        {
            Console.WriteLine("Introduce el nombre (obligatorio)");
            try
            {
                contacto.Name = Console.ReadLine();
            }
            catch (IOException)
            {
                Console.WriteLine("ERROR: se esperaba un nombre");
            }

        } while (contacto.Name == null || contacto.Name.Equals(""));
        do
        {
            Console.WriteLine("Introduce el número de teléfono (obligatorio, 11 dígitos)");
            try
            {
                contacto.Phone = Console.ReadLine();
            }
            catch (IOException)
            {
                Console.WriteLine("ERROR: se esperaba un numero de telefono");
            }
        } while (!checkPhone(contacto.Phone));
        return contacto;
    }

    /*
     * searchContact(List<Contact>) busca un contacto en la agenda por número de teléfono
     */
    public static void searchContact(List<Contact> agenda)
    {
        string phone = "";
        do
        {
            Console.WriteLine("Por favor, introduce un numero de telefono valido");
            try
            {
                phone = Console.ReadLine();
            }
            catch (IOException)
            {
                Console.WriteLine("ERROR: se esperaba un numero");
            }
        } while (!checkPhone(phone));

        Contact contact = agenda.Find(c => c.Phone == phone);
        if (contact == null)
        {
            Console.WriteLine("No se encontró ningún contacto con el número de teléfono " + phone);
        }
        else
        {
            Console.WriteLine("Se encontró el siguiente contacto:");
            printContact(contact);
        }
    }

    /*
     * insertContact(List<Contact>) inserta un nuevo contacto en la agenda
     */
    public static void insertContact(List<Contact> agenda)
    {
        Contact contacto = readContact();
        agenda.Add(contacto);
        Console.WriteLine("Contacto añadido correctamente");
        printContact(contacto);
    }

    /*
     * deleteContact(List<Contact>) elimina un contacto de la agenda
     */
    public static void deleteContact(List<Contact> agenda)
    {
        Contact contacto = readContact();
        if (agenda.Remove(contacto))
        {
            Console.WriteLine("Contacto eliminado correctamente");
        }
        else
        {
            Console.WriteLine("No se encontró ningún contacto con esos datos");
        }
    }

    /*
     * updateContact(List<Contact>) actualiza un contacto de la agenda
     */
    public static void updateContact(List<Contact> agenda)
    {
        Contact contacto = readContact();
        int index = agenda.FindIndex(c => c.Phone == contacto.Phone);
        if (index == -1)
        {
            Console.WriteLine("No se encontró ningún contacto con esos datos");
        }
        else
        {
            agenda[index] = contacto;
            Console.WriteLine("Contacto actualizado correctamente");
            printContact(contacto);
        }
    }

    public static void Main(string[] args)
    {
        List<Contact> contacts = new List<Contact>();
        while (true)
        {
            Console.Clear();
            printMenu();
            int option = 0;
            try
            {
                option = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("ERROR: se esperaba un numero");
            }

            switch (option)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("BUSQUEDA DE CONTACTO POR NUMERO DE TELEFONO");
                    searchContact(contacts);
                    break;

                case 2:
                    Console.Clear();
                    Console.WriteLine("INSERCION DE NUEVO CONTACTO");
                    insertContact(contacts);
                    break;

                case 3:
                    Console.Clear();
                    Console.WriteLine("ACTUALIZACION DE CONTACTO EXISTENTE");
                    updateContact(contacts);
                    break;

                case 4:
                    Console.Clear();
                    Console.WriteLine("BORRADO DE CONTACTOS");
                    deleteContact(contacts);
                    break;

                case 5:
                    Console.WriteLine("Gracias por utilizar la agenda de contactos.");
                    Console.WriteLine("Por favor, presiona cualquier tecla para continuar");
                    Console.ReadKey();
                    Console.Clear();
                    return;

                default:
                    Console.WriteLine("Opción no válida. Por favor, introduce un valor entre 1 y 5");
                    break;
            }
            Console.WriteLine("Por favor, presiona cualquier tecla para continuar");
            Console.ReadKey();
        }

    }
}
