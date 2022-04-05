using System.Collections.Generic;

namespace to_do_list
{
    class Member
    {
        private string name;
        private string surname;
        private int id;
        public static List<Member> memberList;
        public static Dictionary<int,string> memberIdPair;

        public Member(string name, string surname, int id)
        {
            this.Name = name;
            this.Surname = surname;
            this.Id = id;
        }

        static Member()
        {
            memberList = new List<Member>();
            memberList.Add(new Member("Kavita", "Humphries", 1));
            memberList.Add(new Member("Connagh", "Wagstaff", 2));
            memberList.Add(new Member("Lloyd", "Wilkes", 3));

            memberIdPair = new Dictionary<int, string>();

            foreach (Member member in memberList)
                memberIdPair.Add(member.Id, member.Name + " " + member.Surname);
        }

        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public int Id { get => id; set => id = value; }
    }

}