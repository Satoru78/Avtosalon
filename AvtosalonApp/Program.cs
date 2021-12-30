﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AvtosalonApp
{
    class Program
    {
        private static int _action { get; set; }
        private static string _license { get; set; }
        private static int _id { get; set; }
        private Car _car { get; set; }
        private static List<Car> _cars { get; set; }
        public static List<Client> Clients { get; set; }


        static void Main(string[] args)
        {
            //Список авто
                Clients = new List<Client>();

                _cars = new List<Car>()
            {
                new Car(01, "Mitsubishi", "2009", "320 тыс. км", false, 250000),
                new Car(02, "Nissan", "2015", "130 тыс. км",  false, 185000),
                new Car(03, "Toyota", "1990", "145 тыс. км",  false, 214000),
                new Car(04, "Suzuki", "2003", "255 тыс. км", true, 365000),
                new Car(05, "Subaru", "2007", "96 тыс. км", false, 200000),
                new Car(06, "Mazda", "1995", "40 тыс. км",  true, 250000),
               
            };
                // Выбор команд
                while (_action != 3)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Commands();
                    Console.ForegroundColor = ConsoleColor.White;
                    Message("Введите команду: ", ConsoleColor.Yellow);
                    _action = int.Parse(Console.ReadLine());
                    switch (_action)
                    {
                        case 1:
                            foreach (var item in _cars)
                            {
                                if (item.Status == true)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(item.GetInfo());
                                    Console.ForegroundColor = ConsoleColor.Green;
                                }
                                else
                                {

                                    Console.WriteLine(item.GetInfo());

                                }
                            }
                            // Покупка
                            Message("Введите ID авто: ", ConsoleColor.Blue);
                            _id = int.Parse(Console.ReadLine());
                            var bull = _cars.FirstOrDefault(item => item.ID == _id).Status;
                            if (bull == false)
                            {


                                var selectAvto = _cars.FirstOrDefault(item => item.ID == _id);
                                Clients.Add(Buy(new Client()));
                                selectAvto.Status = true;
                                Message("Автомобиль куплен\n", ConsoleColor.Green);
                                Message($"Сумма к оплате: {selectAvto.Price} $ | Покупка прошла успешно", ConsoleColor.Green);


                                using (FileStream stream = new FileStream($"{Environment.CurrentDirectory}/client.csv", FileMode.Create))
                                {
                                    using (StreamWriter ar = new StreamWriter(stream))
                                    {
                                        ar.WriteLine("ID;Имя;Фамилия;Отчество;Год рождения;Пол;Серия паспрта;Номер паспорта;Адрес регистрации;Дата выдачи;Код подразделения;Кем был выдан;");
                                        foreach (var item in Clients)
                                        {
                                            ar.WriteLine($"{item.ID};{item.Name};{item.Surname};{item.Patronymic};{item.DateOfBirth};{item.Gender};{item.Seria};{item.Number};{item.Address};{item.DateOfIssue};{item.DepartmentCode};{item.issuedBy};");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Message("Автомобиля нет в наличии!\n", ConsoleColor.Red);
                            }
                            break;

                        case 2:
                            if (Clients.Any())
                            {
                                foreach (var item in Clients)
                                {
                                    Console.WriteLine(item.GetInfo());
                                }
                            }
                            else
                            {
                                Message("Ошибка ,список пуст", ConsoleColor.Red);
                            }
                            break;
                        case 3:
                            Process.GetCurrentProcess().Kill();
                            break;

                    }

                    Console.ReadKey();
                }
            }

            public static Client Buy(Client client)
            {
                if (client.ID == 0)
                {

                    client = new Client();
                    Console.WriteLine("Введите паспортные данные ниже");
                    Console.Write("Введите ID покупателя: ");
                    client.ID = int.Parse(Console.ReadLine());

                }
                Console.Write("Введите имя: ");
                client.Name = Console.ReadLine();
                Console.Write("Введите фамилию: ");
                client.Surname = Console.ReadLine();
                Console.Write("Введите отчество: ");
                client.Patronymic = Console.ReadLine();
                Console.Write("Введите год рождения: ");
                client.DateOfBirth = DateTime.Parse(Console.ReadLine());
                Console.Write("Введите пол: ");
                client.Gender = Console.ReadLine();
                Console.Write("Введите серия паспорта: ");
                client.Seria = Console.ReadLine();
                Console.Write("Введите номер паспорта: ");
                client.Address = Console.ReadLine();
                Console.Write("Введите дата выдачи паспорта: ");
                client.DateOfIssue = DateTime.Parse(Console.ReadLine());
                Console.Write("Введите код подразделения: ");
                client.DepartmentCode = Console.ReadLine();
                Console.Write("Введите кем был выдан: ");
                client.Number = Console.ReadLine();
                Console.Write("Введите адерс регистрации: ");
                client.issuedBy = Console.ReadLine();
                client.License = _license;
                return client;
            }
            static void Message(string message, ConsoleColor consoleColor)
            {
                Console.ForegroundColor = consoleColor;
                Console.Write(message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            static void Commands()
            {
                Console.WriteLine("1. Приобрести автомобиль");
                Console.WriteLine("2. Вывести данные о клиенте");
                Console.WriteLine("3. Закрыть программу");
            }

        }

    }
