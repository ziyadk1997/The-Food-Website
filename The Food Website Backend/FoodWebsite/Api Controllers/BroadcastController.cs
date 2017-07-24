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
                BroadcastID = Guid.NewGuid()

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
        public void AddOrder(String [] items,int [] values,String [] comments,Guid id)
        {
            List<ItemValue> x = new List<ItemValue>();
            Item n = null;
            for (int i = 0; i < items.Length; i++)
            {
                List<Item> it = Broadcast.Get(id).Restaurant.Items;
                for(int j = 0; j < it.Count; j++)
                {
                    if(items[i].Equals(it[j].Name))
                    {
                        n = it[j];
                        break;
                    }
                }
                x.Add(new ItemValue { Item = n, Quantity = values[i],comments = comments[i] });
            }
            Broadcast.Get(id).Orders.Add(new Order { Items = x, UserId = UserIdentityManager.GetUserId() });
        }
        [HttpGet]
        public Receipt Receipt(Guid id)
        {
            List<ReceiptItem> x = new List<ReceiptItem>();
            double t = 0.0;
            List<Order> o = Broadcast.Get(id).Orders;
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
                    Email = UserIdentityManager.GetName(o[i].UserId),
                    Total = sum
                };
                x.Add(y);
            }
            return new Receipt { ReceiptItems = x, Total = t };
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
            List<Order> cur = Broadcast.Get(id).Orders;
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
            List<Order> cur = Broadcast.Get(id).Orders;
            for (int i = 0; i < cur.Count(); i++)
            {
                if (cur.ElementAt(i).UserId == UserID)
                {
                    cur.Remove(cur[i]);
                }
            }
        }



    }
}

