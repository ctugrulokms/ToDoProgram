using System;

namespace to_do_list
{
    class Program
    {
        

        static void Main(string[] args)
        {
            int selector;

            do
            {

                Console.WriteLine("Lütfen yapmak istediğiniz işlemi seçiniz :)");
                Console.WriteLine("*******************************************");
                Console.WriteLine("(1) Board Listelemek");
                Console.WriteLine("(2) Board'a Kart Eklemek");
                Console.WriteLine("(3) Board'dan Kart Silmek");
                Console.WriteLine("(4) Kart Taşımak");

                selector = int.Parse(Console.ReadLine()!);

                switch (selector)
                {
                    case 1:
                        TaskManager.ListBoard();
                        break;
                    case 2:
                        TaskManager.AddCard();              
                        break;
                    case 3:
                        TaskManager.RemoveCard();     
                        break;
                    case 4:
                        TaskManager.MoveCard();
                        break;
                }

            } while(selector > 0 && selector < 5);
        }
    }
}
