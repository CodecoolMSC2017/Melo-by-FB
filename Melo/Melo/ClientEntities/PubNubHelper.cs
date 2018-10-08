using Newtonsoft.Json;
using PubnubApi;
using System;
using System.Windows;
using Melo;
namespace Melo.ClientEntities { 
    public class PubNubHelper
    {
        Pubnub pubnub;
        private readonly string ChannelName = "win-notification";
        public void Init()
        {
            //Init
            PNConfiguration pnConfiguration = new PNConfiguration
            {
                PublishKey = "pub-c-9e365308-4d42-4402-9e7c-8a34dc1a8829",
                SubscribeKey = "sub-c-5eff9c32-ca37-11e8-b02a-a6a8b6327be1",
                Secure = true
            };
            pubnub = new Pubnub(pnConfiguration);
            //Subscribe
            pubnub.Subscribe<string>()
           .Channels(new string[] {
                   ChannelName
           })
           .WithPresence()
           .Execute();
        }
        //Publish a message
        public void Publish()
        {
            JsonMsg Person = new JsonMsg
            {
                Name = "New File",
                Description = "A new file added to a selected directory",
                Date = DateTime.Now.ToString()
            };
            //Convert to string
            string arrayMessage = JsonConvert.SerializeObject(Person);
            pubnub.Publish()
                .Channel(ChannelName)
                .Message(arrayMessage)
                .Async(new PNPublishResultExt((result, status) => { }));
        }
        //listen to the channel
        public void Listen()
        {
            SubscribeCallbackExt listenerSubscribeCallack = new SubscribeCallbackExt(
            (pubnubObj, message) => {
                    //Call the notification windows from the UI thread
                    Application.Current.Dispatcher.Invoke(new Action(() => {
                        //Show the message as a WPF window message like WIN-10 toast
                        NotificationWindow ts = new NotificationWindow();
                        //Convert the message to JSON
                        JsonMsg bsObj = JsonConvert.DeserializeObject<JsonMsg>(message.Message.ToString());
                    string messageBoxText = "Name: " + bsObj.Name + Environment.NewLine + "Description: " + bsObj.Description + Environment.NewLine + "Date: " + bsObj.Date;
                    ts.NotifText.Text = messageBoxText;
                    ts.Show();
                }));
            },
            (pubnubObj, presence) => {
                    // handle incoming presence data
                },
            (pubnubObj, status) => {
                    // the status object returned is always related to subscribe but could contain
                    // information about subscribe, heartbeat, or errors
                    // use the PNOperationType to switch on different options
                });

            pubnub.AddListener(listenerSubscribeCallack);
        }
    }
}