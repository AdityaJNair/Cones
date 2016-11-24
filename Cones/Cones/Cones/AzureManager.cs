using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cones.Models;
using Cones.Views;

namespace Cones
{
    /// <summary>
    /// Azure manager as shown in MSA
    /// </summary>
    public class AzureManager
    {

        private static AzureManager instance;
        private MobileServiceClient client;
        private IMobileServiceTable<Timeline> timelineTable;
        private IMobileServiceTable<Users> usersTable;
        private IMobileServiceTable<IceCreamOrders> ordersTable;

        private AzureManager()
        {
            client = new MobileServiceClient("http://adijnairmsaphase2.azurewebsites.net/");

            timelineTable = client.GetTable<Timeline>();

            usersTable = client.GetTable<Users>();

            ordersTable = client.GetTable<IceCreamOrders>();
        }

        public MobileServiceClient AzureClient
        {
            get { return client; }
        }

        public static AzureManager AzureManagerInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AzureManager();
                }

                return instance;
            }
        }

        //Timeline information - add a timeline
        public async Task AddTimeline(Timeline timeline)
        {
            await this.timelineTable.InsertAsync(timeline);
        }
        
        //get a timeline based on particular user
        public async Task<List<Timeline>> GetTimelines(string userId)
        {
            return await this.timelineTable.Where(Timeline => Timeline.Userid == userId).ToListAsync();
        }

        //delete a timeline (unused)
        public async Task DeleteTimeline(Timeline timeline)
        {
            await this.timelineTable.DeleteAsync(timeline);
        }

        //Ice Cream orders -- ALL CRUD operations done
        //Adding them to the database but only returning the ones that are assocaited with a specific user id
        public async Task AddIceCreamOrder(IceCreamOrders order)
        {
            await this.ordersTable.InsertAsync(order);
        }

        //get an ice cream order for a particular user.
        public async Task<List<IceCreamOrders>> GetIceCreamOrders(string userId)
        {
            return await this.ordersTable.Where(IceCreamOrders => IceCreamOrders.userId == userId).ToListAsync();
        }

        //delete ice cream for a particular user
        public async Task DeleteIceCreamOrders(IceCreamOrders order)
        {
            await this.ordersTable.DeleteAsync(order);
        }

        //update ice cream order
        public async Task UpdateIceCreamOrders(IceCreamOrders order)
        {
            await this.ordersTable.UpdateAsync(order);
        }

        //Users -- only adding them to the database
        public async Task AddUsers(Users users)
        {
            List<Users> userlist = await GetUsers(users.userId);
            if (userlist == null || userlist.Count == 0)
            {
                await this.usersTable.InsertAsync(users);
            }
        }

        //get a user based on user id from the database
        public async Task<List<Users>> GetUsers(string userid)
        {
            return await this.usersTable.Where(Users => Users.userId == userid).ToListAsync();
        }
    }
}
