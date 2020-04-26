using NBitcoin;

namespace X42.Networks
{
   public static class Networks
   {
      public static NetworksSelector X42
      {
         get
         {
            return new NetworksSelector(() => new X42Main(), () => new X42Test(), () => new X42RegTest());
         }
      }
   }
}
