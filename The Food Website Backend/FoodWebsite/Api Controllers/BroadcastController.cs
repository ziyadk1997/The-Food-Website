using FoodWebsite.DAL;
using FoodWebsite.UserIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace FoodWebsite.Controllers
{
    public class BroadcastController : ApiController
    {
        [HttpGet]
        public void Add(Guid restaurantID, DateTime deadline)
        {
            Restaurant restaurant = DAL.Restaurant.Get(restaurantID);

            Broadcast broadcast = new Broadcast
            {
                Restaurant = restaurant,
                Active = true,
                UserId = UserIdentityManager.GetUserId(),
                Deadline = deadline,
                BroadcastID = Guid.NewGuid(),
                Email = UserIdentityManager.GetUserEmail()

            };

            Broadcast.Add(broadcast);
        }
        [HttpGet]
        public void Close(Guid id)
        {
            Broadcast.Close(id);
        }
        [HttpGet]
        public List<Broadcast> History()
        {
            return Broadcast.GetAll().Where(e => e.Active == false).ToList();
        }
        [HttpGet]
        public void AddOrder([FromUri] String [] items, [FromUri] int [] values, [FromUri] String [] comments,Guid id)
        {
            List<ItemValue> x = new List<ItemValue>();
            Broadcast broadcast = Broadcast.Get(id);
            Guid userId = UserIdentityManager.GetUserId();
            Item n = null;
            for (int i = 0; i < items.Length; i++)
            {
                List<Item> it = broadcast.Restaurant.Items;
                for(int j = 0; j < it.Count; j++)
                {
                    if(items[i].Equals(it[j].Name))
                    {
                        n = it[j];
                        break;
                    }
                }
                x.Add(new ItemValue { Item = n, Quantity = values[i],Comments = comments[i] });
            }

            if(broadcast.Orders.ContainsKey(userId))
            {
                broadcast.Orders[userId] = new Order { Items = x, UserId = userId };
            }
            else
            {
                broadcast.Orders.Add(userId, new Order { Items = x, UserId = userId });
            }
        }
        [HttpGet]
        public Receipt Receipt(Guid id)
        {
            List<ReceiptItem> x = new List<ReceiptItem>();
            double t = 0.0;
            List<Order> o = Broadcast.Get(id).Orders.Select(order => order.Value).ToList();
            for (int i = 0; i < o.Count; i++)
            {
                double sum = 0.0;
                List<ItemValue> it = o[i].Items;
                for(int j = 0; j < it.Count; j++)
                {
                    if (it[j].Item.Price == 0.0)
                    {
                        sum = 0.0;
                        break;
                    }
                        sum = sum + (it[j].Item.Price * it[j].Quantity);
                    
                }
                t = t + sum;
                ReceiptItem y = new ReceiptItem
                {
                    Email = UserIdentityManager.GetUserEmail(o[i].UserId),
                    Total = sum
                };
                x.Add(y);
            }
            return new Receipt { ReceiptItems = x, Total = t };
        }
        [HttpGet]
        public List<ItemValue> ReceiptDetail(Guid id,String email)
        {
            Guid user = UserIdentityManager.GetUserID(email);
            List<Order> x = Broadcast.Get(id).Orders.Select(order => order.Value).ToList();
            for(int i = 0; i < x.Count; i++)
            {
                if(x[i].UserId == user)
                {
                    return x[i].Items;
                }
            }
            return null;
        }
        [HttpGet]
        public List<Broadcast> Active()
        {
            return Broadcast.GetAll().Where(e => e.Active == true).ToList();
        }
        [HttpGet]
        public List<ItemValue> GetCurrentOrder(Guid id)
        {
            Guid UserID = UserIdentityManager.GetUserId();
            List<Order> cur = Broadcast.Get(id).Orders.Select(order => order.Value).ToList();
            for (int i = 0;i< cur.Count(); i++)
            {
                if(cur.ElementAt(i).UserId == UserID)
                {
                    return cur.ElementAt(i).Items;
                }
            }
            return null;
        }

        [HttpGet]
        public void DeleteOrder(Guid id)
        {
            Guid UserID = UserIdentityManager.GetUserId();
            List<Order> cur = Broadcast.Get(id).Orders.Select(order => order.Value).ToList();
            for (int i = 0; i < cur.Count(); i++)
            {
                if (cur.ElementAt(i).UserId == UserID)
                {
                    cur.Remove(cur[i]);
                }
            }
        }

        public List<ItemSummary> GetBroadcastSummary(Guid broadcastId)
        {
            Broadcast broadcast = Broadcast.Get(broadcastId);
            List<Order> orders = broadcast.Orders.Select(order => order.Value).ToList();
            Dictionary<string, int> itemsCnt = new Dictionary<string, int>();
            Dictionary<string, List<string>> itemsComments = new Dictionary<string, List<string>>();
            foreach (var order in orders)
            {
                foreach (var item in order.Items)
                {
                    string comment = $"This is a comment for {item.Quantity} items: " + item.Comments;
                    if (itemsCnt.ContainsKey(item.Item.Name))
                    {
                        itemsCnt[item.Item.Name] += item.Quantity;
                        if(item.Comments != null && item.Comments != string.Empty)
                        {
                            itemsComments[item.Item.Name].Add(comment);
                        }
                    }
                    else
                    {
                        if (item.Comments != null && item.Comments != string.Empty)
                        {
                            itemsComments.Add(item.Item.Name, new List<string> { comment });
                        }

                        itemsCnt.Add(item.Item.Name, item.Quantity);
                    }
                }
            }

            return itemsCnt.Select(e => new ItemSummary { ItemName = e.Key, Quantity = e.Value, Comments = itemsComments.ContainsKey(e.Key) == true ? itemsComments[e.Key] : new List<string>() }).ToList(); 
        }



    }
}

