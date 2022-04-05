using System;
using System.Collections.Generic;

namespace to_do_list
{
    class Card
    {
        private string title;
        private string content;
        private Size size;
        private int appointedMemberId;
        public static List<Card> cardList;
        
        public Card()
        {
            
        }

        public Card(string title, string content, Size size, int appointedMemberId)
        {
            this.Title = title;
            this.Content = content;
            this.Size = size;

            if(Member.memberIdPair.ContainsKey(appointedMemberId))
                this.AppointedMemberId = appointedMemberId;
            else
                throw new InvalidDataException("Bu ID numarasına sahip bir takım üyesi bulunmamaktadır.");

        }

        static Card()
        {
            cardList = new List<Card>();
            cardList.Add(new Card("T-Shirt", "Yaz kreasyonu için t-shirt.", Size.S, 1));
            cardList.Add(new Card("Mont", "Karlı havalar için kayak montu.", Size.M, 2));
            cardList.Add(new Card("Pantolon", "Kamuflaj ve düz desenli seçenekleriyle kargo pantolon.", Size.XL, 3));
        }

        public string Title { get => title; set => title = value; }
        public string Content { get => content; set => content = value; }
        public Size Size { get => size; set => size = value; }
        public int AppointedMemberId { get => appointedMemberId; set => appointedMemberId = value; }
    }

    enum Size
    {
        XS = 1,
        S,
        M,
        L,
        XL
    }
}