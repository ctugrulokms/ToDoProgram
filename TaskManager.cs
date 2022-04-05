using System;

namespace to_do_list
{
    class TaskManager
    {
        static List<Card> toDo = new List<Card>();
        static List<Card> inProgress = new List<Card>();
        static List<Card> done = new List<Card>();

        public static void ListBoard()
        {
            Console.WriteLine("TODO Line");
            Console.WriteLine("************************");
            
            if(toDo.Count == 0)
                Console.WriteLine("~ BOŞ ~");
            else
            {
                foreach(Card card in toDo)
                {
                    Console.WriteLine("Başlık      : " + card.Title);
                    Console.WriteLine("İçerik      : " + card.Content);
                    Console.WriteLine("Atanan Kişi : " + Member.memberIdPair[card.AppointedMemberId]);
                    Console.WriteLine("Büyüklük    : " + card.Size.ToString());
                    Console.WriteLine(" - ");
                }
            }

            Console.WriteLine("IN PROGRESS Line");
            Console.WriteLine("************************");

            if(inProgress.Count == 0)
                Console.WriteLine("~ BOŞ ~");
            else
            {
                foreach(Card card in inProgress)
                {
                    Console.WriteLine("Başlık      : " + card.Title);
                    Console.WriteLine("İçerik      : " + card.Content);
                    Console.WriteLine("Atanan Kişi : " + Member.memberIdPair[card.AppointedMemberId]);
                    Console.WriteLine("Büyüklük    : " + card.Size.ToString());
                    Console.WriteLine(" - ");
                } 
            }
            
            Console.WriteLine("DONE Line");
            Console.WriteLine("************************");

            if(done.Count == 0)
                Console.WriteLine("~ BOŞ ~");
            else
            {
                foreach(Card card in done)
                {
                    Console.WriteLine("Başlık      : " + card.Title);
                    Console.WriteLine("İçerik      : " + card.Content);
                    Console.WriteLine("Atanan Kişi : " + Member.memberIdPair[card.AppointedMemberId]);
                    Console.WriteLine("Büyüklük    : " + card.Size.ToString()); 
                    Console.WriteLine(" - ");   
                }
            }

        }
        public static void AddCard()
        {
            string title;
            string content;
            Size size;
            int appointed;

            Console.Write("Başlık Giriniz: ");
            title = CheckAndReadLine();

            Console.Write("İçerik Giriniz: ");
            content = CheckAndReadLine();

            Console.Write("Büyüklük Seçiniz -> XS(1), S(2), M(3), L(4), XL(5): ");
            int sizeSelect = CheckAndReadInt();
            bool allowed = isSize(sizeSelect);

            while (!allowed)
            {
                Console.WriteLine("Girdiğiniz değer, uygun bir size değeri değildir. Tekrar deneyiniz.");
                Console.Write("Büyüklük Seçiniz -> XS(1), S(2), M(3), L(4), XL(5): ");
                sizeSelect = CheckAndReadInt();
                allowed = isSize(sizeSelect);
            }
            
            size = (Size)sizeSelect;
            
            Console.Write("Kişi Seçiniz: ");
            appointed = CheckAndReadInt();
            allowed = isMember(appointed);

            while(!allowed)
            {
                Console.WriteLine("Girdiğiniz ID'ye sahip bir takım üyesi bulunmamaktadır. Tekrar deneyiniz.");
                Console.Write("Kişi Seçiniz: ");
                appointed = CheckAndReadInt();
                allowed = isMember(appointed);
            }

            Card newCard = new Card(title, content, size, appointed);
            Card.cardList.Add(newCard);
            toDo.Add(newCard);
        }

        public static void RemoveCard()
        {
            string title;
            int selection;
            int count = 0;
            List<Card> cardsToRemove = new List<Card>();

            Console.WriteLine("Öncelikle silmek istediğiniz kartı seçmeniz gerekiyor.");
            Console.Write("Lütfen kart başlığını yazınız: ");
            title = CheckAndReadLine();

            if(toDo.Count != 0)
            {
                foreach (Card card in toDo)
                {
                    if(card.Title == title)
                    {
                        cardsToRemove.Add(card);
                        count++;
                    }   
                }
                if(cardsToRemove.Count != 0)
                {
                    foreach (Card card in cardsToRemove)
                        toDo.Remove(card);
                }
                    
            }

            if(inProgress.Count != 0)
            {
                foreach (Card card in inProgress)
                {
                    if(card.Title == title)
                    {
                        cardsToRemove.Add(card);
                        count++;
                    }
                }
                if(cardsToRemove.Count != 0)
                {
                    foreach (Card card in cardsToRemove)
                        inProgress.Remove(card);
                }
            }

            if(done.Count != 0)
            {
                foreach (Card card in done)
                {
                    if(card.Title == title)
                    {
                        cardsToRemove.Add(card);
                        count++;
                    } 
                }
                if(cardsToRemove.Count != 0)
                {
                    foreach (Card card in cardsToRemove)
                        done.Remove(card);     
                }
            }

            if(count == 0)
            {
                Console.WriteLine("Aradığınız kriterlere uygun kart board'da bulunamadı. Lütfen bir seçim yapınız.");
                Console.WriteLine("* Silmeyi sonlandırmak için : (1)");
                Console.WriteLine("* Yeniden denemek için : (2)");
                selection = CheckAndReadInt();

                switch (selection)
                { 
                    case 1:
                        break;
                    case 2:
                        RemoveCard();
                        break;
                }
            }
        }

        public static void MoveCard()
        {
            string title;
            string from = "";
            Card foundCard;
            List<Card> fromList = new List<Card>();
            bool found = false;

            Console.WriteLine("Öncelikle taşımak istediğiniz kartı seçmeniz gerekiyor.");
            Console.Write("Lütfen kart başlığını yazınız: ");

            title = CheckAndReadLine();

            foundCard = MoveCheck(found, title, fromList);

            if(!foundCard.Title.Equals(null))
                found = true;
  
            if(found)
            {
                Console.WriteLine("Bulunan Kart Bilgileri");
                Console.WriteLine("**************************************");
                Console.WriteLine("Başlık       : " + foundCard.Title);
                Console.WriteLine("İçerik       : " + foundCard.Content);
                Console.WriteLine("Atanan Kişi  : " + Member.memberIdPair[foundCard.AppointedMemberId]);
                Console.WriteLine("Büyüklük     : " + foundCard.Size.ToString());
                if(toDo.Contains(foundCard))
                    from = "TODO";
                else if(inProgress.Contains(foundCard))
                    from = "IN PROGRESS";
                else if(done.Contains(foundCard))
                    from = "DONE";

                Console.WriteLine("Line         : " + from);
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Lütfen taşımak istediğiniz Line'ı seçiniz:");
                Console.WriteLine("(1) TODO");
                Console.WriteLine("(2) IN PROGRESS");
                Console.WriteLine("(3) DONE");

                int selection = CheckAndReadInt();

                switch (selection)
                { 
                    case 1:
                        if(from.Equals("TODO"))
                            toDo.Remove(foundCard);
                        else if(from.Equals("IN PROGRESS"))
                            inProgress.Remove(foundCard);
                        else if(from.Equals("DONE"))
                            done.Remove(foundCard);
                        toDo.Add(foundCard);
                        ListBoard();
                        break;
                    case 2:
                        if(from.Equals("TODO"))
                            toDo.Remove(foundCard);
                        else if(from.Equals("IN PROGRESS"))
                            inProgress.Remove(foundCard);
                        else if(from.Equals("DONE"))
                            done.Remove(foundCard);
                        inProgress.Add(foundCard);
                        ListBoard();
                        break;
                    case 3:
                        if(from.Equals("TODO"))
                            toDo.Remove(foundCard);
                        else if(from.Equals("IN PROGRESS"))
                            inProgress.Remove(foundCard);
                        else if(from.Equals("DONE"))
                            done.Remove(foundCard);
                        done.Add(foundCard);
                        ListBoard();
                        break;
                    default:
                        Console.WriteLine("Hatalı bir seçim yaptınız!");
                        break;
                }
                    
            }
            else
            {
                Console.WriteLine("Aradığınız kriterlere uygun kart board'da bulunamadı. Lütfen bir seçim yapınız.");
                Console.WriteLine("* Silmeyi sonlandırmak için : (1)");
                Console.WriteLine("* Yeniden denemek için : (2)");
                int selection = CheckAndReadInt();

                switch (selection)
                { 
                    case 1:
                        break;
                    case 2:
                        MoveCard();
                        break;
                }
            }
            
        }

        public static string CheckAndReadLine()
        {
            string input = Console.ReadLine()!;

            while(input.Equals(null))
            {
                Console.WriteLine("Boş bir değer giremezsiniz. Tekrar deneyiniz.");
                input = Console.ReadLine()!;
            }

            return input;
        }

        public static int CheckAndReadInt()
        {
            string input = CheckAndReadLine();
            bool allowed = int.TryParse(input, out int parsed);

            while(!allowed)
            {
                Console.WriteLine("Girdiğiniz değer bir sayı değeri değildir. Tekrar deneyiniz.");
                input = CheckAndReadLine();
                allowed = int.TryParse(input, out parsed);
            }
            return parsed;
        }

        public static bool isSize(int input)
        {
            bool allowed = false;

            if(input > 0 && input < 6)
                allowed = true;

            return allowed;
        }

        public static bool isMember(int input)
        {
            bool allowed = false;

            if(Member.memberIdPair.ContainsKey(input))
                allowed = true;

            return allowed;
        }

        public static void removeCheck()
        {

        }
        public static Card MoveCheck(bool found, string title, List<Card> from)
        {
            Card foundCard = new Card();

            do
            {
                foreach (Card card in toDo)
                {
                    if(card.Title == title)
                    {
                        foundCard = card;
                        from = toDo;
                        found = true;
                        break;        
                    }   
                }

                if(found)
                    break;

                foreach (Card card in inProgress)
                {
                    if(card.Title == title)
                    {
                        foundCard = card;
                        from = inProgress;
                        found = true;
                        break;        
                    }   
                }
                
                if(found)
                    break;

                foreach (Card card in done)
                {
                    if(card.Title == title)
                    {
                        foundCard = card;
                        from = done;
                        found = true;
                        break;        
                    }   
                }
                
                if(found)
                    break;

            } while(!found);

            return foundCard;
        }

        static TaskManager()
        {
            foreach (Card card in Card.cardList)
                toDo.Add(card);
        }
    }
}