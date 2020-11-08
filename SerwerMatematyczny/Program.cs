using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SerwerMatematyczny
{
    class Program {
        static void Main(string[] args) {
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 2048);
            server.Start();
            TcpClient client = server.AcceptTcpClient();
            NetworkStream stream = client.GetStream();

            Server_Operations o = new Server_Operations(stream);

            while (true) {
                o.Send_message("Podaj opcję: \n\r1. Dodawanie \n\r2. Odejmowanie \n\r3. Mnożenie \n\r4. Dzielenie \n\r5. Silnia\n\r");
                int response = o.Get_int();
                o.Send_message("Podaj liczbę: ");
                int a = o.Get_int(), b = 0
                float wynik = 0;
                switch (response) {
                    case 1:
                        o.Send_message("\n\rPodaj drugą liczbę: ");
                        b = o.Get_int();
                        wynik = a + b;
                        break;
                    case 2:
                        o.Send_message("\n\rPodaj drugą liczbę: ");
                        b = o.Get_int();
                        wynik = a - b;
                        break;
                    case 3:
                        o.Send_message("\n\rPodaj drugą liczbę: ");
                        b = o.Get_int();
                        wynik = a * b;
                        break;
                    case 4:
                        o.Send_message("\n\rPodaj drugą liczbę: ");
                        b = o.Get_int();
                        wynik = a / b;
                        break;
                    case 5:
                        wynik = Math.Silnia(a);
                        break;
                }
                o.Send_message("Wynik: " + wynik + "\n\r");
            }
        }
    }
}
