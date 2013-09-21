using Cirrious.MvvmCross.Plugins.Messenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenlyLocal.Tests.Mocks
{
    public class MockIMvxMessenger : IMvxMessenger
    {

        public readonly List<MvxMessage> Messages = new List<MvxMessage>();
        public int CountSubscriptionsFor<TMessage>() where TMessage : MvxMessage
        {
            throw new NotImplementedException();
        }

        public int CountSubscriptionsForTag<TMessage>(string tag) where TMessage : MvxMessage
        {
            throw new NotImplementedException();
        }

        public IList<string> GetSubscriptionTagsFor<TMessage>() where TMessage : MvxMessage
        {
            throw new NotImplementedException();
        }

        public bool HasSubscriptionsFor<TMessage>() where TMessage : MvxMessage
        {
            throw new NotImplementedException();
        }

        public bool HasSubscriptionsForTag<TMessage>(string tag) where TMessage : MvxMessage
        {
            throw new NotImplementedException();
        }

        public void Publish(MvxMessage message, Type messageType)
        {
            Messages.Add(message);
        }

        public void Publish(MvxMessage message)
        {
            Messages.Add(message);
        }

        public void Publish<TMessage>(TMessage message) where TMessage : MvxMessage
        {
            Messages.Add(message);
        }

        public void RequestPurge(Type messageType)
        {
            throw new NotImplementedException();
        }

        public void RequestPurgeAll()
        {
            throw new NotImplementedException();
        }

        public MvxSubscriptionToken Subscribe<TMessage>(Action<TMessage> deliveryAction, MvxReference reference = MvxReference.Weak, string tag = null) where TMessage : MvxMessage
        {
            throw new NotImplementedException();
        }

        public MvxSubscriptionToken SubscribeOnMainThread<TMessage>(Action<TMessage> deliveryAction, MvxReference reference = MvxReference.Weak, string tag = null) where TMessage : MvxMessage
        {
            throw new NotImplementedException();
        }

        public MvxSubscriptionToken SubscribeOnThreadPoolThread<TMessage>(Action<TMessage> deliveryAction, MvxReference reference = MvxReference.Weak, string tag = null) where TMessage : MvxMessage
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe<TMessage>(MvxSubscriptionToken mvxSubscriptionId) where TMessage : MvxMessage
        {
            throw new NotImplementedException();
        }
    }
}
