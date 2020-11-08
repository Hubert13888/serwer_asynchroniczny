using System;
using System.IO;
using System.Net.Sockets;
/// <summary>
/// Clasa zawiera metody umożliwiające odczyt i zapis danych z i do serwera
/// </summary>
public class Server_Operations {
    protected NetworkStream stream;
    protected BinaryWriter writer;
    private byte[] clear = new byte[1024];
    /// <summary>
    /// Nadanie wartości writerowi - obiektowi służącemu do łatwiejszej obsługi wyjścia
    /// </summary>
    /// <param name="stream">Strumień danych typu NetworkStream</param>
    public Server_Operations(NetworkStream stream) {
        this.stream = stream;
             writer = new BinaryWriter(stream);
    }
    /// <summary>
    /// funkcja zapisu wraz z impementacją dla różnego typu zmienych
    /// </summary>
    /// <param name="msg">Tekst do wysłania</param>
    public void Send_message(string msg) {
        writer.Write(msg);
    }
    public void Send_message(int msg)
    {
        writer.Write(msg);
    }
    public void Send_message(float msg)
    {
        writer.Write(msg);
    }
    /// <summary>
    /// Funkcja służąca do pobierania danych wpisanych do użytkownika
    /// </summary>
    /// <returns>Zwraca numer</returns>
    public int Get_int()
    {
        byte[] buffer = new byte[1024];
        int msg_len = stream.Read(buffer, 0, 1024);
        string result = "";
        /*
         * Czytany jest tekst właściwy do bufora, a następnie czyszczony niepotrzebny enter. 
         * Pobrane dane są zamieniane na string, a później dla wygody na int
         */
        stream.Read(clear, 0, 1024);
        foreach(byte b in buffer)
            result += Convert.ToChar(b);
        return Convert.ToInt32(result);
    }
}
public class Math
{
	public Math() {
	}
    /// <summary>
    /// Zwraca silnię z liczny n
    /// </summary>
    /// <param name="n">Liczba z której liczyby silnię</param>
    /// <returns></returns>
    static public int Silnia(int n) {
        if (n == 0) return 1;
        return n * Silnia(n - 1);
    }
}
