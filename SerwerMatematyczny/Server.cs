using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Server : Server_Operations
{
    public delegate void TransmissionDataDelegate(NetworkStream stream);
    public delegate int IntegerOperation(int argument);
    private TcpListener server;
	public Server() : base()
	{
        server = new TcpListener(IPAddress.Parse("127.0.0.1"), 2048);
        server.Start();
    }
    public void AcceptClient() {
        while (true) {
            
            TcpClient client = server.AcceptTcpClient();
            NetworkStream stream = client.GetStream();

            TransmissionDataDelegate transmissionDelegate = new TransmissionDataDelegate(BeginDataTransmission);
            transmissionDelegate.BeginInvoke(stream, Callback, client);
        }
    }

    private void Callback(IAsyncResult ar)
    {
        
    }
    
    protected void BeginDataTransmission(NetworkStream stream) {
        BinaryWriter writer = new BinaryWriter(stream);

        stream.ReadTimeout = 10000;

        int buffer_size = 1024;
        byte[] buffer = new byte[buffer_size];
        while (true) {
            try
            {
                Send_message(writer, stream, "Podaj opcję: \n\r1. Dodawanie \n\r2. Odejmowanie \n\r3. Mnożenie \n\r4. Dzielenie \n\r5. Silnia\n\r");
                int response = Get_int(stream);
                Send_message(writer, stream, "Podaj liczbę: ");
                int a = Get_int(stream), b = 0;
                float wynik = 0;
                switch (response)
                {
                    case 1:
                        Send_message(writer, stream, "\n\rPodaj drugą liczbę: ");
                        b = Get_int(stream);
                        wynik = a + b;
                        break;
                    case 2:
                        Send_message(writer, stream, "\n\rPodaj drugą liczbę: ");
                        b = Get_int(stream);
                        wynik = a - b;
                        break;
                    case 3:
                        Send_message(writer, stream, "\n\rPodaj drugą liczbę: ");
                        b = Get_int(stream);
                        wynik = a * b;
                        break;
                    case 4:
                        Send_message(writer, stream, "\n\rPodaj drugą liczbę: ");
                        b = Get_int(stream);
                        wynik = a / b;
                        break;
                    case 5:
                        wynik = Math.Silnia(a);
                        break;
                }
                Send_message(writer, stream, "Wynik: " + wynik + "\n\r");
            }
            catch (IOException e) {
                Send_message(writer, stream, "\n\rRozłączone (10s timeout)");
                break;
            }

        }
    }
}
