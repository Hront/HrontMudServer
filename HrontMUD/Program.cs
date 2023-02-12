namespace HrontMUD
{
    internal class Program
    {
        static void Main()
        {
            Server server = new(); //Создаем экземпляр класса Server.
            server.Acceptor(); //Вызываем асинхронный метод для принятия подключений и комманд от клиента.
            GameLoop gameLoop = new();
            Console.ReadLine();
        }
    }
}