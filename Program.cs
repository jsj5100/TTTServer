using System;
using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static char[] arr = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static void Main()
        {
            var ipep = new IPEndPoint(IPAddress.Any, 10000);

            using (Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                server.Bind(ipep);
                server.Listen(20);

                Console.WriteLine($"Server Start... Listen port {ipep.Port}...");

                using (var client = server.Accept())
                {
                    var ip = client.RemoteEndPoint as IPEndPoint;

                    Console.WriteLine($"Client : (From:{ip.Address.ToString()}:{ip.Port}, " + $"COnnection time: {DateTime.Now})");

                    client.Send(Encoding.ASCII.GetBytes("Welcome server!\r\n"));
                    client.Send(Encoding.ASCII.GetBytes("Press any key to access the game.\r\n"));
                    var sb = new StringBuilder();
                    var TTC = new Byte[1024];
                    string strData = Encoding.Default.GetString(TTC);
                    int endPoint = strData.IndexOf('\0');
                    string parsedMessage = strData.Substring(0, endPoint + 1);

                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("My egg = X / enemy egg = O");
                        Console.WriteLine("\n1~9의 숫자를 쓰고, 보드판의 숫자를 쓰고 Enter을 누르세요");
                        Console.WriteLine("\n");
                        ShowMap();
                        Console.Write("\n입력 : ");
                        client.Receive(TTC);
                        var data = Encoding.ASCII.GetString(TTC);
                        var msg = Encoding.ASCII.GetString(TTC);
                        sb.Append(data.Trim('\0'));
                        if (sb.Length > 2 && sb[sb.Length - 2] == '\r' && sb[sb.Length - 1] == '\n')
                        {
                            data = sb.ToString().Replace("\n", "").Replace("\r", "");
                            if (String.IsNullOrWhiteSpace(data))
                            {
                                continue;
                            }
                            if ("EXIT".Equals(data, StringComparison.OrdinalIgnoreCase))
                            {
                                break;
                            }
                            sb.Length = 0;
                            Console.WriteLine("상대방 = " + data + "\r\n");
                            var sendMsg = Encoding.ASCII.GetBytes(data);
                            client.Send(sendMsg);
                            Console.Write("입력 : ");
                            msg = Console.ReadLine();
                            var dif = Encoding.ASCII.GetBytes(msg);
                            client.Send(dif);
                            if (data == "1" && arr[1] != '0' && arr[1] != 'X')
                            {
                                arr[1] = 'O';
                            }
                            else if (data == "2" && arr[2] != '0' && arr[2] != 'X')
                            {
                                arr[2] = 'O';
                            }
                            else if (data == "3" && arr[3] != '0' && arr[3] != 'X')
                            {
                                arr[3] = 'O';
                            }
                            else if (data == "4" && arr[4] != '0' && arr[4] != 'X')
                            {
                                arr[4] = 'O';
                            }
                            else if (data == "5" && arr[5] != '0' && arr[5] != 'X')
                            {
                                arr[5] = 'O';
                            }
                            else if (data == "6" && arr[6] != '0' && arr[6] != 'X')
                            {
                                arr[6] = 'O';
                            }
                            else if (data == "7" && arr[7] != '0' && arr[7] != 'X')
                            {
                                arr[7] = 'O';
                            }
                            else if (data == "8" && arr[8] != '0' && arr[8] != 'X')
                            {
                                arr[8] = 'O';
                            }
                            else if (data == "9" && arr[9] != '0' && arr[9] != 'X')
                            {
                                arr[9] = 'O';
                            }
                            else
                            {
                                Console.WriteLine("잘못된 숫자나, 중복된 숫자를 입력하셨습니다.", data);
                                Console.WriteLine("\n");
                                Console.WriteLine("2초뒤에 다시 눌러주세요");
                                Thread.Sleep(2000);
                            }
                            if (msg == "1" && arr[1] != '0' && arr[1] != 'X')
                            {
                                arr[1] = 'X';
                            }
                            else if (msg == "2" && arr[2] != '0' && arr[2] != 'X')
                            {
                                arr[2] = 'X';
                            }
                            else if (msg == "3" && arr[3] != '0' && arr[3] != 'X')
                            {
                                arr[3] = 'X';
                            }
                            else if (msg == "4" && arr[4] != '0' && arr[4] != 'X')
                            {
                                arr[4] = 'X';
                            }
                            else if (msg == "5" && arr[5] != '0' && arr[5] != 'X')
                            {
                                arr[5] = 'X';
                            }
                            else if (msg == "6" && arr[6] != '0' && arr[6] != 'X')
                            {
                                arr[6] = 'X';
                            }
                            else if (msg == "7" && arr[7] != '0' && arr[7] != 'X')
                            {
                                arr[7] = 'X';
                            }
                            else if (msg == "8" && arr[8] != '0' && arr[8] != 'X')
                            {
                                arr[8] = 'X';
                            }
                            else if (msg == "9" && arr[9] != '0' && arr[9] != 'X')
                            {
                                arr[9] = 'X';
                            }
                            else
                            {
                                Console.WriteLine("잘못된 숫자나, 중복된 숫자를 입력하셨습니다.", msg);
                                Console.WriteLine("\n");
                                Console.WriteLine("2초뒤에 다시 눌러주세요");
                                Thread.Sleep(2000);
                            }
                        }
                    }
                    Console.WriteLine($"Disconnected : (From: {ip.Address.ToString()}:{ip.Port}, " + $"Connection time: {DateTime.Now}");
                }
            }
            Console.WriteLine("Press Any key...");
            Console.ReadLine();
        }
        public static void ShowMap()
        {
            Console.WriteLine("     |     |      ");
            Console.WriteLine("  {0}  |  {1}  |  {2}", arr[1], arr[2], arr[3]);
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine("  {0}  |  {1}  |  {2}", arr[4], arr[5], arr[6]);
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine("  {0}  |  {1}  |  {2}", arr[7], arr[8], arr[9]);
            Console.WriteLine("     |     |      ");
        }
    }
}