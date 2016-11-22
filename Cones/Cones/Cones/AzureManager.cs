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

        //Timeline information
        public async Task AddTimeline(Timeline timeline)
        {
            await this.timelineTable.InsertAsync(timeline);
        }

        public async Task<List<Timeline>> GetTimelines()
        {
            return await this.timelineTable.ToListAsync();
        }

        //Ice Cream orders
        //Adding them to the database but only returning the ones that are assocaited with a specific user id
        public async Task AddIceCreamOrder(IceCreamOrders order)
        {
            await this.ordersTable.InsertAsync(order);
        }

        public async Task<List<IceCreamOrders>> GetIceCreamOrders(string userId)
        {
            return await this.ordersTable.Where(IceCreamOrders => IceCreamOrders.userId == userId).ToListAsync();
        }

        public async Task DeleteIceCreamOrders(IceCreamOrders order)
        {
            await this.ordersTable.DeleteAsync(order);
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

        public async Task<List<Users>> GetUsers(string userid)
        {
            return await this.usersTable.Where(Users => Users.userId == userid).ToListAsync();
        }
    }
}
