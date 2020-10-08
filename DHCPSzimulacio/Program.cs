using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DHCPSzimulacio
{
    class Program
    {
        static List<string> excluded = new List<string>();
        static Dictionary<string, string> reserved = new Dictionary<string, string>();
        static Dictionary<string, string> dhcp = new Dictionary<string, string>();
        static void BeolvasExcluded()
        {
            try
            {
                StreamReader file = new StreamReader("excluded.csv");
                try
                {
                    while (!file.EndOfStream)
                    {
                        excluded.Add(file.ReadLine());
                    }

                }
                catch (Exception exception)
                {
                        Console.WriteLine(exception.Message);
                }
                finally
                {
                        file.Close();
                }
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static string CimEggyelNo(string cim)
        {
            /*
             * cim = "192.168.10.100"
             * return "192.168.10.101"
             * 
             * szétvégni '.'-mentén
             * az utolsót int-é konvertálni
             * egyet hozzáadni (255-t ne lépje tól)
             * 
             * összefűzni string-é
             */
           
                string[] adatok = cim.Split('.');
                int okt4 = int.Parse(adatok[3]);

                if (okt4 < 255)
                {
                    okt4++;
                }
                return adatok[0] + "." + adatok[1] + "." + adatok[2] + "." + okt4.ToString();

         }

        static void BeolvasDictionary(Dictionary<string, string> d, string filenev)
        {
            try
            {
                StreamReader file = new StreamReader(filenev);
                while (!file.EndOfStream)
                {
                    string[] adatok = file.ReadLine().Split(';');
                    d.Add(adatok[0], adatok[1]);
                }
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message
                    );
            }
        }

            static void Main(string[] args)
            {
                BeolvasExcluded();
            BeolvasDictionary(dhcp, "dhcp.csv");
            BeolvasDictionary(reserved, "reserved.csv");

            foreach (var e in dhcp)
            {
                Console.WriteLine(e);
            }

            foreach (var e in reserved)
            {
                Console.WriteLine(e);
            }

                //foreach (var e in excluded)
                //{
                //    Console.WriteLine(e);
                //}

            Console.WriteLine("\nVége");

            Console.WriteLine(CimEggyelNo("192.168.10.100"));
            Console.ReadKey();
            }
        }

    }