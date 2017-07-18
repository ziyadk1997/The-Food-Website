using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodWebsite.DAL
{
    public class Broadcast
    {
        private static Dictionary<Guid, Broadcast> BroadCasts { set; get; } = new Dictionary<Guid, Broadcast>();

        public double Reciept { set; get; } = 0.0;
        public bool Active { set; get; } = true;
        public Guid BroadcastID { set; get; }
        public List<Order> Orders { set; get; } = new List<Order>();
        public Restaurant Restaurant { set; get; }
        public DateTime Deadline { set; get; }
        public User User { set; get; }

        public static void Add(Broadcast broadcast)
        {
            BroadCasts.Add(broadcast.BroadcastID, broadcast);
        }

        public static void Close(Guid id)
        {
            BroadCasts[id].Active = false;
        }

        public static Broadcast Get(Guid id)
        {
            return BroadCasts[id];
        }

        public static List<Broadcast> GetAll()
        {
            return BroadCasts.ToList().Select(broadcast => broadcast.Value).ToList();
        }

    }
}
